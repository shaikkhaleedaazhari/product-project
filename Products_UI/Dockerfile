FROM python:3.10-slim
WORKDIR /app
COPY . .
EXPOSE 8080
CMD sh -c 'echo "window.ENV = { API_URL: \"${API_URL}\" };" > env.js && python3 -m http.server 8080 --bind 0.0.0.0'
