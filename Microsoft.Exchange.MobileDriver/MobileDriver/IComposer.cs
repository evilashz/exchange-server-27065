using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.TextMessaging.MobileDriver
{
	// Token: 0x02000005 RID: 5
	internal interface IComposer
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000018 RID: 24
		PartType PartType { get; }

		// Token: 0x06000019 RID: 25
		BookmarkRetriever Compose(IList<ProportionedText> proportionedTexts);
	}
}
