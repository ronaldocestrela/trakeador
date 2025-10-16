#!/bin/bash

# Script de gerenciamento do TrakeadorWeb Docker
# Uso: ./manage-docker.sh [command]

set -e

COMPOSE_FILE="docker-compose.yml"

show_help() {
    echo "TrakeadorWeb Docker Management Script"
    echo ""
    echo "Uso: $0 [command]"
    echo ""
    echo "Comandos disponíveis:"
    echo "  start     - Iniciar a aplicação"
    echo "  stop      - Parar a aplicação"
    echo "  restart   - Reiniciar a aplicação"
    echo "  rebuild   - Rebuild e reiniciar"
    echo "  logs      - Ver logs em tempo real"
    echo "  status    - Ver status dos containers"
    echo "  clean     - Parar e remover volumes (ATENÇÃO: apaga dados)"
    echo "  shell     - Acessar shell do container"
    echo "  health    - Verificar saúde da aplicação"
    echo "  backup    - Fazer backup do banco de dados"
    echo "  help      - Mostrar esta ajuda"
}

check_docker() {
    if ! command -v docker &> /dev/null; then
        echo "❌ Docker não encontrado. Instale o Docker primeiro."
        exit 1
    fi
    
    if ! command -v docker-compose &> /dev/null; then
        echo "❌ Docker Compose não encontrado. Instale o Docker Compose primeiro."
        exit 1
    fi
}

start_app() {
    echo "🚀 Iniciando TrakeadorWeb..."
    docker-compose up -d
    sleep 5
    check_health
}

stop_app() {
    echo "🛑 Parando TrakeadorWeb..."
    docker-compose down
}

restart_app() {
    echo "🔄 Reiniciando TrakeadorWeb..."
    docker-compose restart
    sleep 5
    check_health
}

rebuild_app() {
    echo "🔨 Rebuild e reiniciar TrakeadorWeb..."
    docker-compose down
    docker-compose up -d --build
    sleep 10
    check_health
}

show_logs() {
    echo "📋 Logs do TrakeadorWeb (Ctrl+C para sair)..."
    docker-compose logs -f trakeador-web
}

show_status() {
    echo "📊 Status dos containers:"
    docker-compose ps
    echo ""
    echo "💾 Volumes:"
    docker volume ls | grep trakeador
}

clean_all() {
    echo "⚠️  ATENÇÃO: Isso vai apagar TODOS os dados do banco!"
    read -p "Tem certeza? (digite 'sim' para confirmar): " confirm
    
    if [ "$confirm" = "sim" ]; then
        echo "🧹 Limpando tudo..."
        docker-compose down -v
        docker rmi trakeador-trakeador-web 2>/dev/null || true
        echo "✅ Limpeza concluída."
    else
        echo "❌ Operação cancelada."
    fi
}

access_shell() {
    echo "🐚 Acessando shell do container..."
    docker-compose exec trakeador-web /bin/bash
}

check_health() {
    echo "🏥 Verificando saúde da aplicação..."
    
    max_attempts=10
    attempt=1
    
    while [ $attempt -le $max_attempts ]; do
        if curl -f -s http://localhost:8080/health > /dev/null; then
            echo "✅ Aplicação está saudável!"
            echo "🌐 Acesse: http://localhost:8080"
            echo "👤 Admin: admin@trakeador.com / Admin@123"
            return 0
        fi
        
        echo "⏳ Tentativa $attempt/$max_attempts - Aguardando aplicação iniciar..."
        sleep 2
        attempt=$((attempt + 1))
    done
    
    echo "❌ Aplicação não está respondendo após $max_attempts tentativas"
    echo "📋 Verifique os logs: $0 logs"
    return 1
}

backup_database() {
    echo "💾 Fazendo backup do banco de dados..."
    
    backup_dir="./backups"
    mkdir -p "$backup_dir"
    
    timestamp=$(date +"%Y%m%d_%H%M%S")
    backup_file="$backup_dir/trakeador_backup_$timestamp.db"
    
    if docker-compose exec -T trakeador-web test -f /app/data/trakeador.db; then
        docker-compose exec -T trakeador-web cat /app/data/trakeador.db > "$backup_file"
        echo "✅ Backup salvo em: $backup_file"
    else
        echo "❌ Banco de dados não encontrado no container"
        return 1
    fi
}

# Main script logic
case "${1:-help}" in
    start)
        check_docker
        start_app
        ;;
    stop)
        check_docker
        stop_app
        ;;
    restart)
        check_docker
        restart_app
        ;;
    rebuild)
        check_docker
        rebuild_app
        ;;
    logs)
        check_docker
        show_logs
        ;;
    status)
        check_docker
        show_status
        ;;
    clean)
        check_docker
        clean_all
        ;;
    shell)
        check_docker
        access_shell
        ;;
    health)
        check_health
        ;;
    backup)
        check_docker
        backup_database
        ;;
    help|--help|-h)
        show_help
        ;;
    *)
        echo "❌ Comando desconhecido: $1"
        echo ""
        show_help
        exit 1
        ;;
esac