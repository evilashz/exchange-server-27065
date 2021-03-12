using System;
using Microsoft.Exchange.Data.ApplicationLogic.Cafe;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x02000093 RID: 147
	internal class MailboxServerLocatorAsyncState
	{
		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000453 RID: 1107 RVA: 0x00019DB6 File Offset: 0x00017FB6
		// (set) Token: 0x06000454 RID: 1108 RVA: 0x00019DBE File Offset: 0x00017FBE
		public ProxyRequestHandler ProxyRequestHandler { get; set; }

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000455 RID: 1109 RVA: 0x00019DC7 File Offset: 0x00017FC7
		// (set) Token: 0x06000456 RID: 1110 RVA: 0x00019DCF File Offset: 0x00017FCF
		public AnchorMailbox AnchorMailbox { get; set; }

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000457 RID: 1111 RVA: 0x00019DD8 File Offset: 0x00017FD8
		// (set) Token: 0x06000458 RID: 1112 RVA: 0x00019DE0 File Offset: 0x00017FE0
		public MailboxServerLocator Locator { get; set; }
	}
}
