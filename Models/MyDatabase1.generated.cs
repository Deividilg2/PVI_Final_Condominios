//---------------------------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated by T4Model template for T4 (https://github.com/linq2db/linq2db).
//    Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
//---------------------------------------------------------------------------------------------------

#pragma warning disable 1573, 1591

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

using LinqToDB;
using LinqToDB.Common;
using LinqToDB.Configuration;
using LinqToDB.Data;
using LinqToDB.Mapping;

namespace DataModels
{
	/// <summary>
	/// Database       : PVI_ProyectoFinal
	/// Data Source    : DEIVIDILG\DEIVIDILG
	/// Server Version : 14.00.2065
	/// </summary>
	public partial class PviProyectoFinalDB : LinqToDB.Data.DataConnection
	{
		public ITable<Bitacora>     Bitacoras      { get { return this.GetTable<Bitacora>(); } }
		public ITable<Casa>         Casas          { get { return this.GetTable<Casa>(); } }
		public ITable<Categoria>    Categorias     { get { return this.GetTable<Categoria>(); } }
		public ITable<Cobro>        Cobros         { get { return this.GetTable<Cobro>(); } }
		public ITable<DetalleCobro> DetalleCobroes { get { return this.GetTable<DetalleCobro>(); } }
		public ITable<Persona>      Personas       { get { return this.GetTable<Persona>(); } }
		public ITable<Servicio>     Servicios      { get { return this.GetTable<Servicio>(); } }

		public PviProyectoFinalDB()
		{
			InitDataContext();
			InitMappingSchema();
		}

		public PviProyectoFinalDB(string configuration)
			: base(configuration)
		{
			InitDataContext();
			InitMappingSchema();
		}

		public PviProyectoFinalDB(DataOptions options)
			: base(options)
		{
			InitDataContext();
			InitMappingSchema();
		}

		public PviProyectoFinalDB(DataOptions<PviProyectoFinalDB> options)
			: base(options.Options)
		{
			InitDataContext();
			InitMappingSchema();
		}

		partial void InitDataContext  ();
		partial void InitMappingSchema();
	}

	[Table(Schema="dbo", Name="Bitacora")]
	public partial class Bitacora
	{
		[Column("id_bitacora"), PrimaryKey, Identity] public int       IdBitacora { get; set; } // int
		[Column("detalle"),     Nullable            ] public string    Detalle    { get; set; } // varchar(255)
		[Column("id_cobro"),    Nullable            ] public int?      IdCobro    { get; set; } // int
		[Column("id_user"),     Nullable            ] public int?      IdUser     { get; set; } // int
		[Column("fecha"),       Nullable            ] public DateTime? Fecha      { get; set; } // datetime

		#region Associations

		/// <summary>
		/// FK__Bitacora__id_cob__4E88ABD4 (dbo.Cobros)
		/// </summary>
		[Association(ThisKey="IdCobro", OtherKey="IdCobro", CanBeNull=true)]
		public Cobro Idcob4E88ABD { get; set; }

		/// <summary>
		/// FK__Bitacora__id_use__4D94879B (dbo.Persona)
		/// </summary>
		[Association(ThisKey="IdUser", OtherKey="IdPersona", CanBeNull=true)]
		public Persona Iduse4D94879B { get; set; }

		#endregion
	}

	[Table(Schema="dbo", Name="Casas")]
	public partial class Casa
	{
		[Column("id_casa"),             PrimaryKey,  Identity] public int      IdCasa             { get; set; } // int
		[Column("nombre_casa"),         NotNull              ] public string   NombreCasa         { get; set; } // nvarchar(255)
		[Column("metros_cuadrados"),    NotNull              ] public int      MetrosCuadrados    { get; set; } // int
		[Column("numero_habitaciones"), NotNull              ] public int      NumeroHabitaciones { get; set; } // int
		[Column("numero_banos"),        NotNull              ] public int      NumeroBanos        { get; set; } // int
		[Column("precio"),              NotNull              ] public decimal  Precio             { get; set; } // decimal(15, 2)
		[Column("id_persona"),          NotNull              ] public int      IdPersona          { get; set; } // int
		[Column("fecha_construccion"),  NotNull              ] public DateTime FechaConstruccion  { get; set; } // date
		[Column("estado"),                 Nullable          ] public bool?    Estado             { get; set; } // bit

