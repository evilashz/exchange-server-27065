using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Management.Automation;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.SystemConfigurationTasks;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x020005ED RID: 1517
	public abstract class TestWebServicesTaskBase : Task
	{
		// Token: 0x1700100E RID: 4110
		// (get) Token: 0x060035F3 RID: 13811 RVA: 0x000DE3AD File Offset: 0x000DC5AD
		// (set) Token: 0x060035F4 RID: 13812 RVA: 0x000DE3B5 File Offset: 0x000DC5B5
		internal ITopologyConfigurationSession ConfigSession { get; private set; }

		// Token: 0x1700100F RID: 4111
		// (get) Token: 0x060035F5 RID: 13813 RVA: 0x000DE3BE File Offset: 0x000DC5BE
		// (set) Token: 0x060035F6 RID: 13814 RVA: 0x000DE3C6 File Offset: 0x000DC5C6
		internal IRecipientSession RecipientSession { get; private set; }

		// Token: 0x17001010 RID: 4112
		// (get) Token: 0x060035F7 RID: 13815 RVA: 0x000DE3CF File Offset: 0x000DC5CF
		// (set) Token: 0x060035F8 RID: 13816 RVA: 0x000DE3D7 File Offset: 0x000DC5D7
		private protected Server LocalServer { protected get; private set; }

		// Token: 0x17001011 RID: 4113
		// (get) Token: 0x060035F9 RID: 13817 RVA: 0x000DE3E0 File Offset: 0x000DC5E0
		// (set) Token: 0x060035FA RID: 13818 RVA: 0x000DE3E8 File Offset: 0x000DC5E8
		private protected string ClientAccessServerFqdn { protected get; private set; }

		// Token: 0x17001012 RID: 4114
		// (get) Token: 0x060035FB RID: 13819 RVA: 0x000DE3F1 File Offset: 0x000DC5F1
		// (set) Token: 0x060035FC RID: 13820 RVA: 0x000DE3F9 File Offset: 0x000DC5F9
		private protected string AutoDiscoverServerFqdn { protected get; private set; }

		// Token: 0x17001013 RID: 4115
		// (get) Token: 0x060035FD RID: 13821 RVA: 0x000DE402 File Offset: 0x000DC602
		// (set) Token: 0x060035FE RID: 13822 RVA: 0x000DE40A File Offset: 0x000DC60A
		internal UserWithCredential TestAccount { get; private set; }

		// Token: 0x17001014 RID: 4116
		// (get) Token: 0x060035FF RID: 13823 RVA: 0x000DE413 File Offset: 0x000DC613
		// (set) Token: 0x06003600 RID: 13824 RVA: 0x000DE41B File Offset: 0x000DC61B
		private protected string AutoDiscoverUrl { protected get; private set; }

		// Token: 0x17001015 RID: 4117
		// (get) Token: 0x06003601 RID: 13825
		protected abstract string CmdletName { get; }

		// Token: 0x17001016 RID: 4118
		// (get) Token: 0x06003602 RID: 13826
		protected abstract bool IsOutlookProvider { get; }

		// Token: 0x17001017 RID: 4119
		// (get) Token: 0x06003603 RID: 13827 RVA: 0x000DE424 File Offset: 0x000DC624
		protected bool IsFromAutoDiscover
		{
			get
			{
				return !this.MonitoringContext.IsPresent && this.ClientAccessServer == null;
			}
		}

		// Token: 0x17001018 RID: 4120
		// (get) Token: 0x06003604 RID: 13828 RVA: 0x000DE44C File Offset: 0x000DC64C
		protected string UserAgentString
		{
			get
			{
				return string.Format("{0}/{1}/{2}", this.LocalServer.Name, this.CmdletName, this.TestAccount.User.PrimarySmtpAddress);
			}
		}

		// Token: 0x17001019 RID: 4121
		// (get) Token: 0x06003605 RID: 13829 RVA: 0x000DE48C File Offset: 0x000DC68C
		// (set) Token: 0x06003606 RID: 13830 RVA: 0x000DE4B2 File Offset: 0x000DC6B2
		[Parameter(ParameterSetName = "MonitoringContextParameterSet", Mandatory = true)]
		public SwitchParameter MonitoringContext
		{
			get
			{
				return (SwitchParameter)(base.Fields["MonitoringContext"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["MonitoringContext"] = value;
			}
		}

		// Token: 0x1700101A RID: 4122
		// (get) Token: 0x06003607 RID: 13831 RVA: 0x000DE4CA File Offset: 0x000DC6CA
		// (set) Token: 0x06003608 RID: 13832 RVA: 0x000DE4E1 File Offset: 0x000DC6E1
		[Parameter(ParameterSetName = "ClientAccessServerParameterSet", Mandatory = false, ValueFromPipeline = true)]
		[ValidateNotNullOrEmpty]
		public ClientAccessServerIdParameter ClientAccessServer
		{
			get
			{
				return (ClientAccessServerIdParameter)base.Fields["ClientAccessServer"];
			}
			set
			{
				base.Fields["ClientAccessServer"] = value;
			}
		}

		// Token: 0x1700101B RID: 4123
		// (get) Token: 0x06003609 RID: 13833 RVA: 0x000DE4F4 File Offset: 0x000DC6F4
		// (set) Token: 0x0600360A RID: 13834 RVA: 0x000DE50B File Offset: 0x000DC70B
		[Parameter(ParameterSetName = "AutoDiscoverServerParameterSet", Mandatory = true)]
		[ValidateNotNullOrEmpty]
		public ClientAccessServerIdParameter AutoDiscoverServer
		{
			get
			{
				return (ClientAccessServerIdParameter)base.Fields["AutoDiscoverServer"];
			}
			set
			{
				base.Fields["AutoDiscoverServer"] = value;
			}
		}

		// Token: 0x1700101C RID: 4124
		// (get) Token: 0x0600360B RID: 13835 RVA: 0x000DE51E File Offset: 0x000DC71E
		// (set) Token: 0x0600360C RID: 13836 RVA: 0x000DE535 File Offset: 0x000DC735
		[Parameter(Mandatory = false, Position = 0, ValueFromPipeline = true)]
		public MailboxIdParameter Identity
		{
			get
			{
				return (MailboxIdParameter)base.Fields["Identity"];
			}
			set
			{
				base.Fields["Identity"] = value;
			}
		}

		// Token: 0x1700101D RID: 4125
		// (get) Token: 0x0600360D RID: 13837 RVA: 0x000DE548 File Offset: 0x000DC748
		// (set) Token: 0x0600360E RID: 13838 RVA: 0x000DE55F File Offset: 0x000DC75F
		[Parameter(Mandatory = false)]
		public PSCredential MailboxCredential
		{
			get
			{
				return (PSCredential)base.Fields["MailboxCredential"];
			}
			set
			{
				base.Fields["MailboxCredential"] = value;
			}
		}

		// Token: 0x1700101E RID: 4126
		// (get) Token: 0x0600360F RID: 13839 RVA: 0x000DE572 File Offset: 0x000DC772
		// (set) Token: 0x06003610 RID: 13840 RVA: 0x000DE598 File Offset: 0x000DC798
		[Parameter(Mandatory = false)]
		public SwitchParameter TrustAnySSLCertificate
		{
			get
			{
				return (SwitchParameter)(base.Fields["TrustAnySSLCertificate"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["TrustAnySSLCertificate"] = value;
			}
		}

		// Token: 0x06003611 RID: 13841 RVA: 0x000DE5B0 File Offset: 0x000DC7B0
		protected override bool IsKnownException(Exception e)
		{
			return base.IsKnownException(e) || MonitoringHelper.IsKnownExceptionForMonitoring(e) || e is CasHealthInstructResetCredentialsException;
		}

		// Token: 0x06003612 RID: 13842 RVA: 0x000DE5D0 File Offset: 0x000DC7D0
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			base.InternalBeginProcessing();
			this.ConfigSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(true, ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 259, "InternalBeginProcessing", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\SystemConfigurationTasks\\AutoDiscover\\TestWebServicesTaskBase.cs");
			ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest(), OrganizationId.ForestWideOrgId, null, false);
			this.RecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(null, true, ConsistencyMode.IgnoreInvalid, null, sessionSettings, ConfigScopes.TenantSubTree, 270, "InternalBeginProcessing", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\SystemConfigurationTasks\\AutoDiscover\\TestWebServicesTaskBase.cs");
			this.LocalServer = this.ConfigSession.FindLocalServer();
			this.ResolveAutoDiscoverServerFqdn();
			base.WriteVerbose(Strings.VerboseTestSourceServer(this.LocalServer.Fqdn));
			base.WriteVerbose(Strings.VerboseTestSourceSite(this.LocalServer.ServerSite.ToString()));
			TaskLogger.LogExit();
		}

		// Token: 0x06003613 RID: 13843 RVA: 0x000DE694 File Offset: 0x000DC894
		protected override void InternalValidate()
		{
			base.InternalValidate();
			if (this.Identity != null && this.MailboxCredential == null)
			{
				base.WriteError(new TestWebServicesTaskException(Strings.ErrorCredentialRequiredForIdentity(this.Identity.RawIdentity)), ErrorCategory.InvalidArgument, null);
			}
			this.ResolveTestAccount();
			base.WriteVerbose(Strings.VerboseTestUserIdentity(this.TestAccount.User.Identity.ToString()));
			base.WriteVerbose(Strings.VerboseTestUserAddress(this.TestAccount.User.PrimarySmtpAddress.ToString()));
			base.WriteVerbose(Strings.VerboseTestUserOrganization(this.TestAccount.User.OrganizationId.ToString()));
			this.ResolveClientAccessServerFqdn();
			this.ResolveAutoDiscoverUri();
		}

		// Token: 0x06003614 RID: 13844 RVA: 0x000DE758 File Offset: 0x000DC958
		protected override void InternalProcessRecord()
		{
			this.OutputMonitoringData();
		}

		// Token: 0x06003615 RID: 13845 RVA: 0x000DE760 File Offset: 0x000DC960
		protected bool ValidateAutoDiscover(out string ewsUrl, out string oabUrl)
		{
			AutoDiscoverValidator autoDiscoverValidator = new AutoDiscoverValidator(this.AutoDiscoverUrl, this.TestAccount.Credential, this.TestAccount.User.PrimarySmtpAddress.ToString())
			{
				VerboseDelegate = new Task.TaskVerboseLoggingDelegate(base.WriteVerbose),
				UserAgent = this.UserAgentString,
				IgnoreSslCertError = (this.TrustAnySSLCertificate || this.MonitoringContext.IsPresent),
				Provider = (this.IsOutlookProvider ? AutoDiscoverValidator.ProviderSchema.Outlook : AutoDiscoverValidator.ProviderSchema.Soap)
			};
			WebServicesTestOutcome.TestScenario scenario = this.IsOutlookProvider ? WebServicesTestOutcome.TestScenario.AutoDiscoverOutlookProvider : WebServicesTestOutcome.TestScenario.AutoDiscoverSoapProvider;
			bool flag = autoDiscoverValidator.Invoke();
			WebServicesTestOutcome outcome = new WebServicesTestOutcome
			{
				Scenario = scenario,
				Source = this.LocalServer.Fqdn,
				Result = (flag ? CasTransactionResultEnum.Success : CasTransactionResultEnum.Failure),
				Error = string.Format("{0}", autoDiscoverValidator.Error),
				ServiceEndpoint = TestWebServicesTaskBase.FqdnFromUrl(this.AutoDiscoverUrl),
				Latency = autoDiscoverValidator.Latency,
				ScenarioDescription = TestWebServicesTaskBase.GetScenarioDescription(scenario),
				Verbose = autoDiscoverValidator.Verbose
			};
			this.Output(outcome);
			ewsUrl = autoDiscoverValidator.EwsUrl;
			oabUrl = autoDiscoverValidator.OabUrl;
			return flag;
		}

		// Token: 0x06003616 RID: 13846 RVA: 0x000DE8C0 File Offset: 0x000DCAC0
		private void ResolveTestAccount()
		{
			UserWithCredential testAccount;
			if (this.Identity != null)
			{
				testAccount = default(UserWithCredential);
				testAccount.User = this.GetUniqueADObject<ADUser>(this.Identity, this.RecipientSession, true);
				testAccount.Credential = this.MailboxCredential.GetNetworkCredential();
			}
			else
			{
				testAccount = this.GetMonitoringAccount();
			}
			if (SmtpAddress.IsValidSmtpAddress(testAccount.Credential.UserName))
			{
				testAccount.Credential.Domain = null;
			}
			else if (Datacenter.IsMultiTenancyEnabled())
			{
				testAccount.Credential.UserName = string.Format("{0}@{1}", testAccount.Credential.UserName, testAccount.Credential.Domain);
				testAccount.Credential.Domain = null;
			}
			this.TestAccount = testAccount;
		}

		// Token: 0x06003617 RID: 13847 RVA: 0x000DE980 File Offset: 0x000DCB80
		private UserWithCredential GetMonitoringAccount()
		{
			UserWithCredential result = default(UserWithCredential);
			string datacenter = Datacenter.IsMicrosoftHostedOnly(true) ? ".Datacenter" : string.Empty;
			ADSite localSite = this.ConfigSession.GetLocalSite();
			try
			{
				result = CommonTestTasks.GetDefaultTestAccount(new CommonTestTasks.ClientAccessContext
				{
					Instance = this,
					MonitoringContext = true,
					ConfigurationSession = this.ConfigSession,
					RecipientSession = this.RecipientSession,
					Site = localSite,
					WindowsDomain = localSite.Id.DomainId.ToCanonicalName()
				});
			}
			catch (Exception ex)
			{
				base.WriteError(new TestWebServicesTaskException(Strings.ErrorTestMailboxNotFound(ExchangeSetupContext.ScriptPath, datacenter, ex.ToString())), ErrorCategory.InvalidData, null);
			}
			if (string.IsNullOrEmpty(result.Credential.Password))
			{
				base.WriteError(new TestWebServicesTaskException(Strings.ErrorTestMailboxPasswordNotFound(result.User.Identity.ToString(), ExchangeSetupContext.ScriptPath, datacenter)), ErrorCategory.InvalidData, null);
			}
			return result;
		}

		// Token: 0x06003618 RID: 13848 RVA: 0x000DEA80 File Offset: 0x000DCC80
		private void ResolveAutoDiscoverServerFqdn()
		{
			this.AutoDiscoverServerFqdn = null;
			if (this.AutoDiscoverServer == null)
			{
				return;
			}
			this.AutoDiscoverServerFqdn = this.AutoDiscoverServer.ToString();
			Server uniqueADObject = this.GetUniqueADObject<Server>(this.AutoDiscoverServer, this.ConfigSession, false);
			if (uniqueADObject != null)
			{
				if (!uniqueADObject.IsClientAccessServer)
				{
					base.WriteError(new TestWebServicesTaskException(Strings.ErrorServerNotCAS(uniqueADObject.Id.ToString())), ErrorCategory.InvalidArgument, null);
				}
				this.AutoDiscoverServerFqdn = uniqueADObject.Fqdn;
			}
		}

		// Token: 0x06003619 RID: 13849 RVA: 0x000DEAF8 File Offset: 0x000DCCF8
		private void ResolveClientAccessServerFqdn()
		{
			this.ClientAccessServerFqdn = null;
			if (this.ClientAccessServer == null)
			{
				return;
			}
			this.ClientAccessServerFqdn = this.ClientAccessServer.ToString();
			Server uniqueADObject = this.GetUniqueADObject<Server>(this.ClientAccessServer, this.ConfigSession, false);
			if (uniqueADObject != null)
			{
				if (!uniqueADObject.IsClientAccessServer)
				{
					base.WriteError(new TestWebServicesTaskException(Strings.ErrorServerNotCAS(uniqueADObject.Id.ToString())), ErrorCategory.InvalidArgument, null);
				}
				this.ClientAccessServerFqdn = uniqueADObject.Fqdn;
			}
		}

		// Token: 0x0600361A RID: 13850 RVA: 0x000DEB70 File Offset: 0x000DCD70
		private void ResolveAutoDiscoverUri()
		{
			if (this.MonitoringContext.IsPresent)
			{
				this.AutoDiscoverUrl = this.FormatAutoDiscoverUrl(this.LocalServer.Fqdn);
				base.WriteVerbose(Strings.VerboseBuildAutoDiscoverUrl(this.AutoDiscoverUrl));
				return;
			}
			if (this.ClientAccessServerFqdn != null)
			{
				this.AutoDiscoverUrl = this.FormatAutoDiscoverUrl(this.ClientAccessServerFqdn);
				base.WriteVerbose(Strings.VerboseBuildAutoDiscoverUrl(this.AutoDiscoverUrl));
				return;
			}
			if (this.AutoDiscoverServerFqdn != null)
			{
				this.AutoDiscoverUrl = this.FormatAutoDiscoverUrl(this.AutoDiscoverServerFqdn);
				base.WriteVerbose(Strings.VerboseBuildAutoDiscoverUrl(this.AutoDiscoverUrl));
				return;
			}
			SmtpAddress primarySmtpAddress = this.TestAccount.User.PrimarySmtpAddress;
			string text = AutoDiscoverHelper.GetAutoDiscoverEndpoint(primarySmtpAddress.ToString(), this.ConfigSession, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose));
			if (string.IsNullOrEmpty(text))
			{
				base.WriteError(new AutoDiscoverEndpointException(Strings.ErrorAutoDiscoverEndpointNotFound(primarySmtpAddress.Domain)), ErrorCategory.InvalidData, null);
			}
			text = this.NormalizeAutoDiscoverUrl(text);
			base.WriteVerbose(Strings.VerboseFindAutoDiscoverUrl(text, primarySmtpAddress.ToString()));
			this.AutoDiscoverUrl = text;
		}

		// Token: 0x0600361B RID: 13851 RVA: 0x000DEC90 File Offset: 0x000DCE90
		protected string GetSpecifiedEwsUrl()
		{
			if (this.MonitoringContext.IsPresent)
			{
				return TestWebServicesTaskBase.FormatEwsUrl(this.LocalServer.Fqdn);
			}
			if (this.ClientAccessServerFqdn != null)
			{
				return TestWebServicesTaskBase.FormatEwsUrl(this.ClientAccessServerFqdn);
			}
			return null;
		}

		// Token: 0x0600361C RID: 13852 RVA: 0x000DECD4 File Offset: 0x000DCED4
		protected T GetUniqueADObject<T>(ADIdParameter idParameter, IConfigDataProvider provider, bool throwWhenMissing) where T : ADObject, new()
		{
			T t = default(T);
			foreach (T t2 in idParameter.GetObjects<T>(null, provider))
			{
				if (t != null)
				{
					base.WriteError(new ManagementObjectAmbiguousException(Strings.ErrorManagementObjectAmbiguous(idParameter.ToString())), ErrorCategory.InvalidArgument, null);
				}
				t = t2;
			}
			if (t == null && throwWhenMissing)
			{
				base.WriteError(new ManagementObjectNotFoundException(Strings.ErrorManagementObjectNotFound(idParameter.ToString())), ErrorCategory.InvalidArgument, null);
			}
			return t;
		}

		// Token: 0x0600361D RID: 13853 RVA: 0x000DED6C File Offset: 0x000DCF6C
		protected void Output(WebServicesTestOutcome outcome)
		{
			base.WriteObject(outcome);
			if (this.MonitoringContext.IsPresent)
			{
				this.outcomes.Add(outcome);
			}
		}

		// Token: 0x0600361E RID: 13854 RVA: 0x000DED9C File Offset: 0x000DCF9C
		protected void OutputMonitoringData()
		{
			if (this.MonitoringContext.IsPresent)
			{
				MonitoringData monitoringData = new MonitoringData();
				foreach (WebServicesTestOutcome webServicesTestOutcome in this.outcomes)
				{
					MonitoringEvent item = new MonitoringEvent("MSExchange Monitoring " + this.CmdletName, webServicesTestOutcome.MonitoringEventId, TestWebServicesTaskBase.EventTypeFromCasResultEnum(webServicesTestOutcome.Result), webServicesTestOutcome.ToString());
					monitoringData.Events.Add(item);
					if (Datacenter.IsMultiTenancyEnabled())
					{
						EventLogEntryType entryType = EventLogEntryType.Information;
						string text;
						if (webServicesTestOutcome.Result == CasTransactionResultEnum.Failure)
						{
							entryType = EventLogEntryType.Error;
							text = Strings.WebServicesTestErrorEventDetail(this.CmdletName, webServicesTestOutcome.Scenario.ToString(), webServicesTestOutcome.Result.ToString(), webServicesTestOutcome.Latency.ToString(), webServicesTestOutcome.Error, webServicesTestOutcome.Verbose);
						}
						else if (webServicesTestOutcome.Result == CasTransactionResultEnum.Skipped)
						{
							entryType = EventLogEntryType.Warning;
							text = Strings.WebServicesTestEventDetail(this.CmdletName, webServicesTestOutcome.Scenario.ToString(), webServicesTestOutcome.Result.ToString(), string.Empty);
						}
						else
						{
							text = Strings.WebServicesTestEventDetail(this.CmdletName, webServicesTestOutcome.Scenario.ToString(), webServicesTestOutcome.Result.ToString(), webServicesTestOutcome.Latency.ToString());
						}
						ExEventLog.EventTuple tuple = new ExEventLog.EventTuple((uint)webServicesTestOutcome.MonitoringEventId, 0, entryType, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);
						TestWebServicesTaskBase.EventLog.LogEvent(tuple, string.Empty, new object[]
						{
							text
						});
					}
				}
				base.WriteObject(monitoringData);
			}
		}

		// Token: 0x0600361F RID: 13855 RVA: 0x000DEF7C File Offset: 0x000DD17C
		protected void OutputSkippedOutcome(WebServicesTestOutcome.TestScenario scenario, LocalizedString message)
		{
			WebServicesTestOutcome outcome = new WebServicesTestOutcome
			{
				Scenario = scenario,
				Source = this.LocalServer.Fqdn,
				Result = CasTransactionResultEnum.Skipped,
				Error = message.ToString(),
				ServiceEndpoint = string.Empty,
				Latency = 0L,
				ScenarioDescription = TestWebServicesTaskBase.GetScenarioDescription(scenario)
			};
			this.Output(outcome);
		}

		// Token: 0x06003620 RID: 13856 RVA: 0x000DEFE9 File Offset: 0x000DD1E9
		protected string FormatAutoDiscoverUrl(string fqdn)
		{
			return string.Format("https://{0}/AutoDiscover/AutoDiscover.{1}", fqdn, this.IsOutlookProvider ? "xml" : "svc");
		}

		// Token: 0x06003621 RID: 13857 RVA: 0x000DF00C File Offset: 0x000DD20C
		protected string NormalizeAutoDiscoverUrl(string url)
		{
			int length = url.Length;
			string text = url.Substring(length - 3, 3);
			if (!text.Equals("xml", StringComparison.OrdinalIgnoreCase) && !text.Equals("svc", StringComparison.OrdinalIgnoreCase))
			{
				return url;
			}
			return url.Substring(0, length - 3) + (this.IsOutlookProvider ? "xml" : "svc");
		}

		// Token: 0x06003622 RID: 13858 RVA: 0x000DF06C File Offset: 0x000DD26C
		protected static string FormatEwsUrl(string fqdn)
		{
			return string.Format("https://{0}/ews/Exchange.asmx", fqdn);
		}

		// Token: 0x06003623 RID: 13859 RVA: 0x000DF07C File Offset: 0x000DD27C
		protected static string FqdnFromUrl(string url)
		{
			Uri uri = new Uri(url);
			return uri.Host;
		}

		// Token: 0x06003624 RID: 13860 RVA: 0x000DF096 File Offset: 0x000DD296
		protected static string GetScenarioDescription(WebServicesTestOutcome.TestScenario scenario)
		{
			return LocalizedDescriptionAttribute.FromEnum(typeof(WebServicesTestOutcome.TestScenario), scenario);
		}

		// Token: 0x06003625 RID: 13861 RVA: 0x000DF0B0 File Offset: 0x000DD2B0
		private static EventTypeEnumeration EventTypeFromCasResultEnum(CasTransactionResultEnum casResult)
		{
			switch (casResult)
			{
			case CasTransactionResultEnum.Success:
				return EventTypeEnumeration.Success;
			case CasTransactionResultEnum.Failure:
				return EventTypeEnumeration.Error;
			case CasTransactionResultEnum.Skipped:
				return EventTypeEnumeration.Warning;
			default:
				return EventTypeEnumeration.Warning;
			}
		}

		// Token: 0x040024F0 RID: 9456
		protected const string MonitoringContextParameterSet = "MonitoringContextParameterSet";

		// Token: 0x040024F1 RID: 9457
		protected const string ClientAccessServerParameterSet = "ClientAccessServerParameterSet";

		// Token: 0x040024F2 RID: 9458
		protected const string AutoDiscoverServerParameterSet = "AutoDiscoverServerParameterSet";

		// Token: 0x040024F3 RID: 9459
		private static readonly ExEventLog EventLog = new ExEventLog(new Guid("400A88BD-6F94-480C-8B77-28B373BD8574"), "EWS Monitoring", "EWS Monitoring Events");

		// Token: 0x040024F4 RID: 9460
		private List<WebServicesTestOutcome> outcomes = new List<WebServicesTestOutcome>();
	}
}
