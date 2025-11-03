# TrakeadorWeb - Sistema Completo de Gestão e Rastreamento de Links Afiliados

Sistema web completo para gestão de experts em apostas esportivas, casas de apostas, geração e rastreamento de links afiliados, com interface administrativa e bot Telegram para processamento rápido.

## 🚀 Características Principais

- **Gestão Completa de Experts**: Cadastro, edição, ativação/desativação e visualização de especialistas em apostas
- **Gestão de Casas de Apostas**: Administração de múltiplas casas de apostas com URLs base e status
- **Sistema de Canais e Destinos**: Organização hierárquica de canais e destinos para categorização de links
- **Associações Expert-Casa**: Configuração de códigos de afiliado personalizados por combinação expert-casa
- **Geração de Links Rastreados**: Transformação automática de links simples em links com códigos de afiliado e parâmetros de rastreamento
- **Casas de Apostas Suportadas**: Esportiva, Novibet, BetMGM, Betsson, BateuBet
- **Bot Telegram**: Processamento rápido de links via Telegram para Novibet e BetMGM
- **Sistema de Usuários**: Autenticação e autorização com ASP.NET Identity, gestão administrativa de usuários
- **Interface Responsiva**: Bootstrap 5 com design moderno e intuitivo
- **Docker Ready**: Containerização completa para deploy fácil e escalável
- **Sistema de Backup**: Scripts automatizados para backup do banco de dados
- **Health Checks**: Monitoramento de saúde da aplicação

## 🏗️ Tecnologias Utilizadas

- **.NET 9**: Framework principal para desenvolvimento backend
- **ASP.NET Core MVC**: Arquitetura web robusta com padrão Model-View-Controller
- **Blazor Components**: Componentes interativos para interface dinâmica
- **Entity Framework Core**: ORM avançado para mapeamento objeto-relacional
- **SQLite**: Banco de dados leve e eficiente para desenvolvimento e produção
- **ASP.NET Identity**: Sistema completo de autenticação e autorização
- **Python 3**: Linguagem para desenvolvimento do bot Telegram
- **Telebot Library**: Framework para criação de bots no Telegram
- **Bootstrap 5**: Framework CSS responsivo e moderno
- **Docker & Docker Compose**: Containerização e orquestração de serviços
- **Bash Scripts**: Automação de tarefas de gerenciamento e backup

## 📦 Executar com Docker (Recomendado)

### Pré-requisitos
- Docker Engine 20.10+
- Docker Compose 2.0+

### Início Rápido

```bash
# Clonar o repositório
git clone <repo-url>
cd trakeador

# Executar a aplicação completa
./manage-docker.sh start

# Ou usando docker-compose diretamente
docker-compose up -d
```

A aplicação estará disponível em: **http://localhost:8080**

### Credenciais Padrão
- **Usuário**: admin@trakeador.com
- **Senha**: Admin@123

## 🛠️ Scripts de Gerenciamento

##### Script Principal (`manage-docker.sh`)
```bash
# Iniciar aplicação
./manage-docker.sh start

# Parar aplicação
./manage-docker.sh stop

# Ver logs em tempo real
./manage-docker.sh logs

# Verificar status dos serviços
./manage-docker.sh status

# Reiniciar aplicação
./manage-docker.sh restart

# Ver todas as opções disponíveis
./manage-docker.sh help
```

### Backup Automatizado
```bash
# Executar backup manual
./backup.sh

# Backups são salvos em ./backups/
# Arquivos nomeados com timestamp: backup_YYYYMMDD_HHMMSS.db
```

## 🏃‍♂️ Executar Localmente (Desenvolvimento)

### Pré-requisitos
- .NET 9 SDK
- Python 3.8+
- SQLite3

### Configuração da Aplicação Web

```bash
cd TrakeadorWeb

# Restaurar dependências
dotnet restore

# Executar migrações do banco
dotnet ef database update

# Executar aplicação em modo desenvolvimento
dotnet run
```

### Configuração do Bot Telegram

```bash
cd bot

# Instalar dependências
pip install -r requirements.txt

# Executar bot
python bot_trakeador.py
```

## 📋 Funcionalidades Detalhadas

