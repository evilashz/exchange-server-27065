using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Security;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Net.MonitoringWebClient;
using Microsoft.Exchange.Net.MonitoringWebClient.Owa;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x020005DE RID: 1502
	[Cmdlet("Test", "OwaConnectivity", SupportsShouldProcess = true, DefaultParameterSetName = "ClientAccessServer")]
	public class TestOwaConnectivity : TestWebApplicationConnectivity2
	{
		// Token: 0x0600352F RID: 13615 RVA: 0x000DAAF9 File Offset: 0x000D8CF9
		public TestOwaConnectivity() : base(Strings.CasHealthOwaLongName, Strings.CasHealthOwaShortName, "MSExchange Monitoring OWAConnectivity Internal", "MSExchange Monitoring OWAConnectivity External")
		{
		}

		// Token: 0x06003530 RID: 13616 RVA: 0x000DAB15 File Offset: 0x000D8D15
		internal TestOwaConnectivity(LocalizedString applicationName, LocalizedString applicationShortName, string monitoringEventSourceInternal, string monitoringEventSourceExternal) : base(applicationName, applicationShortName, monitoringEventSourceInternal, monitoringEventSourceExternal)
		{
		}

		// Token: 0x06003531 RID: 13617 RVA: 0x000DAB24 File Offset: 0x000D8D24
		private static void InitializeEndpointLists()
		{
			if (TestOwaConnectivity.consumerLiveIdHostNames == null)
			{
				lock (TestOwaConnectivity.lockObject)
				{
					if (TestOwaConnectivity.consumerLiveIdHostNames == null)
					{
						ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.FullyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 107, "InitializeEndpointLists", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Monitoring\\Tasks\\TestOwaConnectivity.cs");
						ServiceEndpointContainer endpointContainer = topologyConfigurationSession.GetEndpointContainer();
						TestOwaConnectivity.exchangeHostNames = new List<string>();
						TestOwaConnectivity.exchangeHostNames.Add(endpointContainer.GetEndpoint(ServiceEndpointId.ExchangeLoginUrl).Uri.Host);
						TestOwaConnectivity.businessLiveIdHostNames = new List<string>();
						TestOwaConnectivity.businessLiveIdHostNames.Add(endpointContainer.GetEndpoint(ServiceEndpointId.MsoServiceLogin2).Uri.Host);
						TestOwaConnectivity.consumerLiveIdHostNames = new List<string>();
						TestOwaConnectivity.consumerLiveIdHostNames.Add(endpointContainer.GetEndpoint(ServiceEndpointId.LiveServiceLogin2).Uri.Host);
					}
				}
			}
		}

		// Token: 0x06003532 RID: 13618 RVA: 0x000DAC10 File Offset: 0x000D8E10
		protected override IEnumerable<ExchangeVirtualDirectory> GetVirtualDirectories(ADObjectId serverId, QueryFilter filter)
		{
			filter = new AndFilter(new QueryFilter[]
			{
				filter,
				new ComparisonFilter(ComparisonOperator.NotEqual, ADOwaVirtualDirectorySchema.OwaVersion, OwaVersions.Exchange2003or2000)
			});
			return base.GetVirtualDirectories<ADOwaVirtualDirectory>(serverId, filter);
		}

		// Token: 0x06003533 RID: 13619 RVA: 0x000DAC4C File Offset: 0x000D8E4C
		internal override IExceptionAnalyzer CreateExceptionAnalyzer(Uri uri)
		{
			TestOwaConnectivity.InitializeEndpointLists();
			Dictionary<string, RequestTarget> dictionary = new Dictionary<string, RequestTarget>();
			dictionary.Add(uri.Host, RequestTarget.Owa);
			this.AddHostMapping(dictionary, RequestTarget.Owa, TestOwaConnectivity.exchangeHostNames);
			this.AddHostMapping(dictionary, RequestTarget.LiveIdConsumer, TestOwaConnectivity.consumerLiveIdHostNames);
			this.AddHostMapping(dictionary, RequestTarget.LiveIdBusiness, TestOwaConnectivity.businessLiveIdHostNames);
			return new OwaExceptionAnalyzer(dictionary);
		}

		// Token: 0x06003534 RID: 13620 RVA: 0x000DACA0 File Offset: 0x000D8EA0
		internal override ITestStep CreateScenario(TestCasConnectivity.TestCasConnectivityRunInstance instance, Uri testUri, string userName, string domain, SecureString password, VirtualDirectoryUriScope testType, string serverFqdn)
		{
			ITestFactory testFactory = new TestFactory();
			ITestStep result;
			if (testType == VirtualDirectoryUriScope.Internal || testType == VirtualDirectoryUriScope.Unknown)
			{
				result = testFactory.CreateOwaLoginScenario(testUri, userName, domain, password, new OwaLoginParameters(), testFactory);
			}
			else
			{
				result = testFactory.CreateOwaExternalLoginAgainstSpecificServerScenario(testUri, userName, domain, password, serverFqdn, testFactory);
			}
			return result;
		}

		// Token: 0x06003535 RID: 13621 RVA: 0x000DACE2 File Offset: 0x000D8EE2
		internal override void CompleteSuccessfulOutcome(CasTransactionOutcome outcome, TestCasConnectivity.TestCasConnectivityRunInstance instance, IResponseTracker responseTracker)
		{
			outcome.Update(CasTransactionResultEnum.Success);
			base.WriteMonitoringEvent(1000, this.MonitoringEventSource, EventTypeEnumeration.Success, Strings.CasHealthAllTransactionsSucceeded);
		}

		// Token: 0x06003536 RID: 13622 RVA: 0x000DAD08 File Offset: 0x000D8F08
		internal override void CompleteFailedOutcome(CasTransactionOutcome outcome, TestCasConnectivity.TestCasConnectivityRunInstance instance, IResponseTracker responseTracker, Exception e)
		{
			TestOwaConnectivityOutcome testOwaConnectivityOutcome = outcome as TestOwaConnectivityOutcome;
			ScenarioException scenarioException = e.GetScenarioException();
			if (scenarioException != null)
			{
				testOwaConnectivityOutcome.FailureSource = scenarioException.FailureSource.ToString();
				testOwaConnectivityOutcome.FailureReason = scenarioException.FailureReason.ToString();
				testOwaConnectivityOutcome.FailingComponent = scenarioException.FailingComponent.ToString();
			}
			testOwaConnectivityOutcome.Update(CasTransactionResultEnum.Failure, (scenarioException != null) ? scenarioException.Message.ToString() : e.Message.ToString(), EventTypeEnumeration.Error);
			base.WriteMonitoringEvent(1001, this.MonitoringEventSource, EventTypeEnumeration.Error, Strings.CasHealthTransactionFailures(this.FormatExceptionText(scenarioException, e)));
		}

		// Token: 0x06003537 RID: 13623 RVA: 0x000DADB4 File Offset: 0x000D8FB4
		internal override CasTransactionOutcome CreateOutcome(TestCasConnectivity.TestCasConnectivityRunInstance instance, Uri testUri, IResponseTracker responseTracker)
		{
			return new TestOwaConnectivityOutcome(instance.CasFqdn, (instance.exchangePrincipal == null) ? null : instance.exchangePrincipal.MailboxInfo.Location.ServerFqdn, Strings.CasHealthOwaLogonScenarioName, Strings.CasHealthOwaLogonScenarioDescription, "Logon Latency", base.LocalSiteName, instance.trustAllCertificates, instance.credentials.UserName, instance.VirtualDirectoryName, testUri, instance.UrlType)
			{
				HttpData = responseTracker.Items
			};
		}

		// Token: 0x06003538 RID: 13624 RVA: 0x000DAE38 File Offset: 0x000D9038
		private void AddHostMapping(Dictionary<string, RequestTarget> sourceMapping, RequestTarget source, List<string> hostNames)
		{
			foreach (string key in hostNames)
			{
				if (!sourceMapping.ContainsKey(key))
				{
					sourceMapping.Add(key, source);
				}
			}
		}

		// Token: 0x06003539 RID: 13625 RVA: 0x000DAE90 File Offset: 0x000D9090
		private string FormatExceptionText(ScenarioException scenarioException, Exception exception)
		{
			string str = exception.Message;
			if (scenarioException != null)
			{
				str = scenarioException.ToString();
			}
			return str + Environment.NewLine + this.verboseOutput.ToString();
		}

		// Token: 0x0400249B RID: 9371
		private const string monitoringEventSourceInternal = "MSExchange Monitoring OWAConnectivity Internal";

		// Token: 0x0400249C RID: 9372
		private const string monitoringEventSourceExternal = "MSExchange Monitoring OWAConnectivity External";

		// Token: 0x0400249D RID: 9373
		private const string MonitoringLatencyPerfCounter = "Logon Latency";

		// Token: 0x0400249E RID: 9374
		private static object lockObject = new object();

		// Token: 0x0400249F RID: 9375
		private static List<string> exchangeHostNames = null;

		// Token: 0x040024A0 RID: 9376
		private static List<string> businessLiveIdHostNames = null;

		// Token: 0x040024A1 RID: 9377
		private static List<string> consumerLiveIdHostNames = null;
	}
}
