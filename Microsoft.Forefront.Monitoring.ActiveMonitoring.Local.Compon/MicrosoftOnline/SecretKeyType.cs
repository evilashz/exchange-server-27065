using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020000B5 RID: 181
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[Serializable]
	public enum SecretKeyType
	{
		// Token: 0x0400031F RID: 799
		Password,
		// Token: 0x04000320 RID: 800
		Symmetric
	}
}
