using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.Exchange.Cluster.Common;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data.HA;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x02000053 RID: 83
	internal abstract class AmMultiNodeRpcMap
	{
		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x0600038E RID: 910 RVA: 0x00013994 File Offset: 0x00011B94
		internal Tuple<AmServerName, Exception>[] ServersStatusWithException
		{
			get
			{
				lock (this.m_locker)
				{
					if (this.m_rpcAttemptMap != null && this.m_rpcAttemptMap.Count > 0)
					{
						return (from x in this.m_rpcAttemptMap
						select new Tuple<AmServerName, Exception>(x.Key, x.Value)).ToArray<Tuple<AmServerName, Exception>>();
					}
				}
				return null;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x0600038F RID: 911 RVA: 0x00013A18 File Offset: 0x00011C18
		protected Trace Tracer
		{
			get
			{
				return ExTraceGlobals.ActiveManagerTracer;
			}
		}

		// Token: 0x06000390 RID: 912 RVA: 0x00013A1F File Offset: 0x00011C1F
		protected AmMultiNodeRpcMap(string traceName)
		{
			this.m_name = traceName;
		}

		// Token: 0x06000391 RID: 913 RVA: 0x00013A49 File Offset: 0x00011C49
		protected AmMultiNodeRpcMap(List<AmServerName> nodeList, string traceName)
		{
			this.m_name = traceName;
			this.Initialize(nodeList);
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000392 RID: 914 RVA: 0x00013A7A File Offset: 0x00011C7A
		public List<AmServerName> ServersToContact
		{
			get
			{
				return this.m_nodeList;
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000393 RID: 915 RVA: 0x00013A82 File Offset: 0x00011C82
		protected bool IsTimedout
		{
			get
			{
				return this.m_isTimedout;
			}
		}

		// Token: 0x06000394 RID: 916 RVA: 0x00013A8C File Offset: 0x00011C8C
		public bool WaitForCompletion(TimeSpan timeout)
		{
			ManualOneShotEvent.Result result = this.m_completionEvent.WaitOne(timeout);
			if (result != ManualOneShotEvent.Result.Success)
			{
				this.Tracer.TraceError<string, string, ManualOneShotEvent.Result>((long)this.GetHashCode(), "{0}: Multinode rpc timedout after {1}. WaitOne Result: {2}", this.m_name, timeout.ToString(), result);
				this.m_isTimedout = true;
				return false;
			}
			return true;
		}

		// Token: 0x06000395 RID: 917 RVA: 0x00013AE0 File Offset: 0x00011CE0
		public Exception GetPossibleExceptionForServer(AmServerName node)
		{
			Exception result = null;
			lock (this.m_locker)
			{
				this.m_rpcAttemptMap.TryGetValue(node, out result);
			}
			return result;
		}

		// Token: 0x06000396 RID: 918 RVA: 0x00013B2C File Offset: 0x00011D2C
		internal virtual void TestInitialState()
		{
			DiagCore.RetailAssert(this.m_completionCount == 0, "m_completionCount should be 0 at the start.", new object[0]);
			DiagCore.RetailAssert(this.m_nodeList == null || this.m_expectedCount == this.m_nodeList.Count, "m_expectedCount should be same as m_nodeList.Count", new object[0]);
			DiagCore.RetailAssert(this.m_rpcAttemptMap != null, "m_rpcAttemptMap should not be null.", new object[0]);
			DiagCore.RetailAssert(this.m_rpcAttemptMap.Count == 0, "m_rpcAttemptMap should have 0 entries.", new object[0]);
		}

		// Token: 0x06000397 RID: 919 RVA: 0x00013BBC File Offset: 0x00011DBC
		internal virtual void TestFinalState()
		{
			DiagCore.RetailAssert(this.IsTimedout || this.m_completionCount == this.m_expectedCount, "m_completionCount should be equal to m_expectedCount.", new object[0]);
			DiagCore.RetailAssert(this.IsTimedout || this.m_rpcAttemptMap.Count == this.m_expectedCount, "m_rpcAttemptMap should have m_expectedCount entries.", new object[0]);
		}

		// Token: 0x06000398 RID: 920 RVA: 0x00013C20 File Offset: 0x00011E20
		protected void Initialize(List<AmServerName> nodeList)
		{
			if (nodeList == null)
			{
				throw new ArgumentNullException("nodeList", "nodeList should not be null!");
			}
			this.m_nodeList = nodeList;
			this.m_expectedCount = nodeList.Count;
			this.m_rpcAttemptMap = new Dictionary<AmServerName, Exception>(nodeList.Count);
			this.Tracer.TraceDebug<string, int>((long)this.GetHashCode(), "{0}: Initializing with m_expectedCount={1}", this.m_name, this.m_expectedCount);
		}

		// Token: 0x06000399 RID: 921 RVA: 0x00013C87 File Offset: 0x00011E87
		protected void RunAllRpcs()
		{
			this.RunAllRpcs(InvokeWithTimeout.InfiniteTimeSpan);
		}

		// Token: 0x0600039A RID: 922 RVA: 0x00013C94 File Offset: 0x00011E94
		protected void RunAllRpcs(TimeSpan timeout)
		{
			if (this.m_nodeList != null && this.m_nodeList.Count > 0)
			{
				ReplayStopwatch replayStopwatch = new ReplayStopwatch();
				replayStopwatch.Start();
				try
				{
					bool flag = false;
					lock (this.m_locker)
					{
						flag = (this.m_rpcAttemptMap.Count == 0);
					}
					if (flag)
					{
						this.Tracer.TraceDebug<string>((long)this.GetHashCode(), "{0}: RunAllRpcs(): Beginning to issue parallel RPCs.", this.m_name);
						List<AmServerName> list = new List<AmServerName>(this.m_nodeList.Count);
						foreach (AmServerName amServerName in this.m_nodeList)
						{
							if (this.TryStartRpc(amServerName))
							{
								list.Add(amServerName);
							}
							else
							{
								this.SkipSingleRpc(amServerName);
							}
						}
						foreach (AmServerName state in list)
						{
							ThreadPool.QueueUserWorkItem(new WaitCallback(this.RunSingleRpc), state);
						}
					}
					this.Tracer.TraceDebug<string>((long)this.GetHashCode(), "{0}: RunAllRpcs(): Waiting for RPCs to complete...", this.m_name);
					ManualOneShotEvent.Result result = this.m_completionEvent.WaitOne(timeout);
					if (!this.m_isTimedout)
					{
						this.m_isTimedout = (result == ManualOneShotEvent.Result.WaitTimedOut);
					}
					this.Tracer.TraceDebug<string, ManualOneShotEvent.Result>((long)this.GetHashCode(), "{0}: RunAllRpcs(): Waiting for RPCs returned: {1}", this.m_name, result);
					if (timeout == InvokeWithTimeout.InfiniteTimeSpan)
					{
						DiagCore.AssertOrWatson(result == ManualOneShotEvent.Result.Success, "waitResult cannot be anything other than Success! Actual: {0}", new object[]
						{
							result
						});
					}
					return;
				}
				finally
				{
					replayStopwatch.Stop();
					this.m_elapsedTime = replayStopwatch.Elapsed;
					this.Tracer.TraceDebug<string, string>((long)this.GetHashCode(), "{0}: RunAllRpcs(): Completed in {1}.", this.m_name, replayStopwatch.ToString());
				}
			}
			this.Tracer.TraceDebug<string>((long)this.GetHashCode(), "{0}: RunAllRpcs(): No servers to contact since m_nodeList is null/empty. Setting completion event.", this.m_name);
			this.m_completionEvent.Set();
			this.m_completionEvent.Close();
		}

		// Token: 0x0600039B RID: 923
		protected abstract Exception RunServerRpc(AmServerName node, out object result);

		// Token: 0x0600039C RID: 924
		protected abstract void UpdateStatus(AmServerName node, object result);

		// Token: 0x0600039D RID: 925 RVA: 0x00013F18 File Offset: 0x00012118
		protected virtual bool TryStartRpc(AmServerName server)
		{
			return true;
		}

		// Token: 0x0600039E RID: 926 RVA: 0x00013F1B File Offset: 0x0001211B
		protected virtual void RecordRpcCompleted(AmServerName server)
		{
		}

		// Token: 0x0600039F RID: 927 RVA: 0x00013F1D File Offset: 0x0001211D
		protected virtual void Cleanup()
		{
			this.m_completionEvent.Close();
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x00013F2C File Offset: 0x0001212C
		private void SkipSingleRpc(AmServerName node)
		{
			this.Tracer.TraceError<string, AmServerName>((long)this.GetHashCode(), "{0}: RunAllRpcs(): Skipping starting another thread to server '{1}' because an RPC thread is already running/hung.", this.m_name, node);
			lock (this.m_locker)
			{
				this.m_skippedCount++;
				this.m_isTimedout = true;
			}
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x00013F9C File Offset: 0x0001219C
		private void RunSingleRpc(object obj)
		{
			AmServerName amServerName = obj as AmServerName;
			lock (this.m_locker)
			{
				this.m_rpcAttemptMap[amServerName] = null;
			}
			ReplayStopwatch replayStopwatch = new ReplayStopwatch();
			Exception ex = null;
			object result;
			try
			{
				this.Tracer.TraceDebug<string, AmServerName>((long)this.GetHashCode(), "{0}: RunSingleRpc: Issuing RPC to server {1}...", this.m_name, amServerName);
				replayStopwatch.Start();
				ex = this.RunServerRpc(amServerName, out result);
			}
			finally
			{
				replayStopwatch.Stop();
				if (ex == null)
				{
					this.Tracer.TraceDebug<string, AmServerName, string>((long)this.GetHashCode(), "{0}: RunSingleRpc: RPC to server {1} completed successfully in {2}.", this.m_name, amServerName, replayStopwatch.ToString());
				}
				else
				{
					this.Tracer.TraceDebug((long)this.GetHashCode(), "{0}: RunSingleRpc: RPC to server {1} completed with error in {2}. Exception: {3}", new object[]
					{
						this.m_name,
						amServerName,
						replayStopwatch.ToString(),
						ex
					});
				}
				this.RecordRpcCompleted(amServerName);
			}
			this.UpdateMap(amServerName, ex, result);
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x000140B4 File Offset: 0x000122B4
		private void UpdateMap(AmServerName node, Exception ex, object result)
		{
			lock (this.m_locker)
			{
				this.m_rpcAttemptMap[node] = ex;
				this.UpdateStatus(node, result);
				this.m_completionCount++;
				if (this.m_completionCount == this.m_expectedCount - this.m_skippedCount)
				{
					this.Tracer.TraceDebug<string>((long)this.GetHashCode(), "{0}: All RPCs completed. Signalling completion event.", this.m_name);
					this.m_completionEvent.Set();
					this.m_completionEvent.Close();
				}
			}
		}

		// Token: 0x0400019A RID: 410
		private const string RpcsCompletedEventName = "RpcsCompletedEvent";

		// Token: 0x0400019B RID: 411
		protected ManualOneShotEvent m_completionEvent = new ManualOneShotEvent("RpcsCompletedEvent");

		// Token: 0x0400019C RID: 412
		protected Dictionary<AmServerName, Exception> m_rpcAttemptMap;

		// Token: 0x0400019D RID: 413
		protected int m_expectedCount;

		// Token: 0x0400019E RID: 414
		protected TimeSpan m_elapsedTime;

		// Token: 0x0400019F RID: 415
		protected string m_name;

		// Token: 0x040001A0 RID: 416
		private object m_locker = new object();

		// Token: 0x040001A1 RID: 417
		private int m_completionCount;

		// Token: 0x040001A2 RID: 418
		private int m_skippedCount;

		// Token: 0x040001A3 RID: 419
		private List<AmServerName> m_nodeList;

		// Token: 0x040001A4 RID: 420
		private bool m_isTimedout;
	}
}
