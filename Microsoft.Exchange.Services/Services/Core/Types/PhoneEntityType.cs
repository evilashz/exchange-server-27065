using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200060E RID: 1550
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class PhoneEntityType : BaseEntityType
	{
		// Token: 0x17000A9F RID: 2719
		// (get) Token: 0x06003086 RID: 12422 RVA: 0x000B65E9 File Offset: 0x000B47E9
		// (set) Token: 0x06003087 RID: 12423 RVA: 0x000B65F1 File Offset: 0x000B47F1
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public string OriginalPhoneString { get; set; }

		// Token: 0x17000AA0 RID: 2720
		// (get) Token: 0x06003088 RID: 12424 RVA: 0x000B65FA File Offset: 0x000B47FA
		// (set) Token: 0x06003089 RID: 12425 RVA: 0x000B6602 File Offset: 0x000B4802
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public string PhoneString { get; set; }

		// Token: 0x17000AA1 RID: 2721
		// (get) Token: 0x0600308A RID: 12426 RVA: 0x000B660B File Offset: 0x000B480B
		// (set) Token: 0x0600308B RID: 12427 RVA: 0x000B6613 File Offset: 0x000B4813
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public string Type { get; set; }
	}
}
