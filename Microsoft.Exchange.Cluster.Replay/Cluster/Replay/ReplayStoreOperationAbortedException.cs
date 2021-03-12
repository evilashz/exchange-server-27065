using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020004D1 RID: 1233
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ReplayStoreOperationAbortedException : LocalizedException
	{
		// Token: 0x06002DF6 RID: 11766 RVA: 0x000C27B1 File Offset: 0x000C09B1
		public ReplayStoreOperationAbortedException(string operationName) : base(ReplayStrings.ReplayStoreOperationAbortedException(operationName))
		{
			this.operationName = operationName;
		}

		// Token: 0x06002DF7 RID: 11767 RVA: 0x000C27C6 File Offset: 0x000C09C6
		public ReplayStoreOperationAbortedException(string operationName, Exception innerException) : base(ReplayStrings.ReplayStoreOperationAbortedException(operationName), innerException)
		{
			this.operationName = operationName;
		}

		// Token: 0x06002DF8 RID: 11768 RVA: 0x000C27DC File Offset: 0x000C09DC
		protected ReplayStoreOperationAbortedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.operationName = (string)info.GetValue("operationName", typeof(string));
		}

		// Token: 0x06002DF9 RID: 11769 RVA: 0x000C2806 File Offset: 0x000C0A06
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("operationName", this.operationName);
		}

		// Token: 0x17000BBC RID: 3004
		// (get) Token: 0x06002DFA RID: 11770 RVA: 0x000C2821 File Offset: 0x000C0A21
		public string OperationName
		{
			get
			{
				return this.operationName;
			}
		}

		// Token: 0x04001559 RID: 5465
		private readonly string operationName;
	}
}
