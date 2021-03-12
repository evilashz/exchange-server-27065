using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.IsMemberOfProvider;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.MessageSecurity;
using Microsoft.Exchange.ProcessManager;
using Microsoft.Exchange.Transport;
using Microsoft.Exchange.Transport.Categorizer;
using Microsoft.Exchange.Transport.Logging;
using Microsoft.Exchange.Transport.MessageThrottling;
using Microsoft.Exchange.Transport.ShadowRedundancy;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x020004C0 RID: 1216
	internal class SmtpInServerState
	{
		// Token: 0x060036FF RID: 14079 RVA: 0x000E1E8C File Offset: 0x000E008C
		public SmtpInServerState()
		{
			this.CurrentTime = DateTime.UtcNow;
			this.ServiceState = ServiceState.Inactive;
			this.RejectionSmtpResponse = SmtpResponse.Empty;
			this.ThrottleDelay = TimeSpan.Zero;
			this.localIPAddresses = new Lazy<IReadOnlyList<IPAddress>>(new Func<IReadOnlyList<IPAddress>>(this.DetermineLocalIPAddresses));
		}

		// Token: 0x06003700 RID: 14080 RVA: 0x000E1EE0 File Offset: 0x000E00E0
		public SmtpInServerState(ISmtpInServer smtpInServer)
		{
			ArgumentValidator.ThrowIfNull("smtpInServer", smtpInServer);
			this.CurrentTime = smtpInServer.CurrentTime;
			this.ServiceState = smtpInServer.TargetRunningState;
			this.ThrottleDelay = smtpInServer.ThrottleDelay;
			this.ThrottleDelayContext = smtpInServer.ThrottleDelayContext;
			this.RejectCommands = smtpInServer.RejectCommands;
			this.RejectMailFromInternet = smtpInServer.RejectMailFromInternet;
			this.RejectSubmits = smtpInServer.RejectSubmits;
			this.RejectionSmtpResponse = smtpInServer.RejectionSmtpResponse;
		}

		// Token: 0x06003701 RID: 14081 RVA: 0x000E1F60 File Offset: 0x000E0160
		public void SetRunTimeDependencies(IAgentRuntime theAgentRuntime, ICategorizer theCategorizer, ICertificateCache theCertificateCache, ICertificateValidator theCertificateValidator, IIsMemberOfResolver<RoutingAddress> theIsMemberOfResolver, IMessageThrottlingManager theMessageThrottlingManager, IShadowRedundancyManager theShadowRedundancyManager, ISmtpInMailItemStorage theMailItemStorage, IQueueQuotaComponent theQueueQuotaComponent)
		{
			ArgumentValidator.ThrowIfNull("theAgentRuntime", theAgentRuntime);
			ArgumentValidator.ThrowIfNull("theCategorizer", theCategorizer);
			ArgumentValidator.ThrowIfNull("theCertificateCache", theCertificateCache);
			ArgumentValidator.ThrowIfNull("theCertificateValidator", theCertificateValidator);
			ArgumentValidator.ThrowIfNull("theIsMemberOfResolver", theIsMemberOfResolver);
			ArgumentValidator.ThrowIfNull("theMessageThrottlingManager", theMessageThrottlingManager);
			ArgumentValidator.ThrowIfNull("theMailItemStorage", theMailItemStorage);
			this.agentRuntime = theAgentRuntime;
			this.categorizer = theCategorizer;
			this.certificateCache = theCertificateCache;
			this.certificateValidator = theCertificateValidator;
			this.isMemberOfResolver = theIsMemberOfResolver;
			this.messageThrottlingManager = theMessageThrottlingManager;
			this.shadowRedundancyManager = theShadowRedundancyManager;
			this.mailItemStorage = theMailItemStorage;
			this.queueQuotaComponent = theQueueQuotaComponent;
			this.maxTlsConnectionsPerMinute = this.transportConfiguration.LocalServer.TransportServer.MaxReceiveTlsRatePerMinute;
		}

		// Token: 0x06003702 RID: 14082 RVA: 0x000E201E File Offset: 0x000E021E
		public void SetLoadTimeDependencies(IProtocolLog protocolLogToUse, ITransportAppConfig transportAppConfigToUse, ITransportConfiguration transportConfigurationToUse)
		{
			ArgumentValidator.ThrowIfNull("protocolLogToUse", protocolLogToUse);
			ArgumentValidator.ThrowIfNull("transportAppConfigToUse", transportAppConfigToUse);
			ArgumentValidator.ThrowIfNull("transportConfigurationToUse", transportConfigurationToUse);
			this.protocolLog = protocolLogToUse;
			this.transportAppConfig = transportAppConfigToUse;
			this.transportConfiguration = transportConfigurationToUse;
		}

		// Token: 0x06003703 RID: 14083 RVA: 0x000E206C File Offset: 0x000E026C
		public bool IsLocalAddress(IPAddress ipAddress)
		{
			return IPAddress.IsLoopback(ipAddress) || this.localIPAddresses.Value.Any((IPAddress localAddress) => localAddress.Equals(ipAddress));
		}

		// Token: 0x1700106A RID: 4202
		// (get) Token: 0x06003704 RID: 14084 RVA: 0x000E20B4 File Offset: 0x000E02B4
		public IAuthzAuthorization AuthzAuthorization
		{
			get
			{
				IAuthzAuthorization result;
				if ((result = this.authzAuthorization) == null)
				{
					result = (this.authzAuthorization = this.CreateAuthzAuthorization());
				}
				return result;
			}
		}

		// Token: 0x1700106B RID: 4203
		// (get) Token: 0x06003705 RID: 14085 RVA: 0x000E20DA File Offset: 0x000E02DA
		public IAgentRuntime AgentRuntime
		{
			get
			{
				return this.agentRuntime;
			}
		}

		// Token: 0x1700106C RID: 4204
		// (get) Token: 0x06003706 RID: 14086 RVA: 0x000E20E2 File Offset: 0x000E02E2
		public ICategorizer Categorizer
		{
			get
			{
				return this.categorizer;
			}
		}

		// Token: 0x1700106D RID: 4205
		// (get) Token: 0x06003707 RID: 14087 RVA: 0x000E20EA File Offset: 0x000E02EA
		public ICertificateCache CertificateCache
		{
			get
			{
				return this.certificateCache;
			}
		}

		// Token: 0x1700106E RID: 4206
		// (get) Token: 0x06003708 RID: 14088 RVA: 0x000E20F2 File Offset: 0x000E02F2
		public ICertificateValidator CertificateValidator
		{
			get
			{
				return this.certificateValidator;
			}
		}

		// Token: 0x1700106F RID: 4207
		// (get) Token: 0x06003709 RID: 14089 RVA: 0x000E20FC File Offset: 0x000E02FC
		public IDirectTrust DirectTrust
		{
			get
			{
				IDirectTrust result;
				if ((result = this.directTrust) == null)
				{
					result = (this.directTrust = this.CreateDirectTrust());
				}
				return result;
			}
		}

		// Token: 0x17001070 RID: 4208
		// (get) Token: 0x0600370A RID: 14090 RVA: 0x000E2124 File Offset: 0x000E0324
		public IExEventLog EventLog
		{
			get
			{
				IExEventLog result;
				if ((result = this.eventLog) == null)
				{
					result = (this.eventLog = this.CreateEventLog());
				}
				return result;
			}
		}

		// Token: 0x17001071 RID: 4209
		// (get) Token: 0x0600370B RID: 14091 RVA: 0x000E214A File Offset: 0x000E034A
		public IMessageThrottlingManager MessageThrottlingManager
		{
			get
			{
				return this.messageThrottlingManager;
			}
		}

		// Token: 0x17001072 RID: 4210
		// (get) Token: 0x0600370C RID: 14092 RVA: 0x000E2154 File Offset: 0x000E0354
		public IPConnectionTable InboundTlsIPConnectionTable
		{
			get
			{
				IPConnectionTable result;
				if ((result = this.connectionTable) == null)
				{
					result = (this.connectionTable = this.CreateConnectionTable());
				}
				return result;
			}
		}

		// Token: 0x17001073 RID: 4211
		// (get) Token: 0x0600370D RID: 14093 RVA: 0x000E217A File Offset: 0x000E037A
		public IProtocolLog ProtocolLog
		{
			get
			{
				return this.protocolLog;
			}
		}

		// Token: 0x17001074 RID: 4212
		// (get) Token: 0x0600370E RID: 14094 RVA: 0x000E2182 File Offset: 0x000E0382
		public IQueueQuotaComponent QueueQuotaComponent
		{
			get
			{
				return this.queueQuotaComponent;
			}
		}

		// Token: 0x17001075 RID: 4213
		// (get) Token: 0x0600370F RID: 14095 RVA: 0x000E218C File Offset: 0x000E038C
		public IEventNotificationItem EventNotificationItem
		{
			get
			{
				IEventNotificationItem result;
				if ((result = this.eventNotificationItem) == null)
				{
					result = (this.eventNotificationItem = this.CreateEventNotificationItem());
				}
				return result;
			}
		}

		// Token: 0x17001076 RID: 4214
		// (get) Token: 0x06003710 RID: 14096 RVA: 0x000E21B2 File Offset: 0x000E03B2
		public bool IsDataRedactionNecessary
		{
			get
			{
				return this.GetIsDataRedactionNecessary();
			}
		}

		// Token: 0x17001077 RID: 4215
		// (get) Token: 0x06003711 RID: 14097 RVA: 0x000E21BA File Offset: 0x000E03BA
		public IIsMemberOfResolver<RoutingAddress> IsMemberOfResolver
		{
			get
			{
				return this.isMemberOfResolver;
			}
		}

		// Token: 0x17001078 RID: 4216
		// (get) Token: 0x06003712 RID: 14098 RVA: 0x000E21C4 File Offset: 0x000E03C4
		public ISmtpMessageContextBlob MessageContextBlob
		{
			get
			{
				ISmtpMessageContextBlob result;
				if ((result = this.messageContextBlob) == null)
				{
					result = (this.messageContextBlob = this.CreateMessageContextBlob());
				}
				return result;
			}
		}

		// Token: 0x17001079 RID: 4217
		// (get) Token: 0x06003713 RID: 14099 RVA: 0x000E21EC File Offset: 0x000E03EC
		public ITracer Tracer
		{
			get
			{
				ITracer result;
				if ((result = this.tracer) == null)
				{
					result = (this.tracer = this.CreateTracer());
				}
				return result;
			}
		}

		// Token: 0x1700107A RID: 4218
		// (get) Token: 0x06003714 RID: 14100 RVA: 0x000E2212 File Offset: 0x000E0412
		public IShadowRedundancyManager ShadowRedundancyManager
		{
			get
			{
				return this.shadowRedundancyManager;
			}
		}

		// Token: 0x1700107B RID: 4219
		// (get) Token: 0x06003715 RID: 14101 RVA: 0x000E221C File Offset: 0x000E041C
		public ISmtpReceiveConfiguration SmtpConfiguration
		{
			get
			{
				ISmtpReceiveConfiguration result;
				if ((result = this.smtpReceiveConfiguration) == null)
				{
					result = (this.smtpReceiveConfiguration = this.CreateSmtpConfiguration());
				}
				return result;
			}
		}

		// Token: 0x1700107C RID: 4220
		// (get) Token: 0x06003716 RID: 14102 RVA: 0x000E2242 File Offset: 0x000E0442
		public ISmtpInMailItemStorage MailItemStorage
		{
			get
			{
				return this.mailItemStorage;
			}
		}

		// Token: 0x1700107D RID: 4221
		// (get) Token: 0x06003717 RID: 14103 RVA: 0x000E224A File Offset: 0x000E044A
		// (set) Token: 0x06003718 RID: 14104 RVA: 0x000E2252 File Offset: 0x000E0452
		public ServiceState ServiceState { get; set; }

		// Token: 0x06003719 RID: 14105 RVA: 0x000E2290 File Offset: 0x000E0490
		public void SetRejectState(bool rejectCommands, bool rejectMailSubmission, bool rejectMailFromInternet, SmtpResponse rejectionResponse)
		{
			ArgumentValidator.ThrowIfInvalidValue<SmtpResponse>("rejectionResponse", rejectionResponse, (SmtpResponse response) => (!rejectCommands && !rejectMailSubmission && !rejectMailFromInternet) || !rejectionResponse.IsEmpty);
			this.RejectCommands = rejectCommands;
			this.RejectSubmits = rejectMailSubmission;
			this.RejectMailFromInternet = rejectMailFromInternet;
			this.RejectionSmtpResponse = rejectionResponse;
		}

		// Token: 0x1700107E RID: 4222
		// (get) Token: 0x0600371A RID: 14106 RVA: 0x000E230C File Offset: 0x000E050C
		// (set) Token: 0x0600371B RID: 14107 RVA: 0x000E2314 File Offset: 0x000E0514
		public bool RejectCommands { get; private set; }

		// Token: 0x1700107F RID: 4223
		// (get) Token: 0x0600371C RID: 14108 RVA: 0x000E231D File Offset: 0x000E051D
		// (set) Token: 0x0600371D RID: 14109 RVA: 0x000E2325 File Offset: 0x000E0525
		public bool RejectSubmits { get; private set; }

		// Token: 0x17001080 RID: 4224
		// (get) Token: 0x0600371E RID: 14110 RVA: 0x000E232E File Offset: 0x000E052E
		// (set) Token: 0x0600371F RID: 14111 RVA: 0x000E2336 File Offset: 0x000E0536
		public bool RejectMailFromInternet { get; private set; }

		// Token: 0x17001081 RID: 4225
		// (get) Token: 0x06003720 RID: 14112 RVA: 0x000E233F File Offset: 0x000E053F
		// (set) Token: 0x06003721 RID: 14113 RVA: 0x000E2347 File Offset: 0x000E0547
		public SmtpResponse RejectionSmtpResponse { get; private set; }

		// Token: 0x17001082 RID: 4226
		// (get) Token: 0x06003722 RID: 14114 RVA: 0x000E2350 File Offset: 0x000E0550
		// (set) Token: 0x06003723 RID: 14115 RVA: 0x000E2358 File Offset: 0x000E0558
		public DateTime CurrentTime { get; set; }

		// Token: 0x06003724 RID: 14116 RVA: 0x000E2361 File Offset: 0x000E0561
		public void SetThrottleState(TimeSpan perMessageDelay, string diagnosticContext)
		{
			this.ThrottleDelay = perMessageDelay;
			this.ThrottleDelayContext = diagnosticContext;
		}

		// Token: 0x17001083 RID: 4227
		// (get) Token: 0x06003725 RID: 14117 RVA: 0x000E2371 File Offset: 0x000E0571
		// (set) Token: 0x06003726 RID: 14118 RVA: 0x000E2379 File Offset: 0x000E0579
		public TimeSpan ThrottleDelay { get; private set; }

		// Token: 0x17001084 RID: 4228
		// (get) Token: 0x06003727 RID: 14119 RVA: 0x000E2382 File Offset: 0x000E0582
		// (set) Token: 0x06003728 RID: 14120 RVA: 0x000E238A File Offset: 0x000E058A
		public string ThrottleDelayContext { get; private set; }

		// Token: 0x06003729 RID: 14121 RVA: 0x000E2393 File Offset: 0x000E0593
		protected virtual IAuthzAuthorization CreateAuthzAuthorization()
		{
			return new AuthzAuthorizationWrapper();
		}

		// Token: 0x0600372A RID: 14122 RVA: 0x000E239A File Offset: 0x000E059A
		protected virtual ICertificateCache CreateCertificateCache()
		{
			return Components.CertificateComponent.Cache;
		}

		// Token: 0x0600372B RID: 14123 RVA: 0x000E23A6 File Offset: 0x000E05A6
		protected virtual IDirectTrust CreateDirectTrust()
		{
			return new DirectTrustWrapper();
		}

		// Token: 0x0600372C RID: 14124 RVA: 0x000E23AD File Offset: 0x000E05AD
		protected virtual IExEventLog CreateEventLog()
		{
			return new ExEventLogWrapper(new ExEventLog(ExTraceGlobals.SmtpReceiveTracer.Category, TransportEventLog.GetEventSource()));
		}

		// Token: 0x0600372D RID: 14125 RVA: 0x000E23C8 File Offset: 0x000E05C8
		protected virtual IPConnectionTable CreateConnectionTable()
		{
			return new IPConnectionTable(this.maxTlsConnectionsPerMinute);
		}

		// Token: 0x0600372E RID: 14126 RVA: 0x000E23D5 File Offset: 0x000E05D5
		protected virtual IEventNotificationItem CreateEventNotificationItem()
		{
			return new EventNotificationItemWrapper();
		}

		// Token: 0x0600372F RID: 14127 RVA: 0x000E23DC File Offset: 0x000E05DC
		protected virtual bool GetIsDataRedactionNecessary()
		{
			return Util.IsDataRedactionNecessary();
		}

		// Token: 0x06003730 RID: 14128 RVA: 0x000E23E3 File Offset: 0x000E05E3
		protected virtual ISmtpMessageContextBlob CreateMessageContextBlob()
		{
			return new SmtpMessageContextBlobWrapper();
		}

		// Token: 0x06003731 RID: 14129 RVA: 0x000E23EA File Offset: 0x000E05EA
		protected virtual ITracer CreateTracer()
		{
			return ExTraceGlobals.SmtpReceiveTracer;
		}

		// Token: 0x06003732 RID: 14130 RVA: 0x000E23F1 File Offset: 0x000E05F1
		protected virtual ISmtpReceiveConfiguration CreateSmtpConfiguration()
		{
			return SmtpReceiveConfiguration.Create(this.transportAppConfig, this.transportConfiguration);
		}

		// Token: 0x06003733 RID: 14131 RVA: 0x000E2404 File Offset: 0x000E0604
		protected virtual ISmtpInMailItemStorage CreateMailItemStorage()
		{
			return new SmtpInMailItemStorage();
		}

		// Token: 0x06003734 RID: 14132 RVA: 0x000E240B File Offset: 0x000E060B
		private IReadOnlyList<IPAddress> DetermineLocalIPAddresses()
		{
			return Util.DetermineLocalIPAddresses(this.EventLog);
		}

		// Token: 0x04001C1F RID: 7199
		private readonly Lazy<IReadOnlyList<IPAddress>> localIPAddresses;

		// Token: 0x04001C20 RID: 7200
		private int maxTlsConnectionsPerMinute;

		// Token: 0x04001C21 RID: 7201
		private IAgentRuntime agentRuntime;

		// Token: 0x04001C22 RID: 7202
		private ICategorizer categorizer;

		// Token: 0x04001C23 RID: 7203
		private ICertificateCache certificateCache;

		// Token: 0x04001C24 RID: 7204
		private ICertificateValidator certificateValidator;

		// Token: 0x04001C25 RID: 7205
		private IDirectTrust directTrust;

		// Token: 0x04001C26 RID: 7206
		private IMessageThrottlingManager messageThrottlingManager;

		// Token: 0x04001C27 RID: 7207
		private IPConnectionTable connectionTable;

		// Token: 0x04001C28 RID: 7208
		private IExEventLog eventLog;

		// Token: 0x04001C29 RID: 7209
		private ITracer tracer;

		// Token: 0x04001C2A RID: 7210
		private IAuthzAuthorization authzAuthorization;

		// Token: 0x04001C2B RID: 7211
		private ISmtpMessageContextBlob messageContextBlob;

		// Token: 0x04001C2C RID: 7212
		private IIsMemberOfResolver<RoutingAddress> isMemberOfResolver;

		// Token: 0x04001C2D RID: 7213
		private IQueueQuotaComponent queueQuotaComponent;

		// Token: 0x04001C2E RID: 7214
		private IEventNotificationItem eventNotificationItem;

		// Token: 0x04001C2F RID: 7215
		private IShadowRedundancyManager shadowRedundancyManager;

		// Token: 0x04001C30 RID: 7216
		private ISmtpInMailItemStorage mailItemStorage;

		// Token: 0x04001C31 RID: 7217
		private ISmtpReceiveConfiguration smtpReceiveConfiguration;

		// Token: 0x04001C32 RID: 7218
		private IProtocolLog protocolLog;

		// Token: 0x04001C33 RID: 7219
		private ITransportAppConfig transportAppConfig;

		// Token: 0x04001C34 RID: 7220
		private ITransportConfiguration transportConfiguration;
	}
}
