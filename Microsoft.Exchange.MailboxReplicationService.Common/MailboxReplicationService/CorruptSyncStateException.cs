using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000377 RID: 887
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CorruptSyncStateException : MailboxReplicationPermanentException
	{
		// Token: 0x0600270F RID: 9999 RVA: 0x00054125 File Offset: 0x00052325
		public CorruptSyncStateException() : base(MrsStrings.CorruptSyncState)
		{
		}

		// Token: 0x06002710 RID: 10000 RVA: 0x00054132 File Offset: 0x00052332
		public CorruptSyncStateException(Exception innerException) : base(MrsStrings.CorruptSyncState, innerException)
		{
		}

		// Token: 0x06002711 RID: 10001 RVA: 0x00054140 File Offset: 0x00052340
		protected CorruptSyncStateException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06002712 RID: 10002 RVA: 0x0005414A File Offset: 0x0005234A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