		#region Associations

		/// <summary>
		/// FK__Cobros__id_casa__46E78A0C_BackReference (dbo.Cobros)
		/// </summary>
		[Association(ThisKey="IdCasa", OtherKey="IdCasa", CanBeNull=true)]
		public IEnumerable<Cobro> Cobrosidcasa46E78A0C { get; set; }

		/// <summary>
		/// FK__Casas__id_person__440B1D61 (dbo.Persona)
		/// </summary>
		[Association(ThisKey="IdPersona", OtherKey="IdPersona", CanBeNull=false)]
		public Persona Idperson440B1D { get; set; }

		#endregion
	}

	[Table(Schema="dbo", Name="Categorias")]
	public partial class Categoria
	{
		[Column("id_categoria"), PrimaryKey,  Identity] public int    IdCategoria { get; set; } // int
		[Column("nombre"),       NotNull              ] public string Nombre      { get; set; } // varchar(100)
		[Column("descripcion"),     Nullable          ] public string Descripcion { get; set; } // text
		[Column("estado"),       NotNull              ] public bool   Estado      { get; set; } // bit

		#region Associations

		/// <summary>
		/// FK__Servicios__id_ca__3F466844_BackReference (dbo.Servicios)
		/// </summary>
		[Association(ThisKey="IdCategoria", OtherKey="IdCategoria", CanBeNull=true)]
		public IEnumerable<Servicio> Serviciosidca3F { get; set; }

		#endregion
	}

	[Table(Schema="dbo", Name="Cobros")]
	public partial class Cobro
	{
		[Column("id_cobro"),     PrimaryKey,  Identity] public int       IdCobro     { get; set; } // int
		[Column("id_casa"),      NotNull              ] public int       IdCasa      { get; set; } // int
		[Column("mes"),          NotNull              ] public int       Mes         { get; set; } // int
		[Column("anno"),         NotNull              ] public int       Anno        { get; set; } // int
		[Column("estado"),          Nullable          ] public string    Estado      { get; set; } // varchar(50)
		[Column("monto"),           Nullable          ] public decimal?  Monto       { get; set; } // decimal(15, 2)
		[Column("fecha_pagada"),    Nullable          ] public DateTime? FechaPagada { get; set; } // date

		#region Associations

		/// <summary>
		/// FK__Bitacora__id_cob__4E88ABD4_BackReference (dbo.Bitacora)
		/// </summary>
		[Association(ThisKey="IdCobro", OtherKey="IdCobro", CanBeNull=true)]
		public IEnumerable<Bitacora> Bitacoraidcob4E88Abds { get; set; }

		/// <summary>
		/// FK__DetalleCo__id_co__4AB81AF0_BackReference (dbo.DetalleCobro)
		/// </summary>
		[Association(ThisKey="IdCobro", OtherKey="IdCobro", CanBeNull=true)]
		public IEnumerable<DetalleCobro> DetalleCoidco4AB81Afs { get; set; }

		/// <summary>
		/// FK__Cobros__id_casa__46E78A0C (dbo.Casas)
		/// </summary>
		[Association(ThisKey="IdCasa", OtherKey="IdCasa", CanBeNull=false)]
		public Casa Idcasa46E78A0C { get; set; }

		#endregion
	}

	[Table(Schema="dbo", Name="DetalleCobro")]
	public partial class DetalleCobro
	{
		[Column("id_servicio"), PrimaryKey(1), NotNull] public int IdServicio { get; set; } // int
		[Column("id_cobro"),    PrimaryKey(2), NotNull] public int IdCobro    { get; set; } // int

		#region Associations

		/// <summary>
		/// FK__DetalleCo__id_co__4AB81AF0 (dbo.Cobros)
		/// </summary>
		[Association(ThisKey="IdCobro", OtherKey="IdCobro", CanBeNull=false)]
		public Cobro DetalleCoidco4AB81AF { get; set; }

		/// <summary>
		/// FK__DetalleCo__id_se__49C3F6B7 (dbo.Servicios)
		/// </summary>
		[Association(ThisKey="IdServicio", OtherKey="IdServicio", CanBeNull=false)]
		public Servicio DetalleCoidse49C3F6B { get; set; }

		#endregion
	}

