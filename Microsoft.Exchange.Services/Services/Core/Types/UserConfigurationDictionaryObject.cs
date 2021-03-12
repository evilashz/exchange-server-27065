using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000692 RID: 1682
	[XmlType("UserConfigurationDictionaryObjectType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class UserConfigurationDictionaryObject
	{
		// Token: 0x17000BEC RID: 3052
		// (get) Token: 0x06003397 RID: 13207 RVA: 0x000B89CF File Offset: 0x000B6BCF
		// (set) Token: 0x06003398 RID: 13208 RVA: 0x000B89D7 File Offset: 0x000B6BD7
		[IgnoreDataMember]
		[XmlElement("Type")]
		public UserConfigurationDictionaryObjectType Type { get; set; }

		// Token: 0x17000BED RID: 3053
		// (get) Token: 0x06003399 RID: 13209 RVA: 0x000B89E0 File Offset: 0x000B6BE0
		// (set) Token: 0x0600339A RID: 13210 RVA: 0x000B89ED File Offset: 0x000B6BED
		[DataMember(Name = "Type", IsRequired = true)]
		[XmlIgnore]
		public string TypeString
		{
			get
			{
				return EnumUtilities.ToString<UserConfigurationDictionaryObjectType>(this.Type);
			}
			set
			{
				this.Type = EnumUtilities.Parse<UserConfigurationDictionaryObjectType>(value);
			}
		}

		// Token: 0x17000BEE RID: 3054
		// (get) Token: 0x0600339B RID: 13211 RVA: 0x000B89FB File Offset: 0x000B6BFB
		// (set) Token: 0x0600339C RID: 13212 RVA: 0x000B8A03 File Offset: 0x000B6C03
		[DataMember(IsRequired = true)]
		[XmlElement("Value")]
		public string[] Value { get; set; }
	}
}
