# Bot Trakeador

Bot de Telegram em Python para rastreamento de links de casas de apostas. Suporta **Novibet** e **BetMGM** com funcionalidades avançadas.

## Funcionalidades

O bot converte automaticamente links e cupons de casas de apostas em links com rastreamento aplicado.

### Exemplos de uso:

#### **Novibet (Links completos):**
**Entrada:** 
```
https://www.novibet.bet.br/sports/shared-bet/5db6d066ae07ae05329d5893911848a80560fc07e5968dce5fc16f758845d85b-0
```

**Saída:**
```
https://rt.novibet.partners/o/MVpiOM?lpage=jcBppl&site_id=1020436&redirect_url=https://www.novibet.bet.br/sports/shared-bet/5db6d066ae07ae05329d5893911848a80560fc07e5968dce5fc16f758845d85b-0
```

#### **BetMGM (Links completos):**
**Entrada:**
```
https://www.betmgm.bet.br/aposta-esportiva#featured?coupon=combination|3906784898,3906729211|30|replace
```

**Saída:**
```
https://ntrfr.betmgm.bet.br/redirect.aspx?pid=3393&bid=1519&redirectURL=https://www.betmgm.bet.br/aposta-esportiva#featured?coupon=combination|3906784898,3906729211|30|replace
```

#### **BetMGM (Apenas cupons - NOVO!):**
**Entrada:**
```
3906784898,3906729211
```
ou
```
3906784898
```

**Saída:**
```
https://ntrfr.betmgm.bet.br/redirect.aspx?pid=3393&bid=1519&redirectURL=https://www.betmgm.bet.br/aposta-esportiva#featured?coupon=combination|3906784898,3906729211|30|replace
```

## Instalação e Execução

### Método 1: Execução Direta (Python)

#### 1. Instalar dependências
```bash
pip install -r requirements.txt
```

#### 2. Executar o bot
```bash
python bot_trakeador.py
```

### Método 2: Execução com Docker (Recomendado)

#### 1. Build e execução automática
```bash
./deploy.sh deploy
```

#### 2. Ou usando Docker Compose diretamente
```bash
# Build da imagem
docker-compose build

# Iniciar o bot
docker-compose up -d

# Ver logs
docker-compose logs -f bot-trakeador

# Parar o bot
docker-compose down
```

#### 3. Script de Deploy Interativo
```bash
./deploy.sh
```

Este script oferece um menu interativo com opções para:
- Build da imagem Docker
- Iniciar/parar o bot
- Ver logs e status
- Reiniciar o bot
- Limpeza completa

## Exemplos Práticos de Uso

### 📱 Conversas com o Bot:

**Exemplo 1 - Novibet:**
```
👤 Usuário: https://www.novibet.bet.br/sports/shared-bet/abc123
🤖 Bot: ✅ Novibet - Link com rastreamento:
       https://rt.novibet.partners/o/MVpiOM?lpage=jcBppl&site_id=1020436&redirect_url=...
```

**Exemplo 2 - BetMGM (Link completo):**
```
👤 Usuário: https://www.betmgm.bet.br/aposta-esportiva#featured?coupon=combination|123,456|30|replace
🤖 Bot: ✅ BetMGM - Link com rastreamento:
       https://ntrfr.betmgm.bet.br/redirect.aspx?pid=3393&bid=1519&redirectURL=...
```

**Exemplo 3 - BetMGM (Cupom simples):**
```
👤 Usuário: 3906784898,3906729211
🤖 Bot: ✅ BetMGM Cupom - Link com rastreamento:
       https://ntrfr.betmgm.bet.br/redirect.aspx?pid=3393&bid=1519&redirectURL=...
```

**Exemplo 4 - BetMGM (Cupom único):**
```
👤 Usuário: 3906784898
🤖 Bot: ✅ BetMGM Cupom - Link com rastreamento:
       https://ntrfr.betmgm.bet.br/redirect.aspx?pid=3393&bid=1519&redirectURL=...
```

## Arquivos do Projeto

### Código Principal
- `bot_trakeador.py` - Arquivo principal do bot
- `requirements.txt` - Dependências Python necessárias
- `test_bot.py` - Script de testes para validação

### Docker
- `Dockerfile` - Configuração para containerização
- `docker-compose.yml` - Orquestração de containers
- `.dockerignore` - Arquivos ignorados no build Docker
- `deploy.sh` - Script automatizado de deploy

### Documentação
- `README.md` - Este arquivo de documentação
- `instructions.md` - Instruções originais do projeto
- `example_novibet.md` - Tutorial de como funciona o rastreamento

## Comandos do Bot

- `/start` ou `/help` - Exibe instruções de uso
- Envie qualquer link da **Novibet** ou **BetMGM** para obter o link com rastreamento
- Envie apenas **números de cupom da BetMGM** (ex: `3906784898,3906729211`) para gerar link automaticamente

## Configuração

O bot está configurado com:

### Novibet
- **Base de rastreamento:** `https://rt.novibet.partners/o/MVpiOM`
- **Parâmetros:** `lpage=jcBppl&site_id=1020436`

### BetMGM
- **Base de rastreamento:** `https://ntrfr.betmgm.bet.br/redirect.aspx`
- **Parâmetros:** `pid=3393&bid=1519`

### Bot Telegram
- **Token:** `8226215035:AAGX1-VVhg7aWaDy7xJCptwCDHFO4itp2MQ`

## Recursos

### Funcionalidades Gerais
✅ Conversão automática de links com rastreamento  
✅ Validação inteligente de URLs e cupons  
✅ Tratamento robusto de erros  
✅ Interface amigável com emojis  
✅ Suporte a múltiplos links por mensagem  
✅ Comandos de ajuda interativos  

### Novibet
✅ Links completos da Novibet (`novibet.bet.br`)  
✅ Detecção automática de URLs da Novibet  

### BetMGM
✅ Links completos da BetMGM (`betmgm.bet.br`)  
✅ **Cupons simplificados** - envie apenas números! ⭐  
✅ Suporte a cupons únicos (`3906784898`)  
✅ Suporte a cupons múltiplos (`3906784898,3906729211`)  
✅ Limpeza automática de espaços nos cupons  

## Tipos de Entrada Suportados

| Tipo | Exemplo de Entrada | Casa de Apostas |
|------|-------------------|-----------------|
| Link completo | `https://www.novibet.bet.br/sports/...` | Novibet |
| Link completo | `https://www.betmgm.bet.br/aposta-esportiva...` | BetMGM |
| Cupom único | `3906784898` | BetMGM |
| Cupom múltiplo | `3906784898,3906729211` | BetMGM |
| Cupom com espaços | `3906784898, 3906729211` | BetMGM |

## Testes

Para executar os testes automatizados:

```bash
# Com ambiente virtual ativo
python test_bot.py
```

Os testes incluem:
- ✅ Validação de links da Novibet
- ✅ Validação de links da BetMGM  
- ✅ Validação de cupons da BetMGM
- ✅ Conversão de links da Novibet
- ✅ Conversão de links da BetMGM
- ✅ Conversão de cupons da BetMGM

## Limitações

- Requer conexão com internet para funcionamento
- Cupons da BetMGM devem conter apenas números, vírgulas e espaços
- Links devem ser válidos e acessíveis