	[Table(Schema="dbo", Name="Persona")]
	public partial class Persona
	{
		[Column("id_persona"),       PrimaryKey,  Identity] public int       IdPersona       { get; set; } // int
		[Column("nombre"),           NotNull              ] public string    Nombre          { get; set; } // varchar(100)
		[Column("apellido"),         NotNull              ] public string    Apellido        { get; set; } // varchar(100)
		[Column("email"),            NotNull              ] public string    Email           { get; set; } // varchar(150)
		[Column("telefono"),            Nullable          ] public string    Telefono        { get; set; } // varchar(15)
		[Column("direccion"),           Nullable          ] public string    Direccion       { get; set; } // varchar(255)
		[Column("fecha_nacimiento"),    Nullable          ] public DateTime? FechaNacimiento { get; set; } // date
		[Column("contrasena"),          Nullable          ] public string    Contrasena      { get; set; } // varchar(255)
		[Column("estado"),              Nullable          ] public bool?     Estado          { get; set; } // bit
		[Column("tipo_persona"),        Nullable          ] public string    TipoPersona     { get; set; } // varchar(50)

		#region Associations

		/// <summary>
		/// FK__Bitacora__id_use__4D94879B_BackReference (dbo.Bitacora)
		/// </summary>
		[Association(ThisKey="IdPersona", OtherKey="IdUser", CanBeNull=true)]
		public IEnumerable<Bitacora> Bitacoraiduse4D94879B { get; set; }

		/// <summary>
		/// FK__Casas__id_person__440B1D61_BackReference (dbo.Casas)
		/// </summary>
		[Association(ThisKey="IdPersona", OtherKey="IdPersona", CanBeNull=true)]
		public IEnumerable<Casa> Casasidperson440B1D { get; set; }

		#endregion
	}

	[Table(Schema="dbo", Name="Servicios")]
	public partial class Servicio
	{
		[Column("id_servicio"),  PrimaryKey,  Identity] public int     IdServicio  { get; set; } // int
		[Column("nombre"),       NotNull              ] public string  Nombre      { get; set; } // varchar(100)
		[Column("descripcion"),     Nullable          ] public string  Descripcion { get; set; } // text
		[Column("precio"),       NotNull              ] public decimal Precio      { get; set; } // decimal(10, 2)
		[Column("id_categoria"), NotNull              ] public int     IdCategoria { get; set; } // int
		[Column("estado"),       NotNull              ] public bool    Estado      { get; set; } // bit

		#region Associations

		/// <summary>
		/// FK__DetalleCo__id_se__49C3F6B7_BackReference (dbo.DetalleCobro)
		/// </summary>
		[Association(ThisKey="IdServicio", OtherKey="IdServicio", CanBeNull=true)]
		public IEnumerable<DetalleCobro> DetalleCoidse49C3F6B { get; set; }

		/// <summary>
		/// FK__Servicios__id_ca__3F466844 (dbo.Categorias)
		/// </summary>
		[Association(ThisKey="IdCategoria", OtherKey="IdCategoria", CanBeNull=false)]
		public Categoria Idca3F { get; set; }

		#endregion
	}

	public static partial class PviProyectoFinalDBStoredProcedures
	{
		#region SpAlterdiagram

		public static int SpAlterdiagram(this PviProyectoFinalDB dataConnection, string @diagramname, int? @ownerId, int? @version, byte[] @definition)
		{
			var parameters = new []
			{
				new DataParameter("@diagramname", @diagramname, LinqToDB.DataType.NVarChar)
				{
					Size = 128
				},
				new DataParameter("@owner_id",    @ownerId,     LinqToDB.DataType.Int32),
				new DataParameter("@version",     @version,     LinqToDB.DataType.Int32),
				new DataParameter("@definition",  @definition,  LinqToDB.DataType.VarBinary)
				{
					Size = -1
				}
			};

			return dataConnection.ExecuteProc("[dbo].[sp_alterdiagram]", parameters);
		}

		#endregion

		#region SpConsultarBitacora

		public static IEnumerable<Bitacora> SpConsultarBitacora(this PviProyectoFinalDB dataConnection)
		{
			return dataConnection.QueryProc<Bitacora>("[dbo].[sp_ConsultarBitacora]");
		}

		#endregion

		#region SpConsultarCasaddl

