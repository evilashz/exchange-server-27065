using System;
using Microsoft.Exchange.Data.TextConverters;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000200 RID: 512
	internal sealed class OwaSafeHtmlWebReadyCallbacks : OwaSafeHtmlOutboundCallbacks
	{
		// Token: 0x060010D4 RID: 4308 RVA: 0x0006727C File Offset: 0x0006547C
		public OwaSafeHtmlWebReadyCallbacks(string documentID) : base(OwaContext.Current, true, true)
		{
			this.urlPrefix = string.Concat(new string[]
			{
				"ev.owa?ns=WebReady&ev=GetFile",
				Utilities.GetCanaryRequestParameter(),
				"&d=",
				documentID,
				"&fileName="
			});
		}

		// Token: 0x060010D5 RID: 4309 RVA: 0x000672CD File Offset: 0x000654CD
		protected override void ProcessImageTag(HtmlTagContextAttribute filterAttribute, HtmlTagContext context, HtmlWriter writer)
		{
			writer.WriteAttribute(filterAttribute.Id, this.urlPrefix + filterAttribute.Value);
		}

		// Token: 0x060010D6 RID: 4310 RVA: 0x000672F0 File Offset: 0x000654F0
		public override void ProcessTag(HtmlTagContext context, HtmlWriter writer)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (OwaSafeHtmlWebReadyCallbacks.IsStyleSheetLinkTag(context))
			{
				context.WriteTag();
				using (HtmlTagContext.AttributeCollection.Enumerator enumerator = context.Attributes.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						HtmlTagContextAttribute htmlTagContextAttribute = enumerator.Current;
						if (htmlTagContextAttribute.Id == HtmlAttributeId.Href)
						{
							writer.WriteAttribute(htmlTagContextAttribute.Id, this.urlPrefix + htmlTagContextAttribute.Value);
						}
						else
						{
							htmlTagContextAttribute.Write();
						}
					}
					return;
				}
			}
			base.ProcessTag(context, writer);
		}

		// Token: 0x060010D7 RID: 4311 RVA: 0x000673A4 File Offset: 0x000655A4
		private static bool IsStyleSheetLinkTag(HtmlTagContext context)
		{
			if (context.TagId != HtmlTagId.Link)
			{
				return false;
			}
			foreach (HtmlTagContextAttribute htmlTagContextAttribute in context.Attributes)
			{
				if (htmlTagContextAttribute.Id == HtmlAttributeId.Rel && !string.IsNullOrEmpty(htmlTagContextAttribute.Value) && htmlTagContextAttribute.Value.Equals("stylesheet", StringComparison.OrdinalIgnoreCase))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060010D8 RID: 4312 RVA: 0x00067434 File Offset: 0x00065634
		protected override void ProcessUseMapAttribute(HtmlTagContextAttribute filterAttribute, HtmlTagContext context, HtmlWriter writer)
		{
			if (context.TagId == HtmlTagId.Img)
			{
				filterAttribute.Write();
				return;
			}
			base.ProcessTag(context, writer);
		}

		// Token: 0x04000B73 RID: 2931
		private string urlPrefix;
	}
}
