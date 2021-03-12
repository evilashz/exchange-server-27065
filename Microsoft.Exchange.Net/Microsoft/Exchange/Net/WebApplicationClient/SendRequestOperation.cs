using System;
using System.IO;
using System.Net;

namespace Microsoft.Exchange.Net.WebApplicationClient
{
	// Token: 0x02000B27 RID: 2855
	internal sealed class SendRequestOperation : AsyncResult
	{
		// Token: 0x06003D9B RID: 15771 RVA: 0x000A06C0 File Offset: 0x0009E8C0
		public SendRequestOperation(HttpWebRequest request, RequestBody requestBody, AsyncCallback callback, object asyncState) : base(callback, asyncState)
		{
			this.Request = request;
			this.requestBody = requestBody;
			if (requestBody != null)
			{
				if (requestBody.ContentType != null)
				{
					this.Request.ContentType = requestBody.ContentType.ToString();
				}
				this.Request.BeginGetRequestStream(new AsyncCallback(this.WriteBody), null);
				return;
			}
			this.Request.BeginGetResponse(new AsyncCallback(this.ReadResponse), null);
		}

		// Token: 0x17000F3C RID: 3900
		// (get) Token: 0x06003D9C RID: 15772 RVA: 0x000A0738 File Offset: 0x0009E938
		// (set) Token: 0x06003D9D RID: 15773 RVA: 0x000A0740 File Offset: 0x0009E940
		public HttpWebRequest Request { get; private set; }

		// Token: 0x17000F3D RID: 3901
		// (get) Token: 0x06003D9E RID: 15774 RVA: 0x000A0749 File Offset: 0x0009E949
		// (set) Token: 0x06003D9F RID: 15775 RVA: 0x000A0751 File Offset: 0x0009E951
		public HttpWebResponse Response { get; private set; }

		// Token: 0x17000F3E RID: 3902
		// (get) Token: 0x06003DA0 RID: 15776 RVA: 0x000A075A File Offset: 0x0009E95A
		public long BytesSent
		{
			get
			{
				if (!base.IsCompleted)
				{
					throw new InvalidOperationException();
				}
				return this.Request.Headers.ToByteArray().LongLength + Math.Max(this.Request.ContentLength, 0L);
			}
		}

		// Token: 0x17000F3F RID: 3903
		// (get) Token: 0x06003DA1 RID: 15777 RVA: 0x000A0794 File Offset: 0x0009E994
		public long BytesReceived
		{
			get
			{
				if (!base.IsCompleted)
				{
					throw new InvalidOperationException();
				}
				if (this.Response == null)
				{
					return 0L;
				}
				return this.Response.Headers.ToByteArray().LongLength + Math.Max(this.Response.ContentLength, 0L);
			}
		}

		// Token: 0x06003DA2 RID: 15778 RVA: 0x000A07E4 File Offset: 0x0009E9E4
		private void WriteBody(IAsyncResult results)
		{
			try
			{
				using (Stream stream = this.Request.EndGetRequestStream(results))
				{
					this.requestBody.Write(stream);
				}
				this.Request.BeginGetResponse(new AsyncCallback(this.ReadResponse), null);
			}
			catch (Exception exception)
			{
				base.Complete(exception, false);
			}
		}

		// Token: 0x06003DA3 RID: 15779 RVA: 0x000A0858 File Offset: 0x0009EA58
		private void ReadResponse(IAsyncResult results)
		{
			try
			{
				this.Response = (HttpWebResponse)this.Request.EndGetResponse(results);
				if (!this.Request.HaveResponse || this.Response == null)
				{
					base.Complete(new WebException(NetException.NoResponseFromHttpServer, WebExceptionStatus.ReceiveFailure), false);
				}
				else
				{
					base.Complete(null, false);
				}
			}
			catch (WebException ex)
			{
				this.Response = (ex.Response as HttpWebResponse);
				base.Complete(ex, false);
			}
			catch (Exception exception)
			{
				base.Complete(exception, false);
			}
		}

		// Token: 0x040035A4 RID: 13732
		private RequestBody requestBody;
	}
}
