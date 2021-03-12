using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200032C RID: 812
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CannotPreventCompletionForCompletingMovePermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x0600258D RID: 9613 RVA: 0x00051963 File Offset: 0x0004FB63
		public CannotPreventCompletionForCompletingMovePermanentException() : base(MrsStrings.ErrorCannotPreventCompletionForCompletingMove)
		{
		}

		// Token: 0x0600258E RID: 9614 RVA: 0x00051970 File Offset: 0x0004FB70
		public CannotPreventCompletionForCompletingMovePermanentException(Exception innerException) : base(MrsStrings.ErrorCannotPreventCompletionForCompletingMove, innerException)
		{
		}

		// Token: 0x0600258F RID: 9615 RVA: 0x0005197E File Offset: 0x0004FB7E
		protected CannotPreventCompletionForCompletingMovePermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06002590 RID: 9616 RVA: 0x00051988 File Offset: 0x0004FB88
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
