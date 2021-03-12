using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000EB0 RID: 3760
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MailboxRelocationSplitSourceNotInContainerException : MailboxReplicationPermanentException
	{
		// Token: 0x0600A846 RID: 43078 RVA: 0x00289BD1 File Offset: 0x00287DD1
		public MailboxRelocationSplitSourceNotInContainerException(string mailbox) : base(Strings.ErrorMailboxRelocationSplitSourceNotInContainer(mailbox))
		{
			this.mailbox = mailbox;
		}

		// Token: 0x0600A847 RID: 43079 RVA: 0x00289BE6 File Offset: 0x00287DE6
		public MailboxRelocationSplitSourceNotInContainerException(string mailbox, Exception innerException) : base(Strings.ErrorMailboxRelocationSplitSourceNotInContainer(mailbox), innerException)
		{
			this.mailbox = mailbox;
		}

		// Token: 0x0600A848 RID: 43080 RVA: 0x00289BFC File Offset: 0x00287DFC
		protected MailboxRelocationSplitSourceNotInContainerException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.mailbox = (string)info.GetValue("mailbox", typeof(string));
		}

		// Token: 0x0600A849 RID: 43081 RVA: 0x00289C26 File Offset: 0x00287E26
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("mailbox", this.mailbox);
		}

		// Token: 0x170036A3 RID: 13987
		// (get) Token: 0x0600A84A RID: 43082 RVA: 0x00289C41 File Offset: 0x00287E41
		public string Mailbox
		{
			get
			{
				return this.mailbox;
			}
		}

		// Token: 0x04006009 RID: 24585
		private readonly string mailbox;
	}
}
