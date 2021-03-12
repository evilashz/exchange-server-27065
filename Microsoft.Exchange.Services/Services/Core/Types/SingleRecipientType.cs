using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000650 RID: 1616
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class SingleRecipientType
	{
		// Token: 0x17000B3D RID: 2877
		// (get) Token: 0x060031FC RID: 12796 RVA: 0x000B76BF File Offset: 0x000B58BF
		// (set) Token: 0x060031FD RID: 12797 RVA: 0x000B76C7 File Offset: 0x000B58C7
		[DataMember(Name = "Mailbox", EmitDefaultValue = false)]
		[XmlElement("Mailbox")]
		public EmailAddressWrapper Mailbox { get; set; }
	}
}
