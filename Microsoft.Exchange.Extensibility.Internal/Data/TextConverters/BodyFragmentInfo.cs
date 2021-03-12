using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Data.TextConverters.Internal.Format;

namespace Microsoft.Exchange.Data.TextConverters
{
	// Token: 0x02000020 RID: 32
	internal class BodyFragmentInfo : FragmentInfo
	{
		// Token: 0x060000D6 RID: 214 RVA: 0x00005918 File Offset: 0x00003B18
		public BodyFragmentInfo(ConversationBodyScanner bodyScanner) : base(bodyScanner, 0, bodyScanner.Lines.Count)
		{
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x0000592D File Offset: 0x00003B2D
		internal BodyFragmentInfo(ConversationBodyScanner bodyScanner, int startLineIndex, int endLineIndex) : base(bodyScanner, startLineIndex, endLineIndex)
		{
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x00005938 File Offset: 0x00003B38
		public BodyTagInfo BodyTag
		{
			get
			{
				if (this.bodyTagInfo == null)
				{
					this.InitializeBodyTag();
				}
				return this.bodyTagInfo;
			}
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00005954 File Offset: 0x00003B54
		public void ExtractNestedBodyParts(BodyTagInfo parentBodyTagInfo, out BodyFragmentInfo parentBodyPart, out FragmentInfo uniqueBodyPart, out FragmentInfo disclaimerPart)
		{
			BodyFragmentInfo bodyFragmentInfo;
			parentBodyPart = (bodyFragmentInfo = null);
			FragmentInfo fragmentInfo;
			disclaimerPart = (fragmentInfo = bodyFragmentInfo);
			uniqueBodyPart = fragmentInfo;
			parentBodyPart = this.GetParentBodyPart(parentBodyTagInfo);
			if (parentBodyPart != null)
			{
				uniqueBodyPart = this.GetUniqueBodyPart(parentBodyPart);
				disclaimerPart = this.GetDisclaimerBodyPart(parentBodyPart);
				if (uniqueBodyPart.IsEmpty && !disclaimerPart.IsEmpty)
				{
					uniqueBodyPart = disclaimerPart;
					disclaimerPart = FragmentInfo.Empty;
				}
			}
		}

		// Token: 0x060000DA RID: 218 RVA: 0x000059B4 File Offset: 0x00003BB4
		internal static bool IsUsefulWord(TextRun word, out uint crc)
		{
			crc = 0U;
			int i = 0;
			int wordLength = word.WordLength;
			while (i < wordLength)
			{
				char wordChar = word.GetWordChar(i);
				if (char.IsLetterOrDigit(wordChar) || CharUnicodeInfo.GetUnicodeCategory(wordChar) == UnicodeCategory.OtherPunctuation)
				{
					if (i % 4 == 3)
					{
						crc += (uint)(wordChar >> 8);
					}
					crc += (uint)((uint)wordChar << (8 * (i % 4) & 31));
				}
				i++;
			}
			return crc != 0U;
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00005A18 File Offset: 0x00003C18
		internal static int GetFormatCrc(FormatStore formatStore, FormatNode formatNode)
		{
			int num = 0;
			TextRun textRun = formatStore.GetTextRun(formatNode.BeginTextPosition);
			if (!textRun.Equals(TextRun.Invalid))
			{
				while (!textRun.IsEnd() && textRun.Type != TextRunType.NonSpace)
				{
					textRun.MoveNext();
				}
				if (!textRun.IsEnd())
				{
					num = (int)textRun.GetWordChar(0);
				}
			}
			Dictionary<PropertyId, List<PropertyValue>> dictionary = new Dictionary<PropertyId, List<PropertyValue>>();
			do
			{
				if (formatNode.Properties != null)
				{
					foreach (Property property in formatNode.Properties)
					{
						if (!property.IsNull)
						{
							List<PropertyValue> list;
							if (!dictionary.TryGetValue(property.Id, out list))
							{
								list = new List<PropertyValue>();
								dictionary.Add(property.Id, list);
							}
							list.Add(property.Value);
						}
					}
				}
				formatNode = formatNode.Parent;
			}
			while (formatNode.Parent != FormatNode.Null);
			foreach (KeyValuePair<PropertyId, List<PropertyValue>> keyValuePair in dictionary)
			{
				int propertyCrc = BodyFragmentInfo.GetPropertyCrc(formatStore, keyValuePair.Key, keyValuePair.Value);
				if (propertyCrc != 0)
				{
					num ^= propertyCrc;
				}
			}
			return num;
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00005B74 File Offset: 0x00003D74
		internal static int GetInlineObjectCount(TextRun firstRun, TextRun? lastRun)
		{
			TextRun next = firstRun.GetNext();
			int num = 0;
			while (!next.IsEnd() && (lastRun == null || next.Position < lastRun.Value.Position))
			{
				if (next.Type == TextRunType.FirstShort)
				{
					num++;
				}
				next.MoveNext();
			}
			return num;
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00005BD4 File Offset: 0x00003DD4
		private static int GetPropertyCrc(FormatStore formatStore, PropertyId propertyId, IList<PropertyValue> propertyValues)
		{
			int num = 0;
			if (propertyId <= PropertyId.Language)
			{
				switch (propertyId)
				{
				case PropertyId.FontColor:
					if (propertyValues[0].Type == PropertyType.Color && propertyValues[0].Color.RGB != BodyFragmentInfo.defaultFontColor.RGB)
					{
						num = (int)propertyValues[0].Color.RGB;
						goto IL_1AA;
					}
					goto IL_1AA;
				case PropertyId.FontSize:
					break;
				case PropertyId.FontFace:
					num = BodyFragmentInfo.GetFontFaceCrc(formatStore, propertyValues[0]);
					goto IL_1AA;
				case PropertyId.TextAlignment:
					num = ((propertyValues[0].Enum == 3) ? 0 : propertyValues[0].Enum);
					goto IL_1AA;
				default:
					if (propertyId == PropertyId.Language)
					{
						num = propertyValues[0].Integer;
						goto IL_1AA;
					}
					break;
				}
			}
			else
			{
				switch (propertyId)
				{
				case PropertyId.RightMargin:
				case PropertyId.LeftMargin:
				{
					float num2 = 0f;
					foreach (PropertyValue propertyValue in propertyValues)
					{
						num2 += propertyValue.Millimeters;
						if (propertyValue.Type == PropertyType.AbsLength)
						{
							break;
						}
					}
					num = (int)num2;
					goto IL_1AA;
				}
				case PropertyId.BottomMargin:
					break;
				default:
					switch (propertyId)
					{
					case PropertyId.RightBorderWidth:
					case PropertyId.LeftBorderWidth:
					{
						float num3 = 0f;
						foreach (PropertyValue propertyValue2 in propertyValues)
						{
							num3 += propertyValue2.Millimeters;
						}
						num = (int)(num3 * 2f);
						goto IL_1AA;
					}
					}
					break;
				}
			}
			return 0;
			IL_1AA:
			if (num != 0)
			{
				int num4 = (int)(propertyId % PropertyId.LeftMargin);
				num = (num << num4) + (num >> 32 - num4);
			}
			return num;
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00005DC4 File Offset: 0x00003FC4
		private static int GetFontFaceCrc(FormatStore formatStore, PropertyValue value)
		{
			if (value.Type == PropertyType.MultiValue)
			{
				return BodyFragmentInfo.GetFontFaceCrc(formatStore, formatStore.MultiValues.Plane(value.Value)[formatStore.MultiValues.Index(value.Value)].Values[0]);
			}
			int num = 0;
			string str = formatStore.Strings.Plane(value.StringHandle)[formatStore.Strings.Index(value.StringHandle)].Str;
			for (int i = 0; i < str.Length; i++)
			{
				char c = char.ToLowerInvariant(str[i]);
				if (i % 4 == 3)
				{
					num += (int)(c >> 8);
				}
				num += (int)((int)c << (8 * (i % 4) & 31));
			}
			return num;
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00005E84 File Offset: 0x00004084
		private void InitializeBodyTag()
		{
			if (this.bodyTagInfo != null)
			{
				return;
			}
			int num = 0;
			uint num2 = 0U;
			int num3 = 0;
			int startWordIndex = base.StartWordIndex;
			int endWordIndex = base.EndWordIndex;
			for (int i = startWordIndex; i < endWordIndex; i++)
			{
				uint num4;
				if (BodyFragmentInfo.IsUsefulWord(base.BodyScanner.Words[i], out num4))
				{
					int num5 = num % 32;
					if (num5 == 0)
					{
						num2 ^= num4;
					}
					else
					{
						num2 ^= (num4 << num5) + (num4 >> 32 - num5);
					}
					num++;
				}
			}
			if (base.StartLineIndex < base.EndLineIndex)
			{
				num3 = BodyFragmentInfo.GetFormatCrc(base.BodyScanner.FormatStore, base.BodyScanner.Lines[base.StartLineIndex].Node);
				if (base.BodyScanner.Words.Count > 0 && startWordIndex < base.BodyScanner.Words.Count)
				{
					num3 ^= BodyFragmentInfo.GetInlineObjectCount(base.BodyScanner.FormatStore.GetTextRun(base.BodyScanner.Lines[base.StartLineIndex].TextPosition), (base.EndLineIndex == base.BodyScanner.Lines.Count) ? null : new TextRun?(base.BodyScanner.FormatStore.GetTextRun(base.BodyScanner.Lines[base.EndLineIndex].TextPosition)));
				}
			}
			this.bodyTagInfo = new BodyTagInfo(num, (int)num2, num3);
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x0000600C File Offset: 0x0000420C
		private BodyFragmentInfo GetParentBodyPart(BodyTagInfo parentBodyTagInfo)
		{
			if (parentBodyTagInfo.WordCount == 0 && parentBodyTagInfo.FormatCrc == 0)
			{
				return new BodyFragmentInfo(base.BodyScanner, base.EndLineIndex, base.EndLineIndex);
			}
			int i = base.EndLineIndex;
			int num = base.EndLineIndex;
			int num2 = base.GetFirstWordIndex(i);
			int num3 = num2;
			int num4 = 0;
			uint num5 = 0U;
			while (num3 >= base.StartWordIndex && i >= base.StartLineIndex)
			{
				if (num4 == parentBodyTagInfo.WordCount && num5 == (uint)parentBodyTagInfo.WordCrc)
				{
					while (i > 0)
					{
						int num6 = base.GetFirstWordIndex(i - 1);
						uint num7 = 0U;
						while (num6 < num2 && !BodyFragmentInfo.IsUsefulWord(base.BodyScanner.Words[num6], out num7))
						{
							num6++;
						}
						if (num6 != num2)
						{
							break;
						}
						i--;
						num2 = base.GetFirstWordIndex(i);
					}
					FragmentInfo.TrimBoundary(base.BodyScanner, ref i, ref num);
					return new BodyFragmentInfo(base.BodyScanner, i, num);
				}
				if (num4 <= parentBodyTagInfo.WordCount)
				{
					if (i == 0)
					{
						break;
					}
					int j = base.GetFirstWordIndex(--i);
					while (j < num2)
					{
						if (base.StartWordIndex >= num2)
						{
							break;
						}
						uint num8 = 0U;
						num2--;
						if (BodyFragmentInfo.IsUsefulWord(base.BodyScanner.Words[num2], out num8))
						{
							num4++;
							num5 = (num5 << 1) + (num5 >> 31);
							num5 ^= num8;
						}
					}
				}
				else if (num4 > parentBodyTagInfo.WordCount)
				{
					int k = base.GetFirstWordIndex(--num);
					while (k < num3)
					{
						num3--;
						uint num9;
						if (BodyFragmentInfo.IsUsefulWord(base.BodyScanner.Words[num3], out num9))
						{
							num4--;
							int num10 = num4 % 32;
							if (num10 == 0)
							{
								num5 ^= num9;
							}
							else
							{
								num5 ^= (num9 << num10) + (num9 >> 32 - num10);
							}
						}
					}
				}
			}
			return null;
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x000061D0 File Offset: 0x000043D0
		private BodyFragmentInfo GetUniqueBodyPart(BodyFragmentInfo parentBodyFragment)
		{
			int startLineIndex = base.StartLineIndex;
			int num = parentBodyFragment.StartLineIndex;
			FragmentInfo.TrimBoundary(base.BodyScanner, ref startLineIndex, ref num);
			int i = 1;
			bool flag = false;
			while (i < base.BodyScanner.Fragments.Count)
			{
				if ((int)base.BodyScanner.Fragments[i].FirstLine == num)
				{
					if (base.BodyScanner.Fragments[i - 1].Category == ConversationBodyScanner.Scanner.FragmentCategory.MsHeader || base.BodyScanner.Fragments[i - 1].Category == ConversationBodyScanner.Scanner.FragmentCategory.NonMsHeader)
					{
						num = (int)base.BodyScanner.Fragments[i - 1].FirstLine;
						FragmentInfo.TrimBoundary(base.BodyScanner, ref startLineIndex, ref num);
						flag = true;
						break;
					}
				}
				else if ((int)base.BodyScanner.Fragments[i].FirstLine > num)
				{
					break;
				}
				i++;
			}
			if (!flag && num > 0 && base.BodyScanner.Lines[num - 1].Category == ConversationBodyScanner.Scanner.LineCategory.PotentialNonMsHeader)
			{
				num--;
			}
			return new BodyFragmentInfo(base.BodyScanner, startLineIndex, num);
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x000062E4 File Offset: 0x000044E4
		private BodyFragmentInfo GetDisclaimerBodyPart(BodyFragmentInfo parentBodyFragment)
		{
			int endLineIndex = parentBodyFragment.EndLineIndex;
			int endLineIndex2 = base.EndLineIndex;
			FragmentInfo.TrimBoundary(base.BodyScanner, ref endLineIndex, ref endLineIndex2);
			return new BodyFragmentInfo(base.BodyScanner, endLineIndex, endLineIndex2);
		}

		// Token: 0x04000115 RID: 277
		private static readonly RGBT defaultFontColor = new RGBT(0U, 0U, 255U);

		// Token: 0x04000116 RID: 278
		private BodyTagInfo bodyTagInfo;
	}
}
