using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Mserve.SettingsRequest
{
	// Token: 0x020008C2 RID: 2242
	[GeneratedCode("xsd", "2.0.50727.1318")]
	[XmlType(Namespace = "HMSETTINGS:")]
	[Serializable]
	public enum HeaderDisplayType
	{
		// Token: 0x04002985 RID: 10629
		[XmlEnum("No Header")]
		NoHeader,
		// Token: 0x04002986 RID: 10630
		Basic,
		// Token: 0x04002987 RID: 10631
		Full,
		// Token: 0x04002988 RID: 10632
		Advanced
	}
}
