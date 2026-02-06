# Quick Start - AWS Deployment

## Prerequisites Checklist

- [ ] AWS Account created
- [ ] AWS CLI installed (`brew install awscli`)
- [ ] AWS CLI configured (`aws configure`)
- [ ] AWS SAM CLI installed (`brew install aws-sam-cli`)
- [ ] Docker Desktop installed and running
- [ ] Node.js 22+ installed

## First-Time Deployment

### 1. Restore Backend Dependencies

```bash
cd backend
dotnet restore
cd ..
```

### 2. Deploy Backend (Lambda + API Gateway)

```bash
./scripts/deploy-backend.sh
```

**First time will take ~5-10 minutes** (building Docker image, pushing to ECR, creating CloudFormation stack)

### 3. Deploy Frontend (S3 + CloudFront)

```bash
./scripts/deploy-frontend.sh prod
```

**Takes ~2-3 minutes** (building React app, uploading to S3, invalidating CloudFront)

### 4. Get Your URLs

```bash
# Get CloudFront URL (your app URL)
aws cloudformation describe-stacks \
  --stack-name encourager-app \
  --query 'Stacks[0].Outputs[?OutputKey==`CloudFrontUrl`].OutputValue' \
  --output text

# Get API Gateway URL (for testing)
aws cloudformation describe-stacks \
  --stack-name encourager-app \
  --query 'Stacks[0].Outputs[?OutputKey==`ApiUrl`].OutputValue' \
  --output text
```

## Testing

1. Open CloudFront URL in browser
2. Test API: `curl https://your-cloudfront-url.cloudfront.net/api/verse/random`

## Common Issues

### "Unable to get AWS Account ID"
- Run `aws configure` and verify credentials
- Test with: `aws sts get-caller-identity`

### "Docker build fails"
- Ensure Docker Desktop is running
- Check Docker is accessible: `docker ps`

### "SAM build fails"
- Verify SAM CLI is installed: `sam --version`
- Update SAM CLI: `brew upgrade aws-sam-cli`

### "ECR repository not found"
- The script creates it automatically
- Or create manually: `aws ecr create-repository --repository-name encourager-api --region us-east-1`

## Updating Deployment

After making code changes:

```bash
# Deploy backend changes
./scripts/deploy-backend.sh

# Deploy frontend changes
./scripts/deploy-frontend.sh prod
```

## Cost Monitoring

Set up billing alerts (see `README-DEPLOYMENT.md` for details):

```bash
# Expected cost: $0-5/month for low traffic
# Free tier covers:
# - 1M Lambda invocations/month
# - 1M API Gateway requests/month
# - 5GB S3 storage
# - 1TB CloudFront data transfer
```

## Next Steps

- Read `README-DEPLOYMENT.md` for detailed documentation
- Set up GitHub Actions CI/CD (see `.github/workflows/deploy.yml`)
- Configure custom domain (optional)
- Set up monitoring and alerts
