using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000139 RID: 313
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class RelativeMonthlyRecurrencePatternType : IntervalRecurrencePatternBaseType
	{
		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x06000E1C RID: 3612 RVA: 0x000226D3 File Offset: 0x000208D3
		// (set) Token: 0x06000E1D RID: 3613 RVA: 0x000226DB File Offset: 0x000208DB
		public DayOfWeekType DaysOfWeek
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

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x06000E1E RID: 3614 RVA: 0x000226E4 File Offset: 0x000208E4
		// (set) Token: 0x06000E1F RID: 3615 RVA: 0x000226EC File Offset: 0x000208EC
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

		// Token: 0x040009BD RID: 2493
		private DayOfWeekType daysOfWeekField;

		// Token: 0x040009BE RID: 2494
		private DayOfWeekIndexType dayOfWeekIndexField;
	}
}
