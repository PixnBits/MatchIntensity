using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsFormsApplication1;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System.Diagnostics;

namespace MatchIntensityApplication
{
    class ExportToPDF
    {
        
        private string savedPath;

        private XGraphics gfx;
        private XFont font_gameLabel;
        private XFont font_gameText;
        private XFont font_RallyNumber;
        

        public bool export(Match matchData, DateTime fileName, System.Drawing.Image logo)
        {
            //TODO this is for testing only!!
            try
            {
                if (matchData.getGame(0).getRally(0) != null)
                    Console.WriteLine("Using user data");
            }
            catch (Exception e) {
                // no matchData data
                matchData = this.generateSampleMatchData(matchData);
                Console.WriteLine("Using random data");
            }


            // Create a new PDF document
            PdfDocument document = new PdfDocument();
            document.Info.Title = "Match Intensity Sheet, " + fileName.ToLongDateString();

            // Create an empty page
            PdfPage page = document.AddPage();
            // landscape letter
            page.Size = PageSize.Letter;
            page.Orientation = PageOrientation.Landscape;
            // now dims are 792 x 612 if at 72dpi

            // Get an XGraphics object for drawing
            this.gfx = XGraphics.FromPdfPage(page);

            // draw the Logo
            //XImage image = XImage.FromFile(IMAGE_logo);
            XImage image = XImage.FromGdiPlusImage(logo);

            // Left position in point
            double x = (250 - image.PixelWidth * 72 / image.HorizontalResolution) / 2;
            gfx.DrawImage(image, 35, 38, 100, 65);

            // Page Title
            XFont font_title = new XFont("Arial", 22, XFontStyle.Bold);
            gfx.DrawString(
                "Match Intensity Sheet", font_title, XBrushes.Black,
                new XRect(144, 36, 226, 16),
                XStringFormats.TopLeft
            );


            // TODO Date, Enterer, Location, Players, 
            XFont font_headerFields = new XFont("Arial", 12, XFontStyle.Regular);
            // Date
            gfx.DrawString(
                "Date "+matchData.data_date.ToShortDateString(),
                font_headerFields,
                XBrushes.Black,
                new XRect(390, 46, 100, 8),
                XStringFormats.TopLeft
            );

            // Enterer
            gfx.DrawString(
                "Enterer: " + matchData.data_enterer,
                font_headerFields,
                XBrushes.Black,
                new XRect(505, 46, 233, 10),
                XStringFormats.TopLeft
            );
            if (matchData.data_enterer == null || matchData.data_enterer.Length < 1)
                gfx.DrawLine(XPens.Black, 550, 57, 744, 57);
            // Event
            gfx.DrawString(
                "Event: " + matchData.data_event,
                font_headerFields,
                XBrushes.Black,
                new XRect(144, 68, 311, 11),
                XStringFormats.TopLeft
            );
            if (matchData.data_event == null || matchData.data_event.Length < 1)
                gfx.DrawLine(XPens.Black, 180, 81, 460, 81);
            // Location
            gfx.DrawString(
                "Location: " + matchData.data_location,
                font_headerFields,
                XBrushes.Black,
                new XRect(480, 68, 250, 11),
                XStringFormats.TopLeft
            );
            if (matchData.data_event == null || matchData.data_event.Length < 1)
                gfx.DrawLine(XPens.Black, 538, 81, 744, 81);
            // Players, label
            gfx.DrawString(
                "Players:",
                font_headerFields,
                XBrushes.Black,
                new XRect(144, 89, 125, 11),
                XStringFormats.TopLeft
            );
            // Players, vs.
            gfx.DrawString(
                "vs.",
                font_headerFields,
                XBrushes.Black,
                new XRect(469, 89, 30, 11),
                XStringFormats.TopLeft
            );
            // Players, player A
            if (matchData.data_playerA == null || matchData.data_playerA.Length < 1)
                gfx.DrawLine(XPens.Black, 192, 102, 460, 102);
            else
                gfx.DrawString(
                matchData.data_playerA,
                font_headerFields,
                XBrushes.Black,
                new XRect(192, 89, 250, 11),
                XStringFormats.Center
            );
            // player B line
            if (matchData.data_playerB == null || matchData.data_playerB.Length < 1)
                gfx.DrawLine(XPens.Black, 490, 102, 744, 102);
            else
                gfx.DrawString(
                matchData.data_playerB,
                font_headerFields,
                XBrushes.Black,
                new XRect(490, 89, 250, 11),
                XStringFormats.Center
            );



            // Match Summary
            gfx.DrawString(
                "MATCH SUMMARY",
                new XFont("Arial", 12, XFontStyle.Bold),
                XBrushes.Black,
                new XRect(35, 110, 109, 10),
                XStringFormats.TopLeft
            );

            XFont font_summaryFields = new XFont("Arial", 10, XFontStyle.Regular);

            //Match Start Time
            gfx.DrawString(
                "Match Start Time: " + matchData.getStartTime(),
                font_summaryFields,
                XBrushes.Black,
                new XRect(35, 129, 150, 10),
                XStringFormats.TopLeft
            );

            //Finish Time
            gfx.DrawString(
                "Finish Time: " + matchData.getFinishTime(),
                font_summaryFields,
                XBrushes.Black,
                new XRect(165, 129, 150, 10),
                XStringFormats.TopLeft
            );

            //# Games
            gfx.DrawString(
                "# Games: " + matchData.getCurrentGameNumber(),
                font_summaryFields,
                XBrushes.Black,
                new XRect(275, 129, 75, 10),
                XStringFormats.TopLeft
            );

            //# Shots
            gfx.DrawString(
                "# Shots: " + matchData.getTotalShotCount(),
                font_summaryFields,
                XBrushes.Black,
                new XRect(335, 129, 75, 10),
                XStringFormats.TopLeft
            );

            //# Rallies:
            gfx.DrawString(
                "# Rallies: " + matchData.getTotalRallyCount(),
                font_summaryFields,
                XBrushes.Black,
                new XRect(395, 129, 75, 10),
                XStringFormats.TopLeft
            );

            //Ave Shots/Rally
            gfx.DrawString(
                "Ave Shots/Rally: " + (matchData.getTotalShotCount() / (float)matchData.getTotalRallyCount()).ToString("0.0"),
                font_summaryFields,
                XBrushes.Black,
                new XRect(460, 129, 75, 10),
                XStringFormats.TopLeft
            );

            List<Rally> longestRallies = matchData.getLongestRallies();
            //Longest Rally:
            gfx.DrawString(
                "Longest Rally: " + (longestRallies[0].getShots()/(float)1000).ToString("0.00"),
                font_summaryFields,
                XBrushes.Black,
                new XRect(570, 129, 75, 10),
                XStringFormats.TopLeft
            );




            //* Second Line *//

            //Total Match Time
            gfx.DrawString(
                "Total Match Time: " + matchData.getTotalMatchTime(),
                font_summaryFields,
                XBrushes.Black,
                new XRect(35, 148, 150, 10),
                XStringFormats.TopLeft
            );

            //(Total) Play Time
            gfx.DrawString(
                "Play Time: " + (matchData.getTotalPlayTime() / (float)1000).ToString("00.00"),
                font_summaryFields,
                XBrushes.Black,
                new XRect(145, 148, 150, 10),
                XStringFormats.TopLeft
            );

            //(Total) Rally Break Time
            gfx.DrawString(
                "Rally Break Time: " + (matchData.getTotalBreakTime() / (float)1000).ToString("00.00"),
                font_summaryFields,
                XBrushes.Black,
                new XRect(245, 148, 150, 10),
                XStringFormats.TopLeft
            );

            //Avg Rally Break Time
            gfx.DrawString(
                "Ave Rally Break Time: " + ((matchData.getTotalBreakTime() / (float)1000) / (float)matchData.getTotalRallyCount()).ToString("0.00"),
                font_summaryFields,
                XBrushes.Black,
                new XRect(365, 148, 150, 10),
                XStringFormats.TopLeft
            );

            
            
            //* Third Line *//

            //MATCH INTENSITY VALUE
            gfx.DrawString(
                "MATCH INTENSITY VALUE: NaN",
                new XFont("Arial", 10, XFontStyle.Bold),
                XBrushes.Black,
                new XRect(35, 167, 150, 10),
                XStringFormats.TopLeft
            );

            //# Shots/Rally--
            gfx.DrawString(
                "# Shots/Rally--",
                new XFont("Arial", 10, XFontStyle.Italic),
                XBrushes.Black,
                new XRect(204, 167, 150, 10),
                XStringFormats.TopLeft
            );

            //30+: 00
            gfx.DrawString(
                "30+: " + matchData.getShotsPerRally(30, Int32.MaxValue),
                font_summaryFields,
                XBrushes.Black,
                new XRect(274, 167, 150, 10),
                XStringFormats.TopLeft
            );

            //20-29
            gfx.DrawString(
                "20-29: " + matchData.getShotsPerRally(20, 29),
                font_summaryFields,
                XBrushes.Black,
                new XRect(320, 167, 150, 10),
                XStringFormats.TopLeft
            );

            //10-19
            gfx.DrawString(
                "10-19: " + matchData.getShotsPerRally(10, 19),
                font_summaryFields,
                XBrushes.Black,
                new XRect(370, 167, 150, 10),
                XStringFormats.TopLeft
            );

            //5-9
            gfx.DrawString(
                "5-9: " + matchData.getShotsPerRally(5, 9),
                font_summaryFields,
                XBrushes.Black,
                new XRect(420, 167, 150, 10),
                XStringFormats.TopLeft
            );

            //1-4
            gfx.DrawString(
                "1-4: " + matchData.getShotsPerRally(1, 4),
                font_summaryFields,
                XBrushes.Black,
                new XRect(455, 167, 150, 10),
                XStringFormats.TopLeft
            );

            //2nd Longest
            gfx.DrawString(
                "2nd Longest: " + (longestRallies[1].getShots() / (float)1000).ToString("0.00"),
                font_summaryFields,
                XBrushes.Black,
                new XRect(490, 167, 150, 10),
                XStringFormats.TopLeft
            );

            //3rd Longest
            gfx.DrawString(
                "3rd Longest: " + (longestRallies[2].getShots() / (float)1000).ToString("0.00"),
                font_summaryFields,
                XBrushes.Black,
                new XRect(585, 167, 150, 10),
                XStringFormats.TopLeft
            );

            //4th Longest
            gfx.DrawString(
                "4th Longest: " + (longestRallies[3].getShots() / (float)1000).ToString("0.00"),
                font_summaryFields,
                XBrushes.Black,
                new XRect(680, 167, 150, 10),
                XStringFormats.TopLeft
            );









            // Draw each game
            this.font_gameLabel = new XFont("Arial", 10, XFontStyle.Bold);
            this.font_gameText = new XFont("Arial", 8, XFontStyle.Regular);
            this.font_RallyNumber = new XFont("Arial", 8, XFontStyle.Underline);

            int gameY = 194;
            for (int gameIndex = 0; gameIndex < matchData.getCurrentGameNumber(); gameIndex++)
            {
                drawGame(matchData.getGame(gameIndex), gameIndex, 35, gameY);
                gameY += 130;
            }

            // Save the document...
            try
            {
                this.savedPath = fileName.Year + "." + fileName.Month+"."+fileName.Day+"_" + fileName.Hour + "." + fileName.Minute + ".pdf";
                // TODO don't overwrite
                document.Save(savedPath);
                // ...and start a viewer.
                Process.Start(savedPath);
                return true;
            }
            catch (NotSupportedException e)
            {
                Console.WriteLine("ERROR! NotSupportedException (bad filename?): ", e);
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR! General Exception: ", e);
            }

            return false;
        }

