﻿namespace MyHostel.Application.Common.Exceptions;

public class NotFoundException(string resource, object key) : Exception($"Recurso '{resource}' con id '{key}' no fue encontrado.")
{
    public string Resource { get; } = resource;
    public object Key { get; } = key;
}
