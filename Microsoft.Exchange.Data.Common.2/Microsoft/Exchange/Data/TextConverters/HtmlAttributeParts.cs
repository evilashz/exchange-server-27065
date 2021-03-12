using System;
using Microsoft.Exchange.Data.TextConverters.Internal.Html;

namespace Microsoft.Exchange.Data.TextConverters
{
	// Token: 0x02000231 RID: 561
	internal struct HtmlAttributeParts
	{
		// Token: 0x06001725 RID: 5925 RVA: 0x000B44A2 File Offset: 0x000B26A2
		internal HtmlAttributeParts(HtmlToken.AttrPartMajor major, HtmlToken.AttrPartMinor minor)
		{
			this.minor = minor;
			this.major = major;
		}

		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x06001726 RID: 5926 RVA: 0x000B44B2 File Offset: 0x000B26B2
		public bool Begin
		{
			get
			{
				return 24 == (byte)(this.major & HtmlToken.AttrPartMajor.Begin);
			}
		}

		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x06001727 RID: 5927 RVA: 0x000B44C2 File Offset: 0x000B26C2
		public bool End
		{
			get
			{
				return 48 == (byte)(this.major & HtmlToken.AttrPartMajor.End);
			}
		}

		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x06001728 RID: 5928 RVA: 0x000B44D2 File Offset: 0x000B26D2
		public bool Complete
		{
			get
			{
				return HtmlToken.AttrPartMajor.Complete == this.major;
			}
		}

		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x06001729 RID: 5929 RVA: 0x000B44DE File Offset: 0x000B26DE
		public bool NameBegin
		{
			get
			{
				return 3 == (byte)(this.minor & HtmlToken.AttrPartMinor.BeginName);
			}
		}

		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x0600172A RID: 5930 RVA: 0x000B44EC File Offset: 0x000B26EC
		public bool Name
		{
			get
			{
				return 2 == (byte)(this.minor & HtmlToken.AttrPartMinor.ContinueName);
			}
		}

		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x0600172B RID: 5931 RVA: 0x000B44FA File Offset: 0x000B26FA
		public bool NameEnd
		{
			get
			{
				return 6 == (byte)(this.minor & HtmlToken.AttrPartMinor.EndName);
			}
		}

		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x0600172C RID: 5932 RVA: 0x000B4508 File Offset: 0x000B2708
		public bool NameComplete
		{
			get
			{
				return 7 == (byte)(this.minor & HtmlToken.AttrPartMinor.CompleteName);
			}
		}

		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x0600172D RID: 5933 RVA: 0x000B4516 File Offset: 0x000B2716
		public bool ValueBegin
		{
			get
			{
				return 24 == (byte)(this.minor & HtmlToken.AttrPartMinor.BeginValue);
			}
		}

		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x0600172E RID: 5934 RVA: 0x000B4526 File Offset: 0x000B2726
		public bool Value
		{
			get
			{
				return 16 == (byte)(this.minor & HtmlToken.AttrPartMinor.ContinueValue);
			}
		}

		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x0600172F RID: 5935 RVA: 0x000B4536 File Offset: 0x000B2736
		public bool ValueEnd
		{
			get
			{
				return 48 == (byte)(this.minor & HtmlToken.AttrPartMinor.EndValue);
			}
		}

		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x06001730 RID: 5936 RVA: 0x000B4546 File Offset: 0x000B2746
		public bool ValueComplete
		{
			get
			{
				return 56 == (byte)(this.minor & HtmlToken.AttrPartMinor.CompleteValue);
			}
		}

		// Token: 0x06001731 RID: 5937 RVA: 0x000B4556 File Offset: 0x000B2756
		public override string ToString()
		{
			return this.major.ToString() + " /" + this.minor.ToString() + "/";
		}

		// Token: 0x04001A78 RID: 6776
		private HtmlToken.AttrPartMajor major;

		// Token: 0x04001A79 RID: 6777
		private HtmlToken.AttrPartMinor minor;
	}
}
