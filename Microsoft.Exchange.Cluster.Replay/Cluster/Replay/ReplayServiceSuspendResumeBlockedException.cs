using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000400 RID: 1024
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ReplayServiceSuspendResumeBlockedException : TaskServerException
	{
		// Token: 0x0600296E RID: 10606 RVA: 0x000B9A11 File Offset: 0x000B7C11
		public ReplayServiceSuspendResumeBlockedException() : base(ReplayStrings.ReplayServiceSuspendResumeBlockedException)
		{
		}

		// Token: 0x0600296F RID: 10607 RVA: 0x000B9A23 File Offset: 0x000B7C23
		public ReplayServiceSuspendResumeBlockedException(Exception innerException) : base(ReplayStrings.ReplayServiceSuspendResumeBlockedException, innerException)
		{
		}

		// Token: 0x06002970 RID: 10608 RVA: 0x000B9A36 File Offset: 0x000B7C36
		protected ReplayServiceSuspendResumeBlockedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06002971 RID: 10609 RVA: 0x000B9A40 File Offset: 0x000B7C40
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
