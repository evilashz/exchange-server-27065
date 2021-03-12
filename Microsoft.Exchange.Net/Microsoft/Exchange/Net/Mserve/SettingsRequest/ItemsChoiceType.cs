using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Mserve.SettingsRequest
{
	// Token: 0x020008C0 RID: 2240
	[XmlType(Namespace = "HMSETTINGS:", IncludeInSchema = false)]
	[GeneratedCode("xsd", "2.0.50727.1318")]
	[Serializable]
	public enum ItemsChoiceType
	{
		// Token: 0x04002976 RID: 10614
		Add,
		// Token: 0x04002977 RID: 10615
		Delete,
		// Token: 0x04002978 RID: 10616
		Set
	}
}
