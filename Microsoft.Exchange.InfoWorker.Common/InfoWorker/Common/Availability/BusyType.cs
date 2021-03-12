using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x020000EB RID: 235
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	public enum BusyType
	{
		// Token: 0x0400038D RID: 909
		Free,
		// Token: 0x0400038E RID: 910
		Tentative,
		// Token: 0x0400038F RID: 911
		Busy,
		// Token: 0x04000390 RID: 912
		OOF,
		// Token: 0x04000391 RID: 913
		WorkingElsewhere,
		// Token: 0x04000392 RID: 914
		NoData
	}
}
