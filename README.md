# 🚀 Mastering .NET Core API Versioning: Build Future-Proof, Scalable APIs

## 🧠 Introduction

APIs are the backbone of digital transformation, but what happens when your business evolves and your API needs to change? Without a solid versioning strategy, you risk breaking client integrations and losing trust. In this article, I’ll show you how to master API versioning in .NET Core — ensuring your APIs remain robust, flexible, and future-ready.

---

## ❓ What is API Versioning?

APIs are contracts. Once published, clients depend on them. As your product grows, so do the demands on your API. Versioning lets you:

* Evolve your API without breaking existing clients.
* Support multiple versions simultaneously, giving clients time to migrate.
* Experiment with new features or designs safely.

> 📊 According to a 2024 Postman survey, **89% of developers** consider versioning essential for API reliability and client satisfaction.

---

## 1️⃣ API Versioning Strategies in .NET Core

.NET Core supports several versioning strategies via the `Asp.Versioning.Mvc.ApiExplorer` package.

### ✅ Install the NuGet Package

```bash
dotnet add package Asp.Versioning.Mvc.ApiExplorer
```

![Screenshot 2025-07-05 130856](https://github.com/user-attachments/assets/a1034046-1216-4f8c-ad26-16895e6d44d7)

### 1. URL Path Versioning:

**Example:** `/api/v1/products`

* Most visible & cache-friendly – the version lives in the route.

```csharp
// Program.cs
services.AddApiVersioning(o =>
{
    o.DefaultApiVersion = new ApiVersion(1, 0);
    o.AssumeDefaultVersionWhenUnspecified = true;
    o.ReportApiVersions = true;
    o.ApiVersionReader = new UrlSegmentApiVersionReader(); // 👈 key line
});

// Controllers
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/products")]
[ApiController]
public class ProductsController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    { 
        return Ok(new { version = "1.0", data = "Old SKU list" });
    }
}

[ApiVersion("2.0")]
[Route("api/v{version:apiVersion}/products")]
[ApiController]
public class ProductsV2Controller : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
       return Ok(new { version = "2.0", data = "New SKU list" });
    }
}
```

**Request examples:**

* `GET /api/v1/products` → returns v1 payload
* `GET /api/v2/products` → returns v2 payload

---

### 2. Query String Versioning:

**Example:** `/api/products?api-version=1.0`

* Keeps URLs clean but less discoverable.

```csharp
// Program.cs
services.AddApiVersioning(o =>
{
    o.DefaultApiVersion = new ApiVersion(1, 0);
    o.AssumeDefaultVersionWhenUnspecified = true;
    o.ReportApiVersions = true;
    o.ApiVersionReader = new QueryStringApiVersionReader("api-version");
});

// One controller, two actions mapped to versions
[Route("api/products")]
[ApiController]
public class ProductsController : ControllerBase
{
    [HttpGet]
    [MapToApiVersion("1.0")]
    public IActionResult GetV1()
    { 
        return Ok(new { version = "1.0", data = "Old SKU list" });
    }

    [HttpGet]
    [MapToApiVersion("2.0")]
    public IActionResult GetV2()
    {
        return Ok(new { version = "2.0", data = "New SKU list" });
    }
}
```

**Request examples:**

* `GET /api/products?api-version=1.0`
* `GET /api/products?api-version=2.0`

---

### 3. Header Versioning:

**Example:** `Header: api-version: 1.0`

* RESTful and clean but requires client setup.

```csharp
// Program.cs
services.AddApiVersioning(o =>
{
    o.DefaultApiVersion = new ApiVersion(1, 0);
    o.AssumeDefaultVersionWhenUnspecified = true;
    o.ReportApiVersions = true;
    o.ApiVersionReader = new HeaderApiVersionReader("api-version"); // 👈
});

// Same controller technique as query-string example
[Route("api/products")]
[ApiController]
public class ProductsController : ControllerBase
{
    [HttpGet]
    [MapToApiVersion("1.0")]
    public IActionResult GetV1() 
    {
	return Ok(new { version = "1.0" });
    }

    [HttpGet]
    [MapToApiVersion("2.0")]
    public IActionResult GetV2()
    {	
	return Ok(new { version = "2.0" });
    }
}
```

**Request examples:**

* `GET /api/products` with header `api-version: 1.0`
* `GET /api/products` with header `api-version: 2.0`

---

### 4. Media Type Versioning:

**Example:** `Accept: application/json;v=1.0`

* Powerful for content negotiation.

```csharp
// Program.cs
services.AddApiVersioning(o =>
{
    o.DefaultApiVersion = new ApiVersion(1, 0);
    o.AssumeDefaultVersionWhenUnspecified = true;
    o.ReportApiVersions = true;
    o.ApiVersionReader = new MediaTypeApiVersionReader("v");  // 👈
});

[Route("api/products")]
[ApiController]
public class ProductsController : ControllerBase
{
    [HttpGet]
    [MapToApiVersion("1.0")]
    public IActionResult GetV1() 
    {
	return Ok(new { version = "1.0" });
    }

    [HttpGet]
    [MapToApiVersion("2.0")]
    public IActionResult GetV2()
    {	
	return Ok(new { version = "2.0" });
    }
}
```

**Request examples:**

* `GET /api/products` with `Accept: application/json;v=1.0`
* `GET /api/products` with `Accept: application/json;v=2.0`

---

## 📈 2. Pros and Cons of Versioning Strategies

| Approach         | Pros                                      | Cons                                 |
|------------------|-------------------------------------------|--------------------------------------|
| URL Path         | Easy to use, visible, cacheable           | Can clutter routes                   |
| Query String     | Clean URLs, easy to implement             | Harder to cache, less visible        |
| Header           | RESTful, no URL changes                   | Requires client changes              |
| Media Type       | Flexible, supports negotiation            | Complex, less common                 |

---

## 🧾 3. Quick Comparison

| Strategy     | Visibility | RESTful | Client Config Needed | Cache Friendly |
| ------------ | ---------- | ------- | -------------------- | -------------- |
| URL Path     | ✅ High     | ✅ Yes   | ❌ No                 | ✅ Yes          |
| Query String | ⚠️ Medium  | ✅ Yes   | ❌ No                 | ⚠️ Partial     |
| Header       | ❌ Low      | ✅ Yes   | ✅ Yes                | ✅ Yes          |
| Media Type   | ❌ Low      | ✅ Yes   | ✅ Yes                | ⚠️ Partial     |

---

## 🖼️ 4. Visuals/Images: Understanding API Versioning at a Glance

Visuals simplify complex concepts. Here’s a diagram to help you visually understand how clients interact with different API versions through an API Gateway in a .NET Core environment:

**Key Elements:**

* 👥 Clients: Web, Mobile
* 🚪 API Gateway
* 🔀 Routes:

  * `/api/v1/products`
  * `/api/v2/products`
  * `/api/v3/products`
* 🧭 Versioning Strategies:

  * 🌐 URL Path
  * ❓ Query String
  * 📩 Header
  * 📰 Media Type
  * 
![Screenshot 2025-07-05 133733](https://github.com/user-attachments/assets/c0c9abdd-6653-41a6-aa89-cf6de0c0eb6b)

![Screenshot 2025-07-05 133811](https://github.com/user-attachments/assets/4ee0bf16-2442-4a03-80ef-0ffb6cfb3f15)

![Screenshot 2025-07-05 133844](https://github.com/user-attachments/assets/2123c270-998b-4736-a384-007fdc98fb0e)

---

## 🧪 5. Real-World Example: Versioning at Scale in a Growing SaaS Platform

A mid-sized SaaS company built their backend using .NET Core Web API. As their customer base grew, so did the demands for new features—but without breaking existing integrations for long-time users.

**🔍 Challenge:** Early on, they had only one API version (v1). Every change—new features, response structure updates—risked breaking production clients.

**✅ Solution:**

They implemented **URL-based API versioning** (/api/v1/, /api/v2/) using .NET Core API Versioning library. New features were added in v2, while v1 remained stable for legacy clients. They gradually rolled out v2 with a sunset strategy for v1.

**📈 Results:**

* ✅ 0% downtime during rollout
* 🚀 70% client migration to v2 within 6 months
* 👨‍💻 Increased developer productivity by isolating logic per version
  
This strategy gave them the flexibility to evolve their APIs without fear of regressions—future-proofing their system as they scaled.
---

## 📊 6. Data/Statistics: Why API Versioning Matters

* 🔁 **83%** of developers say managing breaking changes is a top challenge
* 🚨 APIs without versioning are **3x** more likely to cause client failures
* 🧪 Structured versioning reduces integration issues by **40%+**
* 📈 Improves API lifecycle management and developer experience

---

## 💬 Let's Talk!

🔄 How are you handling API changes in your projects?

🤔 Faced any challenges with versioning?

👇 Share your experiences in the comments — or 🤝 connect with me to discuss best practices and real-world solutions!

✅ Ready to make your APIs future-proof?

🚀 Start implementing versioning in your .NET Core projects today and build with confidence for tomorrow! 💡

---

## 📂 GitHub Project

Want to explore the code? Dive into the complete .NET Core API Versioning project here:
🔗 [https://github.com/sandip-Kalsariya/API-Versioning](https://github.com/sandip-Kalsariya/API-Versioning)
