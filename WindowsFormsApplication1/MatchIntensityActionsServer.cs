using Coho.IpcLibrary;
using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using WindowsFormsApplication1;

namespace MatchIntensityApplication
{
    class MatchIntensityActionsServer : IpcCallback
    {
        //TODO: sit down and think about the protocol, possibilities:
        //   JSON formatted strings
        //   NMEA strings, like GPS (NMEA 0183, ex: http://aprs.gids.nl/nmea/)

        // these will/should be removed for better/proper useage of `state`
        public static int STATE_CONNECTED = 1;
        public static int STATE_DISCONNECTED = 2;
        public static int STATE_GOT_MESSAGE = 4;

        IpcServer commServer;
        Match matchData;

        public void start()
        {
            commServer = new IpcServer("MatchIntensityActions", this, 1);
            Console.WriteLine("Started MatchIntensityActionsServer");
        }

        public void OnAsyncConnect(PipeStream pipe, out Object state)
        {
            Console.WriteLine("MatchIntensityActionsServer: client connected");

            state = STATE_CONNECTED;
        }

        public void OnAsyncDisconnect(PipeStream pipe, Object state)
        {
            Console.WriteLine("MatchIntensityActionsServer: client disconnected");

            state = STATE_DISCONNECTED;
        }

        public void OnAsyncMessage(PipeStream pipe, Byte[] data, Int32 bytes, Object state)
        {
            string dataString = System.Text.Encoding.UTF8.GetString(data, 0, bytes);

            Console.WriteLine("MatchIntensityActionsServer: client sent command: {0}", dataString);

            bool handled = this.handleMessage(dataString);

            string replyString;
            if (handled)
            {
                replyString = "Success";
            }else{
                replyString = "Error";
            }
            byte[] reply = System.Text.Encoding.UTF8.GetBytes(replyString);
            pipe.Write(reply, 0, reply.Length);
            Console.WriteLine("Sent client response: {0}", replyString);

            state = STATE_GOT_MESSAGE;
        }

        private bool handleMessage(string msg){
            if (null == matchData) {
                return false;
            }

            if (msg.IndexOf("Action:") == 0)
            {
                // action requested
                string action = msg.Replace("Action:", "").Trim();
                Console.WriteLine("perform action! {0}", action);
                switch (action)
                {
                    case "shot":
                        matchData.addShot();
                        return true;
                        break;
                    default:
                        break;
                }
            }

            return false;
        }

        internal void connectToMatch(Match match)
        {
            matchData = match;
        }

        internal void end()
        {
            if (null != commServer)
            {
                commServer.IpcServerStop();
            }
        }
    }
}
