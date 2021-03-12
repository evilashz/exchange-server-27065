using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Management.Automation;
using System.Net;
using System.Security;
using System.Threading;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Monitoring;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Win32;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x02000D49 RID: 3401
	[Cmdlet("Test", "UMConnectivity", SupportsShouldProcess = true, DefaultParameterSetName = "LocalLoop")]
	public sealed class TestUMConnectivity : Task
	{
		// Token: 0x17002884 RID: 10372
		// (get) Token: 0x06008262 RID: 33378 RVA: 0x0021543C File Offset: 0x0021363C
		private TestUMConnectivityHelper.LocalUMConnectivityOptionsResults LocalCallRouterResult
		{
			get
			{
				return (TestUMConnectivityHelper.LocalUMConnectivityOptionsResults)this.result;
			}
		}

		// Token: 0x17002885 RID: 10373
		// (get) Token: 0x06008263 RID: 33379 RVA: 0x00215449 File Offset: 0x00213649
		private TestUMConnectivityHelper.LocalUMConnectivityResults LocalBackendResult
		{
			get
			{
				return (TestUMConnectivityHelper.LocalUMConnectivityResults)this.result;
			}
		}

		// Token: 0x17002886 RID: 10374
		// (get) Token: 0x06008264 RID: 33380 RVA: 0x00215456 File Offset: 0x00213656
		// (set) Token: 0x06008265 RID: 33381 RVA: 0x0021546D File Offset: 0x0021366D
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

		// Token: 0x17002887 RID: 10375
		// (get) Token: 0x06008266 RID: 33382 RVA: 0x00215480 File Offset: 0x00213680
		// (set) Token: 0x06008267 RID: 33383 RVA: 0x002154A1 File Offset: 0x002136A1
		[Parameter(Mandatory = false, ParameterSetName = "LocalLoop")]
		[Parameter(Mandatory = false, ParameterSetName = "TuiLogonSpecific")]
		[Parameter(Mandatory = false, ParameterSetName = "TuiLogonGeneral")]
		[Parameter(Mandatory = false, ParameterSetName = "PinReset")]
		[Parameter(Mandatory = false, ParameterSetName = "EndToEnd")]
		public bool MonitoringContext
		{
			get
			{
				return (bool)(base.Fields["MonitoringContext"] ?? false);
			}
			set
			{
				base.Fields["MonitoringContext"] = value;
			}
		}

		// Token: 0x17002888 RID: 10376
		// (get) Token: 0x06008268 RID: 33384 RVA: 0x002154B9 File Offset: 0x002136B9
		// (set) Token: 0x06008269 RID: 33385 RVA: 0x002154EA File Offset: 0x002136EA
		[Parameter(Mandatory = false, ParameterSetName = "EndToEnd")]
		[Parameter(Mandatory = false, ParameterSetName = "LocalLoop")]
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = false, ParameterSetName = "TuiLogonGeneral")]
		[Parameter(Mandatory = false, ParameterSetName = "TuiLogonSpecific")]
		public int ListenPort
		{
			get
			{
				return (int)((base.Fields["ListenPort"] == null) ? 0 : base.Fields["ListenPort"]);
			}
			set
			{
				base.Fields["ListenPort"] = value;
			}
		}

		// Token: 0x17002889 RID: 10377
		// (get) Token: 0x0600826A RID: 33386 RVA: 0x00215502 File Offset: 0x00213702
		// (set) Token: 0x0600826B RID: 33387 RVA: 0x00215533 File Offset: 0x00213733
		[Parameter(Mandatory = false, ParameterSetName = "TuiLogonGeneral")]
		[Parameter(Mandatory = false, ParameterSetName = "TuiLogonSpecific")]
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = false, ParameterSetName = "LocalLoop")]
		public int RemotePort
		{
			get
			{
				return (int)((base.Fields["RemotePort"] == null) ? 0 : base.Fields["RemotePort"]);
			}
			set
			{
				base.Fields["RemotePort"] = value;
			}
		}

		// Token: 0x1700288A RID: 10378
		// (get) Token: 0x0600826C RID: 33388 RVA: 0x0021554B File Offset: 0x0021374B
		// (set) Token: 0x0600826D RID: 33389 RVA: 0x0021557C File Offset: 0x0021377C
		[Parameter(Mandatory = false, ParameterSetName = "TuiLogonSpecific")]
		[Parameter(Mandatory = false, ParameterSetName = "EndToEnd")]
		[Parameter(Mandatory = false, ParameterSetName = "TuiLogonGeneral")]
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = false, ParameterSetName = "LocalLoop")]
		public bool Secured
		{
			get
			{
				return (bool)((base.Fields["Secured"] == null) ? false : base.Fields["Secured"]);
			}
			set
			{
				base.Fields["Secured"] = value;
			}
		}

		// Token: 0x1700288B RID: 10379
		// (get) Token: 0x0600826E RID: 33390 RVA: 0x00215594 File Offset: 0x00213794
		// (set) Token: 0x0600826F RID: 33391 RVA: 0x002155AB File Offset: 0x002137AB
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = false, ParameterSetName = "EndToEnd")]
		[Parameter(Mandatory = false, ParameterSetName = "TuiLogonSpecific")]
		[Parameter(Mandatory = false, ParameterSetName = "LocalLoop")]
		[Parameter(Mandatory = false, ParameterSetName = "TuiLogonGeneral")]
		public string CertificateThumbprint
		{
			get
			{
				return (string)base.Fields["CertificateThumbprint"];
			}
			set
			{
				base.Fields["CertificateThumbprint"] = value;
			}
		}

		// Token: 0x1700288C RID: 10380
		// (get) Token: 0x06008270 RID: 33392 RVA: 0x002155BE File Offset: 0x002137BE
		// (set) Token: 0x06008271 RID: 33393 RVA: 0x002155EF File Offset: 0x002137EF
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = false, ParameterSetName = "TuiLogonGeneral")]
		[Parameter(Mandatory = false, ParameterSetName = "TuiLogonSpecific")]
		[Parameter(Mandatory = false, ParameterSetName = "LocalLoop")]
		[Parameter(Mandatory = false, ParameterSetName = "EndToEnd")]
		public bool MediaSecured
		{
			get
			{
				return (bool)((base.Fields["MediaSecured"] == null) ? false : base.Fields["MediaSecured"]);
			}
			set
			{
				base.Fields["MediaSecured"] = value;
			}
		}

		// Token: 0x1700288D RID: 10381
		// (get) Token: 0x06008272 RID: 33394 RVA: 0x00215607 File Offset: 0x00213807
		// (set) Token: 0x06008273 RID: 33395 RVA: 0x00215638 File Offset: 0x00213838
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = false, ParameterSetName = "EndToEnd")]
		public int DiagInitialSilenceInMilisecs
		{
			get
			{
				return (int)((base.Fields["DiagInitialSilenceInMilisecs"] == null) ? 0 : base.Fields["DiagInitialSilenceInMilisecs"]);
			}
			set
			{
				base.Fields["DiagInitialSilenceInMilisecs"] = value;
			}
		}

		// Token: 0x1700288E RID: 10382
		// (get) Token: 0x06008274 RID: 33396 RVA: 0x00215650 File Offset: 0x00213850
		// (set) Token: 0x06008275 RID: 33397 RVA: 0x00215680 File Offset: 0x00213880
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = false, ParameterSetName = "EndToEnd")]
		public string DiagDtmfSequence
		{
			get
			{
				return (string)((base.Fields["DiagDtmfSequence"] == null) ? "ABCD*#0123456789" : base.Fields["DiagDtmfSequence"]);
			}
			set
			{
				base.Fields["DiagDtmfSequence"] = value;
			}
		}

		// Token: 0x1700288F RID: 10383
		// (get) Token: 0x06008276 RID: 33398 RVA: 0x00215693 File Offset: 0x00213893
		// (set) Token: 0x06008277 RID: 33399 RVA: 0x002156C4 File Offset: 0x002138C4
		[Parameter(Mandatory = false, ParameterSetName = "EndToEnd")]
		[ValidateNotNullOrEmpty]
		public int DiagInterDtmfGapInMilisecs
		{
			get
			{
				return (int)((base.Fields["DiagInterDtmfGapInMilisecs"] == null) ? 0 : base.Fields["DiagInterDtmfGapInMilisecs"]);
			}
			set
			{
				base.Fields["DiagInterDtmfGapInMilisecs"] = value;
			}
		}

		// Token: 0x17002890 RID: 10384
		// (get) Token: 0x06008278 RID: 33400 RVA: 0x002156DC File Offset: 0x002138DC
		// (set) Token: 0x06008279 RID: 33401 RVA: 0x0021570D File Offset: 0x0021390D
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = false, ParameterSetName = "EndToEnd")]
		public int DiagDtmfDurationInMilisecs
		{
			get
			{
				return (int)((base.Fields["DiagDtmfDurationInMilisecs"] == null) ? 0 : base.Fields["DiagDtmfDurationInMilisecs"]);
			}
			set
			{
				base.Fields["DiagDtmfDurationInMilisecs"] = value;
			}
		}

		// Token: 0x17002891 RID: 10385
		// (get) Token: 0x0600827A RID: 33402 RVA: 0x00215725 File Offset: 0x00213925
		// (set) Token: 0x0600827B RID: 33403 RVA: 0x00215755 File Offset: 0x00213955
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = false, ParameterSetName = "EndToEnd")]
		public string DiagInterDtmfDiffGapInMilisecs
		{
			get
			{
				return (string)((base.Fields["DiagInterDtmfDiffGapInMilisecs"] == null) ? string.Empty : base.Fields["DiagInterDtmfDiffGapInMilisecs"]);
			}
			set
			{
				base.Fields["DiagInterDtmfDiffGapInMilisecs"] = value;
			}
		}

		// Token: 0x17002892 RID: 10386
		// (get) Token: 0x0600827C RID: 33404 RVA: 0x00215768 File Offset: 0x00213968
		// (set) Token: 0x0600827D RID: 33405 RVA: 0x0021577F File Offset: 0x0021397F
		[Parameter(Mandatory = false, ParameterSetName = "EndToEnd")]
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = false, ParameterSetName = "LocalLoop")]
		[Parameter(Mandatory = false, ParameterSetName = "TuiLogonSpecific")]
		[Parameter(Mandatory = false, ParameterSetName = "TuiLogonGeneral")]
		public int Timeout
		{
			get
			{
				return (int)base.Fields["Timeout"];
			}
			set
			{
				base.Fields["Timeout"] = value;
			}
		}

		// Token: 0x17002893 RID: 10387
		// (get) Token: 0x0600827E RID: 33406 RVA: 0x00215797 File Offset: 0x00213997
		// (set) Token: 0x0600827F RID: 33407 RVA: 0x002157AE File Offset: 0x002139AE
		[Parameter(Mandatory = true, ParameterSetName = "EndToEnd")]
		[ValidateNotNullOrEmpty]
		public UMIPGatewayIdParameter UMIPGateway
		{
			get
			{
				return (UMIPGatewayIdParameter)base.Fields["IPGateway"];
			}
			set
			{
				base.Fields["IPGateway"] = value;
			}
		}

		// Token: 0x17002894 RID: 10388
		// (get) Token: 0x06008280 RID: 33408 RVA: 0x002157C1 File Offset: 0x002139C1
		// (set) Token: 0x06008281 RID: 33409 RVA: 0x002157D8 File Offset: 0x002139D8
		[Parameter(Mandatory = true, ParameterSetName = "EndToEnd")]
		[Parameter(Mandatory = true, ParameterSetName = "TuiLogonSpecific")]
		[ValidateNotNullOrEmpty]
		public string Phone
		{
			get
			{
				return (string)base.Fields["Phone"];
			}
			set
			{
				base.Fields["Phone"] = value;
			}
		}

		// Token: 0x17002895 RID: 10389
		// (get) Token: 0x06008282 RID: 33410 RVA: 0x002157EB File Offset: 0x002139EB
		// (set) Token: 0x06008283 RID: 33411 RVA: 0x00215802 File Offset: 0x00213A02
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = false, ParameterSetName = "EndToEnd")]
		public string From
		{
			get
			{
				return (string)base.Fields["From"];
			}
			set
			{
				base.Fields["From"] = value;
			}
		}

		// Token: 0x17002896 RID: 10390
		// (get) Token: 0x06008284 RID: 33412 RVA: 0x00215815 File Offset: 0x00213A15
		// (set) Token: 0x06008285 RID: 33413 RVA: 0x0021582C File Offset: 0x00213A2C
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = true, ParameterSetName = "TuiLogonSpecific")]
		public string PIN
		{
			get
			{
				return (string)base.Fields["PIN"];
			}
			set
			{
				base.Fields["PIN"] = value;
			}
		}

		// Token: 0x17002897 RID: 10391
		// (get) Token: 0x06008286 RID: 33414 RVA: 0x0021583F File Offset: 0x00213A3F
		// (set) Token: 0x06008287 RID: 33415 RVA: 0x00215856 File Offset: 0x00213A56
		[Parameter(Mandatory = true, ParameterSetName = "TuiLogonSpecific")]
		[ValidateNotNullOrEmpty]
		public UMDialPlanIdParameter UMDialPlan
		{
			get
			{
				return (UMDialPlanIdParameter)base.Fields["UMDialPlan"];
			}
			set
			{
				base.Fields["UMDialPlan"] = value;
			}
		}

		// Token: 0x17002898 RID: 10392
		// (get) Token: 0x06008288 RID: 33416 RVA: 0x00215869 File Offset: 0x00213A69
		// (set) Token: 0x06008289 RID: 33417 RVA: 0x0021589A File Offset: 0x00213A9A
		[Parameter(Mandatory = true, ParameterSetName = "TuiLogonSpecific")]
		[ValidateNotNullOrEmpty]
		public bool TUILogon
		{
			get
			{
				return (bool)((base.Fields["TUILogon"] == null) ? false : base.Fields["TUILogon"]);
			}
			set
			{
				base.Fields["TUILogon"] = value;
			}
		}

		// Token: 0x17002899 RID: 10393
		// (get) Token: 0x0600828A RID: 33418 RVA: 0x002158B2 File Offset: 0x00213AB2
		// (set) Token: 0x0600828B RID: 33419 RVA: 0x002158E3 File Offset: 0x00213AE3
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = true, ParameterSetName = "TuiLogonGeneral")]
		public bool TUILogonAll
		{
			get
			{
				return (bool)((base.Fields["TUILogonAll"] == null) ? false : base.Fields["TUILogonAll"]);
			}
			set
			{
				base.Fields["TUILogonAll"] = value;
			}
		}

		// Token: 0x1700289A RID: 10394
		// (get) Token: 0x0600828C RID: 33420 RVA: 0x002158FB File Offset: 0x00213AFB
		// (set) Token: 0x0600828D RID: 33421 RVA: 0x0021592C File Offset: 0x00213B2C
		[Parameter(Mandatory = true, ParameterSetName = "PinReset")]
		[ValidateNotNullOrEmpty]
		public bool ResetPIN
		{
			get
			{
				return (bool)((base.Fields["ResetPIN"] == null) ? false : base.Fields["ResetPIN"]);
			}
			set
			{
				base.Fields["ResetPIN"] = value;
			}
		}

		// Token: 0x1700289B RID: 10395
		// (get) Token: 0x0600828E RID: 33422 RVA: 0x00215944 File Offset: 0x00213B44
		// (set) Token: 0x0600828F RID: 33423 RVA: 0x0021596A File Offset: 0x00213B6A
		[Parameter(Mandatory = false, ParameterSetName = "LocalLoop")]
		public SwitchParameter CallRouter
		{
			get
			{
				return (SwitchParameter)(base.Fields["CallRouter"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["CallRouter"] = value;
			}
		}

		// Token: 0x1700289C RID: 10396
		// (get) Token: 0x06008290 RID: 33424 RVA: 0x00215984 File Offset: 0x00213B84
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				if ("EndToEnd" == base.ParameterSetName)
				{
					return Strings.ConfirmationMessageTestUMConnectivityEndToEnd(this.UMIPGateway.ToString(), this.Phone.ToString());
				}
				if ("PinReset" == base.ParameterSetName)
				{
					return Strings.ConfirmationMessageTestUMConnectivityPinReset;
				}
				if ("TuiLogonGeneral" == base.ParameterSetName || "TuiLogonSpecific" == base.ParameterSetName)
				{
					return Strings.ConfirmationMessageTestUMConnectivityTUILocalLoop;
				}
				return Strings.ConfirmationMessageTestUMConnectivityLocalLoop;
			}
		}

		// Token: 0x06008291 RID: 33425 RVA: 0x00215A06 File Offset: 0x00213C06
		protected override void InternalStopProcessing()
		{
			TaskLogger.LogEnter();
			if (this.umconnection != null)
			{
				this.umconnection.EndCall();
				this.umconnection.Shutdown();
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06008292 RID: 33426 RVA: 0x00215A30 File Offset: 0x00213C30
		protected override bool IsKnownException(Exception e)
		{
			return base.IsKnownException(e) || MonitoringHelper.IsKnownExceptionForMonitoring(e);
		}

		// Token: 0x06008293 RID: 33427 RVA: 0x00215A44 File Offset: 0x00213C44
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			base.InternalBeginProcessing();
			this.WriteErrorIfMissingUCMA();
			this.configurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(this.DomainController, true, ConsistencyMode.FullyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 784, "InternalBeginProcessing", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\um\\TestUMConnectivity.cs");
			ADSessionSettings sessionSettings = ADSessionSettings.FromRootOrgScopeSet();
			this.globalCatalogSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(this.DomainController, true, ConsistencyMode.IgnoreInvalid, sessionSettings, 791, "InternalBeginProcessing", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\um\\TestUMConnectivity.cs");
			if (!this.globalCatalogSession.IsReadConnectionAvailable())
			{
				this.globalCatalogSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(ConsistencyMode.IgnoreInvalid, sessionSettings, 800, "InternalBeginProcessing", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\um\\TestUMConnectivity.cs");
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06008294 RID: 33428 RVA: 0x00215AFC File Offset: 0x00213CFC
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			try
			{
				this.ipGateway = this.UMIPGateway;
				this.dialPlan = this.UMDialPlan;
				this.localIPAddress = Utils.GetLocalIPv4Address().ToString();
				this.localLoopTarget = LocalLoopTargetStrategy.Create(this.configurationSession, this.CallRouter);
				base.InternalValidate();
				this.DoOwnValidate();
			}
			catch (LocalizedException ex)
			{
				TestUMConnectivity.EventId id = TestUMConnectivity.EventId.TestExecuteError;
				if (ex is DataSourceTransientException || ex is DataSourceOperationException || ex is DataValidationException)
				{
					id = TestUMConnectivity.EventId.ADError;
				}
				else if (ex is LocalServerNotFoundException || ex is ExchangeServerNotFoundException || ex is SIPFEServerConfigurationNotFoundException || ex is ServiceNotStarted || ex is UMServiceDisabled)
				{
					id = TestUMConnectivity.EventId.ServiceNotInstalledOrRunning;
				}
				this.HandleValidationFailed(ex, id);
			}
			finally
			{
				if (base.HasErrors && this.MonitoringContext)
				{
					base.WriteObject(this.monitoringData);
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06008295 RID: 33429 RVA: 0x00215C00 File Offset: 0x00213E00
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			this.startTime = ExDateTime.UtcNow;
			try
			{
				if (this.taskMode == TestUMConnectivity.Mode.ResetPin)
				{
					this.ExecutePinResetTest();
				}
				else if (this.taskMode == TestUMConnectivity.Mode.LocalLoopTUILogonAll)
				{
					this.ExecuteAutoEnumerateTest();
				}
				else if (this.taskMode == TestUMConnectivity.Mode.EndToEndTest)
				{
					this.ExecuteRemoteTest();
				}
				else if (this.CallRouter)
				{
					this.ExecuteCallRouterTest();
				}
				else
				{
					this.ExecuteTest();
				}
			}
			finally
			{
				if (this.MonitoringContext)
				{
					base.WriteObject(this.monitoringData);
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06008296 RID: 33430 RVA: 0x00215C98 File Offset: 0x00213E98
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (this.umconnection != null)
				{
					this.umconnection.Shutdown();
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x06008297 RID: 33431 RVA: 0x00215CD4 File Offset: 0x00213ED4
		private static bool UserUMEnabled(ADUser user)
		{
			return user != null && UMSubscriber.IsValidSubscriber(user);
		}

		// Token: 0x06008298 RID: 33432 RVA: 0x00215CE1 File Offset: 0x00213EE1
		private bool IsMailboxLocal(ExchangePrincipal principal)
		{
			return string.Equals(this.localLoopTarget.Server.Fqdn, principal.MailboxInfo.Location.ServerFqdn, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06008299 RID: 33433 RVA: 0x00215D09 File Offset: 0x00213F09
		private bool IsParameterSpecified(string parameterName)
		{
			return base.Fields.IsModified(parameterName);
		}

		// Token: 0x0600829A RID: 33434 RVA: 0x00215D18 File Offset: 0x00213F18
		private void ExecutePinResetTest()
		{
			foreach (TestUMConnectivityHelper.UsersForResetPin usersForResetPin in this.pinResetUsers)
			{
				ExDateTime utcNow = ExDateTime.UtcNow;
				if (!UmConnectivityCredentialsHelper.ResetMailboxPassword(usersForResetPin.ExPrincipal, usersForResetPin.NetCreds))
				{
					this.HandlePinResetScenarioOutCome(usersForResetPin, TestUMConnectivity.PinResetOutcome.PasswordResetFailed, utcNow);
				}
				else if (!UmConnectivityCredentialsHelper.ResetUMPin(usersForResetPin.Aduser, usersForResetPin.NetCreds.Password))
				{
					this.HandlePinResetScenarioOutCome(usersForResetPin, TestUMConnectivity.PinResetOutcome.PinResetFailed, utcNow);
				}
				else
				{
					this.HandlePinResetScenarioOutCome(usersForResetPin, TestUMConnectivity.PinResetOutcome.Success, utcNow);
				}
			}
			this.WriteResetPinTestResults();
		}

		// Token: 0x0600829B RID: 33435 RVA: 0x00215DBC File Offset: 0x00213FBC
		private void HandlePinResetScenarioOutCome(TestUMConnectivityHelper.UsersForResetPin user, TestUMConnectivity.PinResetOutcome outcome, ExDateTime time)
		{
			TimeSpan timeSpan = ExDateTime.UtcNow.Subtract(time);
			TestUMConnectivityHelper.ResetPinResults resetPinResults = new TestUMConnectivityHelper.ResetPinResults();
			resetPinResults.Latency = timeSpan.TotalMilliseconds;
			resetPinResults.MailboxServerBeingTested = user.MailboxServer;
			string mailboxServer = user.MailboxServer;
			double performanceValue = resetPinResults.Latency;
			switch (outcome)
			{
			case TestUMConnectivity.PinResetOutcome.PasswordResetFailed:
				resetPinResults.SuccessfullyResetPasswordForValidUMUser = false;
				resetPinResults.SuccessfullyResetPINForValidUMUser = false;
				performanceValue = -1.0;
				this.pinResetTestsListOfServersFailedToResetPasswd = this.pinResetTestsListOfServersFailedToResetPasswd + user.MailboxServer + ", ";
				break;
			case TestUMConnectivity.PinResetOutcome.PinResetFailed:
				resetPinResults.SuccessfullyResetPasswordForValidUMUser = true;
				resetPinResults.SuccessfullyResetPINForValidUMUser = false;
				performanceValue = -1.0;
				this.pinResetTestsListOfServersFailedToResetPin = this.pinResetTestsListOfServersFailedToResetPin + user.MailboxServer + ", ";
				break;
			case TestUMConnectivity.PinResetOutcome.Success:
				resetPinResults.SuccessfullyResetPasswordForValidUMUser = true;
				resetPinResults.SuccessfullyResetPINForValidUMUser = true;
				this.pinResetTestsListOfServersPassed = this.pinResetTestsListOfServersPassed + user.MailboxServer + ", ";
				break;
			}
			this.resetPinResults.Add(resetPinResults);
			this.monitoringData.PerformanceCounters.Add(new MonitoringPerformanceCounter(this.momSourceString, "Call Latency", mailboxServer, performanceValue));
		}

		// Token: 0x0600829C RID: 33436 RVA: 0x00215EE8 File Offset: 0x002140E8
		private void ExecuteAutoEnumerateTest()
		{
			foreach (TestUMConnectivityHelper.TestMailboxUserDetails testMailboxUserDetails in this.tuiEnumerateUsers)
			{
				Thread.Sleep(500);
				ExDateTime utcNow = ExDateTime.UtcNow;
				if (!this.umconnection.MakeCall(this.addressToCall, this.addressLocalCaller))
				{
					this.HandleTUILogonScenarioOutCome(testMailboxUserDetails, TestUMConnectivity.TUILogonAllOutcome.MakeCallFailed, utcNow);
				}
				else
				{
					this.testParams.DpName = testMailboxUserDetails.DialPlan;
					this.testParams.Phone = testMailboxUserDetails.Phone;
					this.testParams.PIN = testMailboxUserDetails.Pin;
					if (!this.umconnection.ExecuteTest(this.testParams))
					{
						this.HandleTUILogonScenarioOutCome(testMailboxUserDetails, TestUMConnectivity.TUILogonAllOutcome.ExecuteScenarioFailed, utcNow);
					}
					else
					{
						this.HandleTUILogonScenarioOutCome(testMailboxUserDetails, TestUMConnectivity.TUILogonAllOutcome.Success, utcNow);
					}
				}
			}
			Thread.Sleep(500);
			this.umconnection.Shutdown();
			this.WriteTUITestResults();
		}

		// Token: 0x0600829D RID: 33437 RVA: 0x00215FE4 File Offset: 0x002141E4
		private void WriteResetPinTestResults()
		{
			this.WriteResults(this.resetPinResults);
			if (!string.IsNullOrEmpty(this.pinResetTestsListOfServersPassed))
			{
				this.monitoringData.Events.Add(new MonitoringEvent(this.momSourceString, 1000, EventTypeEnumeration.Success, Strings.PINResetSuccessful(this.pinResetTestsListOfServersPassed)));
			}
			string text = string.Empty;
			if (!string.IsNullOrEmpty(this.pinResetTestsListOfServersFailedToResetPasswd))
			{
				text += Strings.PINResetfailedToResetPasswd(this.pinResetTestsListOfServersFailedToResetPasswd).ToString();
				text += "   ";
			}
			if (!string.IsNullOrEmpty(this.pinResetTestsListOfServersFailedToResetPin))
			{
				text += Strings.PINResetfailedToResetPin(this.pinResetTestsListOfServersFailedToResetPin).ToString();
				text += "   ";
			}
			if (!string.IsNullOrEmpty(text))
			{
				this.monitoringData.Events.Add(new MonitoringEvent(this.momSourceString, 1006, EventTypeEnumeration.Error, text));
			}
		}

		// Token: 0x0600829E RID: 33438 RVA: 0x002160E0 File Offset: 0x002142E0
		private void WriteTUITestResults()
		{
			this.WriteResults(this.tuiResults);
			if (!string.IsNullOrEmpty(this.tuiAllTestsListOfServersPassed))
			{
				this.monitoringData.Events.Add(new MonitoringEvent(this.momSourceString, 1000, EventTypeEnumeration.Success, Strings.TUILogonSuccessful(this.tuiAllTestsListOfServersPassed)));
			}
			string text = string.Empty;
			if (!string.IsNullOrEmpty(this.tuiAllTestsListOfServersFailedToGetPin))
			{
				text += Strings.TUILogonfailedToGetPin(this.tuiAllTestsListOfServersFailedToGetPin).ToString();
				text += "   ";
			}
			if (!string.IsNullOrEmpty(this.tuiAllTestsListOfServersFailedToMakeCall))
			{
				text += Strings.TUILogonfailedToMakeCall(this.tuiAllTestsListOfServersFailedToMakeCall).ToString();
				text += "   ";
			}
			if (!string.IsNullOrEmpty(this.tuiAllTestsListOfServersFailedToExcuteScenario))
			{
				text += Strings.TUILogonfailedToLogon(this.tuiAllTestsListOfServersFailedToExcuteScenario).ToString();
				text += "   ";
			}
			if (!string.IsNullOrEmpty(text))
			{
				this.monitoringData.Events.Add(new MonitoringEvent(this.momSourceString, 1006, EventTypeEnumeration.Error, text));
			}
		}

		// Token: 0x0600829F RID: 33439 RVA: 0x00216214 File Offset: 0x00214414
		private void HandleTUILogonScenarioOutCome(TestUMConnectivityHelper.TestMailboxUserDetails user, TestUMConnectivity.TUILogonAllOutcome reason, ExDateTime time)
		{
			Thread.Sleep(500);
			this.umconnection.EndCall();
			TimeSpan timeSpan = ExDateTime.UtcNow.Subtract(time);
			TestUMConnectivityHelper.TUILogonEnumerateResults tuilogonEnumerateResults = new TestUMConnectivityHelper.TUILogonEnumerateResults();
			tuilogonEnumerateResults.TotalQueuedMessages = "0";
			tuilogonEnumerateResults.SuccessfullyRetrievedPINForValidUMUser = true;
			tuilogonEnumerateResults.CurrCalls = "0";
			tuilogonEnumerateResults.Latency = timeSpan.TotalMilliseconds;
			tuilogonEnumerateResults.MailboxServerBeingTested = user.MailboxServer;
			tuilogonEnumerateResults.UmserverAcceptingCallAnsweringMessages = true;
			tuilogonEnumerateResults.UmIPAddress = this.localIPAddress;
			string performanceInstance = string.Concat(new string[]
			{
				user.MailboxServer,
				"|",
				user.Phone,
				"|",
				this.localIPAddress
			});
			double performanceValue = tuilogonEnumerateResults.Latency;
			switch (reason)
			{
			case TestUMConnectivity.TUILogonAllOutcome.MakeCallFailed:
				tuilogonEnumerateResults.OutBoundSIPCallSuccess = false;
				tuilogonEnumerateResults.EntireOperationSuccess = false;
				performanceValue = -1.0;
				this.tuiAllTestsListOfServersFailedToMakeCall = this.tuiAllTestsListOfServersFailedToMakeCall + user.MailboxServer + ", ";
				break;
			case TestUMConnectivity.TUILogonAllOutcome.ExecuteScenarioFailed:
				tuilogonEnumerateResults.OutBoundSIPCallSuccess = true;
				tuilogonEnumerateResults.EntireOperationSuccess = false;
				performanceValue = -1.0;
				this.tuiAllTestsListOfServersFailedToExcuteScenario = this.tuiAllTestsListOfServersFailedToExcuteScenario + user.MailboxServer + ", ";
				break;
			case TestUMConnectivity.TUILogonAllOutcome.Success:
				tuilogonEnumerateResults.OutBoundSIPCallSuccess = true;
				tuilogonEnumerateResults.EntireOperationSuccess = true;
				this.tuiAllTestsListOfServersPassed = this.tuiAllTestsListOfServersPassed + user.MailboxServer + ", ";
				tuilogonEnumerateResults.CurrCalls = this.umconnection.CurrCalls;
				tuilogonEnumerateResults.TotalQueuedMessages = ((LocalTUILogonConnectivityTester)this.umconnection).TotalQueueLength;
				tuilogonEnumerateResults.UmserverAcceptingCallAnsweringMessages = ((LocalTUILogonConnectivityTester)this.umconnection).AcceptingCalls;
				break;
			}
			this.tuiResults.Add(tuilogonEnumerateResults);
			this.monitoringData.PerformanceCounters.Add(new MonitoringPerformanceCounter(this.momSourceString, "Call Latency", performanceInstance, performanceValue));
		}

		// Token: 0x060082A0 RID: 33440 RVA: 0x002163FC File Offset: 0x002145FC
		private void ExecuteTest()
		{
			this.PrepareSessionAndMakeCall();
			if (!this.umconnection.ExecuteTest(this.testParams))
			{
				this.result.EntireOperationSuccess = false;
				this.result.ReasonForFailure = this.umconnection.Error.LocalizedString;
				this.Cleanup(true);
				return;
			}
			this.result.EntireOperationSuccess = true;
			this.PopulateResults();
			this.Cleanup();
		}

		// Token: 0x060082A1 RID: 33441 RVA: 0x0021646C File Offset: 0x0021466C
		private void ExecuteCallRouterTest()
		{
			this.LocalCallRouterResult.EntireOperationSuccess = this.umconnection.SendOptions(this.addressToCall);
			this.LocalCallRouterResult.Diagnostics = this.umconnection.MsDiagnosticsHeaderValue;
			LocalizedException error = this.umconnection.Error;
			this.LocalCallRouterResult.ReasonForFailure = ((error != null) ? error.LocalizedString : LocalizedString.Empty);
			this.Cleanup(true);
		}

		// Token: 0x060082A2 RID: 33442 RVA: 0x002164DC File Offset: 0x002146DC
		private void ExecuteRemoteTest()
		{
			if (!this.TryRemoteTest())
			{
				LocalizedException localizedException;
				if (this.umconnection.Error == null)
				{
					localizedException = new TUC_NoDTMFSwereReceived();
				}
				else
				{
					localizedException = this.umconnection.Error;
				}
				this.Cleanup();
				this.HandleError(localizedException, TestUMConnectivity.EventId.TestExecuteError, this.momSourceString);
			}
		}

		// Token: 0x060082A3 RID: 33443 RVA: 0x00216530 File Offset: 0x00214730
		private bool TryRemoteTest()
		{
			this.startTime = ExDateTime.UtcNow;
			this.testParams.DiagDtmfSequence = this.DiagDtmfSequence;
			this.testParams.DiagInitialSilenceInMilisecs = this.DiagInitialSilenceInMilisecs;
			this.testParams.DiagInterDtmfGapInMilisecs = this.DiagInterDtmfGapInMilisecs;
			this.testParams.DiagDtmfDurationInMilisecs = this.DiagDtmfDurationInMilisecs;
			this.testParams.DiagInterDtmfGapDiffInMilisecs = this.DiagInterDtmfDiffGapInMilisecs;
			this.PrepareSessionAndMakeCall();
			if (!this.umconnection.ExecuteTest(this.testParams))
			{
				this.result.EntireOperationSuccess = false;
				if (this.TotalDTMFLoss())
				{
					this.umconnection.EndCall();
					return false;
				}
				this.result = new TestUMConnectivityHelper.RemoteUMConnectivityResults(this.result);
				this.result.ReasonForFailure = this.umconnection.Error.LocalizedString;
				((TestUMConnectivityHelper.RemoteUMConnectivityResults)this.result).ReceivedDigits = ((RemoteConnectivityTester)this.umconnection).AllDTMFsReceived;
				((TestUMConnectivityHelper.RemoteUMConnectivityResults)this.result).ExpectedDigits = Strings.DiagnosticSequence("ABCD*#0123456789");
				this.Cleanup(true);
				return true;
			}
			else
			{
				if (this.MonitoringContext || (!this.MonitoringContext && this.IsUMIPValid()))
				{
					this.result.EntireOperationSuccess = true;
					this.PopulateResults();
					this.Cleanup();
					return true;
				}
				this.result = new TestUMConnectivityHelper.RemoteUMConnectivityResults(this.result);
				this.result.EntireOperationSuccess = false;
				LocalizedException ex = new TUC_InvalidIPAddressReceived();
				this.result.ReasonForFailure = ex.LocalizedString;
				((TestUMConnectivityHelper.RemoteUMConnectivityResults)this.result).ReceivedDigits = ((RemoteConnectivityTester)this.umconnection).AllDTMFsReceived;
				((TestUMConnectivityHelper.RemoteUMConnectivityResults)this.result).ExpectedDigits = Strings.DiagnosticSequence("ABCD*#0123456789");
				this.Cleanup(true);
				return true;
			}
		}

		// Token: 0x060082A4 RID: 33444 RVA: 0x002166FC File Offset: 0x002148FC
		private bool TotalDTMFLoss()
		{
			bool flag = false;
			if (this.taskMode == TestUMConnectivity.Mode.EndToEndTest && this.umconnection.IsCallEstablished && !((RemoteConnectivityTester)this.umconnection).AnyDTMFsReceived)
			{
				flag = true;
			}
			return flag;
		}

		// Token: 0x060082A5 RID: 33445 RVA: 0x00216738 File Offset: 0x00214938
		private bool IsUMIPValid()
		{
			IPAddress ipaddress;
			return this.taskMode != TestUMConnectivity.Mode.EndToEndTest || IPAddress.TryParse(this.umconnection.UmIP, out ipaddress);
		}

		// Token: 0x060082A6 RID: 33446 RVA: 0x00216764 File Offset: 0x00214964
		private void PrepareSessionAndMakeCall()
		{
			if (!this.umconnection.MakeCall(this.addressToCall, this.addressLocalCaller))
			{
				this.Cleanup();
				this.HandleError(this.umconnection.Error, TestUMConnectivity.EventId.TestExecuteError, this.momSourceString);
				return;
			}
			TestUMConnectivityHelper.UMConnectivityCallResults umconnectivityCallResults = (TestUMConnectivityHelper.UMConnectivityCallResults)this.result;
			umconnectivityCallResults.OutBoundSIPCallSuccess = true;
		}

		// Token: 0x060082A7 RID: 33447 RVA: 0x002167C0 File Offset: 0x002149C0
		private void Cleanup()
		{
			this.Cleanup(false);
		}

		// Token: 0x060082A8 RID: 33448 RVA: 0x002167CC File Offset: 0x002149CC
		private void Cleanup(bool cleanUpAnyways)
		{
			Thread.Sleep(1000);
			this.umconnection.EndCall();
			this.umconnection.Shutdown();
			TimeSpan timeSpan = ExDateTime.UtcNow.Subtract(this.startTime);
			TestUMConnectivityHelper.UMConnectivityCallResults umconnectivityCallResults = this.result as TestUMConnectivityHelper.UMConnectivityCallResults;
			if (umconnectivityCallResults != null)
			{
				umconnectivityCallResults.Latency = timeSpan.TotalMilliseconds;
			}
			if (this.result.EntireOperationSuccess || cleanUpAnyways)
			{
				this.WriteResult(this.result);
				string performanceInstance;
				switch (this.taskMode)
				{
				case TestUMConnectivity.Mode.LocalLoopTUILogonSpecific:
					performanceInstance = string.Concat(new string[]
					{
						this.dialPlan.ToString(),
						"|",
						this.Phone,
						"|",
						this.result.UmIPAddress
					});
					break;
				case TestUMConnectivity.Mode.EndToEndTest:
					performanceInstance = this.ipGateway.ToString() + "|" + this.Phone;
					break;
				default:
					performanceInstance = this.result.UmIPAddress;
					break;
				}
				this.monitoringData.PerformanceCounters.Add(new MonitoringPerformanceCounter(this.momSourceString, "Call Latency", performanceInstance, timeSpan.TotalMilliseconds));
				this.monitoringData.Events.Add(new MonitoringEvent(this.momSourceString, 1000, EventTypeEnumeration.Success, Strings.OperationSuccessful));
			}
		}

		// Token: 0x060082A9 RID: 33449 RVA: 0x0021692F File Offset: 0x00214B2F
		private void HandleError(LocalizedException localizedException, TestUMConnectivity.EventId id, string eventSource)
		{
			this.WriteErrorAndMonitoringEvent(localizedException, ErrorCategory.NotSpecified, null, (int)id, eventSource);
		}

		// Token: 0x060082AA RID: 33450 RVA: 0x0021693C File Offset: 0x00214B3C
		private void WriteResult(IConfigurable result)
		{
			base.WriteObject(result);
		}

		// Token: 0x060082AB RID: 33451 RVA: 0x00216948 File Offset: 0x00214B48
		private void WriteResults(IEnumerable dataObjects)
		{
			if (dataObjects != null)
			{
				foreach (object obj in dataObjects)
				{
					IConfigurable sendToPipeline = (IConfigurable)obj;
					base.WriteObject(sendToPipeline);
				}
			}
		}

		// Token: 0x060082AC RID: 33452 RVA: 0x002169A0 File Offset: 0x00214BA0
		private void WriteErrorAndMonitoringEvent(LocalizedException localizedException, ErrorCategory errorCategory, object target, int eventId, string eventSource)
		{
			this.monitoringData.Events.Add(new MonitoringEvent(eventSource, eventId, EventTypeEnumeration.Error, localizedException.LocalizedString));
			string performanceInstance;
			if (this.ipGateway != null)
			{
				performanceInstance = this.ipGateway.ToString() + "|" + this.Phone;
			}
			else if (this.TUILogon)
			{
				performanceInstance = string.Concat(new string[]
				{
					this.dialPlan.ToString(),
					"|",
					this.Phone,
					"|",
					this.localIPAddress
				});
			}
			else if (this.TUILogonAll)
			{
				performanceInstance = this.localIPAddress;
			}
			else
			{
				performanceInstance = this.localIPAddress;
			}
			this.monitoringData.PerformanceCounters.Add(new MonitoringPerformanceCounter(this.momSourceString, "Call Latency", performanceInstance, -1.0));
			base.WriteError(localizedException, errorCategory, target);
		}

		// Token: 0x060082AD RID: 33453 RVA: 0x00216A90 File Offset: 0x00214C90
		private void DoOwnValidate()
		{
			if (this.ipGateway != null)
			{
				this.momSourceString = "MSExchange Monitoring UMConnectivity Remote Voice";
				IEnumerable<UMIPGateway> objects = this.ipGateway.GetObjects<UMIPGateway>(null, this.configurationSession);
				using (IEnumerator<UMIPGateway> enumerator = objects.GetEnumerator())
				{
					if (!enumerator.MoveNext())
					{
						this.HandleValidationFailed(new LocalizedException(Strings.NonExistantIPGateway(this.ipGateway.ToString())), TestUMConnectivity.EventId.NoIPGatewaysFound);
					}
					this.adresults = enumerator.Current;
					if (enumerator.MoveNext())
					{
						this.HandleValidationFailed(new LocalizedException(Strings.MultipleIPGatewaysWithSameId(this.ipGateway.ToString())), TestUMConnectivity.EventId.MultipleIPGatewaysFound);
					}
				}
				UMIPGateway umipgateway = (UMIPGateway)this.adresults;
				this.ValidateRemoteProxy(umipgateway.Address.ToString(), umipgateway.Port);
				this.ValidateLocalSipUri();
				this.taskMode = TestUMConnectivity.Mode.EndToEndTest;
				this.umconnection = new RemoteConnectivityTester(this.hostToCall, this.portToCall);
			}
			else if (this.TUILogonAll)
			{
				this.momSourceString = "MSExchange Monitoring UMConnectivity Local TUI";
				this.taskMode = TestUMConnectivity.Mode.LocalLoopTUILogonAll;
				this.umconnection = new LocalTUILogonConnectivityTester();
			}
			else
			{
				if (this.TUILogon)
				{
					this.momSourceString = "MSExchange Monitoring UMConnectivity Local TUI";
					IEnumerable<UMDialPlan> objects2 = this.dialPlan.GetObjects<UMDialPlan>(null, this.configurationSession);
					using (IEnumerator<UMDialPlan> enumerator2 = objects2.GetEnumerator())
					{
						if (!enumerator2.MoveNext())
						{
							this.HandleValidationFailed(new LocalizedException(Strings.NonExistantDialPlan(this.dialPlan.ToString())), TestUMConnectivity.EventId.NoDialPlansFound);
						}
						this.adresults = enumerator2.Current;
						if (enumerator2.MoveNext())
						{
							this.HandleValidationFailed(new LocalizedException(Strings.MultipleDialplansWithSameId(this.dialPlan.ToString())), TestUMConnectivity.EventId.MultipleDialPlansFound);
						}
					}
					this.umDP = (UMDialPlan)this.adresults;
					this.taskMode = TestUMConnectivity.Mode.LocalLoopTUILogonSpecific;
					this.umconnection = new LocalTUILogonConnectivityTester();
					using (UMSubscriber umsubscriber = UMRecipient.Factory.FromExtension<UMSubscriber>(this.Phone, this.umDP, null))
					{
						if (umsubscriber == null)
						{
							this.HandleValidationFailed(new LocalizedException(Strings.InvalidUMUser(this.Phone, this.dialPlan.ToString())), TestUMConnectivity.EventId.InvalidUMUser);
						}
						else if (!this.IsMailboxLocal(umsubscriber.ExchangePrincipal))
						{
							LocalizedException localizedException = new LocalizedException(Strings.MailboxNotLocal(umsubscriber.ExchangeLegacyDN, umsubscriber.ExchangePrincipal.MailboxInfo.Location.ServerFqdn));
							this.HandleValidationFailed(localizedException, TestUMConnectivity.EventId.InvalidUMUser);
						}
						goto IL_29B;
					}
				}
				if (this.ResetPIN)
				{
					this.momSourceString = "MSExchange Monitoring UMConnectivity Reset Pin";
					this.taskMode = TestUMConnectivity.Mode.ResetPin;
				}
				else
				{
					this.taskMode = TestUMConnectivity.Mode.LocalLoop;
					this.momSourceString = "MSExchange Monitoring UMConnectivity Local Voice";
					this.umconnection = new LocalConnectivityTester();
				}
			}
			IL_29B:
			if (!this.Secured && this.MediaSecured)
			{
				this.HandleValidationFailed(new LocalizedException(Strings.SrtpWithoutTls), TestUMConnectivity.EventId.TestExecuteError);
			}
			if (!this.Secured && !string.IsNullOrEmpty(this.CertificateThumbprint))
			{
				this.HandleValidationFailed(new LocalizedException(Strings.CertWithoutTls), TestUMConnectivity.EventId.TestExecuteError);
			}
			if (this.Secured && string.IsNullOrEmpty(this.CertificateThumbprint) && string.IsNullOrEmpty(this.localLoopTarget.CertificateThumbprint))
			{
				this.HandleValidationFailed(new LocalizedException(Strings.MustSpecifyThumbprint), TestUMConnectivity.EventId.TestExecuteError);
			}
			if (this.taskMode == TestUMConnectivity.Mode.ResetPin)
			{
				this.GetListOfUMTestMailboxUsers();
			}
			else if (this.taskMode == TestUMConnectivity.Mode.EndToEndTest)
			{
				this.result = new TestUMConnectivityHelper.UMConnectivityCallResults();
				this.FormRemoteSipUri();
				this.FormLocalSipUri();
				this.testParams = new TestParameters(this.addressToCall, null, null, null, this.MonitoringContext);
			}
			else
			{
				this.localLoopTarget.CheckServiceRunning();
				if (!this.IsParameterSpecified("Secured"))
				{
					if (this.localLoopTarget.StartupMode == UMStartupMode.TCP)
					{
						this.Secured = false;
					}
					else
					{
						this.Secured = true;
					}
				}
				this.FormSIPUriToCall();
				switch (this.taskMode)
				{
				case TestUMConnectivity.Mode.LocalLoopTUILogonAll:
					this.GetListofUMTestMailboxUsersWithPin();
					break;
				case TestUMConnectivity.Mode.LocalLoopTUILogonSpecific:
					this.PerformFurtherChecks();
					this.result = new TestUMConnectivityHelper.LocalUMConnectivityResults();
					this.result.UmIPAddress = this.localIPAddress;
					break;
				default:
					this.result = this.localLoopTarget.CreateTestResult();
					this.result.UmIPAddress = this.localIPAddress;
					break;
				}
				if (this.taskMode != TestUMConnectivity.Mode.LocalLoop)
				{
					this.testParams = new TestParameters(this.addressToCall, this.PIN, this.Phone, (this.umDP == null) ? null : this.umDP.Name, this.MonitoringContext);
				}
				else
				{
					this.testParams = new TestParameters(this.addressToCall, null, null, null, this.MonitoringContext);
				}
			}
			if (this.taskMode != TestUMConnectivity.Mode.ResetPin && !this.umconnection.Initialize(this.ListenPort, this.Secured, this.MediaSecured, this.CertificateThumbprint))
			{
				this.HandleValidationFailed(this.umconnection.Error, TestUMConnectivity.EventId.TestExecuteError);
			}
		}

		// Token: 0x060082AE RID: 33454 RVA: 0x00216F84 File Offset: 0x00215184
		private void PerformFurtherChecks()
		{
			if (!this.UserFoundAndEnabled(this.Phone, this.umDP))
			{
				this.HandleValidationFailed(new UmUserProblem(), TestUMConnectivity.EventId.ADError);
			}
		}

		// Token: 0x060082AF RID: 33455 RVA: 0x00216FAA File Offset: 0x002151AA
		private void HandleValidationFailed(LocalizedException localizedException, TestUMConnectivity.EventId id)
		{
			if (this.umconnection != null)
			{
				this.umconnection.Shutdown();
			}
			this.HandleError(localizedException, id, this.momSourceString);
		}

		// Token: 0x060082B0 RID: 33456 RVA: 0x00216FD0 File Offset: 0x002151D0
		private void GetListOfUMTestMailboxUsers()
		{
			this.GetListOfCompatibleMailboxServers();
			this.resetPinResults = new List<TestUMConnectivityHelper.ResetPinResults>();
			this.pinResetUsers = new List<TestUMConnectivityHelper.UsersForResetPin>();
			foreach (Server server in this.allMailboxServers)
			{
				ADSite localSite = this.configurationSession.GetLocalSite();
				UmConnectivityCredentialsHelper umConnectivityCredentialsHelper = new UmConnectivityCredentialsHelper(localSite, server);
				umConnectivityCredentialsHelper.InitializeUser(true);
				if (umConnectivityCredentialsHelper.IsUserFound && umConnectivityCredentialsHelper.IsUserUMEnabled && umConnectivityCredentialsHelper.IsExchangePrincipalFound && this.IsMailboxLocal(umConnectivityCredentialsHelper.ExPrincipal))
				{
					this.pinResetUsers.Add(new TestUMConnectivityHelper.UsersForResetPin(umConnectivityCredentialsHelper.ExPrincipal, new NetworkCredential(umConnectivityCredentialsHelper.UserName, string.Empty, umConnectivityCredentialsHelper.UserDomain), umConnectivityCredentialsHelper.User, server.Fqdn));
				}
			}
		}

		// Token: 0x060082B1 RID: 33457 RVA: 0x002170B8 File Offset: 0x002152B8
		private void GetListofUMTestMailboxUsersWithPin()
		{
			this.GetListOfCompatibleMailboxServers();
			this.tuiEnumerateUsers = new List<TestUMConnectivityHelper.TestMailboxUserDetails>();
			this.tuiResults = new List<TestUMConnectivityHelper.TUILogonEnumerateResults>();
			foreach (Server server in this.allMailboxServers)
			{
				ADSite localSite = this.configurationSession.GetLocalSite();
				UmConnectivityCredentialsHelper umConnectivityCredentialsHelper = new UmConnectivityCredentialsHelper(localSite, server);
				umConnectivityCredentialsHelper.InitializeUser(false);
				if (umConnectivityCredentialsHelper.IsUserFound && umConnectivityCredentialsHelper.IsUserUMEnabled && umConnectivityCredentialsHelper.IsExchangePrincipalFound && this.IsMailboxLocal(umConnectivityCredentialsHelper.ExPrincipal))
				{
					if (!umConnectivityCredentialsHelper.SuccessfullyGotPin)
					{
						this.InitializePINRetrievalFaulures(server.Fqdn, umConnectivityCredentialsHelper.User.UMExtension);
					}
					else
					{
						this.tuiEnumerateUsers.Add(new TestUMConnectivityHelper.TestMailboxUserDetails(umConnectivityCredentialsHelper.User.UMExtension, umConnectivityCredentialsHelper.UMPin, umConnectivityCredentialsHelper.UserDP.Name, server.Fqdn));
					}
				}
			}
		}

		// Token: 0x060082B2 RID: 33458 RVA: 0x002171BC File Offset: 0x002153BC
		private void GetListOfCompatibleMailboxServers()
		{
			LocalizedException ex = null;
			Server server = null;
			bool flag = false;
			try
			{
				server = this.configurationSession.FindLocalServer();
			}
			catch (LocalServerNotFoundException ex2)
			{
				ex = ex2;
			}
			if (ex != null)
			{
				TopologyDiscoveryProblem localizedException = new TopologyDiscoveryProblem(ex.Message);
				this.HandleError(localizedException, TestUMConnectivity.EventId.ADError, this.momSourceString);
			}
			if (server != null && server.ServerSite != null)
			{
				QueryFilter filter = new AndFilter(new QueryFilter[]
				{
					new BitMaskAndFilter(ServerSchema.CurrentServerRole, 2UL),
					new ComparisonFilter(ComparisonOperator.Equal, ServerSchema.ServerSite, server.ServerSite)
				});
				ADPagedReader<Server> adpagedReader = this.configurationSession.FindPaged<Server>(null, QueryScope.SubTree, filter, null, 0);
				this.allMailboxServers = new List<Server>();
				foreach (Server server2 in adpagedReader)
				{
					if (!CommonUtil.IsServerCompatible(server2))
					{
						flag = true;
					}
					else
					{
						this.allMailboxServers.Add(server2);
					}
				}
			}
			if (flag)
			{
				this.WriteWarning(Strings.InvalidMailboxServerVersionForTUMCTask);
			}
			if (this.allMailboxServers == null || this.allMailboxServers.Count == 0)
			{
				NoMailboxServersFound localizedException2 = new NoMailboxServersFound();
				this.HandleError(localizedException2, TestUMConnectivity.EventId.ADError, this.momSourceString);
			}
		}

		// Token: 0x060082B3 RID: 33459 RVA: 0x00217308 File Offset: 0x00215508
		private void InitializePINRetrievalFaulures(string server, string phone)
		{
			TestUMConnectivityHelper.TUILogonEnumerateResults tuilogonEnumerateResults = new TestUMConnectivityHelper.TUILogonEnumerateResults();
			tuilogonEnumerateResults.TotalQueuedMessages = "0";
			tuilogonEnumerateResults.SuccessfullyRetrievedPINForValidUMUser = false;
			tuilogonEnumerateResults.CurrCalls = "0";
			tuilogonEnumerateResults.EntireOperationSuccess = false;
			tuilogonEnumerateResults.Latency = 0.0;
			tuilogonEnumerateResults.MailboxServerBeingTested = server;
			tuilogonEnumerateResults.OutBoundSIPCallSuccess = false;
			tuilogonEnumerateResults.UmserverAcceptingCallAnsweringMessages = true;
			tuilogonEnumerateResults.UmIPAddress = this.localIPAddress;
			this.tuiResults.Add(tuilogonEnumerateResults);
			this.tuiAllTestsListOfServersFailedToGetPin = this.tuiAllTestsListOfServersFailedToGetPin + server + ", ";
			string performanceInstance = string.Concat(new string[]
			{
				server,
				"|",
				phone,
				"|",
				this.localIPAddress
			});
			this.monitoringData.PerformanceCounters.Add(new MonitoringPerformanceCounter(this.momSourceString, "Call Latency", performanceInstance, -1.0));
		}

		// Token: 0x060082B4 RID: 33460 RVA: 0x002173EB File Offset: 0x002155EB
		private bool UserFoundAndEnabled(string phone, UMDialPlan dp)
		{
			return UMSubscriber.IsValidSubscriber(phone, dp, null);
		}

		// Token: 0x060082B5 RID: 33461 RVA: 0x002173F8 File Offset: 0x002155F8
		private void PopulateResults()
		{
			((TestUMConnectivityHelper.UMConnectivityCallResults)this.result).CurrCalls = this.umconnection.CurrCalls;
			TestUMConnectivity.Mode mode = this.taskMode;
			if (mode == TestUMConnectivity.Mode.EndToEndTest)
			{
				this.result.UmIPAddress = this.umconnection.UmIP;
				return;
			}
			this.LocalBackendResult.UmserverAcceptingCallAnsweringMessages = ((LocalConnectivityTester)this.umconnection).AcceptingCalls;
			this.LocalBackendResult.TotalQueuedMessages = ((LocalConnectivityTester)this.umconnection).TotalQueueLength;
		}

		// Token: 0x060082B6 RID: 33462 RVA: 0x00217478 File Offset: 0x00215678
		private void FormSIPUriToCall()
		{
			int num = (this.RemotePort == 0) ? this.localLoopTarget.GetPort(this.Secured) : this.RemotePort;
			string targetFqdn = this.localLoopTarget.TargetFqdn;
			this.addressToCall = "sip:" + targetFqdn + ":" + num.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x060082B7 RID: 33463 RVA: 0x002174D8 File Offset: 0x002156D8
		private void ValidateRemoteProxy(string address, int port)
		{
			if (!this.Secured)
			{
				this.hostToCall = address;
				this.portToCall = ((port != 0) ? port : 5060);
				return;
			}
			IPHostEntry iphostEntry;
			if (Utils.HasValidDNSRecord(address, out iphostEntry))
			{
				this.hostToCall = iphostEntry.HostName;
				this.portToCall = ((port != 0) ? port : 5061);
				return;
			}
			DNSEntryNotFound localizedException = new DNSEntryNotFound();
			this.HandleError(localizedException, TestUMConnectivity.EventId.DNSEntryNotFound, this.momSourceString);
		}

		// Token: 0x060082B8 RID: 33464 RVA: 0x00217548 File Offset: 0x00215748
		private void FormRemoteSipUri()
		{
			this.addressToCall = (this.Phone.StartsWith("sip:", StringComparison.InvariantCulture) ? this.Phone : string.Concat(new string[]
			{
				"sip:",
				this.Phone,
				"@",
				this.hostToCall,
				":",
				this.portToCall.ToString(CultureInfo.InvariantCulture)
			}));
		}

		// Token: 0x060082B9 RID: 33465 RVA: 0x002175C2 File Offset: 0x002157C2
		private void FormLocalSipUri()
		{
			this.addressLocalCaller = (string.IsNullOrEmpty(this.From) ? string.Empty : this.From);
		}

		// Token: 0x060082BA RID: 33466 RVA: 0x002175E4 File Offset: 0x002157E4
		private void ValidateLocalSipUri()
		{
			if (!string.IsNullOrEmpty(this.From) && !this.From.StartsWith("sip:", StringComparison.InvariantCulture))
			{
				LocalizedException localizedException = new TUC_SipUriError(" \"From\" ");
				this.HandleError(localizedException, TestUMConnectivity.EventId.TestExecuteError, this.momSourceString);
			}
		}

		// Token: 0x060082BB RID: 33467 RVA: 0x00217630 File Offset: 0x00215830
		private void WriteErrorIfMissingUCMA()
		{
			bool flag = false;
			Exception innerException = null;
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\UCMA\\{902F4F35-D5DC-4363-8671-D5EF0D26C21D}"))
				{
					if (registryKey != null)
					{
						string text = registryKey.GetValue("Version") as string;
						if (!string.IsNullOrEmpty(text))
						{
							Version version = new Version(text);
							if (version.Major >= 5 && version.Minor >= 0)
							{
								flag = true;
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				if (!(ex is ArgumentException) && !(ex is FormatException) && !(ex is OverflowException) && !(ex is SecurityException))
				{
					throw;
				}
				innerException = ex;
			}
			if (!flag)
			{
				base.WriteError(new UCMAPreReqException(innerException), ErrorCategory.NotSpecified, this);
			}
		}

		// Token: 0x04003F3F RID: 16191
		private const string SIP = "sip:";

		// Token: 0x04003F40 RID: 16192
		private const string AT = "@";

		// Token: 0x04003F41 RID: 16193
		private const string COLON = ":";

		// Token: 0x04003F42 RID: 16194
		private const string LocalVoiceSource = " Local Voice";

		// Token: 0x04003F43 RID: 16195
		private const string LocalVoiceTUISource = " Local TUI";

		// Token: 0x04003F44 RID: 16196
		private const string RemoteVoiceSource = " Remote Voice";

		// Token: 0x04003F45 RID: 16197
		private const string ResetPinSource = " Reset Pin";

		// Token: 0x04003F46 RID: 16198
		private const string TaskNoun = "UMConnectivity";

		// Token: 0x04003F47 RID: 16199
		private const string MonitoringPerformanceObject = "Exchange Monitoring";

		// Token: 0x04003F48 RID: 16200
		private const double LatencyPerformanceInCaseOfError = -1.0;

		// Token: 0x04003F49 RID: 16201
		private const string TaskMonitoringEventSource = "MSExchange Monitoring UMConnectivity";

		// Token: 0x04003F4A RID: 16202
		private ITopologyConfigurationSession configurationSession;

		// Token: 0x04003F4B RID: 16203
		private IRecipientSession globalCatalogSession;

		// Token: 0x04003F4C RID: 16204
		private string momSourceString;

		// Token: 0x04003F4D RID: 16205
		private MonitoringData monitoringData = new MonitoringData();

		// Token: 0x04003F4E RID: 16206
		private BaseUMconnectivityTester umconnection;

		// Token: 0x04003F4F RID: 16207
		private ExDateTime startTime;

		// Token: 0x04003F50 RID: 16208
		private TestUMConnectivityHelper.UMConnectivityResults result;

		// Token: 0x04003F51 RID: 16209
		private List<TestUMConnectivityHelper.TUILogonEnumerateResults> tuiResults;

		// Token: 0x04003F52 RID: 16210
		private List<TestUMConnectivityHelper.ResetPinResults> resetPinResults;

		// Token: 0x04003F53 RID: 16211
		private List<Server> allMailboxServers;

		// Token: 0x04003F54 RID: 16212
		private string tuiAllTestsListOfServersPassed = string.Empty;

		// Token: 0x04003F55 RID: 16213
		private string tuiAllTestsListOfServersFailedToGetPin = string.Empty;

		// Token: 0x04003F56 RID: 16214
		private string tuiAllTestsListOfServersFailedToMakeCall = string.Empty;

		// Token: 0x04003F57 RID: 16215
		private string tuiAllTestsListOfServersFailedToExcuteScenario = string.Empty;

		// Token: 0x04003F58 RID: 16216
		private List<TestUMConnectivityHelper.TestMailboxUserDetails> tuiEnumerateUsers;

		// Token: 0x04003F59 RID: 16217
		private List<TestUMConnectivityHelper.UsersForResetPin> pinResetUsers;

		// Token: 0x04003F5A RID: 16218
		private string pinResetTestsListOfServersPassed = string.Empty;

		// Token: 0x04003F5B RID: 16219
		private string pinResetTestsListOfServersFailedToResetPin = string.Empty;

		// Token: 0x04003F5C RID: 16220
		private string pinResetTestsListOfServersFailedToResetPasswd = string.Empty;

		// Token: 0x04003F5D RID: 16221
		private string addressToCall;

		// Token: 0x04003F5E RID: 16222
		private string addressLocalCaller;

		// Token: 0x04003F5F RID: 16223
		private string hostToCall;

		// Token: 0x04003F60 RID: 16224
		private int portToCall;

		// Token: 0x04003F61 RID: 16225
		private string localIPAddress;

		// Token: 0x04003F62 RID: 16226
		private UMDialPlan umDP;

		// Token: 0x04003F63 RID: 16227
		private TestParameters testParams;

		// Token: 0x04003F64 RID: 16228
		private IConfigurable adresults;

		// Token: 0x04003F65 RID: 16229
		private UMIPGatewayIdParameter ipGateway;

		// Token: 0x04003F66 RID: 16230
		private UMDialPlanIdParameter dialPlan;

		// Token: 0x04003F67 RID: 16231
		private TestUMConnectivity.Mode taskMode;

		// Token: 0x04003F68 RID: 16232
		private LocalLoopTargetStrategy localLoopTarget;

		// Token: 0x02000D4A RID: 3402
		private enum Mode
		{
			// Token: 0x04003F6A RID: 16234
			LocalLoop,
			// Token: 0x04003F6B RID: 16235
			LocalLoopTUILogonAll,
			// Token: 0x04003F6C RID: 16236
			LocalLoopTUILogonSpecific,
			// Token: 0x04003F6D RID: 16237
			EndToEndTest,
			// Token: 0x04003F6E RID: 16238
			ResetPin
		}

		// Token: 0x02000D4B RID: 3403
		private enum EventId
		{
			// Token: 0x04003F70 RID: 16240
			OperationSuccessFul = 1000,
			// Token: 0x04003F71 RID: 16241
			MultipleIPGatewaysFound,
			// Token: 0x04003F72 RID: 16242
			NoIPGatewaysFound,
			// Token: 0x04003F73 RID: 16243
			NoDialPlansFound,
			// Token: 0x04003F74 RID: 16244
			MultipleDialPlansFound,
			// Token: 0x04003F75 RID: 16245
			DNSEntryNotFound,
			// Token: 0x04003F76 RID: 16246
			TestExecuteError,
			// Token: 0x04003F77 RID: 16247
			ADError = 1008,
			// Token: 0x04003F78 RID: 16248
			ServiceNotInstalledOrRunning,
			// Token: 0x04003F79 RID: 16249
			InvalidUMUser
		}

		// Token: 0x02000D4C RID: 3404
		private enum TUILogonAllOutcome
		{
			// Token: 0x04003F7B RID: 16251
			MakeCallFailed,
			// Token: 0x04003F7C RID: 16252
			ExecuteScenarioFailed,
			// Token: 0x04003F7D RID: 16253
			Success
		}

		// Token: 0x02000D4D RID: 3405
		private enum PinResetOutcome
		{
			// Token: 0x04003F7F RID: 16255
			PasswordResetFailed,
			// Token: 0x04003F80 RID: 16256
			PinResetFailed,
			// Token: 0x04003F81 RID: 16257
			Success
		}
	}
}
