using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Common
{
	// Token: 0x02000066 RID: 102
	public class EventLogNotification : IDisposable
	{
		// Token: 0x06000361 RID: 865 RVA: 0x00016F04 File Offset: 0x00015104
		private EventLogNotification()
		{
			TimerCallback callback = new TimerCallback(this.SendPeriodicGreenEvents);
			this.greenResetTimer = new Timer(callback, null, EventLogNotification.TimerInterval, EventLogNotification.TimerInterval);
			new EventLogNotification.EventNotificationMetadata
			{
				ServiceName = "EventLogNofiticationDiag",
				ComponentName = "InstanceStart",
				TagName = null,
				Message = string.Format("Instance started at {0}", DateTime.UtcNow.ToString("o"))
			}.GenerateEventNotificationItem().Publish(false);
		}

		// Token: 0x06000362 RID: 866 RVA: 0x00016FF0 File Offset: 0x000151F0
		static EventLogNotification()
		{
			EventLogNotification.LogDebug("Instance initialized", new object[0]);
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000363 RID: 867 RVA: 0x00017033 File Offset: 0x00015233
		private static TimeSpan TimerInterval
		{
			get
			{
				if (EventLogNotification.timerInterval == TimeSpan.Zero)
				{
					EventLogNotification.timerInterval = TimeSpan.FromSeconds((double)RegistryHelper.GetProperty<int>("ProcessorTimerIntervalInSecs", 300, "EventProcessor", null, false));
				}
				return EventLogNotification.timerInterval;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000364 RID: 868 RVA: 0x0001706C File Offset: 0x0001526C
		public static EventLogNotification Instance
		{
			get
			{
				return EventLogNotification.instance;
			}
		}

		// Token: 0x06000365 RID: 869 RVA: 0x00017073 File Offset: 0x00015273
		public static string ConstructResultMask(string subscriptionName, string resourceName = null)
		{
			return NotificationItem.GenerateResultName("EventLogNotification", subscriptionName, string.IsNullOrWhiteSpace(resourceName) ? resourceName : resourceName.ToLower());
		}

		// Token: 0x06000366 RID: 870 RVA: 0x00017091 File Offset: 0x00015291
		private static void LogDebug(string pattern, params object[] arguments)
		{
			WTFDiagnostics.TraceInformation(ExTraceGlobals.CommonComponentsTracer, TracingContext.Default, pattern, arguments, null, "LogDebug", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Common\\EventLogNotification\\EventLogNotification.cs", 214);
		}

		// Token: 0x06000367 RID: 871 RVA: 0x000170EC File Offset: 0x000152EC
		private static string ConstructXPathForEventMatches(IEnumerable<EventLogNotification.EventMatchInternal> matches)
		{
			SortedSet<int> sortedSet = new SortedSet<int>();
			if (matches != null)
			{
				foreach (EventLogNotification.EventMatchInternal eventMatchInternal in matches)
				{
					foreach (int item in eventMatchInternal.EventMatch.EventIds)
					{
						sortedSet.Add(item);
					}
				}
			}
			if (sortedSet.Count < 1)
			{
				return string.Empty;
			}
			List<Tuple<int, int>> sparseRanges = EventLogNotification.SparseRange.GetSparseRanges(sortedSet.ToList<int>(), 6);
			List<string> filterString = new List<string>();
			sparseRanges.ForEach(delegate(Tuple<int, int> r)
			{
				filterString.Add(string.Format("(EventID >= {0} and EventID <= {1})", r.Item1, r.Item2));
			});
			return string.Format("*[System[{0}]]", string.Join(" or ", filterString));
		}

		// Token: 0x06000368 RID: 872 RVA: 0x000171F0 File Offset: 0x000153F0
		public void AddSubscription(EventLogSubscription subscription)
		{
			this.CheckDisposed(true);
			string contentHash = subscription.GetContentHash();
			bool flag = false;
			try
			{
				this.rwLockForSubscription.EnterUpgradeableReadLock();
				flag = true;
				bool flag2 = false;
				if (this.subscriptionHashcodes.ContainsKey(subscription.Name))
				{
					if (this.subscriptionHashcodes[subscription.Name].Equals(contentHash))
					{
						EventLogNotification.LogDebug("Subscription named '{0}' already exists. Hashcode={1}", new object[]
						{
							subscription.Name,
							contentHash
						});
						return;
					}
					EventLogNotification.LogDebug("Subscription of same name encountered, replacing existing one... Name={0}", new object[]
					{
						subscription.Name
					});
					flag2 = true;
				}
				HashSet<string> hashSet = new HashSet<string>();
				bool flag3 = false;
				try
				{
					this.rwLockForSubscription.EnterWriteLock();
					flag3 = true;
					this.subscriptionHashcodes[subscription.Name] = contentHash;
					this.allSubscriptionObjects.AddOrUpdate(subscription.Name, subscription, (string key, EventLogSubscription oldValue) => subscription);
					EventLogNotification.EventMatchInternal[] array = EventLogNotification.EventMatchInternal.ConstructFromSubscription(subscription);
					EventLogNotification.SubscriptionMetadata value = EventLogNotification.SubscriptionMetadata.ConstructFromSubscription(subscription);
					if (flag2)
					{
						foreach (KeyValuePair<string, LinkedList<EventLogNotification.EventMatchInternal>> keyValuePair in this.eventMatchByLogName)
						{
							LinkedList<EventLogNotification.EventMatchInternal> value2 = keyValuePair.Value;
							LinkedListNode<EventLogNotification.EventMatchInternal> next;
							for (LinkedListNode<EventLogNotification.EventMatchInternal> linkedListNode = value2.First; linkedListNode != null; linkedListNode = next)
							{
								next = linkedListNode.Next;
								if (linkedListNode.Value != null && linkedListNode.Value.SubscriptionName.Equals(subscription.Name))
								{
									value2.Remove(linkedListNode);
								}
							}
						}
					}
					this.metadataByName[subscription.Name] = value;
					foreach (EventLogNotification.EventMatchInternal eventMatchInternal in array)
					{
						if (eventMatchInternal != null)
						{
							hashSet.Add(eventMatchInternal.EventMatch.LogName);
							LinkedList<EventLogNotification.EventMatchInternal> linkedList = null;
							if (!this.eventMatchByLogName.TryGetValue(eventMatchInternal.EventMatch.LogName, out linkedList))
							{
								linkedList = new LinkedList<EventLogNotification.EventMatchInternal>();
								this.eventMatchByLogName.Add(eventMatchInternal.EventMatch.LogName, linkedList);
							}
							linkedList.AddLast(eventMatchInternal);
						}
					}
				}
				finally
				{
					if (flag3)
					{
						this.rwLockForSubscription.ExitWriteLock();
					}
				}
				this.RefreshWatchers(hashSet);
			}
			finally
			{
				if (flag)
				{
					this.rwLockForSubscription.ExitUpgradeableReadLock();
				}
			}
		}

		// Token: 0x06000369 RID: 873 RVA: 0x000174DC File Offset: 0x000156DC
		public void Dispose()
		{
			if (this.isDisposed)
			{
				return;
			}
			EventLogNotification.LogDebug("Dispose begins.", new object[0]);
			bool flag = false;
			try
			{
				this.rwLockForEventWatchers.EnterWriteLock();
				flag = true;
				if (this.eventWatchersByLogName != null)
				{
					foreach (KeyValuePair<string, EventLogWatcher> keyValuePair in this.eventWatchersByLogName)
					{
						if (keyValuePair.Value != null)
						{
							keyValuePair.Value.Dispose();
						}
					}
				}
			}
			finally
			{
				if (flag)
				{
					this.rwLockForEventWatchers.ExitWriteLock();
				}
			}
			if (this.greenResetTimer != null)
			{
				this.greenResetTimer.Dispose();
			}
			lock (this.disposeLock)
			{
				this.isDisposed = true;
			}
			EventLogNotification.LogDebug("Dispose finished.", new object[0]);
		}

		// Token: 0x0600036A RID: 874 RVA: 0x000175E0 File Offset: 0x000157E0
		private void RefreshWatchers(IEnumerable<string> logNames)
		{
			if (logNames != null && logNames.Any<string>())
			{
				bool flag = false;
				try
				{
					this.rwLockForEventWatchers.EnterWriteLock();
					flag = true;
					foreach (string text in logNames)
					{
						EventLogWatcher eventLogWatcher = null;
						if (this.eventWatchersByLogName.TryGetValue(text, out eventLogWatcher) && eventLogWatcher != null)
						{
							eventLogWatcher.Enabled = false;
							eventLogWatcher.Dispose();
						}
						eventLogWatcher = this.CreateWatcher(text);
						if (eventLogWatcher != null)
						{
							eventLogWatcher.Enabled = true;
						}
						this.eventWatchersByLogName[text] = eventLogWatcher;
					}
				}
				finally
				{
					if (flag)
					{
						this.rwLockForEventWatchers.ExitWriteLock();
					}
				}
			}
		}

		// Token: 0x0600036B RID: 875 RVA: 0x000176A0 File Offset: 0x000158A0
		private EventLogWatcher CreateWatcher(string logName)
		{
			LinkedList<EventLogNotification.EventMatchInternal> matches = null;
			if (!this.eventMatchByLogName.TryGetValue(logName, out matches))
			{
				EventLogNotification.LogDebug("CreateWatcher: Unable to get any matches by logname {0}", new object[]
				{
					logName
				});
				return null;
			}
			string text = EventLogNotification.ConstructXPathForEventMatches(matches);
			EventLogNotification.LogDebug("CreateWatcher: XPath for logname {0}={1}", new object[]
			{
				logName,
				string.IsNullOrWhiteSpace(text) ? "NULL" : text
			});
			if (!string.IsNullOrWhiteSpace(text))
			{
				EventLogWatcher eventLogWatcher = new EventLogWatcher(new EventLogQuery(logName, PathType.LogName, text));
				eventLogWatcher.EventRecordWritten += this.watcherEventRecordWritten;
				return eventLogWatcher;
			}
			EventLogNotification.LogDebug("CreateWatcher: XPath is empty for logName {0}", new object[]
			{
				logName
			});
			return null;
		}

		// Token: 0x0600036C RID: 876 RVA: 0x00017750 File Offset: 0x00015950
		private void watcherEventRecordWritten(object sender, EventRecordWrittenEventArgs e)
		{
			this.CheckDisposed(true);
			if (e.EventRecord != null)
			{
				using (e.EventRecord)
				{
					EventLogNotification.EventRecordInternal eventRecordInternal = EventLogNotification.EventRecordInternal.ConstructFromEventRecord(e.EventRecord);
					List<EventLogNotification.EventMatchInternal> list = new List<EventLogNotification.EventMatchInternal>();
					bool flag = false;
					try
					{
						this.rwLockForSubscription.EnterReadLock();
						flag = true;
						LinkedList<EventLogNotification.EventMatchInternal> linkedList = null;
						if (!this.eventMatchByLogName.TryGetValue(eventRecordInternal.LogName, out linkedList))
						{
							EventLogNotification.LogDebug("EventRecordWritten: No matches for LogName={0}", new object[]
							{
								eventRecordInternal.LogName
							});
							return;
						}
						if (linkedList != null && linkedList.Count > 0)
						{
							list.AddRange(linkedList);
						}
					}
					finally
					{
						if (flag)
						{
							this.rwLockForSubscription.ExitReadLock();
						}
					}
					if (list != null && list.Count > 0)
					{
						foreach (EventLogNotification.EventMatchInternal eventMatchInternal in list)
						{
							if (eventMatchInternal.IsMatch(eventRecordInternal))
							{
								EventLogNotification.EventNotificationMetadata notification = eventMatchInternal.GetNotification(eventRecordInternal);
								this.SendAndRecordNotification(notification);
								Interlocked.Increment(ref this.eventNotificationSent);
								EventLogSubscription eventLogSubscription = null;
								if (this.allSubscriptionObjects.TryGetValue(eventMatchInternal.SubscriptionName, out eventLogSubscription) && eventLogSubscription != null)
								{
									if (notification.IsCritical && eventLogSubscription.OnRedEvents != null)
									{
										eventLogSubscription.OnRedEvents(eventRecordInternal, notification);
									}
									else if (!notification.IsCritical && eventLogSubscription.OnGreenEvents != null)
									{
										eventLogSubscription.OnGreenEvents(eventRecordInternal, notification);
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x0600036D RID: 877 RVA: 0x00017920 File Offset: 0x00015B20
		private void SendAndRecordNotification(EventLogNotification.EventNotificationMetadata enm)
		{
			string key = string.Format("{0}|{1}|{2}", enm.ServiceName, enm.ComponentName, enm.TagName);
			if (enm.IsCritical)
			{
				this.notificationRecord[key] = DateTime.UtcNow;
			}
			else
			{
				this.notificationRecord[key] = DateTime.MinValue;
			}
			enm.GenerateEventNotificationItem().Publish(false);
		}

		// Token: 0x0600036E RID: 878 RVA: 0x00017984 File Offset: 0x00015B84
		private bool CheckDisposed(bool doThrow = true)
		{
			lock (this.disposeLock)
			{
				if (this.isDisposed)
				{
					if (doThrow)
					{
						throw new ApplicationException("EventLogNotification is already disposed.");
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600036F RID: 879 RVA: 0x000179DC File Offset: 0x00015BDC
		private void SendPeriodicGreenEvents(object stateInfo)
		{
			if (this.CheckDisposed(false))
			{
				EventLogNotification.LogDebug("SendPeriodicGreenEvents: Current instance is disposed.", new object[0]);
				return;
			}
			DateTime utcNow = DateTime.UtcNow;
			if (this.notificationRecord != null && this.notificationRecord.Count > 0)
			{
				foreach (KeyValuePair<string, DateTime> keyValuePair in this.notificationRecord)
				{
					string[] array = keyValuePair.Key.Split(EventLogNotification.keySplitter, StringSplitOptions.RemoveEmptyEntries);
					EventLogNotification.SubscriptionMetadata subscriptionMetadata = null;
					if (array != null && array.Length > 0 && this.metadataByName.TryGetValue(array[0], out subscriptionMetadata) && keyValuePair.Value != DateTime.MinValue && subscriptionMetadata.AutoResetInterval != EventLogSubscription.NoAutoReset && utcNow - keyValuePair.Value >= subscriptionMetadata.AutoResetInterval)
					{
						this.SendAndRecordNotification(new EventLogNotification.EventNotificationMetadata
						{
							ServiceName = array[0],
							ComponentName = ((array.Length > 1) ? array[1] : null),
							TagName = ((array.Length > 2) ? array[2] : null),
							IsCritical = false,
							Message = "Auto Reset Green Event."
						});
					}
				}
			}
			new EventLogNotification.EventNotificationMetadata
			{
				ServiceName = "EventLogNofiticationDiag",
				ComponentName = "Heartbeat",
				TagName = null,
				Message = string.Format("Subscription count = {0}, LogName count monitored = {1}, Events Sent={2}", this.subscriptionHashcodes.Count, this.eventWatchersByLogName.Count, this.eventNotificationSent),
				StateAttribute3 = this.eventNotificationSent.ToString()
			}.GenerateEventNotificationItem().Publish(false);
		}

		// Token: 0x0400027D RID: 637
		private const string EventNotificationServiceName = "EventLogNotification";

		// Token: 0x0400027E RID: 638
		private const string EventNotificationServiceNameForDiag = "EventLogNofiticationDiag";

		// Token: 0x0400027F RID: 639
		private const string EventNotificationInstanceStart = "InstanceStart";

		// Token: 0x04000280 RID: 640
		private const string EventNotificationHeartbeatCompName = "Heartbeat";

		// Token: 0x04000281 RID: 641
		private const string EventResetSubkey = "EventProcessor";

		// Token: 0x04000282 RID: 642
		private const string EventResetIntervalValueName = "ProcessorTimerIntervalInSecs";

		// Token: 0x04000283 RID: 643
		private static TimeSpan timerInterval = TimeSpan.Zero;

		// Token: 0x04000284 RID: 644
		private static char[] keySplitter = new char[]
		{
			'|'
		};

		// Token: 0x04000285 RID: 645
		private static EventLogNotification instance = new EventLogNotification();

		// Token: 0x04000286 RID: 646
		private readonly object disposeLock = new object();

		// Token: 0x04000287 RID: 647
		private int eventNotificationSent;

		// Token: 0x04000288 RID: 648
		private bool isDisposed;

		// Token: 0x04000289 RID: 649
		private ReaderWriterLockSlim rwLockForSubscription = new ReaderWriterLockSlim();

		// Token: 0x0400028A RID: 650
		private Dictionary<string, string> subscriptionHashcodes = new Dictionary<string, string>();

		// Token: 0x0400028B RID: 651
		private Dictionary<string, LinkedList<EventLogNotification.EventMatchInternal>> eventMatchByLogName = new Dictionary<string, LinkedList<EventLogNotification.EventMatchInternal>>();

		// Token: 0x0400028C RID: 652
		private Dictionary<string, EventLogNotification.SubscriptionMetadata> metadataByName = new Dictionary<string, EventLogNotification.SubscriptionMetadata>();

		// Token: 0x0400028D RID: 653
		private ReaderWriterLockSlim rwLockForEventWatchers = new ReaderWriterLockSlim();

		// Token: 0x0400028E RID: 654
		private Dictionary<string, EventLogWatcher> eventWatchersByLogName = new Dictionary<string, EventLogWatcher>();

		// Token: 0x0400028F RID: 655
		private ConcurrentDictionary<string, DateTime> notificationRecord = new ConcurrentDictionary<string, DateTime>();

		// Token: 0x04000290 RID: 656
		private ConcurrentDictionary<string, EventLogSubscription> allSubscriptionObjects = new ConcurrentDictionary<string, EventLogSubscription>();

		// Token: 0x04000291 RID: 657
		private Timer greenResetTimer;

		// Token: 0x02000067 RID: 103
		public static class SparseRange
		{
			// Token: 0x06000370 RID: 880 RVA: 0x00017BE0 File Offset: 0x00015DE0
			public static List<Tuple<int, int>> GetSparseRanges(List<int> sortedSet, int maxRanges = 6)
			{
				List<EventLogNotification.SparseRange.Hole> list = new List<EventLogNotification.SparseRange.Hole>();
				List<Tuple<int, int>> list2 = new List<Tuple<int, int>>();
				if (sortedSet == null || sortedSet.Count < 1)
				{
					return new List<Tuple<int, int>>();
				}
				int num = sortedSet.FirstOrDefault<int>();
				int item = sortedSet.LastOrDefault<int>();
				if (sortedSet.Count <= 2)
				{
					list2.Add(new Tuple<int, int>(num, item));
					return list2;
				}
				for (int i = 0; i < sortedSet.Count - 1; i++)
				{
					if (sortedSet[i + 1] - sortedSet[i] > 1)
					{
						list.Add(new EventLogNotification.SparseRange.Hole
						{
							PreviousInteger = sortedSet[i],
							NextInteger = sortedSet[i + 1],
							HoleSize = sortedSet[i + 1] - sortedSet[i]
						});
					}
				}
				if (list.Count < 1)
				{
					list2.Add(new Tuple<int, int>(num, item));
					return list2;
				}
				list.Sort((EventLogNotification.SparseRange.Hole x, EventLogNotification.SparseRange.Hole y) => y.HoleSize.CompareTo(x.HoleSize));
				List<EventLogNotification.SparseRange.Hole> list3 = new List<EventLogNotification.SparseRange.Hole>();
				int num2 = 1;
				foreach (EventLogNotification.SparseRange.Hole item2 in list)
				{
					if (num2 > maxRanges - 1)
					{
						break;
					}
					list3.Add(item2);
					num2++;
				}
				list3.Sort((EventLogNotification.SparseRange.Hole x, EventLogNotification.SparseRange.Hole y) => x.PreviousInteger.CompareTo(y.PreviousInteger));
				int item3 = num;
				foreach (EventLogNotification.SparseRange.Hole hole in list3)
				{
					list2.Add(new Tuple<int, int>(item3, hole.PreviousInteger));
					item3 = hole.NextInteger;
				}
				list2.Add(new Tuple<int, int>(item3, item));
				return list2;
			}

			// Token: 0x02000068 RID: 104
			private class Hole
			{
				// Token: 0x04000294 RID: 660
				public int PreviousInteger;

				// Token: 0x04000295 RID: 661
				public int NextInteger;

				// Token: 0x04000296 RID: 662
				public int HoleSize;
			}
		}

		// Token: 0x02000069 RID: 105
		public class EventRecordInternal
		{
			// Token: 0x170000BE RID: 190
			// (get) Token: 0x06000374 RID: 884 RVA: 0x00017DD4 File Offset: 0x00015FD4
			// (set) Token: 0x06000375 RID: 885 RVA: 0x00017DDC File Offset: 0x00015FDC
			public string LogName { get; private set; }

			// Token: 0x170000BF RID: 191
			// (get) Token: 0x06000376 RID: 886 RVA: 0x00017DE5 File Offset: 0x00015FE5
			// (set) Token: 0x06000377 RID: 887 RVA: 0x00017DED File Offset: 0x00015FED
			public string ProviderName { get; private set; }

			// Token: 0x170000C0 RID: 192
			// (get) Token: 0x06000378 RID: 888 RVA: 0x00017DF6 File Offset: 0x00015FF6
			// (set) Token: 0x06000379 RID: 889 RVA: 0x00017DFE File Offset: 0x00015FFE
			public int Id { get; private set; }

			// Token: 0x170000C1 RID: 193
			// (get) Token: 0x0600037A RID: 890 RVA: 0x00017E07 File Offset: 0x00016007
			// (set) Token: 0x0600037B RID: 891 RVA: 0x00017E0F File Offset: 0x0001600F
			public EventRecord EventRecord { get; private set; }

			// Token: 0x0600037C RID: 892 RVA: 0x00017E18 File Offset: 0x00016018
			private EventRecordInternal()
			{
			}

			// Token: 0x0600037D RID: 893 RVA: 0x00017E20 File Offset: 0x00016020
			public static EventLogNotification.EventRecordInternal ConstructFromEventRecord(EventRecord record)
			{
				return new EventLogNotification.EventRecordInternal
				{
					EventRecord = record,
					LogName = record.LogName,
					ProviderName = record.ProviderName,
					Id = record.Id
				};
			}

			// Token: 0x0600037E RID: 894 RVA: 0x00017E60 File Offset: 0x00016060
			public string GeneratePropertyXml()
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendLine("<Properties>");
				IList<EventProperty> properties = this.EventRecord.Properties;
				if (properties != null)
				{
					for (int i = 0; i < properties.Count; i++)
					{
						stringBuilder.AppendFormat("<Property index={0}>{1}</Property>{2}", i, (properties[i].Value == null) ? "NULL" : properties[i].Value.ToString(), Environment.NewLine);
					}
				}
				stringBuilder.AppendLine("</Properties>");
				return stringBuilder.ToString();
			}
		}

		// Token: 0x0200006A RID: 106
		public class EventMatchInternal
		{
			// Token: 0x170000C2 RID: 194
			// (get) Token: 0x0600037F RID: 895 RVA: 0x00017EEE File Offset: 0x000160EE
			// (set) Token: 0x06000380 RID: 896 RVA: 0x00017EF6 File Offset: 0x000160F6
			public EventMatchingRule EventMatch { get; private set; }

			// Token: 0x170000C3 RID: 195
			// (get) Token: 0x06000381 RID: 897 RVA: 0x00017EFF File Offset: 0x000160FF
			// (set) Token: 0x06000382 RID: 898 RVA: 0x00017F07 File Offset: 0x00016107
			public bool IsRed { get; private set; }

			// Token: 0x170000C4 RID: 196
			// (get) Token: 0x06000383 RID: 899 RVA: 0x00017F10 File Offset: 0x00016110
			// (set) Token: 0x06000384 RID: 900 RVA: 0x00017F18 File Offset: 0x00016118
			public string SubscriptionName { get; private set; }

			// Token: 0x06000385 RID: 901 RVA: 0x00017F21 File Offset: 0x00016121
			private EventMatchInternal()
			{
			}

			// Token: 0x06000386 RID: 902 RVA: 0x00017F2C File Offset: 0x0001612C
			public static EventLogNotification.EventMatchInternal[] ConstructFromSubscription(EventLogSubscription subscription)
			{
				EventLogNotification.EventMatchInternal[] array = new EventLogNotification.EventMatchInternal[2];
				array[0] = new EventLogNotification.EventMatchInternal();
				array[0].IsRed = true;
				array[0].EventMatch = subscription.RedEvents;
				array[0].SubscriptionName = subscription.Name;
				if (subscription.GreenEvents != null)
				{
					array[1] = new EventLogNotification.EventMatchInternal();
					array[1].IsRed = false;
					array[1].EventMatch = subscription.GreenEvents;
					array[1].SubscriptionName = subscription.Name;
				}
				else
				{
					array[1] = null;
				}
				return array;
			}

			// Token: 0x06000387 RID: 903 RVA: 0x00017FAC File Offset: 0x000161AC
			public bool IsMatch(EventLogNotification.EventRecordInternal record)
			{
				EventMatchingRule eventMatch = this.EventMatch;
				return eventMatch.EventIds.Contains(record.Id) && !string.IsNullOrWhiteSpace(record.LogName) && record.LogName.Equals(eventMatch.LogName, StringComparison.OrdinalIgnoreCase) && (eventMatch.ProviderName.Equals("*") || string.IsNullOrWhiteSpace(record.ProviderName) || record.ProviderName.Equals(eventMatch.ProviderName, StringComparison.OrdinalIgnoreCase)) && (eventMatch.OnMatching == null || eventMatch.OnMatching(record));
			}

			// Token: 0x06000388 RID: 904 RVA: 0x00018044 File Offset: 0x00016244
			public EventLogNotification.EventNotificationMetadata GetNotification(EventLogNotification.EventRecordInternal record)
			{
				EventLogNotification.EventNotificationMetadata eventNotificationMetadata = new EventLogNotification.EventNotificationMetadata();
				eventNotificationMetadata.ServiceName = "EventLogNotification";
				eventNotificationMetadata.ComponentName = this.SubscriptionName;
				string tagName = "Normal";
				if (this.EventMatch.ResourceNameIndex != -1 && record.EventRecord.Properties != null && record.EventRecord.Properties.Count - 1 >= this.EventMatch.ResourceNameIndex && record.EventRecord.Properties[this.EventMatch.ResourceNameIndex].Value != null)
				{
					tagName = record.EventRecord.Properties[this.EventMatch.ResourceNameIndex].Value.ToString().ToLower();
				}
				eventNotificationMetadata.TagName = tagName;
				eventNotificationMetadata.EventMessage = (this.EventMatch.EvaluateMessage ? record.EventRecord.FormatDescription() : "NotEvaluated");
				eventNotificationMetadata.EventPropertiesXml = (this.EventMatch.PopulatePropertiesXml ? record.GeneratePropertyXml() : "NotEvaluated");
				eventNotificationMetadata.IsCritical = this.IsRed;
				eventNotificationMetadata.Message = string.Format("Event LogName={0}, Provider={1}, Id={2} caught. Message={3}, Properties={4}. IsCritical={5} for Subscription {6}", new object[]
				{
					record.LogName,
					record.ProviderName,
					record.Id,
					eventNotificationMetadata.EventMessage,
					eventNotificationMetadata.EventPropertiesXml,
					eventNotificationMetadata.IsCritical,
					this.SubscriptionName
				});
				if (this.EventMatch.OnNotify != null)
				{
					this.EventMatch.OnNotify(record, ref eventNotificationMetadata);
				}
				return eventNotificationMetadata;
			}
		}

		// Token: 0x0200006B RID: 107
		public class SubscriptionMetadata
		{
			// Token: 0x170000C5 RID: 197
			// (get) Token: 0x06000389 RID: 905 RVA: 0x000181D5 File Offset: 0x000163D5
			// (set) Token: 0x0600038A RID: 906 RVA: 0x000181DD File Offset: 0x000163DD
			public string Name { get; set; }

			// Token: 0x170000C6 RID: 198
			// (get) Token: 0x0600038B RID: 907 RVA: 0x000181E6 File Offset: 0x000163E6
			// (set) Token: 0x0600038C RID: 908 RVA: 0x000181EE File Offset: 0x000163EE
			public TimeSpan AutoResetInterval { get; set; }

			// Token: 0x0600038D RID: 909 RVA: 0x000181F7 File Offset: 0x000163F7
			private SubscriptionMetadata()
			{
			}

			// Token: 0x0600038E RID: 910 RVA: 0x00018200 File Offset: 0x00016400
			public static EventLogNotification.SubscriptionMetadata ConstructFromSubscription(EventLogSubscription subscription)
			{
				return new EventLogNotification.SubscriptionMetadata
				{
					Name = subscription.Name,
					AutoResetInterval = subscription.AutoResetInterval
				};
			}
		}

		// Token: 0x0200006C RID: 108
		public class EventNotificationMetadata
		{
			// Token: 0x170000C7 RID: 199
			// (get) Token: 0x0600038F RID: 911 RVA: 0x0001822C File Offset: 0x0001642C
			// (set) Token: 0x06000390 RID: 912 RVA: 0x00018234 File Offset: 0x00016434
			public string ServiceName { get; set; }

			// Token: 0x170000C8 RID: 200
			// (get) Token: 0x06000391 RID: 913 RVA: 0x0001823D File Offset: 0x0001643D
			// (set) Token: 0x06000392 RID: 914 RVA: 0x00018245 File Offset: 0x00016445
			public string ComponentName { get; set; }

			// Token: 0x170000C9 RID: 201
			// (get) Token: 0x06000393 RID: 915 RVA: 0x0001824E File Offset: 0x0001644E
			// (set) Token: 0x06000394 RID: 916 RVA: 0x00018256 File Offset: 0x00016456
			public string TagName { get; set; }

			// Token: 0x170000CA RID: 202
			// (get) Token: 0x06000395 RID: 917 RVA: 0x0001825F File Offset: 0x0001645F
			// (set) Token: 0x06000396 RID: 918 RVA: 0x00018267 File Offset: 0x00016467
			public bool IsCritical { get; set; }

			// Token: 0x170000CB RID: 203
			// (get) Token: 0x06000397 RID: 919 RVA: 0x00018270 File Offset: 0x00016470
			// (set) Token: 0x06000398 RID: 920 RVA: 0x00018278 File Offset: 0x00016478
			public string Message { get; set; }

			// Token: 0x170000CC RID: 204
			// (get) Token: 0x06000399 RID: 921 RVA: 0x00018281 File Offset: 0x00016481
			// (set) Token: 0x0600039A RID: 922 RVA: 0x00018289 File Offset: 0x00016489
			public string EventMessage { get; set; }

			// Token: 0x170000CD RID: 205
			// (get) Token: 0x0600039B RID: 923 RVA: 0x00018292 File Offset: 0x00016492
			// (set) Token: 0x0600039C RID: 924 RVA: 0x0001829A File Offset: 0x0001649A
			public string EventPropertiesXml { get; set; }

			// Token: 0x170000CE RID: 206
			// (get) Token: 0x0600039D RID: 925 RVA: 0x000182A3 File Offset: 0x000164A3
			// (set) Token: 0x0600039E RID: 926 RVA: 0x000182AB File Offset: 0x000164AB
			public string StateAttribute3 { get; set; }

			// Token: 0x170000CF RID: 207
			// (get) Token: 0x0600039F RID: 927 RVA: 0x000182B4 File Offset: 0x000164B4
			// (set) Token: 0x060003A0 RID: 928 RVA: 0x000182BC File Offset: 0x000164BC
			public string StateAttribute4 { get; set; }

			// Token: 0x170000D0 RID: 208
			// (get) Token: 0x060003A1 RID: 929 RVA: 0x000182C5 File Offset: 0x000164C5
			// (set) Token: 0x060003A2 RID: 930 RVA: 0x000182CD File Offset: 0x000164CD
			public string StateAttribute5 { get; set; }

			// Token: 0x060003A3 RID: 931 RVA: 0x000182D6 File Offset: 0x000164D6
			private static string TruncateIfNeeded(string str)
			{
				if (!string.IsNullOrWhiteSpace(str) && str.Length >= 3072)
				{
					return string.Format("{0}...", str.Substring(0, 3069));
				}
				return str;
			}

			// Token: 0x060003A4 RID: 932 RVA: 0x00018308 File Offset: 0x00016508
			public EventNotificationItem GenerateEventNotificationItem()
			{
				string text = this.ComponentName;
				if (string.IsNullOrWhiteSpace(text))
				{
					text = "Normal";
				}
				EventNotificationItem eventNotificationItem = new EventNotificationItem(this.ServiceName, text, this.TagName, this.IsCritical ? ResultSeverityLevel.Critical : ResultSeverityLevel.Informational);
				if (!string.IsNullOrWhiteSpace(this.Message))
				{
					eventNotificationItem.Message = EventLogNotification.EventNotificationMetadata.TruncateIfNeeded(this.Message);
				}
				eventNotificationItem.StateAttribute1 = EventLogNotification.EventNotificationMetadata.TruncateIfNeeded(this.EventMessage);
				eventNotificationItem.StateAttribute2 = EventLogNotification.EventNotificationMetadata.TruncateIfNeeded(this.EventPropertiesXml);
				eventNotificationItem.StateAttribute3 = EventLogNotification.EventNotificationMetadata.TruncateIfNeeded(this.StateAttribute3);
				eventNotificationItem.StateAttribute4 = EventLogNotification.EventNotificationMetadata.TruncateIfNeeded(this.StateAttribute4);
				eventNotificationItem.StateAttribute5 = EventLogNotification.EventNotificationMetadata.TruncateIfNeeded(this.StateAttribute5);
				return eventNotificationItem;
			}

			// Token: 0x040002A0 RID: 672
			public const string DefaultComponentName = "Normal";
		}
	}
}
