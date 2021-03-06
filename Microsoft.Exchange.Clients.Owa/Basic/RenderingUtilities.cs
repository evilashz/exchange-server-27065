using System;
using System.Globalization;
using System.IO;
using System.Web;
using Microsoft.Exchange.Clients.Owa.Basic.Controls;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Security;

namespace Microsoft.Exchange.Clients.Owa.Basic
{
	// Token: 0x020000B2 RID: 178
	public static class RenderingUtilities
	{
		// Token: 0x060006AD RID: 1709 RVA: 0x000346BF File Offset: 0x000328BF
		public static void RenderDateTimeScriptObject(TextWriter output, ExDateTime dateTime)
		{
			RenderingUtilities.RenderDateTimeScriptObject(output, dateTime, false);
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x000346CC File Offset: 0x000328CC
		public static void RenderDateTimeScriptObject(TextWriter output, ExDateTime dateTime, bool seconds)
		{
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			output.Write("new Date(Date.UTC(");
			output.Write(dateTime.Year);
			output.Write(",");
			output.Write(dateTime.Month - 1);
			output.Write(",");
			output.Write(dateTime.Day);
			output.Write(",");
			output.Write(dateTime.Hour);
			output.Write(",");
			output.Write(dateTime.Minute);
			if (seconds)
			{
				output.Write(",");
				output.Write(dateTime.Second);
			}
			output.Write("))");
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x00034788 File Offset: 0x00032988
		public static void RenderLocalDateTimeScriptVariable(TextWriter output, ExDateTime dateTime, string variableName)
		{
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			if (string.IsNullOrEmpty(variableName))
			{
				throw new ArgumentException("variableName may not be null or empty string");
			}
			output.WriteLine("var {0} = new Date();", variableName);
			output.WriteLine("{0}.setFullYear({1},{2},{3});", new object[]
			{
				variableName,
				dateTime.Year,
				dateTime.Month - 1,
				dateTime.Day
			});
			output.WriteLine("{0}.setHours({1});", variableName, dateTime.Hour);
			output.WriteLine("{0}.setMinutes({1});", variableName, dateTime.Minute);
			output.WriteLine("{0}.setSeconds({1});", variableName, dateTime.Second);
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x00034850 File Offset: 0x00032A50
		internal static void RenderSpecialFolderIcon(TextWriter output, UserContext userContext, string folderClass, StoreObjectId folderId)
		{
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			if (folderId == null)
			{
				throw new ArgumentNullException("folderId");
			}
			ThemeFileId themeFileId = ThemeFileId.None;
			switch (Utilities.GetDefaultFolderType(userContext.MailboxSession, folderId))
			{
			case DefaultFolderType.DeletedItems:
				themeFileId = ThemeFileId.BasicDeleted;
				break;
			case DefaultFolderType.Drafts:
				themeFileId = ThemeFileId.Drafts;
				break;
			case DefaultFolderType.Inbox:
				themeFileId = ThemeFileId.Inbox;
				break;
			case DefaultFolderType.JunkEmail:
				themeFileId = ThemeFileId.JunkEMail;
				break;
			case DefaultFolderType.SentItems:
				themeFileId = ThemeFileId.SentItems;
				break;
			}
			if (themeFileId == ThemeFileId.None)
			{
				SmallIconManager.RenderFolderIconUrl(output, userContext, folderClass);
				return;
			}
			userContext.RenderThemeFileUrl(output, themeFileId);
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x000348F4 File Offset: 0x00032AF4
		public static void RenderNavigationFooter(UserContext userContext, string text, TextWriter output)
		{
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			output.Write("<table cellspacing=0 cellpadding=0 class=\"qbt\">");
			output.Write("<tr><td colspan=2 class=\"txt\">{0}</td></tr><tr>", text);
			output.Write("<td class=\"crv\"><img src=\"");
			userContext.RenderThemeFileUrl(output, ThemeFileId.CornerBottomLeft);
			output.Write("\" alt=\"\"></td><td class=\"btm\"><img src=\"");
			userContext.RenderThemeFileUrl(output, ThemeFileId.Clear);
			output.Write("\" alt=\"\"></td></tr></table>");
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x00034968 File Offset: 0x00032B68
		public static void RenderNavigationFooter(UserContext userContext, TextWriter output)
		{
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			output.Write("<table cellspacing=0 cellpadding=0 class=\"qbt\">");
			output.Write("<tr><td colspan=2 class=\"txt\">&nbsp;</td></tr><tr><td class=\"crv\"><img src=\"");
			userContext.RenderThemeFileUrl(output, ThemeFileId.CornerBottomLeft);
			output.Write("\" alt=\"\"></td><td class=\"btm\"><img src=\"");
			userContext.RenderThemeFileUrl(output, ThemeFileId.Clear);
			output.Write("\" alt=\"\"></td></tr></table>");
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x000349D0 File Offset: 0x00032BD0
		public static void RenderClientStrings(TextWriter output)
		{
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			output.Write("<script type=\"text/javascript\" ");
			output.Write("src=\"forms/basic/BasicClientStrings.aspx?v={0}&l={1}\">", Globals.ApplicationVersion, CultureInfo.CurrentUICulture.Name);
			output.Write("</script><script>");
			Utilities.RenderScriptToEnforceUTF8ForPage(output);
			output.Write("</script>");
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x00034A2C File Offset: 0x00032C2C
		public static void RenderStringVariable(TextWriter output, string variableName, Strings.IDs stringID)
		{
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			if (variableName == null)
			{
				throw new ArgumentNullException("variableName");
			}
			if (variableName.Length == 0)
			{
				throw new ArgumentException("variableName cannot be empty");
			}
			output.Write("var ");
			output.Write(variableName);
			output.Write(" = \"");
			output.Write(LocalizedStrings.GetJavascriptEncoded(stringID));
			output.Write("\";\n");
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x00034A9C File Offset: 0x00032C9C
		internal static void RenderSender(UserContext userContext, TextWriter output, MessageItem message)
		{
			if (message == null)
			{
				throw new ArgumentNullException("message");
			}
			if (Utilities.IsOnBehalfOf(message.Sender, message.From))
			{
				RenderingUtilities.RenderSenderOnBehalfOf(message.Sender, message.From, output, userContext);
				return;
			}
			RenderingUtilities.RenderSender(userContext, output, message.Sender);
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x00034AEB File Offset: 0x00032CEB
		internal static void RenderSender(UserContext userContext, TextWriter output, PostItem post)
		{
			if (post == null)
			{
				throw new ArgumentNullException("post");
			}
			RenderingUtilities.RenderSender(userContext, output, post.Sender);
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x00034B08 File Offset: 0x00032D08
		internal static void RenderSender(UserContext userContext, TextWriter output, Participant sender)
		{
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			output.Write("<span class=\"rwRRO\">");
			if (sender == null)
			{
				output.Write(string.Empty);
			}
			else if (string.CompareOrdinal(sender.RoutingType, "EX") == 0)
			{
				IRecipientSession session = Utilities.CreateADRecipientSession(ConsistencyMode.IgnoreInvalid, userContext);
				int num = 1;
				ADRecipient recipientByLegacyExchangeDN = Utilities.GetRecipientByLegacyExchangeDN(session, sender.EmailAddress);
				if (recipientByLegacyExchangeDN != null)
				{
					if (recipientByLegacyExchangeDN is IADDistributionList)
					{
						num = 2;
					}
					output.Write("<a href=\"#\" id=\"");
					Utilities.SanitizeHtmlEncode(Utilities.GetBase64StringFromADObjectId(recipientByLegacyExchangeDN.Id), output);
					output.Write("\" onClick=\"return onClkRcpt(this,{0});\">", num);
					if (!string.IsNullOrEmpty(sender.DisplayName))
					{
						output.Write(Utilities.SanitizeHtmlEncode(sender.DisplayName));
					}
					else if (!string.IsNullOrEmpty(sender.EmailAddress))
					{
						output.Write(Utilities.SanitizeHtmlEncode(sender.EmailAddress));
					}
					output.Write("</a>");
				}
				else if (!string.IsNullOrEmpty(sender.DisplayName))
				{
					Utilities.SanitizeHtmlEncode(sender.DisplayName, output);
				}
			}
			else if (!string.IsNullOrEmpty(sender.DisplayName))
			{
				if (!string.IsNullOrEmpty(sender.EmailAddress) && string.CompareOrdinal(sender.RoutingType, "SMTP") == 0)
				{
					output.Write(Utilities.SanitizeHtmlEncode(string.Format("{0} [{1}]", sender.DisplayName, sender.EmailAddress)));
				}
				else
				{
					output.Write(Utilities.SanitizeHtmlEncode(sender.DisplayName));
				}
			}
			else if (!string.IsNullOrEmpty(sender.EmailAddress))
			{
				output.Write(Utilities.SanitizeHtmlEncode(sender.EmailAddress));
			}
			output.Write("</span>");
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x00034CAC File Offset: 0x00032EAC
		internal static void RenderSenderOnBehalfOf(Participant representiveSender, Participant originalSender, TextWriter writer, UserContext userContext)
		{
			if (representiveSender == null)
			{
				throw new ArgumentNullException("representiveSender");
			}
			if (originalSender == null)
			{
				throw new ArgumentNullException("originalSender");
			}
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			using (SanitizingStringWriter<OwaHtml> sanitizingStringWriter = new SanitizingStringWriter<OwaHtml>())
			{
				using (SanitizingStringWriter<OwaHtml> sanitizingStringWriter2 = new SanitizingStringWriter<OwaHtml>())
				{
					RenderingUtilities.RenderSender(userContext, sanitizingStringWriter, representiveSender);
					RenderingUtilities.RenderSender(userContext, sanitizingStringWriter2, originalSender);
					writer.Write(LocalizedStrings.GetHtmlEncoded(-165544498), sanitizingStringWriter.ToSanitizedString<SanitizedHtmlString>(), sanitizingStringWriter2.ToSanitizedString<SanitizedHtmlString>());
				}
			}
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x00034D5C File Offset: 0x00032F5C
		public static void RenderSentTime(TextWriter output, ExDateTime sentTime, UserContext userContext)
		{
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			output.Write(SanitizedHtmlString.Format(LocalizedStrings.GetNonEncoded(-1617047463), new object[]
			{
				sentTime.ToString(DateTimeFormatInfo.CurrentInfo.LongDatePattern),
				sentTime.ToString(userContext.UserOptions.TimeFormat)
			}));
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x00034DCB File Offset: 0x00032FCB
		internal static void RenderSubject(TextWriter output, Item item)
		{
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			RenderingUtilities.RenderSubject(output, item, string.Empty);
		}

		// Token: 0x060006BB RID: 1723 RVA: 0x00034DE8 File Offset: 0x00032FE8
		internal static void RenderSubject(TextWriter output, Item item, string untitled)
		{
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			string text = (item == null) ? string.Empty : (item.TryGetProperty(ItemSchema.Subject) as string);
			if (string.IsNullOrEmpty(text))
			{
				if (!string.IsNullOrEmpty(untitled))
				{
					Utilities.SanitizeHtmlEncode(untitled, output);
				}
				return;
			}
			if (text.Length > 255)
			{
				Utilities.SanitizeHtmlEncode(text.Substring(0, 255), output);
				return;
			}
			Utilities.SanitizeHtmlEncode(text, output);
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x00034E5D File Offset: 0x0003305D
		public static void RenderHorizontalDivider(UserContext userContext, TextWriter writer)
		{
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			writer.Write("<table cellspacing=0 cellpadding=0 class=\"hdvt\"><tr><td class=\"ihdv\"><img src=\"");
			userContext.RenderThemeFileUrl(writer, ThemeFileId.Clear);
			writer.Write("\" alt=\"\"></td></tr></table>");
		}

		// Token: 0x060006BD RID: 1725 RVA: 0x00034E9C File Offset: 0x0003309C
		public static void RenderHorizontalDividerForFolderManagerForm(UserContext userContext, TextWriter writer)
		{
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			writer.Write("<table cellspacing=0 cellpadding=0 class=\"hdfm\"><tr><td class=\"brk\"><img src=\"");
			userContext.RenderThemeFileUrl(writer, ThemeFileId.Clear);
			writer.Write("\" alt=\"\"></td><td class=\"line\">");
			writer.Write("</td><td class=\"brk\"><img src=\"");
			userContext.RenderThemeFileUrl(writer, ThemeFileId.Clear);
			writer.Write("\" alt=\"\"></td></tr></table>");
		}

		// Token: 0x060006BE RID: 1726 RVA: 0x00034F04 File Offset: 0x00033104
		internal static bool RenderReplyForwardMessageStatus(Item item, Infobar infobar, UserContext userContext)
		{
			object obj = null;
			object obj2 = null;
			MessageItem messageItem = item as MessageItem;
			CalendarItemBase calendarItemBase = item as CalendarItemBase;
			if (messageItem != null)
			{
				obj = messageItem.TryGetProperty(MessageItemSchema.LastVerbExecuted);
				obj2 = messageItem.TryGetProperty(MessageItemSchema.LastVerbExecutionTime);
			}
			else if (calendarItemBase != null)
			{
				obj = calendarItemBase.TryGetProperty(MessageItemSchema.LastVerbExecuted);
				obj2 = calendarItemBase.TryGetProperty(MessageItemSchema.LastVerbExecutionTime);
			}
			if (obj2 is ExDateTime)
			{
				ExDateTime exDateTime = (ExDateTime)obj2;
				if (obj is int && ((int)obj == 102 || (int)obj == 103))
				{
					string messageText = string.Format(CultureInfo.InvariantCulture, LocalizedStrings.GetNonEncoded(-1084747171), new object[]
					{
						exDateTime.ToString(userContext.UserOptions.DateFormat),
						exDateTime.ToString(userContext.UserOptions.TimeFormat)
					});
					infobar.AddMessageText(messageText, InfobarMessageType.Informational);
					return true;
				}
				if (obj is int && (int)obj == 104)
				{
					string messageText = string.Format(CultureInfo.InvariantCulture, LocalizedStrings.GetNonEncoded(1995820000), new object[]
					{
						exDateTime.ToString(userContext.UserOptions.DateFormat),
						exDateTime.ToString(userContext.UserOptions.TimeFormat)
					});
					infobar.AddMessageText(messageText, InfobarMessageType.Informational);
					return true;
				}
			}
			return false;
		}

		// Token: 0x060006BF RID: 1727 RVA: 0x00035050 File Offset: 0x00033250
		public static void RenderJavascriptVariable(TextWriter output, string name, string value)
		{
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentException("name may not be null or empty string");
			}
			output.Write("var ");
			output.Write(name);
			output.Write("=\"");
			Utilities.JavascriptEncode(value, output);
			output.Write("\";");
		}

		// Token: 0x060006C0 RID: 1728 RVA: 0x000350B0 File Offset: 0x000332B0
		public static void RenderJavascriptVariable(TextWriter output, string name, bool value)
		{
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentException("name may not be null or empty string");
			}
			output.Write("var ");
			output.Write(name);
			output.Write("=");
			output.Write(value ? "1" : "0");
			output.Write(";");
		}

		// Token: 0x060006C1 RID: 1729 RVA: 0x0003511B File Offset: 0x0003331B
		public static void RenderJavascriptVariable(TextWriter output, string name, int value)
		{
			output.Write("var ");
			output.Write(name);
			output.Write("=");
			output.Write(value);
			output.Write(";");
		}

		// Token: 0x060006C2 RID: 1730 RVA: 0x0003514C File Offset: 0x0003334C
		public static void RenderOwnerItemInformationFromQueryString(HttpRequest request, TextWriter writer)
		{
			string value = Utilities.GetQueryStringParameter(request, "oId", false) ?? string.Empty;
			RenderingUtilities.RenderJavascriptVariable(writer, "a_sOwId", value);
			string value2 = Utilities.GetQueryStringParameter(request, "oCk", false) ?? string.Empty;
			RenderingUtilities.RenderJavascriptVariable(writer, "a_sOwCk", value2);
			string value3 = Utilities.GetQueryStringParameter(request, "oT", false) ?? string.Empty;
			RenderingUtilities.RenderJavascriptVariable(writer, "a_sOwTp", value3);
			string value4 = Utilities.GetQueryStringParameter(request, "oS", false) ?? string.Empty;
			RenderingUtilities.RenderJavascriptVariable(writer, "a_sOwSt", value4);
		}

		// Token: 0x060006C3 RID: 1731 RVA: 0x000351E4 File Offset: 0x000333E4
		public static void RenderAutoSaveAndKeepAlive(TextWriter writer, UserContext userContext)
		{
			int autoSaveInterval = Globals.AutoSaveInterval;
			int value = 120000;
			if (autoSaveInterval <= 120)
			{
				value = autoSaveInterval * 1000 - 30000;
			}
			RenderingUtilities.RenderJavascriptVariable(writer, "a_iASI", autoSaveInterval);
			RenderingUtilities.RenderJavascriptVariable(writer, "a_iKAI", value);
		}

		// Token: 0x060006C4 RID: 1732 RVA: 0x00035228 File Offset: 0x00033428
		public static void RenderOwaBasicLogo(TextWriter writer, ISessionContext sessionContext)
		{
			sessionContext.RenderThemeFileUrl(writer, ThemeFileId.BasicLogo);
		}
	}
}
