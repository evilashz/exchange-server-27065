using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Mserve.SettingsRequest
{
	// Token: 0x020008BA RID: 2234
	[XmlType(Namespace = "HMSETTINGS:")]
	[GeneratedCode("xsd", "2.0.50727.1318")]
	[Serializable]
	public enum FilterOperatorType
	{
		// Token: 0x04002966 RID: 10598
		Contains,
		// Token: 0x04002967 RID: 10599
		[XmlEnum("Does not contain")]
		Doesnotcontain,
		// Token: 0x04002968 RID: 10600
		[XmlEnum("Contains word")]
		Containsword,
		// Token: 0x04002969 RID: 10601
		[XmlEnum("Starts with")]
		Startswith,
		// Token: 0x0400296A RID: 10602
		[XmlEnum("Ends with")]
		Endswith,
		// Token: 0x0400296B RID: 10603
		Equals
	}
}
