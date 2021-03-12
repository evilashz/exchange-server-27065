using System;
using System.Management.Automation;
using System.Security.Permissions;
using System.ServiceModel.Activation;
using Microsoft.Exchange.Management.ControlPanel.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020002D9 RID: 729
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public class PopSubscriptions : DataSourceService, IPopSubscriptions, INewObjectService<PimSubscriptionRow, NewPopSubscription>, IEditObjectService<PopSubscription, SetPopSubscription>, IGetObjectService<PopSubscription>
	{
		// Token: 0x06002CC3 RID: 11459 RVA: 0x00089A50 File Offset: 0x00087C50
		[PrincipalPermission(SecurityAction.Demand, Role = "MultiTenant+Get-PopSubscription?Mailbox&Identity@R:Self")]
		public PowerShellResults<PopSubscription> GetObject(Identity identity)
		{
			PSCommand psCommand = new PSCommand().AddCommand("Get-PopSubscription");
			psCommand.AddParameters(new GetPopSubscription());
			return base.GetObject<PopSubscription>(psCommand, identity);
		}

		// Token: 0x06002CC4 RID: 11460 RVA: 0x00089A84 File Offset: 0x00087C84
		[PrincipalPermission(SecurityAction.Demand, Role = "MultiTenant+New-PopSubscription?Mailbox&Name@W:Self")]
		public PowerShellResults<PimSubscriptionRow> NewObject(NewPopSubscription properties)
		{
			PowerShellResults<PimSubscriptionRow> powerShellResults = base.NewObject<PimSubscriptionRow, NewPopSubscription>("New-PopSubscription", properties);
			if (powerShellResults.Succeeded)
			{
				string text = OwaOptionStrings.NewSubscriptionSucceed(powerShellResults.Output[0].VerificationFeedbackString);
				powerShellResults.Informations = new string[]
				{
					text
				};
				Util.NotifyOWAUserSettingsChanged(UserSettings.Mail);
			}
			return powerShellResults;
		}

		// Token: 0x06002CC5 RID: 11461 RVA: 0x00089AD8 File Offset: 0x00087CD8
		[PrincipalPermission(SecurityAction.Demand, Role = "MultiTenant+Get-PopSubscription?Mailbox&Identity@R:Self+Set-PopSubscription?Mailbox&Identity@W:Self")]
		public PowerShellResults<PopSubscription> SetObject(Identity identity, SetPopSubscription properties)
		{
			PowerShellResults<PopSubscription> powerShellResults = base.SetObject<PopSubscription, SetPopSubscription>("Set-PopSubscription", identity, properties);
			if (powerShellResults.Succeeded)
			{
				PowerShellResults<PopSubscription> @object = this.GetObject(identity);
				if (@object.Succeeded)
				{
					string verificationFeedbackString = @object.Output[0].VerificationFeedbackString;
					if (verificationFeedbackString != null)
					{
						string text = OwaOptionStrings.SetSubscriptionSucceed(verificationFeedbackString);
						powerShellResults.Informations = new string[]
						{
							text
						};
					}
				}
				Util.NotifyOWAUserSettingsChanged(UserSettings.Mail);
			}
			return powerShellResults;
		}

		// Token: 0x04002205 RID: 8709
		internal const string GetCmdlet = "Get-PopSubscription";

		// Token: 0x04002206 RID: 8710
		internal const string NewCmdlet = "New-PopSubscription";

		// Token: 0x04002207 RID: 8711
		internal const string SetCmdlet = "Set-PopSubscription";

		// Token: 0x04002208 RID: 8712
		internal const string ReadScope = "@R:Self";

		// Token: 0x04002209 RID: 8713
		internal const string WriteScope = "@W:Self";

		// Token: 0x0400220A RID: 8714
		private const string Noun = "PopSubscription";

		// Token: 0x0400220B RID: 8715
		private const string GetObjectRole = "MultiTenant+Get-PopSubscription?Mailbox&Identity@R:Self";

		// Token: 0x0400220C RID: 8716
		private const string NewObjectRole = "MultiTenant+New-PopSubscription?Mailbox&Name@W:Self";

		// Token: 0x0400220D RID: 8717
		private const string SetObjectRole = "MultiTenant+Get-PopSubscription?Mailbox&Identity@R:Self+Set-PopSubscription?Mailbox&Identity@W:Self";
	}
}
