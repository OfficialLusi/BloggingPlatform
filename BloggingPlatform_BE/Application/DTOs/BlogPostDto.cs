namespace BloggingPlatform_BE.Application.DTOs;

public class BlogPostDto
{
    public int PostId { get; set; } // primary key
    public int UserId { get; set; } // foreign key to connect to user
    public Guid PostGuid { get; set; }
    public string PostTitle { get; set; } = string.Empty;
    public string PostContent { get; set; } = string.Empty;
    public string PostTags { get; set; } = string.Empty;
    public DateTime PostCreatedOn { get; set; }
    public DateTime PostModifiedOn { get; set; } = DateTime.MinValue;
}
