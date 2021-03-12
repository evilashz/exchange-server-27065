using System;

namespace Microsoft.Exchange.RpcClientAccess.Diagnostics
{
	// Token: 0x0200003C RID: 60
	internal sealed class RpcAttemptedCounters : IRpcCounters
	{
		// Token: 0x0600024F RID: 591 RVA: 0x00008855 File Offset: 0x00006A55
		public void IncrementCounter(IRpcCounterData counterData)
		{
			this.rpcAttemptedCounter++;
		}

		// Token: 0x06000250 RID: 592 RVA: 0x00008865 File Offset: 0x00006A65
		public override string ToString()
		{
			return string.Format("R={0}", this.rpcAttemptedCounter);
		}

		// Token: 0x040001DB RID: 475
		private int rpcAttemptedCounter;
	}
}
