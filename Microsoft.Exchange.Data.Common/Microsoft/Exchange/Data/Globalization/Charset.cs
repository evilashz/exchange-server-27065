using System;
using System.Text;

namespace Microsoft.Exchange.Data.Globalization
{
	// Token: 0x02000106 RID: 262
	[Serializable]
	public class Charset
	{
		// Token: 0x06000A8F RID: 2703 RVA: 0x0005EE5A File Offset: 0x0005D05A
		internal Charset(int codePage, string name)
		{
			this.codePage = codePage;
			this.name = name;
			this.culture = null;
			this.available = true;
			this.mapIndex = -1;
		}

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06000A90 RID: 2704 RVA: 0x0005EE85 File Offset: 0x0005D085
		public static Charset DefaultMimeCharset
		{
			get
			{
				return Culture.Default.MimeCharset;
			}
		}

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06000A91 RID: 2705 RVA: 0x0005EE91 File Offset: 0x0005D091
		public static bool FallbackToDefaultCharset
		{
			get
			{
				return Culture.FallbackToDefaultCharset;
			}
		}

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06000A92 RID: 2706 RVA: 0x0005EE98 File Offset: 0x0005D098
		public static Charset DefaultWebCharset
		{
			get
			{
				return Culture.Default.WebCharset;
			}
		}

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06000A93 RID: 2707 RVA: 0x0005EEA4 File Offset: 0x0005D0A4
		public static Charset DefaultWindowsCharset
		{
			get
			{
				return Culture.Default.WindowsCharset;
			}
		}

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06000A94 RID: 2708 RVA: 0x0005EEB0 File Offset: 0x0005D0B0
		public static Charset ASCII
		{
			get
			{
				return CultureCharsetDatabase.Data.AsciiCharset;
			}
		}

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06000A95 RID: 2709 RVA: 0x0005EEBC File Offset: 0x0005D0BC
		public static Charset UTF8
		{
			get
			{
				return CultureCharsetDatabase.Data.Utf8Charset;
			}
		}

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06000A96 RID: 2710 RVA: 0x0005EEC8 File Offset: 0x0005D0C8
		public static Charset Unicode
		{
			get
			{
				return CultureCharsetDatabase.Data.UnicodeCharset;
			}
		}

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06000A97 RID: 2711 RVA: 0x0005EED4 File Offset: 0x0005D0D4
		public int CodePage
		{
			get
			{
				return this.codePage;
			}
		}

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06000A98 RID: 2712 RVA: 0x0005EEDC File Offset: 0x0005D0DC
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06000A99 RID: 2713 RVA: 0x0005EEE4 File Offset: 0x0005D0E4
		public Culture Culture
		{
			get
			{
				return this.culture;
			}
		}

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06000A9A RID: 2714 RVA: 0x0005EEEC File Offset: 0x0005D0EC
		public bool IsDetectable
		{
			get
			{
				return this.mapIndex >= 0 && 0 != (byte)(CodePageMapData.codePages[(int)this.mapIndex].flags & CodePageFlags.Detectable);
			}
		}

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06000A9B RID: 2715 RVA: 0x0005EF17 File Offset: 0x0005D117
		public bool IsAvailable
		{
			get
			{
				return this.available && (this.encoding != null || this.CheckAvailable());
			}
		}

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06000A9C RID: 2716 RVA: 0x0005EF33 File Offset: 0x0005D133
		public bool IsWindowsCharset
		{
			get
			{
				return this.windows;
			}
		}

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06000A9D RID: 2717 RVA: 0x0005EF3B File Offset: 0x0005D13B
		public string Description
		{
			get
			{
				return this.description;
			}
		}

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06000A9E RID: 2718 RVA: 0x0005EF43 File Offset: 0x0005D143
		internal static int MaxCharsetNameLength
		{
			get
			{
				return CultureCharsetDatabase.Data.MaxCharsetNameLength;
			}
		}

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06000A9F RID: 2719 RVA: 0x0005EF4F File Offset: 0x0005D14F
		internal int MapIndex
		{
			get
			{
				return (int)this.mapIndex;
			}
		}

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06000AA0 RID: 2720 RVA: 0x0005EF57 File Offset: 0x0005D157
		internal CodePageKind Kind
		{
			get
			{
				if (this.mapIndex >= 0)
				{
					return CodePageMapData.codePages[(int)this.mapIndex].kind;
				}
				return CodePageKind.Unknown;
			}
		}

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06000AA1 RID: 2721 RVA: 0x0005EF79 File Offset: 0x0005D179
		internal CodePageAsciiSupport AsciiSupport
		{
			get
			{
				if (this.mapIndex >= 0)
				{
					return CodePageMapData.codePages[(int)this.mapIndex].asciiSupport;
				}
				return CodePageAsciiSupport.Unknown;
			}
		}

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06000AA2 RID: 2722 RVA: 0x0005EF9B File Offset: 0x0005D19B
		internal CodePageUnicodeCoverage UnicodeCoverage
		{
			get
			{
				if (this.mapIndex >= 0)
				{
					return CodePageMapData.codePages[(int)this.mapIndex].unicodeCoverage;
				}
				return CodePageUnicodeCoverage.Unknown;
			}
		}

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06000AA3 RID: 2723 RVA: 0x0005EFBD File Offset: 0x0005D1BD
		internal bool IsSevenBit
		{
			get
			{
				return this.mapIndex >= 0 && 0 != (byte)(CodePageMapData.codePages[(int)this.mapIndex].flags & CodePageFlags.SevenBit);
			}
		}

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06000AA4 RID: 2724 RVA: 0x0005EFE8 File Offset: 0x0005D1E8
		internal int DetectableCodePageWithEquivalentCoverage
		{
			get
			{
				if (this.mapIndex < 0)
				{
					return 0;
				}
				if ((byte)(CodePageMapData.codePages[(int)this.mapIndex].flags & CodePageFlags.Detectable) == 0)
				{
					return (int)CodePageMapData.codePages[(int)this.mapIndex].detectCpid;
				}
				return this.codePage;
			}
		}

