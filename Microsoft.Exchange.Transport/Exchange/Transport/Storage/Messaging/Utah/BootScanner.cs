using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Transport.Internal.MExRuntime;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.Threading;
using Microsoft.Exchange.Transport.Categorizer;
using Microsoft.Exchange.Transport.Extensibility;
using Microsoft.Exchange.Transport.Logging.MessageTracking;
using Microsoft.Exchange.Transport.RemoteDelivery;
using Microsoft.Exchange.Transport.ShadowRedundancy;

namespace Microsoft.Exchange.Transport.Storage.Messaging.Utah
{
	// Token: 0x0200010A RID: 266
	internal class BootScanner : IBootLoader, IStartableTransportComponent, ITransportComponent
	{
		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000B4B RID: 2891 RVA: 0x00027B70 File Offset: 0x00025D70
		// (remove) Token: 0x06000B4C RID: 2892 RVA: 0x00027BA8 File Offset: 0x00025DA8
		public event Action OnBootLoadCompleted;

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06000B4D RID: 2893 RVA: 0x00027BDD File Offset: 0x00025DDD
		public string CurrentState
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06000B4E RID: 2894 RVA: 0x00027BE0 File Offset: 0x00025DE0
		protected SegmentedSlidingCounter RecentPoisonMessagesCounter
		{
			get
			{
				return this.recentPoisonMessagesCounter;
			}
		}

		// Token: 0x06000B4F RID: 2895 RVA: 0x00027BE8 File Offset: 0x00025DE8
		public void SetLoadTimeDependencies(ExEventLog eventLogger, IMessagingDatabase database, ShadowRedundancyComponent shadowRedundancyComponent, PoisonMessage poisonComponent, ICategorizer categorizerComponent, QueueManager queueManagerComponent, IBootLoaderConfig bootLoaderConfiguration)
		{
			this.database = database;
			this.eventLogger = eventLogger;
			this.shadowRedundancyComponent = shadowRedundancyComponent;
			this.poisonComponent = poisonComponent;
			this.categorizerComponent = categorizerComponent;
			this.queueManagerComponent = queueManagerComponent;
			this.configuration = bootLoaderConfiguration;
		}

		// Token: 0x06000B50 RID: 2896 RVA: 0x00027C20 File Offset: 0x00025E20
		public void Load()
		{
			this.OnBootLoadCompleted += this.database.BootLoadCompleted;
			if (this.configuration.PoisonCountPublishingEnabled)
			{
				int poisonCountLookbackHours = this.configuration.PoisonCountLookbackHours;
				TimeSpan[] array = new TimeSpan[poisonCountLookbackHours];
				for (int i = 0; i < poisonCountLookbackHours; i++)
				{
					array[i] = TimeSpan.FromHours(1.0);
				}
				this.recentPoisonMessagesCounter = new SegmentedSlidingCounter(array, TimeSpan.FromMinutes(5.0));
			}
		}

		// Token: 0x06000B51 RID: 2897 RVA: 0x00027CA5 File Offset: 0x00025EA5
		public void Unload()
		{
			if (this.poisonMessageCountTimer != null)
			{
				this.poisonMessageCountTimer.Dispose(true);
			}
		}

		// Token: 0x06000B52 RID: 2898 RVA: 0x00027CBC File Offset: 0x00025EBC
		public string OnUnhandledException(Exception e)
		{
			string text = this.CreateLoadedMessagesReport();
			this.EventLogLoadedMessages(text);
			StringBuilder stringBuilder = new StringBuilder("The following messages were loaded at startup before Transport crashed: ");
			stringBuilder.Append(text);
			return stringBuilder.ToString();
		}

		// Token: 0x06000B53 RID: 2899 RVA: 0x00027CF0 File Offset: 0x00025EF0
		public void Start(bool initiallyPaused, ServiceState targetRunningState)
		{
			this.targetRunningState = targetRunningState;
			if (!initiallyPaused && this.ShouldExecute())
			{
				this.Start();
			}
		}

