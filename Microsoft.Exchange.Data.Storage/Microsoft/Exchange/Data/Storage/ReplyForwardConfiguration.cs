using System;
using System.Globalization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000860 RID: 2144
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ReplyForwardConfiguration
	{
		// Token: 0x060050AD RID: 20653 RVA: 0x0014F185 File Offset: 0x0014D385
		public ReplyForwardConfiguration(BodyFormat targetFormat) : this(targetFormat, ForwardCreationFlags.None, null)
		{
		}

		// Token: 0x060050AE RID: 20654 RVA: 0x0014F190 File Offset: 0x0014D390
		internal ReplyForwardConfiguration(CultureInfo culture) : this(BodyFormat.TextPlain, ForwardCreationFlags.None, culture)
		{
		}

		// Token: 0x060050AF RID: 20655 RVA: 0x0014F19B File Offset: 0x0014D39B
		internal ReplyForwardConfiguration(ForwardCreationFlags flags, CultureInfo culture) : this(BodyFormat.TextPlain, flags, culture)
		{
		}

		// Token: 0x060050B0 RID: 20656 RVA: 0x0014F1A8 File Offset: 0x0014D3A8
		internal ReplyForwardConfiguration(BodyFormat targetFormat, ForwardCreationFlags flags, CultureInfo culture)
		{
			EnumValidator.ThrowIfInvalid<BodyFormat>(targetFormat, "targetFormat");
			EnumValidator.ThrowIfInvalid<ForwardCreationFlags>(flags, "flags");
			this.targetFormat = targetFormat;
			this.forwardCreationFlags = flags;
			this.culture = culture;
			this.bodyPrefix = null;
			this.conversionCallbacks = null;
			this.subjectPrefix = null;
		}

		// Token: 0x1700169E RID: 5790
		// (get) Token: 0x060050B1 RID: 20657 RVA: 0x0014F1FB File Offset: 0x0014D3FB
		public BodyFormat TargetFormat
		{
			get
			{
				return this.targetFormat;
			}
		}

		// Token: 0x1700169F RID: 5791
		// (get) Token: 0x060050B2 RID: 20658 RVA: 0x0014F203 File Offset: 0x0014D403
		// (set) Token: 0x060050B3 RID: 20659 RVA: 0x0014F210 File Offset: 0x0014D410
		public HtmlCallbackBase HtmlCallbacks
		{
			get
			{
				return this.conversionCallbacks as HtmlCallbackBase;
			}
			set
			{
				this.conversionCallbacks = value;
			}
		}

		// Token: 0x170016A0 RID: 5792
		// (get) Token: 0x060050B4 RID: 20660 RVA: 0x0014F219 File Offset: 0x0014D419
		// (set) Token: 0x060050B5 RID: 20661 RVA: 0x0014F221 File Offset: 0x0014D421
		public bool ShouldSkipFilterHtmlOnBodyWrite { get; set; }

		// Token: 0x170016A1 RID: 5793
		// (get) Token: 0x060050B6 RID: 20662 RVA: 0x0014F22A File Offset: 0x0014D42A
		// (set) Token: 0x060050B7 RID: 20663 RVA: 0x0014F237 File Offset: 0x0014D437
		internal RtfCallbackBase RtfCallbacks
		{
			get
			{
				return this.conversionCallbacks as RtfCallbackBase;
			}
			set
			{
				this.conversionCallbacks = value;
			}
		}

		// Token: 0x170016A2 RID: 5794
		// (get) Token: 0x060050B8 RID: 20664 RVA: 0x0014F240 File Offset: 0x0014D440
		public string BodyPrefix
		{
			get
			{
				return this.bodyPrefix;
			}
		}

		// Token: 0x170016A3 RID: 5795
		// (get) Token: 0x060050B9 RID: 20665 RVA: 0x0014F248 File Offset: 0x0014D448
		public BodyInjectionFormat BodyPrefixFormat
		{
			get
			{
				return this.bodyPrefixFormat;
			}
		}

		// Token: 0x060050BA RID: 20666 RVA: 0x0014F250 File Offset: 0x0014D450
		public void AddBodyPrefix(string bodyPrefix)
		{
			this.bodyPrefix = bodyPrefix;
			this.bodyPrefixFormat = ((this.targetFormat == BodyFormat.TextPlain) ? BodyInjectionFormat.Text : BodyInjectionFormat.Html);
		}

		// Token: 0x060050BB RID: 20667 RVA: 0x0014F26C File Offset: 0x0014D46C
		public void AddBodyPrefix(string bodyPrefix, BodyInjectionFormat bodyPrefixFormat)
		{
			EnumValidator.ThrowIfInvalid<BodyInjectionFormat>(bodyPrefixFormat, "bodyPrefixFormat");
			this.bodyPrefix = bodyPrefix;
			this.bodyPrefixFormat = bodyPrefixFormat;
		}

		// Token: 0x170016A4 RID: 5796
		// (get) Token: 0x060050BC RID: 20668 RVA: 0x0014F287 File Offset: 0x0014D487
		// (set) Token: 0x060050BD RID: 20669 RVA: 0x0014F28F File Offset: 0x0014D48F
		public CultureInfo Culture
		{
			get
			{
				return this.culture;
			}
			set
			{
				this.culture = value;
			}
		}

		// Token: 0x170016A5 RID: 5797
		// (get) Token: 0x060050BE RID: 20670 RVA: 0x0014F298 File Offset: 0x0014D498
		// (set) Token: 0x060050BF RID: 20671 RVA: 0x0014F2A0 File Offset: 0x0014D4A0
		public ForwardCreationFlags ForwardCreationFlags
		{
			get
			{
				return this.forwardCreationFlags;
			}
			set
			{
				EnumValidator.ThrowIfInvalid<ForwardCreationFlags>(value, "value");
				this.forwardCreationFlags = value;
			}
		}

		// Token: 0x170016A6 RID: 5798
		// (get) Token: 0x060050C0 RID: 20672 RVA: 0x0014F2B4 File Offset: 0x0014D4B4
		// (set) Token: 0x060050C1 RID: 20673 RVA: 0x0014F2BC File Offset: 0x0014D4BC
		public string SubjectPrefix
		{
			get
			{
				return this.subjectPrefix;
			}
			set
			{
				this.subjectPrefix = value;
			}
		}

		// Token: 0x170016A7 RID: 5799
		// (get) Token: 0x060050C2 RID: 20674 RVA: 0x0014F2C5 File Offset: 0x0014D4C5
		// (set) Token: 0x060050C3 RID: 20675 RVA: 0x0014F2CD File Offset: 0x0014D4CD
		public string XLoop
		{
			get
			{
				return this.xLoop;
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					throw new ArgumentException("Value must not be null or empty", "value");
				}
				this.xLoop = value;
			}
		}

		// Token: 0x170016A8 RID: 5800
		// (get) Token: 0x060050C4 RID: 20676 RVA: 0x0014F2EE File Offset: 0x0014D4EE
		// (set) Token: 0x060050C5 RID: 20677 RVA: 0x0014F2F6 File Offset: 0x0014D4F6
		public InboundConversionOptions ConversionOptionsForSmime
		{
			get
			{
				return this.optionsForSmime;
			}
			set
			{
				this.optionsForSmime = value;
			}
		}

		// Token: 0x170016A9 RID: 5801
		// (get) Token: 0x060050C6 RID: 20678 RVA: 0x0014F2FF File Offset: 0x0014D4FF
		// (set) Token: 0x060050C7 RID: 20679 RVA: 0x0014F307 File Offset: 0x0014D507
		public ExTimeZone TimeZone
		{
			get
			{
				return this.timeZone;
			}
			set
			{
				this.timeZone = value;
			}
		}

		// Token: 0x170016AA RID: 5802
		// (get) Token: 0x060050C8 RID: 20680 RVA: 0x0014F310 File Offset: 0x0014D510
		// (set) Token: 0x060050C9 RID: 20681 RVA: 0x0014F318 File Offset: 0x0014D518
		public bool ShouldSuppressReadReceipt
		{
			get
			{
				return this.shouldSuppressReadReceipt;
			}
			set
			{
				this.shouldSuppressReadReceipt = value;
			}
		}

		// Token: 0x04002C27 RID: 11303
		private string xLoop;

		// Token: 0x04002C28 RID: 11304
		private string bodyPrefix;

		// Token: 0x04002C29 RID: 11305
		private string subjectPrefix;

		// Token: 0x04002C2A RID: 11306
		private BodyFormat targetFormat;

		// Token: 0x04002C2B RID: 11307
		private BodyInjectionFormat bodyPrefixFormat;

		// Token: 0x04002C2C RID: 11308
		private ConversionCallbackBase conversionCallbacks;

		// Token: 0x04002C2D RID: 11309
		private CultureInfo culture;

		// Token: 0x04002C2E RID: 11310
		private ForwardCreationFlags forwardCreationFlags;

		// Token: 0x04002C2F RID: 11311
		private InboundConversionOptions optionsForSmime;

		// Token: 0x04002C30 RID: 11312
		private ExTimeZone timeZone;

		// Token: 0x04002C31 RID: 11313
		private bool shouldSuppressReadReceipt;
	}
}
