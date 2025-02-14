# Event Aggregator Platform

![Event Aggregator Platform](https://imagekit.io/tools/asset-public-link?detail=%7B%22name%22%3A%22ArchitectureFlow.png%22%2C%22type%22%3A%22image%2Fpng%22%2C%22signedurl_expire%22%3A%222028-02-14T06%3A29%3A39.139Z%22%2C%22signedUrl%22%3A%22https%3A%2F%2Fmedia-hosting.imagekit.io%2F%2F437aa988320a41e8%2FArchitectureFlow.png%3FExpires%3D1834122579%26Key-Pair-Id%3DK2ZIVPTIP2VGHC%26Signature%3Dv-iN12AEjUAs6p~it1r4TbuJfeuaFMQxh8nV8e5TkYM-HQpn5glqOigGo3fQKtF3jpafqzeoyhyhfvosD4fs2lGQp2K9qHroAzToz6wa3NB2LAdI26GwpdYlI5rBAxprymMVYxGUdhvtjo-awiKOAny2~RkKNZFrsiPieOkzNEG9gJr5bp2GCecef-cDFimGTHdSv82wATSA03KGoE~jTIm6Stc5a18qUX3b7chzhANK1MVSK1TuHkhDXu-Pj63R1R6ol02RCVo134JMqfMndoJdXrcYXZaodOe7H4sxHJgOBORuBaz4V7MvEeaphK13BIF8LRpKkmE-cVykE68uYQ__%22%7D)  
*A scalable microservices-based event management platform*

## 🚀 Overview
The **Event Aggregator Platform** is a **multi-tenant event management system** designed for businesses to list events, allow users to register, and provide real-time notifications and analytics. Built using a **.NET 9** microservices architecture, it demonstrates **Domain-Driven Design (DDD), CQRS, event-driven architecture**, and advanced cloud-native practices.

## 🌟 Features
✅ **Multi-Tenant Event Management** – Businesses can create and manage events.  
✅ **User Registration & Authentication** – Secure login via Firebase and custom authentication.  
✅ **Real-Time Notifications** – Updates on event status, new registrations, and reminders.  
✅ **Analytics & Insights** – Track user engagement, event popularity, and trends.  
✅ **API Gateway** – Centralized entry point with secure routing.  
✅ **gRPC & GraphQL** – Efficient service communication for performance optimization.  
✅ **Event-Driven Architecture** – Powered by message queues for scalability.  
✅ **Multi-Database Support** – PostgreSQL (SQL) and NoSQL for optimized storage.  
✅ **Cloud-Ready Deployment** – Designed to be hosted on **Azure Kubernetes Service (AKS)** or other cloud platforms.  

## 🏗️ Tech Stack
- **Backend**: .NET 9 (C#), ASP.NET Core
- **API Communication**: REST, gRPC, GraphQL
- **Databases**: PostgreSQL, NoSQL (MongoDB or CosmosDB)
- **Authentication**: Firebase Auth, JWT
- **Message Queue**: RabbitMQ / Azure Service Bus
- **Containerization**: Docker, Kubernetes
- **CI/CD**: GitHub Actions
- **Cloud Provider**: Azure (AKS, Azure Functions, Blob Storage, etc.)

## 🏛️ Microservices Architecture
The platform follows a **Domain-Driven Design (DDD) and CQRS approach**. It consists of multiple microservices:

| Service | Description |
|---------|-------------|
| **API Gateway** | Routes requests to appropriate services |
| **Identity Service** | Manages authentication & authorization |
| **Event Service** | Handles event creation & management |
| **Registration Service** | Manages user registration for events |
| **Notification Service** | Sends real-time notifications (email, push) |
| **Analytics Service** | Tracks event engagement and trends |

## 📦 Installation & Setup
### 🔹 Prerequisites
Ensure you have the following installed:
- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Docker](https://www.docker.com/get-started) (Optional for containerized setup)
- PostgreSQL / MongoDB (for database support)

### 🔹 Clone the Repository
```sh
git clone https://github.com/Pavan8374/event-aggregator-platform.git
cd event-aggregator-platform
```

### 🔹 Build & Run Services
#### **Run via .NET CLI**
```sh
dotnet restore EventAggregatorPlatform.sln
dotnet build EventAggregatorPlatform.sln --configuration Release
dotnet run --project src/ApiGateway/ApiGateway.csproj
```

#### **Run via Docker Compose**
```sh
docker-compose -f infrastructure/docker-compose.yml up --build
```

## 🚦 CI/CD Pipeline
- **GitHub Actions** is configured to verify pull requests and ensure successful builds.
- Every PR triggers automatic **build validation** and unit tests.

## 📜 Contributing
We welcome contributions! To get started:
1. Fork the repository.
2. Create a new branch: `git checkout -b feature-branch`
3. Make your changes and commit: `git commit -m 'Added new feature'`
4. Push to your branch: `git push origin feature-branch`
5. Create a Pull Request.

## 🛠️ Future Enhancements
- 🌍 **Multi-language Support**
- 📅 **Calendar Integration** (Google Calendar, Outlook)
- 📲 **Mobile App Support** (Flutter/React Native)
- 🏷️ **AI-based Event Recommendations**

## 📜 License
This project is licensed under the [MIT License](LICENSE).

---
🎉 **Let's build the future of event management together!**

