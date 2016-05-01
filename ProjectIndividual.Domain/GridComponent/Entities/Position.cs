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
            var pos2 = obj as Position;
            if (pos2==null)
            {
                return base.Equals(obj);
            }
            return (pos2.X == x && pos2.Y == y);
        }

        public override int GetHashCode()
        {
            return (int)(x.GetHashCode()*17 + y.GetHashCode());
        }
    }
}
