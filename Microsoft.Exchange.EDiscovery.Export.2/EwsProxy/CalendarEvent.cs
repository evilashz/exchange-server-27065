using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200027E RID: 638
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class CalendarEvent
	{
		// Token: 0x17000869 RID: 2153
		// (get) Token: 0x060017A5 RID: 6053 RVA: 0x00027759 File Offset: 0x00025959
		// (set) Token: 0x060017A6 RID: 6054 RVA: 0x00027761 File Offset: 0x00025961
		public DateTime StartTime
		{
			get
			{
				return this.startTimeField;
			}
			set
			{
				this.startTimeField = value;
			}
		}

		// Token: 0x1700086A RID: 2154
		// (get) Token: 0x060017A7 RID: 6055 RVA: 0x0002776A File Offset: 0x0002596A
		// (set) Token: 0x060017A8 RID: 6056 RVA: 0x00027772 File Offset: 0x00025972
		public DateTime EndTime
		{
			get
			{
				return this.endTimeField;
			}
			set
			{
				this.endTimeField = value;
			}
		}

		// Token: 0x1700086B RID: 2155
		// (get) Token: 0x060017A9 RID: 6057 RVA: 0x0002777B File Offset: 0x0002597B
		// (set) Token: 0x060017AA RID: 6058 RVA: 0x00027783 File Offset: 0x00025983
		public LegacyFreeBusyType BusyType
		{
			get
			{
				return this.busyTypeField;
			}
			set
			{
				this.busyTypeField = value;
			}
		}

		// Token: 0x1700086C RID: 2156
		// (get) Token: 0x060017AB RID: 6059 RVA: 0x0002778C File Offset: 0x0002598C
		// (set) Token: 0x060017AC RID: 6060 RVA: 0x00027794 File Offset: 0x00025994
		public CalendarEventDetails CalendarEventDetails
		{
			get
			{
				return this.calendarEventDetailsField;
			}
			set
			{
				this.calendarEventDetailsField = value;
			}
		}

		// Token: 0x04000FEE RID: 4078
		private DateTime startTimeField;

		// Token: 0x04000FEF RID: 4079
		private DateTime endTimeField;

		// Token: 0x04000FF0 RID: 4080
		private LegacyFreeBusyType busyTypeField;

		// Token: 0x04000FF1 RID: 4081
		private CalendarEventDetails calendarEventDetailsField;
	}
}
