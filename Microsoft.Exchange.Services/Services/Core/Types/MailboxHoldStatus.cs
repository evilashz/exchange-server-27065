using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020007F0 RID: 2032
	[DataContract(Name = "MailboxHoldStatus", Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(TypeName = "MailboxHoldStatusType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class MailboxHoldStatus
	{
		// Token: 0x06003B72 RID: 15218 RVA: 0x000D0144 File Offset: 0x000CE344
		public MailboxHoldStatus()
		{
		}

		// Token: 0x06003B73 RID: 15219 RVA: 0x000D014C File Offset: 0x000CE34C
		internal MailboxHoldStatus(string mailbox, HoldStatus status, string additionalInfo)
		{
			this.mailbox = mailbox;
			this.status = status;
			this.additionalInfo = additionalInfo;
		}

		// Token: 0x17000E13 RID: 3603
		// (get) Token: 0x06003B74 RID: 15220 RVA: 0x000D0169 File Offset: 0x000CE369
		// (set) Token: 0x06003B75 RID: 15221 RVA: 0x000D0171 File Offset: 0x000CE371
		[XmlElement("Mailbox")]
		[DataMember(Name = "Mailbox", IsRequired = true)]
		public string Mailbox
		{
			get
			{
				return this.mailbox;
			}
			set
			{
				this.mailbox = value;
			}
		}

		// Token: 0x17000E14 RID: 3604
		// (get) Token: 0x06003B76 RID: 15222 RVA: 0x000D017A File Offset: 0x000CE37A
		// (set) Token: 0x06003B77 RID: 15223 RVA: 0x000D0182 File Offset: 0x000CE382
		[XmlElement("Status")]
		[IgnoreDataMember]
		public HoldStatus Status
		{
			get
			{
				return this.status;
			}
			set
			{
				this.status = value;
			}
		}

		// Token: 0x17000E15 RID: 3605
		// (get) Token: 0x06003B78 RID: 15224 RVA: 0x000D018B File Offset: 0x000CE38B
		// (set) Token: 0x06003B79 RID: 15225 RVA: 0x000D0198 File Offset: 0x000CE398
		[DataMember(Name = "Status", IsRequired = true)]
		[XmlIgnore]
		public string StatusString
		{
			get
			{
				return EnumUtilities.ToString<HoldStatus>(this.status);
			}
			set
			{
				this.status = EnumUtilities.Parse<HoldStatus>(value);
			}
		}

		// Token: 0x17000E16 RID: 3606
		// (get) Token: 0x06003B7A RID: 15226 RVA: 0x000D01A6 File Offset: 0x000CE3A6
		// (set) Token: 0x06003B7B RID: 15227 RVA: 0x000D01AE File Offset: 0x000CE3AE
		[XmlElement("AdditionalInfo")]
		[DataMember(Name = "AdditionalInfo", IsRequired = false)]
		public string AdditionalInfo
		{
			get
			{
				return this.additionalInfo;
			}
			set
			{
				this.additionalInfo = value;
			}
		}

		// Token: 0x040020CD RID: 8397
		private string mailbox;

		// Token: 0x040020CE RID: 8398
		private HoldStatus status;

		// Token: 0x040020CF RID: 8399
		private string additionalInfo;
	}
}
