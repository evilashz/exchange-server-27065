using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020005BA RID: 1466
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class EmailAddressEntityType : BaseEntityType
	{
		// Token: 0x170008EB RID: 2283
		// (get) Token: 0x06002CAA RID: 11434 RVA: 0x000B1314 File Offset: 0x000AF514
		// (set) Token: 0x06002CAB RID: 11435 RVA: 0x000B131C File Offset: 0x000AF51C
		[XmlElement]
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public string EmailAddress { get; set; }
	}
}
