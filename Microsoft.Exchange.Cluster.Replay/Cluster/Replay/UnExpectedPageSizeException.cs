using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020004A6 RID: 1190
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UnExpectedPageSizeException : TransientException
	{
		// Token: 0x06002CF7 RID: 11511 RVA: 0x000C05CC File Offset: 0x000BE7CC
		public UnExpectedPageSizeException(string db, long pageSize) : base(ReplayStrings.UnExpectedPageSize(db, pageSize))
		{
			this.db = db;
			this.pageSize = pageSize;
		}

		// Token: 0x06002CF8 RID: 11512 RVA: 0x000C05E9 File Offset: 0x000BE7E9
		public UnExpectedPageSizeException(string db, long pageSize, Exception innerException) : base(ReplayStrings.UnExpectedPageSize(db, pageSize), innerException)
		{
			this.db = db;
			this.pageSize = pageSize;
		}

		// Token: 0x06002CF9 RID: 11513 RVA: 0x000C0608 File Offset: 0x000BE808
		protected UnExpectedPageSizeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.db = (string)info.GetValue("db", typeof(string));
			this.pageSize = (long)info.GetValue("pageSize", typeof(long));
		}

		// Token: 0x06002CFA RID: 11514 RVA: 0x000C065D File Offset: 0x000BE85D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("db", this.db);
			info.AddValue("pageSize", this.pageSize);
		}

		// Token: 0x17000B69 RID: 2921
		// (get) Token: 0x06002CFB RID: 11515 RVA: 0x000C0689 File Offset: 0x000BE889
		public string Db
		{
			get
			{
				return this.db;
			}
		}

		// Token: 0x17000B6A RID: 2922
		// (get) Token: 0x06002CFC RID: 11516 RVA: 0x000C0691 File Offset: 0x000BE891
		public long PageSize
		{
			get
			{
				return this.pageSize;
			}
		}

		// Token: 0x04001506 RID: 5382
		private readonly string db;

		// Token: 0x04001507 RID: 5383
		private readonly long pageSize;
	}
}
