using System;
using System.IO;
using System.Net;
using System.Net.Cache;
using System.Threading;

namespace Microsoft.Exchange.Data.ApplicationLogic.Extension
{
	// Token: 0x02000116 RID: 278
	internal abstract class BaseAsyncOmexCommand : BaseAsyncCommand
	{
		// Token: 0x06000B95 RID: 2965 RVA: 0x0002F22E File Offset: 0x0002D42E
		public BaseAsyncOmexCommand(OmexWebServiceUrlsCache urlsCache, string scenario) : base(scenario)
		{
			this.urlsCache = urlsCache;
		}

		// Token: 0x06000B96 RID: 2966 RVA: 0x0002F240 File Offset: 0x0002D440
		private void TimeoutCallback(object state, bool timedOut)
		{
			if (timedOut)
			{
				BaseAsyncCommand.Tracer.TraceError<Uri>(0L, "BaseAsyncOmexCommand.TimeoutCallback: Request timed out: {0}", this.uri);
				try
				{
					if (this.request != null)
					{
						this.request.Abort();
					}
				}
				catch (WebException exception)
				{
					this.InternalFailureCallback(exception, null);
					return;
				}
				this.asyncResult = null;
			}
		}

		// Token: 0x06000B97 RID: 2967 RVA: 0x0002F2A0 File Offset: 0x0002D4A0
		protected void InternalExecute(Uri uri)
		{
			this.uri = uri;
			this.request = null;
			try
			{
				this.request = (HttpWebRequest)WebRequest.Create(uri);
				this.request.CachePolicy = BaseAsyncOmexCommand.NoCachePolicy;
				this.request.Method = "GET";
				this.PrepareRequest(this.request);
				this.asyncResult = this.request.BeginGetResponse(new AsyncCallback(this.EndGetResponseCallback), null);
				ThreadPool.RegisterWaitForSingleObject(this.asyncResult.AsyncWaitHandle, new WaitOrTimerCallback(this.TimeoutCallback), null, 60000, true);
			}
			catch (WebException exception)
			{
				this.InternalFailureCallback(exception, null);
			}
		}

		// Token: 0x06000B98 RID: 2968 RVA: 0x0002F358 File Offset: 0x0002D558
		protected virtual void PrepareRequest(HttpWebRequest request)
		{
		}

		// Token: 0x06000B99 RID: 2969 RVA: 0x0002F35A File Offset: 0x0002D55A
		protected virtual void ProcessResponse(HttpWebResponse response)
		{
		}

		// Token: 0x06000B9A RID: 2970 RVA: 0x0002F35C File Offset: 0x0002D55C
		protected virtual long GetMaxResponseBufferSize()
		{
			return 32768L;
		}

		// Token: 0x06000B9B RID: 2971 RVA: 0x0002F424 File Offset: 0x0002D624
		private void EndGetResponseCallback(IAsyncResult asyncResult)
		{
			lock (this.request)
			{
				if (this.requestFailedDueToMisMatchAsyncResult)
				{
					BaseAsyncCommand.Tracer.TraceError(0L, "EndGetResponseCallback called after request already failed. Ignoring callback.");
					return;
				}
				if (this.asyncResult != asyncResult && !asyncResult.CompletedSynchronously)
				{
					this.requestFailedDueToMisMatchAsyncResult = true;
				}
			}
			if (!this.requestFailedDueToMisMatchAsyncResult)
			{
				base.ExecuteWithExceptionHandler(delegate
				{
					try
					{
						HttpWebResponse httpWebResponse = this.request.EndGetResponse(asyncResult) as HttpWebResponse;
						this.ProcessResponse(httpWebResponse);
						this.responseStream = httpWebResponse.GetResponseStream();
						this.responseBuffer = new byte[this.GetMaxResponseBufferSize()];
						this.responseBufferIndex = 0;
						this.BeginResponseStreamRead();
					}
					catch (WebException exception)
					{
						this.InternalFailureCallback(exception, null);
					}
					finally
					{
						this.asyncResult = null;
					}
				});
				return;
			}
			this.InternalFailureCallback(null, "asyncResult in callback does not match the asyncResult from BeginGetResponse. ignoring " + this.uri);
		}

