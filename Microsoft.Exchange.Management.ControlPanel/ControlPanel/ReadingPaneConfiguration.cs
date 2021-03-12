using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200009D RID: 157
	[DataContract]
	public class ReadingPaneConfiguration : MessagingConfigurationBase
	{
		// Token: 0x06001BF4 RID: 7156 RVA: 0x00057BAF File Offset: 0x00055DAF
		public ReadingPaneConfiguration(MailboxMessageConfiguration mailboxMessageConfiguration) : base(mailboxMessageConfiguration)
		{
		}

		// Token: 0x170018C7 RID: 6343
		// (get) Token: 0x06001BF5 RID: 7157 RVA: 0x00057BB8 File Offset: 0x00055DB8
		// (set) Token: 0x06001BF6 RID: 7158 RVA: 0x00057BCF File Offset: 0x00055DCF
		[DataMember]
		public string PreviewMarkAsReadBehavior
		{
			get
			{
				return base.MailboxMessageConfiguration.PreviewMarkAsReadBehavior.ToString();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170018C8 RID: 6344
		// (get) Token: 0x06001BF7 RID: 7159 RVA: 0x00057BD6 File Offset: 0x00055DD6
		// (set) Token: 0x06001BF8 RID: 7160 RVA: 0x00057BE3 File Offset: 0x00055DE3
		[DataMember]
		public int PreviewMarkAsReadDelaytime
		{
			get
			{
				return base.MailboxMessageConfiguration.PreviewMarkAsReadDelaytime;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170018C9 RID: 6345
		// (get) Token: 0x06001BF9 RID: 7161 RVA: 0x00057BEA File Offset: 0x00055DEA
		// (set) Token: 0x06001BFA RID: 7162 RVA: 0x00057C01 File Offset: 0x00055E01
		[DataMember]
		public string EmailComposeMode
		{
			get
			{
				return base.MailboxMessageConfiguration.EmailComposeMode.ToString();
			}
			set
			{
				throw new NotSupportedException();
			}
		}
	}
}
