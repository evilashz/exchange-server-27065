using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Microsoft.Exchange.Services.Core.CssConverter
{
	// Token: 0x020000C1 RID: 193
	internal static class CssParser
	{
		// Token: 0x06000547 RID: 1351 RVA: 0x0001C577 File Offset: 0x0001A777
		public static CssStyleSheet Parse(TextReader reader)
		{
			return CssParser.Parse(reader.ReadToEnd());
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x0001C584 File Offset: 0x0001A784
		public static CssStyleSheet Parse(string cssString)
		{
			List<CssRule> list = new List<CssRule>();
			cssString = "\n" + cssString;
			cssString = CssParser.HtmlCommentDelimitersRegex.Replace(cssString, string.Empty);
			cssString = CssParser.CssCommentsRegex.Replace(cssString, string.Empty);
			IEnumerable<CssFragment> enumerable = CssParser.SplitCssByMedia(cssString);
			foreach (CssFragment cssFragment in enumerable)
			{
				if (cssFragment.MediaDevices.Count == 0 || cssFragment.MediaDevices.Contains("screen"))
				{
					list.AddRange(CssParser.ParseDeclarations(cssFragment.CssText));
				}
			}
			return new CssStyleSheet(list);
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x0001C63C File Offset: 0x0001A83C
		private static IEnumerable<CssFragment> SplitCssByMedia(string cssString)
		{
			bool flag = false;
			int num = 0;
			int num2 = 0;
			List<CssFragment> list = new List<CssFragment>();
			string text = "@";
			int num3;
			string text2;
			for (int i = 0; i < cssString.Length; i++)
			{
				if (flag)
				{
					if (cssString[i] != '{' && num == 0)
					{
						text += cssString[i];
					}
					else if (cssString[i] == '{')
					{
						if (num == 0)
						{
							num2 = i + 1;
						}
						num++;
					}
					else if (cssString[i] == '}')
					{
						num--;
						if (num == 0)
						{
							num3 = i;
							flag = false;
							string[] array = text.ToLowerInvariant().Split(CssParser.SpaceDelimiter, 2);
							IList<string> mediaDevices = CssParser.SplitToList(array[1], CssParser.CommaSeparator, int.MaxValue);
							int length = num3 - num2;
							text2 = cssString.Substring(num2, length).Trim();
							if (!string.IsNullOrEmpty(text2))
							{
								list.Add(new CssFragment(mediaDevices, text2));
							}
							num2 = i + 1;
						}
					}
				}
				else if (cssString.Length >= i + 6 && cssString.Substring(i, 6).ToLowerInvariant() == "@media")
				{
					num3 = i;
					text2 = cssString.Substring(num2, num3 - num2).Trim();
					if (!string.IsNullOrEmpty(text2))
					{
						list.Add(new CssFragment(new List<string>(), text2));
					}
					flag = true;
					num = 0;
					text = "@";
				}
			}
			num3 = cssString.Length;
			text2 = cssString.Substring(num2, num3 - num2).Trim();
			if (!string.IsNullOrEmpty(text2))
			{
				list.Add(new CssFragment(new List<string>(), text2));
			}
			return list;
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x0001C7D8 File Offset: 0x0001A9D8
		public static IList<CssRule> ParseDeclarations(string cssString)
		{
			MatchCollection matchCollection = CssParser.CssDefinitionRegex.Matches(cssString);
			IList<CssRule> list = new List<CssRule>();
			foreach (object obj in matchCollection)
			{
				Match match = (Match)obj;
				string strValue = match.Groups[1].ToString();
				string propertiesStr = match.Groups[2].ToString();
				IList<string> selectors = CssParser.SplitToList(strValue, CssParser.CommaSeparator, int.MaxValue);
				IList<CssProperty> list2 = CssParser.ParseProperties(propertiesStr);
				if (list2.Count > 0)
				{
					CssRule item = new CssRule(selectors, list2);
					list.Add(item);
				}
			}
			return list;
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x0001C8A4 File Offset: 0x0001AAA4
		public static IList<string> SplitToList(string strValue, char[] separators, int count = 2147483647)
		{
			string[] array = strValue.Split(separators, count, StringSplitOptions.RemoveEmptyEntries);
			if (array.Length <= 0)
			{
				return new List<string>();
			}
			return array.ToList<string>().ConvertAll<string>((string str) => str.Trim());
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x0001C8F0 File Offset: 0x0001AAF0
		public static IList<CssProperty> ParseProperties(string propertiesStr)
		{
			IList<string> list = CssParser.SplitToList(propertiesStr, CssParser.PropertyDelimiter, int.MaxValue);
			IList<CssProperty> list2 = new List<CssProperty>();
			foreach (string strValue in list)
			{
				IList<string> list3 = CssParser.SplitToList(strValue, CssParser.NameValueDelimiter, 2);
				if (list3.Count >= 2)
				{
					CssProperty item = new CssProperty
					{
						Name = list3[0],
						Value = list3[1]
					};
					list2.Add(item);
				}
			}
			return list2;
		}

		// Token: 0x0400067B RID: 1659
		private static readonly Regex HtmlCommentDelimitersRegex = new Regex("<!--|-->", RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.CultureInvariant);

		// Token: 0x0400067C RID: 1660
		private static readonly Regex CssCommentsRegex = new Regex("/\\*(.+?)\\*/", RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.CultureInvariant);

		// Token: 0x0400067D RID: 1661
		private static readonly Regex CssDefinitionRegex = new Regex("(.+?) *\\{(.*?)\\}", RegexOptions.Multiline | RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.CultureInvariant);

		// Token: 0x0400067E RID: 1662
		internal static readonly char[] CommaSeparator = new char[]
		{
			','
		};

		// Token: 0x0400067F RID: 1663
		internal static readonly char[] PropertyDelimiter = new char[]
		{
			';'
		};

		// Token: 0x04000680 RID: 1664
		internal static readonly char[] NameValueDelimiter = new char[]
		{
			':'
		};

		// Token: 0x04000681 RID: 1665
		internal static readonly char[] SpaceDelimiter = new char[]
		{
			' '
		};

		// Token: 0x04000682 RID: 1666
		internal static readonly char[] ClassDelimiter = new char[]
		{
			'.'
		};
	}
}
