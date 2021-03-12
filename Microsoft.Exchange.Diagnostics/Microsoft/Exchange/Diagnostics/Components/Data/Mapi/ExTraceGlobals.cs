using System;

namespace Microsoft.Exchange.Diagnostics.Components.Data.Mapi
{
	// Token: 0x0200033D RID: 829
	public static class ExTraceGlobals
	{
		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x0600131E RID: 4894 RVA: 0x00050BCF File Offset: 0x0004EDCF
		public static Trace MapiSessionTracer
		{
			get
			{
				if (ExTraceGlobals.mapiSessionTracer == null)
				{
					ExTraceGlobals.mapiSessionTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.mapiSessionTracer;
			}
		}

		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x0600131F RID: 4895 RVA: 0x00050BED File Offset: 0x0004EDED
		public static Trace MapiObjectTracer
		{
			get
			{
				if (ExTraceGlobals.mapiObjectTracer == null)
				{
					ExTraceGlobals.mapiObjectTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.mapiObjectTracer;
			}
		}

		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x06001320 RID: 4896 RVA: 0x00050C0B File Offset: 0x0004EE0B
		public static Trace PropertyBagTracer
		{
			get
			{
				if (ExTraceGlobals.propertyBagTracer == null)
				{
					ExTraceGlobals.propertyBagTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.propertyBagTracer;
			}
		}

		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x06001321 RID: 4897 RVA: 0x00050C29 File Offset: 0x0004EE29
		public static Trace MessageStoreTracer
		{
			get
			{
				if (ExTraceGlobals.messageStoreTracer == null)
				{
					ExTraceGlobals.messageStoreTracer = new Trace(ExTraceGlobals.componentGuid, 3);
				}
				return ExTraceGlobals.messageStoreTracer;
			}
		}

		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x06001322 RID: 4898 RVA: 0x00050C47 File Offset: 0x0004EE47
		public static Trace FolderTracer
		{
			get
			{
				if (ExTraceGlobals.folderTracer == null)
				{
					ExTraceGlobals.folderTracer = new Trace(ExTraceGlobals.componentGuid, 4);
				}
				return ExTraceGlobals.folderTracer;
			}
		}

		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x06001323 RID: 4899 RVA: 0x00050C65 File Offset: 0x0004EE65
		public static Trace LogonStatisticsTracer
		{
			get
			{
				if (ExTraceGlobals.logonStatisticsTracer == null)
				{
					ExTraceGlobals.logonStatisticsTracer = new Trace(ExTraceGlobals.componentGuid, 5);
				}
				return ExTraceGlobals.logonStatisticsTracer;
			}
		}

		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x06001324 RID: 4900 RVA: 0x00050C83 File Offset: 0x0004EE83
		public static Trace ConvertorTracer
		{
			get
			{
				if (ExTraceGlobals.convertorTracer == null)
				{
					ExTraceGlobals.convertorTracer = new Trace(ExTraceGlobals.componentGuid, 6);
				}
				return ExTraceGlobals.convertorTracer;
			}
		}

		// Token: 0x040017CC RID: 6092
		private static Guid componentGuid = new Guid("C9AAFFBB-C5D9-4e08-B398-7733BC04D45E");

		// Token: 0x040017CD RID: 6093
		private static Trace mapiSessionTracer = null;

		// Token: 0x040017CE RID: 6094
		private static Trace mapiObjectTracer = null;

		// Token: 0x040017CF RID: 6095
		private static Trace propertyBagTracer = null;

		// Token: 0x040017D0 RID: 6096
		private static Trace messageStoreTracer = null;

		// Token: 0x040017D1 RID: 6097
		private static Trace folderTracer = null;

		// Token: 0x040017D2 RID: 6098
		private static Trace logonStatisticsTracer = null;

		// Token: 0x040017D3 RID: 6099
		private static Trace convertorTracer = null;
	}
}
