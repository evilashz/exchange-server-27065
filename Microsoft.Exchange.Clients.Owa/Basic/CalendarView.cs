using System;
using System.Web;
using Microsoft.Exchange.Clients.Owa.Basic.Controls;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Clients.Owa.Basic
{
	// Token: 0x02000094 RID: 148
	public class CalendarView : OwaForm
	{
		// Token: 0x1700014B RID: 331
		// (get) Token: 0x0600047A RID: 1146 RVA: 0x0002691F File Offset: 0x00024B1F
		protected int SelectedYear
		{
			get
			{
				return this.days[0].Year;
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x0600047B RID: 1147 RVA: 0x00026932 File Offset: 0x00024B32
		protected int SelectedMonth
		{
			get
			{
				return this.days[0].Month;
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x0600047C RID: 1148 RVA: 0x00026945 File Offset: 0x00024B45
		protected int SelectedDay
		{
			get
			{
				return this.days[0].Day;
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x0600047D RID: 1149 RVA: 0x00026958 File Offset: 0x00024B58
		protected string FolderName
		{
			get
			{
				return this.calendarAdapter.CalendarTitle;
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x0600047E RID: 1150 RVA: 0x00026965 File Offset: 0x00024B65
		protected string UrlEncodedFolderId
		{
			get
			{
				return HttpUtility.UrlEncode(this.folderId.ToBase64String());
			}
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x00026978 File Offset: 0x00024B78
		protected override void OnLoad(EventArgs e)
		{
			ExTraceGlobals.CalendarCallTracer.TraceDebug((long)this.GetHashCode(), "Basic.CalendarView.OnLoad");
			EditCalendarItemHelper.ClearUserContextData(base.UserContext);
			this.folderId = QueryStringUtilities.CreateFolderStoreObjectId(base.UserContext.MailboxSession, base.Request, false);
			if (this.folderId == null)
			{
				ExTraceGlobals.CalendarTracer.TraceDebug((long)this.GetHashCode(), "folderId is null, using default folder");
				this.folderId = base.UserContext.CalendarFolderId;
			}
			if (this.folderId == null)
			{
				throw new OwaInvalidRequestException("Invalid folder id");
			}
			StorePropertyDefinition displayName = StoreObjectSchema.DisplayName;
			PropertyDefinition calendarViewType = ViewStateProperties.CalendarViewType;
			PropertyDefinition readingPanePosition = ViewStateProperties.ReadingPanePosition;
			PropertyDefinition readingPanePositionMultiDay = ViewStateProperties.ReadingPanePositionMultiDay;
			PropertyDefinition viewWidth = ViewStateProperties.ViewWidth;
			PropertyDefinition dailyViewDays = ViewStateProperties.DailyViewDays;
			this.viewType = CalendarViewType.Min;
			string queryStringParameter = Utilities.GetQueryStringParameter(base.Request, "dy", false);
			string queryStringParameter2 = Utilities.GetQueryStringParameter(base.Request, "mn", false);
			string queryStringParameter3 = Utilities.GetQueryStringParameter(base.Request, "yr", false);
			int day;
			int month;
			int year;
			if (!string.IsNullOrEmpty(queryStringParameter) && int.TryParse(queryStringParameter, out day) && !string.IsNullOrEmpty(queryStringParameter2) && int.TryParse(queryStringParameter2, out month) && !string.IsNullOrEmpty(queryStringParameter3) && int.TryParse(queryStringParameter3, out year))
			{
				try
				{
					ExDateTime exDateTime = new ExDateTime(base.UserContext.TimeZone, year, month, day);
					this.days = new ExDateTime[1];
					this.days[0] = exDateTime.Date;
				}
				catch (ArgumentOutOfRangeException)
				{
					base.Infobar.AddMessageLocalized(883484089, InfobarMessageType.Error);
				}
			}
			if (this.days == null)
			{
				this.days = new ExDateTime[1];
				this.days[0] = DateTimeUtilities.GetLocalTime().Date;
			}
			this.calendarAdapter = new CalendarAdapter(base.UserContext, this.folderId);
			this.calendarAdapter.LoadData(DailyView.QueryProperties, this.days, false, true);
			this.dailyView = new DailyView(base.UserContext, this.calendarAdapter);
			base.OnLoad(e);
			if (base.IsPostFromMyself())
			{
				string formParameter = Utilities.GetFormParameter(base.Request, "hidcmdpst", false);
				if (string.CompareOrdinal(formParameter, "addjnkeml") == 0)
				{
					if (!base.UserContext.IsJunkEmailEnabled)
					{
						throw new OwaInvalidRequestException(LocalizedStrings.GetNonEncoded(552277155));
					}
					InfobarMessage infobarMessage = JunkEmailHelper.AddEmailToSendersList(base.UserContext, base.Request);
					if (infobarMessage != null)
					{
						base.Infobar.AddMessage(infobarMessage);
					}
				}
			}
			base.UserContext.LastClientViewState = new CalendarModuleViewState(this.folderId, this.calendarAdapter.ClassName, this.days[0]);
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x00026C1C File Offset: 0x00024E1C
		protected override void OnUnload(EventArgs e)
		{
			if (this.calendarAdapter != null)
			{
				this.calendarAdapter.Dispose();
				this.calendarAdapter = null;
			}
			base.OnUnload(e);
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x00026C40 File Offset: 0x00024E40
		public void RenderCalendarView()
		{
			CalendarViewType calendarViewType = this.viewType;
			if (calendarViewType != CalendarViewType.Min)
			{
				return;
			}
			this.RenderDailyView();
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x00026C5F File Offset: 0x00024E5F
		private void RenderDailyView()
		{
			this.dailyView.Render(base.Response.Output);
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x00026C78 File Offset: 0x00024E78
		public void RenderNavigation()
		{
			Navigation navigation = new Navigation(NavigationModule.Calendar, base.OwaContext, base.Response.Output);
			navigation.Render();
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x00026CA4 File Offset: 0x00024EA4
		public void RenderCalendarSecondaryNavigation()
		{
			CalendarSecondaryNavigation calendarSecondaryNavigation = new CalendarSecondaryNavigation(base.OwaContext, this.folderId, new ExDateTime?(this.days[0]), null);
			string text = calendarSecondaryNavigation.Render(base.Response.Output);
			if (!string.IsNullOrEmpty(text))
			{
				base.Infobar.AddMessageText(text, InfobarMessageType.Error);
			}
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x00026D04 File Offset: 0x00024F04
		protected override void RenderOptions(string helpFile)
		{
			OptionsBar optionsBar = new OptionsBar(base.UserContext, base.Response.Output, OptionsBar.SearchModule.Calendar, OptionsBar.RenderingFlags.RenderCalendarOptionsLink, null);
			optionsBar.Render(helpFile);
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x00026D34 File Offset: 0x00024F34
		public void RenderCalendarViewHeaderToolbar()
		{
			Toolbar toolbar = new Toolbar(base.Response.Output, true);
			toolbar.RenderStart();
			toolbar.RenderButton(ToolbarButtons.NewAppointment);
			toolbar.RenderSpace();
			toolbar.RenderButton(ToolbarButtons.NewMeetingRequest);
			toolbar.RenderDivider();
			toolbar.RenderButton(ToolbarButtons.Today);
			toolbar.RenderFill();
			toolbar.RenderEnd();
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x00026D94 File Offset: 0x00024F94
		public void RenderCalendarViewFooterToolbar()
		{
			Toolbar toolbar = new Toolbar(base.Response.Output, false);
			toolbar.RenderStart();
			toolbar.RenderFill();
			toolbar.RenderEnd();
		}

		// Token: 0x040003AD RID: 941
		internal const string YearQueryParameter = "yr";

		// Token: 0x040003AE RID: 942
		internal const string MonthQueryParameter = "mn";

		// Token: 0x040003AF RID: 943
		internal const string DayQueryParameter = "dy";

		// Token: 0x040003B0 RID: 944
		private const string CommandPostParameter = "hidcmdpst";

		// Token: 0x040003B1 RID: 945
		private const string AddJunkEmailCommand = "addjnkeml";

		// Token: 0x040003B2 RID: 946
		private CalendarViewType viewType = CalendarViewType.Min;

		// Token: 0x040003B3 RID: 947
		private CalendarAdapter calendarAdapter;

		// Token: 0x040003B4 RID: 948
		private StoreObjectId folderId;

		// Token: 0x040003B5 RID: 949
		private DailyView dailyView;

		// Token: 0x040003B6 RID: 950
		private ExDateTime[] days;
	}
}
