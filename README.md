# 📚 Clube da Leitura

Sistema de controle de empréstimos de revistas para amigos, organizado por caixas e com regras específicas para prazos, status e validações.

---

## 🧾 Descrição

Este projeto foi desenvolvido como parte do curso de **Análise e Desenvolvimento de Sistemas**, com foco em Programação Orientada a Objetos (POO) e boas práticas de desenvolvimento com C# em aplicações de console.

O sistema permite o **cadastro de amigos, caixas e revistas**, além do **registro de empréstimos**, respeitando diversas regras de negócio, como prazo de devolução calculado automaticamente e validações robustas.

---

## 🚀 Funcionalidades

✅ Cadastro, edição, visualização e exclusão de amigos  
✅ Cadastro de caixas com etiqueta única, cor e dias de empréstimo  
✅ Cadastro de revistas com status (disponível, emprestada, reservada)  
✅ Registro de empréstimos e devoluções  
✅ Cálculo automático da data de devolução  
✅ Validações completas nos dados (nome, telefone, edição, datas etc.)  
✅ Exibição dos empréstimos por amigo e status (aberto, concluído, atrasado)

---

## 🧱 Estrutura do Projeto

/ClubeDaLeitura
│
├── Dominio/ # Classes de negócio (Amigo, Caixa, Revista, Emprestimo)
├── Dados/ # Repositórios para persistência em memória
├── Apresentacao/ # Telas de interação com o usuário
├── Program.cs # Menu principal

---

## 🛠️ Tecnologias Utilizadas

- Linguagem: **C#**
- Plataforma: **.NET Console Application**
- Paradigma: **Programação Orientada a Objetos (POO)**
- IDE: **Visual Studio 2022**
- Versionamento: **Git e GitHub**

---
## 🎥 Demonstração

![Demonstração do módulo de Empréstimos](https://imgur.com/SBTSHb6.gif)

---

## 📚 Requisitos de Negócio Atendidos

- 📌 Amigo não pode ser excluído com empréstimos ativos
- 📌 Caixa não pode ser excluída com revistas vinculadas
- 📌 Revista não pode ter edição repetida com mesmo título
- 📌 Cada amigo só pode ter um empréstimo ativo
- 📌 Validação de telefone, nome e campos obrigatórios
- 📌 Empréstimos atrasados destacados visualmente

---
