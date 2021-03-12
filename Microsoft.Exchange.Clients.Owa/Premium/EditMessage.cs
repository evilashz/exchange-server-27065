using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Premium.Controls;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Security;
using Microsoft.Exchange.Security.RightsManagement;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x02000451 RID: 1105
	public class EditMessage : EditMessageOrPostBase
	{
		// Token: 0x0600283E RID: 10302 RVA: 0x000E3503 File Offset: 0x000E1703
		public EditMessage() : base(false)
		{
		}

		// Token: 0x0600283F RID: 10303 RVA: 0x000E350C File Offset: 0x000E170C
		internal EditMessage(bool setNoCacheNoStore) : base(setNoCacheNoStore)
		{
		}

		// Token: 0x17000B7D RID: 2941
		// (get) Token: 0x06002840 RID: 10304 RVA: 0x000E3515 File Offset: 0x000E1715
		protected virtual bool IsPageCacheable
		{
			get
			{
				return this.IsNewMessage && !this.IsSMimeControlNeeded;
			}
		}

		// Token: 0x06002841 RID: 10305 RVA: 0x000E352A File Offset: 0x000E172A
		protected virtual void InitializeMessage()
		{
			this.message = base.Initialize<MessageItem>(false, EditMessage.PrefetchProperties);
			if (this.message != null && base.UserContext.IsIrmEnabled)
			{
				Utilities.IrmDecryptIfRestricted(this.message, base.UserContext, true);
			}
		}

		// Token: 0x06002842 RID: 10306 RVA: 0x000E3566 File Offset: 0x000E1766
		protected virtual void CreateDraftMessage()
		{
			this.message = Utilities.CreateDraftMessageFromQueryString(base.UserContext, base.Request);
			if (this.message != null)
			{
				this.newItemType = NewItemType.ImplicitDraft;
				base.DeleteExistingDraft = true;
				base.Item = this.message;
			}
		}

		// Token: 0x17000B7E RID: 2942
		// (get) Token: 0x06002843 RID: 10307 RVA: 0x000E35A1 File Offset: 0x000E17A1
		protected virtual bool PageSupportSmime
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002844 RID: 10308 RVA: 0x000E35A4 File Offset: 0x000E17A4
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			if (this.IsPageCacheable)
			{
				Utilities.MakePageCacheable(base.Response);
			}
			else
			{
				Utilities.MakePageNoCacheNoStore(base.Response);
			}
			this.InitializeMessage();
			if (this.PageSupportSmime && this.message != null && Utilities.IsSMime(this.message))
			{
				if (!Utilities.IsSMimeFeatureUsable(base.OwaContext))
				{
					throw new OwaNeedsSMimeControlToEditDraftException(LocalizedStrings.GetNonEncoded(-1507367759));
				}
				if (Utilities.IsFlagSet((int)base.ClientSMimeControlStatus, 1))
				{
					throw new OwaNeedsSMimeControlToEditDraftException(LocalizedStrings.GetNonEncoded(-872682934));
				}
				if (Utilities.IsFlagSet((int)base.ClientSMimeControlStatus, 2))
				{
					throw new OwaNeedsSMimeControlToEditDraftException(LocalizedStrings.GetNonEncoded(-1103993212));
				}
				if (!Utilities.CheckSMimeEditFormBasicRequirement(base.ClientSMimeControlStatus, base.OwaContext))
				{
					throw new OwaNeedsSMimeControlToEditDraftException(LocalizedStrings.GetNonEncoded(-1507367759));
				}
			}
			if (!base.UserContext.IsIrmEnabled && this.message != null && Utilities.IsIrmRestricted(this.message))
			{
				SanitizedHtmlString sanitizedHtmlString = SanitizedHtmlString.Format(LocalizedStrings.GetHtmlEncoded(1049269714), new object[]
				{
					Utilities.GetOfficeDownloadAnchor(Microsoft.Exchange.Data.Storage.BodyFormat.TextPlain, base.UserContext.UserCulture)
				});
				throw new OwaCannotEditIrmDraftException(sanitizedHtmlString.ToString());
			}
			if (this.message != null)
			{
				string action = base.OwaContext.FormsRegistryContext.Action;
				string state = base.OwaContext.FormsRegistryContext.State;
				if (string.CompareOrdinal(action, "Reply") == 0 || string.CompareOrdinal(action, "ReplyAll") == 0)
				{
					this.newItemType = NewItemType.Reply;
				}
				else if (string.CompareOrdinal(action, "Forward") == 0)
				{
					this.newItemType = NewItemType.Forward;
				}
				else if (string.Equals(action, "Open", StringComparison.OrdinalIgnoreCase) && string.Equals(state, "Draft", StringComparison.OrdinalIgnoreCase))
				{
					this.newItemType = NewItemType.ExplicitDraft;
					if (this.message.GetValueOrDefault<bool>(MessageItemSchema.HasBeenSubmitted))
					{
						this.message.AbortSubmit();
					}
				}
				else
				{
					this.newItemType = NewItemType.ImplicitDraft;
					base.DeleteExistingDraft = true;
				}
			}
			else
			{
				this.CreateDraftMessage();
			}
			if (this.message != null && Utilities.IsPublic(this.message))
			{
				throw new OwaInvalidRequestException("No way to open a public message in edit form");
			}
			if (this.IsSMimeControlNeeded && this.PageSupportSmime)
			{
				this.bodyMarkup = Markup.Html;
			}
			else
			{
				this.bodyMarkup = BodyConversionUtilities.GetBodyFormatOfEditItem(base.Item, this.newItemType, base.UserContext.UserOptions);
			}
			this.SetIsOtherFolder();
			this.infobar.SetInfobarClass("infobarEdit");
			this.infobar.SetShouldHonorHideByDefault(true);
			if (this.newItemType != NewItemType.New)
			{
				if (this.newItemType == NewItemType.ExplicitDraft)
				{
					this.infobar.AddMessage(SanitizedHtmlString.FromStringId(-1981719796), InfobarMessageType.Informational);
				}
				InfobarMessageBuilder.AddSensitivity(this.infobar, this.message);
				if (this.newItemType != NewItemType.ImplicitDraft)
				{
					InfobarMessageBuilder.AddCompliance(base.UserContext, this.infobar, this.message, true);
				}
				if (base.UserContext.IsIrmEnabled && this.message != null)
				{
					InfobarMessageBuilder.AddIrmInformation(this.infobar, this.message, false, true, false, this.IsIrmAsAttachment);
				}
				this.recipientWell = new MessageRecipientWell(this.message);
				this.showBcc = (this.recipientWell.HasRecipients(RecipientWellType.Bcc) || base.UserContext.UserOptions.AlwaysShowBcc);
				this.showFrom = (this.recipientWell.HasRecipients(RecipientWellType.From) || base.UserContext.UserOptions.AlwaysShowFrom || this.isOtherFolder);
				this.toolbar = this.BuildToolbar();
				this.toolbar.ToolbarType = (base.IsPreviewForm ? ToolbarType.Preview : ToolbarType.Form);
				this.addSignatureToBody = base.ShouldAddSignatureToBody(this.bodyMarkup, this.newItemType);
			}
			else
			{
				this.recipientWell = new MessageRecipientWell();
				this.showBcc = base.UserContext.UserOptions.AlwaysShowBcc;
				this.showFrom = (base.UserContext.UserOptions.AlwaysShowFrom || this.isOtherFolder);
				this.toolbar = new EditMessageToolbar(Importance.Normal, this.bodyMarkup, this.IsSMimeControlMustUpdate, this.IsSMimeControlNeeded, false, false);
				this.toolbar.ToolbarType = (base.IsPreviewForm ? ToolbarType.Preview : ToolbarType.Form);
			}
			if (base.OwaContext.UserContext.IsFeatureEnabled(Feature.SMime) && this.PageSupportSmime)
			{
				if (this.IsSMimeControlNeeded && Utilities.IsFlagSet((int)base.ClientSMimeControlStatus, 4))
				{
					this.infobar.AddMessage(SanitizedHtmlString.FromStringId(330022834), InfobarMessageType.Informational);
				}
				else if (this.IsSMimeControlMustUpdate)
				{
					this.infobar.AddMessage(SanitizedHtmlString.FromStringId(1697878138), InfobarMessageType.Informational);
				}
				if (Utilities.IsSMimeControlNeededForEditForm(base.ClientSMimeControlStatus, base.OwaContext) && this.ShowFrom)
				{
					this.infobar.AddMessage(SanitizedHtmlString.FromStringId(-1863471683), InfobarMessageType.Informational);
				}
				if (!Utilities.IsFlagSet((int)base.ClientSMimeControlStatus, 1) && !Utilities.IsFlagSet((int)base.ClientSMimeControlStatus, 16) && !base.OwaContext.UserContext.IsExplicitLogonOthersMailbox)
				{
					this.infobar.AddMessage(SanitizedHtmlString.FromStringId(-1908761042), InfobarMessageType.Warning);
				}
			}
			if (this.Message != null && this.IsRemoveRestricted)
			{
				this.toolbar.IsComplianceButtonEnabledInForm = false;
			}
			if (this.ShowFrom && this.IsFromWellRestricted)
			{
				this.infobar.AddMessage(SanitizedHtmlString.FromStringId(885106754), InfobarMessageType.Informational);
			}
			if (this.message != null && this.newItemType == NewItemType.ExplicitDraft && Utilities.IsInArchiveMailbox(base.Item))
			{
				this.toolbar.IsSendButtonEnabledInForm = false;
			}
		}

		// Token: 0x06002845 RID: 10309 RVA: 0x000E3B10 File Offset: 0x000E1D10
		protected void CreateAttachmentHelpers()
		{
			if (this.message != null)
			{
				this.attachmentWellRenderObjects = AttachmentWell.GetAttachmentInformation(this.message, base.AttachmentLinks, base.UserContext.IsPublicLogon);
				InfobarRenderingHelper infobarRenderingHelper = new InfobarRenderingHelper(this.attachmentWellRenderObjects);
				if (infobarRenderingHelper.HasLevelOne)
				{
					this.infobar.AddMessage(SanitizedHtmlString.FromStringId(-2118248931), InfobarMessageType.Informational, AttachmentWell.AttachmentInfobarHtmlTag);
				}
			}
		}

		// Token: 0x06002846 RID: 10310 RVA: 0x000E3B76 File Offset: 0x000E1D76
		protected void RenderSubject(bool isTitle)
		{
			if (isTitle)
			{
				RenderingUtilities.RenderSubject(base.SanitizingResponse, this.message, LocalizedStrings.GetNonEncoded(730745110));
				return;
			}
			RenderingUtilities.RenderSubject(base.SanitizingResponse, this.message);
		}

		// Token: 0x06002847 RID: 10311 RVA: 0x000E3BA8 File Offset: 0x000E1DA8
		protected void RenderJavascriptEncodedMessageChangeKey()
		{
			Utilities.JavascriptEncode(this.message.Id.ChangeKeyAsBase64String(), base.SanitizingResponse);
		}

		// Token: 0x06002848 RID: 10312 RVA: 0x000E3BC5 File Offset: 0x000E1DC5
		protected void RenderJavascriptEncodedSourceItemId()
		{
			Utilities.JavascriptEncode(this.SourceItemIdQueryString, base.SanitizingResponse);
		}

		// Token: 0x17000B7F RID: 2943
		// (get) Token: 0x06002849 RID: 10313 RVA: 0x000E3BD8 File Offset: 0x000E1DD8
		protected Markup BodyMarkup
		{
			get
			{
				return this.bodyMarkup;
			}
		}

		// Token: 0x17000B80 RID: 2944
		// (get) Token: 0x0600284A RID: 10314 RVA: 0x000E3BE0 File Offset: 0x000E1DE0
		protected virtual bool ShowBcc
		{
			get
			{
				return this.showBcc;
			}
		}

		// Token: 0x17000B81 RID: 2945
		// (get) Token: 0x0600284B RID: 10315 RVA: 0x000E3BE8 File Offset: 0x000E1DE8
		protected virtual bool ShowFrom
		{
			get
			{
				return this.showFrom;
			}
		}

		// Token: 0x17000B82 RID: 2946
		// (get) Token: 0x0600284C RID: 10316 RVA: 0x000E3BF0 File Offset: 0x000E1DF0
		// (set) Token: 0x0600284D RID: 10317 RVA: 0x000E3BF8 File Offset: 0x000E1DF8
		protected MessageRecipientWell RecipientWell
		{
			get
			{
				return this.recipientWell;
			}
			set
			{
				this.recipientWell = value;
			}
		}

		// Token: 0x0600284E RID: 10318 RVA: 0x000E3C04 File Offset: 0x000E1E04
		protected void RenderSMimeFromRecipientWell()
		{
			RecipientWell recipientWell = new EditMessage.SMimeFromRecipientWell(base.UserContext);
			recipientWell.Render(base.SanitizingResponse, base.UserContext, RecipientWellType.To, Microsoft.Exchange.Clients.Owa.Premium.Controls.RecipientWell.RenderFlags.Hidden, "SMimeFrom");
		}

		// Token: 0x17000B83 RID: 2947
		// (get) Token: 0x0600284F RID: 10319 RVA: 0x000E3C36 File Offset: 0x000E1E36
		// (set) Token: 0x06002850 RID: 10320 RVA: 0x000E3C3E File Offset: 0x000E1E3E
		protected EditMessageToolbar Toolbar
		{
			get
			{
				return this.toolbar;
			}
			set
			{
				this.toolbar = value;
			}
		}

		// Token: 0x17000B84 RID: 2948
		// (get) Token: 0x06002851 RID: 10321 RVA: 0x000E3C47 File Offset: 0x000E1E47
		protected int MessageSensitivity
		{
			get
			{
				if (this.message == null)
				{
					return 0;
				}
				return (int)this.message.Sensitivity;
			}
		}

		// Token: 0x17000B85 RID: 2949
		// (get) Token: 0x06002852 RID: 10322 RVA: 0x000E3C5E File Offset: 0x000E1E5E
		protected bool IsReadReceiptRequested
		{
			get
			{
				return this.message != null && this.message.IsReadReceiptRequested;
			}
		}

		// Token: 0x17000B86 RID: 2950
		// (get) Token: 0x06002853 RID: 10323 RVA: 0x000E3C75 File Offset: 0x000E1E75
		protected bool IsDeliveryReceiptRequested
		{
			get
			{
				return this.message != null && this.message.IsDeliveryReceiptRequested;
			}
		}

		// Token: 0x17000B87 RID: 2951
		// (get) Token: 0x06002854 RID: 10324 RVA: 0x000E3C8C File Offset: 0x000E1E8C
		protected string SourceItemIdQueryString
		{
			get
			{
				return Utilities.GetQueryStringParameter(base.Request, "srcId", false) ?? string.Empty;
			}
		}

		// Token: 0x17000B88 RID: 2952
		// (get) Token: 0x06002855 RID: 10325 RVA: 0x000E3CA8 File Offset: 0x000E1EA8
		protected bool IsReplyForward
		{
			get
			{
				return base.NewItemType == NewItemType.Reply || base.NewItemType == NewItemType.Forward;
			}
		}

		// Token: 0x17000B89 RID: 2953
		// (get) Token: 0x06002856 RID: 10326 RVA: 0x000E3CBE File Offset: 0x000E1EBE
		protected bool IsOtherFolder
		{
			get
			{
				return this.isOtherFolder;
			}
		}

		// Token: 0x17000B8A RID: 2954
		// (get) Token: 0x06002857 RID: 10327 RVA: 0x000E3CC6 File Offset: 0x000E1EC6
		protected bool IsSMimeControlNeeded
		{
			get
			{
				return Utilities.IsSMimeControlNeededForEditForm(base.ClientSMimeControlStatus, base.OwaContext) && !this.ShowFrom && !this.IsOtherFolder && (this.message == null || !Utilities.IsIrmRestricted(this.message));
			}
		}

		// Token: 0x17000B8B RID: 2955
		// (get) Token: 0x06002858 RID: 10328 RVA: 0x000E3D05 File Offset: 0x000E1F05
		protected bool IsSMimeControlMustUpdate
		{
			get
			{
				return Utilities.CheckSMimeEditFormBasicRequirement(base.ClientSMimeControlStatus, base.OwaContext) && Utilities.IsFlagSet((int)base.ClientSMimeControlStatus, 2);
			}
		}

		// Token: 0x17000B8C RID: 2956
		// (get) Token: 0x06002859 RID: 10329 RVA: 0x000E3D28 File Offset: 0x000E1F28
		protected bool ForceFilterWebBeacons
		{
			get
			{
				return base.UserContext.Configuration.FilterWebBeaconsAndHtmlForms == WebBeaconFilterLevels.ForceFilter;
			}
		}

		// Token: 0x0600285A RID: 10330 RVA: 0x000E3D40 File Offset: 0x000E1F40
		protected void LoadComposeBody()
		{
			if (this.PageSupportSmime && this.IsSMimeControlNeeded && base.Item != null && base.NewItemType != NewItemType.ImplicitDraft && Utilities.GetQueryStringParameter(base.Request, "srcId", false) == null)
			{
				return;
			}
			base.LoadMessageBodyIntoStream(base.SanitizingResponse);
			if (this.message != null && base.UserContext.IsIrmEnabled)
			{
				Utilities.IrmDecryptIfRestricted(this.message, base.UserContext, true);
			}
		}

		// Token: 0x0600285B RID: 10331 RVA: 0x000E3DB8 File Offset: 0x000E1FB8
		protected void RenderSMimeSavingSendingWarnings(TextWriter writer)
		{
			SanitizingStringBuilder<OwaHtml> sanitizingStringBuilder = new SanitizingStringBuilder<OwaHtml>();
			sanitizingStringBuilder.Append("<div>");
			sanitizingStringBuilder.Append(LocalizedStrings.GetNonEncoded(-469017928));
			if (!OwaRegistryKeys.AlwaysEncrypt)
			{
				sanitizingStringBuilder.Append(LocalizedStrings.GetNonEncoded(302877857));
			}
			sanitizingStringBuilder.Append("</div><br><div>");
			sanitizingStringBuilder.Append(LocalizedStrings.GetNonEncoded(1922803157));
			sanitizingStringBuilder.Append("</div>");
			RenderingUtilities.RenderStringVariable(writer, "L_NECSv", sanitizingStringBuilder.ToSanitizedString<SanitizedHtmlString>());
			sanitizingStringBuilder = new SanitizingStringBuilder<OwaHtml>();
			sanitizingStringBuilder.Append("<div>");
			sanitizingStringBuilder.Append(LocalizedStrings.GetNonEncoded(1408049888));
			if (!OwaRegistryKeys.AlwaysEncrypt)
			{
				sanitizingStringBuilder.Append(LocalizedStrings.GetNonEncoded(1751385189));
			}
			sanitizingStringBuilder.Append("</div><br><div>");
			sanitizingStringBuilder.Append(LocalizedStrings.GetNonEncoded(1922803157));
			sanitizingStringBuilder.Append("</div>");
			RenderingUtilities.RenderStringVariable(writer, "L_NECSnd", sanitizingStringBuilder.ToSanitizedString<SanitizedHtmlString>());
			sanitizingStringBuilder = new SanitizingStringBuilder<OwaHtml>();
			sanitizingStringBuilder.Append("<div>");
			sanitizingStringBuilder.Append(LocalizedStrings.GetNonEncoded(1971020450));
			if (!OwaRegistryKeys.AlwaysSign)
			{
				sanitizingStringBuilder.Append(LocalizedStrings.GetNonEncoded(721900430));
			}
			sanitizingStringBuilder.Append("</div><br><div>");
			sanitizingStringBuilder.Append(LocalizedStrings.GetNonEncoded(OwaRegistryKeys.AllowUserChoiceOfSigningCertificate ? 2099415568 : 1922347219));
			sanitizingStringBuilder.Append("</div>");
			RenderingUtilities.RenderStringVariable(writer, "L_NSCSv", sanitizingStringBuilder.ToSanitizedString<SanitizedHtmlString>());
			sanitizingStringBuilder = new SanitizingStringBuilder<OwaHtml>();
			sanitizingStringBuilder.Append("<div>");
			sanitizingStringBuilder.Append(LocalizedStrings.GetNonEncoded(-2072041142));
			if (!OwaRegistryKeys.AlwaysSign)
			{
				sanitizingStringBuilder.Append(LocalizedStrings.GetNonEncoded(-1301978547));
			}
			sanitizingStringBuilder.Append("</div><br><div>");
			sanitizingStringBuilder.Append(LocalizedStrings.GetNonEncoded(OwaRegistryKeys.AllowUserChoiceOfSigningCertificate ? 2099415568 : 1922347219));
			sanitizingStringBuilder.Append("</div>");
			RenderingUtilities.RenderStringVariable(writer, "L_NSCSnd", sanitizingStringBuilder.ToSanitizedString<SanitizedHtmlString>());
			if (base.UserContext.UserOptions.UseManuallyPickedSigningCertificate)
			{
				sanitizingStringBuilder = new SanitizingStringBuilder<OwaHtml>();
				sanitizingStringBuilder.Append("<div>");
				sanitizingStringBuilder.Append(LocalizedStrings.GetNonEncoded(1737209722));
				if (!OwaRegistryKeys.AlwaysSign)
				{
					sanitizingStringBuilder.Append(LocalizedStrings.GetNonEncoded(-1301978547));
				}
				sanitizingStringBuilder.Append("</div><br><div>");
				sanitizingStringBuilder.Append(LocalizedStrings.GetNonEncoded(2099415568));
				sanitizingStringBuilder.Append("</div>");
				RenderingUtilities.RenderStringVariable(writer, "L_ISCSnd", sanitizingStringBuilder.ToSanitizedString<SanitizedHtmlString>());
				sanitizingStringBuilder = new SanitizingStringBuilder<OwaHtml>();
				sanitizingStringBuilder.Append("<div>");
				sanitizingStringBuilder.Append(LocalizedStrings.GetNonEncoded(1737209722));
				if (!OwaRegistryKeys.AlwaysSign)
				{
					sanitizingStringBuilder.Append(LocalizedStrings.GetNonEncoded(721900430));
				}
				sanitizingStringBuilder.Append("</div><br><div>");
				sanitizingStringBuilder.Append(LocalizedStrings.GetNonEncoded(2099415568));
				sanitizingStringBuilder.Append("</div>");
				RenderingUtilities.RenderStringVariable(writer, "L_ISCSv", sanitizingStringBuilder.ToSanitizedString<SanitizedHtmlString>());
			}
		}

		// Token: 0x0600285C RID: 10332 RVA: 0x000E4090 File Offset: 0x000E2290
		protected void RenderDefaultMaximumAttachmentSize(TextWriter writer)
		{
			RenderingUtilities.RenderInteger(writer, "a_iDMAS", 5);
		}

		// Token: 0x17000B8D RID: 2957
		// (get) Token: 0x0600285D RID: 10333 RVA: 0x000E40A0 File Offset: 0x000E22A0
		protected bool IsNewMessage
		{
			get
			{
				return base.OwaContext.FormsRegistryContext.Action.Equals("New", StringComparison.OrdinalIgnoreCase) && base.OwaContext.FormsRegistryContext.Type.Equals("IPM.Note", StringComparison.OrdinalIgnoreCase) && Utilities.GetQueryStringParameter(base.Request, "cc", false) != null;
			}
		}

		// Token: 0x17000B8E RID: 2958
		// (get) Token: 0x0600285E RID: 10334 RVA: 0x000E40FD File Offset: 0x000E22FD
		protected virtual int CurrentStoreObjectType
		{
			get
			{
				if (this.message != null && (ObjectClass.IsOfClass(this.message.ClassName, "IPM.Note.Microsoft.Approval.Reply.Approve") || ObjectClass.IsOfClass(this.message.ClassName, "IPM.Note.Microsoft.Approval.Reply.Reject")))
				{
					return 27;
				}
				return 9;
			}
		}

		// Token: 0x0600285F RID: 10335 RVA: 0x000E413C File Offset: 0x000E233C
		protected void SetIsOtherFolder()
		{
			string queryStringParameter = Utilities.GetQueryStringParameter(base.Request, "fOMF", false);
			this.isOtherFolder = (string.CompareOrdinal(queryStringParameter, "1") == 0);
		}

		// Token: 0x17000B8F RID: 2959
		// (get) Token: 0x06002860 RID: 10336 RVA: 0x000E416F File Offset: 0x000E236F
		// (set) Token: 0x06002861 RID: 10337 RVA: 0x000E4177 File Offset: 0x000E2377
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

		// Token: 0x17000B90 RID: 2960
		// (get) Token: 0x06002862 RID: 10338 RVA: 0x000E4180 File Offset: 0x000E2380
		protected bool IsIrmProtected
		{
			get
			{
				return base.UserContext.IsIrmEnabled && this.message != null && Utilities.IsIrmRestricted(this.message);
			}
		}

		// Token: 0x17000B91 RID: 2961
		// (get) Token: 0x06002863 RID: 10339 RVA: 0x000E41A4 File Offset: 0x000E23A4
		protected bool IsIrmAsAttachment
		{
			get
			{
				string queryStringParameter = Utilities.GetQueryStringParameter(base.Request, "fIrmAsAttach", false);
				return string.CompareOrdinal("1", queryStringParameter) == 0;
			}
		}

		// Token: 0x17000B92 RID: 2962
		// (get) Token: 0x06002864 RID: 10340 RVA: 0x000E41D4 File Offset: 0x000E23D4
		protected bool IsRecipientWellRestricted
		{
			get
			{
				if (this.message == null)
				{
					return false;
				}
				if (!base.OwaContext.UserContext.IsIrmEnabled)
				{
					return false;
				}
				if (!Utilities.IsIrmRestrictedAndDecrypted(this.message))
				{
					return false;
				}
				RightsManagedMessageItem rightsManagedMessageItem = (RightsManagedMessageItem)this.message;
				return !rightsManagedMessageItem.UsageRights.IsUsageRightGranted(ContentRight.Forward) || (rightsManagedMessageItem.Restriction.RequiresRepublishingWhenRecipientsChange && !rightsManagedMessageItem.CanRepublish);
			}
		}

		// Token: 0x17000B93 RID: 2963
		// (get) Token: 0x06002865 RID: 10341 RVA: 0x000E4246 File Offset: 0x000E2446
		protected bool IsFromWellRestricted
		{
			get
			{
				return this.IsUsageRightRestricted(ContentRight.Owner);
			}
		}

		// Token: 0x17000B94 RID: 2964
		// (get) Token: 0x06002866 RID: 10342 RVA: 0x000E4250 File Offset: 0x000E2450
		protected bool IsPrintRestricted
		{
			get
			{
				return this.IsUsageRightRestricted(ContentRight.Print);
			}
		}

		// Token: 0x17000B95 RID: 2965
		// (get) Token: 0x06002867 RID: 10343 RVA: 0x000E4259 File Offset: 0x000E2459
		protected bool IsCopyRestricted
		{
			get
			{
				return this.IsUsageRightRestricted(ContentRight.Extract);
			}
		}

		// Token: 0x17000B96 RID: 2966
		// (get) Token: 0x06002868 RID: 10344 RVA: 0x000E4262 File Offset: 0x000E2462
		protected bool IsRemoveRestricted
		{
			get
			{
				return this.IsUsageRightRestricted(ContentRight.Export);
			}
		}

		// Token: 0x17000B97 RID: 2967
		// (get) Token: 0x06002869 RID: 10345 RVA: 0x000E426F File Offset: 0x000E246F
		protected bool IsNotOwner
		{
			get
			{
				return this.IsIrmProtected && this.IsUsageRightRestricted(ContentRight.Owner);
			}
		}

		// Token: 0x0600286A RID: 10346 RVA: 0x000E4284 File Offset: 0x000E2484
		protected virtual EditMessageToolbar BuildToolbar()
		{
			return new EditMessageToolbar(this.message.Importance, this.bodyMarkup, this.IsSMimeControlMustUpdate && this.PageSupportSmime, this.IsSMimeControlNeeded && this.PageSupportSmime, this.IsIrmProtected, this.IsNotOwner);
		}

		// Token: 0x0600286B RID: 10347 RVA: 0x000E42D8 File Offset: 0x000E24D8
		private bool IsUsageRightRestricted(ContentRight right)
		{
			return this.message != null && base.OwaContext.UserContext.IsIrmEnabled && Utilities.IsIrmRestrictedAndDecrypted(this.message) && !((RightsManagedMessageItem)this.message).UsageRights.IsUsageRightGranted(right);
		}

		// Token: 0x04001BF8 RID: 7160
		internal static readonly StorePropertyDefinition[] PrefetchProperties = new StorePropertyDefinition[]
		{
			ItemSchema.BlockStatus,
			ItemSchema.IsClassified,
			ItemSchema.Classification,
			ItemSchema.ClassificationDescription,
			ItemSchema.ClassificationGuid,
			ItemSchema.EdgePcl,
			ItemSchema.LinkEnabled,
			MessageItemSchema.IsDeliveryReceiptRequested,
			MessageItemSchema.HasBeenSubmitted
		};

		// Token: 0x04001BF9 RID: 7161
		private MessageItem message;

		// Token: 0x04001BFA RID: 7162
		private MessageRecipientWell recipientWell;

		// Token: 0x04001BFB RID: 7163
		private EditMessageToolbar toolbar;

		// Token: 0x04001BFC RID: 7164
		private bool showBcc;

		// Token: 0x04001BFD RID: 7165
		private bool showFrom;

		// Token: 0x04001BFE RID: 7166
		private bool isOtherFolder;

		// Token: 0x02000452 RID: 1106
		private class SMimeFromRecipientWell : ItemRecipientWell
		{
			// Token: 0x0600286D RID: 10349 RVA: 0x000E438F File Offset: 0x000E258F
			internal SMimeFromRecipientWell(UserContext userContext)
			{
				this.userContext = userContext;
			}

			// Token: 0x0600286E RID: 10350 RVA: 0x000E4424 File Offset: 0x000E2624
			internal override IEnumerator<Participant> GetRecipientsCollection(RecipientWellType type)
			{
				yield return new Participant(this.userContext.MailboxIdentity.GetOWAMiniRecipient());
				yield break;
			}

			// Token: 0x04001BFF RID: 7167
			private UserContext userContext;
		}
	}
}
