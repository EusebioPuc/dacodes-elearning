# dacodes-elearning

## Instalación

* Descargar los archivos.
* Al abrir el archivo de la solución (elearning.sln) se verá en el Solution Explorer que no se encuentra el proyecto,
  porque la ruta es diferente, entonces hacemos clic derecho sobre el proyecto no encontrado y seleccionamos la opción REMOVE,
  ahora hacemos clic derecho sobre la solución y buscamos la opción ADD>EXISTING PROJECT, buscamos y seleccionamos el proyecto "elearning"
* Crear una base de datos en SQL SERVER (Usada SQL Server Management Studio v17.9) con el nombre "elearning", importar en ella el archivo "elearning/Models/BaseDatosElearning.sql"
* Actualizar la cadena de conexión con la base de datos (web.config), sustituyendo el "data source, user id y password", también se podría cambiar a la autenticación por windows:
<pre>
<!-- <add name="elearningEntities" connectionString="metadata=res://*/Models.Model.csdl|res://*/Models.Model.ssdl|res://*/Models.Model.msl;provider=System.Data.SqlClient;provider connection string=&quot;data source=HQSKY-OLNEUS;initial catalog=elearning;persist security info=True;user id=sa;password=C@7-2016;MultipleActiveResultSets=True;App=EntityFramework&quot;" providerName="System.Data.EntityClient" />
-->
</pre>
* Ejecutar 🚀
* Adicionalmente: después de cargar en el explorador el localhost con el número de puerto, puede agregarse a la URL */swagger* 
para probar todos los controladores y acciones de la API

## Herramientas de desarrollo
* Visual Studio 2017 Community
* Framework 4.6.1
* SQL Server Management Studio v17.9
* GIT

## Paquetes adicionales utilizados
* EntityFramework. Se eligió por su practicidad en el uso para manejar las conexiones y mapeos de la base de datos, 
es sencilla la sincronización de cambios de la base de datos a la aplicación.
* Swashbuckle. Facilita el diseño, construcción, documentación y consumo de servicios web RESTful

## Uso
Se realizaron las siguientes acciones en los siguientes controladores:

### Cursos. 
Se tiene el CRUD (solamente lo pueden usar los maestros activos) y adicionalmente se tiene la llamada GET /api/Cursos/CursosAlumno/{idUsuario}, {idUsuario} es el IdUsuario del alumno, 
    para obtener una lista de todos los cursos, indicando a cuáles puede acceder el estudiante, un bit indica que si puede acceder es porque esta inscrito.

<pre>
//Es el JSON base es
{
  "IdCurso": 0,
  "Curso1": "string",
  "Descripcion": "string",
  "Estatus": "string"
}
</pre>
* Estatus: ACTIVO o INACTIVO, es para cuando no se pueda eliminar el registro por la integridad referencial

### AlumnosCursos
Se tiene el CRUD, se utilizaría para poder gestionar la inscripción de alumnos a los cursos así como la asignación de los maestros,
también se tiene una asignación de estatus: Completado, En curso, Inscrito.

### Lecciones. 
Se tiene el CRUD (solamente lo pueden usar los maestros activos) y adicionalmente se tiene una llamada GET /api/Lecciones/LeccionAlumno/{idUsuario}/{idACurso}/{idCurso}, dónde el {idUsuario} es el IdUsuario del alumno,
 {idACurso} es el IdAlumnoCurso que es la llave principal de una tabla relacional entre Alumno y los Cursos que tiene o ha tenido, {idCurso} hace referencia al Curso en el cual se encuentra, lo que hace es 
  obtener lecciones para un curso, indicando a cuáles puede acceder el estudiante y detalles de la lección para responder sus preguntas. Un bit indica que
  si puede acceder es porque no ha aprobado la lección.
<pre>
//Es el JSON base es
{
  "IdLeccion": 0,
  "Leccion1": "string",
  "Descripcion": "string",
  "Contenido": "string",
  "IdCurso": 0,
  "PuntajeAprobatorio": 0,
  "Estatus": "string",
}
</pre>
* PuntajeAprobatorio: Es la calificación mínima que debe de tener el alumno para considerarse aprobado en la lección
* Estatus: ACTIVO o INACTIVO, es para cuando no se pueda eliminar el registro por la integridad referencial

### AlumnosLecciones
Se tiene el CRUD, se utilizaría para poder gestionar las lecciones de alumnos de los cursos en los cuales se encuentra inscrito,
también se tiene una "Calificacion", para posteriormente compararse con el PuntajeAprobatorio para todas las lecciones del curso y determinar si aprueba o no el curso el alumno.


### Preguntas. 
Se tiene el CRUD (solamente lo pueden usar los maestros activos)
Son las preguntas que tiene la lección, existen 4 tipos en el catálogo TipoPregunta:
* 1	Booleana: 2 Respuestas, 1 es verdadera
* 2	Multiple 1 Correcta: Opción múltiple donde solo una respuesta es correcta 
* 3	Multiple +1 Correcta: Opción múltiple donde más de una respuesta es correcta
* 4	Multiple +1 Forsosa Correcta: Opción múltiple donde más de una respuesta es correcta y todas deben responderse correctamente
Se puso en un catálogo porque son las iniciales en el proyecto, pero podrían crearse más.
<pre>
  //Es el JSON base es
  { 
    "IdPregunta": 2,
    "Pregunta1": "¿Un arreglo es un conjunto de datos?",
    "IdTipoPregunta": 1,
    "IdLeccion": 1,
    "Puntos": 10,
    "Estatus": "ACTIVO"
  }
</pre>


### Respuestas 
Se tiene el CRUD (solamente lo pueden usar los maestros activos) y adicionalmente se tiene una llamada POST /api/Respuestas/RespuestasLeccionAlumno/{id}/{idALeccion}, {id} es el IdUsuario del alumno,
  {idALeccion} es el IdAlumnoLeccion que es la llave principal de una tabla relacional entre Alumno y las Lecciones tiene o ha tenido, también recibe un json que 
  contiene todas las respuestas elegidas por un alumno de una lección:
  <pre>
  [
  {
    "IdAlumnoPregunta": 0,
    "IdAlumnoLeccion": 0,
    "IdPregunta": 0,
    "IdRespuesta": 0,
    "Puntos": 0
  },
  {
    "IdAlumnoPregunta": 0,
    "IdAlumnoLeccion": 0,
    "IdPregunta": 0,
    "IdRespuesta": 0,
    "Puntos": 0
  }
]
  </pre>
* Usuarios. Se tiene el CRUD, la idea es poder crear a los maestros y los alumnos, que inicialmente existirían en el sistema 
sin embargo se dejó la oportunidad de tener un usuario Admin. 
* IdRol: Esto se está manejando por 1 Admin, 2 Maestro, 3 Alumno.
* Token: Se utilizaría para hacer el login 1 vez y usar el token en toda la API como forma de autenticación (no me alcanzo el tiempo para cerrar la aplicación)
* Estatus: ACTIVO o INACTIVO, es para cuando no se pueda eliminar el registro por la integridad referencial





## Adicionalmente pondría haría esto con más tiempo:
* Cerrar la aplicación para hacer forzoso el uso del token en cada petición
* Restringir las peticiones por medio de los roles
* Mensajes de error más amigables
* Cálculo de la calificación de las respuestas de los alumnos, se guardan en la base de datos las lista de respuestas por lección,
pero no se esta ponderando
* ViewModels para devolver solo la información necesaria en cada petición y no todo el objeto del modelo



