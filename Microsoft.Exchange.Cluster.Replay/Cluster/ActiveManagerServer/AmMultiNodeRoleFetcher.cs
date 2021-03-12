using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data.HA;
using Microsoft.Exchange.Rpc.ActiveManager;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x02000058 RID: 88
	internal class AmMultiNodeRoleFetcher : AmMultiNodeRpcMap
	{
		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060003D9 RID: 985 RVA: 0x00014CFD File Offset: 0x00012EFD
		internal Dictionary<AmServerName, AmRole> RoleMap
		{
			get
			{
				return this.roleMap;
			}
		}

		// Token: 0x060003DA RID: 986 RVA: 0x00014D05 File Offset: 0x00012F05
		internal AmMultiNodeRoleFetcher(List<AmServerName> serverList, TimeSpan timeout, bool isCompleteOnMajority) : base(serverList, "AmMultiNodeRoleFetcher")
		{
			this.timeout = timeout;
		}

		// Token: 0x060003DB RID: 987 RVA: 0x00014DA0 File Offset: 0x00012FA0
		protected override Exception RunServerRpc(AmServerName node, out object result)
		{
			Exception exception = null;
			result = null;
			AmRole? role = null;
			try
			{
				InvokeWithTimeout.Invoke(delegate()
				{
					string errorMessage = null;
					exception = AmHelper.HandleKnownExceptions(delegate(object param0, EventArgs param1)
					{
						role = new AmRole?(Dependencies.AmRpcClientWrapper.GetActiveManagerRole(node.Fqdn, out errorMessage));
					});
				}, this.timeout);
			}
			catch (TimeoutException exception)
			{
				TimeoutException exception2;
				exception = exception2;
			}
			if (role != null)
			{
				Interlocked.Increment(ref this.successCount);
				result = role;
			}
			return exception;
		}

		// Token: 0x060003DC RID: 988 RVA: 0x00014E38 File Offset: 0x00013038
		protected override void UpdateStatus(AmServerName node, object result)
		{
			if (result != null)
			{
				this.roleMap[node] = (AmRole)result;
			}
		}

		// Token: 0x060003DD RID: 989 RVA: 0x00014E50 File Offset: 0x00013050
		internal bool IsMajoritySuccessfulRepliesReceived(out int totalServerCount, out int successServerCount)
		{
			bool result = false;
			totalServerCount = this.m_expectedCount;
			successServerCount = this.successCount;
			int num = totalServerCount / 2 + 1;
			if (successServerCount >= num)
			{
				result = true;
			}
			return result;
		}

		// Token: 0x060003DE RID: 990 RVA: 0x00014E7E File Offset: 0x0001307E
		public void Run()
		{
			base.RunAllRpcs();
		}

		// Token: 0x040001BE RID: 446
		private readonly TimeSpan timeout;

		// Token: 0x040001BF RID: 447
		private int successCount;

		// Token: 0x040001C0 RID: 448
		private Dictionary<AmServerName, AmRole> roleMap = new Dictionary<AmServerName, AmRole>();
	}
}
