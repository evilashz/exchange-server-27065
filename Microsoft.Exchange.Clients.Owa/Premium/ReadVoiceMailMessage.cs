using System;
using System.Collections;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Premium.Controls;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.UM.ClientAccess;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x02000474 RID: 1140
	public class ReadVoiceMailMessage : OwaForm, IRegistryOnlyForm
	{
		// Token: 0x17000CCB RID: 3275
		// (get) Token: 0x06002B6D RID: 11117 RVA: 0x000F3A1E File Offset: 0x000F1C1E
		// (set) Token: 0x06002B6E RID: 11118 RVA: 0x000F3A26 File Offset: 0x000F1C26
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

		// Token: 0x06002B6F RID: 11119 RVA: 0x000F3A30 File Offset: 0x000F1C30
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			this.Message = base.Initialize<MessageItem>(new PropertyDefinition[]
			{
				MessageItemSchema.IsRead,
				BodySchema.Codepage,
				BodySchema.InternetCpid,
				MessageItemSchema.MessageAudioNotes,
				MessageItemSchema.SenderTelephoneNumber,
				ItemSchema.FlagStatus,
				ItemSchema.FlagCompleteTime,
				MessageItemSchema.ReplyTime,
				MessageItemSchema.RequireProtectedPlayOnPhone,
				ItemSchema.UtcDueDate,
				ItemSchema.UtcStartDate,
				ItemSchema.ReminderDueBy,
				ItemSchema.ReminderIsSet,
				StoreObjectSchema.EffectiveRights
			});
			this.IrmItemHelper = new IRMItemHelper(this.Message, base.UserContext, base.IsPreviewForm, base.IsEmbeddedItem);
			using (UMClientCommon umclientCommon = new UMClientCommon(base.UserContext.ExchangePrincipal))
			{
				this.isUMEnabled = umclientCommon.IsUMEnabled();
				this.isPlayOnPhoneEnabled = umclientCommon.IsPlayOnPhoneEnabled();
			}
			this.IrmItemHelper.IrmDecryptIfRestricted();
			bool isSuspectedPhishingItem = false;
			bool isLinkEnabled = false;
			bool flag = false;
			this.isMacintoshPlatform = (Utilities.GetBrowserPlatform(base.Request.UserAgent) == BrowserPlatform.Macintosh);
			JunkEmailUtilities.GetJunkEmailPropertiesForItem(this.Message, base.IsEmbeddedItem, base.ForceEnableItemLink, base.UserContext, out this.isInJunkEmailFolder, out isSuspectedPhishingItem, out isLinkEnabled, out flag);
			this.toolbar = new ReadMessageToolbar(base.IsInDeleteItems, base.IsEmbeddedItem, this.Message, this.isInJunkEmailFolder, isSuspectedPhishingItem, isLinkEnabled, false, this.IrmItemHelper.IsReplyRestricted, this.IrmItemHelper.IsReplyAllRestricted, this.IrmItemHelper.IsForwardRestricted, this.IrmItemHelper.IsPrintRestricted);
			this.toolbar.ToolbarType = (base.IsPreviewForm ? ToolbarType.Preview : ToolbarType.Form);
			this.recipientWell = new MessageRecipientWell(this.Message);
			if (flag)
			{
				this.bodyMarkup = Markup.PlainText;
			}
			InfobarMessageBuilder.AddImportance(this.infobar, this.Message);
			InfobarMessageBuilder.AddFlag(this.infobar, this.Message, base.UserContext);
			InfobarMessageBuilder.AddSensitivity(this.infobar, this.Message);
			if (base.UserContext.IsIrmEnabled && Utilities.IsIrmRestrictedAndDecrypted(this.Message))
			{
				InfobarMessageBuilder.AddIrmInformation(this.infobar, this.Message, base.IsPreviewForm, false, false, false);
			}
			if (!this.Message.IsRead && !base.IsPreviewForm && !base.IsEmbeddedItem)
			{
				this.Message.MarkAsRead(Utilities.ShouldSuppressReadReceipt(base.UserContext, this.Message), false);
			}
		}

		// Token: 0x06002B70 RID: 11120 RVA: 0x000F3CC4 File Offset: 0x000F1EC4
		protected void LoadMessageBodyIntoStream(TextWriter writer)
		{
			if (this.IrmItemHelper.IsRestrictedButIrmFeatureDisabledOrDecryptionFailed)
			{
				this.IrmItemHelper.RenderAlternateBodyForIrm(writer, true);
				return;
			}
			string action = base.IsPreviewForm ? "Preview" : string.Empty;
			string attachmentUrl = null;
			if (base.IsEmbeddedItemInNonSMimeItem)
			{
				attachmentUrl = base.RenderEmbeddedUrl();
			}
			base.AttachmentLinks = BodyConversionUtilities.GenerateNonEditableMessageBodyAndRenderInfobarMessages(this.Message, writer, base.OwaContext, this.infobar, base.ForceAllowWebBeacon, base.ForceEnableItemLink, "IPM.Note", action, string.Empty, base.IsEmbeddedItemInNonSMimeItem, attachmentUrl);
		}

		// Token: 0x06002B71 RID: 11121 RVA: 0x000F3D50 File Offset: 0x000F1F50
		protected void CreateAttachmentHelpers()
		{
			if (this.Message.IsRestricted)
			{
				this.shouldRenderAttachmentWell = false;
				return;
			}
			this.attachmentWellRenderObjects = AttachmentWell.GetAttachmentInformation(this.Message, base.AttachmentLinks, base.UserContext.IsPublicLogon, base.IsEmbeddedItem, base.ForceEnableItemLink);
			this.shouldRenderAttachmentWell = RenderingUtilities.AddAttachmentInfobarMessages(base.Item, base.IsEmbeddedItem, base.ForceEnableItemLink, this.infobar, this.attachmentWellRenderObjects);
		}

		// Token: 0x06002B72 RID: 11122 RVA: 0x000F3DCC File Offset: 0x000F1FCC
		protected void RenderVoiceMailPlayer(TextWriter writer)
		{
			if (this.isUMEnabled && !base.IsEmbeddedItem && base.UserContext.BrowserType == BrowserType.IE && !this.isMacintoshPlatform)
			{
				string text = this.Message.TryGetProperty(MessageItemSchema.RequireProtectedPlayOnPhone) as string;
				if (this.IrmItemHelper.IsRestrictedAndIrmFeatureEnabled && !string.IsNullOrEmpty(text) && text.Equals("true", StringComparison.OrdinalIgnoreCase))
				{
					return;
				}
				using (Attachment latestVoiceMailAttachment = Utilities.GetLatestVoiceMailAttachment(this.Message, base.UserContext))
				{
					if (latestVoiceMailAttachment != null)
					{
						writer.Write("<object id=oMpf classid=\"clsid:6bf52a52-394a-11d3-b153-00c04f79faa6\" ");
						writer.Write("type=\"application/x-oleobject\" width=\"212\" height=\"45\">");
						writer.Write("<param name=\"URL\" value=\"");
						Utilities.WriteLatestUrlToAttachment(writer, Utilities.GetIdAsString(this.Message), latestVoiceMailAttachment.FileExtension);
						writer.Write("\">");
						writer.Write("<param name=\"autoStart\" value=\"false\"><param name=\"EnableContextMenu\" value=\"0\">");
						writer.Write("<param name=\"InvokeURLs\" value=\"0\"></object>");
					}
					else
					{
						this.infobar.AddMessage(SanitizedHtmlString.FromStringId(-229902107), InfobarMessageType.Informational);
					}
				}
			}
		}

		// Token: 0x06002B73 RID: 11123 RVA: 0x000F3EE8 File Offset: 0x000F20E8
		protected void RenderSender()
		{
			UnifiedMessagingUtilities.RenderSender(base.UserContext, base.Response.Output, this.Message);
		}

		// Token: 0x06002B74 RID: 11124 RVA: 0x000F3F06 File Offset: 0x000F2106
		protected void RenderToolbar()
		{
			this.toolbar.Render(base.Response.Output);
		}

		// Token: 0x06002B75 RID: 11125 RVA: 0x000F3F1E File Offset: 0x000F211E
		protected void RenderCategoriesJavascriptArray()
		{
			CategorySwatch.RenderCategoriesJavascriptArray(base.SanitizingResponse, base.Item);
		}

		// Token: 0x06002B76 RID: 11126 RVA: 0x000F3F31 File Offset: 0x000F2131
		protected void RenderCategories()
		{
			if (base.Item != null)
			{
				CategorySwatch.RenderCategories(base.OwaContext, base.SanitizingResponse, base.Item);
			}
		}

		// Token: 0x06002B77 RID: 11127 RVA: 0x000F3F52 File Offset: 0x000F2152
		protected void RenderSubject(bool isTitle)
		{
			if (isTitle)
			{
				RenderingUtilities.RenderSubject(base.Response.Output, this.Message, LocalizedStrings.GetNonEncoded(730745110));
				return;
			}
			RenderingUtilities.RenderSubject(base.Response.Output, this.Message);
		}

		// Token: 0x06002B78 RID: 11128 RVA: 0x000F3F8E File Offset: 0x000F218E
		protected void RenderJavascriptEncodedInboxFolderId()
		{
			Utilities.JavascriptEncode(base.UserContext.InboxFolderId.ToBase64String(), base.Response.Output);
		}

		// Token: 0x06002B79 RID: 11129 RVA: 0x000F3FB0 File Offset: 0x000F21B0
		protected void RenderJavascriptEncodedJunkEmailFolderId()
		{
			Utilities.JavascriptEncode(base.UserContext.JunkEmailFolderId.ToBase64String(), base.Response.Output);
		}

		// Token: 0x06002B7A RID: 11130 RVA: 0x000F3FD2 File Offset: 0x000F21D2
		protected void RenderJavascriptEncodedMessageChangeKey()
		{
			Utilities.JavascriptEncode(this.Message.Id.ChangeKeyAsBase64String(), base.Response.Output);
		}

		// Token: 0x17000CCC RID: 3276
		// (get) Token: 0x06002B7B RID: 11131 RVA: 0x000F3FF4 File Offset: 0x000F21F4
		protected Markup BodyMarkup
		{
			get
			{
				return this.bodyMarkup;
			}
		}

		// Token: 0x17000CCD RID: 3277
		// (get) Token: 0x06002B7C RID: 11132 RVA: 0x000F3FFC File Offset: 0x000F21FC
		protected static string SaveNamespace
		{
			get
			{
				return "ReadMessage";
			}
		}

		// Token: 0x17000CCE RID: 3278
		// (get) Token: 0x06002B7D RID: 11133 RVA: 0x000F4003 File Offset: 0x000F2203
		// (set) Token: 0x06002B7E RID: 11134 RVA: 0x000F400B File Offset: 0x000F220B
		protected IRMItemHelper IrmItemHelper { get; set; }

		// Token: 0x17000CCF RID: 3279
		// (get) Token: 0x06002B7F RID: 11135 RVA: 0x000F4014 File Offset: 0x000F2214
		protected ArrayList AttachmentWellRenderObjects
		{
			get
			{
				return this.attachmentWellRenderObjects;
			}
		}

		// Token: 0x17000CD0 RID: 3280
		// (get) Token: 0x06002B80 RID: 11136 RVA: 0x000F401C File Offset: 0x000F221C
		protected bool ShouldRenderAttachmentWell
		{
			get
			{
				return this.shouldRenderAttachmentWell;
			}
		}

		// Token: 0x17000CD1 RID: 3281
		// (get) Token: 0x06002B81 RID: 11137 RVA: 0x000F4024 File Offset: 0x000F2224
		protected RecipientWell RecipientWell
		{
			get
			{
				return this.recipientWell;
			}
		}

		// Token: 0x17000CD2 RID: 3282
		// (get) Token: 0x06002B82 RID: 11138 RVA: 0x000F402C File Offset: 0x000F222C
		protected Infobar Infobar
		{
			get
			{
				return this.infobar;
			}
		}

		// Token: 0x17000CD3 RID: 3283
		// (get) Token: 0x06002B83 RID: 11139 RVA: 0x000F4034 File Offset: 0x000F2234
		protected string AudioNotes
		{
			get
			{
				string text = this.Message.TryGetProperty(MessageItemSchema.MessageAudioNotes) as string;
				if (string.IsNullOrEmpty(text))
				{
					return LocalizedStrings.GetHtmlEncoded(-1207478783);
				}
				return Utilities.HtmlEncode(text);
			}
		}

		// Token: 0x17000CD4 RID: 3284
		// (get) Token: 0x06002B84 RID: 11140 RVA: 0x000F4070 File Offset: 0x000F2270
		protected bool IsAudioNotesPresent
		{
			get
			{
				string value = this.Message.TryGetProperty(MessageItemSchema.MessageAudioNotes) as string;
				return !string.IsNullOrEmpty(value);
			}
		}

		// Token: 0x17000CD5 RID: 3285
		// (get) Token: 0x06002B85 RID: 11141 RVA: 0x000F409C File Offset: 0x000F229C
		protected bool IsAudioNotesEditable
		{
			get
			{
				return this.IsItemEditable && !base.IsEmbeddedItem;
			}
		}

		// Token: 0x17000CD6 RID: 3286
		// (get) Token: 0x06002B86 RID: 11142 RVA: 0x000F40B1 File Offset: 0x000F22B1
		protected bool IsPlayOnPhoneEnabled
		{
			get
			{
				return this.isPlayOnPhoneEnabled;
			}
		}

		// Token: 0x17000CD7 RID: 3287
		// (get) Token: 0x06002B87 RID: 11143 RVA: 0x000F40B9 File Offset: 0x000F22B9
		protected bool IsInJunkMailFolder
		{
			get
			{
				return this.isInJunkEmailFolder;
			}
		}

		// Token: 0x17000CD8 RID: 3288
		// (get) Token: 0x06002B88 RID: 11144 RVA: 0x000F40C1 File Offset: 0x000F22C1
		protected static int StoreObjectTypeMessage
		{
			get
			{
				return 9;
			}
		}

		// Token: 0x17000CD9 RID: 3289
		// (get) Token: 0x06002B89 RID: 11145 RVA: 0x000F40C5 File Offset: 0x000F22C5
		protected ExDateTime MessageSentTime
		{
			get
			{
				return this.Message.SentTime;
			}
		}

		// Token: 0x17000CDA RID: 3290
		// (get) Token: 0x06002B8A RID: 11146 RVA: 0x000F40D2 File Offset: 0x000F22D2
		protected bool IsMacintoshPlatform
		{
			get
			{
				return this.isMacintoshPlatform;
			}
		}

		// Token: 0x17000CDB RID: 3291
		// (get) Token: 0x06002B8B RID: 11147 RVA: 0x000F40DC File Offset: 0x000F22DC
		protected string PhoneNumber
		{
			get
			{
				string result = string.Empty;
				using (UMClientCommon umclientCommon = new UMClientCommon(base.UserContext.ExchangePrincipal))
				{
					if (umclientCommon.IsUMEnabled())
					{
						result = umclientCommon.GetUMProperties().PlayOnPhoneDialString;
					}
				}
				return result;
			}
		}

		// Token: 0x17000CDC RID: 3292
		// (get) Token: 0x06002B8C RID: 11148 RVA: 0x000F4134 File Offset: 0x000F2334
		protected static int UMCallStateIdle
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000CDD RID: 3293
		// (get) Token: 0x06002B8D RID: 11149 RVA: 0x000F4137 File Offset: 0x000F2337
		protected static int UMCallStateConnecting
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000CDE RID: 3294
		// (get) Token: 0x06002B8E RID: 11150 RVA: 0x000F413A File Offset: 0x000F233A
		protected static int UMCallStateConnected
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x17000CDF RID: 3295
		// (get) Token: 0x06002B8F RID: 11151 RVA: 0x000F413D File Offset: 0x000F233D
		protected static int UMCallStateDisconnected
		{
			get
			{
				return 4;
			}
		}

		// Token: 0x17000CE0 RID: 3296
		// (get) Token: 0x06002B90 RID: 11152 RVA: 0x000F4140 File Offset: 0x000F2340
		protected FlagAction FlagAction
		{
			get
			{
				return FlagContextMenu.GetFlagActionForItem(base.UserContext, this.Message);
			}
		}

		// Token: 0x17000CE1 RID: 3297
		// (get) Token: 0x06002B91 RID: 11153 RVA: 0x000F4154 File Offset: 0x000F2354
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

		// Token: 0x04001CEA RID: 7402
		private MessageItem message;

		// Token: 0x04001CEB RID: 7403
		private MessageRecipientWell recipientWell;

		// Token: 0x04001CEC RID: 7404
		private Infobar infobar = new Infobar();

		// Token: 0x04001CED RID: 7405
		private ArrayList attachmentWellRenderObjects;

		// Token: 0x04001CEE RID: 7406
		private ReadMessageToolbar toolbar;

		// Token: 0x04001CEF RID: 7407
		private bool shouldRenderAttachmentWell;

		// Token: 0x04001CF0 RID: 7408
		private bool isPlayOnPhoneEnabled;

		// Token: 0x04001CF1 RID: 7409
		private bool isUMEnabled;

		// Token: 0x04001CF2 RID: 7410
		private Markup bodyMarkup;

		// Token: 0x04001CF3 RID: 7411
		private bool isInJunkEmailFolder;

		// Token: 0x04001CF4 RID: 7412
		private bool isMacintoshPlatform;
	}
}
