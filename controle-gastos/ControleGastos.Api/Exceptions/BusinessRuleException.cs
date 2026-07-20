namespace ControleGastos.Api.Exceptions;

/// <summary>
/// Exceção lançada quando uma regra de negócio é violada.
/// Exemplos: menor de 18 anos tentando cadastrar receita,
/// tipo de transação inválido, etc.
/// Utilizada na camada de Service para sinalizar ao Controller que deve retornar HTTP 400.
/// </summary>
public class BusinessRuleException : Exception
{
    public BusinessRuleException(string message) : base(message) { }
}
