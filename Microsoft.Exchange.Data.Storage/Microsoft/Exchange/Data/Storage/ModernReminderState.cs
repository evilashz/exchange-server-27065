using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000B1C RID: 2844
	[DataContract]
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ModernReminderState : IReminderState
	{
		// Token: 0x06006722 RID: 26402 RVA: 0x001B4658 File Offset: 0x001B2858
		public ModernReminderState()
		{
			this.Initialize();
		}

		// Token: 0x17001C5F RID: 7263
		// (get) Token: 0x06006723 RID: 26403 RVA: 0x001B4666 File Offset: 0x001B2866
		// (set) Token: 0x06006724 RID: 26404 RVA: 0x001B466E File Offset: 0x001B286E
		[DataMember]
		public Guid Identifier { get; set; }

		// Token: 0x17001C60 RID: 7264
		// (get) Token: 0x06006725 RID: 26405 RVA: 0x001B4677 File Offset: 0x001B2877
		// (set) Token: 0x06006726 RID: 26406 RVA: 0x001B4685 File Offset: 0x001B2885
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

		// Token: 0x17001C61 RID: 7265
		// (get) Token: 0x06006727 RID: 26407 RVA: 0x001B4694 File Offset: 0x001B2894
		// (set) Token: 0x06006728 RID: 26408 RVA: 0x001B469C File Offset: 0x001B289C
		[DataMember]
		private DateTime InternalScheduledReminderTime { get; set; }

		// Token: 0x06006729 RID: 26409 RVA: 0x001B46A5 File Offset: 0x001B28A5
		[OnDeserializing]
		public void OnDeserializing(StreamingContext context)
		{
			this.Initialize();
		}

		// Token: 0x0600672A RID: 26410 RVA: 0x001B46AD File Offset: 0x001B28AD
		public int GetCurrentVersion()
		{
			return 1;
		}

		// Token: 0x0600672B RID: 26411 RVA: 0x001B46B0 File Offset: 0x001B28B0
		private void Initialize()
		{
			this.ScheduledReminderTime = ExDateTime.MinValue;
		}

		// Token: 0x0600672C RID: 26412 RVA: 0x001B46BD File Offset: 0x001B28BD
		private DateTime ToDateTime(ExDateTime exDateTime)
		{
			return exDateTime.UniversalTime;
		}

		// Token: 0x0600672D RID: 26413 RVA: 0x001B46C6 File Offset: 0x001B28C6
		private ExDateTime ToExDateTime(DateTime dateTime)
		{
			return new ExDateTime(ExTimeZone.UtcTimeZone, dateTime);
		}

		// Token: 0x04003A7E RID: 14974
		private const int CurrentVersion = 1;
	}
}
