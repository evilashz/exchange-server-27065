using System;
using System.Text;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Data.TextConverters;
using Microsoft.Exchange.Data.TextConverters.Internal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020005A4 RID: 1444
	[ClassAccessLevel(AccessLevel.Consumer)]
	internal class BodyReadConfiguration
	{
		// Token: 0x06003B00 RID: 15104 RVA: 0x000F2D18 File Offset: 0x000F0F18
		internal BodyReadConfiguration(BodyReadConfiguration configuration)
		{
			this.format = configuration.format;
			this.charset = configuration.charset;
			this.injectPrefix = configuration.injectPrefix;
			this.injectSuffix = configuration.injectSuffix;
			this.injectFormat = configuration.injectFormat;
			this.conversionCallback = configuration.conversionCallback;
			this.htmlFlags = configuration.htmlFlags;
			this.styleSheetLimit = configuration.styleSheetLimit;
			this.shouldCalculateLength = configuration.shouldCalculateLength;
			this.ShouldUseNarrowGapForPTagHtmlToTextConversion = configuration.ShouldUseNarrowGapForPTagHtmlToTextConversion;
			this.ShouldOutputAnchorLinks = configuration.ShouldOutputAnchorLinks;
			this.ShouldOutputImageLinks = configuration.ShouldOutputImageLinks;
		}

		// Token: 0x06003B01 RID: 15105 RVA: 0x000F2DCC File Offset: 0x000F0FCC
		public BodyReadConfiguration(BodyFormat targetFormat)
		{
			EnumValidator.ThrowIfInvalid<BodyFormat>(targetFormat, "targetFormat");
			this.format = targetFormat;
			this.charset = null;
			this.injectPrefix = null;
			this.injectSuffix = null;
			this.injectFormat = BodyInjectionFormat.Text;
			this.conversionCallback = null;
			this.htmlFlags = HtmlStreamingFlags.None;
			this.styleSheetLimit = null;
		}

		// Token: 0x06003B02 RID: 15106 RVA: 0x000F2E35 File Offset: 0x000F1035
		public BodyReadConfiguration(BodyFormat targetFormat, string charsetName) : this(targetFormat, BodyReadConfiguration.GetCharsetFromName(charsetName), false)
		{
		}

		// Token: 0x06003B03 RID: 15107 RVA: 0x000F2E45 File Offset: 0x000F1045
		public BodyReadConfiguration(BodyFormat targetFormat, string charsetName, bool shouldCalculateLength) : this(targetFormat, BodyReadConfiguration.GetCharsetFromName(charsetName), shouldCalculateLength)
		{
		}

		// Token: 0x06003B04 RID: 15108 RVA: 0x000F2E55 File Offset: 0x000F1055
		public BodyReadConfiguration(BodyFormat targetFormat, Charset charset) : this(targetFormat, charset, false)
		{
		}

		// Token: 0x06003B05 RID: 15109 RVA: 0x000F2E60 File Offset: 0x000F1060
		public BodyReadConfiguration(BodyFormat targetFormat, Charset charset, bool shouldCalculateLength)
		{
			EnumValidator.ThrowIfInvalid<BodyFormat>(targetFormat, "targetFormat");
			this.format = targetFormat;
			this.charset = charset;
			this.injectPrefix = null;
			this.injectSuffix = null;
			this.injectFormat = BodyInjectionFormat.Text;
			this.conversionCallback = null;
			this.htmlFlags = HtmlStreamingFlags.None;
			this.styleSheetLimit = null;
			this.shouldCalculateLength = shouldCalculateLength;
		}

		// Token: 0x06003B06 RID: 15110 RVA: 0x000F2ED0 File Offset: 0x000F10D0
		internal bool IsBodyTransformationNeeded(Body body)
		{
			if (body.RawFormat != this.format)
			{
				return true;
			}
			if (!string.IsNullOrEmpty(this.injectPrefix) || !string.IsNullOrEmpty(this.injectSuffix))
			{
				return true;
			}
			switch (this.format)
			{
			case BodyFormat.TextPlain:
				return this.charset.CodePage != 1200;
			case BodyFormat.TextHtml:
				return this.charset.CodePage != body.RawCharset.CodePage || this.htmlFlags != HtmlStreamingFlags.None || this.conversionCallback != null || this.styleSheetLimit != null;
			case BodyFormat.ApplicationRtf:
				return false;
			default:
				return true;
			}
		}

		// Token: 0x06003B07 RID: 15111 RVA: 0x000F2F75 File Offset: 0x000F1175
		public void AddInjectedText(string prefix, string suffix, BodyInjectionFormat format)
		{
			if (prefix == null && suffix == null)
			{
				throw new ArgumentException("Either prefix or suffix should be non-null");
			}
			EnumValidator.ThrowIfInvalid<BodyInjectionFormat>(format, "format");
			this.injectPrefix = prefix;
			this.injectSuffix = suffix;
			this.injectFormat = format;
		}

		// Token: 0x06003B08 RID: 15112 RVA: 0x000F2FA8 File Offset: 0x000F11A8
		public void SetHtmlOptions(HtmlStreamingFlags flags, HtmlCallbackBase callback)
		{
			EnumValidator.ThrowIfInvalid<HtmlStreamingFlags>(flags, "flags");
			this.SetHtmlOptions(flags, callback, null);
		}

		// Token: 0x06003B09 RID: 15113 RVA: 0x000F2FD1 File Offset: 0x000F11D1
		public void SetHtmlOptions(HtmlStreamingFlags flags, HtmlCallbackBase callback, int? styleSheetLimit)
		{
			EnumValidator.ThrowIfInvalid<HtmlStreamingFlags>(flags, "flags");
			if (this.Format != BodyFormat.TextHtml)
			{
				throw new InvalidOperationException("BodyReadConfiguration.SetHtmlOptions - target format is not HTML");
			}
			this.htmlFlags = flags;
			this.conversionCallback = callback;
			this.styleSheetLimit = styleSheetLimit;
		}

		// Token: 0x1700120A RID: 4618
		// (get) Token: 0x06003B0A RID: 15114 RVA: 0x000F3007 File Offset: 0x000F1207
		public BodyFormat Format
		{
			get
			{
				return this.format;
			}
		}

		// Token: 0x1700120B RID: 4619
		// (get) Token: 0x06003B0B RID: 15115 RVA: 0x000F300F File Offset: 0x000F120F
		// (set) Token: 0x06003B0C RID: 15116 RVA: 0x000F3017 File Offset: 0x000F1217
		public Charset Charset
		{
			get
			{
				return this.charset;
			}
			internal set
			{
				this.charset = value;
			}
		}

		// Token: 0x1700120C RID: 4620
		// (get) Token: 0x06003B0D RID: 15117 RVA: 0x000F3020 File Offset: 0x000F1220
		public Encoding Encoding
		{
			get
			{
				if (this.charset == null)
				{
					return null;
				}
				return this.charset.GetEncoding();
			}
		}

		// Token: 0x1700120D RID: 4621
		// (get) Token: 0x06003B0E RID: 15118 RVA: 0x000F3037 File Offset: 0x000F1237
		public string InjectPrefix
		{
			get
			{
				return this.injectPrefix;
			}
		}

		// Token: 0x1700120E RID: 4622
		// (get) Token: 0x06003B0F RID: 15119 RVA: 0x000F303F File Offset: 0x000F123F
		public string InjectSuffix
		{
			get
			{
				return this.injectSuffix;
			}
		}

		// Token: 0x1700120F RID: 4623
		// (get) Token: 0x06003B10 RID: 15120 RVA: 0x000F3047 File Offset: 0x000F1247
		public BodyInjectionFormat InjectionFormat
		{
			get
			{
				return this.injectFormat;
			}
		}

		// Token: 0x17001210 RID: 4624
		// (get) Token: 0x06003B11 RID: 15121 RVA: 0x000F304F File Offset: 0x000F124F
		internal HeaderFooterFormat InjectionHeaderFooterFormat
		{
			get
			{
				if (this.InjectionFormat != BodyInjectionFormat.Html)
				{
					return HeaderFooterFormat.Text;
				}
				return HeaderFooterFormat.Html;
			}
		}

		// Token: 0x17001211 RID: 4625
		// (get) Token: 0x06003B12 RID: 15122 RVA: 0x000F305D File Offset: 0x000F125D
		// (set) Token: 0x06003B13 RID: 15123 RVA: 0x000F3065 File Offset: 0x000F1265
		public HtmlStreamingFlags HtmlFlags
		{
			get
			{
				return this.htmlFlags;
			}
			set
			{
				this.htmlFlags = value;
			}
		}

		// Token: 0x17001212 RID: 4626
		// (get) Token: 0x06003B14 RID: 15124 RVA: 0x000F306E File Offset: 0x000F126E
		public bool IsHtmlFragment
		{
			get
			{
				return (this.htmlFlags & HtmlStreamingFlags.Fragment) == HtmlStreamingFlags.Fragment;
			}
		}

		// Token: 0x17001213 RID: 4627
		// (get) Token: 0x06003B15 RID: 15125 RVA: 0x000F307B File Offset: 0x000F127B
		public bool FilterHtml
		{
			get
			{
				return (this.htmlFlags & HtmlStreamingFlags.FilterHtml) == HtmlStreamingFlags.FilterHtml;
			}
		}

		// Token: 0x17001214 RID: 4628
		// (get) Token: 0x06003B16 RID: 15126 RVA: 0x000F3088 File Offset: 0x000F1288
		public int? StyleSheetLimit
		{
			get
			{
				return this.styleSheetLimit;
			}
		}

		// Token: 0x17001215 RID: 4629
		// (get) Token: 0x06003B17 RID: 15127 RVA: 0x000F3090 File Offset: 0x000F1290
		public bool ShouldCalculateLength
		{
			get
			{
				return this.shouldCalculateLength;
			}
		}

		// Token: 0x17001216 RID: 4630
		// (get) Token: 0x06003B18 RID: 15128 RVA: 0x000F3098 File Offset: 0x000F1298
		// (set) Token: 0x06003B19 RID: 15129 RVA: 0x000F30A5 File Offset: 0x000F12A5
		public HtmlCallbackBase HtmlCallback
		{
			get
			{
				return this.conversionCallback as HtmlCallbackBase;
			}
			internal set
			{
				this.conversionCallback = value;
			}
		}

		// Token: 0x17001217 RID: 4631
		// (get) Token: 0x06003B1A RID: 15130 RVA: 0x000F30AE File Offset: 0x000F12AE
		// (set) Token: 0x06003B1B RID: 15131 RVA: 0x000F30B6 File Offset: 0x000F12B6
		public ConversionCallbackBase ConversionCallback
		{
			get
			{
				return this.conversionCallback;
			}
			set
			{
				this.conversionCallback = value;
			}
		}

		// Token: 0x17001218 RID: 4632
		// (get) Token: 0x06003B1C RID: 15132 RVA: 0x000F30BF File Offset: 0x000F12BF
		// (set) Token: 0x06003B1D RID: 15133 RVA: 0x000F30C7 File Offset: 0x000F12C7
		public bool ShouldUseNarrowGapForPTagHtmlToTextConversion { get; set; }

		// Token: 0x17001219 RID: 4633
		// (get) Token: 0x06003B1E RID: 15134 RVA: 0x000F30D0 File Offset: 0x000F12D0
		// (set) Token: 0x06003B1F RID: 15135 RVA: 0x000F30D8 File Offset: 0x000F12D8
		public bool ShouldOutputAnchorLinks
		{
			get
			{
				return this.shouldOutputAnchorLinks;
			}
			set
			{
				this.shouldOutputAnchorLinks = value;
			}
		}

		// Token: 0x1700121A RID: 4634
		// (get) Token: 0x06003B20 RID: 15136 RVA: 0x000F30E1 File Offset: 0x000F12E1
		// (set) Token: 0x06003B21 RID: 15137 RVA: 0x000F30E9 File Offset: 0x000F12E9
		public bool ShouldOutputImageLinks
		{
			get
			{
				return this.shouldOutputImageLinks;
			}
			set
			{
				this.shouldOutputImageLinks = value;
			}
		}

		// Token: 0x1700121B RID: 4635
		// (get) Token: 0x06003B22 RID: 15138 RVA: 0x000F30F4 File Offset: 0x000F12F4
		internal HtmlTagCallback InternalHtmlTagCallback
		{
			get
			{
				HtmlCallbackBase htmlCallback = this.HtmlCallback;
				if (htmlCallback == null)
				{
					return null;
				}
				return new HtmlTagCallback(htmlCallback.ProcessTag);
			}
		}

		// Token: 0x1700121C RID: 4636
		// (get) Token: 0x06003B23 RID: 15139 RVA: 0x000F311C File Offset: 0x000F131C
		internal ImageRenderingCallback ImageRenderingCallback
		{
			get
			{
				RtfCallbackBase rtfCallbackBase = this.conversionCallback as RtfCallbackBase;
				if (rtfCallbackBase == null)
				{
					return null;
				}
				return new ImageRenderingCallback(rtfCallbackBase.ProcessImage);
			}
		}

		// Token: 0x06003B24 RID: 15140 RVA: 0x000F3147 File Offset: 0x000F1347
		private static Charset GetCharsetFromName(string charsetName)
		{
			if (string.IsNullOrEmpty(charsetName))
			{
				throw new ArgumentNullException("charsetName");
			}
			return ConvertUtils.GetCharsetFromCharsetName(charsetName);
		}

		// Token: 0x04001F8A RID: 8074
		private BodyFormat format;

		// Token: 0x04001F8B RID: 8075
		private Charset charset;

		// Token: 0x04001F8C RID: 8076
		private string injectPrefix;

		// Token: 0x04001F8D RID: 8077
		private string injectSuffix;

		// Token: 0x04001F8E RID: 8078
		private BodyInjectionFormat injectFormat;

		// Token: 0x04001F8F RID: 8079
		private HtmlStreamingFlags htmlFlags;

		// Token: 0x04001F90 RID: 8080
		private ConversionCallbackBase conversionCallback;

		// Token: 0x04001F91 RID: 8081
		private int? styleSheetLimit;

		// Token: 0x04001F92 RID: 8082
		private bool shouldCalculateLength;

		// Token: 0x04001F93 RID: 8083
		private bool shouldOutputAnchorLinks = true;

		// Token: 0x04001F94 RID: 8084
		private bool shouldOutputImageLinks = true;
	}
}
