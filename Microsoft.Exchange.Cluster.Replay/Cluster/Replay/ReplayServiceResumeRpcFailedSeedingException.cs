using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020003F8 RID: 1016
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ReplayServiceResumeRpcFailedSeedingException : TaskServerException
	{
		// Token: 0x0600294D RID: 10573 RVA: 0x000B9800 File Offset: 0x000B7A00
		public ReplayServiceResumeRpcFailedSeedingException() : base(ReplayStrings.ReplayServiceResumeRpcFailedSeedingException)
		{
		}

		// Token: 0x0600294E RID: 10574 RVA: 0x000B9812 File Offset: 0x000B7A12
		public ReplayServiceResumeRpcFailedSeedingException(Exception innerException) : base(ReplayStrings.ReplayServiceResumeRpcFailedSeedingException, innerException)
		{
		}

		// Token: 0x0600294F RID: 10575 RVA: 0x000B9825 File Offset: 0x000B7A25
		protected ReplayServiceResumeRpcFailedSeedingException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06002950 RID: 10576 RVA: 0x000B982F File Offset: 0x000B7A2F
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
