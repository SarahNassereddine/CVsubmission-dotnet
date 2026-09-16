# CV Submission App

A CV/resume submission portal built with **ASP.NET Core 8 (Razor Pages)** and **Entity Framework Core**. Applicants fill out a form (with photo upload) that is stored in a database, and can view their own submission through a private link. An authenticated admin area lets an administrator browse and search every submitted CV.

## Features

- **Public CV submission form** — first/last name, birthday, gender, nationalities, skills, email (with confirmation), password, and a profile photo upload, validated on both client and server.
- **Private summary link per applicant** — after submitting, each applicant is redirected to a summary page reachable only through a unique, unguessable token (GUID) embedded in the URL. Applicants cannot view or enumerate anyone else's submission.
- **Admin dashboard** — a separate, authenticated area (`/AllCvs`) lists every submitted CV as searchable cards (search by name), for an administrator to review applicants. This route is protected with `[Authorize]` and is not linked from the public UI.
- **Cookie-based authentication** — a lightweight login (`/Login`) issues an authentication cookie for the admin session; logging out (`/Logout`) clears it.
- **Password hashing** — applicant passwords are hashed (SHA-256) before being persisted; the app never stores plain-text passwords.
- **Photo upload handling** — uploaded images are stored on disk and referenced by path in the database, with a default placeholder when no photo is provided.
- **Anti-bot check** — a simple arithmetic captcha (sum of two randomly generated numbers) on the submission form.

## Tech Stack

- ASP.NET Core 8, Razor Pages
- Entity Framework Core (SQLite provider)
- Bootstrap 5 for styling
- Cookie Authentication (`Microsoft.AspNetCore.Authentication.Cookies`)

## Project Structure

```
Pages/
  Index.cshtml            Landing page
  CVupload.cshtml(.cs)    Public CV submission form
  Summary.cshtml(.cs)     Private per-applicant summary (token-based route)
  AllCvs.cshtml(.cs)      Admin dashboard — [Authorize]
  Login.cshtml(.cs)       Admin login
  Logout.cshtml(.cs)      Admin logout
Models/
  CV.cs                   EF Core entity
  CVBindingModel.cs       Form input model
  CVViewModel.cs          Form redisplay model (validation round-trip)
  ViewProperty.cs         Read-only projection used by Summary/AllCvs
Services/
  IDBServices.cs / DBServices.cs         Data access (save, fetch by token, list/search)
  IPhotoServices.cs / PhotoServices.cs   Image upload handling
Data/
  AppDbContext.cs         EF Core DbContext
```

## Running Locally

1. Restore and build:
   ```
   dotnet restore
   dotnet build
   ```
2. Apply database migrations:
   ```
   dotnet ef database update
   ```
3. Run:
   ```
   dotnet run
   ```
4. Open the printed `https://localhost:xxxx` URL.

## Admin Access

The admin dashboard is intentionally **not linked** anywhere in the public UI — a real deployment wouldn't advertise its admin panel to end users. To access it, navigate directly to:

```
/Login
```

Demo credentials (change these in production via `appsettings.json` under `AdminUser:Username` / `AdminUser:Password`):

```
Username: admin
Password: Admin123!
```

## Security Notes

- **IDOR prevention**: applicant summary pages are addressed by an unguessable GUID token rather than a sequential database ID, so one applicant cannot view another's data by editing the URL.
- **Role separation**: public submission flow requires no login; the aggregate applicant list requires an authenticated admin session.
- **No plain-text passwords**: applicant passwords are hashed before storage. (Note: for production use, prefer a salted algorithm such as BCrypt over SHA-256.)

## Possible Next Steps

- Move to a salted hashing algorithm (BCrypt) for passwords.
- Add pagination to the admin dashboard for large applicant lists.
- Add automated tests for the service layer.
