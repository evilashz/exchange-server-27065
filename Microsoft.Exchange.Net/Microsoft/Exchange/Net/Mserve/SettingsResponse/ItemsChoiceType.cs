using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Mserve.SettingsResponse
{
	// Token: 0x020008D7 RID: 2263
	[GeneratedCode("xsd", "2.0.50727.1318")]
	[XmlType(Namespace = "HMSETTINGS:", IncludeInSchema = false)]
	[Serializable]
	public enum ItemsChoiceType
	{
		// Token: 0x04002A00 RID: 10752
		Add,
		// Token: 0x04002A01 RID: 10753
		Delete,
		// Token: 0x04002A02 RID: 10754
		Set
	}
}
