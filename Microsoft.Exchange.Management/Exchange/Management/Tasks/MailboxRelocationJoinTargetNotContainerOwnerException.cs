using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000EB2 RID: 3762
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MailboxRelocationJoinTargetNotContainerOwnerException : MailboxReplicationPermanentException
	{
		// Token: 0x0600A850 RID: 43088 RVA: 0x00289CC1 File Offset: 0x00287EC1
		public MailboxRelocationJoinTargetNotContainerOwnerException(string mailbox) : base(Strings.ErrorMailboxRelocationJoinTargetNotContainerOwner(mailbox))
		{
			this.mailbox = mailbox;
		}

		// Token: 0x0600A851 RID: 43089 RVA: 0x00289CD6 File Offset: 0x00287ED6
		public MailboxRelocationJoinTargetNotContainerOwnerException(string mailbox, Exception innerException) : base(Strings.ErrorMailboxRelocationJoinTargetNotContainerOwner(mailbox), innerException)
		{
			this.mailbox = mailbox;
		}

		// Token: 0x0600A852 RID: 43090 RVA: 0x00289CEC File Offset: 0x00287EEC
		protected MailboxRelocationJoinTargetNotContainerOwnerException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.mailbox = (string)info.GetValue("mailbox", typeof(string));
		}

		// Token: 0x0600A853 RID: 43091 RVA: 0x00289D16 File Offset: 0x00287F16
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("mailbox", this.mailbox);
		}

		// Token: 0x170036A5 RID: 13989
		// (get) Token: 0x0600A854 RID: 43092 RVA: 0x00289D31 File Offset: 0x00287F31
		public string Mailbox
		{
			get
			{
				return this.mailbox;
			}
		}

		// Token: 0x0400600B RID: 24587
		private readonly string mailbox;
	}
}
