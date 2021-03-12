using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000449 RID: 1097
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SafeDeleteExistingFilesDataRedundancyException : SeedPrepareException
	{
		// Token: 0x06002B00 RID: 11008 RVA: 0x000BCA9D File Offset: 0x000BAC9D
		public SafeDeleteExistingFilesDataRedundancyException(string db, string errMsg2) : base(ReplayStrings.SafeDeleteExistingFilesDataRedundancyException(db, errMsg2))
		{
			this.db = db;
			this.errMsg2 = errMsg2;
		}

		// Token: 0x06002B01 RID: 11009 RVA: 0x000BCABF File Offset: 0x000BACBF
		public SafeDeleteExistingFilesDataRedundancyException(string db, string errMsg2, Exception innerException) : base(ReplayStrings.SafeDeleteExistingFilesDataRedundancyException(db, errMsg2), innerException)
		{
			this.db = db;
			this.errMsg2 = errMsg2;
		}

		// Token: 0x06002B02 RID: 11010 RVA: 0x000BCAE4 File Offset: 0x000BACE4
		protected SafeDeleteExistingFilesDataRedundancyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.db = (string)info.GetValue("db", typeof(string));
			this.errMsg2 = (string)info.GetValue("errMsg2", typeof(string));
		}

		// Token: 0x06002B03 RID: 11011 RVA: 0x000BCB39 File Offset: 0x000BAD39
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("db", this.db);
			info.AddValue("errMsg2", this.errMsg2);
		}

		// Token: 0x17000AE6 RID: 2790
		// (get) Token: 0x06002B04 RID: 11012 RVA: 0x000BCB65 File Offset: 0x000BAD65
		public string Db
		{
			get
			{
				return this.db;
			}
		}

		// Token: 0x17000AE7 RID: 2791
		// (get) Token: 0x06002B05 RID: 11013 RVA: 0x000BCB6D File Offset: 0x000BAD6D
		public string ErrMsg2
		{
			get
			{
				return this.errMsg2;
			}
		}

		// Token: 0x04001483 RID: 5251
		private readonly string db;

		// Token: 0x04001484 RID: 5252
		private readonly string errMsg2;
	}
}
