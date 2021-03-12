using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200061F RID: 1567
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange", Name = "AbsoluteYearlyRecurrence")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class AbsoluteYearlyRecurrencePatternType : RecurrencePatternBaseType
	{
		// Token: 0x17000AE0 RID: 2784
		// (get) Token: 0x06003121 RID: 12577 RVA: 0x000B6DC1 File Offset: 0x000B4FC1
		// (set) Token: 0x06003122 RID: 12578 RVA: 0x000B6DC9 File Offset: 0x000B4FC9
		[DataMember(EmitDefaultValue = false, IsRequired = true, Order = 1)]
		public int DayOfMonth { get; set; }

		// Token: 0x17000AE1 RID: 2785
		// (get) Token: 0x06003123 RID: 12579 RVA: 0x000B6DD2 File Offset: 0x000B4FD2
		// (set) Token: 0x06003124 RID: 12580 RVA: 0x000B6DDA File Offset: 0x000B4FDA
		[DataMember(Name = "Month", IsRequired = true, Order = 2)]
		[XmlElement("Month")]
		public string Month { get; set; }
	}
}
