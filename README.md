# TrakeadorWeb - Sistema de Rastreamento de Links

Sistema web para geração e rastreamento de links afiliados para casas de apostas, com gestão de experts e códigos personalizados.

## 🚀 Características

- **Geração de Links Rastreados**: Transforma links simples em links com códigos de afiliado
- **Gestão de Experts**: Cadastro e gerenciamento de especialistas em apostas
- **Casas de Apostas Suportadas**: Esportiva, Novibet, BetMGM
- **Sistema de Usuários**: Autenticação e autorização com ASP.NET Identity
- **Interface Responsiva**: Bootstrap 5 com design moderno
- **Docker Ready**: Containerização completa para deploy fácil

## 🏗️ Tecnologias

- **.NET 9**: Framework principal
- **ASP.NET Core MVC**: Arquitetura web
- **Blazor**: Componentes interativos
- **Entity Framework Core**: ORM para banco de dados
- **SQLite**: Banco de dados local
- **Bootstrap 5**: Framework CSS
- **Docker**: Containerização

## 📦 Executar com Docker (Recomendado)

### Pré-requisitos
- Docker
- Docker Compose

### Início Rápido

```bash
# Clonar o repositório
git clone <repo-url>
cd trakeador

# Executar a aplicação
./manage-docker.sh start

# Ou usando docker-compose diretamente
docker-compose up -d
```

A aplicação estará disponível em: **http://localhost:8080**

### Credenciais Padrão
- **Usuário**: admin@trakeador.com
- **Senha**: Admin@123

## 🛠️ Scripts de Gerenciamento

### Script Principal (`manage-docker.sh`)
```bash
# Iniciar aplicação
./manage-docker.sh start

# Parar aplicação
./manage-docker.sh stop

# Ver logs
./manage-docker.sh logs

# Status da aplicação
./manage-docker.sh status

# Backup do banco
./manage-docker.sh backup

# Ver todas as opções
./manage-docker.sh help
```

### Backup Manual
```bash
# Executar backup
./backup.sh

# Os backups ficam em ./backups/
```

## 🏃‍♂️ Executar Localmente (Desenvolvimento)

### Pré-requisitos
- .NET 9 SDK
- SQLite

### Comandos

```bash
cd TrakeadorWeb

# Restaurar dependências
dotnet restore

# Executar migrações
dotnet ef database update

# Executar aplicação
dotnet run
```

## 📋 Funcionalidades

### 1. Gestão de Experts
- Cadastro de especialistas em apostas
- Ativação/desativação de experts
- Histórico de criação

### 2. Casas de Apostas
- Suporte para múltiplas casas
- Configuração de URLs base
- Gestão de status ativo/inativo

### 3. Associações Expert-Casa
- Códigos de afiliado personalizados por expert
- Parâmetros adicionais específicos
- Gestão de relacionamentos

### 4. Geração de Links
- Interface pública para gerar links
- Seleção de expert e casa de apostas
- Transformação automática de URLs

### 5. Sistema de Usuários
- Autenticação segura
- Gestão de usuários administrativa
- Reset de senhas
- Controle de acesso

## 🗂️ Estrutura do Projeto

```
trakeador/
├── TrakeadorWeb/           # Aplicação principal
│   ├── Controllers/        # Controladores MVC
│   ├── Data/              # Contexto e migrações
│   ├── Models/            # Modelos de dados
│   ├── Services/          # Lógica de negócio
│   ├── Views/             # Views Razor
│   ├── ViewModels/        # ViewModels
│   └── wwwroot/           # Arquivos estáticos
├── docker-compose.yml     # Orquestração Docker
├── manage-docker.sh       # Script de gerenciamento
├── backup.sh             # Script de backup
├── DOCKER.md             # Documentação Docker
└── README.md             # Este arquivo
```

## 🔧 Configuração

### Variáveis de Ambiente

Copie `.env.example` para `.env` e ajuste conforme necessário:

```bash
cp .env.example .env
```

### Banco de Dados

O sistema usa SQLite por padrão. O banco é criado automaticamente na primeira execução.

**Localização no Docker**: `/app/data/trakeador.db` (volume persistente)

### Casas de Apostas Suportadas

1. **Esportiva**: `https://go.aff.esportiva.bet/`
2. **Novibet**: `https://novibet.com/br/`
3. **BetMGM**: `https://promo.betmgm.com/`

## 🚨 Produção

### Checklist de Deploy

- [ ] Configure HTTPS
- [ ] Use proxy reverso (nginx/traefik)
- [ ] Configure backup automático
- [ ] Configure monitoramento
- [ ] Ajuste variáveis de ambiente
- [ ] Configure logs centralizados

### Backup em Produção

Configure um cron job para backups automáticos:

```bash
# Exemplo: backup diário às 2h da manhã
0 2 * * * /caminho/para/backup.sh
```

## 📞 Suporte

Para suporte e questões:

1. Verifique os logs: `./manage-docker.sh logs`
2. Consulte a documentação Docker: `DOCKER.md`
3. Verifique o health check: `curl http://localhost:8080/health`

## �️ Controle de Versão

### Arquivos Ignorados

O projeto inclui `.gitignore` configurado para ignorar:

- **Arquivos de build**: `bin/`, `obj/`, `publish/`
- **Banco de dados**: `*.db`, `*.sqlite`, `backups/`
- **Configurações locais**: `.env`, `appsettings.Development.json`
- **IDEs**: `.vs/`, `.vscode/`, `.idea/`
- **Logs**: `*.log`, `Logs/`
- **Arquivos temporários**: `*.tmp`, `*.cache`

### Primeiro Commit

```bash
# Inicializar repositório
git init

# Adicionar arquivos
git add .

# Primeiro commit
git commit -m "Initial commit - TrakeadorWeb with Docker setup"
```

## �📄 Licença

Este projeto está sob licença [especificar licença].

---

**TrakeadorWeb** - Sistema completo para gestão de links de afiliados em apostas esportivas.