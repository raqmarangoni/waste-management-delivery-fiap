# language: pt
Funcionalidade: Gestão de coletas de resíduos
  Para apoiar a rastreabilidade ambiental e a governança ESG
  Como usuário da API de gestão de resíduos
  Quero registrar e consultar coletas realizadas

  Cenário: Consultar coletas com paginação
    Dado que a API de gestão de resíduos está disponível
    Quando eu consulto a lista paginada de coletas
    Então o status code deve ser 200
    E a resposta deve conter a lista paginada de coletas
    E o contrato JSON Schema "collections-paged-response.schema.json" deve ser respeitado

  Cenário: Registrar coleta válida de material reciclável
    Dado que a API de gestão de resíduos está disponível
    Quando eu envio uma coleta válida de material "Plástico" com peso 12.5
    Então o status code deve ser 201
    E a coleta deve ser recuperada pelo identificador gerado
    E o contrato JSON Schema "collection-response.schema.json" deve ser respeitado

  Cenário: Consultar coleta inexistente
    Dado que a API de gestão de resíduos está disponível
    Quando eu consulto a coleta de identificador 999999
    Então o status code deve ser 404
