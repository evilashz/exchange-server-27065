using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.TextConverters;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000037 RID: 55
	internal class AttachmentWebBeaconFilterCallback : HtmlCallbackBase
	{
		// Token: 0x06000134 RID: 308 RVA: 0x000053BC File Offset: 0x000035BC
		public override void ProcessTag(HtmlTagContext tagContext, HtmlWriter htmlWriter)
		{
			HtmlTagId tagId = tagContext.TagId;
			if (tagId == HtmlTagId.Img)
			{
				this.ProcessImageTag(tagContext, htmlWriter);
				return;
			}
			tagContext.WriteTag(true);
		}

		// Token: 0x06000135 RID: 309 RVA: 0x000053E8 File Offset: 0x000035E8
		private static void WriteAllAttributesExcept(HtmlTagContext tagContext, HtmlAttributeId attrToSkip)
		{
			foreach (HtmlTagContextAttribute htmlTagContextAttribute in tagContext.Attributes)
			{
				if (htmlTagContextAttribute.Id != attrToSkip)
				{
					htmlTagContextAttribute.Write();
				}
			}
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00005448 File Offset: 0x00003648
		private void ProcessImageTag(HtmlTagContext tagContext, HtmlWriter htmlWriter)
		{
			string value;
			this.GetLinkSource(tagContext, out value);
			tagContext.WriteTag(false);
			htmlWriter.WriteAttribute("blockedImageSrc", value);
			AttachmentWebBeaconFilterCallback.WriteAllAttributesExcept(tagContext, HtmlAttributeId.Src);
		}

		// Token: 0x06000137 RID: 311 RVA: 0x0000547C File Offset: 0x0000367C
		private void GetLinkSource(HtmlTagContext tagContext, out string srcValue)
		{
			srcValue = null;
			foreach (HtmlTagContextAttribute htmlTagContextAttribute in tagContext.Attributes)
			{
				if (htmlTagContextAttribute.Id == HtmlAttributeId.Src)
				{
					srcValue = htmlTagContextAttribute.Value;
					break;
				}
			}
		}
	}
}
