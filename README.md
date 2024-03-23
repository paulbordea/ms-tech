# Microsoft Technical Exercise

## Sources and templates

[GitHub repo](https://github.com/paulbordea/ms-tech)

### Service A

[GitHub repo](https://github.com/paulbordea/ms-tech/tree/main/ServiceA)

A C# WebAPI application with 2 endpoints: 

* /current
* /average

### Service B

A simple nginx deployment that returns a 200 OK on every GET request.

### Docker

[Dockerfile Service A](https://github.com/paulbordea/ms-tech/tree/main/ServiceA/Dockerfile)
[Docker images Service A](https://hub.docker.com/r/paulbordea/servicea)
[Dockerfile Service B](https://github.com/paulbordea/ms-tech/tree/main/ServiceB/Dockerfile)
[Docker images Service B](https://hub.docker.com/r/paulbordea/serviceb)

### Kubernetes

[Terraform](https://github.com/paulbordea/ms-tech/tree/main/terraform)
[Manifests](https://github.com/paulbordea/ms-tech/tree/main/k8s)

## Using the application

### Deploy AKS

To deploy the AKS cluster we first need to have a valid Azure subscription and a storage account for storing the TF state.
Inside the 01-main.tf file we need to fill in the "backend" section with: SubscriptionId, storage account, container name and key.
To deploy the cluster we simply run terraform apply inside the **terraform** folder. The sensitive variables are stored in the _terraform.tfvars_ file.

### SolutionA

SolutionA is a .NET8 WebApi application under the **ServiceA** folder.
It has been dockerized and the image pushed to the _paulbordea/servicea_ repository.
There are 2 endpoints available: _/current_ and _/average_.

### SolutionB

This has no code, it simple contains inside the **ServiceB** folder a Dockerfile that inherits the _nginx_ image and copies the static index.html page which will be served for each GET request.
The resulting image is pushed to the _paulbordea/serviceb_ repository.

### AKS

The manifests for both services and the ingress resource are under the **k8s** folder.
The ingress declares a host and 2 backends thus making the 2 services available under ms-tech.com/servicea and ms-tech.com/serviceb.
