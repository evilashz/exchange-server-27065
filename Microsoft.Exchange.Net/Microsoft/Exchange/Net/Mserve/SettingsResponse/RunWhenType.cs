using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Mserve.SettingsResponse
{
	// Token: 0x020008F4 RID: 2292
	[XmlType(Namespace = "HMSETTINGS:")]
	[GeneratedCode("xsd", "2.0.50727.1318")]
	[Serializable]
	public enum RunWhenType
	{
		// Token: 0x04002A87 RID: 10887
		MessageReceived,
		// Token: 0x04002A88 RID: 10888
		MessageSent
	}
}
