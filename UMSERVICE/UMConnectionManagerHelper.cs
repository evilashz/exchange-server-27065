using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Net.Sockets;
using System.Security;
using System.Security.Cryptography;
using System.Threading;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.Diagnostics.LatencyDetection;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.UM.TroubleshootingTool.Shared;
using Microsoft.Exchange.UM.UcmaPlatform;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCommon.Exceptions;
using Microsoft.Exchange.UM.UMCommon.FaultInjection;
using Microsoft.Exchange.UM.UMCore;
using Microsoft.Exchange.UM.UMCore.Exceptions;
using Microsoft.Exchange.UM.UMService.Exceptions;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Rtc.Signaling;

namespace Microsoft.Exchange.UM.UMService
{
	// Token: 0x02000006 RID: 6
	internal class UMConnectionManagerHelper
	{
		// Token: 0x0600000A RID: 10 RVA: 0x00002258 File Offset: 0x00000458
		internal static UMConnectionManagerHelper.UMConnectionManager CreateConnectionManager(UMStartupMode startupMode, UMService umService)
		{
			bool flag;
			bool flag2;
			Utils.GetLocalIPv4IPv6Support(out flag, out flag2);
			List<UMConnectionManagerHelper.BaseUMConnectionManager> list = new List<UMConnectionManagerHelper.BaseUMConnectionManager>();
			if (flag && (startupMode == UMStartupMode.TCP || startupMode == UMStartupMode.Dual))
			{
				list.Add(new UMConnectionManagerHelper.TCPConnectionManager(umService, IPAddressFamily.IPv4Only));
			}
			if (flag && (startupMode == UMStartupMode.TLS || startupMode == UMStartupMode.Dual))
			{
				list.Add(new UMConnectionManagerHelper.TLSConnectionManager(umService, IPAddressFamily.IPv4Only));
			}
			if (flag2 && (startupMode == UMStartupMode.TCP || startupMode == UMStartupMode.Dual))
			{
				list.Add(new UMConnectionManagerHelper.TCPConnectionManager(umService, IPAddressFamily.IPv6Only));
			}
			if (flag2 && (startupMode == UMStartupMode.TLS || startupMode == UMStartupMode.Dual))
			{
				list.Add(new UMConnectionManagerHelper.TLSConnectionManager(umService, IPAddressFamily.IPv6Only));
			}
			return new UMConnectionManagerHelper.MultiConnectionManager(list.ToArray(), umService);
		}

		// Token: 0x02000007 RID: 7
		internal abstract class UMConnectionManager
		{
			// Token: 0x0600000C RID: 12 RVA: 0x000022E8 File Offset: 0x000004E8
			internal UMConnectionManager(UMService umservice)
			{
				this.serviceInstance = umservice;
			}

			// Token: 0x17000002 RID: 2
			// (get) Token: 0x0600000D RID: 13 RVA: 0x000022F7 File Offset: 0x000004F7
			internal UMService ServiceInstance
			{
				get
				{
					return this.serviceInstance;
				}
			}

			// Token: 0x0600000E RID: 14
			internal abstract void LogStartedListeningForCalls();

			// Token: 0x0600000F RID: 15
			internal abstract void LogStoppedListeningForCalls();

			// Token: 0x06000010 RID: 16
			internal abstract void StartEndPoint();

			// Token: 0x06000011 RID: 17
			internal abstract void StopEndPoint();

			// Token: 0x06000012 RID: 18
			internal abstract void StopListeningForCalls();

			// Token: 0x06000013 RID: 19
			internal abstract void EnsureListeningForCalls();

			// Token: 0x06000014 RID: 20
			internal abstract void Initialize();

			// Token: 0x06000015 RID: 21
			internal abstract void RestartEndPoint();

			// Token: 0x06000016 RID: 22 RVA: 0x00002300 File Offset: 0x00000500
			protected static List<SignalingHeader> GetDiversionHeaders(IEnumerable<SignalingHeader> incomingHeaders)
			{
				List<SignalingHeader> list = new List<SignalingHeader>(1);
				foreach (SignalingHeader signalingHeader in incomingHeaders)
				{
					if (UMConnectionManagerHelper.UMConnectionManager.IsDiversionHeader(signalingHeader.Name))
					{
						list.Add(signalingHeader);
					}
				}
				return list;
			}

			// Token: 0x06000017 RID: 23 RVA: 0x00002360 File Offset: 0x00000560
			protected static SignalingHeader CreateContactHeader(string sipUri)
			{
				sipUri = string.Format(CultureInfo.InvariantCulture, "<{0}>", new object[]
				{
					sipUri
				});
				return new SignalingHeader("Contact", sipUri);
			}

			// Token: 0x06000018 RID: 24 RVA: 0x00002398 File Offset: 0x00000598
			protected static string GetMsOrganizationParameterValue(SipUriParser sipUri)
			{
				SipUriParameter sipUriParameter = sipUri.FindParameter("ms-organization");
				if (sipUriParameter == null)
				{
					return string.Empty;
				}
				return sipUriParameter.Value;
			}

			// Token: 0x06000019 RID: 25 RVA: 0x000023C0 File Offset: 0x000005C0
			private static bool IsDiversionHeader(string headerName)
			{
				return "Diversion".Equals(headerName, StringComparison.OrdinalIgnoreCase) || "CC-Diversion".Equals(headerName, StringComparison.OrdinalIgnoreCase) || "History-Info".Equals(headerName, StringComparison.OrdinalIgnoreCase);
			}

			// Token: 0x04000018 RID: 24
			private UMService serviceInstance;
		}

		// Token: 0x02000008 RID: 8
		internal abstract class BaseUMConnectionManager : UMConnectionManagerHelper.UMConnectionManager
		{
			// Token: 0x0600001A RID: 26 RVA: 0x000023F0 File Offset: 0x000005F0
			internal BaseUMConnectionManager(UMService umservice, IPAddressFamily ipAddressFamily) : base(umservice)
			{
				this.DebugTrace("BaseUMConnectionManager::ctor() {0}", new object[]
				{
					ipAddressFamily
				});
				this.localServer = Utils.GetLocalUMServer();
				this.externalFqdn = this.localServer.ExternalHostFqdn;
				this.ipAddressFamily = ipAddressFamily;
			}

			// Token: 0x17000003 RID: 3
			// (get) Token: 0x0600001B RID: 27 RVA: 0x0000244E File Offset: 0x0000064E
			internal static string SIPUriFormat
			{
				get
				{
					return "sip:{0}";
				}
			}

			// Token: 0x17000004 RID: 4
			// (get) Token: 0x0600001C RID: 28 RVA: 0x00002455 File Offset: 0x00000655
			// (set) Token: 0x0600001D RID: 29 RVA: 0x0000245D File Offset: 0x0000065D
			protected internal RealTimeServerConnectionManager ConnMgr
			{
				get
				{
					return this.connMgr;
				}
				protected set
				{
					this.connMgr = value;
				}
			}

			// Token: 0x17000005 RID: 5
			// (get) Token: 0x0600001E RID: 30 RVA: 0x00002466 File Offset: 0x00000666
			protected internal string LocalFqdn
			{
				get
				{
					return this.localFqdn;
				}
			}

