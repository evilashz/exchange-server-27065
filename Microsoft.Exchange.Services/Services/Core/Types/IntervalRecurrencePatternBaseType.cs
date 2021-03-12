using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200061D RID: 1565
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[KnownType(typeof(WeeklyRecurrencePatternType))]
	[XmlInclude(typeof(DailyRegeneratingPatternType))]
	[KnownType(typeof(DailyRegeneratingPatternType))]
	[XmlInclude(typeof(WeeklyRecurrencePatternType))]
	[XmlInclude(typeof(AbsoluteMonthlyRecurrencePatternType))]
	[XmlInclude(typeof(RelativeMonthlyRecurrencePatternType))]
	[XmlInclude(typeof(RegeneratingPatternBaseType))]
	[XmlInclude(typeof(YearlyRegeneratingPatternType))]
	[XmlInclude(typeof(MonthlyRegeneratingPatternType))]
	[XmlInclude(typeof(WeeklyRegeneratingPatternType))]
	[XmlInclude(typeof(DailyRecurrencePatternType))]
	[KnownType(typeof(DailyRecurrencePatternType))]
	[KnownType(typeof(AbsoluteMonthlyRecurrencePatternType))]
	[KnownType(typeof(RelativeMonthlyRecurrencePatternType))]
	[KnownType(typeof(RegeneratingPatternBaseType))]
	[KnownType(typeof(YearlyRegeneratingPatternType))]
	[KnownType(typeof(MonthlyRegeneratingPatternType))]
	[KnownType(typeof(WeeklyRegeneratingPatternType))]
	[Serializable]
	public abstract class IntervalRecurrencePatternBaseType : RecurrencePatternBaseType
	{
		// Token: 0x17000ADE RID: 2782
		// (get) Token: 0x0600311B RID: 12571 RVA: 0x000B6D8F File Offset: 0x000B4F8F
		// (set) Token: 0x0600311C RID: 12572 RVA: 0x000B6D97 File Offset: 0x000B4F97
		[DataMember(EmitDefaultValue = false, IsRequired = true)]
		public int Interval { get; set; }
	}
}
