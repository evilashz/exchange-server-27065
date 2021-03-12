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
using Microsoft.Exchange.Net.MonitoringWebClient.Ecp;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x020005CF RID: 1487
	[Cmdlet("Test", "EcpConnectivity2", SupportsShouldProcess = true, DefaultParameterSetName = "ClientAccessServer")]
	public class TestEcpConnectivity2 : TestWebApplicationConnectivity2
	{
		// Token: 0x06003449 RID: 13385 RVA: 0x000D3BED File Offset: 0x000D1DED
		public TestEcpConnectivity2() : base(Strings.CasHealthEcpLongName, Strings.CasHealthEcpShortName, "MSExchange Monitoring ECPConnectivity2 Internal", "MSExchange Monitoring ECPConnectivity2 External")
		{
		}

		// Token: 0x0600344A RID: 13386 RVA: 0x000D3C09 File Offset: 0x000D1E09
		internal TestEcpConnectivity2(LocalizedString applicationName, LocalizedString applicationShortName, string monitoringEventSourceInternal, string monitoringEventSourceExternal) : base(applicationName, applicationShortName, monitoringEventSourceInternal, monitoringEventSourceExternal)
		{
		}

		// Token: 0x0600344B RID: 13387 RVA: 0x000D3C18 File Offset: 0x000D1E18
		private static void InitializeEndpointLists()
		{
			if (TestEcpConnectivity2.consumerLiveIdHostNames == null)
			{
				lock (TestEcpConnectivity2.lockObject)
				{
					if (TestEcpConnectivity2.consumerLiveIdHostNames == null)
					{
						ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.FullyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 102, "InitializeEndpointLists", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Monitoring\\Tasks\\TestEcpConnectivity2.cs");
						ServiceEndpointContainer endpointContainer = topologyConfigurationSession.GetEndpointContainer();
						TestEcpConnectivity2.exchangeHostNames = new List<string>();
						TestEcpConnectivity2.exchangeHostNames.Add(endpointContainer.GetEndpoint(ServiceEndpointId.ExchangeLoginUrl).Uri.Host);
						TestEcpConnectivity2.businessLiveIdHostNames = new List<string>();
						TestEcpConnectivity2.businessLiveIdHostNames.Add(endpointContainer.GetEndpoint(ServiceEndpointId.MsoServiceLogin2).Uri.Host);
						TestEcpConnectivity2.consumerLiveIdHostNames = new List<string>();
						TestEcpConnectivity2.consumerLiveIdHostNames.Add(endpointContainer.GetEndpoint(ServiceEndpointId.LiveServiceLogin2).Uri.Host);
					}
				}
			}
		}

		// Token: 0x0600344C RID: 13388 RVA: 0x000D3D04 File Offset: 0x000D1F04
		protected override IEnumerable<ExchangeVirtualDirectory> GetVirtualDirectories(ADObjectId serverId, QueryFilter filter)
		{
			return base.GetVirtualDirectories<ADEcpVirtualDirectory>(serverId, filter);
		}

		// Token: 0x0600344D RID: 13389 RVA: 0x000D3D10 File Offset: 0x000D1F10
		internal override IExceptionAnalyzer CreateExceptionAnalyzer(Uri uri)
		{
			TestEcpConnectivity2.InitializeEndpointLists();
			Dictionary<string, RequestTarget> dictionary = new Dictionary<string, RequestTarget>();
			dictionary.Add(uri.Host, RequestTarget.Ecp);
			this.AddHostMapping(dictionary, RequestTarget.Ecp, TestEcpConnectivity2.exchangeHostNames);
			this.AddHostMapping(dictionary, RequestTarget.LiveIdConsumer, TestEcpConnectivity2.consumerLiveIdHostNames);
			this.AddHostMapping(dictionary, RequestTarget.LiveIdBusiness, TestEcpConnectivity2.businessLiveIdHostNames);
			return new EcpExceptionAnalyzer(dictionary);
		}

		// Token: 0x0600344E RID: 13390 RVA: 0x000D3D64 File Offset: 0x000D1F64
		internal override ITestStep CreateScenario(TestCasConnectivity.TestCasConnectivityRunInstance instance, Uri testUri, string userName, string domain, SecureString password, VirtualDirectoryUriScope testType, string serverFqdn)
		{
			ITestFactory testFactory = new TestFactory();
			ITestStep result;
			if (testType == VirtualDirectoryUriScope.Internal || testType == VirtualDirectoryUriScope.Unknown)
			{
				result = testFactory.CreateEcpLoginScenario(testUri, userName, domain, password, testFactory);
			}
			else
			{
				result = testFactory.CreateEcpExternalLoginAgainstSpecificServerScenario(testUri, userName, domain, password, serverFqdn, testFactory);
			}
			return result;
		}

		// Token: 0x0600344F RID: 13391 RVA: 0x000D3DA3 File Offset: 0x000D1FA3
		internal override void CompleteSuccessfulOutcome(CasTransactionOutcome outcome, TestCasConnectivity.TestCasConnectivityRunInstance instance, IResponseTracker responseTracker)
		{
			outcome.Update(CasTransactionResultEnum.Success);
			base.WriteMonitoringEvent(1000, this.MonitoringEventSource, EventTypeEnumeration.Success, Strings.CasHealthAllTransactionsSucceeded);
		}

		// Token: 0x06003450 RID: 13392 RVA: 0x000D3DC8 File Offset: 0x000D1FC8
		internal override void CompleteFailedOutcome(CasTransactionOutcome outcome, TestCasConnectivity.TestCasConnectivityRunInstance instance, IResponseTracker responseTracker, Exception e)
		{
			TestEcpConnectivityOutcome testEcpConnectivityOutcome = outcome as TestEcpConnectivityOutcome;
			ScenarioException scenarioException = e.GetScenarioException();
			if (scenarioException != null)
			{
				testEcpConnectivityOutcome.FailureSource = scenarioException.FailureSource.ToString();
				testEcpConnectivityOutcome.FailureReason = scenarioException.FailureReason.ToString();
				testEcpConnectivityOutcome.FailingComponent = scenarioException.FailingComponent.ToString();
			}
			testEcpConnectivityOutcome.Update(CasTransactionResultEnum.Failure, (scenarioException != null) ? scenarioException.Message.ToString() : e.Message.ToString(), EventTypeEnumeration.Error);
			base.WriteMonitoringEvent(1001, this.MonitoringEventSource, EventTypeEnumeration.Error, Strings.CasHealthTransactionFailures((scenarioException != null) ? scenarioException.Message.ToString() : e.Message.ToString()));
		}

		// Token: 0x06003451 RID: 13393 RVA: 0x000D3E88 File Offset: 0x000D2088
		internal override CasTransactionOutcome CreateOutcome(TestCasConnectivity.TestCasConnectivityRunInstance instance, Uri testUri, IResponseTracker responseTracker)
		{
			return new TestEcpConnectivityOutcome(instance.CasFqdn, (instance.exchangePrincipal == null) ? null : instance.exchangePrincipal.MailboxInfo.Location.ServerFqdn, Strings.CasHealthEcpScenarioTestWebService, Strings.CasHealthEcpScenarioTestWebServiceDescription, "Logon Latency", base.LocalSiteName, instance.trustAllCertificates, instance.credentials.UserName, instance.VirtualDirectoryName, testUri, instance.UrlType)
			{
				HttpData = responseTracker.Items
			};
		}

		// Token: 0x06003452 RID: 13394 RVA: 0x000D3F0C File Offset: 0x000D210C
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

		// Token: 0x04002429 RID: 9257
		private const string monitoringEventSourceInternal = "MSExchange Monitoring ECPConnectivity2 Internal";

		// Token: 0x0400242A RID: 9258
		private const string monitoringEventSourceExternal = "MSExchange Monitoring ECPConnectivity2 External";

		// Token: 0x0400242B RID: 9259
		private const string MonitoringLatencyPerfCounter = "Logon Latency";

		// Token: 0x0400242C RID: 9260
		private static object lockObject = new object();

		// Token: 0x0400242D RID: 9261
		private static List<string> exchangeHostNames = null;

		// Token: 0x0400242E RID: 9262
		private static List<string> businessLiveIdHostNames = null;

		// Token: 0x0400242F RID: 9263
		private static List<string> consumerLiveIdHostNames = null;
	}
}
