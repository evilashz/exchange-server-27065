using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Mserve.SettingsResponse
{
	// Token: 0x020008F7 RID: 2295
	[GeneratedCode("xsd", "2.0.50727.1318")]
	[XmlType(Namespace = "HMSETTINGS:")]
	[Serializable]
	public enum FilterKeyType
	{
		// Token: 0x04002A8E RID: 10894
		Subject,
		// Token: 0x04002A8F RID: 10895
		[XmlEnum("From Name")]
		FromName,
		// Token: 0x04002A90 RID: 10896
		[XmlEnum("From Address")]
		FromAddress,
		// Token: 0x04002A91 RID: 10897
		[XmlEnum("To or CC Line")]
		ToorCCLine
	}
}
