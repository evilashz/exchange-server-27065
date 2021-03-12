using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000610 RID: 1552
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class PhoneType
	{
		// Token: 0x17000AA5 RID: 2725
		// (get) Token: 0x06003095 RID: 12437 RVA: 0x000B667F File Offset: 0x000B487F
		// (set) Token: 0x06003096 RID: 12438 RVA: 0x000B6687 File Offset: 0x000B4887
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public string OriginalPhoneString { get; set; }

		// Token: 0x17000AA6 RID: 2726
		// (get) Token: 0x06003097 RID: 12439 RVA: 0x000B6690 File Offset: 0x000B4890
		// (set) Token: 0x06003098 RID: 12440 RVA: 0x000B6698 File Offset: 0x000B4898
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public string PhoneString { get; set; }

		// Token: 0x17000AA7 RID: 2727
		// (get) Token: 0x06003099 RID: 12441 RVA: 0x000B66A1 File Offset: 0x000B48A1
		// (set) Token: 0x0600309A RID: 12442 RVA: 0x000B66A9 File Offset: 0x000B48A9
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public string Type { get; set; }
	}
}
