using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Mserve.SettingsRequest
{
	// Token: 0x020008C4 RID: 2244
	[XmlType(Namespace = "HMSETTINGS:")]
	[GeneratedCode("xsd", "2.0.50727.1318")]
	[Serializable]
	public enum JunkLevelType
	{
		// Token: 0x0400298D RID: 10637
		Off,
		// Token: 0x0400298E RID: 10638
		Low,
		// Token: 0x0400298F RID: 10639
		High,
		// Token: 0x04002990 RID: 10640
		Exclusive
	}
}
