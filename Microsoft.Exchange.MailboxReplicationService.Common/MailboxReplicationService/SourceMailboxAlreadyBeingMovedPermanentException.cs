using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000340 RID: 832
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SourceMailboxAlreadyBeingMovedPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x060025FB RID: 9723 RVA: 0x0005265F File Offset: 0x0005085F
		public SourceMailboxAlreadyBeingMovedPermanentException() : base(MrsStrings.SourceMailboxAlreadyBeingMoved)
		{
		}

		// Token: 0x060025FC RID: 9724 RVA: 0x0005266C File Offset: 0x0005086C
		public SourceMailboxAlreadyBeingMovedPermanentException(Exception innerException) : base(MrsStrings.SourceMailboxAlreadyBeingMoved, innerException)
		{
		}

		// Token: 0x060025FD RID: 9725 RVA: 0x0005267A File Offset: 0x0005087A
		protected SourceMailboxAlreadyBeingMovedPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060025FE RID: 9726 RVA: 0x00052684 File Offset: 0x00050884
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
