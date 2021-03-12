using System;
using Microsoft.Exchange.Data.ApplicationLogic.Cafe;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x02000005 RID: 5
	internal class AnonymousAnchorMailbox : AnchorMailbox
	{
		// Token: 0x0600002C RID: 44 RVA: 0x000026FA File Offset: 0x000008FA
		public AnonymousAnchorMailbox(IRequestContext requestContext) : base(AnchorSource.Anonymous, AnonymousAnchorMailbox.AnonymousIdentifier, requestContext)
		{
		}

		// Token: 0x0600002D RID: 45 RVA: 0x0000270A File Offset: 0x0000090A
		public override BackEndServer TryDirectBackEndCalculation()
		{
			return MailboxServerCache.Instance.GetRandomE15Server(base.RequestContext);
		}

		// Token: 0x0400001F RID: 31
		internal static readonly string AnonymousIdentifier = "Anonymous";
	}
}
