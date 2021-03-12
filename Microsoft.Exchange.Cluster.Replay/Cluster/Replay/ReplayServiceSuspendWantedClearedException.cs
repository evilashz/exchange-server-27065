using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000403 RID: 1027
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ReplayServiceSuspendWantedClearedException : TaskServerException
	{
		// Token: 0x0600297A RID: 10618 RVA: 0x000B9ABC File Offset: 0x000B7CBC
		public ReplayServiceSuspendWantedClearedException() : base(ReplayStrings.ReplayServiceSuspendWantedClearedException)
		{
		}

		// Token: 0x0600297B RID: 10619 RVA: 0x000B9ACE File Offset: 0x000B7CCE
		public ReplayServiceSuspendWantedClearedException(Exception innerException) : base(ReplayStrings.ReplayServiceSuspendWantedClearedException, innerException)
		{
		}

		// Token: 0x0600297C RID: 10620 RVA: 0x000B9AE1 File Offset: 0x000B7CE1
		protected ReplayServiceSuspendWantedClearedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600297D RID: 10621 RVA: 0x000B9AEB File Offset: 0x000B7CEB
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
