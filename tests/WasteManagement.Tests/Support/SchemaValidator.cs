using System;
using System.IO;
using FluentAssertions;
using Json.Schema;
using System.Text.Json;

namespace WasteManagement.Tests.Support;

public static class SchemaValidator
{
    public static void Validate(JsonElement json, string schemaFileName)
    {
        var schemaPath = Path.Combine(AppContext.BaseDirectory, "Schemas", schemaFileName);
        var schemaText = File.ReadAllText(schemaPath);
        var schema = JsonSchema.FromText(schemaText);
        var result = schema.Evaluate(json, new EvaluationOptions { OutputFormat = OutputFormat.List });

        result.IsValid.Should().BeTrue($"a resposta deve respeitar o contrato JSON Schema {schemaFileName}");
    }
}