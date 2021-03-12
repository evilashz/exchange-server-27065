using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020000B8 RID: 184
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[Serializable]
	public enum KeyType
	{
		// Token: 0x0400032A RID: 810
		AsymmetricPublicKey,
		// Token: 0x0400032B RID: 811
		AsymmetricX509Cert,
		// Token: 0x0400032C RID: 812
		Password,
		// Token: 0x0400032D RID: 813
		Symmetric
	}
}
