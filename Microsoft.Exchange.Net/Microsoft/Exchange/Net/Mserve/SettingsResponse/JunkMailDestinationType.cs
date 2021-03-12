using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Mserve.SettingsResponse
{
	// Token: 0x020008E0 RID: 2272
	[GeneratedCode("xsd", "2.0.50727.1318")]
	[XmlType(Namespace = "HMSETTINGS:")]
	[Serializable]
	public enum JunkMailDestinationType
	{
		// Token: 0x04002A39 RID: 10809
		[XmlEnum("Immediate Deletion")]
		ImmediateDeletion,
		// Token: 0x04002A3A RID: 10810
		[XmlEnum("Junk Mail")]
		JunkMail
	}
}
