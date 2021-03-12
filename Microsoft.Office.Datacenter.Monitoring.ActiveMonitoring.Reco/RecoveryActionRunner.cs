using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery
{
	// Token: 0x02000044 RID: 68
	public class RecoveryActionRunner
	{
		// Token: 0x060002B7 RID: 695 RVA: 0x00008B4C File Offset: 0x00006D4C
		public RecoveryActionRunner(RecoveryActionId actionId, string resourceName, ResponderWorkItem responderWorkItem, bool isThrowOnException, CancellationToken cancellationToken, string localServerName = null)
		{
			GlobalTunables tunables = Dependencies.ThrottleHelper.Tunables;
			if (!tunables.IsRunningMock || string.IsNullOrEmpty(localServerName))
			{
				localServerName = Dependencies.ThrottleHelper.Tunables.LocalMachineName;
			}
			this.localServerName = localServerName;
			this.TraceContext = TracingContext.Default;
			this.ActionId = actionId;
			this.ResourceName = resourceName;
			this.responderWorkItem = responderWorkItem;
			this.RequesterName = responderWorkItem.Definition.Name;
			this.isThrowOnException = isThrowOnException;
			this.timeout = TimeSpan.FromSeconds((double)responderWorkItem.Definition.TimeoutSeconds);
			this.InstanceId = Interlocked.Increment(ref RecoveryActionRunner.globalInstanceCounter);
			this.inProgressTracker = new ThrottlingProgressTracker(this.InstanceId, actionId, resourceName, this.RequesterName, this.timeout);
			FixedThrottleEntry throttleDefinition = Dependencies.ThrottleHelper.Settings.GetThrottleDefinition(actionId, resourceName, this.responderWorkItem.Definition);
			this.throttleGroupName = this.responderWorkItem.Definition.ThrottleGroupName;
			this.throttleParameters = throttleDefinition.ThrottleParameters;
			this.throttleIdentity = CrimsonHelper.NullCode(throttleDefinition.Identity);
			this.throttlePropertiesXml = CrimsonHelper.NullCode(throttleDefinition.GetThrottlePropertiesAsXml().ToString());
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060002B8 RID: 696 RVA: 0x00008C79 File Offset: 0x00006E79
		// (set) Token: 0x060002B9 RID: 697 RVA: 0x00008C81 File Offset: 0x00006E81
		public bool IgnoreGroupThrottlingWhenMajorityNotSucceeded { get; set; }

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060002BA RID: 698 RVA: 0x00008C8A File Offset: 0x00006E8A
		// (set) Token: 0x060002BB RID: 699 RVA: 0x00008C92 File Offset: 0x00006E92
		public bool IsIgnoreResourceName { get; set; }

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060002BC RID: 700 RVA: 0x00008C9B File Offset: 0x00006E9B
		// (set) Token: 0x060002BD RID: 701 RVA: 0x00008CA3 File Offset: 0x00006EA3
		internal RecoveryActionId ActionId { get; private set; }

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060002BE RID: 702 RVA: 0x00008CAC File Offset: 0x00006EAC
		// (set) Token: 0x060002BF RID: 703 RVA: 0x00008CB4 File Offset: 0x00006EB4
		internal string ResourceName { get; private set; }

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060002C0 RID: 704 RVA: 0x00008CBD File Offset: 0x00006EBD
		// (set) Token: 0x060002C1 RID: 705 RVA: 0x00008CC5 File Offset: 0x00006EC5
		internal string RequesterName { get; private set; }

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060002C2 RID: 706 RVA: 0x00008CCE File Offset: 0x00006ECE
		// (set) Token: 0x060002C3 RID: 707 RVA: 0x00008CD6 File Offset: 0x00006ED6
		internal long InstanceId { get; private set; }

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060002C4 RID: 708 RVA: 0x00008CDF File Offset: 0x00006EDF
		// (set) Token: 0x060002C5 RID: 709 RVA: 0x00008CE7 File Offset: 0x00006EE7
		internal int TotalServersInGroup { get; private set; }

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060002C6 RID: 710 RVA: 0x00008CF0 File Offset: 0x00006EF0
		// (set) Token: 0x060002C7 RID: 711 RVA: 0x00008CF8 File Offset: 0x00006EF8
		internal LocalThrottlingResult LocalThrottleResult { get; private set; }

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060002C8 RID: 712 RVA: 0x00008D01 File Offset: 0x00006F01
		// (set) Token: 0x060002C9 RID: 713 RVA: 0x00008D09 File Offset: 0x00006F09
		internal GroupThrottlingResult GroupThrottleResult { get; private set; }

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060002CA RID: 714 RVA: 0x00008D12 File Offset: 0x00006F12
		// (set) Token: 0x060002CB RID: 715 RVA: 0x00008D1A File Offset: 0x00006F1A
		internal bool IsSynchronousDistributedCheck { get; set; }

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060002CC RID: 716 RVA: 0x00008D23 File Offset: 0x00006F23
		// (set) Token: 0x060002CD RID: 717 RVA: 0x00008D2B File Offset: 0x00006F2B
		private protected TracingContext TraceContext { protected get; private set; }

		// Token: 0x060002CE RID: 718 RVA: 0x00008D34 File Offset: 0x00006F34
		public static void SetThrottleProperties(ResponderDefinition responderDefinition, string throttleGroupName, RecoveryActionId actionId, string resourceName, string[] serversInGroup = null)
		{
			if (throttleGroupName == null)
			{
				throttleGroupName = string.Empty;
			}
			responderDefinition.ThrottleGroupName = throttleGroupName;
			responderDefinition.ThrottlePolicyXml = Dependencies.ThrottleHelper.Settings.GetThrottleDefinitionsAsCompactXml(actionId, resourceName, responderDefinition);
			ThrottleGroupCache.Instance.AddGroup(throttleGroupName);
			ThrottleGroupCache.Instance.AddServers(serversInGroup);
		}

		// Token: 0x060002CF RID: 719 RVA: 0x00008D98 File Offset: 0x00006F98
		public void Execute(Action action)
		{
			this.Execute(delegate(RecoveryActionEntry startEntry)
			{
				action();
			});
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x00008DC4 File Offset: 0x00006FC4
		public void Execute(Action<RecoveryActionEntry> action)
		{
			bool flag = false;
			Exception exception = null;
			if (this.IsIgnoreResourceName)
			{
				Thread.Sleep(new Random().Next() % 1000);
			}
			try
			{
				this.inProgressTracker.MarkBegin();
				this.CheckLocalThrottleLimits();
				this.CheckGroupThrottleLimits();
				flag = true;
			}
			catch (Exception ex)
			{
				exception = ex;
				if (ex is ThrottlingRejectedOperationException)
				{
					this.responderWorkItem.Result.IsThrottled = true;
				}
				if (this.isThrowOnException)
				{
					throw;
				}
			}
			finally
			{
				this.inProgressTracker.MarkEnd();
				this.LogThrottleResults(flag, exception);
			}
			if (flag && action != null)
			{
				this.PerformAction(action);
			}
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x00008E74 File Offset: 0x00007074
		public string[] GetServersInGroup()
		{
			string[] array = null;
			if (!string.IsNullOrEmpty(this.throttleGroupName))
			{
				array = ThrottleGroupCache.Instance.GetServersInGroup(this.throttleGroupName, true);
			}
			if (Utilities.IsSequenceNullOrEmpty<string>(array))
			{
				array = this.throttleServersInGroup;
			}
			this.TotalServersInGroup = ((array != null) ? array.Length : 0);
			return array;
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x00008EC1 File Offset: 0x000070C1
		public void SetServersInGroup(string[] serversInGroup)
		{
			this.throttleServersInGroup = serversInGroup;
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x00008ECA File Offset: 0x000070CA
		public void VerifyThrottleLimitsNotExceeded()
		{
			this.Execute(null);
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x00008ED4 File Offset: 0x000070D4
		internal void PerformAction(Action<RecoveryActionEntry> action)
		{
			Exception ex = null;
			RecoveryActionEntry recoveryActionEntry = null;
			try
			{
				ManagedAvailabilityCrimsonEvents.StartingRecoveryAction.LogGeneric(new object[]
				{
					this.ActionId,
					this.ResourceName,
					this.RequesterName,
					RecoveryActionThrottlingMode.None
				});
				recoveryActionEntry = this.PublishStartEntry();
				this.inProgressTracker.MarkEnd();
				action(recoveryActionEntry);
			}
			catch (Exception ex2)
			{
				ex = ex2;
				if (this.isThrowOnException)
				{
					throw;
				}
			}
			finally
			{
				if (recoveryActionEntry != null)
				{
					this.PublishFinishEntry(recoveryActionEntry, ex);
					if (ex == null)
					{
						ManagedAvailabilityCrimsonEvents.SuccessfulyFinishedRecoveryAction.Log<RecoveryActionId, string, string, string, string, string>(this.ActionId, this.ResourceName, this.RequesterName, "<none>", this.throttleIdentity, this.throttlePropertiesXml);
					}
					else
					{
						ManagedAvailabilityCrimsonEvents.FailedToFinishedRecoveryAction.Log<RecoveryActionId, string, string, string, string, string>(this.ActionId, this.ResourceName, this.RequesterName, ex.ToString(), this.throttleIdentity, this.throttlePropertiesXml);
					}
				}
			}
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x00008FD8 File Offset: 0x000071D8
		internal void LogThrottleResults(bool isThrottlingPassed, Exception exception = null)
		{
			ManagedAvailabilityCrimsonEvent managedAvailabilityCrimsonEvent = ManagedAvailabilityCrimsonEvents.ThrottlingAllowedOperationV2;
			if (!isThrottlingPassed)
			{
				managedAvailabilityCrimsonEvent = ManagedAvailabilityCrimsonEvents.ThrottlingRejectedOperationV2;
			}
			managedAvailabilityCrimsonEvent.LogGeneric(new object[]
			{
				this.InstanceId,
				this.ActionId,
				this.ResourceName,
				this.RequesterName,
				(exception == null) ? "<none>" : exception.Message,
				(this.LocalThrottleResult != null) ? this.LocalThrottleResult.ToXml(false) : "<not attempted>",
				(this.GroupThrottleResult != null) ? this.GroupThrottleResult.ToXml(false) : "<not attempted>",
				this.TotalServersInGroup,
				this.TotalServersInGroup
			});
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x0000909C File Offset: 0x0000729C
		internal RecoveryActionEntry PublishStartEntry()
		{
			RecoveryActionEntry recoveryActionEntry = RecoveryActionHelper.ConstructStartActionEntry(this.ActionId, this.ResourceName, this.RequesterName, ExDateTime.Now.LocalTime + this.timeout, this.throttleIdentity, this.throttlePropertiesXml, null, null);
			this.FillQuotaInfo(recoveryActionEntry);
			this.PublishRecoveryActionEntry(recoveryActionEntry);
			return recoveryActionEntry;
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x000090F8 File Offset: 0x000072F8
		internal RecoveryActionEntry PublishFinishEntry(RecoveryActionEntry startEntry, Exception exception)
		{
			RecoveryActionEntry recoveryActionEntry = RecoveryActionHelper.ConstructFinishActionEntry(startEntry, exception, null, null);
			this.FillQuotaInfo(recoveryActionEntry);
			this.PublishRecoveryActionEntry(recoveryActionEntry);
			return recoveryActionEntry;
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x00009128 File Offset: 0x00007328
		internal void PublishRecoveryActionEntry(RecoveryActionEntry entry)
		{
			try
			{
				Dependencies.LamRpc.UpdateRecoveryActionEntry(this.localServerName, entry, 30000);
			}
			catch (Exception ex)
			{
				ManagedAvailabilityCrimsonEvents.FailedToPublishRecoveryActionEntry.Log<RecoveryActionId, string, string, string, string>(entry.Id, entry.InstanceId, entry.ResourceName, entry.RequestorName, ex.Message);
				throw;
			}
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x00009188 File Offset: 0x00007388
		internal void FillQuotaInfo(RecoveryActionEntry entry)
		{
			string str = string.Empty;
			if (this.LocalThrottleResult != null)
			{
				entry.TotalLocalActionsInOneHour = this.LocalThrottleResult.TotalInOneHour;
				entry.TotalLocalActionsInOneDay = this.LocalThrottleResult.TotalInOneDay;
				str = this.LocalThrottleResult.ToXml(false);
			}
			string str2 = string.Empty;
			if (this.GroupThrottleResult != null)
			{
				entry.TotalGroupActionsInOneDay = this.GroupThrottleResult.TotalInOneDay;
				str2 = this.GroupThrottleResult.ToXml(false);
			}
			entry.Context = str + Environment.NewLine + str2;
		}

		// Token: 0x060002DA RID: 730 RVA: 0x00009210 File Offset: 0x00007410
		internal void CheckLocalThrottleLimits()
		{
			string resourceName = this.IsIgnoreResourceName ? null : this.ResourceName;
			RpcGetThrottlingStatisticsImpl.ThrottlingStatistics throttlingStatistics = Dependencies.LamRpc.GetThrottlingStatistics(this.localServerName, this.ActionId, resourceName, this.throttleParameters.LocalMaximumAllowedAttemptsInOneHour, this.throttleParameters.LocalMaximumAllowedAttemptsInADay, false, true, 35000);
			DateTime localTime = ExDateTime.Now.LocalTime;
			DateTime dateTime = DateTime.MinValue;
			RecoveryActionRunner.ThrottleFailedChecks throttleFailedChecks = RecoveryActionRunner.ThrottleFailedChecks.None;
			if (this.throttleParameters.LocalMinimumMinutesBetweenAttempts != -1 && throttlingStatistics.MostRecentEntry != null)
			{
				TimeSpan t = localTime - throttlingStatistics.MostRecentEntry.EndTime;
				TimeSpan timeSpan = TimeSpan.FromMinutes((double)this.throttleParameters.LocalMinimumMinutesBetweenAttempts);
				if (t < timeSpan)
				{
					throttleFailedChecks |= RecoveryActionRunner.ThrottleFailedChecks.LocalMinimumMinutes;
					dateTime = throttlingStatistics.MostRecentEntry.EndTime + timeSpan;
				}
			}
			if (this.throttleParameters.LocalMaximumAllowedAttemptsInOneHour != -1 && throttlingStatistics.NumberOfActionsInOneHour >= this.throttleParameters.LocalMaximumAllowedAttemptsInOneHour)
			{
				throttleFailedChecks |= RecoveryActionRunner.ThrottleFailedChecks.LocalMaxInHour;
				if (throttlingStatistics.EntryExceedingOneHourLimit != null)
				{
					DateTime dateTime2 = throttlingStatistics.EntryExceedingOneHourLimit.EndTime + TimeSpan.FromHours(1.0);
					if (dateTime2 > dateTime)
					{
						dateTime = dateTime2;
					}
				}
			}
			if (this.throttleParameters.LocalMaximumAllowedAttemptsInADay != -1 && throttlingStatistics.NumberOfActionsInOneDay >= this.throttleParameters.LocalMaximumAllowedAttemptsInADay)
			{
				throttleFailedChecks |= RecoveryActionRunner.ThrottleFailedChecks.LocalMaxInDay;
				if (throttlingStatistics.EntryExceedingOneDayLimit != null)
				{
					DateTime dateTime3 = throttlingStatistics.EntryExceedingOneDayLimit.EndTime + TimeSpan.FromDays(1.0);
					if (dateTime3 > dateTime)
					{
						dateTime = dateTime3;
					}
				}
			}
			if (throttlingStatistics.IsThrottlingInProgress && throttlingStatistics.ThrottleProgressInfo.InstanceId != this.InstanceId)
			{
				throttleFailedChecks |= RecoveryActionRunner.ThrottleFailedChecks.InProgressMismatch;
			}
			if (throttlingStatistics.IsRecoveryInProgress)
			{
				throttleFailedChecks |= RecoveryActionRunner.ThrottleFailedChecks.RecoveryInProgress;
			}
			string text = (throttleFailedChecks != RecoveryActionRunner.ThrottleFailedChecks.None) ? throttleFailedChecks.ToString() : string.Empty;
			LocalThrottlingResult localThrottleResult = new LocalThrottlingResult
			{
				IsPassed = (throttleFailedChecks == RecoveryActionRunner.ThrottleFailedChecks.None),
				MostRecentEntry = throttlingStatistics.MostRecentEntry,
				MinimumMinutes = this.throttleParameters.LocalMinimumMinutesBetweenAttempts,
				TotalInOneHour = throttlingStatistics.NumberOfActionsInOneHour,
				MaxAllowedInOneHour = this.throttleParameters.LocalMaximumAllowedAttemptsInOneHour,
				TotalInOneDay = throttlingStatistics.NumberOfActionsInOneDay,
				MaxAllowedInOneDay = this.throttleParameters.LocalMaximumAllowedAttemptsInADay,
				IsThrottlingInProgress = throttlingStatistics.IsThrottlingInProgress,
				IsRecoveryInProgress = throttlingStatistics.IsRecoveryInProgress,
				ChecksFailed = text,
				TimeToRetryAfter = dateTime
			};
			this.LocalThrottleResult = localThrottleResult;
			if (throttleFailedChecks != RecoveryActionRunner.ThrottleFailedChecks.None)
			{
				throw new LocalThrottlingRejectedOperationException(this.ActionId.ToString(), this.ResourceName, this.RequesterName, text);
			}
		}

		// Token: 0x060002DB RID: 731 RVA: 0x00009504 File Offset: 0x00007704
		internal void CheckGroupThrottleLimits()
		{
			DateTime timeToRetryAfter = DateTime.MinValue;
			if (string.IsNullOrEmpty(this.throttleGroupName) && Utilities.IsSequenceNullOrEmpty<string>(this.throttleServersInGroup))
			{
				this.GroupThrottleResult = new GroupThrottlingResult
				{
					IsPassed = true,
					Comment = "Neither ThrottleGroupName or ServersInGroup are specified. Allowing the operation for backward compatibility"
				};
				return;
			}
			string[] serversInGroup = this.GetServersInGroup();
			if (Utilities.IsSequenceNullOrEmpty<string>(serversInGroup))
			{
				if (this.IgnoreGroupThrottlingWhenMajorityNotSucceeded)
				{
					this.GroupThrottleResult = new GroupThrottlingResult
					{
						IsPassed = true,
						Comment = "No servers were detected in the group. Allowing the operation to continue since IgnoreGroupThrottlingWhenMajorityNotSucceeded is specified."
					};
					return;
				}
				string text = "ServersEmpty";
				this.GroupThrottleResult = new GroupThrottlingResult
				{
					IsPassed = false,
					ChecksFailed = text,
					Comment = "Not a single server is found in the group, but group throttling is requested"
				};
				throw new GroupThrottlingRejectedOperationException(this.ActionId.ToString(), this.ResourceName, this.RequesterName, text);
			}
			else
			{
				ConcurrentDictionary<string, Exception> exceptionsByServer;
				Dictionary<string, RpcGetThrottlingStatisticsImpl.ThrottlingStatistics> throttleStatisticsAcrossGroup = this.GetThrottleStatisticsAcrossGroup(serversInGroup, out exceptionsByServer);
				int count = throttleStatisticsAcrossGroup.Count;
				int num = throttleStatisticsAcrossGroup.Count((KeyValuePair<string, RpcGetThrottlingStatisticsImpl.ThrottlingStatistics> kv) => kv.Value != null);
				RecoveryActionHelper.RecoveryActionEntrySerializable recoveryActionEntrySerializable = null;
				int num2 = 0;
				RpcGetThrottlingStatisticsImpl.ThrottlingStatistics[] array = throttleStatisticsAcrossGroup.Values.ToArray<RpcGetThrottlingStatisticsImpl.ThrottlingStatistics>();
				foreach (RpcGetThrottlingStatisticsImpl.ThrottlingStatistics throttlingStatistics in array)
				{
					if (throttlingStatistics != null)
					{
						if (recoveryActionEntrySerializable == null || (throttlingStatistics.MostRecentEntry != null && throttlingStatistics.MostRecentEntry.EndTime > recoveryActionEntrySerializable.EndTime))
						{
							recoveryActionEntrySerializable = throttlingStatistics.MostRecentEntry;
						}
						num2 += throttlingStatistics.NumberOfActionsInOneDay;
					}
					else if (this.throttleParameters.LocalMaximumAllowedAttemptsInADay != -1)
					{
						num2 += this.throttleParameters.LocalMaximumAllowedAttemptsInADay;
					}
				}
				RecoveryActionRunner.ThrottleFailedChecks throttleFailedChecks = RecoveryActionRunner.ThrottleFailedChecks.None;
				if (this.throttleParameters.GroupMinimumMinutesBetweenAttempts != -1 && recoveryActionEntrySerializable != null)
				{
					DateTime localTime = ExDateTime.Now.LocalTime;
					TimeSpan t = localTime - recoveryActionEntrySerializable.EndTime;
					TimeSpan t2 = TimeSpan.FromMinutes((double)this.throttleParameters.GroupMinimumMinutesBetweenAttempts);
					if (t < TimeSpan.FromMinutes((double)this.throttleParameters.GroupMinimumMinutesBetweenAttempts))
					{
						throttleFailedChecks |= RecoveryActionRunner.ThrottleFailedChecks.GroupMinimumMinutes;
						timeToRetryAfter = recoveryActionEntrySerializable.EndTime + t2;
					}
				}
				if (this.throttleParameters.GroupMaximumAllowedAttemptsInADay != -1 && num2 >= this.throttleParameters.GroupMaximumAllowedAttemptsInADay)
				{
					throttleFailedChecks |= RecoveryActionRunner.ThrottleFailedChecks.GroupMaxInDay;
				}
				string[] array3 = (from s in array
				where s != null && s.IsThrottlingInProgress
				orderby s.ThrottleProgressInfo.OperationStartTime, s.ServerName
				select s.ServerName).ToArray<string>();
				string text2 = array3.FirstOrDefault<string>();
				if (!string.IsNullOrEmpty(text2) && !string.Equals(text2, this.localServerName))
				{
					throttleFailedChecks |= RecoveryActionRunner.ThrottleFailedChecks.GroupThrottlingInProgress;
				}
				string[] array4 = (from s in array
				where s != null && s.IsRecoveryInProgress
				orderby s.ServerName
				select s.ServerName).ToArray<string>();
				if (array4.Length > 0)
				{
					throttleFailedChecks |= RecoveryActionRunner.ThrottleFailedChecks.GroupRecoveryInProgress;
				}
				string text3 = (throttleFailedChecks != RecoveryActionRunner.ThrottleFailedChecks.None) ? throttleFailedChecks.ToString() : string.Empty;
				GroupThrottlingResult groupThrottlingResult = new GroupThrottlingResult
				{
					IsPassed = (throttleFailedChecks == RecoveryActionRunner.ThrottleFailedChecks.None),
					TotalRequestsSent = count,
					TotalRequestsSucceeded = num,
					MostRecentEntry = recoveryActionEntrySerializable,
					MinimumMinutes = this.throttleParameters.GroupMinimumMinutesBetweenAttempts,
					TotalInOneDay = num2,
					MaxAllowedInOneDay = this.throttleParameters.GroupMaximumAllowedAttemptsInADay,
					ThrottlingInProgressServers = array3,
					RecoveryInProgressServers = array4,
					ChecksFailed = text3,
					TimeToRetryAfter = timeToRetryAfter,
					GroupStats = throttleStatisticsAcrossGroup,
					ExceptionsByServer = exceptionsByServer
				};
				this.GroupThrottleResult = groupThrottlingResult;
				if (throttleFailedChecks == RecoveryActionRunner.ThrottleFailedChecks.None)
				{
					return;
				}
				if (throttleFailedChecks.HasFlag(RecoveryActionRunner.ThrottleFailedChecks.GroupMaxInDay) && this.IgnoreGroupThrottlingWhenMajorityNotSucceeded && num <= throttleStatisticsAcrossGroup.Count / 2)
				{
					groupThrottlingResult.IsPassed = true;
					groupThrottlingResult.Comment = "Allowing the operation to continue since majority requests did not succeed, and  IgnoreGroupThrottlingWhenMajorityNotSucceeded is requested";
					return;
				}
				throw new GroupThrottlingRejectedOperationException(this.ActionId.ToString(), this.ResourceName, this.RequesterName, text3);
			}
		}

		// Token: 0x060002DC RID: 732 RVA: 0x00009A38 File Offset: 0x00007C38
		private Dictionary<string, RpcGetThrottlingStatisticsImpl.ThrottlingStatistics> GetThrottleStatisticsAcrossGroup(string[] servers, out ConcurrentDictionary<string, Exception> exceptionsByServer)
		{
			Dictionary<string, RpcGetThrottlingStatisticsImpl.ThrottlingStatistics> groupStats = new Dictionary<string, RpcGetThrottlingStatisticsImpl.ThrottlingStatistics>();
			exceptionsByServer = null;
			if (servers == null || servers.Length == 0)
			{
				return groupStats;
			}
			foreach (string key in servers)
			{
				groupStats[key] = null;
			}
			using (DistributedAction distributedAction = new DistributedAction(servers, servers.Length, this.IsSynchronousDistributedCheck))
			{
				string resourceName = this.IsIgnoreResourceName ? null : this.ResourceName;
				distributedAction.Run(delegate(string serverName)
				{
					RpcGetThrottlingStatisticsImpl.ThrottlingStatistics throttlingStatistics = Dependencies.LamRpc.GetThrottlingStatistics(serverName, this.ActionId, resourceName, -1, -1, false, true, 35000);
					Dictionary<string, RpcGetThrottlingStatisticsImpl.ThrottlingStatistics> groupStats;
					lock (groupStats)
					{
						groupStats[serverName] = throttlingStatistics;
					}
				}, TimeSpan.FromMilliseconds(40000.0));
				exceptionsByServer = distributedAction.ExceptionsByServer;
			}
			return groupStats;
		}

		// Token: 0x040001A0 RID: 416
		public const int RpcTimeoutInMs = 35000;

		// Token: 0x040001A1 RID: 417
		private static long globalInstanceCounter = DateTime.UtcNow.Ticks;

		// Token: 0x040001A2 RID: 418
		private readonly ThrottleParameters throttleParameters;

		// Token: 0x040001A3 RID: 419
		private readonly ResponderWorkItem responderWorkItem;

		// Token: 0x040001A4 RID: 420
		private readonly bool isThrowOnException;

		// Token: 0x040001A5 RID: 421
		private readonly ThrottlingProgressTracker inProgressTracker;

		// Token: 0x040001A6 RID: 422
		private readonly string localServerName;

		// Token: 0x040001A7 RID: 423
		private readonly TimeSpan timeout;

		// Token: 0x040001A8 RID: 424
		private readonly string throttleIdentity;

		// Token: 0x040001A9 RID: 425
		private readonly string throttlePropertiesXml;

		// Token: 0x040001AA RID: 426
		private readonly string throttleGroupName;

		// Token: 0x040001AB RID: 427
		private string[] throttleServersInGroup;

		// Token: 0x02000045 RID: 69
		[Flags]
		public enum ThrottleFailedChecks
		{
			// Token: 0x040001C0 RID: 448
			None = 0,
			// Token: 0x040001C1 RID: 449
			LocalMinimumMinutes = 1,
			// Token: 0x040001C2 RID: 450
			LocalMaxInHour = 2,
			// Token: 0x040001C3 RID: 451
			LocalMaxInDay = 4,
			// Token: 0x040001C4 RID: 452
			InProgressMismatch = 8,
			// Token: 0x040001C5 RID: 453
			RecoveryInProgress = 16,
			// Token: 0x040001C6 RID: 454
			GroupMinimumMinutes = 32,
			// Token: 0x040001C7 RID: 455
			GroupMaxInDay = 64,
			// Token: 0x040001C8 RID: 456
			GroupThrottlingInProgress = 128,
			// Token: 0x040001C9 RID: 457
			GroupRecoveryInProgress = 256
		}
	}
}
