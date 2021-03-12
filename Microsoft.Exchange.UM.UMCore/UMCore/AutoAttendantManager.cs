using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x0200003A RID: 58
	internal class AutoAttendantManager : ActivityManager, IAutoAttendantUI
	{
		// Token: 0x0600026F RID: 623 RVA: 0x0000B6BE File Offset: 0x000098BE
		internal AutoAttendantManager(ActivityManager manager, AutoAttendantManager.ConfigClass config) : base(manager, config)
		{
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000270 RID: 624 RVA: 0x0000B6C8 File Offset: 0x000098C8
		internal bool StarOutToDialPlanEnabled
		{
			get
			{
				return this.commonAABehavior.StarOutToDialPlanEnabled;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000271 RID: 625 RVA: 0x0000B6D5 File Offset: 0x000098D5
		internal bool ForwardCallsToDefaultMailbox
		{
			get
			{
				return this.commonAABehavior.ForwardCallsToDefaultMailbox;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000272 RID: 626 RVA: 0x0000B6E2 File Offset: 0x000098E2
		internal string BusinessName
		{
			get
			{
				return this.commonAABehavior.BusinessName;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000273 RID: 627 RVA: 0x0000B6EF File Offset: 0x000098EF
		internal string BusinessLocation
		{
			get
			{
				return this.commonAABehavior.Config.BusinessLocation;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000274 RID: 628 RVA: 0x0000B701 File Offset: 0x00009901
		internal bool BusinessLocationIsSet
		{
			get
			{
				return !string.IsNullOrEmpty(this.BusinessLocation);
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000275 RID: 629 RVA: 0x0000B711 File Offset: 0x00009911
		internal UMAutoAttendant ThisAutoAttendant
		{
			get
			{
				return this.commonAABehavior.Config;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000276 RID: 630 RVA: 0x0000B71E File Offset: 0x0000991E
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

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000277 RID: 631 RVA: 0x0000B744 File Offset: 0x00009944
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

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000278 RID: 632 RVA: 0x0000B771 File Offset: 0x00009971
		internal AutoAttendantLocationContext AALocationContext
		{
			get
			{
				return new AutoAttendantLocationContext(this.ThisAutoAttendant, this.SelectedMenu);
			}
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0000B784 File Offset: 0x00009984
		public object ReadProperty(string name)
		{
			return this.ReadVariable(name);
		}

		// Token: 0x0600027A RID: 634 RVA: 0x0000B78D File Offset: 0x0000998D
		public object ReadGlobalProperty(string name)
		{
			return this.GlobalManager.ReadVariable(name);
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0000B79B File Offset: 0x0000999B
		public void WriteProperty(string name, object value)
		{
			base.WriteVariable(name, value);
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0000B7A5 File Offset: 0x000099A5
		public void WriteGlobalProperty(string name, object value)
		{
			this.GlobalManager.WriteVariable(name, value);
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0000B7B4 File Offset: 0x000099B4
		public void SetTextPrompt(string name, string promptText)
		{
			base.SetTextPartVariable(name, promptText);
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0000B7BE File Offset: 0x000099BE
		public void SetWavePrompt(string name, ITempWavFile promptFile)
		{
			base.SetWavePartVariable(name, promptFile);
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0000B7C8 File Offset: 0x000099C8
		internal override void Start(BaseUMCallSession vo, string refInfo)
		{
			this.commonAABehavior = AutoAttendantCore.Create(this, vo);
			this.Configure(vo);
			base.WriteVariable("tuiPromptEditingEnabled", this.GlobalManager.ReadVariable("tuiPromptEditingEnabled"));
			base.Start(vo, refInfo);
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0000B801 File Offset: 0x00009A01
		internal override void CheckAuthorization(UMSubscriber u)
		{
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0000B804 File Offset: 0x00009A04
		internal override TransitionBase ExecuteAction(string action, BaseUMCallSession vo)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.AutoAttendantTracer, this, "AutoAttendant Manager asked to do action: {0}.", new object[]
			{
				action
			});
			string input = null;
			if (string.Equals(action, "setPromptProvContext", StringComparison.OrdinalIgnoreCase))
			{
				this.GlobalManager.WriteVariable("promptProvContext", "AutoAttendant");
			}
			else if (!this.commonAABehavior.ExecuteAction(action, vo, ref input))
			{
				return base.ExecuteAction(action, vo);
			}
			return base.CurrentActivity.GetTransition(input);
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0000B87A File Offset: 0x00009A7A
		internal string PrepareForCallAnswering(BaseUMCallSession session)
		{
			return this.commonAABehavior.Action_PrepareForCallAnswering();
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0000B888 File Offset: 0x00009A88
		internal string CheckNonUmExtension(BaseUMCallSession vo)
		{
			string result = null;
			DialPermissionWrapper dialPermissionWrapper = DialPermissionWrapperFactory.Create(vo);
			if (dialPermissionWrapper.CallingNonUmExtensionsAllowed)
			{
				result = "denyCallNonUmExtension";
				PhoneNumber phone;
				if (DirectorySearchManager.DialableNonUmExtension(base.DtmfDigits, vo.CurrentCallContext.DialPlan, out phone))
				{
					PhoneUtil.SetTransferTargetPhone(this, TransferExtension.UserExtension, phone);
					result = "allowCallNonUmExtension";
				}
			}
			return result;
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0000B8D5 File Offset: 0x00009AD5
		internal override void OnTimeout(BaseUMCallSession vo, UMCallSessionEventArgs callSessionEventArgs)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.AutoAttendantTracer, this, "Timeout.", new object[0]);
			this.commonAABehavior.OnTimeout();
			base.CurrentActivity.OnTimeout(vo, callSessionEventArgs);
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000B908 File Offset: 0x00009B08
		internal override void OnTransferComplete(BaseUMCallSession vo, UMCallSessionEventArgs callSessionEventArgs)
		{
			TransferExtension transferExtension = (TransferExtension)this.ReadVariable("transferExtension");
			CallIdTracer.TraceDebug(ExTraceGlobals.AutoAttendantTracer, this, "OnTransferComplete Type = {0}.", new object[]
			{
				transferExtension
			});
			this.commonAABehavior.OnTransferComplete(transferExtension);
			base.OnTransferComplete(vo, callSessionEventArgs);
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0000B95B File Offset: 0x00009B5B
		internal override void OnInput(BaseUMCallSession vo, UMCallSessionEventArgs callSessionEventArgs)
		{
			this.commonAABehavior.OnInput();
			base.OnInput(vo, callSessionEventArgs);
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0000B970 File Offset: 0x00009B70
		internal override void OnUserHangup(BaseUMCallSession vo, UMCallSessionEventArgs callSessionEventArgs)
		{
			this.commonAABehavior.OnHangup();
			base.OnUserHangup(vo, callSessionEventArgs);
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000B988 File Offset: 0x00009B88
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

		// Token: 0x06000289 RID: 649 RVA: 0x0000B9C0 File Offset: 0x00009BC0
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<AutoAttendantManager>(this);
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0000B9C8 File Offset: 0x00009BC8
		private void Configure(BaseUMCallSession vo)
		{
			this.commonAABehavior.Configure();
		}

		// Token: 0x040000CE RID: 206
		private AutoAttendantCore commonAABehavior;

		// Token: 0x0200003B RID: 59
		internal class ConfigClass : ActivityManagerConfig
		{
			// Token: 0x0600028B RID: 651 RVA: 0x0000B9D5 File Offset: 0x00009BD5
			internal ConfigClass(ActivityManagerConfig manager) : base(manager)
			{
			}

			// Token: 0x0600028C RID: 652 RVA: 0x0000B9DE File Offset: 0x00009BDE
			internal override ActivityManager CreateActivityManager(ActivityManager manager)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "Constructing AutoAttendant activity manager.", new object[0]);
				return new AutoAttendantManager(manager, this);
			}
		}
	}
}
