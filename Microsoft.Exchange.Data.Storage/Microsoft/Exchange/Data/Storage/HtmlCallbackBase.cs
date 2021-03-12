using System;
using Microsoft.Exchange.Data.TextConverters;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200059D RID: 1437
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class HtmlCallbackBase : ConversionCallbackBase
	{
		// Token: 0x06003ACF RID: 15055 RVA: 0x000F1FF3 File Offset: 0x000F01F3
		protected HtmlCallbackBase()
		{
		}

		// Token: 0x06003AD0 RID: 15056 RVA: 0x000F1FFB File Offset: 0x000F01FB
		protected HtmlCallbackBase(Item item) : this(item.CoreItem)
		{
		}

		// Token: 0x06003AD1 RID: 15057 RVA: 0x000F2009 File Offset: 0x000F0209
		protected HtmlCallbackBase(ICoreItem item) : base(item)
		{
		}

		// Token: 0x06003AD2 RID: 15058 RVA: 0x000F2012 File Offset: 0x000F0212
		protected HtmlCallbackBase(AttachmentCollection collection, Body itemBody) : this(collection.CoreAttachmentCollection, itemBody)
		{
		}

		// Token: 0x06003AD3 RID: 15059 RVA: 0x000F2021 File Offset: 0x000F0221
		protected HtmlCallbackBase(CoreAttachmentCollection collection, Body itemBody) : base(collection, itemBody)
		{
		}

		// Token: 0x06003AD4 RID: 15060
		public abstract void ProcessTag(HtmlTagContext tagContext, HtmlWriter writer);
	}
}
