using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCore;
using Microsoft.Rtc.Collaboration;
using Microsoft.Rtc.Collaboration.AudioVideo;
using Microsoft.Rtc.Signaling;

namespace Microsoft.Exchange.UM.UcmaPlatform
{
	// Token: 0x0200003C RID: 60
	internal class UcmaCallInfo : PlatformCallInfo
	{
		// Token: 0x0600028F RID: 655 RVA: 0x0000AFD6 File Offset: 0x000091D6
		public UcmaCallInfo(CallMessageData d, Conversation conv, IPAddress remotePeer)
		{
			this.provider = new UcmaCallInfo.OutboundCallInfoProvider(d, conv, remotePeer);
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0000AFEC File Offset: 0x000091EC
		public UcmaCallInfo(CallReceivedEventArgs<AudioVideoCall> e)
		{
			this.provider = new UcmaCallInfo.InboundCallInfoProvider(e);
			this.remoteMatchedFqdn = this.provider.RemoteMatchedFQDN;
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0000B011 File Offset: 0x00009211
		public UcmaCallInfo(MessageReceivedEventArgs e, RealTimeConnection connection)
		{
			this.provider = new UcmaCallInfo.ServiceRequestProvider(e, connection);
			this.RemoteMatchedFQDN = this.provider.RemoteMatchedFQDN;
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000292 RID: 658 RVA: 0x0000B037 File Offset: 0x00009237
		// (set) Token: 0x06000293 RID: 659 RVA: 0x0000B03F File Offset: 0x0000923F
		public override string RemoteMatchedFQDN
		{
			get
			{
				return this.remoteMatchedFqdn;
			}
			set
			{
				this.remoteMatchedFqdn = value;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000294 RID: 660 RVA: 0x0000B048 File Offset: 0x00009248
		public override X509Certificate RemoteCertificate
		{
			get
			{
				return this.provider.RemoteCertificate;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000295 RID: 661 RVA: 0x0000B055 File Offset: 0x00009255
		public override IPAddress RemotePeer
		{
			get
			{
				return this.provider.RemotePeer;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000296 RID: 662 RVA: 0x0000B062 File Offset: 0x00009262
		public override string CallId
		{
			get
			{
				return this.provider.CallId;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000297 RID: 663 RVA: 0x0000B06F File Offset: 0x0000926F
		public override PlatformTelephonyAddress CalledParty
		{
			get
			{
				if (this.lazyCalledParty == null)
				{
					this.lazyCalledParty = UcmaCallInfo.AddressFromFromToHeader(this.provider.ToHeader);
				}
				return this.lazyCalledParty;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000298 RID: 664 RVA: 0x0000B095 File Offset: 0x00009295
		public override PlatformTelephonyAddress CallingParty
		{
			get
			{
				if (this.lazyCallingParty == null)
				{
					this.lazyCallingParty = UcmaCallInfo.AddressFromFromToHeader(this.provider.FromHeader);
				}
				return this.lazyCallingParty;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000299 RID: 665 RVA: 0x0000B0BC File Offset: 0x000092BC
		public override ReadOnlyCollection<PlatformDiversionInfo> DiversionInfo
		{
			get
			{
				if (this.lazyDiversionInfo == null)
				{
					List<PlatformDiversionInfo> diversionInfo = this.provider.DiversionInfo;
					this.lazyDiversionInfo = new ReadOnlyCollection<PlatformDiversionInfo>(diversionInfo);
				}
				return this.lazyDiversionInfo;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600029A RID: 666 RVA: 0x0000B0EF File Offset: 0x000092EF
		public override string FromTag
		{
			get
			{
				return UcmaCallInfo.TagFromFromToHeader(this.provider.FromHeader);
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x0600029B RID: 667 RVA: 0x0000B110 File Offset: 0x00009310
		public override ReadOnlyCollection<PlatformSignalingHeader> RemoteHeaders
		{
			get
			{
				if (this.lazyRemoteHeaders == null)
				{
					IEnumerable<SignalingHeader> signalingHeaders = this.provider.SignalingHeaders;
					if (signalingHeaders != null)
					{
						List<PlatformSignalingHeader> list = signalingHeaders.ConvertAll((SignalingHeader x) => UcmaSignalingHeader.FromSignalingHeader(x, "INVITE"));
						this.lazyRemoteHeaders = new ReadOnlyCollection<PlatformSignalingHeader>(list);
					}
				}
				return this.lazyRemoteHeaders;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600029C RID: 668 RVA: 0x0000B16A File Offset: 0x0000936A
		public override string RemoteUserAgent
		{
			get
			{
				return this.provider.UserAgent;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600029D RID: 669 RVA: 0x0000B178 File Offset: 0x00009378
		public override PlatformSipUri RequestUri
		{
			get
			{
				if (this.requestUri == null)
				{
					string text = this.provider.RequestUri;
					if (!string.IsNullOrEmpty(text))
					{
						this.requestUri = new UcmaSipUri(text);
					}
				}
				return this.requestUri;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600029E RID: 670 RVA: 0x0000B1B3 File Offset: 0x000093B3
		public override string ToTag
		{
			get
			{
				return UcmaCallInfo.TagFromFromToHeader(this.provider.ToHeader);
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600029F RID: 671 RVA: 0x0000B1C5 File Offset: 0x000093C5
		public string QualityReportUri
		{
			get
			{
				return this.provider.QualityReportUri;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060002A0 RID: 672 RVA: 0x0000B1D2 File Offset: 0x000093D2
		public string RemoteContactUri
		{
			get
			{
				return this.provider.RemoteContactUri;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060002A1 RID: 673 RVA: 0x0000B1DF File Offset: 0x000093DF
		public override string ApplicationAor
		{
			get
			{
				return this.provider.ApplicationAor;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060002A2 RID: 674 RVA: 0x0000B1EC File Offset: 0x000093EC
		public override bool IsInbound
		{
			get
			{
				return this.provider is UcmaCallInfo.InboundCallInfoProvider;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060002A3 RID: 675 RVA: 0x0000B1FC File Offset: 0x000093FC
		public override bool IsServiceRequest
		{
			get
			{
				return this.provider is UcmaCallInfo.ServiceRequestProvider;
			}
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0000B20C File Offset: 0x0000940C
		private static PlatformTelephonyAddress AddressFromFromToHeader(FromToHeader h)
		{
			PlatformTelephonyAddress result = null;
			if (h != null)
			{
				result = new PlatformTelephonyAddress(h.DisplayName, new UcmaSipUri(h.Uri));
			}
			return result;
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x0000B238 File Offset: 0x00009438
		private static string TagFromFromToHeader(FromToHeader h)
		{
			string result = string.Empty;
			if (h != null)
			{
				result = h.Tag;
			}
			return result;
		}

		// Token: 0x040000E1 RID: 225
		private UcmaCallInfo.CallInfoProvider provider;

		// Token: 0x040000E2 RID: 226
		private PlatformTelephonyAddress lazyCalledParty;

		// Token: 0x040000E3 RID: 227
		private PlatformTelephonyAddress lazyCallingParty;

		// Token: 0x040000E4 RID: 228
		private ReadOnlyCollection<PlatformDiversionInfo> lazyDiversionInfo;

		// Token: 0x040000E5 RID: 229
		private ReadOnlyCollection<PlatformSignalingHeader> lazyRemoteHeaders;

		// Token: 0x040000E6 RID: 230
		private PlatformSipUri requestUri;

		// Token: 0x040000E7 RID: 231
		private string remoteMatchedFqdn;

		// Token: 0x0200003D RID: 61
		private abstract class CallInfoProvider
		{
			// Token: 0x17000070 RID: 112
			// (get) Token: 0x060002A7 RID: 679
			public abstract string CallId { get; }

			// Token: 0x17000071 RID: 113
			// (get) Token: 0x060002A8 RID: 680
			public abstract ConversationParticipant LocalParticipant { get; }

			// Token: 0x17000072 RID: 114
			// (get) Token: 0x060002A9 RID: 681
			public abstract ConversationParticipant RemoteParticipant { get; }

			// Token: 0x17000073 RID: 115
			// (get) Token: 0x060002AA RID: 682
			public abstract IEnumerable<SignalingHeader> SignalingHeaders { get; }

			// Token: 0x17000074 RID: 116
			// (get) Token: 0x060002AB RID: 683
			public abstract FromToHeader FromHeader { get; }

			// Token: 0x17000075 RID: 117
			// (get) Token: 0x060002AC RID: 684
			public abstract FromToHeader ToHeader { get; }

			// Token: 0x17000076 RID: 118
			// (get) Token: 0x060002AD RID: 685
			public abstract string UserAgent { get; }

			// Token: 0x17000077 RID: 119
			// (get) Token: 0x060002AE RID: 686
			public abstract string RequestUri { get; }

			// Token: 0x17000078 RID: 120
			// (get) Token: 0x060002AF RID: 687
			public abstract List<PlatformDiversionInfo> DiversionInfo { get; }

			// Token: 0x17000079 RID: 121
			// (get) Token: 0x060002B0 RID: 688
			public abstract string RemoteMatchedFQDN { get; }

			// Token: 0x1700007A RID: 122
			// (get) Token: 0x060002B1 RID: 689
			public abstract X509Certificate RemoteCertificate { get; }

			// Token: 0x1700007B RID: 123
			// (get) Token: 0x060002B2 RID: 690
			public abstract IPAddress RemotePeer { get; }

			// Token: 0x1700007C RID: 124
			// (get) Token: 0x060002B3 RID: 691
			public abstract string QualityReportUri { get; }

			// Token: 0x1700007D RID: 125
			// (get) Token: 0x060002B4 RID: 692
			public abstract string RemoteContactUri { get; }

			// Token: 0x1700007E RID: 126
			// (get) Token: 0x060002B5 RID: 693
			public abstract string ApplicationAor { get; }

			// Token: 0x060002B6 RID: 694 RVA: 0x0000B258 File Offset: 0x00009458
			protected string GetHeaderUri(SignalingHeader header)
			{
				string result = null;
				if (header != null)
				{
					SignalingHeaderParser signalingHeaderParser = new SignalingHeaderParser(header);
					if (null != signalingHeaderParser.Uri)
					{
						result = signalingHeaderParser.Uri.ToString();
					}
				}
				return result;
			}
		}

		// Token: 0x0200003E RID: 62
		private class InboundCallInfoProvider : UcmaCallInfo.CallInfoProvider
		{
			// Token: 0x060002B8 RID: 696 RVA: 0x0000B294 File Offset: 0x00009494
			public InboundCallInfoProvider(CallReceivedEventArgs<AudioVideoCall> e)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.UCMATracer, this, "InboundCallInfoProvider.  CallReceivedEventArgs={0}, Connection={1}", new object[]
				{
					e,
					e.Connection
				});
				this.callReceivedEventArgs = e;
			}

			// Token: 0x1700007F RID: 127
			// (get) Token: 0x060002B9 RID: 697 RVA: 0x0000B2D3 File Offset: 0x000094D3
			public override ConversationParticipant LocalParticipant
			{
				get
				{
					return this.callReceivedEventArgs.Call.Conversation.LocalParticipant;
				}
			}

			// Token: 0x17000080 RID: 128
			// (get) Token: 0x060002BA RID: 698 RVA: 0x0000B2EA File Offset: 0x000094EA
			public override ConversationParticipant RemoteParticipant
			{
				get
				{
					return this.callReceivedEventArgs.RemoteParticipant;
				}
			}

			// Token: 0x17000081 RID: 129
			// (get) Token: 0x060002BB RID: 699 RVA: 0x0000B2F7 File Offset: 0x000094F7
			public override IEnumerable<SignalingHeader> SignalingHeaders
			{
				get
				{
					return this.callReceivedEventArgs.RequestData.SignalingHeaders;
				}
			}

			// Token: 0x17000082 RID: 130
			// (get) Token: 0x060002BC RID: 700 RVA: 0x0000B309 File Offset: 0x00009509
			public override FromToHeader FromHeader
			{
				get
				{
					return this.callReceivedEventArgs.RequestData.FromHeader;
				}
			}

			// Token: 0x17000083 RID: 131
			// (get) Token: 0x060002BD RID: 701 RVA: 0x0000B31B File Offset: 0x0000951B
			public override FromToHeader ToHeader
			{
				get
				{
					return this.callReceivedEventArgs.RequestData.ToHeader;
				}
			}

			// Token: 0x17000084 RID: 132
			// (get) Token: 0x060002BE RID: 702 RVA: 0x0000B32D File Offset: 0x0000952D
			public override string UserAgent
			{
				get
				{
					return this.callReceivedEventArgs.RequestData.UserAgent;
				}
			}

			// Token: 0x17000085 RID: 133
			// (get) Token: 0x060002BF RID: 703 RVA: 0x0000B33F File Offset: 0x0000953F
			public override string RequestUri
			{
				get
				{
					return this.callReceivedEventArgs.RequestData.RequestUri;
				}
			}

			// Token: 0x17000086 RID: 134
			// (get) Token: 0x060002C0 RID: 704 RVA: 0x0000B351 File Offset: 0x00009551
			public override string CallId
			{
				get
				{
					return this.callReceivedEventArgs.Call.CallId;
				}
			}

			// Token: 0x17000087 RID: 135
			// (get) Token: 0x060002C1 RID: 705 RVA: 0x0000B364 File Offset: 0x00009564
			public override List<PlatformDiversionInfo> DiversionInfo
			{
				get
				{
					return UcmaDiversionInfo.CreateDiversionInfoList(this.callReceivedEventArgs.DiversionContext);
				}
			}

			// Token: 0x17000088 RID: 136
			// (get) Token: 0x060002C2 RID: 706 RVA: 0x0000B383 File Offset: 0x00009583
			public override string RemoteMatchedFQDN
			{
				get
				{
					return this.callReceivedEventArgs.Connection.MatchingDomainName;
				}
			}

			// Token: 0x17000089 RID: 137
			// (get) Token: 0x060002C3 RID: 707 RVA: 0x0000B395 File Offset: 0x00009595
			public override X509Certificate RemoteCertificate
			{
				get
				{
					return this.callReceivedEventArgs.Connection.RemoteCertificate;
				}
			}

			// Token: 0x1700008A RID: 138
			// (get) Token: 0x060002C4 RID: 708 RVA: 0x0000B3A7 File Offset: 0x000095A7
			public override IPAddress RemotePeer
			{
				get
				{
					return this.callReceivedEventArgs.Connection.RemoteEndpoint.Address;
				}
			}

			// Token: 0x1700008B RID: 139
			// (get) Token: 0x060002C5 RID: 709 RVA: 0x0000B3D4 File Offset: 0x000095D4
			public override string QualityReportUri
			{
				get
				{
					string text = this.GetDiversionUri();
					if (!this.IsValidReportingUri(text))
					{
						text = base.GetHeaderUri(this.SignalingHeaders.FirstOrDefault((SignalingHeader x) => x.Name.Equals("ms-application-aor", StringComparison.OrdinalIgnoreCase)));
						if (!this.IsValidReportingUri(text))
						{
							text = this.ToHeader.Uri;
						}
					}
					ExAssert.RetailAssert(this.IsValidReportingUri(text), "There is no valid uri for reporting!");
					return text;
				}
			}

			// Token: 0x1700008C RID: 140
			// (get) Token: 0x060002C6 RID: 710 RVA: 0x0000B45C File Offset: 0x0000965C
			public override string RemoteContactUri
			{
				get
				{
					return base.GetHeaderUri(this.SignalingHeaders.FirstOrDefault((SignalingHeader x) => x.Name.Equals("Contact", StringComparison.OrdinalIgnoreCase)));
				}
			}

			// Token: 0x1700008D RID: 141
			// (get) Token: 0x060002C7 RID: 711 RVA: 0x0000B4A0 File Offset: 0x000096A0
			public override string ApplicationAor
			{
				get
				{
					string result = null;
					SignalingHeader signalingHeader = this.SignalingHeaders.FirstOrDefault((SignalingHeader x) => x.Name.Equals("ms-application-aor", StringComparison.OrdinalIgnoreCase));
					if (signalingHeader != null)
					{
						try
						{
							SignalingHeaderParser signalingHeaderParser = new SignalingHeaderParser(signalingHeader);
							if (null != signalingHeaderParser.Uri)
							{
								result = signalingHeaderParser.Uri.UserAtHost;
							}
						}
						catch (MessageParsingException ex)
						{
							CallIdTracer.TraceDebug(ExTraceGlobals.UCMATracer, this, "Failed to parse the ApplicationAor header. Exception: {0}", new object[]
							{
								ex
							});
							result = null;
						}
					}
					return result;
				}
			}

			// Token: 0x060002C8 RID: 712 RVA: 0x0000B534 File Offset: 0x00009734
			private string GetDiversionUri()
			{
				string result = null;
				DiversionContext diversionContext = this.callReceivedEventArgs.DiversionContext;
				if (diversionContext != null)
				{
					Collection<DivertedDestination> allDivertedDestinations = diversionContext.GetAllDivertedDestinations();
					if (allDivertedDestinations != null && allDivertedDestinations.Count > 0)
					{
						result = allDivertedDestinations[0].Uri;
					}
				}
				return result;
			}

			// Token: 0x060002C9 RID: 713 RVA: 0x0000B574 File Offset: 0x00009774
			private bool IsValidReportingUri(string input)
			{
				bool result = false;
				if (!string.IsNullOrEmpty(input))
				{
					try
					{
						new RealTimeAddress(input);
						result = true;
					}
					catch (ArgumentException ex)
					{
						CallIdTracer.TraceDebug(ExTraceGlobals.UCMATracer, this, "Ignoring invalid reporting uri = {0} {1}", new object[]
						{
							input,
							ex
						});
					}
				}
				return result;
			}

			// Token: 0x040000E9 RID: 233
			private CallReceivedEventArgs<AudioVideoCall> callReceivedEventArgs;
		}

		// Token: 0x0200003F RID: 63
		private class OutboundCallInfoProvider : UcmaCallInfo.CallInfoProvider
		{
			// Token: 0x060002CD RID: 717 RVA: 0x0000B5CC File Offset: 0x000097CC
			public OutboundCallInfoProvider(CallMessageData d, Conversation conv, IPAddress remotePeer)
			{
				this.data = d;
				this.conv = conv;
				this.remotePeer = remotePeer;
			}

			// Token: 0x1700008E RID: 142
			// (get) Token: 0x060002CE RID: 718 RVA: 0x0000B5E9 File Offset: 0x000097E9
			public override ConversationParticipant LocalParticipant
			{
				get
				{
					return this.conv.LocalParticipant;
				}
			}

			// Token: 0x1700008F RID: 143
			// (get) Token: 0x060002CF RID: 719 RVA: 0x0000B5F6 File Offset: 0x000097F6
			public override ConversationParticipant RemoteParticipant
			{
				get
				{
					if (this.conv.RemoteParticipants != null)
					{
						return this.conv.RemoteParticipants[0];
					}
					return null;
				}
			}

			// Token: 0x17000090 RID: 144
			// (get) Token: 0x060002D0 RID: 720 RVA: 0x0000B618 File Offset: 0x00009818
			public override IEnumerable<SignalingHeader> SignalingHeaders
			{
				get
				{
					return this.data.MessageData.SignalingHeaders;
				}
			}

			// Token: 0x17000091 RID: 145
			// (get) Token: 0x060002D1 RID: 721 RVA: 0x0000B62A File Offset: 0x0000982A
			public override FromToHeader FromHeader
			{
				get
				{
					return this.data.MessageData.FromHeader;
				}
			}

			// Token: 0x17000092 RID: 146
			// (get) Token: 0x060002D2 RID: 722 RVA: 0x0000B63C File Offset: 0x0000983C
			public override FromToHeader ToHeader
			{
				get
				{
					return this.data.MessageData.ToHeader;
				}
			}

			// Token: 0x17000093 RID: 147
			// (get) Token: 0x060002D3 RID: 723 RVA: 0x0000B64E File Offset: 0x0000984E
			public override string UserAgent
			{
				get
				{
					return this.data.MessageData.UserAgent;
				}
			}

			// Token: 0x17000094 RID: 148
			// (get) Token: 0x060002D4 RID: 724 RVA: 0x0000B660 File Offset: 0x00009860
			public override string RequestUri
			{
				get
				{
					return this.data.MessageData.RequestUri;
				}
			}

			// Token: 0x17000095 RID: 149
			// (get) Token: 0x060002D5 RID: 725 RVA: 0x0000B672 File Offset: 0x00009872
			public override string CallId
			{
				get
				{
					return this.data.DialogContext.CallID;
				}
			}

			// Token: 0x17000096 RID: 150
			// (get) Token: 0x060002D6 RID: 726 RVA: 0x0000B684 File Offset: 0x00009884
			public override List<PlatformDiversionInfo> DiversionInfo
			{
				get
				{
					return new List<PlatformDiversionInfo>();
				}
			}

			// Token: 0x17000097 RID: 151
			// (get) Token: 0x060002D7 RID: 727 RVA: 0x0000B68B File Offset: 0x0000988B
			public override string RemoteMatchedFQDN
			{
				get
				{
					return string.Empty;
				}
			}

			// Token: 0x17000098 RID: 152
			// (get) Token: 0x060002D8 RID: 728 RVA: 0x0000B692 File Offset: 0x00009892
			public override X509Certificate RemoteCertificate
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17000099 RID: 153
			// (get) Token: 0x060002D9 RID: 729 RVA: 0x0000B695 File Offset: 0x00009895
			public override IPAddress RemotePeer
			{
				get
				{
					return this.remotePeer;
				}
			}

			// Token: 0x1700009A RID: 154
			// (get) Token: 0x060002DA RID: 730 RVA: 0x0000B69D File Offset: 0x0000989D
			public override string QualityReportUri
			{
				get
				{
					throw new InvalidOperationException();
				}
			}

			// Token: 0x1700009B RID: 155
			// (get) Token: 0x060002DB RID: 731 RVA: 0x0000B6A4 File Offset: 0x000098A4
			public override string RemoteContactUri
			{
				get
				{
					throw new InvalidOperationException();
				}
			}

			// Token: 0x1700009C RID: 156
			// (get) Token: 0x060002DC RID: 732 RVA: 0x0000B6AB File Offset: 0x000098AB
			public override string ApplicationAor
			{
				get
				{
					throw new InvalidOperationException();
				}
			}

			// Token: 0x040000ED RID: 237
			private CallMessageData data;

			// Token: 0x040000EE RID: 238
			private Conversation conv;

			// Token: 0x040000EF RID: 239
			private IPAddress remotePeer;
		}

		// Token: 0x02000040 RID: 64
		private class ServiceRequestProvider : UcmaCallInfo.CallInfoProvider
		{
			// Token: 0x060002DD RID: 733 RVA: 0x0000B6B4 File Offset: 0x000098B4
			public ServiceRequestProvider(MessageReceivedEventArgs e, RealTimeConnection connection)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.UCMATracer, this, "ServiceRequestProvider: args={0} conn={1}", new object[]
				{
					e,
					connection
				});
				this.args = e;
				this.connection = connection;
			}

			// Token: 0x1700009D RID: 157
			// (get) Token: 0x060002DE RID: 734 RVA: 0x0000B6F5 File Offset: 0x000098F5
			public override IEnumerable<SignalingHeader> SignalingHeaders
			{
				get
				{
					return this.args.RequestData.SignalingHeaders;
				}
			}

			// Token: 0x1700009E RID: 158
			// (get) Token: 0x060002DF RID: 735 RVA: 0x0000B707 File Offset: 0x00009907
			public override FromToHeader FromHeader
			{
				get
				{
					return this.args.RequestData.FromHeader;
				}
			}

			// Token: 0x1700009F RID: 159
			// (get) Token: 0x060002E0 RID: 736 RVA: 0x0000B719 File Offset: 0x00009919
			public override FromToHeader ToHeader
			{
				get
				{
					return this.args.RequestData.ToHeader;
				}
			}

			// Token: 0x170000A0 RID: 160
			// (get) Token: 0x060002E1 RID: 737 RVA: 0x0000B72B File Offset: 0x0000992B
			public override string UserAgent
			{
				get
				{
					return this.args.RequestData.UserAgent;
				}
			}

			// Token: 0x170000A1 RID: 161
			// (get) Token: 0x060002E2 RID: 738 RVA: 0x0000B73D File Offset: 0x0000993D
			public override string RequestUri
			{
				get
				{
					return this.args.RequestData.RequestUri;
				}
			}

			// Token: 0x170000A2 RID: 162
			// (get) Token: 0x060002E3 RID: 739 RVA: 0x0000B764 File Offset: 0x00009964
			public override string CallId
			{
				get
				{
					SignalingHeader signalingHeader = this.SignalingHeaders.FirstOrDefault((SignalingHeader x) => x.Name.Equals("Call-ID", StringComparison.OrdinalIgnoreCase));
					if (signalingHeader == null)
					{
						return string.Empty;
					}
					return signalingHeader.GetValue();
				}
			}

			// Token: 0x170000A3 RID: 163
			// (get) Token: 0x060002E4 RID: 740 RVA: 0x0000B7A9 File Offset: 0x000099A9
			public override List<PlatformDiversionInfo> DiversionInfo
			{
				get
				{
					return new List<PlatformDiversionInfo>();
				}
			}

			// Token: 0x170000A4 RID: 164
			// (get) Token: 0x060002E5 RID: 741 RVA: 0x0000B7B0 File Offset: 0x000099B0
			public override string RemoteMatchedFQDN
			{
				get
				{
					return this.connection.MatchingDomainName;
				}
			}

			// Token: 0x170000A5 RID: 165
			// (get) Token: 0x060002E6 RID: 742 RVA: 0x0000B7BD File Offset: 0x000099BD
			public override X509Certificate RemoteCertificate
			{
				get
				{
					return this.connection.RemoteCertificate;
				}
			}

			// Token: 0x170000A6 RID: 166
			// (get) Token: 0x060002E7 RID: 743 RVA: 0x0000B7CA File Offset: 0x000099CA
			public override IPAddress RemotePeer
			{
				get
				{
					return this.connection.RemoteEndpoint.Address;
				}
			}

			// Token: 0x170000A7 RID: 167
			// (get) Token: 0x060002E8 RID: 744 RVA: 0x0000B7DC File Offset: 0x000099DC
			public override ConversationParticipant LocalParticipant
			{
				get
				{
					throw new InvalidOperationException();
				}
			}

			// Token: 0x170000A8 RID: 168
			// (get) Token: 0x060002E9 RID: 745 RVA: 0x0000B7E3 File Offset: 0x000099E3
			public override ConversationParticipant RemoteParticipant
			{
				get
				{
					throw new InvalidOperationException();
				}
			}

			// Token: 0x170000A9 RID: 169
			// (get) Token: 0x060002EA RID: 746 RVA: 0x0000B7EA File Offset: 0x000099EA
			public override string QualityReportUri
			{
				get
				{
					throw new InvalidOperationException();
				}
			}

			// Token: 0x170000AA RID: 170
			// (get) Token: 0x060002EB RID: 747 RVA: 0x0000B7F1 File Offset: 0x000099F1
			public override string RemoteContactUri
			{
				get
				{
					throw new InvalidOperationException();
				}
			}

			// Token: 0x170000AB RID: 171
			// (get) Token: 0x060002EC RID: 748 RVA: 0x0000B7F8 File Offset: 0x000099F8
			public override string ApplicationAor
			{
				get
				{
					throw new InvalidOperationException();
				}
			}

			// Token: 0x040000F0 RID: 240
			private MessageReceivedEventArgs args;

			// Token: 0x040000F1 RID: 241
			private RealTimeConnection connection;
		}
	}
}
