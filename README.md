# SafeGuard Pro — API de Controle de EPIs

> ⚠️ **Projeto acadêmico.** Desenvolvido durante meu curso técnico como trabalho de prática.

## Sobre

O **SafeGuard Pro** é uma API REST para **gerenciar a entrega e o controle de EPIs** (Equipamentos de Proteção Individual) em uma empresa. A ideia era resolver um problema real do dia a dia: saber **quem recebeu qual equipamento**, **quando** e **até quando ele é válido**, sem depender de planilha.

Ela cobre o ciclo básico:

- 👷 **Colaboradores:** cadastro dos funcionários (nome, CPF, CTPS, admissão, contato).
- 🦺 **EPIs:** os equipamentos e a forma correta de usar cada um.
- 📦 **Entregas:** o registro que liga um EPI a um colaborador, com data de entrega e de validade.
- 🔐 **Usuários e login:** autenticação com JWT e dois níveis de acesso (comum e administrador).

## Tecnologias

- **C# / ASP.NET Core** (Web API)
- **Entity Framework Core** para o acesso ao banco
- **PostgreSQL** como banco de dados
- **ASP.NET Identity + JWT** para autenticação e controle de acesso
- **Swagger / OpenAPI** para documentar e testar os endpoints

## O que eu tirei disso

Esse projeto me ensinou a pensar em **modelagem de dados**, **autenticação**, **organização de uma API** e a documentar o que eu construía. Olhando hoje, com mais experiência, eu faria várias coisas diferente.
