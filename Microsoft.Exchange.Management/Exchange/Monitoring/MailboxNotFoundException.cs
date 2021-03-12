using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x020010FF RID: 4351
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MailboxNotFoundException : LocalizedException
	{
		// Token: 0x0600B3E3 RID: 46051 RVA: 0x0029BD85 File Offset: 0x00299F85
		public MailboxNotFoundException(MailboxIdParameter mailbox) : base(Strings.messageMailboxNotFoundException(mailbox))
		{
			this.mailbox = mailbox;
		}

		// Token: 0x0600B3E4 RID: 46052 RVA: 0x0029BD9A File Offset: 0x00299F9A
		public MailboxNotFoundException(MailboxIdParameter mailbox, Exception innerException) : base(Strings.messageMailboxNotFoundException(mailbox), innerException)
		{
			this.mailbox = mailbox;
		}

		// Token: 0x0600B3E5 RID: 46053 RVA: 0x0029BDB0 File Offset: 0x00299FB0
		protected MailboxNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.mailbox = (MailboxIdParameter)info.GetValue("mailbox", typeof(MailboxIdParameter));
		}

		// Token: 0x0600B3E6 RID: 46054 RVA: 0x0029BDDA File Offset: 0x00299FDA
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("mailbox", this.mailbox);
		}

		// Token: 0x17003904 RID: 14596
		// (get) Token: 0x0600B3E7 RID: 46055 RVA: 0x0029BDF5 File Offset: 0x00299FF5
		public MailboxIdParameter Mailbox
		{
			get
			{
				return this.mailbox;
			}
		}

		// Token: 0x0400626A RID: 25194
		private readonly MailboxIdParameter mailbox;
	}
}
