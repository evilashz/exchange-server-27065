using System;
using System.Collections.Generic;
using System.Web.UI;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000529 RID: 1321
	[ClientScriptResource("NewMailboxProperties", "Microsoft.Exchange.Management.ControlPanel.Client.Mailbox.js")]
	public sealed class NewMailboxProperties : Properties, IScriptControl
	{
		// Token: 0x06003EE9 RID: 16105 RVA: 0x000BD6D4 File Offset: 0x000BB8D4
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			if (base.ObjectIdentity != null)
			{
				BaseForm baseForm = this.Page as BaseForm;
				if (baseForm != null)
				{
					baseForm.Title = (baseForm.Caption = Strings.RecoverMailboxCaption);
					baseForm.CommitButtonText = Strings.RecoverButtonText;
					baseForm.HelpId = EACHelpId.RecoverMailbox.ToString();
				}
			}
		}

		// Token: 0x06003EEA RID: 16106 RVA: 0x000BD740 File Offset: 0x000BB940
		protected override ScriptControlDescriptor GetScriptDescriptor()
		{
			ScriptControlDescriptor scriptDescriptor = base.GetScriptDescriptor();
			Control control = this.FindControl("SoftDeletedDataSection");
			if (control != null)
			{
				scriptDescriptor.AddElementProperty("SoftDeletedDataSection", control.ClientID, true);
			}
			return scriptDescriptor;
		}

		// Token: 0x06003EEB RID: 16107 RVA: 0x000BD778 File Offset: 0x000BB978
		IEnumerable<ScriptDescriptor> IScriptControl.GetScriptDescriptors()
		{
			ScriptControlDescriptor scriptDescriptor = this.GetScriptDescriptor();
			scriptDescriptor.Type = "NewMailboxProperties";
			return new ScriptControlDescriptor[]
			{
				scriptDescriptor
			};
		}
	}
}
