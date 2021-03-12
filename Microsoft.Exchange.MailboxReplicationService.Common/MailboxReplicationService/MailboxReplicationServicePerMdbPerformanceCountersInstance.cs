using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000392 RID: 914
	internal sealed class MailboxReplicationServicePerMdbPerformanceCountersInstance : PerformanceCounterInstance
	{
		// Token: 0x06002799 RID: 10137 RVA: 0x000555B8 File Offset: 0x000537B8
		internal MailboxReplicationServicePerMdbPerformanceCountersInstance(string instanceName, MailboxReplicationServicePerMdbPerformanceCountersInstance autoUpdateTotalInstance) : base(instanceName, "MSExchange Mailbox Replication Service Per Mdb")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.ActiveMovesTotal = new ExPerformanceCounter(base.CategoryName, "Active Moves: Total Moves", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.ActiveMovesTotal, new ExPerformanceCounter[0]);
				list.Add(this.ActiveMovesTotal);
				this.ActiveMovesInitialSeeding = new ExPerformanceCounter(base.CategoryName, "Active Moves: Moves in Initial Seeding State", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.ActiveMovesInitialSeeding, new ExPerformanceCounter[0]);
				list.Add(this.ActiveMovesInitialSeeding);
				this.ActiveMovesCompletion = new ExPerformanceCounter(base.CategoryName, "Active Moves: Moves in Completion State", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.ActiveMovesCompletion, new ExPerformanceCounter[0]);
				list.Add(this.ActiveMovesCompletion);
				this.ActiveMovesStalledTotal = new ExPerformanceCounter(base.CategoryName, "Active Moves: Stalled Moves Total", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.ActiveMovesStalledTotal, new ExPerformanceCounter[0]);
				list.Add(this.ActiveMovesStalledTotal);
				this.ActiveMovesStalledHA = new ExPerformanceCounter(base.CategoryName, "Active Moves: Stalled Moves (Database Replication)", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.ActiveMovesStalledHA, new ExPerformanceCounter[0]);
				list.Add(this.ActiveMovesStalledHA);
				this.ActiveMovesStalledCI = new ExPerformanceCounter(base.CategoryName, "Active Moves: Stalled Moves (Content Indexing)", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.ActiveMovesStalledCI, new ExPerformanceCounter[0]);
				list.Add(this.ActiveMovesStalledCI);
				this.ActiveMovesTransientFailures = new ExPerformanceCounter(base.CategoryName, "Active Moves: Transient Failure (Total)", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.ActiveMovesTransientFailures, new ExPerformanceCounter[0]);
				list.Add(this.ActiveMovesTransientFailures);
				this.ActiveMovesNetworkFailures = new ExPerformanceCounter(base.CategoryName, "Active Moves: Transient Failure (Network)", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.ActiveMovesNetworkFailures, new ExPerformanceCounter[0]);
				list.Add(this.ActiveMovesNetworkFailures);
				this.ActiveMovesMDBOffline = new ExPerformanceCounter(base.CategoryName, "Active Moves: Transient Failure (MDB Offline)", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.ActiveMovesMDBOffline, new ExPerformanceCounter[0]);
				list.Add(this.ActiveMovesMDBOffline);
				this.ReadTransferRate = new ExPerformanceCounter(base.CategoryName, "Transfer Rate: Read (KB/sec)", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.ReadTransferRate, new ExPerformanceCounter[0]);
				list.Add(this.ReadTransferRate);
				this.ReadTransferRateBase = new ExPerformanceCounter(base.CategoryName, "Transfer Rate: Read (KB/sec) (base)", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.ReadTransferRateBase, new ExPerformanceCounter[0]);
				list.Add(this.ReadTransferRateBase);
				this.WriteTransferRate = new ExPerformanceCounter(base.CategoryName, "Transfer Rate: Write (KB/sec)", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.WriteTransferRate, new ExPerformanceCounter[0]);
				list.Add(this.WriteTransferRate);
				this.WriteTransferRateBase = new ExPerformanceCounter(base.CategoryName, "Transfer Rate: Write (KB/sec) (base)", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.WriteTransferRateBase, new ExPerformanceCounter[0]);
				list.Add(this.WriteTransferRateBase);
				this.MdbQueueQueued = new ExPerformanceCounter(base.CategoryName, "MDB Queue: Queued", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MdbQueueQueued, new ExPerformanceCounter[0]);
				list.Add(this.MdbQueueQueued);
				this.MdbQueueInProgress = new ExPerformanceCounter(base.CategoryName, "MDB Queue: In Progress", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MdbQueueInProgress, new ExPerformanceCounter[0]);
				list.Add(this.MdbQueueInProgress);
				this.MoveRequestsCompleted = new ExPerformanceCounter(base.CategoryName, "Move Requests: Completed", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MoveRequestsCompleted, new ExPerformanceCounter[0]);
				list.Add(this.MoveRequestsCompleted);
				this.MoveRequestsCompletedRate = new ExPerformanceCounter(base.CategoryName, "Move Requests: Completed/hour", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MoveRequestsCompletedRate, new ExPerformanceCounter[0]);
				list.Add(this.MoveRequestsCompletedRate);
				this.MoveRequestsCompletedRateBase = new ExPerformanceCounter(base.CategoryName, "Move Requests: Completed/hour (base)", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MoveRequestsCompletedRateBase, new ExPerformanceCounter[0]);
				list.Add(this.MoveRequestsCompletedRateBase);
				this.MoveRequestsCompletedWithWarnings = new ExPerformanceCounter(base.CategoryName, "Move Requests: Completed with Warnings", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MoveRequestsCompletedWithWarnings, new ExPerformanceCounter[0]);
				list.Add(this.MoveRequestsCompletedWithWarnings);
				this.MoveRequestsCompletedWithWarningsRate = new ExPerformanceCounter(base.CategoryName, "Move Requests: Completed with Warnings/hour", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MoveRequestsCompletedWithWarningsRate, new ExPerformanceCounter[0]);
				list.Add(this.MoveRequestsCompletedWithWarningsRate);
				this.MoveRequestsCompletedWithWarningsRateBase = new ExPerformanceCounter(base.CategoryName, "Move Requests: Completed with Warnings/hour (base)", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MoveRequestsCompletedWithWarningsRateBase, new ExPerformanceCounter[0]);
				list.Add(this.MoveRequestsCompletedWithWarningsRateBase);
				this.MoveRequestsCanceled = new ExPerformanceCounter(base.CategoryName, "Move Requests: Canceled", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MoveRequestsCanceled, new ExPerformanceCounter[0]);
				list.Add(this.MoveRequestsCanceled);
				this.MoveRequestsCanceledRate = new ExPerformanceCounter(base.CategoryName, "Move Requests: Canceled/hour", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MoveRequestsCanceledRate, new ExPerformanceCounter[0]);
				list.Add(this.MoveRequestsCanceledRate);
				this.MoveRequestsCanceledRateBase = new ExPerformanceCounter(base.CategoryName, "Move Requests: Canceled/hour (base)", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MoveRequestsCanceledRateBase, new ExPerformanceCounter[0]);
				list.Add(this.MoveRequestsCanceledRateBase);
				this.MoveRequestsTransientTotal = new ExPerformanceCounter(base.CategoryName, "Move Requests: Transient Failures", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MoveRequestsTransientTotal, new ExPerformanceCounter[0]);
				list.Add(this.MoveRequestsTransientTotal);
				this.MoveRequestsTransientTotalRate = new ExPerformanceCounter(base.CategoryName, "Move Requests: Transient Failures/hour", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MoveRequestsTransientTotalRate, new ExPerformanceCounter[0]);
				list.Add(this.MoveRequestsTransientTotalRate);
				this.MoveRequestsTransientTotalRateBase = new ExPerformanceCounter(base.CategoryName, "Move Requests: Transient Failures/hour (base)", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MoveRequestsTransientTotalRateBase, new ExPerformanceCounter[0]);
				list.Add(this.MoveRequestsTransientTotalRateBase);
				this.MoveRequestsNetworkFailures = new ExPerformanceCounter(base.CategoryName, "Move Requests: Transient Failures (Network)", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MoveRequestsNetworkFailures, new ExPerformanceCounter[0]);
				list.Add(this.MoveRequestsNetworkFailures);
				this.MoveRequestsNetworkFailuresRate = new ExPerformanceCounter(base.CategoryName, "Move Requests: Transient Failures (Network)/hour", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MoveRequestsNetworkFailuresRate, new ExPerformanceCounter[0]);
				list.Add(this.MoveRequestsNetworkFailuresRate);
				this.MoveRequestsNetworkFailuresRateBase = new ExPerformanceCounter(base.CategoryName, "Move Requests: Transient Failures (Network)/hour (base)", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MoveRequestsNetworkFailuresRateBase, new ExPerformanceCounter[0]);
				list.Add(this.MoveRequestsNetworkFailuresRateBase);
				this.MoveRequestsProxyBackoff = new ExPerformanceCounter(base.CategoryName, "Move Requests: Transient Failures (Proxy Backoff)", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MoveRequestsProxyBackoff, new ExPerformanceCounter[0]);
				list.Add(this.MoveRequestsProxyBackoff);
				this.MoveRequestsProxyBackoffRate = new ExPerformanceCounter(base.CategoryName, "Move Requests: Transient Failures (Proxy Backoff)/hour", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MoveRequestsProxyBackoffRate, new ExPerformanceCounter[0]);
				list.Add(this.MoveRequestsProxyBackoffRate);
				this.MoveRequestsProxyBackoffRateBase = new ExPerformanceCounter(base.CategoryName, "Move Requests: Transient Failures (Proxy Backoff)/hour (base)", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MoveRequestsProxyBackoffRateBase, new ExPerformanceCounter[0]);
				list.Add(this.MoveRequestsProxyBackoffRateBase);
				this.MoveRequestsFailTotal = new ExPerformanceCounter(base.CategoryName, "Move Requests: Failed", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MoveRequestsFailTotal, new ExPerformanceCounter[0]);
				list.Add(this.MoveRequestsFailTotal);
				this.MoveRequestsFailTotalRate = new ExPerformanceCounter(base.CategoryName, "Move Requests: Failed/hour", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MoveRequestsFailTotalRate, new ExPerformanceCounter[0]);
				list.Add(this.MoveRequestsFailTotalRate);
				this.MoveRequestsFailTotalRateBase = new ExPerformanceCounter(base.CategoryName, "Move Requests: Failed/hour (base)", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MoveRequestsFailTotalRateBase, new ExPerformanceCounter[0]);
				list.Add(this.MoveRequestsFailTotalRateBase);
				this.MoveRequestsFailBadItemLimit = new ExPerformanceCounter(base.CategoryName, "Move Requests: Failed (Bad Item Limit)", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MoveRequestsFailBadItemLimit, new ExPerformanceCounter[0]);
				list.Add(this.MoveRequestsFailBadItemLimit);
				this.MoveRequestsFailBadItemLimitRate = new ExPerformanceCounter(base.CategoryName, "Move Requests: Failed (Bad Item Limit)/hour", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MoveRequestsFailBadItemLimitRate, new ExPerformanceCounter[0]);
				list.Add(this.MoveRequestsFailBadItemLimitRate);
				this.MoveRequestsFailBadItemLimitRateBase = new ExPerformanceCounter(base.CategoryName, "Move Requests: Failed (Bad Item Limit)/hour (base)", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MoveRequestsFailBadItemLimitRateBase, new ExPerformanceCounter[0]);
				list.Add(this.MoveRequestsFailBadItemLimitRateBase);
				this.MoveRequestsFailNetwork = new ExPerformanceCounter(base.CategoryName, "Move Requests: Failed (Network)", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MoveRequestsFailNetwork, new ExPerformanceCounter[0]);
				list.Add(this.MoveRequestsFailNetwork);
				this.MoveRequestsFailNetworkRate = new ExPerformanceCounter(base.CategoryName, "Move Requests: Failed (Network)/hour", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MoveRequestsFailNetworkRate, new ExPerformanceCounter[0]);
				list.Add(this.MoveRequestsFailNetworkRate);
				this.MoveRequestsFailNetworkRateBase = new ExPerformanceCounter(base.CategoryName, "Move Requests: Failed (Network)/hour (base)", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MoveRequestsFailNetworkRateBase, new ExPerformanceCounter[0]);
				list.Add(this.MoveRequestsFailNetworkRateBase);
				this.MoveRequestsFailStallCI = new ExPerformanceCounter(base.CategoryName, "Move Requests: Failed (Stall Content Indexing)", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MoveRequestsFailStallCI, new ExPerformanceCounter[0]);
				list.Add(this.MoveRequestsFailStallCI);
				this.MoveRequestsFailStallCIRate = new ExPerformanceCounter(base.CategoryName, "Move Requests: Failed (Stall Content Indexing)/hour", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MoveRequestsFailStallCIRate, new ExPerformanceCounter[0]);
				list.Add(this.MoveRequestsFailStallCIRate);
				this.MoveRequestsFailStallCIRateBase = new ExPerformanceCounter(base.CategoryName, "Move Requests: Failed (Stall Content Indexing)/hour (base)", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MoveRequestsFailStallCIRateBase, new ExPerformanceCounter[0]);
				list.Add(this.MoveRequestsFailStallCIRateBase);
				this.MoveRequestsFailStallHA = new ExPerformanceCounter(base.CategoryName, "Move Requests: Failed (Stall Database Replication)", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MoveRequestsFailStallHA, new ExPerformanceCounter[0]);
				list.Add(this.MoveRequestsFailStallHA);
				this.MoveRequestsFailStallHARate = new ExPerformanceCounter(base.CategoryName, "Move Requests: Failed (Stall Database Replication)/hour", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MoveRequestsFailStallHARate, new ExPerformanceCounter[0]);
				list.Add(this.MoveRequestsFailStallHARate);
				this.MoveRequestsFailStallHARateBase = new ExPerformanceCounter(base.CategoryName, "Move Requests: Failed (Stall Database Replication)/hour (base)", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MoveRequestsFailStallHARateBase, new ExPerformanceCounter[0]);
				list.Add(this.MoveRequestsFailStallHARateBase);
				this.MoveRequestsFailMAPI = new ExPerformanceCounter(base.CategoryName, "Move Requests: Failed (MAPI)", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MoveRequestsFailMAPI, new ExPerformanceCounter[0]);
				list.Add(this.MoveRequestsFailMAPI);
				this.MoveRequestsFailMAPIRate = new ExPerformanceCounter(base.CategoryName, "Move Requests: Failed (MAPI)/hour", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MoveRequestsFailMAPIRate, new ExPerformanceCounter[0]);
				list.Add(this.MoveRequestsFailMAPIRate);
				this.MoveRequestsFailMAPIRateBase = new ExPerformanceCounter(base.CategoryName, "Move Requests: Failed (MAPI)/hour (base)", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MoveRequestsFailMAPIRateBase, new ExPerformanceCounter[0]);
				list.Add(this.MoveRequestsFailMAPIRateBase);
				this.MoveRequestsFailOther = new ExPerformanceCounter(base.CategoryName, "Move Requests: Failed (Other)", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MoveRequestsFailOther, new ExPerformanceCounter[0]);
				list.Add(this.MoveRequestsFailOther);
				this.MoveRequestsFailOtherRate = new ExPerformanceCounter(base.CategoryName, "Move Requests: Failed (Other)/hour", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MoveRequestsFailOtherRate, new ExPerformanceCounter[0]);
				list.Add(this.MoveRequestsFailOtherRate);
				this.MoveRequestsFailOtherRateBase = new ExPerformanceCounter(base.CategoryName, "Move Requests: Failed (Other)/hour (base)", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MoveRequestsFailOtherRateBase, new ExPerformanceCounter[0]);
				list.Add(this.MoveRequestsFailOtherRateBase);
				this.MoveRequestsStallsTotal = new ExPerformanceCounter(base.CategoryName, "Move Requests: Stalls", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MoveRequestsStallsTotal, new ExPerformanceCounter[0]);
				list.Add(this.MoveRequestsStallsTotal);
				this.MoveRequestsStallsTotalRate = new ExPerformanceCounter(base.CategoryName, "Move Requests: Move Stalls/hour", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MoveRequestsStallsTotalRate, new ExPerformanceCounter[0]);
				list.Add(this.MoveRequestsStallsTotalRate);
				this.MoveRequestsStallsTotalRateBase = new ExPerformanceCounter(base.CategoryName, "Move Requests: Move Stalls/hour (base)", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MoveRequestsStallsTotalRateBase, new ExPerformanceCounter[0]);
				list.Add(this.MoveRequestsStallsTotalRateBase);
				this.MoveRequestsStallsHA = new ExPerformanceCounter(base.CategoryName, "Move Requests: Stalls (Database Replication)", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MoveRequestsStallsHA, new ExPerformanceCounter[0]);
				list.Add(this.MoveRequestsStallsHA);
				this.MoveRequestsStallsHARate = new ExPerformanceCounter(base.CategoryName, "Move Requests: Move Stalls (Database Replication)/hour", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MoveRequestsStallsHARate, new ExPerformanceCounter[0]);
				list.Add(this.MoveRequestsStallsHARate);
				this.MoveRequestsStallsHARateBase = new ExPerformanceCounter(base.CategoryName, "Move Requests: Move Stalls (Database Replication)/hour (base)", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MoveRequestsStallsHARateBase, new ExPerformanceCounter[0]);
				list.Add(this.MoveRequestsStallsHARateBase);
				this.MoveRequestsStallsCI = new ExPerformanceCounter(base.CategoryName, "Move Requests: Stalls (Content Indexing)", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MoveRequestsStallsCI, new ExPerformanceCounter[0]);
				list.Add(this.MoveRequestsStallsCI);
				this.MoveRequestsStallsCIRate = new ExPerformanceCounter(base.CategoryName, "Move Requests: Move Stalls (Content Indexing)/hour", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MoveRequestsStallsCIRate, new ExPerformanceCounter[0]);
				list.Add(this.MoveRequestsStallsCIRate);
				this.MoveRequestsStallsCIRateBase = new ExPerformanceCounter(base.CategoryName, "Move Requests: Move Stalls (Content Indexing)/hour (base)", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MoveRequestsStallsCIRateBase, new ExPerformanceCounter[0]);
				list.Add(this.MoveRequestsStallsCIRateBase);
				this.LastScanTime = new ExPerformanceCounter(base.CategoryName, "Last Scan: Timestamp (UTC)", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.LastScanTime, new ExPerformanceCounter[0]);
				list.Add(this.LastScanTime);
				this.LastScanDuration = new ExPerformanceCounter(base.CategoryName, "Last Scan: Duration (msec)", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.LastScanDuration, new ExPerformanceCounter[0]);
				list.Add(this.LastScanDuration);
				this.LastScanFailure = new ExPerformanceCounter(base.CategoryName, "Last Scan: Scan Failure", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.LastScanFailure, new ExPerformanceCounter[0]);
				list.Add(this.LastScanFailure);
				this.UtilizationReadHiPri = new ExPerformanceCounter(base.CategoryName, "Utilization: Read jobs (high priority)", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.UtilizationReadHiPri, new ExPerformanceCounter[0]);
				list.Add(this.UtilizationReadHiPri);
				this.UtilizationReadCustomerExpectation = new ExPerformanceCounter(base.CategoryName, "Utilization: Read jobs (customer expectation)", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.UtilizationReadCustomerExpectation, new ExPerformanceCounter[0]);
				list.Add(this.UtilizationReadCustomerExpectation);
				this.UtilizationReadInternalMaintenance = new ExPerformanceCounter(base.CategoryName, "Utilization: Read jobs (internal maintenance)", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.UtilizationReadInternalMaintenance, new ExPerformanceCounter[0]);
				list.Add(this.UtilizationReadInternalMaintenance);
				this.UtilizationRead = new ExPerformanceCounter(base.CategoryName, "Utilization: Read jobs", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.UtilizationRead, new ExPerformanceCounter[0]);
				list.Add(this.UtilizationRead);
				this.UtilizationWriteHiPri = new ExPerformanceCounter(base.CategoryName, "Utilization: Write jobs (high priority)", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.UtilizationWriteHiPri, new ExPerformanceCounter[0]);
				list.Add(this.UtilizationWriteHiPri);
				this.UtilizationWriteCustomerExpectation = new ExPerformanceCounter(base.CategoryName, "Utilization: Write jobs (customer expectation)", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.UtilizationWriteCustomerExpectation, new ExPerformanceCounter[0]);
				list.Add(this.UtilizationWriteCustomerExpectation);
				this.UtilizationWriteInternalMaintenance = new ExPerformanceCounter(base.CategoryName, "Utilization: Write jobs (internal maintenance)", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.UtilizationWriteInternalMaintenance, new ExPerformanceCounter[0]);
				list.Add(this.UtilizationWriteInternalMaintenance);
				this.UtilizationWrite = new ExPerformanceCounter(base.CategoryName, "Utilization: Write jobs", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.UtilizationWrite, new ExPerformanceCounter[0]);
				list.Add(this.UtilizationWrite);
				this.ResourceHealthMDBLatencyHiPri = new ExPerformanceCounter(base.CategoryName, "Resource Health: MDB latency (high priority)", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.ResourceHealthMDBLatencyHiPri, new ExPerformanceCounter[0]);
				list.Add(this.ResourceHealthMDBLatencyHiPri);
				this.ResourceHealthMDBLatencyCustomerExpectation = new ExPerformanceCounter(base.CategoryName, "Resource Health: MDB latency (customer expectation)", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.ResourceHealthMDBLatencyCustomerExpectation, new ExPerformanceCounter[0]);
				list.Add(this.ResourceHealthMDBLatencyCustomerExpectation);
				this.ResourceHealthMDBLatencyInternalMaintenance = new ExPerformanceCounter(base.CategoryName, "Resource Health: MDB latency (internal maintenance)", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.ResourceHealthMDBLatencyInternalMaintenance, new ExPerformanceCounter[0]);
				list.Add(this.ResourceHealthMDBLatencyInternalMaintenance);
				this.ResourceHealthMDBLatency = new ExPerformanceCounter(base.CategoryName, "Resource Health: MDB latency", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.ResourceHealthMDBLatency, new ExPerformanceCounter[0]);
				list.Add(this.ResourceHealthMDBLatency);
				this.DynamicCapacityMDBLatencyHiPri = new ExPerformanceCounter(base.CategoryName, "Dynamic Capacity: MDB latency (high priority)", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.DynamicCapacityMDBLatencyHiPri, new ExPerformanceCounter[0]);
				list.Add(this.DynamicCapacityMDBLatencyHiPri);
				this.DynamicCapacityMDBLatencyCustomerExpectation = new ExPerformanceCounter(base.CategoryName, "Dynamic Capacity: MDB latency (customer expectation)", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.DynamicCapacityMDBLatencyCustomerExpectation, new ExPerformanceCounter[0]);
				list.Add(this.DynamicCapacityMDBLatencyCustomerExpectation);
				this.DynamicCapacityMDBLatencyInternalMaintenance = new ExPerformanceCounter(base.CategoryName, "Dynamic Capacity: MDB latency (internal maintenance)", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.DynamicCapacityMDBLatencyInternalMaintenance, new ExPerformanceCounter[0]);
				list.Add(this.DynamicCapacityMDBLatencyInternalMaintenance);
				this.DynamicCapacityMDBLatency = new ExPerformanceCounter(base.CategoryName, "Dynamic Capacity: MDB latency", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.DynamicCapacityMDBLatency, new ExPerformanceCounter[0]);
				list.Add(this.DynamicCapacityMDBLatency);
				this.ResourceHealthDiskLatencyHiPri = new ExPerformanceCounter(base.CategoryName, "Resource Health: Disk latency (high priority)", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.ResourceHealthDiskLatencyHiPri, new ExPerformanceCounter[0]);
				list.Add(this.ResourceHealthDiskLatencyHiPri);
				this.ResourceHealthDiskLatencyCustomerExpectation = new ExPerformanceCounter(base.CategoryName, "Resource Health: Disk latency (customer expectation)", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.ResourceHealthDiskLatencyCustomerExpectation, new ExPerformanceCounter[0]);
				list.Add(this.ResourceHealthDiskLatencyCustomerExpectation);
				this.ResourceHealthDiskLatencyInternalMaintenance = new ExPerformanceCounter(base.CategoryName, "Resource Health: Disk latency (internal maintenance)", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.ResourceHealthDiskLatencyInternalMaintenance, new ExPerformanceCounter[0]);
				list.Add(this.ResourceHealthDiskLatencyInternalMaintenance);
				this.ResourceHealthDiskLatency = new ExPerformanceCounter(base.CategoryName, "Resource Health: Disk latency", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.ResourceHealthDiskLatency, new ExPerformanceCounter[0]);
				list.Add(this.ResourceHealthDiskLatency);
				this.DynamicCapacityDiskLatencyHiPri = new ExPerformanceCounter(base.CategoryName, "Dynamic Capacity: Disk latency (high priority)", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.DynamicCapacityDiskLatencyHiPri, new ExPerformanceCounter[0]);
				list.Add(this.DynamicCapacityDiskLatencyHiPri);
				this.DynamicCapacityDiskLatencyCustomerExpectation = new ExPerformanceCounter(base.CategoryName, "Dynamic Capacity: Disk latency (customer expectation)", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.DynamicCapacityDiskLatencyCustomerExpectation, new ExPerformanceCounter[0]);
				list.Add(this.DynamicCapacityDiskLatencyCustomerExpectation);
				this.DynamicCapacityDiskLatencyInternalMaintenance = new ExPerformanceCounter(base.CategoryName, "Dynamic Capacity: Disk latency (internal maintenance)", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.DynamicCapacityDiskLatencyInternalMaintenance, new ExPerformanceCounter[0]);
				list.Add(this.DynamicCapacityDiskLatencyInternalMaintenance);
				this.DynamicCapacityDiskLatency = new ExPerformanceCounter(base.CategoryName, "Dynamic Capacity: Disk latency", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.DynamicCapacityDiskLatency, new ExPerformanceCounter[0]);
				list.Add(this.DynamicCapacityDiskLatency);
				this.ResourceHealthMDBReplicationHiPri = new ExPerformanceCounter(base.CategoryName, "Resource Health: MDB replication (high priority)", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.ResourceHealthMDBReplicationHiPri, new ExPerformanceCounter[0]);
				list.Add(this.ResourceHealthMDBReplicationHiPri);
				this.ResourceHealthMDBReplicationCustomerExpectation = new ExPerformanceCounter(base.CategoryName, "Resource Health: MDB replication (customer expectation)", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.ResourceHealthMDBReplicationCustomerExpectation, new ExPerformanceCounter[0]);
				list.Add(this.ResourceHealthMDBReplicationCustomerExpectation);
				this.ResourceHealthMDBReplicationInternalMaintenance = new ExPerformanceCounter(base.CategoryName, "Resource Health: MDB replication (internal maintenance)", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.ResourceHealthMDBReplicationInternalMaintenance, new ExPerformanceCounter[0]);
				list.Add(this.ResourceHealthMDBReplicationInternalMaintenance);
				this.ResourceHealthMDBReplication = new ExPerformanceCounter(base.CategoryName, "Resource Health: MDB replication", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.ResourceHealthMDBReplication, new ExPerformanceCounter[0]);
				list.Add(this.ResourceHealthMDBReplication);
				this.DynamicCapacityMDBReplicationHiPri = new ExPerformanceCounter(base.CategoryName, "Dynamic Capacity: MDB replication (high priority)", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.DynamicCapacityMDBReplicationHiPri, new ExPerformanceCounter[0]);
				list.Add(this.DynamicCapacityMDBReplicationHiPri);
				this.DynamicCapacityMDBReplicationCustomerExpectation = new ExPerformanceCounter(base.CategoryName, "Dynamic Capacity: MDB replication (customer expectation)", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.DynamicCapacityMDBReplicationCustomerExpectation, new ExPerformanceCounter[0]);
				list.Add(this.DynamicCapacityMDBReplicationCustomerExpectation);
				this.DynamicCapacityMDBReplicationInternalMaintenance = new ExPerformanceCounter(base.CategoryName, "Dynamic Capacity: MDB replication (internal maintenance)", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.DynamicCapacityMDBReplicationInternalMaintenance, new ExPerformanceCounter[0]);
				list.Add(this.DynamicCapacityMDBReplicationInternalMaintenance);
				this.DynamicCapacityMDBReplication = new ExPerformanceCounter(base.CategoryName, "Dynamic Capacity: MDB replication", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.DynamicCapacityMDBReplication, new ExPerformanceCounter[0]);
				list.Add(this.DynamicCapacityMDBReplication);
				this.ResourceHealthMDBAvailabilityHiPri = new ExPerformanceCounter(base.CategoryName, "Resource Health: MDB availability (high priority)", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.ResourceHealthMDBAvailabilityHiPri, new ExPerformanceCounter[0]);
				list.Add(this.ResourceHealthMDBAvailabilityHiPri);
				this.ResourceHealthMDBAvailabilityCustomerExpectation = new ExPerformanceCounter(base.CategoryName, "Resource Health: MDB availability (customer expectation)", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.ResourceHealthMDBAvailabilityCustomerExpectation, new ExPerformanceCounter[0]);
				list.Add(this.ResourceHealthMDBAvailabilityCustomerExpectation);
				this.ResourceHealthMDBAvailabilityInternalMaintenance = new ExPerformanceCounter(base.CategoryName, "Resource Health: MDB availability (internal maintenance)", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.ResourceHealthMDBAvailabilityInternalMaintenance, new ExPerformanceCounter[0]);
				list.Add(this.ResourceHealthMDBAvailabilityInternalMaintenance);
				this.ResourceHealthMDBAvailability = new ExPerformanceCounter(base.CategoryName, "Resource Health: MDB availability", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.ResourceHealthMDBAvailability, new ExPerformanceCounter[0]);
				list.Add(this.ResourceHealthMDBAvailability);
				this.DynamicCapacityMDBAvailabilityHiPri = new ExPerformanceCounter(base.CategoryName, "Dynamic Capacity: MDB availability (high priority)", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.DynamicCapacityMDBAvailabilityHiPri, new ExPerformanceCounter[0]);
				list.Add(this.DynamicCapacityMDBAvailabilityHiPri);
				this.DynamicCapacityMDBAvailabilityCustomerExpectation = new ExPerformanceCounter(base.CategoryName, "Dynamic Capacity: MDB availability (customer expectation)", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.DynamicCapacityMDBAvailabilityCustomerExpectation, new ExPerformanceCounter[0]);
				list.Add(this.DynamicCapacityMDBAvailabilityCustomerExpectation);
				this.DynamicCapacityMDBAvailabilityInternalMaintenance = new ExPerformanceCounter(base.CategoryName, "Dynamic Capacity: MDB availability (internal maintenance)", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.DynamicCapacityMDBAvailabilityInternalMaintenance, new ExPerformanceCounter[0]);
				list.Add(this.DynamicCapacityMDBAvailabilityInternalMaintenance);
				this.DynamicCapacityMDBAvailability = new ExPerformanceCounter(base.CategoryName, "Dynamic Capacity: MDB availability", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.DynamicCapacityMDBAvailability, new ExPerformanceCounter[0]);
				list.Add(this.DynamicCapacityMDBAvailability);
				this.ResourceHealthCIAgeOfLastNotificationHiPri = new ExPerformanceCounter(base.CategoryName, "Resource Health: CI age of last notification (high priority)", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.ResourceHealthCIAgeOfLastNotificationHiPri, new ExPerformanceCounter[0]);
				list.Add(this.ResourceHealthCIAgeOfLastNotificationHiPri);
				this.ResourceHealthCIAgeOfLastNotificationCustomerExpectation = new ExPerformanceCounter(base.CategoryName, "Resource Health: CI age of last notification (customer expectation)", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.ResourceHealthCIAgeOfLastNotificationCustomerExpectation, new ExPerformanceCounter[0]);
				list.Add(this.ResourceHealthCIAgeOfLastNotificationCustomerExpectation);
				this.ResourceHealthCIAgeOfLastNotificationInternalMaintenance = new ExPerformanceCounter(base.CategoryName, "Resource Health: CI age of last notification (internal maintenance)", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.ResourceHealthCIAgeOfLastNotificationInternalMaintenance, new ExPerformanceCounter[0]);
				list.Add(this.ResourceHealthCIAgeOfLastNotificationInternalMaintenance);
				this.ResourceHealthCIAgeOfLastNotification = new ExPerformanceCounter(base.CategoryName, "Resource Health: CI age of last notification", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.ResourceHealthCIAgeOfLastNotification, new ExPerformanceCounter[0]);
				list.Add(this.ResourceHealthCIAgeOfLastNotification);
				this.DynamicCapacityCIAgeOfLastNotificationHiPri = new ExPerformanceCounter(base.CategoryName, "Dynamic Capacity: CI age of last notification (high priority)", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.DynamicCapacityCIAgeOfLastNotificationHiPri, new ExPerformanceCounter[0]);
				list.Add(this.DynamicCapacityCIAgeOfLastNotificationHiPri);
				this.DynamicCapacityCIAgeOfLastNotificationCustomerExpectation = new ExPerformanceCounter(base.CategoryName, "Dynamic Capacity: CI age of last notification (customer expectation)", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.DynamicCapacityCIAgeOfLastNotificationCustomerExpectation, new ExPerformanceCounter[0]);
				list.Add(this.DynamicCapacityCIAgeOfLastNotificationCustomerExpectation);
				this.DynamicCapacityCIAgeOfLastNotificationInternalMaintenance = new ExPerformanceCounter(base.CategoryName, "Dynamic Capacity: CI age of last notification (internal maintenance)", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.DynamicCapacityCIAgeOfLastNotificationInternalMaintenance, new ExPerformanceCounter[0]);
				list.Add(this.DynamicCapacityCIAgeOfLastNotificationInternalMaintenance);
				this.DynamicCapacityCIAgeOfLastNotification = new ExPerformanceCounter(base.CategoryName, "Dynamic Capacity: CI age of last notification", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.DynamicCapacityCIAgeOfLastNotification, new ExPerformanceCounter[0]);
				list.Add(this.DynamicCapacityCIAgeOfLastNotification);
				long num = this.ActiveMovesTotal.RawValue;
				num += 1L;
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					foreach (ExPerformanceCounter exPerformanceCounter in list)
					{
						exPerformanceCounter.Close();
					}
				}
			}
			this.counters = list.ToArray();
		}

		// Token: 0x0600279A RID: 10138 RVA: 0x00056E70 File Offset: 0x00055070
		internal MailboxReplicationServicePerMdbPerformanceCountersInstance(string instanceName) : base(instanceName, "MSExchange Mailbox Replication Service Per Mdb")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.ActiveMovesTotal = new ExPerformanceCounter(base.CategoryName, "Active Moves: Total Moves", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ActiveMovesTotal);
				this.ActiveMovesInitialSeeding = new ExPerformanceCounter(base.CategoryName, "Active Moves: Moves in Initial Seeding State", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ActiveMovesInitialSeeding);
				this.ActiveMovesCompletion = new ExPerformanceCounter(base.CategoryName, "Active Moves: Moves in Completion State", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ActiveMovesCompletion);
				this.ActiveMovesStalledTotal = new ExPerformanceCounter(base.CategoryName, "Active Moves: Stalled Moves Total", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ActiveMovesStalledTotal);
				this.ActiveMovesStalledHA = new ExPerformanceCounter(base.CategoryName, "Active Moves: Stalled Moves (Database Replication)", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ActiveMovesStalledHA);
				this.ActiveMovesStalledCI = new ExPerformanceCounter(base.CategoryName, "Active Moves: Stalled Moves (Content Indexing)", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ActiveMovesStalledCI);
				this.ActiveMovesTransientFailures = new ExPerformanceCounter(base.CategoryName, "Active Moves: Transient Failure (Total)", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ActiveMovesTransientFailures);
				this.ActiveMovesNetworkFailures = new ExPerformanceCounter(base.CategoryName, "Active Moves: Transient Failure (Network)", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ActiveMovesNetworkFailures);
				this.ActiveMovesMDBOffline = new ExPerformanceCounter(base.CategoryName, "Active Moves: Transient Failure (MDB Offline)", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ActiveMovesMDBOffline);
				this.ReadTransferRate = new ExPerformanceCounter(base.CategoryName, "Transfer Rate: Read (KB/sec)", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ReadTransferRate);
				this.ReadTransferRateBase = new ExPerformanceCounter(base.CategoryName, "Transfer Rate: Read (KB/sec) (base)", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ReadTransferRateBase);
				this.WriteTransferRate = new ExPerformanceCounter(base.CategoryName, "Transfer Rate: Write (KB/sec)", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.WriteTransferRate);
				this.WriteTransferRateBase = new ExPerformanceCounter(base.CategoryName, "Transfer Rate: Write (KB/sec) (base)", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.WriteTransferRateBase);
				this.MdbQueueQueued = new ExPerformanceCounter(base.CategoryName, "MDB Queue: Queued", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MdbQueueQueued);
				this.MdbQueueInProgress = new ExPerformanceCounter(base.CategoryName, "MDB Queue: In Progress", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MdbQueueInProgress);
				this.MoveRequestsCompleted = new ExPerformanceCounter(base.CategoryName, "Move Requests: Completed", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MoveRequestsCompleted);
				this.MoveRequestsCompletedRate = new ExPerformanceCounter(base.CategoryName, "Move Requests: Completed/hour", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MoveRequestsCompletedRate);
				this.MoveRequestsCompletedRateBase = new ExPerformanceCounter(base.CategoryName, "Move Requests: Completed/hour (base)", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MoveRequestsCompletedRateBase);
				this.MoveRequestsCompletedWithWarnings = new ExPerformanceCounter(base.CategoryName, "Move Requests: Completed with Warnings", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MoveRequestsCompletedWithWarnings);
				this.MoveRequestsCompletedWithWarningsRate = new ExPerformanceCounter(base.CategoryName, "Move Requests: Completed with Warnings/hour", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MoveRequestsCompletedWithWarningsRate);
				this.MoveRequestsCompletedWithWarningsRateBase = new ExPerformanceCounter(base.CategoryName, "Move Requests: Completed with Warnings/hour (base)", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MoveRequestsCompletedWithWarningsRateBase);
				this.MoveRequestsCanceled = new ExPerformanceCounter(base.CategoryName, "Move Requests: Canceled", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MoveRequestsCanceled);
				this.MoveRequestsCanceledRate = new ExPerformanceCounter(base.CategoryName, "Move Requests: Canceled/hour", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MoveRequestsCanceledRate);
				this.MoveRequestsCanceledRateBase = new ExPerformanceCounter(base.CategoryName, "Move Requests: Canceled/hour (base)", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MoveRequestsCanceledRateBase);
				this.MoveRequestsTransientTotal = new ExPerformanceCounter(base.CategoryName, "Move Requests: Transient Failures", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MoveRequestsTransientTotal);
				this.MoveRequestsTransientTotalRate = new ExPerformanceCounter(base.CategoryName, "Move Requests: Transient Failures/hour", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MoveRequestsTransientTotalRate);
				this.MoveRequestsTransientTotalRateBase = new ExPerformanceCounter(base.CategoryName, "Move Requests: Transient Failures/hour (base)", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MoveRequestsTransientTotalRateBase);
				this.MoveRequestsNetworkFailures = new ExPerformanceCounter(base.CategoryName, "Move Requests: Transient Failures (Network)", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MoveRequestsNetworkFailures);
				this.MoveRequestsNetworkFailuresRate = new ExPerformanceCounter(base.CategoryName, "Move Requests: Transient Failures (Network)/hour", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MoveRequestsNetworkFailuresRate);
				this.MoveRequestsNetworkFailuresRateBase = new ExPerformanceCounter(base.CategoryName, "Move Requests: Transient Failures (Network)/hour (base)", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MoveRequestsNetworkFailuresRateBase);
				this.MoveRequestsProxyBackoff = new ExPerformanceCounter(base.CategoryName, "Move Requests: Transient Failures (Proxy Backoff)", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MoveRequestsProxyBackoff);
				this.MoveRequestsProxyBackoffRate = new ExPerformanceCounter(base.CategoryName, "Move Requests: Transient Failures (Proxy Backoff)/hour", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MoveRequestsProxyBackoffRate);
				this.MoveRequestsProxyBackoffRateBase = new ExPerformanceCounter(base.CategoryName, "Move Requests: Transient Failures (Proxy Backoff)/hour (base)", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MoveRequestsProxyBackoffRateBase);
				this.MoveRequestsFailTotal = new ExPerformanceCounter(base.CategoryName, "Move Requests: Failed", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MoveRequestsFailTotal);
				this.MoveRequestsFailTotalRate = new ExPerformanceCounter(base.CategoryName, "Move Requests: Failed/hour", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MoveRequestsFailTotalRate);
				this.MoveRequestsFailTotalRateBase = new ExPerformanceCounter(base.CategoryName, "Move Requests: Failed/hour (base)", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MoveRequestsFailTotalRateBase);
				this.MoveRequestsFailBadItemLimit = new ExPerformanceCounter(base.CategoryName, "Move Requests: Failed (Bad Item Limit)", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MoveRequestsFailBadItemLimit);
				this.MoveRequestsFailBadItemLimitRate = new ExPerformanceCounter(base.CategoryName, "Move Requests: Failed (Bad Item Limit)/hour", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MoveRequestsFailBadItemLimitRate);
				this.MoveRequestsFailBadItemLimitRateBase = new ExPerformanceCounter(base.CategoryName, "Move Requests: Failed (Bad Item Limit)/hour (base)", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MoveRequestsFailBadItemLimitRateBase);
				this.MoveRequestsFailNetwork = new ExPerformanceCounter(base.CategoryName, "Move Requests: Failed (Network)", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MoveRequestsFailNetwork);
				this.MoveRequestsFailNetworkRate = new ExPerformanceCounter(base.CategoryName, "Move Requests: Failed (Network)/hour", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MoveRequestsFailNetworkRate);
				this.MoveRequestsFailNetworkRateBase = new ExPerformanceCounter(base.CategoryName, "Move Requests: Failed (Network)/hour (base)", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MoveRequestsFailNetworkRateBase);
				this.MoveRequestsFailStallCI = new ExPerformanceCounter(base.CategoryName, "Move Requests: Failed (Stall Content Indexing)", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MoveRequestsFailStallCI);
				this.MoveRequestsFailStallCIRate = new ExPerformanceCounter(base.CategoryName, "Move Requests: Failed (Stall Content Indexing)/hour", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MoveRequestsFailStallCIRate);
				this.MoveRequestsFailStallCIRateBase = new ExPerformanceCounter(base.CategoryName, "Move Requests: Failed (Stall Content Indexing)/hour (base)", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MoveRequestsFailStallCIRateBase);
				this.MoveRequestsFailStallHA = new ExPerformanceCounter(base.CategoryName, "Move Requests: Failed (Stall Database Replication)", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MoveRequestsFailStallHA);
				this.MoveRequestsFailStallHARate = new ExPerformanceCounter(base.CategoryName, "Move Requests: Failed (Stall Database Replication)/hour", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MoveRequestsFailStallHARate);
				this.MoveRequestsFailStallHARateBase = new ExPerformanceCounter(base.CategoryName, "Move Requests: Failed (Stall Database Replication)/hour (base)", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MoveRequestsFailStallHARateBase);
				this.MoveRequestsFailMAPI = new ExPerformanceCounter(base.CategoryName, "Move Requests: Failed (MAPI)", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MoveRequestsFailMAPI);
				this.MoveRequestsFailMAPIRate = new ExPerformanceCounter(base.CategoryName, "Move Requests: Failed (MAPI)/hour", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MoveRequestsFailMAPIRate);
				this.MoveRequestsFailMAPIRateBase = new ExPerformanceCounter(base.CategoryName, "Move Requests: Failed (MAPI)/hour (base)", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MoveRequestsFailMAPIRateBase);
				this.MoveRequestsFailOther = new ExPerformanceCounter(base.CategoryName, "Move Requests: Failed (Other)", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MoveRequestsFailOther);
				this.MoveRequestsFailOtherRate = new ExPerformanceCounter(base.CategoryName, "Move Requests: Failed (Other)/hour", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MoveRequestsFailOtherRate);
				this.MoveRequestsFailOtherRateBase = new ExPerformanceCounter(base.CategoryName, "Move Requests: Failed (Other)/hour (base)", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MoveRequestsFailOtherRateBase);
				this.MoveRequestsStallsTotal = new ExPerformanceCounter(base.CategoryName, "Move Requests: Stalls", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MoveRequestsStallsTotal);
				this.MoveRequestsStallsTotalRate = new ExPerformanceCounter(base.CategoryName, "Move Requests: Move Stalls/hour", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MoveRequestsStallsTotalRate);
				this.MoveRequestsStallsTotalRateBase = new ExPerformanceCounter(base.CategoryName, "Move Requests: Move Stalls/hour (base)", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MoveRequestsStallsTotalRateBase);
				this.MoveRequestsStallsHA = new ExPerformanceCounter(base.CategoryName, "Move Requests: Stalls (Database Replication)", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MoveRequestsStallsHA);
				this.MoveRequestsStallsHARate = new ExPerformanceCounter(base.CategoryName, "Move Requests: Move Stalls (Database Replication)/hour", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MoveRequestsStallsHARate);
				this.MoveRequestsStallsHARateBase = new ExPerformanceCounter(base.CategoryName, "Move Requests: Move Stalls (Database Replication)/hour (base)", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MoveRequestsStallsHARateBase);
				this.MoveRequestsStallsCI = new ExPerformanceCounter(base.CategoryName, "Move Requests: Stalls (Content Indexing)", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MoveRequestsStallsCI);
				this.MoveRequestsStallsCIRate = new ExPerformanceCounter(base.CategoryName, "Move Requests: Move Stalls (Content Indexing)/hour", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MoveRequestsStallsCIRate);
				this.MoveRequestsStallsCIRateBase = new ExPerformanceCounter(base.CategoryName, "Move Requests: Move Stalls (Content Indexing)/hour (base)", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MoveRequestsStallsCIRateBase);
				this.LastScanTime = new ExPerformanceCounter(base.CategoryName, "Last Scan: Timestamp (UTC)", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.LastScanTime);
				this.LastScanDuration = new ExPerformanceCounter(base.CategoryName, "Last Scan: Duration (msec)", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.LastScanDuration);
				this.LastScanFailure = new ExPerformanceCounter(base.CategoryName, "Last Scan: Scan Failure", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.LastScanFailure);
				this.UtilizationReadHiPri = new ExPerformanceCounter(base.CategoryName, "Utilization: Read jobs (high priority)", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.UtilizationReadHiPri);
				this.UtilizationReadCustomerExpectation = new ExPerformanceCounter(base.CategoryName, "Utilization: Read jobs (customer expectation)", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.UtilizationReadCustomerExpectation);
				this.UtilizationReadInternalMaintenance = new ExPerformanceCounter(base.CategoryName, "Utilization: Read jobs (internal maintenance)", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.UtilizationReadInternalMaintenance);
				this.UtilizationRead = new ExPerformanceCounter(base.CategoryName, "Utilization: Read jobs", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.UtilizationRead);
				this.UtilizationWriteHiPri = new ExPerformanceCounter(base.CategoryName, "Utilization: Write jobs (high priority)", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.UtilizationWriteHiPri);
				this.UtilizationWriteCustomerExpectation = new ExPerformanceCounter(base.CategoryName, "Utilization: Write jobs (customer expectation)", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.UtilizationWriteCustomerExpectation);
				this.UtilizationWriteInternalMaintenance = new ExPerformanceCounter(base.CategoryName, "Utilization: Write jobs (internal maintenance)", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.UtilizationWriteInternalMaintenance);
				this.UtilizationWrite = new ExPerformanceCounter(base.CategoryName, "Utilization: Write jobs", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.UtilizationWrite);
				this.ResourceHealthMDBLatencyHiPri = new ExPerformanceCounter(base.CategoryName, "Resource Health: MDB latency (high priority)", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ResourceHealthMDBLatencyHiPri);
				this.ResourceHealthMDBLatencyCustomerExpectation = new ExPerformanceCounter(base.CategoryName, "Resource Health: MDB latency (customer expectation)", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ResourceHealthMDBLatencyCustomerExpectation);
				this.ResourceHealthMDBLatencyInternalMaintenance = new ExPerformanceCounter(base.CategoryName, "Resource Health: MDB latency (internal maintenance)", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ResourceHealthMDBLatencyInternalMaintenance);
				this.ResourceHealthMDBLatency = new ExPerformanceCounter(base.CategoryName, "Resource Health: MDB latency", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ResourceHealthMDBLatency);
				this.DynamicCapacityMDBLatencyHiPri = new ExPerformanceCounter(base.CategoryName, "Dynamic Capacity: MDB latency (high priority)", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.DynamicCapacityMDBLatencyHiPri);
				this.DynamicCapacityMDBLatencyCustomerExpectation = new ExPerformanceCounter(base.CategoryName, "Dynamic Capacity: MDB latency (customer expectation)", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.DynamicCapacityMDBLatencyCustomerExpectation);
				this.DynamicCapacityMDBLatencyInternalMaintenance = new ExPerformanceCounter(base.CategoryName, "Dynamic Capacity: MDB latency (internal maintenance)", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.DynamicCapacityMDBLatencyInternalMaintenance);
				this.DynamicCapacityMDBLatency = new ExPerformanceCounter(base.CategoryName, "Dynamic Capacity: MDB latency", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.DynamicCapacityMDBLatency);
				this.ResourceHealthDiskLatencyHiPri = new ExPerformanceCounter(base.CategoryName, "Resource Health: Disk latency (high priority)", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ResourceHealthDiskLatencyHiPri);
				this.ResourceHealthDiskLatencyCustomerExpectation = new ExPerformanceCounter(base.CategoryName, "Resource Health: Disk latency (customer expectation)", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ResourceHealthDiskLatencyCustomerExpectation);
				this.ResourceHealthDiskLatencyInternalMaintenance = new ExPerformanceCounter(base.CategoryName, "Resource Health: Disk latency (internal maintenance)", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ResourceHealthDiskLatencyInternalMaintenance);
				this.ResourceHealthDiskLatency = new ExPerformanceCounter(base.CategoryName, "Resource Health: Disk latency", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ResourceHealthDiskLatency);
				this.DynamicCapacityDiskLatencyHiPri = new ExPerformanceCounter(base.CategoryName, "Dynamic Capacity: Disk latency (high priority)", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.DynamicCapacityDiskLatencyHiPri);
				this.DynamicCapacityDiskLatencyCustomerExpectation = new ExPerformanceCounter(base.CategoryName, "Dynamic Capacity: Disk latency (customer expectation)", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.DynamicCapacityDiskLatencyCustomerExpectation);
				this.DynamicCapacityDiskLatencyInternalMaintenance = new ExPerformanceCounter(base.CategoryName, "Dynamic Capacity: Disk latency (internal maintenance)", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.DynamicCapacityDiskLatencyInternalMaintenance);
				this.DynamicCapacityDiskLatency = new ExPerformanceCounter(base.CategoryName, "Dynamic Capacity: Disk latency", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.DynamicCapacityDiskLatency);
				this.ResourceHealthMDBReplicationHiPri = new ExPerformanceCounter(base.CategoryName, "Resource Health: MDB replication (high priority)", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ResourceHealthMDBReplicationHiPri);
				this.ResourceHealthMDBReplicationCustomerExpectation = new ExPerformanceCounter(base.CategoryName, "Resource Health: MDB replication (customer expectation)", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ResourceHealthMDBReplicationCustomerExpectation);
				this.ResourceHealthMDBReplicationInternalMaintenance = new ExPerformanceCounter(base.CategoryName, "Resource Health: MDB replication (internal maintenance)", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ResourceHealthMDBReplicationInternalMaintenance);
				this.ResourceHealthMDBReplication = new ExPerformanceCounter(base.CategoryName, "Resource Health: MDB replication", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ResourceHealthMDBReplication);
				this.DynamicCapacityMDBReplicationHiPri = new ExPerformanceCounter(base.CategoryName, "Dynamic Capacity: MDB replication (high priority)", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.DynamicCapacityMDBReplicationHiPri);
				this.DynamicCapacityMDBReplicationCustomerExpectation = new ExPerformanceCounter(base.CategoryName, "Dynamic Capacity: MDB replication (customer expectation)", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.DynamicCapacityMDBReplicationCustomerExpectation);
				this.DynamicCapacityMDBReplicationInternalMaintenance = new ExPerformanceCounter(base.CategoryName, "Dynamic Capacity: MDB replication (internal maintenance)", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.DynamicCapacityMDBReplicationInternalMaintenance);
				this.DynamicCapacityMDBReplication = new ExPerformanceCounter(base.CategoryName, "Dynamic Capacity: MDB replication", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.DynamicCapacityMDBReplication);
				this.ResourceHealthMDBAvailabilityHiPri = new ExPerformanceCounter(base.CategoryName, "Resource Health: MDB availability (high priority)", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ResourceHealthMDBAvailabilityHiPri);
				this.ResourceHealthMDBAvailabilityCustomerExpectation = new ExPerformanceCounter(base.CategoryName, "Resource Health: MDB availability (customer expectation)", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ResourceHealthMDBAvailabilityCustomerExpectation);
				this.ResourceHealthMDBAvailabilityInternalMaintenance = new ExPerformanceCounter(base.CategoryName, "Resource Health: MDB availability (internal maintenance)", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ResourceHealthMDBAvailabilityInternalMaintenance);
				this.ResourceHealthMDBAvailability = new ExPerformanceCounter(base.CategoryName, "Resource Health: MDB availability", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ResourceHealthMDBAvailability);
				this.DynamicCapacityMDBAvailabilityHiPri = new ExPerformanceCounter(base.CategoryName, "Dynamic Capacity: MDB availability (high priority)", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.DynamicCapacityMDBAvailabilityHiPri);
				this.DynamicCapacityMDBAvailabilityCustomerExpectation = new ExPerformanceCounter(base.CategoryName, "Dynamic Capacity: MDB availability (customer expectation)", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.DynamicCapacityMDBAvailabilityCustomerExpectation);
				this.DynamicCapacityMDBAvailabilityInternalMaintenance = new ExPerformanceCounter(base.CategoryName, "Dynamic Capacity: MDB availability (internal maintenance)", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.DynamicCapacityMDBAvailabilityInternalMaintenance);
				this.DynamicCapacityMDBAvailability = new ExPerformanceCounter(base.CategoryName, "Dynamic Capacity: MDB availability", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.DynamicCapacityMDBAvailability);
				this.ResourceHealthCIAgeOfLastNotificationHiPri = new ExPerformanceCounter(base.CategoryName, "Resource Health: CI age of last notification (high priority)", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ResourceHealthCIAgeOfLastNotificationHiPri);
				this.ResourceHealthCIAgeOfLastNotificationCustomerExpectation = new ExPerformanceCounter(base.CategoryName, "Resource Health: CI age of last notification (customer expectation)", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ResourceHealthCIAgeOfLastNotificationCustomerExpectation);
				this.ResourceHealthCIAgeOfLastNotificationInternalMaintenance = new ExPerformanceCounter(base.CategoryName, "Resource Health: CI age of last notification (internal maintenance)", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ResourceHealthCIAgeOfLastNotificationInternalMaintenance);
				this.ResourceHealthCIAgeOfLastNotification = new ExPerformanceCounter(base.CategoryName, "Resource Health: CI age of last notification", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ResourceHealthCIAgeOfLastNotification);
				this.DynamicCapacityCIAgeOfLastNotificationHiPri = new ExPerformanceCounter(base.CategoryName, "Dynamic Capacity: CI age of last notification (high priority)", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.DynamicCapacityCIAgeOfLastNotificationHiPri);
				this.DynamicCapacityCIAgeOfLastNotificationCustomerExpectation = new ExPerformanceCounter(base.CategoryName, "Dynamic Capacity: CI age of last notification (customer expectation)", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.DynamicCapacityCIAgeOfLastNotificationCustomerExpectation);
				this.DynamicCapacityCIAgeOfLastNotificationInternalMaintenance = new ExPerformanceCounter(base.CategoryName, "Dynamic Capacity: CI age of last notification (internal maintenance)", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.DynamicCapacityCIAgeOfLastNotificationInternalMaintenance);
				this.DynamicCapacityCIAgeOfLastNotification = new ExPerformanceCounter(base.CategoryName, "Dynamic Capacity: CI age of last notification", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.DynamicCapacityCIAgeOfLastNotification);
				long num = this.ActiveMovesTotal.RawValue;
				num += 1L;
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					foreach (ExPerformanceCounter exPerformanceCounter in list)
					{
						exPerformanceCounter.Close();
					}
				}
			}
			this.counters = list.ToArray();
		}

		// Token: 0x0600279B RID: 10139 RVA: 0x00058244 File Offset: 0x00056444
		public override void GetPerfCounterDiagnosticsInfo(XElement topElement)
		{
			XElement xelement = null;
			foreach (ExPerformanceCounter exPerformanceCounter in this.counters)
			{
				try
				{
					if (xelement == null)
					{
						xelement = new XElement(ExPerformanceCounter.GetEncodedName(exPerformanceCounter.InstanceName));
						topElement.Add(xelement);
					}
					xelement.Add(new XElement(ExPerformanceCounter.GetEncodedName(exPerformanceCounter.CounterName), exPerformanceCounter.NextValue()));
				}
				catch (XmlException ex)
				{
					XElement content = new XElement("Error", ex.Message);
					topElement.Add(content);
				}
			}
		}

		// Token: 0x040010C8 RID: 4296
		public readonly ExPerformanceCounter ActiveMovesTotal;

		// Token: 0x040010C9 RID: 4297
		public readonly ExPerformanceCounter ActiveMovesInitialSeeding;

		// Token: 0x040010CA RID: 4298
		public readonly ExPerformanceCounter ActiveMovesCompletion;

		// Token: 0x040010CB RID: 4299
		public readonly ExPerformanceCounter ActiveMovesStalledTotal;

		// Token: 0x040010CC RID: 4300
		public readonly ExPerformanceCounter ActiveMovesStalledHA;

		// Token: 0x040010CD RID: 4301
		public readonly ExPerformanceCounter ActiveMovesStalledCI;

		// Token: 0x040010CE RID: 4302
		public readonly ExPerformanceCounter ActiveMovesTransientFailures;

		// Token: 0x040010CF RID: 4303
		public readonly ExPerformanceCounter ActiveMovesNetworkFailures;

		// Token: 0x040010D0 RID: 4304
		public readonly ExPerformanceCounter ActiveMovesMDBOffline;

		// Token: 0x040010D1 RID: 4305
		public readonly ExPerformanceCounter ReadTransferRate;

		// Token: 0x040010D2 RID: 4306
		public readonly ExPerformanceCounter ReadTransferRateBase;

		// Token: 0x040010D3 RID: 4307
		public readonly ExPerformanceCounter WriteTransferRate;

		// Token: 0x040010D4 RID: 4308
		public readonly ExPerformanceCounter WriteTransferRateBase;

		// Token: 0x040010D5 RID: 4309
		public readonly ExPerformanceCounter MdbQueueQueued;

		// Token: 0x040010D6 RID: 4310
		public readonly ExPerformanceCounter MdbQueueInProgress;

		// Token: 0x040010D7 RID: 4311
		public readonly ExPerformanceCounter MoveRequestsCompleted;

		// Token: 0x040010D8 RID: 4312
		public readonly ExPerformanceCounter MoveRequestsCompletedRate;

		// Token: 0x040010D9 RID: 4313
		public readonly ExPerformanceCounter MoveRequestsCompletedRateBase;

		// Token: 0x040010DA RID: 4314
		public readonly ExPerformanceCounter MoveRequestsCompletedWithWarnings;

		// Token: 0x040010DB RID: 4315
		public readonly ExPerformanceCounter MoveRequestsCompletedWithWarningsRate;

		// Token: 0x040010DC RID: 4316
		public readonly ExPerformanceCounter MoveRequestsCompletedWithWarningsRateBase;

		// Token: 0x040010DD RID: 4317
		public readonly ExPerformanceCounter MoveRequestsCanceled;

		// Token: 0x040010DE RID: 4318
		public readonly ExPerformanceCounter MoveRequestsCanceledRate;

		// Token: 0x040010DF RID: 4319
		public readonly ExPerformanceCounter MoveRequestsCanceledRateBase;

		// Token: 0x040010E0 RID: 4320
		public readonly ExPerformanceCounter MoveRequestsTransientTotal;

		// Token: 0x040010E1 RID: 4321
		public readonly ExPerformanceCounter MoveRequestsTransientTotalRate;

		// Token: 0x040010E2 RID: 4322
		public readonly ExPerformanceCounter MoveRequestsTransientTotalRateBase;

		// Token: 0x040010E3 RID: 4323
		public readonly ExPerformanceCounter MoveRequestsNetworkFailures;

		// Token: 0x040010E4 RID: 4324
		public readonly ExPerformanceCounter MoveRequestsNetworkFailuresRate;

		// Token: 0x040010E5 RID: 4325
		public readonly ExPerformanceCounter MoveRequestsNetworkFailuresRateBase;

		// Token: 0x040010E6 RID: 4326
		public readonly ExPerformanceCounter MoveRequestsProxyBackoff;

		// Token: 0x040010E7 RID: 4327
		public readonly ExPerformanceCounter MoveRequestsProxyBackoffRate;

		// Token: 0x040010E8 RID: 4328
		public readonly ExPerformanceCounter MoveRequestsProxyBackoffRateBase;

		// Token: 0x040010E9 RID: 4329
		public readonly ExPerformanceCounter MoveRequestsFailTotal;

		// Token: 0x040010EA RID: 4330
		public readonly ExPerformanceCounter MoveRequestsFailTotalRate;

		// Token: 0x040010EB RID: 4331
		public readonly ExPerformanceCounter MoveRequestsFailTotalRateBase;

		// Token: 0x040010EC RID: 4332
		public readonly ExPerformanceCounter MoveRequestsFailBadItemLimit;

		// Token: 0x040010ED RID: 4333
		public readonly ExPerformanceCounter MoveRequestsFailBadItemLimitRate;

		// Token: 0x040010EE RID: 4334
		public readonly ExPerformanceCounter MoveRequestsFailBadItemLimitRateBase;

		// Token: 0x040010EF RID: 4335
		public readonly ExPerformanceCounter MoveRequestsFailNetwork;

		// Token: 0x040010F0 RID: 4336
		public readonly ExPerformanceCounter MoveRequestsFailNetworkRate;

		// Token: 0x040010F1 RID: 4337
		public readonly ExPerformanceCounter MoveRequestsFailNetworkRateBase;

		// Token: 0x040010F2 RID: 4338
		public readonly ExPerformanceCounter MoveRequestsFailStallCI;

		// Token: 0x040010F3 RID: 4339
		public readonly ExPerformanceCounter MoveRequestsFailStallCIRate;

		// Token: 0x040010F4 RID: 4340
		public readonly ExPerformanceCounter MoveRequestsFailStallCIRateBase;

		// Token: 0x040010F5 RID: 4341
		public readonly ExPerformanceCounter MoveRequestsFailStallHA;

		// Token: 0x040010F6 RID: 4342
		public readonly ExPerformanceCounter MoveRequestsFailStallHARate;

		// Token: 0x040010F7 RID: 4343
		public readonly ExPerformanceCounter MoveRequestsFailStallHARateBase;

		// Token: 0x040010F8 RID: 4344
		public readonly ExPerformanceCounter MoveRequestsFailMAPI;

		// Token: 0x040010F9 RID: 4345
		public readonly ExPerformanceCounter MoveRequestsFailMAPIRate;

		// Token: 0x040010FA RID: 4346
		public readonly ExPerformanceCounter MoveRequestsFailMAPIRateBase;

		// Token: 0x040010FB RID: 4347
		public readonly ExPerformanceCounter MoveRequestsFailOther;

		// Token: 0x040010FC RID: 4348
		public readonly ExPerformanceCounter MoveRequestsFailOtherRate;

		// Token: 0x040010FD RID: 4349
		public readonly ExPerformanceCounter MoveRequestsFailOtherRateBase;

		// Token: 0x040010FE RID: 4350
		public readonly ExPerformanceCounter MoveRequestsStallsTotal;

		// Token: 0x040010FF RID: 4351
		public readonly ExPerformanceCounter MoveRequestsStallsTotalRate;

		// Token: 0x04001100 RID: 4352
		public readonly ExPerformanceCounter MoveRequestsStallsTotalRateBase;

		// Token: 0x04001101 RID: 4353
		public readonly ExPerformanceCounter MoveRequestsStallsHA;

		// Token: 0x04001102 RID: 4354
		public readonly ExPerformanceCounter MoveRequestsStallsHARate;

		// Token: 0x04001103 RID: 4355
		public readonly ExPerformanceCounter MoveRequestsStallsHARateBase;

		// Token: 0x04001104 RID: 4356
		public readonly ExPerformanceCounter MoveRequestsStallsCI;

		// Token: 0x04001105 RID: 4357
		public readonly ExPerformanceCounter MoveRequestsStallsCIRate;

		// Token: 0x04001106 RID: 4358
		public readonly ExPerformanceCounter MoveRequestsStallsCIRateBase;

		// Token: 0x04001107 RID: 4359
		public readonly ExPerformanceCounter LastScanTime;

		// Token: 0x04001108 RID: 4360
		public readonly ExPerformanceCounter LastScanDuration;

		// Token: 0x04001109 RID: 4361
		public readonly ExPerformanceCounter LastScanFailure;

		// Token: 0x0400110A RID: 4362
		public readonly ExPerformanceCounter UtilizationReadHiPri;

		// Token: 0x0400110B RID: 4363
		public readonly ExPerformanceCounter UtilizationReadCustomerExpectation;

		// Token: 0x0400110C RID: 4364
		public readonly ExPerformanceCounter UtilizationReadInternalMaintenance;

		// Token: 0x0400110D RID: 4365
		public readonly ExPerformanceCounter UtilizationRead;

		// Token: 0x0400110E RID: 4366
		public readonly ExPerformanceCounter UtilizationWriteHiPri;

		// Token: 0x0400110F RID: 4367
		public readonly ExPerformanceCounter UtilizationWriteCustomerExpectation;

		// Token: 0x04001110 RID: 4368
		public readonly ExPerformanceCounter UtilizationWriteInternalMaintenance;

		// Token: 0x04001111 RID: 4369
		public readonly ExPerformanceCounter UtilizationWrite;

		// Token: 0x04001112 RID: 4370
		public readonly ExPerformanceCounter ResourceHealthMDBLatencyHiPri;

		// Token: 0x04001113 RID: 4371
		public readonly ExPerformanceCounter ResourceHealthMDBLatencyCustomerExpectation;

		// Token: 0x04001114 RID: 4372
		public readonly ExPerformanceCounter ResourceHealthMDBLatencyInternalMaintenance;

		// Token: 0x04001115 RID: 4373
		public readonly ExPerformanceCounter ResourceHealthMDBLatency;

		// Token: 0x04001116 RID: 4374
		public readonly ExPerformanceCounter DynamicCapacityMDBLatencyHiPri;

		// Token: 0x04001117 RID: 4375
		public readonly ExPerformanceCounter DynamicCapacityMDBLatencyCustomerExpectation;

		// Token: 0x04001118 RID: 4376
		public readonly ExPerformanceCounter DynamicCapacityMDBLatencyInternalMaintenance;

		// Token: 0x04001119 RID: 4377
		public readonly ExPerformanceCounter DynamicCapacityMDBLatency;

		// Token: 0x0400111A RID: 4378
		public readonly ExPerformanceCounter ResourceHealthDiskLatencyHiPri;

		// Token: 0x0400111B RID: 4379
		public readonly ExPerformanceCounter ResourceHealthDiskLatencyCustomerExpectation;

		// Token: 0x0400111C RID: 4380
		public readonly ExPerformanceCounter ResourceHealthDiskLatencyInternalMaintenance;

		// Token: 0x0400111D RID: 4381
		public readonly ExPerformanceCounter ResourceHealthDiskLatency;

		// Token: 0x0400111E RID: 4382
		public readonly ExPerformanceCounter DynamicCapacityDiskLatencyHiPri;

		// Token: 0x0400111F RID: 4383
		public readonly ExPerformanceCounter DynamicCapacityDiskLatencyCustomerExpectation;

		// Token: 0x04001120 RID: 4384
		public readonly ExPerformanceCounter DynamicCapacityDiskLatencyInternalMaintenance;

		// Token: 0x04001121 RID: 4385
		public readonly ExPerformanceCounter DynamicCapacityDiskLatency;

		// Token: 0x04001122 RID: 4386
		public readonly ExPerformanceCounter ResourceHealthMDBReplicationHiPri;

		// Token: 0x04001123 RID: 4387
		public readonly ExPerformanceCounter ResourceHealthMDBReplicationCustomerExpectation;

		// Token: 0x04001124 RID: 4388
		public readonly ExPerformanceCounter ResourceHealthMDBReplicationInternalMaintenance;

		// Token: 0x04001125 RID: 4389
		public readonly ExPerformanceCounter ResourceHealthMDBReplication;

		// Token: 0x04001126 RID: 4390
		public readonly ExPerformanceCounter DynamicCapacityMDBReplicationHiPri;

		// Token: 0x04001127 RID: 4391
		public readonly ExPerformanceCounter DynamicCapacityMDBReplicationCustomerExpectation;

		// Token: 0x04001128 RID: 4392
		public readonly ExPerformanceCounter DynamicCapacityMDBReplicationInternalMaintenance;

		// Token: 0x04001129 RID: 4393
		public readonly ExPerformanceCounter DynamicCapacityMDBReplication;

		// Token: 0x0400112A RID: 4394
		public readonly ExPerformanceCounter ResourceHealthMDBAvailabilityHiPri;

		// Token: 0x0400112B RID: 4395
		public readonly ExPerformanceCounter ResourceHealthMDBAvailabilityCustomerExpectation;

		// Token: 0x0400112C RID: 4396
		public readonly ExPerformanceCounter ResourceHealthMDBAvailabilityInternalMaintenance;

		// Token: 0x0400112D RID: 4397
		public readonly ExPerformanceCounter ResourceHealthMDBAvailability;

		// Token: 0x0400112E RID: 4398
		public readonly ExPerformanceCounter DynamicCapacityMDBAvailabilityHiPri;

		// Token: 0x0400112F RID: 4399
		public readonly ExPerformanceCounter DynamicCapacityMDBAvailabilityCustomerExpectation;

		// Token: 0x04001130 RID: 4400
		public readonly ExPerformanceCounter DynamicCapacityMDBAvailabilityInternalMaintenance;

		// Token: 0x04001131 RID: 4401
		public readonly ExPerformanceCounter DynamicCapacityMDBAvailability;

		// Token: 0x04001132 RID: 4402
		public readonly ExPerformanceCounter ResourceHealthCIAgeOfLastNotificationHiPri;

		// Token: 0x04001133 RID: 4403
		public readonly ExPerformanceCounter ResourceHealthCIAgeOfLastNotificationCustomerExpectation;

		// Token: 0x04001134 RID: 4404
		public readonly ExPerformanceCounter ResourceHealthCIAgeOfLastNotificationInternalMaintenance;

		// Token: 0x04001135 RID: 4405
		public readonly ExPerformanceCounter ResourceHealthCIAgeOfLastNotification;

		// Token: 0x04001136 RID: 4406
		public readonly ExPerformanceCounter DynamicCapacityCIAgeOfLastNotificationHiPri;

		// Token: 0x04001137 RID: 4407
		public readonly ExPerformanceCounter DynamicCapacityCIAgeOfLastNotificationCustomerExpectation;

		// Token: 0x04001138 RID: 4408
		public readonly ExPerformanceCounter DynamicCapacityCIAgeOfLastNotificationInternalMaintenance;

		// Token: 0x04001139 RID: 4409
		public readonly ExPerformanceCounter DynamicCapacityCIAgeOfLastNotification;
	}
}