			// Token: 0x17000006 RID: 6
			// (get) Token: 0x0600001F RID: 31 RVA: 0x0000246E File Offset: 0x0000066E
			protected internal string ExternalFqdn
			{
				get
				{
					if (this.externalFqdn == null)
					{
						return string.Empty;
					}
					return this.externalFqdn.ToString();
				}
			}

			// Token: 0x17000007 RID: 7
			// (get) Token: 0x06000020 RID: 32 RVA: 0x00002489 File Offset: 0x00000689
			// (set) Token: 0x06000021 RID: 33 RVA: 0x00002491 File Offset: 0x00000691
			protected internal SipPeerToPeerEndpoint SIPEndPoint
			{
				get
				{
					return this.endPoint;
				}
				protected set
				{
					this.endPoint = value;
				}
			}

			// Token: 0x17000008 RID: 8
			// (get) Token: 0x06000022 RID: 34 RVA: 0x0000249A File Offset: 0x0000069A
			// (set) Token: 0x06000023 RID: 35 RVA: 0x000024A2 File Offset: 0x000006A2
			protected internal List<SignalingHeader> OptionsReplyHeaders
			{
				get
				{
					return this.optionsReplyHeaders;
				}
				protected set
				{
					this.optionsReplyHeaders = value;
				}
			}

			// Token: 0x17000009 RID: 9
			// (get) Token: 0x06000024 RID: 36 RVA: 0x000024AB File Offset: 0x000006AB
			// (set) Token: 0x06000025 RID: 37 RVA: 0x000024B3 File Offset: 0x000006B3
			protected internal List<SignalingHeader> OptionsRequestHeaders
			{
				get
				{
					return this.optionsRequestHeaders;
				}
				protected set
				{
					this.optionsRequestHeaders = value;
				}
			}

			// Token: 0x1700000A RID: 10
			// (get) Token: 0x06000026 RID: 38 RVA: 0x000024BC File Offset: 0x000006BC
			// (set) Token: 0x06000027 RID: 39 RVA: 0x000024C4 File Offset: 0x000006C4
			protected internal bool IsSecured
			{
				get
				{
					return this.isSecured;
				}
				protected set
				{
					this.isSecured = value;
				}
			}

			// Token: 0x1700000B RID: 11
			// (get) Token: 0x06000028 RID: 40 RVA: 0x000024CD File Offset: 0x000006CD
			protected UMServer LocalServer
			{
				get
				{
					return this.localServer;
				}
			}

			// Token: 0x1700000C RID: 12
			// (get) Token: 0x06000029 RID: 41 RVA: 0x000024D5 File Offset: 0x000006D5
			protected IPAddress IPAddress
			{
				get
				{
					if (this.ipAddressFamily != IPAddressFamily.IPv4Only)
					{
						return IPAddress.IPv6Any;
					}
					return IPAddress.Any;
				}
			}

			// Token: 0x0600002A RID: 42 RVA: 0x000024EC File Offset: 0x000006EC
			internal override void EnsureListeningForCalls()
			{
				if (!this.ConnMgr.IsListening)
				{
					int num = 0;
					Exception ex = null;
					this.ConstructEndpoint();
					do
					{
						try
						{
							this.StartEndPoint();
							break;
						}
						catch (UMServiceBaseException ex2)
						{
							num++;
							ex = ex2;
						}
						Thread.Sleep(1000);
					}
					while (num < 5);
					if (num >= 5)
					{
						throw new UMServiceBaseException(Strings.SipEndpointStartFailure + ex.Message, ex);
					}
					this.LogStartedListeningForCalls();
				}
			}

			// Token: 0x0600002B RID: 43 RVA: 0x00002568 File Offset: 0x00000768
			internal override void StopListeningForCalls()
			{
				try
				{
					this.DestructEndpoint();
					if (this.ConnMgr.IsListening)
					{
						this.ConnMgr.StopListening();
						this.LogStoppedListeningForCalls();
					}
					ReadOnlyCollection<ConnectionPool> connectionPools = this.ConnMgr.GetConnectionPools();
					foreach (ConnectionPool connectionPool in connectionPools)
					{
						ReadOnlyCollection<RealTimeConnection> connections = connectionPool.GetConnections();
						foreach (RealTimeConnection realTimeConnection in connections)
						{
							realTimeConnection.Disconnect();
						}
					}
					ReadOnlyCollection<RealTimeConnection> incomingConnections = this.ConnMgr.GetIncomingConnections();
					foreach (RealTimeConnection realTimeConnection2 in incomingConnections)
					{
						realTimeConnection2.Disconnect();
					}
				}
				catch (Exception ex)
				{
					if (ex is RealTimeException || ex is SocketException || ex is InvalidOperationException)
					{
						this.ErrorTrace("StopListeningAndCloseSockets failed, error={0}", new object[]
						{
							ex.ToString()
						});
						throw new UMServiceBaseException(ex.Message, ex);
					}
					throw;
				}
			}

			// Token: 0x0600002C RID: 44 RVA: 0x0000273C File Offset: 0x0000093C
			internal void OnMessageReceived(object sender, MessageReceivedEventArgs e)
			{
				try
				{
					try
					{
						this.CheckServiceAvailable();
						if (e.MessageType == 4)
						{
							this.HandleServiceRequest(e);
						}
						else if (e.MessageType == 2)
						{
							this.SendReplyToOptionsRequest(e);
						}
						else
						{
							this.RtcSessionSafeOperation(delegate
							{
								e.SendResponse(403);
							});
						}
					}
					catch (Exception ex)
					{
						PlatformSignalingHeader diagnostic = CallRejectedException.RenderDiagnosticHeader(CallEndingReason.TransientError, null, new object[0]);
						if (ex is RealTimeException)
						{
							throw;
						}
						int responseCode;
						if (ex is CallRejectedException)
						{
							responseCode = 503;
							diagnostic = ((CallRejectedException)ex).DiagnosticHeader;
						}
						else if (ex is ADTransientException || ex is ADOperationException || ex is ExchangeServerNotFoundException)
						{
							responseCode = 500;
						}
						else
						{
							if (!GrayException.IsGrayException(ex))
							{
								throw;
							}
							responseCode = 500;
							ExceptionHandling.SendWatsonWithoutDumpWithExtraData(ex);
						}
						this.ErrorTrace("OnMessageReceived::Error encountered: {0} - DiagnosticHeader: {1}", new object[]
						{
							ex,
							diagnostic.Value
						});
						this.RtcSessionSafeOperation(delegate
						{
							SignalingHeader[] array = new SignalingHeader[]
							{
								new SignalingHeader(diagnostic.Name, diagnostic.Value)
							};
							e.SendResponse(responseCode, null, null, array);
						});
					}
				}
				catch (RealTimeException ex2)
				{
					this.ErrorTrace("RealTime Exception in SIP OnMessageReceived. Details = {0}", new object[]
					{
						ex2
					});
					UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_PingResponseFailure, null, new object[]
					{
						ex2
					});
				}
			}

			// Token: 0x0600002D RID: 45 RVA: 0x00002904 File Offset: 0x00000B04
			protected static void SetCallRejectionCounters(bool successRedirect)
			{
				if (!successRedirect)
				{
					Util.IncrementCounter(AvailabilityCounters.UMSerivceCallsRejected);
				}
				Util.SetCounter(AvailabilityCounters.RecentUMSerivceCallsRejected, (long)UMConnectionManagerHelper.BaseUMConnectionManager.recentPercentageRejectedCalls.Update(successRedirect));
			}