		public static IEnumerable<SpConsultarCasaddlResult> SpConsultarCasaddl(this PviProyectoFinalDB dataConnection, int? @idPersona)
		{
			var parameters = new []
			{
				new DataParameter("@idPersona", @idPersona, LinqToDB.DataType.Int32)
			};

			return dataConnection.QueryProc<SpConsultarCasaddlResult>("[dbo].[sp_ConsultarCasaddl]", parameters);
		}

		public partial class SpConsultarCasaddlResult
		{
			[Column("id_casa")    ] public int    Id_casa     { get; set; }
			[Column("nombre_casa")] public string Nombre_casa { get; set; }
			[Column("estado")     ] public bool?  Estado      { get; set; }
		}

		#endregion

		#region SpConsultarCategorias

		public static IEnumerable<Categoria> SpConsultarCategorias(this PviProyectoFinalDB dataConnection)
		{
			return dataConnection.QueryProc<Categoria>("[dbo].[sp_ConsultarCategorias]");
		}

		#endregion

		#region SpConsultarCobrodeCasaporId

		public static IEnumerable<SpConsultarCobrodeCasaporIdResult> SpConsultarCobrodeCasaporId(this PviProyectoFinalDB dataConnection, int? @Idcasa, int? @mes, int? @anno)
		{
			var parameters = new []
			{
				new DataParameter("@Idcasa", @Idcasa, LinqToDB.DataType.Int32),
				new DataParameter("@mes",    @mes,    LinqToDB.DataType.Int32),
				new DataParameter("@anno",   @anno,   LinqToDB.DataType.Int32)
			};

			return dataConnection.QueryProc<SpConsultarCobrodeCasaporIdResult>("[dbo].[sp_ConsultarCobrodeCasaporId]", parameters);
		}

		public partial class SpConsultarCobrodeCasaporIdResult
		{
			[Column("estado")] public string Estado { get; set; }
		}

		#endregion

		#region SpConsultarCobroporId

		public static IEnumerable<SpConsultarCobroporIdResult> SpConsultarCobroporId(this PviProyectoFinalDB dataConnection, int? @idCobro)
		{
			var parameters = new []
			{
				new DataParameter("@idCobro", @idCobro, LinqToDB.DataType.Int32)
			};

			return dataConnection.QueryProc<SpConsultarCobroporIdResult>("[dbo].[sp_ConsultarCobroporId]", parameters);
		}

		public partial class SpConsultarCobroporIdResult
		{
			[Column("id_cobro")  ] public int      Id_cobro   { get; set; }
			[Column("id_casa")   ] public int      Id_casa    { get; set; }
			[Column("mes")       ] public int      Mes        { get; set; }
			[Column("anno")      ] public int      Anno       { get; set; }
			[Column("estado")    ] public string   Estado     { get; set; }
			[Column("monto")     ] public decimal? Monto      { get; set; }
			[Column("id_persona")] public int      Id_persona { get; set; }
			[Column("nombre")    ] public string   Nombre     { get; set; }
		}

		#endregion

		#region SpConsultarCobros

		public static IEnumerable<SpConsultarCobrosResult> SpConsultarCobros(this PviProyectoFinalDB dataConnection)
		{
			return dataConnection.QueryProc<SpConsultarCobrosResult>("[dbo].[sp_ConsultarCobros]");
		}

		public partial class SpConsultarCobrosResult
		{
			[Column("id_cobro")    ] public int       Id_cobro     { get; set; }
			[Column("id_casa")     ] public int       Id_casa      { get; set; }
			[Column("nombre")      ] public string    Nombre       { get; set; }
			[Column("mes")         ] public int       Mes          { get; set; }
			[Column("anno")        ] public int       Anno         { get; set; }
			[Column("estado")      ] public string    Estado       { get; set; }
			[Column("monto")       ] public decimal?  Monto        { get; set; }
			[Column("fecha_pagada")] public DateTime? Fecha_pagada { get; set; }
		}

		#endregion

		#region SpConsultarDetalleCobro

		public static IEnumerable<DetalleCobro> SpConsultarDetalleCobro(this PviProyectoFinalDB dataConnection)
		{
			return dataConnection.QueryProc<DetalleCobro>("[dbo].[sp_ConsultarDetalleCobro]");
		}

		#endregion

		#region SpConsultarEstadoCasaporPersona

