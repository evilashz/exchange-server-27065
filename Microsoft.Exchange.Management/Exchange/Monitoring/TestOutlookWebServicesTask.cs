using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.SystemConfigurationTasks;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x020007FE RID: 2046
	[Cmdlet("Test", "OutlookWebServices", SupportsShouldProcess = true, DefaultParameterSetName = "ClientAccessServerParameterSet")]
	public sealed class TestOutlookWebServicesTask : TestWebServicesTaskBase
	{
		// Token: 0x1700159A RID: 5530
		// (get) Token: 0x0600474F RID: 18255 RVA: 0x00124C16 File Offset: 0x00122E16
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageTestOutlookWebServices;
			}
		}

		// Token: 0x1700159B RID: 5531
		// (get) Token: 0x06004750 RID: 18256 RVA: 0x00124C1D File Offset: 0x00122E1D
		protected override string CmdletName
		{
			get
			{
				return "Test-OutlookWebServices";
			}
		}

		// Token: 0x1700159C RID: 5532
		// (get) Token: 0x06004751 RID: 18257 RVA: 0x00124C24 File Offset: 0x00122E24
		protected override bool IsOutlookProvider
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004752 RID: 18258 RVA: 0x00124C28 File Offset: 0x00122E28
		protected override void InternalProcessRecord()
		{
			string text = null;
			string text2 = null;
			bool flag = base.ValidateAutoDiscover(out text, out text2);
			if (!base.IsFromAutoDiscover)
			{
				text = base.GetSpecifiedEwsUrl();
				text2 = this.GetSpecifiedOabUrl();
			}
			if (string.IsNullOrEmpty(text))
			{
				base.OutputSkippedOutcome(WebServicesTestOutcome.TestScenario.ExchangeWebServices, Strings.InformationSkippedEws);
				base.OutputSkippedOutcome(WebServicesTestOutcome.TestScenario.AvailabilityService, Strings.InformationSkippedAsForAutodiscover);
			}
			else
			{
				if (base.IsFromAutoDiscover)
				{
					base.WriteVerbose(Strings.VerboseTestEwsFromAutoDiscover(text));
				}
				else
				{
					base.WriteVerbose(Strings.VerboseTestEwsFromParameter(text));
				}
				EwsValidator ewsValidator = new EwsValidator(text, base.TestAccount.Credential)
				{
					VerboseDelegate = new Task.TaskVerboseLoggingDelegate(base.WriteVerbose),
					UserAgent = base.UserAgentString,
					IgnoreSslCertError = (base.TrustAnySSLCertificate || base.MonitoringContext.IsPresent)
				};
				flag = ewsValidator.Invoke();
				WebServicesTestOutcome outcome = new WebServicesTestOutcome
				{
					Scenario = WebServicesTestOutcome.TestScenario.ExchangeWebServices,
					Source = base.LocalServer.Fqdn,
					Result = (flag ? CasTransactionResultEnum.Success : CasTransactionResultEnum.Failure),
					Error = string.Format("{0}", ewsValidator.Error),
					ServiceEndpoint = TestWebServicesTaskBase.FqdnFromUrl(text),
					Latency = ewsValidator.Latency,
					ScenarioDescription = TestWebServicesTaskBase.GetScenarioDescription(WebServicesTestOutcome.TestScenario.ExchangeWebServices),
					Verbose = ewsValidator.Verbose
				};
				base.Output(outcome);
				if (flag)
				{
					EwsValidator ewsValidator2 = new EwsValidator(text, base.TestAccount.Credential)
					{
						VerboseDelegate = new Task.TaskVerboseLoggingDelegate(base.WriteVerbose),
						UserAgent = base.UserAgentString,
						IgnoreSslCertError = (base.TrustAnySSLCertificate || base.MonitoringContext.IsPresent),
						EmailAddress = base.TestAccount.User.PrimarySmtpAddress.ToString(),
						Operation = EwsValidator.RequestOperation.GetUserAvailability
					};
					flag = ewsValidator2.Invoke();
					outcome = new WebServicesTestOutcome
					{
						Scenario = WebServicesTestOutcome.TestScenario.AvailabilityService,
						Source = base.LocalServer.Fqdn,
						Result = (flag ? CasTransactionResultEnum.Success : CasTransactionResultEnum.Failure),
						Error = string.Format("{0}", ewsValidator2.Error),
						ServiceEndpoint = TestWebServicesTaskBase.FqdnFromUrl(text),
						Latency = ewsValidator2.Latency,
						ScenarioDescription = TestWebServicesTaskBase.GetScenarioDescription(WebServicesTestOutcome.TestScenario.AvailabilityService),
						Verbose = ewsValidator2.Verbose
					};
					base.Output(outcome);
				}
				else
				{
					base.OutputSkippedOutcome(WebServicesTestOutcome.TestScenario.AvailabilityService, Strings.InformationSkippedAsForEWS);
				}
			}
			if (string.IsNullOrEmpty(text2))
			{
				base.OutputSkippedOutcome(WebServicesTestOutcome.TestScenario.OfflineAddressBook, Strings.InformationSkippedOab);
			}
			else
			{
				if (base.IsFromAutoDiscover)
				{
					base.WriteVerbose(Strings.VerboseTestOabFromAutoDiscover(text2));
				}
				else
				{
					base.WriteVerbose(Strings.VerboseTestOabFromParameter(text2));
				}
				OabValidator oabValidator = new OabValidator(text2, base.TestAccount.Credential)
				{
					VerboseDelegate = new Task.TaskVerboseLoggingDelegate(base.WriteVerbose),
					UserAgent = base.UserAgentString,
					IgnoreSslCertError = (base.TrustAnySSLCertificate || base.MonitoringContext.IsPresent)
				};
				flag = oabValidator.Invoke();
				WebServicesTestOutcome outcome2 = new WebServicesTestOutcome
				{
					Scenario = WebServicesTestOutcome.TestScenario.OfflineAddressBook,
					Source = base.LocalServer.Fqdn,
					Result = (flag ? CasTransactionResultEnum.Success : CasTransactionResultEnum.Failure),
					Error = string.Format("{0}", oabValidator.Error),
					ServiceEndpoint = TestWebServicesTaskBase.FqdnFromUrl(text2),
					Latency = oabValidator.Latency,
					ScenarioDescription = TestWebServicesTaskBase.GetScenarioDescription(WebServicesTestOutcome.TestScenario.OfflineAddressBook),
					Verbose = oabValidator.Verbose
				};
				base.Output(outcome2);
			}
			base.InternalProcessRecord();
		}

		// Token: 0x06004753 RID: 18259 RVA: 0x00125024 File Offset: 0x00123224
		private string GetSpecifiedOabUrl()
		{
			string oabObjectId = this.GetOabObjectId(base.TestAccount.User);
			if (base.MonitoringContext.IsPresent)
			{
				return TestOutlookWebServicesTask.FormatOabUrl(base.LocalServer.Fqdn, oabObjectId);
			}
			if (base.ClientAccessServerFqdn != null)
			{
				return TestOutlookWebServicesTask.FormatOabUrl(base.ClientAccessServerFqdn, oabObjectId);
			}
			return null;
		}

		// Token: 0x06004754 RID: 18260 RVA: 0x00125080 File Offset: 0x00123280
		private string GetOabObjectId(ADUser user)
		{
			if (user.OfflineAddressBook != null)
			{
				return user.OfflineAddressBook.ObjectGuid.ToString();
			}
			MailboxDatabase mailboxDatabase = base.ConfigSession.Read<MailboxDatabase>(user.Database);
			if (mailboxDatabase == null)
			{
				base.WriteError(new TestWebServicesTaskException(Strings.ErrorOabNotFoundForUser(user.Identity.ToString())), ErrorCategory.InvalidData, null);
			}
			else if (mailboxDatabase.OfflineAddressBook != null)
			{
				return mailboxDatabase.OfflineAddressBook.ObjectGuid.ToString();
			}
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(true, ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromAllTenantsOrRootOrgAutoDetect(user.Id), 258, "GetOabObjectId", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\SystemConfigurationTasks\\AutoDiscover\\TestOutlookWebServicesTask.cs");
			OfflineAddressBook[] array = tenantOrTopologyConfigurationSession.Find<OfflineAddressBook>(null, QueryScope.SubTree, new ComparisonFilter(ComparisonOperator.Equal, OfflineAddressBookSchema.IsDefault, true), null, 1);
			if (array.Length > 0)
			{
				return array[0].Id.ObjectGuid.ToString();
			}
			base.WriteError(new TestWebServicesTaskException(Strings.ErrorOabNotFoundForUser(user.Identity.ToString())), ErrorCategory.InvalidData, null);
			return null;
		}

		// Token: 0x06004755 RID: 18261 RVA: 0x0012518A File Offset: 0x0012338A
		private static string FormatOabUrl(string fqdn, string oabGuid)
		{
			return string.Format("https://{0}/oab/{1}", fqdn, oabGuid);
		}
	}
}
