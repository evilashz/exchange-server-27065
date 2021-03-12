using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200032A RID: 810
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class TargetMailboxConnectionWasLostPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x06002585 RID: 9605 RVA: 0x00051905 File Offset: 0x0004FB05
		public TargetMailboxConnectionWasLostPermanentException() : base(MrsStrings.TargetMailboxConnectionWasLost)
		{
		}

		// Token: 0x06002586 RID: 9606 RVA: 0x00051912 File Offset: 0x0004FB12
		public TargetMailboxConnectionWasLostPermanentException(Exception innerException) : base(MrsStrings.TargetMailboxConnectionWasLost, innerException)
		{
		}

		// Token: 0x06002587 RID: 9607 RVA: 0x00051920 File Offset: 0x0004FB20
		protected TargetMailboxConnectionWasLostPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06002588 RID: 9608 RVA: 0x0005192A File Offset: 0x0004FB2A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
