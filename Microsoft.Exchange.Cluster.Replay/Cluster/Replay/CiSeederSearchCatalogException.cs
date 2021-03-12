using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200044F RID: 1103
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CiSeederSearchCatalogException : SeedInProgressException
	{
		// Token: 0x06002B20 RID: 11040 RVA: 0x000BCE62 File Offset: 0x000BB062
		public CiSeederSearchCatalogException(string sourceServer, Guid database, string specificError) : base(ReplayStrings.CiSeederSearchCatalogException(sourceServer, database, specificError))
		{
			this.sourceServer = sourceServer;
			this.database = database;
			this.specificError = specificError;
		}

		// Token: 0x06002B21 RID: 11041 RVA: 0x000BCE8C File Offset: 0x000BB08C
		public CiSeederSearchCatalogException(string sourceServer, Guid database, string specificError, Exception innerException) : base(ReplayStrings.CiSeederSearchCatalogException(sourceServer, database, specificError), innerException)
		{
			this.sourceServer = sourceServer;
			this.database = database;
			this.specificError = specificError;
		}

		// Token: 0x06002B22 RID: 11042 RVA: 0x000BCEB8 File Offset: 0x000BB0B8
		protected CiSeederSearchCatalogException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.sourceServer = (string)info.GetValue("sourceServer", typeof(string));
			this.database = (Guid)info.GetValue("database", typeof(Guid));
			this.specificError = (string)info.GetValue("specificError", typeof(string));
		}

		// Token: 0x06002B23 RID: 11043 RVA: 0x000BCF30 File Offset: 0x000BB130
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("sourceServer", this.sourceServer);
			info.AddValue("database", this.database);
			info.AddValue("specificError", this.specificError);
		}

		// Token: 0x17000AEE RID: 2798
		// (get) Token: 0x06002B24 RID: 11044 RVA: 0x000BCF7D File Offset: 0x000BB17D
		public string SourceServer
		{
			get
			{
				return this.sourceServer;
			}
		}

		// Token: 0x17000AEF RID: 2799
		// (get) Token: 0x06002B25 RID: 11045 RVA: 0x000BCF85 File Offset: 0x000BB185
		public Guid Database
		{
			get
			{
				return this.database;
			}
		}

		// Token: 0x17000AF0 RID: 2800
		// (get) Token: 0x06002B26 RID: 11046 RVA: 0x000BCF8D File Offset: 0x000BB18D
		public string SpecificError
		{
			get
			{
				return this.specificError;
			}
		}

		// Token: 0x0400148B RID: 5259
		private readonly string sourceServer;

		// Token: 0x0400148C RID: 5260
		private readonly Guid database;

		// Token: 0x0400148D RID: 5261
		private readonly string specificError;
	}
}
