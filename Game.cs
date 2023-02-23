
using System.Runtime.CompilerServices;

public class App
{
    
    //bool of game
    public static bool status = true;
    public static int score = 0;

    //data of console`s width and height
    public static int windowHeight = Console.WindowHeight - 2;
    public static int windowWidth = Console.WindowWidth;

    //main starsArr of string with stars(#) and empty symbols( )
    public static string[][] arrayOfStars = new string[windowHeight][];

    public static void Main()
        
    {
        Console.CursorVisible= false;
        Menu.StartMenu();
    }
    public static void Game()
    {

        while (status)
        {
            Console.Clear();
            Console.CursorVisible = false;
            Stars.SetStarsOnTheMap();
            SpaceShip.WriteTheShip();
            WriteScore();
            ConsoleKeyInfo key = Console.ReadKey();
            GameControls.ControlShip(key);
            if (key.Key == ConsoleKey.Q)
            {
                status = false;
                Console.Clear();
                Menu.PauseMenu();
            }
            GameControls.ArrayOfStarsRewrite();
            GameControls.ArrayOfStarsCheck(SpaceShip.SpaceShipArray, arrayOfStars);
            //Console.Clear();

            static void WriteScore()
            {
                Console.SetCursorPosition(windowWidth - (windowWidth - 5), windowHeight);
                Console.Write($"You`r score:{score}");
            }
        }

    }
    public class SpaceShip

    {   //coordinates of ship;
        public static int x = windowWidth / 2;
        public static int y = windowHeight - 3;
        public static int[] shipA;
        public static int[] shipB;
        public static int[] shipC;
        public static int[] shipD;
        public static int[] shipE;
        public static int[] shipF;
        public static int[][] SpaceShipArray;
        //the sighn of the ship part
        public static string shipSighn = "\u25A1";
        //main method for writing of the ship
        public static void WriteTheShip(int x, int y)
        {
            SetShipCoord(x, y);
            WritePoint();
        }
        public static void WriteTheShip()
        {
            SetShipCoord(x, y);
            WritePoint();
        }
        //write every point of the ship
        public static void WritePoint()
        {
            for (int j = 0; j < SpaceShipArray.Length; j++)
            {
                int xWay = SpaceShipArray[j][0];
                int yWay = SpaceShipArray[j][1];
                Console.OutputEncoding = System.Text.Encoding.Unicode;
                Console.SetCursorPosition(xWay, yWay);
                Console.Write(shipSighn);
            }

        }
        //set coordinates for points of the ship
        public static void SetShipCoord(int xNum, int yNum)
        {
            x = xNum;
            y = yNum;

            shipA = new[] { x, y };
            shipB = new[] { x - 1, y + 1 };
            shipC = new[] { x, y + 1 };
            shipD = new[] { x + 1, y + 1 };
            shipE = new[] { x - 1, y + 2 };
            shipF = new[] { x + 1, y + 2 };

            SpaceShipArray = new[] { shipA, shipB, shipC, shipD, shipE, shipF };




        }

    }

    private class Stars
    {

