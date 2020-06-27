# calcula-juros-api

API que calcula juros compostos

* Azure App Service: https://api-calcula-juros.azurewebsites.net/swagger/index.html

### comandos Docker ###

* docker build -t api-calcula-juros-imagem -f Dockerfile .
* docker images
* docker run --name api-calcula-juros -d -p 5008:80 --rm api-calcula-juros-imagem
* docker ps -a
* docker stop api-calcula-juros
* docker rm api-calcula-juros
* docker rmi api-calcula-juros-imagem:latest

### comandos Docker Compose ###

* docker-compose up
* docker-compose down
