using System;
using System.Collections.Generic;
using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BloggingPlatform_BE.Application.DTOs;
using BloggingPlatform_BE.Domain.Interfaces;
using BloggingPlatform_BE.Infrastructure.Controllers;

namespace BloggingPlatform_TEST
{
    public class BloggingPlatformControllerTests
    {
        private readonly Mock<IApplicationService> _mockService;
        private readonly Mock<ILogger<BloggingPlatformController>> _mockLogger;
        private readonly BloggingPlatformController _controller;

        public BloggingPlatformControllerTests()
        {
            _mockService = new Mock<IApplicationService>();
            _mockLogger = new Mock<ILogger<BloggingPlatformController>>();

            _controller = new BloggingPlatformController(_mockService.Object, _mockLogger.Object);
        }

        #region User

        [Fact]
        public void AddUser_ReturnsOkResult()
        {
            // Arrange
            UserDto userDto = new UserDto
            {
                UserGuid = Guid.NewGuid(),
                UserName = "UserName_test",
                UserSurname = "UserSurname_test",
                UserEmail = "test@test.test",
                UserPassword = "password_test",
                UserCreatedOn = DateTime.UtcNow
            };

            _mockService.Setup(s => s.AddUser(It.IsAny<UserDto>())).Verifiable();

            // Act
            IActionResult result = _controller.AddUser(userDto);

            // Assert
            Assert.IsType<OkResult>(result);
            _mockService.Verify(s => s.AddUser(It.Is<UserDto>(u => u == userDto)), Times.Once);
        }

        [Fact]
        public void UpdateUser_ReturnsOkResult()
        {
            // Arrange
            UserDto userDto = new UserDto
            {
                UserName = "UserName_test",
                UserSurname = "UserSurname_test",
                UserEmail = "test@test.test",
                UserPassword = "password_test"
            };

            _mockService.Setup(s => s.AddUser(It.IsAny<UserDto>())).Verifiable();

            // Act
            IActionResult result = _controller.UpdateUser(userDto);

            // Assert
            Assert.IsType<OkResult>(result);
            _mockService.Verify(s => s.UpdateUser(It.Is<UserDto>(u => u == userDto)), Times.Once);
        }

        [Fact]
        public void DeleteUser_ReturnsOkResult()
        {
            // Arrange
            Guid userGuid = Guid.NewGuid();

            _mockService.Setup(s => s.DeleteUser(It.IsAny<Guid>())).Verifiable();

            // Act
            IActionResult result = _controller.DeleteUser(userGuid.ToString());

            // Assert
            Assert.IsType<OkResult>(result);
            _mockService.Verify(s => s.DeleteUser(It.Is<Guid>(u => u == userGuid)), Times.Once);
        }

        [Fact]
        public void GetUserByGuid_ReturnsOkObjectResult_WithUser()
        {
            // Arrange
            Guid testGuid = Guid.NewGuid();
            UserDto userDto = new UserDto
            {
                UserGuid = Guid.NewGuid(),
                UserName = "UserName_test",
                UserSurname = "UserSurname_test",
                UserEmail = "test@test.test",
                UserPassword = "password_test",
                UserCreatedOn = DateTime.UtcNow
            };

            _mockService.Setup(s => s.GetUserByGuid(testGuid)).Returns(userDto);

            // Act
            IActionResult actionResult = _controller.GetUserByGuid(testGuid.ToString());

            // Assert
            OkObjectResult okResult = Assert.IsType<OkObjectResult>(actionResult);
            Assert.Equal(userDto, okResult.Value);
        }

        [Fact]
        public void GetAllUsers_ReturnsOkObjectResult_WithListOfUsers()
        {
            // Arrange
            List<UserDto> users = new List<UserDto>
            {
                new UserDto { UserGuid = Guid.NewGuid(), UserName = "User1", UserSurname = "Surname1", UserEmail = "user1@example.com", UserPassword = "pass1", UserCreatedOn = DateTime.UtcNow },
                new UserDto { UserGuid = Guid.NewGuid(), UserName = "User2", UserSurname = "Surname2", UserEmail = "user2@example.com", UserPassword = "pass2", UserCreatedOn = DateTime.UtcNow }
            };

            _mockService.Setup(s => s.GetAllUsers()).Returns(users);

            // Act
            IActionResult actionResult = _controller.GetAllUsers();

            // Assert
            OkObjectResult okResult = Assert.IsType<OkObjectResult>(actionResult);
            Assert.Equal(users, okResult.Value);
        }

