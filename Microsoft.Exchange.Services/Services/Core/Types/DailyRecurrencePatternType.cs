using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000621 RID: 1569
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange", Name = "DailyRecurrence")]
	[Serializable]
	public class DailyRecurrencePatternType : IntervalRecurrencePatternBaseType
	{
	}
}
