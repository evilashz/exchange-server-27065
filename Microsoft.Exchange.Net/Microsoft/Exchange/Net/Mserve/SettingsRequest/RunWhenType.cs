using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Mserve.SettingsRequest
{
	// Token: 0x020008B6 RID: 2230
	[GeneratedCode("xsd", "2.0.50727.1318")]
	[XmlType(Namespace = "HMSETTINGS:")]
	[Serializable]
	public enum RunWhenType
	{
		// Token: 0x0400295A RID: 10586
		MessageReceived,
		// Token: 0x0400295B RID: 10587
		MessageSent
	}
}
