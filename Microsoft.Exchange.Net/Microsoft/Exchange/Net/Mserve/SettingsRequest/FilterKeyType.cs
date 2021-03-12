using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Mserve.SettingsRequest
{
	// Token: 0x020008B9 RID: 2233
	[XmlType(Namespace = "HMSETTINGS:")]
	[GeneratedCode("xsd", "2.0.50727.1318")]
	[Serializable]
	public enum FilterKeyType
	{
		// Token: 0x04002961 RID: 10593
		Subject,
		// Token: 0x04002962 RID: 10594
		[XmlEnum("From Name")]
		FromName,
		// Token: 0x04002963 RID: 10595
		[XmlEnum("From Address")]
		FromAddress,
		// Token: 0x04002964 RID: 10596
		[XmlEnum("To or CC Line")]
		ToorCCLine
	}
}
