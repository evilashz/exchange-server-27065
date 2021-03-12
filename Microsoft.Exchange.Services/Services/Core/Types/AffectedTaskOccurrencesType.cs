using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020006AC RID: 1708
	[XmlType("AffectedTaskOccurrencesType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	public enum AffectedTaskOccurrencesType
	{
		// Token: 0x04001DB1 RID: 7601
		AllOccurrences,
		// Token: 0x04001DB2 RID: 7602
		SpecifiedOccurrenceOnly
	}
}
