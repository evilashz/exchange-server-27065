using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000408 RID: 1032
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ReplayDatabaseOperationCancelledException : TaskServerException
	{
		// Token: 0x06002993 RID: 10643 RVA: 0x000B9D56 File Offset: 0x000B7F56
		public ReplayDatabaseOperationCancelledException(string operationName, string db) : base(ReplayStrings.ReplayDatabaseOperationCancelledException(operationName, db))
		{
			this.operationName = operationName;
			this.db = db;
		}

		// Token: 0x06002994 RID: 10644 RVA: 0x000B9D78 File Offset: 0x000B7F78
		public ReplayDatabaseOperationCancelledException(string operationName, string db, Exception innerException) : base(ReplayStrings.ReplayDatabaseOperationCancelledException(operationName, db), innerException)
		{
			this.operationName = operationName;
			this.db = db;
		}

		// Token: 0x06002995 RID: 10645 RVA: 0x000B9D9C File Offset: 0x000B7F9C
		protected ReplayDatabaseOperationCancelledException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.operationName = (string)info.GetValue("operationName", typeof(string));
			this.db = (string)info.GetValue("db", typeof(string));
		}

		// Token: 0x06002996 RID: 10646 RVA: 0x000B9DF1 File Offset: 0x000B7FF1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("operationName", this.operationName);
			info.AddValue("db", this.db);
		}

		// Token: 0x17000A7D RID: 2685
		// (get) Token: 0x06002997 RID: 10647 RVA: 0x000B9E1D File Offset: 0x000B801D
		public string OperationName
		{
			get
			{
				return this.operationName;
			}
		}

		// Token: 0x17000A7E RID: 2686
		// (get) Token: 0x06002998 RID: 10648 RVA: 0x000B9E25 File Offset: 0x000B8025
		public string Db
		{
			get
			{
				return this.db;
			}
		}

		// Token: 0x0400141A RID: 5146
		private readonly string operationName;

		// Token: 0x0400141B RID: 5147
		private readonly string db;
	}
}
