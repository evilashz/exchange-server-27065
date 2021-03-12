using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020005DF RID: 1503
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class ImapSmimeMimeProvider : IImapMimeProvider
	{
		// Token: 0x06003DDB RID: 15835 RVA: 0x001001C6 File Offset: 0x000FE3C6
		internal ImapSmimeMimeProvider(MimePart smimePart, MimeDocument smimeDoc)
		{
			this.smimePart = smimePart;
			this.smimeDoc = smimeDoc;
		}

		// Token: 0x06003DDC RID: 15836 RVA: 0x001001DC File Offset: 0x000FE3DC
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<ImapSmimeMimeProvider>(this);
		}

		// Token: 0x06003DDD RID: 15837 RVA: 0x001001E4 File Offset: 0x000FE3E4
		protected override void InternalDispose(bool disposing)
		{
			base.InternalDispose(disposing);
		}

		// Token: 0x06003DDE RID: 15838 RVA: 0x001001ED File Offset: 0x000FE3ED
		internal override void WriteMimePart(MimeStreamWriter mimeWriter, ConversionLimitsTracker limits, MimePartInfo part, ItemToMimeConverter.MimeFlags mimeFlags)
		{
			if ((mimeFlags & ItemToMimeConverter.MimeFlags.SkipContent) != ItemToMimeConverter.MimeFlags.SkipContent)
			{
				mimeWriter.WritePartWithHeaders(part.SmimePart, false);
				return;
			}
			mimeWriter.WriteHeadersFromPart(part.SmimePart);
		}

		// Token: 0x06003DDF RID: 15839 RVA: 0x00100210 File Offset: 0x000FE410
		internal override MimePartInfo CalculateMimeStructure(Charset itemCharset)
		{
			int num = 0;
			return this.CalculateMimeSubStructure(this.smimePart, itemCharset, ref num);
		}

		// Token: 0x06003DE0 RID: 15840 RVA: 0x0010022E File Offset: 0x000FE42E
		internal override List<MimeDocument> ExtractSkeletons()
		{
			return new List<MimeDocument>(0);
		}

		// Token: 0x06003DE1 RID: 15841 RVA: 0x00100238 File Offset: 0x000FE438
		private MimePartInfo CalculateMimeSubStructure(MimePart part, Charset itemCharset, ref int partIndex)
		{
			MimePartInfo mimePartInfo = null;
			if (part.IsEmbeddedMessage)
			{
				mimePartInfo = new MimePartInfo(itemCharset, null, MimePartContentType.ItemAttachment, null, null, null, part, this.smimeDoc, ref partIndex);
				int num = 0;
				mimePartInfo.AttachedItemStructure = this.CalculateMimeSubStructure((MimePart)part.FirstChild, itemCharset, ref num);
			}
			else
			{
				mimePartInfo = new MimePartInfo(itemCharset, null, MimePartInfo.GetContentType(part.ContentType), null, null, null, part, this.smimeDoc, ref partIndex);
				foreach (MimePart part2 in part)
				{
					mimePartInfo.AddChild(this.CalculateMimeSubStructure(part2, itemCharset, ref partIndex));
				}
			}
			foreach (Header header in part.Headers)
			{
				mimePartInfo.AddHeader(header);
			}
			MimeStreamWriter.CalculateBodySize(mimePartInfo, part);
			return mimePartInfo;
		}

		// Token: 0x0400212F RID: 8495
		private MimePart smimePart;

		// Token: 0x04002130 RID: 8496
		private MimeDocument smimeDoc;
	}
}
