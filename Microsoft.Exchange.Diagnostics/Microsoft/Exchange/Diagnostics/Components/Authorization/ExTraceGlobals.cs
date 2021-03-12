using System;
using Microsoft.Exchange.Diagnostics.FaultInjection;

namespace Microsoft.Exchange.Diagnostics.Components.Authorization
{
	// Token: 0x020003A0 RID: 928
	public static class ExTraceGlobals
	{
		// Token: 0x170008AD RID: 2221
		// (get) Token: 0x06001668 RID: 5736 RVA: 0x00057DBB File Offset: 0x00055FBB
		public static Trace ADConfigTracer
		{
			get
			{
				if (ExTraceGlobals.aDConfigTracer == null)
				{
					ExTraceGlobals.aDConfigTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.aDConfigTracer;
			}
		}

		// Token: 0x170008AE RID: 2222
		// (get) Token: 0x06001669 RID: 5737 RVA: 0x00057DD9 File Offset: 0x00055FD9
		public static Trace AccessDeniedTracer
		{
			get
			{
				if (ExTraceGlobals.accessDeniedTracer == null)
				{
					ExTraceGlobals.accessDeniedTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.accessDeniedTracer;
			}
		}

		// Token: 0x170008AF RID: 2223
		// (get) Token: 0x0600166A RID: 5738 RVA: 0x00057DF7 File Offset: 0x00055FF7
		public static Trace RunspaceConfigTracer
		{
			get
			{
				if (ExTraceGlobals.runspaceConfigTracer == null)
				{
					ExTraceGlobals.runspaceConfigTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.runspaceConfigTracer;
			}
		}

		// Token: 0x170008B0 RID: 2224
		// (get) Token: 0x0600166B RID: 5739 RVA: 0x00057E15 File Offset: 0x00056015
		public static Trace AccessCheckTracer
		{
			get
			{
				if (ExTraceGlobals.accessCheckTracer == null)
				{
					ExTraceGlobals.accessCheckTracer = new Trace(ExTraceGlobals.componentGuid, 3);
				}
				return ExTraceGlobals.accessCheckTracer;
			}
		}

		// Token: 0x170008B1 RID: 2225
		// (get) Token: 0x0600166C RID: 5740 RVA: 0x00057E33 File Offset: 0x00056033
		public static Trace PublicCreationAPITracer
		{
			get
			{
				if (ExTraceGlobals.publicCreationAPITracer == null)
				{
					ExTraceGlobals.publicCreationAPITracer = new Trace(ExTraceGlobals.componentGuid, 4);
				}
				return ExTraceGlobals.publicCreationAPITracer;
			}
		}

		// Token: 0x170008B2 RID: 2226
		// (get) Token: 0x0600166D RID: 5741 RVA: 0x00057E51 File Offset: 0x00056051
		public static Trace PublicInstanceAPITracer
		{
			get
			{
				if (ExTraceGlobals.publicInstanceAPITracer == null)
				{
					ExTraceGlobals.publicInstanceAPITracer = new Trace(ExTraceGlobals.componentGuid, 5);
				}
				return ExTraceGlobals.publicInstanceAPITracer;
			}
		}

		// Token: 0x170008B3 RID: 2227
		// (get) Token: 0x0600166E RID: 5742 RVA: 0x00057E6F File Offset: 0x0005606F
		public static Trace IssBuilderTracer
		{
			get
			{
				if (ExTraceGlobals.issBuilderTracer == null)
				{
					ExTraceGlobals.issBuilderTracer = new Trace(ExTraceGlobals.componentGuid, 6);
				}
				return ExTraceGlobals.issBuilderTracer;
			}
		}

		// Token: 0x170008B4 RID: 2228
		// (get) Token: 0x0600166F RID: 5743 RVA: 0x00057E8D File Offset: 0x0005608D
		public static Trace PublicPluginAPITracer
		{
			get
			{
				if (ExTraceGlobals.publicPluginAPITracer == null)
				{
					ExTraceGlobals.publicPluginAPITracer = new Trace(ExTraceGlobals.componentGuid, 7);
				}
				return ExTraceGlobals.publicPluginAPITracer;
			}
		}

		// Token: 0x170008B5 RID: 2229
		// (get) Token: 0x06001670 RID: 5744 RVA: 0x00057EAB File Offset: 0x000560AB
		public static Trace IssBuilderDetailTracer
		{
			get
			{
				if (ExTraceGlobals.issBuilderDetailTracer == null)
				{
					ExTraceGlobals.issBuilderDetailTracer = new Trace(ExTraceGlobals.componentGuid, 8);
				}
				return ExTraceGlobals.issBuilderDetailTracer;
			}
		}

		// Token: 0x170008B6 RID: 2230
		// (get) Token: 0x06001671 RID: 5745 RVA: 0x00057EC9 File Offset: 0x000560C9
		public static FaultInjectionTrace FaultInjectionTracer
		{
			get
			{
				if (ExTraceGlobals.faultInjectionTracer == null)
				{
					ExTraceGlobals.faultInjectionTracer = new FaultInjectionTrace(ExTraceGlobals.componentGuid, 9);
				}
				return ExTraceGlobals.faultInjectionTracer;
			}
		}

		// Token: 0x04001B16 RID: 6934
		private static Guid componentGuid = new Guid("96825f4e-464a-44ef-af25-a76d1d0cec77");

		// Token: 0x04001B17 RID: 6935
		private static Trace aDConfigTracer = null;

		// Token: 0x04001B18 RID: 6936
		private static Trace accessDeniedTracer = null;

		// Token: 0x04001B19 RID: 6937
		private static Trace runspaceConfigTracer = null;

		// Token: 0x04001B1A RID: 6938
		private static Trace accessCheckTracer = null;

		// Token: 0x04001B1B RID: 6939
		private static Trace publicCreationAPITracer = null;

		// Token: 0x04001B1C RID: 6940
		private static Trace publicInstanceAPITracer = null;

		// Token: 0x04001B1D RID: 6941
		private static Trace issBuilderTracer = null;

		// Token: 0x04001B1E RID: 6942
		private static Trace publicPluginAPITracer = null;

		// Token: 0x04001B1F RID: 6943
		private static Trace issBuilderDetailTracer = null;

		// Token: 0x04001B20 RID: 6944
		private static FaultInjectionTrace faultInjectionTracer = null;
	}
}
