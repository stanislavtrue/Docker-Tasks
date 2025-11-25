<h1 align="center">Завдання №5</h1>
<h2 align="center">Намалювати спрощену C4- або UML-діаграму системи</h2>
<p align="center">
  <img src="https://github.com/user-attachments/assets/679f5c0a-1a7a-483d-9f50-cb717b114cf6"
</p>
<h2 align="center">Реалізувати цю архітектуру у вигляді docker-compose.yml з 3–4 сервісів</h2>

```docker-compose.yml
services:
  frontend:
    build:
      context: ./frontend
      dockerfile: Dockerfile
    ports:
      - "3000:3000"
    depends_on:
      - backend

  backend:
    build: 
      context: ./backend
      dockerfile: Dockerfile
    ports:
      - "8080:8080"
    container_name: notes_backend
    depends_on:
      - db
      - redis
  
  db:
    image: docker.io/library/postgres:15
    ports:
      - "5432:5432"
    environment:
      - POSTGRES_USER=postgres
      - POSTGRES_PASSWORD=1234
    volumes:
      - db_data: var/lib/postgresql/data
  
  redis:
    image: docker.io/library/redis:alpine
    ports:
      - "6379:6379"

volumes:
  db_data:
```
