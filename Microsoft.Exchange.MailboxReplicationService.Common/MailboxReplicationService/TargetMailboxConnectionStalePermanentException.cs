using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020002CD RID: 717
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class TargetMailboxConnectionStalePermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x060023C4 RID: 9156 RVA: 0x0004F097 File Offset: 0x0004D297
		public TargetMailboxConnectionStalePermanentException() : base(MrsStrings.CouldNotConnectToTargetMailbox)
		{
		}

		// Token: 0x060023C5 RID: 9157 RVA: 0x0004F0A4 File Offset: 0x0004D2A4
		public TargetMailboxConnectionStalePermanentException(Exception innerException) : base(MrsStrings.CouldNotConnectToTargetMailbox, innerException)
		{
		}

		// Token: 0x060023C6 RID: 9158 RVA: 0x0004F0B2 File Offset: 0x0004D2B2
		protected TargetMailboxConnectionStalePermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060023C7 RID: 9159 RVA: 0x0004F0BC File Offset: 0x0004D2BC
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
