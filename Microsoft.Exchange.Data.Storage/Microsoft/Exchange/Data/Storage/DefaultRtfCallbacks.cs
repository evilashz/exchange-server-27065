using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020005A0 RID: 1440
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class DefaultRtfCallbacks : RtfCallbackBase
	{
		// Token: 0x06003AE7 RID: 15079 RVA: 0x000F23D5 File Offset: 0x000F05D5
		internal DefaultRtfCallbacks(ICoreItem coreItem, bool itemReadOnly) : base(coreItem)
		{
			this.readOnly = itemReadOnly;
		}

		// Token: 0x06003AE8 RID: 15080 RVA: 0x000F23E5 File Offset: 0x000F05E5
		internal DefaultRtfCallbacks(CoreAttachmentCollection collection, Body itemBody, bool itemReadOnly) : base(collection, itemBody)
		{
			this.readOnly = itemReadOnly;
		}

		// Token: 0x06003AE9 RID: 15081 RVA: 0x000F23F8 File Offset: 0x000F05F8
		public override bool ProcessImage(string imageUrl, int approximateRenderingPosition)
		{
			AttachmentLink attachmentLink = base.FindAttachmentByBodyReference(imageUrl);
			if (attachmentLink == null)
			{
				return false;
			}
			attachmentLink.RenderingPosition = approximateRenderingPosition;
			attachmentLink.IsHidden = false;
			attachmentLink.MarkInline(true);
			return true;
		}

		// Token: 0x06003AEA RID: 15082 RVA: 0x000F2428 File Offset: 0x000F0628
		public override bool SaveChanges()
		{
			return !this.readOnly && base.SaveChanges();
		}

		// Token: 0x04001F86 RID: 8070
		private bool readOnly;
	}
}
