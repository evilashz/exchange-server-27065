using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web;
using Microsoft.Exchange.Clients.Owa.Basic;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Conversations;
using Microsoft.Exchange.Data.TextConverters;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Security;
using Microsoft.Exchange.Security.RightsManagement;
using Microsoft.Exchange.UM.ClientAccess;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x0200039D RID: 925
	internal class ItemPartWriter : DisposeTrackableBase
	{
		// Token: 0x17000955 RID: 2389
		// (get) Token: 0x060022E6 RID: 8934 RVA: 0x000C7C91 File Offset: 0x000C5E91
		// (set) Token: 0x060022E7 RID: 8935 RVA: 0x000C7C99 File Offset: 0x000C5E99
		private protected UserContext UserContext { protected get; private set; }

		// Token: 0x17000956 RID: 2390
		// (get) Token: 0x060022E8 RID: 8936 RVA: 0x000C7CA2 File Offset: 0x000C5EA2
		// (set) Token: 0x060022E9 RID: 8937 RVA: 0x000C7CAA File Offset: 0x000C5EAA
		private protected TextWriter Writer { protected get; private set; }

		// Token: 0x17000957 RID: 2391
		// (get) Token: 0x060022EA RID: 8938 RVA: 0x000C7CB3 File Offset: 0x000C5EB3
		// (set) Token: 0x060022EB RID: 8939 RVA: 0x000C7CBB File Offset: 0x000C5EBB
		private protected OwaStoreObjectId ConversationId { protected get; private set; }

		// Token: 0x17000958 RID: 2392
		// (get) Token: 0x060022EC RID: 8940 RVA: 0x000C7CC4 File Offset: 0x000C5EC4
		protected IStorePropertyBag StorePropertyBag
		{
			get
			{
				return this.conversationTreeNode.StorePropertyBags[0];
			}
		}

		// Token: 0x060022ED RID: 8941 RVA: 0x000C7CD8 File Offset: 0x000C5ED8
		protected ItemPartWriter(UserContext userContext, TextWriter writer, OwaStoreObjectId conversationId, Conversation conversation, IConversationTreeNode conversationTreeNode)
		{
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (conversationId == null)
			{
				throw new ArgumentNullException("converstationId");
			}
			if (conversation == null)
			{
				throw new ArgumentNullException("conversation");
			}
			if (conversationTreeNode == null)
			{
				throw new ArgumentNullException("conversationTreeNode");
			}
			this.UserContext = userContext;
			this.Writer = writer;
			this.ConversationId = conversationId;
			this.conversation = conversation;
			this.conversationTreeNode = conversationTreeNode;
			this.isCelebrated = !ConversationUtilities.IsLocalNode(this.conversationTreeNode);
			this.isDeleted = ConversationUtilities.IsDeletedItemPart((MailboxSession)this.ConversationId.GetSession(this.UserContext), this.conversationTreeNode);
			this.isDraft = ItemUtility.GetProperty<bool>(this.StorePropertyBag, MessageItemSchema.IsDraft, false);
		}

		// Token: 0x060022EE RID: 8942 RVA: 0x000C7DAA File Offset: 0x000C5FAA
		public static PropertyDefinition[] GetRequestedProperties()
		{
			return ItemPartWriter.requestedProperties;
		}

		// Token: 0x060022EF RID: 8943 RVA: 0x000C7DB4 File Offset: 0x000C5FB4
		internal static void OnBeforeItemLoad(object sender, LoadItemEventArgs e)
		{
			e.HtmlStreamOptionCallback = new HtmlStreamOptionCallback(ItemPartWriter.GetSafeHtmlCallbacks);
			List<PropertyDefinition> list = new List<PropertyDefinition>();
			IStorePropertyBag storePropertyBag = e.StorePropertyBag;
			FlagStatus property = ItemUtility.GetProperty<FlagStatus>(storePropertyBag, ItemSchema.FlagStatus, FlagStatus.NotFlagged);
			if (property != FlagStatus.NotFlagged)
			{
				list.AddRange(ItemPartWriter.flagsInfobarProperties);
			}
			bool property2 = ItemUtility.GetProperty<bool>(storePropertyBag, ItemSchema.IsClassified, false);
			if (property2)
			{
				list.AddRange(ItemPartWriter.complianceInfobarProperties);
			}
			if (ObjectClass.IsVoiceMessage(ItemUtility.GetProperty<string>(storePropertyBag, StoreObjectSchema.ItemClass, string.Empty)))
			{
				list.Add(MessageItemSchema.MessageAudioNotes);
			}
			if (ObjectClass.IsSmsMessage(ItemUtility.GetProperty<string>(storePropertyBag, StoreObjectSchema.ItemClass, string.Empty)))
			{
				list.Add(MessageItemSchema.TextMessageDeliveryStatus);
				list.Add(ItemSchema.DisplayTo);
			}
			bool flag = false;
			foreach (IConversationTreeNode conversationTreeNode in e.TreeNode.ChildNodes)
			{
				ConversationTreeNode conversationTreeNode2 = (ConversationTreeNode)conversationTreeNode;
				ItemPartWriter.JunkAndPhishingInfo junkAndPhishingInfo = ItemPartWriter.GetJunkAndPhishingInfo(conversationTreeNode2.StorePropertyBags[0]);
				if (junkAndPhishingInfo.IsSuspectedPhishingItem && !junkAndPhishingInfo.ItemLinkEnabled)
				{
					flag = true;
					break;
				}
			}
			if (flag)
			{
				list.Add(CalendarItemBaseSchema.RecurrencePattern);
			}
			e.MessagePropertyDefinitions = list.ToArray();
		}

		// Token: 0x060022F0 RID: 8944 RVA: 0x000C7EF8 File Offset: 0x000C60F8
		internal static ItemPartWriter GetWriter(UserContext userContext, TextWriter textWriter, OwaStoreObjectId conversationId, Conversation conversation, IConversationTreeNode conversationTreeNode)
		{
			return new ItemPartWriter(userContext, textWriter, conversationId, conversation, conversationTreeNode);
		}

		// Token: 0x060022F1 RID: 8945 RVA: 0x000C7F08 File Offset: 0x000C6108
		public bool Render(string elementId, bool isExpanded, bool isBranched, bool isSelected)
		{
			MailboxSession mailboxSession = (MailboxSession)this.ConversationId.GetSession(this.UserContext);
			if (!ConversationUtilities.ShouldRenderItem(this.conversationTreeNode, mailboxSession))
			{
				return false;
			}
			VersionedId property = ItemUtility.GetProperty<VersionedId>(this.StorePropertyBag, ItemSchema.Id, null);
			OwaStoreObjectId owaStoreObjectId = OwaStoreObjectId.CreateFromSessionFolderId(this.UserContext, mailboxSession, ItemUtility.GetProperty<StoreObjectId>(this.StorePropertyBag, StoreObjectSchema.ParentItemId, null));
			string property2 = ItemUtility.GetProperty<string>(this.StorePropertyBag, StoreObjectSchema.ItemClass, null);
			string property3 = ItemUtility.GetProperty<string>(this.StorePropertyBag, ItemSchema.InternetMessageId, null);
			bool property4 = ItemUtility.GetProperty<bool>(this.StorePropertyBag, MessageItemSchema.IsRead, false);
			JunkEmailUtilities.IsSuspectedPhishingItem(this.StorePropertyBag);
			bool flag = ObjectClass.IsMeetingRequest(property2);
			bool property5 = ItemUtility.GetProperty<bool>(this.StorePropertyBag, ItemSchema.IsResponseRequested, false);
			ItemPartWriter.JunkAndPhishingInfo junkAndPhishingInfo = ItemPartWriter.GetJunkAndPhishingInfo(this.StorePropertyBag);
			bool property6 = ItemUtility.GetProperty<bool>(this.StorePropertyBag, StoreObjectSchema.IsRestricted, false);
			if (this.isDraft)
			{
				isExpanded = true;
			}
			ConversationSortOrder conversationSortOrder = this.UserContext.UserOptions.ConversationSortOrder;
			this.Writer.Write("<div");
			RenderingUtilities.RenderExpando(this.Writer, "id", elementId);
			RenderingUtilities.RenderExpando(this.Writer, "fLocal", ConversationUtilities.IsLocalNode(this.conversationTreeNode) ? "1" : "0");
			RenderingUtilities.RenderExpando(this.Writer, "class", property4 ? "itmPrt" : "itmPrt itmPrtUr");
			RenderingUtilities.RenderExpando(this.Writer, "sItmId", Utilities.GetItemIdString(property.ObjectId, this.ConversationId));
			RenderingUtilities.RenderExpando(this.Writer, "sCK", property.ChangeKeyAsBase64String());
			RenderingUtilities.RenderExpando(this.Writer, "sT", property2);
			RenderingUtilities.RenderExpando(this.Writer, "fExp", isExpanded ? "1" : "0");
			RenderingUtilities.RenderExpando(this.Writer, "sFId", owaStoreObjectId.ToString());
			RenderingUtilities.RenderExpando(this.Writer, "iImgFlt", this.GetImageFilteringValue());
			RenderingUtilities.RenderExpando(this.Writer, "fDraft", this.isDraft ? "1" : "0");
			RenderingUtilities.RenderExpando(this.Writer, "fPhsh", (junkAndPhishingInfo.IsSuspectedPhishingItem && !junkAndPhishingInfo.ItemLinkEnabled) ? "1" : "0");
			RenderingUtilities.RenderExpando(this.Writer, "fJnk", junkAndPhishingInfo.IsInJunkEmailFolder ? "1" : "0");
			RenderingUtilities.RenderExpando(this.Writer, "fMR", flag ? "1" : "0");
			RenderingUtilities.RenderExpando(this.Writer, "fRR", property5 ? "1" : "0");
			RenderingUtilities.RenderExpando(this.Writer, "iInternetMId", (!string.IsNullOrEmpty(property3)) ? property3.GetHashCode().ToString() : "0");
			RenderingUtilities.RenderExpando(this.Writer, "fSup", ConversationUtilities.IsSupportedReadingPaneType(this.UserContext, this.StorePropertyBag) ? "1" : "0");
			RenderingUtilities.RenderExpando(this.Writer, "fOlk", ConversationUtilities.IsOpenInOutlookType(this.StorePropertyBag) ? "1" : "0");
			RenderingUtilities.RenderExpando(this.Writer, "fArchive", Utilities.IsArchiveMailbox(mailboxSession) ? "1" : "0");
			if (property6 && this.UserContext.IsIrmEnabled)
			{
				ItemPart itemPart = ItemPartWriter.GetItemPart(this.conversation, this.conversationTreeNode);
				if (this.IsUsageRightRestricted(itemPart, ContentRight.Reply))
				{
					RenderingUtilities.RenderExpando(this.Writer, "fRplR", 1);
				}
				if (this.IsUsageRightRestricted(itemPart, ContentRight.ReplyAll))
				{
					RenderingUtilities.RenderExpando(this.Writer, "fRAR", 1);
				}
				if (this.IsUsageRightRestricted(itemPart, ContentRight.Forward))
				{
					RenderingUtilities.RenderExpando(this.Writer, "fFR", 1);
				}
				if (this.IsUsageRightRestricted(itemPart, ContentRight.Print))
				{
					RenderingUtilities.RenderExpando(this.Writer, "fPR", 1);
				}
				if (this.IsUsageRightRestricted(itemPart, ContentRight.Extract))
				{
					RenderingUtilities.RenderExpando(this.Writer, "fCR", 1);
				}
				if (!this.IsUsageRightRestricted(itemPart, ContentRight.Export))
				{
					RenderingUtilities.RenderExpando(this.Writer, "fRmR", 1);
				}
			}
			string value = InfobarMessageBuilderBase.ShouldRenderReadReceiptNoticeInfobar(this.UserContext, this.conversationTreeNode.StorePropertyBags[0]) ? "1" : "0";
			RenderingUtilities.RenderExpando(this.Writer, "fReadRcp", value);
			if (this.conversationTreeNode.ParentNode != null && this.conversationTreeNode.ParentNode.StorePropertyBags != null)
			{
				VersionedId property7 = ItemUtility.GetProperty<VersionedId>(this.conversationTreeNode.ParentNode.StorePropertyBags[0], ItemSchema.Id, null);
				RenderingUtilities.RenderExpando(this.Writer, "sPId", Utilities.GetItemIdString(property7.ObjectId, this.ConversationId));
			}
			RenderingUtilities.RenderExpando(this.Writer, "fR", property4 ? "1" : "0");
			this.Writer.Write(">");
			SanitizedHtmlString sanitizedHtmlString = null;
			if (isBranched && this.conversationTreeNode.ParentNode != null && this.conversationTreeNode.ParentNode.StorePropertyBags != null)
			{
				sanitizedHtmlString = this.GetBranchTextHtml(this.conversationTreeNode.ParentNode);
				if (!isExpanded)
				{
					this.Writer.Write("<div id=\"divBrTxt\" style=\"display:none\">");
					this.Writer.Write(sanitizedHtmlString);
					this.Writer.Write("</div>");
				}
			}
			if (this.UserContext.IsRtl)
			{
				this.UserContext.RenderThemeImage(this.Writer, isExpanded ? ThemeFileId.MinusRTL : ThemeFileId.PlusRTL, null, new string[]
				{
					"id=imgEC",
					isExpanded ? "style=\"top:8px;\"" : "style=\"top:0px;\""
				});
			}
			else
			{
				this.UserContext.RenderThemeImage(this.Writer, isExpanded ? ThemeFileId.Minus : ThemeFileId.Plus, null, new string[]
				{
					"id=imgEC",
					isExpanded ? "style=\"top:8px;\"" : "style=\"top:0px;\""
				});
			}
			this.Writer.Write("<img id=imgPrg class=\"prg\" style=\"display:none\" src=\"");
			this.UserContext.RenderThemeFileUrl(this.Writer, ThemeFileId.ProgressSmall);
			this.Writer.Write("\">");
			this.RenderCollapsedItemPart(!isExpanded);
			if (isExpanded)
			{
				this.RenderExpandedItemPart(isExpanded, isSelected, sanitizedHtmlString);
			}
			this.Writer.Write("</div>");
			return true;
		}

		// Token: 0x060022F2 RID: 8946 RVA: 0x000C8580 File Offset: 0x000C6780
		public void RenderExpandedItemPart(bool isVisible, bool isSelected, SanitizedHtmlString branchTextHtml)
		{
			using (ItemPartWriter.ExpandedItemPartWriter expandedItemPartWriter = this.GetExpandedItemPartWriter())
			{
				expandedItemPartWriter.Render(isVisible, isSelected, branchTextHtml);
			}
		}

		// Token: 0x060022F3 RID: 8947 RVA: 0x000C85BC File Offset: 0x000C67BC
		private void RenderCollapsedItemPart(bool isVisible)
		{
			this.Writer.Write("<div id=\"divClps\" class=\"divClps");
			if (this.isDraft)
			{
				this.Writer.Write(" draftItem");
			}
			this.Writer.Write(isVisible ? "\">" : "\" style=\"display:none\">");
			if (this.isDraft)
			{
				this.Writer.Write("<div id=\"divDraftsBk\">");
				this.Writer.Write("<div id=\"divDraftsBkMask\"></div>");
			}
			else
			{
				RenderingUtilities.RenderGradientDivider(this.Writer, this.UserContext);
				this.Writer.Write("<div id=\"divPrtBk\">");
			}
			this.Writer.Write("</div>");
			this.Writer.Write("<div id=\"divSn\" class=\"divSn{0} divTx\">", this.isCelebrated ? " divCelSn" : string.Empty);
			if (this.isDraft)
			{
				this.Writer.Write("<span id=\"spnItemNotSent\">");
				this.Writer.Write(SanitizedHtmlString.GetNonEncoded(-1388076595));
				this.Writer.Write("</span>");
			}
			else
			{
				Participant property = ItemUtility.GetProperty<Participant>(this.StorePropertyBag, ItemSchema.From, null);
				if (property != null && !string.IsNullOrEmpty(property.DisplayName))
				{
					if (this.isDeleted)
					{
						this.Writer.Write("<strike>");
					}
					Utilities.SanitizeHtmlEncode(property.DisplayName, this.Writer);
					if (this.isDeleted)
					{
						this.Writer.Write("</strike>");
					}
				}
			}
			this.Writer.Write("</div>");
			string text = ItemUtility.GetProperty<string>(this.conversationTreeNode.StorePropertyBags[0], ItemSchema.TextBody, string.Empty);
			if (string.IsNullOrEmpty(text))
			{
				PropertyError propertyError = this.conversationTreeNode.StorePropertyBags[0].TryGetProperty(ItemSchema.TextBody) as PropertyError;
				if (propertyError != null)
				{
					text = Strings.ConversationMessageLoadFailure;
				}
			}
			SanitizedHtmlString value = Utilities.SanitizeHtmlEncode(text);
			this.Writer.Write("<div id=\"divSm\" class=\"divTx\">");
			if (this.isDeleted)
			{
				this.Writer.Write("<strike>");
			}
			this.Writer.Write(value);
			if (this.isDeleted)
			{
				this.Writer.Write("</strike>");
			}
			this.Writer.Write("</div>");
			this.RenderInformationalIcons(this.Writer, this.StorePropertyBag);
			this.Writer.Write("<div id=divDt><div class=divTx>");
			if (this.isDraft)
			{
				this.Writer.Write("<span class=spnDtDr>");
				this.Writer.Write(SanitizedHtmlString.FromStringId(1039446174));
				this.Writer.Write("</span>");
			}
			else
			{
				ExDateTime property2 = ItemUtility.GetProperty<ExDateTime>(this.StorePropertyBag, ItemSchema.ReceivedTime, ExDateTime.MinValue);
				if (property2 != ExDateTime.MinValue)
				{
					ListViewContentsRenderingUtilities.RenderSmartDate(this.Writer, this.UserContext, property2);
				}
				else
				{
					this.Writer.Write("&nbsp;");
				}
			}
			this.Writer.Write("</div></div></div>");
		}

		// Token: 0x060022F4 RID: 8948 RVA: 0x000C88B4 File Offset: 0x000C6AB4
		private SanitizedHtmlString GetBranchTextHtml(IConversationTreeNode parentNode)
		{
			Participant property = ItemUtility.GetProperty<Participant>(parentNode.StorePropertyBags[0], ItemSchema.From, null);
			ExDateTime property2 = ItemUtility.GetProperty<ExDateTime>(parentNode.StorePropertyBags[0], ItemSchema.ReceivedTime, ExDateTime.MinValue);
			if (property == null && property2 == ExDateTime.MinValue)
			{
				return null;
			}
			StringBuilder builder = new StringBuilder();
			SanitizedHtmlString result;
			using (SanitizingStringWriter<OwaHtml> sanitizingStringWriter = new SanitizingStringWriter<OwaHtml>(builder))
			{
				sanitizingStringWriter.Write("<span id=\"spnBrSn\">");
				SanitizedHtmlString sanitizedHtmlString = SanitizedHtmlString.Empty;
				SanitizedHtmlString sanitizedHtmlString2 = SanitizedHtmlString.Empty;
				if (property != null && !string.IsNullOrEmpty(property.DisplayName))
				{
					sanitizedHtmlString = Utilities.SanitizeHtmlEncode(property.DisplayName);
				}
				if (property2 != ExDateTime.MinValue)
				{
					StringBuilder builder2 = new StringBuilder();
					using (SanitizingStringWriter<OwaHtml> sanitizingStringWriter2 = new SanitizingStringWriter<OwaHtml>(builder2))
					{
						sanitizingStringWriter2.Write("<span>");
						ListViewContentsRenderingUtilities.RenderSmartDate(sanitizingStringWriter2, this.UserContext, property2);
						sanitizingStringWriter2.Write("</span>");
						sanitizedHtmlString2 = sanitizingStringWriter2.ToSanitizedString<SanitizedHtmlString>();
					}
				}
				sanitizingStringWriter.Write(SanitizedHtmlString.Format(CultureInfo.InvariantCulture, LocalizedStrings.GetHtmlEncoded(1993321225), new object[]
				{
					sanitizedHtmlString,
					sanitizedHtmlString2
				}));
				sanitizingStringWriter.Write("</span>");
				result = sanitizingStringWriter.ToSanitizedString<SanitizedHtmlString>();
			}
			return result;
		}

		// Token: 0x060022F5 RID: 8949 RVA: 0x000C8A20 File Offset: 0x000C6C20
		private void RenderInformationalIcons(TextWriter writer, IStorePropertyBag storePropertyBag)
		{
			writer.Write("<div id=divInfIc>");
			Importance property = ItemUtility.GetProperty<Importance>(storePropertyBag, ItemSchema.Importance, Importance.Normal);
			if (property == Importance.High || property == Importance.Low)
			{
				this.UserContext.RenderThemeImage(writer, (property == Importance.High) ? ThemeFileId.ImportanceHigh2 : ThemeFileId.ImportanceLow2, null, new object[]
				{
					"id=imgImp"
				});
			}
			bool flag = ItemUtility.GetProperty<bool>(storePropertyBag, ItemSchema.HasAttachment, false);
			if (flag)
			{
				string property2 = ItemUtility.GetProperty<string>(storePropertyBag, StoreObjectSchema.ItemClass, null);
				if (ObjectClass.IsSmsMessage(property2))
				{
					flag = false;
				}
			}
			if (flag)
			{
				this.UserContext.RenderThemeImage(writer, ThemeFileId.Attachment1, null, new object[]
				{
					"id=imgAtt"
				});
			}
			this.RenderCategorySwatches(writer, storePropertyBag);
			FlagStatus property3 = (FlagStatus)ItemUtility.GetProperty<int>(storePropertyBag, ItemSchema.FlagStatus, 0);
			if (property3 == FlagStatus.Flagged)
			{
				this.UserContext.RenderThemeImage(writer, ThemeFileId.Flag, null, new object[]
				{
					"id=imgFlg"
				});
			}
			writer.Write("</div>");
		}

		// Token: 0x060022F6 RID: 8950 RVA: 0x000C8B14 File Offset: 0x000C6D14
		private void RenderCategorySwatches(TextWriter writer, IStorePropertyBag storePropertyBag)
		{
			string[] property = ItemUtility.GetProperty<string[]>(storePropertyBag, ItemSchema.Categories, null);
			if (property == null || property.Length <= 0)
			{
				return;
			}
			writer.Write("<span id=spnCnvCat");
			if (this.UserContext.CanActAsOwner && property != null && 0 < property.Length)
			{
				writer.Write(" title=\"");
				for (int i = 0; i < property.Length; i++)
				{
					if (i != 0)
					{
						writer.Write("; ");
					}
					Utilities.SanitizeHtmlEncode(property[i], writer);
				}
				writer.Write("\"");
			}
			writer.Write(">");
			FlagStatus property2 = ItemUtility.GetProperty<FlagStatus>(storePropertyBag, ItemSchema.FlagStatus, FlagStatus.NotFlagged);
			int property3 = ItemUtility.GetProperty<int>(storePropertyBag, ItemSchema.ItemColor, -1);
			bool property4 = ItemUtility.GetProperty<bool>(storePropertyBag, ItemSchema.IsToDoItem, false);
			CategorySwatch.RenderViewCategorySwatches(writer, this.UserContext, property, property4, property2, property3, null, false);
			writer.Write("</span>");
		}

		// Token: 0x060022F7 RID: 8951 RVA: 0x000C8BE8 File Offset: 0x000C6DE8
		private SanitizedHtmlString GetAlternateBodyForIrm(ItemPart itemPart, Microsoft.Exchange.Data.Storage.BodyFormat bodyFormat)
		{
			if (itemPart == null)
			{
				throw new ArgumentNullException("itemPart");
			}
			if (!itemPart.IrmInfo.IsRestricted)
			{
				throw new ArgumentException("itemPart", "This item part is not for a restricted item.");
			}
			string property = ItemUtility.GetProperty<string>(this.StorePropertyBag, StoreObjectSchema.ItemClass, string.Empty);
			return Utilities.GetAlternateBodyForIrm(this.UserContext, bodyFormat, itemPart.IrmInfo.DecryptionStatus, ObjectClass.IsVoiceMessage(property));
		}

		// Token: 0x060022F8 RID: 8952 RVA: 0x000C8C54 File Offset: 0x000C6E54
		private SanitizedHtmlString GetAlternateBodyForMessageLoadFailure(ItemPart itemPart)
		{
			SanitizingStringBuilder<OwaHtml> sanitizingStringBuilder = new SanitizingStringBuilder<OwaHtml>();
			sanitizingStringBuilder.AppendFormat("<font face=\"{0}\" size=\"2\">", new object[]
			{
				Utilities.GetDefaultFontName()
			});
			sanitizingStringBuilder.AppendFormat("<img src=\"{0}\">&nbsp;", new object[]
			{
				this.UserContext.GetThemeFileUrl(ThemeFileId.Error)
			});
			sanitizingStringBuilder.AppendFormat(LocalizedStrings.GetNonEncoded(1403581698), new object[0]);
			sanitizingStringBuilder.Append("</font>");
			return sanitizingStringBuilder.ToSanitizedString<SanitizedHtmlString>();
		}

		// Token: 0x060022F9 RID: 8953 RVA: 0x000C8CCC File Offset: 0x000C6ECC
		private static KeyValuePair<HtmlStreamingFlags, HtmlCallbackBase> GetSafeHtmlCallbacks(Item item)
		{
			ItemPartWriter.JunkAndPhishingInfo junkAndPhishingInfo = ItemPartWriter.GetJunkAndPhishingInfo(item);
			HtmlCallbackBase value = new OwaSafeHtmlConversationsCallbacks(item, UserContextManager.GetUserContext().IsPublicLogon, junkAndPhishingInfo.IsJunkOrPhishing, OwaContext.Current);
			return new KeyValuePair<HtmlStreamingFlags, HtmlCallbackBase>(HtmlStreamingFlags.FilterHtml, value);
		}

		// Token: 0x060022FA RID: 8954 RVA: 0x000C8D04 File Offset: 0x000C6F04
		private ItemPartWriter.ExpandedItemPartWriter GetExpandedItemPartWriter()
		{
			string property = ItemUtility.GetProperty<string>(this.StorePropertyBag, StoreObjectSchema.ItemClass, null);
			if (ObjectClass.IsMeetingRequest(property) || ObjectClass.IsMeetingCancellation(property) || ObjectClass.IsMeetingResponse(property))
			{
				return new ItemPartWriter.ExpandedMeetingItemPartWriter(this);
			}
			if (ObjectClass.IsOfClass(property, "IPM.Sharing") && this.UserContext.IsFeatureEnabled(Feature.Calendar))
			{
				StoreObjectId objectId = ItemUtility.GetProperty<VersionedId>(this.StorePropertyBag, ItemSchema.Id, null).ObjectId;
				SharingMessageItem sharingMessageItem = SharingMessageItem.Bind(this.ConversationId.GetSession(this.UserContext), objectId, ReadSharingMessage.PrefetchProperties);
				DefaultFolderType defaultFolderType = DefaultFolderType.None;
				try
				{
					defaultFolderType = sharingMessageItem.SharedFolderType;
				}
				catch (NotSupportedSharingMessageException ex)
				{
					ExTraceGlobals.CoreTracer.TraceDebug<string>(0L, "Cannot handle this sharing message due to {0}", ex.Message);
				}
				if (defaultFolderType == DefaultFolderType.Calendar)
				{
					return new ItemPartWriter.ExpandedSharingItemPartWriter(sharingMessageItem, this);
				}
				sharingMessageItem.Dispose();
			}
			return new ItemPartWriter.ExpandedMessageItemPartWriter(this);
		}

		// Token: 0x060022FB RID: 8955 RVA: 0x000C8DE4 File Offset: 0x000C6FE4
		private static ItemPartWriter.JunkAndPhishingInfo GetJunkAndPhishingInfo(IStorePropertyBag storePropertyBag)
		{
			ItemPartWriter.JunkAndPhishingInfo result = default(ItemPartWriter.JunkAndPhishingInfo);
			result.IsInJunkEmailFolder = false;
			result.IsSuspectedPhishingItem = false;
			result.ItemLinkEnabled = false;
			result.IsJunkOrPhishing = false;
			JunkEmailUtilities.GetJunkEmailPropertiesForItem(storePropertyBag, false, false, UserContextManager.GetUserContext(), out result.IsInJunkEmailFolder, out result.IsSuspectedPhishingItem, out result.ItemLinkEnabled, out result.IsJunkOrPhishing);
			return result;
		}

		// Token: 0x060022FC RID: 8956 RVA: 0x000C8E44 File Offset: 0x000C7044
		private string GetImageFilteringValue()
		{
			IStorePropertyBag storePropertyBag = this.conversationTreeNode.StorePropertyBags[0];
			int num;
			if (ItemPartWriter.GetJunkAndPhishingInfo(storePropertyBag).IsJunkOrPhishing)
			{
				num = 1;
			}
			else if (this.UserContext.Configuration.FilterWebBeaconsAndHtmlForms == WebBeaconFilterLevels.ForceFilter)
			{
				num = 2;
			}
			else if (Utilities.IsWebBeaconsAllowed(storePropertyBag) || this.UserContext.Configuration.FilterWebBeaconsAndHtmlForms == WebBeaconFilterLevels.DisableFilter)
			{
				num = 3;
			}
			else
			{
				num = 4;
			}
			return num.ToString();
		}

		// Token: 0x060022FD RID: 8957 RVA: 0x000C8EB8 File Offset: 0x000C70B8
		private static ItemPart GetItemPart(Conversation conversation, IConversationTreeNode conversationTreeNode)
		{
			IStorePropertyBag propertyBag = conversationTreeNode.StorePropertyBags[0];
			VersionedId property = ItemUtility.GetProperty<VersionedId>(propertyBag, ItemSchema.Id, null);
			return conversation.GetItemPart(StoreId.GetStoreObjectId(property));
		}

		// Token: 0x060022FE RID: 8958 RVA: 0x000C8EEC File Offset: 0x000C70EC
		private bool IsRestrictedButFeatureDisabledOrDecryptionFailed(ItemPart itemPart)
		{
			return itemPart.IrmInfo.IsRestricted && (!this.UserContext.IsIrmEnabled || itemPart.IrmInfo.DecryptionStatus.Failed);
		}

		// Token: 0x060022FF RID: 8959 RVA: 0x000C8F2C File Offset: 0x000C712C
		private bool IsUsageRightRestricted(ItemPart itemPart, ContentRight right)
		{
			if (itemPart == null)
			{
				throw new ArgumentNullException("itemPart");
			}
			if (!itemPart.IrmInfo.IsRestricted)
			{
				return false;
			}
			if (!this.UserContext.IsIrmEnabled)
			{
				return false;
			}
			if (itemPart.IrmInfo.DecryptionStatus.Failed)
			{
				return !right.IsUsageRightGranted(ContentRight.Extract) && !right.IsUsageRightGranted(ContentRight.Print);
			}
			return !itemPart.IrmInfo.UsageRights.IsUsageRightGranted(right);
		}

		// Token: 0x06002300 RID: 8960 RVA: 0x000C8FA4 File Offset: 0x000C71A4
		protected override void InternalDispose(bool isDisposing)
		{
		}

		// Token: 0x06002301 RID: 8961 RVA: 0x000C8FA6 File Offset: 0x000C71A6
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<ItemPartWriter>(this);
		}

		// Token: 0x04001884 RID: 6276
		protected const string MessagePartToolbarId = "mpToolbar";

		// Token: 0x04001885 RID: 6277
		private static PropertyDefinition[] requestedProperties = new PropertyDefinition[]
		{
			ItemSchema.Id,
			StoreObjectSchema.ChangeKey,
			StoreObjectSchema.ItemClass,
			ItemSchema.Subject,
			ItemSchema.IconIndex,
			MessageItemSchema.MessageInConflict,
			MessageItemSchema.IsRead,
			ItemSchema.From,
			ItemSchema.Sender,
			ItemSchema.ReceivedTime,
			ItemSchema.HasAttachment,
			StoreObjectSchema.ParentItemId,
			MessageItemSchema.IsDraft,
			ItemSchema.Categories,
			ItemSchema.ItemColor,
			ItemSchema.IsToDoItem,
			ItemSchema.FlagStatus,
			ItemSchema.FlagRequest,
			MessageItemSchema.LastVerbExecuted,
			MessageItemSchema.LastVerbExecutionTime,
			ItemSchema.Importance,
			ItemSchema.Sensitivity,
			ItemSchema.IsClassified,
			MessageItemSchema.IsReadReceiptPending,
			ItemSchema.BlockStatus,
			ItemSchema.EdgePcl,
			ItemSchema.LinkEnabled,
			ItemSchema.IsResponseRequested,
			ItemSchema.InternetMessageId,
			MessageItemSchema.VoiceMessageAttachmentOrder,
			MessageItemSchema.RequireProtectedPlayOnPhone,
			MessageItemSchema.VotingBlob,
			StoreObjectSchema.IsRestricted,
			StoreObjectSchema.PolicyTag,
			ItemSchema.RetentionDate
		};

		// Token: 0x04001886 RID: 6278
		private static PropertyDefinition[] flagsInfobarProperties = new PropertyDefinition[]
		{
			ItemSchema.FlagStatus,
			ItemSchema.FlagRequest,
			ItemSchema.CompleteDate,
			ItemSchema.FlagCompleteTime,
			ItemSchema.UtcStartDate,
			ItemSchema.UtcDueDate,
			ItemSchema.ReminderDueBy,
			ItemSchema.ReminderIsSet,
			MessageItemSchema.ReplyTime
		};

		// Token: 0x04001887 RID: 6279
		private static PropertyDefinition[] complianceInfobarProperties = new PropertyDefinition[]
		{
			ItemSchema.IsClassified,
			ItemSchema.ClassificationGuid,
			ItemSchema.ClassificationDescription,
			ItemSchema.Classification
		};

		// Token: 0x04001888 RID: 6280
		private bool isCelebrated;

		// Token: 0x04001889 RID: 6281
		private bool isDeleted;

		// Token: 0x0400188A RID: 6282
		private bool isDraft;

		// Token: 0x0400188B RID: 6283
		private Conversation conversation;

		// Token: 0x0400188C RID: 6284
		private IConversationTreeNode conversationTreeNode;

		// Token: 0x0200039E RID: 926
		private enum ImageFiltering
		{
			// Token: 0x04001891 RID: 6289
			AlwaysBlockNoInfobar = 1,
			// Token: 0x04001892 RID: 6290
			AlwaysBlock,
			// Token: 0x04001893 RID: 6291
			AlwaysDisplay,
			// Token: 0x04001894 RID: 6292
			UserChoice
		}

		// Token: 0x0200039F RID: 927
		internal struct JunkAndPhishingInfo
		{
			// Token: 0x04001895 RID: 6293
			public bool IsInJunkEmailFolder;

			// Token: 0x04001896 RID: 6294
			public bool IsSuspectedPhishingItem;

			// Token: 0x04001897 RID: 6295
			public bool ItemLinkEnabled;

			// Token: 0x04001898 RID: 6296
			public bool IsJunkOrPhishing;
		}

		// Token: 0x020003A0 RID: 928
		protected abstract class ExpandedItemPartWriter : DisposeTrackableBase
		{
			// Token: 0x06002303 RID: 8963 RVA: 0x000C9180 File Offset: 0x000C7380
			protected ExpandedItemPartWriter(ItemPartWriter itemPartWriter)
			{
				this.ItemPartWriter = itemPartWriter;
			}

			// Token: 0x17000959 RID: 2393
			// (get) Token: 0x06002304 RID: 8964 RVA: 0x000C918F File Offset: 0x000C738F
			// (set) Token: 0x06002305 RID: 8965 RVA: 0x000C9197 File Offset: 0x000C7397
			internal ItemPartWriter ItemPartWriter { get; private set; }

			// Token: 0x1700095A RID: 2394
			// (get) Token: 0x06002306 RID: 8966 RVA: 0x000C91A0 File Offset: 0x000C73A0
			internal UserContext UserContext
			{
				get
				{
					return this.ItemPartWriter.UserContext;
				}
			}

			// Token: 0x1700095B RID: 2395
			// (get) Token: 0x06002307 RID: 8967 RVA: 0x000C91AD File Offset: 0x000C73AD
			internal TextWriter Writer
			{
				get
				{
					return this.ItemPartWriter.Writer;
				}
			}

			// Token: 0x1700095C RID: 2396
			// (get) Token: 0x06002308 RID: 8968 RVA: 0x000C91BA File Offset: 0x000C73BA
			internal OwaStoreObjectId ConversationId
			{
				get
				{
					return this.ItemPartWriter.ConversationId;
				}
			}

			// Token: 0x1700095D RID: 2397
			// (get) Token: 0x06002309 RID: 8969 RVA: 0x000C91C7 File Offset: 0x000C73C7
			internal Conversation Conversation
			{
				get
				{
					return this.ItemPartWriter.conversation;
				}
			}

			// Token: 0x1700095E RID: 2398
			// (get) Token: 0x0600230A RID: 8970 RVA: 0x000C91D4 File Offset: 0x000C73D4
			internal IConversationTreeNode ConversationTreeNode
			{
				get
				{
					return this.ItemPartWriter.conversationTreeNode;
				}
			}

			// Token: 0x1700095F RID: 2399
			// (get) Token: 0x0600230B RID: 8971 RVA: 0x000C91E1 File Offset: 0x000C73E1
			internal ItemPart ItemPart
			{
				get
				{
					if (this.itemPart == null)
					{
						this.itemPart = ItemPartWriter.GetItemPart(this.Conversation, this.ConversationTreeNode);
					}
					return this.itemPart;
				}
			}

			// Token: 0x17000960 RID: 2400
			// (get) Token: 0x0600230C RID: 8972 RVA: 0x000C9208 File Offset: 0x000C7408
			internal IStorePropertyBag StorePropertyBag
			{
				get
				{
					return this.ItemPart.StorePropertyBag;
				}
			}

			// Token: 0x17000961 RID: 2401
			// (get) Token: 0x0600230D RID: 8973 RVA: 0x000C9215 File Offset: 0x000C7415
			internal bool IsCelebrated
			{
				get
				{
					return this.ItemPartWriter.isCelebrated;
				}
			}

			// Token: 0x17000962 RID: 2402
			// (get) Token: 0x0600230E RID: 8974 RVA: 0x000C9222 File Offset: 0x000C7422
			internal bool IsDeleted
			{
				get
				{
					return this.ItemPartWriter.isDeleted;
				}
			}

			// Token: 0x17000963 RID: 2403
			// (get) Token: 0x0600230F RID: 8975 RVA: 0x000C922F File Offset: 0x000C742F
			protected virtual bool IsDraft
			{
				get
				{
					return this.ItemPartWriter.isDraft;
				}
			}

			// Token: 0x06002310 RID: 8976 RVA: 0x000C923C File Offset: 0x000C743C
			public void Render(bool isVisible, bool isSelected, SanitizedHtmlString branchTextHtml)
			{
				this.Writer.Write("<div id=\"divExp\" class=\"divExp");
				if (this.IsDraft)
				{
					this.Writer.Write(" draftItem\"");
				}
				else
				{
					this.Writer.Write("\"");
				}
				this.RenderExpandedPartExpandos();
				this.Writer.Write(isVisible ? ">" : " style=\"display:none\">");
				if (this.IsDraft)
				{
					this.Writer.Write("<div id=\"divDraftsBk\">");
					this.Writer.Write("<div id=\"divDraftsBkMask\"></div>");
				}
				else
				{
					this.Writer.Write("<div id=\"divPrtBk\">");
				}
				this.Writer.Write("</div>");
				this.RenderHeader(isSelected);
				ArrayList attachmentWellRenderObjects;
				bool shouldRenderAttachmentWell = this.InitAttachmentWell(out attachmentWellRenderObjects);
				string property = ItemUtility.GetProperty<string>(this.ItemPart.StorePropertyBag, StoreObjectSchema.ItemClass, string.Empty);
				if (this.UserContext.BrowserType != BrowserType.IE && ObjectClass.IsVoiceMessage(property) && this.itemPart.IrmInfo.IsRestricted)
				{
					this.Infobar.AddMessage(-1230602352, InfobarMessageType.Informational);
				}
				if (ObjectClass.IsTaskRequest(property))
				{
					this.Infobar.AddMessage(357315796, InfobarMessageType.Informational);
				}
				this.Writer.Write("<div id=\"divSelDisplay\"");
				this.Writer.Write(isSelected ? ">" : " class=\"unselected\">");
				if (!this.IsDraft)
				{
					this.RenderBranchTextPlaceholder(branchTextHtml);
				}
				this.RenderWells(shouldRenderAttachmentWell, attachmentWellRenderObjects);
				if (!this.UserContext.IsSenderPhotosFeatureEnabled(Feature.DisplayPhotos))
				{
					this.RenderSubHeader();
				}
				this.Infobar.Render(this.Writer, false, false);
				this.Writer.Write("</div>");
				this.RenderToolbar();
				this.RenderVoicemailPlayer();
				this.RenderBody();
				this.Writer.Write("</div>");
				if (this.UserContext.IsIrmEnabled && this.ItemPart.IrmInfo.IsRestricted && this.ItemPart.IrmInfo.DecryptionStatus.FailureCode == RightsManagementFailureCode.MissingLicense && !Utilities.IsPrefetchRequest(OwaContext.Current))
				{
					MissingRightsManagementLicenseException ex = (MissingRightsManagementLicenseException)this.ItemPart.IrmInfo.DecryptionStatus.Exception;
					if (ex != null && ex.MessageStoreId != null && !ex.MessageStoreId.IsFakeId && !string.IsNullOrEmpty(ex.PublishLicense))
					{
						OwaStoreObjectId messageId = OwaStoreObjectId.CreateFromItemId(ex.MessageStoreId, null, ex.MessageInArchive ? OwaStoreObjectIdType.ArchiveMailboxObject : OwaStoreObjectIdType.MailBoxObject, ex.MailboxOwnerLegacyDN);
						this.UserContext.AsyncAcquireIrmLicenses(messageId, ex.PublishLicense, ex.RequestCorrelator);
					}
				}
			}

			// Token: 0x06002311 RID: 8977 RVA: 0x000C94DD File Offset: 0x000C76DD
			protected override void InternalDispose(bool isDisposing)
			{
			}

			// Token: 0x06002312 RID: 8978 RVA: 0x000C94DF File Offset: 0x000C76DF
			protected override DisposeTracker InternalGetDisposeTracker()
			{
				return DisposeTracker.Get<ItemPartWriter.ExpandedItemPartWriter>(this);
			}

			// Token: 0x17000964 RID: 2404
			// (get) Token: 0x06002313 RID: 8979 RVA: 0x000C94E7 File Offset: 0x000C76E7
			protected Infobar Infobar
			{
				get
				{
					if (this.infoBar == null)
					{
						this.infoBar = this.BuildInfobar();
					}
					return this.infoBar;
				}
			}

			// Token: 0x06002314 RID: 8980 RVA: 0x000C9503 File Offset: 0x000C7703
			protected virtual void RenderExpandedPartExpandos()
			{
			}

			// Token: 0x06002315 RID: 8981 RVA: 0x000C9508 File Offset: 0x000C7708
			protected virtual void RenderHeader(bool isSelected)
			{
				this.Writer.Write("<div id=\"divExpHdr\"");
				if (this.UserContext.IsSenderPhotosFeatureEnabled(Feature.DisplayPhotos))
				{
					this.Writer.Write(" class=\"withPhoto\"");
				}
				this.Writer.Write(">");
				if (!this.IsDraft)
				{
					RenderingUtilities.RenderGradientDivider(this.Writer, this.UserContext);
				}
				this.Writer.Write("<div id=\"divSn\" class=\"divSn{0}{1} divTx\">", this.IsCelebrated ? " divCelSn" : string.Empty, this.IsDeleted ? " divDelSn" : string.Empty);
				Participant property = ItemUtility.GetProperty<Participant>(this.ItemPart.StorePropertyBag, ItemSchema.From, null);
				Participant property2 = ItemUtility.GetProperty<Participant>(this.ItemPart.StorePropertyBag, ItemSchema.Sender, null);
				if (!this.IsDraft)
				{
					if (property2 != null && !string.IsNullOrEmpty(property2.DisplayName))
					{
						if (property != null && !string.IsNullOrEmpty(property.DisplayName))
						{
							RenderingUtilities.RenderSender(this.UserContext, this.Writer, property2, property, new RenderSubHeaderDelegate(this.RenderSubHeaderInSender));
						}
						else
						{
							RenderingUtilities.RenderSender(this.UserContext, this.Writer, property2, new RenderSubHeaderDelegate(this.RenderSubHeaderInSender));
						}
					}
					else
					{
						this.Writer.Write("&nbsp;");
					}
				}
				else
				{
					this.Writer.Write("<span id=\"spnItemNotSent\">");
					this.Writer.Write(SanitizedHtmlString.GetNonEncoded(-1388076595));
					this.Writer.Write("</span>");
				}
				this.Writer.Write("</div>");
				if (!this.IsDraft)
				{
					this.RenderActionIcons(isSelected);
				}
				this.ItemPartWriter.RenderInformationalIcons(this.Writer, this.ItemPart.StorePropertyBag);
				this.Writer.Write("<div id=\"divActions\" style=\"display:none\"><span id=\"spnActions\">");
				this.Writer.Write(SanitizedHtmlString.FromStringId(-859543544));
				this.Writer.Write("</span>");
				this.UserContext.RenderThemeImage(this.Writer, ThemeFileId.DownButton3);
				this.Writer.Write("</div>");
				if (this.IsDraft)
				{
					this.Writer.Write("<div id=\"divDraft\"><span id=\"spnDraft\">");
					this.Writer.Write(SanitizedHtmlString.FromStringId(-42472915));
					this.Writer.Write("</span></div>");
				}
				this.Writer.Write("</div>");
			}

			// Token: 0x06002316 RID: 8982 RVA: 0x000C9778 File Offset: 0x000C7978
			private void RenderActionIcons(bool isSelected)
			{
				string property = ItemUtility.GetProperty<string>(this.ItemPart.StorePropertyBag, StoreObjectSchema.ItemClass, string.Empty);
				bool isPost = ObjectClass.IsPost(property);
				if (isSelected)
				{
					RenderingUtilities.RenderActiveActionIcons(this.Writer, this.UserContext, isPost);
					return;
				}
				RenderingUtilities.RenderGhostActionIcons(this.Writer, this.UserContext, isPost);
			}

			// Token: 0x06002317 RID: 8983 RVA: 0x000C97D0 File Offset: 0x000C79D0
			private void RenderSubHeader()
			{
				this.Writer.Write("<div id=\"divExpSubHdr\"><div id=\"divSubFs\">");
				SanitizingStringBuilder<OwaHtml> sanitizingStringBuilder = new SanitizingStringBuilder<OwaHtml>();
				this.RenderFolders(sanitizingStringBuilder, false);
				this.Writer.Write(sanitizingStringBuilder.ToSanitizedString<SanitizedHtmlString>());
				sanitizingStringBuilder.Clear();
				this.Writer.Write("</div><div id=\"divSubSent\">");
				this.RenderSentTime(sanitizingStringBuilder);
				this.Writer.Write(sanitizingStringBuilder.ToSanitizedString<SanitizedHtmlString>());
				this.Writer.Write("</div></div>");
			}

			// Token: 0x06002318 RID: 8984 RVA: 0x000C984A File Offset: 0x000C7A4A
			private void RenderSubHeaderInSender(SanitizingStringBuilder<OwaHtml> stringBuilder)
			{
				this.RenderFolders(stringBuilder, true);
				this.RenderSentTime(stringBuilder);
			}

			// Token: 0x06002319 RID: 8985 RVA: 0x000C985C File Offset: 0x000C7A5C
			private void RenderSentTime(SanitizingStringBuilder<OwaHtml> stringBuilder)
			{
				stringBuilder.Append("<span id=\"spnSent\">");
				ExDateTime property = ItemUtility.GetProperty<ExDateTime>(this.ItemPart.StorePropertyBag, ItemSchema.ReceivedTime, ExDateTime.MinValue);
				if (property != ExDateTime.MinValue)
				{
					RenderingUtilities.RenderSentTime(stringBuilder, property, this.UserContext);
				}
				else
				{
					stringBuilder.Append("&nbsp;");
				}
				stringBuilder.Append("</span>");
			}

			// Token: 0x0600231A RID: 8986 RVA: 0x000C98C1 File Offset: 0x000C7AC1
			protected virtual void RenderToolbar()
			{
			}

			// Token: 0x0600231B RID: 8987 RVA: 0x000C98C3 File Offset: 0x000C7AC3
			private void RenderBranchTextPlaceholder(SanitizedHtmlString branchTextHtml)
			{
				this.Writer.Write("<div id=\"divBrTxtOnExpansion\">");
				if (branchTextHtml != null)
				{
					this.Writer.Write(branchTextHtml);
				}
				this.Writer.Write("</div>");
			}

			// Token: 0x0600231C RID: 8988 RVA: 0x000C98F4 File Offset: 0x000C7AF4
			protected virtual void RenderWells(bool shouldRenderAttachmentWell, ArrayList attachmentWellRenderObjects)
			{
				ItemRecipientWell recipientWell = this.GetRecipientWell();
				this.Writer.Write("<div id=\"divRws\">");
				this.RenderRecipientWell(recipientWell, RecipientWellType.To, -829627742, "To");
				this.RenderRecipientWell(recipientWell, RecipientWellType.Cc, -798075995, "Cc");
				this.RenderRecipientWell(recipientWell, RecipientWellType.Bcc, 503783879, "Bcc");
				this.Writer.Write("</div>");
				this.RenderAttachmentWell(shouldRenderAttachmentWell, attachmentWellRenderObjects, this.ItemPartWriter.IsUsageRightRestricted(this.ItemPart, ContentRight.Extract));
				this.RenderCategoryWell();
			}

			// Token: 0x0600231D RID: 8989 RVA: 0x000C997E File Offset: 0x000C7B7E
			protected virtual ItemRecipientWell GetRecipientWell()
			{
				return new ItemPartRecipientWell(this.ItemPart);
			}

			// Token: 0x0600231E RID: 8990 RVA: 0x000C998B File Offset: 0x000C7B8B
			protected virtual void RenderVoicemailPlayer()
			{
			}

			// Token: 0x0600231F RID: 8991 RVA: 0x000C9990 File Offset: 0x000C7B90
			private void RenderBody()
			{
				ItemPartWriter.JunkAndPhishingInfo junkAndPhishingInfo = ItemPartWriter.GetJunkAndPhishingInfo(this.ItemPart.StorePropertyBag);
				this.Writer.Write("<div id=\"divBdy\" class=\"bdyItmPrt\" _fAllwCM=\"1\">");
				if (!this.ItemPart.DidLoadSucceed)
				{
					this.Writer.Write(this.ItemPartWriter.GetAlternateBodyForMessageLoadFailure(this.ItemPart));
				}
				else
				{
					if (junkAndPhishingInfo.IsJunkOrPhishing)
					{
						using (StringReader stringReader = new StringReader(this.ItemPart.BodyPart))
						{
							HtmlToText htmlToText = new HtmlToText();
							TextToHtml textToHtml = new TextToHtml();
							using (MemoryStream memoryStream = new MemoryStream())
							{
								using (StreamWriter streamWriter = Utilities.CreateStreamWriter(memoryStream))
								{
									htmlToText.Convert(stringReader, streamWriter);
									memoryStream.Seek(0L, SeekOrigin.Begin);
									using (StreamReader streamReader = new StreamReader(memoryStream))
									{
										textToHtml.FilterHtml = true;
										textToHtml.OutputHtmlFragment = true;
										textToHtml.HtmlTagCallback = new HtmlTagCallback(BodyConversionUtilities.RemoveLinkCallback);
										textToHtml.Convert(streamReader, this.Writer);
									}
								}
							}
							goto IL_16C;
						}
					}
					if (this.ItemPartWriter.IsRestrictedButFeatureDisabledOrDecryptionFailed(this.ItemPart))
					{
						this.Writer.Write(this.ItemPartWriter.GetAlternateBodyForIrm(this.ItemPart, Microsoft.Exchange.Data.Storage.BodyFormat.TextHtml));
					}
					else
					{
						try
						{
							using (HtmlWriter htmlWriter = new HtmlWriter(this.Writer))
							{
								this.ItemPart.WriteUniquePart(htmlWriter);
							}
						}
						catch (TextConvertersException)
						{
							throw new OwaUnsupportedConversationItemException();
						}
					}
				}
				IL_16C:
				this.Writer.Write("</div>");
			}

			// Token: 0x06002320 RID: 8992 RVA: 0x000C9B68 File Offset: 0x000C7D68
			protected virtual Infobar BuildInfobar()
			{
				Infobar infobar = new Infobar();
				IStorePropertyBag storePropertyBag = this.ItemPart.StorePropertyBag;
				RenderingUtilities.RenderReplyForwardMessageStatus(storePropertyBag, infobar, this.UserContext);
				if (!this.IsDraft)
				{
					InfobarMessageBuilder.AddImportance(infobar, storePropertyBag);
					InfobarMessageBuilder.AddSensitivity(infobar, storePropertyBag);
					InfobarMessageBuilder.AddFlag(infobar, storePropertyBag, this.UserContext);
					InfobarMessageBuilder.AddCompliance(this.UserContext, infobar, storePropertyBag, false);
					InfobarMessageBuilder.AddDeletePolicyInformation(infobar, storePropertyBag, this.UserContext);
				}
				string property = ItemUtility.GetProperty<string>(this.ItemPart.StorePropertyBag, StoreObjectSchema.ItemClass, string.Empty);
				if (TextMessagingUtilities.NeedToAddUnsyncedMessageInfobar(property, storePropertyBag, this.UserContext.MailboxSession))
				{
					infobar.AddMessage(SanitizedHtmlString.FromStringId(882347163), InfobarMessageType.Informational);
				}
				if (this.ItemPart.IrmInfo.IsRestricted && this.UserContext.IsIrmEnabled && !this.ItemPart.IrmInfo.DecryptionStatus.Failed)
				{
					bool addRemoveLink = this.ItemPart.IrmInfo.UsageRights.IsUsageRightGranted(ContentRight.Export) && !ObjectClass.IsVoiceMessage(property);
					InfobarMessageBuilder.AddIrmInformation(infobar, this.ItemPart, addRemoveLink);
				}
				ItemPartWriter.JunkAndPhishingInfo junkAndPhishingInfo = ItemPartWriter.GetJunkAndPhishingInfo(storePropertyBag);
				if (junkAndPhishingInfo.IsInJunkEmailFolder)
				{
					if (junkAndPhishingInfo.IsSuspectedPhishingItem)
					{
						infobar.AddMessage(SanitizedHtmlString.Format("{0} {1}", new object[]
						{
							LocalizedStrings.GetNonEncoded(1581910613),
							LocalizedStrings.GetNonEncoded(-2026026928)
						}), InfobarMessageType.Phishing);
					}
					else if (this.UserContext.IsJunkEmailEnabled)
					{
						infobar.AddMessage(SanitizedHtmlString.Format("{0} {1}", new object[]
						{
							LocalizedStrings.GetNonEncoded(59853257),
							LocalizedStrings.GetNonEncoded(-2129859766)
						}), InfobarMessageType.JunkEmail);
					}
				}
				else if (junkAndPhishingInfo.IsSuspectedPhishingItem && !junkAndPhishingInfo.ItemLinkEnabled)
				{
					string str = string.Format(CultureInfo.InvariantCulture, ">{0}</a> {1} ", new object[]
					{
						LocalizedStrings.GetHtmlEncoded(-672110188),
						LocalizedStrings.GetHtmlEncoded(-1020475744)
					});
					string s = "<a id=\"aIbBlk\" href=\"#\" " + str;
					string str2 = Utilities.JavascriptEncode(Utilities.BuildEhcHref(HelpIdsLight.EmailSafetyLight.ToString()));
					SanitizedEventHandlerString scriptHandler = Utilities.GetScriptHandler("onclick", "opnHlp(\"" + str2 + "\");");
					string s2 = string.Format(CultureInfo.InvariantCulture, "<a href=\"#\" " + scriptHandler + ">{0}</a>", new object[]
					{
						LocalizedStrings.GetHtmlEncoded(338562664)
					});
					infobar.AddMessage(SanitizedHtmlString.Format("{0}{1}{2}", new object[]
					{
						LocalizedStrings.GetNonEncoded(1581910613),
						SanitizedHtmlString.GetSanitizedStringWithoutEncoding(s),
						SanitizedHtmlString.GetSanitizedStringWithoutEncoding(s2)
					}), InfobarMessageType.Phishing);
				}
				return infobar;
			}

			// Token: 0x06002321 RID: 8993 RVA: 0x000C9E30 File Offset: 0x000C8030
			private void RenderFolders(SanitizingStringBuilder<OwaHtml> stringBuilder, bool isInSender)
			{
				IList<IStorePropertyBag> storePropertyBags = this.ConversationTreeNode.StorePropertyBags;
				if (!isInSender)
				{
					stringBuilder.Append("<div id=divFs><div class=divTx>");
				}
				else
				{
					stringBuilder.Append("<span class=\"spnFolders\">");
				}
				if (this.ConversationId.ParentFolderId != null)
				{
					bool isFirstName = true;
					int num = 0;
					int num2 = 0;
					StoreObjectId storeObjectId = (StoreObjectId)storePropertyBags[0][StoreObjectSchema.ParentItemId];
					StringBuilder stringBuilder2 = new StringBuilder();
					if (storePropertyBags.Count == 1 && this.ConversationId.ParentFolderId.Equals(storeObjectId))
					{
						stringBuilder.Append("&nbsp;");
					}
					else
					{
						MailboxSession mailboxSession = (MailboxSession)this.ConversationId.GetSession(this.UserContext);
						for (int i = 1; i < storePropertyBags.Count; i++)
						{
							if (i == 0 || !storeObjectId.Equals((StoreObjectId)storePropertyBags[i][StoreObjectSchema.ParentItemId]))
							{
								this.RenderFolderName(stringBuilder2, mailboxSession, storeObjectId, num, isFirstName, !this.ConversationId.ParentFolderId.Equals(storeObjectId));
								num = 1;
								storeObjectId = (StoreObjectId)storePropertyBags[i][StoreObjectSchema.ParentItemId];
								isFirstName = false;
								num2++;
							}
							else
							{
								num++;
							}
						}
						this.RenderFolderName(stringBuilder2, mailboxSession, storeObjectId, num, isFirstName, !this.ConversationId.ParentFolderId.Equals(storeObjectId));
						num2++;
						stringBuilder.Append<SanitizedHtmlString>(SanitizedHtmlString.GetSanitizedStringWithoutEncoding(stringBuilder2.ToString()));
					}
				}
				else
				{
					stringBuilder.Append("&nbsp;");
				}
				if (!isInSender)
				{
					stringBuilder.Append("</div></div>");
					return;
				}
				stringBuilder.Append("</span>");
			}

			// Token: 0x06002322 RID: 8994 RVA: 0x000C9FC1 File Offset: 0x000C81C1
			protected virtual Toolbar GetToolbar()
			{
				return null;
			}

			// Token: 0x06002323 RID: 8995 RVA: 0x000C9FC4 File Offset: 0x000C81C4
			private void RenderAttachmentWell(bool shouldRenderAttachmentWell, ArrayList attachmentWellRenderObjects, bool isIrmCopyRestricted)
			{
				if (shouldRenderAttachmentWell && attachmentWellRenderObjects != null && attachmentWellRenderObjects.Count > 0)
				{
					string property = ItemUtility.GetProperty<string>(this.ItemPart.StorePropertyBag, StoreObjectSchema.ItemClass, string.Empty);
					bool flag = ObjectClass.IsVoiceMessage(property);
					ItemPartWriter.JunkAndPhishingInfo junkAndPhishingInfo = ItemPartWriter.GetJunkAndPhishingInfo(this.ItemPart.StorePropertyBag);
					this.Writer.Write("<div id=\"divWellAttach\" class=\"roWellRow roAttMrg\">");
					this.Writer.Write("<div id=\"divAttachL\" class=\"roCatAttWellLabel ");
					bool flag2 = false;
					int num = 0;
					if (!flag && !junkAndPhishingInfo.IsJunkOrPhishing)
					{
						num = AttachmentUtility.GetCountForDownloadAttachments(attachmentWellRenderObjects);
						if (num > 1)
						{
							flag2 = true;
							this.Writer.Write("attLbl ");
						}
					}
					this.Writer.Write("pvwLabel\">");
					this.Writer.Write(SanitizedHtmlString.FromStringId(-599039349));
					this.Writer.Write("</div>");
					if (flag2)
					{
						OwaStoreObjectId owaStoreObjectId = OwaStoreObjectId.CreateFromStoreObjectId(this.ItemPart.ItemId, this.ConversationId);
						AttachmentUtility.RenderDownloadAllAttachmentsLink(OwaContext.Current.SanitizingResponseWriter, HttpContext.Current.Request, Utilities.UrlEncode(owaStoreObjectId.ToString()), false, this.UserContext, num);
					}
					this.Writer.Write("<div class=\"roWellWrap rowAttMaxH\"><div id=\"divFieldAttach\" class=\"wellField\">");
					AttachmentWell.RenderAttachmentWell(this.Writer, AttachmentWellType.ReadOnly, attachmentWellRenderObjects, this.UserContext, isIrmCopyRestricted);
					this.Writer.Write("</div></div></div>");
				}
			}

			// Token: 0x06002324 RID: 8996 RVA: 0x000CA120 File Offset: 0x000C8320
			private void RenderCategoryWell()
			{
				if (!ItemUtility.HasCategories(this.ItemPart.StorePropertyBag))
				{
					return;
				}
				this.Writer.Write("<div id=divWellCategories class=\"roCatRow\">");
				this.Writer.Write("<div id=divCategoriesL class=\"roCatAttWellLabel pvwLabel\">");
				this.Writer.Write("<span class=nowrap>");
				this.Writer.Write(SanitizedHtmlString.FromStringId(-998639321));
				this.Writer.Write("</span></div>");
				this.Writer.Write("<div class=roWellWrap>");
				this.Writer.Write("<div id=divFieldCategories class=roHdrField>");
				CategorySwatch.RenderCategories(OwaContext.Current, this.Writer, this.ItemPart.StorePropertyBag, null);
				this.Writer.Write("</div></div></div>");
			}

			// Token: 0x06002325 RID: 8997 RVA: 0x000CA1E4 File Offset: 0x000C83E4
			private bool InitAttachmentWell(out ArrayList attachmentWellRenderObjects)
			{
				if (this.ItemPartWriter.IsRestrictedButFeatureDisabledOrDecryptionFailed(this.ItemPart))
				{
					attachmentWellRenderObjects = new ArrayList();
					return false;
				}
				string property = ItemUtility.GetProperty<string>(this.ItemPart.StorePropertyBag, StoreObjectSchema.ItemClass, string.Empty);
				if (this.ItemPart.IrmInfo.IsRestricted && this.UserContext.IsIrmEnabled && ObjectClass.IsVoiceMessage(property))
				{
					attachmentWellRenderObjects = new ArrayList();
					return false;
				}
				attachmentWellRenderObjects = AttachmentWell.GetAttachmentInformation(this.ConversationId, this.ItemPart, this.UserContext.IsPublicLogon, false, false);
				if (attachmentWellRenderObjects != null && attachmentWellRenderObjects.Count > 0 && Utilities.IsSMime(this.ItemPart.StorePropertyBag))
				{
					AttachmentUtility.RemoveSmimeAttachment(attachmentWellRenderObjects);
				}
				return RenderingUtilities.AddAttachmentInfobarMessages(this.ItemPart.StorePropertyBag, false, false, this.Infobar, attachmentWellRenderObjects);
			}

			// Token: 0x06002326 RID: 8998 RVA: 0x000CA2BC File Offset: 0x000C84BC
			private void RenderFolderName(StringBuilder builder, MailboxSession mailboxSession, StoreObjectId folderId, int folderCount, bool isFirstName, bool isCelebrated)
			{
				using (SanitizingStringWriter<OwaHtml> sanitizingStringWriter = new SanitizingStringWriter<OwaHtml>(builder))
				{
					if (!isFirstName)
					{
						sanitizingStringWriter.Write(", ");
					}
					if (isCelebrated)
					{
						sanitizingStringWriter.Write("<span class=spnCelF>");
					}
					string cachedFolderName = this.UserContext.GetCachedFolderName(folderId, mailboxSession);
					sanitizingStringWriter.Write(Utilities.SanitizeHtmlEncode(cachedFolderName));
					if (folderCount > 1)
					{
						Utilities.RenderDirectionEnhancedValue(sanitizingStringWriter, SanitizedHtmlString.Format(" [{0}]", new object[]
						{
							folderCount
						}), this.UserContext.IsRtl);
					}
					if (isCelebrated)
					{
						sanitizingStringWriter.Write("</span>");
					}
				}
			}

			// Token: 0x06002327 RID: 8999 RVA: 0x000CA368 File Offset: 0x000C8568
			private void RenderRecipientWell(RecipientWell recipientWell, RecipientWellType recipientWellType, Strings.IDs label, string name)
			{
				if (!this.HasRecipients(recipientWell, recipientWellType))
				{
					return;
				}
				this.Writer.Write("<div id=divWell{0} class=roWellRow>", name);
				this.Writer.Write("<div id=div{0}L class=\"roWellLabel pvwLabel\">", name);
				this.Writer.Write("<span class=nowrap>");
				this.Writer.Write(SanitizedHtmlString.FromStringId(label));
				this.Writer.Write("</span></div>");
				this.Writer.Write("<div class=roWellWrap>");
				this.Writer.Write("<div id=divField{0} class=wellField>", name);
				this.RenderRecipientWellContents(recipientWell, recipientWellType);
				this.Writer.Write("</div></div></div>");
			}

			// Token: 0x06002328 RID: 9000 RVA: 0x000CA40F File Offset: 0x000C860F
			protected virtual bool HasRecipients(RecipientWell recipientWell, RecipientWellType recipientWellType)
			{
				return recipientWell.HasRecipients(recipientWellType);
			}

			// Token: 0x06002329 RID: 9001 RVA: 0x000CA418 File Offset: 0x000C8618
			protected virtual void RenderRecipientWellContents(RecipientWell recipientWell, RecipientWellType recipientWellType)
			{
				recipientWell.Render(this.Writer, this.UserContext, recipientWellType, RecipientWell.RenderFlags.ReadOnly);
			}

			// Token: 0x04001899 RID: 6297
			private ItemPart itemPart;

			// Token: 0x0400189A RID: 6298
			private Infobar infoBar;
		}

		// Token: 0x020003A1 RID: 929
		private class ExpandedMeetingItemPartWriter : ItemPartWriter.ExpandedItemPartWriter
		{
			// Token: 0x0600232A RID: 9002 RVA: 0x000CA430 File Offset: 0x000C8630
			public ExpandedMeetingItemPartWriter(ItemPartWriter itemPartWriter) : base(itemPartWriter)
			{
				bool flag = false;
				try
				{
					this.item = MeetingMessage.Bind(base.ConversationId.GetSession(base.UserContext), base.ItemPart.ItemId, MeetingPageWriter.MeetingMessagePrefetchProperties);
					this.writerHelpers = this.GetMeetingPageWriter();
					flag = true;
				}
				finally
				{
					if (!flag)
					{
						this.Dispose();
					}
				}
			}

			// Token: 0x0600232B RID: 9003 RVA: 0x000CA49C File Offset: 0x000C869C
			protected override Infobar BuildInfobar()
			{
				if (this.item.Id.ObjectId.ObjectType == StoreObjectType.MeetingRequest)
				{
					return this.writerHelpers.FormInfobar;
				}
				return base.BuildInfobar();
			}

			// Token: 0x0600232C RID: 9004 RVA: 0x000CA4CC File Offset: 0x000C86CC
			protected override void RenderExpandedPartExpandos()
			{
				base.RenderExpandedPartExpandos();
				if (base.ItemPart.ItemId.ObjectType == StoreObjectType.MeetingRequest || base.ItemPart.ItemId.ObjectType == StoreObjectType.MeetingCancellation)
				{
					object obj = this.item.TryGetProperty(CalendarItemInstanceSchema.StartTime);
					if (!(obj is ExDateTime))
					{
						obj = DateTimeUtilities.GetLocalTime();
					}
					base.Writer.Write(" dtST='");
					RenderingUtilities.RenderDateTimeScriptObject(base.Writer, (ExDateTime)obj);
					base.Writer.Write("' ");
				}
				RenderingUtilities.RenderExpando(base.Writer, "sCK", this.item.Id.ChangeKeyAsBase64String());
				if (this.writerHelpers != null && !this.IsDraft && this.IsOwnerMailboxSession && (this.IsItemFromOtherMailbox || this.IsDelegated) && (this.item.Id.ObjectId.ObjectType == StoreObjectType.MeetingRequest || this.item.Id.ObjectId.ObjectType == StoreObjectType.MeetingCancellation))
				{
					string value = null;
					string text = null;
					bool flag = false;
					try
					{
						value = this.writerHelpers.GetPrincipalCalendarFolderId(false);
					}
					catch (OwaSharedFromOlderVersionException)
					{
						flag = true;
						CalendarUtilities.GetReceiverGSCalendarIdStringAndDisplayName(base.UserContext, this.item, out value, out text);
					}
					if (!string.IsNullOrEmpty(value))
					{
						RenderingUtilities.RenderExpando(base.Writer, "pCFId", value);
					}
					if (flag)
					{
						RenderingUtilities.RenderExpando(base.Writer, "fOlderVersion", "1");
						this.writerHelpers.FormInfobar.AddMessage(SanitizedHtmlString.Format(LocalizedStrings.GetNonEncoded(1896884103), new object[]
						{
							text
						}), InfobarMessageType.Informational);
					}
				}
			}

			// Token: 0x0600232D RID: 9005 RVA: 0x000CA684 File Offset: 0x000C8884
			protected override void RenderWells(bool shouldRenderAttachmentWell, ArrayList attachmentWellRenderObjects)
			{
				base.RenderWells(shouldRenderAttachmentWell, attachmentWellRenderObjects);
			}

			// Token: 0x0600232E RID: 9006 RVA: 0x000CA690 File Offset: 0x000C8890
			protected override bool HasRecipients(RecipientWell recipientWell, RecipientWellType recipientWellType)
			{
				string itemClass = string.Empty;
				object obj = this.item.TryGetProperty(StoreObjectSchema.ItemClass);
				if (!(obj is PropertyError))
				{
					itemClass = (string)obj;
				}
				if (ObjectClass.IsOfClass(itemClass, "IPM.Schedule.Meeting.Request") && this.item.IsDelegated())
				{
					string displayAttendees = CalendarUtilities.GetDisplayAttendees((MeetingRequest)this.item, recipientWellType);
					return !string.IsNullOrEmpty(displayAttendees);
				}
				return base.HasRecipients(recipientWell, recipientWellType);
			}

			// Token: 0x0600232F RID: 9007 RVA: 0x000CA704 File Offset: 0x000C8904
			protected override void RenderRecipientWellContents(RecipientWell recipientWell, RecipientWellType recipientWellType)
			{
				string itemClass = string.Empty;
				object obj = this.item.TryGetProperty(StoreObjectSchema.ItemClass);
				if (!(obj is PropertyError))
				{
					itemClass = (string)obj;
				}
				if (ObjectClass.IsOfClass(itemClass, "IPM.Schedule.Meeting.Request") && this.item.IsDelegated())
				{
					string displayAttendees = CalendarUtilities.GetDisplayAttendees((MeetingRequest)this.item, recipientWellType);
					Utilities.SanitizeHtmlEncode(displayAttendees, base.Writer);
					return;
				}
				base.RenderRecipientWellContents(recipientWell, recipientWellType);
			}

			// Token: 0x06002330 RID: 9008 RVA: 0x000CA778 File Offset: 0x000C8978
			protected override Toolbar GetToolbar()
			{
				switch (base.ItemPart.ItemId.ObjectType)
				{
				case StoreObjectType.MeetingRequest:
				case StoreObjectType.MeetingCancellation:
					return this.writerHelpers.Toolbar;
				}
				return null;
			}

			// Token: 0x06002331 RID: 9009 RVA: 0x000CA7BA File Offset: 0x000C89BA
			protected override void RenderToolbar()
			{
				this.writerHelpers.RenderMeetingInfoArea(base.Writer, true);
			}

			// Token: 0x17000965 RID: 2405
			// (get) Token: 0x06002332 RID: 9010 RVA: 0x000CA7D0 File Offset: 0x000C89D0
			protected override bool IsDraft
			{
				get
				{
					object obj = this.item.TryGetProperty(MessageItemSchema.IsDraft);
					return obj is bool && (bool)obj;
				}
			}

			// Token: 0x17000966 RID: 2406
			// (get) Token: 0x06002333 RID: 9011 RVA: 0x000CA7FE File Offset: 0x000C89FE
			protected bool IsOwnerMailboxSession
			{
				get
				{
					return base.UserContext.MailboxSession.LogonType == LogonType.Owner;
				}
			}

			// Token: 0x17000967 RID: 2407
			// (get) Token: 0x06002334 RID: 9012 RVA: 0x000CA813 File Offset: 0x000C8A13
			protected bool IsDelegated
			{
				get
				{
					return this.item.IsDelegated();
				}
			}

			// Token: 0x17000968 RID: 2408
			// (get) Token: 0x06002335 RID: 9013 RVA: 0x000CA820 File Offset: 0x000C8A20
			protected bool IsItemFromOtherMailbox
			{
				get
				{
					return base.UserContext.IsInOtherMailbox(this.item);
				}
			}

			// Token: 0x06002336 RID: 9014 RVA: 0x000CA833 File Offset: 0x000C8A33
			protected override void InternalDispose(bool isDisposing)
			{
				if (this.writerHelpers != null)
				{
					this.writerHelpers.Dispose();
					this.writerHelpers = null;
				}
				if (this.item != null)
				{
					this.item.Dispose();
					this.item = null;
				}
				base.InternalDispose(isDisposing);
			}

			// Token: 0x06002337 RID: 9015 RVA: 0x000CA870 File Offset: 0x000C8A70
			protected override DisposeTracker InternalGetDisposeTracker()
			{
				return DisposeTracker.Get<ItemPartWriter.ExpandedMeetingItemPartWriter>(this);
			}

			// Token: 0x06002338 RID: 9016 RVA: 0x000CA878 File Offset: 0x000C8A78
			protected override ItemRecipientWell GetRecipientWell()
			{
				if (this.item.Id.ObjectId.ObjectType == StoreObjectType.MeetingRequest)
				{
					return new MeetingRequestRecipientWell((MeetingRequest)this.item);
				}
				return base.GetRecipientWell();
			}

			// Token: 0x06002339 RID: 9017 RVA: 0x000CA8AC File Offset: 0x000C8AAC
			private MeetingPageWriter GetMeetingPageWriter()
			{
				JunkEmailUtilities.GetJunkEmailPropertiesForItem(this.item, false, false, base.UserContext, out this.isInJunkEmailFolder, out this.isSuspectedPhishingItem, out this.itemLinkEnabled, out this.isJunkOrPhishing);
				if (this.item.Id.ObjectId.ObjectType == StoreObjectType.MeetingRequest)
				{
					return new MeetingInviteWriter((MeetingRequest)this.item, base.UserContext, "mpToolbar", true, false, false, this.isInJunkEmailFolder, this.isSuspectedPhishingItem, this.itemLinkEnabled);
				}
				if (this.item.Id.ObjectId.ObjectType == StoreObjectType.MeetingCancellation)
				{
					return new MeetingCancelWriter((MeetingCancellation)this.item, base.UserContext, "mpToolbar", true, base.IsDeleted, false, this.isInJunkEmailFolder, this.isSuspectedPhishingItem, true);
				}
				if (this.item.Id.ObjectId.ObjectType == StoreObjectType.MeetingResponse)
				{
					return new MeetingResponseWriter((MeetingResponse)this.item, base.UserContext, true, false, false, this.isInJunkEmailFolder, this.isSuspectedPhishingItem, true);
				}
				return null;
			}

			// Token: 0x0400189C RID: 6300
			private MeetingMessage item;

			// Token: 0x0400189D RID: 6301
			private MeetingPageWriter writerHelpers;

			// Token: 0x0400189E RID: 6302
			private bool isInJunkEmailFolder;

			// Token: 0x0400189F RID: 6303
			private bool isSuspectedPhishingItem;

			// Token: 0x040018A0 RID: 6304
			private bool itemLinkEnabled;

			// Token: 0x040018A1 RID: 6305
			private bool isJunkOrPhishing;
		}

		// Token: 0x020003A2 RID: 930
		private class ExpandedMessageItemPartWriter : ItemPartWriter.ExpandedItemPartWriter
		{
			// Token: 0x0600233A RID: 9018 RVA: 0x000CA9B9 File Offset: 0x000C8BB9
			public ExpandedMessageItemPartWriter(ItemPartWriter itemPartWriter) : base(itemPartWriter)
			{
			}

			// Token: 0x0600233B RID: 9019 RVA: 0x000CA9C4 File Offset: 0x000C8BC4
			protected override void RenderVoicemailPlayer()
			{
				string property = ItemUtility.GetProperty<string>(base.ItemPart.StorePropertyBag, StoreObjectSchema.ItemClass, string.Empty);
				if (base.UserContext.BrowserType == BrowserType.IE && Utilities.GetBrowserPlatform(HttpContext.Current.Request.UserAgent) != BrowserPlatform.Macintosh && ObjectClass.IsVoiceMessage(property))
				{
					using (UMClientCommon umclientCommon = new UMClientCommon(base.UserContext.ExchangePrincipal))
					{
						if (!umclientCommon.IsUMEnabled())
						{
							return;
						}
						if (base.ItemPart.IrmInfo.IsRestricted && base.UserContext.IsIrmEnabled)
						{
							string property2 = ItemUtility.GetProperty<string>(base.ItemPart.StorePropertyBag, MessageItemSchema.RequireProtectedPlayOnPhone, string.Empty);
							if (property2.Equals("true", StringComparison.OrdinalIgnoreCase))
							{
								return;
							}
						}
					}
					string latestVoiceMailFileName = Utilities.GetLatestVoiceMailFileName(base.ItemPart.StorePropertyBag);
					if (latestVoiceMailFileName != null)
					{
						foreach (AttachmentInfo attachmentInfo in base.ItemPart.Attachments)
						{
							if (string.Equals(attachmentInfo.FileName, latestVoiceMailFileName, StringComparison.InvariantCultureIgnoreCase))
							{
								AttachmentPolicy.Level attachmentLevel = AttachmentLevelLookup.GetAttachmentLevel(attachmentInfo, base.UserContext);
								if (attachmentLevel == AttachmentPolicy.Level.Block)
								{
									return;
								}
								base.Writer.Write("<div class=\"divMp\"><div id=\"divMp\">");
								base.Writer.Write("<object id=\"oMpf\" classid=\"clsid:6bf52a52-394a-11d3-b153-00c04f79faa6\" type=\"application/x-oleobject\" width=\"212\" height=\"45\">");
								base.Writer.Write("<param name=\"autoStart\" value=\"false\"/>");
								base.Writer.Write("<param name=\"EnableContextMenu\" value=\"0\"/>");
								base.Writer.Write("<param name=\"InvokeURLs\" value=\"0\"/>");
								base.Writer.Write("<param name=\"URL\" value=\"");
								Utilities.WriteLatestUrlToAttachment(base.Writer, Utilities.GetItemIdString(base.ItemPart.ItemId, base.ConversationId), base.ItemPart.Attachments[0].FileExtension);
								base.Writer.Write("\"/>");
								base.Writer.Write("</object>");
								base.Writer.Write("</div></div>");
							}
						}
						return;
					}
					return;
				}
			}
		}

		// Token: 0x020003A3 RID: 931
		private class ExpandedSharingItemPartWriter : ItemPartWriter.ExpandedItemPartWriter
		{
			// Token: 0x0600233C RID: 9020 RVA: 0x000CAC08 File Offset: 0x000C8E08
			public ExpandedSharingItemPartWriter(SharingMessageItem sharingMessageItem, ItemPartWriter itemPartWriter) : base(itemPartWriter)
			{
				if (sharingMessageItem == null)
				{
					throw new ArgumentNullException("sharingMessageItem");
				}
				this.sharingMessageItem = sharingMessageItem;
				this.sharingMessageWriter = new SharingMessageWriter(this.sharingMessageItem, base.UserContext);
			}

			// Token: 0x0600233D RID: 9021 RVA: 0x000CAC40 File Offset: 0x000C8E40
			protected override void RenderExpandedPartExpandos()
			{
				base.RenderExpandedPartExpandos();
				if (this.sharingMessageItem.BrowseUrl != null)
				{
					RenderingUtilities.RenderExpando(base.Writer, "sBrowseUrl", this.sharingMessageItem.BrowseUrl);
					RenderingUtilities.RenderExpando(base.Writer, "sRedirectBrowseUrl", Redir.BuildRedirUrl(base.UserContext, this.sharingMessageItem.BrowseUrl));
				}
			}

			// Token: 0x0600233E RID: 9022 RVA: 0x000CACA1 File Offset: 0x000C8EA1
			protected override Toolbar GetToolbar()
			{
				if (this.sharingMessageWriter.ShouldShowSharingToolbar)
				{
					return this.sharingMessageWriter.SharingToolbar;
				}
				return null;
			}

			// Token: 0x0600233F RID: 9023 RVA: 0x000CACC0 File Offset: 0x000C8EC0
			protected override Infobar BuildInfobar()
			{
				Infobar infobar = base.BuildInfobar();
				this.sharingMessageWriter.AddSharingInfoToInfobar(infobar);
				return infobar;
			}

			// Token: 0x06002340 RID: 9024 RVA: 0x000CACE1 File Offset: 0x000C8EE1
			protected override void RenderWells(bool shouldRenderAttachmentWell, ArrayList attachmentWellRenderObjects)
			{
				base.RenderWells(shouldRenderAttachmentWell, attachmentWellRenderObjects);
			}

			// Token: 0x06002341 RID: 9025 RVA: 0x000CACEC File Offset: 0x000C8EEC
			protected override void RenderToolbar()
			{
				if (this.sharingMessageItem.IsPublishing)
				{
					this.sharingMessageWriter.RenderPublishLinks(base.Writer, true);
				}
				else
				{
					this.sharingMessageWriter.RenderSharingSetting(base.Writer, true);
				}
				this.sharingMessageWriter.RenderFolderInformation(base.Writer);
				Toolbar toolbar = this.GetToolbar();
				if (toolbar != null)
				{
					toolbar.Render(base.Writer);
				}
			}

			// Token: 0x06002342 RID: 9026 RVA: 0x000CAD53 File Offset: 0x000C8F53
			protected override void InternalDispose(bool isDisposing)
			{
				this.sharingMessageItem.Dispose();
				base.InternalDispose(isDisposing);
			}

			// Token: 0x06002343 RID: 9027 RVA: 0x000CAD67 File Offset: 0x000C8F67
			protected override DisposeTracker InternalGetDisposeTracker()
			{
				return DisposeTracker.Get<ItemPartWriter.ExpandedSharingItemPartWriter>(this);
			}

			// Token: 0x040018A2 RID: 6306
			private SharingMessageItem sharingMessageItem;

			// Token: 0x040018A3 RID: 6307
			private SharingMessageWriter sharingMessageWriter;
		}
	}
}
