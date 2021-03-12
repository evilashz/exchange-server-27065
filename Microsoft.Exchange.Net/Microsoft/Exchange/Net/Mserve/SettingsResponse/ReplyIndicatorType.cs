using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Mserve.SettingsResponse
{
	// Token: 0x020008E1 RID: 2273
	[XmlType(Namespace = "HMSETTINGS:")]
	[GeneratedCode("xsd", "2.0.50727.1318")]
	[Serializable]
	public enum ReplyIndicatorType
	{
		// Token: 0x04002A3C RID: 10812
		None,
		// Token: 0x04002A3D RID: 10813
		Line,
		// Token: 0x04002A3E RID: 10814
		Arrow
	}
}
