using System;
using System.Collections.Generic;
using Microsoft.Exchange.TextMessaging.MobileDriver.Resources;

namespace Microsoft.Exchange.TextMessaging.MobileDriver
{
	// Token: 0x02000006 RID: 6
	internal abstract class ShortPartComposerBase : IComposer
	{
		// Token: 0x0600001A RID: 26 RVA: 0x000028C3 File Offset: 0x00000AC3
		public ShortPartComposerBase(int maxParts)
		{
			if (0 > maxParts)
			{
				throw new ArgumentOutOfRangeException("maxParts");
			}
			this.MaximumParts = maxParts;
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600001B RID: 27 RVA: 0x000028E1 File Offset: 0x00000AE1
		public PartType PartType
		{
			get
			{
				return PartType.Short;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600001C RID: 28
		protected abstract PureSplitterBase SplittingToMaximumParts { get; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600001D RID: 29
		protected abstract PureSplitterBase SplittingToTheEnd { get; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600001E RID: 30 RVA: 0x000028E4 File Offset: 0x00000AE4
		// (set) Token: 0x0600001F RID: 31 RVA: 0x000028EC File Offset: 0x00000AEC
		private int MaximumParts { get; set; }

		// Token: 0x06000020 RID: 32 RVA: 0x000028F8 File Offset: 0x00000AF8
		public BookmarkRetriever Compose(IList<ProportionedText> texts)
		{
			if (texts == null)
			{
				throw new ArgumentNullException("texts");
			}
			string paddingFormat = "({0}/{1})\n";
			ProportionedContent proportionedContent = new ProportionedContent(texts);
			ProportionedText.PresentationContent presentationContent = proportionedContent.GetPresentationContent(proportionedContent.IdealWeightedTextCapacity, false);
			BookmarkRetriever bookmarkRetriever = this.SplittingToTheEnd.NumberedSplit(presentationContent, paddingFormat);
			if (bookmarkRetriever != null && bookmarkRetriever.Parts.Count <= 1)
			{
				return this.TrailEllipsis(presentationContent, bookmarkRetriever, this.SplittingToTheEnd, paddingFormat, null);
			}
			bool withOptional = false;
			presentationContent = proportionedContent.GetPresentationContent(0, withOptional);
			bookmarkRetriever = this.SplittingToTheEnd.NumberedSplit(presentationContent, paddingFormat);
			if (bookmarkRetriever == null)
			{
				throw new MobileDriverDataException(Strings.ErrorTooManyParts);
			}
			int count = bookmarkRetriever.Parts.Count;
			if (count > 1)
			{
				withOptional = true;
				presentationContent = proportionedContent.GetPresentationContent(0, withOptional);
				bookmarkRetriever = this.SplittingToTheEnd.NumberedSplit(presentationContent, paddingFormat);
				if (bookmarkRetriever == null)
				{
					throw new MobileDriverDataException(Strings.ErrorTooManyParts);
				}
				count = bookmarkRetriever.Parts.Count;
			}
			if (count > this.MaximumParts)
			{
				if (presentationContent.IsGrouped)
				{
					presentationContent = proportionedContent.GetPresentationContent(0, withOptional, false);
				}
				bookmarkRetriever = this.SplittingToMaximumParts.NumberedSplit(presentationContent, paddingFormat);
				return this.TrailEllipsis(presentationContent, bookmarkRetriever, this.SplittingToMaximumParts, paddingFormat, null);
			}
			BookmarkRetriever bookmarkRetriever2 = null;
			int num = proportionedContent.IdealWeightedTextCapacity * 2;
			while (bookmarkRetriever2 == null)
			{
				num /= 2;
				ProportionedText.PresentationContent presentationContent2 = proportionedContent.GetPresentationContent(num, withOptional);
				bookmarkRetriever2 = this.SplittingToTheEnd.NumberedSplit(presentationContent2, paddingFormat);
			}
			int i = 0;
			int num2 = num;
			while (i < num2)
			{
				int num3 = (i + num2) / 2;
				ProportionedText.PresentationContent presentationContent2 = proportionedContent.GetPresentationContent(num3, withOptional);
				bookmarkRetriever2 = this.SplittingToTheEnd.NumberedSplit(presentationContent2, paddingFormat);
				if (bookmarkRetriever2.Parts.Count == count)
				{
					presentationContent = presentationContent2;
					bookmarkRetriever = bookmarkRetriever2;
					i = num3 + 1;
				}
				else if (bookmarkRetriever2.Parts.Count > count)
				{
					num2 = num3 - 1;
				}
				else if (bookmarkRetriever2.Parts.Count < count)
				{
					i = num3 + 1;
				}
			}
			string optionalSuffix = "\n\n";
			return this.TrailEllipsis(presentationContent, bookmarkRetriever, this.SplittingToTheEnd, paddingFormat, optionalSuffix);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002AE0 File Offset: 0x00000CE0
		private static int GetOrderNumberOccupiedCharacterLength(int sequence, int total, string paddingFormat)
		{
			int num = 0;
			int num2 = sequence;
			while (0 < num2)
			{
				num++;
				num2 /= 10;
			}
			int num3 = total;
			while (0 < num3)
			{
				num++;
				num3 /= 10;
			}
			return num + paddingFormat.Length - 6;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002B1C File Offset: 0x00000D1C
		private BookmarkRetriever TrailEllipsis(ProportionedText.PresentationContent nodeGroups, BookmarkRetriever bookmarks, PureSplitterBase pureSplitter, string paddingFormat, string optionalSuffix)
		{
			if (nodeGroups == null)
			{
				throw new ArgumentNullException("nodeGroups");
			}
			int num = 0;
			if (optionalSuffix != null && nodeGroups.IsGrouped)
			{
				num = optionalSuffix.Length;
			}
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			int num5 = 0;
			CodingScheme codingScheme = CodingScheme.Neutral;
			int num6 = 0;
			while (nodeGroups.PresentationTexts.Count > num6)
			{
				if (nodeGroups.PresentationTexts[num6].GroupId != num5)
				{
					num5 = nodeGroups.PresentationTexts[num6].GroupId;
				}
				else
				{
					num2 += nodeGroups.PresentationTexts[num6].Text.Length;
					if (nodeGroups.PresentationTexts[num6].NeedEllipsisTrail)
					{
						while (num3 < num2 && num4 < bookmarks.Parts.Count)
						{
							codingScheme = bookmarks.Parts[num4].CodingScheme;
							num3 += bookmarks.Parts[num4].CharacterCount - ShortPartComposerBase.GetOrderNumberOccupiedCharacterLength(num4 + 1, bookmarks.Parts.Count, paddingFormat) + num;
							num4++;
						}
						nodeGroups.PresentationTexts[num6] = new ProportionedText.PresentationText(new EllipsisTrailer(codingScheme).Trail(nodeGroups.PresentationTexts[num6].Text), false, nodeGroups.PresentationTexts[num6].GroupId);
					}
				}
				num6++;
			}
			return pureSplitter.NumberedSplit(nodeGroups, paddingFormat);
		}
	}
}
