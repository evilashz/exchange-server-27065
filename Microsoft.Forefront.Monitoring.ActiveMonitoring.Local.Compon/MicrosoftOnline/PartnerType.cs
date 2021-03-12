using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020001A5 RID: 421
	[XmlType(Namespace = "http://www.ccs.com/TestServices/")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[Serializable]
	public enum PartnerType
	{
		// Token: 0x040006CF RID: 1743
		Min,
		// Token: 0x040006D0 RID: 1744
		MicrosoftSupport,
		// Token: 0x040006D1 RID: 1745
		SyndicatePartner,
		// Token: 0x040006D2 RID: 1746
		BreadthPartner,
		// Token: 0x040006D3 RID: 1747
		BreadthPartnerDelegatedAdmin,
		// Token: 0x040006D4 RID: 1748
		OperatingCompany,
		// Token: 0x040006D5 RID: 1749
		IndependentSoftwareVendor,
		// Token: 0x040006D6 RID: 1750
		Max
	}
}
