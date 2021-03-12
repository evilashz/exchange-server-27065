using System;

namespace Microsoft.Exchange.Diagnostics.Components.Messenger
{
	// Token: 0x020003B6 RID: 950
	public static class ExTraceGlobals
	{
		// Token: 0x1700090E RID: 2318
		// (get) Token: 0x060016DF RID: 5855 RVA: 0x00058CFE File Offset: 0x00056EFE
		public static Trace CoreTracer
		{
			get
			{
				if (ExTraceGlobals.coreTracer == null)
				{
					ExTraceGlobals.coreTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.coreTracer;
			}
		}

		// Token: 0x1700090F RID: 2319
		// (get) Token: 0x060016E0 RID: 5856 RVA: 0x00058D1C File Offset: 0x00056F1C
		public static Trace MSNPTracer
		{
			get
			{
				if (ExTraceGlobals.mSNPTracer == null)
				{
					ExTraceGlobals.mSNPTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.mSNPTracer;
			}
		}

		// Token: 0x17000910 RID: 2320
		// (get) Token: 0x060016E1 RID: 5857 RVA: 0x00058D3A File Offset: 0x00056F3A
		public static Trace ABCHTracer
		{
			get
			{
				if (ExTraceGlobals.aBCHTracer == null)
				{
					ExTraceGlobals.aBCHTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.aBCHTracer;
			}
		}

		// Token: 0x17000911 RID: 2321
		// (get) Token: 0x060016E2 RID: 5858 RVA: 0x00058D58 File Offset: 0x00056F58
		public static Trace SharingTracer
		{
			get
			{
				if (ExTraceGlobals.sharingTracer == null)
				{
					ExTraceGlobals.sharingTracer = new Trace(ExTraceGlobals.componentGuid, 3);
				}
				return ExTraceGlobals.sharingTracer;
			}
		}

		// Token: 0x17000912 RID: 2322
		// (get) Token: 0x060016E3 RID: 5859 RVA: 0x00058D76 File Offset: 0x00056F76
		public static Trace StorageTracer
		{
			get
			{
				if (ExTraceGlobals.storageTracer == null)
				{
					ExTraceGlobals.storageTracer = new Trace(ExTraceGlobals.componentGuid, 4);
				}
				return ExTraceGlobals.storageTracer;
			}
		}

		// Token: 0x04001B8D RID: 7053
		private static Guid componentGuid = new Guid("5099defc-8a21-405a-ba04-e0857dd8d94e");

		// Token: 0x04001B8E RID: 7054
		private static Trace coreTracer = null;

		// Token: 0x04001B8F RID: 7055
		private static Trace mSNPTracer = null;

		// Token: 0x04001B90 RID: 7056
		private static Trace aBCHTracer = null;

		// Token: 0x04001B91 RID: 7057
		private static Trace sharingTracer = null;

		// Token: 0x04001B92 RID: 7058
		private static Trace storageTracer = null;
	}
}
