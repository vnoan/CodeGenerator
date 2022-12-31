O programa está feito de uma forma que é possível customizar certos parâmetros de acordo com a necessidade:
- O campo *Uniqueness* indica se o programa deverá ou não se preocupar com unicidade dos códigos gerados.
- O campo *BlockSize* indica a quantidade máxima de códigos que podem ficar armazenados em memória durante a execução. Quanto maior o número, melhor será a performance do sistema, porém mais memória será consumida.
- O campo *CodeLength* indica o tamanho dos códigos gerados.

Toda essa personalização tem o objetivo de permitir alterações ao programa sem alterações no código-fonte. Por estar no appsettings, é possível escolher um BlockSize diferente para cada ambiente, por exemplo.

### Comentários técnicos
Para ter uma performance melhor, optei por usar o paralelismo do C# atrávels do Parallel. Para isso, foi necessario utilizar uma ConcurrentBag e um ConcurrentDictionary onde normalmente se utilizaria uma List e um Dictionary. Listas e dicionários não são thread safe e por isso não é recomendado o uso, já que não se garante a integridade. 
Vocês vão perceber que existem momentos em que não foi utilizado o paralelimos. Isso se dá porque ConcurrentBag não possui um comportamento adequado quando se trata de remoção de dados. Quando o programa remove algum código, ele precisa lidar com uma lista o que impossibilita o uso de paralelimos.

Como o processo pode ser bem demorado em alguns casos, existem logs no Handler que informam ao usuário quantos códigos faltam, o comprimento dos códigos e se o programa está removendo duplicidades.

A opção de ter um handler foi para remover o máximo possível da lógica de dentro da Main, permitindo que fosse possível usar este código em uma API ou em uma estrutura serverless.

O consumo de memória é bem baixo, por volta de 100 a 200 MB e isso se dá por todo gerenciamento feito. 
OBS:Durante o desenvolvimento, fiz uma versão salvando tudo em memória e o resultado para 10 milhões de códigos foi um consumo de quase 5 GB. 

Embora não tenha teste unitário, o programa possui a abstração através de interfaces que facilitam a adição.

### Como rodar
Da mesma forma que o outro.

O programa enviado está com a configuração de unicidade desabilitada. Para alterar isso basta alterar o campo *Uniqueness* no appsettings.

Vocês vão perceber que existe um código comentado na Main do projeto. Isso foi **intencional** já que esse código facilita a averiguação dos benchmarks. É um código simples que não estaria ali em outra situação. 

### Benchmark - Código Original
| Linhas | Tempo   |
|  ----  | -----   |
|10      |18ms     |
|100     |21ms     |
|1000    |51ms     |
|10000   |671ms    |
|100000  | inviável|
|1000000 | inviável|
|10000000| inviável|

### Benchmark - Sem Unicidade
BlockSize de 1 milhão

| Linhas | Tempo   |
|  ----  | -----   |
|10      |3ms      |
|100     |6ms      |
|1000    |6ms      |
|10000   |40ms     |
|100000  |379ms    |
|1000000 |2680ms   |
|10000000|26073ms  |

### Benchmark - Com Unicidade
BlockSize de 1 milhão

| Linhas | Tempo   |
|  ----  | -----   |
|10      |36ms     |
|100     |172ms    |
|1000    |169ms    |
|10000   |867ms    |
|100000  |6282ms   |
|1000000 |69852ms  |
|10000000|inviável*|

* considerei inviável por questão do tempo. O programa é capaz de gerar mais 10.000.000 de códigos, porém demoraria muito tempo.




