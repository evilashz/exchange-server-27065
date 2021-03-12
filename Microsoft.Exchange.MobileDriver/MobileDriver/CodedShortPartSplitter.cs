using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Exchange.TextMessaging.MobileDriver.Resources;

namespace Microsoft.Exchange.TextMessaging.MobileDriver
{
	// Token: 0x0200000A RID: 10
	internal class CodedShortPartSplitter : PureSplitterBase
	{
		// Token: 0x06000031 RID: 49 RVA: 0x0000311B File Offset: 0x0000131B
		public CodedShortPartSplitter(CodingScheme codingScheme, int radixPerPart) : this(codingScheme, radixPerPart, true, '?', 0)
		{
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00003129 File Offset: 0x00001329
		public CodedShortPartSplitter(CodingScheme codingScheme, int radixPerPart, int maximumSegments) : this(codingScheme, radixPerPart, true, '?', maximumSegments)
		{
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00003137 File Offset: 0x00001337
		public CodedShortPartSplitter(CodingScheme codingScheme, int radixPerPart, char fallbackCharacter) : this(codingScheme, radixPerPart, false, fallbackCharacter, 0)
		{
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00003144 File Offset: 0x00001344
		public CodedShortPartSplitter(CodingScheme codingScheme, int radixPerPart, char fallbackCharacter, int maximumSegments) : this(codingScheme, radixPerPart, false, fallbackCharacter, maximumSegments)
		{
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00003154 File Offset: 0x00001354
		private CodedShortPartSplitter(CodingScheme codingScheme, int radixPerPart, bool throwIfNotCodable, char fallbackCharacter, int maximumSegments)
		{
			this.coding = new CodingSupportability(codingScheme, radixPerPart, radixPerPart);
			this.ThrowIfNotCodable = throwIfNotCodable;
			if (this.coding.CodingSchemeInfo.Coder.GetCodedRadixCount(fallbackCharacter) == 0)
			{
				throw new ArgumentOutOfRangeException("fallbackCharacter");
			}
			if (0 > maximumSegments)
			{
				throw new ArgumentOutOfRangeException("maximumSegments");
			}
			this.FallbackCharacter = fallbackCharacter;
			this.maximumSegments = maximumSegments;
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000036 RID: 54 RVA: 0x000031C0 File Offset: 0x000013C0
		public CodingScheme CodingScheme
		{
			get
			{
				return this.coding.CodingScheme;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000037 RID: 55 RVA: 0x000031CD File Offset: 0x000013CD
		public int RadixPerPart
		{
			get
			{
				return this.coding.RadixPerPart;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000038 RID: 56 RVA: 0x000031DA File Offset: 0x000013DA
		// (set) Token: 0x06000039 RID: 57 RVA: 0x000031E2 File Offset: 0x000013E2
		public bool ThrowIfNotCodable { get; private set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600003A RID: 58 RVA: 0x000031EB File Offset: 0x000013EB
		// (set) Token: 0x0600003B RID: 59 RVA: 0x000031F3 File Offset: 0x000013F3
		public char FallbackCharacter { get; private set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600003C RID: 60 RVA: 0x000031FC File Offset: 0x000013FC
		public override bool OnePass
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600003D RID: 61 RVA: 0x000031FF File Offset: 0x000013FF
		public override PartType PartType
		{
			get
			{
				return PartType.Short;
			}
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00003228 File Offset: 0x00001428
		internal override IList<Bookmark> Split(string text, IEnumerable<Bookmark> existing, int desiredCount, out bool more)
		{
			more = false;
			if (string.IsNullOrEmpty(text))
			{
				return new ReadOnlyCollection<Bookmark>(new Bookmark[]
				{
					Bookmark.Empty
				});
			}
			CodedText codedText = this.coding.CodingSchemeInfo.Coder.Code(text);
			if (!codedText.CanBeCodedEntirely)
			{
				if (this.ThrowIfNotCodable)
				{
					throw new MobileDriverCantBeCodedException(Strings.ErrorCantBeCoded(codedText.CodingScheme.ToString(), codedText.ToString()));
				}
				codedText.ReplaceUncodableCharacters(this.FallbackCharacter);
			}
			bool flag = 0 > desiredCount;
			List<Bookmark> list;
			if (existing == null)
			{
				list = new List<Bookmark>();
			}
			else
			{
				list = new List<Bookmark>(existing);
				list.Sort((Bookmark a, Bookmark b) => a.BeginLocation.CompareTo(b.BeginLocation));
			}
			if (0 < list.Count && list[list.Count - 1].IncompleteEnd)
			{
				throw new ArgumentOutOfRangeException("existing");
			}
			if (this.maximumSegments != 0)
			{
				while (list.Count > this.maximumSegments)
				{
					list.RemoveAt(list.Count - 1);
				}
				if (list.Count == this.maximumSegments)
				{
					BookmarkHelper.RebuildBookmarksWithTrailingEllipsis(list);
					return list;
				}
			}
			int num = (0 < list.Count) ? (list[list.Count - 1].EndLocation + 1) : 0;
			int num2 = 0;
			while (codedText.Text.Length > num)
			{
				int num3 = codedText.GetRadixCount(num);
				if (num3 == 0)
				{
					num3 = this.coding.CodingSchemeInfo.Coder.GetCodedRadixCount(this.FallbackCharacter);
				}
				if (num2 == 0)
				{
					if (0 < list.Count)
					{
						list[list.Count - 1] = new Bookmark(list[list.Count - 1].FullText, list[list.Count - 1].PartType, list[list.Count - 1].PartNumber, list[list.Count - 1].CodingScheme, list[list.Count - 1].BeginLocation, num - 1);
						if (this.maximumSegments != 0 && list.Count == this.maximumSegments)
						{
							BookmarkHelper.RebuildBookmarksWithTrailingEllipsis(list);
							break;
						}
					}
					if (!flag && 0 > --desiredCount)
					{
						more = true;
						break;
					}
					list.Add(new Bookmark(codedText.Text, PartType.Short, (0 < list.Count) ? (list[list.Count - 1].PartNumber + 1) : 0, codedText.CodingScheme, num, -1));
				}
				num2 += num3;
				if (this.coding.RadixPerPart < num2)
				{
					num--;
					num2 = 0;
				}
				num++;
			}
			if (0 < list.Count && -1 == list[list.Count - 1].EndLocation)
			{
				list[list.Count - 1] = new Bookmark(list[list.Count - 1].FullText, list[list.Count - 1].PartType, list[list.Count - 1].PartNumber, list[list.Count - 1].CodingScheme, list[list.Count - 1].BeginLocation, num - 1);
			}
			return list.AsReadOnly();
		}

		// Token: 0x0400001C RID: 28
		private CodingSupportability coding;

		// Token: 0x0400001D RID: 29
		private int maximumSegments;
	}
}
