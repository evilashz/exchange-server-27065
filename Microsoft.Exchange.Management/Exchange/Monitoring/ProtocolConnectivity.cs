using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Management.Automation;
using System.Net;
using System.Security;
using System.Security.Principal;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000518 RID: 1304
	public abstract class ProtocolConnectivity : TestCasConnectivity
	{
		// Token: 0x17000DF3 RID: 3571
		// (get) Token: 0x06002EFA RID: 12026 RVA: 0x000BD493 File Offset: 0x000BB693
		// (set) Token: 0x06002EFB RID: 12027 RVA: 0x000BD49B File Offset: 0x000BB69B
		[Parameter(Mandatory = false, ParameterSetName = "Identity", ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, Position = 0)]
		[Alias(new string[]
		{
			"Identity"
		})]
		[ValidateNotNullOrEmpty]
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

		// Token: 0x17000DF4 RID: 3572
		// (get) Token: 0x06002EFC RID: 12028 RVA: 0x000BD4A4 File Offset: 0x000BB6A4
		// (set) Token: 0x06002EFD RID: 12029 RVA: 0x000BD4AC File Offset: 0x000BB6AC
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public PSCredential MailboxCredential
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

		// Token: 0x17000DF5 RID: 3573
		// (get) Token: 0x06002EFE RID: 12030 RVA: 0x000BD4B5 File Offset: 0x000BB6B5
		// (set) Token: 0x06002EFF RID: 12031 RVA: 0x000BD4BD File Offset: 0x000BB6BD
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public ProtocolConnectionType ConnectionType
		{
			get
			{
				return this.connection;
			}
			set
			{
				this.connection = value;
			}
		}

		// Token: 0x17000DF6 RID: 3574
		// (get) Token: 0x06002F00 RID: 12032 RVA: 0x000BD4C6 File Offset: 0x000BB6C6
		// (set) Token: 0x06002F01 RID: 12033 RVA: 0x000BD4D3 File Offset: 0x000BB6D3
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
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

		// Token: 0x17000DF7 RID: 3575
		// (get) Token: 0x06002F02 RID: 12034 RVA: 0x000BD4E1 File Offset: 0x000BB6E1
		// (set) Token: 0x06002F03 RID: 12035 RVA: 0x000BD4E9 File Offset: 0x000BB6E9
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public int PortClientAccessServer
		{
			get
			{
				return this.port;
			}
			set
			{
				this.port = value;
			}
		}

		// Token: 0x17000DF8 RID: 3576
		// (get) Token: 0x06002F04 RID: 12036 RVA: 0x000BD4F2 File Offset: 0x000BB6F2
		// (set) Token: 0x06002F05 RID: 12037 RVA: 0x000BD514 File Offset: 0x000BB714
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		[ValidateRange(1, 120)]
		public int PerConnectionTimeout
		{
			get
			{
				return (int)(base.Fields["PerConnectionTimeout"] ?? 24);
			}
			set
			{
				base.Fields["PerConnectionTimeout"] = value;
			}
		}

		// Token: 0x17000DF9 RID: 3577
		// (get) Token: 0x06002F06 RID: 12038 RVA: 0x000BD52C File Offset: 0x000BB72C
		// (set) Token: 0x06002F07 RID: 12039 RVA: 0x000BD534 File Offset: 0x000BB734
		protected bool SendTestMessage
		{
			get
			{
				return this.sendTestMessage;
			}
			set
			{
				this.sendTestMessage = value;
			}
		}

		// Token: 0x17000DFA RID: 3578
		// (get) Token: 0x06002F08 RID: 12040 RVA: 0x000BD53D File Offset: 0x000BB73D
		// (set) Token: 0x06002F09 RID: 12041 RVA: 0x000BD545 File Offset: 0x000BB745
		protected string CurrentProtocol
		{
			get
			{
				return this.currrentProtocol;
			}
			set
			{
				this.currrentProtocol = value;
			}
		}

		// Token: 0x17000DFB RID: 3579
		// (get) Token: 0x06002F0A RID: 12042 RVA: 0x000BD54E File Offset: 0x000BB74E
		protected string MailSubject
		{
			get
			{
				return this.mailSubject;
			}
		}

		// Token: 0x17000DFC RID: 3580
		// (get) Token: 0x06002F0B RID: 12043 RVA: 0x000BD556 File Offset: 0x000BB756
		protected ADUser User
		{
			get
			{
				return this.user;
			}
		}

		// Token: 0x17000DFD RID: 3581
		// (get) Token: 0x06002F0C RID: 12044 RVA: 0x000BD55E File Offset: 0x000BB75E
		protected string MailboxFqdn
		{
			get
			{
				if (!string.IsNullOrEmpty(this.mailboxFqdn))
				{
					return this.mailboxFqdn;
				}
				return string.Empty;
			}
		}

		// Token: 0x17000DFE RID: 3582
		// (get) Token: 0x06002F0D RID: 12045 RVA: 0x000BD579 File Offset: 0x000BB779
		protected string CmdletMonitoringEventSource
		{
			get
			{
				return "MSExchange Monitoring " + this.CmdletNoun;
			}
		}

		// Token: 0x17000DFF RID: 3583
		// (get) Token: 0x06002F0E RID: 12046 RVA: 0x000BD58B File Offset: 0x000BB78B
		// (set) Token: 0x06002F0F RID: 12047 RVA: 0x000BD593 File Offset: 0x000BB793
		protected string CmdletNoun
		{
			get
			{
				return this.cmdletNoun;
			}
			set
			{
				this.cmdletNoun = value;
			}
		}

		// Token: 0x17000E00 RID: 3584
		// (get) Token: 0x06002F10 RID: 12048 RVA: 0x000BD59C File Offset: 0x000BB79C
		// (set) Token: 0x06002F11 RID: 12049 RVA: 0x000BD5A4 File Offset: 0x000BB7A4
		protected string MailSubjectPrefix
		{
			get
			{
				return this.mailSubjectPrefix;
			}
			set
			{
				this.mailSubjectPrefix = value;
			}
		}

		// Token: 0x17000E01 RID: 3585
		// (get) Token: 0x06002F12 RID: 12050 RVA: 0x000BD5AD File Offset: 0x000BB7AD
		protected override string TransactionSuccessEventMessage
		{
			get
			{
				return Strings.ProtocolTransactionsSucceeded(this.currrentProtocol);
			}
		}

		// Token: 0x17000E02 RID: 3586
		// (get) Token: 0x06002F13 RID: 12051 RVA: 0x000BD5C0 File Offset: 0x000BB7C0
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				string text = Strings.ProtocolConfrimationMessage(this.CurrentProtocol);
				text = text + Strings.SelectedConnectionType(this.connection.ToString()) + "\r\n\r\n";
				if (this.TrustAnySSLCertificate)
				{
					text = text + Strings.ProtolcolWarnTrustAllCertificates.ToString() + "\r\n\r\n";
				}
				if (this.MailboxCredential != null)
				{
					try
					{
						NetworkCredential networkCredential = this.MailboxCredential.GetNetworkCredential();
						text = text + Strings.CasHealthWarnUserCredentials(networkCredential.Domain + "\\" + networkCredential.UserName).ToString() + "\r\n\r\n";
					}
					catch (PSArgumentException)
					{
						text = text + Strings.CasHealthWarnUserCredentials(this.MailboxCredential.UserName).ToString() + "\r\n\r\n";
					}
				}
				return new LocalizedString(text);
			}
		}

		// Token: 0x06002F14 RID: 12052 RVA: 0x000BD6C4 File Offset: 0x000BB8C4
		internal static string ShortErrorMessageFromException(Exception exception)
		{
			if (exception == null)
			{
				throw new ArgumentNullException("exception");
			}
			string result = string.Empty;
			result = exception.Message;
			if (exception.InnerException != null)
			{
				result = Strings.ProtocolTransactionShortErrorMsgFromExceptionWithInnerException(exception.GetType().ToString(), exception.Message, exception.InnerException.GetType().ToString(), exception.InnerException.Message);
			}
			else
			{
				result = Strings.ProtocolTransactionShortErrorMsgFromException(exception.GetType().ToString(), exception.Message);
			}
			return result;
		}

		// Token: 0x06002F15 RID: 12053 RVA: 0x000BD74C File Offset: 0x000BB94C
		internal static LocalizedException SetUserInformation(TestCasConnectivity.TestCasConnectivityRunInstance instance, IConfigDataProvider session, ref ADUser user)
		{
			if (session == null)
			{
				throw new LocalizedException(Strings.ErrorNullParameter("session"));
			}
			if (instance == null)
			{
				throw new LocalizedException(Strings.ErrorNullParameter("instance"));
			}
			IRecipientSession recipientSession = session as IRecipientSession;
			if (recipientSession == null)
			{
				throw new LocalizedException(Strings.ErrorNullParameter("recipientSession"));
			}
			if (!string.IsNullOrEmpty(instance.credentials.Domain) && instance.credentials.UserName.IndexOf('@') == -1)
			{
				WindowsIdentity windowsIdentity = null;
				string text = instance.credentials.UserName + "@" + instance.credentials.Domain;
				try
				{
					windowsIdentity = new WindowsIdentity(text);
				}
				catch (SecurityException ex)
				{
					return new CasHealthUserNotFoundException(text, ex.Message);
				}
				using (windowsIdentity)
				{
					user = (recipientSession.FindBySid(windowsIdentity.User) as ADUser);
					if (user == null)
					{
						return new AdUserNotFoundException(text, string.Empty);
					}
					goto IL_174;
				}
			}
			user = null;
			RecipientIdParameter recipientIdParameter = RecipientIdParameter.Parse(instance.credentials.UserName);
			IEnumerable<ADUser> objects = recipientIdParameter.GetObjects<ADUser>(null, recipientSession);
			if (objects != null)
			{
				IEnumerator<ADUser> enumerator = objects.GetEnumerator();
				if (enumerator != null && enumerator.MoveNext())
				{
					user = enumerator.Current;
				}
				if (enumerator.MoveNext())
				{
					user = null;
					return new AdUserNotUniqueException(instance.credentials.UserName);
				}
			}
			if (user == null)
			{
				return new AdUserNotFoundException(instance.credentials.UserName, string.Empty);
			}
			IL_174:
			return null;
		}

		// Token: 0x06002F16 RID: 12054 RVA: 0x000BD8F0 File Offset: 0x000BBAF0
		internal static LocalizedException SetExchangePrincipalInformation(TestCasConnectivity.TestCasConnectivityRunInstance instance, ADUser user)
		{
			if (instance == null)
			{
				return new LocalizedException(Strings.ErrorNullParameter("instance"));
			}
			if (user == null)
			{
				return new LocalizedException(Strings.ErrorNullParameter("user"));
			}
			if (instance.exchangePrincipal == null)
			{
				try
				{
					if (user is ADSystemMailbox)
					{
						throw new NotSupportedException("not supported for ADSystemMailbox");
					}
					instance.exchangePrincipal = ExchangePrincipal.FromADUser(user, null);
				}
				catch (ObjectNotFoundException result)
				{
					return result;
				}
				catch (InvalidCastException innerException)
				{
					return new LocalizedException(Strings.ErrorInvalidCasting, innerException);
				}
				catch (LocalizedException result2)
				{
					return result2;
				}
			}
			return null;
		}

		// Token: 0x06002F17 RID: 12055 RVA: 0x000BD994 File Offset: 0x000BBB94
		protected override string TransactionFailuresEventMessage(string detailedInformation)
		{
			return Strings.ProtocolTransactionsFailed(this.currrentProtocol, detailedInformation);
		}

		// Token: 0x06002F18 RID: 12056 RVA: 0x000BD9A7 File Offset: 0x000BBBA7
		protected override string TransactionWarningsEventMessage(string detailedInformation)
		{
			return Strings.ProtocolTransactionWarnings(this.currrentProtocol, detailedInformation);
		}

		// Token: 0x06002F19 RID: 12057 RVA: 0x000BD9BC File Offset: 0x000BBBBC
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			try
			{
				base.InternalValidate();
				if (this.casToTest == null && this.clientAccessServer == null)
				{
					CasHealthMustSpecifyCasException exception = new CasHealthMustSpecifyCasException();
					base.WriteMonitoringEvent(2020, this.CmdletMonitoringEventSource, EventTypeEnumeration.Error, base.ShortErrorMsgFromException(exception));
				}
			}
			catch (ADTransientException exception2)
			{
				base.CasConnectivityWriteError(exception2, ErrorCategory.ObjectNotFound, null);
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

		// Token: 0x06002F1A RID: 12058 RVA: 0x000BDA58 File Offset: 0x000BBC58
		protected override void InternalBeginProcessing()
		{
			base.InternalBeginProcessing();
			this.recipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(base.DomainController, true, ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 617, "InternalBeginProcessing", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Monitoring\\Common\\ProtocolConnectivity.cs");
			if (!this.recipientSession.IsReadConnectionAvailable())
			{
				this.recipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 625, "InternalBeginProcessing", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Monitoring\\Common\\ProtocolConnectivity.cs");
			}
			this.recipientSession.UseGlobalCatalog = true;
			this.recipientSession.ServerTimeout = new TimeSpan?(TimeSpan.FromSeconds(15.0));
			this.systemConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(base.DomainController, false, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 634, "InternalBeginProcessing", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Monitoring\\Common\\ProtocolConnectivity.cs");
			this.systemConfigurationSession.ServerTimeout = new TimeSpan?(TimeSpan.FromSeconds(15.0));
		}

		// Token: 0x06002F1B RID: 12059 RVA: 0x000BDB48 File Offset: 0x000BBD48
		protected override List<CasTransactionOutcome> ExecuteTests(TestCasConnectivity.TestCasConnectivityRunInstance instance)
		{
			CasTransactionOutcome casTransactionOutcome = null;
			if (!this.SetUpConnectivityTests(instance, out casTransactionOutcome))
			{
				instance.Outcomes.Enqueue(casTransactionOutcome);
				instance.Result.Outcomes.Add(casTransactionOutcome);
				instance.Result.Complete();
				return null;
			}
			this.ExecuteTestConnectivity(instance);
			return null;
		}

		// Token: 0x06002F1C RID: 12060 RVA: 0x000BDB94 File Offset: 0x000BBD94
		protected override List<TestCasConnectivity.TestCasConnectivityRunInstance> PopulateInfoPerCas(TestCasConnectivity.TestCasConnectivityRunInstance instance, List<CasTransactionOutcome> outcomeList)
		{
			List<TestCasConnectivity.TestCasConnectivityRunInstance> list = new List<TestCasConnectivity.TestCasConnectivityRunInstance>();
			if (string.IsNullOrEmpty(instance.CasFqdn) || this.casToTest == null)
			{
				CasHealthMustSpecifyCasException exception = new CasHealthMustSpecifyCasException();
				base.CasConnectivityWriteError(exception, ErrorCategory.ObjectNotFound, null);
			}
			else
			{
				list.Add(instance);
			}
			return list;
		}

		// Token: 0x06002F1D RID: 12061 RVA: 0x000BDBD6 File Offset: 0x000BBDD6
		protected virtual void ExecuteTestConnectivity(TestCasConnectivity.TestCasConnectivityRunInstance instance)
		{
			throw new NotImplementedException("ExecuteTestConnectivity");
		}

		// Token: 0x06002F1E RID: 12062 RVA: 0x000BDBE2 File Offset: 0x000BBDE2
		protected virtual string MonitoringLatencyPerformanceCounter()
		{
			return string.Empty;
		}

		// Token: 0x06002F1F RID: 12063 RVA: 0x000BDBEC File Offset: 0x000BBDEC
		protected Exception CheckDatabaseStatus(string databaseName, string mailboxServerName, ref bool databaseResult)
		{
			databaseResult = false;
			LocalizedString localizedString = default(LocalizedString);
			MailboxDatabase mailboxDatabase = null;
			try
			{
				if (string.IsNullOrEmpty(mailboxServerName))
				{
					throw new ArgumentNullException("mailboxServerName");
				}
				if (string.IsNullOrEmpty(databaseName))
				{
					throw new ArgumentNullException("databaseName");
				}
				Server server = base.FindServerByHostName(mailboxServerName);
				if (server == null)
				{
					return new CasHealthMailboxServerNotFoundException(mailboxServerName);
				}
				MailboxDatabase[] mailboxDatabases = server.GetMailboxDatabases();
				foreach (MailboxDatabase mailboxDatabase2 in mailboxDatabases)
				{
					if (string.Compare(databaseName, mailboxDatabase2.Name, true, CultureInfo.InvariantCulture) == 0)
					{
						mailboxDatabase = mailboxDatabase2;
						break;
					}
				}
			}
			catch (ArgumentException result)
			{
				return result;
			}
			catch (ADTransientException result2)
			{
				return result2;
			}
			if (mailboxDatabase == null)
			{
				localizedString = new LocalizedString(Strings.ErrorMailboxDatabaseNotFound(databaseName));
				return new LocalizedException(localizedString);
			}
			if (mailboxDatabase.Recovery)
			{
				string databaseId = mailboxDatabase.ServerName + "\\\\" + mailboxDatabase.Name;
				RecoveryMailboxDatabaseNotMonitoredException exception = new RecoveryMailboxDatabaseNotMonitoredException(databaseId);
				base.WriteMonitoringEvent(2009, this.CmdletMonitoringEventSource, EventTypeEnumeration.Error, base.ShortErrorMsgFromException(exception));
			}
			else
			{
				using (ExRpcAdmin exRpcAdmin = ExRpcAdmin.Create("Client=Management", mailboxDatabase.ServerName, null, null, null))
				{
					MdbStatus[] array2 = exRpcAdmin.ListMdbStatus(new Guid[]
					{
						mailboxDatabase.Guid
					});
					if (array2.Length != 0)
					{
						databaseResult = ((array2[0].Status & MdbStatusFlags.Online) == MdbStatusFlags.Online);
						if (!databaseResult)
						{
							base.WriteMonitoringEvent(2023, this.CmdletMonitoringEventSource, EventTypeEnumeration.Error, base.ShortErrorMsgFromException(new LocalizedException(Strings.ErrorDatabaseOffline)));
						}
					}
				}
			}
			return null;
		}

		// Token: 0x06002F20 RID: 12064 RVA: 0x000BDDBC File Offset: 0x000BBFBC
		protected override List<CasTransactionOutcome> BuildPerformanceOutcomes(TestCasConnectivity.TestCasConnectivityRunInstance instance, string mbxFqdn)
		{
			return new List<CasTransactionOutcome>
			{
				new CasTransactionOutcome(instance.CasFqdn, Strings.TestProtocolConnectivity(this.CurrentProtocol), Strings.ProtocolConnectivityScenario(this.CurrentProtocol), this.MonitoringLatencyPerformanceCounter(), base.LocalSiteName, true, instance.credentials.UserName)
			};
		}

		// Token: 0x06002F21 RID: 12065 RVA: 0x000BDE1C File Offset: 0x000BC01C
		private bool SetUpConnectivityTests(TestCasConnectivity.TestCasConnectivityRunInstance instance, out CasTransactionOutcome outcome)
		{
			MailboxSession mailboxSession = null;
			outcome = new CasTransactionOutcome(base.CasFqdn, Strings.ValidatingTestCasConnectivityRunInstance, Strings.ValidatingTestCasConnectivityRunInstance, this.MonitoringLatencyPerformanceCounter(), base.LocalSiteName, true, string.Empty);
			if (instance == null)
			{
				base.WriteMonitoringEvent(2020, this.CmdletMonitoringEventSource, EventTypeEnumeration.Error, base.ShortErrorMsgFromException(new ArgumentNullException("instance")));
				outcome.Update(CasTransactionResultEnum.Failure);
				return false;
			}
			this.userName = instance.credentials.UserName;
			if (!this.SetUpConnTestValidateAndMakeADUser(instance, out outcome))
			{
				return false;
			}
			if (!this.SetUpConnTestSetExchangePrincipal(instance, out outcome))
			{
				return false;
			}
			if (instance.credentials.Password == null)
			{
				base.WriteMonitoringEvent(2020, this.CmdletMonitoringEventSource, EventTypeEnumeration.Error, base.ShortErrorMsgFromException(new ArgumentNullException("password")));
				outcome.Update(CasTransactionResultEnum.Failure);
				return false;
			}
			if (instance.LightMode)
			{
				return true;
			}
			try
			{
				if (!this.SetUpConnTestSetCreateMailboxSession(instance, out mailboxSession, out outcome))
				{
					return false;
				}
				if (!this.SetUpConnTestSendTestMessage(instance, mailboxSession, out outcome))
				{
					return false;
				}
			}
			finally
			{
				if (mailboxSession != null)
				{
					mailboxSession.Dispose();
					mailboxSession = null;
				}
			}
			return true;
		}

		// Token: 0x06002F22 RID: 12066 RVA: 0x000BDF3C File Offset: 0x000BC13C
		private bool SetUpConnTestValidateAndMakeADUser(TestCasConnectivity.TestCasConnectivityRunInstance instance, out CasTransactionOutcome outcome)
		{
			outcome = new CasTransactionOutcome(base.CasFqdn, Strings.ValidatingUserObject, Strings.ValidatingUserObjectDescription, this.MonitoringLatencyPerformanceCounter(), base.LocalSiteName, false, this.userName);
			LocalizedException ex = ProtocolConnectivity.SetUserInformation(instance, this.recipientSession, ref this.user);
			if (ex != null)
			{
				base.WriteMonitoringEvent(2001, this.CmdletMonitoringEventSource, EventTypeEnumeration.Error, base.ShortErrorMsgFromException(ex));
				outcome.Update(CasTransactionResultEnum.Failure);
				return false;
			}
			if (this.user != null && !this.user.IsMailboxEnabled)
			{
				UserIsNotMailBoxEnabledException exception = new UserIsNotMailBoxEnabledException(instance.credentials.UserName);
				base.WriteMonitoringEvent(2003, this.CmdletMonitoringEventSource, EventTypeEnumeration.Error, base.ShortErrorMsgFromException(exception));
				outcome.Update(CasTransactionResultEnum.Failure);
				return false;
			}
			outcome.Update(CasTransactionResultEnum.Success);
			return true;
		}

		// Token: 0x06002F23 RID: 12067 RVA: 0x000BE008 File Offset: 0x000BC208
		private bool SetUpConnTestSetExchangePrincipal(TestCasConnectivity.TestCasConnectivityRunInstance instance, out CasTransactionOutcome outcome)
		{
			outcome = null;
			if (instance.exchangePrincipal == null)
			{
				outcome = new CasTransactionOutcome(base.CasFqdn, Strings.CreateExchangePrincipalObject, Strings.CreateExchangePrincipalObject, this.MonitoringLatencyPerformanceCounter(), base.LocalSiteName, false, this.userName);
				LocalizedException ex = ProtocolConnectivity.SetExchangePrincipalInformation(instance, this.User);
				if (ex != null)
				{
					base.WriteMonitoringEvent(2021, this.CmdletMonitoringEventSource, EventTypeEnumeration.Error, base.ShortErrorMsgFromException(ex));
					outcome.Update(CasTransactionResultEnum.Failure);
					return false;
				}
				outcome.Update(CasTransactionResultEnum.Success);
			}
			return true;
		}

		// Token: 0x06002F24 RID: 12068 RVA: 0x000BE090 File Offset: 0x000BC290
		private bool SetUpConnTestSetCreateMailboxSession(TestCasConnectivity.TestCasConnectivityRunInstance instance, out MailboxSession mailboxSession, out CasTransactionOutcome outcome)
		{
			this.mailboxFqdn = instance.exchangePrincipal.MailboxInfo.Location.ServerFqdn;
			string text = string.Empty;
			mailboxSession = null;
			outcome = null;
			try
			{
				outcome = this.BuildOutcome(Strings.CreateMailboxSession, Strings.CreateMailboxSessionDetail(this.User.PrimarySmtpAddress.ToString()), instance);
				mailboxSession = MailboxSession.OpenAsAdmin(instance.exchangePrincipal, CultureInfo.InvariantCulture, "Client=Monitoring;Action=System Management");
			}
			catch (StorageTransientException exception)
			{
				text = ProtocolConnectivity.ShortErrorMessageFromException(exception);
				new MailboxCreationException(this.User.PrimarySmtpAddress.ToString(), text);
				outcome.Update(CasTransactionResultEnum.Failure, Strings.ErrorMailboxCreationFailure(this.User.PrimarySmtpAddress.ToString(), text));
				return false;
			}
			catch (StoragePermanentException exception2)
			{
				text = ProtocolConnectivity.ShortErrorMessageFromException(exception2);
				new MailboxCreationException(this.User.PrimarySmtpAddress.ToString(), text);
				outcome.Update(CasTransactionResultEnum.Failure, Strings.ErrorMailboxCreationFailure(this.User.PrimarySmtpAddress.ToString(), text));
				return false;
			}
			catch (ArgumentException exception3)
			{
				text = ProtocolConnectivity.ShortErrorMessageFromException(exception3);
				LocalizedException exception4 = new LocalizedException(new LocalizedString(text));
				base.WriteMonitoringEvent(2013, this.CmdletMonitoringEventSource, EventTypeEnumeration.Error, base.ShortErrorMsgFromException(exception4));
				outcome.Update(CasTransactionResultEnum.Failure);
				return false;
			}
			outcome.Update(CasTransactionResultEnum.Success);
			return true;
		}

		// Token: 0x06002F25 RID: 12069 RVA: 0x000BE240 File Offset: 0x000BC440
		private bool SetUpConnTestSendTestMessage(TestCasConnectivity.TestCasConnectivityRunInstance instance, MailboxSession mailboxSession, out CasTransactionOutcome outcome)
		{
			outcome = null;
			if (this.SendTestMessage)
			{
				outcome = this.BuildOutcome(Strings.DatabaseStatus, Strings.CheckDatabaseStatus(this.user.Database.Name), instance);
				bool flag = false;
				Exception ex = this.CheckDatabaseStatus(this.user.Database.Name, this.MailboxFqdn, ref flag);
				if (!flag)
				{
					if (ex == null)
					{
						goto IL_16C;
					}
				}
				try
				{
					outcome = this.BuildOutcome(Strings.SendMessage, Strings.SendMessage, instance);
					using (MessageItem messageItem = MessageItem.Create(mailboxSession, mailboxSession.GetDefaultFolderId(DefaultFolderType.Inbox)))
					{
						this.mailSubject = this.MailSubjectPrefix + Guid.NewGuid().ToString();
						messageItem.Subject = this.MailSubject;
						using (TextWriter textWriter = messageItem.Body.OpenTextWriter(BodyFormat.TextPlain))
						{
							textWriter.Write("Body of -");
							textWriter.Write(this.MailSubject);
						}
						messageItem.IsReadReceiptRequested = false;
						messageItem.Save(SaveMode.NoConflictResolution);
						instance.Outcomes.Enqueue(Strings.TestMessageSent(this.MailSubject, this.user.PrimarySmtpAddress.ToString()));
					}
					return true;
				}
				catch (LocalizedException exception)
				{
					base.WriteMonitoringEvent(2008, this.CmdletMonitoringEventSource, EventTypeEnumeration.Error, base.ShortErrorMsgFromException(exception));
					outcome.Update(CasTransactionResultEnum.Failure);
					return false;
				}
				IL_16C:
				if (ex != null)
				{
					outcome.Update(CasTransactionResultEnum.Failure, ex.Message);
				}
				else
				{
					outcome.Update(CasTransactionResultEnum.Failure);
				}
				return false;
			}
			return true;
		}

		// Token: 0x04002190 RID: 8592
		protected const string CrlfText = "\r\n\r\n";

		// Token: 0x04002191 RID: 8593
		protected const double LatencyPerformanceIncaseOfError = -1.0;

		// Token: 0x04002192 RID: 8594
		private const int DefaultConnectionCacheSize = 50;

		// Token: 0x04002193 RID: 8595
		private const int DefaultADOperationsTimeoutInSeconds = 15;

		// Token: 0x04002194 RID: 8596
		private const int DefaultConnectionTimeoutInSeconds = 24;

		// Token: 0x04002195 RID: 8597
		private string mailboxFqdn;

		// Token: 0x04002196 RID: 8598
		private string cmdletNoun = "ProtocolConnectivity";

		// Token: 0x04002197 RID: 8599
		private string mailSubjectPrefix = "Connectivity";

		// Token: 0x04002198 RID: 8600
		private string mailSubject;

		// Token: 0x04002199 RID: 8601
		private string userName;

		// Token: 0x0400219A RID: 8602
		private ADUser user;

		// Token: 0x0400219B RID: 8603
		private int port;

		// Token: 0x0400219C RID: 8604
		private ProtocolConnectionType connection;

		// Token: 0x0400219D RID: 8605
		private string currrentProtocol;

		// Token: 0x0400219E RID: 8606
		private bool sendTestMessage;

		// Token: 0x0400219F RID: 8607
		private IRecipientSession recipientSession;

		// Token: 0x040021A0 RID: 8608
		private IConfigurationSession systemConfigurationSession;

		// Token: 0x02000519 RID: 1305
		internal new static class EventId
		{
			// Token: 0x040021A1 RID: 8609
			public const int TransactionSucceeded = 2000;

			// Token: 0x040021A2 RID: 8610
			public const int UserNotFound = 2001;

			// Token: 0x040021A3 RID: 8611
			public const int SecurityErrorFound = 2002;

			// Token: 0x040021A4 RID: 8612
			public const int MailboxNotEnabled = 2003;

			// Token: 0x040021A5 RID: 8613
			public const int OperationOnOldServer = 2004;

			// Token: 0x040021A6 RID: 8614
			public const int SessionObjectNotCreated = 2005;

			// Token: 0x040021A7 RID: 8615
			public const int NetworkCredentialIsNull = 2006;

			// Token: 0x040021A8 RID: 8616
			public const int NodeDoesNotOwnExchangeVirtualServer = 2007;

			// Token: 0x040021A9 RID: 8617
			public const int MessageNotSent = 2008;

			// Token: 0x040021AA RID: 8618
			public const int RecoveryMailboxDatabaseNotMonitored = 2009;

			// Token: 0x040021AB RID: 8619
			public const int UserNotUnique = 2010;

			// Token: 0x040021AC RID: 8620
			public const int ServerIsNotCas = 2011;

			// Token: 0x040021AD RID: 8621
			public const int CasServerNotFound = 2012;

			// Token: 0x040021AE RID: 8622
			public const int ServiceNotRunning = 2013;

			// Token: 0x040021AF RID: 8623
			public const int InvalidMonadTask = 2014;

			// Token: 0x040021B0 RID: 8624
			public const int MailboxDatabaseNotFound = 2015;

			// Token: 0x040021B1 RID: 8625
			public const int MailboxDatabaseNotUnique = 2016;

			// Token: 0x040021B2 RID: 8626
			public const int CasServerNotSpecified = 2017;

			// Token: 0x040021B3 RID: 8627
			public const int CasServerNotRpcEnabled = 2018;

			// Token: 0x040021B4 RID: 8628
			public const int TransactionFailed = 2019;

			// Token: 0x040021B5 RID: 8629
			public const int InvalidData = 2020;

			// Token: 0x040021B6 RID: 8630
			public const int ExchangePrincipalCreationFailed = 2021;

			// Token: 0x040021B7 RID: 8631
			public const int MailboxAndCasSeverInDiffrentDomain = 2022;

			// Token: 0x040021B8 RID: 8632
			public const int MailboxDatabaseOffline = 2023;
		}
	}
}
