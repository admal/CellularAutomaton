namespace ProjectIndividual.Domain.GridComponent.Entities
{
    public class Position
    {
        long x, y;

        public Position(long x, long y)
        {
            this.x = x;
            this.y = y;
        }

        public long X
        {
            get { return x; }
        }

        public long Y
        {
            get { return y; }
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return (int)(x.GetHashCode()*17 + y.GetHashCode());
        }
    }
}
