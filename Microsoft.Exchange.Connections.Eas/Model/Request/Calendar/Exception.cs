using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.Exchange.Connections.Eas.Model.Common.Email;
using Microsoft.Exchange.Connections.Eas.Model.Request.AirSyncBase;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Model.Request.Calendar
{
	// Token: 0x0200009E RID: 158
	[ClassAccessLevel(AccessLevel.Implementation)]
	[XmlType(Namespace = "Calendar", TypeName = "Exception")]
	public class Exception : ICalendarData
	{
		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000393 RID: 915 RVA: 0x0000A157 File Offset: 0x00008357
		// (set) Token: 0x06000394 RID: 916 RVA: 0x0000A15F File Offset: 0x0000835F
		[XmlElement(ElementName = "Deleted")]
		public byte? Deleted { get; set; }

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000395 RID: 917 RVA: 0x0000A168 File Offset: 0x00008368
		// (set) Token: 0x06000396 RID: 918 RVA: 0x0000A170 File Offset: 0x00008370
		[XmlElement(ElementName = "StartTime")]
		public string StartTime { get; set; }

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000397 RID: 919 RVA: 0x0000A179 File Offset: 0x00008379
		// (set) Token: 0x06000398 RID: 920 RVA: 0x0000A181 File Offset: 0x00008381
		[XmlElement(ElementName = "Body", Namespace = "AirSyncBase")]
		public Body Body { get; set; }

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06000399 RID: 921 RVA: 0x0000A18A File Offset: 0x0000838A
		// (set) Token: 0x0600039A RID: 922 RVA: 0x0000A192 File Offset: 0x00008392
		[XmlElement(ElementName = "Subject")]
		public string CalendarSubject { get; set; }

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x0600039B RID: 923 RVA: 0x0000A19B File Offset: 0x0000839B
		// (set) Token: 0x0600039C RID: 924 RVA: 0x0000A1A3 File Offset: 0x000083A3
		[XmlElement(ElementName = "EndTime")]
		public string EndTime { get; set; }

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x0600039D RID: 925 RVA: 0x0000A1AC File Offset: 0x000083AC
		// (set) Token: 0x0600039E RID: 926 RVA: 0x0000A1B4 File Offset: 0x000083B4
		[XmlElement(ElementName = "ExceptionStartTime")]
		public string ExceptionStartTime { get; set; }

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x0600039F RID: 927 RVA: 0x0000A1BD File Offset: 0x000083BD
		// (set) Token: 0x060003A0 RID: 928 RVA: 0x0000A1C5 File Offset: 0x000083C5
		[XmlElement(ElementName = "BusyStatus")]
		public byte? BusyStatus { get; set; }

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x060003A1 RID: 929 RVA: 0x0000A1CE File Offset: 0x000083CE
		// (set) Token: 0x060003A2 RID: 930 RVA: 0x0000A1D6 File Offset: 0x000083D6
		[XmlElement(ElementName = "AllDayEvent")]
		public byte? AllDayEvent { get; set; }

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x060003A3 RID: 931 RVA: 0x0000A1DF File Offset: 0x000083DF
		// (set) Token: 0x060003A4 RID: 932 RVA: 0x0000A1E7 File Offset: 0x000083E7
		[XmlElement(ElementName = "Location")]
		public string Location { get; set; }

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x060003A5 RID: 933 RVA: 0x0000A1F0 File Offset: 0x000083F0
		// (set) Token: 0x060003A6 RID: 934 RVA: 0x0000A1F8 File Offset: 0x000083F8
		[XmlElement(ElementName = "Reminder")]
		public uint? Reminder { get; set; }

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x060003A7 RID: 935 RVA: 0x0000A201 File Offset: 0x00008401
		// (set) Token: 0x060003A8 RID: 936 RVA: 0x0000A209 File Offset: 0x00008409
		[XmlElement(ElementName = "Sensitivity", Namespace = "Calendar")]
		public byte? Sensitivity { get; set; }

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060003A9 RID: 937 RVA: 0x0000A212 File Offset: 0x00008412
		// (set) Token: 0x060003AA RID: 938 RVA: 0x0000A21A File Offset: 0x0000841A
		[XmlElement(ElementName = "DtStamp", Namespace = "Calendar")]
		public string DtStamp { get; set; }

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060003AB RID: 939 RVA: 0x0000A223 File Offset: 0x00008423
		// (set) Token: 0x060003AC RID: 940 RVA: 0x0000A22B File Offset: 0x0000842B
		[XmlElement(ElementName = "MeetingStatus", Namespace = "Calendar")]
		public byte? MeetingStatus { get; set; }

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x060003AD RID: 941 RVA: 0x0000A234 File Offset: 0x00008434
		// (set) Token: 0x060003AE RID: 942 RVA: 0x0000A23C File Offset: 0x0000843C
		[XmlArray(ElementName = "Attendees", Namespace = "Calendar")]
		public List<Attendee> Attendees { get; set; }

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x060003AF RID: 943 RVA: 0x0000A245 File Offset: 0x00008445
		// (set) Token: 0x060003B0 RID: 944 RVA: 0x0000A24D File Offset: 0x0000844D
		[XmlArray(ElementName = "Categories", Namespace = "Calendar")]
		public List<Category> CalendarCategories { get; set; }

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x060003B1 RID: 945 RVA: 0x0000A258 File Offset: 0x00008458
		[XmlIgnore]
		public bool DeletedSpecified
		{
			get
			{
				return this.Deleted != null;
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x060003B2 RID: 946 RVA: 0x0000A274 File Offset: 0x00008474
		[XmlIgnore]
		public bool AllDayEventSpecified
		{
			get
			{
				return this.AllDayEvent != null;
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x060003B3 RID: 947 RVA: 0x0000A290 File Offset: 0x00008490
		[XmlIgnore]
		public bool BusyStatusSpecified
		{
			get
			{
				return this.BusyStatus != null;
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x060003B4 RID: 948 RVA: 0x0000A2AC File Offset: 0x000084AC
		[XmlIgnore]
		public bool ReminderSpecified
		{
			get
			{
				return this.Reminder != null;
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x060003B5 RID: 949 RVA: 0x0000A2C8 File Offset: 0x000084C8
		[XmlIgnore]
		public bool SensitivitySpecified
		{
			get
			{
				return this.Sensitivity != null;
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x060003B6 RID: 950 RVA: 0x0000A2E4 File Offset: 0x000084E4
		[XmlIgnore]
		public bool MeetingStatusSpecified
		{
			get
			{
				return this.MeetingStatus != null;
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x060003B7 RID: 951 RVA: 0x0000A2FF File Offset: 0x000084FF
		[XmlIgnore]
		public bool AttendeesSpecified
		{
			get
			{
				return this.Attendees != null;
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x060003B8 RID: 952 RVA: 0x0000A30D File Offset: 0x0000850D
		[XmlIgnore]
		public bool CalendarCategoriesSpecified
		{
			get
			{
				return this.CalendarCategories != null;
			}
		}
	}
}
