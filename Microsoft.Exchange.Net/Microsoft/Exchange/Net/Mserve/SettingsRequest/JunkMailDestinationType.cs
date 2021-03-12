using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Mserve.SettingsRequest
{
	// Token: 0x020008C5 RID: 2245
	[GeneratedCode("xsd", "2.0.50727.1318")]
	[XmlType(Namespace = "HMSETTINGS:")]
	[Serializable]
	public enum JunkMailDestinationType
	{
		// Token: 0x04002992 RID: 10642
		[XmlEnum("Immediate Deletion")]
		ImmediateDeletion,
		// Token: 0x04002993 RID: 10643
		[XmlEnum("Junk Mail")]
		JunkMail
	}
}
