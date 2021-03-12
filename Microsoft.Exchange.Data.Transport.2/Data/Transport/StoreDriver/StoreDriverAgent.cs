using System;

namespace Microsoft.Exchange.Data.Transport.StoreDriver
{
	// Token: 0x0200009E RID: 158
	internal abstract class StoreDriverAgent : Agent
	{
		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000389 RID: 905 RVA: 0x00008783 File Offset: 0x00006983
		// (set) Token: 0x0600038A RID: 906 RVA: 0x0000878B File Offset: 0x0000698B
		internal override object HostState
		{
			get
			{
				return base.HostStateInternal;
			}
			set
			{
				base.HostStateInternal = value;
				((SmtpServer)base.HostStateInternal).AssociatedAgent = this;
			}
		}

		// Token: 0x0600038B RID: 907 RVA: 0x000087A5 File Offset: 0x000069A5
		internal override void AsyncComplete()
		{
			((SmtpServer)this.HostState).AddressBook.RecipientCache = null;
		}
	}
}
