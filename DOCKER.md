# TrakeadorWeb - Docker Setup

Este diretório contém os arquivos de configuração Docker para executar a aplicação TrakeadorWeb.

## Arquivos Docker

- **Dockerfile**: Define a imagem Docker da aplicação
- **docker-compose.yml**: Orquestra os serviços necessários
- **.dockerignore**: Otimiza o build excluindo arquivos desnecessários
- **appsettings.Production.json**: Configurações específicas para produção

## Como executar

### Opção 1: Usando o Script de Gerenciamento (Recomendado)

```bash
# Tornar o script executável (apenas na primeira vez)
chmod +x manage-docker.sh

# Iniciar a aplicação
./manage-docker.sh start

# Ver outros comandos disponíveis
./manage-docker.sh help
```

### Opção 2: Usando Docker Compose diretamente

```bash
# No diretório raiz do projeto (onde está o docker-compose.yml)
docker-compose up -d
```

### Opção 3: Usando Docker diretamente

```bash
# Fazer build da imagem
cd TrakeadorWeb
docker build -t trakeador-web .

# Executar o container
docker run -d \
  --name trakeador-web \
  -p 8080:8080 \
  -v trakeador-data:/app/data \
  trakeador-web
```

## Acessando a aplicação

Após iniciar os containers, acesse:
- **URL**: http://localhost:8080
- **Usuário admin**: admin@trakeador.com
- **Senha**: Admin@123

## Script de Gerenciamento

O script `manage-docker.sh` fornece comandos convenientes para gerenciar a aplicação:

```bash
./manage-docker.sh start     # Iniciar a aplicação
./manage-docker.sh stop      # Parar a aplicação
./manage-docker.sh restart   # Reiniciar a aplicação
./manage-docker.sh rebuild   # Rebuild e reiniciar
./manage-docker.sh logs      # Ver logs em tempo real
./manage-docker.sh status    # Ver status dos containers
./manage-docker.sh health    # Verificar saúde da aplicação
./manage-docker.sh backup    # Fazer backup do banco
./manage-docker.sh shell     # Acessar shell do container
./manage-docker.sh clean     # Parar e remover volumes (CUIDADO!)
./manage-docker.sh help      # Ver todos os comandos
```

## Comandos Docker tradicionais

Se preferir usar Docker Compose diretamente:

```bash
# Ver logs
docker-compose logs -f trakeador-web

# Parar os serviços
docker-compose down

# Rebuild e restart
docker-compose up -d --build

# Acessar o container
docker-compose exec trakeador-web /bin/bash

# Ver status dos containers
docker-compose ps

# Remover volumes (atenção: apaga o banco de dados)
docker-compose down -v
```

## Configurações importantes

- **Porta**: A aplicação roda na porta 8080 dentro do container
- **Banco de dados**: SQLite armazenado no volume `trakeador-data`
- **Health check**: Disponível em `/health`
- **Dados persistentes**: O banco de dados é mantido no volume Docker

## Estrutura de volumes

```
trakeador-data/
└── trakeador.db  # Banco de dados SQLite
```

## Troubleshooting

### Container não inicia
```bash
# Ver logs detalhados
docker-compose logs trakeador-web

# Verificar se a porta está sendo usada
netstat -tlnp | grep :8080
```

### Problemas de permissão
```bash
# Verificar se o volume foi criado corretamente
docker volume inspect trakeador_trakeador-data
```

### Reset completo
```bash
# Parar tudo e remover volumes
docker-compose down -v

# Remover imagens
docker rmi trakeador_trakeador-web

# Rebuild completo
docker-compose up -d --build
```

## Ambiente de produção

Para produção, considere:

1. **Variáveis de ambiente**: Configure através do docker-compose.yml
2. **Reverse proxy**: Use nginx ou traefik na frente
3. **SSL/TLS**: Configure certificados apropriados  
4. **Backup**: Configure backup regular do volume `trakeador-data`
5. **Monitoramento**: Use o endpoint `/health` para monitoramento
6. **Logs**: Configure log aggregation se necessário