		// Token: 0x06000B54 RID: 2900 RVA: 0x00027D0A File Offset: 0x00025F0A
		public void Stop()
		{
			this.Stop(false);
		}

		// Token: 0x06000B55 RID: 2901 RVA: 0x00027D13 File Offset: 0x00025F13
		public void Pause()
		{
			this.Stop(true);
		}

		// Token: 0x06000B56 RID: 2902 RVA: 0x00027D1C File Offset: 0x00025F1C
		public void Continue()
		{
			if (this.ShouldExecute())
			{
				this.Start();
			}
		}

		// Token: 0x06000B57 RID: 2903 RVA: 0x00027D2C File Offset: 0x00025F2C
		protected virtual bool IsPoison(TransportMailItem mailItem, out bool newPoisonMessage)
		{
			return this.poisonComponent.HandlePoison(mailItem, out newPoisonMessage);
		}

		// Token: 0x06000B58 RID: 2904 RVA: 0x00027D3C File Offset: 0x00025F3C
		protected virtual void SendToPoisonQueue(TransportMailItem mailItem)
		{
			if (Components.MessageDepotComponent.Enabled)
			{
				MessageDepotMailItem item = new MessageDepotMailItem(mailItem);
				Components.MessageDepotComponent.MessageDepot.Add(item);
				return;
			}
			this.queueManagerComponent.PoisonMessageQueue.Enqueue(mailItem);
		}

		// Token: 0x06000B59 RID: 2905 RVA: 0x00027D7E File Offset: 0x00025F7E
		protected virtual void SendToSubmissionQueue(TransportMailItem categorizerMailItem)
		{
			LatencyTracker.TrackPreProcessLatency(LatencyComponent.ServiceRestart, categorizerMailItem.LatencyTracker, categorizerMailItem.DateReceived);
			this.categorizerComponent.EnqueueSubmittedMessage(categorizerMailItem);
			ExTraceGlobals.StorageTracer.TraceDebug(categorizerMailItem.MsgId, "Bootloader submitted the message to categorizer.");
		}

		// Token: 0x06000B5A RID: 2906 RVA: 0x00027DB5 File Offset: 0x00025FB5
		protected virtual void SendToShadowQueue(TransportMailItem shadowRedundancyMailItem)
		{
			this.shadowRedundancyComponent.ShadowRedundancyManager.ProcessMailItemOnStartup(shadowRedundancyMailItem);
			ExTraceGlobals.StorageTracer.TraceDebug(shadowRedundancyMailItem.MsgId, "Bootloader submitted the message to shadow manager.");
		}

		// Token: 0x06000B5B RID: 2907 RVA: 0x00027DDD File Offset: 0x00025FDD
		protected virtual void SendToDiscardPendingQueue(TransportMailItem mailItem)
		{
			this.shadowRedundancyComponent.ShadowRedundancyManager.EnqueueDiscardPendingMailItem(mailItem);
			ExTraceGlobals.StorageTracer.TraceDebug(mailItem.MsgId, "Bootloader moved message to the discard queue.");
		}

		// Token: 0x06000B5C RID: 2908 RVA: 0x00027E05 File Offset: 0x00026005
		protected virtual TransportMailItem MoveUndeliveredRecipientsToNewClone(TransportMailItem mailItem)
		{
			return this.shadowRedundancyComponent.ShadowRedundancyManager.MoveUndeliveredRecipientsToNewClone(mailItem);
		}

		// Token: 0x06000B5D RID: 2909 RVA: 0x00027E18 File Offset: 0x00026018
		protected virtual void NotifyShadowManagerMailItemIsPoison(TransportMailItem mailItem)
		{
			this.shadowRedundancyComponent.ShadowRedundancyManager.NotifyMailItemPoison(mailItem);
		}

