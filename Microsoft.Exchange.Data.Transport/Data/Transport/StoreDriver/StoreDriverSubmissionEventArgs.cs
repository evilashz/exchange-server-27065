using System;

namespace Microsoft.Exchange.Data.Transport.StoreDriver
{
	// Token: 0x020000AC RID: 172
	internal abstract class StoreDriverSubmissionEventArgs : StoreDriverEventArgs
	{
		// Token: 0x060003CA RID: 970 RVA: 0x00008D51 File Offset: 0x00006F51
		internal StoreDriverSubmissionEventArgs()
		{
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060003CB RID: 971
		public abstract MailItem MailItem { get; }
	}
}
