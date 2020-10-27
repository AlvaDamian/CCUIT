using CUIT;

using NUnit.Framework;

namespace CUITTests
{
    public class CUITTests
    {
        [Test]
        public void InicializaSinErrores()
        {
            CUIT.CUIT cuit = new CUIT.CUIT(1, 2);
        }

        [Test]
        public void IndicaCUITInvalidoCuandoSeInicializaTipoConNegativo()
        {
            CUIT.CUIT cuit = new CUIT.CUIT(-2, 3);

            Assert.IsFalse(cuit.IsValido);
        }

        [Test]
        public void IndicaCUITInvalidoCuandoSeInicializaTipoConCero()
        {
            CUIT.CUIT cuit = new CUIT.CUIT(0, 3);
            Assert.IsFalse(cuit.IsValido);
        }

        [Test]
        public void AsignaTipoProvistoEnConstructor()
        {
            int tipo = 20;
            CUIT.CUIT cuit = new CUIT.CUIT(tipo, 3);

            Assert.AreEqual(tipo, cuit.Tipo);
        }

        [Test]
        public void AsignaNumeroDocumentoProvistoEnConstructor()
        {
            int numeroDocumento = 12345678;
            CUIT.CUIT cuit = new CUIT.CUIT(1, numeroDocumento);

            Assert.AreEqual(numeroDocumento, cuit.NumeroDocumento);
        }

        [Test]
        public void AsignaMismoDigitoVerificadorQueElCalculado()
        {
            //34-99903208-9: Número de CUIT de la ciudad de BS. AS.
            int tipo = 34;
            int numeroDocumento = 99903208;
            int digitoVerificador = CUIT.CUIT.CalcularDigitoVerificador(tipo, numeroDocumento);

            CUIT.CUIT cuit = new CUIT.CUIT(tipo, numeroDocumento);

            Assert.AreEqual(digitoVerificador, cuit.DigitoVerificador);
        }

        [Test]
        public void ElNumeroCompletoSeConformaPorTipoNumeroDocumentoYDigitoVerificador()
        {
            //34-99903208-9: Número de CUIT de la ciudad de BS. AS.
            int tipo = 34;
            int numeroDocumento = 99903208;
            int digitoVerificador = CUIT.CUIT.CalcularDigitoVerificador(tipo, numeroDocumento);//9
            long numeroCUITCompleto = 34999032089;

            CUIT.CUIT cuit = new CUIT.CUIT(tipo, numeroDocumento);

            Assert.AreEqual(numeroCUITCompleto, cuit.Numero);
        }

        [Test]
        public void CalculaElDigitoVerificadorCorrespondiente()
        {
            //34-99903208-9: Número de CUIT de la ciudad de BS. AS.
            int tipo = 34;
            int numeroDocumento = 99903208;
            int digitoVerificador = 9;
            int digitoVerificadorCalculado = CUIT.CUIT.CalcularDigitoVerificador(tipo, numeroDocumento);

            Assert.AreEqual(digitoVerificador, digitoVerificadorCalculado);
        }

        [Test]
        public void ValidarCUITInformaQueEsInvalidoCuandoElTipoEsNegativo()
        {
            ResultadoValidacion resultado = CUIT.CUIT.EsNumeroCUITValido(-1, 2, 3);
            Assert.IsFalse(resultado.Valido);

            resultado = CUIT.CUIT.EsNumeroCUITValido(-34999032089);
            Assert.IsFalse(resultado.Valido);

            resultado = CUIT.CUIT.EsNumeroCUITValido("-34999032089");
            Assert.IsFalse(resultado.Valido);
        }

        [Test]
        public void ValidarCUITInformaQueEsInvalidoCuandoElTipoEsCero()
        {
            ResultadoValidacion resultado = CUIT.CUIT.EsNumeroCUITValido(0, 2, 3);
            Assert.IsFalse(resultado.Valido);

            resultado = CUIT.CUIT.EsNumeroCUITValido(999032089);
            Assert.IsFalse(resultado.Valido);

            resultado = CUIT.CUIT.EsNumeroCUITValido("999032089");
            Assert.IsFalse(resultado.Valido);
        }

