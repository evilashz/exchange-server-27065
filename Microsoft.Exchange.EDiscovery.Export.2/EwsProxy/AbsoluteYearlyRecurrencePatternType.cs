using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000133 RID: 307
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[Serializable]
	public class AbsoluteYearlyRecurrencePatternType : RecurrencePatternBaseType
	{
		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x06000E0A RID: 3594 RVA: 0x0002263C File Offset: 0x0002083C
		// (set) Token: 0x06000E0B RID: 3595 RVA: 0x00022644 File Offset: 0x00020844
		public int DayOfMonth
		{
			get
			{
				return this.dayOfMonthField;
			}
			set
			{
				this.dayOfMonthField = value;
			}
		}

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x06000E0C RID: 3596 RVA: 0x0002264D File Offset: 0x0002084D
		// (set) Token: 0x06000E0D RID: 3597 RVA: 0x00022655 File Offset: 0x00020855
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

		// Token: 0x040009A3 RID: 2467
		private int dayOfMonthField;

		// Token: 0x040009A4 RID: 2468
		private MonthNamesType monthField;
	}
}
