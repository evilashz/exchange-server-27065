using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x02000104 RID: 260
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[Serializable]
	public enum LiveNamespaceType
	{
		// Token: 0x04000406 RID: 1030
		None,
		// Token: 0x04000407 RID: 1031
		Managed,
		// Token: 0x04000408 RID: 1032
		Federated
	}
}
