using System;
using System.Globalization;
using System.Net;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x0200020C RID: 524
	internal class UmDiagnosticManager : ActivityManager
	{
		// Token: 0x06000F5E RID: 3934 RVA: 0x000457E0 File Offset: 0x000439E0
		internal UmDiagnosticManager(ActivityManager manager, UmDiagnosticManager.ConfigClass config) : base(manager, config)
		{
		}

		// Token: 0x06000F5F RID: 3935 RVA: 0x000457EA File Offset: 0x000439EA
		internal override void CheckAuthorization(UMSubscriber u)
		{
		}

		// Token: 0x06000F60 RID: 3936 RVA: 0x000457EC File Offset: 0x000439EC
		internal override void Start(BaseUMCallSession callSession, string refInfo)
		{
			callSession.CurrentCallContext.CallType = 7;
			callSession.CurrentCallContext.IsDiagnosticCall = true;
			base.Start(callSession, refInfo);
		}

		// Token: 0x06000F61 RID: 3937 RVA: 0x00045810 File Offset: 0x00043A10
		internal override TransitionBase ExecuteAction(string action, BaseUMCallSession vo)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "UmDiagnosticManager executing action={0}.", new object[]
			{
				action
			});
			string input = null;
			if (string.Equals(action, "isLocal", StringComparison.OrdinalIgnoreCase))
			{
				if (vo.CurrentCallContext.IsLocalDiagnosticCall)
				{
					input = "local";
				}
				return base.CurrentActivity.GetTransition(input);
			}
			if (string.Equals(action, "sendDtmf", StringComparison.OrdinalIgnoreCase))
			{
				this.SendDiagnosticDtmf(vo);
				return new StopTransition();
			}
			return base.ExecuteAction(action, vo);
		}

		// Token: 0x06000F62 RID: 3938 RVA: 0x0004588E File Offset: 0x00043A8E
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<UmDiagnosticManager>(this);
		}

		// Token: 0x06000F63 RID: 3939 RVA: 0x00045898 File Offset: 0x00043A98
		private void SendDiagnosticDtmf(BaseUMCallSession callSession)
		{
			string text = this.GlobalManager.ReadVariable("diagnosticDtmfDigits") as string;
			string str;
			try
			{
				IPAddress localIPv4Address = Utils.GetLocalIPv4Address();
				str = localIPv4Address.ToString().Replace('.', '*');
			}
			catch (ProtocolViolationException ex)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "UmDiagnosticManager has an issue = {0}.", new object[]
				{
					ex.Message
				});
				str = string.Empty;
			}
			string str2 = "#" + str + "#";
			string str3;
			if (UmServiceGlobals.ArePerfCountersEnabled)
			{
				long rawValue = GeneralCounters.CurrentCalls.RawValue;
				if (rawValue < 0L)
				{
					str3 = "0";
				}
				else
				{
					str3 = rawValue.ToString(CultureInfo.InvariantCulture);
				}
			}
			else
			{
				str3 = "0";
			}
			text = text + str2 + str3;
			string text2 = string.Concat(new object[]
			{
				"#",
				text.Length,
				"#",
				text
			});
			UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_DiagnosticResponseSequence, null, new object[]
			{
				text2,
				callSession.CallId
			});
			callSession.SendDtmf(text2, new TimeSpan(0, 0, 0, 0, 1000));
		}

		// Token: 0x0200020D RID: 525
		internal class ConfigClass : ActivityManagerConfig
		{
			// Token: 0x06000F64 RID: 3940 RVA: 0x000459E0 File Offset: 0x00043BE0
			public ConfigClass(ActivityManagerConfig manager) : base(manager)
			{
			}

			// Token: 0x06000F65 RID: 3941 RVA: 0x000459E9 File Offset: 0x00043BE9
			internal override ActivityManager CreateActivityManager(ActivityManager manager)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "Constructing UMDiagnostic activity manager.", new object[0]);
				return new UmDiagnosticManager(manager, this);
			}
		}
	}
}
