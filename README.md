# 📄 CV Submission Web Application

A full-stack web application built with **.NET 8 (Razor Pages)** that allows applicants to upload and submit their CVs, while providing an authenticated **Admin Dashboard** to review, filter, and manage applicant submissions.

---

## 🌐 Live Demo & Testing

You can test the deployed application and explore both pages live on Render:

- 📝 **CV Submission Page**: [https://cv-submission-dotnet.onrender.com](https://cv-submission-dotnet.onrender.com)
- 🔐 **Admin Login Page**: [https://cv-submission-dotnet.onrender.com/Login](https://cv-submission-dotnet.onrender.com/Login)
  for testing: username: "admin"
   password: "Admin123!"
  

---

## Server-Side and Client-Side

This application leverages a hybrid approach using ASP.NET Core Razor Pages for seamless performance, security, and validation:

- **Server-Side (ASP.NET Core Razor Pages)**:
  - **Dynamic Rendering**: Pages like `/CVupload` and `/Admin` are rendered on the server, serving pre-compiled HTML to the browser.
  - **Secure Logic & Authentication**: Admin authentication, session checks, and database operations happen strictly on the server for maximum security.
 <img width="942" height="767" alt="img1_2" src="https://github.com/user-attachments/assets/551a5912-eee3-4ecc-9972-4ce4af88193b" />


- **Client-Side (HTML5 / JS / DataAnnotations)**:
  - **Instant Form Validation**: Client-side validation catches missing fields or invalid file inputs before hitting the server.
    <img width="877" height="525" alt="image" src="https://github.com/user-attachments/assets/0c369f6f-b1d6-43de-bee2-c21d461aad9c" />



---

## ✨ Features

- **CV Upload & Validation**: Applicants can submit their details and upload CV files with built-in model validation (`DataAnnotations`).
- **CV Summary**:
  <img width="952" height="767" alt="img3" src="https://github.com/user-attachments/assets/256dbef2-3a55-45fd-ab46-b79e9293220c" />

- **Admin Dashboard**: Secure access for admins to view submitted CVs and candidate details.
  <img width="1701" height="797" alt="img4_2" src="https://github.com/user-attachments/assets/1093d5f1-1802-44c2-ac4a-2090b9bad495" />

- **Cookie Authentication**: Protected admin routes using ASP.NET Core Cookie Authentication.
- **Dockerized Deployment**: Fully containerized with Docker for seamless deployment on cloud platforms like Render.

---

##  Tech Stack

- **Framework**: .NET 8 / ASP.NET Core Razor Pages & Minimal APIs
- **Database**: SQLite / Entity Framework Core (EF Core)
- **Authentication**: Cookie-based Authentication
- **DevOps & Deployment**: Docker, Render

---

##  Getting Started Locally

### Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- Visual Studio / JetBrains Rider / VS Code

### Run Steps

1. **Clone the repository:**
   ```bash
   git clone [https://github.com/YOUR_USERNAME/YOUR_REPO_NAME.git](https://github.com/YOUR_USERNAME/YOUR_REPO_NAME.git)
   cd YOUR_REPO_NAME
