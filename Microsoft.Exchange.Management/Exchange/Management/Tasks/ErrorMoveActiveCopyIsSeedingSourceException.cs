using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000EEB RID: 3819
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ErrorMoveActiveCopyIsSeedingSourceException : LocalizedException
	{
		// Token: 0x0600A97E RID: 43390 RVA: 0x0028BD71 File Offset: 0x00289F71
		public ErrorMoveActiveCopyIsSeedingSourceException(string db, string server) : base(Strings.ErrorMoveActiveCopyIsSeedingSourceException(db, server))
		{
			this.db = db;
			this.server = server;
		}

		// Token: 0x0600A97F RID: 43391 RVA: 0x0028BD8E File Offset: 0x00289F8E
		public ErrorMoveActiveCopyIsSeedingSourceException(string db, string server, Exception innerException) : base(Strings.ErrorMoveActiveCopyIsSeedingSourceException(db, server), innerException)
		{
			this.db = db;
			this.server = server;
		}

		// Token: 0x0600A980 RID: 43392 RVA: 0x0028BDAC File Offset: 0x00289FAC
		protected ErrorMoveActiveCopyIsSeedingSourceException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.db = (string)info.GetValue("db", typeof(string));
			this.server = (string)info.GetValue("server", typeof(string));
		}

		// Token: 0x0600A981 RID: 43393 RVA: 0x0028BE01 File Offset: 0x0028A001
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("db", this.db);
			info.AddValue("server", this.server);
		}

		// Token: 0x170036EF RID: 14063
		// (get) Token: 0x0600A982 RID: 43394 RVA: 0x0028BE2D File Offset: 0x0028A02D
		public string Db
		{
			get
			{
				return this.db;
			}
		}

		// Token: 0x170036F0 RID: 14064
		// (get) Token: 0x0600A983 RID: 43395 RVA: 0x0028BE35 File Offset: 0x0028A035
		public string Server
		{
			get
			{
				return this.server;
			}
		}

		// Token: 0x04006055 RID: 24661
		private readonly string db;

		// Token: 0x04006056 RID: 24662
		private readonly string server;
	}
}
