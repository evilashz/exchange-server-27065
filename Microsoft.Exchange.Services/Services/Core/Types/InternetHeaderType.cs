using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020005E5 RID: 1509
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class InternetHeaderType
	{
		// Token: 0x17000936 RID: 2358
		// (get) Token: 0x06002D6E RID: 11630 RVA: 0x000B2300 File Offset: 0x000B0500
		// (set) Token: 0x06002D6F RID: 11631 RVA: 0x000B2308 File Offset: 0x000B0508
		[DataMember(EmitDefaultValue = false, Order = 0)]
		[XmlAttribute]
		public string HeaderName { get; set; }

		// Token: 0x17000937 RID: 2359
		// (get) Token: 0x06002D70 RID: 11632 RVA: 0x000B2311 File Offset: 0x000B0511
		// (set) Token: 0x06002D71 RID: 11633 RVA: 0x000B2319 File Offset: 0x000B0519
		[XmlText]
		[DataMember(EmitDefaultValue = false, Order = 1)]
		public string Value { get; set; }
	}
}