		// Token: 0x06000B5E RID: 2910 RVA: 0x00027E2B File Offset: 0x0002602B
		protected virtual void LogPoisonMessageCount(int poisonMessageCount)
		{
			this.poisonComponent.LogPoisonMessageCount(poisonMessageCount);
		}

		// Token: 0x06000B5F RID: 2911 RVA: 0x00027E39 File Offset: 0x00026039
		protected virtual IMExSession CreateMExSession()
		{
			return StorageAgentMExEvents.GetExecutionContext();
		}

		// Token: 0x06000B60 RID: 2912 RVA: 0x00027E40 File Offset: 0x00026040
		protected virtual void RaiseOnMessageLoadEvent(IMExSession session, TransportMailItem mailItem)
		{
			TransportMailItemWrapper mailItem2 = new TransportMailItemWrapper(mailItem, true);
			OnLoadedMessageEventSource onLoadedMessageEventSource = new OnLoadedMessageEventSource(mailItem);
			OnLoadedMessageEventArgs onLoadedMessageEventArgs = new OnLoadedMessageEventArgs(mailItem2);
			StorageAgentMExEvents.RaiseEvent(session, "OnLoadedMessage", new object[]
			{
				onLoadedMessageEventSource,
				onLoadedMessageEventArgs
			});
		}

		// Token: 0x06000B61 RID: 2913 RVA: 0x00027E7E File Offset: 0x0002607E
		protected virtual void CloseMExSession(IMExSession session)
		{
			session.Close();
		}

		// Token: 0x06000B62 RID: 2914 RVA: 0x00027E86 File Offset: 0x00026086
		protected virtual void ExtendMailItemExpiry(TransportMailItem mailItem)
		{
			mailItem.SetExpirationTime(DateTime.UtcNow + this.configuration.MessageExpirationGracePeriod);
		}

		// Token: 0x06000B63 RID: 2915 RVA: 0x00027EA3 File Offset: 0x000260A3
		protected virtual DateTime GetMailItemExpiry(TransportMailItem mailItem)
		{
			return mailItem.Expiry;
		}

		// Token: 0x06000B64 RID: 2916 RVA: 0x00027EAC File Offset: 0x000260AC
		private void HandlePoison(TransportMailItem mailItem, bool newPoisonMessage)
		{
			LatencyTracker.TrackPreProcessLatency(LatencyComponent.ServiceRestart, mailItem.LatencyTracker, mailItem.DateReceived);
			ExTraceGlobals.StorageTracer.TraceDebug(mailItem.MsgId, "Poison message detected.");
			mailItem.DropBreadcrumb(Breadcrumb.MailItemPoison);
			if (newPoisonMessage)
			{
				MessageTrackingLog.TrackPoisonMessage(MessageTrackingSource.POISONMESSAGE, mailItem);
			}
			if (this.IsPoisonMessageTooOld(mailItem))
			{
				this.DeletePoisonMessage(mailItem);
				return;
			}
			this.SendToPoisonQueue(mailItem);
			if (mailItem.IsShadowed())
			{
				this.NotifyShadowManagerMailItemIsPoison(mailItem);
				mailItem.CommitLazy();
			}
		}

		// Token: 0x06000B65 RID: 2917 RVA: 0x00027F24 File Offset: 0x00026124
		protected virtual void DeletePoisonMessage(TransportMailItem mailItem)
		{
			mailItem.Ack(AckStatus.Fail, AckReason.PoisonMessageExpired, mailItem.Recipients, null);
			MessageTrackingLog.TrackPoisonMessageDeleted(MessageTrackingSource.BOOTLOADER, "PoisonExpired", mailItem);
			mailItem.ReleaseFromActiveMaterializedLazy();
			ExTraceGlobals.QueuingTracer.TraceDebug<string>(0L, "Poison message {0} was deleted by the Bootscanner", mailItem.InternetMessageId);
		}

		// Token: 0x06000B66 RID: 2918 RVA: 0x00027F63 File Offset: 0x00026163
		private bool IsPoisonMessageTooOld(TransportMailItem mailItem)
		{
			return mailItem.DateReceived < DateTime.UtcNow - this.configuration.PoisonMessageRetentionPeriod;
		}

