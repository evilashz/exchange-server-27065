using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020003EC RID: 1004
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ReplayDbOperationWrapperTransientException : ReplayDbOperationTransientException
	{
		// Token: 0x0600290C RID: 10508 RVA: 0x000B903D File Offset: 0x000B723D
		public ReplayDbOperationWrapperTransientException(string operationError) : base(ReplayStrings.ReplayDbOperationWrapperTransientException(operationError))
		{
			this.operationError = operationError;
		}

		// Token: 0x0600290D RID: 10509 RVA: 0x000B9057 File Offset: 0x000B7257
		public ReplayDbOperationWrapperTransientException(string operationError, Exception innerException) : base(ReplayStrings.ReplayDbOperationWrapperTransientException(operationError), innerException)
		{
			this.operationError = operationError;
		}

		// Token: 0x0600290E RID: 10510 RVA: 0x000B9072 File Offset: 0x000B7272
		protected ReplayDbOperationWrapperTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.operationError = (string)info.GetValue("operationError", typeof(string));
		}

		// Token: 0x0600290F RID: 10511 RVA: 0x000B909C File Offset: 0x000B729C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("operationError", this.operationError);
		}

		// Token: 0x17000A66 RID: 2662
		// (get) Token: 0x06002910 RID: 10512 RVA: 0x000B90B7 File Offset: 0x000B72B7
		public string OperationError
		{
			get
			{
				return this.operationError;
			}
		}

		// Token: 0x04001403 RID: 5123
		private readonly string operationError;
	}
}
