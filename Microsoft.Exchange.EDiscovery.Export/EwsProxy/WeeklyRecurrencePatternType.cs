using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000138 RID: 312
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class WeeklyRecurrencePatternType : IntervalRecurrencePatternBaseType
	{
		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x06000E17 RID: 3607 RVA: 0x000226A9 File Offset: 0x000208A9
		// (set) Token: 0x06000E18 RID: 3608 RVA: 0x000226B1 File Offset: 0x000208B1
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

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x06000E19 RID: 3609 RVA: 0x000226BA File Offset: 0x000208BA
		// (set) Token: 0x06000E1A RID: 3610 RVA: 0x000226C2 File Offset: 0x000208C2
		public string FirstDayOfWeek
		{
			get
			{
				return this.firstDayOfWeekField;
			}
			set
			{
				this.firstDayOfWeekField = value;
			}
		}

		// Token: 0x040009BB RID: 2491
		private string daysOfWeekField;

		// Token: 0x040009BC RID: 2492
		private string firstDayOfWeekField;
	}
}
