using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020000E8 RID: 232
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[Serializable]
	public enum ProvisioningStatus
	{
		// Token: 0x040003BC RID: 956
		Success,
		// Token: 0x040003BD RID: 957
		Error,
		// Token: 0x040003BE RID: 958
		PendingInput
	}
}
