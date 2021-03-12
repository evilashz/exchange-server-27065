using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020000A5 RID: 165
	[XmlType(TypeName = "ProvisioningStatus", Namespace = "http://www.ccs.com/TestServices/")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[Serializable]
	public enum ProvisioningStatus1
	{
		// Token: 0x040002FA RID: 762
		None,
		// Token: 0x040002FB RID: 763
		Success,
		// Token: 0x040002FC RID: 764
		Error,
		// Token: 0x040002FD RID: 765
		PendingInput
	}
}
