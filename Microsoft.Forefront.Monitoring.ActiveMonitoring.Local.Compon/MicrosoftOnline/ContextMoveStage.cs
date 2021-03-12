using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020000FF RID: 255
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[Serializable]
	public enum ContextMoveStage
	{
		// Token: 0x040003F9 RID: 1017
		Synchronizing,
		// Token: 0x040003FA RID: 1018
		Failed,
		// Token: 0x040003FB RID: 1019
		Completed
	}
}
