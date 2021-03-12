using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Timers;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MapiHttpClient;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.RpcClientAccess;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x0200000B RID: 11
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class MapiHttpClient : BaseObject
	{
		// Token: 0x0600005D RID: 93 RVA: 0x00004778 File Offset: 0x00002978
		protected MapiHttpClient(MapiHttpBindingInfo bindingInfo)
		{
			using (DisposeGuard disposeGuard = this.Guard())
			{
				Util.ThrowOnNullArgument(bindingInfo, "bindingInfo");
				this.bindingInfo = bindingInfo;
				this.pingCompletionEvent = new AutoResetEvent(false);
				this.backgroundTimer = new System.Timers.Timer();
				this.backgroundTimer.AutoReset = false;
				this.backgroundTimer.Interval = MapiHttpClient.backgroundWakeupPeriod.TotalMilliseconds;
				this.backgroundTimer.Elapsed += this.BackgroundTimerEvent;
				this.backgroundTimer.Start();
				disposeGuard.Success();
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00004840 File Offset: 0x00002A40
		// (set) Token: 0x0600005F RID: 95 RVA: 0x00004848 File Offset: 0x00002A48
		public WebHeaderCollection LastHttpResponseHeaders { get; protected set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00004851 File Offset: 0x00002A51
		// (set) Token: 0x06000061 RID: 97 RVA: 0x00004859 File Offset: 0x00002A59
		public WebHeaderCollection LastHttpRequestHeaders { get; protected set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00004862 File Offset: 0x00002A62
		// (set) Token: 0x06000063 RID: 99 RVA: 0x0000486A File Offset: 0x00002A6A
		public HttpStatusCode? LastResponseStatusCode { get; protected set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00004873 File Offset: 0x00002A73
		// (set) Token: 0x06000065 RID: 101 RVA: 0x0000487B File Offset: 0x00002A7B
		public string LastResponseStatusDescription { get; protected set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000066 RID: 102
		internal abstract string VdirPath { get; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00004884 File Offset: 0x00002A84
		protected IntPtr[] ContextHandles
		{
			get
			{
				return this.contextCache.Keys.ToArray<IntPtr>();
			}
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00004898 File Offset: 0x00002A98
		public bool TryGetContextInfo(IntPtr contextHandle, out MapiHttpContextInfo contextInfo)
		{
			base.CheckDisposed();
			contextInfo = null;
			ClientSessionContext clientSessionContext;
			if (this.TryGetContext(contextHandle, out clientSessionContext))
			{
				contextInfo = new MapiHttpContextInfo(clientSessionContext);
				return true;
			}
			return false;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x000048C4 File Offset: 0x00002AC4
		public void SetAdditionalRequestHeaders(IntPtr contextHandle, HttpWebRequest request)
		{
			ClientSessionContext clientSessionContext;
			if (this.TryGetContext(contextHandle, out clientSessionContext))
			{
				clientSessionContext.SetAdditionalRequestHeaders(request);
			}
		}

		// Token: 0x0600006A RID: 106 RVA: 0x000048E4 File Offset: 0x00002AE4
		internal HttpWebRequest CreateRequest(IntPtr contextHandle, out string requestId)
		{
			ClientSessionContext clientSessionContext;
			if (!this.TryGetContext(contextHandle, out clientSessionContext))
			{
				throw ProtocolException.FromResponseCode((LID)45788, string.Format("Context handle {0} is invalid", contextHandle), ResponseCode.ContextNotFound, null);
			}
			return clientSessionContext.CreateRequest(out requestId);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00004924 File Offset: 0x00002B24
		protected ICancelableAsyncResult BeginWrapper<T>(IntPtr contextHandle, bool needNewContext, Func<ClientSessionContext, T> beginFuncToWrap) where T : ClientAsyncOperation
		{
			ClientSessionContext arg;
			if (needNewContext)
			{
				arg = new ClientSessionContext(this.bindingInfo, this.VdirPath, contextHandle);
			}
			else if (!this.TryGetContext(contextHandle, out arg))
			{
				throw ProtocolException.FromResponseCode((LID)55840, string.Format("Context handle {0} is invalid", contextHandle), ResponseCode.ContextNotFound, null);
			}
			return beginFuncToWrap(arg);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00004980 File Offset: 0x00002B80
		protected ErrorCode EndWrapper<T>(ICancelableAsyncResult asyncResult, bool dropContextOnSuccess, bool dropContextOnFailure, out IntPtr contextHandle, Func<T, ErrorCode> endFuncToWrap) where T : ClientAsyncOperation
		{
			T arg = (T)((object)asyncResult);
			bool flag = false;
			bool flag2 = false;
			ErrorCode result;
			try
			{
				ClientAsyncOperation clientAsyncOperation = (ClientAsyncOperation)asyncResult;
				this.LastHttpResponseHeaders = clientAsyncOperation.HttpWebResponseHeaders;
				this.LastResponseStatusCode = new HttpStatusCode?(clientAsyncOperation.LastResponseStatusCode);
				this.LastResponseStatusDescription = clientAsyncOperation.LastResponseStatusDescription;
				this.LastHttpRequestHeaders = clientAsyncOperation.HttpWebRequestHeaders;
				ErrorCode errorCode = endFuncToWrap(arg);
				contextHandle = arg.Context.ContextHandle;
				if (errorCode == ErrorCode.None)
				{
					if (dropContextOnSuccess)
					{
						flag = true;
					}
					else
					{
						this.AddContext(arg.Context);
					}
				}
				else if (dropContextOnFailure)
				{
					flag = true;
				}
				result = errorCode;
			}
			catch (AggregateException exception)
			{
				if (exception.FindException<ContextNotFoundException>() != null)
				{
					flag = true;
				}
				else if (exception.FindException<ProtocolTransportException>() != null)
				{
					flag2 = true;
				}
				throw;
			}
			catch (ContextNotFoundException)
			{
				flag = true;
				throw;
			}
			catch (ProtocolTransportException)
			{
				flag2 = true;
				throw;
			}
			finally
			{
				if (flag)
				{
					if (arg.Context.ContextHandle != IntPtr.Zero)
					{
						this.RemoveContext(arg.Context.ContextHandle);
					}
					contextHandle = IntPtr.Zero;
				}
				if (flag2)
				{
					arg.Context.Reset();
				}
			}
			return result;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00004ADC File Offset: 0x00002CDC
		protected IntPtr CreateNewContextHandle(Func<IntPtr, IntPtr> contextHandleModifier)
		{
			IntPtr intPtr;
			ClientSessionContext clientSessionContext;
			do
			{
				intPtr = new IntPtr(Interlocked.Increment(ref this.contextHandleCounter));
				if (contextHandleModifier != null)
				{
					intPtr = contextHandleModifier(intPtr);
				}
			}
			while (this.TryGetContext(intPtr, out clientSessionContext));
			return intPtr;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00004B14 File Offset: 0x00002D14
		protected override void InternalDispose()
		{
			lock (this.backgroundTimerDisposeLock)
			{
				Util.DisposeIfPresent(this.backgroundTimer);
				this.isTimerDisposed = true;
				if (!this.isBackgroundProcessing)
				{
					Util.DisposeIfPresent(this.pingCompletionEvent);
				}
			}
			IntPtr[] array = this.contextCache.Keys.ToArray<IntPtr>();
			foreach (IntPtr contextHandle in array)
			{
				this.RemoveContext(contextHandle);
			}
			base.InternalDispose();
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00004BB8 File Offset: 0x00002DB8
		private void AddContext(ClientSessionContext context)
		{
			if (this.contextCache.TryAdd(context.ContextHandle, context))
			{
				ExTraceGlobals.ClientSessionContextTracer.TraceInformation<IntPtr, string>(49568, 0L, "MapiHttpClient: Added new session context to context cache; ContextHandle={0}, RequestGroupId={1}", context.ContextHandle, context.RequestGroupId);
			}
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00004BF0 File Offset: 0x00002DF0
		private bool TryGetContext(IntPtr contextHandle, out ClientSessionContext context)
		{
			return this.contextCache.TryGetValue(contextHandle, out context);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00004C00 File Offset: 0x00002E00
		private void RemoveContext(IntPtr contextHandle)
		{
			ClientSessionContext clientSessionContext;
			if (this.contextCache.TryRemove(contextHandle, out clientSessionContext))
			{
				clientSessionContext.Reset();
				ExTraceGlobals.ClientSessionContextTracer.TraceInformation<IntPtr, string>(49056, 0L, "MapiHttpClient: Removed session context from context cache; ContextHandle={0}, RequestGroupId={1}", clientSessionContext.ContextHandle, clientSessionContext.RequestGroupId);
			}
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00004C48 File Offset: 0x00002E48
		private void BackgroundTimerEvent(object sender, ElapsedEventArgs e)
		{
			lock (this.backgroundTimerDisposeLock)
			{
				if (this.isTimerDisposed)
				{
					return;
				}
				this.isBackgroundProcessing = true;
			}
			int tickCount = Environment.TickCount;
			try
			{
				if (this.bindingInfo.KeepContextsAlive)
				{
					foreach (KeyValuePair<IntPtr, ClientSessionContext> keyValuePair in this.contextCache)
					{
						if (this.isTimerDisposed)
						{
							break;
						}
						if (keyValuePair.Value != null && keyValuePair.Value.NeedsRefresh)
						{
							this.RefreshContext(keyValuePair.Value);
						}
					}
				}
			}
			finally
			{
				lock (this.backgroundTimerDisposeLock)
				{
					if (!this.isTimerDisposed)
					{
						TimeSpan t = TimeSpan.FromMilliseconds((double)(Environment.TickCount - tickCount));
						if (t > MapiHttpClient.backgroundWakeupPeriod - TimeSpan.FromSeconds(5.0))
						{
							t = MapiHttpClient.backgroundWakeupPeriod - TimeSpan.FromSeconds(5.0);
						}
						this.backgroundTimer.Interval = MapiHttpClient.backgroundWakeupPeriod.TotalMilliseconds - t.TotalMilliseconds;
						this.backgroundTimer.Start();
					}
					else
					{
						Util.DisposeIfPresent(this.pingCompletionEvent);
					}
					this.isBackgroundProcessing = false;
				}
			}
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00004E24 File Offset: 0x00003024
		private void RefreshContext(ClientSessionContext clientSessionContext)
		{
			try
			{
				this.pingCompletionEvent.Reset();
				ICancelableAsyncResult asyncResult = this.BeginWrapper<PingClientAsyncOperation>(clientSessionContext.ContextHandle, false, delegate(ClientSessionContext context)
				{
					PingClientAsyncOperation pingClientAsyncOperation = new PingClientAsyncOperation(context, delegate(ICancelableAsyncResult innerAsyncResult)
					{
						this.pingCompletionEvent.Set();
					}, null);
					pingClientAsyncOperation.Begin();
					return pingClientAsyncOperation;
				});
				this.pingCompletionEvent.WaitOne();
				IntPtr intPtr;
				this.EndWrapper<PingClientAsyncOperation>(asyncResult, false, false, out intPtr, (PingClientAsyncOperation operation) => operation.End());
				ExTraceGlobals.ClientSessionContextTracer.TraceInformation<IntPtr, string, ExDateTime?>(57248, 0L, "MapiHttpClient: Refreshed session context; ContextHandle={0}, RequestGroupId={1}, Expires={2}", clientSessionContext.ContextHandle, clientSessionContext.RequestGroupId, clientSessionContext.Expires);
			}
			catch (AggregateException arg)
			{
				ExTraceGlobals.ClientSessionContextTracer.TraceInformation<IntPtr, string, AggregateException>(65440, 0L, "MapiHttpClient: Failed to refreshed session context; ContextHandle={0}, RequestGroupId={1}, Exception={2}", clientSessionContext.ContextHandle, clientSessionContext.RequestGroupId, arg);
			}
			catch (ProtocolException arg2)
			{
				ExTraceGlobals.ClientSessionContextTracer.TraceInformation<IntPtr, string, ProtocolException>(40864, 0L, "MapiHttpClient: Failed to refreshed session context; ContextHandle={0}, RequestGroupId={1}, Exception={2}", clientSessionContext.ContextHandle, clientSessionContext.RequestGroupId, arg2);
			}
		}

		// Token: 0x0400002D RID: 45
		private static readonly TimeSpan backgroundWakeupPeriod = TimeSpan.FromSeconds(10.0);

		// Token: 0x0400002E RID: 46
		private readonly MapiHttpBindingInfo bindingInfo;

		// Token: 0x0400002F RID: 47
		private readonly ConcurrentDictionary<IntPtr, ClientSessionContext> contextCache = new ConcurrentDictionary<IntPtr, ClientSessionContext>();

		// Token: 0x04000030 RID: 48
		private readonly AutoResetEvent pingCompletionEvent;

		// Token: 0x04000031 RID: 49
		private readonly object backgroundTimerDisposeLock = new object();

		// Token: 0x04000032 RID: 50
		private readonly System.Timers.Timer backgroundTimer;

		// Token: 0x04000033 RID: 51
		private bool isTimerDisposed;

		// Token: 0x04000034 RID: 52
		private int contextHandleCounter;

		// Token: 0x04000035 RID: 53
		private bool isBackgroundProcessing;
	}
}
