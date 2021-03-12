using System;
using Microsoft.Exchange.Diagnostics.FaultInjection;

namespace Microsoft.Exchange.Diagnostics.Components.ManagedStore.HA
{
	// Token: 0x02000390 RID: 912
	public static class ExTraceGlobals
	{
		// Token: 0x170007FB RID: 2043
		// (get) Token: 0x060015A6 RID: 5542 RVA: 0x0005626C File Offset: 0x0005446C
		public static Trace EsebackTracer
		{
			get
			{
				if (ExTraceGlobals.esebackTracer == null)
				{
					ExTraceGlobals.esebackTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.esebackTracer;
			}
		}

		// Token: 0x170007FC RID: 2044
		// (get) Token: 0x060015A7 RID: 5543 RVA: 0x0005628A File Offset: 0x0005448A
		public static Trace LogReplayStatusTracer
		{
			get
			{
				if (ExTraceGlobals.logReplayStatusTracer == null)
				{
					ExTraceGlobals.logReplayStatusTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.logReplayStatusTracer;
			}
		}

		// Token: 0x170007FD RID: 2045
		// (get) Token: 0x060015A8 RID: 5544 RVA: 0x000562A8 File Offset: 0x000544A8
		public static Trace BlockModeCollectorTracer
		{
			get
			{
				if (ExTraceGlobals.blockModeCollectorTracer == null)
				{
					ExTraceGlobals.blockModeCollectorTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.blockModeCollectorTracer;
			}
		}

		// Token: 0x170007FE RID: 2046
		// (get) Token: 0x060015A9 RID: 5545 RVA: 0x000562C6 File Offset: 0x000544C6
		public static Trace BlockModeMessageStreamTracer
		{
			get
			{
				if (ExTraceGlobals.blockModeMessageStreamTracer == null)
				{
					ExTraceGlobals.blockModeMessageStreamTracer = new Trace(ExTraceGlobals.componentGuid, 3);
				}
				return ExTraceGlobals.blockModeMessageStreamTracer;
			}
		}

		// Token: 0x170007FF RID: 2047
		// (get) Token: 0x060015AA RID: 5546 RVA: 0x000562E4 File Offset: 0x000544E4
		public static Trace BlockModeSenderTracer
		{
			get
			{
				if (ExTraceGlobals.blockModeSenderTracer == null)
				{
					ExTraceGlobals.blockModeSenderTracer = new Trace(ExTraceGlobals.componentGuid, 4);
				}
				return ExTraceGlobals.blockModeSenderTracer;
			}
		}

		// Token: 0x17000800 RID: 2048
		// (get) Token: 0x060015AB RID: 5547 RVA: 0x00056302 File Offset: 0x00054502
		public static Trace JetHADatabaseTracer
		{
			get
			{
				if (ExTraceGlobals.jetHADatabaseTracer == null)
				{
					ExTraceGlobals.jetHADatabaseTracer = new Trace(ExTraceGlobals.componentGuid, 5);
				}
				return ExTraceGlobals.jetHADatabaseTracer;
			}
		}

		// Token: 0x17000801 RID: 2049
		// (get) Token: 0x060015AC RID: 5548 RVA: 0x00056320 File Offset: 0x00054520
		public static Trace LastLogWriterTracer
		{
			get
			{
				if (ExTraceGlobals.lastLogWriterTracer == null)
				{
					ExTraceGlobals.lastLogWriterTracer = new Trace(ExTraceGlobals.componentGuid, 6);
				}
				return ExTraceGlobals.lastLogWriterTracer;
			}
		}

		// Token: 0x17000802 RID: 2050
		// (get) Token: 0x060015AD RID: 5549 RVA: 0x0005633E File Offset: 0x0005453E
		public static FaultInjectionTrace FaultInjectionTracer
		{
			get
			{
				if (ExTraceGlobals.faultInjectionTracer == null)
				{
					ExTraceGlobals.faultInjectionTracer = new FaultInjectionTrace(ExTraceGlobals.componentGuid, 20);
				}
				return ExTraceGlobals.faultInjectionTracer;
			}
		}

		// Token: 0x04001A54 RID: 6740
		private static Guid componentGuid = new Guid("0081576A-CB7C-4aba-9BB4-7D0B44290C2C");

		// Token: 0x04001A55 RID: 6741
		private static Trace esebackTracer = null;

		// Token: 0x04001A56 RID: 6742
		private static Trace logReplayStatusTracer = null;

		// Token: 0x04001A57 RID: 6743
		private static Trace blockModeCollectorTracer = null;

		// Token: 0x04001A58 RID: 6744
		private static Trace blockModeMessageStreamTracer = null;

		// Token: 0x04001A59 RID: 6745
		private static Trace blockModeSenderTracer = null;

		// Token: 0x04001A5A RID: 6746
		private static Trace jetHADatabaseTracer = null;

		// Token: 0x04001A5B RID: 6747
		private static Trace lastLogWriterTracer = null;

		// Token: 0x04001A5C RID: 6748
		private static FaultInjectionTrace faultInjectionTracer = null;
	}
}
