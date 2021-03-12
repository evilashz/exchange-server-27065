using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200030B RID: 779
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class JobRehomingDisallowedPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x060024EA RID: 9450 RVA: 0x000509E8 File Offset: 0x0004EBE8
		public JobRehomingDisallowedPermanentException() : base(MrsStrings.JobCannotBeRehomedWhenInProgress)
		{
		}

		// Token: 0x060024EB RID: 9451 RVA: 0x000509F5 File Offset: 0x0004EBF5
		public JobRehomingDisallowedPermanentException(Exception innerException) : base(MrsStrings.JobCannotBeRehomedWhenInProgress, innerException)
		{
		}

		// Token: 0x060024EC RID: 9452 RVA: 0x00050A03 File Offset: 0x0004EC03
		protected JobRehomingDisallowedPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060024ED RID: 9453 RVA: 0x00050A0D File Offset: 0x0004EC0D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
