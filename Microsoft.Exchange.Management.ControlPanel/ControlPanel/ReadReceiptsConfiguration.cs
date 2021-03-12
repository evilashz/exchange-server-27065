using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020000A2 RID: 162
	[DataContract]
	public class ReadReceiptsConfiguration : MessagingConfigurationBase
	{
		// Token: 0x06001C08 RID: 7176 RVA: 0x00057D1B File Offset: 0x00055F1B
		public ReadReceiptsConfiguration(MailboxMessageConfiguration mailboxMessageConfiguration) : base(mailboxMessageConfiguration)
		{
		}

		// Token: 0x170018CD RID: 6349
		// (get) Token: 0x06001C09 RID: 7177 RVA: 0x00057D24 File Offset: 0x00055F24
		// (set) Token: 0x06001C0A RID: 7178 RVA: 0x00057D3B File Offset: 0x00055F3B
		[DataMember]
		public string ReadReceiptResponse
		{
			get
			{
				return base.MailboxMessageConfiguration.ReadReceiptResponse.ToString();
			}
			set
			{
				throw new NotSupportedException();
			}
		}
	}
}
