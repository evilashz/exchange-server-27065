using System;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.AddressBook.Service;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Nspi.Rfri;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Exchange.RpcClientAccess.Server;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.AddressBook.Service
{
	// Token: 0x02000047 RID: 71
	internal sealed class RfriAsyncDispatch : IRfriAsyncDispatch
	{
		// Token: 0x06000305 RID: 773 RVA: 0x00012E38 File Offset: 0x00011038
		public ICancelableAsyncResult BeginGetNewDSA(ProtocolRequestInfo protocolRequestInfo, ClientBinding clientBinding, RfriGetNewDSAFlags flags, string userDn, CancelableAsyncCallback asyncCallback, object asyncState)
		{
			return this.BeginWrapper("BeginGetNewDSA", asyncCallback, asyncState, clientBinding, userDn, (RfriContext context) => new RfriGetNewDSADispatchTask(asyncCallback, asyncState, protocolRequestInfo, clientBinding, context, flags, userDn));
		}

		// Token: 0x06000306 RID: 774 RVA: 0x00012EC4 File Offset: 0x000110C4
		public RfriStatus EndGetNewDSA(ICancelableAsyncResult asyncResult, out string serverDn)
		{
			string localServerDn = null;
			RfriStatus result;
			try
			{
				result = this.EndWrapper("EndGetNewDSA", asyncResult, (RfriDispatchTask task) => ((RfriGetNewDSADispatchTask)task).End(out localServerDn));
			}
			finally
			{
				serverDn = localServerDn;
			}
			return result;
		}

		// Token: 0x06000307 RID: 775 RVA: 0x00012F50 File Offset: 0x00011150
		public ICancelableAsyncResult BeginGetFQDNFromLegacyDN(ProtocolRequestInfo protocolRequestInfo, ClientBinding clientBinding, RfriGetFQDNFromLegacyDNFlags flags, string serverDn, CancelableAsyncCallback asyncCallback, object asyncState)
		{
			return this.BeginWrapper("BeginGetFQDNFromLegacyDN", asyncCallback, asyncState, clientBinding, serverDn, (RfriContext context) => new RfriGetFQDNFromLegacyDNDispatchTask(asyncCallback, asyncState, protocolRequestInfo, clientBinding, context, flags, serverDn));
		}

		// Token: 0x06000308 RID: 776 RVA: 0x00012FDC File Offset: 0x000111DC
		public RfriStatus EndGetFQDNFromLegacyDN(ICancelableAsyncResult asyncResult, out string serverFqdn)
		{
			string localServerFqdn = null;
			RfriStatus result;
			try
			{
				result = this.EndWrapper("EndGetFQDNFromLegacyDN", asyncResult, (RfriDispatchTask task) => ((RfriGetFQDNFromLegacyDNDispatchTask)task).End(out localServerFqdn));
			}
			finally
			{
				serverFqdn = localServerFqdn;
			}
			return result;
		}

		// Token: 0x06000309 RID: 777 RVA: 0x00013070 File Offset: 0x00011270
		public ICancelableAsyncResult BeginGetAddressBookUrl(ProtocolRequestInfo protocolRequestInfo, ClientBinding clientBinding, RfriGetAddressBookUrlFlags flags, string hostname, string userDn, CancelableAsyncCallback asyncCallback, object asyncState)
		{
			return this.BeginWrapper("BeginGetAddressBookUrl", asyncCallback, asyncState, clientBinding, userDn, (RfriContext context) => new RfriGetAddressBookUrlDispatchTask(asyncCallback, asyncState, protocolRequestInfo, clientBinding, context, flags, hostname, userDn));
		}

		// Token: 0x0600030A RID: 778 RVA: 0x00013104 File Offset: 0x00011304
		public RfriStatus EndGetAddressBookUrl(ICancelableAsyncResult asyncResult, out string serverUrl)
		{
			string localServerUrl = null;
			RfriStatus result;
			try
			{
				result = this.EndWrapper("EndGetAddressBookUrl", asyncResult, (RfriDispatchTask task) => ((RfriGetAddressBookUrlDispatchTask)task).End(out localServerUrl));
			}
			finally
			{
				serverUrl = localServerUrl;
			}
			return result;
		}

		// Token: 0x0600030B RID: 779 RVA: 0x00013198 File Offset: 0x00011398
		public ICancelableAsyncResult BeginGetMailboxUrl(ProtocolRequestInfo protocolRequestInfo, ClientBinding clientBinding, RfriGetMailboxUrlFlags flags, string hostname, string serverDn, CancelableAsyncCallback asyncCallback, object asyncState)
		{
			return this.BeginWrapper("BeginGetMailboxUrl", asyncCallback, asyncState, clientBinding, serverDn, (RfriContext context) => new RfriGetMailboxUrlDispatchTask(asyncCallback, asyncState, protocolRequestInfo, clientBinding, context, flags, hostname, serverDn));
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0001322C File Offset: 0x0001142C
		public RfriStatus EndGetMailboxUrl(ICancelableAsyncResult asyncResult, out string serverUrl)
		{
			string localServerUrl = null;
			RfriStatus result;
			try
			{
				result = this.EndWrapper("EndGetMailboxUrl", asyncResult, (RfriDispatchTask task) => ((RfriGetMailboxUrlDispatchTask)task).End(out localServerUrl));
			}
			finally
			{
				serverUrl = localServerUrl;
			}
			return result;
		}

		// Token: 0x0600030D RID: 781 RVA: 0x00013284 File Offset: 0x00011484
		internal void ShuttingDown()
		{
			this.isShuttingDown = true;
		}

		// Token: 0x0600030E RID: 782 RVA: 0x00013290 File Offset: 0x00011490
		private static void FailureCallback(object state)
		{
			FailureAsyncResult<RfriStatus> failureAsyncResult = (FailureAsyncResult<RfriStatus>)state;
			failureAsyncResult.InvokeCallback();
		}

		// Token: 0x0600030F RID: 783 RVA: 0x000132AC File Offset: 0x000114AC
		private static void ConditionalExceptionWrapper(bool wrapException, Action wrappedAction, Action<Exception> exceptionDelegate)
		{
			if (wrapException)
			{
				try
				{
					wrappedAction();
					return;
				}
				catch (Exception obj)
				{
					if (exceptionDelegate != null)
					{
						exceptionDelegate(obj);
					}
					throw;
				}
			}
			wrappedAction();
		}

		// Token: 0x06000310 RID: 784 RVA: 0x000132E8 File Offset: 0x000114E8
		private static RfriContext CreateRfriContext(ClientBinding clientBinding)
		{
			RfriContext rfriContext = null;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				ClientSecurityContext clientSecurityContext = null;
				bool isAnonymous = false;
				string text = null;
				string userDomain = null;
				RpcHttpConnectionProperties rpcHttpConnectionProperties = null;
				if (!RpcDispatch.TryGetAuthContextInfo(clientBinding, out clientSecurityContext, out isAnonymous, out text, out userDomain, out rpcHttpConnectionProperties))
				{
					ExTraceGlobals.ReferralTracer.TraceError<Guid>(0L, "Could not resolve anonymous user for session id: {0}", clientBinding.AssociationGuid);
					throw new RfriException(RfriStatus.LogonFailed, "Could not resolve anonymous user.");
				}
				disposeGuard.Add<ClientSecurityContext>(clientSecurityContext);
				Guid empty = Guid.Empty;
				if (rpcHttpConnectionProperties != null && rpcHttpConnectionProperties.RequestIds.Length > 0)
				{
					Guid.TryParse(rpcHttpConnectionProperties.RequestIds[rpcHttpConnectionProperties.RequestIds.Length - 1], out empty);
				}
				rfriContext = new RfriContext(clientSecurityContext, userDomain, clientBinding.ClientAddress, clientBinding.ServerAddress, clientBinding.ProtocolSequence, clientBinding.AuthenticationType.ToString(), clientBinding.IsEncrypted, isAnonymous, empty);
				disposeGuard.Add<RfriContext>(rfriContext);
				if (!rfriContext.TryAcquireBudget())
				{
					ExTraceGlobals.ReferralTracer.TraceError((long)rfriContext.ContextHandle, "Could not acquire budget");
					throw new RfriException(RfriStatus.GeneralFailure, "Failed to acquire budget.");
				}
				disposeGuard.Success();
			}
			return rfriContext;
		}

		// Token: 0x06000311 RID: 785 RVA: 0x00013414 File Offset: 0x00011614
		private void SubmitTask(RfriDispatchTask task)
		{
			this.CheckShuttingDown();
			if (!UserWorkloadManager.Singleton.TrySubmitNewTask(task))
			{
				ExTraceGlobals.ReferralTracer.TraceError((long)task.ContextHandle, "Could not submit task");
				throw new ServerTooBusyException("Unable to submit task; queue full");
			}
		}

		// Token: 0x06000312 RID: 786 RVA: 0x0001344A File Offset: 0x0001164A
		private void CheckShuttingDown()
		{
			if (this.isShuttingDown)
			{
				throw new ServerUnavailableException("Shutting down");
			}
		}

		// Token: 0x06000313 RID: 787 RVA: 0x0001366C File Offset: 0x0001186C
		private ICancelableAsyncResult BeginWrapper(string methodName, CancelableAsyncCallback asyncCallback, object asyncState, ClientBinding clientBinding, string legacyDn, Func<RfriContext, RfriDispatchTask> beginDelegate)
		{
			ICancelableAsyncResult asyncResult = null;
			RfriAsyncDispatch.ConditionalExceptionWrapper(ExTraceGlobals.ReferralTracer.IsTraceEnabled(TraceType.DebugTrace), delegate
			{
				if (ExTraceGlobals.ReferralTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					using (ClientSecurityContext clientSecurityContext = clientBinding.GetClientSecurityContext())
					{
						ExTraceGlobals.ReferralTracer.TraceDebug(0, 0L, "{0} started. LegacyDn={1}. ClientAddress={2}. ServerAddress={3}. ProtocolSequence={4}. EndPoint={5}. IsEncrypted={6}. ClientSecurityContext={7}.", new object[]
						{
							methodName,
							legacyDn,
							clientBinding.ClientAddress,
							clientBinding.ServerAddress,
							clientBinding.ProtocolSequence,
							clientBinding.ClientEndpoint,
							clientBinding.IsEncrypted,
							clientSecurityContext
						});
					}
				}
				FailureAsyncResult<RfriStatus> failureAsyncResult = null;
				this.CheckShuttingDown();
				try
				{
					using (DisposeGuard disposeGuard = default(DisposeGuard))
					{
						RfriContext rfriContext = RfriAsyncDispatch.CreateRfriContext(clientBinding);
						disposeGuard.Add<RfriContext>(rfriContext);
						RfriDispatchTask rfriDispatchTask = beginDelegate(rfriContext);
						disposeGuard.Add<RfriDispatchTask>(rfriDispatchTask);
						asyncResult = rfriDispatchTask.AsyncResult;
						this.SubmitTask(rfriDispatchTask);
						disposeGuard.Success();
					}
				}
				catch (FailRpcException ex)
				{
					failureAsyncResult = new FailureAsyncResult<RfriStatus>((RfriStatus)ex.ErrorCode, IntPtr.Zero, ex, asyncCallback, asyncState);
					asyncResult = failureAsyncResult;
				}
				catch (RfriException ex2)
				{
					failureAsyncResult = new FailureAsyncResult<RfriStatus>(ex2.Status, IntPtr.Zero, ex2, asyncCallback, asyncState);
					asyncResult = failureAsyncResult;
				}
				if (failureAsyncResult != null && !ThreadPool.QueueUserWorkItem(RfriAsyncDispatch.FailureWaitCallback, failureAsyncResult))
				{
					failureAsyncResult.InvokeCallback();
				}
				ExTraceGlobals.ReferralTracer.TraceDebug<string>(0, 0L, "{0} succeeded.", methodName);
			}, delegate(Exception exception)
			{
				ExTraceGlobals.ReferralTracer.TraceDebug<string, Exception>(0, 0L, "{0} failed. Exception={1}.", methodName, exception);
			});
			return asyncResult;
		}

		// Token: 0x06000314 RID: 788 RVA: 0x000137EC File Offset: 0x000119EC
		private RfriStatus EndWrapper(string methodName, ICancelableAsyncResult asyncResult, Func<RfriDispatchTask, RfriStatus> endDelegate)
		{
			RfriStatus rfriStatus = RfriStatus.Success;
			RfriAsyncDispatch.ConditionalExceptionWrapper(ExTraceGlobals.ReferralTracer.IsTraceEnabled(TraceType.DebugTrace), delegate
			{
				DispatchTaskAsyncResult dispatchTaskAsyncResult = asyncResult as DispatchTaskAsyncResult;
				if (dispatchTaskAsyncResult != null)
				{
					RfriDispatchTask rfriDispatchTask = (RfriDispatchTask)dispatchTaskAsyncResult.DispatchTask;
					using (DisposeGuard disposeGuard = default(DisposeGuard))
					{
						disposeGuard.Add<RfriDispatchTask>(rfriDispatchTask);
						rfriStatus = endDelegate(rfriDispatchTask);
					}
					ExTraceGlobals.ReferralTracer.TraceDebug<string, RfriStatus>(0, 0L, "{0} succeeded. RfriStatus={1}.", methodName, rfriStatus);
					return;
				}
				FailureAsyncResult<RfriStatus> failureAsyncResult = asyncResult as FailureAsyncResult<RfriStatus>;
				if (failureAsyncResult != null)
				{
					rfriStatus = failureAsyncResult.ErrorCode;
					ExTraceGlobals.ReferralTracer.TraceDebug<string, RfriStatus, Exception>(0, 0L, "{0} failed. RfriStatus={1}. Exception={2}.", methodName, rfriStatus, failureAsyncResult.Exception);
					return;
				}
				throw new InvalidOperationException(string.Format("Invalid IAsyncResult encountered; {0}", asyncResult));
			}, delegate(Exception exception)
			{
				ExTraceGlobals.ReferralTracer.TraceDebug<string, Exception>(0, 0L, "{0} failed. Exception={1}.", methodName, exception);
			});
			return rfriStatus;
		}

		// Token: 0x040001B6 RID: 438
		private static readonly WaitCallback FailureWaitCallback = new WaitCallback(RfriAsyncDispatch.FailureCallback);

		// Token: 0x040001B7 RID: 439
		private bool isShuttingDown;
	}
}
