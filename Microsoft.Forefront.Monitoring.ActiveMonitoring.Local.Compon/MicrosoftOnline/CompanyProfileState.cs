using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020000A0 RID: 160
	[XmlType(Namespace = "http://www.ccs.com/TestServices/")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[Serializable]
	public enum CompanyProfileState
	{
		// Token: 0x040002E2 RID: 738
		Other,
		// Token: 0x040002E3 RID: 739
		Active,
		// Token: 0x040002E4 RID: 740
		Suspend,
		// Token: 0x040002E5 RID: 741
		Delete
	}
}
