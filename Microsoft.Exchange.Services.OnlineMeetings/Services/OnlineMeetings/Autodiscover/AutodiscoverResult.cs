using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Services.OnlineMeetings.Autodiscover
{
	// Token: 0x02000031 RID: 49
	internal class AutodiscoverResult : LogEntry
	{
		// Token: 0x060001C9 RID: 457 RVA: 0x000067B6 File Offset: 0x000049B6
		internal AutodiscoverResult()
		{
			this.AuthenticatedRedirects = new List<string>();
			this.UnauthenticatedRedirects = new List<string>();
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060001CA RID: 458 RVA: 0x000067D4 File Offset: 0x000049D4
		// (set) Token: 0x060001CB RID: 459 RVA: 0x000067DC File Offset: 0x000049DC
		internal string SipUri { get; set; }

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060001CC RID: 460 RVA: 0x000067E5 File Offset: 0x000049E5
		// (set) Token: 0x060001CD RID: 461 RVA: 0x000067ED File Offset: 0x000049ED
		internal bool IsOnlineMeetingEnabled { get; set; }

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060001CE RID: 462 RVA: 0x000067F6 File Offset: 0x000049F6
		// (set) Token: 0x060001CF RID: 463 RVA: 0x000067FE File Offset: 0x000049FE
		internal bool IsAuthdServerFromCache { get; set; }

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060001D0 RID: 464 RVA: 0x00006807 File Offset: 0x00004A07
		// (set) Token: 0x060001D1 RID: 465 RVA: 0x0000680F File Offset: 0x00004A0F
		internal string AuthenticatedLyncAutodiscoverServer { get; set; }

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060001D2 RID: 466 RVA: 0x00006818 File Offset: 0x00004A18
		// (set) Token: 0x060001D3 RID: 467 RVA: 0x00006820 File Offset: 0x00004A20
		internal bool IsUcwaUrlFromCache { get; set; }

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060001D4 RID: 468 RVA: 0x00006829 File Offset: 0x00004A29
		internal bool IsUcwaSupported
		{
			get
			{
				return !string.IsNullOrEmpty(this.UcwaDiscoveryUrl);
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x00006839 File Offset: 0x00004A39
		// (set) Token: 0x060001D6 RID: 470 RVA: 0x00006841 File Offset: 0x00004A41
		internal string UcwaDiscoveryUrl { get; set; }

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x0000684A File Offset: 0x00004A4A
		// (set) Token: 0x060001D8 RID: 472 RVA: 0x00006852 File Offset: 0x00004A52
		internal List<string> UnauthenticatedRedirects { get; set; }

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x0000685B File Offset: 0x00004A5B
		// (set) Token: 0x060001DA RID: 474 RVA: 0x00006863 File Offset: 0x00004A63
		internal List<string> AuthenticatedRedirects { get; set; }

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060001DB RID: 475 RVA: 0x0000686C File Offset: 0x00004A6C
		// (set) Token: 0x060001DC RID: 476 RVA: 0x00006874 File Offset: 0x00004A74
		internal AutodiscoverError Error { get; set; }

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060001DD RID: 477 RVA: 0x0000687D File Offset: 0x00004A7D
		// (set) Token: 0x060001DE RID: 478 RVA: 0x00006885 File Offset: 0x00004A85
		internal string ResponseBody { get; set; }

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060001DF RID: 479 RVA: 0x0000688E File Offset: 0x00004A8E
		internal bool HasError
		{
			get
			{
				return this.Error != null;
			}
		}
	}
}
