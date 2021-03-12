using System;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.SoapWebClient.AutoDiscover;

namespace Microsoft.Exchange.SoapWebClient
{
	// Token: 0x020006C3 RID: 1731
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class AutodiscoverResultData
	{
		// Token: 0x1700086F RID: 2159
		// (get) Token: 0x06002050 RID: 8272 RVA: 0x0003F7AC File Offset: 0x0003D9AC
		// (set) Token: 0x06002051 RID: 8273 RVA: 0x0003F7B4 File Offset: 0x0003D9B4
		public AutodiscoverResult Type { get; internal set; }

		// Token: 0x17000870 RID: 2160
		// (get) Token: 0x06002052 RID: 8274 RVA: 0x0003F7BD File Offset: 0x0003D9BD
		// (set) Token: 0x06002053 RID: 8275 RVA: 0x0003F7C5 File Offset: 0x0003D9C5
		public Uri Url { get; internal set; }

		// Token: 0x17000871 RID: 2161
		// (get) Token: 0x06002054 RID: 8276 RVA: 0x0003F7CE File Offset: 0x0003D9CE
		// (set) Token: 0x06002055 RID: 8277 RVA: 0x0003F7D6 File Offset: 0x0003D9D6
		public Exception Exception { get; internal set; }

		// Token: 0x17000872 RID: 2162
		// (get) Token: 0x06002056 RID: 8278 RVA: 0x0003F7DF File Offset: 0x0003D9DF
		// (set) Token: 0x06002057 RID: 8279 RVA: 0x0003F7E7 File Offset: 0x0003D9E7
		public Uri RedirectUrl { get; internal set; }

		// Token: 0x17000873 RID: 2163
		// (get) Token: 0x06002058 RID: 8280 RVA: 0x0003F7F0 File Offset: 0x0003D9F0
		// (set) Token: 0x06002059 RID: 8281 RVA: 0x0003F7F8 File Offset: 0x0003D9F8
		public StringList SslCertificateHostnames { get; internal set; }

		// Token: 0x17000874 RID: 2164
		// (get) Token: 0x0600205A RID: 8282 RVA: 0x0003F801 File Offset: 0x0003DA01
		// (set) Token: 0x0600205B RID: 8283 RVA: 0x0003F809 File Offset: 0x0003DA09
		public AutodiscoverResponse Response { get; internal set; }

		// Token: 0x17000875 RID: 2165
		// (get) Token: 0x0600205C RID: 8284 RVA: 0x0003F812 File Offset: 0x0003DA12
		// (set) Token: 0x0600205D RID: 8285 RVA: 0x0003F81A File Offset: 0x0003DA1A
		public AutodiscoverResultData Alternate { get; internal set; }

		// Token: 0x0600205E RID: 8286 RVA: 0x0003F824 File Offset: 0x0003DA24
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(400);
			stringBuilder.Append("Type=" + this.Type + ";");
			stringBuilder.Append("Url=" + this.Url + ";");
			if (this.Exception != null)
			{
				stringBuilder.Append("Exception=" + this.Exception.Message + ";");
			}
			if (this.RedirectUrl != null)
			{
				stringBuilder.Append("RedirectUrl=" + this.RedirectUrl + ";");
			}
			if (this.SslCertificateHostnames != null)
			{
				stringBuilder.Append("SslCertificateHostnames=" + this.SslCertificateHostnames.ToString() + ";");
			}
			if (this.Alternate != null)
			{
				stringBuilder.Append("Alternate=(" + this.Alternate.ToString() + ");");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600205F RID: 8287 RVA: 0x0003F924 File Offset: 0x0003DB24
		internal AutodiscoverResultData()
		{
		}
	}
}
