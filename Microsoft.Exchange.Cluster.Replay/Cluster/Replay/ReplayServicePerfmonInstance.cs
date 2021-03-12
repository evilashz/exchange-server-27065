using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200038F RID: 911
	internal sealed class ReplayServicePerfmonInstance : PerformanceCounterInstance
	{
		// Token: 0x06002474 RID: 9332 RVA: 0x000AB254 File Offset: 0x000A9454
		internal ReplayServicePerfmonInstance(string instanceName, ReplayServicePerfmonInstance autoUpdateTotalInstance) : base(instanceName, "MSExchange Replication")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.CopyNotificationGenerationNumber = new ExPerformanceCounter(base.CategoryName, "CopyNotificationGenerationNumber", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.CopyNotificationGenerationNumber, new ExPerformanceCounter[0]);
				list.Add(this.CopyNotificationGenerationNumber);
				this.CopyGenerationNumber = new ExPerformanceCounter(base.CategoryName, "CopyGenerationNumber", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.CopyGenerationNumber, new ExPerformanceCounter[0]);
				list.Add(this.CopyGenerationNumber);
				this.LogCopyThruput = new ExPerformanceCounter(base.CategoryName, "Log Copy KB/Sec", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.LogCopyThruput, new ExPerformanceCounter[0]);
				list.Add(this.LogCopyThruput);
				this.AvgLogCopyNetReadLatency = new ExPerformanceCounter(base.CategoryName, "Avg. Network sec/Read", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.AvgLogCopyNetReadLatency, new ExPerformanceCounter[0]);
				list.Add(this.AvgLogCopyNetReadLatency);
				this.AvgLogCopyNetReadLatencyBase = new ExPerformanceCounter(base.CategoryName, "Base for LogCopyNetReadLatency", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.AvgLogCopyNetReadLatencyBase, new ExPerformanceCounter[0]);
				list.Add(this.AvgLogCopyNetReadLatencyBase);
				this.InspectorGenerationNumber = new ExPerformanceCounter(base.CategoryName, "InspectorGenerationNumber", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.InspectorGenerationNumber, new ExPerformanceCounter[0]);
				list.Add(this.InspectorGenerationNumber);
				this.ReplayNotificationGenerationNumber = new ExPerformanceCounter(base.CategoryName, "ReplayNotificationGenerationNumber", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.ReplayNotificationGenerationNumber, new ExPerformanceCounter[0]);
				list.Add(this.ReplayNotificationGenerationNumber);
				this.ReplayGenerationNumber = new ExPerformanceCounter(base.CategoryName, "ReplayGenerationNumber", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.ReplayGenerationNumber, new ExPerformanceCounter[0]);
				list.Add(this.ReplayGenerationNumber);
				this.ReplayQueueLength = new ExPerformanceCounter(base.CategoryName, "ReplayQueueLength", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.ReplayQueueLength, new ExPerformanceCounter[0]);
				list.Add(this.ReplayQueueLength);
				this.CopyQueueLength = new ExPerformanceCounter(base.CategoryName, "CopyQueueLength", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.CopyQueueLength, new ExPerformanceCounter[0]);
				list.Add(this.CopyQueueLength);
				this.RawCopyQueueLength = new ExPerformanceCounter(base.CategoryName, "CopyQueueLength excluding inspection", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.RawCopyQueueLength, new ExPerformanceCounter[0]);
				list.Add(this.RawCopyQueueLength);
				this.Failed = new ExPerformanceCounter(base.CategoryName, "Failed", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.Failed, new ExPerformanceCounter[0]);
				list.Add(this.Failed);
				this.Initializing = new ExPerformanceCounter(base.CategoryName, "Initializing", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.Initializing, new ExPerformanceCounter[0]);
				list.Add(this.Initializing);
				this.FailedSuspended = new ExPerformanceCounter(base.CategoryName, "FailedSuspended", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.FailedSuspended, new ExPerformanceCounter[0]);
				list.Add(this.FailedSuspended);
				this.Resynchronizing = new ExPerformanceCounter(base.CategoryName, "Resynchronizing", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.Resynchronizing, new ExPerformanceCounter[0]);
				list.Add(this.Resynchronizing);
				this.Disconnected = new ExPerformanceCounter(base.CategoryName, "Disconnected", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.Disconnected, new ExPerformanceCounter[0]);
				list.Add(this.Disconnected);
				this.SinglePageRestore = new ExPerformanceCounter(base.CategoryName, "SinglePageRestore", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.SinglePageRestore, new ExPerformanceCounter[0]);
				list.Add(this.SinglePageRestore);
				this.ActivationSuspended = new ExPerformanceCounter(base.CategoryName, "ActivationSuspended", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.ActivationSuspended, new ExPerformanceCounter[0]);
				list.Add(this.ActivationSuspended);
				this.Suspended = new ExPerformanceCounter(base.CategoryName, "Suspended", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.Suspended, new ExPerformanceCounter[0]);
				list.Add(this.Suspended);
				this.SuspendedAndNotSeeding = new ExPerformanceCounter(base.CategoryName, "Suspended and not Seeding", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.SuspendedAndNotSeeding, new ExPerformanceCounter[0]);
				list.Add(this.SuspendedAndNotSeeding);
				this.Seeding = new ExPerformanceCounter(base.CategoryName, "Seeding", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.Seeding, new ExPerformanceCounter[0]);
				list.Add(this.Seeding);
				this.ReplayLagDisabled = new ExPerformanceCounter(base.CategoryName, "ReplayLagDisabled", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.ReplayLagDisabled, new ExPerformanceCounter[0]);
				list.Add(this.ReplayLagDisabled);
				this.ReplayLagPercentage = new ExPerformanceCounter(base.CategoryName, "ReplayLag Percent of Configured Lag", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.ReplayLagPercentage, new ExPerformanceCounter[0]);
				list.Add(this.ReplayLagPercentage);
				this.CompressionEnabled = new ExPerformanceCounter(base.CategoryName, "CompressionEnabled", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.CompressionEnabled, new ExPerformanceCounter[0]);
				list.Add(this.CompressionEnabled);
				this.EncryptionEnabled = new ExPerformanceCounter(base.CategoryName, "EncryptionEnabled", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.EncryptionEnabled, new ExPerformanceCounter[0]);
				list.Add(this.EncryptionEnabled);
				this.TruncatedGenerationNumber = new ExPerformanceCounter(base.CategoryName, "TruncatedGenerationNumber", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.TruncatedGenerationNumber, new ExPerformanceCounter[0]);
				list.Add(this.TruncatedGenerationNumber);
				this.IncReseedDBPagesReadNumber = new ExPerformanceCounter(base.CategoryName, "IncReseedDBPagesReadNumber", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.IncReseedDBPagesReadNumber, new ExPerformanceCounter[0]);
				list.Add(this.IncReseedDBPagesReadNumber);
				this.IncReseedLogCopiedNumber = new ExPerformanceCounter(base.CategoryName, "IncReseedLogCopiedNumber", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.IncReseedLogCopiedNumber, new ExPerformanceCounter[0]);
				list.Add(this.IncReseedLogCopiedNumber);
				this.CopyNotificationGenerationsPerSecond = new ExPerformanceCounter(base.CategoryName, "Log Generation Rate on Source (generations/sec)", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.CopyNotificationGenerationsPerSecond, new ExPerformanceCounter[0]);
				list.Add(this.CopyNotificationGenerationsPerSecond);
				this.InspectorGenerationsPerSecond = new ExPerformanceCounter(base.CategoryName, "Log Inspection Rate (generations/sec)", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.InspectorGenerationsPerSecond, new ExPerformanceCounter[0]);
				list.Add(this.InspectorGenerationsPerSecond);
				this.CopyQueueNotKeepingUp = new ExPerformanceCounter(base.CategoryName, "Log Copying is Not Keeping Up", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.CopyQueueNotKeepingUp, new ExPerformanceCounter[0]);
				list.Add(this.CopyQueueNotKeepingUp);
				this.ReplayGenerationsPerSecond = new ExPerformanceCounter(base.CategoryName, "Log Replay Rate (generations/sec)", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.ReplayGenerationsPerSecond, new ExPerformanceCounter[0]);
				list.Add(this.ReplayGenerationsPerSecond);
				this.ReplayQueueNotKeepingUp = new ExPerformanceCounter(base.CategoryName, "Log Replay is Not Keeping Up", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.ReplayQueueNotKeepingUp, new ExPerformanceCounter[0]);
				list.Add(this.ReplayQueueNotKeepingUp);
				this.GranularReplication = new ExPerformanceCounter(base.CategoryName, "Continuous replication - block mode Active", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.GranularReplication, new ExPerformanceCounter[0]);
				list.Add(this.GranularReplication);
				this.TotalGranularBytesReceived = new ExPerformanceCounter(base.CategoryName, "Total Bytes Received Block Mode", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.TotalGranularBytesReceived, new ExPerformanceCounter[0]);
				list.Add(this.TotalGranularBytesReceived);
				this.AverageGranularBytesPerLog = new ExPerformanceCounter(base.CategoryName, "Average Bytes Per Log Generation - Block Mode", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.AverageGranularBytesPerLog, new ExPerformanceCounter[0]);
				list.Add(this.AverageGranularBytesPerLog);
				this.AvgBlockModeConsumerWriteTime = new ExPerformanceCounter(base.CategoryName, "Avg. Block Mode Disk sec/Write", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.AvgBlockModeConsumerWriteTime, new ExPerformanceCounter[0]);
				list.Add(this.AvgBlockModeConsumerWriteTime);
				this.AvgBlockModeConsumerWriteTimeBase = new ExPerformanceCounter(base.CategoryName, "AvgBlockModeConsumerWriteTimeBase", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.AvgBlockModeConsumerWriteTimeBase, new ExPerformanceCounter[0]);
				list.Add(this.AvgBlockModeConsumerWriteTimeBase);
				this.AvgFileModeWriteTime = new ExPerformanceCounter(base.CategoryName, "Avg. File Mode Disk sec/Log Generation", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.AvgFileModeWriteTime, new ExPerformanceCounter[0]);
				list.Add(this.AvgFileModeWriteTime);
				this.AvgFileModeWriteTimeBase = new ExPerformanceCounter(base.CategoryName, "AvgFileModeWriteTimeBase", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.AvgFileModeWriteTimeBase, new ExPerformanceCounter[0]);
				list.Add(this.AvgFileModeWriteTimeBase);
				this.PassiveSeedingSource = new ExPerformanceCounter(base.CategoryName, "SeedingSource", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PassiveSeedingSource, new ExPerformanceCounter[0]);
				list.Add(this.PassiveSeedingSource);
				this.GetCopyStatusInstanceCalls = new ExPerformanceCounter(base.CategoryName, "GetCopyStatus Server-Side Calls", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.GetCopyStatusInstanceCalls, new ExPerformanceCounter[0]);
				list.Add(this.GetCopyStatusInstanceCalls);
				this.GetCopyStatusInstanceCallsPerSec = new ExPerformanceCounter(base.CategoryName, "GetCopyStatus Server-Side Calls/sec", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.GetCopyStatusInstanceCallsPerSec, new ExPerformanceCounter[0]);
				list.Add(this.GetCopyStatusInstanceCallsPerSec);
				long num = this.CopyNotificationGenerationNumber.RawValue;
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

		// Token: 0x06002475 RID: 9333 RVA: 0x000ABBE8 File Offset: 0x000A9DE8
		internal ReplayServicePerfmonInstance(string instanceName) : base(instanceName, "MSExchange Replication")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.CopyNotificationGenerationNumber = new ExPerformanceCounter(base.CategoryName, "CopyNotificationGenerationNumber", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.CopyNotificationGenerationNumber);
				this.CopyGenerationNumber = new ExPerformanceCounter(base.CategoryName, "CopyGenerationNumber", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.CopyGenerationNumber);
				this.LogCopyThruput = new ExPerformanceCounter(base.CategoryName, "Log Copy KB/Sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.LogCopyThruput);
				this.AvgLogCopyNetReadLatency = new ExPerformanceCounter(base.CategoryName, "Avg. Network sec/Read", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AvgLogCopyNetReadLatency);
				this.AvgLogCopyNetReadLatencyBase = new ExPerformanceCounter(base.CategoryName, "Base for LogCopyNetReadLatency", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AvgLogCopyNetReadLatencyBase);
				this.InspectorGenerationNumber = new ExPerformanceCounter(base.CategoryName, "InspectorGenerationNumber", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.InspectorGenerationNumber);
				this.ReplayNotificationGenerationNumber = new ExPerformanceCounter(base.CategoryName, "ReplayNotificationGenerationNumber", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.ReplayNotificationGenerationNumber);
				this.ReplayGenerationNumber = new ExPerformanceCounter(base.CategoryName, "ReplayGenerationNumber", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.ReplayGenerationNumber);
				this.ReplayQueueLength = new ExPerformanceCounter(base.CategoryName, "ReplayQueueLength", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.ReplayQueueLength);
				this.CopyQueueLength = new ExPerformanceCounter(base.CategoryName, "CopyQueueLength", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.CopyQueueLength);
				this.RawCopyQueueLength = new ExPerformanceCounter(base.CategoryName, "CopyQueueLength excluding inspection", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.RawCopyQueueLength);
				this.Failed = new ExPerformanceCounter(base.CategoryName, "Failed", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.Failed);
				this.Initializing = new ExPerformanceCounter(base.CategoryName, "Initializing", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.Initializing);
				this.FailedSuspended = new ExPerformanceCounter(base.CategoryName, "FailedSuspended", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.FailedSuspended);
				this.Resynchronizing = new ExPerformanceCounter(base.CategoryName, "Resynchronizing", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.Resynchronizing);
				this.Disconnected = new ExPerformanceCounter(base.CategoryName, "Disconnected", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.Disconnected);
				this.SinglePageRestore = new ExPerformanceCounter(base.CategoryName, "SinglePageRestore", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.SinglePageRestore);
				this.ActivationSuspended = new ExPerformanceCounter(base.CategoryName, "ActivationSuspended", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.ActivationSuspended);
				this.Suspended = new ExPerformanceCounter(base.CategoryName, "Suspended", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.Suspended);
				this.SuspendedAndNotSeeding = new ExPerformanceCounter(base.CategoryName, "Suspended and not Seeding", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.SuspendedAndNotSeeding);
				this.Seeding = new ExPerformanceCounter(base.CategoryName, "Seeding", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.Seeding);
				this.ReplayLagDisabled = new ExPerformanceCounter(base.CategoryName, "ReplayLagDisabled", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.ReplayLagDisabled);
				this.ReplayLagPercentage = new ExPerformanceCounter(base.CategoryName, "ReplayLag Percent of Configured Lag", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.ReplayLagPercentage);
				this.CompressionEnabled = new ExPerformanceCounter(base.CategoryName, "CompressionEnabled", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.CompressionEnabled);
				this.EncryptionEnabled = new ExPerformanceCounter(base.CategoryName, "EncryptionEnabled", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.EncryptionEnabled);
				this.TruncatedGenerationNumber = new ExPerformanceCounter(base.CategoryName, "TruncatedGenerationNumber", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TruncatedGenerationNumber);
				this.IncReseedDBPagesReadNumber = new ExPerformanceCounter(base.CategoryName, "IncReseedDBPagesReadNumber", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.IncReseedDBPagesReadNumber);
				this.IncReseedLogCopiedNumber = new ExPerformanceCounter(base.CategoryName, "IncReseedLogCopiedNumber", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.IncReseedLogCopiedNumber);
				this.CopyNotificationGenerationsPerSecond = new ExPerformanceCounter(base.CategoryName, "Log Generation Rate on Source (generations/sec)", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.CopyNotificationGenerationsPerSecond);
				this.InspectorGenerationsPerSecond = new ExPerformanceCounter(base.CategoryName, "Log Inspection Rate (generations/sec)", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.InspectorGenerationsPerSecond);
				this.CopyQueueNotKeepingUp = new ExPerformanceCounter(base.CategoryName, "Log Copying is Not Keeping Up", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.CopyQueueNotKeepingUp);
				this.ReplayGenerationsPerSecond = new ExPerformanceCounter(base.CategoryName, "Log Replay Rate (generations/sec)", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.ReplayGenerationsPerSecond);
				this.ReplayQueueNotKeepingUp = new ExPerformanceCounter(base.CategoryName, "Log Replay is Not Keeping Up", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.ReplayQueueNotKeepingUp);
				this.GranularReplication = new ExPerformanceCounter(base.CategoryName, "Continuous replication - block mode Active", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.GranularReplication);
				this.TotalGranularBytesReceived = new ExPerformanceCounter(base.CategoryName, "Total Bytes Received Block Mode", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TotalGranularBytesReceived);
				this.AverageGranularBytesPerLog = new ExPerformanceCounter(base.CategoryName, "Average Bytes Per Log Generation - Block Mode", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageGranularBytesPerLog);
				this.AvgBlockModeConsumerWriteTime = new ExPerformanceCounter(base.CategoryName, "Avg. Block Mode Disk sec/Write", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AvgBlockModeConsumerWriteTime);
				this.AvgBlockModeConsumerWriteTimeBase = new ExPerformanceCounter(base.CategoryName, "AvgBlockModeConsumerWriteTimeBase", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AvgBlockModeConsumerWriteTimeBase);
				this.AvgFileModeWriteTime = new ExPerformanceCounter(base.CategoryName, "Avg. File Mode Disk sec/Log Generation", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AvgFileModeWriteTime);
				this.AvgFileModeWriteTimeBase = new ExPerformanceCounter(base.CategoryName, "AvgFileModeWriteTimeBase", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AvgFileModeWriteTimeBase);
				this.PassiveSeedingSource = new ExPerformanceCounter(base.CategoryName, "SeedingSource", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PassiveSeedingSource);
				this.GetCopyStatusInstanceCalls = new ExPerformanceCounter(base.CategoryName, "GetCopyStatus Server-Side Calls", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.GetCopyStatusInstanceCalls);
				this.GetCopyStatusInstanceCallsPerSec = new ExPerformanceCounter(base.CategoryName, "GetCopyStatus Server-Side Calls/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.GetCopyStatusInstanceCallsPerSec);
				long num = this.CopyNotificationGenerationNumber.RawValue;
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

		// Token: 0x06002476 RID: 9334 RVA: 0x000AC3A4 File Offset: 0x000AA5A4
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

		// Token: 0x0400109C RID: 4252
		public readonly ExPerformanceCounter CopyNotificationGenerationNumber;

		// Token: 0x0400109D RID: 4253
		public readonly ExPerformanceCounter CopyGenerationNumber;

		// Token: 0x0400109E RID: 4254
		public readonly ExPerformanceCounter LogCopyThruput;

		// Token: 0x0400109F RID: 4255
		public readonly ExPerformanceCounter AvgLogCopyNetReadLatency;

		// Token: 0x040010A0 RID: 4256
		public readonly ExPerformanceCounter AvgLogCopyNetReadLatencyBase;

		// Token: 0x040010A1 RID: 4257
		public readonly ExPerformanceCounter InspectorGenerationNumber;

		// Token: 0x040010A2 RID: 4258
		public readonly ExPerformanceCounter ReplayNotificationGenerationNumber;

		// Token: 0x040010A3 RID: 4259
		public readonly ExPerformanceCounter ReplayGenerationNumber;

		// Token: 0x040010A4 RID: 4260
		public readonly ExPerformanceCounter ReplayQueueLength;

		// Token: 0x040010A5 RID: 4261
		public readonly ExPerformanceCounter CopyQueueLength;

		// Token: 0x040010A6 RID: 4262
		public readonly ExPerformanceCounter RawCopyQueueLength;

		// Token: 0x040010A7 RID: 4263
		public readonly ExPerformanceCounter Failed;

		// Token: 0x040010A8 RID: 4264
		public readonly ExPerformanceCounter Initializing;

		// Token: 0x040010A9 RID: 4265
		public readonly ExPerformanceCounter FailedSuspended;

		// Token: 0x040010AA RID: 4266
		public readonly ExPerformanceCounter Resynchronizing;

		// Token: 0x040010AB RID: 4267
		public readonly ExPerformanceCounter Disconnected;

		// Token: 0x040010AC RID: 4268
		public readonly ExPerformanceCounter SinglePageRestore;

		// Token: 0x040010AD RID: 4269
		public readonly ExPerformanceCounter ActivationSuspended;

		// Token: 0x040010AE RID: 4270
		public readonly ExPerformanceCounter Suspended;

		// Token: 0x040010AF RID: 4271
		public readonly ExPerformanceCounter SuspendedAndNotSeeding;

		// Token: 0x040010B0 RID: 4272
		public readonly ExPerformanceCounter Seeding;

		// Token: 0x040010B1 RID: 4273
		public readonly ExPerformanceCounter ReplayLagDisabled;

		// Token: 0x040010B2 RID: 4274
		public readonly ExPerformanceCounter ReplayLagPercentage;

		// Token: 0x040010B3 RID: 4275
		public readonly ExPerformanceCounter CompressionEnabled;

		// Token: 0x040010B4 RID: 4276
		public readonly ExPerformanceCounter EncryptionEnabled;

		// Token: 0x040010B5 RID: 4277
		public readonly ExPerformanceCounter TruncatedGenerationNumber;

		// Token: 0x040010B6 RID: 4278
		public readonly ExPerformanceCounter IncReseedDBPagesReadNumber;

		// Token: 0x040010B7 RID: 4279
		public readonly ExPerformanceCounter IncReseedLogCopiedNumber;

		// Token: 0x040010B8 RID: 4280
		public readonly ExPerformanceCounter CopyNotificationGenerationsPerSecond;

		// Token: 0x040010B9 RID: 4281
		public readonly ExPerformanceCounter InspectorGenerationsPerSecond;

		// Token: 0x040010BA RID: 4282
		public readonly ExPerformanceCounter CopyQueueNotKeepingUp;

		// Token: 0x040010BB RID: 4283
		public readonly ExPerformanceCounter ReplayGenerationsPerSecond;

		// Token: 0x040010BC RID: 4284
		public readonly ExPerformanceCounter ReplayQueueNotKeepingUp;

		// Token: 0x040010BD RID: 4285
		public readonly ExPerformanceCounter GranularReplication;

		// Token: 0x040010BE RID: 4286
		public readonly ExPerformanceCounter TotalGranularBytesReceived;

		// Token: 0x040010BF RID: 4287
		public readonly ExPerformanceCounter AverageGranularBytesPerLog;

		// Token: 0x040010C0 RID: 4288
		public readonly ExPerformanceCounter AvgBlockModeConsumerWriteTime;

		// Token: 0x040010C1 RID: 4289
		public readonly ExPerformanceCounter AvgBlockModeConsumerWriteTimeBase;

		// Token: 0x040010C2 RID: 4290
		public readonly ExPerformanceCounter AvgFileModeWriteTime;

		// Token: 0x040010C3 RID: 4291
		public readonly ExPerformanceCounter AvgFileModeWriteTimeBase;

		// Token: 0x040010C4 RID: 4292
		public readonly ExPerformanceCounter PassiveSeedingSource;

		// Token: 0x040010C5 RID: 4293
		public readonly ExPerformanceCounter GetCopyStatusInstanceCalls;

		// Token: 0x040010C6 RID: 4294
		public readonly ExPerformanceCounter GetCopyStatusInstanceCallsPerSec;
	}
}
