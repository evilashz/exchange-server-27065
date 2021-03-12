using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Internal.MExRuntime;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.Protocols.Smtp;

namespace Microsoft.Exchange.Transport.Extensibility
{
	// Token: 0x0200030B RID: 779
	internal class SmtpAgentSession : ISmtpAgentSession
	{
		// Token: 0x060021E2 RID: 8674 RVA: 0x0008031B File Offset: 0x0007E51B
		public SmtpAgentSession(IMExRuntime mexRuntime, ICloneableInternal smtpReceiveServer, ISmtpInSession smtpInSession, SmtpSession smtpSession) : this(mexRuntime, smtpReceiveServer, smtpSession)
		{
			ArgumentValidator.ThrowIfNull("smtpInSession", smtpInSession);
			this.smtpInSession = smtpInSession;
			this.smtpInSession.MexSession = this.mexSession;
		}

		// Token: 0x060021E3 RID: 8675 RVA: 0x0008034A File Offset: 0x0007E54A
		public SmtpAgentSession(IMExRuntime mexRuntime, ICloneableInternal smtpReceiveServer, SmtpInSessionState sessionState, out IMExSession mexSession) : this(mexRuntime, smtpReceiveServer, sessionState)
		{
			ArgumentValidator.ThrowIfNull("sessionState", sessionState);
			mexSession = this.mexSession;
			this.sessionState = sessionState;
			this.sessionState.MexRuntimeSession = this.mexSession;
		}

		// Token: 0x060021E4 RID: 8676 RVA: 0x00080384 File Offset: 0x0007E584
		private SmtpAgentSession(IMExRuntime mexRuntime, ICloneableInternal smtpReceiveServer, SmtpSession smtpSession)
		{
			ArgumentValidator.ThrowIfNull("mexRuntime", mexRuntime);
			ArgumentValidator.ThrowIfNull("smtpReceiveServer", smtpReceiveServer);
			ArgumentValidator.ThrowIfNull("smtpSession", smtpSession);
			this.mexSession = this.CreaateMExSession(mexRuntime, smtpReceiveServer);
			this.smtpSession = smtpSession;
			this.smtpSession.ExecutionControl = this.mexSession;
			this.latencyTracker = new AgentLatencyTracker(this.mexSession);
		}

		// Token: 0x060021E5 RID: 8677 RVA: 0x000803F0 File Offset: 0x0007E5F0
		public IAsyncResult BeginNoEvent(AsyncCallback callback, object state)
		{
			IAsyncResult asyncResult = new SmtpAgentSession.NoEventAsyncResult(state);
			callback(asyncResult);
			return asyncResult;
		}

		// Token: 0x060021E6 RID: 8678 RVA: 0x0008040C File Offset: 0x0007E60C
		public IAsyncResult BeginRaiseEvent(string eventTopic, object eventSource, object eventArgs, AsyncCallback callback, object state)
		{
			return this.mexSession.BeginInvoke(eventTopic, eventSource, eventArgs, callback, state);
		}

		// Token: 0x060021E7 RID: 8679 RVA: 0x00080420 File Offset: 0x0007E620
		public SmtpResponse EndRaiseEvent(IAsyncResult ar)
		{
			if (ar == null || ar is SmtpAgentSession.NoEventAsyncResult)
			{
				return SmtpResponse.Empty;
			}
			try
			{
				this.mexSession.EndInvoke(ar);
			}
			catch (Exception e)
			{
				SmtpResponse result;
				if (!this.HandleEndInvokeException(e, out result))
				{
					throw;
				}
				return result;
			}
			return SmtpResponse.Empty;
		}

		// Token: 0x060021E8 RID: 8680 RVA: 0x00080478 File Offset: 0x0007E678
		public Task<SmtpResponse> RaiseEventAsync(string eventTopic, object eventSource, object eventArgs)
		{
			return Task.Factory.FromAsync<string, object, object, SmtpResponse>(new Func<string, object, object, AsyncCallback, object, IAsyncResult>(this.BeginRaiseEvent), new Func<IAsyncResult, SmtpResponse>(this.EndRaiseEvent), eventTopic, eventSource, eventArgs, null);
		}

		// Token: 0x17000AD0 RID: 2768
		// (get) Token: 0x060021E9 RID: 8681 RVA: 0x000804A0 File Offset: 0x0007E6A0
		public SmtpSession SessionSource
		{
			get
			{
				return this.smtpSession;
			}
		}

