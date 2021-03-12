using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x0200028E RID: 654
	internal class WebPartUtilities
	{
		// Token: 0x06001920 RID: 6432 RVA: 0x000924B8 File Offset: 0x000906B8
		private static Dictionary<StoreObjectType, Dictionary<string, WebPartListView>> LoadViews()
		{
			Dictionary<StoreObjectType, Dictionary<string, WebPartListView>> dictionary = new Dictionary<StoreObjectType, Dictionary<string, WebPartListView>>();
			Dictionary<string, WebPartListView> dictionary2 = new Dictionary<string, WebPartListView>(StringComparer.OrdinalIgnoreCase);
			dictionary2["messages"] = new WebPartListView(new int?(7), null, new bool?(false), false);
			dictionary2["by sender"] = new WebPartListView(new int?(1), new int?(0), new bool?(false), false);
			dictionary2["by subject"] = new WebPartListView(new int?(3), new int?(0), new bool?(false), false);
			dictionary2["by conversation topic"] = new WebPartListView(null, null, null, true);
			dictionary2["two line"] = new WebPartListView(null, null, new bool?(true), true);
			dictionary[StoreObjectType.Folder] = dictionary2;
			Dictionary<string, WebPartListView> dictionary3 = new Dictionary<string, WebPartListView>(StringComparer.OrdinalIgnoreCase);
			dictionary3["daily"] = new WebPartListView(null, null, null, false);
			dictionary3["weekly"] = new WebPartListView(null, null, null, false);
			dictionary3["monthly"] = new WebPartListView(null, null, null, false);
			dictionary[StoreObjectType.CalendarFolder] = dictionary3;
			Dictionary<string, WebPartListView> dictionary4 = new Dictionary<string, WebPartListView>(StringComparer.OrdinalIgnoreCase);
			dictionary4["two line"] = new WebPartListView(null, null, new bool?(true), true);
			dictionary4["phone list"] = new WebPartListView(null, null, new bool?(false), false);
			dictionary[StoreObjectType.ContactsFolder] = dictionary4;
			Dictionary<string, WebPartListView> dictionary5 = new Dictionary<string, WebPartListView>(StringComparer.OrdinalIgnoreCase);
			dictionary5["by due date"] = new WebPartListView(new int?(74), new int?(1), null, true);
			dictionary5["by subject"] = new WebPartListView(new int?(3), new int?(0), new bool?(false), true);
			dictionary[StoreObjectType.TasksFolder] = dictionary5;
			return dictionary;
		}

		// Token: 0x06001921 RID: 6433 RVA: 0x00092714 File Offset: 0x00090914
		public static bool IsCmdWebPart(HttpRequest request)
		{
			string queryStringParameter = Utilities.GetQueryStringParameter(request, "cmd", false);
			return !string.IsNullOrEmpty(queryStringParameter) && string.Equals(queryStringParameter, "contents", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06001922 RID: 6434 RVA: 0x00092744 File Offset: 0x00090944
		private static int GetDatePartValue(string datePart, int defaultDatePartValue)
		{
			int result = -1;
			bool flag = true;
			if (string.IsNullOrEmpty(datePart))
			{
				result = defaultDatePartValue;
			}
			else
			{
				flag = int.TryParse(datePart, out result);
			}
			if (!flag)
			{
				return -1;
			}
			return result;
		}

		// Token: 0x06001923 RID: 6435 RVA: 0x00092770 File Offset: 0x00090970
		public static string ValidateDate(string day, string month, string year)
		{
			ExDateTime localTime = DateTimeUtilities.GetLocalTime();
			int datePartValue = WebPartUtilities.GetDatePartValue(day, localTime.Day);
			if (datePartValue == -1)
			{
				return null;
			}
			int datePartValue2 = WebPartUtilities.GetDatePartValue(month, localTime.Month);
			if (datePartValue2 == -1)
			{
				return null;
			}
			int datePartValue3 = WebPartUtilities.GetDatePartValue(year, localTime.Year);
			if (datePartValue3 <= 1 || datePartValue3 >= 9999)
			{
				return null;
			}
			string result;
			try
			{
				ExDateTime date = new ExDateTime(UserContextManager.GetUserContext().TimeZone, datePartValue3, datePartValue2, datePartValue);
				result = DateTimeUtilities.GetIsoDateFormat(date);
			}
			catch (ArgumentOutOfRangeException)
			{
				ExTraceGlobals.CalendarTracer.TraceDebug<int, int, int>(0L, "Invalid date parameters for day: {1} month: {2}, year: {3}", datePartValue, datePartValue2, datePartValue3);
				result = null;
			}
			return result;
		}

		// Token: 0x06001924 RID: 6436 RVA: 0x00092814 File Offset: 0x00090A14
		public static WebPartListView LookUpWebPartView(StoreObjectType folderObjectType, string className, string view)
		{
			WebPartListView result = null;
			Dictionary<string, WebPartListView> dictionary = null;
			if (string.IsNullOrEmpty(view))
			{
				view = WebPartUtilities.GetDefaultView(className);
			}
			if (WebPartUtilities.Views.TryGetValue(folderObjectType, out dictionary))
			{
				dictionary.TryGetValue(view, out result);
			}
			return result;
		}

		// Token: 0x06001925 RID: 6437 RVA: 0x0009284F File Offset: 0x00090A4F
		public static string GetDefaultView(string className)
		{
			if (ObjectClass.IsMessageFolder(className))
			{
				return "by conversation topic";
			}
			if (ObjectClass.IsCalendarFolder(className))
			{
				return "daily";
			}
			if (ObjectClass.IsContactsFolder(className))
			{
				return "two line";
			}
			if (ObjectClass.IsTaskFolder(className))
			{
				return "by due date";
			}
			return "messages";
		}

		// Token: 0x06001926 RID: 6438 RVA: 0x0009288E File Offset: 0x00090A8E
		public static string TryGetLocalMachineTimeZone()
		{
			if (string.IsNullOrEmpty(ExTimeZone.CurrentTimeZone.Id))
			{
				return string.Empty;
			}
			return ExTimeZone.CurrentTimeZone.Id;
		}

		// Token: 0x06001927 RID: 6439 RVA: 0x000928B4 File Offset: 0x00090AB4
		internal static void RenderError(OwaContext owaContext, TextWriter writer)
		{
			if (owaContext == null)
			{
				throw new ArgumentNullException("owaContext");
			}
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (owaContext.ErrorInformation == null)
			{
				throw new ArgumentNullException("owaContext", "owaContext.ErrorInformation is null");
			}
			owaContext.HttpContext.Response.Clear();
			writer.Write("<meta http-equiv=\"Content-Type\" content=\"text/html; CHARSET=utf-8\">");
			writer.Write("<body style=\"background-color:#FFEFB2;font-size:11pt;\"");
			if (owaContext.SessionContext != null && owaContext.SessionContext.IsRtl)
			{
				writer.Write(" dir=\"rtl\"");
			}
			writer.Write(">");
			string value = string.Format("{{font-family:{0};}}", Utilities.GetDefaultFontName());
			writer.Write("<style>");
			writer.Write("BODY");
			if (Culture.GetUserCulture().LCID == 5124 || Culture.GetUserCulture().LCID == 1028 || Culture.GetUserCulture().LCID == 3076)
			{
				writer.Write(OwaPlainTextStyle.GetStyleFromCharset("big5"));
			}
			else if (Culture.GetUserCulture().LCID == 2052 || Culture.GetUserCulture().LCID == 4100)
			{
				writer.Write(OwaPlainTextStyle.GetStyleFromCharset("gb2312"));
			}
			else if (Culture.GetUserCulture().LCID == 1041)
			{
				writer.Write(OwaPlainTextStyle.GetStyleFromCharset("iso-2022-jp"));
			}
			else if (Culture.GetUserCulture().LCID == 1042)
			{
				writer.Write(OwaPlainTextStyle.GetStyleFromCharset("iso-2022-kr"));
			}
			else
			{
				writer.Write(value);
			}
			writer.Write("</style>");
			Utilities.HtmlEncode(owaContext.ErrorInformation.Message, writer);
			if (owaContext.ErrorInformation.OwaEventHandlerErrorCode == OwaEventHandlerErrorCode.WebPartFirstAccessError)
			{
				writer.Write("<br><a target=_blank href=\"");
				writer.Write(Utilities.HtmlEncode(OwaUrl.ApplicationRoot.GetExplicitUrl(owaContext)));
				writer.Write("\">");
				writer.Write(LocalizedStrings.GetHtmlEncoded(675765292));
				writer.Write("</a>");
			}
			else if (owaContext.ErrorInformation.OwaEventHandlerErrorCode == OwaEventHandlerErrorCode.WebPartTaskFolderError || owaContext.ErrorInformation.OwaEventHandlerErrorCode == OwaEventHandlerErrorCode.ErrorEarlyBrowserOnPublishedCalendar || owaContext.ErrorInformation.OwaEventHandlerErrorCode == OwaEventHandlerErrorCode.WebPartCalendarFolderError || owaContext.ErrorInformation.OwaEventHandlerErrorCode == OwaEventHandlerErrorCode.WebPartAccessPublicFolderViaOwaBasicError)
			{
				string value2 = Utilities.HtmlEncode(Globals.SupportedBrowserHelpUrl);
				writer.Write("<br><a target=\"_blank\" href=\"");
				writer.Write(value2);
				writer.Write("\">");
				writer.Write(value2);
				writer.Write("</a>");
			}
			if (owaContext.ErrorInformation.MessageDetails != null)
			{
				writer.Write("<br>");
				Utilities.HtmlEncode(owaContext.ErrorInformation.MessageDetails, writer);
			}
			writer.Write("</body>");
			owaContext.HttpContext.Response.ContentType = Utilities.GetContentTypeString(OwaEventContentType.Html);
			owaContext.HttpContext.Response.AppendHeader("X-OWA-EventResult", "1");
		}

		// Token: 0x04001256 RID: 4694
		private const string Command = "cmd";

		// Token: 0x04001257 RID: 4695
		private const string CommandValue = "contents";

		// Token: 0x04001258 RID: 4696
		public const string MessagesView = "messages";

		// Token: 0x04001259 RID: 4697
		public const string BySenderView = "by sender";

		// Token: 0x0400125A RID: 4698
		public const string BySubjectView = "by subject";

		// Token: 0x0400125B RID: 4699
		public const string ByConversationTopicView = "by conversation topic";

		// Token: 0x0400125C RID: 4700
		public const string TwoLineView = "two line";

		// Token: 0x0400125D RID: 4701
		public const string DailyView = "daily";

		// Token: 0x0400125E RID: 4702
		public const string WeeklyView = "weekly";

		// Token: 0x0400125F RID: 4703
		public const string MonthlyView = "monthly";

		// Token: 0x04001260 RID: 4704
		public const string PhoneListView = "phone list";

		// Token: 0x04001261 RID: 4705
		public const string ByDueDateView = "by due date";

		// Token: 0x04001262 RID: 4706
		public static Dictionary<StoreObjectType, Dictionary<string, WebPartListView>> Views = WebPartUtilities.LoadViews();
	}
}
