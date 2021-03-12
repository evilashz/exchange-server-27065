using System;
using System.Collections.Generic;
using System.IO;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x0200022C RID: 556
	internal abstract class EwsResponseWireWriter : IDisposable
	{
		// Token: 0x06000E3E RID: 3646 RVA: 0x0004591E File Offset: 0x00043B1E
		public static EwsResponseWireWriter Create(CallContext context)
		{
			return EwsResponseWireWriter.Create(context, false);
		}

		// Token: 0x06000E3F RID: 3647 RVA: 0x00045927 File Offset: 0x00043B27
		public static EwsResponseWireWriter Create(CallContext context, bool initWaitEvent)
		{
			return new EwsResponseWireWriter.EwsHttpResponseWireWriter(context, initWaitEvent);
		}

		// Token: 0x06000E40 RID: 3648
		public abstract void WriteResponseToWire(BaseSoapResponse response, bool isLastResponse);

		// Token: 0x06000E41 RID: 3649
		public abstract void WriteResponseToWire(byte[] responseBytes, int offset, int count);

		// Token: 0x06000E42 RID: 3650
		public abstract void FinishWritesAndCompleteResponse(CompleteRequestAsyncCallback completeRequest);

		// Token: 0x06000E43 RID: 3651
		public abstract void WaitForSendCompletion();

		// Token: 0x06000E44 RID: 3652
		public abstract void Dispose();

		// Token: 0x0200022D RID: 557
		private class EwsHttpResponseWireWriter : EwsResponseWireWriter, IDisposeTrackable, IDisposable
		{
			// Token: 0x170001A9 RID: 425
			// (get) Token: 0x06000E46 RID: 3654 RVA: 0x00045938 File Offset: 0x00043B38
			private static HttpResponseMessageProperty WcfHttpResponseMessageProperty
			{
				get
				{
					if (EwsResponseWireWriter.EwsHttpResponseWireWriter.httpResponseMessageProperty == null)
					{
						EwsResponseWireWriter.EwsHttpResponseWireWriter.httpResponseMessageProperty = new HttpResponseMessageProperty();
						EwsResponseWireWriter.EwsHttpResponseWireWriter.httpResponseMessageProperty.SuppressPreamble = true;
						EwsResponseWireWriter.EwsHttpResponseWireWriter.httpResponseMessageProperty.SuppressEntityBody = true;
					}
					return EwsResponseWireWriter.EwsHttpResponseWireWriter.httpResponseMessageProperty;
				}
			}

			// Token: 0x06000E47 RID: 3655 RVA: 0x00045968 File Offset: 0x00043B68
			internal EwsHttpResponseWireWriter(CallContext callContext, bool initWaitEvent)
			{
				ExTraceGlobals.GetEventsCallTracer.TraceFunction<int, bool>((long)this.GetHashCode(), "EwsHttpResponseWireWriter.ctor: entering. hashcode: {0}, initWaitEvent: {1}", this.GetHashCode(), initWaitEvent);
				this.callContext = callContext;
				this.responsesToSend = new Queue<Tuple<object, bool>>();
				this.version = ExchangeVersion.Current;
				this.writeTimer = new Timer(new TimerCallback(this.WriteTimeoutCallback), this, -1, -1);
				this.callContext.OperationContext.OutgoingMessageProperties[HttpResponseMessageProperty.Name] = EwsResponseWireWriter.EwsHttpResponseWireWriter.WcfHttpResponseMessageProperty;
				this.callContext.HttpContext.Items["ResponseHasBegun"] = true;
				this.disposeTracker = this.GetDisposeTracker();
				this.initWaitEvent = initWaitEvent;
				bool flag = !this.initWaitEvent;
				ExTraceGlobals.GetEventsCallTracer.TraceDebug<bool>((long)this.GetHashCode(), "EwsHttpResponseWireWriter.ctor: useChunkedStreaming {0}", flag);
				this.streamingResponse = (flag ? new EwsResponseWireWriter.EwsHttpResponseWireWriter.ChunkedStreamingResponse(this.callContext.HttpContext.Response) : new EwsResponseWireWriter.EwsHttpResponseWireWriter.StreamingResponse(this.callContext.HttpContext.Response));
			}

			// Token: 0x06000E48 RID: 3656 RVA: 0x00045A8D File Offset: 0x00043C8D
			public virtual DisposeTracker GetDisposeTracker()
			{
				return DisposeTracker.Get<EwsResponseWireWriter.EwsHttpResponseWireWriter>(this);
			}

			// Token: 0x06000E49 RID: 3657 RVA: 0x00045A95 File Offset: 0x00043C95
			public void SuppressDisposeTracker()
			{
				if (this.disposeTracker != null)
				{
					this.disposeTracker.Suppress();
				}
			}

			// Token: 0x06000E4A RID: 3658 RVA: 0x00045AAC File Offset: 0x00043CAC
			public override void WriteResponseToWire(BaseSoapResponse response, bool isLastResponse)
			{
				ExTraceGlobals.GetEventsCallTracer.TraceFunction<Type, bool>((long)this.GetHashCode(), "EwsHttpResponseWireWriter.WriteResponseToWire(BaseSoapResponse): {0}, isLastResponse: {1}", response.GetType(), isLastResponse);
				lock (this.lockObject)
				{
					if (this.finalCallback != null)
					{
						throw new InvalidOperationException("EwsHttpResponseWireWriter.WriteResponseToWire(BaseSoapResponse) Cannot write a new response when a final response has been given");
					}
					this.responsesToSend.Enqueue(new Tuple<object, bool>(response, isLastResponse));
					ExTraceGlobals.GetEventsCallTracer.TraceDebug<int>((long)this.GetHashCode(), "EwsHttpResponseWireWriter.WriteResponseToWire(BaseSoapResponse): responses length {0}", this.responsesToSend.Count);
				}
				this.BeginSendIfNecessary();
			}

			// Token: 0x06000E4B RID: 3659 RVA: 0x00045B50 File Offset: 0x00043D50
			public override void WriteResponseToWire(byte[] responseBytes, int offset, int count)
			{
				ExTraceGlobals.GetEventsCallTracer.TraceFunction<int>((long)this.GetHashCode(), "EwsHttpResponseWireWriter.WriteResponseToWire(byte[]) entering, count: {0}", count);
				lock (this.lockObject)
				{
					if (this.finalCallback != null)
					{
						throw new InvalidOperationException("EwsHttpResponseWireWriter.WriteResponseToWire(byte[]) Cannot write a new response when a final response has been given");
					}
					if (responseBytes == null)
					{
						throw new ArgumentNullException("responseBytes");
					}
					if (offset < 0 || offset >= responseBytes.Length)
					{
						throw new ArgumentOutOfRangeException("offset");
					}
					if (count <= 0 || count + offset > responseBytes.Length)
					{
						throw new ArgumentOutOfRangeException("count");
					}
					byte[] array = new byte[count];
					Buffer.BlockCopy(responseBytes, offset, array, 0, count);
					this.responsesToSend.Enqueue(new Tuple<object, bool>(array, false));
					ExTraceGlobals.GetEventsCallTracer.TraceDebug<int>((long)this.GetHashCode(), "EwsHttpResponseWireWriter.WriteResponseToWire(byte[]): responses length {0}", this.responsesToSend.Count);
				}
				this.BeginSendIfNecessary();
			}

			// Token: 0x06000E4C RID: 3660 RVA: 0x00045C38 File Offset: 0x00043E38
			public override void FinishWritesAndCompleteResponse(CompleteRequestAsyncCallback completeRequest)
			{
				ExTraceGlobals.GetEventsCallTracer.TraceFunction((long)this.GetHashCode(), "EwsHttpResponseWireWriter.FinishWritesAndCompleteResponse entering");
				lock (this.lockObject)
				{
					this.finalCallback = completeRequest;
					ExTraceGlobals.GetEventsCallTracer.TraceDebug<int, EwsResponseWireWriter.EwsHttpResponseWireWriter.SendState>((long)this.GetHashCode(), "EwsHttpResponseWireWriter.FinishWritesAndCompleteResponse: responses length: {0}, sendState: {1}", this.responsesToSend.Count, this.sendState);
					if (!this.callContext.HttpContext.Response.IsClientConnected || (this.responsesToSend.Count == 0 && this.sendState != EwsResponseWireWriter.EwsHttpResponseWireWriter.SendState.Sending))
					{
						this.CompleteResponse();
					}
				}
			}

			// Token: 0x06000E4D RID: 3661 RVA: 0x00045CEC File Offset: 0x00043EEC
			public override void WaitForSendCompletion()
			{
				ExTraceGlobals.GetEventsCallTracer.TraceFunction((long)this.GetHashCode(), "EwsHttpResponseWireWriter.WaitForSendCompletion entering");
				if (!this.initWaitEvent)
				{
					throw new InvalidOperationException("EwsResponseWireWriter was not created with wait handle.");
				}
				if (this.sendFinishEvent != null)
				{
					this.sendFinishEvent.WaitOne(60000);
				}
				ExTraceGlobals.GetEventsCallTracer.TraceFunction((long)this.GetHashCode(), "EwsHttpResponseWireWriter.WaitForSendCompletion exiting");
			}

			// Token: 0x06000E4E RID: 3662 RVA: 0x00045D54 File Offset: 0x00043F54
			private static XmlSerializer GetSerializer(object objectToSerialize)
			{
				Type type = objectToSerialize.GetType();
				XmlSerializer result;
				lock (EwsResponseWireWriter.EwsHttpResponseWireWriter.serializers)
				{
					if (!EwsResponseWireWriter.EwsHttpResponseWireWriter.serializers.ContainsKey(type))
					{
						SafeXmlSerializer value = new SafeXmlSerializer(type);
						EwsResponseWireWriter.EwsHttpResponseWireWriter.serializers.Add(type, value);
					}
					result = EwsResponseWireWriter.EwsHttpResponseWireWriter.serializers[type];
				}
				return result;
			}

			// Token: 0x06000E4F RID: 3663 RVA: 0x00045DC4 File Offset: 0x00043FC4
			private void WriteTimeoutCallback(object state)
			{
				ExTraceGlobals.GetEventsCallTracer.TraceFunction((long)this.GetHashCode(), "EwsHttpResponseWireWriter.WriteTimeoutCallback entering");
				lock (this.lockObject)
				{
					this.connectionTimedOut = true;
					try
					{
						if (this.writeOperationAsyncResult != null)
						{
							this.callContext.HttpContext.Response.OutputStream.Close();
							this.UpdateWriteTimer(-1);
						}
					}
					finally
					{
						this.SignalSendFinishEvent();
					}
				}
			}

			// Token: 0x06000E50 RID: 3664 RVA: 0x00045E5C File Offset: 0x0004405C
			private void UpdateWriteTimer(int timeoutMilliseconds)
			{
				lock (this.lockObject)
				{
					if (this.writeTimer != null)
					{
						this.writeTimer.Change(timeoutMilliseconds, -1);
					}
				}
			}

			// Token: 0x06000E51 RID: 3665 RVA: 0x00045EAC File Offset: 0x000440AC
			private void BeginSendIfNecessary()
			{
				ExTraceGlobals.GetEventsCallTracer.TraceFunction((long)this.GetHashCode(), "EwsHttpResponseWireWriter.BeginSendIfNecessary entering");
				bool flag = false;
				lock (this.lockObject)
				{
					EwsResponseWireWriter.EwsHttpResponseWireWriter.SendState arg = this.sendState;
					if (this.sendState != EwsResponseWireWriter.EwsHttpResponseWireWriter.SendState.Sending)
					{
						if (this.sendState == EwsResponseWireWriter.EwsHttpResponseWireWriter.SendState.Closed || !this.callContext.HttpContext.Response.IsClientConnected)
						{
							this.sendState = EwsResponseWireWriter.EwsHttpResponseWireWriter.SendState.Closed;
							ExTraceGlobals.GetEventsCallTracer.TraceDebug((long)this.GetHashCode(), "[EwsHttpResponseWireWriter::BeginSendIfNecessary] The client disconnected from this request.");
							throw new HttpException("Client connection is closed");
						}
						this.sendState = EwsResponseWireWriter.EwsHttpResponseWireWriter.SendState.Sending;
						flag = true;
					}
					ExTraceGlobals.GetEventsCallTracer.TraceDebug<EwsResponseWireWriter.EwsHttpResponseWireWriter.SendState, EwsResponseWireWriter.EwsHttpResponseWireWriter.SendState, bool>((long)this.GetHashCode(), "EwsHttpResponseWireWriter.BeginSendIfNecessary: old SendState: {0}, new: {1}, beginSend: {2}", arg, this.sendState, flag);
				}
				if (flag)
				{
					this.SendResponse();
				}
			}

			// Token: 0x06000E52 RID: 3666 RVA: 0x00045F88 File Offset: 0x00044188
			private void SendResponse()
			{
				ExchangeVersion.ExecuteWithSpecifiedVersion(this.version, new ExchangeVersion.ExchangeVersionDelegate(this.SendResponseWithVersionSet));
			}

			// Token: 0x06000E53 RID: 3667 RVA: 0x00045FA4 File Offset: 0x000441A4
			private void SendResponseWithVersionSet()
			{
				ExTraceGlobals.GetEventsCallTracer.TraceFunction((long)this.GetHashCode(), "EwsHttpResponseWireWriter.SendResponseWithVersionSet entering");
				object item;
				bool item2;
				lock (this.lockObject)
				{
					ExTraceGlobals.GetEventsCallTracer.TraceDebug<int>((long)this.GetHashCode(), "EwsHttpResponseWireWriter.SendResponseWithVersionSet: responses length: {0}", this.responsesToSend.Count);
					if (this.responsesToSend.Count <= 0)
					{
						this.OnSendingComplete();
						this.SignalSendFinishEvent();
						return;
					}
					Tuple<object, bool> tuple = this.responsesToSend.Dequeue();
					item = tuple.Item1;
					item2 = tuple.Item2;
				}
				XmlSerializer serializer = EwsResponseWireWriter.EwsHttpResponseWireWriter.GetSerializer(item);
				XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
				xmlWriterSettings.OmitXmlDeclaration = true;
				int num = 0;
				lock (this.lockObject)
				{
					byte[] array;
					int num2;
					if (item is BaseSoapResponse)
					{
						this.responseBufferStream.Seek(0L, SeekOrigin.Begin);
						XmlWriter xmlWriter = XmlWriter.Create(this.responseBufferStream, xmlWriterSettings);
						serializer.Serialize(xmlWriter, item);
						array = this.responseBufferStream.ToArray();
						num2 = (int)this.responseBufferStream.Position;
						if (num2 > 3 && array[0] == 239 && array[1] == 187 && array[2] == 191)
						{
							num = 3;
							num2 -= num;
						}
					}
					else
					{
						if (!(item is byte[]))
						{
							throw new InvalidOperationException("Stored responses must be either of type BaseSoapResponse or of type byte[].");
						}
						array = (item as byte[]);
						num2 = array.Length;
					}
					try
					{
						if (this.sendFinishEvent == null)
						{
							if (this.initWaitEvent)
							{
								this.sendFinishEvent = new ManualResetEvent(false);
							}
						}
						else
						{
							this.sendFinishEvent.Reset();
						}
						this.writeOperationAsyncResult = this.streamingResponse.BeginWrite(array, num, num2, item2, new AsyncCallback(this.EndSendResponse), this);
						this.UpdateWriteTimer(60000);
					}
					catch (Exception arg)
					{
						ExTraceGlobals.GetEventsCallTracer.TraceError<Exception>((long)this.GetHashCode(), "EwsHttpResponseWireWriter.SendResponseWithVersionSet: exception: {0}", arg);
						this.SignalSendFinishEvent();
						throw;
					}
				}
			}

			// Token: 0x06000E54 RID: 3668 RVA: 0x000461E8 File Offset: 0x000443E8
			private void SignalSendFinishEvent()
			{
				ExTraceGlobals.GetEventsCallTracer.TraceFunction((long)this.GetHashCode(), "EwsHttpResponseWireWriter.SignalSendFinishEvent entering");
				lock (this.lockObject)
				{
					if (this.sendFinishEvent != null)
					{
						ExTraceGlobals.GetEventsCallTracer.TraceDebug((long)this.GetHashCode(), "EwsHttpResponseWireWriter.SignalSendFinishEvent: set sendFinishEvent");
						this.sendFinishEvent.Set();
					}
				}
			}

			// Token: 0x06000E55 RID: 3669 RVA: 0x00046264 File Offset: 0x00044464
			private void EndSendResponse(IAsyncResult result)
			{
				ExTraceGlobals.GetEventsCallTracer.TraceFunction((long)this.GetHashCode(), "EwsHttpResponseWireWriter.EndSendResponse entering");
				bool flag = false;
				try
				{
					flag = this.InternalEndSendResponse(result);
					ExTraceGlobals.GetEventsCallTracer.TraceFunction<bool>((long)this.GetHashCode(), "EwsHttpResponseWireWriter.EndSendResponse: shouldContinue value is {0}", flag);
				}
				finally
				{
					if (!flag)
					{
						this.SignalSendFinishEvent();
					}
				}
			}

			// Token: 0x06000E56 RID: 3670 RVA: 0x000462C4 File Offset: 0x000444C4
			private bool InternalEndSendResponse(IAsyncResult result)
			{
				bool result2 = false;
				Exception ex = null;
				try
				{
					lock (this.lockObject)
					{
						this.UpdateWriteTimer(-1);
						this.writeOperationAsyncResult = null;
						if (this.connectionTimedOut)
						{
							throw new TimeoutException("Write operation has timed out.");
						}
					}
					this.streamingResponse.EndWrite(result);
					this.streamingResponse.Flush();
				}
				catch (NullReferenceException ex2)
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
				catch (TimeoutException ex5)
				{
					ex = ex5;
				}
				catch (AggregateException ex6)
				{
					ex = ex6;
				}
				if (ex != null)
				{
					ExTraceGlobals.GetEventsCallTracer.TraceDebug<Exception>((long)this.GetHashCode(), "[EwsHttpResponseWireWriter::InternalEndSendResponse] Exception sending response: {0}", ex);
					lock (this.lockObject)
					{
						this.sendState = EwsResponseWireWriter.EwsHttpResponseWireWriter.SendState.Closed;
					}
					return result2;
				}
				EwsResponseWireWriter.EwsHttpResponseWireWriter.SendState sendState;
				lock (this.lockObject)
				{
					if (this.responsesToSend.Count == 0)
					{
						this.OnSendingComplete();
					}
					else if (!this.callContext.HttpContext.Response.IsClientConnected)
					{
						this.sendState = EwsResponseWireWriter.EwsHttpResponseWireWriter.SendState.Closed;
					}
					sendState = this.sendState;
					ExTraceGlobals.GetEventsCallTracer.TraceDebug<EwsResponseWireWriter.EwsHttpResponseWireWriter.SendState, int>((long)this.GetHashCode(), "[EwsHttpResponseWireWriter::InternalEndSendResponse] sendState: {0}, responses length: {1}", this.sendState, this.responsesToSend.Count);
				}
				if (sendState == EwsResponseWireWriter.EwsHttpResponseWireWriter.SendState.Sending)
				{
					ExTraceGlobals.GetEventsCallTracer.TraceDebug((long)this.GetHashCode(), "[EwsHttpResponseWireWriter::InternalEndSendResponse] going to call SendResponse again");
					try
					{
						this.SendResponse();
						return true;
					}
					catch (ObjectDisposedException arg)
					{
						ExTraceGlobals.CommonAlgorithmTracer.TraceError<ObjectDisposedException>((long)this.GetHashCode(), "[EwsResponseWireWriter.InternalEndSendResponse] ObjectDisposedException encountered.  {0}", arg);
						return false;
					}
				}
				if (sendState == EwsResponseWireWriter.EwsHttpResponseWireWriter.SendState.Closed && this.finalCallback != null)
				{
					ExTraceGlobals.GetEventsCallTracer.TraceDebug((long)this.GetHashCode(), "[EwsHttpResponseWireWriter::InternalEndSendResponse] going to call CompleteResponse");
					this.CompleteResponse();
				}
				return result2;
			}

			// Token: 0x06000E57 RID: 3671 RVA: 0x000464F4 File Offset: 0x000446F4
			private void OnSendingComplete()
			{
				ExTraceGlobals.GetEventsCallTracer.TraceDebug<bool>((long)this.GetHashCode(), "EwsHttpResponseWireWriter.OnSendingComplete] entering, finalCallback == null: {0}", this.finalCallback == null);
				if (this.finalCallback == null)
				{
					this.sendState = EwsResponseWireWriter.EwsHttpResponseWireWriter.SendState.Idle;
					return;
				}
				this.sendState = EwsResponseWireWriter.EwsHttpResponseWireWriter.SendState.Closed;
			}

			// Token: 0x06000E58 RID: 3672 RVA: 0x0004652C File Offset: 0x0004472C
			private void CompleteResponse()
			{
				ExTraceGlobals.GetEventsCallTracer.TraceFunction((long)this.GetHashCode(), "EwsHttpResponseWireWriter.CompleteResponse] entering");
				lock (this.lockObject)
				{
					if (!this.finalWcfCallbackInvoked)
					{
						ExTraceGlobals.GetEventsCallTracer.TraceFunction((long)this.GetHashCode(), "EwsHttpResponseWireWriter.CompleteResponse] going to dispose stuffs and finalCallback");
						this.responseBufferStream.Close();
						if (this.writeTimer != null)
						{
							this.writeTimer.Dispose();
							this.writeTimer = null;
						}
						if (this.finalCallback != null)
						{
							this.finalCallback(null);
						}
						if (this.streamingResponse != null)
						{
							this.streamingResponse.Dispose();
							this.streamingResponse = null;
						}
						this.finalWcfCallbackInvoked = true;
					}
				}
			}

			// Token: 0x06000E59 RID: 3673 RVA: 0x000465F4 File Offset: 0x000447F4
			public override void Dispose()
			{
				if (this.disposeTracker != null)
				{
					this.disposeTracker.Dispose();
				}
				this.Dispose(true);
			}

			// Token: 0x06000E5A RID: 3674 RVA: 0x00046610 File Offset: 0x00044810
			private void Dispose(bool suppressFinalizer)
			{
				lock (this.lockObject)
				{
					if (!this.isDisposed)
					{
						if (suppressFinalizer)
						{
							GC.SuppressFinalize(this);
						}
						this.isDisposed = true;
					}
				}
			}

			// Token: 0x04000B00 RID: 2816
			private const int MaxWriteTimeout = 60000;

			// Token: 0x04000B01 RID: 2817
			private const string IsaNoBuffering = "X-NoBuffering";

			// Token: 0x04000B02 RID: 2818
			private readonly DisposeTracker disposeTracker;

			// Token: 0x04000B03 RID: 2819
			private readonly bool initWaitEvent;

			// Token: 0x04000B04 RID: 2820
			private static HttpResponseMessageProperty httpResponseMessageProperty;

			// Token: 0x04000B05 RID: 2821
			private static Dictionary<Type, XmlSerializer> serializers = new Dictionary<Type, XmlSerializer>();

			// Token: 0x04000B06 RID: 2822
			private CallContext callContext;

			// Token: 0x04000B07 RID: 2823
			private Queue<Tuple<object, bool>> responsesToSend;

			// Token: 0x04000B08 RID: 2824
			private EwsResponseWireWriter.EwsHttpResponseWireWriter.SendState sendState;

			// Token: 0x04000B09 RID: 2825
			private ExchangeVersion version;

			// Token: 0x04000B0A RID: 2826
			private CompleteRequestAsyncCallback finalCallback;

			// Token: 0x04000B0B RID: 2827
			private bool finalWcfCallbackInvoked;

			// Token: 0x04000B0C RID: 2828
			private object lockObject = new object();

			// Token: 0x04000B0D RID: 2829
			private MemoryStream responseBufferStream = new MemoryStream();

			// Token: 0x04000B0E RID: 2830
			private Timer writeTimer;

			// Token: 0x04000B0F RID: 2831
			private bool connectionTimedOut;

			// Token: 0x04000B10 RID: 2832
			private IAsyncResult writeOperationAsyncResult;

			// Token: 0x04000B11 RID: 2833
			private EventWaitHandle sendFinishEvent;

			// Token: 0x04000B12 RID: 2834
			private EwsResponseWireWriter.EwsHttpResponseWireWriter.StreamingResponse streamingResponse;

			// Token: 0x04000B13 RID: 2835
			private bool isDisposed;

			// Token: 0x0200022E RID: 558
			private enum SendState
			{
				// Token: 0x04000B15 RID: 2837
				Idle,
				// Token: 0x04000B16 RID: 2838
				Sending,
				// Token: 0x04000B17 RID: 2839
				Closed
			}

			// Token: 0x0200022F RID: 559
			private class StreamingResponse : DisposeTrackableBase
			{
				// Token: 0x06000E5C RID: 3676 RVA: 0x00046670 File Offset: 0x00044870
				public StreamingResponse(HttpResponse httpResponse)
				{
					this.response = httpResponse;
					this.response.BufferOutput = false;
					this.response.Buffer = false;
					this.response.AppendHeader("X-NoBuffering", "1");
					this.response.ContentType = string.Empty;
					this.response.ClearContent();
				}

				// Token: 0x06000E5D RID: 3677 RVA: 0x000466D2 File Offset: 0x000448D2
				public virtual IAsyncResult BeginWrite(byte[] buffer, int offset, int count, bool isLastResponse, AsyncCallback ac, object state)
				{
					return this.response.OutputStream.BeginWrite(buffer, offset, count, ac, state);
				}

				// Token: 0x06000E5E RID: 3678 RVA: 0x000466EB File Offset: 0x000448EB
				public virtual void EndWrite(IAsyncResult ar)
				{
					this.response.OutputStream.EndWrite(ar);
				}

				// Token: 0x06000E5F RID: 3679 RVA: 0x000466FE File Offset: 0x000448FE
				public virtual void Flush()
				{
					this.response.Flush();
				}

				// Token: 0x06000E60 RID: 3680 RVA: 0x0004670B File Offset: 0x0004490B
				protected override DisposeTracker InternalGetDisposeTracker()
				{
					return DisposeTracker.Get<EwsResponseWireWriter.EwsHttpResponseWireWriter.StreamingResponse>(this);
				}

				// Token: 0x06000E61 RID: 3681 RVA: 0x00046713 File Offset: 0x00044913
				protected override void InternalDispose(bool isDisposing)
				{
				}

				// Token: 0x04000B18 RID: 2840
				protected HttpResponse response;
			}

			// Token: 0x02000230 RID: 560
			private sealed class ChunkedStreamingResponse : EwsResponseWireWriter.EwsHttpResponseWireWriter.StreamingResponse
			{
				// Token: 0x06000E62 RID: 3682 RVA: 0x00046718 File Offset: 0x00044918
				internal ChunkedStreamingResponse(HttpResponse httpResponse) : base(httpResponse)
				{
					this.response.AddHeader("Transfer-Encoding", "chunked");
					this.streamWriter = new StreamWriter(this.response.OutputStream, new UTF8Encoding(false, false), 8192);
					this.sentBefore = false;
				}

				// Token: 0x06000E63 RID: 3683 RVA: 0x000467A0 File Offset: 0x000449A0
				public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, bool isLastResponse, AsyncCallback ac, object state)
				{
					string notificationString = (offset == 0) ? Encoding.UTF8.GetString(buffer, 0, buffer.Length) : Encoding.UTF8.GetString(buffer, offset, count);
					Task task = Task.Factory.StartNew(delegate(object x)
					{
						this.Write(notificationString, isLastResponse);
					}, state);
					task.ContinueWith(delegate(Task res)
					{
						ac(task);
					});
					return task;
				}

				// Token: 0x06000E64 RID: 3684 RVA: 0x00046830 File Offset: 0x00044A30
				public override void EndWrite(IAsyncResult ar)
				{
					using (Task task = ar as Task)
					{
						if (task.Exception != null)
						{
							throw task.Exception;
						}
					}
				}

				// Token: 0x06000E65 RID: 3685 RVA: 0x00046870 File Offset: 0x00044A70
				public override void Flush()
				{
				}

				// Token: 0x06000E66 RID: 3686 RVA: 0x00046874 File Offset: 0x00044A74
				private void Write(string content, bool isLastResponse)
				{
					string value;
					if (isLastResponse)
					{
						value = string.Format("{0}\r\n{1:x}\r\n{2}\r\n{3:x}\r\n\r\n", new object[]
						{
							" ",
							content.Length,
							content,
							0
						});
					}
					else if (!this.sentBefore)
					{
						value = string.Format("{0:x}\r\n{1}\r\n{2:x}\r\n{3}", new object[]
						{
							content.Length,
							content,
							"  ".Length,
							" "
						});
						this.sentBefore = true;
					}
					else
					{
						value = string.Format("{0}\r\n{1:x}\r\n{2}\r\n{3:x}\r\n{4}", new object[]
						{
							" ",
							content.Length,
							content,
							"  ".Length,
							" "
						});
					}
					this.streamWriter.Write(value);
					this.streamWriter.Flush();
				}

				// Token: 0x06000E67 RID: 3687 RVA: 0x0004696F File Offset: 0x00044B6F
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

				// Token: 0x04000B19 RID: 2841
				private const string FullDummyResponse = "  ";

				// Token: 0x04000B1A RID: 2842
				private const string PartialDummyResponse = " ";

				// Token: 0x04000B1B RID: 2843
				private const string FirstChunkStringFormat = "{0:x}\r\n{1}\r\n{2:x}\r\n{3}";

				// Token: 0x04000B1C RID: 2844
				private const string MidChunkStringFormat = "{0}\r\n{1:x}\r\n{2}\r\n{3:x}\r\n{4}";

				// Token: 0x04000B1D RID: 2845
				private const string LastChunkStringFormat = "{0}\r\n{1:x}\r\n{2}\r\n{3:x}\r\n\r\n";

				// Token: 0x04000B1E RID: 2846
				private StreamWriter streamWriter;

				// Token: 0x04000B1F RID: 2847
				private bool disposed;

				// Token: 0x04000B20 RID: 2848
				private bool sentBefore;
			}
		}
	}
}
