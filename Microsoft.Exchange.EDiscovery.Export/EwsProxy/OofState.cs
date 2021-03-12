using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200026E RID: 622
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum OofState
	{
		// Token: 0x04000FBE RID: 4030
		Disabled,
		// Token: 0x04000FBF RID: 4031
		Enabled,
		// Token: 0x04000FC0 RID: 4032
		Scheduled
	}
}
