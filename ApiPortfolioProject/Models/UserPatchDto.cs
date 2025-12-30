namespace ApiPortfolioProject.Models
{
    public class UserPatchDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public bool? IsActive { get; set; }
    }
}