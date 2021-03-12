using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Net.Sockets;
using System.Security;
using System.Security.Cryptography;
using System.Threading;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.Diagnostics.LatencyDetection;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.UM.TroubleshootingTool.Shared;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCore;
using Microsoft.Exchange.UM.UMCore.Exceptions;
using Microsoft.Rtc.Collaboration;
using Microsoft.Rtc.Collaboration.AudioVideo;
using Microsoft.Rtc.Signaling;

namespace Microsoft.Exchange.UM.UcmaPlatform
{
	// Token: 0x02000042 RID: 66
	internal class UcmaCallRouterPlatform : BaseCallRouterPlatform
	{
		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060002F3 RID: 755 RVA: 0x0000B8BD File Offset: 0x00009ABD
		// (set) Token: 0x060002F4 RID: 756 RVA: 0x0000B8C5 File Offset: 0x00009AC5
		private DiagnosticHelper Diag { get; set; }

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060002F5 RID: 757 RVA: 0x0000B8CE File Offset: 0x00009ACE
		// (set) Token: 0x060002F6 RID: 758 RVA: 0x0000B8EB File Offset: 0x00009AEB
		private UcmaCallRouterApplicationEndpoint TcpEndpointExternalInterface
		{
			get
			{
				if (this.tcpEndpointExternalInterface == null)
				{
					this.tcpEndpointExternalInterface = this.GetExternalInterfaceEndPoint(1);
				}
				return this.tcpEndpointExternalInterface;
			}
			set
			{
				this.tcpEndpointExternalInterface = value;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060002F7 RID: 759 RVA: 0x0000B8F4 File Offset: 0x00009AF4
		// (set) Token: 0x060002F8 RID: 760 RVA: 0x0000B911 File Offset: 0x00009B11
		private UcmaCallRouterApplicationEndpoint TlsEndpointExternalInterface
		{
			get
			{
				if (this.tlsEndpointExternalInterface == null)
				{
					this.tlsEndpointExternalInterface = this.GetExternalInterfaceEndPoint(2);
				}
				return this.tlsEndpointExternalInterface;
			}
			set
			{
				this.tlsEndpointExternalInterface = value;
			}
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x0000B91C File Offset: 0x00009B1C
		public UcmaCallRouterPlatform(LocalizedString serviceName, LocalizedString serverName, UMADSettings config) : base(serviceName, serverName, config)
		{
			this.Diag = new DiagnosticHelper(this, ExTraceGlobals.UMCallRouterTracer);
			this.Diag.Trace("UcmaCallRouterPlatform : startupMode = {0}", new object[]
			{
				config.UMStartupMode
			});
			this.optionsReplyHeaders = UcmaCallRouterPlatform.FrameOptionsHeaders();
			SipPeerManager.Instance.SipPeerListChanged += this.SipPeerListChanged;
		}

		// Token: 0x060002FA RID: 762 RVA: 0x0000B9F0 File Offset: 0x00009BF0
		public override void StopListening()
		{
			this.Diag.Trace("UcmaCallRouterPlatform : StopListening on external interfaces.", new object[0]);
			this.isPlatformEnabled = false;
			this.ExecuteAndHandleErrors(delegate
			{
				lock (this.endpointLock)
				{
					this.StopListening(false);
					this.externalInterfaceConnections = null;
					this.TcpEndpointExternalInterface = null;
					this.TlsEndpointExternalInterface = null;
				}
			}, Strings.UnableToStopListening(this.serviceName));
			this.StartListeningOnLoopBack();
		}

		// Token: 0x060002FB RID: 763 RVA: 0x0000BA74 File Offset: 0x00009C74
		public override void ChangeCertificate()
		{
			this.Diag.Trace("UcmaCallRouterPlatform : ChangeCertificate", new object[0]);
			this.ExecuteAndHandleErrors(delegate
			{
				if (this.TlsEndpointExternalInterface != null)
				{
					UcmaUtils.ChangeCertificate(this.TlsEndpointExternalInterface.Platform);
					this.Diag.Trace("UcmaCallRouterPlatform : ChangeCertificate done", new object[0]);
				}
			}, Strings.ErrorChangingCertificates(this.serviceName, this.serverName));
		}

		// Token: 0x060002FC RID: 764 RVA: 0x0000BBD4 File Offset: 0x00009DD4
		public override void SendPingAsync(PingInfo info, PingCompletedDelegate pingCompleted)
		{
			bool flag = false;
			try
			{
				object obj;
				Monitor.Enter(obj = this.endpointLock, ref flag);
				RealTimeEndpoint endpoint = null;
				if (info.Peer.UseMutualTLS)
				{
					if (this.TlsEndpointExternalInterface != null)
					{
						endpoint = this.TlsEndpointExternalInterface.InnerEndpoint;
					}
				}
				else if (this.TcpEndpointExternalInterface != null)
				{
					endpoint = this.TcpEndpointExternalInterface.InnerEndpoint;
				}
				if (endpoint != null)
				{
					Exception ex = UcmaUtils.CatchRealtimeErrors(delegate
					{
						endpoint.BeginSendMessage(2, new RealTimeAddress(info.TargetUri), null, null, this.optionsReplyHeaders, delegate(IAsyncResult ar)
						{
							SipResponseData data = null;
							Exception ex2 = UcmaUtils.CatchRealtimeErrors(delegate
							{
								data = endpoint.EndSendMessage(ar);
							}, this.Diag);
							UcmaUtils.SetPingInfoErrorResult(info, data, ex2);
							pingCompleted(info);
						}, null);
					}, this.Diag);
					if (ex != null)
					{
						UcmaUtils.SetPingInfoErrorResult(info, null, ex);
						pingCompleted(info);
					}
				}
			}
			finally
			{
				if (flag)
				{
					object obj;
					Monitor.Exit(obj);
				}
			}
		}

		// Token: 0x060002FD RID: 765 RVA: 0x0000BD04 File Offset: 0x00009F04
		public override void StartListening()
		{
			this.Diag.Trace("UcmaCallRouterPlatform : StartListening on external interfaces. Stop listening on only loopback interfaces.", new object[0]);
			this.StopListeningOnLoopBack();
			this.ExecuteAndHandleErrors(delegate
			{
				this.isPlatformEnabled = true;
				this.userNotificationEventManager = new UserNotificationEventManager();
				this.externalInterfaceConnections = new List<UcmaCallRouterApplicationEndpoint>(2);
				this.InitializeConnections(false);
			}, Strings.SipEndpointStartFailure);
			UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_CallRouterSocketOpen, null, new object[]
			{
				this.eventLogStringForMode,
				this.eventLogStringForPorts
			});
			this.Diag.Trace("All External Interface platforms started!", new object[0]);
		}

		// Token: 0x060002FE RID: 766 RVA: 0x0000BD8C File Offset: 0x00009F8C
		private static List<SignalingHeader> FrameOptionsHeaders()
		{
			return new List<SignalingHeader>(10)
			{
				new SignalingHeader("Accept", "application/sdp"),
				new SignalingHeader("Allow", "INVITE"),
				new SignalingHeader("Allow", "BYE"),
				new SignalingHeader("Allow", "CANCEL"),
				new SignalingHeader("Allow", "OPTIONS"),
				new SignalingHeader("Allow", "ACK"),
				new SignalingHeader("Allow", "INFO"),
				new SignalingHeader("Allow", "NOTIFY")
			};
		}

		// Token: 0x060002FF RID: 767 RVA: 0x0000BE4C File Offset: 0x0000A04C
		private void StopListening(bool isLocal)
		{
			List<UcmaCallRouterApplicationEndpoint> list = isLocal ? this.localLoopConnections : this.externalInterfaceConnections;
			if (list != null)
			{
				foreach (UcmaCallRouterApplicationEndpoint ucmaCallRouterApplicationEndpoint in list)
				{
					this.Diag.Trace(string.Format("Stopping on connection {0}:{1} ", ucmaCallRouterApplicationEndpoint.Platform.Transport, ucmaCallRouterApplicationEndpoint.Platform.ListeningIPAddress), new object[0]);
					this.StopListeningAndDestroyEndpoint(ucmaCallRouterApplicationEndpoint);
				}
			}
			if (this.userNotificationEventManager != null)
			{
				this.Diag.Trace("Stopping UserNotificationEventManager", new object[0]);
				this.userNotificationEventManager.Terminate();
				this.userNotificationEventManager = null;
			}
			UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_UMCallRouterSocketShutdown, null, new object[0]);
		}

		// Token: 0x06000300 RID: 768 RVA: 0x0000BF54 File Offset: 0x0000A154
		private void StopListeningOnLoopBack()
		{
			this.Diag.Trace("UcmaCallRouterPlatform : StopListeningOnLoopBack", new object[0]);
			this.isPlatformEnabled = false;
			this.ExecuteAndHandleErrors(delegate
			{
				this.StopListening(true);
				this.localLoopConnections = null;
				this.Diag.Trace("All LocalLoop platforms stopped!", new object[0]);
			}, Strings.UnableToStopListening(this.serviceName));
		}

		// Token: 0x06000301 RID: 769 RVA: 0x0000BFCC File Offset: 0x0000A1CC
		private void StartListeningOnLoopBack()
		{
			this.Diag.Trace("UcmaCallRouterPlatform : StartListeningOnLoopBack", new object[0]);
			this.ExecuteAndHandleErrors(delegate
			{
				this.isPlatformEnabled = true;
				this.userNotificationEventManager = new UserNotificationEventManager();
				this.localLoopConnections = new List<UcmaCallRouterApplicationEndpoint>();
				this.InitializeConnections(true);
			}, Strings.SipEndpointStartFailure);
			UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_CallRouterSocketOpen, null, new object[]
			{
				this.eventLogStringForMode,
				this.eventLogStringForPorts
			});
			this.Diag.Trace("All platforms started!", new object[0]);
		}

