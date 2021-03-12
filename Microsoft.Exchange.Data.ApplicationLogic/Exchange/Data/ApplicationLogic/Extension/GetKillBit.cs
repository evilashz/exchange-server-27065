using System;
using System.IO;
using System.Threading;
using System.Xml;
using System.Xml.Linq;

namespace Microsoft.Exchange.Data.ApplicationLogic.Extension
{
	// Token: 0x02000120 RID: 288
	internal sealed class GetKillBit : BaseAsyncOmexCommand
	{
		// Token: 0x06000BDE RID: 3038 RVA: 0x000308D4 File Offset: 0x0002EAD4
		public GetKillBit(OmexWebServiceUrlsCache urlsCache) : base(urlsCache, "GetKillBit")
		{
		}

		// Token: 0x06000BDF RID: 3039 RVA: 0x000308E4 File Offset: 0x0002EAE4
		public void Execute(GetKillBit.SuccessCallback successCallback, BaseAsyncCommand.FailureCallback failureCallback)
		{
			if (successCallback == null)
			{
				throw new ArgumentNullException("successCallback");
			}
			if (failureCallback == null)
			{
				throw new ArgumentNullException("failureCallback");
			}
			this.successCallback = successCallback;
			this.failureCallback = failureCallback;
			if (this.urlsCache.IsInitialized)
			{
				this.InternalExecute();
				return;
			}
			this.urlsCache.Initialize(new OmexWebServiceUrlsCache.InitializeCompletionCallback(this.UrlsCacheInitializationCompletionCallback));
		}

		// Token: 0x06000BE0 RID: 3040 RVA: 0x00030946 File Offset: 0x0002EB46
		private void UrlsCacheInitializationCompletionCallback(bool isInitialized)
		{
			if (isInitialized)
			{
				this.InternalExecute();
				return;
			}
			this.InternalFailureCallback(null, "UrlsCache initialization failed. Killbit method won't be called");
		}

		// Token: 0x06000BE1 RID: 3041 RVA: 0x00030960 File Offset: 0x0002EB60
		private void InternalExecute()
		{
			base.ResetRequestID();
			Uri uri = new Uri(this.urlsCache.KillbitUrl + string.Format("?rt=XML&corr={0}", this.requestId));
			base.InternalExecute(uri);
		}

		// Token: 0x06000BE2 RID: 3042 RVA: 0x000309A0 File Offset: 0x0002EBA0
		protected override void ParseResponse(byte[] responseBuffer, int responseBufferSize)
		{
			XDocument xdocument = null;
			int num = 0;
			IOException ex;
			using (MemoryStream memoryStream = new MemoryStream(responseBuffer, 0, responseBufferSize))
			{
				try
				{
					ex = null;
					xdocument = XDocument.Load(memoryStream);
					xdocument.Save(KillBitHelper.KillBitFilePath);
				}
				catch (DirectoryNotFoundException exception)
				{
					this.InternalFailureCallback(exception, null);
					return;
				}
				catch (IOException ex2)
				{
					BaseAsyncCommand.Tracer.TraceWarning(0L, ex2.Message);
					ex = ex2;
					num++;
					Thread.Sleep(50);
				}
				catch (XmlException exception2)
				{
					this.InternalFailureCallback(exception2, null);
					return;
				}
				goto IL_B4;
			}
			try
			{
				IL_71:
				ex = null;
				xdocument.Save(KillBitHelper.KillBitFilePath);
			}
			catch (IOException ex3)
			{
				BaseAsyncCommand.Tracer.TraceWarning(0L, ex3.Message);
				ex = ex3;
				if (num >= 3)
				{
					this.InternalFailureCallback(ex3, null);
					return;
				}
				num++;
				Thread.Sleep(50);
			}
			IL_B4:
			if (ex != null && xdocument != null)
			{
				goto IL_71;
			}
			base.LogResponseParsed();
			this.successCallback();
		}

		// Token: 0x04000610 RID: 1552
		private const string KillbitQueryStringFormat = "?rt=XML&corr={0}";

		// Token: 0x04000611 RID: 1553
		private const int MaxRetryCount = 3;

		// Token: 0x04000612 RID: 1554
		private const int RetryTimeInterval = 50;

		// Token: 0x04000613 RID: 1555
		private GetKillBit.SuccessCallback successCallback;

		// Token: 0x02000121 RID: 289
		// (Invoke) Token: 0x06000BE4 RID: 3044
		internal delegate void SuccessCallback();
	}
}
