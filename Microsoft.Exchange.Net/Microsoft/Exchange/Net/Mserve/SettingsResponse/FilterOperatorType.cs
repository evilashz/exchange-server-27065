using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Mserve.SettingsResponse
{
	// Token: 0x020008F8 RID: 2296
	[GeneratedCode("xsd", "2.0.50727.1318")]
	[XmlType(Namespace = "HMSETTINGS:")]
	[Serializable]
	public enum FilterOperatorType
	{
		// Token: 0x04002A93 RID: 10899
		Contains,
		// Token: 0x04002A94 RID: 10900
		[XmlEnum("Does not contain")]
		Doesnotcontain,
		// Token: 0x04002A95 RID: 10901
		[XmlEnum("Contains word")]
		Containsword,
		// Token: 0x04002A96 RID: 10902
		[XmlEnum("Starts with")]
		Startswith,
		// Token: 0x04002A97 RID: 10903
		[XmlEnum("Ends with")]
		Endswith,
		// Token: 0x04002A98 RID: 10904
		Equals
	}
}
