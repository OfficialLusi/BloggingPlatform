namespace BloggingPlatform_FE.Interfaces;

public interface INavigationService
{
    /// <summary>
    /// Switch pages on the window
    /// </summary>
    /// <param name="viewName">name of the page to navigate to</param>
    void NavigateTo(string viewName);
}
