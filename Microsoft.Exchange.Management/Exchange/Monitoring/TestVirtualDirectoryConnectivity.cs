using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Metabase;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x020005C8 RID: 1480
	public abstract class TestVirtualDirectoryConnectivity : TestCasConnectivity
	{
		// Token: 0x060033D9 RID: 13273 RVA: 0x000D1F36 File Offset: 0x000D0136
		internal TestVirtualDirectoryConnectivity(LocalizedString applicationName, LocalizedString applicationShortName, TransientErrorCache transientErrorCache, string monitoringEventSourceInternal, string monitoringEventSourceExternal)
		{
			this.ApplicationName = applicationName;
			this.ApplicationShortName = applicationShortName;
			this.transientErrorCache = transientErrorCache;
			this.monitoringEventSourceInternal = monitoringEventSourceInternal;
			this.monitoringEventSourceExternal = monitoringEventSourceExternal;
		}

		// Token: 0x17000F81 RID: 3969
		// (get) Token: 0x060033DA RID: 13274 RVA: 0x000D1F63 File Offset: 0x000D0163
		// (set) Token: 0x060033DB RID: 13275 RVA: 0x000D1F6B File Offset: 0x000D016B
		[Alias(new string[]
		{
			"Identity"
		})]
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = false, ParameterSetName = "Identity", ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, Position = 0)]
		public ServerIdParameter ClientAccessServer
		{
			get
			{
				return this.clientAccessServer;
			}
			set
			{
				this.clientAccessServer = value;
			}
		}

		// Token: 0x17000F82 RID: 3970
		// (get) Token: 0x060033DC RID: 13276 RVA: 0x000D1F74 File Offset: 0x000D0174
		// (set) Token: 0x060033DD RID: 13277 RVA: 0x000D1F81 File Offset: 0x000D0181
		[Parameter(Mandatory = false)]
		public SwitchParameter TrustAnySSLCertificate
		{
			get
			{
				return this.trustAllCertificates;
			}
			set
			{
				this.trustAllCertificates = value;
			}
		}

		// Token: 0x17000F83 RID: 3971
		// (get) Token: 0x060033DE RID: 13278 RVA: 0x000D1F8F File Offset: 0x000D018F
		// (set) Token: 0x060033DF RID: 13279 RVA: 0x000D1F97 File Offset: 0x000D0197
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public OwaConnectivityTestType TestType
		{
			get
			{
				return this.testType;
			}
			set
			{
				this.testType = value;
			}
		}

		// Token: 0x17000F84 RID: 3972
		// (get) Token: 0x060033E0 RID: 13280 RVA: 0x000D1FA0 File Offset: 0x000D01A0
		// (set) Token: 0x060033E1 RID: 13281 RVA: 0x000D1FA8 File Offset: 0x000D01A8
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public string VirtualDirectoryName { get; set; }

		// Token: 0x17000F85 RID: 3973
		// (get) Token: 0x060033E2 RID: 13282 RVA: 0x000D1FB1 File Offset: 0x000D01B1
		// (set) Token: 0x060033E3 RID: 13283 RVA: 0x000D1FB9 File Offset: 0x000D01B9
		private protected LocalizedString ApplicationName { protected get; private set; }

		// Token: 0x17000F86 RID: 3974
		// (get) Token: 0x060033E4 RID: 13284 RVA: 0x000D1FC2 File Offset: 0x000D01C2
		// (set) Token: 0x060033E5 RID: 13285 RVA: 0x000D1FCA File Offset: 0x000D01CA
		private protected LocalizedString ApplicationShortName { protected get; private set; }

		// Token: 0x17000F87 RID: 3975
		// (get) Token: 0x060033E6 RID: 13286 RVA: 0x000D1FD3 File Offset: 0x000D01D3
		protected bool UseInternalUrl
		{
			get
			{
				return this.TestType == OwaConnectivityTestType.Internal;
			}
		}

		// Token: 0x17000F88 RID: 3976
		// (get) Token: 0x060033E7 RID: 13287 RVA: 0x000D1FDE File Offset: 0x000D01DE
		protected sealed override string PerformanceObject
		{
			get
			{
				return this.MonitoringEventSource;
			}
		}

		// Token: 0x17000F89 RID: 3977
		// (get) Token: 0x060033E8 RID: 13288 RVA: 0x000D1FE6 File Offset: 0x000D01E6
		protected sealed override string MonitoringEventSource
		{
			get
			{
				if (!this.UseInternalUrl)
				{
					return this.monitoringEventSourceExternal;
				}
				return this.monitoringEventSourceInternal;
			}
		}

		// Token: 0x060033E9 RID: 13289 RVA: 0x000D1FFD File Offset: 0x000D01FD
		protected sealed override string TransactionFailuresEventMessage(string detailedInformation)
		{
			return Strings.CasHealthWebAppSomeTransactionsFailed(this.ApplicationShortName, detailedInformation);
		}

		// Token: 0x060033EA RID: 13290 RVA: 0x000D2010 File Offset: 0x000D0210
		protected sealed override string TransactionWarningsEventMessage(string detailedInformation)
		{
			return Strings.CasHealthWebAppTransactionWarnings(this.ApplicationShortName, detailedInformation);
		}

		// Token: 0x17000F8A RID: 3978
		// (get) Token: 0x060033EB RID: 13291 RVA: 0x000D2023 File Offset: 0x000D0223
		protected override string TransactionSuccessEventMessage
		{
			get
			{
				return Strings.CasHealthWebAppAllTransactionsSucceeded(this.ApplicationName);
			}
		}

		// Token: 0x060033EC RID: 13292 RVA: 0x000D2035 File Offset: 0x000D0235
		protected override uint GetDefaultTimeOut()
		{
			return 30U;
		}

		// Token: 0x060033ED RID: 13293 RVA: 0x000D2039 File Offset: 0x000D0239
		internal override TransientErrorCache GetTransientErrorCache()
		{
			if (!base.MonitoringContext)
			{
				return null;
			}
			return this.transientErrorCache;
		}

		// Token: 0x17000F8B RID: 3979
		// (get) Token: 0x060033EE RID: 13294 RVA: 0x000D2050 File Offset: 0x000D0250
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				string text = "";
				if (this.TrustAnySSLCertificate)
				{
					text = text + Strings.CasHealthWarnTrustAllCertificates + "\r\n";
				}
				if (this.casToTest != null)
				{
					text = text + Strings.CasHealthWebAppConfirmTestWithServer(this.ApplicationName, this.casToTest.Fqdn) + "\r\n";
				}
				return new LocalizedString(text);
			}
		}

		// Token: 0x060033EF RID: 13295 RVA: 0x000D20BC File Offset: 0x000D02BC
		protected sealed override void InternalValidate()
		{
			TaskLogger.LogEnter();
			try
			{
				base.InternalValidate();
				this.ValidateTestWebApplicationRequirements();
			}
			finally
			{
				if (base.HasErrors && base.MonitoringContext)
				{
					base.WriteObject(this.monitoringData);
				}
				TaskLogger.LogExit();
			}
		}

		// Token: 0x060033F0 RID: 13296 RVA: 0x000D2114 File Offset: 0x000D0314
		protected virtual void ValidateTestWebApplicationRequirements()
		{
			if (this.casToTest == null)
			{
				base.CasConnectivityWriteError(new ApplicationException(this.NoCasArgumentError), ErrorCategory.InvalidArgument, null);
			}
		}

		// Token: 0x17000F8C RID: 3980
		// (get) Token: 0x060033F1 RID: 13297 RVA: 0x000D2136 File Offset: 0x000D0336
		protected virtual LocalizedString NoCasArgumentError
		{
			get
			{
				return Strings.CasHealthWebAppNoCasArgumentError;
			}
		}

		// Token: 0x060033F2 RID: 13298 RVA: 0x000D2174 File Offset: 0x000D0374
		protected override List<TestCasConnectivity.TestCasConnectivityRunInstance> PopulateInfoPerCas(TestCasConnectivity.TestCasConnectivityRunInstance clientAccessServerInstance, List<CasTransactionOutcome> outcomeList)
		{
			TaskLogger.LogEnter();
			List<TestCasConnectivity.TestCasConnectivityRunInstance> result;
			try
			{
				base.WriteVerbose(Strings.CasHealthWebAppBuildVdirList(this.ApplicationName, clientAccessServerInstance.CasFqdn));
				IEnumerable<ExchangeVirtualDirectory> virtualDirectories = this.GetVirtualDirectories(this.casToTest.Id);
				result = (from virtualDirectory in virtualDirectories
				where this.ShouldTestVirtualDirectory(clientAccessServerInstance, outcomeList, virtualDirectory)
				select this.CreateRunInstanceForVirtualDirectory(clientAccessServerInstance, virtualDirectory)).ToList<TestCasConnectivity.TestCasConnectivityRunInstance>();
			}
			finally
			{
				TaskLogger.LogExit();
			}
			return result;
		}

		// Token: 0x060033F3 RID: 13299 RVA: 0x000D2224 File Offset: 0x000D0424
		private IEnumerable<ExchangeVirtualDirectory> GetVirtualDirectories(ADObjectId serverId)
		{
			base.TraceInfo("GetVirtualDirectories: server = {0}", new object[]
			{
				serverId
			});
			QueryFilter queryFilter = new ExistsFilter(ExchangeVirtualDirectorySchema.MetabasePath);
			if (this.VirtualDirectoryName != null)
			{
				queryFilter = new AndFilter(new QueryFilter[]
				{
					queryFilter,
					new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Name, this.VirtualDirectoryName)
				});
			}
			ExchangeVirtualDirectory[] array = this.GetVirtualDirectories(serverId, queryFilter).ToArray<ExchangeVirtualDirectory>();
			if (array.Length == 0)
			{
				if (this.VirtualDirectoryName != null)
				{
					base.WriteErrorAndMonitoringEvent(new ApplicationException(Strings.CasHealthWebAppVdirNotFoundError(this.ApplicationShortName, this.VirtualDirectoryName)), ErrorCategory.ObjectNotFound, null, 1010, this.MonitoringEventSource, true);
				}
				else
				{
					this.WriteWarning(Strings.CasHealthWebAppNoVirtualDirectories(this.ApplicationShortName, serverId.Name));
				}
			}
			return array;
		}

		// Token: 0x060033F4 RID: 13300
		protected abstract IEnumerable<ExchangeVirtualDirectory> GetVirtualDirectories(ADObjectId serverId, QueryFilter filter);

		// Token: 0x060033F5 RID: 13301 RVA: 0x000D22E8 File Offset: 0x000D04E8
		protected IEnumerable<ExchangeVirtualDirectory> GetVirtualDirectories<TExchangeVirtualDirectory>(ADObjectId serverId, QueryFilter filter) where TExchangeVirtualDirectory : ExchangeVirtualDirectory, new()
		{
			ADPagedReader<TExchangeVirtualDirectory> source = base.CasConfigurationSession.FindPaged<TExchangeVirtualDirectory>(serverId, QueryScope.SubTree, filter, null, 0);
			return source.Cast<ExchangeVirtualDirectory>();
		}

		// Token: 0x060033F6 RID: 13302 RVA: 0x000D230C File Offset: 0x000D050C
		private bool ShouldTestVirtualDirectory(TestCasConnectivity.TestCasConnectivityRunInstance clientAccessServerInstance, List<CasTransactionOutcome> outcomeList, ExchangeVirtualDirectory virtualDirectory)
		{
			if (this.IsOrphanVdir(virtualDirectory))
			{
				this.WriteWarning(Strings.CasHealthWebAppOrphanVirtualDirectory(this.ApplicationShortName, virtualDirectory.Name));
				CasTransactionOutcome casTransactionOutcome = this.CreateVirtualDirectoryConfigErrorOutcome(clientAccessServerInstance, virtualDirectory);
				this.TestFailedBadVdirConfig(casTransactionOutcome);
				base.WriteObject(casTransactionOutcome);
				outcomeList.Add(casTransactionOutcome);
				return false;
			}
			if ((this.UseInternalUrl && (virtualDirectory.InternalUrl == null || string.IsNullOrEmpty(virtualDirectory.InternalUrl.AbsoluteUri))) || (!this.UseInternalUrl && (virtualDirectory.ExternalUrl == null || string.IsNullOrEmpty(virtualDirectory.ExternalUrl.AbsoluteUri))))
			{
				CasTransactionOutcome casTransactionOutcome2 = this.CreateVirtualDirectoryConfigErrorOutcome(clientAccessServerInstance, virtualDirectory);
				this.TestFailedUrlPropertyNotSet(casTransactionOutcome2);
				base.WriteObject(casTransactionOutcome2);
				outcomeList.Add(casTransactionOutcome2);
				return false;
			}
			return true;
		}

		// Token: 0x060033F7 RID: 13303 RVA: 0x000D23CC File Offset: 0x000D05CC
		private bool IsOrphanVdir(ExchangeVirtualDirectory vdir)
		{
			string hostName = IisUtility.GetHostName(vdir.MetabasePath);
			if (string.IsNullOrEmpty(hostName))
			{
				base.TraceInfo("IsOrphanVdir: hostname from vdir.MetabasePath is null.");
				return false;
			}
			string text = TestVirtualDirectoryConnectivity.GetFirstPeriodDelimitedWord(Environment.MachineName).ToLower();
			base.TraceInfo("vdir hostname is {0}, localHost is {1}, vdir metabasepath: {2}", new object[]
			{
				hostName.ToLower(),
				text.ToLower(),
				vdir.MetabasePath
			});
			if (!TestVirtualDirectoryConnectivity.GetFirstPeriodDelimitedWord(hostName.ToLower()).Equals(text))
			{
				base.TraceInfo("IsOrphanVdir: Vdir is not on localhost, so can't check whether it is an orphan.");
				return false;
			}
			if (!IisUtility.Exists(vdir.MetabasePath))
			{
				base.TraceInfo("IsOrphanVdir: Vdir is an orphan.");
				return true;
			}
			base.TraceInfo("IsOrphanVdir: Vdir is not an orphan.");
			return false;
		}

		// Token: 0x060033F8 RID: 13304 RVA: 0x000D2480 File Offset: 0x000D0680
		private TestCasConnectivity.TestCasConnectivityRunInstance CreateRunInstanceForVirtualDirectory(TestCasConnectivity.TestCasConnectivityRunInstance clientAccessServerInstance, ExchangeVirtualDirectory virtualDirectory)
		{
			TestCasConnectivity.TestCasConnectivityRunInstance testCasConnectivityRunInstance = new TestCasConnectivity.TestCasConnectivityRunInstance(clientAccessServerInstance)
			{
				UrlType = (this.UseInternalUrl ? VirtualDirectoryUriScope.Internal : VirtualDirectoryUriScope.External),
				VirtualDirectory = virtualDirectory
			};
			base.WriteVerbose(Strings.CasHealthWebAppAddingTestInstance(clientAccessServerInstance.CasFqdn, testCasConnectivityRunInstance.baseUri.AbsoluteUri));
			return testCasConnectivityRunInstance;
		}

		// Token: 0x060033F9 RID: 13305 RVA: 0x000D24CC File Offset: 0x000D06CC
		protected override List<CasTransactionOutcome> BuildPerformanceOutcomes(TestCasConnectivity.TestCasConnectivityRunInstance instance, string mailboxFqdn)
		{
			return new List<CasTransactionOutcome>
			{
				this.CreateLogonOutcome(instance)
			};
		}

		// Token: 0x060033FA RID: 13306 RVA: 0x000D24ED File Offset: 0x000D06ED
		protected CasTransactionOutcome CreateLogonOutcome(TestCasConnectivity.TestCasConnectivityRunInstance instance)
		{
			return this.BuildOutcome(Strings.CasHealthWebAppLogonScenarioName, Strings.CasHealthWebAppLogonScenarioDescription(this.ApplicationName), instance);
		}

		// Token: 0x060033FB RID: 13307 RVA: 0x000D2510 File Offset: 0x000D0710
		protected virtual CasTransactionOutcome CreateVirtualDirectoryConfigErrorOutcome(TestCasConnectivity.TestCasConnectivityRunInstance clientAccessServerInstance, ExchangeVirtualDirectory virtualDirectory)
		{
			return new CasTransactionOutcome(clientAccessServerInstance.CasFqdn, Strings.CasHealthOwaLogonScenarioName, Strings.CasHealthWebAppLogonScenarioDescription(this.ApplicationName), null, base.LocalSiteName, !this.allowUnsecureAccess, clientAccessServerInstance.credentials.UserName, virtualDirectory.Name, null, this.UseInternalUrl ? VirtualDirectoryUriScope.Internal : VirtualDirectoryUriScope.External);
		}

		// Token: 0x060033FC RID: 13308 RVA: 0x000D2570 File Offset: 0x000D0770
		protected override CasTransactionOutcome BuildOutcome(string scenarioName, string scenarioDescription, TestCasConnectivity.TestCasConnectivityRunInstance instance)
		{
			return this.BuildOutcome(scenarioName, scenarioDescription, "Logon Latency", instance);
		}

		// Token: 0x060033FD RID: 13309 RVA: 0x000D2580 File Offset: 0x000D0780
		protected virtual CasTransactionOutcome BuildOutcome(string scenarioName, string scenarioDescription, string perfCounterName, TestCasConnectivity.TestCasConnectivityRunInstance instance)
		{
			return new CasTransactionOutcome(instance.CasFqdn, scenarioName, scenarioDescription, perfCounterName, base.LocalSiteName, !instance.allowUnsecureAccess, instance.credentials.UserName, instance.VirtualDirectoryName, instance.baseUri, instance.UrlType)
			{
				Result = 
				{
					Value = CasTransactionResultEnum.Success
				}
			};
		}

		// Token: 0x060033FE RID: 13310 RVA: 0x000D25DC File Offset: 0x000D07DC
		protected static string GetFirstPeriodDelimitedWord(string s)
		{
			if (string.IsNullOrEmpty(s))
			{
				return s;
			}
			int num = s.IndexOf('.');
			if (num >= 0)
			{
				return s.Substring(0, num);
			}
			return s;
		}

		// Token: 0x060033FF RID: 13311 RVA: 0x000D260C File Offset: 0x000D080C
		private void TestFailedBadVdirConfig(CasTransactionOutcome outcome)
		{
			string text = Strings.CasHealthWebAppBadVdirConfig(this.ApplicationShortName);
			if (base.MonitoringContext)
			{
				text += this.BuildVdirIdentityString(outcome);
			}
			base.SetTestOutcome(outcome, EventTypeEnumeration.Warning, text, null);
		}

		// Token: 0x06003400 RID: 13312 RVA: 0x000D2650 File Offset: 0x000D0850
		private void TestFailedUrlPropertyNotSet(CasTransactionOutcome outcome)
		{
			string text = (outcome.UrlType == VirtualDirectoryUriScope.Internal) ? Strings.CasHealthOwaNoInternalUrl : Strings.CasHealthOwaNoExternalUrl;
			if (base.MonitoringContext)
			{
				text += this.BuildVdirIdentityString(outcome);
			}
			base.SetTestOutcome(outcome, EventTypeEnumeration.Warning, text, null);
		}

		// Token: 0x06003401 RID: 13313 RVA: 0x000D26A0 File Offset: 0x000D08A0
		protected string BuildVdirIdentityString(CasTransactionOutcome outcome)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append(Strings.CasHealthOwaVdirColon(outcome.VirtualDirectoryName));
			return stringBuilder.ToString();
		}

		// Token: 0x040023F7 RID: 9207
		private const uint DefaultTimeout = 30U;

		// Token: 0x040023F8 RID: 9208
		private const string MonitoringLatencyPerfCounter = "Logon Latency";

		// Token: 0x040023F9 RID: 9209
		private readonly TransientErrorCache transientErrorCache;

		// Token: 0x040023FA RID: 9210
		private readonly string monitoringEventSourceInternal;

		// Token: 0x040023FB RID: 9211
		private readonly string monitoringEventSourceExternal;

		// Token: 0x040023FC RID: 9212
		private OwaConnectivityTestType testType;
	}
}
