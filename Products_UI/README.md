# Frontend (Product Catalog UI)

## 📁 Location

```
/frontend
```

## ⚙️ Prerequisites

Make sure you have the following installed:

```bash
# Node.js & npm (Install both via Node.js)
node -v
npm -v
```

If not installed:

```bash
# Windows/macOS/Linux
Download and install from:
https://nodejs.org/
```

## 🚀 How to Run the Frontend

```bash
cd frontend
npm install         # Install dependencies
npm run dev         # Start the development server
```

The frontend will be available at:

```
http://localhost:8080
```

> ⚠️ Make sure the backend API is running at `https://localhost:7000` (or whatever you configure). You can change the endpoint in your frontend JS code.

## 💡 Configuration

In your main frontend script (e.g., `main.js`, `index.js`, etc.), ensure this line matches your backend address:

```js
const API_URL = 'https://localhost:7000/api/products';
```

---

Let me know if you are using a framework like React, Vue, or Angular so I can tailor this guide further.
