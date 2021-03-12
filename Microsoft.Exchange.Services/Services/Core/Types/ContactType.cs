using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020005B1 RID: 1457
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class ContactType : BaseEntityType
	{
		// Token: 0x17000866 RID: 2150
		// (get) Token: 0x06002B8F RID: 11151 RVA: 0x000AFC7A File Offset: 0x000ADE7A
		// (set) Token: 0x06002B90 RID: 11152 RVA: 0x000AFC82 File Offset: 0x000ADE82
		[XmlElement]
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public string PersonName { get; set; }

		// Token: 0x17000867 RID: 2151
		// (get) Token: 0x06002B91 RID: 11153 RVA: 0x000AFC8B File Offset: 0x000ADE8B
		// (set) Token: 0x06002B92 RID: 11154 RVA: 0x000AFC93 File Offset: 0x000ADE93
		[XmlElement]
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public string BusinessName { get; set; }

		// Token: 0x17000868 RID: 2152
		// (get) Token: 0x06002B93 RID: 11155 RVA: 0x000AFC9C File Offset: 0x000ADE9C
		// (set) Token: 0x06002B94 RID: 11156 RVA: 0x000AFCA4 File Offset: 0x000ADEA4
		[XmlArrayItem(ElementName = "Phone", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", Type = typeof(PhoneType))]
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public PhoneType[] PhoneNumbers { get; set; }

		// Token: 0x17000869 RID: 2153
		// (get) Token: 0x06002B95 RID: 11157 RVA: 0x000AFCAD File Offset: 0x000ADEAD
		// (set) Token: 0x06002B96 RID: 11158 RVA: 0x000AFCB5 File Offset: 0x000ADEB5
		[XmlArrayItem(ElementName = "Url", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", Type = typeof(string))]
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public string[] Urls { get; set; }

		// Token: 0x1700086A RID: 2154
		// (get) Token: 0x06002B97 RID: 11159 RVA: 0x000AFCBE File Offset: 0x000ADEBE
		// (set) Token: 0x06002B98 RID: 11160 RVA: 0x000AFCC6 File Offset: 0x000ADEC6
		[XmlArrayItem(ElementName = "EmailAddress", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", Type = typeof(string))]
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public string[] EmailAddresses { get; set; }

		// Token: 0x1700086B RID: 2155
		// (get) Token: 0x06002B99 RID: 11161 RVA: 0x000AFCCF File Offset: 0x000ADECF
		// (set) Token: 0x06002B9A RID: 11162 RVA: 0x000AFCD7 File Offset: 0x000ADED7
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		[XmlArrayItem(ElementName = "Address", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", Type = typeof(string))]
		public string[] Addresses { get; set; }

		// Token: 0x1700086C RID: 2156
		// (get) Token: 0x06002B9B RID: 11163 RVA: 0x000AFCE0 File Offset: 0x000ADEE0
		// (set) Token: 0x06002B9C RID: 11164 RVA: 0x000AFCE8 File Offset: 0x000ADEE8
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		[XmlElement]
		public string ContactString { get; set; }
	}
}
