using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020000E4 RID: 228
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class UnableToFindServerForDatabaseException : ObjectNotFoundException
	{
		// Token: 0x060012FD RID: 4861 RVA: 0x00068889 File Offset: 0x00066A89
		public UnableToFindServerForDatabaseException(string dbName, string databaseGuid) : base(ServerStrings.ServerForDatabaseNotFound(dbName, databaseGuid))
		{
			this.dbName = dbName;
			this.databaseGuid = databaseGuid;
		}

		// Token: 0x060012FE RID: 4862 RVA: 0x000688A6 File Offset: 0x00066AA6
		public UnableToFindServerForDatabaseException(string dbName, string databaseGuid, Exception innerException) : base(ServerStrings.ServerForDatabaseNotFound(dbName, databaseGuid), innerException)
		{
			this.dbName = dbName;
			this.databaseGuid = databaseGuid;
		}

		// Token: 0x060012FF RID: 4863 RVA: 0x000688C4 File Offset: 0x00066AC4
		protected UnableToFindServerForDatabaseException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dbName = (string)info.GetValue("dbName", typeof(string));
			this.databaseGuid = (string)info.GetValue("databaseGuid", typeof(string));
		}

		// Token: 0x06001300 RID: 4864 RVA: 0x00068919 File Offset: 0x00066B19
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dbName", this.dbName);
			info.AddValue("databaseGuid", this.databaseGuid);
		}

		// Token: 0x1700065C RID: 1628
		// (get) Token: 0x06001301 RID: 4865 RVA: 0x00068945 File Offset: 0x00066B45
		public string DbName
		{
			get
			{
				return this.dbName;
			}
		}

		// Token: 0x1700065D RID: 1629
		// (get) Token: 0x06001302 RID: 4866 RVA: 0x0006894D File Offset: 0x00066B4D
		public string DatabaseGuid
		{
			get
			{
				return this.databaseGuid;
			}
		}

		// Token: 0x04000974 RID: 2420
		private readonly string dbName;

		// Token: 0x04000975 RID: 2421
		private readonly string databaseGuid;
	}
}
