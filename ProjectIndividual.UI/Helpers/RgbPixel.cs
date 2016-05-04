using System;

namespace ProjectIndividual.UI.Helpers
{
    public class RgbPixel
    {
        public byte Red;
        public byte Green;
        public byte Blue;

        public byte Alpha;

        public RgbPixel()
        {
            Red = 0;
            Green = 0;
            Blue = 0;
            Alpha = 0;
        }

        public RgbPixel(byte r, byte g, byte b)
        {
            Red = r;
            Green = g;
            Blue = b;
            Alpha = 0;
        }
        public RgbPixel(int r, int g, int b)
        {
            Red = PutInRange(r);
            Green = PutInRange(g);
            Blue = PutInRange(b);
            Alpha = 0;

        }

        public RgbPixel Invert()
        {
            RgbPixel pixel = new RgbPixel();
            pixel.Red = (byte)(255 - this.Red);
            pixel.Green = (byte)(255 - this.Green);
            pixel.Blue = (byte)(255 - this.Blue);
            return pixel;
        }

        public RgbPixel IncreaseValues(float val)
        {
            byte r = PutInRange(this.Red + val);//  this.Red + val >= 255 ? (byte)255 : (byte)(this.Red + val);
            byte g = PutInRange(this.Green + val);//this.Green + val >= 255 ? (byte)255 : (byte)(this.Green + val);
            byte b = PutInRange(this.Blue + val);// this.Blue + val >= 255 ? (byte)255 : (byte)(this.Blue + val);

            return new RgbPixel(r, g, b);
        }

        public static byte PutInRange(float val)
        {
            if (val > 255)
                val = 255;
            else if (val < 0)
                val = 0;

            return (byte)val;
        }

        public double GetDistance(RgbPixel pixel)
        {
            var distance = Math.Sqrt(Math.Pow(Red - pixel.Red, 2)
                           + Math.Pow(Green - pixel.Green, 2)
                           + Math.Pow(Blue - pixel.Blue, 2));
            return distance;
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() == typeof(RgbPixel))
            {
                var pixel = obj as RgbPixel;
                return pixel.Red == Red && pixel.Green == Green && pixel.Blue == Blue;
            }
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return Red.GetHashCode() ^ Blue.GetHashCode() ^ Green.GetHashCode();
        }
    }
}