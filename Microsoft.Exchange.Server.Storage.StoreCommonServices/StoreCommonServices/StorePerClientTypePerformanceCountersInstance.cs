using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x02000009 RID: 9
	internal sealed class StorePerClientTypePerformanceCountersInstance : PerformanceCounterInstance
	{
		// Token: 0x0600006F RID: 111 RVA: 0x00004FEC File Offset: 0x000031EC
		internal StorePerClientTypePerformanceCountersInstance(string instanceName, StorePerClientTypePerformanceCountersInstance autoUpdateTotalInstance) : base(instanceName, "MSExchangeIS Client Type")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.AdminRPCsInProgress = new BufferedPerformanceCounter(base.CategoryName, "Admin RPC Requests", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.AdminRPCsInProgress, new ExPerformanceCounter[0]);
				list.Add(this.AdminRPCsInProgress);
				this.AdminRPCsRateOfExecuteTask = new BufferedPerformanceCounter(base.CategoryName, "Administrative RPC requests/sec", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.AdminRPCsRateOfExecuteTask, new ExPerformanceCounter[0]);
				list.Add(this.AdminRPCsRateOfExecuteTask);
				this.DirectoryAccessSearchRate = new BufferedPerformanceCounter(base.CategoryName, "Directory Access: LDAP Searches/sec", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.DirectoryAccessSearchRate, new ExPerformanceCounter[0]);
				list.Add(this.DirectoryAccessSearchRate);
				this.RPCRequests = new BufferedPerformanceCounter(base.CategoryName, "RPC Requests", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.RPCRequests, new ExPerformanceCounter[0]);
				list.Add(this.RPCRequests);
				this.RPCBytesInRate = new BufferedPerformanceCounter(base.CategoryName, "RPC Bytes Received/sec", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.RPCBytesInRate, new ExPerformanceCounter[0]);
				list.Add(this.RPCBytesInRate);
				this.RPCBytesOutRate = new BufferedPerformanceCounter(base.CategoryName, "RPC Bytes Sent/sec", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.RPCBytesOutRate, new ExPerformanceCounter[0]);
				list.Add(this.RPCBytesOutRate);
				this.RPCPacketsRate = new BufferedPerformanceCounter(base.CategoryName, "RPC Packets/sec", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.RPCPacketsRate, new ExPerformanceCounter[0]);
				list.Add(this.RPCPacketsRate);
				this.RPCOperationRate = new BufferedPerformanceCounter(base.CategoryName, "RPC Operations/sec", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.RPCOperationRate, new ExPerformanceCounter[0]);
				list.Add(this.RPCOperationRate);
				this.RPCAverageLatency = new BufferedPerformanceCounter(base.CategoryName, "RPC Average Latency", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.RPCAverageLatency, new ExPerformanceCounter[0]);
				list.Add(this.RPCAverageLatency);
				this.RPCAverageLatencyBase = new BufferedPerformanceCounter(base.CategoryName, "Average Time spent in an RPC Base", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.RPCAverageLatencyBase, new ExPerformanceCounter[0]);
				list.Add(this.RPCAverageLatencyBase);
				this.LazyIndexesCreatedRate = new BufferedPerformanceCounter(base.CategoryName, "Lazy indexes created/sec", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.LazyIndexesCreatedRate, new ExPerformanceCounter[0]);
				list.Add(this.LazyIndexesCreatedRate);
				this.LazyIndexesDeletedRate = new BufferedPerformanceCounter(base.CategoryName, "Lazy indexes deleted/sec", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.LazyIndexesDeletedRate, new ExPerformanceCounter[0]);
				list.Add(this.LazyIndexesDeletedRate);
				this.LazyIndexesFullRefreshRate = new BufferedPerformanceCounter(base.CategoryName, "Lazy index full refresh/sec", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.LazyIndexesFullRefreshRate, new ExPerformanceCounter[0]);
				list.Add(this.LazyIndexesFullRefreshRate);
				this.LazyIndexesIncrementalRefreshRate = new BufferedPerformanceCounter(base.CategoryName, "Lazy index incremental refresh/sec", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.LazyIndexesIncrementalRefreshRate, new ExPerformanceCounter[0]);
				list.Add(this.LazyIndexesIncrementalRefreshRate);
				this.MessagesOpenedRate = new BufferedPerformanceCounter(base.CategoryName, "Messages opened/sec", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MessagesOpenedRate, new ExPerformanceCounter[0]);
				list.Add(this.MessagesOpenedRate);
				this.MessagesCreatedRate = new BufferedPerformanceCounter(base.CategoryName, "Messages created/sec", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MessagesCreatedRate, new ExPerformanceCounter[0]);
				list.Add(this.MessagesCreatedRate);
				this.MessagesUpdatedRate = new BufferedPerformanceCounter(base.CategoryName, "Messages updated/sec", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MessagesUpdatedRate, new ExPerformanceCounter[0]);
				list.Add(this.MessagesUpdatedRate);
				this.MessagesDeletedRate = new BufferedPerformanceCounter(base.CategoryName, "Messages deleted/sec", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MessagesDeletedRate, new ExPerformanceCounter[0]);
				list.Add(this.MessagesDeletedRate);
				this.PropertyPromotionRate = new BufferedPerformanceCounter(base.CategoryName, "Property promotions/sec", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PropertyPromotionRate, new ExPerformanceCounter[0]);
				list.Add(this.PropertyPromotionRate);
				this.JetPageReferencedRate = new BufferedPerformanceCounter(base.CategoryName, "Jet Pages Referenced/sec", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.JetPageReferencedRate, new ExPerformanceCounter[0]);
				list.Add(this.JetPageReferencedRate);
				this.JetPageReadRate = new BufferedPerformanceCounter(base.CategoryName, "Jet Pages Read/sec", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.JetPageReadRate, new ExPerformanceCounter[0]);
				list.Add(this.JetPageReadRate);
				this.JetPagePrereadRate = new BufferedPerformanceCounter(base.CategoryName, "Jet Pages Preread/sec", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.JetPagePrereadRate, new ExPerformanceCounter[0]);
				list.Add(this.JetPagePrereadRate);
				this.JetPageDirtiedRate = new BufferedPerformanceCounter(base.CategoryName, "Jet Pages Modified/sec", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.JetPageDirtiedRate, new ExPerformanceCounter[0]);
				list.Add(this.JetPageDirtiedRate);
				this.JetPageReDirtiedRate = new BufferedPerformanceCounter(base.CategoryName, "Jet Pages Remodified/sec", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.JetPageReDirtiedRate, new ExPerformanceCounter[0]);
				list.Add(this.JetPageReDirtiedRate);
				this.JetLogRecordRate = new BufferedPerformanceCounter(base.CategoryName, "Jet Log Records/sec", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.JetLogRecordRate, new ExPerformanceCounter[0]);
				list.Add(this.JetLogRecordRate);
				this.JetLogRecordBytesRate = new BufferedPerformanceCounter(base.CategoryName, "Jet Log Record Bytes/sec", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.JetLogRecordBytesRate, new ExPerformanceCounter[0]);
				list.Add(this.JetLogRecordBytesRate);
				long num = this.AdminRPCsInProgress.RawValue;
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

		// Token: 0x06000070 RID: 112 RVA: 0x000055FC File Offset: 0x000037FC
		internal StorePerClientTypePerformanceCountersInstance(string instanceName) : base(instanceName, "MSExchangeIS Client Type")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.AdminRPCsInProgress = new ExPerformanceCounter(base.CategoryName, "Admin RPC Requests", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AdminRPCsInProgress);
				this.AdminRPCsRateOfExecuteTask = new ExPerformanceCounter(base.CategoryName, "Administrative RPC requests/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AdminRPCsRateOfExecuteTask);
				this.DirectoryAccessSearchRate = new ExPerformanceCounter(base.CategoryName, "Directory Access: LDAP Searches/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.DirectoryAccessSearchRate);
				this.RPCRequests = new ExPerformanceCounter(base.CategoryName, "RPC Requests", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.RPCRequests);
				this.RPCBytesInRate = new ExPerformanceCounter(base.CategoryName, "RPC Bytes Received/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.RPCBytesInRate);
				this.RPCBytesOutRate = new ExPerformanceCounter(base.CategoryName, "RPC Bytes Sent/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.RPCBytesOutRate);
				this.RPCPacketsRate = new ExPerformanceCounter(base.CategoryName, "RPC Packets/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.RPCPacketsRate);
				this.RPCOperationRate = new ExPerformanceCounter(base.CategoryName, "RPC Operations/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.RPCOperationRate);
				this.RPCAverageLatency = new ExPerformanceCounter(base.CategoryName, "RPC Average Latency", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.RPCAverageLatency);
				this.RPCAverageLatencyBase = new ExPerformanceCounter(base.CategoryName, "Average Time spent in an RPC Base", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.RPCAverageLatencyBase);
				this.LazyIndexesCreatedRate = new ExPerformanceCounter(base.CategoryName, "Lazy indexes created/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.LazyIndexesCreatedRate);
				this.LazyIndexesDeletedRate = new ExPerformanceCounter(base.CategoryName, "Lazy indexes deleted/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.LazyIndexesDeletedRate);
				this.LazyIndexesFullRefreshRate = new ExPerformanceCounter(base.CategoryName, "Lazy index full refresh/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.LazyIndexesFullRefreshRate);
				this.LazyIndexesIncrementalRefreshRate = new ExPerformanceCounter(base.CategoryName, "Lazy index incremental refresh/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.LazyIndexesIncrementalRefreshRate);
				this.MessagesOpenedRate = new ExPerformanceCounter(base.CategoryName, "Messages opened/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MessagesOpenedRate);
				this.MessagesCreatedRate = new ExPerformanceCounter(base.CategoryName, "Messages created/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MessagesCreatedRate);
				this.MessagesUpdatedRate = new ExPerformanceCounter(base.CategoryName, "Messages updated/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MessagesUpdatedRate);
				this.MessagesDeletedRate = new ExPerformanceCounter(base.CategoryName, "Messages deleted/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MessagesDeletedRate);
				this.PropertyPromotionRate = new ExPerformanceCounter(base.CategoryName, "Property promotions/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PropertyPromotionRate);
				this.JetPageReferencedRate = new ExPerformanceCounter(base.CategoryName, "Jet Pages Referenced/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.JetPageReferencedRate);
				this.JetPageReadRate = new ExPerformanceCounter(base.CategoryName, "Jet Pages Read/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.JetPageReadRate);
				this.JetPagePrereadRate = new ExPerformanceCounter(base.CategoryName, "Jet Pages Preread/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.JetPagePrereadRate);
				this.JetPageDirtiedRate = new ExPerformanceCounter(base.CategoryName, "Jet Pages Modified/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.JetPageDirtiedRate);
				this.JetPageReDirtiedRate = new ExPerformanceCounter(base.CategoryName, "Jet Pages Remodified/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.JetPageReDirtiedRate);
				this.JetLogRecordRate = new ExPerformanceCounter(base.CategoryName, "Jet Log Records/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.JetLogRecordRate);
				this.JetLogRecordBytesRate = new ExPerformanceCounter(base.CategoryName, "Jet Log Record Bytes/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.JetLogRecordBytesRate);
				long num = this.AdminRPCsInProgress.RawValue;
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

		// Token: 0x06000071 RID: 113 RVA: 0x00005AEC File Offset: 0x00003CEC
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

		// Token: 0x04000099 RID: 153
		public readonly ExPerformanceCounter AdminRPCsInProgress;

		// Token: 0x0400009A RID: 154
		public readonly ExPerformanceCounter AdminRPCsRateOfExecuteTask;

		// Token: 0x0400009B RID: 155
		public readonly ExPerformanceCounter DirectoryAccessSearchRate;

		// Token: 0x0400009C RID: 156
		public readonly ExPerformanceCounter RPCRequests;

		// Token: 0x0400009D RID: 157
		public readonly ExPerformanceCounter RPCBytesInRate;

		// Token: 0x0400009E RID: 158
		public readonly ExPerformanceCounter RPCBytesOutRate;

		// Token: 0x0400009F RID: 159
		public readonly ExPerformanceCounter RPCPacketsRate;

		// Token: 0x040000A0 RID: 160
		public readonly ExPerformanceCounter RPCOperationRate;

		// Token: 0x040000A1 RID: 161
		public readonly ExPerformanceCounter RPCAverageLatency;

		// Token: 0x040000A2 RID: 162
		public readonly ExPerformanceCounter RPCAverageLatencyBase;

		// Token: 0x040000A3 RID: 163
		public readonly ExPerformanceCounter LazyIndexesCreatedRate;

		// Token: 0x040000A4 RID: 164
		public readonly ExPerformanceCounter LazyIndexesDeletedRate;

		// Token: 0x040000A5 RID: 165
		public readonly ExPerformanceCounter LazyIndexesFullRefreshRate;

		// Token: 0x040000A6 RID: 166
		public readonly ExPerformanceCounter LazyIndexesIncrementalRefreshRate;

		// Token: 0x040000A7 RID: 167
		public readonly ExPerformanceCounter MessagesOpenedRate;

		// Token: 0x040000A8 RID: 168
		public readonly ExPerformanceCounter MessagesCreatedRate;

		// Token: 0x040000A9 RID: 169
		public readonly ExPerformanceCounter MessagesUpdatedRate;

		// Token: 0x040000AA RID: 170
		public readonly ExPerformanceCounter MessagesDeletedRate;

		// Token: 0x040000AB RID: 171
		public readonly ExPerformanceCounter PropertyPromotionRate;

		// Token: 0x040000AC RID: 172
		public readonly ExPerformanceCounter JetPageReferencedRate;

		// Token: 0x040000AD RID: 173
		public readonly ExPerformanceCounter JetPageReadRate;

		// Token: 0x040000AE RID: 174
		public readonly ExPerformanceCounter JetPagePrereadRate;

		// Token: 0x040000AF RID: 175
		public readonly ExPerformanceCounter JetPageDirtiedRate;

		// Token: 0x040000B0 RID: 176
		public readonly ExPerformanceCounter JetPageReDirtiedRate;

		// Token: 0x040000B1 RID: 177
		public readonly ExPerformanceCounter JetLogRecordRate;

		// Token: 0x040000B2 RID: 178
		public readonly ExPerformanceCounter JetLogRecordBytesRate;
	}
}
