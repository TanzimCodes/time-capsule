# Time Capsule Project

The **Time Capsule Project** is a unique .NET-based application designed to store messages, memories, or data that can be opened and accessed at a future date. The goal is to create a digital "time capsule" where users can securely store their files, notes, or even videos, with the assurance that they will be available to them or others at a pre-determined future time.

## Features:
- **Date-Locked Access**: Store data that remains locked until a specific future date.
- **Secure Encryption**: Ensure privacy and security of the stored content using encryption techniques.
- **Multiple File Support**: Store various types of media such as text, images, and videos.
- **User-Friendly Interface**: Easy-to-use design for seamless interaction with the platform.

## Technologies Used:
- **Frontend**: Blazor (WebAssembly or Server)
- **Backend**: ASP.NET Core
- **Database**: SQL Server / In Memory
- **Encryption**: AES-256 / RSA
- **Deployment**: Docker, AWS

## Installation:
1. Clone the repository:  
   `git clone https://github.com/username/time-capsule.git`
2. Open the project in Visual Studio or Visual Studio Code.
3. Restore dependencies:  
   `dotnet restore`
4. Build the application:  
   `dotnet build`
5. Run the application:  
   `dotnet run`

## Future Improvements:
- Add more file types and larger file size support.
- Incorporate social sharing features for collaborative capsules.
- Allow users to set capsule expiration dates or unlock after a number of years.
- Implement AI-driven content curation for time capsule creation.
