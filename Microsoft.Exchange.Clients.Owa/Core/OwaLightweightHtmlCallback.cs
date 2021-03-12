using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.TextConverters;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020001E0 RID: 480
	internal class OwaLightweightHtmlCallback : HtmlCallbackBase
	{
		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x06000F65 RID: 3941 RVA: 0x0005F71E File Offset: 0x0005D91E
		public Uri BaseRef
		{
			get
			{
				return this.baseRef;
			}
		}

		// Token: 0x06000F66 RID: 3942 RVA: 0x0005F728 File Offset: 0x0005D928
		public override void ProcessTag(HtmlTagContext context, HtmlWriter writer)
		{
			if (context.TagId == HtmlTagId.Base)
			{
				foreach (HtmlTagContextAttribute attribute in context.Attributes)
				{
					if (OwaSafeHtmlCallbackBase.IsBaseTag(context.TagId, attribute))
					{
						string value = attribute.Value;
						this.baseRef = Utilities.TryParseUri(value);
						break;
					}
				}
			}
		}

		// Token: 0x04000A5D RID: 2653
		protected Uri baseRef;
	}
}
