using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020000F4 RID: 244
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[Serializable]
	public enum DirSyncState
	{
		// Token: 0x040003DC RID: 988
		Disabled,
		// Token: 0x040003DD RID: 989
		Enabled,
		// Token: 0x040003DE RID: 990
		PendingEnabled,
		// Token: 0x040003DF RID: 991
		PendingDisabledDraining,
		// Token: 0x040003E0 RID: 992
		PendingDisabledTransferring
	}
}
