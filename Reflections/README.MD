# Reflecciones en .NET

La reflexión es una característica poderosa en el entorno de desarrollo de .NET que permite examinar y manipular la estructura de los tipos en tiempo de ejecución. A través de la reflexión, se puede obtener información sobre los tipos, sus propiedades, métodos y eventos, e incluso crear instancias de tipos dinámicamente.

## ¿Qué es la reflexión?

La reflexión es el proceso de examinar los metadatos de los tipos en tiempo de ejecución. Los metadatos incluyen información sobre la estructura y los miembros de un tipo, como el nombre, los parámetros, los atributos y más. .NET proporciona un conjunto de clases en el nemespaces `System.Reflection` que permiten realizar operaciones de reflexión.

## ¿Por qué usar la reflexión?

La reflexión es útil en una variedad de escenarios, como:

- Inspección de tipos: puedes obtener información sobre los tipos, como sus propiedades, métodos y eventos, lo que te permite realizar análisis dinámico de los tipos en tiempo de ejecución.

- Creación de instancias dinámicas: puedes crear instancias de tipos en tiempo de ejecución, sin conocerlos en tiempo de compilación. Esto es útil cuando necesitas trabajar con tipos desconocidos o cuando se desea cargar tipos dinámicamente desde ensamblados externos.

- Invocación dinámica de miembros: Se puede invocar métodos y propiedades de tipos en tiempo de ejecución, incluso si no se conoce esos miembros en tiempo de compilación.

## Uso básico de reflexiones

A continuación se muestra un ejemplo básico de cómo utilizar la reflexión en .NET:
```C#
// Obtener el tipo de una clase
Type tipo = typeof(MiClase);

// Obtener información sobre los miembros del tipo
MemberInfo[] miembros = tipo.GetMembers();

// Recorrer los miembros e imprimir sus nombres
foreach (MemberInfo miembro in miembros)
{
    Console.WriteLine(miembro.Name);
}
```

En este ejemplo, `typeof(MiClase)` devuelve un objeto `Type` que representa el tipo de la clase MiClase. A partir de este objeto Type, se puede obtener información sobre los miembros del tipo utilizando métodos como `GetMembers()`. En este caso, se recorren los miembros y se imprime el nombre de cada uno.

## Clase `Type`
```C#

static void Main(string[] args)
{
    int a = 15;
    Type type = a.GetType();
    Console.WriteLine(type);
}
```
En este ejemplo, la salida es: `System.Int32` que es el tipo de dato de la variable a.

```C#
static void Main(string[] args)
{
    int a = 15;
    Type type = a.GetType();
    Console.WriteLine(type.FullName);
}
```
En este ejemplo, `type.FullName` devuelve el nombre completo de la clase.

## Conclusiones

La reflexión en .NET es una característica poderosa que le permite examinar y manipular tipos en tiempo de ejecución. Le permite obtener información sobre los miembros de un tipo, crear instancias dinámicamente e invocar métodos y propiedades en tiempo de ejecución. Si bien la reflexión puede ser útil en ciertos escenarios, es importante tener en cuenta que también tiene un impacto en el rendimiento y puede llevar a un código menos seguro si no se utiliza correctamente. Por lo tanto, se recomienda utilizar la reflexión con cuidado y considerar alternativas más estáticas cuando sea posible.


