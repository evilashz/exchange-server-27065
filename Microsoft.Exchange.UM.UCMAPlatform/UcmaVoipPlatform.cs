﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mime;
using System.Threading;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.Diagnostics.LatencyDetection;
using Microsoft.Exchange.UM.TroubleshootingTool.Shared;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCore;
using Microsoft.Exchange.UM.UMCore.Exceptions;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Rtc.Collaboration;
using Microsoft.Rtc.Collaboration.AudioVideo;
using Microsoft.Rtc.Signaling;

namespace Microsoft.Exchange.UM.UcmaPlatform
{
	// Token: 0x0200004C RID: 76
	internal class UcmaVoipPlatform : BaseUMVoipPlatform
	{
		// Token: 0x14000016 RID: 22
		// (add) Token: 0x0600037A RID: 890 RVA: 0x0000F2FC File Offset: 0x0000D4FC
		// (remove) Token: 0x0600037B RID: 891 RVA: 0x0000F334 File Offset: 0x0000D534
		internal override event VoipPlatformEventHandler<SendNotifyMessageCompletedEventArgs> OnSendNotifyMessageCompleted;

		// Token: 0x14000017 RID: 23
		// (add) Token: 0x0600037C RID: 892 RVA: 0x0000F36C File Offset: 0x0000D56C
		// (remove) Token: 0x0600037D RID: 893 RVA: 0x0000F3A4 File Offset: 0x0000D5A4
		internal override event VoipPlatformEventHandler<InfoMessage.PlatformMessageReceivedEventArgs> OnMessageReceived;

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x0600037E RID: 894 RVA: 0x0000F3D9 File Offset: 0x0000D5D9
		// (set) Token: 0x0600037F RID: 895 RVA: 0x0000F3E1 File Offset: 0x0000D5E1
		private DiagnosticHelper Diag { get; set; }

		// Token: 0x06000380 RID: 896 RVA: 0x0000F3EA File Offset: 0x0000D5EA
		internal static void InitializeOutboundCallContext(UcmaCallInfo callInfo, CallContext cc)
		{
			ValidateArgument.NotNull(callInfo, "callInfo");
			ValidateArgument.NotNull(cc, "cc");
			cc.PopulateCallContext(true, callInfo);
		}

		// Token: 0x06000381 RID: 897 RVA: 0x0000F40C File Offset: 0x0000D60C
		internal static void InitializeInboundCallContext(UcmaCallInfo callInfo, UMIPGateway gw, CallContext cc)
		{
			ValidateArgument.NotNull(callInfo, "callInfo");
			ValidateArgument.NotNull(cc, "cc");
			cc.GatewayConfig = gw;
			cc.PopulateCallContext(false, callInfo);
			if (cc.DialPlan == null && !cc.IsLocalDiagnosticCall && cc.OfferResult != OfferResult.Redirect)
			{
				throw CallRejectedException.Create(Strings.NoDialPlanFound, CallEndingReason.DialPlanNotFound, null, new object[0]);
			}
		}

