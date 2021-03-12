using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Microsoft.Exchange.TextMessaging.MobileDriver
{
	// Token: 0x0200002C RID: 44
	internal class GsmShortPartSplitter : PureSplitterBase
	{
		// Token: 0x060000C0 RID: 192 RVA: 0x0000545A File Offset: 0x0000365A
		public GsmShortPartSplitter() : this(160, 70)
		{
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00005469 File Offset: 0x00003669
		public GsmShortPartSplitter(int gsmDefaultPerPart, int unicodePerPart) : this(gsmDefaultPerPart, unicodePerPart, 0)
		{
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00005474 File Offset: 0x00003674
		public GsmShortPartSplitter(int gsmDefaultPerPart, int unicodePerPart, int maximumSegments)
		{
			if (0 >= gsmDefaultPerPart)
			{
				throw new ArgumentOutOfRangeException("gsmDefaultPerPart");
			}
			if (0 >= unicodePerPart)
			{
				throw new ArgumentOutOfRangeException("unicodePerPart");
			}
			if (0 > maximumSegments)
			{
				throw new ArgumentOutOfRangeException("maximumSegments");
			}
			this.GsmDefaultCoding = new CodingSupportability(CodingScheme.GsmDefault, gsmDefaultPerPart, gsmDefaultPerPart);
			this.UnicodeCoding = new CodingSupportability(CodingScheme.Unicode, unicodePerPart, unicodePerPart);
			this.MaximumSegments = maximumSegments;
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x000054D7 File Offset: 0x000036D7
		public int GsmDefaultPerPart
		{
			get
			{
				return this.GsmDefaultCoding.RadixPerPart;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x000054E4 File Offset: 0x000036E4
		public int UnicodePerPart
		{
			get
			{
				return this.UnicodeCoding.RadixPerPart;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x000054F1 File Offset: 0x000036F1
		public override bool OnePass
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x000054F4 File Offset: 0x000036F4
		public override PartType PartType
		{
			get
			{
				return PartType.Short;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x000054F7 File Offset: 0x000036F7
		// (set) Token: 0x060000C8 RID: 200 RVA: 0x000054FF File Offset: 0x000036FF
		protected CodingSupportability GsmDefaultCoding { get; set; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x00005508 File Offset: 0x00003708
		// (set) Token: 0x060000CA RID: 202 RVA: 0x00005510 File Offset: 0x00003710
		protected CodingSupportability UnicodeCoding { get; set; }

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000CB RID: 203 RVA: 0x00005519 File Offset: 0x00003719
		// (set) Token: 0x060000CC RID: 204 RVA: 0x00005521 File Offset: 0x00003721
		private protected int MaximumSegments { protected get; private set; }

		// Token: 0x060000CD RID: 205 RVA: 0x00005550 File Offset: 0x00003750
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
			CodedText codedText = this.GsmDefaultCoding.CodingSchemeInfo.Coder.Code(text);
			CodedText codedText2 = this.UnicodeCoding.CodingSchemeInfo.Coder.Code(text);
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
			if (this.MaximumSegments != 0)
			{
				while (list.Count > this.MaximumSegments)
				{
					list.RemoveAt(list.Count - 1);
				}
				if (list.Count == this.MaximumSegments)
				{
					BookmarkHelper.RebuildBookmarksWithTrailingEllipsis(list);
					return list;
				}
			}
			CodedText codedText3 = codedText;
			int num = (0 < list.Count) ? (list[list.Count - 1].EndLocation + 1) : 0;
			int num2 = 0;
			while (codedText3.ToString().Length > num)
			{
				int radixCount = codedText3.GetRadixCount(num);
				if (CodingScheme.GsmDefault == codedText3.CodingScheme && radixCount == 0)
				{
					codedText3 = codedText2;
					radixCount = codedText3.GetRadixCount(num);
					int num3 = (0 < list.Count) ? (num - list[list.Count - 1].BeginLocation) : num;
					if (this.GetRadixPerPart(codedText3.CodingScheme) < num3)
					{
						num2 = 0;
					}
					else if (0 < list.Count)
					{
						list[list.Count - 1] = new Bookmark(list[list.Count - 1].FullText, list[list.Count - 1].PartType, list[list.Count - 1].PartNumber, codedText3.CodingScheme, list[list.Count - 1].BeginLocation, list[list.Count - 1].EndLocation);
					}
				}
				if (num2 == 0)
				{
					if (0 < list.Count)
					{
						list[list.Count - 1] = new Bookmark(list[list.Count - 1].FullText, list[list.Count - 1].PartType, list[list.Count - 1].PartNumber, list[list.Count - 1].CodingScheme, list[list.Count - 1].BeginLocation, num - 1);
						if (this.MaximumSegments != 0 && list.Count == this.MaximumSegments)
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
					list.Add(new Bookmark(codedText3.Text, PartType.Short, (0 < list.Count) ? (list[list.Count - 1].PartNumber + 1) : 0, codedText3.CodingScheme, num, -1));
				}
				num2 += radixCount;
				if (this.GetRadixPerPart(codedText3.CodingScheme) < num2)
				{
					if (CodingScheme.Unicode == codedText3.CodingScheme)
					{
						codedText3 = codedText;
					}
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

		// Token: 0x060000CE RID: 206 RVA: 0x000059B0 File Offset: 0x00003BB0
		private int GetRadixPerPart(CodingScheme codingScheme)
		{
			switch (codingScheme)
			{
			case CodingScheme.GsmDefault:
				return this.GsmDefaultPerPart;
			case CodingScheme.Unicode:
				return this.UnicodePerPart;
			default:
				throw new ArgumentOutOfRangeException("codingScheme");
			}
		}

		// Token: 0x0400008C RID: 140
		public const int MaxGsmDefaultPerPart = 160;

		// Token: 0x0400008D RID: 141
		public const int MaxUnicodePerPart = 70;
	}
}
