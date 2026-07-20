namespace ControleGastos.Api.Exceptions;

/// <summary>
/// Exceção lançada quando um recurso solicitado não é encontrado no banco de dados.
/// Utilizada na camada de Service para sinalizar ao Controller que deve retornar HTTP 404.
/// </summary>
public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message) { }
}
