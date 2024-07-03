namespace Project_Courses.Services.Auth
{
    public interface ILoginService
    {
        Task<string> ValidatePermissionAsync(Guid id);
    }
}