### 1. Gestão de Experts
- **CRUD Completo**: Criar, listar, visualizar, editar e desativar experts
- **Ativação/Desativação**: Controle de status ativo/inativo
- **Histórico**: Registro automático de data de criação
- **Associações**: Visualização de casas de apostas vinculadas

### 2. Gestão de Casas de Apostas
- **CRUD Completo**: Administração completa das casas suportadas
- **Configuração de URLs**: Definição de URLs base para cada casa
- **Status de Atividade**: Controle de casas ativas/inativas
- **Integração com Experts**: Relacionamento muitos-para-muitos

### 3. Sistema de Canais e Destinos
- **Canais**: Categorias principais para organização (ex: Instagram, Twitter, YouTube)
- **Destinos**: Subcategorias dentro de canais para segmentação específica
- **Hierarquia**: Estrutura organizada canal → destinos
- **Integração com Links**: Parâmetros de rastreamento baseados em canal/destino

### 4. Associações Expert-Casa de Apostas
- **Códigos Afiliados**: Configuração personalizada por combinação expert-casa
- **Parâmetros Adicionais**: Configurações específicas por relacionamento
- **Status de Atividade**: Controle granular de associações ativas
- **Histórico**: Registro de data de criação das associações

### 5. Geração de Links Rastreados
- **Interface Pública**: Formulário acessível para geração de links
- **Seleção Dinâmica**: Escolha de expert, casa e canal/destino
- **Processamento Automático**: Transformação baseada na casa de apostas
- **Parâmetros de Rastreamento**:
  - `afp`: Canal de origem
  - `afp1`: Data formatada (DD_MM_YY)
  - `afp2`: Semana do ano (semana{N}mes)
  - `afp6`: Destino específico
  - `afp9`: Detalhes adicionais
  - `home`: Indicador de página inicial

### 6. Suporte a Múltiplas Casas de Apostas

#### Esportiva (`go.aff.esportiva.bet/{codigo}`)
- Integração com sistema de share codes
- Parâmetros de rastreamento avançados
- Suporte a cupons e apostas compartilhadas

#### Novibet (`rt.novibet.partners`)
- Redirecionamento com tracking
- Suporte a múltiplos tipos de link
- Parâmetros personalizados

#### BetMGM (`ntrfr.betmgm.bet.br`)
- Sistema de cupons numéricos
- Conversão automática de números para URLs
- Tracking de combinações

#### Betsson
- Integração completa com plataforma
- Parâmetros de afiliado customizados

#### BateuBet
- Suporte a novos formatos de link
- Rastreamento avançado

### 7. Bot Telegram (@bot_trakeador)
- **Processamento Rápido**: Conversão instantânea de links
- **Suporte a Cupons**: Entrada direta de números BetMGM
- **Casas Suportadas**: Novibet e BetMGM
- **Interface Intuitiva**: Comandos /start e /help
- **Respostas Automáticas**: Formatação clara dos links rastreados

### 8. Sistema de Usuários e Segurança
- **Autenticação ASP.NET Identity**: Sistema robusto de login/logout
- **Gestão Administrativa**: CRUD de usuários pelo admin
- **Controle de Acesso**: Autorização baseada em roles
- **Registro Desabilitado**: Controle público desabilitado por middleware
- **Reset de Senhas**: Funcionalidade administrativa

### 9. Monitoramento e Health Checks
- **Endpoints de Saúde**: `/health` para verificação de status
- **Logs Estruturados**: Sistema de logging integrado
- **Monitoramento Docker**: Health checks nos containers

## 🗂️ Estrutura do Projeto

