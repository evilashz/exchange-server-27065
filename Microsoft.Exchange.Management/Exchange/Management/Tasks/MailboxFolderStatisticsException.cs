using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000FC0 RID: 4032
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MailboxFolderStatisticsException : LocalizedException
	{
		// Token: 0x0600AD95 RID: 44437 RVA: 0x00291E45 File Offset: 0x00290045
		public MailboxFolderStatisticsException(string mailbox, string failure) : base(Strings.MailboxFolderStatisticsException(mailbox, failure))
		{
			this.mailbox = mailbox;
			this.failure = failure;
		}

		// Token: 0x0600AD96 RID: 44438 RVA: 0x00291E62 File Offset: 0x00290062
		public MailboxFolderStatisticsException(string mailbox, string failure, Exception innerException) : base(Strings.MailboxFolderStatisticsException(mailbox, failure), innerException)
		{
			this.mailbox = mailbox;
			this.failure = failure;
		}

		// Token: 0x0600AD97 RID: 44439 RVA: 0x00291E80 File Offset: 0x00290080
		protected MailboxFolderStatisticsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.mailbox = (string)info.GetValue("mailbox", typeof(string));
			this.failure = (string)info.GetValue("failure", typeof(string));
		}

		// Token: 0x0600AD98 RID: 44440 RVA: 0x00291ED5 File Offset: 0x002900D5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("mailbox", this.mailbox);
			info.AddValue("failure", this.failure);
		}

		// Token: 0x170037B2 RID: 14258
		// (get) Token: 0x0600AD99 RID: 44441 RVA: 0x00291F01 File Offset: 0x00290101
		public string Mailbox
		{
			get
			{
				return this.mailbox;
			}
		}

		// Token: 0x170037B3 RID: 14259
		// (get) Token: 0x0600AD9A RID: 44442 RVA: 0x00291F09 File Offset: 0x00290109
		public string Failure
		{
			get
			{
				return this.failure;
			}
		}

		// Token: 0x04006118 RID: 24856
		private readonly string mailbox;

		// Token: 0x04006119 RID: 24857
		private readonly string failure;
	}
}
