# language: pt
Funcionalidade: Relatórios e alertas ESG
  Para acompanhar indicadores ambientais e ocorrências operacionais
  Como gestor da aplicação
  Quero consultar relatórios consolidados e alertas registrados

  Cenário: Consultar relatório consolidado de coletas
    Dado que a API de gestão de resíduos está disponível
    E existe uma coleta registrada de material "Vidro" com peso 8.0
    Quando eu consulto o relatório consolidado
    Então o status code deve ser 200
    E a resposta deve conter os indicadores de coletas por material
    E o contrato JSON Schema "report-summary-response.schema.json" deve ser respeitado

  Cenário: Registrar e consultar alerta operacional
    Dado que a API de gestão de resíduos está disponível
    Quando eu registro um alerta com a mensagem "Container quase cheio"
    Então o status code deve ser 201
    Quando eu consulto a lista de alertas
    Então o status code deve ser 200
    E a resposta deve conter o alerta "Container quase cheio"
    E o contrato JSON Schema "alerts-response.schema.json" deve ser respeitado
