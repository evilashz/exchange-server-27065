using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Mserve.SettingsResponse
{
	// Token: 0x020008DF RID: 2271
	[XmlType(Namespace = "HMSETTINGS:")]
	[GeneratedCode("xsd", "2.0.50727.1318")]
	[Serializable]
	public enum JunkLevelType
	{
		// Token: 0x04002A34 RID: 10804
		Off,
		// Token: 0x04002A35 RID: 10805
		Low,
		// Token: 0x04002A36 RID: 10806
		High,
		// Token: 0x04002A37 RID: 10807
		Exclusive
	}
}
