using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000342 RID: 834
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DestMailboxAlreadyBeingMovedPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x06002603 RID: 9731 RVA: 0x000526BD File Offset: 0x000508BD
		public DestMailboxAlreadyBeingMovedPermanentException() : base(MrsStrings.DestMailboxAlreadyBeingMoved)
		{
		}

		// Token: 0x06002604 RID: 9732 RVA: 0x000526CA File Offset: 0x000508CA
		public DestMailboxAlreadyBeingMovedPermanentException(Exception innerException) : base(MrsStrings.DestMailboxAlreadyBeingMoved, innerException)
		{
		}

		// Token: 0x06002605 RID: 9733 RVA: 0x000526D8 File Offset: 0x000508D8
		protected DestMailboxAlreadyBeingMovedPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06002606 RID: 9734 RVA: 0x000526E2 File Offset: 0x000508E2
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
