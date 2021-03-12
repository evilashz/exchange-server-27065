using System;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x020002F5 RID: 757
	internal class ForwardReplyHeaderOptions
	{
		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x0600155C RID: 5468 RVA: 0x0006D641 File Offset: 0x0006B841
		public int ComposeFontSize
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x0600155D RID: 5469 RVA: 0x0006D644 File Offset: 0x0006B844
		public string ComposeFontColor
		{
			get
			{
				return "#000000";
			}
		}

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x0600155E RID: 5470 RVA: 0x0006D64B File Offset: 0x0006B84B
		public string ComposeFontName
		{
			get
			{
				return "Calibri";
			}
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x0600155F RID: 5471 RVA: 0x0006D652 File Offset: 0x0006B852
		public bool ComposeFontBold
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06001560 RID: 5472 RVA: 0x0006D655 File Offset: 0x0006B855
		public bool ComposeFontUnderline
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06001561 RID: 5473 RVA: 0x0006D658 File Offset: 0x0006B858
		public bool ComposeFontItalics
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06001562 RID: 5474 RVA: 0x0006D65B File Offset: 0x0006B85B
		public bool AutoAddSignature
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06001563 RID: 5475 RVA: 0x0006D65E File Offset: 0x0006B85E
		public string SignatureText
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x04000E84 RID: 3716
		private const string ComposeFontNameValue = "Calibri";

		// Token: 0x04000E85 RID: 3717
		private const int ComposeFontSizeValue = 3;

		// Token: 0x04000E86 RID: 3718
		private const string ComposeFontColorValue = "#000000";

		// Token: 0x04000E87 RID: 3719
		private const bool ComposeFontBoldValue = false;

		// Token: 0x04000E88 RID: 3720
		private const bool ComposeFontUnderlineValue = false;

		// Token: 0x04000E89 RID: 3721
		private const bool ComposeFontItalicsValue = false;

		// Token: 0x04000E8A RID: 3722
		private const bool AutoAddSignatureValue = false;
	}
}
