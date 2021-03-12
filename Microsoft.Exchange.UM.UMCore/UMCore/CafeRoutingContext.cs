using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x0200024D RID: 589
	internal class CafeRoutingContext : DisposableBase, IRoutingContext
	{
		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x06001147 RID: 4423 RVA: 0x0004CF31 File Offset: 0x0004B131
		// (set) Token: 0x06001148 RID: 4424 RVA: 0x0004CF39 File Offset: 0x0004B139
		public SipRoutingHelper RoutingHelper { get; internal set; }

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x06001149 RID: 4425 RVA: 0x0004CF42 File Offset: 0x0004B142
		public string CallId
		{
			get
			{
				return this.CallInfo.CallId;
			}
		}

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x0600114A RID: 4426 RVA: 0x0004CF4F File Offset: 0x0004B14F
		// (set) Token: 0x0600114B RID: 4427 RVA: 0x0004CF57 File Offset: 0x0004B157
		public UMDialPlan DialPlan { get; set; }

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x0600114C RID: 4428 RVA: 0x0004CF60 File Offset: 0x0004B160
		public bool IsSecuredCall
		{
			get
			{
				return this.CallInfo.RemoteCertificate != null;
			}
		}

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x0600114D RID: 4429 RVA: 0x0004CF73 File Offset: 0x0004B173
		public PlatformSipUri RequestUriOfCall
		{
			get
			{
				return this.CallInfo.RequestUri;
			}
		}

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x0600114E RID: 4430 RVA: 0x0004CF80 File Offset: 0x0004B180
		// (set) Token: 0x0600114F RID: 4431 RVA: 0x0004CF88 File Offset: 0x0004B188
		public PlatformSipUri RedirectUri { get; set; }

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x06001150 RID: 4432 RVA: 0x0004CF91 File Offset: 0x0004B191
		public int RedirectCode
		{
			get
			{
				return this.RoutingHelper.RedirectResponseCode;
			}
		}

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x06001151 RID: 4433 RVA: 0x0004CF9E File Offset: 0x0004B19E
		// (set) Token: 0x06001152 RID: 4434 RVA: 0x0004CFA6 File Offset: 0x0004B1A6
		public bool IsDiagnosticCall { get; private set; }

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x06001153 RID: 4435 RVA: 0x0004CFAF File Offset: 0x0004B1AF
		// (set) Token: 0x06001154 RID: 4436 RVA: 0x0004CFB7 File Offset: 0x0004B1B7
		public bool IsActiveMonitoring { get; private set; }

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x06001155 RID: 4437 RVA: 0x0004CFC0 File Offset: 0x0004B1C0
		// (set) Token: 0x06001156 RID: 4438 RVA: 0x0004CFC8 File Offset: 0x0004B1C8
		public PlatformCallInfo CallInfo { get; private set; }

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x06001157 RID: 4439 RVA: 0x0004CFD1 File Offset: 0x0004B1D1
		// (set) Token: 0x06001158 RID: 4440 RVA: 0x0004CFD9 File Offset: 0x0004B1D9
		public DiagnosticHelper Tracer { get; private set; }

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x06001159 RID: 4441 RVA: 0x0004CFE2 File Offset: 0x0004B1E2
		// (set) Token: 0x0600115A RID: 4442 RVA: 0x0004CFEA File Offset: 0x0004B1EA
		public string ReferredByHeader { get; private set; }

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x0600115B RID: 4443 RVA: 0x0004CFF3 File Offset: 0x0004B1F3
		public bool IsDivertedCall
		{
			get
			{
				return this.CallInfo.DiversionInfo.Count > 0;
			}
		}

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x0600115C RID: 4444 RVA: 0x0004D008 File Offset: 0x0004B208
		// (set) Token: 0x0600115D RID: 4445 RVA: 0x0004D010 File Offset: 0x0004B210
		public string RemoteMatchedFqdn
		{
			get
			{
				return this.remoteMatchedFqdn;
			}
			set
			{
				this.remoteMatchedFqdn = value;
				this.RemotePeer = (string.IsNullOrEmpty(this.remoteMatchedFqdn) ? new UMSmartHost(this.CallInfo.RemotePeer.ToString()) : new UMSmartHost(this.remoteMatchedFqdn));
			}
		}

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x0600115E RID: 4446 RVA: 0x0004D04E File Offset: 0x0004B24E
		// (set) Token: 0x0600115F RID: 4447 RVA: 0x0004D056 File Offset: 0x0004B256
		public UMIPGateway Gateway { get; set; }

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x06001160 RID: 4448 RVA: 0x0004D05F File Offset: 0x0004B25F
		// (set) Token: 0x06001161 RID: 4449 RVA: 0x0004D067 File Offset: 0x0004B267
		public UMAutoAttendant AutoAttendant { get; set; }

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x06001162 RID: 4450 RVA: 0x0004D070 File Offset: 0x0004B270
		// (set) Token: 0x06001163 RID: 4451 RVA: 0x0004D078 File Offset: 0x0004B278
		public UMHuntGroup HuntGroup { get; set; }

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x06001164 RID: 4452 RVA: 0x0004D081 File Offset: 0x0004B281
		// (set) Token: 0x06001165 RID: 4453 RVA: 0x0004D089 File Offset: 0x0004B289
		public IADSystemConfigurationLookup ScopedADConfigurationSession { get; set; }

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x06001166 RID: 4454 RVA: 0x0004D092 File Offset: 0x0004B292
		// (set) Token: 0x06001167 RID: 4455 RVA: 0x0004D09A File Offset: 0x0004B29A
		public PhoneNumber CalledParty { get; set; }

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x06001168 RID: 4456 RVA: 0x0004D0A3 File Offset: 0x0004B2A3
		// (set) Token: 0x06001169 RID: 4457 RVA: 0x0004D0AB File Offset: 0x0004B2AB
		public PhoneNumber CallingParty { get; set; }

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x0600116A RID: 4458 RVA: 0x0004D0B4 File Offset: 0x0004B2B4
		// (set) Token: 0x0600116B RID: 4459 RVA: 0x0004D0BC File Offset: 0x0004B2BC
		public UMRecipient DivertedUser { get; set; }

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x0600116C RID: 4460 RVA: 0x0004D0C5 File Offset: 0x0004B2C5
		// (set) Token: 0x0600116D RID: 4461 RVA: 0x0004D0CD File Offset: 0x0004B2CD
		public Guid TenantGuid { get; set; }

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x0600116E RID: 4462 RVA: 0x0004D0D6 File Offset: 0x0004B2D6
		public bool IsAnonymousCaller
		{
			get
			{
				return UtilityMethods.IsAnonymousNumber(this.CallInfo.RequestUri.User);
			}
		}

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x0600116F RID: 4463 RVA: 0x0004D0ED File Offset: 0x0004B2ED
		public string PilotNumber
		{
			get
			{
				if (!this.IsAnonymousCaller)
				{
					return this.CallInfo.RequestUri.User;
				}
				return null;
			}
		}

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x06001170 RID: 4464 RVA: 0x0004D109 File Offset: 0x0004B309
		// (set) Token: 0x06001171 RID: 4465 RVA: 0x0004D111 File Offset: 0x0004B311
		public UMADSettings CallRouterConfiguration { get; private set; }

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x06001172 RID: 4466 RVA: 0x0004D11A File Offset: 0x0004B31A
		public PlatformSipUri ToUri
		{
			get
			{
				return this.CallInfo.CalledParty.Uri;
			}
		}

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x06001173 RID: 4467 RVA: 0x0004D12C File Offset: 0x0004B32C
		public PlatformSipUri FromUri
		{
			get
			{
				return this.CallInfo.CallingParty.Uri;
			}
		}

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x06001174 RID: 4468 RVA: 0x0004D13E File Offset: 0x0004B33E
		public bool IsAccessProxyCall
		{
			get
			{
				return this.RoutingHelper.SupportsMsOrganizationRouting;
			}
		}

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x06001175 RID: 4469 RVA: 0x0004D14B File Offset: 0x0004B34B
		// (set) Token: 0x06001176 RID: 4470 RVA: 0x0004D153 File Offset: 0x0004B353
		public UMSmartHost RemotePeer { get; private set; }

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x06001177 RID: 4471 RVA: 0x0004D15C File Offset: 0x0004B35C
		// (set) Token: 0x06001178 RID: 4472 RVA: 0x0004D164 File Offset: 0x0004B364
		public string UMPodRedirectTemplate { get; set; }

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x06001179 RID: 4473 RVA: 0x0004D16D File Offset: 0x0004B36D
		internal ExDateTime CallReceivedTime
		{
			get
			{
				return this.callStartTime;
			}
		}

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x0600117A RID: 4474 RVA: 0x0004D175 File Offset: 0x0004B375
		// (set) Token: 0x0600117B RID: 4475 RVA: 0x0004D17D File Offset: 0x0004B37D
		private ExDateTime CallFinishTime
		{
			get
			{
				return this.callFinishTime;
			}
			set
			{
				this.callFinishTime = value;
			}
		}

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x0600117C RID: 4476 RVA: 0x0004D188 File Offset: 0x0004B388
		internal TimeSpan CallLatency
		{
			get
			{
				return this.CallFinishTime.UniversalTime.Subtract(this.CallReceivedTime.UniversalTime);
			}
		}

		// Token: 0x0600117D RID: 4477 RVA: 0x0004D1BC File Offset: 0x0004B3BC
		public CafeRoutingContext(PlatformCallInfo callInfo, UMADSettings config)
		{
			ValidateArgument.NotNull(callInfo, "PlatformCallInfo");
			ValidateArgument.NotNull(config, "UMADSettings");
			this.CallRouterConfiguration = config;
			this.UMPodRedirectTemplate = this.CallRouterConfiguration.UMPodRedirectTemplate;
			this.IsDiagnosticCall = SipPeerManager.Instance.IsLocalDiagnosticCall(callInfo.RemotePeer, callInfo.RemoteHeaders);
			this.IsActiveMonitoring = (!string.IsNullOrEmpty(callInfo.RemoteUserAgent) && callInfo.RemoteUserAgent.IndexOf("ActiveMonitoringClient", StringComparison.OrdinalIgnoreCase) > 0);
			this.RoutingHelper = SipRoutingHelper.Create(callInfo);
			this.Tracer = new DiagnosticHelper(this, ExTraceGlobals.UMCallRouterTracer);
			this.CallInfo = callInfo;
			this.remoteMatchedFqdn = (callInfo.RemoteMatchedFQDN ?? string.Empty);
			this.RemotePeer = (string.IsNullOrEmpty(this.remoteMatchedFqdn) ? new UMSmartHost(this.CallInfo.RemotePeer.ToString()) : new UMSmartHost(this.remoteMatchedFqdn));
			this.ReferredByHeader = RouterUtils.GetReferredByHeader(this.CallInfo.RemoteHeaders);
			this.Tracer.Trace("RouterCallHandler : CallId:{0}, remoteEP: {1}, remoteFqdn: {2}, RoutingHelperType {3}", new object[]
			{
				callInfo.CallId,
				callInfo.RemotePeer,
				string.IsNullOrEmpty(callInfo.RemoteMatchedFQDN) ? "null" : callInfo.RemoteMatchedFQDN,
				this.RoutingHelper.GetType().Name
			});
		}

		// Token: 0x0600117E RID: 4478 RVA: 0x0004D343 File Offset: 0x0004B543
		internal void AddDiagnosticsTimer(Timer timer)
		{
			if (timer != null)
			{
				this.diagnosticsTimers.Add(timer);
			}
		}

		// Token: 0x0600117F RID: 4479 RVA: 0x0004D35C File Offset: 0x0004B55C
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				this.CallFinishTime = ExDateTime.UtcNow;
				CafeLoggingHelper.LogCallStatistics(this);
				if (this.DivertedUser != null)
				{
					this.DivertedUser.Dispose();
					this.DivertedUser = null;
				}
				this.diagnosticsTimers.ForEach(delegate(Timer o)
				{
					o.Dispose();
				});
				this.diagnosticsTimers.Clear();
			}
		}

		// Token: 0x06001180 RID: 4480 RVA: 0x0004D3CA File Offset: 0x0004B5CA
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<CafeRoutingContext>(this);
		}

		// Token: 0x04000BD4 RID: 3028
		private string remoteMatchedFqdn;

		// Token: 0x04000BD5 RID: 3029
		private List<Timer> diagnosticsTimers = new List<Timer>(2);

		// Token: 0x04000BD6 RID: 3030
		private ExDateTime callStartTime = ExDateTime.UtcNow;

		// Token: 0x04000BD7 RID: 3031
		private ExDateTime callFinishTime = ExDateTime.UtcNow;
	}
}
