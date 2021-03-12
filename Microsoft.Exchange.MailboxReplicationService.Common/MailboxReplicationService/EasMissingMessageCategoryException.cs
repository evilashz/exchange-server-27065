using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200038E RID: 910
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class EasMissingMessageCategoryException : MailboxReplicationPermanentException
	{
		// Token: 0x06002782 RID: 10114 RVA: 0x00054BFA File Offset: 0x00052DFA
		public EasMissingMessageCategoryException() : base(MrsStrings.EasMissingMessageCategory)
		{
		}

		// Token: 0x06002783 RID: 10115 RVA: 0x00054C07 File Offset: 0x00052E07
		public EasMissingMessageCategoryException(Exception innerException) : base(MrsStrings.EasMissingMessageCategory, innerException)
		{
		}

		// Token: 0x06002784 RID: 10116 RVA: 0x00054C15 File Offset: 0x00052E15
		protected EasMissingMessageCategoryException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06002785 RID: 10117 RVA: 0x00054C1F File Offset: 0x00052E1F
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
