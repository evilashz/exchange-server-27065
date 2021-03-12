using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000132 RID: 306
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class AbsoluteMonthlyRecurrencePatternType : IntervalRecurrencePatternBaseType
	{
		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x06000E07 RID: 3591 RVA: 0x00022623 File Offset: 0x00020823
		// (set) Token: 0x06000E08 RID: 3592 RVA: 0x0002262B File Offset: 0x0002082B
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

		// Token: 0x040009A2 RID: 2466
		private int dayOfMonthField;
	}
}
