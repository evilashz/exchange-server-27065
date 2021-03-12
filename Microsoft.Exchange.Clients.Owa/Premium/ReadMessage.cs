using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Clients.Owa.Premium.Controls;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Security;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x02000470 RID: 1136
	public class ReadMessage : OwaFormSubPage, IRegistryOnlyForm
	{
		// Token: 0x17000C87 RID: 3207
		// (get) Token: 0x06002ADB RID: 10971 RVA: 0x000F13DA File Offset: 0x000EF5DA
		// (set) Token: 0x06002ADC RID: 10972 RVA: 0x000F13E2 File Offset: 0x000EF5E2
		internal MessageItem Message
		{
			get
			{
				return this.message;
			}
			set
			{
				this.message = value;
			}
		}

		// Token: 0x06002ADD RID: 10973 RVA: 0x000F13EC File Offset: 0x000EF5EC
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			string itemType = base.GetItemType();
			StorePropertyDefinition[] prefetchProperties = new StorePropertyDefinition[]
			{
				ItemSchema.BlockStatus,
				ItemSchema.IsClassified,
				ItemSchema.Classification,
				ItemSchema.ClassificationDescription,
				ItemSchema.ClassificationGuid,
				ItemSchema.EdgePcl,
				ItemSchema.LinkEnabled,
				BodySchema.Codepage,
				BodySchema.InternetCpid,
				MessageItemSchema.SenderTelephoneNumber,
				ItemSchema.FlagStatus,
				ItemSchema.FlagCompleteTime,
				MessageItemSchema.ReplyTime,
				ItemSchema.UtcDueDate,
				ItemSchema.UtcStartDate,
				ItemSchema.ReminderDueBy,
				ItemSchema.ReminderIsSet,
				StoreObjectSchema.EffectiveRights,
				ItemSchema.Categories,
				MessageItemSchema.IsReadReceiptPending,
				MessageItemSchema.ApprovalDecision,
				MessageItemSchema.ApprovalDecisionMaker,
				MessageItemSchema.ApprovalDecisionTime,
				StoreObjectSchema.PolicyTag,
				ItemSchema.RetentionDate,
				MessageItemSchema.TextMessageDeliveryStatus,
				StoreObjectSchema.ParentItemId
			};
			if (ObjectClass.IsMessage(itemType, false))
			{
				this.Message = base.Initialize<MessageItem>(prefetchProperties);
			}
			else
			{
				this.Message = base.InitializeAsMessageItem(prefetchProperties);
			}
			this.IrmItemHelper = new IRMItemHelper(this.Message, base.UserContext, base.IsPreviewForm, base.IsEmbeddedItem);
			this.IrmItemHelper.IrmDecryptIfRestricted();
			if (ObjectClass.IsOfClass(itemType, "IPM.Note.Microsoft.Fax.CA"))
			{
				this.isFaxMessage = true;
			}
			this.InitializeReadMessageFormElements();
			if (!this.IsSMimeItem)
			{
				RenderingUtilities.RenderVotingInfobarMessages(this.Message, this.infobar, base.UserContext);
			}
			object obj = this.Message.TryGetProperty(MessageItemSchema.IsDraft);
			if (obj is bool && (bool)obj)
			{
				this.isDraftMessage = true;
				if (!base.IsPreviewForm)
				{
					this.infobar.AddMessage(SanitizedHtmlString.FromStringId(-1981719796), InfobarMessageType.Informational);
				}
				this.AddIrmMessageToInfobar();
			}
			else
			{
				this.AddMessagesToInfobar();
				if (this.Message.Id != null && !base.IsEmbeddedItem && !this.Message.IsRead && !base.IsPreviewForm)
				{
					this.Message.MarkAsRead(Utilities.ShouldSuppressReadReceipt(base.UserContext, this.Message), false);
				}
			}
			SanitizedHtmlString sanitizedHtmlString = null;
			if (Utilities.IsFlagSet((int)base.ClientSMimeControlStatus, 1))
			{
				if (this.IsClearSignedItem || this.IsOpaqueSignedItem)
				{
					sanitizedHtmlString = SanitizedHtmlString.FromStringId(Utilities.IsSMimeFeatureUsable(base.OwaContext) ? (base.IsPreviewForm ? 1871698343 : 1683614199) : -1329088272);
				}
				else if (this.IsEncryptedItem)
				{
					sanitizedHtmlString = SanitizedHtmlString.FromStringId(Utilities.IsSMimeFeatureUsable(base.OwaContext) ? (base.IsPreviewForm ? 958219031 : 906798671) : -767943720);
				}
			}
			else if (this.IsSMimeItem)
			{
				if (Utilities.IsFlagSet((int)base.ClientSMimeControlStatus, 16))
				{
					if (Utilities.IsFlagSet((int)base.ClientSMimeControlStatus, 2))
					{
						sanitizedHtmlString = SanitizedHtmlString.FromStringId(base.IsPreviewForm ? -1214530702 : 1697878138);
					}
					else if (Utilities.IsFlagSet((int)base.ClientSMimeControlStatus, 4))
					{
						sanitizedHtmlString = SanitizedHtmlString.FromStringId(base.IsPreviewForm ? 1899236370 : 330022834);
					}
				}
				else
				{
					sanitizedHtmlString = SanitizedHtmlString.FromStringId((this.IsClearSignedItem || this.IsOpaqueSignedItem) ? 1965026784 : -514535677);
				}
			}
			if (sanitizedHtmlString != null)
			{
				this.infobar.AddMessage(sanitizedHtmlString, InfobarMessageType.Warning);
			}
		}

		// Token: 0x06002ADE RID: 10974 RVA: 0x000F1774 File Offset: 0x000EF974
		protected void InitializeReadMessageFormElements()
		{
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			JunkEmailUtilities.GetJunkEmailPropertiesForItem(this.Message, base.IsEmbeddedItem, base.ForceEnableItemLink, base.UserContext, out this.isInJunkEmailFolder, out flag, out flag2, out flag3);
			this.isSuspectedPhishingItemWithoutLinkEnabled = (flag && !flag2);
			this.toolbar = new ReadMessageToolbar(base.IsInDeleteItems, base.IsEmbeddedItem, this.Message, this.isInJunkEmailFolder, flag, flag2, true, this.IrmItemHelper != null && this.IrmItemHelper.IsReplyRestricted, this.IrmItemHelper != null && this.IrmItemHelper.IsReplyAllRestricted, this.IrmItemHelper != null && this.IrmItemHelper.IsForwardRestricted, this.IrmItemHelper != null && this.IrmItemHelper.IsPrintRestricted);
			this.toolbar.ToolbarType = (base.IsPreviewForm ? ToolbarType.Preview : ToolbarType.Form);
			if (flag3)
			{
				this.bodyMarkup = Markup.PlainText;
			}
			this.recipientWell = new MessageRecipientWell(this.Message);
			RenderingUtilities.RenderReplyForwardMessageStatus(this.Message, this.infobar, base.UserContext);
		}

		// Token: 0x06002ADF RID: 10975 RVA: 0x000F1888 File Offset: 0x000EFA88
		protected virtual void AddMessagesToInfobar()
		{
			InfobarMessageBuilder.AddImportance(this.infobar, this.Message);
			InfobarMessageBuilder.AddSensitivity(this.infobar, this.Message);
			InfobarMessageBuilder.AddFlag(this.infobar, this.Message, base.UserContext);
			InfobarMessageBuilder.AddCompliance(base.UserContext, this.infobar, this.Message, false);
			InfobarMessageBuilder.AddDeletePolicyInformation(this.infobar, this.Message, base.UserContext);
			this.AddIrmMessageToInfobar();
			if (!base.IsEmbeddedItem && !this.IsPublicItem)
			{
				InfobarMessageBuilder.AddReadReceiptNotice(base.UserContext, this.infobar, this.Message);
			}
			if (ObjectClass.IsTaskRequest(this.Message.ClassName))
			{
				this.infobar.AddMessage(SanitizedHtmlString.FromStringId(357315796), InfobarMessageType.Informational);
			}
			if (TextMessagingUtilities.NeedToAddUnsyncedMessageInfobar(this.Message.ClassName, this.Message, base.UserContext.MailboxSession))
			{
				this.infobar.AddMessage(SanitizedHtmlString.FromStringId(882347163), InfobarMessageType.Informational);
			}
		}

		// Token: 0x06002AE0 RID: 10976 RVA: 0x000F198C File Offset: 0x000EFB8C
		protected void LoadMessageBodyIntoStream(TextWriter writer)
		{
			if (this.IrmItemHelper != null && this.IrmItemHelper.IsRestrictedButIrmFeatureDisabledOrDecryptionFailed)
			{
				this.IrmItemHelper.RenderAlternateBodyForIrm(writer, false);
				return;
			}
			string action = base.IsPreviewForm ? "Preview" : string.Empty;
			string attachmentUrl = null;
			if (base.IsEmbeddedItemInNonSMimeItem)
			{
				attachmentUrl = base.RenderEmbeddedUrl();
			}
			base.AttachmentLinks = BodyConversionUtilities.GenerateNonEditableMessageBodyAndRenderInfobarMessages(this.Message, writer, base.OwaContext, this.infobar, base.ForceAllowWebBeacon, base.ForceEnableItemLink, this.Message.ClassName, action, string.Empty, base.IsEmbeddedItemInNonSMimeItem, attachmentUrl);
		}

		// Token: 0x06002AE1 RID: 10977 RVA: 0x000F1A24 File Offset: 0x000EFC24
		protected void CreateAttachmentHelpers()
		{
			if (this.IrmItemHelper != null && this.IrmItemHelper.IsRestrictedButIrmFeatureDisabledOrDecryptionFailed)
			{
				this.shouldRenderAttachmentWell = false;
				return;
			}
			this.attachmentWellRenderObjects = AttachmentWell.GetAttachmentInformation(this.Message, base.AttachmentLinks, base.UserContext.IsPublicLogon, base.IsEmbeddedItem, base.ForceEnableItemLink);
			if (this.attachmentWellRenderObjects != null && this.attachmentWellRenderObjects.Count > 0 && this.IsSMimeItem)
			{
				AttachmentUtility.RemoveSmimeAttachment(this.attachmentWellRenderObjects);
			}
			base.SetShouldRenderDownloadAllLink(this.attachmentWellRenderObjects);
			this.shouldRenderAttachmentWell = RenderingUtilities.AddAttachmentInfobarMessages(base.Item, base.IsEmbeddedItem, base.ForceEnableItemLink, this.infobar, this.attachmentWellRenderObjects);
		}

		// Token: 0x17000C88 RID: 3208
		// (get) Token: 0x06002AE2 RID: 10978 RVA: 0x000F1ADA File Offset: 0x000EFCDA
		protected bool HasSender
		{
			get
			{
				return this.Message.Sender != null;
			}
		}

		// Token: 0x06002AE3 RID: 10979 RVA: 0x000F1AF0 File Offset: 0x000EFCF0
		protected void RenderSender(TextWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (!this.isFaxMessage)
			{
				RenderingUtilities.RenderSender(base.UserContext, writer, this.Message, new RenderSubHeaderDelegate(this.RenderSentTime));
				return;
			}
			writer.Write(UnifiedMessagingUtilities.GetUMSender(base.UserContext, this.Message, "spnSender"));
		}

		// Token: 0x06002AE4 RID: 10980 RVA: 0x000F1B50 File Offset: 0x000EFD50
		protected void RenderSentTime()
		{
			SanitizingStringBuilder<OwaHtml> sanitizingStringBuilder = new SanitizingStringBuilder<OwaHtml>();
			this.RenderSentTime(sanitizingStringBuilder);
			base.SanitizingResponse.Write(sanitizingStringBuilder.ToSanitizedString<SanitizedHtmlString>());
		}

		// Token: 0x06002AE5 RID: 10981 RVA: 0x000F1B7B File Offset: 0x000EFD7B
		protected void RenderSentTime(SanitizingStringBuilder<OwaHtml> stringBuilder)
		{
			stringBuilder.Append("<span id=\"spnSent\">");
			RenderingUtilities.RenderSentTime(stringBuilder, this.MessageSentTime, base.UserContext);
			stringBuilder.Append("</span>");
		}

		// Token: 0x06002AE6 RID: 10982 RVA: 0x000F1BA5 File Offset: 0x000EFDA5
		protected void RenderToolbar()
		{
			this.toolbar.Render(base.Response.Output);
		}

		// Token: 0x06002AE7 RID: 10983 RVA: 0x000F1BC0 File Offset: 0x000EFDC0
		protected void RenderApprovalToolbar()
		{
			if (this.Message.VotingInfo != null)
			{
				IList<VotingInfo.OptionData> optionsDataList = this.Message.VotingInfo.GetOptionsDataList();
				if (optionsDataList == null || optionsDataList.Count != 2)
				{
					return;
				}
				this.approvalRequestToolbar = new ApprovalRequestToolbar(optionsDataList[0].SendPrompt != VotingInfo.SendPrompt.Send, optionsDataList[1].SendPrompt != VotingInfo.SendPrompt.Send);
				this.approvalRequestToolbar.Render(base.Response.Output);
			}
		}

		// Token: 0x06002AE8 RID: 10984 RVA: 0x000F1C3D File Offset: 0x000EFE3D
		protected void RenderCategoriesJavascriptArray()
		{
			CategorySwatch.RenderCategoriesJavascriptArray(base.SanitizingResponse, base.Item);
		}

		// Token: 0x06002AE9 RID: 10985 RVA: 0x000F1C50 File Offset: 0x000EFE50
		protected void RenderCategories()
		{
			if (base.Item != null)
			{
				CategorySwatch.RenderCategories(base.OwaContext, base.SanitizingResponse, base.Item);
			}
		}

		// Token: 0x06002AEA RID: 10986 RVA: 0x000F1C71 File Offset: 0x000EFE71
		protected virtual void RenderSubject()
		{
			if (this.IsSMimeItem)
			{
				this.RenderEncryptedMessageIcon(base.SanitizingResponse);
			}
			RenderingUtilities.RenderSubject(base.SanitizingResponse, this.Message);
		}

		// Token: 0x06002AEB RID: 10987 RVA: 0x000F1C98 File Offset: 0x000EFE98
		protected void RenderJavascriptEncodedInboxFolderId()
		{
			Utilities.JavascriptEncode(base.UserContext.InboxFolderId.ToBase64String(), base.SanitizingResponse);
		}

		// Token: 0x06002AEC RID: 10988 RVA: 0x000F1CB8 File Offset: 0x000EFEB8
		protected void RenderJavascriptEncodedLocalizedApprovalDecision(bool decision)
		{
			if (this.Message.VotingInfo != null)
			{
				int index = decision ? 0 : 1;
				IList<VotingInfo.OptionData> optionsDataList = this.Message.VotingInfo.GetOptionsDataList();
				if (optionsDataList != null && optionsDataList.Count == 2)
				{
					Utilities.JavascriptEncode(optionsDataList[index].DisplayName, base.SanitizingResponse);
				}
			}
		}

		// Token: 0x06002AED RID: 10989 RVA: 0x000F1D0E File Offset: 0x000EFF0E
		protected void RenderJavascriptEncodedLocalizedApproval()
		{
			this.RenderJavascriptEncodedLocalizedApprovalDecision(true);
		}

		// Token: 0x06002AEE RID: 10990 RVA: 0x000F1D17 File Offset: 0x000EFF17
		protected void RenderJavascriptEncodedLocalizedReject()
		{
			this.RenderJavascriptEncodedLocalizedApprovalDecision(false);
		}

		// Token: 0x06002AEF RID: 10991 RVA: 0x000F1D20 File Offset: 0x000EFF20
		protected void RenderJavascriptEncodedJunkEmailFolderId()
		{
			Utilities.JavascriptEncode(base.UserContext.JunkEmailFolderId.ToBase64String(), base.SanitizingResponse);
		}

		// Token: 0x06002AF0 RID: 10992 RVA: 0x000F1D3D File Offset: 0x000EFF3D
		protected void RenderJavascriptEncodedMessageChangeKey()
		{
			Utilities.JavascriptEncode(this.Message.Id.ChangeKeyAsBase64String(), base.SanitizingResponse);
		}

		// Token: 0x06002AF1 RID: 10993 RVA: 0x000F1D5C File Offset: 0x000EFF5C
		protected void RenderEncryptedMessageIcon(TextWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			writer.Write("<span id=\"spEn\" style=\"display:none\">");
			base.UserContext.RenderThemeImage(writer, ThemeFileId.Encrypted, null, new object[]
			{
				"id=\"imgEn\"",
				"title=\"",
				SanitizedHtmlString.FromStringId(1362348905),
				"\""
			});
			writer.Write("</span>");
		}

		// Token: 0x06002AF2 RID: 10994 RVA: 0x000F1DCC File Offset: 0x000EFFCC
		protected void RenderSignatureLine(TextWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			writer.Write("<span id=\"spSigLi\">");
			writer.Write("<span id=\"spSigPro\" style=\"display:none\">");
			writer.Write("<span id=\"spSPI\">");
			writer.Write("<img id=\"imgSVP\" src=\"");
			base.UserContext.RenderThemeFileUrl(writer, ThemeFileId.ProgressSmall);
			writer.Write("\">");
			writer.Write("</span><span id=\"spSPS\">");
			writer.Write(SanitizedHtmlString.FromStringId(-1793529945));
			writer.Write("</span>");
			writer.Write("</span>");
			writer.Write("<span class=\"sl\" id=\"spnSigRes\" style=\"display:none\" onresize=\"rszSl();\"></span>");
			writer.Write("</span>");
		}

		// Token: 0x06002AF3 RID: 10995 RVA: 0x000F1E78 File Offset: 0x000F0078
		private void RenderSignatureImage(TextWriter writer, string id, ThemeFileId themeId)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (string.IsNullOrEmpty(id))
			{
				throw new ArgumentException("id may not be null or empty string");
			}
			base.UserContext.RenderThemeImage(writer, themeId, null, new object[]
			{
				"id=\"" + id + "\"",
				"style=\"display:none\""
			});
		}

		// Token: 0x06002AF4 RID: 10996 RVA: 0x000F1ED8 File Offset: 0x000F00D8
		protected void RenderSignatureInfoDiv(TextWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			writer.Write("<div id=\"divSDtl\" style=\"display:none\">");
			writer.Write("<div class=\"ssiSctHdr\">");
			writer.Write("<div class=\"ssiSctTxt ssiSctTxtFw\">");
			writer.Write(SanitizedHtmlString.FromStringId(40886466));
			writer.Write("</div>");
			writer.Write("</div>");
			writer.Write("<div id=\"siSctBdy\">");
			writer.Write("<table>");
			writer.Write("<tr id=\"trSigDlgEn\" style=\"display:none\">");
			writer.Write("<td>");
			base.UserContext.RenderThemeImage(writer, ThemeFileId.Encrypted);
			writer.Write("</td><td id=\"tdssDlgEI\">");
			writer.Write(SanitizedHtmlString.FromStringId(1362348905));
			writer.Write("</td></tr>");
			writer.Write("<tr>");
			writer.Write("<td>");
			this.RenderSignatureImage(writer, "imgDVS", ThemeFileId.ValidSignature);
			this.RenderSignatureImage(writer, "imgDWS", ThemeFileId.WarningSignature);
			this.RenderSignatureImage(writer, "imgDIVS", ThemeFileId.InvalidSignature);
			writer.Write("</td>");
			writer.Write("<td id=\"tdSsSs\"></td>");
			writer.Write("</tr>");
			writer.Write("<tr>");
			writer.Write("<td></td>");
			writer.Write("<td id=\"tdSsEs\"></td>");
			writer.Write("</tr>");
			writer.Write("</table>");
			writer.Write("</div>");
			writer.Write("<div class=\"ssiSctHdr\">");
			writer.Write("<div class=\"ssiSctTxt ssiSctTxtFw\">");
			writer.Write(SanitizedHtmlString.FromStringId(63112306));
			writer.Write("</div>");
			writer.Write("</div>");
			writer.Write("<div id=\"siInBdy\" class=\"ssiSctSiBdy\">");
			writer.Write("<table id=\"tblSIBdy\">");
			writer.Write("<tr>");
			writer.Write("<td class=\"t\">");
			writer.Write(SanitizedHtmlString.FromStringId(-881075747));
			writer.Write("</td>");
			writer.Write("<td id=\"tdSubj\" nowrap></td>");
			writer.Write("</tr>");
			writer.Write("<tr>");
			writer.Write("<td class=\"t\">");
			writer.Write(SanitizedHtmlString.FromStringId(-1376223345));
			writer.Write("</td>");
			writer.Write("<td id=\"tdFrom\" nowrap></td>");
			writer.Write("</tr>");
			writer.Write("<tr>");
			writer.Write("<td class=\"t\">");
			writer.Write(SanitizedHtmlString.FromStringId(2124841137));
			writer.Write("</td>");
			writer.Write("<td id=\"tdSigBy\" nowrap></td>");
			writer.Write("</tr>");
			writer.Write("<tr>");
			writer.Write("<td class=\"t\">");
			writer.Write(SanitizedHtmlString.FromStringId(46763188));
			writer.Write("</td>");
			writer.Write("<td id=\"tdIssBy\" nowrap></td>");
			writer.Write("</tr>");
			writer.Write("</table>");
			writer.Write("</div>");
			writer.Write("<div id=\"siErrBdy\" class=\"ssiSctSiBdy\">");
			writer.Write(SanitizedHtmlString.FromStringId(1766818386));
			writer.Write("</div>");
			writer.Write("</div>");
		}

		// Token: 0x17000C89 RID: 3209
		// (get) Token: 0x06002AF5 RID: 10997 RVA: 0x000F21FE File Offset: 0x000F03FE
		protected Markup BodyMarkup
		{
			get
			{
				return this.bodyMarkup;
			}
		}

		// Token: 0x17000C8A RID: 3210
		// (get) Token: 0x06002AF6 RID: 10998 RVA: 0x000F2206 File Offset: 0x000F0406
		protected ArrayList AttachmentWellRenderObjects
		{
			get
			{
				return this.attachmentWellRenderObjects;
			}
		}

		// Token: 0x17000C8B RID: 3211
		// (get) Token: 0x06002AF7 RID: 10999 RVA: 0x000F220E File Offset: 0x000F040E
		protected static string SaveNamespace
		{
			get
			{
				return "ReadMessage";
			}
		}

		// Token: 0x17000C8C RID: 3212
		// (get) Token: 0x06002AF8 RID: 11000 RVA: 0x000F2215 File Offset: 0x000F0415
		// (set) Token: 0x06002AF9 RID: 11001 RVA: 0x000F221D File Offset: 0x000F041D
		protected IRMItemHelper IrmItemHelper { get; set; }

		// Token: 0x17000C8D RID: 3213
		// (get) Token: 0x06002AFA RID: 11002 RVA: 0x000F2226 File Offset: 0x000F0426
		protected bool ShouldRenderAttachmentWell
		{
			get
			{
				return this.shouldRenderAttachmentWell || this.IsSMimeControlNeeded;
			}
		}

		// Token: 0x17000C8E RID: 3214
		// (get) Token: 0x06002AFB RID: 11003 RVA: 0x000F2238 File Offset: 0x000F0438
		protected RecipientWell RecipientWell
		{
			get
			{
				return this.recipientWell;
			}
		}

		// Token: 0x17000C8F RID: 3215
		// (get) Token: 0x06002AFC RID: 11004 RVA: 0x000F2240 File Offset: 0x000F0440
		protected Infobar Infobar
		{
			get
			{
				return this.infobar;
			}
		}

		// Token: 0x17000C90 RID: 3216
		// (get) Token: 0x06002AFD RID: 11005 RVA: 0x000F2248 File Offset: 0x000F0448
		protected bool IsDraftMessage
		{
			get
			{
				return this.isDraftMessage;
			}
		}

		// Token: 0x17000C91 RID: 3217
		// (get) Token: 0x06002AFE RID: 11006 RVA: 0x000F2250 File Offset: 0x000F0450
		protected bool IsInJunkMailFolder
		{
			get
			{
				return this.isInJunkEmailFolder;
			}
		}

		// Token: 0x17000C92 RID: 3218
		// (get) Token: 0x06002AFF RID: 11007 RVA: 0x000F2258 File Offset: 0x000F0458
		protected bool IsSuspectedPhishingItemWithoutLinkEnabled
		{
			get
			{
				return this.isSuspectedPhishingItemWithoutLinkEnabled;
			}
		}

		// Token: 0x17000C93 RID: 3219
		// (get) Token: 0x06002B00 RID: 11008 RVA: 0x000F2260 File Offset: 0x000F0460
		protected bool ShowBccInSentItems
		{
			get
			{
				return this.recipientWell.HasRecipients(RecipientWellType.Bcc);
			}
		}

		// Token: 0x17000C94 RID: 3220
		// (get) Token: 0x06002B01 RID: 11009 RVA: 0x000F226E File Offset: 0x000F046E
		protected int CurrentStoreObjectType
		{
			get
			{
				if (this.Message != null && ObjectClass.IsOfClass(this.Message.ClassName, "IPM.Note.Microsoft.Approval.Request"))
				{
					return 26;
				}
				return 9;
			}
		}

		// Token: 0x17000C95 RID: 3221
		// (get) Token: 0x06002B02 RID: 11010 RVA: 0x000F2294 File Offset: 0x000F0494
		protected static int StoreObjectTypeApprovalRequest
		{
			get
			{
				return 26;
			}
		}

		// Token: 0x17000C96 RID: 3222
		// (get) Token: 0x06002B03 RID: 11011 RVA: 0x000F2298 File Offset: 0x000F0498
		protected static int StoreObjectTypeMessage
		{
			get
			{
				return 9;
			}
		}

		// Token: 0x17000C97 RID: 3223
		// (get) Token: 0x06002B04 RID: 11012 RVA: 0x000F229C File Offset: 0x000F049C
		protected ExDateTime MessageSentTime
		{
			get
			{
				return this.Message.SentTime;
			}
		}

		// Token: 0x17000C98 RID: 3224
		// (get) Token: 0x06002B05 RID: 11013 RVA: 0x000F22A9 File Offset: 0x000F04A9
		protected bool IsClearSignedItem
		{
			get
			{
				return Utilities.IsClearSigned(this.Message);
			}
		}

		// Token: 0x17000C99 RID: 3225
		// (get) Token: 0x06002B06 RID: 11014 RVA: 0x000F22B6 File Offset: 0x000F04B6
		protected bool IsOpaqueSignedItem
		{
			get
			{
				return Utilities.IsOpaqueSigned(this.Message);
			}
		}

		// Token: 0x17000C9A RID: 3226
		// (get) Token: 0x06002B07 RID: 11015 RVA: 0x000F22C3 File Offset: 0x000F04C3
		protected bool IsSMimeItem
		{
			get
			{
				return Utilities.IsSMime(this.Message);
			}
		}

		// Token: 0x17000C9B RID: 3227
		// (get) Token: 0x06002B08 RID: 11016 RVA: 0x000F22D0 File Offset: 0x000F04D0
		protected bool IsEncryptedItem
		{
			get
			{
				return Utilities.IsEncrypted(this.Message);
			}
		}

		// Token: 0x17000C9C RID: 3228
		// (get) Token: 0x06002B09 RID: 11017 RVA: 0x000F22E0 File Offset: 0x000F04E0
		protected bool IsSMimeControlNeeded
		{
			get
			{
				return !JunkEmailUtilities.IsJunkOrPhishing(base.Item, base.IsEmbeddedItem, base.UserContext) && Utilities.IsClientSMimeControlUsable(base.ClientSMimeControlStatus) && this.IsSMimeItem && Utilities.IsFlagSet((int)base.ClientSMimeControlStatus, 16);
			}
		}

		// Token: 0x17000C9D RID: 3229
		// (get) Token: 0x06002B0A RID: 11018 RVA: 0x000F232C File Offset: 0x000F052C
		protected SanitizedHtmlString WebBeaconBlockedInfobarText
		{
			get
			{
				return BodyConversionUtilities.GetWebBeaconBlockedInfobarMessage(base.UserContext.Configuration.FilterWebBeaconsAndHtmlForms);
			}
		}

		// Token: 0x17000C9E RID: 3230
		// (get) Token: 0x06002B0B RID: 11019 RVA: 0x000F2343 File Offset: 0x000F0543
		protected bool IsWebBeaconAllowed
		{
			get
			{
				return base.ForceAllowWebBeacon || (!this.IsPublicItem && Utilities.IsWebBeaconsAllowed(base.Item));
			}
		}

		// Token: 0x17000C9F RID: 3231
		// (get) Token: 0x06002B0C RID: 11020 RVA: 0x000F2364 File Offset: 0x000F0564
		protected bool ShouldRenderApprovalToolbar
		{
			get
			{
				return Utilities.IsValidUndecidedApprovalRequest(this.Message) && !this.IsOtherMailboxItem;
			}
		}

		// Token: 0x17000CA0 RID: 3232
		// (get) Token: 0x06002B0D RID: 11021 RVA: 0x000F237E File Offset: 0x000F057E
		protected override bool IsSubjectEditable
		{
			get
			{
				return !this.IsSMimeItem && (!base.UserContext.IsSmsEnabled || !ObjectClass.IsSmsMessage(this.Message.ClassName)) && base.IsSubjectEditable;
			}
		}

		// Token: 0x17000CA1 RID: 3233
		// (get) Token: 0x06002B0E RID: 11022 RVA: 0x000F23B0 File Offset: 0x000F05B0
		protected RecipientJunkEmailContextMenuType RecipientJunkEmailMenuType
		{
			get
			{
				RecipientJunkEmailContextMenuType result = RecipientJunkEmailContextMenuType.None;
				if (base.UserContext.IsJunkEmailEnabled)
				{
					result = RecipientJunkEmailContextMenuType.SenderAndRecipient;
				}
				return result;
			}
		}

		// Token: 0x17000CA2 RID: 3234
		// (get) Token: 0x06002B0F RID: 11023 RVA: 0x000F23CF File Offset: 0x000F05CF
		protected FlagAction FlagAction
		{
			get
			{
				return FlagContextMenu.GetFlagActionForItem(base.UserContext, this.Message);
			}
		}

		// Token: 0x06002B10 RID: 11024 RVA: 0x000F23E2 File Offset: 0x000F05E2
		private void AddIrmMessageToInfobar()
		{
			if (base.UserContext.IsIrmEnabled && Utilities.IsIrmRestrictedAndDecrypted(this.Message))
			{
				InfobarMessageBuilder.AddIrmInformation(this.infobar, this.Message, base.IsPreviewForm, true, this.IrmItemHelper.IsRemoveAllowed, false);
			}
		}

		// Token: 0x17000CA3 RID: 3235
		// (get) Token: 0x06002B11 RID: 11025 RVA: 0x000F2724 File Offset: 0x000F0924
		public override IEnumerable<string> ExternalScriptFiles
		{
			get
			{
				foreach (string s in this.externalScriptFiles)
				{
					yield return s;
				}
				if (base.UserContext.ExchangePrincipal.RecipientTypeDetails == RecipientTypeDetails.DiscoveryMailbox)
				{
					foreach (string s2 in this.externalScriptFilesForDiscoveryMailbox)
					{
						yield return s2;
					}
				}
				if (this.IsSMimeControlNeeded)
				{
					foreach (string s3 in this.externalScriptFilesForSMIME)
					{
						yield return s3;
					}
				}
				yield break;
			}
		}

		// Token: 0x17000CA4 RID: 3236
		// (get) Token: 0x06002B12 RID: 11026 RVA: 0x000F2744 File Offset: 0x000F0944
		public override SanitizedHtmlString Title
		{
			get
			{
				if (base.UserContext.IsSmsEnabled && ObjectClass.IsSmsMessage(this.Message.ClassName) && this.Message.From != null)
				{
					Participant from = this.Message.From;
					SanitizingStringBuilder<OwaHtml> sanitizingStringBuilder = new SanitizingStringBuilder<OwaHtml>(128);
					if (!string.IsNullOrEmpty(from.DisplayName))
					{
						sanitizingStringBuilder.Append(from.DisplayName);
					}
					if (Utilities.IsMobileRoutingType(from.RoutingType))
					{
						sanitizingStringBuilder.Append<char>(' ');
						sanitizingStringBuilder.Append(base.UserContext.DirectionMark);
						sanitizingStringBuilder.Append<char>('[');
						sanitizingStringBuilder.Append(from.EmailAddress);
						sanitizingStringBuilder.Append<char>(']');
						sanitizingStringBuilder.Append(base.UserContext.DirectionMark);
					}
					return SanitizedHtmlString.Format(LocalizedStrings.GetHtmlEncoded(1856268034), new object[]
					{
						sanitizingStringBuilder.ToSanitizedString<SanitizedHtmlString>()
					});
				}
				return RenderingUtilities.GetSubject(this.Message, LocalizedStrings.GetNonEncoded(730745110));
			}
		}

		// Token: 0x17000CA5 RID: 3237
		// (get) Token: 0x06002B13 RID: 11027 RVA: 0x000F2845 File Offset: 0x000F0A45
		public override string PageType
		{
			get
			{
				return "ReadMessagePage";
			}
		}

		// Token: 0x17000CA6 RID: 3238
		// (get) Token: 0x06002B14 RID: 11028 RVA: 0x000F284C File Offset: 0x000F0A4C
		public override string BodyCssClass
		{
			get
			{
				if (!base.IsPreviewForm)
				{
					return "rdFrmBody";
				}
				return string.Empty;
			}
		}

		// Token: 0x17000CA7 RID: 3239
		// (get) Token: 0x06002B15 RID: 11029 RVA: 0x000F2861 File Offset: 0x000F0A61
		public override string HtmlAdditionalAttributes
		{
			get
			{
				if (!this.IsSMimeControlNeeded)
				{
					return string.Empty;
				}
				return "xmlns:MIME";
			}
		}

		// Token: 0x04001CC4 RID: 7364
		private MessageItem message;

		// Token: 0x04001CC5 RID: 7365
		private MessageRecipientWell recipientWell;

		// Token: 0x04001CC6 RID: 7366
		private Infobar infobar = new Infobar();

		// Token: 0x04001CC7 RID: 7367
		private ArrayList attachmentWellRenderObjects;

		// Token: 0x04001CC8 RID: 7368
		private ReadMessageToolbar toolbar;

		// Token: 0x04001CC9 RID: 7369
		private ApprovalRequestToolbar approvalRequestToolbar;

		// Token: 0x04001CCA RID: 7370
		private bool shouldRenderAttachmentWell;

		// Token: 0x04001CCB RID: 7371
		private bool isFaxMessage;

		// Token: 0x04001CCC RID: 7372
		private bool isDraftMessage;

		// Token: 0x04001CCD RID: 7373
		private Markup bodyMarkup;

		// Token: 0x04001CCE RID: 7374
		private bool isInJunkEmailFolder;

		// Token: 0x04001CCF RID: 7375
		private bool isSuspectedPhishingItemWithoutLinkEnabled;

		// Token: 0x04001CD0 RID: 7376
		private string[] externalScriptFiles = new string[]
		{
			"freadmsg.js"
		};

		// Token: 0x04001CD1 RID: 7377
		private string[] externalScriptFilesForSMIME = new string[]
		{
			"cattach.js",
			"smallicons.aspx"
		};

		// Token: 0x04001CD2 RID: 7378
		private string[] externalScriptFilesForDiscoveryMailbox = new string[]
		{
			"MessageAnnotationDialog.js"
		};
	}
}
