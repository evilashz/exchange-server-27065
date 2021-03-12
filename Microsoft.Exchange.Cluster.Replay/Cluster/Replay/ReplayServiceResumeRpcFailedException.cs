using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020003F5 RID: 1013
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ReplayServiceResumeRpcFailedException : TaskServerException
	{
		// Token: 0x0600293F RID: 10559 RVA: 0x000B96C3 File Offset: 0x000B78C3
		public ReplayServiceResumeRpcFailedException() : base(ReplayStrings.ReplayServiceResumeRpcFailedException)
		{
		}

		// Token: 0x06002940 RID: 10560 RVA: 0x000B96D5 File Offset: 0x000B78D5
		public ReplayServiceResumeRpcFailedException(Exception innerException) : base(ReplayStrings.ReplayServiceResumeRpcFailedException, innerException)
		{
		}

		// Token: 0x06002941 RID: 10561 RVA: 0x000B96E8 File Offset: 0x000B78E8
		protected ReplayServiceResumeRpcFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06002942 RID: 10562 RVA: 0x000B96F2 File Offset: 0x000B78F2
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
