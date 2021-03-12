using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020003FF RID: 1023
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ReplayServiceSuspendReseedBlockedException : TaskServerException
	{
		// Token: 0x0600296A RID: 10602 RVA: 0x000B99D8 File Offset: 0x000B7BD8
		public ReplayServiceSuspendReseedBlockedException() : base(ReplayStrings.ReplayServiceSuspendReseedBlockedException)
		{
		}

		// Token: 0x0600296B RID: 10603 RVA: 0x000B99EA File Offset: 0x000B7BEA
		public ReplayServiceSuspendReseedBlockedException(Exception innerException) : base(ReplayStrings.ReplayServiceSuspendReseedBlockedException, innerException)
		{
		}

		// Token: 0x0600296C RID: 10604 RVA: 0x000B99FD File Offset: 0x000B7BFD
		protected ReplayServiceSuspendReseedBlockedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600296D RID: 10605 RVA: 0x000B9A07 File Offset: 0x000B7C07
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
