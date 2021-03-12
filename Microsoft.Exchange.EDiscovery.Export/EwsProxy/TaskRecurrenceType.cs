using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000163 RID: 355
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class TaskRecurrenceType
	{
		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x06001043 RID: 4163 RVA: 0x00023910 File Offset: 0x00021B10
		// (set) Token: 0x06001044 RID: 4164 RVA: 0x00023918 File Offset: 0x00021B18
		[XmlElement("MonthlyRegeneration", typeof(MonthlyRegeneratingPatternType))]
		[XmlElement("DailyRegeneration", typeof(DailyRegeneratingPatternType))]
		[XmlElement("DailyRecurrence", typeof(DailyRecurrencePatternType))]
		[XmlElement("RelativeYearlyRecurrence", typeof(RelativeYearlyRecurrencePatternType))]
		[XmlElement("AbsoluteMonthlyRecurrence", typeof(AbsoluteMonthlyRecurrencePatternType))]
		[XmlElement("AbsoluteYearlyRecurrence", typeof(AbsoluteYearlyRecurrencePatternType))]
		[XmlElement("RelativeMonthlyRecurrence", typeof(RelativeMonthlyRecurrencePatternType))]
		[XmlElement("WeeklyRegeneration", typeof(WeeklyRegeneratingPatternType))]
		[XmlElement("WeeklyRecurrence", typeof(WeeklyRecurrencePatternType))]
		[XmlElement("YearlyRegeneration", typeof(YearlyRegeneratingPatternType))]
		public RecurrencePatternBaseType Item
		{
			get
			{
				return this.itemField;
			}
			set
			{
				this.itemField = value;
			}
		}

		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x06001045 RID: 4165 RVA: 0x00023921 File Offset: 0x00021B21
		// (set) Token: 0x06001046 RID: 4166 RVA: 0x00023929 File Offset: 0x00021B29
		[XmlElement("NumberedRecurrence", typeof(NumberedRecurrenceRangeType))]
		[XmlElement("EndDateRecurrence", typeof(EndDateRecurrenceRangeType))]
		[XmlElement("NoEndRecurrence", typeof(NoEndRecurrenceRangeType))]
		public RecurrenceRangeBaseType Item1
		{
			get
			{
				return this.item1Field;
			}
			set
			{
				this.item1Field = value;
			}
		}

		// Token: 0x04000B23 RID: 2851
		private RecurrencePatternBaseType itemField;

		// Token: 0x04000B24 RID: 2852
		private RecurrenceRangeBaseType item1Field;
	}
}
