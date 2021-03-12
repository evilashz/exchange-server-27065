using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020000A1 RID: 161
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[XmlType(Namespace = "http://www.ccs.com/TestServices/")]
	[Serializable]
	public enum CompanyProfileStateChangeReason
	{
		// Token: 0x040002E7 RID: 743
		Other,
		// Token: 0x040002E8 RID: 744
		Lifecycle,
		// Token: 0x040002E9 RID: 745
		UserRequest,
		// Token: 0x040002EA RID: 746
		Fraud
	}
}
