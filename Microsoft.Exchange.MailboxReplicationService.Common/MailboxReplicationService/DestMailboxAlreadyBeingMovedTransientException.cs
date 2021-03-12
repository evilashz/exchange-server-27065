using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000341 RID: 833
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DestMailboxAlreadyBeingMovedTransientException : MailboxReplicationTransientException
	{
		// Token: 0x060025FF RID: 9727 RVA: 0x0005268E File Offset: 0x0005088E
		public DestMailboxAlreadyBeingMovedTransientException() : base(MrsStrings.DestMailboxAlreadyBeingMoved)
		{
		}

		// Token: 0x06002600 RID: 9728 RVA: 0x0005269B File Offset: 0x0005089B
		public DestMailboxAlreadyBeingMovedTransientException(Exception innerException) : base(MrsStrings.DestMailboxAlreadyBeingMoved, innerException)
		{
		}

		// Token: 0x06002601 RID: 9729 RVA: 0x000526A9 File Offset: 0x000508A9
		protected DestMailboxAlreadyBeingMovedTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06002602 RID: 9730 RVA: 0x000526B3 File Offset: 0x000508B3
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
