using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Mserve.SettingsRequest
{
	// Token: 0x020008C6 RID: 2246
	[XmlType(Namespace = "HMSETTINGS:")]
	[GeneratedCode("xsd", "2.0.50727.1318")]
	[Serializable]
	public enum ReplyIndicatorType
	{
		// Token: 0x04002995 RID: 10645
		None,
		// Token: 0x04002996 RID: 10646
		Line,
		// Token: 0x04002997 RID: 10647
		Arrow
	}
}
