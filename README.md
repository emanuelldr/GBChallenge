# GBChallenge - Desafio Grupo Boticário
Desafio Grupo Boticário

## Stack 

- Dotnet Core 2.2
- EF Core
- SQLIte
- Swagger

## Requesitos

- .Net Core 2.2 

## Documentação

- Documentação das rotas realizada com Swagger, que você pode encontrar em: [Swagger.json](docs/Swagger_GrupoBoticario_Challenge.json)

## Execução do Projeto

- Para executar o Projeto:
    - Com o terminal, acesse o seguinte diretório: "~GBChallenge\src\GBChallenge.API"
    
    - Execute os seguintes comandos

    - dotnet build
    - dotnet run

## Disponibilização
- As seguintes portas serão disponibilizadas:
    - https://localhost:44330
    - http://localhost:64229


## Rotas 
- POST - /api/Revendedores => Adicionar/Registrar Revendedor
- POST - /api/Revendedores/autenticar => Autenticar/Validar Revendedor
- GET - /api/Revendedores/cashback => Retornar o Acumulado de CashBack para o Revendedor (API Segura, necessita JWT)
      
- POST /api/Compras => Adicionar nova Compra
- GET - /api/Compras => Listar compras do Revendedor
- DELETE - /api/Compras/{id} => Excluir Compra
- PATCH - /api/Compras/{id} => Atualizar Compra


