using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020000EB RID: 235
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[Serializable]
	public enum PropagationTaskStatus
	{
		// Token: 0x040003CA RID: 970
		Ready,
		// Token: 0x040003CB RID: 971
		Incomplete
	}
}
