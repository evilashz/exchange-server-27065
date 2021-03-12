using System;
using System.Collections.Generic;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Clients.Owa.Premium.Controls;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x02000465 RID: 1125
	public class PublishedCalendarView : OwaSubPage, IRegistryOnlyForm, IPublishedView
	{
		// Token: 0x17000C65 RID: 3173
		// (get) Token: 0x06002A35 RID: 10805 RVA: 0x000EC786 File Offset: 0x000EA986
		public override IEnumerable<string> ExternalScriptFiles
		{
			get
			{
				return this.externalScriptFiles;
			}
		}

		// Token: 0x17000C66 RID: 3174
		// (get) Token: 0x06002A36 RID: 10806 RVA: 0x000EC78E File Offset: 0x000EA98E
		public override SanitizedHtmlString Title
		{
			get
			{
				return new SanitizedHtmlString(this.calendarAdapter.CalendarTitle);
			}
		}

		// Token: 0x17000C67 RID: 3175
		// (get) Token: 0x06002A37 RID: 10807 RVA: 0x000EC7A0 File Offset: 0x000EA9A0
		public override string PageType
		{
			get
			{
				return "CalendarViewPage";
			}
		}

		// Token: 0x06002A38 RID: 10808 RVA: 0x000EC7A8 File Offset: 0x000EA9A8
		protected override void OnLoad(EventArgs e)
		{
			ExTraceGlobals.CalendarCallTracer.TraceDebug(0L, "PublishedCalendarView.OnLoad");
			AnonymousSessionContext anonymousSessionContext = base.SessionContext as AnonymousSessionContext;
			if (anonymousSessionContext == null)
			{
				throw new OwaInvalidRequestException("This request can only be sent to Calendar VDir");
			}
			this.calendarAdapter = new PublishedCalendarAdapter(anonymousSessionContext);
			this.calendarAdapter.LoadData(CalendarUtilities.QueryProperties, null, CalendarViewType.Monthly);
			if (!this.calendarAdapter.UserCanReadItem)
			{
				throw new OwaInvalidRequestException("The calendar you requested is not existing or not published.");
			}
			this.monthlyView = new MonthlyView(anonymousSessionContext, this.calendarAdapter);
			base.OnLoad(e);
		}

		// Token: 0x06002A39 RID: 10809 RVA: 0x000EC82F File Offset: 0x000EAA2F
		protected override void OnUnload(EventArgs e)
		{
			if (this.calendarAdapter != null)
			{
				this.calendarAdapter.Dispose();
				this.calendarAdapter = null;
			}
		}

		// Token: 0x06002A3A RID: 10810 RVA: 0x000EC84C File Offset: 0x000EAA4C
		protected void RenderToolbar()
		{
			Toolbar toolbar = new PublishedCalendarViewToolbar();
			Toolbar toolbar2 = new CalendarTimeZoneToolbar();
			Toolbar toolbar3 = new MultipartToolbar(new MultipartToolbar.ToolbarInfo[]
			{
				new MultipartToolbar.ToolbarInfo(toolbar, "divCalendarViewToolbar"),
				new MultipartToolbar.ToolbarInfo(toolbar2, "divTimeZoneToolbar")
			});
			toolbar3.Render(base.SanitizingResponse);
		}

		// Token: 0x06002A3B RID: 10811 RVA: 0x000EC89C File Offset: 0x000EAA9C
		protected void RenderView()
		{
			base.SanitizingResponse.Write("<div id=divLVHide></div>");
			base.SanitizingResponse.Write("<div id=\"divCalendarView\">");
			new Infobar().Render(base.SanitizingResponse);
			base.SanitizingResponse.Write("<div id=\"divCalendarToolbar\">");
			this.RenderToolbar();
			base.SanitizingResponse.Write("</div>");
			using (CalendarAdapterBase calendarAdapterBase = new PublishedCalendarAdapter((AnonymousSessionContext)base.SessionContext))
			{
				DailyView dailyView = new DailyView(base.SessionContext, calendarAdapterBase);
				dailyView.RenderHeadersAndEventArea(base.SanitizingResponse, false);
				base.SanitizingResponse.Write("<div id=\"divDV\" style=\"display:none;\">");
				base.SanitizingResponse.Write("<div id=\"divTS\">");
				dailyView.RenderTimeStrip(base.SanitizingResponse);
				base.SanitizingResponse.Write("</div>");
				dailyView.RenderSchedulingArea(base.SanitizingResponse);
				base.SanitizingResponse.Write("</div>");
				this.monthlyView.RenderView(base.SanitizingResponse, true);
				base.SanitizingResponse.Write("</div>");
				base.SanitizingResponse.Write("<div id=\"divSubjectFH\" class=\"visSbj\">MM</div>");
				base.SanitizingResponse.Write("<div id=\"divLocationFH\" class=\"visLn\">MM</div>");
			}
		}

		// Token: 0x06002A3C RID: 10812 RVA: 0x000EC9E0 File Offset: 0x000EABE0
		protected void RenderReadingPane()
		{
			base.SanitizingResponse.Write("<iframe allowtransparency id=\"ifCalRP\" frameborder=\"0\" src=\"");
			base.SanitizingResponse.Write(base.SessionContext.GetBlankPage());
			base.SanitizingResponse.Write("\" style=\"display:none\"></iframe>");
		}

		// Token: 0x06002A3D RID: 10813 RVA: 0x000ECA18 File Offset: 0x000EAC18
		protected void RenderViewPayload()
		{
			base.SanitizingResponse.Write("function (){ return ");
			CalendarViewPayloadWriter calendarViewPayloadWriter = new MonthlyViewPayloadWriter(base.SessionContext, base.SanitizingResponse, this.monthlyView);
			calendarViewPayloadWriter.Render(0, CalendarViewType.Monthly, ReadingPanePosition.Off, ReadingPanePosition.Off);
			base.SanitizingResponse.Write(";}");
		}

		// Token: 0x06002A3E RID: 10814 RVA: 0x000ECA67 File Offset: 0x000EAC67
		protected void RenderHtmlEncodedFolderName()
		{
			base.SanitizingResponse.Write(this.calendarAdapter.CalendarTitle);
		}

		// Token: 0x06002A3F RID: 10815 RVA: 0x000ECA7F File Offset: 0x000EAC7F
		protected string GetJavascriptString(Strings.IDs id)
		{
			return LocalizedStrings.GetJavascriptEncoded(id);
		}

		// Token: 0x17000C68 RID: 3176
		// (get) Token: 0x06002A40 RID: 10816 RVA: 0x000ECA87 File Offset: 0x000EAC87
		protected string TimeZone
		{
			get
			{
				return base.SessionContext.TimeZone.Id;
			}
		}

		// Token: 0x17000C69 RID: 3177
		// (get) Token: 0x06002A41 RID: 10817 RVA: 0x000ECA99 File Offset: 0x000EAC99
		public string DisplayName
		{
			get
			{
				return this.calendarAdapter.CalendarTitle;
			}
		}

		// Token: 0x17000C6A RID: 3178
		// (get) Token: 0x06002A42 RID: 10818 RVA: 0x000ECAA6 File Offset: 0x000EACA6
		public string PublisherDisplayName
		{
			get
			{
				return this.calendarAdapter.CalendarOwnerDisplayName;
			}
		}

		// Token: 0x17000C6B RID: 3179
		// (get) Token: 0x06002A43 RID: 10819 RVA: 0x000ECAB3 File Offset: 0x000EACB3
		public string ICalUrl
		{
			get
			{
				return this.calendarAdapter.ICalUrl;
			}
		}

		// Token: 0x17000C6C RID: 3180
		// (get) Token: 0x06002A44 RID: 10820 RVA: 0x000ECAC0 File Offset: 0x000EACC0
		public SanitizedHtmlString PublishTimeRange
		{
			get
			{
				return SanitizedHtmlString.Format(LocalizedStrings.GetNonEncoded(-1428371010), new object[]
				{
					this.calendarAdapter.PublishedFromDateTime.ToShortDateString(),
					this.calendarAdapter.PublishedToDateTime.ToShortDateString()
				});
			}
		}

		// Token: 0x04001C8C RID: 7308
		private PublishedCalendarAdapter calendarAdapter;

		// Token: 0x04001C8D RID: 7309
		private MonthlyView monthlyView;

		// Token: 0x04001C8E RID: 7310
		private string[] externalScriptFiles = new string[]
		{
			"cdayvw.js"
		};
	}
}