		// Token: 0x06000302 RID: 770 RVA: 0x0000C04C File Offset: 0x0000A24C
		private SipTransportType[] GetEnabledTransportTypes()
		{
			SipTransportType[] result;
			if (this.config.UMStartupMode == UMStartupMode.Dual)
			{
				result = new SipTransportType[]
				{
					1,
					2
				};
			}
			else if (this.config.UMStartupMode == UMStartupMode.TCP)
			{
				result = new SipTransportType[]
				{
					1
				};
			}
			else
			{
				result = new SipTransportType[]
				{
					2
				};
			}
			return result;
		}

		// Token: 0x06000303 RID: 771 RVA: 0x0000C0A4 File Offset: 0x0000A2A4
		private void InitializeConnections(bool isLocal)
		{
			IPAddress[] array2;
			if (!isLocal)
			{
				IPAddress[] array = new IPAddress[1];
				array2 = array;
			}
			else
			{
				array2 = Utils.GetLoopbackIPAddresses();
			}
			IPAddress[] array3 = array2;
			foreach (IPAddress ipaddress in array3)
			{
				foreach (SipTransportType sipTransportType in this.GetEnabledTransportTypes())
				{
					this.Diag.Trace("Starting {0} platform on IP {1}", new object[]
					{
						sipTransportType,
						(ipaddress == null) ? "Any" : ipaddress.ToString()
					});
					CollaborationPlatform platform = this.InitializePlatform(sipTransportType, ipaddress);
					UcmaCallRouterApplicationEndpoint item = this.CreateEndpointAndStartListening(platform);
					if (isLocal)
					{
						this.localLoopConnections.Add(item);
					}
					else
					{
						this.externalInterfaceConnections.Add(item);
					}
				}
			}
		}

