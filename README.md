# Project Overview
This project is a personal blogging platform designed with a focus on clean architecture and modern software development practices. 
It consists of a robust backend powered by C# and .NET 8 API, as well as a responsive frontend built using WPF and the MVVM design pattern.

## Key Features
RESTful API with CRUD Operations:
The backend provides a comprehensive set of RESTful endpoints to create, read, update, and delete blog posts. 
Each post includes details such as title, content, tags, and timestamp metadata for creation and updates.

**Clean Architecture**:
The backend is structured following clean architecture principles. 
This design separates concerns into distinct layers, enhancing maintainability, scalability, and testability by isolating core business logic from external dependencies.

**WPF Frontend with MVVM**:
The user interface is built with Windows Presentation Foundation (WPF), utilizing the Model-View-ViewModel (MVVM) pattern. 
This approach promotes a clear separation between UI and business logic, making the frontend highly modular and easy to test.

**Comprehensive Testing**:
A dedicated test project is included to ensure the application’s functionality using xUnit. 
This project covers various aspects of the API and UI, ensuring that all CRUD operations and interactions work as expected.

**Custom Shared Library**:
A personal library has been developed and integrated to manage common functionalities across both the backend and frontend. 
This library streamlines development, reduces code duplication, and enhances consistency throughout the project.

## Technology Stack
Backend: C# with .NET 8 API
Frontend: WPF with MVVM design pattern
Testing: Custom test project for automated testing (xUnit)
Custom Library: A self-developed library used to handle shared operations between backend and frontend

## Project Structure
### API Layer:
Implements RESTful endpoints for managing blog posts with proper validation and error handling. 
Follows HTTP best practices with appropriate status codes (e.g., 201 Created, 200 OK, 204 No Content, 400 Bad Request, 404 Not Found).

### Frontend Layer:
A WPF application that consumes the API and presents a user-friendly interface. 
It follows the MVVM pattern to maintain a clean separation between views, view models, and business logic.

### Testing Layer:
Contains a suite of tests to validate the functionality of both the API and the frontend.
This ensures reliability and aids in future refactoring efforts.

### Shared Library:
Houses reusable components and utilities that facilitate operations across the application, reducing redundancy and improving overall code quality.

# Conclusion
This project demonstrates a robust approach to building a personal blogging platform by leveraging modern technologies and architectural patterns. 
The integration of a cleanly architected backend with a well-structured WPF frontend, complemented by comprehensive testing and a custom shared library, 
results in a scalable and maintainable solution ready for further enhancements.
