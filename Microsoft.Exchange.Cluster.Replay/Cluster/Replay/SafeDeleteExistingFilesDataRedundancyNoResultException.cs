using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200044A RID: 1098
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SafeDeleteExistingFilesDataRedundancyNoResultException : SeedPrepareException
	{
		// Token: 0x06002B06 RID: 11014 RVA: 0x000BCB75 File Offset: 0x000BAD75
		public SafeDeleteExistingFilesDataRedundancyNoResultException(string db) : base(ReplayStrings.SafeDeleteExistingFilesDataRedundancyNoResultException(db))
		{
			this.db = db;
		}

		// Token: 0x06002B07 RID: 11015 RVA: 0x000BCB8F File Offset: 0x000BAD8F
		public SafeDeleteExistingFilesDataRedundancyNoResultException(string db, Exception innerException) : base(ReplayStrings.SafeDeleteExistingFilesDataRedundancyNoResultException(db), innerException)
		{
			this.db = db;
		}

		// Token: 0x06002B08 RID: 11016 RVA: 0x000BCBAA File Offset: 0x000BADAA
		protected SafeDeleteExistingFilesDataRedundancyNoResultException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.db = (string)info.GetValue("db", typeof(string));
		}

		// Token: 0x06002B09 RID: 11017 RVA: 0x000BCBD4 File Offset: 0x000BADD4
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("db", this.db);
		}

		// Token: 0x17000AE8 RID: 2792
		// (get) Token: 0x06002B0A RID: 11018 RVA: 0x000BCBEF File Offset: 0x000BADEF
		public string Db
		{
			get
			{
				return this.db;
			}
		}

		// Token: 0x04001485 RID: 5253
		private readonly string db;
	}
}
