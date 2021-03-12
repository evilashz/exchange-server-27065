using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000406 RID: 1030
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ReplaySystemOperationCancelledException : TaskServerException
	{
		// Token: 0x06002988 RID: 10632 RVA: 0x000B9BF9 File Offset: 0x000B7DF9
		public ReplaySystemOperationCancelledException(string operationName) : base(ReplayStrings.ReplaySystemOperationCancelledException(operationName))
		{
			this.operationName = operationName;
		}

		// Token: 0x06002989 RID: 10633 RVA: 0x000B9C13 File Offset: 0x000B7E13
		public ReplaySystemOperationCancelledException(string operationName, Exception innerException) : base(ReplayStrings.ReplaySystemOperationCancelledException(operationName), innerException)
		{
			this.operationName = operationName;
		}

		// Token: 0x0600298A RID: 10634 RVA: 0x000B9C2E File Offset: 0x000B7E2E
		protected ReplaySystemOperationCancelledException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.operationName = (string)info.GetValue("operationName", typeof(string));
		}

		// Token: 0x0600298B RID: 10635 RVA: 0x000B9C58 File Offset: 0x000B7E58
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("operationName", this.operationName);
		}

		// Token: 0x17000A7A RID: 2682
		// (get) Token: 0x0600298C RID: 10636 RVA: 0x000B9C73 File Offset: 0x000B7E73
		public string OperationName
		{
			get
			{
				return this.operationName;
			}
		}

		// Token: 0x04001417 RID: 5143
		private readonly string operationName;
	}
}