		// Token: 0x06000304 RID: 772 RVA: 0x0000C172 File Offset: 0x0000A372
		private void SipPeerListChanged(object sender, EventArgs args)
		{
			if (this.TlsEndpointExternalInterface != null)
			{
				UcmaUtils.HandleSIPPeerListChanged(this.TlsEndpointExternalInterface.Platform, this.Diag);
			}
		}

		// Token: 0x06000305 RID: 773 RVA: 0x0000C194 File Offset: 0x0000A394
		private CollaborationPlatform InitializePlatform(SipTransportType transport, IPAddress ipAddress = null)
		{
			CollaborationPlatform result = null;
			if (transport == 1)
			{
				if (!UcmaUtils.IsPortValid(this.config.SipTcpListeningPort))
				{
					throw new UMServiceBaseException(Strings.InvalidTCPPort(this.config.SipTcpListeningPort.ToString()));
				}
				result = UcmaUtils.GetTCPPlatform(this.config.SipTcpListeningPort, 0, true, this.Diag, ipAddress);
				this.Diag.Trace("UcmaCallRouterPlatform: InitializePlatform : transportType = {0}, port = {1}", new object[]
				{
					transport,
					this.config.SipTcpListeningPort
				});
			}
			else if (transport == 2)
			{
				if (!UcmaUtils.IsPortValid(this.config.SipTlsListeningPort))
				{
					throw new UMServiceBaseException(Strings.InvalidTCPPort(this.config.SipTcpListeningPort.ToString()));
				}
				result = UcmaUtils.GetTLSPlatform(this.config.SipTlsListeningPort, 0, true, this.Diag, new EventHandler<ErrorEventArgs>(this.ConnectionManager_IncomingTlsNegotiationFailed), ipAddress);
				this.Diag.Trace("UcmaCallRouterPlatform: InitializePlatform : transportType = {0}, port = {1}", new object[]
				{
					transport,
					this.config.SipTlsListeningPort
				});
			}
			return result;
		}

