using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x02000111 RID: 273
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[Serializable]
	public enum DirectoryObjectClassAddressList
	{
		// Token: 0x04000437 RID: 1079
		Contact,
		// Token: 0x04000438 RID: 1080
		Group,
		// Token: 0x04000439 RID: 1081
		User
	}
}