		// Token: 0x06000B67 RID: 2919 RVA: 0x00027F88 File Offset: 0x00026188
		private void HandleStrandedMessage(TransportMailItem mailItem, IMExSession session)
		{
			this.poisonComponent.SetMessageContext(mailItem, MessageProcessingSource.BootLoader);
			this.RaiseOnMessageLoadEvent(session, mailItem);
			if (!mailItem.IsActive && !mailItem.IsDiscardPending)
			{
				ExTraceGlobals.StorageTracer.TraceDebug<string, string>(0L, "Bootscanner skipping item {0} with subject {1} because it was deleted by an agent", mailItem.InternetMessageId, mailItem.Subject);
				MessageTrackingLog.TrackExpiredMessageDropped(MessageTrackingSource.BOOTLOADER, mailItem, mailItem.Recipients.AllUnprocessed.ToList<MailRecipient>(), AckReason.MessageNotActive);
				return;
			}
			ExTraceGlobals.StorageTracer.TraceDebug<string, string>(0L, "Bootscanner handling item {0} with subject {1}", mailItem.InternetMessageId, mailItem.Subject);
			this.TrackLoadedMessage(mailItem);
			if (mailItem.IsHeartbeat)
			{
				ExTraceGlobals.StorageTracer.TraceDebug(mailItem.MsgId, "Bootloader found a heartbeat message.");
				this.SendToShadowQueue(mailItem);
				return;
			}
			if (mailItem.IsDiscardPending)
			{
				this.SendToDiscardPendingQueue(mailItem);
				if (!mailItem.IsShadow())
				{
					return;
				}
			}
			if (mailItem.IsShadow())
			{
				this.ProcessShadowItem(mailItem);
				return;
			}
			this.ProcessMessageForSubmission(mailItem);
		}

		// Token: 0x06000B68 RID: 2920 RVA: 0x00028070 File Offset: 0x00026270
		private void Start()
		{
			if (this.thread != null)
			{
				if (!this.stopRequested)
				{
					return;
				}
				this.thread.Join();
				this.thread = null;
			}
			this.stopRequested = false;
			if (this.workCompleted)
			{
				return;
			}
			this.thread = new Thread(new ThreadStart(this.BackgroundScanner));
			this.thread.Start();
		}

		// Token: 0x06000B69 RID: 2921 RVA: 0x000280D6 File Offset: 0x000262D6
		private bool ShouldExecute()
		{
			return this.targetRunningState == ServiceState.Active || this.targetRunningState == ServiceState.Draining;
		}

		// Token: 0x06000B6A RID: 2922 RVA: 0x000280EC File Offset: 0x000262EC
		private void Stop(bool async)
		{
			this.stopRequested = true;
			if (!async && this.thread != null)
			{
				this.thread.Join();
				this.thread = null;
			}
		}

		// Token: 0x06000B6B RID: 2923 RVA: 0x00028114 File Offset: 0x00026314
		private void ProcessMessageForSubmission(TransportMailItem categorizerMailItem)
		{
			DateTime mailItemExpiry = this.GetMailItemExpiry(categorizerMailItem);
			if (DateTime.UtcNow > mailItemExpiry + this.configuration.MessageDropTimeout)
			{
				List<MailRecipient> list = categorizerMailItem.Recipients.AllUnprocessed.ToList<MailRecipient>();
				foreach (MailRecipient mailRecipient in list)
				{
					mailRecipient.Ack(AckStatus.SuccessNoDsn, AckReason.MessageTooOld);
				}
				categorizerMailItem.ReleaseFromActive();
				categorizerMailItem.CommitLazy();
				ExTraceGlobals.StorageTracer.TraceDebug<string, DateTime>(0L, "Bootscanner dropped expired mail item {0} with expiry time {1}", categorizerMailItem.InternetMessageId, mailItemExpiry);
				MessageTrackingLog.TrackExpiredMessageDropped(MessageTrackingSource.BOOTLOADER, categorizerMailItem, list, AckReason.MessageTooOld);
				return;
			}
			if (mailItemExpiry < DateTime.UtcNow)
			{
				this.ExtendMailItemExpiry(categorizerMailItem);
			}
			this.SendToSubmissionQueue(categorizerMailItem);
		}

