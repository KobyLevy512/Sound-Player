

using System;
using System.Drawing;

namespace WindowsFormsApp1
{
    class Circle
    {
        int AnimState = 255;
        bool UpAnim = false;
        float locX, locY;
        public void Paint(Graphics e)
        {
            Random rnd = new Random();
            if (UpAnim)
            {
                AnimState++;
                for (int i = 0; i < AnimState; i++)
                {
                    e.DrawEllipse(new Pen(Color.FromArgb((byte)(100.0 / i), rnd.Next(0, (i + 50) % 255), rnd.Next(0, 255 - i % 255), 100), i / 15), locX + 260, locY + 180, (i + 1) / 2, (i + 1) / 2);
                }
                if(AnimState >= 255)
                {
                    UpAnim = false;
                }
                locX -= 0.2f;
                locY -= 0.4f;
            }
            else
            { 
                for (int i = 0; i < AnimState; i++)
                {
                    e.DrawEllipse(new Pen(Color.FromArgb((byte)(i / 100), rnd.Next(0, (i + 50) % 255), rnd.Next(0, 255 - i % 255), 100), i / 15), locX + 220, locY + 150, (i + 1) / 2, (i + 1) / 2);
                }
                if (--AnimState <= 0)
                {
                    AnimState = 0;
                    locX = 0;
                    locY = 0;
                    UpAnim = true;
                }
                locX += 0.2f;
                locY -= 0.4f;
            }
        }
    }
}
