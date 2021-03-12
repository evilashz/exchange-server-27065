using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000279 RID: 633
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class WorkingPeriod
	{
		// Token: 0x17000853 RID: 2131
		// (get) Token: 0x06001774 RID: 6004 RVA: 0x000275BB File Offset: 0x000257BB
		// (set) Token: 0x06001775 RID: 6005 RVA: 0x000275C3 File Offset: 0x000257C3
		public string DayOfWeek
		{
			get
			{
				return this.dayOfWeekField;
			}
			set
			{
				this.dayOfWeekField = value;
			}
		}

		// Token: 0x17000854 RID: 2132
		// (get) Token: 0x06001776 RID: 6006 RVA: 0x000275CC File Offset: 0x000257CC
		// (set) Token: 0x06001777 RID: 6007 RVA: 0x000275D4 File Offset: 0x000257D4
		public int StartTimeInMinutes
		{
			get
			{
				return this.startTimeInMinutesField;
			}
			set
			{
				this.startTimeInMinutesField = value;
			}
		}

		// Token: 0x17000855 RID: 2133
		// (get) Token: 0x06001778 RID: 6008 RVA: 0x000275DD File Offset: 0x000257DD
		// (set) Token: 0x06001779 RID: 6009 RVA: 0x000275E5 File Offset: 0x000257E5
		public int EndTimeInMinutes
		{
			get
			{
				return this.endTimeInMinutesField;
			}
			set
			{
				this.endTimeInMinutesField = value;
			}
		}

		// Token: 0x04000FD8 RID: 4056
		private string dayOfWeekField;

		// Token: 0x04000FD9 RID: 4057
		private int startTimeInMinutesField;

		// Token: 0x04000FDA RID: 4058
		private int endTimeInMinutesField;
	}
}
