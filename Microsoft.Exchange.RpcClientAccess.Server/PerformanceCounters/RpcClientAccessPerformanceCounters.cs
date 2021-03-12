using System;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.PerformanceCounters
{
	// Token: 0x02000034 RID: 52
	internal static class RpcClientAccessPerformanceCounters
	{
		// Token: 0x0600018E RID: 398 RVA: 0x0000781C File Offset: 0x00005A1C
		public static void GetPerfCounterInfo(XElement element)
		{
			if (RpcClientAccessPerformanceCounters.AllCounters == null)
			{
				return;
			}
			foreach (ExPerformanceCounter exPerformanceCounter in RpcClientAccessPerformanceCounters.AllCounters)
			{
				try
				{
					element.Add(new XElement(ExPerformanceCounter.GetEncodedName(exPerformanceCounter.CounterName), exPerformanceCounter.NextValue()));
				}
				catch (XmlException ex)
				{
					XElement content = new XElement("Error", ex.Message);
					element.Add(content);
				}
			}
		}

		// Token: 0x040000AA RID: 170
		public const string CategoryName = "MSExchange RpcClientAccess";

		// Token: 0x040000AB RID: 171
		public static readonly ExPerformanceCounter RPCRequests = new ExPerformanceCounter("MSExchange RpcClientAccess", "RPC Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000AC RID: 172
		public static readonly ExPerformanceCounter RPCPacketsRate = new ExPerformanceCounter("MSExchange RpcClientAccess", "RPC Packets/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000AD RID: 173
		public static readonly ExPerformanceCounter RPCOperationsRate = new ExPerformanceCounter("MSExchange RpcClientAccess", "RPC Operations/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000AE RID: 174
		public static readonly ExPerformanceCounter RPCAveragedLatency = new ExPerformanceCounter("MSExchange RpcClientAccess", "RPC Averaged Latency", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000AF RID: 175
		public static readonly ExPerformanceCounter RPCBytesRead = new ExPerformanceCounter("MSExchange RpcClientAccess", "RPC Clients Bytes Read", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000B0 RID: 176
		public static readonly ExPerformanceCounter RPCBytesWritten = new ExPerformanceCounter("MSExchange RpcClientAccess", "RPC Clients Bytes Written", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000B1 RID: 177
		public static readonly ExPerformanceCounter RPCUncompressedBytesRead = new ExPerformanceCounter("MSExchange RpcClientAccess", "RPC Clients Uncompressed Bytes Read", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000B2 RID: 178
		public static readonly ExPerformanceCounter RPCUncompressedBytesWritten = new ExPerformanceCounter("MSExchange RpcClientAccess", "RPC Clients Uncompressed Bytes Written", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000B3 RID: 179
		public static readonly ExPerformanceCounter ConnectionCount = new ExPerformanceCounter("MSExchange RpcClientAccess", "Connection Count", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000B4 RID: 180
		public static readonly ExPerformanceCounter ActiveUserCount = new ExPerformanceCounter("MSExchange RpcClientAccess", "Active User Count", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000B5 RID: 181
		public static readonly ExPerformanceCounter UserCount = new ExPerformanceCounter("MSExchange RpcClientAccess", "User Count", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000B6 RID: 182
		public static readonly ExPerformanceCounter ClientRpcAttempted = new ExPerformanceCounter("MSExchange RpcClientAccess", "Client: RPCs attempted", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000B7 RID: 183
		public static readonly ExPerformanceCounter ClientRpcSucceeded = new ExPerformanceCounter("MSExchange RpcClientAccess", "Client: RPCs succeeded", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000B8 RID: 184
		public static readonly ExPerformanceCounter ClientBackgroundRpcSucceeded = new ExPerformanceCounter("MSExchange RpcClientAccess", "Client: Background RPCs succeeded", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000B9 RID: 185
		public static readonly ExPerformanceCounter ClientForegroundRpcSucceeded = new ExPerformanceCounter("MSExchange RpcClientAccess", "Client: Foreground RPCs succeeded", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000BA RID: 186
		public static readonly ExPerformanceCounter ClientRpcFailed = new ExPerformanceCounter("MSExchange RpcClientAccess", "Client: RPCs Failed", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000BB RID: 187
		public static readonly ExPerformanceCounter ClientBackgroundRpcFailed = new ExPerformanceCounter("MSExchange RpcClientAccess", "Client: Background RPCs Failed", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000BC RID: 188
		public static readonly ExPerformanceCounter ClientForegroundRpcFailed = new ExPerformanceCounter("MSExchange RpcClientAccess", "Client: Foreground RPCs Failed", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000BD RID: 189
		public static readonly ExPerformanceCounter ClientRpcSlow1 = new ExPerformanceCounter("MSExchange RpcClientAccess", "Client: Latency > 2 sec RPCs", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000BE RID: 190
		public static readonly ExPerformanceCounter ClientRpcSlow2 = new ExPerformanceCounter("MSExchange RpcClientAccess", "Client: Latency > 5 sec RPCs", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000BF RID: 191
		public static readonly ExPerformanceCounter ClientRpcSlow3 = new ExPerformanceCounter("MSExchange RpcClientAccess", "Client: Latency > 10 sec RPCs", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000C0 RID: 192
		public static readonly ExPerformanceCounter RPCDispatchTaskQueueLength = new ExPerformanceCounter("MSExchange RpcClientAccess", "RPC dispatch task queue length", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000C1 RID: 193
		public static readonly ExPerformanceCounter RPCDispatchTaskThreads = new ExPerformanceCounter("MSExchange RpcClientAccess", "RPC dispatch task threads", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000C2 RID: 194
		public static readonly ExPerformanceCounter RPCDispatchTaskActiveThreads = new ExPerformanceCounter("MSExchange RpcClientAccess", "RPC dispatch task active threads", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000C3 RID: 195
		public static readonly ExPerformanceCounter RPCDispatchTaskOperationsRate = new ExPerformanceCounter("MSExchange RpcClientAccess", "RPC dispatch task operations/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000C4 RID: 196
		public static readonly ExPerformanceCounter XTCDispatchTaskQueueLength = new ExPerformanceCounter("MSExchange RpcClientAccess", "XTC dispatch task queue length", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000C5 RID: 197
		public static readonly ExPerformanceCounter XTCDispatchTaskThreads = new ExPerformanceCounter("MSExchange RpcClientAccess", "XTC dispatch task threads", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000C6 RID: 198
		public static readonly ExPerformanceCounter XTCDispatchTaskActiveThreads = new ExPerformanceCounter("MSExchange RpcClientAccess", "XTC dispatch task active threads", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000C7 RID: 199
		public static readonly ExPerformanceCounter XTCDispatchTaskOperationsRate = new ExPerformanceCounter("MSExchange RpcClientAccess", "XTC dispatch task operations/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000C8 RID: 200
		public static readonly ExPerformanceCounter RpcHttpConnectionRegistrationDispatchTaskQueueLength = new ExPerformanceCounter("MSExchange RpcClientAccess", "RpcHttpConnectionRegistration dispatch task queue length", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000C9 RID: 201
		public static readonly ExPerformanceCounter RpcHttpConnectionRegistrationDispatchTaskThreads = new ExPerformanceCounter("MSExchange RpcClientAccess", "RpcHttpConnectionRegistration dispatch task threads", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000CA RID: 202
		public static readonly ExPerformanceCounter RpcHttpConnectionRegistrationDispatchTaskActiveThreads = new ExPerformanceCounter("MSExchange RpcClientAccess", "RpcHttpConnectionRegistration dispatch task active threads", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000CB RID: 203
		public static readonly ExPerformanceCounter RpcHttpConnectionRegistrationDispatchTaskOperationsRate = new ExPerformanceCounter("MSExchange RpcClientAccess", "RpcHttpConnectionRegistration dispatch task operations/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000CC RID: 204
		public static readonly ExPerformanceCounter[] AllCounters = new ExPerformanceCounter[]
		{
			RpcClientAccessPerformanceCounters.RPCRequests,
			RpcClientAccessPerformanceCounters.RPCPacketsRate,
			RpcClientAccessPerformanceCounters.RPCOperationsRate,
			RpcClientAccessPerformanceCounters.RPCAveragedLatency,
			RpcClientAccessPerformanceCounters.RPCBytesRead,
			RpcClientAccessPerformanceCounters.RPCBytesWritten,
			RpcClientAccessPerformanceCounters.RPCUncompressedBytesRead,
			RpcClientAccessPerformanceCounters.RPCUncompressedBytesWritten,
			RpcClientAccessPerformanceCounters.ConnectionCount,
			RpcClientAccessPerformanceCounters.ActiveUserCount,
			RpcClientAccessPerformanceCounters.UserCount,
			RpcClientAccessPerformanceCounters.ClientRpcAttempted,
			RpcClientAccessPerformanceCounters.ClientRpcSucceeded,
			RpcClientAccessPerformanceCounters.ClientBackgroundRpcSucceeded,
			RpcClientAccessPerformanceCounters.ClientForegroundRpcSucceeded,
			RpcClientAccessPerformanceCounters.ClientRpcFailed,
			RpcClientAccessPerformanceCounters.ClientBackgroundRpcFailed,
			RpcClientAccessPerformanceCounters.ClientForegroundRpcFailed,
			RpcClientAccessPerformanceCounters.ClientRpcSlow1,
			RpcClientAccessPerformanceCounters.ClientRpcSlow2,
			RpcClientAccessPerformanceCounters.ClientRpcSlow3,
			RpcClientAccessPerformanceCounters.RPCDispatchTaskQueueLength,
			RpcClientAccessPerformanceCounters.RPCDispatchTaskThreads,
			RpcClientAccessPerformanceCounters.RPCDispatchTaskActiveThreads,
			RpcClientAccessPerformanceCounters.RPCDispatchTaskOperationsRate,
			RpcClientAccessPerformanceCounters.XTCDispatchTaskQueueLength,
			RpcClientAccessPerformanceCounters.XTCDispatchTaskThreads,
			RpcClientAccessPerformanceCounters.XTCDispatchTaskActiveThreads,
			RpcClientAccessPerformanceCounters.XTCDispatchTaskOperationsRate,
			RpcClientAccessPerformanceCounters.RpcHttpConnectionRegistrationDispatchTaskQueueLength,
			RpcClientAccessPerformanceCounters.RpcHttpConnectionRegistrationDispatchTaskThreads,
			RpcClientAccessPerformanceCounters.RpcHttpConnectionRegistrationDispatchTaskActiveThreads,
			RpcClientAccessPerformanceCounters.RpcHttpConnectionRegistrationDispatchTaskOperationsRate
		};
	}
}
