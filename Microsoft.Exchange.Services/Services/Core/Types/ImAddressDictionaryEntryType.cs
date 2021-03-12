using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020005E0 RID: 1504
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class ImAddressDictionaryEntryType
	{
		// Token: 0x06002D43 RID: 11587 RVA: 0x000B1F45 File Offset: 0x000B0145
		public ImAddressDictionaryEntryType()
		{
		}

		// Token: 0x06002D44 RID: 11588 RVA: 0x000B1F4D File Offset: 0x000B014D
		public ImAddressDictionaryEntryType(ImAddressKeyType key, string value)
		{
			this.Key = key;
			this.Value = value;
		}

		// Token: 0x17000925 RID: 2341
		// (get) Token: 0x06002D45 RID: 11589 RVA: 0x000B1F63 File Offset: 0x000B0163
		// (set) Token: 0x06002D46 RID: 11590 RVA: 0x000B1F6B File Offset: 0x000B016B
		[IgnoreDataMember]
		[XmlAttribute]
		public ImAddressKeyType Key { get; set; }

		// Token: 0x17000926 RID: 2342
		// (get) Token: 0x06002D47 RID: 11591 RVA: 0x000B1F74 File Offset: 0x000B0174
		// (set) Token: 0x06002D48 RID: 11592 RVA: 0x000B1F81 File Offset: 0x000B0181
		[XmlIgnore]
		[DataMember(Name = "Key", EmitDefaultValue = false, Order = 0)]
		public string KeyString
		{
			get
			{
				return EnumUtilities.ToString<ImAddressKeyType>(this.Key);
			}
			set
			{
				this.Key = EnumUtilities.Parse<ImAddressKeyType>(value);
			}
		}

		// Token: 0x17000927 RID: 2343
		// (get) Token: 0x06002D49 RID: 11593 RVA: 0x000B1F8F File Offset: 0x000B018F
		// (set) Token: 0x06002D4A RID: 11594 RVA: 0x000B1F97 File Offset: 0x000B0197
		[DataMember(Name = "ImAddress", EmitDefaultValue = false, Order = 1)]
		[XmlText]
		public string Value { get; set; }
	}
}
