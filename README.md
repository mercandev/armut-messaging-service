## Armut Messaging Service

The chat structure of the messaging service works as follows. Developed with WebSocket. The structure is only suitable for two users to text at the same time. Multiple sessions are not supported. Users must be logged in and received a token in order to send a message to the username they entered. User can see the chat rooms and message details used outside of messaging.

## Structure
<img width="1000" alt="armut_messaging_service_diagram" src="https://user-images.githubusercontent.com/22862224/224198744-920c30f5-5120-4b0a-b5d4-24afbfc3abe9.png">

## Step-by-step Installation

Run the docker command below to install "MongoDb" , "ElasticSearch" , "Kibana" required for the project.

```bash
  docker compose up
```

Run the following command to create the image of the application.

```bash
  sudo docker build -t armutmessaging .
```

Run the following command to start the created image as a container from port 8080:8080.

```bash
  docker run -p 8080:8080 --name armutmessaging armutmessaging . 
```

Run the following command to get the created container to the same network as other containers.

```bash
  docker network connect armut-messaging-service_bridge armutmessaging
```

After the installations are complete, you can see the containers made up of the Docker Desktop application. You can see the same by running the following code from the terminal.

```bash
  docker ps
```

<img width="1000" alt="image" src="https://user-images.githubusercontent.com/22862224/224241128-1ae2340f-9841-49d7-b9e5-69e1a8999579.png">

## Test

To test the application, enter the url to test with the "Websocket Request" option on "Postman".


<img width="1000" alt="image" src="https://user-images.githubusercontent.com/22862224/224198496-508be300-48fb-4eff-b25e-697cec33b799.png"> <img width="1000" alt="image" src="https://user-images.githubusercontent.com/22862224/224198555-0f4c34b8-1ee6-4db6-92af-8befbb2f507c.png">

## Kibana & Error and Informations Logs

After ElasticSearch and Kibana are installed, go to the following index by following the path of Discover -> Create Index while opening Kibana. When you add the following command to the box that appears in front of you, you will be able to see the errors and notifications that occur in the application.


```bash
  armut.ms.api--logs-*
```

<img width="1000" alt="image" src="https://user-images.githubusercontent.com/22862224/224200183-015f1316-526b-4ac0-98eb-a17e0d76f20a.png">


