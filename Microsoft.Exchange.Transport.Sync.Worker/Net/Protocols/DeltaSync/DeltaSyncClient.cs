using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ContentAggregation;
using Microsoft.Exchange.Diagnostics.Components.Net;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.MailboxTransport.ContentAggregation;
using Microsoft.Exchange.Net.LiveIDAuthentication;
using Microsoft.Exchange.Net.Logging;
using Microsoft.Exchange.Net.WebApplicationClient;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Common.Logging;
using Microsoft.Exchange.Transport.Sync.Worker.Framework;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync
{
	// Token: 0x0200005E RID: 94
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class DeltaSyncClient : ProtocolClient, IDeltaSyncClient, IDisposable
	{
		// Token: 0x06000481 RID: 1153 RVA: 0x0001546C File Offset: 0x0001366C
		internal DeltaSyncClient(DeltaSyncUserAccount userAccount, int timeout, IWebProxy proxy, long maxDownloadSizePerMessage, ProtocolLog httpProtocolLog) : this(userAccount, timeout, proxy, maxDownloadSizePerMessage, httpProtocolLog, CommonLoggingHelper.SyncLogSession, null)
		{
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x00015484 File Offset: 0x00013684
		internal DeltaSyncClient(DeltaSyncUserAccount userAccount, int timeout, IWebProxy proxy, long maxDownloadSizePerMessage, ProtocolLog httpProtocolLog, SyncLogSession syncLogSession, EventHandler<RoundtripCompleteEventArgs> roundtripCompleteEventHandler)
		{
			SyncUtilities.ThrowIfArgumentNull("userAccount", userAccount);
			SyncUtilities.ThrowIfArgumentNull("syncLogSession", syncLogSession);
			this.userAccount = userAccount;
			this.httpClient = new HttpClient();
			this.httpClient.SendingRequest += DeltaSyncClient.SetupCertificateValidation;
			this.deltaSyncRequestGenerator = new DeltaSyncRequestGenerator();
			this.deltaSyncResponseHandler = new DeltaSyncResponseHandler(syncLogSession);
			if (roundtripCompleteEventHandler != null)
			{
				this.RoundtripComplete += roundtripCompleteEventHandler;
			}
			this.httpSessionConfig = new HttpSessionConfig();
			this.httpSessionConfig.Timeout = timeout;
			this.httpSessionConfig.AllowAutoRedirect = true;
			this.httpSessionConfig.Proxy = proxy;
			this.httpSessionConfig.UserAgent = "ExchangeHostedServices/1.0";
			this.httpSessionConfig.Method = "POST";
			this.httpSessionConfig.KeepAlive = true;
			this.httpSessionConfig.MaximumResponseBodyLength = -1L;
			this.httpSessionConfig.ProtocolLog = httpProtocolLog;
			this.maxDownloadSizePerMessage = maxDownloadSizePerMessage;
			this.requestStream = TemporaryStorage.Create();
			this.syncLogSession = syncLogSession;
		}

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x06000483 RID: 1155 RVA: 0x00015598 File Offset: 0x00013798
		// (remove) Token: 0x06000484 RID: 1156 RVA: 0x000155D0 File Offset: 0x000137D0
		private event EventHandler<RoundtripCompleteEventArgs> RoundtripComplete;

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000485 RID: 1157 RVA: 0x00015605 File Offset: 0x00013805
		// (set) Token: 0x06000486 RID: 1158 RVA: 0x0001560C File Offset: 0x0001380C
		internal static Uri RstLiveEndpointUri
		{
			get
			{
				return DeltaSyncClient.rstLiveEndpointUri;
			}
			set
			{
				DeltaSyncClient.rstLiveEndpointUri = value;
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000487 RID: 1159 RVA: 0x00015614 File Offset: 0x00013814
		internal ExDateTime TimeSent
		{
			get
			{
				base.CheckDisposed();
				return this.timeSent;
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000488 RID: 1160 RVA: 0x00015624 File Offset: 0x00013824
		// (set) Token: 0x06000489 RID: 1161 RVA: 0x0001567C File Offset: 0x0001387C
		private LiveIDAuthenticationClient AuthenticationClient
		{
			get
			{
				if (this.authenticationClient == null)
				{
					this.authenticationClient = new LiveIDAuthenticationClient(this.httpSessionConfig.Timeout, this.httpSessionConfig.Proxy, DeltaSyncClient.RstLiveEndpointUri);
					this.authenticationClient.SendingRequest += DeltaSyncClient.SetupCertificateValidation;
				}
				return this.authenticationClient;
			}
			set
			{
				this.authenticationClient = value;
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x0600048A RID: 1162 RVA: 0x00015685 File Offset: 0x00013885
		private AlternateWlidEndpointHandler AlternateWlidEndpointHandler
		{
			get
			{
				if (this.alternateWlidEndpointHandler == null)
				{
					this.alternateWlidEndpointHandler = new AlternateWlidEndpointHandler("DeltaSyncAlternateWlidEndpoint", this.syncLogSession, DeltaSyncClient.Tracer);
				}
				return this.alternateWlidEndpointHandler;
			}
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x000156B0 File Offset: 0x000138B0
		public IAsyncResult BeginGetChanges(AsyncCallback callback, object asyncState, object syncPoisonContext)
		{
			return this.BeginGetChanges(2000, callback, asyncState, syncPoisonContext);
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x000156C0 File Offset: 0x000138C0
		public IAsyncResult BeginGetChanges(int windowSize, AsyncCallback callback, object asyncState, object syncPoisonContext)
		{
			base.CheckDisposed();
			this.ThrowIfPending();
			this.syncLogSession.LogDebugging((TSLID)647UL, DeltaSyncClient.Tracer, "Begin Get Changes [User:{0}] [Window Size:{1}]", new object[]
			{
				this.userAccount,
				windowSize
			});
			this.sessionClosed = false;
			this.isLatestToken = false;
			this.commandType = DeltaSyncCommandType.Sync;
			AsyncResult<DeltaSyncClient, DeltaSyncResultData> asyncResult = new AsyncResult<DeltaSyncClient, DeltaSyncResultData>(this, this, callback, asyncState, syncPoisonContext);
			this.SetupGetChangesRequestStream((windowSize > 0) ? windowSize : 2000);
			this.InitializeRequestProperties(DeltaSyncCommon.TextXmlContentType, -1L);
			this.BeginRequest(asyncResult, false);
			return asyncResult;
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x0001575C File Offset: 0x0001395C
		public IAsyncResult BeginApplyChanges(List<DeltaSyncOperation> deltaSyncOperations, ConflictResolution conflictResolution, AsyncCallback callback, object asyncState, object syncPoisonContext)
		{
			base.CheckDisposed();
			this.ThrowIfPending();
			SyncUtilities.ThrowIfArgumentNull("deltaSyncOperations", deltaSyncOperations);
			this.syncLogSession.LogDebugging((TSLID)648UL, DeltaSyncClient.Tracer, "Begin Apply Changes [User:{0}]", new object[]
			{
				this.userAccount
			});
			this.sessionClosed = false;
			this.isLatestToken = false;
			this.commandType = DeltaSyncCommandType.Sync;
			AsyncResult<DeltaSyncClient, DeltaSyncResultData> asyncResult = new AsyncResult<DeltaSyncClient, DeltaSyncResultData>(this, this, callback, asyncState, syncPoisonContext);
			this.SetupApplyChangesRequestStream(deltaSyncOperations, conflictResolution);
			this.InitializeRequestProperties(DeltaSyncCommon.ApplicationXopXmlContentType, -1L);
			this.BeginRequest(asyncResult, false);
			return asyncResult;
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x000157F4 File Offset: 0x000139F4
		public IAsyncResult BeginSendMessage(DeltaSyncMail deltaSyncEmail, bool saveInSentItems, DeltaSyncRecipients deltaSyncRecipients, AsyncCallback callback, object asyncState, object syncPoisonContext)
		{
			base.CheckDisposed();
			this.ThrowIfPending();
			SyncUtilities.ThrowIfArgumentNull("deltaSyncEmail", deltaSyncEmail);
			SyncUtilities.ThrowIfArgumentNull("deltaSyncRecipients", deltaSyncRecipients);
			if (deltaSyncRecipients.Count < 1)
			{
				throw new ArgumentOutOfRangeException("deltaSyncRecipients", "there must be at least one recipient");
			}
			this.syncLogSession.LogDebugging((TSLID)649UL, DeltaSyncClient.Tracer, "Begin Send Message [User:{0}]", new object[]
			{
				this.userAccount
			});
			this.sessionClosed = false;
			this.isLatestToken = false;
			this.commandType = DeltaSyncCommandType.Send;
			AsyncResult<DeltaSyncClient, DeltaSyncResultData> asyncResult = new AsyncResult<DeltaSyncClient, DeltaSyncResultData>(this, this, callback, asyncState, syncPoisonContext);
			this.SetupSendMessageRequestStream(deltaSyncEmail, saveInSentItems, deltaSyncRecipients);
			this.InitializeRequestProperties(DeltaSyncCommon.ApplicationXopXmlContentType, -1L);
			this.BeginRequest(asyncResult, false);
			return asyncResult;
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x000158B0 File Offset: 0x00013AB0
		public IAsyncResult BeginFetchMessage(Guid serverId, AsyncCallback callback, object asyncState, object syncPoisonContext)
		{
			base.CheckDisposed();
			this.ThrowIfPending();
			this.syncLogSession.LogDebugging((TSLID)650UL, DeltaSyncClient.Tracer, "Begin Fetch Message [User:{0}] [ServerId: {1}]", new object[]
			{
				this.userAccount,
				serverId
			});
			Microsoft.Exchange.Diagnostics.Components.ContentAggregation.ExTraceGlobals.FaultInjectionTracer.TraceTest(2724605245U);
			this.sessionClosed = false;
			this.isLatestToken = false;
			this.commandType = DeltaSyncCommandType.Fetch;
			AsyncResult<DeltaSyncClient, DeltaSyncResultData> asyncResult = new AsyncResult<DeltaSyncClient, DeltaSyncResultData>(this, this, callback, asyncState, syncPoisonContext);
			this.SetupFetchMessageRequestStream(serverId);
			this.InitializeRequestProperties(DeltaSyncCommon.TextXmlContentType, this.maxDownloadSizePerMessage);
			this.BeginRequest(asyncResult, false);
			return asyncResult;
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x00015954 File Offset: 0x00013B54
		public IAsyncResult BeginVerifyAccount(AsyncCallback callback, object asyncState, object syncPoisonContext)
		{
			base.CheckDisposed();
			this.syncLogSession.LogDebugging((TSLID)651UL, DeltaSyncClient.Tracer, "Begin Verify Account [User:{0}]", new object[]
			{
				this.userAccount
			});
			return this.BeginGetSettings(callback, asyncState, syncPoisonContext);
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x000159A1 File Offset: 0x00013BA1
		public IAsyncResult BeginGetSettings(AsyncCallback callback, object asyncState, object syncPoisonContext)
		{
			return this.BeginRequest(new DeltaSyncClient.SetupGetRequest(this.SetupGetSettingsRequestStream), DeltaSyncCommandType.Settings, callback, asyncState, syncPoisonContext);
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x000159B9 File Offset: 0x00013BB9
		public IAsyncResult BeginGetStatistics(AsyncCallback callback, object asyncState, object syncPoisonContext)
		{
			return this.BeginRequest(new DeltaSyncClient.SetupGetRequest(this.SetupGetStatisticsRequestStream), DeltaSyncCommandType.Stateless, callback, asyncState, syncPoisonContext);
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x000159D4 File Offset: 0x00013BD4
		public AsyncOperationResult<DeltaSyncResultData> EndVerifyAccount(IAsyncResult asyncResult)
		{
			base.CheckDisposed();
			this.syncLogSession.LogDebugging((TSLID)653UL, DeltaSyncClient.Tracer, "End Verify Account [User:{0}]", new object[]
			{
				this.userAccount
			});
			return this.EndGetSettings(asyncResult);
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x00015A20 File Offset: 0x00013C20
		public AsyncOperationResult<DeltaSyncResultData> EndGetChanges(IAsyncResult asyncResult)
		{
			base.CheckDisposed();
			this.syncLogSession.LogDebugging((TSLID)654UL, DeltaSyncClient.Tracer, "End Get Changes [User:{0}]", new object[]
			{
				this.userAccount
			});
			AsyncResult<DeltaSyncClient, DeltaSyncResultData> asyncResult2 = (AsyncResult<DeltaSyncClient, DeltaSyncResultData>)asyncResult;
			return asyncResult2.WaitForCompletion();
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x00015A74 File Offset: 0x00013C74
		public AsyncOperationResult<DeltaSyncResultData> EndApplyChanges(IAsyncResult asyncResult)
		{
			base.CheckDisposed();
			this.syncLogSession.LogDebugging((TSLID)655UL, DeltaSyncClient.Tracer, "End Apply Changes [User:{0}]", new object[]
			{
				this.userAccount
			});
			AsyncResult<DeltaSyncClient, DeltaSyncResultData> asyncResult2 = (AsyncResult<DeltaSyncClient, DeltaSyncResultData>)asyncResult;
			return asyncResult2.WaitForCompletion();
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x00015AC8 File Offset: 0x00013CC8
		public AsyncOperationResult<DeltaSyncResultData> EndSendMessage(IAsyncResult asyncResult)
		{
			base.CheckDisposed();
			this.syncLogSession.LogDebugging((TSLID)656UL, DeltaSyncClient.Tracer, "End Send Message [User:{0}]", new object[]
			{
				this.userAccount
			});
			AsyncResult<DeltaSyncClient, DeltaSyncResultData> asyncResult2 = (AsyncResult<DeltaSyncClient, DeltaSyncResultData>)asyncResult;
			return asyncResult2.WaitForCompletion();
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x00015B1C File Offset: 0x00013D1C
		public AsyncOperationResult<DeltaSyncResultData> EndFetchMessage(IAsyncResult asyncResult)
		{
			base.CheckDisposed();
			this.syncLogSession.LogDebugging((TSLID)657UL, DeltaSyncClient.Tracer, "End Fetch Message [User:{0}]", new object[]
			{
				this.userAccount
			});
			AsyncResult<DeltaSyncClient, DeltaSyncResultData> asyncResult2 = (AsyncResult<DeltaSyncClient, DeltaSyncResultData>)asyncResult;
			return asyncResult2.WaitForCompletion();
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x00015B70 File Offset: 0x00013D70
		public AsyncOperationResult<DeltaSyncResultData> EndGetSettings(IAsyncResult asyncResult)
		{
			base.CheckDisposed();
			this.syncLogSession.LogDebugging((TSLID)658UL, DeltaSyncClient.Tracer, "End Get Settings [User:{0}]", new object[]
			{
				this.userAccount
			});
			AsyncResult<DeltaSyncClient, DeltaSyncResultData> asyncResult2 = (AsyncResult<DeltaSyncClient, DeltaSyncResultData>)asyncResult;
			return asyncResult2.WaitForCompletion();
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x00015BC4 File Offset: 0x00013DC4
		public AsyncOperationResult<DeltaSyncResultData> EndGetStatistics(IAsyncResult asyncResult)
		{
			base.CheckDisposed();
			this.syncLogSession.LogDebugging((TSLID)298UL, DeltaSyncClient.Tracer, "End Get Statistics [User:{0}]", new object[]
			{
				this.userAccount
			});
			AsyncResult<DeltaSyncClient, DeltaSyncResultData> asyncResult2 = (AsyncResult<DeltaSyncClient, DeltaSyncResultData>)asyncResult;
			return asyncResult2.WaitForCompletion();
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x00015C15 File Offset: 0x00013E15
		public void SubscribeDownloadCompletedEvent(EventHandler<DownloadCompleteEventArgs> eventHandler)
		{
			base.CheckDisposed();
			this.httpClient.DownloadCompleted += eventHandler;
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x00015C29 File Offset: 0x00013E29
		public void NotifyRoundtripComplete(object sender, RoundtripCompleteEventArgs roundtripCompleteEventArgs)
		{
			base.CheckDisposed();
			if (this.RoundtripComplete != null)
			{
				this.RoundtripComplete(sender, roundtripCompleteEventArgs);
			}
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x00015C48 File Offset: 0x00013E48
		internal override bool TryCancel()
		{
			base.CheckDisposed();
			bool result;
			lock (this.syncRoot)
			{
				if (this.TryStopProcess())
				{
					if (this.pendingAsyncResult != null)
					{
						this.pendingAsyncResult.Cancel();
						this.pendingAsyncResult = null;
					}
					this.syncLogSession.LogDebugging((TSLID)659UL, DeltaSyncClient.Tracer, "Pending Operation Cancelled", new object[0]);
					result = true;
				}
				else
				{
					result = false;
				}
			}
			return result;
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x00015CE0 File Offset: 0x00013EE0
		protected override void InternalDispose(bool disposing)
		{
			lock (this.syncRoot)
			{
				if (disposing)
				{
					this.TryCancel();
					if (this.requestStream != null)
					{
						this.requestStream.Dispose();
						this.requestStream = null;
					}
					if (this.httpClient != null)
					{
						this.httpClient.Dispose();
						this.httpClient = null;
					}
					if (this.AuthenticationClient != null)
					{
						this.AuthenticationClient.Dispose();
						this.AuthenticationClient = null;
					}
					this.syncLogSession.LogDebugging((TSLID)660UL, DeltaSyncClient.Tracer, base.GetType().Name + " Disposed", new object[0]);
				}
			}
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x00015DAC File Offset: 0x00013FAC
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<DeltaSyncClient>(this);
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x00015DB4 File Offset: 0x00013FB4
		private static bool TryLoadServiceEndpoints(out Exception exception, SyncLogSession syncLogSession)
		{
			exception = null;
			if (!DeltaSyncClient.serviceEndpointsLoaded)
			{
				lock (DeltaSyncClient.serviceEndpointSyncLock)
				{
					if (!DeltaSyncClient.serviceEndpointsLoaded)
					{
						try
						{
							Microsoft.Exchange.Diagnostics.Components.ContentAggregation.ExTraceGlobals.FaultInjectionTracer.TraceTest(3739626813U);
							ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 945, "TryLoadServiceEndpoints", "f:\\15.00.1497\\sources\\dev\\transportSync\\src\\Worker\\Framework\\Provider\\DeltaSync\\Client\\DeltaSyncClient.cs");
							ServiceEndpointContainer endpointContainer = topologyConfigurationSession.GetEndpointContainer();
							ServiceEndpoint endpoint = endpointContainer.GetEndpoint(ServiceEndpointId.DeltaSyncPartnerItemOperations);
							ServiceEndpoint endpoint2 = endpointContainer.GetEndpoint(ServiceEndpointId.DeltaSyncPartnerSend);
							ServiceEndpoint endpoint3 = endpointContainer.GetEndpoint(ServiceEndpointId.DeltaSyncPartnerSettings);
							ServiceEndpoint endpoint4 = endpointContainer.GetEndpoint(ServiceEndpointId.DeltaSyncPartnerSync);
							ServiceEndpoint endpoint5 = endpointContainer.GetEndpoint(ServiceEndpointId.DeltaSyncPartnerStateless);
							DeltaSyncClient.partnerClientToken = endpoint4.Token;
							syncLogSession.LogDebugging((TSLID)492UL, DeltaSyncClient.Tracer, "Delta Sync Partner Client Token Loaded: {0}", new object[]
							{
								DeltaSyncClient.partnerClientToken
							});
							if (DeltaSyncClient.RstLiveEndpointUri == null)
							{
								ServiceEndpoint endpoint6 = endpointContainer.GetEndpoint(ServiceEndpointId.LiveServiceLogin1);
								DeltaSyncClient.RstLiveEndpointUri = endpoint6.Uri;
							}
							DeltaSyncClient.partnerItemOperationsUri = endpoint.Uri;
							DeltaSyncClient.partnerSendUri = endpoint2.Uri;
							DeltaSyncClient.partnerSettingsUri = endpoint3.Uri;
							DeltaSyncClient.partnerSyncUri = endpoint4.Uri;
							DeltaSyncClient.partnerStatelessUri = endpoint5.Uri;
							ServiceEndpoint endpoint7 = endpointContainer.GetEndpoint(ServiceEndpointId.DeltaSyncUserItemOperations);
							ServiceEndpoint endpoint8 = endpointContainer.GetEndpoint(ServiceEndpointId.DeltaSyncUserSend);
							ServiceEndpoint endpoint9 = endpointContainer.GetEndpoint(ServiceEndpointId.DeltaSyncUserSettings);
							ServiceEndpoint endpoint10 = endpointContainer.GetEndpoint(ServiceEndpointId.DeltaSyncUserSync);
							ServiceEndpoint endpoint11 = endpointContainer.GetEndpoint(ServiceEndpointId.DeltaSyncUserStateless);
							DeltaSyncClient.passportItemOperationsUri = endpoint7.Uri;
							DeltaSyncClient.passportSendUri = endpoint8.Uri;
							DeltaSyncClient.passportSettingsUri = endpoint9.Uri;
							DeltaSyncClient.passportSyncUri = endpoint10.Uri;
							DeltaSyncClient.passportStatelessUri = endpoint11.Uri;
							syncLogSession.LogDebugging((TSLID)661UL, DeltaSyncClient.Tracer, "Delta Sync Partner Uris Loaded: Sync:{0}, Send:{1}, ItemOperations:{2}, Settings:{3} , Stateless:{4}", new object[]
							{
								DeltaSyncClient.partnerSyncUri,
								DeltaSyncClient.partnerSendUri,
								DeltaSyncClient.partnerItemOperationsUri,
								DeltaSyncClient.partnerSettingsUri,
								DeltaSyncClient.partnerStatelessUri
							});
							syncLogSession.LogDebugging((TSLID)662UL, DeltaSyncClient.Tracer, "Delta Sync Passport Uris Loaded. Sync:{0}, Send:{1}, ItemOperations:{2}, Settings:{3} , Stateless:{4}", new object[]
							{
								DeltaSyncClient.passportSyncUri,
								DeltaSyncClient.passportSendUri,
								DeltaSyncClient.passportItemOperationsUri,
								DeltaSyncClient.passportSettingsUri,
								DeltaSyncClient.passportStatelessUri
							});
							DeltaSyncClient.serviceEndpointsLoaded = true;
							return true;
						}
						catch (ServiceEndpointNotFoundException innerException)
						{
							exception = new DeltaSyncServiceEndpointsLoadException(innerException);
						}
						catch (EndpointContainerNotFoundException innerException2)
						{
							exception = new DeltaSyncServiceEndpointsLoadException(innerException2);
						}
						catch (ADTransientException innerException3)
						{
							exception = new DeltaSyncServiceEndpointsLoadException(innerException3);
						}
						catch (ADOperationException innerException4)
						{
							exception = new DeltaSyncServiceEndpointsLoadException(innerException4);
						}
						catch (DataValidationException innerException5)
						{
							exception = new DeltaSyncServiceEndpointsLoadException(innerException5);
						}
						syncLogSession.LogError((TSLID)663UL, DeltaSyncClient.Tracer, "Unable to load Service Endpoints for Delta Sync, excpetion: {0}", new object[]
						{
							exception
						});
						return false;
					}
				}
				return true;
			}
			return true;
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x00016148 File Offset: 0x00014348
		private static bool SslCertificateValidationCallback(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslError)
		{
			return sslError == SslPolicyErrors.None || sslError == SslPolicyErrors.RemoteCertificateNameMismatch;
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x00016154 File Offset: 0x00014354
		private static void SetupCertificateValidation(object sender, HttpWebRequestEventArgs e)
		{
			CertificateValidationManager.SetComponentId(e.Request, "MicrosoftExchangeServer-DeltaSyncClient");
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x00016166 File Offset: 0x00014366
		private static void CountCommand(AsyncResult<DeltaSyncClient, DeltaSyncResultData> curOp, string remoteServerName, bool successful)
		{
			curOp.State.NotifyRoundtripComplete(null, new RemoteServerRoundtripCompleteEventArgs(remoteServerName, ExDateTime.UtcNow - curOp.State.TimeSent, successful));
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x00016190 File Offset: 0x00014390
		private void BeginRequest(AsyncResult<DeltaSyncClient, DeltaSyncResultData> deltaSyncAsyncResult, bool forceAuth)
		{
			Exception exception = null;
			this.timeSent = ExDateTime.UtcNow;
			if (!DeltaSyncClient.TryLoadServiceEndpoints(out exception, this.syncLogSession))
			{
				this.HandleResult(deltaSyncAsyncResult, null, exception);
				return;
			}
			this.userAccount.PartnerClientToken = DeltaSyncClient.partnerClientToken;
			CertificateValidationManager.RegisterCallback("MicrosoftExchangeServer-DeltaSyncClient", new RemoteCertificateValidationCallback(DeltaSyncClient.SslCertificateValidationCallback));
			if (forceAuth || this.userAccount.NeedsAuthentication)
			{
				this.BeginAuthRequest(deltaSyncAsyncResult);
				this.isLatestToken = true;
				return;
			}
			this.BeginDeltaSyncRequest(deltaSyncAsyncResult);
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x0001620F File Offset: 0x0001440F
		private void InitializeRequestProperties(string requestContentType, long maxDownloadLimit)
		{
			this.httpSessionConfig.ContentType = requestContentType;
			this.httpSessionConfig.RequestStream = this.requestStream;
			this.httpSessionConfig.MaximumResponseBodyLength = maxDownloadLimit;
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x0001623C File Offset: 0x0001443C
		private void BeginAuthRequest(AsyncResult<DeltaSyncClient, DeltaSyncResultData> deltaSyncAsyncResult)
		{
			this.ThrowIfPending();
			this.AlternateWlidEndpointHandler.SetWlidEndpoint(this.AuthenticationClient);
			ICancelableAsyncResult asyncResult = this.AuthenticationClient.BeginGetToken("MicrosoftExchangeServer-DeltaSyncClient", this.userAccount.Username, this.userAccount.Password, this.userAccount.AuthPolicy, DeltaSyncClient.passportSyncUri.Host, deltaSyncAsyncResult.GetCancelableAsyncCallbackWithPoisonContextAndUnhandledExceptionRedirect(new CancelableAsyncCallback(this.AuthCallback)), deltaSyncAsyncResult);
			this.CachePendingAsyncResult(asyncResult);
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x000162B8 File Offset: 0x000144B8
		private void CachePendingAsyncResult(ICancelableAsyncResult asyncResult)
		{
			if (!asyncResult.IsCompleted)
			{
				lock (this.syncRoot)
				{
					if (!asyncResult.IsCompleted)
					{
						this.pendingAsyncResult = asyncResult;
					}
				}
			}
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x0001630C File Offset: 0x0001450C
		private void BeginDeltaSyncRequest(AsyncResult<DeltaSyncClient, DeltaSyncResultData> deltaSyncAsyncResult)
		{
			this.ThrowIfPending();
			Uri deltaSyncRequestUri = this.GetDeltaSyncRequestUri();
			Uri uri = new UriBuilder(deltaSyncRequestUri)
			{
				Query = string.Empty
			}.Uri;
			this.syncLogSession.LogDebugging((TSLID)667UL, DeltaSyncClient.Tracer, "Using Delta Sync Request Uri:{0} for commandType: {1}", new object[]
			{
				uri,
				this.commandType
			});
			if (this.httpSessionConfig.Headers == null)
			{
				this.httpSessionConfig.Headers = new WebHeaderCollection();
			}
			this.httpSessionConfig.Headers[HttpRequestHeader.Cookie] = this.deltaSyncCookie;
			ICancelableAsyncResult asyncResult = this.httpClient.BeginDownload(deltaSyncRequestUri, this.httpSessionConfig, deltaSyncAsyncResult.GetCancelableAsyncCallbackWithPoisonContextAndUnhandledExceptionRedirect(new CancelableAsyncCallback(this.DeltaSyncResponseCallback)), deltaSyncAsyncResult);
			this.CachePendingAsyncResult(asyncResult);
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x000163E0 File Offset: 0x000145E0
		private Uri GetDeltaSyncRequestUri()
		{
			Uri uriBasedOnCommandTypeAndAuthType = this.GetUriBasedOnCommandTypeAndAuthType();
			UriBuilder uriBuilder = new UriBuilder(uriBasedOnCommandTypeAndAuthType);
			if (!string.IsNullOrEmpty(this.userAccount.DeltaSyncServer))
			{
				uriBuilder.Host = this.userAccount.DeltaSyncServer;
			}
			uriBuilder.Query = this.userAccount.GetRequestQueryString();
			return uriBuilder.Uri;
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x00016438 File Offset: 0x00014638
		private Uri GetUriBasedOnCommandTypeAndAuthType()
		{
			if (this.userAccount.PassportAuthenticationEnabled)
			{
				switch (this.commandType)
				{
				case DeltaSyncCommandType.Sync:
					return DeltaSyncClient.passportSyncUri;
				case DeltaSyncCommandType.Fetch:
					return DeltaSyncClient.passportItemOperationsUri;
				case DeltaSyncCommandType.Settings:
					return DeltaSyncClient.passportSettingsUri;
				case DeltaSyncCommandType.Send:
					return DeltaSyncClient.passportSendUri;
				case DeltaSyncCommandType.Stateless:
					return DeltaSyncClient.passportStatelessUri;
				default:
					throw new InvalidOperationException("Unknown command type: " + this.commandType);
				}
			}
			else
			{
				switch (this.commandType)
				{
				case DeltaSyncCommandType.Sync:
					return DeltaSyncClient.partnerSyncUri;
				case DeltaSyncCommandType.Fetch:
					return DeltaSyncClient.partnerItemOperationsUri;
				case DeltaSyncCommandType.Settings:
					return DeltaSyncClient.partnerSettingsUri;
				case DeltaSyncCommandType.Send:
					return DeltaSyncClient.partnerSendUri;
				case DeltaSyncCommandType.Stateless:
					return DeltaSyncClient.partnerStatelessUri;
				default:
					throw new InvalidOperationException("Unknown command type: " + this.commandType);
				}
			}
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x0001650C File Offset: 0x0001470C
		private void SetupGetChangesRequestStream(int windowSize)
		{
			this.requestStream.Position = 0L;
			this.deltaSyncRequestGenerator.SetupGetChangesRequest(this.userAccount.FolderSyncKey, this.userAccount.EmailSyncKey, windowSize, this.requestStream);
			this.requestStream.SetLength(this.requestStream.Position);
			this.requestStream.Position = 0L;
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x00016574 File Offset: 0x00014774
		private void SetupApplyChangesRequestStream(List<DeltaSyncOperation> operations, ConflictResolution conflictResolution)
		{
			this.requestStream.Position = 0L;
			this.deltaSyncRequestGenerator.SetupApplyChangesRequest(operations, conflictResolution, this.userAccount.FolderSyncKey, this.userAccount.EmailSyncKey, this.requestStream);
			this.requestStream.SetLength(this.requestStream.Position);
			this.requestStream.Position = 0L;
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x000165DC File Offset: 0x000147DC
		private void SetupSendMessageRequestStream(DeltaSyncMail deltaSyncEmail, bool saveInSentItems, DeltaSyncRecipients recipients)
		{
			this.requestStream.Position = 0L;
			this.deltaSyncRequestGenerator.SetupSendMessageRequest(deltaSyncEmail, saveInSentItems, recipients, this.requestStream);
			this.requestStream.SetLength(this.requestStream.Position);
			this.requestStream.Position = 0L;
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x00016630 File Offset: 0x00014830
		private void SetupFetchMessageRequestStream(Guid serverId)
		{
			this.requestStream.Position = 0L;
			this.deltaSyncRequestGenerator.SetupFetchMessageRequest(serverId, this.requestStream);
			this.requestStream.SetLength(this.requestStream.Position);
			this.requestStream.Position = 0L;
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x0001667F File Offset: 0x0001487F
		private void SetupGetSettingsRequestStream()
		{
			this.SetupRequestStream(new DeltaSyncClient.SetupRequest(this.deltaSyncRequestGenerator.SetupGetSettingsRequest));
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x00016698 File Offset: 0x00014898
		private void SetupGetStatisticsRequestStream()
		{
			this.SetupRequestStream(new DeltaSyncClient.SetupRequest(this.deltaSyncRequestGenerator.SetupGetStatisticsRequest));
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x000166B1 File Offset: 0x000148B1
		private void SetupRequestStream(DeltaSyncClient.SetupRequest setupRequest)
		{
			this.requestStream.Position = 0L;
			setupRequest(this.requestStream);
			this.requestStream.SetLength(this.requestStream.Position);
			this.requestStream.Position = 0L;
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x000166F0 File Offset: 0x000148F0
		private IAsyncResult BeginRequest(DeltaSyncClient.SetupGetRequest getRequest, DeltaSyncCommandType cmdType, AsyncCallback callback, object asyncState, object syncPoisonContext)
		{
			base.CheckDisposed();
			this.ThrowIfPending();
			this.syncLogSession.LogDebugging((TSLID)652UL, DeltaSyncClient.Tracer, "Begin Get request [User:{0}]", new object[]
			{
				this.userAccount
			});
			this.sessionClosed = false;
			this.isLatestToken = false;
			this.commandType = cmdType;
			AsyncResult<DeltaSyncClient, DeltaSyncResultData> asyncResult = new AsyncResult<DeltaSyncClient, DeltaSyncResultData>(this, this, callback, asyncState, syncPoisonContext);
			getRequest();
			this.InitializeRequestProperties(DeltaSyncCommon.TextXmlContentType, -1L);
			this.BeginRequest(asyncResult, false);
			return asyncResult;
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x00016778 File Offset: 0x00014978
		private void AuthCallback(ICancelableAsyncResult asyncResult)
		{
			AsyncResult<DeltaSyncClient, DeltaSyncResultData> asyncResult2 = null;
			Exception exception = null;
			bool flag = false;
			lock (this.syncRoot)
			{
				if (this.sessionClosed)
				{
					return;
				}
				this.pendingAsyncResult = null;
				asyncResult2 = (AsyncResult<DeltaSyncClient, DeltaSyncResultData>)asyncResult.AsyncState;
				if (asyncResult.CompletedSynchronously)
				{
					asyncResult2.SetCompletedSynchronously();
				}
				AuthenticationResult authenticationResult = this.AuthenticationClient.EndGetToken(asyncResult);
				if (authenticationResult.IsSucceeded)
				{
					this.userAccount.AuthToken = authenticationResult.Token;
					this.userAccount.Puid = authenticationResult.Token.PUID;
					flag = true;
				}
				else
				{
					exception = authenticationResult.Exception;
				}
			}
			if (flag)
			{
				this.BeginDeltaSyncRequest(asyncResult2);
				return;
			}
			this.HandleResult(asyncResult2, null, exception);
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x00016848 File Offset: 0x00014A48
		private void DeltaSyncResponseCallback(ICancelableAsyncResult asyncResult)
		{
			AsyncResult<DeltaSyncClient, DeltaSyncResultData> asyncResult2 = null;
			DeltaSyncResultData deltaSyncResultData = null;
			bool flag = false;
			Exception exception = null;
			lock (this.syncRoot)
			{
				if (this.sessionClosed)
				{
					return;
				}
				this.pendingAsyncResult = null;
				asyncResult2 = (AsyncResult<DeltaSyncClient, DeltaSyncResultData>)asyncResult.AsyncState;
				if (asyncResult.CompletedSynchronously)
				{
					asyncResult2.SetCompletedSynchronously();
				}
				DownloadResult deltaSyncResponse = this.httpClient.EndDownload(asyncResult);
				string host = deltaSyncResponse.LastKnownRequestedUri.Host;
				if (deltaSyncResponse.IsSucceeded)
				{
					DeltaSyncClient.CountCommand(asyncResult2, host, true);
					this.userAccount.DeltaSyncServer = host;
					if (!string.IsNullOrEmpty(host))
					{
						EndPointHealth.UpdateDeltaSyncEndPointStatus(host, true, this.syncLogSession);
						this.syncLogSession.LogVerbose((TSLID)1298UL, "Connected to host: {0}", new object[]
						{
							host
						});
					}
					if (deltaSyncResponse.ResponseHeaders != null)
					{
						this.deltaSyncCookie = deltaSyncResponse.ResponseHeaders[HttpResponseHeader.SetCookie];
					}
					try
					{
						deltaSyncResultData = this.deltaSyncResponseHandler.ParseDeltaSyncResponse(deltaSyncResponse, this.commandType);
						flag = (this.userAccount.PassportAuthenticationEnabled && deltaSyncResultData.IsAuthenticationError && !this.isLatestToken);
						goto IL_174;
					}
					catch (InvalidServerResponseException ex)
					{
						exception = ex;
						goto IL_174;
					}
				}
				DeltaSyncClient.CountCommand(asyncResult2, host, false);
				bool isRetryable = deltaSyncResponse.IsRetryable;
				if (isRetryable)
				{
					exception = new DownloadTransientException(deltaSyncResponse.Exception);
				}
				else
				{
					exception = new DownloadPermanentException(deltaSyncResponse.Exception);
				}
				if (!string.IsNullOrEmpty(host))
				{
					EndPointHealth.UpdateDeltaSyncEndPointStatus(host, !isRetryable, this.syncLogSession);
				}
				IL_174:;
			}
			if (flag)
			{
				this.httpSessionConfig.RequestStream.Position = 0L;
				this.BeginRequest(asyncResult2, true);
				return;
			}
			this.HandleResult(asyncResult2, deltaSyncResultData, exception);
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x00016A34 File Offset: 0x00014C34
		private void HandleResult(AsyncResult<DeltaSyncClient, DeltaSyncResultData> deltaSyncAsyncResult, DeltaSyncResultData deltaSyncResultData, Exception exception)
		{
			if (this.TryStopProcess())
			{
				this.AlternateWlidEndpointHandler.RestoreWlidEndpoint(this.AuthenticationClient);
				if (exception == null)
				{
					if (deltaSyncResultData.IsTopLevelOperationSuccessful)
					{
						this.syncLogSession.LogDebugging((TSLID)668UL, DeltaSyncClient.Tracer, "Delta Sync Request succeeded with Top Level Status Code: {0}", new object[]
						{
							deltaSyncResultData.TopLevelStatusCode
						});
					}
					else
					{
						this.syncLogSession.LogError((TSLID)1399UL, "DeltaSync Request Failed with Top Level Status Code:{0}, FaultCode:{1}, FaultString:{2}, FaultDetail:{3}.", new object[]
						{
							deltaSyncResultData.TopLevelStatusCode,
							deltaSyncResultData.FaultCode,
							deltaSyncResultData.FaultString,
							deltaSyncResultData.FaultDetail
						});
					}
					deltaSyncAsyncResult.ProcessCompleted(deltaSyncResultData);
					return;
				}
				this.syncLogSession.LogError((TSLID)669UL, DeltaSyncClient.Tracer, "Delta Sync Request failed with exception: {0}", new object[]
				{
					exception
				});
				deltaSyncAsyncResult.ProcessCompleted(exception);
			}
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x00016B2C File Offset: 0x00014D2C
		private bool TryStopProcess()
		{
			lock (this.syncRoot)
			{
				if (this.sessionClosed)
				{
					return false;
				}
				this.sessionClosed = true;
			}
			return true;
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x00016B7C File Offset: 0x00014D7C
		private void ThrowIfPending()
		{
			if (this.pendingAsyncResult != null)
			{
				throw new InvalidOperationException("async operation still pending");
			}
		}

		// Token: 0x04000225 RID: 549
		internal const string DeltaSyncAlternateWlidEndpointRegistryName = "DeltaSyncAlternateWlidEndpoint";

		// Token: 0x04000226 RID: 550
		private const string ApplicationID = "MicrosoftExchangeServer-DeltaSyncClient";

		// Token: 0x04000227 RID: 551
		private const string ExchangeHostedServicesUserAgent = "ExchangeHostedServices/1.0";

		// Token: 0x04000228 RID: 552
		private const int DefaultWindowSize = 2000;

		// Token: 0x04000229 RID: 553
		private const long NoDownloadLimit = -1L;

		// Token: 0x0400022A RID: 554
		private static readonly Trace Tracer = Microsoft.Exchange.Diagnostics.Components.Net.ExTraceGlobals.DeltaSyncClientTracer;

		// Token: 0x0400022B RID: 555
		private static readonly object serviceEndpointSyncLock = new object();

		// Token: 0x0400022C RID: 556
		private static volatile bool serviceEndpointsLoaded;

		// Token: 0x0400022D RID: 557
		private static Uri partnerSyncUri;

		// Token: 0x0400022E RID: 558
		private static Uri partnerSendUri;

		// Token: 0x0400022F RID: 559
		private static Uri partnerItemOperationsUri;

		// Token: 0x04000230 RID: 560
		private static Uri partnerSettingsUri;

		// Token: 0x04000231 RID: 561
		private static Uri passportSyncUri;

		// Token: 0x04000232 RID: 562
		private static Uri passportSendUri;

		// Token: 0x04000233 RID: 563
		private static Uri passportItemOperationsUri;

		// Token: 0x04000234 RID: 564
		private static Uri passportSettingsUri;

		// Token: 0x04000235 RID: 565
		private static Uri rstLiveEndpointUri;

		// Token: 0x04000236 RID: 566
		private static Uri partnerStatelessUri;

		// Token: 0x04000237 RID: 567
		private static Uri passportStatelessUri;

		// Token: 0x04000238 RID: 568
		private static string partnerClientToken;

		// Token: 0x04000239 RID: 569
		private readonly SyncLogSession syncLogSession;

		// Token: 0x0400023A RID: 570
		private HttpClient httpClient;

		// Token: 0x0400023B RID: 571
		private LiveIDAuthenticationClient authenticationClient;

		// Token: 0x0400023C RID: 572
		private volatile ICancelableAsyncResult pendingAsyncResult;

		// Token: 0x0400023D RID: 573
		private HttpSessionConfig httpSessionConfig;

		// Token: 0x0400023E RID: 574
		private object syncRoot = new object();

		// Token: 0x0400023F RID: 575
		private bool sessionClosed;

		// Token: 0x04000240 RID: 576
		private DeltaSyncUserAccount userAccount;

		// Token: 0x04000241 RID: 577
		private Stream requestStream;

		// Token: 0x04000242 RID: 578
		private bool isLatestToken;

		// Token: 0x04000243 RID: 579
		private DeltaSyncCommandType commandType;

		// Token: 0x04000244 RID: 580
		private DeltaSyncRequestGenerator deltaSyncRequestGenerator;

		// Token: 0x04000245 RID: 581
		private DeltaSyncResponseHandler deltaSyncResponseHandler;

		// Token: 0x04000246 RID: 582
		private AlternateWlidEndpointHandler alternateWlidEndpointHandler;

		// Token: 0x04000247 RID: 583
		private string deltaSyncCookie;

		// Token: 0x04000248 RID: 584
		private long maxDownloadSizePerMessage;

		// Token: 0x04000249 RID: 585
		private ExDateTime timeSent;

		// Token: 0x0200005F RID: 95
		// (Invoke) Token: 0x060004B9 RID: 1209
		private delegate void SetupRequest(Stream requestStream);

		// Token: 0x02000060 RID: 96
		// (Invoke) Token: 0x060004BD RID: 1213
		private delegate void SetupGetRequest();
	}
}
