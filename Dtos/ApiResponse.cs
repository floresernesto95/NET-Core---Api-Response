using System.Net;

namespace Project_Courses.Dtos
{
    public class ApiResponse<T>
    {
        public HttpStatusCode Status { get; set; }
        public T Data { get; set; }
        public string ErrorMessage { get; set; }
    }
}
