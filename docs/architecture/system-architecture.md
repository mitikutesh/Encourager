# System Architecture

## Overview

Encourager follows a serverless architecture pattern, leveraging AWS services for scalability and cost-effectiveness. The system is split into three main components: frontend, backend API, and infrastructure.

## High-Level Architecture

```mermaid
graph TB
    subgraph "Client Layer"
        User[User Device]
        Browser[Web Browser]
        PWA[PWA App]
    end

    subgraph "CDN & Distribution"
        CF[CloudFront CDN]
    end

    subgraph "Frontend"
        S3[S3 Bucket<br/>Static Assets]
    end

    subgraph "API Layer"
        APIGW[API Gateway<br/>REST API]
    end

    subgraph "Backend"
        Lambda[AWS Lambda<br/>.NET 10 Container]
        VerseService[VerseService<br/>Singleton]
        VerseData[Verse Data Classes<br/>English/Amharic/Finnish]
    end

    subgraph "Storage"
        LocalStorage[Browser localStorage<br/>Daily Blessing State]
    end

    User -->|Scan QR Code| Browser
    Browser -->|HTTPS| CF
    CF -->|Static Assets| S3
    CF -->|/api/* Requests| APIGW
    APIGW -->|Invoke| Lambda
    Lambda --> VerseService
    VerseService --> VerseData
    PWA -->|Read/Write| LocalStorage
    Browser -->|Install| PWA

    style CF fill:#1a374f,color:#fff
    style Lambda fill:#6f9078,color:#fff
    style S3 fill:#d06450,color:#fff
    style APIGW fill:#6f9078,color:#fff
```

## Architecture Layers

### 1. Client Layer
- **Web Browser**: Initial access point via QR code or direct URL
- **PWA**: Progressive Web App installed on user's device
- **Offline Support**: Service worker caches assets and API responses

### 2. CDN & Distribution
- **CloudFront**: Global content delivery network
- Routes `/api/*` requests to API Gateway
- Serves static assets from S3
- Handles HTTPS termination and caching

### 3. Frontend Layer
- **S3 Bucket**: Hosts React SPA build artifacts
- **React 19**: Modern UI framework with TypeScript
- **Tailwind CSS 4**: Utility-first styling
- **Vite**: Build tool and dev server

### 4. API Layer
- **API Gateway**: RESTful API endpoint
- Handles CORS configuration
- Routes requests to Lambda function
- Provides request/response transformation

### 5. Backend Layer
- **AWS Lambda**: Serverless compute (container image)
- **.NET 10 Minimal APIs**: Lightweight web framework
- **VerseService**: Singleton service managing verse data
- **In-Memory Data**: Static verse collections (no database)

### 6. Storage Layer
- **Browser localStorage**: Client-side persistence
- Stores daily blessing state (`last_blessing_data`)
- Stores language preference (`lang`)

## Key Design Decisions

1. **Serverless Architecture**: No servers to manage, auto-scaling, pay-per-use
2. **Static Frontend**: S3 + CloudFront for global distribution and caching
3. **In-Memory Backend**: No database needed for static verse data (~50 verses per language)
4. **Client-Side State**: Daily blessing rule enforced in browser, no backend state
5. **Dual Entry Points**: `Program.cs` for local dev, `LambdaEntryPoint.cs` for AWS

## Technology Choices

- **React 19**: Latest React features, compiler optimizations
- **.NET 10**: Modern C# 14 features, minimal APIs
- **AWS SAM**: Infrastructure as Code for CloudFormation
- **Docker**: Containerized Lambda deployment
- **GitHub Actions**: CI/CD automation
