using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml.Linq;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.QueueViewer;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Protocols.Smtp;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.Threading;
using Microsoft.Exchange.Transport.Common;
using Microsoft.Exchange.Transport.Logging.MessageTracking;
using Microsoft.Exchange.Transport.RemoteDelivery;
using Microsoft.Exchange.Transport.Storage;
using Microsoft.Exchange.Transport.Storage.Messaging;

namespace Microsoft.Exchange.Transport.ShadowRedundancy
{
	// Token: 0x02000383 RID: 899
	internal class ShadowRedundancyManager : IShadowRedundancyManager, IShadowRedundancyManagerFacade
	{
		// Token: 0x06002724 RID: 10020 RVA: 0x00096CDC File Offset: 0x00094EDC
		public ShadowRedundancyManager(IShadowRedundancyConfigurationSource configurationSource, IShadowRedundancyPerformanceCounters perfCounters, ShadowRedundancyEventLogger shadowRedundancyEventLogger, IMessagingDatabase database)
		{
			ExTraceGlobals.ShadowRedundancyTracer.TraceDebug(0L, "Constructor");
			if (configurationSource == null)
			{
				throw new ArgumentNullException("configurationSource");
			}
			if (perfCounters == null)
			{
				throw new ArgumentNullException("perfCounter");
			}
			if (shadowRedundancyEventLogger == null)
			{
				throw new ArgumentNullException("shadowRedundancyEventLogger");
			}
			if (database == null)
			{
				throw new ArgumentNullException("database");
			}
			this.serverContextStringPool = new StringPool(StringComparer.OrdinalIgnoreCase);
			this.shadowRedundancyEventLogger = shadowRedundancyEventLogger;
			this.configurationSource = configurationSource;
			this.database = database;
			this.primaryServerInfoMap = new PrimaryServerInfoMap(shadowRedundancyEventLogger, this.database.ServerInfoTable, true);
			configurationSource.Load();
			configurationSource.SetShadowRedundancyConfigChangeNotification(new ShadowRedundancyConfigChange(this.NotifyConfigUpdated));
			ShadowRedundancyManager.shadowRedundancyPerformanceCounters = perfCounters;
			ShadowRedundancyManager.PrimaryServerInfoScanner primaryServerInfoScanner = new ShadowRedundancyManager.PrimaryServerInfoScanner(this.database);
			primaryServerInfoScanner.Load(this.primaryServerInfoMap);
			DateTime utcNow = DateTime.UtcNow;
			IEnumerable<PrimaryServerInfo> enumerable = null;
			lock (this.syncQueues)
			{
				enumerable = this.primaryServerInfoMap.RemoveExpiredServers(utcNow);
			}
			if (enumerable != null)
			{
				PrimaryServerInfo.CommitLazy(enumerable, this.database.ServerInfoTable);
			}
			if (this.primaryServerInfoMap.Count > 0)
			{
				PrimaryServerInfo active = this.primaryServerInfoMap.GetActive("$localhost$");
				if (active == null)
				{
					throw new InvalidOperationException("ShadowRedundancyManager: The current server database state Guid is not saved in database.");
				}
				if (!GuidHelper.TryParseGuid(active.DatabaseState, out this.databaseId))
				{
					throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "ShadowRedundancyManager: The current server database state '{0}' is not a valid Guid.", new object[]
					{
						active.DatabaseState
					}));
				}
				this.primaryServerInfoMap.Remove(active);
				ExTraceGlobals.ShadowRedundancyTracer.TraceDebug<string>(0L, "Database Id (as loaded from database) = '{0}'.", active.DatabaseState);
			}
			else
			{
				this.databaseId = Guid.NewGuid();
				PrimaryServerInfo primaryServerInfo = new PrimaryServerInfo(this.database.ServerInfoTable);
				primaryServerInfo.ServerFqdn = "$localhost$";
				primaryServerInfo.DatabaseState = this.databaseId.ToString();
				primaryServerInfo.StartTime = utcNow;
				primaryServerInfo.Version = this.Configuration.CompatibilityVersion;
				primaryServerInfo.Commit(TransactionCommitMode.MediumLatencyLazy);
				ExTraceGlobals.ShadowRedundancyTracer.TraceDebug<string>(0L, "DatabaseId (just created) = '{0}'.", primaryServerInfo.DatabaseState);
			}
			this.DatabaseIdForTransmit = Convert.ToBase64String(this.databaseId.ToByteArray());
			this.shadowSessionFactory = new ShadowSessionFactory(this);
			this.delayedAckCleanupTimer = new GuardedTimer(new TimerCallback(this.DelayedAckCleanupCallback), null, (long)this.configurationSource.DelayedAckCheckExpiryInterval.TotalMilliseconds, (long)this.configurationSource.DelayedAckCheckExpiryInterval.TotalMilliseconds);
		}

		// Token: 0x17000BD4 RID: 3028
		// (get) Token: 0x06002725 RID: 10021 RVA: 0x00097054 File Offset: 0x00095254
		public static IShadowRedundancyPerformanceCounters PerfCounters
		{
			get
			{
				return ShadowRedundancyManager.shadowRedundancyPerformanceCounters;
			}
		}

		// Token: 0x17000BD5 RID: 3029
		// (get) Token: 0x06002726 RID: 10022 RVA: 0x0009705B File Offset: 0x0009525B
		public Guid DatabaseId
		{
			get
			{
				return this.databaseId;
			}
		}

		// Token: 0x17000BD6 RID: 3030
		// (get) Token: 0x06002727 RID: 10023 RVA: 0x00097063 File Offset: 0x00095263
		// (set) Token: 0x06002728 RID: 10024 RVA: 0x0009706B File Offset: 0x0009526B
		public string DatabaseIdForTransmit { get; private set; }

		// Token: 0x17000BD7 RID: 3031
		// (get) Token: 0x06002729 RID: 10025 RVA: 0x00097074 File Offset: 0x00095274
		public IShadowRedundancyConfigurationSource Configuration
		{
			get
			{
				return this.configurationSource;
			}
		}

		// Token: 0x17000BD8 RID: 3032
		// (get) Token: 0x0600272A RID: 10026 RVA: 0x0009707C File Offset: 0x0009527C
		public object SyncQueues
		{
			get
			{
				return this.syncQueues;
			}
		}

		// Token: 0x17000BD9 RID: 3033
		// (get) Token: 0x0600272B RID: 10027 RVA: 0x00097084 File Offset: 0x00095284
		public IPrimaryServerInfoMap PrimaryServerInfos
		{
			get
			{
				return this.primaryServerInfoMap;
			}
		}

		// Token: 0x17000BDA RID: 3034
		// (get) Token: 0x0600272C RID: 10028 RVA: 0x0009708C File Offset: 0x0009528C
		public ShadowRedundancyEventLogger EventLogger
		{
			get
			{
				return this.shadowRedundancyEventLogger;
			}
		}

		// Token: 0x17000BDB RID: 3035
		// (get) Token: 0x0600272D RID: 10029 RVA: 0x00097094 File Offset: 0x00095294
		private bool HeartbeatEnabled
		{
			get
			{
				return this.bootLoaderComplete && this.heartbeatEnabled && this.discardEventsToApplyAfterBootLoader == null;
			}
		}

		// Token: 0x0600272E RID: 10030 RVA: 0x000970B1 File Offset: 0x000952B1
		public static Guid GetShadowNextHopConnectorId(NextHopSolutionKey key)
		{
			return key.NextHopConnector;
		}

		// Token: 0x0600272F RID: 10031 RVA: 0x000970BA File Offset: 0x000952BA
		public static bool IsShadowableDeliveryType(DeliveryType deliveryType)
		{
			return deliveryType == DeliveryType.DnsConnectorDelivery || deliveryType == DeliveryType.SmartHostConnectorDelivery || deliveryType == DeliveryType.SmtpRelayToDag || deliveryType == DeliveryType.SmtpRelayToMailboxDeliveryGroup || deliveryType == DeliveryType.SmtpRelayToConnectorSourceServers || deliveryType == DeliveryType.SmtpRelayToServers || deliveryType == DeliveryType.SmtpRelayToRemoteAdSite || deliveryType == DeliveryType.SmtpRelayWithinAdSite || deliveryType == DeliveryType.SmtpRelayWithinAdSiteToEdge || deliveryType == DeliveryType.Heartbeat;
		}

		// Token: 0x06002730 RID: 10032 RVA: 0x000970EC File Offset: 0x000952EC
		public static void PrepareRecipientForShadowing(MailRecipient mailRecipient, string serverFqdn)
		{
			if (mailRecipient == null)
			{
				throw new ArgumentNullException("mailRecipient");
			}
			if (string.IsNullOrEmpty(serverFqdn))
			{
				throw new ArgumentNullException("serverFqdn");
			}
			NextHopSolutionKey nextHopSolutionKey = NextHopSolutionKey.CreateShadowNextHopSolutionKey(serverFqdn, Guid.Empty, null);
			mailRecipient.PrimaryServerFqdnGuid = new ShadowRedundancyManager.QualifiedPrimaryServerId(nextHopSolutionKey).ToString();
			mailRecipient.NextHop = nextHopSolutionKey;
		}

		// Token: 0x06002731 RID: 10033 RVA: 0x00097148 File Offset: 0x00095348
		public void Start(bool initiallyPaused, ServiceState targetRunningState)
		{
			if (initiallyPaused)
			{
				this.paused = true;
			}
			this.targetRunningState = targetRunningState;
			if (targetRunningState != ServiceState.Inactive)
			{
				this.heartbeatEnabled = true;
				ExTraceGlobals.ShadowRedundancyTracer.TraceDebug(0L, "Heartbeats enabled");
				return;
			}
			ExTraceGlobals.ShadowRedundancyTracer.TraceDebug(0L, "Heartbeats not enabled as the service state is inactive");
		}

		// Token: 0x06002732 RID: 10034 RVA: 0x00097188 File Offset: 0x00095388
		public void Stop()
		{
			this.heartbeatEnabled = false;
			ExTraceGlobals.ShadowRedundancyTracer.TraceDebug(0L, "Heartbeats disabled");
		}

		// Token: 0x06002733 RID: 10035 RVA: 0x000971A2 File Offset: 0x000953A2
		public void Pause()
		{
			this.paused = true;
		}

		// Token: 0x06002734 RID: 10036 RVA: 0x000971AB File Offset: 0x000953AB
		public void Continue()
		{
			this.paused = false;
		}

		// Token: 0x06002735 RID: 10037 RVA: 0x000971C4 File Offset: 0x000953C4
		public TransportMailItem MoveUndeliveredRecipientsToNewClone(TransportMailItem mailItem)
		{
			if (mailItem == null)
			{
				throw new ArgumentNullException("mailItem");
			}
			if (mailItem.NextHopSolutions.Count > 0)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Mail Item '{0}' with shadow message id '{1}' received with '{2}' next hop solutions.  This method is designed to be called from Boot Loader where there should be no NextHopSolutions", new object[]
				{
					mailItem.RecordId,
					mailItem.ShadowMessageId,
					mailItem.NextHopSolutions.Count
				}));
			}
			List<MailRecipient> list = (from r in mailItem.Recipients
			where string.IsNullOrEmpty(r.PrimaryServerFqdnGuid)
			select r).ToList<MailRecipient>();
			if (list.Count == 0)
			{
				return null;
			}
			TransportMailItem transportMailItem = mailItem.NewCloneWithoutRecipients();
			foreach (MailRecipient mailRecipient in list)
			{
				mailRecipient.MoveTo(transportMailItem);
			}
			transportMailItem.CommitLazy();
			return transportMailItem;
		}

		// Token: 0x06002736 RID: 10038 RVA: 0x000972C4 File Offset: 0x000954C4
		public void AddDiagnosticInfoTo(XElement componentElement, bool showBasic, bool showVerbose)
		{
			showBasic = (showBasic || showVerbose);
			componentElement.Add(new XElement("Configuration", new object[]
			{
				new XElement("delayedAckCheckExpiryInterval", this.configurationSource.DelayedAckCheckExpiryInterval),
				new XElement("delayedAckSkippingEnabled", this.configurationSource.DelayedAckSkippingEnabled),
				new XElement("delayedAckSkippingQueueLength", this.configurationSource.DelayedAckSkippingQueueLength),
				new XElement("discardEventExpireInterval", this.configurationSource.DiscardEventExpireInterval),
				new XElement("discardEventsCheckExpiryInterval", this.configurationSource.DiscardEventsCheckExpiryInterval),
				new XElement("enabled", this.configurationSource.Enabled),
				new XElement("compatibilityMode", this.configurationSource.CompatibilityVersion),
				new XElement("shadowMessagePreference", this.configurationSource.ShadowMessagePreference),
				new XElement("maxRemoteShadowAttempts", this.configurationSource.MaxRemoteShadowAttempts),
				new XElement("maxLocalShadowAttempts", this.configurationSource.MaxLocalShadowAttempts),
				new XElement("rejectMessageOnShadowFailure", this.configurationSource.RejectMessageOnShadowFailure),
				new XElement("heartbeatRetryCount", this.configurationSource.HeartbeatRetryCount),
				new XElement("heartbeatFrequency", this.configurationSource.HeartbeatFrequency),
				new XElement("primaryServerInfoCleanupInterval", this.configurationSource.PrimaryServerInfoCleanupInterval),
				new XElement("queueMaxIdleTimeInterval", this.configurationSource.QueueMaxIdleTimeInterval),
				new XElement("shadowMessageAutoDiscardInterval", this.configurationSource.ShadowMessageAutoDiscardInterval),
				new XElement("shadowQueueCheckExpiryInterval", this.configurationSource.ShadowQueueCheckExpiryInterval),
				new XElement("shadowServerInfoMaxIdleTimeInterval", this.configurationSource.ShadowServerInfoMaxIdleTimeInterval),
				new XElement("stringPoolCleanupInterval", this.configurationSource.StringPoolCleanupInterval)
			}));
			if (showBasic)
			{
				Dictionary<ShadowRedundancyManager.QualifiedPrimaryServerId, List<Guid>> dictionary = this.discardEventsToApplyAfterBootLoader;
				componentElement.Add(new object[]
				{
					new XElement("databaseStateId", this.databaseId),
					new XElement("bootLoaderComplete", this.bootLoaderComplete),
					new XElement("heartbeatEnabled", this.heartbeatEnabled),
					new XElement("shuttingDown", this.shuttingDown),
					new XElement("activeResubmits", this.activeResubmits),
					new XElement("lastPrimaryServerInfoMapCleanup", this.lastPrimaryServerInfoMapCleanup),
					new XElement("lastDelayedCleanupCompleteTime", this.lastDelayedCleanupCompleteTime),
					new XElement("primaryServerInfoMapCleanupGuard", this.primaryServerInfoMapCleanupGuard),
					new XElement("discardEventsToApplyAfterBootLoader", (dictionary == null) ? 0 : dictionary.Count),
					new XElement("queuedRecoveryResubmits", this.queuedRecoveryResubmits.Count)
				});
				lock (this.delayedAckEntries)
				{
					XElement xelement = new XElement("DelayedAckEntryCollection", new XElement("count", this.delayedAckEntries.Count));
					if (showVerbose)
					{
						using (Dictionary<Guid, ShadowRedundancyManager.DelayedAckEntry>.Enumerator enumerator = this.delayedAckEntries.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								KeyValuePair<Guid, ShadowRedundancyManager.DelayedAckEntry> keyValuePair = enumerator.Current;
								xelement.Add(new XElement("DelayedAck", new object[]
								{
									new XElement("shadowMessageId", keyValuePair.Key),
									new XElement("context", keyValuePair.Value.Context),
									new XElement("startTime", keyValuePair.Value.StartTime),
									new XElement("completionStatus", keyValuePair.Value.CompletionStatus),
									new XElement("state", keyValuePair.Value.State)
								}));
							}
							goto IL_57E;
						}
					}
					xelement.Add(new XElement("help", "Use 'verbose' argument to get the delayed ack entries."));
					IL_57E:
					componentElement.Add(xelement);
				}
				this.shadowServerInfoMapReaderWriterLock.EnterReadLock();
				try
				{
					XElement xelement2 = new XElement("ShadowServerCollection", new XElement("count", this.shadowServerInfoMap.Count));
					if (showVerbose || showBasic)
					{
						using (Dictionary<string, ShadowRedundancyManager.ShadowServerInfo>.Enumerator enumerator2 = this.shadowServerInfoMap.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								KeyValuePair<string, ShadowRedundancyManager.ShadowServerInfo> keyValuePair2 = enumerator2.Current;
								xelement2.Add(new XElement("ShadowServer", new object[]
								{
									new XElement("context", keyValuePair2.Key),
									keyValuePair2.Value.GetDiagnosticInfo(showVerbose)
								}));
							}
							goto IL_668;
						}
					}
					xelement2.Add(new XElement("help", "Use 'verbose' argument to get the delayed ack entries."));
					IL_668:
					componentElement.Add(xelement2);
				}
				finally
				{
					this.shadowServerInfoMapReaderWriterLock.ExitReadLock();
				}
				lock (this.syncAllMailItems)
				{
					XElement xelement3 = new XElement("ShadowMailItemsCollection", new XElement("count", this.allMailItemsByRecordId.Count));
					if (showVerbose)
					{
						using (Dictionary<long, TransportMailItem>.Enumerator enumerator3 = this.allMailItemsByRecordId.GetEnumerator())
						{
							while (enumerator3.MoveNext())
							{
								KeyValuePair<long, TransportMailItem> keyValuePair3 = enumerator3.Current;
								lock (keyValuePair3.Value)
								{
									xelement3.Add(new XElement("ShadowedTransportMailItem", new object[]
									{
										new XElement("internalId", keyValuePair3.Key),
										new XElement("internalMessageId", keyValuePair3.Value.InternetMessageId),
										new XElement("shadowMessageId", keyValuePair3.Value.ShadowMessageId),
										new XElement("nextHopSolutionsCount", (keyValuePair3.Value.NextHopSolutions == null) ? "null" : keyValuePair3.Value.NextHopSolutions.Count.ToString(CultureInfo.InvariantCulture)),
										new XElement("heloDomain", keyValuePair3.Value.HeloDomain),
										new XElement("dateReceived", keyValuePair3.Value.DateReceived),
										new XElement("status", keyValuePair3.Value.Status)
									}));
								}
							}
							goto IL_86A;
						}
					}
					xelement3.Add(new XElement("help", "Use 'verbose' argument to get the shadow mail item entries."));
					IL_86A:
					componentElement.Add(xelement3);
				}
				lock (this.completedResubmitEntries)
				{
					XElement xelement4 = new XElement("CompletedServerResubmitEvents", new XElement("count", this.completedResubmitEntries.Count));
					if (showVerbose)
					{
						using (Queue<ShadowRedundancyManager.CompletedResubmitEntry>.Enumerator enumerator4 = this.completedResubmitEntries.GetEnumerator())
						{
							while (enumerator4.MoveNext())
							{
								ShadowRedundancyManager.CompletedResubmitEntry completedResubmitEntry = enumerator4.Current;
								xelement4.Add(new XElement("ResubmitEvent", new object[]
								{
									new XElement("timestamp", completedResubmitEntry.Timestamp),
									new XElement("primaryServerFqdn", completedResubmitEntry.PrimaryServerFqdn),
									new XElement("reason", completedResubmitEntry.Reason),
									new XElement("messageCount", completedResubmitEntry.MessageCount)
								}));
							}
							goto IL_9B2;
						}
					}
					xelement4.Add(new XElement("help", "Use 'verbose' argument to get the completed server resubmit events."));
					IL_9B2:
					componentElement.Add(xelement4);
				}
				ShadowMessageQueue[] queueArray = this.GetQueueArray();
				XElement xelement5 = new XElement("ShadowQueues", new XElement("count", queueArray.Length));
				foreach (ShadowMessageQueue shadowMessageQueue in queueArray)
				{
					xelement5.Add(new XElement("ShadowQueue", new object[]
					{
						new XElement("Id", shadowMessageQueue.Id),
						new XElement("Key", shadowMessageQueue.Key.ToString()),
						new XElement("NextHopDomain", shadowMessageQueue.NextHopDomain),
						new XElement("IsEmpty", shadowMessageQueue.IsEmpty),
						new XElement("Suspended", shadowMessageQueue.Suspended),
						new XElement("HasHeartbeatFailure", shadowMessageQueue.HasHeartbeatFailure),
						new XElement("IsResubmissionSuppressed", shadowMessageQueue.IsResubmissionSuppressed),
						new XElement("Count", shadowMessageQueue.Count),
						new XElement("LastHeartbeatTime", shadowMessageQueue.LastHeartbeatTime),
						new XElement("LastExpiryCheck", shadowMessageQueue.LastExpiryCheck),
						new XElement("ValidDiscardIdCount", shadowMessageQueue.ValidDiscardIdCount),
						new XElement("IgnoredDiscardIdCount", shadowMessageQueue.IgnoredDiscardIdCount)
					}));
				}
				componentElement.Add(xelement5);
			}
		}

		// Token: 0x06002737 RID: 10039 RVA: 0x00097F70 File Offset: 0x00096170
		public bool IsVersion(ShadowRedundancyCompatibilityVersion version)
		{
			return this.configurationSource.CompatibilityVersion == version;
		}

		// Token: 0x06002738 RID: 10040 RVA: 0x00097F80 File Offset: 0x00096180
		public string GetShadowContextForInboundSession()
		{
			ExTraceGlobals.ShadowRedundancyTracer.TraceDebug(0L, "Getting shadow context for SmtpInSession");
			if (!this.IsVersion(ShadowRedundancyCompatibilityVersion.E15))
			{
				throw new InvalidOperationException("GetShadowContextForInboundSession called under non-E15 compatibility level");
			}
			return ShadowRedundancyManager.EncodeShadowContext(ShadowRedundancyManager.GetE15ShadowContext());
		}

		// Token: 0x06002739 RID: 10041 RVA: 0x00097FB4 File Offset: 0x000961B4
		public string GetShadowContext(IEhloOptions ehloOptions, NextHopSolutionKey key)
		{
			ExTraceGlobals.ShadowRedundancyTracer.TraceDebug<ShadowRedundancyCompatibilityVersion, bool, NextHopSolutionKey>(0L, "Getting shadow context for SmtpOutSession, compatibility version = {0}, XShadowRequest = {1}, key = {2}", this.configurationSource.CompatibilityVersion, ehloOptions.XShadowRequest, key);
			if (ehloOptions == null)
			{
				throw new ArgumentNullException("ehloOptions");
			}
			string shadowContext;
			if (this.IsVersion(ShadowRedundancyCompatibilityVersion.E14) || !ehloOptions.XShadowRequest)
			{
				Guid shadowNextHopConnectorId = ShadowRedundancyManager.GetShadowNextHopConnectorId(key);
				shadowContext = string.Format(CultureInfo.InvariantCulture, "{0}@{1}@{2}", new object[]
				{
					shadowNextHopConnectorId,
					SmtpReceiveServer.ServerName,
					key.NextHopTlsDomain
				});
			}
			else
			{
				shadowContext = ShadowRedundancyManager.GetE15ShadowContext();
			}
			return ShadowRedundancyManager.EncodeShadowContext(shadowContext);
		}

		// Token: 0x0600273A RID: 10042 RVA: 0x0009804B File Offset: 0x0009624B
		public bool ShouldShadowMailItem(IReadOnlyMailItem mailItem)
		{
			if (mailItem == null)
			{
				throw new ArgumentNullException("mailItem");
			}
			return !mailItem.IsHeartbeat && this.configurationSource.Enabled && !mailItem.IsPfReplica();
		}

		// Token: 0x0600273B RID: 10043 RVA: 0x0009807B File Offset: 0x0009627B
		public IShadowSession GetShadowSession(ISmtpInSession inSession, bool isBdat)
		{
			if (inSession == null)
			{
				throw new ArgumentNullException("inSession");
			}
			if (!inSession.IsShadowedBySender && !inSession.IsPeerShadowSession)
			{
				return this.shadowSessionFactory.GetShadowSession(inSession, isBdat);
			}
			return new NullShadowSession();
		}

		// Token: 0x0600273C RID: 10044 RVA: 0x000980B0 File Offset: 0x000962B0
		public void SetSmtpInEhloOptions(EhloOptions ehloOptions, ReceiveConnector receiveConnector)
		{
			ExTraceGlobals.ShadowRedundancyTracer.TraceDebug(0L, "Setting shadow redundancy EhloOptions");
			if (ehloOptions == null)
			{
				throw new ArgumentNullException("ehloOptions");
			}
			if (receiveConnector == null)
			{
				throw new ArgumentNullException("receiveConnector");
			}
			ExTraceGlobals.ShadowRedundancyTracer.TraceDebug<bool, ShadowRedundancyCompatibilityVersion, AuthMechanisms>(0L, "Enabled:{0} CompatibilityVersion:{1}, AuthMechanism:{2}", this.Configuration.Enabled, this.Configuration.CompatibilityVersion, receiveConnector.AuthMechanism);
			ehloOptions.XShadow = (this.Configuration.Enabled && this.Configuration.CompatibilityVersion == ShadowRedundancyCompatibilityVersion.E14 && (receiveConnector.AuthMechanism & (AuthMechanisms.ExchangeServer | AuthMechanisms.ExternalAuthoritative)) != AuthMechanisms.None);
			ehloOptions.XShadowRequest = (this.Configuration.Enabled && this.Configuration.CompatibilityVersion == ShadowRedundancyCompatibilityVersion.E15 && (receiveConnector.AuthMechanism & (AuthMechanisms.ExchangeServer | AuthMechanisms.ExternalAuthoritative)) != AuthMechanisms.None);
		}

		// Token: 0x0600273D RID: 10045 RVA: 0x0009817C File Offset: 0x0009637C
		public void SetDelayedAckCompletedHandler(DelayedAckItemHandler delayedAckCompleted)
		{
			if (delayedAckCompleted == null)
			{
				throw new ArgumentNullException("delayedAckCompleted");
			}
			if (this.delayedAckCompleted != null)
			{
				throw new InvalidOperationException("delayedAckCompleted delegate already set.");
			}
			this.delayedAckCompleted = delayedAckCompleted;
		}

		// Token: 0x0600273E RID: 10046 RVA: 0x000981A8 File Offset: 0x000963A8
		public void ApplyDiscardEvents(string serverFqdn, NextHopSolutionKey solutionKey, ICollection<string> messageIds, out int invalid, out int notFound)
		{
			if (string.IsNullOrEmpty(serverFqdn))
			{
				throw new ArgumentNullException("serverFqdn");
			}
			if (messageIds == null)
			{
				throw new ArgumentNullException("messageIds");
			}
			ExTraceGlobals.ShadowRedundancyTracer.TraceError<int, string>(0L, "ApplyDiscardEvents(): Applying {0} events for primary server '{1}'", messageIds.Count, serverFqdn);
			invalid = 0;
			notFound = 0;
			if (!this.configurationSource.Enabled)
			{
				return;
			}
			Guid shadowNextHopConnectorId = ShadowRedundancyManager.GetShadowNextHopConnectorId(solutionKey);
			ShadowRedundancyManager.QualifiedPrimaryServerId serverId = new ShadowRedundancyManager.QualifiedPrimaryServerId(serverFqdn, shadowNextHopConnectorId, solutionKey.NextHopTlsDomain);
			if (messageIds.Count == 0)
			{
				return;
			}
			List<Guid> list = this.ConvertMessageIdsFromStringToGuid(messageIds);
			invalid = messageIds.Count - list.Count;
			if (invalid > 0)
			{
				ExTraceGlobals.ShadowRedundancyTracer.TraceError<string, int>(0L, "ApplyDiscardEvents(): Primary server '{0}' provided '{1}' invalid discard ids.", serverFqdn, invalid);
			}
			this.ApplyDiscardEvents(serverId, list, out notFound);
		}

		// Token: 0x0600273F RID: 10047 RVA: 0x00098264 File Offset: 0x00096464
		public string[] QueryDiscardEvents(string queryingServerContext, int maxDiscardEvents)
		{
			if (string.IsNullOrEmpty(queryingServerContext))
			{
				throw new ArgumentNullException("queryingServerContext");
			}
			if (maxDiscardEvents < 1)
			{
				throw new ArgumentOutOfRangeException(string.Format(CultureInfo.InvariantCulture, "maxDiscardEvents must be 1 or more.  Value Received ='{0}'.", new object[]
				{
					maxDiscardEvents
				}));
			}
			if (!this.configurationSource.Enabled)
			{
				ExTraceGlobals.ShadowRedundancyTracer.TraceDebug<string>(0L, "QueryDiscardEvents(serverContext='{0}') - shadowing disabled.", queryingServerContext);
				return new string[0];
			}
			ExTraceGlobals.ShadowRedundancyTracer.TraceDebug<string, int>(0L, "QueryDiscardEvents(context='{0}', maxDiscardEvents='{1}')", queryingServerContext, maxDiscardEvents);
			ShadowRedundancyManager.ShadowServerInfo shadowServerInfo = this.GetShadowServerInfo(queryingServerContext, false);
			if (shadowServerInfo == null)
			{
				ExTraceGlobals.ShadowRedundancyTracer.TraceDebug<string>(0L, "QueryDiscardEvents(serverContext='{0}') - shadow server info not found.", queryingServerContext);
				return new string[0];
			}
			string[] array = shadowServerInfo.DequeueDiscardEvents(maxDiscardEvents);
			ExTraceGlobals.ShadowRedundancyTracer.TraceDebug<string, int, int>(0L, "QueryDiscardEvents(server='{0}', maxDiscardEvents='{1}') - Found {2} discard events.", queryingServerContext, maxDiscardEvents, array.Length);
			return array;
		}

		// Token: 0x06002740 RID: 10048 RVA: 0x0009832C File Offset: 0x0009652C
		public void NotifyBootLoaderDone()
		{
			if (this.bootLoaderComplete)
			{
				throw new InvalidOperationException("NotifyBootLoaderDone() called more than once.");
			}
			ExTraceGlobals.ShadowRedundancyTracer.TraceDebug(0L, "NotifyBootLoaderDone: Boot loader done.");
			this.bootLoaderComplete = true;
			this.hashsetToDetectShadowGroupsOnStartup = null;
			lock (this.timerCreateDestroySync)
			{
				if (!this.shuttingDown)
				{
					this.discardsCleanupTimer = new GuardedTimer(new TimerCallback(this.DiscardsCleanupCallback), null, TimeSpan.Zero, this.configurationSource.DiscardEventsCheckExpiryInterval);
					this.stringPoolCleanupTimer = new GuardedTimer(new TimerCallback(this.StringPoolExpirationCallback), null, this.configurationSource.StringPoolCleanupInterval, this.configurationSource.StringPoolCleanupInterval);
				}
			}
			this.ProcessPendingDiscardEvents();
			this.ProcessPendingResubmits();
		}

		// Token: 0x06002741 RID: 10049 RVA: 0x00098404 File Offset: 0x00096604
		public void NotifyShuttingDown()
		{
			if (this.shuttingDown)
			{
				throw new InvalidOperationException("NotifyShuttingDown() called more than once.");
			}
			lock (this.timerCreateDestroySync)
			{
				this.shuttingDown = true;
				if (this.stringPoolCleanupTimer != null)
				{
					this.stringPoolCleanupTimer.Dispose(true);
				}
				if (this.discardsCleanupTimer != null)
				{
					this.discardsCleanupTimer.Dispose(true);
				}
				if (this.delayedAckCleanupTimer != null)
				{
					this.delayedAckCleanupTimer.Dispose(true);
				}
			}
			this.resubmitEvent.WaitOne();
		}

		// Token: 0x06002742 RID: 10050 RVA: 0x000984A0 File Offset: 0x000966A0
		public void NotifyMailItemBifurcated(string shadowServerContext, string shadowServerDiscardId)
		{
			ShadowRedundancyManager.QualifiedShadowServerDiscardId qualifiedDiscardId = new ShadowRedundancyManager.QualifiedShadowServerDiscardId(this.serverContextStringPool.Intern(shadowServerContext), shadowServerDiscardId);
			this.NotifyMailItemCreatedInShadowGroup(qualifiedDiscardId);
		}

		// Token: 0x06002743 RID: 10051 RVA: 0x000984C8 File Offset: 0x000966C8
		public void NotifyMailItemPreEnqueuing(IReadOnlyMailItem mailItem, TransportMessageQueue queue)
		{
			if (!this.ValidCandidateForDelayedAckSkipping(mailItem, queue))
			{
				return;
			}
			SubmitMessageQueue submitMessageQueue;
			RoutedMessageQueue routedMessageQueue;
			UnreachableMessageQueue unreachableMessageQueue;
			string queueId = this.GetQueueId(queue, out submitMessageQueue, out routedMessageQueue, out unreachableMessageQueue);
			if (queueId == null)
			{
				throw new ArgumentException(string.Format("Unexpected queue type '{0}'.", queue.GetType()));
			}
			ExTraceGlobals.ShadowRedundancyTracer.TraceDebug<long, string>((long)this.GetHashCode(), "Mail item '{0}' queued in '{1}'.", mailItem.RecordId, queueId);
			string text = null;
			if (unreachableMessageQueue != null)
			{
				text = "Unreachable";
			}
			if (text == null && queue.Suspended)
			{
				text = "QueueIsSuspended";
			}
			if (text == null && routedMessageQueue != null && this.IsQueueInRetry(routedMessageQueue))
			{
				text = "QueueInRetry";
			}
			if (text == null && submitMessageQueue == null)
			{
				int activeCount = queue.ActiveCount;
				if (activeCount >= this.configurationSource.DelayedAckSkippingQueueLength)
				{
					text = string.Format(CultureInfo.InvariantCulture, "QueueLength={0}>={1}", new object[]
					{
						activeCount,
						this.configurationSource.DelayedAckSkippingQueueLength
					});
				}
			}
			if (text != null)
			{
				this.SkipDelayedAckForMessage(mailItem, text, queueId);
			}
		}

		// Token: 0x06002744 RID: 10052 RVA: 0x000985C4 File Offset: 0x000967C4
		public void NotifyMailItemDeferred(IReadOnlyMailItem mailItem, TransportMessageQueue queue, DateTime deferUntil)
		{
			if (!this.ValidCandidateForDelayedAckSkipping(mailItem, queue))
			{
				return;
			}
			SubmitMessageQueue submitMessageQueue;
			RoutedMessageQueue routedMessageQueue;
			UnreachableMessageQueue unreachableMessageQueue;
			string queueId = this.GetQueueId(queue, out submitMessageQueue, out routedMessageQueue, out unreachableMessageQueue);
			if (queueId == null)
			{
				throw new ArgumentException(string.Format("Unexpected queue type '{0}'.", queue.GetType()));
			}
			if (submitMessageQueue != null && deferUntil <= DateTime.UtcNow)
			{
				return;
			}
			ExTraceGlobals.ShadowRedundancyTracer.TraceDebug<long, string>((long)this.GetHashCode(), "Mail item '{0}' deferred in '{1}'.", mailItem.RecordId, queueId);
			this.SkipDelayedAckForMessage(mailItem, "ItemDeferred=" + deferUntil.ToString("yyyy-MM-ddTHH\\:mm\\:ss.fffZ", DateTimeFormatInfo.InvariantInfo), queueId);
		}

		// Token: 0x06002745 RID: 10053 RVA: 0x00098654 File Offset: 0x00096854
		public void LinkSideEffectMailItem(IReadOnlyMailItem originalMailItem, TransportMailItem sideEffectMailItem)
		{
			if (originalMailItem == null)
			{
				throw new ArgumentNullException("originalMailItem");
			}
			if (sideEffectMailItem == null)
			{
				throw new ArgumentNullException("sideEffectMailItem");
			}
			if (!originalMailItem.IsShadowed())
			{
				throw new ArgumentException("Attempt to link to a non-shadowed message", "originalMailItem");
			}
			sideEffectMailItem.ShadowServerDiscardId = originalMailItem.ShadowServerDiscardId;
			sideEffectMailItem.ShadowServerContext = originalMailItem.ShadowServerContext;
			ShadowRedundancyManager.QualifiedShadowServerDiscardId qualifiedDiscardId = new ShadowRedundancyManager.QualifiedShadowServerDiscardId(originalMailItem.ShadowServerContext, originalMailItem.ShadowServerDiscardId);
			ExTraceGlobals.ShadowRedundancyTracer.TraceDebug<long, long>((long)this.GetHashCode(), "Linking side effect message {0} to shadow for original message {1}", sideEffectMailItem.RecordId, originalMailItem.RecordId);
			this.NotifyMailItemCreatedInShadowGroup(qualifiedDiscardId);
		}

		// Token: 0x06002746 RID: 10054 RVA: 0x000986E9 File Offset: 0x000968E9
		public void NotifyMailItemDelivered(TransportMailItem mailItem)
		{
			if (mailItem == null)
			{
				throw new ArgumentNullException("mailItem");
			}
			ExTraceGlobals.ShadowRedundancyTracer.TraceDebug<long>(0L, "NotifyMailItemDelivered() called for mail item with record id '{0}'.", mailItem.RecordId);
			this.NotifyMailItemInstanceHandled(mailItem);
		}

		// Token: 0x06002747 RID: 10055 RVA: 0x00098717 File Offset: 0x00096917
		public void NotifyMailItemPoison(TransportMailItem mailItem)
		{
			if (mailItem == null)
			{
				throw new ArgumentNullException("mailItem");
			}
			ExTraceGlobals.ShadowRedundancyTracer.TraceDebug<long>(0L, "NotifyMailItemPoison() called for mail item with record id '{0}'.", mailItem.RecordId);
			this.NotifyMailItemInstanceHandled(mailItem);
		}

		// Token: 0x06002748 RID: 10056 RVA: 0x00098745 File Offset: 0x00096945
		public void NotifyMailItemReleased(TransportMailItem mailItem)
		{
			if (mailItem == null)
			{
				throw new ArgumentNullException("mailItem");
			}
			ExTraceGlobals.ShadowRedundancyTracer.TraceDebug<long>(0L, "NotifyMailItemReleased() called for mail item with record id '{0}'.", mailItem.RecordId);
			this.NotifyMailItemInstanceHandled(mailItem);
		}

		// Token: 0x06002749 RID: 10057 RVA: 0x00098774 File Offset: 0x00096974
		public void NotifyPrimaryServerState(string serverFqdn, string state, ShadowRedundancyCompatibilityVersion version)
		{
			if (string.IsNullOrEmpty(serverFqdn))
			{
				throw new ArgumentNullException("serverFqdn");
			}
			if (string.IsNullOrEmpty(state))
			{
				state = "0de1e7ed-0de1-0de1-0de1-de1e7edele7e";
			}
			PrimaryServerInfo primaryServerInfo = this.primaryServerInfoMap.UpdateServerState(serverFqdn, state, version);
			if (!primaryServerInfo.IsActive)
			{
				bool flag = false;
				IEnumerable<ShadowMessageQueue> queues = this.GetQueues(serverFqdn);
				foreach (ShadowMessageQueue shadowMessageQueue in queues)
				{
					if (!shadowMessageQueue.IsEmpty)
					{
						flag = true;
						break;
					}
				}
				if (flag)
				{
					lock (this.queuedRecoveryResubmits)
					{
						ExTraceGlobals.ShadowRedundancyTracer.TraceDebug<string>(0L, "NotifyPrimaryServerState(): Queuing a resubmit for server '{0}'.", serverFqdn);
						this.queuedRecoveryResubmits.Add(serverFqdn);
						return;
					}
				}
				ExTraceGlobals.ShadowRedundancyTracer.TraceDebug<string>(0L, "NotifyPrimaryServerState(): There are no shadow messages for server '{0}', hence, no resubmit queued.", serverFqdn);
			}
		}

		// Token: 0x0600274A RID: 10058 RVA: 0x0009886C File Offset: 0x00096A6C
		public void NotifyServerViolatedSmtpContract(string serverFqdnOrContext)
		{
			if (string.IsNullOrEmpty(serverFqdnOrContext))
			{
				throw new ArgumentNullException("serverFqdnOrContext");
			}
			ExTraceGlobals.ShadowRedundancyTracer.TraceError<string>(0L, "NotifyServerViolatedSmtpContract() Server with Fqdn/Context '{0}' violdated Smtp protocol and hence state dumped.", serverFqdnOrContext);
			this.DiscardShadowResourcesForServer(serverFqdnOrContext);
		}

		// Token: 0x0600274B RID: 10059 RVA: 0x0009889C File Offset: 0x00096A9C
		public void EnqueueDelayedAckMessage(Guid shadowMessageId, object state, DateTime startTime, TimeSpan maxDelayDuration)
		{
			if (!this.IsVersion(ShadowRedundancyCompatibilityVersion.E14))
			{
				throw new InvalidOperationException("EnqueueDelayedAckMessage can only be called in E14 mode.");
			}
			if (shadowMessageId == Guid.Empty)
			{
				throw new ArgumentException("shadowMessageId must not be Guid.Empty", "shadowMessageId");
			}
			if (this.delayedAckCompleted == null)
			{
				throw new InvalidOperationException("EnqueueDelayedAckMessage() is called while delayedAckCompleted is null.");
			}
			ShadowRedundancyManager.DelayedAckEntry delayedAckEntry = new ShadowRedundancyManager.DelayedAckEntry(state, startTime, startTime.Add(maxDelayDuration));
			ExTraceGlobals.ShadowRedundancyTracer.TraceDebug<ShadowRedundancyManager.DelayedAckEntry>(0L, "EnqueueDelayedAckMessage(): {0}", delayedAckEntry);
			lock (this.delayedAckEntries)
			{
				TransportHelpers.AttemptAddToDictionary<Guid, ShadowRedundancyManager.DelayedAckEntry>(this.delayedAckEntries, shadowMessageId, delayedAckEntry, null);
				if (!this.configurationSource.Enabled)
				{
					delayedAckEntry.MarkAsExpired("ShadowRedundancyDisabled");
				}
			}
		}

		// Token: 0x0600274C RID: 10060 RVA: 0x00098964 File Offset: 0x00096B64
		public bool ShouldDelayAck()
		{
			return this.Configuration.Enabled && this.Configuration.CompatibilityVersion == ShadowRedundancyCompatibilityVersion.E14;
		}

		// Token: 0x0600274D RID: 10061 RVA: 0x00098984 File Offset: 0x00096B84
		public bool ShouldSmtpOutSendXQDiscard(string serverFqdn)
		{
			if (string.IsNullOrEmpty(serverFqdn))
			{
				throw new ArgumentNullException("serverFqdn");
			}
			lock (this.syncQueues)
			{
				List<ShadowMessageQueue> list;
				if (this.serverFqdnToShadowQueueMap.TryGetValue(serverFqdn, out list))
				{
					foreach (ShadowMessageQueue shadowMessageQueue in list)
					{
						if (!shadowMessageQueue.IsEmpty)
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x0600274E RID: 10062 RVA: 0x00098A2C File Offset: 0x00096C2C
		public bool ShouldSmtpOutSendXShadow(Permission sessionPermissions, DeliveryType nextHopDeliveryType, IEhloOptions advertisedEhloOptions, SmtpSendConnectorConfig connector)
		{
			if ((sessionPermissions & Permission.SMTPSendXShadow) == Permission.None)
			{
				ExTraceGlobals.ShadowRedundancyTracer.TraceDebug(0L, "ShouldSmtpOutSendXShadow returning false because of missing SMTPSendXShadow permission");
				return false;
			}
			if (connector != null && connector.FrontendProxyEnabled)
			{
				ExTraceGlobals.ShadowRedundancyTracer.TraceDebug(0L, "ShouldSmtpOutSendXShadow returning false because send connector will proxy through FE");
				return false;
			}
			if (this.IsVersion(ShadowRedundancyCompatibilityVersion.E15) && nextHopDeliveryType == DeliveryType.Heartbeat && (advertisedEhloOptions.XShadow || advertisedEhloOptions.XShadowRequest))
			{
				ExTraceGlobals.ShadowRedundancyTracer.TraceDebug(0L, "ShouldSmtpOutSendXShadow returning true for heartbeat");
				return true;
			}
			return advertisedEhloOptions.XShadow && ShadowRedundancyManager.IsShadowableDeliveryType(nextHopDeliveryType);
		}

		// Token: 0x0600274F RID: 10063 RVA: 0x00098AB8 File Offset: 0x00096CB8
		public bool EnqueueShadowMailItem(TransportMailItem mailItem, NextHopSolution originalMessageSolution, string primaryServer, bool shadowedMailItem, AckStatus ackStatus)
		{
			if (mailItem == null)
			{
				throw new ArgumentNullException("mailItem");
			}
			if (string.IsNullOrEmpty(primaryServer))
			{
				return false;
			}
			bool flag = ShadowRedundancyManager.ShouldShadowAckStatus(ackStatus);
			bool flag2 = this.configurationSource.Enabled && shadowedMailItem && originalMessageSolution.DeliveryStatus == DeliveryStatus.Complete && flag && this.ShouldShadowMailItem(mailItem);
			ExTraceGlobals.ShadowRedundancyTracer.TraceDebug(0L, "EnqueueShadowMailItem(E14) params: shadowAckStatus={0} createShadow={1} primaryServer={2} shadowedMailItem={3} ackStatus={4} NextHopSolutionKey={5} DeliveryStatus={6}", new object[]
			{
				flag,
				flag2,
				primaryServer,
				shadowedMailItem,
				ackStatus,
				originalMessageSolution.NextHopSolutionKey,
				originalMessageSolution.DeliveryStatus
			});
			ExTraceGlobals.ShadowRedundancyTracer.TraceDebug(0L, "EnqueueShadowMailItem(E14) mailitem: IsHeartbeat={0} IsPoison={1} IsActive={2} InternetMessageId={3} ShadowMessageId={4} ShadowServerContext={5} ShadowServerDiscardId={6}", new object[]
			{
				mailItem.IsHeartbeat,
				mailItem.IsPoison,
				mailItem.IsActive,
				mailItem.InternetMessageId,
				mailItem.ShadowMessageId,
				mailItem.ShadowServerContext,
				mailItem.ShadowServerDiscardId
			});
			if (flag2)
			{
				if (!ShadowRedundancyManager.IsShadowableDeliveryType(originalMessageSolution.NextHopSolutionKey.NextHopType.DeliveryType))
				{
					throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "This kind of next hop solution should not be shadowed.  Delivery Type : '{0}'.", new object[]
					{
						originalMessageSolution.NextHopSolutionKey.NextHopType.DeliveryType
					}));
				}
				ShadowMailItem shadowMailItem = this.CreateShadowMailItemAndReplaceSolution(mailItem, originalMessageSolution, primaryServer);
				this.EnqueueShadowMailItem(shadowMailItem);
				ExTraceGlobals.ShadowRedundancyTracer.TraceDebug<long, Guid, string>(0L, "Transport Mail Item '{0}' with ShadowMessageId '{1}' added to shadow queue '{2}'. (E14)", mailItem.RecordId, mailItem.ShadowMessageId, primaryServer);
			}
			return flag2;
		}

		// Token: 0x06002750 RID: 10064 RVA: 0x00098C78 File Offset: 0x00096E78
		public bool EnqueuePeerShadowMailItem(TransportMailItem mailItem, string primaryServer)
		{
			ExTraceGlobals.ShadowRedundancyTracer.TraceDebug(0L, "Enqueuing shadow item without next hop");
			if (mailItem == null)
			{
				throw new ArgumentNullException("mailItem");
			}
			if (string.IsNullOrEmpty(primaryServer))
			{
				throw new ArgumentNullException("primaryServer");
			}
			if (!this.IsVersion(ShadowRedundancyCompatibilityVersion.E15))
			{
				throw new InvalidOperationException("Attempt to enqueue a shadow message with compatibility == E14");
			}
			if (!this.configurationSource.Enabled)
			{
				mailItem.ShadowServerContext = null;
				mailItem.ReleaseFromActiveMaterializedLazy();
				return false;
			}
			ShadowMailItem shadowMailItem = this.CreateShadowMailItem(mailItem, primaryServer);
			this.EnqueueShadowMailItem(shadowMailItem);
			this.LogNewShadowMailItem(shadowMailItem);
			ExTraceGlobals.ShadowRedundancyTracer.TraceDebug<long, Guid, string>(0L, "Transport Mail Item '{0}' with ShadowMessageId '{1}' added to shadow queue '{2}'. (E15)", mailItem.RecordId, mailItem.ShadowMessageId, primaryServer);
			SystemProbeHelper.ShadowRedundancyTracer.TracePass<string>(mailItem, 0L, "Message added to the shadow queue {0}", primaryServer);
			mailItem.MinimizeMemory();
			return true;
		}

		// Token: 0x06002751 RID: 10065 RVA: 0x00098D38 File Offset: 0x00096F38
		public void EnqueueDiscardPendingMailItem(TransportMailItem mailItem)
		{
			ExTraceGlobals.ShadowRedundancyTracer.TraceDebug(0L, "Enqueuing discard item");
			if (mailItem == null)
			{
				throw new ArgumentNullException("mailItem");
			}
			if (string.IsNullOrEmpty(mailItem.ShadowServerContext) || string.IsNullOrEmpty(mailItem.ShadowServerDiscardId))
			{
				ExTraceGlobals.ShadowRedundancyTracer.TraceDebug(0L, "Ignoring invalid mail item");
				return;
			}
			this.EnqueueDiscardEventForShadowServer(mailItem, mailItem.GetExpiryTime(false));
		}

		// Token: 0x06002752 RID: 10066 RVA: 0x00098DA0 File Offset: 0x00096FA0
		public void UpdateQueues()
		{
			ExTraceGlobals.ShadowRedundancyTracer.TraceDebug<bool>(0L, "ShadowRedundancyManager updating queues heartbeatEnabled={0}", this.HeartbeatEnabled);
			ShadowMessageQueue[] queueArray = this.GetQueueArray();
			foreach (ShadowMessageQueue shadowMessageQueue in queueArray)
			{
				shadowMessageQueue.UpdateQueue(this.HeartbeatEnabled, this.paused);
				if (shadowMessageQueue.CanBeDeleted(this.configurationSource.QueueMaxIdleTimeInterval))
				{
					lock (this.syncQueues)
					{
						if (shadowMessageQueue.CanBeDeleted(this.configurationSource.QueueMaxIdleTimeInterval))
						{
							ShadowRedundancyManager.QualifiedPrimaryServerId key = new ShadowRedundancyManager.QualifiedPrimaryServerId(shadowMessageQueue.Key);
							List<ShadowMessageQueue> list;
							if (this.serverFqdnToShadowQueueMap.TryGetValue(key.Fqdn, out list) && list.Contains(shadowMessageQueue))
							{
								this.serverIdToShadowQueueMap.Remove(key);
								this.queueIdToShadowQueueMap.Remove(shadowMessageQueue.Id);
								if (list.Count > 1)
								{
									list.Remove(shadowMessageQueue);
								}
								else
								{
									this.serverFqdnToShadowQueueMap.Remove(key.Fqdn);
								}
								shadowMessageQueue.Delete();
							}
						}
					}
				}
			}
			if (this.bootLoaderComplete)
			{
				DateTime utcNow = DateTime.UtcNow;
				if (utcNow >= this.lastPrimaryServerInfoMapCleanup.Add(this.configurationSource.PrimaryServerInfoCleanupInterval))
				{
					try
					{
						if (Interlocked.Increment(ref this.primaryServerInfoMapCleanupGuard) == 1)
						{
							this.lastPrimaryServerInfoMapCleanup = utcNow;
							IEnumerable<PrimaryServerInfo> enumerable = null;
							lock (this.syncQueues)
							{
								enumerable = this.primaryServerInfoMap.RemoveExpiredServers(utcNow);
							}
							if (enumerable != null)
							{
								PrimaryServerInfo.CommitLazy(enumerable, this.database.ServerInfoTable);
							}
						}
					}
					finally
					{
						Interlocked.Decrement(ref this.primaryServerInfoMapCleanupGuard);
					}
				}
				this.ProcessPendingResubmits();
			}
		}

		// Token: 0x06002753 RID: 10067 RVA: 0x00098F90 File Offset: 0x00097190
		public ShadowMessageQueue GetQueue(NextHopSolutionKey key)
		{
			if (key.NextHopType != NextHopType.ShadowRedundancy && key.NextHopType != NextHopType.Heartbeat)
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "key has invalid delivery type '{0}'.", new object[]
				{
					key.NextHopType
				}));
			}
			ShadowRedundancyManager.QualifiedPrimaryServerId primaryServerId = new ShadowRedundancyManager.QualifiedPrimaryServerId(key);
			return this.GetQueue(primaryServerId);
		}

		// Token: 0x06002754 RID: 10068 RVA: 0x00099000 File Offset: 0x00097200
		public ShadowMessageQueue[] GetQueueArray()
		{
			ShadowMessageQueue[] result;
			lock (this.syncQueues)
			{
				ShadowMessageQueue[] array = new ShadowMessageQueue[this.serverIdToShadowQueueMap.Count];
				this.serverIdToShadowQueueMap.Values.CopyTo(array, 0);
				result = array;
			}
			return result;
		}

		// Token: 0x06002755 RID: 10069 RVA: 0x00099060 File Offset: 0x00097260
		public TransportMailItem GetMailItem(long mailItemId)
		{
			if (mailItemId <= 0L)
			{
				throw new ArgumentOutOfRangeException("The mailItemId is expected to be greater than 0, it is currently set to " + mailItemId);
			}
			TransportMailItem result;
			lock (this.syncAllMailItems)
			{
				this.allMailItemsByRecordId.TryGetValue(mailItemId, out result);
			}
			return result;
		}

		// Token: 0x06002756 RID: 10070 RVA: 0x000990C8 File Offset: 0x000972C8
		public void ProcessMailItemOnStartup(TransportMailItem mailItem)
		{
			if (mailItem == null)
			{
				throw new ArgumentNullException("mailItem");
			}
			if (this.bootLoaderComplete)
			{
				throw new InvalidOperationException("ProcessMailItemOnStartup() must not be called except while boot loader is active.");
			}
			lock (mailItem)
			{
				if (string.Equals(mailItem.ShadowServerContext, "$localhost$", StringComparison.OrdinalIgnoreCase))
				{
					mailItem.ShadowServerContext = null;
					mailItem.ShadowServerDiscardId = null;
				}
				if (!this.configurationSource.Enabled || mailItem.IsHeartbeat)
				{
					ShadowRedundancyManager.ClearRecipientsPrimaryServerFqdnGuid(mailItem);
					mailItem.ReleaseFromShadowRedundancy();
				}
				else
				{
					if (mailItem.IsShadowed())
					{
						ShadowRedundancyManager.QualifiedShadowServerDiscardId qualifiedShadowServerDiscardId = new ShadowRedundancyManager.QualifiedShadowServerDiscardId(this.serverContextStringPool.Intern(mailItem.ShadowServerContext), mailItem.ShadowServerDiscardId);
						if (!this.hashsetToDetectShadowGroupsOnStartup.TryAdd(qualifiedShadowServerDiscardId))
						{
							this.NotifyMailItemCreatedInShadowGroup(qualifiedShadowServerDiscardId);
						}
					}
					Dictionary<NextHopSolutionKey, NextHopSolution> dictionary = null;
					foreach (MailRecipient mailRecipient in mailItem.Recipients)
					{
						string primaryServerFqdnGuid = mailRecipient.PrimaryServerFqdnGuid;
						if (!string.IsNullOrEmpty(primaryServerFqdnGuid))
						{
							ShadowRedundancyManager.QualifiedPrimaryServerId qualifiedPrimaryServerId;
							if (!ShadowRedundancyManager.QualifiedPrimaryServerId.TryParse(primaryServerFqdnGuid, out qualifiedPrimaryServerId))
							{
								ExTraceGlobals.ShadowRedundancyTracer.TraceError(0L, "Transport Mail Item '{0}' with ShadowMessageId '{1}' not shadowed for recipient '{2}' due to failure in parsing primaryServerFqdnGuid '{3}'", new object[]
								{
									mailItem.RecordId,
									mailItem.ShadowMessageId,
									mailRecipient.Email,
									primaryServerFqdnGuid
								});
								mailRecipient.PrimaryServerFqdnGuid = null;
							}
							else
							{
								NextHopSolutionKey key = NextHopSolutionKey.CreateShadowNextHopSolutionKey(qualifiedPrimaryServerId.Fqdn, qualifiedPrimaryServerId.NextHopGuid, mailRecipient.GetTlsDomain());
								NextHopSolution nextHopSolution;
								if ((dictionary == null && !mailItem.NextHopSolutions.TryGetValue(key, out nextHopSolution)) || !dictionary.TryGetValue(key, out nextHopSolution))
								{
									if (dictionary == null)
									{
										dictionary = new Dictionary<NextHopSolutionKey, NextHopSolution>(mailItem.NextHopSolutions);
									}
									nextHopSolution = new NextHopSolution(key);
									dictionary.Add(key, nextHopSolution);
								}
								nextHopSolution.AddRecipient(mailRecipient);
								mailRecipient.NextHop = nextHopSolution.NextHopSolutionKey;
							}
						}
					}
					if (dictionary != null)
					{
						mailItem.NextHopSolutions = dictionary;
					}
					bool flag2 = false;
					foreach (KeyValuePair<NextHopSolutionKey, NextHopSolution> keyValuePair in mailItem.NextHopSolutions)
					{
						if (keyValuePair.Key.NextHopType.DeliveryType == DeliveryType.ShadowRedundancy)
						{
							this.EnqueueShadowMailItem(this.CreateShadowMailItem(mailItem, keyValuePair.Value));
							flag2 = true;
						}
					}
					if (!flag2)
					{
						mailItem.ReleaseFromShadowRedundancy();
						ExTraceGlobals.ShadowRedundancyTracer.TraceError<long, Guid>(0L, "Transport Mail Item '{0}' with ShadowMessageId '{1}' not shadowed", mailItem.RecordId, mailItem.ShadowMessageId);
					}
					else
					{
						mailItem.CommitLazyAndDehydrateMessageIfPossible(Breadcrumb.DehydrateOnAddToShadowQueue);
					}
				}
			}
		}

		// Token: 0x06002757 RID: 10071 RVA: 0x000993A4 File Offset: 0x000975A4
		public void VisitMailItems(Func<TransportMailItem, bool> visitor)
		{
			if (visitor == null)
			{
				throw new ArgumentNullException("visitor");
			}
			lock (this.syncAllMailItems)
			{
				foreach (TransportMailItem arg in this.allMailItemsByRecordId.Values)
				{
					if (!visitor(arg))
					{
						break;
					}
				}
			}
		}

		// Token: 0x06002758 RID: 10072 RVA: 0x00099438 File Offset: 0x00097638
		public List<ShadowMessageQueue> FindByQueueIdentity(QueueIdentity queueIdentity)
		{
			if (queueIdentity == null)
			{
				throw new ArgumentNullException("queueIdentity");
			}
			if (!queueIdentity.IsLocal || queueIdentity.Type != QueueType.Shadow)
			{
				return new List<ShadowMessageQueue>();
			}
			if (queueIdentity.RowId > 0L)
			{
				List<ShadowMessageQueue> list = new List<ShadowMessageQueue>();
				ShadowMessageQueue shadowMessageQueue = this.FindById(queueIdentity.RowId);
				if (shadowMessageQueue != null)
				{
					list.Add(shadowMessageQueue);
				}
				return list;
			}
			return this.GetQueues(queueIdentity.NextHopDomain);
		}

		// Token: 0x06002759 RID: 10073 RVA: 0x000994A8 File Offset: 0x000976A8
		public void LoadQueue(RoutedQueueBase queueStorage)
		{
			if (queueStorage == null)
			{
				throw new ArgumentNullException("queueStorage");
			}
			NextHopSolutionKey key = new NextHopSolutionKey(queueStorage.NextHopType, queueStorage.NextHopDomain, queueStorage.NextHopConnector, queueStorage.NextHopTlsDomain);
			ShadowRedundancyManager.QualifiedPrimaryServerId qualifiedPrimaryServerId = new ShadowRedundancyManager.QualifiedPrimaryServerId(key);
			lock (this.syncQueues)
			{
				ShadowMessageQueue queue;
				if (!this.serverIdToShadowQueueMap.TryGetValue(qualifiedPrimaryServerId, out queue))
				{
					queue = new ShadowMessageQueue(queueStorage, key, new ShadowMessageQueue.ItemExpiredHandler(this.ItemExpiryHandlerCallback), this.configurationSource, new ShouldSuppressResubmission(this.ShouldSuppressResubmission), this.shadowRedundancyEventLogger, null, null);
				}
				this.AddQueue(qualifiedPrimaryServerId, queue);
			}
		}

		// Token: 0x0600275A RID: 10074 RVA: 0x00099560 File Offset: 0x00097760
		public void EvaluateHeartbeatAttempt(NextHopSolutionKey key, out bool sendHeartbeat, out bool abortHeartbeat)
		{
			if (key.NextHopType != NextHopType.Heartbeat)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Unexpected delivery type {0}", new object[]
				{
					key.NextHopType.DeliveryType
				}));
			}
			sendHeartbeat = false;
			abortHeartbeat = false;
			ShadowMessageQueue queue = this.GetQueue(key);
			if (queue != null)
			{
				queue.EvaluateHeartbeatAttempt(out sendHeartbeat, out abortHeartbeat);
			}
			else
			{
				abortHeartbeat = true;
			}
			ExTraceGlobals.ShadowRedundancyTracer.TraceDebug<bool, bool, NextHopSolutionKey>(0L, "ShadowRedundancyManager.EvaluateHeartbeatAttempt returning send={0} abort={1} for queue={2}", sendHeartbeat, abortHeartbeat, key);
		}

		// Token: 0x0600275B RID: 10075 RVA: 0x000995E8 File Offset: 0x000977E8
		public void NotifyHeartbeatConfigChanged(NextHopSolutionKey key, out bool abortHeartbeat)
		{
			if (key.NextHopType != NextHopType.Heartbeat)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Unexpected delivery type {0}", new object[]
				{
					key.NextHopType.DeliveryType
				}));
			}
			abortHeartbeat = false;
			ShadowMessageQueue queue = this.GetQueue(key);
			if (queue != null)
			{
				ExTraceGlobals.ShadowRedundancyTracer.TraceDebug<NextHopSolutionKey>(0L, "ShadowRedundancyManager.NotifyHeartbeatConfigChanged queue={0}", key);
				queue.NotifyHeartbeatConfigChanged(key, out abortHeartbeat);
				return;
			}
			abortHeartbeat = true;
		}

		// Token: 0x0600275C RID: 10076 RVA: 0x00099668 File Offset: 0x00097868
		public void NotifyHeartbeatRetry(NextHopSolutionKey key, out bool abortHeartbeat)
		{
			if (key.NextHopType != NextHopType.Heartbeat)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Unexpected delivery type {0}", new object[]
				{
					key.NextHopType.DeliveryType
				}));
			}
			abortHeartbeat = false;
			ShadowMessageQueue queue = this.GetQueue(key);
			if (queue != null)
			{
				ExTraceGlobals.ShadowRedundancyTracer.TraceDebug<NextHopSolutionKey>(0L, "ShadowRedundancyManager.NotifyHeartbeatRetry queue={0}", key);
				queue.UpdateHeartbeat(DateTime.UtcNow, key, false);
				return;
			}
			abortHeartbeat = true;
		}

		// Token: 0x0600275D RID: 10077 RVA: 0x000996F0 File Offset: 0x000978F0
		public void NotifyHeartbeatStatus(string serverFqdn, NextHopSolutionKey solutionKey, bool heartbeatSucceeded)
		{
			Guid shadowNextHopConnectorId = ShadowRedundancyManager.GetShadowNextHopConnectorId(solutionKey);
			ShadowRedundancyManager.QualifiedPrimaryServerId qualifiedPrimaryServerId = new ShadowRedundancyManager.QualifiedPrimaryServerId(serverFqdn, shadowNextHopConnectorId, solutionKey.NextHopTlsDomain);
			this.UpdateHeartbeat(qualifiedPrimaryServerId.Fqdn, heartbeatSucceeded);
		}

		// Token: 0x0600275E RID: 10078 RVA: 0x000998C0 File Offset: 0x00097AC0
		public IEnumerable<PrimaryServerInfo> GetPrimaryServersForMessageResubmission()
		{
			foreach (PrimaryServerInfo primaryServerInfo in this.primaryServerInfoMap.GetAll())
			{
				bool isActive = primaryServerInfo.IsActive;
				yield return primaryServerInfo;
			}
			yield break;
		}

		// Token: 0x0600275F RID: 10079 RVA: 0x000998E0 File Offset: 0x00097AE0
		void IShadowRedundancyManagerFacade.LinkSideEffectMailItemIfNeeded(ITransportMailItemFacade originalMailItem, ITransportMailItemFacade sideEffectMailItem)
		{
			TransportMailItem transportMailItem = (TransportMailItem)originalMailItem;
			if (transportMailItem.IsShadowed())
			{
				this.LinkSideEffectMailItem(transportMailItem, (TransportMailItem)sideEffectMailItem);
			}
		}

		// Token: 0x06002760 RID: 10080 RVA: 0x0009990C File Offset: 0x00097B0C
		protected virtual void SkipDelayedAckForMessage(IReadOnlyMailItem mailItem, string skipDelayedAckReason, string queueId)
		{
			lock (this.delayedAckEntries)
			{
				ShadowRedundancyManager.DelayedAckEntry delayedAckEntry;
				if (this.delayedAckEntries.TryGetValue(mailItem.ShadowMessageId, out delayedAckEntry))
				{
					ExTraceGlobals.ShadowRedundancyTracer.TraceDebug<long, string, string>((long)this.GetHashCode(), "Delayed Ack for mail item '{0}' skipped due to '{1}' applying to queue '{2}'.", mailItem.RecordId, skipDelayedAckReason, queueId);
					delayedAckEntry.MarkAsSkipped(skipDelayedAckReason + ";NextHopDomain=" + queueId);
					this.FinalizeDelayedAck(mailItem.ShadowMessageId, delayedAckEntry, DateTime.UtcNow);
				}
			}
		}

		// Token: 0x06002761 RID: 10081 RVA: 0x000999A0 File Offset: 0x00097BA0
		private static string GetE15ShadowContext()
		{
			return SmtpReceiveServer.ServerName;
		}

		// Token: 0x06002762 RID: 10082 RVA: 0x000999A7 File Offset: 0x00097BA7
		private static string EncodeShadowContext(string shadowContext)
		{
			ExTraceGlobals.ShadowRedundancyTracer.TraceDebug<string>(0L, "Encoding shadow context: {0}", shadowContext);
			return Convert.ToBase64String(Encoding.Default.GetBytes(shadowContext));
		}

		// Token: 0x06002763 RID: 10083 RVA: 0x000999CC File Offset: 0x00097BCC
		private static bool ShouldShadowAckStatus(AckStatus ackStatus)
		{
			switch (ackStatus)
			{
			case AckStatus.Pending:
			case AckStatus.Retry:
			case AckStatus.Fail:
			case AckStatus.Resubmit:
			case AckStatus.Quarantine:
			case AckStatus.Skip:
				return false;
			case AckStatus.Success:
			case AckStatus.Expand:
			case AckStatus.Relay:
			case AckStatus.SuccessNoDsn:
				return true;
			default:
				throw new ArgumentOutOfRangeException(string.Format(CultureInfo.InvariantCulture, "Unknown AckStatus: {0}", new object[]
				{
					ackStatus.ToString()
				}));
			}
		}

		// Token: 0x06002764 RID: 10084 RVA: 0x00099A3C File Offset: 0x00097C3C
		private static void ClearRecipientsPrimaryServerFqdnGuid(TransportMailItem mailItem)
		{
			foreach (MailRecipient mailRecipient in mailItem.Recipients)
			{
				if (!string.IsNullOrEmpty(mailRecipient.PrimaryServerFqdnGuid))
				{
					mailRecipient.PrimaryServerFqdnGuid = null;
				}
			}
		}

		// Token: 0x06002765 RID: 10085 RVA: 0x00099A98 File Offset: 0x00097C98
		private static void MarkMailItemAsDiscardPending(TransportMailItem mailItem)
		{
			if (string.IsNullOrEmpty(mailItem.ShadowServerContext))
			{
				throw new ArgumentNullException("mailItem.ShadowServerContext");
			}
			if (string.IsNullOrEmpty(mailItem.ShadowServerDiscardId))
			{
				throw new ArgumentNullException("mailItem.ShadowServerDiscardId");
			}
			mailItem.IsDiscardPending = true;
			mailItem.CommitLazy();
		}

		// Token: 0x06002766 RID: 10086 RVA: 0x00099AD7 File Offset: 0x00097CD7
		private static void MarkMailItemAsDiscardCompleted(TransportMailItem mailItem)
		{
			mailItem.ShadowServerContext = null;
			mailItem.ShadowServerDiscardId = null;
			mailItem.IsDiscardPending = false;
			mailItem.CommitLazy();
		}

		// Token: 0x06002767 RID: 10087 RVA: 0x00099AF4 File Offset: 0x00097CF4
		private bool IsQueueInRetry(RoutedMessageQueue queue)
		{
			return queue.RetryConnectionScheduled && queue.ActiveConnections == 0;
		}

		// Token: 0x06002768 RID: 10088 RVA: 0x00099B0C File Offset: 0x00097D0C
		private void FinalizeDelayedAck(Guid shadowMessageId, ShadowRedundancyManager.DelayedAckEntry delayedAckEntry, DateTime now)
		{
			if (delayedAckEntry.CompletionStatus == null)
			{
				throw new InvalidOperationException("CompletionStatus must no be null at completion time.");
			}
			if (this.delayedAckCompleted(delayedAckEntry.State, delayedAckEntry.CompletionStatus.Value, now.Subtract(delayedAckEntry.StartTime), delayedAckEntry.Context))
			{
				ExTraceGlobals.ShadowRedundancyTracer.TraceDebug<Guid, DelayedAckCompletionStatus?>(0L, "FinalizeDelayedAck(): Completion callback notified successfully about delayed ack for message '{0}' with completion status '{1}'.", shadowMessageId, delayedAckEntry.CompletionStatus);
				if (!this.delayedAckEntries.Remove(shadowMessageId))
				{
					throw new InvalidOperationException("Expected delayedAckEntry is not found in list.");
				}
				if (delayedAckEntry.CompletionStatus.Value != DelayedAckCompletionStatus.Delivered)
				{
					ShadowRedundancyManager.PerfCounters.DelayedAckExpired(1L);
					return;
				}
			}
			else
			{
				ExTraceGlobals.ShadowRedundancyTracer.TraceDebug<Guid, DelayedAckCompletionStatus?>(0L, "FinalizeDelayedAck(): Completion callback returned false for message '{0}' with completion status '{1}'.  Will try again later.", shadowMessageId, delayedAckEntry.CompletionStatus);
			}
		}

		// Token: 0x06002769 RID: 10089 RVA: 0x00099BD0 File Offset: 0x00097DD0
		private bool ShouldSuppressResubmission(IEnumerable<INextHopServer> relatedBridgeheads)
		{
			if (this.targetRunningState != ServiceState.Active)
			{
				return true;
			}
			if (relatedBridgeheads == null)
			{
				return false;
			}
			foreach (INextHopServer nextHopServer in relatedBridgeheads)
			{
				string primaryFqdn = nextHopServer.IsIPAddress ? nextHopServer.Address.ToString() : nextHopServer.Fqdn;
				List<ShadowMessageQueue> queues = this.GetQueues(primaryFqdn);
				foreach (ShadowMessageQueue shadowMessageQueue in queues)
				{
					if (!shadowMessageQueue.IsEmpty && !shadowMessageQueue.Suspended && !shadowMessageQueue.HasHeartbeatFailure && !shadowMessageQueue.IsResubmissionSuppressed)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x0600276A RID: 10090 RVA: 0x00099CB0 File Offset: 0x00097EB0
		private ShadowMessageQueue FindById(long id)
		{
			ShadowMessageQueue result;
			lock (this.syncQueues)
			{
				ShadowMessageQueue shadowMessageQueue;
				result = (this.queueIdToShadowQueueMap.TryGetValue(id, out shadowMessageQueue) ? shadowMessageQueue : null);
			}
			return result;
		}

		// Token: 0x0600276B RID: 10091 RVA: 0x00099D00 File Offset: 0x00097F00
		private ShadowMessageQueue CreateQueue(ShadowRedundancyManager.QualifiedPrimaryServerId serverId, FindRelatedBridgeHeads findRelatedBridgeHeads, GetRoutedMessageQueueStatus getRoutedMessageQueueStatus)
		{
			ShadowMessageQueue shadowMessageQueue = ShadowMessageQueue.NewQueue(NextHopSolutionKey.CreateShadowNextHopSolutionKey(serverId.Fqdn, serverId.NextHopGuid), new ShadowMessageQueue.ItemExpiredHandler(this.ItemExpiryHandlerCallback), this.configurationSource, new ShouldSuppressResubmission(this.ShouldSuppressResubmission), this.shadowRedundancyEventLogger, findRelatedBridgeHeads, getRoutedMessageQueueStatus);
			this.AddQueue(serverId, shadowMessageQueue);
			return shadowMessageQueue;
		}

		// Token: 0x0600276C RID: 10092 RVA: 0x00099D58 File Offset: 0x00097F58
		private void ItemExpiryHandlerCallback(ShadowMessageQueue shadowMessageQueue, ShadowMailItem shadowMailItem)
		{
			bool flag = false;
			TransportMailItem transportMailItem = shadowMailItem.TransportMailItem;
			long recordId;
			Guid shadowMessageId;
			lock (transportMailItem)
			{
				recordId = transportMailItem.RecordId;
				shadowMessageId = transportMailItem.ShadowMessageId;
				ExTraceGlobals.ShadowRedundancyTracer.TraceDebug(0L, "Shadow mail item expired for Transport Mail Item '{0}' with ShadowMessageId '{1}' from queue '{2}'.  DiscardReason '{3}'.", new object[]
				{
					recordId,
					shadowMessageId,
					shadowMessageQueue.NextHopDomain,
					shadowMailItem.DiscardReason
				});
				foreach (MailRecipient mailRecipient in shadowMailItem.NextHopSolution.Recipients)
				{
					mailRecipient.PrimaryServerFqdnGuid = null;
				}
				Dictionary<NextHopSolutionKey, NextHopSolution> dictionary = new Dictionary<NextHopSolutionKey, NextHopSolution>(transportMailItem.NextHopSolutions);
				dictionary.Remove(shadowMailItem.NextHopSolution.NextHopSolutionKey);
				transportMailItem.NextHopSolutions = dictionary;
				if (!transportMailItem.IsShadow())
				{
					this.MarkShadowMailAsDelivered(shadowMailItem, shadowMessageQueue.NextHopDomain);
					transportMailItem.ReleaseFromShadowRedundancy();
					flag = true;
				}
				else
				{
					transportMailItem.CommitLazy(shadowMailItem.NextHopSolution);
				}
			}
			if (flag)
			{
				ExTraceGlobals.ShadowRedundancyTracer.TraceDebug<long, Guid>(0L, "Transport Mail Item '{0}' with ShadowMessageId '{1}' released.", recordId, shadowMessageId);
				lock (this.syncAllMailItems)
				{
					this.allMailItemsByRecordId.Remove(recordId);
				}
			}
			ShadowRedundancyManager.PerfCounters.DecrementCounter(ShadowRedundancyCounterId.AggregateShadowQueueLength);
			if (shadowMailItem.DiscardReason.Equals(DiscardReason.Expired))
			{
				ShadowRedundancyManager.PerfCounters.IncrementCounter(ShadowRedundancyCounterId.ShadowQueueAutoDiscardsTotal);
			}
		}

		// Token: 0x0600276D RID: 10093 RVA: 0x00099F10 File Offset: 0x00098110
		private ShadowMailItem CreateShadowMailItemAndReplaceSolution(TransportMailItem mailItem, NextHopSolution originalMessageSolution, string serverFqdn)
		{
			Guid shadowNextHopConnectorId = ShadowRedundancyManager.GetShadowNextHopConnectorId(originalMessageSolution.NextHopSolutionKey);
			NextHopSolutionKey nextHopSolutionKey = NextHopSolutionKey.CreateShadowNextHopSolutionKey(serverFqdn, shadowNextHopConnectorId, originalMessageSolution.NextHopSolutionKey.NextHopTlsDomain);
			ShadowMailItem result;
			lock (mailItem)
			{
				foreach (MailRecipient mailRecipient in originalMessageSolution.Recipients)
				{
					if (!mailRecipient.IsProcessed)
					{
						throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Recipients in shadowed solutions are expected to be marked as processed.  Recipient '{0}'.", new object[]
						{
							mailRecipient
						}));
					}
					if (!string.IsNullOrEmpty(mailRecipient.PrimaryServerFqdnGuid))
					{
						throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Recipients being shadowed must not already have PrimaryServerFqdnGuid set.  Recipient '{0}' has it set to '{1}'.", new object[]
						{
							mailRecipient,
							mailRecipient.PrimaryServerFqdnGuid
						}));
					}
					ShadowRedundancyManager.QualifiedPrimaryServerId qualifiedPrimaryServerId = new ShadowRedundancyManager.QualifiedPrimaryServerId(nextHopSolutionKey);
					mailRecipient.PrimaryServerFqdnGuid = qualifiedPrimaryServerId.ToString();
					mailRecipient.NextHop = nextHopSolutionKey;
				}
				Dictionary<NextHopSolutionKey, NextHopSolution> dictionary = new Dictionary<NextHopSolutionKey, NextHopSolution>(mailItem.NextHopSolutions);
				dictionary.Remove(originalMessageSolution.NextHopSolutionKey);
				mailItem.NextHopSolutions = dictionary;
				NextHopSolution shadowNextHopSolution = mailItem.UpdateNextHopSolutionTable(nextHopSolutionKey, originalMessageSolution.Recipients, true);
				result = this.CreateShadowMailItem(mailItem, shadowNextHopSolution);
			}
			return result;
		}

		// Token: 0x0600276E RID: 10094 RVA: 0x0009A090 File Offset: 0x00098290
		private void EnqueueShadowMailItem(ShadowMailItem shadowMailItem)
		{
			bool flag = false;
			ShadowMessageQueue shadowMessageQueue = null;
			ShadowRedundancyManager.QualifiedPrimaryServerId qualifiedPrimaryServerId = new ShadowRedundancyManager.QualifiedPrimaryServerId(shadowMailItem.NextHopSolution.NextHopSolutionKey);
			try
			{
				this.AcquireMailItem(shadowMailItem.TransportMailItem);
				lock (this.syncQueues)
				{
					if (!this.serverIdToShadowQueueMap.TryGetValue(qualifiedPrimaryServerId, out shadowMessageQueue))
					{
						shadowMessageQueue = this.CreateQueue(qualifiedPrimaryServerId, null, null);
					}
					shadowMessageQueue.AddReference();
					flag = true;
				}
				shadowMessageQueue.Enqueue(shadowMailItem);
				lock (shadowMailItem.TransportMailItem)
				{
					if (DateTime.UtcNow < ((IQueueItem)shadowMailItem).Expiry)
					{
						foreach (MailRecipient mailRecipient in shadowMailItem.NextHopSolution.Recipients)
						{
							mailRecipient.PrimaryServerFqdnGuid = qualifiedPrimaryServerId.ToString();
						}
					}
					shadowMailItem.TransportMailItem.CommitLazy();
				}
				ShadowRedundancyManager.PerfCounters.IncrementCounter(ShadowRedundancyCounterId.AggregateShadowQueueLength);
			}
			finally
			{
				if (shadowMessageQueue != null && flag)
				{
					shadowMessageQueue.ReleaseReference();
				}
			}
		}

		// Token: 0x0600276F RID: 10095 RVA: 0x0009A1E0 File Offset: 0x000983E0
		private ShadowMailItem CreateShadowMailItem(TransportMailItem mailItem, NextHopSolution shadowNextHopSolution)
		{
			if (shadowNextHopSolution.NextHopSolutionKey.NextHopType.DeliveryType != DeliveryType.ShadowRedundancy)
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "shadowNextHopSolution has invalid delivery type '{0}'.", new object[]
				{
					shadowNextHopSolution.NextHopSolutionKey.NextHopType.DeliveryType
				}));
			}
			DateTime dateTime = DateTime.UtcNow;
			DateTime expiryTime = mailItem.GetExpiryTime(false);
			if (expiryTime < dateTime)
			{
				dateTime = expiryTime;
			}
			return new ShadowMailItem(mailItem, shadowNextHopSolution, dateTime, this.configurationSource);
		}

		// Token: 0x06002770 RID: 10096 RVA: 0x0009A270 File Offset: 0x00098470
		private ShadowMailItem CreateShadowMailItem(TransportMailItem mailItem, string serverFqdn)
		{
			NextHopSolutionKey key = NextHopSolutionKey.CreateShadowNextHopSolutionKey(serverFqdn, Guid.Empty, null);
			ShadowMailItem result;
			lock (mailItem)
			{
				NextHopSolution shadowNextHopSolution = mailItem.UpdateNextHopSolutionTable(key, mailItem.Recipients.All, true);
				result = this.CreateShadowMailItem(mailItem, shadowNextHopSolution);
			}
			return result;
		}

		// Token: 0x06002771 RID: 10097 RVA: 0x0009A2D4 File Offset: 0x000984D4
		private void AcquireMailItem(TransportMailItem mailItem)
		{
			if (mailItem == null)
			{
				throw new ArgumentNullException("mailItem");
			}
			long recordId;
			lock (mailItem)
			{
				recordId = mailItem.RecordId;
			}
			lock (this.syncAllMailItems)
			{
				this.allMailItemsByRecordId[recordId] = mailItem;
			}
		}

		// Token: 0x06002772 RID: 10098 RVA: 0x0009A358 File Offset: 0x00098558
		private void DiscardShadowResourcesForServer(string serverFqdnOrContext)
		{
			List<ShadowMessageQueue> queues = this.GetQueues(serverFqdnOrContext);
			foreach (ShadowMessageQueue shadowMessageQueue in queues)
			{
				shadowMessageQueue.DiscardAll();
			}
			ShadowRedundancyManager.ShadowServerInfo shadowServerInfo = this.GetShadowServerInfo(serverFqdnOrContext, false);
			if (shadowServerInfo != null)
			{
				shadowServerInfo.ClearDiscardEvents();
			}
		}

		// Token: 0x06002773 RID: 10099 RVA: 0x0009A3C0 File Offset: 0x000985C0
		private void EnqueueDiscardEventForShadowServer(TransportMailItem mailItem, DateTime mailItemExpiryTime)
		{
			if (!this.configurationSource.Enabled)
			{
				ExTraceGlobals.ShadowRedundancyTracer.TraceDebug<string, string>(0L, "Ignored discard event '{0}:{1}' due to shadow redundancy being disabled.", mailItem.ShadowServerContext, mailItem.ShadowServerDiscardId);
				ShadowRedundancyManager.MarkMailItemAsDiscardCompleted(mailItem);
				return;
			}
			ShadowRedundancyManager.ShadowServerInfo shadowServerInfo = this.GetShadowServerInfo(mailItem.ShadowServerContext, true);
			DateTime dateTime = DateTime.UtcNow.Add(this.configurationSource.DiscardEventExpireInterval);
			DateTime dateTime2 = (mailItemExpiryTime < dateTime) ? mailItemExpiryTime : dateTime;
			ExTraceGlobals.ShadowRedundancyTracer.TraceDebug<DateTime, DateTime, DateTime>(0L, "Effective discard expiry time {0}, Mail item expiry time {1}, Discard interval based expirty time {2}", dateTime2, mailItemExpiryTime, dateTime);
			if (DateTime.UtcNow > mailItemExpiryTime)
			{
				ExTraceGlobals.ShadowRedundancyTracer.TraceDebug<string, string>(mailItem.MsgId, "Expired mail item pending discard, dropping the message. server context '{0}' and discard id '{1}'", mailItem.ShadowServerContext, mailItem.ShadowServerDiscardId);
				ShadowRedundancyManager.MarkMailItemAsDiscardCompleted(mailItem);
				return;
			}
			shadowServerInfo.EnqueueDiscardEvent(mailItem, dateTime2);
			ExTraceGlobals.ShadowRedundancyTracer.TraceDebug<DateTime, string, string>(0L, "Enqueued discard event with expiry time '{0}', server context '{1}' and discard id '{2}'", dateTime2, mailItem.ShadowServerContext, mailItem.ShadowServerDiscardId);
		}

		// Token: 0x06002774 RID: 10100 RVA: 0x0009A4A8 File Offset: 0x000986A8
		private ShadowRedundancyManager.ShadowServerInfo GetShadowServerInfo(string serverContext, bool createIfNotFound)
		{
			this.shadowServerInfoMapReaderWriterLock.EnterUpgradeableReadLock();
			ShadowRedundancyManager.ShadowServerInfo shadowServerInfo;
			try
			{
				if (!this.shadowServerInfoMap.TryGetValue(serverContext, out shadowServerInfo) && createIfNotFound)
				{
					this.shadowServerInfoMapReaderWriterLock.EnterWriteLock();
					try
					{
						if (!this.shadowServerInfoMap.TryGetValue(serverContext, out shadowServerInfo))
						{
							serverContext = this.serverContextStringPool.Intern(serverContext);
							shadowServerInfo = new ShadowRedundancyManager.ShadowServerInfo(serverContext);
							this.shadowServerInfoMap.Add(serverContext, shadowServerInfo);
						}
					}
					finally
					{
						this.shadowServerInfoMapReaderWriterLock.ExitWriteLock();
					}
				}
			}
			finally
			{
				this.shadowServerInfoMapReaderWriterLock.ExitUpgradeableReadLock();
			}
			if (shadowServerInfo == null && createIfNotFound)
			{
				throw new InvalidOperationException("shadowServerInfo should never be null if createIfNotFound = true.");
			}
			return shadowServerInfo;
		}

		// Token: 0x06002775 RID: 10101 RVA: 0x0009A55C File Offset: 0x0009875C
		private void NotifyMailItemCreatedInShadowGroup(ShadowRedundancyManager.QualifiedShadowServerDiscardId qualifiedDiscardId)
		{
			lock (this.shadowGroupMap)
			{
				int num;
				if (!this.shadowGroupMap.TryGetValue(qualifiedDiscardId, out num))
				{
					num = 1;
				}
				else
				{
					num++;
				}
				this.shadowGroupMap[qualifiedDiscardId] = num;
			}
		}

		// Token: 0x06002776 RID: 10102 RVA: 0x0009A5BC File Offset: 0x000987BC
		private void DelayedAckCleanupCallback(object state)
		{
			if (this.delayedAckEntries.Count == 0)
			{
				return;
			}
			DateTime utcNow = DateTime.UtcNow;
			if ((utcNow - this.lastDelayedCleanupCompleteTime).Ticks < this.configurationSource.DelayedAckCheckExpiryInterval.Ticks / 2L)
			{
				return;
			}
			List<KeyValuePair<Guid, ShadowRedundancyManager.DelayedAckEntry>> list = null;
			lock (this.delayedAckEntries)
			{
				foreach (KeyValuePair<Guid, ShadowRedundancyManager.DelayedAckEntry> item in this.delayedAckEntries)
				{
					ShadowRedundancyManager.DelayedAckEntry value = item.Value;
					value.CheckForExpiry(utcNow);
					if (value.CompletionStatus != null)
					{
						if (list == null)
						{
							list = new List<KeyValuePair<Guid, ShadowRedundancyManager.DelayedAckEntry>>();
						}
						list.Add(item);
					}
				}
				if (list != null)
				{
					foreach (KeyValuePair<Guid, ShadowRedundancyManager.DelayedAckEntry> keyValuePair in list)
					{
						this.FinalizeDelayedAck(keyValuePair.Key, keyValuePair.Value, utcNow);
					}
				}
			}
			this.lastDelayedCleanupCompleteTime = DateTime.UtcNow;
			if (list != null)
			{
				list.Clear();
				list = null;
			}
		}

		// Token: 0x06002777 RID: 10103 RVA: 0x0009A714 File Offset: 0x00098914
		private void DiscardsCleanupCallback(object state)
		{
			if (this.shuttingDown)
			{
				return;
			}
			DateTime.UtcNow.Subtract(this.configurationSource.DiscardEventExpireInterval);
			this.shadowServerInfoMapReaderWriterLock.EnterWriteLock();
			try
			{
				List<string> list = new List<string>();
				foreach (ShadowRedundancyManager.ShadowServerInfo shadowServerInfo in this.shadowServerInfoMap.Values)
				{
					shadowServerInfo.DeleteExpiredDiscardEvents();
					if (shadowServerInfo.CanBeDeleted(this.configurationSource.ShadowServerInfoMaxIdleTimeInterval))
					{
						list.Add(shadowServerInfo.ServerContext);
					}
				}
				foreach (string key in list)
				{
					this.shadowServerInfoMap.Remove(key);
				}
			}
			finally
			{
				this.shadowServerInfoMapReaderWriterLock.ExitWriteLock();
			}
		}

		// Token: 0x06002778 RID: 10104 RVA: 0x0009A81C File Offset: 0x00098A1C
		private void StringPoolExpirationCallback(object state)
		{
			this.serverContextStringPool = new StringPool(StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x06002779 RID: 10105 RVA: 0x0009A830 File Offset: 0x00098A30
		private void ProcessPendingDiscardEvents()
		{
			lock (this.discardEventsToApplyAfterBootLoader)
			{
				foreach (KeyValuePair<ShadowRedundancyManager.QualifiedPrimaryServerId, List<Guid>> keyValuePair in this.discardEventsToApplyAfterBootLoader)
				{
					int num;
					this.ApplyDiscardEvents(keyValuePair.Key, keyValuePair.Value, out num);
					if (num > 0)
					{
						ExTraceGlobals.ShadowRedundancyTracer.TraceError<int, ShadowRedundancyManager.QualifiedPrimaryServerId, int>(0L, "Applied '{0}' discard events from primary server '{1}' with '{2}' not found.", keyValuePair.Value.Count, keyValuePair.Key, num);
					}
					else
					{
						ExTraceGlobals.ShadowRedundancyTracer.TraceDebug<int, ShadowRedundancyManager.QualifiedPrimaryServerId>(0L, "Applied '{0}' discard events from primary server '{1}'.", keyValuePair.Value.Count, keyValuePair.Key);
					}
				}
				ExTraceGlobals.ShadowRedundancyTracer.TraceDebug(0L, "All pending discard events were applied after boot loader complete.");
			}
			this.discardEventsToApplyAfterBootLoader = null;
		}

		// Token: 0x0600277A RID: 10106 RVA: 0x0009A924 File Offset: 0x00098B24
		private List<Guid> ConvertMessageIdsFromStringToGuid(ICollection<string> messageIds)
		{
			List<Guid> list = new List<Guid>(messageIds.Count);
			foreach (string text in messageIds)
			{
				Guid item;
				if (!GuidHelper.TryParseGuid(text, out item))
				{
					ExTraceGlobals.ShadowRedundancyTracer.TraceError<string>(0L, "ApplyDiscardEvents(): Discard id '{0}' is not a Guid.", text);
				}
				else
				{
					list.Add(item);
				}
			}
			return list;
		}

		// Token: 0x0600277B RID: 10107 RVA: 0x0009A998 File Offset: 0x00098B98
		private void ApplyDiscardEvents(ShadowRedundancyManager.QualifiedPrimaryServerId serverId, ICollection<Guid> messageIds, out int notFound)
		{
			notFound = 0;
			if (!this.configurationSource.Enabled)
			{
				return;
			}
			if (messageIds.Count == 0)
			{
				return;
			}
			if (!this.bootLoaderComplete)
			{
				ExTraceGlobals.ShadowRedundancyTracer.TraceDebug(0L, "ApplyDiscardEvents suppressing because bootloader is not finished");
				lock (this.discardEventsToApplyAfterBootLoader)
				{
					List<Guid> list;
					if (!this.discardEventsToApplyAfterBootLoader.TryGetValue(serverId, out list))
					{
						this.discardEventsToApplyAfterBootLoader.Add(serverId, new List<Guid>(messageIds));
					}
					else
					{
						list.AddRange(messageIds);
					}
					ExTraceGlobals.ShadowRedundancyTracer.TraceDebug<string, int>(0L, "ApplyDiscardEvents(serverId='{0}', messages='{1}') - message(s) queued since boot loader is still running.", serverId.ToString(), messageIds.Count);
				}
				return;
			}
			ExTraceGlobals.ShadowRedundancyTracer.TraceDebug<string, int>(0L, "ApplyDiscardEvents(serverId='{0}', messages='{1}')", serverId.ToString(), messageIds.Count);
			ShadowMessageQueue queue = this.GetQueue(serverId);
			if (queue == null)
			{
				notFound = messageIds.Count;
				ExTraceGlobals.ShadowRedundancyTracer.TraceDebug<ShadowRedundancyManager.QualifiedPrimaryServerId, int>(0L, "ApplyDiscardEvents(): Shadow queues for '{0}' not found.  '{1}' discard event(s) not found.", serverId, notFound);
				return;
			}
			foreach (Guid guid in messageIds)
			{
				if (!queue.Discard(guid, DiscardReason.ExplicitlyDiscarded))
				{
					notFound++;
					ExTraceGlobals.ShadowRedundancyTracer.TraceDebug<Guid, ShadowRedundancyManager.QualifiedPrimaryServerId>(0L, "ApplyDiscardEvents(): ShadowMailItem with shadow message id '{0}' not found in any queues for '{1}'.", guid, serverId);
				}
			}
		}

		// Token: 0x0600277C RID: 10108 RVA: 0x0009AB00 File Offset: 0x00098D00
		private void MarkShadowMailAsDelivered(ShadowMailItem shadowMailItem, string serverFqdn)
		{
			ExTraceGlobals.ShadowRedundancyTracer.TraceDebug<Guid, string>(0L, "MarkShadowMailAsDelivered(): Marking shadow message id '{0}' delivered from server '{1}'.", shadowMailItem.TransportMailItem.ShadowMessageId, serverFqdn);
			PrimaryServerInfo active = this.primaryServerInfoMap.GetActive(serverFqdn);
			if (active == null)
			{
				ExTraceGlobals.ShadowRedundancyTracer.TraceWarning<string>(0L, "MarkShadowMailAsDelivered(): Primary server '{0}' not found in map.", serverFqdn);
				return;
			}
			Destination deliveredDestination = new Destination(Destination.DestinationType.Shadow, active.DatabaseState);
			foreach (MailRecipient mailRecipient in shadowMailItem.TransportMailItem.Recipients)
			{
				mailRecipient.DeliveredDestination = deliveredDestination;
				mailRecipient.DeliveryTime = new DateTime?(DateTime.UtcNow);
				Components.MessageResubmissionComponent.StoreRecipient(mailRecipient);
				ExTraceGlobals.ShadowRedundancyTracer.TraceDebug<MailRecipient>(0L, "MarkShadowMailAsDelivered(): Moved recipient '{0}' to safety net.", mailRecipient);
			}
			SystemProbeHelper.ShadowRedundancyTracer.TracePass<string>(shadowMailItem.TransportMailItem, 0L, "Message delivered by {0} and moved to Safety Net shadow", serverFqdn);
		}

		// Token: 0x0600277D RID: 10109 RVA: 0x0009ABE4 File Offset: 0x00098DE4
		private void NotifyMailItemInstanceHandled(TransportMailItem mailItem)
		{
			ShadowRedundancyManager.QualifiedShadowServerDiscardId qualifiedShadowServerDiscardId = new ShadowRedundancyManager.QualifiedShadowServerDiscardId(mailItem.ShadowServerContext, mailItem.ShadowServerDiscardId);
			bool flag = false;
			lock (this.shadowGroupMap)
			{
				int num;
				if (this.shadowGroupMap.TryGetValue(qualifiedShadowServerDiscardId, out num))
				{
					if (num <= 0)
					{
						throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Number of bifurcations for message '{0}' is '{1}'.  This number should always be positive.", new object[]
						{
							qualifiedShadowServerDiscardId.ToString(),
							num
						}));
					}
					if (num > 1)
					{
						num = (this.shadowGroupMap[qualifiedShadowServerDiscardId] = num - 1);
					}
					else
					{
						this.shadowGroupMap.Remove(qualifiedShadowServerDiscardId);
					}
				}
				else
				{
					flag = true;
				}
			}
			if (flag)
			{
				this.NotifyMailItemHandled(mailItem, qualifiedShadowServerDiscardId, ((IQueueItem)mailItem).Expiry);
			}
		}

		// Token: 0x0600277E RID: 10110 RVA: 0x0009ACBC File Offset: 0x00098EBC
		private void NotifyMailItemHandled(TransportMailItem mailItem, ShadowRedundancyManager.QualifiedShadowServerDiscardId qualifiedDiscardId, DateTime mailItemExpiryTime)
		{
			if (qualifiedDiscardId.IsDelayedAck)
			{
				Guid guid;
				if (GuidHelper.TryParseGuid(qualifiedDiscardId.ServerDiscardId, out guid))
				{
					bool flag = false;
					ShadowRedundancyManager.DelayedAckEntry delayedAckEntry;
					lock (this.delayedAckEntries)
					{
						if (this.delayedAckEntries.TryGetValue(guid, out delayedAckEntry))
						{
							flag = true;
							this.delayedAckEntries.Remove(guid);
						}
					}
					if (flag)
					{
						ExTraceGlobals.ShadowRedundancyTracer.TraceDebug<string>(0L, "NotifyMailItemHandled(): Delayed ack message with ShadowMessageId '{0}' delivered successfully before timeout.  Notifying Smtp...", qualifiedDiscardId.ServerDiscardId);
						delayedAckEntry.MarkAsDelivered();
						if (this.delayedAckCompleted(delayedAckEntry.State, delayedAckEntry.CompletionStatus.Value, DateTime.UtcNow.Subtract(delayedAckEntry.StartTime), delayedAckEntry.Context))
						{
							return;
						}
						lock (this.delayedAckEntries)
						{
							TransportHelpers.AttemptAddToDictionary<Guid, ShadowRedundancyManager.DelayedAckEntry>(this.delayedAckEntries, guid, delayedAckEntry, null);
							return;
						}
					}
					ShadowRedundancyManager.PerfCounters.DelayedAckDeliveredAfterExpiry(1L);
					ExTraceGlobals.ShadowRedundancyTracer.TraceDebug<string>(0L, "NotifyMailItemHandled(): Delayed ack message with ShadowMessageId '{0}' has been delivered after delayed ack expired.", qualifiedDiscardId.ServerDiscardId);
					return;
				}
				ExTraceGlobals.ShadowRedundancyTracer.TraceError<string>(0L, "NotifyMailItemHandled(): ShadowMessageId '{0}' is not a valid Guid, delayed ack messages must have a Guid discard ids.  Generate Watson...", qualifiedDiscardId.ServerDiscardId);
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Invalid ServerDiscardId ('{0}') for a delayed ack message.", new object[]
				{
					qualifiedDiscardId.ServerDiscardId
				}));
			}
			else
			{
				ShadowRedundancyManager.MarkMailItemAsDiscardPending(mailItem);
				this.EnqueueDiscardEventForShadowServer(mailItem, mailItemExpiryTime);
				SystemProbeHelper.ShadowRedundancyTracer.TracePass(mailItem, 0L, "Message delivered by primary hub server and moved to Safety Net.");
			}
		}

		// Token: 0x0600277F RID: 10111 RVA: 0x0009AE64 File Offset: 0x00099064
		private ShadowMessageQueue GetQueue(ShadowRedundancyManager.QualifiedPrimaryServerId primaryServerId)
		{
			ShadowMessageQueue shadowMessageQueue;
			lock (this.syncQueues)
			{
				this.serverIdToShadowQueueMap.TryGetValue(primaryServerId, out shadowMessageQueue);
			}
			if (shadowMessageQueue == null)
			{
				ExTraceGlobals.ShadowRedundancyTracer.TraceError<ShadowRedundancyManager.QualifiedPrimaryServerId>(0L, "Shadow Queue '{0}' not found", primaryServerId);
			}
			return shadowMessageQueue;
		}

		// Token: 0x06002780 RID: 10112 RVA: 0x0009AEC4 File Offset: 0x000990C4
		private List<ShadowMessageQueue> GetQueues(string primaryFqdn)
		{
			List<ShadowMessageQueue> result;
			lock (this.syncQueues)
			{
				List<ShadowMessageQueue> collection;
				if (!this.serverFqdnToShadowQueueMap.TryGetValue(primaryFqdn, out collection))
				{
					result = new List<ShadowMessageQueue>();
				}
				else
				{
					result = new List<ShadowMessageQueue>(collection);
				}
			}
			return result;
		}

		// Token: 0x06002781 RID: 10113 RVA: 0x0009AF20 File Offset: 0x00099120
		private void AddQueue(ShadowRedundancyManager.QualifiedPrimaryServerId serverId, ShadowMessageQueue queue)
		{
			if (serverId.Equals(ShadowRedundancyManager.QualifiedPrimaryServerId.Empty))
			{
				throw new ArgumentException("serverId is empty");
			}
			TransportHelpers.AttemptAddToDictionary<ShadowRedundancyManager.QualifiedPrimaryServerId, ShadowMessageQueue>(this.serverIdToShadowQueueMap, serverId, queue, null);
			TransportHelpers.AttemptAddToDictionary<long, ShadowMessageQueue>(this.queueIdToShadowQueueMap, queue.Id, queue, null);
			List<ShadowMessageQueue> list;
			if (!this.serverFqdnToShadowQueueMap.TryGetValue(serverId.Fqdn, out list))
			{
				list = new List<ShadowMessageQueue>();
				this.serverFqdnToShadowQueueMap.Add(serverId.Fqdn, list);
			}
			list.Add(queue);
		}

		// Token: 0x06002782 RID: 10114 RVA: 0x0009AFA0 File Offset: 0x000991A0
		private void ProcessPendingResubmits()
		{
			if (this.queuedRecoveryResubmits.Count > 0)
			{
				string[] array;
				lock (this.queuedRecoveryResubmits)
				{
					array = this.queuedRecoveryResubmits.ToArray();
					this.queuedRecoveryResubmits.Clear();
				}
				foreach (string serverFqdn in array)
				{
					this.ResubmitAsync(serverFqdn, ResubmitReason.ShadowStateChange);
				}
			}
		}

		// Token: 0x06002783 RID: 10115 RVA: 0x0009B024 File Offset: 0x00099224
		private void ResubmitAsync(string serverFqdn, ResubmitReason resubmitReason)
		{
			ExTraceGlobals.ShadowRedundancyTracer.TraceDebug<string, ResubmitReason>(0L, "ResubmitAsync(): Scheduling a resubmit for server '{0}' with resubmit reason '{1}'.", serverFqdn, resubmitReason);
			lock (this.activeResubmitsSync)
			{
				this.activeResubmits++;
				if (this.activeResubmits == 1)
				{
					this.resubmitEvent.Reset();
				}
			}
			ThreadPool.QueueUserWorkItem(new WaitCallback(this.ResubmitCallback), new ShadowRedundancyManager.ResubmitState(serverFqdn, resubmitReason));
		}

		// Token: 0x06002784 RID: 10116 RVA: 0x0009B0B0 File Offset: 0x000992B0
		private void ResubmitCallback(object state)
		{
			ShadowRedundancyManager.ResubmitState resubmitState = (ShadowRedundancyManager.ResubmitState)state;
			this.ResubmitInternal(resubmitState.ServerFqdn, resubmitState.ResubmitReason);
			lock (this.activeResubmitsSync)
			{
				this.activeResubmits--;
				if (this.activeResubmits == 0)
				{
					this.resubmitEvent.Set();
				}
				else if (this.activeResubmits < 0)
				{
					throw new InvalidOperationException("this.activeResubmits shouldn't be negative.");
				}
			}
		}

		// Token: 0x06002785 RID: 10117 RVA: 0x0009B13C File Offset: 0x0009933C
		private void ResubmitInternal(string serverFqdn, ResubmitReason resubmitReason)
		{
			ICollection<ShadowMessageQueue> queues = this.GetQueues(serverFqdn);
			ExTraceGlobals.ShadowRedundancyTracer.TraceDebug<int, string, ResubmitReason>(0L, "ResubmitInternal(): Resubmit '{0}' queues for server '{1}' with resubmit reason '{2}'.", queues.Count, serverFqdn, resubmitReason);
			int num = 0;
			foreach (ShadowMessageQueue shadowMessageQueue in queues)
			{
				num += shadowMessageQueue.Resubmit(resubmitReason);
			}
			ShadowRedundancyManager.CompletedResubmitEntry item = new ShadowRedundancyManager.CompletedResubmitEntry(DateTime.UtcNow, serverFqdn, resubmitReason, num);
			lock (this.completedResubmitEntries)
			{
				this.completedResubmitEntries.Enqueue(item);
				if (this.completedResubmitEntries.Count > 1000)
				{
					this.completedResubmitEntries.Dequeue();
				}
			}
		}

		// Token: 0x06002786 RID: 10118 RVA: 0x0009B214 File Offset: 0x00099414
		private void UpdateHeartbeat(string primaryServer, bool successful)
		{
			List<ShadowMessageQueue> queues = this.GetQueues(primaryServer);
			ExTraceGlobals.ShadowRedundancyTracer.TraceDebug<string, int, bool>(0L, "UpdateHeartbeat({0}): updating {1} queues with status {2}", primaryServer, queues.Count, successful);
			if (queues.Count > 0)
			{
				DateTime utcNow = DateTime.UtcNow;
				foreach (ShadowMessageQueue shadowMessageQueue in queues)
				{
					ExTraceGlobals.ShadowRedundancyTracer.TraceDebug<NextHopSolutionKey, bool>(0L, "UpdateHeartbeat: updating queue {0} with status {1}", shadowMessageQueue.Key, successful);
					shadowMessageQueue.UpdateHeartbeat(utcNow, NextHopSolutionKey.Empty, successful);
				}
			}
		}

		// Token: 0x06002787 RID: 10119 RVA: 0x0009B2B0 File Offset: 0x000994B0
		private void NotifyConfigUpdated(IShadowRedundancyConfigurationSource oldConfiguration)
		{
			if (oldConfiguration == null)
			{
				throw new ArgumentNullException("oldConfiguration");
			}
			if (!this.configurationSource.Enabled)
			{
				lock (this.syncQueues)
				{
					foreach (List<ShadowMessageQueue> list in this.serverFqdnToShadowQueueMap.Values)
					{
						foreach (ShadowMessageQueue shadowMessageQueue in list)
						{
							shadowMessageQueue.DiscardAll();
						}
					}
				}
				try
				{
					this.shadowServerInfoMapReaderWriterLock.EnterWriteLock();
					this.shadowServerInfoMap.Clear();
					ShadowRedundancyManager.PerfCounters.DecrementCounterBy(ShadowRedundancyCounterId.RedundantMessageDiscardEvents, ShadowRedundancyManager.PerfCounters.RedundantMessageDiscardEvents);
				}
				finally
				{
					this.shadowServerInfoMapReaderWriterLock.ExitWriteLock();
				}
				lock (this.shadowGroupMap)
				{
					this.shadowGroupMap.Clear();
				}
			}
			if (this.configurationSource.HeartbeatRetryCount != oldConfiguration.HeartbeatRetryCount || this.configurationSource.HeartbeatFrequency != oldConfiguration.HeartbeatFrequency)
			{
				lock (this.syncQueues)
				{
					foreach (ShadowMessageQueue shadowMessageQueue2 in this.serverIdToShadowQueueMap.Values)
					{
						shadowMessageQueue2.NotifyConfigUpdated(oldConfiguration);
					}
				}
			}
		}

		// Token: 0x06002788 RID: 10120 RVA: 0x0009B4A8 File Offset: 0x000996A8
		private bool ValidCandidateForDelayedAckSkipping(IReadOnlyMailItem mailItem, TransportMessageQueue queue)
		{
			if (mailItem == null)
			{
				throw new ArgumentNullException("mailItem");
			}
			if (queue == null)
			{
				throw new ArgumentNullException("queue");
			}
			return this.configurationSource.Enabled && this.configurationSource.DelayedAckSkippingEnabled && mailItem.IsDelayedAck();
		}

		// Token: 0x06002789 RID: 10121 RVA: 0x0009B4F8 File Offset: 0x000996F8
		private string GetQueueId(TransportMessageQueue queue, out SubmitMessageQueue submitMessageQueue, out RoutedMessageQueue routedMessageQueue, out UnreachableMessageQueue unreachableMessageQueue)
		{
			routedMessageQueue = null;
			unreachableMessageQueue = null;
			submitMessageQueue = (queue as SubmitMessageQueue);
			if (submitMessageQueue != null)
			{
				return "Submission";
			}
			routedMessageQueue = (queue as RoutedMessageQueue);
			if (routedMessageQueue != null)
			{
				return routedMessageQueue.Key.NextHopDomain;
			}
			unreachableMessageQueue = (queue as UnreachableMessageQueue);
			if (unreachableMessageQueue != null)
			{
				return "Unreachable";
			}
			return null;
		}

		// Token: 0x0600278A RID: 10122 RVA: 0x0009B550 File Offset: 0x00099750
		private void LogNewShadowMailItem(ShadowMailItem shadowItem)
		{
			MessageTrackingLog.TrackHighAvailabilityReceive(MessageTrackingSource.SMTP, shadowItem.NextHopSolution.NextHopSolutionKey.NextHopDomain, shadowItem.TransportMailItem);
		}

		// Token: 0x040013E5 RID: 5093
		public const string CurrentServerToken = "$localhost$";

		// Token: 0x040013E6 RID: 5094
		public const string PrioritizationReason = "ShadowRedundancy";

		// Token: 0x040013E7 RID: 5095
		public const string DeletedState = "0de1e7ed-0de1-0de1-0de1-de1e7edele7e";

		// Token: 0x040013E8 RID: 5096
		private const int MaxCompletedResubmitEntries = 1000;

		// Token: 0x040013E9 RID: 5097
		private const string SubmissionQueue = "Submission";

		// Token: 0x040013EA RID: 5098
		private const string UnreachableQueue = "Unreachable";

		// Token: 0x040013EB RID: 5099
		internal static readonly ITracer ReceiveTracer = new CompositeTracer(ExTraceGlobals.SmtpReceiveTracer, ExTraceGlobals.ShadowRedundancyTracer);

		// Token: 0x040013EC RID: 5100
		internal static readonly ITracer SendTracer = new CompositeTracer(ExTraceGlobals.SmtpSendTracer, ExTraceGlobals.ShadowRedundancyTracer);

		// Token: 0x040013ED RID: 5101
		private static IShadowRedundancyPerformanceCounters shadowRedundancyPerformanceCounters;

		// Token: 0x040013EE RID: 5102
		private readonly Dictionary<long, TransportMailItem> allMailItemsByRecordId = new Dictionary<long, TransportMailItem>();

		// Token: 0x040013EF RID: 5103
		private readonly Dictionary<ShadowRedundancyManager.QualifiedPrimaryServerId, ShadowMessageQueue> serverIdToShadowQueueMap = new Dictionary<ShadowRedundancyManager.QualifiedPrimaryServerId, ShadowMessageQueue>();

		// Token: 0x040013F0 RID: 5104
		private readonly Dictionary<string, List<ShadowMessageQueue>> serverFqdnToShadowQueueMap = new Dictionary<string, List<ShadowMessageQueue>>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x040013F1 RID: 5105
		private readonly Dictionary<long, ShadowMessageQueue> queueIdToShadowQueueMap = new Dictionary<long, ShadowMessageQueue>();

		// Token: 0x040013F2 RID: 5106
		private readonly Dictionary<ShadowRedundancyManager.QualifiedShadowServerDiscardId, int> shadowGroupMap = new Dictionary<ShadowRedundancyManager.QualifiedShadowServerDiscardId, int>();

		// Token: 0x040013F3 RID: 5107
		private readonly Dictionary<string, ShadowRedundancyManager.ShadowServerInfo> shadowServerInfoMap = new Dictionary<string, ShadowRedundancyManager.ShadowServerInfo>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x040013F4 RID: 5108
		private readonly IPrimaryServerInfoMap primaryServerInfoMap;

		// Token: 0x040013F5 RID: 5109
		private readonly Guid databaseId;

		// Token: 0x040013F6 RID: 5110
		private ShadowRedundancyEventLogger shadowRedundancyEventLogger;

		// Token: 0x040013F7 RID: 5111
		private DelayedAckItemHandler delayedAckCompleted;

		// Token: 0x040013F8 RID: 5112
		private Dictionary<Guid, ShadowRedundancyManager.DelayedAckEntry> delayedAckEntries = new Dictionary<Guid, ShadowRedundancyManager.DelayedAckEntry>();

		// Token: 0x040013F9 RID: 5113
		private DateTime lastPrimaryServerInfoMapCleanup = DateTime.UtcNow;

		// Token: 0x040013FA RID: 5114
		private IShadowRedundancyConfigurationSource configurationSource;

		// Token: 0x040013FB RID: 5115
		private object syncQueues = new object();

		// Token: 0x040013FC RID: 5116
		private object syncAllMailItems = new object();

		// Token: 0x040013FD RID: 5117
		private ReaderWriterLockSlim shadowServerInfoMapReaderWriterLock = new ReaderWriterLockSlim();

		// Token: 0x040013FE RID: 5118
		private Dictionary<ShadowRedundancyManager.QualifiedPrimaryServerId, List<Guid>> discardEventsToApplyAfterBootLoader = new Dictionary<ShadowRedundancyManager.QualifiedPrimaryServerId, List<Guid>>();

		// Token: 0x040013FF RID: 5119
		private List<string> queuedRecoveryResubmits = new List<string>();

		// Token: 0x04001400 RID: 5120
		private int activeResubmits;

		// Token: 0x04001401 RID: 5121
		private ManualResetEvent resubmitEvent = new ManualResetEvent(true);

		// Token: 0x04001402 RID: 5122
		private object activeResubmitsSync = new object();

		// Token: 0x04001403 RID: 5123
		private HashSet<ShadowRedundancyManager.QualifiedShadowServerDiscardId> hashsetToDetectShadowGroupsOnStartup = new HashSet<ShadowRedundancyManager.QualifiedShadowServerDiscardId>();

		// Token: 0x04001404 RID: 5124
		private bool bootLoaderComplete;

		// Token: 0x04001405 RID: 5125
		private StringPool serverContextStringPool;

		// Token: 0x04001406 RID: 5126
		private GuardedTimer discardsCleanupTimer;

		// Token: 0x04001407 RID: 5127
		private GuardedTimer delayedAckCleanupTimer;

		// Token: 0x04001408 RID: 5128
		private DateTime lastDelayedCleanupCompleteTime = DateTime.MinValue;

		// Token: 0x04001409 RID: 5129
		private int primaryServerInfoMapCleanupGuard;

		// Token: 0x0400140A RID: 5130
		private GuardedTimer stringPoolCleanupTimer;

		// Token: 0x0400140B RID: 5131
		private bool shuttingDown;

		// Token: 0x0400140C RID: 5132
		private object timerCreateDestroySync = new object();

		// Token: 0x0400140D RID: 5133
		private Queue<ShadowRedundancyManager.CompletedResubmitEntry> completedResubmitEntries = new Queue<ShadowRedundancyManager.CompletedResubmitEntry>();

		// Token: 0x0400140E RID: 5134
		private IMessagingDatabase database;

		// Token: 0x0400140F RID: 5135
		private ShadowSessionFactory shadowSessionFactory;

		// Token: 0x04001410 RID: 5136
		private bool heartbeatEnabled;

		// Token: 0x04001411 RID: 5137
		private ServiceState targetRunningState;

		// Token: 0x04001412 RID: 5138
		private bool paused;

		// Token: 0x02000384 RID: 900
		internal struct QualifiedPrimaryServerId
		{
			// Token: 0x0600278D RID: 10125 RVA: 0x0009B5A6 File Offset: 0x000997A6
			public QualifiedPrimaryServerId(NextHopSolutionKey key)
			{
				this = new ShadowRedundancyManager.QualifiedPrimaryServerId(key.NextHopDomain, key.NextHopConnector, key.NextHopTlsDomain);
			}

			// Token: 0x0600278E RID: 10126 RVA: 0x0009B5C3 File Offset: 0x000997C3
			public QualifiedPrimaryServerId(string primaryFqdn, Guid nextHopGuid)
			{
				this = new ShadowRedundancyManager.QualifiedPrimaryServerId(primaryFqdn, nextHopGuid, string.Empty);
			}

			// Token: 0x0600278F RID: 10127 RVA: 0x0009B5D2 File Offset: 0x000997D2
			public QualifiedPrimaryServerId(string primaryFqdn, Guid nextHopGuid, string tlsDomain)
			{
				if (string.IsNullOrEmpty(primaryFqdn))
				{
					throw new ArgumentNullException("primaryFqdn");
				}
				if (tlsDomain == null)
				{
					throw new ArgumentNullException("tlsDomain");
				}
				this.fqdn = primaryFqdn;
				this.nextHopGuid = nextHopGuid;
				this.tlsDomain = tlsDomain;
			}

			// Token: 0x17000BDC RID: 3036
			// (get) Token: 0x06002790 RID: 10128 RVA: 0x0009B60A File Offset: 0x0009980A
			public string Fqdn
			{
				get
				{
					return this.fqdn;
				}
			}

			// Token: 0x17000BDD RID: 3037
			// (get) Token: 0x06002791 RID: 10129 RVA: 0x0009B612 File Offset: 0x00099812
			public Guid NextHopGuid
			{
				get
				{
					return this.nextHopGuid;
				}
			}

			// Token: 0x17000BDE RID: 3038
			// (get) Token: 0x06002792 RID: 10130 RVA: 0x0009B61A File Offset: 0x0009981A
			public string TlsDomain
			{
				get
				{
					if (this.tlsDomain != null)
					{
						return this.tlsDomain;
					}
					return string.Empty;
				}
			}

			// Token: 0x06002793 RID: 10131 RVA: 0x0009B630 File Offset: 0x00099830
			public static bool TryParse(string stringToParse, out ShadowRedundancyManager.QualifiedPrimaryServerId primaryServerId)
			{
				if (string.IsNullOrEmpty(stringToParse))
				{
					throw new ArgumentNullException("stringToParse");
				}
				primaryServerId = ShadowRedundancyManager.QualifiedPrimaryServerId.Empty;
				string[] array = stringToParse.Split(new char[]
				{
					'/'
				});
				Guid empty = Guid.Empty;
				switch (array.Length)
				{
				case 1:
					primaryServerId = new ShadowRedundancyManager.QualifiedPrimaryServerId(stringToParse, Guid.Empty);
					break;
				case 2:
					if (array[0].Equals(string.Empty))
					{
						return false;
					}
					empty = Guid.Empty;
					if (!GuidHelper.TryParseGuid(array[1], out empty))
					{
						return false;
					}
					primaryServerId = new ShadowRedundancyManager.QualifiedPrimaryServerId(array[0], empty);
					break;
				case 3:
					if (array[0].Equals(string.Empty) || array[2].Equals(string.Empty))
					{
						primaryServerId = ShadowRedundancyManager.QualifiedPrimaryServerId.Empty;
						return false;
					}
					if (!GuidHelper.TryParseGuid(array[1], out empty))
					{
						return false;
					}
					primaryServerId = new ShadowRedundancyManager.QualifiedPrimaryServerId(array[0], empty, array[2]);
					break;
				}
				return true;
			}

			// Token: 0x06002794 RID: 10132 RVA: 0x0009B72C File Offset: 0x0009992C
			public override string ToString()
			{
				if (this.nextHopGuid == Guid.Empty)
				{
					return this.fqdn;
				}
				return string.Concat(new object[]
				{
					this.fqdn,
					'/',
					this.nextHopGuid.ToString(),
					string.IsNullOrEmpty(this.TlsDomain) ? string.Empty : ('/' + this.TlsDomain)
				});
			}

			// Token: 0x06002795 RID: 10133 RVA: 0x0009B7B2 File Offset: 0x000999B2
			public bool Equals(ShadowRedundancyManager.QualifiedPrimaryServerId other)
			{
				return this.nextHopGuid == other.nextHopGuid && this.fqdn.Equals(other.fqdn, StringComparison.OrdinalIgnoreCase) && this.TlsDomain.Equals(other.TlsDomain, StringComparison.OrdinalIgnoreCase);
			}

			// Token: 0x06002796 RID: 10134 RVA: 0x0009B7F2 File Offset: 0x000999F2
			public override bool Equals(object other)
			{
				return this.Equals((ShadowRedundancyManager.QualifiedPrimaryServerId)other);
			}

			// Token: 0x06002797 RID: 10135 RVA: 0x0009B800 File Offset: 0x00099A00
			public override int GetHashCode()
			{
				return StringComparer.OrdinalIgnoreCase.GetHashCode(this.fqdn) ^ this.nextHopGuid.GetHashCode() ^ StringComparer.OrdinalIgnoreCase.GetHashCode(this.TlsDomain);
			}

			// Token: 0x04001415 RID: 5141
			private const char PrimaryFqdnDelimiter = '/';

			// Token: 0x04001416 RID: 5142
			public static readonly ShadowRedundancyManager.QualifiedPrimaryServerId Empty = default(ShadowRedundancyManager.QualifiedPrimaryServerId);

			// Token: 0x04001417 RID: 5143
			private readonly string fqdn;

			// Token: 0x04001418 RID: 5144
			private readonly Guid nextHopGuid;

			// Token: 0x04001419 RID: 5145
			private readonly string tlsDomain;
		}

		// Token: 0x02000385 RID: 901
		internal struct QualifiedShadowServerDiscardId
		{
			// Token: 0x06002799 RID: 10137 RVA: 0x0009B850 File Offset: 0x00099A50
			public QualifiedShadowServerDiscardId(string serverContext, string serverDiscardId)
			{
				ShadowRedundancyManager.QualifiedShadowServerDiscardId.EnsureServerContextValid(serverContext);
				ShadowRedundancyManager.QualifiedShadowServerDiscardId.EnsureShadowDiscardIdValid(serverDiscardId);
				this.serverContext = serverContext;
				this.serverDiscardId = serverDiscardId;
			}

			// Token: 0x17000BDF RID: 3039
			// (get) Token: 0x0600279A RID: 10138 RVA: 0x0009B86C File Offset: 0x00099A6C
			public string ServerContext
			{
				get
				{
					return this.serverContext;
				}
			}

			// Token: 0x17000BE0 RID: 3040
			// (get) Token: 0x0600279B RID: 10139 RVA: 0x0009B874 File Offset: 0x00099A74
			public string ServerDiscardId
			{
				get
				{
					return this.serverDiscardId;
				}
			}

			// Token: 0x17000BE1 RID: 3041
			// (get) Token: 0x0600279C RID: 10140 RVA: 0x0009B87C File Offset: 0x00099A7C
			public bool IsDelayedAck
			{
				get
				{
					return string.Equals(this.serverContext, "$localhost$", StringComparison.OrdinalIgnoreCase);
				}
			}

			// Token: 0x0600279D RID: 10141 RVA: 0x0009B88F File Offset: 0x00099A8F
			public static void EnsureServerContextValid(string serverContext)
			{
				if (string.IsNullOrEmpty(serverContext))
				{
					throw new ArgumentNullException("serverContext");
				}
			}

			// Token: 0x0600279E RID: 10142 RVA: 0x0009B8A4 File Offset: 0x00099AA4
			public static void EnsureShadowDiscardIdValid(string serverDiscardId)
			{
				if (string.IsNullOrEmpty(serverDiscardId))
				{
					throw new ArgumentNullException("serverDiscardId");
				}
			}

			// Token: 0x0600279F RID: 10143 RVA: 0x0009B8B9 File Offset: 0x00099AB9
			public override string ToString()
			{
				return this.serverContext + "::" + this.serverDiscardId;
			}

			// Token: 0x060027A0 RID: 10144 RVA: 0x0009B8D1 File Offset: 0x00099AD1
			public bool Equals(ShadowRedundancyManager.QualifiedShadowServerDiscardId other)
			{
				return this.serverDiscardId.Equals(other.serverDiscardId, StringComparison.OrdinalIgnoreCase) && this.serverContext.Equals(other.serverContext, StringComparison.OrdinalIgnoreCase);
			}

			// Token: 0x060027A1 RID: 10145 RVA: 0x0009B8FD File Offset: 0x00099AFD
			public override bool Equals(object other)
			{
				return this.Equals((ShadowRedundancyManager.QualifiedShadowServerDiscardId)other);
			}

			// Token: 0x060027A2 RID: 10146 RVA: 0x0009B90B File Offset: 0x00099B0B
			public override int GetHashCode()
			{
				return this.serverContext.GetHashCode() ^ this.serverDiscardId.GetHashCode();
			}

			// Token: 0x0400141A RID: 5146
			private readonly string serverContext;

			// Token: 0x0400141B RID: 5147
			private readonly string serverDiscardId;
		}

		// Token: 0x02000386 RID: 902
		private sealed class DelayedAckEntry
		{
			// Token: 0x060027A3 RID: 10147 RVA: 0x0009B924 File Offset: 0x00099B24
			public DelayedAckEntry(object state, DateTime startTime, DateTime expiryTime)
			{
				if (state == null)
				{
					throw new ArgumentNullException("state");
				}
				this.state = state;
				this.startTime = startTime;
				this.expiryTime = expiryTime;
			}

			// Token: 0x17000BE2 RID: 3042
			// (get) Token: 0x060027A4 RID: 10148 RVA: 0x0009B94F File Offset: 0x00099B4F
			public object State
			{
				get
				{
					return this.state;
				}
			}

			// Token: 0x17000BE3 RID: 3043
			// (get) Token: 0x060027A5 RID: 10149 RVA: 0x0009B957 File Offset: 0x00099B57
			public DateTime StartTime
			{
				get
				{
					return this.startTime;
				}
			}

			// Token: 0x17000BE4 RID: 3044
			// (get) Token: 0x060027A6 RID: 10150 RVA: 0x0009B95F File Offset: 0x00099B5F
			public DelayedAckCompletionStatus? CompletionStatus
			{
				get
				{
					return this.completionStatus;
				}
			}

			// Token: 0x17000BE5 RID: 3045
			// (get) Token: 0x060027A7 RID: 10151 RVA: 0x0009B967 File Offset: 0x00099B67
			public string Context
			{
				get
				{
					return this.context;
				}
			}

			// Token: 0x060027A8 RID: 10152 RVA: 0x0009B96F File Offset: 0x00099B6F
			public void CheckForExpiry(DateTime expiryThreshold)
			{
				if (this.expiryTime <= expiryThreshold)
				{
					this.MarkAsExpired("Timeout");
				}
			}

			// Token: 0x060027A9 RID: 10153 RVA: 0x0009B98A File Offset: 0x00099B8A
			public void MarkAsDelivered()
			{
				if (this.completionStatus == null)
				{
					this.completionStatus = new DelayedAckCompletionStatus?(DelayedAckCompletionStatus.Delivered);
				}
			}

			// Token: 0x060027AA RID: 10154 RVA: 0x0009B9A5 File Offset: 0x00099BA5
			public void MarkAsSkipped(string context)
			{
				if (this.completionStatus == null)
				{
					this.completionStatus = new DelayedAckCompletionStatus?(DelayedAckCompletionStatus.Skipped);
					this.context = context;
				}
			}

			// Token: 0x060027AB RID: 10155 RVA: 0x0009B9C7 File Offset: 0x00099BC7
			public void MarkAsExpired(string context)
			{
				if (this.completionStatus == null)
				{
					this.completionStatus = new DelayedAckCompletionStatus?(DelayedAckCompletionStatus.Expired);
					this.context = context;
				}
			}

			// Token: 0x060027AC RID: 10156 RVA: 0x0009B9EC File Offset: 0x00099BEC
			public override string ToString()
			{
				return string.Format(CultureInfo.InvariantCulture, "[DelayedAckEntry: state='{0}' startTime='{1}' expiryTime='{2}' completionStatus='{3}' delay='{4}' context='{5}']", new object[]
				{
					this.state,
					this.startTime,
					this.expiryTime,
					(this.completionStatus != null) ? this.completionStatus.Value.ToString() : "<null>",
					DateTime.UtcNow - this.startTime,
					this.context ?? "<null>"
				});
			}

			// Token: 0x0400141C RID: 5148
			private readonly DateTime startTime;

			// Token: 0x0400141D RID: 5149
			private readonly DateTime expiryTime;

			// Token: 0x0400141E RID: 5150
			private object state;

			// Token: 0x0400141F RID: 5151
			private DelayedAckCompletionStatus? completionStatus;

			// Token: 0x04001420 RID: 5152
			private string context;
		}

		// Token: 0x02000387 RID: 903
		private sealed class ResubmitState
		{
			// Token: 0x060027AD RID: 10157 RVA: 0x0009BA8B File Offset: 0x00099C8B
			public ResubmitState(string serverFqdn, ResubmitReason resubmitReason)
			{
				if (string.IsNullOrEmpty(serverFqdn))
				{
					throw new ArgumentNullException("serverFqdn");
				}
				ShadowMessageQueue.EnsureValidResubmitReason(resubmitReason);
				this.serverFqdn = serverFqdn;
				this.resubmitReason = resubmitReason;
			}

			// Token: 0x17000BE6 RID: 3046
			// (get) Token: 0x060027AE RID: 10158 RVA: 0x0009BABA File Offset: 0x00099CBA
			public string ServerFqdn
			{
				get
				{
					return this.serverFqdn;
				}
			}

			// Token: 0x17000BE7 RID: 3047
			// (get) Token: 0x060027AF RID: 10159 RVA: 0x0009BAC2 File Offset: 0x00099CC2
			public ResubmitReason ResubmitReason
			{
				get
				{
					return this.resubmitReason;
				}
			}

			// Token: 0x04001421 RID: 5153
			private readonly string serverFqdn;

			// Token: 0x04001422 RID: 5154
			private readonly ResubmitReason resubmitReason;
		}

		// Token: 0x02000388 RID: 904
		private sealed class ShadowServerInfo
		{
			// Token: 0x060027B0 RID: 10160 RVA: 0x0009BACA File Offset: 0x00099CCA
			public ShadowServerInfo(string serverContext)
			{
				ShadowRedundancyManager.QualifiedShadowServerDiscardId.EnsureServerContextValid(serverContext);
				this.serverContext = serverContext;
				this.lastActivity = DateTime.UtcNow;
			}

			// Token: 0x17000BE8 RID: 3048
			// (get) Token: 0x060027B1 RID: 10161 RVA: 0x0009BAEA File Offset: 0x00099CEA
			public string ServerContext
			{
				get
				{
					return this.serverContext;
				}
			}

			// Token: 0x060027B2 RID: 10162 RVA: 0x0009BAF4 File Offset: 0x00099CF4
			public bool CanBeDeleted(TimeSpan idleTime)
			{
				bool result;
				lock (this)
				{
					result = ((this.queue == null || this.queue.Count == 0) && DateTime.UtcNow >= this.lastActivity.Add(idleTime));
				}
				return result;
			}

			// Token: 0x060027B3 RID: 10163 RVA: 0x0009BB5C File Offset: 0x00099D5C
			public string[] DequeueDiscardEvents(int max)
			{
				if (this.queue == null)
				{
					return new string[0];
				}
				int num;
				string[] array;
				lock (this)
				{
					num = Math.Min(this.queue.Count, max);
					ExTraceGlobals.ShadowRedundancyTracer.TraceDebug<int>(0L, "Preparing to return {0} discard ids", num);
					array = new string[num];
					for (int i = 0; i < num; i++)
					{
						ShadowRedundancyManager.ShadowServerInfo.DiscardEntryForShadow discardEntryForShadow = this.queue.Dequeue();
						array[i] = discardEntryForShadow.MessageId;
						ShadowRedundancyManager.MarkMailItemAsDiscardCompleted(discardEntryForShadow.MailItem);
						ExTraceGlobals.ShadowRedundancyTracer.TraceDebug<string>(0L, "Dequeued discard id {0}", array[i] ?? "NULL");
					}
					this.lastActivity = DateTime.UtcNow;
				}
				ShadowRedundancyManager.PerfCounters.DecrementCounterBy(ShadowRedundancyCounterId.RedundantMessageDiscardEvents, (long)num);
				return array;
			}

			// Token: 0x060027B4 RID: 10164 RVA: 0x0009BC34 File Offset: 0x00099E34
			public XElement GetDiagnosticInfo(bool showVerbose)
			{
				XElement xelement = new XElement("ShadowServerInfo", new XElement("lastActivity", this.lastActivity));
				lock (this)
				{
					int num;
					if (this.queue == null || this.queue.Count == 0)
					{
						num = 0;
					}
					else
					{
						num = this.queue.Count;
					}
					xelement.Add(new XElement("discardEventsCount", num));
					if (showVerbose && this.queue != null)
					{
						using (Queue<ShadowRedundancyManager.ShadowServerInfo.DiscardEntryForShadow>.Enumerator enumerator = this.queue.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								ShadowRedundancyManager.ShadowServerInfo.DiscardEntryForShadow discardEntryForShadow = enumerator.Current;
								xelement.Add(new XElement("discardEventMessageId", discardEntryForShadow.MessageId));
							}
							goto IL_E8;
						}
					}
					xelement.Add(new XElement("help", "Use 'verbose' argument to show discard events."));
					IL_E8:;
				}
				return xelement;
			}

			// Token: 0x060027B5 RID: 10165 RVA: 0x0009BD54 File Offset: 0x00099F54
			public void EnqueueDiscardEvent(TransportMailItem mailItem, DateTime expiryTime)
			{
				lock (this)
				{
					if (this.queue == null)
					{
						this.queue = new Queue<ShadowRedundancyManager.ShadowServerInfo.DiscardEntryForShadow>();
					}
					this.queue.Enqueue(new ShadowRedundancyManager.ShadowServerInfo.DiscardEntryForShadow(mailItem, expiryTime));
					this.lastActivity = DateTime.UtcNow;
				}
				ShadowRedundancyManager.PerfCounters.IncrementCounter(ShadowRedundancyCounterId.RedundantMessageDiscardEvents);
			}

			// Token: 0x060027B6 RID: 10166 RVA: 0x0009BDC4 File Offset: 0x00099FC4
			public void ClearDiscardEvents()
			{
				int num = 0;
				lock (this)
				{
					num = this.queue.Count;
					foreach (ShadowRedundancyManager.ShadowServerInfo.DiscardEntryForShadow discardEntryForShadow in this.queue)
					{
						ShadowRedundancyManager.MarkMailItemAsDiscardCompleted(discardEntryForShadow.MailItem);
					}
					this.queue = null;
					this.lastActivity = DateTime.UtcNow;
				}
				ShadowRedundancyManager.PerfCounters.DecrementCounterBy(ShadowRedundancyCounterId.RedundantMessageDiscardEvents, (long)num);
			}

			// Token: 0x060027B7 RID: 10167 RVA: 0x0009BE70 File Offset: 0x0009A070
			public void DeleteExpiredDiscardEvents()
			{
				if (this.queue == null)
				{
					return;
				}
				int num = 0;
				lock (this)
				{
					foreach (ShadowRedundancyManager.ShadowServerInfo.DiscardEntryForShadow discardEntryForShadow in this.queue)
					{
						if (discardEntryForShadow.IsExpired())
						{
							num++;
						}
					}
					if (num == 0)
					{
						return;
					}
					Queue<ShadowRedundancyManager.ShadowServerInfo.DiscardEntryForShadow> queue;
					if (this.queue.Count - num > 0)
					{
						queue = new Queue<ShadowRedundancyManager.ShadowServerInfo.DiscardEntryForShadow>(this.queue.Count - num);
					}
					else
					{
						queue = new Queue<ShadowRedundancyManager.ShadowServerInfo.DiscardEntryForShadow>();
					}
					foreach (ShadowRedundancyManager.ShadowServerInfo.DiscardEntryForShadow item in this.queue)
					{
						if (item.IsExpired())
						{
							ShadowRedundancyManager.MarkMailItemAsDiscardCompleted(item.MailItem);
						}
						else
						{
							queue.Enqueue(item);
						}
					}
					this.queue = queue;
				}
				if (num > 0)
				{
					ShadowRedundancyManager.PerfCounters.IncrementCounterBy(ShadowRedundancyCounterId.RedundantMessageDiscardEventsExpired, (long)num);
					ShadowRedundancyManager.PerfCounters.DecrementCounterBy(ShadowRedundancyCounterId.RedundantMessageDiscardEvents, (long)num);
				}
			}

			// Token: 0x060027B8 RID: 10168 RVA: 0x0009BFB0 File Offset: 0x0009A1B0
			public override int GetHashCode()
			{
				return this.serverContext.GetHashCode();
			}

			// Token: 0x04001423 RID: 5155
			private readonly string serverContext;

			// Token: 0x04001424 RID: 5156
			private DateTime lastActivity;

			// Token: 0x04001425 RID: 5157
			private Queue<ShadowRedundancyManager.ShadowServerInfo.DiscardEntryForShadow> queue;

			// Token: 0x02000389 RID: 905
			private struct DiscardEntryForShadow
			{
				// Token: 0x060027B9 RID: 10169 RVA: 0x0009BFBD File Offset: 0x0009A1BD
				public DiscardEntryForShadow(TransportMailItem mailItem, DateTime expiryTime)
				{
					ShadowRedundancyManager.QualifiedShadowServerDiscardId.EnsureShadowDiscardIdValid(mailItem.ShadowServerDiscardId);
					this.mailItem = mailItem;
					this.expiryTime = expiryTime;
					this.discardId = mailItem.ShadowServerDiscardId;
				}

				// Token: 0x17000BE9 RID: 3049
				// (get) Token: 0x060027BA RID: 10170 RVA: 0x0009BFE4 File Offset: 0x0009A1E4
				public TransportMailItem MailItem
				{
					get
					{
						return this.mailItem;
					}
				}

				// Token: 0x17000BEA RID: 3050
				// (get) Token: 0x060027BB RID: 10171 RVA: 0x0009BFEC File Offset: 0x0009A1EC
				public string MessageId
				{
					get
					{
						return this.discardId;
					}
				}

				// Token: 0x060027BC RID: 10172 RVA: 0x0009BFF4 File Offset: 0x0009A1F4
				public bool IsExpired()
				{
					return this.expiryTime <= DateTime.UtcNow;
				}

				// Token: 0x04001426 RID: 5158
				private readonly TransportMailItem mailItem;

				// Token: 0x04001427 RID: 5159
				private readonly DateTime expiryTime;

				// Token: 0x04001428 RID: 5160
				private readonly string discardId;
			}
		}

		// Token: 0x0200038A RID: 906
		private sealed class PrimaryServerInfoScanner : ChunkingScanner
		{
			// Token: 0x060027BD RID: 10173 RVA: 0x0009C006 File Offset: 0x0009A206
			public PrimaryServerInfoScanner(IMessagingDatabase database)
			{
				this.database = database;
			}

			// Token: 0x060027BE RID: 10174 RVA: 0x0009C018 File Offset: 0x0009A218
			public void Load(IPrimaryServerInfoMap primaryServerInfoMap)
			{
				if (primaryServerInfoMap == null)
				{
					throw new ArgumentNullException("primaryServerInfoMap");
				}
				if (primaryServerInfoMap.Count > 0)
				{
					throw new ArgumentException("primaryServerInfoMap must be empty.");
				}
				ExTraceGlobals.ShadowRedundancyTracer.TraceDebug(0L, "PrimaryServerInfoScanner.Load(): Started loading primary server info from database.");
				this.primaryServerInfoMap = primaryServerInfoMap;
				using (DataTableCursor cursor = this.database.ServerInfoTable.GetCursor())
				{
					using (Transaction transaction = cursor.BeginTransaction())
					{
						this.Scan(transaction, cursor, true);
					}
				}
				ExTraceGlobals.ShadowRedundancyTracer.TraceDebug<int>(0L, "PrimaryServerInfoScanner.Load(): Loaded '{0}' primary server info records from database (including current server state).", this.primaryServerInfoMap.Count);
				this.primaryServerInfoMap = null;
			}

			// Token: 0x060027BF RID: 10175 RVA: 0x0009C0D8 File Offset: 0x0009A2D8
			protected override ChunkingScanner.ScanControl HandleRecord(DataTableCursor cursor)
			{
				PrimaryServerInfo primaryServerInfo = PrimaryServerInfo.Load(cursor, this.database.ServerInfoTable);
				this.primaryServerInfoMap.Add(primaryServerInfo);
				return ChunkingScanner.ScanControl.Continue;
			}

			// Token: 0x04001429 RID: 5161
			private IPrimaryServerInfoMap primaryServerInfoMap;

			// Token: 0x0400142A RID: 5162
			private IMessagingDatabase database;
		}

		// Token: 0x0200038B RID: 907
		private sealed class CompletedResubmitEntry
		{
			// Token: 0x060027C0 RID: 10176 RVA: 0x0009C104 File Offset: 0x0009A304
			public CompletedResubmitEntry(DateTime timestamp, string primaryServerFqdn, ResubmitReason reason, int messageCount)
			{
				this.timestamp = timestamp;
				this.primaryServerFqdn = primaryServerFqdn;
				this.reason = reason;
				this.messageCount = messageCount;
			}

			// Token: 0x17000BEB RID: 3051
			// (get) Token: 0x060027C1 RID: 10177 RVA: 0x0009C129 File Offset: 0x0009A329
			public DateTime Timestamp
			{
				get
				{
					return this.timestamp;
				}
			}

			// Token: 0x17000BEC RID: 3052
			// (get) Token: 0x060027C2 RID: 10178 RVA: 0x0009C131 File Offset: 0x0009A331
			public string PrimaryServerFqdn
			{
				get
				{
					return this.primaryServerFqdn;
				}
			}

			// Token: 0x17000BED RID: 3053
			// (get) Token: 0x060027C3 RID: 10179 RVA: 0x0009C139 File Offset: 0x0009A339
			public ResubmitReason Reason
			{
				get
				{
					return this.reason;
				}
			}

			// Token: 0x17000BEE RID: 3054
			// (get) Token: 0x060027C4 RID: 10180 RVA: 0x0009C141 File Offset: 0x0009A341
			public int MessageCount
			{
				get
				{
					return this.messageCount;
				}
			}

			// Token: 0x0400142B RID: 5163
			private readonly DateTime timestamp;

			// Token: 0x0400142C RID: 5164
			private readonly string primaryServerFqdn;

			// Token: 0x0400142D RID: 5165
			private readonly ResubmitReason reason;

			// Token: 0x0400142E RID: 5166
			private readonly int messageCount;
		}
	}
}