        public static void SetStarsOnTheMap()
        {
            Stars.StarGenerator();
            for (int i = 0; i < arrayOfStars.Length; i++)
            {
                if (arrayOfStars[i] != null)
                {
                    foreach (string s in arrayOfStars[i])
                    {
                        Console.OutputEncoding = System.Text.Encoding.Unicode;
                        Console.Write(s);
                    }
                }
            }
        }
        //generate and write the generated stings into the arry (ArrayOfStars)
        public static void StarGenerator()
        {
            //making a list for filling into the method
            List<string> list = new List<string>();
            //making lenght of line
            for (int i = 0; i < windowWidth; i++)
            {
                ////generate random int for chrar
                Random rnd = new Random();
                int randomNum = rnd.Next(0, 20);
                ////select and adding of a char
                if (randomNum < 19)
                //if (countAr < maxCount)
                {
                    list.Add(" ");
                }
                else
                {
                    list.Add("*");
                }
            }
            //writing the new generated line to 
            arrayOfStars[0] = list.ToArray();
        }
    }
    private class AppControls
    {
        public static int MenuSelection(int position, string[] menuArray, ConsoleKeyInfo key)
        {
            ConsoleKeyInfo keyInfo = key;
            if (keyInfo.Key == ConsoleKey.UpArrow)
            {

                int newPos = position - 1;
                if (newPos >= 0 && newPos < menuArray.Length)
                {
                    return newPos;
                }
                else
                {
                    return position;
                }

            }
            else if (keyInfo.Key == ConsoleKey.DownArrow)
            {
                int newPos = position + 1;
                if (newPos >= 0 && newPos < menuArray.Length)
                {
                    return newPos;
                }
                else
                {
                    return position;
                }

            }
            else { return position; };
        }
    }
    class GameControls
    {
        public static void ArrayOfStarsCheck(int[][] shipArray, string[][] starsArr)
        {
            for (int j = 0; j < shipArray.Length; j++)
            {
                int xWay = shipArray[j][0];
                int yWay = shipArray[j][1];
                int starXWay;
                int starYWay;
                for (int i = 0; i < starsArr.Length; i++)
                {
                    starYWay = i;
                    if (starsArr[i] != null)
                    {
                        for (int k = 0; k < starsArr[i].Length; k++)
                        {
                            starXWay = k;
                            if (xWay == starXWay && yWay == starYWay)
                            {
                                if (starsArr[i][k] == "*")
                                {
                                    //Console.WriteLine(xWay + " " + yWay + " " + starXWay + " " + starYWay);

                                    Console.Clear();
                                    status = false;
                                    arrayOfStars = new string[windowHeight][];
                                    SpaceShip.x = windowWidth / 2;
                                    SpaceShip.y = windowHeight - 3;
                                    Menu.GameOver();
                                }
                            }

                        }
                    }
                    else
                    {
                        continue;
                    }

                }

            }
        }
        public static void ArrayOfStarsRewrite()
        {
            for (int i = arrayOfStars.Length - 1; i > 0; i--)
            {
                arrayOfStars[i] = arrayOfStars[i - 1];
            };
            score++;
        }
        public static void ControlShip(ConsoleKeyInfo keyInfo)
        {
            if (keyInfo.Key == ConsoleKey.LeftArrow && SpaceShip.shipB[0] > 0)
            {
                SpaceShip.x--;
            }
            if (keyInfo.Key == ConsoleKey.RightArrow && SpaceShip.shipD[0] > 0)
            {
                SpaceShip.x++;
            }
        }

    }
    private class Menu
    {
        private static void WriteMenu(int menuPosition, string[] menu)
        {
            for (int i = 0; i < menu.Length; i++)
            {
                if (i == menuPosition)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(menu[i]);
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine(menu[i]);
                }

            }

        }
        public static void StartMenu()
        {
            int position = 0;
            bool condition = true;
            while (condition)
            {
                Console.Clear();
                string[] startArray = new string[]
                 {
                "Start new game",
                "Info",
                "Exit game"
                 };
                WriteMenu(position, startArray);
                ConsoleKeyInfo key = Console.ReadKey();
                position = AppControls.MenuSelection(position, startArray, key);
                if (key.Key == ConsoleKey.Enter)
                {
                    if (position == 0)
                    {
                        Console.Clear();
                        condition = false;
                        status = true;
                        Game();
                    }
                    if (position == 1)
                    {
                        Console.Clear();
                        Console.WriteLine("Information");
                        Console.WriteLine("Use aroows: " +
                            "LeftArrow and RightArrow for controll space ship" +
                            "\n\nPress Q for pause" +
                            "\n\nPress any button for back");

                        Console.ReadKey();
                        StartMenu();
                    }
                    if (position == 2)
                    {
                        Console.Clear();
                        Environment.Exit(0);

                    }

                }
            }

        }
        public static void PauseMenu()
        {
            int position = 0;
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"Pause\nYour score:{score}\n\n");
                string[] resume = new string[]
                         {
                "Resume the game",
                "Exit the game"
                         };
                WriteMenu(position, resume);
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                position = AppControls.MenuSelection(position, resume, keyInfo);
                if (keyInfo.Key == ConsoleKey.Enter)
                {
                    if (position == 0)
                    {
                        Console.Clear();
                        status = true;
                        Game();
                        
                    }
                    if (position == 1)
                    {
                        Console.Clear();
                        Environment.Exit(0);
                    }
                }
            }


        }
        public static void GameOver()
        {
            int position = 0;
            bool condition = true;
            while (condition)
            {

                Console.Clear();
                Console.WriteLine($"Game Over\nYour score:{score}\n\n");

                string[] resume = new string[]
                         {
                "New game",
                "Exit the game"
                         };
                WriteMenu(position, resume);
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                position = AppControls.MenuSelection(position, resume, keyInfo);
                if (keyInfo.Key == ConsoleKey.Enter)
                {
                    if (position == 0)
                    {
                        Console.Clear();
                        status = true;
                        condition = false;
                        score = 0;
                        Game();
                    }
                    if (position == 1)
                    {
                        Console.Clear();
                        Environment.Exit(0);
                    }
                }
            }

        }
    }
}

