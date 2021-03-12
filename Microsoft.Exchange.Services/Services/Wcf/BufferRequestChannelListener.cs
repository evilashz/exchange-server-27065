using System;
using System.ServiceModel.Channels;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000B6F RID: 2927
	internal class BufferRequestChannelListener : ChannelListenerBase<IReplyChannel>
	{
		// Token: 0x060052CC RID: 21196 RVA: 0x0010BEC3 File Offset: 0x0010A0C3
		public BufferRequestChannelListener(IChannelListener<IReplyChannel> innerListener)
		{
			this.innerListener = innerListener;
		}

		// Token: 0x060052CD RID: 21197 RVA: 0x0010BED2 File Offset: 0x0010A0D2
		public override T GetProperty<T>()
		{
			return this.innerListener.GetProperty<T>();
		}

		// Token: 0x060052CE RID: 21198 RVA: 0x0010BEDF File Offset: 0x0010A0DF
		protected override IReplyChannel OnAcceptChannel(TimeSpan timeout)
		{
			return this.WrapChannel(this.innerListener.AcceptChannel(timeout));
		}

		// Token: 0x060052CF RID: 21199 RVA: 0x0010BEF3 File Offset: 0x0010A0F3
		protected override IAsyncResult OnBeginAcceptChannel(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.innerListener.BeginAcceptChannel(timeout, callback, state);
		}

		// Token: 0x060052D0 RID: 21200 RVA: 0x0010BF03 File Offset: 0x0010A103
		protected override IReplyChannel OnEndAcceptChannel(IAsyncResult result)
		{
			return this.WrapChannel(this.innerListener.EndAcceptChannel(result));
		}

		// Token: 0x060052D1 RID: 21201 RVA: 0x0010BF17 File Offset: 0x0010A117
		protected override IAsyncResult OnBeginWaitForChannel(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.innerListener.BeginWaitForChannel(timeout, callback, state);
		}

		// Token: 0x060052D2 RID: 21202 RVA: 0x0010BF27 File Offset: 0x0010A127
		protected override bool OnEndWaitForChannel(IAsyncResult result)
		{
			return this.innerListener.EndWaitForChannel(result);
		}

		// Token: 0x060052D3 RID: 21203 RVA: 0x0010BF35 File Offset: 0x0010A135
		protected override bool OnWaitForChannel(TimeSpan timeout)
		{
			return this.innerListener.WaitForChannel(timeout);
		}

		// Token: 0x1700141F RID: 5151
		// (get) Token: 0x060052D4 RID: 21204 RVA: 0x0010BF43 File Offset: 0x0010A143
		public override Uri Uri
		{
			get
			{
				return this.innerListener.Uri;
			}
		}

		// Token: 0x060052D5 RID: 21205 RVA: 0x0010BF50 File Offset: 0x0010A150
		protected override void OnAbort()
		{
			this.innerListener.Abort();
		}

		// Token: 0x060052D6 RID: 21206 RVA: 0x0010BF5D File Offset: 0x0010A15D
		protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.innerListener.BeginClose(timeout, callback, state);
		}

		// Token: 0x060052D7 RID: 21207 RVA: 0x0010BF6D File Offset: 0x0010A16D
		protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.innerListener.BeginOpen(timeout, callback, state);
		}

		// Token: 0x060052D8 RID: 21208 RVA: 0x0010BF7D File Offset: 0x0010A17D
		protected override void OnClose(TimeSpan timeout)
		{
			this.innerListener.Close(timeout);
		}

		// Token: 0x060052D9 RID: 21209 RVA: 0x0010BF8B File Offset: 0x0010A18B
		protected override void OnEndClose(IAsyncResult result)
		{
			this.innerListener.EndClose(result);
		}

		// Token: 0x060052DA RID: 21210 RVA: 0x0010BF99 File Offset: 0x0010A199
		protected override void OnEndOpen(IAsyncResult result)
		{
			this.innerListener.EndOpen(result);
		}

		// Token: 0x060052DB RID: 21211 RVA: 0x0010BFA7 File Offset: 0x0010A1A7
		protected override void OnOpen(TimeSpan timeout)
		{
			this.innerListener.Open(timeout);
		}

		// Token: 0x060052DC RID: 21212 RVA: 0x0010BFB5 File Offset: 0x0010A1B5
		private IReplyChannel WrapChannel(IReplyChannel innerChannel)
		{
			if (innerChannel == null)
			{
				return null;
			}
			return new BufferRequestChannel(this, innerChannel);
		}

		// Token: 0x04002E19 RID: 11801
		private IChannelListener<IReplyChannel> innerListener;
	}
}
