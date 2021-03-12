using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000B63 RID: 2915
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class WeatherProviderAttribution
	{
		// Token: 0x17001412 RID: 5138
		// (get) Token: 0x060052A0 RID: 21152 RVA: 0x0010B5BB File Offset: 0x001097BB
		// (set) Token: 0x060052A1 RID: 21153 RVA: 0x0010B5C3 File Offset: 0x001097C3
		[DataMember]
		public string Text { get; set; }

		// Token: 0x17001413 RID: 5139
		// (get) Token: 0x060052A2 RID: 21154 RVA: 0x0010B5CC File Offset: 0x001097CC
		// (set) Token: 0x060052A3 RID: 21155 RVA: 0x0010B5D4 File Offset: 0x001097D4
		[DataMember]
		public string Link { get; set; }
	}
}
