using System;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Web;
using Microsoft.Exchange.Clients.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.Security.Authentication;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200016C RID: 364
	internal sealed class ChunkedHttpResponse : DisposeTrackableBase
	{
		// Token: 0x06000D6B RID: 3435 RVA: 0x00032D30 File Offset: 0x00030F30
		internal ChunkedHttpResponse(HttpContext context)
		{
			this.Response = context.Response;
			HttpUtilities.MakePageNoCacheNoStore(this.Response);
			this.evalChunksNotSupportedByXmlhttpRequest = (HttpUtilities.GetQueryStringParameter(context.Request, "ecnsq", false) == "1");
			this.browserNameQueryParamValue = HttpUtilities.GetQueryStringParameter(context.Request, "brwnm", false);
			this.userAgent = new UserAgent(context.Request.UserAgent, UserContextManager.GetUserContext(context).FeaturesManager.ClientServerSettings.ChangeLayout.Enabled, context.Request.Cookies);
			this.accountValidationContext = (context.Items["AccountValidationContext"] as IAccountValidationContext);
			this.Response.BufferOutput = false;
			this.Response.Buffer = false;
			this.Response.ContentType = "text/html; charset=UTF-8";
			this.Response.AddHeader("Transfer-Encoding", "chunked");
			if ((string.Equals("iPhone", this.userAgent.Platform) || string.Equals("iPad", this.userAgent.Platform)) && ((this.userAgent.Browser == "Safari" && this.userAgent.BrowserVersion.Build > 5) || this.browserNameQueryParamValue == "safari"))
			{
				this.Response.AddHeader("X-FromBackEnd-ClientConnection", "close");
			}
			if (!this.evalChunksNotSupportedByXmlhttpRequest)
			{
				this.Response.TrySkipIisCustomErrors = true;
			}
			this.streamWriter = PendingRequestUtilities.CreateStreamWriter(this.Response.OutputStream);
			this.WriteFirstChunk();
		}

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06000D6C RID: 3436 RVA: 0x00032ED7 File Offset: 0x000310D7
		public IAccountValidationContext AccountValidationContext
		{
			get
			{
				return this.accountValidationContext;
			}
		}

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06000D6D RID: 3437 RVA: 0x00032EDF File Offset: 0x000310DF
		public bool IsClientConnected
		{
			get
			{
				return this.response.IsClientConnected;
			}
		}

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06000D6E RID: 3438 RVA: 0x00032EEC File Offset: 0x000310EC
		// (set) Token: 0x06000D6F RID: 3439 RVA: 0x00032EF4 File Offset: 0x000310F4
		private HttpResponse Response
		{
			get
			{
				return this.response;
			}
			set
			{
				this.response = value;
			}
		}

		// Token: 0x06000D70 RID: 3440 RVA: 0x00032EFD File Offset: 0x000310FD
		public void WriteIsRequestAlive(bool isAlive, long notificationMark)
		{
			if (isAlive)
			{
				this.Write(string.Format("{{id:'pg',data:'alive1',mark:'{0}'}}", notificationMark));
				this.WritePendingGetNotification("noerr");
				return;
			}
			this.WritePendingGetNotification("alive0");
		}

		// Token: 0x06000D71 RID: 3441 RVA: 0x00032F2F File Offset: 0x0003112F
		public void WriteReinitializeSubscriptions()
		{
			this.WritePendingGetNotification("reinitSubscription");
		}

		// Token: 0x06000D72 RID: 3442 RVA: 0x00032F3C File Offset: 0x0003113C
		public void WritePendingGeMark(long notificationMark)
		{
			this.Write(string.Format("{{id:'pg',data:'update',mark:'{0}'}}", notificationMark));
		}

		// Token: 0x06000D73 RID: 3443 RVA: 0x00032F54 File Offset: 0x00031154
		public void WriteError(string s)
		{
			this.Write(string.Format("{{id:'pg',data:'err',ex:'{0}'}}", s));
		}

		// Token: 0x06000D74 RID: 3444 RVA: 0x00032F68 File Offset: 0x00031168
		public void Write(string notificationString)
		{
			if (notificationString == null)
			{
				throw new ArgumentNullException();
			}
			if (this.evalChunksNotSupportedByXmlhttpRequest)
			{
				this.ChunkWrite(string.Format(CultureInfo.InvariantCulture, "<script>var y=parent;if(y){{var x=y.pR;if(x) x(\"{0}\");}}</script>\r\n", new object[]
				{
					PendingRequestUtilities.JavascriptEncode(notificationString)
				}), NotificationChunkOrder.Mid);
				return;
			}
			this.ChunkWrite(string.Format(CultureInfo.InvariantCulture, "<script>{0}</script>\r\n", new object[]
			{
				PendingRequestUtilities.JavascriptEscapeNonAscii(notificationString)
			}), NotificationChunkOrder.Mid);
		}

		// Token: 0x06000D75 RID: 3445 RVA: 0x00032FD5 File Offset: 0x000311D5
		internal void WriteEmptyNotification()
		{
			this.WritePendingGetNotification("update");
		}

		// Token: 0x06000D76 RID: 3446 RVA: 0x00032FE4 File Offset: 0x000311E4
		internal void WriteLastChunk()
		{
			try
			{
				this.ChunkWrite(string.Empty, NotificationChunkOrder.Last);
			}
			catch (OwaNotificationPipeWriteException ex)
			{
				ExTraceGlobals.CoreTracer.TraceError<string>((long)this.GetHashCode(), "Exception when writing the last data chunk. Exception message:{0};", (ex.InnerException != null) ? ex.InnerException.Message : ex.Message);
			}
			this.streamWriter.Close();
			this.response.End();
		}

		// Token: 0x06000D77 RID: 3447 RVA: 0x0003305C File Offset: 0x0003125C
		internal void RestartRequest()
		{
			this.WritePendingGetNotification("restart");
		}

		// Token: 0x06000D78 RID: 3448 RVA: 0x00033069 File Offset: 0x00031269
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<ChunkedHttpResponse>(this);
		}

		// Token: 0x06000D79 RID: 3449 RVA: 0x00033071 File Offset: 0x00031271
		protected override void InternalDispose(bool isDisposing)
		{
			if (!this.disposed)
			{
				if (isDisposing && this.streamWriter != null)
				{
					this.streamWriter.Dispose();
				}
				this.disposed = true;
			}
		}

		// Token: 0x06000D7A RID: 3450 RVA: 0x00033098 File Offset: 0x00031298
		private void WritePendingGetNotification(string notification)
		{
			this.Write(string.Format("{{id:'pg',data:'{0}'}}", notification));
		}

		// Token: 0x06000D7B RID: 3451 RVA: 0x000330AB File Offset: 0x000312AB
		private void WriteFirstChunk()
		{
			this.ChunkWrite("012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678 01234 567 89012 345\r\n" + ((this.browserNameQueryParamValue == "chrome") ? "012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678 01234 567 89012 345\r\n" : string.Empty), NotificationChunkOrder.First);
		}

		// Token: 0x06000D7C RID: 3452 RVA: 0x000330DC File Offset: 0x000312DC
		private void ChunkWrite(string s, NotificationChunkOrder order)
		{
			Exception ex = null;
			try
			{
				if (order == NotificationChunkOrder.First)
				{
					this.streamWriter.Write("{0:x}\r\n{1}{2}", s.Length + "<script></script>\r\n<script></script>\r\n".Length, s, "<script></script>\r\n");
				}
				else if (order == NotificationChunkOrder.Last)
				{
					this.streamWriter.Write("{0}\r\n{1:x}\r\n{2}\r\n", "<script></script>\r\n", s.Length, s);
				}
				else
				{
					this.streamWriter.Write("{0}\r\n{1:x}\r\n{2}{3}", new object[]
					{
						"<script></script>\r\n",
						s.Length + "<script></script>\r\n<script></script>\r\n".Length,
						s,
						"<script></script>\r\n"
					});
				}
				this.streamWriter.Flush();
			}
			catch (ObjectDisposedException ex2)
			{
				ex = ex2;
			}
			catch (IOException ex3)
			{
				ex = ex3;
			}
			catch (HttpException ex4)
			{
				ex = ex4;
			}
			catch (COMException ex5)
			{
				ex = ex5;
			}
			catch (ArgumentException ex6)
			{
				ex = ex6;
			}
			finally
			{
				if (ex != null)
				{
					throw new OwaNotificationPipeWriteException("Some exception was raised while trying to write in the response", ex);
				}
			}
		}

		// Token: 0x04000816 RID: 2070
		internal const string EvalChunkNotSupportedQueryParamName = "ecnsq";

		// Token: 0x04000817 RID: 2071
		internal const string BrowserNameQueryParam = "brwnm";

		// Token: 0x04000818 RID: 2072
		private const string EvalChunkNotSupportedQueryParamValue = "1";

		// Token: 0x04000819 RID: 2073
		private const string SafariBrowserNameQueryParamValue = "safari";

		// Token: 0x0400081A RID: 2074
		private const string ChromeBrowserNameQueryParamValue = "chrome";

		// Token: 0x0400081B RID: 2075
		private const string PendingGetFormat = "{{id:'pg',data:'{0}'}}";

		// Token: 0x0400081C RID: 2076
		private const string PendingGetErrorFormat = "{{id:'pg',data:'err',ex:'{0}'}}";

		// Token: 0x0400081D RID: 2077
		private const string PendingGetMarkFormat = "{{id:'pg',data:'update',mark:'{0}'}}";

		// Token: 0x0400081E RID: 2078
		private const string IsRequestAliveFormat = "{{id:'pg',data:'alive1',mark:'{0}'}}";

		// Token: 0x0400081F RID: 2079
		private const string IsRequestNotAliveFormat = "alive0";

		// Token: 0x04000820 RID: 2080
		private const string ReinitializeSubscription = "reinitSubscription";

		// Token: 0x04000821 RID: 2081
		private const string ClearErrorFlag = "noerr";

		// Token: 0x04000822 RID: 2082
		private const string ChunkedStringFormat = "{0:x}\r\n{1}\r\n";

		// Token: 0x04000823 RID: 2083
		private const string ChunkedWrapperFormatIE = "<script>var y=parent;if(y){{var x=y.pR;if(x) x(\"{0}\");}}</script>\r\n";

		// Token: 0x04000824 RID: 2084
		private const string ChunkedWrapperFormat = "<script>{0}</script>\r\n";

		// Token: 0x04000825 RID: 2085
		private const string CommonFirstChunkResponseFormat = "012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678 01234 567 89012 345\r\n";

		// Token: 0x04000826 RID: 2086
		private const string RestartRequestScript = "restart";

		// Token: 0x04000827 RID: 2087
		private const string EmptyNotificationScript = "update";

		// Token: 0x04000828 RID: 2088
		private const string HtmlContentType = "text/html; charset=UTF-8";

		// Token: 0x04000829 RID: 2089
		private const string FullDummyResponse = "<script></script>\r\n<script></script>\r\n";

		// Token: 0x0400082A RID: 2090
		private const string PartialDummyResponse = "<script></script>\r\n";

		// Token: 0x0400082B RID: 2091
		private const string PrefixDummyStringFormat = "{0}\r\n";

		// Token: 0x0400082C RID: 2092
		private const string PostfixDummyStringFormat = "{0:x}\r\n{1}";

		// Token: 0x0400082D RID: 2093
		private const string FirstChunkStringFormat = "{0:x}\r\n{1}{2}";

		// Token: 0x0400082E RID: 2094
		private const string MidChunkStringFormat = "{0}\r\n{1:x}\r\n{2}{3}";

		// Token: 0x0400082F RID: 2095
		private const string LastChunkStringFormat = "{0}\r\n{1:x}\r\n{2}\r\n";

		// Token: 0x04000830 RID: 2096
		private HttpResponse response;

		// Token: 0x04000831 RID: 2097
		private StreamWriter streamWriter;

		// Token: 0x04000832 RID: 2098
		private UserAgent userAgent;

		// Token: 0x04000833 RID: 2099
		private bool disposed;

		// Token: 0x04000834 RID: 2100
		private readonly string browserNameQueryParamValue;

		// Token: 0x04000835 RID: 2101
		private readonly bool evalChunksNotSupportedByXmlhttpRequest;

		// Token: 0x04000836 RID: 2102
		private readonly IAccountValidationContext accountValidationContext;
	}
}
