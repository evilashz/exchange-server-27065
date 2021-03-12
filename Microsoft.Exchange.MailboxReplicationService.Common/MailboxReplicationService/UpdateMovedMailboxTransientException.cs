using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000344 RID: 836
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UpdateMovedMailboxTransientException : MailboxReplicationTransientException
	{
		// Token: 0x0600260C RID: 9740 RVA: 0x00052769 File Offset: 0x00050969
		public UpdateMovedMailboxTransientException() : base(MrsStrings.ErrorWhileUpdatingMovedMailbox)
		{
		}

		// Token: 0x0600260D RID: 9741 RVA: 0x00052776 File Offset: 0x00050976
		public UpdateMovedMailboxTransientException(Exception innerException) : base(MrsStrings.ErrorWhileUpdatingMovedMailbox, innerException)
		{
		}

		// Token: 0x0600260E RID: 9742 RVA: 0x00052784 File Offset: 0x00050984
		protected UpdateMovedMailboxTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600260F RID: 9743 RVA: 0x0005278E File Offset: 0x0005098E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