		// Token: 0x17000AD1 RID: 2769
		// (get) Token: 0x060021EA RID: 8682 RVA: 0x000804A8 File Offset: 0x0007E6A8
		public AgentLatencyTracker LatencyTracker
		{
			get
			{
				return this.latencyTracker;
			}
		}

		// Token: 0x060021EB RID: 8683 RVA: 0x000804B0 File Offset: 0x0007E6B0
		public void Close()
		{
			this.latencyTracker.Dispose();
			this.latencyTracker = null;
			this.mexSession.Close();
		}

		// Token: 0x060021EC RID: 8684 RVA: 0x000804CF File Offset: 0x0007E6CF
		protected virtual bool HandleEndInvokeException(Exception e, out SmtpResponse response)
		{
			return SmtpInExceptionHandler.ShouldHandleException(e, ExTraceGlobals.SmtpReceiveTracer, null, out response);
		}

		// Token: 0x060021ED RID: 8685 RVA: 0x000804E0 File Offset: 0x0007E6E0
		private void TrackAsyncMessage()
		{
			TransportMailItem transportMailItem = this.TransportMailItem;
			if (transportMailItem != null)
			{
				TransportMailItem.TrackAsyncMessage(transportMailItem.InternetMessageId);
			}
		}

		// Token: 0x060021EE RID: 8686 RVA: 0x00080504 File Offset: 0x0007E704
		private void TrackAsyncMessageCompleted()
		{
			TransportMailItem transportMailItem = this.TransportMailItem;
			if (transportMailItem != null)
			{
				TransportMailItem.TrackAsyncMessageCompleted(transportMailItem.InternetMessageId);
			}
		}

		// Token: 0x060021EF RID: 8687 RVA: 0x00080526 File Offset: 0x0007E726
		private IMExSession CreaateMExSession(IMExRuntime mexRuntime, ICloneableInternal smtpReceiveServer)
		{
			return mexRuntime.CreateSession(smtpReceiveServer, "SMTP", new Action(this.TrackAsyncMessage), new Action(this.TrackAsyncMessageCompleted), null);
		}

		// Token: 0x17000AD2 RID: 2770
		// (get) Token: 0x060021F0 RID: 8688 RVA: 0x0008054D File Offset: 0x0007E74D
		private TransportMailItem TransportMailItem
		{
			get
			{
				if (this.smtpInSession != null)
				{
					return this.smtpInSession.TransportMailItem;
				}
				return this.sessionState.TransportMailItem;
			}
		}

		// Token: 0x040011C3 RID: 4547
		private AgentLatencyTracker latencyTracker;

		// Token: 0x040011C4 RID: 4548
		private readonly IMExSession mexSession;

		// Token: 0x040011C5 RID: 4549
		private readonly ISmtpInSession smtpInSession;

		// Token: 0x040011C6 RID: 4550
		private readonly SmtpInSessionState sessionState;

		// Token: 0x040011C7 RID: 4551
		private readonly SmtpSession smtpSession;

		// Token: 0x0200030C RID: 780
		private class NoEventAsyncResult : IAsyncResult
		{
			// Token: 0x060021F1 RID: 8689 RVA: 0x0008056E File Offset: 0x0007E76E
			public NoEventAsyncResult(object state)
			{
				this.state = state;
			}

			// Token: 0x17000AD3 RID: 2771
			// (get) Token: 0x060021F2 RID: 8690 RVA: 0x0008057D File Offset: 0x0007E77D
			public object AsyncState
			{
				get
				{
					return this.state;
				}
			}

			// Token: 0x17000AD4 RID: 2772
			// (get) Token: 0x060021F3 RID: 8691 RVA: 0x00080585 File Offset: 0x0007E785
			public bool CompletedSynchronously
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000AD5 RID: 2773
			// (get) Token: 0x060021F4 RID: 8692 RVA: 0x00080588 File Offset: 0x0007E788
			public WaitHandle AsyncWaitHandle
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x17000AD6 RID: 2774
			// (get) Token: 0x060021F5 RID: 8693 RVA: 0x0008058F File Offset: 0x0007E78F
			public bool IsCompleted
			{
				get
				{
					return true;
				}
			}

			// Token: 0x040011C8 RID: 4552
			private readonly object state;
		}
	}
}
