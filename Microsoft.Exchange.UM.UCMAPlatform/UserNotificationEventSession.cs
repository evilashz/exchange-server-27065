using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.UM.TroubleshootingTool.Shared;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCore;
using Microsoft.Exchange.UM.UMCore.Exceptions;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Rtc.Signaling;

namespace Microsoft.Exchange.UM.UcmaPlatform
{
	// Token: 0x02000060 RID: 96
	internal class UserNotificationEventSession : IOfferAnswer
	{
		// Token: 0x06000423 RID: 1059 RVA: 0x00012DD4 File Offset: 0x00010FD4
		internal UserNotificationEventSession(DiagnosticHelper diagnostics, SignalingSession session, UserNotificationEventHandler handler)
		{
			this.diagnostics = diagnostics;
			this.session = session;
			this.handler = handler;
			this.sessionId = session.CallId;
			this.session.OfferAnswerNegotiation = this;
			this.session.StateChanged += this.HandleStateChanged;
			this.session.MessageReceived += this.HandleMessageReceived;
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000424 RID: 1060 RVA: 0x00012E58 File Offset: 0x00011058
		internal TimeSpan IdleTime
		{
			get
			{
				return ExDateTime.UtcNow.Subtract(this.lastInfoReceivedDate);
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000425 RID: 1061 RVA: 0x00012E78 File Offset: 0x00011078
		internal string Id
		{
			get
			{
				return this.sessionId;
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000426 RID: 1062 RVA: 0x00012E80 File Offset: 0x00011080
		internal bool IsActive
		{
			get
			{
				return this.session != null;
			}
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x00012F6C File Offset: 0x0001116C
		internal void Start(SessionReceivedEventArgs args)
		{
			this.TraceDebug("Start()", new object[0]);
			UserNotificationEventSession.SIPStatus status = UserNotificationEventSession.SIPStatus.Forbidden;
			lock (this)
			{
				Exception ex = UcmaUtils.CatchRealtimeErrors(delegate
				{
					if (this.ValidateSession(args))
					{
						status = this.ProcessRemoteSdp(args.MediaDescription);
					}
					if (status.Success)
					{
						this.mediaDescription = this.GenerateSessionDescription();
						this.BeginParticipate();
						return;
					}
					object obj;
					if (this.session != null)
					{
						obj = this.session.Endpoint;
						this.RejectSession(status, UserNotificationEventSession.SIPStatus.InvalidSDPWarning);
					}
					else
					{
						obj = string.Empty;
					}
					UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_EventNotifSessionInvalidFormat, null, new object[]
					{
						obj,
						this.Id
					});
				}, this.diagnostics);
				if (ex != null)
				{
					this.TraceError("UserNotificationEventSession.Start: {0}", new object[]
					{
						ex
					});
					this.LogSignalingErrorEvent(ex);
				}
			}
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x00013018 File Offset: 0x00011218
		internal void Terminate()
		{
			lock (this)
			{
				if (this.session != null)
				{
					try
					{
						if (this.session.State != null && this.session.State != 4)
						{
							this.session.BeginTerminate(null, null);
						}
						else
						{
							this.TraceDebug("Terminate():Session({0}).State={1} - waiting for StateChange event", new object[]
							{
								this.Id,
								this.session.State
							});
						}
						goto IL_C2;
					}
					catch (RealTimeException ex)
					{
						this.TraceError("Terminate: {0}", new object[]
						{
							ex
						});
						goto IL_C2;
					}
					catch (InvalidOperationException ex2)
					{
						this.TraceError("Terminate: {0}", new object[]
						{
							ex2
						});
						goto IL_C2;
					}
				}
				this.TraceDebug("Terminate: Session was already terminated", new object[0]);
				IL_C2:;
			}
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x0001311C File Offset: 0x0001131C
		private static string GetHeaderValue(IEnumerable<SignalingHeader> headerList, string name)
		{
			foreach (SignalingHeader signalingHeader in headerList)
			{
				if (string.Compare(signalingHeader.Name, name, StringComparison.OrdinalIgnoreCase) == 0)
				{
					return signalingHeader.GetValue();
				}
			}
			return string.Empty;
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x0001317C File Offset: 0x0001137C
		private static bool IsTLSCall(SessionReceivedEventArgs args)
		{
			string protocolName = args.Session.Connection.ProtocolName;
			return 0 == string.Compare(protocolName, "TLS", StringComparison.InvariantCultureIgnoreCase);
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x000131AC File Offset: 0x000113AC
		private void LogSignalingErrorEvent(Exception error)
		{
			RealTimeEndpoint realTimeEndpoint = (this.session != null) ? this.session.Endpoint : null;
			UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_EventNotifSessionSignalingError, null, new object[]
			{
				realTimeEndpoint,
				this.Id,
				error.Message
			});
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x000131FF File Offset: 0x000113FF
		private void BeginParticipate()
		{
			this.TraceDebug("BeginParticipate()", new object[0]);
			this.session.BeginParticipate(null, null);
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x00013220 File Offset: 0x00011420
		private void RejectSession(UserNotificationEventSession.SIPStatus status, UserNotificationEventSession.SIPStatus warning)
		{
			this.TraceDebug("RejectSession - Status={0}, Warning={1}", new object[]
			{
				status,
				warning
			});
			PlatformSignalingHeader platformSignalingHeader = CallRejectedException.RenderDiagnosticHeader(CallEndingReason.TransientError, status.Text, new object[0]);
			List<SignalingHeader> list = new List<SignalingHeader>();
			string text = string.Format(CultureInfo.InvariantCulture, "{0} {1} \"{2}\"", new object[]
			{
				warning.Code,
				Utils.GetLocalHostName(),
				warning.Text
			});
			list.Add(new SignalingHeader("Warning", text));
			list.Add(new SignalingHeader(platformSignalingHeader.Name, platformSignalingHeader.Value));
			this.session.TerminateWithRejection(status.Code, status.Text, list);
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x000132E4 File Offset: 0x000114E4
		private void HandleStateChanged(object sender, SignalingStateChangedEventArgs e)
		{
			this.TraceDebug("HandleStateChanged: PreviousState={0}, NewState={1}, Reason={2}", new object[]
			{
				e.PreviousState,
				e.State,
				e.Reason
			});
			if (e.State == 4 || e.State == null)
			{
				this.HandleSessionTerminated();
			}
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x000134A4 File Offset: 0x000116A4
		private void HandleMessageReceived(object sender, MessageReceivedEventArgs e)
		{
			this.TraceDebug("HandleMessageReceived - {0}", new object[]
			{
				e.MessageType
			});
			RealTimeConnection connection = e.Connection;
			if (connection == null)
			{
				this.TraceDebug("Connection is null. Ignoring request.", new object[0]);
				return;
			}
			lock (this)
			{
				this.lastInfoReceivedDate = ExDateTime.UtcNow;
			}
			int? responseCode = null;
			PlatformSignalingHeader diagnosticHeader = null;
			UserNotificationEventContext context = new UserNotificationEventContext();
			using (new CallId(this.Id))
			{
				Exception innerError = null;
				Exception ex = UcmaUtils.CatchRealtimeErrors(delegate
				{
					if (VariantConfiguration.InvariantNoFlightingSnapshot.UM.SipInfoNotifications.Enabled)
					{
						innerError = UcmaUtils.ProcessPlatformRequestAndReportErrors(delegate
						{
							this.handler(new UcmaCallInfo(e, connection), e.GetBody(), context);
						}, this.Id, out diagnosticHeader);
						if (innerError == null)
						{
							diagnosticHeader = null;
							return;
						}
					}
					else
					{
						this.TraceError("User notification events via SIP INFO are not supported in hosted exchange", new object[0]);
						responseCode = new int?(UserNotificationEventSession.SIPStatus.Forbidden.Code);
						innerError = CallRejectedException.Create(Strings.InvalidRequest, CallEndingReason.UnsupportedRequest, "User notification events via SIP INFO are not supported in hosted Exchange", new object[0]);
					}
				}, this.diagnostics);
				Exception ex2 = innerError ?? ex;
				bool flag2 = false;
				CallRejectedException ex3 = ex2 as CallRejectedException;
				if (ex3 != null)
				{
					flag2 = (ex3.Reason == CallEndingReason.NotificationNotSupportedForLegacyUser || ex3.Reason == CallEndingReason.MailboxIsNotUMEnabled);
				}
				if (ex2 != null && !flag2)
				{
					this.TraceError("HandleMessageReceived - Encountered error {0}", new object[]
					{
						ex2
					});
					if (ex2 is UMGrayException && (ex2 as UMGrayException).InnerException is ArgumentException)
					{
						responseCode = new int?(UserNotificationEventSession.SIPStatus.BadRequest.Code);
					}
					responseCode = new int?(responseCode ?? UserNotificationEventSession.SIPStatus.InternalServerError.Code);
					diagnosticHeader = (diagnosticHeader ?? CallRejectedException.RenderDiagnosticHeader(CallEndingReason.TransientError, null, null));
				}
				else
				{
					responseCode = new int?(UserNotificationEventSession.SIPStatus.OK.Code);
				}
				List<SignalingHeader> headers = new List<SignalingHeader>(1);
				if (diagnosticHeader != null)
				{
					this.TraceError("HandleMessageReceived - diagnostic header = {0}", new object[]
					{
						diagnosticHeader
					});
					headers.Add(new SignalingHeader(diagnosticHeader.Name, diagnosticHeader.Value));
				}
				UcmaUtils.CatchRealtimeErrors(delegate
				{
					e.SendResponse(responseCode.Value, null, null, headers);
				}, this.diagnostics);
				if (!flag2)
				{
					bool flag3 = responseCode == UserNotificationEventSession.SIPStatus.OK.Code;
					if (!flag3)
					{
						UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_UserNotificationFailed, null, new object[]
						{
							CommonUtil.ToEventLogString(e.RequestData.FromHeader.Uri),
							CommonUtil.ToEventLogString(e.RequestData.ToHeader.Uri),
							CommonUtil.ToEventLogString(responseCode),
							CommonUtil.ToEventLogString(diagnosticHeader),
							CommonUtil.ToEventLogString(context.User),
							CommonUtil.ToEventLogString((context.Backend != null) ? context.Backend.Fqdn : string.Empty)
						});
					}
					Util.SetCounter(CallRouterAvailabilityCounters.RecentMissedCallNotificationProxyFailed, (long)UserNotificationEventSession.recentMissedCallNotificationsProxyFailed.Update(flag3));
				}
			}
			this.TraceDebug("HandleMessageReceived - responseCode = {0}", new object[]
			{
				responseCode
			});
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x0001388C File Offset: 0x00011A8C
		private void HandleSessionTerminated()
		{
			this.TraceDebug("HandleSessionTerminated()", new object[0]);
			lock (this)
			{
				if (this.session != null)
				{
					try
					{
						try
						{
							this.TraceDebug("HandleSessionTerminated - unwiring event handlers", new object[0]);
							this.session.OfferAnswerNegotiation = null;
							this.session.StateChanged -= this.HandleStateChanged;
							this.session.MessageReceived -= this.HandleMessageReceived;
						}
						catch (RealTimeException ex)
						{
							this.TraceError("HandleSessionTerminated: {0}", new object[]
							{
								ex
							});
						}
						catch (InvalidOperationException ex2)
						{
							this.TraceError("HandleSessionTerminated: {0}", new object[]
							{
								ex2
							});
						}
						goto IL_CA;
					}
					finally
					{
						this.session = null;
					}
				}
				this.TraceDebug("HandleSessionTerminated: Session was already terminated", new object[0]);
				IL_CA:;
			}
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x000139A4 File Offset: 0x00011BA4
		private bool ValidateSession(SessionReceivedEventArgs args)
		{
			if (args.Session.Connection == null)
			{
				this.TraceError("ValidateSession: Connection is unavailable.", new object[0]);
				return false;
			}
			if (!UserNotificationEventSession.IsTLSCall(args))
			{
				this.TraceError("ValidateSession: Session is not a TLS call!", new object[0]);
				return false;
			}
			string headerValue = UserNotificationEventSession.GetHeaderValue(args.RequestData.SignalingHeaders, "FROM");
			return 0 < headerValue.IndexOf("sip:A410AA79-D874-4e56-9B46-709BDD0EB850", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x00013A14 File Offset: 0x00011C14
		private ContentDescription GenerateSessionDescription()
		{
			Sdp<SdpGlobalDescription, SdpMediaDescription> sdp = new Sdp<SdpGlobalDescription, SdpMediaDescription>();
			sdp.GlobalDescription.Origin.Version = 0L;
			sdp.GlobalDescription.Origin.Connection.HostName = this.session.Endpoint.ConnectionManager.LocalHostName;
			sdp.GlobalDescription.Origin.SessionId = "0";
			SdpMediaDescription sdpMediaDescription = new SdpMediaDescription("application");
			sdpMediaDescription.Port = 9;
			sdpMediaDescription.TransportProtocol = "SIP";
			sdpMediaDescription.Formats = "*";
			sdpMediaDescription.Attributes.Add(new SdpAttribute("recvonly", null));
			sdpMediaDescription.Attributes.Add(new SdpAttribute("accept-types", "application/ms-rtc-usernotification+xml"));
			sdpMediaDescription.Attributes.Add(new SdpAttribute("ms-rtc-accept-eventtemplates", "RtcDefault"));
			sdp.MediaDescriptions.Add(sdpMediaDescription);
			return new ContentDescription(Constants.OCS.SDPContentType, sdp.GetBytes());
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x00013B08 File Offset: 0x00011D08
		private UserNotificationEventSession.SIPStatus ProcessRemoteSdp(ContentDescription description)
		{
			if (description == null)
			{
				return UserNotificationEventSession.SIPStatus.BadRequest;
			}
			if (!description.ContentType.Equals(Constants.OCS.SDPContentType))
			{
				return UserNotificationEventSession.SIPStatus.UnsupportedMediaType;
			}
			Sdp<SdpGlobalDescription, SdpMediaDescription> sdp = new Sdp<SdpGlobalDescription, SdpMediaDescription>();
			this.TraceDebug("ProcessRemoteSdp():\r\n{0}", new object[]
			{
				Encoding.ASCII.GetString(description.GetBody())
			});
			if (!sdp.TryParse(description.GetBody()))
			{
				return UserNotificationEventSession.SIPStatus.NotAcceptableHere;
			}
			if (sdp.MediaDescriptions.Count != 1)
			{
				return UserNotificationEventSession.SIPStatus.NotAcceptableHere;
			}
			SdpMediaDescription sdpMediaDescription = sdp.MediaDescriptions[0];
			if (string.Compare(sdpMediaDescription.MediaName, "application", StringComparison.InvariantCultureIgnoreCase) != 0)
			{
				return UserNotificationEventSession.SIPStatus.NotAcceptableHere;
			}
			if (!sdpMediaDescription.TransportProtocol.Equals("SIP", StringComparison.InvariantCultureIgnoreCase))
			{
				return UserNotificationEventSession.SIPStatus.NotAcceptableHere;
			}
			foreach (SdpAttribute sdpAttribute in sdpMediaDescription.Attributes)
			{
				if (string.Compare(sdpAttribute.Name, "accept-types", StringComparison.InvariantCultureIgnoreCase) == 0 && sdpAttribute.Value != null && sdpAttribute.Value.IndexOf("application/ms-rtc-usernotification+xml", StringComparison.InvariantCultureIgnoreCase) >= 0)
				{
					return UserNotificationEventSession.SIPStatus.OK;
				}
			}
			return UserNotificationEventSession.SIPStatus.NotAcceptableHere;
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x00013C48 File Offset: 0x00011E48
		public ContentDescription GetAnswer(object sender, ContentDescription remoteSdp)
		{
			string text = (this.mediaDescription != null) ? Encoding.ASCII.GetString(this.mediaDescription.GetBody()) : null;
			this.TraceDebug("GetMediaAnswer called -> returning:\r\n{0}", new object[]
			{
				text
			});
			return this.mediaDescription;
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x00013C93 File Offset: 0x00011E93
		public ContentDescription GetOffer(object sender)
		{
			this.TraceDebug("GetMediaOffer called -> returning null", new object[0]);
			return null;
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x00013CA7 File Offset: 0x00011EA7
		public void SetAnswer(object sender, ContentDescription descriptionResponse)
		{
			this.TraceDebug("SetMediaAnswer called -> ignore", new object[0]);
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x00013CBC File Offset: 0x00011EBC
		public void HandleOfferInReInvite(object sender, OfferInReInviteEventArgs e)
		{
			this.TraceDebug("HandleOfferInReInvite called -> sending original SDP", new object[0]);
			try
			{
				e.BeginAccept(null, null, null);
			}
			catch (RealTimeException ex)
			{
				this.TraceError("HandleRenegotiationReceived: {0}", new object[]
				{
					ex
				});
			}
			catch (InvalidOperationException ex2)
			{
				this.TraceError("HandleRenegotiationReceived: {0}", new object[]
				{
					ex2
				});
			}
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x00013D38 File Offset: 0x00011F38
		public void HandleOfferInInviteResponse(object sender, OfferInInviteResponseEventArgs e)
		{
			this.TraceDebug("HandleOfferInInviteResponse called -> ignoring", new object[0]);
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x00013D4B File Offset: 0x00011F4B
		private void TraceDebug(string format, params object[] args)
		{
			this.diagnostics.Trace(format, args);
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x00013D5A File Offset: 0x00011F5A
		private void TraceError(string format, params object[] args)
		{
			this.diagnostics.TraceError(format, args);
		}

		// Token: 0x04000144 RID: 324
		private static PercentageBooleanSlidingCounter recentMissedCallNotificationsProxyFailed = PercentageBooleanSlidingCounter.CreateFailureCounter(1000, TimeSpan.FromHours(1.0));

		// Token: 0x04000145 RID: 325
		private readonly string sessionId = string.Empty;

		// Token: 0x04000146 RID: 326
		private readonly UserNotificationEventHandler handler;

		// Token: 0x04000147 RID: 327
		private readonly DiagnosticHelper diagnostics;

		// Token: 0x04000148 RID: 328
		private SignalingSession session;

		// Token: 0x04000149 RID: 329
		private ExDateTime lastInfoReceivedDate = ExDateTime.UtcNow;

		// Token: 0x0400014A RID: 330
		private ContentDescription mediaDescription;

		// Token: 0x02000061 RID: 97
		internal class SIPStatus
		{
			// Token: 0x0600043C RID: 1084 RVA: 0x00013D88 File Offset: 0x00011F88
			private SIPStatus(int code, string text, bool success)
			{
				this.Code = code;
				this.Text = text;
				this.Success = success;
			}

			// Token: 0x0600043D RID: 1085 RVA: 0x00013DA8 File Offset: 0x00011FA8
			public override string ToString()
			{
				return string.Format(CultureInfo.InvariantCulture, "{0} {1}", new object[]
				{
					this.Code,
					this.Text
				});
			}

			// Token: 0x0400014B RID: 331
			internal static readonly UserNotificationEventSession.SIPStatus OK = new UserNotificationEventSession.SIPStatus(200, "OK", true);

			// Token: 0x0400014C RID: 332
			internal static readonly UserNotificationEventSession.SIPStatus Forbidden = new UserNotificationEventSession.SIPStatus(403, "Forbidden", false);

			// Token: 0x0400014D RID: 333
			internal static readonly UserNotificationEventSession.SIPStatus BadRequest = new UserNotificationEventSession.SIPStatus(400, "Bad Request", false);

			// Token: 0x0400014E RID: 334
			internal static readonly UserNotificationEventSession.SIPStatus InvalidSDPWarning = new UserNotificationEventSession.SIPStatus(305, "Incompatible Media Format", false);

			// Token: 0x0400014F RID: 335
			internal static readonly UserNotificationEventSession.SIPStatus NotAcceptableHere = new UserNotificationEventSession.SIPStatus(488, "Not Acceptable Here", false);

			// Token: 0x04000150 RID: 336
			internal static readonly UserNotificationEventSession.SIPStatus InternalServerError = new UserNotificationEventSession.SIPStatus(500, "Internal Server Error", false);

			// Token: 0x04000151 RID: 337
			internal static readonly UserNotificationEventSession.SIPStatus UnsupportedMediaType = new UserNotificationEventSession.SIPStatus(415, "Unsupported Media Type", false);

			// Token: 0x04000152 RID: 338
			internal readonly int Code;

			// Token: 0x04000153 RID: 339
			internal readonly bool Success;

			// Token: 0x04000154 RID: 340
			internal readonly string Text;
		}
	}
}
