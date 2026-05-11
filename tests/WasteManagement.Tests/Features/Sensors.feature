# language: pt
Funcionalidade: Telemetria de sensores IoT
  Para otimizar rotas e reduzir desperdício operacional
  Como sistema integrado de sensores
  Quero enviar o nível de preenchimento dos coletores

  Cenário: Receber telemetria dentro do limite operacional
    Dado que a API de gestão de resíduos está disponível
    Quando eu envio uma telemetria do container "C-001" com nível 50
    Então o status code deve ser 200
    E a resposta deve confirmar o recebimento da telemetria
    E o contrato JSON Schema "sensor-telemetry-ok-response.schema.json" deve ser respeitado

  Cenário: Gerar alerta para container quase cheio
    Dado que a API de gestão de resíduos está disponível
    Quando eu envio uma telemetria do container "C-002" com nível 95
    Então o status code deve ser 202
    E a resposta deve informar alerta de container quase cheio
    E o contrato JSON Schema "sensor-telemetry-alert-response.schema.json" deve ser respeitado
