namespace PMSystem.Middlewares
{
    public class NotFoundException:Exception
    {
        public NotFoundException(string msg):base(msg) { }
    }
}
