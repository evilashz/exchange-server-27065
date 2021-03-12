using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.StatelessResponse
{
	// Token: 0x020001A1 RID: 417
	[XmlType(Namespace = "HMMAIL:", IncludeInSchema = false)]
	[GeneratedCode("xsd", "2.0.50727.3038")]
	[Serializable]
	public enum ItemsChoiceType2
	{
		// Token: 0x040006A5 RID: 1701
		Completed,
		// Token: 0x040006A6 RID: 1702
		ReminderDate,
		// Token: 0x040006A7 RID: 1703
		State,
		// Token: 0x040006A8 RID: 1704
		Title
	}
}
