using System;
using System.Net.Sockets;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.IsMemberOfProvider;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Transport;
using Microsoft.Exchange.Transport.Categorizer;
using Microsoft.Exchange.Transport.MessageThrottling;
using Microsoft.Exchange.Transport.ShadowRedundancy;
using Microsoft.Exchange.Transport.Storage.Messaging;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x020004B4 RID: 1204
	internal interface ISmtpInComponent : IStartableTransportComponent, ITransportComponent
	{
		// Token: 0x1700102F RID: 4143
		// (set) Token: 0x06003656 RID: 13910
		bool SelfListening { set; }

		// Token: 0x17001030 RID: 4144
		// (get) Token: 0x06003657 RID: 13911
		ServiceState TargetRunningState { get; }

		// Token: 0x06003658 RID: 13912
		void UpdateTime(DateTime time);

		// Token: 0x06003659 RID: 13913
		void FlushProtocolLog();

		// Token: 0x0600365A RID: 13914
		void RejectCommands();

		// Token: 0x0600365B RID: 13915
		void RejectSubmits();

		// Token: 0x0600365C RID: 13916
		bool HandleConnection(Socket connection);

		// Token: 0x0600365D RID: 13917
		void SetLoadTimeDependencies(TransportAppConfig appConfig, ITransportConfiguration transportConfig);

		// Token: 0x0600365E RID: 13918
		void SetRunTimeDependencies(IAgentRuntime agentRuntime, IMailRouter mailRouter, IProxyHubSelector proxyHubSelector, IEnhancedDns enhancedDns, ICategorizer categorizer, ICertificateCache certificateCache, ICertificateValidator certificateValidator, IIsMemberOfResolver<RoutingAddress> memberOfResolver, IMessagingDatabase messagingDatabase, IMessageThrottlingManager messageThrottlingManager, IShadowRedundancyManager shadowRedundancyManager, SmtpOutConnectionHandler smtpOutConnectionHandler, IQueueQuotaComponent queueQuotaComponent);

		// Token: 0x0600365F RID: 13919
		void Pause(bool rejectSubmits, SmtpResponse reasonForPause);

		// Token: 0x06003660 RID: 13920
		void SetThrottleDelay(TimeSpan throttleDelay, string throttleDelayContext);
	}
}
