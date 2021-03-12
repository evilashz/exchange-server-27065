using System;
using System.IO;
using System.Net;
using System.ServiceModel.Channels;
using System.Web;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000B88 RID: 2952
	internal class HttpResponseRenderer : BaseResponseRenderer
	{
		// Token: 0x060055EA RID: 21994 RVA: 0x00110EB4 File Offset: 0x0010F0B4
		public static HttpResponseRenderer Create(HttpStatusCode statusCode)
		{
			return new HttpResponseRenderer(statusCode, null, null);
		}

		// Token: 0x060055EB RID: 21995 RVA: 0x00110EBE File Offset: 0x0010F0BE
		public static HttpResponseRenderer Create(HttpStatusCode statusCode, string body)
		{
			return new HttpResponseRenderer(statusCode, body, null);
		}

		// Token: 0x060055EC RID: 21996 RVA: 0x00110EC8 File Offset: 0x0010F0C8
		public static HttpResponseRenderer CreateRedirect(string location)
		{
			return new HttpResponseRenderer(HttpStatusCode.Found, null, location);
		}

		// Token: 0x060055ED RID: 21997 RVA: 0x00110ED6 File Offset: 0x0010F0D6
		private HttpResponseRenderer(HttpStatusCode statusCode, string body, string location)
		{
			this.statusCode = statusCode;
			this.responseBody = body;
			this.location = location;
		}

		// Token: 0x060055EE RID: 21998 RVA: 0x00110EF3 File Offset: 0x0010F0F3
		internal override void Render(Message message, Stream stream)
		{
			this.Render(message, stream, HttpContext.Current.Response);
		}

		// Token: 0x060055EF RID: 21999 RVA: 0x00110F08 File Offset: 0x0010F108
		internal override void Render(Message message, Stream stream, HttpResponse response)
		{
			if (response.IsClientConnected)
			{
				response.StatusCode = (int)this.statusCode;
				if (this.statusCode == HttpStatusCode.Found)
				{
					response.Redirect(this.location, false);
				}
				if (string.IsNullOrEmpty(this.responseBody))
				{
					response.SuppressContent = true;
				}
				else
				{
					response.Write(this.responseBody);
				}
				response.Flush();
			}
		}

		// Token: 0x04002EDD RID: 11997
		private HttpStatusCode statusCode;

		// Token: 0x04002EDE RID: 11998
		private string responseBody;

		// Token: 0x04002EDF RID: 11999
		private string location;
	}
}