```
trakeador/
├── TrakeadorWeb/              # Aplicação web principal
│   ├── Controllers/           # Controladores MVC
│   │   ├── ExpertsController.cs
│   │   ├── CasasDeApostasController.cs
│   │   ├── CanaisController.cs
│   │   ├── DestinosController.cs
│   │   ├── ExpertCasaApostasController.cs
│   │   ├── LinkTrackingController.cs
│   │   ├── UserManagementController.cs
│   │   └── HomeController.cs
│   ├── Data/                  # Contexto e migrações EF Core
│   ├── Models/                # Modelos de domínio
│   │   ├── Expert.cs
│   │   ├── CasaDeApostas.cs
│   │   ├── Canal.cs
│   │   ├── Destino.cs
│   │   └── ExpertCasaApostasAfiliado.cs
│   ├── Services/              # Lógica de negócio
│   │   └── LinkTrackingService.cs
│   ├── ViewModels/            # ViewModels para forms
│   ├── Views/                 # Views Razor
│   ├── wwwroot/               # Arquivos estáticos
│   └── appsettings.json       # Configurações
├── bot/                       # Bot Telegram
│   ├── bot_trakeador.py       # Código principal do bot
│   ├── requirements.txt       # Dependências Python
│   └── Dockerfile             # Container do bot
├── docker-compose.yml         # Orquestração completa
├── manage-docker.sh           # Script de gerenciamento
├── backup.sh                  # Script de backup
├── DOCKER.md                  # Documentação Docker detalhada
└── README.md                  # Este arquivo
```

## 🔧 Configuração Avançada

### Variáveis de Ambiente

Crie um arquivo `.env` na raiz do projeto:

```bash
# Banco de dados
DATABASE_PATH=/app/data/trakeador.db

# Configurações da aplicação
ASPNETCORE_ENVIRONMENT=Production
ASPNETCORE_URLS=http://+:8080

# Configurações de segurança
ADMIN_EMAIL=admin@trakeador.com
ADMIN_PASSWORD=Admin@123
```

### Banco de Dados

- **Desenvolvimento**: SQLite local (`trakeador.db`)
- **Produção**: SQLite em volume Docker persistente (`/app/data/trakeador.db`)
- **Migrações**: Automáticas na inicialização

### Configuração de Produção

#### Checklist de Deploy
- [ ] Configurar HTTPS com certificado válido
- [ ] Usar proxy reverso (nginx/traefik/Caddy)
- [ ] Configurar backup automático via cron
- [ ] Implementar monitoramento (Prometheus/Grafana)
- [ ] Configurar logs centralizados
- [ ] Ajustar variáveis de ambiente de produção
- [ ] Configurar firewall e segurança de rede

#### Backup em Produção
```bash
# Exemplo de cron job para backup diário às 2h
0 2 * * * /caminho/para/backup.sh

# Ou usando Docker
0 2 * * * docker exec trakeador-web /app/backup.sh
```

## 🚨 Segurança e Boas Práticas

### Autenticação
- Senhas fortes obrigatórias (mínimo 6 caracteres)
- Confirmação de email desabilitada para simplicidade
- Middleware para desabilitar registro público

### Autorização
- Controle de acesso baseado em `[Authorize]`
- Gestão administrativa de usuários
- Validação anti-forgery em formulários

### Dados Sensíveis
- Códigos de afiliado armazenados de forma segura
- Logs não contêm informações sensíveis
- Backup criptografado (recomendado)

## 📞 Suporte e Troubleshooting

### Verificação de Saúde
```bash
# Health check da aplicação
curl http://localhost:8080/health

# Status dos containers
./manage-docker.sh status

# Logs detalhados
./manage-docker.sh logs
```

### Problemas Comuns

1. **Erro de Conexão DB**: Verificar permissões do volume Docker
2. **Bot Não Responde**: Verificar token do Telegram e conectividade
3. **Links Não Processam**: Verificar códigos de afiliado ativos
4. **Erro de Migração**: Executar `dotnet ef database update` manualmente

### Logs e Debug
- **Aplicação Web**: Logs disponíveis via `./manage-docker.sh logs web`
- **Bot Telegram**: Logs no console ou arquivo de log
- **Banco de Dados**: Verificar integridade do arquivo SQLite

## 🔄 Controle de Versão

### Arquivos Ignorados
```
# Build e binários
bin/
obj/
publish/

# Banco de dados e dados
*.db
*.sqlite
backups/
*.log

# Configurações locais
.env
appsettings.Development.json

# IDEs
.vscode/
.idea/
*.swp

# OS
.DS_Store
Thumbs.db
```

### Fluxo de Desenvolvimento
```bash
# Desenvolvimento local
git checkout -b feature/nova-funcionalidade
# ... desenvolvimento ...
git add .
git commit -m "feat: adicionar nova funcionalidade"
git push origin feature/nova-funcionalidade

# Merge via Pull Request
# Deploy automático via Docker
```

