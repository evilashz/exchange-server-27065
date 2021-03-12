using System;
using System.Diagnostics;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ThrottlingService;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.Rpc.Throttling;

namespace Microsoft.Exchange.Data.ThrottlingService.Client
{
	// Token: 0x02000006 RID: 6
	internal sealed class ThrottlingRpcClientImpl : IDisposable
	{
		// Token: 0x0600001B RID: 27 RVA: 0x00002834 File Offset: 0x00000A34
		public ThrottlingRpcClientImpl(string serverName) : this(serverName, Process.GetCurrentProcess().Id.ToString())
		{
		}

		// Token: 0x0600001C RID: 28 RVA: 0x0000285A File Offset: 0x00000A5A
		public ThrottlingRpcClientImpl(string serverName, string processName) : this(serverName, new ThrottlingClientPerformanceCountersImpl(processName + serverName))
		{
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002870 File Offset: 0x00000A70
		public ThrottlingRpcClientImpl(string serverName, IThrottlingClientPerformanceCounters perfCounters)
		{
			if (string.IsNullOrEmpty(serverName))
			{
				throw new ArgumentException("Server name cannot be null or empty", "serverName");
			}
			if (perfCounters == null)
			{
				throw new ArgumentNullException("perfCounters");
			}
			this.serverName = serverName;
			this.syncRoot = new object();
			this.perfCounters = perfCounters;
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600001E RID: 30 RVA: 0x000028C2 File Offset: 0x00000AC2
		public static ExEventLog EventLogger
		{
			get
			{
				return ThrottlingRpcClientImpl.eventLogger;
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000028CC File Offset: 0x00000ACC
		public void Dispose()
		{
			lock (this.syncRoot)
			{
				this.disposed = true;
				if (this.rpcClient != null)
				{
					if (!ThrottlingRpcClientImpl.Unref(this.rpcClient))
					{
						throw new InvalidOperationException(string.Format("Detected extra reference(s) to RPC client instance for server {0}", this.serverName));
					}
					this.rpcClient = null;
				}
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002940 File Offset: 0x00000B40
		public ThrottlingRpcResult ObtainSubmissionTokens(Guid mailboxGuid, int requestedTokenCount, int totalTokenCount, int submissionType)
		{
			this.ThrowIfDisposed();
			ThrottlingRpcClient throttlingRpcClient;
			if (!this.TryGetRpcClient(out throttlingRpcClient))
			{
				ThrottlingRpcClientImpl.tracer.TraceDebug(0L, "Bypassed RPC for mailbox server {0} and mailbox GUID <{1}> because of failure count {2} and last failure time {3}", new object[]
				{
					this.serverName,
					mailboxGuid,
					this.failureCount,
					this.lastFailureTime
				});
				this.perfCounters.AddRequestStatus(ThrottlingRpcResult.Bypassed);
				return ThrottlingRpcResult.Bypassed;
			}
			long requestStartTime = 0L;
			long requestCompletionTime = 0L;
			ThrottlingRpcResult throttlingRpcResult;
			try
			{
				ThrottlingRpcClientImpl.tracer.TraceDebug(0L, "Invoking RPC for mailbox server {0}, mailbox GUID <{1}>, requestedCount {2}, totalCount {3}, submissionType {4}", new object[]
				{
					this.serverName,
					mailboxGuid,
					requestedTokenCount,
					totalTokenCount,
					submissionType
				});
				requestStartTime = Stopwatch.GetTimestamp();
				bool flag = throttlingRpcClient.ObtainSubmissionTokens(mailboxGuid, requestedTokenCount, totalTokenCount, submissionType);
				requestCompletionTime = Stopwatch.GetTimestamp();
				ThrottlingRpcClientImpl.tracer.TraceDebug<bool>(0L, "RPC request succeeded and returned {0}", flag);
				throttlingRpcResult = (flag ? ThrottlingRpcResult.Allowed : ThrottlingRpcResult.Denied);
			}
			catch (RpcException ex)
			{
				requestCompletionTime = Stopwatch.GetTimestamp();
				ThrottlingRpcClientImpl.tracer.TraceError<string, RpcException>(0L, "Exception in RPC request to mailbox server {0}: {1}", this.serverName, ex);
				ThrottlingRpcClientImpl.EventLogger.LogEvent(ThrottlingClientEventLogConstants.Tuple_RpcClientOperationFailure, this.serverName, new object[]
				{
					this.serverName,
					ex.ErrorCode,
					ex,
					(ex.InnerException != null) ? ex.InnerException.ToString() : "none"
				});
				throttlingRpcResult = ThrottlingRpcResult.Failed;
			}
			this.HandleResult(throttlingRpcResult, requestStartTime, requestCompletionTime, throttlingRpcClient);
			return throttlingRpcResult;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002AE8 File Offset: 0x00000CE8
		public ThrottlingRpcResult ObtainTokens(Guid mailboxGuid, RequestedAction requestedAction, int requestedTokenCount, int totalTokenCount, string clientInfo)
		{
			this.ThrowIfDisposed();
			ThrottlingRpcClient throttlingRpcClient;
			if (!this.TryGetRpcClient(out throttlingRpcClient))
			{
				ThrottlingRpcClientImpl.tracer.TraceDebug(0L, "Bypassed RPC for mailbox server {0} and mailbox GUID <{1}> because of failure count {2} and last failure time {3}", new object[]
				{
					this.serverName,
					mailboxGuid,
					this.failureCount,
					this.lastFailureTime
				});
				this.perfCounters.AddRequestStatus(ThrottlingRpcResult.Bypassed);
				return ThrottlingRpcResult.Bypassed;
			}
			long requestStartTime = 0L;
			long requestCompletionTime = 0L;
			ThrottlingRpcResult throttlingRpcResult;
			try
			{
				ThrottlingRpcClientImpl.tracer.TraceDebug(0L, "Invoking RPC for mailbox server {0}, mailbox GUID <{1}>, requestedAction {2}, requestedTokenCount {3}, totalTokenCount {4}", new object[]
				{
					this.serverName,
					mailboxGuid,
					requestedAction,
					requestedTokenCount,
					totalTokenCount
				});
				byte[] inBytes = ThrottlingRpcClientImpl.PackRequest(mailboxGuid, requestedAction, requestedTokenCount, totalTokenCount, clientInfo);
				requestStartTime = Stopwatch.GetTimestamp();
				byte[] responseByteArray = throttlingRpcClient.ObtainTokens(inBytes);
				requestCompletionTime = Stopwatch.GetTimestamp();
				bool flag;
				ThrottlingRpcClientImpl.UnpackResponse(responseByteArray, out flag);
				ThrottlingRpcClientImpl.tracer.TraceDebug<bool>(0L, "RPC request succeeded and returned {0}", flag);
				throttlingRpcResult = (flag ? ThrottlingRpcResult.Allowed : ThrottlingRpcResult.Denied);
			}
			catch (RpcException ex)
			{
				requestCompletionTime = Stopwatch.GetTimestamp();
				ThrottlingRpcClientImpl.tracer.TraceError<string, RpcException>(0L, "Exception in RPC request to mailbox server {0}: {1}", this.serverName, ex);
				ThrottlingRpcClientImpl.EventLogger.LogEvent(ThrottlingClientEventLogConstants.Tuple_RpcClientOperationFailure, this.serverName, new object[]
				{
					this.serverName,
					ex.ErrorCode,
					ex,
					(ex.InnerException != null) ? ex.InnerException.ToString() : "none"
				});
				throttlingRpcResult = ThrottlingRpcResult.Failed;
			}
			this.HandleResult(throttlingRpcResult, requestStartTime, requestCompletionTime, throttlingRpcClient);
			return throttlingRpcResult;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002CA4 File Offset: 0x00000EA4
		private static byte[] PackRequest(Guid mailboxGuid, RequestedAction requestedAction, int requestedTokenCount, int totalTokenCount, string clientInfo)
		{
			MdbefPropertyCollection mdbefPropertyCollection = new MdbefPropertyCollection();
			mdbefPropertyCollection.Add(2415984712U, mailboxGuid);
			mdbefPropertyCollection.Add(2416050179U, (int)requestedAction);
			mdbefPropertyCollection.Add(2416115715U, requestedTokenCount);
			mdbefPropertyCollection.Add(2416181251U, totalTokenCount);
			mdbefPropertyCollection.Add(2416312351U, Environment.MachineName);
			mdbefPropertyCollection.Add(2416377887U, ThrottlingRpcClientImpl.currentProcess.ProcessName);
			if (!string.IsNullOrEmpty(clientInfo))
			{
				mdbefPropertyCollection.Add(2416246815U, clientInfo);
			}
			return mdbefPropertyCollection.GetBytes();
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002D40 File Offset: 0x00000F40
		private static void UnpackResponse(byte[] responseByteArray, out bool allowed)
		{
			MdbefPropertyCollection mdbefPropertyCollection = MdbefPropertyCollection.Create(responseByteArray, 0, responseByteArray.Length);
			allowed = true;
			object obj;
			if (mdbefPropertyCollection.TryGetValue(2566914059U, out obj) && obj is bool)
			{
				allowed = (bool)obj;
			}
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002D7A File Offset: 0x00000F7A
		private static long GetMsecInterval(long t1, long t2)
		{
			return (t2 - t1) / ThrottlingRpcClientImpl.TicksPerMsec;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002D85 File Offset: 0x00000F85
		private static bool Unref(ThrottlingRpcClient client)
		{
			if (client.RemoveRef() == 0)
			{
				client.Dispose();
				return true;
			}
			return false;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002D98 File Offset: 0x00000F98
		private bool TryGetRpcClient(out ThrottlingRpcClient client)
		{
			client = null;
			lock (this.syncRoot)
			{
				if (this.ShouldBypassRpc())
				{
					return false;
				}
				if (this.rpcClient == null)
				{
					this.TryCreateRpcClient();
				}
				if (this.rpcClient != null)
				{
					client = this.rpcClient;
					client.AddRef();
				}
			}
			return client != null;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002E14 File Offset: 0x00001014
		private bool ShouldBypassRpc()
		{
			if (this.failureCount >= 2)
			{
				long msecInterval = ThrottlingRpcClientImpl.GetMsecInterval(this.lastFailureTime, Stopwatch.GetTimestamp());
				if (msecInterval < 60000L)
				{
					ThrottlingRpcClientImpl.EventLogger.LogEvent(ThrottlingClientEventLogConstants.Tuple_RpcRequestBypassed, this.serverName, new object[]
					{
						this.serverName,
						this.failureCount,
						TimeSpan.FromMilliseconds((double)msecInterval)
					});
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002E90 File Offset: 0x00001090
		private bool TryCreateRpcClient()
		{
			try
			{
				ThrottlingRpcClient throttlingRpcClient = new ThrottlingRpcClient(this.serverName);
				ThrottlingRpcClientImpl.tracer.TraceDebug<string>(0L, "Successfully created a new RPC client for server {0}", this.serverName);
				throttlingRpcClient.SetTimeOut(5000);
				ThrottlingRpcClientImpl.tracer.TraceDebug<string>(0L, "Successfully set RPC timeout for server {0}", this.serverName);
				this.rpcClient = throttlingRpcClient;
			}
			catch (RpcException ex)
			{
				this.HandleFailure(Stopwatch.GetTimestamp(), null);
				ThrottlingRpcClientImpl.EventLogger.LogEvent(ThrottlingClientEventLogConstants.Tuple_CreateRpcClientFailure, this.serverName, new object[]
				{
					this.serverName,
					ex.ErrorCode,
					ex.Message,
					(ex.InnerException != null) ? ex.InnerException.ToString() : "none"
				});
				ThrottlingRpcClientImpl.tracer.TraceError<string, RpcException>(0L, "RPC exception when creating an RPC client for server {0}: {1}", this.serverName, ex);
			}
			return this.rpcClient != null;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002F8C File Offset: 0x0000118C
		private void HandleResult(ThrottlingRpcResult rpcResult, long requestStartTime, long requestCompletionTime, ThrottlingRpcClient client)
		{
			long msecInterval = ThrottlingRpcClientImpl.GetMsecInterval(requestStartTime, requestCompletionTime);
			if (rpcResult != ThrottlingRpcResult.Failed && msecInterval <= 5000L)
			{
				this.HandleSuccessfulResponse();
			}
			else if (rpcResult != ThrottlingRpcResult.Failed)
			{
				ThrottlingRpcClientImpl.tracer.TraceError<string, long>(0L, "RPC request to mailbox server {0} took {1} milliseconds and treated as failed", this.serverName, msecInterval);
				ThrottlingRpcClientImpl.EventLogger.LogEvent(ThrottlingClientEventLogConstants.Tuple_RpcRequestTimedout, this.serverName, new object[]
				{
					this.serverName,
					msecInterval
				});
				this.HandleFailure(requestCompletionTime, null);
			}
			else
			{
				this.HandleFailure(requestCompletionTime, client);
			}
			ThrottlingRpcClientImpl.Unref(client);
			this.perfCounters.AddRequestStatus(rpcResult, msecInterval);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x0000302C File Offset: 0x0000122C
		private void HandleSuccessfulResponse()
		{
			if (this.failureCount > 0)
			{
				lock (this.syncRoot)
				{
					this.failureCount = 0;
					this.lastFailureTime = 0L;
				}
			}
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00003084 File Offset: 0x00001284
		private void HandleFailure(long failureTime, ThrottlingRpcClient client)
		{
			lock (this.syncRoot)
			{
				this.failureCount++;
				this.lastFailureTime = failureTime;
				if (client != null && this.rpcClient == client)
				{
					ThrottlingRpcClientImpl.Unref(this.rpcClient);
					this.rpcClient = null;
				}
			}
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000030F8 File Offset: 0x000012F8
		private void ThrowIfDisposed()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(string.Format("RPC client for server {0} has already been disposed", this.serverName));
			}
		}

		// Token: 0x04000016 RID: 22
		public const int MaxConsecutiveFailures = 2;

		// Token: 0x04000017 RID: 23
		private const int ConnectionTimeoutMsec = 5000;

		// Token: 0x04000018 RID: 24
		private const long RpcBypassIntervalMsec = 60000L;

		// Token: 0x04000019 RID: 25
		private const int MaxRequestTimeMsec = 5000;

		// Token: 0x0400001A RID: 26
		private static readonly long TicksPerMsec = Stopwatch.Frequency / 1000L;

		// Token: 0x0400001B RID: 27
		private static Microsoft.Exchange.Diagnostics.Trace tracer = ExTraceGlobals.ThrottlingClientTracer;

		// Token: 0x0400001C RID: 28
		private static ExEventLog eventLogger = new ExEventLog(ThrottlingRpcClientImpl.tracer.Category, "MSExchangeThrottlingClient");

		// Token: 0x0400001D RID: 29
		private static Process currentProcess = Process.GetCurrentProcess();

		// Token: 0x0400001E RID: 30
		private readonly string serverName;

		// Token: 0x0400001F RID: 31
		private ThrottlingRpcClient rpcClient;

		// Token: 0x04000020 RID: 32
		private volatile int failureCount;

		// Token: 0x04000021 RID: 33
		private long lastFailureTime;

		// Token: 0x04000022 RID: 34
		private object syncRoot;

		// Token: 0x04000023 RID: 35
		private IThrottlingClientPerformanceCounters perfCounters;

		// Token: 0x04000024 RID: 36
		private bool disposed;
	}
}
