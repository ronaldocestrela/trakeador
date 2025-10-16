#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""
Bot Trakeador - Bot para rastreamento de links de casas de apostas
Suporta: Novibet e BetMGM
"""

import telebot
import re
import urllib.parse
from typing import Optional

# Token do bot fornecido
BOT_TOKEN = "8226215035:AAGX1-VVhg7aWaDy7xJCptwCDHFO4itp2MQ"

# Configurações para Novibet
NOVIBET_TRACKING_BASE = "https://rt.novibet.partners/o/MVpiOM"
NOVIBET_PARAMS = {
    "lpage": "jcBppl",
    "site_id": "1020436"
}

# Configurações para BetMGM
BETMGM_TRACKING_BASE = "https://ntrfr.betmgm.bet.br/redirect.aspx"
BETMGM_PARAMS = {
    "pid": "3393",
    "bid": "1519"
}

# Inicializar o bot
bot = telebot.TeleBot(BOT_TOKEN)

def is_novibet_link(url: str) -> bool:
    """
    Verifica se o link é da Novibet
    """
    return "novibet.bet.br" in url

def is_betmgm_link(url: str) -> bool:
    """
    Verifica se o link é da BetMGM
    """
    return "betmgm.bet.br" in url

def is_betmgm_coupon(text: str) -> bool:
    """
    Verifica se o texto é um cupom da BetMGM (apenas números e vírgulas)
    """
    # Remove espaços em branco
    text = text.strip()
    
    # Verificar se contém apenas números, vírgulas e espaços
    if not text:
        return False
    
    # Pattern para números separados por vírgula (com espaços opcionais)
    import re
    pattern = r'^[\d\s,]+$'
    return bool(re.match(pattern, text)) and any(c.isdigit() for c in text)

def convert_novibet_to_tracking_link(original_url: str) -> str:
    """
    Converte um link da Novibet para um link com rastreamento
    
    Args:
        original_url: URL original da Novibet
        
    Returns:
        URL com rastreamento aplicado
    """
    # Construir parâmetros da URL de rastreamento
    params = NOVIBET_PARAMS.copy()
    params["redirect_url"] = original_url
    
    # Construir a URL final
    query_string = urllib.parse.urlencode(params)
    tracking_url = f"{NOVIBET_TRACKING_BASE}?{query_string}"
    
    return tracking_url

def convert_betmgm_to_tracking_link(original_url: str) -> str:
    """
    Converte um link da BetMGM para um link com rastreamento
    
    Args:
        original_url: URL original da BetMGM
        
    Returns:
        URL com rastreamento aplicado
    """
    # Construir parâmetros da URL de rastreamento
    params = BETMGM_PARAMS.copy()
    params["redirectURL"] = original_url
    
    # Construir a URL final
    query_string = urllib.parse.urlencode(params)
    tracking_url = f"{BETMGM_TRACKING_BASE}?{query_string}"
    
    return tracking_url

def convert_betmgm_coupon_to_tracking_link(coupon_numbers: str) -> str:
    """
    Converte números de cupom da BetMGM em um link com rastreamento
    
    Args:
        coupon_numbers: Números do cupom (ex: "3906784898,3906729211" ou "3906784898")
        
    Returns:
        URL com rastreamento aplicado
    """
    # Limpar e formatar os números
    numbers = coupon_numbers.replace(" ", "").strip()
    
    # Construir a URL original da BetMGM
    betmgm_url = f"https://www.betmgm.bet.br/aposta-esportiva#featured?coupon=combination|{numbers}|30|replace"
    
    # Converter para link com rastreamento
    return convert_betmgm_to_tracking_link(betmgm_url)

@bot.message_handler(commands=['start', 'help'])
def send_welcome(message):
    """
    Handler para comandos /start e /help
    """
    welcome_text = """
🎯 *Bot Trakeador*

Olá! Eu sou o bot para rastreamento de links de casas de apostas.

📝 *Como usar:*
• Envie um link da Novibet ou BetMGM
• Ou envie números de cupom da BetMGM
• Receba o link com rastreamento aplicado

🔗 *Exemplos:*

**Novibet:**
Você envia: `https://www.novibet.bet.br/sports/shared-bet/...`
Eu retorno: `https://rt.novibet.partners/o/MVpiOM?...`

**BetMGM - Link completo:**
Você envia: `https://www.betmgm.bet.br/aposta-esportiva#featured?coupon=...`
Eu retorno: `https://ntrfr.betmgm.bet.br/redirect.aspx?...`

**BetMGM - Apenas cupom:**
Você envia: `3906784898,3906729211` ou `3906784898`
Eu retorno: `https://ntrfr.betmgm.bet.br/redirect.aspx?...`

Envie um link ou cupom para começar! 🚀
    """
    bot.reply_to(message, welcome_text, parse_mode='Markdown')

@bot.message_handler(func=lambda message: True)
def handle_message(message):
    """
    Handler principal para todas as mensagens
    """
    try:
        text = message.text.strip()
        
        # Verificar primeiro se é um cupom da BetMGM (apenas números)
        if is_betmgm_coupon(text):
            try:
                tracking_url = convert_betmgm_coupon_to_tracking_link(text)
                response = f"✅ **BetMGM Cupom** - Link com rastreamento:\n`{tracking_url}`"
                bot.reply_to(message, response, parse_mode='Markdown')
                return
            except Exception as e:
                bot.reply_to(message, f"❌ Erro ao processar cupom BetMGM: {str(e)}")
                return
        
        # Verificar se a mensagem contém URLs
        url_pattern = r'https?://[^\s]+'
        urls = re.findall(url_pattern, text)
        
        if not urls:
            bot.reply_to(message, "❌ Por favor, envie:\n• Um link da Novibet ou BetMGM\n• Números do cupom BetMGM (ex: 3906784898,3906729211)")
            return
        
        processed_links = []
        
        for url in urls:
            try:
                if is_novibet_link(url):
                    tracking_url = convert_novibet_to_tracking_link(url)
                    processed_links.append(f"✅ **Novibet** - Link com rastreamento:\n`{tracking_url}`")
                elif is_betmgm_link(url):
                    tracking_url = convert_betmgm_to_tracking_link(url)
                    processed_links.append(f"✅ **BetMGM** - Link com rastreamento:\n`{tracking_url}`")
                else:
                    processed_links.append(f"❌ Link não suportado: {url}\n(Apenas links da Novibet e BetMGM são aceitos)")
            except Exception as e:
                processed_links.append(f"❌ Erro ao processar link: {url}\nErro: {str(e)}")
        
        if processed_links:
            response = "\n\n".join(processed_links)
            bot.reply_to(message, response, parse_mode='Markdown')
        else:
            bot.reply_to(message, "❌ Não foi possível processar nenhum link.")
            
    except Exception as e:
        print(f"❌ Erro no handler de mensagem: {e}")
        bot.reply_to(message, "❌ Ocorreu um erro interno. Tente novamente.")

def main():
    """
    Função principal do bot
    """
    print("🚀 Bot Trakeador iniciado...")
    print(f"📋 Bot configurado para: {bot.get_me().username}")
    
    try:
        # Iniciar o bot
        bot.infinity_polling(timeout=10, long_polling_timeout=5)
    except Exception as e:
        print(f"❌ Erro ao executar o bot: {e}")

if __name__ == "__main__":
    main()