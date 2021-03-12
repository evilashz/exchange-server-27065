using System;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x0200010A RID: 266
	public class ErrorInformation
	{
		// Token: 0x060008D0 RID: 2256 RVA: 0x0004097C File Offset: 0x0003EB7C
		public ErrorInformation()
		{
		}

		// Token: 0x060008D1 RID: 2257 RVA: 0x000409A6 File Offset: 0x0003EBA6
		public ErrorInformation(string message)
		{
			this.message = message;
		}

		// Token: 0x060008D2 RID: 2258 RVA: 0x000409D7 File Offset: 0x0003EBD7
		public ErrorInformation(string message, string details)
		{
			this.message = message;
			this.messageDetails = details;
		}

		// Token: 0x060008D3 RID: 2259 RVA: 0x00040A0F File Offset: 0x0003EC0F
		public ErrorInformation(string message, OwaEventHandlerErrorCode errorCode)
		{
			this.message = message;
			this.OwaEventHandlerErrorCode = errorCode;
		}

		// Token: 0x060008D4 RID: 2260 RVA: 0x00040A47 File Offset: 0x0003EC47
		public ErrorInformation(string message, string details, OwaEventHandlerErrorCode errorCode)
		{
			this.message = message;
			this.messageDetails = details;
			this.OwaEventHandlerErrorCode = errorCode;
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x060008D5 RID: 2261 RVA: 0x00040A86 File Offset: 0x0003EC86
		// (set) Token: 0x060008D6 RID: 2262 RVA: 0x00040A8E File Offset: 0x0003EC8E
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

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x060008D7 RID: 2263 RVA: 0x00040A97 File Offset: 0x0003EC97
		// (set) Token: 0x060008D8 RID: 2264 RVA: 0x00040A9F File Offset: 0x0003EC9F
		public string Message
		{
			get
			{
				return this.message;
			}
			set
			{
				this.message = value;
			}
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x060008D9 RID: 2265 RVA: 0x00040AA8 File Offset: 0x0003ECA8
		// (set) Token: 0x060008DA RID: 2266 RVA: 0x00040AB0 File Offset: 0x0003ECB0
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

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x060008DB RID: 2267 RVA: 0x00040AB9 File Offset: 0x0003ECB9
		// (set) Token: 0x060008DC RID: 2268 RVA: 0x00040AC1 File Offset: 0x0003ECC1
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

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x060008DD RID: 2269 RVA: 0x00040ACA File Offset: 0x0003ECCA
		// (set) Token: 0x060008DE RID: 2270 RVA: 0x00040AD2 File Offset: 0x0003ECD2
		public OwaEventHandlerErrorCode OwaEventHandlerErrorCode
		{
			get
			{
				return this.owaEventHandlerErrorCode;
			}
			set
			{
				this.owaEventHandlerErrorCode = value;
			}
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x060008DF RID: 2271 RVA: 0x00040ADB File Offset: 0x0003ECDB
		// (set) Token: 0x060008E0 RID: 2272 RVA: 0x00040AE3 File Offset: 0x0003ECE3
		public bool HideDebugInformation
		{
			get
			{
				return this.hideDebugInformation;
			}
			set
			{
				this.hideDebugInformation = value;
			}
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x060008E1 RID: 2273 RVA: 0x00040AEC File Offset: 0x0003ECEC
		// (set) Token: 0x060008E2 RID: 2274 RVA: 0x00040AF4 File Offset: 0x0003ECF4
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

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x060008E3 RID: 2275 RVA: 0x00040AFD File Offset: 0x0003ECFD
		// (set) Token: 0x060008E4 RID: 2276 RVA: 0x00040B05 File Offset: 0x0003ED05
		public bool SendWatsonReport
		{
			get
			{
				return this.sendWatsonReport;
			}
			set
			{
				this.sendWatsonReport = value;
			}
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x060008E5 RID: 2277 RVA: 0x00040B0E File Offset: 0x0003ED0E
		// (set) Token: 0x060008E6 RID: 2278 RVA: 0x00040B16 File Offset: 0x0003ED16
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

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x060008E7 RID: 2279 RVA: 0x00040B1F File Offset: 0x0003ED1F
		// (set) Token: 0x060008E8 RID: 2280 RVA: 0x00040B27 File Offset: 0x0003ED27
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

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x060008E9 RID: 2281 RVA: 0x00040B30 File Offset: 0x0003ED30
		// (set) Token: 0x060008EA RID: 2282 RVA: 0x00040B38 File Offset: 0x0003ED38
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

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x060008EB RID: 2283 RVA: 0x00040B41 File Offset: 0x0003ED41
		// (set) Token: 0x060008EC RID: 2284 RVA: 0x00040B49 File Offset: 0x0003ED49
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

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x060008ED RID: 2285 RVA: 0x00040B52 File Offset: 0x0003ED52
		// (set) Token: 0x060008EE RID: 2286 RVA: 0x00040B5A File Offset: 0x0003ED5A
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

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x060008EF RID: 2287 RVA: 0x00040B63 File Offset: 0x0003ED63
		// (set) Token: 0x060008F0 RID: 2288 RVA: 0x00040B6B File Offset: 0x0003ED6B
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

		// Token: 0x0400063F RID: 1599
		private Exception exception;

		// Token: 0x04000640 RID: 1600
		private string message;

		// Token: 0x04000641 RID: 1601
		private string messageDetails;

		// Token: 0x04000642 RID: 1602
		private OwaEventHandlerErrorCode owaEventHandlerErrorCode = OwaEventHandlerErrorCode.NotSet;

		// Token: 0x04000643 RID: 1603
		private bool hideDebugInformation;

		// Token: 0x04000644 RID: 1604
		private bool sendWatsonReport;

		// Token: 0x04000645 RID: 1605
		private ThemeFileId icon = ThemeFileId.Error;

		// Token: 0x04000646 RID: 1606
		private ThemeFileId background;

		// Token: 0x04000647 RID: 1607
		private OwaUrl owaUrl = OwaUrl.ErrorPage;

		// Token: 0x04000648 RID: 1608
		private bool isDetailedErrorMessageHtmlEncoded;

		// Token: 0x04000649 RID: 1609
		private bool isErrorMessageHtmlEncoded;

		// Token: 0x0400064A RID: 1610
		private string previousPageUrl;

		// Token: 0x0400064B RID: 1611
		private string externalPageUrl;

		// Token: 0x0400064C RID: 1612
		private bool showLogOffAndContinueBrowse = true;
	}
}
