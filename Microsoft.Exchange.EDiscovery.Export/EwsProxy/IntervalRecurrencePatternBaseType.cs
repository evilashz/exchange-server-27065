using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000131 RID: 305
	[XmlInclude(typeof(RegeneratingPatternBaseType))]
	[XmlInclude(typeof(WeeklyRegeneratingPatternType))]
	[XmlInclude(typeof(DailyRecurrencePatternType))]
	[XmlInclude(typeof(AbsoluteMonthlyRecurrencePatternType))]
	[XmlInclude(typeof(RelativeMonthlyRecurrencePatternType))]
	[XmlInclude(typeof(YearlyRegeneratingPatternType))]
	[XmlInclude(typeof(MonthlyRegeneratingPatternType))]
	[XmlInclude(typeof(WeeklyRecurrencePatternType))]
	[XmlInclude(typeof(DailyRegeneratingPatternType))]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public abstract class IntervalRecurrencePatternBaseType : RecurrencePatternBaseType
	{
		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x06000E04 RID: 3588 RVA: 0x0002260A File Offset: 0x0002080A
		// (set) Token: 0x06000E05 RID: 3589 RVA: 0x00022612 File Offset: 0x00020812
		public int Interval
		{
			get
			{
				return this.intervalField;
			}
			set
			{
				this.intervalField = value;
			}
		}

		// Token: 0x040009A1 RID: 2465
		private int intervalField;
	}
}
