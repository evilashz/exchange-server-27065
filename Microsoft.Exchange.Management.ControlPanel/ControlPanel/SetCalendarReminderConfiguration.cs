using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000074 RID: 116
	[DataContract]
	public class SetCalendarReminderConfiguration : SetCalendarConfigurationBase
	{
		// Token: 0x1700187E RID: 6270
		// (get) Token: 0x06001B18 RID: 6936 RVA: 0x000567ED File Offset: 0x000549ED
		// (set) Token: 0x06001B19 RID: 6937 RVA: 0x00056809 File Offset: 0x00054A09
		[DataMember]
		public bool RemindersEnabled
		{
			get
			{
				return (bool)(base["RemindersEnabled"] ?? false);
			}
			set
			{
				base["RemindersEnabled"] = value;
			}
		}

		// Token: 0x1700187F RID: 6271
		// (get) Token: 0x06001B1A RID: 6938 RVA: 0x0005681C File Offset: 0x00054A1C
		// (set) Token: 0x06001B1B RID: 6939 RVA: 0x00056838 File Offset: 0x00054A38
		[DataMember]
		public bool ReminderSoundEnabled
		{
			get
			{
				return (bool)(base["ReminderSoundEnabled"] ?? false);
			}
			set
			{
				base["ReminderSoundEnabled"] = value;
			}
		}

		// Token: 0x17001880 RID: 6272
		// (get) Token: 0x06001B1C RID: 6940 RVA: 0x0005684C File Offset: 0x00054A4C
		// (set) Token: 0x06001B1D RID: 6941 RVA: 0x00056880 File Offset: 0x00054A80
		[DataMember]
		public int DefaultReminderTime
		{
			get
			{
				return (int)((TimeSpan)(base["DefaultReminderTime"] ?? TimeSpan.Zero)).TotalMinutes;
			}
			set
			{
				base["DefaultReminderTime"] = TimeSpan.FromMinutes((double)value);
			}
		}
	}
}
