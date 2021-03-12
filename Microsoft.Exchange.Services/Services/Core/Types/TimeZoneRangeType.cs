using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200067D RID: 1661
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class TimeZoneRangeType
	{
		// Token: 0x17000BA4 RID: 2980
		// (get) Token: 0x060032FA RID: 13050 RVA: 0x000B834C File Offset: 0x000B654C
		// (set) Token: 0x060032FB RID: 13051 RVA: 0x000B8354 File Offset: 0x000B6554
		[DataMember(EmitDefaultValue = false, Order = 0)]
		[XmlAttribute]
		public string UtcTime { get; set; }

		// Token: 0x17000BA5 RID: 2981
		// (get) Token: 0x060032FC RID: 13052 RVA: 0x000B835D File Offset: 0x000B655D
		// (set) Token: 0x060032FD RID: 13053 RVA: 0x000B8365 File Offset: 0x000B6565
		[DataMember(EmitDefaultValue = true, Order = 0)]
		[XmlAttribute]
		public int Offset { get; set; }
	}
}
