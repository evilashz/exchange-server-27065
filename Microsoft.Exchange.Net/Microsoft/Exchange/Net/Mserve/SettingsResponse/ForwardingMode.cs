using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Mserve.SettingsResponse
{
	// Token: 0x020008E5 RID: 2277
	[XmlType(Namespace = "HMSETTINGS:")]
	[GeneratedCode("xsd", "2.0.50727.1318")]
	[Serializable]
	public enum ForwardingMode
	{
		// Token: 0x04002A4A RID: 10826
		NoForwarding,
		// Token: 0x04002A4B RID: 10827
		ForwardOnly,
		// Token: 0x04002A4C RID: 10828
		StoreAndForward
	}
}
