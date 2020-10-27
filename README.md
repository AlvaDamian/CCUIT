# CCUIT
Objeto simple para validaciones de CUIT argentino.

## Uso

### Creación de objeto

Para inicializar el objeto se necesita el tipo de CUIT y el número de documento. El digito verificador se determina a partir de esos datos.

En un CUIT, el tipo corresponde a los 2 primeros digito, el número de documento a los 8 digito siguientes y, el dígito verificador, al último digito.


    int tipo = 34;
    int numeroDocumento = 99903208;
    CUIT cuit = new CUIT(tipo, numeroDocumento);
    
    System.Console.WriteLine(cuit.Tipo);//34
    System.Console.WriteLine(cuit.NumeroDocumento);//99903208
    System.Console.WriteLine(cuit.Valido);//true
    System.Console.WriteLine(cuit.DigitoVerificador);//9
    System.Console.WriteLine(cuit.Numero);//34999032089

## Funcionalidades extras

### Validación de CUIT

El CUIT se puede validar utilizando el método estático **EsNumeroCUITValido** en alguna de sus variantes. El método retornara una estructura indicando si el CUIT es valido o no. En caso de que no sea valido, se incluye un código del motivo.

    //Variante 1
    int tipo = 34;
    int numeroDocumento = 99903208;
    int digitoVerificador = 5;//incorrecto, es 9
    ResultadoValidacion resultado = CUIT.CUIT.EsNumeroCUITValido(tipo, numeroDocumento, digitoVerificador);
    
    System.Console.WriteLine(resultado.Valido);//false
    System.Console.WriteLine(resultado.CodigoResultado);//DIGITO_VERIFICADOR_INCORRECTO
    
    //Variante 2
    long cuit = 34999032089;//CUIT valido
    
    ResultadoValidacion resultado = CUIT.CUIT.EsNumeroCUITValido(cuit);
    
    System.Console.WriteLine(resultado.Valido);//true
    System.Console.WriteLine(resultado.CodigoResultado);//CUIT_VALIDO
    
    
    //variante 3
    string cuit = "34999032089";
    
    ResultadoValidacion resultado = CUIT.CUIT.EsNumeroCUITValido(cuit);
    
    System.Console.WriteLine(resultado.Valido);//true
    System.Console.WriteLine(resultado.CodigoResultado);//CUIT_VALIDO

### Generación de digito verificador

A partir del tipo de CUIT y número de documento, se genera el digito verificador.

    int tipo = 34;
    int numeroDocumento = 99903208;
    
    //Este método arroja excepción si alguno de los argumentos es invalido.
    int digitoVerificador = CUIT.CalcularDigitoVerificador(tipo, numeroDocumento);//9