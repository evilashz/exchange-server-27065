using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Security.RightsManagement.SOAP.Server
{
	// Token: 0x020009E8 RID: 2536
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[XmlType(Namespace = "http://microsoft.com/DRM/ServerService")]
	[Serializable]
	public enum ServerInfoType
	{
		// Token: 0x04002F07 RID: 12039
		VersionInfo,
		// Token: 0x04002F08 RID: 12040
		ServerFeatureInfo,
		// Token: 0x04002F09 RID: 12041
		ServerLicensorCertificate,
		// Token: 0x04002F0A RID: 12042
		ServiceLocations
	}
}
