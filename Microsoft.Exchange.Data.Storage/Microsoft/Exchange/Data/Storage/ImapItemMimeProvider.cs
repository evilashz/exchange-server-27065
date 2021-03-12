using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020005E0 RID: 1504
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class ImapItemMimeProvider : IImapMimeProvider
	{
		// Token: 0x06003DE2 RID: 15842 RVA: 0x00100334 File Offset: 0x000FE534
		internal ImapItemMimeProvider(ItemToMimeConverter itemConverter)
		{
			this.itemConverter = itemConverter;
		}

		// Token: 0x06003DE3 RID: 15843 RVA: 0x00100343 File Offset: 0x000FE543
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<ImapItemMimeProvider>(this);
		}

		// Token: 0x06003DE4 RID: 15844 RVA: 0x0010034B File Offset: 0x000FE54B
		protected override void InternalDispose(bool disposing)
		{
			base.InternalDispose(disposing);
			if (disposing && this.itemConverter != null)
			{
				this.itemConverter.Dispose();
				this.itemConverter = null;
			}
		}

		// Token: 0x06003DE5 RID: 15845 RVA: 0x00100371 File Offset: 0x000FE571
		internal override void WriteMimePart(MimeStreamWriter mimeWriter, ConversionLimitsTracker limits, MimePartInfo part, ItemToMimeConverter.MimeFlags mimeFlags)
		{
			this.itemConverter.WriteMimePart(mimeWriter, limits, part, mimeFlags);
		}

		// Token: 0x06003DE6 RID: 15846 RVA: 0x00100384 File Offset: 0x000FE584
		internal override MimePartInfo CalculateMimeStructure(Charset itemCharset)
		{
			return this.itemConverter.CalculateMimeStructure(itemCharset);
		}

		// Token: 0x06003DE7 RID: 15847 RVA: 0x00100392 File Offset: 0x000FE592
		internal override List<MimeDocument> ExtractSkeletons()
		{
			return this.itemConverter.ExtractSkeletons();
		}

		// Token: 0x04002131 RID: 8497
		private ItemToMimeConverter itemConverter;
	}
}
