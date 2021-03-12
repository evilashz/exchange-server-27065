using System;
using System.Management.Automation;
using System.Security.Permissions;
using System.ServiceModel;
using System.ServiceModel.Activation;
using Microsoft.Exchange.Management.ControlPanel.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020002A2 RID: 674
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public class EmailSubscriptions : DataSourceService, IEmailSubscriptions, INewObjectService<PimSubscription, NewSubscription>, IGetListService<EmailSubscriptionFilter, PimSubscriptionRow>, IRemoveObjectsService, IRemoveObjectsService<BaseWebServiceParameters>
	{
		// Token: 0x06002B81 RID: 11137 RVA: 0x00087CC8 File Offset: 0x00085EC8
		[PrincipalPermission(SecurityAction.Demand, Role = "MultiTenant+Get-Subscription?Mailbox@R:Self")]
		public PowerShellResults<PimSubscriptionRow> GetList(EmailSubscriptionFilter filter, SortOptions sort)
		{
			return base.GetList<PimSubscriptionRow, EmailSubscriptionFilter>("Get-Subscription", filter, sort, "EmailAddress");
		}

		// Token: 0x06002B82 RID: 11138 RVA: 0x00087CDC File Offset: 0x00085EDC
		[PrincipalPermission(SecurityAction.Demand, Role = "MultiTenant+New-Subscription?Mailbox&Force&DisplayName&Name@W:Self")]
		public PowerShellResults<PimSubscription> NewObject(NewSubscription properties)
		{
			PowerShellResults<PimSubscription> powerShellResults = base.NewObject<PimSubscription, NewSubscription>("New-Subscription", properties);
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

		// Token: 0x06002B83 RID: 11139 RVA: 0x00087D30 File Offset: 0x00085F30
		[PrincipalPermission(SecurityAction.Demand, Role = "MultiTenant+Remove-Subscription?Mailbox&Identity@W:Self")]
		public PowerShellResults RemoveObjects(Identity[] identities, BaseWebServiceParameters parameters)
		{
			PSCommand psCommand = new PSCommand().AddCommand("Remove-Subscription");
			psCommand.AddParameters(new RemoveSubscription());
			PowerShellResults powerShellResults = base.RemoveObjects(psCommand, identities, parameters);
			if (powerShellResults != null && powerShellResults.Succeeded)
			{
				Util.NotifyOWAUserSettingsChanged(UserSettings.Mail);
			}
			return powerShellResults;
		}

		// Token: 0x06002B84 RID: 11140 RVA: 0x00087D78 File Offset: 0x00085F78
		[PrincipalPermission(SecurityAction.Demand, Role = "MultiTenant+Get-PopSubscription?Mailbox&Identity@R:Self+Set-PopSubscription?Identity&ResendVerification@W:Self")]
		public PowerShellResults<PimSubscriptionRow> ResendPopVerificationEmail(Identity[] identities, BaseWebServiceParameters parameters)
		{
			identities.FaultIfNotExactlyOne();
			SetPopSubscription setPopSubscription = new SetPopSubscription();
			setPopSubscription.ResendVerification = true;
			PowerShellResults<PimSubscriptionRow> powerShellResults = base.SetObject<PopSubscription, SetPopSubscription, PimSubscriptionRow>("Set-PopSubscription", identities[0], setPopSubscription);
			if (powerShellResults.Succeeded)
			{
				PopSubscriptions popSubscriptions = new PopSubscriptions();
				PowerShellResults<PopSubscription> @object = popSubscriptions.GetObject(identities[0]);
				if (!@object.SucceededWithValue)
				{
					throw new FaultException(OwaOptionStrings.SubscriptionProcessingError);
				}
				string verificationFeedbackString = @object.Output[0].VerificationFeedbackString;
				if (verificationFeedbackString != null)
				{
					powerShellResults.Informations = new string[]
					{
						verificationFeedbackString
					};
				}
			}
			return powerShellResults;
		}

		// Token: 0x06002B85 RID: 11141 RVA: 0x00087E04 File Offset: 0x00086004
		[PrincipalPermission(SecurityAction.Demand, Role = "MultiTenant+Get-ImapSubscription?Mailbox&Identity@R:Self+Set-ImapSubscription?Identity&ResendVerification@W:Self")]
		public PowerShellResults<PimSubscriptionRow> ResendImapVerificationEmail(Identity[] identities, BaseWebServiceParameters parameters)
		{
			identities.FaultIfNotExactlyOne();
			SetImapSubscription setImapSubscription = new SetImapSubscription();
			setImapSubscription.ResendVerification = true;
			PowerShellResults<PimSubscriptionRow> powerShellResults = base.SetObject<ImapSubscription, SetImapSubscription, PimSubscriptionRow>("Set-ImapSubscription", identities[0], setImapSubscription);
			if (powerShellResults.Succeeded)
			{
				ImapSubscriptions imapSubscriptions = new ImapSubscriptions();
				PowerShellResults<ImapSubscription> @object = imapSubscriptions.GetObject(identities[0]);
				if (!@object.SucceededWithValue)
				{
					throw new FaultException(OwaOptionStrings.SubscriptionProcessingError);
				}
				string verificationFeedbackString = @object.Output[0].VerificationFeedbackString;
				if (verificationFeedbackString != null)
				{
					powerShellResults.Informations = new string[]
					{
						verificationFeedbackString
					};
				}
			}
			return powerShellResults;
		}

		// Token: 0x0400219F RID: 8607
		private const string Noun = "Subscription";

		// Token: 0x040021A0 RID: 8608
		internal const string GetCmdlet = "Get-Subscription";

		// Token: 0x040021A1 RID: 8609
		internal const string GetImapCmdlet = "Get-ImapSubscription";

		// Token: 0x040021A2 RID: 8610
		internal const string GetPopCmdlet = "Get-PopSubscription";

		// Token: 0x040021A3 RID: 8611
		internal const string NewCmdlet = "New-Subscription";

		// Token: 0x040021A4 RID: 8612
		internal const string RemoveCmdlet = "Remove-Subscription";

		// Token: 0x040021A5 RID: 8613
		internal const string ReadScope = "@R:Self";

		// Token: 0x040021A6 RID: 8614
		internal const string WriteScope = "@W:Self";

		// Token: 0x040021A7 RID: 8615
		private const string GetListRole = "MultiTenant+Get-Subscription?Mailbox@R:Self";

		// Token: 0x040021A8 RID: 8616
		private const string NewObjectRole = "MultiTenant+New-Subscription?Mailbox&Force&DisplayName&Name@W:Self";

		// Token: 0x040021A9 RID: 8617
		private const string RemoveObjectsRole = "MultiTenant+Remove-Subscription?Mailbox&Identity@W:Self";

		// Token: 0x040021AA RID: 8618
		private const string ResendPopVerificationRole = "MultiTenant+Get-PopSubscription?Mailbox&Identity@R:Self+Set-PopSubscription?Identity&ResendVerification@W:Self";

		// Token: 0x040021AB RID: 8619
		private const string ResendImapVerificationRole = "MultiTenant+Get-ImapSubscription?Mailbox&Identity@R:Self+Set-ImapSubscription?Identity&ResendVerification@W:Self";
	}
}
