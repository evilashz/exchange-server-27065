using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Storage.VersionedXml
{
	// Token: 0x02000ECC RID: 3788
	[Serializable]
	public class TimeSlotMonitoringSettings : SwitchableSettingsBase
	{
		// Token: 0x060082A9 RID: 33449 RVA: 0x00239E06 File Offset: 0x00238006
		public TimeSlotMonitoringSettings()
		{
		}

		// Token: 0x060082AA RID: 33450 RVA: 0x00239E0E File Offset: 0x0023800E
		public TimeSlotMonitoringSettings(bool enabled, bool notifyInWorkHoursTimeSlot, DateTime notifyingStartTimeInDay, DateTime notifyingEndTimeInDay, Duration duration) : base(enabled)
		{
			this.NotifyInWorkHoursTimeSlot = notifyInWorkHoursTimeSlot;
			this.NotifyingStartTimeInDay = notifyingStartTimeInDay;
			this.NotifyingEndTimeInDay = notifyingEndTimeInDay;
			this.Duration = duration;
		}

		// Token: 0x170022A0 RID: 8864
		// (get) Token: 0x060082AB RID: 33451 RVA: 0x00239E35 File Offset: 0x00238035
		// (set) Token: 0x060082AC RID: 33452 RVA: 0x00239E3D File Offset: 0x0023803D
		[XmlElement("NotifyInWorkHoursTimeSlot")]
		public bool NotifyInWorkHoursTimeSlot { get; set; }

		// Token: 0x170022A1 RID: 8865
		// (get) Token: 0x060082AD RID: 33453 RVA: 0x00239E46 File Offset: 0x00238046
		// (set) Token: 0x060082AE RID: 33454 RVA: 0x00239E4E File Offset: 0x0023804E
		[XmlElement("NotifyingStartTimeInDay")]
		public DateTime NotifyingStartTimeInDay { get; set; }

		// Token: 0x170022A2 RID: 8866
		// (get) Token: 0x060082AF RID: 33455 RVA: 0x00239E57 File Offset: 0x00238057
		// (set) Token: 0x060082B0 RID: 33456 RVA: 0x00239E5F File Offset: 0x0023805F
		[XmlElement("NotifyingEndTimeInDay")]
		public DateTime NotifyingEndTimeInDay { get; set; }

		// Token: 0x170022A3 RID: 8867
		// (get) Token: 0x060082B1 RID: 33457 RVA: 0x00239E68 File Offset: 0x00238068
		// (set) Token: 0x060082B2 RID: 33458 RVA: 0x00239E75 File Offset: 0x00238075
		[XmlElement("Duration")]
		public Duration Duration
		{
			get
			{
				return AccessorTemplates.DefaultConstructionPropertyGetter<Duration>(ref this.duration);
			}
			set
			{
				this.duration = value;
			}
		}

		// Token: 0x040057B3 RID: 22451
		private Duration duration;
	}
}
