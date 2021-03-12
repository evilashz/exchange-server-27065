using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020003EB RID: 1003
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ReplayDbOperationWrapperException : ReplayDbOperationException
	{
		// Token: 0x06002907 RID: 10503 RVA: 0x000B8FBB File Offset: 0x000B71BB
		public ReplayDbOperationWrapperException(string operationError) : base(ReplayStrings.ReplayDbOperationWrapperException(operationError))
		{
			this.operationError = operationError;
		}

		// Token: 0x06002908 RID: 10504 RVA: 0x000B8FD5 File Offset: 0x000B71D5
		public ReplayDbOperationWrapperException(string operationError, Exception innerException) : base(ReplayStrings.ReplayDbOperationWrapperException(operationError), innerException)
		{
			this.operationError = operationError;
		}

		// Token: 0x06002909 RID: 10505 RVA: 0x000B8FF0 File Offset: 0x000B71F0
		protected ReplayDbOperationWrapperException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.operationError = (string)info.GetValue("operationError", typeof(string));
		}

		// Token: 0x0600290A RID: 10506 RVA: 0x000B901A File Offset: 0x000B721A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("operationError", this.operationError);
		}

		// Token: 0x17000A65 RID: 2661
		// (get) Token: 0x0600290B RID: 10507 RVA: 0x000B9035 File Offset: 0x000B7235
		public string OperationError
		{
			get
			{
				return this.operationError;
			}
		}

		// Token: 0x04001402 RID: 5122
		private readonly string operationError;
	}
}
