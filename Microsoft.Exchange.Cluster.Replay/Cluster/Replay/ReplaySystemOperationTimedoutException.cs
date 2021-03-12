using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000407 RID: 1031
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ReplaySystemOperationTimedoutException : TaskServerException
	{
		// Token: 0x0600298D RID: 10637 RVA: 0x000B9C7B File Offset: 0x000B7E7B
		public ReplaySystemOperationTimedoutException(string operationName, TimeSpan timeout) : base(ReplayStrings.ReplaySystemOperationTimedoutException(operationName, timeout))
		{
			this.operationName = operationName;
			this.timeout = timeout;
		}

		// Token: 0x0600298E RID: 10638 RVA: 0x000B9C9D File Offset: 0x000B7E9D
		public ReplaySystemOperationTimedoutException(string operationName, TimeSpan timeout, Exception innerException) : base(ReplayStrings.ReplaySystemOperationTimedoutException(operationName, timeout), innerException)
		{
			this.operationName = operationName;
			this.timeout = timeout;
		}

		// Token: 0x0600298F RID: 10639 RVA: 0x000B9CC0 File Offset: 0x000B7EC0
		protected ReplaySystemOperationTimedoutException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.operationName = (string)info.GetValue("operationName", typeof(string));
			this.timeout = (TimeSpan)info.GetValue("timeout", typeof(TimeSpan));
		}

		// Token: 0x06002990 RID: 10640 RVA: 0x000B9D15 File Offset: 0x000B7F15
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("operationName", this.operationName);
			info.AddValue("timeout", this.timeout);
		}

		// Token: 0x17000A7B RID: 2683
		// (get) Token: 0x06002991 RID: 10641 RVA: 0x000B9D46 File Offset: 0x000B7F46
		public string OperationName
		{
			get
			{
				return this.operationName;
			}
		}

		// Token: 0x17000A7C RID: 2684
		// (get) Token: 0x06002992 RID: 10642 RVA: 0x000B9D4E File Offset: 0x000B7F4E
		public TimeSpan Timeout
		{
			get
			{
				return this.timeout;
			}
		}

		// Token: 0x04001418 RID: 5144
		private readonly string operationName;

		// Token: 0x04001419 RID: 5145
		private readonly TimeSpan timeout;
	}
}
