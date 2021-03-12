using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020002F2 RID: 754
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class TooManyMissingItemsPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x06002479 RID: 9337 RVA: 0x0005017D File Offset: 0x0004E37D
		public TooManyMissingItemsPermanentException() : base(MrsStrings.TooManyMissingItems)
		{
		}

		// Token: 0x0600247A RID: 9338 RVA: 0x0005018A File Offset: 0x0004E38A
		public TooManyMissingItemsPermanentException(Exception innerException) : base(MrsStrings.TooManyMissingItems, innerException)
		{
		}

		// Token: 0x0600247B RID: 9339 RVA: 0x00050198 File Offset: 0x0004E398
		protected TooManyMissingItemsPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600247C RID: 9340 RVA: 0x000501A2 File Offset: 0x0004E3A2
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
