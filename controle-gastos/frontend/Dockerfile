# Estágio de build: Node.js + Vite
FROM node:18-alpine AS build
WORKDIR /app

# Copiar package.json e instalar dependências
COPY package.json package-lock.json* ./
RUN npm install

# Copiar resto do código e buildar
COPY . .
RUN npm run build

# Estágio de runtime: Nginx
FROM nginx:alpine

# Copiar o build do React para o Nginx
COPY --from=build /app/dist /usr/share/nginx/html

# Copiar configuração customizada do Nginx para suportar SPA routing (React Router)
RUN echo 'server { \
    listen 80; \
    location / { \
        root /usr/share/nginx/html; \
        index index.html index.htm; \
        try_files $uri $uri/ /index.html; \
    } \
}' > /etc/nginx/conf.d/default.conf

EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