		// Token: 0x06000306 RID: 774 RVA: 0x0000C2CD File Offset: 0x0000A4CD
		private void RegisterForIncomingCalls(UcmaCallRouterApplicationEndpoint endpoint)
		{
			this.Diag.Trace("UcmaCallRouterPlatform : RegisterForIncomingCalls", new object[0]);
			endpoint.RegisterForIncomingCall<AudioVideoCall>(new IncomingCallDelegate<AudioVideoCall>(this.ApplicationEndpoint_CallReceived));
		}

		// Token: 0x06000307 RID: 775 RVA: 0x0000C2F7 File Offset: 0x0000A4F7
		private void UnRegisterForIncomingCalls(UcmaCallRouterApplicationEndpoint endpoint)
		{
			this.Diag.Trace("UcmaCallRouterPlatform : UnRegisterForIncomingCalls", new object[0]);
			endpoint.UnregisterForIncomingCall<AudioVideoCall>(new IncomingCallDelegate<AudioVideoCall>(this.ApplicationEndpoint_CallReceived));
		}

		// Token: 0x06000308 RID: 776 RVA: 0x0000C32A File Offset: 0x0000A52A
		private void ApplicationEndpoint_MessageReceived(object sender, MessageReceivedEventArgs e)
		{
			UcmaUtils.HandleMessageReceived(this.Diag, e, delegate(InfoMessage.PlatformMessageReceivedEventArgs args)
			{
				base.HandleMessageReceived(args);
			});
		}

		// Token: 0x06000309 RID: 777 RVA: 0x0000C3C0 File Offset: 0x0000A5C0
		private void ApplicationEndpoint_LegacyLyncNotificationCallReceived(object sender, SessionReceivedEventArgs e)
		{
			UcmaUtils.CatchRealtimeErrors(delegate
			{
				using (new CallId(e.Session.CallId))
				{
					this.userNotificationEventManager.OnIncomingSession(this.Diag, e, new UserNotificationEventHandler(this.HandleLegacyLyncNotification));
				}
			}, this.Diag);
		}

