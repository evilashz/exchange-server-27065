using System;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x020000BC RID: 188
	internal class MailboxConnectionArgs : EventArgs
	{
		// Token: 0x06000680 RID: 1664 RVA: 0x00019314 File Offset: 0x00017514
		internal MailboxConnectionArgs(bool connected)
		{
			this.SuccessfulConnection = connected;
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000681 RID: 1665 RVA: 0x00019323 File Offset: 0x00017523
		// (set) Token: 0x06000682 RID: 1666 RVA: 0x0001932B File Offset: 0x0001752B
		internal bool SuccessfulConnection { get; private set; }
	}
}
