using System;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Web;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.InfoWorker.Common.Availability;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020000E8 RID: 232
	internal sealed class ChunkedHttpResponse
	{
		// Token: 0x060007C6 RID: 1990 RVA: 0x0003AC7C File Offset: 0x00038E7C
		internal ChunkedHttpResponse(HttpContext context)
		{
			this.Response = context.Response;
			Utilities.MakePageNoCacheNoStore(this.Response);
			this.browserType = Utilities.GetBrowserType(context.Request.UserAgent);
			this.Response.BufferOutput = false;
			this.Response.Buffer = false;
			this.Response.ContentType = "text/html; charset=UTF-8";
			this.Response.AddHeader("Transfer-Encoding", "chunked");
			this.streamWriter = Utilities.CreateStreamWriter(this.Response.OutputStream);
			this.ChunkWrite(((this.browserType == BrowserType.IE) ? "<script>try{{document.domain=document.domain;}}catch(e){{}}</script>" : string.Empty) + "012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678 01234 567 89012 345\r\n" + ((this.browserType == BrowserType.Chrome) ? "012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678 01234 567 89012 345\r\n" : string.Empty));
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x060007C7 RID: 1991 RVA: 0x0003AD49 File Offset: 0x00038F49
		// (set) Token: 0x060007C8 RID: 1992 RVA: 0x0003AD51 File Offset: 0x00038F51
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

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x060007C9 RID: 1993 RVA: 0x0003AD5A File Offset: 0x00038F5A
		public bool IsClientConnected
		{
			get
			{
				return this.response.IsClientConnected;
			}
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x0003AD68 File Offset: 0x00038F68
		public void WriteIsRequestAlive(bool isAlive)
		{
			this.Write(string.Format(CultureInfo.InvariantCulture, "a_oPndGt.pndGtMgr.fRqAlive = {0};", new object[]
			{
				isAlive ? "1" : "0"
			}));
			if (isAlive)
			{
				this.Write("a_oPndGt.pndGtMgr.fErrLstPndGt = 0;");
			}
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x0003ADB2 File Offset: 0x00038FB2
		public void WriteReInitializeOWA()
		{
			this.Write("Owa.Utility.ReInitializeOWA();");
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x0003ADC0 File Offset: 0x00038FC0
		public void WriteError(string s)
		{
			this.ChunkWrite(string.Format(CultureInfo.InvariantCulture, (this.browserType == BrowserType.IE) ? "<script>var y=parent;if(y){{var x=y.pdnRsp;if(x) x(\"{0}\");}}</script>\r\n" : "<script>window.evlRsp(\"{0}\");</script>\r\n", new object[]
			{
				Utilities.JavascriptEncode("a_oPndGt.pndGtMgr.errorDiv='" + s + "';", true)
			}));
			this.Write("a_oPndGt.pndGtMgr.hndErrPndGt(1);");
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x0003AE20 File Offset: 0x00039020
		public void Write(string notificationString)
		{
			if (notificationString == null)
			{
				throw new ArgumentNullException();
			}
			this.ChunkWrite(string.Format(CultureInfo.InvariantCulture, (this.browserType == BrowserType.IE) ? "<script>var y=parent;if(y){{var x=y.pdnRsp;if(x) x(\"{0}\");}}</script>\r\n" : "<script>window.evlRsp(\"{0}\");</script>\r\n", new object[]
			{
				Utilities.JavascriptEncode(notificationString, true)
			}));
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x0003AE70 File Offset: 0x00039070
		public void Log(RequestLogger requestLogger)
		{
			if (!this.hasEnded)
			{
				try
				{
					requestLogger.LogToResponse(this.response);
				}
				catch (Exception ex)
				{
					ExTraceGlobals.CoreTracer.TraceError((long)this.GetHashCode(), "Exception caught while logging. Log data:{0}{1}. Exception:{2}{3}", new object[]
					{
						Environment.NewLine,
						requestLogger.LogData,
						Environment.NewLine,
						ex
					});
				}
			}
		}

		// Token: 0x060007CF RID: 1999 RVA: 0x0003AEE4 File Offset: 0x000390E4
		internal void WriteEmptyNotification()
		{
			this.Write("a_oPndGt.pndGtMgr.updTmStmp();");
		}

		// Token: 0x060007D0 RID: 2000 RVA: 0x0003AEF4 File Offset: 0x000390F4
		internal void WriteLastChunk()
		{
			try
			{
				this.ChunkWrite(string.Empty);
			}
			catch (OwaNotificationPipeWriteException ex)
			{
				ExTraceGlobals.CoreTracer.TraceError<string>((long)this.GetHashCode(), "Exception when writing the last data chunk. Exception message:{0};", (ex.InnerException != null) ? ex.InnerException.Message : ex.Message);
			}
			this.hasEnded = true;
			this.streamWriter.Close();
			this.response.End();
		}

		// Token: 0x060007D1 RID: 2001 RVA: 0x0003AF70 File Offset: 0x00039170
		internal void RestartRequest()
		{
			this.Write("rstPndRq();");
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x0003AF80 File Offset: 0x00039180
		private void ChunkWrite(string s)
		{
			Exception ex = null;
			try
			{
				this.streamWriter.Write("{0:x}\r\n{1}\r\n", s.Length, s);
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

		// Token: 0x04000582 RID: 1410
		private const string IsRequestAliveFormat = "a_oPndGt.pndGtMgr.fRqAlive = {0};";

		// Token: 0x04000583 RID: 1411
		private const string ClearErrorFlag = "a_oPndGt.pndGtMgr.fErrLstPndGt = 0;";

		// Token: 0x04000584 RID: 1412
		private const string ChunkedStringFormat = "{0:x}\r\n{1}\r\n";

		// Token: 0x04000585 RID: 1413
		private const string ChunkedWrapperFormatIE = "<script>var y=parent;if(y){{var x=y.pdnRsp;if(x) x(\"{0}\");}}</script>\r\n";

		// Token: 0x04000586 RID: 1414
		private const string ChunkedWrapperFormat = "<script>window.evlRsp(\"{0}\");</script>\r\n";

		// Token: 0x04000587 RID: 1415
		private const string IEFirstChunkResponseFormat = "<script>try{{document.domain=document.domain;}}catch(e){{}}</script>";

		// Token: 0x04000588 RID: 1416
		private const string CommonFirstChunkResponseFormat = "012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678 01234 567 89012 345\r\n";

		// Token: 0x04000589 RID: 1417
		private const string RestartRequestScript = "rstPndRq();";

		// Token: 0x0400058A RID: 1418
		private const string EmptyNotificationScript = "a_oPndGt.pndGtMgr.updTmStmp();";

		// Token: 0x0400058B RID: 1419
		private const string HandleErrorScript = "a_oPndGt.pndGtMgr.hndErrPndGt(1);";

		// Token: 0x0400058C RID: 1420
		private const string HtmlContentType = "text/html; charset=UTF-8";

		// Token: 0x0400058D RID: 1421
		private HttpResponse response;

		// Token: 0x0400058E RID: 1422
		private StreamWriter streamWriter;

		// Token: 0x0400058F RID: 1423
		private BrowserType browserType;

		// Token: 0x04000590 RID: 1424
		private bool hasEnded;
	}
}