		// Token: 0x0600030A RID: 778 RVA: 0x0000C434 File Offset: 0x0000A634
		private void ApplicationEndpoint_CallReceived(object sender, CallReceivedEventArgs<AudioVideoCall> args)
		{
			Util.IncrementCounter(CallRouterAvailabilityCounters.UMCallRouterCallsReceived);
			this.Diag.Trace("UcmaCallRouterPlatform: ApplicationEndpoint_CallReceived", new object[0]);
			using (new CallId(args.Call.CallId))
			{
				LatencyDetectionContext latencyContext = Util.StartCallLatencyDetection("UcmaCallRouterPlatform", args.Call.CallId);
				ExDateTime now = ExDateTime.Now;
				Exception ex = null;
				Exception realTimeError = null;
				PlatformSignalingHeader platformSignalingHeader = null;
				bool isDiagnosticCall = false;
				try
				{
					if (args.Connection == null)
					{
						this.Diag.Trace("Ignoring ApplicationEndpoint_CallReceived with null connection", new object[0]);
					}
					else if (this.isPlatformEnabled)
					{
						ex = UcmaUtils.ProcessPlatformRequestAndReportErrors(delegate
						{
							this.TryHandleIncomingCall(args, out realTimeError, out isDiagnosticCall);
						}, args.Call.CallId, out platformSignalingHeader);
					}
				}
				finally
				{
					Util.EndCallLatencyDetection(latencyContext, args.Call.CallId, now, args.RequestData.UserAgent, null, realTimeError == null && ex == null);
					ex = ((realTimeError != null) ? realTimeError : ex);
					if (ex != null && platformSignalingHeader == null)
					{
						platformSignalingHeader = CallRejectedException.RenderDiagnosticHeader(CallEndingReason.TransientError, null, new object[0]);
					}
					this.FinalizeCallReceived(ex, args, platformSignalingHeader, isDiagnosticCall);
				}
			}
		}

		// Token: 0x0600030B RID: 779 RVA: 0x0000C614 File Offset: 0x0000A814
		private bool TryHandleIncomingCall(CallReceivedEventArgs<AudioVideoCall> args, out Exception error, out bool isDiagnosticCall)
		{
			error = null;
			isDiagnosticCall = false;
			try
			{
				UcmaCallInfo ucmaCallInfo = new UcmaCallInfo(args);
				using (CafeRoutingContext context = new CafeRoutingContext(ucmaCallInfo, this.config))
				{
					if (args.Call != null && args.Call.Conversation != null)
					{
						args.Call.Conversation.Impersonate(ucmaCallInfo.QualityReportUri, null, null);
					}
					if (context.IsActiveMonitoring)
					{
						UcmaUtils.SendDiagnosticsInfoCallReceived(args.Call, this.Diag);
						UcmaUtils.CreateDiagnosticsTimers(args.Call, delegate(Timer o)
						{
							context.AddDiagnosticsTimer(o);
						}, this.Diag);
						UcmaUtils.AddCallDelayFaultInjectionInTestMode(3062246717U);
					}
					isDiagnosticCall = context.IsDiagnosticCall;
					RouterCallHandler.Handle(context);
					ExAssert.RetailAssert(context.RedirectUri != null, "Redirection Uri has not been set");
					this.Diag.Trace("Redirecting Call to target {0} with Code {1}", new object[]
					{
						context.RedirectUri,
						context.RedirectCode
					});
					if (context.IsActiveMonitoring)
					{
						UcmaUtils.SendDiagnosticsInfoCallRedirect(args.Call, context.RedirectUri.ToString(), this.Diag);
					}
					CallForwardOptions callForwardOptions = new CallForwardOptions(context.RedirectCode);
					args.Call.Forward(context.RedirectUri.ToString(), callForwardOptions);
					this.Diag.Trace("Call forwarded complete.", new object[0]);
					UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_CallRoutedSuccessfully, null, new object[]
					{
						CommonUtil.ToEventLogString(context.CallInfo.CallingParty.Uri),
						CommonUtil.ToEventLogString(context.CallInfo.CalledParty.Uri),
						CommonUtil.ToEventLogString(RouterUtils.GetDiversionLogString(context.CallInfo.DiversionInfo)),
						CommonUtil.ToEventLogString(context.ReferredByHeader),
						CommonUtil.ToEventLogString(context.CallInfo.CallId),
						CommonUtil.ToEventLogString(context.RedirectUri)
					});
				}
			}
			catch (InvalidOperationException ex)
			{
				error = ex;
			}
			catch (RealTimeException ex2)
			{
				error = ex2;
			}
			if (error != null)
			{
				this.Diag.Trace("TryHandleIncomingCall. Error: {0}", new object[]
				{
					error
				});
			}
			return error != null;
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0000C928 File Offset: 0x0000AB28
		private void FinalizeCallReceived(Exception error, CallReceivedEventArgs<AudioVideoCall> args, PlatformSignalingHeader diagnosticHeader, bool isDiagnosticCall)
		{
			this.Diag.Trace("UcmaCallRouterPlatform : FinalizeCallReceived.  Error='{0}' , isDiagnosticCall ={1}", new object[]
			{
				(error == null) ? "null" : error.ToString(),
				isDiagnosticCall
			});
			if (error != null)
			{
				this.LogCallRejected(error, diagnosticHeader, isDiagnosticCall);
				CallDeclineOptions options = new CallDeclineOptions(403);
				options.Headers.Add(new SignalingHeader(diagnosticHeader.Name, diagnosticHeader.Value));
				this.Diag.Trace("UcmaCallRouterPlatform : FinalizeCallReceived. Call rejected. DiagnosticHeader:{0}", new object[]
				{
					diagnosticHeader.Value
				});
				UcmaUtils.CatchRealtimeErrors(delegate
				{
					args.Call.Decline(options);
				}, this.Diag);
				return;
			}
			CallRejectionCounterHelper.Instance.SetCounters(null, new Action<bool>(BaseCallRouterPlatform.SetCallRejectionCounters), true, isDiagnosticCall);
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0000CA1C File Offset: 0x0000AC1C
		private void LogCallRejected(Exception error, PlatformSignalingHeader diagnosticHeader, bool isDiagnosticCall)
		{
			if (error is InvalidOperationException || error is OperationTimeoutException)
			{
				this.Diag.Trace("UcmaCallRouterPlatform: LogCallRejected: ignoring error.", new object[0]);
				return;
			}
			UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_CallRouterCallRejected, null, new object[]
			{
				diagnosticHeader.Value + " " + CommonUtil.ToEventLogString(error)
			});
			CallRejectionCounterHelper.Instance.SetCounters(error, new Action<bool>(BaseCallRouterPlatform.SetCallRejectionCounters), false, isDiagnosticCall);
		}

