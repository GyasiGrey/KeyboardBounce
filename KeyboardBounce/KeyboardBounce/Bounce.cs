using CUE.NET;
using CUE.NET.Devices.Generic.Enums;
using CUE.NET.Devices.Keyboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyboardBounce
{
    public class Bounce
    {
        private int X;
        private int Y;

        private const int MaxX = 13;
        private const int MaxY = 4;

        private int DirectionX = 1;
        private int DirectionY = 1;

        private CorsairKeyboard keyboard;

        private CorsairLedId[,] ledMap;

        public Bounce()
        {
            CueSDK.Initialize();
            CueSDK.UpdateMode = CUE.NET.Devices.Generic.Enums.UpdateMode.Continuous;
            CueSDK.UpdateFrequency = 1f / 30f;

            keyboard = CueSDK.KeyboardSDK;

            ledMap = new CorsairLedId[MaxX + 1, MaxY + 1];

            ledMap[0, 0] = CorsairLedId.GraveAccentAndTilde;
            ledMap[1, 0] = CorsairLedId.D1;
            ledMap[2, 0] = CorsairLedId.D2;
            ledMap[3, 0] = CorsairLedId.D3;
            ledMap[4, 0] = CorsairLedId.D4;
            ledMap[5, 0] = CorsairLedId.D5;
            ledMap[6, 0] = CorsairLedId.D6;
            ledMap[7, 0] = CorsairLedId.D7;
            ledMap[8, 0] = CorsairLedId.D8;
            ledMap[9, 0] = CorsairLedId.D9;
            ledMap[10, 0] = CorsairLedId.D0;
            ledMap[11, 0] = CorsairLedId.MinusAndUnderscore;
            ledMap[12, 0] = CorsairLedId.EqualsAndPlus;
            ledMap[13, 0] = CorsairLedId.Backspace;

            ledMap[0, 1] = CorsairLedId.Tab;
            ledMap[1, 1] = CorsairLedId.Q;
            ledMap[2, 1] = CorsairLedId.W;
            ledMap[3, 1] = CorsairLedId.E;
            ledMap[4, 1] = CorsairLedId.R;
            ledMap[5, 1] = CorsairLedId.T;
            ledMap[6, 1] = CorsairLedId.Y;
            ledMap[7, 1] = CorsairLedId.U;
            ledMap[8, 1] = CorsairLedId.I;
            ledMap[9, 1] = CorsairLedId.O;
            ledMap[10, 1] = CorsairLedId.P;
            ledMap[11, 1] = CorsairLedId.BracketLeft;
            ledMap[12, 1] = CorsairLedId.BracketRight;
            ledMap[13, 1] = CorsairLedId.Backslash;

            ledMap[0, 2] = CorsairLedId.CapsLock;
            ledMap[1, 2] = CorsairLedId.A;
            ledMap[2, 2] = CorsairLedId.S;
            ledMap[3, 2] = CorsairLedId.D;
            ledMap[4, 2] = CorsairLedId.F;
            ledMap[5, 2] = CorsairLedId.G;
            ledMap[6, 2] = CorsairLedId.H;
            ledMap[7, 2] = CorsairLedId.J;
            ledMap[8, 2] = CorsairLedId.K;
            ledMap[9, 2] = CorsairLedId.K;
            ledMap[10, 2] = CorsairLedId.L;
            ledMap[11, 2] = CorsairLedId.SemicolonAndColon;
            ledMap[12, 2] = CorsairLedId.ApostropheAndDoubleQuote;
            ledMap[13, 2] = CorsairLedId.Enter;

            ledMap[0, 3] = CorsairLedId.LeftShift;
            ledMap[1, 3] = CorsairLedId.Z;
            ledMap[2, 3] = CorsairLedId.X;
            ledMap[3, 3] = CorsairLedId.C;
            ledMap[4, 3] = CorsairLedId.V;
            ledMap[5, 3] = CorsairLedId.B;
            ledMap[6, 3] = CorsairLedId.N;
            ledMap[7, 3] = CorsairLedId.M;
            ledMap[8, 3] = CorsairLedId.CommaAndLessThan;
            ledMap[9, 3] = CorsairLedId.PeriodAndBiggerThan;
            ledMap[10, 3] = CorsairLedId.SlashAndQuestionMark;
            ledMap[11, 3] = CorsairLedId.MinusAndUnderscore;
            ledMap[12, 3] = CorsairLedId.RightShift;
            ledMap[13, 3] = CorsairLedId.RightShift;

            ledMap[0, 4] = CorsairLedId.LeftCtrl;
            ledMap[1, 4] = CorsairLedId.LeftLogo;
            ledMap[2, 4] = CorsairLedId.LeftAlt;
            ledMap[3, 4] = CorsairLedId.Space;
            ledMap[4, 4] = CorsairLedId.Space;
            ledMap[5, 4] = CorsairLedId.Space;
            ledMap[6, 4] = CorsairLedId.Space;
            ledMap[7, 4] = CorsairLedId.Space;
            ledMap[8, 4] = CorsairLedId.Space;
            ledMap[9, 4] = CorsairLedId.Space;
            ledMap[10, 4] = CorsairLedId.RightAlt;
            ledMap[11, 4] = CorsairLedId.RightLogo;
            ledMap[12, 4] = CorsairLedId.RightGui;
            ledMap[13, 4] = CorsairLedId.RightCtrl;
        }

        public void Tick()
        {
            X += DirectionX;
            Y += DirectionY;

            if(X > MaxX)
            {
                DirectionX = -DirectionX;
                X = MaxX;
            }

            if (Y > MaxY)
            {
                DirectionY = -DirectionY;
                Y = MaxY;
            }

            if (X < 0)
            {
                DirectionX = -DirectionX;
                X = 0;
            }

            if (Y < 0)
            {
                DirectionY = -DirectionY;
                Y = 0;
            }

            SetLEDs();
        }

        private void SetLEDs()
        {
            for(int loopX = 0; loopX < MaxX; loopX++)
            {
                for(int loopY = 0; loopY < MaxY; loopY++)
                {
                    if(X == loopX && Y == loopY)
                    {
                        keyboard[ledMap[loopX, loopY]].Color = new CUE.NET.Devices.Generic.CorsairColor(255, 255, 255); 
                    }
                    else
                    {
                        keyboard[ledMap[loopX, loopY]].Color = new CUE.NET.Devices.Generic.CorsairColor(0, 0, 0);
                    }
                }
            }
        }
    }
}
