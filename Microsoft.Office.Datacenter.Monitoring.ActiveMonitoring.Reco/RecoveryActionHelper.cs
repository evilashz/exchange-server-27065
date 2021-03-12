using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery
{
	// Token: 0x02000041 RID: 65
	public class RecoveryActionHelper
	{
		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x0600025E RID: 606 RVA: 0x00007DC0 File Offset: 0x00005FC0
		internal static DateTime CurrentProcessStartTime
		{
			get
			{
				if (RecoveryActionHelper.currentProcessStartTime == DateTime.MaxValue)
				{
					lock (RecoveryActionHelper.locker)
					{
						using (Process currentProcess = Process.GetCurrentProcess())
						{
							RecoveryActionHelper.currentProcessStartTime = currentProcess.StartTime;
						}
					}
				}
				return RecoveryActionHelper.currentProcessStartTime;
			}
		}

		// Token: 0x0600025F RID: 607 RVA: 0x00007E38 File Offset: 0x00006038
		public static bool CommunicateWorkerProcessInfoToHostProcess(bool isForce = false)
		{
			if (!isForce && RecoveryActionHelper.isWorkerProcessInfoSentToHost)
			{
				return true;
			}
			bool result = false;
			RpcSetWorkerProcessInfoImpl.WorkerProcessInfo info = new RpcSetWorkerProcessInfoImpl.WorkerProcessInfo
			{
				ProcessStartTime = RecoveryActionHelper.CurrentProcessStartTime
			};
			try
			{
				Dependencies.LamRpc.SetWorkerProcessInfo(Dependencies.ThrottleHelper.Tunables.LocalMachineName, info, 30000);
				result = true;
				RecoveryActionHelper.isWorkerProcessInfoSentToHost = true;
			}
			catch (Exception ex)
			{
				WTFDiagnostics.TraceError<string>(ExTraceGlobals.RecoveryActionTracer, RecoveryActionHelper.traceContext, "SetWorkerProcessInfo() failed with exception: {0})", ex.Message, null, "CommunicateWorkerProcessInfoToHostProcess", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Recovery\\RecoveryActionHelper.cs", 118);
			}
			return result;
		}

		// Token: 0x06000260 RID: 608 RVA: 0x00007ECC File Offset: 0x000060CC
		public static Exception RunAndMeasure(string operation, bool isRethrow, ManagedAvailabilityCrimsonEvent crimsonEvent, Func<string> action)
		{
			Stopwatch stopwatch = new Stopwatch();
			TimeSpan timeSpan = TimeSpan.MinValue;
			string text = string.Empty;
			Exception ex = null;
			try
			{
				stopwatch.Start();
				text = action();
				timeSpan = stopwatch.Elapsed;
			}
			catch (Exception ex2)
			{
				timeSpan = stopwatch.Elapsed;
				ex = ex2;
				if (isRethrow)
				{
					throw;
				}
			}
			finally
			{
				if (ex != null)
				{
					WTFDiagnostics.TraceError<string, string, string>(ExTraceGlobals.RecoveryActionTracer, RecoveryActionHelper.traceContext, "{0} failed with exception. (Duration: {1}, Exception: {2})", operation, timeSpan.ToString(), ex.ToString(), null, "RunAndMeasure", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Recovery\\RecoveryActionHelper.cs", 169);
				}
				else
				{
					WTFDiagnostics.TraceDebug<string, string>(ExTraceGlobals.RecoveryActionTracer, RecoveryActionHelper.traceContext, "{0} returned. (Duration: {1})", operation, timeSpan.ToString(), null, "RunAndMeasure", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Recovery\\RecoveryActionHelper.cs", 179);
				}
				if (crimsonEvent != null)
				{
					crimsonEvent.LogGeneric(new object[]
					{
						operation,
						timeSpan.ToString(),
						text,
						(ex != null) ? ex.ToString() : "<none>"
					});
				}
			}
			return ex;
		}

		// Token: 0x06000261 RID: 609 RVA: 0x00007FEC File Offset: 0x000061EC
		public static string ConstructInstanceId(RecoveryActionId id, DateTime startTime)
		{
			string str = startTime.ToString("yyMMdd.hhmmss.fffff.");
			int num = (int)id;
			return str + num.ToString("000");
		}

		// Token: 0x06000262 RID: 610 RVA: 0x00008018 File Offset: 0x00006218
		public static RecoveryActionEntry ConstructStartActionEntry(RecoveryActionId id, string resourceName, string requesterName, DateTime expectedEndTime, string throttleIdentity, string throttleParametersXml, string context = null, string instanceId = null)
		{
			return RecoveryActionHelper.ConstructStartActionEntry(id, resourceName, requesterName, ExDateTime.Now.LocalTime, expectedEndTime, throttleIdentity, throttleParametersXml, context, instanceId, RecoveryActionHelper.CurrentProcessStartTime);
		}

		// Token: 0x06000263 RID: 611 RVA: 0x00008048 File Offset: 0x00006248
		public static RecoveryActionEntry ConstructStartActionEntry(RecoveryActionId id, string resourceName, string requesterName, DateTime startTime, DateTime expectedEndTime, string throttleIdentity, string throttleParametersXml, string context, string instanceId, DateTime lamProcessStartTime)
		{
			return new RecoveryActionEntry
			{
				Id = id,
				ResourceName = resourceName,
				RequestorName = requesterName,
				State = RecoveryActionState.Started,
				Result = RecoveryActionResult.Succeeded,
				StartTime = startTime,
				EndTime = expectedEndTime,
				Context = context,
				InstanceId = (instanceId ?? RecoveryActionHelper.ConstructInstanceId(id, startTime)),
				LamProcessStartTime = lamProcessStartTime,
				ThrottleIdentity = throttleIdentity,
				ThrottleParametersXml = throttleParametersXml
			};
		}

		// Token: 0x06000264 RID: 612 RVA: 0x000080C4 File Offset: 0x000062C4
		public static RecoveryActionEntry ConstructFinishActionEntry(RecoveryActionEntry startActionEntry, Exception exception = null, DateTime? finishTime = null, string finishContext = null)
		{
			RecoveryActionEntry recoveryActionEntry = new RecoveryActionEntry();
			recoveryActionEntry.Id = startActionEntry.Id;
			recoveryActionEntry.ResourceName = startActionEntry.ResourceName;
			recoveryActionEntry.RequestorName = startActionEntry.RequestorName;
			recoveryActionEntry.InstanceId = startActionEntry.InstanceId;
			recoveryActionEntry.CustomArg1 = startActionEntry.CustomArg1;
			recoveryActionEntry.CustomArg2 = startActionEntry.CustomArg2;
			recoveryActionEntry.CustomArg3 = startActionEntry.CustomArg3;
			recoveryActionEntry.Context = (finishContext ?? startActionEntry.Context);
			recoveryActionEntry.StartTime = startActionEntry.StartTime;
			recoveryActionEntry.EndTime = ((finishTime != null) ? finishTime.Value : ExDateTime.Now.LocalTime);
			recoveryActionEntry.State = RecoveryActionState.Finished;
			recoveryActionEntry.ThrottleIdentity = startActionEntry.ThrottleIdentity;
			recoveryActionEntry.ThrottleParametersXml = startActionEntry.ThrottleParametersXml;
			recoveryActionEntry.WriteRecordDelayInMilliSeconds = 1001;
			if (exception == null)
			{
				recoveryActionEntry.Result = RecoveryActionResult.Succeeded;
			}
			else
			{
				recoveryActionEntry.Result = RecoveryActionResult.Failed;
				recoveryActionEntry.ExceptionName = exception.GetType().Name;
				recoveryActionEntry.ExceptionMessage = exception.Message;
			}
			return recoveryActionEntry;
		}

		// Token: 0x06000265 RID: 613 RVA: 0x000081C8 File Offset: 0x000063C8
		public static DateTime GetSystemBootTime()
		{
			Exception ex = null;
			DateTime dateTime = RecoveryActionHelper.GetSystemBootTime(out ex);
			if (dateTime == DateTime.MinValue)
			{
				dateTime = ExDateTime.Now.LocalTime;
			}
			return dateTime;
		}

		// Token: 0x06000266 RID: 614 RVA: 0x00008208 File Offset: 0x00006408
		public static DateTime GetSystemBootTime(out Exception exception)
		{
			exception = null;
			DateTime dateTime = DateTime.MinValue;
			if (BugcheckSimulator.Instance.IsEnabled)
			{
				dateTime = BugcheckSimulator.Instance.SimulatedSystemBootTime;
			}
			if (dateTime == DateTime.MinValue)
			{
				if (RecoveryActionHelper.currentBootTime == DateTime.MinValue)
				{
					lock (RecoveryActionHelper.locker)
					{
						exception = WmiHelper.HandleWmiExceptions(delegate
						{
							RecoveryActionHelper.currentBootTime = WmiHelper.GetSystemBootTime();
						});
						if (exception != null)
						{
							WTFDiagnostics.TraceError<string>(ExTraceGlobals.RecoveryActionTracer, RecoveryActionHelper.traceContext, "GetSystemBootTime() failed with exception: {0})", exception.Message, null, "GetSystemBootTime", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Recovery\\RecoveryActionHelper.cs", 388);
						}
					}
				}
				dateTime = RecoveryActionHelper.currentBootTime;
			}
			return dateTime;
		}

		// Token: 0x06000267 RID: 615 RVA: 0x000082DC File Offset: 0x000064DC
		public static string GetShortServerName(string fqdn)
		{
			string result = fqdn;
			if (fqdn != null)
			{
				string[] array = fqdn.Split(new char[]
				{
					'.'
				});
				if (array != null && array.Length > 0)
				{
					result = array[0];
				}
			}
			return result;
		}

		// Token: 0x06000268 RID: 616 RVA: 0x00008310 File Offset: 0x00006510
		public static bool IsLocalServerName(string serverName)
		{
			bool result = false;
			if (serverName != null)
			{
				string shortServerName = RecoveryActionHelper.GetShortServerName(serverName);
				if (string.Equals(Dependencies.ThrottleHelper.Tunables.LocalMachineName, shortServerName, StringComparison.OrdinalIgnoreCase))
				{
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06000269 RID: 617 RVA: 0x00008344 File Offset: 0x00006544
		public static RecoveryActionEntry FindActionEntry(RecoveryActionId actionId, string resourceName, DateTime queryStartTime, DateTime queryEndTime)
		{
			RecoveryActionEntry result = null;
			bool flag = false;
			List<RecoveryActionEntry> list = RecoveryActionHelper.ReadEntries(actionId, null, null, RecoveryActionState.None, RecoveryActionResult.None, queryStartTime, queryEndTime, out flag, null, TimeSpan.MaxValue, 1);
			if (list.Count > 0)
			{
				result = list.First<RecoveryActionEntry>();
			}
			return result;
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0000837C File Offset: 0x0000657C
		public static void Sleep(TimeSpan duration, CancellationToken cancellationToken)
		{
			if (cancellationToken.WaitHandle != null)
			{
				cancellationToken.WaitHandle.WaitOne(duration);
				cancellationToken.ThrowIfCancellationRequested();
				return;
			}
			Thread.Sleep(duration);
		}

		// Token: 0x0600026B RID: 619 RVA: 0x000083A3 File Offset: 0x000065A3
		internal static string ConstructActionName(RecoveryActionId actionId, string resourceName)
		{
			return string.Format("{0}_{1}", actionId, resourceName);
		}

		// Token: 0x0600026C RID: 620 RVA: 0x000083B8 File Offset: 0x000065B8
		internal static string ConstructCondition(List<string> conditions, bool condition, string propertyName, object propValue)
		{
			string text = string.Empty;
			if (condition)
			{
				text = string.Format("{0}='{1}'", propertyName, propValue);
				conditions.Add(text);
			}
			return text;
		}

		// Token: 0x0600026D RID: 621 RVA: 0x000083E4 File Offset: 0x000065E4
		internal static string PrepareQueryConditionString(RecoveryActionId actionId, string resourceName, string instanceId, RecoveryActionState state, RecoveryActionResult result)
		{
			List<string> list = new List<string>();
			RecoveryActionHelper.ConstructCondition(list, actionId != RecoveryActionId.None, "Id", actionId);
			RecoveryActionHelper.ConstructCondition(list, !string.IsNullOrEmpty(resourceName), "ResourceName", resourceName);
			RecoveryActionHelper.ConstructCondition(list, !string.IsNullOrEmpty(instanceId), "InstanceId", instanceId);
			RecoveryActionHelper.ConstructCondition(list, state != RecoveryActionState.None, "State", state);
			RecoveryActionHelper.ConstructCondition(list, result != RecoveryActionResult.None, "Result", result);
			string result2 = null;
			if (list.Count > 0)
			{
				result2 = string.Format("({0})", string.Join(" and ", list.ToArray()));
			}
			return result2;
		}

		// Token: 0x0600026E RID: 622 RVA: 0x00008498 File Offset: 0x00006698
		internal static bool ForeachMatching(Func<RecoveryActionEntry, bool, bool> action, RecoveryActionId actionId, string resourceName, string instanceId, RecoveryActionState state, RecoveryActionResult result, DateTime startTime, DateTime endTime, out bool isMoreRecordsAvailable, string xpathQueryString, TimeSpan timeout, int maxCount = 4096)
		{
			isMoreRecordsAvailable = false;
			Stopwatch stopwatch = Stopwatch.StartNew();
			CrimsonReader<RecoveryActionEntry> crimsonReader = RecoveryActionHelper.PrepareReader(actionId, resourceName, instanceId, state, result, startTime, endTime, xpathQueryString);
			bool isReverseDirection = crimsonReader.IsReverseDirection;
			int num = 0;
			bool flag = false;
			for (;;)
			{
				RecoveryActionEntry recoveryActionEntry = crimsonReader.ReadNext();
				if (recoveryActionEntry == null)
				{
					return flag;
				}
				num++;
				flag = action(recoveryActionEntry, isReverseDirection);
				if (flag)
				{
					return flag;
				}
				if (maxCount != -1 && num > maxCount)
				{
					break;
				}
				if (stopwatch.Elapsed > timeout)
				{
					goto Block_5;
				}
				if (num % 1000 == 0)
				{
					Thread.Sleep(10);
				}
			}
			isMoreRecordsAvailable = true;
			return flag;
			Block_5:
			isMoreRecordsAvailable = true;
			throw new TimeoutException();
		}

		// Token: 0x0600026F RID: 623 RVA: 0x00008540 File Offset: 0x00006740
		internal static RecoveryActionHelper.RecoveryActionQuotaInfo GetRecoveryActionQuotaInfo(RecoveryActionId actionId, string resourceName, int maxAllowedQuota, DateTime queryStartTime, DateTime queryEndTime, TimeSpan timeout)
		{
			RecoveryActionHelper.RecoveryActionQuotaInfo quotaInfo = new RecoveryActionHelper.RecoveryActionQuotaInfo(actionId, resourceName, maxAllowedQuota);
			bool flag = false;
			quotaInfo.IsTimedout = RecoveryActionHelper.ForeachMatching((RecoveryActionEntry entry, bool isNewestToOldest) => quotaInfo.Update(entry), actionId, resourceName, null, RecoveryActionState.None, RecoveryActionResult.None, queryStartTime, queryEndTime, out flag, null, timeout, 4096);
			return quotaInfo;
		}

		// Token: 0x06000270 RID: 624 RVA: 0x000085B0 File Offset: 0x000067B0
		internal static List<RecoveryActionEntry> ReadEntries(RecoveryActionId actionId, string resourceName, string instanceId, RecoveryActionState state, RecoveryActionResult result, DateTime startTime, DateTime endTime, out bool isMoreRecordsAvailable, string xpathQueryString, TimeSpan timeout, int maxCount)
		{
			List<RecoveryActionEntry> entries = new List<RecoveryActionEntry>();
			RecoveryActionHelper.ForeachMatching(delegate(RecoveryActionEntry entry, bool isNewestToOldest)
			{
				entries.Add(entry);
				return false;
			}, actionId, resourceName, instanceId, state, result, startTime, endTime, out isMoreRecordsAvailable, null, timeout, maxCount);
			return entries;
		}

		// Token: 0x06000271 RID: 625 RVA: 0x000085F8 File Offset: 0x000067F8
		internal static CrimsonReader<RecoveryActionEntry> PrepareReader(RecoveryActionId actionId, string resourceName, string instanceId, RecoveryActionState state, RecoveryActionResult result, DateTime startTime, DateTime endTime, string xpathQueryString = null)
		{
			CrimsonReader<RecoveryActionEntry> crimsonReader = new CrimsonReader<RecoveryActionEntry>(null, null, "Microsoft-Exchange-ManagedAvailability/RecoveryActionResults");
			crimsonReader.IsReverseDirection = true;
			if (string.IsNullOrEmpty(xpathQueryString))
			{
				crimsonReader.QueryUserPropertyCondition = RecoveryActionHelper.PrepareQueryConditionString(actionId, resourceName, instanceId, state, result);
				crimsonReader.QueryStartTime = new DateTime?(startTime);
				crimsonReader.QueryEndTime = new DateTime?(endTime);
			}
			else
			{
				crimsonReader.ExplicitXPathQuery = xpathQueryString;
			}
			return crimsonReader;
		}

		// Token: 0x06000272 RID: 626 RVA: 0x00008658 File Offset: 0x00006858
		internal static RecoveryActionThrottlingMode GetRecoveryActionLocalThrottlingMode(RecoveryActionId actionId, RecoveryActionThrottlingMode defaultMode = RecoveryActionThrottlingMode.None)
		{
			return RegistryHelper.GetProperty<RecoveryActionThrottlingMode>("LocalThrottlingMode", defaultMode, string.Format("RecoveryAction\\{0}", actionId), null, false);
		}

		// Token: 0x06000273 RID: 627 RVA: 0x00008684 File Offset: 0x00006884
		internal static RecoveryActionThrottlingMode GetRecoveryActionDistributedThrottlingMode(RecoveryActionId actionId, RecoveryActionThrottlingMode defaultMode = RecoveryActionThrottlingMode.None)
		{
			return RegistryHelper.GetProperty<RecoveryActionThrottlingMode>("DistributedThrottlingMode", defaultMode, string.Format("RecoveryAction\\{0}", actionId), null, false);
		}

		// Token: 0x06000274 RID: 628 RVA: 0x000086B0 File Offset: 0x000068B0
		internal static RecoveryActionHelper.RecoveryActionEntrySerializable CreateSerializableRecoveryActionEntry(RecoveryActionEntry entry)
		{
			if (entry == null)
			{
				return null;
			}
			return new RecoveryActionHelper.RecoveryActionEntrySerializable(entry);
		}

		// Token: 0x0400017A RID: 378
		internal const string RecoveryActionChannelName = "Microsoft-Exchange-ManagedAvailability/RecoveryActionResults";

		// Token: 0x0400017B RID: 379
		private static object locker = new object();

		// Token: 0x0400017C RID: 380
		private static DateTime currentProcessStartTime = DateTime.MaxValue;

		// Token: 0x0400017D RID: 381
		private static DateTime currentBootTime = DateTime.MinValue;

		// Token: 0x0400017E RID: 382
		private static TracingContext traceContext = TracingContext.Default;

		// Token: 0x0400017F RID: 383
		private static bool isWorkerProcessInfoSentToHost = false;

		// Token: 0x02000042 RID: 66
		[Serializable]
		public class RecoveryActionEntrySerializable
		{
			// Token: 0x06000278 RID: 632 RVA: 0x000086F8 File Offset: 0x000068F8
			public RecoveryActionEntrySerializable(RecoveryActionEntry entry)
			{
				this.Id = entry.Id;
				this.InstanceId = entry.InstanceId;
				this.ResourceName = entry.ResourceName;
				this.StartTimeUtc = entry.StartTime.ToUniversalTime();
				this.EndTimeUtc = entry.EndTime.ToUniversalTime();
				this.State = entry.State;
				this.Result = entry.Result;
				this.RequestorName = entry.RequestorName;
				this.ExceptionName = entry.ExceptionName;
				this.ExceptionMessage = entry.ExceptionMessage;
				this.Context = entry.Context;
				this.CustomArg1 = entry.CustomArg1;
				this.CustomArg2 = entry.CustomArg2;
				this.CustomArg3 = entry.CustomArg3;
				this.LamProcessStartTime = entry.LamProcessStartTime;
				this.ThrottleIdentity = entry.ThrottleIdentity;
				this.ThrottleParametersXml = entry.ThrottleParametersXml;
				this.TotalLocalActionsInOneHour = entry.TotalLocalActionsInOneHour;
				this.TotalLocalActionsInOneDay = entry.TotalLocalActionsInOneDay;
				this.TotalGroupActionsInOneDay = entry.TotalGroupActionsInOneDay;
			}

			// Token: 0x170000D9 RID: 217
			// (get) Token: 0x06000279 RID: 633 RVA: 0x0000880B File Offset: 0x00006A0B
			// (set) Token: 0x0600027A RID: 634 RVA: 0x00008813 File Offset: 0x00006A13
			public RecoveryActionId Id { get; set; }

			// Token: 0x170000DA RID: 218
			// (get) Token: 0x0600027B RID: 635 RVA: 0x0000881C File Offset: 0x00006A1C
			// (set) Token: 0x0600027C RID: 636 RVA: 0x00008824 File Offset: 0x00006A24
			public string InstanceId { get; set; }

			// Token: 0x170000DB RID: 219
			// (get) Token: 0x0600027D RID: 637 RVA: 0x0000882D File Offset: 0x00006A2D
			// (set) Token: 0x0600027E RID: 638 RVA: 0x00008835 File Offset: 0x00006A35
			public string ResourceName { get; set; }

			// Token: 0x170000DC RID: 220
			// (get) Token: 0x0600027F RID: 639 RVA: 0x0000883E File Offset: 0x00006A3E
			// (set) Token: 0x06000280 RID: 640 RVA: 0x00008846 File Offset: 0x00006A46
			public DateTime StartTimeUtc { get; set; }

			// Token: 0x170000DD RID: 221
			// (get) Token: 0x06000281 RID: 641 RVA: 0x00008850 File Offset: 0x00006A50
			public DateTime StartTime
			{
				get
				{
					if (this.startTime == null)
					{
						this.startTime = new DateTime?(this.StartTimeUtc.ToLocalTime());
					}
					return this.startTime.Value;
				}
			}

			// Token: 0x170000DE RID: 222
			// (get) Token: 0x06000282 RID: 642 RVA: 0x0000888E File Offset: 0x00006A8E
			// (set) Token: 0x06000283 RID: 643 RVA: 0x00008896 File Offset: 0x00006A96
			public DateTime EndTimeUtc { get; set; }

			// Token: 0x170000DF RID: 223
			// (get) Token: 0x06000284 RID: 644 RVA: 0x000088A0 File Offset: 0x00006AA0
			public DateTime EndTime
			{
				get
				{
					if (this.endTime == null)
					{
						this.endTime = new DateTime?(this.EndTimeUtc.ToLocalTime());
					}
					return this.endTime.Value;
				}
			}

			// Token: 0x170000E0 RID: 224
			// (get) Token: 0x06000285 RID: 645 RVA: 0x000088DE File Offset: 0x00006ADE
			// (set) Token: 0x06000286 RID: 646 RVA: 0x000088E6 File Offset: 0x00006AE6
			public RecoveryActionState State { get; set; }

			// Token: 0x170000E1 RID: 225
			// (get) Token: 0x06000287 RID: 647 RVA: 0x000088EF File Offset: 0x00006AEF
			// (set) Token: 0x06000288 RID: 648 RVA: 0x000088F7 File Offset: 0x00006AF7
			public RecoveryActionResult Result { get; set; }

			// Token: 0x170000E2 RID: 226
			// (get) Token: 0x06000289 RID: 649 RVA: 0x00008900 File Offset: 0x00006B00
			// (set) Token: 0x0600028A RID: 650 RVA: 0x00008908 File Offset: 0x00006B08
			public string RequestorName { get; set; }

			// Token: 0x170000E3 RID: 227
			// (get) Token: 0x0600028B RID: 651 RVA: 0x00008911 File Offset: 0x00006B11
			// (set) Token: 0x0600028C RID: 652 RVA: 0x00008919 File Offset: 0x00006B19
			public string ExceptionName { get; set; }

			// Token: 0x170000E4 RID: 228
			// (get) Token: 0x0600028D RID: 653 RVA: 0x00008922 File Offset: 0x00006B22
			// (set) Token: 0x0600028E RID: 654 RVA: 0x0000892A File Offset: 0x00006B2A
			public string ExceptionMessage { get; set; }

			// Token: 0x170000E5 RID: 229
			// (get) Token: 0x0600028F RID: 655 RVA: 0x00008933 File Offset: 0x00006B33
			// (set) Token: 0x06000290 RID: 656 RVA: 0x0000893B File Offset: 0x00006B3B
			public string Context { get; set; }

			// Token: 0x170000E6 RID: 230
			// (get) Token: 0x06000291 RID: 657 RVA: 0x00008944 File Offset: 0x00006B44
			// (set) Token: 0x06000292 RID: 658 RVA: 0x0000894C File Offset: 0x00006B4C
			public string CustomArg1 { get; set; }

			// Token: 0x170000E7 RID: 231
			// (get) Token: 0x06000293 RID: 659 RVA: 0x00008955 File Offset: 0x00006B55
			// (set) Token: 0x06000294 RID: 660 RVA: 0x0000895D File Offset: 0x00006B5D
			public string CustomArg2 { get; set; }

			// Token: 0x170000E8 RID: 232
			// (get) Token: 0x06000295 RID: 661 RVA: 0x00008966 File Offset: 0x00006B66
			// (set) Token: 0x06000296 RID: 662 RVA: 0x0000896E File Offset: 0x00006B6E
			public string CustomArg3 { get; set; }

			// Token: 0x170000E9 RID: 233
			// (get) Token: 0x06000297 RID: 663 RVA: 0x00008977 File Offset: 0x00006B77
			// (set) Token: 0x06000298 RID: 664 RVA: 0x0000897F File Offset: 0x00006B7F
			public DateTime LamProcessStartTime { get; set; }

			// Token: 0x170000EA RID: 234
			// (get) Token: 0x06000299 RID: 665 RVA: 0x00008988 File Offset: 0x00006B88
			// (set) Token: 0x0600029A RID: 666 RVA: 0x00008990 File Offset: 0x00006B90
			public string ThrottleIdentity { get; set; }

			// Token: 0x170000EB RID: 235
			// (get) Token: 0x0600029B RID: 667 RVA: 0x00008999 File Offset: 0x00006B99
			// (set) Token: 0x0600029C RID: 668 RVA: 0x000089A1 File Offset: 0x00006BA1
			public string ThrottleParametersXml { get; set; }

			// Token: 0x170000EC RID: 236
			// (get) Token: 0x0600029D RID: 669 RVA: 0x000089AA File Offset: 0x00006BAA
			// (set) Token: 0x0600029E RID: 670 RVA: 0x000089B2 File Offset: 0x00006BB2
			public int TotalLocalActionsInOneHour { get; set; }

			// Token: 0x170000ED RID: 237
			// (get) Token: 0x0600029F RID: 671 RVA: 0x000089BB File Offset: 0x00006BBB
			// (set) Token: 0x060002A0 RID: 672 RVA: 0x000089C3 File Offset: 0x00006BC3
			public int TotalLocalActionsInOneDay { get; set; }

			// Token: 0x170000EE RID: 238
			// (get) Token: 0x060002A1 RID: 673 RVA: 0x000089CC File Offset: 0x00006BCC
			// (set) Token: 0x060002A2 RID: 674 RVA: 0x000089D4 File Offset: 0x00006BD4
			public int TotalGroupActionsInOneDay { get; set; }

			// Token: 0x04000181 RID: 385
			private DateTime? startTime;

			// Token: 0x04000182 RID: 386
			private DateTime? endTime;
		}

		// Token: 0x02000043 RID: 67
		[Serializable]
		public class RecoveryActionQuotaInfo
		{
			// Token: 0x060002A3 RID: 675 RVA: 0x000089DD File Offset: 0x00006BDD
			internal RecoveryActionQuotaInfo(RecoveryActionId actionId, string resourceName, int maxAllowedQuota)
			{
				this.ActionId = actionId;
				this.ResourceName = resourceName;
				this.MaxAllowedQuota = maxAllowedQuota;
				this.RemainingQuota = maxAllowedQuota;
				this.LastSystemBootTime = RecoveryActionHelper.GetSystemBootTime();
			}

			// Token: 0x170000EF RID: 239
			// (get) Token: 0x060002A4 RID: 676 RVA: 0x00008A0C File Offset: 0x00006C0C
			// (set) Token: 0x060002A5 RID: 677 RVA: 0x00008A14 File Offset: 0x00006C14
			public RecoveryActionId ActionId { get; private set; }

			// Token: 0x170000F0 RID: 240
			// (get) Token: 0x060002A6 RID: 678 RVA: 0x00008A1D File Offset: 0x00006C1D
			// (set) Token: 0x060002A7 RID: 679 RVA: 0x00008A25 File Offset: 0x00006C25
			public string ResourceName { get; private set; }

			// Token: 0x170000F1 RID: 241
			// (get) Token: 0x060002A8 RID: 680 RVA: 0x00008A2E File Offset: 0x00006C2E
			// (set) Token: 0x060002A9 RID: 681 RVA: 0x00008A36 File Offset: 0x00006C36
			public int RemainingQuota { get; private set; }

			// Token: 0x170000F2 RID: 242
			// (get) Token: 0x060002AA RID: 682 RVA: 0x00008A3F File Offset: 0x00006C3F
			// (set) Token: 0x060002AB RID: 683 RVA: 0x00008A47 File Offset: 0x00006C47
			public int MaxAllowedQuota { get; private set; }

			// Token: 0x170000F3 RID: 243
			// (get) Token: 0x060002AC RID: 684 RVA: 0x00008A50 File Offset: 0x00006C50
			// (set) Token: 0x060002AD RID: 685 RVA: 0x00008A58 File Offset: 0x00006C58
			public RecoveryActionHelper.RecoveryActionEntrySerializable LastStartedEntry { get; private set; }

			// Token: 0x170000F4 RID: 244
			// (get) Token: 0x060002AE RID: 686 RVA: 0x00008A61 File Offset: 0x00006C61
			// (set) Token: 0x060002AF RID: 687 RVA: 0x00008A69 File Offset: 0x00006C69
			public RecoveryActionHelper.RecoveryActionEntrySerializable LastSucceededEntry { get; private set; }

			// Token: 0x170000F5 RID: 245
			// (get) Token: 0x060002B0 RID: 688 RVA: 0x00008A72 File Offset: 0x00006C72
			// (set) Token: 0x060002B1 RID: 689 RVA: 0x00008A7A File Offset: 0x00006C7A
			public DateTime LastSystemBootTime { get; private set; }

			// Token: 0x170000F6 RID: 246
			// (get) Token: 0x060002B2 RID: 690 RVA: 0x00008A83 File Offset: 0x00006C83
			// (set) Token: 0x060002B3 RID: 691 RVA: 0x00008A8B File Offset: 0x00006C8B
			public string CustomArg { get; set; }

			// Token: 0x170000F7 RID: 247
			// (get) Token: 0x060002B4 RID: 692 RVA: 0x00008A94 File Offset: 0x00006C94
			// (set) Token: 0x060002B5 RID: 693 RVA: 0x00008A9C File Offset: 0x00006C9C
			public bool IsTimedout { get; set; }

			// Token: 0x060002B6 RID: 694 RVA: 0x00008AA8 File Offset: 0x00006CA8
			public bool Update(RecoveryActionEntry entry)
			{
				bool result = false;
				switch (entry.State)
				{
				case RecoveryActionState.Started:
					if (this.LastStartedEntry == null || entry.StartTime > this.LastStartedEntry.StartTime)
					{
						this.LastStartedEntry = new RecoveryActionHelper.RecoveryActionEntrySerializable(entry);
					}
					break;
				case RecoveryActionState.Finished:
					if (entry.Result == RecoveryActionResult.Succeeded)
					{
						this.RemainingQuota--;
						if (this.RemainingQuota == 0)
						{
							result = true;
						}
						if (this.LastSucceededEntry == null || entry.StartTime > this.LastSucceededEntry.StartTime)
						{
							this.LastSucceededEntry = new RecoveryActionHelper.RecoveryActionEntrySerializable(entry);
						}
					}
					break;
				}
				return result;
			}
		}
	}
}
