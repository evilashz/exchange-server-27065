using System;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Clients.Owa.Premium.Controls;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.InfoWorker.Common.Availability;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x0200048E RID: 1166
	[OwaEventNamespace("DatePicker")]
	internal sealed class DatePickerEventHandler : OwaEventHandlerBase
	{
		// Token: 0x06002D1F RID: 11551 RVA: 0x000FD8E7 File Offset: 0x000FBAE7
		public static void Register()
		{
			OwaEventRegistry.RegisterEnum(typeof(DatePicker.Features));
			OwaEventRegistry.RegisterHandler(typeof(DatePickerEventHandler));
		}

		// Token: 0x06002D20 RID: 11552 RVA: 0x000FD908 File Offset: 0x000FBB08
		[OwaEventVerb(OwaEventVerb.Get)]
		[OwaEvent("RenderMonth", false, true)]
		[OwaEventParameter("uF", typeof(int), false, false)]
		[OwaEventParameter("m", typeof(ExDateTime))]
		public void RenderMonth()
		{
			ExTraceGlobals.CalendarCallTracer.TraceDebug((long)this.GetHashCode(), "DatePickerEventHandler.RenderMonth");
			int features = (int)base.GetParameter("uF");
			ExDateTime month = (ExDateTime)base.GetParameter("m");
			DatePicker datePicker = new DatePicker(string.Empty, month, features);
			datePicker.RenderMonth(this.Writer);
		}

		// Token: 0x06002D21 RID: 11553 RVA: 0x000FD968 File Offset: 0x000FBB68
		[OwaEventParameter("fId", typeof(OwaStoreObjectId), true, true)]
		[OwaEvent("GetFreeBusy")]
		[OwaEventParameter("m", typeof(ExDateTime), false, false)]
		public void GetFreeBusy()
		{
			ExTraceGlobals.CalendarCallTracer.TraceDebug((long)this.GetHashCode(), "DatePickerEventHandler.GetFreeBusy");
			ExDateTime month = (ExDateTime)base.GetParameter("m");
			OwaStoreObjectId[] array;
			if (!base.IsParameterSet("fId"))
			{
				array = new OwaStoreObjectId[]
				{
					base.UserContext.CalendarFolderOwaId
				};
			}
			else
			{
				array = (OwaStoreObjectId[])base.GetParameter("fId");
				if (array.Length > 5)
				{
					throw new OwaInvalidRequestException("Too many folders");
				}
				if (array.Length == 0)
				{
					throw new OwaInvalidRequestException("Must pass at least one folder id");
				}
			}
			ExDateTime exDateTime;
			ExDateTime arg;
			DatePickerBase.GetVisibleDateRange(month, out exDateTime, out arg, base.UserContext.TimeZone);
			Duration timeWindow = new Duration((DateTime)exDateTime, (DateTime)arg.IncrementDays(1));
			ExTraceGlobals.CalendarTracer.TraceDebug<ExDateTime, ExDateTime>((long)this.GetHashCode(), "Getting free/busy data from {0} to {1}", exDateTime, arg);
			string multiCalendarFreeBusyDataForDatePicker = Utilities.GetMultiCalendarFreeBusyDataForDatePicker(timeWindow, array, base.UserContext);
			this.Writer.Write("<div id=fb _m=\"");
			this.Writer.Write(month.Month);
			this.Writer.Write("\" _y=\"");
			this.Writer.Write(month.Year);
			this.Writer.Write("\">");
			Utilities.HtmlEncode(multiCalendarFreeBusyDataForDatePicker, this.Writer);
			this.Writer.Write("</div>");
		}

		// Token: 0x04001DD7 RID: 7639
		public const string EventNamespace = "DatePicker";

		// Token: 0x04001DD8 RID: 7640
		public const string MethodRenderMonth = "RenderMonth";

		// Token: 0x04001DD9 RID: 7641
		public const string MethodGetFreeBusy = "GetFreeBusy";

		// Token: 0x04001DDA RID: 7642
		public const string Month = "m";

		// Token: 0x04001DDB RID: 7643
		public const string Features = "uF";

		// Token: 0x04001DDC RID: 7644
		public const string FolderId = "fId";
	}
}
