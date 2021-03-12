using System;
using Microsoft.Exchange.Search.Core.Abstraction;
using Microsoft.Exchange.Search.Core.Common;

namespace Microsoft.Exchange.Search.Core.AsyncTask
{
	// Token: 0x0200004C RID: 76
	internal sealed class AsyncStop : AsyncTask
	{
		// Token: 0x06000174 RID: 372 RVA: 0x00002E37 File Offset: 0x00001037
		internal AsyncStop(IStartStop component)
		{
			Util.ThrowOnNullArgument(component, "component");
			this.component = component;
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00002E51 File Offset: 0x00001051
		public override string ToString()
		{
			return string.Format("AsyncStop for {0}", this.component);
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00002EA0 File Offset: 0x000010A0
		internal override void InternalExecute()
		{
			this.component.BeginStop(delegate(IAsyncResult ar)
			{
				ComponentException exception = null;
				try
				{
					this.component.EndStop(ar);
				}
				catch (ComponentException ex)
				{
					exception = ex;
				}
				base.Complete(exception);
			}, null);
		}

		// Token: 0x04000095 RID: 149
		private readonly IStartStop component;
	}
}
