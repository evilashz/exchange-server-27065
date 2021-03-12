using System;
using System.Globalization;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x0200036C RID: 876
	public class EditMessageToolbar : Toolbar
	{
		// Token: 0x060020F6 RID: 8438 RVA: 0x000BDBF1 File Offset: 0x000BBDF1
		internal EditMessageToolbar(Importance importance, Markup currentMarkup, bool isSMimeControlMustUpdate, bool isSMimeEditForm, bool isIrmProtected, bool isNotOwner) : this(importance, currentMarkup)
		{
			this.isSMimeControlMustUpdate = isSMimeControlMustUpdate;
			this.isSMimeEditForm = isSMimeEditForm;
			this.isIrmProtected = isIrmProtected;
			this.isNotOwner = isNotOwner;
		}

		// Token: 0x060020F7 RID: 8439 RVA: 0x000BDC1A File Offset: 0x000BBE1A
		internal EditMessageToolbar(Importance importance)
		{
			this.importance = Importance.Normal;
			this.isComplianceButtonAllowedInForm = true;
			this.isComplianceButtonEnabledInForm = true;
			this.isSendButtonEnabledInForm = true;
			base..ctor(ToolbarType.Form);
			this.importance = importance;
		}

		// Token: 0x060020F8 RID: 8440 RVA: 0x000BDC46 File Offset: 0x000BBE46
		internal EditMessageToolbar(Importance importance, Markup currentMarkup)
		{
			this.importance = Importance.Normal;
			this.isComplianceButtonAllowedInForm = true;
			this.isComplianceButtonEnabledInForm = true;
			this.isSendButtonEnabledInForm = true;
			base..ctor(ToolbarType.Form);
			this.importance = importance;
			this.initialMarkup = currentMarkup;
		}

		// Token: 0x17000899 RID: 2201
		// (get) Token: 0x060020F9 RID: 8441 RVA: 0x000BDC79 File Offset: 0x000BBE79
		// (set) Token: 0x060020FA RID: 8442 RVA: 0x000BDC81 File Offset: 0x000BBE81
		public bool IsComplianceButtonAllowedInForm
		{
			get
			{
				return this.isComplianceButtonAllowedInForm;
			}
			set
			{
				this.isComplianceButtonAllowedInForm = value;
			}
		}

		// Token: 0x1700089A RID: 2202
		// (get) Token: 0x060020FB RID: 8443 RVA: 0x000BDC8A File Offset: 0x000BBE8A
		// (set) Token: 0x060020FC RID: 8444 RVA: 0x000BDC92 File Offset: 0x000BBE92
		public bool IsComplianceButtonEnabledInForm
		{
			get
			{
				return this.isComplianceButtonEnabledInForm;
			}
			set
			{
				this.isComplianceButtonEnabledInForm = value;
			}
		}

		// Token: 0x1700089B RID: 2203
		// (get) Token: 0x060020FD RID: 8445 RVA: 0x000BDC9B File Offset: 0x000BBE9B
		protected override bool IsNarrow
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700089C RID: 2204
		// (get) Token: 0x060020FE RID: 8446 RVA: 0x000BDC9E File Offset: 0x000BBE9E
		// (set) Token: 0x060020FF RID: 8447 RVA: 0x000BDCA6 File Offset: 0x000BBEA6
		public bool IsSendButtonEnabledInForm
		{
			get
			{
				return this.isSendButtonEnabledInForm;
			}
			set
			{
				this.isSendButtonEnabledInForm = value;
			}
		}

		// Token: 0x06002100 RID: 8448 RVA: 0x000BDCB0 File Offset: 0x000BBEB0
		protected override void RenderButtons()
		{
			bool flag = base.UserContext.BrowserType == BrowserType.IE;
			ToolbarButtonFlags flags = this.isSendButtonEnabledInForm ? ToolbarButtonFlags.None : ToolbarButtonFlags.Disabled;
			base.RenderHelpButton(this.HelpId, string.Empty);
			base.RenderButton(ToolbarButtons.Send, flags);
			base.RenderButton(ToolbarButtons.SaveImageOnly);
			base.RenderButton(ToolbarButtons.AttachFile);
			if (!flag || !this.isSMimeEditForm)
			{
				base.RenderButton(ToolbarButtons.InsertImage);
			}
			base.RenderButton(ToolbarButtons.AddressBook);
			base.RenderButton(ToolbarButtons.CheckNames);
			base.RenderButton(ToolbarButtons.ImportanceHigh, (this.importance == Importance.High) ? ToolbarButtonFlags.Pressed : ToolbarButtonFlags.None);
			base.RenderButton(ToolbarButtons.ImportanceLow, (this.importance == Importance.Low) ? ToolbarButtonFlags.Pressed : ToolbarButtonFlags.None);
			if (flag && (this.isSMimeControlMustUpdate || this.isSMimeEditForm))
			{
				ToolbarButtonFlags flags2 = this.isSMimeControlMustUpdate ? ToolbarButtonFlags.Disabled : ToolbarButtonFlags.None;
				base.RenderButton(ToolbarButtons.MessageDigitalSignature, flags2);
				base.RenderButton(ToolbarButtons.MessageEncryptContents, flags2);
			}
			if (base.UserContext.IsFeatureEnabled(Feature.Signature))
			{
				base.RenderButton(ToolbarButtons.InsertSignature);
			}
			if (flag)
			{
				ToolbarButtonFlags flags3 = base.UserContext.IsFeatureEnabled(Feature.SpellChecker) ? ToolbarButtonFlags.None : ToolbarButtonFlags.Disabled;
				base.RenderButton(ToolbarButtons.SpellCheck, flags3, new Toolbar.RenderMenuItems(base.RenderSpellCheckLanguageDialog));
			}
			if (this.IsComplianceButtonAllowedInForm && base.UserContext.ComplianceReader.IsComplianceFeatureEnabled(base.UserContext.IsIrmEnabled, this.isIrmProtected, CultureInfo.CurrentUICulture))
			{
				if (this.isComplianceButtonEnabledInForm)
				{
					base.RenderButton(ToolbarButtons.Compliance);
				}
				else
				{
					base.RenderButton(ToolbarButtons.Compliance, ToolbarButtonFlags.Disabled);
				}
			}
			base.RenderButton(ToolbarButtons.MessageOptions);
			base.RenderHtmlTextToggle((this.initialMarkup == Markup.Html) ? "0" : "1", this.isSMimeEditForm || this.isNotOwner);
			base.RenderButton(ToolbarButtons.MailTips);
		}

		// Token: 0x1700089D RID: 2205
		// (get) Token: 0x06002101 RID: 8449 RVA: 0x000BDE94 File Offset: 0x000BC094
		protected virtual string HelpId
		{
			get
			{
				return HelpIdsLight.MailLight.ToString();
			}
		}

		// Token: 0x0400178E RID: 6030
		private Importance importance;

		// Token: 0x0400178F RID: 6031
		private Markup initialMarkup;

		// Token: 0x04001790 RID: 6032
		private bool isComplianceButtonAllowedInForm;

		// Token: 0x04001791 RID: 6033
		private bool isComplianceButtonEnabledInForm;

		// Token: 0x04001792 RID: 6034
		private bool isSMimeControlMustUpdate;

		// Token: 0x04001793 RID: 6035
		private bool isSMimeEditForm;

		// Token: 0x04001794 RID: 6036
		private bool isIrmProtected;

		// Token: 0x04001795 RID: 6037
		private bool isNotOwner;

		// Token: 0x04001796 RID: 6038
		private bool isSendButtonEnabledInForm;
	}
}
