using System;
using System.Security.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net.XropService;
using Microsoft.Exchange.Rpc;

namespace Microsoft.Exchange.RpcClientAccess.Server
{
	// Token: 0x02000054 RID: 84
	internal sealed class WebServiceServerSession : DisposeTrackableBase, IServerSession
	{
		// Token: 0x06000301 RID: 769 RVA: 0x000100BA File Offset: 0x0000E2BA
		public WebServiceServerSession(WindowsIdentity userIdentity, WebServiceUserInformation userInformation, IExchangeAsyncDispatch asyncDispatch)
		{
			this.asyncDispatch = asyncDispatch;
			this.userIdentity = userIdentity;
			this.userInformation = userInformation;
			this.contextHandle = IntPtr.Zero;
		}

		// Token: 0x06000302 RID: 770 RVA: 0x000100E4 File Offset: 0x0000E2E4
		public IAsyncResult BeginConnect(ConnectRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			WebServiceCallStateConnect webServiceCallStateConnect = new WebServiceCallStateConnect(this.userInformation, this.asyncDispatch, asyncCallback, asyncState, this.contextHandle, this.userIdentity);
			return webServiceCallStateConnect.Begin(request);
		}

		// Token: 0x06000303 RID: 771 RVA: 0x00010118 File Offset: 0x0000E318
		public ConnectResponse EndConnect(IAsyncResult asyncResult)
		{
			WebServiceCallStateConnect webServiceCallStateConnect = (WebServiceCallStateConnect)asyncResult;
			ConnectResponse result;
			try
			{
				result = webServiceCallStateConnect.End();
			}
			finally
			{
				this.contextHandle = webServiceCallStateConnect.ContextHandle;
			}
			return result;
		}

		// Token: 0x06000304 RID: 772 RVA: 0x00010154 File Offset: 0x0000E354
		public IAsyncResult BeginExecute(ExecuteRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			WebServiceCallStateExecute webServiceCallStateExecute = new WebServiceCallStateExecute(this.userInformation, this.asyncDispatch, asyncCallback, asyncState, this.contextHandle);
			return webServiceCallStateExecute.Begin(request);
		}

		// Token: 0x06000305 RID: 773 RVA: 0x00010184 File Offset: 0x0000E384
		public ExecuteResponse EndExecute(IAsyncResult asyncResult)
		{
			WebServiceCallStateExecute webServiceCallStateExecute = (WebServiceCallStateExecute)asyncResult;
			ExecuteResponse result;
			try
			{
				result = webServiceCallStateExecute.End();
			}
			finally
			{
				this.contextHandle = webServiceCallStateExecute.ContextHandle;
			}
			return result;
		}

		// Token: 0x06000306 RID: 774 RVA: 0x000101C0 File Offset: 0x0000E3C0
		public IAsyncResult BeginDisconnect(DisconnectRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			WebServiceCallStateDisconnect webServiceCallStateDisconnect = new WebServiceCallStateDisconnect(this.userInformation, this.asyncDispatch, asyncCallback, asyncState, this.contextHandle);
			return webServiceCallStateDisconnect.Begin(request);
		}

		// Token: 0x06000307 RID: 775 RVA: 0x000101F0 File Offset: 0x0000E3F0
		public DisconnectResponse EndDisconnect(IAsyncResult asyncResult)
		{
			WebServiceCallStateDisconnect webServiceCallStateDisconnect = (WebServiceCallStateDisconnect)asyncResult;
			DisconnectResponse result;
			try
			{
				result = webServiceCallStateDisconnect.End();
			}
			finally
			{
				this.contextHandle = webServiceCallStateDisconnect.ContextHandle;
			}
			return result;
		}

		// Token: 0x06000308 RID: 776 RVA: 0x0001022C File Offset: 0x0000E42C
		protected override void InternalDispose(bool disposing)
		{
			if (disposing && this.asyncDispatch != null && this.contextHandle != IntPtr.Zero)
			{
				try
				{
					this.asyncDispatch.ContextHandleRundown(this.contextHandle);
				}
				catch (RpcServiceException)
				{
				}
				catch (OutOfMemoryException)
				{
				}
				this.contextHandle = IntPtr.Zero;
			}
		}

		// Token: 0x06000309 RID: 777 RVA: 0x00010298 File Offset: 0x0000E498
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<WebServiceServerSession>(this);
		}

		// Token: 0x04000183 RID: 387
		private readonly IExchangeAsyncDispatch asyncDispatch;

		// Token: 0x04000184 RID: 388
		private readonly WindowsIdentity userIdentity;

		// Token: 0x04000185 RID: 389
		private readonly WebServiceUserInformation userInformation;

		// Token: 0x04000186 RID: 390
		private IntPtr contextHandle;
	}
}
