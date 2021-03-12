using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020000E4 RID: 228
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[Serializable]
	public enum SortProperty
	{
		// Token: 0x04000391 RID: 913
		None,
		// Token: 0x04000392 RID: 914
		DisplayName,
		// Token: 0x04000393 RID: 915
		UserPrincipalName
	}
}
