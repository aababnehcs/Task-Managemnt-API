API Project Documentation
Overview
This API provides a robust backend solution with built-in authentication and role-based access control. The system comes pre-configured with default admin and user accounts for testing purposes.

Features
Secure Authentication: Uses password hashing for maximum security

Role-Based Access Control: Different permissions for admin vs regular users

Ready-to-Use Test Accounts: Pre-configured credentials for immediate testing

RESTful Design: Clean, predictable endpoints

Getting Started
Prerequisites
.NET 6.0

SQL Server (or compatible database)

Postman/Insomnia (for API testing)

Default Test Accounts
The system automatically creates these accounts on first run:

Username	Password	Role
admin	Admin@123	Admin
user	User@123	User
Security Note: Change these default credentials before deploying to production.

Authentication
All endpoints (except login) require authentication via JWT Bearer tokens.

Login Example
http
POST /api/auth/login
Content-Type: application/json

{
  "username": "admin",
  "password": "Admin@123"
}
Successful response includes an access token:

json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "expires": "2023-12-31T23:59:59Z"
}
Include this token in subsequent requests as:

text
Authorization: Bearer <token>
API Endpoints
Admin Endpoints (Require Admin Role)
GET /api/users - List all users

POST /api/users - Create new user

PUT /api/users/{id} - Update user

DELETE /api/users/{id} - Delete user

User Endpoints
GET /api/profile - Get current user profile

PUT /api/profile - Update own profile

GET /api/resources - Access application resources

Security Considerations
Passwords are hashed using industry-standard PBKDF2 algorithm

JWT tokens have configurable expiration

Role-based authorization enforced at controller level

Always use HTTPS in production

Development Setup
Clone the repository

Configure connection string in appsettings.json

Run database migrations:

bash
dotnet ef database update
Start the application:

bash
dotnet run
Deployment
For production deployment:

Generate new secure passwords for default accounts

Configure proper CORS policies

Set up HTTPS

Implement rate limiting

Configure logging and monitoring

Support
For assistance, please contact ahmad.ababneh@hitmail.com or open an issue in our repository.
