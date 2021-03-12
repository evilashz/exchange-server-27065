using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02001149 RID: 4425
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MailboxIsNotLockedException : LocalizedException
	{
		// Token: 0x0600B552 RID: 46418 RVA: 0x0029E04D File Offset: 0x0029C24D
		public MailboxIsNotLockedException(string mailboxId) : base(Strings.MailboxIsNotLocked(mailboxId))
		{
			this.mailboxId = mailboxId;
		}

		// Token: 0x0600B553 RID: 46419 RVA: 0x0029E062 File Offset: 0x0029C262
		public MailboxIsNotLockedException(string mailboxId, Exception innerException) : base(Strings.MailboxIsNotLocked(mailboxId), innerException)
		{
			this.mailboxId = mailboxId;
		}

		// Token: 0x0600B554 RID: 46420 RVA: 0x0029E078 File Offset: 0x0029C278
		protected MailboxIsNotLockedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.mailboxId = (string)info.GetValue("mailboxId", typeof(string));
		}

		// Token: 0x0600B555 RID: 46421 RVA: 0x0029E0A2 File Offset: 0x0029C2A2
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("mailboxId", this.mailboxId);
		}

		// Token: 0x1700394B RID: 14667
		// (get) Token: 0x0600B556 RID: 46422 RVA: 0x0029E0BD File Offset: 0x0029C2BD
		public string MailboxId
		{
			get
			{
				return this.mailboxId;
			}
		}

		// Token: 0x040062B1 RID: 25265
		private readonly string mailboxId;
	}
}
