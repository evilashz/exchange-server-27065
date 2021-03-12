using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200040A RID: 1034
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ReplayServiceUnknownReplicaInstanceException : TaskServerException
	{
		// Token: 0x060029A0 RID: 10656 RVA: 0x000B9F61 File Offset: 0x000B8161
		public ReplayServiceUnknownReplicaInstanceException(string operationName, string db) : base(ReplayStrings.ReplayServiceUnknownReplicaInstanceException(operationName, db))
		{
			this.operationName = operationName;
			this.db = db;
		}

		// Token: 0x060029A1 RID: 10657 RVA: 0x000B9F83 File Offset: 0x000B8183
		public ReplayServiceUnknownReplicaInstanceException(string operationName, string db, Exception innerException) : base(ReplayStrings.ReplayServiceUnknownReplicaInstanceException(operationName, db), innerException)
		{
			this.operationName = operationName;
			this.db = db;
		}

		// Token: 0x060029A2 RID: 10658 RVA: 0x000B9FA8 File Offset: 0x000B81A8
		protected ReplayServiceUnknownReplicaInstanceException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.operationName = (string)info.GetValue("operationName", typeof(string));
			this.db = (string)info.GetValue("db", typeof(string));
		}

		// Token: 0x060029A3 RID: 10659 RVA: 0x000B9FFD File Offset: 0x000B81FD
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("operationName", this.operationName);
			info.AddValue("db", this.db);
		}

		// Token: 0x17000A82 RID: 2690
		// (get) Token: 0x060029A4 RID: 10660 RVA: 0x000BA029 File Offset: 0x000B8229
		public string OperationName
		{
			get
			{
				return this.operationName;
			}
		}

		// Token: 0x17000A83 RID: 2691
		// (get) Token: 0x060029A5 RID: 10661 RVA: 0x000BA031 File Offset: 0x000B8231
		public string Db
		{
			get
			{
				return this.db;
			}
		}

		// Token: 0x0400141F RID: 5151
		private readonly string operationName;

		// Token: 0x04001420 RID: 5152
		private readonly string db;
	}
}
