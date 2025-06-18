# 📦 Product Catalog – Full‑Stack Application

A simple product catalog featuring:

* **Backend** – ASP.NET Core Web API + Entity Framework Core
* **Frontend** – Plain HTML, Bootstrap 5, and vanilla JavaScript

Copy‑and‑paste this README into your project root so teammates can run either side entirely from the command line.

---

## 🖥️ Backend – ASP.NET Core Web API

### 🔧 Prerequisites

| Tool                 | Check Installed       | Install (if needed)                                                            |
| -------------------- | --------------------- | ------------------------------------------------------------------------------ |
| .NET SDK 6 or later  | `dotnet --version`    | [https://dotnet.microsoft.com/download](https://dotnet.microsoft.com/download) |
| EF Core CLI          | `dotnet ef --version` | `dotnet tool install --global dotnet-ef`                                       |
| SQL Server / LocalDB | *Optional*            | [https://aka.ms/sql-latest](https://aka.ms/sql-latest)                         |

> **Repo layout** (update if yours differs):
>
> ```text
> /backend     ← Web API .csproj lives here
> /frontend    ← index.html, script.js, etc.
> ```

---

### 🚀 Quick‑start

```bash
# 1 – open a terminal at repo root
dcd backend

# 2 – restore packages
dotnet restore

# 3 – create or update DB (if first run)
dotnet ef database update    # applies existing migrations
#   └─ or ──> create a fresh migration:
# dotnet ef migrations add Init
# dotnet ef database update

# 4 – run API
dotnet run
```

By default Kestrel listens on:

```
https://localhost:7000   (HTTPS)
http://localhost:7001    (HTTP)
```

---

### 📂 Seed Data

EF Core seeds three demo products in **ApplicationDbContext.OnModelCreating()**.  Comment them out or change as you wish.

---

### 🔌 API Endpoints

| Verb   | Endpoint             | Description             |
| ------ | -------------------- | ----------------------- |
| GET    | `/api/products`      | List all products       |
| GET    | `/api/products/{id}` | Get single product      |
| POST   | `/api/products`      | Add product (JSON body) |
| PUT    | `/api/products/{id}` | Update product          |
| DELETE | `/api/products/{id}` | Remove product          |

Example `POST` payload:

```json
{
  "name": "Sample Phone",
  "description": "A new phone",
  "price": 49999,
  "stock": 25,
  "imageUrl": "https://example.com/phone.jpg"
}
```

---

## 🌐 Frontend – Static HTML + JS

### 🔧 Prerequisites

* Any modern browser
* (Optional) [VS Code](https://code.visualstudio.com/) with the **Live Server** extension

---

### 🚀 Quick‑start

```bash
# 1 – open second terminal at repo root
cd frontend

# 2 – simple Python web server (choose one)
python -m http.server 8080             # Python 3.x
# OR Live Server from VS Code (Recommended)
```

Then visit [http://localhost:8080](http://localhost:8080).

#### Configure API URL

Edit `script.js`:

```javascript
const API_URL = 'https://localhost:7000/api/products';
```

Change port if you altered Kestrel’s launch settings.

#### Broken‑image fallback

`script.js` already includes an `onerror` handler that swaps to a placeholder if an image fails to load.

---

## 🔄 Common Commands

```bash
# Run backend tests (if you add any)
dotnet test

# Add new EF migration
dotnet ef migrations add <Name>

# Re‑create the DB from scratch (destructive!)
dotnet ef database drop -f && dotnet ef database update
```

---

## ❓ Troubleshooting

| Issue                        | Fix                                                                                                                                         |                                       |
| ---------------------------- | ------------------------------------------------------------------------------------------------------------------------------------------- | ------------------------------------- |
| **Port 7000 already in use** | Edit `Properties/launchSettings.json` or run \`netstat -ano                                                                                 | findstr :7000\` and kill the process. |
| **CORS error in browser**    | Ensure `builder.Services.AddCors` + `app.UseCors("AllowFrontend")` are enabled and that your frontend origin appears in `WithOrigins(...)`. |                                       |
| **EF precision warning**     | `Price` field is configured with `HasPrecision(18,2)`—no further action necessary.                                                          |                                       |

---

## 📜 License

MIT

---

## 👤 Author

Bharath Kumar Bellam
