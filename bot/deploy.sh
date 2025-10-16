#!/bin/bash

# Script de Deploy do Bot Trakeador
# Este script facilita o build e execução do bot usando Docker

set -e

echo "🚀 Bot Trakeador - Script de Deploy"
echo "=================================="

# Cores para output
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
NC='\033[0m' # No Color

# Função para exibir mensagens coloridas
print_status() {
    echo -e "${GREEN}[INFO]${NC} $1"
}

print_warning() {
    echo -e "${YELLOW}[WARNING]${NC} $1"
}

print_error() {
    echo -e "${RED}[ERROR]${NC} $1"
}

# Verificar se Docker está instalado
check_docker() {
    if ! command -v docker &> /dev/null; then
        print_error "Docker não está instalado. Por favor, instale o Docker primeiro."
        exit 1
    fi

    if ! command -v docker-compose &> /dev/null; then
        print_error "Docker Compose não está instalado. Por favor, instale o Docker Compose primeiro."
        exit 1
    fi

    print_status "Docker e Docker Compose encontrados ✅"
}

# Função para build da imagem
build_image() {
    print_status "Construindo imagem Docker do Bot Trakeador..."
    docker-compose build --no-cache
    print_status "Imagem construída com sucesso ✅"
}

# Função para iniciar o bot
start_bot() {
    print_status "Iniciando Bot Trakeador..."
    docker-compose up -d
    print_status "Bot iniciado em background ✅"
    
    # Aguardar alguns segundos e verificar status
    sleep 5
    check_status
}

# Função para parar o bot
stop_bot() {
    print_status "Parando Bot Trakeador..."
    docker-compose down
    print_status "Bot parado ✅"
}

# Função para verificar status
check_status() {
    print_status "Verificando status do bot..."
    docker-compose ps
    
    # Verificar logs recentes
    print_status "Logs recentes:"
    docker-compose logs --tail=10 bot-trakeador
}

# Função para mostrar logs
show_logs() {
    print_status "Exibindo logs do bot (Ctrl+C para sair)..."
    docker-compose logs -f bot-trakeador
}

# Função para restart
restart_bot() {
    print_status "Reiniciando Bot Trakeador..."
    docker-compose restart
    sleep 5
    check_status
}

# Função para limpeza
cleanup() {
    print_warning "Removendo containers e imagens..."
    docker-compose down --rmi all --volumes --remove-orphans
    print_status "Limpeza concluída ✅"
}

# Menu principal
show_menu() {
    echo ""
    echo "Escolha uma opção:"
    echo "1) Build da imagem"
    echo "2) Iniciar bot"
    echo "3) Parar bot"
    echo "4) Reiniciar bot"
    echo "5) Ver status"
    echo "6) Ver logs"
    echo "7) Limpeza completa"
    echo "8) Deploy completo (build + start)"
    echo "9) Sair"
    echo ""
}

# Função main
main() {
    check_docker

    if [ $# -eq 0 ]; then
        # Modo interativo
        while true; do
            show_menu
            read -p "Digite sua opção [1-9]: " choice
            
            case $choice in
                1)
                    build_image
                    ;;
                2)
                    start_bot
                    ;;
                3)
                    stop_bot
                    ;;
                4)
                    restart_bot
                    ;;
                5)
                    check_status
                    ;;
                6)
                    show_logs
                    ;;
                7)
                    cleanup
                    ;;
                8)
                    build_image
                    start_bot
                    ;;
                9)
                    print_status "Saindo..."
                    exit 0
                    ;;
                *)
                    print_error "Opção inválida!"
                    ;;
            esac
        done
    else
        # Modo comando
        case $1 in
            build)
                build_image
                ;;
            start)
                start_bot
                ;;
            stop)
                stop_bot
                ;;
            restart)
                restart_bot
                ;;
            status)
                check_status
                ;;
            logs)
                show_logs
                ;;
            cleanup)
                cleanup
                ;;
            deploy)
                build_image
                start_bot
                ;;
            *)
                echo "Uso: $0 [build|start|stop|restart|status|logs|cleanup|deploy]"
                exit 1
                ;;
        esac
    fi
}

# Executar função main
main "$@"