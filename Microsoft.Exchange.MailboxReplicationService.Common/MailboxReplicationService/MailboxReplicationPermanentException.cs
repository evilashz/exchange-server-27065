using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000107 RID: 263
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MailboxReplicationPermanentException : LocalizedException
	{
		// Token: 0x0600094F RID: 2383 RVA: 0x00012A1F File Offset: 0x00010C1F
		public MailboxReplicationPermanentException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06000950 RID: 2384 RVA: 0x00012A28 File Offset: 0x00010C28
		public MailboxReplicationPermanentException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000951 RID: 2385 RVA: 0x00012A32 File Offset: 0x00010C32
		protected MailboxReplicationPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000952 RID: 2386 RVA: 0x00012A3C File Offset: 0x00010C3C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
