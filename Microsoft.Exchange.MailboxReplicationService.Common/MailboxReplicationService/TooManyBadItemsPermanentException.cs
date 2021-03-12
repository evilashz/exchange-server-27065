using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020002F0 RID: 752
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class TooManyBadItemsPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x06002471 RID: 9329 RVA: 0x0005011F File Offset: 0x0004E31F
		public TooManyBadItemsPermanentException() : base(MrsStrings.TooManyBadItems)
		{
		}

		// Token: 0x06002472 RID: 9330 RVA: 0x0005012C File Offset: 0x0004E32C
		public TooManyBadItemsPermanentException(Exception innerException) : base(MrsStrings.TooManyBadItems, innerException)
		{
		}

		// Token: 0x06002473 RID: 9331 RVA: 0x0005013A File Offset: 0x0004E33A
		protected TooManyBadItemsPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06002474 RID: 9332 RVA: 0x00050144 File Offset: 0x0004E344
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
