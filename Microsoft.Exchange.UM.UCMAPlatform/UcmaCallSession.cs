using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Audio;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.UM.TroubleshootingTool.Shared;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCore;
using Microsoft.Rtc.Collaboration;
using Microsoft.Rtc.Collaboration.AudioVideo;
using Microsoft.Rtc.Signaling;
using Microsoft.Speech.AudioFormat;
using Microsoft.Speech.Recognition;
using Microsoft.Speech.Synthesis;

namespace Microsoft.Exchange.UM.UcmaPlatform
{
	// Token: 0x0200000A RID: 10
	internal class UcmaCallSession : BaseUMCallSession, IUMSpeechRecognizer, ISerializationGuard
	{
		// Token: 0x0600003D RID: 61 RVA: 0x00002CF4 File Offset: 0x00000EF4
		public UcmaCallSession(ISessionSerializer serializer, ApplicationEndpoint localEndpoint, CallContext cc) : this(serializer, localEndpoint, null, cc, null)
		{
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002D04 File Offset: 0x00000F04
		public UcmaCallSession(ISessionSerializer serializer, ApplicationEndpoint localEndpoint, UcmaCallInfo callInfo, CallContext cc, AudioVideoCall call)
		{
			this.Diag = new DiagnosticHelper(this, ExTraceGlobals.UCMATracer);
			this.CallInfo = callInfo;
			this.ConsecutiveErrors = new List<Exception>();
			this.Serializer = serializer;
			base.CurrentCallContext = cc;
			this.loggingManager = new UcmaLoggingManager(this.Diag);
			this.LocalEndpoint = localEndpoint;
			this.ToneAccumulator = new ToneAccumulator();
			this.TestInfo = new UcmaCallSession.TestInfoManager(this);
			this.CurrentState = new UcmaCallSession.IdleSessionState(this);
			this.CurrentState.Start();
			base.RemoteCertificate = ((callInfo != null) ? callInfo.RemoteCertificate : null);
			base.RemoteMatchedFQDN = cc.RemoteFQDN;
			this.CommandAndControlLoggingCounter = 0;
			this.CommandAndControlLoggingEnabled = UcmaAudioLogging.CmdAndControlAudioLoggingEnabled;
			if (call != null)
			{
				this.AssociateCall(call);
			}
			if (this.Subscriber == null)
			{
				this.Subscriber = new UcmaCallSession.SubscriptionHelper(this);
			}
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x0600003F RID: 63 RVA: 0x00002DE0 File Offset: 0x00000FE0
		// (remove) Token: 0x06000040 RID: 64 RVA: 0x00002E18 File Offset: 0x00001018
		public event UMCallSessionHandler<UMSpeechEventArgs> OnSpeech;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000041 RID: 65 RVA: 0x00002E50 File Offset: 0x00001050
		// (remove) Token: 0x06000042 RID: 66 RVA: 0x00002E88 File Offset: 0x00001088
		internal override event UMCallSessionHandler<EventArgs> OnCallConnected;

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000043 RID: 67 RVA: 0x00002EC0 File Offset: 0x000010C0
		// (remove) Token: 0x06000044 RID: 68 RVA: 0x00002EF8 File Offset: 0x000010F8
		internal override event UMCallSessionHandler<UMCallSessionEventArgs> OnCancelled;

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06000045 RID: 69 RVA: 0x00002F30 File Offset: 0x00001130
		// (remove) Token: 0x06000046 RID: 70 RVA: 0x00002F68 File Offset: 0x00001168
		internal override event UMCallSessionHandler<UMCallSessionEventArgs> OnComplete;

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x06000047 RID: 71 RVA: 0x00002FA0 File Offset: 0x000011A0
		// (remove) Token: 0x06000048 RID: 72 RVA: 0x00002FD8 File Offset: 0x000011D8
		internal override event UMCallSessionHandler<EventArgs> OnDispose;

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x06000049 RID: 73 RVA: 0x00003010 File Offset: 0x00001210
		// (remove) Token: 0x0600004A RID: 74 RVA: 0x00003048 File Offset: 0x00001248
		internal override event UMCallSessionHandler<UMCallSessionEventArgs> OnDtmf;

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x0600004B RID: 75 RVA: 0x00003080 File Offset: 0x00001280
		// (remove) Token: 0x0600004C RID: 76 RVA: 0x000030B8 File Offset: 0x000012B8
		internal override event UMCallSessionHandler<UMCallSessionEventArgs> OnError;

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x0600004D RID: 77 RVA: 0x000030F0 File Offset: 0x000012F0
		// (remove) Token: 0x0600004E RID: 78 RVA: 0x00003128 File Offset: 0x00001328
		internal override event UMCallSessionHandler<UMCallSessionEventArgs> OnFaxRequestReceive;

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x0600004F RID: 79 RVA: 0x00003160 File Offset: 0x00001360
		// (remove) Token: 0x06000050 RID: 80 RVA: 0x00003198 File Offset: 0x00001398
		internal override event UMCallSessionHandler<UMCallSessionEventArgs> OnHangup;

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x06000051 RID: 81 RVA: 0x000031D0 File Offset: 0x000013D0
		// (remove) Token: 0x06000052 RID: 82 RVA: 0x00003208 File Offset: 0x00001408
		internal override event UMCallSessionHandler<HeavyBlockingOperationEventArgs> OnHeavyBlockingOperation;

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x06000053 RID: 83 RVA: 0x00003240 File Offset: 0x00001440
		// (remove) Token: 0x06000054 RID: 84 RVA: 0x00003278 File Offset: 0x00001478
		internal override event UMCallSessionHandler<InfoMessage.MessageReceivedEventArgs> OnMessageReceived;

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x06000055 RID: 85 RVA: 0x000032B0 File Offset: 0x000014B0
		// (remove) Token: 0x06000056 RID: 86 RVA: 0x000032E8 File Offset: 0x000014E8
		internal override event UMCallSessionHandler<EventArgs> OnMessageSent;

		// Token: 0x1400000F RID: 15
		// (add) Token: 0x06000057 RID: 87 RVA: 0x00003320 File Offset: 0x00001520
		// (remove) Token: 0x06000058 RID: 88 RVA: 0x00003358 File Offset: 0x00001558
		internal override event UMCallSessionHandler<OutboundCallDetailsEventArgs> OnOutboundCallRequestCompleted;

		// Token: 0x14000010 RID: 16
		// (add) Token: 0x06000059 RID: 89 RVA: 0x00003390 File Offset: 0x00001590
		// (remove) Token: 0x0600005A RID: 90 RVA: 0x000033C8 File Offset: 0x000015C8
		internal override event UMCallSessionHandler<UMCallSessionEventArgs> OnStateInfoSent;

		// Token: 0x14000011 RID: 17
		// (add) Token: 0x0600005B RID: 91 RVA: 0x00003400 File Offset: 0x00001600
		// (remove) Token: 0x0600005C RID: 92 RVA: 0x00003438 File Offset: 0x00001638
		internal override event UMCallSessionHandler<UMCallSessionEventArgs> OnTimeout;

		// Token: 0x14000012 RID: 18
		// (add) Token: 0x0600005D RID: 93 RVA: 0x00003470 File Offset: 0x00001670
		// (remove) Token: 0x0600005E RID: 94 RVA: 0x000034A8 File Offset: 0x000016A8
		internal override event UMCallSessionHandler<UMCallSessionEventArgs> OnTransferComplete;

		// Token: 0x14000013 RID: 19
		// (add) Token: 0x0600005F RID: 95 RVA: 0x000034E0 File Offset: 0x000016E0
		// (remove) Token: 0x06000060 RID: 96 RVA: 0x00003518 File Offset: 0x00001718
		internal override event UMCallSessionHandler<UMCallSessionEventArgs> OnHold;

		// Token: 0x14000014 RID: 20
		// (add) Token: 0x06000061 RID: 97 RVA: 0x00003550 File Offset: 0x00001750
		// (remove) Token: 0x06000062 RID: 98 RVA: 0x00003588 File Offset: 0x00001788
		internal override event UMCallSessionHandler<UMCallSessionEventArgs> OnResume;

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000063 RID: 99 RVA: 0x000035C0 File Offset: 0x000017C0
		public MediaEncryption MediaEncryptionPolicy
		{
			get
			{
				MediaEncryption mediaEncryption;
				if (base.CurrentCallContext.IsLocalDiagnosticCall)
				{
					mediaEncryption = 2;
				}
				else if (base.CurrentCallContext.DialPlan.VoIPSecurity == UMVoIPSecurityType.Secured)
				{
					mediaEncryption = 3;
				}
				else
				{
					mediaEncryption = 1;
				}
				this.Diag.Trace("get_MediaEncryptionPolicy '{0}'", new object[]
				{
					mediaEncryption
				});
				return mediaEncryption;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000064 RID: 100 RVA: 0x0000361B File Offset: 0x0000181B
		// (set) Token: 0x06000065 RID: 101 RVA: 0x00003623 File Offset: 0x00001823
		public bool StopSerializedEvents { get; private set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000066 RID: 102 RVA: 0x0000362C File Offset: 0x0000182C
		public object SerializationLocker
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000067 RID: 103 RVA: 0x0000362F File Offset: 0x0000182F
		// (set) Token: 0x06000068 RID: 104 RVA: 0x00003637 File Offset: 0x00001837
		public ApplicationEndpoint LocalEndpoint { get; private set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00003640 File Offset: 0x00001840
		// (set) Token: 0x0600006A RID: 106 RVA: 0x00003648 File Offset: 0x00001848
		public ISessionSerializer Serializer { get; private set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00003651 File Offset: 0x00001851
		// (set) Token: 0x0600006C RID: 108 RVA: 0x00003659 File Offset: 0x00001859
		public int CommandAndControlLoggingCounter { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00003662 File Offset: 0x00001862
		// (set) Token: 0x0600006E RID: 110 RVA: 0x0000366A File Offset: 0x0000186A
		public bool CommandAndControlLoggingEnabled { get; private set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600006F RID: 111 RVA: 0x00003673 File Offset: 0x00001873
		internal override IUMSpeechRecognizer SpeechRecognizer
		{
			get
			{
				return this;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000070 RID: 112 RVA: 0x00003676 File Offset: 0x00001876
		internal override bool IsClosing
		{
			get
			{
				return this.IsSignalingTerminatingOrTerminated;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000071 RID: 113 RVA: 0x00003680 File Offset: 0x00001880
		internal override UMCallState State
		{
			get
			{
				UMCallState result = UMCallState.Disconnected;
				switch (this.SnapshotCallState())
				{
				case 0:
					result = UMCallState.Idle;
					break;
				case 1:
					result = UMCallState.Incoming;
					break;
				case 2:
					result = UMCallState.Connecting;
					break;
				case 3:
					result = UMCallState.Connected;
					break;
				case 5:
					result = UMCallState.Transferring;
					break;
				case 7:
					result = UMCallState.Disconnected;
					break;
				case 8:
					result = UMCallState.Disconnected;
					break;
				}
				return result;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000072 RID: 114 RVA: 0x000036DF File Offset: 0x000018DF
		internal override UMLoggingManager LoggingManager
		{
			get
			{
				return this.loggingManager;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000073 RID: 115 RVA: 0x000036E7 File Offset: 0x000018E7
		internal override string CallLegId
		{
			get
			{
				return this.Ucma.Call.CallId;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000074 RID: 116 RVA: 0x000036F9 File Offset: 0x000018F9
		internal bool IsInvalidOperationExplainable
		{
			get
			{
				return this.IsSignalingTerminatingOrTerminated || this.IsMediaTerminatingOrTerminated;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000075 RID: 117 RVA: 0x0000370B File Offset: 0x0000190B
		protected override bool IsDependentSession
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000076 RID: 118 RVA: 0x0000370E File Offset: 0x0000190E
		// (set) Token: 0x06000077 RID: 119 RVA: 0x00003716 File Offset: 0x00001916
		private UcmaCallSession.UcmaObjects Ucma { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000078 RID: 120 RVA: 0x0000371F File Offset: 0x0000191F
		// (set) Token: 0x06000079 RID: 121 RVA: 0x00003727 File Offset: 0x00001927
		private UcmaCallSession.SubscriptionHelper Subscriber { get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00003730 File Offset: 0x00001930
		// (set) Token: 0x0600007B RID: 123 RVA: 0x00003738 File Offset: 0x00001938
		private UcmaCallSession.TestInfoManager TestInfo { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00003741 File Offset: 0x00001941
		// (set) Token: 0x0600007D RID: 125 RVA: 0x00003749 File Offset: 0x00001949
		private DiagnosticHelper Diag { get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00003752 File Offset: 0x00001952
		// (set) Token: 0x0600007F RID: 127 RVA: 0x0000375A File Offset: 0x0000195A
		private UcmaCallInfo CallInfo { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000080 RID: 128 RVA: 0x00003763 File Offset: 0x00001963
		// (set) Token: 0x06000081 RID: 129 RVA: 0x0000376B File Offset: 0x0000196B
		private List<Exception> ConsecutiveErrors { get; set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000082 RID: 130 RVA: 0x00003774 File Offset: 0x00001974
		// (set) Token: 0x06000083 RID: 131 RVA: 0x0000377C File Offset: 0x0000197C
		private UcmaCallSession.SessionState CurrentState { get; set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000084 RID: 132 RVA: 0x00003785 File Offset: 0x00001985
		// (set) Token: 0x06000085 RID: 133 RVA: 0x0000378D File Offset: 0x0000198D
		private ToneAccumulator ToneAccumulator { get; set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000086 RID: 134 RVA: 0x00003796 File Offset: 0x00001996
		// (set) Token: 0x06000087 RID: 135 RVA: 0x0000379E File Offset: 0x0000199E
		private CallStateTransitionReason CallStateTransitionReason { get; set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000088 RID: 136 RVA: 0x000037A7 File Offset: 0x000019A7
		// (set) Token: 0x06000089 RID: 137 RVA: 0x000037AF File Offset: 0x000019AF
		private CallState LastCallStateChangeProcessed { get; set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600008A RID: 138 RVA: 0x000037B8 File Offset: 0x000019B8
		private bool IsSignalingTerminatingOrTerminated
		{
			get
			{
				CallState callState = this.SnapshotCallState();
				return callState == 7 || callState == 8;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600008B RID: 139 RVA: 0x000037D8 File Offset: 0x000019D8
		private bool IsMediaTerminatingOrTerminated
		{
			get
			{
				MediaFlowState mediaFlowState = this.SnapshotFlowState();
				return mediaFlowState == 2;
			}
		}

		// Token: 0x0600008C RID: 140 RVA: 0x000037F0 File Offset: 0x000019F0
		public static string FormatStateChange<T>(string reference, T previous, T next)
		{
			return string.Format("{0}_StateChanged from {1} to {2}", reference, previous, next);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x0000380C File Offset: 0x00001A0C
		public static byte ToneToByte(DtmfTone tone)
		{
			byte result = 0;
			switch (tone)
			{
			case 0:
				result = 48;
				break;
			case 1:
				result = 49;
				break;
			case 2:
				result = 50;
				break;
			case 3:
				result = 51;
				break;
			case 4:
				result = 52;
				break;
			case 5:
				result = 53;
				break;
			case 6:
				result = 54;
				break;
			case 7:
				result = 55;
				break;
			case 8:
				result = 56;
				break;
			case 9:
				result = 57;
				break;
			case 10:
				result = 42;
				break;
			case 11:
				result = 35;
				break;
			case 12:
				result = 65;
				break;
			case 13:
				result = 66;
				break;
			case 14:
				result = 67;
				break;
			case 15:
				result = 68;
				break;
			}
			return result;
		}

		// Token: 0x0600008E RID: 142 RVA: 0x000038B4 File Offset: 0x00001AB4
		public static ToneId CharToToneId(char c)
		{
			ToneId result = 0;
			switch (c)
			{
			case '#':
				result = 11;
				break;
			case '$':
			case '%':
			case '&':
			case '\'':
			case '(':
			case ')':
			case '+':
			case ',':
			case '-':
			case '.':
			case '/':
				break;
			case '*':
				result = 10;
				break;
			case '0':
				result = 0;
				break;
			case '1':
				result = 1;
				break;
			case '2':
				result = 2;
				break;
			case '3':
				result = 3;
				break;
			case '4':
				result = 4;
				break;
			case '5':
				result = 5;
				break;
			case '6':
				result = 6;
				break;
			case '7':
				result = 7;
				break;
			case '8':
				result = 8;
				break;
			case '9':
				result = 9;
				break;
			default:
				switch (c)
				{
				case 'A':
					result = 12;
					break;
				case 'B':
					result = 13;
					break;
				case 'C':
					result = 14;
					break;
				case 'D':
					result = 15;
					break;
				}
				break;
			}
			return result;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x0000398B File Offset: 0x00001B8B
		public override void RebufferDigits(byte[] dtmfDigits)
		{
			this.ToneAccumulator.RebufferDigits(dtmfDigits);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00003999 File Offset: 0x00001B99
		public void AssociateCall(AudioVideoCall call)
		{
			this.TeardownUcmaObjects();
			this.TeardownSubscriber();
			base.CallId = call.CallId;
			this.Ucma = new UcmaCallSession.UcmaObjects(this, call);
			this.Subscriber = new UcmaCallSession.SubscriptionHelper(this);
			this.Subscriber.SubscribeTo(call);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x000039D8 File Offset: 0x00001BD8
		public void PlayPrompts(ArrayList prompts, int minDigits, int maxDigits, int timeout, string stopTones, int interDigitTimeout, StopPatterns stopPatterns, int startIdx, TimeSpan offset, List<UMGrammar> grammars, bool expetingSpeechInput, int babbleTimeout, bool stopPromptOnBargeIn, string turnName, int initialSilenceTimeout)
		{
			UcmaCallSession.PlaybackInfo promptInfo = new UcmaCallSession.PlaybackInfo
			{
				Prompts = prompts,
				MinDigits = minDigits,
				MaxDigits = maxDigits,
				Timeout = TimeSpan.FromSeconds((double)timeout),
				StopTones = stopTones,
				InterDigitTimeout = TimeSpan.FromSeconds((double)interDigitTimeout),
				StopPatterns = stopPatterns,
				Offset = offset,
				Grammars = grammars,
				BabbleTimeout = TimeSpan.FromSeconds((double)babbleTimeout),
				UnconditionalBargeIn = stopPromptOnBargeIn,
				TurnName = this.activityState,
				InitialSilenceTimeout = TimeSpan.FromSeconds((double)initialSilenceTimeout)
			};
			if (this.TestInfo.IsFeatureEnabled(UcmaCallSession.TestInfoFeatures.PlayAudio) || this.TestInfo.IsFeatureEnabled(UcmaCallSession.TestInfoFeatures.RecoEmulate))
			{
				this.ChangeState(new UcmaCallSession.PlayPromptsAndRecoEmulateSessionState(this, promptInfo));
				return;
			}
			if (this.CommandAndControlLoggingEnabled)
			{
				this.ChangeState(new UcmaCallSession.CommandAndControlLoggingSessionState(this, promptInfo));
				return;
			}
			this.ChangeState(new UcmaCallSession.PlayPromptsAndRecoSpeechSessionState(this, promptInfo));
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00003ABC File Offset: 0x00001CBC
		internal override void PlayPrompts(ArrayList prompts, int minDigits, int maxDigits, int timeout, string stopTones, int interDigitTimeout, StopPatterns stopPatterns, int startIdx, TimeSpan offset, bool stopPromptOnBargeIn, string turnName, int initialSilenceTimeout)
		{
			UcmaCallSession.PlaybackInfo promptInfo = new UcmaCallSession.PlaybackInfo
			{
				Prompts = prompts,
				MinDigits = minDigits,
				MaxDigits = maxDigits,
				Timeout = TimeSpan.FromSeconds((double)timeout),
				InterDigitTimeout = TimeSpan.FromSeconds((double)interDigitTimeout),
				StopTones = stopTones,
				StopPatterns = stopPatterns,
				UnconditionalBargeIn = stopPromptOnBargeIn,
				Offset = offset,
				Uninterruptable = false,
				TurnName = this.activityState,
				InitialSilenceTimeout = TimeSpan.FromSeconds((double)initialSilenceTimeout)
			};
			if (maxDigits == 0)
			{
				this.ChangeState(new UcmaCallSession.PlayPromptsSessionState(this, promptInfo));
				return;
			}
			this.ChangeState(new UcmaCallSession.PlayPromptsAndRecoDtmfSessionState(this, promptInfo));
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003B64 File Offset: 0x00001D64
		internal override void PlayUninterruptiblePrompts(ArrayList prompts)
		{
			UcmaCallSession.PlaybackInfo promptInfo = new UcmaCallSession.PlaybackInfo
			{
				UnconditionalBargeIn = true,
				Prompts = prompts,
				Uninterruptable = true,
				TurnName = this.activityState
			};
			this.ChangeState(new UcmaCallSession.PlayPromptsSessionState(this, promptInfo));
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00003C08 File Offset: 0x00001E08
		internal override BaseUMAsyncTimer StartTimer(BaseUMAsyncTimer.UMTimerCallback callback, int dueTime)
		{
			SerializableCallback<BaseUMCallSession> serializeableCallbackWithErrorHandling = delegate(BaseUMCallSession o)
			{
				this.CatchAndFireOnError(delegate
				{
					callback(this);
				});
			};
			BaseUMAsyncTimer baseUMAsyncTimer = new UcmaAsyncTimer(this, delegate(BaseUMCallSession o)
			{
				this.Serializer.SerializeCallback<BaseUMCallSession>(this, serializeableCallbackWithErrorHandling, this, false, "StartTimer");
			}, dueTime);
			base.Timers.Add(baseUMAsyncTimer);
			return baseUMAsyncTimer;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00003C5C File Offset: 0x00001E5C
		internal override void RecordFile(string fileName, int maxSilence, int maxSeconds, string stopTones, bool append, ArrayList comfortPrompts)
		{
			UcmaCallSession.RecordInfo r = new UcmaCallSession.RecordInfo
			{
				FileName = fileName,
				EndSilenceTimeout = TimeSpan.FromSeconds((double)maxSilence),
				MaxDuration = TimeSpan.FromSeconds((double)maxSeconds),
				StopTones = stopTones,
				Append = append
			};
			this.ChangeState(new UcmaCallSession.RecordFileSessionState(this, r));
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00003CAF File Offset: 0x00001EAF
		internal override void CancelPendingOperations()
		{
			this.CurrentState.Cancel(true);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00003CBD File Offset: 0x00001EBD
		internal override void DisconnectCall(PlatformSignalingHeader diagnosticHeader)
		{
			this.Diag.Trace("Teardown due to DisconnectCall", new object[0]);
			this.ChangeState(new UcmaCallSession.TeardownSessionState(this, UcmaCallSession.DisconnectType.Local, diagnosticHeader));
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00003CE4 File Offset: 0x00001EE4
		internal override void CloseSession()
		{
			UcmaCallSession.EstablishCallSessionState establishCallSessionState = this.CurrentState as UcmaCallSession.EstablishCallSessionState;
			if (establishCallSessionState != null)
			{
				establishCallSessionState.CancelOutboundCall();
				return;
			}
			this.Diag.Trace("Teardown due to CloseSession", new object[0]);
			this.ChangeState(new UcmaCallSession.TeardownSessionState(this, UcmaCallSession.DisconnectType.Remote, null));
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00003D2B File Offset: 0x00001F2B
		internal override void RunHeavyBlockingOperation(IUMHeavyBlockingOperation operation, ArrayList prompts)
		{
			this.ChangeState(new UcmaCallSession.HeavyBlockingOperationSessionState(this, operation, prompts));
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00003D3C File Offset: 0x00001F3C
		internal override void SendMessage(InfoMessage m)
		{
			lock (this)
			{
				this.Diag.Trace("Sending info message within state {0}", new object[]
				{
					this.CurrentState.Name
				});
				this.CurrentState.SendMessage(m);
			}
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00003DA4 File Offset: 0x00001FA4
		internal override void SendDtmf(string dtmfSequence, TimeSpan initialSilence)
		{
			this.ChangeState(new UcmaCallSession.SendDtmfSessionState(this, dtmfSequence, initialSilence));
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00003DB4 File Offset: 0x00001FB4
		internal override void TransferAsync()
		{
			this.ChangeState(new UcmaCallSession.SupervisedTransferSessionState(this, null, null));
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00003DC4 File Offset: 0x00001FC4
		internal override void TransferAsync(PlatformSipUri target)
		{
			this.TransferAsync(target, null);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00003DD0 File Offset: 0x00001FD0
		internal override void TransferAsync(PlatformSipUri target, IList<PlatformSignalingHeader> headers)
		{
			bool flag = false;
			if (base.DependentSessionDetails != null)
			{
				UcmaCallSession ucmaCallSession = base.DependentSessionDetails.DependentUMCallSession as UcmaCallSession;
				if (ucmaCallSession.Ucma != null)
				{
					flag = (null != ucmaCallSession.Ucma.Call);
				}
			}
			this.Diag.Trace("TransferAsync: supervised={0}", new object[]
			{
				flag
			});
			if (flag)
			{
				this.ChangeState(new UcmaCallSession.SupervisedTransferSessionState(this, target, headers));
				return;
			}
			this.ChangeState(new UcmaCallSession.BlindTransferSessionState(this, target, headers));
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00003E53 File Offset: 0x00002053
		internal override void TransferAsync(string phoneNumber)
		{
			this.ChangeState(new UcmaCallSession.BlindTransferSessionState(this, phoneNumber));
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00003E62 File Offset: 0x00002062
		internal override void Redirect(string host, int port, int code)
		{
			this.ChangeState(new UcmaCallSession.RedirectSessionState(this, host, port, code));
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00003E73 File Offset: 0x00002073
		internal override void SendStateInfo(string callId, string state)
		{
			this.activityState = state;
			this.ChangeState(new UcmaCallSession.SendFsmInfoSessionState(this, callId, state));
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00003E8A File Offset: 0x0000208A
		internal override void ClearDigits(int sleepMsec)
		{
			this.ToneAccumulator.Clear();
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00003E97 File Offset: 0x00002097
		internal override void AcceptFax()
		{
			this.ChangeState(new UcmaCallSession.AcceptFaxSessionState(this));
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00003EA8 File Offset: 0x000020A8
		internal override bool IsDuringPlayback()
		{
			bool result = false;
			UcmaCallSession.PlayPromptsSessionState playPromptsSessionState = this.CurrentState as UcmaCallSession.PlayPromptsSessionState;
			if (playPromptsSessionState != null)
			{
				result = playPromptsSessionState.IsSpeaking;
			}
			return result;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00003ED0 File Offset: 0x000020D0
		internal override void StopPlayback()
		{
			UcmaCallSession.PlayPromptsSessionState playPromptsSessionState = this.CurrentState as UcmaCallSession.PlayPromptsSessionState;
			this.Diag.Assert(null != playPromptsSessionState);
			playPromptsSessionState.HandleApplicationRequestToStopPlayback();
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00003F04 File Offset: 0x00002104
		internal override void StopPlaybackAndCancelRecognition()
		{
			UcmaCallSession.PlayPromptsSessionState playPromptsSessionState = this.CurrentState as UcmaCallSession.PlayPromptsSessionState;
			if (playPromptsSessionState != null && !playPromptsSessionState.IsIdle)
			{
				playPromptsSessionState.Cancel(false);
			}
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00003F30 File Offset: 0x00002130
		internal override void Skip(TimeSpan timeToSkip)
		{
			UcmaCallSession.PlayPromptsSessionState playPromptsSessionState = this.CurrentState as UcmaCallSession.PlayPromptsSessionState;
			if (playPromptsSessionState != null && !playPromptsSessionState.IsIdle)
			{
				playPromptsSessionState.Skip(timeToSkip);
			}
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00003F5C File Offset: 0x0000215C
		internal override void InitializeConnectedCall(OutboundCallDetailsEventArgs args)
		{
			this.Diag.Assert(this.CallInfo != null, "CallInfo has not be set, but InitializeConnectedCall is invoked");
			this.Diag.Assert(base.CurrentCallContext != null, "CallContext has not be set, but InitializeConnectedCall is invoked");
			base.CurrentCallContext.ConnectionTime = new ExDateTime?(ExDateTime.UtcNow);
			UcmaVoipPlatform.InitializeOutboundCallContext(this.CallInfo, base.CurrentCallContext);
			this.Fire<EventArgs>(this.OnCallConnected, null);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00003FD4 File Offset: 0x000021D4
		protected virtual void Teardown()
		{
			this.Dispose();
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00003FDC File Offset: 0x000021DC
		protected override void InternalAcceptCall()
		{
			this.ChangeState(new UcmaCallSession.AcceptCallSessionState(this));
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00003FEA File Offset: 0x000021EA
		protected override void SetContactUriForAccept()
		{
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00003FEC File Offset: 0x000021EC
		protected override void DeclineCall(PlatformSignalingHeader diagnosticHeader)
		{
			this.ChangeState(new UcmaCallSession.DeclineSessionState(this, diagnosticHeader));
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00003FFB File Offset: 0x000021FB
		protected override BaseUMCallSession InternalCreateDependentSession(DependentSessionDetails details, CallContext context)
		{
			return new UcmaDependentCallSession(details, this.Serializer, this.LocalEndpoint, context);
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00004010 File Offset: 0x00002210
		protected override void InternalOpenAsync(BaseUMCallSession.OutboundCallInfo info, IList<PlatformSignalingHeader> headers)
		{
			this.ChangeState(new UcmaCallSession.EstablishCallSessionState(this, info, headers));
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00004020 File Offset: 0x00002220
		protected override void InternalDispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					this.StopSerializedEvents = true;
					this.DisposeLoggingManager();
				}
			}
			finally
			{
				base.InternalDispose(disposing);
			}
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00004058 File Offset: 0x00002258
		protected override void TeardownCurrentCall()
		{
			this.Diag.Trace("TeardownCurrentCall", new object[0]);
			try
			{
				this.Fire<EventArgs>(this.OnDispose, null);
				this.TeardownSubscriber();
				this.TeardownUcmaObjects();
				this.TeardownEvents();
				this.TeardownCallState();
			}
			finally
			{
				base.TeardownCurrentCall();
			}
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x000040BC File Offset: 0x000022BC
		private static bool IsGrayException(Exception e)
		{
			bool result = false;
			if (e is WatsoningDueToRecycling)
			{
				result = true;
			}
			return result;
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x000040D8 File Offset: 0x000022D8
		private void ChangeState(UcmaCallSession.SessionState nextState)
		{
			this.Diag.Assert(this.CurrentState == null || this.CurrentState.IsIdle || nextState is UcmaCallSession.TeardownSessionState);
			lock (this)
			{
				if (this.IgnoreStateChange(nextState))
				{
					this.Diag.Trace("Disposing nextState '{0}' because it's being ignored", new object[]
					{
						nextState.Name
					});
					nextState.Dispose();
				}
				else
				{
					this.Diag.Trace("Changing session state from '{0}' to '{1}'", new object[]
					{
						this.CurrentState.Name,
						nextState.Name
					});
					if (this.CurrentState != null)
					{
						this.CurrentState.Dispose();
					}
					this.CurrentState = nextState;
					this.CurrentState.Start();
					this.TeardownIdleSession();
				}
			}
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x000041C8 File Offset: 0x000023C8
		private void TeardownIdleSession()
		{
			if (!base.IsDisposed && this.CurrentState != null && !this.CurrentState.IsDisposed && this.CurrentState.IsIdle)
			{
				this.Diag.Assert(this.IsSignalingTerminatingOrTerminated, "The call session is idle while the call is active!");
				this.Diag.Trace("Tearing down an idle call session.", new object[0]);
				this.ChangeState(new UcmaCallSession.TeardownSessionState(this, UcmaCallSession.DisconnectType.Local, null));
			}
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x0000423C File Offset: 0x0000243C
		private bool IgnoreStateChange(UcmaCallSession.SessionState nextState)
		{
			bool flag = this.CurrentState is UcmaCallSession.IdleSessionState && nextState is UcmaCallSession.IdleSessionState;
			bool flag2 = this.CurrentState is UcmaCallSession.TeardownSessionState;
			return flag || flag2;
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00004278 File Offset: 0x00002478
		private CallState SnapshotCallState()
		{
			CallState result = 0;
			if (this.Ucma != null && this.Ucma.Call != null)
			{
				result = this.Ucma.Call.State;
			}
			return result;
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x000042B0 File Offset: 0x000024B0
		private MediaFlowState SnapshotFlowState()
		{
			MediaFlowState result = 0;
			if (this.Ucma != null)
			{
				if (this.Ucma.Flow != null)
				{
					result = this.Ucma.Flow.State;
				}
				if (this.Ucma.MediaDropped)
				{
					this.Diag.Trace("Assuming media flow is terminated because a connector went inactive", new object[0]);
					result = 2;
				}
			}
			return result;
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x0000430C File Offset: 0x0000250C
		private void Fire<TArgs>(UMCallSessionHandler<TArgs> handler, TArgs args)
		{
			if (handler != null)
			{
				this.Diag.Trace("Firing {0}", new object[]
				{
					handler.Method.Name
				});
				if (handler.Equals(this.OnHangup))
				{
					this.OnHangup = null;
				}
				else if (handler.Equals(this.OnDispose))
				{
					this.OnDispose = null;
				}
				handler(this, args);
			}
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x000044F0 File Offset: 0x000026F0
		private void CatchAndFireOnError(GrayException.UserCodeDelegate function)
		{
			Exception error = null;
			try
			{
				ExceptionHandling.MapAndReportGrayExceptions(delegate()
				{
					try
					{
						function();
					}
					catch (StorageTransientException error2)
					{
						error = error2;
					}
					catch (StoragePermanentException error3)
					{
						error = error3;
					}
					catch (ADTransientException ex)
					{
						this.CurrentCallContext.TrackDirectoryAccessFailures(ex);
						error = ex;
					}
					catch (DataValidationException ex2)
					{
						this.CurrentCallContext.TrackDirectoryAccessFailures(ex2);
						error = ex2;
					}
					catch (InvalidOperationException ex3)
					{
						bool isInvalidOperationExplainable = this.IsInvalidOperationExplainable;
						this.Diag.Trace("Caught IOP='{0}'.  IsInvalidOperationExplainable='{1}'", new object[]
						{
							ex3,
							isInvalidOperationExplainable
						});
						if (!isInvalidOperationExplainable)
						{
							throw;
						}
						error = (this.CurrentState.ConditionalWaitForTerminatedOrFax() ? null : ex3);
					}
					catch (RealTimeException error4)
					{
						error = error4;
					}
					catch (LocalizedException error5)
					{
						error = error5;
					}
				}, new GrayException.IsGrayExceptionDelegate(UcmaCallSession.IsGrayException));
			}
			catch (UMGrayException error)
			{
				UMGrayException error6;
				error = error6;
			}
			catch (Exception e)
			{
				ExceptionHandling.SendWatsonWithExtraData(e, true);
				Utils.KillThisProcess();
			}
			if (error != null)
			{
				this.CatchAndFireOnError(delegate
				{
					this.LogException(error);
					this.CurrentState.FireError(error);
				});
			}
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x0000459C File Offset: 0x0000279C
		private void LogException(Exception e)
		{
			this.Diag.Trace("LogException: {0}", new object[]
			{
				e
			});
			if (this.ShouldLog(e))
			{
				string text = CommonUtil.ToEventLogString(e);
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_PlatformException, null, new object[]
				{
					text,
					base.CallId
				});
				this.LogTlsException(e);
			}
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00004604 File Offset: 0x00002804
		private void LogTlsException(Exception e)
		{
			TlsFailureException ex = e as TlsFailureException;
			int num = 100;
			while (e != null && ex == null && --num > 0)
			{
				ex = (e as TlsFailureException);
				e = e.InnerException;
			}
			this.Diag.Assert(num > 0);
			if (ex != null)
			{
				this.Diag.Trace("LogTlsError: {0}", new object[]
				{
					ex
				});
				string text = CommonUtil.ToEventLogString(UcmaUtils.GetTlsError(ex));
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_PlatformTlsException, null, new object[]
				{
					base.CallId,
					text
				});
			}
		}

		// Token: 0x060000BB RID: 187 RVA: 0x000046A0 File Offset: 0x000028A0
		private bool ShouldLog(Exception e)
		{
			bool flag = true;
			if ((e is InvalidOperationException || e is OperationFailureException) && this.IsInvalidOperationExplainable)
			{
				flag = false;
			}
			if (!flag)
			{
				this.Diag.Trace("Not logging {0}", new object[]
				{
					e
				});
			}
			return flag;
		}

		// Token: 0x060000BC RID: 188 RVA: 0x000046E9 File Offset: 0x000028E9
		private void TeardownUcmaObjects()
		{
			this.Diag.Trace("TeardownUcmaObjects", new object[0]);
			if (this.Ucma != null)
			{
				this.Ucma.Dispose();
				this.Ucma = null;
			}
		}

		// Token: 0x060000BD RID: 189 RVA: 0x0000471B File Offset: 0x0000291B
		private void TeardownSubscriber()
		{
			this.Diag.Trace("TeardownSubscriber", new object[0]);
			if (this.Subscriber != null)
			{
				this.Subscriber.Dispose();
				this.Subscriber = null;
			}
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00004750 File Offset: 0x00002950
		private void TeardownEvents()
		{
			this.Diag.Trace("TeardownEvents", new object[0]);
			this.OnCallConnected = null;
			this.OnCancelled = null;
			this.OnComplete = null;
			this.OnDispose = null;
			this.OnDtmf = null;
			this.OnError = null;
			this.OnFaxRequestReceive = null;
			this.OnHangup = null;
			this.OnHeavyBlockingOperation = null;
			this.OnMessageReceived = null;
			this.OnMessageSent = null;
			this.OnOutboundCallRequestCompleted = null;
			this.OnSpeech = null;
			this.OnStateInfoSent = null;
			this.OnTimeout = null;
			this.OnTransferComplete = null;
			this.OnHold = null;
			this.OnResume = null;
		}

		// Token: 0x060000BF RID: 191 RVA: 0x000047F4 File Offset: 0x000029F4
		private void TeardownCallState()
		{
			this.Diag.Trace("TeardownCallState", new object[0]);
			if (this.CurrentState != null)
			{
				this.CurrentState.Dispose();
			}
			this.CallInfo = null;
			this.ConsecutiveErrors.Clear();
			this.ToneAccumulator.Clear();
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00004847 File Offset: 0x00002A47
		private void DisposeLoggingManager()
		{
			this.Diag.Trace("DisposeLoggingManager", new object[0]);
			if (this.loggingManager != null)
			{
				this.loggingManager.Dispose();
			}
		}

		// Token: 0x04000014 RID: 20
		private const int MaxConsecutiveErrors = 5;

		// Token: 0x04000015 RID: 21
		public static readonly SpeechAudioFormatInfo SpeechAudioFormatInfo = new SpeechAudioFormatInfo(8000, 16, 1);

		// Token: 0x04000016 RID: 22
		private UMLoggingManager loggingManager;

		// Token: 0x04000017 RID: 23
		private string activityState;

		// Token: 0x0200000B RID: 11
		internal enum TestInfoHandling
		{
			// Token: 0x0400003A RID: 58
			HandleNow,
			// Token: 0x0400003B RID: 59
			StateNotMatch,
			// Token: 0x0400003C RID: 60
			FeatureDisabled
		}

		// Token: 0x0200000C RID: 12
		internal abstract class TestInfoEvent
		{
			// Token: 0x060000C2 RID: 194
			public abstract UcmaCallSession.TestInfoHandling CanHandle(UcmaCallSession session);

			// Token: 0x060000C3 RID: 195
			public abstract void ProcessEvent(UcmaCallSession session);
		}

		// Token: 0x0200000D RID: 13
		internal abstract class BaseTestInfoEvent : UcmaCallSession.TestInfoEvent
		{
			// Token: 0x060000C5 RID: 197 RVA: 0x00004890 File Offset: 0x00002A90
			public override UcmaCallSession.TestInfoHandling CanHandle(UcmaCallSession session)
			{
				if (!this.IsRequiredFeatureEnabled(session))
				{
					session.Diag.Trace("TestInfo feature {0} is not enabled", new object[]
					{
						this.requiredFeatures
					});
					return UcmaCallSession.TestInfoHandling.FeatureDisabled;
				}
				bool flag = false;
				if (this.requiredStateType.Length == 0)
				{
					flag = true;
				}
				foreach (Type left in this.requiredStateType)
				{
					if (left == session.CurrentState.GetType())
					{
						flag = true;
					}
				}
				if (flag)
				{
					return UcmaCallSession.TestInfoHandling.HandleNow;
				}
				session.Diag.Trace("TestInfo state {0} is not in correct state", new object[]
				{
					session.CurrentState
				});
				return UcmaCallSession.TestInfoHandling.StateNotMatch;
			}

			// Token: 0x060000C6 RID: 198 RVA: 0x00004940 File Offset: 0x00002B40
			protected T ForceCreate<T>(params object[] args)
			{
				object obj = typeof(T).InvokeMember(".ctor", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.CreateInstance, null, null, args);
				return (T)((object)obj);
			}

			// Token: 0x060000C7 RID: 199 RVA: 0x00004970 File Offset: 0x00002B70
			protected bool IsRequiredFeatureEnabled(UcmaCallSession session)
			{
				return session.TestInfo.IsFeatureEnabled(this.requiredFeatures);
			}

			// Token: 0x0400003D RID: 61
			protected Type[] requiredStateType = new Type[0];

			// Token: 0x0400003E RID: 62
			protected UcmaCallSession.TestInfoFeatures requiredFeatures;
		}

		// Token: 0x0200000E RID: 14
		internal static class TestInfoEventFactory
		{
			// Token: 0x060000C9 RID: 201 RVA: 0x0000499C File Offset: 0x00002B9C
			public static UcmaCallSession.TestInfoEvent CreateFromSipInfo(InfoMessage message)
			{
				if (message == null || message.Body == null)
				{
					return null;
				}
				string @string = Encoding.UTF8.GetString(message.Body);
				if (string.IsNullOrEmpty(@string))
				{
					return null;
				}
				string[] array = @string.Split("\r\n".ToCharArray(), 2);
				if (array.Length != 2 || array[0] != "TEST-INFO")
				{
					return null;
				}
				string[] array2 = array[1].Split("\r\n".ToCharArray(), 2);
				if (array2.Length != 2)
				{
					return null;
				}
				string text = array2[0];
				string text2 = array2[1];
				string a;
				if ((a = text) != null)
				{
					if (a == "Initialize")
					{
						return new UcmaCallSession.TestInfoEventFactory.InitializeTestInfoEvent(text2);
					}
					if (a == "PlayDtmf")
					{
						return new UcmaCallSession.TestInfoEventFactory.PlayDtmfTestInfoEvent(text2);
					}
					if (a == "PlayAudio")
					{
						return new UcmaCallSession.TestInfoEventFactory.PlayAudioTestInfoEvent(text2);
					}
					if (a == "RecoEmulate")
					{
						return new UcmaCallSession.TestInfoEventFactory.RecoEmulateTestInfoEvent(text2);
					}
				}
				return null;
			}

			// Token: 0x0200000F RID: 15
			private class InitializeTestInfoEvent : UcmaCallSession.BaseTestInfoEvent
			{
				// Token: 0x060000CA RID: 202 RVA: 0x00004A80 File Offset: 0x00002C80
				public InitializeTestInfoEvent(string features)
				{
					this.requiredFeatures = UcmaCallSession.TestInfoFeatures.None;
					this.requiredStateType = new Type[0];
					this.featureList = features.Split(new char[]
					{
						','
					});
				}

				// Token: 0x060000CB RID: 203 RVA: 0x00004AC0 File Offset: 0x00002CC0
				public override void ProcessEvent(UcmaCallSession session)
				{
					string[] array = this.featureList;
					int i = 0;
					while (i < array.Length)
					{
						string text = array[i];
						string a;
						if ((a = text.Trim()) == null)
						{
							goto IL_8A;
						}
						if (!(a == "PlayDtmf"))
						{
							if (!(a == "PlayAudio"))
							{
								if (!(a == "RecoEmulate"))
								{
									if (!(a == "SkipPrompt"))
									{
										goto IL_8A;
									}
									session.TestInfo.EnableFeature(UcmaCallSession.TestInfoFeatures.SkipPrompt);
								}
								else
								{
									session.TestInfo.EnableFeature(UcmaCallSession.TestInfoFeatures.RecoEmulate);
								}
							}
							else
							{
								session.TestInfo.EnableFeature(UcmaCallSession.TestInfoFeatures.PlayAudio);
							}
						}
						else
						{
							session.TestInfo.EnableFeature(UcmaCallSession.TestInfoFeatures.PlayDtmf);
						}
						IL_A9:
						i++;
						continue;
						IL_8A:
						session.Diag.Trace("Unknown Test Info Feature requested {0}", new object[]
						{
							text
						});
						goto IL_A9;
					}
				}

				// Token: 0x0400003F RID: 63
				private string[] featureList;
			}

			// Token: 0x02000010 RID: 16
			private class PlayDtmfTestInfoEvent : UcmaCallSession.BaseTestInfoEvent
			{
				// Token: 0x060000CC RID: 204 RVA: 0x00004B84 File Offset: 0x00002D84
				public PlayDtmfTestInfoEvent(string dtmf)
				{
					this.requiredFeatures = UcmaCallSession.TestInfoFeatures.PlayDtmf;
					this.requiredStateType = new Type[]
					{
						typeof(UcmaCallSession.PlayPromptsAndRecoEmulateSessionState),
						typeof(UcmaCallSession.PlayPromptsAndRecoDtmfSessionState),
						typeof(UcmaCallSession.RecordFileSessionState)
					};
					this.dtmf = dtmf;
				}

				// Token: 0x060000CD RID: 205 RVA: 0x00004BDC File Offset: 0x00002DDC
				public override void ProcessEvent(UcmaCallSession session)
				{
					foreach (char c in this.dtmf)
					{
						ToneControllerEventArgs e = base.ForceCreate<ToneControllerEventArgs>(new object[]
						{
							UcmaCallSession.CharToToneId(c),
							100f
						});
						session.CurrentState.ToneController_ToneReceived(this, e);
					}
				}

				// Token: 0x04000040 RID: 64
				private readonly string dtmf;
			}

			// Token: 0x02000011 RID: 17
			private class PlayAudioTestInfoEvent : UcmaCallSession.BaseTestInfoEvent
			{
				// Token: 0x060000CE RID: 206 RVA: 0x00004C44 File Offset: 0x00002E44
				public PlayAudioTestInfoEvent(string wave)
				{
					this.requiredFeatures = UcmaCallSession.TestInfoFeatures.PlayAudio;
					this.requiredStateType = new Type[]
					{
						typeof(UcmaCallSession.PlayPromptsAndRecoEmulateSessionState),
						typeof(UcmaCallSession.RecordFileSessionState)
					};
					this.wave = wave;
				}

				// Token: 0x060000CF RID: 207 RVA: 0x00004C90 File Offset: 0x00002E90
				public override void ProcessEvent(UcmaCallSession session)
				{
					if (session.CurrentState is UcmaCallSession.PlayPromptsAndRecoEmulateSessionState)
					{
						UcmaCallSession.PlayPromptsAndRecoEmulateSessionState playPromptsAndRecoEmulateSessionState = session.CurrentState as UcmaCallSession.PlayPromptsAndRecoEmulateSessionState;
						playPromptsAndRecoEmulateSessionState.RecognizeWave(this.wave);
						return;
					}
					if (session.CurrentState is UcmaCallSession.RecordFileSessionState)
					{
						UcmaCallSession.RecordFileSessionState recordFileSessionState = session.CurrentState as UcmaCallSession.RecordFileSessionState;
						recordFileSessionState.TestInfoRecordedFileName = this.wave;
						return;
					}
					session.Diag.Assert(false, "Unexpected state {0} in {1}", new object[]
					{
						session.CurrentState.Name,
						"PlayAudioTestInfoEvent.ProcessEvent"
					});
				}

				// Token: 0x04000041 RID: 65
				private readonly string wave;
			}

			// Token: 0x02000012 RID: 18
			private class RecoEmulateTestInfoEvent : UcmaCallSession.BaseTestInfoEvent
			{
				// Token: 0x060000D0 RID: 208 RVA: 0x00004D18 File Offset: 0x00002F18
				public RecoEmulateTestInfoEvent(string text)
				{
					this.requiredFeatures = UcmaCallSession.TestInfoFeatures.RecoEmulate;
					this.requiredStateType = new Type[]
					{
						typeof(UcmaCallSession.PlayPromptsAndRecoEmulateSessionState)
					};
					this.text = text;
				}

				// Token: 0x060000D1 RID: 209 RVA: 0x00004D54 File Offset: 0x00002F54
				public override void ProcessEvent(UcmaCallSession session)
				{
					if (session.CurrentState is UcmaCallSession.PlayPromptsAndRecoEmulateSessionState)
					{
						UcmaCallSession.PlayPromptsAndRecoEmulateSessionState playPromptsAndRecoEmulateSessionState = session.CurrentState as UcmaCallSession.PlayPromptsAndRecoEmulateSessionState;
						playPromptsAndRecoEmulateSessionState.EmulateWave(this.text);
						return;
					}
					session.Diag.Assert(false, "Unexpected state {0} in {1}", new object[]
					{
						session.CurrentState.Name,
						"RecoSimTestInfoEvent.ProcessEvent"
					});
				}

				// Token: 0x04000042 RID: 66
				private readonly string text;
			}
		}

		// Token: 0x02000013 RID: 19
		[Flags]
		internal enum TestInfoFeatures
		{
			// Token: 0x04000044 RID: 68
			None = 0,
			// Token: 0x04000045 RID: 69
			PlayDtmf = 1,
			// Token: 0x04000046 RID: 70
			PlayAudio = 2,
			// Token: 0x04000047 RID: 71
			RecoEmulate = 4,
			// Token: 0x04000048 RID: 72
			SkipPrompt = 8,
			// Token: 0x04000049 RID: 73
			All = 1023
		}

		// Token: 0x02000014 RID: 20
		internal class TestInfoManager
		{
			// Token: 0x1700001F RID: 31
			// (get) Token: 0x060000D2 RID: 210 RVA: 0x00004DB6 File Offset: 0x00002FB6
			public bool TestInfoEnabled
			{
				get
				{
					return Utils.RunningInTestMode && (this.features & UcmaCallSession.TestInfoFeatures.All) != UcmaCallSession.TestInfoFeatures.None;
				}
			}

			// Token: 0x060000D3 RID: 211 RVA: 0x00004DD3 File Offset: 0x00002FD3
			public TestInfoManager(UcmaCallSession session)
			{
				this.session = session;
				this.features = UcmaCallSession.TestInfoFeatures.None;
			}

			// Token: 0x060000D4 RID: 212 RVA: 0x00004E00 File Offset: 0x00003000
			public void EnableFeature(UcmaCallSession.TestInfoFeatures feature)
			{
				this.session.Diag.Trace("Feature {0} Enabled", new object[]
				{
					this.features.ToString()
				});
				this.features |= feature;
			}

			// Token: 0x060000D5 RID: 213 RVA: 0x00004E4C File Offset: 0x0000304C
			public void DisableFeature(UcmaCallSession.TestInfoFeatures feature)
			{
				this.session.Diag.Trace("Feature {0} Disabled", new object[]
				{
					this.features.ToString()
				});
				this.features &= ~feature;
			}

			// Token: 0x060000D6 RID: 214 RVA: 0x00004E98 File Offset: 0x00003098
			public bool IsFeatureEnabled(UcmaCallSession.TestInfoFeatures feature)
			{
				return Utils.RunningInTestMode && (this.features & feature) == feature;
			}

			// Token: 0x060000D7 RID: 215 RVA: 0x00004EB0 File Offset: 0x000030B0
			public void QueueEvent(UcmaCallSession.TestInfoEvent testInfoEvent)
			{
				lock (this.lockObject)
				{
					this.testInfoQueue.Enqueue(testInfoEvent);
				}
			}

			// Token: 0x060000D8 RID: 216 RVA: 0x00004EF8 File Offset: 0x000030F8
			public void ProcessQueue()
			{
				lock (this.lockObject)
				{
					while (this.testInfoQueue.Count > 0)
					{
						UcmaCallSession.TestInfoHandling testInfoHandling = this.testInfoQueue.Peek().CanHandle(this.session);
						if (testInfoHandling == UcmaCallSession.TestInfoHandling.HandleNow)
						{
							this.testInfoQueue.Dequeue().ProcessEvent(this.session);
						}
						else if (testInfoHandling == UcmaCallSession.TestInfoHandling.FeatureDisabled)
						{
							this.testInfoQueue.Dequeue();
						}
						else if (testInfoHandling == UcmaCallSession.TestInfoHandling.StateNotMatch)
						{
							break;
						}
					}
				}
			}

			// Token: 0x0400004A RID: 74
			private Queue<UcmaCallSession.TestInfoEvent> testInfoQueue = new Queue<UcmaCallSession.TestInfoEvent>();

			// Token: 0x0400004B RID: 75
			private object lockObject = new object();

			// Token: 0x0400004C RID: 76
			private UcmaCallSession session;

			// Token: 0x0400004D RID: 77
			private UcmaCallSession.TestInfoFeatures features;
		}

		// Token: 0x02000015 RID: 21
		internal class UcmaObjects : DisposableBase
		{
			// Token: 0x060000D9 RID: 217 RVA: 0x00004F8C File Offset: 0x0000318C
			public UcmaObjects(UcmaCallSession session, AudioVideoCall call)
			{
				ValidateArgument.NotNull(session, "session");
				ValidateArgument.NotNull(call, "call");
				this.session = session;
				this.call = call;
			}

			// Token: 0x17000020 RID: 32
			// (get) Token: 0x060000DA RID: 218 RVA: 0x00004FB8 File Offset: 0x000031B8
			public AudioVideoCall Call
			{
				get
				{
					return this.call;
				}
			}

			// Token: 0x17000021 RID: 33
			// (get) Token: 0x060000DB RID: 219 RVA: 0x00004FC0 File Offset: 0x000031C0
			public AudioVideoFlow Flow
			{
				get
				{
					return this.call.Flow;
				}
			}

			// Token: 0x17000022 RID: 34
			// (get) Token: 0x060000DC RID: 220 RVA: 0x00004FCD File Offset: 0x000031CD
			public PromptPlayer Player
			{
				get
				{
					return this.player;
				}
			}

			// Token: 0x17000023 RID: 35
			// (get) Token: 0x060000DD RID: 221 RVA: 0x00004FD5 File Offset: 0x000031D5
			public Recorder MediaRecorder
			{
				get
				{
					return this.mediaRecorder;
				}
			}

			// Token: 0x17000024 RID: 36
			// (get) Token: 0x060000DE RID: 222 RVA: 0x00004FDD File Offset: 0x000031DD
			public ToneController ToneController
			{
				get
				{
					return this.toneController;
				}
			}

			// Token: 0x17000025 RID: 37
			// (get) Token: 0x060000DF RID: 223 RVA: 0x00004FE5 File Offset: 0x000031E5
			public SpeechRecognitionEngine SpeechReco
			{
				get
				{
					return this.speechReco;
				}
			}

			// Token: 0x17000026 RID: 38
			// (get) Token: 0x060000E0 RID: 224 RVA: 0x00004FF0 File Offset: 0x000031F0
			public bool MediaDropped
			{
				get
				{
					bool flag = this.connectorShouldBeActive && !this.speechRecoConnector.IsActive;
					bool flag2 = this.player != null && this.player.MediaDropped;
					bool flag3 = flag || flag2;
					if (flag3)
					{
						this.session.Diag.Trace("Media was dropped!  reco='{0}', player='{1}'", new object[]
						{
							flag,
							flag2
						});
					}
					return flag3;
				}
			}

			// Token: 0x060000E1 RID: 225 RVA: 0x0000506B File Offset: 0x0000326B
			public void ActivateFlow()
			{
				this.session.Diag.Assert(!this.flowActivated, "Flow has already been activated!");
				this.flowActivated = true;
				this.ActivatePlayer();
				this.ActivateRecorder();
				this.ActivateToneController();
			}

			// Token: 0x060000E2 RID: 226 RVA: 0x000050A4 File Offset: 0x000032A4
			public void EnsureRecognition(CultureInfo culture)
			{
				this.EnsureSpeechRecognizer(culture);
				this.EnsureSpeechRecognitionConnector();
				this.EnsureSpeechRecognitionStream();
			}

			// Token: 0x060000E3 RID: 227 RVA: 0x000050B9 File Offset: 0x000032B9
			public void HaltRecognition()
			{
				this.connectorShouldBeActive = false;
				if (this.speechRecoConnector != null)
				{
					this.speechRecoConnector.Stop();
				}
				if (this.speechReco != null)
				{
					this.speechReco.RecognizeAsyncCancel();
				}
			}

			// Token: 0x060000E4 RID: 228 RVA: 0x000050E8 File Offset: 0x000032E8
			public void Pause()
			{
				if (this.Player != null)
				{
					this.Player.Pause();
				}
				if (this.MediaRecorder != null && this.MediaRecorder.State == null)
				{
					this.MediaRecorder.Pause();
				}
			}

			// Token: 0x060000E5 RID: 229 RVA: 0x0000511D File Offset: 0x0000331D
			public void Resume()
			{
				if (this.Player != null)
				{
					this.Player.Resume();
				}
				if (this.MediaRecorder != null && this.MediaRecorder.State == 2)
				{
					this.MediaRecorder.Start();
				}
			}

			// Token: 0x060000E6 RID: 230 RVA: 0x00005153 File Offset: 0x00003353
			protected override DisposeTracker InternalGetDisposeTracker()
			{
				return DisposeTracker.Get<UcmaCallSession.UcmaObjects>(this);
			}

			// Token: 0x060000E7 RID: 231 RVA: 0x0000515C File Offset: 0x0000335C
			protected override void InternalDispose(bool disposing)
			{
				if (disposing)
				{
					if (this.speechRecoConnector != null)
					{
						this.speechRecoConnector.Dispose();
					}
					if (this.speechReco != null)
					{
						this.speechReco.Dispose();
					}
					if (this.speechRecoStream != null)
					{
						this.speechRecoStream.Dispose();
					}
					if (this.player != null)
					{
						this.player.Dispose();
					}
					this.call = null;
					this.player = null;
					this.mediaRecorder = null;
					this.speechReco = null;
					this.speechRecoConnector = null;
					this.speechRecoStream = null;
					this.toneController = null;
				}
			}

			// Token: 0x060000E8 RID: 232 RVA: 0x000051E9 File Offset: 0x000033E9
			private void ActivatePlayer()
			{
				this.player = new PromptPlayer(this.session);
				this.player.AttachFlow(this.Flow);
				this.session.Subscriber.SubscribeTo(this.player);
			}

			// Token: 0x060000E9 RID: 233 RVA: 0x00005223 File Offset: 0x00003423
			private void ActivateRecorder()
			{
				this.mediaRecorder = new Recorder();
				this.mediaRecorder.AttachFlow(this.Flow);
				this.session.Subscriber.SubscribeTo(this.mediaRecorder);
			}

			// Token: 0x060000EA RID: 234 RVA: 0x00005257 File Offset: 0x00003457
			private void ActivateToneController()
			{
				this.toneController = new ToneController();
				this.toneController.AttachFlow(this.Flow);
				this.session.Subscriber.SubscribeTo(this.toneController);
			}

			// Token: 0x060000EB RID: 235 RVA: 0x0000528B File Offset: 0x0000348B
			private void EnsureSpeechRecognitionConnector()
			{
				if (this.speechRecoConnector == null)
				{
					this.speechRecoConnector = new SpeechRecognitionConnector();
					this.speechRecoConnector.AttachFlow(this.Flow);
				}
			}

			// Token: 0x060000EC RID: 236 RVA: 0x000052B4 File Offset: 0x000034B4
			private void EnsureSpeechRecognizer(CultureInfo culture)
			{
				if (this.SpeechReco != null && this.speechReco.RecognizerInfo.Culture.LCID != culture.LCID)
				{
					this.session.Diag.Trace("Language changed from {0} to {1}, discarding old engine", new object[]
					{
						this.speechReco.RecognizerInfo.Culture.Name,
						culture.Name
					});
					this.speechReco.Dispose();
					this.speechReco = null;
				}
				if (this.speechReco == null)
				{
					this.session.Diag.Trace("Changing culture to {0}, creating new engine", new object[]
					{
						culture.Name
					});
					this.speechReco = new SpeechRecognitionEngine(UcmaInstalledRecognizers.GetRecognizerId(SpeechRecognitionEngineType.CmdAndControl, culture));
					this.speechReco.UpdateRecognizerSetting("AssumeCFGFromTrustedSource", 1);
					this.session.Subscriber.SubscribeTo(this.speechReco);
					this.session.Diag.Assert(culture.LCID == this.speechReco.RecognizerInfo.Culture.LCID, "Reco culture cannot be changed requested {0}, existing {1}", new object[]
					{
						culture.LCID,
						this.speechReco.RecognizerInfo.Culture.LCID
					});
				}
			}

			// Token: 0x060000ED RID: 237 RVA: 0x00005408 File Offset: 0x00003608
			private void EnsureSpeechRecognitionStream()
			{
				this.connectorShouldBeActive = true;
				if (!this.speechRecoConnector.IsActive)
				{
					if (this.speechRecoStream != null)
					{
						this.speechRecoStream.Dispose();
					}
					this.speechRecoStream = this.speechRecoConnector.Start();
					this.speechReco.SetInputToAudioStream(this.speechRecoStream, UcmaCallSession.SpeechAudioFormatInfo);
				}
			}

			// Token: 0x0400004E RID: 78
			private AudioVideoCall call;

			// Token: 0x0400004F RID: 79
			private PromptPlayer player;

			// Token: 0x04000050 RID: 80
			private Recorder mediaRecorder;

			// Token: 0x04000051 RID: 81
			private SpeechRecognitionEngine speechReco;

			// Token: 0x04000052 RID: 82
			private SpeechRecognitionConnector speechRecoConnector;

			// Token: 0x04000053 RID: 83
			private ToneController toneController;

			// Token: 0x04000054 RID: 84
			private UcmaCallSession session;

			// Token: 0x04000055 RID: 85
			private Stream speechRecoStream;

			// Token: 0x04000056 RID: 86
			private bool connectorShouldBeActive;

			// Token: 0x04000057 RID: 87
			private bool flowActivated;
		}

		// Token: 0x02000016 RID: 22
		private abstract class SessionState : DisposableBase
		{
			// Token: 0x060000EE RID: 238 RVA: 0x00005463 File Offset: 0x00003663
			public SessionState(UcmaCallSession session)
			{
				this.Session = session;
				this.Args = new UMCallSessionEventArgs();
			}

			// Token: 0x17000027 RID: 39
			// (get) Token: 0x060000EF RID: 239 RVA: 0x0000547D File Offset: 0x0000367D
			public bool IsWaitingForAsyncCompletion
			{
				get
				{
					return this.waiting != UcmaCallSession.SessionState.AsyncOperation.None;
				}
			}

			// Token: 0x17000028 RID: 40
			// (get) Token: 0x060000F0 RID: 240
			public abstract string Name { get; }

			// Token: 0x17000029 RID: 41
			// (get) Token: 0x060000F1 RID: 241 RVA: 0x0000548B File Offset: 0x0000368B
			public DiagnosticHelper Diag
			{
				get
				{
					return this.Session.Diag;
				}
			}

			// Token: 0x1700002A RID: 42
			// (get) Token: 0x060000F2 RID: 242 RVA: 0x00005498 File Offset: 0x00003698
			public virtual bool IsIdle
			{
				get
				{
					return !this.IsWaitingForAsyncCompletion;
				}
			}

			// Token: 0x1700002B RID: 43
			// (get) Token: 0x060000F3 RID: 243 RVA: 0x000054A3 File Offset: 0x000036A3
			// (set) Token: 0x060000F4 RID: 244 RVA: 0x000054AB File Offset: 0x000036AB
			private protected UcmaCallSession Session { protected get; private set; }

			// Token: 0x1700002C RID: 44
			// (get) Token: 0x060000F5 RID: 245 RVA: 0x000054B4 File Offset: 0x000036B4
			// (set) Token: 0x060000F6 RID: 246 RVA: 0x000054BC File Offset: 0x000036BC
			private protected UMCallSessionEventArgs Args { protected get; private set; }

			// Token: 0x1700002D RID: 45
			// (get) Token: 0x060000F7 RID: 247 RVA: 0x000054C5 File Offset: 0x000036C5
			protected UcmaCallSession.SubscriptionHelper EventSubscriber
			{
				get
				{
					return this.Session.Subscriber;
				}
			}

			// Token: 0x060000F8 RID: 248 RVA: 0x000054D4 File Offset: 0x000036D4
			public void Start()
			{
				this.Diag.Trace("Starting session state {0}", new object[]
				{
					this.Name
				});
				this.InternalStart();
				this.Session.TestInfo.ProcessQueue();
			}

			// Token: 0x060000F9 RID: 249 RVA: 0x00005518 File Offset: 0x00003718
			public void Cancel(bool fireOnCancelled)
			{
				if (fireOnCancelled)
				{
					this.completion = UcmaCallSession.SessionState.CompletionReason.Cancel;
				}
				if (this.IsIdle)
				{
					this.CompleteState();
					return;
				}
				this.InternalCancel();
			}

			// Token: 0x060000FA RID: 250 RVA: 0x00005539 File Offset: 0x00003739
			public void CompleteAsyncCallback()
			{
				if (!this.IsWaitingForAsyncCompletion)
				{
					this.CompleteState();
				}
			}

			// Token: 0x060000FB RID: 251 RVA: 0x0000554C File Offset: 0x0000374C
			public void FireError(Exception e)
			{
				this.Args.Error = e;
				this.Session.ConsecutiveErrors.Add(this.Args.Error);
				try
				{
					if (!this.IsIdle)
					{
						this.Cancel(false);
					}
					else
					{
						this.Diag.Assert(!this.IsWaitingForAsyncCompletion, "Idle, yet waiting!");
						this.Session.ChangeState(new UcmaCallSession.ErrorSessionState(this.Session, e));
						this.Session.ConsecutiveErrors.Clear();
						this.Diag.Trace("Successfully processed OnError", new object[0]);
					}
				}
				finally
				{
					if (this.Session.ConsecutiveErrors.Count > 5)
					{
						this.DropCallDueToConsecutiveErrors();
					}
				}
			}

			// Token: 0x060000FC RID: 252 RVA: 0x00005614 File Offset: 0x00003814
			public virtual void Call_EstablishCompleted(IAsyncResult r)
			{
				this.Diag.Assert(false, "Call_EstablishCompleted unhandled");
			}

			// Token: 0x060000FD RID: 253 RVA: 0x00005627 File Offset: 0x00003827
			public virtual void Call_AudioVideoFlowConfigurationRequested(object sender, AudioVideoFlowConfigurationRequestedEventArgs e)
			{
				this.Diag.Trace("Call_AudioVideoFlowConfigurationRequested unhandled", new object[0]);
			}

			// Token: 0x060000FE RID: 254 RVA: 0x00005640 File Offset: 0x00003840
			public virtual void Call_MediaTroubleshootingDataReported(object sender, MediaTroubleshootingDataReportedEventArgs e)
			{
				this.Diag.Trace("MediaTroubleshootingDataReportedEventArgs:  QualityOfExperienceContent: {0}", new object[]
				{
					e.QualityOfExperienceContent
				});
				if (e.QualityOfExperienceContent != null)
				{
					AudioQuality audioQuality;
					string text;
					if (AudioQuality.TryParseAudioQuality(e.QualityOfExperienceContent.GetBody(), ref audioQuality, ref text))
					{
						this.Diag.Trace("AudioQuality metrics: {0}", new object[]
						{
							audioQuality
						});
						this.Diag.Assert(this.Session.CurrentCallContext != null, "Call context already disposed of");
						this.Session.CurrentCallContext.CallLoggingHelper.AudioMetrics = audioQuality;
					}
					else
					{
						this.Diag.Trace("AudioQuality Validation Errors: {0}", new object[]
						{
							text
						});
					}
				}
				if (e.MediaChannelEstablishmentDataCollection != null && e.MediaChannelEstablishmentDataCollection.Count > 0 && this.Session.CurrentCallContext.DialPlan != null && this.Session.CurrentCallContext.DialPlan.URIType == UMUriType.SipName)
				{
					UcmaVoipPlatform.HandlePossibleMRASIssues(e.MediaChannelEstablishmentDataCollection[0]);
				}
			}

			// Token: 0x060000FF RID: 255 RVA: 0x00005754 File Offset: 0x00003954
			public virtual void Call_StateChanged(object sender, CallStateChangedEventArgs e)
			{
				this.Diag.Trace(UcmaCallSession.FormatStateChange<CallState>("Call", e.PreviousState, e.State), new object[0]);
				this.Diag.Trace("Reason: {0}", new object[]
				{
					e.TransitionReason
				});
				this.Session.CallStateTransitionReason = e.TransitionReason;
				this.Session.LastCallStateChangeProcessed = e.State;
				if ((!(this.Session is UcmaDependentCallSession) || !((UcmaDependentCallSession)this.Session).IgnoreBye) && this.Session.IsSignalingTerminatingOrTerminated)
				{
					if (e.State == 8 && this.IsWaitingFor(UcmaCallSession.SessionState.AsyncOperation.TerminatedOrFax))
					{
						this.completion = UcmaCallSession.SessionState.CompletionReason.Teardown;
						this.StopWaitFor(UcmaCallSession.SessionState.AsyncOperation.TerminatedOrFax);
					}
					else if (e.TransitionReason == 8 || e.TransitionReason == 9)
					{
						this.completion = UcmaCallSession.SessionState.CompletionReason.Hangup;
					}
					this.Cancel(false);
				}
			}

			// Token: 0x06000100 RID: 256 RVA: 0x0000584C File Offset: 0x00003A4C
			public virtual void Call_InfoReceived(object sender, MessageReceivedEventArgs e)
			{
				int num = 500;
				string text = null;
				try
				{
					InfoMessage infoMessage = new InfoMessage
					{
						ContentType = e.ContentType,
						Body = e.GetBody()
					};
					foreach (SignalingHeader signalingHeader in e.RequestData.SignalingHeaders)
					{
						text = signalingHeader.Name;
						string value = signalingHeader.GetValue();
						if (!infoMessage.Headers.ContainsKey(text))
						{
							infoMessage.Headers.Add(text, value);
						}
					}
					UcmaCallSession.TestInfoEvent testInfoEvent = UcmaCallSession.TestInfoEventFactory.CreateFromSipInfo(infoMessage);
					if (testInfoEvent != null)
					{
						this.Session.TestInfo.QueueEvent(testInfoEvent);
					}
					else
					{
						InfoMessage.MessageReceivedEventArgs args = new InfoMessage.MessageReceivedEventArgs(infoMessage);
						this.Session.ConditionalUpdateCallerId(args);
						this.Session.Fire<InfoMessage.MessageReceivedEventArgs>(this.Session.OnMessageReceived, args);
					}
					this.Session.TestInfo.ProcessQueue();
					num = 200;
				}
				catch (MessageParsingException ex)
				{
					this.Diag.Trace("Ignoring invalid SIP INFO message header: {0}.  {1}", new object[]
					{
						text,
						ex
					});
					num = 400;
				}
				finally
				{
					e.SendResponse(num);
				}
			}

			// Token: 0x06000101 RID: 257 RVA: 0x000059D0 File Offset: 0x00003BD0
			public virtual void Call_RemoteParticipantChanged(object sender, RemoteParticipantChangedEventArgs e)
			{
				if (this.Session.CallInfo != null && this.Session.CallInfo.IsInbound)
				{
					PlatformTelephonyAddress callerAddress;
					try
					{
						callerAddress = new PlatformTelephonyAddress(e.NewParticipant.DisplayName, new UcmaSipUri(e.NewParticipant.Uri));
					}
					catch (ArgumentException ex)
					{
						this.Diag.Trace("Ignoring SIP UPDATE for invalid remote participant uri: {0}. error ={1}", new object[]
						{
							e.NewParticipant.Uri,
							ex
						});
						return;
					}
					this.Session.UpdateCallerId(callerAddress);
				}
			}

			// Token: 0x06000102 RID: 258 RVA: 0x00005A68 File Offset: 0x00003C68
			public virtual void Call_AcceptCompleted(IAsyncResult r)
			{
				this.Diag.Assert(false, "Call_AcceptCompleted Unhandled");
			}

			// Token: 0x06000103 RID: 259 RVA: 0x00005A7B File Offset: 0x00003C7B
			public virtual void Call_SendInfoCompleted(IAsyncResult r)
			{
				this.StopWaitFor(UcmaCallSession.SessionState.AsyncOperation.SendMessageComplete);
				this.Session.Ucma.Call.EndSendMessage(r);
				this.Session.Fire<EventArgs>(this.Session.OnMessageSent, null);
			}

			// Token: 0x06000104 RID: 260 RVA: 0x00005AB6 File Offset: 0x00003CB6
			public virtual void Call_SendPromptInfoCompleted(IAsyncResult r)
			{
				this.Diag.Assert(false, "Call_SendPromptInfoCompleted Unhandled");
			}

			// Token: 0x06000105 RID: 261 RVA: 0x00005AC9 File Offset: 0x00003CC9
			public virtual void Call_TerminateCompleted(IAsyncResult r)
			{
				this.Diag.Assert(false, "Call_TerminateCompleted Unhandled");
			}

			// Token: 0x06000106 RID: 262 RVA: 0x00005ADC File Offset: 0x00003CDC
			public virtual void Call_TransferCompleted(IAsyncResult r)
			{
				this.Diag.Assert(false, "Call_TransferCompleted Unhandled");
			}

			// Token: 0x06000107 RID: 263 RVA: 0x00005AEF File Offset: 0x00003CEF
			public virtual void Flow_StateChanged(object sender, MediaFlowStateChangedEventArgs e)
			{
				this.Diag.Trace(UcmaCallSession.FormatStateChange<MediaFlowState>("Flow_State", e.PreviousState, e.State), new object[0]);
			}

			// Token: 0x06000108 RID: 264 RVA: 0x00005B18 File Offset: 0x00003D18
			public virtual void Flow_ConfigurationChanged(object sender, AudioVideoFlowConfigurationChangedEventArgs e)
			{
				this.Diag.Trace("Flow_ConfigurationChanged called", new object[0]);
				bool flag = this.callOnHold;
				AudioVideoFlow audioVideoFlow = (AudioVideoFlow)sender;
				if (audioVideoFlow.State != 1)
				{
					this.Diag.Trace("Flow_ConfigurationChanged called when state is {0}", new object[]
					{
						audioVideoFlow.State
					});
					return;
				}
				AudioChannel audioChannel = null;
				if (audioVideoFlow.Audio.GetChannels().TryGetValue(0, out audioChannel))
				{
					this.Diag.Trace("Flow_ConfigurationChanged called: ChannelLabel.AudioMono present", new object[0]);
					if (audioChannel.Direction == 2 || audioChannel.Direction == null)
					{
						if (!flag)
						{
							this.callOnHold = true;
							this.Session.Ucma.Pause();
							this.OnHoldNotify();
							this.Session.Fire<UMCallSessionEventArgs>(this.Session.OnHold, this.Args);
							return;
						}
					}
					else if (flag)
					{
						this.callOnHold = false;
						this.Session.Ucma.Resume();
						this.OnResumeNotify();
						this.Session.Fire<UMCallSessionEventArgs>(this.Session.OnResume, this.Args);
						return;
					}
				}
				else
				{
					this.Diag.Trace("Flow_ConfigurationChanged called: ChannelLabel.AudioMono not present", new object[0]);
				}
			}

			// Token: 0x06000109 RID: 265 RVA: 0x00005C4D File Offset: 0x00003E4D
			public virtual void Player_BookmarkReached(object sender, BookmarkReachedEventArgs e)
			{
				this.Diag.Assert(false, "Player_BookmarkReached Unhandled");
			}

			// Token: 0x0600010A RID: 266 RVA: 0x00005C60 File Offset: 0x00003E60
			public virtual void Player_SpeakCompleted(object sender, PromptPlayer.PlayerCompletedEventArgs e)
			{
				this.Diag.Assert(false, "Player_SpeakCompleted Unhandled");
			}

			// Token: 0x0600010B RID: 267 RVA: 0x00005C74 File Offset: 0x00003E74
			public virtual void SpeechReco_RecognizeCompleted(object sender, RecognizeCompletedEventArgs e)
			{
				this.Diag.Assert(false, "SpeechReco_RecognizeCompleted Unhandled in {0}", new object[]
				{
					this.Name
				});
			}

			// Token: 0x0600010C RID: 268 RVA: 0x00005CA3 File Offset: 0x00003EA3
			public virtual void SpeechReco_SpeechDetected(object sender, SpeechDetectedEventArgs e)
			{
				this.Diag.Trace("Ignoring SpeechReco_SpeechDetected", new object[0]);
			}

			// Token: 0x0600010D RID: 269 RVA: 0x00005CBC File Offset: 0x00003EBC
			public virtual void SpeechReco_SpeechHypothesized(object sender, SpeechHypothesizedEventArgs e)
			{
				this.Diag.Assert(false, "SpeechReco_SpeechHypothesized Unhandled in {0}", new object[]
				{
					this.Name
				});
			}

			// Token: 0x0600010E RID: 270 RVA: 0x00005CEC File Offset: 0x00003EEC
			public virtual void SpeechReco_EmulateRecognizeCompleted(object sender, EmulateRecognizeCompletedEventArgs e)
			{
				this.Diag.Assert(false, "SpeechReco_EmulateRecognizeCompleted Unhandled in {0}", new object[]
				{
					this.Name
				});
			}

			// Token: 0x0600010F RID: 271 RVA: 0x00005D1B File Offset: 0x00003F1B
			public virtual void MediaPlayer_StateChanged(object sender, PlayerStateChangedEventArgs e)
			{
				this.Diag.Assert(false, "MediaPlayer_StateChanged Unhandled");
			}

			// Token: 0x06000110 RID: 272 RVA: 0x00005D2E File Offset: 0x00003F2E
			public virtual void MediaRecorder_StateChanged(object sender, RecorderStateChangedEventArgs e)
			{
				if (this.Session.CommandAndControlLoggingEnabled)
				{
					this.Diag.Trace("Ignoring MediaRecorder_StateChanged", new object[0]);
					return;
				}
				this.Diag.Assert(false, "MediaRecorder_StateChanged Unhandled");
			}

			// Token: 0x06000111 RID: 273 RVA: 0x00005D65 File Offset: 0x00003F65
			public virtual void MediaRecorder_VoiceActivityChanged(object sender, VoiceActivityChangedEventArgs e)
			{
				this.Diag.Trace("Ignoring VoiceActivityChanged event.", new object[0]);
			}

			// Token: 0x06000112 RID: 274 RVA: 0x00005D7D File Offset: 0x00003F7D
			public virtual void ToneController_ToneReceived(object sender, ToneControllerEventArgs e)
			{
				this.Session.ToneAccumulator.Add(UcmaCallSession.ToneToByte((byte)e.Tone));
			}

			// Token: 0x06000113 RID: 275 RVA: 0x00005D9C File Offset: 0x00003F9C
			public virtual void ToneController_IncomingFaxDetected(object sender, IncomingFaxDetectedEventArgs e)
			{
				this.StopWaitFor(UcmaCallSession.SessionState.AsyncOperation.TerminatedOrFax);
				this.completion = UcmaCallSession.SessionState.CompletionReason.Fax;
				this.Session.CurrentCallContext.FaxToneReceived = true;
				this.Args.DtmfDigits = Encoding.ASCII.GetBytes("faxtone");
				this.Cancel(false);
			}

			// Token: 0x06000114 RID: 276 RVA: 0x00005E18 File Offset: 0x00004018
			public virtual void SendMessage(InfoMessage message)
			{
				this.Diag.Assert(!this.IsWaitingFor(UcmaCallSession.SessionState.AsyncOperation.SendMessageComplete), "Cannot send a new INFO message until the previous INFO callback completes.");
				List<SignalingHeader> list = null;
				if (message.Headers != null && message.Headers.Count > 0)
				{
					list = message.Headers.ConvertAll((KeyValuePair<string, string> kvp) => new SignalingHeader(kvp.Key, kvp.Value));
				}
				AsyncCallback asyncCallback = this.EventSubscriber.CreateSerializedAsyncCallback(delegate(IAsyncResult r)
				{
					this.Session.CurrentState.Call_SendInfoCompleted(r);
				}, "Call_SendInfoCompleted");
				CallSendMessageRequestOptions callSendMessageRequestOptions = new CallSendMessageRequestOptions();
				if (list != null)
				{
					CollectionExtensions.AddRange<SignalingHeader>(callSendMessageRequestOptions.Headers, list);
				}
				ContentDescription contentDescription = new ContentDescription(message.ContentType, message.Body);
				this.Session.Ucma.Call.BeginSendMessage(1, contentDescription, callSendMessageRequestOptions, asyncCallback, this);
				this.StartWaitFor(UcmaCallSession.SessionState.AsyncOperation.SendMessageComplete);
			}

			// Token: 0x06000115 RID: 277 RVA: 0x00005EF0 File Offset: 0x000040F0
			public bool ConditionalWaitForTerminatedOrFax()
			{
				if (this.Session.IsMediaTerminatingOrTerminated && this.Session.LastCallStateChangeProcessed != 8 && !this.Session.CurrentCallContext.FaxToneReceived)
				{
					this.StartWaitFor(UcmaCallSession.SessionState.AsyncOperation.TerminatedOrFax);
				}
				return this.IsWaitingFor(UcmaCallSession.SessionState.AsyncOperation.TerminatedOrFax);
			}

			// Token: 0x06000116 RID: 278 RVA: 0x00005F40 File Offset: 0x00004140
			protected static TimeSpan CalculateMinTimerDueTime(TimeSpan t1, TimeSpan t2)
			{
				if (t1 < TimeSpan.Zero || t2 < TimeSpan.Zero)
				{
					return TimeSpan.Zero;
				}
				if (t1 < t2)
				{
					return t1;
				}
				return t2;
			}

			// Token: 0x06000117 RID: 279 RVA: 0x00005F6E File Offset: 0x0000416E
			protected virtual void InternalCancel()
			{
				this.ForceAsyncWaitingCompletions();
			}

			// Token: 0x06000118 RID: 280 RVA: 0x00005F76 File Offset: 0x00004176
			protected override void InternalDispose(bool disposing)
			{
			}

			// Token: 0x06000119 RID: 281 RVA: 0x00005F78 File Offset: 0x00004178
			protected void StartWaitFor(UcmaCallSession.SessionState.AsyncOperation op)
			{
				if (!this.IsWaitingFor(op))
				{
					this.Diag.Trace("Now waiting for {0}", new object[]
					{
						op
					});
					this.waiting |= op;
				}
			}

			// Token: 0x0600011A RID: 282 RVA: 0x00005FC0 File Offset: 0x000041C0
			protected void StopWaitFor(UcmaCallSession.SessionState.AsyncOperation op)
			{
				if (this.IsWaitingFor(op))
				{
					this.Diag.Trace("Done waiting for {0}", new object[]
					{
						op
					});
					this.waiting &= ~op;
					this.Diag.Trace("Still waiting for {0}", new object[]
					{
						this.waiting
					});
				}
			}

			// Token: 0x0600011B RID: 283 RVA: 0x0000602C File Offset: 0x0000422C
			protected bool IsWaitingFor(UcmaCallSession.SessionState.AsyncOperation op)
			{
				return (this.waiting & op) == op;
			}

			// Token: 0x0600011C RID: 284 RVA: 0x0000603C File Offset: 0x0000423C
			protected bool RequiresSigningAsTransferor(bool isSigningEnabled, PlatformSignalingHeader h, out string transferor)
			{
				transferor = null;
				if (isSigningEnabled && string.Equals(h.Name, "Referred-By", StringComparison.OrdinalIgnoreCase))
				{
					transferor = UcmaCallSession.SessionState.GetTransferorFromReferredBy(h);
					this.Diag.Trace("RequiresSigningAsTransferor: Signing required. Transferor: {0}", new object[]
					{
						transferor
					});
				}
				return null != transferor;
			}

			// Token: 0x0600011D RID: 285
			protected abstract void InternalStart();

			// Token: 0x0600011E RID: 286
			protected abstract void ForceAsyncWaitingCompletions();

			// Token: 0x0600011F RID: 287
			protected abstract void CompleteFinalAsyncCallback();

			// Token: 0x06000120 RID: 288 RVA: 0x0000608F File Offset: 0x0000428F
			protected virtual void OnHoldNotify()
			{
				this.Diag.Trace("On Hold notification received", new object[0]);
			}

			// Token: 0x06000121 RID: 289 RVA: 0x000060A7 File Offset: 0x000042A7
			protected virtual void OnResumeNotify()
			{
				this.Diag.Trace("On Resume notification received", new object[0]);
			}

			// Token: 0x06000122 RID: 290 RVA: 0x000060C0 File Offset: 0x000042C0
			private static string GetTransferorFromReferredBy(PlatformSignalingHeader referredBy)
			{
				string text = referredBy.Value;
				if (text.StartsWith("<", StringComparison.OrdinalIgnoreCase) && text.EndsWith(">", StringComparison.OrdinalIgnoreCase))
				{
					text = text.Substring(1, text.Length - 2);
				}
				return text.ToLowerInvariant();
			}

			// Token: 0x06000123 RID: 291 RVA: 0x00006106 File Offset: 0x00004306
			private void CompleteState()
			{
				this.Diag.Assert(!this.IsWaitingForAsyncCompletion, "Waiting for Async completion, but completing state!");
				if (!base.IsDisposed)
				{
					if (this is UcmaCallSession.TeardownSessionState)
					{
						this.CompleteFinalAsyncCallback();
						return;
					}
					this.CompleteNonTeardownState();
				}
			}

			// Token: 0x06000124 RID: 292 RVA: 0x00006140 File Offset: 0x00004340
			private void CompleteNonTeardownState()
			{
				if (this.Args.Error != null)
				{
					this.Diag.Trace("CompleteAsyncCallback with error='{0}'", new object[]
					{
						this.Args.Error
					});
					this.FireError(this.Args.Error);
				}
				else if (this.completion == UcmaCallSession.SessionState.CompletionReason.Cancel)
				{
					this.Diag.Trace("CompleteAsyncCallback with OnCancelled", new object[0]);
					this.Session.Fire<UMCallSessionEventArgs>(this.Session.OnCancelled, this.Args);
				}
				else if (this.completion == UcmaCallSession.SessionState.CompletionReason.Hangup)
				{
					this.Diag.Trace("CompleteAsyncCallback with OnHangup", new object[0]);
					this.Session.Fire<UMCallSessionEventArgs>(this.Session.OnHangup, this.Args);
				}
				else if (this.completion == UcmaCallSession.SessionState.CompletionReason.Fax)
				{
					this.Diag.Trace("CompleteAsyncCallback with OnFax (OnDtmf)", new object[0]);
					this.Session.Fire<UMCallSessionEventArgs>(this.Session.OnDtmf, this.Args);
				}
				else if (this.completion == UcmaCallSession.SessionState.CompletionReason.Teardown)
				{
					this.Diag.Trace("CompleteAsyncCallback with Teardown", new object[0]);
					this.Session.ChangeState(new UcmaCallSession.TeardownSessionState(this.Session, UcmaCallSession.DisconnectType.Remote, null));
				}
				else
				{
					this.Diag.Trace("CompleteAsyncCallback using derived class '{0}'", new object[]
					{
						this.Name
					});
					this.CompleteFinalAsyncCallback();
				}
				this.Session.TeardownIdleSession();
			}

			// Token: 0x06000125 RID: 293 RVA: 0x000062C0 File Offset: 0x000044C0
			private void DropCallDueToConsecutiveErrors()
			{
				this.Diag.Trace("Droppring call because there have been too many consecutive unhandled errors", new object[0]);
				this.TraceConsecutiveErrors();
				ExceptionHandling.SendWatsonWithExtraData(this.Args.Error, false);
				this.Session.ChangeState(new UcmaCallSession.TeardownSessionState(this.Session, UcmaCallSession.DisconnectType.Remote, null));
			}

			// Token: 0x06000126 RID: 294 RVA: 0x00006314 File Offset: 0x00004514
			private void TraceConsecutiveErrors()
			{
				foreach (Exception ex in this.Session.ConsecutiveErrors)
				{
					this.Diag.Trace("=> {0} followed by:", new object[]
					{
						ex
					});
				}
				this.Diag.Trace("=> Drop Call", new object[0]);
			}

			// Token: 0x04000058 RID: 88
			private UcmaCallSession.SessionState.AsyncOperation waiting;

			// Token: 0x04000059 RID: 89
			private UcmaCallSession.SessionState.CompletionReason completion;

			// Token: 0x0400005A RID: 90
			private bool callOnHold;

			// Token: 0x02000017 RID: 23
			[Flags]
			protected enum AsyncOperation
			{
				// Token: 0x0400005F RID: 95
				None = 0,
				// Token: 0x04000060 RID: 96
				CallAccepted = 1,
				// Token: 0x04000061 RID: 97
				CallEstablished = 2,
				// Token: 0x04000062 RID: 98
				FlowEstablished = 4,
				// Token: 0x04000063 RID: 99
				TransferComplete = 8,
				// Token: 0x04000064 RID: 100
				DtmfInputTimer = 16,
				// Token: 0x04000065 RID: 101
				HeavyBlockingComplete = 32,
				// Token: 0x04000066 RID: 102
				PromptsComplete = 64,
				// Token: 0x04000067 RID: 103
				SpeechRecoComplete = 256,
				// Token: 0x04000068 RID: 104
				BeepComplete = 512,
				// Token: 0x04000069 RID: 105
				RecordComplete = 1024,
				// Token: 0x0400006A RID: 106
				SendMessageComplete = 2048,
				// Token: 0x0400006B RID: 107
				TerminateComplete = 4096,
				// Token: 0x0400006C RID: 108
				MaxRecordingTimer = 8192,
				// Token: 0x0400006D RID: 109
				VoiceActivityTimer = 16384,
				// Token: 0x0400006E RID: 110
				SendDtmfComplete = 32768,
				// Token: 0x0400006F RID: 111
				SendPromptInfoComplete = 65536,
				// Token: 0x04000070 RID: 112
				CallStateTerminated = 131072,
				// Token: 0x04000071 RID: 113
				TerminatedOrFax = 262144,
				// Token: 0x04000072 RID: 114
				SpeechEmulateComplete = 524288
			}

			// Token: 0x02000018 RID: 24
			private enum CompletionReason
			{
				// Token: 0x04000074 RID: 116
				None,
				// Token: 0x04000075 RID: 117
				Cancel,
				// Token: 0x04000076 RID: 118
				Hangup,
				// Token: 0x04000077 RID: 119
				Fax,
				// Token: 0x04000078 RID: 120
				Teardown
			}
		}

		// Token: 0x02000019 RID: 25
		private abstract class InitializeFlowSessionState : UcmaCallSession.SessionState
		{
			// Token: 0x06000129 RID: 297 RVA: 0x00006398 File Offset: 0x00004598
			public InitializeFlowSessionState(UcmaCallSession session) : base(session)
			{
			}

			// Token: 0x1700002E RID: 46
			// (get) Token: 0x0600012A RID: 298 RVA: 0x000063A1 File Offset: 0x000045A1
			// (set) Token: 0x0600012B RID: 299 RVA: 0x000063A9 File Offset: 0x000045A9
			private protected bool FlowActivated { protected get; private set; }

			// Token: 0x0600012C RID: 300 RVA: 0x000063B4 File Offset: 0x000045B4
			public override void Call_AudioVideoFlowConfigurationRequested(object sender, AudioVideoFlowConfigurationRequestedEventArgs e)
			{
				AudioVideoFlowTemplate audioVideoFlowTemplate = new AudioVideoFlowTemplate(e.Flow);
				audioVideoFlowTemplate.EncryptionPolicy = base.Session.MediaEncryptionPolicy;
				AudioChannelTemplate audioChannelTemplate = audioVideoFlowTemplate.Audio.GetChannels()[0];
				audioChannelTemplate.UseHighPerformance = !AppConfig.Instance.Service.EnableRTAudio;
				e.Flow.Initialize(audioVideoFlowTemplate);
				base.Session.Subscriber.SubscribeTo(e.Flow);
			}

			// Token: 0x0600012D RID: 301 RVA: 0x0000642C File Offset: 0x0000462C
			public override void Flow_StateChanged(object sender, MediaFlowStateChangedEventArgs e)
			{
				base.Diag.Trace(UcmaCallSession.FormatStateChange<MediaFlowState>("Flow", e.PreviousState, e.State), new object[0]);
				if (base.IsWaitingFor(UcmaCallSession.SessionState.AsyncOperation.FlowEstablished) && e.State == 1)
				{
					base.StopWaitFor(UcmaCallSession.SessionState.AsyncOperation.FlowEstablished);
					base.Session.Ucma.ActivateFlow();
					this.FlowActivated = true;
				}
			}

			// Token: 0x0600012E RID: 302 RVA: 0x00006490 File Offset: 0x00004690
			protected override void InternalStart()
			{
				base.StartWaitFor(UcmaCallSession.SessionState.AsyncOperation.FlowEstablished);
			}

			// Token: 0x0600012F RID: 303 RVA: 0x00006499 File Offset: 0x00004699
			protected override void ForceAsyncWaitingCompletions()
			{
				base.StopWaitFor(UcmaCallSession.SessionState.AsyncOperation.FlowEstablished);
			}
		}

		// Token: 0x0200001A RID: 26
		private class AcceptCallSessionState : UcmaCallSession.InitializeFlowSessionState
		{
			// Token: 0x06000130 RID: 304 RVA: 0x000064A2 File Offset: 0x000046A2
			public AcceptCallSessionState(UcmaCallSession session) : base(session)
			{
			}

			// Token: 0x1700002F RID: 47
			// (get) Token: 0x06000131 RID: 305 RVA: 0x000064AB File Offset: 0x000046AB
			public override string Name
			{
				get
				{
					return "AcceptCallSessionState";
				}
			}

			// Token: 0x06000132 RID: 306 RVA: 0x000064B2 File Offset: 0x000046B2
			public override void Call_AcceptCompleted(IAsyncResult r)
			{
				base.StopWaitFor(UcmaCallSession.SessionState.AsyncOperation.CallAccepted);
				base.Session.Ucma.Call.EndAccept(r);
			}

			// Token: 0x06000133 RID: 307 RVA: 0x000064E8 File Offset: 0x000046E8
			protected override void InternalStart()
			{
				AsyncCallback asyncCallback = base.EventSubscriber.CreateSerializedAsyncCallback(delegate(IAsyncResult r)
				{
					base.Session.CurrentState.Call_AcceptCompleted(r);
				}, "Call_AcceptCompleted");
				CallAcceptOptions callAcceptOptions = new CallAcceptOptions();
				callAcceptOptions.RedirectDueToBandwidthPolicyEnabled = false;
				base.Session.Ucma.Call.BeginAccept(callAcceptOptions, asyncCallback, this);
				base.StartWaitFor(UcmaCallSession.SessionState.AsyncOperation.CallAccepted);
				base.InternalStart();
			}

			// Token: 0x06000134 RID: 308 RVA: 0x00006545 File Offset: 0x00004745
			protected override void ForceAsyncWaitingCompletions()
			{
				base.ForceAsyncWaitingCompletions();
			}

			// Token: 0x06000135 RID: 309 RVA: 0x00006550 File Offset: 0x00004750
			protected override void CompleteFinalAsyncCallback()
			{
				if (!base.FlowActivated)
				{
					base.Diag.Trace("Flow was never established.  Tearing down", new object[0]);
					base.Session.ChangeState(new UcmaCallSession.TeardownSessionState(base.Session, UcmaCallSession.DisconnectType.Remote, null));
					return;
				}
				base.Session.CurrentCallContext.ConnectionTime = new ExDateTime?(ExDateTime.UtcNow);
				base.Diag.Trace("Flow established.  Pausing, then firing OnCallConnected.", new object[0]);
				Thread.Sleep(250);
				base.Session.Fire<EventArgs>(base.Session.OnCallConnected, null);
			}

			// Token: 0x06000136 RID: 310 RVA: 0x000065E5 File Offset: 0x000047E5
			protected override DisposeTracker InternalGetDisposeTracker()
			{
				return DisposeTracker.Get<UcmaCallSession.AcceptCallSessionState>(this);
			}
		}

		// Token: 0x0200001B RID: 27
		private class AcceptFaxSessionState : UcmaCallSession.SessionState
		{
			// Token: 0x06000138 RID: 312 RVA: 0x000065ED File Offset: 0x000047ED
			public AcceptFaxSessionState(UcmaCallSession session) : base(session)
			{
			}

			// Token: 0x17000030 RID: 48
			// (get) Token: 0x06000139 RID: 313 RVA: 0x000065F6 File Offset: 0x000047F6
			public override string Name
			{
				get
				{
					return "AcceptFaxSessionState";
				}
			}

			// Token: 0x0600013A RID: 314 RVA: 0x000065FD File Offset: 0x000047FD
			protected override void InternalStart()
			{
				base.Session.CurrentCallContext.CallType = 6;
				base.Session.TaskCallType = CommonConstants.TaskCallType.Fax;
				base.Session.Fire<UMCallSessionEventArgs>(base.Session.OnFaxRequestReceive, base.Args);
			}

			// Token: 0x0600013B RID: 315 RVA: 0x00006638 File Offset: 0x00004838
			protected override void ForceAsyncWaitingCompletions()
			{
			}

			// Token: 0x0600013C RID: 316 RVA: 0x0000663A File Offset: 0x0000483A
			protected override void CompleteFinalAsyncCallback()
			{
			}

			// Token: 0x0600013D RID: 317 RVA: 0x0000663C File Offset: 0x0000483C
			protected override DisposeTracker InternalGetDisposeTracker()
			{
				return DisposeTracker.Get<UcmaCallSession.AcceptFaxSessionState>(this);
			}
		}

		// Token: 0x0200001C RID: 28
		private class BlindTransferSessionState : UcmaCallSession.SessionState
		{
			// Token: 0x0600013E RID: 318 RVA: 0x00006644 File Offset: 0x00004844
			public BlindTransferSessionState(UcmaCallSession session, string phoneNumber) : base(session)
			{
				bool flag = false;
				try
				{
					this.Initialize(this.UriFromPhoneNumber(phoneNumber), null);
					flag = true;
				}
				finally
				{
					if (!flag)
					{
						this.Dispose();
					}
				}
			}

			// Token: 0x0600013F RID: 319 RVA: 0x00006688 File Offset: 0x00004888
			public BlindTransferSessionState(UcmaCallSession session, PlatformSipUri uri, IEnumerable<PlatformSignalingHeader> headers) : base(session)
			{
				this.Initialize(uri, headers);
			}

			// Token: 0x17000031 RID: 49
			// (get) Token: 0x06000140 RID: 320 RVA: 0x00006699 File Offset: 0x00004899
			public override string Name
			{
				get
				{
					return "BlindTransferSessionState";
				}
			}

			// Token: 0x06000141 RID: 321 RVA: 0x000066A0 File Offset: 0x000048A0
			public override void Call_TransferCompleted(IAsyncResult r)
			{
				base.StopWaitFor(UcmaCallSession.SessionState.AsyncOperation.TransferComplete);
				base.Session.Ucma.Call.EndTransfer(r);
			}

			// Token: 0x06000142 RID: 322 RVA: 0x000066D4 File Offset: 0x000048D4
			protected override void InternalStart()
			{
				UcmaPlatform.ValidateRealTimeUri(this.uri.ToString());
				AsyncCallback asyncCallback = base.EventSubscriber.CreateSerializedAsyncCallback(delegate(IAsyncResult r)
				{
					base.Session.CurrentState.Call_TransferCompleted(r);
				}, "Call_TransferCompleted");
				base.Session.Ucma.Call.BeginTransfer(this.uri.ToString(), this.CreateCallTransferOptions(), asyncCallback, this);
				base.StartWaitFor(UcmaCallSession.SessionState.AsyncOperation.TransferComplete);
			}

			// Token: 0x06000143 RID: 323 RVA: 0x0000673E File Offset: 0x0000493E
			protected override void ForceAsyncWaitingCompletions()
			{
			}

			// Token: 0x06000144 RID: 324 RVA: 0x00006740 File Offset: 0x00004940
			protected override void CompleteFinalAsyncCallback()
			{
				base.Session.Fire<UMCallSessionEventArgs>(base.Session.OnTransferComplete, base.Args);
			}

			// Token: 0x06000145 RID: 325 RVA: 0x0000675E File Offset: 0x0000495E
			protected override DisposeTracker InternalGetDisposeTracker()
			{
				return DisposeTracker.Get<UcmaCallSession.BlindTransferSessionState>(this);
			}

			// Token: 0x06000146 RID: 326 RVA: 0x00006766 File Offset: 0x00004966
			private void Initialize(PlatformSipUri uri, IEnumerable<PlatformSignalingHeader> headers)
			{
				this.uri = uri;
				this.headers = headers;
			}

			// Token: 0x06000147 RID: 327 RVA: 0x00006778 File Offset: 0x00004978
			private PlatformSipUri UriFromPhoneNumber(string phoneNumber)
			{
				UcmaCallInfo callInfo = base.Session.CallInfo;
				base.Diag.Assert(null != callInfo, "no call info in blind transfer!");
				base.Diag.Assert(null != callInfo.RemoteContactUri, "remote contact uri is null in blind transfer!");
				return new UcmaSipUri(callInfo.RemoteContactUri.ToString())
				{
					User = phoneNumber,
					UserParameter = UserParameter.Phone,
					TransportParameter = ((base.Session.LocalEndpoint.Platform.Transport == 1) ? TransportParameter.Tcp : TransportParameter.Tls)
				};
			}

			// Token: 0x06000148 RID: 328 RVA: 0x00006808 File Offset: 0x00004A08
			private CallTransferOptions CreateCallTransferOptions()
			{
				CallTransferOptions callTransferOptions = new CallTransferOptions(0);
				bool supportsMsOrganizationRouting = base.Session.CurrentCallContext.RoutingHelper.SupportsMsOrganizationRouting;
				if (this.headers != null)
				{
					foreach (PlatformSignalingHeader platformSignalingHeader in this.headers)
					{
						string transferor;
						if (base.RequiresSigningAsTransferor(supportsMsOrganizationRouting, platformSignalingHeader, out transferor))
						{
							callTransferOptions.Transferor = transferor;
						}
						else
						{
							callTransferOptions.Headers.Add(new SignalingHeader(platformSignalingHeader.Name, platformSignalingHeader.Value));
						}
					}
				}
				return callTransferOptions;
			}

			// Token: 0x0400007A RID: 122
			private PlatformSipUri uri;

			// Token: 0x0400007B RID: 123
			private IEnumerable<PlatformSignalingHeader> headers;
		}

		// Token: 0x0200001D RID: 29
		private class PlayPromptsSessionState : UcmaCallSession.SessionState
		{
			// Token: 0x0600014A RID: 330 RVA: 0x000068AC File Offset: 0x00004AAC
			public PlayPromptsSessionState(UcmaCallSession session, UcmaCallSession.PlaybackInfo promptInfo) : base(session)
			{
				this.PlaybackInfo = promptInfo;
			}

			// Token: 0x17000032 RID: 50
			// (get) Token: 0x0600014B RID: 331 RVA: 0x000068BC File Offset: 0x00004ABC
			// (set) Token: 0x0600014C RID: 332 RVA: 0x000068C4 File Offset: 0x00004AC4
			public bool IsSpeaking { get; private set; }

			// Token: 0x17000033 RID: 51
			// (get) Token: 0x0600014D RID: 333 RVA: 0x000068CD File Offset: 0x00004ACD
			public override bool IsIdle
			{
				get
				{
					return !base.IsWaitingForAsyncCompletion && !this.IsSpeaking;
				}
			}

			// Token: 0x17000034 RID: 52
			// (get) Token: 0x0600014E RID: 334 RVA: 0x000068E2 File Offset: 0x00004AE2
			public override string Name
			{
				get
				{
					return "PlayPromptsSessionState";
				}
			}

			// Token: 0x17000035 RID: 53
			// (get) Token: 0x0600014F RID: 335 RVA: 0x000068E9 File Offset: 0x00004AE9
			// (set) Token: 0x06000150 RID: 336 RVA: 0x000068F1 File Offset: 0x00004AF1
			private protected UcmaCallSession.PlaybackInfo PlaybackInfo { protected get; private set; }

			// Token: 0x06000151 RID: 337 RVA: 0x000068FA File Offset: 0x00004AFA
			public override void Player_SpeakCompleted(object sender, PromptPlayer.PlayerCompletedEventArgs e)
			{
				base.StopWaitFor(UcmaCallSession.SessionState.AsyncOperation.PromptsComplete);
				this.IsSpeaking = false;
				base.Args.PlayTime = this.PlaybackInfo.Offset + (DateTime.UtcNow - this.promptStartTimeUtc);
			}

			// Token: 0x06000152 RID: 338 RVA: 0x00006936 File Offset: 0x00004B36
			public override void ToneController_ToneReceived(object sender, ToneControllerEventArgs e)
			{
				if (!this.PlaybackInfo.Uninterruptable)
				{
					this.ForceAsyncWaitingCompletions();
					base.ToneController_ToneReceived(sender, e);
				}
			}

			// Token: 0x06000153 RID: 339 RVA: 0x00006954 File Offset: 0x00004B54
			public override void Call_SendPromptInfoCompleted(IAsyncResult r)
			{
				base.StopWaitFor(UcmaCallSession.SessionState.AsyncOperation.SendPromptInfoComplete);
				try
				{
					base.Session.Ucma.Call.EndSendMessage(r);
				}
				catch (FailureResponseException ex)
				{
					base.Diag.Trace("An exception is hit in sending SIP Info for prompts: {0}", new object[]
					{
						ex
					});
				}
			}

			// Token: 0x06000154 RID: 340 RVA: 0x000069B4 File Offset: 0x00004BB4
			public virtual void HandleApplicationRequestToStopPlayback()
			{
				base.Args.WasPlaybackStopped = true;
				base.Session.Ucma.Player.Cancel();
			}

			// Token: 0x06000155 RID: 341 RVA: 0x000069D7 File Offset: 0x00004BD7
			public void Skip(TimeSpan timeToSkip)
			{
				base.Session.Ucma.Player.Skip(timeToSkip);
			}

			// Token: 0x06000156 RID: 342 RVA: 0x000069EF File Offset: 0x00004BEF
			protected override void InternalStart()
			{
				this.ConditionalSendPromptInfoMessage();
				this.ConditionalStartPrompts();
			}

			// Token: 0x06000157 RID: 343 RVA: 0x000069FD File Offset: 0x00004BFD
			protected override void InternalCancel()
			{
				this.ForceAsyncWaitingCompletions();
				base.Session.Ucma.Player.Cancel();
			}

			// Token: 0x06000158 RID: 344 RVA: 0x00006A1A File Offset: 0x00004C1A
			protected override DisposeTracker InternalGetDisposeTracker()
			{
				return DisposeTracker.Get<UcmaCallSession.PlayPromptsSessionState>(this);
			}

			// Token: 0x06000159 RID: 345 RVA: 0x00006A22 File Offset: 0x00004C22
			protected override void ForceAsyncWaitingCompletions()
			{
				if (base.IsWaitingFor(UcmaCallSession.SessionState.AsyncOperation.PromptsComplete))
				{
					base.Session.Ucma.Player.Cancel();
				}
			}

			// Token: 0x0600015A RID: 346 RVA: 0x00006A44 File Offset: 0x00004C44
			protected override void CompleteFinalAsyncCallback()
			{
				base.Diag.Assert(!this.IsSpeaking || !this.PlaybackInfo.UnconditionalBargeIn);
				if (!this.IsSpeaking)
				{
					base.Session.Fire<UMCallSessionEventArgs>(base.Session.OnComplete, base.Args);
				}
			}

			// Token: 0x0600015B RID: 347 RVA: 0x00006AAC File Offset: 0x00004CAC
			protected void ConditionalSendPromptInfoMessage()
			{
				if (this.ShouldSendTestInfo())
				{
					string text = base.Session.BuildPromptInfoMessageString(this.PlaybackInfo.TurnName, this.PlaybackInfo.Prompts);
					AsyncCallback asyncCallback = base.EventSubscriber.CreateSerializedAsyncCallback(delegate(IAsyncResult r)
					{
						base.Session.CurrentState.Call_SendPromptInfoCompleted(r);
					}, "Call_SendPromptInfoCompleted");
					ContentDescription contentDescription = new ContentDescription(CommonConstants.ContentTypeTextPlain, text);
					base.Session.Ucma.Call.BeginSendMessage(1, contentDescription, new CallSendMessageRequestOptions(), asyncCallback, this);
					base.StartWaitFor(UcmaCallSession.SessionState.AsyncOperation.SendPromptInfoComplete);
				}
			}

			// Token: 0x0600015C RID: 348 RVA: 0x00006B3D File Offset: 0x00004D3D
			private void ConditionalStartPrompts()
			{
				if (this.IsTypeAheadAvailable())
				{
					this.ForceAsyncWaitingCompletions();
					base.CompleteAsyncCallback();
					return;
				}
				if (this.PlaybackInfo.Prompts.Count == 0)
				{
					this.SimulateSpeakCompleted();
					return;
				}
				this.StartPrompts();
			}

			// Token: 0x0600015D RID: 349 RVA: 0x00006B74 File Offset: 0x00004D74
			private void SimulateSpeakCompleted()
			{
				PromptPlayer.PlayerCompletedEventArgs e = new PromptPlayer.PlayerCompletedEventArgs();
				this.Player_SpeakCompleted(this, e);
				base.CompleteAsyncCallback();
			}

			// Token: 0x0600015E RID: 350 RVA: 0x00006B98 File Offset: 0x00004D98
			private void StartPrompts()
			{
				this.promptStartTimeUtc = DateTime.UtcNow;
				ArrayList prompts = base.Session.TestInfo.IsFeatureEnabled(UcmaCallSession.TestInfoFeatures.SkipPrompt) ? new ArrayList() : this.PlaybackInfo.Prompts;
				base.Session.Ucma.Player.Play(prompts, base.Session.CurrentCallContext.Culture, this.PlaybackInfo.Offset);
				this.IsSpeaking = true;
				if (this.PlaybackInfo.UnconditionalBargeIn)
				{
					base.StartWaitFor(UcmaCallSession.SessionState.AsyncOperation.PromptsComplete);
				}
			}

			// Token: 0x0600015F RID: 351 RVA: 0x00006C24 File Offset: 0x00004E24
			private bool ShouldSendTestInfo()
			{
				bool result = false;
				if (this.PlaybackInfo.Prompts.Count > 0 && (base.Session.CurrentCallContext.IsTestCall || base.Session.CurrentCallContext.IsTUIDiagnosticCall))
				{
					result = true;
				}
				return result;
			}

			// Token: 0x06000160 RID: 352 RVA: 0x00006C6D File Offset: 0x00004E6D
			private bool IsTypeAheadAvailable()
			{
				return !base.Session.ToneAccumulator.IsEmpty && !this.PlaybackInfo.Uninterruptable;
			}

			// Token: 0x0400007C RID: 124
			private DateTime promptStartTimeUtc;
		}

		// Token: 0x0200001E RID: 30
		private class PlayPromptsAndRecoDtmfSessionState : UcmaCallSession.PlayPromptsSessionState
		{
			// Token: 0x06000162 RID: 354 RVA: 0x00006C91 File Offset: 0x00004E91
			public PlayPromptsAndRecoDtmfSessionState(UcmaCallSession session, UcmaCallSession.PlaybackInfo promptInfo) : base(session, promptInfo)
			{
				this.finalTimeoutUtc = DateTime.MaxValue;
				this.Completion = UcmaCallSession.PlayPromptsAndRecoDtmfSessionState.CompletionReason.None;
			}

			// Token: 0x17000036 RID: 54
			// (get) Token: 0x06000163 RID: 355 RVA: 0x00006CAD File Offset: 0x00004EAD
			public override string Name
			{
				get
				{
					return "PlayPromptsAndRecoDtmfSessionState";
				}
			}

			// Token: 0x17000037 RID: 55
			// (get) Token: 0x06000164 RID: 356 RVA: 0x00006CB4 File Offset: 0x00004EB4
			// (set) Token: 0x06000165 RID: 357 RVA: 0x00006CBC File Offset: 0x00004EBC
			private protected UcmaCallSession.PlayPromptsAndRecoDtmfSessionState.CompletionReason Completion { protected get; private set; }

			// Token: 0x06000166 RID: 358 RVA: 0x00006CC8 File Offset: 0x00004EC8
			public override void Player_SpeakCompleted(object sender, PromptPlayer.PlayerCompletedEventArgs e)
			{
				base.Player_SpeakCompleted(sender, e);
				if (!e.Cancelled)
				{
					this.Completion = UcmaCallSession.PlayPromptsAndRecoDtmfSessionState.CompletionReason.None;
					if (this.ExpectingInput())
					{
						this.finalTimeoutUtc = DateTime.UtcNow + base.PlaybackInfo.Timeout;
						this.EnsureTimer(base.PlaybackInfo.InitialSilenceTimeout);
						return;
					}
					this.ForceAsyncWaitingCompletions();
				}
			}

			// Token: 0x06000167 RID: 359 RVA: 0x00006D28 File Offset: 0x00004F28
			public override void ToneController_ToneReceived(object sender, ToneControllerEventArgs e)
			{
				this.SetCompletionReasonIfCurrentlyUnset(UcmaCallSession.PlayPromptsAndRecoDtmfSessionState.CompletionReason.Tone);
				base.Session.ToneAccumulator.Add(UcmaCallSession.ToneToByte((byte)e.Tone));
				InputState inputState = this.ComputeInputState();
				if (base.IsSpeaking && (base.PlaybackInfo.UnconditionalBargeIn || base.Session.ToneAccumulator.ContainsBargeInPattern(base.PlaybackInfo.StopPatterns)))
				{
					base.StartWaitFor(UcmaCallSession.SessionState.AsyncOperation.PromptsComplete);
				}
				base.ForceAsyncWaitingCompletions();
				if (InputStateHelper.IsUnambiguous(inputState))
				{
					this.ForceAsyncWaitingCompletions();
					return;
				}
				this.ContinueDtmfRecognition(inputState);
			}

			// Token: 0x06000168 RID: 360 RVA: 0x00006DB5 File Offset: 0x00004FB5
			protected override void OnHoldNotify()
			{
				if (this.timer != null && !this.isTimerDisposed)
				{
					this.timer.Change(-1, 0);
					base.StopWaitFor(UcmaCallSession.SessionState.AsyncOperation.DtmfInputTimer);
				}
			}

			// Token: 0x06000169 RID: 361 RVA: 0x00006DE0 File Offset: 0x00004FE0
			protected override void OnResumeNotify()
			{
				if (this.timer != null && !this.isTimerDisposed)
				{
					this.finalTimeoutUtc = DateTime.UtcNow + base.PlaybackInfo.Timeout;
					this.timer.Change(base.PlaybackInfo.InitialSilenceTimeout, TimeSpan.Zero);
					base.StartWaitFor(UcmaCallSession.SessionState.AsyncOperation.DtmfInputTimer);
				}
			}

			// Token: 0x0600016A RID: 362 RVA: 0x00006E3C File Offset: 0x0000503C
			protected override void InternalStart()
			{
				InputState inputState = this.ComputeInputState();
				base.Diag.Assert(InputStateHelper.IsAllowed(inputState));
				if (!InputStateHelper.IsStarted(inputState))
				{
					base.InternalStart();
					return;
				}
				base.ConditionalSendPromptInfoMessage();
				if (InputStateHelper.IsUnambiguous(inputState))
				{
					this.SetCompletionReasonIfCurrentlyUnset(UcmaCallSession.PlayPromptsAndRecoDtmfSessionState.CompletionReason.Tone);
					this.ForceAsyncWaitingCompletions();
					base.CompleteAsyncCallback();
					return;
				}
				this.ContinueDtmfRecognition(inputState);
			}

			// Token: 0x0600016B RID: 363 RVA: 0x00006E99 File Offset: 0x00005099
			protected virtual bool ExpectingInput()
			{
				return base.PlaybackInfo.MinDigits > 0;
			}

			// Token: 0x0600016C RID: 364 RVA: 0x00006EA9 File Offset: 0x000050A9
			protected override void ForceAsyncWaitingCompletions()
			{
				base.StopWaitFor(UcmaCallSession.SessionState.AsyncOperation.DtmfInputTimer);
				base.ForceAsyncWaitingCompletions();
			}

			// Token: 0x0600016D RID: 365 RVA: 0x00006EBC File Offset: 0x000050BC
			protected override void CompleteFinalAsyncCallback()
			{
				switch (this.Completion)
				{
				case UcmaCallSession.PlayPromptsAndRecoDtmfSessionState.CompletionReason.Tone:
					this.SetDigitsForCompletion();
					base.Session.Fire<UMCallSessionEventArgs>(base.Session.OnDtmf, base.Args);
					this.RestartRecognitionIfAppropriate();
					return;
				case UcmaCallSession.PlayPromptsAndRecoDtmfSessionState.CompletionReason.Timeout:
					base.Session.Fire<UMCallSessionEventArgs>(base.Session.OnTimeout, base.Args);
					return;
				default:
					base.CompleteFinalAsyncCallback();
					return;
				}
			}

			// Token: 0x0600016E RID: 366 RVA: 0x00006F30 File Offset: 0x00005130
			protected override void InternalDispose(bool disposing)
			{
				try
				{
					if (this.timer != null)
					{
						this.timer.Dispose();
						this.isTimerDisposed = true;
					}
				}
				finally
				{
					base.InternalDispose(disposing);
				}
			}

			// Token: 0x0600016F RID: 367 RVA: 0x00006F74 File Offset: 0x00005174
			private void SetCompletionReasonIfCurrentlyUnset(UcmaCallSession.PlayPromptsAndRecoDtmfSessionState.CompletionReason reason)
			{
				if (this.Completion == UcmaCallSession.PlayPromptsAndRecoDtmfSessionState.CompletionReason.None)
				{
					this.Completion = reason;
				}
			}

			// Token: 0x06000170 RID: 368 RVA: 0x00006F88 File Offset: 0x00005188
			private void SetDigitsForCompletion()
			{
				if (!this.digitsConsumed)
				{
					base.Args.DtmfDigits = base.Session.ToneAccumulator.ConsumeAccumulatedDigits(base.PlaybackInfo.MinDigits, base.PlaybackInfo.MaxDigits, base.PlaybackInfo.StopPatterns);
					this.digitsConsumed = true;
				}
			}

			// Token: 0x06000171 RID: 369 RVA: 0x00006FE0 File Offset: 0x000051E0
			private void DtmfInputTimer_Expired(object state)
			{
				if (base.IsWaitingFor(UcmaCallSession.SessionState.AsyncOperation.DtmfInputTimer))
				{
					base.StopWaitFor(UcmaCallSession.SessionState.AsyncOperation.DtmfInputTimer);
					if (base.Session.ToneAccumulator.IsEmpty)
					{
						this.SetCompletionReasonIfCurrentlyUnset(UcmaCallSession.PlayPromptsAndRecoDtmfSessionState.CompletionReason.Timeout);
						this.ForceAsyncWaitingCompletions();
						return;
					}
					this.SetCompletionReasonIfCurrentlyUnset(UcmaCallSession.PlayPromptsAndRecoDtmfSessionState.CompletionReason.Tone);
					this.ForceAsyncWaitingCompletions();
				}
			}

			// Token: 0x06000172 RID: 370 RVA: 0x0000702C File Offset: 0x0000522C
			private void ContinueDtmfRecognition(InputState inputState)
			{
				base.Diag.Assert(InputStateHelper.IsStarted(inputState), "Continuing DTMF recognition but it's not started!");
				TimeSpan t = base.PlaybackInfo.InterDigitTimeout;
				if (inputState == InputState.StartedCompleteAmbiguous)
				{
					t = TimeSpan.FromSeconds(1.0);
				}
				TimeSpan nextTimeout = UcmaCallSession.SessionState.CalculateMinTimerDueTime(this.finalTimeoutUtc - DateTime.UtcNow, t);
				this.EnsureTimer(nextTimeout);
			}

			// Token: 0x06000173 RID: 371 RVA: 0x00007098 File Offset: 0x00005298
			protected void EnsureTimer(TimeSpan nextTimeout)
			{
				if (this.timer != null)
				{
					if (!this.isTimerDisposed)
					{
						this.timer.Change(nextTimeout, TimeSpan.Zero);
					}
				}
				else
				{
					TimerCallback callback = base.EventSubscriber.CreateSerializedTimerCallback(delegate(object r)
					{
						this.DtmfInputTimer_Expired(r);
					}, "DtmfInputTimer");
					this.timer = new Timer(callback, this, nextTimeout, TimeSpan.Zero);
					this.isTimerDisposed = false;
				}
				base.StartWaitFor(UcmaCallSession.SessionState.AsyncOperation.DtmfInputTimer);
			}

			// Token: 0x06000174 RID: 372 RVA: 0x0000710F File Offset: 0x0000530F
			private InputState ComputeInputState()
			{
				return base.Session.ToneAccumulator.ComputeInputState(base.PlaybackInfo.MinDigits, base.PlaybackInfo.MaxDigits, base.PlaybackInfo.StopPatterns, base.PlaybackInfo.StopTones);
			}

			// Token: 0x06000175 RID: 373 RVA: 0x0000714D File Offset: 0x0000534D
			private void RestartRecognitionIfAppropriate()
			{
				if (!base.Args.WasPlaybackStopped)
				{
					this.digitsConsumed = false;
					this.Completion = UcmaCallSession.PlayPromptsAndRecoDtmfSessionState.CompletionReason.None;
					base.Args.Reset();
				}
			}

			// Token: 0x0400007F RID: 127
			private DateTime finalTimeoutUtc;

			// Token: 0x04000080 RID: 128
			private Timer timer;

			// Token: 0x04000081 RID: 129
			private bool digitsConsumed;

			// Token: 0x04000082 RID: 130
			private bool isTimerDisposed;

			// Token: 0x0200001F RID: 31
			protected enum CompletionReason
			{
				// Token: 0x04000085 RID: 133
				None,
				// Token: 0x04000086 RID: 134
				Tone,
				// Token: 0x04000087 RID: 135
				Timeout
			}
		}

		// Token: 0x02000020 RID: 32
		private class PlayPromptsAndRecoSpeechSessionState : UcmaCallSession.PlayPromptsAndRecoDtmfSessionState
		{
			// Token: 0x06000177 RID: 375 RVA: 0x00007175 File Offset: 0x00005375
			public PlayPromptsAndRecoSpeechSessionState(UcmaCallSession session, UcmaCallSession.PlaybackInfo promptInfo) : base(session, promptInfo)
			{
			}

			// Token: 0x17000038 RID: 56
			// (get) Token: 0x06000178 RID: 376 RVA: 0x0000718A File Offset: 0x0000538A
			public override string Name
			{
				get
				{
					return "PlayPromptsAndRecoSpeechSessionState";
				}
			}

			// Token: 0x06000179 RID: 377 RVA: 0x00007191 File Offset: 0x00005391
			public override void HandleApplicationRequestToStopPlayback()
			{
				this.speechArgs.WasPlaybackStopped = true;
				base.HandleApplicationRequestToStopPlayback();
			}

			// Token: 0x0600017A RID: 378 RVA: 0x000071A5 File Offset: 0x000053A5
			public override void SpeechReco_SpeechHypothesized(object sender, SpeechHypothesizedEventArgs e)
			{
				if (base.IsWaitingFor(UcmaCallSession.SessionState.AsyncOperation.PromptsComplete))
				{
					base.Session.Ucma.Player.Cancel();
				}
			}

			// Token: 0x0600017B RID: 379 RVA: 0x000071C6 File Offset: 0x000053C6
			public override void SpeechReco_SpeechDetected(object sender, SpeechDetectedEventArgs e)
			{
				base.EnsureTimer(base.PlaybackInfo.Timeout);
			}

			// Token: 0x0600017C RID: 380 RVA: 0x000071DC File Offset: 0x000053DC
			public override void SpeechReco_RecognizeCompleted(object sender, RecognizeCompletedEventArgs e)
			{
				base.StopWaitFor(UcmaCallSession.SessionState.AsyncOperation.SpeechRecoComplete);
				this.ForceAsyncWaitingCompletions();
				if (e.Error != null)
				{
					base.Args.Error = e.Error;
					return;
				}
				if (!e.Cancelled)
				{
					base.Diag.Trace("RecognizedCompleted.  babble='{0}', silence='{1}', inputEnded='{2}', result='{3}'", new object[]
					{
						e.BabbleTimeout,
						e.InitialSilenceTimeout,
						e.InputStreamEnded,
						e.Result
					});
					this.fireOnSpeech = true;
					if (e.Result != null && UcmaCallSession.PlayPromptsAndRecoSpeechSessionState.TestSemanticAccess(e.Result))
					{
						this.speechArgs.Result = new UcmaRecognitionResult(e.Result);
					}
				}
			}

			// Token: 0x0600017D RID: 381 RVA: 0x00007299 File Offset: 0x00005499
			public override void Player_BookmarkReached(object sender, BookmarkReachedEventArgs e)
			{
				this.speechArgs.BookmarkReached = e.Bookmark;
			}

			// Token: 0x0600017E RID: 382 RVA: 0x000072AC File Offset: 0x000054AC
			public override void Player_SpeakCompleted(object sender, PromptPlayer.PlayerCompletedEventArgs e)
			{
				this.SetRecognizerSensitivity(0);
				base.Player_SpeakCompleted(sender, e);
			}

			// Token: 0x0600017F RID: 383 RVA: 0x000072C0 File Offset: 0x000054C0
			protected static bool TestSemanticAccess(RecognitionResult result)
			{
				ValidateArgument.NotNull(result, "result");
				bool result2 = false;
				try
				{
					SemanticValue semantics = result.Semantics;
					result2 = true;
				}
				catch (InvalidOperationException ex)
				{
					CallIdTracer.TraceError(ExTraceGlobals.UCMATracer, null, "Failed to access semantic items.  {0}", new object[]
					{
						ex
					});
				}
				return result2;
			}

			// Token: 0x06000180 RID: 384 RVA: 0x00007318 File Offset: 0x00005518
			protected override void CompleteFinalAsyncCallback()
			{
				this.speechArgs.PlayTime = base.Args.PlayTime;
				if (this.fireOnSpeech)
				{
					base.Session.Fire<UMSpeechEventArgs>(base.Session.OnSpeech, this.speechArgs);
					this.RestartRecognitionIfAppropriate();
					return;
				}
				base.CompleteFinalAsyncCallback();
			}

			// Token: 0x06000181 RID: 385 RVA: 0x0000736C File Offset: 0x0000556C
			protected override void ForceAsyncWaitingCompletions()
			{
				base.Session.Ucma.HaltRecognition();
				base.ForceAsyncWaitingCompletions();
			}

			// Token: 0x06000182 RID: 386 RVA: 0x00007384 File Offset: 0x00005584
			protected override void InternalStart()
			{
				base.Session.Ucma.EnsureRecognition(base.Session.CurrentCallContext.Culture);
				this.SetRecognizerSensitivity(100);
				this.LoadGrammars();
				this.StartRecognizer();
				base.InternalStart();
			}

			// Token: 0x06000183 RID: 387 RVA: 0x000073C0 File Offset: 0x000055C0
			protected override bool ExpectingInput()
			{
				return true;
			}

			// Token: 0x06000184 RID: 388 RVA: 0x000073C4 File Offset: 0x000055C4
			protected virtual void StartRecognizer()
			{
				base.Session.Ucma.EnsureRecognition(base.Session.CurrentCallContext.Culture);
				base.Session.Ucma.SpeechReco.InitialSilenceTimeout = TimeSpan.MaxValue;
				base.Session.Ucma.SpeechReco.BabbleTimeout = base.PlaybackInfo.BabbleTimeout;
				base.Session.Ucma.SpeechReco.RecognizeAsync();
				base.StartWaitFor(UcmaCallSession.SessionState.AsyncOperation.SpeechRecoComplete);
			}

			// Token: 0x06000185 RID: 389 RVA: 0x0000744C File Offset: 0x0000564C
			private void LoadGrammars()
			{
				base.Session.Ucma.SpeechReco.UnloadAllGrammars();
				foreach (UMGrammar grammar in base.PlaybackInfo.Grammars)
				{
					this.LoadGrammar(grammar);
				}
			}

			// Token: 0x06000186 RID: 390 RVA: 0x000074BC File Offset: 0x000056BC
			private void LoadGrammar(UMGrammar grammar)
			{
				base.Diag.Trace("Loading grammar '{0}'", new object[]
				{
					grammar.ToString()
				});
				Grammar grammar2 = UcmaUtils.CreateGrammar(grammar);
				try
				{
					base.Session.Ucma.SpeechReco.LoadGrammar(grammar2);
				}
				catch (IOException ex)
				{
					base.Diag.Trace("Could not load grammar '{0}'. Error: {1}", new object[]
					{
						grammar,
						ex
					});
					throw new UMGrayException(ex);
				}
				catch (UnauthorizedAccessException ex2)
				{
					base.Diag.Trace("Could not load grammar '{0}'. Error: {1}", new object[]
					{
						grammar,
						ex2
					});
					throw new UMGrayException(ex2);
				}
			}

			// Token: 0x06000187 RID: 391 RVA: 0x00007580 File Offset: 0x00005780
			private void RestartRecognitionIfAppropriate()
			{
				if (this.ShouldRestartRecognition())
				{
					this.RestartRecognition();
				}
			}

			// Token: 0x06000188 RID: 392 RVA: 0x00007590 File Offset: 0x00005790
			private bool ShouldRestartRecognition()
			{
				return base.Session.CurrentState.Equals(this) && !this.speechArgs.WasPlaybackStopped;
			}

			// Token: 0x06000189 RID: 393 RVA: 0x000075B5 File Offset: 0x000057B5
			private void RestartRecognition()
			{
				this.speechArgs = new UMSpeechEventArgs();
				this.fireOnSpeech = false;
				this.StartRecognizer();
			}

			// Token: 0x0600018A RID: 394 RVA: 0x000075CF File Offset: 0x000057CF
			private void SetRecognizerSensitivity(int setting)
			{
				base.Session.Ucma.SpeechReco.UpdateRecognizerSetting("BackgroundSpeechSensitivity", setting);
			}

			// Token: 0x04000088 RID: 136
			private const string BackgroundSpeechSensitivity = "BackgroundSpeechSensitivity";

			// Token: 0x04000089 RID: 137
			private const int Normal = 0;

			// Token: 0x0400008A RID: 138
			private const int EchoCancel = 100;

			// Token: 0x0400008B RID: 139
			protected UMSpeechEventArgs speechArgs = new UMSpeechEventArgs();

			// Token: 0x0400008C RID: 140
			protected bool fireOnSpeech;
		}

		// Token: 0x02000021 RID: 33
		private class CommandAndControlLoggingSessionState : UcmaCallSession.PlayPromptsAndRecoSpeechSessionState
		{
			// Token: 0x0600018B RID: 395 RVA: 0x000075EC File Offset: 0x000057EC
			public CommandAndControlLoggingSessionState(UcmaCallSession session, UcmaCallSession.PlaybackInfo promptInfo) : base(session, promptInfo)
			{
				base.Diag.Trace("Command And Control Logging Enabled", new object[0]);
				this.diagnosticRecorder = session.Ucma.MediaRecorder;
				string path = Path.Combine(Utils.TempPath, "CommandAndControlDiagnosticRecording");
				this.diagnosticCallIdFolder = Path.Combine(path, base.Session.CurrentCallContext.CallId);
				Directory.CreateDirectory(this.diagnosticCallIdFolder);
			}

			// Token: 0x17000039 RID: 57
			// (get) Token: 0x0600018C RID: 396 RVA: 0x00007660 File Offset: 0x00005860
			public override string Name
			{
				get
				{
					return "CommandAndControlLoggingSessionState";
				}
			}

			// Token: 0x0600018D RID: 397 RVA: 0x00007667 File Offset: 0x00005867
			public override void SpeechReco_RecognizeCompleted(object sender, RecognizeCompletedEventArgs e)
			{
				this.StopCommandAndControlDiagnosticRecording(e);
				base.SpeechReco_RecognizeCompleted(sender, e);
			}

			// Token: 0x0600018E RID: 398 RVA: 0x00007678 File Offset: 0x00005878
			protected override void StartRecognizer()
			{
				this.StartCommandAndControlDiagnosticRecording();
				base.StartRecognizer();
			}

			// Token: 0x0600018F RID: 399 RVA: 0x00007688 File Offset: 0x00005888
			private void StartCommandAndControlDiagnosticRecording()
			{
				this.diagnosticTmpWma = TempFileFactory.CreateTempWmaFile();
				WmaFileSink wmaFileSink = new WmaFileSink(this.diagnosticTmpWma.FilePath);
				wmaFileSink.EncodingFormat = 2;
				this.diagnosticRecorder.SetSink(wmaFileSink);
				this.diagnosticRecorder.Start();
			}

			// Token: 0x06000190 RID: 400 RVA: 0x000076D0 File Offset: 0x000058D0
			private void StopCommandAndControlDiagnosticRecording(RecognizeCompletedEventArgs result)
			{
				this.diagnosticRecorder.Stop();
				FileInfo fileInfo = new FileInfo(this.diagnosticTmpWma.FilePath);
				if (fileInfo.Exists && fileInfo.Length > 0L)
				{
					using (ITempFile tempFile = MediaMethods.ToPcm(this.diagnosticTmpWma))
					{
						this.SetFinalFileAndLogRecoResults(tempFile, result);
					}
				}
			}

			// Token: 0x06000191 RID: 401 RVA: 0x0000773C File Offset: 0x0000593C
			private void SetFinalFileAndLogRecoResults(ITempFile wav, RecognizeCompletedEventArgs recoEvent)
			{
				string arg = string.Empty;
				try
				{
					arg = Path.Combine(this.diagnosticCallIdFolder, string.Format("{0}_{1}", base.Session.CommandAndControlLoggingCounter++, base.PlaybackInfo.TurnName));
				}
				catch (ArgumentException)
				{
					arg = Path.Combine(this.diagnosticCallIdFolder, string.Format("{0}_{1}", base.Session.CommandAndControlLoggingCounter++, Guid.NewGuid().ToString()));
				}
				string text = string.Format("{0}.wma", arg);
				string path = string.Format("{0}.txt", arg);
				try
				{
					File.Delete(text);
					File.Move(wav.FilePath, text);
					StringBuilder stringBuilder = new StringBuilder();
					if (recoEvent.Error != null)
					{
						stringBuilder.AppendLine(string.Format("Recognized Error: {0}", recoEvent.Error.Message)).AppendLine();
					}
					RecognitionResult result = recoEvent.Result;
					if (result == null)
					{
						stringBuilder.AppendLine("Recognition Result is null").AppendLine();
					}
					else
					{
						stringBuilder.AppendLine(string.Format("Recognized Phrase:{0}", result.Text));
						stringBuilder.AppendLine(string.Format("Confidence:{0}", result.Confidence)).AppendLine();
						stringBuilder.Append("Recognized WordUnit[Word,Confidence]:");
						foreach (RecognizedWordUnit recognizedWordUnit in result.Words)
						{
							stringBuilder.Append(string.Format("[{0},{1}] ", recognizedWordUnit.Text, recognizedWordUnit.Confidence));
						}
						stringBuilder.AppendLine().AppendLine();
						stringBuilder.Append("Alternates[Word,Confidence]:");
						foreach (RecognizedPhrase recognizedPhrase in result.Alternates)
						{
							stringBuilder.Append(string.Format("[{0},{1}]", recognizedPhrase.Text, recognizedPhrase.Confidence));
						}
						stringBuilder.AppendLine().AppendLine();
						stringBuilder.AppendLine(string.Format("Recognized Grammar:[RuleName:{0}, Weight:{1} , Loaded:{2}, Priority:{3}]", new object[]
						{
							result.Grammar.RuleName,
							result.Grammar.Weight,
							result.Grammar.Loaded,
							result.Grammar.Priority
						})).AppendLine();
					}
					stringBuilder.AppendLine("Loaded Grammars:");
					foreach (UMGrammar umgrammar in base.PlaybackInfo.Grammars)
					{
						stringBuilder.AppendLine(umgrammar.ToString());
					}
					using (StreamWriter streamWriter = new StreamWriter(path))
					{
						streamWriter.Write(stringBuilder.ToString());
					}
				}
				catch (Exception ex)
				{
					base.Diag.Trace("Ignoring Exception since these generated files are for diagnostic purposes, e ='{0}'", new object[]
					{
						ex
					});
				}
			}

			// Token: 0x0400008D RID: 141
			private readonly string diagnosticCallIdFolder;

			// Token: 0x0400008E RID: 142
			private readonly Recorder diagnosticRecorder;

			// Token: 0x0400008F RID: 143
			private ITempFile diagnosticTmpWma;
		}

		// Token: 0x02000022 RID: 34
		private class DeclineSessionState : UcmaCallSession.SessionState
		{
			// Token: 0x06000192 RID: 402 RVA: 0x00007B04 File Offset: 0x00005D04
			public DeclineSessionState(UcmaCallSession session, PlatformSignalingHeader diagnosticHeader) : base(session)
			{
				this.diagnosticHeader = diagnosticHeader;
			}

			// Token: 0x1700003A RID: 58
			// (get) Token: 0x06000193 RID: 403 RVA: 0x00007B14 File Offset: 0x00005D14
			public override string Name
			{
				get
				{
					return "DeclineSessionState";
				}
			}

			// Token: 0x06000194 RID: 404 RVA: 0x00007B1C File Offset: 0x00005D1C
			protected override void InternalStart()
			{
				CallDeclineOptions callDeclineOptions = new CallDeclineOptions(403);
				callDeclineOptions.Headers.Add(new SignalingHeader(this.diagnosticHeader.Name, this.diagnosticHeader.Value));
				base.Diag.Trace("DeclineSessionState.InternalStart: DiagnosticHeader:{0}", new object[]
				{
					this.diagnosticHeader.Value
				});
				base.Session.Ucma.Call.Decline(callDeclineOptions);
				base.Session.DisconnectCall(this.diagnosticHeader);
			}

			// Token: 0x06000195 RID: 405 RVA: 0x00007BA7 File Offset: 0x00005DA7
			protected override void ForceAsyncWaitingCompletions()
			{
			}

			// Token: 0x06000196 RID: 406 RVA: 0x00007BA9 File Offset: 0x00005DA9
			protected override void CompleteFinalAsyncCallback()
			{
			}

			// Token: 0x06000197 RID: 407 RVA: 0x00007BAB File Offset: 0x00005DAB
			protected override DisposeTracker InternalGetDisposeTracker()
			{
				return DisposeTracker.Get<UcmaCallSession.DeclineSessionState>(this);
			}

			// Token: 0x04000090 RID: 144
			private PlatformSignalingHeader diagnosticHeader;
		}

		// Token: 0x02000023 RID: 35
		private class EstablishCallSessionState : UcmaCallSession.InitializeFlowSessionState
		{
			// Token: 0x06000198 RID: 408 RVA: 0x00007BB4 File Offset: 0x00005DB4
			public EstablishCallSessionState(UcmaCallSession session, BaseUMCallSession.OutboundCallInfo info, IList<PlatformSignalingHeader> headers) : base(session)
			{
				this.info = info;
				this.headers = headers;
				Conversation conversation = new Conversation(base.Session.LocalEndpoint);
				this.SetCallingLineId(conversation);
				this.outboundCall = new AudioVideoCall(conversation);
				base.Session.AssociateCall(this.outboundCall);
			}

			// Token: 0x1700003B RID: 59
			// (get) Token: 0x06000199 RID: 409 RVA: 0x00007C0B File Offset: 0x00005E0B
			public override string Name
			{
				get
				{
					return "EstablishCallSessionState";
				}
			}

			// Token: 0x0600019A RID: 410 RVA: 0x00007C14 File Offset: 0x00005E14
			public override void Call_EstablishCompleted(IAsyncResult r)
			{
				try
				{
					base.StopWaitFor(UcmaCallSession.SessionState.AsyncOperation.CallEstablished);
					this.InitializeOutboundCallContext(this.outboundCall.EndEstablish(r));
				}
				catch (InvalidOperationException ex)
				{
					this.outboundCallError = ex;
				}
				catch (RealTimeException ex2)
				{
					this.outboundCallError = ex2;
				}
				finally
				{
					if (this.outboundCallError != null)
					{
						base.Diag.Trace("Call.EndEstablish failed. e='{0}'", new object[]
						{
							this.outboundCallError
						});
						this.ForceAsyncWaitingCompletions();
					}
				}
			}

			// Token: 0x0600019B RID: 411 RVA: 0x00007CAC File Offset: 0x00005EAC
			public override void Call_TerminateCompleted(IAsyncResult r)
			{
				base.StopWaitFor(UcmaCallSession.SessionState.AsyncOperation.TerminateComplete);
			}

			// Token: 0x0600019C RID: 412 RVA: 0x00007CCC File Offset: 0x00005ECC
			public void CancelOutboundCall()
			{
				this.outboundCallError = new OutboundCallCancelledException();
				this.ForceAsyncWaitingCompletions();
				AsyncCallback asyncCallback = base.EventSubscriber.CreateSerializedAsyncCallback(delegate(IAsyncResult r)
				{
					base.Session.CurrentState.Call_TerminateCompleted(r);
				}, "Call_TerminateCompleted");
				this.outboundCall.BeginTerminate(asyncCallback, this);
				base.StartWaitFor(UcmaCallSession.SessionState.AsyncOperation.TerminateComplete);
			}

			// Token: 0x0600019D RID: 413 RVA: 0x00007D34 File Offset: 0x00005F34
			protected override void InternalStart()
			{
				UcmaPlatform.ValidateRealTimeUri(this.info.CalledParty);
				AsyncCallback asyncCallback = base.EventSubscriber.CreateSerializedAsyncCallback(delegate(IAsyncResult r)
				{
					base.Session.CurrentState.Call_EstablishCompleted(r);
				}, "Call_EstablishCompleted");
				CallEstablishOptions callEstablishOptions = this.CreateCallEstablishOptions();
				this.outboundCall.BeginEstablish(this.info.CalledParty, callEstablishOptions, asyncCallback, this);
				base.StartWaitFor(UcmaCallSession.SessionState.AsyncOperation.CallEstablished);
				base.InternalStart();
			}

			// Token: 0x0600019E RID: 414 RVA: 0x00007D9C File Offset: 0x00005F9C
			protected override void ForceAsyncWaitingCompletions()
			{
				base.ForceAsyncWaitingCompletions();
			}

			// Token: 0x0600019F RID: 415 RVA: 0x00007DA4 File Offset: 0x00005FA4
			protected override void CompleteFinalAsyncCallback()
			{
				this.SetOutboundCallArgs();
				base.Session.Fire<OutboundCallDetailsEventArgs>(base.Session.OnOutboundCallRequestCompleted, this.outboundEventArgs);
			}

			// Token: 0x060001A0 RID: 416 RVA: 0x00007DC8 File Offset: 0x00005FC8
			protected override DisposeTracker InternalGetDisposeTracker()
			{
				return DisposeTracker.Get<UcmaCallSession.EstablishCallSessionState>(this);
			}

			// Token: 0x060001A1 RID: 417 RVA: 0x00007DD0 File Offset: 0x00005FD0
			private void SetCallingLineId(Conversation c)
			{
				try
				{
					if (!string.IsNullOrEmpty(this.info.CallingParty))
					{
						c.Impersonate(this.info.CallingParty, null, null);
						base.Diag.Trace("Outbound CLID set to '{0}'", new object[]
						{
							this.info.CallingParty
						});
					}
				}
				catch (ArgumentException ex)
				{
					base.Diag.Trace("Exception in SetCallingLineId '{0}'", new object[]
					{
						ex
					});
				}
			}

			// Token: 0x060001A2 RID: 418 RVA: 0x00007E5C File Offset: 0x0000605C
			private CallEstablishOptions CreateCallEstablishOptions()
			{
				CallEstablishOptions callEstablishOptions = new CallEstablishOptions();
				callEstablishOptions.ConnectionContext = UcmaUtils.CreateConnectionContext(this.info.Gateway.NextHopForOutboundRouting.Address.ToString(), this.info.Gateway.NextHopForOutboundRouting.Port);
				callEstablishOptions.ConnectionContext.AddressFamilyHint = new AddressFamilyHint?(UcmaUtils.MapIPAddressFamilyToHint(this.info.Gateway.IPAddressFamily));
				base.Diag.Trace("CreateCallEstablishOptions: using IPAddressFamily {0}", new object[]
				{
					this.info.Gateway.IPAddressFamily
				});
				if (this.headers != null)
				{
					foreach (PlatformSignalingHeader platformSignalingHeader in this.headers)
					{
						string transferor;
						if (base.RequiresSigningAsTransferor(true, platformSignalingHeader, out transferor))
						{
							callEstablishOptions.Transferor = transferor;
						}
						else
						{
							callEstablishOptions.Headers.Add(new SignalingHeader(platformSignalingHeader.Name, platformSignalingHeader.Value));
						}
					}
				}
				return callEstablishOptions;
			}

			// Token: 0x060001A3 RID: 419 RVA: 0x00007F78 File Offset: 0x00006178
			private void InitializeOutboundCallContext(CallMessageData data)
			{
				ValidateArgument.NotNull(data, "data");
				UcmaCallInfo callInfo = new UcmaCallInfo(data, this.outboundCall.Conversation, this.info.Gateway.Address.Address);
				base.Session.CallInfo = callInfo;
			}

			// Token: 0x060001A4 RID: 420 RVA: 0x00007FC3 File Offset: 0x000061C3
			private void SetOutboundCallArgs()
			{
				this.SetErrorIfNotConnected();
				if (this.outboundCallError == null && base.FlowActivated)
				{
					this.outboundEventArgs = this.CreateOutboundArgsForSuccess();
					return;
				}
				this.outboundEventArgs = this.CreateOutboundArgsForFailure(this.outboundCallError);
			}

			// Token: 0x060001A5 RID: 421 RVA: 0x00007FFC File Offset: 0x000061FC
			private void SetErrorIfNotConnected()
			{
				if (this.outboundCallError == null)
				{
					UMCallState state = base.Session.State;
					if (state != UMCallState.Connected)
					{
						base.Diag.Trace("CallState is not connected.  State={0}", new object[]
						{
							state
						});
						this.outboundCallError = new EstablishCallFailureException(string.Empty);
					}
				}
			}

			// Token: 0x060001A6 RID: 422 RVA: 0x00008054 File Offset: 0x00006254
			private OutboundCallDetailsEventArgs CreateOutboundArgsForSuccess()
			{
				UMCallInfoEx exInfo = new UMCallInfoEx
				{
					CallState = UMCallState.Connected,
					EndResult = UMOperationResult.Success
				};
				return new OutboundCallDetailsEventArgs(null, exInfo, null);
			}

			// Token: 0x060001A7 RID: 423 RVA: 0x0000807F File Offset: 0x0000627F
			private OutboundCallDetailsEventArgs CreateOutboundArgsForFailure(Exception e)
			{
				if (this.outboundCallError != null)
				{
					base.Session.LogOutboundCallFailed(this.info, e.Message, e.Message);
				}
				return new OutboundCallDetailsEventArgs(e, this.CreateCallInfoEx(e), null);
			}

			// Token: 0x060001A8 RID: 424 RVA: 0x000080B4 File Offset: 0x000062B4
			private UMCallInfoEx CreateCallInfoEx(Exception e)
			{
				FailureResponseException ex = e as FailureResponseException;
				int num = 100;
				while (e != null && ex == null && --num > 0)
				{
					ex = (e as FailureResponseException);
					e = e.InnerException;
				}
				base.Diag.Assert(num > 0);
				UMEventCause eventCause = UMEventCause.None;
				int num2 = 0;
				string responseText = string.Empty;
				if (ex != null)
				{
					num2 = ex.ResponseData.ResponseCode;
					responseText = ex.ResponseData.ResponseText;
					eventCause = BaseUMCallSession.GetUMEventCause(num2);
				}
				return new UMCallInfoEx
				{
					CallState = UMCallState.Disconnected,
					EndResult = UMOperationResult.Failure,
					EventCause = eventCause,
					ResponseCode = num2,
					ResponseText = responseText
				};
			}

			// Token: 0x04000091 RID: 145
			private AudioVideoCall outboundCall;

			// Token: 0x04000092 RID: 146
			private BaseUMCallSession.OutboundCallInfo info;

			// Token: 0x04000093 RID: 147
			private IList<PlatformSignalingHeader> headers;

			// Token: 0x04000094 RID: 148
			private Exception outboundCallError;

			// Token: 0x04000095 RID: 149
			private OutboundCallDetailsEventArgs outboundEventArgs;
		}

		// Token: 0x02000024 RID: 36
		private class ErrorSessionState : UcmaCallSession.SessionState
		{
			// Token: 0x060001AB RID: 427 RVA: 0x00008159 File Offset: 0x00006359
			public ErrorSessionState(UcmaCallSession session, Exception e) : base(session)
			{
				base.Args.Error = e;
			}

			// Token: 0x1700003C RID: 60
			// (get) Token: 0x060001AC RID: 428 RVA: 0x0000816E File Offset: 0x0000636E
			public override string Name
			{
				get
				{
					return "ErrorSessionState";
				}
			}

			// Token: 0x060001AD RID: 429 RVA: 0x00008178 File Offset: 0x00006378
			protected override void InternalStart()
			{
				if (base.Session.IsInvalidOperationExplainable && base.Args.Error is InvalidOperationException)
				{
					base.Diag.Trace("Ignoring IOP with disconnected call.  Tearing down.  e='{0}'", new object[]
					{
						base.Args.Error
					});
					base.Session.ChangeState(new UcmaCallSession.TeardownSessionState(base.Session, UcmaCallSession.DisconnectType.Remote, null));
					return;
				}
				if (base.Session.OnError == null)
				{
					base.Diag.Trace("Tearing down the call session because the application has not yet subscribed to the error event handler.  e={0}", new object[]
					{
						base.Args.Error
					});
					base.Session.ChangeState(new UcmaCallSession.TeardownSessionState(base.Session, UcmaCallSession.DisconnectType.Remote, null));
					return;
				}
				base.Session.Fire<UMCallSessionEventArgs>(base.Session.OnError, base.Args);
			}

			// Token: 0x060001AE RID: 430 RVA: 0x0000824B File Offset: 0x0000644B
			protected override void ForceAsyncWaitingCompletions()
			{
			}

			// Token: 0x060001AF RID: 431 RVA: 0x0000824D File Offset: 0x0000644D
			protected override void CompleteFinalAsyncCallback()
			{
			}

			// Token: 0x060001B0 RID: 432 RVA: 0x0000824F File Offset: 0x0000644F
			protected override DisposeTracker InternalGetDisposeTracker()
			{
				return DisposeTracker.Get<UcmaCallSession.ErrorSessionState>(this);
			}
		}

		// Token: 0x02000025 RID: 37
		private class HeavyBlockingOperationSessionState : UcmaCallSession.SessionState
		{
			// Token: 0x060001B1 RID: 433 RVA: 0x000082AC File Offset: 0x000064AC
			public HeavyBlockingOperationSessionState(UcmaCallSession session, IUMHeavyBlockingOperation operation, ArrayList prompts) : base(session)
			{
				UcmaCallSession.HeavyBlockingOperationSessionState <>4__this = this;
				this.prompts = prompts;
				this.hboEventArgs = new HeavyBlockingOperationEventArgs(operation);
				this.hbo = delegate()
				{
					using (new CallId(<>4__this.Session.CallId))
					{
						operation.Execute();
					}
				};
			}

			// Token: 0x1700003D RID: 61
			// (get) Token: 0x060001B2 RID: 434 RVA: 0x00008305 File Offset: 0x00006505
			public override string Name
			{
				get
				{
					return "HeavingBlockingOperationSessionState";
				}
			}

			// Token: 0x060001B3 RID: 435 RVA: 0x0000830C File Offset: 0x0000650C
			public override void Player_SpeakCompleted(object sender, PromptPlayer.PlayerCompletedEventArgs e)
			{
				base.StopWaitFor(UcmaCallSession.SessionState.AsyncOperation.PromptsComplete);
				if (!e.Cancelled)
				{
					this.hboEventArgs.CompletionType = HeavyBlockingOperationCompletionType.Timeout;
				}
			}

			// Token: 0x060001B4 RID: 436 RVA: 0x0000832A File Offset: 0x0000652A
			protected override void ForceAsyncWaitingCompletions()
			{
				base.Session.Ucma.Player.Cancel();
			}

			// Token: 0x060001B5 RID: 437 RVA: 0x00008344 File Offset: 0x00006544
			protected override void CompleteFinalAsyncCallback()
			{
				base.Diag.Trace("HBO Completion: {0}", new object[]
				{
					this.hboEventArgs.CompletionType
				});
				if (this.hboEventArgs.CompletionType == HeavyBlockingOperationCompletionType.Success)
				{
					base.Session.Fire<HeavyBlockingOperationEventArgs>(base.Session.OnHeavyBlockingOperation, this.hboEventArgs);
					return;
				}
				base.Session.ChangeState(new UcmaCallSession.TeardownSessionState(base.Session, UcmaCallSession.DisconnectType.Local, null));
			}

			// Token: 0x060001B6 RID: 438 RVA: 0x000083BF File Offset: 0x000065BF
			protected override DisposeTracker InternalGetDisposeTracker()
			{
				return DisposeTracker.Get<UcmaCallSession.HeavyBlockingOperationSessionState>(this);
			}

			// Token: 0x060001B7 RID: 439 RVA: 0x000083C7 File Offset: 0x000065C7
			protected override void InternalStart()
			{
				this.StartAudioHourglass();
				this.StartOperation();
			}

			// Token: 0x060001B8 RID: 440 RVA: 0x000083D8 File Offset: 0x000065D8
			private void EndOperation(IAsyncResult r)
			{
				try
				{
					base.StopWaitFor(UcmaCallSession.SessionState.AsyncOperation.HeavyBlockingComplete);
					this.ForceAsyncWaitingCompletions();
					this.hboEventArgs.Latency = DateTime.UtcNow - this.operationStartTimeUtc;
					if (this.hboEventArgs.CompletionType == HeavyBlockingOperationCompletionType.None)
					{
						this.hboEventArgs.CompletionType = HeavyBlockingOperationCompletionType.Success;
					}
					this.UpdatePerformanceCounters();
					this.hbo.EndInvoke(r);
				}
				catch (Exception error)
				{
					this.hboEventArgs.Error = error;
					throw;
				}
			}

			// Token: 0x060001B9 RID: 441 RVA: 0x0000845C File Offset: 0x0000665C
			private void StartAudioHourglass()
			{
				if (!base.Session.IsMediaTerminatingOrTerminated)
				{
					base.Session.Ucma.Player.Play(this.prompts, base.Session.CurrentCallContext.Culture, TimeSpan.Zero);
					base.StartWaitFor(UcmaCallSession.SessionState.AsyncOperation.PromptsComplete);
				}
			}

			// Token: 0x060001BA RID: 442 RVA: 0x000084B8 File Offset: 0x000066B8
			private void StartOperation()
			{
				this.operationStartTimeUtc = DateTime.UtcNow;
				AsyncCallback callback = base.EventSubscriber.CreateSerializedAsyncCallback(delegate(IAsyncResult r)
				{
					this.EndOperation(r);
				}, "EndOperation", true);
				this.hbo.BeginInvoke(callback, this);
				base.StartWaitFor(UcmaCallSession.SessionState.AsyncOperation.HeavyBlockingComplete);
			}

			// Token: 0x060001BB RID: 443 RVA: 0x00008504 File Offset: 0x00006704
			private void UpdatePerformanceCounters()
			{
				if (this.hboEventArgs.Latency > Constants.HeavyBlockingOperationDelay)
				{
					if (base.Session.IsSignalingTerminatingOrTerminated)
					{
						base.Session.CurrentCallContext.IncrementHungUpAfterDelayCounter();
					}
					if (!base.Session.CurrentCallContext.IsDelayedCallsCounterIncremented)
					{
						base.Session.IncrementCounter(GeneralCounters.DelayedCalls);
					}
				}
				base.Session.SetCounter(GeneralCounters.UserResponseLatency, BaseUMCallSession.AverageUserResponseLatency.Update(this.hboEventArgs.Latency.TotalMilliseconds));
			}

			// Token: 0x04000096 RID: 150
			private UcmaCallSession.HeavyBlockingOperationSessionState.HboDelegate hbo;

			// Token: 0x04000097 RID: 151
			private HeavyBlockingOperationEventArgs hboEventArgs;

			// Token: 0x04000098 RID: 152
			private DateTime operationStartTimeUtc;

			// Token: 0x04000099 RID: 153
			private ArrayList prompts;

			// Token: 0x02000026 RID: 38
			// (Invoke) Token: 0x060001BE RID: 446
			private delegate void HboDelegate();
		}

		// Token: 0x02000027 RID: 39
		private class IdleSessionState : UcmaCallSession.SessionState
		{
			// Token: 0x060001C1 RID: 449 RVA: 0x00008594 File Offset: 0x00006794
			public IdleSessionState(UcmaCallSession session) : base(session)
			{
			}

			// Token: 0x1700003E RID: 62
			// (get) Token: 0x060001C2 RID: 450 RVA: 0x0000859D File Offset: 0x0000679D
			public override string Name
			{
				get
				{
					return "IdleCallState";
				}
			}

			// Token: 0x060001C3 RID: 451 RVA: 0x000085A4 File Offset: 0x000067A4
			protected override void InternalStart()
			{
				base.Diag.Trace("Session is now idle.", new object[0]);
			}

			// Token: 0x060001C4 RID: 452 RVA: 0x000085BC File Offset: 0x000067BC
			protected override void ForceAsyncWaitingCompletions()
			{
			}

			// Token: 0x060001C5 RID: 453 RVA: 0x000085BE File Offset: 0x000067BE
			protected override void CompleteFinalAsyncCallback()
			{
			}

			// Token: 0x060001C6 RID: 454 RVA: 0x000085C0 File Offset: 0x000067C0
			protected override DisposeTracker InternalGetDisposeTracker()
			{
				return DisposeTracker.Get<UcmaCallSession.IdleSessionState>(this);
			}
		}

		// Token: 0x02000028 RID: 40
		private class PlayPromptsAndRecoEmulateSessionState : UcmaCallSession.PlayPromptsAndRecoSpeechSessionState
		{
			// Token: 0x060001C7 RID: 455 RVA: 0x000085C8 File Offset: 0x000067C8
			public PlayPromptsAndRecoEmulateSessionState(UcmaCallSession session, UcmaCallSession.PlaybackInfo promptInfo) : base(session, promptInfo)
			{
			}

			// Token: 0x1700003F RID: 63
			// (get) Token: 0x060001C8 RID: 456 RVA: 0x000085D2 File Offset: 0x000067D2
			public override string Name
			{
				get
				{
					return "PlayPromptsAndRecoEmulateSessionState";
				}
			}

			// Token: 0x060001C9 RID: 457 RVA: 0x000085D9 File Offset: 0x000067D9
			public override void SpeechReco_RecognizeCompleted(object sender, RecognizeCompletedEventArgs e)
			{
				base.StopWaitFor(UcmaCallSession.SessionState.AsyncOperation.SpeechEmulateComplete);
				base.SpeechReco_RecognizeCompleted(sender, e);
			}

			// Token: 0x060001CA RID: 458 RVA: 0x000085F0 File Offset: 0x000067F0
			public override void SpeechReco_EmulateRecognizeCompleted(object sender, EmulateRecognizeCompletedEventArgs e)
			{
				base.StopWaitFor(UcmaCallSession.SessionState.AsyncOperation.SpeechEmulateComplete);
				this.ForceAsyncWaitingCompletions();
				base.Session.Ucma.HaltRecognition();
				if (e.Error != null)
				{
					base.Args.Error = e.Error;
					return;
				}
				if (!e.Cancelled)
				{
					base.Diag.Trace("EmulateRecognizedCompleted.  result='{0}'", new object[]
					{
						e.Result
					});
					this.fireOnSpeech = true;
					if (e.Result != null && UcmaCallSession.PlayPromptsAndRecoSpeechSessionState.TestSemanticAccess(e.Result))
					{
						this.speechArgs.Result = new UcmaRecognitionResult(e.Result);
					}
				}
			}

			// Token: 0x060001CB RID: 459 RVA: 0x00008693 File Offset: 0x00006893
			protected override void StartRecognizer()
			{
				base.Session.Ucma.EnsureRecognition(base.Session.CurrentCallContext.Culture);
			}

			// Token: 0x060001CC RID: 460 RVA: 0x000086B5 File Offset: 0x000068B5
			public void RecognizeWave(string file)
			{
				base.StartWaitFor(UcmaCallSession.SessionState.AsyncOperation.SpeechEmulateComplete);
				base.Session.Ucma.SpeechReco.SetInputToWaveFile(file);
				base.Session.Ucma.SpeechReco.RecognizeAsync();
			}

			// Token: 0x060001CD RID: 461 RVA: 0x000086ED File Offset: 0x000068ED
			public void EmulateWave(string text)
			{
				base.StartWaitFor(UcmaCallSession.SessionState.AsyncOperation.SpeechEmulateComplete);
				base.Session.Ucma.SpeechReco.EmulateRecognizeAsync(text);
			}
		}

		// Token: 0x02000029 RID: 41
		internal class PlaybackInfo
		{
			// Token: 0x17000040 RID: 64
			// (get) Token: 0x060001CE RID: 462 RVA: 0x00008710 File Offset: 0x00006910
			// (set) Token: 0x060001CF RID: 463 RVA: 0x00008718 File Offset: 0x00006918
			public ArrayList Prompts { get; set; }

			// Token: 0x17000041 RID: 65
			// (get) Token: 0x060001D0 RID: 464 RVA: 0x00008721 File Offset: 0x00006921
			// (set) Token: 0x060001D1 RID: 465 RVA: 0x00008729 File Offset: 0x00006929
			public TimeSpan InitialSilenceTimeout { get; set; }

			// Token: 0x17000042 RID: 66
			// (get) Token: 0x060001D2 RID: 466 RVA: 0x00008732 File Offset: 0x00006932
			// (set) Token: 0x060001D3 RID: 467 RVA: 0x0000873A File Offset: 0x0000693A
			public int MinDigits { get; set; }

			// Token: 0x17000043 RID: 67
			// (get) Token: 0x060001D4 RID: 468 RVA: 0x00008743 File Offset: 0x00006943
			// (set) Token: 0x060001D5 RID: 469 RVA: 0x0000874B File Offset: 0x0000694B
			public int MaxDigits { get; set; }

			// Token: 0x17000044 RID: 68
			// (get) Token: 0x060001D6 RID: 470 RVA: 0x00008754 File Offset: 0x00006954
			// (set) Token: 0x060001D7 RID: 471 RVA: 0x0000875C File Offset: 0x0000695C
			public TimeSpan Timeout { get; set; }

			// Token: 0x17000045 RID: 69
			// (get) Token: 0x060001D8 RID: 472 RVA: 0x00008765 File Offset: 0x00006965
			// (set) Token: 0x060001D9 RID: 473 RVA: 0x0000876D File Offset: 0x0000696D
			public TimeSpan Offset { get; set; }

			// Token: 0x17000046 RID: 70
			// (get) Token: 0x060001DA RID: 474 RVA: 0x00008776 File Offset: 0x00006976
			// (set) Token: 0x060001DB RID: 475 RVA: 0x0000877E File Offset: 0x0000697E
			public TimeSpan InterDigitTimeout { get; set; }

			// Token: 0x17000047 RID: 71
			// (get) Token: 0x060001DC RID: 476 RVA: 0x00008787 File Offset: 0x00006987
			// (set) Token: 0x060001DD RID: 477 RVA: 0x0000878F File Offset: 0x0000698F
			public string StopTones { get; set; }

			// Token: 0x17000048 RID: 72
			// (get) Token: 0x060001DE RID: 478 RVA: 0x00008798 File Offset: 0x00006998
			// (set) Token: 0x060001DF RID: 479 RVA: 0x000087A0 File Offset: 0x000069A0
			public StopPatterns StopPatterns { get; set; }

			// Token: 0x17000049 RID: 73
			// (get) Token: 0x060001E0 RID: 480 RVA: 0x000087A9 File Offset: 0x000069A9
			// (set) Token: 0x060001E1 RID: 481 RVA: 0x000087B1 File Offset: 0x000069B1
			public bool UnconditionalBargeIn { get; set; }

			// Token: 0x1700004A RID: 74
			// (get) Token: 0x060001E2 RID: 482 RVA: 0x000087BA File Offset: 0x000069BA
			// (set) Token: 0x060001E3 RID: 483 RVA: 0x000087C2 File Offset: 0x000069C2
			public bool Uninterruptable { get; set; }

			// Token: 0x1700004B RID: 75
			// (get) Token: 0x060001E4 RID: 484 RVA: 0x000087CB File Offset: 0x000069CB
			// (set) Token: 0x060001E5 RID: 485 RVA: 0x000087D3 File Offset: 0x000069D3
			public string TurnName { get; set; }

			// Token: 0x1700004C RID: 76
			// (get) Token: 0x060001E6 RID: 486 RVA: 0x000087DC File Offset: 0x000069DC
			// (set) Token: 0x060001E7 RID: 487 RVA: 0x000087E4 File Offset: 0x000069E4
			public List<UMGrammar> Grammars { get; set; }

			// Token: 0x1700004D RID: 77
			// (get) Token: 0x060001E8 RID: 488 RVA: 0x000087ED File Offset: 0x000069ED
			// (set) Token: 0x060001E9 RID: 489 RVA: 0x000087F5 File Offset: 0x000069F5
			public TimeSpan BabbleTimeout { get; set; }
		}

		// Token: 0x0200002A RID: 42
		internal class RecordInfo
		{
			// Token: 0x1700004E RID: 78
			// (get) Token: 0x060001EB RID: 491 RVA: 0x00008806 File Offset: 0x00006A06
			// (set) Token: 0x060001EC RID: 492 RVA: 0x0000880E File Offset: 0x00006A0E
			public string FileName { get; set; }

			// Token: 0x1700004F RID: 79
			// (get) Token: 0x060001ED RID: 493 RVA: 0x00008817 File Offset: 0x00006A17
			// (set) Token: 0x060001EE RID: 494 RVA: 0x0000881F File Offset: 0x00006A1F
			public TimeSpan EndSilenceTimeout { get; set; }

			// Token: 0x17000050 RID: 80
			// (get) Token: 0x060001EF RID: 495 RVA: 0x00008828 File Offset: 0x00006A28
			// (set) Token: 0x060001F0 RID: 496 RVA: 0x00008830 File Offset: 0x00006A30
			public TimeSpan MaxDuration { get; set; }

			// Token: 0x17000051 RID: 81
			// (get) Token: 0x060001F1 RID: 497 RVA: 0x00008839 File Offset: 0x00006A39
			// (set) Token: 0x060001F2 RID: 498 RVA: 0x00008841 File Offset: 0x00006A41
			public string StopTones { get; set; }

			// Token: 0x17000052 RID: 82
			// (get) Token: 0x060001F3 RID: 499 RVA: 0x0000884A File Offset: 0x00006A4A
			// (set) Token: 0x060001F4 RID: 500 RVA: 0x00008852 File Offset: 0x00006A52
			public bool Append { get; set; }
		}

		// Token: 0x0200002B RID: 43
		private class RecordFileSessionState : UcmaCallSession.SessionState
		{
			// Token: 0x060001F6 RID: 502 RVA: 0x00008864 File Offset: 0x00006A64
			static RecordFileSessionState()
			{
				FilePrompt value = new FilePrompt("Beep.wav", CommonConstants.DefaultCulture);
				UcmaCallSession.RecordFileSessionState.beepFiles.Add(value);
			}

			// Token: 0x060001F7 RID: 503 RVA: 0x000088AC File Offset: 0x00006AAC
			public RecordFileSessionState(UcmaCallSession session, UcmaCallSession.RecordInfo r) : base(session)
			{
				this.recordInfo = r;
				this.tmpWma = TempFileFactory.CreateTempWmaFile();
				this.isSilenceTimerAllowed = true;
				this.InitializeEventArgs();
				this.completionReason = UcmaCallSession.RecordFileSessionState.CompletionReason.None;
				this.SetRecordingFormat();
				this.recordInfo.MaxDuration = UcmaCallSession.SessionState.CalculateMinTimerDueTime(this.recordInfo.MaxDuration - base.Args.TotalRecordTime, TimeSpan.MaxValue);
			}

			// Token: 0x17000053 RID: 83
			// (get) Token: 0x060001F8 RID: 504 RVA: 0x0000891C File Offset: 0x00006B1C
			// (set) Token: 0x060001F9 RID: 505 RVA: 0x00008924 File Offset: 0x00006B24
			internal string TestInfoRecordedFileName { get; set; }

			// Token: 0x17000054 RID: 84
			// (get) Token: 0x060001FA RID: 506 RVA: 0x0000892D File Offset: 0x00006B2D
			public override string Name
			{
				get
				{
					return "RecordFileSessionState";
				}
			}

			// Token: 0x060001FB RID: 507 RVA: 0x00008934 File Offset: 0x00006B34
			public override void Player_SpeakCompleted(object sender, PromptPlayer.PlayerCompletedEventArgs e)
			{
				base.StopWaitFor(UcmaCallSession.SessionState.AsyncOperation.BeepComplete);
			}

			// Token: 0x060001FC RID: 508 RVA: 0x00008944 File Offset: 0x00006B44
			public override void MediaRecorder_StateChanged(object sender, RecorderStateChangedEventArgs e)
			{
				base.Diag.Trace(UcmaCallSession.FormatStateChange<RecorderState>("MediaRecorder", e.PreviousState, e.State), new object[0]);
				if (e.State == 1)
				{
					base.StopWaitFor(UcmaCallSession.SessionState.AsyncOperation.RecordComplete);
					this.UpdateCompletionReason(UcmaCallSession.RecordFileSessionState.CompletionReason.MediaEnded);
					this.FinalizeFile();
				}
			}

			// Token: 0x060001FD RID: 509 RVA: 0x0000899C File Offset: 0x00006B9C
			public override void MediaRecorder_VoiceActivityChanged(object sender, VoiceActivityChangedEventArgs e)
			{
				base.Diag.Trace("MediaRecorder_VoiceActivityChanged Voice={0} Time={1} Silence Timer Allowed={2}", new object[]
				{
					e.IsVoice,
					e.TimeStamp,
					this.isSilenceTimerAllowed
				});
				this.lastVoiceActivity = e;
				if (e.IsVoice)
				{
					base.StopWaitFor(UcmaCallSession.SessionState.AsyncOperation.VoiceActivityTimer);
					this.voiceActivityTimer.Change(-1, 0);
					this.userSpoke = true;
					return;
				}
				if (this.isSilenceTimerAllowed)
				{
					this.voiceActivityTimer.Change(this.recordInfo.EndSilenceTimeout, TimeSpan.Zero);
					base.StartWaitFor(UcmaCallSession.SessionState.AsyncOperation.VoiceActivityTimer);
				}
			}

			// Token: 0x060001FE RID: 510 RVA: 0x00008A4C File Offset: 0x00006C4C
			public override void ToneController_ToneReceived(object sender, ToneControllerEventArgs e)
			{
				base.Args.DtmfDigits = new byte[]
				{
					UcmaCallSession.ToneToByte((byte)e.Tone)
				};
				this.UpdateCompletionReason(UcmaCallSession.RecordFileSessionState.CompletionReason.Tone);
				this.ForceAsyncWaitingCompletions();
			}

			// Token: 0x060001FF RID: 511 RVA: 0x00008A88 File Offset: 0x00006C88
			protected override void OnHoldNotify()
			{
				if (this.voiceActivityTimer != null)
				{
					base.StopWaitFor(UcmaCallSession.SessionState.AsyncOperation.VoiceActivityTimer);
					this.voiceActivityTimer.Change(-1, 0);
				}
			}

			// Token: 0x06000200 RID: 512 RVA: 0x00008AAB File Offset: 0x00006CAB
			protected override void OnResumeNotify()
			{
				if (this.voiceActivityTimer != null && this.isSilenceTimerAllowed)
				{
					this.voiceActivityTimer.Change(this.recordInfo.EndSilenceTimeout, TimeSpan.Zero);
					base.StartWaitFor(UcmaCallSession.SessionState.AsyncOperation.VoiceActivityTimer);
				}
			}

			// Token: 0x06000201 RID: 513 RVA: 0x00008AE4 File Offset: 0x00006CE4
			protected override void InternalStart()
			{
				base.Session.ToneAccumulator.Clear();
				this.StartPlayBeep();
				this.StartRecording();
				this.StartTimers();
			}

			// Token: 0x06000202 RID: 514 RVA: 0x00008B08 File Offset: 0x00006D08
			protected override DisposeTracker InternalGetDisposeTracker()
			{
				return DisposeTracker.Get<UcmaCallSession.RecordFileSessionState>(this);
			}

			// Token: 0x06000203 RID: 515 RVA: 0x00008B10 File Offset: 0x00006D10
			protected override void ForceAsyncWaitingCompletions()
			{
				base.StopWaitFor(UcmaCallSession.SessionState.AsyncOperation.MaxRecordingTimer);
				base.StopWaitFor(UcmaCallSession.SessionState.AsyncOperation.VoiceActivityTimer);
				if (base.IsWaitingFor(UcmaCallSession.SessionState.AsyncOperation.BeepComplete))
				{
					base.Session.Ucma.Player.Cancel();
				}
				if (base.IsWaitingFor(UcmaCallSession.SessionState.AsyncOperation.RecordComplete))
				{
					base.Session.Ucma.MediaRecorder.Stop();
				}
				this.isSilenceTimerAllowed = false;
			}

			// Token: 0x06000204 RID: 516 RVA: 0x00008B80 File Offset: 0x00006D80
			protected override void CompleteFinalAsyncCallback()
			{
				switch (this.completionReason)
				{
				case UcmaCallSession.RecordFileSessionState.CompletionReason.Tone:
					base.Session.Fire<UMCallSessionEventArgs>(base.Session.OnDtmf, base.Args);
					return;
				case UcmaCallSession.RecordFileSessionState.CompletionReason.Timeout:
					base.Session.Fire<UMCallSessionEventArgs>(base.Session.OnComplete, base.Args);
					return;
				case UcmaCallSession.RecordFileSessionState.CompletionReason.Silence:
					base.Session.Fire<UMCallSessionEventArgs>(base.Session.OnComplete, base.Args);
					return;
				case UcmaCallSession.RecordFileSessionState.CompletionReason.MediaEnded:
					base.Session.Fire<UMCallSessionEventArgs>(base.Session.OnHangup, base.Args);
					return;
				default:
					return;
				}
			}

			// Token: 0x06000205 RID: 517 RVA: 0x00008C20 File Offset: 0x00006E20
			protected override void InternalDispose(bool disposing)
			{
				try
				{
					if (this.maxRecordingtimer != null)
					{
						this.maxRecordingtimer.Dispose();
					}
					if (this.voiceActivityTimer != null)
					{
						this.voiceActivityTimer.Dispose();
					}
					if (this.tmpWma != null)
					{
						this.tmpWma.Dispose();
					}
				}
				finally
				{
					base.InternalDispose(disposing);
				}
			}

			// Token: 0x06000206 RID: 518 RVA: 0x00008C80 File Offset: 0x00006E80
			private void MaxRecordingTimer_Expired(object state)
			{
				if (base.IsWaitingFor(UcmaCallSession.SessionState.AsyncOperation.MaxRecordingTimer))
				{
					base.StopWaitFor(UcmaCallSession.SessionState.AsyncOperation.MaxRecordingTimer);
					this.UpdateCompletionReason(UcmaCallSession.RecordFileSessionState.CompletionReason.Timeout);
					this.ForceAsyncWaitingCompletions();
				}
			}

			// Token: 0x06000207 RID: 519 RVA: 0x00008CA7 File Offset: 0x00006EA7
			private void VoiceActivityTimer_Expired(object state)
			{
				if (base.IsWaitingFor(UcmaCallSession.SessionState.AsyncOperation.VoiceActivityTimer))
				{
					base.StopWaitFor(UcmaCallSession.SessionState.AsyncOperation.VoiceActivityTimer);
					this.UpdateCompletionReason(UcmaCallSession.RecordFileSessionState.CompletionReason.Silence);
					this.ForceAsyncWaitingCompletions();
				}
			}

			// Token: 0x06000208 RID: 520 RVA: 0x00008CCE File Offset: 0x00006ECE
			private void StartPlayBeep()
			{
				base.Session.Ucma.Player.Play(UcmaCallSession.RecordFileSessionState.beepFiles, CommonConstants.DefaultCulture, TimeSpan.Zero);
				base.StartWaitFor(UcmaCallSession.SessionState.AsyncOperation.BeepComplete);
			}

			// Token: 0x06000209 RID: 521 RVA: 0x00008D00 File Offset: 0x00006F00
			private void StartRecording()
			{
				WmaFileSink wmaFileSink = new WmaFileSink(this.tmpWma.FilePath);
				wmaFileSink.EncodingFormat = this.format;
				base.Session.Ucma.MediaRecorder.SetSink(wmaFileSink);
				base.Session.Ucma.MediaRecorder.Start();
				base.StartWaitFor(UcmaCallSession.SessionState.AsyncOperation.RecordComplete);
			}

			// Token: 0x0600020A RID: 522 RVA: 0x00008D60 File Offset: 0x00006F60
			private void StartTimers()
			{
				this.StartMaxRecordingTimer();
				this.StartVoiceActivityTimer();
			}

			// Token: 0x0600020B RID: 523 RVA: 0x00008D78 File Offset: 0x00006F78
			private void StartMaxRecordingTimer()
			{
				this.maxRecordingtimer = new Timer(base.EventSubscriber.CreateSerializedTimerCallback(delegate(object r)
				{
					this.MaxRecordingTimer_Expired(r);
				}, "MaxRecordingTimer_Expired"), this, this.recordInfo.MaxDuration, TimeSpan.Zero);
				base.StartWaitFor(UcmaCallSession.SessionState.AsyncOperation.MaxRecordingTimer);
			}

			// Token: 0x0600020C RID: 524 RVA: 0x00008DD4 File Offset: 0x00006FD4
			private void StartVoiceActivityTimer()
			{
				this.voiceActivityTimer = new Timer(base.EventSubscriber.CreateSerializedTimerCallback(delegate(object r)
				{
					this.VoiceActivityTimer_Expired(r);
				}, "VoiceActivityTimer_Expired"), this, this.recordInfo.EndSilenceTimeout, TimeSpan.Zero);
				base.StartWaitFor(UcmaCallSession.SessionState.AsyncOperation.VoiceActivityTimer);
			}

			// Token: 0x0600020D RID: 525 RVA: 0x00008E24 File Offset: 0x00007024
			private void UpdateCompletionReason(UcmaCallSession.RecordFileSessionState.CompletionReason reason)
			{
				if (this.completionReason == UcmaCallSession.RecordFileSessionState.CompletionReason.None)
				{
					this.completionReason = reason;
				}
			}

			// Token: 0x0600020E RID: 526 RVA: 0x00008E38 File Offset: 0x00007038
			private void FinalizeFile()
			{
				if (!base.Session.TestInfo.IsFeatureEnabled(UcmaCallSession.TestInfoFeatures.PlayAudio) || string.IsNullOrEmpty(this.TestInfoRecordedFileName))
				{
					if (this.userSpoke)
					{
						FileInfo fileInfo = new FileInfo(this.tmpWma.FilePath);
						if (!fileInfo.Exists || fileInfo.Length <= 0L)
						{
							return;
						}
						using (ITempFile tempFile = MediaMethods.ToPcm(this.tmpWma))
						{
							using (ITempFile tempFile2 = this.CreateFinalFile(tempFile))
							{
								this.SetFinalFile(tempFile2);
							}
							return;
						}
					}
					Util.TryDeleteFile(this.tmpWma.FilePath);
					return;
				}
				File.Delete(this.recordInfo.FileName);
				File.Copy(this.TestInfoRecordedFileName, this.recordInfo.FileName, true);
				base.Args.RecordTime = this.CalculateRecordingTime(this.recordInfo.FileName);
				base.Args.TotalRecordTime += base.Args.RecordTime;
			}

			// Token: 0x0600020F RID: 527 RVA: 0x00008F60 File Offset: 0x00007160
			private void InitializeEventArgs()
			{
				base.Args.TotalRecordTime = TimeSpan.Zero;
				if (this.recordInfo.Append)
				{
					base.Args.TotalRecordTime = this.CalculateRecordingTime(this.recordInfo.FileName);
				}
			}

			// Token: 0x06000210 RID: 528 RVA: 0x00008F9C File Offset: 0x0000719C
			private TimeSpan CalculateRecordingTime(string filename)
			{
				TimeSpan result = TimeSpan.Zero;
				using (DisposeGuard disposeGuard = default(DisposeGuard))
				{
					if (File.Exists(filename))
					{
						SoundReader soundReader = null;
						if (PcmReader.TryCreate(filename, out soundReader))
						{
							disposeGuard.Add<SoundReader>(soundReader);
							double num = (double)soundReader.WaveDataLength;
							double num2 = (double)((int)soundReader.WaveFormat.BitsPerSample * soundReader.WaveFormat.SamplesPerSec / 8);
							result = TimeSpan.FromSeconds(num / num2);
						}
					}
				}
				return result;
			}

			// Token: 0x06000211 RID: 529 RVA: 0x00009024 File Offset: 0x00007224
			private ITempFile CreateFinalFile(ITempFile newRecording)
			{
				ITempFile tempFile = newRecording;
				this.TrimEndSilence(tempFile);
				base.Args.RecordTime = this.CalculateRecordingTime(tempFile.FilePath);
				base.Args.TotalRecordTime += base.Args.RecordTime;
				if (this.recordInfo.Append)
				{
					tempFile = this.AppendToExistingRecording(tempFile);
				}
				return tempFile;
			}

			// Token: 0x06000212 RID: 530 RVA: 0x00009088 File Offset: 0x00007288
			private void TrimEndSilence(ITempFile file)
			{
				if (this.lastVoiceActivity != null && !this.lastVoiceActivity.IsVoice)
				{
					TimeSpan t = this.CalculateRecordingTime(file.FilePath);
					TimeSpan timeSpan = t - this.lastVoiceActivity.TimeStamp;
					if (timeSpan > UcmaCallSession.RecordFileSessionState.VoiceActivityDetectionLatency)
					{
						base.Diag.Trace("Trimming {0} ms of end silence from file {1} whose original length was {2} ms", new object[]
						{
							timeSpan.TotalMilliseconds,
							file.FilePath,
							t.TotalMilliseconds
						});
						MediaMethods.RemoveAudioFromEnd(file.FilePath, timeSpan);
					}
				}
			}

			// Token: 0x06000213 RID: 531 RVA: 0x00009124 File Offset: 0x00007324
			private ITempFile AppendToExistingRecording(ITempFile newRecording)
			{
				SoundReader soundReader = null;
				ITempFile tempFile = newRecording;
				using (DisposeGuard disposeGuard = default(DisposeGuard))
				{
					if (!string.IsNullOrEmpty(this.recordInfo.FileName) && PcmReader.TryCreate(this.recordInfo.FileName, out soundReader))
					{
						disposeGuard.Add<SoundReader>(soundReader);
						SoundReader soundReader2 = new PcmReader(newRecording.FilePath);
						disposeGuard.Add<SoundReader>(soundReader2);
						tempFile = TempFileFactory.CreateTempWavFile();
						PcmWriter pcmWriter = new PcmWriter(tempFile.FilePath, soundReader.WaveFormat);
						disposeGuard.Add<PcmWriter>(pcmWriter);
						MediaMethods.Append(soundReader, soundReader2, pcmWriter);
					}
				}
				return tempFile;
			}

			// Token: 0x06000214 RID: 532 RVA: 0x000091D0 File Offset: 0x000073D0
			private void SetFinalFile(ITempFile wav)
			{
				File.Delete(this.recordInfo.FileName);
				File.Move(wav.FilePath, this.recordInfo.FileName);
			}

			// Token: 0x06000215 RID: 533 RVA: 0x000091F8 File Offset: 0x000073F8
			private void SetRecordingFormat()
			{
				this.format = 2;
				AudioChannel audioChannel = null;
				if (base.Session.Ucma.Flow.Audio.GetChannels().TryGetValue(0, out audioChannel))
				{
					base.Diag.Trace("AudioChannel receive sampling rate is: {0}.", new object[]
					{
						audioChannel.ReceiveDirectionSamplingRate
					});
					if ((audioChannel.ReceiveDirectionSamplingRate & 2) != null)
					{
						this.format = 3;
					}
				}
				base.Diag.Trace("Setting the recording format to: {0}.", new object[]
				{
					this.format
				});
			}

			// Token: 0x040000AD RID: 173
			private static readonly TimeSpan VoiceActivityDetectionLatency = TimeSpan.FromSeconds(2.0);

			// Token: 0x040000AE RID: 174
			private static ArrayList beepFiles = new ArrayList();

			// Token: 0x040000AF RID: 175
			private UcmaCallSession.RecordInfo recordInfo;

			// Token: 0x040000B0 RID: 176
			private UcmaCallSession.RecordFileSessionState.CompletionReason completionReason;

			// Token: 0x040000B1 RID: 177
			private Timer maxRecordingtimer;

			// Token: 0x040000B2 RID: 178
			private Timer voiceActivityTimer;

			// Token: 0x040000B3 RID: 179
			private ITempFile tmpWma;

			// Token: 0x040000B4 RID: 180
			private WmaEncodingFormat format;

			// Token: 0x040000B5 RID: 181
			private bool userSpoke;

			// Token: 0x040000B6 RID: 182
			private bool isSilenceTimerAllowed;

			// Token: 0x040000B7 RID: 183
			private VoiceActivityChangedEventArgs lastVoiceActivity;

			// Token: 0x0200002C RID: 44
			private enum CompletionReason
			{
				// Token: 0x040000BA RID: 186
				None,
				// Token: 0x040000BB RID: 187
				Tone,
				// Token: 0x040000BC RID: 188
				Timeout,
				// Token: 0x040000BD RID: 189
				Silence,
				// Token: 0x040000BE RID: 190
				MediaEnded
			}
		}

		// Token: 0x0200002D RID: 45
		private class RedirectSessionState : UcmaCallSession.SessionState
		{
			// Token: 0x06000218 RID: 536 RVA: 0x0000928F File Offset: 0x0000748F
			public RedirectSessionState(UcmaCallSession session, string host, int port, int code) : base(session)
			{
				this.host = host;
				this.port = port;
				this.code = code;
			}

			// Token: 0x17000055 RID: 85
			// (get) Token: 0x06000219 RID: 537 RVA: 0x000092AE File Offset: 0x000074AE
			public override string Name
			{
				get
				{
					return "RedirectSessionState";
				}
			}

			// Token: 0x0600021A RID: 538 RVA: 0x000092B8 File Offset: 0x000074B8
			protected override void InternalStart()
			{
				PlatformSipUri redirectContactUri = base.Session.GetRedirectContactUri(this.host, this.port);
				CallForwardOptions callForwardOptions = new CallForwardOptions(this.code);
				base.Session.Ucma.Call.Forward(redirectContactUri.ToString(), callForwardOptions);
				base.Session.DisconnectCall(null);
			}

			// Token: 0x0600021B RID: 539 RVA: 0x00009311 File Offset: 0x00007511
			protected override void ForceAsyncWaitingCompletions()
			{
			}

			// Token: 0x0600021C RID: 540 RVA: 0x00009313 File Offset: 0x00007513
			protected override void CompleteFinalAsyncCallback()
			{
			}

			// Token: 0x0600021D RID: 541 RVA: 0x00009315 File Offset: 0x00007515
			protected override DisposeTracker InternalGetDisposeTracker()
			{
				return DisposeTracker.Get<UcmaCallSession.RedirectSessionState>(this);
			}

			// Token: 0x040000BF RID: 191
			private readonly string host;

			// Token: 0x040000C0 RID: 192
			private readonly int port;

			// Token: 0x040000C1 RID: 193
			private readonly int code;
		}

		// Token: 0x0200002E RID: 46
		private class SendDtmfSessionState : UcmaCallSession.SessionState
		{
			// Token: 0x0600021E RID: 542 RVA: 0x0000931D File Offset: 0x0000751D
			public SendDtmfSessionState(UcmaCallSession session, string dtmfSequence, TimeSpan initialSilence) : base(session)
			{
				this.dtmfSequence = dtmfSequence;
				this.initialSilence = initialSilence;
			}

			// Token: 0x17000056 RID: 86
			// (get) Token: 0x0600021F RID: 543 RVA: 0x00009334 File Offset: 0x00007534
			public override string Name
			{
				get
				{
					return "SendDtmfSessionState";
				}
			}

			// Token: 0x06000220 RID: 544 RVA: 0x00009344 File Offset: 0x00007544
			protected override void InternalStart()
			{
				Thread.Sleep(this.initialSilence);
				foreach (char c in this.dtmfSequence)
				{
					base.Session.Ucma.ToneController.Send(UcmaCallSession.CharToToneId(c));
				}
				base.StartWaitFor(UcmaCallSession.SessionState.AsyncOperation.SendDtmfComplete);
				WaitCallback callBack = base.EventSubscriber.CreateSerializedWaitCallback(delegate(object r)
				{
					this.FinishSendingDtmf(r);
				}, "SendDtmfSessionState_FinishSendingDtmf", true);
				base.Diag.Assert(ThreadPool.QueueUserWorkItem(callBack, this));
			}

			// Token: 0x06000221 RID: 545 RVA: 0x000093D2 File Offset: 0x000075D2
			protected override void ForceAsyncWaitingCompletions()
			{
			}

			// Token: 0x06000222 RID: 546 RVA: 0x000093D4 File Offset: 0x000075D4
			protected override void CompleteFinalAsyncCallback()
			{
				base.Session.Fire<UMCallSessionEventArgs>(base.Session.OnComplete, base.Args);
			}

			// Token: 0x06000223 RID: 547 RVA: 0x000093F2 File Offset: 0x000075F2
			protected override DisposeTracker InternalGetDisposeTracker()
			{
				return DisposeTracker.Get<UcmaCallSession.SendDtmfSessionState>(this);
			}

			// Token: 0x06000224 RID: 548 RVA: 0x000093FA File Offset: 0x000075FA
			private void FinishSendingDtmf(object o)
			{
				base.StopWaitFor(UcmaCallSession.SessionState.AsyncOperation.SendDtmfComplete);
				base.Args.SendDtmfCompleted = true;
			}

			// Token: 0x040000C2 RID: 194
			private readonly string dtmfSequence;

			// Token: 0x040000C3 RID: 195
			private readonly TimeSpan initialSilence;
		}

		// Token: 0x0200002F RID: 47
		private class SendFsmInfoSessionState : UcmaCallSession.SessionState
		{
			// Token: 0x06000226 RID: 550 RVA: 0x00009414 File Offset: 0x00007614
			public SendFsmInfoSessionState(UcmaCallSession session, string callId, string state) : base(session)
			{
				this.message = string.Format(CultureInfo.InvariantCulture, "Call-Id: {0}\r\nCall-State: {1}\r\n", new object[]
				{
					callId,
					state
				});
			}

			// Token: 0x17000057 RID: 87
			// (get) Token: 0x06000227 RID: 551 RVA: 0x0000944D File Offset: 0x0000764D
			public override string Name
			{
				get
				{
					return "SendFsmInfoSessionState";
				}
			}

			// Token: 0x06000228 RID: 552 RVA: 0x00009454 File Offset: 0x00007654
			public override void Call_SendInfoCompleted(IAsyncResult r)
			{
				base.StopWaitFor(UcmaCallSession.SessionState.AsyncOperation.SendMessageComplete);
				try
				{
					base.Session.Ucma.Call.EndSendMessage(r);
				}
				catch (FailureResponseException ex)
				{
					base.Diag.Trace("An exception is hit in sending SIP info for state change: {0}", new object[]
					{
						ex
					});
				}
			}

			// Token: 0x06000229 RID: 553 RVA: 0x000094B4 File Offset: 0x000076B4
			public override void SendMessage(InfoMessage message)
			{
				this.infoReason = UcmaCallSession.SendFsmInfoSessionState.INFOPurpose.ProductInfo;
				base.SendMessage(message);
			}

			// Token: 0x0600022A RID: 554 RVA: 0x000094D8 File Offset: 0x000076D8
			protected override void InternalStart()
			{
				AsyncCallback asyncCallback = base.EventSubscriber.CreateSerializedAsyncCallback(delegate(IAsyncResult r)
				{
					base.Session.CurrentState.Call_SendInfoCompleted(r);
				}, "Call_SendInfoCompleted");
				this.infoReason = UcmaCallSession.SendFsmInfoSessionState.INFOPurpose.TestStateChange;
				ContentDescription contentDescription = new ContentDescription(CommonConstants.ContentTypeTextPlain, this.message);
				base.Session.Ucma.Call.BeginSendMessage(1, contentDescription, new CallSendMessageRequestOptions(), asyncCallback, this);
				base.StartWaitFor(UcmaCallSession.SessionState.AsyncOperation.SendMessageComplete);
			}

			// Token: 0x0600022B RID: 555 RVA: 0x00009544 File Offset: 0x00007744
			protected override void ForceAsyncWaitingCompletions()
			{
			}

			// Token: 0x0600022C RID: 556 RVA: 0x00009546 File Offset: 0x00007746
			protected override void CompleteFinalAsyncCallback()
			{
				if (this.infoReason == UcmaCallSession.SendFsmInfoSessionState.INFOPurpose.TestStateChange)
				{
					base.Session.Fire<UMCallSessionEventArgs>(base.Session.OnStateInfoSent, base.Args);
					return;
				}
				base.Session.Fire<EventArgs>(base.Session.OnMessageSent, null);
			}

			// Token: 0x0600022D RID: 557 RVA: 0x00009584 File Offset: 0x00007784
			protected override DisposeTracker InternalGetDisposeTracker()
			{
				return DisposeTracker.Get<UcmaCallSession.SendFsmInfoSessionState>(this);
			}

			// Token: 0x040000C4 RID: 196
			private readonly string message;

			// Token: 0x040000C5 RID: 197
			private UcmaCallSession.SendFsmInfoSessionState.INFOPurpose infoReason;

			// Token: 0x02000030 RID: 48
			private enum INFOPurpose
			{
				// Token: 0x040000C7 RID: 199
				TestStateChange,
				// Token: 0x040000C8 RID: 200
				ProductInfo
			}
		}

		// Token: 0x02000031 RID: 49
		private class SupervisedTransferSessionState : UcmaCallSession.SessionState
		{
			// Token: 0x0600022F RID: 559 RVA: 0x0000959F File Offset: 0x0000779F
			public SupervisedTransferSessionState(UcmaCallSession session, PlatformSipUri uri, IList<PlatformSignalingHeader> headers) : base(session)
			{
				this.uri = uri;
				List<SignalingHeader> list;
				if (headers != null)
				{
					list = headers.ConvertAll((PlatformSignalingHeader x) => new SignalingHeader(x.Name, x.Value));
				}
				else
				{
					list = null;
				}
				this.headers = list;
			}

			// Token: 0x17000058 RID: 88
			// (get) Token: 0x06000230 RID: 560 RVA: 0x000095DE File Offset: 0x000077DE
			public override string Name
			{
				get
				{
					return "SupervisedTransferSessionState";
				}
			}

			// Token: 0x06000231 RID: 561 RVA: 0x000095E5 File Offset: 0x000077E5
			public override void Call_TransferCompleted(IAsyncResult r)
			{
				base.StopWaitFor(UcmaCallSession.SessionState.AsyncOperation.TransferComplete);
				base.Session.Ucma.Call.EndTransfer(r);
			}

			// Token: 0x06000232 RID: 562 RVA: 0x00009618 File Offset: 0x00007818
			protected override void InternalStart()
			{
				base.Diag.Assert(null != base.Session.DependentSessionDetails);
				base.Diag.Assert(null != base.Session.DependentSessionDetails.DependentUMCallSession);
				CallTransferOptions callTransferOptions = new CallTransferOptions(0);
				if (this.headers != null)
				{
					foreach (SignalingHeader item in this.headers)
					{
						callTransferOptions.Headers.Add(item);
					}
				}
				UcmaDependentCallSession ucmaDependentCallSession = (UcmaDependentCallSession)base.Session.DependentSessionDetails.DependentUMCallSession;
				AsyncCallback asyncCallback = base.EventSubscriber.CreateSerializedAsyncCallback(delegate(IAsyncResult r)
				{
					base.Session.CurrentState.Call_TransferCompleted(r);
				}, "Call_TransferCompleted");
				try
				{
					if (this.uri == null)
					{
						SipUriParser sipUriParser = new SipUriParser(ucmaDependentCallSession.Ucma.Call.RemoteEndpoint.Uri);
						if (sipUriParser.User != null)
						{
							base.Session.Ucma.Call.BeginTransfer(ucmaDependentCallSession.Ucma.Call, callTransferOptions, asyncCallback, this);
						}
						else
						{
							base.Session.Ucma.Call.BeginTransfer(this.GetCompleteReferTargetUri(ucmaDependentCallSession), callTransferOptions, asyncCallback, this);
						}
					}
					else
					{
						base.Session.Ucma.Call.BeginTransfer(this.GetCompleteReferTargetUri(ucmaDependentCallSession), callTransferOptions, asyncCallback, this);
						ucmaDependentCallSession.IgnoreBye = true;
					}
					base.StartWaitFor(UcmaCallSession.SessionState.AsyncOperation.TransferComplete);
				}
				catch (InvalidOperationException ex)
				{
					throw new RealTimeException(ex.Message, ex);
				}
			}

			// Token: 0x06000233 RID: 563 RVA: 0x000097B4 File Offset: 0x000079B4
			protected override void ForceAsyncWaitingCompletions()
			{
			}

			// Token: 0x06000234 RID: 564 RVA: 0x000097B6 File Offset: 0x000079B6
			protected override void CompleteFinalAsyncCallback()
			{
				base.Session.Fire<UMCallSessionEventArgs>(base.Session.OnTransferComplete, base.Args);
			}

			// Token: 0x06000235 RID: 565 RVA: 0x000097D4 File Offset: 0x000079D4
			protected override DisposeTracker InternalGetDisposeTracker()
			{
				return DisposeTracker.Get<UcmaCallSession.SupervisedTransferSessionState>(this);
			}

			// Token: 0x06000236 RID: 566 RVA: 0x000097DC File Offset: 0x000079DC
			private string GetCompleteReferTargetUri(UcmaDependentCallSession cs)
			{
				StringBuilder stringBuilder = new StringBuilder(cs.Ucma.Call.CallId);
				stringBuilder.Append(";from-tag=");
				stringBuilder.Append(cs.Ucma.Call.LocalTag);
				stringBuilder.Append(";to-tag=");
				stringBuilder.Append(cs.Ucma.Call.RemoteTag);
				SignalingHeader signalingHeader = new SignalingHeader("Replaces", stringBuilder.ToString());
				base.Diag.Trace("GetCompleteReferTargetUri: Going to use REPLACES header : {0}", new object[]
				{
					stringBuilder
				});
				SipUriParser sipUriParser;
				if (this.uri != null)
				{
					sipUriParser = new SipUriParser(this.uri.ToString());
				}
				else
				{
					base.Diag.Trace("GetCompleteReferTargetUri: get uri from remote endpoint uri", new object[0]);
					sipUriParser = new SipUriParser(cs.Ucma.Call.RemoteEndpoint.Uri);
					if (sipUriParser.User == null)
					{
						base.Diag.Trace("GetCompleteReferTargetUri: uri user part is null - get user from OriginalDestinationUri", new object[0]);
						SipUriParser sipUriParser2 = new SipUriParser(cs.Ucma.Call.OriginalDestinationUri);
						if (sipUriParser2.User != null)
						{
							sipUriParser.User = sipUriParser2.User;
						}
						else
						{
							base.Diag.Trace("GetCompleteReferTargetUri: user part of OriginalDestinationUri is null", new object[0]);
						}
					}
				}
				sipUriParser.AddHeader(signalingHeader);
				base.Diag.Trace("GetCompleteReferTargetUri: target uri : {0}", new object[]
				{
					sipUriParser
				});
				return sipUriParser.ToString();
			}

			// Token: 0x040000C9 RID: 201
			private PlatformSipUri uri;

			// Token: 0x040000CA RID: 202
			private List<SignalingHeader> headers;
		}

		// Token: 0x02000032 RID: 50
		private enum DisconnectType
		{
			// Token: 0x040000CD RID: 205
			Local,
			// Token: 0x040000CE RID: 206
			Remote
		}

		// Token: 0x02000033 RID: 51
		private class TeardownSessionState : UcmaCallSession.SessionState
		{
			// Token: 0x06000239 RID: 569 RVA: 0x00009950 File Offset: 0x00007B50
			public TeardownSessionState(UcmaCallSession session, UcmaCallSession.DisconnectType disconnectType, PlatformSignalingHeader diagnosticHeader) : this(session, null, disconnectType, diagnosticHeader)
			{
			}

			// Token: 0x0600023A RID: 570 RVA: 0x0000995C File Offset: 0x00007B5C
			public TeardownSessionState(UcmaCallSession session, Exception e, UcmaCallSession.DisconnectType assumedDisconnectType, PlatformSignalingHeader diagnosticHeader) : base(session)
			{
				this.SetDisconnectType(assumedDisconnectType);
				this.SetDiagnosticHeader(diagnosticHeader);
			}

			// Token: 0x17000059 RID: 89
			// (get) Token: 0x0600023B RID: 571 RVA: 0x00009974 File Offset: 0x00007B74
			public override string Name
			{
				get
				{
					return "TeardownSessionState";
				}
			}

			// Token: 0x1700005A RID: 90
			// (get) Token: 0x0600023C RID: 572 RVA: 0x0000997B File Offset: 0x00007B7B
			private bool CallExists
			{
				get
				{
					return base.Session.Ucma != null && null != base.Session.Ucma.Call;
				}
			}

			// Token: 0x0600023D RID: 573 RVA: 0x000099A2 File Offset: 0x00007BA2
			public override void Call_TerminateCompleted(IAsyncResult r)
			{
				base.StopWaitFor(UcmaCallSession.SessionState.AsyncOperation.TerminateComplete);
				base.Session.Ucma.Call.EndTerminate(r);
			}

			// Token: 0x0600023E RID: 574 RVA: 0x000099C5 File Offset: 0x00007BC5
			public override void Call_StateChanged(object sender, CallStateChangedEventArgs e)
			{
				if (e.State == 8)
				{
					base.StopWaitFor(UcmaCallSession.SessionState.AsyncOperation.CallStateTerminated);
				}
				base.Call_StateChanged(sender, e);
			}

			// Token: 0x0600023F RID: 575 RVA: 0x000099E3 File Offset: 0x00007BE3
			protected override void InternalStart()
			{
				base.Session.StopSerializedEvents = true;
				this.ConditionalLogDisconnectEvent();
				this.ConditionalFireOnHangup();
				this.ConditionalBeginSessionTeardown();
			}

			// Token: 0x06000240 RID: 576 RVA: 0x00009A03 File Offset: 0x00007C03
			protected override void CompleteFinalAsyncCallback()
			{
				this.TeardownSession();
			}

			// Token: 0x06000241 RID: 577 RVA: 0x00009A0B File Offset: 0x00007C0B
			protected override void ForceAsyncWaitingCompletions()
			{
			}

			// Token: 0x06000242 RID: 578 RVA: 0x00009A0D File Offset: 0x00007C0D
			protected override DisposeTracker InternalGetDisposeTracker()
			{
				return DisposeTracker.Get<UcmaCallSession.TeardownSessionState>(this);
			}

			// Token: 0x06000243 RID: 579 RVA: 0x00009A18 File Offset: 0x00007C18
			private void ConditionalLogDisconnectEvent()
			{
				if (this.CallExists)
				{
					if (this.disconnectType == UcmaCallSession.DisconnectType.Remote)
					{
						UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_CallEndedByUser, null, new object[]
						{
							base.Session.CallId
						});
						return;
					}
					UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_CallEndedByApplication, null, new object[]
					{
						base.Session.CallId
					});
				}
			}

			// Token: 0x06000244 RID: 580 RVA: 0x00009A85 File Offset: 0x00007C85
			private void ConditionalFireOnHangup()
			{
				if (this.CallExists && this.disconnectType == UcmaCallSession.DisconnectType.Remote)
				{
					base.Session.Fire<UMCallSessionEventArgs>(base.Session.OnHangup, base.Args);
				}
			}

			// Token: 0x06000245 RID: 581 RVA: 0x00009AC8 File Offset: 0x00007CC8
			private void TerminateCall()
			{
				base.Diag.Assert(this.CallExists, "Call must exist in TerminateCall!");
				List<SignalingHeader> list = new List<SignalingHeader>();
				list.Add(new SignalingHeader(this.diagnosticHeader.Name, this.diagnosticHeader.Value));
				AsyncCallback asyncCallback = base.EventSubscriber.CreateSerializedAsyncCallback(delegate(IAsyncResult r)
				{
					base.Session.CurrentState.Call_TerminateCompleted(r);
				}, "Call_TerminateCompleted", true, true);
				Thread.Sleep(250);
				base.Session.Ucma.Call.BeginTerminate(list, asyncCallback, this);
				base.StartWaitFor(UcmaCallSession.SessionState.AsyncOperation.TerminateComplete);
			}

			// Token: 0x06000246 RID: 582 RVA: 0x00009B5F File Offset: 0x00007D5F
			private void ConditionalBeginSessionTeardown()
			{
				if (this.CallExists)
				{
					this.TerminateCall();
					if (base.Session.LastCallStateChangeProcessed != 8)
					{
						base.StartWaitFor(UcmaCallSession.SessionState.AsyncOperation.CallStateTerminated);
						return;
					}
				}
				else
				{
					this.TeardownSession();
				}
			}

			// Token: 0x06000247 RID: 583 RVA: 0x00009B8F File Offset: 0x00007D8F
			private void TeardownSession()
			{
				if (!this.teardownComplete)
				{
					base.Session.Teardown();
					this.teardownComplete = true;
				}
			}

			// Token: 0x06000248 RID: 584 RVA: 0x00009BAB File Offset: 0x00007DAB
			private void SetDisconnectType(UcmaCallSession.DisconnectType assumedDisconnectType)
			{
				if (base.Session.CallStateTransitionReason == 7)
				{
					this.disconnectType = UcmaCallSession.DisconnectType.Local;
					return;
				}
				if (base.Session.CallStateTransitionReason == 8)
				{
					this.disconnectType = UcmaCallSession.DisconnectType.Remote;
					return;
				}
				this.disconnectType = assumedDisconnectType;
			}

			// Token: 0x06000249 RID: 585 RVA: 0x00009BE0 File Offset: 0x00007DE0
			private void SetDiagnosticHeader(PlatformSignalingHeader diagnosticHeader)
			{
				if (diagnosticHeader != null)
				{
					this.diagnosticHeader = diagnosticHeader;
					return;
				}
				this.diagnosticHeader = CallRejectedException.RenderDiagnosticHeader(CallEndingReason.GraceFulTermination, null, new object[0]);
			}

			// Token: 0x040000CF RID: 207
			private UcmaCallSession.DisconnectType disconnectType;

			// Token: 0x040000D0 RID: 208
			private bool teardownComplete;

			// Token: 0x040000D1 RID: 209
			private PlatformSignalingHeader diagnosticHeader;
		}

		// Token: 0x02000034 RID: 52
		internal class SubscriptionHelper : DisposableBase
		{
			// Token: 0x0600024B RID: 587 RVA: 0x00009C04 File Offset: 0x00007E04
			public SubscriptionHelper(UcmaCallSession session)
			{
				this.session = session;
				this.registry = new UcmaCallSession.SubscriptionHelper.EventRegistry();
			}

			// Token: 0x1700005B RID: 91
			// (get) Token: 0x0600024C RID: 588 RVA: 0x00009C1E File Offset: 0x00007E1E
			private DiagnosticHelper Diag
			{
				get
				{
					return this.session.Diag;
				}
			}

			// Token: 0x0600024D RID: 589 RVA: 0x00009CE0 File Offset: 0x00007EE0
			public void SubscribeTo(AudioVideoCall call)
			{
				call.StateChanged += this.CreateSerializedEventHandler<CallStateChangedEventArgs>(delegate(object sender, CallStateChangedEventArgs e)
				{
					this.session.CurrentState.Call_StateChanged(sender, e);
				}, delegate(EventHandler<CallStateChangedEventArgs> x)
				{
					call.StateChanged -= x;
				}, true, "Call_StateChanged");
				call.MediaTroubleshootingDataReported += this.CreateSerializedEventHandler<MediaTroubleshootingDataReportedEventArgs>(delegate(object sender, MediaTroubleshootingDataReportedEventArgs e)
				{
					this.session.CurrentState.Call_MediaTroubleshootingDataReported(sender, e);
				}, delegate(EventHandler<MediaTroubleshootingDataReportedEventArgs> x)
				{
					call.MediaTroubleshootingDataReported -= x;
				}, true, "Call_QoeDataReported");
				call.AudioVideoFlowConfigurationRequested += this.CreateSerializedEventHandler<AudioVideoFlowConfigurationRequestedEventArgs>(delegate(object sender, AudioVideoFlowConfigurationRequestedEventArgs e)
				{
					this.session.CurrentState.Call_AudioVideoFlowConfigurationRequested(sender, e);
				}, delegate(EventHandler<AudioVideoFlowConfigurationRequestedEventArgs> x)
				{
					call.AudioVideoFlowConfigurationRequested -= x;
				}, "Call_AudioVideoFlowConfigurationRequested");
				call.InfoReceived += this.CreateSerializedEventHandler<MessageReceivedEventArgs>(delegate(object sender, MessageReceivedEventArgs e)
				{
					this.session.CurrentState.Call_InfoReceived(sender, e);
				}, delegate(EventHandler<MessageReceivedEventArgs> x)
				{
					call.InfoReceived -= x;
				}, "Call_InfoReceived");
				call.RemoteParticipantChanged += this.CreateSerializedEventHandler<RemoteParticipantChangedEventArgs>(delegate(object sender, RemoteParticipantChangedEventArgs e)
				{
					this.session.CurrentState.Call_RemoteParticipantChanged(sender, e);
				}, delegate(EventHandler<RemoteParticipantChangedEventArgs> x)
				{
					call.RemoteParticipantChanged -= x;
				}, "Call_RemoteParticipantChanged");
			}

			// Token: 0x0600024E RID: 590 RVA: 0x00009E38 File Offset: 0x00008038
			public void SubscribeTo(AudioVideoFlow flow)
			{
				flow.StateChanged += this.CreateSerializedEventHandler<MediaFlowStateChangedEventArgs>(delegate(object xsender, MediaFlowStateChangedEventArgs xe)
				{
					this.session.CurrentState.Flow_StateChanged(xsender, xe);
				}, delegate(EventHandler<MediaFlowStateChangedEventArgs> x)
				{
					flow.StateChanged -= x;
				}, "Flow_StateChanged");
				flow.ConfigurationChanged += this.CreateSerializedEventHandler<AudioVideoFlowConfigurationChangedEventArgs>(delegate(object xsender, AudioVideoFlowConfigurationChangedEventArgs xe)
				{
					this.session.CurrentState.Flow_ConfigurationChanged(xsender, xe);
				}, delegate(EventHandler<AudioVideoFlowConfigurationChangedEventArgs> x)
				{
					flow.ConfigurationChanged -= x;
				}, "Flow_ConfigurationChanged");
			}

			// Token: 0x0600024F RID: 591 RVA: 0x00009F04 File Offset: 0x00008104
			public void SubscribeTo(PromptPlayer player)
			{
				player.BookmarkReached += this.CreateSerializedEventHandler<BookmarkReachedEventArgs>(delegate(object sender, BookmarkReachedEventArgs e)
				{
					this.session.CurrentState.Player_BookmarkReached(sender, e);
				}, delegate(EventHandler<BookmarkReachedEventArgs> x)
				{
					player.BookmarkReached -= x;
				}, "Player_BookmarkReached");
				player.SpeakCompleted += this.CreateSerializedEventHandler<PromptPlayer.PlayerCompletedEventArgs>(delegate(object sender, PromptPlayer.PlayerCompletedEventArgs e)
				{
					this.session.CurrentState.Player_SpeakCompleted(sender, e);
				}, delegate(EventHandler<PromptPlayer.PlayerCompletedEventArgs> x)
				{
					player.SpeakCompleted -= x;
				}, "Player_SpeakCompleted");
			}

			// Token: 0x06000250 RID: 592 RVA: 0x00009FD0 File Offset: 0x000081D0
			public void SubscribeTo(Recorder mediaRecorder)
			{
				mediaRecorder.StateChanged += this.CreateSerializedEventHandler<RecorderStateChangedEventArgs>(delegate(object sender, RecorderStateChangedEventArgs args)
				{
					this.session.CurrentState.MediaRecorder_StateChanged(sender, args);
				}, delegate(EventHandler<RecorderStateChangedEventArgs> x)
				{
					mediaRecorder.StateChanged -= x;
				}, "MediaRecorder_StateChanged");
				mediaRecorder.VoiceActivityChanged += this.CreateSerializedEventHandler<VoiceActivityChangedEventArgs>(delegate(object sender, VoiceActivityChangedEventArgs args)
				{
					this.session.CurrentState.MediaRecorder_VoiceActivityChanged(sender, args);
				}, delegate(EventHandler<VoiceActivityChangedEventArgs> x)
				{
					mediaRecorder.VoiceActivityChanged -= x;
				}, "MediaRecorder_VoiceActivityChanged");
			}

			// Token: 0x06000251 RID: 593 RVA: 0x0000A09C File Offset: 0x0000829C
			public void SubscribeTo(ToneController toneController)
			{
				toneController.ToneReceived += this.CreateSerializedEventHandler<ToneControllerEventArgs>(delegate(object sender, ToneControllerEventArgs args)
				{
					this.session.CurrentState.ToneController_ToneReceived(sender, args);
				}, delegate(EventHandler<ToneControllerEventArgs> x)
				{
					toneController.ToneReceived -= x;
				}, "ToneController_ToneReceived");
				toneController.IncomingFaxDetected += this.CreateSerializedEventHandler<IncomingFaxDetectedEventArgs>(delegate(object sender, IncomingFaxDetectedEventArgs args)
				{
					this.session.CurrentState.ToneController_IncomingFaxDetected(sender, args);
				}, delegate(EventHandler<IncomingFaxDetectedEventArgs> x)
				{
					toneController.IncomingFaxDetected -= x;
				}, "ToneController_IncomingFaxDetected");
			}

			// Token: 0x06000252 RID: 594 RVA: 0x0000A1AC File Offset: 0x000083AC
			public void SubscribeTo(SpeechRecognitionEngine speechReco)
			{
				speechReco.RecognizeCompleted += this.CreateSerializedEventHandler<RecognizeCompletedEventArgs>(delegate(object sender, RecognizeCompletedEventArgs args)
				{
					this.session.CurrentState.SpeechReco_RecognizeCompleted(sender, args);
				}, delegate(EventHandler<RecognizeCompletedEventArgs> x)
				{
					speechReco.RecognizeCompleted -= x;
				}, "SpeechReco_RecognizeCompleted");
				speechReco.SpeechHypothesized += this.CreateSerializedEventHandler<SpeechHypothesizedEventArgs>(delegate(object sender, SpeechHypothesizedEventArgs args)
				{
					this.session.CurrentState.SpeechReco_SpeechHypothesized(sender, args);
				}, delegate(EventHandler<SpeechHypothesizedEventArgs> x)
				{
					speechReco.SpeechHypothesized -= x;
				}, "SpeechReco_SpeechHypothesized");
				speechReco.EmulateRecognizeCompleted += this.CreateSerializedEventHandler<EmulateRecognizeCompletedEventArgs>(delegate(object sender, EmulateRecognizeCompletedEventArgs args)
				{
					this.session.CurrentState.SpeechReco_EmulateRecognizeCompleted(sender, args);
				}, delegate(EventHandler<EmulateRecognizeCompletedEventArgs> x)
				{
					speechReco.EmulateRecognizeCompleted -= x;
				}, "SpeechReco_EmulateRecognizeCompleted");
				speechReco.SpeechDetected += this.CreateSerializedEventHandler<SpeechDetectedEventArgs>(delegate(object sender, SpeechDetectedEventArgs args)
				{
					this.session.CurrentState.SpeechReco_SpeechDetected(sender, args);
				}, delegate(EventHandler<SpeechDetectedEventArgs> x)
				{
					speechReco.SpeechDetected -= x;
				}, "SpeechReco_SpeechDetected");
			}

			// Token: 0x06000253 RID: 595 RVA: 0x0000A30C File Offset: 0x0000850C
			public WaitCallback CreateSerializedWaitCallback(SerializableCallback<object> callback, string callbackTraceName, bool assertState)
			{
				return delegate(object s)
				{
					SerializableCallback<object> callback2 = assertState ? this.WrapCallbackAndAssertState<object>(callback, (UcmaCallSession.SessionState)s, callbackTraceName) : this.WrapCallbackAndIgnoreState<object>(callback, (UcmaCallSession.SessionState)s, callbackTraceName);
					this.session.Serializer.SerializeCallback<object>(s, callback2, this.session, false, callbackTraceName);
				};
			}

			// Token: 0x06000254 RID: 596 RVA: 0x0000A3A4 File Offset: 0x000085A4
			public TimerCallback CreateSerializedTimerCallback(SerializableCallback<object> callback, string callbackTraceName)
			{
				return delegate(object s)
				{
					SerializableCallback<object> callback2 = this.WrapCallbackAndIgnoreState<object>(callback, (UcmaCallSession.SessionState)s, callbackTraceName);
					this.session.Serializer.SerializeCallback<object>(s, callback2, this.session, false, callbackTraceName);
				};
			}

			// Token: 0x06000255 RID: 597 RVA: 0x0000A3D8 File Offset: 0x000085D8
			public AsyncCallback CreateSerializedAsyncCallback(SerializableCallback<IAsyncResult> callback, string callbackTraceName)
			{
				return this.CreateSerializedAsyncCallback(callback, callbackTraceName, true);
			}

			// Token: 0x06000256 RID: 598 RVA: 0x0000A3E3 File Offset: 0x000085E3
			public AsyncCallback CreateSerializedAsyncCallback(SerializableCallback<IAsyncResult> callback, string callbackTraceName, bool assertState)
			{
				return this.CreateSerializedAsyncCallback(callback, callbackTraceName, assertState, false);
			}

			// Token: 0x06000257 RID: 599 RVA: 0x0000A484 File Offset: 0x00008684
			public AsyncCallback CreateSerializedAsyncCallback(SerializableCallback<IAsyncResult> callback, string callbackTraceName, bool assertState, bool forceCallback)
			{
				return delegate(IAsyncResult s)
				{
					SerializableCallback<IAsyncResult> callback2 = assertState ? this.WrapCallbackAndAssertState<IAsyncResult>(callback, (UcmaCallSession.SessionState)s.AsyncState, callbackTraceName) : this.WrapCallbackAndIgnoreState<IAsyncResult>(callback, (UcmaCallSession.SessionState)s.AsyncState, callbackTraceName);
					this.session.Serializer.SerializeCallback<IAsyncResult>(s, callback2, this.session, forceCallback, callbackTraceName);
				};
			}

			// Token: 0x06000258 RID: 600 RVA: 0x0000A4C7 File Offset: 0x000086C7
			protected override DisposeTracker InternalGetDisposeTracker()
			{
				return DisposeTracker.Get<UcmaCallSession.SubscriptionHelper>(this);
			}

			// Token: 0x06000259 RID: 601 RVA: 0x0000A4CF File Offset: 0x000086CF
			protected override void InternalDispose(bool disposing)
			{
				this.registry.Dispose();
			}

			// Token: 0x0600025A RID: 602 RVA: 0x0000A4DC File Offset: 0x000086DC
			private SerializableCallback<TState> WrapCallbackAndAssertState<TState>(SerializableCallback<TState> callback, UcmaCallSession.SessionState callbackState, string callbackTraceName)
			{
				return this.WrapCallback<TState>(callback, callbackState, callbackTraceName, true);
			}

			// Token: 0x0600025B RID: 603 RVA: 0x0000A4E8 File Offset: 0x000086E8
			private SerializableCallback<TState> WrapCallbackAndIgnoreState<TState>(SerializableCallback<TState> callback, UcmaCallSession.SessionState callbackState, string callbackTraceName)
			{
				return this.WrapCallback<TState>(callback, callbackState, callbackTraceName, false);
			}

			// Token: 0x0600025C RID: 604 RVA: 0x0000A72C File Offset: 0x0000892C
			private SerializableCallback<TState> WrapCallback<TState>(SerializableCallback<TState> callback, UcmaCallSession.SessionState callbackState, string callbackTraceName, bool assertState)
			{
				return delegate(TState o)
				{
					using (new CallId(this.session.CallId))
					{
						this.session.CatchAndFireOnError(delegate
						{
							this.Diag.Trace("Event: {0}::{1}", new object[]
							{
								this.session.CurrentState.Name,
								callbackTraceName
							});
							if (this.IsDisposed)
							{
								this.Diag.Trace("Ignoring callback for state {0} because the subscription manager has been disposed", new object[]
								{
									callbackState.Name
								});
								return;
							}
							if (callbackState.Equals(this.session.CurrentState))
							{
								callback(o);
								this.session.CurrentState.CompleteAsyncCallback();
								return;
							}
							if (assertState)
							{
								this.Diag.Assert(false, "callback state {0} is not equal to current state {1} !", new object[]
								{
									callbackState.Name,
									this.session.CurrentState.Name
								});
								return;
							}
							this.Diag.Trace("Ignoring callback because current state {0} does not equal callback state {1}", new object[]
							{
								callbackState.Name,
								this.session.CurrentState.Name
							});
						});
					}
				};
			}

			// Token: 0x0600025D RID: 605 RVA: 0x0000A771 File Offset: 0x00008971
			private EventHandler<TArgs> CreateSerializedEventHandler<TArgs>(SerializableEventHandler<TArgs> callback, UcmaCallSession.SubscriptionHelper.EventRegistry.UnregisterEventHandler<TArgs> unregister, string eventTraceName) where TArgs : EventArgs
			{
				return this.CreateSerializedEventHandler<TArgs>(callback, unregister, false, eventTraceName);
			}

			// Token: 0x0600025E RID: 606 RVA: 0x0000A90C File Offset: 0x00008B0C
			private EventHandler<TArgs> CreateSerializedEventHandler<TArgs>(SerializableEventHandler<TArgs> callback, UcmaCallSession.SubscriptionHelper.EventRegistry.UnregisterEventHandler<TArgs> unregister, bool forceEvent, string eventTraceName) where TArgs : EventArgs
			{
				SerializableEventHandler<TArgs> wrappedHandler = delegate(object sender, TArgs args)
				{
					using (new CallId(this.session.CallId))
					{
						this.session.CatchAndFireOnError(delegate
						{
							this.Diag.Trace("Event: {0}::{1}", new object[]
							{
								this.session.CurrentState.Name,
								eventTraceName
							});
							if (this.IsDisposed)
							{
								this.Diag.Trace("Ignoring event handler because the subscription manager has been disposed", new object[0]);
								return;
							}
							callback(sender, args);
							this.session.CurrentState.CompleteAsyncCallback();
						});
					}
				};
				EventHandler<TArgs> eventHandler = delegate(object sender, TArgs args)
				{
					this.session.Serializer.SerializeEvent<TArgs>(sender, args, wrappedHandler, this.session, forceEvent, eventTraceName);
				};
				this.registry.Register(unregister, eventHandler);
				return eventHandler;
			}

			// Token: 0x040000D2 RID: 210
			private UcmaCallSession.SubscriptionHelper.EventRegistry registry;

			// Token: 0x040000D3 RID: 211
			private UcmaCallSession session;

			// Token: 0x02000035 RID: 53
			private class EventRegistry : DisposableBase
			{
				// Token: 0x06000270 RID: 624 RVA: 0x0000A969 File Offset: 0x00008B69
				public void Register(Delegate unsubscribeDelegate, Delegate eventHandler)
				{
					this.registrations.Add(new KeyValuePair<Delegate, Delegate>(unsubscribeDelegate, eventHandler));
				}

				// Token: 0x06000271 RID: 625 RVA: 0x0000A980 File Offset: 0x00008B80
				protected override void InternalDispose(bool disposing)
				{
					if (disposing)
					{
						foreach (KeyValuePair<Delegate, Delegate> keyValuePair in this.registrations)
						{
							keyValuePair.Key.DynamicInvoke(new object[]
							{
								keyValuePair.Value
							});
						}
					}
				}

				// Token: 0x06000272 RID: 626 RVA: 0x0000A9F0 File Offset: 0x00008BF0
				protected override DisposeTracker InternalGetDisposeTracker()
				{
					return DisposeTracker.Get<UcmaCallSession.SubscriptionHelper.EventRegistry>(this);
				}

				// Token: 0x040000D4 RID: 212
				private List<KeyValuePair<Delegate, Delegate>> registrations = new List<KeyValuePair<Delegate, Delegate>>(32);

				// Token: 0x02000036 RID: 54
				// (Invoke) Token: 0x06000275 RID: 629
				public delegate void UnregisterEventHandler<TArgs>(EventHandler<TArgs> handler) where TArgs : EventArgs;
			}
		}
	}
}
