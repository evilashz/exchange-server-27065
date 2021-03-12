using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.StatelessRequest
{
	// Token: 0x0200017A RID: 378
	[GeneratedCode("xsd", "2.0.50727.3038")]
	[XmlType(Namespace = "DeltaSyncV2:", IncludeInSchema = false)]
	[Serializable]
	public enum ItemChoiceType
	{
		// Token: 0x04000623 RID: 1571
		And,
		// Token: 0x04000624 RID: 1572
		Clause,
		// Token: 0x04000625 RID: 1573
		Not,
		// Token: 0x04000626 RID: 1574
		Or
	}
}
