using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Mserve.SettingsRequest
{
	// Token: 0x020008CA RID: 2250
	[XmlType(Namespace = "HMSETTINGS:")]
	[GeneratedCode("xsd", "2.0.50727.1318")]
	[Serializable]
	public enum ForwardingMode
	{
		// Token: 0x040029A3 RID: 10659
		NoForwarding,
		// Token: 0x040029A4 RID: 10660
		ForwardOnly,
		// Token: 0x040029A5 RID: 10661
		StoreAndForward
	}
}