## 📈 Roadmap e Melhorias Futuras

- [ ] **Dashboard Analytics**: Métricas de conversão e performance
- [ ] **API REST**: Endpoints para integração externa
- [ ] **Multi-idioma**: Suporte a português e inglês
- [ ] **Notificações**: Alertas para links expirados
- [ ] **Testes Automatizados**: Cobertura completa com xUnit
- [ ] **CI/CD**: Pipeline GitHub Actions
- [ ] **Cache Redis**: Performance para dados frequentes
- [ ] **Microserviços**: Separação bot e web app

## �️ Licença

Este projeto é proprietário e confidencial.

---

**TrakeadorWeb** - Sistema completo para gestão profissional de links afiliados em apostas esportivas, combinando interface web robusta com automação via Telegram.

## 🚀 Características Principais

- **Gestão Completa de Experts**: Cadastro, edição, ativação/desativação e visualização de especialistas em apostas
- **Gestão de Casas de Apostas**: Administração de múltiplas casas de apostas com URLs base e status
- **Sistema de Canais e Destinos**: Organização hierárquica de canais e destinos para categorização de links
- **Associações Expert-Casa**: Configuração de códigos de afiliado personalizados por combinação expert-casa
- **Geração de Links Rastreados**: Transformação automática de links simples em links com códigos de afiliado e parâmetros de rastreamento
- **Casas de Apostas Suportadas**: Esportiva, Novibet, BetMGM, Betsson, BateuBet
- **Bot Telegram**: Processamento rápido de links via Telegram para Novibet e BetMGM
- **Sistema de Usuários**: Autenticação e autorização com ASP.NET Identity, gestão administrativa de usuários
- **Interface Responsiva**: Bootstrap 5 com design moderno e intuitivo
- **Docker Ready**: Containerização completa para deploy fácil e escalável
- **Sistema de Backup**: Scripts automatizados para backup do banco de dados
- **Health Checks**: Monitoramento de saúde da aplicação

## 🏗️ Tecnologias Utilizadas

- **.NET 9**: Framework principal para desenvolvimento backend
- **ASP.NET Core MVC**: Arquitetura web robusta com padrão Model-View-Controller
- **Blazor Components**: Componentes interativos para interface dinâmica
- **Entity Framework Core**: ORM avançado para mapeamento objeto-relacional
- **SQLite**: Banco de dados leve e eficiente para desenvolvimento e produção
- **ASP.NET Identity**: Sistema completo de autenticação e autorização
- **Python 3**: Linguagem para desenvolvimento do bot Telegram
- **Telebot Library**: Framework para criação de bots no Telegram
- **Bootstrap 5**: Framework CSS responsivo e moderno
- **Docker & Docker Compose**: Containerização e orquestração de serviços
- **Bash Scripts**: Automação de tarefas de gerenciamento e backup

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
- Python 3.8+
- SQLite3

### Configuração da Aplicação Web

```bash
cd TrakeadorWeb

# Restaurar dependências
dotnet restore

# Executar migrações do banco
dotnet ef database update

# Executar aplicação em modo desenvolvimento
dotnet run
```

### Configuração do Bot Telegram

```bash
cd bot

# Instalar dependências
pip install -r requirements.txt

# Executar bot
python bot_trakeador.py
```

## 📋 Funcionalidades Detalhadas

### 1. Gestão de Experts
- **CRUD Completo**: Criar, listar, visualizar, editar e desativar experts
- **Ativação/Desativação**: Controle de status ativo/inativo
- **Histórico**: Registro automático de data de criação
- **Associações**: Visualização de casas de apostas vinculadas

### 2. Gestão de Casas de Apostas
- **CRUD Completo**: Administração completa das casas suportadas
- **Configuração de URLs**: Definição de URLs base para cada casa
- **Status de Atividade**: Controle de casas ativas/inativas
- **Integração com Experts**: Relacionamento muitos-para-muitos

### 3. Sistema de Canais e Destinos
- **Canais**: Categorias principais para organização (ex: Instagram, Twitter, YouTube)
- **Destinos**: Subcategorias dentro de canais para segmentação específica
- **Hierarquia**: Estrutura organizada canal → destinos
- **Integração com Links**: Parâmetros de rastreamento baseados em canal/destino

