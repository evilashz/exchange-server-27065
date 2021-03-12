using System;
using System.Windows.Forms;

namespace Microsoft.Exchange.Management.SnapIn
{
	// Token: 0x020002A2 RID: 674
	internal abstract class UIInteractionHandler
	{
		// Token: 0x06001C82 RID: 7298
		public abstract void DoActionSynchronizely(Action<IWin32Window> action);
	}
}
