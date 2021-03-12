using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.StatelessRequest
{
	// Token: 0x0200016E RID: 366
	[GeneratedCode("xsd", "2.0.50727.3038")]
	[XmlType(Namespace = "DeltaSyncV2:", IncludeInSchema = false)]
	[Serializable]
	public enum ItemsChoiceType
	{
		// Token: 0x040005E9 RID: 1513
		Completed,
		// Token: 0x040005EA RID: 1514
		ReminderDate,
		// Token: 0x040005EB RID: 1515
		State,
		// Token: 0x040005EC RID: 1516
		Title
	}
}
