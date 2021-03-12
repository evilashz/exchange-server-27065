using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020005CF RID: 1487
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum EmailReminderChangeType
	{
		// Token: 0x04001AEF RID: 6895
		None,
		// Token: 0x04001AF0 RID: 6896
		Added,
		// Token: 0x04001AF1 RID: 6897
		Override,
		// Token: 0x04001AF2 RID: 6898
		Deleted
	}
}
