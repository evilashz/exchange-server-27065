using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200027F RID: 639
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class FreeBusyView
	{
		// Token: 0x1700086D RID: 2157
		// (get) Token: 0x060017AE RID: 6062 RVA: 0x000277A5 File Offset: 0x000259A5
		// (set) Token: 0x060017AF RID: 6063 RVA: 0x000277AD File Offset: 0x000259AD
		public FreeBusyViewType FreeBusyViewType
		{
			get
			{
				return this.freeBusyViewTypeField;
			}
			set
			{
				this.freeBusyViewTypeField = value;
			}
		}

		// Token: 0x1700086E RID: 2158
		// (get) Token: 0x060017B0 RID: 6064 RVA: 0x000277B6 File Offset: 0x000259B6
		// (set) Token: 0x060017B1 RID: 6065 RVA: 0x000277BE File Offset: 0x000259BE
		public string MergedFreeBusy
		{
			get
			{
				return this.mergedFreeBusyField;
			}
			set
			{
				this.mergedFreeBusyField = value;
			}
		}

		// Token: 0x1700086F RID: 2159
		// (get) Token: 0x060017B2 RID: 6066 RVA: 0x000277C7 File Offset: 0x000259C7
		// (set) Token: 0x060017B3 RID: 6067 RVA: 0x000277CF File Offset: 0x000259CF
		[XmlArrayItem(IsNullable = false)]
		public CalendarEvent[] CalendarEventArray
		{
			get
			{
				return this.calendarEventArrayField;
			}
			set
			{
				this.calendarEventArrayField = value;
			}
		}

		// Token: 0x17000870 RID: 2160
		// (get) Token: 0x060017B4 RID: 6068 RVA: 0x000277D8 File Offset: 0x000259D8
		// (set) Token: 0x060017B5 RID: 6069 RVA: 0x000277E0 File Offset: 0x000259E0
		public WorkingHours WorkingHours
		{
			get
			{
				return this.workingHoursField;
			}
			set
			{
				this.workingHoursField = value;
			}
		}

		// Token: 0x04000FF2 RID: 4082
		private FreeBusyViewType freeBusyViewTypeField;

		// Token: 0x04000FF3 RID: 4083
		private string mergedFreeBusyField;

		// Token: 0x04000FF4 RID: 4084
		private CalendarEvent[] calendarEventArrayField;

		// Token: 0x04000FF5 RID: 4085
		private WorkingHours workingHoursField;
	}
}