        private Match generateSampleMatchData( Match orig = null)
        {

            Random rand = new Random();
            int tmpInt_a, tmpInt_b;

            Match matchData = new Match();

            if (orig != null)
            {
                matchData.data_date = orig.data_date;
                matchData.data_enterer = orig.data_enterer;
                matchData.data_event = orig.data_event;
                matchData.data_location = orig.data_location;
                matchData.data_playerA = orig.data_playerA;
                matchData.data_playerB = orig.data_playerB;
            }

            matchData.startRally();
            matchData.addShot();
            matchData.addShot();
            System.Threading.Thread.Sleep(300);
            matchData.endRally();
            System.Threading.Thread.Sleep(100);

            matchData.startRally();
            matchData.addShot();
            matchData.addShot();
            matchData.addShot();
            matchData.addShot();
            System.Threading.Thread.Sleep(400);
            matchData.endRally();
            System.Threading.Thread.Sleep(100);

            matchData.endGame();
            System.Threading.Thread.Sleep(100);

            matchData.startRally();
            matchData.addShot();
            matchData.addShot();
            matchData.addShot();
            matchData.addShot();
            matchData.addShot();
            System.Threading.Thread.Sleep(250);
            matchData.endRally();
            System.Threading.Thread.Sleep(100);

            matchData.startRally();
            matchData.addShot();
            matchData.addShot();
            matchData.addShot();
            matchData.addShot();
            matchData.addShot();
            matchData.addShot();
            matchData.addShot();
            matchData.addShot();
            System.Threading.Thread.Sleep(800);
            matchData.endRally();
            System.Threading.Thread.Sleep(100);

            /*
            tmpInt_a = rand.Next(57);
            int delay = rand.Next(200, 275);
            for (int i = 1; i < tmpInt_a; i++)
            {
                
                matchData.startRally();
                System.Threading.Thread.Sleep(delay);

                tmpInt_b = rand.Next(5, 16);
                for (int ii = 0; ii < tmpInt_b; ii++)
                {
                    matchData.addShot();
                    //System.Threading.Thread.Sleep(delay);
                }
                matchData.endRally();
                System.Threading.Thread.Sleep(delay);
            }
            //*/

            matchData.endMatch();

            return matchData;
        }

