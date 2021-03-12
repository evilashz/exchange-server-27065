using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Transport.Sync.Common
{
	// Token: 0x0200005C RID: 92
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class MailboxOverQuotaException : TransientException
	{
		// Token: 0x0600025D RID: 605 RVA: 0x00006AFC File Offset: 0x00004CFC
		public MailboxOverQuotaException() : base(Strings.MailboxOverQuotaException)
		{
		}

		// Token: 0x0600025E RID: 606 RVA: 0x00006B09 File Offset: 0x00004D09
		public MailboxOverQuotaException(Exception innerException) : base(Strings.MailboxOverQuotaException, innerException)
		{
		}

		// Token: 0x0600025F RID: 607 RVA: 0x00006B17 File Offset: 0x00004D17
		protected MailboxOverQuotaException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000260 RID: 608 RVA: 0x00006B21 File Offset: 0x00004D21
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
