using System;
using System.Diagnostics;
using Microsoft.Exchange.Clients.Common;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Clients.Owa.Premium.Controls;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Security;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x02000441 RID: 1089
	public class CalendarView : OwaPage, IRegistryOnlyForm
	{
		// Token: 0x17000AF6 RID: 2806
		// (get) Token: 0x0600273E RID: 10046 RVA: 0x000DFB5F File Offset: 0x000DDD5F
		// (set) Token: 0x0600273F RID: 10047 RVA: 0x000DFB67 File Offset: 0x000DDD67
		private protected Infobar Infobar { protected get; private set; }

		// Token: 0x06002740 RID: 10048 RVA: 0x000DFB70 File Offset: 0x000DDD70
		protected override void OnLoad(EventArgs e)
		{
			ExTraceGlobals.CalendarCallTracer.TraceDebug(0L, "CalendarView.OnLoad");
			this.Infobar = new Infobar();
			this.readingPaneMarkupBegin = SanitizedHtmlString.Format("{0}{1}{2}", new object[]
			{
				"<iframe allowtransparency id=\"ifCalRP\" frameborder=\"0\" src=\"",
				base.UserContext.GetBlankPage(),
				"\""
			});
			string queryStringParameter = Utilities.GetQueryStringParameter(base.Request, "id", false);
			OwaStoreObjectId owaStoreObjectId;
			if (queryStringParameter != null)
			{
				owaStoreObjectId = OwaStoreObjectId.CreateFromString(queryStringParameter);
			}
			else
			{
				ExTraceGlobals.CalendarTracer.TraceDebug(0L, "folder Id is null, using default folder");
				owaStoreObjectId = base.UserContext.CalendarFolderOwaId;
			}
			if (owaStoreObjectId == null)
			{
				throw new OwaInvalidRequestException("Invalid folder id");
			}
			string queryStringParameter2 = Utilities.GetQueryStringParameter(base.Request, "d", false);
			ExDateTime[] days = null;
			if (queryStringParameter2 != null)
			{
				try
				{
					days = new ExDateTime[]
					{
						DateTimeUtilities.ParseIsoDate(queryStringParameter2, base.UserContext.TimeZone).Date
					};
				}
				catch (OwaParsingErrorException)
				{
					ExTraceGlobals.CalendarTracer.TraceDebug<string>(0L, "Invalid date provided on URL '{0}'", queryStringParameter2);
					throw new OwaInvalidRequestException("Invalid date on URL");
				}
			}
			if (base.UserContext.IsWebPartRequest)
			{
				string text = Utilities.GetQueryStringParameter(base.Request, "view", false);
				if (string.IsNullOrEmpty(text))
				{
					text = WebPartUtilities.GetDefaultView("IPF.Appointment");
				}
				if (string.Equals(text, "daily", StringComparison.OrdinalIgnoreCase))
				{
					this.viewType = CalendarViewType.Min;
				}
				else if (string.CompareOrdinal(text, "monthly") == 0)
				{
					this.viewType = CalendarViewType.Monthly;
				}
				else
				{
					this.viewType = CalendarViewType.Weekly;
				}
			}
			this.calendarAdapter = new CalendarAdapter(base.UserContext, owaStoreObjectId);
			this.calendarAdapter.LoadData(CalendarUtilities.QueryProperties, days, true, ref this.viewType, out this.viewWidth, out this.readingPanePosition);
			if (!this.calendarAdapter.UserCanReadItem)
			{
				return;
			}
			owaStoreObjectId = this.calendarAdapter.FolderId;
			using (CalendarAdapter calendarAdapter = new CalendarAdapter(base.UserContext, owaStoreObjectId))
			{
				if (this.viewType == CalendarViewType.Monthly)
				{
					this.contentView = new CalendarView.ContentView(new DailyView(base.UserContext, calendarAdapter), new MonthlyView(base.UserContext, this.calendarAdapter), false);
				}
				else
				{
					this.contentView = new CalendarView.ContentView(new DailyView(base.UserContext, this.calendarAdapter), new MonthlyView(base.UserContext, calendarAdapter), true);
				}
			}
			if (this.viewType != CalendarViewType.Monthly)
			{
				string queryStringParameter3 = Utilities.GetQueryStringParameter(base.Request, "srp", false);
				int num;
				if (!string.IsNullOrEmpty(queryStringParameter3) && int.TryParse(queryStringParameter3, out num))
				{
					this.readingPanePosition = ((num != 0) ? ReadingPanePosition.Right : ReadingPanePosition.Off);
					this.requestReadingPanePosition = this.readingPanePosition;
				}
			}
			else
			{
				this.readingPanePosition = ReadingPanePosition.Off;
			}
			string queryStringParameter4 = Utilities.GetQueryStringParameter(base.Request, "sid", false);
			if (queryStringParameter4 != null)
			{
				OwaStoreObjectId selectedItemId = OwaStoreObjectId.CreateFromString(queryStringParameter4);
				this.contentView.MainView.SelectedItemId = selectedItemId;
			}
			OwaSingleCounters.CalendarViewsLoaded.Increment();
			base.OnLoad(e);
		}

		// Token: 0x17000AF7 RID: 2807
		// (get) Token: 0x06002741 RID: 10049 RVA: 0x000DFE74 File Offset: 0x000DE074
		protected string TooManyCalendarsErrorMessage
		{
			get
			{
				return string.Format(LocalizedStrings.GetNonEncoded(1943603574), this.MaximumCalendarCount);
			}
		}

		// Token: 0x17000AF8 RID: 2808
		// (get) Token: 0x06002742 RID: 10050 RVA: 0x000DFE90 File Offset: 0x000DE090
		protected int MaximumCalendarCount
		{
			get
			{
				return 5;
			}
		}

		// Token: 0x06002743 RID: 10051 RVA: 0x000DFE93 File Offset: 0x000DE093
		protected override void OnUnload(EventArgs e)
		{
			if (this.calendarAdapter != null)
			{
				this.calendarAdapter.Dispose();
				this.calendarAdapter = null;
			}
		}

		// Token: 0x17000AF9 RID: 2809
		// (get) Token: 0x06002744 RID: 10052 RVA: 0x000DFEAF File Offset: 0x000DE0AF
		protected ReadingPanePosition ReadingPanePosition
		{
			get
			{
				return this.readingPanePosition;
			}
		}

		// Token: 0x17000AFA RID: 2810
		// (get) Token: 0x06002745 RID: 10053 RVA: 0x000DFEB7 File Offset: 0x000DE0B7
		protected int ViewWidth
		{
			get
			{
				return this.viewWidth;
			}
		}

		// Token: 0x17000AFB RID: 2811
		// (get) Token: 0x06002746 RID: 10054 RVA: 0x000DFEBF File Offset: 0x000DE0BF
		protected int ViewTypeDaily
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000AFC RID: 2812
		// (get) Token: 0x06002747 RID: 10055 RVA: 0x000DFEC2 File Offset: 0x000DE0C2
		protected int DailyViewTimeStripMode
		{
			get
			{
				return (int)this.contentView.Daily.TimeStripMode;
			}
		}

		// Token: 0x17000AFD RID: 2813
		// (get) Token: 0x06002748 RID: 10056 RVA: 0x000DFED4 File Offset: 0x000DE0D4
		protected bool CanCreateItem
		{
			get
			{
				return this.calendarAdapter.DataSource != null && this.calendarAdapter.DataSource.UserCanCreateItem;
			}
		}

		// Token: 0x17000AFE RID: 2814
		// (get) Token: 0x06002749 RID: 10057 RVA: 0x000DFEF5 File Offset: 0x000DE0F5
		protected bool IsSecondaryCalendarFromOlderExchange
		{
			get
			{
				return this.calendarAdapter.OlderExchangeSharedCalendarType == NavigationNodeFolder.OlderExchangeCalendarType.Secondary;
			}
		}

		// Token: 0x17000AFF RID: 2815
		// (get) Token: 0x0600274A RID: 10058 RVA: 0x000DFF05 File Offset: 0x000DE105
		protected bool IsPublicFolder
		{
			get
			{
				return this.calendarAdapter.IsPublic;
			}
		}

		// Token: 0x0600274B RID: 10059 RVA: 0x000DFF14 File Offset: 0x000DE114
		protected void RenderToolbar(ReadingPanePosition readingPanePosition)
		{
			if (this.readingPanePosition != readingPanePosition)
			{
				return;
			}
			SanitizedHtmlString folderInfo = null;
			if (base.UserContext.IsWebPartRequest & this.UserHasRightToLoad)
			{
				folderInfo = this.GetFolderDateAndProgressSpanMarkup();
			}
			CalendarViewToolbar calendarViewToolbar = new CalendarViewToolbar(this.UserHasRightToLoad ? this.viewType : CalendarViewType.Min, this.IsPublicFolder, this.CanCreateItem, this.UserHasRightToLoad, base.UserContext.IsWebPartRequest, readingPanePosition, folderInfo);
			base.SanitizingResponse.Write("<div id=\"divCalendarViewToolbar\">");
			calendarViewToolbar.Render(base.SanitizingResponse);
			base.SanitizingResponse.Write("</div>");
		}

		// Token: 0x17000B00 RID: 2816
		// (get) Token: 0x0600274C RID: 10060 RVA: 0x000DFFAC File Offset: 0x000DE1AC
		protected bool UserHasRightToLoad
		{
			get
			{
				return this.calendarAdapter.UserCanReadItem;
			}
		}

		// Token: 0x17000B01 RID: 2817
		// (get) Token: 0x0600274D RID: 10061 RVA: 0x000DFFBC File Offset: 0x000DE1BC
		protected bool CanPublish
		{
			get
			{
				bool result = false;
				SharingPolicy sharingPolicy = DirectoryHelper.ReadSharingPolicy(base.UserContext.MailboxSession.MailboxOwner.MailboxInfo.MailboxGuid, base.UserContext.MailboxSession.MailboxOwner.MailboxInfo.IsArchive, base.UserContext.MailboxSession.GetADRecipientSession(true, ConsistencyMode.IgnoreInvalid));
				if (sharingPolicy != null && sharingPolicy.IsAllowedForAnonymousCalendarSharing())
				{
					result = true;
				}
				return result;
			}
		}

		// Token: 0x17000B02 RID: 2818
		// (get) Token: 0x0600274E RID: 10062 RVA: 0x000E0025 File Offset: 0x000DE225
		protected bool CanSubscribe
		{
			get
			{
				return CalendarUtilities.CanSubscribeInternetCalendar();
			}
		}

		// Token: 0x0600274F RID: 10063 RVA: 0x000E002C File Offset: 0x000DE22C
		protected void RenderView(ReadingPanePosition readingPanePosition)
		{
			if (this.readingPanePosition != readingPanePosition)
			{
				return;
			}
			base.SanitizingResponse.Write("<div id=\"divLVHide\"></div>");
			base.SanitizingResponse.Write("<div id=\"divYearMonthBar\"></div>");
			base.SanitizingResponse.Write("<div id=\"divCalendarView\">");
			base.SanitizingResponse.Write("<div id=\"divCalendarToolbar\">");
			this.RenderToolbar(this.ReadingPanePosition);
			base.SanitizingResponse.Write("</div>");
			this.Infobar.Render(base.SanitizingResponse);
			this.contentView.Daily.RenderHeadersAndEventArea(base.SanitizingResponse, this.viewType != CalendarViewType.Monthly);
			base.SanitizingResponse.Write("<div id=\"divDV\"");
			if (this.viewType == CalendarViewType.Monthly)
			{
				base.SanitizingResponse.Write(" style=\"display:none;\"");
			}
			base.SanitizingResponse.Write(">");
			base.SanitizingResponse.Write("<div id=\"divTS\">");
			this.contentView.Daily.RenderTimeStrip(base.SanitizingResponse);
			base.SanitizingResponse.Write("</div>");
			this.contentView.Daily.RenderSchedulingArea(base.SanitizingResponse);
			base.SanitizingResponse.Write("</div>");
			this.contentView.Monthly.RenderView(base.SanitizingResponse, this.viewType == CalendarViewType.Monthly);
			base.SanitizingResponse.Write("</div>");
			base.SanitizingResponse.Write("<div id=\"divSubjectFH\" class=\"visSbj\">MM</div>");
			base.SanitizingResponse.Write("<div id=\"divLocationFH\" class=\"visLn\">MM</div>");
		}

		// Token: 0x06002750 RID: 10064 RVA: 0x000E01B8 File Offset: 0x000DE3B8
		protected void RenderReadingPane(ReadingPanePosition readingPanePosition)
		{
			if ((this.readingPanePosition == ReadingPanePosition.Off && readingPanePosition == ReadingPanePosition.Right) || this.readingPanePosition == readingPanePosition)
			{
				base.SanitizingResponse.Write(this.readingPaneMarkupBegin);
				if (this.contentView.MainView.Count == 0)
				{
					base.SanitizingResponse.Write(" style=\"display:none\"");
				}
				base.SanitizingResponse.Write("></iframe>");
			}
		}

		// Token: 0x06002751 RID: 10065 RVA: 0x000E0220 File Offset: 0x000DE420
		protected SanitizedHtmlString GetFolderDateAndProgressSpanMarkup()
		{
			SanitizingStringBuilder<OwaHtml> sanitizingStringBuilder = new SanitizingStringBuilder<OwaHtml>(256);
			sanitizingStringBuilder.Append("<span id=spnFn class=fn> ");
			sanitizingStringBuilder.Append(this.contentView.MainView.CalendarAdapter.CalendarTitle);
			sanitizingStringBuilder.Append("</span>");
			sanitizingStringBuilder.Append("<img id=imgPrg style=display:none src=\"");
			sanitizingStringBuilder.Append(base.UserContext.GetThemeFileUrl(ThemeFileId.ProgressSmall));
			sanitizingStringBuilder.Append("\"><span id=spnPrg class=fn style=display:none>\t&nbsp;");
			sanitizingStringBuilder.Append(LocalizedStrings.GetNonEncoded(-1961594409));
			sanitizingStringBuilder.Append("</span>");
			return sanitizingStringBuilder.ToSanitizedString<SanitizedHtmlString>();
		}

		// Token: 0x06002752 RID: 10066 RVA: 0x000E02B8 File Offset: 0x000DE4B8
		protected void RenderCalendarItemContextMenu()
		{
			CalendarItemContextMenu calendarItemContextMenu = new CalendarItemContextMenu(base.UserContext);
			calendarItemContextMenu.Render(base.SanitizingResponse);
		}

		// Token: 0x06002753 RID: 10067 RVA: 0x000E02E0 File Offset: 0x000DE4E0
		protected void RenderShareCalendarContextMenu()
		{
			ShareCalendarContextMenu shareCalendarContextMenu = new ShareCalendarContextMenu(base.UserContext);
			shareCalendarContextMenu.Render(base.SanitizingResponse);
		}

		// Token: 0x06002754 RID: 10068 RVA: 0x000E0308 File Offset: 0x000DE508
		protected void RenderDataPayload()
		{
			Stopwatch watch = Utilities.StartWatch();
			base.SanitizingResponse.Write("function a_initVwPld(){ return ");
			CalendarViewPayloadWriter calendarViewPayloadWriter;
			if (this.contentView.MainView is DailyView)
			{
				calendarViewPayloadWriter = new DailyViewPayloadWriter(base.UserContext, base.SanitizingResponse, this.contentView.Daily);
			}
			else
			{
				calendarViewPayloadWriter = new MonthlyViewPayloadWriter(base.UserContext, base.SanitizingResponse, this.contentView.Monthly);
			}
			calendarViewPayloadWriter.Render(this.viewWidth, this.viewType, this.readingPanePosition, this.requestReadingPanePosition);
			base.SanitizingResponse.Write(";}");
			Utilities.StopWatch(watch, "DailyView.RenderDataPayload");
		}

		// Token: 0x06002755 RID: 10069 RVA: 0x000E03B5 File Offset: 0x000DE5B5
		protected void RenderCancelRecurrenceMeetingDialog()
		{
			CalendarUtilities.RenderCancelRecurrenceMeetingDialog(base.SanitizingResponse, true);
		}

		// Token: 0x06002756 RID: 10070 RVA: 0x000E03C3 File Offset: 0x000DE5C3
		protected void RenderHtmlEncodedFolderName()
		{
			if (this.UserHasRightToLoad)
			{
				base.SanitizingResponse.Write(this.contentView.MainView.CalendarAdapter.CalendarTitle);
			}
		}

		// Token: 0x04001B7E RID: 7038
		private const string FolderIdQueryParameter = "id";

		// Token: 0x04001B7F RID: 7039
		private const string DayQueryParameter = "d";

		// Token: 0x04001B80 RID: 7040
		private const string ShowReadingPaneQueryParameter = "srp";

		// Token: 0x04001B81 RID: 7041
		private const string SelectedItemIdQueryParameter = "sid";

		// Token: 0x04001B82 RID: 7042
		private const int ViewTypeDailyValue = 1;

		// Token: 0x04001B83 RID: 7043
		private const string ColorQueryParameter = "clr";

		// Token: 0x04001B84 RID: 7044
		private SanitizedHtmlString readingPaneMarkupBegin;

		// Token: 0x04001B85 RID: 7045
		private CalendarAdapter calendarAdapter;

		// Token: 0x04001B86 RID: 7046
		private CalendarView.ContentView contentView;

		// Token: 0x04001B87 RID: 7047
		private int viewWidth = 450;

		// Token: 0x04001B88 RID: 7048
		private CalendarViewType viewType;

		// Token: 0x04001B89 RID: 7049
		private ReadingPanePosition readingPanePosition = ReadingPanePosition.Right;

		// Token: 0x04001B8A RID: 7050
		private ReadingPanePosition requestReadingPanePosition = ReadingPanePosition.Min;

		// Token: 0x02000442 RID: 1090
		private class ContentView
		{
			// Token: 0x06002758 RID: 10072 RVA: 0x000E040E File Offset: 0x000DE60E
			public ContentView(DailyView daily, MonthlyView monthly, bool isDailyMainView)
			{
				this.Daily = daily;
				this.Monthly = monthly;
				this.IsDailyMainView = isDailyMainView;
			}

			// Token: 0x17000B03 RID: 2819
			// (get) Token: 0x06002759 RID: 10073 RVA: 0x000E042B File Offset: 0x000DE62B
			public ICalendarViewControl MainView
			{
				get
				{
					if (this.IsDailyMainView)
					{
						return this.Daily;
					}
					return this.Monthly;
				}
			}

			// Token: 0x04001B8C RID: 7052
			public readonly bool IsDailyMainView;

			// Token: 0x04001B8D RID: 7053
			public readonly DailyView Daily;

			// Token: 0x04001B8E RID: 7054
			public readonly MonthlyView Monthly;
		}
	}
}
