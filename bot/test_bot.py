#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""
Teste para o Bot Trakeador
"""

import sys
import os

# Adicionar o diretório atual ao path para importar o bot
sys.path.append(os.path.dirname(os.path.abspath(__file__)))

from bot_trakeador import (
    is_novibet_link, 
    is_betmgm_link,
    is_betmgm_coupon,
    convert_novibet_to_tracking_link, 
    convert_betmgm_to_tracking_link,
    convert_betmgm_coupon_to_tracking_link
)

def test_novibet_link_validation():
    """Testa a validação de links da Novibet"""
    print("🧪 Testando validação de links da Novibet...")
    
    # Links válidos da Novibet
    valid_links = [
        "https://www.novibet.bet.br/sports/shared-bet/5db6d066ae07ae05329d5893911848a80560fc07e5968dce5fc16f758845d85b-0",
        "https://novibet.bet.br/sports/football",
        "http://www.novibet.bet.br/casino"
    ]
    
    # Links inválidos (que não são Novibet)
    invalid_links = [
        "https://www.bet365.com/sports",
        "https://www.google.com",
        "https://github.com/user/repo",
        "https://www.betmgm.bet.br/aposta-esportiva"  # Este é BetMGM, não Novibet
    ]
    
    # Testar links válidos
    for link in valid_links:
        result = is_novibet_link(link)
        print(f"✅ Novibet {link} -> {result}")
        assert result == True, f"Link da Novibet não foi reconhecido: {link}"
    
    # Testar links inválidos
    for link in invalid_links:
        result = is_novibet_link(link)
        print(f"❌ Não-Novibet {link} -> {result}")
        assert result == False, f"Link não-Novibet foi incorretamente aceito: {link}"
    
    print("✅ Validação de links da Novibet funcionando corretamente!\n")

def test_betmgm_link_validation():
    """Testa a validação de links da BetMGM"""
    print("🧪 Testando validação de links da BetMGM...")
    
    # Links válidos da BetMGM
    valid_links = [
        "https://www.betmgm.bet.br/aposta-esportiva#featured?coupon=combination|3906784898,3906729211|30|replace",
        "https://betmgm.bet.br/sports/football",
        "http://www.betmgm.bet.br/casino"
    ]
    
    # Links inválidos (que não são BetMGM)
    invalid_links = [
        "https://www.bet365.com/sports",
        "https://www.google.com",
        "https://github.com/user/repo",
        "https://www.novibet.bet.br/sports"  # Este é Novibet, não BetMGM
    ]
    
    # Testar links válidos
    for link in valid_links:
        result = is_betmgm_link(link)
        print(f"✅ BetMGM {link} -> {result}")
        assert result == True, f"Link da BetMGM não foi reconhecido: {link}"
    
    # Testar links inválidos
    for link in invalid_links:
        result = is_betmgm_link(link)
        print(f"❌ Não-BetMGM {link} -> {result}")
        assert result == False, f"Link não-BetMGM foi incorretamente aceito: {link}"
    
    print("✅ Validação de links da BetMGM funcionando corretamente!\n")

def test_novibet_link_conversion():
    """Testa a conversão de links da Novibet"""
    print("🧪 Testando conversão de links da Novibet...")
    
    original_link = "https://www.novibet.bet.br/sports/shared-bet/5db6d066ae07ae05329d5893911848a80560fc07e5968dce5fc16f758845d85b-0"
    expected_base = "https://rt.novibet.partners/o/MVpiOM"
    
    converted_link = convert_novibet_to_tracking_link(original_link)
    
    print(f"🔗 Link Novibet original: {original_link}")
    print(f"🔗 Link Novibet convertido: {converted_link}")
    
    # Verificar se a base está correta
    assert converted_link.startswith(expected_base), f"Base do link Novibet incorreta"
    
    # Verificar se o link original está como redirect_url (URL encoded)
    import urllib.parse
    encoded_url = urllib.parse.quote(original_link, safe='')
    assert f"redirect_url={encoded_url}" in converted_link, "Link original não encontrado como redirect_url"
    
    # Verificar se os parâmetros estão presentes
    assert "lpage=jcBppl" in converted_link, "Parâmetro lpage não encontrado"
    assert "site_id=1020436" in converted_link, "Parâmetro site_id não encontrado"
    
    print("✅ Conversão de links da Novibet funcionando corretamente!\n")

def test_betmgm_link_conversion():
    """Testa a conversão de links da BetMGM"""
    print("🧪 Testando conversão de links da BetMGM...")
    
    original_link = "https://www.betmgm.bet.br/aposta-esportiva#featured?coupon=combination|3906784898,3906729211|30|replace"
    expected_base = "https://ntrfr.betmgm.bet.br/redirect.aspx"
    
    converted_link = convert_betmgm_to_tracking_link(original_link)
    
    print(f"🔗 Link BetMGM original: {original_link}")
    print(f"🔗 Link BetMGM convertido: {converted_link}")
    
    # Verificar se a base está correta
    assert converted_link.startswith(expected_base), f"Base do link BetMGM incorreta"
    
    # Verificar se o link original está como redirectURL (URL encoded)
    import urllib.parse
    encoded_url = urllib.parse.quote(original_link, safe='')
    assert f"redirectURL={encoded_url}" in converted_link, "Link original não encontrado como redirectURL"
    
    # Verificar se os parâmetros estão presentes
    assert "pid=3393" in converted_link, "Parâmetro pid não encontrado"
    assert "bid=1519" in converted_link, "Parâmetro bid não encontrado"
    
    print("✅ Conversão de links da BetMGM funcionando corretamente!\n")

def test_betmgm_coupon_validation():
    """Testa a validação de cupons da BetMGM"""
    print("🧪 Testando validação de cupons da BetMGM...")
    
    # Cupons válidos
    valid_coupons = [
        "3906784898,3906729211",
        "3906784898",
        "123456789,987654321,555444333",
        "3906784898, 3906729211",  # com espaços
        " 123456789 ",  # com espaços nas bordas
    ]
    
    # Textos inválidos (que não são cupons)
    invalid_coupons = [
        "https://www.google.com",
        "abc123,456def",
        "texto normal",
        "",
        "abc",
        "123abc456",
        "3906784898,abc,3906729211"
    ]
    
    # Testar cupons válidos
    for coupon in valid_coupons:
        result = is_betmgm_coupon(coupon)
        print(f"✅ Cupom válido '{coupon}' -> {result}")
        assert result == True, f"Cupom válido não foi reconhecido: {coupon}"
    
    # Testar textos inválidos
    for coupon in invalid_coupons:
        result = is_betmgm_coupon(coupon)
        print(f"❌ Texto inválido '{coupon}' -> {result}")
        assert result == False, f"Texto inválido foi aceito como cupom: {coupon}"
    
    print("✅ Validação de cupons da BetMGM funcionando corretamente!\n")

def test_betmgm_coupon_conversion():
    """Testa a conversão de cupons da BetMGM"""
    print("🧪 Testando conversão de cupons da BetMGM...")
    
    # Teste cupom múltiplo
    coupon1 = "3906784898,3906729211"
    converted1 = convert_betmgm_coupon_to_tracking_link(coupon1)
    
    print(f"🔗 Cupom múltiplo: {coupon1}")
    print(f"🔗 Link convertido: {converted1}")
    
    # Verificações para cupom múltiplo
    assert "ntrfr.betmgm.bet.br/redirect.aspx" in converted1, "Base do link incorreta"
    assert "pid=3393" in converted1, "Parâmetro pid não encontrado"
    assert "bid=1519" in converted1, "Parâmetro bid não encontrado"
    assert "combination%7C3906784898%2C3906729211" in converted1, "Números do cupom não encontrados"
    
    # Teste cupom único
    coupon2 = "3906784898"
    converted2 = convert_betmgm_coupon_to_tracking_link(coupon2)
    
    print(f"🔗 Cupom único: {coupon2}")
    print(f"🔗 Link convertido: {converted2}")
    
    # Verificações para cupom único
    assert "ntrfr.betmgm.bet.br/redirect.aspx" in converted2, "Base do link incorreta"
    assert "combination%7C3906784898" in converted2, "Número do cupom não encontrado"
    
    print("✅ Conversão de cupons da BetMGM funcionando corretamente!\n")

def main():
    """Executa todos os testes"""
    print("🚀 Iniciando testes do Bot Trakeador...\n")
    
    try:
        test_novibet_link_validation()
        test_betmgm_link_validation()
        test_betmgm_coupon_validation()
        test_novibet_link_conversion()
        test_betmgm_link_conversion()
        test_betmgm_coupon_conversion()
        
        print("🎉 Todos os testes passaram! O bot está pronto para uso com Novibet e BetMGM (links e cupons).")
        
    except AssertionError as e:
        print(f"❌ Teste falhou: {e}")
        sys.exit(1)
    except Exception as e:
        print(f"❌ Erro durante os testes: {e}")
        sys.exit(1)

if __name__ == "__main__":
    main()