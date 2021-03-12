using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000B16 RID: 2838
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[DataContract]
	internal class EventTimeBasedInboxReminderState : IReminderState
	{
		// Token: 0x060066E8 RID: 26344 RVA: 0x001B4360 File Offset: 0x001B2560
		public EventTimeBasedInboxReminderState()
		{
			this.Initialize();
		}

		// Token: 0x17001C4A RID: 7242
		// (get) Token: 0x060066E9 RID: 26345 RVA: 0x001B436E File Offset: 0x001B256E
		// (set) Token: 0x060066EA RID: 26346 RVA: 0x001B4376 File Offset: 0x001B2576
		[DataMember]
		public Guid Identifier { get; set; }

		// Token: 0x17001C4B RID: 7243
		// (get) Token: 0x060066EB RID: 26347 RVA: 0x001B437F File Offset: 0x001B257F
		// (set) Token: 0x060066EC RID: 26348 RVA: 0x001B438D File Offset: 0x001B258D
		[IgnoreDataMember]
		public ExDateTime ScheduledReminderTime
		{
			get
			{
				return this.ToExDateTime(this.InternalScheduledReminderTime);
			}
			set
			{
				this.InternalScheduledReminderTime = this.ToDateTime(value);
			}
		}

		// Token: 0x17001C4C RID: 7244
		// (get) Token: 0x060066ED RID: 26349 RVA: 0x001B439C File Offset: 0x001B259C
		// (set) Token: 0x060066EE RID: 26350 RVA: 0x001B43A4 File Offset: 0x001B25A4
		[DataMember]
		private DateTime InternalScheduledReminderTime { get; set; }

		// Token: 0x060066EF RID: 26351 RVA: 0x001B43AD File Offset: 0x001B25AD
		[OnDeserializing]
		public void OnDeserializing(StreamingContext context)
		{
			this.Initialize();
		}

		// Token: 0x060066F0 RID: 26352 RVA: 0x001B43B5 File Offset: 0x001B25B5
		public int GetCurrentVersion()
		{
			return 1;
		}

		// Token: 0x060066F1 RID: 26353 RVA: 0x001B43B8 File Offset: 0x001B25B8
		private void Initialize()
		{
			this.ScheduledReminderTime = ExDateTime.MinValue;
		}

		// Token: 0x060066F2 RID: 26354 RVA: 0x001B43C5 File Offset: 0x001B25C5
		private DateTime ToDateTime(ExDateTime exDateTime)
		{
			return exDateTime.UniversalTime;
		}

		// Token: 0x060066F3 RID: 26355 RVA: 0x001B43CE File Offset: 0x001B25CE
		private ExDateTime ToExDateTime(DateTime dateTime)
		{
			return new ExDateTime(ExTimeZone.UtcTimeZone, dateTime);
		}

		// Token: 0x04003A56 RID: 14934
		private const int CurrentVersion = 1;
	}
}
