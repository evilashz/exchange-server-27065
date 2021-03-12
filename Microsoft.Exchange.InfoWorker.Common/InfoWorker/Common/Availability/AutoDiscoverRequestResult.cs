using System;
using System.Text;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x02000064 RID: 100
	internal sealed class AutoDiscoverRequestResult
	{
		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600025B RID: 603 RVA: 0x0000B7D8 File Offset: 0x000099D8
		// (set) Token: 0x0600025C RID: 604 RVA: 0x0000B7E0 File Offset: 0x000099E0
		public Exception Exception { get; private set; }

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x0600025D RID: 605 RVA: 0x0000B7E9 File Offset: 0x000099E9
		// (set) Token: 0x0600025E RID: 606 RVA: 0x0000B7F1 File Offset: 0x000099F1
		public string RedirectAddress { get; private set; }

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x0600025F RID: 607 RVA: 0x0000B7FA File Offset: 0x000099FA
		// (set) Token: 0x06000260 RID: 608 RVA: 0x0000B802 File Offset: 0x00009A02
		public string FrontEndServerName { get; set; }

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000261 RID: 609 RVA: 0x0000B80B File Offset: 0x00009A0B
		// (set) Token: 0x06000262 RID: 610 RVA: 0x0000B813 File Offset: 0x00009A13
		public string BackEndServerName { get; set; }

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000263 RID: 611 RVA: 0x0000B81C File Offset: 0x00009A1C
		// (set) Token: 0x06000264 RID: 612 RVA: 0x0000B824 File Offset: 0x00009A24
		public Uri Url { get; private set; }

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000265 RID: 613 RVA: 0x0000B82D File Offset: 0x00009A2D
		// (set) Token: 0x06000266 RID: 614 RVA: 0x0000B835 File Offset: 0x00009A35
		public Uri AutoDiscoverRedirectUri { get; private set; }

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000267 RID: 615 RVA: 0x0000B83E File Offset: 0x00009A3E
		// (set) Token: 0x06000268 RID: 616 RVA: 0x0000B846 File Offset: 0x00009A46
		public WebServiceUri WebServiceUri { get; private set; }

		// Token: 0x06000269 RID: 617 RVA: 0x0000B84F File Offset: 0x00009A4F
		public AutoDiscoverRequestResult(Uri url, string redirectAddress, Uri autoDiscoverRedirectUri, WebServiceUri webServiceUri, string frontEndServerName, string backEnderServerName)
		{
			this.Url = url;
			this.RedirectAddress = redirectAddress;
			this.AutoDiscoverRedirectUri = autoDiscoverRedirectUri;
			this.WebServiceUri = webServiceUri;
			this.FrontEndServerName = frontEndServerName;
			this.BackEndServerName = backEnderServerName;
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0000B884 File Offset: 0x00009A84
		public AutoDiscoverRequestResult(Uri url, Exception exception, string frontEndServerName, string backEnderServerName)
		{
			this.Url = url;
			this.Exception = exception;
			this.FrontEndServerName = frontEndServerName;
			this.BackEndServerName = backEnderServerName;
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0000B8AC File Offset: 0x00009AAC
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(512);
			if (this.RedirectAddress != null)
			{
				stringBuilder.Append("RedirectAddress=");
				stringBuilder.AppendLine(this.RedirectAddress);
			}
			if (this.AutoDiscoverRedirectUri != null)
			{
				stringBuilder.Append("AutoDiscoverRedirectUri=");
				stringBuilder.AppendLine(this.AutoDiscoverRedirectUri.ToString());
			}
			if (this.WebServiceUri != null)
			{
				stringBuilder.Append("WebServiceUri=");
				stringBuilder.AppendLine(this.WebServiceUri.ToString());
			}
			if (this.Exception != null)
			{
				stringBuilder.Append("Exception=");
				stringBuilder.AppendLine(this.Exception.ToString());
			}
			return stringBuilder.ToString();
		}
	}
}
