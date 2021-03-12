using System;
using System.Text;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Data.TextConverters;
using Microsoft.Exchange.Data.TextConverters.Internal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020005AD RID: 1453
	[ClassAccessLevel(AccessLevel.Consumer)]
	internal class BodyWriteConfiguration
	{
		// Token: 0x06003B9F RID: 15263 RVA: 0x000F5A48 File Offset: 0x000F3C48
		internal BodyWriteConfiguration(BodyWriteConfiguration configuration)
		{
			this.sourceFormat = configuration.sourceFormat;
			this.sourceCharset = configuration.sourceCharset;
			this.targetFormat = configuration.targetFormat;
			this.targetCharset = configuration.targetCharset;
			this.targetCharsetFlags = configuration.targetCharsetFlags;
			this.injectPrefix = configuration.injectPrefix;
			this.injectSuffix = configuration.injectSuffix;
			this.injectFormat = configuration.injectFormat;
			this.conversionCallbacks = configuration.conversionCallbacks;
			this.htmlFlags = configuration.htmlFlags;
			this.trustHtmlMetaTag = configuration.trustHtmlMetaTag;
		}

		// Token: 0x06003BA0 RID: 15264 RVA: 0x000F5AEC File Offset: 0x000F3CEC
		public BodyWriteConfiguration(BodyFormat sourceFormat)
		{
			EnumValidator.ThrowIfInvalid<BodyFormat>(sourceFormat, "sourceFormat");
			this.sourceFormat = sourceFormat;
			this.sourceCharset = ConvertUtils.UnicodeCharset;
			this.targetFormat = sourceFormat;
			this.targetCharset = null;
			this.targetCharsetFlags = BodyCharsetFlags.None;
			this.injectPrefix = null;
			this.injectSuffix = null;
			this.injectFormat = BodyInjectionFormat.Text;
			this.htmlFlags = HtmlStreamingFlags.None;
		}

		// Token: 0x06003BA1 RID: 15265 RVA: 0x000F5B5C File Offset: 0x000F3D5C
		public BodyWriteConfiguration(BodyFormat sourceFormat, Charset sourceCharset)
		{
			EnumValidator.ThrowIfInvalid<BodyFormat>(sourceFormat, "sourceFormat");
			this.sourceFormat = sourceFormat;
			this.sourceCharset = sourceCharset;
			this.targetFormat = this.sourceFormat;
			this.targetCharset = this.sourceCharset;
			this.targetCharsetFlags = BodyCharsetFlags.None;
			this.injectPrefix = null;
			this.injectSuffix = null;
			this.injectFormat = BodyInjectionFormat.Text;
			this.htmlFlags = HtmlStreamingFlags.None;
		}

		// Token: 0x06003BA2 RID: 15266 RVA: 0x000F5BCF File Offset: 0x000F3DCF
		public BodyWriteConfiguration(BodyFormat sourceFormat, string sourceCharsetName) : this(sourceFormat, string.IsNullOrEmpty(sourceCharsetName) ? null : ConvertUtils.GetCharsetFromCharsetName(sourceCharsetName))
		{
		}

		// Token: 0x06003BA3 RID: 15267 RVA: 0x000F5BEC File Offset: 0x000F3DEC
		internal bool IsContentTransformationNeeded(ICoreItem coreItem)
		{
			if (this.sourceFormat != this.targetFormat)
			{
				return true;
			}
			if (!string.IsNullOrEmpty(this.injectPrefix) || !string.IsNullOrEmpty(this.injectSuffix))
			{
				return true;
			}
			switch (this.targetFormat)
			{
			case BodyFormat.TextPlain:
				return this.sourceCharset.CodePage != 1200;
			case BodyFormat.TextHtml:
			{
				Charset charset;
				return this.htmlFlags != HtmlStreamingFlags.None || this.HtmlCallback != null || !this.trustHtmlMetaTag || !coreItem.CharsetDetector.IsItemCharsetKnownWithoutDetection(this.targetCharsetFlags, this.targetCharset, out charset) || charset.CodePage != this.sourceCharset.CodePage;
			}
			case BodyFormat.ApplicationRtf:
				return false;
			default:
				return true;
			}
		}

		// Token: 0x06003BA4 RID: 15268 RVA: 0x000F5CA7 File Offset: 0x000F3EA7
		public void SetTargetFormat(BodyFormat targetFormat, Charset targetCharset)
		{
			this.SetTargetFormat(targetFormat, targetCharset, BodyCharsetFlags.None);
		}

		// Token: 0x06003BA5 RID: 15269 RVA: 0x000F5CB2 File Offset: 0x000F3EB2
		public void SetTargetFormat(BodyFormat targetFormat, string targetCharsetName)
		{
			this.SetTargetFormat(targetFormat, targetCharsetName, BodyCharsetFlags.None);
		}

		// Token: 0x06003BA6 RID: 15270 RVA: 0x000F5CBD File Offset: 0x000F3EBD
		public void SetTargetFormat(BodyFormat targetFormat, Charset targetCharset, BodyCharsetFlags flags)
		{
			EnumValidator.ThrowIfInvalid<BodyFormat>(targetFormat, "targetFormat");
			if (targetCharset == null && (flags & BodyCharsetFlags.CharsetDetectionMask) == BodyCharsetFlags.DisableCharsetDetection)
			{
				throw new ArgumentNullException("targetCharset");
			}
			this.targetCharset = targetCharset;
			this.targetFormat = targetFormat;
			this.targetCharsetFlags = flags;
		}

		// Token: 0x06003BA7 RID: 15271 RVA: 0x000F5CF8 File Offset: 0x000F3EF8
		public void SetTargetFormat(BodyFormat targetFormat, string targetCharsetName, BodyCharsetFlags flags)
		{
			EnumValidator.ThrowIfInvalid<BodyFormat>(targetFormat, "targetFormat");
			Charset charset = null;
			if (!string.IsNullOrEmpty(targetCharsetName))
			{
				charset = ConvertUtils.GetCharsetFromCharsetName(targetCharsetName);
			}
			this.SetTargetFormat(targetFormat, charset, flags);
		}

		// Token: 0x06003BA8 RID: 15272 RVA: 0x000F5D2A File Offset: 0x000F3F2A
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

		// Token: 0x06003BA9 RID: 15273 RVA: 0x000F5D5D File Offset: 0x000F3F5D
		public void SetHtmlOptions(HtmlStreamingFlags flags, HtmlCallbackBase callback)
		{
			EnumValidator.ThrowIfInvalid<HtmlStreamingFlags>(flags, "flags");
			if (this.targetFormat != BodyFormat.TextHtml && this.sourceFormat != BodyFormat.TextHtml)
			{
				throw new InvalidOperationException("BodyReadConfiguration.SetHtmlOptions - neither source not target format is HTML");
			}
			this.htmlFlags = flags;
			this.conversionCallbacks.HtmlCallback = callback;
		}

		// Token: 0x17001230 RID: 4656
		// (get) Token: 0x06003BAA RID: 15274 RVA: 0x000F5D9A File Offset: 0x000F3F9A
		public BodyFormat SourceFormat
		{
			get
			{
				return this.sourceFormat;
			}
		}

		// Token: 0x17001231 RID: 4657
		// (get) Token: 0x06003BAB RID: 15275 RVA: 0x000F5DA2 File Offset: 0x000F3FA2
		// (set) Token: 0x06003BAC RID: 15276 RVA: 0x000F5DAA File Offset: 0x000F3FAA
		public Charset SourceCharset
		{
			get
			{
				return this.sourceCharset;
			}
			internal set
			{
				this.sourceCharset = value;
			}
		}

		// Token: 0x17001232 RID: 4658
		// (get) Token: 0x06003BAD RID: 15277 RVA: 0x000F5DB3 File Offset: 0x000F3FB3
		internal Encoding SourceEncoding
		{
			get
			{
				if (this.sourceCharset == null)
				{
					return null;
				}
				return this.sourceCharset.GetEncoding();
			}
		}

		// Token: 0x17001233 RID: 4659
		// (get) Token: 0x06003BAE RID: 15278 RVA: 0x000F5DCA File Offset: 0x000F3FCA
		// (set) Token: 0x06003BAF RID: 15279 RVA: 0x000F5DD2 File Offset: 0x000F3FD2
		internal bool TrustHtmlMetaTag
		{
			get
			{
				return this.trustHtmlMetaTag;
			}
			set
			{
				this.trustHtmlMetaTag = value;
			}
		}

		// Token: 0x17001234 RID: 4660
		// (get) Token: 0x06003BB0 RID: 15280 RVA: 0x000F5DDB File Offset: 0x000F3FDB
		public BodyFormat TargetFormat
		{
			get
			{
				return this.targetFormat;
			}
		}

		// Token: 0x17001235 RID: 4661
		// (get) Token: 0x06003BB1 RID: 15281 RVA: 0x000F5DE3 File Offset: 0x000F3FE3
		public Charset TargetCharset
		{
			get
			{
				return this.targetCharset;
			}
		}

		// Token: 0x17001236 RID: 4662
		// (get) Token: 0x06003BB2 RID: 15282 RVA: 0x000F5DEB File Offset: 0x000F3FEB
		internal Encoding TargetEncoding
		{
			get
			{
				if (this.targetCharset == null)
				{
					return null;
				}
				return this.targetCharset.GetEncoding();
			}
		}

		// Token: 0x17001237 RID: 4663
		// (get) Token: 0x06003BB3 RID: 15283 RVA: 0x000F5E02 File Offset: 0x000F4002
		public BodyCharsetFlags TargetCharsetFlags
		{
			get
			{
				return this.targetCharsetFlags;
			}
		}

		// Token: 0x17001238 RID: 4664
		// (get) Token: 0x06003BB4 RID: 15284 RVA: 0x000F5E0A File Offset: 0x000F400A
		public string InjectPrefix
		{
			get
			{
				return this.injectPrefix;
			}
		}

		// Token: 0x17001239 RID: 4665
		// (get) Token: 0x06003BB5 RID: 15285 RVA: 0x000F5E12 File Offset: 0x000F4012
		public string InjectSuffix
		{
			get
			{
				return this.injectSuffix;
			}
		}

		// Token: 0x1700123A RID: 4666
		// (get) Token: 0x06003BB6 RID: 15286 RVA: 0x000F5E1A File Offset: 0x000F401A
		public BodyInjectionFormat InjectionFormat
		{
			get
			{
				return this.injectFormat;
			}
		}

		// Token: 0x1700123B RID: 4667
		// (get) Token: 0x06003BB7 RID: 15287 RVA: 0x000F5E22 File Offset: 0x000F4022
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

		// Token: 0x1700123C RID: 4668
		// (get) Token: 0x06003BB8 RID: 15288 RVA: 0x000F5E30 File Offset: 0x000F4030
		// (set) Token: 0x06003BB9 RID: 15289 RVA: 0x000F5E38 File Offset: 0x000F4038
		public HtmlStreamingFlags HtmlFlags
		{
			get
			{
				return this.htmlFlags;
			}
			internal set
			{
				this.htmlFlags = value;
			}
		}

		// Token: 0x1700123D RID: 4669
		// (get) Token: 0x06003BBA RID: 15290 RVA: 0x000F5E41 File Offset: 0x000F4041
		public bool FilterHtml
		{
			get
			{
				return (this.htmlFlags & HtmlStreamingFlags.FilterHtml) == HtmlStreamingFlags.FilterHtml;
			}
		}

		// Token: 0x1700123E RID: 4670
		// (get) Token: 0x06003BBB RID: 15291 RVA: 0x000F5E4E File Offset: 0x000F404E
		// (set) Token: 0x06003BBC RID: 15292 RVA: 0x000F5E5B File Offset: 0x000F405B
		public HtmlCallbackBase HtmlCallback
		{
			get
			{
				return this.conversionCallbacks.HtmlCallback;
			}
			internal set
			{
				this.conversionCallbacks.HtmlCallback = value;
			}
		}

		// Token: 0x1700123F RID: 4671
		// (get) Token: 0x06003BBD RID: 15293 RVA: 0x000F5E69 File Offset: 0x000F4069
		// (set) Token: 0x06003BBE RID: 15294 RVA: 0x000F5E76 File Offset: 0x000F4076
		internal RtfCallbackBase RtfCallback
		{
			get
			{
				return this.conversionCallbacks.RtfCallback;
			}
			set
			{
				this.conversionCallbacks.RtfCallback = value;
			}
		}

		// Token: 0x17001240 RID: 4672
		// (get) Token: 0x06003BBF RID: 15295 RVA: 0x000F5E84 File Offset: 0x000F4084
		internal HtmlTagCallback InternalHtmlTagCallback
		{
			get
			{
				if (this.conversionCallbacks.HtmlCallback == null)
				{
					return null;
				}
				return new HtmlTagCallback(this.conversionCallbacks.HtmlCallback.ProcessTag);
			}
		}

		// Token: 0x17001241 RID: 4673
		// (get) Token: 0x06003BC0 RID: 15296 RVA: 0x000F5EAC File Offset: 0x000F40AC
		internal ImageRenderingCallback ImageRenderingCallback
		{
			get
			{
				if (this.conversionCallbacks.RtfCallback == null)
				{
					return null;
				}
				return new ImageRenderingCallback(this.conversionCallbacks.RtfCallback.ProcessImage);
			}
		}

		// Token: 0x04001FBE RID: 8126
		private BodyFormat sourceFormat;

		// Token: 0x04001FBF RID: 8127
		private Charset sourceCharset;

		// Token: 0x04001FC0 RID: 8128
		private BodyFormat targetFormat;

		// Token: 0x04001FC1 RID: 8129
		private Charset targetCharset;

		// Token: 0x04001FC2 RID: 8130
		private BodyCharsetFlags targetCharsetFlags;

		// Token: 0x04001FC3 RID: 8131
		private string injectPrefix;

		// Token: 0x04001FC4 RID: 8132
		private string injectSuffix;

		// Token: 0x04001FC5 RID: 8133
		private BodyInjectionFormat injectFormat;

		// Token: 0x04001FC6 RID: 8134
		private HtmlStreamingFlags htmlFlags;

		// Token: 0x04001FC7 RID: 8135
		private BodyConversionCallbacks conversionCallbacks = default(BodyConversionCallbacks);

		// Token: 0x04001FC8 RID: 8136
		private bool trustHtmlMetaTag;
	}
}
