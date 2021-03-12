using System;
using System.Collections.Generic;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x0200009A RID: 154
	internal sealed class CallbackWrappers
	{
		// Token: 0x0600070C RID: 1804 RVA: 0x00010530 File Offset: 0x0000E730
		public JetCallbackWrapper Add(JET_CALLBACK callback)
		{
			JetCallbackWrapper result;
			lock (this.lockObject)
			{
				JetCallbackWrapper jetCallbackWrapper;
				if (!this.TryFindWrapperFor(callback, out jetCallbackWrapper))
				{
					jetCallbackWrapper = new JetCallbackWrapper(callback);
					this.callbackWrappers.Add(jetCallbackWrapper);
				}
				result = jetCallbackWrapper;
			}
			return result;
		}

		// Token: 0x0600070D RID: 1805 RVA: 0x00010598 File Offset: 0x0000E798
		public void Collect()
		{
			lock (this.lockObject)
			{
				this.callbackWrappers.RemoveAll((JetCallbackWrapper wrapper) => !wrapper.IsAlive);
			}
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x000105FC File Offset: 0x0000E7FC
		private bool TryFindWrapperFor(JET_CALLBACK callback, out JetCallbackWrapper wrapper)
		{
			foreach (JetCallbackWrapper jetCallbackWrapper in this.callbackWrappers)
			{
				if (jetCallbackWrapper.IsWrapping(callback))
				{
					wrapper = jetCallbackWrapper;
					return true;
				}
			}
			wrapper = null;
			return false;
		}

		// Token: 0x04000316 RID: 790
		private readonly object lockObject = new object();

		// Token: 0x04000317 RID: 791
		private readonly List<JetCallbackWrapper> callbackWrappers = new List<JetCallbackWrapper>();
	}
}
