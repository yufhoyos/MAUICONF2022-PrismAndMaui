namespace PrismAndMauiApp1.Exceptions;

using System;

public class ApiErrorResult : Exception
{
    public int CodigoError { get; set; }
    public int Tipo { get; set; }
    public string Mensaje { get; set; }
    public string UrlRetorno { get; set; }
    public string Detalle { get; set; }

    public ApiErrorResult()
    {

    }

    /// <summary>
    /// Constructor con tipo por defecto Error.
    /// </summary>
    /// <param name="codigoError"></param>
    /// <param name="mensaje"></param>
    public ApiErrorResult(int codigoError, string mensaje)
    {
        this.CodigoError = CodigoError;
        this.Mensaje = mensaje;
        this.Tipo = 3;
    }

    /// <summary>
    /// Constructor con todos los parametros.
    /// </summary>
    /// <param name="codigoError"></param>
    /// <param name="mensaje"></param>
    /// <param name="tipo"></param>
    public ApiErrorResult(int codigoError, string mensaje, int tipo)
    {
        this.CodigoError = CodigoError;
        this.Mensaje = mensaje;
        this.Tipo = tipo;
    }
}
