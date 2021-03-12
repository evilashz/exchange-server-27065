using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Microsoft.Exchange.Net.MonitoringWebClient
{
	// Token: 0x02000793 RID: 1939
	internal static class ParsingUtility
	{
		// Token: 0x0600267C RID: 9852 RVA: 0x00051070 File Offset: 0x0004F270
		public static string ParseJsonField(HttpWebResponseWrapper response, string name)
		{
			if (string.IsNullOrEmpty(response.Body))
			{
				throw new MissingKeywordException(MonitoringWebClientStrings.MissingJsonVariable(name), response.Request, response, name);
			}
			Regex regex = new Regex(string.Format("{0}\\s*:\\s*['\"](?<value>[^'\"]*)['\"]", Regex.Escape(name)));
			Match match = regex.Match(response.Body);
			if (!match.Success)
			{
				throw new MissingKeywordException(MonitoringWebClientStrings.MissingJsonVariable(name), response.Request, response, name);
			}
			return match.Result("${value}");
		}

		// Token: 0x0600267D RID: 9853 RVA: 0x000510E8 File Offset: 0x0004F2E8
		public static string ParseJavascriptStringVariable(HttpWebResponseWrapper response, string name)
		{
			if (string.IsNullOrEmpty(response.Body))
			{
				throw new MissingKeywordException(MonitoringWebClientStrings.MissingJavascriptEmptyBody(name), response.Request, response, name);
			}
			string result;
			if (!ParsingUtility.TryParseJavascriptStringVariable(response, name, out result))
			{
				throw new MissingKeywordException(MonitoringWebClientStrings.MissingJavascriptVariable(name), response.Request, response, name);
			}
			return result;
		}

		// Token: 0x0600267E RID: 9854 RVA: 0x00051138 File Offset: 0x0004F338
		public static bool TryParseJavascriptStringVariable(HttpWebResponseWrapper response, string name, out string result)
		{
			if (string.IsNullOrEmpty(response.Body))
			{
				result = null;
				return false;
			}
			Regex regex = new Regex(string.Format("var {0}[\\s]*=[\\s]*['\"](?<value>[^'\"]*)['\"]", Regex.Escape(name)));
			Match match = regex.Match(response.Body);
			if (!match.Success)
			{
				result = null;
				return false;
			}
			result = match.Result("${value}");
			return true;
		}

		// Token: 0x0600267F RID: 9855 RVA: 0x00051198 File Offset: 0x0004F398
		public static bool TryParseQueryParameter(Uri uri, string name, out string result)
		{
			if (uri == null)
			{
				result = null;
				return false;
			}
			Regex regex = new Regex(string.Format("{0}=(?<value>[^;]*)", Regex.Escape(name)));
			Match match = regex.Match(uri.Query);
			if (!match.Success)
			{
				result = null;
				return false;
			}
			result = match.Result("${value}");
			return true;
		}

		// Token: 0x06002680 RID: 9856 RVA: 0x000511F4 File Offset: 0x0004F3F4
		public static Dictionary<string, string> ParseHiddenFields(HttpWebResponseWrapper response)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			if (response.Body == null)
			{
				return dictionary;
			}
			Regex regex = new Regex("<input.*?type=[\"']hidden[\"'].*?name=[\"'](?<Name>[^\"']*)[\"'].*?value=[\"'](?<Value>[^\"']*)[\"'].*?[/]*>", RegexOptions.IgnoreCase);
			MatchCollection matchCollection = regex.Matches(response.Body);
			foreach (object obj in matchCollection)
			{
				Match match = (Match)obj;
				dictionary.Add(match.Result("${Name}"), HttpUtility.HtmlDecode(match.Result("${Value}")));
			}
			return dictionary;
		}

		// Token: 0x06002681 RID: 9857 RVA: 0x00051294 File Offset: 0x0004F494
		public static Dictionary<string, string> ParseInputFields(HttpWebResponseWrapper response)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			if (response.Body == null)
			{
				return dictionary;
			}
			Regex regex = new Regex("<input(.*?name=[\"'](?<Name1>[^\"']*)[\"'])?.*?type=[\"'](text|password|hidden|submit)[\"'].*?(.*?name=[\"'](?<Name2>[^\"']*)[\"'])?(.*?value=[\"'](?<Value>[^\"']*)[\"'].*?[/]*)?", RegexOptions.IgnoreCase);
			MatchCollection matchCollection = regex.Matches(response.Body);
			foreach (object obj in matchCollection)
			{
				Match match = (Match)obj;
				string text = match.Result("${Name1}");
				if (string.IsNullOrEmpty(text))
				{
					text = match.Result("${Name2}");
				}
				string text2 = match.Result("${Value}");
				if (!string.IsNullOrEmpty(text2))
				{
					text2 = HttpUtility.HtmlDecode(text2);
				}
				dictionary.Add(text, text2);
			}
			return dictionary;
		}

		// Token: 0x06002682 RID: 9858 RVA: 0x00051360 File Offset: 0x0004F560
		public static string ParseFormAction(HttpWebResponseWrapper response)
		{
			if (response.Body == null)
			{
				return null;
			}
			string regexString = "action[\\s]*=(?:(?:\\s*(?<value>[^'\"\\s]+)\\s)|[\\s]*[\"](?<value>[^\"]*)[\"]|[\\s]*['](?<value>[^']*)['])";
			return HttpUtility.HtmlDecode(ParsingUtility.ParseRegexResult(response, regexString, "${value}"));
		}

		// Token: 0x06002683 RID: 9859 RVA: 0x00051390 File Offset: 0x0004F590
		public static string ParseFormDestination(HttpWebResponseWrapper response)
		{
			string regexString = "<input\\s+type=\"hidden\"\\s+name=\"destination\"\\s+value=\"(?<value>.*?)\">";
			return ParsingUtility.ParseRegexResult(response, regexString, "${value}");
		}

		// Token: 0x06002684 RID: 9860 RVA: 0x000513B0 File Offset: 0x0004F5B0
		public static string ParseFilePath(HttpWebResponseWrapper response, string fileName)
		{
			string regexString = string.Format("src=[\"'](?<path>[^\\s'\"]+?{0})[\"']", Regex.Escape(fileName));
			return ParsingUtility.ParseRegexResult(response, regexString, "${path}");
		}

		// Token: 0x06002685 RID: 9861 RVA: 0x000513DC File Offset: 0x0004F5DC
		public static string ParseInnerHtml(HttpWebResponseWrapper response, string elementName, string elementId)
		{
			int num = ParsingUtility.FindOccurrence(response, "id=[\"']+" + elementId);
			if (num < 0)
			{
				return null;
			}
			int num2 = response.Body.IndexOf('>', num);
			if (num2 < 0)
			{
				return null;
			}
			int num3 = response.Body.IndexOf(string.Format("</{0}>", elementName), num2, StringComparison.InvariantCultureIgnoreCase);
			if (num3 < 0)
			{
				return null;
			}
			return response.Body.Substring(num2 + 1, num3 - num2);
		}

		// Token: 0x06002686 RID: 9862 RVA: 0x00051448 File Offset: 0x0004F648
		public static string RemoveHtmlTags(string html)
		{
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = false;
			foreach (char c in html)
			{
				if (c == '<')
				{
					flag = true;
					stringBuilder.Append(" ");
				}
				else if (c == '>')
				{
					flag = false;
				}
				else if (!flag)
				{
					stringBuilder.Append(c);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002687 RID: 9863 RVA: 0x000514A8 File Offset: 0x0004F6A8
		private static string ParseRegexResult(HttpWebResponseWrapper response, string regexString, string resultString)
		{
			Regex regex = new Regex(regexString, RegexOptions.Compiled);
			Match match = regex.Match(response.Body);
			if (!match.Success)
			{
				return null;
			}
			return match.Result(resultString);
		}

		// Token: 0x06002688 RID: 9864 RVA: 0x000514DC File Offset: 0x0004F6DC
		private static int FindOccurrence(HttpWebResponseWrapper response, string regexString)
		{
			if (response.Body == null)
			{
				return -2;
			}
			Regex regex = new Regex(regexString, RegexOptions.Compiled);
			Match match = regex.Match(response.Body);
			if (!match.Success)
			{
				return -1;
			}
			return match.Index;
		}

		// Token: 0x06002689 RID: 9865 RVA: 0x0005151C File Offset: 0x0004F71C
		public static string JavascriptDecode(string s)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s", "String to javascript decode can not be null");
			}
			StringBuilder sb = new StringBuilder();
			string result;
			using (StringWriter stringWriter = new StringWriter(sb))
			{
				for (int i = 0; i < s.Length; i++)
				{
					if (s[i] == '\\')
					{
						if (i + 1 < s.Length)
						{
							char c = s[i + 1];
							if (c <= '>')
							{
								if (c <= '\'')
								{
									switch (c)
									{
									case '!':
									case '"':
										break;
									default:
										if (c != '\'')
										{
											goto IL_130;
										}
										break;
									}
								}
								else if (c != '/')
								{
									switch (c)
									{
									case '<':
									case '>':
										break;
									case '=':
										goto IL_130;
									default:
										goto IL_130;
									}
								}
								stringWriter.Write(s[i + 1]);
								i++;
							}
							else if (c <= 'n')
							{
								if (c != '\\')
								{
									if (c == 'n')
									{
										stringWriter.Write('\n');
										i++;
									}
								}
								else
								{
									stringWriter.Write('\\');
									i++;
								}
							}
							else if (c != 'r')
							{
								if (c == 'u')
								{
									string s2 = s.Substring(i + 2, 4);
									int num = int.Parse(s2, NumberStyles.HexNumber);
									stringWriter.Write((char)num);
									i += 5;
								}
							}
							else
							{
								stringWriter.Write('\r');
								i++;
							}
						}
					}
					else
					{
						stringWriter.Write(s[i]);
					}
					IL_130:;
				}
				result = stringWriter.ToString();
			}
			return result;
		}
	}
}
