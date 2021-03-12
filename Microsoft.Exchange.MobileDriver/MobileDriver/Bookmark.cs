using System;

namespace Microsoft.Exchange.TextMessaging.MobileDriver
{
	// Token: 0x02000003 RID: 3
	internal struct Bookmark
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000022BC File Offset: 0x000004BC
		internal Bookmark(string text, PartType partType, int partNumber, CodingScheme codingScheme, int beginLoc, int endLoc)
		{
			this = new Bookmark(text, partType, partNumber, codingScheme, beginLoc, endLoc, false, false);
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000022DC File Offset: 0x000004DC
		internal Bookmark(string text, PartType partType, int partNumber, CodingScheme codingScheme, int beginLoc, int endLoc, bool incompBegin, bool incompEnd)
		{
			if (string.IsNullOrEmpty(text) && (-1 != beginLoc || -1 != endLoc))
			{
				throw new ArgumentNullException("text");
			}
			if (0 > partNumber)
			{
				throw new ArgumentOutOfRangeException("partNumbe");
			}
			if (-1 != beginLoc && (0 > beginLoc || text.Length <= beginLoc))
			{
				throw new ArgumentOutOfRangeException("beginLoc");
			}
			if (-1 != endLoc && (0 > endLoc || text.Length <= endLoc))
			{
				throw new ArgumentOutOfRangeException("endLoc");
			}
			if (beginLoc > endLoc && -1 != endLoc)
			{
				throw new ArgumentException("endLoc");
			}
			this.text = text;
			this.partType = partType;
			this.partNumber = partNumber;
			this.codingScheme = codingScheme;
			this.beginLocation = beginLoc;
			this.endLocation = endLoc;
			this.incompleteBegin = incompBegin;
			this.incompleteEnd = incompEnd;
			this.literal = null;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000005 RID: 5 RVA: 0x000023AD File Offset: 0x000005AD
		public string FullText
		{
			get
			{
				return this.text;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000006 RID: 6 RVA: 0x000023B5 File Offset: 0x000005B5
		public PartType PartType
		{
			get
			{
				return this.partType;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000007 RID: 7 RVA: 0x000023BD File Offset: 0x000005BD
		public int PartNumber
		{
			get
			{
				return this.partNumber;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000008 RID: 8 RVA: 0x000023C5 File Offset: 0x000005C5
		public CodingScheme CodingScheme
		{
			get
			{
				return this.codingScheme;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000009 RID: 9 RVA: 0x000023CD File Offset: 0x000005CD
		public int BeginLocation
		{
			get
			{
				return this.beginLocation;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000A RID: 10 RVA: 0x000023D5 File Offset: 0x000005D5
		public int EndLocation
		{
			get
			{
				return this.endLocation;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000023DD File Offset: 0x000005DD
		public bool IncompleteBegin
		{
			get
			{
				return this.incompleteBegin;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000023E5 File Offset: 0x000005E5
		public bool IncompleteEnd
		{
			get
			{
				return this.incompleteEnd;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600000D RID: 13 RVA: 0x000023ED File Offset: 0x000005ED
		public int CharacterCount
		{
			get
			{
				if (-1 == this.BeginLocation || -1 == this.EndLocation)
				{
					return 0;
				}
				return (this.IncompleteEnd ? (this.EndLocation - 1) : this.EndLocation) - this.BeginLocation + 1;
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002424 File Offset: 0x00000624
		public override string ToString()
		{
			if (-1 == this.BeginLocation || -1 == this.EndLocation)
			{
				return string.Empty;
			}
			string result;
			if ((result = this.literal) == null)
			{
				result = (this.literal = this.text.Substring(this.BeginLocation, this.CharacterCount));
			}
			return result;
		}

		// Token: 0x0400000B RID: 11
		public const int InvalidLocation = -1;

		// Token: 0x0400000C RID: 12
		public static readonly Bookmark Empty = new Bookmark(null, PartType.Short, 0, CodingScheme.Neutral, -1, -1, false, false);

		// Token: 0x0400000D RID: 13
		private string text;

		// Token: 0x0400000E RID: 14
		private PartType partType;

		// Token: 0x0400000F RID: 15
		private int partNumber;

		// Token: 0x04000010 RID: 16
		private CodingScheme codingScheme;

		// Token: 0x04000011 RID: 17
		private int beginLocation;

		// Token: 0x04000012 RID: 18
		private int endLocation;

		// Token: 0x04000013 RID: 19
		private bool incompleteBegin;

		// Token: 0x04000014 RID: 20
		private bool incompleteEnd;

		// Token: 0x04000015 RID: 21
		private string literal;
	}
}
