using System;
using System.Collections.Generic;

namespace Snake
{
    public class ScreenBuffer
    {
        public int Width;
        public int Height;
        
        //initiate important variables
        public char[,] initialScreenBufferArray; //main buffer array
        public char[,] screenBufferArray; //main buffer array
        public string screenBuffer; //buffer as string (used when drawing)
        public Char[] arr; //temporary array for drawing string
        public int i = 0; //keeps track of the place in the array to draw to

        public ScreenBuffer()
        {
            OpenAlternateBuffer();
            HideCursor();
            Width = Console.WindowWidth;
            Height = Console.WindowHeight;
            
            // Console.SetWindowSize(roomWidth, roomHeight);
            
            initialScreenBufferArray = new char[Width, Height];
            
            for (int iy = 0; iy < Height-1; iy++)
            {
                for (int ix = 0; ix < Width; ix++)
                {
                    initialScreenBufferArray[ix, iy] = ' ';
                }
            }

            screenBufferArray = (char[,])initialScreenBufferArray.Clone();
        }

        public void Fill(char fillChar)
        {
            for (int iy = 0; iy < Height-1; iy++)
            {
                for (int ix = 0; ix < Width; ix++)
                {
                    initialScreenBufferArray[ix, iy] = fillChar;
                }
            }
        }

        public void OpenAlternateBuffer()
        {
            Console.Write("\x1b[?1049h");
            Console.Clear();
        }

        public void CloseAlternateBuffer()
        {
            Console.Clear();
            Console.Write("\x1b[?1049l");
        }
        
        public void ShowCursor()
        {
            Console.Write("\x1b[?25h");
        }
        
        public void HideCursor()
        {
            Console.Write("\x1b[?25l");
        }
        
        //this method takes a string, and a pair of coordinates and writes it to the buffer
        public void Draw(string text, int x, int y)
        {
            //split text into array
            arr = text.ToCharArray(0, text.Length);
            //iterate through the array, adding values to buffer 
            i = 0;
            foreach (char c in arr)
            {
                screenBufferArray[x + i,y] = c;
                i++;
            }   
        }
 
        public void DrawScreen()
        {
            
            screenBuffer = string.Empty;
            
            //iterate through buffer, adding each value to screenBuffer
            for (int iy = 0; iy < Height-1; iy++)
            {
                for (int ix = 0; ix < Width; ix++)
                {
                    screenBuffer += screenBufferArray[ix, iy];
                }
            }
            
            //set cursor position to top left and draw the string
            Console.SetCursorPosition(0, 0);
            Console.Write(screenBuffer);
            
            screenBufferArray = (char[,])initialScreenBufferArray.Clone();
            //note that the screen is NOT cleared at any point as this will simply overwrite the existing values on screen. Clearing will cause flickering again.
        }
    }
}