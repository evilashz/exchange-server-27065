using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Management.ControlPanel.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000073 RID: 115
	[DataContract]
	public class CalendarReminderConfiguration : CalendarConfigurationBase
	{
		// Token: 0x06001B10 RID: 6928 RVA: 0x00056608 File Offset: 0x00054808
		public CalendarReminderConfiguration(MailboxCalendarConfiguration mailboxCalendarConfiguration) : base(mailboxCalendarConfiguration)
		{
		}

		// Token: 0x1700187B RID: 6267
		// (get) Token: 0x06001B11 RID: 6929 RVA: 0x00056611 File Offset: 0x00054811
		// (set) Token: 0x06001B12 RID: 6930 RVA: 0x0005661E File Offset: 0x0005481E
		[DataMember]
		public bool RemindersEnabled
		{
			get
			{
				return base.MailboxCalendarConfiguration.RemindersEnabled;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700187C RID: 6268
		// (get) Token: 0x06001B13 RID: 6931 RVA: 0x00056625 File Offset: 0x00054825
		// (set) Token: 0x06001B14 RID: 6932 RVA: 0x00056632 File Offset: 0x00054832
		[DataMember]
		public bool ReminderSoundEnabled
		{
			get
			{
				return base.MailboxCalendarConfiguration.ReminderSoundEnabled;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700187D RID: 6269
		// (get) Token: 0x06001B15 RID: 6933 RVA: 0x0005663C File Offset: 0x0005483C
		// (set) Token: 0x06001B16 RID: 6934 RVA: 0x00056663 File Offset: 0x00054863
		[DataMember]
		public Identity DefaultReminderTime
		{
			get
			{
				return this.ReminderTime((int)base.MailboxCalendarConfiguration.DefaultReminderTime.TotalMinutes);
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06001B17 RID: 6935 RVA: 0x0005666C File Offset: 0x0005486C
		private Identity ReminderTime(int totalMinutes)
		{
			int num = totalMinutes / 10080;
			int num2 = totalMinutes % 10080 / 1440;
			int num3 = totalMinutes % 10080 % 1440 / 60;
			int num4 = totalMinutes % 10080 % 1440 % 60;
			string text = (num > 0) ? string.Format("{0}{1}{0}", RtlUtil.DecodedDirectionMark, string.Format("{0} {1} ", num, (num > 1) ? OwaOptionStrings.Weeks : OwaOptionStrings.Week)) : string.Empty;
			string text2 = (num2 > 0) ? string.Format("{0}{1}{0}", RtlUtil.DecodedDirectionMark, string.Format("{0} {1} ", num2, (num2 > 1) ? OwaOptionStrings.Days : OwaOptionStrings.Day)) : string.Empty;
			string text3 = (num3 > 0) ? string.Format("{0}{1}{0}", RtlUtil.DecodedDirectionMark, string.Format("{0} {1} ", num3, (num3 > 1) ? OwaOptionStrings.Hours : OwaOptionStrings.Hour)) : string.Empty;
			string text4 = (num4 > 0) ? string.Format("{0}{1}{0}", RtlUtil.DecodedDirectionMark, string.Format("{0} {1} ", num4, (num4 > 1) ? OwaOptionStrings.Minutes : OwaOptionStrings.Minute)) : string.Empty;
			return new Identity(totalMinutes.ToString(), string.Format("{0}{1}{2}{3}", new object[]
			{
				text,
				text2,
				text3,
				text4
			}));
		}
	}
}
