using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x0200000C RID: 12
	internal abstract class ActivityManager : ActivityBase
	{
		// Token: 0x0600007C RID: 124 RVA: 0x000038C4 File Offset: 0x00001AC4
		internal ActivityManager(ActivityManager manager, ActivityManagerConfig config) : base(manager, config)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, null, "ActivityManager being constructed manager={0}, config={1}.", new object[]
			{
				manager,
				config
			});
			this.contextVariables = new Hashtable();
			this.currentActivity = config.InitialActivity.CreateActivity(this);
			this.playBackCtx = new PlayBackContext();
			this.recCtx = new RecordContext();
			this.msgPlayerCtx = new MessagePlayerContext();
			if (manager != null)
			{
				this.RecoResult = manager.RecoResult;
				this.WriteVariable("lastActivity", base.Manager.LastActivity);
				this.WriteVariable("lastRecoEvent", base.Manager.LastRecoEvent);
				this.WriteVariable("useAsr", base.Manager.UseASR);
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00003998 File Offset: 0x00001B98
		// (set) Token: 0x0600007E RID: 126 RVA: 0x000039A0 File Offset: 0x00001BA0
		internal bool IsSentImportant
		{
			get
			{
				return this.isSentImportant;
			}
			set
			{
				this.isSentImportant = value;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600007F RID: 127 RVA: 0x000039A9 File Offset: 0x00001BA9
		// (set) Token: 0x06000080 RID: 128 RVA: 0x000039B1 File Offset: 0x00001BB1
		internal bool MessageMarkedPrivate
		{
			get
			{
				return this.messageMarkedPrivate;
			}
			set
			{
				this.messageMarkedPrivate = value;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000081 RID: 129 RVA: 0x000039BA File Offset: 0x00001BBA
		// (set) Token: 0x06000082 RID: 130 RVA: 0x000039C2 File Offset: 0x00001BC2
		internal bool IsQuickMessage
		{
			get
			{
				return this.isQuickMessage;
			}
			set
			{
				this.isQuickMessage = value;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000083 RID: 131 RVA: 0x000039CB File Offset: 0x00001BCB
		// (set) Token: 0x06000084 RID: 132 RVA: 0x000039D3 File Offset: 0x00001BD3
		internal bool MessageHasBeenSentWithHighImportance { get; set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000085 RID: 133 RVA: 0x000039DC File Offset: 0x00001BDC
		// (set) Token: 0x06000086 RID: 134 RVA: 0x000039E4 File Offset: 0x00001BE4
		internal PhoneNumber TargetPhoneNumber { get; set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000087 RID: 135 RVA: 0x000039ED File Offset: 0x00001BED
		// (set) Token: 0x06000088 RID: 136 RVA: 0x000039F5 File Offset: 0x00001BF5
		internal int NumericInput
		{
			get
			{
				return this.numberInput;
			}
			set
			{
				this.numberInput = value;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000089 RID: 137 RVA: 0x000039FE File Offset: 0x00001BFE
		// (set) Token: 0x0600008A RID: 138 RVA: 0x00003A06 File Offset: 0x00001C06
		internal string DtmfDigits
		{
			get
			{
				return this.dtmfDigits;
			}
			set
			{
				this.dtmfDigits = value;
				if (base.Manager != null)
				{
					base.Manager.DtmfDigits = value;
				}
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600008B RID: 139 RVA: 0x00003A23 File Offset: 0x00001C23
		// (set) Token: 0x0600008C RID: 140 RVA: 0x00003A2B File Offset: 0x00001C2B
		internal EncryptedBuffer Password
		{
			get
			{
				return this.password;
			}
			set
			{
				this.password = value;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600008D RID: 141 RVA: 0x00003A34 File Offset: 0x00001C34
		// (set) Token: 0x0600008E RID: 142 RVA: 0x00003A3C File Offset: 0x00001C3C
		internal PlayBackContext PlayBackContext
		{
			get
			{
				return this.playBackCtx;
			}
			set
			{
				this.playBackCtx = value;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00003A45 File Offset: 0x00001C45
		// (set) Token: 0x06000090 RID: 144 RVA: 0x00003A4D File Offset: 0x00001C4D
		internal RecordContext RecordContext
		{
			get
			{
				return this.recCtx;
			}
			set
			{
				this.recCtx = value;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00003A56 File Offset: 0x00001C56
		// (set) Token: 0x06000092 RID: 146 RVA: 0x00003A5E File Offset: 0x00001C5E
		internal MessagePlayerContext MessagePlayerContext
		{
			get
			{
				return this.msgPlayerCtx;
			}
			set
			{
				this.msgPlayerCtx = value;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00003A67 File Offset: 0x00001C67
		// (set) Token: 0x06000094 RID: 148 RVA: 0x00003A6F File Offset: 0x00001C6F
		internal Shortcut LastShortcut
		{
			get
			{
				return this.lastShortcut;
			}
			set
			{
				this.lastShortcut = value;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000095 RID: 149 RVA: 0x00003A78 File Offset: 0x00001C78
		internal BaseUMCallSession CallSession
		{
			get
			{
				return this.callSession;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000096 RID: 150 RVA: 0x00003A80 File Offset: 0x00001C80
		// (set) Token: 0x06000097 RID: 151 RVA: 0x00003A88 File Offset: 0x00001C88
		internal IUMRecognitionResult RecoResult
		{
			get
			{
				return this.recoResult;
			}
			set
			{
				this.recoResult = value;
				this.LastRecoEvent = ((value == null) ? string.Empty : (value["RecoEvent"] as string));
				if (base.Manager != null)
				{
					base.Manager.RecoResult = value;
				}
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000098 RID: 152 RVA: 0x00003AC5 File Offset: 0x00001CC5
		// (set) Token: 0x06000099 RID: 153 RVA: 0x00003ACD File Offset: 0x00001CCD
		internal string LastBookmarkReached
		{
			get
			{
				return this.lastBookmarkReached;
			}
			set
			{
				this.lastBookmarkReached = value;
				if (base.Manager != null)
				{
					base.Manager.LastBookmarkReached = value;
				}
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600009A RID: 154 RVA: 0x00003AEC File Offset: 0x00001CEC
		// (set) Token: 0x0600009B RID: 155 RVA: 0x00003B14 File Offset: 0x00001D14
		internal string LastRecoEvent
		{
			get
			{
				string text = this.ReadVariable("lastRecoEvent") as string;
				return text ?? string.Empty;
			}
			set
			{
				string text = value ?? string.Empty;
				this.WriteVariable("lastRecoEvent", text);
				if (base.Manager != null)
				{
					base.Manager.LastRecoEvent = text;
				}
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600009C RID: 156 RVA: 0x00003B4C File Offset: 0x00001D4C
		// (set) Token: 0x0600009D RID: 157 RVA: 0x00003B74 File Offset: 0x00001D74
		internal string LastActivity
		{
			get
			{
				string text = this.ReadVariable("lastActivity") as string;
				return text ?? string.Empty;
			}
			set
			{
				string text = value ?? string.Empty;
				this.WriteVariable("lastActivity", text);
				if (base.Manager != null)
				{
					base.Manager.LastActivity = text;
				}
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600009E RID: 158 RVA: 0x00003BAC File Offset: 0x00001DAC
		// (set) Token: 0x0600009F RID: 159 RVA: 0x00003BD0 File Offset: 0x00001DD0
		internal bool UseASR
		{
			get
			{
				object obj = this.ReadVariable("useAsr");
				return obj == null || (bool)obj;
			}
			set
			{
				this.WriteVariable("useAsr", value);
				if (base.Manager != null)
				{
					base.Manager.UseASR = value;
				}
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x00003BF7 File Offset: 0x00001DF7
		internal virtual DirectoryGrammarHandler DirectoryGrammarHandler
		{
			get
			{
				return this.GlobalManager.DirectoryGrammarHandler;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x00003C04 File Offset: 0x00001E04
		internal virtual GlobalActivityManager GlobalManager
		{
			get
			{
				return base.Manager.GlobalManager;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x00003C11 File Offset: 0x00001E11
		internal virtual bool LargeGrammarsNeeded
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x00003C14 File Offset: 0x00001E14
		// (set) Token: 0x060000A4 RID: 164 RVA: 0x00003C21 File Offset: 0x00001E21
		internal virtual float ProsodyRate
		{
			get
			{
				return this.GlobalManager.ProsodyRate;
			}
			set
			{
				this.GlobalManager.ProsodyRate = value;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x00003C30 File Offset: 0x00001E30
		internal int LastInputNum
		{
			get
			{
				int result = 0;
				if (int.TryParse((string)this.ReadVariable("lastInput"), NumberStyles.Number, CultureInfo.InvariantCulture, out result))
				{
					return result;
				}
				return 0;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x00003C62 File Offset: 0x00001E62
		// (set) Token: 0x060000A7 RID: 167 RVA: 0x00003C6A File Offset: 0x00001E6A
		protected ActivityBase CurrentActivity
		{
			get
			{
				return this.currentActivity;
			}
			set
			{
				this.currentActivity = value;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x00003C73 File Offset: 0x00001E73
		// (set) Token: 0x060000A9 RID: 169 RVA: 0x00003C7B File Offset: 0x00001E7B
		protected bool PlayingLastMenu
		{
			get
			{
				return this.playingLastMenu;
			}
			set
			{
				this.playingLastMenu = value;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000AA RID: 170 RVA: 0x00003C84 File Offset: 0x00001E84
		// (set) Token: 0x060000AB RID: 171 RVA: 0x00003C8C File Offset: 0x00001E8C
		protected bool PlayingSystemPrompt
		{
			get
			{
				return this.playingSystemPrompt;
			}
			set
			{
				this.playingSystemPrompt = value;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000AC RID: 172 RVA: 0x00003C95 File Offset: 0x00001E95
		// (set) Token: 0x060000AD RID: 173 RVA: 0x00003C9D File Offset: 0x00001E9D
		protected IntroType MessageIntroType
		{
			get
			{
				return this.messageIntroType;
			}
			set
			{
				this.messageIntroType = value;
			}
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00003CA8 File Offset: 0x00001EA8
		internal override void Start(BaseUMCallSession vo, string refInfo)
		{
			this.navigationState = UMNavigationState.Success;
			this.navigationStateMessage = string.Empty;
			this.navigationLogger = vo.LoggingManager;
			this.navigationLogger.EnterTask(base.UniqueId);
			this.FetchGalGrammar(vo);
			this.callSession = vo;
			this.CheckAuthorization(vo.CurrentCallContext.CallerInfo);
			this.currentActivity.Start(vo, refInfo);
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00003D10 File Offset: 0x00001F10
		internal virtual void CheckAuthorization(UMSubscriber u)
		{
			if (!u.IsAuthenticated)
			{
				PIIMessage data = PIIMessage.Create(PIIType._User, u);
				CallIdTracer.TraceError(ExTraceGlobals.StateMachineTracer, this, data, "User=_User unauthenticated in activity={0}.", new object[]
				{
					this
				});
				throw new UmAuthorizationException(u.ToString(), this.ToString());
			}
			if (this.GlobalManager.LimitedOVAAccess)
			{
				PIIMessage data2 = PIIMessage.Create(PIIType._User, u);
				CallIdTracer.TraceError(ExTraceGlobals.StateMachineTracer, this, data2, "Activity {0} is not allowed in limited OVA Access mode. User = _User", new object[]
				{
					this
				});
				throw new UmAuthorizationException(u.ToString(), this.ToString());
			}
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00003DA0 File Offset: 0x00001FA0
		internal override void OnSpeech(object sender, UMSpeechEventArgs args)
		{
			BaseUMCallSession baseUMCallSession = (BaseUMCallSession)sender;
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "ActivityManager={0} received speech.", new object[]
			{
				base.GetType()
			});
			this.currentActivity.OnSpeech(sender, args);
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00003DE2 File Offset: 0x00001FE2
		internal override void OnOutBoundCallRequestCompleted(BaseUMCallSession vo, OutboundCallDetailsEventArgs callSessionEventArgs)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "Activity Manager : OnOutBoundCallRequestCompleted", new object[0]);
			this.currentActivity.OnOutBoundCallRequestCompleted(vo, callSessionEventArgs);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00003E07 File Offset: 0x00002007
		internal override void OnInput(BaseUMCallSession vo, UMCallSessionEventArgs callSessionEventArgs)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "Received DTMF input.", new object[0]);
			this.currentActivity.OnInput(vo, callSessionEventArgs);
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00003E2C File Offset: 0x0000202C
		internal override void OnCancelled(BaseUMCallSession vo, UMCallSessionEventArgs callSessionEventArgs)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "All operations cancelled.", new object[0]);
			this.currentActivity.OnCancelled(vo, callSessionEventArgs);
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00003E54 File Offset: 0x00002054
		internal override void OnUserHangup(BaseUMCallSession vo, UMCallSessionEventArgs callSessionEventArgs)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "User hangup.", new object[0]);
			if (this.playingLastMenu)
			{
				vo.DisconnectCall();
				return;
			}
			if (this.playingSystemPrompt)
			{
				this.playingSystemPrompt = false;
			}
			this.currentActivity.OnUserHangup(vo, callSessionEventArgs);
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00003EA2 File Offset: 0x000020A2
		internal override void OnDispose(BaseUMCallSession vo, EventArgs e)
		{
			this.currentActivity.OnDispose(vo, e);
			this.Dispose();
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00003EB8 File Offset: 0x000020B8
		internal override void OnComplete(BaseUMCallSession vo, UMCallSessionEventArgs callSessionEventArgs)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "Prompt completed.", new object[0]);
			if (this.playingLastMenu)
			{
				vo.DisconnectCall();
				return;
			}
			if (this.playingSystemPrompt)
			{
				this.playingSystemPrompt = false;
				this.currentActivity.Start(vo, null);
				return;
			}
			this.currentActivity.OnComplete(vo, callSessionEventArgs);
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00003F14 File Offset: 0x00002114
		internal override void OnTimeout(BaseUMCallSession vo, UMCallSessionEventArgs callSessionEventArgs)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "Timeout.", new object[0]);
			if (this.playingLastMenu)
			{
				vo.DisconnectCall();
				return;
			}
			if (this.playingSystemPrompt)
			{
				this.playingSystemPrompt = false;
				this.currentActivity.Start(vo, null);
				return;
			}
			this.currentActivity.OnTimeout(vo, callSessionEventArgs);
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00003F70 File Offset: 0x00002170
		internal override void OnStateInfoSent(BaseUMCallSession vo, UMCallSessionEventArgs callSessionEventArgs)
		{
			base.OnStateInfoSent(vo, callSessionEventArgs);
			this.currentActivity.OnStateInfoSent(vo, callSessionEventArgs);
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00003F88 File Offset: 0x00002188
		internal override void OnError(BaseUMCallSession vo, UMCallSessionEventArgs callSessionEventArgs)
		{
			if (callSessionEventArgs.Error is UmAuthorizationException)
			{
				ExceptionHandling.SendWatsonWithoutDump(callSessionEventArgs.Error);
				callSessionEventArgs.Error = null;
				CallIdTracer.TraceError(ExTraceGlobals.StateMachineTracer, this, "Manager {0} received UMauthorizationException. Dropping the call. ", new object[]
				{
					this
				});
				this.DropCall(vo, DropCallReason.SystemError);
				this.GetTransition("stopEvent").Execute(this, vo);
				return;
			}
			if (this.playingLastMenu)
			{
				CallIdTracer.TraceError(ExTraceGlobals.StateMachineTracer, this, "Manager {0} got an exception trying to play the last menu prompt. Giving up.", new object[]
				{
					this
				});
				vo.DisconnectCall();
				return;
			}
			this.currentActivity.OnError(vo, callSessionEventArgs);
			if (callSessionEventArgs.Error != null)
			{
				this.SetNavigationFailure((callSessionEventArgs.Error != null) ? callSessionEventArgs.Error.Message : string.Empty);
				if (this.HandleError(vo, callSessionEventArgs))
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "Manager {0} is handling error.", new object[]
					{
						this
					});
					return;
				}
				CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "Manager {0} is unable to handles error. Bubbling Up.", new object[]
				{
					this
				});
			}
		}

		// Token: 0x060000BA RID: 186 RVA: 0x0000408E File Offset: 0x0000228E
		internal override void OnHold(BaseUMCallSession vo, UMCallSessionEventArgs callSessionEventArgs)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "Received Hold input.", new object[0]);
			this.currentActivity.OnHold(vo, callSessionEventArgs);
		}

		// Token: 0x060000BB RID: 187 RVA: 0x000040B3 File Offset: 0x000022B3
		internal override void OnResume(BaseUMCallSession vo, UMCallSessionEventArgs callSessionEventArgs)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "Received Hold input.", new object[0]);
			this.currentActivity.OnResume(vo, callSessionEventArgs);
		}

		// Token: 0x060000BC RID: 188 RVA: 0x000040D8 File Offset: 0x000022D8
		internal virtual void PreActionExecute(BaseUMCallSession vo)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "ActivityManager.PreActionExecute()", new object[0]);
			UMMailboxRecipient ummailboxRecipient = vo.CurrentCallContext.CalleeInfo as UMMailboxRecipient;
			UMMailboxRecipient callerInfo = vo.CurrentCallContext.CallerInfo;
			if (ummailboxRecipient != null)
			{
				this.calleeSessionGuard = ummailboxRecipient.CreateConnectionGuard();
			}
			if (callerInfo != null)
			{
				this.callerSessionGuard = callerInfo.CreateConnectionGuard();
			}
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00004138 File Offset: 0x00002338
		internal virtual void PostActionExecute(BaseUMCallSession vo)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "ActivityManager.PostActionExecute()", new object[0]);
			if (this.calleeSessionGuard != null)
			{
				this.calleeSessionGuard.Dispose();
				this.calleeSessionGuard = null;
			}
			if (this.callerSessionGuard != null)
			{
				this.callerSessionGuard.Dispose();
				this.callerSessionGuard = null;
			}
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00004190 File Offset: 0x00002390
		internal virtual bool HandleError(BaseUMCallSession vo, UMCallSessionEventArgs callSessionEventArgs)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "HandleError called.", new object[0]);
			TransitionBase transition = this.GetTransition(this.GetExceptionCategory(callSessionEventArgs));
			if (transition == null)
			{
				return false;
			}
			callSessionEventArgs.Error = null;
			transition.Execute(this, vo);
			return true;
		}

		// Token: 0x060000BF RID: 191 RVA: 0x000041D6 File Offset: 0x000023D6
		internal override void OnTransferComplete(BaseUMCallSession vo, UMCallSessionEventArgs callSessionEventArgs)
		{
			base.OnTransferComplete(vo, callSessionEventArgs);
			this.currentActivity.OnTransferComplete(vo, callSessionEventArgs);
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x000041ED File Offset: 0x000023ED
		internal override void OnMessageReceived(BaseUMCallSession vo, InfoMessage.MessageReceivedEventArgs e)
		{
			base.OnMessageReceived(vo, e);
			this.currentActivity.OnMessageReceived(vo, e);
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00004204 File Offset: 0x00002404
		internal override void OnMessageSent(BaseUMCallSession vo, EventArgs e)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "Message sent.", new object[0]);
			this.currentActivity.OnMessageSent(vo, e);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00004229 File Offset: 0x00002429
		internal override void OnHeavyBlockingOperation(BaseUMCallSession vo, HeavyBlockingOperationEventArgs hboea)
		{
			this.currentActivity.OnHeavyBlockingOperation(vo, hboea);
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00004238 File Offset: 0x00002438
		internal virtual void ChangeActivity(ActivityBase next, BaseUMCallSession vo, string refInfo)
		{
			if (this.currentActivity.UniqueId != next.UniqueId)
			{
				this.WriteVariable("more", false);
			}
			this.WriteVariable("currentActivity", next.ActivityId);
			this.LastActivity = this.currentActivity.ActivityId;
			this.currentActivity.Dispose();
			this.currentActivity = next;
			next.Start(vo, refInfo);
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x000042AA File Offset: 0x000024AA
		internal virtual void DropCall(BaseUMCallSession vo, DropCallReason reason)
		{
			if (base.Manager != null)
			{
				base.Manager.DropCall(vo, reason);
			}
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x000042C4 File Offset: 0x000024C4
		internal override TransitionBase GetTransition(string input)
		{
			TransitionBase transition = this.currentActivity.GetTransition(input);
			if (transition == null)
			{
				transition = base.Config.GetTransition(input, this);
			}
			return transition;
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x000042F0 File Offset: 0x000024F0
		internal virtual TransitionBase ExecuteAction(string action, BaseUMCallSession vo)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "ActivityManager::ExecuteAction({0})", new object[]
			{
				action
			});
			string input = null;
			if (string.Equals(action, "disconnect", StringComparison.OrdinalIgnoreCase))
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "ActivityManager dropping Call.", new object[0]);
				this.DropCall(vo, DropCallReason.GracefulHangup);
				input = "stopEvent";
			}
			else if (string.Equals(action, "more", StringComparison.OrdinalIgnoreCase))
			{
				object obj = this.ReadVariable("more");
				bool flag = obj == null || !(bool)obj;
				this.WriteVariable("more", flag);
			}
			else if (string.Equals(action, "stopASR", StringComparison.OrdinalIgnoreCase))
			{
				this.UseASR = false;
			}
			else if (string.Equals(action, "clearRecording", StringComparison.OrdinalIgnoreCase))
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "Clearing current recording.", new object[0]);
				this.WriteVariable("recording", null);
				this.RecordContext.Reset();
			}
			else if (string.Equals(action, "appendRecording", StringComparison.OrdinalIgnoreCase))
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "setting append recording flag.", new object[0]);
				this.RecordContext.Append = true;
			}
			else if (string.Equals(action, "pause", StringComparison.OrdinalIgnoreCase))
			{
				this.PlayBackContext.Commit();
				this.PlayBackContext.Offset = this.PlayBackContext.Offset - TimeSpan.FromSeconds(5.0);
				if (this.PlayBackContext.Offset < TimeSpan.Zero)
				{
					this.PlayBackContext.Offset = TimeSpan.Zero;
				}
			}
			else if (string.Equals(action, "rewind", StringComparison.OrdinalIgnoreCase))
			{
				ActivityManager.Rewind(vo);
			}
			else if (string.Equals(action, "fastForward", StringComparison.OrdinalIgnoreCase))
			{
				ActivityManager.FastForward(vo);
			}
			else if (string.Equals(action, "speedUp", StringComparison.OrdinalIgnoreCase))
			{
				input = this.SpeedUp(vo);
			}
			else if (string.Equals(action, "slowDown", StringComparison.OrdinalIgnoreCase))
			{
				input = this.SlowDown(vo);
			}
			else if (string.Equals(action, "resetPlayback", StringComparison.OrdinalIgnoreCase))
			{
				this.PlayBackContext.Reset();
			}
			else if (string.Equals(action, "saveRecoEvent", StringComparison.OrdinalIgnoreCase))
			{
				this.WriteVariable("savedRecoEvent", this.LastRecoEvent);
			}
			else
			{
				if (!string.Equals(action, "setSpeechError", StringComparison.OrdinalIgnoreCase))
				{
					return base.Manager.ExecuteAction(action, vo);
				}
				base.Manager.LastRecoEvent = "recoFailure";
			}
			return this.currentActivity.GetTransition(input);
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00004570 File Offset: 0x00002770
		internal void RunHeavyBlockingOperation(BaseUMCallSession vo, HeavyBlockingOperation hbo)
		{
			CultureInfo culture = vo.CurrentCallContext.Culture;
			vo.RunHeavyBlockingOperation(hbo, GlobCfg.DefaultPromptHelper.Build(this, culture, GlobCfg.DefaultPrompts.ComfortNoise));
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x000045A4 File Offset: 0x000027A4
		internal virtual object ReadVariable(string varName)
		{
			object obj = this.contextVariables[varName];
			if (obj == null && base.Manager != null)
			{
				obj = base.Manager.ReadVariable(varName);
			}
			return obj;
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x000045D7 File Offset: 0x000027D7
		internal void WriteVariable(string varName, object varValue)
		{
			this.contextVariables[varName] = varValue;
		}

		// Token: 0x060000CA RID: 202 RVA: 0x000045E8 File Offset: 0x000027E8
		internal void PlaySystemPrompt(ArrayList prompts, BaseUMCallSession vo)
		{
			if (vo.IsDuringPlayback())
			{
				vo.StopPlayback();
				return;
			}
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "Activity Manager playing system prompt.", new object[0]);
			this.playingSystemPrompt = true;
			if (vo.CurrentCallContext.IsTestCall)
			{
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < prompts.Count; i++)
				{
					stringBuilder.Append("\n");
					stringBuilder.Append(prompts[i].ToString());
				}
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_PromptsPlayed, null, new object[]
				{
					base.Config.ActivityId,
					stringBuilder.ToString()
				});
			}
			vo.PlayUninterruptiblePrompts(prompts);
		}

		// Token: 0x060000CB RID: 203 RVA: 0x0000469B File Offset: 0x0000289B
		internal void SetNavigationFailure(string message)
		{
			this.navigationState = UMNavigationState.Failure;
			this.navigationStateMessage = message;
		}

		// Token: 0x060000CC RID: 204 RVA: 0x000046AC File Offset: 0x000028AC
		internal void SetRecordedName(string variableName, string recipientName, IADRecipient r, bool disambiguate, DisambiguationFieldEnum disambiguationField, ref bool haveNameRecording)
		{
			object recordedName = this.GetRecordedName(recipientName, r, disambiguate, disambiguationField, ref haveNameRecording);
			this.WriteVariable(variableName, recordedName);
		}

		// Token: 0x060000CD RID: 205 RVA: 0x000046D0 File Offset: 0x000028D0
		internal object GetRecordedName(string recipientName, IADRecipient r, bool disambiguate, DisambiguationFieldEnum disambiguationField, ref bool haveNameRecording)
		{
			haveNameRecording = false;
			string text = null;
			string text2 = null;
			object result = null;
			UMCoreADUtil.GetDisambiguatedNameForRecipient(r, recipientName, disambiguate, disambiguationField, out text, out text2);
			using (GreetingBase greetingBase = new ADGreeting(r as ADRecipient, "RecordedName"))
			{
				ITempWavFile tempWavFile = greetingBase.Get();
				PIIMessage data = PIIMessage.Create(PIIType._UserDisplayName, text);
				if (tempWavFile == null)
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, data, "Did not find a recorded name for aduser=_UserDisplayName.  Using tts.", new object[0]);
					this.CallSession.IncrementCounter(AvailabilityCounters.NameTTSed);
					if (text2 != null)
					{
						DisambiguatedName disambiguatedName = new DisambiguatedName(text, text2, disambiguationField);
						result = disambiguatedName;
					}
					else
					{
						result = text;
					}
				}
				else
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, data, "Found a recorded name for aduser=_UserDisplayName.", new object[0]);
					this.CallSession.IncrementCounter(AvailabilityCounters.SpokenNameAccessed);
					result = tempWavFile;
					tempWavFile.ExtraInfo = text;
					haveNameRecording = true;
				}
			}
			return result;
		}

		// Token: 0x060000CE RID: 206 RVA: 0x000047B0 File Offset: 0x000029B0
		internal string Repeat(BaseUMCallSession vo)
		{
			this.WriteVariable("repeat", true);
			return null;
		}

		// Token: 0x060000CF RID: 207 RVA: 0x000047C4 File Offset: 0x000029C4
		protected override void InternalDispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "ActivityManager::Dispose.", new object[0]);
					this.contextVariables.Clear();
					this.Password = null;
					this.DtmfDigits = null;
					this.NumericInput = 0;
					this.PlayBackContext.Reset();
					this.RecordContext.Reset();
					if (this.navigationLogger != null)
					{
						this.navigationLogger.ExitTask(this.navigationState, this.navigationStateMessage);
					}
					if (this.currentActivity != null)
					{
						this.currentActivity.Dispose();
					}
				}
			}
			finally
			{
				base.InternalDispose(disposing);
			}
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x0000486C File Offset: 0x00002A6C
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<ActivityManager>(this);
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00004874 File Offset: 0x00002A74
		protected object GetRecordedName(IADRecipient r)
		{
			bool flag = false;
			string recipientName;
			if ((recipientName = r.DisplayName) == null)
			{
				recipientName = (r.Alias ?? string.Empty);
			}
			return this.GetRecordedName(recipientName, r, false, DisambiguationFieldEnum.None, ref flag);
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x000048A7 File Offset: 0x00002AA7
		protected void SetTextPartVariable(string variableName, string recordedName)
		{
			this.WriteVariable(variableName, recordedName);
			this.WriteVariable("textPart", true);
			this.WriteVariable("wavePart", false);
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x000048D3 File Offset: 0x00002AD3
		protected void SetWavePartVariable(string variableName, ITempWavFile recordedName)
		{
			this.WriteVariable(variableName, recordedName);
			this.WriteVariable("textPart", false);
			this.WriteVariable("wavePart", true);
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00004900 File Offset: 0x00002B00
		protected void SetRecordedName(string variableName, IADRecipient r)
		{
			bool flag = false;
			this.SetRecordedName(variableName, r, ref flag);
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00004919 File Offset: 0x00002B19
		protected void SetRecordedName(string variableName, IADRecipient r, ref bool haveNameRecording)
		{
			string recipientName;
			if ((recipientName = r.DisplayName) == null)
			{
				recipientName = (r.Alias ?? string.Empty);
			}
			this.SetRecordedName(variableName, recipientName, r, false, DisambiguationFieldEnum.None, ref haveNameRecording);
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00004940 File Offset: 0x00002B40
		protected string SelectLanguage()
		{
			CultureInfo language = CultureInfo.GetCultureInfo(this.RecoResult["Language"] as string);
			language = UmCultures.GetBestSupportedPromptCulture(language);
			return this.SetLanguage(language);
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00004978 File Offset: 0x00002B78
		protected string NextLanguage(BaseUMCallSession vo)
		{
			CultureInfo cultureInfo = this.MessagePlayerContext.Language;
			if (cultureInfo == null)
			{
				cultureInfo = vo.CurrentCallContext.Culture;
			}
			List<CultureInfo> sortedSupportedPromptCultures = UmCultures.GetSortedSupportedPromptCultures(vo.CurrentCallContext.Culture);
			CultureInfo language = sortedSupportedPromptCultures[0];
			CultureInfo objA = null;
			foreach (CultureInfo cultureInfo2 in sortedSupportedPromptCultures)
			{
				if (object.Equals(objA, cultureInfo))
				{
					language = cultureInfo2;
					break;
				}
				objA = cultureInfo2;
			}
			return this.SetLanguage(language);
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00004A10 File Offset: 0x00002C10
		protected string SetLanguage(CultureInfo language)
		{
			string result = null;
			if (language != null)
			{
				this.MessagePlayerContext.Language = language;
				this.WriteVariable("messageLanguage", this.MessagePlayerContext.Language);
				this.WriteVariable("languageDetected", null);
			}
			else
			{
				result = "unknownLanguage";
			}
			return result;
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00004A5C File Offset: 0x00002C5C
		protected void WriteReplyIntroType(IntroType introType)
		{
			this.WriteVariable("declineIntro", introType == IntroType.Decline);
			this.WriteVariable("replyIntro", introType == IntroType.Reply);
			this.WriteVariable("replyAllIntro", introType == IntroType.ReplyAll);
			this.WriteVariable("cancelIntro", introType == IntroType.Cancel || introType == IntroType.ClearCalendar);
			this.WriteVariable("forwardIntro", introType == IntroType.Forward);
			this.WriteVariable("clearCalendarIntro", introType == IntroType.ClearCalendar);
			this.messageIntroType = introType;
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00004AEF File Offset: 0x00002CEF
		private static void Rewind(BaseUMCallSession vo)
		{
			vo.Skip(-Constants.SeekTime);
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00004B01 File Offset: 0x00002D01
		private static void FastForward(BaseUMCallSession vo)
		{
			vo.Skip(Constants.SeekTime);
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00004B0E File Offset: 0x00002D0E
		private void FetchGalGrammar(BaseUMCallSession vo)
		{
			if (this.LargeGrammarsNeeded && this.UseASR && Util.IsSpeechCulture(vo.CurrentCallContext.Culture))
			{
				this.WriteVariable("namesGrammar", null);
			}
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00004B40 File Offset: 0x00002D40
		private string SpeedUp(BaseUMCallSession vo)
		{
			string result = null;
			float prosodyRate = this.ProsodyRate;
			this.ProsodyRate += 0.15f;
			if (this.ProsodyRate > 0.6f)
			{
				this.ProsodyRate = 0.6f;
				result = "maxProsodyRate";
			}
			this.PlayBackContext.Commit();
			this.PlayBackContext.Offset = this.PlayBackContext.Offset - TimeSpan.FromSeconds(2.0);
			if (this.PlayBackContext.Offset < TimeSpan.Zero)
			{
				this.PlayBackContext.Offset = TimeSpan.Zero;
			}
			return result;
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00004BE4 File Offset: 0x00002DE4
		private string SlowDown(BaseUMCallSession vo)
		{
			string result = null;
			float prosodyRate = this.ProsodyRate;
			this.ProsodyRate -= 0.15f;
			if (this.ProsodyRate < -0.6f)
			{
				this.ProsodyRate = -0.6f;
				result = "minProsodyRate";
			}
			this.PlayBackContext.Commit();
			this.PlayBackContext.Offset = this.PlayBackContext.Offset + TimeSpan.FromSeconds(2.0);
			if (this.PlayBackContext.Offset < TimeSpan.Zero)
			{
				this.PlayBackContext.Offset = TimeSpan.Zero;
			}
			return result;
		}

		// Token: 0x0400002C RID: 44
		private bool isSentImportant;

		// Token: 0x0400002D RID: 45
		private bool messageMarkedPrivate;

		// Token: 0x0400002E RID: 46
		private Shortcut lastShortcut;

		// Token: 0x0400002F RID: 47
		private bool isQuickMessage;

		// Token: 0x04000030 RID: 48
		private IUMRecognitionResult recoResult;

		// Token: 0x04000031 RID: 49
		private string lastBookmarkReached;

		// Token: 0x04000032 RID: 50
		private UMNavigationState navigationState;

		// Token: 0x04000033 RID: 51
		private string navigationStateMessage;

		// Token: 0x04000034 RID: 52
		private UMLoggingManager navigationLogger;

		// Token: 0x04000035 RID: 53
		private ActivityBase currentActivity;

		// Token: 0x04000036 RID: 54
		private Hashtable contextVariables;

		// Token: 0x04000037 RID: 55
		private int numberInput;

		// Token: 0x04000038 RID: 56
		private string dtmfDigits = string.Empty;

		// Token: 0x04000039 RID: 57
		private EncryptedBuffer password;

		// Token: 0x0400003A RID: 58
		private bool playingLastMenu;

		// Token: 0x0400003B RID: 59
		private bool playingSystemPrompt;

		// Token: 0x0400003C RID: 60
		private PlayBackContext playBackCtx;

		// Token: 0x0400003D RID: 61
		private RecordContext recCtx;

		// Token: 0x0400003E RID: 62
		private MessagePlayerContext msgPlayerCtx;

		// Token: 0x0400003F RID: 63
		private BaseUMCallSession callSession;

		// Token: 0x04000040 RID: 64
		private IntroType messageIntroType;

		// Token: 0x04000041 RID: 65
		private UMMailboxRecipient.MailboxConnectionGuard calleeSessionGuard;

		// Token: 0x04000042 RID: 66
		private UMMailboxRecipient.MailboxConnectionGuard callerSessionGuard;
	}
}
