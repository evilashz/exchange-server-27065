using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Management.SystemConfigurationTasks;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x020005EE RID: 1518
	[Cmdlet("Test", "WebServicesConnectivity", SupportsShouldProcess = true, DefaultParameterSetName = "ClientAccessServerParameterSet")]
	public sealed class TestWebServicesConnectivity : TestWebServicesTaskBase
	{
		// Token: 0x1700101F RID: 4127
		// (get) Token: 0x06003628 RID: 13864 RVA: 0x000DF10F File Offset: 0x000DD30F
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageTestWebServicesConnectivity;
			}
		}

		// Token: 0x17001020 RID: 4128
		// (get) Token: 0x06003629 RID: 13865 RVA: 0x000DF116 File Offset: 0x000DD316
		// (set) Token: 0x0600362A RID: 13866 RVA: 0x000DF13C File Offset: 0x000DD33C
		[Parameter(Mandatory = false)]
		public SwitchParameter LightMode
		{
			get
			{
				return (bool)(base.Fields["LightMode"] ?? false);
			}
			set
			{
				base.Fields["LightMode"] = value.IsPresent;
			}
		}

		// Token: 0x17001021 RID: 4129
		// (get) Token: 0x0600362B RID: 13867 RVA: 0x000DF15A File Offset: 0x000DD35A
		protected override string CmdletName
		{
			get
			{
				return "Test-WebServicesConnectivity";
			}
		}

		// Token: 0x17001022 RID: 4130
		// (get) Token: 0x0600362C RID: 13868 RVA: 0x000DF161 File Offset: 0x000DD361
		protected override bool IsOutlookProvider
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600362D RID: 13869 RVA: 0x000DF164 File Offset: 0x000DD364
		protected override void InternalProcessRecord()
		{
			string text;
			string text2;
			bool flag = base.ValidateAutoDiscover(out text, out text2);
			string text3 = null;
			WebServicesTestOutcome.TestScenario scenario = this.LightMode.IsPresent ? WebServicesTestOutcome.TestScenario.EwsConvertId : WebServicesTestOutcome.TestScenario.EwsGetFolder;
			if (base.IsFromAutoDiscover)
			{
				if (string.IsNullOrEmpty(text))
				{
					base.OutputSkippedOutcome(scenario, Strings.InformationSkippedEws);
				}
				else
				{
					text3 = text;
					base.WriteVerbose(Strings.VerboseTestEwsFromAutoDiscover(text3));
				}
			}
			else
			{
				text3 = base.GetSpecifiedEwsUrl();
				base.WriteVerbose(Strings.VerboseTestEwsFromParameter(text3));
			}
			if (!string.IsNullOrEmpty(text3))
			{
				EwsValidator ewsValidator = new EwsValidator(text3, base.TestAccount.Credential, base.TestAccount.User.PrimarySmtpAddress.ToString())
				{
					VerboseDelegate = new Task.TaskVerboseLoggingDelegate(base.WriteVerbose),
					UserAgent = base.UserAgentString,
					IgnoreSslCertError = (base.TrustAnySSLCertificate || base.MonitoringContext.IsPresent),
					Operation = (this.LightMode.IsPresent ? EwsValidator.RequestOperation.ConvertId : EwsValidator.RequestOperation.GetFolder),
					PublicFolderMailboxGuid = ((base.TestAccount.User.RecipientTypeDetails == RecipientTypeDetails.PublicFolderMailbox) ? base.TestAccount.User.ExchangeGuid : Guid.Empty)
				};
				flag = ewsValidator.Invoke();
				WebServicesTestOutcome outcome = new WebServicesTestOutcome
				{
					Scenario = scenario,
					Source = base.LocalServer.Fqdn,
					Result = (flag ? CasTransactionResultEnum.Success : CasTransactionResultEnum.Failure),
					Error = string.Format("{0}", ewsValidator.Error),
					ServiceEndpoint = TestWebServicesTaskBase.FqdnFromUrl(text3),
					Latency = ewsValidator.Latency,
					ScenarioDescription = TestWebServicesTaskBase.GetScenarioDescription(scenario),
					Verbose = ewsValidator.Verbose
				};
				base.Output(outcome);
			}
			base.InternalProcessRecord();
		}
	}
}
