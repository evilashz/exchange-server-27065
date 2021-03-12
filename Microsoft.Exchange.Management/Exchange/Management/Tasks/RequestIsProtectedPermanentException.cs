using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000ECA RID: 3786
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RequestIsProtectedPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x0600A8CB RID: 43211 RVA: 0x0028A929 File Offset: 0x00288B29
		public RequestIsProtectedPermanentException(string mailbox) : base(Strings.ErrorRequestIsProtected(mailbox))
		{
			this.mailbox = mailbox;
		}

		// Token: 0x0600A8CC RID: 43212 RVA: 0x0028A93E File Offset: 0x00288B3E
		public RequestIsProtectedPermanentException(string mailbox, Exception innerException) : base(Strings.ErrorRequestIsProtected(mailbox), innerException)
		{
			this.mailbox = mailbox;
		}

		// Token: 0x0600A8CD RID: 43213 RVA: 0x0028A954 File Offset: 0x00288B54
		protected RequestIsProtectedPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.mailbox = (string)info.GetValue("mailbox", typeof(string));
		}

		// Token: 0x0600A8CE RID: 43214 RVA: 0x0028A97E File Offset: 0x00288B7E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("mailbox", this.mailbox);
		}

		// Token: 0x170036C0 RID: 14016
		// (get) Token: 0x0600A8CF RID: 43215 RVA: 0x0028A999 File Offset: 0x00288B99
		public string Mailbox
		{
			get
			{
				return this.mailbox;
			}
		}

		// Token: 0x04006026 RID: 24614
		private readonly string mailbox;
	}
}
