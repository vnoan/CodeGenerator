# Desafio - Gerador de códigos em arquivo

A empresa **StnX** solicitou um projeto que gere códigos com ***7 caracteres*** para utilização em campanhas de marketing. 
O algoritmo deve gerar um arquivo com um número de linhas especificado pelo usuário, contendo esses códigos gerados e precisa seguir algumas regras:

- cada linha precisa ter 7 caracteres exatos;
- esses caracteres precisam ser aleatórios e não pode se repetir na mesma linha;

    `ABCDEFG é válido.`

    `AABCDEF não é válido.`
- esses caracteres só podem ser letras;
- números e caracteres especiais não devem entrar no arquivo;
- caso aconteça de duplicar algum código, não tem problema;
- O programa deve ser capaz de gerar 10 mil códigos;

O programa que você recebeu atende todos esses requisitos, mas a empresa **StnX** precisa fazer uma campanha com um volume muito grande agora, e precisa de 1 milhão de códigos.

Nosso programa atual não está conseguindo gerar esse volume de códigos dentro de um tempo aceitável e dentro dos recursos de infra que temos.

Se você executar nosso programa com mais de 10 mil linhas, verá que grande parte dos recursos da máquina onde está sendo executado é utilizado.

> ## Precisamos da sua ajuda para resolver esse problema.

Refatore esse programa para que seja possível gerar um arquivo com 1 milhão de códigos de forma mais performática que conseguir.

No fluxo atual, o projeto não consegue gerar nem 100 mil códigos e quando consegue gerar um número maior que 10 mil, leva muito tempo, o que impacta nosso o cliente.

### Benchmark
| Linhas | Tempo   |
|  ----  | -----   |
|10      |18ms     |
|100     |21ms     |
|1000    |51ms     |
|10000   |671ms    |
|100000  | inviável|
|1000000 | inviável|
|10000000| inviável|

<br>


## Desafios adicionais

- _Quando você conseguir fazer rodar para um arquivo com **1 milhão de códigos**, que tal tentar fazer ele rodar para **10 milhões**?!_ ;)
- _Na versão atual do algoritmo, um mesmo código pode ser gerado em duplicidade. Consegue fazer com que a lista final de códigos só contenha códigos únicos, ou seja, sem repetição?_

<br>

## E aí, podemos contar com você?!
<br><br>

---

## Como Rodar

Basta rodar o comando `dotnet run` dentro da pasta que se encontra este README, e informar a quantidade de linhas.

<br>

---

*Disclaimer*

Esse código está bem abaixo dos padrões de qualidade que temos nos nossos times e gostaríamos que você melhorasse esse código, dado que ele nem executa direito para um número de linhas muito grande.

O algoritmo utilizado pode ser alterado completamente, desde que o resultado final esteja dentro do que nosso cliente solicitou.
Isso inclui a remoção de mensagens e logs eventuais.
O importante é termos o arquivo gerado no final conforme os requisitos.

Nós vamos avaliar seu conhecimento em algoritmos, C#, orientação a objetos, princípios SOLID, técnicas de escrita em arquivo, testes, resiliência e design de código (pode separar seu código em mais classes, caso faça sentido na sua refatoração).


O comitê avaliador costuma avaliar os projetos considerando o checklist abaixo.
Atente-se aos tópicos para garantir uma melhor entrega. Boa sorte! :)

1. Código está rodando e resolve o problema proposto?
2. Resolveu os desafios adicionais?
3. Código executa dentro da performance esperada?
    1. Consumo de memória é elevado?
    2. Algoritmo utilizado apresenta uma performance aceitável?
    3. Resolveu o problema com força bruta ou alguma outra técnica de algoritmos?
    4. A escrita do arquivo é feita de modo performática?
    5. A solução preserva o GC?
4. Escreveu testes unitários?
    1. Os testes garantem, de fato, a qualidade do código apresentado?
    2. Os testes estão escritos dentro dos padrões utilizados no mercado?
5. Dividiu bem as responsabilidades entre as classes e métodos?
    1. Existe violação do SOLID?
    2. Nível de abstração e conhecimentos sobre POO
    3. Tratamento de erros
    4. Logs
6. A nomenclatura de classes, métodos e variáveis estão de acordo com as melhores práticas?
7. Está atualizado com os conceitos de C#?





