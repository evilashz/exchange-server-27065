using System;
using System.IO;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.HttpProxy;
using Microsoft.Exchange.HttpProxy.Common;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x02000074 RID: 116
	internal class StreamProxy : IDisposeTrackable, IDisposable
	{
		// Token: 0x06000386 RID: 902 RVA: 0x00014C45 File Offset: 0x00012E45
		public StreamProxy(StreamProxy.StreamProxyType streamProxyType, Stream source, Stream target, byte[] buffer, IRequestContext requestContext) : this(streamProxyType, source, target, requestContext)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			this.buffer = buffer;
		}

		// Token: 0x06000387 RID: 903 RVA: 0x00014C6C File Offset: 0x00012E6C
		public StreamProxy(StreamProxy.StreamProxyType streamProxyType, Stream source, Stream target, BufferPoolCollection.BufferSize maxBufferPoolSize, BufferPoolCollection.BufferSize minBufferPoolSize, IRequestContext requestContext) : this(streamProxyType, source, target, requestContext)
		{
			this.maxBufferPoolSize = maxBufferPoolSize;
			this.minBufferPoolSize = minBufferPoolSize;
			this.currentBufferPoolSize = minBufferPoolSize;
			this.currentBufferPool = BufferPoolCollection.AutoCleanupCollection.Acquire(this.currentBufferPoolSize);
			this.buffer = this.currentBufferPool.Acquire();
			this.previousBufferSize = this.buffer.Length;
		}

		// Token: 0x06000388 RID: 904 RVA: 0x00014CD4 File Offset: 0x00012ED4
		private StreamProxy(StreamProxy.StreamProxyType streamProxyType, Stream source, Stream target, IRequestContext requestContext)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			if (requestContext == null)
			{
				throw new ArgumentException("requestContext");
			}
			this.disposeTracker = this.GetDisposeTracker();
			this.isDisposed = false;
			this.proxyType = streamProxyType;
			this.sourceStream = source;
			this.targetStream = target;
			this.requestContext = requestContext;
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000389 RID: 905 RVA: 0x00014D4D File Offset: 0x00012F4D
		public StreamProxy.StreamProxyType ProxyType
		{
			get
			{
				this.CheckDispose();
				return this.proxyType;
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x0600038A RID: 906 RVA: 0x00014D5B File Offset: 0x00012F5B
		public StreamProxy.StreamProxyState StreamState
		{
			get
			{
				this.CheckDispose();
				return this.streamState;
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x0600038B RID: 907 RVA: 0x00014D69 File Offset: 0x00012F69
		public Stream SourceStream
		{
			get
			{
				this.CheckDispose();
				return this.sourceStream;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x0600038C RID: 908 RVA: 0x00014D77 File Offset: 0x00012F77
		public Stream TargetStream
		{
			get
			{
				this.CheckDispose();
				return this.targetStream;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x0600038D RID: 909 RVA: 0x00014D85 File Offset: 0x00012F85
		public IRequestContext RequestContext
		{
			get
			{
				this.CheckDispose();
				return this.requestContext;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x0600038E RID: 910 RVA: 0x00014D93 File Offset: 0x00012F93
		// (set) Token: 0x0600038F RID: 911 RVA: 0x00014DA1 File Offset: 0x00012FA1
		public Stream AuxTargetStream
		{
			get
			{
				this.CheckDispose();
				return this.auxTargetStream;
			}
			set
			{
				this.CheckDispose();
				this.auxTargetStream = value;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000390 RID: 912 RVA: 0x00014DB0 File Offset: 0x00012FB0
		public long TotalBytesProxied
		{
			get
			{
				this.CheckDispose();
				return this.totalBytesProxied;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000391 RID: 913 RVA: 0x00014DBE File Offset: 0x00012FBE
		public long NumberOfReadsCompleted
		{
			get
			{
				this.CheckDispose();
				return this.numberOfReadsCompleted;
			}
		}

		// Token: 0x06000392 RID: 914 RVA: 0x00014DCC File Offset: 0x00012FCC
		public IAsyncResult BeginProcess(AsyncCallback asyncCallback, object asyncState)
		{
			this.CheckDispose();
			this.LogElapsedTime("E_BegProc");
			if (asyncCallback == null)
			{
				throw new ArgumentNullException("asyncCallback");
			}
			IAsyncResult result;
			try
			{
				lock (this.lockObject)
				{
					if (this.lazyAsyncResult != null)
					{
						throw new InvalidOperationException("BeginProcess() cannot be called more than once.");
					}
					ExTraceGlobals.VerboseTracer.TraceDebug<int, StreamProxy.StreamProxyType>((long)this.GetHashCode(), "[StreamProxy::BeginProcess] Context: {0}, Type :{1}.", this.requestContext.TraceContext, this.proxyType);
					this.lazyAsyncResult = new LazyAsyncResult(this, asyncState, asyncCallback);
					this.asyncException = null;
					this.streamState = StreamProxy.StreamProxyState.None;
					this.asyncStateHolder = new AsyncStateHolder(this);
					try
					{
						if (this.sourceStream != null)
						{
							this.BeginRead();
						}
						else
						{
							this.BeginSend((int)this.totalBytesProxied);
						}
					}
					catch (Exception innerException)
					{
						throw new StreamProxyException(innerException);
					}
					result = this.lazyAsyncResult;
				}
			}
			finally
			{
				this.LogElapsedTime("L_BegProc");
			}
			return result;
		}

		// Token: 0x06000393 RID: 915 RVA: 0x00014EDC File Offset: 0x000130DC
		public void EndProcess(IAsyncResult asyncResult)
		{
			this.CheckDispose();
			this.LogElapsedTime("E_EndProc");
			try
			{
				if (asyncResult == null)
				{
					throw new ArgumentNullException("asyncResult");
				}
				lock (this.lockObject)
				{
					if (this.lazyAsyncResult == null)
					{
						throw new InvalidOperationException("BeginProcess() was not called.");
					}
					if (!object.ReferenceEquals(asyncResult, this.lazyAsyncResult))
					{
						throw new InvalidOperationException("The wrong asyncResult is passed.");
					}
				}
				ExTraceGlobals.VerboseTracer.TraceDebug<int, StreamProxy.StreamProxyType>((long)this.GetHashCode(), "[StreamProxy::EndProcess] Context: {0}, Type :{1}. ", this.requestContext.TraceContext, this.proxyType);
				this.lazyAsyncResult.InternalWaitForCompletion();
				this.lazyAsyncResult = null;
				this.asyncStateHolder.Dispose();
				this.asyncStateHolder = null;
				if (this.asyncException != null)
				{
					throw new StreamProxyException(this.asyncException);
				}
			}
			finally
			{
				this.LogElapsedTime("L_EndProc");
			}
		}

		// Token: 0x06000394 RID: 916 RVA: 0x00014FDC File Offset: 0x000131DC
		public void SetTargetStreamForBufferedSend(Stream newTargetStream)
		{
			this.CheckDispose();
			this.LogElapsedTime("E_SetTargetStream");
			lock (this.lockObject)
			{
				this.sourceStream = null;
				this.targetStream = newTargetStream;
				this.OnTargetStreamUpdate();
			}
			this.LogElapsedTime("L_SetTargetStream");
		}

		// Token: 0x06000395 RID: 917 RVA: 0x00015048 File Offset: 0x00013248
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<StreamProxy>(this);
		}

		// Token: 0x06000396 RID: 918 RVA: 0x00015050 File Offset: 0x00013250
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
				this.disposeTracker = null;
			}
		}

		// Token: 0x06000397 RID: 919 RVA: 0x0001506C File Offset: 0x0001326C
		public void Dispose()
		{
			if (!this.isDisposed)
			{
				this.ReleaseBuffer();
				if (this.disposeTracker != null)
				{
					this.disposeTracker.Dispose();
					this.disposeTracker = null;
				}
				GC.SuppressFinalize(this);
				this.isDisposed = true;
			}
		}

		// Token: 0x06000398 RID: 920 RVA: 0x000150A3 File Offset: 0x000132A3
		protected virtual byte[] GetUpdatedBufferToSend(ArraySegment<byte> buffer)
		{
			return null;
		}

		// Token: 0x06000399 RID: 921 RVA: 0x000150A6 File Offset: 0x000132A6
		protected virtual void OnTargetStreamUpdate()
		{
		}

		// Token: 0x0600039A RID: 922 RVA: 0x000150A8 File Offset: 0x000132A8
		private static void ReadCompleteCallback(IAsyncResult asyncResult)
		{
			StreamProxy streamProxy = AsyncStateHolder.Unwrap<StreamProxy>(asyncResult);
			if (asyncResult.CompletedSynchronously)
			{
				ThreadPool.QueueUserWorkItem(new WaitCallback(streamProxy.OnReadComplete), asyncResult);
				return;
			}
			streamProxy.OnReadComplete(asyncResult);
		}

		// Token: 0x0600039B RID: 923 RVA: 0x000150E0 File Offset: 0x000132E0
		private static void WriteCompleteCallback(IAsyncResult asyncResult)
		{
			StreamProxy streamProxy = AsyncStateHolder.Unwrap<StreamProxy>(asyncResult);
			if (asyncResult.CompletedSynchronously)
			{
				ThreadPool.QueueUserWorkItem(new WaitCallback(streamProxy.OnWriteComplete), asyncResult);
				return;
			}
			streamProxy.OnWriteComplete(asyncResult);
		}

		// Token: 0x0600039C RID: 924 RVA: 0x00015118 File Offset: 0x00013318
		private void BeginRead()
		{
			this.LogElapsedTime("E_BeginRead");
			try
			{
				ExTraceGlobals.VerboseTracer.TraceDebug<int, StreamProxy.StreamProxyType>((long)this.GetHashCode(), "[StreamProxy::BeginRead] Context: {0}, Type :{1}. ", this.requestContext.TraceContext, this.proxyType);
				this.requestContext.LatencyTracker.StartTracking(LatencyTrackerKey.StreamingLatency, true);
				this.sourceStream.BeginRead(this.buffer, 0, this.buffer.Length, new AsyncCallback(StreamProxy.ReadCompleteCallback), this.asyncStateHolder);
				this.streamState = StreamProxy.StreamProxyState.ExpectReadCallback;
			}
			finally
			{
				this.LogElapsedTime("L_BeginRead");
			}
		}

		// Token: 0x0600039D RID: 925 RVA: 0x000151BC File Offset: 0x000133BC
		private void BeginSend(int bytesToSend)
		{
			this.LogElapsedTime("E_BeginSend");
			try
			{
				ExTraceGlobals.VerboseTracer.TraceDebug<int, StreamProxy.StreamProxyType>((long)this.GetHashCode(), "[StreamProxy::BeginSend] Context: {0}, Type :{1}. ", this.requestContext.TraceContext, this.proxyType);
				if (bytesToSend != this.numberOfBytesInBuffer)
				{
					throw new InvalidOperationException(string.Format("Invalid SendBuffer - {0} bytes in buffer, {1} bytes to be sent", this.numberOfBytesInBuffer, bytesToSend));
				}
				byte[] updatedBufferToSend = this.GetUpdatedBufferToSend(new ArraySegment<byte>(this.buffer, 0, bytesToSend));
				if (updatedBufferToSend != null)
				{
					bytesToSend = updatedBufferToSend.Length;
					ExTraceGlobals.VerboseTracer.TraceDebug<int, StreamProxy.StreamProxyType, int>((long)this.GetHashCode(), "[StreamProxy::BeginSend] Context: {0}, Type :{1}. GetUpdatedBufferToSend() returns new buffer with size {2}.", this.requestContext.TraceContext, this.proxyType, bytesToSend);
				}
				this.requestContext.LatencyTracker.StartTracking(LatencyTrackerKey.StreamingLatency, true);
				this.BeginWrite(updatedBufferToSend ?? this.buffer, bytesToSend);
			}
			finally
			{
				this.LogElapsedTime("L_BeginSend");
			}
		}

		// Token: 0x0600039E RID: 926 RVA: 0x000152AC File Offset: 0x000134AC
		private void BeginWrite(byte[] buffer, int count)
		{
			this.LogElapsedTime("E_BegWrite");
			try
			{
				ExTraceGlobals.VerboseTracer.TraceDebug((long)this.GetHashCode(), "[StreamProxy::BeginWrite] Context: {0}, Type :{1}. Writing buffer with size {2} and count {3}.", new object[]
				{
					this.requestContext.TraceContext,
					this.proxyType,
					buffer.Length,
					count
				});
				if (this.AuxTargetStream != null)
				{
					this.AuxTargetStream.Write(buffer, 0, count);
				}
				this.targetStream.BeginWrite(buffer, 0, count, new AsyncCallback(StreamProxy.WriteCompleteCallback), this.asyncStateHolder);
				this.streamState = StreamProxy.StreamProxyState.ExpectWriteCallback;
			}
			finally
			{
				this.LogElapsedTime("L_BegWrite");
			}
		}

		// Token: 0x0600039F RID: 927 RVA: 0x00015374 File Offset: 0x00013574
		private void LogElapsedTime(string latencyName)
		{
			if (HttpProxySettings.DetailedLatencyTracingEnabled.Value && this.requestContext != null && this.requestContext.LatencyTracker != null)
			{
				this.requestContext.LatencyTracker.LogElapsedTime(this.requestContext.Logger, latencyName + "_" + this.proxyType.ToString());
			}
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x000155F4 File Offset: 0x000137F4
		private void OnReadComplete(object asyncState)
		{
			Diagnostics.SendWatsonReportOnUnhandledException(delegate()
			{
				IAsyncResult asyncResult = (IAsyncResult)asyncState;
				this.LogElapsedTime("E_OnReadComp");
				try
				{
					lock (this.lockObject)
					{
						ExTraceGlobals.VerboseTracer.TraceDebug<int, StreamProxy.StreamProxyType>((long)this.GetHashCode(), "[StreamProxy::OnReadComplete] Context: {0}, Type :{1}. ", this.requestContext.TraceContext, this.proxyType);
						int num = this.sourceStream.EndRead(asyncResult);
						this.streamState = StreamProxy.StreamProxyState.None;
						if (num > 0)
						{
							switch (this.proxyType)
							{
							case StreamProxy.StreamProxyType.Request:
								PerfCounters.HttpProxyCountersInstance.TotalBytesIn.IncrementBy((long)num);
								break;
							case StreamProxy.StreamProxyType.Response:
								PerfCounters.HttpProxyCountersInstance.TotalBytesOut.IncrementBy((long)num);
								break;
							}
							this.requestContext.LatencyTracker.LogElapsedTimeAsLatency(this.requestContext.Logger, LatencyTrackerKey.StreamingLatency, this.GetReadProtocolLogKey());
							this.numberOfBytesInBuffer = num;
							this.numberOfReadsCompleted += 1L;
							this.totalBytesProxied += (long)num;
							this.BeginSend(num);
						}
						else
						{
							this.Complete(null);
						}
					}
				}
				catch (Exception ex)
				{
					ExTraceGlobals.VerboseTracer.TraceError<int, StreamProxy.StreamProxyType, Exception>((long)this.GetHashCode(), "[StreamProxy::OnReadComplete] Context: {0}, Type :{1}. Error occured thrown when processing read. Exception: {2}", this.requestContext.TraceContext, this.proxyType, ex);
					this.Complete(ex);
				}
				finally
				{
					this.LogElapsedTime("L_OnReadComp");
				}
			}, new Diagnostics.LastChanceExceptionHandler(RequestDetailsLogger.LastChanceExceptionHandler));
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x000157E4 File Offset: 0x000139E4
		private void OnWriteComplete(object asyncState)
		{
			Diagnostics.SendWatsonReportOnUnhandledException(delegate()
			{
				IAsyncResult asyncResult = (IAsyncResult)asyncState;
				this.LogElapsedTime("E_OnWriteComp");
				try
				{
					lock (this.lockObject)
					{
						ExTraceGlobals.VerboseTracer.TraceDebug<int, StreamProxy.StreamProxyType>((long)this.GetHashCode(), "[StreamProxy::OnWriteComplete] Context: {0}, Type :{1}. ", this.requestContext.TraceContext, this.proxyType);
						this.targetStream.EndWrite(asyncResult);
						this.streamState = StreamProxy.StreamProxyState.None;
						this.requestContext.LatencyTracker.LogElapsedTimeAsLatency(this.requestContext.Logger, LatencyTrackerKey.StreamingLatency, this.GetWriteProtocolLogKey());
						if (this.sourceStream != null)
						{
							this.AdjustBuffer();
							this.BeginRead();
						}
						else
						{
							this.Complete(null);
						}
					}
				}
				catch (Exception ex)
				{
					ExTraceGlobals.VerboseTracer.TraceError<int, StreamProxy.StreamProxyType, Exception>((long)this.GetHashCode(), "[StreamProxy::OnWriteComplete] Context: {0}, Type :{1}. Error occured thrown when processing write. Exception: {2}", this.requestContext.TraceContext, this.proxyType, ex);
					this.Complete(ex);
				}
				finally
				{
					this.LogElapsedTime("L_OnWriteComp");
				}
			}, new Diagnostics.LastChanceExceptionHandler(RequestDetailsLogger.LastChanceExceptionHandler));
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x00015824 File Offset: 0x00013A24
		private void Complete(Exception exception)
		{
			this.LogElapsedTime("E_SPComplete");
			try
			{
				ExTraceGlobals.VerboseTracer.TraceDebug<int, StreamProxy.StreamProxyType, Exception>((long)this.GetHashCode(), "[StreamProxy::Complete] Context: {0}, Type :{1}. Complete with exception: {2}", this.requestContext.TraceContext, this.proxyType, exception);
				this.asyncException = exception;
				this.lazyAsyncResult.InvokeCallback();
			}
			finally
			{
				this.LogElapsedTime("L_SPComplete");
			}
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x00015898 File Offset: 0x00013A98
		private HttpProxyMetadata GetReadProtocolLogKey()
		{
			if (this.proxyType == StreamProxy.StreamProxyType.Request)
			{
				return HttpProxyMetadata.ClientRequestStreamingLatency;
			}
			return HttpProxyMetadata.BackendResponseStreamingLatency;
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x000158A7 File Offset: 0x00013AA7
		private HttpProxyMetadata GetWriteProtocolLogKey()
		{
			if (this.proxyType == StreamProxy.StreamProxyType.Request)
			{
				return HttpProxyMetadata.BackendRequestStreamingLatency;
			}
			return HttpProxyMetadata.ClientResponseStreamingLatency;
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x000158B8 File Offset: 0x00013AB8
		private void ReleaseBuffer()
		{
			if (this.buffer != null && this.currentBufferPool != null)
			{
				try
				{
					this.currentBufferPool.Release(this.buffer);
				}
				finally
				{
					this.buffer = null;
				}
			}
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x00015900 File Offset: 0x00013B00
		private void AdjustBuffer()
		{
			if (this.currentBufferPool == null)
			{
				return;
			}
			if (this.numberOfBytesInBuffer >= this.buffer.Length)
			{
				if (this.currentBufferPoolSize < this.maxBufferPoolSize)
				{
					this.previousBufferSize = this.buffer.Length;
					this.ReleaseBuffer();
					this.currentBufferPoolSize++;
					this.currentBufferPool = BufferPoolCollection.AutoCleanupCollection.Acquire(this.currentBufferPoolSize);
					this.buffer = this.currentBufferPool.Acquire();
					return;
				}
			}
			else if (this.currentBufferPoolSize > this.minBufferPoolSize)
			{
				if (this.numberOfBytesInBuffer == this.previousBufferSize)
				{
					this.ReleaseBuffer();
					this.currentBufferPoolSize--;
					this.currentBufferPool = BufferPoolCollection.AutoCleanupCollection.Acquire(this.currentBufferPoolSize);
					this.buffer = this.currentBufferPool.Acquire();
					this.maxBufferPoolSize = this.currentBufferPoolSize;
					this.minBufferPoolSize = this.currentBufferPoolSize;
					return;
				}
				if (this.numberOfBytesInBuffer > this.previousBufferSize)
				{
					this.previousBufferSize = this.buffer.Length;
				}
			}
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x00015A0D File Offset: 0x00013C0D
		private void CheckDispose()
		{
			if (!this.isDisposed)
			{
				return;
			}
			throw new ObjectDisposedException("StreamProxy");
		}

		// Token: 0x04000283 RID: 643
		private readonly object lockObject = new object();

		// Token: 0x04000284 RID: 644
		private readonly StreamProxy.StreamProxyType proxyType;

		// Token: 0x04000285 RID: 645
		private readonly IRequestContext requestContext;

		// Token: 0x04000286 RID: 646
		private Stream sourceStream;

		// Token: 0x04000287 RID: 647
		private Stream targetStream;

		// Token: 0x04000288 RID: 648
		private StreamProxy.StreamProxyState streamState;

		// Token: 0x04000289 RID: 649
		private Stream auxTargetStream;

		// Token: 0x0400028A RID: 650
		private long totalBytesProxied;

		// Token: 0x0400028B RID: 651
		private long numberOfReadsCompleted;

		// Token: 0x0400028C RID: 652
		private int numberOfBytesInBuffer;

		// Token: 0x0400028D RID: 653
		private LazyAsyncResult lazyAsyncResult;

		// Token: 0x0400028E RID: 654
		private AsyncStateHolder asyncStateHolder;

		// Token: 0x0400028F RID: 655
		private Exception asyncException;

		// Token: 0x04000290 RID: 656
		private byte[] buffer;

		// Token: 0x04000291 RID: 657
		private BufferPoolCollection.BufferSize maxBufferPoolSize;

		// Token: 0x04000292 RID: 658
		private BufferPoolCollection.BufferSize minBufferPoolSize;

		// Token: 0x04000293 RID: 659
		private BufferPoolCollection.BufferSize currentBufferPoolSize;

		// Token: 0x04000294 RID: 660
		private int previousBufferSize;

		// Token: 0x04000295 RID: 661
		private BufferPool currentBufferPool;

		// Token: 0x04000296 RID: 662
		private DisposeTracker disposeTracker;

		// Token: 0x04000297 RID: 663
		private bool isDisposed;

		// Token: 0x02000075 RID: 117
		internal enum StreamProxyType
		{
			// Token: 0x04000299 RID: 665
			Request,
			// Token: 0x0400029A RID: 666
			Response
		}

		// Token: 0x02000076 RID: 118
		internal enum StreamProxyState
		{
			// Token: 0x0400029C RID: 668
			None,
			// Token: 0x0400029D RID: 669
			ExpectReadCallback,
			// Token: 0x0400029E RID: 670
			ExpectWriteCallback
		}
	}
}
