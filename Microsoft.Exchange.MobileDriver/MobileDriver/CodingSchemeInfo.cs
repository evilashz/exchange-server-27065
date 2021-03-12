using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Microsoft.Exchange.TextMessaging.MobileDriver
{
	// Token: 0x02000016 RID: 22
	internal class CodingSchemeInfo
	{
		// Token: 0x0600006F RID: 111 RVA: 0x00003A60 File Offset: 0x00001C60
		static CodingSchemeInfo()
		{
			CodingSchemeInfo.known.Add(CodingScheme.GsmDefault, new CodingSchemeInfo(CodingScheme.GsmDefault, 7, new ReadOnlyCollection<int>(new int[]
			{
				1,
				2
			}), new GsmDefaultCoder()));
			CodingSchemeInfo.known.Add(CodingScheme.Unicode, new CodingSchemeInfo(CodingScheme.Unicode, 16, new ReadOnlyCollection<int>(new int[]
			{
				1
			}), new UnicodeCoder()));
			CodingSchemeInfo.known.Add(CodingScheme.UsAscii, new CodingSchemeInfo(CodingScheme.UsAscii, 7, new ReadOnlyCollection<int>(new int[]
			{
				1
			}), new UsAsciiCoder()));
			CodingSchemeInfo.known.Add(CodingScheme.Ia5, new CodingSchemeInfo(CodingScheme.Ia5, 7, new ReadOnlyCollection<int>(new int[]
			{
				1
			}), new Ia5Coder()));
			CodingSchemeInfo.known.Add(CodingScheme.Iso_8859_1, new CodingSchemeInfo(CodingScheme.Iso_8859_1, 8, new ReadOnlyCollection<int>(new int[]
			{
				1
			}), new Iso_8859_1Coder()));
			CodingSchemeInfo.known.Add(CodingScheme.Iso_8859_8, new CodingSchemeInfo(CodingScheme.Iso_8859_8, 8, new ReadOnlyCollection<int>(new int[]
			{
				1
			}), new Iso_8859_8Coder()));
			CodingSchemeInfo.known.Add(CodingScheme.ShiftJis, new CodingSchemeInfo(CodingScheme.ShiftJis, 8, new ReadOnlyCollection<int>(new int[]
			{
				1,
				2
			}), new ShiftJisCoder()));
			CodingSchemeInfo.known.Add(CodingScheme.EucKr, new CodingSchemeInfo(CodingScheme.EucKr, 8, new ReadOnlyCollection<int>(new int[]
			{
				1,
				2
			}), new EucKrCoder()));
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003BD4 File Offset: 0x00001DD4
		private CodingSchemeInfo(CodingScheme codingScheme, int codingBitsRadix, IList<int> codingRadixesAllowance, ICoder coder)
		{
			if (codingScheme == CodingScheme.Neutral)
			{
				throw new ArgumentOutOfRangeException("codingScheme");
			}
			if (0 >= codingBitsRadix)
			{
				throw new ArgumentOutOfRangeException("codingBitsRadix");
			}
			if (codingRadixesAllowance == null)
			{
				throw new ArgumentNullException("codingRadixesAllowance");
			}
			if (coder == null)
			{
				throw new ArgumentNullException("coder");
			}
			foreach (int num in codingRadixesAllowance)
			{
				if (0 >= num)
				{
					throw new ArgumentOutOfRangeException("codingRadixesAllowance");
				}
			}
			this.CodingScheme = codingScheme;
			this.CodingBitsRadix = codingBitsRadix;
			List<int> list = new List<int>(codingRadixesAllowance);
			list.Sort();
			this.CodingRadixesAllowance = list.AsReadOnly();
			this.Coder = coder;
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000071 RID: 113 RVA: 0x00003C94 File Offset: 0x00001E94
		// (set) Token: 0x06000072 RID: 114 RVA: 0x00003C9C File Offset: 0x00001E9C
		public CodingScheme CodingScheme { get; private set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000073 RID: 115 RVA: 0x00003CA5 File Offset: 0x00001EA5
		// (set) Token: 0x06000074 RID: 116 RVA: 0x00003CAD File Offset: 0x00001EAD
		public int CodingBitsRadix { get; private set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000075 RID: 117 RVA: 0x00003CB6 File Offset: 0x00001EB6
		// (set) Token: 0x06000076 RID: 118 RVA: 0x00003CBE File Offset: 0x00001EBE
		public IList<int> CodingRadixesAllowance { get; private set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00003CC7 File Offset: 0x00001EC7
		// (set) Token: 0x06000078 RID: 120 RVA: 0x00003CCF File Offset: 0x00001ECF
		public ICoder Coder { get; private set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000079 RID: 121 RVA: 0x00003CD8 File Offset: 0x00001ED8
		public CodingCategory CodingCategory
		{
			get
			{
				if (1 == this.CodingRadixesAllowance.Count)
				{
					return CodingCategory.Fixed;
				}
				return CodingCategory.Variant;
			}
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003CEB File Offset: 0x00001EEB
		public static CodingSchemeInfo GetCodingSchemeInfo(CodingScheme codingScheme)
		{
			if (!CodingSchemeInfo.known.ContainsKey(codingScheme))
			{
				throw new ArgumentOutOfRangeException("codingScheme");
			}
			return CodingSchemeInfo.known[codingScheme];
		}

		// Token: 0x0400002B RID: 43
		private static IDictionary<CodingScheme, CodingSchemeInfo> known = new Dictionary<CodingScheme, CodingSchemeInfo>();
	}
}
