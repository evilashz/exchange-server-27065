using System;
using System.Globalization;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Management.ControlPanel.WebControls;
using Microsoft.Exchange.PowerShell.RbacHostingTools;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200005F RID: 95
	[DataContract]
	public class CalendarAppearanceConfiguration : CalendarConfigurationBase
	{
		// Token: 0x06001A35 RID: 6709 RVA: 0x0005400A File Offset: 0x0005220A
		public CalendarAppearanceConfiguration(MailboxCalendarConfiguration mailboxCalendarConfiguration) : base(mailboxCalendarConfiguration)
		{
			this.workingHoursTimeZone = base.MailboxCalendarConfiguration.WorkingHoursTimeZone.ExTimeZone;
			this.currentUserTimeZone = RbacPrincipal.Current.UserTimeZone;
		}

		// Token: 0x17001832 RID: 6194
		// (get) Token: 0x06001A36 RID: 6710 RVA: 0x00054039 File Offset: 0x00052239
		// (set) Token: 0x06001A37 RID: 6711 RVA: 0x00054046 File Offset: 0x00052246
		[DataMember]
		public int WorkDays
		{
			get
			{
				return (int)base.MailboxCalendarConfiguration.WorkDays;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001833 RID: 6195
		// (get) Token: 0x06001A38 RID: 6712 RVA: 0x00054050 File Offset: 0x00052250
		// (set) Token: 0x06001A39 RID: 6713 RVA: 0x00054077 File Offset: 0x00052277
		[DataMember]
		public Identity WorkingHoursStartTime
		{
			get
			{
				return this.WorkingHoursTime((int)base.MailboxCalendarConfiguration.WorkingHoursStartTime.TotalMinutes);
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001834 RID: 6196
		// (get) Token: 0x06001A3A RID: 6714 RVA: 0x00054080 File Offset: 0x00052280
		// (set) Token: 0x06001A3B RID: 6715 RVA: 0x000540A7 File Offset: 0x000522A7
		[DataMember]
		public Identity WorkingHoursEndTime
		{
			get
			{
				return this.WorkingHoursTime((int)base.MailboxCalendarConfiguration.WorkingHoursEndTime.TotalMinutes);
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001835 RID: 6197
		// (get) Token: 0x06001A3C RID: 6716 RVA: 0x000540B0 File Offset: 0x000522B0
		// (set) Token: 0x06001A3D RID: 6717 RVA: 0x000540EE File Offset: 0x000522EE
		[DataMember]
		public string WorkingHoursTimeZone
		{
			get
			{
				return string.Format(OwaOptionStrings.TimeZoneLabelText, RtlUtil.ConvertToBidiString(this.workingHoursTimeZone.LocalizableDisplayName.ToString(CultureInfo.CurrentCulture), RtlUtil.IsRtl));
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001836 RID: 6198
		// (get) Token: 0x06001A3E RID: 6718 RVA: 0x000540F5 File Offset: 0x000522F5
		// (set) Token: 0x06001A3F RID: 6719 RVA: 0x0005411C File Offset: 0x0005231C
		[DataMember]
		public bool IsTimeZoneDifferent
		{
			get
			{
				return this.currentUserTimeZone != null && this.workingHoursTimeZone.Id != this.currentUserTimeZone.Id;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001837 RID: 6199
		// (get) Token: 0x06001A40 RID: 6720 RVA: 0x00054124 File Offset: 0x00052324
		// (set) Token: 0x06001A41 RID: 6721 RVA: 0x00054175 File Offset: 0x00052375
		[DataMember]
		public string UpdateTimeZoneNoteLink
		{
			get
			{
				ExTimeZone exTimeZone = (this.currentUserTimeZone == null) ? ExTimeZone.CurrentTimeZone : this.currentUserTimeZone;
				string arg = RtlUtil.ConvertToBidiString(exTimeZone.LocalizableDisplayName.ToString(CultureInfo.CurrentCulture), RtlUtil.IsRtl);
				return string.Format(OwaOptionStrings.UpdateTimeZoneNoteLinkText, arg);
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001838 RID: 6200
		// (get) Token: 0x06001A42 RID: 6722 RVA: 0x0005417C File Offset: 0x0005237C
		// (set) Token: 0x06001A43 RID: 6723 RVA: 0x00054189 File Offset: 0x00052389
		[DataMember]
		public int WeekStartDay
		{
			get
			{
				return (int)base.MailboxCalendarConfiguration.WeekStartDay;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001839 RID: 6201
		// (get) Token: 0x06001A44 RID: 6724 RVA: 0x00054190 File Offset: 0x00052390
		// (set) Token: 0x06001A45 RID: 6725 RVA: 0x000541AC File Offset: 0x000523AC
		[DataMember]
		public int FirstWeekOfYear
		{
			get
			{
				if (base.MailboxCalendarConfiguration.FirstWeekOfYear != FirstWeekRules.LegacyNotSet)
				{
					return (int)base.MailboxCalendarConfiguration.FirstWeekOfYear;
				}
				return 1;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700183A RID: 6202
		// (get) Token: 0x06001A46 RID: 6726 RVA: 0x000541B3 File Offset: 0x000523B3
		// (set) Token: 0x06001A47 RID: 6727 RVA: 0x000541C0 File Offset: 0x000523C0
		[DataMember]
		public bool ShowWeekNumbers
		{
			get
			{
				return base.MailboxCalendarConfiguration.ShowWeekNumbers;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700183B RID: 6203
		// (get) Token: 0x06001A48 RID: 6728 RVA: 0x000541C8 File Offset: 0x000523C8
		// (set) Token: 0x06001A49 RID: 6729 RVA: 0x000541E8 File Offset: 0x000523E8
		[DataMember]
		public string TimeIncrement
		{
			get
			{
				return ((int)base.MailboxCalendarConfiguration.TimeIncrement).ToString();
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06001A4A RID: 6730 RVA: 0x000541F0 File Offset: 0x000523F0
		private Identity WorkingHoursTime(int minutes)
		{
			DateTime dateTime = DateTime.UtcNow.Date + TimeSpan.FromMinutes((double)minutes);
			return new Identity(minutes.ToString(), dateTime.ToString(RbacPrincipal.Current.TimeFormat));
		}

		// Token: 0x04001B0F RID: 6927
		private ExTimeZone workingHoursTimeZone;

		// Token: 0x04001B10 RID: 6928
		private ExTimeZone currentUserTimeZone;
	}
}