		// Token: 0x06000B6C RID: 2924 RVA: 0x000281EC File Offset: 0x000263EC
		private void ProcessShadowItem(TransportMailItem mailItem)
		{
			TransportMailItem transportMailItem = null;
			if (mailItem.Status != Status.Complete)
			{
				transportMailItem = this.MoveUndeliveredRecipientsToNewClone(mailItem);
			}
			ExTraceGlobals.StorageTracer.TraceDebug<string>(mailItem.MsgId, "Bootloader found a {0} message.", mailItem.IsHeartbeat ? "heartbeat" : "shadow");
			this.SendToShadowQueue(mailItem);
			if (transportMailItem != null)
			{
				ExTraceGlobals.StorageTracer.TraceDebug<long>(mailItem.MsgId, "Bootloader loaded an hybrid shadow message and moved the undelivered items to a new message with id {0}.", transportMailItem.MsgId);
				this.ProcessMessageForSubmission(transportMailItem);
			}
		}

		// Token: 0x06000B6D RID: 2925 RVA: 0x00028261 File Offset: 0x00026461
		private void TrackLoadedMessage(TransportMailItem mailItem)
		{
			if (this.lastMessagesLoaded.Count == 10)
			{
				this.lastMessagesLoaded.Dequeue();
			}
			this.lastMessagesLoaded.Enqueue(mailItem);
		}

		// Token: 0x06000B6E RID: 2926 RVA: 0x0002828C File Offset: 0x0002648C
		private void AddLoadedMessageToReport(TransportMailItem mailItem, StringBuilder sb)
		{
			sb.AppendFormat("From: {0}", mailItem.Message.From.SmtpAddress);
			sb.AppendLine();
			StringBuilder stringBuilder = new StringBuilder();
			foreach (MailRecipient mailRecipient in mailItem.Recipients)
			{
				stringBuilder.AppendFormat("{0}, ", mailRecipient.Email.ToString());
			}
			if (stringBuilder.Length > 1)
			{
				stringBuilder.Remove(stringBuilder.Length - 2, 2);
			}
			sb.AppendFormat("To: {0}", stringBuilder);
			sb.AppendLine();
			sb.AppendFormat("Subject: {0}", mailItem.Message.Subject);
			sb.AppendLine();
			sb.AppendFormat("Message ID: {0}", mailItem.Message.MessageId);
			sb.AppendLine();
		}

		// Token: 0x06000B6F RID: 2927 RVA: 0x00028384 File Offset: 0x00026584
		private string CreateLoadedMessagesReport()
		{
			if (this.lastMessagesLoaded.Count > 0)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendLine();
				stringBuilder.AppendLine();
				foreach (TransportMailItem transportMailItem in this.lastMessagesLoaded)
				{
					if (transportMailItem.IsActive)
					{
						this.AddLoadedMessageToReport(transportMailItem, stringBuilder);
						stringBuilder.AppendLine();
					}
				}
				return stringBuilder.ToString();
			}
			return null;
		}

		// Token: 0x06000B70 RID: 2928 RVA: 0x00028414 File Offset: 0x00026614
		private void EventLogLoadedMessages(string loadedMessagesReport)
		{
			if (!string.IsNullOrEmpty(loadedMessagesReport))
			{
				this.eventLogger.LogEvent(TransportEventLogConstants.Tuple_LastMessagesLoadedByBootScanner, null, new object[]
				{
					loadedMessagesReport
				});
			}
		}

