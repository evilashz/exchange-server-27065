using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x02000157 RID: 343
	internal static class SpeechUtils
	{
		// Token: 0x06000B0A RID: 2826 RVA: 0x000295F8 File Offset: 0x000277F8
		internal static string SrgsEncode(string inText)
		{
			if (string.IsNullOrEmpty(inText))
			{
				return string.Empty;
			}
			return SpeechUtils.XmlEncode(inText.Replace("\"", "'"));
		}

		// Token: 0x06000B0B RID: 2827 RVA: 0x0002961D File Offset: 0x0002781D
		internal static string XmlEncode(string inText)
		{
			return SpeechUtils.XmlEncode(string.Empty, inText, string.Empty);
		}

		// Token: 0x06000B0C RID: 2828 RVA: 0x00029630 File Offset: 0x00027830
		internal static string XmlEncode(string prefix, string body, string suffix)
		{
			StringBuilder stringBuilder = null;
			body = (body ?? string.Empty);
			prefix = (prefix ?? string.Empty);
			suffix = (suffix ?? string.Empty);
			MatchCollection matchCollection = Regex.Matches(body, "[\r\n<>&'\"\\x00-\\x09\\x0b-\\x0c\\x0e-\\x1f\\x7f]");
			int num = prefix.Length + body.Length + suffix.Length;
			if (matchCollection.Count == 0)
			{
				if (prefix.Length == 0 && suffix.Length == 0)
				{
					return body;
				}
				stringBuilder = new StringBuilder(num);
				stringBuilder.Append(prefix).Append(body).Append(suffix);
			}
			else
			{
				num += 6 * matchCollection.Count;
				stringBuilder = new StringBuilder(num);
				int num2 = 0;
				stringBuilder.Append(prefix);
				Match match = null;
				foreach (object obj in matchCollection)
				{
					Match match2 = (Match)obj;
					string value = string.Empty;
					char c = match2.Value[0];
					if (c <= '\r')
					{
						if (c != '\n')
						{
							if (c != '\r')
							{
								goto IL_18A;
							}
							value = "<p/>";
						}
						else if (match != null && '\r' == match.Value[0] && match.Index + 1 == match2.Index)
						{
							value = string.Empty;
						}
						else
						{
							value = "<p/>";
						}
					}
					else if (c != '"')
					{
						switch (c)
						{
						case '&':
							value = "&amp;";
							break;
						case '\'':
							value = "&apos;";
							break;
						default:
							switch (c)
							{
							case '<':
								value = "&lt;";
								break;
							case '=':
								goto IL_18A;
							case '>':
								value = "&gt;";
								break;
							default:
								goto IL_18A;
							}
							break;
						}
					}
					else
					{
						value = "&quot;";
					}
					IL_191:
					int length = match2.Index - num2;
					stringBuilder.Append(body.Substring(num2, length));
					stringBuilder.Append(value);
					num2 = match2.Index + match2.Length;
					match = match2;
					continue;
					IL_18A:
					value = " ";
					goto IL_191;
				}
				stringBuilder.Append(body.Substring(num2));
				stringBuilder.Append(suffix);
			}
			return stringBuilder.ToString();
		}
	}
}
