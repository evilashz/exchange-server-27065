using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Microsoft.Exchange.Clients.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Security;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000234 RID: 564
	internal static class ReplyForwardUtilities
	{
		// Token: 0x060012F2 RID: 4850 RVA: 0x00072448 File Offset: 0x00070648
		public static BodyFormat GetReplyForwardBodyFormat(Item item, UserContext userContext)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			Body body = item.Body;
			if (!userContext.IsBasicExperience && userContext.IsIrmEnabled && Utilities.IsIrmDecrypted(item))
			{
				body = ((RightsManagedMessageItem)item).ProtectedBody;
			}
			if (body.Format == BodyFormat.TextPlain)
			{
				return BodyFormat.TextPlain;
			}
			return BodyFormat.TextHtml;
		}

		// Token: 0x060012F3 RID: 4851 RVA: 0x000724A8 File Offset: 0x000706A8
		internal static string CreateReplyForwardHeader(BodyFormat bodyFormat, Item item, UserContext userContext, StoreObjectId parentFolderId)
		{
			MessageItem messageItem = item as MessageItem;
			PostItem postItem = item as PostItem;
			CalendarItemBase calendarItemBase = item as CalendarItemBase;
			if (messageItem == null && calendarItemBase == null && postItem == null)
			{
				throw new ArgumentException("HTML reply forward headers can only be created for MessageItem and CalendarItemBase or PostItem. ");
			}
			if (postItem != null)
			{
				if (parentFolderId == null)
				{
					throw new ArgumentNullException("parentFolderId");
				}
				string parentFolderName = Utilities.GetParentFolderName(item, parentFolderId, userContext);
				return ReplyForwardUtilities.CreatePostReplyForwardHeader(bodyFormat, item, userContext, parentFolderName);
			}
			else
			{
				IList<IRecipientBase> toRecipients = null;
				IList<IRecipientBase> ccRecipients = null;
				string fromLabel = string.Empty;
				StringBuilder stringBuilder = new StringBuilder();
				string toLabel = string.Empty;
				string ccLabel = string.Empty;
				bool flag = bodyFormat == BodyFormat.TextHtml;
				if (messageItem != null)
				{
					fromLabel = (flag ? LocalizedStrings.GetHtmlEncoded(-1376223345) : LocalizedStrings.GetNonEncoded(-1376223345));
					if (messageItem.Sender != null)
					{
						if (Utilities.IsOnBehalfOf(messageItem.Sender, messageItem.From))
						{
							stringBuilder.Append(string.Format(LocalizedStrings.GetHtmlEncoded(-1426120402), ReplyForwardUtilities.GetParticipantDisplayString(messageItem.Sender, flag), ReplyForwardUtilities.GetParticipantDisplayString(messageItem.From, flag)));
						}
						else
						{
							ReplyForwardUtilities.AppendParticipantDisplayString(messageItem.Sender, stringBuilder, flag);
						}
					}
					toLabel = (flag ? LocalizedStrings.GetHtmlEncoded(-829627742) : LocalizedStrings.GetNonEncoded(-829627742));
					ccLabel = (flag ? LocalizedStrings.GetHtmlEncoded(-798075995) : LocalizedStrings.GetNonEncoded(-798075995));
					toRecipients = ReplyForwardUtilities.GetMessageRecipientCollection(RecipientItemType.To, messageItem);
					ccRecipients = ReplyForwardUtilities.GetMessageRecipientCollection(RecipientItemType.Cc, messageItem);
				}
				else if (calendarItemBase != null)
				{
					fromLabel = (flag ? LocalizedStrings.GetHtmlEncoded(-1376223345) : LocalizedStrings.GetNonEncoded(-1376223345));
					if (calendarItemBase.Organizer != null)
					{
						ReplyForwardUtilities.AppendParticipantDisplayString(calendarItemBase.Organizer, stringBuilder, flag);
					}
					toLabel = (flag ? LocalizedStrings.GetHtmlEncoded(-1709254790) : LocalizedStrings.GetNonEncoded(-1709254790));
					ccLabel = (flag ? LocalizedStrings.GetHtmlEncoded(-98673561) : LocalizedStrings.GetNonEncoded(-98673561));
					toRecipients = ReplyForwardUtilities.GetCalendarItemRecipientCollection(AttendeeType.Required, calendarItemBase);
					ccRecipients = ReplyForwardUtilities.GetCalendarItemRecipientCollection(AttendeeType.Optional, calendarItemBase);
				}
				switch (bodyFormat)
				{
				case BodyFormat.TextPlain:
					return ReplyForwardUtilities.CreateTextReplyForwardHeader(item, userContext, fromLabel, stringBuilder.ToString(), toLabel, ccLabel, toRecipients, ccRecipients);
				case BodyFormat.TextHtml:
					return ReplyForwardUtilities.CreateHtmlReplyForwardHeader(item, userContext, fromLabel, stringBuilder.ToString(), toLabel, ccLabel, toRecipients, ccRecipients);
				default:
					throw new ArgumentException("Unsupported body format");
				}
			}
		}

		// Token: 0x060012F4 RID: 4852 RVA: 0x000726DC File Offset: 0x000708DC
		private static string CreatePostReplyForwardHeader(BodyFormat bodyFormat, Item item, UserContext userContext, string postToFolderName)
		{
			PostItem postItem = item as PostItem;
			if (postItem == null)
			{
				throw new ArgumentException("OWA logic error . CreatePostReplyForwardheader is called on a non-PostItem item.");
			}
			if (postToFolderName == null)
			{
				throw new ArgumentNullException("postToFolderName");
			}
			if (string.Empty == postToFolderName)
			{
				throw new ArgumentException("postToFolderName should not be empty. ");
			}
			StringBuilder stringBuilder = new StringBuilder();
			bool outputHtml = BodyFormat.TextHtml == bodyFormat;
			if (postItem.Sender != null)
			{
				if (Utilities.IsOnBehalfOf(postItem.Sender, postItem.From))
				{
					stringBuilder.Append(string.Format(LocalizedStrings.GetHtmlEncoded(-1426120402), ReplyForwardUtilities.GetParticipantDisplayString(postItem.Sender, outputHtml), ReplyForwardUtilities.GetParticipantDisplayString(postItem.From, outputHtml)));
				}
				else
				{
					ReplyForwardUtilities.AppendParticipantDisplayString(postItem.Sender, stringBuilder, outputHtml);
				}
			}
			string fromLabel = string.Empty;
			switch (bodyFormat)
			{
			case BodyFormat.TextPlain:
				fromLabel = LocalizedStrings.GetNonEncoded(-1376223345);
				return ReplyForwardUtilities.CreatePostTextReplyForwardHeader(postItem, userContext, fromLabel, stringBuilder.ToString(), postToFolderName);
			case BodyFormat.TextHtml:
				fromLabel = LocalizedStrings.GetHtmlEncoded(-1376223345);
				return ReplyForwardUtilities.CreatePostHtmlReplyForwardHeader(postItem, userContext, fromLabel, stringBuilder.ToString(), postToFolderName);
			default:
				throw new ArgumentException("Unsupported body format");
			}
		}

		// Token: 0x060012F5 RID: 4853 RVA: 0x000727EC File Offset: 0x000709EC
		private static string CreatePostHtmlReplyForwardHeader(PostItem item, UserContext userContext, string fromLabel, string fromHtmlMarkup, string postToFolderName)
		{
			StringBuilder stringBuilder = new StringBuilder(150);
			stringBuilder.Append("<HR tabindex=\"-1\">");
			stringBuilder.Append("<DIV id=divRplyFwdMsg>");
			stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "<FONT FACE=\"{0}\" size=\"2\">", new object[]
			{
				Utilities.GetDefaultFontName()
			});
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
			stringBuilder.Append(LocalizedStrings.GetHtmlEncoded(1890363921));
			stringBuilder.Append("</B> ");
			stringBuilder.AppendFormat(Utilities.HtmlEncode(Strings.SentTime), item.PostedTime.ToLongDateString(), item.PostedTime.ToString(userContext.UserOptions.TimeFormat));
			stringBuilder.Append("<BR>");
			stringBuilder.Append("<B>");
			stringBuilder.Append(LocalizedStrings.GetHtmlEncoded(737701919));
			stringBuilder.Append("</B> ");
			Utilities.HtmlEncode(postToFolderName, stringBuilder);
			stringBuilder.Append("<BR>");
			stringBuilder.Append("<B>");
			stringBuilder.Append(LocalizedStrings.GetHtmlEncoded(-1239615092));
			stringBuilder.Append("</B> ");
			Utilities.HtmlEncode(item.ConversationTopic, stringBuilder);
			stringBuilder.Append("<BR>");
			stringBuilder.Append("</FONT><BR></DIV>");
			stringBuilder.Append("</DIV>");
			return stringBuilder.ToString();
		}

		// Token: 0x060012F6 RID: 4854 RVA: 0x0007298C File Offset: 0x00070B8C
		private static string CreateHtmlReplyForwardHeader(Item item, UserContext userContext, string fromLabel, string fromHtmlMarkup, string toLabel, string ccLabel, IList<IRecipientBase> toRecipients, IList<IRecipientBase> ccRecipients)
		{
			StringBuilder stringBuilder = new StringBuilder(150);
			stringBuilder.Append("<HR tabindex=\"-1\">");
			stringBuilder.Append("<DIV id=divRplyFwdMsg>");
			stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "<FONT FACE=\"{0}\" size=\"2\" color=\"#000000\">", new object[]
			{
				Utilities.GetDefaultFontName()
			});
			if (!string.IsNullOrEmpty(fromHtmlMarkup))
			{
				stringBuilder.Append("<B>");
				stringBuilder.Append(fromLabel);
				stringBuilder.Append("</B> ");
				stringBuilder.Append(fromHtmlMarkup);
				stringBuilder.Append("<BR>");
			}
			object obj = item.TryGetProperty(ItemSchema.SentTime);
			if (obj != null && obj is ExDateTime)
			{
				ExDateTime exDateTime = (ExDateTime)obj;
				stringBuilder.Append("<B>");
				stringBuilder.Append(LocalizedStrings.GetHtmlEncoded(295620541));
				stringBuilder.Append("</B> ");
				stringBuilder.AppendFormat(Utilities.HtmlEncode(Strings.SentTime), exDateTime.ToLongDateString(), exDateTime.ToString(userContext.UserOptions.TimeFormat));
				stringBuilder.Append("<BR>");
			}
			if (0 < toRecipients.Count)
			{
				stringBuilder.Append("<B>");
				stringBuilder.Append(toLabel);
				stringBuilder.Append("</B> ");
				int num = 0;
				foreach (IRecipientBase recipientBase in toRecipients)
				{
					num++;
					Utilities.HtmlEncode(recipientBase.Participant.DisplayName, stringBuilder);
					if (num < toRecipients.Count)
					{
						stringBuilder.Append("; ");
					}
				}
				stringBuilder.Append("<BR>");
			}
			if (0 < ccRecipients.Count)
			{
				stringBuilder.Append("<B>");
				stringBuilder.Append(ccLabel);
				stringBuilder.Append("</B> ");
				int num2 = 0;
				foreach (IRecipientBase recipientBase2 in ccRecipients)
				{
					num2++;
					Utilities.HtmlEncode(recipientBase2.Participant.DisplayName, stringBuilder);
					if (num2 < ccRecipients.Count)
					{
						stringBuilder.Append("; ");
					}
				}
				stringBuilder.Append("<BR>");
			}
			stringBuilder.Append("<B>");
			stringBuilder.Append(LocalizedStrings.GetHtmlEncoded(-881075747));
			stringBuilder.Append("</B> ");
			string text = item.TryGetProperty(ItemSchema.Subject) as string;
			if (text == null)
			{
				text = string.Empty;
			}
			Utilities.HtmlEncode(text, stringBuilder);
			stringBuilder.Append("<BR>");
			if (ReplyForwardUtilities.IsMeetingItem(item))
			{
				stringBuilder.Append("<B>");
				stringBuilder.Append(LocalizedStrings.GetHtmlEncoded(-524211323));
				stringBuilder.Append("</B> ");
				Utilities.HtmlEncode(Utilities.GenerateWhen(item), stringBuilder);
				stringBuilder.Append("<BR><B>");
				stringBuilder.Append(LocalizedStrings.GetHtmlEncoded(1666265192));
				stringBuilder.Append("</B> ");
				string text2 = item.TryGetProperty(CalendarItemBaseSchema.Location) as string;
				if (!string.IsNullOrEmpty(text2))
				{
					Utilities.HtmlEncode(text2, stringBuilder);
				}
				stringBuilder.Append("<BR>");
			}
			stringBuilder.Append("</FONT><BR></DIV>");
			stringBuilder.Append("</DIV>");
			return stringBuilder.ToString();
		}

		// Token: 0x060012F7 RID: 4855 RVA: 0x00072CF0 File Offset: 0x00070EF0
		private static string CreatePostTextReplyForwardHeader(PostItem item, UserContext userContext, string fromLabel, string from, string postToFolderName)
		{
			StringBuilder stringBuilder = new StringBuilder(100);
			if (userContext.IsFeatureEnabled(Feature.Signature) && userContext.UserOptions.AutoAddSignature)
			{
				stringBuilder.Append(userContext.UserOptions.SignatureText);
			}
			stringBuilder.Append("\n");
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
			stringBuilder.Append(LocalizedStrings.GetNonEncoded(1890363921));
			stringBuilder.Append(" ");
			stringBuilder.AppendFormat(Strings.SentTime, item.PostedTime.ToLongDateString(), item.PostedTime.ToString(userContext.UserOptions.TimeFormat));
			stringBuilder.Append("\n");
			stringBuilder.Append(LocalizedStrings.GetNonEncoded(737701919));
			stringBuilder.Append(" ");
			stringBuilder.Append(postToFolderName);
			stringBuilder.Append("\n");
			stringBuilder.Append(LocalizedStrings.GetNonEncoded(-1239615092));
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

		// Token: 0x060012F8 RID: 4856 RVA: 0x00072E64 File Offset: 0x00071064
		private static string CreateTextReplyForwardHeader(Item item, UserContext userContext, string fromLabel, string from, string toLabel, string ccLabel, IList<IRecipientBase> toRecipients, IList<IRecipientBase> ccRecipients)
		{
			StringBuilder stringBuilder = new StringBuilder(100);
			if (userContext.IsFeatureEnabled(Feature.Signature) && userContext.UserOptions.AutoAddSignature)
			{
				stringBuilder.Append("\n");
				stringBuilder.Append(userContext.UserOptions.SignatureText);
			}
			stringBuilder.Append("\n________________________________________\n");
			if (!string.IsNullOrEmpty(from))
			{
				stringBuilder.Append(fromLabel);
				stringBuilder.Append(" ");
				stringBuilder.Append(from);
				stringBuilder.Append("\n");
			}
			object obj = item.TryGetProperty(ItemSchema.SentTime);
			if (obj != null && obj is ExDateTime)
			{
				ExDateTime exDateTime = (ExDateTime)obj;
				stringBuilder.Append(LocalizedStrings.GetNonEncoded(295620541));
				stringBuilder.Append(" ");
				stringBuilder.AppendFormat(Strings.SentTime, exDateTime.ToLongDateString(), exDateTime.ToString(userContext.UserOptions.TimeFormat));
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
			stringBuilder.Append(LocalizedStrings.GetNonEncoded(-881075747));
			stringBuilder.Append(" ");
			string text = item.TryGetProperty(ItemSchema.Subject) as string;
			if (text == null)
			{
				text = string.Empty;
			}
			stringBuilder.Append(text);
			stringBuilder.Append("\n");
			if (ReplyForwardUtilities.IsMeetingItem(item))
			{
				stringBuilder.Append(LocalizedStrings.GetNonEncoded(-524211323));
				stringBuilder.Append(" ");
				stringBuilder.Append(Utilities.GenerateWhen(item));
				stringBuilder.Append("\n");
				stringBuilder.Append(LocalizedStrings.GetNonEncoded(1666265192));
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

		// Token: 0x060012F9 RID: 4857 RVA: 0x00073168 File Offset: 0x00071368
		public static Item CreatePostReplyItem(BodyFormat bodyFormat, PostItem item, UserContext userContext, StoreObjectId parentFolderId)
		{
			PostItem postItem = null;
			bool flag = true;
			try
			{
				string bodyPrefix = ReplyForwardUtilities.CreateReplyForwardHeader(bodyFormat, item, userContext, parentFolderId);
				string legacyDN = null;
				OwaStoreObjectIdType owaStoreObjectIdType = Utilities.GetOwaStoreObjectIdType(userContext, item);
				if (owaStoreObjectIdType == OwaStoreObjectIdType.OtherUserMailboxObject)
				{
					legacyDN = Utilities.GetMailboxSessionLegacyDN(item);
				}
				OwaStoreObjectId destinationFolderId = OwaStoreObjectId.CreateFromFolderId(parentFolderId, owaStoreObjectIdType, legacyDN);
				OwaStoreObjectId scratchPadForImplicitDraft = Utilities.GetScratchPadForImplicitDraft(StoreObjectType.Post, destinationFolderId);
				try
				{
					ReplyForwardConfiguration replyForwardConfiguration = new ReplyForwardConfiguration(bodyFormat);
					replyForwardConfiguration.ConversionOptionsForSmime = Utilities.CreateInboundConversionOptions(userContext);
					replyForwardConfiguration.AddBodyPrefix(bodyPrefix);
					postItem = item.ReplyToFolder(scratchPadForImplicitDraft.GetSession(userContext), scratchPadForImplicitDraft.StoreObjectId, replyForwardConfiguration);
				}
				catch (StoragePermanentException ex)
				{
					if (ex.InnerException is MapiExceptionNoCreateRight)
					{
						throw new OwaAccessDeniedException(LocalizedStrings.GetNonEncoded(995407892), ex);
					}
					throw ex;
				}
				ReplyForwardUtilities.CopyMessageClassificationProperties(item, postItem, userContext);
				Utilities.SetPostSender(postItem, userContext, Utilities.IsPublic(item));
				postItem.ConversationTopic = item.ConversationTopic;
				flag = false;
			}
			finally
			{
				if (flag && postItem != null)
				{
					postItem.Dispose();
					postItem = null;
				}
			}
			return postItem;
		}

		// Token: 0x060012FA RID: 4858 RVA: 0x00073260 File Offset: 0x00071460
		public static Item CreateReplyItem(BodyFormat bodyFormat, Item item, ReplyForwardFlags flags, UserContext userContext, StoreObjectId parentFolderId)
		{
			return ReplyForwardUtilities.CreateReplyOrReplyAllItem(bodyFormat, item, flags, false, userContext, parentFolderId);
		}

		// Token: 0x060012FB RID: 4859 RVA: 0x0007326E File Offset: 0x0007146E
		public static Item CreateReplyAllItem(BodyFormat bodyFormat, Item item, ReplyForwardFlags flags, UserContext userContext, StoreObjectId parentFolderId)
		{
			return ReplyForwardUtilities.CreateReplyOrReplyAllItem(bodyFormat, item, flags, true, userContext, parentFolderId);
		}

		// Token: 0x060012FC RID: 4860 RVA: 0x0007327C File Offset: 0x0007147C
		internal static void SetAlternateIrmBody(Item item, BodyFormat bodyFormat, UserContext userContext, StoreObjectId parentFolderId, RightsManagedMessageDecryptionStatus decryptionStatus, bool isProtectedVoicemail)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			if (parentFolderId == null)
			{
				throw new ArgumentNullException("parentFolderId");
			}
			if (!decryptionStatus.Failed)
			{
				return;
			}
			string text = ReplyForwardUtilities.CreateReplyForwardHeader(bodyFormat, item, userContext, parentFolderId);
			text += Utilities.GetAlternateBodyForIrm(userContext, bodyFormat, decryptionStatus, isProtectedVoicemail);
			ItemUtility.SetItemBody(item, bodyFormat, text);
		}

		// Token: 0x060012FD RID: 4861 RVA: 0x000732E4 File Offset: 0x000714E4
		private static Item CreateReplyOrReplyAllItem(BodyFormat bodyFormat, Item item, ReplyForwardFlags flags, bool replyAll, UserContext userContext, StoreObjectId parentFolderId)
		{
			if (bodyFormat < (BodyFormat)0)
			{
				throw new ArgumentOutOfRangeException("bodyFormat", bodyFormat, "bodyFormat has an invalid value");
			}
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			MessageItem messageItem = null;
			bool flag = true;
			try
			{
				bool flag2 = true;
				string text = string.Empty;
				if (!Utilities.IsFlagSet((int)flags, 2))
				{
					text = ReplyForwardUtilities.CreateReplyForwardHeader(bodyFormat, item, userContext, parentFolderId);
				}
				ReplyForwardConfiguration replyForwardConfiguration = new ReplyForwardConfiguration(bodyFormat);
				replyForwardConfiguration.ConversionOptionsForSmime = Utilities.CreateInboundConversionOptions(userContext);
				replyForwardConfiguration.AddBodyPrefix(text);
				MessageItem messageItem2;
				CalendarItemBase calendarItemBase;
				PostItem postItem;
				if ((messageItem2 = (item as MessageItem)) != null)
				{
					if (replyAll)
					{
						messageItem = messageItem2.CreateReplyAll(userContext.MailboxSession, userContext.DraftsFolderId, replyForwardConfiguration);
					}
					else
					{
						messageItem = messageItem2.CreateReply(userContext.MailboxSession, userContext.DraftsFolderId, replyForwardConfiguration);
					}
					ReplyForwardUtilities.SmartReplyOrForward(messageItem2, messageItem, userContext);
					ReplyForwardUtilities.CopyMessageClassificationProperties(item, messageItem, userContext);
				}
				else if ((calendarItemBase = (item as CalendarItemBase)) != null)
				{
					if (calendarItemBase.IsMeeting)
					{
						if (replyAll)
						{
							messageItem = calendarItemBase.CreateReplyAll(userContext.MailboxSession, userContext.DraftsFolderId, replyForwardConfiguration);
						}
						else
						{
							messageItem = calendarItemBase.CreateReply(userContext.MailboxSession, userContext.DraftsFolderId, replyForwardConfiguration);
						}
					}
					else
					{
						flag2 = false;
					}
				}
				else if ((postItem = (item as PostItem)) != null)
				{
					if (replyAll)
					{
						messageItem = postItem.CreateReplyAll(userContext.MailboxSession, userContext.DraftsFolderId, replyForwardConfiguration);
					}
					else
					{
						messageItem = postItem.CreateReply(userContext.MailboxSession, userContext.DraftsFolderId, replyForwardConfiguration);
					}
				}
				else
				{
					flag2 = false;
				}
				if (!flag2)
				{
					throw new OwaOperationNotSupportedException(LocalizedStrings.GetNonEncoded(293574673));
				}
				if (Globals.ArePerfCountersEnabled)
				{
					OwaSingleCounters.ItemsCreated.Increment();
				}
				if (Utilities.IsFlagSet((int)flags, 1))
				{
					ItemUtility.SetItemBody(messageItem, bodyFormat, text);
				}
				flag = false;
			}
			catch (NotSupportedException ex)
			{
				ExTraceGlobals.CoreTracer.TraceDebug<string>(0L, "Unable to create a Reply or ReplyAll item. Exception {0}", ex.Message);
				throw new OwaInvalidRequestException("Unable to create a Reply or ReplyAll item.");
			}
			finally
			{
				if (flag && messageItem != null)
				{
					messageItem.Dispose();
					messageItem = null;
				}
			}
			return messageItem;
		}

		// Token: 0x060012FE RID: 4862 RVA: 0x00073500 File Offset: 0x00071700
		public static Item CreateForwardItem(BodyFormat bodyFormat, Item item, ReplyForwardFlags flags, UserContext userContext, StoreObjectId parentFolderId)
		{
			if (bodyFormat < (BodyFormat)0)
			{
				throw new ArgumentOutOfRangeException("bodyFormat", bodyFormat, "bodyFormat has an invalid value");
			}
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			MessageItem messageItem = null;
			bool flag = true;
			try
			{
				string text = string.Empty;
				if (!Utilities.IsFlagSet((int)flags, 2))
				{
					text = ReplyForwardUtilities.CreateReplyForwardHeader(bodyFormat, item, userContext, parentFolderId);
				}
				ReplyForwardConfiguration replyForwardConfiguration = new ReplyForwardConfiguration(bodyFormat);
				replyForwardConfiguration.ConversionOptionsForSmime = Utilities.CreateInboundConversionOptions(userContext);
				replyForwardConfiguration.AddBodyPrefix(text);
				MessageItem messageItem2;
				CalendarItemBase calendarItemBase;
				if ((messageItem2 = (item as MessageItem)) != null)
				{
					messageItem = messageItem2.CreateForward(userContext.MailboxSession, userContext.DraftsFolderId, replyForwardConfiguration);
					if (!(messageItem2 is MeetingMessage))
					{
						ReplyForwardUtilities.CopyMessageClassificationProperties(item, messageItem, userContext);
					}
					ReplyForwardUtilities.SmartReplyOrForward(messageItem2, messageItem, userContext);
				}
				else if ((calendarItemBase = (item as CalendarItemBase)) != null)
				{
					messageItem = calendarItemBase.CreateForward(userContext.MailboxSession, userContext.DraftsFolderId, replyForwardConfiguration, null, null);
				}
				else
				{
					PostItem postItem;
					if ((postItem = (item as PostItem)) == null)
					{
						throw new ArgumentException("HTML reply forward headers can only be created for MessageItem or CalendarItemBase or PostItem. ");
					}
					messageItem = postItem.CreateForward(userContext.MailboxSession, userContext.DraftsFolderId, replyForwardConfiguration);
				}
				if (Globals.ArePerfCountersEnabled)
				{
					OwaSingleCounters.ItemsCreated.Increment();
				}
				if (Utilities.IsFlagSet((int)flags, 1))
				{
					ItemUtility.SetItemBody(messageItem, bodyFormat, text);
				}
				flag = false;
			}
			finally
			{
				if (flag && messageItem != null)
				{
					messageItem.Dispose();
					messageItem = null;
				}
			}
			return messageItem;
		}

		// Token: 0x060012FF RID: 4863 RVA: 0x00073664 File Offset: 0x00071864
		public static MessageItem CreateForwardMessageWithItemAttached(Item itemToAttach, UserContext userContext)
		{
			MessageItem messageItem = MessageItem.Create(userContext.MailboxSession, userContext.DraftsFolderId);
			messageItem[ItemSchema.ConversationIndexTracking] = true;
			string property;
			if (itemToAttach is Contact)
			{
				property = ItemUtility.GetProperty<string>(itemToAttach, StoreObjectSchema.DisplayName, string.Empty);
			}
			else if (itemToAttach is DistributionList)
			{
				property = ItemUtility.GetProperty<string>(itemToAttach, StoreObjectSchema.DisplayName, string.Empty);
			}
			else
			{
				property = ItemUtility.GetProperty<string>(itemToAttach, ItemSchema.Subject, string.Empty);
			}
			if (property.Length <= 255)
			{
				messageItem[ItemSchema.Subject] = property;
			}
			else
			{
				messageItem[ItemSchema.Subject] = property.Substring(0, 255);
			}
			using (ItemAttachment itemAttachment = messageItem.AttachmentCollection.AddExistingItem(itemToAttach))
			{
				itemAttachment.Save();
			}
			if (Globals.ArePerfCountersEnabled)
			{
				OwaSingleCounters.ItemsCreated.Increment();
			}
			return messageItem;
		}

		// Token: 0x06001300 RID: 4864 RVA: 0x00073750 File Offset: 0x00071950
		public static void DeleteLevelOneAttachments(Item newItem, UserContext userContext)
		{
			if (newItem == null)
			{
				throw new ArgumentNullException("newItem");
			}
			List<AttachmentId> list = new List<AttachmentId>();
			foreach (AttachmentHandle handle in newItem.AttachmentCollection)
			{
				using (Attachment attachment = newItem.AttachmentCollection.Open(handle))
				{
					AttachmentPolicy.Level attachmentLevel = AttachmentLevelLookup.GetAttachmentLevel(attachment, userContext);
					if (attachmentLevel == AttachmentPolicy.Level.Block)
					{
						list.Add(attachment.Id);
					}
				}
			}
			if (list.Count != 0)
			{
				foreach (AttachmentId attachmentId in list)
				{
					newItem.AttachmentCollection.Remove(attachmentId);
				}
				ConflictResolutionResult conflictResolutionResult = newItem.Save(SaveMode.ResolveConflicts);
				if (conflictResolutionResult.SaveStatus == SaveResult.IrresolvableConflict)
				{
					newItem.Dispose();
					newItem = null;
					throw new OwaSaveConflictException(LocalizedStrings.GetNonEncoded(-482397486), conflictResolutionResult);
				}
			}
			newItem.Load();
		}

		// Token: 0x06001301 RID: 4865 RVA: 0x00073870 File Offset: 0x00071A70
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

		// Token: 0x06001302 RID: 4866 RVA: 0x000738D0 File Offset: 0x00071AD0
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

		// Token: 0x06001303 RID: 4867 RVA: 0x00073918 File Offset: 0x00071B18
		public static SanitizedHtmlString GetDefaultUserFontStyle(UserContext userContext)
		{
			UserOptions userOptions = userContext.UserOptions;
			SanitizingStringBuilder<OwaHtml> sanitizingStringBuilder = new SanitizingStringBuilder<OwaHtml>(256);
			FontFlags composeFontFlags = userOptions.ComposeFontFlags;
			sanitizingStringBuilder.Append("direction: ");
			sanitizingStringBuilder.Append(userContext.IsRtl ? "rtl" : "ltr");
			sanitizingStringBuilder.Append<char>(';');
			sanitizingStringBuilder.Append("font-family: ");
			sanitizingStringBuilder.Append<SanitizedJScriptString>(new SanitizedJScriptString(userOptions.ComposeFontName));
			sanitizingStringBuilder.Append<char>(';');
			sanitizingStringBuilder.Append("color: ");
			sanitizingStringBuilder.Append<SanitizedJScriptString>(new SanitizedJScriptString(userOptions.ComposeFontColor));
			sanitizingStringBuilder.Append<char>(';');
			sanitizingStringBuilder.Append("font-size: ");
			sanitizingStringBuilder.Append(ReplyForwardUtilities.fontSizeMapping[userOptions.ComposeFontSize]);
			sanitizingStringBuilder.Append<char>(';');
			return sanitizingStringBuilder.ToSanitizedString<SanitizedHtmlString>();
		}

		// Token: 0x06001304 RID: 4868 RVA: 0x000739E0 File Offset: 0x00071BE0
		public static Item GetItemForRequest(OwaContext owaContext, out Item parentItem)
		{
			PropertyDefinition[] prefetchProperties = new PropertyDefinition[]
			{
				ItemSchema.SentTime,
				ItemSchema.IsClassified,
				ItemSchema.Classification,
				ItemSchema.ClassificationDescription,
				ItemSchema.ClassificationGuid,
				ItemSchema.ClassificationKeep,
				ItemSchema.BlockStatus,
				MessageItemSchema.SharingInstanceGuid,
				MessageItemSchema.MessageBccMe
			};
			string type = owaContext.FormsRegistryContext.Type;
			if (ObjectClass.IsCalendarItemCalendarItemOccurrenceOrRecurrenceException(type))
			{
				return Utilities.GetItemForRequest<CalendarItemBase>(owaContext, out parentItem, prefetchProperties);
			}
			if (ObjectClass.IsMeetingRequest(type))
			{
				return Utilities.GetItemForRequest<MeetingRequest>(owaContext, out parentItem, prefetchProperties);
			}
			if (ObjectClass.IsMeetingCancellation(type))
			{
				return Utilities.GetItemForRequest<MeetingCancellation>(owaContext, out parentItem, prefetchProperties);
			}
			if (ObjectClass.IsMeetingResponse(type))
			{
				return Utilities.GetItemForRequest<MeetingResponse>(owaContext, out parentItem, prefetchProperties);
			}
			if (ObjectClass.IsMessage(type, false) && !ObjectClass.IsReport(type) && !ObjectClass.IsSmsMessage(type))
			{
				return Utilities.GetItemForRequest<MessageItem>(owaContext, out parentItem, prefetchProperties);
			}
			if (ObjectClass.IsContact(type))
			{
				return Utilities.GetItemForRequest<Contact>(owaContext, out parentItem, prefetchProperties);
			}
			if (ObjectClass.IsDistributionList(type))
			{
				return Utilities.GetItemForRequest<DistributionList>(owaContext, out parentItem, prefetchProperties);
			}
			if (ObjectClass.IsTask(type))
			{
				return Utilities.GetItemForRequest<Task>(owaContext, out parentItem, prefetchProperties);
			}
			if (ObjectClass.IsPost(type))
			{
				return Utilities.GetItemForRequest<PostItem>(owaContext, out parentItem, prefetchProperties);
			}
			return Utilities.GetItemForRequest<MessageItem>(owaContext, out parentItem, true, prefetchProperties);
		}

		// Token: 0x06001305 RID: 4869 RVA: 0x00073AFE File Offset: 0x00071CFE
		private static bool IsClassificationRetained(Item item)
		{
			return ItemUtility.GetProperty<bool>(item, ItemSchema.ClassificationKeep, false);
		}

		// Token: 0x06001306 RID: 4870 RVA: 0x00073B0C File Offset: 0x00071D0C
		private static void CopyMessageClassificationProperties(Item oldItem, Item newItem, UserContext userContext)
		{
			if (ReplyForwardUtilities.IsClassificationRetained(oldItem))
			{
				string property = ItemUtility.GetProperty<string>(oldItem, ItemSchema.ClassificationGuid, string.Empty);
				if (!string.IsNullOrEmpty(property))
				{
					Guid empty = Guid.Empty;
					if (GuidHelper.TryParseGuid(property, out empty) && userContext.ComplianceReader.MessageClassificationReader.LookupMessageClassification(empty, CultureInfo.CurrentUICulture) != null)
					{
						newItem[ItemSchema.ClassificationGuid] = property;
						bool property2 = ItemUtility.GetProperty<bool>(oldItem, ItemSchema.IsClassified, false);
						newItem[ItemSchema.IsClassified] = property2;
						newItem[ItemSchema.Classification] = ItemUtility.GetProperty<string>(oldItem, ItemSchema.Classification, string.Empty);
						newItem[ItemSchema.ClassificationDescription] = ItemUtility.GetProperty<string>(oldItem, ItemSchema.ClassificationDescription, string.Empty);
						newItem[ItemSchema.ClassificationKeep] = ItemUtility.GetProperty<bool>(oldItem, ItemSchema.ClassificationKeep, false);
					}
				}
			}
		}

		// Token: 0x06001307 RID: 4871 RVA: 0x00073BE8 File Offset: 0x00071DE8
		internal static string GetParticipantDisplayString(Participant participant, bool outputHtml)
		{
			if (participant == null)
			{
				throw new ArgumentNullException("participant");
			}
			StringBuilder stringBuilder = new StringBuilder();
			ReplyForwardUtilities.AppendParticipantDisplayString(participant, stringBuilder, outputHtml);
			return stringBuilder.ToString();
		}

		// Token: 0x06001308 RID: 4872 RVA: 0x00073C20 File Offset: 0x00071E20
		internal static void AppendParticipantDisplayString(Participant participant, StringBuilder stringBuilder, bool outputHtml)
		{
			if (participant == null)
			{
				throw new ArgumentNullException("participant");
			}
			if (stringBuilder == null)
			{
				throw new ArgumentNullException("stringBuilder");
			}
			if (participant.DisplayName != null)
			{
				if (outputHtml)
				{
					Utilities.HtmlEncode(participant.DisplayName, stringBuilder);
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
			else if (Utilities.IsCustomRoutingType(participant.EmailAddress, participant.RoutingType))
			{
				flag = true;
				text = participant.RoutingType + ": " + participant.EmailAddress;
			}
			if (flag)
			{
				UserContext userContext = UserContextManager.GetUserContext();
				if (outputHtml)
				{
					stringBuilder.Append(" ");
					if (userContext.IsRtl)
					{
						stringBuilder.Append("&#x200F;");
					}
					stringBuilder.Append("[");
					Utilities.HtmlEncode(text, stringBuilder);
					stringBuilder.Append("]");
					if (userContext.IsRtl)
					{
						stringBuilder.Append("&#x200F;");
						return;
					}
				}
				else
				{
					stringBuilder.Append(" [").Append(text).Append("]");
				}
			}
		}

		// Token: 0x06001309 RID: 4873 RVA: 0x00073D52 File Offset: 0x00071F52
		private static bool IsMeetingItem(Item item)
		{
			return item is MeetingMessage || item is CalendarItemBase;
		}

		// Token: 0x0600130A RID: 4874 RVA: 0x00073D68 File Offset: 0x00071F68
		public static void RemoveInvalidRecipientsFromSmsMessage(MessageItem message)
		{
			Dictionary<RecipientItemType, List<string>> dictionary = null;
			int i = message.Recipients.Count - 1;
			while (i >= 0)
			{
				Participant participant = message.Recipients[i].Participant;
				string routingType = participant.RoutingType;
				string emailAddress = participant.EmailAddress;
				if (string.Equals(routingType, "SMTP", StringComparison.OrdinalIgnoreCase) && ImceaAddress.IsImceaAddress(emailAddress))
				{
					Utilities.TryDecodeImceaAddress(participant.EmailAddress, ref routingType, ref emailAddress);
				}
				if (Utilities.IsMobileRoutingType(routingType))
				{
					if (string.IsNullOrEmpty(Utilities.NormalizePhoneNumber(emailAddress)))
					{
						goto IL_D3;
					}
				}
				else
				{
					if (string.Equals(routingType, "EX", StringComparison.OrdinalIgnoreCase))
					{
						if (dictionary == null)
						{
							dictionary = new Dictionary<RecipientItemType, List<string>>(3);
						}
						RecipientItemType recipientItemType = message.Recipients[i].RecipientItemType;
						if (!dictionary.ContainsKey(recipientItemType))
						{
							dictionary[recipientItemType] = new List<string>(message.Recipients.Count - 1);
						}
						dictionary[recipientItemType].Add(emailAddress);
						goto IL_D3;
					}
					goto IL_D3;
				}
				IL_DF:
				i--;
				continue;
				IL_D3:
				message.Recipients.RemoveAt(i);
				goto IL_DF;
			}
			if (dictionary == null || dictionary.Count == 0)
			{
				return;
			}
			IRecipientSession recipientSession = Utilities.CreateADRecipientSession(ConsistencyMode.IgnoreInvalid, UserContextManager.GetUserContext());
			foreach (KeyValuePair<RecipientItemType, List<string>> keyValuePair in dictionary)
			{
				Result<ADRawEntry>[] array = recipientSession.FindByLegacyExchangeDNs(keyValuePair.Value.ToArray(), new PropertyDefinition[]
				{
					ADRecipientSchema.DisplayName,
					ADOrgPersonSchema.MobilePhone
				});
				if (array != null && array.Length != 0)
				{
					foreach (Result<ADRawEntry> result in array)
					{
						if (result.Data != null)
						{
							string text = result.Data[ADRecipientSchema.DisplayName] as string;
							string text2 = Utilities.NormalizePhoneNumber(result.Data[ADOrgPersonSchema.MobilePhone] as string);
							if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(text2))
							{
								message.Recipients.Add(new Participant(text, text2, "MOBILE"), keyValuePair.Key);
							}
						}
					}
				}
			}
		}

		// Token: 0x0600130B RID: 4875 RVA: 0x00073FB4 File Offset: 0x000721B4
		private static void SmartReplyOrForward(MessageItem originalItem, MessageItem newItem, UserContext userContext)
		{
			if (!RecipientCache.RunGetCacheOperationUnderDefaultExceptionHandler(delegate
			{
				SubscriptionCache.GetCache(userContext);
			}, userContext.GetHashCode()))
			{
				return;
			}
			SendAddressDefaultSetting sendAddressDefaultSetting = new SendAddressDefaultSetting(userContext);
			if (sendAddressDefaultSetting.IsUserEmailAddress)
			{
				return;
			}
			SubscriptionCacheEntry subscriptionCacheEntry;
			if (userContext.SubscriptionCache.TryGetSendAsDefaultEntry(sendAddressDefaultSetting, out subscriptionCacheEntry))
			{
				newItem.From = new Participant(subscriptionCacheEntry.DisplayName, subscriptionCacheEntry.Address, "SMTP");
				return;
			}
			try
			{
				object obj = originalItem.TryGetProperty(MessageItemSchema.SharingInstanceGuid);
				SubscriptionCacheEntry subscriptionCacheEntry2;
				if (obj is Guid && userContext.SubscriptionCache.TryGetEntry((Guid)obj, out subscriptionCacheEntry2))
				{
					newItem.From = new Participant(subscriptionCacheEntry2.DisplayName, subscriptionCacheEntry2.Address, "SMTP");
				}
			}
			catch (NotInBagPropertyErrorException)
			{
			}
		}

		// Token: 0x04000D18 RID: 3352
		private const string MessageDelimiter = "\n________________________________________\n";

		// Token: 0x04000D19 RID: 3353
		private static readonly string[] fontSizeMapping = new string[]
		{
			string.Empty,
			"8pt",
			"10pt",
			"12pt",
			"14pt",
			"18pt",
			"24pt",
			"36pt"
		};
	}
}
