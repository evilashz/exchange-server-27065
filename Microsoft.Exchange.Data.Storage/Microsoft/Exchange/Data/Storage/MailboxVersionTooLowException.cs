using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000116 RID: 278
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MailboxVersionTooLowException : DataSourceOperationException
	{
		// Token: 0x060013FE RID: 5118 RVA: 0x0006A172 File Offset: 0x00068372
		public MailboxVersionTooLowException(string mailbox, string expectedVersion, string actualVersion) : base(ServerStrings.MailboxVersionTooLow(mailbox, expectedVersion, actualVersion))
		{
			this.mailbox = mailbox;
			this.expectedVersion = expectedVersion;
			this.actualVersion = actualVersion;
		}

		// Token: 0x060013FF RID: 5119 RVA: 0x0006A197 File Offset: 0x00068397
		public MailboxVersionTooLowException(string mailbox, string expectedVersion, string actualVersion, Exception innerException) : base(ServerStrings.MailboxVersionTooLow(mailbox, expectedVersion, actualVersion), innerException)
		{
			this.mailbox = mailbox;
			this.expectedVersion = expectedVersion;
			this.actualVersion = actualVersion;
		}

		// Token: 0x06001400 RID: 5120 RVA: 0x0006A1C0 File Offset: 0x000683C0
		protected MailboxVersionTooLowException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.mailbox = (string)info.GetValue("mailbox", typeof(string));
			this.expectedVersion = (string)info.GetValue("expectedVersion", typeof(string));
			this.actualVersion = (string)info.GetValue("actualVersion", typeof(string));
		}

		// Token: 0x06001401 RID: 5121 RVA: 0x0006A235 File Offset: 0x00068435
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("mailbox", this.mailbox);
			info.AddValue("expectedVersion", this.expectedVersion);
			info.AddValue("actualVersion", this.actualVersion);
		}

		// Token: 0x1700068E RID: 1678
		// (get) Token: 0x06001402 RID: 5122 RVA: 0x0006A272 File Offset: 0x00068472
		public string Mailbox
		{
			get
			{
				return this.mailbox;
			}
		}

		// Token: 0x1700068F RID: 1679
		// (get) Token: 0x06001403 RID: 5123 RVA: 0x0006A27A File Offset: 0x0006847A
		public string ExpectedVersion
		{
			get
			{
				return this.expectedVersion;
			}
		}

		// Token: 0x17000690 RID: 1680
		// (get) Token: 0x06001404 RID: 5124 RVA: 0x0006A282 File Offset: 0x00068482
		public string ActualVersion
		{
			get
			{
				return this.actualVersion;
			}
		}

		// Token: 0x040009A5 RID: 2469
		private readonly string mailbox;

		// Token: 0x040009A6 RID: 2470
		private readonly string expectedVersion;

		// Token: 0x040009A7 RID: 2471
		private readonly string actualVersion;
	}
}
