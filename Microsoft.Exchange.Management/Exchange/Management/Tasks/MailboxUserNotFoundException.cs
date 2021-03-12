using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200119F RID: 4511
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MailboxUserNotFoundException : LocalizedException
	{
		// Token: 0x0600B70C RID: 46860 RVA: 0x002A0D2E File Offset: 0x0029EF2E
		public MailboxUserNotFoundException(string mailboxName) : base(Strings.MailboxUserNotFoundException(mailboxName))
		{
			this.mailboxName = mailboxName;
		}

		// Token: 0x0600B70D RID: 46861 RVA: 0x002A0D43 File Offset: 0x0029EF43
		public MailboxUserNotFoundException(string mailboxName, Exception innerException) : base(Strings.MailboxUserNotFoundException(mailboxName), innerException)
		{
			this.mailboxName = mailboxName;
		}

		// Token: 0x0600B70E RID: 46862 RVA: 0x002A0D59 File Offset: 0x0029EF59
		protected MailboxUserNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.mailboxName = (string)info.GetValue("mailboxName", typeof(string));
		}

		// Token: 0x0600B70F RID: 46863 RVA: 0x002A0D83 File Offset: 0x0029EF83
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("mailboxName", this.mailboxName);
		}

		// Token: 0x170039AD RID: 14765
		// (get) Token: 0x0600B710 RID: 46864 RVA: 0x002A0D9E File Offset: 0x0029EF9E
		public string MailboxName
		{
			get
			{
				return this.mailboxName;
			}
		}

		// Token: 0x04006313 RID: 25363
		private readonly string mailboxName;
	}
}
