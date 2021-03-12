using System;
using Microsoft.Exchange.Search.Core.Abstraction;
using Microsoft.Exchange.Search.Core.Common;

namespace Microsoft.Exchange.Search.Core.AsyncTask
{
	// Token: 0x0200004A RID: 74
	internal sealed class AsyncPrepareToStart : AsyncTask
	{
		// Token: 0x0600016C RID: 364 RVA: 0x00002D30 File Offset: 0x00000F30
		internal AsyncPrepareToStart(IStartStop component)
		{
			Util.ThrowOnNullArgument(component, "component");
			this.component = component;
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00002D4A File Offset: 0x00000F4A
		public override string ToString()
		{
			return string.Format("AsyncPrepareToStart for {0}", this.component);
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00002D98 File Offset: 0x00000F98
		internal override void InternalExecute()
		{
			this.component.BeginPrepareToStart(delegate(IAsyncResult ar)
			{
				ComponentException exception = null;
				try
				{
					this.component.EndPrepareToStart(ar);
				}
				catch (ComponentException ex)
				{
					exception = ex;
				}
				base.Complete(exception);
			}, null);
		}

		// Token: 0x04000093 RID: 147
		private readonly IStartStop component;
	}
}
