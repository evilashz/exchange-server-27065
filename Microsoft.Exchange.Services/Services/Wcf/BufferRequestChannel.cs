using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000B70 RID: 2928
	public class BufferRequestChannel : ChannelBase, IReplyChannel, IChannel, ICommunicationObject
	{
		// Token: 0x17001420 RID: 5152
		// (get) Token: 0x060052DD RID: 21213 RVA: 0x0010BFC3 File Offset: 0x0010A1C3
		protected IReplyChannel InnerChannel
		{
			get
			{
				return this.innerChannel;
			}
		}

		// Token: 0x060052DE RID: 21214 RVA: 0x0010BFCB File Offset: 0x0010A1CB
		public BufferRequestChannel(ChannelManagerBase channelManager, IReplyChannel innerChannel) : base(channelManager)
		{
			if (innerChannel == null)
			{
				throw new ArgumentNullException("innerChannel");
			}
			this.innerChannel = innerChannel;
		}

		// Token: 0x060052DF RID: 21215 RVA: 0x0010BFE9 File Offset: 0x0010A1E9
		public IAsyncResult BeginReceiveRequest(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.innerChannel.BeginReceiveRequest(timeout, callback, state);
		}

		// Token: 0x060052E0 RID: 21216 RVA: 0x0010BFF9 File Offset: 0x0010A1F9
		public IAsyncResult BeginReceiveRequest(AsyncCallback callback, object state)
		{
			return this.BeginReceiveRequest(base.DefaultReceiveTimeout, callback, state);
		}

		// Token: 0x060052E1 RID: 21217 RVA: 0x0010C009 File Offset: 0x0010A209
		public IAsyncResult BeginTryReceiveRequest(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new BufferRequestChannel.OnTryReceiveRequestAsyncResult(this, timeout, callback, state);
		}

		// Token: 0x060052E2 RID: 21218 RVA: 0x0010C014 File Offset: 0x0010A214
		public bool EndTryReceiveRequest(IAsyncResult result, out RequestContext context)
		{
			return BufferRequestChannel.OnTryReceiveRequestAsyncResult.End(result, out context);
		}

		// Token: 0x060052E3 RID: 21219 RVA: 0x0010C01D File Offset: 0x0010A21D
		public IAsyncResult BeginWaitForRequest(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.innerChannel.BeginWaitForRequest(timeout, callback, state);
		}

		// Token: 0x060052E4 RID: 21220 RVA: 0x0010C02D File Offset: 0x0010A22D
		public RequestContext EndReceiveRequest(IAsyncResult result)
		{
			return this.innerChannel.EndReceiveRequest(result);
		}

		// Token: 0x060052E5 RID: 21221 RVA: 0x0010C03B File Offset: 0x0010A23B
		public bool EndWaitForRequest(IAsyncResult result)
		{
			return this.innerChannel.EndWaitForRequest(result);
		}

		// Token: 0x060052E6 RID: 21222 RVA: 0x0010C049 File Offset: 0x0010A249
		public RequestContext ReceiveRequest(TimeSpan timeout)
		{
			return this.innerChannel.ReceiveRequest(timeout);
		}

		// Token: 0x060052E7 RID: 21223 RVA: 0x0010C057 File Offset: 0x0010A257
		public RequestContext ReceiveRequest()
		{
			return this.innerChannel.ReceiveRequest();
		}

		// Token: 0x060052E8 RID: 21224 RVA: 0x0010C064 File Offset: 0x0010A264
		public bool TryReceiveRequest(TimeSpan timeout, out RequestContext context)
		{
			return this.innerChannel.TryReceiveRequest(timeout, out context);
		}

		// Token: 0x060052E9 RID: 21225 RVA: 0x0010C073 File Offset: 0x0010A273
		public bool WaitForRequest(TimeSpan timeout)
		{
			return this.innerChannel.WaitForRequest(timeout);
		}

		// Token: 0x060052EA RID: 21226 RVA: 0x0010C081 File Offset: 0x0010A281
		public override T GetProperty<T>()
		{
			return this.innerChannel.GetProperty<T>();
		}

		// Token: 0x060052EB RID: 21227 RVA: 0x0010C08E File Offset: 0x0010A28E
		protected override void OnAbort()
		{
			this.innerChannel.Abort();
		}

		// Token: 0x060052EC RID: 21228 RVA: 0x0010C09B File Offset: 0x0010A29B
		protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.innerChannel.BeginClose(timeout, callback, state);
		}

		// Token: 0x060052ED RID: 21229 RVA: 0x0010C0AB File Offset: 0x0010A2AB
		protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.innerChannel.BeginOpen(timeout, callback, state);
		}

		// Token: 0x060052EE RID: 21230 RVA: 0x0010C0BB File Offset: 0x0010A2BB
		protected override void OnClose(TimeSpan timeout)
		{
			this.innerChannel.Close(timeout);
		}

		// Token: 0x060052EF RID: 21231 RVA: 0x0010C0C9 File Offset: 0x0010A2C9
		protected override void OnEndClose(IAsyncResult result)
		{
			this.innerChannel.EndClose(result);
		}

		// Token: 0x060052F0 RID: 21232 RVA: 0x0010C0D7 File Offset: 0x0010A2D7
		protected override void OnEndOpen(IAsyncResult result)
		{
			this.innerChannel.EndOpen(result);
		}

		// Token: 0x060052F1 RID: 21233 RVA: 0x0010C0E5 File Offset: 0x0010A2E5
		protected override void OnOpen(TimeSpan timeout)
		{
			this.innerChannel.Open(timeout);
		}

		// Token: 0x17001421 RID: 5153
		// (get) Token: 0x060052F2 RID: 21234 RVA: 0x0010C0F3 File Offset: 0x0010A2F3
		public EndpointAddress LocalAddress
		{
			get
			{
				return this.innerChannel.LocalAddress;
			}
		}

		// Token: 0x060052F3 RID: 21235 RVA: 0x0010C100 File Offset: 0x0010A300
		private RequestContext BufferMessageAndWrapContext(RequestContext requestContext)
		{
			BufferRequestChannel.WrappingRequestContext wrappingRequestContext = new BufferRequestChannel.WrappingRequestContext(requestContext, this);
			wrappingRequestContext.BufferMessage();
			return wrappingRequestContext;
		}

		// Token: 0x04002E1A RID: 11802
		private IReplyChannel innerChannel;

		// Token: 0x02000B71 RID: 2929
		private class OnTryReceiveRequestAsyncResult : AsyncResultBase
		{
			// Token: 0x060052F4 RID: 21236 RVA: 0x0010C11C File Offset: 0x0010A31C
			public OnTryReceiveRequestAsyncResult(BufferRequestChannel channel, TimeSpan timeout, AsyncCallback callback, object state) : base(callback, state)
			{
				if (channel == null)
				{
					throw new ArgumentException("channel");
				}
				this.channel = channel;
				IAsyncResult asyncResult = this.channel.InnerChannel.BeginTryReceiveRequest(timeout, BufferRequestChannel.OnTryReceiveRequestAsyncResult.onTryReceiveRequest, this);
				if (asyncResult.CompletedSynchronously)
				{
					this.tryReceiveRequestSuccess = this.channel.InnerChannel.EndTryReceiveRequest(asyncResult, out this.requestContext);
					if (this.tryReceiveRequestSuccess && this.requestContext != null)
					{
						ThreadPool.QueueUserWorkItem(BufferRequestChannel.OnTryReceiveRequestAsyncResult.enqueueBufferMessageAndWrapContext, this);
						return;
					}
					base.Complete(true);
				}
			}

			// Token: 0x060052F5 RID: 21237 RVA: 0x0010C1A8 File Offset: 0x0010A3A8
			private static void OnTryReceiveRequest(IAsyncResult result)
			{
				if (result.CompletedSynchronously)
				{
					return;
				}
				BufferRequestChannel.OnTryReceiveRequestAsyncResult onTryReceiveRequestAsyncResult = (BufferRequestChannel.OnTryReceiveRequestAsyncResult)result.AsyncState;
				try
				{
					onTryReceiveRequestAsyncResult.HandleTryReceiveRequest(result);
					onTryReceiveRequestAsyncResult.Complete(false);
				}
				catch (Exception ex)
				{
					if (onTryReceiveRequestAsyncResult.requestContext != null && onTryReceiveRequestAsyncResult.requestContext.RequestMessage != null)
					{
						onTryReceiveRequestAsyncResult.requestContext.RequestMessage.Properties["WS_WcfDelayedExceptionKey"] = ex;
						onTryReceiveRequestAsyncResult.Complete(false);
					}
					else
					{
						onTryReceiveRequestAsyncResult.Complete(false, ex);
					}
				}
			}

			// Token: 0x060052F6 RID: 21238 RVA: 0x0010C230 File Offset: 0x0010A430
			private void HandleTryReceiveRequest(IAsyncResult result)
			{
				this.tryReceiveRequestSuccess = this.channel.InnerChannel.EndTryReceiveRequest(result, out this.requestContext);
				if (this.tryReceiveRequestSuccess && this.requestContext != null)
				{
					this.requestContext = this.channel.BufferMessageAndWrapContext(this.requestContext);
				}
			}

			// Token: 0x060052F7 RID: 21239 RVA: 0x0010C284 File Offset: 0x0010A484
			private static void EnqueueBufferMessageAndWrapContext(object state)
			{
				BufferRequestChannel.OnTryReceiveRequestAsyncResult onTryReceiveRequestAsyncResult = (BufferRequestChannel.OnTryReceiveRequestAsyncResult)state;
				bool flag = true;
				try
				{
					onTryReceiveRequestAsyncResult.requestContext = onTryReceiveRequestAsyncResult.channel.BufferMessageAndWrapContext(onTryReceiveRequestAsyncResult.requestContext);
					flag = false;
					onTryReceiveRequestAsyncResult.Complete(false);
				}
				catch (Exception ex)
				{
					if (onTryReceiveRequestAsyncResult.requestContext != null && onTryReceiveRequestAsyncResult.requestContext.RequestMessage != null)
					{
						onTryReceiveRequestAsyncResult.requestContext.RequestMessage.Properties["WS_WcfDelayedExceptionKey"] = ex;
						if (flag)
						{
							onTryReceiveRequestAsyncResult.Complete(false);
						}
					}
					else if (flag)
					{
						onTryReceiveRequestAsyncResult.Complete(false, ex);
					}
				}
			}

			// Token: 0x060052F8 RID: 21240 RVA: 0x0010C318 File Offset: 0x0010A518
			public static bool End(IAsyncResult result, out RequestContext requestContext)
			{
				BufferRequestChannel.OnTryReceiveRequestAsyncResult onTryReceiveRequestAsyncResult = AsyncResultBase.End<BufferRequestChannel.OnTryReceiveRequestAsyncResult>(result);
				requestContext = onTryReceiveRequestAsyncResult.requestContext;
				return onTryReceiveRequestAsyncResult.tryReceiveRequestSuccess;
			}

			// Token: 0x04002E1B RID: 11803
			private BufferRequestChannel channel;

			// Token: 0x04002E1C RID: 11804
			private bool tryReceiveRequestSuccess;

			// Token: 0x04002E1D RID: 11805
			private RequestContext requestContext;

			// Token: 0x04002E1E RID: 11806
			private static AsyncCallback onTryReceiveRequest = new AsyncCallback(BufferRequestChannel.OnTryReceiveRequestAsyncResult.OnTryReceiveRequest);

			// Token: 0x04002E1F RID: 11807
			private static WaitCallback enqueueBufferMessageAndWrapContext = new WaitCallback(BufferRequestChannel.OnTryReceiveRequestAsyncResult.EnqueueBufferMessageAndWrapContext);
		}

		// Token: 0x02000B72 RID: 2930
		private class WrappingRequestContext : RequestContext
		{
			// Token: 0x060052FA RID: 21242 RVA: 0x0010C35E File Offset: 0x0010A55E
			public WrappingRequestContext(RequestContext innerContext, BufferRequestChannel channel)
			{
				this.innerContext = innerContext;
				this.channel = channel;
				this.message = this.innerContext.RequestMessage;
			}

			// Token: 0x060052FB RID: 21243 RVA: 0x0010C385 File Offset: 0x0010A585
			internal void BufferMessage()
			{
				if (this.innerContext.RequestMessage.State == MessageState.Created)
				{
					this.message = this.innerContext.RequestMessage.CreateBufferedCopy(int.MaxValue).CreateMessage();
				}
			}

			// Token: 0x060052FC RID: 21244 RVA: 0x0010C3B9 File Offset: 0x0010A5B9
			public override void Abort()
			{
				this.innerContext.Abort();
			}

			// Token: 0x060052FD RID: 21245 RVA: 0x0010C3C6 File Offset: 0x0010A5C6
			public override IAsyncResult BeginReply(Message message, TimeSpan timeout, AsyncCallback callback, object state)
			{
				return this.innerContext.BeginReply(message, timeout, callback, state);
			}

			// Token: 0x060052FE RID: 21246 RVA: 0x0010C3D8 File Offset: 0x0010A5D8
			public override IAsyncResult BeginReply(Message message, AsyncCallback callback, object state)
			{
				return this.BeginReply(message, this.channel.DefaultSendTimeout, callback, state);
			}

			// Token: 0x060052FF RID: 21247 RVA: 0x0010C3EE File Offset: 0x0010A5EE
			public override void Close(TimeSpan timeout)
			{
				this.innerContext.Close(timeout);
			}

			// Token: 0x06005300 RID: 21248 RVA: 0x0010C3FC File Offset: 0x0010A5FC
			public override void Close()
			{
				this.innerContext.Close();
			}

			// Token: 0x06005301 RID: 21249 RVA: 0x0010C409 File Offset: 0x0010A609
			public override void EndReply(IAsyncResult result)
			{
				this.innerContext.EndReply(result);
			}

			// Token: 0x06005302 RID: 21250 RVA: 0x0010C417 File Offset: 0x0010A617
			public override void Reply(Message message, TimeSpan timeout)
			{
				this.innerContext.Reply(message, timeout);
			}

			// Token: 0x06005303 RID: 21251 RVA: 0x0010C426 File Offset: 0x0010A626
			public override void Reply(Message message)
			{
				this.Reply(message, this.channel.DefaultSendTimeout);
			}

			// Token: 0x17001422 RID: 5154
			// (get) Token: 0x06005304 RID: 21252 RVA: 0x0010C43A File Offset: 0x0010A63A
			public override Message RequestMessage
			{
				get
				{
					return this.message;
				}
			}

			// Token: 0x04002E20 RID: 11808
			private RequestContext innerContext;

			// Token: 0x04002E21 RID: 11809
			private BufferRequestChannel channel;

			// Token: 0x04002E22 RID: 11810
			private Message message;
		}
	}
}