		public static IEnumerable<SpConsultarEstadoCasaporPersonaResult> SpConsultarEstadoCasaporPersona(this PviProyectoFinalDB dataConnection, int? @IdCasa, int? @mes, int? @anno)
		{
			var parameters = new []
			{
				new DataParameter("@IdCasa", @IdCasa, LinqToDB.DataType.Int32),
				new DataParameter("@mes",    @mes,    LinqToDB.DataType.Int32),
				new DataParameter("@anno",   @anno,   LinqToDB.DataType.Int32)
			};

			return dataConnection.QueryProc<SpConsultarEstadoCasaporPersonaResult>("[dbo].[sp_ConsultarEstadoCasaporPersona]", parameters);
		}

		public partial class SpConsultarEstadoCasaporPersonaResult
		{
			[Column("estado")] public string Estado { get; set; }
		}

		#endregion

		#region SpConsultarPersona

		public static IEnumerable<Persona> SpConsultarPersona(this PviProyectoFinalDB dataConnection)
		{
			return dataConnection.QueryProc<Persona>("[dbo].[sp_ConsultarPersona]");
		}

		#endregion

		#region SpConsultarPersonaporId

		public static IEnumerable<SpConsultarPersonaporIdResult> SpConsultarPersonaporId(this PviProyectoFinalDB dataConnection, int? @idPersona)
		{
			var parameters = new []
			{
				new DataParameter("@idPersona", @idPersona, LinqToDB.DataType.Int32)
			};

			return dataConnection.QueryProc<SpConsultarPersonaporIdResult>("[dbo].[sp_ConsultarPersonaporId]", parameters);
		}

		public partial class SpConsultarPersonaporIdResult
		{
			[Column("id_persona")  ] public int    Id_persona   { get; set; }
			[Column("nombre")      ] public string Nombre       { get; set; }
			[Column("apellido")    ] public string Apellido     { get; set; }
			[Column("email")       ] public string Email        { get; set; }
			[Column("telefono")    ] public string Telefono     { get; set; }
			[Column("direccion")   ] public string Direccion    { get; set; }
			[Column("tipo_persona")] public string Tipo_persona { get; set; }
		}

		#endregion

		#region SpConsultarPrecioServicioporId

		public static IEnumerable<SpConsultarPrecioServicioporIdResult> SpConsultarPrecioServicioporId(this PviProyectoFinalDB dataConnection, int? @IdServicio)
		{
			var parameters = new []
			{
				new DataParameter("@IdServicio", @IdServicio, LinqToDB.DataType.Int32)
			};

			return dataConnection.QueryProc<SpConsultarPrecioServicioporIdResult>("[dbo].[sp_ConsultarPrecioServicioporId]", parameters);
		}

		public partial class SpConsultarPrecioServicioporIdResult
		{
			[Column("precio")] public decimal Precio { get; set; }
		}

		#endregion

		#region SpConsultarServicios

		public static IEnumerable<SpConsultarServiciosResult> SpConsultarServicios(this PviProyectoFinalDB dataConnection)
		{
			var ms = dataConnection.MappingSchema;

			return dataConnection.QueryProc(dataReader =>
				new SpConsultarServiciosResult
				{
					Id_servicio  = Converter.ChangeTypeTo<int>    (dataReader.GetValue(0), ms),
					Nombre       = Converter.ChangeTypeTo<string> (dataReader.GetValue(1), ms),
					Precio       = Converter.ChangeTypeTo<decimal>(dataReader.GetValue(2), ms),
					Id_categoria = Converter.ChangeTypeTo<int>    (dataReader.GetValue(3), ms),
					Column5      = Converter.ChangeTypeTo<string> (dataReader.GetValue(4), ms),
				},
				"[dbo].[sp_ConsultarServicios]");
		}

		public partial class SpConsultarServiciosResult
		{
			[Column("id_servicio") ] public int     Id_servicio  { get; set; }
			[Column("nombre")      ] public string  Nombre       { get; set; }
			[Column("precio")      ] public decimal Precio       { get; set; }
			[Column("id_categoria")] public int     Id_categoria { get; set; }
			[Column("nombre")      ] public string  Column5      { get; set; }
		}

		#endregion

		#region SpConsultarServicioscbx

