# Deployment Guide

## Overview

Encourager uses AWS SAM (Serverless Application Model) for infrastructure deployment. The deployment process involves:

1. Building backend Docker image
2. Deploying infrastructure (Lambda, API Gateway, S3, CloudFront)
3. Building and uploading frontend
4. Invalidating CloudFront cache

## Prerequisites

### AWS Setup

1. **AWS Account**: Active AWS account
2. **AWS CLI Configured**: 
   ```bash
   aws configure
   ```
3. **AWS SAM CLI Installed**: See [Getting Started](./getting-started.md)
4. **Docker Running**: For building Lambda container image
5. **ECR Repository**: Create ECR repository for Lambda image
   ```bash
   aws ecr create-repository --repository-name encourager-api
   ```

### Environment Variables

Set these in your shell or CI/CD:

```bash
export AWS_REGION=us-east-1
export AWS_ACCOUNT_ID=123456789012
export ECR_REPOSITORY=encourager-api
export STACK_NAME=encourager-prod
```

## Deployment Process

### 1. Backend Deployment

**Manual Deployment:**

```bash
cd backend
./scripts/deploy-backend.sh
```

**What It Does:**

1. **Build Docker Image**
   ```bash
   docker build -t encourager-api:latest .
   ```

2. **Login to ECR**
   ```bash
   aws ecr get-login-password --region $AWS_REGION | \
     docker login --username AWS --password-stdin \
     $AWS_ACCOUNT_ID.dkr.ecr.$AWS_REGION.amazonaws.com
   ```

3. **Tag and Push Image**
   ```bash
   docker tag encourager-api:latest \
     $AWS_ACCOUNT_ID.dkr.ecr.$AWS_REGION.amazonaws.com/$ECR_REPOSITORY:latest
   docker push $AWS_ACCOUNT_ID.dkr.ecr.$AWS_REGION.amazonaws.com/$ECR_REPOSITORY:latest
   ```

4. **Deploy SAM Stack**
   ```bash
   cd ../infrastructure
   sam deploy \
     --stack-name $STACK_NAME \
     --template-file template.yaml \
     --capabilities CAPABILITY_IAM \
     --parameter-overrides \
       Environment=prod \
       ImageTag=latest \
       AllowedOrigin=https://your-cloudfront-url.cloudfront.net
   ```

### 2. Frontend Deployment

**Manual Deployment:**

```bash
cd frontend
./scripts/deploy-frontend.sh
```

**What It Does:**

1. **Build React App**
   ```bash
   npm run build
   ```

2. **Upload to S3**
   ```bash
   aws s3 sync dist/ s3://$S3_BUCKET_NAME/ \
     --delete \
     --cache-control "public, max-age=31536000, immutable"
   ```

3. **Invalidate CloudFront**
   ```bash
   aws cloudfront create-invalidation \
     --distribution-id $CLOUDFRONT_DISTRIBUTION_ID \
     --paths "/*"
   ```

## CI/CD Deployment (GitHub Actions)

### Workflow Files

**`.github/workflows/ci.yml`** - Build and test
**`.github/workflows/deploy.yml`** - Deploy to production

### Deployment Triggers

- **Push to `main`**: Auto-deploy to production
- **Push to `develop`**: Deploy to staging (if configured)
- **Manual**: Trigger via GitHub Actions UI

### GitHub Secrets Required

Set these in GitHub repository settings:

- `AWS_ACCESS_KEY_ID`: AWS access key
- `AWS_SECRET_ACCESS_KEY`: AWS secret key
- `AWS_REGION`: AWS region (e.g., `us-east-1`)
- `ECR_REPOSITORY`: ECR repository name
- `S3_BUCKET_NAME`: S3 bucket name (from SAM outputs)
- `CLOUDFRONT_DISTRIBUTION_ID`: CloudFront distribution ID (from SAM outputs)

## Environment Configuration

### Development

**Local:**
- Backend: `http://localhost:5226`
- Frontend: `http://localhost:5173`
- CORS: `*` (all origins)

### Staging

**Configuration:**
- Environment: `staging`
- Stack Name: `encourager-staging`
- CORS: Staging CloudFront URL

### Production

**Configuration:**
- Environment: `prod`
- Stack Name: `encourager-prod`
- CORS: Production CloudFront URL
- `ALLOWED_ORIGIN`: Set to CloudFront URL (not `*`)

## SAM Template Parameters

### Required Parameters

| Parameter | Description | Example |
|-----------|-------------|---------|
| `Environment` | Environment name | `prod`, `staging`, `dev` |
| `ImageTag` | Docker image tag | `latest`, `v1.0.0` |
| `AllowedOrigin` | CORS origin | `https://*.cloudfront.net` |

### Optional Parameters

| Parameter | Description | Default |
|-----------|-------------|---------|
| `DomainName` | Custom domain name | `""` |

## Deployment Checklist

### Pre-Deployment

- [ ] All tests passing
- [ ] Code reviewed and approved
- [ ] Version bumped (if needed)
- [ ] Changelog updated
- [ ] Documentation updated

### Backend Deployment