		// Token: 0x06000B9C RID: 2972 RVA: 0x0002F4E4 File Offset: 0x0002D6E4
		private void BeginResponseStreamRead()
		{
			try
			{
				if (this.responseBufferIndex >= this.responseBuffer.Length)
				{
					this.responseStream.Close();
					this.InternalFailureCallback(null, "BaseAsyncOmexCommand.BeginResponseStreamRead: Response buffer too small for request: " + this.uri);
					ExtensionDiagnostics.Logger.LogEvent(ApplicationLogicEventLogConstants.Tuple_ResponseExceedsBufferSize, this.uri.AbsolutePath, new object[]
					{
						this.scenario,
						this.requestId,
						base.GetLoggedMailboxIdentifier(),
						this.uri,
						this.GetMaxResponseBufferSize()
					});
				}
				else
				{
					this.responseStream.BeginRead(this.responseBuffer, this.responseBufferIndex, this.responseBuffer.Length - this.responseBufferIndex, new AsyncCallback(this.EndResponseStreamReadCallback), null);
				}
			}
			catch (IOException exception)
			{
				this.InternalFailureCallback(exception, null);
			}
			catch (WebException exception2)
			{
				this.InternalFailureCallback(exception2, null);
			}
		}

		// Token: 0x06000B9D RID: 2973 RVA: 0x0002F6A4 File Offset: 0x0002D8A4
		private void EndResponseStreamReadCallback(IAsyncResult asyncResult)
		{
			base.ExecuteWithExceptionHandler(delegate
			{
				try
				{
					int num = this.responseStream.EndRead(asyncResult);
					this.responseBufferIndex += num;
					if (num != 0)
					{
						this.BeginResponseStreamRead();
					}
					else
					{
						BaseAsyncCommand.Tracer.Information(0L, "The web service response was received.");
						this.ParseResponse(this.responseBuffer, this.responseBufferIndex);
					}
				}
				catch (IOException exception)
				{
					this.InternalFailureCallback(exception, null);
				}
				catch (WebException exception2)
				{
					this.InternalFailureCallback(exception2, null);
				}
			});
		}

		// Token: 0x06000B9E RID: 2974 RVA: 0x0002F6D8 File Offset: 0x0002D8D8
		protected void LogResponseParsed()
		{
			ExtensionDiagnostics.LogToDatacenterOnly(ApplicationLogicEventLogConstants.Tuple_OmexWebServiceResponseParsed, null, new object[]
			{
				this.scenario,
				this.requestId,
				base.GetLoggedMailboxIdentifier(),
				this.uri
			});
		}

		// Token: 0x06000B9F RID: 2975
		protected abstract void ParseResponse(byte[] responseBuffer, int responseBufferSize);

		// Token: 0x040005DB RID: 1499
		private const int RequestTimeout = 60000;

		// Token: 0x040005DC RID: 1500
		private const string GetRequestMethod = "GET";

		// Token: 0x040005DD RID: 1501
		private const long defaultMaxBufferSize = 32768L;

		// Token: 0x040005DE RID: 1502
		private static HttpRequestCachePolicy NoCachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);

		// Token: 0x040005DF RID: 1503
		private HttpWebRequest request;

		// Token: 0x040005E0 RID: 1504
		private IAsyncResult asyncResult;

		// Token: 0x040005E1 RID: 1505
		private bool requestFailedDueToMisMatchAsyncResult;

		// Token: 0x040005E2 RID: 1506
		private Stream responseStream;

		// Token: 0x040005E3 RID: 1507
		private byte[] responseBuffer;

		// Token: 0x040005E4 RID: 1508
		private int responseBufferIndex;

		// Token: 0x040005E5 RID: 1509
		protected OmexWebServiceUrlsCache urlsCache;
	}
}
