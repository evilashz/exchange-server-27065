using System;
using System.Management.Automation;
using System.Security.Permissions;
using System.ServiceModel.Activation;
using Microsoft.Exchange.Management.ControlPanel.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020002B7 RID: 695
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public class ImapSubscriptions : DataSourceService, IImapSubscriptions, INewObjectService<PimSubscriptionRow, NewImapSubscription>, IEditObjectService<ImapSubscription, SetImapSubscription>, IGetObjectService<ImapSubscription>
	{
		// Token: 0x06002C00 RID: 11264 RVA: 0x000887FC File Offset: 0x000869FC
		[PrincipalPermission(SecurityAction.Demand, Role = "MultiTenant+Get-ImapSubscription?Mailbox&Identity@R:Self")]
		public PowerShellResults<ImapSubscription> GetObject(Identity identity)
		{
			PSCommand psCommand = new PSCommand().AddCommand("Get-ImapSubscription");
			psCommand.AddParameters(new GetImapSubscription());
			return base.GetObject<ImapSubscription>(psCommand, identity);
		}

		// Token: 0x06002C01 RID: 11265 RVA: 0x00088830 File Offset: 0x00086A30
		[PrincipalPermission(SecurityAction.Demand, Role = "MultiTenant+New-ImapSubscription?Mailbox&Name@W:Self")]
		public PowerShellResults<PimSubscriptionRow> NewObject(NewImapSubscription properties)
		{
			PowerShellResults<PimSubscriptionRow> powerShellResults = base.NewObject<PimSubscriptionRow, NewImapSubscription>("New-ImapSubscription", properties);
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

		// Token: 0x06002C02 RID: 11266 RVA: 0x00088884 File Offset: 0x00086A84
		[PrincipalPermission(SecurityAction.Demand, Role = "MultiTenant+Get-ImapSubscription?Mailbox&Identity@R:Self+Set-ImapSubscription?Mailbox&Identity@W:Self")]
		public PowerShellResults<ImapSubscription> SetObject(Identity identity, SetImapSubscription properties)
		{
			PowerShellResults<ImapSubscription> powerShellResults = base.SetObject<ImapSubscription, SetImapSubscription>("Set-ImapSubscription", identity, properties);
			if (powerShellResults.Succeeded)
			{
				PowerShellResults<ImapSubscription> @object = this.GetObject(identity);
				if (@object.Succeeded)
				{
					string verificationFeedbackString = @object.Output[0].VerificationFeedbackString;
					if (verificationFeedbackString != null)
					{
						string text = OwaOptionStrings.NewSubscriptionSucceed(verificationFeedbackString);
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

		// Token: 0x040021C1 RID: 8641
		internal const string GetCmdlet = "Get-ImapSubscription";

		// Token: 0x040021C2 RID: 8642
		internal const string NewCmdlet = "New-ImapSubscription";

		// Token: 0x040021C3 RID: 8643
		internal const string SetCmdlet = "Set-ImapSubscription";

		// Token: 0x040021C4 RID: 8644
		internal const string ReadScope = "@R:Self";

		// Token: 0x040021C5 RID: 8645
		internal const string WriteScope = "@W:Self";

		// Token: 0x040021C6 RID: 8646
		private const string Noun = "ImapSubscription";

		// Token: 0x040021C7 RID: 8647
		private const string GetObjectRole = "MultiTenant+Get-ImapSubscription?Mailbox&Identity@R:Self";

		// Token: 0x040021C8 RID: 8648
		private const string NewObjectRole = "MultiTenant+New-ImapSubscription?Mailbox&Name@W:Self";

		// Token: 0x040021C9 RID: 8649
		private const string SetObjectRole = "MultiTenant+Get-ImapSubscription?Mailbox&Identity@R:Self+Set-ImapSubscription?Mailbox&Identity@W:Self";
	}
}
