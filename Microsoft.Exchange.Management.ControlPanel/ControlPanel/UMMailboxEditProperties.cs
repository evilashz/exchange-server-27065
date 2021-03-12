using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using Microsoft.Exchange.Management.ControlPanel.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020004C0 RID: 1216
	[ClientScriptResource("UMMailboxEditProperties", "Microsoft.Exchange.Management.ControlPanel.Client.UnifiedMessaging.js")]
	public sealed class UMMailboxEditProperties : Properties
	{
		// Token: 0x06003BD2 RID: 15314 RVA: 0x000B47D4 File Offset: 0x000B29D4
		protected override ScriptControlDescriptor GetScriptDescriptor()
		{
			ScriptControlDescriptor scriptDescriptor = base.GetScriptDescriptor();
			scriptDescriptor.Type = "UMMailboxEditProperties";
			Section section = base.Sections["UMMailboxConfigurationSection"];
			scriptDescriptor.AddElementProperty("LockedOutStatusLabel", section.FindControl("txtLockedOutStatus").ClientID);
			scriptDescriptor.AddElementProperty("LockedOutStatusActionLabel", section.FindControl("lblLockedOutStatusAction").ClientID);
			return scriptDescriptor;
		}

		// Token: 0x06003BD3 RID: 15315 RVA: 0x000B483C File Offset: 0x000B2A3C
		protected override void OnPreRender(EventArgs e)
		{
			this.SetResetPinUrl();
			base.OnPreRender(e);
			PowerShellResults<SetUMMailboxConfiguration> powerShellResults = (PowerShellResults<SetUMMailboxConfiguration>)base.Results;
			if (powerShellResults != null && powerShellResults.SucceededWithValue)
			{
				this.SetDynamicLabels(powerShellResults);
				this.SetAddExtensionUrl(powerShellResults);
			}
		}

		// Token: 0x06003BD4 RID: 15316 RVA: 0x000B487C File Offset: 0x000B2A7C
		private void SetResetPinUrl()
		{
			Section section = base.Sections["UMMailboxConfigurationSection"];
			PopupLauncher popupLauncher = (PopupLauncher)section.FindControl("popupLauncherDataCenter");
			popupLauncher.NavigationUrl = EcpUrl.AppendQueryParameter(popupLauncher.NavigationUrl, "id", base.ObjectIdentity.RawIdentity);
		}

		// Token: 0x06003BD5 RID: 15317 RVA: 0x000B48CC File Offset: 0x000B2ACC
		private void SetAddExtensionUrl(PowerShellResults<SetUMMailboxConfiguration> result)
		{
			Section section = base.Sections["UMMailboxExtensionSection"];
			EcpCollectionEditor ecpCollectionEditor = (EcpCollectionEditor)section.FindControl("ceExtensions");
			ecpCollectionEditor.PickerFormUrl = EcpUrl.AppendQueryParameter(ecpCollectionEditor.PickerFormUrl, "dialPlanId", result.Value.DialPlanId);
		}

		// Token: 0x06003BD6 RID: 15318 RVA: 0x000B491C File Offset: 0x000B2B1C
		private void SetDynamicLabels(PowerShellResults<SetUMMailboxConfiguration> result)
		{
			Section section = base.Sections["UMMailboxConfigurationSection"];
			Label label = (Label)section.FindControl("txtExtension_label");
			Label label2 = (Label)section.FindControl("lblLockedOutStatusAction");
			if (result.Output[0].IsSipDialPlan)
			{
				label.Text = Strings.UMMailboxSipLabel;
			}
			else if (result.Output[0].IsE164DialPlan)
			{
				label.Text = Strings.UMMailboxE164Label;
			}
			else
			{
				label.Text = Strings.UMMailboxExtensionLabel;
			}
			if (result.Output[0].AccountLockedOut)
			{
				label2.Style["display"] = "inline";
				return;
			}
			label2.Style["display"] = "none";
		}
	}
}
