using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000345 RID: 837
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UpdateMovedMailboxPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x06002610 RID: 9744 RVA: 0x00052798 File Offset: 0x00050998
		public UpdateMovedMailboxPermanentException() : base(MrsStrings.ErrorWhileUpdatingMovedMailbox)
		{
		}

		// Token: 0x06002611 RID: 9745 RVA: 0x000527A5 File Offset: 0x000509A5
		public UpdateMovedMailboxPermanentException(Exception innerException) : base(MrsStrings.ErrorWhileUpdatingMovedMailbox, innerException)
		{
		}

		// Token: 0x06002612 RID: 9746 RVA: 0x000527B3 File Offset: 0x000509B3
		protected UpdateMovedMailboxPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06002613 RID: 9747 RVA: 0x000527BD File Offset: 0x000509BD
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
