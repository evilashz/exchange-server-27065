using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200046B RID: 1131
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AmDbActionDismountFailedException : AmDbActionException
	{
		// Token: 0x06002BAF RID: 11183 RVA: 0x000BDDB4 File Offset: 0x000BBFB4
		public AmDbActionDismountFailedException() : base(ReplayStrings.AmDbActionDismountFailedException)
		{
		}

		// Token: 0x06002BB0 RID: 11184 RVA: 0x000BDDC6 File Offset: 0x000BBFC6
		public AmDbActionDismountFailedException(Exception innerException) : base(ReplayStrings.AmDbActionDismountFailedException, innerException)
		{
		}

		// Token: 0x06002BB1 RID: 11185 RVA: 0x000BDDD9 File Offset: 0x000BBFD9
		protected AmDbActionDismountFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06002BB2 RID: 11186 RVA: 0x000BDDE3 File Offset: 0x000BBFE3
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
