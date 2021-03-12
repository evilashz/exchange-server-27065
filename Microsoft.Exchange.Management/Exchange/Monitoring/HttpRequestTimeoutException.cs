using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x0200110B RID: 4363
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class HttpRequestTimeoutException : LocalizedException
	{
		// Token: 0x0600B428 RID: 46120 RVA: 0x0029C621 File Offset: 0x0029A821
		public HttpRequestTimeoutException(string httpRequest, string statusCode) : base(Strings.messageHttpRequestTimeoutException(httpRequest, statusCode))
		{
			this.httpRequest = httpRequest;
			this.statusCode = statusCode;
		}

		// Token: 0x0600B429 RID: 46121 RVA: 0x0029C63E File Offset: 0x0029A83E
		public HttpRequestTimeoutException(string httpRequest, string statusCode, Exception innerException) : base(Strings.messageHttpRequestTimeoutException(httpRequest, statusCode), innerException)
		{
			this.httpRequest = httpRequest;
			this.statusCode = statusCode;
		}

		// Token: 0x0600B42A RID: 46122 RVA: 0x0029C65C File Offset: 0x0029A85C
		protected HttpRequestTimeoutException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.httpRequest = (string)info.GetValue("httpRequest", typeof(string));
			this.statusCode = (string)info.GetValue("statusCode", typeof(string));
		}

		// Token: 0x0600B42B RID: 46123 RVA: 0x0029C6B1 File Offset: 0x0029A8B1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("httpRequest", this.httpRequest);
			info.AddValue("statusCode", this.statusCode);
		}

		// Token: 0x17003919 RID: 14617
		// (get) Token: 0x0600B42C RID: 46124 RVA: 0x0029C6DD File Offset: 0x0029A8DD
		public string HttpRequest
		{
			get
			{
				return this.httpRequest;
			}
		}

		// Token: 0x1700391A RID: 14618
		// (get) Token: 0x0600B42D RID: 46125 RVA: 0x0029C6E5 File Offset: 0x0029A8E5
		public string StatusCode
		{
			get
			{
				return this.statusCode;
			}
		}

		// Token: 0x0400627F RID: 25215
		private readonly string httpRequest;

		// Token: 0x04006280 RID: 25216
		private readonly string statusCode;
	}
}
