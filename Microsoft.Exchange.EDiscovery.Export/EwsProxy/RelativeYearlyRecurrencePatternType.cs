using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000135 RID: 309
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class RelativeYearlyRecurrencePatternType : RecurrencePatternBaseType
	{
		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x06000E0F RID: 3599 RVA: 0x00022666 File Offset: 0x00020866
		// (set) Token: 0x06000E10 RID: 3600 RVA: 0x0002266E File Offset: 0x0002086E
		public string DaysOfWeek
		{
			get
			{
				return this.daysOfWeekField;
			}
			set
			{
				this.daysOfWeekField = value;
			}
		}

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x06000E11 RID: 3601 RVA: 0x00022677 File Offset: 0x00020877
		// (set) Token: 0x06000E12 RID: 3602 RVA: 0x0002267F File Offset: 0x0002087F
		public DayOfWeekIndexType DayOfWeekIndex
		{
			get
			{
				return this.dayOfWeekIndexField;
			}
			set
			{
				this.dayOfWeekIndexField = value;
			}
		}

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x06000E13 RID: 3603 RVA: 0x00022688 File Offset: 0x00020888
		// (set) Token: 0x06000E14 RID: 3604 RVA: 0x00022690 File Offset: 0x00020890
		public MonthNamesType Month
		{
			get
			{
				return this.monthField;
			}
			set
			{
				this.monthField = value;
			}
		}

		// Token: 0x040009B2 RID: 2482
		private string daysOfWeekField;

		// Token: 0x040009B3 RID: 2483
		private DayOfWeekIndexType dayOfWeekIndexField;

		// Token: 0x040009B4 RID: 2484
		private MonthNamesType monthField;
	}
}
