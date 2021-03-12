using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Exchange.TextMessaging.MobileDriver
{
	// Token: 0x0200000B RID: 11
	internal class CodedText
	{
		// Token: 0x06000040 RID: 64 RVA: 0x000035AC File Offset: 0x000017AC
		public CodedText(CodingScheme codingScheme, string text, IList<int> radixCountMap)
		{
			if (codingScheme == CodingScheme.Neutral)
			{
				throw new ArgumentOutOfRangeException("codingScheme");
			}
			if (string.IsNullOrEmpty(text))
			{
				throw new ArgumentNullException("text");
			}
			if (radixCountMap == null || text.Length != radixCountMap.Count)
			{
				throw new ArgumentOutOfRangeException("radixCountMap");
			}
			this.CodingScheme = codingScheme;
			this.Text = text;
			this.RadixCountMap = radixCountMap;
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00003614 File Offset: 0x00001814
		public bool CanBeCodedEntirely
		{
			get
			{
				if (this.canBeCodedEntirely == null)
				{
					this.canBeCodedEntirely = new bool?(true);
					using (IEnumerator<int> enumerator = this.RadixCountMap.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							if (enumerator.Current == 0)
							{
								this.canBeCodedEntirely = new bool?(false);
								break;
							}
						}
					}
				}
				return this.canBeCodedEntirely.Value;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00003690 File Offset: 0x00001890
		// (set) Token: 0x06000043 RID: 67 RVA: 0x00003698 File Offset: 0x00001898
		public CodingScheme CodingScheme { get; private set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000044 RID: 68 RVA: 0x000036A1 File Offset: 0x000018A1
		// (set) Token: 0x06000045 RID: 69 RVA: 0x000036A9 File Offset: 0x000018A9
		public string Text { get; set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000046 RID: 70 RVA: 0x000036B2 File Offset: 0x000018B2
		// (set) Token: 0x06000047 RID: 71 RVA: 0x000036BA File Offset: 0x000018BA
		private IList<int> RadixCountMap { get; set; }

		// Token: 0x06000048 RID: 72 RVA: 0x000036C3 File Offset: 0x000018C3
		public override string ToString()
		{
			return this.Text;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000036CB File Offset: 0x000018CB
		public int GetRadixCount(int index)
		{
			if (0 > index || this.Text.Length <= index)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			return this.RadixCountMap[index];
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000036F8 File Offset: 0x000018F8
		public void ReplaceUncodableCharacters(char fallbackCharacter)
		{
			if (this.CanBeCodedEntirely)
			{
				return;
			}
			if (CodingSchemeInfo.GetCodingSchemeInfo(this.CodingScheme).Coder.GetCodedRadixCount(fallbackCharacter) == 0)
			{
				throw new ArgumentOutOfRangeException("fallbackCharacter");
			}
			StringBuilder stringBuilder = new StringBuilder(this.Text.Length);
			int num = 0;
			while (this.RadixCountMap.Count > num)
			{
				stringBuilder.Append((0 < this.RadixCountMap[num]) ? this.Text[num] : fallbackCharacter);
				num++;
			}
			this.Text = stringBuilder.ToString();
		}

		// Token: 0x04000021 RID: 33
		public const char DefaultFallbackCharacter = '?';

		// Token: 0x04000022 RID: 34
		private bool? canBeCodedEntirely;
	}
}
