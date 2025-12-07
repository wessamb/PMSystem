namespace PMSystem.Middlewares
{
    public class UnauthorizedAccessException:Exception
    {
        public UnauthorizedAccessException(string msg):base(msg) { }
    }
}
