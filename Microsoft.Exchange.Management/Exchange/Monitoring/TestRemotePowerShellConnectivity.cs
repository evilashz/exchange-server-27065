using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Management.Automation;
using System.Management.Automation.Host;
using System.Management.Automation.Runspaces;
using System.Net;
using System.Security;
using System.ServiceProcess;
using System.Text;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Extensions;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.EventMessages;
using Microsoft.Exchange.Management.Metabase;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Security.Cryptography.X509Certificates;
using Microsoft.PowerShell.HostingTools;
using Microsoft.Web.Administration;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x020005E1 RID: 1505
	[Cmdlet("Test", "PowerShellConnectivity", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class TestRemotePowerShellConnectivity : TestVirtualDirectoryConnectivity
	{
		// Token: 0x17000FE3 RID: 4067
		// (get) Token: 0x06003551 RID: 13649 RVA: 0x000DB3C2 File Offset: 0x000D95C2
		// (set) Token: 0x06003552 RID: 13650 RVA: 0x000DB3ED File Offset: 0x000D95ED
		[Parameter]
		public AuthenticationMechanism Authentication
		{
			get
			{
				if (!base.Fields.Contains("Authentication"))
				{
					return AuthenticationMechanism.Default;
				}
				return (AuthenticationMechanism)base.Fields["Authentication"];
			}
			set
			{
				base.Fields["Authentication"] = value;
			}
		}

		// Token: 0x17000FE4 RID: 4068
		// (get) Token: 0x06003553 RID: 13651 RVA: 0x000DB405 File Offset: 0x000D9605
		// (set) Token: 0x06003554 RID: 13652 RVA: 0x000DB41C File Offset: 0x000D961C
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = true, ParameterSetName = "URL")]
		public Uri ConnectionUri
		{
			get
			{
				return (Uri)base.Fields["ConnectionUri"];
			}
			set
			{
				base.Fields["ConnectionUri"] = value;
			}
		}

		// Token: 0x17000FE5 RID: 4069
		// (get) Token: 0x06003555 RID: 13653 RVA: 0x000DB42F File Offset: 0x000D962F
		// (set) Token: 0x06003556 RID: 13654 RVA: 0x000DB437 File Offset: 0x000D9637
		[ValidateNotNull]
		[Parameter(Mandatory = true, ParameterSetName = "URL")]
		public PSCredential TestCredential
		{
			get
			{
				return this.mailboxCredential;
			}
			set
			{
				this.mailboxCredential = value;
			}
		}

		// Token: 0x17000FE6 RID: 4070
		// (get) Token: 0x06003557 RID: 13655 RVA: 0x000DB440 File Offset: 0x000D9640
		// (set) Token: 0x06003558 RID: 13656 RVA: 0x000DB448 File Offset: 0x000D9648
		private new SwitchParameter LightMode
		{
			get
			{
				return base.LightMode;
			}
			set
			{
				base.LightMode = value;
			}
		}

		// Token: 0x17000FE7 RID: 4071
		// (get) Token: 0x06003559 RID: 13657 RVA: 0x000DB451 File Offset: 0x000D9651
		// (set) Token: 0x0600355A RID: 13658 RVA: 0x000DB459 File Offset: 0x000D9659
		private new uint Timeout
		{
			get
			{
				return base.Timeout;
			}
			set
			{
				base.Timeout = value;
			}
		}

		// Token: 0x0600355B RID: 13659 RVA: 0x000DB464 File Offset: 0x000D9664
		public TestRemotePowerShellConnectivity() : base(Strings.CasHealthPowerShellLongName, Strings.CasHealthPowerShellShortName, TransientErrorCache.PowerShellTransientErrorCache, "MSExchange Monitoring PowerShellConnectivity Internal", "MSExchange Monitoring PowerShellConnectivity External")
		{
		}

		// Token: 0x0600355C RID: 13660 RVA: 0x000DB4CC File Offset: 0x000D96CC
		protected override IEnumerable<ExchangeVirtualDirectory> GetVirtualDirectories(ADObjectId serverId, QueryFilter filter)
		{
			List<ExchangeVirtualDirectory> list = new List<ExchangeVirtualDirectory>();
			QueryFilter queryFilter = new OrFilter(new QueryFilter[]
			{
				new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Name, "PowerShell (Default Web Site)"),
				new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Name, "PowerShell-LiveID (Default Web Site)")
			});
			filter = new AndFilter(new QueryFilter[]
			{
				filter,
				queryFilter
			});
			foreach (ExchangeVirtualDirectory exchangeVirtualDirectory in base.GetVirtualDirectories<ADPowerShellVirtualDirectory>(serverId, filter))
			{
				ADPowerShellVirtualDirectory adpowerShellVirtualDirectory = (ADPowerShellVirtualDirectory)exchangeVirtualDirectory;
				this.ProcessMetabaseProperties(adpowerShellVirtualDirectory);
				list.Add(adpowerShellVirtualDirectory);
				base.TraceInfo("Virtual Directory " + adpowerShellVirtualDirectory.DistinguishedName.ToString() + " found in server " + serverId.DistinguishedName.ToString());
			}
			return list;
		}

		// Token: 0x0600355D RID: 13661 RVA: 0x000DB5B0 File Offset: 0x000D97B0
		internal override void UpdateTransientErrorCache(TestCasConnectivity.TestCasConnectivityRunInstance instance, int cFailed, int cWarning, ref int cFailedTransactions, ref int cWarningTransactions, StringBuilder failedStr, StringBuilder warningStr, ref StringBuilder failedTransactionsStr, ref StringBuilder warningTransactionsStr)
		{
			TransientErrorCache transientErrorCache = this.GetTransientErrorCache();
			string text = (instance.exchangePrincipal != null) ? instance.exchangePrincipal.MailboxInfo.Location.ServerFqdn : null;
			if (cFailed > 0 || cWarning > 0)
			{
				if (transientErrorCache != null && !string.IsNullOrEmpty(text) && !transientErrorCache.ContainsError(text, instance.VirtualDirectoryName))
				{
					transientErrorCache.Add(new CASServiceError(text, instance.VirtualDirectoryName));
					return;
				}
				if (cFailed > 0)
				{
					cFailedTransactions += cFailed;
					failedTransactionsStr.Append(failedStr);
				}
				if (cWarning > 0)
				{
					cWarningTransactions += cWarning;
					warningTransactionsStr.Append(warningStr);
					return;
				}
			}
			else if (transientErrorCache != null && !string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(instance.VirtualDirectoryName))
			{
				transientErrorCache.Remove(text, instance.VirtualDirectoryName);
			}
		}

		// Token: 0x0600355E RID: 13662 RVA: 0x000DB66C File Offset: 0x000D986C
		protected override void ValidateTestWebApplicationRequirements()
		{
			if (this.casToTest == null)
			{
				if (this.localServer != null && (this.localServer.IsMailboxServer || this.localServer.IsUnifiedMessagingServer || this.localServer.IsHubTransportServer))
				{
					base.TraceInfo("The MBX,UM or HT server to test is the local server.");
					this.isCasServer = false;
					this.casToTest = this.localServer;
				}
				else
				{
					base.TraceInfo("Local computer is not a CAS,MBX, UM or HT server. No CAS, MBX, UM or HT server has been found.");
				}
				if (this.casToTest == null)
				{
					base.CasConnectivityWriteError(new ApplicationException(this.NoCasMbxUmHtArgumentError), ErrorCategory.InvalidArgument, null);
				}
			}
		}

		// Token: 0x17000FE8 RID: 4072
		// (get) Token: 0x0600355F RID: 13663 RVA: 0x000DB6FB File Offset: 0x000D98FB
		internal LocalizedString NoCasMbxUmHtArgumentError
		{
			get
			{
				return Strings.CasHealthWebAppNoCasMbxUmHtArgumentError;
			}
		}

		// Token: 0x06003560 RID: 13664 RVA: 0x000DB704 File Offset: 0x000D9904
		protected override List<TestCasConnectivity.TestCasConnectivityRunInstance> PopulateInfoPerCas(TestCasConnectivity.TestCasConnectivityRunInstance instance, List<CasTransactionOutcome> outcomeList)
		{
			TaskLogger.LogEnter();
			List<TestCasConnectivity.TestCasConnectivityRunInstance> result;
			try
			{
				if (base.Fields.IsModified("ConnectionUri"))
				{
					base.WriteVerbose(Strings.CasHealthOwaTestUrlSpecified(this.ConnectionUri.AbsoluteUri));
					TestCasConnectivity.TestCasConnectivityRunInstance testCasConnectivityRunInstance = new TestCasConnectivity.TestCasConnectivityRunInstance(instance);
					testCasConnectivityRunInstance.baseUri = TestCasConnectivity.GetUrlWithTrailingSlash(this.ConnectionUri);
					testCasConnectivityRunInstance.UrlType = VirtualDirectoryUriScope.Unknown;
					testCasConnectivityRunInstance.CasFqdn = null;
					result = new List<TestCasConnectivity.TestCasConnectivityRunInstance>
					{
						testCasConnectivityRunInstance
					};
				}
				else
				{
					result = base.PopulateInfoPerCas(instance, outcomeList);
				}
			}
			finally
			{
				TaskLogger.LogExit();
			}
			return result;
		}

		// Token: 0x06003561 RID: 13665 RVA: 0x000DB798 File Offset: 0x000D9998
		protected override List<CasTransactionOutcome> ExecuteTests(TestCasConnectivity.TestCasConnectivityRunInstance instance)
		{
			ExDateTime now = ExDateTime.Now;
			TaskLogger.LogEnter();
			base.WriteVerbose(Strings.CasHealthScenarioLogon);
			if (base.TestType == OwaConnectivityTestType.External && !this.ValidateExternalTest(instance))
			{
				instance.Result.Complete();
				return null;
			}
			LocalizedString localizedString = LocalizedString.Empty;
			Uri uri;
			if (base.Fields.IsModified("ConnectionUri"))
			{
				uri = this.ConnectionUri;
				localizedString = Strings.CasHealthPowerShellConnectionUri(uri.ToString(), "user supplied Uri");
			}
			else if (base.TestType == OwaConnectivityTestType.External)
			{
				uri = instance.VirtualDirectory.ExternalUrl;
				localizedString = Strings.CasHealthPowerShellConnectionUri(uri.ToString(), "Virtual Directory External Uri");
			}
			else
			{
				uri = instance.VirtualDirectory.InternalUrl;
				localizedString = Strings.CasHealthPowerShellConnectionUri(uri.ToString(), "Virtual Directory Internal Uri");
			}
			base.TraceInfo(localizedString);
			base.WriteVerbose(localizedString);
			ADPowerShellVirtualDirectory adpowerShellVirtualDirectory = instance.VirtualDirectory as ADPowerShellVirtualDirectory;
			if (adpowerShellVirtualDirectory != null)
			{
				base.TraceInfo(Strings.CasHealthPowerShellConnectionVirtualDirectory(adpowerShellVirtualDirectory.Name));
				base.WriteVerbose(Strings.CasHealthPowerShellConnectionVirtualDirectory(adpowerShellVirtualDirectory.Name));
			}
			if (uri == null)
			{
				CasTransactionOutcome casTransactionOutcome = this.BuildOutcome(Strings.CasHealthOwaLogonScenarioName, null, instance);
				base.TraceInfo("No External or Internal Url found for testing");
				casTransactionOutcome.Update(CasTransactionResultEnum.Failure, (base.TestType == OwaConnectivityTestType.External) ? Strings.CasHealthOwaNoExternalUrl : Strings.CasHealthOwaNoInternalUrl);
				instance.Outcomes.Enqueue(casTransactionOutcome);
				instance.Result.Outcomes.Add(casTransactionOutcome);
				base.WriteErrorAndMonitoringEvent(new LocalizedException((base.TestType == OwaConnectivityTestType.External) ? Strings.CasHealthOwaNoExternalUrl : Strings.CasHealthOwaNoInternalUrl), (ErrorCategory)1001, null, 1010, (base.TestType == OwaConnectivityTestType.External) ? "MSExchange Monitoring PowerShellConnectivity External" : "MSExchange Monitoring PowerShellConnectivity Internal", false);
				base.WriteObject(casTransactionOutcome);
			}
			else
			{
				CasTransactionOutcome casTransactionOutcome2 = this.BuildOutcome(Strings.CasHealthScenarioLogon, Strings.CasHealthPowerShellRemoteConnectionScenario(uri.ToString(), this.Authentication.ToString()), instance);
				PSCredential credential;
				if (base.ParameterSetName == "URL")
				{
					credential = this.TestCredential;
					localizedString = Strings.CasHealthPowerShellConnectionUserCredential(this.TestCredential.UserName, "user supplied");
				}
				else
				{
					credential = new PSCredential(instance.credentials.UserName, instance.credentials.Password.ConvertToSecureString());
					localizedString = Strings.CasHealthPowerShellConnectionUserCredential(instance.credentials.UserName, "default for test user");
				}
				base.TraceInfo(localizedString);
				base.WriteVerbose(localizedString);
				WSManConnectionInfo wsmanConnectionInfo;
				if (base.TestType == OwaConnectivityTestType.External)
				{
					wsmanConnectionInfo = new WSManConnectionInfo(uri, "http://schemas.microsoft.com/powershell/Microsoft.Exchange", this.certThumbprint);
				}
				else
				{
					wsmanConnectionInfo = new WSManConnectionInfo(uri, "http://schemas.microsoft.com/powershell/Microsoft.Exchange", credential);
				}
				if (base.Fields.IsModified("Authentication"))
				{
					wsmanConnectionInfo.AuthenticationMechanism = this.Authentication;
				}
				else if (adpowerShellVirtualDirectory != null && adpowerShellVirtualDirectory.LiveIdBasicAuthentication != null && adpowerShellVirtualDirectory.LiveIdBasicAuthentication.Value)
				{
					wsmanConnectionInfo.AuthenticationMechanism = AuthenticationMechanism.Basic;
				}
				base.TraceInfo(Strings.CasHealthPowerShellConnectionAuthenticationType(wsmanConnectionInfo.AuthenticationMechanism.ToString()));
				base.WriteVerbose(Strings.CasHealthPowerShellConnectionAuthenticationType(wsmanConnectionInfo.AuthenticationMechanism.ToString()));
				if (base.TrustAnySSLCertificate)
				{
					wsmanConnectionInfo.SkipCACheck = true;
					wsmanConnectionInfo.SkipCNCheck = true;
					wsmanConnectionInfo.SkipRevocationCheck = true;
				}
				using (Runspace runspace = RunspaceFactory.CreateRunspace(TestRemotePowerShellConnectivity.psHost, wsmanConnectionInfo))
				{
					try
					{
						runspace.Open();
						base.WriteVerbose(Strings.CasHealtRemotePowerShellOpenRunspaceSucceeded);
						base.TraceInfo(Strings.CasHealtRemotePowerShellOpenRunspaceSucceeded);
						runspace.Close();
						base.TraceInfo(Strings.CasHealtRemotePowerShellCloseRunspaceSucceeded);
						base.WriteVerbose(Strings.CasHealtRemotePowerShellCloseRunspaceSucceeded);
						casTransactionOutcome2.Update(CasTransactionResultEnum.Success, base.ComputeLatency(now), null);
					}
					catch (Exception ex)
					{
						casTransactionOutcome2.Update(CasTransactionResultEnum.Failure, base.ComputeLatency(now), ex.Message);
						instance.Outcomes.Enqueue(casTransactionOutcome2);
						instance.Result.Outcomes.Add(casTransactionOutcome2);
						try
						{
							string hostName = Dns.GetHostName();
							if (adpowerShellVirtualDirectory != null && string.Compare(this.casToTest.Fqdn, Dns.GetHostEntry(hostName).HostName, true) == 0)
							{
								this.CheckRequiredServicesAndAppPool(adpowerShellVirtualDirectory);
							}
						}
						catch (Exception)
						{
						}
						base.WriteErrorAndMonitoringEvent(new LocalizedException(Strings.CasHealthPowerShellLogonFailed((adpowerShellVirtualDirectory != null) ? adpowerShellVirtualDirectory.Name : this.ConnectionUri.ToString(), ex.Message)), (ErrorCategory)1001, null, 1001, (base.TestType == OwaConnectivityTestType.External) ? "MSExchange Monitoring PowerShellConnectivity External" : "MSExchange Monitoring PowerShellConnectivity Internal", false);
					}
					finally
					{
						base.WriteObject(casTransactionOutcome2);
					}
				}
				instance.Result.Complete();
			}
			return null;
		}

		// Token: 0x06003562 RID: 13666 RVA: 0x000DBC90 File Offset: 0x000D9E90
		private void CheckRequiredServicesAndAppPool(ADPowerShellVirtualDirectory psVdir)
		{
			this.CheckServices(this.requiredCommonServices, psVdir);
			if (string.Compare(psVdir.Name, "PowerShell (Default Web Site)", true) == 0)
			{
				this.CheckAppPool("MSExchangePowerShellAppPool");
				return;
			}
			if (string.Compare(psVdir.Name, "PowerShell-LiveID (Default Web Site)", true) == 0)
			{
				this.CheckAppPool("MSExchangePowerShellLiveIDAppPool");
				this.CheckServices(this.requiredPowershellLiveIdServices, psVdir);
			}
		}

		// Token: 0x06003563 RID: 13667 RVA: 0x000DBCF4 File Offset: 0x000D9EF4
		private void CheckServices(string[] requiredServices, ADPowerShellVirtualDirectory psVdir)
		{
			foreach (string text in requiredServices)
			{
				if (ManageServiceBase.GetServiceStatus(text) != ServiceControllerStatus.Running)
				{
					ExManagementApplicationLogger.LogEvent(ManagementEventLogConstants.Tuple_RequiredServiceNotRunning, new string[]
					{
						psVdir.Name,
						text
					});
					base.WriteVerbose(Strings.CasHealthPowerShellServiceNotRunning(psVdir.Name, text));
					base.TraceInfo(Strings.CasHealthPowerShellServiceNotRunning(psVdir.Name, text));
				}
			}
		}

		// Token: 0x06003564 RID: 13668 RVA: 0x000DBD68 File Offset: 0x000D9F68
		private void CheckAppPool(string appPoolName)
		{
			using (ServerManager serverManager = new ServerManager())
			{
				ApplicationPool applicationPool = serverManager.ApplicationPools[appPoolName];
				if (applicationPool != null && applicationPool.State != 1)
				{
					ExManagementApplicationLogger.LogEvent(ManagementEventLogConstants.Tuple_AppPoolNotRunning, new string[]
					{
						applicationPool.Name
					});
					base.WriteVerbose(Strings.CasHealthPowerShellAppPoolNotRunning(applicationPool.Name));
					base.TraceInfo(Strings.CasHealthPowerShellAppPoolNotRunning(applicationPool.Name));
				}
			}
		}

		// Token: 0x06003565 RID: 13669 RVA: 0x000DBDF4 File Offset: 0x000D9FF4
		private void ProcessMetabaseProperties(ADPowerShellVirtualDirectory virtualDirectory)
		{
			try
			{
				using (DirectoryEntry directoryEntry = IisUtility.CreateIISDirectoryEntry(virtualDirectory.MetabasePath, new Task.TaskErrorLoggingReThrowDelegate(this.WriteError), virtualDirectory.Identity))
				{
					virtualDirectory.BasicAuthentication = new bool?(IisUtility.CheckForAuthenticationMethod(directoryEntry, AuthenticationMethodFlags.Basic));
					virtualDirectory.DigestAuthentication = new bool?(IisUtility.CheckForAuthenticationMethod(directoryEntry, AuthenticationMethodFlags.Digest));
					virtualDirectory.WindowsAuthentication = new bool?(IisUtility.CheckForAuthenticationMethod(directoryEntry, AuthenticationMethodFlags.Ntlm));
					virtualDirectory.CertificateAuthentication = new bool?(IisUtility.CheckForAuthenticationMethod(directoryEntry, AuthenticationMethodFlags.Certificate));
					virtualDirectory.LiveIdBasicAuthentication = new bool?(virtualDirectory.InternalAuthenticationMethods.Contains(AuthenticationMethod.LiveIdBasic));
					virtualDirectory.WSSecurityAuthentication = new bool?(virtualDirectory.InternalAuthenticationMethods.Contains(AuthenticationMethod.WSSecurity) && IisUtility.CheckForAuthenticationMethod(directoryEntry, AuthenticationMethodFlags.WSSecurity));
					virtualDirectory.ResetChangeTracking();
				}
			}
			catch (Exception ex)
			{
				base.WriteErrorAndMonitoringEvent(new CannotPopulateMetabaseInformationException(virtualDirectory.Name, ex.Message, ex), (ErrorCategory)1001, null, 1001, "MSExchange Monitoring PowerShellConnectivity Internal", true);
			}
		}

		// Token: 0x06003566 RID: 13670 RVA: 0x000DBF08 File Offset: 0x000DA108
		private void GetRpsCertificateThumbprint()
		{
			if (this.isCasServer)
			{
				Exception ex = null;
				try
				{
					ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 614, "GetRpsCertificateThumbprint", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Monitoring\\Tasks\\TestRemotePowerShellConnectivity.cs");
					ServiceEndpoint endpoint = topologyConfigurationSession.GetEndpointContainer().GetEndpoint("ForwardSyncRpsEndPoint");
					this.certThumbprint = TlsCertificateInfo.FindFirstCertWithSubjectDistinguishedName(endpoint.CertificateSubject).Thumbprint;
				}
				catch (ServiceEndpointNotFoundException ex2)
				{
					ex = ex2;
				}
				catch (ArgumentException ex3)
				{
					ex = ex3;
				}
				if (ex != null)
				{
					this.monitoringData.Events.Add(new MonitoringEvent("MSExchange Monitoring PowerShellConnectivity External", 1012, EventTypeEnumeration.Warning, "PS ExternalUrl test for certificate connection skipped:" + ex.ToString()));
				}
			}
		}

		// Token: 0x06003567 RID: 13671 RVA: 0x000DBFC8 File Offset: 0x000DA1C8
		private bool ValidateExternalTest(TestCasConnectivity.TestCasConnectivityRunInstance instance)
		{
			bool result = true;
			if (string.Compare(instance.VirtualDirectory.Name, "PowerShell-LiveID (Default Web Site)", true) == 0)
			{
				base.WriteVerbose(Strings.CasHealthPowerShellSkipCertVDir("PS liveID ExternalUrl"));
				result = false;
			}
			else
			{
				this.GetRpsCertificateThumbprint();
				if (this.certThumbprint == null)
				{
					this.WriteWarning(Strings.CasHealthPowerShellSkipCertVDir("PS ExternalUrl without ForwardSync Certificate"));
					result = false;
				}
			}
			return result;
		}

		// Token: 0x040024B0 RID: 9392
		private const string monitoringEventSourceInternal = "MSExchange Monitoring PowerShellConnectivity Internal";

		// Token: 0x040024B1 RID: 9393
		private const string monitoringEventSourceExternal = "MSExchange Monitoring PowerShellConnectivity External";

		// Token: 0x040024B2 RID: 9394
		private const string ExchangeShellSchema = "http://schemas.microsoft.com/powershell/Microsoft.Exchange";

		// Token: 0x040024B3 RID: 9395
		private const string strPSVdirAppPool = "MSExchangePowerShellAppPool";

		// Token: 0x040024B4 RID: 9396
		private const string strPSLiveIdVdirAppPool = "MSExchangePowerShellLiveIDAppPool";

		// Token: 0x040024B5 RID: 9397
		private const string strPSVdirName = "PowerShell (Default Web Site)";

		// Token: 0x040024B6 RID: 9398
		private const string strPSLiveIdVdirName = "PowerShell-LiveID (Default Web Site)";

		// Token: 0x040024B7 RID: 9399
		private string[] requiredCommonServices = new string[]
		{
			"W3SVC",
			"MSExchangeIS"
		};

		// Token: 0x040024B8 RID: 9400
		private string[] requiredPowershellLiveIdServices = new string[]
		{
			"MSExchangeProtectedServiceHost"
		};

		// Token: 0x040024B9 RID: 9401
		private static TestRemotePowerShellConnectivity.TPSHost psHost = new TestRemotePowerShellConnectivity.TPSHost();

		// Token: 0x040024BA RID: 9402
		private bool isCasServer = true;

		// Token: 0x040024BB RID: 9403
		private string certThumbprint;

		// Token: 0x020005E2 RID: 1506
		private class TPSHost : RunspaceHost
		{
			// Token: 0x06003569 RID: 13673 RVA: 0x000DC030 File Offset: 0x000DA230
			public TPSHost()
			{
				this.hostUI = new TestRemotePowerShellConnectivity.TPSHostUI(this);
				this.hostRawUI = new TestRemotePowerShellConnectivity.TPSHostRawUI(this, (TestRemotePowerShellConnectivity.TPSHostUI)this.UI);
			}

			// Token: 0x17000FE9 RID: 4073
			// (get) Token: 0x0600356A RID: 13674 RVA: 0x000DC05B File Offset: 0x000DA25B
			public sealed override PSHostUserInterface UI
			{
				get
				{
					return this.hostUI;
				}
			}

			// Token: 0x17000FEA RID: 4074
			// (get) Token: 0x0600356B RID: 13675 RVA: 0x000DC063 File Offset: 0x000DA263
			public TestRemotePowerShellConnectivity.TPSHostRawUI RawUI
			{
				get
				{
					return this.hostRawUI;
				}
			}

			// Token: 0x040024BC RID: 9404
			private TestRemotePowerShellConnectivity.TPSHostUI hostUI;

			// Token: 0x040024BD RID: 9405
			private TestRemotePowerShellConnectivity.TPSHostRawUI hostRawUI;
		}

		// Token: 0x020005E3 RID: 1507
		private class TPSHostUI : RunspaceHostUI
		{
			// Token: 0x0600356C RID: 13676 RVA: 0x000DC06B File Offset: 0x000DA26B
			public TPSHostUI(TestRemotePowerShellConnectivity.TPSHost owner) : base(owner)
			{
				this.owner = owner;
			}

			// Token: 0x17000FEB RID: 4075
			// (get) Token: 0x0600356D RID: 13677 RVA: 0x000DC07B File Offset: 0x000DA27B
			public override PSHostRawUserInterface RawUI
			{
				get
				{
					return this.owner.RawUI;
				}
			}

			// Token: 0x0600356E RID: 13678 RVA: 0x000DC088 File Offset: 0x000DA288
			public override string ReadLine()
			{
				return null;
			}

			// Token: 0x0600356F RID: 13679 RVA: 0x000DC08B File Offset: 0x000DA28B
			public override SecureString ReadLineAsSecureString()
			{
				return null;
			}

			// Token: 0x06003570 RID: 13680 RVA: 0x000DC08E File Offset: 0x000DA28E
			public override void Write(ConsoleColor foregroundColor, ConsoleColor backgroundColor, string value)
			{
			}

			// Token: 0x06003571 RID: 13681 RVA: 0x000DC090 File Offset: 0x000DA290
			public override void Write(string value)
			{
			}

			// Token: 0x06003572 RID: 13682 RVA: 0x000DC092 File Offset: 0x000DA292
			public override void WriteDebugLine(string message)
			{
			}

			// Token: 0x06003573 RID: 13683 RVA: 0x000DC094 File Offset: 0x000DA294
			public override void WriteLine(string value)
			{
			}

			// Token: 0x06003574 RID: 13684 RVA: 0x000DC096 File Offset: 0x000DA296
			public override void WriteVerboseLine(string message)
			{
			}

			// Token: 0x06003575 RID: 13685 RVA: 0x000DC098 File Offset: 0x000DA298
			public override void WriteProgress(long value, ProgressRecord record)
			{
			}

			// Token: 0x06003576 RID: 13686 RVA: 0x000DC09A File Offset: 0x000DA29A
			public override void WriteErrorLine(string message)
			{
			}

			// Token: 0x06003577 RID: 13687 RVA: 0x000DC09C File Offset: 0x000DA29C
			public override void WriteWarningLine(string message)
			{
			}

			// Token: 0x040024BE RID: 9406
			private TestRemotePowerShellConnectivity.TPSHost owner;
		}

		// Token: 0x020005E4 RID: 1508
		private class TPSHostRawUI : PSHostRawUserInterface
		{
			// Token: 0x06003578 RID: 13688 RVA: 0x000DC09E File Offset: 0x000DA29E
			public TPSHostRawUI(TestRemotePowerShellConnectivity.TPSHost owner, TestRemotePowerShellConnectivity.TPSHostUI ownerUI)
			{
			}

			// Token: 0x17000FEC RID: 4076
			// (get) Token: 0x06003579 RID: 13689 RVA: 0x000DC0A6 File Offset: 0x000DA2A6
			// (set) Token: 0x0600357A RID: 13690 RVA: 0x000DC0AA File Offset: 0x000DA2AA
			public override ConsoleColor ForegroundColor
			{
				get
				{
					return ConsoleColor.White;
				}
				set
				{
				}
			}

			// Token: 0x17000FED RID: 4077
			// (get) Token: 0x0600357B RID: 13691 RVA: 0x000DC0AC File Offset: 0x000DA2AC
			// (set) Token: 0x0600357C RID: 13692 RVA: 0x000DC0AF File Offset: 0x000DA2AF
			public override ConsoleColor BackgroundColor
			{
				get
				{
					return ConsoleColor.Black;
				}
				set
				{
				}
			}

			// Token: 0x17000FEE RID: 4078
			// (get) Token: 0x0600357D RID: 13693 RVA: 0x000DC0B1 File Offset: 0x000DA2B1
			// (set) Token: 0x0600357E RID: 13694 RVA: 0x000DC0BA File Offset: 0x000DA2BA
			public override Size BufferSize
			{
				get
				{
					return new Size(0, 0);
				}
				set
				{
				}
			}

			// Token: 0x17000FEF RID: 4079
			// (get) Token: 0x0600357F RID: 13695 RVA: 0x000DC0BC File Offset: 0x000DA2BC
			// (set) Token: 0x06003580 RID: 13696 RVA: 0x000DC0C5 File Offset: 0x000DA2C5
			public override Coordinates CursorPosition
			{
				get
				{
					return new Coordinates(0, 0);
				}
				set
				{
				}
			}

			// Token: 0x17000FF0 RID: 4080
			// (get) Token: 0x06003581 RID: 13697 RVA: 0x000DC0C7 File Offset: 0x000DA2C7
			// (set) Token: 0x06003582 RID: 13698 RVA: 0x000DC0D0 File Offset: 0x000DA2D0
			public override Coordinates WindowPosition
			{
				get
				{
					return new Coordinates(0, 0);
				}
				set
				{
				}
			}

			// Token: 0x17000FF1 RID: 4081
			// (get) Token: 0x06003583 RID: 13699 RVA: 0x000DC0D2 File Offset: 0x000DA2D2
			// (set) Token: 0x06003584 RID: 13700 RVA: 0x000DC0DB File Offset: 0x000DA2DB
			public override Size WindowSize
			{
				get
				{
					return new Size(0, 0);
				}
				set
				{
				}
			}

			// Token: 0x17000FF2 RID: 4082
			// (get) Token: 0x06003585 RID: 13701 RVA: 0x000DC0DD File Offset: 0x000DA2DD
			public override Size MaxWindowSize
			{
				get
				{
					return new Size(0, 0);
				}
			}

			// Token: 0x17000FF3 RID: 4083
			// (get) Token: 0x06003586 RID: 13702 RVA: 0x000DC0E6 File Offset: 0x000DA2E6
			public override Size MaxPhysicalWindowSize
			{
				get
				{
					return new Size(0, 0);
				}
			}

			// Token: 0x06003587 RID: 13703 RVA: 0x000DC0F0 File Offset: 0x000DA2F0
			public override KeyInfo ReadKey(ReadKeyOptions options)
			{
				return default(KeyInfo);
			}

			// Token: 0x17000FF4 RID: 4084
			// (get) Token: 0x06003588 RID: 13704 RVA: 0x000DC106 File Offset: 0x000DA306
			public override bool KeyAvailable
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000FF5 RID: 4085
			// (get) Token: 0x06003589 RID: 13705 RVA: 0x000DC109 File Offset: 0x000DA309
			// (set) Token: 0x0600358A RID: 13706 RVA: 0x000DC110 File Offset: 0x000DA310
			public override string WindowTitle
			{
				get
				{
					return string.Empty;
				}
				set
				{
				}
			}

			// Token: 0x0600358B RID: 13707 RVA: 0x000DC112 File Offset: 0x000DA312
			public override void SetBufferContents(Coordinates origin, BufferCell[,] contents)
			{
			}

			// Token: 0x0600358C RID: 13708 RVA: 0x000DC114 File Offset: 0x000DA314
			public override void SetBufferContents(Rectangle rectangle, BufferCell fill)
			{
			}

			// Token: 0x0600358D RID: 13709 RVA: 0x000DC116 File Offset: 0x000DA316
			public override BufferCell[,] GetBufferContents(Rectangle rectangle)
			{
				return null;
			}

			// Token: 0x0600358E RID: 13710 RVA: 0x000DC119 File Offset: 0x000DA319
			public override void ScrollBufferContents(Rectangle source, Coordinates destination, Rectangle clip, BufferCell fill)
			{
			}

			// Token: 0x17000FF6 RID: 4086
			// (get) Token: 0x0600358F RID: 13711 RVA: 0x000DC11B File Offset: 0x000DA31B
			// (set) Token: 0x06003590 RID: 13712 RVA: 0x000DC11E File Offset: 0x000DA31E
			public override int CursorSize
			{
				get
				{
					return 0;
				}
				set
				{
				}
			}

			// Token: 0x06003591 RID: 13713 RVA: 0x000DC120 File Offset: 0x000DA320
			public override void FlushInputBuffer()
			{
			}
		}
	}
}
