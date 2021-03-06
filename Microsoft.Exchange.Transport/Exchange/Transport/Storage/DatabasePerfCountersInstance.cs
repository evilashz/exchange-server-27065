using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Storage
{
	// Token: 0x0200055E RID: 1374
	internal sealed class DatabasePerfCountersInstance : PerformanceCounterInstance
	{
		// Token: 0x06003F23 RID: 16163 RVA: 0x0010EC54 File Offset: 0x0010CE54
		internal DatabasePerfCountersInstance(string instanceName, DatabasePerfCountersInstance autoUpdateTotalInstance) : base(instanceName, "MSExchangeTransport Database")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.TransactionPendingCount = new ExPerformanceCounter(base.CategoryName, "Transaction Pending Count", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.TransactionPendingCount, new ExPerformanceCounter[0]);
				list.Add(this.TransactionPendingCount);
				ExPerformanceCounter exPerformanceCounter = new ExPerformanceCounter(base.CategoryName, "Transactions/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter);
				this.TransactionCount = new ExPerformanceCounter(base.CategoryName, "Transaction Count", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.TransactionCount, new ExPerformanceCounter[]
				{
					exPerformanceCounter
				});
				list.Add(this.TransactionCount);
				this.TransactionPending99PercentileDuration = new ExPerformanceCounter(base.CategoryName, "Transaction Pending 99 Percentile Duration", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.TransactionPending99PercentileDuration, new ExPerformanceCounter[0]);
				list.Add(this.TransactionPending99PercentileDuration);
				this.TransactionAveragePendingDuration = new ExPerformanceCounter(base.CategoryName, "Transaction Pending Average Duration", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.TransactionAveragePendingDuration, new ExPerformanceCounter[0]);
				list.Add(this.TransactionAveragePendingDuration);
				this.TransactionAveragePendingDurationBase = new ExPerformanceCounter(base.CategoryName, "Transaction Pending Average Duration Base", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.TransactionAveragePendingDurationBase, new ExPerformanceCounter[0]);
				list.Add(this.TransactionAveragePendingDurationBase);
				this.TransactionSoftCommitPendingCount = new ExPerformanceCounter(base.CategoryName, "Transaction Commit Soft Pending", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.TransactionSoftCommitPendingCount, new ExPerformanceCounter[0]);
				list.Add(this.TransactionSoftCommitPendingCount);
				ExPerformanceCounter exPerformanceCounter2 = new ExPerformanceCounter(base.CategoryName, "Transaction Commit Soft/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter2);
				this.TransactionSoftCommitCount = new ExPerformanceCounter(base.CategoryName, "Transaction Commit Soft Count", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.TransactionSoftCommitCount, new ExPerformanceCounter[]
				{
					exPerformanceCounter2
				});
				list.Add(this.TransactionSoftCommitCount);
				this.TransactionSoftCommitAveragePendingDuration = new ExPerformanceCounter(base.CategoryName, "Transaction Commit Soft Average Pending Duration", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.TransactionSoftCommitAveragePendingDuration, new ExPerformanceCounter[0]);
				list.Add(this.TransactionSoftCommitAveragePendingDuration);
				this.TransactionSoftCommitAveragePendingDurationBase = new ExPerformanceCounter(base.CategoryName, "Transaction Commit Soft Average Pending Duration Base", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.TransactionSoftCommitAveragePendingDurationBase, new ExPerformanceCounter[0]);
				list.Add(this.TransactionSoftCommitAveragePendingDurationBase);
				this.TransactionHardCommitPendingCount = new ExPerformanceCounter(base.CategoryName, "Transaction Commit Hard Pending", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.TransactionHardCommitPendingCount, new ExPerformanceCounter[0]);
				list.Add(this.TransactionHardCommitPendingCount);
				ExPerformanceCounter exPerformanceCounter3 = new ExPerformanceCounter(base.CategoryName, "Transaction Commit Hard/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter3);
				this.TransactionHardCommitCount = new ExPerformanceCounter(base.CategoryName, "Transaction Commit Hard Count", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.TransactionHardCommitCount, new ExPerformanceCounter[]
				{
					exPerformanceCounter3
				});
				list.Add(this.TransactionHardCommitCount);
				ExPerformanceCounter exPerformanceCounter4 = new ExPerformanceCounter(base.CategoryName, "Transaction Aborts/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter4);
				this.TransactionAbortCount = new ExPerformanceCounter(base.CategoryName, "Transaction Abort Count", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.TransactionAbortCount, new ExPerformanceCounter[]
				{
					exPerformanceCounter4
				});
				list.Add(this.TransactionAbortCount);
				this.TransactionHardCommitAveragePendingDuration = new ExPerformanceCounter(base.CategoryName, "Transaction Commit Hard Pending Average Duration", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.TransactionHardCommitAveragePendingDuration, new ExPerformanceCounter[0]);
				list.Add(this.TransactionHardCommitAveragePendingDuration);
				this.TransactionHardCommitAveragePendingDurationBase = new ExPerformanceCounter(base.CategoryName, "Transaction Commit Hard Pending Average Duration Base", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.TransactionHardCommitAveragePendingDurationBase, new ExPerformanceCounter[0]);
				list.Add(this.TransactionHardCommitAveragePendingDurationBase);
				this.TransactionAsyncCommitPendingCount = new ExPerformanceCounter(base.CategoryName, "Transaction Commit Async Pending", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.TransactionAsyncCommitPendingCount, new ExPerformanceCounter[0]);
				list.Add(this.TransactionAsyncCommitPendingCount);
				ExPerformanceCounter exPerformanceCounter5 = new ExPerformanceCounter(base.CategoryName, "Transaction Commit Async/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter5);
				this.TransactionAsyncCommitCount = new ExPerformanceCounter(base.CategoryName, "Transaction Commit Async Count", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.TransactionAsyncCommitCount, new ExPerformanceCounter[]
				{
					exPerformanceCounter5
				});
				list.Add(this.TransactionAsyncCommitCount);
				this.TransactionAsyncCommitAveragePendingDuration = new ExPerformanceCounter(base.CategoryName, "Transaction Commit Async Average Pending Duration", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.TransactionAsyncCommitAveragePendingDuration, new ExPerformanceCounter[0]);
				list.Add(this.TransactionAsyncCommitAveragePendingDuration);
				this.TransactionAsyncCommitAveragePendingDurationBase = new ExPerformanceCounter(base.CategoryName, "Transaction Commit Async Pending Average Duration Base", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.TransactionAsyncCommitAveragePendingDurationBase, new ExPerformanceCounter[0]);
				list.Add(this.TransactionAsyncCommitAveragePendingDurationBase);
				ExPerformanceCounter exPerformanceCounter6 = new ExPerformanceCounter(base.CategoryName, "Transaction Durable Callback Count/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter6);
				this.TransactionDurableCallbackCount = new ExPerformanceCounter(base.CategoryName, "Transaction Durable Callback Count", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.TransactionDurableCallbackCount, new ExPerformanceCounter[]
				{
					exPerformanceCounter6
				});
				list.Add(this.TransactionDurableCallbackCount);
				this.MailItemCount = new ExPerformanceCounter(base.CategoryName, "MailItem Count", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MailItemCount, new ExPerformanceCounter[0]);
				list.Add(this.MailItemCount);
				this.MailRecipientCount = new ExPerformanceCounter(base.CategoryName, "MailRecipient Count", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MailRecipientCount, new ExPerformanceCounter[0]);
				list.Add(this.MailRecipientCount);
				this.MailRecipientActiveCount = new ExPerformanceCounter(base.CategoryName, "MailRecipient Active Count", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MailRecipientActiveCount, new ExPerformanceCounter[0]);
				list.Add(this.MailRecipientActiveCount);
				this.MailRecipientSafetyNetCount = new ExPerformanceCounter(base.CategoryName, "MailRecipient SafetyNet Count", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MailRecipientSafetyNetCount, new ExPerformanceCounter[0]);
				list.Add(this.MailRecipientSafetyNetCount);
				this.MailRecipientSafetyNetMdbCount = new ExPerformanceCounter(base.CategoryName, "MailRecipient SafetyNet Mdb Count", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MailRecipientSafetyNetMdbCount, new ExPerformanceCounter[0]);
				list.Add(this.MailRecipientSafetyNetMdbCount);
				this.MailRecipientShadowSafetyNetCount = new ExPerformanceCounter(base.CategoryName, "MailRecipient Shadow SafetyNet Count", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MailRecipientShadowSafetyNetCount, new ExPerformanceCounter[0]);
				list.Add(this.MailRecipientShadowSafetyNetCount);
				this.TransportQueueDatabaseFileSize = new ExPerformanceCounter(base.CategoryName, "Transport Queue Database File Size (MB)", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.TransportQueueDatabaseFileSize, new ExPerformanceCounter[0]);
				list.Add(this.TransportQueueDatabaseFileSize);
				this.TransportQueueDatabaseInternalFreeSpace = new ExPerformanceCounter(base.CategoryName, "Transport Queue Database Internal Free Space (MB)", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.TransportQueueDatabaseInternalFreeSpace, new ExPerformanceCounter[0]);
				list.Add(this.TransportQueueDatabaseInternalFreeSpace);
				this.GenerationCount = new ExPerformanceCounter(base.CategoryName, "Generation Count", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.GenerationCount, new ExPerformanceCounter[0]);
				list.Add(this.GenerationCount);
				this.GenerationExpiredCount = new ExPerformanceCounter(base.CategoryName, "Generation Expired Count", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.GenerationExpiredCount, new ExPerformanceCounter[0]);
				list.Add(this.GenerationExpiredCount);
				this.GenerationLastCleanupLatency = new ExPerformanceCounter(base.CategoryName, "Generation Last Cleanup Latency", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.GenerationLastCleanupLatency, new ExPerformanceCounter[0]);
				list.Add(this.GenerationLastCleanupLatency);
				this.BootloaderOutstandingItems = new ExPerformanceCounter(base.CategoryName, "Bootloader Outstanding Items", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.BootloaderOutstandingItems, new ExPerformanceCounter[0]);
				list.Add(this.BootloaderOutstandingItems);
				ExPerformanceCounter exPerformanceCounter7 = new ExPerformanceCounter(base.CategoryName, "Bootloaded Items/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter7);
				this.BootloadedItemCount = new ExPerformanceCounter(base.CategoryName, "Bootloaded Item Count", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.BootloadedItemCount, new ExPerformanceCounter[]
				{
					exPerformanceCounter7
				});
				list.Add(this.BootloadedItemCount);
				this.BootloadedItemAverageLatency = new ExPerformanceCounter(base.CategoryName, "Bootloaded Item Average Latency", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.BootloadedItemAverageLatency, new ExPerformanceCounter[0]);
				list.Add(this.BootloadedItemAverageLatency);
				this.BootloadedItemAverageLatencyBase = new ExPerformanceCounter(base.CategoryName, "Bootloaded Item Average Latency Base", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.BootloadedItemAverageLatencyBase, new ExPerformanceCounter[0]);
				list.Add(this.BootloadedItemAverageLatencyBase);
				this.BootloadedRecentPoisonMessageCount = new ExPerformanceCounter(base.CategoryName, "Bootloaded Recent (within 24 hours) Poison Messages", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.BootloadedRecentPoisonMessageCount, new ExPerformanceCounter[0]);
				list.Add(this.BootloadedRecentPoisonMessageCount);
				ExPerformanceCounter exPerformanceCounter8 = new ExPerformanceCounter(base.CategoryName, "Replayed Items/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter8);
				this.ReplayedItemCount = new ExPerformanceCounter(base.CategoryName, "Replayed Item Count", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.ReplayedItemCount, new ExPerformanceCounter[]
				{
					exPerformanceCounter8
				});
				list.Add(this.ReplayedItemCount);
				this.ReplayedItemAverageLatency = new ExPerformanceCounter(base.CategoryName, "Replayed Item Average Latency", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.ReplayedItemAverageLatency, new ExPerformanceCounter[0]);
				list.Add(this.ReplayedItemAverageLatency);
				this.ReplayedItemAverageLatencyBase = new ExPerformanceCounter(base.CategoryName, "Average Replayed Item Average Latency Base", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.ReplayedItemAverageLatencyBase, new ExPerformanceCounter[0]);
				list.Add(this.ReplayedItemAverageLatencyBase);
				this.ReplayBookmarkAverageLatency = new ExPerformanceCounter(base.CategoryName, "Replay Bookmark Average Latency", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.ReplayBookmarkAverageLatency, new ExPerformanceCounter[0]);
				list.Add(this.ReplayBookmarkAverageLatency);
				this.ReplayBookmarkAverageLatencyBase = new ExPerformanceCounter(base.CategoryName, "Replay Bookmark Average Latency Base", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.ReplayBookmarkAverageLatencyBase, new ExPerformanceCounter[0]);
				list.Add(this.ReplayBookmarkAverageLatencyBase);
				ExPerformanceCounter exPerformanceCounter9 = new ExPerformanceCounter(base.CategoryName, "DataRow seeks/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter9);
				this.Seeks = new ExPerformanceCounter(base.CategoryName, "DataRow seeks total", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.Seeks, new ExPerformanceCounter[]
				{
					exPerformanceCounter9
				});
				list.Add(this.Seeks);
				ExPerformanceCounter exPerformanceCounter10 = new ExPerformanceCounter(base.CategoryName, "DataRow seeks prefix/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter10);
				this.PrefixSeeks = new ExPerformanceCounter(base.CategoryName, "DataRow seeks prefix total", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PrefixSeeks, new ExPerformanceCounter[]
				{
					exPerformanceCounter10
				});
				list.Add(this.PrefixSeeks);
				ExPerformanceCounter exPerformanceCounter11 = new ExPerformanceCounter(base.CategoryName, "DataRow loads/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter11);
				this.LoadFromCurrent = new ExPerformanceCounter(base.CategoryName, "DataRow loads total", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.LoadFromCurrent, new ExPerformanceCounter[]
				{
					exPerformanceCounter11
				});
				list.Add(this.LoadFromCurrent);
				ExPerformanceCounter exPerformanceCounter12 = new ExPerformanceCounter(base.CategoryName, "DataRow updates/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter12);
				this.Update = new ExPerformanceCounter(base.CategoryName, "DataRow updates total", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.Update, new ExPerformanceCounter[]
				{
					exPerformanceCounter12
				});
				list.Add(this.Update);
				ExPerformanceCounter exPerformanceCounter13 = new ExPerformanceCounter(base.CategoryName, "DataRow new inserts/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter13);
				this.New = new ExPerformanceCounter(base.CategoryName, "DataRow new inserts total", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.New, new ExPerformanceCounter[]
				{
					exPerformanceCounter13
				});
				list.Add(this.New);
				ExPerformanceCounter exPerformanceCounter14 = new ExPerformanceCounter(base.CategoryName, "DataRow clones/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter14);
				this.Clone = new ExPerformanceCounter(base.CategoryName, "DataRow clones total", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.Clone, new ExPerformanceCounter[]
				{
					exPerformanceCounter14
				});
				list.Add(this.Clone);
				ExPerformanceCounter exPerformanceCounter15 = new ExPerformanceCounter(base.CategoryName, "DataRow moves/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter15);
				this.Move = new ExPerformanceCounter(base.CategoryName, "DataRow moves total", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.Move, new ExPerformanceCounter[]
				{
					exPerformanceCounter15
				});
				list.Add(this.Move);
				ExPerformanceCounter exPerformanceCounter16 = new ExPerformanceCounter(base.CategoryName, "DataRow deletes/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter16);
				this.Delete = new ExPerformanceCounter(base.CategoryName, "DataRow deletes total", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.Delete, new ExPerformanceCounter[]
				{
					exPerformanceCounter16
				});
				list.Add(this.Delete);
				ExPerformanceCounter exPerformanceCounter17 = new ExPerformanceCounter(base.CategoryName, "DataRow minimize memory/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter17);
				this.MinimizeMemory = new ExPerformanceCounter(base.CategoryName, "DataRow minimize memory total", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MinimizeMemory, new ExPerformanceCounter[]
				{
					exPerformanceCounter17
				});
				list.Add(this.MinimizeMemory);
				ExPerformanceCounter exPerformanceCounter18 = new ExPerformanceCounter(base.CategoryName, "Stream read/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter18);
				this.StreamReads = new ExPerformanceCounter(base.CategoryName, "Stream read total", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.StreamReads, new ExPerformanceCounter[]
				{
					exPerformanceCounter18
				});
				list.Add(this.StreamReads);
				ExPerformanceCounter exPerformanceCounter19 = new ExPerformanceCounter(base.CategoryName, "Stream bytes read/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter19);
				this.StreamBytesRead = new ExPerformanceCounter(base.CategoryName, "Stream bytes read total", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.StreamBytesRead, new ExPerformanceCounter[]
				{
					exPerformanceCounter19
				});
				list.Add(this.StreamBytesRead);
				ExPerformanceCounter exPerformanceCounter20 = new ExPerformanceCounter(base.CategoryName, "Stream writes/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter20);
				this.StreamWrites = new ExPerformanceCounter(base.CategoryName, "Stream writes total", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.StreamWrites, new ExPerformanceCounter[]
				{
					exPerformanceCounter20
				});
				list.Add(this.StreamWrites);
				ExPerformanceCounter exPerformanceCounter21 = new ExPerformanceCounter(base.CategoryName, "Stream bytes written/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter21);
				this.StreamBytesWritten = new ExPerformanceCounter(base.CategoryName, "Stream bytes written total", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.StreamBytesWritten, new ExPerformanceCounter[]
				{
					exPerformanceCounter21
				});
				list.Add(this.StreamBytesWritten);
				ExPerformanceCounter exPerformanceCounter22 = new ExPerformanceCounter(base.CategoryName, "Stream set length/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter22);
				this.StreamSetLen = new ExPerformanceCounter(base.CategoryName, "Stream set length count", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.StreamSetLen, new ExPerformanceCounter[]
				{
					exPerformanceCounter22
				});
				list.Add(this.StreamSetLen);
				ExPerformanceCounter exPerformanceCounter23 = new ExPerformanceCounter(base.CategoryName, "Lazy bytes load requested/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter23);
				this.LazyBytesLoadRequested = new ExPerformanceCounter(base.CategoryName, "Lazy bytes load requested total", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.LazyBytesLoadRequested, new ExPerformanceCounter[]
				{
					exPerformanceCounter23
				});
				list.Add(this.LazyBytesLoadRequested);
				ExPerformanceCounter exPerformanceCounter24 = new ExPerformanceCounter(base.CategoryName, "Lazy bytes load performed/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter24);
				this.LazyBytesLoadPerformed = new ExPerformanceCounter(base.CategoryName, "Lazy bytes load performed total", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.LazyBytesLoadPerformed, new ExPerformanceCounter[]
				{
					exPerformanceCounter24
				});
				list.Add(this.LazyBytesLoadPerformed);
				ExPerformanceCounter exPerformanceCounter25 = new ExPerformanceCounter(base.CategoryName, "Column cache load/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter25);
				this.ColumnsCacheLoads = new ExPerformanceCounter(base.CategoryName, "Column cache load total", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.ColumnsCacheLoads, new ExPerformanceCounter[]
				{
					exPerformanceCounter25
				});
				list.Add(this.ColumnsCacheLoads);
				ExPerformanceCounter exPerformanceCounter26 = new ExPerformanceCounter(base.CategoryName, "Column cache loaded columns/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter26);
				this.ColumnsCacheColumnLoads = new ExPerformanceCounter(base.CategoryName, "Column cache loaded columns total", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.ColumnsCacheColumnLoads, new ExPerformanceCounter[]
				{
					exPerformanceCounter26
				});
				list.Add(this.ColumnsCacheColumnLoads);
				ExPerformanceCounter exPerformanceCounter27 = new ExPerformanceCounter(base.CategoryName, "Column cache loaded bytes/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter27);
				this.ColumnsCacheByteLoads = new ExPerformanceCounter(base.CategoryName, "Column cache loaded bytes total", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.ColumnsCacheByteLoads, new ExPerformanceCounter[]
				{
					exPerformanceCounter27
				});
				list.Add(this.ColumnsCacheByteLoads);
				ExPerformanceCounter exPerformanceCounter28 = new ExPerformanceCounter(base.CategoryName, "Column cache save/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter28);
				this.ColumnsCacheSaves = new ExPerformanceCounter(base.CategoryName, "Column cache save total", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.ColumnsCacheSaves, new ExPerformanceCounter[]
				{
					exPerformanceCounter28
				});
				list.Add(this.ColumnsCacheSaves);
				ExPerformanceCounter exPerformanceCounter29 = new ExPerformanceCounter(base.CategoryName, "Column cache saved columns/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter29);
				this.ColumnsCacheColumnSaves = new ExPerformanceCounter(base.CategoryName, "Column cache saved columns total", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.ColumnsCacheColumnSaves, new ExPerformanceCounter[]
				{
					exPerformanceCounter29
				});
				list.Add(this.ColumnsCacheColumnSaves);
				ExPerformanceCounter exPerformanceCounter30 = new ExPerformanceCounter(base.CategoryName, "Column cache saved bytes/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter30);
				this.ColumnsCacheByteSaves = new ExPerformanceCounter(base.CategoryName, "Column cache saved bytes total", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.ColumnsCacheByteSaves, new ExPerformanceCounter[]
				{
					exPerformanceCounter30
				});
				list.Add(this.ColumnsCacheByteSaves);
				ExPerformanceCounter exPerformanceCounter31 = new ExPerformanceCounter(base.CategoryName, "Extended property writes/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter31);
				this.ExtendedPropertyWrites = new ExPerformanceCounter(base.CategoryName, "Extended property writes total", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.ExtendedPropertyWrites, new ExPerformanceCounter[]
				{
					exPerformanceCounter31
				});
				list.Add(this.ExtendedPropertyWrites);
				ExPerformanceCounter exPerformanceCounter32 = new ExPerformanceCounter(base.CategoryName, "Extended property bytes written/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter32);
				this.ExtendedPropertyBytesWritten = new ExPerformanceCounter(base.CategoryName, "Extended property bytes written total", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.ExtendedPropertyBytesWritten, new ExPerformanceCounter[]
				{
					exPerformanceCounter32
				});
				list.Add(this.ExtendedPropertyBytesWritten);
				ExPerformanceCounter exPerformanceCounter33 = new ExPerformanceCounter(base.CategoryName, "Extended property reads/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter33);
				this.ExtendedPropertyReads = new ExPerformanceCounter(base.CategoryName, "Extended property reads total", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.ExtendedPropertyReads, new ExPerformanceCounter[]
				{
					exPerformanceCounter33
				});
				list.Add(this.ExtendedPropertyReads);
				ExPerformanceCounter exPerformanceCounter34 = new ExPerformanceCounter(base.CategoryName, "Extended property bytes read/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter34);
				this.ExtendedPropertyBytesRead = new ExPerformanceCounter(base.CategoryName, "Extended property bytes read total", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.ExtendedPropertyBytesRead, new ExPerformanceCounter[]
				{
					exPerformanceCounter34
				});
				list.Add(this.ExtendedPropertyBytesRead);
				ExPerformanceCounter exPerformanceCounter35 = new ExPerformanceCounter(base.CategoryName, "MailItem new/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter35);
				this.NewMailItem = new ExPerformanceCounter(base.CategoryName, "MailItem new total", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.NewMailItem, new ExPerformanceCounter[]
				{
					exPerformanceCounter35
				});
				list.Add(this.NewMailItem);
				ExPerformanceCounter exPerformanceCounter36 = new ExPerformanceCounter(base.CategoryName, "MailItem clone create/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter36);
				this.NewCloneMailItem = new ExPerformanceCounter(base.CategoryName, "MailItem clone create total", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.NewCloneMailItem, new ExPerformanceCounter[]
				{
					exPerformanceCounter36
				});
				list.Add(this.NewCloneMailItem);
				ExPerformanceCounter exPerformanceCounter37 = new ExPerformanceCounter(base.CategoryName, "MailItem dehydrate/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter37);
				this.DehydrateMailItem = new ExPerformanceCounter(base.CategoryName, "MailItem dehydrate total", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.DehydrateMailItem, new ExPerformanceCounter[]
				{
					exPerformanceCounter37
				});
				list.Add(this.DehydrateMailItem);
				ExPerformanceCounter exPerformanceCounter38 = new ExPerformanceCounter(base.CategoryName, "MailItem load/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter38);
				this.LoadMailItem = new ExPerformanceCounter(base.CategoryName, "MailItem load total", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.LoadMailItem, new ExPerformanceCounter[]
				{
					exPerformanceCounter38
				});
				list.Add(this.LoadMailItem);
				ExPerformanceCounter exPerformanceCounter39 = new ExPerformanceCounter(base.CategoryName, "MailItem commit immediate/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter39);
				this.CommitImmediateMailItem = new ExPerformanceCounter(base.CategoryName, "MailItem commit immediate total", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.CommitImmediateMailItem, new ExPerformanceCounter[]
				{
					exPerformanceCounter39
				});
				list.Add(this.CommitImmediateMailItem);
				ExPerformanceCounter exPerformanceCounter40 = new ExPerformanceCounter(base.CategoryName, "MailItem materialize/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter40);
				this.MaterializeMailItem = new ExPerformanceCounter(base.CategoryName, "MailItem materialize", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MaterializeMailItem, new ExPerformanceCounter[]
				{
					exPerformanceCounter40
				});
				list.Add(this.MaterializeMailItem);
				ExPerformanceCounter exPerformanceCounter41 = new ExPerformanceCounter(base.CategoryName, "MailItem begin commit/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter41);
				this.BeginCommitMailItem = new ExPerformanceCounter(base.CategoryName, "MailItem begin commit total", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.BeginCommitMailItem, new ExPerformanceCounter[]
				{
					exPerformanceCounter41
				});
				list.Add(this.BeginCommitMailItem);
				ExPerformanceCounter exPerformanceCounter42 = new ExPerformanceCounter(base.CategoryName, "MailItem commit lazy/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter42);
				this.CommitLazyMailItem = new ExPerformanceCounter(base.CategoryName, "MailItem commit lazy total", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.CommitLazyMailItem, new ExPerformanceCounter[]
				{
					exPerformanceCounter42
				});
				list.Add(this.CommitLazyMailItem);
				ExPerformanceCounter exPerformanceCounter43 = new ExPerformanceCounter(base.CategoryName, "MailItem delete lazy/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter43);
				this.DeleteLazyMailItem = new ExPerformanceCounter(base.CategoryName, "MailItem delete lazy total", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.DeleteLazyMailItem, new ExPerformanceCounter[]
				{
					exPerformanceCounter43
				});
				list.Add(this.DeleteLazyMailItem);
				ExPerformanceCounter exPerformanceCounter44 = new ExPerformanceCounter(base.CategoryName, "MailItem writeMimeTo/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter44);
				this.WriteMimeTo = new ExPerformanceCounter(base.CategoryName, "MailItem writeMimeTo total", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.WriteMimeTo, new ExPerformanceCounter[]
				{
					exPerformanceCounter44
				});
				list.Add(this.WriteMimeTo);
				this.CurrentConnections = new ExPerformanceCounter(base.CategoryName, "Database connections current", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.CurrentConnections, new ExPerformanceCounter[0]);
				list.Add(this.CurrentConnections);
				this.RejectedConnections = new ExPerformanceCounter(base.CategoryName, "Database connections rejected total", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.RejectedConnections, new ExPerformanceCounter[0]);
				list.Add(this.RejectedConnections);
				this.CursorsOpened = new ExPerformanceCounter(base.CategoryName, "Cursors opened total", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.CursorsOpened, new ExPerformanceCounter[0]);
				list.Add(this.CursorsOpened);
				this.CursorsClosed = new ExPerformanceCounter(base.CategoryName, "Cursors closed total", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.CursorsClosed, new ExPerformanceCounter[0]);
				list.Add(this.CursorsClosed);
				long num = this.Seeks.RawValue;
				num += 1L;
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					foreach (ExPerformanceCounter exPerformanceCounter45 in list)
					{
						exPerformanceCounter45.Close();
					}
				}
			}
			this.counters = list.ToArray();
		}

		// Token: 0x06003F24 RID: 16164 RVA: 0x0011061C File Offset: 0x0010E81C
		internal DatabasePerfCountersInstance(string instanceName) : base(instanceName, "MSExchangeTransport Database")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.TransactionPendingCount = new ExPerformanceCounter(base.CategoryName, "Transaction Pending Count", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.TransactionPendingCount);
				ExPerformanceCounter exPerformanceCounter = new ExPerformanceCounter(base.CategoryName, "Transactions/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter);
				this.TransactionCount = new ExPerformanceCounter(base.CategoryName, "Transaction Count", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter
				});
				list.Add(this.TransactionCount);
				this.TransactionPending99PercentileDuration = new ExPerformanceCounter(base.CategoryName, "Transaction Pending 99 Percentile Duration", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.TransactionPending99PercentileDuration);
				this.TransactionAveragePendingDuration = new ExPerformanceCounter(base.CategoryName, "Transaction Pending Average Duration", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.TransactionAveragePendingDuration);
				this.TransactionAveragePendingDurationBase = new ExPerformanceCounter(base.CategoryName, "Transaction Pending Average Duration Base", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.TransactionAveragePendingDurationBase);
				this.TransactionSoftCommitPendingCount = new ExPerformanceCounter(base.CategoryName, "Transaction Commit Soft Pending", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.TransactionSoftCommitPendingCount);
				ExPerformanceCounter exPerformanceCounter2 = new ExPerformanceCounter(base.CategoryName, "Transaction Commit Soft/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter2);
				this.TransactionSoftCommitCount = new ExPerformanceCounter(base.CategoryName, "Transaction Commit Soft Count", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter2
				});
				list.Add(this.TransactionSoftCommitCount);
				this.TransactionSoftCommitAveragePendingDuration = new ExPerformanceCounter(base.CategoryName, "Transaction Commit Soft Average Pending Duration", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.TransactionSoftCommitAveragePendingDuration);
				this.TransactionSoftCommitAveragePendingDurationBase = new ExPerformanceCounter(base.CategoryName, "Transaction Commit Soft Average Pending Duration Base", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.TransactionSoftCommitAveragePendingDurationBase);
				this.TransactionHardCommitPendingCount = new ExPerformanceCounter(base.CategoryName, "Transaction Commit Hard Pending", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.TransactionHardCommitPendingCount);
				ExPerformanceCounter exPerformanceCounter3 = new ExPerformanceCounter(base.CategoryName, "Transaction Commit Hard/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter3);
				this.TransactionHardCommitCount = new ExPerformanceCounter(base.CategoryName, "Transaction Commit Hard Count", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter3
				});
				list.Add(this.TransactionHardCommitCount);
				ExPerformanceCounter exPerformanceCounter4 = new ExPerformanceCounter(base.CategoryName, "Transaction Aborts/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter4);
				this.TransactionAbortCount = new ExPerformanceCounter(base.CategoryName, "Transaction Abort Count", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter4
				});
				list.Add(this.TransactionAbortCount);
				this.TransactionHardCommitAveragePendingDuration = new ExPerformanceCounter(base.CategoryName, "Transaction Commit Hard Pending Average Duration", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.TransactionHardCommitAveragePendingDuration);
				this.TransactionHardCommitAveragePendingDurationBase = new ExPerformanceCounter(base.CategoryName, "Transaction Commit Hard Pending Average Duration Base", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.TransactionHardCommitAveragePendingDurationBase);
				this.TransactionAsyncCommitPendingCount = new ExPerformanceCounter(base.CategoryName, "Transaction Commit Async Pending", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.TransactionAsyncCommitPendingCount);
				ExPerformanceCounter exPerformanceCounter5 = new ExPerformanceCounter(base.CategoryName, "Transaction Commit Async/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter5);
				this.TransactionAsyncCommitCount = new ExPerformanceCounter(base.CategoryName, "Transaction Commit Async Count", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter5
				});
				list.Add(this.TransactionAsyncCommitCount);
				this.TransactionAsyncCommitAveragePendingDuration = new ExPerformanceCounter(base.CategoryName, "Transaction Commit Async Average Pending Duration", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.TransactionAsyncCommitAveragePendingDuration);
				this.TransactionAsyncCommitAveragePendingDurationBase = new ExPerformanceCounter(base.CategoryName, "Transaction Commit Async Pending Average Duration Base", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.TransactionAsyncCommitAveragePendingDurationBase);
				ExPerformanceCounter exPerformanceCounter6 = new ExPerformanceCounter(base.CategoryName, "Transaction Durable Callback Count/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter6);
				this.TransactionDurableCallbackCount = new ExPerformanceCounter(base.CategoryName, "Transaction Durable Callback Count", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter6
				});
				list.Add(this.TransactionDurableCallbackCount);
				this.MailItemCount = new ExPerformanceCounter(base.CategoryName, "MailItem Count", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MailItemCount);
				this.MailRecipientCount = new ExPerformanceCounter(base.CategoryName, "MailRecipient Count", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MailRecipientCount);
				this.MailRecipientActiveCount = new ExPerformanceCounter(base.CategoryName, "MailRecipient Active Count", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MailRecipientActiveCount);
				this.MailRecipientSafetyNetCount = new ExPerformanceCounter(base.CategoryName, "MailRecipient SafetyNet Count", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MailRecipientSafetyNetCount);
				this.MailRecipientSafetyNetMdbCount = new ExPerformanceCounter(base.CategoryName, "MailRecipient SafetyNet Mdb Count", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MailRecipientSafetyNetMdbCount);
				this.MailRecipientShadowSafetyNetCount = new ExPerformanceCounter(base.CategoryName, "MailRecipient Shadow SafetyNet Count", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MailRecipientShadowSafetyNetCount);
				this.TransportQueueDatabaseFileSize = new ExPerformanceCounter(base.CategoryName, "Transport Queue Database File Size (MB)", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.TransportQueueDatabaseFileSize);
				this.TransportQueueDatabaseInternalFreeSpace = new ExPerformanceCounter(base.CategoryName, "Transport Queue Database Internal Free Space (MB)", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.TransportQueueDatabaseInternalFreeSpace);
				this.GenerationCount = new ExPerformanceCounter(base.CategoryName, "Generation Count", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.GenerationCount);
				this.GenerationExpiredCount = new ExPerformanceCounter(base.CategoryName, "Generation Expired Count", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.GenerationExpiredCount);
				this.GenerationLastCleanupLatency = new ExPerformanceCounter(base.CategoryName, "Generation Last Cleanup Latency", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.GenerationLastCleanupLatency);
				this.BootloaderOutstandingItems = new ExPerformanceCounter(base.CategoryName, "Bootloader Outstanding Items", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.BootloaderOutstandingItems);
				ExPerformanceCounter exPerformanceCounter7 = new ExPerformanceCounter(base.CategoryName, "Bootloaded Items/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter7);
				this.BootloadedItemCount = new ExPerformanceCounter(base.CategoryName, "Bootloaded Item Count", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter7
				});
				list.Add(this.BootloadedItemCount);
				this.BootloadedItemAverageLatency = new ExPerformanceCounter(base.CategoryName, "Bootloaded Item Average Latency", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.BootloadedItemAverageLatency);
				this.BootloadedItemAverageLatencyBase = new ExPerformanceCounter(base.CategoryName, "Bootloaded Item Average Latency Base", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.BootloadedItemAverageLatencyBase);
				this.BootloadedRecentPoisonMessageCount = new ExPerformanceCounter(base.CategoryName, "Bootloaded Recent (within 24 hours) Poison Messages", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.BootloadedRecentPoisonMessageCount);
				ExPerformanceCounter exPerformanceCounter8 = new ExPerformanceCounter(base.CategoryName, "Replayed Items/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter8);
				this.ReplayedItemCount = new ExPerformanceCounter(base.CategoryName, "Replayed Item Count", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter8
				});
				list.Add(this.ReplayedItemCount);
				this.ReplayedItemAverageLatency = new ExPerformanceCounter(base.CategoryName, "Replayed Item Average Latency", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ReplayedItemAverageLatency);
				this.ReplayedItemAverageLatencyBase = new ExPerformanceCounter(base.CategoryName, "Average Replayed Item Average Latency Base", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ReplayedItemAverageLatencyBase);
				this.ReplayBookmarkAverageLatency = new ExPerformanceCounter(base.CategoryName, "Replay Bookmark Average Latency", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ReplayBookmarkAverageLatency);
				this.ReplayBookmarkAverageLatencyBase = new ExPerformanceCounter(base.CategoryName, "Replay Bookmark Average Latency Base", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ReplayBookmarkAverageLatencyBase);
				ExPerformanceCounter exPerformanceCounter9 = new ExPerformanceCounter(base.CategoryName, "DataRow seeks/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter9);
				this.Seeks = new ExPerformanceCounter(base.CategoryName, "DataRow seeks total", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter9
				});
				list.Add(this.Seeks);
				ExPerformanceCounter exPerformanceCounter10 = new ExPerformanceCounter(base.CategoryName, "DataRow seeks prefix/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter10);
				this.PrefixSeeks = new ExPerformanceCounter(base.CategoryName, "DataRow seeks prefix total", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter10
				});
				list.Add(this.PrefixSeeks);
				ExPerformanceCounter exPerformanceCounter11 = new ExPerformanceCounter(base.CategoryName, "DataRow loads/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter11);
				this.LoadFromCurrent = new ExPerformanceCounter(base.CategoryName, "DataRow loads total", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter11
				});
				list.Add(this.LoadFromCurrent);
				ExPerformanceCounter exPerformanceCounter12 = new ExPerformanceCounter(base.CategoryName, "DataRow updates/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter12);
				this.Update = new ExPerformanceCounter(base.CategoryName, "DataRow updates total", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter12
				});
				list.Add(this.Update);
				ExPerformanceCounter exPerformanceCounter13 = new ExPerformanceCounter(base.CategoryName, "DataRow new inserts/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter13);
				this.New = new ExPerformanceCounter(base.CategoryName, "DataRow new inserts total", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter13
				});
				list.Add(this.New);
				ExPerformanceCounter exPerformanceCounter14 = new ExPerformanceCounter(base.CategoryName, "DataRow clones/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter14);
				this.Clone = new ExPerformanceCounter(base.CategoryName, "DataRow clones total", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter14
				});
				list.Add(this.Clone);
				ExPerformanceCounter exPerformanceCounter15 = new ExPerformanceCounter(base.CategoryName, "DataRow moves/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter15);
				this.Move = new ExPerformanceCounter(base.CategoryName, "DataRow moves total", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter15
				});
				list.Add(this.Move);
				ExPerformanceCounter exPerformanceCounter16 = new ExPerformanceCounter(base.CategoryName, "DataRow deletes/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter16);
				this.Delete = new ExPerformanceCounter(base.CategoryName, "DataRow deletes total", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter16
				});
				list.Add(this.Delete);
				ExPerformanceCounter exPerformanceCounter17 = new ExPerformanceCounter(base.CategoryName, "DataRow minimize memory/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter17);
				this.MinimizeMemory = new ExPerformanceCounter(base.CategoryName, "DataRow minimize memory total", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter17
				});
				list.Add(this.MinimizeMemory);
				ExPerformanceCounter exPerformanceCounter18 = new ExPerformanceCounter(base.CategoryName, "Stream read/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter18);
				this.StreamReads = new ExPerformanceCounter(base.CategoryName, "Stream read total", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter18
				});
				list.Add(this.StreamReads);
				ExPerformanceCounter exPerformanceCounter19 = new ExPerformanceCounter(base.CategoryName, "Stream bytes read/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter19);
				this.StreamBytesRead = new ExPerformanceCounter(base.CategoryName, "Stream bytes read total", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter19
				});
				list.Add(this.StreamBytesRead);
				ExPerformanceCounter exPerformanceCounter20 = new ExPerformanceCounter(base.CategoryName, "Stream writes/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter20);
				this.StreamWrites = new ExPerformanceCounter(base.CategoryName, "Stream writes total", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter20
				});
				list.Add(this.StreamWrites);
				ExPerformanceCounter exPerformanceCounter21 = new ExPerformanceCounter(base.CategoryName, "Stream bytes written/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter21);
				this.StreamBytesWritten = new ExPerformanceCounter(base.CategoryName, "Stream bytes written total", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter21
				});
				list.Add(this.StreamBytesWritten);
				ExPerformanceCounter exPerformanceCounter22 = new ExPerformanceCounter(base.CategoryName, "Stream set length/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter22);
				this.StreamSetLen = new ExPerformanceCounter(base.CategoryName, "Stream set length count", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter22
				});
				list.Add(this.StreamSetLen);
				ExPerformanceCounter exPerformanceCounter23 = new ExPerformanceCounter(base.CategoryName, "Lazy bytes load requested/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter23);
				this.LazyBytesLoadRequested = new ExPerformanceCounter(base.CategoryName, "Lazy bytes load requested total", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter23
				});
				list.Add(this.LazyBytesLoadRequested);
				ExPerformanceCounter exPerformanceCounter24 = new ExPerformanceCounter(base.CategoryName, "Lazy bytes load performed/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter24);
				this.LazyBytesLoadPerformed = new ExPerformanceCounter(base.CategoryName, "Lazy bytes load performed total", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter24
				});
				list.Add(this.LazyBytesLoadPerformed);
				ExPerformanceCounter exPerformanceCounter25 = new ExPerformanceCounter(base.CategoryName, "Column cache load/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter25);
				this.ColumnsCacheLoads = new ExPerformanceCounter(base.CategoryName, "Column cache load total", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter25
				});
				list.Add(this.ColumnsCacheLoads);
				ExPerformanceCounter exPerformanceCounter26 = new ExPerformanceCounter(base.CategoryName, "Column cache loaded columns/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter26);
				this.ColumnsCacheColumnLoads = new ExPerformanceCounter(base.CategoryName, "Column cache loaded columns total", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter26
				});
				list.Add(this.ColumnsCacheColumnLoads);
				ExPerformanceCounter exPerformanceCounter27 = new ExPerformanceCounter(base.CategoryName, "Column cache loaded bytes/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter27);
				this.ColumnsCacheByteLoads = new ExPerformanceCounter(base.CategoryName, "Column cache loaded bytes total", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter27
				});
				list.Add(this.ColumnsCacheByteLoads);
				ExPerformanceCounter exPerformanceCounter28 = new ExPerformanceCounter(base.CategoryName, "Column cache save/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter28);
				this.ColumnsCacheSaves = new ExPerformanceCounter(base.CategoryName, "Column cache save total", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter28
				});
				list.Add(this.ColumnsCacheSaves);
				ExPerformanceCounter exPerformanceCounter29 = new ExPerformanceCounter(base.CategoryName, "Column cache saved columns/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter29);
				this.ColumnsCacheColumnSaves = new ExPerformanceCounter(base.CategoryName, "Column cache saved columns total", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter29
				});
				list.Add(this.ColumnsCacheColumnSaves);
				ExPerformanceCounter exPerformanceCounter30 = new ExPerformanceCounter(base.CategoryName, "Column cache saved bytes/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter30);
				this.ColumnsCacheByteSaves = new ExPerformanceCounter(base.CategoryName, "Column cache saved bytes total", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter30
				});
				list.Add(this.ColumnsCacheByteSaves);
				ExPerformanceCounter exPerformanceCounter31 = new ExPerformanceCounter(base.CategoryName, "Extended property writes/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter31);
				this.ExtendedPropertyWrites = new ExPerformanceCounter(base.CategoryName, "Extended property writes total", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter31
				});
				list.Add(this.ExtendedPropertyWrites);
				ExPerformanceCounter exPerformanceCounter32 = new ExPerformanceCounter(base.CategoryName, "Extended property bytes written/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter32);
				this.ExtendedPropertyBytesWritten = new ExPerformanceCounter(base.CategoryName, "Extended property bytes written total", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter32
				});
				list.Add(this.ExtendedPropertyBytesWritten);
				ExPerformanceCounter exPerformanceCounter33 = new ExPerformanceCounter(base.CategoryName, "Extended property reads/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter33);
				this.ExtendedPropertyReads = new ExPerformanceCounter(base.CategoryName, "Extended property reads total", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter33
				});
				list.Add(this.ExtendedPropertyReads);
				ExPerformanceCounter exPerformanceCounter34 = new ExPerformanceCounter(base.CategoryName, "Extended property bytes read/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter34);
				this.ExtendedPropertyBytesRead = new ExPerformanceCounter(base.CategoryName, "Extended property bytes read total", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter34
				});
				list.Add(this.ExtendedPropertyBytesRead);
				ExPerformanceCounter exPerformanceCounter35 = new ExPerformanceCounter(base.CategoryName, "MailItem new/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter35);
				this.NewMailItem = new ExPerformanceCounter(base.CategoryName, "MailItem new total", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter35
				});
				list.Add(this.NewMailItem);
				ExPerformanceCounter exPerformanceCounter36 = new ExPerformanceCounter(base.CategoryName, "MailItem clone create/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter36);
				this.NewCloneMailItem = new ExPerformanceCounter(base.CategoryName, "MailItem clone create total", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter36
				});
				list.Add(this.NewCloneMailItem);
				ExPerformanceCounter exPerformanceCounter37 = new ExPerformanceCounter(base.CategoryName, "MailItem dehydrate/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter37);
				this.DehydrateMailItem = new ExPerformanceCounter(base.CategoryName, "MailItem dehydrate total", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter37
				});
				list.Add(this.DehydrateMailItem);
				ExPerformanceCounter exPerformanceCounter38 = new ExPerformanceCounter(base.CategoryName, "MailItem load/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter38);
				this.LoadMailItem = new ExPerformanceCounter(base.CategoryName, "MailItem load total", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter38
				});
				list.Add(this.LoadMailItem);
				ExPerformanceCounter exPerformanceCounter39 = new ExPerformanceCounter(base.CategoryName, "MailItem commit immediate/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter39);
				this.CommitImmediateMailItem = new ExPerformanceCounter(base.CategoryName, "MailItem commit immediate total", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter39
				});
				list.Add(this.CommitImmediateMailItem);
				ExPerformanceCounter exPerformanceCounter40 = new ExPerformanceCounter(base.CategoryName, "MailItem materialize/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter40);
				this.MaterializeMailItem = new ExPerformanceCounter(base.CategoryName, "MailItem materialize", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter40
				});
				list.Add(this.MaterializeMailItem);
				ExPerformanceCounter exPerformanceCounter41 = new ExPerformanceCounter(base.CategoryName, "MailItem begin commit/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter41);
				this.BeginCommitMailItem = new ExPerformanceCounter(base.CategoryName, "MailItem begin commit total", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter41
				});
				list.Add(this.BeginCommitMailItem);
				ExPerformanceCounter exPerformanceCounter42 = new ExPerformanceCounter(base.CategoryName, "MailItem commit lazy/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter42);
				this.CommitLazyMailItem = new ExPerformanceCounter(base.CategoryName, "MailItem commit lazy total", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter42
				});
				list.Add(this.CommitLazyMailItem);
				ExPerformanceCounter exPerformanceCounter43 = new ExPerformanceCounter(base.CategoryName, "MailItem delete lazy/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter43);
				this.DeleteLazyMailItem = new ExPerformanceCounter(base.CategoryName, "MailItem delete lazy total", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter43
				});
				list.Add(this.DeleteLazyMailItem);
				ExPerformanceCounter exPerformanceCounter44 = new ExPerformanceCounter(base.CategoryName, "MailItem writeMimeTo/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter44);
				this.WriteMimeTo = new ExPerformanceCounter(base.CategoryName, "MailItem writeMimeTo total", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter44
				});
				list.Add(this.WriteMimeTo);
				this.CurrentConnections = new ExPerformanceCounter(base.CategoryName, "Database connections current", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.CurrentConnections);
				this.RejectedConnections = new ExPerformanceCounter(base.CategoryName, "Database connections rejected total", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.RejectedConnections);
				this.CursorsOpened = new ExPerformanceCounter(base.CategoryName, "Cursors opened total", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.CursorsOpened);
				this.CursorsClosed = new ExPerformanceCounter(base.CategoryName, "Cursors closed total", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.CursorsClosed);
				long num = this.Seeks.RawValue;
				num += 1L;
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					foreach (ExPerformanceCounter exPerformanceCounter45 in list)
					{
						exPerformanceCounter45.Close();
					}
				}
			}
			this.counters = list.ToArray();
		}

		// Token: 0x06003F25 RID: 16165 RVA: 0x00111C74 File Offset: 0x0010FE74
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

		// Token: 0x04002329 RID: 9001
		public readonly ExPerformanceCounter Seeks;

		// Token: 0x0400232A RID: 9002
		public readonly ExPerformanceCounter PrefixSeeks;

		// Token: 0x0400232B RID: 9003
		public readonly ExPerformanceCounter LoadFromCurrent;

		// Token: 0x0400232C RID: 9004
		public readonly ExPerformanceCounter Update;

		// Token: 0x0400232D RID: 9005
		public readonly ExPerformanceCounter New;

		// Token: 0x0400232E RID: 9006
		public readonly ExPerformanceCounter Clone;

		// Token: 0x0400232F RID: 9007
		public readonly ExPerformanceCounter Move;

		// Token: 0x04002330 RID: 9008
		public readonly ExPerformanceCounter Delete;

		// Token: 0x04002331 RID: 9009
		public readonly ExPerformanceCounter MinimizeMemory;

		// Token: 0x04002332 RID: 9010
		public readonly ExPerformanceCounter StreamReads;

		// Token: 0x04002333 RID: 9011
		public readonly ExPerformanceCounter StreamBytesRead;

		// Token: 0x04002334 RID: 9012
		public readonly ExPerformanceCounter StreamWrites;

		// Token: 0x04002335 RID: 9013
		public readonly ExPerformanceCounter StreamBytesWritten;

		// Token: 0x04002336 RID: 9014
		public readonly ExPerformanceCounter StreamSetLen;

		// Token: 0x04002337 RID: 9015
		public readonly ExPerformanceCounter LazyBytesLoadRequested;

		// Token: 0x04002338 RID: 9016
		public readonly ExPerformanceCounter LazyBytesLoadPerformed;

		// Token: 0x04002339 RID: 9017
		public readonly ExPerformanceCounter ColumnsCacheLoads;

		// Token: 0x0400233A RID: 9018
		public readonly ExPerformanceCounter ColumnsCacheColumnLoads;

		// Token: 0x0400233B RID: 9019
		public readonly ExPerformanceCounter ColumnsCacheByteLoads;

		// Token: 0x0400233C RID: 9020
		public readonly ExPerformanceCounter ColumnsCacheSaves;

		// Token: 0x0400233D RID: 9021
		public readonly ExPerformanceCounter ColumnsCacheColumnSaves;

		// Token: 0x0400233E RID: 9022
		public readonly ExPerformanceCounter ColumnsCacheByteSaves;

		// Token: 0x0400233F RID: 9023
		public readonly ExPerformanceCounter ExtendedPropertyWrites;

		// Token: 0x04002340 RID: 9024
		public readonly ExPerformanceCounter ExtendedPropertyBytesWritten;

		// Token: 0x04002341 RID: 9025
		public readonly ExPerformanceCounter ExtendedPropertyReads;

		// Token: 0x04002342 RID: 9026
		public readonly ExPerformanceCounter ExtendedPropertyBytesRead;

		// Token: 0x04002343 RID: 9027
		public readonly ExPerformanceCounter TransactionPendingCount;

		// Token: 0x04002344 RID: 9028
		public readonly ExPerformanceCounter TransactionCount;

		// Token: 0x04002345 RID: 9029
		public readonly ExPerformanceCounter TransactionPending99PercentileDuration;

		// Token: 0x04002346 RID: 9030
		public readonly ExPerformanceCounter TransactionAveragePendingDuration;

		// Token: 0x04002347 RID: 9031
		public readonly ExPerformanceCounter TransactionAveragePendingDurationBase;

		// Token: 0x04002348 RID: 9032
		public readonly ExPerformanceCounter TransactionSoftCommitPendingCount;

		// Token: 0x04002349 RID: 9033
		public readonly ExPerformanceCounter TransactionSoftCommitCount;

		// Token: 0x0400234A RID: 9034
		public readonly ExPerformanceCounter TransactionSoftCommitAveragePendingDuration;

		// Token: 0x0400234B RID: 9035
		public readonly ExPerformanceCounter TransactionSoftCommitAveragePendingDurationBase;

		// Token: 0x0400234C RID: 9036
		public readonly ExPerformanceCounter TransactionHardCommitPendingCount;

		// Token: 0x0400234D RID: 9037
		public readonly ExPerformanceCounter TransactionHardCommitCount;

		// Token: 0x0400234E RID: 9038
		public readonly ExPerformanceCounter TransactionAbortCount;

		// Token: 0x0400234F RID: 9039
		public readonly ExPerformanceCounter TransactionHardCommitAveragePendingDuration;

		// Token: 0x04002350 RID: 9040
		public readonly ExPerformanceCounter TransactionHardCommitAveragePendingDurationBase;

		// Token: 0x04002351 RID: 9041
		public readonly ExPerformanceCounter TransactionAsyncCommitPendingCount;

		// Token: 0x04002352 RID: 9042
		public readonly ExPerformanceCounter TransactionAsyncCommitCount;

		// Token: 0x04002353 RID: 9043
		public readonly ExPerformanceCounter TransactionAsyncCommitAveragePendingDuration;

		// Token: 0x04002354 RID: 9044
		public readonly ExPerformanceCounter TransactionAsyncCommitAveragePendingDurationBase;

		// Token: 0x04002355 RID: 9045
		public readonly ExPerformanceCounter TransactionDurableCallbackCount;

		// Token: 0x04002356 RID: 9046
		public readonly ExPerformanceCounter NewMailItem;

		// Token: 0x04002357 RID: 9047
		public readonly ExPerformanceCounter NewCloneMailItem;

		// Token: 0x04002358 RID: 9048
		public readonly ExPerformanceCounter DehydrateMailItem;

		// Token: 0x04002359 RID: 9049
		public readonly ExPerformanceCounter LoadMailItem;

		// Token: 0x0400235A RID: 9050
		public readonly ExPerformanceCounter CommitImmediateMailItem;

		// Token: 0x0400235B RID: 9051
		public readonly ExPerformanceCounter MaterializeMailItem;

		// Token: 0x0400235C RID: 9052
		public readonly ExPerformanceCounter BeginCommitMailItem;

		// Token: 0x0400235D RID: 9053
		public readonly ExPerformanceCounter CommitLazyMailItem;

		// Token: 0x0400235E RID: 9054
		public readonly ExPerformanceCounter DeleteLazyMailItem;

		// Token: 0x0400235F RID: 9055
		public readonly ExPerformanceCounter WriteMimeTo;

		// Token: 0x04002360 RID: 9056
		public readonly ExPerformanceCounter MailItemCount;

		// Token: 0x04002361 RID: 9057
		public readonly ExPerformanceCounter MailRecipientCount;

		// Token: 0x04002362 RID: 9058
		public readonly ExPerformanceCounter MailRecipientActiveCount;

		// Token: 0x04002363 RID: 9059
		public readonly ExPerformanceCounter MailRecipientSafetyNetCount;

		// Token: 0x04002364 RID: 9060
		public readonly ExPerformanceCounter MailRecipientSafetyNetMdbCount;

		// Token: 0x04002365 RID: 9061
		public readonly ExPerformanceCounter MailRecipientShadowSafetyNetCount;

		// Token: 0x04002366 RID: 9062
		public readonly ExPerformanceCounter TransportQueueDatabaseFileSize;

		// Token: 0x04002367 RID: 9063
		public readonly ExPerformanceCounter TransportQueueDatabaseInternalFreeSpace;

		// Token: 0x04002368 RID: 9064
		public readonly ExPerformanceCounter GenerationCount;

		// Token: 0x04002369 RID: 9065
		public readonly ExPerformanceCounter GenerationExpiredCount;

		// Token: 0x0400236A RID: 9066
		public readonly ExPerformanceCounter GenerationLastCleanupLatency;

		// Token: 0x0400236B RID: 9067
		public readonly ExPerformanceCounter BootloaderOutstandingItems;

		// Token: 0x0400236C RID: 9068
		public readonly ExPerformanceCounter BootloadedItemCount;

		// Token: 0x0400236D RID: 9069
		public readonly ExPerformanceCounter BootloadedItemAverageLatency;

		// Token: 0x0400236E RID: 9070
		public readonly ExPerformanceCounter BootloadedItemAverageLatencyBase;

		// Token: 0x0400236F RID: 9071
		public readonly ExPerformanceCounter BootloadedRecentPoisonMessageCount;

		// Token: 0x04002370 RID: 9072
		public readonly ExPerformanceCounter ReplayedItemCount;

		// Token: 0x04002371 RID: 9073
		public readonly ExPerformanceCounter ReplayedItemAverageLatency;

		// Token: 0x04002372 RID: 9074
		public readonly ExPerformanceCounter ReplayedItemAverageLatencyBase;

		// Token: 0x04002373 RID: 9075
		public readonly ExPerformanceCounter ReplayBookmarkAverageLatency;

		// Token: 0x04002374 RID: 9076
		public readonly ExPerformanceCounter ReplayBookmarkAverageLatencyBase;

		// Token: 0x04002375 RID: 9077
		public readonly ExPerformanceCounter CurrentConnections;

		// Token: 0x04002376 RID: 9078
		public readonly ExPerformanceCounter RejectedConnections;

		// Token: 0x04002377 RID: 9079
		public readonly ExPerformanceCounter CursorsOpened;

		// Token: 0x04002378 RID: 9080
		public readonly ExPerformanceCounter CursorsClosed;
	}
}
