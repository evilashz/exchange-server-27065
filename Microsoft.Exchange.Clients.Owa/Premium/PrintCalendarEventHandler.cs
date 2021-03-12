using System;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Clients.Owa.Premium.Controls;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x020004CC RID: 1228
	[OwaEventNamespace("PrintCalendar")]
	[OwaEventSegmentation(Feature.Calendar)]
	internal sealed class PrintCalendarEventHandler : OwaEventHandlerBase
	{
		// Token: 0x06002EE6 RID: 12006 RVA: 0x0010DC29 File Offset: 0x0010BE29
		public static void Register()
		{
			OwaEventRegistry.RegisterHandler(typeof(PrintCalendarEventHandler));
		}

		// Token: 0x06002EE7 RID: 12007 RVA: 0x0010DC3C File Offset: 0x0010BE3C
		[OwaEventParameter("fId", typeof(OwaStoreObjectId))]
		[OwaEvent("GetEventList")]
		[OwaEventParameter("days", typeof(ExDateTime), true)]
		[OwaEventParameter("vt", typeof(CalendarViewType))]
		[OwaEventParameter("SH", typeof(int))]
		[OwaEventParameter("EH", typeof(int))]
		[OwaEventParameter("wo", typeof(bool), false, true)]
		public void GetEventList()
		{
			OwaStoreObjectId folderId = (OwaStoreObjectId)base.GetParameter("fId");
			ExDateTime[] days = (ExDateTime[])base.GetParameter("days");
			CalendarViewType viewType = (CalendarViewType)base.GetParameter("vt");
			int startHour = (int)base.GetParameter("SH");
			int endHour = (int)base.GetParameter("EH");
			bool workingDayOnly = false;
			if (base.IsParameterSet("wo"))
			{
				workingDayOnly = (bool)base.GetParameter("wo");
			}
			using (CalendarAdapter calendarAdapter = new CalendarAdapter(base.UserContext, folderId))
			{
				int num;
				ReadingPanePosition readingPanePosition;
				calendarAdapter.LoadData(CalendarUtilities.PrintQueryProperties, days, false, startHour, endHour, ref viewType, out num, out readingPanePosition);
				if (!calendarAdapter.UserCanReadItem)
				{
					throw new OwaInvalidRequestException("no read access to the calendar");
				}
				PrintEventList printEventList = new PrintEventList(base.UserContext, calendarAdapter, viewType, workingDayOnly);
				printEventList.RenderView(this.SanitizingWriter);
			}
		}

		// Token: 0x06002EE8 RID: 12008 RVA: 0x0010DD38 File Offset: 0x0010BF38
		[OwaEvent("GetEventListForPublishedCalendar", false, true)]
		[OwaEventParameter("days", typeof(ExDateTime), true)]
		[OwaEventParameter("vt", typeof(CalendarViewType))]
		[OwaEventParameter("SH", typeof(int))]
		[OwaEventParameter("EH", typeof(int))]
		public void GetEventListForPublishedCalendar()
		{
			ExDateTime[] days = (ExDateTime[])base.GetParameter("days");
			CalendarViewType viewType = (CalendarViewType)base.GetParameter("vt");
			int startHour = (int)base.GetParameter("SH");
			int endHour = (int)base.GetParameter("EH");
			AnonymousSessionContext anonymousSessionContext = base.SessionContext as AnonymousSessionContext;
			if (anonymousSessionContext == null)
			{
				throw new OwaInvalidRequestException("This request can only be sent to Calendar VDir");
			}
			using (PublishedCalendarAdapter publishedCalendarAdapter = new PublishedCalendarAdapter(anonymousSessionContext))
			{
				publishedCalendarAdapter.LoadData(CalendarUtilities.QueryProperties, days, startHour, endHour, viewType);
				if (!publishedCalendarAdapter.UserCanReadItem)
				{
					throw new OwaInvalidRequestException("no read access to the calendar");
				}
				PrintEventList printEventList = new PrintEventList(base.SessionContext, publishedCalendarAdapter, viewType, false);
				printEventList.RenderView(this.SanitizingWriter);
			}
		}

		// Token: 0x040020C4 RID: 8388
		public const string EventNamespace = "PrintCalendar";

		// Token: 0x040020C5 RID: 8389
		public const string MethodGetEventList = "GetEventList";

		// Token: 0x040020C6 RID: 8390
		public const string MethodGetEventListForPublishedCalendar = "GetEventListForPublishedCalendar";

		// Token: 0x040020C7 RID: 8391
		public const string FolderId = "fId";

		// Token: 0x040020C8 RID: 8392
		public const string Days = "days";

		// Token: 0x040020C9 RID: 8393
		public const string ViewType = "vt";

		// Token: 0x040020CA RID: 8394
		public const string StartHour = "SH";

		// Token: 0x040020CB RID: 8395
		public const string EndHour = "EH";

		// Token: 0x040020CC RID: 8396
		public const string WorkingDayOnly = "wo";
	}
}