			// Token: 0x0600002E RID: 46 RVA: 0x0000292C File Offset: 0x00000B2C
			protected static SipRoutingHelper CreateRoutingHelper(MessageReceivedEventArgs args)
			{
				bool flag = false;
				return UMConnectionManagerHelper.BaseUMConnectionManager.CreateRoutingHelper(args, null, out flag);
			}

			// Token: 0x0600002F RID: 47 RVA: 0x00002944 File Offset: 0x00000B44
			protected static SipRoutingHelper CreateRoutingHelper(SipRequestReceivedEventArgs args, IEnumerable<PlatformSignalingHeader> diagnosticsHeaders, out bool isDiagnosticCall)
			{
				isDiagnosticCall = false;
				RealTimeConnection connection;
				if (args is SessionReceivedEventArgs)
				{
					connection = ((SessionReceivedEventArgs)args).Session.Connection;
				}
				else
				{
					if (!(args is MessageReceivedEventArgs))
					{
						throw new ArgumentException("args");
					}
					connection = ((MessageReceivedEventArgs)args).Connection;
				}
				if (connection == null)
				{
					throw CallRejectedException.Create(Strings.InvalidRequest, CallEndingReason.InvalidRequest, "Connection is unavailable.", new object[0]);
				}
				string text = connection.ApplicationContext as string;
				SipUriParser sipUri = new SipUriParser(args.RequestData.RequestUri);
				string msOrganizationParameterValue = UMConnectionManagerHelper.UMConnectionManager.GetMsOrganizationParameterValue(sipUri);
				SipRoutingHelper sipRoutingHelper;
				if (args is MessageReceivedEventArgs)
				{
					sipRoutingHelper = SipRoutingHelper.CreateForServiceRequest(text, msOrganizationParameterValue);
				}
				else
				{
					isDiagnosticCall = SipPeerManager.Instance.IsLocalDiagnosticCall(connection.RemoteEndpoint.Address, diagnosticsHeaders);
					sipRoutingHelper = SipRoutingHelper.CreateForInbound(isDiagnosticCall, text, msOrganizationParameterValue);
				}
				CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "CreateRoutingHelper({0}:{1})->{2}", new object[]
				{
					string.IsNullOrEmpty(text) ? "no matchedFqdn" : text,
					connection,
					sipRoutingHelper.GetType().Name
				});
				return sipRoutingHelper;
			}

			// Token: 0x06000030 RID: 48 RVA: 0x00002A54 File Offset: 0x00000C54
			protected void RtcSessionSafeOperation(UMConnectionManagerHelper.BaseUMConnectionManager.RtcOpertaion operation)
			{
				try
				{
					operation();
				}
				catch (InvalidOperationException ex)
				{
					this.ErrorTrace("RtcSessionSafeOperation encountered error: {0}", new object[]
					{
						ex
					});
				}
			}

			// Token: 0x06000031 RID: 49 RVA: 0x00002A94 File Offset: 0x00000C94
			protected void FrameOptionsHeaders()
			{
				this.optionsReplyHeaders = new List<SignalingHeader>();
				SignalingHeader item = new SignalingHeader("Accept", "application/sdp");
				this.optionsReplyHeaders.Add(item);
				item = new SignalingHeader("Allow", "INVITE");
				this.optionsReplyHeaders.Add(item);
				item = new SignalingHeader("Allow", "BYE");
				this.optionsReplyHeaders.Add(item);
				item = new SignalingHeader("Allow", "CANCEL");
				this.optionsReplyHeaders.Add(item);
				item = new SignalingHeader("Allow", "OPTIONS");
				this.optionsReplyHeaders.Add(item);
				item = new SignalingHeader("Allow", "ACK");
				this.optionsReplyHeaders.Add(item);
				item = new SignalingHeader("Allow", "INFO");
				this.optionsReplyHeaders.Add(item);
				item = new SignalingHeader("Allow", "NOTIFY");
				this.optionsReplyHeaders.Add(item);
				this.optionsRequestHeaders = new List<SignalingHeader>();
				item = new SignalingHeader("Accept", "application/sdp");
				this.optionsRequestHeaders.Add(item);
			}

