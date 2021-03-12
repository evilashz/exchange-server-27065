using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x0200022F RID: 559
	internal sealed class RecipientCacheTransaction : DisposeTrackableBase
	{
		// Token: 0x060012DB RID: 4827 RVA: 0x00071AB0 File Offset: 0x0006FCB0
		public RecipientCacheTransaction(string configurationName, UserContext userContext)
		{
			using (DisposeGuard disposeGuard = this.Guard())
			{
				if (userContext.CanActAsOwner)
				{
					this.configuration = UserConfigurationUtilities.GetUserConfiguration(configurationName, userContext);
				}
				disposeGuard.Success();
			}
		}

		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x060012DC RID: 4828 RVA: 0x00071B08 File Offset: 0x0006FD08
		public UserConfiguration Configuration
		{
			get
			{
				return this.configuration;
			}
		}

		// Token: 0x060012DD RID: 4829 RVA: 0x00071B10 File Offset: 0x0006FD10
		protected override void InternalDispose(bool isDisposing)
		{
			if (!this.disposed)
			{
				if (isDisposing && this.configuration != null)
				{
					this.configuration.Dispose();
				}
				this.disposed = true;
			}
		}

		// Token: 0x060012DE RID: 4830 RVA: 0x00071B37 File Offset: 0x0006FD37
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<RecipientCacheTransaction>(this);
		}

		// Token: 0x04000CF5 RID: 3317
		private UserConfiguration configuration;

		// Token: 0x04000CF6 RID: 3318
		private bool disposed;
	}
}
