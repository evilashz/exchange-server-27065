using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000EB1 RID: 3761
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MailboxRelocationJoinTargetNotContainerException : MailboxReplicationPermanentException
	{
		// Token: 0x0600A84B RID: 43083 RVA: 0x00289C49 File Offset: 0x00287E49
		public MailboxRelocationJoinTargetNotContainerException(string mailbox) : base(Strings.ErrorMailboxRelocationJoinTargetNotContainer(mailbox))
		{
			this.mailbox = mailbox;
		}

		// Token: 0x0600A84C RID: 43084 RVA: 0x00289C5E File Offset: 0x00287E5E
		public MailboxRelocationJoinTargetNotContainerException(string mailbox, Exception innerException) : base(Strings.ErrorMailboxRelocationJoinTargetNotContainer(mailbox), innerException)
		{
			this.mailbox = mailbox;
		}

		// Token: 0x0600A84D RID: 43085 RVA: 0x00289C74 File Offset: 0x00287E74
		protected MailboxRelocationJoinTargetNotContainerException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.mailbox = (string)info.GetValue("mailbox", typeof(string));
		}

		// Token: 0x0600A84E RID: 43086 RVA: 0x00289C9E File Offset: 0x00287E9E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("mailbox", this.mailbox);
		}

		// Token: 0x170036A4 RID: 13988
		// (get) Token: 0x0600A84F RID: 43087 RVA: 0x00289CB9 File Offset: 0x00287EB9
		public string Mailbox
		{
			get
			{
				return this.mailbox;
			}
		}

		// Token: 0x0400600A RID: 24586
		private readonly string mailbox;
	}
}
