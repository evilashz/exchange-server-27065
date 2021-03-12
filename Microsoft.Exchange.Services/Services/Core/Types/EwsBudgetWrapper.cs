using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Mapi.Unmanaged;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020003DC RID: 988
	internal class EwsBudgetWrapper : StandardBudgetWrapper, IEwsBudget, IStandardBudget, IBudget, IDisposable
	{
		// Token: 0x06001BB4 RID: 7092 RVA: 0x0009D432 File Offset: 0x0009B632
		public EwsBudgetWrapper(EwsBudget innerBudget) : base(innerBudget)
		{
			this.initGen0Collections = GC.CollectionCount(0);
			this.initGen1Collections = GC.CollectionCount(1);
			this.initGen2Collections = GC.CollectionCount(2);
			this.connectionCostType = EwsBudget.GetConnectionCostType();
			this.LogBudgetToIIS(true);
		}

		// Token: 0x06001BB5 RID: 7093 RVA: 0x0009D474 File Offset: 0x0009B674
		public bool SleepIfNecessary()
		{
			int num;
			float num2;
			return this.SleepIfNecessary(out num, out num2);
		}

		// Token: 0x06001BB6 RID: 7094 RVA: 0x0009D48B File Offset: 0x0009B68B
		public bool SleepIfNecessary(out int sleepTime, out float cpuPercent)
		{
			if (CPUBasedSleeper.SleepIfNecessary(out sleepTime, out cpuPercent))
			{
				ThrottlingLogInfo.AddDataPoint(sleepTime, cpuPercent);
				return true;
			}
			return false;
		}

		// Token: 0x06001BB7 RID: 7095 RVA: 0x0009D4A4 File Offset: 0x0009B6A4
		public void LogEndStateToIIS()
		{
			this.SetContextItemIfNotAlreadySet("TotalLdapRequestCount", this.TotalLdapRequestCount);
			this.SetContextItemIfNotAlreadySet("TotalLdapRequestLatency", this.TotalLdapRequestLatency);
			this.SetContextItemIfNotAlreadySet("TotalRpcRequestCount", this.TotalRpcRequestCount);
			this.SetContextItemIfNotAlreadySet("TotalRpcRequestLatency", this.TotalRpcRequestLatency);
			this.LogBudgetToIIS(false);
		}

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06001BB8 RID: 7096 RVA: 0x0009D510 File Offset: 0x0009B710
		public uint TotalRpcRequestCount
		{
			get
			{
				return this.totalRpcRequestCount;
			}
		}

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06001BB9 RID: 7097 RVA: 0x0009D518 File Offset: 0x0009B718
		public ulong TotalRpcRequestLatency
		{
			get
			{
				return this.totalRpcRequestLatencyInTicks / 10000UL;
			}
		}

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06001BBA RID: 7098 RVA: 0x0009D527 File Offset: 0x0009B727
		public uint TotalLdapRequestCount
		{
			get
			{
				return this.totalLdapRequestCount;
			}
		}

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x06001BBB RID: 7099 RVA: 0x0009D52F File Offset: 0x0009B72F
		public long TotalLdapRequestLatency
		{
			get
			{
				return this.totalLdapRequestLatencyInMsec;
			}
		}

		// Token: 0x06001BBC RID: 7100 RVA: 0x0009D537 File Offset: 0x0009B737
		public bool TryIncrementFoundObjectCount(uint foundCount, out int maxPossible)
		{
			if (this.CanAllocateFoundObjects(foundCount, out maxPossible))
			{
				this.foundObjects += foundCount;
				return true;
			}
			return false;
		}

		// Token: 0x06001BBD RID: 7101 RVA: 0x0009D554 File Offset: 0x0009B754
		public bool CanAllocateFoundObjects(uint foundCount, out int maxPossible)
		{
			int findCountLimit = Global.FindCountLimit;
			ExTraceGlobals.FaultInjectionTracer.TraceTest<int>(3913690429U, ref findCountLimit);
			int num = findCountLimit - (int)this.foundObjects;
			if ((ulong)foundCount <= (ulong)((long)num))
			{
				maxPossible = (int)foundCount;
				return true;
			}
			maxPossible = Math.Max(0, num);
			return false;
		}

		// Token: 0x06001BBE RID: 7102 RVA: 0x0009D596 File Offset: 0x0009B796
		public void StartPerformanceContext()
		{
			NativeMethods.GetTLSPerformanceContext(out this.rpcPerformanceContext);
			this.ldapPerformanceContext = new PerformanceContext(PerformanceContext.Current);
			this.perfDataThreadId = Environment.CurrentManagedThreadId;
		}

		// Token: 0x06001BBF RID: 7103 RVA: 0x0009D5C0 File Offset: 0x0009B7C0
		public void StopPerformanceContext()
		{
			if (Environment.CurrentManagedThreadId == this.perfDataThreadId)
			{
				PerformanceContext performanceContext;
				if (NativeMethods.GetTLSPerformanceContext(out performanceContext))
				{
					this.totalRpcRequestCount += performanceContext.rpcCount - this.rpcPerformanceContext.rpcCount;
					this.totalRpcRequestLatencyInTicks += performanceContext.rpcLatency - this.rpcPerformanceContext.rpcLatency;
				}
				if (this.ldapPerformanceContext != null)
				{
					this.totalLdapRequestCount += PerformanceContext.Current.RequestCount - this.ldapPerformanceContext.RequestCount;
					this.totalLdapRequestLatencyInMsec += (long)(PerformanceContext.Current.RequestLatency - this.ldapPerformanceContext.RequestLatency);
				}
				this.perfDataThreadId = -1;
			}
		}

		// Token: 0x06001BC0 RID: 7104 RVA: 0x0009D67C File Offset: 0x0009B87C
		protected override CostHandle InternalStartConnection(string callerInfo)
		{
			EwsBudget ewsBudget = (EwsBudget)base.GetInnerBudget();
			if (this.connectionCostType != CostType.Connection)
			{
				return ewsBudget.StartHangingConnection(new Action<CostHandle>(base.HandleCostHandleRelease));
			}
			return ewsBudget.StartConnection(new Action<CostHandle>(base.HandleCostHandleRelease), callerInfo);
		}

		// Token: 0x06001BC1 RID: 7105 RVA: 0x0009D6C4 File Offset: 0x0009B8C4
		private string GetGCInfo()
		{
			int num = GC.CollectionCount(0) - this.initGen0Collections;
			int num2 = GC.CollectionCount(1) - this.initGen1Collections;
			int num3 = GC.CollectionCount(2) - this.initGen2Collections;
			if (num == 0 && num2 == 0 && num3 == 0)
			{
				return string.Empty;
			}
			return string.Format(";GC:{0}/{1}/{2};", num, num2, num3);
		}

		// Token: 0x06001BC2 RID: 7106 RVA: 0x0009D728 File Offset: 0x0009B928
		private void LogBudgetToIIS(bool isInit)
		{
			if (Global.WriteRequestDetailsToLog && CallContext.Current != null && CallContext.Current.HttpContext != null && CallContext.Current.HttpContext.Items != null)
			{
				string key = isInit ? "StartBudget" : "EndBudget";
				string text = this.ToString();
				if (!isInit)
				{
					text += this.GetGCInfo();
				}
				CallContext.Current.HttpContext.Items[key] = text;
			}
		}

		// Token: 0x06001BC3 RID: 7107 RVA: 0x0009D7A0 File Offset: 0x0009B9A0
		private void SetContextItemIfNotAlreadySet(string key, object value)
		{
			if (CallContext.Current != null && CallContext.Current.HttpContext != null && CallContext.Current.HttpContext.Items != null && CallContext.Current.HttpContext.Items[key] == null)
			{
				CallContext.Current.HttpContext.Items[key] = value;
			}
		}

		// Token: 0x06001BC4 RID: 7108 RVA: 0x0009D7FE File Offset: 0x0009B9FE
		protected override StandardBudget ReacquireBudget()
		{
			return EwsBudgetCache.Singleton.Get(base.Owner);
		}

		// Token: 0x04001224 RID: 4644
		private readonly int initGen0Collections;

		// Token: 0x04001225 RID: 4645
		private readonly int initGen1Collections;

		// Token: 0x04001226 RID: 4646
		private readonly int initGen2Collections;

		// Token: 0x04001227 RID: 4647
		private CostType connectionCostType;

		// Token: 0x04001228 RID: 4648
		private uint foundObjects;

		// Token: 0x04001229 RID: 4649
		private int perfDataThreadId;

		// Token: 0x0400122A RID: 4650
		private PerformanceContext rpcPerformanceContext;

		// Token: 0x0400122B RID: 4651
		private PerformanceContext ldapPerformanceContext;

		// Token: 0x0400122C RID: 4652
		private uint totalRpcRequestCount;

		// Token: 0x0400122D RID: 4653
		private ulong totalRpcRequestLatencyInTicks;

		// Token: 0x0400122E RID: 4654
		private uint totalLdapRequestCount;

		// Token: 0x0400122F RID: 4655
		private long totalLdapRequestLatencyInMsec;
	}
}
