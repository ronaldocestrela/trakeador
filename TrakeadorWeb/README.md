# TrakeadorWeb - Sistema de Links Rastreados

Sistema web desenvolvido em .NET 9 para gerenciar experts e gerar links rastreados para casas de apostas.

## 🚀 Tecnologias

- **.NET 9** - Framework principal
- **ASP.NET Core MVC** - Padrão arquitetural
- **Blazor Components** - Componentes interativos
- **Entity Framework Core** - ORM
- **SQLite** - Banco de dados
- **ASP.NET Core Identity** - Autenticação
- **Bootstrap 5** - Framework CSS
- **Font Awesome** - Ícones

## 📋 Funcionalidades

### 🎯 Gestão de Experts
- Listagem de experts cadastrados
- Criação, edição e exclusão de experts
- Visualização de detalhes com estatísticas
- **Gerenciamento de casas de apostas associadas**

### 🏠 Casas de Apostas Suportadas
1. **Esportiva.bet** - Transformação de links com parâmetros de afiliado
2. **Novibet** - Geração de links de redirecionamento
3. **BetMGM** - Criação de links com cupons de apostas

### 🏠 Gestão de Casas de Apostas
- **Associação Expert-Casa**: Configure quais casas cada expert pode usar
- **Códigos personalizados**: Defina códigos de afiliado específicos para cada expert
- **Parâmetros flexíveis**: Configure parâmetros adicionais por associação
- **Auto-sugestões**: Sistema sugere códigos baseados na casa selecionada

### 🔗 Geração de Links Rastreados
- Interface intuitiva para inserir links originais
- Processamento automático baseado na casa de apostas
- Cópia rápida do link gerado
- Validação de entrada e tratamento de erros
- **Links personalizados**: Cada expert usa seus próprios códigos de afiliado

## 🛠️ Como executar

### Pré-requisitos
- .NET 9 SDK
- Git

### Passos

1. **Clone o repositório**
```bash
git clone <url-do-repositorio>
cd trakeador/TrakeadorWeb
```

2. **Restaurar dependências**
```bash
dotnet restore
```

3. **Executar migrações (se necessário)**
```bash
dotnet ef database update
```

4. **Executar a aplicação**
```bash
dotnet run
```

5. **Acessar no navegador**
```
http://localhost:5234
```

## 📊 Estrutura do Banco de Dados

### Entidades principais:

- **Expert**: Representa um especialista em apostas
- **CasaDeApostas**: Casas de apostas disponíveis
- **ExpertCasaApostasAfiliado**: Relação entre expert e casa com códigos de afiliado

## 🔐 Autenticação e Segurança

O sistema utiliza ASP.NET Core Identity com:
- **Usuário Master**: Criado automaticamente no primeiro acesso
  - Email: `admin@trakeador.com`
  - Senha: `Admin@123`
- **Registro Privado**: Apenas usuários autenticados podem criar novos usuários
- **Acesso público**: Visualização de experts e geração de links (não requer login)
- **Acesso restrito**: Criação, edição e exclusão de experts + gerenciamento de usuários (requer autenticação)

## 🎨 Interface

- **Design responsivo** com Bootstrap 5
- **Ícones** Font Awesome
- **Cards** para melhor organização visual
- **Feedback visual** para ações do usuário
- **Alertas** informativos e de erro

## 📱 Exemplos de Uso

### Esportiva.bet
**Link original:**
```
https://go.aff.esportiva.bet/zyr47z0k?shareCode=TGI5FRERKSL
```

**Link rastreado:**
```
https://go.aff.esportiva.bet/zyr47z0k?afp=trafego&afp1=14_10_25&afp2=semana3out&afp6=superodd&shareCode=TGI5FRERKSL&afp9=SPODDBOTXFLAGP&home=1
```

### Novibet
**Link original:**
```
https://www.novibet.bet.br/sports/shared-bet/5db6d066ae07ae05329d5893911848a80560fc07e5968dce5fc16f758845d85b-0
```

**Link rastreado:**
```
https://rt.novibet.partners/o/MVpiOM?lpage=jcBppl&site_id=1020436&redirect_url=https%3A%2F%2Fwww.novibet.bet.br%2Fsports%2Fshared-bet%2F5db6d066ae07ae05329d5893911848a80560fc07e5968dce5fc16f758845d85b-0
```

