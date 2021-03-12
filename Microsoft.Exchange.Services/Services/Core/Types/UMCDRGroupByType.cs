using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000681 RID: 1665
	[XmlType(TypeName = "UMCDRGroupByType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum UMCDRGroupByType
	{
		// Token: 0x04001CDC RID: 7388
		[XmlEnum("Day")]
		Day,
		// Token: 0x04001CDD RID: 7389
		[XmlEnum("Month")]
		Month,
		// Token: 0x04001CDE RID: 7390
		[XmlEnum("Total")]
		Total
	}
}