### 4. Associações Expert-Casa de Apostas
- **Códigos Afiliados**: Configuração personalizada por combinação expert-casa
- **Parâmetros Adicionais**: Configurações específicas por relacionamento
- **Status de Atividade**: Controle granular de associações ativas
- **Histórico**: Registro de data de criação das associações

### 5. Geração de Links Rastreados
- **Interface Pública**: Formulário acessível para geração de links
- **Seleção Dinâmica**: Escolha de expert, casa e canal/destino
- **Processamento Automático**: Transformação baseada na casa de apostas
- **Parâmetros de Rastreamento**:
  - `afp`: Canal de origem
  - `afp1`: Data formatada (DD_MM_YY)
  - `afp2`: Semana do ano (semana{N}mes)
  - `afp6`: Destino específico
  - `afp9`: Detalhes adicionais
  - `home`: Indicador de página inicial

### 6. Suporte a Múltiplas Casas de Apostas

#### Esportiva (`go.aff.esportiva.bet/{codigo}`)
- Integração com sistema de share codes
- Parâmetros de rastreamento avançados
- Suporte a cupons e apostas compartilhadas

#### Novibet (`rt.novibet.partners`)
- Redirecionamento com tracking
- Suporte a múltiplos tipos de link
- Parâmetros personalizados

#### BetMGM (`ntrfr.betmgm.bet.br`)
- Sistema de cupons numéricos
- Conversão automática de números para URLs
- Tracking de combinações

#### Betsson
- Integração completa com plataforma
- Parâmetros de afiliado customizados

#### BateuBet
- Suporte a novos formatos de link
- Rastreamento avançado

### 7. Bot Telegram (@bot_trakeador)
- **Processamento Rápido**: Conversão instantânea de links
- **Suporte a Cupons**: Entrada direta de números BetMGM
- **Casas Suportadas**: Novibet e BetMGM
- **Interface Intuitiva**: Comandos /start e /help
- **Respostas Automáticas**: Formatação clara dos links rastreados

### 8. Sistema de Usuários e Segurança
- **Autenticação ASP.NET Identity**: Sistema robusto de login/logout
- **Gestão Administrativa**: CRUD de usuários pelo admin
- **Controle de Acesso**: Autorização baseada em roles
- **Registro Desabilitado**: Controle público desabilitado por middleware
- **Reset de Senhas**: Funcionalidade administrativa

### 9. Monitoramento e Health Checks
- **Endpoints de Saúde**: `/health` para verificação de status
- **Logs Estruturados**: Sistema de logging integrado
- **Monitoramento Docker**: Health checks nos containers

## 🗂️ Estrutura do Projeto

```
trakeador/
├── TrakeadorWeb/              # Aplicação web principal
│   ├── Controllers/           # Controladores MVC
│   │   ├── ExpertsController.cs
│   │   ├── CasasDeApostasController.cs
│   │   ├── CanaisController.cs
│   │   ├── DestinosController.cs
│   │   ├── ExpertCasaApostasController.cs
│   │   ├── LinkTrackingController.cs
│   │   ├── UserManagementController.cs
│   │   └── HomeController.cs
│   ├── Data/                  # Contexto e migrações EF Core
│   ├── Models/                # Modelos de domínio
│   │   ├── Expert.cs
│   │   ├── CasaDeApostas.cs
│   │   ├── Canal.cs
│   │   ├── Destino.cs
│   │   └── ExpertCasaApostasAfiliado.cs
│   ├── Services/              # Lógica de negócio
│   │   └── LinkTrackingService.cs
│   ├── ViewModels/            # ViewModels para forms
│   ├── Views/                 # Views Razor
│   ├── wwwroot/               # Arquivos estáticos
│   └── appsettings.json       # Configurações
├── bot/                       # Bot Telegram
│   ├── bot_trakeador.py       # Código principal do bot
│   ├── requirements.txt       # Dependências Python
│   └── Dockerfile             # Container do bot
├── docker-compose.yml         # Orquestração completa
├── manage-docker.sh           # Script de gerenciamento
├── backup.sh                  # Script de backup
├── DOCKER.md                  # Documentação Docker detalhada
└── README.md                  # Este arquivo
```