        #endregion

        #region BlogPost
        [Fact]
        public void AddBlogPost_ReturnsOkResult()
        {
            // Arrange
            BlogPostDto blogPost = new BlogPostDto
            {
                PostId = 1,
                PostGuid = Guid.NewGuid(),
                PostTitle = "Title1",
                PostContent = "Content1",
                PostTags = "[tag1, tag2]",
                PostCreatedOn = DateTime.UtcNow,
                PostModifiedOn = DateTime.MinValue
            };

            _mockService.Setup(s => s.AddBlogPost(It.IsAny<BlogPostDto>())).Verifiable();

            // Act
            IActionResult result = _controller.AddBlogPost(blogPost);

            // Assert
            Assert.IsType<OkResult>(result);
            _mockService.Verify(s => s.AddBlogPost(It.Is<BlogPostDto>(u => u == blogPost)), Times.Once);
        }

        [Fact]
        public void UpdateBlogPost_ReturnsOkResult()
        {
            // Arrange
            BlogPostDto blogPost = new BlogPostDto
            {
                PostTitle = "Title1",
                PostContent = "Content1",
                PostTags = "[tag1, tag2]",
                PostModifiedOn = DateTime.UtcNow
            };

            _mockService.Setup(s => s.UpdateBlogPost(It.IsAny<BlogPostDto>())).Verifiable();

            // Act
            IActionResult result = _controller.UpdateBlogPost(blogPost);

            // Assert
            Assert.IsType<OkResult>(result);
            _mockService.Verify(s => s.UpdateBlogPost(It.Is<BlogPostDto>(u => u == blogPost)), Times.Once);
        }

        [Fact]
        public void DeleteBlogPost_ReturnsOkResult()
        {
            // Arrange
            Guid blogPostGuid = Guid.NewGuid();

            _mockService.Setup(s => s.DeleteBlogPost(It.IsAny<Guid>())).Verifiable();

            // Act
            IActionResult result = _controller.DeleteBlogPost(blogPostGuid.ToString());

            // Assert
            Assert.IsType<OkResult>(result);
            _mockService.Verify(s => s.DeleteBlogPost(It.Is<Guid>(u => u == blogPostGuid)), Times.Once);
        }

        [Fact]
        public void GetBlogPostByGuid_ReturnsOkObjectResult_WithBlogPost()
        {
            // Arrange
            Guid testGuid = Guid.NewGuid();
            BlogPostDto blogPost = new BlogPostDto
            {
                PostId = 1,
                PostGuid = Guid.NewGuid(),
                PostTitle = "Title1",
                PostContent = "Content1",
                PostTags = "[tag1, tag2]",
                PostCreatedOn = DateTime.UtcNow,
                PostModifiedOn = DateTime.MinValue
            };

            _mockService.Setup(s => s.GetBlogPostByGuid(testGuid)).Returns(blogPost);

            // Act
            IActionResult actionResult = _controller.GetBlogPostByGuid(testGuid.ToString());

            // Assert
            OkObjectResult okResult = Assert.IsType<OkObjectResult>(actionResult);
            Assert.Equal(blogPost, okResult.Value);
        }

        [Fact]
        public void GetAllBlogPosts_ReturnsOkObjectResult_WithListOfBlogPosts()
        {
            // Arrange
            List<BlogPostDto> blogPosts = new List<BlogPostDto>
            {
                new BlogPostDto { PostGuid = Guid.NewGuid(), PostTitle = "Title1", PostContent = "Content1", PostTags = "[tag1, tag2]", PostCreatedOn = DateTime.UtcNow, PostModifiedOn = DateTime.MinValue },
                new BlogPostDto { PostGuid = Guid.NewGuid(), PostTitle = "Title2", PostContent = "Content2", PostTags = "[tag1, tag2]", PostCreatedOn = DateTime.UtcNow, PostModifiedOn = DateTime.MinValue }
            };

            _mockService.Setup(s => s.GetAllBlogPosts()).Returns(blogPosts);

            // Act
            IActionResult actionResult = _controller.GetAllBlogPosts();

            // Assert
            OkObjectResult okResult = Assert.IsType<OkObjectResult>(actionResult);
            Assert.Equal(blogPosts, okResult.Value);
        }
        #endregion

    }
}
