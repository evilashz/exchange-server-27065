using System;
using System.Windows.Forms;

namespace Microsoft.Exchange.Management.SnapIn
{
	// Token: 0x020002A3 RID: 675
	internal sealed class WindowUIInteractionHandler : UIInteractionHandler
	{
		// Token: 0x06001C84 RID: 7300 RVA: 0x0007B6E0 File Offset: 0x000798E0
		public WindowUIInteractionHandler(Control window)
		{
			if (window == null)
			{
				throw new ArgumentNullException("window");
			}
			this.window = window;
		}

		// Token: 0x06001C85 RID: 7301 RVA: 0x0007B700 File Offset: 0x00079900
		public override void DoActionSynchronizely(Action<IWin32Window> action)
		{
			if (this.window.InvokeRequired)
			{
				this.window.Invoke(action, new object[]
				{
					this.window
				});
				return;
			}
			action(this.window);
		}

		// Token: 0x04000AA8 RID: 2728
		private Control window;
	}
}
