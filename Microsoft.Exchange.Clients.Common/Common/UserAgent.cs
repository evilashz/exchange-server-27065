using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Web;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Common
{
	// Token: 0x0200002E RID: 46
	public class UserAgent
	{
		// Token: 0x06000138 RID: 312 RVA: 0x000090C1 File Offset: 0x000072C1
		public UserAgent(string userAgent, bool changeLayoutFeatureEnabled, HttpCookieCollection cookies)
		{
			this.userAgent = userAgent;
			this.layoutOverrideFeatureEnabled = changeLayoutFeatureEnabled;
			this.cookies = cookies;
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000139 RID: 313 RVA: 0x000090DE File Offset: 0x000072DE
		public string RawString
		{
			get
			{
				return this.userAgent;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600013A RID: 314 RVA: 0x000090E8 File Offset: 0x000072E8
		// (set) Token: 0x0600013B RID: 315 RVA: 0x00009200 File Offset: 0x00007400
		public LayoutType Layout
		{
			get
			{
				if (this.layout == null)
				{
					if (this.HasString("iPad") || (this.HasString("Android") && !this.HasString("Mobile")))
					{
						this.layout = new LayoutType?(LayoutType.TouchWide);
					}
					else if (this.HasString("iPhone") || this.HasString("Android") || this.HasString("mobile") || this.HasString("phone"))
					{
						this.layout = new LayoutType?(LayoutType.TouchNarrow);
					}
					else if (this.layoutOverrideFeatureEnabled)
					{
						HttpCookie httpCookie = this.cookies.Get("Layout");
						LayoutType value;
						if (httpCookie != null && Enum.TryParse<LayoutType>(httpCookie.Value, out value))
						{
							this.layout = new LayoutType?(value);
						}
						else if (this.IsBrowserIE() && this.HasString("ARM"))
						{
							this.layout = new LayoutType?(LayoutType.TouchWide);
						}
						else
						{
							this.layout = new LayoutType?(LayoutType.Mouse);
						}
					}
					else
					{
						this.layout = new LayoutType?(LayoutType.Mouse);
					}
				}
				return this.layout.Value;
			}
			set
			{
				this.layout = new LayoutType?(value);
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600013C RID: 316 RVA: 0x00009210 File Offset: 0x00007410
		public string LayoutString
		{
			get
			{
				switch (this.Layout)
				{
				case LayoutType.TouchNarrow:
					return "tnarrow";
				case LayoutType.TouchWide:
					return "twide";
				case LayoutType.Mouse:
					return "mouse";
				default:
					throw new InvalidProgramException();
				}
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600013D RID: 317 RVA: 0x00009254 File Offset: 0x00007454
		public string Platform
		{
			get
			{
				if (string.IsNullOrEmpty(this.platform))
				{
					for (int i = 0; i < UserAgent.clientPlatform.Length; i++)
					{
						if (-1 != this.userAgent.IndexOf(UserAgent.clientPlatform[i], StringComparison.OrdinalIgnoreCase) && (!UserAgent.clientPlatform[i].Equals("Macintosh") || (this.userAgent.IndexOf("iPhone", StringComparison.OrdinalIgnoreCase) < 0 && this.userAgent.IndexOf("iPad", StringComparison.OrdinalIgnoreCase) < 0 && this.userAgent.IndexOf("iPod", StringComparison.OrdinalIgnoreCase) < 0)) && (!UserAgent.clientPlatform[i].Equals("Linux") || this.userAgent.IndexOf("Android", StringComparison.OrdinalIgnoreCase) < 0))
						{
							this.platform = UserAgent.clientPlatform[i];
							break;
						}
					}
				}
				return this.platform;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600013E RID: 318 RVA: 0x00009330 File Offset: 0x00007530
		public string Browser
		{
			get
			{
				if (string.IsNullOrEmpty(this.browser))
				{
					for (int i = 0; i < UserAgent.browserList.Length; i++)
					{
						if (-1 != this.userAgent.IndexOf(UserAgent.browserList[i], StringComparison.OrdinalIgnoreCase) && (!string.Equals(UserAgent.browserList[i], "Safari", StringComparison.OrdinalIgnoreCase) || -1 == this.userAgent.IndexOf("Chrome", StringComparison.OrdinalIgnoreCase)))
						{
							this.browser = UserAgent.browserList[i];
							break;
						}
					}
					if (string.IsNullOrEmpty(this.browser) && this.userAgent.IndexOf("AppleWebKit") >= 0)
					{
						this.browser = "Safari";
					}
				}
				return this.browser;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600013F RID: 319 RVA: 0x000093DD File Offset: 0x000075DD
		public UserAgentVersion PlatformVersion
		{
			get
			{
				if (this.platformVersion == null)
				{
					this.ParsePlatformVersion();
				}
				return this.platformVersion;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000140 RID: 320 RVA: 0x000093F3 File Offset: 0x000075F3
		public UserAgentVersion BrowserVersion
		{
			get
			{
				if (this.browserVersion == null)
				{
					this.ParseBrowserVersion();
				}
				return this.browserVersion;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000141 RID: 321 RVA: 0x00009409 File Offset: 0x00007609
		public bool IsIos
		{
			get
			{
				return string.Equals("iPhone", this.Platform) || string.Equals("iPad", this.Platform);
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000142 RID: 322 RVA: 0x0000942F File Offset: 0x0000762F
		public bool IsAndroid
		{
			get
			{
				return string.Equals("Android", this.Platform);
			}
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00009441 File Offset: 0x00007641
		public bool IsOsWindows8OrLater()
		{
			return this.IsOsWindowsNtVersionOrLater(6, 2);
		}

		// Token: 0x06000144 RID: 324 RVA: 0x0000944C File Offset: 0x0000764C
		public bool IsOsWindowsNtVersionOrLater(int minMajorVersion, int minMinorVersion)
		{
			if (this.Platform != "Windows NT")
			{
				return false;
			}
			Match match = UserAgent.windowsNTVersionRegex.Match(this.RawString);
			int num;
			int num2;
			return match.Success && int.TryParse(match.Groups["majorVersion"].Value, out num) && int.TryParse(match.Groups["minorVersion"].Value, out num2) && (num > minMajorVersion || (num == minMajorVersion && num2 >= minMinorVersion));
		}

		// Token: 0x06000145 RID: 325 RVA: 0x000094D7 File Offset: 0x000076D7
		public bool IsBrowserIE10Plus()
		{
			return this.HasString("MSIE 1") || this.IsBrowserIE11Plus();
		}

		// Token: 0x06000146 RID: 326 RVA: 0x000094EE File Offset: 0x000076EE
		public bool IsBrowserIE11Plus()
		{
			return this.HasString("rv:") && this.HasString("Trident");
		}

		// Token: 0x06000147 RID: 327 RVA: 0x0000950A File Offset: 0x0000770A
		public bool IsBrowserIE()
		{
			if (this.isIE == null)
			{
				this.isIE = new bool?(this.HasString("MSIE") || this.IsBrowserIE11Plus());
			}
			return this.isIE.Value;
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00009545 File Offset: 0x00007745
		public bool IsBrowserIE8()
		{
			return this.IsBrowserIE() && this.BrowserVersion.Build == 8;
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00009560 File Offset: 0x00007760
		public double GetTridentVersion()
		{
			double result = 0.0;
			if (this.IsBrowserIE())
			{
				Match match = UserAgent.tridentVersionRegex.Match(this.RawString);
				if (match.Groups.Count == 2)
				{
					double.TryParse(match.Groups[1].Value, NumberStyles.AllowDecimalPoint, NumberFormatInfo.InvariantInfo, out result);
				}
			}
			return result;
		}

		// Token: 0x0600014A RID: 330 RVA: 0x000095BF File Offset: 0x000077BF
		public bool IsBrowserMobileIE()
		{
			return this.IsBrowserIE() && (this.HasString("IEMobile") || this.HasString("ZuneWP7") || this.HasString("WPDesktop"));
		}

		// Token: 0x0600014B RID: 331 RVA: 0x000095F2 File Offset: 0x000077F2
		public bool IsMobileIEDesktopMode()
		{
			return this.HasString("WPDesktop");
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00009600 File Offset: 0x00007800
		public bool IsBrowserFirefox()
		{
			if (this.isFirefox == null)
			{
				this.isFirefox = new bool?(this.HasString("Firefox") && !this.HasString("Trident"));
			}
			return this.isFirefox.Value;
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00009650 File Offset: 0x00007850
		public bool IsBrowserChrome()
		{
			if (this.isChrome == null)
			{
				this.isChrome = new bool?(this.HasString("Chrome") && !this.HasString("Trident"));
			}
			return this.isChrome.Value;
		}

		// Token: 0x0600014E RID: 334 RVA: 0x000096A0 File Offset: 0x000078A0
		public bool IsBrowserSafari()
		{
			if (this.isSafari == null)
			{
				this.isSafari = new bool?(this.HasString("Safari") && !this.HasString("Chrome") && !this.HasString("silk-accelerated") && !this.HasString("Trident"));
			}
			return this.isSafari.Value;
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00009708 File Offset: 0x00007908
		public void SetLayoutFromString(string layout)
		{
			if (string.IsNullOrEmpty(layout))
			{
				return;
			}
			layout = layout.ToLowerInvariant();
			if (layout == "mouse")
			{
				this.Layout = LayoutType.Mouse;
				return;
			}
			if (layout == "twide" || layout == "touchwide")
			{
				this.Layout = LayoutType.TouchWide;
				return;
			}
			if (layout == "tnarrow" || layout == "touchnarrow")
			{
				this.Layout = LayoutType.TouchNarrow;
			}
		}

		// Token: 0x06000150 RID: 336 RVA: 0x0000977E File Offset: 0x0000797E
		private bool HasString(string str)
		{
			return this.userAgent.IndexOf(str, StringComparison.OrdinalIgnoreCase) != -1;
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00009794 File Offset: 0x00007994
		private void ParseBrowserVersion()
		{
			int num = -1;
			int num2;
			if (this.IsBrowserIE11Plus())
			{
				string text = "rv:";
				num2 = this.userAgent.IndexOf(text) + text.Length;
			}
			else if (this.IsBrowserIE() || this.IsBrowserFirefox() || this.IsBrowserChrome())
			{
				num2 = this.userAgent.IndexOf(this.Browser, StringComparison.OrdinalIgnoreCase) + this.Browser.Length + 1;
			}
			else
			{
				if (!this.IsBrowserSafari())
				{
					this.browserVersion = new UserAgentVersion(0, 0, 0);
					return;
				}
				string text2 = "Version/";
				num2 = this.userAgent.IndexOf(text2) + text2.Length;
			}
			int i;
			for (i = num2; i < this.userAgent.Length; i++)
			{
				if (!char.IsDigit(this.userAgent, i) && this.userAgent[i] != '.' && this.userAgent[i] != '_')
				{
					num = i;
					break;
				}
			}
			if (num == -1)
			{
				num = this.userAgent.Length;
			}
			if (i == num2)
			{
				ExTraceGlobals.CoreTracer.TraceError<string>(0L, "Unable to parse browser version.  Could not find semicolon in user agent string {0}", this.userAgent);
				this.browserVersion = new UserAgentVersion(0, 0, 0);
				return;
			}
			string text3 = this.userAgent.Substring(num2, num - num2);
			try
			{
				this.browserVersion = new UserAgentVersion(text3);
			}
			catch (ArgumentException)
			{
				ExTraceGlobals.CoreTracer.TraceError<string>(0L, "TryParse failed, unable to parse browser version = {0}", text3);
				this.browserVersion = new UserAgentVersion(0, 0, 0);
			}
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00009918 File Offset: 0x00007B18
		private void ParsePlatformVersion()
		{
			int num = -1;
			int num2;
			if (string.Equals("iPhone", this.Platform) || string.Equals("iPad", this.Platform))
			{
				string text = "OS ";
				num2 = this.userAgent.IndexOf(text) + text.Length;
			}
			else
			{
				if (!string.Equals("Android", this.Platform))
				{
					this.platformVersion = new UserAgentVersion(0, 0, 0);
					return;
				}
				string text2 = "Android ";
				num2 = this.userAgent.IndexOf(text2) + text2.Length;
			}
			int i;
			for (i = num2; i < this.userAgent.Length; i++)
			{
				if (!char.IsDigit(this.userAgent, i) && this.userAgent[i] != '.' && this.userAgent[i] != '_')
				{
					num = i;
					break;
				}
			}
			if (num == -1)
			{
				num = this.userAgent.Length;
			}
			if (i == num2)
			{
				ExTraceGlobals.CoreTracer.TraceError<string>(0L, "Unable to parse platform version.  Could not find semicolon in user agent string {0}", this.userAgent);
				this.platformVersion = new UserAgentVersion(0, 0, 0);
				return;
			}
			string text3 = this.userAgent.Substring(num2, num - num2);
			try
			{
				this.platformVersion = new UserAgentVersion(text3);
			}
			catch (ArgumentException)
			{
				ExTraceGlobals.CoreTracer.TraceError<string>(0L, "TryParse failed, unable to parse platform version = {0}", text3);
				this.platformVersion = new UserAgentVersion(0, 0, 0);
			}
		}

		// Token: 0x040002B0 RID: 688
		private const string WindowsNTString = "Windows NT";

		// Token: 0x040002B1 RID: 689
		internal const string DesktopLayoutString = "mouse";

		// Token: 0x040002B2 RID: 690
		private const string LayoutCookieName = "Layout";

		// Token: 0x040002B3 RID: 691
		private const string TabletLayoutString = "twide";

		// Token: 0x040002B4 RID: 692
		private const string PhoneLayoutString = "tnarrow";

		// Token: 0x040002B5 RID: 693
		private static Regex tridentVersionRegex = new Regex("Trident\\/([\\d+.]*\\d+)", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);

		// Token: 0x040002B6 RID: 694
		private static Regex windowsNTVersionRegex = new Regex("Windows NT (?<majorVersion>\\d+)\\.(?<minorVersion>\\d+)", RegexOptions.Compiled | RegexOptions.CultureInvariant);

		// Token: 0x040002B7 RID: 695
		private static string[] clientPlatform = new string[]
		{
			"Windows NT",
			"Windows 98; Win 9x 4.90",
			"Windows 2000",
			"iPhone",
			"iPad",
			"Android",
			"Linux",
			"Macintosh",
			"CrOS"
		};

		// Token: 0x040002B8 RID: 696
		private static string[] browserList = new string[]
		{
			"Opera",
			"Netscape",
			"MSIE",
			"Safari",
			"Firefox",
			"Chrome"
		};

		// Token: 0x040002B9 RID: 697
		private readonly string userAgent;

		// Token: 0x040002BA RID: 698
		private bool? isIE;

		// Token: 0x040002BB RID: 699
		private bool? isFirefox;

		// Token: 0x040002BC RID: 700
		private bool? isChrome;

		// Token: 0x040002BD RID: 701
		private bool? isSafari;

		// Token: 0x040002BE RID: 702
		private string browser;

		// Token: 0x040002BF RID: 703
		private string platform;

		// Token: 0x040002C0 RID: 704
		private UserAgentVersion browserVersion;

		// Token: 0x040002C1 RID: 705
		private UserAgentVersion platformVersion;

		// Token: 0x040002C2 RID: 706
		private LayoutType? layout;

		// Token: 0x040002C3 RID: 707
		private readonly bool layoutOverrideFeatureEnabled;

		// Token: 0x040002C4 RID: 708
		private HttpCookieCollection cookies;
	}
}
