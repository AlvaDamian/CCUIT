using System;

namespace CUIT
{
    public sealed class CUIT
    {
        /// <summary>
        /// Cantidad de digitos que debe posee un CUIT.
        /// </summary>
        public static int CANTIDAD_CARACTERES_CUIT = 11;

        /// <summary>
        /// Tipo de CUIT. Corresponde a los dos primeros números.
        /// </summary>
        public int Tipo { get; private set; }

        /// <summary>
        /// Número de documento del CUIT. Corresponde  alos números
        /// del medio.
        /// </summary>
        public int NumeroDocumento { get; private set; }

        /// <summary>
        /// Digito verificador calculado. Corresponde al último número
        /// del CUIT.
        /// </summary>
        public int DigitoVerificador
        {
            get
            {
                return CalcularDigitoVerificador(this.Tipo, this.NumeroDocumento);
            }
        }

        /// <summary>
        /// Indica si el CUIT formado con los datos provisto es valido.
        /// </summary>
        public bool IsValido {
            get
            {
                try
                {
                    return EsNumeroCUITValido(this.Numero).Valido;
                }
                catch (Exception)
                {

                    return false;
                }
            }
        }

        /// <summary>
        /// Número completo de CUIT. Es una concatenación
        /// del tipo con el número de documento y el digito
        /// verificador.
        /// </summary>
        public long Numero {
            get
            {
                //Primero se debe convertir a long para no sobrepasar el límite del tipo
                long ret = (long)this.Tipo * 1000000000;
                ret += this.NumeroDocumento * 10;
                ret += this.DigitoVerificador;

                return ret;
            }
        }

        public CUIT(int tipo, int numeroDocumento)
        {
            this.Tipo = tipo;
            this.NumeroDocumento = numeroDocumento;
        }

        public static ResultadoValidacion EsNumeroCUITValido(int tipo, int numeroDocumento, int digitoVerificado)
        {
            long cuitCompleto = digitoVerificado;
            cuitCompleto += numeroDocumento * 10;
            cuitCompleto += (long)tipo * 1000000000;

            if (numeroDocumento < 10000000 || numeroDocumento > 99999999)
            {
                return ResultadoValidacion.CrearResultadoInvalido(CodigoResultadoValidacion.CANTIDAD_DIGITOS_INCORRECTOS);
            }

            if (digitoVerificado > 9)
            {
                return ResultadoValidacion.CrearResultadoInvalido(CodigoResultadoValidacion.CANTIDAD_DIGITOS_INCORRECTOS);
            }
            
            if (tipo > 99)
            {
                return ResultadoValidacion.CrearResultadoInvalido(CodigoResultadoValidacion.CANTIDAD_DIGITOS_INCORRECTOS);
            }

            return EsNumeroCUITValido(cuitCompleto);
        }

        /// <summary>
        /// Verifica que el número de CUIT sea valido.
        /// </summary>
        /// <param name="numeroCUITVerificar">número de CUIT a validar.</param>
        /// <returns>Un <see cref="ResultadoValidacion"/> con el resultado
        /// de la validación.</returns>
        public static ResultadoValidacion EsNumeroCUITValido(long numeroCUITVerificar)
        {
            if (numeroCUITVerificar <= 10000000000 || numeroCUITVerificar > 99999999999)
            {
                return ResultadoValidacion.CrearResultadoInvalido(CodigoResultadoValidacion.FORMATO_CUIT_INVALIDO);
            }

            //Se obtiene el tipo a partir de los 2 primeros números
            int tipo = (int)(numeroCUITVerificar / 1000000000);

            //Se quita el digito verificador.
            long cuitSinDigitoVerificador = numeroCUITVerificar / 10;

            //Se obtiene el número documento
            int numeroDocumento = (int)(cuitSinDigitoVerificador - tipo * 100000000);

            //Digito verificador provisto por el cliente
            long digitoVerificadorProvisto = numeroCUITVerificar % 10;

            //Digito verificador calculado
            long digitoVerificador;
            try
            {
                digitoVerificador = CalcularDigitoVerificador(tipo, numeroDocumento);
            }
            catch (Exception)
            {
                return ResultadoValidacion.CrearResultadoInvalido(CodigoResultadoValidacion.FORMATO_CUIT_INVALIDO);
            }
            

            if (!digitoVerificadorProvisto.Equals(digitoVerificador))
            {
                return ResultadoValidacion.CrearResultadoInvalido(CodigoResultadoValidacion.DIGITO_VERIFICADOR_INCORRECTO);
            }

            return ResultadoValidacion.CrearResultadoValido();
        }
        