			// Token: 0x06000032 RID: 50 RVA: 0x00002BB3 File Offset: 0x00000DB3
			protected void DebugTrace(string formatString, params object[] formatObjects)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, this.GetHashCode(), formatString, formatObjects);
			}

			// Token: 0x06000033 RID: 51 RVA: 0x00002BCC File Offset: 0x00000DCC
			protected void ErrorTrace(string formatString, params object[] formatObjects)
			{
				CallIdTracer.TraceError(ExTraceGlobals.ServiceTracer, this.GetHashCode(), formatString, formatObjects);
			}

			// Token: 0x06000034 RID: 52 RVA: 0x00002BE8 File Offset: 0x00000DE8
			protected void Stop()
			{
				this.DebugTrace("Stopping BaseUMConnectionManager Stop() {0}", new object[]
				{
					ExDateTime.UtcNow
				});
				if (this.endPoint != null)
				{
					try
					{
						this.DebugTrace("Stopping SIPPeertoPeerEndpoint", new object[0]);
						this.connMgr.StopListening();
						this.connMgr.Dispose();
						this.DestructEndpoint();
					}
					catch (RealTimeException ex)
					{
						this.ErrorTrace("In Stopping BaseUMConnectionManager, error={0}", new object[]
						{
							ex.ToString()
						});
					}
					catch (InvalidOperationException ex2)
					{
						this.ErrorTrace("In Stopping BaseUMConnectionManager, error={0}", new object[]
						{
							ex2.ToString()
						});
					}
				}
			}

			// Token: 0x06000035 RID: 53 RVA: 0x00002CAC File Offset: 0x00000EAC
			protected virtual void OnIncomingSession(object sender, SessionReceivedEventArgs args)
			{
				this.DebugTrace("Inside BaseUMConnectionManager OnIncomingSession()", new object[0]);
				CallIdTracer.TracePfd(ExTraceGlobals.PFDUMCallAcceptanceTracer, 0, "PFD UMC {0} - Received Inbound Call in UM Master Process.", new object[]
				{
					15354
				});
				SignalingSession session = args.Session;
				bool isDiagnosticCall = false;
				LatencyDetectionContext latencyContext = Util.StartCallLatencyDetection("BaseUMConnectionManager", session.CallId);
				bool addToLog = false;
				ExDateTime now = ExDateTime.Now;
				using (new CallId(session.CallId))
				{
					try
					{
						string empty = string.Empty;
						IEnumerable<PlatformSignalingHeader> diagnosticHeaders = UMConnectionManagerHelper.BaseUMConnectionManager.GetDiagnosticHeaders(args.RequestData.SignalingHeaders);
						bool flag = SipPeerManager.Instance.IsActiveMonitoringCall(diagnosticHeaders);
						if (flag)
						{
							this.SendDiagnosticsInfoCallReceived(session);
						}
						SipRoutingHelper routingHelper = UMConnectionManagerHelper.BaseUMConnectionManager.CreateRoutingHelper(args, diagnosticHeaders, out isDiagnosticCall);
						this.CheckServiceAvailable();
						if (!this.TryMapCallToWorkerProcess(args, routingHelper, out empty))
						{
							this.DebugTrace("No worker process to handle call. Ending it.", new object[0]);
							throw CallRejectedException.Create(Strings.UMWorkerProcessNotAvailableError, CallEndingReason.NoWorkerProcess, null, new object[0]);
						}
						this.DebugTrace("Redirecting call to worker process -- '{0}'.", new object[]
						{
							empty
						});
						this.RedirectCall(args, routingHelper, empty, isDiagnosticCall, flag);
						addToLog = true;
					}
					catch (Exception ex)
					{
						if (!this.HandleExceptionAndRejectCall(session, ex, isDiagnosticCall))
						{
							throw;
						}
					}
					finally
					{
						Util.EndCallLatencyDetection(latencyContext, session.CallId, now, args.RequestData.UserAgent, null, addToLog);
					}
				}
			}

			// Token: 0x06000036 RID: 54 RVA: 0x00002E7C File Offset: 0x0000107C
			protected void HandleServiceRequest(MessageReceivedEventArgs eventArgs)
			{
				this.DebugTrace("BaseUMConnectionManager::HandleServiceRequest() Target:{0}, From:{1}", new object[]
				{
					eventArgs.RequestData.ToHeader,
					eventArgs.RequestData.FromHeader
				});
				SipRoutingHelper routingHelper = UMConnectionManagerHelper.BaseUMConnectionManager.CreateRoutingHelper(eventArgs);
				string uri = null;
				if (this.TryMapCallToWorkerProcess(eventArgs, routingHelper, out uri))
				{
					this.DebugTrace("HandleServiceRequest. Need redirection to worker process -> uri:{0}", new object[]
					{
						uri
					});
					this.RtcSessionSafeOperation(delegate
					{
						eventArgs.SendResponse(routingHelper.RedirectResponseCode, null, null, new SignalingHeader[]
						{
							UMConnectionManagerHelper.UMConnectionManager.CreateContactHeader(uri)
						});
					});
					return;
				}
				this.ErrorTrace("HandleServiceRequest. No redirect targets found.", new object[0]);
				throw CallRejectedException.Create(Strings.InvalidRequest, CallEndingReason.CannotFindRedirectTarget, null, new object[0]);
			}

			// Token: 0x06000037 RID: 55 RVA: 0x00002F5C File Offset: 0x0000115C
			protected void ConstructEndpoint()
			{
				this.FrameOptionsHeaders();
				this.endPoint = new SipPeerToPeerEndpoint(string.Format(CultureInfo.InvariantCulture, UMConnectionManagerHelper.BaseUMConnectionManager.SIPUriFormat, new object[]
				{
					this.LocalFqdn
				}), this.connMgr);
				this.endPoint.MessageReceived += this.OnMessageReceived;
				this.endPoint.SessionReceived += this.OnIncomingSession;
			}

			// Token: 0x06000038 RID: 56 RVA: 0x00002FD0 File Offset: 0x000011D0
			private static IEnumerable<PlatformSignalingHeader> GetDiagnosticHeaders(IEnumerable<SignalingHeader> headerList)
			{
				if (headerList == null)
				{
					return null;
				}
				IEnumerable<PlatformSignalingHeader> result;
				try
				{
					List<PlatformSignalingHeader> list = new List<PlatformSignalingHeader>(2);
					foreach (SignalingHeader signalingHeader in headerList)
					{
						if (string.Equals(signalingHeader.Name, "msexum-connectivitytest", StringComparison.OrdinalIgnoreCase) || string.Equals(signalingHeader.Name, "user-agent", StringComparison.OrdinalIgnoreCase))
						{
							list.Add(UcmaSignalingHeader.FromSignalingHeader(signalingHeader, "INVITE"));
						}
					}
					result = list;
				}
				catch (InvalidSIPHeaderException ex)
				{
					CallIdTracer.TraceError(ExTraceGlobals.ServiceTracer, 0, "Failed to parse diagnostic headers. Error: {0}", new object[]
					{
						ex
					});
					result = null;
				}
				return result;
			}

			// Token: 0x06000039 RID: 57 RVA: 0x00003094 File Offset: 0x00001294
			private void CheckServiceAvailable()
			{
				this.CheckCallLimit();
				this.CheckDiskSpace();
			}

			// Token: 0x0600003A RID: 58 RVA: 0x000030A2 File Offset: 0x000012A2
			private void DestructEndpoint()
			{
				this.endPoint.SessionReceived -= this.OnIncomingSession;
				this.endPoint.MessageReceived -= this.OnMessageReceived;
				this.endPoint.Terminate();
			}

			// Token: 0x0600003B RID: 59 RVA: 0x000030E0 File Offset: 0x000012E0
			private void CheckDiskSpace()
			{
				bool flag = false;
				long num = VariantConfiguration.InvariantNoFlightingSnapshot.UM.VoicemailDiskSpaceDatacenterLimit.Enabled ? 50L : 500L;
				long num2 = num * 1024L * 1024L;
				long warning = 786432000L;
				long num3;
				if (!Util.IsDiskSpaceAvailable(out num3, out flag))
				{
					throw CallRejectedException.Create(Strings.FreeDiskSpaceLimitExceeded(num3, num2), CallEndingReason.DiskspaceFull, "Available: {0}. Minimum required: {1}.", new object[]
					{
						num3,
						num2
					});
				}
				if (flag)
				{
					UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_UMServiceLowOnResources, "UMServiceLowResourceKey", new object[]
					{
						Strings.FreeDiskSpaceLimitWarning(num3, num2, warning)
					});
				}
			}

			// Token: 0x0600003C RID: 60 RVA: 0x000031A4 File Offset: 0x000013A4
			private void CheckCallLimit()
			{
				bool flag = false;
				if (Util.MaxCallLimitExceeded(out flag))
				{
					throw CallRejectedException.Create(Strings.MaxCallsLimitReached(CommonConstants.MaxCallsAllowed.Value), CallEndingReason.MaxCallsReached, "Maximum configured value: {0}.", new object[]
					{
						CommonConstants.MaxCallsAllowed.Value
					});
				}
				if (flag)
				{
					UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_UMServiceLowOnResources, "UMServiceLowResourceKey", new object[]
					{
						Strings.MaxCallsLimitReachedWarning((int)GeneralCounters.CurrentCalls.RawValue, CommonConstants.MaxCallsAllowed.Value)
					});
				}
			}

			// Token: 0x0600003D RID: 61 RVA: 0x000032B0 File Offset: 0x000014B0
			private void SendReplyToOptionsRequest(MessageReceivedEventArgs e)
			{
				this.DebugTrace("BaseUMConnectionManager::SendReplyToOptionsRequest() received an OPTIONS request for target={0}, from {1}", new object[]
				{
					e.RequestData.ToHeader,
					e.Participant.Uri
				});
				int num;
				if (!base.ServiceInstance.CanRedirect(this.IsSecured, out num))
				{
					this.DebugTrace("No worker process to handle Options. Ending it.", new object[0]);
					throw CallRejectedException.Create(Strings.UMWorkerProcessNotAvailableError, CallEndingReason.NoWorkerProcess, null, new object[0]);
				}
				this.RtcSessionSafeOperation(delegate
				{
					try
					{
						e.SendResponse(200, new System.Net.Mime.ContentType("application/sdp"), null, this.optionsReplyHeaders);
					}
					catch (ArgumentException ex)
					{
						this.DebugTrace("BaseUMConnectionManager:: Responding to Options() incurred exception: {0}", new object[]
						{
							ex
						});
					}
				});
			}

			// Token: 0x0600003E RID: 62 RVA: 0x0000335C File Offset: 0x0000155C
			private bool TryMapCallToWorkerProcess(SipRequestReceivedEventArgs args, SipRoutingHelper routingHelper, out string uri)
			{
				uri = string.Empty;
				if (args == null)
				{
					throw new ArgumentNullException("args");
				}
				int num = 0;
				bool flag = false;
				for (int i = 0; i < 5; i++)
				{
					CallIdTracer.TracePfd(ExTraceGlobals.PFDUMCallAcceptanceTracer, 0, "PFD UMC {0} - Locating Available Worker Process.", new object[]
					{
						8762
					});
					flag = base.ServiceInstance.CanRedirect(this.IsSecured, out num);
					if (flag)
					{
						break;
					}
					Thread.Sleep(1000);
				}
				FaultInjectionUtils.FaultInjectChangeValue<bool>(2334534973U, ref flag);
				if (flag)
				{
					this.DebugTrace("Call can be redirected to worker process port = '{0}', Secured = '{1}'", new object[]
					{
						num,
						this.IsSecured
					});
					uri = this.BuildRedirectUri(args, routingHelper, Utils.TryGetRedirectTargetFqdnForServer((Server)this.localServer.DataObject), num, this.isSecured ? "TLS" : "TCP");
				}
				return flag;
			}

			// Token: 0x0600003F RID: 63 RVA: 0x000034B0 File Offset: 0x000016B0
			private void RedirectCall(SessionReceivedEventArgs args, SipRoutingHelper routingHelper, string uri, bool isDiagnosticCall, bool isActiveMonitoringCall)
			{
				if (args == null)
				{
					throw new ArgumentNullException("args");
				}
				if (uri == null)
				{
					throw new ArgumentNullException("uri");
				}
				SignalingSession session = args.Session;
				if (isActiveMonitoringCall)
				{
					this.SendDiagnosticsInfo(session, uri);
				}
				List<SignalingHeader> signalingHeaders = UMConnectionManagerHelper.UMConnectionManager.GetDiversionHeaders(args.RequestData.SignalingHeaders);
				int responseCode = routingHelper.RedirectResponseCode;
				this.DebugTrace("Redirecting call to '{0}' ResponseCode:{1}", new object[]
				{
					uri,
					responseCode
				});
				CallIdTracer.TracePfd(ExTraceGlobals.PFDUMCallAcceptanceTracer, 0, "PFD UMC {0} - Call with ID {1} is being redirected to {2}.", new object[]
				{
					11258,
					session.CallId,
					uri
				});
				this.RtcSessionSafeOperation(delegate
				{
					signalingHeaders.Add(UMConnectionManagerHelper.UMConnectionManager.CreateContactHeader(uri));
					session.TerminateWithRejection(responseCode, null, signalingHeaders);
					CallRejectionCounterHelper.Instance.SetCounters(null, new Action<bool>(UMConnectionManagerHelper.BaseUMConnectionManager.SetCallRejectionCounters), true, isDiagnosticCall);
				});
			}

			// Token: 0x06000040 RID: 64 RVA: 0x000035B4 File Offset: 0x000017B4
			private List<SignalingHeader> CreateProvisionalResponseDiagnosticInfo(string headerValue)
			{
				List<SignalingHeader> list = new List<SignalingHeader>();
				string text = "ms-diagnostics-public";
				this.DebugTrace("{0}:{1}", new object[]
				{
					text,
					headerValue
				});
				list.Add(new SignalingHeader(text, headerValue));
				return list;
			}

			// Token: 0x06000041 RID: 65 RVA: 0x0000363C File Offset: 0x0000183C
			private void SendDiagnosticsInfo(SignalingSession session, string uri)
			{
				this.DebugTrace("Sending 101 Diagnostic message for ActiveMonitoring client to track Redirects", new object[0]);
				this.RtcSessionSafeOperation(delegate
				{
					string headerValue = Util.FormatDiagnosticsInfoRedirect(uri);
					List<SignalingHeader> list = this.CreateProvisionalResponseDiagnosticInfo(headerValue);
					session.SendProvisionalResponse(101, "Diagnostics", list, false);
				});
			}

			// Token: 0x06000042 RID: 66 RVA: 0x000036C4 File Offset: 0x000018C4
			private void SendDiagnosticsInfoCallReceived(SignalingSession session)
			{
				this.DebugTrace("Sending 101 Diagnostic message for ActiveMonitoring client to track call received", new object[0]);
				this.RtcSessionSafeOperation(delegate
				{
					string headerValue = Util.FormatDiagnosticsInfoCallReceived();
					List<SignalingHeader> list = this.CreateProvisionalResponseDiagnosticInfo(headerValue);
					session.SendProvisionalResponse(101, "Diagnostics", list, false);
				});
			}

			// Token: 0x06000043 RID: 67 RVA: 0x00003708 File Offset: 0x00001908
			private string BuildRedirectUri(SipRequestReceivedEventArgs args, SipRoutingHelper routingHelper, string host, int port, string transportParameter)
			{
				string requestUri = args.RequestData.RequestUri;
				if (requestUri == null)
				{
					throw new ArgumentNullException("originalRequestUri");
				}
				if (host == null)
				{
					throw new ArgumentNullException("host");
				}
				if (transportParameter == null)
				{
					throw new ArgumentNullException("transportParameter");
				}
				this.DebugTrace("Building uri for redirection, originalRequestUri = '{0}', host = '{1}', port = '{2}', transportParameter = '{3}'", new object[]
				{
					requestUri,
					host,
					port,
					transportParameter
				});
				string text = null;
				SipUriParser sipUriParser = new SipUriParser(requestUri);
				sipUriParser.Port = port;
				sipUriParser.TransportParameter = transportParameter;
				sipUriParser.Host = routingHelper.GetContactHeaderHost(host, out text);
				if (!string.IsNullOrEmpty(text))
				{
					sipUriParser.AddParameter(new SipUriParameter("ms-fe", text));
				}
				SipUriParameter sipUriParameter = sipUriParser.FindParameter("maddr");
				if (sipUriParameter != null)
				{
					sipUriParser.AddParameter(new SipUriParameter("maddr", host));
				}
				string text2 = sipUriParser.ToString();
				this.DebugTrace("BuildRedirectUri: {0}", new object[]
				{
					text2
				});
				return text2;
			}

			// Token: 0x06000044 RID: 68 RVA: 0x00003840 File Offset: 0x00001A40
			private bool HandleExceptionAndRejectCall(SignalingSession session, Exception ex, bool isDiagnosticCall)
			{
				int responseCode = 500;
				string responseText = "Server Internal Error";
				PlatformSignalingHeader platformSignalingHeader = CallRejectedException.RenderDiagnosticHeader(CallEndingReason.TransientError, null, new object[0]);
				bool flag = true;
				if (ex is RealTimeException)
				{
					flag = false;
				}
				else if (ex is CallRejectedException)
				{
					CallRejectedException ex2 = (CallRejectedException)ex;
					if (ex2.LocalizedString.Equals(Strings.PartnerGatewayNotFoundError))
					{
						responseCode = 404;
						responseText = "Gateway not found";
					}
					else
					{
						responseCode = 503;
						responseText = "Service Unavailable";
					}
					platformSignalingHeader = ex2.DiagnosticHeader;
				}
				else if (ex is ADTransientException || ex is ADOperationException || ex is ExchangeServerNotFoundException)
				{
					platformSignalingHeader = CallRejectedException.RenderDiagnosticHeader(CallEndingReason.ADError, ex.GetType().FullName, new object[0]);
				}
				else
				{
					if (!GrayException.IsGrayException(ex))
					{
						return false;
					}
					ExceptionHandling.SendWatsonWithoutDumpWithExtraData(ex);
				}
				this.ErrorTrace("HandleExceptionAndRejectCall: Call is being rejected. Reason:{0} - DiagnosticHeader:{1}", new object[]
				{
					ex,
					platformSignalingHeader.Value
				});
				CallRejectionCounterHelper.Instance.SetCounters(ex, new Action<bool>(UMConnectionManagerHelper.BaseUMConnectionManager.SetCallRejectionCounters), false, isDiagnosticCall);
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_UMServiceCallRejected, null, new object[]
				{
					UMServiceBase.ServiceNameForEventLogging,
					CommonUtil.ToEventLogString(ex)
				});
				if (flag)
				{
					try
					{
						List<SignalingHeader> header = new List<SignalingHeader>();
						header.Add(new SignalingHeader(platformSignalingHeader.Name, platformSignalingHeader.Value));
						this.RtcSessionSafeOperation(delegate
						{
							session.TerminateWithRejection(responseCode, responseText, header);
						});
					}
					catch (RealTimeException ex3)
					{
						this.ErrorTrace("HandleExceptionAndRejectCallL: Error :{0}", new object[]
						{
							ex3
						});
					}
				}
				return true;
			}

			// Token: 0x04000019 RID: 25
			private const string LowResourcePeriodicKey = "UMServiceLowResourceKey";

			// Token: 0x0400001A RID: 26
			protected IPAddressFamily ipAddressFamily;

			// Token: 0x0400001B RID: 27
			private static PercentageBooleanSlidingCounter recentPercentageRejectedCalls = PercentageBooleanSlidingCounter.CreateFailureCounter(1000, TimeSpan.FromHours(1.0));

			// Token: 0x0400001C RID: 28
			private readonly string localFqdn = Utils.GetLocalHostFqdn();

			// Token: 0x0400001D RID: 29
			private SipPeerToPeerEndpoint endPoint;

			// Token: 0x0400001E RID: 30
			private RealTimeServerConnectionManager connMgr;

			// Token: 0x0400001F RID: 31
			private UMServer localServer;

			// Token: 0x04000020 RID: 32
			private UMSmartHost externalFqdn;

			// Token: 0x04000021 RID: 33
			private List<SignalingHeader> optionsReplyHeaders;

			// Token: 0x04000022 RID: 34
			private List<SignalingHeader> optionsRequestHeaders;

			// Token: 0x04000023 RID: 35
			private bool isSecured;

			// Token: 0x02000009 RID: 9
			// (Invoke) Token: 0x06000047 RID: 71
			protected delegate void RtcOpertaion();
		}

		// Token: 0x0200000A RID: 10
		internal class TCPConnectionManager : UMConnectionManagerHelper.BaseUMConnectionManager
		{
			// Token: 0x0600004A RID: 74 RVA: 0x00003A4C File Offset: 0x00001C4C
			internal TCPConnectionManager(UMService umservice, IPAddressFamily ipAddressFamily) : base(umservice, ipAddressFamily)
			{
				base.IsSecured = false;
				if (!string.IsNullOrEmpty(base.ExternalFqdn))
				{
					UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_ExternalFqdnDetected, null, new object[]
					{
						"SIP/TCP",
						base.ExternalFqdn
					});
				}
			}

			// Token: 0x0600004B RID: 75 RVA: 0x00003AA0 File Offset: 0x00001CA0
			internal override void StartEndPoint()
			{
				base.DebugTrace("Inside TCPConnectionManager StartEndPoint: {0} {1}", new object[]
				{
					base.IPAddress,
					UMRecyclerConfig.TcpListeningPort
				});
				try
				{
					base.SIPEndPoint.ConnectionManager.StartListening(new IPEndPoint(base.IPAddress, UMRecyclerConfig.TcpListeningPort));
				}
				catch (Exception ex)
				{
					if (ex is RealTimeException || ex is SocketException)
					{
						base.ErrorTrace("Couldnt start SIP endpoint, error={0}", new object[]
						{
							ex.ToString()
						});
						throw new UMServiceBaseException(Strings.SipEndpointStartFailure + ex.Message, ex);
					}
					throw;
				}
			}

			// Token: 0x0600004C RID: 76 RVA: 0x00003B54 File Offset: 0x00001D54
			internal override void RestartEndPoint()
			{
				throw new InvalidOperationException("Unsupported");
			}

			// Token: 0x0600004D RID: 77 RVA: 0x00003B60 File Offset: 0x00001D60
			internal override void StopEndPoint()
			{
				base.DebugTrace("Inside TCPConnectionManager StopEndPoint()", new object[0]);
				base.Stop();
			}

			// Token: 0x0600004E RID: 78 RVA: 0x00003B7C File Offset: 0x00001D7C
			internal override void Initialize()
			{
				base.DebugTrace("Inside TCPConnectionManager Initialize()", new object[0]);
				if (UMRecyclerConfig.TcpListeningPort < 0 || UMRecyclerConfig.TcpListeningPort > 65535)
				{
					throw new UMServiceBaseException(Strings.InvalidTCPPort(UMRecyclerConfig.TcpListeningPort.ToString(CultureInfo.InvariantCulture)));
				}
				try
				{
					base.ConnMgr = new RealTimeServerTcpConnectionManager(base.LocalFqdn);
				}
				catch (ArgumentException ex)
				{
					base.ErrorTrace("Couldnt initialize SIP endpoint, error={0}", new object[]
					{
						ex.ToString()
					});
					throw new UMServiceBaseException(Strings.SipEndpointStartFailure + ex.Message, ex);
				}
			}

			// Token: 0x0600004F RID: 79 RVA: 0x00003C2C File Offset: 0x00001E2C
			internal override void LogStartedListeningForCalls()
			{
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_UMServiceSocketOpen, null, new object[]
				{
					UMServiceBase.ServerNameForEventLogging,
					UMStartupMode.TCP,
					UMRecyclerConfig.TcpListeningPort,
					(this.ipAddressFamily == IPAddressFamily.IPv4Only) ? Strings.IPv4Only : Strings.IPv6Only
				});
			}

			// Token: 0x06000050 RID: 80 RVA: 0x00003C94 File Offset: 0x00001E94
			internal override void LogStoppedListeningForCalls()
			{
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_UMServiceSocketShutdown, null, new object[]
				{
					UMStartupMode.TCP,
					UMRecyclerConfig.TcpListeningPort
				});
			}
		}

		// Token: 0x0200000B RID: 11
		internal class TLSConnectionManager : UMConnectionManagerHelper.BaseUMConnectionManager
		{
			// Token: 0x06000051 RID: 81 RVA: 0x00003CD0 File Offset: 0x00001ED0
			internal TLSConnectionManager(UMService umservice, IPAddressFamily ipAddressFamily) : base(umservice, ipAddressFamily)
			{
				base.IsSecured = true;
				if (!string.IsNullOrEmpty(base.ExternalFqdn))
				{
					UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_ExternalFqdnDetected, null, new object[]
					{
						"SIP/TLS",
						base.ExternalFqdn
					});
				}
			}

			// Token: 0x06000052 RID: 82 RVA: 0x00003D24 File Offset: 0x00001F24
			internal override void LogStartedListeningForCalls()
			{
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_UMServiceSocketOpen, null, new object[]
				{
					UMServiceBase.ServerNameForEventLogging,
					UMStartupMode.TLS,
					UMRecyclerConfig.TlsListeningPort,
					(this.ipAddressFamily == IPAddressFamily.IPv4Only) ? Strings.IPv4Only : Strings.IPv6Only
				});
			}

			// Token: 0x06000053 RID: 83 RVA: 0x00003D8C File Offset: 0x00001F8C
			internal override void LogStoppedListeningForCalls()
			{
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_UMServiceSocketShutdown, null, new object[]
				{
					UMStartupMode.TLS,
					UMRecyclerConfig.TlsListeningPort
				});
			}

			// Token: 0x06000054 RID: 84 RVA: 0x00003DC8 File Offset: 0x00001FC8
			internal override void StartEndPoint()
			{
				base.DebugTrace("Inside TLSConnectionManager StartEndPoint: {0} {1}", new object[]
				{
					base.IPAddress,
					UMRecyclerConfig.TlsListeningPort
				});
				try
				{
					base.SIPEndPoint.ConnectionManager.StartListening(new IPEndPoint(base.IPAddress, UMRecyclerConfig.TlsListeningPort));
				}
				catch (Exception ex)
				{
					if (ex is RealTimeException || ex is SocketException)
					{
						base.ErrorTrace("Couldnt start SIP endpoint, error={0}", new object[]
						{
							ex.ToString()
						});
						throw new UMServiceBaseException(Strings.SipEndpointStartFailure + ex.Message, ex);
					}
					throw;
				}
			}

			// Token: 0x06000055 RID: 85 RVA: 0x00003E7C File Offset: 0x0000207C
			internal override void StopEndPoint()
			{
				base.DebugTrace("Inside TLSConnectionManager StopEndPoint()", new object[0]);
				if (base.ConnMgr != null)
				{
					base.ConnMgr.IncomingConnectionAdded -= this.IncomingConnectionAddedHandler;
					base.ConnMgr.IncomingTlsNegotiationFailed -= this.IncomingCallTlsErrorHandler;
				}
				base.Stop();
			}

			// Token: 0x06000056 RID: 86 RVA: 0x00003EE0 File Offset: 0x000020E0
			internal override void Initialize()
			{
				base.DebugTrace("Inside TLSConnectionManager PreReqsCheck()", new object[0]);
				if (UMRecyclerConfig.TlsListeningPort < 0 || UMRecyclerConfig.TlsListeningPort > 65535)
				{
					throw new UMServiceBaseException(Strings.InvalidTLSPort(UMRecyclerConfig.TlsListeningPort.ToString(CultureInfo.InvariantCulture)));
				}
				try
				{
					CertificateUtils.UMCertificate = base.ServiceInstance.GetCertificateFromThumbprint(base.LocalServer.UMCertificateThumbprint);
					base.ConnMgr = new RealTimeServerTlsConnectionManager(base.LocalFqdn, CertificateUtils.UMCertificate.Issuer, CertificateUtils.UMCertificate.GetSerialNumber());
					base.ConnMgr.OutboundConnectionDefaultAddressFamilyHint = new AddressFamilyHint?(0);
					((RealTimeServerTlsConnectionManager)base.ConnMgr).NeedMutualTls = true;
					this.SetAllowedDomains();
					SipPeerManager.Instance.SipPeerListChanged += delegate(object sender, EventArgs args)
					{
						this.SetAllowedDomains();
					};
					base.ConnMgr.IncomingConnectionAdded += this.IncomingConnectionAddedHandler;
					base.ConnMgr.IncomingTlsNegotiationFailed += this.IncomingCallTlsErrorHandler;
				}
				catch (Exception ex)
				{
					base.ErrorTrace("Couldnt initialize SIP TLS endpoint, error={0}", new object[]
					{
						ex.ToString()
					});
					if (ex is CryptographicException || ex is SecurityException || ex is ArgumentException || ex is IOException)
					{
						throw new UMServiceBaseException(Strings.TLSEndPointInitializationFailure(ex.Message));
					}
					if (ex is TlsFailureException)
					{
						throw new UMServiceBaseException(Strings.TLSEndPointInitializationFailure(UMConnectionManagerHelper.TLSConnectionManager.GetTlsError((TlsFailureException)ex)));
					}
					throw;
				}
			}

			// Token: 0x06000057 RID: 87 RVA: 0x00004074 File Offset: 0x00002274
			internal override void RestartEndPoint()
			{
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_StoppingListeningforCertificateChange, null, new object[0]);
				bool flag = false;
				try
				{
					base.DebugTrace("Stopping RealTimeServerConnectionManager for certificate changing.", new object[0]);
					base.ConnMgr.StopListening();
					base.ConnMgr.SetLocalCertificate(CertificateUtils.UMCertificate.Issuer, CertificateUtils.UMCertificate.GetSerialNumber());
					base.DebugTrace("Certificate successfully set on the RealTimeServerConnectionManager.", new object[0]);
					flag = true;
				}
				catch (Exception ex)
				{
					base.ErrorTrace("In RestartEndPoint, error={0}", new object[]
					{
						ex.ToString()
					});
					if (ex is RealTimeException || ex is InvalidOperationException || ex is CryptographicException || ex is IOException)
					{
						throw new UMServiceBaseException(Strings.ErrorChangingCertificates(UMServiceBase.ServiceNameForEventLogging, UMServiceBase.ServerNameForEventLogging) + ex.Message, ex);
					}
					throw;
				}
				if (flag)
				{
					int num = 0;
					Exception ex2 = null;
					base.DebugTrace("Restarting RealTimeServerConnectionManager.", new object[0]);
					do
					{
						try
						{
							base.ConnMgr.StartListening(new IPEndPoint(base.IPAddress, UMRecyclerConfig.TlsListeningPort));
							break;
						}
						catch (RealTimeException ex3)
						{
							ex2 = ex3;
							num++;
							base.ErrorTrace("In RestartEndPoints StartEndpoint, error={0}", new object[]
							{
								ex3.ToString()
							});
						}
						catch (SocketException ex4)
						{
							ex2 = ex4;
							num++;
							base.ErrorTrace("In RestartEndPoints StartEndpoint, error={0}", new object[]
							{
								ex4.ToString()
							});
						}
						catch (InvalidOperationException ex5)
						{
							ex2 = ex5;
							num++;
							base.ErrorTrace("In RestartEndPoints StartEndpoint, error={0}", new object[]
							{
								ex5.ToString()
							});
						}
						Thread.Sleep(1000);
					}
					while (num < 5);
					if (num >= 5)
					{
						throw new UMServiceBaseException(Strings.ErrorChangingCertificates(UMServiceBase.ServiceNameForEventLogging, UMServiceBase.ServerNameForEventLogging) + ex2.Message, ex2);
					}
					UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_StartedListeningWithNewCertificate, null, new object[0]);
					base.DebugTrace("RestartEndPoints - all success resturning.", new object[0]);
				}
			}

			// Token: 0x06000058 RID: 88 RVA: 0x000042BC File Offset: 0x000024BC
			internal void IncomingCallTlsErrorHandler(object sender, ErrorEventArgs e)
			{
				TlsFailureException ex = (TlsFailureException)e.GetException();
				CallIdTracer.TraceError(ExTraceGlobals.ServiceTracer, 0, "received an IncomingCallTlsFailure {0}", new object[]
				{
					ex
				});
				Util.AddTlsErrorEventLogEntry(UMEventLogConstants.Tuple_IncomingTLSCallFailure, ex.RemoteCertificate, ex.RemoteEndpoint, ex.LocalEndpoint, UMConnectionManagerHelper.TLSConnectionManager.GetTlsError(ex));
				if (ex.RemoteCertificate != null)
				{
					UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_UMServiceCallRejected, null, new object[]
					{
						UMServiceBase.ServiceNameForEventLogging,
						UMConnectionManagerHelper.TLSConnectionManager.GetTlsError(ex)
					});
					CallRejectionCounterHelper.Instance.SetCounters(ex, new Action<bool>(UMConnectionManagerHelper.BaseUMConnectionManager.SetCallRejectionCounters), false, false);
				}
			}

			// Token: 0x06000059 RID: 89 RVA: 0x00004368 File Offset: 0x00002568
			private static string GetTlsError(TlsFailureException e)
			{
				switch (e.FailureReason)
				{
				case 0:
					return Strings.TlsOther(e.ErrorCode, e.Message);
				case 1:
					return Strings.TlsLocalCertificateNotFound(e.ErrorCode, e.Message);
				case 2:
					return Strings.TlsUntrustedRemoteCertificate(e.ErrorCode, e.Message);
				case 3:
					return Strings.TlsIncorrectNameInRemoteCertificate(e.ErrorCode, e.Message);
				case 4:
					return Strings.TlsCertificateExpired(e.ErrorCode, e.Message);
				case 5:
					return Strings.TlsTlsNegotiationFailure(e.ErrorCode, e.Message);
				case 6:
					return Strings.TlsRemoteDisconnected(e.ErrorCode, e.Message);
				case 7:
					return Strings.TlsRemoteCertificateRevoked(e.ErrorCode, e.Message);
				case 8:
					return Strings.TlsRemoteCertificateInvalidUsage(e.ErrorCode, e.Message);
				default:
					return e.Message;
				}
			}

			// Token: 0x0600005A RID: 90 RVA: 0x00004480 File Offset: 0x00002680
			private void SetAllowedDomains()
			{
				List<string> list = new List<string>();
				foreach (UMSipPeer umsipPeer in SipPeerManager.Instance.GetSecuredSipPeers())
				{
					list.Add(umsipPeer.Address.ToString());
				}
				((RealTimeServerTlsConnectionManager)base.ConnMgr).SetAllowedDomains(list);
			}

			// Token: 0x0600005B RID: 91 RVA: 0x000044F8 File Offset: 0x000026F8
			private void IncomingConnectionAddedHandler(object sender, IncomingConnectionAddedEventArgs e)
			{
				e.Item.ApplicationContext = e.MatchingDomainName;
			}
		}

		// Token: 0x0200000C RID: 12
		internal class MultiConnectionManager : UMConnectionManagerHelper.UMConnectionManager
		{
			// Token: 0x0600005D RID: 93 RVA: 0x0000450B File Offset: 0x0000270B
			internal MultiConnectionManager(UMConnectionManagerHelper.BaseUMConnectionManager[] connectionManagers, UMService umservice) : base(umservice)
			{
				this.connectionManagers = connectionManagers;
			}

			// Token: 0x0600005E RID: 94 RVA: 0x0000451C File Offset: 0x0000271C
			internal override void StartEndPoint()
			{
				this.DebugTrace("Inside MultiConnectionManager StartEndPoint()", new object[0]);
				foreach (UMConnectionManagerHelper.BaseUMConnectionManager baseUMConnectionManager in this.connectionManagers)
				{
					baseUMConnectionManager.StartEndPoint();
				}
			}

			// Token: 0x0600005F RID: 95 RVA: 0x0000455C File Offset: 0x0000275C
			internal override void StopEndPoint()
			{
				this.DebugTrace("Inside MultiConnectionManager StopEndPoint()", new object[0]);
				foreach (UMConnectionManagerHelper.BaseUMConnectionManager baseUMConnectionManager in this.connectionManagers)
				{
					baseUMConnectionManager.StopEndPoint();
				}
			}

			// Token: 0x06000060 RID: 96 RVA: 0x000045A4 File Offset: 0x000027A4
			internal override void RestartEndPoint()
			{
				foreach (UMConnectionManagerHelper.BaseUMConnectionManager baseUMConnectionManager in from cm in this.connectionManagers
				where cm is UMConnectionManagerHelper.TLSConnectionManager
				select cm)
				{
					baseUMConnectionManager.RestartEndPoint();
				}
			}

			// Token: 0x06000061 RID: 97 RVA: 0x00004614 File Offset: 0x00002814
			internal override void Initialize()
			{
				this.DebugTrace("Inside MultiConnectionManager Initialize()", new object[0]);
				foreach (UMConnectionManagerHelper.BaseUMConnectionManager baseUMConnectionManager in this.connectionManagers)
				{
					baseUMConnectionManager.Initialize();
				}
			}

			// Token: 0x06000062 RID: 98 RVA: 0x00004654 File Offset: 0x00002854
			internal override void EnsureListeningForCalls()
			{
				this.DebugTrace("Inside MultiConnectionManager EnsureListeningForCalls()", new object[0]);
				foreach (UMConnectionManagerHelper.BaseUMConnectionManager baseUMConnectionManager in this.connectionManagers)
				{
					baseUMConnectionManager.EnsureListeningForCalls();
				}
			}

			// Token: 0x06000063 RID: 99 RVA: 0x00004694 File Offset: 0x00002894
			internal override void StopListeningForCalls()
			{
				this.DebugTrace("Inside MultiConnectionManager StopListeningAndCloseSockets()", new object[0]);
				foreach (UMConnectionManagerHelper.BaseUMConnectionManager baseUMConnectionManager in this.connectionManagers)
				{
					baseUMConnectionManager.StopListeningForCalls();
				}
			}

			// Token: 0x06000064 RID: 100 RVA: 0x000046D4 File Offset: 0x000028D4
			internal override void LogStartedListeningForCalls()
			{
				foreach (UMConnectionManagerHelper.BaseUMConnectionManager baseUMConnectionManager in this.connectionManagers)
				{
					baseUMConnectionManager.LogStartedListeningForCalls();
				}
			}

			// Token: 0x06000065 RID: 101 RVA: 0x00004700 File Offset: 0x00002900
			internal override void LogStoppedListeningForCalls()
			{
				foreach (UMConnectionManagerHelper.BaseUMConnectionManager baseUMConnectionManager in this.connectionManagers)
				{
					baseUMConnectionManager.LogStoppedListeningForCalls();
				}
			}

			// Token: 0x06000066 RID: 102 RVA: 0x0000472C File Offset: 0x0000292C
			private void DebugTrace(string formatString, params object[] formatObjects)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, this.GetHashCode(), formatString, formatObjects);
			}

			// Token: 0x04000024 RID: 36
			private UMConnectionManagerHelper.BaseUMConnectionManager[] connectionManagers;
		}
	}
}
