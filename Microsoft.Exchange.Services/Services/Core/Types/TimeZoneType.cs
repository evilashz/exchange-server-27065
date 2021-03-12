using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200067E RID: 1662
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class TimeZoneType
	{
		// Token: 0x17000BA6 RID: 2982
		// (get) Token: 0x060032FF RID: 13055 RVA: 0x000B8376 File Offset: 0x000B6576
		// (set) Token: 0x06003300 RID: 13056 RVA: 0x000B837E File Offset: 0x000B657E
		[XmlElement]
		[DataMember(EmitDefaultValue = false, IsRequired = true, Order = 1)]
		public string BaseOffset { get; set; }

		// Token: 0x17000BA7 RID: 2983
		// (get) Token: 0x06003301 RID: 13057 RVA: 0x000B8387 File Offset: 0x000B6587
		// (set) Token: 0x06003302 RID: 13058 RVA: 0x000B838F File Offset: 0x000B658F
		[DataMember(EmitDefaultValue = false, IsRequired = false, Order = 2)]
		public TimeChangeType Standard { get; set; }

		// Token: 0x17000BA8 RID: 2984
		// (get) Token: 0x06003303 RID: 13059 RVA: 0x000B8398 File Offset: 0x000B6598
		// (set) Token: 0x06003304 RID: 13060 RVA: 0x000B83A0 File Offset: 0x000B65A0
		[DataMember(EmitDefaultValue = false, IsRequired = false, Order = 3)]
		public TimeChangeType Daylight { get; set; }

		// Token: 0x17000BA9 RID: 2985
		// (get) Token: 0x06003305 RID: 13061 RVA: 0x000B83A9 File Offset: 0x000B65A9
		// (set) Token: 0x06003306 RID: 13062 RVA: 0x000B83B1 File Offset: 0x000B65B1
		[XmlAttribute]
		[DataMember(EmitDefaultValue = false, IsRequired = false, Order = 0)]
		public string TimeZoneName { get; set; }
	}
}
