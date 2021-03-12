using System;
using System.Text;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.TroubleshootingTool.Shared;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x0200022F RID: 559
	internal class UmTroubleshootingToolManager : ActivityManager
	{
		// Token: 0x06001042 RID: 4162 RVA: 0x00048708 File Offset: 0x00046908
		internal UmTroubleshootingToolManager(ActivityManager manager, UmTroubleshootingToolManager.ConfigClass config) : base(manager, config)
		{
		}

		// Token: 0x06001043 RID: 4163 RVA: 0x00048712 File Offset: 0x00046912
		internal override void CheckAuthorization(UMSubscriber u)
		{
		}

		// Token: 0x06001044 RID: 4164 RVA: 0x00048714 File Offset: 0x00046914
		internal string ConfirmAcceptedCallType(BaseUMCallSession vo)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.DiagnosticTracer, this, "ConfirmAcceptedCallType", new object[0]);
			InfoMessage infoMessage = new InfoMessage();
			infoMessage.ContentType = CommonConstants.ContentTypeTextPlain;
			infoMessage.Headers["msexum-diagtool-info"] = 0.ToString();
			UMToolCallAccepted umtoolCallAccepted = new UMToolCallAccepted(UmTroubleshootingToolManager.GetToolCallType(vo.CurrentCallContext.CallType), (vo.CurrentCallContext.CalleeInfo == null) ? string.Empty : vo.CurrentCallContext.CalleeInfo.DisplayName, vo.CurrentCallContext.DialPlan.Name, (vo.CurrentCallContext.AutoAttendantInfo == null) ? string.Empty : vo.CurrentCallContext.AutoAttendantInfo.Name, string.Empty, (vo.CurrentCallContext.DialPlan.URIType == UMUriType.TelExtn && vo.CurrentCallContext.CalleeInfo != null) ? vo.CurrentCallContext.CalleeInfo.ADRecipient.UMExtension : string.Empty, (vo.CurrentCallContext.DialPlan.URIType != UMUriType.TelExtn && vo.CurrentCallContext.CalleeInfo != null) ? vo.CurrentCallContext.CalleeInfo.ADRecipient.UMExtension : string.Empty, (vo.CurrentCallContext.CalleeInfo == null) ? string.Empty : vo.CurrentCallContext.CalleeInfo.MailAddress);
			infoMessage.Body = Encoding.UTF8.GetBytes(UMTypeSerializer.SerializeToString<UMToolCallAccepted>(umtoolCallAccepted));
			vo.SendMessage(infoMessage);
			return "stopEvent";
		}

		// Token: 0x06001045 RID: 4165 RVA: 0x00048894 File Offset: 0x00046A94
		internal override void OnMessageSent(BaseUMCallSession vo, EventArgs e)
		{
			base.OnMessageSent(vo, e);
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "UmTroubleshootingToolManager in OnMessageSent.", new object[0]);
			TransitionBase transition = this.GetTransition("toolInfoSent");
			if (transition != null)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "Menu Activity making toolinfosent transition: {0}.", new object[]
				{
					transition
				});
				transition.Execute(this, vo);
				return;
			}
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "Menu Activity has no toolinfosent transition", new object[0]);
			this.DropCall(vo, DropCallReason.UserError);
		}

		// Token: 0x06001046 RID: 4166 RVA: 0x00048914 File Offset: 0x00046B14
		internal string EchoBackDtmf(BaseUMCallSession vo)
		{
			PIIMessage data = PIIMessage.Create(PIIType._PII, base.DtmfDigits);
			CallIdTracer.TraceDebug(ExTraceGlobals.DiagnosticTracer, this, data, "EchoBackDtmf. Dtmfs received so far _PII", new object[0]);
			if (!string.IsNullOrEmpty(base.DtmfDigits))
			{
				vo.SendDtmf(base.DtmfDigits, TimeSpan.Zero);
				return "stopEvent";
			}
			this.DropCall(vo, DropCallReason.GracefulHangup);
			return null;
		}

		// Token: 0x06001047 RID: 4167 RVA: 0x00048972 File Offset: 0x00046B72
		internal string SendStopRecordingDtmf(BaseUMCallSession vo)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.DiagnosticTracer, this, "SendStopRecordingDtmf. sending #", new object[0]);
			vo.SendDtmf("#", TimeSpan.Zero);
			return "stopEvent";
		}

		// Token: 0x06001048 RID: 4168 RVA: 0x000489A0 File Offset: 0x00046BA0
		private static string GetToolCallType(CallType type)
		{
			switch (type)
			{
			case 2:
				return type.ToString();
			case 3:
				return type.ToString();
			case 4:
				return type.ToString();
			case 5:
				return type.ToString();
			case 9:
				return type.ToString();
			}
			return 0.ToString();
		}

		// Token: 0x06001049 RID: 4169 RVA: 0x00048A20 File Offset: 0x00046C20
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<UmTroubleshootingToolManager>(this);
		}

		// Token: 0x02000230 RID: 560
		internal class ConfigClass : ActivityManagerConfig
		{
			// Token: 0x0600104A RID: 4170 RVA: 0x00048A28 File Offset: 0x00046C28
			public ConfigClass(ActivityManagerConfig manager) : base(manager)
			{
			}

			// Token: 0x0600104B RID: 4171 RVA: 0x00048A31 File Offset: 0x00046C31
			internal override ActivityManager CreateActivityManager(ActivityManager manager)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "Constructing UmTroubleshootingTool activity manager.", new object[0]);
				return new UmTroubleshootingToolManager(manager, this);
			}
		}
	}
}
