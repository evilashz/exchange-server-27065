using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200116A RID: 4458
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FailedMailboxQuarantineException : LocalizedException
	{
		// Token: 0x0600B5FC RID: 46588 RVA: 0x0029F17A File Offset: 0x0029D37A
		public FailedMailboxQuarantineException(string mailbox, string failure) : base(Strings.FailedMailboxQuarantine(mailbox, failure))
		{
			this.mailbox = mailbox;
			this.failure = failure;
		}

		// Token: 0x0600B5FD RID: 46589 RVA: 0x0029F197 File Offset: 0x0029D397
		public FailedMailboxQuarantineException(string mailbox, string failure, Exception innerException) : base(Strings.FailedMailboxQuarantine(mailbox, failure), innerException)
		{
			this.mailbox = mailbox;
			this.failure = failure;
		}

		// Token: 0x0600B5FE RID: 46590 RVA: 0x0029F1B8 File Offset: 0x0029D3B8
		protected FailedMailboxQuarantineException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.mailbox = (string)info.GetValue("mailbox", typeof(string));
			this.failure = (string)info.GetValue("failure", typeof(string));
		}

		// Token: 0x0600B5FF RID: 46591 RVA: 0x0029F20D File Offset: 0x0029D40D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("mailbox", this.mailbox);
			info.AddValue("failure", this.failure);
		}

		// Token: 0x17003971 RID: 14705
		// (get) Token: 0x0600B600 RID: 46592 RVA: 0x0029F239 File Offset: 0x0029D439
		public string Mailbox
		{
			get
			{
				return this.mailbox;
			}
		}

		// Token: 0x17003972 RID: 14706
		// (get) Token: 0x0600B601 RID: 46593 RVA: 0x0029F241 File Offset: 0x0029D441
		public string Failure
		{
			get
			{
				return this.failure;
			}
		}

		// Token: 0x040062D7 RID: 25303
		private readonly string mailbox;

		// Token: 0x040062D8 RID: 25304
		private readonly string failure;
	}
}
