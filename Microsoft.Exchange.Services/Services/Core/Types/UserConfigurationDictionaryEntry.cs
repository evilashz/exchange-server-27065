using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000691 RID: 1681
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType("UserConfigurationDictionaryEntryType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class UserConfigurationDictionaryEntry
	{
		// Token: 0x17000BEA RID: 3050
		// (get) Token: 0x06003392 RID: 13202 RVA: 0x000B89A5 File Offset: 0x000B6BA5
		// (set) Token: 0x06003393 RID: 13203 RVA: 0x000B89AD File Offset: 0x000B6BAD
		[DataMember(IsRequired = true)]
		[XmlElement]
		public UserConfigurationDictionaryObject DictionaryKey { get; set; }

		// Token: 0x17000BEB RID: 3051
		// (get) Token: 0x06003394 RID: 13204 RVA: 0x000B89B6 File Offset: 0x000B6BB6
		// (set) Token: 0x06003395 RID: 13205 RVA: 0x000B89BE File Offset: 0x000B6BBE
		[DataMember(IsRequired = false)]
		[XmlElement(IsNullable = true)]
		public UserConfigurationDictionaryObject DictionaryValue { get; set; }
	}
}
