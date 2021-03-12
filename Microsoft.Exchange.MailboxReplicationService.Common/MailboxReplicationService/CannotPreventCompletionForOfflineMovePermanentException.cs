using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200032B RID: 811
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CannotPreventCompletionForOfflineMovePermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x06002589 RID: 9609 RVA: 0x00051934 File Offset: 0x0004FB34
		public CannotPreventCompletionForOfflineMovePermanentException() : base(MrsStrings.ErrorCannotPreventCompletionForOfflineMove)
		{
		}

		// Token: 0x0600258A RID: 9610 RVA: 0x00051941 File Offset: 0x0004FB41
		public CannotPreventCompletionForOfflineMovePermanentException(Exception innerException) : base(MrsStrings.ErrorCannotPreventCompletionForOfflineMove, innerException)
		{
		}

		// Token: 0x0600258B RID: 9611 RVA: 0x0005194F File Offset: 0x0004FB4F
		protected CannotPreventCompletionForOfflineMovePermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600258C RID: 9612 RVA: 0x00051959 File Offset: 0x0004FB59
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
