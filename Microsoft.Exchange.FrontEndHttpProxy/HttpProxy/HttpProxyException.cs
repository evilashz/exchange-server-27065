using System;
using System.Net;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Diagnostics.Components.HttpProxy;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x02000044 RID: 68
	[Serializable]
	internal class HttpProxyException : Exception
	{
		// Token: 0x06000215 RID: 533 RVA: 0x0000BD44 File Offset: 0x00009F44
		public HttpProxyException(HttpStatusCode statusCode, HttpProxySubErrorCode errorCode, string message) : base(message)
		{
			this.statusCode = statusCode;
			this.errorCode = errorCode;
		}

		// Token: 0x06000216 RID: 534 RVA: 0x0000BD5B File Offset: 0x00009F5B
		public HttpProxyException(HttpStatusCode statusCode, HttpProxySubErrorCode errorCode, string message, Exception innerException) : base(message, innerException)
		{
			this.statusCode = statusCode;
			this.errorCode = errorCode;
		}

		// Token: 0x06000217 RID: 535 RVA: 0x0000BD74 File Offset: 0x00009F74
		protected HttpProxyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			if (info != null)
			{
				this.statusCode = (HttpStatusCode)info.GetValue("statusCode", typeof(int));
				this.errorCode = (HttpProxySubErrorCode)info.GetValue("errorCode", typeof(HttpProxySubErrorCode));
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000218 RID: 536 RVA: 0x0000BDCC File Offset: 0x00009FCC
		public HttpStatusCode StatusCode
		{
			get
			{
				return this.statusCode;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000219 RID: 537 RVA: 0x0000BDD4 File Offset: 0x00009FD4
		public HttpProxySubErrorCode ErrorCode
		{
			get
			{
				return this.errorCode;
			}
		}

		// Token: 0x0600021A RID: 538 RVA: 0x0000BDDC File Offset: 0x00009FDC
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			if (info != null)
			{
				info.AddValue("statusCode", this.StatusCode);
				info.AddValue("errorCode", this.ErrorCode);
			}
		}

		// Token: 0x04000115 RID: 277
		private readonly HttpStatusCode statusCode;

		// Token: 0x04000116 RID: 278
		private readonly HttpProxySubErrorCode errorCode;
	}
}
