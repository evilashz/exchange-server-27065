using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200022F RID: 559
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum FileAsMappingType
	{
		// Token: 0x04000E6F RID: 3695
		None,
		// Token: 0x04000E70 RID: 3696
		LastCommaFirst,
		// Token: 0x04000E71 RID: 3697
		FirstSpaceLast,
		// Token: 0x04000E72 RID: 3698
		Company,
		// Token: 0x04000E73 RID: 3699
		LastCommaFirstCompany,
		// Token: 0x04000E74 RID: 3700
		CompanyLastFirst,
		// Token: 0x04000E75 RID: 3701
		LastFirst,
		// Token: 0x04000E76 RID: 3702
		LastFirstCompany,
		// Token: 0x04000E77 RID: 3703
		CompanyLastCommaFirst,
		// Token: 0x04000E78 RID: 3704
		LastFirstSuffix,
		// Token: 0x04000E79 RID: 3705
		LastSpaceFirstCompany,
		// Token: 0x04000E7A RID: 3706
		CompanyLastSpaceFirst,
		// Token: 0x04000E7B RID: 3707
		LastSpaceFirst,
		// Token: 0x04000E7C RID: 3708
		DisplayName,
		// Token: 0x04000E7D RID: 3709
		FirstName,
		// Token: 0x04000E7E RID: 3710
		LastFirstMiddleSuffix,
		// Token: 0x04000E7F RID: 3711
		LastName,
		// Token: 0x04000E80 RID: 3712
		Empty
	}
}
