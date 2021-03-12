using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000325 RID: 805
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RehomeRequestPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x0600256E RID: 9582 RVA: 0x0005172E File Offset: 0x0004F92E
		public RehomeRequestPermanentException() : base(MrsStrings.RehomeRequestFailure)
		{
		}

		// Token: 0x0600256F RID: 9583 RVA: 0x0005173B File Offset: 0x0004F93B
		public RehomeRequestPermanentException(Exception innerException) : base(MrsStrings.RehomeRequestFailure, innerException)
		{
		}

		// Token: 0x06002570 RID: 9584 RVA: 0x00051749 File Offset: 0x0004F949
		protected RehomeRequestPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06002571 RID: 9585 RVA: 0x00051753 File Offset: 0x0004F953
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
