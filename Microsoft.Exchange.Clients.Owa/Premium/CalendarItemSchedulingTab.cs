using System;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Clients.Owa.Premium.Controls;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x0200043B RID: 1083
	public class CalendarItemSchedulingTab : OwaForm
	{
		// Token: 0x17000AE2 RID: 2786
		// (get) Token: 0x06002719 RID: 10009 RVA: 0x000DF130 File Offset: 0x000DD330
		protected static int FreeBusyInterval
		{
			get
			{
				return 30;
			}
		}

		// Token: 0x17000AE3 RID: 2787
		// (get) Token: 0x0600271A RID: 10010 RVA: 0x000DF134 File Offset: 0x000DD334
		protected ExDateTime StartDate
		{
			get
			{
				return this.startDateTime;
			}
		}

		// Token: 0x17000AE4 RID: 2788
		// (get) Token: 0x0600271B RID: 10011 RVA: 0x000DF13C File Offset: 0x000DD33C
		protected ExDateTime EndDate
		{
			get
			{
				return this.endDateTime;
			}
		}

		// Token: 0x17000AE5 RID: 2789
		// (get) Token: 0x0600271C RID: 10012 RVA: 0x000DF144 File Offset: 0x000DD344
		protected int MeetingDuration
		{
			get
			{
				return this.meetingDuration;
			}
		}

		// Token: 0x17000AE6 RID: 2790
		// (get) Token: 0x0600271D RID: 10013 RVA: 0x000DF14C File Offset: 0x000DD34C
		protected bool Show24Hours
		{
			get
			{
				return this.show24Hours;
			}
		}

		// Token: 0x17000AE7 RID: 2791
		// (get) Token: 0x0600271E RID: 10014 RVA: 0x000DF154 File Offset: 0x000DD354
		protected int Days
		{
			get
			{
				return ((DateTime)this.endDateTime - (DateTime)this.startDateTime).Days + 1;
			}
		}

		// Token: 0x17000AE8 RID: 2792
		// (get) Token: 0x0600271F RID: 10015 RVA: 0x000DF188 File Offset: 0x000DD388
		internal WorkingHours WorkingHours
		{
			get
			{
				if (this.workingHours == null)
				{
					if (this.folderId == null || !this.folderId.IsOtherMailbox)
					{
						this.workingHours = base.UserContext.WorkingHours;
					}
					else
					{
						this.workingHours = base.UserContext.GetOthersWorkingHours(this.folderId);
					}
				}
				return this.workingHours;
			}
		}

		// Token: 0x17000AE9 RID: 2793
		// (get) Token: 0x06002720 RID: 10016 RVA: 0x000DF1E2 File Offset: 0x000DD3E2
		protected int HoursPerDay
		{
			get
			{
				if (this.show24Hours)
				{
					return 24;
				}
				return SchedulingTabRenderingUtilities.CalculateTotalWorkingHours(this.WorkingHours);
			}
		}

		// Token: 0x17000AEA RID: 2794
		// (get) Token: 0x06002721 RID: 10017 RVA: 0x000DF1FA File Offset: 0x000DD3FA
		internal IExchangePrincipal OrganizerExchangePrincipal
		{
			get
			{
				if (this.organizerExchangePrincipal == null)
				{
					this.organizerExchangePrincipal = Utilities.GetFolderOwnerExchangePrincipal(this.folderId, base.UserContext);
				}
				return this.organizerExchangePrincipal;
			}
		}

		// Token: 0x17000AEB RID: 2795
		// (get) Token: 0x06002722 RID: 10018 RVA: 0x000DF224 File Offset: 0x000DD424
		protected string OrganizerSmtpAddress
		{
			get
			{
				return this.OrganizerExchangePrincipal.MailboxInfo.PrimarySmtpAddress.ToString();
			}
		}

		// Token: 0x17000AEC RID: 2796
		// (get) Token: 0x06002723 RID: 10019 RVA: 0x000DF24F File Offset: 0x000DD44F
		protected string OrganizerEmailAddress
		{
			get
			{
				return this.OrganizerExchangePrincipal.LegacyDn;
			}
		}

		// Token: 0x17000AED RID: 2797
		// (get) Token: 0x06002724 RID: 10020 RVA: 0x000DF25C File Offset: 0x000DD45C
		protected string OrganizerDisplayName
		{
			get
			{
				return this.OrganizerExchangePrincipal.MailboxInfo.DisplayName;
			}
		}

		// Token: 0x17000AEE RID: 2798
		// (get) Token: 0x06002725 RID: 10021 RVA: 0x000DF270 File Offset: 0x000DD470
		protected string OrganizerAdObjectId
		{
			get
			{
				return Convert.ToBase64String(this.OrganizerExchangePrincipal.ObjectId.ObjectGuid.ToByteArray());
			}
		}

		// Token: 0x17000AEF RID: 2799
		// (get) Token: 0x06002726 RID: 10022 RVA: 0x000DF29A File Offset: 0x000DD49A
		protected RecipientWell RecipientWell
		{
			get
			{
				return this.recipientWell;
			}
		}

		// Token: 0x17000AF0 RID: 2800
		// (get) Token: 0x06002727 RID: 10023 RVA: 0x000DF2A2 File Offset: 0x000DD4A2
		protected int WorkDayStart
		{
			get
			{
				return SchedulingTabRenderingUtilities.GetWorkDayStartHour(this.WorkingHours, this.selectedDate) * 60;
			}
		}

		// Token: 0x17000AF1 RID: 2801
		// (get) Token: 0x06002728 RID: 10024 RVA: 0x000DF2B8 File Offset: 0x000DD4B8
		protected int WorkDayEnd
		{
			get
			{
				return SchedulingTabRenderingUtilities.GetWorkDayEndHour(this.WorkingHours, this.selectedDate) * 60;
			}
		}

		// Token: 0x17000AF2 RID: 2802
		// (get) Token: 0x06002729 RID: 10025 RVA: 0x000DF2CE File Offset: 0x000DD4CE
		private bool DifferentWorkingHoursTimeZone
		{
			get
			{
				return this.WorkingHours.IsTimeZoneDifferent;
			}
		}

		// Token: 0x17000AF3 RID: 2803
		// (get) Token: 0x0600272A RID: 10026 RVA: 0x000DF2DB File Offset: 0x000DD4DB
		protected bool ForceShowing24Hours
		{
			get
			{
				return this.WorkingHours != base.UserContext.WorkingHours || this.DifferentWorkingHoursTimeZone || this.WorkHoursDuration < 60;
			}
		}

		// Token: 0x17000AF4 RID: 2804
		// (get) Token: 0x0600272B RID: 10027 RVA: 0x000DF306 File Offset: 0x000DD506
		private int WorkHoursDuration
		{
			get
			{
				return this.WorkingHours.WorkDayEndTimeInWorkingHoursTimeZone - this.WorkingHours.WorkDayStartTimeInWorkingHoursTimeZone;
			}
		}

		// Token: 0x17000AF5 RID: 2805
		// (get) Token: 0x0600272C RID: 10028 RVA: 0x000DF31F File Offset: 0x000DD51F
		protected bool IsFolderIdNull
		{
			get
			{
				return this.folderId == null;
			}
		}

		// Token: 0x0600272D RID: 10029 RVA: 0x000DF32C File Offset: 0x000DD52C
		protected override void OnLoad(EventArgs e)
		{
			this.selectedDate = (this.startDateTime = Utilities.GetQueryStringParameterDateTime(base.Request, "sd", base.UserContext.TimeZone));
			this.endDateTime = Utilities.GetQueryStringParameterDateTime(base.Request, "ed", base.UserContext.TimeZone);
			string queryStringParameter = Utilities.GetQueryStringParameter(base.Request, "fid", false);
			if (!string.IsNullOrEmpty(queryStringParameter))
			{
				this.folderId = OwaStoreObjectId.CreateFromString(queryStringParameter);
			}
			if (this.startDateTime < this.endDateTime)
			{
				this.meetingDuration = (int)(this.endDateTime - this.startDateTime).TotalMinutes;
			}
			DatePickerBase.GetVisibleDateRange(this.selectedDate, out this.startDateTime, out this.endDateTime, base.UserContext.TimeZone);
			if (this.selectedDate.TimeOfDay.TotalMinutes < (double)this.WorkingHours.GetWorkDayStartTime(this.selectedDate) || (double)this.WorkingHours.GetWorkDayEndTime(this.selectedDate) <= this.selectedDate.TimeOfDay.TotalMinutes || (double)this.WorkingHours.GetWorkDayEndTime(this.selectedDate) < this.selectedDate.TimeOfDay.TotalMinutes + (double)this.meetingDuration || this.ForceShowing24Hours)
			{
				this.show24Hours = true;
			}
			this.recipientWell = new CalendarItemRecipientWell();
		}

		// Token: 0x0600272E RID: 10030 RVA: 0x000DF498 File Offset: 0x000DD698
		protected void RenderDatePicker()
		{
			DatePicker datePicker = new DatePicker("dpSchd", this.WorkingHours, new ExDateTime[]
			{
				this.selectedDate
			});
			datePicker.Render(base.Response.Output);
		}

		// Token: 0x0600272F RID: 10031 RVA: 0x000DF4E1 File Offset: 0x000DD6E1
		protected void RenderDurationDropdown()
		{
			DurationDropDownList.RenderDurationPicker(base.Response.Output, this.meetingDuration, "divSchedulingDur");
		}

		// Token: 0x06002730 RID: 10032 RVA: 0x000DF4FE File Offset: 0x000DD6FE
		protected void RenderGridDayNames()
		{
			SchedulingTabRenderingUtilities.RenderGridDayNames(base.Response.Output, this.startDateTime, this.endDateTime);
		}

		// Token: 0x06002731 RID: 10033 RVA: 0x000DF51C File Offset: 0x000DD71C
		protected void RenderJavaScriptEncodedFolderId()
		{
			if (this.folderId != null)
			{
				Utilities.JavascriptEncode(this.folderId.ToBase64String(), base.Response.Output);
			}
		}

		// Token: 0x04001B6A RID: 7018
		private ExDateTime selectedDate;

		// Token: 0x04001B6B RID: 7019
		private ExDateTime startDateTime;

		// Token: 0x04001B6C RID: 7020
		private ExDateTime endDateTime;

		// Token: 0x04001B6D RID: 7021
		private bool show24Hours;

		// Token: 0x04001B6E RID: 7022
		private int meetingDuration = 30;

		// Token: 0x04001B6F RID: 7023
		private CalendarItemRecipientWell recipientWell;

		// Token: 0x04001B70 RID: 7024
		private OwaStoreObjectId folderId;

		// Token: 0x04001B71 RID: 7025
		private WorkingHours workingHours;

		// Token: 0x04001B72 RID: 7026
		private IExchangePrincipal organizerExchangePrincipal;
	}
}
