using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Net.Sockets;
using System.Security;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Common.IL;
using Microsoft.Exchange.Data.Internal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ContentAggregation;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.MailboxTransport.ContentAggregation.Schema;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Protocols.Smtp;
using Microsoft.Exchange.Security;
using Microsoft.Exchange.Security.Authentication;
using Microsoft.Exchange.Transport.Logging.ConnectionLog;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Common.Exceptions;
using Microsoft.Exchange.Transport.Sync.Common.Logging;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Pop;
using Microsoft.Exchange.Transport.Sync.Worker.Framework;

namespace Microsoft.Exchange.MailboxTransport.ContentAggregation
{
	// Token: 0x020001EE RID: 494
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class Pop3Client : DisposeTrackableBase, ISyncSourceSession
	{
		// Token: 0x0600104C RID: 4172 RVA: 0x00033530 File Offset: 0x00031730
		internal Pop3Client(Guid subscriptionGuid, string host, int port, string username, SecureString password, AuthenticationMechanism authMechanism, SecurityMechanism secMechanism, string userLegacyDN, int connectionTimeout, SyncLogSession logSession, EventHandler<DownloadCompleteEventArgs> downloadCompleted, EventHandler<RoundtripCompleteEventArgs> roundtripComplete) : this(subscriptionGuid, host, port, username, password, Pop3Client.GetAuthType(authMechanism, secMechanism), userLegacyDN, connectionTimeout, logSession, downloadCompleted, roundtripComplete)
		{
		}

		// Token: 0x0600104D RID: 4173 RVA: 0x00033560 File Offset: 0x00031760
		internal Pop3Client(Guid subscriptionGuid, string host, int port, string username, SecureString password, Pop3AuthType authType, string userLegacyDN, int connectionTimeout, SyncLogSession logSession, EventHandler<DownloadCompleteEventArgs> downloadCompleted, EventHandler<RoundtripCompleteEventArgs> roundtripComplete)
		{
			SyncUtilities.ThrowIfGuidEmpty("subscriptionGuid", subscriptionGuid);
			SyncUtilities.ThrowIfArgumentNullOrEmpty("host", host);
			SyncUtilities.ThrowIfArgumentLessThanZero("port", port);
			SyncUtilities.ThrowIfArgumentNullOrEmpty("username", username);
			SyncUtilities.ThrowIfArgumentNull("logSession", logSession);
			SyncUtilities.ThrowIfArgumentNullOrEmpty("userLegacyDN", userLegacyDN);
			this.subscriptionGuid = subscriptionGuid;
			this.host = host;
			this.port = port;
			this.remoteEndPoint = host + ":" + port.ToString(NumberFormatInfo.InvariantInfo);
			this.username = username;
			this.authType = authType;
			this.userLegacyDN = userLegacyDN;
			this.logSession = logSession;
			this.connectionTimeout = connectionTimeout;
			this.state = Pop3ClientState.ProcessConnection;
			this.emailDropCount = -1;
			if (password == null)
			{
				this.password = new SecureString();
			}
			else
			{
				this.password = password;
			}
			if (downloadCompleted != null)
			{
				this.RetrieveMessageCompleted += downloadCompleted;
			}
			if (roundtripComplete != null)
			{
				this.RoundtripComplete += roundtripComplete;
			}
		}

		// Token: 0x14000010 RID: 16
		// (add) Token: 0x0600104E RID: 4174 RVA: 0x0003366C File Offset: 0x0003186C
		// (remove) Token: 0x0600104F RID: 4175 RVA: 0x000336A4 File Offset: 0x000318A4
		internal event EventHandler<DownloadCompleteEventArgs> RetrieveMessageCompleted;

		// Token: 0x14000011 RID: 17
		// (add) Token: 0x06001050 RID: 4176 RVA: 0x000336DC File Offset: 0x000318DC
		// (remove) Token: 0x06001051 RID: 4177 RVA: 0x00033714 File Offset: 0x00031914
		private event EventHandler<RoundtripCompleteEventArgs> RoundtripComplete;

		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x06001052 RID: 4178 RVA: 0x00033749 File Offset: 0x00031949
		string ISyncSourceSession.Protocol
		{
			get
			{
				return "POP";
			}
		}

		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x06001053 RID: 4179 RVA: 0x00033750 File Offset: 0x00031950
		string ISyncSourceSession.SessionId
		{
			get
			{
				return this.sessionId;
			}
		}

		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x06001054 RID: 4180 RVA: 0x00033758 File Offset: 0x00031958
		string ISyncSourceSession.Server
		{
			get
			{
				return this.remoteEndPoint;
			}
		}

		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x06001055 RID: 4181 RVA: 0x00033760 File Offset: 0x00031960
		internal ExDateTime TimeSent
		{
			get
			{
				base.CheckDisposed();
				return this.timeSent;
			}
		}

		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x06001056 RID: 4182 RVA: 0x0003376E File Offset: 0x0003196E
		internal int EmailDropCount
		{
			get
			{
				base.CheckDisposed();
				return this.emailDropCount;
			}
		}

		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x06001057 RID: 4183 RVA: 0x0003377C File Offset: 0x0003197C
		private bool IsFirstLineBeingProcessed
		{
			get
			{
				return this.response == null;
			}
		}

		// Token: 0x06001058 RID: 4184 RVA: 0x00033788 File Offset: 0x00031988
		public override string ToString()
		{
			if (this.nc != null)
			{
				return string.Format(CultureInfo.InvariantCulture, "Connection {0} from {1} to {2}", new object[]
				{
					this.nc.ConnectionId,
					this.nc.LocalEndPoint,
					this.nc.RemoteEndPoint
				});
			}
			return string.Format(CultureInfo.InvariantCulture, "Disconnected connection", new object[0]);
		}

		// Token: 0x06001059 RID: 4185 RVA: 0x000337FC File Offset: 0x000319FC
		internal static Pop3Client FromSubscription(PopAggregationSubscription pop3Subscription, int connectionTimeout, SyncLogSession logSession, EventHandler<DownloadCompleteEventArgs> downloadCompleted, EventHandler<RoundtripCompleteEventArgs> roundtripComplete)
		{
			SyncUtilities.ThrowIfArgumentNull("pop3Subscription", pop3Subscription);
			return new Pop3Client(pop3Subscription.SubscriptionGuid, pop3Subscription.PopServer, pop3Subscription.PopPort, pop3Subscription.PopLogonName, pop3Subscription.LogonPasswordSecured, Pop3Client.GetAuthType(pop3Subscription), pop3Subscription.UserLegacyDN, connectionTimeout, logSession, downloadCompleted, roundtripComplete);
		}

		// Token: 0x0600105A RID: 4186 RVA: 0x00033850 File Offset: 0x00031A50
		internal static IAsyncResult BeginGetUniqueIds(Pop3Client thisPtr, AsyncCallback callback, object callbackState, object syncPoisonContext)
		{
			if (thisPtr == null)
			{
				throw new ArgumentNullException("thisPtr");
			}
			thisPtr.breadcrumbs.Drop(Pop3Client.Breadcrumbs.EnterGetUniqueIds);
			thisPtr.CheckDisposed();
			thisPtr.logSession.LogDebugging((TSLID)888UL, "Pop3Client.BeginGetUniqueIds", new object[0]);
			thisPtr.ThrowIfAsyncIOPending();
			thisPtr.CloseConnectionIfExists();
			thisPtr.resultData = new AsyncOperationResult<Pop3ResultData>(new Pop3ResultData());
			thisPtr.currentAsyncResult = new AsyncResult<Pop3Client, Pop3ResultData>(thisPtr, thisPtr, callback, callbackState, syncPoisonContext);
			thisPtr.seeServerCapabilities = true;
			Pop3AuthType pop3AuthType = thisPtr.authType;
			switch (pop3AuthType)
			{
			case Pop3AuthType.Basic:
				goto IL_13B;
			case Pop3AuthType.Ntlm:
				break;
			default:
				switch (pop3AuthType)
				{
				case Pop3AuthType.SSL:
					goto IL_13B;
				case Pop3AuthType.NtlmOverSSL:
					break;
				default:
					switch (pop3AuthType)
					{
					case Pop3AuthType.TLS:
						thisPtr.ProcessStates(new Pop3ClientState[]
						{
							Pop3ClientState.ProcessConnection,
							Pop3ClientState.ProcessStlsCommand,
							Pop3ClientState.ProcessUserCommand,
							Pop3ClientState.ProcessPassCommand,
							Pop3ClientState.ProcessCapaCommand,
							Pop3ClientState.ProcessStatCommand,
							Pop3ClientState.ProcessUidlCommand,
							Pop3ClientState.ProcessListCommand,
							Pop3ClientState.ProcessTopCommand
						});
						goto IL_177;
					case Pop3AuthType.NtlmOverTLS:
						thisPtr.ProcessStates(new Pop3ClientState[]
						{
							Pop3ClientState.ProcessConnection,
							Pop3ClientState.ProcessStlsCommand,
							Pop3ClientState.ProcessAuthNtlmCommand,
							Pop3ClientState.ProcessCapaCommand,
							Pop3ClientState.ProcessStatCommand,
							Pop3ClientState.ProcessUidlCommand,
							Pop3ClientState.ProcessListCommand,
							Pop3ClientState.ProcessTopCommand
						});
						goto IL_177;
					default:
						throw new NotSupportedException();
					}
					break;
				}
				break;
			}
			thisPtr.ProcessStates(new Pop3ClientState[]
			{
				Pop3ClientState.ProcessConnection,
				Pop3ClientState.ProcessAuthNtlmCommand,
				Pop3ClientState.ProcessCapaCommand,
				Pop3ClientState.ProcessStatCommand,
				Pop3ClientState.ProcessUidlCommand,
				Pop3ClientState.ProcessListCommand,
				Pop3ClientState.ProcessTopCommand
			});
			goto IL_177;
			IL_13B:
			thisPtr.ProcessStates(new Pop3ClientState[]
			{
				Pop3ClientState.ProcessConnection,
				Pop3ClientState.ProcessUserCommand,
				Pop3ClientState.ProcessPassCommand,
				Pop3ClientState.ProcessCapaCommand,
				Pop3ClientState.ProcessStatCommand,
				Pop3ClientState.ProcessUidlCommand,
				Pop3ClientState.ProcessListCommand,
				Pop3ClientState.ProcessTopCommand
			});
			IL_177:
			return thisPtr.currentAsyncResult;
		}

		// Token: 0x0600105B RID: 4187 RVA: 0x000339DC File Offset: 0x00031BDC
		internal static AsyncOperationResult<Pop3ResultData> EndGetUniqueIds(IAsyncResult asyncResult)
		{
			AsyncResult<Pop3Client, Pop3ResultData> asyncResult2 = (AsyncResult<Pop3Client, Pop3ResultData>)asyncResult;
			asyncResult2.State.CheckDisposed();
			asyncResult2.State.breadcrumbs.Drop(Pop3Client.Breadcrumbs.LeaveGetUniqueIds);
			return asyncResult2.WaitForCompletion();
		}

		// Token: 0x0600105C RID: 4188 RVA: 0x00033A14 File Offset: 0x00031C14
		internal static AsyncResult<Pop3Client, Pop3ResultData> BeginVerifyAccount(Pop3Client thisPtr, AsyncCallback callback, object callbackState, object syncPoisonContext)
		{
			if (thisPtr == null)
			{
				throw new ArgumentNullException("thisPtr");
			}
			thisPtr.breadcrumbs.Drop(Pop3Client.Breadcrumbs.EnterVerifyAccount);
			thisPtr.CheckDisposed();
			thisPtr.ThrowIfAsyncIOPending();
			thisPtr.CloseConnectionIfExists();
			thisPtr.resultData = new AsyncOperationResult<Pop3ResultData>(new Pop3ResultData());
			thisPtr.tempAsyncResult = new AsyncResult<Pop3Client, Pop3ResultData>(thisPtr, thisPtr, callback, callbackState, syncPoisonContext);
			thisPtr.currentAsyncResult = new AsyncResult<Pop3Client, Pop3ResultData>(thisPtr, thisPtr, new AsyncCallback(Pop3Client.CapaListComplete), thisPtr, syncPoisonContext);
			thisPtr.ensureQuit = true;
			Pop3AuthType pop3AuthType = thisPtr.authType;
			switch (pop3AuthType)
			{
			case Pop3AuthType.Basic:
				goto IL_FB;
			case Pop3AuthType.Ntlm:
				break;
			default:
				switch (pop3AuthType)
				{
				case Pop3AuthType.SSL:
					goto IL_FB;
				case Pop3AuthType.NtlmOverSSL:
					break;
				default:
					switch (pop3AuthType)
					{
					case Pop3AuthType.TLS:
						thisPtr.ProcessStates(new Pop3ClientState[]
						{
							Pop3ClientState.ProcessConnection,
							Pop3ClientState.ProcessStlsCommand,
							Pop3ClientState.ProcessUserCommand,
							Pop3ClientState.ProcessPassCommand,
							Pop3ClientState.ProcessCapaCommand
						});
						goto IL_122;
					case Pop3AuthType.NtlmOverTLS:
						thisPtr.ProcessStates(new Pop3ClientState[]
						{
							Pop3ClientState.ProcessConnection,
							Pop3ClientState.ProcessStlsCommand,
							Pop3ClientState.ProcessAuthNtlmCommand,
							Pop3ClientState.ProcessCapaCommand
						});
						goto IL_122;
					default:
						throw new NotSupportedException();
					}
					break;
				}
				break;
			}
			thisPtr.ProcessStates(new Pop3ClientState[]
			{
				Pop3ClientState.ProcessConnection,
				Pop3ClientState.ProcessAuthNtlmCommand,
				Pop3ClientState.ProcessCapaCommand
			});
			goto IL_122;
			IL_FB:
			thisPtr.ProcessStates(new Pop3ClientState[]
			{
				Pop3ClientState.ProcessConnection,
				Pop3ClientState.ProcessUserCommand,
				Pop3ClientState.ProcessPassCommand,
				Pop3ClientState.ProcessCapaCommand
			});
			IL_122:
			return thisPtr.tempAsyncResult;
		}

		// Token: 0x0600105D RID: 4189 RVA: 0x00033B49 File Offset: 0x00031D49
		internal static AsyncOperationResult<Pop3ResultData> EndVerifyAccount(AsyncResult<Pop3Client, Pop3ResultData> asyncResult)
		{
			asyncResult.State.CheckDisposed();
			asyncResult.State.breadcrumbs.Drop(Pop3Client.Breadcrumbs.LeaveVerifyAccount);
			return asyncResult.WaitForCompletion();
		}

		// Token: 0x0600105E RID: 4190 RVA: 0x00033B70 File Offset: 0x00031D70
		internal static AsyncResult<Pop3Client, Pop3ResultData> BeginGetEmail(Pop3Client thisPtr, int messageId, AsyncCallback callback, object callbackState, object syncPoisonContext)
		{
			if (thisPtr == null)
			{
				throw new ArgumentNullException("thisPtr");
			}
			thisPtr.breadcrumbs.Drop(Pop3Client.Breadcrumbs.EnterGetEmail);
			thisPtr.logSession.LogDebugging((TSLID)889UL, "Pop3Client.BeginGetEmail {0}", new object[]
			{
				messageId
			});
			thisPtr.CheckDisposed();
			ExTraceGlobals.FaultInjectionTracer.TraceTest(2187734333U);
			thisPtr.currentAsyncResult = new AsyncResult<Pop3Client, Pop3ResultData>(thisPtr, thisPtr, callback, callbackState, syncPoisonContext);
			if (!thisPtr.ProcessCompletedOnConnectionError(true))
			{
				thisPtr.messageIdTracker = new MessageIdTracker(messageId, messageId, false);
				thisPtr.ProcessStates(new Pop3ClientState[]
				{
					Pop3ClientState.ProcessRetrCommand
				});
			}
			return thisPtr.currentAsyncResult;
		}

		// Token: 0x0600105F RID: 4191 RVA: 0x00033C1C File Offset: 0x00031E1C
		internal static AsyncOperationResult<Pop3ResultData> EndGetEmail(IAsyncResult asyncResult)
		{
			AsyncResult<Pop3Client, Pop3ResultData> asyncResult2 = (AsyncResult<Pop3Client, Pop3ResultData>)asyncResult;
			asyncResult2.State.CheckDisposed();
			asyncResult2.State.breadcrumbs.Drop(Pop3Client.Breadcrumbs.LeaveGetEmail);
			return asyncResult2.WaitForCompletion();
		}

		// Token: 0x06001060 RID: 4192 RVA: 0x00033C54 File Offset: 0x00031E54
		internal static AsyncResult<Pop3Client, Pop3ResultData> BeginDeleteEmails(Pop3Client thisPtr, List<int> messageIds, AsyncCallback callback, object callbackState, object syncPoisonContext)
		{
			if (thisPtr == null)
			{
				throw new ArgumentNullException("thisPtr");
			}
			thisPtr.breadcrumbs.Drop(Pop3Client.Breadcrumbs.EnterDeleteEmails);
			thisPtr.logSession.LogDebugging((TSLID)890UL, "Pop3Client.BeginDeleteEmails", new object[0]);
			if (messageIds == null)
			{
				throw new ArgumentNullException("messageId");
			}
			thisPtr.CheckDisposed();
			thisPtr.currentAsyncResult = new AsyncResult<Pop3Client, Pop3ResultData>(thisPtr, thisPtr, callback, callbackState, syncPoisonContext);
			if (!thisPtr.ProcessCompletedOnConnectionError())
			{
				thisPtr.messageIdTracker = new MessageIdTracker(messageIds);
				thisPtr.ProcessStates(new Pop3ClientState[]
				{
					Pop3ClientState.ProcessDeleCommand
				});
			}
			return thisPtr.currentAsyncResult;
		}

		// Token: 0x06001061 RID: 4193 RVA: 0x00033CF0 File Offset: 0x00031EF0
		internal static AsyncOperationResult<Pop3ResultData> EndDeleteEmails(IAsyncResult asyncResult)
		{
			AsyncResult<Pop3Client, Pop3ResultData> asyncResult2 = (AsyncResult<Pop3Client, Pop3ResultData>)asyncResult;
			asyncResult2.State.CheckDisposed();
			asyncResult2.State.breadcrumbs.Drop(Pop3Client.Breadcrumbs.LeaveDeleteEmails);
			return asyncResult2.WaitForCompletion();
		}

		// Token: 0x06001062 RID: 4194 RVA: 0x00033D28 File Offset: 0x00031F28
		internal static AsyncResult<Pop3Client, Pop3ResultData> BeginQuit(Pop3Client thisPtr, AsyncCallback callback, object callbackState, object syncPoisonContext)
		{
			if (thisPtr == null)
			{
				throw new ArgumentNullException("thisPtr");
			}
			thisPtr.breadcrumbs.Drop(Pop3Client.Breadcrumbs.EnterQuit);
			thisPtr.logSession.LogDebugging((TSLID)891UL, "Pop3Client.BeginQuit", new object[0]);
			thisPtr.CheckDisposed();
			thisPtr.currentAsyncResult = new AsyncResult<Pop3Client, Pop3ResultData>(thisPtr, thisPtr, callback, callbackState, syncPoisonContext);
			if (!thisPtr.ProcessCompletedOnConnectionError())
			{
				thisPtr.ProcessStates(new Pop3ClientState[]
				{
					Pop3ClientState.ProcessQuitCommand
				});
			}
			return thisPtr.currentAsyncResult;
		}

		// Token: 0x06001063 RID: 4195 RVA: 0x00033DA8 File Offset: 0x00031FA8
		internal static AsyncOperationResult<Pop3ResultData> EndQuit(IAsyncResult asyncResult)
		{
			AsyncResult<Pop3Client, Pop3ResultData> asyncResult2 = (AsyncResult<Pop3Client, Pop3ResultData>)asyncResult;
			asyncResult2.State.CheckDisposed();
			asyncResult2.State.breadcrumbs.Drop(Pop3Client.Breadcrumbs.LeaveQuit);
			return asyncResult2.WaitForCompletion();
		}

		// Token: 0x06001064 RID: 4196 RVA: 0x00033DE0 File Offset: 0x00031FE0
		internal void Cancel(IAsyncResult asyncResult)
		{
			base.CheckDisposed();
			this.logSession.LogDebugging((TSLID)892UL, Pop3Client.Tracer, (long)this.GetHashCode(), "Pop3Client.Cancel", new object[0]);
			AsyncResult<Pop3Client, Pop3ResultData> asyncResult2 = (AsyncResult<Pop3Client, Pop3ResultData>)asyncResult;
			this.disconnectReason = DisconnectReason.Local;
			if (asyncResult2.PendingAsyncResult != null)
			{
				asyncResult2.PendingAsyncResult.AsyncWaitHandle.WaitOne();
			}
		}

		// Token: 0x06001065 RID: 4197 RVA: 0x00033E47 File Offset: 0x00032047
		internal void NotifyRoundtripComplete(object sender, RoundtripCompleteEventArgs roundtripCompleteEventArgs)
		{
			base.CheckDisposed();
			if (this.RoundtripComplete != null)
			{
				this.RoundtripComplete(sender, roundtripCompleteEventArgs);
			}
		}

		// Token: 0x06001066 RID: 4198 RVA: 0x00033E64 File Offset: 0x00032064
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				this.CloseConnectionIfExists();
				if (this.enumerator != null)
				{
					this.enumerator.Dispose();
					this.enumerator = null;
				}
			}
		}

		// Token: 0x06001067 RID: 4199 RVA: 0x00033E89 File Offset: 0x00032089
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<Pop3Client>(this);
		}

		// Token: 0x06001068 RID: 4200 RVA: 0x00033E94 File Offset: 0x00032094
		private static void CapaListComplete(IAsyncResult asyncResult)
		{
			Pop3Client pop3Client = asyncResult.AsyncState as Pop3Client;
			if (pop3Client.currentAsyncResult.Result.Exception != null && pop3Client.currentAsyncResult.Result.Exception.InnerException != null && pop3Client.currentAsyncResult.Result.Exception.InnerException is Pop3CapabilitiesNotSupportedException)
			{
				pop3Client.currentAsyncResult = pop3Client.tempAsyncResult;
				pop3Client.checkUniqueIdSupport = true;
				pop3Client.ProcessStates(new Pop3ClientState[]
				{
					Pop3ClientState.ProcessUidlCommand
				});
				return;
			}
			pop3Client.tempAsyncResult.ProcessCompleted(pop3Client.currentAsyncResult.Result.Data, pop3Client.currentAsyncResult.Result.Exception);
		}

		// Token: 0x06001069 RID: 4201 RVA: 0x00033F43 File Offset: 0x00032143
		private static Pop3AuthType GetAuthType(PopAggregationSubscription pop3Subscription)
		{
			return Pop3Client.GetAuthType(pop3Subscription.PopAuthentication, pop3Subscription.PopSecurity);
		}

		// Token: 0x0600106A RID: 4202 RVA: 0x00033F58 File Offset: 0x00032158
		private static Pop3AuthType GetAuthType(AuthenticationMechanism authMechanism, SecurityMechanism securityMechanism)
		{
			Pop3AuthType result;
			if (authMechanism != AuthenticationMechanism.Basic)
			{
				if (authMechanism != AuthenticationMechanism.Spa)
				{
					throw new InvalidOperationException("PopSubscription has an invalid PopAuthentication and PopSecurity set.");
				}
				switch (securityMechanism)
				{
				case SecurityMechanism.None:
					result = Pop3AuthType.Ntlm;
					break;
				case SecurityMechanism.Ssl:
					result = Pop3AuthType.NtlmOverSSL;
					break;
				case SecurityMechanism.Tls:
					result = Pop3AuthType.NtlmOverTLS;
					break;
				default:
					throw new InvalidOperationException("PopSubscription has an invalid PopAuthentication and PopSecurity set.");
				}
			}
			else
			{
				switch (securityMechanism)
				{
				case SecurityMechanism.None:
					result = Pop3AuthType.Basic;
					break;
				case SecurityMechanism.Ssl:
					result = Pop3AuthType.SSL;
					break;
				case SecurityMechanism.Tls:
					result = Pop3AuthType.TLS;
					break;
				default:
					throw new InvalidOperationException("PopSubscription has an invalid PopAuthentication and PopSecurity set.");
				}
			}
			return result;
		}

		// Token: 0x0600106B RID: 4203 RVA: 0x00034058 File Offset: 0x00032258
		private static void ProcessState(Pop3Client thisPtr)
		{
			Pop3Client.<>c__DisplayClass6 CS$<>8__locals1 = new Pop3Client.<>c__DisplayClass6();
			CS$<>8__locals1.thisPtr = thisPtr;
			CS$<>8__locals1.thisPtr.logSession.LogDebugging((TSLID)893UL, "Pop3Client.ProcessState {0}", new object[]
			{
				CS$<>8__locals1.thisPtr.state
			});
			bool flag = true;
			if ((CS$<>8__locals1.thisPtr.state == Pop3ClientState.ProcessDeleCommand || CS$<>8__locals1.thisPtr.state == Pop3ClientState.ProcessTopCommand || CS$<>8__locals1.thisPtr.state == Pop3ClientState.ProcessRetrCommand) && CS$<>8__locals1.thisPtr.messageIdTracker.MoveNext())
			{
				flag = false;
			}
			if (flag)
			{
				CS$<>8__locals1.thisPtr.MoveToNextState();
				if (CS$<>8__locals1.thisPtr.state != Pop3ClientState.ProcessUidlCommand && CS$<>8__locals1.thisPtr.messageIdTracker != null)
				{
					CS$<>8__locals1.thisPtr.messageIdTracker.Reset();
					if (!CS$<>8__locals1.thisPtr.messageIdTracker.MoveNext())
					{
						CS$<>8__locals1.thisPtr.MoveToNextState();
					}
				}
			}
			CS$<>8__locals1.thisPtr.logSession.LogDebugging((TSLID)894UL, "Processing state {0}", new object[]
			{
				CS$<>8__locals1.thisPtr.state
			});
			switch (CS$<>8__locals1.thisPtr.state)
			{
			case Pop3ClientState.ProcessConnection:
				CS$<>8__locals1.thisPtr.connectionError = null;
				ILUtil.DoTryFilterCatch(new TryDelegate(CS$<>8__locals1, (UIntPtr)ldftn(<ProcessState>b__0)), new FilterDelegate(CS$<>8__locals1, (UIntPtr)ldftn(<ProcessState>b__1)), new CatchDelegate(null, (UIntPtr)ldftn(<ProcessState>b__2)));
				CS$<>8__locals1.thisPtr.command = null;
				CS$<>8__locals1.thisPtr.response = null;
				try
				{
					AsyncCallback asyncCallbackWithPoisonContextAndUnhandledExceptionRedirect = CS$<>8__locals1.thisPtr.currentAsyncResult.GetAsyncCallbackWithPoisonContextAndUnhandledExceptionRedirect(Pop3Client.connectComplete);
					CS$<>8__locals1.thisPtr.currentAsyncResult.PendingAsyncResult = CS$<>8__locals1.thisPtr.socket.BeginConnect(CS$<>8__locals1.thisPtr.host, CS$<>8__locals1.thisPtr.port, asyncCallbackWithPoisonContextAndUnhandledExceptionRedirect, CS$<>8__locals1.thisPtr);
					return;
				}
				catch (ArgumentException exception)
				{
					CS$<>8__locals1.thisPtr.socket = null;
					CS$<>8__locals1.thisPtr.HandleResult(exception);
					return;
				}
				catch (SocketException exception2)
				{
					CS$<>8__locals1.thisPtr.socket = null;
					CS$<>8__locals1.thisPtr.HandleResult(exception2);
					return;
				}
				break;
			case Pop3ClientState.ProcessCapaCommand:
				Pop3Client.SendRequestThenReadLine(CS$<>8__locals1.thisPtr, Pop3Command.Capa);
				return;
			case Pop3ClientState.ProcessTopCommand:
				Pop3Client.SendRequestThenReadLine(CS$<>8__locals1.thisPtr, Pop3Command.Top(CS$<>8__locals1.thisPtr.messageIdTracker.Current, 0));
				return;
			case Pop3ClientState.ProcessStlsCommand:
				break;
			case Pop3ClientState.ProcessAuthNtlmCommand:
				CS$<>8__locals1.thisPtr.authNtlmResponseCount = 0;
				Pop3Client.SendRequestThenReadLine(CS$<>8__locals1.thisPtr, Pop3Command.AuthNtlm);
				return;
			case Pop3ClientState.ProcessUserCommand:
			case Pop3ClientState.ProcessPassCommand:
			{
				Pop3Command pop3Command = null;
				try
				{
					if (CS$<>8__locals1.thisPtr.state == Pop3ClientState.ProcessUserCommand)
					{
						pop3Command = Pop3Command.User(CS$<>8__locals1.thisPtr.username);
					}
					else
					{
						pop3Command = Pop3Command.Pass(CS$<>8__locals1.thisPtr.password);
					}
				}
				catch (Pop3InvalidCommandException exception3)
				{
					CS$<>8__locals1.thisPtr.logSession.LogError((TSLID)896UL, Pop3Client.Tracer, "The 'USER' or 'PASS' POP3 command is invalid (username = {0}).", new object[]
					{
						CS$<>8__locals1.thisPtr.username
					});
					CS$<>8__locals1.thisPtr.HandleResult(exception3);
					return;
				}
				Pop3Client.SendRequestThenReadLine(CS$<>8__locals1.thisPtr, pop3Command);
				return;
			}
			case Pop3ClientState.ProcessStatCommand:
				Pop3Client.SendRequestThenReadLine(CS$<>8__locals1.thisPtr, Pop3Command.Stat);
				return;
			case Pop3ClientState.ProcessUidlCommand:
				Pop3Client.SendRequestThenReadLine(CS$<>8__locals1.thisPtr, Pop3Command.Uidl());
				return;
			case Pop3ClientState.ProcessListCommand:
				Pop3Client.SendRequestThenReadLine(CS$<>8__locals1.thisPtr, Pop3Command.List());
				return;
			case Pop3ClientState.ProcessRetrCommand:
				Pop3Client.SendRequestThenReadLine(CS$<>8__locals1.thisPtr, Pop3Command.Retr(CS$<>8__locals1.thisPtr.messageIdTracker.Current));
				return;
			case Pop3ClientState.ProcessDeleCommand:
				Pop3Client.SendRequestThenReadLine(CS$<>8__locals1.thisPtr, Pop3Command.Dele(CS$<>8__locals1.thisPtr.messageIdTracker.Current));
				return;
			case Pop3ClientState.ProcessQuitCommand:
				Pop3Client.SendRequestThenReadLine(CS$<>8__locals1.thisPtr, Pop3Command.Quit);
				return;
			case Pop3ClientState.ProcessEnd:
				if (CS$<>8__locals1.thisPtr.ensureQuit)
				{
					CS$<>8__locals1.thisPtr.ensureQuit = false;
					Pop3Client.SendRequestThenReadLine(CS$<>8__locals1.thisPtr, Pop3Command.Quit);
					return;
				}
				CS$<>8__locals1.thisPtr.currentAsyncResult.ProcessCompleted(CS$<>8__locals1.thisPtr.resultData.Data, CS$<>8__locals1.thisPtr.resultData.Exception);
				return;
			default:
				return;
			}
			Pop3Client.SendRequestThenReadLine(CS$<>8__locals1.thisPtr, Pop3Command.Stls);
		}

		// Token: 0x0600106C RID: 4204 RVA: 0x00034518 File Offset: 0x00032718
		private static void ConnectComplete(IAsyncResult asyncResult)
		{
			Pop3Client pop3Client = (Pop3Client)asyncResult.AsyncState;
			pop3Client.logSession.LogDebugging((TSLID)897UL, "Pop3Client.ConnectComplete", new object[0]);
			try
			{
				pop3Client.currentAsyncResult.PendingAsyncResult = null;
				pop3Client.socket.EndConnect(asyncResult);
				ExTraceGlobals.FaultInjectionTracer.TraceTest(2489724221U);
				if (pop3Client.currentAsyncResult.IsCanceled)
				{
					pop3Client.socket = null;
					pop3Client.HandleCancelation();
					return;
				}
				pop3Client.nc = new NetworkConnection(pop3Client.socket, 4096);
				pop3Client.socket = null;
				pop3Client.nc.Timeout = (pop3Client.connectionTimeout + 999) / 1000;
				pop3Client.logSession.LogVerbose((TSLID)898UL, Pop3Client.Tracer, (long)pop3Client.GetHashCode(), "Connection Completed. Connection ID : {0}, Remote End Point {1}", new object[]
				{
					pop3Client.nc.ConnectionId,
					pop3Client.nc.RemoteEndPoint
				});
			}
			catch (SocketException exception)
			{
				pop3Client.socket = null;
				pop3Client.HandleResult(exception);
				return;
			}
			ConnectionLog.AggregationConnectionStart("POP", pop3Client.sessionId, pop3Client.remoteEndPoint, pop3Client.subscriptionGuid);
			pop3Client.logSession.LogConnect((TSLID)899UL, "sessionId={0}, auth={1}", new object[]
			{
				pop3Client.sessionId,
				pop3Client.authType
			});
			pop3Client.logSession.LogConnectionInformation((TSLID)900UL, string.Format(CultureInfo.InvariantCulture, "{0} - {1}", new object[]
			{
				pop3Client.host,
				pop3Client.nc.RemoteEndPoint
			}));
			switch (pop3Client.authType)
			{
			case Pop3AuthType.SSL:
			case Pop3AuthType.NtlmOverSSL:
				pop3Client.currentAsyncResult.PendingAsyncResult = pop3Client.nc.BeginNegotiateTlsAsClient(null, pop3Client.nc.RemoteEndPoint.Address.ToString(), pop3Client.currentAsyncResult.GetAsyncCallbackWithPoisonContext(Pop3Client.negotiateTlsCompleteReadLine), pop3Client);
				return;
			default:
				Pop3Client.StartReadLine(pop3Client);
				break;
			}
		}

		// Token: 0x0600106D RID: 4205 RVA: 0x00034748 File Offset: 0x00032948
		private static void NegotiateTlsCompleteReadLine(IAsyncResult asyncResult)
		{
			Pop3Client pop3Client = (Pop3Client)asyncResult.AsyncState;
			pop3Client.logSession.LogDebugging((TSLID)901UL, Pop3Client.Tracer, (long)pop3Client.GetHashCode(), "Pop3Client.NegotiateTlsCompleteReadline", new object[0]);
			pop3Client.currentAsyncResult.PendingAsyncResult = null;
			object obj;
			pop3Client.nc.EndNegotiateTlsAsClient(asyncResult, out obj);
			if (obj != null)
			{
				pop3Client.HandleError(obj);
				return;
			}
			if (pop3Client.currentAsyncResult.IsCanceled)
			{
				pop3Client.HandleCancelation();
				return;
			}
			if (pop3Client.nc.RemoteCertificate == null)
			{
				pop3Client.logSession.LogError((TSLID)902UL, Pop3Client.Tracer, (long)pop3Client.GetHashCode(), "No remote certificate present", new object[0]);
				pop3Client.HandleResult(new TlsFailureException(Strings.TlsRemoteCertificateInvalid));
				return;
			}
			pop3Client.logSession.LogDebugging((TSLID)903UL, Pop3Client.Tracer, (long)pop3Client.GetHashCode(), "TLS negotiation completed for connection.", new object[0]);
			Pop3AuthType pop3AuthType = pop3Client.authType;
			switch (pop3AuthType)
			{
			case Pop3AuthType.SSL:
			case Pop3AuthType.NtlmOverSSL:
				Pop3Client.StartReadLine(pop3Client);
				return;
			default:
				switch (pop3AuthType)
				{
				case Pop3AuthType.TLS:
				case Pop3AuthType.NtlmOverTLS:
					Pop3Client.ProcessState(pop3Client);
					return;
				default:
					throw new NotSupportedException();
				}
				break;
			}
		}

		// Token: 0x0600106E RID: 4206 RVA: 0x000348CC File Offset: 0x00032ACC
		private static void StartReadLine(Pop3Client thisPtr)
		{
			if (thisPtr.recursionLevel < 20)
			{
				try
				{
					Interlocked.Increment(ref thisPtr.recursionLevel);
					thisPtr.breadcrumbs.Drop(Pop3Client.Breadcrumbs.EnterReadNetworkBytes);
					thisPtr.currentAsyncResult.PendingAsyncResult = thisPtr.nc.BeginReadLine(thisPtr.currentAsyncResult.GetAsyncCallbackWithPoisonContext(Pop3Client.readLineComplete), thisPtr);
					return;
				}
				finally
				{
					Interlocked.Decrement(ref thisPtr.recursionLevel);
				}
			}
			ThreadPool.QueueUserWorkItem(delegate(object callstate)
			{
				Pop3Client pop3Client = (Pop3Client)callstate;
				pop3Client.breadcrumbs.Drop(Pop3Client.Breadcrumbs.EnterReadNetworkBytes);
				pop3Client.currentAsyncResult.PendingAsyncResult = pop3Client.nc.BeginReadLine(pop3Client.currentAsyncResult.GetAsyncCallbackWithPoisonContext(Pop3Client.readLineComplete), pop3Client);
			}, thisPtr);
		}

		// Token: 0x0600106F RID: 4207 RVA: 0x00034968 File Offset: 0x00032B68
		private static void ReadLineComplete(IAsyncResult asyncResult)
		{
			Pop3Client pop3Client = (Pop3Client)asyncResult.AsyncState;
			pop3Client.breadcrumbs.Drop(Pop3Client.Breadcrumbs.LeaveReadNetworkBytes);
			pop3Client.currentAsyncResult.PendingAsyncResult = null;
			byte[] buffer;
			int offset;
			int size;
			object obj;
			pop3Client.nc.EndReadLine(asyncResult, out buffer, out offset, out size, out obj);
			bool moreResponseDataAvailable = false;
			if (obj != null)
			{
				if (!(obj is SocketError) || (SocketError)obj != SocketError.MessageSize)
				{
					pop3Client.HandleError(obj);
					return;
				}
				moreResponseDataAvailable = true;
			}
			if (pop3Client.currentAsyncResult.IsCanceled)
			{
				pop3Client.logSession.LogError((TSLID)904UL, Pop3Client.Tracer, (long)pop3Client.GetHashCode(), "Response received from the network connection, but we are already disconnected", new object[0]);
				pop3Client.HandleCancelation();
				return;
			}
			Pop3Client.StartProcessingResponse(pop3Client, buffer, offset, size, moreResponseDataAvailable);
		}

		// Token: 0x06001070 RID: 4208 RVA: 0x00034A2C File Offset: 0x00032C2C
		private static void StartProcessingResponse(Pop3Client thisPtr, byte[] buffer, int offset, int size, bool moreResponseDataAvailable)
		{
			thisPtr.logSession.LogDebugging((TSLID)905UL, "Pop3Client.StartProcessingResponse", new object[0]);
			BufferBuilder bufferBuilder = thisPtr.responseBuffer ?? new BufferBuilder(size);
			thisPtr.responseBuffer = null;
			bool isFirstLineBeingProcessed = thisPtr.IsFirstLineBeingProcessed;
			int num = isFirstLineBeingProcessed ? 510 : 32768;
			if (bufferBuilder.Length + size > num)
			{
				string text = string.Format(CultureInfo.InvariantCulture, "The response line has got {0} bytes, which exceeds the maximum length: {1}.", new object[]
				{
					bufferBuilder.Length + size,
					num
				});
				thisPtr.logSession.LogError((TSLID)906UL, text, new object[0]);
				thisPtr.HandleError(new Pop3NonCompliantServerException(new FormatException(text)));
				return;
			}
			bufferBuilder.Append(buffer, offset, size);
			if (moreResponseDataAvailable)
			{
				thisPtr.responseBuffer = bufferBuilder;
				Pop3Client.StartReadLine(thisPtr);
				return;
			}
			byte[] buffer2 = bufferBuilder.GetBuffer();
			bool flag = BufferParser.CompareArg(Pop3Client.DotBytes, buffer2, 0, Pop3Client.DotBytes.Length);
			if (isFirstLineBeingProcessed)
			{
				Pop3Response pop3Response = new Pop3Response(bufferBuilder.ToString());
				if (thisPtr.state == Pop3ClientState.ProcessAuthNtlmCommand)
				{
					thisPtr.authNtlmResponseCount++;
					if (thisPtr.authNtlmResponseCount == 2 && pop3Response.Type != Pop3ResponseType.sendMore)
					{
						string text2 = string.Format(CultureInfo.InvariantCulture, "The response line was type {0} but expected was {1}.", new object[]
						{
							pop3Response.Type,
							Pop3ResponseType.sendMore
						});
						thisPtr.logSession.LogError((TSLID)1303UL, text2, new object[0]);
						thisPtr.HandleError(new Pop3NonCompliantServerException(new InvalidDataException(text2)));
						return;
					}
				}
				thisPtr.response = pop3Response;
				if (thisPtr.response.Type == Pop3ResponseType.unknown && thisPtr.response.Headline.StartsWith(" "))
				{
					string message = string.Format(CultureInfo.InvariantCulture, "Pop server {0} returned a response that started with leading space for legacyDn: {1} user name: {2} subscription: {3} while processing state: {4} Response was: [{5}]", new object[]
					{
						thisPtr.host,
						thisPtr.userLegacyDN,
						thisPtr.username,
						thisPtr.subscriptionGuid,
						thisPtr.state.ToString(),
						thisPtr.response.Headline
					});
					thisPtr.logSession.ReportWatson(message);
				}
			}
			else if (!flag && (thisPtr.command.Type == Pop3CommandType.Capa || thisPtr.command.Type == Pop3CommandType.List || thisPtr.command.Type == Pop3CommandType.Uidl))
			{
				int num2;
				if (thisPtr.resultData.Data.EmailDropCount > 0)
				{
					num2 = Math.Min(thisPtr.resultData.Data.EmailDropCount, 50000);
				}
				else
				{
					num2 = 50000;
				}
				if (thisPtr.response.ListingCount == num2)
				{
					thisPtr.logSession.LogError((TSLID)907UL, Pop3Client.Tracer, "'{0}' has already got {1} response lines that is the maximum line count or the email drop count.", new object[]
					{
						thisPtr.command,
						thisPtr.response.ListingCount
					});
					Pop3Client.CountCommand(thisPtr.currentAsyncResult, false);
					thisPtr.HandleResult(new Pop3BrokenResponseException(thisPtr.command.ToString(), thisPtr.response.ToString()));
					return;
				}
				thisPtr.response.AppendListing(bufferBuilder.ToString());
			}
			bool flag2 = thisPtr.command != null && thisPtr.command.Listings && thisPtr.response.Type == Pop3ResponseType.ok;
			bool flag3 = !flag2 || (buffer2.Length == 1 && flag);
			if (isFirstLineBeingProcessed || (thisPtr.command != null && (thisPtr.command.Type == Pop3CommandType.Uidl || thisPtr.command.Type == Pop3CommandType.List || thisPtr.command.Type == Pop3CommandType.Capa)))
			{
				bufferBuilder.RemoveUnusedBufferSpace();
				thisPtr.logSession.LogReceive((TSLID)908UL, bufferBuilder.GetBuffer());
			}
			if (flag3)
			{
				Pop3Client.ProcessResponse(thisPtr);
				return;
			}
			if (thisPtr.response.ListingCount == 0 && (thisPtr.command.Type == Pop3CommandType.Capa || thisPtr.command.Type == Pop3CommandType.List || thisPtr.command.Type == Pop3CommandType.Uidl))
			{
				thisPtr.response.ListingCapacity = thisPtr.resultData.Data.EmailDropCount;
			}
			if (thisPtr.command.Type == Pop3CommandType.Retr || thisPtr.command.Type == Pop3CommandType.Top)
			{
				if (thisPtr.dotStuffedParser == null)
				{
					thisPtr.dotStuffedParser = new SmtpInDataParser();
				}
				else
				{
					thisPtr.dotStuffedParser.Reset();
				}
				thisPtr.dotStuffedParser.BodyStream = thisPtr.response.MessageStream;
				thisPtr.currentAsyncResult.PendingAsyncResult = thisPtr.nc.BeginRead(thisPtr.currentAsyncResult.GetAsyncCallbackWithPoisonContext(Pop3Client.readCompleteConsumeData), thisPtr);
				return;
			}
			Pop3Client.StartReadLine(thisPtr);
		}

		// Token: 0x06001071 RID: 4209 RVA: 0x00034F00 File Offset: 0x00033100
		private static void ReadCompleteConsumeData(IAsyncResult asyncResult)
		{
			Pop3Client pop3Client = (Pop3Client)asyncResult.AsyncState;
			pop3Client.currentAsyncResult.PendingAsyncResult = null;
			byte[] data;
			int offset;
			int num;
			object obj;
			pop3Client.nc.EndRead(asyncResult, out data, out offset, out num, out obj);
			if (pop3Client.currentAsyncResult.IsCanceled)
			{
				pop3Client.HandleCancelation();
				return;
			}
			if (obj != null)
			{
				pop3Client.HandleError(obj);
				return;
			}
			int num2;
			bool flag = pop3Client.dotStuffedParser.Write(data, offset, num, out num2);
			int bytesUnconsumed = num - num2;
			if (flag)
			{
				pop3Client.nc.PutBackReceivedBytes(bytesUnconsumed);
				pop3Client.response.MessageStream.Seek(0L, SeekOrigin.Begin);
				string s = string.Format(CultureInfo.InvariantCulture, ">>> retrieves {0} bytes>>>", new object[]
				{
					pop3Client.response.MessageStream.Length
				});
				pop3Client.logSession.LogReceive((TSLID)909UL, Encoding.ASCII.GetBytes(s));
				Pop3Client.ProcessResponse(pop3Client);
				return;
			}
			if ((double)pop3Client.dotStuffedParser.TotalBytesRead > (double)AggregationConfiguration.Instance.MaxDownloadSizePerItem * 1.4)
			{
				pop3Client.HandleError(new MessageSizeLimitExceededException());
				return;
			}
			pop3Client.nc.PutBackReceivedBytes(bytesUnconsumed);
			pop3Client.currentAsyncResult.PendingAsyncResult = pop3Client.nc.BeginRead(pop3Client.currentAsyncResult.GetAsyncCallbackWithPoisonContext(Pop3Client.readCompleteConsumeData), pop3Client);
		}

		// Token: 0x06001072 RID: 4210 RVA: 0x0003506C File Offset: 0x0003326C
		private static void SendAuthBlobThenReadLine(Pop3Client thisPtr)
		{
			if (thisPtr.authContext == null)
			{
				AuthenticationMechanism mechanism = AuthenticationMechanism.Ntlm;
				string spn = "POP/" + thisPtr.host;
				thisPtr.authContext = new AuthenticationContext();
				SecurityStatus securityStatus = thisPtr.authContext.InitializeForOutboundNegotiate(mechanism, spn, thisPtr.username, thisPtr.password);
				if (securityStatus != SecurityStatus.OK)
				{
					thisPtr.HandleError(securityStatus);
					return;
				}
			}
			string s = thisPtr.response.Headline.Substring(2);
			if (thisPtr.authNtlmResponseCount == 1)
			{
				s = string.Empty;
			}
			byte[] bytes = Encoding.ASCII.GetBytes(s);
			byte[] blob = null;
			SecurityStatus securityStatus2 = thisPtr.authContext.NegotiateSecurityContext(bytes, out blob);
			SecurityStatus securityStatus3 = securityStatus2;
			if (securityStatus3 != SecurityStatus.OK && securityStatus3 != SecurityStatus.ContinueNeeded)
			{
				thisPtr.HandleError(securityStatus2);
				return;
			}
			Pop3Client.SendRequestThenReadLine(thisPtr, Pop3Command.Blob(blob));
		}

		// Token: 0x06001073 RID: 4211 RVA: 0x00035138 File Offset: 0x00033338
		private static void ProcessResponse(Pop3Client thisPtr)
		{
			thisPtr.logSession.LogDebugging((TSLID)910UL, "Pop3Client.ProcessResponse {0}", new object[]
			{
				thisPtr.response.Type
			});
			if (thisPtr.command != null)
			{
				Pop3Client.CountCommand(thisPtr.currentAsyncResult, true);
			}
			switch (thisPtr.response.Type)
			{
			case Pop3ResponseType.ok:
				if (thisPtr.state == Pop3ClientState.ProcessAuthNtlmCommand && thisPtr.authNtlmResponseCount == 1)
				{
					Pop3Client.SendAuthBlobThenReadLine(thisPtr);
					return;
				}
				switch (thisPtr.state)
				{
				case Pop3ClientState.ProcessCapaCommand:
				{
					bool uidlCommandSupported = false;
					int? retentionDays = null;
					Exception ex = thisPtr.response.TryParseCapaResponse(out uidlCommandSupported, out retentionDays);
					if (ex != null)
					{
						thisPtr.HandleResult(new Pop3BrokenResponseException(thisPtr.command.ToString(), thisPtr.response.ToString(), ex));
						return;
					}
					thisPtr.resultData.Data.RetentionDays = retentionDays;
					thisPtr.resultData.Data.UidlCommandSupported = uidlCommandSupported;
					break;
				}
				case Pop3ClientState.ProcessTopCommand:
				{
					ExDateTime exDateTime = thisPtr.response.ParseReceivedDate(false);
					if (thisPtr.messageIdTracker.Current == 1)
					{
						thisPtr.resultData.Data.FirstReceivedDate = exDateTime;
					}
					else
					{
						if (thisPtr.messageIdTracker.Current != thisPtr.resultData.Data.EmailDropCount)
						{
							throw new InvalidOperationException("The message id must be 1 or the number of messages in the maildrop.");
						}
						thisPtr.resultData.Data.LastReceivedDate = exDateTime;
					}
					break;
				}
				case Pop3ClientState.ProcessStlsCommand:
					thisPtr.currentAsyncResult.PendingAsyncResult = thisPtr.nc.BeginNegotiateTlsAsClient(null, thisPtr.nc.RemoteEndPoint.Address.ToString(), thisPtr.currentAsyncResult.GetAsyncCallbackWithPoisonContext(Pop3Client.negotiateTlsCompleteReadLine), thisPtr);
					return;
				case Pop3ClientState.ProcessStatCommand:
				{
					int num;
					Exception ex2 = thisPtr.response.TryParseSTATResponse(out num);
					if (ex2 != null)
					{
						thisPtr.HandleResult(new Pop3BrokenResponseException(thisPtr.command.ToString(), thisPtr.response.ToString(), ex2));
						return;
					}
					thisPtr.emailDropCount = num;
					if (num > 50000)
					{
						thisPtr.logSession.LogError((TSLID)913UL, Pop3Client.Tracer, "'STAT' indicates that there are {0} response lines coming, and that is above the maximum email drop count.  Stopping download.", new object[]
						{
							num
						});
						thisPtr.HandleResult(new Pop3BrokenResponseException(thisPtr.command.ToString(), thisPtr.response.ToString()));
					}
					else
					{
						thisPtr.resultData.Data.EmailDropCount = num;
						if (num == 0)
						{
							thisPtr.MoveToEndState();
						}
					}
					break;
				}
				case Pop3ClientState.ProcessUidlCommand:
					if (thisPtr.checkUniqueIdSupport)
					{
						thisPtr.checkUniqueIdSupport = false;
					}
					else
					{
						Exception ex3 = thisPtr.response.TryParseUIDLResponse(thisPtr.resultData.Data);
						if (ex3 != null)
						{
							thisPtr.HandleResult(new Pop3BrokenResponseException(thisPtr.command.ToString(), thisPtr.response.ToString(), ex3));
							return;
						}
					}
					break;
				case Pop3ClientState.ProcessListCommand:
				{
					Exception ex4 = thisPtr.response.TryParseLISTResponse(thisPtr.resultData.Data);
					if (ex4 != null)
					{
						thisPtr.HandleResult(new Pop3BrokenResponseException(thisPtr.command.ToString(), thisPtr.response.ToString(), ex4));
						return;
					}
					bool flag = thisPtr.resultData.Data.EmailDropCount > 2;
					if (flag)
					{
						thisPtr.messageIdTracker = new MessageIdTracker(new int[]
						{
							1,
							thisPtr.resultData.Data.EmailDropCount
						});
					}
					else
					{
						thisPtr.MoveToEndState();
					}
					break;
				}
				case Pop3ClientState.ProcessRetrCommand:
				{
					ExTraceGlobals.FaultInjectionTracer.TraceTest(3261476157U);
					ExDateTime exDateTime2 = thisPtr.response.ParseReceivedDate(true);
					if (exDateTime2 == ExDateTime.MinValue)
					{
						exDateTime2 = ExDateTime.UtcNow;
					}
					thisPtr.resultData.Data.Email = new Pop3Email(thisPtr, exDateTime2, thisPtr.response.MessageStream);
					thisPtr.submittedMessagesCount += 1UL;
					if (thisPtr.RetrieveMessageCompleted != null)
					{
						thisPtr.RetrieveMessageCompleted(thisPtr, new DownloadCompleteEventArgs(thisPtr.response.MessageStream.Length));
					}
					break;
				}
				case Pop3ClientState.ProcessDeleCommand:
					thisPtr.resultData.Data.AddDeletedMessageId(thisPtr.messageIdTracker.Current);
					break;
				}
				Pop3Client.ProcessState(thisPtr);
				return;
			case Pop3ResponseType.err:
				if (thisPtr.state != Pop3ClientState.ProcessTopCommand && thisPtr.state != Pop3ClientState.ProcessCapaCommand && thisPtr.state != Pop3ClientState.ProcessUidlCommand)
				{
					thisPtr.logSession.LogError((TSLID)912UL, Pop3Client.Tracer, (long)thisPtr.GetHashCode(), "Error response returned: {0}.", new object[]
					{
						thisPtr.response.ToString()
					});
				}
				if (!thisPtr.response.HasPermanentError)
				{
					switch (thisPtr.state)
					{
					case Pop3ClientState.ProcessCapaCommand:
						if (!thisPtr.seeServerCapabilities)
						{
							thisPtr.HandleResult(new Pop3CapabilitiesNotSupportedException());
							return;
						}
						Pop3Client.ProcessState(thisPtr);
						return;
					case Pop3ClientState.ProcessTopCommand:
						thisPtr.MoveToEndState();
						Pop3Client.ProcessState(thisPtr);
						return;
					case Pop3ClientState.ProcessUserCommand:
					case Pop3ClientState.ProcessPassCommand:
					{
						Exception exception;
						if (thisPtr.response.HasInUseAuthenticationError)
						{
							exception = new Pop3TransientInUseAuthErrorException();
						}
						else if (thisPtr.response.HasLogInDelayAuthenticationError)
						{
							exception = new Pop3TransientLoginDelayedAuthErrorException();
						}
						else if (thisPtr.response.HasSystemTemporaryError)
						{
							exception = new Pop3TransientSystemAuthErrorException();
						}
						else
						{
							exception = new Pop3AuthErrorException();
						}
						thisPtr.HandleResult(exception);
						return;
					}
					case Pop3ClientState.ProcessUidlCommand:
						thisPtr.resultData.Data.UidlCommandSupported = false;
						Pop3Client.ProcessState(thisPtr);
						return;
					}
					thisPtr.HandleResult(new Pop3ErrorResponseException((thisPtr.command == null) ? string.Empty : thisPtr.command.ToString(), (thisPtr.response == null) ? string.Empty : thisPtr.response.ToString()));
					return;
				}
				if (thisPtr.state == Pop3ClientState.ProcessUserCommand || thisPtr.state == Pop3ClientState.ProcessPassCommand)
				{
					thisPtr.HandleResult(new Pop3DisabledResponseException());
					return;
				}
				thisPtr.HandleResult(new Pop3PermErrorResponseException((thisPtr.command == null) ? string.Empty : thisPtr.command.ToString(), (thisPtr.response == null) ? string.Empty : thisPtr.response.ToString()));
				return;
			case Pop3ResponseType.unknown:
				break;
			case Pop3ResponseType.sendMore:
				if (thisPtr.state == Pop3ClientState.ProcessAuthNtlmCommand)
				{
					Pop3Client.SendAuthBlobThenReadLine(thisPtr);
					return;
				}
				break;
			default:
				throw new InvalidOperationException("The Pop3 response type is invalid.");
			}
			thisPtr.logSession.LogError((TSLID)911UL, Pop3Client.Tracer, (long)thisPtr.GetHashCode(), "Unknown response returned: {0}.", new object[]
			{
				thisPtr.response.ToString()
			});
			thisPtr.HandleResult(new Pop3UnknownResponseException((thisPtr.command == null) ? string.Empty : thisPtr.command.ToString(), (thisPtr.response == null) ? string.Empty : thisPtr.response.ToString()));
		}

		// Token: 0x06001074 RID: 4212 RVA: 0x00035800 File Offset: 0x00033A00
		private static void ReadCompleteSendRequest(IAsyncResult asyncResult)
		{
			Pop3Client pop3Client = (Pop3Client)asyncResult.AsyncState;
			pop3Client.breadcrumbs.Drop(Pop3Client.Breadcrumbs.LeaveReadNetworkBytes);
			pop3Client.currentAsyncResult.PendingAsyncResult = null;
			byte[] value;
			int offset;
			int num;
			object obj;
			pop3Client.nc.EndRead(asyncResult, out value, out offset, out num, out obj);
			if (pop3Client.currentAsyncResult.IsCanceled)
			{
				pop3Client.HandleCancelation();
				return;
			}
			if (obj != null)
			{
				pop3Client.HandleError(obj);
				return;
			}
			BufferBuilder bufferBuilder = new BufferBuilder(num);
			bufferBuilder.Append(value, offset, num);
			bufferBuilder.RemoveUnusedBufferSpace();
			pop3Client.logSession.LogReceive((TSLID)914UL, bufferBuilder.GetBuffer());
			Pop3Client.SendRequestThenReadLine(pop3Client, pop3Client.command);
		}

		// Token: 0x06001075 RID: 4213 RVA: 0x000358B4 File Offset: 0x00033AB4
		private static void SendRequestThenReadLine(Pop3Client thisPtr, Pop3Command pop3Command)
		{
			thisPtr.command = pop3Command;
			if (thisPtr.nc.IsDataAvailable)
			{
				thisPtr.breadcrumbs.Drop(Pop3Client.Breadcrumbs.EnterReadNetworkBytes);
				thisPtr.currentAsyncResult.PendingAsyncResult = thisPtr.nc.BeginRead(thisPtr.currentAsyncResult.GetAsyncCallbackWithPoisonContext(Pop3Client.readCompleteSendRequest), thisPtr);
				return;
			}
			byte[] buffer = thisPtr.command.Buffer;
			thisPtr.logSession.LogSend((TSLID)915UL, (thisPtr.command.Type != Pop3CommandType.Pass) ? buffer : Pop3Client.PassBytes);
			thisPtr.breadcrumbs.Drop(Pop3Client.Breadcrumbs.EnterSendNetworkBytes);
			thisPtr.response = null;
			thisPtr.timeSent = ExDateTime.UtcNow;
			thisPtr.currentAsyncResult.PendingAsyncResult = thisPtr.nc.BeginWrite(buffer, 0, buffer.Length, thisPtr.currentAsyncResult.GetAsyncCallbackWithPoisonContext(Pop3Client.sendCompleteReadLine), thisPtr);
		}

		// Token: 0x06001076 RID: 4214 RVA: 0x0003598C File Offset: 0x00033B8C
		private static void SendCompleteReadLine(IAsyncResult asyncResult)
		{
			Pop3Client pop3Client = (Pop3Client)asyncResult.AsyncState;
			pop3Client.command.ClearBuffer();
			pop3Client.breadcrumbs.Drop(Pop3Client.Breadcrumbs.LeaveSendNetworkBytes);
			pop3Client.currentAsyncResult.PendingAsyncResult = null;
			object obj;
			pop3Client.nc.EndWrite(asyncResult, out obj);
			if (obj != null)
			{
				pop3Client.HandleError(obj);
				return;
			}
			if (pop3Client.currentAsyncResult.IsCanceled)
			{
				pop3Client.HandleCancelation();
				return;
			}
			Pop3Client.StartReadLine(pop3Client);
		}

		// Token: 0x06001077 RID: 4215 RVA: 0x000359FC File Offset: 0x00033BFC
		private static void CountCommand(AsyncResult<Pop3Client, Pop3ResultData> curOp, bool successful)
		{
			curOp.State.NotifyRoundtripComplete(null, new RemoteServerRoundtripCompleteEventArgs(curOp.State.host, ExDateTime.UtcNow - curOp.State.TimeSent, successful));
		}

		// Token: 0x06001078 RID: 4216 RVA: 0x00035A30 File Offset: 0x00033C30
		private void HandleError(object errorCode)
		{
			Pop3Client.CountCommand(this.currentAsyncResult, false);
			Exception exception;
			if (errorCode is SocketError)
			{
				this.logSession.LogDebugging((TSLID)916UL, Pop3Client.Tracer, (long)this.GetHashCode(), "Pop3Client.HandleError (SocketError={0})", new object[]
				{
					errorCode
				});
				this.disconnectReason = DisconnectReason.Remote;
				this.connectionError = new SocketException((int)errorCode);
				exception = this.connectionError;
			}
			else if (errorCode is SecurityStatus)
			{
				this.logSession.LogDebugging((TSLID)917UL, Pop3Client.Tracer, (long)this.GetHashCode(), "Pop3Client.HandleError (SecurityStatus={0})", new object[]
				{
					errorCode
				});
				this.disconnectReason = DisconnectReason.Local;
				this.connectionError = new TlsFailureException(Strings.TlsFailureErrorOccurred((SecurityStatus)errorCode));
				exception = this.connectionError;
			}
			else if (errorCode is Pop3NonCompliantServerException)
			{
				Pop3NonCompliantServerException ex = (Pop3NonCompliantServerException)errorCode;
				this.disconnectReason = DisconnectReason.Local;
				this.connectionError = ex;
				exception = ex;
			}
			else if (errorCode is MessageSizeLimitExceededException)
			{
				Exception ex2 = (Exception)errorCode;
				this.disconnectReason = DisconnectReason.Local;
				this.connectionError = ex2;
				exception = ex2;
			}
			else
			{
				this.logSession.LogDebugging((TSLID)918UL, Pop3Client.Tracer, (long)this.GetHashCode(), "Pop3Client.HandleError (error={0})", new object[]
				{
					errorCode
				});
				this.disconnectReason = DisconnectReason.Local;
				this.connectionError = new Win32Exception((int)errorCode);
				exception = this.connectionError;
			}
			this.CloseConnectionIfExists();
			this.HandleResult(exception);
		}

		// Token: 0x06001079 RID: 4217 RVA: 0x00035BBC File Offset: 0x00033DBC
		private void HandleResult(Exception exception)
		{
			this.logSession.LogError((TSLID)919UL, Pop3Client.Tracer, "Pop3Client.HandleResult (exception={0})", new object[]
			{
				exception
			});
			Exception exception2;
			if (exception is Pop3ErrorResponseException)
			{
				exception2 = SyncTransientException.CreateOperationLevelException(DetailedAggregationStatus.CommunicationError, exception, true);
			}
			else if (exception is Pop3AuthErrorException)
			{
				exception2 = SyncTransientException.CreateOperationLevelException(DetailedAggregationStatus.AuthenticationError, exception, true);
			}
			else if (exception is Pop3TransientInUseAuthErrorException || exception is Pop3TransientLoginDelayedAuthErrorException || exception is Pop3TransientSystemAuthErrorException)
			{
				exception2 = SyncTransientException.CreateOperationLevelException(DetailedAggregationStatus.AuthenticationError, exception, true);
			}
			else if (exception is SocketException)
			{
				exception2 = SyncTransientException.CreateOperationLevelException(DetailedAggregationStatus.ConnectionError, new Pop3CannotConnectToServerException(exception), true);
			}
			else if (exception is Pop3DisabledResponseException)
			{
				exception2 = SyncTransientException.CreateOperationLevelException(DetailedAggregationStatus.CommunicationError, exception, true);
			}
			else if (exception is Pop3PermErrorResponseException || exception is Pop3InvalidCommandException || exception is ArgumentException)
			{
				exception2 = SyncPermanentException.CreateOperationLevelException(DetailedAggregationStatus.CommunicationError, exception);
			}
			else if (exception is MessageSizeLimitExceededException)
			{
				exception2 = SyncPermanentException.CreateItemLevelException(exception);
			}
			else
			{
				exception2 = SyncTransientException.CreateOperationLevelException(DetailedAggregationStatus.CommunicationError, exception, true);
			}
			this.MoveToEndState();
			this.currentAsyncResult.ProcessCompleted(new Pop3ResultData(), exception2);
		}

		// Token: 0x0600107A RID: 4218 RVA: 0x00035CC4 File Offset: 0x00033EC4
		private void HandleCancelation()
		{
			Pop3Client.CountCommand(this.currentAsyncResult, false);
			this.HandleResult(SyncTransientException.CreateOperationLevelException(DetailedAggregationStatus.CommunicationError, AsyncOperationResult<Pop3ResultData>.CanceledException));
		}

		// Token: 0x0600107B RID: 4219 RVA: 0x00035CE4 File Offset: 0x00033EE4
		private void CloseConnectionIfExists()
		{
			if (this.authContext != null)
			{
				this.authContext.Dispose();
				this.authContext = null;
			}
			if (this.nc != null)
			{
				this.nc.Dispose();
				this.nc = null;
				this.logSession.LogDisconnect((TSLID)920UL, string.Format(CultureInfo.InvariantCulture, "sessionId={0}, reason={1}", new object[]
				{
					this.sessionId,
					this.disconnectReason
				}));
				ConnectionLog.AggregationConnectionStop("POP", this.sessionId, this.remoteEndPoint, this.submittedMessagesCount);
			}
		}

		// Token: 0x0600107C RID: 4220 RVA: 0x00035D85 File Offset: 0x00033F85
		private bool ProcessCompletedOnConnectionError()
		{
			return this.ProcessCompletedOnConnectionError(false);
		}

		// Token: 0x0600107D RID: 4221 RVA: 0x00035D90 File Offset: 0x00033F90
		private bool ProcessCompletedOnConnectionError(bool isEmailRetrieve)
		{
			if (this.connectionError == null)
			{
				return false;
			}
			Exception innerException = isEmailRetrieve ? new ConnectionClosedException(this.connectionError) : this.connectionError;
			this.currentAsyncResult.ProcessCompleted(new Pop3ResultData(), SyncTransientException.CreateOperationLevelException(DetailedAggregationStatus.ConnectionError, innerException, true));
			return true;
		}

		// Token: 0x0600107E RID: 4222 RVA: 0x00035DD7 File Offset: 0x00033FD7
		private void ThrowIfAsyncIOPending()
		{
			if (this.currentAsyncResult != null && this.currentAsyncResult.PendingAsyncResult != null && !this.currentAsyncResult.PendingAsyncResult.IsCompleted)
			{
				throw new InvalidOperationException("The Pop3 client already has pending async I/O(s).");
			}
		}

		// Token: 0x0600107F RID: 4223 RVA: 0x00035E0C File Offset: 0x0003400C
		private void ProcessStates(params Pop3ClientState[] states)
		{
			if (states == null)
			{
				throw new ArgumentNullException("states");
			}
			if (states.Length == 0)
			{
				throw new ArgumentOutOfRangeException("states");
			}
			if (this.enumerator != null && this.enumerator.Current != Pop3ClientState.ProcessEnd)
			{
				throw new InvalidOperationException("The previous async operation request hasnt completed yet.");
			}
			List<Pop3ClientState> list = new List<Pop3ClientState>(states.Length);
			for (int i = 0; i < states.Length; i++)
			{
				list.Add(states[i]);
			}
			this.enumerator = list.GetEnumerator();
			Pop3Client.ProcessState(this);
		}

		// Token: 0x06001080 RID: 4224 RVA: 0x00035E90 File Offset: 0x00034090
		private void MoveToNextState()
		{
			if (this.enumerator != null)
			{
				if (this.enumerator.MoveNext())
				{
					this.state = this.enumerator.Current;
					return;
				}
				this.enumerator.Dispose();
				this.enumerator = null;
			}
			this.state = Pop3ClientState.ProcessEnd;
		}

		// Token: 0x06001081 RID: 4225 RVA: 0x00035EDE File Offset: 0x000340DE
		private void MoveToEndState()
		{
			if (this.enumerator != null)
			{
				this.enumerator.Dispose();
				this.enumerator = null;
			}
			this.state = Pop3ClientState.ProcessEnd;
		}

		// Token: 0x040008E0 RID: 2272
		private const int NumberOfBreadcrumbs = 64;

		// Token: 0x040008E1 RID: 2273
		private const string PopComponentId = "POP";

		// Token: 0x040008E2 RID: 2274
		private const string PopSpnPrefix = "POP/";

		// Token: 0x040008E3 RID: 2275
		private const int MaxResponseLineLength = 32768;

		// Token: 0x040008E4 RID: 2276
		private const int MaxHeaderLineLength = 510;

		// Token: 0x040008E5 RID: 2277
		private const int MaxResponseLineCount = 50000;

		// Token: 0x040008E6 RID: 2278
		private const int MaxRecursionLevel = 20;

		// Token: 0x040008E7 RID: 2279
		private static readonly Trace Tracer = ExTraceGlobals.Pop3ClientTracer;

		// Token: 0x040008E8 RID: 2280
		private static readonly AsyncCallback connectComplete = new AsyncCallback(Pop3Client.ConnectComplete);

		// Token: 0x040008E9 RID: 2281
		private static readonly AsyncCallback negotiateTlsCompleteReadLine = new AsyncCallback(Pop3Client.NegotiateTlsCompleteReadLine);

		// Token: 0x040008EA RID: 2282
		private static readonly AsyncCallback readLineComplete = new AsyncCallback(Pop3Client.ReadLineComplete);

		// Token: 0x040008EB RID: 2283
		private static readonly AsyncCallback readCompleteSendRequest = new AsyncCallback(Pop3Client.ReadCompleteSendRequest);

		// Token: 0x040008EC RID: 2284
		private static readonly AsyncCallback readCompleteConsumeData = new AsyncCallback(Pop3Client.ReadCompleteConsumeData);

		// Token: 0x040008ED RID: 2285
		private static readonly AsyncCallback sendCompleteReadLine = new AsyncCallback(Pop3Client.SendCompleteReadLine);

		// Token: 0x040008EE RID: 2286
		private static readonly byte[] PassBytes = Encoding.ASCII.GetBytes("PASS *****");

		// Token: 0x040008EF RID: 2287
		private static readonly byte[] CrlfBytes = Encoding.ASCII.GetBytes("\r\n");

		// Token: 0x040008F0 RID: 2288
		private static readonly byte[] DotBytes = Encoding.ASCII.GetBytes(".");

		// Token: 0x040008F1 RID: 2289
		private readonly Guid subscriptionGuid;

		// Token: 0x040008F2 RID: 2290
		private readonly string host;

		// Token: 0x040008F3 RID: 2291
		private readonly int port;

		// Token: 0x040008F4 RID: 2292
		private readonly string remoteEndPoint;

		// Token: 0x040008F5 RID: 2293
		private Breadcrumbs<Pop3Client.Breadcrumbs> breadcrumbs = new Breadcrumbs<Pop3Client.Breadcrumbs>(64);

		// Token: 0x040008F6 RID: 2294
		private int recursionLevel;

		// Token: 0x040008F7 RID: 2295
		private int connectionTimeout;

		// Token: 0x040008F8 RID: 2296
		private string username;

		// Token: 0x040008F9 RID: 2297
		private SecureString password;

		// Token: 0x040008FA RID: 2298
		private AuthenticationContext authContext;

		// Token: 0x040008FB RID: 2299
		private Pop3AuthType authType;

		// Token: 0x040008FC RID: 2300
		private Pop3ClientState state;

		// Token: 0x040008FD RID: 2301
		private SyncLogSession logSession;

		// Token: 0x040008FE RID: 2302
		private Socket socket;

		// Token: 0x040008FF RID: 2303
		private NetworkConnection nc;

		// Token: 0x04000900 RID: 2304
		private DisconnectReason disconnectReason;

		// Token: 0x04000901 RID: 2305
		private Pop3Command command;

		// Token: 0x04000902 RID: 2306
		private ExDateTime timeSent;

		// Token: 0x04000903 RID: 2307
		private Pop3Response response;

		// Token: 0x04000904 RID: 2308
		private BufferBuilder responseBuffer;

		// Token: 0x04000905 RID: 2309
		private AsyncOperationResult<Pop3ResultData> resultData;

		// Token: 0x04000906 RID: 2310
		private AsyncResult<Pop3Client, Pop3ResultData> currentAsyncResult;

		// Token: 0x04000907 RID: 2311
		private AsyncResult<Pop3Client, Pop3ResultData> tempAsyncResult;

		// Token: 0x04000908 RID: 2312
		private MessageIdTracker messageIdTracker;

		// Token: 0x04000909 RID: 2313
		private IEnumerator<Pop3ClientState> enumerator;

		// Token: 0x0400090A RID: 2314
		private string sessionId = SyncUtilities.GetNextSessionId();

		// Token: 0x0400090B RID: 2315
		private ulong submittedMessagesCount;

		// Token: 0x0400090C RID: 2316
		private bool checkUniqueIdSupport;

		// Token: 0x0400090D RID: 2317
		private bool seeServerCapabilities;

		// Token: 0x0400090E RID: 2318
		private bool ensureQuit;

		// Token: 0x0400090F RID: 2319
		private Exception connectionError;

		// Token: 0x04000910 RID: 2320
		private int emailDropCount;

		// Token: 0x04000911 RID: 2321
		private SmtpInDataParser dotStuffedParser;

		// Token: 0x04000912 RID: 2322
		private string userLegacyDN;

		// Token: 0x04000913 RID: 2323
		private int authNtlmResponseCount;

		// Token: 0x020001EF RID: 495
		private enum Breadcrumbs
		{
			// Token: 0x04000919 RID: 2329
			EnterGetUniqueIds = 1,
			// Token: 0x0400091A RID: 2330
			EnterGetEmail,
			// Token: 0x0400091B RID: 2331
			EnterDeleteEmails,
			// Token: 0x0400091C RID: 2332
			EnterQuit,
			// Token: 0x0400091D RID: 2333
			EnterVerifyAccount,
			// Token: 0x0400091E RID: 2334
			EnterReadNetworkBytes,
			// Token: 0x0400091F RID: 2335
			EnterSendNetworkBytes,
			// Token: 0x04000920 RID: 2336
			LeaveGetUniqueIds = 21,
			// Token: 0x04000921 RID: 2337
			LeaveGetEmail,
			// Token: 0x04000922 RID: 2338
			LeaveDeleteEmails,
			// Token: 0x04000923 RID: 2339
			LeaveQuit,
			// Token: 0x04000924 RID: 2340
			LeaveVerifyAccount,
			// Token: 0x04000925 RID: 2341
			LeaveReadNetworkBytes,
			// Token: 0x04000926 RID: 2342
			LeaveSendNetworkBytes
		}
	}
}
