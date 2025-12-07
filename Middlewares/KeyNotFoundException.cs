namespace PMSystem.Middlewares
{
    public class KeyNotFoundException:Exception
    {
        public KeyNotFoundException(string msg) : base(msg) { }
    }
}
