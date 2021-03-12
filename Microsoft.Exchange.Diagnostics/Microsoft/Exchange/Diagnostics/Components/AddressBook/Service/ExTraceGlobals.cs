using System;

namespace Microsoft.Exchange.Diagnostics.Components.AddressBook.Service
{
	// Token: 0x020003A8 RID: 936
	public static class ExTraceGlobals
	{
		// Token: 0x170008E4 RID: 2276
		// (get) Token: 0x060016A7 RID: 5799 RVA: 0x00058628 File Offset: 0x00056828
		public static Trace GeneralTracer
		{
			get
			{
				if (ExTraceGlobals.generalTracer == null)
				{
					ExTraceGlobals.generalTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.generalTracer;
			}
		}

		// Token: 0x170008E5 RID: 2277
		// (get) Token: 0x060016A8 RID: 5800 RVA: 0x00058646 File Offset: 0x00056846
		public static Trace NspiTracer
		{
			get
			{
				if (ExTraceGlobals.nspiTracer == null)
				{
					ExTraceGlobals.nspiTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.nspiTracer;
			}
		}

		// Token: 0x170008E6 RID: 2278
		// (get) Token: 0x060016A9 RID: 5801 RVA: 0x00058664 File Offset: 0x00056864
		public static Trace ReferralTracer
		{
			get
			{
				if (ExTraceGlobals.referralTracer == null)
				{
					ExTraceGlobals.referralTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.referralTracer;
			}
		}

		// Token: 0x170008E7 RID: 2279
		// (get) Token: 0x060016AA RID: 5802 RVA: 0x00058682 File Offset: 0x00056882
		public static Trace PropertyMapperTracer
		{
			get
			{
				if (ExTraceGlobals.propertyMapperTracer == null)
				{
					ExTraceGlobals.propertyMapperTracer = new Trace(ExTraceGlobals.componentGuid, 3);
				}
				return ExTraceGlobals.propertyMapperTracer;
			}
		}

		// Token: 0x170008E8 RID: 2280
		// (get) Token: 0x060016AB RID: 5803 RVA: 0x000586A0 File Offset: 0x000568A0
		public static Trace ModCacheTracer
		{
			get
			{
				if (ExTraceGlobals.modCacheTracer == null)
				{
					ExTraceGlobals.modCacheTracer = new Trace(ExTraceGlobals.componentGuid, 4);
				}
				return ExTraceGlobals.modCacheTracer;
			}
		}

		// Token: 0x170008E9 RID: 2281
		// (get) Token: 0x060016AC RID: 5804 RVA: 0x000586BE File Offset: 0x000568BE
		public static Trace NspiConnectionTracer
		{
			get
			{
				if (ExTraceGlobals.nspiConnectionTracer == null)
				{
					ExTraceGlobals.nspiConnectionTracer = new Trace(ExTraceGlobals.componentGuid, 5);
				}
				return ExTraceGlobals.nspiConnectionTracer;
			}
		}

		// Token: 0x04001B55 RID: 6997
		private static Guid componentGuid = new Guid("583dfb2d-4ab4-4416-848b-88cc74d39e1f");

		// Token: 0x04001B56 RID: 6998
		private static Trace generalTracer = null;

		// Token: 0x04001B57 RID: 6999
		private static Trace nspiTracer = null;

		// Token: 0x04001B58 RID: 7000
		private static Trace referralTracer = null;

		// Token: 0x04001B59 RID: 7001
		private static Trace propertyMapperTracer = null;

		// Token: 0x04001B5A RID: 7002
		private static Trace modCacheTracer = null;

		// Token: 0x04001B5B RID: 7003
		private static Trace nspiConnectionTracer = null;
	}
}
