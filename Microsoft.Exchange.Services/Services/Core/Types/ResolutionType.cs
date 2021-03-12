using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000632 RID: 1586
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange", Name = "Resolution")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", TypeName = "Resolution")]
	[Serializable]
	public class ResolutionType
	{
		// Token: 0x17000AFD RID: 2813
		// (get) Token: 0x06003170 RID: 12656 RVA: 0x000B7130 File Offset: 0x000B5330
		// (set) Token: 0x06003171 RID: 12657 RVA: 0x000B7138 File Offset: 0x000B5338
		[DataMember(Name = "Mailbox", EmitDefaultValue = false, Order = 1)]
		[XmlElement("Mailbox")]
		public EmailAddressWrapper Mailbox
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

		// Token: 0x17000AFE RID: 2814
		// (get) Token: 0x06003172 RID: 12658 RVA: 0x000B7141 File Offset: 0x000B5341
		// (set) Token: 0x06003173 RID: 12659 RVA: 0x000B7149 File Offset: 0x000B5349
		[DataMember(Name = "Contact", EmitDefaultValue = false, Order = 2)]
		[XmlElement("Contact")]
		public ContactItemType Contact
		{
			get
			{
				return this.contact;
			}
			set
			{
				this.contact = value;
			}
		}

		// Token: 0x04001C53 RID: 7251
		private EmailAddressWrapper mailbox;

		// Token: 0x04001C54 RID: 7252
		private ContactItemType contact;
	}
}
