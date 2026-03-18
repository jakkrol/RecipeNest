# 🍲 RecipeNest: Cross-Platform Recipe Management

RecipeNest is a full-stack ecosystem built with the **.NET stack**, designed with an **Offline-First** philosophy. It enables users to digitize, manage, and synchronize recipes across devices using OCR and a robust background sync engine.

> **Current Status:** 🏗️ **Active Backend Development.** Currently engineering the backend logic and synchronization logic between SQLite (Client) and PostgreSQL (Server).

---

## 🏗️ System Architecture

The solution is architected into three distinct layers to maximize code reusability and maintain strict type safety:

| Project | Role | Tech Stack |
| :--- | :--- | :--- |
| **RecipeNest.Shared** | Core Class Library | .NET Standard / DTOs |
| **RecipeNest.App** | Cross-platform Client | .NET MAUI, MVVM, SQLite |
| **RecipeNest.Backend** | RESTful API Service | ASP.NET Core, PostgreSQL |

---

## 🚀 Key Features

* **📶 True Offline-First:** Powered by **SQLite**, the app remains fully functional without an internet connection.
* **🔄 Intelligent Sync:** Background reconciliation logic to merge local changes with the cloud once connectivity is restored.
* **📸 OCR Digitization:** Integrated **Tesseract OCR** to transform photos of physical cookbooks into structured digital data.
* **🛡️ Enterprise Patterns:** * **MVVM:** Clean separation of UI and business logic.
    * **Dependency Injection:** Decoupled components for better testability.
    * **Shared Data Contracts:** Unified DTOs to eliminate "magic strings" and mapping errors.

---

## 🔬 Technical Deep Dive

### 📂 Data Persistence & Synchronization
The application implements an **Optimistic UI** pattern. User actions are committed instantly to the local **SQLite** database to ensure zero latency.

**The Sync Workflow:**
1.  **Change Tracking:** The app identifies new or modified records in the local store.
2.  **Delta Push:** Only the changes (deltas) are sent to the **ASP.NET Core** backend via DTOs.
3.  **Conflict Resolution:** The backend compares timestamps and versions against the **PostgreSQL** master record to ensure data integrity.
4.  **State Reconcile:** The local client updates its state based on the server's confirmation.

### 🧠 Shared Logic (.Shared)
By housing all **Data Transfer Objects (DTOs)** in a shared library, we achieve a "Single Source of Truth." This ensures that any change in the data model is automatically enforced across both the MAUI app and the Web API during compile time.

---

## 🛠️ Tech Stack & Tools

* **Language:** C# 12 / .NET 8
* **Frontend:** .NET MAUI (Multi-platform App UI)
* **Backend:** ASP.NET Core Web API
* **Databases:** PostgreSQL (Cloud), SQLite (Local)
* **ORM:** Entity Framework Core
* **OCR Engine:** Tesseract

---

## ⚙️ Development Setup

### Backend
1. Navigate to `RecipeNest.Backend/`.
2. Configure your connection string in `appsettings.json`.
3. Run `dotnet ef database update` followed by `dotnet run`.

### Mobile/Desktop App
1. Open `RecipeNest.sln` in Visual Studio 2022.
2. Select `RecipeNest.App` as the Startup Project.
3. Deploy to your desired target (Android, iOS, or Windows).

---

## 📝 Roadmap & Current Focus
- [x] Initial MAUI UI & MVVM Setup
- [x] SQLite Local Persistence
- [ ] **[IN PROGRESS]** ASP.NET Core Sync Endpoints
- [ ] **[IN PROGRESS]** PostgreSQL Schema Optimization
- [ ] Tesseract OCR Parsing Logic
