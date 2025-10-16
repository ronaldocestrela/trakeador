#!/bin/bash

# Script de backup para TrakeadorWeb
# Este script faz backup do banco de dados SQLite

CONTAINER_NAME="trakeador-web"
BACKUP_DIR="./backups"
DATE=$(date +%Y%m%d_%H%M%S)
BACKUP_FILE="trakeador_backup_${DATE}.db"

# Cores para output
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
NC='\033[0m' # No Color

echo -e "${YELLOW}=== TrakeadorWeb Backup Script ===${NC}"

# Criar diretório de backup se não existir
if [ ! -d "$BACKUP_DIR" ]; then
    mkdir -p "$BACKUP_DIR"
    echo -e "${GREEN}Diretório de backup criado: $BACKUP_DIR${NC}"
fi

# Verificar se o container está rodando
if ! docker ps | grep -q "$CONTAINER_NAME"; then
    echo -e "${RED}Erro: Container $CONTAINER_NAME não está rodando${NC}"
    exit 1
fi

echo -e "${YELLOW}Fazendo backup do banco de dados...${NC}"

# Fazer backup do banco de dados (cópia direta)
docker cp "$CONTAINER_NAME:/app/data/trakeador.db" "$BACKUP_DIR/$BACKUP_FILE"



if [ $? -eq 0 ]; then
    echo -e "${GREEN}Backup criado com sucesso: $BACKUP_DIR/$BACKUP_FILE${NC}"
    
    # Mostrar informações do backup
    BACKUP_SIZE=$(du -h "$BACKUP_DIR/$BACKUP_FILE" | cut -f1)
    echo -e "${GREEN}Tamanho do backup: $BACKUP_SIZE${NC}"
    
    # Listar backups existentes
    echo -e "${YELLOW}Backups existentes:${NC}"
    ls -lh "$BACKUP_DIR"/*.db 2>/dev/null | tail -5
    
else
    echo -e "${RED}Erro ao copiar backup do container${NC}"
    exit 1
fi

# Limpar backups antigos (manter apenas os 10 mais recentes)
BACKUP_COUNT=$(ls "$BACKUP_DIR"/trakeador_backup_*.db 2>/dev/null | wc -l)
if [ "$BACKUP_COUNT" -gt 10 ]; then
    echo -e "${YELLOW}Removendo backups antigos...${NC}"
    ls -t "$BACKUP_DIR"/trakeador_backup_*.db | tail -n +11 | xargs rm -f
    echo -e "${GREEN}Backups antigos removidos (mantendo os 10 mais recentes)${NC}"
fi

echo -e "${GREEN}Backup concluído!${NC}"