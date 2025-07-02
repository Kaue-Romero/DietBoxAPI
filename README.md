# 🥗 Meal Plan API

A production-ready RESTful API designed to help **nutritionists** and **health professionals** create and manage meal plans for their patients. Developed as a technical challenge, this project emphasizes **clean architecture**, **scalability**, and **developer best practices**.

---

## 🚀 Features

- **Patient, Food, and Meal Plan Management**  
  Full CRUD operations for:
  - `/api/Patient`
  - `/api/Food`
  - `/api/Mealplan`

- **Meal Plan Customization**  
  Add food items with portion sizes to meal plans. Total calorie count is calculated automatically.

- **Daily Patient Meal Plan View**  
  Retrieve today’s meal plan for a specific patient:  
  `GET /api/Patient/{id}/mealplans/today`

- **JWT Authentication**  
  Role-based access for:
  - `ADMIN`
  - `NUTRITIONIST`

- **Soft Deletion**  
  Logical deletion of patients (soft delete) enables data recovery.

---

## 🛠️ Technologies Used

| Layer                | Technology                             |
|---------------------|----------------------------------------|
| **Backend**         | ASP.NET Core 8.0 Web API               |
| **Language**        | C# 12                                  |
| **ORM**             | Entity Framework Core 8                |
| **Database**        | MySQL (prod/dev), In-Memory (testing)  |
| **Documentation**   | Swagger                                |
| **Authentication**  | JWT Bearer Tokens                      |
| **Architecture**    | MVC, Services, DTOs, Interfaces        |
| **Testing**         | xUnit                                  |
| **Containerization**| Docker & Docker Compose                |

---

## ✅ Quality Criteria & Best Practices

- **Test Coverage**: with xUnit  
- **SOLID & GoF Patterns**: Maintainable and extensible codebase  
- **Asynchronous Programming**: `async/await` across I/O operations  

---

## ⚙️ DevOps & CI/CD

- **Docker Integration**  
  Easily spin up the app and MySQL with `docker-compose`.

- **GitHub Actions CI**  
  Automated:
  - Build
  - Test
  - Docker image creation  
  on every push to maintain code quality.

---

## 🧪 Getting Started

### 🔧 Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)
- [Docker Desktop](https://www.docker.com/products/docker-desktop/) *(optional but recommended)*

---

### 🚀 Run Locally

#### Clone the repository:

```bash
git clone <your-repository-url>
cd meal-plan-api
```

#### Using Docker Compose (Recommended):

```bash
docker-compose up --build
```

API will be available at:  
`http://localhost:8080`

#### Manual Setup (No Docker):

1. **Configure MySQL**  
   Update `appsettings.json` with the example credentials
 ```json
{
  "JwtSettings": {
    "SecretKey": "mysupersecretkeythatneeds32chars!",
    "Issuer": "DietBox",
    "Audience": "DietBoxUsers",
    "ExpiresInMinutes": 60
  },
  "ConnectionStrings": {
    "DefaultConnection": "Server=db;Port=3306;Database=dietbox;User=dietboxuser;Password=DietBoxUserPass!;"
  }
}
```

3. **Apply Migrations:**

```bash
docker exec -it <api_container_name> dotnet ef database update
```

3. **Run the API:**

```bash
docker exec -it <api_container_name> dotnet run
```

API will be available at:  
`https://localhost:7001` or `http://localhost:5000`

---

## 📘 API Documentation

Access Swagger UI:

- Docker: `http://localhost:8080/swagger`
- Local: `https://localhost:7001/swagger`

---

## 🔐 Default Credentials

| Role          | Username                | Password               |
|---------------|-------------------------|------------------------|
| **ADMIN**     | `admin@example.com`     | `AdminPassword123`     |
| **NUTRITIONIST** | `nutritionist@example.com` | `NutritionistPassword123` |

---

## 🧪 Running Tests

```bash
dotnet test
```

All unit tests are located in the `DietBoxAPI.Tests` project.

---

## 🤝 Contributing

Contributions are welcome! To propose a change:

1. Fork the repository
2. Create a new branch
3. Commit your changes
4. Submit a pull request

---

## 📄 License

MIT License (or your chosen license)

---