### BetMGM
**Link original:**
```
3906784898,3906729211
```

**Link rastreado:**
```
https://ntrfr.betmgm.bet.br/redirect.aspx?pid=3393&bid=1519&redirectURL=https://www.betmgm.bet.br/aposta-esportiva#featured?coupon=combination|3906784898,3906729211|30|replace
```

## 🏗️ Arquitetura

```
TrakeadorWeb/
├── Controllers/           # Controladores MVC
├── Data/                 # Contexto do EF e Seeder
├── Models/               # Entidades do domínio
├── Services/             # Serviços de negócio
├── ViewModels/           # ViewModels para as Views
├── Views/                # Views Razor
├── wwwroot/              # Arquivos estáticos
└── Program.cs            # Configuração da aplicação
```

## 🎮 Fluxo de Uso

**👁️ Para usuários (sem login):**
1. Acessar a aplicação
2. Ver lista de experts na página inicial
3. Clicar em "Links" em um expert
4. Escolher uma casa de apostas
5. Colar o link original
6. Clicar em "Gerar Link Rastreado"
7. Copiar o link processado

**👤 Para administradores (com login):**
- Criar, editar e excluir experts
- **Associar casas de apostas aos experts**
- **Configurar códigos de afiliado personalizados**
- **Gerenciar parâmetros específicos por casa/expert**
- Gerenciar usuários do sistema
- Criar novos usuários administrativos
- Redefinir senhas de usuários
- Todas as funcionalidades de usuário comum

**🔑 Primeiro acesso:**
1. Acesse o sistema
2. Clique em "Login"
3. Use as credenciais master:
   - Email: `admin@trakeador.com`
   - Senha: `Admin@123`
4. **Altere a senha master imediatamente por segurança**

**🔧 Configuração de Casas de Apostas (Administradores):**
1. Fazer login como administrador
2. Na lista de experts, clicar em "Casas" ou "Gerenciar Casas de Apostas"
3. Clicar em "Associar Casa de Apostas"
4. Selecionar a casa de apostas desejada
5. Inserir o código de afiliado específico do expert
6. Configurar parâmetros adicionais (se necessário)
7. Salvar a associação

> 💡 **Dica**: O sistema oferece sugestões automáticas de códigos baseados na casa selecionada

## 📝 Dados de Exemplo

O sistema é populado automaticamente com:

### Experts de exemplo:
- **João Silva** - Especialista em futebol brasileiro
- **Maria Santos** - Analista de mercados internacionais

### Casas de Apostas disponíveis:
- **Esportiva** - Casa de apostas esportivas brasileira
- **Novibet** - Plataforma internacional de apostas
- **BetMGM** - Casa de apostas com sistema de cupons

> ⚠️ **Importante**: As associações entre experts e casas devem ser configuradas manualmente pelo administrador

## 🔧 Personalização

Para adicionar novas casas de apostas:

1. Adicione a casa no seeder (`Data/DbSeeder.cs`)
2. Implemente a lógica no serviço (`Services/LinkTrackingService.cs`)
3. Atualize o controlador para reconhecer a nova casa

## �️ Segurança

### Credenciais Padrão
- **Email**: `admin@trakeador.com`
- **Senha**: `Admin@123`

⚠️ **IMPORTANTE**: Altere a senha padrão imediatamente após o primeiro acesso!

### Políticas Implementadas
- ✅ Registro público desabilitado
- ✅ Apenas usuários autenticados podem criar novos usuários  
- ✅ Senhas com política de segurança (maiúscula, minúscula, número, 6+ caracteres)
- ✅ Proteção contra exclusão do último usuário
- ✅ Middleware de bloqueio de registro automático

### Gerenciamento de Usuários
- **Criar usuários**: Menu "Usuários" → "Novo Usuário"
- **Redefinir senhas**: Botão "🔑" na lista de usuários
- **Excluir usuários**: Botão "🗑️" na lista de usuários
- **Proteção**: Usuário master não pode ser excluído se for o único

## �📞 Suporte

Para dúvidas ou problemas:
- Verifique se todas as dependências estão instaladas
- Confirme se o .NET 9 está corretamente configurado
- Verifique se o banco de dados foi criado corretamente
- **Esqueceu a senha master?** Delete o arquivo `trakeador.db` e execute novamente

---

*Desenvolvido com ❤️ em .NET 9*