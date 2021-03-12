using System;
using System.IO;
using System.Net;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Monitoring
{
	// Token: 0x0200002E RID: 46
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class VerifyRpcProxyResult : CallResult
	{
		// Token: 0x06000131 RID: 305 RVA: 0x00005124 File Offset: 0x00003324
		public VerifyRpcProxyResult()
		{
			this.RequestedRpcProxyAuthenticationTypes = Array<string>.Empty;
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000132 RID: 306 RVA: 0x00005138 File Offset: 0x00003338
		public override bool IsSuccessful
		{
			get
			{
				return this.ResponseStatusCode == HttpStatusCode.OK;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000133 RID: 307 RVA: 0x00005163 File Offset: 0x00003363
		// (set) Token: 0x06000134 RID: 308 RVA: 0x0000516B File Offset: 0x0000336B
		public WebHeaderCollection ResponseWebHeaderCollection { get; internal set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000135 RID: 309 RVA: 0x00005174 File Offset: 0x00003374
		// (set) Token: 0x06000136 RID: 310 RVA: 0x0000517C File Offset: 0x0000337C
		public HttpStatusCode? ResponseStatusCode { get; internal set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000137 RID: 311 RVA: 0x00005185 File Offset: 0x00003385
		// (set) Token: 0x06000138 RID: 312 RVA: 0x0000518D File Offset: 0x0000338D
		public string RpcProxyUrl { get; internal set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000139 RID: 313 RVA: 0x00005196 File Offset: 0x00003396
		// (set) Token: 0x0600013A RID: 314 RVA: 0x0000519E File Offset: 0x0000339E
		public CertificateValidationError ServerCertificateValidationError { get; internal set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600013B RID: 315 RVA: 0x000051A7 File Offset: 0x000033A7
		// (set) Token: 0x0600013C RID: 316 RVA: 0x000051AF File Offset: 0x000033AF
		public string[] RequestedRpcProxyAuthenticationTypes { get; internal set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600013D RID: 317 RVA: 0x000051B8 File Offset: 0x000033B8
		// (set) Token: 0x0600013E RID: 318 RVA: 0x000051C0 File Offset: 0x000033C0
		public string ResponseStatusDescription { get; internal set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600013F RID: 319 RVA: 0x000051C9 File Offset: 0x000033C9
		// (set) Token: 0x06000140 RID: 320 RVA: 0x000051D1 File Offset: 0x000033D1
		public string ResponseBody { get; internal set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000141 RID: 321 RVA: 0x000051DA File Offset: 0x000033DA
		// (set) Token: 0x06000142 RID: 322 RVA: 0x000051E2 File Offset: 0x000033E2
		public WebException Exception { get; internal set; }

		// Token: 0x06000143 RID: 323 RVA: 0x000051EC File Offset: 0x000033EC
		public override void Validate()
		{
			ExAssert.RetailAssert(this.RequestedRpcProxyAuthenticationTypes != null, "RequestedRpcProxyAuthenticationTypes shouldn't be null");
			ExAssert.RetailAssert(this.IsSuccessful == (this.Exception == null) && (!this.IsSuccessful || this.ResponseWebHeaderCollection != null), string.Format("Inconclusive data: status code={0}, exception is null={1}, headers is null={2}", this.ResponseStatusCode, this.Exception == null, this.ResponseWebHeaderCollection == null));
			base.Validate();
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00005278 File Offset: 0x00003478
		internal static VerifyRpcProxyResult CreateSuccessfulResult(HttpWebResponse response)
		{
			VerifyRpcProxyResult verifyRpcProxyResult = new VerifyRpcProxyResult();
			verifyRpcProxyResult.ApplyHttpWebResponse(response);
			return verifyRpcProxyResult;
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00005294 File Offset: 0x00003494
		internal static VerifyRpcProxyResult CreateFailureResult(HttpWebResponse response, WebException webException)
		{
			VerifyRpcProxyResult verifyRpcProxyResult = new VerifyRpcProxyResult();
			if (response != null)
			{
				verifyRpcProxyResult.ApplyHttpWebResponse(response);
			}
			verifyRpcProxyResult.ApplyWebException(webException);
			return verifyRpcProxyResult;
		}

		// Token: 0x06000146 RID: 326 RVA: 0x000052B9 File Offset: 0x000034B9
		private void ApplyWebException(WebException webException)
		{
			this.Exception = webException;
		}

		// Token: 0x06000147 RID: 327 RVA: 0x000052C4 File Offset: 0x000034C4
		private void ApplyHttpWebResponse(HttpWebResponse response)
		{
			Util.ThrowOnNullArgument(response, "response");
			using (StreamReader streamReader = new StreamReader(response.GetResponseStream()))
			{
				this.ResponseBody = streamReader.ReadToEnd();
			}
			this.ResponseStatusCode = new HttpStatusCode?(response.StatusCode);
			this.ResponseStatusDescription = response.StatusDescription;
			this.ResponseWebHeaderCollection = response.Headers;
		}
	}
}
