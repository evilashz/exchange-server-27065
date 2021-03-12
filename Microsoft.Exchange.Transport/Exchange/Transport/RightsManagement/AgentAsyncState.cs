using System;
using Microsoft.Exchange.Data.Transport;

namespace Microsoft.Exchange.Transport.RightsManagement
{
	// Token: 0x020003D3 RID: 979
	internal class AgentAsyncState
	{
		// Token: 0x06002CC6 RID: 11462 RVA: 0x000B2600 File Offset: 0x000B0800
		internal AgentAsyncState()
		{
		}

		// Token: 0x06002CC7 RID: 11463 RVA: 0x000B2608 File Offset: 0x000B0808
		public AgentAsyncState(AgentAsyncContext agentAsyncContext)
		{
			if (agentAsyncContext == null)
			{
				throw new ArgumentException("AgentAsyncContext");
			}
			this.agentAsyncContext = agentAsyncContext;
		}

		// Token: 0x06002CC8 RID: 11464 RVA: 0x000B2625 File Offset: 0x000B0825
		public virtual void Resume()
		{
			this.agentAsyncContext.Resume();
		}

		// Token: 0x06002CC9 RID: 11465 RVA: 0x000B2632 File Offset: 0x000B0832
		public virtual void Complete()
		{
			this.agentAsyncContext.Complete();
			this.agentAsyncContext = null;
		}

		// Token: 0x04001658 RID: 5720
		private AgentAsyncContext agentAsyncContext;
	}
}
