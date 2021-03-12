using System;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;

namespace System.Security.Util
{
	// Token: 0x02000353 RID: 851
	[Serializable]
	internal sealed class URLString : SiteString
	{
		// Token: 0x06002B56 RID: 11094 RVA: 0x000A154A File Offset: 0x0009F74A
		[OnDeserialized]
		public void OnDeserialized(StreamingContext ctx)
		{
			if (this.m_urlOriginal == null)
			{
				this.m_parseDeferred = false;
				this.m_parsedOriginal = false;
				this.m_userpass = "";
				this.m_urlOriginal = this.m_fullurl;
				this.m_fullurl = null;
			}
		}

		// Token: 0x06002B57 RID: 11095 RVA: 0x000A1580 File Offset: 0x0009F780
		[OnSerializing]
		private void OnSerializing(StreamingContext ctx)
		{
			if ((ctx.State & ~(StreamingContextStates.Clone | StreamingContextStates.CrossAppDomain)) != (StreamingContextStates)0)
			{
				this.DoDeferredParse();
				this.m_fullurl = this.m_urlOriginal;
			}
		}

		// Token: 0x06002B58 RID: 11096 RVA: 0x000A15A3 File Offset: 0x0009F7A3
		[OnSerialized]
		private void OnSerialized(StreamingContext ctx)
		{
			if ((ctx.State & ~(StreamingContextStates.Clone | StreamingContextStates.CrossAppDomain)) != (StreamingContextStates)0)
			{
				this.m_fullurl = null;
			}
		}

		// Token: 0x06002B59 RID: 11097 RVA: 0x000A15BC File Offset: 0x0009F7BC
		public URLString()
		{
			this.m_protocol = "";
			this.m_userpass = "";
			this.m_siteString = new SiteString();
			this.m_port = -1;
			this.m_localSite = null;
			this.m_directory = new DirectoryString();
			this.m_parseDeferred = false;
		}

		// Token: 0x06002B5A RID: 11098 RVA: 0x000A1610 File Offset: 0x0009F810
		private void DoDeferredParse()
		{
			if (this.m_parseDeferred)
			{
				this.ParseString(this.m_urlOriginal, this.m_parsedOriginal);
				this.m_parseDeferred = false;
			}
		}

		// Token: 0x06002B5B RID: 11099 RVA: 0x000A1633 File Offset: 0x0009F833
		public URLString(string url) : this(url, false, false)
		{
		}

		// Token: 0x06002B5C RID: 11100 RVA: 0x000A163E File Offset: 0x0009F83E
		public URLString(string url, bool parsed) : this(url, parsed, false)
		{
		}

		// Token: 0x06002B5D RID: 11101 RVA: 0x000A1649 File Offset: 0x0009F849
		internal URLString(string url, bool parsed, bool doDeferredParsing)
		{
			this.m_port = -1;
			this.m_userpass = "";
			this.DoFastChecks(url);
			this.m_urlOriginal = url;
			this.m_parsedOriginal = parsed;
			this.m_parseDeferred = true;
			if (doDeferredParsing)
			{
				this.DoDeferredParse();
			}
		}

