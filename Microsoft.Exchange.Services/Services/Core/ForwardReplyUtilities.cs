using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Reflection;
using System.Resources;
using System.Text;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x020002F6 RID: 758
	internal static class ForwardReplyUtilities
	{
		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06001564 RID: 5476 RVA: 0x0006D668 File Offset: 0x0006B868
		public static ResourceManager ClientsResourceManager
		{
			get
			{
				if (ForwardReplyUtilities.resourceManager == null)
				{
					AssemblyName name = typeof(ForwardReplyUtilities).GetTypeInfo().Assembly.GetName();
					string assemblyName = name.FullName.Replace(name.Name, "Microsoft.Exchange.Clients.Strings");
					ForwardReplyUtilities.resourceManager = new ResourceManager("Microsoft.Exchange.Clients.Strings", Assembly.Load(new AssemblyName(assemblyName)));
				}
				return ForwardReplyUtilities.resourceManager;
			}
		}

		// Token: 0x06001565 RID: 5477 RVA: 0x0006D6CC File Offset: 0x0006B8CC
		public static string CreateForwardReplyHeader(BodyFormat bodyFormat, Item item, ForwardReplyHeaderOptions headerOptions, bool isMeetingItem, CultureInfo culture, string timeFormat, ExTimeZone timeZone = null)
		{
			if (!(item is MessageItem) && !(item is CalendarItemBase) && !(item is PostItem))
			{
				throw new ArgumentException("HTML reply forward headers can only be created for MessageItem, CalendarItemBase and PostItem");
			}
			if (item is PostItem)
			{
				return ForwardReplyUtilities.CreatePostReplyForwardHeader(bodyFormat, item, headerOptions, culture, timeFormat);
			}
			IList<IRecipientBase> toRecipients = null;
			IList<IRecipientBase> ccRecipients = null;
			string from = string.Empty;
			string sender = null;
			string @string = ForwardReplyUtilities.ClientsResourceManager.GetString("FromColon", culture);
			string string2 = ForwardReplyUtilities.ClientsResourceManager.GetString("ToColon", culture);
			string string3 = ForwardReplyUtilities.ClientsResourceManager.GetString("CcColon", culture);
			if (item is MessageItem)
			{
				MessageItem messageItem = (MessageItem)item;
				if (messageItem.From != null)
				{
					from = ForwardReplyUtilities.GetParticipantDisplayString(messageItem.From);
				}
				if (messageItem.Sender != null)
				{
					sender = ForwardReplyUtilities.GetParticipantDisplayString(messageItem.Sender);
				}
				toRecipients = ForwardReplyUtilities.GetMessageRecipientCollection(RecipientItemType.To, messageItem);
				ccRecipients = ForwardReplyUtilities.GetMessageRecipientCollection(RecipientItemType.Cc, messageItem);
			}
			else if (item is CalendarItemBase)
			{
				CalendarItemBase calendarItemBase = (CalendarItemBase)item;
				if (calendarItemBase.Organizer != null)
				{
					from = calendarItemBase.Organizer.DisplayName;
				}
				toRecipients = ForwardReplyUtilities.GetCalendarItemRecipientCollection(AttendeeType.Required, calendarItemBase);
				ccRecipients = ForwardReplyUtilities.GetCalendarItemRecipientCollection(AttendeeType.Optional, calendarItemBase);
			}
			switch (bodyFormat)
			{
			case BodyFormat.TextPlain:
				return ForwardReplyUtilities.CreateTextReplyForwardHeader(item, headerOptions, @string, from, sender, string2, string3, toRecipients, ccRecipients, isMeetingItem, culture, timeFormat, timeZone);
			case BodyFormat.TextHtml:
				return ForwardReplyUtilities.CreateHtmlReplyForwardHeader(item, headerOptions, @string, from, sender, string2, string3, toRecipients, ccRecipients, isMeetingItem, culture, timeFormat, timeZone);
			default:
				throw new ArgumentException("Unsupported body format");
			}
		}

		// Token: 0x06001566 RID: 5478 RVA: 0x0006D844 File Offset: 0x0006BA44
		private static string GetParticipantDisplayString(Participant participant)
		{
			MailboxHelper.MailboxTypeType mailboxType = MailboxHelper.GetMailboxType(participant.Origin, participant.RoutingType);
			string result;
			if ((mailboxType == MailboxHelper.MailboxTypeType.Unknown || mailboxType == MailboxHelper.MailboxTypeType.OneOff) && string.CompareOrdinal(participant.RoutingType, "SMTP") == 0 && !string.IsNullOrWhiteSpace(participant.EmailAddress))
			{
				result = string.Format("{0} <{1}>", participant.DisplayName, participant.EmailAddress);
			}
			else
			{
				result = participant.DisplayName;
			}
			return result;
		}

		// Token: 0x06001567 RID: 5479 RVA: 0x0006D8AC File Offset: 0x0006BAAC
		private static string CreateHtmlReplyForwardHeader(Item item, ForwardReplyHeaderOptions headerOptions, string fromLabel, string from, string sender, string toLabel, string ccLabel, IList<IRecipientBase> toRecipients, IList<IRecipientBase> ccRecipients, bool isMeetingItem, CultureInfo culture, string timeFormat, ExTimeZone timeZone)
		{
			StringBuilder stringBuilder = new StringBuilder(150);
			stringBuilder.Append("<HR style=\"display:inline-block;width:98%\" tabindex=\"-1\">");
			bool flag = ForwardReplyUtilities.IsRightToLeft(culture);
			stringBuilder.Append("<DIV id=divRplyFwdMsg ");
			stringBuilder.Append(flag ? "dir=\"rtl\">" : "dir=\"ltr\">");
			stringBuilder.Append("<FONT FACE=\"Calibri, sans-serif\" style=\"font-size:11pt\" color=\"#000000\">");
			bool flag2 = !string.IsNullOrEmpty(from);
			bool flag3 = !string.IsNullOrEmpty(sender);
			bool flag4 = true;
			if (flag3 || flag2)
			{
				stringBuilder.Append("<B>");
				stringBuilder.Append(fromLabel);
				stringBuilder.Append("</B> ");
				if (flag3 && string.Compare(sender, from, StringComparison.Ordinal) != 0)
				{
					ForwardReplyUtilities.HtmlEncode(sender, stringBuilder);
					if (flag2)
					{
						stringBuilder.Append(string.Format(culture, ForwardReplyUtilities.ClientsResourceManager.GetString("OnBehalfOf", culture), new object[]
						{
							string.Empty,
							string.Empty
						}));
					}
				}
				if (flag2)
				{
					ForwardReplyUtilities.HtmlEncode(from, stringBuilder);
				}
				flag4 = false;
			}
			object obj = item.TryGetProperty(ItemSchema.SentTime);
			if (obj != null && obj is ExDateTime)
			{
				ExDateTime exDateTime = (ExDateTime)obj;
				if (timeZone != null)
				{
					exDateTime = timeZone.ConvertDateTime(exDateTime);
				}
				if (!flag4)
				{
					stringBuilder.Append("<BR>");
				}
				stringBuilder.Append("<B>");
				stringBuilder.Append(ForwardReplyUtilities.ClientsResourceManager.GetString("SentColon", culture));
				stringBuilder.Append("</B> ");
				stringBuilder.AppendFormat(ForwardReplyUtilities.ClientsResourceManager.GetString("SentTime", culture), exDateTime.ToLongDateString(), string.IsNullOrWhiteSpace(timeFormat) ? exDateTime.ToLongTimeString() : exDateTime.ToString(timeFormat));
				flag4 = false;
			}
			if (0 < toRecipients.Count)
			{
				if (!flag4)
				{
					stringBuilder.Append("<BR>");
				}
				stringBuilder.Append("<B>");
				stringBuilder.Append(toLabel);
				stringBuilder.Append("</B> ");
				int num = 0;
				foreach (IRecipientBase recipientBase in toRecipients)
				{
					num++;
					ForwardReplyUtilities.HtmlEncode(recipientBase.Participant.DisplayName, stringBuilder);
					if (num < toRecipients.Count)
					{
						stringBuilder.Append("; ");
					}
				}
				flag4 = false;
			}
			if (0 < ccRecipients.Count)
			{
				if (!flag4)
				{
					stringBuilder.Append("<BR>");
				}
				stringBuilder.Append("<B>");
				stringBuilder.Append(ccLabel);
				stringBuilder.Append("</B> ");
				int num2 = 0;
				foreach (IRecipientBase recipientBase2 in ccRecipients)
				{
					num2++;
					ForwardReplyUtilities.HtmlEncode(recipientBase2.Participant.DisplayName, stringBuilder);
					if (num2 < ccRecipients.Count)
					{
						stringBuilder.Append("; ");
					}
				}
				flag4 = false;
			}
			if (!flag4)
			{
				stringBuilder.Append("<BR>");
			}
			stringBuilder.Append("<B>");
			stringBuilder.Append(ForwardReplyUtilities.ClientsResourceManager.GetString("SubjectColon", culture));
			stringBuilder.Append("</B> ");
			string text = item.TryGetProperty(ItemSchema.Subject) as string;
			if (text == null)
			{
				text = string.Empty;
			}
			ForwardReplyUtilities.HtmlEncode(text, stringBuilder);
			if (isMeetingItem)
			{
				stringBuilder.Append("<BR>");
				stringBuilder.Append("<B>");
				stringBuilder.Append(ForwardReplyUtilities.ClientsResourceManager.GetString("WhenColon", culture));
				stringBuilder.Append("</B> ");
				ForwardReplyUtilities.HtmlEncode(ForwardReplyUtilities.GenerateWhen(item), stringBuilder);
				stringBuilder.Append("<BR><B>");
				stringBuilder.Append(ForwardReplyUtilities.ClientsResourceManager.GetString("WhereColon", culture));
				stringBuilder.Append("</B> ");
				string text2 = item.TryGetProperty(CalendarItemBaseSchema.Location) as string;
				if (!string.IsNullOrEmpty(text2))
				{
					ForwardReplyUtilities.HtmlEncode(text2, stringBuilder);
				}
			}
			stringBuilder.Append("</FONT><DIV>&nbsp;</DIV></DIV>");
			return stringBuilder.ToString();
		}

		// Token: 0x06001568 RID: 5480 RVA: 0x0006DCD4 File Offset: 0x0006BED4
		private static string CreateTextReplyForwardHeader(Item item, ForwardReplyHeaderOptions headerOptions, string fromLabel, string from, string sender, string toLabel, string ccLabel, IList<IRecipientBase> toRecipients, IList<IRecipientBase> ccRecipients, bool isMeetingItem, CultureInfo culture, string timeFormat, ExTimeZone timeZone)
		{
			StringBuilder stringBuilder = new StringBuilder(100);
			if (headerOptions.AutoAddSignature)
			{
				stringBuilder.Append("\n\n");
				stringBuilder.Append(headerOptions.SignatureText);
			}
			stringBuilder.Append("\n________________________________________\n");
			bool flag = !string.IsNullOrEmpty(from);
			bool flag2 = !string.IsNullOrEmpty(sender);
			if (flag2 || flag)
			{
				stringBuilder.Append(fromLabel);
				stringBuilder.Append(" ");
				if (flag2 && string.Compare(sender, from, StringComparison.Ordinal) != 0)
				{
					stringBuilder.Append(sender);
					if (flag)
					{
						stringBuilder.Append(string.Format(culture, ForwardReplyUtilities.ClientsResourceManager.GetString("OnBehalfOf", culture), new object[]
						{
							string.Empty,
							string.Empty
						}));
					}
				}
				if (flag)
				{
					stringBuilder.Append(from);
				}
				stringBuilder.Append("\n");
			}
			object obj = item.TryGetProperty(ItemSchema.SentTime);
			if (obj != null && obj is ExDateTime)
			{
				ExDateTime exDateTime = (ExDateTime)obj;
				if (timeZone != null)
				{
					exDateTime = timeZone.ConvertDateTime(exDateTime);
				}
				stringBuilder.Append(ForwardReplyUtilities.ClientsResourceManager.GetString("SentColon", culture));
				stringBuilder.Append(" ");
				stringBuilder.AppendFormat(ForwardReplyUtilities.ClientsResourceManager.GetString("SentTime", culture), exDateTime.ToLongDateString(), string.IsNullOrWhiteSpace(timeFormat) ? exDateTime.ToLongTimeString() : exDateTime.ToString(timeFormat));
				stringBuilder.Append("\n");
			}
			if (0 < toRecipients.Count)
			{
				stringBuilder.Append(toLabel);
				stringBuilder.Append(" ");
				int num = 0;
				foreach (IRecipientBase recipientBase in toRecipients)
				{
					num++;
					stringBuilder.Append(recipientBase.Participant.DisplayName);
					if (num < toRecipients.Count)
					{
						stringBuilder.Append("; ");
					}
				}
				stringBuilder.Append("\n");
			}
			if (0 < ccRecipients.Count)
			{
				stringBuilder.Append(ccLabel);
				stringBuilder.Append(" ");
				int num2 = 0;
				foreach (IRecipientBase recipientBase2 in ccRecipients)
				{
					num2++;
					stringBuilder.Append(recipientBase2.Participant.DisplayName);
					if (num2 < ccRecipients.Count)
					{
						stringBuilder.Append("; ");
					}
				}
				stringBuilder.Append("\n");
			}
			stringBuilder.Append(ForwardReplyUtilities.ClientsResourceManager.GetString("SubjectColon", culture));
			stringBuilder.Append(" ");
			string text = item.TryGetProperty(ItemSchema.Subject) as string;
			if (text == null)
			{
				text = string.Empty;
			}
			stringBuilder.Append(text);
			stringBuilder.Append("\n");
			if (isMeetingItem)
			{
				stringBuilder.Append(ForwardReplyUtilities.ClientsResourceManager.GetString("WhenColon", culture));
				stringBuilder.Append(" ");
				stringBuilder.Append(ForwardReplyUtilities.GenerateWhen(item));
				stringBuilder.Append("\n");
				stringBuilder.Append(ForwardReplyUtilities.ClientsResourceManager.GetString("WhereColon", culture));
				stringBuilder.Append(" ");
				string value = item.TryGetProperty(CalendarItemBaseSchema.Location) as string;
				if (!string.IsNullOrEmpty(value))
				{
					stringBuilder.Append(value);
				}
				stringBuilder.Append("\n");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001569 RID: 5481 RVA: 0x0006E074 File Offset: 0x0006C274
		public static string CreatePostReplyForwardHeader(BodyFormat bodyFormat, Item item, ForwardReplyHeaderOptions options, CultureInfo culture, string timeFormat)
		{
			if (!(item is PostItem))
			{
				throw new ArgumentException("CreatePostReplyForwardheader is called on a non-PostItem item.");
			}
			PostItem postItem = (PostItem)item;
			StringBuilder stringBuilder = new StringBuilder();
			bool outputHtml = BodyFormat.TextHtml == bodyFormat;
			if (postItem.Sender != null)
			{
				if (postItem.From != null && string.Compare(postItem.Sender.DisplayName, postItem.From.DisplayName, StringComparison.Ordinal) != 0)
				{
					stringBuilder.Append(string.Format(ForwardReplyUtilities.ClientsResourceManager.GetString("OnBehalfOf", culture), ForwardReplyUtilities.GetParticipantDisplayString(postItem.Sender, outputHtml), ForwardReplyUtilities.GetParticipantDisplayString(postItem.From, outputHtml)));
				}
				else
				{
					ForwardReplyUtilities.AppendParticipantDisplayString(postItem.Sender, stringBuilder, outputHtml);
				}
			}
			string fromLabel = string.Empty;
			switch (bodyFormat)
			{
			case BodyFormat.TextPlain:
				fromLabel = ForwardReplyUtilities.ClientsResourceManager.GetString("FromColon", culture);
				return ForwardReplyUtilities.CreatePostTextReplyForwardHeader(postItem, options, fromLabel, stringBuilder.ToString(), culture, timeFormat);
			case BodyFormat.TextHtml:
				fromLabel = ForwardReplyUtilities.ClientsResourceManager.GetString("FromColon", culture);
				return ForwardReplyUtilities.CreatePostHtmlReplyForwardHeader(postItem, options, fromLabel, stringBuilder.ToString(), culture, timeFormat);
			default:
				throw new ArgumentException("Unsupported body format");
			}
		}

		// Token: 0x0600156A RID: 5482 RVA: 0x0006E190 File Offset: 0x0006C390
		private static string CreatePostHtmlReplyForwardHeader(PostItem item, ForwardReplyHeaderOptions headerOptions, string fromLabel, string fromHtmlMarkup, CultureInfo culture, string timeFormat)
		{
			StringBuilder stringBuilder = new StringBuilder(150);
			using (StringWriter stringWriter = new StringWriter(stringBuilder, culture))
			{
				ForwardReplyUtilities.RenderDefaultUserFontMarkup(headerOptions, stringWriter);
			}
			stringBuilder.Append("<DIV id=divRplyFwdMsg>");
			stringBuilder.Append("<HR style=\"display:inline-block;width:98%\" tabindex=\"-1\">");
			stringBuilder.Append("<FONT FACE=\"Calibri, sans-serif\" style=\"font-size:11pt\">");
			if (!string.IsNullOrEmpty(fromHtmlMarkup))
			{
				stringBuilder.Append("<B>");
				stringBuilder.Append(fromLabel);
				stringBuilder.Append("</B> ");
				stringBuilder.Append(fromHtmlMarkup);
				stringBuilder.Append("<BR>");
			}
			ExDateTime postedTime = item.PostedTime;
			stringBuilder.Append("<B>");
			stringBuilder.Append(ForwardReplyUtilities.ClientsResourceManager.GetString("PostedOnColon", culture));
			stringBuilder.Append("</B> ");
			stringBuilder.AppendFormat(ForwardReplyUtilities.ClientsResourceManager.GetString("SentTime", culture), item.PostedTime.ToLongDateString(), string.IsNullOrWhiteSpace(timeFormat) ? item.PostedTime.ToLongTimeString() : item.PostedTime.ToString(timeFormat));
			stringBuilder.Append("<BR>");
			stringBuilder.Append("<B>");
			stringBuilder.Append(ForwardReplyUtilities.ClientsResourceManager.GetString("PostedToColon", culture));
			stringBuilder.Append("</B> ");
			string s = null;
			using (Folder folder = Folder.Bind(item.Session, item.ParentId, null))
			{
				s = folder.DisplayName;
			}
			ForwardReplyUtilities.HtmlEncode(s, stringBuilder);
			stringBuilder.Append("<BR>");
			stringBuilder.Append("<B>");
			stringBuilder.Append(ForwardReplyUtilities.ClientsResourceManager.GetString("ConversationColon", culture));
			stringBuilder.Append("</B> ");
			ForwardReplyUtilities.HtmlEncode(item.ConversationTopic, stringBuilder);
			stringBuilder.Append("<BR>");
			stringBuilder.Append("</FONT><BR></DIV>");
			stringBuilder.Append("</DIV>");
			return stringBuilder.ToString();
		}

		// Token: 0x0600156B RID: 5483 RVA: 0x0006E3A8 File Offset: 0x0006C5A8
		private static string CreatePostTextReplyForwardHeader(PostItem item, ForwardReplyHeaderOptions options, string fromLabel, string from, CultureInfo culture, string timeFormat)
		{
			StringBuilder stringBuilder = new StringBuilder(100);
			if (!string.IsNullOrEmpty(options.SignatureText))
			{
				stringBuilder.Append(options.SignatureText);
			}
			stringBuilder.Append("\n________________________________________\n");
			if (!string.IsNullOrEmpty(from))
			{
				stringBuilder.Append(fromLabel);
				stringBuilder.Append(" ");
				stringBuilder.Append(from);
				stringBuilder.Append("\n");
			}
			item.TryGetProperty(ItemSchema.SentTime);
			ExDateTime postedTime = item.PostedTime;
			stringBuilder.Append(ForwardReplyUtilities.ClientsResourceManager.GetString("PostedOnColon", culture));
			stringBuilder.Append(" ");
			stringBuilder.AppendFormat(ForwardReplyUtilities.ClientsResourceManager.GetString("SentTime", culture), item.PostedTime.ToLongDateString(), string.IsNullOrWhiteSpace(timeFormat) ? item.PostedTime.ToLongTimeString() : item.PostedTime.ToString(timeFormat));
			stringBuilder.Append("\n");
			stringBuilder.Append(ForwardReplyUtilities.ClientsResourceManager.GetString("PostedToColon", culture));
			stringBuilder.Append(" ");
			using (Folder folder = Folder.Bind(item.Session, item.ParentId, null))
			{
				stringBuilder.Append(folder.DisplayName);
			}
			stringBuilder.Append("\n");
			stringBuilder.Append(ForwardReplyUtilities.ClientsResourceManager.GetString("ConversationColon", culture));
			stringBuilder.Append(" ");
			string value = null;
			if (item.Subject != null)
			{
				value = item.ConversationTopic;
			}
			stringBuilder.Append(value);
			stringBuilder.Append("\n");
			return stringBuilder.ToString();
		}

		// Token: 0x0600156C RID: 5484 RVA: 0x0006E564 File Offset: 0x0006C764
		private static IList<IRecipientBase> GetMessageRecipientCollection(RecipientItemType type, MessageItem item)
		{
			IList<IRecipientBase> list = new List<IRecipientBase>();
			foreach (Recipient recipient in item.Recipients)
			{
				if (recipient.RecipientItemType == type)
				{
					list.Add(recipient);
				}
			}
			return list;
		}

		// Token: 0x0600156D RID: 5485 RVA: 0x0006E5C4 File Offset: 0x0006C7C4
		private static IList<IRecipientBase> GetCalendarItemRecipientCollection(AttendeeType type, CalendarItemBase item)
		{
			IList<IRecipientBase> list = new List<IRecipientBase>();
			for (int i = 0; i < item.AttendeeCollection.Count; i++)
			{
				Attendee attendee = item.AttendeeCollection[i];
				if (attendee.AttendeeType == type)
				{
					list.Add(attendee);
				}
			}
			return list;
		}

		// Token: 0x0600156E RID: 5486 RVA: 0x0006E60C File Offset: 0x0006C80C
		private static void RenderDefaultUserFontMarkup(ForwardReplyHeaderOptions headerOptions, TextWriter writer)
		{
			if (headerOptions.ComposeFontBold)
			{
				writer.Write("<strong>");
			}
			if (headerOptions.ComposeFontItalics)
			{
				writer.Write("<em>");
			}
			if (headerOptions.ComposeFontUnderline)
			{
				writer.Write("<u>");
			}
			writer.Write("<div><font face=\"");
			writer.Write(headerOptions.ComposeFontName);
			writer.Write("\" color=\"");
			writer.Write(headerOptions.ComposeFontColor);
			writer.Write("\" size=\"");
			writer.Write(headerOptions.ComposeFontSize);
			writer.Write("\">&nbsp;</font></div>");
			if (headerOptions.ComposeFontUnderline)
			{
				writer.Write("</u>");
			}
			if (headerOptions.ComposeFontItalics)
			{
				writer.Write("</em>");
			}
			if (headerOptions.ComposeFontBold)
			{
				writer.Write("</strong>");
			}
		}

		// Token: 0x0600156F RID: 5487 RVA: 0x0006E6DB File Offset: 0x0006C8DB
		private static void HtmlEncode(string s, StringBuilder stringBuilder)
		{
			stringBuilder.Append(WebUtility.HtmlEncode(s));
		}

		// Token: 0x06001570 RID: 5488 RVA: 0x0006E6EA File Offset: 0x0006C8EA
		private static string GenerateWhen(Item item)
		{
			if (item is MeetingMessage)
			{
				return (item as MeetingMessage).GenerateWhen(item.Session.PreferedCulture);
			}
			if (item is CalendarItemBase)
			{
				return (item as CalendarItemBase).GenerateWhen();
			}
			throw new ArgumentException("Unsupported type, this is a bug");
		}

		// Token: 0x06001571 RID: 5489 RVA: 0x0006E72C File Offset: 0x0006C92C
		internal static string GetParticipantDisplayString(Participant participant, bool outputHtml)
		{
			StringBuilder stringBuilder = new StringBuilder();
			ForwardReplyUtilities.AppendParticipantDisplayString(participant, stringBuilder, outputHtml);
			return stringBuilder.ToString();
		}

		// Token: 0x06001572 RID: 5490 RVA: 0x0006E750 File Offset: 0x0006C950
		internal static void AppendParticipantDisplayString(Participant participant, StringBuilder stringBuilder, bool outputHtml)
		{
			if (participant.DisplayName != null)
			{
				if (outputHtml)
				{
					ForwardReplyUtilities.HtmlEncode(participant.DisplayName, stringBuilder);
				}
				else
				{
					stringBuilder.Append(participant.DisplayName);
				}
			}
			bool flag = false;
			string text = string.Empty;
			if (participant.RoutingType != null && string.CompareOrdinal(participant.RoutingType, "SMTP") == 0 && participant.EmailAddress != null)
			{
				flag = true;
				text = participant.EmailAddress;
			}
			if (flag)
			{
				if (outputHtml)
				{
					stringBuilder.Append(" [");
					ForwardReplyUtilities.HtmlEncode(text, stringBuilder);
					stringBuilder.Append("]");
					return;
				}
				stringBuilder.Append(" [").Append(text).Append("]");
			}
		}

		// Token: 0x06001573 RID: 5491 RVA: 0x0006E7F8 File Offset: 0x0006C9F8
		private static bool IsRightToLeft(CultureInfo culture)
		{
			return culture.TextInfo.IsRightToLeft;
		}

		// Token: 0x04000E8B RID: 3723
		private const string MessageDelimiter = "\n________________________________________\n";

		// Token: 0x04000E8C RID: 3724
		private const string ExternalFromFormatString = "{0} <{1}>";

		// Token: 0x04000E8D RID: 3725
		private const string ClientsResourceBaseName = "Microsoft.Exchange.Clients.Strings";

		// Token: 0x04000E8E RID: 3726
		private const string ClientsResoucesAssemblyName = "Microsoft.Exchange.Clients.Strings";

		// Token: 0x04000E8F RID: 3727
		private const string FromColon = "FromColon";

		// Token: 0x04000E90 RID: 3728
		private const string ToColon = "ToColon";

		// Token: 0x04000E91 RID: 3729
		private const string CcColon = "CcColon";

		// Token: 0x04000E92 RID: 3730
		private const string OnBehalfOf = "OnBehalfOf";

		// Token: 0x04000E93 RID: 3731
		private const string SentColon = "SentColon";

		// Token: 0x04000E94 RID: 3732
		private const string SentTime = "SentTime";

		// Token: 0x04000E95 RID: 3733
		private const string SubjectColon = "SubjectColon";

		// Token: 0x04000E96 RID: 3734
		private const string WhenColon = "WhenColon";

		// Token: 0x04000E97 RID: 3735
		private const string WhereColon = "WhereColon";

		// Token: 0x04000E98 RID: 3736
		private const string PostedOnColon = "PostedOnColon";

		// Token: 0x04000E99 RID: 3737
		private const string PostedToColon = "PostedToColon";

		// Token: 0x04000E9A RID: 3738
		private const string ConversationColon = "ConversationColon";

		// Token: 0x04000E9B RID: 3739
		private static ResourceManager resourceManager;
	}
}
