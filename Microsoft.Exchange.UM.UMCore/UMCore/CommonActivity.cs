using System;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000080 RID: 128
	internal abstract class CommonActivity : ActivityBase
	{
		// Token: 0x060005D3 RID: 1491 RVA: 0x000191A6 File Offset: 0x000173A6
		internal CommonActivity(ActivityManager manager, ActivityConfig config) : base(manager, config)
		{
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x000191B0 File Offset: 0x000173B0
		internal override void Start(BaseUMCallSession vo, string refInfo)
		{
			if (vo.CurrentCallContext.IsTestCall || vo.CurrentCallContext.IsTUIDiagnosticCall)
			{
				this.refInfo = refInfo;
				vo.SendStateInfo(vo.CallId, base.UniqueId);
				return;
			}
			this.StartActivity(vo, refInfo);
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x000191EE File Offset: 0x000173EE
		internal override void OnStateInfoSent(BaseUMCallSession vo, UMCallSessionEventArgs voiceObjectEventArgs)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "ActivityBase OnStateInfoSent called.", new object[0]);
			this.StartActivity(vo, this.refInfo);
		}

		// Token: 0x060005D6 RID: 1494
		internal abstract void StartActivity(BaseUMCallSession vo, string refInfo);

		// Token: 0x04000254 RID: 596
		private string refInfo;
	}
}
