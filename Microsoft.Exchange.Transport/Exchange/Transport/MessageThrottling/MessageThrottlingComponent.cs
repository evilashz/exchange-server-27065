using System;

namespace Microsoft.Exchange.Transport.MessageThrottling
{
	// Token: 0x0200012D RID: 301
	internal class MessageThrottlingComponent : ITransportComponent
	{
		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x06000D69 RID: 3433 RVA: 0x00030E8D File Offset: 0x0002F08D
		public MessageThrottlingManager MessageThrottlingManager
		{
			get
			{
				this.ThrowIfNotLoaded();
				return this.messageThrottlingManager;
			}
		}

		// Token: 0x06000D6A RID: 3434 RVA: 0x00030E9B File Offset: 0x0002F09B
		public void Load()
		{
			this.ThrowIfLoaded();
			this.messageThrottlingManager = new MessageThrottlingManager();
		}

		// Token: 0x06000D6B RID: 3435 RVA: 0x00030EAE File Offset: 0x0002F0AE
		public void Unload()
		{
			this.ThrowIfNotLoaded();
			this.messageThrottlingManager = null;
		}

		// Token: 0x06000D6C RID: 3436 RVA: 0x00030EBD File Offset: 0x0002F0BD
		public string OnUnhandledException(Exception e)
		{
			return null;
		}

		// Token: 0x06000D6D RID: 3437 RVA: 0x00030EC0 File Offset: 0x0002F0C0
		private void ThrowIfLoaded()
		{
			if (this.messageThrottlingManager != null)
			{
				throw new InvalidOperationException("Message Throttling Component is already loaded.");
			}
		}

		// Token: 0x06000D6E RID: 3438 RVA: 0x00030ED5 File Offset: 0x0002F0D5
		private void ThrowIfNotLoaded()
		{
			if (this.messageThrottlingManager == null)
			{
				throw new InvalidOperationException("Message Throttling Component is not loaded.");
			}
		}

		// Token: 0x040005C6 RID: 1478
		private MessageThrottlingManager messageThrottlingManager;
	}
}
