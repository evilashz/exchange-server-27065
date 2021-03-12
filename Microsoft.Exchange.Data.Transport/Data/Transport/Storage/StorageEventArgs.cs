using System;

namespace Microsoft.Exchange.Data.Transport.Storage
{
	// Token: 0x02000095 RID: 149
	public abstract class StorageEventArgs : EventArgs
	{
		// Token: 0x0600036F RID: 879 RVA: 0x0000877B File Offset: 0x0000697B
		internal StorageEventArgs()
		{
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000370 RID: 880
		public abstract MailItem MailItem { get; }
	}
}