        [Test]
        public void ValidarCUITInformaQueEsInvalidoCuandoElNumeroDocumentoEsNegativo()
        {
            ResultadoValidacion resultado = CUIT.CUIT.EsNumeroCUITValido(1, -2, 3);
            Assert.IsFalse(resultado.Valido);

            resultado = CUIT.CUIT.EsNumeroCUITValido(-1000000003);
            Assert.IsFalse(resultado.Valido);

            resultado = CUIT.CUIT.EsNumeroCUITValido("-1000000003");
            Assert.IsFalse(resultado.Valido);
        }

        [Test]
        public void ValidarCUITInformaQueEsInvalidoCuandoElNumeroDocumentoEsCero()
        {
            ResultadoValidacion resultado = CUIT.CUIT.EsNumeroCUITValido(1, 0, 3);
            Assert.IsFalse(resultado.Valido);

            resultado = CUIT.CUIT.EsNumeroCUITValido(34000000009);
            Assert.IsFalse(resultado.Valido);

            resultado = CUIT.CUIT.EsNumeroCUITValido("34000000009");
            Assert.IsFalse(resultado.Valido);
        }

        [Test]
        public void ValidarCUITInformaQueEsInvalidoCuandoElDigitoVerificadorEsNegativo()
        {
            ResultadoValidacion resultado = CUIT.CUIT.EsNumeroCUITValido(1, 2, -3);
            Assert.IsFalse(resultado.Valido);

            resultado = CUIT.CUIT.EsNumeroCUITValido(-34999032089);
            Assert.IsFalse(resultado.Valido);

            resultado = CUIT.CUIT.EsNumeroCUITValido("-34999032089");
            Assert.IsFalse(resultado.Valido);
        }

        [Test]
        public void ValidarCUITInformaQueEsInvalidoCuandoNoTiene11Digitos()
        {
            int tipo = 34;
            int numeroDocumento = 999045;
            int digitoVerificador = 9;

            ResultadoValidacion resultado = CUIT.CUIT.EsNumeroCUITValido(tipo, numeroDocumento, digitoVerificador);

            Assert.IsFalse(resultado.Valido);
            Assert.AreEqual(CodigoResultadoValidacion.CANTIDAD_DIGITOS_INCORRECTOS, resultado.CodigoResultado);
        }

        [Test]
        public void ValidarCUITInformaQueEsInvalidoCuandoLoEs()
        {
            //34-99903208-9: Número de CUIT de la ciudad de BS. AS.
            int tipo = 34;
            int numeroDocumento = 99903208;
            int digitoVerificador = 9 - 1;

            ResultadoValidacion resultado = CUIT.CUIT.EsNumeroCUITValido(tipo, numeroDocumento, digitoVerificador);
            Assert.IsFalse(resultado.Valido);

            resultado = CUIT.CUIT.EsNumeroCUITValido(34999032088);
            Assert.IsFalse(resultado.Valido);

            resultado = CUIT.CUIT.EsNumeroCUITValido("34999032088");
            Assert.IsFalse(resultado.Valido);
        }

        [Test]
        public void ValidarCUITInformaQueEsValidoCuandoLoEs()
        {
            //34-99903208-9: Número de CUIT de la ciudad de BS. AS.
            int tipo = 34;
            int numeroDocumento = 99903208;
            int digitoVerificador = 9;

            ResultadoValidacion resultado = CUIT.CUIT.EsNumeroCUITValido(tipo, numeroDocumento, digitoVerificador);
            
            Assert.IsTrue(resultado.Valido);
            Assert.AreEqual(CodigoResultadoValidacion.CUIT_VALIDO, resultado.CodigoResultado);



            resultado = CUIT.CUIT.EsNumeroCUITValido(34999032089);

            Assert.IsTrue(resultado.Valido);
            Assert.AreEqual(CodigoResultadoValidacion.CUIT_VALIDO, resultado.CodigoResultado);



            resultado = CUIT.CUIT.EsNumeroCUITValido("34999032089");

            Assert.IsTrue(resultado.Valido);
            Assert.AreEqual(CodigoResultadoValidacion.CUIT_VALIDO, resultado.CodigoResultado);
        }
    }
}