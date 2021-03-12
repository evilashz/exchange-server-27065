using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Storage.VersionedXml
{
	// Token: 0x02000ED0 RID: 3792
	[CalendarNotificationSettingsRoot]
	[Serializable]
	public class CalendarNotificationSettingsVersion1Point0 : CalendarNotificationSettingsBase
	{
		// Token: 0x060082D3 RID: 33491 RVA: 0x00239FFB File Offset: 0x002381FB
		public CalendarNotificationSettingsVersion1Point0() : base(new Version(1, 0))
		{
		}

		// Token: 0x060082D4 RID: 33492 RVA: 0x0023A00A File Offset: 0x0023820A
		public CalendarNotificationSettingsVersion1Point0(TimeSlotMonitoringSettings updateSettings, TimeSlotMonitoringSettings reminderSettings, TimePointScaningSettings summarySettings, IEnumerable<Emitter> emitters) : base(new Version(1, 0))
		{
			this.UpdateSettings = updateSettings;
			this.ReminderSettings = reminderSettings;
			this.SummarySettings = summarySettings;
			if (emitters != null)
			{
				this.Emitters = new List<Emitter>(emitters);
			}
		}

		// Token: 0x170022B1 RID: 8881
		// (get) Token: 0x060082D5 RID: 33493 RVA: 0x0023A03F File Offset: 0x0023823F
		// (set) Token: 0x060082D6 RID: 33494 RVA: 0x0023A04C File Offset: 0x0023824C
		[XmlElement("UpdateSettings")]
		public TimeSlotMonitoringSettings UpdateSettings
		{
			get
			{
				return AccessorTemplates.DefaultConstructionPropertyGetter<TimeSlotMonitoringSettings>(ref this.updateSettings);
			}
			set
			{
				this.updateSettings = value;
			}
		}

		// Token: 0x170022B2 RID: 8882
		// (get) Token: 0x060082D7 RID: 33495 RVA: 0x0023A055 File Offset: 0x00238255
		// (set) Token: 0x060082D8 RID: 33496 RVA: 0x0023A062 File Offset: 0x00238262
		[XmlElement("ReminderSettings")]
		public TimeSlotMonitoringSettings ReminderSettings
		{
			get
			{
				return AccessorTemplates.DefaultConstructionPropertyGetter<TimeSlotMonitoringSettings>(ref this.reminderSettings);
			}
			set
			{
				this.reminderSettings = value;
			}
		}

		// Token: 0x170022B3 RID: 8883
		// (get) Token: 0x060082D9 RID: 33497 RVA: 0x0023A06B File Offset: 0x0023826B
		// (set) Token: 0x060082DA RID: 33498 RVA: 0x0023A078 File Offset: 0x00238278
		[XmlElement("SummarySettings")]
		public TimePointScaningSettings SummarySettings
		{
			get
			{
				return AccessorTemplates.DefaultConstructionPropertyGetter<TimePointScaningSettings>(ref this.summarySettings);
			}
			set
			{
				this.summarySettings = value;
			}
		}

		// Token: 0x170022B4 RID: 8884
		// (get) Token: 0x060082DB RID: 33499 RVA: 0x0023A081 File Offset: 0x00238281
		// (set) Token: 0x060082DC RID: 33500 RVA: 0x0023A08E File Offset: 0x0023828E
		[XmlElement("Emitter")]
		public List<Emitter> Emitters
		{
			get
			{
				return AccessorTemplates.ListPropertyGetter<Emitter>(ref this.emitters);
			}
			set
			{
				AccessorTemplates.ListPropertySetter<Emitter>(ref this.emitters, value);
			}
		}

		// Token: 0x040057C4 RID: 22468
		private TimeSlotMonitoringSettings updateSettings;

		// Token: 0x040057C5 RID: 22469
		private TimeSlotMonitoringSettings reminderSettings;

		// Token: 0x040057C6 RID: 22470
		private TimePointScaningSettings summarySettings;

		// Token: 0x040057C7 RID: 22471
		private List<Emitter> emitters;
	}
}
