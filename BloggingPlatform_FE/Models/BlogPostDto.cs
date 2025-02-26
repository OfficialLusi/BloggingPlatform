namespace BloggingPlatform_FE.Models;

public class BlogPostDto
{
    public int PostId { get; set; }
    public int UserId { get; set; }
    public Guid PostGuid { get; set; }
    public string PostTitle { get; set; } = string.Empty;
    public string PostContent { get; set; } = string.Empty;
    public string PostTags { get; set; } = string.Empty;
    public DateTime PostCreatedOn { get; set; }
    public DateTime PostModifiedOn { get; set; }
}