![Architecture Flow](https://media-hosting.imagekit.io//437aa988320a41e8/ArchitectureFlow.png?Expires=1834122579&Key-Pair-Id=K2ZIVPTIP2VGHC&Signature=v-iN12AEjUAs6p~it1r4TbuJfeuaFMQxh8nV8e5TkYM-HQpn5glqOigGo3fQKtF3jpafqzeoyhyhfvosD4fs2lGQp2K9qHroAzToz6wa3NB2LAdI26GwpdYlI5rBAxprymMVYxGUdhvtjo-awiKOAny2~RkKNZFrsiPieOkzNEG9gJr5bp2GCecef-cDFimGTHdSv82wATSA03KGoE~jTIm6Stc5a18qUX3b7chzhANK1MVSK1TuHkhDXu-Pj63R1R6ol02RCVo134JMqfMndoJdXrcYXZaodOe7H4sxHJgOBORuBaz4V7MvEeaphK13BIF8LRpKkmE-cVykE68uYQ__)

# Event Aggregator Platform

*A scalable microservices-based event management platform*

## [🚀 Overview](pplx://action/followup)
The **Event Aggregator Platform** is a **multi-tenant event management system** designed for businesses to list events, allow users to register, and provide real-time notifications and analytics. Built using a **.NET 9** microservices architecture, it demonstrates **Domain-Driven Design (DDD), CQRS, event-driven architecture**, and advanced cloud-native practices.

## [🌟 Features](pplx://action/followup)
✅ **Multi-Tenant Event Management** – Businesses can create and manage events.
✅ **User Registration & Authentication** – Secure login via Firebase and custom authentication.
✅ **Real-Time Notifications** – Updates on event status, new registrations, and reminders.
✅ **Analytics & Insights** – Track user engagement, event popularity, and trends.
✅ **API Gateway** – Centralized entry point with secure routing.
✅ **gRPC & GraphQL** – Efficient service communication for performance optimization.
✅ **Event-Driven Architecture** – Powered by message queues for scalability.
✅ **Multi-Database Support** – PostgreSQL (SQL) and NoSQL for optimized storage.
✅ **Cloud-Ready Deployment** – Designed to be hosted on **Azure Kubernetes Service (AKS)** or other cloud platforms.

## [🏗️ Tech Stack](pplx://action/followup)
- **[Backend](pplx://action/followup)**: .NET 9 (C#), ASP.NET Core
- **[API Communication](pplx://action/followup)**: REST, gRPC, GraphQL
- **[Databases](pplx://action/followup)**: PostgreSQL, NoSQL (MongoDB or CosmosDB)
- **[Authentication](pplx://action/followup)**: Firebase Auth, JWT
- **[Message Queue](pplx://action/followup)**: RabbitMQ / Azure Service Bus
- **[Containerization](pplx://action/followup)**: Docker, Kubernetes
- **[CI/CD](pplx://action/followup)**: GitHub Actions
- **[Cloud Provider](pplx://action/followup)**: Azure (AKS, Azure Functions, Blob Storage, etc.)

## [🏛️ Microservices Architecture](pplx://action/followup)
The platform follows a **Domain-Driven Design (DDD) and CQRS approach**. It consists of multiple microservices:

| Service | Description |
|---------|-------------|
| **[API Gateway](pplx://action/followup)** | Routes requests to appropriate services |
| **[Identity Service](pplx://action/followup)** | Manages authentication & authorization |
| **[Event Service](pplx://action/followup)** | Handles event creation & management |
| **[Registration Service](pplx://action/followup)** | Manages user registration for events |
| **[Notification Service](pplx://action/followup)** | Sends real-time notifications (email, push) |
| **[Analytics Service](pplx://action/followup)** | Tracks event engagement and trends |

## [📦 Installation & Setup](pplx://action/followup)
### [🔹 Prerequisites](pplx://action/followup)
Ensure you have the following installed:
- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Docker](https://www.docker.com/get-started) (Optional for containerized setup)
- PostgreSQL / MongoDB (for database support)

### [🔹 Clone the Repository](pplx://action/followup)

git clone https://github.com/Pavan8374/event-aggregator-platform.git
cd event-aggregator-platform


### [🔹 Build & Run Services](pplx://action/followup)
#### [**Run via .NET CLI**](pplx://action/followup)

dotnet restore EventAggregatorPlatform.sln
dotnet build EventAggregatorPlatform.sln --configuration Release
dotnet run --project src/ApiGateway/ApiGateway.csproj


#### [**Run via Docker Compose**](pplx://action/followup)
docker-compose -f infrastructure/docker-compose.yml up --build


## [🚦 CI/CD Pipeline](pplx://action/followup)
- **[GitHub Actions](pplx://action/followup)** is configured to verify pull requests and ensure successful builds.
- Every PR triggers automatic **build validation** and unit tests.

## [📜 Contributing](pplx://action/followup)
We welcome contributions! To get started:
1. Fork the repository.
2. Create a new branch: `git checkout -b feature-branch`
3. Make your changes and commit: `git commit -m 'Added new feature'`
4. Push to your branch: `git push origin feature-branch`
5. Create a Pull Request.

## [🛠️ Future Enhancements](pplx://action/followup)
- 🌍 **Multi-language Support**
- 📅 **Calendar Integration** (Google Calendar, Outlook)
- 📲 **Mobile App Support** (Flutter/React Native)
- 🏷️ **AI-based Event Recommendations**

## [📜 License](pplx://action/followup)
This project is licensed under the [MIT License](LICENSE).

---
🎉 **Let's build the future of event management together!**

