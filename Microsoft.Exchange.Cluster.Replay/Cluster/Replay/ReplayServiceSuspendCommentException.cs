using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020003FE RID: 1022
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ReplayServiceSuspendCommentException : TaskServerException
	{
		// Token: 0x06002966 RID: 10598 RVA: 0x000B999F File Offset: 0x000B7B9F
		public ReplayServiceSuspendCommentException() : base(ReplayStrings.ReplayServiceSuspendCommentException)
		{
		}

		// Token: 0x06002967 RID: 10599 RVA: 0x000B99B1 File Offset: 0x000B7BB1
		public ReplayServiceSuspendCommentException(Exception innerException) : base(ReplayStrings.ReplayServiceSuspendCommentException, innerException)
		{
		}

		// Token: 0x06002968 RID: 10600 RVA: 0x000B99C4 File Offset: 0x000B7BC4
		protected ReplayServiceSuspendCommentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06002969 RID: 10601 RVA: 0x000B99CE File Offset: 0x000B7BCE
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