		// Token: 0x06002B5E RID: 11102 RVA: 0x000A1688 File Offset: 0x0009F888
		private string UnescapeURL(string url)
		{
			StringBuilder stringBuilder = StringBuilderCache.Acquire(url.Length);
			int num = 0;
			int num2 = -1;
			int num3 = -1;
			num2 = url.IndexOf('[', num);
			if (num2 != -1)
			{
				num3 = url.IndexOf(']', num2);
			}
			for (;;)
			{
				int num4 = url.IndexOf('%', num);
				if (num4 == -1)
				{
					break;
				}
				if (num4 > num2 && num4 < num3)
				{
					stringBuilder = stringBuilder.Append(url, num, num3 - num + 1);
					num = num3 + 1;
				}
				else
				{
					if (url.Length - num4 < 2)
					{
						goto Block_5;
					}
					if (url[num4 + 1] == 'u' || url[num4 + 1] == 'U')
					{
						if (url.Length - num4 < 6)
						{
							goto Block_7;
						}
						try
						{
							char value = (char)(Hex.ConvertHexDigit(url[num4 + 2]) << 12 | Hex.ConvertHexDigit(url[num4 + 3]) << 8 | Hex.ConvertHexDigit(url[num4 + 4]) << 4 | Hex.ConvertHexDigit(url[num4 + 5]));
							stringBuilder = stringBuilder.Append(url, num, num4 - num);
							stringBuilder = stringBuilder.Append(value);
						}
						catch (ArgumentException)
						{
							throw new ArgumentException(Environment.GetResourceString("Argument_InvalidUrl"));
						}
						num = num4 + 6;
					}
					else
					{
						if (url.Length - num4 < 3)
						{
							goto Block_9;
						}
						try
						{
							char value2 = (char)(Hex.ConvertHexDigit(url[num4 + 1]) << 4 | Hex.ConvertHexDigit(url[num4 + 2]));
							stringBuilder = stringBuilder.Append(url, num, num4 - num);
							stringBuilder = stringBuilder.Append(value2);
						}
						catch (ArgumentException)
						{
							throw new ArgumentException(Environment.GetResourceString("Argument_InvalidUrl"));
						}
						num = num4 + 3;
					}
				}
			}
			stringBuilder = stringBuilder.Append(url, num, url.Length - num);
			return StringBuilderCache.GetStringAndRelease(stringBuilder);
			Block_5:
			throw new ArgumentException(Environment.GetResourceString("Argument_InvalidUrl"));
			Block_7:
			throw new ArgumentException(Environment.GetResourceString("Argument_InvalidUrl"));
			Block_9:
			throw new ArgumentException(Environment.GetResourceString("Argument_InvalidUrl"));
		}

		// Token: 0x06002B5F RID: 11103 RVA: 0x000A1860 File Offset: 0x0009FA60
		private string ParseProtocol(string url)
		{
			int num = url.IndexOf(':');
			if (num == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidUrl"));
			}
			string result;
			if (num == -1)
			{
				this.m_protocol = "file";
				result = url;
			}
			else
			{
				if (url.Length <= num + 1)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidUrl"));
				}
				if (num == "file".Length && string.Compare(url, 0, "file", 0, num, StringComparison.OrdinalIgnoreCase) == 0)
				{
					this.m_protocol = "file";
					result = url.Substring(num + 1);
					this.m_isUncShare = true;
				}
				else if (url[num + 1] != '\\')
				{
					if (url.Length <= num + 2 || url[num + 1] != '/' || url[num + 2] != '/')
					{
						throw new ArgumentException(Environment.GetResourceString("Argument_InvalidUrl"));
					}
					this.m_protocol = url.Substring(0, num);
					for (int i = 0; i < this.m_protocol.Length; i++)
					{
						char c = this.m_protocol[i];
						if ((c < 'a' || c > 'z') && (c < 'A' || c > 'Z') && (c < '0' || c > '9') && c != '+' && c != '.' && c != '-')
						{
							throw new ArgumentException(Environment.GetResourceString("Argument_InvalidUrl"));
						}
					}
					result = url.Substring(num + 3);
				}
				else
				{
					this.m_protocol = "file";
					result = url;
				}
			}
			return result;
		}

