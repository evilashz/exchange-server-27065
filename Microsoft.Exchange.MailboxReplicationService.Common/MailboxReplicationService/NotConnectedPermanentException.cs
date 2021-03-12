using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200033E RID: 830
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NotConnectedPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x060025F3 RID: 9715 RVA: 0x00052601 File Offset: 0x00050801
		public NotConnectedPermanentException() : base(MrsStrings.NotConnected)
		{
		}

		// Token: 0x060025F4 RID: 9716 RVA: 0x0005260E File Offset: 0x0005080E
		public NotConnectedPermanentException(Exception innerException) : base(MrsStrings.NotConnected, innerException)
		{
		}

		// Token: 0x060025F5 RID: 9717 RVA: 0x0005261C File Offset: 0x0005081C
		protected NotConnectedPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060025F6 RID: 9718 RVA: 0x00052626 File Offset: 0x00050826
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
