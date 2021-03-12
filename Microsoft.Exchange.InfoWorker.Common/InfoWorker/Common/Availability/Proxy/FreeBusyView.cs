using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.InfoWorker.Common.Availability.Proxy
{
	// Token: 0x0200010A RID: 266
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class FreeBusyView
	{
		// Token: 0x170001BA RID: 442
		// (get) Token: 0x0600072B RID: 1835 RVA: 0x0001F783 File Offset: 0x0001D983
		// (set) Token: 0x0600072C RID: 1836 RVA: 0x0001F78B File Offset: 0x0001D98B
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

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x0600072D RID: 1837 RVA: 0x0001F794 File Offset: 0x0001D994
		// (set) Token: 0x0600072E RID: 1838 RVA: 0x0001F79C File Offset: 0x0001D99C
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

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x0600072F RID: 1839 RVA: 0x0001F7A5 File Offset: 0x0001D9A5
		// (set) Token: 0x06000730 RID: 1840 RVA: 0x0001F7AD File Offset: 0x0001D9AD
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

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06000731 RID: 1841 RVA: 0x0001F7B6 File Offset: 0x0001D9B6
		// (set) Token: 0x06000732 RID: 1842 RVA: 0x0001F7BE File Offset: 0x0001D9BE
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

		// Token: 0x04000454 RID: 1108
		private FreeBusyViewType freeBusyViewTypeField;

		// Token: 0x04000455 RID: 1109
		private string mergedFreeBusyField;

		// Token: 0x04000456 RID: 1110
		private CalendarEvent[] calendarEventArrayField;

		// Token: 0x04000457 RID: 1111
		private WorkingHours workingHoursField;
	}
}
