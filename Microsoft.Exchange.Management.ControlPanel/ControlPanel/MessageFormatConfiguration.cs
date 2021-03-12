using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000094 RID: 148
	[DataContract]
	public class MessageFormatConfiguration : MessagingConfigurationBase
	{
		// Token: 0x06001BBB RID: 7099 RVA: 0x00057787 File Offset: 0x00055987
		public MessageFormatConfiguration(MailboxMessageConfiguration mailboxMessageConfiguration) : base(mailboxMessageConfiguration)
		{
		}

		// Token: 0x170018B0 RID: 6320
		// (get) Token: 0x06001BBC RID: 7100 RVA: 0x00057790 File Offset: 0x00055990
		// (set) Token: 0x06001BBD RID: 7101 RVA: 0x0005779D File Offset: 0x0005599D
		[DataMember]
		public bool AlwaysShowBcc
		{
			get
			{
				return base.MailboxMessageConfiguration.AlwaysShowBcc;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170018B1 RID: 6321
		// (get) Token: 0x06001BBE RID: 7102 RVA: 0x000577A4 File Offset: 0x000559A4
		// (set) Token: 0x06001BBF RID: 7103 RVA: 0x000577B1 File Offset: 0x000559B1
		[DataMember]
		public bool AlwaysShowFrom
		{
			get
			{
				return base.MailboxMessageConfiguration.AlwaysShowFrom;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170018B2 RID: 6322
		// (get) Token: 0x06001BC0 RID: 7104 RVA: 0x000577B8 File Offset: 0x000559B8
		// (set) Token: 0x06001BC1 RID: 7105 RVA: 0x000577CF File Offset: 0x000559CF
		[DataMember]
		public string DefaultFormat
		{
			get
			{
				return base.MailboxMessageConfiguration.DefaultFormat.ToString();
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170018B3 RID: 6323
		// (get) Token: 0x06001BC2 RID: 7106 RVA: 0x000577D8 File Offset: 0x000559D8
		// (set) Token: 0x06001BC3 RID: 7107 RVA: 0x0005781F File Offset: 0x00055A1F
		[DataMember]
		public FormatBarState MessageFont
		{
			get
			{
				return new FormatBarState(base.MailboxMessageConfiguration.DefaultFontName, base.MailboxMessageConfiguration.DefaultFontSize, base.MailboxMessageConfiguration.DefaultFontFlags, base.MailboxMessageConfiguration.DefaultFontColor.Remove(0, 1));
			}
			private set
			{
				throw new NotSupportedException();
			}
		}
	}
}
