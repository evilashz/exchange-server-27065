using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Mserve.SettingsResponse
{
	// Token: 0x020008DD RID: 2269
	[GeneratedCode("xsd", "2.0.50727.1318")]
	[XmlType(Namespace = "HMSETTINGS:")]
	[Serializable]
	public enum HeaderDisplayType
	{
		// Token: 0x04002A2C RID: 10796
		[XmlEnum("No Header")]
		NoHeader,
		// Token: 0x04002A2D RID: 10797
		Basic,
		// Token: 0x04002A2E RID: 10798
		Full,
		// Token: 0x04002A2F RID: 10799
		Advanced
	}
}
