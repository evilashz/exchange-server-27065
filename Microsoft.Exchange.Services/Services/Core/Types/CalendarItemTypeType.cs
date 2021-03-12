using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020005BE RID: 1470
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum CalendarItemTypeType
	{
		// Token: 0x04001A8D RID: 6797
		Single,
		// Token: 0x04001A8E RID: 6798
		Occurrence,
		// Token: 0x04001A8F RID: 6799
		Exception,
		// Token: 0x04001A90 RID: 6800
		RecurringMaster
	}
}
