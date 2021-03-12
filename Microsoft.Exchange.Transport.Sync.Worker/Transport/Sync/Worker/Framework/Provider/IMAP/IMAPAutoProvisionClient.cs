using System;
using System.Security;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxTransport.ContentAggregation;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Common.Logging;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Pim;
using Microsoft.Exchange.Transport.Sync.Worker.Framework.Provider.IMAP.Client;

namespace Microsoft.Exchange.Transport.Sync.Worker.Framework.Provider.IMAP
{
	// Token: 0x020001E3 RID: 483
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class IMAPAutoProvisionClient : DisposeTrackableBase
	{
		// Token: 0x06000FB4 RID: 4020 RVA: 0x00030E10 File Offset: 0x0002F010
		public IMAPAutoProvisionClient(string host, int port, string username, SecureString password, IMAPAuthenticationMechanism authMechanism, IMAPSecurityMechanism secMechanism, AggregationType aggregationType, int connectionTimeout, int maxDownload, SyncLogSession logSession)
		{
			this.clientState = new IMAPClientState(new Fqdn(host), port, username, password, null, logSession, SyncUtilities.GetNextSessionId(), Guid.NewGuid(), authMechanism, secMechanism, aggregationType, (long)maxDownload, connectionTimeout, null, null, null, null);
		}

		// Token: 0x06000FB5 RID: 4021 RVA: 0x00030E54 File Offset: 0x0002F054
		public IAsyncResult BeginVerifyAccount(AsyncCallback callback, object callbackState, object syncPoisonContext)
		{
			base.CheckDisposed();
			AsyncResult<IMAPClientState, DBNull> asyncResult = new AsyncResult<IMAPClientState, DBNull>(this, this.clientState, callback, callbackState, syncPoisonContext);
			asyncResult.PendingAsyncResult = IMAPClient.BeginConnectAndAuthenticate(this.clientState, new AsyncCallback(IMAPAutoProvisionClient.OnEndConnectAndAuthenticate), asyncResult, syncPoisonContext);
			return asyncResult;
		}

		// Token: 0x06000FB6 RID: 4022 RVA: 0x00030E98 File Offset: 0x0002F098
		public AsyncOperationResult<DBNull> EndVerifyAccount(IAsyncResult asyncResult)
		{
			base.CheckDisposed();
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			AsyncResult<IMAPClientState, DBNull> asyncResult2 = (AsyncResult<IMAPClientState, DBNull>)asyncResult;
			return asyncResult2.WaitForCompletion();
		}

		// Token: 0x06000FB7 RID: 4023 RVA: 0x00030EC6 File Offset: 0x0002F0C6
		protected override void InternalDispose(bool disposing)
		{
			if (disposing && this.clientState != null)
			{
				this.clientState.Dispose();
				this.clientState = null;
			}
		}

		// Token: 0x06000FB8 RID: 4024 RVA: 0x00030EE5 File Offset: 0x0002F0E5
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<IMAPAutoProvisionClient>(this);
		}

		// Token: 0x06000FB9 RID: 4025 RVA: 0x00030EF0 File Offset: 0x0002F0F0
		private static void OnEndConnectAndAuthenticate(IAsyncResult asyncResult)
		{
			AsyncResult<IMAPClientState, DBNull> asyncResult2 = (AsyncResult<IMAPClientState, DBNull>)asyncResult.AsyncState;
			IMAPClientState state = asyncResult2.State;
			AsyncOperationResult<DBNull> asyncOperationResult = IMAPClient.EndConnectAndAuthenticate(asyncResult);
			if (asyncResult.CompletedSynchronously)
			{
				asyncResult2.SetCompletedSynchronously();
			}
			if (!asyncOperationResult.IsSucceeded)
			{
				state.Log.LogError((TSLID)676UL, "Failed to authenticate and login.", new object[0]);
				asyncResult2.ProcessCompleted(null, asyncOperationResult.Exception);
				return;
			}
			asyncResult2.PendingAsyncResult = IMAPClient.BeginLogOff(state, new AsyncCallback(IMAPAutoProvisionClient.OnEndLogOff), asyncResult2, asyncResult2.SyncPoisonContext);
			asyncResult2.ProcessCompleted();
		}

		// Token: 0x06000FBA RID: 4026 RVA: 0x00030F81 File Offset: 0x0002F181
		private static void OnEndLogOff(IAsyncResult asyncResult)
		{
		}

		// Token: 0x040008A0 RID: 2208
		private IMAPClientState clientState;
	}
}
