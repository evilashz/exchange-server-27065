using System;
using System.ComponentModel;
using System.Threading;

namespace Microsoft.Exchange.Management.SnapIn
{
	// Token: 0x0200029E RID: 670
	internal sealed class SynchronizeInvokeSynchronizationContext : SynchronizationContext
	{
		// Token: 0x06001C60 RID: 7264 RVA: 0x0007AFAC File Offset: 0x000791AC
		public SynchronizeInvokeSynchronizationContext(ISynchronizeInvoke syncInvoke)
		{
			this.syncInvoke = syncInvoke;
		}

		// Token: 0x06001C61 RID: 7265 RVA: 0x0007AFBC File Offset: 0x000791BC
		public override void Send(SendOrPostCallback d, object state)
		{
			this.syncInvoke.Invoke(d, new object[]
			{
				state
			});
		}

		// Token: 0x06001C62 RID: 7266 RVA: 0x0007AFE4 File Offset: 0x000791E4
		public override void Post(SendOrPostCallback d, object state)
		{
			this.syncInvoke.BeginInvoke(d, new object[]
			{
				state
			});
		}

		// Token: 0x06001C63 RID: 7267 RVA: 0x0007B00A File Offset: 0x0007920A
		public override SynchronizationContext CreateCopy()
		{
			return new SynchronizeInvokeSynchronizationContext(this.syncInvoke);
		}

		// Token: 0x04000A95 RID: 2709
		private ISynchronizeInvoke syncInvoke;
	}
}
