using System;
using System.Globalization;
using Microsoft.Exchange.Data.Globalization;

namespace Microsoft.Exchange.Extensibility.Internal
{
	// Token: 0x0200003F RID: 63
	internal sealed class QuotaInformation
	{
		// Token: 0x060002A4 RID: 676 RVA: 0x000101C8 File Offset: 0x0000E3C8
		public QuotaInformation(Charset messageCharset, CultureInfo culture, string subject, string topText, string details, string finalText, string topTextFont, string finalTextFont, string bodyTextFont, string currentSizeTitle, string currentSizeText, string maxSizeTitle, string maxSizeText, bool hasMaxSize, bool isWarning, float percentFull)
		{
			this.culture = culture;
			this.CurrentSizeText = currentSizeText;
			this.TopText = topText;
			this.Details = details;
			this.FinalText = finalText;
			this.TopTextFont = topTextFont;
			this.FinalTextFont = finalTextFont;
			this.BodyTextFont = bodyTextFont;
			this.HasMaxSize = hasMaxSize;
			this.CurrentSizeTitle = currentSizeTitle;
			this.MaxSizeTitle = maxSizeTitle;
			this.MaxSizeText = maxSizeText;
			this.IsWarning = isWarning;
			this.messageCharset = messageCharset;
			this.PercentFull = percentFull;
			this.Subject = subject;
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x00010258 File Offset: 0x0000E458
		public CultureInfo Culture
		{
			get
			{
				return this.culture;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060002A6 RID: 678 RVA: 0x00010260 File Offset: 0x0000E460
		public Charset MessageCharset
		{
			get
			{
				return this.messageCharset;
			}
		}

		// Token: 0x040002FF RID: 767
		public readonly bool HasMaxSize;

		// Token: 0x04000300 RID: 768
		public readonly string CurrentSizeText;

		// Token: 0x04000301 RID: 769
		public readonly string Subject;

		// Token: 0x04000302 RID: 770
		public readonly string TopText;

		// Token: 0x04000303 RID: 771
		public readonly string FinalText;

		// Token: 0x04000304 RID: 772
		public readonly string Details;

		// Token: 0x04000305 RID: 773
		public readonly string TopTextFont;

		// Token: 0x04000306 RID: 774
		public readonly string FinalTextFont;

		// Token: 0x04000307 RID: 775
		public readonly string BodyTextFont;

		// Token: 0x04000308 RID: 776
		public readonly string CurrentSizeTitle;

		// Token: 0x04000309 RID: 777
		public readonly string MaxSizeTitle;

		// Token: 0x0400030A RID: 778
		public readonly string MaxSizeText;

		// Token: 0x0400030B RID: 779
		public readonly bool IsWarning;

		// Token: 0x0400030C RID: 780
		public readonly float PercentFull;

		// Token: 0x0400030D RID: 781
		private Charset messageCharset;

		// Token: 0x0400030E RID: 782
		private CultureInfo culture;
	}
}
