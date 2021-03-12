using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200014E RID: 334
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum FileAsMappingType
	{
		// Token: 0x04000A1D RID: 2589
		None,
		// Token: 0x04000A1E RID: 2590
		LastCommaFirst,
		// Token: 0x04000A1F RID: 2591
		FirstSpaceLast,
		// Token: 0x04000A20 RID: 2592
		Company,
		// Token: 0x04000A21 RID: 2593
		LastCommaFirstCompany,
		// Token: 0x04000A22 RID: 2594
		CompanyLastFirst,
		// Token: 0x04000A23 RID: 2595
		LastFirst,
		// Token: 0x04000A24 RID: 2596
		LastFirstCompany,
		// Token: 0x04000A25 RID: 2597
		CompanyLastCommaFirst,
		// Token: 0x04000A26 RID: 2598
		LastFirstSuffix,
		// Token: 0x04000A27 RID: 2599
		LastSpaceFirstCompany,
		// Token: 0x04000A28 RID: 2600
		CompanyLastSpaceFirst,
		// Token: 0x04000A29 RID: 2601
		LastSpaceFirst,
		// Token: 0x04000A2A RID: 2602
		DisplayName,
		// Token: 0x04000A2B RID: 2603
		FirstName,
		// Token: 0x04000A2C RID: 2604
		LastFirstMiddleSuffix,
		// Token: 0x04000A2D RID: 2605
		LastName,
		// Token: 0x04000A2E RID: 2606
		Empty
	}
}
