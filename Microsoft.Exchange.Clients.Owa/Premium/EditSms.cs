using System;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Premium.Controls;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x02000456 RID: 1110
	public class EditSms : EditMessageOrPostBase
	{
		// Token: 0x060028CD RID: 10445 RVA: 0x000E791C File Offset: 0x000E5B1C
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			E164Number smsSyncPhoneNumber = null;
			string smsSyncDeviceProtocol = null;
			string smsSyncDeviceType = null;
			string smsSyncDeviceId = null;
			string text = null;
			this.IsSmsAccountEnabled = TextMessagingUtilities.IsSmsSyncEnabled(base.UserContext, out smsSyncPhoneNumber, out smsSyncDeviceProtocol, out smsSyncDeviceType, out smsSyncDeviceId, out text);
			string text2 = null;
			if (this.IsSmsAccountEnabled && TextMessagingUtilities.IsSmsSyncDeviceInactive(base.UserContext, smsSyncPhoneNumber, smsSyncDeviceProtocol, smsSyncDeviceType, smsSyncDeviceId, out text2))
			{
				if (string.IsNullOrEmpty(text2))
				{
					text2 = text;
				}
				SanitizedHtmlString messageHtml;
				if (string.IsNullOrEmpty(text2))
				{
					messageHtml = SanitizedHtmlString.Format(LocalizedStrings.GetHtmlEncoded(2099454980), new object[]
					{
						EditSms.SmsSyncHelpLinkBeginTag,
						EditSms.SmsSyncHelpLinkEndTag
					});
				}
				else
				{
					messageHtml = SanitizedHtmlString.Format(LocalizedStrings.GetHtmlEncoded(739255527), new object[]
					{
						text2,
						EditSms.SmsSyncHelpLinkBeginTag,
						EditSms.SmsSyncHelpLinkEndTag
					});
				}
				base.Infobar.AddMessage(messageHtml, InfobarMessageType.Informational);
			}
			this.message = base.Initialize<MessageItem>(false, new PropertyDefinition[0]);
			this.bodyMarkup = Markup.PlainText;
			if (this.message != null)
			{
				string action = base.OwaContext.FormsRegistryContext.Action;
				string state = base.OwaContext.FormsRegistryContext.State;
				if (string.CompareOrdinal(action, "Reply") == 0)
				{
					this.newItemType = NewItemType.Reply;
				}
				else if (string.CompareOrdinal(action, "Forward") == 0)
				{
					this.newItemType = NewItemType.Forward;
				}
				else if (string.CompareOrdinal(action, "Open") == 0 && string.CompareOrdinal(state, "Draft") == 0)
				{
					this.newItemType = NewItemType.ExplicitDraft;
				}
				else
				{
					this.newItemType = NewItemType.ImplicitDraft;
					base.DeleteExistingDraft = true;
				}
			}
			else
			{
				this.message = Utilities.CreateDraftMessageFromQueryString(base.UserContext, base.Request);
				if (this.message != null)
				{
					this.newItemType = NewItemType.ImplicitDraft;
					base.DeleteExistingDraft = true;
					if (!ObjectClass.IsSmsMessage(this.message.ClassName))
					{
						this.message.ClassName = "IPM.Note.Mobile.SMS";
						this.message.Save(SaveMode.ResolveConflicts);
						this.message.Load();
					}
					base.Item = this.message;
				}
			}
			if (this.newItemType != NewItemType.New)
			{
				if (this.newItemType == NewItemType.ExplicitDraft)
				{
					this.infobar.AddMessage(SanitizedHtmlString.FromStringId(-1981719796), InfobarMessageType.Informational);
				}
				this.recipientWell = new MessageRecipientWell(this.message);
				if (this.newItemType == NewItemType.Reply && this.recipientWell != null && !this.recipientWell.HasRecipients(RecipientWellType.To))
				{
					this.infobar.AddMessage(SanitizedHtmlString.FromStringId(-575462747), InfobarMessageType.Warning, "divSmsReNumMis");
				}
			}
			else
			{
				this.recipientWell = new MessageRecipientWell();
			}
			this.toolbar = new EditSmsToolbar();
		}

		// Token: 0x060028CE RID: 10446 RVA: 0x000E7BB6 File Offset: 0x000E5DB6
		protected void RenderTitle()
		{
			base.Response.Write(LocalizedStrings.GetHtmlEncoded(1509309420));
		}

		// Token: 0x17000BB4 RID: 2996
		// (get) Token: 0x060028CF RID: 10447 RVA: 0x000E7BCD File Offset: 0x000E5DCD
		protected Toolbar Toolbar
		{
			get
			{
				return this.toolbar;
			}
		}

		// Token: 0x17000BB5 RID: 2997
		// (get) Token: 0x060028D0 RID: 10448 RVA: 0x000E7BD5 File Offset: 0x000E5DD5
		protected Markup BodyMarkup
		{
			get
			{
				return this.bodyMarkup;
			}
		}

		// Token: 0x17000BB6 RID: 2998
		// (get) Token: 0x060028D1 RID: 10449 RVA: 0x000E7BDD File Offset: 0x000E5DDD
		protected RecipientWell RecipientWell
		{
			get
			{
				return this.recipientWell;
			}
		}

		// Token: 0x17000BB7 RID: 2999
		// (get) Token: 0x060028D2 RID: 10450 RVA: 0x000E7BE5 File Offset: 0x000E5DE5
		// (set) Token: 0x060028D3 RID: 10451 RVA: 0x000E7BED File Offset: 0x000E5DED
		private protected bool IsSmsAccountEnabled { protected get; private set; }

		// Token: 0x060028D4 RID: 10452 RVA: 0x000E7BF8 File Offset: 0x000E5DF8
		protected void RenderFontStyle(bool forIE)
		{
			if (base.UserContext.IsInternetExplorer7() != forIE)
			{
				return;
			}
			UserOptions userOptions = base.UserContext.UserOptions;
			base.Response.Write(OwaPlainTextStyle.GetStyleContentFromUserOption(userOptions, true));
		}

		// Token: 0x04001C11 RID: 7185
		private static readonly SanitizedHtmlString SmsSyncHelpLinkBeginTag = SanitizedHtmlString.GetSanitizedStringWithoutEncoding(string.Format("<a href=\"#\" {0}>", Utilities.GetScriptHandler("onclick", string.Format("opnHlp(\"{0}\");", "http://go.microsoft.com/fwlink/?LinkId=186816"))));

		// Token: 0x04001C12 RID: 7186
		private static readonly SanitizedHtmlString SmsSyncHelpLinkEndTag = SanitizedHtmlString.GetSanitizedStringWithoutEncoding("</a>");

		// Token: 0x04001C13 RID: 7187
		private MessageItem message;

		// Token: 0x04001C14 RID: 7188
		private MessageRecipientWell recipientWell;

		// Token: 0x04001C15 RID: 7189
		private EditSmsToolbar toolbar;
	}
}
