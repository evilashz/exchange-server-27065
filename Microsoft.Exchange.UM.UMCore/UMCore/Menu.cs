using System;
using System.Collections;
using System.Globalization;
using System.Text;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000174 RID: 372
	internal class Menu : CommonActivity
	{
		// Token: 0x06000AF9 RID: 2809 RVA: 0x0002EC33 File Offset: 0x0002CE33
		internal Menu(ActivityManager manager, ActivityConfig config) : base(manager, config)
		{
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06000AFA RID: 2810 RVA: 0x0002EC4F File Offset: 0x0002CE4F
		// (set) Token: 0x06000AFB RID: 2811 RVA: 0x0002EC57 File Offset: 0x0002CE57
		protected bool IsTestCall
		{
			get
			{
				return this.isTestCall;
			}
			set
			{
				this.isTestCall = value;
			}
		}

		// Token: 0x06000AFC RID: 2812 RVA: 0x0002EC60 File Offset: 0x0002CE60
		internal override void StartActivity(BaseUMCallSession vo, string refInfo)
		{
			ActivityConfig config = base.Config;
			UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_FsmActivityStart, null, new object[]
			{
				this.ToString(),
				vo.CallId
			});
			this.IsTestCall = vo.CurrentCallContext.IsTestCall;
			if (vo.CurrentCallContext.CallerInfo != null && vo.CurrentCallContext.CallerInfo.DialPlan != null)
			{
				this.numAllowedFailures = vo.CurrentCallContext.CallerInfo.DialPlan.InputFailuresBeforeDisconnect;
			}
			this.MenuStart(vo);
		}

		// Token: 0x06000AFD RID: 2813 RVA: 0x0002ECF4 File Offset: 0x0002CEF4
		internal override void OnInput(BaseUMCallSession vo, UMCallSessionEventArgs voiceObjectEventArgs)
		{
			base.Manager.PlayBackContext.Update(base.UniqueId, voiceObjectEventArgs.LastPrompt, voiceObjectEventArgs.PlayTime);
			TransitionBase transitionBase = this.ChooseTransition(vo, voiceObjectEventArgs);
			if (transitionBase == null && this.IsInputFaxTone(voiceObjectEventArgs))
			{
				this.DefaultFaxTransition(vo, voiceObjectEventArgs);
				return;
			}
			this.TakeTransition(vo, voiceObjectEventArgs, transitionBase);
		}

		// Token: 0x06000AFE RID: 2814 RVA: 0x0002ED4C File Offset: 0x0002CF4C
		internal override void OnComplete(BaseUMCallSession vo, UMCallSessionEventArgs voiceObjectEventArgs)
		{
			base.Manager.PlayBackContext.Update(base.UniqueId, 0, TimeSpan.Zero);
			base.OnComplete(vo, voiceObjectEventArgs);
			TransitionBase transition;
			if (!voiceObjectEventArgs.SendDtmfCompleted)
			{
				transition = this.GetTransition(this.transitionEventOnComplete);
			}
			else
			{
				transition = this.GetTransition("dtmfSent");
			}
			if (transition != null)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "Menu Activity making NOKEY transition: {0}.", new object[]
				{
					transition
				});
				transition.Execute(base.Manager, vo);
				return;
			}
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "Menu Activity has no NOKEY transition or call is disconnected", new object[0]);
			base.Manager.DropCall(vo, DropCallReason.UserError);
		}

		// Token: 0x06000AFF RID: 2815 RVA: 0x0002EDF4 File Offset: 0x0002CFF4
		internal override void OnTimeout(BaseUMCallSession vo, UMCallSessionEventArgs voiceObjectEventArgs)
		{
			MenuConfig menuConfig = base.Config as MenuConfig;
			base.Manager.PlayBackContext.Update(base.UniqueId, 0, TimeSpan.Zero);
			base.Manager.NumericInput = 0;
			base.Manager.DtmfDigits = string.Empty;
			if (!string.Equals(menuConfig.DtmfInputValue, "password", StringComparison.OrdinalIgnoreCase))
			{
				string text = Encoding.ASCII.GetString(voiceObjectEventArgs.DtmfDigits);
				text = text.TrimEnd(menuConfig.DtmfStopTones.ToCharArray());
				base.Manager.WriteVariable("lastInput", text);
			}
			base.OnTimeout(vo, voiceObjectEventArgs);
			TransitionBase transition = this.GetTransition("timeout");
			if (transition != null)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "OnTimeout triggered an explicit transition t={0}.", new object[]
				{
					transition
				});
				transition.Execute(base.Manager, vo);
				return;
			}
			base.NumFailures++;
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "OnTimeout event being handled by Menu.  NumFailures={0}.", new object[]
			{
				base.NumFailures
			});
			if (base.NumFailures < this.numAllowedFailures)
			{
				CultureInfo culture = vo.CurrentCallContext.Culture;
				base.Manager.PlaySystemPrompt(GlobCfg.DefaultPromptHelper.Build(base.Manager, culture, new PromptConfigBase[]
				{
					GlobCfg.DefaultPrompts.AreYouThere
				}), vo);
				return;
			}
			base.Manager.DropCall(vo, DropCallReason.UserError);
		}

		// Token: 0x06000B00 RID: 2816 RVA: 0x0002EF60 File Offset: 0x0002D160
		internal override void OnMessageReceived(BaseUMCallSession vo, InfoMessage.MessageReceivedEventArgs e)
		{
			CallContext currentCallContext = vo.CurrentCallContext;
			base.OnMessageReceived(vo, e);
			if (currentCallContext.DialPlan == null || currentCallContext.DialPlan.URIType != UMUriType.TelExtn)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "MenuActivity: Received INFO on a non-TelExtn dialplan ({0}).", new object[]
				{
					(currentCallContext.DialPlan == null) ? "null" : currentCallContext.DialPlan.URIType.ToString()
				});
				return;
			}
			object obj = base.Manager.ReadVariable("waitForSourcePartyInfo");
			bool flag = obj != null && (bool)obj;
			if (!flag)
			{
				base.Manager.WriteVariable("waitForSourcePartyInfo", false);
				return;
			}
			base.Manager.WriteVariable("waitForSourcePartyInfo", false);
			if (!vo.IsDuringPlayback())
			{
				CallIdTracer.TraceError(ExTraceGlobals.StateMachineTracer, this, "MenuActivity: Received INFO outside silence prompt", new object[0]);
				return;
			}
			string text = null;
			string calledParty = null;
			if (e.Message.Headers.TryGetValue("Diversion", out text) && Util.TryParseDiversionHeader(text, out calledParty))
			{
				PlatformDiversionInfo diversionInfo = new PlatformDiversionInfo(text, calledParty, string.Empty, RedirectReason.None, DiversionSource.SipInfo);
				this.transitionEventOnComplete = base.Manager.GlobalManager.HandleSourcePartyInfoDiversion(diversionInfo);
			}
			else
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "MenuActivity: Received INFO with empty or invalid diversion header {0}.", new object[]
				{
					text
				});
			}
			vo.StopPlayback();
		}

		// Token: 0x06000B01 RID: 2817 RVA: 0x0002F0C0 File Offset: 0x0002D2C0
		internal override void OnUserHangup(BaseUMCallSession vo, UMCallSessionEventArgs voiceObjectEventArgs)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "MenuActivity OnUserHangup.", new object[0]);
			TransitionBase transition = this.GetTransition("userHangup");
			if (transition != null)
			{
				transition.Execute(base.Manager, vo);
				return;
			}
			base.OnUserHangup(vo, voiceObjectEventArgs);
		}

		// Token: 0x06000B02 RID: 2818 RVA: 0x0002F108 File Offset: 0x0002D308
		protected internal virtual void InitializePrompts(ArrayList prompts, bool isTestCall)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "Initializing {0} prompts.", new object[]
			{
				prompts.Count
			});
			if (prompts.Count > 0 && isTestCall)
			{
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < prompts.Count; i++)
				{
					stringBuilder.Append("\n");
					stringBuilder.Append(prompts[i].ToString());
				}
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_PromptsPlayed, null, new object[]
				{
					base.UniqueId.ToString(),
					stringBuilder.ToString()
				});
			}
		}

		// Token: 0x06000B03 RID: 2819 RVA: 0x0002F1B0 File Offset: 0x0002D3B0
		protected static IPromptCounter GetPromptCounter(BaseUMCallSession vo)
		{
			IPromptCounter result = null;
			UMSubscriber callerInfo = vo.CurrentCallContext.CallerInfo;
			if (callerInfo != null)
			{
				result = callerInfo.ConfigFolder;
			}
			return result;
		}

		// Token: 0x06000B04 RID: 2820 RVA: 0x0002F1D8 File Offset: 0x0002D3D8
		protected void SetProsodyRates(ArrayList prompts)
		{
			foreach (object obj in prompts)
			{
				Prompt prompt = (Prompt)obj;
				prompt.SetProsodyRate(base.Manager.ProsodyRate);
			}
		}

		// Token: 0x06000B05 RID: 2821 RVA: 0x0002F238 File Offset: 0x0002D438
		protected void SetLanguages(ArrayList prompts)
		{
			foreach (object obj in prompts)
			{
				Prompt prompt = (Prompt)obj;
				prompt.TTSLanguage = base.Manager.MessagePlayerContext.Language;
			}
		}

		// Token: 0x06000B06 RID: 2822 RVA: 0x0002F29C File Offset: 0x0002D49C
		protected virtual void PlayPrompts(ArrayList prompts, BaseUMCallSession vo)
		{
			MenuConfig menuConfig = base.Config as MenuConfig;
			if (menuConfig.Uninterruptible)
			{
				this.PlayUninterruptiblePrompts(prompts, vo);
				return;
			}
			this.PlayInterruptiblePrompts(prompts, vo);
		}

		// Token: 0x06000B07 RID: 2823 RVA: 0x0002F2D0 File Offset: 0x0002D4D0
		protected void PlayUninterruptiblePrompts(ArrayList prompts, BaseUMCallSession vo)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "PlayUninterruptiblePrompts for menu: {0}.", new object[]
			{
				base.UniqueId
			});
			ActivityConfig config = base.Config;
			this.InitializePrompts(prompts, this.IsTestCall);
			vo.PlayUninterruptiblePrompts(prompts);
		}

		// Token: 0x06000B08 RID: 2824 RVA: 0x0002F31C File Offset: 0x0002D51C
		protected void PlayInterruptiblePrompts(ArrayList prompts, BaseUMCallSession vo)
		{
			MenuConfig menuConfig = base.Config as MenuConfig;
			int num;
			int num2;
			if (string.Equals(menuConfig.DtmfInputValue, "extension", StringComparison.OrdinalIgnoreCase))
			{
				num = vo.CurrentCallContext.DialPlan.NumberOfDigitsInExtension;
				num2 = num;
			}
			else
			{
				num = (int)menuConfig.MinDtmfSize;
				num2 = (int)menuConfig.MaxDtmfSize;
			}
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "Menu Activity using minSize={0} maxSize={1} with inputValue={2}.", new object[]
			{
				num,
				num2,
				menuConfig.DtmfInputValue
			});
			if (string.Compare(base.Manager.PlayBackContext.Id, base.UniqueId, true, CultureInfo.InvariantCulture) != 0)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "Playback manager not applying current context because context id={0} not equal to this id={1}", new object[]
				{
					base.Manager.PlayBackContext.Id,
					base.UniqueId
				});
				this.InitializePrompts(prompts, this.IsTestCall);
				base.Manager.PlayBackContext.Update(prompts);
				vo.PlayPrompts(prompts, num, num2, menuConfig.InputTimeout, menuConfig.DtmfStopTones, menuConfig.InterDigitTimeout, menuConfig.ComputeStopPatterns(base.Manager), menuConfig.StopPromptOnBargeIn, string.Equals(menuConfig.DtmfInputValue, "password", StringComparison.OrdinalIgnoreCase) ? null : base.UniqueId, menuConfig.InitialSilenceTimeout);
				return;
			}
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "PlayBackContext={0} being applied to activity={1}.", new object[]
			{
				base.Manager.PlayBackContext,
				this
			});
			prompts = base.Manager.PlayBackContext.Prompts;
			int lastPrompt = base.Manager.PlayBackContext.LastPrompt;
			TimeSpan offset = base.Manager.PlayBackContext.Offset;
			this.SetProsodyRates(prompts);
			this.SetLanguages(prompts);
			base.Manager.PlayBackContext.Reset();
			base.Manager.PlayBackContext.Update(prompts);
			if (base.Manager.PlayBackContext.LastPrompt < prompts.Count)
			{
				vo.PlayPrompts(prompts, (int)menuConfig.MinDtmfSize, (int)menuConfig.MaxDtmfSize, 10, menuConfig.DtmfStopTones, menuConfig.InterDigitTimeout, menuConfig.ComputeStopPatterns(base.Manager), lastPrompt, offset, menuConfig.StopPromptOnBargeIn, string.Equals(menuConfig.DtmfInputValue, "password", StringComparison.OrdinalIgnoreCase) ? null : base.UniqueId, menuConfig.InitialSilenceTimeout);
				return;
			}
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "PlayBackActivity firing OnComplete event to itself.", new object[0]);
			this.OnComplete(vo, new UMCallSessionEventArgs
			{
				LastPrompt = lastPrompt
			});
		}

		// Token: 0x06000B09 RID: 2825 RVA: 0x0002F5A0 File Offset: 0x0002D7A0
		protected void TakeTransition(BaseUMCallSession vo, UMCallSessionEventArgs voiceObjectEventArgs, TransitionBase transition)
		{
			if (transition != null)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "Menu Activity making transition: {0}.", new object[]
				{
					transition
				});
				transition.Execute(base.Manager, vo);
				return;
			}
			if (vo.IsDuringPlayback())
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "Menu Activity received and invalid key during playback... stopping playback.", new object[0]);
				vo.StopPlayback();
				return;
			}
			if (!voiceObjectEventArgs.WasPlaybackStopped)
			{
				base.NumFailures++;
			}
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "Menu Activity has no transition for input {0}.  NumFailures now at={1}.", new object[]
			{
				Encoding.ASCII.GetString(voiceObjectEventArgs.DtmfDigits),
				base.NumFailures
			});
			vo.ClearDigits(1000);
			if (base.NumFailures < this.numAllowedFailures)
			{
				CultureInfo culture = vo.CurrentCallContext.Culture;
				base.Manager.PlaySystemPrompt(GlobCfg.DefaultPromptHelper.Build(base.Manager, culture, new PromptConfigBase[]
				{
					GlobCfg.DefaultPrompts.InvalidKey
				}), vo);
				return;
			}
			base.Manager.SetNavigationFailure(base.UniqueId);
			base.Manager.DropCall(vo, DropCallReason.UserError);
		}

		// Token: 0x06000B0A RID: 2826 RVA: 0x0002F6C0 File Offset: 0x0002D8C0
		protected TransitionBase ChooseTransition(BaseUMCallSession vo, UMCallSessionEventArgs voiceObjectEventArgs)
		{
			MenuConfig menuConfig = base.Config as MenuConfig;
			TransitionBase transitionBase = null;
			if (vo.CurrentCallContext.CallType != 7 && this.CheckFindMeCall(voiceObjectEventArgs))
			{
				base.Manager.DropCall(vo, DropCallReason.GracefulHangup);
				return new StopTransition();
			}
			if (this.CheckDiagnosticCall(voiceObjectEventArgs) && vo.CurrentCallContext.CallType != 7)
			{
				transitionBase = this.GetTransition("umdiagnosticCall");
				if (transitionBase != null)
				{
					return transitionBase;
				}
			}
			if (string.Equals(menuConfig.DtmfInputValue, "password", StringComparison.OrdinalIgnoreCase))
			{
				if (1 == voiceObjectEventArgs.DtmfDigits.Length && (Constants.StarByte == voiceObjectEventArgs.DtmfDigits[0] || Constants.PoundByte == voiceObjectEventArgs.DtmfDigits[0]))
				{
					base.OnInput(vo, voiceObjectEventArgs);
					base.Manager.DtmfDigits = Encoding.ASCII.GetString(voiceObjectEventArgs.DtmfDigits);
					transitionBase = this.GetTransition(Encoding.ASCII.GetString(voiceObjectEventArgs.DtmfDigits));
				}
				else if (Encoding.ASCII.GetString(voiceObjectEventArgs.DtmfDigits).Equals("faxtone"))
				{
					base.OnInput(vo, voiceObjectEventArgs);
					transitionBase = this.GetTransition(Encoding.ASCII.GetString(voiceObjectEventArgs.DtmfDigits));
				}
				else
				{
					base.Manager.Password = new EncryptedBuffer(voiceObjectEventArgs.DtmfDigits, menuConfig.DtmfStopTones);
					Array.Clear(voiceObjectEventArgs.DtmfDigits, 0, voiceObjectEventArgs.DtmfDigits.Length);
					transitionBase = this.GetTransition("anyKey");
				}
			}
			else if (string.Equals(menuConfig.DtmfInputValue, "option", StringComparison.OrdinalIgnoreCase) || string.Equals(menuConfig.DtmfInputValue, "extension", StringComparison.OrdinalIgnoreCase) || string.Equals(menuConfig.DtmfInputValue, "name", StringComparison.OrdinalIgnoreCase))
			{
				base.OnInput(vo, voiceObjectEventArgs);
				string @string = Encoding.ASCII.GetString(voiceObjectEventArgs.DtmfDigits);
				base.Manager.DtmfDigits = @string.TrimEnd(menuConfig.DtmfStopTones.ToCharArray());
				transitionBase = this.GetTransition(@string);
				if (transitionBase == null && 0 < @string.Length && !this.IsInputFaxTone(voiceObjectEventArgs))
				{
					transitionBase = this.GetTransition("anyKey");
				}
				if (menuConfig.KeepDtmfOnNoMatch && transitionBase == null && @string.Length > 0)
				{
					transitionBase = this.GetTransition("noKey");
					if (transitionBase != null)
					{
						vo.RebufferDigits(voiceObjectEventArgs.DtmfDigits);
					}
				}
			}
			else if (string.Equals(menuConfig.DtmfInputValue, "numeric", StringComparison.OrdinalIgnoreCase))
			{
				base.OnInput(vo, voiceObjectEventArgs);
				string string2 = Encoding.ASCII.GetString(voiceObjectEventArgs.DtmfDigits);
				base.Manager.DtmfDigits = string2.TrimEnd(menuConfig.DtmfStopTones.ToCharArray());
				transitionBase = this.GetTransition(string2);
				if (transitionBase == null)
				{
					if (base.Manager.DtmfDigits.Length == 0)
					{
						base.Manager.DtmfDigits = "0";
					}
					int num;
					if (!int.TryParse(base.Manager.DtmfDigits, NumberStyles.Integer, CultureInfo.InvariantCulture, out num) || (long)num > (long)((ulong)menuConfig.MaxNumeric) || (long)num < (long)((ulong)menuConfig.MinNumeric))
					{
						CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "NUMERIC INPUT invalid. tmp={0} Choose transition returning null.", new object[]
						{
							num
						});
						transitionBase = null;
					}
					else
					{
						CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "NUMERIC INPUT choosing anykey transition.", new object[0]);
						base.Manager.NumericInput = num;
						transitionBase = this.GetTransition("anyKey");
					}
				}
			}
			base.Manager.WriteVariable("lastInput", base.Manager.DtmfDigits);
			return transitionBase;
		}

		// Token: 0x06000B0B RID: 2827 RVA: 0x0002FA28 File Offset: 0x0002DC28
		private static bool CheckDiagnosticSequence(string dtmf)
		{
			string text = string.Empty;
			int num = dtmf.IndexOf("faxtone", StringComparison.InvariantCultureIgnoreCase);
			if (num >= 0)
			{
				text = dtmf.Substring(0, num);
			}
			else
			{
				text = dtmf;
			}
			return text.IndexOf("A", StringComparison.InvariantCultureIgnoreCase) >= 0 || text.IndexOf("B", StringComparison.InvariantCultureIgnoreCase) >= 0 || text.IndexOf("C", StringComparison.InvariantCultureIgnoreCase) >= 0;
		}

		// Token: 0x06000B0C RID: 2828 RVA: 0x0002FA8C File Offset: 0x0002DC8C
		private void MenuStart(BaseUMCallSession vo)
		{
			MenuConfig menuConfig = base.Config as MenuConfig;
			base.Manager.DtmfDigits = string.Empty;
			base.Manager.NumericInput = 0;
			IPromptCounter promptCounter = Menu.GetPromptCounter(vo);
			ArrayList prompts = menuConfig.GetPrompts(base.Manager, "Main", vo.CurrentCallContext.Culture, promptCounter);
			if (promptCounter != null)
			{
				promptCounter.SavePromptCount();
			}
			if (prompts.Count > 0 || menuConfig.MinDtmfSize > 0)
			{
				this.PlayPrompts(prompts, vo);
				return;
			}
			this.OnComplete(vo, new UMCallSessionEventArgs());
		}

		// Token: 0x06000B0D RID: 2829 RVA: 0x0002FB18 File Offset: 0x0002DD18
		private bool CheckFindMeCall(UMCallSessionEventArgs voiceEventArgs)
		{
			string @string = Encoding.ASCII.GetString(voiceEventArgs.DtmfDigits);
			if (@string.StartsWith("D", StringComparison.InvariantCulture))
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "DTMFs received is for find-me call : {0}", new object[]
				{
					@string
				});
				return true;
			}
			return false;
		}

		// Token: 0x06000B0E RID: 2830 RVA: 0x0002FB64 File Offset: 0x0002DD64
		private bool CheckDiagnosticCall(UMCallSessionEventArgs voiceEventArgs)
		{
			string text = base.Manager.GlobalManager.ReadVariable("diagnosticDtmfDigits") as string;
			string @string = Encoding.ASCII.GetString(voiceEventArgs.DtmfDigits);
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "DTMFs saved previously: {0}, DTMFs received now: {1}", new object[]
			{
				text,
				@string
			});
			if (string.IsNullOrEmpty(text))
			{
				if (Menu.CheckDiagnosticSequence(@string))
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "Saving diagnostic digits {0}", new object[]
					{
						@string
					});
					base.Manager.GlobalManager.WriteVariable("diagnosticDtmfDigits", @string);
					return true;
				}
			}
			else
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "Accumulating {0} to {1}", new object[]
				{
					@string,
					text
				});
				base.Manager.GlobalManager.WriteVariable("diagnosticDtmfDigits", text + @string);
			}
			return false;
		}

		// Token: 0x06000B0F RID: 2831 RVA: 0x0002FC44 File Offset: 0x0002DE44
		private bool IsInputFaxTone(UMCallSessionEventArgs voiceObjectEventArgs)
		{
			string @string = Encoding.ASCII.GetString(voiceObjectEventArgs.DtmfDigits);
			return string.Equals(@string, "faxtone", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000B10 RID: 2832 RVA: 0x0002FC74 File Offset: 0x0002DE74
		private void DefaultFaxTransition(BaseUMCallSession vo, UMCallSessionEventArgs voiceObjectEventArgs)
		{
			if (this.IsFaxRequestAcceptable(vo))
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "Menu Activity making faxTone transition to GlobalManager", new object[0]);
				TransitionBase transition = base.Manager.GlobalManager.GetTransition("faxtone");
				transition.Execute(base.Manager.GlobalManager, vo);
				return;
			}
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "GlobalManager drop this call because fax request is not acceptable.", new object[0]);
			UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_FAXRequestIsNotAcceptable, null, new object[]
			{
				base.UniqueId
			});
			base.Manager.GlobalManager.DropCall(vo, DropCallReason.UserError);
		}

		// Token: 0x06000B11 RID: 2833 RVA: 0x0002FD13 File Offset: 0x0002DF13
		private bool IsFaxRequestAcceptable(BaseUMCallSession vo)
		{
			if (vo.CurrentCallContext != null && vo.CurrentCallContext.CallType == 4)
			{
				ExAssert.RetailAssert(vo.CurrentCallContext.CalleeInfo != null, "CalleeInfo is null when UM detects faxtone for call answering");
				return true;
			}
			return false;
		}

		// Token: 0x04000991 RID: 2449
		private int numAllowedFailures = 3;

		// Token: 0x04000992 RID: 2450
		private string transitionEventOnComplete = "noKey";

		// Token: 0x04000993 RID: 2451
		private bool isTestCall;
	}
}