		// Token: 0x06000AA5 RID: 2725 RVA: 0x0005F038 File Offset: 0x0005D238
		public static Charset GetCharset(string name)
		{
			Charset result;
			if (!Charset.TryGetCharset(name, out result))
			{
				throw new InvalidCharsetException(name);
			}
			return result;
		}

		// Token: 0x06000AA6 RID: 2726 RVA: 0x0005F058 File Offset: 0x0005D258
		public static bool TryGetCharset(string name, out Charset charset)
		{
			if (name == null)
			{
				charset = null;
				return false;
			}
			if (CultureCharsetDatabase.Data.NameToCharset.TryGetValue(name, out charset))
			{
				return true;
			}
			if (name.StartsWith("cp", StringComparison.OrdinalIgnoreCase) || name.StartsWith("ms", StringComparison.OrdinalIgnoreCase))
			{
				int num = 0;
				for (int i = 2; i < name.Length; i++)
				{
					if (name[i] < '0' || name[i] > '9')
					{
						num = 0;
						break;
					}
					num = num * 10 + (int)(name[i] - '0');
					if (num >= 65536)
					{
						num = 0;
						break;
					}
				}
				if (num != 0 && Charset.TryGetCharset(num, out charset))
				{
					return true;
				}
			}
			return ((Charset.FallbackToDefaultCharset && Charset.DefaultMimeCharset != null) || (!Charset.FallbackToDefaultCharset && Charset.DefaultMimeCharset != null && Charset.DefaultMimeCharset.Name.Equals("iso-2022-jp", StringComparison.OrdinalIgnoreCase))) && CultureCharsetDatabase.Data.NameToCharset.TryGetValue(Charset.DefaultMimeCharset.Name, out charset);
		}

		// Token: 0x06000AA7 RID: 2727 RVA: 0x0005F148 File Offset: 0x0005D348
		public static Charset GetCharset(int codePage)
		{
			Charset result;
			if (!Charset.TryGetCharset(codePage, out result))
			{
				throw new InvalidCharsetException(codePage);
			}
			return result;
		}

		// Token: 0x06000AA8 RID: 2728 RVA: 0x0005F168 File Offset: 0x0005D368
		public static Charset GetCharset(Encoding encoding)
		{
			int num = CodePageMap.GetCodePage(encoding);
			return Charset.GetCharset(num);
		}

		// Token: 0x06000AA9 RID: 2729 RVA: 0x0005F182 File Offset: 0x0005D382
		public static bool TryGetCharset(int codePage, out Charset charset)
		{
			return CultureCharsetDatabase.Data.CodePageToCharset.TryGetValue(codePage, out charset);
		}