## 🔧 Configuração Avançada

### Variáveis de Ambiente

Crie um arquivo `.env` na raiz do projeto:

```bash
# Banco de dados
DATABASE_PATH=/app/data/trakeador.db

# Configurações da aplicação
ASPNETCORE_ENVIRONMENT=Production
ASPNETCORE_URLS=http://+:8080

# Configurações de segurança
ADMIN_EMAIL=admin@trakeador.com
ADMIN_PASSWORD=Admin@123
```

### Banco de Dados

- **Desenvolvimento**: SQLite local (`trakeador.db`)
- **Produção**: SQLite em volume Docker persistente (`/app/data/trakeador.db`)
- **Migrações**: Automáticas na inicialização

### Configuração de Produção

#### Checklist de Deploy
- [ ] Configurar HTTPS com certificado válido
- [ ] Usar proxy reverso (nginx/traefik/Caddy)
- [ ] Configurar backup automático via cron
- [ ] Implementar monitoramento (Prometheus/Grafana)
- [ ] Configurar logs centralizados
- [ ] Ajustar variáveis de ambiente de produção
- [ ] Configurar firewall e segurança de rede

#### Backup em Produção
```bash
# Exemplo de cron job para backup diário às 2h
0 2 * * * /caminho/para/backup.sh

# Ou usando Docker
0 2 * * * docker exec trakeador-web /app/backup.sh
```

## � Segurança e Boas Práticas

### Autenticação
- Senhas fortes obrigatórias (mínimo 6 caracteres)
- Confirmação de email desabilitada para simplicidade
- Middleware para desabilitar registro público

### Autorização
- Controle de acesso baseado em `[Authorize]`
- Gestão administrativa de usuários
- Validação anti-forgery em formulários

### Dados Sensíveis
- Códigos de afiliado armazenados de forma segura
- Logs não contêm informações sensíveis
- Backup criptografado (recomendado)

## 📞 Suporte e Troubleshooting

### Verificação de Saúde
```bash
# Health check da aplicação
curl http://localhost:8080/health

# Status dos containers
./manage-docker.sh status

# Logs detalhados
./manage-docker.sh logs
```

### Problemas Comuns

1. **Erro de Conexão DB**: Verificar permissões do volume Docker
2. **Bot Não Responde**: Verificar token do Telegram e conectividade
3. **Links Não Processam**: Verificar códigos de afiliado ativos
4. **Erro de Migração**: Executar `dotnet ef database update` manualmente

### Logs e Debug
- **Aplicação Web**: Logs disponíveis via `./manage-docker.sh logs web`
- **Bot Telegram**: Logs no console ou arquivo de log
- **Banco de Dados**: Verificar integridade do arquivo SQLite

## 🔄 Controle de Versão

### Arquivos Ignorados
```
# Build e binários
bin/
obj/
publish/

# Banco de dados e dados
*.db
*.sqlite
backups/
*.log

# Configurações locais
.env
appsettings.Development.json

# IDEs
.vscode/
.idea/
*.swp

# OS
.DS_Store
Thumbs.db
```

### Fluxo de Desenvolvimento
```bash
# Desenvolvimento local
git checkout -b feature/nova-funcionalidade
# ... desenvolvimento ...
git add .
git commit -m "feat: adicionar nova funcionalidade"
git push origin feature/nova-funcionalidade

# Merge via Pull Request
# Deploy automático via Docker
```

## 📈 Roadmap e Melhorias Futuras

- [ ] **Dashboard Analytics**: Métricas de conversão e performance
- [ ] **API REST**: Endpoints para integração externa
- [ ] **Multi-idioma**: Suporte a português e inglês
- [ ] **Notificações**: Alertas para links expirados
- [ ] **Testes Automatizados**: Cobertura completa com xUnit
- [ ] **CI/CD**: Pipeline GitHub Actions
- [ ] **Cache Redis**: Performance para dados frequentes
- [ ] **Microserviços**: Separação bot e web app

## �️ Licença

Este projeto é proprietário e confidencial.

---

**TrakeadorWeb** - Sistema completo para gestão profissional de links afiliados em apostas esportivas, combinando interface web robusta com automação via Telegram.