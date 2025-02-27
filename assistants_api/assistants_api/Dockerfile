FROM openjdk:8
MAINTAINER Harris<harris.li@huameisoft.cn>

RUN mkdir -p /home/gen-ai/assistants-api
RUN mkdir -p /home/gen-ai/logs
RUN mkdir -p /home/gen-ai/code

WORKDIR /home/gen-ai

COPY target/AssistantsApi-1.0.jar /home/gen-ai/assistants-api/AssistantsApi.jar
COPY logs /home/gen-ai/logs/
COPY code /home/gen-ai/code/

EXPOSE 8080

ENTRYPOINT ["java", "-jar", "-Duser.timezone=GMT+8", "/home/gen-ai/assistants-api/AssistantsApi.jar"]