		// Token: 0x0600030E RID: 782 RVA: 0x0000CA9C File Offset: 0x0000AC9C
		private void StopListeningAndDestroyEndpoint(UcmaCallRouterApplicationEndpoint endpoint)
		{
			this.Diag.Trace("UcmaCallRouterPlatform: StopListeningAndDestroyEndpoint", new object[0]);
			endpoint.InnerEndpoint.MessageReceived -= this.ApplicationEndpoint_MessageReceived;
			endpoint.LegacyLyncNotificationCallReceived -= this.ApplicationEndpoint_LegacyLyncNotificationCallReceived;
			this.UnRegisterForIncomingCalls(endpoint);
			if (endpoint.Platform.Transport == 2)
			{
				RealTimeServerTlsConnectionManager realTimeServerTlsConnectionManager = (RealTimeServerTlsConnectionManager)endpoint.Platform.ConnectionManager;
				realTimeServerTlsConnectionManager.IncomingTlsNegotiationFailed -= this.ConnectionManager_IncomingTlsNegotiationFailed;
			}
			UcmaUtils.StopPlatform(endpoint.Platform);
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0000CB48 File Offset: 0x0000AD48
		private UcmaCallRouterApplicationEndpoint GetExternalInterfaceEndPoint(SipTransportType transport)
		{
			if (this.externalInterfaceConnections != null)
			{
				return this.externalInterfaceConnections.Find((UcmaCallRouterApplicationEndpoint connection) => connection.Platform.Transport == transport);
			}
			return null;
		}

		// Token: 0x06000310 RID: 784 RVA: 0x0000CB8C File Offset: 0x0000AD8C
		private UcmaCallRouterApplicationEndpoint CreateEndpointAndStartListening(CollaborationPlatform platform)
		{
			this.Diag.Trace("UcmaCallRouterPlatform: CreateEndpointAndStartListening", new object[0]);
			UcmaUtils.StartPlatform(platform);
			this.Diag.Trace("CreateEndpointAndStartListening : platform started", new object[0]);
			ApplicationEndpointSettings applicationEndpointSettings = new ApplicationEndpointSettings(UcmaUtils.GetOwnerUri());
			applicationEndpointSettings.IsDefaultRoutingEndpoint = true;
			applicationEndpointSettings.SetEndpointType(1, 0);
			if (CommonConstants.UseDataCenterLogging)
			{
				applicationEndpointSettings.ProvisioningDataQueryDisabled = true;
				applicationEndpointSettings.PublishingQoeMetricsDisabled = false;
			}
			UcmaCallRouterApplicationEndpoint ucmaCallRouterApplicationEndpoint = new UcmaCallRouterApplicationEndpoint(platform, applicationEndpointSettings);
			UcmaUtils.StartEndpoint(ucmaCallRouterApplicationEndpoint);
			this.Diag.Trace("CreateEndpointAndStartListening : endpoint started", new object[0]);
			this.RegisterForIncomingCalls(ucmaCallRouterApplicationEndpoint);
			this.Diag.Trace("CreateEndpointAndStartListening : Registered For IncomingCalls", new object[0]);
			ucmaCallRouterApplicationEndpoint.InnerEndpoint.MessageReceived += this.ApplicationEndpoint_MessageReceived;
			ucmaCallRouterApplicationEndpoint.LegacyLyncNotificationCallReceived += this.ApplicationEndpoint_LegacyLyncNotificationCallReceived;
			return ucmaCallRouterApplicationEndpoint;
		}

		// Token: 0x06000311 RID: 785 RVA: 0x0000CC68 File Offset: 0x0000AE68
		private void ConnectionManager_IncomingTlsNegotiationFailed(object sender, ErrorEventArgs e)
		{
			TlsFailureException ex = (TlsFailureException)e.GetException();
			this.Diag.Trace("TLS Negotiation Failed: {0}", new object[]
			{
				ex
			});
			Util.AddTlsErrorEventLogEntry(UMEventLogConstants.Tuple_CallRouterIncomingTLSCallFailure, ex.RemoteCertificate, ex.RemoteEndpoint, ex.LocalEndpoint, UcmaUtils.GetTlsError(ex));
			if (ex.RemoteCertificate != null)
			{
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_CallRouterCallRejected, null, new object[]
				{
					UcmaUtils.GetTlsError(ex)
				});
				Util.IncrementCounter(CallRouterAvailabilityCounters.UMCallRouterCallsReceived);
				if (!CommonConstants.UseDataCenterLogging)
				{
					CallRejectionCounterHelper.Instance.SetCounters(ex, new Action<bool>(BaseCallRouterPlatform.SetCallRejectionCounters), false, false);
				}
			}
		}

