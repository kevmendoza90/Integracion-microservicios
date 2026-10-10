namespace Common.Wrappers
{
    public class HttpResponse<T>
    {
        // Constructor vacío: lo necesita Newtonsoft para deserializar
        // la respuesta que llega desde el otro microservicio.
        public HttpResponse() { }

        public HttpResponse(T result, string message = null)
        {
            Succeeded = true;
            Result = result;
            Message = message;
        }

        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public List<string> ValidationErrors { get; set; }
        public T Result { get; set; }
    }
}