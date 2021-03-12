using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000845 RID: 2117
	[XmlType(TypeName = "PhoneCallIdType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class PhoneCallId
	{
		// Token: 0x06003D01 RID: 15617 RVA: 0x000D700D File Offset: 0x000D520D
		public PhoneCallId()
		{
		}

		// Token: 0x06003D02 RID: 15618 RVA: 0x000D7015 File Offset: 0x000D5215
		internal PhoneCallId(string id)
		{
			this.Id = id;
		}

		// Token: 0x17000E8C RID: 3724
		// (get) Token: 0x06003D03 RID: 15619 RVA: 0x000D7024 File Offset: 0x000D5224
		// (set) Token: 0x06003D04 RID: 15620 RVA: 0x000D702C File Offset: 0x000D522C
		[DataMember(IsRequired = true)]
		[XmlAttribute("Id", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		public string Id { get; set; }
	}
}
