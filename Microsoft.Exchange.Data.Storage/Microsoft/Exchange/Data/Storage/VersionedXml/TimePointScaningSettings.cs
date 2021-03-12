using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Storage.VersionedXml
{
	// Token: 0x02000ECE RID: 3790
	[Serializable]
	public class TimePointScaningSettings : SwitchableSettingsBase
	{
		// Token: 0x060082C1 RID: 33473 RVA: 0x00239F21 File Offset: 0x00238121
		public TimePointScaningSettings()
		{
		}

		// Token: 0x060082C2 RID: 33474 RVA: 0x00239F29 File Offset: 0x00238129
		public TimePointScaningSettings(bool enabled, DateTime notifyingTimeInDay, Duration duration, Recurrence recurrence) : base(enabled)
		{
			this.NotifyingTimeInDay = notifyingTimeInDay;
			this.Duration = duration;
			this.Recurrence = recurrence;
		}

		// Token: 0x170022AA RID: 8874
		// (get) Token: 0x060082C3 RID: 33475 RVA: 0x00239F48 File Offset: 0x00238148
		// (set) Token: 0x060082C4 RID: 33476 RVA: 0x00239F50 File Offset: 0x00238150
		[XmlElement("NotifyingTimeInDay")]
		public DateTime NotifyingTimeInDay { get; set; }

		// Token: 0x170022AB RID: 8875
		// (get) Token: 0x060082C5 RID: 33477 RVA: 0x00239F59 File Offset: 0x00238159
		// (set) Token: 0x060082C6 RID: 33478 RVA: 0x00239F66 File Offset: 0x00238166
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

		// Token: 0x170022AC RID: 8876
		// (get) Token: 0x060082C7 RID: 33479 RVA: 0x00239F6F File Offset: 0x0023816F
		// (set) Token: 0x060082C8 RID: 33480 RVA: 0x00239F7C File Offset: 0x0023817C
		[XmlElement("Recurrence")]
		public Recurrence Recurrence
		{
			get
			{
				return AccessorTemplates.DefaultConstructionPropertyGetter<Recurrence>(ref this.recurrence);
			}
			set
			{
				this.recurrence = value;
			}
		}

		// Token: 0x040057BD RID: 22461
		private Duration duration;

		// Token: 0x040057BE RID: 22462
		private Recurrence recurrence;
	}
}
