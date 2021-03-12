using System;
using Microsoft.Exchange.Clients.Common;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000053 RID: 83
	public class ErrorInformation
	{
		// Token: 0x0600028D RID: 653 RVA: 0x0000EE52 File Offset: 0x0000D052
		public ErrorInformation()
		{
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0000EE74 File Offset: 0x0000D074
		public ErrorInformation(int httpCode)
		{
			this.httpCode = httpCode;
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0000EE9D File Offset: 0x0000D09D
		public ErrorInformation(int httpCode, string details)
		{
			this.httpCode = httpCode;
			this.messageDetails = details;
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0000EECD File Offset: 0x0000D0CD
		public ErrorInformation(int httpCode, string details, bool sharePointApp)
		{
			this.httpCode = httpCode;
			this.messageDetails = details;
			this.SharePointApp = sharePointApp;
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000291 RID: 657 RVA: 0x0000EF04 File Offset: 0x0000D104
		// (set) Token: 0x06000292 RID: 658 RVA: 0x0000EF0C File Offset: 0x0000D10C
		public ErrorMode? ErrorMode { get; set; }

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000293 RID: 659 RVA: 0x0000EF15 File Offset: 0x0000D115
		// (set) Token: 0x06000294 RID: 660 RVA: 0x0000EF1D File Offset: 0x0000D11D
		public Exception Exception
		{
			get
			{
				return this.exception;
			}
			set
			{
				this.exception = value;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000295 RID: 661 RVA: 0x0000EF26 File Offset: 0x0000D126
		// (set) Token: 0x06000296 RID: 662 RVA: 0x0000EF2E File Offset: 0x0000D12E
		public int HttpCode
		{
			get
			{
				return this.httpCode;
			}
			set
			{
				this.httpCode = value;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000297 RID: 663 RVA: 0x0000EF37 File Offset: 0x0000D137
		// (set) Token: 0x06000298 RID: 664 RVA: 0x0000EF3F File Offset: 0x0000D13F
		public bool IsErrorMessageHtmlEncoded
		{
			get
			{
				return this.isErrorMessageHtmlEncoded;
			}
			set
			{
				this.isErrorMessageHtmlEncoded = value;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000299 RID: 665 RVA: 0x0000EF48 File Offset: 0x0000D148
		// (set) Token: 0x0600029A RID: 666 RVA: 0x0000EF50 File Offset: 0x0000D150
		public string MessageDetails
		{
			get
			{
				return this.messageDetails;
			}
			set
			{
				this.messageDetails = value;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x0600029B RID: 667 RVA: 0x0000EF59 File Offset: 0x0000D159
		// (set) Token: 0x0600029C RID: 668 RVA: 0x0000EF61 File Offset: 0x0000D161
		public bool IsDetailedErrorHtmlEncoded
		{
			get
			{
				return this.isDetailedErrorMessageHtmlEncoded;
			}
			set
			{
				this.isDetailedErrorMessageHtmlEncoded = value;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x0600029D RID: 669 RVA: 0x0000EF6A File Offset: 0x0000D16A
		// (set) Token: 0x0600029E RID: 670 RVA: 0x0000EF72 File Offset: 0x0000D172
		public ThemeFileId Icon
		{
			get
			{
				return this.icon;
			}
			set
			{
				this.icon = value;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x0600029F RID: 671 RVA: 0x0000EF7B File Offset: 0x0000D17B
		// (set) Token: 0x060002A0 RID: 672 RVA: 0x0000EF83 File Offset: 0x0000D183
		public ThemeFileId Background
		{
			get
			{
				return this.background;
			}
			set
			{
				this.background = value;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060002A1 RID: 673 RVA: 0x0000EF8C File Offset: 0x0000D18C
		// (set) Token: 0x060002A2 RID: 674 RVA: 0x0000EF94 File Offset: 0x0000D194
		public OwaUrl OwaUrl
		{
			get
			{
				return this.owaUrl;
			}
			set
			{
				this.owaUrl = value;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060002A3 RID: 675 RVA: 0x0000EF9D File Offset: 0x0000D19D
		// (set) Token: 0x060002A4 RID: 676 RVA: 0x0000EFA5 File Offset: 0x0000D1A5
		public string PreviousPageUrl
		{
			get
			{
				return this.previousPageUrl;
			}
			set
			{
				this.previousPageUrl = value;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x0000EFAE File Offset: 0x0000D1AE
		// (set) Token: 0x060002A6 RID: 678 RVA: 0x0000EFB6 File Offset: 0x0000D1B6
		public string ExternalPageLink
		{
			get
			{
				return this.externalPageUrl;
			}
			set
			{
				this.externalPageUrl = value;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060002A7 RID: 679 RVA: 0x0000EFBF File Offset: 0x0000D1BF
		// (set) Token: 0x060002A8 RID: 680 RVA: 0x0000EFC7 File Offset: 0x0000D1C7
		public bool ShowLogoffAndWorkButton
		{
			get
			{
				return this.showLogOffAndContinueBrowse;
			}
			set
			{
				this.showLogOffAndContinueBrowse = value;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060002A9 RID: 681 RVA: 0x0000EFD0 File Offset: 0x0000D1D0
		// (set) Token: 0x060002AA RID: 682 RVA: 0x0000EFD8 File Offset: 0x0000D1D8
		public bool SharePointApp
		{
			get
			{
				return this.sharePointApp;
			}
			set
			{
				this.sharePointApp = value;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060002AB RID: 683 RVA: 0x0000EFE1 File Offset: 0x0000D1E1
		// (set) Token: 0x060002AC RID: 684 RVA: 0x0000EFE9 File Offset: 0x0000D1E9
		public bool SiteMailbox
		{
			get
			{
				return this.siteMailbox;
			}
			set
			{
				this.siteMailbox = value;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060002AD RID: 685 RVA: 0x0000EFF2 File Offset: 0x0000D1F2
		public bool GroupMailbox
		{
			get
			{
				return !string.IsNullOrEmpty(this.groupMailboxDestination);
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060002AE RID: 686 RVA: 0x0000F002 File Offset: 0x0000D202
		// (set) Token: 0x060002AF RID: 687 RVA: 0x0000F00A File Offset: 0x0000D20A
		public string GroupMailboxDestination
		{
			get
			{
				return this.groupMailboxDestination;
			}
			set
			{
				this.groupMailboxDestination = value;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060002B0 RID: 688 RVA: 0x0000F013 File Offset: 0x0000D213
		// (set) Token: 0x060002B1 RID: 689 RVA: 0x0000F01B File Offset: 0x0000D21B
		public string RedirectionUrl { get; set; }

		// Token: 0x04000169 RID: 361
		private Exception exception;

		// Token: 0x0400016A RID: 362
		private int httpCode;

		// Token: 0x0400016B RID: 363
		private string messageDetails;

		// Token: 0x0400016C RID: 364
		private ThemeFileId icon = ThemeFileId.Error;

		// Token: 0x0400016D RID: 365
		private ThemeFileId background;

		// Token: 0x0400016E RID: 366
		private OwaUrl owaUrl = OwaUrl.ErrorPage;

		// Token: 0x0400016F RID: 367
		private bool isDetailedErrorMessageHtmlEncoded;

		// Token: 0x04000170 RID: 368
		private bool isErrorMessageHtmlEncoded;

		// Token: 0x04000171 RID: 369
		private string previousPageUrl;

		// Token: 0x04000172 RID: 370
		private string externalPageUrl;

		// Token: 0x04000173 RID: 371
		private bool showLogOffAndContinueBrowse = true;

		// Token: 0x04000174 RID: 372
		private bool sharePointApp;

		// Token: 0x04000175 RID: 373
		private bool siteMailbox;

		// Token: 0x04000176 RID: 374
		private string groupMailboxDestination;
	}
}
