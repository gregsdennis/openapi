using System.Text.Json;
using Graeae.Models.SchemaDraft4;
using Json.Pointer;
using Json.Schema;

namespace Graeae.Models.Tests;

public class PayloadValidationTests
{

	[TestCase("payload-valid.json")]
	public async Task ReferencesValid(string fileName)
	{
		var schemaFileName = GetFile("schema-components.json");
		var fileText = await File.ReadAllTextAsync(schemaFileName);
		var openApiDoc = JsonSerializer.Deserialize(fileText, TestSerializerContext.Default.OpenApiDocument);

		var options = new EvaluationOptions
		{
			EvaluateAs = Draft4Support.Draft4Version,
		};
		await openApiDoc!.Initialize(options.SchemaRegistry);

		var componentRef = JsonPointer.Parse("#/components/schemas/outer");
		var fullFileName = GetFile(fileName);
		var payloadJson = await File.ReadAllTextAsync(fullFileName);
		var document = JsonDocument.Parse(payloadJson);

		var results = openApiDoc.EvaluatePayload(document, componentRef, options);
		Assert.That(results!.IsValid, Is.True);
	}


	[TestCase("payload-invalid1.json")]
	[TestCase("payload-invalid2.json")]
	[TestCase("payload-invalid3.json")]
	[TestCase("payload-invalid4.json")]
	public async Task ReferencesInvalid(string fileName)
	{
		var schemaFileName = GetFile("schema-components.json");

		var fileText = await File.ReadAllTextAsync(schemaFileName);
		var openApiDoc = JsonSerializer.Deserialize(fileText, TestSerializerContext.Default.OpenApiDocument);

		var options = new EvaluationOptions
		{
			EvaluateAs = Draft4Support.Draft4Version,
		};
		await openApiDoc!.Initialize(options.SchemaRegistry);

		var componentRef = JsonPointer.Parse("#/components/schemas/outer");

		var fullFileName = GetFile(fileName);
		var payloadJson = await File.ReadAllTextAsync(fullFileName);
		var document = JsonDocument.Parse(payloadJson);

		var results = openApiDoc.EvaluatePayload(document, componentRef, options);
		Assert.That(results!.IsValid, Is.False);
	}
}