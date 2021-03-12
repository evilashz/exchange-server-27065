using System;
using Microsoft.Exchange.InstantMessaging;
using Microsoft.Exchange.Services;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200012E RID: 302
	internal class DeliverySuccessNotification
	{
		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06000A16 RID: 2582 RVA: 0x0002328A File Offset: 0x0002148A
		// (set) Token: 0x06000A17 RID: 2583 RVA: 0x00023292 File Offset: 0x00021492
		internal InstantMessageOCSProvider Provider { get; private set; }

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06000A18 RID: 2584 RVA: 0x0002329B File Offset: 0x0002149B
		// (set) Token: 0x06000A19 RID: 2585 RVA: 0x000232A3 File Offset: 0x000214A3
		internal IIMModality Context { get; private set; }

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06000A1A RID: 2586 RVA: 0x000232AC File Offset: 0x000214AC
		// (set) Token: 0x06000A1B RID: 2587 RVA: 0x000232B4 File Offset: 0x000214B4
		internal int MessageId { get; private set; }

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06000A1C RID: 2588 RVA: 0x000232BD File Offset: 0x000214BD
		// (set) Token: 0x06000A1D RID: 2589 RVA: 0x000232C5 File Offset: 0x000214C5
		internal RequestDetailsLogger Logger { get; private set; }

		// Token: 0x06000A1E RID: 2590 RVA: 0x000232CE File Offset: 0x000214CE
		internal DeliverySuccessNotification(InstantMessageOCSProvider provider, IIMModality context, int messageId, RequestDetailsLogger logger)
		{
			this.Provider = provider;
			this.Context = context;
			this.MessageId = messageId;
			this.Logger = logger;
		}
	}
}
