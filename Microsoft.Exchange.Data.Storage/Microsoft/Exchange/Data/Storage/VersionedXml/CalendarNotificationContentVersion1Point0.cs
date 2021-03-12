using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.Data.Storage.VersionedXml
{
	// Token: 0x02000EC2 RID: 3778
	[CalendarNotificationContentRoot]
	[Serializable]
	public class CalendarNotificationContentVersion1Point0 : CalendarNotificationContentBase
	{
		// Token: 0x0600828C RID: 33420 RVA: 0x00239CAC File Offset: 0x00237EAC
		public CalendarNotificationContentVersion1Point0() : base(new Version(1, 0))
		{
		}

		// Token: 0x0600828D RID: 33421 RVA: 0x00239CBB File Offset: 0x00237EBB
		public CalendarNotificationContentVersion1Point0(CalendarNotificationType type, string typeDesc, IEnumerable<CalendarEvent> events)
		{
			this.CalNotifType = type;
			this.CalNotifTypeDesc = typeDesc;
			if (events != null)
			{
				this.CalEvents = new List<CalendarEvent>(events);
			}
		}

		// Token: 0x17002296 RID: 8854
		// (get) Token: 0x0600828E RID: 33422 RVA: 0x00239CE0 File Offset: 0x00237EE0
		// (set) Token: 0x0600828F RID: 33423 RVA: 0x00239CE8 File Offset: 0x00237EE8
		[XmlElement("CalNotifType")]
		public CalendarNotificationType CalNotifType { get; set; }

		// Token: 0x17002297 RID: 8855
		// (get) Token: 0x06008290 RID: 33424 RVA: 0x00239CF1 File Offset: 0x00237EF1
		// (set) Token: 0x06008291 RID: 33425 RVA: 0x00239CF9 File Offset: 0x00237EF9
		[XmlElement("CalNotifTypeDesc")]
		public string CalNotifTypeDesc { get; set; }

		// Token: 0x17002298 RID: 8856
		// (get) Token: 0x06008292 RID: 33426 RVA: 0x00239D02 File Offset: 0x00237F02
		// (set) Token: 0x06008293 RID: 33427 RVA: 0x00239D0F File Offset: 0x00237F0F
		[XmlElement("CalEvent")]
		public List<CalendarEvent> CalEvents
		{
			get
			{
				return AccessorTemplates.ListPropertyGetter<CalendarEvent>(ref this.calEvents);
			}
			set
			{
				AccessorTemplates.ListPropertySetter<CalendarEvent>(ref this.calEvents, value);
			}
		}

		// Token: 0x04005785 RID: 22405
		private List<CalendarEvent> calEvents;
	}
}