        internal string getExportPath()
        {
            return this.savedPath;
        }

        private void drawGame(Game game, int index, int startX, int startY)
        {

            if (game == null)
                return;

            gfx.DrawLine(XPens.Gray, startX, startY - 8, 792-35, startY - 8);

            // Game label
            gfx.DrawString(
                "GAME " + (index+1),
                font_gameLabel,
                XBrushes.Black,
                new XRect(startX, startY, 46, 8), // orig 194
                XStringFormats.TopLeft
            );

            // stats
            XFont font_gameStats = new XFont("Arial", 8, XFontStyle.Regular);

            //Game Break: 00:00
            gfx.DrawString(
                "Game Break: --:--",
                font_gameStats,
                XBrushes.Black,
                new XRect(startX + 50 * 1, startY + 2, 150, 10),
                XStringFormats.TopLeft
            );

            //Game Time
            gfx.DrawString(
                "Game Time: " + (game.getTotalTime() / (float)1000).ToString("0.00"),
                font_gameStats,
                XBrushes.Black,
                new XRect(startX + 125, startY + 2, 150, 10),
                XStringFormats.TopLeft
            );

            //Play:
            gfx.DrawString(
                "Play: " + (game.getTotalPlayTime() / (float)1000).ToString("0.00"),
                font_gameStats,
                XBrushes.Black,
                new XRect(startX + 195, startY + 2, 150, 10),
                XStringFormats.TopLeft
            );

            //Rally Breaks
            gfx.DrawString(
                "Rally Breaks: " + (game.getTotalRestTime()/(float)1000).ToString("0.00"),
                font_gameStats,
                XBrushes.Black,
                new XRect(startX + 250, startY + 2, 150, 10),
                XStringFormats.TopLeft
            );

            //% Play: 00.0%
            gfx.DrawString(
                "% Play: " + (100 * game.getTotalPlayTime() / (float)game.getTotalTime()).ToString("00.0") + "%",
                font_gameStats,
                XBrushes.Black,
                new XRect(startX + 330, startY + 2, 150, 10),
                XStringFormats.TopLeft
            );

            //# Shots
            gfx.DrawString(
                "# Shots: " + game.getTotalShots(),
                font_gameStats,
                XBrushes.Black,
                new XRect(startX + 392, startY + 2, 150, 10),
                XStringFormats.TopLeft
            );

            //# Rallies
            gfx.DrawString(
                "# Rallies: " + game.getTotalRallies(),
                font_gameStats,
                XBrushes.Black,
                new XRect(startX + 446, startY + 2, 150, 10),
                XStringFormats.TopLeft
            );

            //Longest Rally
            gfx.DrawString(
                "Longest Rally: " + (game.getLongestRally()/ (float)1000).ToString("0.00"),
                font_gameStats,
                XBrushes.Black,
                new XRect(startX + 498, startY + 2, 150, 10),
                XStringFormats.TopLeft
            );

            //# Shots/Rally
            gfx.DrawString(
                "# Shots/Rally: " + (game.getTotalShots() / (float)game.getTotalRallies() ).ToString("0.0"),
                font_gameStats,
                XBrushes.Black,
                new XRect(startX + 575, startY + 2, 150, 10),
                XStringFormats.TopLeft
            );

            //Time/Shot
            gfx.DrawString(
                "Time/Shot: " + (100 * game.getTotalShots() / (float)game.getTotalPlayTime()).ToString("0.0") + "/sec",
                font_gameStats,
                XBrushes.Black,
                new XRect(startX + 645, startY + 2, 150, 10),
                XStringFormats.TopLeft
            );



            int rallyX = 77;
            int rallyY = startY + 21;

            this.drawRallyLabels(startX, rallyY);

            for (int rallyIndex = 0; rallyIndex < game.getCurrentRallyNumber(); rallyIndex++)
            {
                drawRally(game, rallyIndex, rallyX, rallyY);
                rallyX += 22;
                if (rallyIndex == 29)
                {
                    rallyX = 77;
                    rallyY += 50;
                    this.drawRallyLabels(startX, rallyY);
                }
            }
            // Totals
            gfx.DrawString(
                "TOTALS",
                this.font_RallyNumber,
                XBrushes.Black,
                new XRect(737, startY+71, 20, 8),
                XStringFormats.Center
            );
            // # Shots
            gfx.DrawString(
                game.getTotalShots().ToString(),
                this.font_gameText,
                XBrushes.Black,
                new XRect(737, startY + 71 + 9, 20, 8),
                XStringFormats.Center
            );
            // Rally Sec
            gfx.DrawString(
                (game.getPlayTime()/(float)1000).ToString("0.00"),
                this.font_gameText,
                XBrushes.Black,
                new XRect(737, startY + 71 + 9 * 2, 20, 8),
                XStringFormats.Center
            );
            // Shots/sec
            gfx.DrawString(
                ((game.getTotalShots()*1000)/(float)game.getPlayTime()).ToString("0.00"),
                this.font_gameText,
                XBrushes.Black,
                new XRect(737, startY + 71 + 9 * 3, 20, 8),
                XStringFormats.Center
            );
            // Sec after
            gfx.DrawString(
                (game.getTotalRestTime()/(float)1000).ToString("0.00"),
                this.font_gameText,
                XBrushes.Black,
                new XRect(737, startY + 71 + 9 * 4, 20, 8),
                XStringFormats.Center
            );

        }

