using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Notifications.Broker;
using Microsoft.Exchange.Threading;

namespace Microsoft.Exchange.Notifications.Broker
{
	// Token: 0x0200000C RID: 12
	internal class Generator : DisposeTrackableBase, IGenerator
	{
		// Token: 0x06000060 RID: 96 RVA: 0x00003478 File Offset: 0x00001678
		private Generator()
		{
			this.mailboxManagers = new Dictionary<string, MailboxNotificationManager>();
			this.subscriptionStore = BrokerSubscriptionStore.Instance;
			this.mailboxManangerCleanupTimer = new GuardedTimer(new TimerCallback(this.HandleMailboxManagerCleanupTimer), null, Generator.MailboxManagerCleanupTimerInterval, Generator.MailboxManagerCleanupTimerInterval);
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000061 RID: 97 RVA: 0x000034CE File Offset: 0x000016CE
		public static Generator Singleton
		{
			get
			{
				return Generator.singleton;
			}
		}

		// Token: 0x06000062 RID: 98 RVA: 0x000034D8 File Offset: 0x000016D8
		public void Subscribe(BrokerSubscription brokerSubscription)
		{
			ExTraceGlobals.GeneratorTracer.TraceFunction<Guid>((long)this.GetHashCode(), "Subscribe begin: {0}", brokerSubscription.SubscriptionId);
			this.CreateMapiNotificationHandler(brokerSubscription);
			this.subscriptionStore.Subscribe(brokerSubscription);
			ExTraceGlobals.GeneratorTracer.TraceFunction<Guid>((long)this.GetHashCode(), "Subscribe end: {0}", brokerSubscription.SubscriptionId);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003530 File Offset: 0x00001730
		public void CreateMapiNotificationHandler(BrokerSubscription brokerSubscription)
		{
			Generator.ThrowIfInvalidSubscription(brokerSubscription);
			MailboxNotificationManager mailboxManager = this.GetMailboxManager(brokerSubscription);
			brokerSubscription.Handler = mailboxManager.Subscribe(brokerSubscription);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003558 File Offset: 0x00001758
		public void Unsubscribe(BrokerSubscription brokerSubscription)
		{
			ExTraceGlobals.GeneratorTracer.TraceDebug<Guid>((long)this.GetHashCode(), "Unsubscribe begin: {0}", brokerSubscription.SubscriptionId);
			this.subscriptionStore.Unsubscribe(brokerSubscription);
			ExTraceGlobals.GeneratorTracer.TraceDebug<Guid>((long)this.GetHashCode(), "Unsubscribe end: {0}", brokerSubscription.SubscriptionId);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x000035A9 File Offset: 0x000017A9
		public bool MailboxIsHostedLocally(Guid mailboxGuid)
		{
			return this.subscriptionStore.MailboxIsHostedLocally(mailboxGuid);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x000035B7 File Offset: 0x000017B7
		public bool MailboxIsHostedLocally(Guid databaseGuid, Guid mailboxGuid)
		{
			return this.subscriptionStore.MailboxIsHostedLocally(databaseGuid, mailboxGuid);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x000035C8 File Offset: 0x000017C8
		public SubscriptionsForHandlersSnapshot GetSubscriptionsForHandlerDiagnostics(Guid mdbGuid, Guid mbxGuid, string handlerName)
		{
			SubscriptionsForHandlersSnapshot subscriptionsForHandlersSnapshot = new SubscriptionsForHandlersSnapshot();
			Dictionary<string, SubscriptionsForHandlersSnapshot.SubscriptionsForHandlerSnapshot> dictionary = new Dictionary<string, SubscriptionsForHandlersSnapshot.SubscriptionsForHandlerSnapshot>();
			foreach (KeyValuePair<Guid, BrokerDatabaseData> keyValuePair in this.subscriptionStore.Databases)
			{
				BrokerDatabaseData value = keyValuePair.Value;
				if ((mdbGuid == Guid.Empty || mdbGuid == value.DatabaseGuid) && value.MailboxData != null)
				{
					foreach (KeyValuePair<Guid, BrokerMailboxData> keyValuePair2 in value.MailboxData)
					{
						BrokerMailboxData value2 = keyValuePair2.Value;
						if ((mbxGuid == Guid.Empty || mbxGuid == value2.MailboxGuid) && value2.Subscriptions != null)
						{
							foreach (KeyValuePair<Guid, BrokerSubscription> keyValuePair3 in value2.Subscriptions)
							{
								BrokerSubscription value3 = keyValuePair3.Value;
								string name = value3.Handler.GetType().Name;
								if (handlerName == null || name.Equals(handlerName, StringComparison.OrdinalIgnoreCase))
								{
									SubscriptionsForHandlersSnapshot.SubscriptionsForHandlerSnapshot subscriptionsForHandlerSnapshot;
									if (!dictionary.TryGetValue(name, out subscriptionsForHandlerSnapshot))
									{
										subscriptionsForHandlerSnapshot = new SubscriptionsForHandlersSnapshot.SubscriptionsForHandlerSnapshot();
										subscriptionsForHandlerSnapshot.Handler = name;
										subscriptionsForHandlerSnapshot.Subscriptions = new List<BrokerSubscription>();
										dictionary[name] = subscriptionsForHandlerSnapshot;
									}
									subscriptionsForHandlerSnapshot.SubscriptionCount++;
									if (handlerName != null || mbxGuid != Guid.Empty)
									{
										subscriptionsForHandlerSnapshot.Subscriptions.Add(value3);
									}
								}
							}
						}
					}
				}
			}
			if (dictionary.Count > 0)
			{
				subscriptionsForHandlersSnapshot.Handlers = new List<SubscriptionsForHandlersSnapshot.SubscriptionsForHandlerSnapshot>(dictionary.Count);
				foreach (KeyValuePair<string, SubscriptionsForHandlersSnapshot.SubscriptionsForHandlerSnapshot> keyValuePair4 in dictionary)
				{
					subscriptionsForHandlersSnapshot.Handlers.Add(keyValuePair4.Value);
				}
			}
			return subscriptionsForHandlersSnapshot;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003830 File Offset: 0x00001A30
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				if (this.mailboxManangerCleanupTimer != null)
				{
					this.mailboxManangerCleanupTimer.Dispose(true);
					this.mailboxManangerCleanupTimer = null;
				}
				lock (this.syncRoot)
				{
					if (this.mailboxManagers != null)
					{
						foreach (MailboxNotificationManager mailboxNotificationManager in this.mailboxManagers.Values)
						{
							mailboxNotificationManager.Dispose();
						}
						this.mailboxManagers.Clear();
						this.mailboxManagers = null;
					}
				}
			}
		}

		// Token: 0x06000069 RID: 105 RVA: 0x000038EC File Offset: 0x00001AEC
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<Generator>(this);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x000038F4 File Offset: 0x00001AF4
		private static void ThrowIfInvalidSubscription(BrokerSubscription brokerSubscription)
		{
			if (!brokerSubscription.IsValid)
			{
				throw new InvalidBrokerSubscriptionException(brokerSubscription.ToJson());
			}
		}

		// Token: 0x0600006B RID: 107 RVA: 0x0000390C File Offset: 0x00001B0C
		private static string GetManagerMapString(BrokerSubscription brokerSubscription)
		{
			return string.Concat(new object[]
			{
				"M:",
				brokerSubscription.Sender.MailboxGuid,
				"-L:",
				brokerSubscription.Parameters.CultureInfo.Name
			});
		}

		// Token: 0x0600006C RID: 108 RVA: 0x0000395C File Offset: 0x00001B5C
		private MailboxNotificationManager GetMailboxManager(BrokerSubscription brokerSubscription)
		{
			MailboxNotificationManager result;
			lock (this.syncRoot)
			{
				MailboxNotificationManager mailboxNotificationManager = null;
				string managerMapString = Generator.GetManagerMapString(brokerSubscription);
				if (!this.mailboxManagers.TryGetValue(managerMapString, out mailboxNotificationManager))
				{
					mailboxNotificationManager = (this.mailboxManagers[managerMapString] = new MailboxNotificationManager(brokerSubscription));
				}
				result = mailboxNotificationManager;
			}
			return result;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003A2C File Offset: 0x00001C2C
		private void HandleMailboxManagerCleanupTimer(object state)
		{
			ExTraceGlobals.GeneratorTracer.TraceDebug((long)this.GetHashCode(), "HandleMailboxManagerCleanupTimer start");
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			StringBuilder stringBuilder = new StringBuilder();
			using (BrokerLogger.StartEvent("MailboxManagerCleanupTimer"))
			{
				try
				{
					try
					{
						GrayException.MapAndReportGrayExceptions(new GrayException.UserCodeDelegate(this.subscriptionStore.CleanupExpiredSubscriptions));
					}
					catch (GrayException arg)
					{
						stringBuilder.AppendFormat("[CleanupExpiredSubscriptions Failed - {0}]", arg);
					}
					num2 = this.CleanupHandlersInManagers();
					Exception exception = null;
					using (IEnumerator<MailboxNotificationManager> enumerator = this.GetInactiveMailboxManagers().GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							MailboxNotificationManager manager = enumerator.Current;
							try
							{
								num3++;
								GrayException.MapAndReportGrayExceptions(delegate()
								{
									try
									{
										manager.Dispose();
									}
									catch (StoragePermanentException exception2)
									{
										exception = exception2;
									}
									catch (StorageTransientException exception3)
									{
										exception = exception3;
									}
								});
							}
							catch (GrayException exception)
							{
								GrayException exception4;
								exception = exception4;
							}
							finally
							{
								if (exception != null)
								{
									num++;
									stringBuilder.AppendFormat("[{0}]", exception);
									ExTraceGlobals.GeneratorTracer.TraceError<Exception>((long)this.GetHashCode(), "Caught an exception during HandleMailboxManagerCleanupTimer. Exception - {0}", exception);
								}
							}
						}
					}
				}
				finally
				{
					BrokerLogger.Set(LogField.ManagerTotalCount, num2);
					BrokerLogger.Set(LogField.ManagerCleanupCount, num3);
					BrokerLogger.Set(LogField.ManagerFailureCount, num);
					BrokerLogger.Set(LogField.Exception, stringBuilder);
					ExTraceGlobals.GeneratorTracer.TraceDebug<int, int>((long)this.GetHashCode(), "HandleMailboxManagerCleanupTimer End: ManagerTotalCount - {0} : ManagerFailureCount - {1}", num2, num);
				}
			}
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003C4C File Offset: 0x00001E4C
		private int CleanupHandlersInManagers()
		{
			MailboxNotificationManager[] array;
			lock (this.syncRoot)
			{
				array = this.mailboxManagers.Values.ToArray<MailboxNotificationManager>();
			}
			foreach (MailboxNotificationManager mailboxNotificationManager in array)
			{
				mailboxNotificationManager.CleanupInactiveHandlers();
			}
			return array.Length;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003CEC File Offset: 0x00001EEC
		private IEnumerable<MailboxNotificationManager> GetInactiveMailboxManagers()
		{
			IEnumerable<MailboxNotificationManager> result;
			lock (this.syncRoot)
			{
				result = (from kvp in this.mailboxManagers
				where kvp.Value.HandlerCount == 0
				select kvp).ToArray<KeyValuePair<string, MailboxNotificationManager>>().Select(delegate(KeyValuePair<string, MailboxNotificationManager> kvp)
				{
					this.mailboxManagers.Remove(kvp.Key);
					return kvp.Value;
				});
			}
			return result;
		}

		// Token: 0x0400003E RID: 62
		private static readonly TimeSpan MailboxManagerCleanupTimerInterval = TimeSpan.FromMinutes(1.0);

		// Token: 0x0400003F RID: 63
		private static readonly Generator singleton = new Generator();

		// Token: 0x04000040 RID: 64
		private readonly object syncRoot = new object();

		// Token: 0x04000041 RID: 65
		private readonly BrokerSubscriptionStore subscriptionStore;

		// Token: 0x04000042 RID: 66
		private Dictionary<string, MailboxNotificationManager> mailboxManagers;

		// Token: 0x04000043 RID: 67
		private GuardedTimer mailboxManangerCleanupTimer;
	}
}
