using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020005C1 RID: 1473
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum DayOfWeekIndexType
	{
		// Token: 0x04001AA1 RID: 6817
		None,
		// Token: 0x04001AA2 RID: 6818
		First,
		// Token: 0x04001AA3 RID: 6819
		Second,
		// Token: 0x04001AA4 RID: 6820
		Third,
		// Token: 0x04001AA5 RID: 6821
		Fourth,
		// Token: 0x04001AA6 RID: 6822
		Last = -1
	}
}
