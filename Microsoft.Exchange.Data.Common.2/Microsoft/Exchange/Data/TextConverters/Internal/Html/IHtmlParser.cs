using System;

namespace Microsoft.Exchange.Data.TextConverters.Internal.Html
{
	// Token: 0x020001FF RID: 511
	internal interface IHtmlParser
	{
		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x06001509 RID: 5385
		HtmlToken Token { get; }

		// Token: 0x0600150A RID: 5386
		HtmlTokenId Parse();

		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x0600150B RID: 5387
		int CurrentOffset { get; }

		// Token: 0x0600150C RID: 5388
		void SetRestartConsumer(IRestartable restartConsumer);
	}
}
