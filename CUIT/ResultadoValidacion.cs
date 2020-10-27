namespace CUIT
{
    /// <summary>
    /// Códigos del resultado de validación del CUIT.
    /// </summary>
    public enum CodigoResultadoValidacion
    {
        /// <summary>
        /// El CUIT validado es correcto.
        /// </summary>
        CUIT_VALIDO,

        /// <summary>
        /// <para>
        /// La cantidad de digitos del CUIT validado es incorrecta.
        /// </para>
        /// 
        /// <para>
        /// Un CUIT siempre tiene 11 digitos.
        /// </para>
        /// </summary>
        CANTIDAD_DIGITOS_INCORRECTOS,

        /// <summary>
        /// El formato del CUIT validado es incorrecto. Debe ser
        /// un número entero.
        /// </summary>
        FORMATO_CUIT_INVALIDO,

        /// <summary>
        /// El digito verificador del CUIT validado es incorrecto.
        /// </summary>
        DIGITO_VERIFICADOR_INCORRECTO
    }

    /// <summary>
    /// Resultado de validación del CUIT.
    /// </summary>
    public struct ResultadoValidacion
    {
        /// <summary>
        /// true si el CUIT es valido, false de lo contrario.
        /// </summary>
        public bool Valido { get; private set; }

        /// <summary>
        /// Código del resultado de la validación del CUIT.
        /// </summary>
        public CodigoResultadoValidacion CodigoResultado { get; private set; }


        private ResultadoValidacion(bool valido, CodigoResultadoValidacion codigoResultado)
        {
            this.Valido = valido;
            this.CodigoResultado = codigoResultado;
        }

        /// <summary>
        /// Crea un <see cref="ResultadoValidacion"/> indicando que el
        /// CUIT es valido.
        /// </summary>
        /// <returns>Un <see cref="ResultadoValidacion"/> con su datos
        /// inicializados.</returns>
        public static ResultadoValidacion CrearResultadoValido()
        {
            return new ResultadoValidacion(true, CodigoResultadoValidacion.CUIT_VALIDO);
        }

        /// <summary>
        /// Crea un <see cref="ResultadoValidacion"/> indicando que el
        /// CUIT es invalido.
        /// </summary>
        /// <param name="codigoResultado">Código de resultado de la validación
        /// que indica el error detectado.</param>
        /// <returns>Un <see cref="ResultadoValidacion"/> con sus datos
        /// inicializados.</returns>
        public static ResultadoValidacion CrearResultadoInvalido(CodigoResultadoValidacion codigoResultado)
        {
            return new ResultadoValidacion(false, codigoResultado);
        }
    }
}
