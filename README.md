
# 📒 Address Book Backend

This is the backend for the **Address Book Web Application**, built with **.NET Core Web API**.  
It provides a set of RESTful APIs to manage address book entries, including CRUD operations, search, authentication, and Excel export functionality.

---

## 🚀 Features
- **Address Book Management**
  - Add, edit, delete, and view contacts.
  - Fields include:
    - Full Name  
    - Job Title (Dropdown list)  
    - Department (Dropdown list)  
    - Mobile Number  
    - Date of Birth  
    - Address  
    - Email  
    - Password (for authentication)  
    - Photo  
    - Age (calculated automatically)
  - Validation for **emails** and **phone numbers**.

- **Jobs & Departments**
  - Manage Job Titles and Departments (Add/Edit/Delete).

- **Search & Filter**
  - Search contacts by **all fields**.
  - Filter by **date of birth range**.

- **Export to Excel**
  - Export the entire address book as an **Excel file**.

- **Authentication**
  - Login & Register endpoints with **JWT-based authentication**.

- **Real-time Updates**
  - Add or edit records **without page reload** (SPA-friendly APIs).

- **Architecture**
  - Built using **Clean Architecture** for maintainability and scalability.
  - **Code-First EF Core** approach to generate the database.

- **Supabase Storage**
  - All files (like user photos and exported Excel files) are stored in **Supabase Storage**.
  - To **download any file**, you **must prepend** the following base URL:
    ```
    https://hjrkjcfhkhesbbkkjoon.supabase.co/storage/v1/object/public/3pillars/
    ```

---

## 🛠 Tech Stack
- **.NET Core 8 Web API**
- **Entity Framework Core (Code-First)**
- **SQL Server** (hosted on [MonsterASP Free Plan](https://www.smarterasp.net/))
- **Supabase Storage** (for file management)
- **AutoMapper** (for DTO mapping)
- **FluentValidation** (for input validation)
- **JWT Authentication**
- **ClosedXML** (for Excel export)

---

## 📂 Project Structure
```
AddressBook.Backend/
│
├── AddressBook.Api/           # API Layer (Controllers & Endpoints)
├── AddressBook.Application/   # Application Layer (Services, DTOs, Validation)
├── AddressBook.Domain/        # Domain Entities & Interfaces
├── AddressBook.Infrastructure/ # EF Core, Repositories, and DB Context
└── AddressBook.Tests/         # Unit and Integration Tests
```

---

## ⚙️ Getting Started

### **1️⃣ Prerequisites**
Make sure you have installed:
- [.NET SDK 8+](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [Visual Studio](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)

---

### **2️⃣ Setup Database**
Update the connection string in `appsettings.json`:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=AddressBookDb;Trusted_Connection=True;Encrypt=False;"
}
```
Run migrations:
```bash
cd AddressBook.Api
dotnet ef database update
```

---

### **3️⃣ Run the Application**
```bash
cd AddressBook.Api
dotnet run
```
API will run on:
```
https://localhost:5001
http://localhost:5000
```

---

## 🧪 API Endpoints

### **Authentication**
| Method | Endpoint | Description |
|--------|-----------|-------------|
| POST   | `/api/v1/user` | Register new user |
| PUT    | `/api/v1/user/auth` | Login user and get JWT token |

### **Users**
| Method | Endpoint | Description |
|--------|-----------|-------------|
| GET    | `/api/v1/user` | Get all users (supports search and filters) |
| GET    | `/api/v1/user/{id}` | Get a user by ID |
| POST   | `/api/v1/user` | Create a new user |
| PUT    | `/api/v1/user` | Update a user |
| DELETE | `/api/v1/user/{id}` | Delete a user |
| GET    | `/api/v1/user/xlsx` | Export users to Excel |

### **Jobs**
| Method | Endpoint | Description |
|--------|-----------|-------------|
| GET    | `/api/v1/job` | Get all jobs |
| POST   | `/api/v1/job` | Add a new job |
| PUT    | `/api/v1/job` | Update a job |
| DELETE | `/api/v1/job/{id}` | Delete a job |

### **Departments**
| Method | Endpoint | Description |
|--------|-----------|-------------|
| GET    | `/api/v1/department` | Get all departments |
| POST   | `/api/v1/department` | Add a new department |
| PUT    | `/api/v1/department` | Update a department |
| DELETE | `/api/v1/department/{id}` | Delete a department |

### **File Upload**
| Method | Endpoint | Description |
|--------|-----------|-------------|
| POST   | `/api/v1/stream` | Upload file |
| DELETE | `/api/v1/stream` | Delete file |

**Note:**  
To **download** any uploaded file, concatenate the file `url` with:
```
https://hjrkjcfhkhesbbkkjoon.supabase.co/storage/v1/object/public/3pillars/
```

Example:
If the `url` returned is `user/photos/image1.png`,  
the full download URL will be:
```
https://hjrkjcfhkhesbbkkjoon.supabase.co/storage/v1/object/public/3pillars/user/photos/image1.png
```

---

## 📊 Example Contact JSON
```json
{
  "fullName": "John Doe",
  "jobTitleId": 1,
  "departmentId": 2,
  "mobileNumber": "+201234567890",
  "dateOfBirth": "1990-05-15",
  "address": "Cairo, Egypt",
  "email": "john.doe@example.com",
  "password": "StrongPass123!",
  "photoUrl": "user/photos/image1.png"
}
```

---

## 🗃 Export to Excel
```
GET /api/v1/user/xlsx
```
Response:
```json
{
  "name": "users.xlsx",
  "url": "exports/users.xlsx",
  "size": 10240,
  "extensaion": ".xlsx"
}
```

Download link:
```
https://hjrkjcfhkhesbbkkjoon.supabase.co/storage/v1/object/public/3pillars/exports/users.xlsx
```

---

## 🧱 Considerations
- **No Visual Studio scaffolding** was used.
- Fully **responsive** design supported by the frontend.
- All forms include **validation** for correct data entry.
- Deployment-ready for free hosting using **MonsterASP SQL Server free plan**.

---

## 🧑‍💻 Author
**Ahmed Adel Basha**  
.NET Backend Developer | [GitHub](https://github.com/ahmed1672003)

---
© 2025 Ahmed Adel Basha
