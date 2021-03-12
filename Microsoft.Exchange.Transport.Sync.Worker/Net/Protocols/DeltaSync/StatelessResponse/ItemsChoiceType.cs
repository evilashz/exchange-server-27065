using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.StatelessResponse
{
	// Token: 0x0200018F RID: 399
	[XmlType(Namespace = "DeltaSyncV2:", IncludeInSchema = false)]
	[GeneratedCode("xsd", "2.0.50727.3038")]
	[Serializable]
	public enum ItemsChoiceType
	{
		// Token: 0x0400065C RID: 1628
		Completed,
		// Token: 0x0400065D RID: 1629
		ReminderDate,
		// Token: 0x0400065E RID: 1630
		State,
		// Token: 0x0400065F RID: 1631
		Title
	}
}
