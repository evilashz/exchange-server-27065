using System;
using System.Collections;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Premium.Controls;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x02000450 RID: 1104
	public class EditMeetingResponse : EditItemForm, IRegistryOnlyForm
	{
		// Token: 0x06002819 RID: 10265 RVA: 0x000E2E20 File Offset: 0x000E1020
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			this.infobar.SetInfobarClass("infobarEdit");
			this.infobar.SetShouldHonorHideByDefault(true);
			string queryStringParameter = Utilities.GetQueryStringParameter(base.Request, "a", false);
			string type = base.OwaContext.FormsRegistryContext.Type;
			if (ObjectClass.IsMeetingResponse(type))
			{
				this.isNew = string.Equals(queryStringParameter, "New", StringComparison.OrdinalIgnoreCase);
				if (this.isNew)
				{
					this.newItemType = NewItemType.New;
				}
				base.Item = base.Initialize<MeetingResponse>(EditMeetingResponse.prefetchProperties);
				MeetingMessage meetingMessage = (MeetingMessage)base.Item;
				if (meetingMessage.From != null)
				{
					this.isSendOnBehalfOf = (string.CompareOrdinal(base.UserContext.ExchangePrincipal.LegacyDn, meetingMessage.From.EmailAddress) != 0);
				}
				this.InitializeMeetingResponse();
			}
			else if (ObjectClass.IsMeetingRequest(type))
			{
				this.itemType = StoreObjectType.MeetingRequest;
				this.isNew = string.Equals(queryStringParameter, "Forward", StringComparison.OrdinalIgnoreCase);
				if (this.isNew)
				{
					this.newItemType = NewItemType.Forward;
				}
				base.Item = base.Initialize<MeetingRequest>(EditMeetingResponse.prefetchProperties);
			}
			else
			{
				if (!ObjectClass.IsMeetingCancellation(type))
				{
					ExTraceGlobals.CalendarDataTracer.TraceDebug<string>((long)this.GetHashCode(), "Unsupported item type '{0}' for edit meeting page", type);
					throw new OwaInvalidRequestException(string.Format("Unsupported item type '{0}' for edit meeting page", type));
				}
				this.itemType = StoreObjectType.MeetingCancellation;
				this.isNew = string.Equals(queryStringParameter, "Forward", StringComparison.OrdinalIgnoreCase);
				if (this.isNew)
				{
					this.newItemType = NewItemType.Forward;
				}
				this.infobar.AddMessage(SanitizedHtmlString.FromStringId(-161808760), InfobarMessageType.Informational);
				base.Item = base.Initialize<MeetingCancellation>(EditMeetingResponse.prefetchProperties);
			}
			base.DeleteExistingDraft = this.isNew;
			if (!this.isNew && base.Item is MessageItem)
			{
				MessageItem messageItem = (MessageItem)base.Item;
				if (messageItem.GetValueOrDefault<bool>(MessageItemSchema.HasBeenSubmitted))
				{
					messageItem.AbortSubmit();
				}
			}
			this.bodyMarkup = BodyConversionUtilities.GetBodyFormatOfEditItem(base.Item, this.newItemType, base.UserContext.UserOptions);
			this.toolbar = new EditMessageToolbar(((MeetingMessage)base.Item).Importance, this.bodyMarkup);
			this.toolbar.ToolbarType = (base.IsPreviewForm ? ToolbarType.Preview : ToolbarType.Form);
			this.toolbar.IsComplianceButtonAllowedInForm = false;
			this.messageRecipientWell = new MessageRecipientWell((MeetingMessage)base.Item);
			this.showBcc = this.messageRecipientWell.HasRecipients(RecipientWellType.Bcc);
		}

		// Token: 0x0600281A RID: 10266 RVA: 0x000E3098 File Offset: 0x000E1298
		private void InitializeMeetingResponse()
		{
			MeetingResponse meetingResponse = (MeetingResponse)base.Item;
			this.responseType = meetingResponse.ResponseType;
			string queryStringParameter = Utilities.GetQueryStringParameter(base.Request, "mid", false);
			if (queryStringParameter != null)
			{
				this.meetingRequestId = OwaStoreObjectId.CreateFromString(queryStringParameter);
			}
			this.isMeetingInviteInDeleteItems = (Utilities.GetQueryStringParameter(base.Request, "d", false) != null);
			this.infobar.AddMessage(SanitizedHtmlString.FromStringId(-1981719796), InfobarMessageType.Informational);
			string format = string.Empty;
			switch (this.responseType)
			{
			case ResponseType.Tentative:
				format = LocalizedStrings.GetHtmlEncoded(-588720585);
				break;
			case ResponseType.Accept:
				format = LocalizedStrings.GetHtmlEncoded(-14610226);
				break;
			case ResponseType.Decline:
				format = LocalizedStrings.GetHtmlEncoded(-1615218790);
				break;
			}
			SanitizedHtmlString messageHtml;
			if (this.isSendOnBehalfOf)
			{
				messageHtml = SanitizedHtmlString.Format(format, new object[]
				{
					meetingResponse.From.DisplayName
				});
			}
			else
			{
				messageHtml = SanitizedHtmlString.Format(format, new object[]
				{
					LocalizedStrings.GetNonEncoded(372029413)
				});
			}
			this.infobar.AddMessage(messageHtml, InfobarMessageType.Informational);
		}

		// Token: 0x0600281B RID: 10267 RVA: 0x000E31B4 File Offset: 0x000E13B4
		protected void LoadMessageBodyIntoStream(TextWriter writer)
		{
			bool flag = BodyConversionUtilities.GenerateEditableMessageBodyAndRenderInfobarMessages(base.Item, writer, this.newItemType, base.OwaContext, ref this.shouldPromptUserForUnblockingOnFormLoad, ref this.hasInlineImages, this.infobar, base.IsRequestCallbackForWebBeacons, this.bodyMarkup);
			if (flag)
			{
				base.Item.Load();
			}
		}

		// Token: 0x0600281C RID: 10268 RVA: 0x000E3208 File Offset: 0x000E1408
		protected void CreateAttachmentHelpers()
		{
			bool isPublicLogon = base.UserContext.IsPublicLogon;
			this.attachmentWellRenderObjects = AttachmentWell.GetAttachmentInformation(base.Item, base.AttachmentLinks, isPublicLogon);
			InfobarRenderingHelper infobarRenderingHelper = new InfobarRenderingHelper(this.attachmentWellRenderObjects);
			if (infobarRenderingHelper.HasLevelOne)
			{
				this.infobar.AddMessage(SanitizedHtmlString.FromStringId(-2118248931), InfobarMessageType.Informational, AttachmentWell.AttachmentInfobarHtmlTag);
			}
		}

		// Token: 0x0600281D RID: 10269 RVA: 0x000E3268 File Offset: 0x000E1468
		protected void RenderSender(TextWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			MeetingMessage meetingMessage = base.Item as MeetingMessage;
			if (Utilities.IsOnBehalfOf(meetingMessage.Sender, meetingMessage.From))
			{
				writer.Write(LocalizedStrings.GetHtmlEncoded(-165544498), RenderingUtilities.GetSender(base.UserContext, meetingMessage.Sender, "spnFrom", false, SenderDisplayMode.NameOnly), RenderingUtilities.GetSender(base.UserContext, meetingMessage.From, "spnOrg", false, SenderDisplayMode.NameOnly));
				return;
			}
			RenderingUtilities.RenderSender(base.UserContext, writer, meetingMessage.From, SenderDisplayMode.NameOnly, null);
		}

		// Token: 0x0600281E RID: 10270 RVA: 0x000E32F8 File Offset: 0x000E14F8
		protected void RenderSendOnBehalf(TextWriter writer)
		{
			MeetingMessage meetingMessage = (MeetingMessage)base.Item;
			if (this.isSendOnBehalfOf)
			{
				RenderingUtilities.RenderSendOnBehalf(writer, base.UserContext, meetingMessage.From);
			}
		}

		// Token: 0x0600281F RID: 10271 RVA: 0x000E332B File Offset: 0x000E152B
		protected void RenderToolbar()
		{
			this.toolbar.Render(base.SanitizingResponse);
		}

		// Token: 0x06002820 RID: 10272 RVA: 0x000E333E File Offset: 0x000E153E
		protected void RenderSubject(bool isTitle)
		{
			if (isTitle)
			{
				RenderingUtilities.RenderSubject(base.SanitizingResponse, base.Item, LocalizedStrings.GetNonEncoded(-1500721828));
				return;
			}
			RenderingUtilities.RenderSubject(base.SanitizingResponse, base.Item);
		}

		// Token: 0x06002821 RID: 10273 RVA: 0x000E3370 File Offset: 0x000E1570
		protected void RenderJavascriptEncodedMeetingRequestId()
		{
			if (this.meetingRequestId != null)
			{
				Utilities.JavascriptEncode(this.meetingRequestId.ToBase64String(), base.SanitizingResponse);
			}
		}

		// Token: 0x17000B63 RID: 2915
		// (get) Token: 0x06002822 RID: 10274 RVA: 0x000E3390 File Offset: 0x000E1590
		protected int CurrentStoreObjectType
		{
			get
			{
				return (int)this.itemType;
			}
		}

		// Token: 0x17000B64 RID: 2916
		// (get) Token: 0x06002823 RID: 10275 RVA: 0x000E3398 File Offset: 0x000E1598
		protected NewItemType ItemState
		{
			get
			{
				return this.newItemType;
			}
		}

		// Token: 0x17000B65 RID: 2917
		// (get) Token: 0x06002824 RID: 10276 RVA: 0x000E33A0 File Offset: 0x000E15A0
		protected int MeetingResponseType
		{
			get
			{
				return (int)this.responseType;
			}
		}

		// Token: 0x17000B66 RID: 2918
		// (get) Token: 0x06002825 RID: 10277 RVA: 0x000E33A8 File Offset: 0x000E15A8
		protected bool IsSendOnBehalfOf
		{
			get
			{
				return this.isSendOnBehalfOf;
			}
		}

		// Token: 0x17000B67 RID: 2919
		// (get) Token: 0x06002826 RID: 10278 RVA: 0x000E33B0 File Offset: 0x000E15B0
		protected SanitizedHtmlString When
		{
			get
			{
				return Utilities.SanitizeHtmlEncode(Utilities.GenerateWhen(base.Item));
			}
		}

		// Token: 0x17000B68 RID: 2920
		// (get) Token: 0x06002827 RID: 10279 RVA: 0x000E33C4 File Offset: 0x000E15C4
		protected SanitizedHtmlString Location
		{
			get
			{
				string text = base.Item.TryGetProperty(CalendarItemBaseSchema.Location) as string;
				if (text != null)
				{
					return Utilities.SanitizeHtmlEncode(text);
				}
				return SanitizedHtmlString.Empty;
			}
		}

		// Token: 0x17000B69 RID: 2921
		// (get) Token: 0x06002828 RID: 10280 RVA: 0x000E33F6 File Offset: 0x000E15F6
		protected bool ShowBcc
		{
			get
			{
				return this.showBcc;
			}
		}

		// Token: 0x17000B6A RID: 2922
		// (get) Token: 0x06002829 RID: 10281 RVA: 0x000E33FE File Offset: 0x000E15FE
		protected RecipientWell RecipientWell
		{
			get
			{
				return this.messageRecipientWell;
			}
		}

		// Token: 0x17000B6B RID: 2923
		// (get) Token: 0x0600282A RID: 10282 RVA: 0x000E3406 File Offset: 0x000E1606
		protected EditMessageToolbar Toolbar
		{
			get
			{
				return this.toolbar;
			}
		}

		// Token: 0x17000B6C RID: 2924
		// (get) Token: 0x0600282B RID: 10283 RVA: 0x000E340E File Offset: 0x000E160E
		protected Infobar Infobar
		{
			get
			{
				return this.infobar;
			}
		}

		// Token: 0x17000B6D RID: 2925
		// (get) Token: 0x0600282C RID: 10284 RVA: 0x000E3416 File Offset: 0x000E1616
		protected ArrayList AttachmentWellRenderObjects
		{
			get
			{
				return this.attachmentWellRenderObjects;
			}
		}

		// Token: 0x17000B6E RID: 2926
		// (get) Token: 0x0600282D RID: 10285 RVA: 0x000E341E File Offset: 0x000E161E
		protected Markup BodyMarkup
		{
			get
			{
				return this.bodyMarkup;
			}
		}

		// Token: 0x17000B6F RID: 2927
		// (get) Token: 0x0600282E RID: 10286 RVA: 0x000E3426 File Offset: 0x000E1626
		protected bool IsMeetingInviteInDeleteItems
		{
			get
			{
				return this.isMeetingInviteInDeleteItems;
			}
		}

		// Token: 0x17000B70 RID: 2928
		// (get) Token: 0x0600282F RID: 10287 RVA: 0x000E342E File Offset: 0x000E162E
		protected bool IsReadReceiptRequested
		{
			get
			{
				return !this.isNew && ((MessageItem)base.Item).IsReadReceiptRequested;
			}
		}

		// Token: 0x17000B71 RID: 2929
		// (get) Token: 0x06002830 RID: 10288 RVA: 0x000E344A File Offset: 0x000E164A
		protected bool IsDeliveryReceiptRequested
		{
			get
			{
				return !this.isNew && ((MessageItem)base.Item).IsDeliveryReceiptRequested;
			}
		}

		// Token: 0x17000B72 RID: 2930
		// (get) Token: 0x06002831 RID: 10289 RVA: 0x000E3466 File Offset: 0x000E1666
		protected bool ShouldPromptUser
		{
			get
			{
				return this.shouldPromptUserForUnblockingOnFormLoad;
			}
		}

		// Token: 0x17000B73 RID: 2931
		// (get) Token: 0x06002832 RID: 10290 RVA: 0x000E346E File Offset: 0x000E166E
		protected bool HasInlineImages
		{
			get
			{
				return this.hasInlineImages;
			}
		}

		// Token: 0x17000B74 RID: 2932
		// (get) Token: 0x06002833 RID: 10291 RVA: 0x000E3476 File Offset: 0x000E1676
		protected static int StoreObjectTypeMessage
		{
			get
			{
				return 9;
			}
		}

		// Token: 0x17000B75 RID: 2933
		// (get) Token: 0x06002834 RID: 10292 RVA: 0x000E347A File Offset: 0x000E167A
		protected static int StoreObjectTypeMeetingResponse
		{
			get
			{
				return 12;
			}
		}

		// Token: 0x17000B76 RID: 2934
		// (get) Token: 0x06002835 RID: 10293 RVA: 0x000E347E File Offset: 0x000E167E
		protected static int StoreObjectTypeMeetingRequest
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x17000B77 RID: 2935
		// (get) Token: 0x06002836 RID: 10294 RVA: 0x000E3482 File Offset: 0x000E1682
		protected static int StoreObjectTypeMeetingCancellation
		{
			get
			{
				return 13;
			}
		}

		// Token: 0x17000B78 RID: 2936
		// (get) Token: 0x06002837 RID: 10295 RVA: 0x000E3486 File Offset: 0x000E1686
		protected bool IsMeetingRequestIdNull
		{
			get
			{
				return this.meetingRequestId == null;
			}
		}

		// Token: 0x17000B79 RID: 2937
		// (get) Token: 0x06002838 RID: 10296 RVA: 0x000E3491 File Offset: 0x000E1691
		protected static int ImportanceLow
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000B7A RID: 2938
		// (get) Token: 0x06002839 RID: 10297 RVA: 0x000E3494 File Offset: 0x000E1694
		protected static int ImportanceNormal
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000B7B RID: 2939
		// (get) Token: 0x0600283A RID: 10298 RVA: 0x000E3497 File Offset: 0x000E1697
		protected static int ImportanceHigh
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x17000B7C RID: 2940
		// (get) Token: 0x0600283B RID: 10299 RVA: 0x000E349A File Offset: 0x000E169A
		protected int ItemSensitivity
		{
			get
			{
				return (int)base.Item.Sensitivity;
			}
		}

		// Token: 0x04001BE8 RID: 7144
		private static readonly PropertyDefinition[] prefetchProperties = new PropertyDefinition[]
		{
			MessageItemSchema.IsDraft,
			MessageItemSchema.IsDeliveryReceiptRequested,
			MessageItemSchema.HasBeenSubmitted
		};

		// Token: 0x04001BE9 RID: 7145
		private MessageRecipientWell messageRecipientWell;

		// Token: 0x04001BEA RID: 7146
		private ArrayList attachmentWellRenderObjects;

		// Token: 0x04001BEB RID: 7147
		private Infobar infobar = new Infobar();

		// Token: 0x04001BEC RID: 7148
		private EditMessageToolbar toolbar;

		// Token: 0x04001BED RID: 7149
		private bool showBcc;

		// Token: 0x04001BEE RID: 7150
		private bool isNew;

		// Token: 0x04001BEF RID: 7151
		private Markup bodyMarkup;

		// Token: 0x04001BF0 RID: 7152
		private bool shouldPromptUserForUnblockingOnFormLoad;

		// Token: 0x04001BF1 RID: 7153
		private bool hasInlineImages;

		// Token: 0x04001BF2 RID: 7154
		private StoreObjectType itemType = StoreObjectType.MeetingResponse;

		// Token: 0x04001BF3 RID: 7155
		private OwaStoreObjectId meetingRequestId;

		// Token: 0x04001BF4 RID: 7156
		private ResponseType responseType = ResponseType.Tentative;

		// Token: 0x04001BF5 RID: 7157
		private bool isMeetingInviteInDeleteItems;

		// Token: 0x04001BF6 RID: 7158
		private NewItemType newItemType = NewItemType.ExplicitDraft;

		// Token: 0x04001BF7 RID: 7159
		private bool isSendOnBehalfOf;
	}
}
