using System;
using Microsoft.Exchange.Search.Core.Abstraction;
using Microsoft.Exchange.Search.Core.Common;

namespace Microsoft.Exchange.Search.Core.AsyncTask
{
	// Token: 0x0200004B RID: 75
	internal sealed class AsyncStart : AsyncTask
	{
		// Token: 0x06000170 RID: 368 RVA: 0x00002DB3 File Offset: 0x00000FB3
		internal AsyncStart(IStartStop component)
		{
			Util.ThrowOnNullArgument(component, "component");
			this.component = component;
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00002DCD File Offset: 0x00000FCD
		public override string ToString()
		{
			return string.Format("AsyncStart for {0}", this.component);
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00002E1C File Offset: 0x0000101C
		internal override void InternalExecute()
		{
			this.component.BeginStart(delegate(IAsyncResult ar)
			{
				ComponentException exception = null;
				try
				{
					this.component.EndStart(ar);
				}
				catch (ComponentException ex)
				{
					exception = ex;
				}
				base.Complete(exception);
			}, null);
		}

		// Token: 0x04000094 RID: 148
		private readonly IStartStop component;
	}
}
