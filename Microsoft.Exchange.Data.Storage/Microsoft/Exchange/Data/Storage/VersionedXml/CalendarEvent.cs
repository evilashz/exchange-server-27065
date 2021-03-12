using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Storage.VersionedXml
{
	// Token: 0x02000EC1 RID: 3777
	[Serializable]
	public class CalendarEvent
	{
		// Token: 0x0600827A RID: 33402 RVA: 0x00239BC9 File Offset: 0x00237DC9
		public CalendarEvent()
		{
		}

		// Token: 0x0600827B RID: 33403 RVA: 0x00239BD4 File Offset: 0x00237DD4
		public CalendarEvent(string dowOfStartTime, string dateOfStartTime, string timeOfStartTime, string dowOfEndTime, string dateOfEndTime, string timeOfEndTime, string subject, string location)
		{
			this.DayOfWeekOfStartTime = dowOfStartTime;
			this.DateOfStartTime = dateOfStartTime;
			this.TimeOfStartTime = timeOfStartTime;
			this.DayOfWeekOfEndTime = dowOfEndTime;
			this.DateOfEndTime = dateOfEndTime;
			this.TimeOfEndTime = timeOfEndTime;
			this.Subject = subject;
			this.Location = location;
		}

		// Token: 0x1700228E RID: 8846
		// (get) Token: 0x0600827C RID: 33404 RVA: 0x00239C24 File Offset: 0x00237E24
		// (set) Token: 0x0600827D RID: 33405 RVA: 0x00239C2C File Offset: 0x00237E2C
		[XmlElement("DayOfWeekOfStartTime")]
		public string DayOfWeekOfStartTime { get; set; }

		// Token: 0x1700228F RID: 8847
		// (get) Token: 0x0600827E RID: 33406 RVA: 0x00239C35 File Offset: 0x00237E35
		// (set) Token: 0x0600827F RID: 33407 RVA: 0x00239C3D File Offset: 0x00237E3D
		[XmlElement("DateOfStartTime")]
		public string DateOfStartTime { get; set; }

		// Token: 0x17002290 RID: 8848
		// (get) Token: 0x06008280 RID: 33408 RVA: 0x00239C46 File Offset: 0x00237E46
		// (set) Token: 0x06008281 RID: 33409 RVA: 0x00239C4E File Offset: 0x00237E4E
		[XmlElement("TimeOfStartTime")]
		public string TimeOfStartTime { get; set; }

		// Token: 0x17002291 RID: 8849
		// (get) Token: 0x06008282 RID: 33410 RVA: 0x00239C57 File Offset: 0x00237E57
		// (set) Token: 0x06008283 RID: 33411 RVA: 0x00239C5F File Offset: 0x00237E5F
		[XmlElement("DayOfWeekOfEndTime")]
		public string DayOfWeekOfEndTime { get; set; }

		// Token: 0x17002292 RID: 8850
		// (get) Token: 0x06008284 RID: 33412 RVA: 0x00239C68 File Offset: 0x00237E68
		// (set) Token: 0x06008285 RID: 33413 RVA: 0x00239C70 File Offset: 0x00237E70
		[XmlElement("DateOfEndTime")]
		public string DateOfEndTime { get; set; }

		// Token: 0x17002293 RID: 8851
		// (get) Token: 0x06008286 RID: 33414 RVA: 0x00239C79 File Offset: 0x00237E79
		// (set) Token: 0x06008287 RID: 33415 RVA: 0x00239C81 File Offset: 0x00237E81
		[XmlElement("TimeOfEndTime")]
		public string TimeOfEndTime { get; set; }

		// Token: 0x17002294 RID: 8852
		// (get) Token: 0x06008288 RID: 33416 RVA: 0x00239C8A File Offset: 0x00237E8A
		// (set) Token: 0x06008289 RID: 33417 RVA: 0x00239C92 File Offset: 0x00237E92
		[XmlElement("Subject")]
		public string Subject { get; set; }

		// Token: 0x17002295 RID: 8853
		// (get) Token: 0x0600828A RID: 33418 RVA: 0x00239C9B File Offset: 0x00237E9B
		// (set) Token: 0x0600828B RID: 33419 RVA: 0x00239CA3 File Offset: 0x00237EA3
		[XmlElement("Location")]
		public string Location { get; set; }
	}
}