		// Token: 0x06000382 RID: 898 RVA: 0x0000F470 File Offset: 0x0000D670
		internal static void HandlePossibleMRASIssues(MediaChannelEstablishmentData data)
		{
			MediaChannelEstablishmentDiagnosticsReason diagnosticsReason = data.GetDiagnosticsReason();
			if (data.EstablishmentStatus != 1)
			{
				UcmaVoipPlatform.LogChannelEstablishmentFailure(data);
			}
			CallIdTracer.TraceDebug(ExTraceGlobals.UCMATracer, null, "HandlePossibleMRASIssues: IsHeuristicBasedSIPAccessService = {0}, MediaChannelEstablishmentStatus = {1}, MediaChannelEstablishmentDiagnosticsReason = {2}", new object[]
			{
				SipPeerManager.Instance.IsHeuristicBasedSIPAccessService,
				data.EstablishmentStatus,
				diagnosticsReason
			});
			if (!SipPeerManager.Instance.IsHeuristicBasedSIPAccessService)
			{
				bool flag = false;
				bool flag2 = false;
				switch (diagnosticsReason)
				{
				case 2:
					UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_MRASCredentialsAcquisitionFailed, null, new object[]
					{
						(UcmaVoipPlatform.currentlyPassedSIPProxy == null) ? string.Empty : UcmaVoipPlatform.currentlyPassedSIPProxy.ToHostPortString()
					});
					UMEventNotificationHelper.PublishUMFailureEventNotificationItem(ExchangeComponent.UMProtocol, UMNotificationEvent.MediaEdgeAuthenticationServiceCredentialsAcquisition.ToString());
					flag = true;
					break;
				case 3:
				{
					MediaEdgeResourceAllocationDiagnosticsReason mediaEdgeResourceAllocationDiagnosticsReason = data.GetMediaEdgeResourceAllocationDiagnosticsReason();
					CallIdTracer.TraceDebug(ExTraceGlobals.UCMATracer, null, "MediaEdgeResourceAllocationFailed with Error Code: {0}", new object[]
					{
						mediaEdgeResourceAllocationDiagnosticsReason
					});
					UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_MRASResourceAllocationFailed, null, new object[]
					{
						(UcmaVoipPlatform.currentlyPassedSIPProxy == null) ? string.Empty : UcmaVoipPlatform.currentlyPassedSIPProxy.ToHostPortString(),
						mediaEdgeResourceAllocationDiagnosticsReason
					});
					UMEventNotificationHelper.PublishUMFailureEventNotificationItem(ExchangeComponent.UMProtocol, UMNotificationEvent.MediaEdgeResourceAllocation.ToString());
					flag2 = true;
					break;
				}
				}
				if (!flag)
				{
					UMEventNotificationHelper.PublishUMSuccessEventNotificationItem(ExchangeComponent.UMProtocol, UMNotificationEvent.MediaEdgeAuthenticationServiceCredentialsAcquisition.ToString());
				}
				if (!flag2)
				{
					UMEventNotificationHelper.PublishUMSuccessEventNotificationItem(ExchangeComponent.UMProtocol, UMNotificationEvent.MediaEdgeResourceAllocation.ToString());
				}
				return;
			}
			if (data.EstablishmentStatus != 1)
			{
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_MRASMediaEstablishedStatusFailed, null, new object[]
				{
					(UcmaVoipPlatform.currentlyPassedSIPProxy == null) ? string.Empty : UcmaVoipPlatform.currentlyPassedSIPProxy.ToHostPortString()
				});
				UMEventNotificationHelper.PublishUMFailureEventNotificationItem(ExchangeComponent.UMProtocol, UMNotificationEvent.MediaEstablishedStatus.ToString());
				return;
			}
			UMEventNotificationHelper.PublishUMSuccessEventNotificationItem(ExchangeComponent.UMProtocol, UMNotificationEvent.MediaEstablishedStatus.ToString());
		}

		// Token: 0x06000383 RID: 899 RVA: 0x0000F67C File Offset: 0x0000D87C
		private static void LogChannelEstablishmentFailure(MediaChannelEstablishmentData data)
		{
			string text = string.Empty;
			string text2 = string.Empty;
			switch (data.GetDiagnosticsReason())
			{
			case 1:
				text = Strings.MediaEdgeAuthenticationServiceDiscoveryFailed;
				goto IL_DE;
			case 2:
				text = Strings.MediaEdgeAuthenticationServiceCredentialsAcquisitionFailed;
				goto IL_DE;
			case 3:
				text = Strings.MediaEdgeResourceAllocationFailed;
				switch (data.GetMediaEdgeResourceAllocationDiagnosticsReason())
				{
				case 2:
					text2 = Strings.MediaEdgeDnsResolutionFailure;
					goto IL_DE;
				case 3:
					text2 = Strings.MediaEdgeConnectionFailure;
					goto IL_DE;
				case 4:
					text2 = Strings.MediaEdgeCredentialsRejected;
					goto IL_DE;
				case 5:
					text2 = Strings.MediaEdgeFipsEncryptionNegotiationFailure;
					goto IL_DE;
				case 6:
					text2 = string.Empty;
					goto IL_DE;
				}
				text2 = Strings.MediaEdgeResourceAllocationUnknown;
				goto IL_DE;
			}
			text = Strings.MediaEdgeChannelEstablishmentUnknown;
			IL_DE:
			UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_MRASMediaChannelEstablishFailed, null, new object[]
			{
				(UcmaVoipPlatform.currentlyPassedSIPProxy == null) ? string.Empty : UcmaVoipPlatform.currentlyPassedSIPProxy.ToHostPortString(),
				text,
				text2
			});
			if (UcmaVoipPlatform.currentlyPassedSIPProxy == null)
			{
				CallIdTracer.TraceError(ExTraceGlobals.UCMATracer, null, "LogChannelEstablishmentFailure: {0} {1}", new object[]
				{
					text,
					text2
				});
				return;
			}
			PIIMessage data2 = PIIMessage.Create(PIIType._Uri, UcmaVoipPlatform.currentlyPassedSIPProxy.ToHostPortString());
			CallIdTracer.TraceError(ExTraceGlobals.UCMATracer, null, data2, "LogChannelEstablishmentFailure: _Uri {0} {1}", new object[]
			{
				text,
				text2
			});
		}

		// Token: 0x06000384 RID: 900 RVA: 0x0000F80A File Offset: 0x0000DA0A
		internal override void Initialize(int sipPort, UMCallSessionHandler<EventArgs> connectionHandler)
		{
			ValidateArgument.NotNull(connectionHandler, "connectionHandler");
			base.Initialize(sipPort, connectionHandler);
			this.Diag = new DiagnosticHelper(this, ExTraceGlobals.VoipPlatformTracer);
			UcmaVoipPlatform.SetMediaPortRange();
		}

		// Token: 0x06000385 RID: 901 RVA: 0x0000F838 File Offset: 0x0000DA38
		internal override void SendNotifyMessageAsync(PlatformSipUri sipUri, UMSipPeer nextHop, System.Net.Mime.ContentType contentType, byte[] body, string eventHeader, IList<PlatformSignalingHeader> headers, object asyncState)
		{
			ValidateArgument.NotNull(sipUri, "sipUri");
			ValidateArgument.NotNull(body, "body");
			ValidateArgument.NotNull(headers, "headers");
			RealTimeEndpoint realTimeEndpoint = nextHop.UseMutualTLS ? this.tlsEndpoint.InnerEndpoint : this.tcpEndpoint.InnerEndpoint;
			this.Diag.Assert(null != realTimeEndpoint, "SendNotifyMessageRequested on an unsupported transport. UseMutualTLS='{0}'", new object[]
			{
				nextHop.UseMutualTLS
			});
			UcmaVoipPlatform.SendNotifyMessageUserState sendNotifyMessageUserState = new UcmaVoipPlatform.SendNotifyMessageUserState
			{
				Endpoint = realTimeEndpoint,
				State = asyncState
			};
			SendMessageOptions sendMessageOptions = new SendMessageOptions();
			sendMessageOptions.ConnectionContext = UcmaUtils.CreateConnectionContext(nextHop.NextHopForOutboundRouting.Address.ToString(), nextHop.NextHopForOutboundRouting.Port);
			sendMessageOptions.ConnectionContext.AddressFamilyHint = new AddressFamilyHint?(UcmaUtils.MapIPAddressFamilyToHint(nextHop.IPAddressFamily));
			this.Diag.Trace("SendNotifyMessageAsync: using IPAddressFamily {0}", new object[]
			{
				nextHop.IPAddressFamily
			});
			sendMessageOptions.ContentDescription = new ContentDescription(contentType, body);
			sendMessageOptions.SetLocalIdentity(sipUri.ToString(), null);
			foreach (PlatformSignalingHeader platformSignalingHeader in headers)
			{
				sendMessageOptions.Headers.Add(new SignalingHeader(platformSignalingHeader.Name, platformSignalingHeader.Value));
			}
			sendMessageOptions.Headers.Add(new SignalingHeader("Event", eventHeader));
			realTimeEndpoint.BeginSendMessage(5, new RealTimeAddress(sipUri.ToString()), sendMessageOptions, new AsyncCallback(this.Endpoint_SendNotifyMessageComplete), sendNotifyMessageUserState);
		}

		// Token: 0x06000386 RID: 902 RVA: 0x0000F9F0 File Offset: 0x0000DBF0
		internal override void Start()
		{
			if (UmServiceGlobals.StartupMode != UMStartupMode.TLS)
			{
				this.StartTcp();
			}
			if (UmServiceGlobals.StartupMode != UMStartupMode.TCP)
			{
				this.StartTls();
			}
		}

		// Token: 0x06000387 RID: 903 RVA: 0x0000FA10 File Offset: 0x0000DC10
		protected override BaseUMCallSession InternalCreateOutboundCallSession(CallContext context, UMCallSessionHandler<OutboundCallDetailsEventArgs> handler, UMVoIPSecurityType security)
		{
			this.Diag.Trace("InternalCreateOutboundCallSession", new object[0]);
			ValidateArgument.NotNull(context, "context");
			ValidateArgument.NotNull(handler, "handler");
			ApplicationEndpoint applicationEndpoint = (security == UMVoIPSecurityType.Unsecured) ? this.tcpEndpoint : this.tlsEndpoint;
			this.Diag.Assert(null != applicationEndpoint, "Request for an invalid endpoint given startup mode.  Security='{0}'", new object[]
			{
				security
			});
			return new UcmaCallSession(new SessionLockSerializer(), applicationEndpoint, context);
		}

		// Token: 0x06000388 RID: 904 RVA: 0x0000FA94 File Offset: 0x0000DC94
		protected override void SipPeerListChanged(object sender, EventArgs args)
		{
			if (this.tlsCollabPlatform != null)
			{
				UcmaUtils.HandleSIPPeerListChanged(this.tlsCollabPlatform, this.Diag);
				if (SipPeerManager.Instance.SIPAccessService != null)
				{
					this.Diag.Trace("UcmaVoipPlatform : Present SIPProxy ={0} {1}: New SIPProxy ={2} {3}", new object[]
					{
						(UcmaVoipPlatform.currentlyPassedSIPProxy == null) ? "null" : UcmaVoipPlatform.currentlyPassedSIPProxy.ToHostPortString(),
						(UcmaVoipPlatform.currentlyPassedSIPProxy == null) ? "null" : UcmaVoipPlatform.currentlyPassedSIPProxy.IPAddressFamily.ToString(),
						SipPeerManager.Instance.SIPAccessService.ToHostPortString(),
						SipPeerManager.Instance.SIPAccessService.IPAddressFamily
					});
					if (UcmaVoipPlatform.currentlyPassedSIPProxy == null || !UcmaVoipPlatform.currentlyPassedSIPProxy.Address.Equals(SipPeerManager.Instance.SIPAccessService.Address) || !UcmaVoipPlatform.currentlyPassedSIPProxy.Port.Equals(SipPeerManager.Instance.SIPAccessService.Port))
					{
						UcmaVoipPlatform.currentlyPassedSIPProxy = SipPeerManager.Instance.SIPAccessService;
						ConnectionContext connectionContext = UcmaUtils.CreateConnectionContext(UcmaVoipPlatform.currentlyPassedSIPProxy.Address.Domain.HostnameString, UcmaVoipPlatform.currentlyPassedSIPProxy.Port);
						connectionContext.AddressFamilyHint = new AddressFamilyHint?(UcmaUtils.MapIPAddressFamilyToHint(UcmaVoipPlatform.currentlyPassedSIPProxy.IPAddressFamily));
						this.tlsEndpoint.SetProxyInformation(connectionContext);
						this.LogSipProxyChosenEvent();
					}
				}
			}
		}

		// Token: 0x06000389 RID: 905 RVA: 0x0000FBF8 File Offset: 0x0000DDF8
		private static void SetMediaPortRange()
		{
			try
			{
				NetworkPortRange networkPortRange = new NetworkPortRange();
				networkPortRange.SetRange(AppConfig.Instance.Service.MinimumRtpPort, AppConfig.Instance.Service.MaximumRtpPort);
				CollaborationPlatform.AudioVideoSettings.SetPortRange(networkPortRange);
				CallIdTracer.TraceDebug(ExTraceGlobals.UCMATracer, null, "Media port range is {0} to {1}", new object[]
				{
					networkPortRange.LocalNetworkPortMin,
					networkPortRange.LocalNetworkPortMax
				});
			}
			catch (ArgumentException ex)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.UCMATracer, null, "Media port range set failed. {0}", new object[]
				{
					ex
				});
			}
		}

		// Token: 0x0600038A RID: 906 RVA: 0x0000FCA0 File Offset: 0x0000DEA0
		private void LogSipProxyChosenEvent()
		{
			if (SipPeerManager.Instance.IsHeuristicBasedSIPAccessService)
			{
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_HeuristicallyChosenSIPProxy, null, new object[]
				{
					UcmaVoipPlatform.currentlyPassedSIPProxy.ToHostPortString(),
					UcmaVoipPlatform.currentlyPassedSIPProxy.IPAddressFamily
				});
				return;
			}
			UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_SIPProxyDetails, null, new object[]
			{
				UcmaVoipPlatform.currentlyPassedSIPProxy.ToHostPortString(),
				UcmaVoipPlatform.currentlyPassedSIPProxy.IPAddressFamily
			});
		}

		// Token: 0x0600038B RID: 907 RVA: 0x0000FD2A File Offset: 0x0000DF2A
		private void StartTcp()
		{
			this.StartTcpPlatform();
			this.StartTcpEndpoint();
			this.RegisterForIncomingCalls(this.tcpEndpoint);
		}

		// Token: 0x0600038C RID: 908 RVA: 0x0000FD44 File Offset: 0x0000DF44
		private void StartTls()
		{
			this.StartTlsPlatform();
			this.StartTlsEndpoint();
			this.RegisterForIncomingCalls(this.tlsEndpoint);
		}

		// Token: 0x0600038D RID: 909 RVA: 0x0000FD60 File Offset: 0x0000DF60
		private void StartTcpPlatform()
		{
			this.Diag.Trace("Starting Tcp platform", new object[0]);
			this.tcpCollabPlatform = UcmaUtils.GetTCPPlatform(base.SipPort, 0, false, this.Diag, null);
			UcmaUtils.StartPlatform(this.tcpCollabPlatform);
			this.Diag.Trace("Tcp platform started!", new object[0]);
		}

		// Token: 0x0600038E RID: 910 RVA: 0x0000FDC0 File Offset: 0x0000DFC0
		private void StartTcpEndpoint()
		{
			this.Diag.Trace("Starting Tcp endpoint", new object[0]);
			this.tcpEndpoint = new ApplicationEndpoint(this.tcpCollabPlatform, this.ConstructEndpointSettings(1));
			this.tcpEndpoint.StateChanged += this.ApplicationEndpoint_StateChanged;
			UcmaUtils.StartEndpoint(this.tcpEndpoint);
			this.Diag.Trace("Tcp endpoint started", new object[0]);
		}

		// Token: 0x0600038F RID: 911 RVA: 0x0000FE34 File Offset: 0x0000E034
		private void StartTlsPlatform()
		{
			int port = (UmServiceGlobals.StartupMode == UMStartupMode.TLS) ? base.SipPort : (base.SipPort + 1);
			this.tlsCollabPlatform = UcmaUtils.GetTLSPlatform(port, 0, false, this.Diag, new EventHandler<ErrorEventArgs>(this.ConnectionManager_IncomingTlsNegotiationFailed), null);
			UcmaUtils.StartPlatform(this.tlsCollabPlatform);
			this.Diag.Trace("Tls platform started!", new object[0]);
		}

		// Token: 0x06000390 RID: 912 RVA: 0x0000FE9C File Offset: 0x0000E09C
		private void StartTlsEndpoint()
		{
			this.Diag.Trace("Starting tls endpoint", new object[0]);
			this.tlsEndpoint = new ApplicationEndpoint(this.tlsCollabPlatform, this.ConstructEndpointSettings(2));
			this.tlsEndpoint.StateChanged += this.ApplicationEndpoint_StateChanged;
			this.tlsEndpoint.InnerEndpoint.MessageReceived += this.InnerEndpoint_MessageReceived;
			UcmaUtils.StartEndpoint(this.tlsEndpoint);
			this.Diag.Trace("Tls endpoint started", new object[0]);
		}

		// Token: 0x06000391 RID: 913 RVA: 0x0000FF2C File Offset: 0x0000E12C
		private ApplicationEndpointSettings ConstructEndpointSettings(SipTransportType transportType)
		{
			string ownerUri = UcmaUtils.GetOwnerUri();
			ApplicationEndpointSettings applicationEndpointSettings;
			switch (transportType)
			{
			case 1:
				applicationEndpointSettings = new ApplicationEndpointSettings(ownerUri);
				break;
			case 2:
			{
				UMSipPeer sipaccessService = SipPeerManager.Instance.SIPAccessService;
				UMSipPeer avauthenticationService = SipPeerManager.Instance.AVAuthenticationService;
				this.Diag.Trace("ConstructEndpointSettings: TLS - SIPAccessService:{0} AVAuthenticationService:{1}", new object[]
				{
					sipaccessService,
					avauthenticationService
				});
				if (sipaccessService == null)
				{
					applicationEndpointSettings = new ApplicationEndpointSettings(ownerUri);
				}
				else
				{
					UcmaVoipPlatform.currentlyPassedSIPProxy = sipaccessService;
					ConnectionContext connectionContext = UcmaUtils.CreateConnectionContext(sipaccessService.Address.Domain.HostnameString, sipaccessService.Port);
					connectionContext.AddressFamilyHint = new AddressFamilyHint?(UcmaUtils.MapIPAddressFamilyToHint(sipaccessService.IPAddressFamily));
					applicationEndpointSettings = new ApplicationEndpointSettings(ownerUri, UcmaVoipPlatform.currentlyPassedSIPProxy.Address.ToString(), sipaccessService.Port);
					this.Diag.Trace("ConstructEndpointSettings: TLS - proxy IPAddressFamily {0}", new object[]
					{
						sipaccessService.IPAddressFamily
					});
					this.LogSipProxyChosenEvent();
				}
				if (avauthenticationService != null)
				{
					applicationEndpointSettings.MediaEdgeAuthenticationService = UcmaUtils.CreateConnectionContext(avauthenticationService.Address.ToString(), avauthenticationService.Port);
					applicationEndpointSettings.MediaEdgeAuthenticationService.AddressFamilyHint = new AddressFamilyHint?(UcmaUtils.MapIPAddressFamilyToHint(avauthenticationService.IPAddressFamily));
					this.Diag.Trace("ConstructEndpointSettings: TLS - MediaEdgeAuthenticationService IPAddressFamily {0}", new object[]
					{
						avauthenticationService.IPAddressFamily
					});
				}
				break;
			}
			default:
				throw new ArgumentException("SipTransportType is incorrect");
			}
			applicationEndpointSettings.IsDefaultRoutingEndpoint = true;
			applicationEndpointSettings.SetEndpointType(1, 0);
			if (CommonConstants.UseDataCenterLogging)
			{
				applicationEndpointSettings.ProvisioningDataQueryDisabled = true;
				applicationEndpointSettings.PublishingQoeMetricsDisabled = false;
			}
			return applicationEndpointSettings;
		}

		// Token: 0x06000392 RID: 914 RVA: 0x000100CC File Offset: 0x0000E2CC
		private void ConnectionManager_IncomingTlsNegotiationFailed(object sender, ErrorEventArgs e)
		{
			TlsFailureException ex = (TlsFailureException)e.GetException();
			this.Diag.Trace("TLS Negotiation Failed: {0}", new object[]
			{
				ex
			});
			Util.AddTlsErrorEventLogEntry(UMEventLogConstants.Tuple_MSSIncomingTLSCallFailure, ex.RemoteCertificate, ex.RemoteEndpoint, ex.LocalEndpoint, UcmaUtils.GetTlsError(ex));
			if (ex.RemoteCertificate != null)
			{
				using (CallContext callContext = new CallContext())
				{
					UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_CallRejected, null, new object[]
					{
						null,
						UcmaUtils.GetTlsError(ex)
					});
					callContext.CallRejectionException = ex;
				}
			}
		}

		// Token: 0x06000393 RID: 915 RVA: 0x00010178 File Offset: 0x0000E378
		private void RegisterForIncomingCalls(ApplicationEndpoint endpoint)
		{
			endpoint.RegisterForIncomingCall<AudioVideoCall>(new IncomingCallDelegate<AudioVideoCall>(this.ApplicationEndpoint_CallReceived));
		}

		// Token: 0x06000394 RID: 916 RVA: 0x000101EC File Offset: 0x0000E3EC
		private void ApplicationEndpoint_CallReceived(object sender, CallReceivedEventArgs<AudioVideoCall> args)
		{
			using (new CallId(args.Call.CallId))
			{
				bool accepted = false;
				CallContext cc = new CallContext();
				UcmaCallSession cs = null;
				Exception ex = null;
				Exception realTimeError = null;
				PlatformSignalingHeader platformSignalingHeader = null;
				this.Diag.Trace("ApplicationEndpoint_CallReceived!", new object[0]);
				try
				{
					if (args.Connection == null)
					{
						this.Diag.Trace("Ignoring ApplicationEndpoint_CallReceived with null connection", new object[0]);
					}
					else if (!base.ShutdownInProgress)
					{
						ex = UcmaUtils.ProcessPlatformRequestAndReportErrors(delegate
						{
							accepted = this.TryAcceptCall((ApplicationEndpoint)sender, args, cc, out cs, out realTimeError);
						}, args.Call.CallId, out platformSignalingHeader);
					}
				}
				finally
				{
					ex = ((realTimeError != null) ? realTimeError : ex);
					if (ex != null && platformSignalingHeader == null)
					{
						platformSignalingHeader = CallRejectedException.RenderDiagnosticHeader(CallEndingReason.TransientError, null, new object[0]);
					}
					this.FinalizeCallReceived(ex, accepted, args, cc, cs, platformSignalingHeader);
				}
			}
		}

		// Token: 0x06000395 RID: 917 RVA: 0x0001036C File Offset: 0x0000E56C
		private bool TryAcceptCall(ApplicationEndpoint endpoint, CallReceivedEventArgs<AudioVideoCall> args, CallContext cc, out UcmaCallSession cs, out Exception error)
		{
			bool flag = false;
			cs = null;
			error = null;
			string text = string.Empty;
			LatencyDetectionContext latencyContext = Util.StartCallLatencyDetection("UcmaVoipPlatform", args.Call.CallId);
			try
			{
				UcmaCallInfo ucmaCallInfo = new UcmaCallInfo(args);
				UMIPGateway gw = null;
				string remoteMatchedFQDN = ucmaCallInfo.RemoteMatchedFQDN;
				text = ucmaCallInfo.RemoteUserAgent;
				if (args.Call != null && args.Call.Conversation != null)
				{
					args.Call.Conversation.Impersonate(ucmaCallInfo.QualityReportUri, null, null);
				}
				if (!string.IsNullOrEmpty(text) && text.IndexOf("ActiveMonitoringClient", StringComparison.OrdinalIgnoreCase) > 0)
				{
					UcmaUtils.SendDiagnosticsInfoCallReceived(args.Call, this.Diag);
					UcmaUtils.CreateDiagnosticsTimers(args.Call, delegate(Timer o)
					{
						cc.AddDiagnosticsTimer(o);
					}, this.Diag);
					UcmaUtils.AddCallDelayFaultInjectionInTestMode(2525375805U);
				}
				if (!SipPeerManager.Instance.IsLocalDiagnosticCall(ucmaCallInfo.RemotePeer, ucmaCallInfo.RemoteHeaders) && !RouterUtils.ShouldAccept(ucmaCallInfo, Util.GetTenantGuid(ucmaCallInfo.RequestUri), out remoteMatchedFQDN, out gw))
				{
					throw CallRejectedException.Create(Strings.CallFromInvalidGateway(ucmaCallInfo.RemotePeer.ToString()), CommonConstants.UseDataCenterCallRouting ? CallEndingReason.UnAuthorizedGatewayDatacenter : CallEndingReason.UnAuthorizedGateway, null, new object[0]);
				}
				ucmaCallInfo.RemoteMatchedFQDN = remoteMatchedFQDN;
				UcmaVoipPlatform.InitializeInboundCallContext(ucmaCallInfo, gw, cc);
				cs = new UcmaCallSession(new SessionLockSerializer(), endpoint, ucmaCallInfo, cc, args.Call);
				base.RegisterAndHandleCall(cs, ucmaCallInfo.RemoteHeaders);
				flag = true;
			}
			catch (RealTimeException ex)
			{
				error = ex;
			}
			catch (InvalidOperationException ex2)
			{
				error = ex2;
			}
			finally
			{
				Util.EndCallLatencyDetection(latencyContext, args.Call.CallId, cc.CallReceivedTime, text, cc.UmSubscriberData, flag);
			}
			return flag;
		}

		// Token: 0x06000396 RID: 918 RVA: 0x000105B4 File Offset: 0x0000E7B4
		private void FinalizeCallReceived(Exception error, bool accepted, CallReceivedEventArgs<AudioVideoCall> args, CallContext cc, UcmaCallSession cs, PlatformSignalingHeader diagnosticHeader)
		{
			this.Diag.Trace("FinalizeCallReceived.  Error='{0}', Accepted='{1}'", new object[]
			{
				error,
				accepted
			});
			if (!accepted)
			{
				this.LogCallRejected(error, args, cc, cs, diagnosticHeader);
				if (cc != null)
				{
					cc.Dispose();
				}
				if (cs != null)
				{
					cs.Dispose();
				}
				CallDeclineOptions options = new CallDeclineOptions(403);
				options.Headers.Add(new SignalingHeader(diagnosticHeader.Name, diagnosticHeader.Value));
				this.Diag.Trace("FinalizeCallReceived. Call rejected. DiagnosticHeader:{0}", new object[]
				{
					diagnosticHeader.Value
				});
				UcmaUtils.CatchRealtimeErrors(delegate
				{
					args.Call.Decline(options);
				}, this.Diag);
			}
		}

		// Token: 0x06000397 RID: 919 RVA: 0x0001069C File Offset: 0x0000E89C
		private void LogCallRejected(Exception error, CallReceivedEventArgs<AudioVideoCall> args, CallContext cc, UcmaCallSession cs, PlatformSignalingHeader diagnosticHeader)
		{
			bool flag = false;
			ConversationState state = args.Call.Conversation.State;
			if ((error is InvalidOperationException && cs != null && cs.IsInvalidOperationExplainable) || error is OperationTimeoutException || (error is InvalidOperationException && (state == 7 || state == 8)))
			{
				flag = true;
			}
			if (!flag)
			{
				string text = diagnosticHeader.Value + " " + CommonUtil.ToEventLogString(error);
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_CallRejected, null, new object[]
				{
					args.Call.CallId,
					text
				});
				if (cc != null)
				{
					if (cc.OfferResult == OfferResult.None)
					{
						cc.OfferResult = OfferResult.Reject;
					}
					cc.CallRejectionException = error;
					return;
				}
			}
			else
			{
				this.Diag.Trace("LogCallRejected: ignoring error.", new object[0]);
			}
		}

		// Token: 0x06000398 RID: 920 RVA: 0x00010764 File Offset: 0x0000E964
		private void ApplicationEndpoint_StateChanged(object sender, LocalEndpointStateChangedEventArgs e)
		{
			this.Diag.Trace("Endpoint changed: {0} to {1}", new object[]
			{
				e.PreviousState,
				e.State
			});
		}

		// Token: 0x06000399 RID: 921 RVA: 0x000107BC File Offset: 0x0000E9BC
		private void InnerEndpoint_MessageReceived(object sender, MessageReceivedEventArgs e)
		{
			UcmaUtils.HandleMessageReceived(this.Diag, e, delegate(InfoMessage.PlatformMessageReceivedEventArgs args)
			{
				if (!args.IsOptions)
				{
					base.Fire<InfoMessage.PlatformMessageReceivedEventArgs>(this.OnMessageReceived, args);
				}
			});
		}

		// Token: 0x0600039A RID: 922 RVA: 0x000107D8 File Offset: 0x0000E9D8
		private int GetErrorResponseParameters(Exception error, PlatformSignalingHeader diag, out SignalingHeader[] responseHeaders)
		{
			responseHeaders = new SignalingHeader[]
			{
				new SignalingHeader(diag.Name, diag.Value)
			};
			CallRejectedException ex = error as CallRejectedException;
			if (ex == null || ex.Reason.ErrorCode != CallEndingReason.PipelineFull.ErrorCode)
			{
				return 500;
			}
			return 503;
		}

		// Token: 0x0600039B RID: 923 RVA: 0x00010874 File Offset: 0x0000EA74
		private void Endpoint_SendNotifyMessageComplete(IAsyncResult r)
		{
			UcmaVoipPlatform.SendNotifyMessageUserState state = (UcmaVoipPlatform.SendNotifyMessageUserState)r.AsyncState;
			int sipResponseCode = 0;
			string sipResponseText = null;
			Exception ex = UcmaUtils.CatchRealtimeErrors(delegate
			{
				SipResponseData sipResponseData = state.Endpoint.EndSendMessage(r);
				sipResponseCode = sipResponseData.ResponseCode;
				sipResponseText = sipResponseData.ResponseText;
			}, this.Diag);
			FailureResponseException ex2 = ex as FailureResponseException;
			if (ex2 != null && ex2.ResponseData != null)
			{
				sipResponseCode = ex2.ResponseData.ResponseCode;
				sipResponseText = ex2.ResponseData.ResponseText;
			}
			SendNotifyMessageCompletedEventArgs args = new SendNotifyMessageCompletedEventArgs
			{
				Error = ex,
				ResponseCode = sipResponseCode,
				ResponseReason = sipResponseText,
				UserState = state.State
			};
			base.Fire<SendNotifyMessageCompletedEventArgs>(this.OnSendNotifyMessageCompleted, args);
		}

		// Token: 0x04000111 RID: 273
		private static UMSipPeer currentlyPassedSIPProxy;

		// Token: 0x04000112 RID: 274
		private CollaborationPlatform tcpCollabPlatform;

		// Token: 0x04000113 RID: 275
		private ApplicationEndpoint tcpEndpoint;

		// Token: 0x04000114 RID: 276
		private CollaborationPlatform tlsCollabPlatform;

		// Token: 0x04000115 RID: 277
		private ApplicationEndpoint tlsEndpoint;

		// Token: 0x0200004D RID: 77
		private class SendNotifyMessageUserState
		{
			// Token: 0x170000B4 RID: 180
			// (get) Token: 0x0600039E RID: 926 RVA: 0x00010957 File Offset: 0x0000EB57
			// (set) Token: 0x0600039F RID: 927 RVA: 0x0001095F File Offset: 0x0000EB5F
			public RealTimeEndpoint Endpoint { get; set; }

			// Token: 0x170000B5 RID: 181
			// (get) Token: 0x060003A0 RID: 928 RVA: 0x00010968 File Offset: 0x0000EB68
			// (set) Token: 0x060003A1 RID: 929 RVA: 0x00010970 File Offset: 0x0000EB70
			public object State { get; set; }
		}

		// Token: 0x0200004E RID: 78
		private class TlsAuthorizationContext
		{
			// Token: 0x060003A3 RID: 931 RVA: 0x00010981 File Offset: 0x0000EB81
			private TlsAuthorizationContext(string fqdn, UMIPGateway gw)
			{
				this.Fqdn = fqdn;
				this.Gateway = gw;
			}

			// Token: 0x060003A4 RID: 932 RVA: 0x00010997 File Offset: 0x0000EB97
			public static UcmaVoipPlatform.TlsAuthorizationContext CreateForUMIPGateway(string matchedFqdn, UMIPGateway gateway)
			{
				return new UcmaVoipPlatform.TlsAuthorizationContext(matchedFqdn, gateway);
			}

			// Token: 0x170000B6 RID: 182
			// (get) Token: 0x060003A5 RID: 933 RVA: 0x000109A0 File Offset: 0x0000EBA0
			// (set) Token: 0x060003A6 RID: 934 RVA: 0x000109A8 File Offset: 0x0000EBA8
			public UMIPGateway Gateway { get; private set; }

			// Token: 0x170000B7 RID: 183
			// (get) Token: 0x060003A7 RID: 935 RVA: 0x000109B1 File Offset: 0x0000EBB1
			// (set) Token: 0x060003A8 RID: 936 RVA: 0x000109B9 File Offset: 0x0000EBB9
			public string Fqdn { get; private set; }
		}
	}
}
