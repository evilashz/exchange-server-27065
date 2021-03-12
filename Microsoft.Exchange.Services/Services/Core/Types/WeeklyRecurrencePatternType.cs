using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000622 RID: 1570
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange", Name = "WeeklyRecurrence")]
	[Serializable]
	public class WeeklyRecurrencePatternType : IntervalRecurrencePatternBaseType
	{
		// Token: 0x17000AE6 RID: 2790
		// (get) Token: 0x06003130 RID: 12592 RVA: 0x000B6E49 File Offset: 0x000B5049
		// (set) Token: 0x06003131 RID: 12593 RVA: 0x000B6E51 File Offset: 0x000B5051
		[DataMember(EmitDefaultValue = false, IsRequired = true, Order = 1)]
		public string DaysOfWeek { get; set; }

		// Token: 0x17000AE7 RID: 2791
		// (get) Token: 0x06003132 RID: 12594 RVA: 0x000B6E5A File Offset: 0x000B505A
		// (set) Token: 0x06003133 RID: 12595 RVA: 0x000B6E62 File Offset: 0x000B5062
		[DataMember(EmitDefaultValue = false, IsRequired = true, Order = 2)]
		public string FirstDayOfWeek { get; set; }
	}
}
