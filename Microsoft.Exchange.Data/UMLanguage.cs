using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020001C9 RID: 457
	[Serializable]
	public class UMLanguage : IEquatable<UMLanguage>, IComparable, IComparable<UMLanguage>
	{
		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x0600100D RID: 4109 RVA: 0x00030DB5 File Offset: 0x0002EFB5
		public string DisplayName
		{
			get
			{
				return this.localizedDisplayName;
			}
		}

		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x0600100E RID: 4110 RVA: 0x00030DBD File Offset: 0x0002EFBD
		public string EnglishName
		{
			get
			{
				return this.englishName;
			}
		}

		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x0600100F RID: 4111 RVA: 0x00030DC5 File Offset: 0x0002EFC5
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x06001010 RID: 4112 RVA: 0x00030DCD File Offset: 0x0002EFCD
		public int LCID
		{
			get
			{
				return this.lcid;
			}
		}

		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x06001011 RID: 4113 RVA: 0x00030DD5 File Offset: 0x0002EFD5
		public CultureInfo Culture
		{
			get
			{
				return this.culture;
			}
		}

		// Token: 0x06001012 RID: 4114 RVA: 0x00030DDD File Offset: 0x0002EFDD
		public UMLanguage(int lcid)
		{
			this.lcid = lcid;
			this.SetCultureInfo(null);
		}

		// Token: 0x06001013 RID: 4115 RVA: 0x00030DF3 File Offset: 0x0002EFF3
		public UMLanguage(CultureInfo culture)
		{
			this.lcid = culture.LCID;
			this.SetCultureInfo(culture);
		}

		// Token: 0x06001014 RID: 4116 RVA: 0x00030E0E File Offset: 0x0002F00E
		[OnDeserialized]
		private void OnDeserialized(StreamingContext context)
		{
			this.SetCultureInfo(null);
		}

		// Token: 0x06001015 RID: 4117 RVA: 0x00030E17 File Offset: 0x0002F017
		private void SetCultureInfo(CultureInfo culture)
		{
			if (this.lcid == 22538)
			{
				this.SetLatinAmericanSpanish();
				return;
			}
			if (culture == null)
			{
				culture = new CultureInfo(this.lcid);
			}
			this.SetPropsFromCulture(culture);
		}

		// Token: 0x06001016 RID: 4118 RVA: 0x00030E44 File Offset: 0x0002F044
		public static UMLanguage Parse(string language)
		{
			if (string.IsNullOrEmpty(language))
			{
				throw new ArgumentNullException(language);
			}
			if (string.Compare(language, "es-419", StringComparison.OrdinalIgnoreCase) == 0)
			{
				return new UMLanguage(22538);
			}
			CultureInfo cultureInfo = new CultureInfo(language);
			return new UMLanguage(cultureInfo);
		}

		// Token: 0x06001017 RID: 4119 RVA: 0x00030E86 File Offset: 0x0002F086
		public override string ToString()
		{
			return this.name;
		}

		// Token: 0x06001018 RID: 4120 RVA: 0x00030E8E File Offset: 0x0002F08E
		private void SetLatinAmericanSpanish()
		{
			this.localizedDisplayName = DataStrings.LatAmSpanish;
			this.englishName = "Spanish (Latin America)";
			this.name = "es-419";
			this.culture = new CultureInfo("es-mx");
		}

		// Token: 0x06001019 RID: 4121 RVA: 0x00030EC6 File Offset: 0x0002F0C6
		private void SetPropsFromCulture(CultureInfo culture)
		{
			this.culture = culture;
			this.localizedDisplayName = culture.DisplayName;
			this.englishName = culture.EnglishName;
			this.name = culture.Name;
		}

		// Token: 0x0600101A RID: 4122 RVA: 0x00030EF3 File Offset: 0x0002F0F3
		public override int GetHashCode()
		{
			return this.lcid.GetHashCode();
		}

		// Token: 0x0600101B RID: 4123 RVA: 0x00030F00 File Offset: 0x0002F100
		public override bool Equals(object obj)
		{
			if (!(obj is UMLanguage))
			{
				return false;
			}
			UMLanguage umlanguage = (UMLanguage)obj;
			return this.lcid == umlanguage.LCID;
		}

		// Token: 0x0600101C RID: 4124 RVA: 0x00030F2C File Offset: 0x0002F12C
		public int CompareTo(UMLanguage other)
		{
			return this.lcid.CompareTo(other.LCID);
		}

		// Token: 0x0600101D RID: 4125 RVA: 0x00030F40 File Offset: 0x0002F140
		public int CompareTo(object obj)
		{
			if (!(obj is UMLanguage))
			{
				return -1;
			}
			UMLanguage umlanguage = (UMLanguage)obj;
			return this.lcid.CompareTo(umlanguage.LCID);
		}

		// Token: 0x0600101E RID: 4126 RVA: 0x00030F6F File Offset: 0x0002F16F
		public bool Equals(UMLanguage other)
		{
			return this.lcid == other.LCID;
		}

		// Token: 0x0400098F RID: 2447
		private const string LatinAmericanSpanishRfcName = "es-419";

		// Token: 0x04000990 RID: 2448
		private const string LatinAmericanSpanish_EnglishName = "Spanish (Latin America)";

		// Token: 0x04000991 RID: 2449
		private const int LatinAmericanSpanishLcid = 22538;

		// Token: 0x04000992 RID: 2450
		private int lcid;

		// Token: 0x04000993 RID: 2451
		[NonSerialized]
		private string localizedDisplayName;

		// Token: 0x04000994 RID: 2452
		[NonSerialized]
		private string englishName;

		// Token: 0x04000995 RID: 2453
		[NonSerialized]
		private string name;

		// Token: 0x04000996 RID: 2454
		[NonSerialized]
		private CultureInfo culture;

		// Token: 0x04000997 RID: 2455
		internal static UMLanguage DefaultLanguage = new UMLanguage(1033);

		// Token: 0x04000998 RID: 2456
		internal static UMLanguage[] Datacenterlanguages = new UMLanguage[]
		{
			UMLanguage.Parse("en-US"),
			UMLanguage.Parse("ca-ES"),
			UMLanguage.Parse("da-DK"),
			UMLanguage.Parse("de-DE"),
			UMLanguage.Parse("en-AU"),
			UMLanguage.Parse("en-CA"),
			UMLanguage.Parse("en-GB"),
			UMLanguage.Parse("en-IN"),
			UMLanguage.Parse("es-ES"),
			UMLanguage.Parse("es-MX"),
			UMLanguage.Parse("fi-FI"),
			UMLanguage.Parse("fr-FR"),
			UMLanguage.Parse("fr-CA"),
			UMLanguage.Parse("it-IT"),
			UMLanguage.Parse("ja-JP"),
			UMLanguage.Parse("ko-KR"),
			UMLanguage.Parse("nl-NL"),
			UMLanguage.Parse("nb-NO"),
			UMLanguage.Parse("pl-PL"),
			UMLanguage.Parse("pt-BR"),
			UMLanguage.Parse("pt-PT"),
			UMLanguage.Parse("ru-RU"),
			UMLanguage.Parse("sv-SE"),
			UMLanguage.Parse("zh-CN"),
			UMLanguage.Parse("zh-TW"),
			UMLanguage.Parse("zh-HK")
		};
	}
}
