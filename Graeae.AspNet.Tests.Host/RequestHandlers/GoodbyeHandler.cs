﻿using GeneratedCode;

using Microsoft.AspNetCore.Mvc;
using Graeae.AspNet.Tests.Host.RequestHandlers.Models;

namespace Graeae.AspNet.Tests.Host.RequestHandlers;

[RequestHandler("/goodbye")]
public static class GoodbyeHandler
{
	public static IResult Get(HttpContext context, [FromBody] Person? name)
	{
		return TypedResults.Ok($"Hello, {name ?? "World"}");
	}
}

public class Check
{
    public Janitor Janitor{ get; set; } = new();
}