using System;
using System.Collections;
using System.Collections.Generic;
using System.Management.Automation;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Principal;
using System.Text;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Monitoring;
using Microsoft.Exchange.Diagnostics.Components.Tasks;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Extensions;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.EventMessages;
using Microsoft.Exchange.Management.SystemConfigurationTasks;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Net.LiveIDAuthentication;
using Microsoft.Exchange.Win32;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000515 RID: 1301
	public abstract class TestCasConnectivity : Task
	{
		// Token: 0x17000DDD RID: 3549
		// (get) Token: 0x06002EA2 RID: 11938 RVA: 0x000BABB8 File Offset: 0x000B8DB8
		protected string CasFqdn
		{
			get
			{
				if (this.casToTest == null)
				{
					return null;
				}
				return this.casToTest.Fqdn;
			}
		}

		// Token: 0x17000DDE RID: 3550
		// (get) Token: 0x06002EA3 RID: 11939 RVA: 0x000BABCF File Offset: 0x000B8DCF
		protected virtual DatacenterUserType DefaultUserType
		{
			get
			{
				return DatacenterUserType.LEGACY;
			}
		}

		// Token: 0x06002EA4 RID: 11940 RVA: 0x000BABD2 File Offset: 0x000B8DD2
		protected void CasConnectivityWriteError(Exception exception, ErrorCategory category, object target)
		{
			this.CasConnectivityWriteError(exception, category, target, true);
		}

		// Token: 0x06002EA5 RID: 11941 RVA: 0x000BABDE File Offset: 0x000B8DDE
		protected void CasConnectivityWriteError(Exception exception, ErrorCategory category, object target, bool reThrow)
		{
			if (!this.MonitoringContext || reThrow)
			{
				this.WriteError(exception, category, target, reThrow);
			}
		}

		// Token: 0x06002EA6 RID: 11942 RVA: 0x000BABFC File Offset: 0x000B8DFC
		protected string ShortErrorMsgFromException(Exception exception)
		{
			string result = string.Empty;
			if (exception.InnerException != null && !(exception is StorageTransientException) && !(exception is StoragePermanentException))
			{
				result = Strings.CasHealthShortErrorMsgFromExceptionWithInnerException(exception.GetType().ToString(), exception.Message, exception.InnerException.GetType().ToString(), exception.InnerException.Message);
			}
			else
			{
				result = Strings.CasHealthShortErrorMsgFromException(exception.GetType().ToString(), exception.Message);
			}
			return result;
		}

		// Token: 0x06002EA7 RID: 11943 RVA: 0x000BAC7D File Offset: 0x000B8E7D
		protected virtual CasTransactionOutcome BuildOutcome(string scenarioName, string scenarioDescription, TestCasConnectivity.TestCasConnectivityRunInstance instance)
		{
			return new CasTransactionOutcome(instance.CasFqdn, scenarioName, scenarioDescription, null, this.LocalSiteName, true, instance.credentials.UserName);
		}

		// Token: 0x06002EA8 RID: 11944 RVA: 0x000BACA0 File Offset: 0x000B8EA0
		protected LocalizedException ResetAutomatedCredentialsOnMailboxServer(Server server, bool onlyResetIfLocalMailbox)
		{
			bool flag = TestConnectivityCredentialsManager.IsExchangeMultiTenant();
			ADSite localSite = this.configurationSession.GetLocalSite();
			TestCasConnectivity.TestCasConnectivityRunInstance testCasConnectivityRunInstance = new TestCasConnectivity.TestCasConnectivityRunInstance();
			testCasConnectivityRunInstance.credentials = new NetworkCredential("", "", server.Domain);
			SmtpAddress? address;
			if (flag)
			{
				address = TestConnectivityCredentialsManager.GetMultiTenantAutomatedTaskUser(this, this.configurationSession, localSite);
			}
			else
			{
				address = TestConnectivityCredentialsManager.GetEnterpriseAutomatedTaskUser(localSite, server.Domain);
			}
			if (address == null)
			{
				return new CasHealthCouldNotBuildTestUserNameException(this.localServer.Fqdn);
			}
			testCasConnectivityRunInstance.credentials.UserName = TestCasConnectivity.GetInstanceUserNameFromTestUser(address);
			LocalizedException ex = this.SetExchangePrincipal(testCasConnectivityRunInstance);
			ex = this.ProcessSetExchangePrincipalResult(ex, testCasConnectivityRunInstance, server);
			if (ex != null)
			{
				return ex;
			}
			if (!flag)
			{
				if (onlyResetIfLocalMailbox)
				{
					this.TraceInfo("Checking if test user's mailbox is on current server: " + server.Fqdn);
					string serverFqdn = testCasConnectivityRunInstance.exchangePrincipal.MailboxInfo.Location.ServerFqdn;
					if (!serverFqdn.Equals(server.Fqdn, StringComparison.InvariantCultureIgnoreCase))
					{
						this.TraceInfo("Test user's mailbox is on " + serverFqdn + " skip restting password");
						return null;
					}
				}
				return this.ResetAutomatedCredentialsAndVerify(testCasConnectivityRunInstance);
			}
			if (this.mailboxCredential == null)
			{
				return new CasHealthCouldNotBuildTestUserNameException(this.localServer.Fqdn);
			}
			testCasConnectivityRunInstance.credentials.UserName = this.mailboxCredential.UserName;
			testCasConnectivityRunInstance.credentials.Password = this.mailboxCredential.Password.AsUnsecureString();
			return this.SaveAutomatedCredentials(testCasConnectivityRunInstance);
		}

		// Token: 0x06002EA9 RID: 11945 RVA: 0x000BADFC File Offset: 0x000B8FFC
		protected LocalizedException SaveAutomatedCredentials(TestCasConnectivity.TestCasConnectivityRunInstance instance)
		{
			ExDateTime now = ExDateTime.Now;
			string scenarioDescription = Strings.CasHealthResetCredentialsScenario(instance.exchangePrincipal.MailboxInfo.Location.ServerFqdn);
			CasTransactionOutcome casTransactionOutcome = this.BuildOutcome(Strings.CasHealthScenarioResetCredentials, scenarioDescription, instance);
			LocalizedException ex = TestConnectivityCredentialsManager.SaveAutomatedCredentials(instance.exchangePrincipal, instance.credentials);
			if (ex == null)
			{
				casTransactionOutcome.Update(CasTransactionResultEnum.Success, this.ComputeLatency(now), null);
				base.WriteObject(casTransactionOutcome);
			}
			else
			{
				casTransactionOutcome.Update(CasTransactionResultEnum.Failure, this.ComputeLatency(now), this.ShortErrorMsgFromException(ex));
				base.WriteObject(casTransactionOutcome);
			}
			instance.safeToBuildUpnString = true;
			return ex;
		}

		// Token: 0x06002EAA RID: 11946 RVA: 0x000BAE94 File Offset: 0x000B9094
		protected LocalizedException ResetAutomatedCredentialsAndVerify(TestCasConnectivity.TestCasConnectivityRunInstance instance)
		{
			ExDateTime now = ExDateTime.Now;
			string scenarioDescription = Strings.CasHealthResetCredentialsScenario(instance.exchangePrincipal.MailboxInfo.Location.ServerFqdn);
			CasTransactionOutcome casTransactionOutcome = this.BuildOutcome(Strings.CasHealthScenarioResetCredentials, scenarioDescription, instance);
			bool flag = false;
			LocalizedException ex = TestConnectivityCredentialsManager.ResetAutomatedCredentialsAndVerify(instance.exchangePrincipal, instance.credentials, this.ResetTestAccountCredentials, out flag);
			if (ex == null)
			{
				if (flag)
				{
					casTransactionOutcome.Update(CasTransactionResultEnum.Success, this.ComputeLatency(now), null);
					base.WriteObject(casTransactionOutcome);
				}
			}
			else
			{
				casTransactionOutcome.Update(CasTransactionResultEnum.Failure, this.ComputeLatency(now), this.ShortErrorMsgFromException(ex));
				base.WriteObject(casTransactionOutcome);
			}
			instance.safeToBuildUpnString = true;
			return ex;
		}

		// Token: 0x06002EAB RID: 11947 RVA: 0x000BAF44 File Offset: 0x000B9144
		protected LocalizedException LoadAutomatedCredentials(TestCasConnectivity.TestCasConnectivityRunInstance instance)
		{
			ExDateTime now = ExDateTime.Now;
			string scenarioDescription = Strings.CasHealthResetCredentialsScenario(instance.exchangePrincipal.MailboxInfo.Location.ServerFqdn);
			CasTransactionOutcome casTransactionOutcome = this.BuildOutcome(Strings.CasHealthScenarioResetCredentials, scenarioDescription, instance);
			LocalizedException ex = TestConnectivityCredentialsManager.LoadAutomatedTestCasConnectivityInfo(instance.exchangePrincipal, instance.credentials);
			if (ex != null)
			{
				casTransactionOutcome.Update(CasTransactionResultEnum.Failure, this.ComputeLatency(now), this.ShortErrorMsgFromException(ex));
				base.WriteObject(casTransactionOutcome);
			}
			instance.safeToBuildUpnString = true;
			return ex;
		}

		// Token: 0x06002EAC RID: 11948 RVA: 0x000BAFC3 File Offset: 0x000B91C3
		protected virtual List<CasTransactionOutcome> DoAutodiscoverCas(TestCasConnectivity.TestCasConnectivityRunInstance instance, List<TestCasConnectivity.TestCasConnectivityRunInstance> newInstances)
		{
			throw new NotImplementedException("DoAutodiscoverCas");
		}

		// Token: 0x17000DDF RID: 3551
		// (get) Token: 0x06002EAD RID: 11949 RVA: 0x000BAFCF File Offset: 0x000B91CF
		protected virtual string PerformanceObject
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000DE0 RID: 3552
		// (get) Token: 0x06002EAE RID: 11950 RVA: 0x000BAFD6 File Offset: 0x000B91D6
		protected virtual string MonitoringEventSource
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06002EAF RID: 11951 RVA: 0x000BAFDD File Offset: 0x000B91DD
		protected virtual string TransactionFailuresEventMessage(string detailedInformation)
		{
			return Strings.CasHealthTransactionFailures(detailedInformation);
		}

		// Token: 0x06002EB0 RID: 11952 RVA: 0x000BAFEA File Offset: 0x000B91EA
		protected virtual string TransactionWarningsEventMessage(string detailedInformation)
		{
			return Strings.CasHealthTransactionWarnings(detailedInformation);
		}

		// Token: 0x06002EB1 RID: 11953
		protected abstract uint GetDefaultTimeOut();

		// Token: 0x17000DE1 RID: 3553
		// (get) Token: 0x06002EB2 RID: 11954 RVA: 0x000BAFF7 File Offset: 0x000B91F7
		protected virtual string TransactionSuccessEventMessage
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06002EB3 RID: 11955 RVA: 0x000BAFFE File Offset: 0x000B91FE
		protected virtual List<TestCasConnectivity.TestCasConnectivityRunInstance> PopulateInfoPerCas(TestCasConnectivity.TestCasConnectivityRunInstance instance, List<CasTransactionOutcome> outcomeList)
		{
			throw new NotImplementedException("PopulateInfoPerCas");
		}

		// Token: 0x06002EB4 RID: 11956 RVA: 0x000BB00A File Offset: 0x000B920A
		protected virtual List<CasTransactionOutcome> ExecuteTests(TestCasConnectivity.TestCasConnectivityRunInstance instance)
		{
			throw new NotImplementedException("ExecuteTests");
		}

		// Token: 0x06002EB5 RID: 11957 RVA: 0x000BB016 File Offset: 0x000B9216
		internal virtual TransientErrorCache GetTransientErrorCache()
		{
			return null;
		}

		// Token: 0x06002EB6 RID: 11958 RVA: 0x000BB01C File Offset: 0x000B921C
		internal virtual void UpdateTransientErrorCache(TestCasConnectivity.TestCasConnectivityRunInstance instance, int cFailed, int cWarning, ref int cFailedTransactions, ref int cWarningTransactions, StringBuilder failedStr, StringBuilder warningStr, ref StringBuilder failedTransactionsStr, ref StringBuilder warningTransactionsStr)
		{
			TransientErrorCache transientErrorCache = this.GetTransientErrorCache();
			string text = (instance.exchangePrincipal != null) ? instance.exchangePrincipal.MailboxInfo.Location.ServerFqdn : null;
			if (cFailed > 0 || cWarning > 0)
			{
				if (transientErrorCache != null && !string.IsNullOrEmpty(text) && !transientErrorCache.ContainsError(text))
				{
					ExManagementApplicationLogger.LogEvent(ManagementEventLogConstants.Tuple_TransientErrorCacheInsertEntry, new string[]
					{
						(null != instance.baseUri) ? instance.baseUri.ToString() : string.Empty,
						text,
						failedStr.ToString(),
						warningStr.ToString()
					});
					transientErrorCache.Add(new CASServiceError(text));
					return;
				}
				ExManagementApplicationLogger.LogEvent(ManagementEventLogConstants.Tuple_TransientErrorCacheFindEntry, new string[]
				{
					(null != instance.baseUri) ? instance.baseUri.ToString() : string.Empty,
					text,
					failedStr.ToString(),
					warningStr.ToString()
				});
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
			else if (transientErrorCache != null && !string.IsNullOrEmpty(text))
			{
				if (transientErrorCache.ContainsError(text))
				{
					ExManagementApplicationLogger.LogEvent(ManagementEventLogConstants.Tuple_TransientErrorCacheRemoveEntry, new string[]
					{
						(null != instance.baseUri) ? instance.baseUri.ToString() : string.Empty,
						text
					});
				}
				transientErrorCache.Remove(text);
			}
		}

		// Token: 0x06002EB7 RID: 11959 RVA: 0x000BB1A0 File Offset: 0x000B93A0
		private IAsyncResult BeginExecute(TestCasConnectivity.TestCasConnectivityRunInstance instance)
		{
			instance.Result = new AsyncResult<CasTransactionOutcome>();
			this.InvokeExecuteTests(instance);
			if (!this.MonitoringContext)
			{
				uint seconds = (this.Timeout <= 0U || this.Timeout > 3600U) ? this.GetDefaultTimeOut() : this.Timeout;
				this.WaitAll(new IAsyncResult[]
				{
					instance.Result
				}, ExDateTime.Now.Add(new TimeSpan(0, 0, (int)seconds)), instance.Outcomes);
			}
			return instance.Result;
		}

		// Token: 0x06002EB8 RID: 11960 RVA: 0x000BB22C File Offset: 0x000B942C
		private void InvokeExecuteTests(object state)
		{
			TestCasConnectivity.TestCasConnectivityRunInstance instance = state as TestCasConnectivity.TestCasConnectivityRunInstance;
			this.ExecuteTests(instance);
		}

		// Token: 0x17000DE2 RID: 3554
		// (get) Token: 0x06002EB9 RID: 11961 RVA: 0x000BB248 File Offset: 0x000B9448
		// (set) Token: 0x06002EBA RID: 11962 RVA: 0x000BB25F File Offset: 0x000B945F
		[Parameter]
		public Fqdn DomainController
		{
			get
			{
				return (Fqdn)base.Fields["DomainController"];
			}
			set
			{
				base.Fields["DomainController"] = value;
			}
		}

		// Token: 0x17000DE3 RID: 3555
		// (get) Token: 0x06002EBB RID: 11963 RVA: 0x000BB272 File Offset: 0x000B9472
		// (set) Token: 0x06002EBC RID: 11964 RVA: 0x000BB298 File Offset: 0x000B9498
		[Parameter(Mandatory = false)]
		public SwitchParameter ResetTestAccountCredentials
		{
			get
			{
				return (bool)(base.Fields["ResetTestAccountCredentials"] ?? false);
			}
			set
			{
				base.Fields["ResetTestAccountCredentials"] = value.IsPresent;
			}
		}

		// Token: 0x17000DE4 RID: 3556
		// (get) Token: 0x06002EBD RID: 11965 RVA: 0x000BB2B6 File Offset: 0x000B94B6
		// (set) Token: 0x06002EBE RID: 11966 RVA: 0x000BB2DC File Offset: 0x000B94DC
		[Parameter(Mandatory = false)]
		public SwitchParameter MonitoringContext
		{
			get
			{
				return (bool)(base.Fields["MonitoringContext"] ?? false);
			}
			set
			{
				base.Fields["MonitoringContext"] = value.IsPresent;
			}
		}

		// Token: 0x17000DE5 RID: 3557
		// (get) Token: 0x06002EBF RID: 11967 RVA: 0x000BB2FA File Offset: 0x000B94FA
		// (set) Token: 0x06002EC0 RID: 11968 RVA: 0x000BB320 File Offset: 0x000B9520
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

		// Token: 0x17000DE6 RID: 3558
		// (get) Token: 0x06002EC1 RID: 11969 RVA: 0x000BB33E File Offset: 0x000B953E
		// (set) Token: 0x06002EC2 RID: 11970 RVA: 0x000BB346 File Offset: 0x000B9546
		[Parameter(Mandatory = false)]
		public uint Timeout
		{
			get
			{
				return this.timeout;
			}
			set
			{
				this.timeout = value;
			}
		}

		// Token: 0x17000DE7 RID: 3559
		// (get) Token: 0x06002EC3 RID: 11971 RVA: 0x000BB34F File Offset: 0x000B954F
		// (set) Token: 0x06002EC4 RID: 11972 RVA: 0x000BB357 File Offset: 0x000B9557
		[Parameter(Mandatory = false)]
		[ValidateNotNullOrEmpty]
		public ServerIdParameter MailboxServer
		{
			get
			{
				return this.mailboxServer;
			}
			set
			{
				this.mailboxServer = value;
			}
		}

		// Token: 0x17000DE8 RID: 3560
		// (get) Token: 0x06002EC5 RID: 11973 RVA: 0x000BB360 File Offset: 0x000B9560
		// (set) Token: 0x06002EC6 RID: 11974 RVA: 0x000BB386 File Offset: 0x000B9586
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = false)]
		public DatacenterUserType UserType
		{
			get
			{
				return (DatacenterUserType)(base.Fields["UserType"] ?? this.DefaultUserType);
			}
			set
			{
				base.Fields["UserType"] = value;
			}
		}

		// Token: 0x06002EC7 RID: 11975 RVA: 0x000BB39E File Offset: 0x000B959E
		protected override bool IsKnownException(Exception exception)
		{
			return base.IsKnownException(exception) || DataAccessHelper.IsDataAccessKnownException(exception) || MonitoringHelper.IsKnownExceptionForMonitoring(exception);
		}

		// Token: 0x17000DE9 RID: 3561
		// (get) Token: 0x06002EC8 RID: 11976 RVA: 0x000BB3B9 File Offset: 0x000B95B9
		internal ITopologyConfigurationSession CasConfigurationSession
		{
			get
			{
				return this.configurationSession;
			}
		}

		// Token: 0x17000DEA RID: 3562
		// (get) Token: 0x06002EC9 RID: 11977 RVA: 0x000BB3C1 File Offset: 0x000B95C1
		internal IRecipientSession CasGlobalCatalogSession
		{
			get
			{
				return this.globalCatalogSession;
			}
		}

		// Token: 0x17000DEB RID: 3563
		// (get) Token: 0x06002ECA RID: 11978 RVA: 0x000BB3C9 File Offset: 0x000B95C9
		internal string LocalSiteName
		{
			get
			{
				return this.CasConfigurationSession.GetLocalSite().Name;
			}
		}

		// Token: 0x06002ECB RID: 11979 RVA: 0x000BB3DC File Offset: 0x000B95DC
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			base.InternalBeginProcessing();
			ADSessionSettings sessionSettings = ADSessionSettings.FromRootOrgScopeSet();
			this.configurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(this.DomainController, false, ConsistencyMode.PartiallyConsistent, sessionSettings, 1218, "InternalBeginProcessing", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Monitoring\\Cas\\TestCasConnectivity.cs");
			this.configurationSession.ServerTimeout = new TimeSpan?(new TimeSpan(0, 0, 30000));
			this.globalCatalogSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(this.DomainController, true, ConsistencyMode.IgnoreInvalid, sessionSettings, 1226, "InternalBeginProcessing", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Monitoring\\Cas\\TestCasConnectivity.cs");
			if (!this.globalCatalogSession.IsReadConnectionAvailable())
			{
				this.globalCatalogSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(ConsistencyMode.IgnoreInvalid, sessionSettings, 1234, "InternalBeginProcessing", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Monitoring\\Cas\\TestCasConnectivity.cs");
			}
			this.globalCatalogSession.ServerTimeout = new TimeSpan?(new TimeSpan(0, 0, 30000));
			TaskLogger.LogExit();
		}

		// Token: 0x06002ECC RID: 11980 RVA: 0x000BB4C0 File Offset: 0x000B96C0
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			try
			{
				this.InitializeTopologyInformation();
			}
			catch (ADTransientException exception)
			{
				this.CasConnectivityWriteError(exception, ErrorCategory.ObjectNotFound, null);
			}
			finally
			{
				if (base.HasErrors && this.MonitoringContext)
				{
					base.WriteObject(this.monitoringData);
				}
				TaskLogger.LogExit();
			}
		}

		// Token: 0x06002ECD RID: 11981 RVA: 0x000BB52C File Offset: 0x000B972C
		private Server GetMailboxServerObjectFromParam()
		{
			if (this.mailboxServer == null)
			{
				return null;
			}
			LocalizedString? localizedString;
			IEnumerable<Server> objects = this.mailboxServer.GetObjects<Server>(null, this.CasConfigurationSession, null, out localizedString);
			IEnumerator<Server> enumerator = objects.GetEnumerator();
			if (base.HasErrors || !enumerator.MoveNext())
			{
				CasHealthMailboxServerNotFoundException exception = new CasHealthMailboxServerNotFoundException(this.mailboxServer.ToString());
				this.CasConnectivityWriteError(exception, ErrorCategory.ObjectNotFound, null);
				return null;
			}
			Server server = enumerator.Current;
			if (server.MajorVersion != Server.CurrentExchangeMajorVersion)
			{
				this.TraceInfo("The specified mailbox server is not on Exchange {0}", new object[]
				{
					Server.CurrentExchangeMajorVersion
				});
				this.CasConnectivityWriteError(new LocalizedException(Strings.CasHealthSpecifiedMailboxServerVersionNotMatched(base.CommandRuntime.ToString(), server.MajorVersion)), ErrorCategory.InvalidArgument, null);
			}
			if (enumerator.MoveNext())
			{
				this.WriteWarning(Strings.CasHealthTestMultipleMailboxServersFound);
			}
			return server;
		}

		// Token: 0x06002ECE RID: 11982 RVA: 0x000BB600 File Offset: 0x000B9800
		private string GenerateDeviceId()
		{
			IPGlobalProperties ipglobalProperties = IPGlobalProperties.GetIPGlobalProperties();
			int num = string.Concat(new string[]
			{
				ipglobalProperties.HostName,
				".",
				ipglobalProperties.DomainName,
				":",
				this.monitoringInstance,
				":",
				this.autodiscoverCas ? "1" : "0"
			}).GetHashCode();
			if (num < 0)
			{
				num = -num;
			}
			string text = num.ToString();
			this.TraceInfo("DeviceId generated: " + text);
			return text;
		}

		// Token: 0x06002ECF RID: 11983 RVA: 0x000BB694 File Offset: 0x000B9894
		private List<TestCasConnectivity.TestCasConnectivityRunInstance> BuildRunInstances()
		{
			string deviceId = this.GenerateDeviceId();
			List<TestCasConnectivity.TestCasConnectivityRunInstance> result;
			if (this.mailboxCredential == null)
			{
				result = this.BuildRunInstanceForSiteMBox(deviceId);
			}
			else
			{
				result = this.BuildRunInstancesForOneMbox(deviceId);
			}
			return result;
		}

		// Token: 0x06002ED0 RID: 11984 RVA: 0x000BB6C4 File Offset: 0x000B98C4
		private List<TestCasConnectivity.TestCasConnectivityRunInstance> BuildRunInstancesForOneMbox(string deviceId)
		{
			List<TestCasConnectivity.TestCasConnectivityRunInstance> list = new List<TestCasConnectivity.TestCasConnectivityRunInstance>();
			List<CasTransactionOutcome> list2 = new List<CasTransactionOutcome>();
			TestCasConnectivity.TestCasConnectivityRunInstance testCasConnectivityRunInstance = new TestCasConnectivity.TestCasConnectivityRunInstance();
			testCasConnectivityRunInstance.CasFqdn = this.CasFqdn;
			List<TestCasConnectivity.TestCasConnectivityRunInstance> result;
			try
			{
				if (TestConnectivityCredentialsManager.IsExchangeMultiTenant())
				{
					testCasConnectivityRunInstance.credentials = new NetworkCredential(this.mailboxCredential.UserName, this.mailboxCredential.Password.AsUnsecureString(), (this.casToTest != null) ? this.casToTest.Domain : string.Empty);
				}
				else
				{
					testCasConnectivityRunInstance.credentials = this.mailboxCredential.GetNetworkCredential();
				}
				testCasConnectivityRunInstance.deviceId = deviceId;
				testCasConnectivityRunInstance.allowUnsecureAccess = this.allowUnsecureAccess;
				testCasConnectivityRunInstance.trustAllCertificates = this.trustAllCertificates;
				bool flag2;
				if (this.clientAccessServer == null && this.autodiscoverCas)
				{
					LocalizedException ex = this.SetExchangePrincipal(testCasConnectivityRunInstance);
					if (ex != null)
					{
						LocalizedException exception = new CasHealthCouldNotLogUserForAutodiscoverException(testCasConnectivityRunInstance.credentials.Domain, testCasConnectivityRunInstance.credentials.UserName, this.ShortErrorMsgFromException(ex));
						this.CasConnectivityWriteError(exception, ErrorCategory.ObjectNotFound, null);
						return null;
					}
					this.TraceInfo("Calling LogonUser for user " + testCasConnectivityRunInstance.credentials.Domain + "\\" + testCasConnectivityRunInstance.credentials.UserName);
					SafeUserTokenHandle safeUserTokenHandle;
					bool flag = NativeMethods.LogonUser(testCasConnectivityRunInstance.credentials.UserName, testCasConnectivityRunInstance.credentials.Domain, testCasConnectivityRunInstance.credentials.Password, 8, 0, out safeUserTokenHandle);
					safeUserTokenHandle.Dispose();
					if (!flag)
					{
						ex = new CasHealthCouldNotLogUserException(TestConnectivityCredentialsManager.GetDomainUserNameFromCredentials(testCasConnectivityRunInstance.credentials), "", TestConnectivityCredentialsManager.NewTestUserScriptName, Marshal.GetLastWin32Error().ToString());
						this.CasConnectivityWriteError(ex, ErrorCategory.ObjectNotFound, null, false);
						return null;
					}
					List<TestCasConnectivity.TestCasConnectivityRunInstance> list3 = new List<TestCasConnectivity.TestCasConnectivityRunInstance>();
					this.DoAutodiscoverCas(testCasConnectivityRunInstance, list3);
					if (list3.Count == 0)
					{
						return null;
					}
					foreach (TestCasConnectivity.TestCasConnectivityRunInstance testCasConnectivityRunInstance2 in list3)
					{
						testCasConnectivityRunInstance2.CasFqdn = this.CasFqdn;
					}
					list.AddRange(list3);
					flag2 = true;
					if (null == testCasConnectivityRunInstance.baseUri)
					{
						return null;
					}
					this.TraceInfo("Exchange Autodiscovery found CAS URI: " + testCasConnectivityRunInstance.baseUri);
				}
				else
				{
					list.AddRange(this.PopulateInfoPerCas(testCasConnectivityRunInstance, list2));
					flag2 = true;
				}
				if (list2.Count > 0)
				{
					this.WriteMonitoringEventForOutcomes(list2, 1010, EventTypeEnumeration.Warning);
				}
				else if (flag2)
				{
					this.WriteMonitoringEvent(1011, this.MonitoringEventSource, EventTypeEnumeration.Success, Strings.CasConfigurationCheckSuccess);
				}
				result = list;
			}
			catch (PSArgumentException exception2)
			{
				base.WriteError(exception2, ErrorCategory.InvalidArgument, this.mailboxCredential);
				result = null;
			}
			return result;
		}

		// Token: 0x06002ED1 RID: 11985 RVA: 0x000BB994 File Offset: 0x000B9B94
		private LocalizedException ProcessSetExchangePrincipalResult(LocalizedException originalException, TestCasConnectivity.TestCasConnectivityRunInstance instance, Server server)
		{
			LocalizedException result = null;
			if (this.MonitoringContext)
			{
				return originalException;
			}
			if (originalException != null)
			{
				bool flag = TestConnectivityCredentialsManager.IsExchangeMultiTenant();
				this.TraceInfo("Exception getting ex principal mailbox server {0}, exception {1}", new object[]
				{
					server.Name,
					originalException.Message
				});
				if (flag)
				{
					this.WriteWarning(Strings.CasHealthTestUserInDataCenterNotAccessible(TestConnectivityCredentialsManager.GetAutomatedTaskDataCenterDomain(this.configurationSession.GetLocalSite())));
				}
				else
				{
					this.WriteWarning(Strings.CasHealthTestUserNotAccessible(instance.credentials.UserName));
				}
				if (!(originalException is CasHealthUserNotFoundException))
				{
					string errorString = this.ShortErrorMsgFromException(originalException);
					if (flag)
					{
						result = new CasHealthCouldNotLogUserDataCenterException(TestConnectivityCredentialsManager.GetAutomatedTaskDataCenterDomain(this.configurationSession.GetLocalSite()), TestConnectivityCredentialsManager.NewTestUserScriptName, errorString);
					}
					else
					{
						result = new CasHealthCouldNotLogUserException(TestConnectivityCredentialsManager.GetDomainUserNameFromCredentials(instance.credentials), (server.Fqdn == null) ? "" : server.Fqdn, TestConnectivityCredentialsManager.NewTestUserScriptName, errorString);
					}
				}
				else if (flag)
				{
					result = new CasHealthCouldNotLogUserDataCenterNoDetailedInfoException(TestConnectivityCredentialsManager.GetAutomatedTaskDataCenterDomain(this.configurationSession.GetLocalSite()), TestConnectivityCredentialsManager.NewTestUserScriptName);
				}
				else
				{
					result = new CasHealthCouldNotLogUserNoDetailedInfoException(TestConnectivityCredentialsManager.GetDomainUserNameFromCredentials(instance.credentials), (server.Fqdn == null) ? "" : server.Fqdn, TestConnectivityCredentialsManager.NewTestUserScriptName);
				}
			}
			return result;
		}

		// Token: 0x06002ED2 RID: 11986 RVA: 0x000BBACC File Offset: 0x000B9CCC
		private List<TestCasConnectivity.TestCasConnectivityRunInstance> BuildRunInstanceForSiteMBox(string deviceId)
		{
			if (this.casToTest == null)
			{
				CasHealthTaskNotRunOnExchangeServerException exception = new CasHealthTaskNotRunOnExchangeServerException();
				this.WriteErrorAndMonitoringEvent(exception, ErrorCategory.PermissionDenied, null, 1002, this.MonitoringEventSource, true);
				return null;
			}
			if (this.casToTest.ServerSite == null)
			{
				CasHealthTaskCasHasNoTopologySiteException exception2 = new CasHealthTaskCasHasNoTopologySiteException();
				this.WriteErrorAndMonitoringEvent(exception2, ErrorCategory.PermissionDenied, null, 1002, this.MonitoringEventSource, true);
				return null;
			}
			List<TestCasConnectivity.TestCasConnectivityRunInstance> list = new List<TestCasConnectivity.TestCasConnectivityRunInstance>();
			List<CasTransactionOutcome> list2 = new List<CasTransactionOutcome>();
			List<CasTransactionOutcome> list3 = new List<CasTransactionOutcome>();
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			ADSite localSite = this.configurationSession.GetLocalSite();
			List<CasTransactionOutcome> list4 = new List<CasTransactionOutcome>();
			List<CasTransactionOutcome> list5 = new List<CasTransactionOutcome>();
			bool flag4 = false;
			TransientErrorCache transientErrorCache = this.GetTransientErrorCache();
			Server server = this.casToTest;
			TestCasConnectivity.TestCasConnectivityRunInstance testCasConnectivityRunInstance = new TestCasConnectivity.TestCasConnectivityRunInstance();
			testCasConnectivityRunInstance.CasFqdn = this.CasFqdn;
			testCasConnectivityRunInstance.deviceId = deviceId;
			testCasConnectivityRunInstance.allowUnsecureAccess = this.allowUnsecureAccess;
			testCasConnectivityRunInstance.trustAllCertificates = this.trustAllCertificates;
			SmtpAddress? address;
			if (TestConnectivityCredentialsManager.IsExchangeMultiTenant())
			{
				address = TestConnectivityCredentialsManager.GetMultiTenantAutomatedTaskUser(this, this.configurationSession, localSite, this.UserType);
			}
			else
			{
				address = TestConnectivityCredentialsManager.GetEnterpriseAutomatedTaskUser(localSite, this.casToTest.Domain);
			}
			testCasConnectivityRunInstance.credentials = new NetworkCredential();
			testCasConnectivityRunInstance.credentials.UserName = TestCasConnectivity.GetInstanceUserNameFromTestUser(address);
			if (testCasConnectivityRunInstance.credentials.UserName == null)
			{
				this.WriteError(new CasHealthCouldNotBuildTestUserNameException(server.Fqdn), ErrorCategory.ObjectNotFound, null, false);
			}
			else
			{
				testCasConnectivityRunInstance.credentials.Domain = this.casToTest.Domain;
				testCasConnectivityRunInstance.safeToBuildUpnString = true;
				LocalizedException ex = this.SetExchangePrincipal(testCasConnectivityRunInstance);
				ex = this.ProcessSetExchangePrincipalResult(ex, testCasConnectivityRunInstance, server);
				if (ex != null)
				{
					this.CasConnectivityWriteError(ex, ErrorCategory.ObjectNotFound, null, false);
					List<CasTransactionOutcome> list6 = this.BuildFailedPerformanceOutcomesWithMessage(testCasConnectivityRunInstance, this.ShortErrorMsgFromException(ex), this.casToTest.Fqdn);
					if (list6 != null)
					{
						list5.AddRange(list6);
					}
				}
				else
				{
					string serverFqdn = testCasConnectivityRunInstance.exchangePrincipal.MailboxInfo.Location.ServerFqdn;
					ex = this.VerifyMailboxServerIsInSite(testCasConnectivityRunInstance.credentials.UserName, localSite, serverFqdn);
					flag3 = true;
					if (!this.MonitoringContext && !TestConnectivityCredentialsManager.IsExchangeMultiTenant())
					{
						ex = this.ResetAutomatedCredentialsAndVerify(testCasConnectivityRunInstance);
					}
					else
					{
						ex = this.LoadAutomatedCredentials(testCasConnectivityRunInstance);
						testCasConnectivityRunInstance.safeToBuildUpnString = true;
					}
					if (ex != null)
					{
						List<CasTransactionOutcome> list7 = this.BuildFailedPerformanceOutcomesWithMessage(testCasConnectivityRunInstance, this.ShortErrorMsgFromException(ex), serverFqdn);
						if (list7 != null)
						{
							if (ex is CasHealthInstructResetCredentialsException)
							{
								list5.AddRange(list7);
							}
							else
							{
								list4.AddRange(list7);
							}
						}
					}
					else
					{
						flag3 = true;
						bool flag5;
						if (Datacenter.IsMicrosoftHostedOnly(false))
						{
							flag5 = true;
						}
						else
						{
							string text = string.Empty;
							if (Datacenter.IsPartnerHostedOnly(false))
							{
								text = testCasConnectivityRunInstance.credentials.UserName;
							}
							else
							{
								text = testCasConnectivityRunInstance.credentials.UserName + "@" + testCasConnectivityRunInstance.credentials.Domain;
							}
							this.TraceInfo("Calling LogonUser for user " + text);
							Microsoft.Exchange.Diagnostics.Components.Monitoring.ExTraceGlobals.MonitoringTasksTracer.TraceDebug<string>((long)this.GetHashCode(), "BuildRunInstancesForCasMboxTuples: calling LogonUser for User: {0}", text);
							SafeUserTokenHandle safeUserTokenHandle;
							flag5 = NativeMethods.LogonUser(text, null, testCasConnectivityRunInstance.credentials.Password, 8, 0, out safeUserTokenHandle);
							safeUserTokenHandle.Dispose();
						}
						if (!flag5)
						{
							CasHealthCouldNotLogUserException exception3 = new CasHealthCouldNotLogUserException(TestConnectivityCredentialsManager.GetDomainUserNameFromCredentials(testCasConnectivityRunInstance.credentials), serverFqdn, TestConnectivityCredentialsManager.NewTestUserScriptName, Marshal.GetLastWin32Error().ToString());
							this.CasConnectivityWriteError(exception3, ErrorCategory.PermissionDenied, null, false);
							List<CasTransactionOutcome> list8 = this.BuildFailedPerformanceOutcomesWithMessage(testCasConnectivityRunInstance, this.ShortErrorMsgFromException(exception3), testCasConnectivityRunInstance.exchangePrincipal.MailboxInfo.Location.ServerFqdn);
							if (list8 != null)
							{
								list5.AddRange(list8);
							}
						}
						else
						{
							flag2 = true;
							if (this.autodiscoverCas)
							{
								List<TestCasConnectivity.TestCasConnectivityRunInstance> list9 = new List<TestCasConnectivity.TestCasConnectivityRunInstance>();
								List<CasTransactionOutcome> list10 = this.DoAutodiscoverCas(testCasConnectivityRunInstance, list9);
								if (list9.Count == 0)
								{
									if (list10 != null && list10.Count != 0)
									{
										flag4 = true;
										list2.AddRange(list10);
									}
								}
								else
								{
									foreach (TestCasConnectivity.TestCasConnectivityRunInstance testCasConnectivityRunInstance2 in list9)
									{
										testCasConnectivityRunInstance.CasFqdn = this.CasFqdn;
									}
									list.AddRange(list9);
									this.TraceInfo("Exchange Autodiscovery found CAS URI: " + testCasConnectivityRunInstance.baseUri);
								}
							}
							else
							{
								list.AddRange(this.PopulateInfoPerCas(testCasConnectivityRunInstance, list3));
								flag = true;
							}
						}
					}
				}
			}
			if (flag4)
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (CasTransactionOutcome casTransactionOutcome in list2)
				{
					stringBuilder.Append(casTransactionOutcome.ToString() + "\r\n\r\n");
				}
				this.WriteMonitoringEvent(1003, this.MonitoringEventSource, EventTypeEnumeration.Warning, stringBuilder.ToString());
			}
			else if (this.autodiscoverCas)
			{
				this.WriteMonitoringEvent(1004, this.MonitoringEventSource, EventTypeEnumeration.Success, Strings.AutodiscoverySuccess);
			}
			if (list3.Count > 0)
			{
				this.WriteMonitoringEventForOutcomes(list3, 1010, EventTypeEnumeration.Warning);
			}
			else if (flag)
			{
				this.WriteMonitoringEvent(1011, this.MonitoringEventSource, EventTypeEnumeration.Success, Strings.CasConfigurationCheckSuccess);
			}
			if (list5.Count != 0)
			{
				StringBuilder stringBuilder2 = new StringBuilder();
				foreach (CasTransactionOutcome casTransactionOutcome2 in list5)
				{
					if (casTransactionOutcome2.LocalSite != null)
					{
						stringBuilder2.Append(Strings.LocalSiteColon + casTransactionOutcome2.LocalSite + "\r\n\r\n");
					}
					stringBuilder2.Append(casTransactionOutcome2.Error + "\r\n\r\n\r\n");
				}
				this.WriteMonitoringEvent(1008, this.MonitoringEventSource, EventTypeEnumeration.Warning, Strings.InstructResetCredentials(stringBuilder2.ToString()));
			}
			else if (flag2 && list4.Count == 0)
			{
				this.WriteMonitoringEvent(1007, this.MonitoringEventSource, EventTypeEnumeration.Success, Strings.LoadCredentialsSuccess);
			}
			if (list4.Count != 0)
			{
				bool flag6 = false;
				StringBuilder stringBuilder3 = new StringBuilder();
				foreach (CasTransactionOutcome casTransactionOutcome3 in list4)
				{
					string text2 = string.Empty;
					string text3 = string.Empty;
					if (casTransactionOutcome3.LocalSite != null)
					{
						text3 = casTransactionOutcome3.LocalSite.ToString();
						text2 = Strings.LocalSiteColon + casTransactionOutcome3.LocalSite + "\r\n\r\n";
					}
					text2 = text2 + casTransactionOutcome3.Error + "\r\n\r\n\r\n";
					if (!string.IsNullOrEmpty(text3) && transientErrorCache != null && !transientErrorCache.ContainsError(text3))
					{
						transientErrorCache.Add(new CASServiceError(text3));
					}
					else
					{
						stringBuilder3.Append(text2);
						flag6 = true;
					}
					this.WriteMomPerformanceCounters(casTransactionOutcome3);
				}
				if (flag6)
				{
					this.WriteMonitoringEvent(1005, this.MonitoringEventSource, EventTypeEnumeration.Error, Strings.AccessStoreError(stringBuilder3.ToString()));
				}
			}
			else if (flag3)
			{
				this.WriteMonitoringEvent(1006, this.MonitoringEventSource, EventTypeEnumeration.Success, Strings.AccessStoreSuccess);
			}
			return list;
		}

		// Token: 0x06002ED3 RID: 11987 RVA: 0x000BC20C File Offset: 0x000BA40C
		private LocalizedException VerifyMailboxServerIsInSite(string userName, ADSite localSite, string serverFqdn)
		{
			LocalizedException result = null;
			Server server = this.FindServerByHostName(serverFqdn);
			if (server == null || server.ServerSite == null || !server.ServerSite.DistinguishedName.Equals(localSite.DistinguishedName, StringComparison.InvariantCultureIgnoreCase))
			{
				result = new CasHealthTestUserOnWrongSiteException(userName, localSite.Name, server.ServerSite.Name);
			}
			return result;
		}

		// Token: 0x06002ED4 RID: 11988 RVA: 0x000BC260 File Offset: 0x000BA460
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			try
			{
				this.DetermineCasServer();
				if (this.monitoringInstance == null)
				{
					this.monitoringInstance = "";
				}
				List<TestCasConnectivity.TestCasConnectivityRunInstance> list = new List<TestCasConnectivity.TestCasConnectivityRunInstance>();
				if (this.ResetTestAccountCredentials)
				{
					LocalizedException ex = null;
					Server server = null;
					if (this.mailboxServer != null)
					{
						server = this.GetMailboxServerObjectFromParam();
					}
					else if (this.localServer != null && this.localServer.IsMailboxServer)
					{
						server = this.localServer;
					}
					else
					{
						ex = new CasHealthSpecifyMailboxForResetCredentialsException();
					}
					if (server != null)
					{
						ex = this.ResetAutomatedCredentialsOnMailboxServer(server, false);
					}
					if (ex != null)
					{
						this.CasConnectivityWriteError(ex, ErrorCategory.ObjectNotFound, null, false);
					}
				}
				else
				{
					list = this.BuildRunInstances();
					if (list == null)
					{
						return;
					}
					if (list.Count == 0 && !this.ResetTestAccountCredentials)
					{
						this.WriteWarning(Strings.CasHealthNoTuplesToTest);
						if (this.HandleNoRunInstances())
						{
							return;
						}
					}
				}
				StringBuilder stringBuilder = new StringBuilder();
				StringBuilder stringBuilder2 = new StringBuilder();
				int num = 0;
				int num2 = 0;
				if (this.ResetTestAccountCredentials)
				{
					list.Clear();
				}
				List<IAsyncResult> list2 = new List<IAsyncResult>();
				Queue queue = Queue.Synchronized(new Queue());
				foreach (TestCasConnectivity.TestCasConnectivityRunInstance testCasConnectivityRunInstance in list)
				{
					testCasConnectivityRunInstance.Outcomes = queue;
					testCasConnectivityRunInstance.LightMode = this.LightMode;
					testCasConnectivityRunInstance.MonitoringContext = this.MonitoringContext;
					list2.Add(this.BeginExecute(testCasConnectivityRunInstance));
				}
				uint seconds = (this.Timeout <= 0U || this.Timeout > 3600U) ? this.GetDefaultTimeOut() : this.Timeout;
				if (this.MonitoringContext)
				{
					this.WaitAll(list2.ToArray(), ExDateTime.Now.Add(new TimeSpan(0, 0, (int)seconds)), queue);
				}
				foreach (TestCasConnectivity.TestCasConnectivityRunInstance testCasConnectivityRunInstance2 in list)
				{
					if (testCasConnectivityRunInstance2.Outcomes.Count <= 0)
					{
						break;
					}
					lock (testCasConnectivityRunInstance2.Outcomes.SyncRoot)
					{
						foreach (object output in testCasConnectivityRunInstance2.Outcomes)
						{
							this.WriteToConsole(output);
						}
					}
				}
				foreach (TestCasConnectivity.TestCasConnectivityRunInstance testCasConnectivityRunInstance3 in list)
				{
					using (AsyncResult<CasTransactionOutcome> result = testCasConnectivityRunInstance3.Result)
					{
						List<CasTransactionOutcome> list3 = result.Outcomes;
						bool flag2 = result.DidTimeout();
						string mailboxFqdn = (testCasConnectivityRunInstance3.exchangePrincipal != null) ? testCasConnectivityRunInstance3.exchangePrincipal.MailboxInfo.Location.ServerFqdn : null;
						if (flag2)
						{
							list3 = this.BuildFailedPerformanceOutcomesWithMessage(testCasConnectivityRunInstance3, Strings.CasConnectivityTaskTimeout(seconds), mailboxFqdn);
							if (list3 != null && list3.Count > 0)
							{
								this.WriteToConsole(list3[0]);
							}
						}
						StringBuilder stringBuilder3 = new StringBuilder();
						StringBuilder stringBuilder4 = new StringBuilder();
						int num3 = 0;
						int num4 = 0;
						foreach (CasTransactionOutcome casTransactionOutcome in list3)
						{
							if (casTransactionOutcome.ClientAccessServer != null)
							{
								this.WriteMomPerformanceCounters(casTransactionOutcome);
								if (CasTransactionResultEnum.Success == casTransactionOutcome.Result.Value)
								{
									lock (testCasConnectivityRunInstance3.Outcomes.SyncRoot)
									{
										testCasConnectivityRunInstance3.Outcomes.Clear();
										continue;
									}
								}
								string transactionTarget = casTransactionOutcome.ClientAccessServer.ToString() + "|" + casTransactionOutcome.LocalSite;
								StringBuilder stringBuilder5 = new StringBuilder();
								if (testCasConnectivityRunInstance3.Outcomes.Count > 0)
								{
									while (testCasConnectivityRunInstance3.Outcomes.Count > 0)
									{
										object obj = testCasConnectivityRunInstance3.Outcomes.Dequeue();
										stringBuilder5.AppendLine();
										if (obj is Warning)
										{
											stringBuilder5.AppendLine();
										}
										stringBuilder5.Append(obj);
									}
									stringBuilder5.AppendLine();
								}
								if (casTransactionOutcome.EventType == EventTypeEnumeration.Warning)
								{
									stringBuilder4.Append(Strings.CasHealthTransactionFailedSummary(transactionTarget, casTransactionOutcome.Error, stringBuilder5.ToString()));
									num4++;
								}
								else
								{
									stringBuilder3.Append(Strings.CasHealthTransactionFailedSummary(transactionTarget, casTransactionOutcome.Error, stringBuilder5.ToString()));
									num3++;
								}
							}
						}
						this.UpdateTransientErrorCache(testCasConnectivityRunInstance3, num3, num4, ref num, ref num2, stringBuilder3, stringBuilder4, ref stringBuilder, ref stringBuilder2);
					}
				}
				if (num > 0 || num2 > 0)
				{
					if (num > 0)
					{
						stringBuilder.AppendLine(Strings.CasHealthPowerShellCmdletExecutionSummary(Environment.MachineName));
						this.WriteMonitoringEvent(1001, this.MonitoringEventSource, EventTypeEnumeration.Error, this.TransactionFailuresEventMessage(stringBuilder.ToString()));
					}
					if (num2 > 0)
					{
						stringBuilder2.AppendLine(Strings.CasHealthPowerShellCmdletExecutionSummary(Environment.MachineName));
						this.WriteMonitoringEvent(1012, this.MonitoringEventSource, EventTypeEnumeration.Warning, this.TransactionWarningsEventMessage(stringBuilder2.ToString()));
					}
				}
				else if (list.Count != 0)
				{
					this.WriteMonitoringEvent(1000, this.MonitoringEventSource, EventTypeEnumeration.Success, this.TransactionSuccessEventMessage);
				}
			}
			catch (StorageTransientException exception)
			{
				this.CasConnectivityWriteError(exception, ErrorCategory.ObjectNotFound, null);
			}
			catch (ADTransientException exception2)
			{
				this.CasConnectivityWriteError(exception2, ErrorCategory.ObjectNotFound, null);
			}
			catch (DataValidationException ex2)
			{
				CasHealthDataValidationErrorException exception3 = new CasHealthDataValidationErrorException(ex2.ToString());
				this.WriteErrorAndMonitoringEvent(exception3, ErrorCategory.InvalidData, null, 1009, this.MonitoringEventSource, true);
			}
			finally
			{
				if (this.MonitoringContext)
				{
					this.WriteMonitoringData();
				}
				TaskLogger.LogExit();
			}
		}

		// Token: 0x06002ED5 RID: 11989 RVA: 0x000BC944 File Offset: 0x000BAB44
		internal void WriteMonitoringEventForOutcomes(List<CasTransactionOutcome> outcomes, int eventId, EventTypeEnumeration eventType)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (CasTransactionOutcome casTransactionOutcome in outcomes)
			{
				stringBuilder.Append(casTransactionOutcome.Error + "\r\n\r\n");
			}
			this.WriteMonitoringEvent(eventId, this.MonitoringEventSource, EventTypeEnumeration.Warning, stringBuilder.ToString());
		}

		// Token: 0x06002ED6 RID: 11990 RVA: 0x000BC9BC File Offset: 0x000BABBC
		protected void WriteMonitoringData()
		{
			base.WriteObject(this.monitoringData);
		}

		// Token: 0x06002ED7 RID: 11991 RVA: 0x000BC9CA File Offset: 0x000BABCA
		protected virtual bool HandleNoRunInstances()
		{
			return false;
		}

		// Token: 0x06002ED8 RID: 11992
		protected abstract List<CasTransactionOutcome> BuildPerformanceOutcomes(TestCasConnectivity.TestCasConnectivityRunInstance instance, string mailboxFqdn);

		// Token: 0x06002ED9 RID: 11993 RVA: 0x000BC9D0 File Offset: 0x000BABD0
		private List<CasTransactionOutcome> BuildFailedPerformanceOutcomesWithMessage(TestCasConnectivity.TestCasConnectivityRunInstance instance, string message, string mailboxFqdn)
		{
			List<CasTransactionOutcome> list = this.BuildPerformanceOutcomes(instance, mailboxFqdn);
			if (list != null)
			{
				foreach (CasTransactionOutcome casTransactionOutcome in list)
				{
					casTransactionOutcome.Update(CasTransactionResultEnum.Failure, new TimeSpan(0, 0, 0, 0, -1000), message);
				}
			}
			return list;
		}

		// Token: 0x06002EDA RID: 11994 RVA: 0x000BCA3C File Offset: 0x000BAC3C
		private void WriteMomPerformanceCounters(CasTransactionOutcome outcome)
		{
			string text = outcome.ClientAccessServer.ToString() + "|" + outcome.LocalSite;
			if (!string.IsNullOrEmpty(outcome.PerformanceCounterName))
			{
				double num;
				if (CasTransactionResultEnum.Success == outcome.Result.Value)
				{
					num = outcome.Latency.TotalMilliseconds;
				}
				else
				{
					num = -1000.0;
				}
				this.TraceInfo(string.Concat(new object[]
				{
					"Outputing performance counter instance ",
					text,
					" with latency ",
					num
				}));
				this.monitoringData.PerformanceCounters.Add(new MonitoringPerformanceCounter(this.PerformanceObject, outcome.PerformanceCounterName, text, num));
			}
		}

		// Token: 0x06002EDB RID: 11995 RVA: 0x000BCAF0 File Offset: 0x000BACF0
		internal static string GetInstanceUserNameFromTestUser(SmtpAddress? address)
		{
			if (address == null)
			{
				return null;
			}
			if (Datacenter.IsMultiTenancyEnabled())
			{
				return address.Value.ToString();
			}
			return address.Value.Local;
		}

		// Token: 0x06002EDC RID: 11996 RVA: 0x000BCB34 File Offset: 0x000BAD34
		protected LocalizedException SetExchangePrincipal(TestCasConnectivity.TestCasConnectivityRunInstance instance)
		{
			this.TraceInfo(string.Concat(new string[]
			{
				"Looking up user in ActiveDirectory: userName: '",
				instance.credentials.UserName,
				"' domain: '",
				instance.credentials.Domain,
				"'"
			}));
			if (instance.exchangePrincipal != null)
			{
				return null;
			}
			ADUser aduser = null;
			string text = null;
			if (instance.safeToBuildUpnString)
			{
				string text2 = instance.credentials.UserName;
				if (!Datacenter.IsMultiTenancyEnabled())
				{
					text2 = text2 + "@" + instance.credentials.Domain;
				}
				this.TraceInfo("UPN is {0}", new object[]
				{
					text2
				});
				try
				{
					using (WindowsIdentity windowsIdentity = new WindowsIdentity(text2))
					{
						aduser = (ADUser)this.CasGlobalCatalogSession.FindBySid(windowsIdentity.User);
					}
				}
				catch (SecurityException ex)
				{
					this.TraceInfo("new WindowsIdentity has security exception: {0}", new object[]
					{
						ex.Message
					});
					return new CasHealthUserNotFoundException(text2, this.ShortErrorMsgFromException(ex));
				}
				if (aduser == null)
				{
					this.TraceInfo("FindBySid returned null");
					return new CasHealthUserNotFoundException(text2, "");
				}
				text = text2;
			}
			else
			{
				string domain;
				if (Datacenter.IsMultiTenancyEnabled())
				{
					SmtpAddress smtpAddress = SmtpAddress.Parse(instance.credentials.UserName);
					text = smtpAddress.Local;
					domain = smtpAddress.Domain;
				}
				else
				{
					text = instance.credentials.UserName;
					domain = instance.credentials.Domain;
				}
				if (string.IsNullOrEmpty(domain))
				{
					if (this.casToTest != null)
					{
						domain = this.casToTest.Domain;
					}
					else
					{
						domain = this.localServer.Domain;
					}
				}
				if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(domain))
				{
					MailboxIdParameter mailboxIdParameter = new MailboxIdParameter(string.Format("{0}\\{1}", domain, text));
					foreach (ADUser aduser2 in mailboxIdParameter.GetObjects<ADUser>(null, this.CasGlobalCatalogSession))
					{
						if (aduser != null)
						{
							throw new NonUniqueRecipientException(mailboxIdParameter.ToString(), new ObjectValidationError(DirectoryStrings.ErrorNonUniqueDomainAccount(domain, text), null, string.Empty));
						}
						aduser = aduser2;
					}
				}
				if (aduser == null)
				{
					this.TraceInfo("AD FindByAccountName returned null.");
					return new CasHealthUserNotFoundException(text, "");
				}
				this.TraceInfo("Found user in the ActiveDirectory!");
			}
			this.TraceInfo("Found user in the ActiveDirectory!");
			this.TraceInfo("Building Exchange Principal...");
			try
			{
				instance.exchangePrincipal = ExchangePrincipal.FromADUser(aduser, null);
			}
			catch (ObjectNotFoundException ex2)
			{
				this.TraceInfo("ObjectNotFoundException: {0}", new object[]
				{
					ex2.Message
				});
				return new CasHealthMailboxNotFoundException(text);
			}
			this.TraceInfo("Exchange Principal built.");
			return null;
		}

		// Token: 0x06002EDD RID: 11997 RVA: 0x000BCE30 File Offset: 0x000BB030
		protected void InitializeTopologyInformation()
		{
			if (this.thisFqdn == null)
			{
				this.thisFqdn = NativeHelpers.GetLocalComputerFqdn(false);
				if (this.thisFqdn == null)
				{
					CasHealthCouldNotObtainFqdnException exception = new CasHealthCouldNotObtainFqdnException();
					this.CasConnectivityWriteError(exception, ErrorCategory.PermissionDenied, null);
				}
				this.localServer = this.CasConfigurationSession.ReadLocalServer();
			}
			if (this.casToTest == null)
			{
				this.DetermineCasServer();
			}
		}

		// Token: 0x06002EDE RID: 11998 RVA: 0x000BCE88 File Offset: 0x000BB088
		protected void DetermineCasServer()
		{
			if (this.clientAccessServer != null && !string.IsNullOrEmpty(this.clientAccessServer.RawIdentity))
			{
				this.TraceInfo("The CAS server to test is determined by -ClientAccessServer argument.");
				Server server = this.FindServerByHostName(this.clientAccessServer.ToString());
				if (server == null)
				{
					this.TraceInfo("The specified server was not found.");
					this.CasConnectivityWriteError(new LocalizedException(Strings.CasHealthCasServerNotFound), ErrorCategory.ObjectNotFound, null);
					return;
				}
				if (!server.IsClientAccessServer)
				{
					this.TraceInfo("The specified server is not a CAS server.");
					this.CasConnectivityWriteError(new LocalizedException(Strings.CasHealthSpecifiedServerIsNotCas), ErrorCategory.ObjectNotFound, null);
					return;
				}
				this.TraceInfo("The specified server is a CAS server.");
				if (server.MajorVersion != Server.CurrentExchangeMajorVersion)
				{
					this.TraceInfo("The specified CAS server is not on Exchange {0}", new object[]
					{
						Server.CurrentExchangeMajorVersion
					});
					this.CasConnectivityWriteError(new LocalizedException(Strings.CasHealthSpecifiedCASServerVersionNotMatched(base.CommandRuntime.ToString(), server.MajorVersion)), ErrorCategory.InvalidArgument, null);
					return;
				}
				this.casToTest = server;
				return;
			}
			else
			{
				if (this.localServer != null && this.localServer.IsClientAccessServer)
				{
					this.TraceInfo("The CAS server to test is the local server.");
					this.casToTest = this.localServer;
					return;
				}
				this.TraceInfo("Local computer is not a CAS server. No CAS server has been found.");
				return;
			}
		}

		// Token: 0x06002EDF RID: 11999 RVA: 0x000BCFBA File Offset: 0x000BB1BA
		protected Server FindServerByHostName(string hostName)
		{
			if (hostName.IndexOf('.') > 0)
			{
				return this.configurationSession.FindServerByFqdn(hostName);
			}
			return this.configurationSession.FindServerByName(hostName);
		}

		// Token: 0x06002EE0 RID: 12000 RVA: 0x000BCFE0 File Offset: 0x000BB1E0
		protected TimeSpan ComputeLatency(ExDateTime startTime)
		{
			ExDateTime now = ExDateTime.Now;
			while (now == startTime)
			{
				now = ExDateTime.Now;
			}
			return now - startTime;
		}

		// Token: 0x06002EE1 RID: 12001 RVA: 0x000BD00B File Offset: 0x000BB20B
		internal static void TraceInfo(int hashCode, string info)
		{
			Microsoft.Exchange.Diagnostics.Components.Tasks.ExTraceGlobals.TraceTracer.Information((long)hashCode, info);
		}

		// Token: 0x06002EE2 RID: 12002 RVA: 0x000BD01A File Offset: 0x000BB21A
		protected void TraceInfo(string info)
		{
			TestCasConnectivity.TraceInfo(this.GetHashCode(), info);
		}

		// Token: 0x06002EE3 RID: 12003 RVA: 0x000BD028 File Offset: 0x000BB228
		protected void TraceInfo(string fmt, params object[] p)
		{
			this.TraceInfo(string.Format(fmt, p));
		}

		// Token: 0x06002EE4 RID: 12004 RVA: 0x000BD037 File Offset: 0x000BB237
		protected void WriteErrorAndMonitoringEvent(Exception exception, ErrorCategory errorCategory, object target, int eventId, string eventSource, bool terminateTask)
		{
			this.WriteMonitoringEvent(eventId, eventSource, EventTypeEnumeration.Error, this.ShortErrorMsgFromException(exception));
			this.CasConnectivityWriteError(exception, errorCategory, target, terminateTask);
		}

		// Token: 0x06002EE5 RID: 12005 RVA: 0x000BD056 File Offset: 0x000BB256
		internal void WriteMonitoringEvent(int eventId, string eventSource, EventTypeEnumeration eventType, string eventMessage)
		{
			this.monitoringData.Events.Add(new MonitoringEvent(eventSource, eventId, eventType, eventMessage));
		}

		// Token: 0x06002EE6 RID: 12006 RVA: 0x000BD074 File Offset: 0x000BB274
		private bool WaitAll(IAsyncResult[] asyncResultArray, ExDateTime deadline, Queue outcomeQueue)
		{
			bool flag = false;
			for (int i = asyncResultArray.Length - 1; i >= 0; i--)
			{
				IAsyncResult asyncResult = asyncResultArray[i];
				if (!asyncResult.IsCompleted)
				{
					TimeSpan t = deadline - ExDateTime.Now;
					if (t <= TimeSpan.Zero)
					{
						flag = true;
						break;
					}
					if (!this.MonitoringContext)
					{
						while (!asyncResult.AsyncWaitHandle.WaitOne(100, false))
						{
							for (int j = outcomeQueue.Count; j > 0; j--)
							{
								object output = outcomeQueue.Dequeue();
								this.WriteToConsole(output);
							}
							t = deadline - ExDateTime.Now;
							if (t <= TimeSpan.Zero)
							{
								flag = true;
								break;
							}
						}
					}
					if (flag)
					{
						break;
					}
					t = deadline - ExDateTime.Now;
					if (t <= TimeSpan.Zero)
					{
						flag = true;
						break;
					}
					if (!asyncResult.AsyncWaitHandle.WaitOne(t, false))
					{
						flag = true;
						break;
					}
				}
			}
			if (flag)
			{
				foreach (IAsyncResult asyncResult2 in asyncResultArray)
				{
					if (!asyncResult2.IsCompleted)
					{
						((AsyncResult<CasTransactionOutcome>)asyncResult2).SetTimeout();
					}
				}
				return false;
			}
			return true;
		}

		// Token: 0x06002EE7 RID: 12007 RVA: 0x000BD194 File Offset: 0x000BB394
		private void WriteToConsole(object output)
		{
			if (output != null)
			{
				if (output is CasTransactionOutcome)
				{
					base.WriteObject(output);
					return;
				}
				if (output is Warning)
				{
					base.WriteWarning(((Warning)output).Message);
					return;
				}
				base.WriteVerbose((LocalizedString)output);
			}
		}

		// Token: 0x06002EE8 RID: 12008 RVA: 0x000BD1D0 File Offset: 0x000BB3D0
		protected static Uri GetUrlWithTrailingSlash(Uri originalUri)
		{
			if (originalUri.AbsoluteUri.EndsWith("/"))
			{
				return originalUri;
			}
			UriBuilder uriBuilder = new UriBuilder(originalUri.AbsoluteUri + "/");
			return uriBuilder.Uri;
		}

		// Token: 0x06002EE9 RID: 12009 RVA: 0x000BD20D File Offset: 0x000BB40D
		protected void SetTestOutcome(CasTransactionOutcome outcome, EventTypeEnumeration eventType, string message, Queue instanceOutcomes)
		{
			if (eventType == EventTypeEnumeration.Warning)
			{
				outcome.Update(CasTransactionResultEnum.Skipped, message, eventType);
			}
			else
			{
				outcome.Update(CasTransactionResultEnum.Failure, message, eventType);
			}
			if (instanceOutcomes != null)
			{
				instanceOutcomes.Enqueue(new Warning(message));
				return;
			}
			base.WriteWarning(message);
		}

		// Token: 0x0400215B RID: 8539
		protected const int ADSessionTimeout = 30000;

		// Token: 0x0400215C RID: 8540
		internal const string VerboseFormat = "[{1}] : {0}";

		// Token: 0x0400215D RID: 8541
		internal const string VerboseTimeFormat = "HH:mm:ss.fff";

		// Token: 0x0400215E RID: 8542
		protected const double LatencyPerformanceInCaseOfError = -1000.0;

		// Token: 0x0400215F RID: 8543
		private ITopologyConfigurationSession configurationSession;

		// Token: 0x04002160 RID: 8544
		private IRecipientSession globalCatalogSession;

		// Token: 0x04002161 RID: 8545
		protected string thisFqdn;

		// Token: 0x04002162 RID: 8546
		protected Server casToTest;

		// Token: 0x04002163 RID: 8547
		protected Server localServer;

		// Token: 0x04002164 RID: 8548
		protected PSCredential mailboxCredential;

		// Token: 0x04002165 RID: 8549
		protected bool allowUnsecureAccess;

		// Token: 0x04002166 RID: 8550
		protected bool trustAllCertificates;

		// Token: 0x04002167 RID: 8551
		protected bool autodiscoverCas;

		// Token: 0x04002168 RID: 8552
		protected string monitoringInstance;

		// Token: 0x04002169 RID: 8553
		protected ServerIdParameter clientAccessServer;

		// Token: 0x0400216A RID: 8554
		protected ServerIdParameter mailboxServer;

		// Token: 0x0400216B RID: 8555
		protected SecureString password;

		// Token: 0x0400216C RID: 8556
		protected MonitoringData monitoringData = new MonitoringData();

		// Token: 0x0400216D RID: 8557
		private uint timeout;

		// Token: 0x02000516 RID: 1302
		internal static class EventId
		{
			// Token: 0x0400216E RID: 8558
			internal const int AllTransactionsSucceeded = 1000;

			// Token: 0x0400216F RID: 8559
			internal const int SomeTransactionsFailed = 1001;

			// Token: 0x04002170 RID: 8560
			internal const int AutomatedTaskMustBeRunOnExchangeServer = 1002;

			// Token: 0x04002171 RID: 8561
			internal const int AutodiscoveryError = 1003;

			// Token: 0x04002172 RID: 8562
			internal const int AutodiscoverySuccess = 1004;

			// Token: 0x04002173 RID: 8563
			internal const int AccessStoreError = 1005;

			// Token: 0x04002174 RID: 8564
			internal const int AccessStoreSuccess = 1006;

			// Token: 0x04002175 RID: 8565
			internal const int LoadCredentialsSuccess = 1007;

			// Token: 0x04002176 RID: 8566
			internal const int InstructResetCredentials = 1008;

			// Token: 0x04002177 RID: 8567
			internal const int DataValidationError = 1009;

			// Token: 0x04002178 RID: 8568
			internal const int CasConfigurationError = 1010;

			// Token: 0x04002179 RID: 8569
			internal const int CasConfigurationSuccess = 1011;

			// Token: 0x0400217A RID: 8570
			internal const int SomeTransactionsHadWarnings = 1012;
		}

		// Token: 0x02000517 RID: 1303
		public class TestCasConnectivityRunInstance
		{
			// Token: 0x17000DEC RID: 3564
			// (get) Token: 0x06002EEB RID: 12011 RVA: 0x000BD254 File Offset: 0x000BB454
			internal LiveIdAuthenticationConfiguration LiveIdAuthenticationConfiguration
			{
				get
				{
					if (this.liveIdAuthenticationConfiguration == null)
					{
						LiveIdAuthenticationConfiguration liveIdAuthenticationConfiguration = new LiveIdAuthenticationConfiguration();
						FederationTrust federationTrust = FederationTrustCache.GetFederationTrust("MicrosoftOnline");
						ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.FullyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 343, "LiveIdAuthenticationConfiguration", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Monitoring\\Cas\\TestCasConnectivity.cs");
						ServiceEndpointContainer endpointContainer = topologyConfigurationSession.GetEndpointContainer();
						liveIdAuthenticationConfiguration.MsoTokenIssuerUri = federationTrust.TokenIssuerUri.ToString();
						liveIdAuthenticationConfiguration.LiveServiceLogin1Uri = endpointContainer.GetEndpoint(ServiceEndpointId.LiveServiceLogin1).Uri;
						liveIdAuthenticationConfiguration.LiveServiceLogin2Uri = endpointContainer.GetEndpoint(ServiceEndpointId.LiveServiceLogin2).Uri;
						liveIdAuthenticationConfiguration.MsoServiceLogin2Uri = endpointContainer.GetEndpoint(ServiceEndpointId.MsoServiceLogin2).Uri;
						liveIdAuthenticationConfiguration.MsoGetUserRealmUri = endpointContainer.GetEndpoint(ServiceEndpointId.MsoGetUserRealm).Uri;
						this.liveIdAuthenticationConfiguration = liveIdAuthenticationConfiguration;
					}
					return this.liveIdAuthenticationConfiguration;
				}
			}

			// Token: 0x17000DED RID: 3565
			// (get) Token: 0x06002EEC RID: 12012 RVA: 0x000BD31A File Offset: 0x000BB51A
			// (set) Token: 0x06002EED RID: 12013 RVA: 0x000BD322 File Offset: 0x000BB522
			public bool LightMode
			{
				get
				{
					return this.lightMode;
				}
				set
				{
					this.lightMode = value;
				}
			}

			// Token: 0x17000DEE RID: 3566
			// (get) Token: 0x06002EEE RID: 12014 RVA: 0x000BD32B File Offset: 0x000BB52B
			// (set) Token: 0x06002EEF RID: 12015 RVA: 0x000BD333 File Offset: 0x000BB533
			public bool MonitoringContext
			{
				get
				{
					return this.monitoringContext;
				}
				set
				{
					this.monitoringContext = value;
				}
			}

			// Token: 0x17000DEF RID: 3567
			// (get) Token: 0x06002EF0 RID: 12016 RVA: 0x000BD33C File Offset: 0x000BB53C
			// (set) Token: 0x06002EF1 RID: 12017 RVA: 0x000BD344 File Offset: 0x000BB544
			public string CasFqdn
			{
				get
				{
					return this.casFqdn;
				}
				set
				{
					this.casFqdn = value;
				}
			}

			// Token: 0x17000DF0 RID: 3568
			// (get) Token: 0x06002EF2 RID: 12018 RVA: 0x000BD34D File Offset: 0x000BB54D
			// (set) Token: 0x06002EF3 RID: 12019 RVA: 0x000BD355 File Offset: 0x000BB555
			public string VirtualDirectoryName { get; private set; }

			// Token: 0x17000DF1 RID: 3569
			// (get) Token: 0x06002EF4 RID: 12020 RVA: 0x000BD35E File Offset: 0x000BB55E
			// (set) Token: 0x06002EF5 RID: 12021 RVA: 0x000BD366 File Offset: 0x000BB566
			public VirtualDirectoryUriScope UrlType
			{
				get
				{
					return this.urlType;
				}
				set
				{
					this.urlType = value;
				}
			}

			// Token: 0x17000DF2 RID: 3570
			// (get) Token: 0x06002EF6 RID: 12022 RVA: 0x000BD36F File Offset: 0x000BB56F
			// (set) Token: 0x06002EF7 RID: 12023 RVA: 0x000BD378 File Offset: 0x000BB578
			public ExchangeVirtualDirectory VirtualDirectory
			{
				get
				{
					return this.virtualDirectory;
				}
				set
				{
					this.virtualDirectory = value;
					if (value != null)
					{
						this.VirtualDirectoryName = value.Name;
						if (this.UrlType == VirtualDirectoryUriScope.External)
						{
							this.baseUri = TestCasConnectivity.GetUrlWithTrailingSlash(value.ExternalUrl);
						}
						else
						{
							this.baseUri = TestCasConnectivity.GetUrlWithTrailingSlash(value.InternalUrl);
						}
						if (value is ExchangeWebAppVirtualDirectory)
						{
							WebAppVirtualDirectoryHelper.UpdateFromMetabase((ExchangeWebAppVirtualDirectory)value);
						}
					}
				}
			}

			// Token: 0x06002EF8 RID: 12024 RVA: 0x000BD3DB File Offset: 0x000BB5DB
			public TestCasConnectivityRunInstance()
			{
			}

			// Token: 0x06002EF9 RID: 12025 RVA: 0x000BD3E4 File Offset: 0x000BB5E4
			public TestCasConnectivityRunInstance(TestCasConnectivity.TestCasConnectivityRunInstance other)
			{
				this.allowUnsecureAccess = other.allowUnsecureAccess;
				this.autodiscoveredCas = other.autodiscoveredCas;
				this.liveRSTEndpointUri = other.liveRSTEndpointUri;
				this.baseUri = other.baseUri;
				this.credentials = other.credentials;
				this.deviceId = other.deviceId;
				this.exchangePrincipal = other.exchangePrincipal;
				this.safeToBuildUpnString = other.safeToBuildUpnString;
				this.trustAllCertificates = other.trustAllCertificates;
				this.urlType = other.urlType;
				this.casFqdn = other.casFqdn;
				this.ConnectionType = other.ConnectionType;
				this.Port = other.Port;
			}

			// Token: 0x0400217B RID: 8571
			internal ExchangePrincipal exchangePrincipal;

			// Token: 0x0400217C RID: 8572
			internal AsyncResult<CasTransactionOutcome> Result;

			// Token: 0x0400217D RID: 8573
			public Queue Outcomes;

			// Token: 0x0400217E RID: 8574
			internal CreateTestItemContext createTestItemContext;

			// Token: 0x0400217F RID: 8575
			public NetworkCredential credentials;

			// Token: 0x04002180 RID: 8576
			public string deviceId;

			// Token: 0x04002181 RID: 8577
			public Uri baseUri;

			// Token: 0x04002182 RID: 8578
			public Uri liveRSTEndpointUri;

			// Token: 0x04002183 RID: 8579
			private LiveIdAuthenticationConfiguration liveIdAuthenticationConfiguration;

			// Token: 0x04002184 RID: 8580
			public bool allowUnsecureAccess;

			// Token: 0x04002185 RID: 8581
			public bool trustAllCertificates;

			// Token: 0x04002186 RID: 8582
			public bool autodiscoveredCas;

			// Token: 0x04002187 RID: 8583
			public bool safeToBuildUpnString;

			// Token: 0x04002188 RID: 8584
			private bool lightMode;

			// Token: 0x04002189 RID: 8585
			private bool monitoringContext;

			// Token: 0x0400218A RID: 8586
			private string casFqdn;

			// Token: 0x0400218B RID: 8587
			public ProtocolConnectionType ConnectionType;

			// Token: 0x0400218C RID: 8588
			public int Port;

			// Token: 0x0400218D RID: 8589
			private VirtualDirectoryUriScope urlType;

			// Token: 0x0400218E RID: 8590
			private ExchangeVirtualDirectory virtualDirectory;
		}
	}
}
