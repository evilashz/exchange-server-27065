using System;
using Microsoft.Exchange.Data.TextConverters.Internal.Html;

namespace Microsoft.Exchange.Data.TextConverters
{
	// Token: 0x02000230 RID: 560
	internal struct HtmlTagParts
	{
		// Token: 0x0600171A RID: 5914 RVA: 0x000B43DD File Offset: 0x000B25DD
		internal HtmlTagParts(HtmlToken.TagPartMajor major, HtmlToken.TagPartMinor minor)
		{
			this.minor = minor;
			this.major = major;
		}

		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x0600171B RID: 5915 RVA: 0x000B43ED File Offset: 0x000B25ED
		public bool Begin
		{
			get
			{
				return 3 == (byte)(this.major & HtmlToken.TagPartMajor.Begin);
			}
		}

		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x0600171C RID: 5916 RVA: 0x000B43FB File Offset: 0x000B25FB
		public bool End
		{
			get
			{
				return 6 == (byte)(this.major & HtmlToken.TagPartMajor.End);
			}
		}

		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x0600171D RID: 5917 RVA: 0x000B4409 File Offset: 0x000B2609
		public bool Complete
		{
			get
			{
				return HtmlToken.TagPartMajor.Complete == this.major;
			}
		}

		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x0600171E RID: 5918 RVA: 0x000B4414 File Offset: 0x000B2614
		public bool NameBegin
		{
			get
			{
				return 3 == (byte)(this.minor & HtmlToken.TagPartMinor.BeginName);
			}
		}

		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x0600171F RID: 5919 RVA: 0x000B4422 File Offset: 0x000B2622
		public bool Name
		{
			get
			{
				return 2 == (byte)(this.minor & HtmlToken.TagPartMinor.ContinueName);
			}
		}

		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x06001720 RID: 5920 RVA: 0x000B4430 File Offset: 0x000B2630
		public bool NameEnd
		{
			get
			{
				return 6 == (byte)(this.minor & HtmlToken.TagPartMinor.EndName);
			}
		}

		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x06001721 RID: 5921 RVA: 0x000B443E File Offset: 0x000B263E
		public bool NameComplete
		{
			get
			{
				return 7 == (byte)(this.minor & HtmlToken.TagPartMinor.CompleteName);
			}
		}

		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x06001722 RID: 5922 RVA: 0x000B444C File Offset: 0x000B264C
		public bool Attributes
		{
			get
			{
				return 0 != (byte)(this.minor & (HtmlToken.TagPartMinor)144);
			}
		}

		// Token: 0x06001723 RID: 5923 RVA: 0x000B4461 File Offset: 0x000B2661
		public override string ToString()
		{
			return this.major.ToString() + " /" + this.minor.ToString() + "/";
		}

		// Token: 0x06001724 RID: 5924 RVA: 0x000B4492 File Offset: 0x000B2692
		internal void Reset()
		{
			this.minor = HtmlToken.TagPartMinor.Empty;
			this.major = HtmlToken.TagPartMajor.None;
		}

		// Token: 0x04001A76 RID: 6774
		private HtmlToken.TagPartMajor major;

		// Token: 0x04001A77 RID: 6775
		private HtmlToken.TagPartMinor minor;
	}
}
