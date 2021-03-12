using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.StatelessRequest
{
	// Token: 0x02000186 RID: 390
	[XmlType(Namespace = "HMMAIL:", IncludeInSchema = false)]
	[GeneratedCode("xsd", "2.0.50727.3038")]
	[Serializable]
	public enum ItemsChoiceType2
	{
		// Token: 0x04000642 RID: 1602
		Completed,
		// Token: 0x04000643 RID: 1603
		ReminderDate,
		// Token: 0x04000644 RID: 1604
		State,
		// Token: 0x04000645 RID: 1605
		Title
	}
}
