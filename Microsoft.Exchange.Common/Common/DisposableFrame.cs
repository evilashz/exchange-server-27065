using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Common
{
	// Token: 0x02000005 RID: 5
	public class DisposableFrame : DisposeTrackableBase
	{
		// Token: 0x06000015 RID: 21 RVA: 0x000022E0 File Offset: 0x000004E0
		public DisposableFrame(Action onDispose)
		{
			this.onDispose = onDispose;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000022EF File Offset: 0x000004EF
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<DisposableFrame>(this);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000022F7 File Offset: 0x000004F7
		protected override void InternalDispose(bool disposing)
		{
			if (disposing && this.onDispose != null)
			{
				this.onDispose();
			}
		}

		// Token: 0x04000006 RID: 6
		private Action onDispose;
	}
}