		// Token: 0x06000312 RID: 786 RVA: 0x0000CD13 File Offset: 0x0000AF13
		private bool IsKnownException(Exception e)
		{
			return e is CryptographicException || e is SecurityException || e is IOException || e is RealTimeException || e is SocketException || e is InvalidOperationException;
		}

		// Token: 0x06000313 RID: 787 RVA: 0x0000CD48 File Offset: 0x0000AF48
		private void ExecuteAndHandleErrors(UcmaCallRouterPlatform.PlatformCallDelegate function, LocalizedString message)
		{
			try
			{
				function();
			}
			catch (Exception ex)
			{
				this.Diag.Trace("UcmaCallRouterPlatform : ExecuteAndHandleErrors error ={0}", new object[]
				{
					ex
				});
				if (this.IsKnownException(ex))
				{
					throw new UMServiceBaseException(message + " " + ex.Message, ex);
				}
				throw;
			}
		}

		// Token: 0x06000314 RID: 788 RVA: 0x0000CDB4 File Offset: 0x0000AFB4
		protected override void SendServiceRequest(string fromUri, RedirectionTarget.ResultSet backend, byte[] messageBody)
		{
			if (this.TlsEndpointExternalInterface == null)
			{
				throw new InvalidOperationException();
			}
			RealTimeEndpoint innerEndpoint = this.TlsEndpointExternalInterface.InnerEndpoint;
			RealTimeAddress realTimeAddress = new RealTimeAddress(backend.Uri.ToString());
			SendMessageOptions sendMessageOptions = new SendMessageOptions();
			sendMessageOptions.SetLocalIdentity(fromUri, string.Empty);
			ConnectionContext connectionContext = UcmaUtils.CreateConnectionContext(backend.Fqdn, backend.Port);
			connectionContext.AddressFamilyHint = new AddressFamilyHint?(0);
			System.Net.Mime.ContentType contentType = new System.Net.Mime.ContentType("application/ms-rtc-usernotification+xml");
			sendMessageOptions.ContentDescription = new ContentDescription(contentType, messageBody);
			int num = 0;
			Exception innerException = null;
			do
			{
				try
				{
					sendMessageOptions.ConnectionContext = connectionContext;
					innerEndpoint.EndSendMessage(innerEndpoint.BeginSendMessage(4, realTimeAddress, sendMessageOptions, null, null));
					UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_UserNotificationProxied, null, new object[]
					{
						CommonUtil.ToEventLogString(fromUri),
						CommonUtil.ToEventLogString(realTimeAddress.Uri),
						CommonUtil.ToEventLogString(connectionContext.Host),
						CommonUtil.ToEventLogString(connectionContext.Port)
					});
					return;
				}
				catch (FailureResponseException ex)
				{
					innerException = ex;
					SipResponseData responseData = ex.ResponseData;
					if (responseData.ResponseCode != 303)
					{
						throw;
					}
					this.GetRedirectionTarget(responseData, out realTimeAddress, out connectionContext);
				}
			}
			while (++num < 2);
			throw new InvalidOperationException("Too many redirects", innerException);
		}

