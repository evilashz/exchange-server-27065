using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.TextConverters;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x0200015A RID: 346
	internal class HtmlBodyCallbackForBaseTag : HtmlCallbackBase
	{
		// Token: 0x1700015C RID: 348
		// (get) Token: 0x060009B6 RID: 2486 RVA: 0x0002EE4D File Offset: 0x0002D04D
		// (set) Token: 0x060009B7 RID: 2487 RVA: 0x0002EE55 File Offset: 0x0002D055
		public Uri BaseHref { get; private set; }

		// Token: 0x060009B8 RID: 2488 RVA: 0x0002EE60 File Offset: 0x0002D060
		public override void ProcessTag(HtmlTagContext htmlTagContext, HtmlWriter htmlWriter)
		{
			if (htmlTagContext.TagId == HtmlTagId.Base)
			{
				foreach (HtmlTagContextAttribute htmlTagContextAttribute in htmlTagContext.Attributes)
				{
					if (htmlTagContextAttribute.Id == HtmlAttributeId.Href)
					{
						string value = htmlTagContextAttribute.Value;
						this.BaseHref = HtmlBodyCallback.TryParseUri(value, UriKind.Absolute);
						break;
					}
				}
			}
		}
	}
}
