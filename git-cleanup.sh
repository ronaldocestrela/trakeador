#!/bin/bash

# Script para limpar arquivos que devem ser ignorados pelo Git
# Execute este script antes do primeiro commit

echo "🧹 Limpando arquivos que devem ser ignorados pelo Git..."

# Diretório raiz do projeto
PROJECT_ROOT="/home/ronaldo/EasyCompany/trakeador"
cd "$PROJECT_ROOT"

# Contador de arquivos removidos
REMOVED_COUNT=0

# Função para remover arquivo/diretório se existir
remove_if_exists() {
    local path="$1"
    local description="$2"
    
    if [ -e "$path" ]; then
        echo "  Removendo $description: $path"
        rm -rf "$path"
        REMOVED_COUNT=$((REMOVED_COUNT + 1))
    fi
}

echo "📁 Verificando TrakeadorWeb/..."

# Arquivos de build do .NET
remove_if_exists "TrakeadorWeb/bin" "diretório bin"
remove_if_exists "TrakeadorWeb/obj" "diretório obj"

# Arquivos de banco de dados
remove_if_exists "TrakeadorWeb/trakeador.db" "banco SQLite principal"
remove_if_exists "TrakeadorWeb/trakeador.db-shm" "arquivo SQLite shared memory"
remove_if_exists "TrakeadorWeb/trakeador.db-wal" "arquivo SQLite write-ahead log"

# Logs
remove_if_exists "TrakeadorWeb/Logs" "diretório de logs"
find TrakeadorWeb -name "*.log" -type f -delete 2>/dev/null

# Arquivos temporários
find TrakeadorWeb -name "*.tmp" -type f -delete 2>/dev/null
find TrakeadorWeb -name "*.cache" -type f -delete 2>/dev/null

# Visual Studio / VS Code
remove_if_exists "TrakeadorWeb/.vs" "cache do Visual Studio"
remove_if_exists ".vs" "cache do Visual Studio (raiz)"

echo "📁 Verificando diretório raiz..."

# Backups (manter estrutura mas limpar conteúdo)
if [ -d "backups" ]; then
    echo "  Limpando backups existentes..."
    rm -f backups/*.db 2>/dev/null
    rm -f backups/*.backup 2>/dev/null
fi

# Arquivo .env se existir (manter apenas .env.example)
remove_if_exists ".env" "arquivo de ambiente local"

# Logs na raiz
remove_if_exists "*.log" "arquivos de log"

echo ""
if [ $REMOVED_COUNT -eq 0 ]; then
    echo "✅ Nenhum arquivo para limpar foi encontrado!"
else
    echo "✅ Limpeza concluída! $REMOVED_COUNT itens removidos."
fi

echo ""
echo "📋 Próximos passos:"
echo "1. Execute: git status"
echo "2. Se estiver tudo correto: git add ."
echo "3. Faça o primeiro commit: git commit -m 'Initial commit - TrakeadorWeb setup'"

echo ""
echo "ℹ️  Arquivos ignorados pelo .gitignore não aparecerão no git status"