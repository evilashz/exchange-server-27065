using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020002F1 RID: 753
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class TooManyLargeItemsPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x06002475 RID: 9333 RVA: 0x0005014E File Offset: 0x0004E34E
		public TooManyLargeItemsPermanentException() : base(MrsStrings.TooManyLargeItems)
		{
		}

		// Token: 0x06002476 RID: 9334 RVA: 0x0005015B File Offset: 0x0004E35B
		public TooManyLargeItemsPermanentException(Exception innerException) : base(MrsStrings.TooManyLargeItems, innerException)
		{
		}

		// Token: 0x06002477 RID: 9335 RVA: 0x00050169 File Offset: 0x0004E369
		protected TooManyLargeItemsPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06002478 RID: 9336 RVA: 0x00050173 File Offset: 0x0004E373
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
