Controllers/: Contains the logic to handle HTTP requests and manage the flow of data between the views and models.
Db/: Holds database-related classes and context setup for the application.
Migrations/: Manages schema changes and database versioning through Entity Framework migrations.
Models/: Represents the data structure used throughout the application.
VmModel/: Contains the ViewModel classes, which shape the data sent between controllers and views.
Uploads/: This folder stores files uploaded by users or administrators through the application.
Properties/: Contains project-level metadata such as assembly information and settings.
bin/ and obj/: Compiled binaries and object files for debugging and release builds.
Build & Run Instructions
Prerequisites
.NET SDK (version 8.0 or later)
Visual Studio or any preferred IDE for C# development
Steps to Run
Clone the repository.
Open the project in Visual Studio or your preferred IDE.
Build the project.
run the program.
Database Setup
If the project uses a database, make sure to:

Update the connection string in the appsettings.json file.
Run the database migrations.
Content management: Handles creation, editing, and deletion of various content types.
User uploads: Manages file uploads through the Uploads/ directory.
View Models: Provides a clean separation between the data displayed in views and the core application models.
