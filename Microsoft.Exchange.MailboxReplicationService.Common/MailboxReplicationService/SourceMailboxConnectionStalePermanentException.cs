using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020002CC RID: 716
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SourceMailboxConnectionStalePermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x060023C0 RID: 9152 RVA: 0x0004F068 File Offset: 0x0004D268
		public SourceMailboxConnectionStalePermanentException() : base(MrsStrings.CouldNotConnectToSourceMailbox)
		{
		}

		// Token: 0x060023C1 RID: 9153 RVA: 0x0004F075 File Offset: 0x0004D275
		public SourceMailboxConnectionStalePermanentException(Exception innerException) : base(MrsStrings.CouldNotConnectToSourceMailbox, innerException)
		{
		}

		// Token: 0x060023C2 RID: 9154 RVA: 0x0004F083 File Offset: 0x0004D283
		protected SourceMailboxConnectionStalePermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060023C3 RID: 9155 RVA: 0x0004F08D File Offset: 0x0004D28D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
