using System;

namespace Microsoft.Exchange.Diagnostics.Components.Services.PSCmdlets
{
	// Token: 0x02000335 RID: 821
	public static class ExTraceGlobals
	{
		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x06001261 RID: 4705 RVA: 0x0004F11E File Offset: 0x0004D31E
		public static Trace MessageInspectorTracer
		{
			get
			{
				if (ExTraceGlobals.messageInspectorTracer == null)
				{
					ExTraceGlobals.messageInspectorTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.messageInspectorTracer;
			}
		}

		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x06001262 RID: 4706 RVA: 0x0004F13C File Offset: 0x0004D33C
		public static Trace KnownExceptionTracer
		{
			get
			{
				if (ExTraceGlobals.knownExceptionTracer == null)
				{
					ExTraceGlobals.knownExceptionTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.knownExceptionTracer;
			}
		}

		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x06001263 RID: 4707 RVA: 0x0004F15A File Offset: 0x0004D35A
		public static Trace PowerShellTracer
		{
			get
			{
				if (ExTraceGlobals.powerShellTracer == null)
				{
					ExTraceGlobals.powerShellTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.powerShellTracer;
			}
		}

		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x06001264 RID: 4708 RVA: 0x0004F178 File Offset: 0x0004D378
		public static Trace RbacAuthorizationTracer
		{
			get
			{
				if (ExTraceGlobals.rbacAuthorizationTracer == null)
				{
					ExTraceGlobals.rbacAuthorizationTracer = new Trace(ExTraceGlobals.componentGuid, 3);
				}
				return ExTraceGlobals.rbacAuthorizationTracer;
			}
		}

		// Token: 0x0400170F RID: 5903
		private static Guid componentGuid = new Guid("0df9c122-5f11-416d-9ed1-7b6dd48beb8e");

		// Token: 0x04001710 RID: 5904
		private static Trace messageInspectorTracer = null;

		// Token: 0x04001711 RID: 5905
		private static Trace knownExceptionTracer = null;

		// Token: 0x04001712 RID: 5906
		private static Trace powerShellTracer = null;

		// Token: 0x04001713 RID: 5907
		private static Trace rbacAuthorizationTracer = null;
	}
}
