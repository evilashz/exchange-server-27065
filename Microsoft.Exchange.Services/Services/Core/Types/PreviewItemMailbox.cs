using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020007F7 RID: 2039
	[DataContract(Name = "PreviewItemMailbox", Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(TypeName = "PreviewItemMailboxType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class PreviewItemMailbox
	{
		// Token: 0x06003B9F RID: 15263 RVA: 0x000D033A File Offset: 0x000CE53A
		public PreviewItemMailbox()
		{
		}

		// Token: 0x06003BA0 RID: 15264 RVA: 0x000D0342 File Offset: 0x000CE542
		public PreviewItemMailbox(string mailboxId, string primarySmtpAddress)
		{
			this.MailboxId = mailboxId;
			this.PrimarySmtpAddress = primarySmtpAddress;
		}

		// Token: 0x17000E24 RID: 3620
		// (get) Token: 0x06003BA1 RID: 15265 RVA: 0x000D0358 File Offset: 0x000CE558
		// (set) Token: 0x06003BA2 RID: 15266 RVA: 0x000D0360 File Offset: 0x000CE560
		[DataMember(Name = "MailboxId", IsRequired = true)]
		[XmlElement("MailboxId")]
		public string MailboxId { get; set; }

		// Token: 0x17000E25 RID: 3621
		// (get) Token: 0x06003BA3 RID: 15267 RVA: 0x000D0369 File Offset: 0x000CE569
		// (set) Token: 0x06003BA4 RID: 15268 RVA: 0x000D0384 File Offset: 0x000CE584
		[DataMember(Name = "PrimarySmtpAddress", IsRequired = true)]
		[XmlElement("PrimarySmtpAddress")]
		public string PrimarySmtpAddress
		{
			get
			{
				if (string.IsNullOrEmpty(this.primarySmtpAddres))
				{
					return string.Empty;
				}
				return this.primarySmtpAddres;
			}
			set
			{
				this.primarySmtpAddres = value;
			}
		}

		// Token: 0x040020DE RID: 8414
		private string primarySmtpAddres;
	}
}
