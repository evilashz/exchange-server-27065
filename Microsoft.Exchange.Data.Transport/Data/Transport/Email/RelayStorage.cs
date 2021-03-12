using System;
using System.IO;
using Microsoft.Exchange.Data.Internal;

namespace Microsoft.Exchange.Data.Transport.Email
{
	// Token: 0x02000106 RID: 262
	internal class RelayStorage : DataStorage
	{
		// Token: 0x06000804 RID: 2052 RVA: 0x0001D1AE File Offset: 0x0001B3AE
		internal RelayStorage(IRelayable relayable)
		{
			this.relayable = relayable;
		}

		// Token: 0x06000805 RID: 2053 RVA: 0x0001D1BD File Offset: 0x0001B3BD
		public override Stream OpenReadStream(long start, long end)
		{
			base.ThrowIfDisposed();
			if (this.temporaryStorage == null)
			{
				this.Relay();
			}
			return this.temporaryStorage.OpenReadStream(start, end);
		}

		// Token: 0x06000806 RID: 2054 RVA: 0x0001D1E0 File Offset: 0x0001B3E0
		internal override void SetReadOnly(bool makeReadOnly)
		{
			base.ThrowIfDisposed();
			base.SetReadOnly(makeReadOnly);
			if (this.temporaryStorage != null)
			{
				this.temporaryStorage.SetReadOnly(makeReadOnly);
			}
		}

		// Token: 0x06000807 RID: 2055 RVA: 0x0001D203 File Offset: 0x0001B403
		protected override void Dispose(bool disposing)
		{
			if (disposing && !base.IsDisposed && this.relayable != null)
			{
				this.Invalidate();
				this.relayable = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x0001D22C File Offset: 0x0001B42C
		internal void Invalidate()
		{
			base.ThrowIfDisposed();
			if (this.temporaryStorage != null)
			{
				this.temporaryStorage.Release();
				this.temporaryStorage = null;
			}
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x0001D24E File Offset: 0x0001B44E
		internal void PermanentlyRelay()
		{
			base.ThrowIfDisposed();
			if (this.temporaryStorage == null)
			{
				this.Relay();
			}
			this.relayable = null;
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x0001D26C File Offset: 0x0001B46C
		private void Relay()
		{
			this.temporaryStorage = new TemporaryDataStorage();
			using (Stream stream = this.temporaryStorage.OpenWriteStream(true))
			{
				this.relayable.WriteTo(stream);
			}
			this.temporaryStorage.SetReadOnly(base.IsReadOnly);
		}

		// Token: 0x04000464 RID: 1124
		private TemporaryDataStorage temporaryStorage;

		// Token: 0x04000465 RID: 1125
		private IRelayable relayable;
	}
}
