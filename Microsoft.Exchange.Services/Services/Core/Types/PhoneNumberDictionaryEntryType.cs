using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200060F RID: 1551
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class PhoneNumberDictionaryEntryType
	{
		// Token: 0x0600308D RID: 12429 RVA: 0x000B6624 File Offset: 0x000B4824
		public PhoneNumberDictionaryEntryType()
		{
		}

		// Token: 0x0600308E RID: 12430 RVA: 0x000B662C File Offset: 0x000B482C
		public PhoneNumberDictionaryEntryType(PhoneNumberKeyType key, string value)
		{
			this.Key = key;
			this.Value = value;
		}

		// Token: 0x17000AA2 RID: 2722
		// (get) Token: 0x0600308F RID: 12431 RVA: 0x000B6642 File Offset: 0x000B4842
		// (set) Token: 0x06003090 RID: 12432 RVA: 0x000B664A File Offset: 0x000B484A
		[IgnoreDataMember]
		[XmlAttribute]
		public PhoneNumberKeyType Key { get; set; }

		// Token: 0x17000AA3 RID: 2723
		// (get) Token: 0x06003091 RID: 12433 RVA: 0x000B6653 File Offset: 0x000B4853
		// (set) Token: 0x06003092 RID: 12434 RVA: 0x000B6660 File Offset: 0x000B4860
		[DataMember(Name = "Key", EmitDefaultValue = false, Order = 0)]
		[XmlIgnore]
		public string KeyString
		{
			get
			{
				return EnumUtilities.ToString<PhoneNumberKeyType>(this.Key);
			}
			set
			{
				this.Key = EnumUtilities.Parse<PhoneNumberKeyType>(value);
			}
		}

		// Token: 0x17000AA4 RID: 2724
		// (get) Token: 0x06003093 RID: 12435 RVA: 0x000B666E File Offset: 0x000B486E
		// (set) Token: 0x06003094 RID: 12436 RVA: 0x000B6676 File Offset: 0x000B4876
		[XmlText]
		[DataMember(Name = "PhoneNumber", EmitDefaultValue = false, Order = 1)]
		public string Value { get; set; }
	}
}