		// Token: 0x06000B71 RID: 2929 RVA: 0x0002844C File Offset: 0x0002664C
		private void BackgroundScanner()
		{
			int num = 0;
			int num2 = 0;
			this.CacheUnprocessedMessageIds();
			IMExSession session = this.CreateMExSession();
			string text;
			string text2;
			this.GetMessageCountDetails(out text, out text2);
			this.eventLogger.LogEvent(TransportEventLogConstants.Tuple_StartScanForMessages, null, new object[]
			{
				text2
			});
			ExTraceGlobals.StorageTracer.TraceDebug<string>(0L, "Starting to scan for active messages.{0}", text2);
			Stopwatch stopwatch = Stopwatch.StartNew();
			foreach (byte b in from i in this.unprocessedMessageIds.Keys
			orderby i
			select i)
			{
				ExTraceGlobals.StorageTracer.TraceDebug<byte>(0L, "Starting to scan for active messages priority {0}.", b);
				foreach (MailItemAndRecipients mailItemAndRecipients in this.database.GetMessages(this.unprocessedMessageIds[b]))
				{
					TransportMailItem transportMailItem = TransportMailItem.NewMailItem(mailItemAndRecipients.MailItem, LatencyComponent.ServiceRestart);
					foreach (IMailRecipientStorage recipStorage in mailItemAndRecipients.Recipients)
					{
						transportMailItem.AddRecipient(recipStorage);
					}
					stopwatch.Stop();
					if (this.configuration.BootLoaderMessageTrackingEnabled)
					{
						MessageTrackingLog.TrackLoadedMessage(MessageTrackingSource.BOOTLOADER, MessageTrackingEvent.LOAD, transportMailItem);
					}
					bool newPoisonMessage;
					if (this.IsPoison(transportMailItem, out newPoisonMessage))
					{
						this.HandlePoison(transportMailItem, newPoisonMessage);
						num++;
						if (this.configuration.PoisonCountPublishingEnabled)
						{
							this.recentPoisonMessagesCounter.AddEventsAt(transportMailItem.DateReceived, 1L);
						}
					}
					else
					{
						this.HandleStrandedMessage(transportMailItem, session);
					}
					num2++;
					this.perfCounters.BootloaderOutstandingItems.Decrement();
					this.perfCounters.BootloadedItemCount.Increment();
					this.perfCounters.BootloadedItemAverageLatency.IncrementBy(stopwatch.ElapsedTicks);
					this.perfCounters.BootloadedItemAverageLatencyBase.Increment();
					stopwatch.Restart();
					if (this.stopRequested)
					{
						break;
					}
				}
				this.GetMessageCountDetails(out text, out text2);
				ExTraceGlobals.StorageTracer.TraceDebug<byte, string, string>(0L, "Finished scan for active messages priority {0}. Processed({1}), Unprocessed({2})", b, text, text2);
				this.eventLogger.LogEvent(this.stopRequested ? TransportEventLogConstants.Tuple_StopScanForMessages : TransportEventLogConstants.Tuple_EndScanForMessages, null, new object[]
				{
					text,
					text2
				});
				if (this.stopRequested)
				{
					break;
				}
			}
			this.CloseMExSession(session);
			ExTraceGlobals.StorageTracer.TraceDebug<string, int, int>(0L, "Bootscanner {0}. {1} items completed; {2} poison.", this.stopRequested ? "was stopped" : "has completed", num2, num);
			this.LogPoisonMessageCount(num);
			this.workCompleted = !this.stopRequested;
			if (this.workCompleted)
			{
				if (this.configuration.PoisonCountPublishingEnabled)
				{
					this.StartPoisonCountTimer();
				}
				this.OnBootLoadCompleted();
			}
		}

