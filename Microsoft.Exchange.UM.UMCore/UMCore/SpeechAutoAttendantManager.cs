using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020001DC RID: 476
	internal class SpeechAutoAttendantManager : AsrContactsManager, IAutoAttendantUI
	{
		// Token: 0x06000DED RID: 3565 RVA: 0x0003E0A2 File Offset: 0x0003C2A2
		internal SpeechAutoAttendantManager(ActivityManager manager, SpeechAutoAttendantManager.ConfigClass config) : base(manager, config)
		{
		}

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x06000DEE RID: 3566 RVA: 0x0003E0B3 File Offset: 0x0003C2B3
		internal bool StarOutToDialPlanEnabled
		{
			get
			{
				return this.commonAABehavior.StarOutToDialPlanEnabled;
			}
		}

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06000DEF RID: 3567 RVA: 0x0003E0C0 File Offset: 0x0003C2C0
		internal bool ForwardCallsToDefaultMailbox
		{
			get
			{
				return this.commonAABehavior.ForwardCallsToDefaultMailbox;
			}
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06000DF0 RID: 3568 RVA: 0x0003E0CD File Offset: 0x0003C2CD
		internal string BusinessName
		{
			get
			{
				return this.commonAABehavior.BusinessName;
			}
		}

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06000DF1 RID: 3569 RVA: 0x0003E0DA File Offset: 0x0003C2DA
		internal string BusinessLocation
		{
			get
			{
				return this.commonAABehavior.Config.BusinessLocation;
			}
		}

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06000DF2 RID: 3570 RVA: 0x0003E0EC File Offset: 0x0003C2EC
		internal bool BusinessLocationIsSet
		{
			get
			{
				return !string.IsNullOrEmpty(this.BusinessLocation);
			}
		}

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06000DF3 RID: 3571 RVA: 0x0003E0FC File Offset: 0x0003C2FC
		internal UMAutoAttendant ThisAutoAttendant
		{
			get
			{
				return this.commonAABehavior.Config;
			}
		}

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06000DF4 RID: 3572 RVA: 0x0003E10C File Offset: 0x0003C30C
		internal AutoAttendantContext AAContext
		{
			get
			{
				bool isBusinessHours = false;
				HolidaySchedule holidaySchedule = null;
				this.ThisAutoAttendant.GetCurrentSettings(out holidaySchedule, ref isBusinessHours);
				return new AutoAttendantContext(this.ThisAutoAttendant, isBusinessHours);
			}
		}

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06000DF5 RID: 3573 RVA: 0x0003E139 File Offset: 0x0003C339
		internal AutoAttendantLocationContext AALocationContext
		{
			get
			{
				return new AutoAttendantLocationContext(this.ThisAutoAttendant, this.SelectedMenu);
			}
		}

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06000DF6 RID: 3574 RVA: 0x0003E14C File Offset: 0x0003C34C
		internal string SelectedMenu
		{
			get
			{
				if (this.commonAABehavior.SelectedMenu == null)
				{
					return string.Empty;
				}
				return this.commonAABehavior.SelectedMenu.Description;
			}
		}

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06000DF7 RID: 3575 RVA: 0x0003E171 File Offset: 0x0003C371
		internal bool RepeatMainMenu
		{
			get
			{
				return this.repeatMainMenu;
			}
		}

		// Token: 0x06000DF8 RID: 3576 RVA: 0x0003E179 File Offset: 0x0003C379
		public object ReadProperty(string name)
		{
			return this.ReadVariable(name);
		}

		// Token: 0x06000DF9 RID: 3577 RVA: 0x0003E182 File Offset: 0x0003C382
		public object ReadGlobalProperty(string name)
		{
			return this.GlobalManager.ReadVariable(name);
		}

		// Token: 0x06000DFA RID: 3578 RVA: 0x0003E190 File Offset: 0x0003C390
		public void WriteProperty(string name, object value)
		{
			base.WriteVariable(name, value);
		}

		// Token: 0x06000DFB RID: 3579 RVA: 0x0003E19A File Offset: 0x0003C39A
		public void WriteGlobalProperty(string name, object value)
		{
			this.GlobalManager.WriteVariable(name, value);
		}

		// Token: 0x06000DFC RID: 3580 RVA: 0x0003E1A9 File Offset: 0x0003C3A9
		public void SetTextPrompt(string name, string promptText)
		{
			base.SetTextPartVariable(name, promptText);
		}

		// Token: 0x06000DFD RID: 3581 RVA: 0x0003E1B3 File Offset: 0x0003C3B3
		public void SetWavePrompt(string name, ITempWavFile promptFile)
		{
			base.SetWavePartVariable(name, promptFile);
		}

		// Token: 0x06000DFE RID: 3582 RVA: 0x0003E1BD File Offset: 0x0003C3BD
		internal override void Start(BaseUMCallSession vo, string refInfo)
		{
			base.WriteVariable("tuiPromptEditingEnabled", this.GlobalManager.ReadVariable("tuiPromptEditingEnabled"));
			this.commonAABehavior = AutoAttendantCore.Create(this, vo);
			base.SetInitialSearchTargetGal();
			base.Start(vo, refInfo);
		}

		// Token: 0x06000DFF RID: 3583 RVA: 0x0003E1F5 File Offset: 0x0003C3F5
		internal override void CheckAuthorization(UMSubscriber u)
		{
		}

		// Token: 0x06000E00 RID: 3584 RVA: 0x0003E1F8 File Offset: 0x0003C3F8
		internal override TransitionBase ExecuteAction(string action, BaseUMCallSession vo)
		{
			string text = null;
			if (string.Equals(action, "checkRestrictedUser", StringComparison.OrdinalIgnoreCase))
			{
				text = "unrestrictedUser";
				if (base.SelectedSearchItem != null && (base.SelectedSearchItem.Recipient.AllowUMCallsFromNonUsers & AllowUMCallsFromNonUsersFlags.SearchEnabled) != AllowUMCallsFromNonUsersFlags.SearchEnabled)
				{
					text = "restrictedUser";
					PIIMessage data = PIIMessage.Create(PIIType._UserDisplayName, base.SelectedSearchItem.Recipient.DisplayName);
					CallIdTracer.TraceDebug(ExTraceGlobals.AutoAttendantTracer, this, data, "Recipient _UserDisplayName is a protected user. returning autoEvent: {0}.", new object[]
					{
						text
					});
				}
			}
			else if (string.Equals(action, "setPromptProvContext", StringComparison.OrdinalIgnoreCase))
			{
				this.GlobalManager.WriteVariable("promptProvContext", "AutoAttendant");
			}
			else if (!this.commonAABehavior.ExecuteAction(action, vo, ref text))
			{
				return base.ExecuteAction(action, vo);
			}
			return base.CurrentActivity.GetTransition(text);
		}

		// Token: 0x06000E01 RID: 3585 RVA: 0x0003E2C2 File Offset: 0x0003C4C2
		internal string PrepareForCallAnswering(BaseUMCallSession session)
		{
			return this.commonAABehavior.Action_PrepareForCallAnswering();
		}

		// Token: 0x06000E02 RID: 3586 RVA: 0x0003E2CF File Offset: 0x0003C4CF
		internal string DisableMainMenuRepetition(BaseUMCallSession session)
		{
			this.repeatMainMenu = false;
			return null;
		}

		// Token: 0x06000E03 RID: 3587 RVA: 0x0003E2D9 File Offset: 0x0003C4D9
		internal string EnableMainMenuRepetition(BaseUMCallSession session)
		{
			this.repeatMainMenu = true;
			return null;
		}

		// Token: 0x06000E04 RID: 3588 RVA: 0x0003E2E4 File Offset: 0x0003C4E4
		internal override void OnTransferComplete(BaseUMCallSession vo, UMCallSessionEventArgs voiceEventArgs)
		{
			TransferExtension transferExtension = (TransferExtension)this.ReadVariable("transferExtension");
			CallIdTracer.TraceDebug(ExTraceGlobals.AutoAttendantTracer, this, "OnTransferComplete Type = {0}.", new object[]
			{
				transferExtension
			});
			this.commonAABehavior.OnTransferComplete(transferExtension);
			base.OnTransferComplete(vo, voiceEventArgs);
		}

		// Token: 0x06000E05 RID: 3589 RVA: 0x0003E337 File Offset: 0x0003C537
		internal override void OnInput(BaseUMCallSession vo, UMCallSessionEventArgs voiceEventArgs)
		{
			this.commonAABehavior.OnInput();
			base.OnInput(vo, voiceEventArgs);
		}

		// Token: 0x06000E06 RID: 3590 RVA: 0x0003E34C File Offset: 0x0003C54C
		internal override void OnUserHangup(BaseUMCallSession vo, UMCallSessionEventArgs voiceEventArgs)
		{
			this.commonAABehavior.OnHangup();
			base.OnUserHangup(vo, voiceEventArgs);
		}

		// Token: 0x06000E07 RID: 3591 RVA: 0x0003E361 File Offset: 0x0003C561
		internal override void OnTimeout(BaseUMCallSession vo, UMCallSessionEventArgs voiceEventArgs)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.AutoAttendantTracer, this, "Timeout.", new object[0]);
			this.commonAABehavior.OnTimeout();
			base.CurrentActivity.OnTimeout(vo, voiceEventArgs);
		}

		// Token: 0x06000E08 RID: 3592 RVA: 0x0003E391 File Offset: 0x0003C591
		internal override void OnNameSpoken()
		{
			this.commonAABehavior.OnNameSpoken();
		}

		// Token: 0x06000E09 RID: 3593 RVA: 0x0003E39E File Offset: 0x0003C59E
		internal override void PrepareForNBestPhase2()
		{
			this.commonAABehavior.Initialize();
		}

		// Token: 0x06000E0A RID: 3594 RVA: 0x0003E3AB File Offset: 0x0003C5AB
		internal override void OnSpeech(object sender, UMSpeechEventArgs args)
		{
			this.commonAABehavior.OnSpeech();
			base.OnSpeech(sender, args);
		}

		// Token: 0x06000E0B RID: 3595 RVA: 0x0003E3C0 File Offset: 0x0003C5C0
		protected override void InternalDispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					this.commonAABehavior.Dispose();
				}
			}
			finally
			{
				base.InternalDispose(disposing);
			}
		}

		// Token: 0x06000E0C RID: 3596 RVA: 0x0003E3F8 File Offset: 0x0003C5F8
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<SpeechAutoAttendantManager>(this);
		}

		// Token: 0x06000E0D RID: 3597 RVA: 0x0003E400 File Offset: 0x0003C600
		protected override void LookupRecipientAndDialPlan(BaseUMCallSession vo, PhoneNumber numberToDial)
		{
			if (base.SelectedResultType == ResultType.Department)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.AutoAttendantTracer, this, "Result is a department, assuming same dialplan as originating dialplan.", new object[0]);
				base.TargetDialPlan = base.OriginatingDialPlan;
				return;
			}
			base.LookupRecipientAndDialPlan(vo, numberToDial);
		}

		// Token: 0x06000E0E RID: 3598 RVA: 0x0003E436 File Offset: 0x0003C636
		protected override void ConfigureForCall(BaseUMCallSession vo)
		{
			this.commonAABehavior.Configure();
		}

		// Token: 0x06000E0F RID: 3599 RVA: 0x0003E444 File Offset: 0x0003C644
		protected override SearchGrammarFile CreateNamesGrammar(BaseUMCallSession vo)
		{
			ExAssert.RetailAssert(this.DirectoryGrammarHandler != null, "DirectoryGrammarHandler was not pre-initialized for Speech AA Call");
			SearchGrammarFile searchGrammarFile = this.DirectoryGrammarHandler.WaitForPrepareGrammarCompletion();
			if (searchGrammarFile == null)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.AsrContactsTracer, this, "SpeechAAManager: CreateNamesGrammar : Gal Grammar Fetch Error for grammar='{0}'", new object[]
				{
					this.DirectoryGrammarHandler
				});
				GalGrammarFile.LogErrorEvent(vo.CurrentCallContext);
			}
			return searchGrammarFile;
		}

		// Token: 0x06000E10 RID: 3600 RVA: 0x0003E4A3 File Offset: 0x0003C6A3
		protected override bool CheckDialPermissions(BaseUMCallSession vo, out PhoneNumber numberToDial)
		{
			return DialPermissions.Check(base.CanonicalizedNumber, vo.CurrentCallContext.AutoAttendantInfo, vo.CurrentCallContext.DialPlan, base.TargetDialPlan, out numberToDial);
		}

		// Token: 0x04000AB3 RID: 2739
		private AutoAttendantCore commonAABehavior;

		// Token: 0x04000AB4 RID: 2740
		private bool repeatMainMenu = true;

		// Token: 0x020001DD RID: 477
		internal new class ConfigClass : AsrContactsManager.ConfigClass
		{
			// Token: 0x06000E11 RID: 3601 RVA: 0x0003E4CD File Offset: 0x0003C6CD
			public ConfigClass(ActivityManagerConfig manager) : base(manager)
			{
			}

			// Token: 0x06000E12 RID: 3602 RVA: 0x0003E4D6 File Offset: 0x0003C6D6
			internal override ActivityManager CreateActivityManager(ActivityManager manager)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "Constructing AutoAttendant activity manager.", new object[0]);
				return new SpeechAutoAttendantManager(manager, this);
			}
		}
	}
}