		// Token: 0x06000AAA RID: 2730 RVA: 0x0005F198 File Offset: 0x0005D398
		public static bool TryGetEncoding(int codePage, out Encoding encoding)
		{
			Charset charset;
			if (!Charset.TryGetCharset(codePage, out charset))
			{
				encoding = null;
				return false;
			}
			return charset.TryGetEncoding(out encoding);
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x0005F1BC File Offset: 0x0005D3BC
		public static bool TryGetEncoding(string name, out Encoding encoding)
		{
			Charset charset;
			if (!Charset.TryGetCharset(name, out charset))
			{
				encoding = null;
				return false;
			}
			return charset.TryGetEncoding(out encoding);
		}

		// Token: 0x06000AAC RID: 2732 RVA: 0x0005F1E0 File Offset: 0x0005D3E0
		public static Encoding GetEncoding(int codePage)
		{
			Charset charset = Charset.GetCharset(codePage);
			return charset.GetEncoding();
		}

		// Token: 0x06000AAD RID: 2733 RVA: 0x0005F1FC File Offset: 0x0005D3FC
		public static Encoding GetEncoding(string name)
		{
			Charset charset = Charset.GetCharset(name);
			return charset.GetEncoding();
		}

		// Token: 0x06000AAE RID: 2734 RVA: 0x0005F218 File Offset: 0x0005D418
		public Encoding GetEncoding()
		{
			Encoding result;
			if (!this.TryGetEncoding(out result))
			{
				throw new CharsetNotInstalledException(this.codePage, this.name);
			}
			return result;
		}

		// Token: 0x06000AAF RID: 2735 RVA: 0x0005F242 File Offset: 0x0005D442
		internal void SetCulture(Culture culture)
		{
			this.culture = culture;
		}

		// Token: 0x06000AB0 RID: 2736 RVA: 0x0005F24B File Offset: 0x0005D44B
		internal void SetDescription(string description)
		{
			this.description = description;
		}

		// Token: 0x06000AB1 RID: 2737 RVA: 0x0005F254 File Offset: 0x0005D454
		internal void SetDefaultName(string name)
		{
			this.name = name;
		}

		// Token: 0x06000AB2 RID: 2738 RVA: 0x0005F25D File Offset: 0x0005D45D
		internal void SetWindows()
		{
			this.windows = true;
		}

		// Token: 0x06000AB3 RID: 2739 RVA: 0x0005F266 File Offset: 0x0005D466
		internal void SetMapIndex(int index)
		{
			this.mapIndex = (short)index;
		}

		// Token: 0x06000AB4 RID: 2740 RVA: 0x0005F270 File Offset: 0x0005D470
		internal bool CheckAvailable()
		{
			Encoding encoding;
			return this.TryGetEncoding(out encoding);
		}

		// Token: 0x06000AB5 RID: 2741 RVA: 0x0005F285 File Offset: 0x0005D485
		public static bool TryGetCharset(Encoding encoding, out Charset charset)
		{
			return Charset.TryGetCharset(encoding.CodePage, out charset);
		}

		// Token: 0x06000AB6 RID: 2742 RVA: 0x0005F294 File Offset: 0x0005D494
		public bool TryGetEncoding(out Encoding encoding)
		{
			if (this.encoding == null && this.available)
			{
				try
				{
					if (this.codePage == 20127)
					{
						this.encoding = Encoding.GetEncoding(this.codePage, new AsciiEncoderFallback(), DecoderFallback.ReplacementFallback);
					}
					else if (this.codePage == 28591 || this.codePage == 28599)
					{
						this.encoding = new RemapEncoding(this.codePage);
					}
					else if (this.codePage == 50220 || this.codePage == 50221 || this.codePage == 50222)
					{
						this.encoding = new Iso2022JpEncoding(this.codePage);
					}
					else
					{
						this.encoding = Encoding.GetEncoding(this.codePage);
					}
				}
				catch (ArgumentException)
				{
					this.encoding = null;
				}
				catch (NotSupportedException)
				{
					this.encoding = null;
				}
				if (this.encoding == null)
				{
					this.available = false;
				}
			}
			encoding = this.encoding;
			return encoding != null;
		}

		// Token: 0x04000DBD RID: 3517
		private int codePage;

		// Token: 0x04000DBE RID: 3518
		private string name;

		// Token: 0x04000DBF RID: 3519
		private Culture culture;

		// Token: 0x04000DC0 RID: 3520
		private short mapIndex;

		// Token: 0x04000DC1 RID: 3521
		private bool windows;

		// Token: 0x04000DC2 RID: 3522
		private bool available;

		// Token: 0x04000DC3 RID: 3523
		private Encoding encoding;

		// Token: 0x04000DC4 RID: 3524
		private string description;
	}
}
