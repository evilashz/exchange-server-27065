using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Monitoring;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x02000D3E RID: 3390
	[Cmdlet("Set", "UMConnectivityTestCredentials")]
	public class SetUMConnectivityTestCredentials : Task
	{
		// Token: 0x1700285C RID: 10332
		// (get) Token: 0x060081ED RID: 33261 RVA: 0x0021345D File Offset: 0x0021165D
		// (set) Token: 0x060081EE RID: 33262 RVA: 0x0021347E File Offset: 0x0021167E
		[Parameter(Mandatory = false)]
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

		// Token: 0x1700285D RID: 10333
		// (get) Token: 0x060081EF RID: 33263 RVA: 0x00213496 File Offset: 0x00211696
		// (set) Token: 0x060081F0 RID: 33264 RVA: 0x002134AD File Offset: 0x002116AD
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

		// Token: 0x060081F1 RID: 33265 RVA: 0x002134C0 File Offset: 0x002116C0
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			this.DoOwnValidate();
			TaskLogger.LogExit();
		}

		// Token: 0x060081F2 RID: 33266 RVA: 0x002134D8 File Offset: 0x002116D8
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			this.configurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(this.DomainController, false, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 148, "InternalProcessRecord", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\um\\setUMConnectivityTestCredentials.cs");
			ADSite localSite = this.configurationSession.GetLocalSite();
			UmConnectivityCredentialsHelper umConnectivityCredentialsHelper = new UmConnectivityCredentialsHelper(localSite, this.serv);
			umConnectivityCredentialsHelper.InitializeUser(false);
			if (!umConnectivityCredentialsHelper.IsUserFound || !umConnectivityCredentialsHelper.IsUserUMEnabled)
			{
				this.HandleSuccess();
			}
			if (umConnectivityCredentialsHelper.IsExchangePrincipalFound)
			{
				if (umConnectivityCredentialsHelper.SuccessfullyGotPin)
				{
					this.SaveThePin(umConnectivityCredentialsHelper);
					this.HandleSuccess();
				}
				else
				{
					SUC_CouldnotRetreivePasswd localizedException = new SUC_CouldnotRetreivePasswd();
					this.HandleError(localizedException, SetUMConnectivityTestCredentials.EventId.ADError, "MSExchange Monitoring UMConnectivityTestCredentials");
				}
			}
			else
			{
				SUC_ExchangePrincipalError localizedException2 = new SUC_ExchangePrincipalError();
				this.HandleError(localizedException2, SetUMConnectivityTestCredentials.EventId.ADError, "MSExchange Monitoring UMConnectivityTestCredentials");
			}
			TaskLogger.LogExit();
		}

		// Token: 0x060081F3 RID: 33267 RVA: 0x002135A8 File Offset: 0x002117A8
		private void DoOwnValidate()
		{
			try
			{
				IConfigurationSession session = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 198, "DoOwnValidate", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\um\\setUMConnectivityTestCredentials.cs");
				this.serv = Utility.GetServerFromName(Utils.GetLocalHostName(), session);
				if (this.serv == null)
				{
					ADError localizedException = new ADError();
					this.HandleError(localizedException, SetUMConnectivityTestCredentials.EventId.ADError, "MSExchange Monitoring UMConnectivityTestCredentials");
				}
			}
			catch (ADTransientException innerException)
			{
				ADError localizedException2 = new ADError(innerException);
				this.HandleError(localizedException2, SetUMConnectivityTestCredentials.EventId.ADError, "MSExchange Monitoring UMConnectivityTestCredentials");
			}
			catch (DataSourceOperationException innerException2)
			{
				ADError localizedException3 = new ADError(innerException2);
				this.HandleError(localizedException3, SetUMConnectivityTestCredentials.EventId.ADError, "MSExchange Monitoring UMConnectivityTestCredentials");
			}
			catch (DataValidationException innerException3)
			{
				ADError localizedException4 = new ADError(innerException3);
				this.HandleError(localizedException4, SetUMConnectivityTestCredentials.EventId.ADError, "MSExchange Monitoring UMConnectivityTestCredentials");
			}
			if (this.serv != null)
			{
				if (!UmConnectivityCredentialsHelper.IsMailboxServer(this.serv))
				{
					SUC_NotMailboxServer localizedException5 = new SUC_NotMailboxServer();
					this.HandleError(localizedException5, SetUMConnectivityTestCredentials.EventId.ADError, "MSExchange Monitoring UMConnectivityTestCredentials");
					return;
				}
			}
			else
			{
				ADError localizedException6 = new ADError();
				this.HandleError(localizedException6, SetUMConnectivityTestCredentials.EventId.ADError, "MSExchange Monitoring UMConnectivityTestCredentials");
			}
		}

		// Token: 0x060081F4 RID: 33268 RVA: 0x002136D0 File Offset: 0x002118D0
		private void SaveThePin(UmConnectivityCredentialsHelper help)
		{
			LocalizedException ex = UmConnectivityCredentialsHelper.SaveUMPin(help.User, help.UMPin);
			if (ex != null)
			{
				this.HandleError(ex, SetUMConnectivityTestCredentials.EventId.SavePinFailure, "MSExchange Monitoring UMConnectivityTestCredentials");
			}
		}

		// Token: 0x060081F5 RID: 33269 RVA: 0x00213703 File Offset: 0x00211903
		private void HandleError(LocalizedException localizedException, SetUMConnectivityTestCredentials.EventId id, string eventSource)
		{
			this.WriteErrorAndMonitoringEvent(localizedException, ErrorCategory.NotSpecified, null, (int)id, eventSource);
			if (this.MonitoringContext)
			{
				base.WriteObject(this.monitoringData);
			}
		}

		// Token: 0x060081F6 RID: 33270 RVA: 0x00213724 File Offset: 0x00211924
		private void HandleSuccess()
		{
			this.monitoringData.Events.Add(new MonitoringEvent("MSExchange Monitoring UMConnectivityTestCredentials", 1000, EventTypeEnumeration.Success, Strings.OperationSuccessful));
			if (this.MonitoringContext)
			{
				base.WriteObject(this.monitoringData);
			}
		}

		// Token: 0x060081F7 RID: 33271 RVA: 0x00213764 File Offset: 0x00211964
		private void WriteErrorAndMonitoringEvent(LocalizedException localizedException, ErrorCategory errorCategory, object target, int eventId, string eventSource)
		{
			this.monitoringData.Events.Add(new MonitoringEvent(eventSource, eventId, EventTypeEnumeration.Error, localizedException.LocalizedString));
			base.WriteError(localizedException, errorCategory, target);
		}

		// Token: 0x04003F30 RID: 16176
		private const string TaskMonitoringEventSource = "MSExchange Monitoring UMConnectivityTestCredentials";

		// Token: 0x04003F31 RID: 16177
		private MonitoringData monitoringData = new MonitoringData();

		// Token: 0x04003F32 RID: 16178
		private Server serv;

		// Token: 0x04003F33 RID: 16179
		private ITopologyConfigurationSession configurationSession;

		// Token: 0x02000D3F RID: 3391
		private enum EventId
		{
			// Token: 0x04003F35 RID: 16181
			OperationSuccessFul = 1000,
			// Token: 0x04003F36 RID: 16182
			ADError,
			// Token: 0x04003F37 RID: 16183
			SavePinFailure
		}
	}
}