		public static IEnumerable<SpConsultarServicioscbxResult> SpConsultarServicioscbx(this PviProyectoFinalDB dataConnection)
		{
			var ms = dataConnection.MappingSchema;

			return dataConnection.QueryProc(dataReader =>
				new SpConsultarServicioscbxResult
				{
					Id_servicio  = Converter.ChangeTypeTo<int>   (dataReader.GetValue(0), ms),
					Id_categoria = Converter.ChangeTypeTo<int>   (dataReader.GetValue(1), ms),
					Nombre       = Converter.ChangeTypeTo<string>(dataReader.GetValue(2), ms),
					Column4      = Converter.ChangeTypeTo<string>(dataReader.GetValue(3), ms),
				},
				"[dbo].[sp_ConsultarServicioscbx]");
		}

		public partial class SpConsultarServicioscbxResult
		{
			[Column("id_servicio") ] public int    Id_servicio  { get; set; }
			[Column("id_categoria")] public int    Id_categoria { get; set; }
			[Column("nombre")      ] public string Nombre       { get; set; }
			[Column("nombre")      ] public string Column4      { get; set; }
		}

		#endregion

		#region SpConsultarServiciosporCobro

		public static IEnumerable<SpConsultarServiciosporCobroResult> SpConsultarServiciosporCobro(this PviProyectoFinalDB dataConnection, int? @idcobro)
		{
			var parameters = new []
			{
				new DataParameter("@idcobro", @idcobro, LinqToDB.DataType.Int32)
			};

			return dataConnection.QueryProc<SpConsultarServiciosporCobroResult>("[dbo].[sp_ConsultarServiciosporCobro]", parameters);
		}

		public partial class SpConsultarServiciosporCobroResult
		{
			[Column("id_servicio")] public int Id_servicio { get; set; }
		}

		#endregion

		#region SpCreatediagram

		public static int SpCreatediagram(this PviProyectoFinalDB dataConnection, string @diagramname, int? @ownerId, int? @version, byte[] @definition)
		{
			var parameters = new []
			{
				new DataParameter("@diagramname", @diagramname, LinqToDB.DataType.NVarChar)
				{
					Size = 128
				},
				new DataParameter("@owner_id",    @ownerId,     LinqToDB.DataType.Int32),
				new DataParameter("@version",     @version,     LinqToDB.DataType.Int32),
				new DataParameter("@definition",  @definition,  LinqToDB.DataType.VarBinary)
				{
					Size = -1
				}
			};

			return dataConnection.ExecuteProc("[dbo].[sp_creatediagram]", parameters);
		}

		#endregion

		#region SpDropdiagram

		public static int SpDropdiagram(this PviProyectoFinalDB dataConnection, string @diagramname, int? @ownerId)
		{
			var parameters = new []
			{
				new DataParameter("@diagramname", @diagramname, LinqToDB.DataType.NVarChar)
				{
					Size = 128
				},
				new DataParameter("@owner_id",    @ownerId,     LinqToDB.DataType.Int32)
			};

			return dataConnection.ExecuteProc("[dbo].[sp_dropdiagram]", parameters);
		}

		#endregion

		#region SpHelpdiagramdefinition

		public static IEnumerable<SpHelpdiagramdefinitionResult> SpHelpdiagramdefinition(this PviProyectoFinalDB dataConnection, string @diagramname, int? @ownerId)
		{
			var parameters = new []
			{
				new DataParameter("@diagramname", @diagramname, LinqToDB.DataType.NVarChar)
				{
					Size = 128
				},
				new DataParameter("@owner_id",    @ownerId,     LinqToDB.DataType.Int32)
			};

			return dataConnection.QueryProc<SpHelpdiagramdefinitionResult>("[dbo].[sp_helpdiagramdefinition]", parameters);
		}

		public partial class SpHelpdiagramdefinitionResult
		{
			[Column("version")   ] public int?   Version    { get; set; }
			[Column("definition")] public byte[] Definition { get; set; }
		}

		#endregion

		#region SpHelpdiagrams

		public static IEnumerable<SpHelpdiagramsResult> SpHelpdiagrams(this PviProyectoFinalDB dataConnection, string @diagramname, int? @ownerId)
		{
			var parameters = new []
			{
				new DataParameter("@diagramname", @diagramname, LinqToDB.DataType.NVarChar)
				{
					Size = 128
				},
				new DataParameter("@owner_id",    @ownerId,     LinqToDB.DataType.Int32)
			};

			return dataConnection.QueryProc<SpHelpdiagramsResult>("[dbo].[sp_helpdiagrams]", parameters);
		}

