using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Audio;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Office.Datacenter.WorkerTaskFramework;
using Microsoft.Rtc.Collaboration;
using Microsoft.Rtc.Collaboration.AudioVideo;
using Microsoft.Rtc.Signaling;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.UM.Probes
{
	// Token: 0x020004BD RID: 1213
	public sealed class VoiceObject : IDisposable
	{
		// Token: 0x1700063C RID: 1596
		// (get) Token: 0x06001E24 RID: 7716 RVA: 0x000B68C2 File Offset: 0x000B4AC2
		// (set) Token: 0x06001E25 RID: 7717 RVA: 0x000B68CA File Offset: 0x000B4ACA
		private bool AudioReceived { get; set; }

		// Token: 0x1700063D RID: 1597
		// (get) Token: 0x06001E26 RID: 7718 RVA: 0x000B68D3 File Offset: 0x000B4AD3
		// (set) Token: 0x06001E27 RID: 7719 RVA: 0x000B68DB File Offset: 0x000B4ADB
		internal DiagnosticsTracker DiagnosticsTracker { get; private set; }

		// Token: 0x06001E28 RID: 7720 RVA: 0x000B68E4 File Offset: 0x000B4AE4
		public VoiceObject(TracingContext traceContext, string remoteFQDN, bool isLyncPeer, X509Certificate2 cert, SipTransportType sipTransportType, MediaProtocol mediaType) : this(traceContext, remoteFQDN, isLyncPeer, cert, sipTransportType, mediaType, false)
		{
		}

		// Token: 0x06001E29 RID: 7721 RVA: 0x000B68F8 File Offset: 0x000B4AF8
		public VoiceObject(TracingContext traceContext, string remoteFQDN, bool isLyncPeer, X509Certificate2 cert, SipTransportType sipTransportType, MediaProtocol mediaType, bool waitForMedia)
		{
			this.isDisposed = false;
			this.traceContext = traceContext;
			this.DiagnosticsTracker = new DiagnosticsTracker();
			this.DiagnosticsTracker.TrackLocalDiagnostics(15900, "VoiceObject created.", new string[0]);
			this.isLyncPeer = isLyncPeer;
			this.mediaType = mediaType;
			this.waitForMedia = waitForMedia;
			this.AudioReceived = false;
			this.sipTransportType = sipTransportType;
			if (this.sipTransportType == SipTransportType.TCP)
			{
				VoiceObject.InitializeTCPCollaborationPlatform();
				VoiceObject.InitializeTCPEndPoint();
			}
			else
			{
				VoiceObject.InitializeTlsCollaborationPlatform(cert);
				this.AddTrustedDomainToTlsCollaborationPlatform(remoteFQDN);
				VoiceObject.InitializeTLSEndPoint();
			}
			this.DiagnosticsTracker.TrackLocalDiagnostics(15900, "VoiceObject initialized.", new string[0]);
		}

		// Token: 0x06001E2A RID: 7722 RVA: 0x000B69AC File Offset: 0x000B4BAC
		~VoiceObject()
		{
			this.Dispose(false);
		}

		// Token: 0x1700063E RID: 1598
		// (get) Token: 0x06001E2B RID: 7723 RVA: 0x000B69DC File Offset: 0x000B4BDC
		// (set) Token: 0x06001E2C RID: 7724 RVA: 0x000B69E3 File Offset: 0x000B4BE3
		public static X509Certificate2 CertUsedByTlsCollaborationPlatform { get; private set; }

		// Token: 0x1700063F RID: 1599
		// (get) Token: 0x06001E2D RID: 7725 RVA: 0x000B69EB File Offset: 0x000B4BEB
		// (set) Token: 0x06001E2E RID: 7726 RVA: 0x000B69F3 File Offset: 0x000B4BF3
		public VoiceObject.VoErrorInfo VoErrorInformation { get; set; }

		// Token: 0x17000640 RID: 1600
		// (get) Token: 0x06001E2F RID: 7727 RVA: 0x000B69FC File Offset: 0x000B4BFC
		// (set) Token: 0x06001E30 RID: 7728 RVA: 0x000B6A04 File Offset: 0x000B4C04
		public string CallId { get; set; }

		// Token: 0x17000641 RID: 1601
		// (get) Token: 0x06001E31 RID: 7729 RVA: 0x000B6A0D File Offset: 0x000B4C0D
		// (set) Token: 0x06001E32 RID: 7730 RVA: 0x000B6A15 File Offset: 0x000B4C15
		public string ConnectionEndpoint { get; set; }

		// Token: 0x17000642 RID: 1602
		// (get) Token: 0x06001E33 RID: 7731 RVA: 0x000B6A1E File Offset: 0x000B4C1E
		// (set) Token: 0x06001E34 RID: 7732 RVA: 0x000B6A25 File Offset: 0x000B4C25
		private static CollaborationPlatform TcpCollaborationPlatformInstance { get; set; }

		// Token: 0x17000643 RID: 1603
		// (get) Token: 0x06001E35 RID: 7733 RVA: 0x000B6A2D File Offset: 0x000B4C2D
		// (set) Token: 0x06001E36 RID: 7734 RVA: 0x000B6A34 File Offset: 0x000B4C34
		private static CollaborationPlatform TlsCollaborationPlatformInstance { get; set; }

		// Token: 0x17000644 RID: 1604
		// (get) Token: 0x06001E37 RID: 7735 RVA: 0x000B6A3C File Offset: 0x000B4C3C
		// (set) Token: 0x06001E38 RID: 7736 RVA: 0x000B6A43 File Offset: 0x000B4C43
		private static LocalEndpoint TcpEndpoint { get; set; }

		// Token: 0x17000645 RID: 1605
		// (get) Token: 0x06001E39 RID: 7737 RVA: 0x000B6A4B File Offset: 0x000B4C4B
		// (set) Token: 0x06001E3A RID: 7738 RVA: 0x000B6A52 File Offset: 0x000B4C52
		private static LocalEndpoint TlsEndpoint { get; set; }

		// Token: 0x17000646 RID: 1606
		// (get) Token: 0x06001E3B RID: 7739 RVA: 0x000B6A5A File Offset: 0x000B4C5A
		private AudioVideoCall Call
		{
			get
			{
				return this.call;
			}
		}

		// Token: 0x06001E3C RID: 7740 RVA: 0x000B6A64 File Offset: 0x000B4C64
		public void CallUM(string remoteHostFqdn, int remoteSipPort, string umHuntgroupNumber, string callerId, DiversionType diversionType, string diversionValue, string organizationName)
		{
			IList<VoiceObject.HeaderInfo> list = null;
			Conversation conversation = new Conversation(this.GetEndPoint());
			AudioVideoCall audioVideoCall = new AudioVideoCall(conversation);
			this.RegisterCall(audioVideoCall);
			if (this.isLyncPeer)
			{
				list = new List<VoiceObject.HeaderInfo>();
				list.Add(new VoiceObject.HeaderInfo("Route", string.Format(CultureInfo.InvariantCulture, "<sip:{0};{1}={2};lr>;{3}", new object[]
				{
					umHuntgroupNumber,
					"transport",
					this.sipTransportType,
					"ms-edge-route"
				})));
				list.Add(new VoiceObject.HeaderInfo("ms-split-domain-info", "ms-traffic-type=SplitIntra"));
			}
			this.InternalCallUM(remoteHostFqdn, remoteSipPort, umHuntgroupNumber, callerId, diversionValue, organizationName, list);
		}

		// Token: 0x06001E3D RID: 7741 RVA: 0x000B6B0C File Offset: 0x000B4D0C
		public void DisconnectCall()
		{
			try
			{
				this.WriteTrace("Attempting to disconnect the call...", new object[0]);
				if (this.Call != null)
				{
					Conversation conversation = this.Call.Conversation;
					CallTerminateOptions callTerminateOptions = new CallTerminateOptions();
					if (this.isLyncPeer)
					{
						SignalingHeader item = new SignalingHeader("ms-split-domain-info", "ms-traffic-type=SplitIntra");
						callTerminateOptions.Headers.Add(item);
					}
					this.Call.EndTerminate(this.Call.BeginTerminate(callTerminateOptions, null, null));
					if (conversation != null)
					{
						conversation.EndTerminate(conversation.BeginTerminate(null, null));
					}
					this.WriteTrace("Call Disconnected", new object[0]);
					this.call = null;
				}
				else
				{
					this.WriteTrace("No call to disconnect (call == null)", new object[0]);
				}
			}
			catch (RealTimeException ex)
			{
				this.WriteTrace("RealTimeException occured {0}", new object[]
				{
					ex
				});
			}
			catch (InvalidOperationException ex2)
			{
				this.WriteTrace("InvalidOperationException occured {0}", new object[]
				{
					ex2
				});
			}
		}

		// Token: 0x06001E3E RID: 7742 RVA: 0x000B6C68 File Offset: 0x000B4E68
		public bool SendSipOptionPing(string remoteHostFqdn, string certificateSubjectName, int remoteSipPort)
		{
			SipResponseData responseData = null;
			int num = 0;
			RealTimeAddress realTimeAddressForRemoteEndpoint;
			if (this.sipTransportType == SipTransportType.TCP)
			{
				realTimeAddressForRemoteEndpoint = new RealTimeAddress(string.Format(CultureInfo.InvariantCulture, "sip:{0}:{1};{2}={3}", new object[]
				{
					remoteHostFqdn,
					remoteSipPort,
					"transport",
					this.sipTransportType
				}));
			}
			else
			{
				realTimeAddressForRemoteEndpoint = new RealTimeAddress(string.Format(CultureInfo.InvariantCulture, "sip:{0}:{1};{2}={3};{4}={5}", new object[]
				{
					certificateSubjectName,
					remoteSipPort,
					"ms-fe",
					remoteHostFqdn,
					"transport",
					this.sipTransportType
				}));
			}
			this.RunAndCatchRealTimeAndInvalidOperationException(delegate
			{
				responseData = this.GetEndPoint().InnerEndpoint.EndSendMessage(this.GetEndPoint().InnerEndpoint.BeginSendMessage(2, realTimeAddressForRemoteEndpoint, null, null, null, null));
			});
			if (responseData != null)
			{
				num = responseData.ResponseCode;
			}
			return num == 200;
		}

		// Token: 0x06001E3F RID: 7743 RVA: 0x000B6D59 File Offset: 0x000B4F59
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001E40 RID: 7744 RVA: 0x000B6D68 File Offset: 0x000B4F68
		private static void InitializeTlsCollaborationPlatform(X509Certificate2 cert)
		{
			if (VoiceObject.TlsCollaborationPlatformInstance == null)
			{
				lock (VoiceObject.syncLock)
				{
					if (VoiceObject.TlsCollaborationPlatformInstance == null)
					{
						ServerPlatformSettings serverPlatformSettings = new ServerPlatformSettings("ActiveMonitoringClient", Utils.GetLocalHostIPv4().ToString(), 0, null, cert.Issuer, cert.GetSerialNumber());
						VoiceObject.TlsCollaborationPlatformInstance = VoiceObject.InitializeCollaborationPlatform(serverPlatformSettings);
						VoiceObject.CertUsedByTlsCollaborationPlatform = cert;
					}
				}
			}
		}

		// Token: 0x06001E41 RID: 7745 RVA: 0x000B6DE4 File Offset: 0x000B4FE4
		private static void InitializeTCPCollaborationPlatform()
		{
			if (VoiceObject.TcpCollaborationPlatformInstance == null)
			{
				lock (VoiceObject.syncLock)
				{
					if (VoiceObject.TcpCollaborationPlatformInstance == null)
					{
						ServerPlatformSettings serverPlatformSettings = new ServerPlatformSettings("ActiveMonitoringClient", Utils.GetLocalHostIPv4().ToString(), 0, null);
						VoiceObject.TcpCollaborationPlatformInstance = VoiceObject.InitializeCollaborationPlatform(serverPlatformSettings);
					}
				}
			}
		}

		// Token: 0x06001E42 RID: 7746 RVA: 0x000B6E50 File Offset: 0x000B5050
		private static CollaborationPlatform InitializeCollaborationPlatform(ServerPlatformSettings serverPlatformSettings)
		{
			CollaborationPlatform collaborationPlatform = new CollaborationPlatform(serverPlatformSettings);
			collaborationPlatform.EndStartup(collaborationPlatform.BeginStartup(null, null));
			return collaborationPlatform;
		}

		// Token: 0x06001E43 RID: 7747 RVA: 0x000B6E74 File Offset: 0x000B5074
		private static void InitializeTCPEndPoint()
		{
			if (VoiceObject.TcpEndpoint == null)
			{
				lock (VoiceObject.syncLock)
				{
					if (VoiceObject.TcpEndpoint == null)
					{
						VoiceObject.TcpEndpoint = VoiceObject.InitializeEndPoint(VoiceObject.TcpCollaborationPlatformInstance);
					}
				}
			}
		}

		// Token: 0x06001E44 RID: 7748 RVA: 0x000B6ECC File Offset: 0x000B50CC
		private static void InitializeTLSEndPoint()
		{
			if (VoiceObject.TlsEndpoint == null)
			{
				lock (VoiceObject.syncLock)
				{
					if (VoiceObject.TlsEndpoint == null)
					{
						VoiceObject.TlsEndpoint = VoiceObject.InitializeEndPoint(VoiceObject.TlsCollaborationPlatformInstance);
					}
				}
			}
		}

		// Token: 0x06001E45 RID: 7749 RVA: 0x000B6F24 File Offset: 0x000B5124
		private static LocalEndpoint InitializeEndPoint(CollaborationPlatform collaborationPlatform)
		{
			string text = string.Format(CultureInfo.InvariantCulture, "sip:{0}@{1}", new object[]
			{
				"Callmanager",
				Utils.GetLocalHostIPv4().ToString()
			});
			ApplicationEndpointSettings applicationEndpointSettings = new ApplicationEndpointSettings(text);
			applicationEndpointSettings.IsDefaultRoutingEndpoint = true;
			applicationEndpointSettings.SetEndpointType(1, 0);
			applicationEndpointSettings.ProvisioningDataQueryDisabled = true;
			applicationEndpointSettings.PublishingQoeMetricsDisabled = true;
			LocalEndpoint localEndpoint = new ApplicationEndpoint(collaborationPlatform, applicationEndpointSettings);
			localEndpoint.EndEstablish(localEndpoint.BeginEstablish(null, null));
			return localEndpoint;
		}

		// Token: 0x06001E46 RID: 7750 RVA: 0x000B6F9C File Offset: 0x000B519C
		private void AddTrustedDomainToTlsCollaborationPlatform(string remoteHostFqdn)
		{
			TrustedDomainMode trustedDomainMode = this.isLyncPeer ? 0 : 1;
			TrustedDomain trustedDomain = new TrustedDomain(remoteHostFqdn, trustedDomainMode);
			lock (VoiceObject.syncLock)
			{
				VoiceObject.TlsCollaborationPlatformInstance.AddTrustedDomain(trustedDomain);
			}
		}

		// Token: 0x06001E47 RID: 7751 RVA: 0x000B6FF8 File Offset: 0x000B51F8
		private LocalEndpoint GetEndPoint()
		{
			if (this.sipTransportType == SipTransportType.TCP)
			{
				return VoiceObject.TcpEndpoint;
			}
			return VoiceObject.TlsEndpoint;
		}

		// Token: 0x06001E48 RID: 7752 RVA: 0x000B700D File Offset: 0x000B520D
		private void Dispose(bool disposing)
		{
			if (!this.isDisposed && disposing)
			{
				this.ClearFlowObjects();
				this.ClearCall();
				this.isDisposed = true;
			}
		}

		// Token: 0x06001E49 RID: 7753 RVA: 0x000B702D File Offset: 0x000B522D
		private void ClearCall()
		{
			if (this.Call != null)
			{
				this.Call.AudioVideoFlowConfigurationRequested -= this.OnAudioVideoFlowConfigurationRequested;
				this.Call.StateChanged -= this.OnStateChanged;
				this.DisconnectCall();
			}
		}

		// Token: 0x06001E4A RID: 7754 RVA: 0x000B70B4 File Offset: 0x000B52B4
		private void InternalCallUM(string remoteHostFqdn, int remoteSipPort, string umHuntgroupNumber, string callerId, string diversionValue, string organizationName, IList<VoiceObject.HeaderInfo> otherHeaders)
		{
			this.callOptions = new CallEstablishOptions();
			string remoteSipURI;
			string text;
			string text3;
			if (this.isLyncPeer)
			{
				text = string.Format(CultureInfo.InvariantCulture, "<sip:{0}> ;reason=no-answer", new object[]
				{
					diversionValue
				});
				string text2 = string.Format(CultureInfo.InvariantCulture, "{0}={1}", new object[]
				{
					"ms-organization",
					organizationName
				});
				text3 = string.Format(CultureInfo.InvariantCulture, "sip:{0}:{1};{2}={3}", new object[]
				{
					callerId,
					remoteSipPort,
					"transport",
					this.sipTransportType
				});
				remoteSipURI = string.Format(CultureInfo.InvariantCulture, "sip:{0}:{1};{2}", new object[]
				{
					diversionValue,
					remoteSipPort,
					text2
				});
				this.callOptions.ConnectionContext = new ConnectionContext(remoteHostFqdn, remoteSipPort);
				this.callOptions.ConnectionContext.AddressFamilyHint = new AddressFamilyHint?(0);
			}
			else
			{
				text = string.Format("<tel:{0}> ;reason=no-answer", diversionValue);
				text3 = this.GetEndPoint().InnerEndpoint.Uri;
				text3 = text3.Replace("Callmanager", callerId);
				remoteSipURI = string.Format(CultureInfo.InvariantCulture, "sip:{0}@{1}:{2};{3}={4}", new object[]
				{
					umHuntgroupNumber,
					remoteHostFqdn,
					remoteSipPort,
					"transport",
					this.sipTransportType
				});
			}
			this.SetOptionalHeaders(otherHeaders);
			SignalingHeader item = new SignalingHeader("Diversion", text);
			this.callOptions.Headers.Add(item);
			this.WriteTrace("Diversion Header: {0}", new object[]
			{
				text
			});
			this.WriteTrace("local SipURI = {0}", new object[]
			{
				text3
			});
			this.WriteTrace("remote SipURI = {0}", new object[]
			{
				remoteSipURI
			});
			foreach (SignalingHeader signalingHeader in this.callOptions.Headers)
			{
				this.WriteTrace("header {0} = {1}", new object[]
				{
					signalingHeader.Name,
					signalingHeader.GetValue()
				});
			}
			this.Call.Conversation.Impersonate(text3, null, string.Empty);
			this.DiagnosticsTracker.TrackLocalDiagnostics(15901, "VoiceObject internal call UM.", new string[0]);
			this.RunAndCatchRealTimeAndInvalidOperationException(delegate
			{
				this.Call.EndEstablish(this.Call.BeginEstablish(remoteSipURI, this.callOptions, null, null));
			});
			if (this.waitForMedia)
			{
				this.RunAndCatchRealTimeAndInvalidOperationException(delegate
				{
					this.WaitForMedia();
				});
			}
			this.DisconnectCall();
			this.DiagnosticsTracker.TrackLocalDiagnostics(15905, "VoiceObject call disconnected", new string[0]);
		}

		// Token: 0x06001E4B RID: 7755 RVA: 0x000B73C0 File Offset: 0x000B55C0
		private void WaitForMedia()
		{
			Thread.Sleep(TimeSpan.FromSeconds(30.0));
			if (!this.AudioReceived)
			{
				throw new MediaException("Audio not received");
			}
		}

		// Token: 0x06001E4C RID: 7756 RVA: 0x000B73E8 File Offset: 0x000B55E8
		private void SetOptionalHeaders(IEnumerable<VoiceObject.HeaderInfo> headers)
		{
			if (headers != null)
			{
				foreach (VoiceObject.HeaderInfo headerInfo in headers)
				{
					this.callOptions.Headers.Add(new SignalingHeader(headerInfo.HeaderName, headerInfo.HeaderValue));
				}
			}
		}

		// Token: 0x06001E4D RID: 7757 RVA: 0x000B7450 File Offset: 0x000B5650
		private void RegisterCall(AudioVideoCall call)
		{
			this.call = call;
			this.Call.AudioVideoFlowConfigurationRequested += this.OnAudioVideoFlowConfigurationRequested;
			this.Call.StateChanged += this.OnStateChanged;
			this.Call.ProvisionalResponseReceived += this.OnProvisionalResponseReceived;
		}

		// Token: 0x06001E4E RID: 7758 RVA: 0x000B753C File Offset: 0x000B573C
		private void OnAudioVideoFlowConfigurationRequested(object sender, AudioVideoFlowConfigurationRequestedEventArgs e)
		{
			this.RunAndCatchRealTimeAndInvalidOperationException(delegate
			{
				this.WriteTrace("configuring SDP so that it supports {0}", new object[]
				{
					this.mediaType
				});
				this.RegisterFlow(e.Flow);
				AudioVideoFlowTemplate audioVideoFlowTemplate = new AudioVideoFlowTemplate(this.flow);
				audioVideoFlowTemplate.EncryptionPolicy = ((this.mediaType == MediaProtocol.SRTP) ? 3 : 1);
				this.flow.Initialize(audioVideoFlowTemplate);
			});
		}

		// Token: 0x06001E4F RID: 7759 RVA: 0x000B756F File Offset: 0x000B576F
		private void RegisterFlow(AudioVideoFlow flow)
		{
			this.flow = flow;
			this.flow.StateChanged += this.OnFlowStateChanged;
		}

		// Token: 0x06001E50 RID: 7760 RVA: 0x000B758F File Offset: 0x000B578F
		private void ClearFlowObjects()
		{
			if (this.flow != null)
			{
				this.flow.StateChanged -= this.OnFlowStateChanged;
				this.flow = null;
			}
		}

		// Token: 0x06001E51 RID: 7761 RVA: 0x000B7644 File Offset: 0x000B5844
		private void OnFlowStateChanged(object sender, MediaFlowStateChangedEventArgs e)
		{
			this.RunAndCatchRealTimeAndInvalidOperationException(delegate
			{
				this.WriteTrace("Media flow state: {0} ==> {1}", new object[]
				{
					e.PreviousState,
					e.State
				});
				if (this.waitForMedia && this.flow.State == 1)
				{
					this.AudioReceived = true;
					this.HandleAudio();
				}
			});
		}

		// Token: 0x06001E52 RID: 7762 RVA: 0x000B7678 File Offset: 0x000B5878
		private void HandleAudio()
		{
			SpeechRecognitionConnector speechRecognitionConnector = new SpeechRecognitionConnector();
			speechRecognitionConnector.AttachFlow(this.flow);
			SpeechRecognitionStream speechRecognitionStream = speechRecognitionConnector.Start();
			byte[] array = new byte[16000];
			bool flag = true;
			int numofBytes;
			while ((numofBytes = speechRecognitionStream.Read(array, 0, array.Length)) != 0)
			{
				double num = AudioNormalizer.CalcEnergyRms(array, numofBytes);
				if (num > 0.088)
				{
					flag = false;
					this.DiagnosticsTracker.TrackLocalDiagnostics(15906, "Received audio in call", new string[0]);
					break;
				}
			}
			if (flag)
			{
				this.WriteTrace("Only noise in the received audio", new object[0]);
				throw new MediaException("Only noise found in media");
			}
		}

		// Token: 0x06001E53 RID: 7763 RVA: 0x000B7964 File Offset: 0x000B5B64
		private void OnStateChanged(object sender, CallStateChangedEventArgs e)
		{
			this.RunAndCatchRealTimeAndInvalidOperationException(delegate
			{
				if (string.IsNullOrEmpty(this.CallId))
				{
					if (e.MessageData != null && !string.IsNullOrEmpty(e.MessageData.CallId))
					{
						this.CallId = e.MessageData.CallId;
					}
					else if (!string.IsNullOrEmpty(this.Call.CallId))
					{
						this.CallId = this.Call.CallId;
					}
					if (!string.IsNullOrEmpty(this.CallId))
					{
						this.WriteTrace("CallId for this Call: {0}", new object[]
						{
							this.CallId
						});
					}
				}
				if (string.IsNullOrEmpty(this.ConnectionEndpoint))
				{
					object propertyByName = this.GetPropertyByName("PrimarySession", this.Call);
					object propertyByName2 = this.GetPropertyByName("SignalingSession", propertyByName);
					object propertyByName3 = this.GetPropertyByName("LastKnownConnection", propertyByName2);
					object propertyByName4 = this.GetPropertyByName("SipStackConnection", propertyByName3);
					object propertyByName5 = this.GetPropertyByName("DestinationEndPoint", propertyByName4);
					if (propertyByName5 != null)
					{
						this.ConnectionEndpoint = propertyByName5.ToString();
					}
				}
				this.WriteTrace("Call state: {0} ==> {1}  Reason: {2}", new object[]
				{
					e.PreviousState,
					e.State,
					e.TransitionReason
				});
				if (e.TransitionReason == 3)
				{
					this.DiagnosticsTracker.TrackLocalDiagnostics(15902, "VoiceObject call establishing.", new string[0]);
					return;
				}
				if (e.TransitionReason == 5)
				{
					this.DiagnosticsTracker.TrackLocalDiagnostics(15903, "VoiceObject call established.", new string[0]);
					return;
				}
				if (e.TransitionReason == 6)
				{
					this.DiagnosticsTracker.TrackLocalDiagnostics(15904, "VoiceObject call establish failed.", new string[0]);
				}
			});
		}

		// Token: 0x06001E54 RID: 7764 RVA: 0x000B79D4 File Offset: 0x000B5BD4
		private void OnProvisionalResponseReceived(object sender, CallProvisionalResponseReceivedEventArgs e)
		{
			this.RunAndCatchRealTimeAndInvalidOperationException(delegate
			{
				SipResponseData responseData = e.ResponseData;
				if (responseData.ResponseCode == 101)
				{
					this.ProcessMsDiagnostics(responseData.SignalingHeaders);
				}
			});
		}

		// Token: 0x06001E55 RID: 7765 RVA: 0x000B7A08 File Offset: 0x000B5C08
		private void ProcessMsDiagnostics(IEnumerable<SignalingHeader> headers)
		{
			foreach (SignalingHeader signalingHeader in headers)
			{
				if (string.Equals(signalingHeader.Name, "ms-diagnostics-public", StringComparison.InvariantCultureIgnoreCase) || string.Equals(signalingHeader.Name, "ms-diagnostics", StringComparison.InvariantCultureIgnoreCase))
				{
					this.DiagnosticsTracker.TrackDiagnostics(signalingHeader.GetValue());
					break;
				}
			}
		}

		// Token: 0x06001E56 RID: 7766 RVA: 0x000B7A84 File Offset: 0x000B5C84
		private string GetHostNameFromFQDN(string fqdn)
		{
			int num = fqdn.IndexOf('.');
			if (num != -1)
			{
				fqdn = fqdn.Substring(0, num);
			}
			return fqdn;
		}

		// Token: 0x06001E57 RID: 7767 RVA: 0x000B7AAC File Offset: 0x000B5CAC
		private void RunAndCatchRealTimeAndInvalidOperationException(Action action)
		{
			try
			{
				action();
			}
			catch (Exception e)
			{
				this.TryExtractFailureResponseException(e);
			}
		}

		// Token: 0x06001E58 RID: 7768 RVA: 0x000B7ADC File Offset: 0x000B5CDC
		private void TryExtractFailureResponseException(Exception e)
		{
			if (e is RealTimeException)
			{
				SipResponseData sipResponseData = null;
				if (e is FailureResponseException)
				{
					sipResponseData = (e as FailureResponseException).ResponseData;
				}
				else if (e.InnerException is FailureResponseException)
				{
					FailureResponseException ex = e.InnerException as FailureResponseException;
					sipResponseData = ex.ResponseData;
				}
				this.WriteTrace("Get the InnerException error message {0}", new object[]
				{
					e.InnerException
				});
				if (sipResponseData != null)
				{
					IList<VoiceObject.HeaderInfo> list = new List<VoiceObject.HeaderInfo>();
					this.WriteTrace("Get responseData {0}, {1}", new object[]
					{
						sipResponseData.ResponseCode,
						sipResponseData.ResponseText
					});
					StringBuilder stringBuilder = new StringBuilder();
					foreach (SignalingHeader signalingHeader in sipResponseData.SignalingHeaders)
					{
						stringBuilder.AppendLine(signalingHeader.Name + " --> " + signalingHeader.GetValue());
						list.Add(new VoiceObject.HeaderInfo(signalingHeader.Name, signalingHeader.GetValue()));
					}
					this.WriteTrace("ResponseData Header \n{0}", new object[]
					{
						stringBuilder
					});
					this.ProcessMsDiagnostics(sipResponseData.SignalingHeaders);
					this.VoErrorInformation = new VoiceObject.VoErrorInfo(sipResponseData.ResponseCode, sipResponseData.ResponseText, list, e);
					return;
				}
				this.WriteTrace("No response message accept!", new object[0]);
				this.VoErrorInformation = new VoiceObject.VoErrorInfo(e);
				return;
			}
			else
			{
				if (e is InvalidOperationException)
				{
					this.WriteTrace("InvalidOperationException has occurred", new object[]
					{
						e
					});
					this.VoErrorInformation = new VoiceObject.VoErrorInfo(e);
					return;
				}
				if (e is MediaException)
				{
					this.WriteTrace("Media Exception occured", new object[0]);
					this.VoErrorInformation = new VoiceObject.VoErrorInfo(e);
					return;
				}
				throw e;
			}
		}

		// Token: 0x06001E59 RID: 7769 RVA: 0x000B7CB8 File Offset: 0x000B5EB8
		private void WriteTrace(string format, params object[] objs)
		{
			WTFDiagnostics.TraceInformation(ExTraceGlobals.UnifiedMessagingTracer, this.traceContext, string.Format(format, objs), null, "WriteTrace", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\UM\\VoiceObject.cs", 1104);
		}

		// Token: 0x06001E5A RID: 7770 RVA: 0x000B7CE4 File Offset: 0x000B5EE4
		private object GetPropertyByName(string propertyName, object instance)
		{
			if (instance != null)
			{
				try
				{
					Type type = instance.GetType();
					return type.InvokeMember(propertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.GetProperty, null, instance, null);
				}
				catch (MissingMemberException ex)
				{
					this.WriteTrace("UCMA property changed. {0}", new object[]
					{
						ex
					});
					return null;
				}
			}
			return null;
		}

		// Token: 0x040015A7 RID: 5543
		private const string ApplicationUserAgent = "ActiveMonitoringClient";

		// Token: 0x040015A8 RID: 5544
		private const string Identity = "Callmanager";

		// Token: 0x040015A9 RID: 5545
		private const string Diversion = "Diversion";

		// Token: 0x040015AA RID: 5546
		private const string Route = "Route";

		// Token: 0x040015AB RID: 5547
		private const string Transport = "transport";

		// Token: 0x040015AC RID: 5548
		private const string MsEdgeRoute = "ms-edge-route";

		// Token: 0x040015AD RID: 5549
		private const string MsSplitDomainInfo = "ms-split-domain-info";

		// Token: 0x040015AE RID: 5550
		private const string SplitIntra = "ms-traffic-type=SplitIntra";

		// Token: 0x040015AF RID: 5551
		private const string MsOrganization = "ms-organization";

		// Token: 0x040015B0 RID: 5552
		private readonly bool isLyncPeer;

		// Token: 0x040015B1 RID: 5553
		private static object syncLock = new object();

		// Token: 0x040015B2 RID: 5554
		private MediaProtocol mediaType;

		// Token: 0x040015B3 RID: 5555
		private readonly bool waitForMedia;

		// Token: 0x040015B4 RID: 5556
		private SipTransportType sipTransportType;

		// Token: 0x040015B5 RID: 5557
		private bool isDisposed;

		// Token: 0x040015B6 RID: 5558
		private AudioVideoCall call;

		// Token: 0x040015B7 RID: 5559
		private AudioVideoFlow flow;

		// Token: 0x040015B8 RID: 5560
		private CallEstablishOptions callOptions;

		// Token: 0x040015B9 RID: 5561
		private TracingContext traceContext;

		// Token: 0x020004BE RID: 1214
		public class HeaderInfo
		{
			// Token: 0x06001E5D RID: 7773 RVA: 0x000B7D50 File Offset: 0x000B5F50
			public HeaderInfo(string name, string value)
			{
				this.HeaderName = name;
				this.HeaderValue = value;
			}

			// Token: 0x17000647 RID: 1607
			// (get) Token: 0x06001E5E RID: 7774 RVA: 0x000B7D66 File Offset: 0x000B5F66
			// (set) Token: 0x06001E5F RID: 7775 RVA: 0x000B7D6E File Offset: 0x000B5F6E
			public string HeaderName { get; private set; }

			// Token: 0x17000648 RID: 1608
			// (get) Token: 0x06001E60 RID: 7776 RVA: 0x000B7D77 File Offset: 0x000B5F77
			// (set) Token: 0x06001E61 RID: 7777 RVA: 0x000B7D7F File Offset: 0x000B5F7F
			public string HeaderValue { get; private set; }
		}

		// Token: 0x020004BF RID: 1215
		public class VoErrorInfo
		{
			// Token: 0x06001E62 RID: 7778 RVA: 0x000B7D88 File Offset: 0x000B5F88
			public VoErrorInfo(int code, string message, IList<VoiceObject.HeaderInfo> headers, Exception exception)
			{
				this.Code = code;
				this.Message = message;
				this.Headers = headers;
				this.Exception = exception;
			}

			// Token: 0x06001E63 RID: 7779 RVA: 0x000B7DAD File Offset: 0x000B5FAD
			public VoErrorInfo(Exception ex) : this(-1, null, new List<VoiceObject.HeaderInfo>(), ex)
			{
			}

			// Token: 0x17000649 RID: 1609
			// (get) Token: 0x06001E64 RID: 7780 RVA: 0x000B7DBD File Offset: 0x000B5FBD
			// (set) Token: 0x06001E65 RID: 7781 RVA: 0x000B7DC5 File Offset: 0x000B5FC5
			public int Code { get; private set; }

			// Token: 0x1700064A RID: 1610
			// (get) Token: 0x06001E66 RID: 7782 RVA: 0x000B7DCE File Offset: 0x000B5FCE
			// (set) Token: 0x06001E67 RID: 7783 RVA: 0x000B7DD6 File Offset: 0x000B5FD6
			public string Message { get; private set; }

			// Token: 0x1700064B RID: 1611
			// (get) Token: 0x06001E68 RID: 7784 RVA: 0x000B7DDF File Offset: 0x000B5FDF
			// (set) Token: 0x06001E69 RID: 7785 RVA: 0x000B7DE7 File Offset: 0x000B5FE7
			public IList<VoiceObject.HeaderInfo> Headers { get; private set; }

			// Token: 0x1700064C RID: 1612
			// (get) Token: 0x06001E6A RID: 7786 RVA: 0x000B7DF0 File Offset: 0x000B5FF0
			// (set) Token: 0x06001E6B RID: 7787 RVA: 0x000B7DF8 File Offset: 0x000B5FF8
			public Exception Exception { get; private set; }

			// Token: 0x06001E6C RID: 7788 RVA: 0x000B7E04 File Offset: 0x000B6004
			public string GetMsDiagnostics()
			{
				string result = null;
				if (this.Headers != null)
				{
					foreach (VoiceObject.HeaderInfo headerInfo in this.Headers)
					{
						if (string.Equals(headerInfo.HeaderName, "ms-diagnostics", StringComparison.InvariantCultureIgnoreCase) || string.Equals(headerInfo.HeaderName, "ms-diagnostics-public", StringComparison.InvariantCultureIgnoreCase))
						{
							result = headerInfo.HeaderValue;
							break;
						}
					}
				}
				return result;
			}
		}
	}
}
