using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200059E RID: 1438
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class RtfCallbackBase : ConversionCallbackBase
	{
		// Token: 0x06003AD5 RID: 15061 RVA: 0x000F202B File Offset: 0x000F022B
		protected RtfCallbackBase()
		{
		}

		// Token: 0x06003AD6 RID: 15062 RVA: 0x000F2033 File Offset: 0x000F0233
		protected RtfCallbackBase(ICoreItem coreItem) : base(coreItem)
		{
		}

		// Token: 0x06003AD7 RID: 15063 RVA: 0x000F203C File Offset: 0x000F023C
		protected RtfCallbackBase(CoreAttachmentCollection collection, Body itemBody) : base(collection, itemBody)
		{
		}

		// Token: 0x06003AD8 RID: 15064
		public abstract bool ProcessImage(string imageUrl, int approximateRenderingPosition);
	}
}
