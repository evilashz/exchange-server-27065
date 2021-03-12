using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200032D RID: 813
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FinalizationIsBlockedPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x06002591 RID: 9617 RVA: 0x00051992 File Offset: 0x0004FB92
		public FinalizationIsBlockedPermanentException() : base(MrsStrings.ErrorFinalizationIsBlocked)
		{
		}

		// Token: 0x06002592 RID: 9618 RVA: 0x0005199F File Offset: 0x0004FB9F
		public FinalizationIsBlockedPermanentException(Exception innerException) : base(MrsStrings.ErrorFinalizationIsBlocked, innerException)
		{
		}

		// Token: 0x06002593 RID: 9619 RVA: 0x000519AD File Offset: 0x0004FBAD
		protected FinalizationIsBlockedPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06002594 RID: 9620 RVA: 0x000519B7 File Offset: 0x0004FBB7
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