		// Token: 0x06000B72 RID: 2930 RVA: 0x0002879C File Offset: 0x0002699C
		private void CacheUnprocessedMessageIds()
		{
			if (this.unprocessedMessageIds == null)
			{
				MessagingDatabaseResultStatus messagingDatabaseResultStatus = this.database.ReadUnprocessedMessageIds(out this.unprocessedMessageIds);
				if (messagingDatabaseResultStatus != MessagingDatabaseResultStatus.Complete)
				{
					throw new InvalidOperationException("Could not get complete list of unprocessed message Ids");
				}
				this.perfCounters.BootloaderOutstandingItems.RawValue = (long)this.unprocessedMessageIds.Values.Sum((List<long> i) => i.Count);
			}
		}

		// Token: 0x06000B73 RID: 2931 RVA: 0x000288B4 File Offset: 0x00026AB4
		private void GetMessageCountDetails(out string processedCountDescription, out string unprocessedCountDescription)
		{
			Dictionary<byte, int> dictionary = this.unprocessedMessageIds.ToDictionary((KeyValuePair<byte, List<long>> o) => o.Key, (KeyValuePair<byte, List<long>> o) => o.Value.Count((long msgId) => msgId == 0L));
			processedCountDescription = string.Format("{0}({1})", dictionary.Values.Sum(), string.Join(",", from i in dictionary
			select string.Format("P{0}={1}", i.Key, i.Value)));
			unprocessedCountDescription = string.Format("{0}({1})", this.unprocessedMessageIds.Values.Sum((List<long> o) => o.Count) - dictionary.Values.Sum(), string.Join(",", from pc in dictionary
			select string.Format("P{0}={1}", pc.Key, this.unprocessedMessageIds[pc.Key].Count - pc.Value)));
		}

		// Token: 0x06000B74 RID: 2932 RVA: 0x000289B6 File Offset: 0x00026BB6
		protected virtual void StartPoisonCountTimer()
		{
			this.poisonMessageCountTimer = new GuardedTimer(new TimerCallback(this.PublishPoisonCount), null, TimeSpan.FromMinutes(5.0));
		}

		// Token: 0x06000B75 RID: 2933 RVA: 0x000289DE File Offset: 0x00026BDE
		protected void PublishPoisonCount(object state)
		{
			this.perfCounters.BootloadedRecentPoisonMessageCount.RawValue = this.recentPoisonMessagesCounter.TimedUpdate();
		}

		// Token: 0x040004D5 RID: 1237
		private const int MaxLoadedMessagesToTrack = 10;

		// Token: 0x040004D6 RID: 1238
		protected IMessagingDatabase database;

		// Token: 0x040004D7 RID: 1239
		private readonly DatabasePerfCountersInstance perfCounters = DatabasePerfCounters.GetInstance("other");

		// Token: 0x040004D8 RID: 1240
		private Thread thread;

		// Token: 0x040004D9 RID: 1241
		private volatile bool stopRequested;

		// Token: 0x040004DA RID: 1242
		private ExEventLog eventLogger;

		// Token: 0x040004DB RID: 1243
		private ShadowRedundancyComponent shadowRedundancyComponent;

		// Token: 0x040004DC RID: 1244
		private PoisonMessage poisonComponent;

		// Token: 0x040004DD RID: 1245
		private QueueManager queueManagerComponent;

		// Token: 0x040004DE RID: 1246
		private ICategorizer categorizerComponent;

		// Token: 0x040004DF RID: 1247
		private bool workCompleted;

		// Token: 0x040004E0 RID: 1248
		private Dictionary<byte, List<long>> unprocessedMessageIds;

		// Token: 0x040004E1 RID: 1249
		private Queue<TransportMailItem> lastMessagesLoaded = new Queue<TransportMailItem>();

		// Token: 0x040004E2 RID: 1250
		private ServiceState targetRunningState;

		// Token: 0x040004E3 RID: 1251
		private IBootLoaderConfig configuration;

		// Token: 0x040004E4 RID: 1252
		private SegmentedSlidingCounter recentPoisonMessagesCounter;

		// Token: 0x040004E5 RID: 1253
		private GuardedTimer poisonMessageCountTimer;
	}
}
