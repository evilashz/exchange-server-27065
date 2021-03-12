using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.QueueViewer;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.Protocols.Smtp;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.Transport.RemoteDelivery;

namespace Microsoft.Exchange.Transport.ShadowRedundancy
{
	// Token: 0x02000368 RID: 872
	internal interface IShadowRedundancyManager : IShadowRedundancyManagerFacade
	{
		// Token: 0x17000B5D RID: 2909
		// (get) Token: 0x060025B3 RID: 9651
		Guid DatabaseId { get; }

		// Token: 0x17000B5E RID: 2910
		// (get) Token: 0x060025B4 RID: 9652
		string DatabaseIdForTransmit { get; }

		// Token: 0x17000B5F RID: 2911
		// (get) Token: 0x060025B5 RID: 9653
		IShadowRedundancyConfigurationSource Configuration { get; }

		// Token: 0x17000B60 RID: 2912
		// (get) Token: 0x060025B6 RID: 9654
		object SyncQueues { get; }

		// Token: 0x17000B61 RID: 2913
		// (get) Token: 0x060025B7 RID: 9655
		IPrimaryServerInfoMap PrimaryServerInfos { get; }

		// Token: 0x17000B62 RID: 2914
		// (get) Token: 0x060025B8 RID: 9656
		ShadowRedundancyEventLogger EventLogger { get; }

		// Token: 0x060025B9 RID: 9657
		void Start(bool initiallyPaused, ServiceState targetRunningState);

		// Token: 0x060025BA RID: 9658
		void Stop();

		// Token: 0x060025BB RID: 9659
		void Pause();

		// Token: 0x060025BC RID: 9660
		void Continue();

		// Token: 0x060025BD RID: 9661
		TransportMailItem MoveUndeliveredRecipientsToNewClone(TransportMailItem mailItem);

		// Token: 0x060025BE RID: 9662
		void AddDiagnosticInfoTo(XElement componentElement, bool showBasic, bool showVerbose);

		// Token: 0x060025BF RID: 9663
		bool IsVersion(ShadowRedundancyCompatibilityVersion version);

		// Token: 0x060025C0 RID: 9664
		string GetShadowContextForInboundSession();

		// Token: 0x060025C1 RID: 9665
		string GetShadowContext(IEhloOptions ehloOptions, NextHopSolutionKey key);

		// Token: 0x060025C2 RID: 9666
		bool ShouldShadowMailItem(IReadOnlyMailItem mailItem);

		// Token: 0x060025C3 RID: 9667
		IShadowSession GetShadowSession(ISmtpInSession inSession, bool isBdat);

		// Token: 0x060025C4 RID: 9668
		void SetSmtpInEhloOptions(EhloOptions ehloOptions, ReceiveConnector receiveConnector);

		// Token: 0x060025C5 RID: 9669
		void SetDelayedAckCompletedHandler(DelayedAckItemHandler delayedAckCompleted);

		// Token: 0x060025C6 RID: 9670
		void ApplyDiscardEvents(string serverFqdn, NextHopSolutionKey solutionKey, ICollection<string> messageIds, out int invalid, out int notFound);

		// Token: 0x060025C7 RID: 9671
		string[] QueryDiscardEvents(string queryingServerContext, int maxDiscardEvents);

		// Token: 0x060025C8 RID: 9672
		void NotifyBootLoaderDone();

		// Token: 0x060025C9 RID: 9673
		void NotifyShuttingDown();

		// Token: 0x060025CA RID: 9674
		void NotifyMailItemBifurcated(string shadowServerContext, string shadowServerDiscardId);

		// Token: 0x060025CB RID: 9675
		void NotifyMailItemPreEnqueuing(IReadOnlyMailItem mailItem, TransportMessageQueue queue);

		// Token: 0x060025CC RID: 9676
		void NotifyMailItemDeferred(IReadOnlyMailItem mailItem, TransportMessageQueue queue, DateTime deferUntil);

		// Token: 0x060025CD RID: 9677
		void LinkSideEffectMailItem(IReadOnlyMailItem originalMailItem, TransportMailItem sideEffectMailItem);

		// Token: 0x060025CE RID: 9678
		void NotifyMailItemDelivered(TransportMailItem mailItem);

		// Token: 0x060025CF RID: 9679
		void NotifyMailItemPoison(TransportMailItem mailItem);

		// Token: 0x060025D0 RID: 9680
		void NotifyMailItemReleased(TransportMailItem mailItem);

		// Token: 0x060025D1 RID: 9681
		void NotifyPrimaryServerState(string serverFqdn, string state, ShadowRedundancyCompatibilityVersion version);

		// Token: 0x060025D2 RID: 9682
		void NotifyServerViolatedSmtpContract(string serverFqdnOrContext);

		// Token: 0x060025D3 RID: 9683
		void EnqueueDelayedAckMessage(Guid shadowMessageId, object state, DateTime startTime, TimeSpan maxDelayDuration);

		// Token: 0x060025D4 RID: 9684
		bool ShouldDelayAck();

		// Token: 0x060025D5 RID: 9685
		bool ShouldSmtpOutSendXQDiscard(string serverFqdn);

		// Token: 0x060025D6 RID: 9686
		bool ShouldSmtpOutSendXShadow(Permission sessionPermissions, DeliveryType nextHopDeliveryType, IEhloOptions advertisedEhloOptions, SmtpSendConnectorConfig connector);

		// Token: 0x060025D7 RID: 9687
		bool EnqueueShadowMailItem(TransportMailItem mailItem, NextHopSolution originalMessageSolution, string primaryServer, bool shadowedMailItem, AckStatus ackStatus);

		// Token: 0x060025D8 RID: 9688
		bool EnqueuePeerShadowMailItem(TransportMailItem mailItem, string primaryServer);

		// Token: 0x060025D9 RID: 9689
		void EnqueueDiscardPendingMailItem(TransportMailItem mailItem);

		// Token: 0x060025DA RID: 9690
		void UpdateQueues();

		// Token: 0x060025DB RID: 9691
		ShadowMessageQueue GetQueue(NextHopSolutionKey key);

		// Token: 0x060025DC RID: 9692
		ShadowMessageQueue[] GetQueueArray();

		// Token: 0x060025DD RID: 9693
		TransportMailItem GetMailItem(long mailItemId);

		// Token: 0x060025DE RID: 9694
		void ProcessMailItemOnStartup(TransportMailItem mailItem);

		// Token: 0x060025DF RID: 9695
		void VisitMailItems(Func<TransportMailItem, bool> visitor);

		// Token: 0x060025E0 RID: 9696
		List<ShadowMessageQueue> FindByQueueIdentity(QueueIdentity queueIdentity);

		// Token: 0x060025E1 RID: 9697
		void LoadQueue(RoutedQueueBase queueStorage);

		// Token: 0x060025E2 RID: 9698
		void EvaluateHeartbeatAttempt(NextHopSolutionKey key, out bool sendHeartbeat, out bool abortHeartbeat);

		// Token: 0x060025E3 RID: 9699
		void NotifyHeartbeatConfigChanged(NextHopSolutionKey key, out bool abortHeartbeat);

		// Token: 0x060025E4 RID: 9700
		void NotifyHeartbeatRetry(NextHopSolutionKey key, out bool abortHeartbeat);

		// Token: 0x060025E5 RID: 9701
		void NotifyHeartbeatStatus(string serverFqdn, NextHopSolutionKey solutionKey, bool heartbeatSucceeded);

		// Token: 0x060025E6 RID: 9702
		IEnumerable<PrimaryServerInfo> GetPrimaryServersForMessageResubmission();
	}
}
