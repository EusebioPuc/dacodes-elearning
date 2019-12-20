USE [master]
GO
/****** Object:  Database [elearning]    Script Date: 20/12/2019 09:32:21 a. m. ******/
CREATE DATABASE [elearning]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'elearning', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\elearning.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'elearning_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\elearning_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [elearning] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [elearning].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [elearning] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [elearning] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [elearning] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [elearning] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [elearning] SET ARITHABORT OFF 
GO
ALTER DATABASE [elearning] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [elearning] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [elearning] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [elearning] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [elearning] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [elearning] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [elearning] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [elearning] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [elearning] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [elearning] SET  DISABLE_BROKER 
GO
ALTER DATABASE [elearning] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [elearning] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [elearning] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [elearning] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [elearning] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [elearning] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [elearning] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [elearning] SET RECOVERY FULL 
GO
ALTER DATABASE [elearning] SET  MULTI_USER 
GO
ALTER DATABASE [elearning] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [elearning] SET DB_CHAINING OFF 
GO
ALTER DATABASE [elearning] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [elearning] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [elearning]
GO
/****** Object:  Table [dbo].[AlumnoCurso]    Script Date: 20/12/2019 09:32:21 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AlumnoCurso](
	[IdAlumnoCurso] [int] IDENTITY(1,1) NOT NULL,
	[IdAlumno] [int] NOT NULL,
	[IdCurso] [int] NOT NULL,
	[IdMaestro] [int] NULL,
	[IdAlumnoCursoEstatus] [int] NOT NULL,
	[Calificacion] [decimal](5, 2) NULL,
 CONSTRAINT [PK_AlumnoCurso] PRIMARY KEY CLUSTERED 
(
	[IdAlumnoCurso] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AlumnoCursoEstatus]    Script Date: 20/12/2019 09:32:21 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AlumnoCursoEstatus](
	[IdAlumnoCursoEstatus] [int] IDENTITY(1,1) NOT NULL,
	[EstatusCurso] [varchar](10) NOT NULL,
 CONSTRAINT [PK_AlumnoCursoEstatus] PRIMARY KEY CLUSTERED 
(
	[IdAlumnoCursoEstatus] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AlumnoLeccion]    Script Date: 20/12/2019 09:32:21 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AlumnoLeccion](
	[IdAlumnoLeccion] [int] IDENTITY(1,1) NOT NULL,
	[IdAlumnoCurso] [int] NOT NULL,
	[IdLeccion] [int] NOT NULL,
	[Calificacion] [decimal](5, 2) NULL,
 CONSTRAINT [PK_AlumnoLeccion] PRIMARY KEY CLUSTERED 
(
	[IdAlumnoLeccion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AlumnoPregunta]    Script Date: 20/12/2019 09:32:21 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AlumnoPregunta](
	[IdAlumnoPregunta] [int] IDENTITY(1,1) NOT NULL,
	[IdAlumnoLeccion] [int] NOT NULL,
	[IdPregunta] [int] NOT NULL,
	[IdRespuesta] [int] NOT NULL,
	[Puntos] [decimal](5, 2) NOT NULL,
	[Correcto] [bit] NULL,
 CONSTRAINT [PK_AlumnoPregunta] PRIMARY KEY CLUSTERED 
(
	[IdAlumnoPregunta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Curso]    Script Date: 20/12/2019 09:32:21 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Curso](
	[IdCurso] [int] IDENTITY(1,1) NOT NULL,
	[Curso] [varchar](150) NOT NULL,
	[Descripcion] [varchar](max) NULL,
	[Estatus] [varchar](10) NOT NULL,
 CONSTRAINT [PK_Curso] PRIMARY KEY CLUSTERED 
(
	[IdCurso] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Leccion]    Script Date: 20/12/2019 09:32:21 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Leccion](
	[IdLeccion] [int] IDENTITY(1,1) NOT NULL,
	[Leccion] [varchar](150) NOT NULL,
	[Descripcion] [varchar](max) NULL,
	[Contenido] [varchar](max) NULL,
	[IdCurso] [int] NOT NULL,
	[PuntajeAprobatorio] [decimal](5, 2) NOT NULL,
	[Estatus] [varchar](10) NOT NULL,
 CONSTRAINT [PK_Leccion] PRIMARY KEY CLUSTERED 
(
	[IdLeccion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pregunta]    Script Date: 20/12/2019 09:32:21 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pregunta](
	[IdPregunta] [int] IDENTITY(1,1) NOT NULL,
	[Pregunta] [varchar](max) NULL,
	[IdTipoPregunta] [int] NOT NULL,
	[IdLeccion] [int] NOT NULL,
	[Puntos] [decimal](5, 2) NULL,
	[Estatus] [varchar](10) NULL,
 CONSTRAINT [PK_Pregunta] PRIMARY KEY CLUSTERED 
(
	[IdPregunta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Respuesta]    Script Date: 20/12/2019 09:32:21 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Respuesta](
	[IdRespuesta] [int] IDENTITY(1,1) NOT NULL,
	[Respuesta] [varchar](max) NOT NULL,
	[Correcta] [bit] NOT NULL,
	[IdPregunta] [int] NOT NULL,
	[Estatus] [varchar](10) NOT NULL,
 CONSTRAINT [PK_Respuesta] PRIMARY KEY CLUSTERED 
(
	[IdRespuesta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TipoPregunta]    Script Date: 20/12/2019 09:32:21 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TipoPregunta](
	[IdTipoPregunta] [int] IDENTITY(1,1) NOT NULL,
	[TipoPregunta] [varchar](50) NOT NULL,
	[Descripcion] [varchar](250) NULL,
 CONSTRAINT [PK_TipoPregunta] PRIMARY KEY CLUSTERED 
(
	[IdTipoPregunta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 20/12/2019 09:32:21 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario](
	[IdUsuario] [int] IDENTITY(1,1) NOT NULL,
	[Correo] [varchar](max) NOT NULL,
	[Contrasenia] [varchar](max) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Apellidos] [varchar](50) NULL,
	[IdRol] [int] NOT NULL,
	[Token] [varchar](max) NULL,
	[Estatus] [varchar](10) NOT NULL,
 CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED 
(
	[IdUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[AlumnoCurso] ON 

INSERT [dbo].[AlumnoCurso] ([IdAlumnoCurso], [IdAlumno], [IdCurso], [IdMaestro], [IdAlumnoCursoEstatus], [Calificacion]) VALUES (1, 2, 1, 1, 3, NULL)
SET IDENTITY_INSERT [dbo].[AlumnoCurso] OFF
SET IDENTITY_INSERT [dbo].[AlumnoCursoEstatus] ON 

INSERT [dbo].[AlumnoCursoEstatus] ([IdAlumnoCursoEstatus], [EstatusCurso]) VALUES (1, N'Completado')
INSERT [dbo].[AlumnoCursoEstatus] ([IdAlumnoCursoEstatus], [EstatusCurso]) VALUES (2, N'En curso')
INSERT [dbo].[AlumnoCursoEstatus] ([IdAlumnoCursoEstatus], [EstatusCurso]) VALUES (3, N'Inscrito')
SET IDENTITY_INSERT [dbo].[AlumnoCursoEstatus] OFF
SET IDENTITY_INSERT [dbo].[AlumnoLeccion] ON 

INSERT [dbo].[AlumnoLeccion] ([IdAlumnoLeccion], [IdAlumnoCurso], [IdLeccion], [Calificacion]) VALUES (1, 1, 1, NULL)
SET IDENTITY_INSERT [dbo].[AlumnoLeccion] OFF
SET IDENTITY_INSERT [dbo].[AlumnoPregunta] ON 

INSERT [dbo].[AlumnoPregunta] ([IdAlumnoPregunta], [IdAlumnoLeccion], [IdPregunta], [IdRespuesta], [Puntos], [Correcto]) VALUES (1, 1, 1, 1, CAST(10.00 AS Decimal(5, 2)), NULL)
INSERT [dbo].[AlumnoPregunta] ([IdAlumnoPregunta], [IdAlumnoLeccion], [IdPregunta], [IdRespuesta], [Puntos], [Correcto]) VALUES (2, 1, 2, 5, CAST(10.00 AS Decimal(5, 2)), NULL)
SET IDENTITY_INSERT [dbo].[AlumnoPregunta] OFF
SET IDENTITY_INSERT [dbo].[Curso] ON 

INSERT [dbo].[Curso] ([IdCurso], [Curso], [Descripcion], [Estatus]) VALUES (1, N'Estrutura de datos', N'Se verán todas las estructuras de datos y algunos algoritmos', N'ACTIVO')
INSERT [dbo].[Curso] ([IdCurso], [Curso], [Descripcion], [Estatus]) VALUES (2, N'Ingeniería del software', N'Todo el proceso', N'ACTIVO')
SET IDENTITY_INSERT [dbo].[Curso] OFF
SET IDENTITY_INSERT [dbo].[Leccion] ON 

INSERT [dbo].[Leccion] ([IdLeccion], [Leccion], [Descripcion], [Contenido], [IdCurso], [PuntajeAprobatorio], [Estatus]) VALUES (1, N'Arreglos y Vectores', N'Ver todo lo relacionado a los vectores', N' ', 1, CAST(60.00 AS Decimal(5, 2)), N'ACTIVO')
SET IDENTITY_INSERT [dbo].[Leccion] OFF
SET IDENTITY_INSERT [dbo].[Pregunta] ON 

INSERT [dbo].[Pregunta] ([IdPregunta], [Pregunta], [IdTipoPregunta], [IdLeccion], [Puntos], [Estatus]) VALUES (1, N'¿Qué es un arreglo?', 2, 1, CAST(10.00 AS Decimal(5, 2)), N'ACTIVO')
INSERT [dbo].[Pregunta] ([IdPregunta], [Pregunta], [IdTipoPregunta], [IdLeccion], [Puntos], [Estatus]) VALUES (2, N'¿Un arreglo es un conjunto de datos?', 1, 1, CAST(10.00 AS Decimal(5, 2)), N'ACTIVO')
SET IDENTITY_INSERT [dbo].[Pregunta] OFF
SET IDENTITY_INSERT [dbo].[Respuesta] ON 

INSERT [dbo].[Respuesta] ([IdRespuesta], [Respuesta], [Correcta], [IdPregunta], [Estatus]) VALUES (1, N'conjunto de datos o una estructura de datos homogéneos que se encuentran ubicados en forma consecutiva en la memoria RAM', 1, 1, N'ACTIVO')
INSERT [dbo].[Respuesta] ([IdRespuesta], [Respuesta], [Correcta], [IdPregunta], [Estatus]) VALUES (2, N'datos homogéneos que se encuentran ubicados en forma consecutiva en la memoria RAM', 0, 1, N'ACTIVO')
INSERT [dbo].[Respuesta] ([IdRespuesta], [Respuesta], [Correcta], [IdPregunta], [Estatus]) VALUES (3, N'datos homogéneos que se encuentran ubicados en forma consecutiva', 0, 1, N'ACTIVO')
INSERT [dbo].[Respuesta] ([IdRespuesta], [Respuesta], [Correcta], [IdPregunta], [Estatus]) VALUES (4, N'Si', 1, 2, N'ACTIVO')
INSERT [dbo].[Respuesta] ([IdRespuesta], [Respuesta], [Correcta], [IdPregunta], [Estatus]) VALUES (5, N'No', 0, 2, N'ACTIVO')
SET IDENTITY_INSERT [dbo].[Respuesta] OFF
SET IDENTITY_INSERT [dbo].[TipoPregunta] ON 

INSERT [dbo].[TipoPregunta] ([IdTipoPregunta], [TipoPregunta], [Descripcion]) VALUES (1, N'Booleana', N'2 Respuestas, 1 es verdadera')
INSERT [dbo].[TipoPregunta] ([IdTipoPregunta], [TipoPregunta], [Descripcion]) VALUES (2, N'Multiple 1 Correcta', N'Opción múltiple donde solo una respuesta es correcta ')
INSERT [dbo].[TipoPregunta] ([IdTipoPregunta], [TipoPregunta], [Descripcion]) VALUES (3, N'Multiple +1 Correcta', N'Opción múltiple donde más de una respuesta es correcta')
INSERT [dbo].[TipoPregunta] ([IdTipoPregunta], [TipoPregunta], [Descripcion]) VALUES (4, N'Multiple +1 Forsosa Correcta', N'Opción múltiple donde más de una respuesta es correcta y todas deben responderse correctamente')
SET IDENTITY_INSERT [dbo].[TipoPregunta] OFF
SET IDENTITY_INSERT [dbo].[Usuario] ON 

INSERT [dbo].[Usuario] ([IdUsuario], [Correo], [Contrasenia], [Nombre], [Apellidos], [IdRol], [Token], [Estatus]) VALUES (1, N'eusebio.puc@gmail.com', N'prueba', N'Eusebio', N'Puc', 2, N'c0ed723e-42f4-4b3b-84a0-198a749b590a', N'ACTIVO')
INSERT [dbo].[Usuario] ([IdUsuario], [Correo], [Contrasenia], [Nombre], [Apellidos], [IdRol], [Token], [Estatus]) VALUES (2, N'alumno@gmail.com', N'prueba', N'Prueba', N'alumno', 3, NULL, N'ACTIVO')
SET IDENTITY_INSERT [dbo].[Usuario] OFF
ALTER TABLE [dbo].[Curso] ADD  CONSTRAINT [DF_Curso_Estatus]  DEFAULT ('ACTIVO') FOR [Estatus]
GO
ALTER TABLE [dbo].[Leccion] ADD  CONSTRAINT [DF_Leccion_Estatus]  DEFAULT ('ACTIVO') FOR [Estatus]
GO
ALTER TABLE [dbo].[Pregunta] ADD  CONSTRAINT [DF_Pregunta_Estatus]  DEFAULT ('ACTIVO') FOR [Estatus]
GO
ALTER TABLE [dbo].[Respuesta] ADD  CONSTRAINT [DF_Respuesta_Estatus]  DEFAULT ('ACTIVO') FOR [Estatus]
GO
ALTER TABLE [dbo].[Usuario] ADD  CONSTRAINT [DF_Usuario_IdRol]  DEFAULT ((1)) FOR [IdRol]
GO
ALTER TABLE [dbo].[Usuario] ADD  CONSTRAINT [DF_Usuario_Estatus]  DEFAULT ('ACTIVO') FOR [Estatus]
GO
ALTER TABLE [dbo].[AlumnoCurso]  WITH CHECK ADD  CONSTRAINT [FK_AlumnoCurso_AlumnoCursoEstatus] FOREIGN KEY([IdAlumnoCursoEstatus])
REFERENCES [dbo].[AlumnoCursoEstatus] ([IdAlumnoCursoEstatus])
GO
ALTER TABLE [dbo].[AlumnoCurso] CHECK CONSTRAINT [FK_AlumnoCurso_AlumnoCursoEstatus]
GO
ALTER TABLE [dbo].[AlumnoCurso]  WITH CHECK ADD  CONSTRAINT [FK_AlumnoCurso_Curso] FOREIGN KEY([IdCurso])
REFERENCES [dbo].[Curso] ([IdCurso])
GO
ALTER TABLE [dbo].[AlumnoCurso] CHECK CONSTRAINT [FK_AlumnoCurso_Curso]
GO
ALTER TABLE [dbo].[AlumnoCurso]  WITH CHECK ADD  CONSTRAINT [FK_AlumnoCurso_Usuario] FOREIGN KEY([IdAlumno])
REFERENCES [dbo].[Usuario] ([IdUsuario])
GO
ALTER TABLE [dbo].[AlumnoCurso] CHECK CONSTRAINT [FK_AlumnoCurso_Usuario]
GO
ALTER TABLE [dbo].[AlumnoCurso]  WITH CHECK ADD  CONSTRAINT [FK_AlumnoCurso_Usuario1] FOREIGN KEY([IdMaestro])
REFERENCES [dbo].[Usuario] ([IdUsuario])
GO
ALTER TABLE [dbo].[AlumnoCurso] CHECK CONSTRAINT [FK_AlumnoCurso_Usuario1]
GO
ALTER TABLE [dbo].[AlumnoLeccion]  WITH CHECK ADD  CONSTRAINT [FK_AlumnoLeccion_AlumnoCurso] FOREIGN KEY([IdAlumnoCurso])
REFERENCES [dbo].[AlumnoCurso] ([IdAlumnoCurso])
GO
ALTER TABLE [dbo].[AlumnoLeccion] CHECK CONSTRAINT [FK_AlumnoLeccion_AlumnoCurso]
GO
ALTER TABLE [dbo].[AlumnoLeccion]  WITH CHECK ADD  CONSTRAINT [FK_AlumnoLeccion_Leccion] FOREIGN KEY([IdLeccion])
REFERENCES [dbo].[Leccion] ([IdLeccion])
GO
ALTER TABLE [dbo].[AlumnoLeccion] CHECK CONSTRAINT [FK_AlumnoLeccion_Leccion]
GO
ALTER TABLE [dbo].[AlumnoPregunta]  WITH CHECK ADD  CONSTRAINT [FK_AlumnoPregunta_AlumnoLeccion] FOREIGN KEY([IdAlumnoLeccion])
REFERENCES [dbo].[AlumnoLeccion] ([IdAlumnoLeccion])
GO
ALTER TABLE [dbo].[AlumnoPregunta] CHECK CONSTRAINT [FK_AlumnoPregunta_AlumnoLeccion]
GO
ALTER TABLE [dbo].[AlumnoPregunta]  WITH CHECK ADD  CONSTRAINT [FK_AlumnoPregunta_Pregunta] FOREIGN KEY([IdPregunta])
REFERENCES [dbo].[Pregunta] ([IdPregunta])
GO
ALTER TABLE [dbo].[AlumnoPregunta] CHECK CONSTRAINT [FK_AlumnoPregunta_Pregunta]
GO
ALTER TABLE [dbo].[AlumnoPregunta]  WITH CHECK ADD  CONSTRAINT [FK_AlumnoPregunta_Respuesta] FOREIGN KEY([IdRespuesta])
REFERENCES [dbo].[Respuesta] ([IdRespuesta])
GO
ALTER TABLE [dbo].[AlumnoPregunta] CHECK CONSTRAINT [FK_AlumnoPregunta_Respuesta]
GO
ALTER TABLE [dbo].[Leccion]  WITH CHECK ADD  CONSTRAINT [FK_Leccion_Curso] FOREIGN KEY([IdCurso])
REFERENCES [dbo].[Curso] ([IdCurso])
GO
ALTER TABLE [dbo].[Leccion] CHECK CONSTRAINT [FK_Leccion_Curso]
GO
ALTER TABLE [dbo].[Pregunta]  WITH CHECK ADD  CONSTRAINT [FK_Pregunta_Leccion] FOREIGN KEY([IdLeccion])
REFERENCES [dbo].[Leccion] ([IdLeccion])
GO
ALTER TABLE [dbo].[Pregunta] CHECK CONSTRAINT [FK_Pregunta_Leccion]
GO
ALTER TABLE [dbo].[Pregunta]  WITH CHECK ADD  CONSTRAINT [FK_Pregunta_TipoPregunta] FOREIGN KEY([IdTipoPregunta])
REFERENCES [dbo].[TipoPregunta] ([IdTipoPregunta])
GO
ALTER TABLE [dbo].[Pregunta] CHECK CONSTRAINT [FK_Pregunta_TipoPregunta]
GO
ALTER TABLE [dbo].[Respuesta]  WITH CHECK ADD  CONSTRAINT [FK_Respuesta_Pregunta] FOREIGN KEY([IdPregunta])
REFERENCES [dbo].[Pregunta] ([IdPregunta])
GO
ALTER TABLE [dbo].[Respuesta] CHECK CONSTRAINT [FK_Respuesta_Pregunta]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Podrían ser en curso, completado, ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AlumnoCurso', @level2type=N'COLUMN',@level2name=N'IdAlumnoCursoEstatus'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'100 es la calificación máxima' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Leccion', @level2type=N'COLUMN',@level2name=N'PuntajeAprobatorio'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1 Admin, 2 Maestro, 3 Alumno' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Usuario', @level2type=N'COLUMN',@level2name=N'IdRol'
GO
USE [master]
GO
ALTER DATABASE [elearning] SET  READ_WRITE 
GO
