using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000690 RID: 1680
	[XmlType(TypeName = "UserConfigurationType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class ServiceUserConfiguration
	{
		// Token: 0x17000BE5 RID: 3045
		// (get) Token: 0x06003387 RID: 13191 RVA: 0x000B8948 File Offset: 0x000B6B48
		// (set) Token: 0x06003388 RID: 13192 RVA: 0x000B8950 File Offset: 0x000B6B50
		[XmlElement("UserConfigurationName")]
		[DataMember(IsRequired = true, Order = 1)]
		public UserConfigurationNameType UserConfigurationName { get; set; }

		// Token: 0x17000BE6 RID: 3046
		// (get) Token: 0x06003389 RID: 13193 RVA: 0x000B8959 File Offset: 0x000B6B59
		// (set) Token: 0x0600338A RID: 13194 RVA: 0x000B8961 File Offset: 0x000B6B61
		[DataMember(Name = "ItemId", IsRequired = false, EmitDefaultValue = false, Order = 2)]
		[XmlElement("ItemId")]
		public ItemId ItemId { get; set; }

		// Token: 0x17000BE7 RID: 3047
		// (get) Token: 0x0600338B RID: 13195 RVA: 0x000B896A File Offset: 0x000B6B6A
		// (set) Token: 0x0600338C RID: 13196 RVA: 0x000B8972 File Offset: 0x000B6B72
		[DataMember(Name = "Dictionary", IsRequired = false, EmitDefaultValue = false, Order = 3)]
		[XmlArrayItem("DictionaryEntry", IsNullable = false)]
		public UserConfigurationDictionaryEntry[] Dictionary { get; set; }

		// Token: 0x17000BE8 RID: 3048
		// (get) Token: 0x0600338D RID: 13197 RVA: 0x000B897B File Offset: 0x000B6B7B
		// (set) Token: 0x0600338E RID: 13198 RVA: 0x000B8983 File Offset: 0x000B6B83
		[DataMember(Name = "XmlData", IsRequired = false, EmitDefaultValue = false, Order = 4)]
		[XmlElement]
		public string XmlData { get; set; }

		// Token: 0x17000BE9 RID: 3049
		// (get) Token: 0x0600338F RID: 13199 RVA: 0x000B898C File Offset: 0x000B6B8C
		// (set) Token: 0x06003390 RID: 13200 RVA: 0x000B8994 File Offset: 0x000B6B94
		[DataMember(Name = "BinaryData", IsRequired = false, EmitDefaultValue = false, Order = 5)]
		public string BinaryData { get; set; }
	}
}
