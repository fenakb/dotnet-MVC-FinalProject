## Mercado Natural


#### Introdução

Projecto final realizado no âmbito da academia de desenvolvimento de Software Rumos, sendo o objetivo desenvolver uma web app de uma loja de supermercado contemplando a base de dados, modelo MVC com autenticação e WebAPI.

#### 1. Diagrama de classes

![Diagrama de classes](diagrama.png?raw=true "Diagrama de classes")

#### 2.	Descrição sumária da abordagem utilizada e dos padrões de programação escolhidos.

Gerar uma base de dados com uma library de classes que utiliza Entity Framework a partir do método code first com Data Annotations para indicar nome de tabela, id’s-chave, chaves estrangeiras e nome de display das colunas. 

Após, foram gerados um projeto WebAPI e um projeto MVC com autenticação que possuem acesso a base de dados. Com as classes da library foram gerados os Controllers para ambos projetos e as Views do projeto MVC. 

Por fim, foram feitos ajustes nos Controllers do projeto MVC para ligar a Autenticação com os empregados e, assim, gerar as permissões.

#### 3.	Guia de utilização 

Para criar um novo empregado é preciso registar um novo usuário. O empregado necessita de fazer o Login para poder criar ou editar produtos. Se não estiver logado, a pessoa não poderá inserir nem editar produtos.


##### Lista de Produtos antes do login:
![Lista de Produtos sem Login](produtos_sem_login.png?raw=true "Lista de Produtos sem Login")

##### Lista de Produtos depois do login:
![Lista de Produtos com Login](produtos_com_login.png?raw=true "Lista de Produtos com Login")

Um funcionário só poderá gerir os produtos que ele mesmo inseriu.  

Os dados podem ser acessados através de um web service. Para utilizar este serviço é necessário digitar portal / api / controllerapi. Como, por exemplo, para acessar a lista de empregados o pedido será: http://localhost:51330/api/empregadosapi.

##### Acesso à lista de funcionários através de WebService:

![Lista de funcionários através de WebService](webservice.png?raw=true "Lista de funcionários através de WebService")