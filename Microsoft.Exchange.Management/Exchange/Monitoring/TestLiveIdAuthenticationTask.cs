using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Management.Automation;
using System.Net;
using System.Runtime.InteropServices;
using System.Security;
using System.ServiceModel;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Security.Authentication;
using Microsoft.Exchange.Security.Authentication.FederatedAuthService;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x0200001F RID: 31
	[Cmdlet("Test", "LiveIdAuthentication", SupportsShouldProcess = true)]
	public sealed class TestLiveIdAuthenticationTask : Task
	{
		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x00004EB7 File Offset: 0x000030B7
		// (set) Token: 0x060000E2 RID: 226 RVA: 0x00004EDD File Offset: 0x000030DD
		[Parameter(Mandatory = false)]
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

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x00004EF5 File Offset: 0x000030F5
		// (set) Token: 0x060000E4 RID: 228 RVA: 0x00004F15 File Offset: 0x00003115
		[Parameter(Mandatory = false, ParameterSetName = "Default")]
		public ServerIdParameter Server
		{
			get
			{
				return ((ServerIdParameter)base.Fields["Server"]) ?? new ServerIdParameter();
			}
			set
			{
				base.Fields["Server"] = value;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x00004F28 File Offset: 0x00003128
		// (set) Token: 0x060000E6 RID: 230 RVA: 0x00004F49 File Offset: 0x00003149
		[Parameter(Mandatory = false)]
		public LiveIdAuthenticationUserTypeEnum UserType
		{
			get
			{
				return (LiveIdAuthenticationUserTypeEnum)(base.Fields["UserType"] ?? LiveIdAuthenticationUserTypeEnum.ManagedConsumer);
			}
			set
			{
				base.Fields["UserType"] = value;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x00004F61 File Offset: 0x00003161
		// (set) Token: 0x060000E8 RID: 232 RVA: 0x00004F78 File Offset: 0x00003178
		[Parameter(Mandatory = false, ValueFromPipeline = true)]
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

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x00004F8B File Offset: 0x0000318B
		// (set) Token: 0x060000EA RID: 234 RVA: 0x00004F93 File Offset: 0x00003193
		[Parameter(Mandatory = false, ValueFromPipeline = true)]
		public SwitchParameter TestLegacyAPI { get; set; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000EB RID: 235 RVA: 0x00004F9C File Offset: 0x0000319C
		// (set) Token: 0x060000EC RID: 236 RVA: 0x00004FA4 File Offset: 0x000031A4
		[Parameter(Mandatory = false, ValueFromPipeline = true)]
		public SwitchParameter SyncADBackendOnly { get; set; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000ED RID: 237 RVA: 0x00004FAD File Offset: 0x000031AD
		// (set) Token: 0x060000EE RID: 238 RVA: 0x00004FD3 File Offset: 0x000031D3
		[Parameter(Mandatory = false, ValueFromPipeline = true)]
		public SwitchParameter PreferOfflineAuth
		{
			get
			{
				return (SwitchParameter)(base.Fields["PreferOfflineAuth"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["PreferOfflineAuth"] = value;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000EF RID: 239 RVA: 0x00004FEB File Offset: 0x000031EB
		// (set) Token: 0x060000F0 RID: 240 RVA: 0x0000500C File Offset: 0x0000320C
		[Parameter(Mandatory = false, ValueFromPipeline = true)]
		public FailoverFlags TestFailOver
		{
			get
			{
				return (FailoverFlags)(base.Fields["TestFailOver"] ?? FailoverFlags.None);
			}
			set
			{
				base.Fields["TestFailOver"] = value;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x00005024 File Offset: 0x00003224
		// (set) Token: 0x060000F2 RID: 242 RVA: 0x0000504A File Offset: 0x0000324A
		[Parameter(Mandatory = false, ValueFromPipeline = true)]
		public SwitchParameter IgnoreLowPasswordConfidence
		{
			get
			{
				return (SwitchParameter)(base.Fields["IgnoreLowPasswordConfidence"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["IgnoreLowPasswordConfidence"] = value;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x00005062 File Offset: 0x00003262
		// (set) Token: 0x060000F4 RID: 244 RVA: 0x00005088 File Offset: 0x00003288
		[Parameter(Mandatory = false, ValueFromPipeline = true)]
		public SwitchParameter LiveIdXmlAuth
		{
			get
			{
				return (SwitchParameter)(base.Fields["LiveIdXmlAuth"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["LiveIdXmlAuth"] = value;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x000050A0 File Offset: 0x000032A0
		internal ITopologyConfigurationSession SystemConfigurationSession
		{
			get
			{
				if (this.systemConfigurationSession == null)
				{
					this.systemConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 339, "SystemConfigurationSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Monitoring\\Authentication\\TestLiveIdAuthenticationTask.cs");
				}
				return this.systemConfigurationSession;
			}
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x000050D5 File Offset: 0x000032D5
		protected override bool IsKnownException(Exception e)
		{
			return base.IsKnownException(e) || MonitoringHelper.IsKnownExceptionForMonitoring(e);
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x000050FC File Offset: 0x000032FC
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			try
			{
				base.InternalValidate();
				if (!base.HasErrors)
				{
					this.ServerObject = TestLiveIdAuthenticationTask.EnsureSingleObject<Server>(() => this.Server.GetObjects<Server>(null, this.SystemConfigurationSession));
					if (this.ServerObject == null)
					{
						throw new CasHealthMailboxServerNotFoundException(this.Server.ToString());
					}
					if (this.MailboxCredential == null)
					{
						this.GetTestMailbox();
					}
				}
			}
			catch (LocalizedException exception)
			{
				this.WriteError(exception, ErrorCategory.OperationStopped, this, true);
			}
			finally
			{
				TaskLogger.LogExit();
			}
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00005198 File Offset: 0x00003398
		protected override void InternalBeginProcessing()
		{
			if (this.MonitoringContext)
			{
				this.monitoringData = new MonitoringData();
			}
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x000051B4 File Offset: 0x000033B4
		protected override void InternalProcessRecord()
		{
			base.InternalBeginProcessing();
			TaskLogger.LogEnter();
			try
			{
				this.VerifyServiceIsRunning();
				LiveIdAuthenticationOutcome sendToPipeline = new LiveIdAuthenticationOutcome(this.Server.ToString(), this.MailboxCredential.UserName);
				this.PerformLiveIdAuthenticationTest(ref sendToPipeline);
				base.WriteObject(sendToPipeline);
			}
			catch (LocalizedException e)
			{
				this.HandleException(e);
			}
			finally
			{
				if (this.MonitoringContext)
				{
					base.WriteObject(this.monitoringData);
				}
				TaskLogger.LogExit();
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000FA RID: 250 RVA: 0x00005248 File Offset: 0x00003448
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageTestLiveIdAuthenticationIdentity(this.MailboxCredential.UserName);
			}
		}

		// Token: 0x060000FB RID: 251 RVA: 0x0000525C File Offset: 0x0000345C
		private void VerifyServiceIsRunning()
		{
			string name = "MSExchangeProtectedServiceHost";
			try
			{
				using (ServiceController serviceController = new ServiceController(name, this.Server.Fqdn))
				{
					if (serviceController.Status != ServiceControllerStatus.Running)
					{
						base.WriteVerbose(new LocalizedString("Service is not Running, Trying to start the service"));
						serviceController.Start();
						base.WriteVerbose(new LocalizedString("Service has been Started"));
					}
				}
			}
			catch (InvalidOperationException ex)
			{
				base.WriteVerbose(new LocalizedString("InvalidOperationException while verifying the service status : " + ex.ToString()));
			}
			catch (Win32Exception ex2)
			{
				base.WriteVerbose(new LocalizedString("Win32Exception while verifying the service status : " + ex2.ToString()));
			}
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00005324 File Offset: 0x00003524
		private void KillAndStartService()
		{
			string text = "Microsoft.Exchange.ProtectedServiceHost";
			int num = 30000;
			int millisecondsTimeout = 500;
			try
			{
				base.WriteVerbose(new LocalizedString("Before killing and restarting making sure the problem exists"));
				bool flag;
				LiveIdAuthenticationError liveIdAuthenticationError;
				string text2;
				TimeSpan timeSpan;
				this.InternalPerformLiveIdAuthentication(out flag, out liveIdAuthenticationError, out text2, out timeSpan);
				if (liveIdAuthenticationError == LiveIdAuthenticationError.None)
				{
					base.WriteVerbose(new LocalizedString("Issue was self fixed"));
				}
				else
				{
					Process[] processesByName = Process.GetProcessesByName(text);
					if (processesByName.Length != 1)
					{
						throw new InvalidOperationException(string.Format("{0} process not found", text));
					}
					processesByName[0].Kill();
					Stopwatch stopwatch = Stopwatch.StartNew();
					for (long num2 = 0L; num2 < (long)num; num2 = stopwatch.ElapsedMilliseconds)
					{
						stopwatch.Start();
						processesByName = Process.GetProcessesByName(text);
						if (processesByName.Length == 0)
						{
							break;
						}
						Thread.Sleep(millisecondsTimeout);
						stopwatch.Stop();
					}
					this.VerifyServiceIsRunning();
				}
			}
			catch (Exception ex)
			{
				base.WriteVerbose(new LocalizedString("Exception while killing and starting the service : " + ex.ToString()));
			}
		}

		// Token: 0x060000FD RID: 253 RVA: 0x0000541C File Offset: 0x0000361C
		private void InternalPerformLiveIdAuthentication(out bool success, out LiveIdAuthenticationError error, out string iisLogs, out TimeSpan latency)
		{
			int num = 1009;
			string uri = string.Format("net.tcp://{0}:{1}/Microsoft.Exchange.Security.Authentication.FederatedAuthService", this.Server.ToString(), num);
			NetTcpBinding binding = new NetTcpBinding(SecurityMode.Transport);
			EndpointAddress remoteAddress = new EndpointAddress(uri);
			using (AuthServiceClient authServiceClient = new AuthServiceClient(binding, remoteAddress))
			{
				error = LiveIdAuthenticationError.None;
				byte[] bytes = Encoding.Default.GetBytes(this.MailboxCredential.UserName);
				byte[] bytes2 = Encoding.Default.GetBytes(this.ConvertToUnsecureString(this.MailboxCredential.Password));
				Stopwatch stopwatch = Stopwatch.StartNew();
				try
				{
					TestFailoverFlags testFailoverFlags;
					if (this.TestFailOver == FailoverFlags.Random)
					{
						if (!this.PreferOfflineAuth)
						{
							TestFailoverFlags[] array;
							if (this.UserType == LiveIdAuthenticationUserTypeEnum.ManagedConsumer)
							{
								array = new TestFailoverFlags[]
								{
									TestFailoverFlags.HRDRequest,
									TestFailoverFlags.HRDResponse,
									TestFailoverFlags.LiveIdRequest,
									TestFailoverFlags.LiveIdResponse,
									TestFailoverFlags.OrgIdRequest,
									TestFailoverFlags.OrgIdResponse,
									TestFailoverFlags.HRDRequestTimeout,
									TestFailoverFlags.LiveIdRequestTimeout,
									TestFailoverFlags.OrgIdRequestTimeout
								};
							}
							else
							{
								array = new TestFailoverFlags[]
								{
									TestFailoverFlags.HRDRequest,
									TestFailoverFlags.HRDResponse,
									TestFailoverFlags.OrgIdRequest,
									TestFailoverFlags.OrgIdResponse,
									TestFailoverFlags.HRDRequestTimeout,
									TestFailoverFlags.OrgIdRequestTimeout
								};
							}
							testFailoverFlags = array[new Random().Next(0, array.Length)];
						}
						else
						{
							TestFailoverFlags[] array2;
							if (this.UserType == LiveIdAuthenticationUserTypeEnum.ManagedConsumer)
							{
								array2 = new TestFailoverFlags[]
								{
									TestFailoverFlags.OfflineHRD,
									TestFailoverFlags.OfflineAuthentication,
									TestFailoverFlags.LowPasswordConfidence,
									TestFailoverFlags.LiveIdRequest,
									TestFailoverFlags.LiveIdResponse
								};
							}
							else
							{
								array2 = new TestFailoverFlags[]
								{
									TestFailoverFlags.OfflineHRD,
									TestFailoverFlags.OfflineAuthentication,
									TestFailoverFlags.LowPasswordConfidence
								};
							}
							testFailoverFlags = array2[new Random().Next(0, array2.Length)];
						}
					}
					else
					{
						testFailoverFlags = (TestFailoverFlags)this.TestFailOver;
					}
					AuthOptions authOptions = AuthOptions.SyncAD;
					if (this.TestLegacyAPI)
					{
						authOptions |= AuthOptions.ReturnWindowsIdentity;
					}
					if (this.SyncADBackendOnly)
					{
						authOptions |= AuthOptions.SyncADBackEndOnly;
					}
					if (this.LiveIdXmlAuth)
					{
						authOptions |= AuthOptions.LiveIdXmlAuth;
					}
					string text;
					AuthStatus authStatus = authServiceClient.LogonCommonAccessTokenFederationCredsTest(uint.MaxValue, bytes, bytes2, authOptions, null, null, null, null, Guid.NewGuid(), new bool?(this.PreferOfflineAuth), testFailoverFlags, out text, out iisLogs);
					if (this.TestFailOver != FailoverFlags.None)
					{
						iisLogs = testFailoverFlags.ToString() + "." + iisLogs;
					}
					error = ((authStatus == AuthStatus.LogonSuccess) ? LiveIdAuthenticationError.None : LiveIdAuthenticationError.LoginFailure);
					if (this.IgnoreLowPasswordConfidence && iisLogs != null && iisLogs.IndexOf("low confidence") > 0)
					{
						error = LiveIdAuthenticationError.None;
					}
				}
				catch (CommunicationException ex)
				{
					iisLogs = ex.Message;
					error = LiveIdAuthenticationError.CommunicationException;
				}
				catch (InvalidOperationException ex2)
				{
					iisLogs = ex2.Message;
					error = LiveIdAuthenticationError.InvalidOperationException;
				}
				catch (Exception ex3)
				{
					iisLogs = ex3.Message;
					error = LiveIdAuthenticationError.OtherException;
				}
				stopwatch.Stop();
				success = (error == LiveIdAuthenticationError.None);
				latency = stopwatch.Elapsed;
			}
		}

		// Token: 0x060000FE RID: 254 RVA: 0x0000575C File Offset: 0x0000395C
		private void PerformLiveIdAuthenticationTest(ref LiveIdAuthenticationOutcome result)
		{
			bool flag = false;
			string empty = string.Empty;
			StringBuilder stringBuilder = new StringBuilder();
			LiveIdAuthenticationError liveIdAuthenticationError = LiveIdAuthenticationError.None;
			TimeSpan latency = new TimeSpan(0, 0, 0);
			for (int i = 0; i < 3; i++)
			{
				this.InternalPerformLiveIdAuthentication(out flag, out liveIdAuthenticationError, out empty, out latency);
				if (liveIdAuthenticationError == LiveIdAuthenticationError.LoginFailure || liveIdAuthenticationError == LiveIdAuthenticationError.None)
				{
					break;
				}
				stringBuilder.AppendLine(" Retrying: " + empty);
				base.WriteVerbose(new LocalizedString(string.Format("Failed with Error {0}. Trying to Kill and Restart.", empty)));
				this.KillAndStartService();
			}
			stringBuilder.AppendLine(empty);
			result.Update(flag ? LiveIdAuthenticationResultEnum.Success : LiveIdAuthenticationResultEnum.Failure, latency, stringBuilder.ToString());
			liveIdAuthenticationError = (flag ? LiveIdAuthenticationError.None : LiveIdAuthenticationError.LoginFailure);
			if (this.MonitoringContext)
			{
				this.monitoringData.Events.Add(new MonitoringEvent(TestLiveIdAuthenticationTask.CmdletMonitoringEventSource, (int)((flag ? 1000 : 2000) + this.UserType + (int)liveIdAuthenticationError), flag ? EventTypeEnumeration.Success : EventTypeEnumeration.Error, flag ? Strings.LiveIdAuthenticationSuccess(this.UserType.ToString()) : Strings.LiveIdAuthenticationFailed(this.UserType.ToString(), stringBuilder.ToString())));
			}
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00005881 File Offset: 0x00003A81
		private bool IsExplicitlySet(string param)
		{
			return base.Fields.Contains(param);
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00005890 File Offset: 0x00003A90
		private string ConvertToUnsecureString(SecureString securePassword)
		{
			if (securePassword == null)
			{
				throw new ArgumentNullException("securePassword");
			}
			IntPtr intPtr = IntPtr.Zero;
			string result;
			try
			{
				intPtr = Marshal.SecureStringToGlobalAllocUnicode(securePassword);
				result = Marshal.PtrToStringUni(intPtr);
			}
			finally
			{
				Marshal.ZeroFreeGlobalAllocUnicode(intPtr);
			}
			return result;
		}

		// Token: 0x06000101 RID: 257 RVA: 0x000058DC File Offset: 0x00003ADC
		private void GetTestMailbox()
		{
			ADSite localSite = this.SystemConfigurationSession.GetLocalSite();
			base.WriteVerbose(Strings.RunCmdletOnSite(localSite.ToString()));
			string domain = this.ServerObject.Domain;
			base.WriteVerbose(Strings.RunCmdletOnDomain(domain));
			NetworkCredential defaultTestAccount = TestLiveIdAuthenticationTask.GetDefaultTestAccount(new TestLiveIdAuthenticationTask.ClientAccessContext
			{
				Instance = this,
				MonitoringContext = this.MonitoringContext,
				ConfigurationSession = this.SystemConfigurationSession,
				WindowsDomain = domain,
				Site = localSite
			}, this.UserType);
			base.WriteVerbose(Strings.RunCmdletOnUser(defaultTestAccount.UserName));
			this.MailboxCredential = TestLiveIdAuthenticationTask.MakePSCredential(defaultTestAccount);
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00005988 File Offset: 0x00003B88
		internal static IRecipientSession GetRecipientSession(string domain)
		{
			ADSessionSettings sessionSettings = ADSessionSettings.FromTenantAcceptedDomain(domain);
			return DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(null, true, ConsistencyMode.PartiallyConsistent, null, sessionSettings, ConfigScopes.TenantLocal, 806, "GetRecipientSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Monitoring\\Authentication\\TestLiveIdAuthenticationTask.cs");
		}

		// Token: 0x06000103 RID: 259 RVA: 0x000059F8 File Offset: 0x00003BF8
		internal static NetworkCredential GetDefaultTestAccount(TestLiveIdAuthenticationTask.ClientAccessContext context, LiveIdAuthenticationUserTypeEnum userType)
		{
			SmtpAddress? defaultTestUser = null;
			if (!TestConnectivityCredentialsManager.IsExchangeMultiTenant())
			{
				throw new InvalidOperationException();
			}
			if (userType <= LiveIdAuthenticationUserTypeEnum.ManagedBusiness)
			{
				if (userType != LiveIdAuthenticationUserTypeEnum.ManagedConsumer)
				{
					if (userType == LiveIdAuthenticationUserTypeEnum.ManagedBusiness)
					{
						defaultTestUser = TestConnectivityCredentialsManager.GetMultiTenantAutomatedTaskUser(context.Instance, context.ConfigurationSession, context.Site, DatacenterUserType.BPOS);
					}
				}
				else
				{
					defaultTestUser = TestConnectivityCredentialsManager.GetMultiTenantAutomatedTaskUser(context.Instance, context.ConfigurationSession, context.Site, DatacenterUserType.EDU);
				}
			}
			else if (userType == LiveIdAuthenticationUserTypeEnum.FederatedConsumer || userType == LiveIdAuthenticationUserTypeEnum.FederatedBusiness)
			{
				throw new MailboxNotFoundException(new MailboxIdParameter(), null);
			}
			if (defaultTestUser == null)
			{
				throw new MailboxNotFoundException(new MailboxIdParameter(), null);
			}
			MailboxIdParameter localMailboxId = new MailboxIdParameter(string.Format("{0}", defaultTestUser.Value.Local));
			ADUser aduser = TestLiveIdAuthenticationTask.EnsureSingleObject<ADUser>(() => localMailboxId.GetObjects<ADUser>(null, TestLiveIdAuthenticationTask.GetRecipientSession(defaultTestUser.GetValueOrDefault().Domain)));
			if (aduser == null)
			{
				throw new MailboxNotFoundException(new MailboxIdParameter(defaultTestUser.ToString()), null);
			}
			ExchangePrincipal exchangePrincipal = ExchangePrincipal.FromADUser(aduser.OrganizationId.ToADSessionSettings(), aduser);
			if (exchangePrincipal == null)
			{
				throw new MailboxNotFoundException(new MailboxIdParameter(defaultTestUser.ToString()), null);
			}
			NetworkCredential networkCredential = new NetworkCredential(defaultTestUser.Value.ToString(), string.Empty, context.WindowsDomain);
			NetworkCredential networkCredential2 = TestLiveIdAuthenticationTask.MakeCasCredential(networkCredential);
			LocalizedException ex = TestConnectivityCredentialsManager.LoadAutomatedTestCasConnectivityInfo(exchangePrincipal, networkCredential2);
			if (ex != null)
			{
				throw ex;
			}
			networkCredential.Domain = defaultTestUser.Value.Domain;
			networkCredential.Password = networkCredential2.Password;
			return networkCredential;
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00005BB8 File Offset: 0x00003DB8
		internal static NetworkCredential MakeCasCredential(NetworkCredential networkCredential)
		{
			return new NetworkCredential(networkCredential.UserName, networkCredential.Password, networkCredential.Domain);
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00005BE0 File Offset: 0x00003DE0
		private static PSCredential MakePSCredential(NetworkCredential networkCredential)
		{
			string userName = networkCredential.UserName;
			SecureString secureString = new SecureString();
			foreach (char c in networkCredential.Password)
			{
				secureString.AppendChar(c);
			}
			return new PSCredential(userName, secureString);
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000106 RID: 262 RVA: 0x00005C2D File Offset: 0x00003E2D
		// (set) Token: 0x06000107 RID: 263 RVA: 0x00005C35 File Offset: 0x00003E35
		private TimeSpan TotalLatency { get; set; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000108 RID: 264 RVA: 0x00005C3E File Offset: 0x00003E3E
		// (set) Token: 0x06000109 RID: 265 RVA: 0x00005C46 File Offset: 0x00003E46
		private Server ServerObject { get; set; }

		// Token: 0x0600010A RID: 266 RVA: 0x00005C50 File Offset: 0x00003E50
		internal static T EnsureSingleObject<T>(Func<IEnumerable<T>> getObjects) where T : class
		{
			T t = default(T);
			foreach (T t2 in getObjects())
			{
				if (t != null)
				{
					throw new DataValidationException(new ObjectValidationError(Strings.MoreThanOneObjects(typeof(T).ToString()), null, null));
				}
				t = t2;
			}
			return t;
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00005CCC File Offset: 0x00003ECC
		private void HandleException(LocalizedException e)
		{
			if (!this.MonitoringContext)
			{
				this.WriteError(e, ErrorCategory.OperationStopped, this, true);
				return;
			}
			this.monitoringData.Events.Add(new MonitoringEvent(TestLiveIdAuthenticationTask.CmdletMonitoringEventSource, 3006, EventTypeEnumeration.Error, Strings.LiveIdConnectivityExceptionThrown(e.ToString())));
		}

		// Token: 0x0400008F RID: 143
		private const LiveIdAuthenticationUserTypeEnum DefaultUserType = LiveIdAuthenticationUserTypeEnum.ManagedConsumer;

		// Token: 0x04000090 RID: 144
		private const string ServerParam = "Server";

		// Token: 0x04000091 RID: 145
		private const string MailboxCredentialParam = "MailboxCredential";

		// Token: 0x04000092 RID: 146
		private const string MonitoringContextParam = "MonitoringContext";

		// Token: 0x04000093 RID: 147
		private const string PreferOfflineAuthParam = "PreferOfflineAuth";

		// Token: 0x04000094 RID: 148
		private const string TestFailOverParam = "TestFailOver";

		// Token: 0x04000095 RID: 149
		private const string IgnoreLowPasswordConfidenceParam = "IgnoreLowPasswordConfidence";

		// Token: 0x04000096 RID: 150
		private const string LiveIdXmlAuthParam = "LiveIdXmlAuth";

		// Token: 0x04000097 RID: 151
		private const string UserTypeParam = "UserType";

		// Token: 0x04000098 RID: 152
		private const int FailedEventIdBase = 2000;

		// Token: 0x04000099 RID: 153
		private const int SuccessEventIdBase = 1000;

		// Token: 0x0400009A RID: 154
		internal const string LiveIdAuthentication = "LiveIdAuthentication";

		// Token: 0x0400009B RID: 155
		private MonitoringData monitoringData;

		// Token: 0x0400009C RID: 156
		private ITopologyConfigurationSession systemConfigurationSession;

		// Token: 0x0400009D RID: 157
		public static readonly string CmdletMonitoringEventSource = "MSExchange Monitoring LiveIdAuthentication";

		// Token: 0x0400009E RID: 158
		public static readonly string PerformanceCounter = "LiveIdAuthentication Latency";

		// Token: 0x02000020 RID: 32
		public enum ScenarioId
		{
			// Token: 0x040000A4 RID: 164
			PlaceHolderNoException = 1006,
			// Token: 0x040000A5 RID: 165
			ExceptionThrown = 3006,
			// Token: 0x040000A6 RID: 166
			AllTransactionsSucceeded = 3001
		}

		// Token: 0x02000021 RID: 33
		internal struct ClientAccessContext
		{
			// Token: 0x040000A7 RID: 167
			internal Task Instance;

			// Token: 0x040000A8 RID: 168
			internal bool MonitoringContext;

			// Token: 0x040000A9 RID: 169
			internal ITopologyConfigurationSession ConfigurationSession;

			// Token: 0x040000AA RID: 170
			internal string WindowsDomain;

			// Token: 0x040000AB RID: 171
			internal ADSite Site;
		}
	}
}