		public partial class SpHelpdiagramsResult
		{
			public string Database { get; set; }
			public string Name     { get; set; }
			public int    ID       { get; set; }
			public string Owner    { get; set; }
			public int    OwnerID  { get; set; }
		}

		#endregion

		#region SpInsertarCasa

		public static int SpInsertarCasa(this PviProyectoFinalDB dataConnection, string @nombreCasa, double? @metrosCuadrados, int? @numeroHabitaciones, int? @numeroBanos, decimal? @precio, int? @idPersona, DateTime? @fechaConstruccion, bool? @estado)
		{
			var parameters = new []
			{
				new DataParameter("@nombre_casa",         @nombreCasa,         LinqToDB.DataType.NVarChar)
				{
					Size = 255
				},
				new DataParameter("@metros_cuadrados",    @metrosCuadrados,    LinqToDB.DataType.Double),
				new DataParameter("@numero_habitaciones", @numeroHabitaciones, LinqToDB.DataType.Int32),
				new DataParameter("@numero_banos",        @numeroBanos,        LinqToDB.DataType.Int32),
				new DataParameter("@precio",              @precio,             LinqToDB.DataType.Decimal),
				new DataParameter("@id_persona",          @idPersona,          LinqToDB.DataType.Int32),
				new DataParameter("@fecha_construccion",  @fechaConstruccion,  LinqToDB.DataType.Date),
				new DataParameter("@estado",              @estado,             LinqToDB.DataType.Boolean)
			};

			return dataConnection.ExecuteProc("[dbo].[sp_InsertarCasa]", parameters);
		}

		#endregion

		#region SpInsertarCobro

		public static int SpInsertarCobro(this PviProyectoFinalDB dataConnection, int? @idCasa, int? @mes, int? @anno, decimal? @monto)
		{
			var parameters = new []
			{
				new DataParameter("@id_casa", @idCasa, LinqToDB.DataType.Int32),
				new DataParameter("@mes",     @mes,    LinqToDB.DataType.Int32),
				new DataParameter("@anno",    @anno,   LinqToDB.DataType.Int32),
				new DataParameter("@monto",   @monto,  LinqToDB.DataType.Decimal)
			};

			return dataConnection.ExecuteProc("[dbo].[sp_InsertarCobro]", parameters);
		}

		#endregion

		#region SpInsertarDetalleCobro

		public static int SpInsertarDetalleCobro(this PviProyectoFinalDB dataConnection, int? @IdServicio, int? @IdCobro, int? @IdCasa, int? @mes, int? @anno, decimal? @monto)
		{
			var parameters = new []
			{
				new DataParameter("@IdServicio", @IdServicio, LinqToDB.DataType.Int32),
				new DataParameter("@IdCobro",    @IdCobro,    LinqToDB.DataType.Int32),
				new DataParameter("@IdCasa",     @IdCasa,     LinqToDB.DataType.Int32),
				new DataParameter("@mes",        @mes,        LinqToDB.DataType.Int32),
				new DataParameter("@anno",       @anno,       LinqToDB.DataType.Int32),
				new DataParameter("@monto",      @monto,      LinqToDB.DataType.Decimal)
			};

			return dataConnection.ExecuteProc("[dbo].[sp_InsertarDetalleCobro]", parameters);
		}

		#endregion

		#region SpModificarCasa