		// Token: 0x06002B60 RID: 11104 RVA: 0x000A19D4 File Offset: 0x0009FBD4
		private string ParsePort(string url)
		{
			char[] anyOf = new char[]
			{
				':',
				'/'
			};
			int num = 0;
			int num2 = url.IndexOf('@');
			if (num2 != -1 && url.IndexOf('/', 0, num2) == -1)
			{
				this.m_userpass = url.Substring(0, num2);
				num = num2 + 1;
			}
			int num3 = -1;
			int num4 = url.IndexOf('[', num);
			if (num4 != -1)
			{
				num3 = url.IndexOf(']', num4);
			}
			int num5;
			if (num3 != -1)
			{
				num5 = url.IndexOfAny(anyOf, num3);
			}
			else
			{
				num5 = url.IndexOfAny(anyOf, num);
			}
			string result;
			if (num5 != -1 && url[num5] == ':')
			{
				if (url[num5 + 1] < '0' || url[num5 + 1] > '9')
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidUrl"));
				}
				int num6 = url.IndexOf('/', num);
				if (num6 == -1)
				{
					this.m_port = int.Parse(url.Substring(num5 + 1), CultureInfo.InvariantCulture);
					if (this.m_port < 0)
					{
						throw new ArgumentException(Environment.GetResourceString("Argument_InvalidUrl"));
					}
					result = url.Substring(num, num5 - num);
				}
				else
				{
					if (num6 <= num5)
					{
						throw new ArgumentException(Environment.GetResourceString("Argument_InvalidUrl"));
					}
					this.m_port = int.Parse(url.Substring(num5 + 1, num6 - num5 - 1), CultureInfo.InvariantCulture);
					result = url.Substring(num, num5 - num) + url.Substring(num6);
				}
			}
			else
			{
				result = url.Substring(num);
			}
			return result;
		}

		// Token: 0x06002B61 RID: 11105 RVA: 0x000A1B58 File Offset: 0x0009FD58
		internal static string PreProcessForExtendedPathRemoval(string url, bool isFileUrl)
		{
			return URLString.PreProcessForExtendedPathRemoval(true, url, isFileUrl);
		}

		// Token: 0x06002B62 RID: 11106 RVA: 0x000A1B64 File Offset: 0x0009FD64
		internal static string PreProcessForExtendedPathRemoval(bool checkPathLength, string url, bool isFileUrl)
		{
			bool flag = false;
			return URLString.PreProcessForExtendedPathRemoval(checkPathLength, url, isFileUrl, ref flag);
		}

		// Token: 0x06002B63 RID: 11107 RVA: 0x000A1B7D File Offset: 0x0009FD7D
		private static string PreProcessForExtendedPathRemoval(string url, bool isFileUrl, ref bool isUncShare)
		{
			return URLString.PreProcessForExtendedPathRemoval(true, url, isFileUrl, ref isUncShare);
		}

		// Token: 0x06002B64 RID: 11108 RVA: 0x000A1B88 File Offset: 0x0009FD88
		private static string PreProcessForExtendedPathRemoval(bool checkPathLength, string url, bool isFileUrl, ref bool isUncShare)
		{
			StringBuilder stringBuilder = new StringBuilder(url);
			int num = 0;
			int num2 = 0;
			if (url.Length - num >= 4 && (string.Compare(url, num, "//?/", 0, 4, StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(url, num, "//./", 0, 4, StringComparison.OrdinalIgnoreCase) == 0))
			{
				stringBuilder.Remove(num2, 4);
				num += 4;
			}
			else
			{
				if (isFileUrl)
				{
					while (url[num] == '/')
					{
						num++;
						num2++;
					}
				}
				if (url.Length - num >= 4 && (string.Compare(url, num, "\\\\?\\", 0, 4, StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(url, num, "\\\\?/", 0, 4, StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(url, num, "\\\\.\\", 0, 4, StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(url, num, "\\\\./", 0, 4, StringComparison.OrdinalIgnoreCase) == 0))
				{
					stringBuilder.Remove(num2, 4);
					num += 4;
				}
			}
			if (isFileUrl)
			{
				int num3 = 0;
				bool flag = false;
				while (num3 < stringBuilder.Length && (stringBuilder[num3] == '/' || stringBuilder[num3] == '\\'))
				{
					if (!flag && stringBuilder[num3] == '\\')
					{
						flag = true;
						if (num3 + 1 < stringBuilder.Length && stringBuilder[num3 + 1] == '\\')
						{
							isUncShare = true;
						}
					}
					num3++;
				}
				stringBuilder.Remove(0, num3);
				stringBuilder.Replace('\\', '/');
			}
			if (checkPathLength)
			{
				URLString.CheckPathTooLong(stringBuilder);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002B65 RID: 11109 RVA: 0x000A1CCD File Offset: 0x0009FECD
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static void CheckPathTooLong(StringBuilder path)
		{
			if (path.Length >= (AppContextSwitches.BlockLongPaths ? 260 : 32767))
			{
				throw new PathTooLongException(Environment.GetResourceString("IO.PathTooLong"));
			}
		}

		// Token: 0x06002B66 RID: 11110 RVA: 0x000A1CFA File Offset: 0x0009FEFA
		private string PreProcessURL(string url, bool isFileURL)
		{
			if (isFileURL)
			{
				url = URLString.PreProcessForExtendedPathRemoval(url, true, ref this.m_isUncShare);
			}
			else
			{
				url = url.Replace('\\', '/');
			}
			return url;
		}

		// Token: 0x06002B67 RID: 11111 RVA: 0x000A1D20 File Offset: 0x0009FF20
		private void ParseFileURL(string url)
		{
			int num = url.IndexOf('/');
			if (num != -1 && ((num == 2 && url[num - 1] != ':' && url[num - 1] != '|') || num != 2) && num != url.Length - 1)
			{
				int num2 = url.IndexOf('/', num + 1);
				if (num2 != -1)
				{
					num = num2;
				}
				else
				{
					num = -1;
				}
			}
			string text;
			if (num == -1)
			{
				text = url;
			}
			else
			{
				text = url.Substring(0, num);
			}
			if (text.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidUrl"));
			}
			bool flag;
			int i;
			if (text[0] == '\\' && text[1] == '\\')
			{
				flag = true;
				i = 2;
			}
			else
			{
				i = 0;
				flag = false;
			}
			bool flag2 = true;
			while (i < text.Length)
			{
				char c = text[i];
				if ((c < 'A' || c > 'Z') && (c < 'a' || c > 'z') && (c < '0' || c > '9') && c != '-' && c != '/' && c != ':' && c != '|' && c != '.' && c != '*' && c != '$' && (!flag || c != ' '))
				{
					flag2 = false;
					break;
				}
				i++;
			}
			if (flag2)
			{
				text = string.SmallCharToUpper(text);
			}
			else
			{
				text = text.ToUpper(CultureInfo.InvariantCulture);
			}
			this.m_localSite = new LocalSiteString(text);
			if (num == -1)
			{
				if (text[text.Length - 1] == '*')
				{
					this.m_directory = new DirectoryString("*", false);
				}
				else
				{
					this.m_directory = new DirectoryString();
				}
			}
			else
			{
				string text2 = url.Substring(num + 1);
				if (text2.Length == 0)
				{
					this.m_directory = new DirectoryString();
				}
				else
				{
					this.m_directory = new DirectoryString(text2, true);
				}
			}
			this.m_siteString = null;
		}

		// Token: 0x06002B68 RID: 11112 RVA: 0x000A1ED8 File Offset: 0x000A00D8
		private void ParseNonFileURL(string url)
		{
			int num = url.IndexOf('/');
			if (num == -1)
			{
				this.m_localSite = null;
				this.m_siteString = new SiteString(url);
				this.m_directory = new DirectoryString();
				return;
			}
			string site = url.Substring(0, num);
			this.m_localSite = null;
			this.m_siteString = new SiteString(site);
			string text = url.Substring(num + 1);
			if (text.Length == 0)
			{
				this.m_directory = new DirectoryString();
				return;
			}
			this.m_directory = new DirectoryString(text, false);
		}

		// Token: 0x06002B69 RID: 11113 RVA: 0x000A1F5A File Offset: 0x000A015A
		private void DoFastChecks(string url)
		{
			if (url == null)
			{
				throw new ArgumentNullException("url");
			}
			if (url.Length == 0)
			{
				throw new FormatException(Environment.GetResourceString("Format_StringZeroLength"));
			}
		}

		// Token: 0x06002B6A RID: 11114 RVA: 0x000A1F84 File Offset: 0x000A0184
		private void ParseString(string url, bool parsed)
		{
			if (!parsed)
			{
				url = this.UnescapeURL(url);
			}
			string url2 = this.ParseProtocol(url);
			bool flag = string.Compare(this.m_protocol, "file", StringComparison.OrdinalIgnoreCase) == 0;
			url2 = this.PreProcessURL(url2, flag);
			if (flag)
			{
				this.ParseFileURL(url2);
				return;
			}
			url2 = this.ParsePort(url2);
			this.ParseNonFileURL(url2);
		}

		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x06002B6B RID: 11115 RVA: 0x000A1FDD File Offset: 0x000A01DD
		public string Scheme
		{
			get
			{
				this.DoDeferredParse();
				return this.m_protocol;
			}
		}

		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x06002B6C RID: 11116 RVA: 0x000A1FEB File Offset: 0x000A01EB
		public string Host
		{
			get
			{
				this.DoDeferredParse();
				if (this.m_siteString != null)
				{
					return this.m_siteString.ToString();
				}
				return this.m_localSite.ToString();
			}
		}

		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x06002B6D RID: 11117 RVA: 0x000A2012 File Offset: 0x000A0212
		public string Port
		{
			get
			{
				this.DoDeferredParse();
				if (this.m_port == -1)
				{
					return null;
				}
				return this.m_port.ToString(CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x06002B6E RID: 11118 RVA: 0x000A2035 File Offset: 0x000A0235
		public string Directory
		{
			get
			{
				this.DoDeferredParse();
				return this.m_directory.ToString();
			}
		}

		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x06002B6F RID: 11119 RVA: 0x000A2048 File Offset: 0x000A0248
		public bool IsRelativeFileUrl
		{
			get
			{
				this.DoDeferredParse();
				if (!string.Equals(this.m_protocol, "file", StringComparison.OrdinalIgnoreCase) || this.m_isUncShare)
				{
					return false;
				}
				string text = (this.m_localSite != null) ? this.m_localSite.ToString() : null;
				if (text.EndsWith('*'))
				{
					return false;
				}
				string value = (this.m_directory != null) ? this.m_directory.ToString() : null;
				return text == null || text.Length < 2 || !text.EndsWith(':') || string.IsNullOrEmpty(value);
			}
		}

		// Token: 0x06002B70 RID: 11120 RVA: 0x000A20D0 File Offset: 0x000A02D0
		public string GetFileName()
		{
			this.DoDeferredParse();
			if (string.Compare(this.m_protocol, "file", StringComparison.OrdinalIgnoreCase) != 0)
			{
				return null;
			}
			string str = this.Directory.Replace('/', '\\');
			string text = this.Host.Replace('/', '\\');
			int num = text.IndexOf('\\');
			if (num == -1)
			{
				if (text.Length != 2 || (text[1] != ':' && text[1] != '|'))
				{
					text = "\\\\" + text;
				}
			}
			else if (num != 2 || (num == 2 && text[1] != ':' && text[1] != '|'))
			{
				text = "\\\\" + text;
			}
			return text + "\\" + str;
		}

		// Token: 0x06002B71 RID: 11121 RVA: 0x000A218C File Offset: 0x000A038C
		public string GetDirectoryName()
		{
			this.DoDeferredParse();
			if (string.Compare(this.m_protocol, "file", StringComparison.OrdinalIgnoreCase) != 0)
			{
				return null;
			}
			string text = this.Directory.Replace('/', '\\');
			int num = 0;
			for (int i = text.Length; i > 0; i--)
			{
				if (text[i - 1] == '\\')
				{
					num = i;
					break;
				}
			}
			string text2 = this.Host.Replace('/', '\\');
			int num2 = text2.IndexOf('\\');
			if (num2 == -1)
			{
				if (text2.Length != 2 || (text2[1] != ':' && text2[1] != '|'))
				{
					text2 = "\\\\" + text2;
				}
			}
			else if (num2 > 2 || (num2 == 2 && text2[1] != ':' && text2[1] != '|'))
			{
				text2 = "\\\\" + text2;
			}
			text2 += "\\";
			if (num > 0)
			{
				text2 += text.Substring(0, num);
			}
			return text2;
		}

		// Token: 0x06002B72 RID: 11122 RVA: 0x000A2282 File Offset: 0x000A0482
		public override SiteString Copy()
		{
			return new URLString(this.m_urlOriginal, this.m_parsedOriginal);
		}

		// Token: 0x06002B73 RID: 11123 RVA: 0x000A2298 File Offset: 0x000A0498
		public override bool IsSubsetOf(SiteString site)
		{
			if (site == null)
			{
				return false;
			}
			URLString urlstring = site as URLString;
			if (urlstring == null)
			{
				return false;
			}
			this.DoDeferredParse();
			urlstring.DoDeferredParse();
			URLString urlstring2 = this.SpecialNormalizeUrl();
			URLString urlstring3 = urlstring.SpecialNormalizeUrl();
			if (string.Compare(urlstring2.m_protocol, urlstring3.m_protocol, StringComparison.OrdinalIgnoreCase) != 0 || !urlstring2.m_directory.IsSubsetOf(urlstring3.m_directory))
			{
				return false;
			}
			if (urlstring2.m_localSite != null)
			{
				return urlstring2.m_localSite.IsSubsetOf(urlstring3.m_localSite);
			}
			return urlstring2.m_port == urlstring3.m_port && urlstring3.m_siteString != null && urlstring2.m_siteString.IsSubsetOf(urlstring3.m_siteString);
		}

		// Token: 0x06002B74 RID: 11124 RVA: 0x000A233E File Offset: 0x000A053E
		public override string ToString()
		{
			return this.m_urlOriginal;
		}

		// Token: 0x06002B75 RID: 11125 RVA: 0x000A2346 File Offset: 0x000A0546
		public override bool Equals(object o)
		{
			this.DoDeferredParse();
			return o != null && o is URLString && this.Equals((URLString)o);
		}

		// Token: 0x06002B76 RID: 11126 RVA: 0x000A2368 File Offset: 0x000A0568
		public override int GetHashCode()
		{
			this.DoDeferredParse();
			TextInfo textInfo = CultureInfo.InvariantCulture.TextInfo;
			int num = 0;
			if (this.m_protocol != null)
			{
				num = textInfo.GetCaseInsensitiveHashCode(this.m_protocol);
			}
			if (this.m_localSite != null)
			{
				num ^= this.m_localSite.GetHashCode();
			}
			else
			{
				num ^= this.m_siteString.GetHashCode();
			}
			return num ^ this.m_directory.GetHashCode();
		}

		// Token: 0x06002B77 RID: 11127 RVA: 0x000A23D2 File Offset: 0x000A05D2
		public bool Equals(URLString url)
		{
			return URLString.CompareUrls(this, url);
		}

		// Token: 0x06002B78 RID: 11128 RVA: 0x000A23DC File Offset: 0x000A05DC
		public static bool CompareUrls(URLString url1, URLString url2)
		{
			if (url1 == null && url2 == null)
			{
				return true;
			}
			if (url1 == null || url2 == null)
			{
				return false;
			}
			url1.DoDeferredParse();
			url2.DoDeferredParse();
			URLString urlstring = url1.SpecialNormalizeUrl();
			URLString urlstring2 = url2.SpecialNormalizeUrl();
			if (string.Compare(urlstring.m_protocol, urlstring2.m_protocol, StringComparison.OrdinalIgnoreCase) != 0)
			{
				return false;
			}
			if (string.Compare(urlstring.m_protocol, "file", StringComparison.OrdinalIgnoreCase) == 0)
			{
				if (!urlstring.m_localSite.IsSubsetOf(urlstring2.m_localSite) || !urlstring2.m_localSite.IsSubsetOf(urlstring.m_localSite))
				{
					return false;
				}
			}
			else
			{
				if (string.Compare(urlstring.m_userpass, urlstring2.m_userpass, StringComparison.Ordinal) != 0)
				{
					return false;
				}
				if (!urlstring.m_siteString.IsSubsetOf(urlstring2.m_siteString) || !urlstring2.m_siteString.IsSubsetOf(urlstring.m_siteString))
				{
					return false;
				}
				if (url1.m_port != url2.m_port)
				{
					return false;
				}
			}
			return urlstring.m_directory.IsSubsetOf(urlstring2.m_directory) && urlstring2.m_directory.IsSubsetOf(urlstring.m_directory);
		}

		// Token: 0x06002B79 RID: 11129 RVA: 0x000A24DC File Offset: 0x000A06DC
		internal string NormalizeUrl()
		{
			this.DoDeferredParse();
			StringBuilder stringBuilder = StringBuilderCache.Acquire(16);
			if (string.Compare(this.m_protocol, "file", StringComparison.OrdinalIgnoreCase) == 0)
			{
				stringBuilder = stringBuilder.AppendFormat("FILE:///{0}/{1}", this.m_localSite.ToString(), this.m_directory.ToString());
			}
			else
			{
				stringBuilder = stringBuilder.AppendFormat("{0}://{1}{2}", this.m_protocol, this.m_userpass, this.m_siteString.ToString());
				if (this.m_port != -1)
				{
					stringBuilder = stringBuilder.AppendFormat("{0}", this.m_port);
				}
				stringBuilder = stringBuilder.AppendFormat("/{0}", this.m_directory.ToString());
			}
			return StringBuilderCache.GetStringAndRelease(stringBuilder).ToUpper(CultureInfo.InvariantCulture);
		}

		// Token: 0x06002B7A RID: 11130 RVA: 0x000A2598 File Offset: 0x000A0798
		[SecuritySafeCritical]
		internal URLString SpecialNormalizeUrl()
		{
			this.DoDeferredParse();
			if (string.Compare(this.m_protocol, "file", StringComparison.OrdinalIgnoreCase) != 0)
			{
				return this;
			}
			string text = this.m_localSite.ToString();
			if (text.Length != 2 || (text[1] != '|' && text[1] != ':'))
			{
				return this;
			}
			string text2 = null;
			URLString.GetDeviceName(text, JitHelpers.GetStringHandleOnStack(ref text2));
			if (text2 == null)
			{
				return this;
			}
			if (text2.IndexOf("://", StringComparison.Ordinal) != -1)
			{
				URLString urlstring = new URLString(text2 + "/" + this.m_directory.ToString());
				urlstring.DoDeferredParse();
				return urlstring;
			}
			URLString urlstring2 = new URLString("file://" + text2 + "/" + this.m_directory.ToString());
			urlstring2.DoDeferredParse();
			return urlstring2;
		}

		// Token: 0x06002B7B RID: 11131
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void GetDeviceName(string driveLetter, StringHandleOnStack retDeviceName);

		// Token: 0x04001120 RID: 4384
		private string m_protocol;

		// Token: 0x04001121 RID: 4385
		[OptionalField(VersionAdded = 2)]
		private string m_userpass;

		// Token: 0x04001122 RID: 4386
		private SiteString m_siteString;

		// Token: 0x04001123 RID: 4387
		private int m_port;

		// Token: 0x04001124 RID: 4388
		private LocalSiteString m_localSite;

		// Token: 0x04001125 RID: 4389
		private DirectoryString m_directory;

		// Token: 0x04001126 RID: 4390
		private const string m_defaultProtocol = "file";

		// Token: 0x04001127 RID: 4391
		[OptionalField(VersionAdded = 2)]
		private bool m_parseDeferred;

		// Token: 0x04001128 RID: 4392
		[OptionalField(VersionAdded = 2)]
		private string m_urlOriginal;

		// Token: 0x04001129 RID: 4393
		[OptionalField(VersionAdded = 2)]
		private bool m_parsedOriginal;

		// Token: 0x0400112A RID: 4394
		[OptionalField(VersionAdded = 3)]
		private bool m_isUncShare;

		// Token: 0x0400112B RID: 4395
		private string m_fullurl;
	}
}
