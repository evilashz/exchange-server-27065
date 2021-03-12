using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200012F RID: 303
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class RecurrenceType
	{
		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x06000DFE RID: 3582 RVA: 0x000225D8 File Offset: 0x000207D8
		// (set) Token: 0x06000DFF RID: 3583 RVA: 0x000225E0 File Offset: 0x000207E0
		[XmlElement("AbsoluteYearlyRecurrence", typeof(AbsoluteYearlyRecurrencePatternType))]
		[XmlElement("RelativeMonthlyRecurrence", typeof(RelativeMonthlyRecurrencePatternType))]
		[XmlElement("DailyRecurrence", typeof(DailyRecurrencePatternType))]
		[XmlElement("RelativeYearlyRecurrence", typeof(RelativeYearlyRecurrencePatternType))]
		[XmlElement("AbsoluteMonthlyRecurrence", typeof(AbsoluteMonthlyRecurrencePatternType))]
		[XmlElement("WeeklyRecurrence", typeof(WeeklyRecurrencePatternType))]
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

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x06000E00 RID: 3584 RVA: 0x000225E9 File Offset: 0x000207E9
		// (set) Token: 0x06000E01 RID: 3585 RVA: 0x000225F1 File Offset: 0x000207F1
		[XmlElement("NoEndRecurrence", typeof(NoEndRecurrenceRangeType))]
		[XmlElement("NumberedRecurrence", typeof(NumberedRecurrenceRangeType))]
		[XmlElement("EndDateRecurrence", typeof(EndDateRecurrenceRangeType))]
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

		// Token: 0x0400099F RID: 2463
		private RecurrencePatternBaseType itemField;

		// Token: 0x040009A0 RID: 2464
		private RecurrenceRangeBaseType item1Field;
	}
}