        /// <summary>
        /// Verifica que el número de CUIT sea valido.
        /// </summary>
        /// <param name="numeroCUITVerificar">Número de CUIT a validar.</param>
        /// <returns>Un <see cref="ResultadoValidacion"/> con el resultado
        /// de la validación.</returns>
        public static ResultadoValidacion EsNumeroCUITValido(string numeroCUITVerificar)
        {
            if (string.IsNullOrEmpty(numeroCUITVerificar))
            {
                return ResultadoValidacion.CrearResultadoInvalido(CodigoResultadoValidacion.CANTIDAD_DIGITOS_INCORRECTOS);
            }

            string cuitNormalizado = numeroCUITVerificar.Trim();

            if (cuitNormalizado.Length != CANTIDAD_CARACTERES_CUIT)
            {
                return ResultadoValidacion.CrearResultadoInvalido(CodigoResultadoValidacion.CANTIDAD_DIGITOS_INCORRECTOS);
            }


            if (!long.TryParse(cuitNormalizado, out long cuitNumerico))
            {
                return ResultadoValidacion.CrearResultadoInvalido(CodigoResultadoValidacion.FORMATO_CUIT_INVALIDO);
            }

            return EsNumeroCUITValido(cuitNumerico);
        }

        /// <summary>
        /// Calcula el dígito verificador del CUIT.
        /// </summary>
        /// <param name="tipo"></param>
        /// <param name="numeroDocumento"></param>
        /// <returns>El dígito verificador calculado.</returns>
        public static int CalcularDigitoVerificador(int tipo, int numeroDocumento)
        {
            if (tipo <= 0)
            {
                throw new ArgumentException("'tipo' debe ser positivo.");
            }

            if (numeroDocumento <= 0)
            {
                throw new ArgumentException("'numeroDocumento' debe ser positivo.");
            }

            /**
             * La constante de multiplicación es en realidad 5432765432, pero
             * se invierte para facilitar el calculo.
             */
            uint constanteMultiplicacion = 2345672345;
            uint cuitSinDigitoVerificador = (uint)(tipo * 100000000 + numeroDocumento);
            int ret = 0;

            //Paso 1: Se multiplica cada digito por una constante y se suman los resultados.
            while (constanteMultiplicacion / 10 > 0)
            {
                int resto = (int)(cuitSinDigitoVerificador % 10);
                ret += ((int)(constanteMultiplicacion % 10)) * resto;

                cuitSinDigitoVerificador /= 10;
                constanteMultiplicacion /= 10;
            }

            //paso 2: Se calcula el modulo del resultado del paso 1 con 11
            ret %= 11;

            //Paso 3: se calcula la diferencia entre 11 y el resultado del paso 2
            ret = 11 - ret;

            /**
             * Paso 4:
             *  * si el resultado del paso 3 es 11, el digito verificador es 0.
             *  * si el resultado del paso 4 es 10, el digito verificador es 9.
             *  * en cualquier otro caso, el dígito verificador es el resultado del paso 3.
             */
            if (ret == 11)
            {
                return 0;

            }
            else if (ret == 10)
            {
                return 9;
            }

            return ret;
        }
    }
}
