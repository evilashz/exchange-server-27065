using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020000E3 RID: 227
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ServerForDatabaseNotFoundException : AmServerTransientException
	{
		// Token: 0x060012F7 RID: 4855 RVA: 0x000687B0 File Offset: 0x000669B0
		public ServerForDatabaseNotFoundException(string dbName, string databaseGuid) : base(ServerStrings.ServerForDatabaseNotFound(dbName, databaseGuid))
		{
			this.dbName = dbName;
			this.databaseGuid = databaseGuid;
		}

		// Token: 0x060012F8 RID: 4856 RVA: 0x000687D2 File Offset: 0x000669D2
		public ServerForDatabaseNotFoundException(string dbName, string databaseGuid, Exception innerException) : base(ServerStrings.ServerForDatabaseNotFound(dbName, databaseGuid), innerException)
		{
			this.dbName = dbName;
			this.databaseGuid = databaseGuid;
		}

		// Token: 0x060012F9 RID: 4857 RVA: 0x000687F8 File Offset: 0x000669F8
		protected ServerForDatabaseNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dbName = (string)info.GetValue("dbName", typeof(string));
			this.databaseGuid = (string)info.GetValue("databaseGuid", typeof(string));
		}

		// Token: 0x060012FA RID: 4858 RVA: 0x0006884D File Offset: 0x00066A4D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dbName", this.dbName);
			info.AddValue("databaseGuid", this.databaseGuid);
		}

		// Token: 0x1700065A RID: 1626
		// (get) Token: 0x060012FB RID: 4859 RVA: 0x00068879 File Offset: 0x00066A79
		public string DbName
		{
			get
			{
				return this.dbName;
			}
		}

		// Token: 0x1700065B RID: 1627
		// (get) Token: 0x060012FC RID: 4860 RVA: 0x00068881 File Offset: 0x00066A81
		public string DatabaseGuid
		{
			get
			{
				return this.databaseGuid;
			}
		}

		// Token: 0x04000972 RID: 2418
		private readonly string dbName;

		// Token: 0x04000973 RID: 2419
		private readonly string databaseGuid;
	}
}
