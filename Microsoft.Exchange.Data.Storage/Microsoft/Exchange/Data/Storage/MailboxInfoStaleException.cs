using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020000BE RID: 190
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class MailboxInfoStaleException : ConnectionFailedPermanentException
	{
		// Token: 0x06001229 RID: 4649 RVA: 0x0006755F File Offset: 0x0006575F
		public MailboxInfoStaleException(string mailboxId) : base(ServerStrings.idMailboxInfoStaleException(mailboxId))
		{
			this.mailboxId = mailboxId;
		}

		// Token: 0x0600122A RID: 4650 RVA: 0x00067574 File Offset: 0x00065774
		public MailboxInfoStaleException(string mailboxId, Exception innerException) : base(ServerStrings.idMailboxInfoStaleException(mailboxId), innerException)
		{
			this.mailboxId = mailboxId;
		}

		// Token: 0x0600122B RID: 4651 RVA: 0x0006758A File Offset: 0x0006578A
		protected MailboxInfoStaleException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.mailboxId = (string)info.GetValue("mailboxId", typeof(string));
		}

		// Token: 0x0600122C RID: 4652 RVA: 0x000675B4 File Offset: 0x000657B4
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("mailboxId", this.mailboxId);
		}

		// Token: 0x1700061F RID: 1567
		// (get) Token: 0x0600122D RID: 4653 RVA: 0x000675CF File Offset: 0x000657CF
		public string MailboxId
		{
			get
			{
				return this.mailboxId;
			}
		}

		// Token: 0x04000950 RID: 2384
		private readonly string mailboxId;
	}
}
