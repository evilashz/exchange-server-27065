using System;
using System.Collections.Generic;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data.HA.DirectoryServices;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000340 RID: 832
	internal class IOBufferPool
	{
		// Token: 0x060021B3 RID: 8627 RVA: 0x0009C82A File Offset: 0x0009AA2A
		public static IOBuffer Allocate()
		{
			return IOBufferPool.Instance.DispenseBuffer();
		}

		// Token: 0x060021B4 RID: 8628 RVA: 0x0009C836 File Offset: 0x0009AA36
		public static void Free(IOBuffer buf)
		{
			IOBufferPool.Instance.ReturnBuffer(buf);
		}

		// Token: 0x060021B5 RID: 8629 RVA: 0x0009C843 File Offset: 0x0009AA43
		public static void ConfigureCopyCount(int copyCount)
		{
			IOBufferPool.Instance.ConfigureCopyCountInternal(copyCount);
		}

		// Token: 0x060021B6 RID: 8630 RVA: 0x0009C850 File Offset: 0x0009AA50
		private IOBuffer DispenseBuffer()
		{
			IOBuffer result;
			lock (this)
			{
				IOBuffer iobuffer;
				if (this.freeList.Count > 0)
				{
					int index = this.freeList.Count - 1;
					iobuffer = this.freeList[index];
					this.freeList.RemoveAt(index);
				}
				else
				{
					if (this.knownList.Count >= this.maxBufferCount)
					{
						throw new IOBufferPoolLimitException(this.maxBufferCount, 1048576);
					}
					iobuffer = new IOBuffer(1048576, false);
					this.knownList.Add(iobuffer);
				}
				result = iobuffer;
			}
			return result;
		}

		// Token: 0x060021B7 RID: 8631 RVA: 0x0009C900 File Offset: 0x0009AB00
		private void ReturnBuffer(IOBuffer buf)
		{
			lock (this)
			{
				if (!buf.PreAllocated)
				{
					this.knownList.Remove(buf);
				}
				else
				{
					this.freeList.Add(buf);
				}
			}
		}

		// Token: 0x060021B8 RID: 8632 RVA: 0x0009C958 File Offset: 0x0009AB58
		private void ConfigureCopyCountInternal(int copyCount)
		{
			lock (this)
			{
				if (!this.hasPreallocationOccurred)
				{
					IOBufferPool.Tracer.TraceDebug(0L, "IOBufferPool is handling preallocation");
					this.HandlePreallocation(copyCount);
					this.hasPreallocationOccurred = true;
				}
				else
				{
					int num = copyCount * this.buffersPerCopy;
					if (num > this.maxBufferCount)
					{
						IOBufferPool.Tracer.TraceDebug<int>(0L, "IOBufferPool.ConfigureCopyCount increases max buffers to {0}", num);
						this.maxBufferCount = num;
					}
				}
			}
		}

		// Token: 0x060021B9 RID: 8633 RVA: 0x0009C9E4 File Offset: 0x0009ABE4
		private void HandlePreallocation(int copyCount)
		{
			int num = 50;
			string text = null;
			int iobufferPoolPreallocationOverride = RegistryParameters.IOBufferPoolPreallocationOverride;
			if (iobufferPoolPreallocationOverride >= 0)
			{
				num = iobufferPoolPreallocationOverride;
				text = string.Format("IOBufferPoolPreallocationOverride was used. BufCount={0}", iobufferPoolPreallocationOverride);
			}
			else
			{
				IMonitoringADConfig monitoringADConfig = null;
				try
				{
					monitoringADConfig = Dependencies.MonitoringADConfigProvider.GetConfig(true);
				}
				catch (MonitoringADConfigException arg)
				{
					IOBufferPool.Tracer.TraceError<MonitoringADConfigException>(0L, "IOBufferPool.HandlePreallocation failed to get adConfig: {0}", arg);
					text = string.Format("Failed to get adConfig: {0}", arg);
				}
				if (monitoringADConfig == null)
				{
					if (text == null)
					{
						text = "Failed to get adConfig but no exception was thrown";
					}
				}
				else if (monitoringADConfig.ServerRole != MonitoringServerRole.DagMember)
				{
					num = 0;
					text = string.Format("Non-DAG role: {0}", monitoringADConfig.ServerRole);
				}
				else
				{
					IADServer srv = monitoringADConfig.LookupMiniServerByName(AmServerName.LocalComputerName);
					int maxMemoryPerDatabase = PassiveBlockMode.GetMaxMemoryPerDatabase(srv);
					int maxBuffersPerDatabase = PassiveBlockMode.GetMaxBuffersPerDatabase(maxMemoryPerDatabase);
					this.buffersPerCopy = maxBuffersPerDatabase;
					IADDatabaseAvailabilityGroup dag = monitoringADConfig.Dag;
					int num2 = 0;
					if (dag.AutoDagTotalNumberOfServers > 0)
					{
						num2 = dag.AutoDagTotalNumberOfDatabases * dag.AutoDagDatabaseCopiesPerDatabase / dag.AutoDagTotalNumberOfServers;
					}
					text = string.Format("BuffersPerCopy={0}. ExpectedCopiesPerServer={1} RIMgr reports {2} copies.", this.buffersPerCopy, num2, copyCount);
					copyCount = Math.Max(copyCount, num2);
					num = copyCount * this.buffersPerCopy;
				}
			}
			if (num > this.maxBufferCount)
			{
				this.maxBufferCount = num;
			}
			this.Preallocate(num);
			ReplayCrimsonEvents.IoBufferPoolInit.Log<int, string>(num, text);
		}

		// Token: 0x060021BA RID: 8634 RVA: 0x0009CB44 File Offset: 0x0009AD44
		private void Preallocate(int bufsToPrealloc)
		{
			int num = bufsToPrealloc;
			while (num-- > 0)
			{
				IOBuffer item = new IOBuffer(1048576, true);
				this.knownList.Add(item);
				this.freeList.Add(item);
			}
			IOBufferPool.Tracer.TraceDebug<int>(0L, "IOBufferPool.Preallocated {0} bufs", bufsToPrealloc);
		}

		// Token: 0x04000DE2 RID: 3554
		public const int BufferSize = 1048576;

		// Token: 0x04000DE3 RID: 3555
		public const int DefaultInitialAllocationOfBuffers = 50;

		// Token: 0x04000DE4 RID: 3556
		private static readonly Trace Tracer = ExTraceGlobals.PassiveBlockModeTracer;

		// Token: 0x04000DE5 RID: 3557
		private static IOBufferPool Instance = new IOBufferPool();

		// Token: 0x04000DE6 RID: 3558
		private List<IOBuffer> freeList = new List<IOBuffer>();

		// Token: 0x04000DE7 RID: 3559
		private List<IOBuffer> knownList = new List<IOBuffer>();

		// Token: 0x04000DE8 RID: 3560
		private int maxBufferCount = 50;

		// Token: 0x04000DE9 RID: 3561
		private int buffersPerCopy = 10;

		// Token: 0x04000DEA RID: 3562
		private bool hasPreallocationOccurred;
	}
}