		// Token: 0x06000315 RID: 789 RVA: 0x0000CF1C File Offset: 0x0000B11C
		protected void GetRedirectionTarget(SipResponseData response, out RealTimeAddress uriTarget, out ConnectionContext connectionTarget)
		{
			uriTarget = null;
			connectionTarget = null;
			SignalingHeader signalingHeader = response.SignalingHeaders.FirstOrDefault((SignalingHeader x) => x.Name.Equals("Contact", StringComparison.OrdinalIgnoreCase));
			if (signalingHeader != null)
			{
				SignalingHeaderParser signalingHeaderParser = new SignalingHeaderParser(signalingHeader);
				if (null != signalingHeaderParser.Uri)
				{
					uriTarget = new RealTimeAddress(signalingHeaderParser.Uri.ToString());
					connectionTarget = UcmaUtils.CreateConnectionContext(signalingHeaderParser.Uri.Host, signalingHeaderParser.Uri.Port);
					connectionTarget.AddressFamilyHint = new AddressFamilyHint?(0);
				}
			}
			if (uriTarget == null)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x06000316 RID: 790 RVA: 0x0000CFBC File Offset: 0x0000B1BC
		protected override void InternalDispose(bool disposing)
		{
		}

		// Token: 0x06000317 RID: 791 RVA: 0x0000CFBE File Offset: 0x0000B1BE
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<UcmaCallRouterPlatform>(this);
		}

		// Token: 0x040000F4 RID: 244
		private readonly List<SignalingHeader> optionsReplyHeaders;

		// Token: 0x040000F5 RID: 245
		private object endpointLock = new object();

		// Token: 0x040000F6 RID: 246
		private UcmaCallRouterApplicationEndpoint tcpEndpointExternalInterface;

		// Token: 0x040000F7 RID: 247
		private UcmaCallRouterApplicationEndpoint tlsEndpointExternalInterface;

		// Token: 0x040000F8 RID: 248
		private List<UcmaCallRouterApplicationEndpoint> externalInterfaceConnections;

		// Token: 0x040000F9 RID: 249
		private List<UcmaCallRouterApplicationEndpoint> localLoopConnections;

		// Token: 0x040000FA RID: 250
		private UserNotificationEventManager userNotificationEventManager;

		// Token: 0x02000043 RID: 67
		// (Invoke) Token: 0x06000320 RID: 800
		private delegate void PlatformCallDelegate();
	}
}
