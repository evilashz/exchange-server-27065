using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200033F RID: 831
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SourceMailboxAlreadyBeingMovedTransientException : MailboxReplicationTransientException
	{
		// Token: 0x060025F7 RID: 9719 RVA: 0x00052630 File Offset: 0x00050830
		public SourceMailboxAlreadyBeingMovedTransientException() : base(MrsStrings.SourceMailboxAlreadyBeingMoved)
		{
		}

		// Token: 0x060025F8 RID: 9720 RVA: 0x0005263D File Offset: 0x0005083D
		public SourceMailboxAlreadyBeingMovedTransientException(Exception innerException) : base(MrsStrings.SourceMailboxAlreadyBeingMoved, innerException)
		{
		}

		// Token: 0x060025F9 RID: 9721 RVA: 0x0005264B File Offset: 0x0005084B
		protected SourceMailboxAlreadyBeingMovedTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060025FA RID: 9722 RVA: 0x00052655 File Offset: 0x00050855
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
