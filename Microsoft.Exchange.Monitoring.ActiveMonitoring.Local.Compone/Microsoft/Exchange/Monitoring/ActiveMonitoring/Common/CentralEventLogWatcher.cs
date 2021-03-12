using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Common
{
	// Token: 0x020000C2 RID: 194
	public class CentralEventLogWatcher : IDisposable
	{
		// Token: 0x06000684 RID: 1668 RVA: 0x00026558 File Offset: 0x00024758
		private CentralEventLogWatcher()
		{
			TimerCallback callback = new TimerCallback(this.ProcessEvents);
			this.processorTimer = new Timer(callback, null, CentralEventLogWatcher.EventProcessorTimerInterval, CentralEventLogWatcher.EventProcessorTimerInterval);
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x00026628 File Offset: 0x00024828
		private static void AddValueToDictionary<T>(Dictionary<string, HashSet<T>> dict, string key, T[] entriesToAdd)
		{
			if (entriesToAdd == null || entriesToAdd.Length < 1)
			{
				return;
			}
			HashSet<T> hashSet = null;
			if (!dict.TryGetValue(key, out hashSet))
			{
				hashSet = new HashSet<T>();
				dict.Add(key, hashSet);
			}
			foreach (T item in entriesToAdd)
			{
				hashSet.Add(item);
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000686 RID: 1670 RVA: 0x0002667A File Offset: 0x0002487A
		private static TimeSpan EventProcessorTimerInterval
		{
			get
			{
				if (CentralEventLogWatcher.timerInterval == TimeSpan.Zero)
				{
					CentralEventLogWatcher.timerInterval = TimeSpan.FromSeconds((double)RegistryHelper.GetProperty<int>("ProcessorTimerIntervalInSecs", 300, "EventProcessor", null, false));
				}
				return CentralEventLogWatcher.timerInterval;
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000687 RID: 1671 RVA: 0x000266B4 File Offset: 0x000248B4
		public static CentralEventLogWatcher Instance
		{
			get
			{
				lock (CentralEventLogWatcher.singletonLock)
				{
					if (CentralEventLogWatcher.watcherInstance == null)
					{
						CentralEventLogWatcher.watcherInstance = new CentralEventLogWatcher();
					}
				}
				return CentralEventLogWatcher.watcherInstance;
			}
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000688 RID: 1672 RVA: 0x00026704 File Offset: 0x00024904
		// (remove) Token: 0x06000689 RID: 1673 RVA: 0x0002673C File Offset: 0x0002493C
		public event Action<EventRecord, CentralEventLogWatcher.EventRecordMini> BeforeEnqueueEvent;

		// Token: 0x0600068A RID: 1674 RVA: 0x00026774 File Offset: 0x00024974
		public void Dispose()
		{
			lock (this.disposeLock)
			{
				if (!this.isDisposed)
				{
					if (this.processorTimer != null)
					{
						WTFDiagnostics.TraceInformation(ExTraceGlobals.CommonComponentsTracer, this.traceContext, "CentralEventLogWatcher:: Disposing ProcessorTimer...", null, "Dispose", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Common\\Probes\\CentralEventLogWatcher.cs", 306);
						this.processorTimer.Dispose();
						this.processorTimer = null;
					}
					this.isDisposed = true;
				}
			}
		}

		// Token: 0x0600068B RID: 1675 RVA: 0x000267FC File Offset: 0x000249FC
		public bool IsEventWatchRuleExists(CentralEventLogWatcher.IEventLogWatcherRule rule)
		{
			return this.eventProbeRules.ContainsKey(rule.RuleName) && this.eventProbeRules[rule.RuleName].SameAs(rule);
		}

		// Token: 0x0600068C RID: 1676 RVA: 0x0002683C File Offset: 0x00024A3C
		public void AddEventWatchRule(CentralEventLogWatcher.IEventLogWatcherRule rule)
		{
			this.CheckIsDisposedAndThrow();
			if (string.IsNullOrWhiteSpace(rule.LogName) || string.IsNullOrWhiteSpace(rule.RuleName))
			{
				throw new CentralEventLogWatcher.InvalidRuleException(string.Format("Invalid EventWatchRule - LogName={0}, RuleName={1}", rule.LogName, rule.RuleName));
			}
			string logName = rule.LogName;
			string[] providerNames = rule.ProviderNames;
			long[] eventIds = rule.EventIds;
			if (!EventLog.Exists(logName) && !EventLogSession.GlobalSession.GetLogNames().Contains(logName))
			{
				throw new Exception(string.Format("LogName - {0} does not exists!", rule.LogName));
			}
			CentralEventLogWatcher.IEventLogWatcherRule eventLogWatcherRule = null;
			if (this.eventProbeRules.TryGetValue(rule.RuleName, out eventLogWatcherRule) && !this.eventProbeRules[rule.RuleName].GetType().Equals(rule.GetType()))
			{
				throw new CentralEventLogWatcher.InvalidRuleException(string.Format("Rule with name {0} is enrolled into watcher but not of the same type. You cannot overwrite existing rule with same name but of different type. RuleExistsType={1}, incoming type={2}", rule.RuleName, this.eventProbeRules[rule.RuleName].GetType(), rule.GetType()));
			}
			WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.CommonComponentsTracer, this.traceContext, "CentralEventLogWatcher: Writing rule name={0}", rule.RuleName, null, "AddEventWatchRule", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Common\\Probes\\CentralEventLogWatcher.cs", 372);
			this.rwLockSubscriberRequests.EnterWriteLock();
			try
			{
				CentralEventLogWatcher.AddValueToDictionary<long>(this.subscriberRequestIds, logName, eventIds);
				CentralEventLogWatcher.AddValueToDictionary<string>(this.subscriberRequestProviders, logName, providerNames);
			}
			finally
			{
				this.rwLockSubscriberRequests.ExitWriteLock();
			}
			this.RebuildEventFilterList(rule);
			this.eventProbeRules.AddOrUpdate(rule.RuleName, rule, (string str, CentralEventLogWatcher.IEventLogWatcherRule currentRule) => rule);
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x00026A48 File Offset: 0x00024C48
		public int PopLastEventRecordForRule(string ruleName, out CentralEventLogWatcher.EventRecordMini record)
		{
			this.CheckIsDisposedAndThrow();
			int result = 0;
			this.rwLockEventSorting.EnterUpgradeableReadLock();
			try
			{
				if (this.eventBuckets.ContainsKey(ruleName))
				{
					WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.CommonComponentsTracer, this.traceContext, "CentralEventLogWatcher:: Clearing bucket {0}", ruleName, null, "PopLastEventRecordForRule", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Common\\Probes\\CentralEventLogWatcher.cs", 417);
					this.rwLockEventSorting.EnterWriteLock();
					try
					{
						if (!this.eventBuckets.TryRemove(ruleName, out record))
						{
							WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.CommonComponentsTracer, this.traceContext, "CentralEventLogWatcher:: Unable to clear bucket {0}.", ruleName, null, "PopLastEventRecordForRule", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Common\\Probes\\CentralEventLogWatcher.cs", 428);
						}
						goto IL_98;
					}
					finally
					{
						this.rwLockEventSorting.ExitWriteLock();
					}
				}
				record = null;
				IL_98:
				if (this.eventCount.ContainsKey(ruleName))
				{
					WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.CommonComponentsTracer, this.traceContext, "CentralEventLogWatcher:: Clearing count bucket {0}", ruleName, null, "PopLastEventRecordForRule", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Common\\Probes\\CentralEventLogWatcher.cs", 447);
					this.rwLockEventSorting.EnterWriteLock();
					try
					{
						if (!this.eventCount.TryRemove(ruleName, out result))
						{
							WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.CommonComponentsTracer, this.traceContext, "CentralEventLogWatcher:: Unable to clear count bucket {0}.", ruleName, null, "PopLastEventRecordForRule", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Common\\Probes\\CentralEventLogWatcher.cs", 458);
						}
					}
					finally
					{
						this.rwLockEventSorting.ExitWriteLock();
					}
				}
			}
			finally
			{
				this.rwLockEventSorting.ExitUpgradeableReadLock();
			}
			return result;
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x0600068E RID: 1678 RVA: 0x00026BCC File Offset: 0x00024DCC
		public CentralEventLogWatcher.EventProcessorStatus EventProcessorCurrentStatus
		{
			get
			{
				this.rwLockEventProcessorStatus.EnterReadLock();
				CentralEventLogWatcher.EventProcessorStatus result;
				try
				{
					result = new CentralEventLogWatcher.EventProcessorStatus
					{
						LastEventProcessorRuntime = this.processorLastRunTime,
						EventsProcessedSinceInstanceStart = this.processorEventProcessedCount,
						TimerInterval = (int)CentralEventLogWatcher.EventProcessorTimerInterval.TotalSeconds,
						LastEventProcessorTimeSpentInMs = this.processorRuntimeDurationInMs,
						EventProcessorsRunningCount = this.eventProcessorsCount
					};
				}
				finally
				{
					this.rwLockEventProcessorStatus.ExitReadLock();
				}
				return result;
			}
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x00026C58 File Offset: 0x00024E58
		private string ConstructXPathForLogName(string LogName, DateTime startTime, DateTime endTime)
		{
			List<string> list = new List<string>();
			List<string> list2 = new List<string>();
			if (this.subscriberRequestIds.ContainsKey(LogName))
			{
				foreach (long num in this.subscriberRequestIds[LogName])
				{
					list.Add(string.Format("EventID={0}", num));
				}
			}
			if (this.subscriberRequestProviders.ContainsKey(LogName))
			{
				foreach (string arg in this.subscriberRequestProviders[LogName])
				{
					list2.Add(string.Format("@Name='{0}'", arg));
				}
			}
			if (list.Count < 1)
			{
				return string.Empty;
			}
			if (list2.Count < 1)
			{
				return string.Format("*[System[({0}) and TimeCreated[@SystemTime>='{1}' and @SystemTime<='{2}']]]", string.Join(" or ", list), startTime.ToString("o"), endTime.ToString("o"));
			}
			return string.Format("*[System[Provider[{0}] and ({1}) and TimeCreated[@SystemTime>='{2}' and @SystemTime<='{3}']]]", new object[]
			{
				string.Join(" or ", list2),
				string.Join(" or ", list),
				startTime.ToString("o"),
				endTime.ToString("o")
			});
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x00026DD4 File Offset: 0x00024FD4
		private CentralEventLogWatcher.EventRecordMini PreProcessEvent(EventRecord e)
		{
			this.CheckIsDisposedAndThrow();
			if (e != null)
			{
				CentralEventLogWatcher.EventRecordMini eventRecordMini = CentralEventLogWatcher.EventRecordMini.ConstructFromEventRecord(e);
				if (this.BeforeEnqueueEvent != null)
				{
					this.BeforeEnqueueEvent(e, eventRecordMini);
				}
				return eventRecordMini;
			}
			WTFDiagnostics.TraceInformation(ExTraceGlobals.CommonComponentsTracer, this.traceContext, "CentralEventLogWatcher:: EventRecord is Empty", null, "PreProcessEvent", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Common\\Probes\\CentralEventLogWatcher.cs", 573);
			return null;
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x00026E30 File Offset: 0x00025030
		private void ProcessEvents(object stateInfo)
		{
			if (this.isDisposed)
			{
				WTFDiagnostics.TraceInformation<bool>(ExTraceGlobals.CommonComponentsTracer, this.traceContext, "CentralEventLogWatcher:: Instance is already disposed. Skipping EventQuery... isDisposed={0}", this.isDisposed, null, "ProcessEvents", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Common\\Probes\\CentralEventLogWatcher.cs", 590);
				return;
			}
			DateTime dateTime = DateTime.UtcNow.AddHours(-1.0);
			DateTime utcNow = DateTime.UtcNow;
			this.rwLockEventProcessorStatus.EnterWriteLock();
			try
			{
				dateTime = ((this.processorLastRunTime == DateTime.MinValue) ? dateTime : this.processorLastRunTime);
				this.eventProcessorsCount++;
				this.processorLastRunTime = utcNow;
			}
			finally
			{
				this.rwLockEventProcessorStatus.ExitWriteLock();
			}
			Dictionary<string, string> dictionary = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
			CentralEventLogWatcher.IEventLogWatcherRule[] rulesArray = null;
			lock (this.eventFilterListLock)
			{
				if (this.eventFilterList != null && this.eventFilterList.Count > 0)
				{
					rulesArray = this.eventFilterList.ToArray();
				}
			}
			try
			{
				this.rwLockSubscriberRequests.EnterReadLock();
				try
				{
					foreach (string text in this.subscriberRequestIds.Keys)
					{
						dictionary.Add(text, this.ConstructXPathForLogName(text, dateTime, utcNow));
					}
				}
				finally
				{
					this.rwLockSubscriberRequests.ExitReadLock();
				}
				foreach (KeyValuePair<string, string> keyValuePair in dictionary)
				{
					try
					{
						using (EventLogReader eventLogReader = new EventLogReader(new EventLogQuery(keyValuePair.Key, PathType.LogName, keyValuePair.Value)))
						{
							for (EventRecord eventRecord = eventLogReader.ReadEvent(); eventRecord != null; eventRecord = eventLogReader.ReadEvent())
							{
								using (eventRecord)
								{
									this.PutEventsIntoBuckets(this.PreProcessEvent(eventRecord), rulesArray);
								}
							}
						}
					}
					catch (Exception ex)
					{
						WTFDiagnostics.TraceInformation<string, string>(ExTraceGlobals.CommonComponentsTracer, this.traceContext, "CentralEventLogWatcher:: Exception logged when querying/processing events for log {0} - {1}", keyValuePair.Key, ex.ToString(), null, "ProcessEvents", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Common\\Probes\\CentralEventLogWatcher.cs", 667);
					}
				}
			}
			finally
			{
				this.rwLockEventProcessorStatus.EnterWriteLock();
				try
				{
					this.eventProcessorsCount--;
					this.processorRuntimeDurationInMs = (double)((int)(DateTime.UtcNow - utcNow).TotalMilliseconds);
				}
				finally
				{
					this.rwLockEventProcessorStatus.ExitWriteLock();
				}
			}
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x00027194 File Offset: 0x00025394
		private void PutEventsIntoBuckets(CentralEventLogWatcher.EventRecordMini evtRecord, CentralEventLogWatcher.IEventLogWatcherRule[] rulesArray)
		{
			if (this.isDisposed)
			{
				WTFDiagnostics.TraceInformation<bool>(ExTraceGlobals.CommonComponentsTracer, this.traceContext, "CentralEventLogWatcher:: Instance is already disposed. Skipping PutEventsIntoBuckets... isDisposed={0}", this.isDisposed, null, "PutEventsIntoBuckets", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Common\\Probes\\CentralEventLogWatcher.cs", 700);
				return;
			}
			if (rulesArray != null && rulesArray.Length > 0)
			{
				foreach (CentralEventLogWatcher.IEventLogWatcherRule eventLogWatcherRule in rulesArray)
				{
					if (eventLogWatcherRule.MatchRule(evtRecord))
					{
						string ruleName = eventLogWatcherRule.RuleName;
						this.rwLockEventSorting.EnterUpgradeableReadLock();
						try
						{
							if (!this.eventBuckets.ContainsKey(ruleName) || this.eventBuckets[ruleName] == null || !(this.eventBuckets[ruleName].TimeCreated > evtRecord.TimeCreated))
							{
								this.rwLockEventSorting.EnterWriteLock();
								try
								{
									this.eventBuckets[ruleName] = evtRecord;
									this.eventCount[ruleName] = 1 + this.eventCount.GetOrAdd(ruleName, 0);
								}
								finally
								{
									this.rwLockEventSorting.ExitWriteLock();
								}
							}
						}
						finally
						{
							this.rwLockEventSorting.ExitUpgradeableReadLock();
						}
					}
				}
			}
			this.rwLockEventProcessorStatus.EnterWriteLock();
			try
			{
				this.processorEventProcessedCount += 1L;
			}
			finally
			{
				this.rwLockEventProcessorStatus.ExitWriteLock();
			}
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x0002731C File Offset: 0x0002551C
		private void RebuildEventFilterList(CentralEventLogWatcher.IEventLogWatcherRule rule)
		{
			this.CheckIsDisposedAndThrow();
			lock (this.eventFilterListLock)
			{
				int num = -1;
				for (int i = 0; i < this.eventFilterList.Count; i++)
				{
					if (this.eventFilterList[i].RuleName.Equals(rule.RuleName))
					{
						num = i;
						break;
					}
				}
				if (num >= 0)
				{
					WTFDiagnostics.TraceInformation<int, string>(ExTraceGlobals.CommonComponentsTracer, this.traceContext, "CentralEventLogWatcher:: Replace rule in eventFilterList Index={0}, Rule={1}", num, rule.RuleName, null, "RebuildEventFilterList", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Common\\Probes\\CentralEventLogWatcher.cs", 782);
					this.eventFilterList[num] = rule;
				}
				else
				{
					WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.CommonComponentsTracer, this.traceContext, "CentralEventLogWatcher:: Adding rule in eventFilterList Rule={0}", rule.RuleName, null, "RebuildEventFilterList", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Common\\Probes\\CentralEventLogWatcher.cs", 793);
					this.eventFilterList.Add(rule);
				}
			}
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x0002740C File Offset: 0x0002560C
		private void CheckIsDisposedAndThrow()
		{
			lock (this.disposeLock)
			{
				if (this.isDisposed)
				{
					throw new ObjectDisposedException(base.GetType().FullName);
				}
			}
		}

		// Token: 0x04000420 RID: 1056
		private const string EventProcessorSubkey = "EventProcessor";

		// Token: 0x04000421 RID: 1057
		private const string EventProcessorIntervalValueName = "ProcessorTimerIntervalInSecs";

		// Token: 0x04000422 RID: 1058
		private static CentralEventLogWatcher watcherInstance = null;

		// Token: 0x04000423 RID: 1059
		private static readonly object singletonLock = new object();

		// Token: 0x04000424 RID: 1060
		private static TimeSpan timerInterval = TimeSpan.Zero;

		// Token: 0x04000425 RID: 1061
		private TracingContext traceContext = TracingContext.Default;

		// Token: 0x04000426 RID: 1062
		private readonly ReaderWriterLockSlim rwLockSubscriberRequests = new ReaderWriterLockSlim();

		// Token: 0x04000427 RID: 1063
		private readonly Dictionary<string, HashSet<long>> subscriberRequestIds = new Dictionary<string, HashSet<long>>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x04000428 RID: 1064
		private readonly Dictionary<string, HashSet<string>> subscriberRequestProviders = new Dictionary<string, HashSet<string>>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x04000429 RID: 1065
		private readonly ConcurrentDictionary<string, CentralEventLogWatcher.IEventLogWatcherRule> eventProbeRules = new ConcurrentDictionary<string, CentralEventLogWatcher.IEventLogWatcherRule>();

		// Token: 0x0400042A RID: 1066
		private readonly ReaderWriterLockSlim rwLockEventSorting = new ReaderWriterLockSlim();

		// Token: 0x0400042B RID: 1067
		private readonly ConcurrentDictionary<string, CentralEventLogWatcher.EventRecordMini> eventBuckets = new ConcurrentDictionary<string, CentralEventLogWatcher.EventRecordMini>();

		// Token: 0x0400042C RID: 1068
		private readonly ConcurrentDictionary<string, int> eventCount = new ConcurrentDictionary<string, int>();

		// Token: 0x0400042D RID: 1069
		private readonly object eventFilterListLock = new object();

		// Token: 0x0400042E RID: 1070
		private readonly List<CentralEventLogWatcher.IEventLogWatcherRule> eventFilterList = new List<CentralEventLogWatcher.IEventLogWatcherRule>();

		// Token: 0x0400042F RID: 1071
		private readonly ReaderWriterLockSlim rwLockEventProcessorStatus = new ReaderWriterLockSlim();

		// Token: 0x04000430 RID: 1072
		private Timer processorTimer;

		// Token: 0x04000431 RID: 1073
		private int eventProcessorsCount;

		// Token: 0x04000432 RID: 1074
		private long processorEventProcessedCount;

		// Token: 0x04000433 RID: 1075
		private DateTime processorLastRunTime = DateTime.MinValue;

		// Token: 0x04000434 RID: 1076
		private double processorRuntimeDurationInMs;

		// Token: 0x04000435 RID: 1077
		private bool isDisposed;

		// Token: 0x04000436 RID: 1078
		private readonly object disposeLock = new object();

		// Token: 0x020000C3 RID: 195
		public interface IEventLogWatcherRule
		{
			// Token: 0x17000190 RID: 400
			// (get) Token: 0x06000696 RID: 1686
			string RuleName { get; }

			// Token: 0x17000191 RID: 401
			// (get) Token: 0x06000697 RID: 1687
			string LogName { get; }

			// Token: 0x17000192 RID: 402
			// (get) Token: 0x06000698 RID: 1688
			long[] EventIds { get; }

			// Token: 0x17000193 RID: 403
			// (get) Token: 0x06000699 RID: 1689
			string[] ProviderNames { get; }

			// Token: 0x0600069A RID: 1690
			bool SameAs(CentralEventLogWatcher.IEventLogWatcherRule rule);

			// Token: 0x0600069B RID: 1691
			bool MatchRule(CentralEventLogWatcher.EventRecordMini evt);
		}

		// Token: 0x020000C4 RID: 196
		public class EventRecordMini
		{
			// Token: 0x0600069C RID: 1692 RVA: 0x0002747C File Offset: 0x0002567C
			public static CentralEventLogWatcher.EventRecordMini ConstructFromEventRecord(EventRecord evt)
			{
				CentralEventLogWatcher.EventRecordMini eventRecordMini = new CentralEventLogWatcher.EventRecordMini();
				eventRecordMini.LogName = evt.LogName;
				eventRecordMini.Source = evt.ProviderName;
				eventRecordMini.EventId = evt.Id;
				eventRecordMini.TimeCreated = evt.TimeCreated;
				eventRecordMini.CustomizedProperties = new List<string>();
				foreach (EventProperty eventProperty in evt.Properties)
				{
					eventRecordMini.CustomizedProperties.Add(eventProperty.Value.ToString());
				}
				return eventRecordMini;
			}

			// Token: 0x04000438 RID: 1080
			public string LogName;

			// Token: 0x04000439 RID: 1081
			public string Source;

			// Token: 0x0400043A RID: 1082
			public int EventId;

			// Token: 0x0400043B RID: 1083
			public DateTime? TimeCreated;

			// Token: 0x0400043C RID: 1084
			public string WatsonProcessName;

			// Token: 0x0400043D RID: 1085
			public string WatsonExtendedPropertyField1;

			// Token: 0x0400043E RID: 1086
			public string WatsonExtendedPropertyField2;

			// Token: 0x0400043F RID: 1087
			public string WatsonExtendedPropertyField3;

			// Token: 0x04000440 RID: 1088
			public bool IsProcessTerminatingWatson;

			// Token: 0x04000441 RID: 1089
			public string ExtendedPropertyField1;

			// Token: 0x04000442 RID: 1090
			public List<string> CustomizedProperties;
		}

		// Token: 0x020000C5 RID: 197
		public struct EventProcessorStatus
		{
			// Token: 0x04000443 RID: 1091
			public long EventsProcessedSinceInstanceStart;

			// Token: 0x04000444 RID: 1092
			public double LastEventProcessorTimeSpentInMs;

			// Token: 0x04000445 RID: 1093
			public DateTime LastEventProcessorRuntime;

			// Token: 0x04000446 RID: 1094
			public int TimerInterval;

			// Token: 0x04000447 RID: 1095
			public int EventProcessorsRunningCount;
		}

		// Token: 0x020000C6 RID: 198
		public class EventProbeRule : CentralEventLogWatcher.IEventLogWatcherRule
		{
			// Token: 0x0600069E RID: 1694 RVA: 0x00027528 File Offset: 0x00025728
			public EventProbeRule(string ruleName, string logName, string source, int[] greenEventIds, int[] redEventIds)
			{
				this.EventRuleName = ruleName;
				this.EventLogName = logName;
				this.Source = source;
				this.providerNames = new string[]
				{
					source
				};
				this.greenEventIds = greenEventIds;
				this.redEventIds = redEventIds;
				List<int> list = new List<int>();
				if (this.greenEventIds != null && this.greenEventIds.Length > 0)
				{
					list.AddRange(this.greenEventIds);
				}
				if (this.redEventIds != null && this.redEventIds.Length > 0)
				{
					list.AddRange(this.redEventIds);
				}
				this.allEventIds = (from id in list
				select (long)id).ToArray<long>();
			}

			// Token: 0x17000194 RID: 404
			// (get) Token: 0x0600069F RID: 1695 RVA: 0x000275E4 File Offset: 0x000257E4
			public int[] GreenEventIds
			{
				get
				{
					if (this.greenEventIds == null)
					{
						return null;
					}
					return (int[])this.greenEventIds.Clone();
				}
			}

			// Token: 0x17000195 RID: 405
			// (get) Token: 0x060006A0 RID: 1696 RVA: 0x00027600 File Offset: 0x00025800
			public int[] RedEventIds
			{
				get
				{
					if (this.redEventIds == null)
					{
						return null;
					}
					return (int[])this.redEventIds.Clone();
				}
			}

			// Token: 0x17000196 RID: 406
			// (get) Token: 0x060006A1 RID: 1697 RVA: 0x0002761C File Offset: 0x0002581C
			public string LogName
			{
				get
				{
					return this.EventLogName;
				}
			}

			// Token: 0x17000197 RID: 407
			// (get) Token: 0x060006A2 RID: 1698 RVA: 0x00027624 File Offset: 0x00025824
			public string RuleName
			{
				get
				{
					return this.EventRuleName;
				}
			}

			// Token: 0x17000198 RID: 408
			// (get) Token: 0x060006A3 RID: 1699 RVA: 0x0002762C File Offset: 0x0002582C
			public long[] EventIds
			{
				get
				{
					return this.allEventIds;
				}
			}

			// Token: 0x17000199 RID: 409
			// (get) Token: 0x060006A4 RID: 1700 RVA: 0x00027634 File Offset: 0x00025834
			public string[] ProviderNames
			{
				get
				{
					return this.providerNames;
				}
			}

			// Token: 0x060006A5 RID: 1701 RVA: 0x0002763C File Offset: 0x0002583C
			public bool SameAs(CentralEventLogWatcher.IEventLogWatcherRule rule)
			{
				if (rule is CentralEventLogWatcher.EventProbeRule)
				{
					CentralEventLogWatcher.EventProbeRule eventProbeRule = (CentralEventLogWatcher.EventProbeRule)rule;
					return string.Equals(this.EventLogName, eventProbeRule.EventLogName, StringComparison.Ordinal) && string.Equals(this.Source, eventProbeRule.Source, StringComparison.Ordinal) && string.Equals(this.RuleName, eventProbeRule.RuleName, StringComparison.Ordinal) && CentralEventLogWatcher.EventProbeRule.IsArrayEqual<int>(this.greenEventIds, eventProbeRule.greenEventIds) && CentralEventLogWatcher.EventProbeRule.IsArrayEqual<int>(this.redEventIds, eventProbeRule.redEventIds);
				}
				return false;
			}

			// Token: 0x060006A6 RID: 1702 RVA: 0x000276C7 File Offset: 0x000258C7
			public virtual bool MatchRule(CentralEventLogWatcher.EventRecordMini evt)
			{
				return (this.Source.Equals("*") || this.Source.Equals(evt.Source, StringComparison.OrdinalIgnoreCase)) && this.EventIds.Contains((long)evt.EventId);
			}

			// Token: 0x060006A7 RID: 1703 RVA: 0x00027708 File Offset: 0x00025908
			private static bool IsArrayEqual<T>(T[] source, T[] target)
			{
				if (object.ReferenceEquals(source, target))
				{
					return true;
				}
				if (source == null || target == null)
				{
					return false;
				}
				if (source.Length != target.Length)
				{
					return false;
				}
				EqualityComparer<T> @default = EqualityComparer<T>.Default;
				for (int i = 0; i < source.Length; i++)
				{
					if (!@default.Equals(source[i], target[i]))
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x04000448 RID: 1096
			public readonly string EventRuleName;

			// Token: 0x04000449 RID: 1097
			public readonly string EventLogName;

			// Token: 0x0400044A RID: 1098
			public readonly string Source;

			// Token: 0x0400044B RID: 1099
			private int[] greenEventIds;

			// Token: 0x0400044C RID: 1100
			private int[] redEventIds;

			// Token: 0x0400044D RID: 1101
			private long[] allEventIds;

			// Token: 0x0400044E RID: 1102
			private string[] providerNames;
		}

		// Token: 0x020000C7 RID: 199
		public class ProcessCrashRule : CentralEventLogWatcher.IEventLogWatcherRule
		{
			// Token: 0x060006A9 RID: 1705 RVA: 0x0002775F File Offset: 0x0002595F
			public ProcessCrashRule(string ruleName, string serviceName, string moduleName = null, bool skipInformationalWatson = false)
			{
				this.EventRuleName = ruleName;
				this.ServiceName = serviceName;
				this.ModuleName = moduleName;
				this.SkipInformationalWatson = skipInformationalWatson;
			}

			// Token: 0x1700019A RID: 410
			// (get) Token: 0x060006AA RID: 1706 RVA: 0x00027784 File Offset: 0x00025984
			public string RuleName
			{
				get
				{
					return this.EventRuleName;
				}
			}

			// Token: 0x1700019B RID: 411
			// (get) Token: 0x060006AB RID: 1707 RVA: 0x0002778C File Offset: 0x0002598C
			public string LogName
			{
				get
				{
					return "Application";
				}
			}

			// Token: 0x1700019C RID: 412
			// (get) Token: 0x060006AC RID: 1708 RVA: 0x00027793 File Offset: 0x00025993
			public long[] EventIds
			{
				get
				{
					return CentralEventLogWatcher.ProcessCrashRule.eventIds;
				}
			}

			// Token: 0x1700019D RID: 413
			// (get) Token: 0x060006AD RID: 1709 RVA: 0x0002779A File Offset: 0x0002599A
			public string[] ProviderNames
			{
				get
				{
					return CentralEventLogWatcher.ProcessCrashRule.providerNames;
				}
			}

			// Token: 0x060006AE RID: 1710 RVA: 0x000277A4 File Offset: 0x000259A4
			public bool SameAs(CentralEventLogWatcher.IEventLogWatcherRule rule)
			{
				if (rule is CentralEventLogWatcher.ProcessCrashRule)
				{
					CentralEventLogWatcher.ProcessCrashRule processCrashRule = (CentralEventLogWatcher.ProcessCrashRule)rule;
					return string.Equals(processCrashRule.ServiceName, this.ServiceName) && string.Equals(rule.RuleName, this.RuleName) && string.Equals(processCrashRule.ModuleName, this.ModuleName) && processCrashRule.SkipInformationalWatson == this.SkipInformationalWatson;
				}
				return false;
			}

			// Token: 0x060006AF RID: 1711 RVA: 0x0002780C File Offset: 0x00025A0C
			public bool MatchRule(CentralEventLogWatcher.EventRecordMini evt)
			{
				if (string.IsNullOrEmpty(evt.Source) || string.IsNullOrEmpty(evt.WatsonProcessName))
				{
					return false;
				}
				bool flag = false;
				if (string.Equals(evt.Source, "MSExchange Common") && evt.EventId == 4999 && string.Compare(evt.WatsonProcessName, this.ServiceName, StringComparison.OrdinalIgnoreCase) == 0)
				{
					if (string.IsNullOrEmpty(this.ModuleName))
					{
						flag = true;
					}
					else
					{
						string watsonExtendedPropertyField = evt.WatsonExtendedPropertyField1;
						flag = (!string.IsNullOrEmpty(watsonExtendedPropertyField) && watsonExtendedPropertyField.IndexOf(this.ModuleName, StringComparison.OrdinalIgnoreCase) != -1);
					}
					if (this.SkipInformationalWatson)
					{
						flag = (flag && evt.IsProcessTerminatingWatson);
					}
				}
				return flag;
			}

			// Token: 0x04000450 RID: 1104
			public const string Source = "MSExchange Common";

			// Token: 0x04000451 RID: 1105
			public const int EventId = 4999;

			// Token: 0x04000452 RID: 1106
			public const string EventLogName = "Application";

			// Token: 0x04000453 RID: 1107
			internal const int WatsonIssueTypeIndex = 1;

			// Token: 0x04000454 RID: 1108
			internal const int WatsonProcessNameIndex = 4;

			// Token: 0x04000455 RID: 1109
			internal const int WatsonAssemblyNameIndex = 5;

			// Token: 0x04000456 RID: 1110
			internal const int WatsonMethodIndex = 6;

			// Token: 0x04000457 RID: 1111
			internal const int WatsonExceptionTypeIndex = 7;

			// Token: 0x04000458 RID: 1112
			internal const int WatsonTerminatingProcessIndex = 11;

			// Token: 0x04000459 RID: 1113
			internal const string NativeCodeCrashTypeName = "E12N";

			// Token: 0x0400045A RID: 1114
			private static readonly long[] eventIds = new long[]
			{
				4999L
			};

			// Token: 0x0400045B RID: 1115
			private static readonly string[] providerNames = new string[]
			{
				"MSExchange Common"
			};

			// Token: 0x0400045C RID: 1116
			public readonly string EventRuleName;

			// Token: 0x0400045D RID: 1117
			public readonly string ServiceName;

			// Token: 0x0400045E RID: 1118
			public readonly string ModuleName;

			// Token: 0x0400045F RID: 1119
			public readonly bool SkipInformationalWatson;
		}

		// Token: 0x020000C8 RID: 200
		[Serializable]
		public class InvalidRuleException : Exception
		{
			// Token: 0x060006B1 RID: 1713 RVA: 0x000278F0 File Offset: 0x00025AF0
			public InvalidRuleException(string exceptionMessage) : base(exceptionMessage)
			{
			}
		}
	}
}