        private void drawRallyLabels(int rallyX, int rallyY)
        {
            // Rally Labels:
            gfx.DrawString(
                "Rally #:",
                font_gameText,
                XBrushes.Black,
                new XRect(rallyX, rallyY, 46, 8),
                XStringFormats.TopLeft
            );

            gfx.DrawString(
                "# Shots:",
                font_gameText,
                XBrushes.Black,
                new XRect(rallyX, rallyY + 9, 46, 8),
                XStringFormats.TopLeft
            );

            gfx.DrawString(
                "Rally sec:",
                font_gameText,
                XBrushes.Black,
                new XRect(rallyX, rallyY + 9 * 2, 46, 8),
                XStringFormats.TopLeft
            );

            gfx.DrawString(
                "Shots/sec:",
                font_gameText,
                XBrushes.Black,
                new XRect(rallyX, rallyY + 9 * 3, 46, 8),
                XStringFormats.TopLeft
            );

            gfx.DrawString(
                "Sec after:",
                font_gameText,
                XBrushes.Black,
                new XRect(rallyX, rallyY + 9 * 4, 46, 8),
                XStringFormats.TopLeft
            );
        }

        private void drawRally(Game game, int index, int x, int startY)
        {
            Rally ral = game.getRally(index);
            // Rally #
            gfx.DrawString(
                (index+1).ToString(),
                this.font_RallyNumber,
                XBrushes.Black,
                new XRect(x, startY, 20, 8),
                XStringFormats.Center
            );
            // # Shots
            gfx.DrawString(
                ral.getShots().ToString(),
                this.font_gameText,
                XBrushes.Black,
                new XRect(x, startY + 9, 20, 8),
                XStringFormats.Center
            );
            // Rally Sec
            gfx.DrawString(
                (ral.getDuration() / (float)1000).ToString("0.00"),
                this.font_gameText,
                XBrushes.Black,
                new XRect(x, startY + 2 * 9, 20, 8),
                XStringFormats.Center
            );
            // Shots/sec
            gfx.DrawString(
                ((ral.getShots()*1000)/(float)ral.getDuration()).ToString("0.00"),
                this.font_gameText,
                XBrushes.Black,
                new XRect(x, startY + 3 * 9, 20, 8),
                XStringFormats.Center
            );
            // Sec after
            gfx.DrawString(
                (game.getRestTime(index) / (float)1000).ToString("0.00"),
                this.font_gameText,
                XBrushes.Black,
                new XRect(x, startY + 4 * 9, 20, 8),
                XStringFormats.Center
            );
        }
    }
}

