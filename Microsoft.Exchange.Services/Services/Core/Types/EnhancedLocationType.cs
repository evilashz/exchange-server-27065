using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020005BC RID: 1468
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange", Name = "EnhancedLocation")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", TypeName = "EnhancedLocationType")]
	[Serializable]
	public class EnhancedLocationType
	{
		// Token: 0x170008EE RID: 2286
		// (get) Token: 0x06002CB2 RID: 11442 RVA: 0x000B1357 File Offset: 0x000AF557
		// (set) Token: 0x06002CB3 RID: 11443 RVA: 0x000B135F File Offset: 0x000AF55F
		[DataMember(EmitDefaultValue = false, Order = 1)]
		public string DisplayName { get; set; }

		// Token: 0x170008EF RID: 2287
		// (get) Token: 0x06002CB4 RID: 11444 RVA: 0x000B1368 File Offset: 0x000AF568
		// (set) Token: 0x06002CB5 RID: 11445 RVA: 0x000B1370 File Offset: 0x000AF570
		[DataMember(EmitDefaultValue = false, Order = 2)]
		public string Annotation { get; set; }

		// Token: 0x170008F0 RID: 2288
		// (get) Token: 0x06002CB6 RID: 11446 RVA: 0x000B1379 File Offset: 0x000AF579
		// (set) Token: 0x06002CB7 RID: 11447 RVA: 0x000B1381 File Offset: 0x000AF581
		[DataMember(EmitDefaultValue = false, Order = 3)]
		public PostalAddress PostalAddress { get; set; }
	}
}