- [ ] Docker image builds successfully
- [ ] ECR login successful
- [ ] Image pushed to ECR
- [ ] SAM template valid
- [ ] Stack deployment successful
- [ ] Lambda function updated
- [ ] Health endpoint returns 200

### Frontend Deployment

- [ ] Build succeeds without errors
- [ ] TypeScript compilation passes
- [ ] S3 upload successful
- [ ] CloudFront invalidation created
- [ ] App loads in browser
- [ ] All routes work
- [ ] PWA installable

### Post-Deployment

- [ ] Smoke test: Visit app URL
- [ ] Test: Get random verse
- [ ] Test: Daily blessing rule
- [ ] Test: Language switching
- [ ] Test: Offline functionality
- [ ] Monitor CloudWatch logs
- [ ] Check CloudFront metrics
- [ ] Verify health endpoint

## Rollback Procedure

### Backend Rollback

1. **Revert Code**
   ```bash
   git revert <commit-hash>
   git push
   ```

2. **Redeploy Previous Image**
   ```bash
   # Tag previous image
   docker tag encourager-api:previous encourager-api:latest
   # Push and deploy
   ```

3. **Or Update SAM Stack**
   ```bash
   sam deploy --parameter-overrides ImageTag=previous-tag
   ```

### Frontend Rollback

1. **Revert Code**
   ```bash
   git revert <commit-hash>
   git push
   ```

2. **Redeploy Previous Build**
   ```bash
   git checkout <previous-commit>
   npm run build
   aws s3 sync dist/ s3://$S3_BUCKET_NAME/
   aws cloudfront create-invalidation --distribution-id $ID --paths "/*"
   ```

## Monitoring

### CloudWatch Logs

**Lambda Logs:**
```bash
aws logs tail /aws/lambda/encourager-api --follow
```

**Filter Errors:**
```bash
aws logs filter-log-events \
  --log-group-name /aws/lambda/encourager-api \
  --filter-pattern "ERROR"
```

### Health Monitoring

**Check Health Endpoint:**
```bash
curl https://your-api-url/api/health
```

**Expected Response:**
```json
{
  "status": "healthy",
  "timestamp": "2026-02-06T12:34:56.789Z"
}
```

### CloudFront Metrics

Monitor in AWS Console:
- Request count
- Error rate
- Cache hit ratio
- Data transfer

## Troubleshooting Deployment

### Backend Issues

**Docker Build Fails:**
- Check Dockerfile syntax
- Verify .NET SDK version
- Check build context

**ECR Push Fails:**
- Verify AWS credentials
- Check ECR repository exists
- Verify IAM permissions

**SAM Deploy Fails:**
- Check template syntax: `sam validate`
- Verify IAM permissions
- Check stack status: `aws cloudformation describe-stacks`

**Lambda Not Updating:**
- Check image tag matches
- Verify ECR image exists
- Check Lambda function configuration

### Frontend Issues

**Build Fails:**
- Check TypeScript errors
- Verify dependencies installed
- Check Node.js version

**S3 Upload Fails:**
- Verify AWS credentials
- Check S3 bucket exists
- Verify IAM permissions

**CloudFront Not Updating:**
- Wait for invalidation (can take 5-15 minutes)
- Check invalidation status
- Verify distribution is deployed

**App Not Loading:**
- Check S3 bucket policy
- Verify CloudFront origin configuration
- Check browser console for errors

## Security Best Practices

1. **CORS Configuration**: Set `ALLOWED_ORIGIN` to CloudFront URL (not `*`)
2. **S3 Bucket**: Private bucket, CloudFront OAC only
3. **HTTPS**: Enforce HTTPS in CloudFront
4. **IAM Roles**: Use least privilege principle
5. **Secrets**: Store secrets in GitHub Secrets, not code
6. **Environment Variables**: Don't commit sensitive data

## Cost Optimization

1. **Lambda**: Right-size memory (512 MB is sufficient)
2. **CloudFront**: Use appropriate cache TTLs
3. **S3**: Use appropriate storage class
4. **API Gateway**: Monitor request volume
5. **CloudWatch**: Set log retention limits

## Advanced Topics

### Custom Domain

1. **Request Certificate**: AWS Certificate Manager
2. **Update CloudFront**: Add custom domain
3. **Update DNS**: Point domain to CloudFront
4. **Update SAM**: Add `DomainName` parameter

### Multi-Environment

1. **Separate Stacks**: One stack per environment
2. **Parameter Files**: Use `samconfig.toml`
3. **Environment Variables**: Set per environment
4. **Separate ECR**: One repository per environment (optional)

### Blue-Green Deployment

1. **Deploy New Version**: Deploy alongside current
2. **Test New Version**: Verify functionality
3. **Switch Traffic**: Update CloudFront origin
4. **Monitor**: Watch for errors
5. **Cleanup**: Remove old version

## Resources

- [AWS SAM Documentation](https://docs.aws.amazon.com/serverless-application-model/)
- [CloudFormation Documentation](https://docs.aws.amazon.com/cloudformation/)
- [Lambda Container Images](https://docs.aws.amazon.com/lambda/latest/dg/images-create.html)
- [CloudFront Best Practices](https://docs.aws.amazon.com/AmazonCloudFront/latest/DeveloperGuide/best-practices.html)
