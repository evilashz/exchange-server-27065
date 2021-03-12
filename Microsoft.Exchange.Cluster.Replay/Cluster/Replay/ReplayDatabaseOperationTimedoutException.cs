using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000409 RID: 1033
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ReplayDatabaseOperationTimedoutException : TaskServerException
	{
		// Token: 0x06002999 RID: 10649 RVA: 0x000B9E2D File Offset: 0x000B802D
		public ReplayDatabaseOperationTimedoutException(string operationName, string db, TimeSpan timeout) : base(ReplayStrings.ReplayDatabaseOperationTimedoutException(operationName, db, timeout))
		{
			this.operationName = operationName;
			this.db = db;
			this.timeout = timeout;
		}

		// Token: 0x0600299A RID: 10650 RVA: 0x000B9E57 File Offset: 0x000B8057
		public ReplayDatabaseOperationTimedoutException(string operationName, string db, TimeSpan timeout, Exception innerException) : base(ReplayStrings.ReplayDatabaseOperationTimedoutException(operationName, db, timeout), innerException)
		{
			this.operationName = operationName;
			this.db = db;
			this.timeout = timeout;
		}

		// Token: 0x0600299B RID: 10651 RVA: 0x000B9E84 File Offset: 0x000B8084
		protected ReplayDatabaseOperationTimedoutException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.operationName = (string)info.GetValue("operationName", typeof(string));
			this.db = (string)info.GetValue("db", typeof(string));
			this.timeout = (TimeSpan)info.GetValue("timeout", typeof(TimeSpan));
		}

		// Token: 0x0600299C RID: 10652 RVA: 0x000B9EFC File Offset: 0x000B80FC
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("operationName", this.operationName);
			info.AddValue("db", this.db);
			info.AddValue("timeout", this.timeout);
		}

		// Token: 0x17000A7F RID: 2687
		// (get) Token: 0x0600299D RID: 10653 RVA: 0x000B9F49 File Offset: 0x000B8149
		public string OperationName
		{
			get
			{
				return this.operationName;
			}
		}

		// Token: 0x17000A80 RID: 2688
		// (get) Token: 0x0600299E RID: 10654 RVA: 0x000B9F51 File Offset: 0x000B8151
		public string Db
		{
			get
			{
				return this.db;
			}
		}

		// Token: 0x17000A81 RID: 2689
		// (get) Token: 0x0600299F RID: 10655 RVA: 0x000B9F59 File Offset: 0x000B8159
		public TimeSpan Timeout
		{
			get
			{
				return this.timeout;
			}
		}

		// Token: 0x0400141C RID: 5148
		private readonly string operationName;

		// Token: 0x0400141D RID: 5149
		private readonly string db;

		// Token: 0x0400141E RID: 5150
		private readonly TimeSpan timeout;
	}
}
