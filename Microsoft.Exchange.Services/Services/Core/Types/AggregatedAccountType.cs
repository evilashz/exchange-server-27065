using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200059D RID: 1437
	[DataContract]
	[XmlType(TypeName = "AggregatedAccountType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[KnownType(typeof(ConnectionSettingsInfo))]
	[XmlInclude(typeof(ConnectionSettingsInfo))]
	[Serializable]
	public class AggregatedAccountType
	{
		// Token: 0x060028A8 RID: 10408 RVA: 0x000AC762 File Offset: 0x000AA962
		public AggregatedAccountType()
		{
		}

		// Token: 0x060028A9 RID: 10409 RVA: 0x000AC76A File Offset: 0x000AA96A
		internal AggregatedAccountType(Guid mailboxGuid, Guid subscriptionGuid, string emailAddress, string userName, ConnectionSettingsInfo connectionSettings)
		{
			this.MailboxGuid = mailboxGuid;
			this.SubscriptionGuid = subscriptionGuid;
			this.EmailAddress = emailAddress;
			this.UserName = userName;
			this.ConnectionSettings = connectionSettings;
		}

		// Token: 0x17000712 RID: 1810
		// (get) Token: 0x060028AA RID: 10410 RVA: 0x000AC797 File Offset: 0x000AA997
		// (set) Token: 0x060028AB RID: 10411 RVA: 0x000AC79F File Offset: 0x000AA99F
		[XmlElement("MailboxGuid")]
		[DataMember]
		public Guid MailboxGuid { get; set; }

		// Token: 0x17000713 RID: 1811
		// (get) Token: 0x060028AC RID: 10412 RVA: 0x000AC7A8 File Offset: 0x000AA9A8
		// (set) Token: 0x060028AD RID: 10413 RVA: 0x000AC7B0 File Offset: 0x000AA9B0
		[DataMember]
		[XmlElement("SubscriptionGuid")]
		public Guid SubscriptionGuid { get; set; }

		// Token: 0x17000714 RID: 1812
		// (get) Token: 0x060028AE RID: 10414 RVA: 0x000AC7B9 File Offset: 0x000AA9B9
		// (set) Token: 0x060028AF RID: 10415 RVA: 0x000AC7C1 File Offset: 0x000AA9C1
		[DataMember]
		[XmlElement("EmailAddress")]
		public string EmailAddress { get; set; }

		// Token: 0x17000715 RID: 1813
		// (get) Token: 0x060028B0 RID: 10416 RVA: 0x000AC7CA File Offset: 0x000AA9CA
		// (set) Token: 0x060028B1 RID: 10417 RVA: 0x000AC7D2 File Offset: 0x000AA9D2
		[XmlElement("UserName")]
		[DataMember]
		public string UserName { get; set; }

		// Token: 0x17000716 RID: 1814
		// (get) Token: 0x060028B2 RID: 10418 RVA: 0x000AC7DB File Offset: 0x000AA9DB
		// (set) Token: 0x060028B3 RID: 10419 RVA: 0x000AC7E3 File Offset: 0x000AA9E3
		[XmlElement("ConnectionSettings")]
		[DataMember]
		public ConnectionSettingsInfo ConnectionSettings { get; set; }
	}
}
