using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020002A2 RID: 674
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CannotSetCompletingPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x060022E7 RID: 8935 RVA: 0x0004D9F0 File Offset: 0x0004BBF0
		public CannotSetCompletingPermanentException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060022E8 RID: 8936 RVA: 0x0004D9F9 File Offset: 0x0004BBF9
		public CannotSetCompletingPermanentException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060022E9 RID: 8937 RVA: 0x0004DA03 File Offset: 0x0004BC03
		protected CannotSetCompletingPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060022EA RID: 8938 RVA: 0x0004DA0D File Offset: 0x0004BC0D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
