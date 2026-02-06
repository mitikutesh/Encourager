# AWS Deployment Guide

This guide explains how to deploy the Encourager app to AWS using Lambda + API Gateway + S3 + CloudFront.

## Architecture

- **Backend**: AWS Lambda (container image) + API Gateway
- **Frontend**: S3 (static hosting) + CloudFront (CDN)
- **Estimated Cost**: $0-5/month for low traffic (within AWS free tier)

## Prerequisites

1. **AWS Account**: Sign up at https://aws.amazon.com
2. **AWS CLI**: Install and configure
   ```bash
   brew install awscli  # macOS
   aws configure
   ```
3. **AWS SAM CLI**: Install
   ```bash
   brew install aws-sam-cli  # macOS
   # Or: pip install aws-sam-cli
   ```
4. **Docker Desktop**: Required for building Lambda container images
5. **Node.js 22+**: Required for frontend builds

## Initial Setup

### 1. Configure AWS Credentials

```bash
aws configure
# Enter your AWS Access Key ID
# Enter your AWS Secret Access Key
# Enter default region (e.g., us-east-1)
# Enter default output format (json)
```

### 2. Create ECR Repository (if not exists)

The deployment script will create this automatically, but you can create it manually:

```bash
aws ecr create-repository --repository-name encourager-api --region us-east-1
```

## Deployment Steps

### Option 1: Manual Deployment

#### Deploy Backend (Lambda + API Gateway)

```bash
./scripts/deploy-backend.sh
```

This script will:
1. Build the Docker image for Lambda
2. Push the image to ECR
3. Deploy the SAM stack (creates Lambda, API Gateway, S3, CloudFront)

#### Deploy Frontend (S3 + CloudFront)

```bash
./scripts/deploy-frontend.sh prod
```

This script will:
1. Build the React frontend
2. Upload files to S3
3. Invalidate CloudFront cache

### Option 2: Automated CI/CD (GitHub Actions)

1. **Add GitHub Secrets**:
   - Go to your GitHub repository → Settings → Secrets and variables → Actions
   - Add `AWS_ACCESS_KEY_ID`
   - Add `AWS_SECRET_ACCESS_KEY`

2. **Push to main branch**:
   ```bash
   git push origin main
   ```

3. The workflow will automatically:
   - Deploy backend when backend changes are detected
   - Deploy frontend after backend deployment completes

## Environment Variables

You can customize deployment by setting environment variables:

```bash
export AWS_REGION=us-east-1
export STACK_NAME=encourager-app
./scripts/deploy-backend.sh
```

## Getting Deployment URLs

After deployment, get your URLs:

```bash
# Get API Gateway URL
aws cloudformation describe-stacks \
  --stack-name encourager-app \
  --query 'Stacks[0].Outputs[?OutputKey==`ApiUrl`].OutputValue' \
  --output text

# Get CloudFront URL
aws cloudformation describe-stacks \
  --stack-name encourager-app \
  --query 'Stacks[0].Outputs[?OutputKey==`CloudFrontUrl`].OutputValue' \
  --output text
```

## Testing

1. **Test API directly**:
   ```bash
   curl https://your-api-gateway-url.execute-api.us-east-1.amazonaws.com/api/verse/random
   ```

2. **Test via CloudFront**:
   Open `https://your-cloudfront-url.cloudfront.net` in your browser

3. **Test API via CloudFront**:
   ```bash
   curl https://your-cloudfront-url.cloudfront.net/api/verse/random
   ```

## Monitoring

### View Lambda Logs

```bash
aws logs tail /aws/lambda/encourager-app-VerseApiFunction --follow
```

### View CloudFormation Stack

```bash
aws cloudformation describe-stacks --stack-name encourager-app
```

### View CloudFront Distribution

```bash
aws cloudfront list-distributions \
  --query "DistributionList.Items[?Comment=='Encourager App Distribution']"
```

## Cost Monitoring

### Set Up Billing Alerts

```bash
# Create SNS topic
aws sns create-topic --name billing-alerts

# Subscribe to topic (replace with your email)
TOPIC_ARN=$(aws sns list-topics --query 'Topics[?contains(TopicArn, `billing-alerts`)].TopicArn' --output text)
aws sns subscribe --topic-arn $TOPIC_ARN --protocol email --notification-endpoint your-email@example.com

# Create CloudWatch billing alarm ($5 threshold)
aws cloudwatch put-metric-alarm \
  --alarm-name encourager-billing-alert-5 \
  --alarm-description "Alert when estimated charges exceed $5" \
  --metric-name EstimatedCharges \
  --namespace AWS/Billing \
  --statistic Maximum \
  --period 86400 \
  --evaluation-periods 1 \
  --threshold 5 \
  --comparison-operator GreaterThanThreshold \
  --dimensions Name=Currency,Value=USD
```

## Troubleshooting

### Lambda Cold Starts

First request may take 2-5 seconds. This is normal for serverless functions.

### CORS Issues

Ensure API Gateway CORS is configured (already done in SAM template). Check CloudFront is forwarding headers correctly.

### CloudFront Cache

API responses are cached for 5 minutes (300s). To clear cache immediately:

```bash
DIST_ID=$(aws cloudformation describe-stacks --stack-name encourager-app --query 'Stacks[0].Outputs[?OutputKey==`CloudFrontDistributionId`].OutputValue' --output text)
aws cloudfront create-invalidation --distribution-id $DIST_ID --paths "/*"
```

### Build Failures

- **Docker build fails**: Ensure Docker Desktop is running
- **SAM build fails**: Ensure SAM CLI is installed and up to date
- **ECR push fails**: Check AWS credentials and ECR repository exists

### Frontend Not Updating

1. Check S3 bucket contents: `aws s3 ls s3://encourager-frontend-prod-ACCOUNT_ID/`
2. Check CloudFront invalidation status
3. Hard refresh browser (Cmd+Shift+R / Ctrl+Shift+R)

## Cleanup

To delete all resources:

```bash
aws cloudformation delete-stack --stack-name encourager-app

# Wait for stack deletion, then delete ECR repository
aws ecr delete-repository --repository-name encourager-api --force
```

## Local Development

The app still works locally:

```bash
# Backend
cd backend
dotnet run

# Frontend (in another terminal)
cd frontend
npm run dev
```

The frontend will proxy `/api/*` requests to `http://localhost:5226` (configured in `vite.config.ts`).

## Cost Breakdown

**Monthly Estimates (Low Traffic - 10K requests/month)**:
- S3 Storage (100MB): $0.002
- S3 Requests: $0.05
- CloudFront Data Transfer (5GB): $0.43
- Lambda Invocations: $0.00 (free tier)
- API Gateway: $0.00 (free tier)
- **Total: ~$0.50/month**

**Monthly Estimates (Moderate Traffic - 100K requests/month)**:
- S3 Storage: $0.002
- S3 Requests: $0.50
- CloudFront Data Transfer (50GB): $4.25
- Lambda Invocations: $0.00 (free tier)
- API Gateway: $0.00 (free tier)
- **Total: ~$5/month**

**After Free Tier (1M+ requests/month)**:
- Add ~$3.50 per 1M API Gateway requests
- Add ~$0.20 per 1M Lambda invocations
- **Total: ~$8-15/month**
