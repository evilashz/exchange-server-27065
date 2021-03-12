using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000305 RID: 773
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class JobCanceledPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x060024D2 RID: 9426 RVA: 0x000508D6 File Offset: 0x0004EAD6
		public JobCanceledPermanentException() : base(MrsStrings.JobHasBeenCanceled)
		{
		}

		// Token: 0x060024D3 RID: 9427 RVA: 0x000508E3 File Offset: 0x0004EAE3
		public JobCanceledPermanentException(Exception innerException) : base(MrsStrings.JobHasBeenCanceled, innerException)
		{
		}

		// Token: 0x060024D4 RID: 9428 RVA: 0x000508F1 File Offset: 0x0004EAF1
		protected JobCanceledPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060024D5 RID: 9429 RVA: 0x000508FB File Offset: 0x0004EAFB
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
