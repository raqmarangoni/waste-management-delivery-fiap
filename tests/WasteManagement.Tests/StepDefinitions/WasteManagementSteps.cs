using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using FluentAssertions;
using Reqnroll;
using WasteManagement.Tests.Support;

namespace WasteManagement.Tests.StepDefinitions;

[Binding]
public sealed class WasteManagementSteps
{
    private WasteManagementApiFactory _factory = null!;
    private HttpClient _client = null!;
    private HttpResponseMessage? _response;
    private JsonDocument? _jsonDocument;
    private int _createdCollectionId;

    [BeforeScenario]
    public void BeforeScenario()
    {
        _factory = new WasteManagementApiFactory();
        _client = _factory.CreateClient();
    }

    [AfterScenario]
    public void AfterScenario()
    {
        _jsonDocument?.Dispose();
        _client.Dispose();
        _factory.Dispose();
    }

    [Given(@"que a API de gestão de resíduos está disponível")]
    public void ApiDisponivel()
    {
        _client.Should().NotBeNull();
    }

    [Given(@"existe uma coleta registrada de material ""(.*)"" com peso (.*)")]
    public async Task ExisteColetaRegistrada(string material, double peso)
    {
        await EnvioUmaColetaValida(material, peso);
        ((int)_response!.StatusCode).Should().Be(201);
    }

    [When(@"eu consulto a lista paginada de coletas")]
    public async Task ConsultoListaPaginadaDeColetas()
    {
        _response = await _client.GetAsync("/api/collections?page=1&pageSize=10");
        await StoreJsonResponseAsync();
    }

    [When(@"eu envio uma coleta válida de material ""(.*)"" com peso (.*)")]
    public async Task EnvioUmaColetaValida(string material, double peso)
    {
        var payload = new
        {
            location = "EcoPonto Centro",
            collectedAt = DateTime.UtcNow,
            materialType = material,
            weightKg = peso
        };

        _response = await _client.PostAsJsonAsync("/api/collections", payload);
        await StoreJsonResponseAsync();

        if (_response.StatusCode == HttpStatusCode.Created)
        {
            var location = _response.Headers.Location?.ToString();
            if (!string.IsNullOrWhiteSpace(location) && int.TryParse(location.Split('/').Last(), out var id))
            {
                _createdCollectionId = id;
            }
        }
    }

    [When(@"eu consulto a coleta de identificador (.*)")]
    public async Task ConsultoColetaPorIdentificador(int id)
    {
        _response = await _client.GetAsync($"/api/collections/{id}");
        await StoreJsonResponseAsync(allowEmpty: true);
    }

    [When(@"eu envio uma telemetria do container ""(.*)"" com nível (.*)")]
    public async Task EnvioTelemetria(string containerId, double nivel)
    {
        var payload = new { containerId, fillLevelPercent = nivel };
        _response = await _client.PostAsJsonAsync("/api/sensors/telemetry", payload);
        await StoreJsonResponseAsync();
    }

    [When(@"eu consulto o relatório consolidado")]
    public async Task ConsultoRelatorioConsolidado()
    {
        _response = await _client.GetAsync("/api/reports/summary");
        await StoreJsonResponseAsync();
    }

    [When(@"eu registro um alerta com a mensagem ""(.*)""")]
    public async Task RegistroAlerta(string mensagem)
    {
        var payload = new { message = mensagem, createdAt = DateTime.UtcNow, resolved = false };
        _response = await _client.PostAsJsonAsync("/api/alerts", payload);
        await StoreJsonResponseAsync();
    }

    [When(@"eu consulto a lista de alertas")]
    public async Task ConsultoListaDeAlertas()
    {
        _response = await _client.GetAsync("/api/alerts");
        await StoreJsonResponseAsync();
    }

    [Then(@"o status code deve ser (.*)")]
    public void StatusCodeDeveSer(int statusCode)
    {
        _response.Should().NotBeNull();
        ((int)_response!.StatusCode).Should().Be(statusCode);
    }

    [Then(@"a resposta deve conter a lista paginada de coletas")]
    public void RespostaContemListaPaginada()
    {
        var root = CurrentJson();
        root.GetProperty("page").GetInt32().Should().Be(1);
        root.GetProperty("pageSize").GetInt32().Should().Be(10);
        root.GetProperty("total").GetInt32().Should().BeGreaterThanOrEqualTo(0);
        root.GetProperty("items").ValueKind.Should().Be(JsonValueKind.Array);
    }

    [Then(@"a coleta deve ser recuperada pelo identificador gerado")]
    public async Task ColetaRecuperadaPeloIdentificadorGerado()
    {
        _createdCollectionId.Should().BeGreaterThan(0);

        _response = await _client.GetAsync($"/api/collections/{_createdCollectionId}");
        _response.StatusCode.Should().Be(HttpStatusCode.OK);
        await StoreJsonResponseAsync();

        var root = CurrentJson();
        root.GetProperty("id").GetInt32().Should().Be(_createdCollectionId);
        root.GetProperty("location").GetString().Should().Be("EcoPonto Centro");
    }

    [Then(@"a resposta deve confirmar o recebimento da telemetria")]
    public void RespostaConfirmaTelemetria()
    {
        CurrentJson().GetProperty("received").GetBoolean().Should().BeTrue();
    }

    [Then(@"a resposta deve informar alerta de container quase cheio")]
    public void RespostaInformaAlertaContainerCheio()
    {
        CurrentJson().GetProperty("alert").GetString().Should().Contain("Container almost full");
    }

    [Then(@"a resposta deve conter os indicadores de coletas por material")]
    public void RespostaContemIndicadores()
    {
        var root = CurrentJson();
        root.GetProperty("totalCollections").GetInt32().Should().BeGreaterThanOrEqualTo(1);
        root.GetProperty("totalWeight").GetDouble().Should().BeGreaterThan(0);
        root.GetProperty("byMaterial").EnumerateArray().Should().Contain(item => item.GetProperty("material").GetString() == "Vidro");
    }

    [Then(@"a resposta deve conter o alerta ""(.*)""")]
    public void RespostaContemAlerta(string mensagem)
    {
        CurrentJson().EnumerateArray().Should().Contain(item => item.GetProperty("message").GetString() == mensagem);
    }

    [Then(@"o contrato JSON Schema ""(.*)"" deve ser respeitado")]
    public void ContratoJsonSchemaDeveSerRespeitado(string schemaFileName)
    {
        SchemaValidator.Validate(CurrentJson(), schemaFileName);
    }

    private async Task StoreJsonResponseAsync(bool allowEmpty = false)
    {
        _jsonDocument?.Dispose();
        _jsonDocument = null;

        if (_response == null)
        {
            return;
        }

        var content = await _response.Content.ReadAsStringAsync();
        if (string.IsNullOrWhiteSpace(content))
        {
            if (!allowEmpty)
            {
                content.Should().NotBeNullOrWhiteSpace();
            }
            return;
        }

        _jsonDocument = JsonDocument.Parse(content);
    }

    private JsonElement CurrentJson()
    {
        _jsonDocument.Should().NotBeNull("a resposta deve possuir corpo JSON");
        return _jsonDocument!.RootElement;
    }
}