		public static int SpModificarCasa(this PviProyectoFinalDB dataConnection, int? @idCasa, string @nombreCasa, double? @metrosCuadrados, int? @numeroHabitaciones, int? @numeroBanos, decimal? @precio, int? @idPersona, DateTime? @fechaConstruccion, bool? @estado)
		{
			var parameters = new []
			{
				new DataParameter("@id_casa",             @idCasa,             LinqToDB.DataType.Int32),
				new DataParameter("@nombre_casa",         @nombreCasa,         LinqToDB.DataType.NVarChar)
				{
					Size = 255
				},
				new DataParameter("@metros_cuadrados",    @metrosCuadrados,    LinqToDB.DataType.Double),
				new DataParameter("@numero_habitaciones", @numeroHabitaciones, LinqToDB.DataType.Int32),
				new DataParameter("@numero_banos",        @numeroBanos,        LinqToDB.DataType.Int32),
				new DataParameter("@precio",              @precio,             LinqToDB.DataType.Decimal),
				new DataParameter("@id_persona",          @idPersona,          LinqToDB.DataType.Int32),
				new DataParameter("@fecha_construccion",  @fechaConstruccion,  LinqToDB.DataType.Date),
				new DataParameter("@estado",              @estado,             LinqToDB.DataType.Boolean)
			};

			return dataConnection.ExecuteProc("[dbo].[sp_ModificarCasa]", parameters);
		}

		#endregion

		#region SpModificarCobro

		public static int SpModificarCobro(this PviProyectoFinalDB dataConnection, int? @idCobro, int? @idCasa, int? @mes, int? @anno, string @estado, decimal? @monto)
		{
			var parameters = new []
			{
				new DataParameter("@id_cobro", @idCobro, LinqToDB.DataType.Int32),
				new DataParameter("@id_casa",  @idCasa,  LinqToDB.DataType.Int32),
				new DataParameter("@mes",      @mes,     LinqToDB.DataType.Int32),
				new DataParameter("@anno",     @anno,    LinqToDB.DataType.Int32),
				new DataParameter("@estado",   @estado,  LinqToDB.DataType.VarChar)
				{
					Size = 50
				},
				new DataParameter("@monto",    @monto,   LinqToDB.DataType.Decimal)
			};

			return dataConnection.ExecuteProc("[dbo].[sp_ModificarCobro]", parameters);
		}

		#endregion

		#region SpRenamediagram

		public static int SpRenamediagram(this PviProyectoFinalDB dataConnection, string @diagramname, int? @ownerId, string @newDiagramname)
		{
			var parameters = new []
			{
				new DataParameter("@diagramname",     @diagramname,    LinqToDB.DataType.NVarChar)
				{
					Size = 128
				},
				new DataParameter("@owner_id",        @ownerId,        LinqToDB.DataType.Int32),
				new DataParameter("@new_diagramname", @newDiagramname, LinqToDB.DataType.NVarChar)
				{
					Size = 128
				}
			};

			return dataConnection.ExecuteProc("[dbo].[sp_renamediagram]", parameters);
		}

		#endregion

		#region SpUpgraddiagrams

		public static int SpUpgraddiagrams(this PviProyectoFinalDB dataConnection)
		{
			return dataConnection.ExecuteProc("[dbo].[sp_upgraddiagrams]");
		}

		#endregion
	}

	public static partial class SqlFunctions
	{
		#region FnDiagramobjects

		[Sql.Function(Name="[dbo].[fn_diagramobjects]", ServerSideOnly=true)]
		public static int? FnDiagramobjects()
		{
			throw new InvalidOperationException();
		}

		#endregion
	}

	public static partial class TableExtensions
	{
		public static Bitacora Find(this ITable<Bitacora> table, int IdBitacora)
		{
			return table.FirstOrDefault(t =>
				t.IdBitacora == IdBitacora);
		}

		public static Casa Find(this ITable<Casa> table, int IdCasa)
		{
			return table.FirstOrDefault(t =>
				t.IdCasa == IdCasa);
		}

		public static Categoria Find(this ITable<Categoria> table, int IdCategoria)
		{
			return table.FirstOrDefault(t =>
				t.IdCategoria == IdCategoria);
		}

		public static Cobro Find(this ITable<Cobro> table, int IdCobro)
		{
			return table.FirstOrDefault(t =>
				t.IdCobro == IdCobro);
		}

		public static DetalleCobro Find(this ITable<DetalleCobro> table, int IdServicio, int IdCobro)
		{
			return table.FirstOrDefault(t =>
				t.IdServicio == IdServicio &&
				t.IdCobro    == IdCobro);
		}

		public static Persona Find(this ITable<Persona> table, int IdPersona)
		{
			return table.FirstOrDefault(t =>
				t.IdPersona == IdPersona);
		}

		public static Servicio Find(this ITable<Servicio> table, int IdServicio)
		{
			return table.FirstOrDefault(t =>
				t.IdServicio == IdServicio);
		}
	}
}
