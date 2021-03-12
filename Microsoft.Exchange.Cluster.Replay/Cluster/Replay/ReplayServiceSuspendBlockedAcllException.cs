using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020003FB RID: 1019
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ReplayServiceSuspendBlockedAcllException : TaskServerException
	{
		// Token: 0x0600295A RID: 10586 RVA: 0x000B98F4 File Offset: 0x000B7AF4
		public ReplayServiceSuspendBlockedAcllException() : base(ReplayStrings.ReplayServiceSuspendBlockedAcllException)
		{
		}

		// Token: 0x0600295B RID: 10587 RVA: 0x000B9906 File Offset: 0x000B7B06
		public ReplayServiceSuspendBlockedAcllException(Exception innerException) : base(ReplayStrings.ReplayServiceSuspendBlockedAcllException, innerException)
		{
		}

		// Token: 0x0600295C RID: 10588 RVA: 0x000B9919 File Offset: 0x000B7B19
		protected ReplayServiceSuspendBlockedAcllException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600295D RID: 10589 RVA: 0x000B9923 File Offset: 0x000B7B23
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
