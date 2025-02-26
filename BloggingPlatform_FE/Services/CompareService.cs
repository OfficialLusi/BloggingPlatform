using BloggingPlatform_FE.Interfaces;
using BloggingPlatform_FE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloggingPlatform_FE.Services;

public class CompareService : IComparer<BlogPostDto>
{
    /// <summary>
    /// Function called when the _compareService is needed in a sort method for a string
    /// </summary>
    /// <param name="x">blog post dto instance 1</param>
    /// <param name="y">blog post dto instance 2</param>
    /// <returns></returns>
    public int Compare(BlogPostDto? x, BlogPostDto? y)
    {
        // if the modified date is different to minValue take it, if it is not, take the created on value
        DateTime date1 = x.PostModifiedOn != DateTime.MinValue ? x.PostModifiedOn : x.PostCreatedOn;
        DateTime date2 = y.PostModifiedOn != DateTime.MinValue ? y.PostModifiedOn : y.PostCreatedOn;

        // compare in a descendant way the two dates
        return DateTime.Compare(date2, date1);
    }
}
