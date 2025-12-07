namespace PMSystem.Middlewares
{
    public class BadRequestException:Exception
    {
        public BadRequestException(string msg):base(msg) { }
    }
}
