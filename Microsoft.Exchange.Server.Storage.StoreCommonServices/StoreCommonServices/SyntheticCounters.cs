using System;
using System.Collections.Generic;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x02000127 RID: 295
	internal sealed class SyntheticCounters
	{
		// Token: 0x06000B70 RID: 2928 RVA: 0x00039968 File Offset: 0x00037B68
		private SyntheticCounters()
		{
			this.totalsByOperationType = new Dictionary<OperationType, double>(50);
			this.totalsByClientType = new Dictionary<ClientType, double>(50);
			this.totalsByActivity = new Dictionary<uint, double>(200);
			this.totalsByOperation = new Dictionary<OperationType, Dictionary<byte, double>>(5);
			this.totalsByWorkLoad = new Dictionary<WorkLoadType, double>(5);
		}

		// Token: 0x06000B71 RID: 2929 RVA: 0x000399BD File Offset: 0x00037BBD
		public static SyntheticCounters Create()
		{
			return new SyntheticCounters();
		}

		// Token: 0x06000B72 RID: 2930 RVA: 0x000399C4 File Offset: 0x00037BC4
		public void Add(OperationType operationType, double value)
		{
			if (this.totalsByOperationType.ContainsKey(operationType))
			{
				Dictionary<OperationType, double> dictionary;
				(dictionary = this.totalsByOperationType)[operationType] = dictionary[operationType] + value;
				return;
			}
			this.totalsByOperationType[operationType] = value;
		}

		// Token: 0x06000B73 RID: 2931 RVA: 0x00039A08 File Offset: 0x00037C08
		public void Add(ClientType clientType, double value)
		{
			if (this.totalsByClientType.ContainsKey(clientType))
			{
				Dictionary<ClientType, double> dictionary;
				(dictionary = this.totalsByClientType)[clientType] = dictionary[clientType] + value;
				return;
			}
			this.totalsByClientType[clientType] = value;
		}

		// Token: 0x06000B74 RID: 2932 RVA: 0x00039A4C File Offset: 0x00037C4C
		public void Add(uint activityId, double value)
		{
			if (this.totalsByActivity.ContainsKey(activityId))
			{
				Dictionary<uint, double> dictionary;
				(dictionary = this.totalsByActivity)[activityId] = dictionary[activityId] + value;
				return;
			}
			this.totalsByActivity[activityId] = value;
		}

		// Token: 0x06000B75 RID: 2933 RVA: 0x00039A90 File Offset: 0x00037C90
		public void Add(OperationType operationType, byte operationId, double value)
		{
			if (!this.totalsByOperation.ContainsKey(operationType))
			{
				this.totalsByOperation[operationType] = new Dictionary<byte, double>(200);
				this.totalsByOperation[operationType][operationId] = value;
				return;
			}
			if (this.totalsByOperation[operationType].ContainsKey(operationId))
			{
				Dictionary<byte, double> dictionary;
				(dictionary = this.totalsByOperation[operationType])[operationId] = dictionary[operationId] + value;
				return;
			}
			this.totalsByOperation[operationType][operationId] = value;
		}

		// Token: 0x06000B76 RID: 2934 RVA: 0x00039B1C File Offset: 0x00037D1C
		public void Add(WorkLoadType workLoadType, double value)
		{
			if (this.totalsByWorkLoad.ContainsKey(workLoadType))
			{
				Dictionary<WorkLoadType, double> dictionary;
				(dictionary = this.totalsByWorkLoad)[workLoadType] = dictionary[workLoadType] + value;
				return;
			}
			this.totalsByWorkLoad[workLoadType] = value;
		}

		// Token: 0x06000B77 RID: 2935 RVA: 0x00039B60 File Offset: 0x00037D60
		public void WriteTrace()
		{
			IBinaryLogger logger = LoggerManager.GetLogger(LoggerType.SyntheticCounters);
			if (logger == null || !logger.IsLoggingEnabled)
			{
				return;
			}
			foreach (OperationType operationType in this.totalsByOperationType.Keys)
			{
				using (TraceBuffer traceBuffer = TraceRecord.Create(LoggerManager.TraceGuids.SyntheticCounters, false, false, StoreEnvironment.MachineName, "ProcessorByOperationType", operationType.ToString(), this.totalsByOperationType[operationType]))
				{
					logger.TryWrite(traceBuffer);
				}
			}
			foreach (ClientType clientType in this.totalsByClientType.Keys)
			{
				using (TraceBuffer traceBuffer2 = TraceRecord.Create(LoggerManager.TraceGuids.SyntheticCounters, false, false, StoreEnvironment.MachineName, "ProcessorByClient", clientType.ToString(), this.totalsByClientType[clientType]))
				{
					logger.TryWrite(traceBuffer2);
				}
			}
			foreach (uint num in this.totalsByActivity.Keys)
			{
				using (TraceBuffer traceBuffer3 = TraceRecord.Create(LoggerManager.TraceGuids.SyntheticCounters, false, false, StoreEnvironment.MachineName, "ProcessorByActivity", ClientActivityStrings.GetString(num), this.totalsByActivity[num]))
				{
					logger.TryWrite(traceBuffer3);
				}
			}
			foreach (OperationType operationType2 in this.totalsByOperation.Keys)
			{
				foreach (KeyValuePair<byte, double> keyValuePair in this.totalsByOperation[operationType2])
				{
					string arg = RopSummaryResolver.ContainsKey(operationType2) ? RopSummaryResolver.Get(operationType2)(keyValuePair.Key) : "Unknown";
					string strValue = string.Format("{0}.{1}", operationType2, arg);
					using (TraceBuffer traceBuffer4 = TraceRecord.Create(LoggerManager.TraceGuids.SyntheticCounters, false, false, StoreEnvironment.MachineName, "ProcessorByOperation", strValue, keyValuePair.Value))
					{
						logger.TryWrite(traceBuffer4);
					}
				}
			}
			foreach (WorkLoadType workLoadType in this.totalsByWorkLoad.Keys)
			{
				using (TraceBuffer traceBuffer5 = TraceRecord.Create(LoggerManager.TraceGuids.SyntheticCounters, false, false, StoreEnvironment.MachineName, "ProcessorByWorkLoadType", workLoadType.ToString(), this.totalsByWorkLoad[workLoadType]))
				{
					logger.TryWrite(traceBuffer5);
				}
			}
			this.totalsByOperationType.Clear();
			this.totalsByClientType.Clear();
			this.totalsByActivity.Clear();
			this.totalsByOperation.Clear();
			this.totalsByWorkLoad.Clear();
		}

		// Token: 0x0400064F RID: 1615
		private const string ProcessorByOperationType = "ProcessorByOperationType";

		// Token: 0x04000650 RID: 1616
		private const string ProcessorByClient = "ProcessorByClient";

		// Token: 0x04000651 RID: 1617
		private const string ProcessorByActivity = "ProcessorByActivity";

		// Token: 0x04000652 RID: 1618
		private const string ProcessorByOperation = "ProcessorByOperation";

		// Token: 0x04000653 RID: 1619
		private const string ProcessorByWorkLoadType = "ProcessorByWorkLoadType";

		// Token: 0x04000654 RID: 1620
		private readonly Dictionary<OperationType, double> totalsByOperationType;

		// Token: 0x04000655 RID: 1621
		private readonly Dictionary<ClientType, double> totalsByClientType;

		// Token: 0x04000656 RID: 1622
		private readonly Dictionary<uint, double> totalsByActivity;

		// Token: 0x04000657 RID: 1623
		private readonly Dictionary<OperationType, Dictionary<byte, double>> totalsByOperation;

		// Token: 0x04000658 RID: 1624
		private readonly Dictionary<WorkLoadType, double> totalsByWorkLoad;
	}
}
