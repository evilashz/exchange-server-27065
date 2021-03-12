using System;
using System.Threading;
using System.Windows.Forms;

namespace Microsoft.Exchange.Management.SnapIn
{
	// Token: 0x020002A4 RID: 676
	internal sealed class SyncContextUIInteractionHandler : UIInteractionHandler
	{
		// Token: 0x06001C86 RID: 7302 RVA: 0x0007B745 File Offset: 0x00079945
		public SyncContextUIInteractionHandler(SynchronizationContext uiSyncContext)
		{
			if (uiSyncContext == null)
			{
				throw new ArgumentNullException("uiSyncContext");
			}
			this.uiSyncContext = uiSyncContext;
		}

		// Token: 0x06001C87 RID: 7303 RVA: 0x0007B778 File Offset: 0x00079978
		public override void DoActionSynchronizely(Action<IWin32Window> action)
		{
			this.uiSyncContext.Send(delegate(object param0)
			{
				action(null);
			}, null);
		}

		// Token: 0x04000AA9 RID: 2729
		private SynchronizationContext uiSyncContext;
	}
}
