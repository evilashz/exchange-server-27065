using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.ProcessManager
{
	// Token: 0x0200017E RID: 382
	internal static class ProcessManagerServiceEventLogConstants
	{
		// Token: 0x0400065A RID: 1626
		public const string EventSource = "MSExchange Process Manager";

		// Token: 0x0400065B RID: 1627
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ServiceStartAttempt = new ExEventLog.EventTuple(1074004968U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400065C RID: 1628
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ServiceStartedSuccessFully = new ExEventLog.EventTuple(1074004969U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400065D RID: 1629
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ServiceStopAttempt = new ExEventLog.EventTuple(1074004970U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400065E RID: 1630
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ServiceStopped = new ExEventLog.EventTuple(1074004971U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400065F RID: 1631
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ConfigNotFound = new ExEventLog.EventTuple(3221488626U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000660 RID: 1632
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_BindingConfigNotFound = new ExEventLog.EventTuple(3221488627U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000661 RID: 1633
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CreateJobObjectFailed = new ExEventLog.EventTuple(3221488628U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000662 RID: 1634
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SetJobObjectFailed = new ExEventLog.EventTuple(3221488629U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000663 RID: 1635
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_WorkerImagePathNotExist = new ExEventLog.EventTuple(3221488630U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000664 RID: 1636
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_WorkerStartFailed = new ExEventLog.EventTuple(3221488631U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000665 RID: 1637
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_WorkerStartThrashCount = new ExEventLog.EventTuple(3221488632U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000666 RID: 1638
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_BindingIPv6ButDisabled = new ExEventLog.EventTuple(3221488633U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000667 RID: 1639
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AddressInUse = new ExEventLog.EventTuple(3221488634U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000668 RID: 1640
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SocketListenError = new ExEventLog.EventTuple(3221488635U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000669 RID: 1641
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_WorkerProcessExit = new ExEventLog.EventTuple(1074004988U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400066A RID: 1642
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_WorkerProcessReplace = new ExEventLog.EventTuple(1074004989U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400066B RID: 1643
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_WorkerProcessExitServiceStop = new ExEventLog.EventTuple(1074004990U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400066C RID: 1644
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_WorkerProcessRefreshControlCommand = new ExEventLog.EventTuple(1074004991U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400066D RID: 1645
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_WorkerProcessRefreshProcessData = new ExEventLog.EventTuple(1074004992U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400066E RID: 1646
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_WorkerProcessRefreshMaxThread = new ExEventLog.EventTuple(1074004993U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400066F RID: 1647
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_WorkerProcessRefreshMaxWorkingSet = new ExEventLog.EventTuple(1074004994U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000670 RID: 1648
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_WorkerProcessRefreshInterval = new ExEventLog.EventTuple(1074004995U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000671 RID: 1649
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_WorkerProcessPauseCommand = new ExEventLog.EventTuple(1074004996U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000672 RID: 1650
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_WorkerProcessContinueCommand = new ExEventLog.EventTuple(1074004997U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000673 RID: 1651
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_IPConnectionRateExceeded = new ExEventLog.EventTuple(1074004998U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000674 RID: 1652
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AppConfigLoadFailed = new ExEventLog.EventTuple(3221488647U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000675 RID: 1653
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SocketAccessDenied = new ExEventLog.EventTuple(3221488648U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000676 RID: 1654
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_WorkerProcessRestartScheduled = new ExEventLog.EventTuple(3221488649U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000677 RID: 1655
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RestartWorkerProcess = new ExEventLog.EventTuple(1074005002U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000678 RID: 1656
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ServiceStoppingOnSocketOpenError = new ExEventLog.EventTuple(3221488652U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000679 RID: 1657
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_WorkerProcessForcedWatsonException = new ExEventLog.EventTuple(1074005005U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400067A RID: 1658
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_WorkerProcessClearConfigCache = new ExEventLog.EventTuple(1074005006U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400067B RID: 1659
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_WaitForProcessHandleCloseTimedOut = new ExEventLog.EventTuple(2147746831U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400067C RID: 1660
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ExistingWorkerProcessHasExitedValue = new ExEventLog.EventTuple(1074005008U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400067D RID: 1661
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FailedToStoreLastWorkerProcessId = new ExEventLog.EventTuple(3221488657U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400067E RID: 1662
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CreatedExistingJobObject = new ExEventLog.EventTuple(2147746834U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400067F RID: 1663
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FailedToKillWorkerProcess = new ExEventLog.EventTuple(2147746835U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000680 RID: 1664
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AttemptToKillWorkerProcess = new ExEventLog.EventTuple(1074005012U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000681 RID: 1665
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_WorkerProcessRefreshProcessDataFetchFailed = new ExEventLog.EventTuple(1074005013U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000682 RID: 1666
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_WorkerProcessExitServiceTerminateWithUnhandledException = new ExEventLog.EventTuple(2147746838U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0200017F RID: 383
		private enum Category : short
		{
			// Token: 0x04000684 RID: 1668
			ProcessManager = 1
		}

		// Token: 0x02000180 RID: 384
		internal enum Message : uint
		{
			// Token: 0x04000686 RID: 1670
			ServiceStartAttempt = 1074004968U,
			// Token: 0x04000687 RID: 1671
			ServiceStartedSuccessFully,
			// Token: 0x04000688 RID: 1672
			ServiceStopAttempt,
			// Token: 0x04000689 RID: 1673
			ServiceStopped,
			// Token: 0x0400068A RID: 1674
			ConfigNotFound = 3221488626U,
			// Token: 0x0400068B RID: 1675
			BindingConfigNotFound,
			// Token: 0x0400068C RID: 1676
			CreateJobObjectFailed,
			// Token: 0x0400068D RID: 1677
			SetJobObjectFailed,
			// Token: 0x0400068E RID: 1678
			WorkerImagePathNotExist,
			// Token: 0x0400068F RID: 1679
			WorkerStartFailed,
			// Token: 0x04000690 RID: 1680
			WorkerStartThrashCount,
			// Token: 0x04000691 RID: 1681
			BindingIPv6ButDisabled,
			// Token: 0x04000692 RID: 1682
			AddressInUse,
			// Token: 0x04000693 RID: 1683
			SocketListenError,
			// Token: 0x04000694 RID: 1684
			WorkerProcessExit = 1074004988U,
			// Token: 0x04000695 RID: 1685
			WorkerProcessReplace,
			// Token: 0x04000696 RID: 1686
			WorkerProcessExitServiceStop,
			// Token: 0x04000697 RID: 1687
			WorkerProcessRefreshControlCommand,
			// Token: 0x04000698 RID: 1688
			WorkerProcessRefreshProcessData,
			// Token: 0x04000699 RID: 1689
			WorkerProcessRefreshMaxThread,
			// Token: 0x0400069A RID: 1690
			WorkerProcessRefreshMaxWorkingSet,
			// Token: 0x0400069B RID: 1691
			WorkerProcessRefreshInterval,
			// Token: 0x0400069C RID: 1692
			WorkerProcessPauseCommand,
			// Token: 0x0400069D RID: 1693
			WorkerProcessContinueCommand,
			// Token: 0x0400069E RID: 1694
			IPConnectionRateExceeded,
			// Token: 0x0400069F RID: 1695
			AppConfigLoadFailed = 3221488647U,
			// Token: 0x040006A0 RID: 1696
			SocketAccessDenied,
			// Token: 0x040006A1 RID: 1697
			WorkerProcessRestartScheduled,
			// Token: 0x040006A2 RID: 1698
			RestartWorkerProcess = 1074005002U,
			// Token: 0x040006A3 RID: 1699
			ServiceStoppingOnSocketOpenError = 3221488652U,
			// Token: 0x040006A4 RID: 1700
			WorkerProcessForcedWatsonException = 1074005005U,
			// Token: 0x040006A5 RID: 1701
			WorkerProcessClearConfigCache,
			// Token: 0x040006A6 RID: 1702
			WaitForProcessHandleCloseTimedOut = 2147746831U,
			// Token: 0x040006A7 RID: 1703
			ExistingWorkerProcessHasExitedValue = 1074005008U,
			// Token: 0x040006A8 RID: 1704
			FailedToStoreLastWorkerProcessId = 3221488657U,
			// Token: 0x040006A9 RID: 1705
			CreatedExistingJobObject = 2147746834U,
			// Token: 0x040006AA RID: 1706
			FailedToKillWorkerProcess,
			// Token: 0x040006AB RID: 1707
			AttemptToKillWorkerProcess = 1074005012U,
			// Token: 0x040006AC RID: 1708
			WorkerProcessRefreshProcessDataFetchFailed,
			// Token: 0x040006AD RID: 1709
			WorkerProcessExitServiceTerminateWithUnhandledException = 2147746838U
		}
	}
}
