using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020000C2 RID: 194
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[Serializable]
	public enum AddressType
	{
		// Token: 0x0400033F RID: 831
		Reply,
		// Token: 0x04000340 RID: 832
		Realm,
		// Token: 0x04000341 RID: 833
		Error
	}
}
