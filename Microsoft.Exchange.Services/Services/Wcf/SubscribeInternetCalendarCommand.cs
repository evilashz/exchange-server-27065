using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000975 RID: 2421
	internal sealed class SubscribeInternetCalendarCommand : ServiceCommand<CalendarActionFolderIdResponse>
	{
		// Token: 0x0600457F RID: 17791 RVA: 0x000F4040 File Offset: 0x000F2240
		public SubscribeInternetCalendarCommand(CallContext callContext, SubscribeInternetCalendarRequest request) : base(callContext)
		{
			this.request = request;
			this.request.ValidateRequest();
		}

		// Token: 0x06004580 RID: 17792 RVA: 0x000F405C File Offset: 0x000F225C
		protected override CalendarActionFolderIdResponse InternalExecute()
		{
			CalendarActionFolderIdResponse result;
			try
			{
				result = new SubscribeInternetCalendar(base.CallContext.SessionCache.GetMailboxIdentityMailboxSession(), this.request.ICalUrl, this.request.GroupId, this.request.CalendarName).Execute();
			}
			catch (InvalidSharingDataException ex)
			{
				SubscribeInternetCalendarCommand.TraceError(this, ex, "Bad ICal Url format");
				result = new CalendarActionFolderIdResponse(CalendarActionError.CalendarActionInvalidUrlFormat);
			}
			catch (NotSupportedWithMailboxVersionException ex2)
			{
				SubscribeInternetCalendarCommand.TraceError(this, ex2, "Your account isn't set up to allow calendars to be added from the Internet.");
				result = new CalendarActionFolderIdResponse(CalendarActionError.CalendarActionUnableToSubscribeToCalendar);
			}
			return result;
		}

		// Token: 0x06004581 RID: 17793 RVA: 0x000F40F4 File Offset: 0x000F22F4
		public static void TraceError(object source, string message)
		{
			SubscribeInternetCalendarCommand.TraceError(source, null, message);
		}

		// Token: 0x06004582 RID: 17794 RVA: 0x000F40FE File Offset: 0x000F22FE
		public static void TraceError(object source, Exception ex, string message)
		{
			if (ex == null)
			{
				ExTraceGlobals.SubscribeInternetCalendarCallTracer.TraceError((long)source.GetHashCode(), message);
				return;
			}
			ExTraceGlobals.SubscribeInternetCalendarCallTracer.TraceError<string, string>((long)source.GetHashCode(), message, ex.Message, ex.StackTrace);
		}

		// Token: 0x06004583 RID: 17795 RVA: 0x000F4134 File Offset: 0x000F2334
		public static void TraceDebug(object source, string message)
		{
			ExTraceGlobals.SubscribeInternetCalendarCallTracer.TraceDebug((long)source.GetHashCode(), message);
		}

		// Token: 0x0400286C RID: 10348
		private readonly SubscribeInternetCalendarRequest request;
	}
}
