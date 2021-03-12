using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020005DE RID: 1502
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal abstract class IImapMimeProvider : DisposableObject
	{
		// Token: 0x06003DD5 RID: 15829 RVA: 0x001001AD File Offset: 0x000FE3AD
		internal static IImapMimeProvider CreateInstance(ItemToMimeConverter itemConverter)
		{
			return new ImapItemMimeProvider(itemConverter);
		}

		// Token: 0x06003DD6 RID: 15830 RVA: 0x001001B5 File Offset: 0x000FE3B5
		internal static IImapMimeProvider CreateInstance(MimePart smimePart, MimeDocument smimeDoc)
		{
			return new ImapSmimeMimeProvider(smimePart, smimeDoc);
		}

		// Token: 0x06003DD7 RID: 15831
		internal abstract void WriteMimePart(MimeStreamWriter mimeWriter, ConversionLimitsTracker limits, MimePartInfo part, ItemToMimeConverter.MimeFlags mimeFlags);

		// Token: 0x06003DD8 RID: 15832
		internal abstract MimePartInfo CalculateMimeStructure(Charset itemCharset);

		// Token: 0x06003DD9 RID: 15833
		internal abstract List<MimeDocument> ExtractSkeletons();
	}
}
