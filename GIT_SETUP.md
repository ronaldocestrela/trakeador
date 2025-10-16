# Git Setup para TrakeadorWeb

## Inicialização do Repositório

```bash
# No diretório raiz do projeto
cd /home/ronaldo/EasyCompany/trakeador

# Inicializar repositório Git
git init

# Configurar informações do usuário (se necessário)
git config user.name "Seu Nome"
git config user.email "seu.email@exemplo.com"

# Adicionar todos os arquivos
git add .

# Verificar o que será commitado
git status

# Primeiro commit
git commit -m "feat: initial TrakeadorWeb setup with Docker configuration"
```

## Estrutura dos Commits

### Convenção de Commits

Use a convenção de commits semânticos:

```
feat: nova funcionalidade
fix: correção de bug  
docs: alterações na documentação
style: formatação, ponto e vírgula, etc
refactor: refatoração de código
test: adição de testes
chore: tarefas de manutenção
```

### Exemplos de Commits

```bash
git commit -m "feat: add Docker configuration with health checks"
git commit -m "docs: update README with Docker setup instructions"
git commit -m "fix: correct database migration issues in Docker"
git commit -m "chore: add backup script for production deployment"
```

## Arquivos no Controle de Versão

### ✅ Incluídos
- Código fonte da aplicação
- Configurações Docker (`Dockerfile`, `docker-compose.yml`)
- Scripts de gerenciamento (`manage-docker.sh`, `backup.sh`)
- Documentação (`README.md`, `DOCKER.md`)
- Configurações de exemplo (`.env.example`)

### ❌ Ignorados (.gitignore)
- Arquivos de build (`bin/`, `obj/`)
- Banco de dados (`*.db`, `backups/`)
- Configurações locais (`.env`)
- Logs e arquivos temporários
- Arquivos específicos de IDEs

## Branches Sugeridas

```bash
# Branch principal
main (ou master)

# Branch de desenvolvimento  
develop

# Features
feature/nome-da-funcionalidade

# Correções
fix/nome-da-correcao

# Releases
release/v1.0.0
```

## Comandos Úteis

```bash
# Ver status dos arquivos
git status

# Ver diferenças
git diff

# Adicionar arquivos específicos
git add arquivo.cs

# Commit com mensagem
git commit -m "mensagem do commit"

# Ver histórico
git log --oneline

# Criar nova branch
git checkout -b feature/nova-funcionalidade

# Mudar de branch
git checkout main

# Merge de branches
git merge feature/nova-funcionalidade
```

## Ignorar Arquivos Existentes

Se algum arquivo que deveria ser ignorado já está no repositório:

```bash
# Remover do controle de versão mas manter no disco
git rm --cached arquivo.db

# Remover diretório do controle de versão
git rm -r --cached bin/

# Commit a remoção
git commit -m "chore: remove ignored files from version control"
```

## Configuração para Produção

Para deploy em produção, considere usar:

```bash
# Tags para releases
git tag -a v1.0.0 -m "Release version 1.0.0"
git push origin v1.0.0

# Branch de produção separada
git checkout -b production
```

## Hooks Úteis (Opcional)

Criar arquivo `.git/hooks/pre-commit`:

```bash
#!/bin/sh
# Verificar se há arquivos .db sendo commitados
if git diff --cached --name-only | grep -q "\.db$"; then
    echo "Erro: Tentativa de commitar arquivo de banco de dados!"
    echo "Verifique o .gitignore e remova arquivos .db do commit."
    exit 1
fi
```