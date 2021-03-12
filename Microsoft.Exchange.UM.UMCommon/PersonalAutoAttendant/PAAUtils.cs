using System;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.Diagnostics.LatencyDetection;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.PersonalAutoAttendant
{
	// Token: 0x0200010A RID: 266
	internal class PAAUtils
	{
		// Token: 0x1700021C RID: 540
		// (get) Token: 0x060008D5 RID: 2261 RVA: 0x0002140C File Offset: 0x0001F60C
		internal static LatencyDetectionContextFactory GetCallAnsweringDataFactory
		{
			get
			{
				if (PAAUtils.getCallAnsweringDataFactory == null)
				{
					lock (PAAUtils.syncObj)
					{
						if (PAAUtils.getCallAnsweringDataFactory == null)
						{
							PAAUtils.getCallAnsweringDataFactory = PAAUtils.CreateLatencyDetectionFactory("UM.GetCallAnsweringData");
						}
					}
				}
				return PAAUtils.getCallAnsweringDataFactory;
			}
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x060008D6 RID: 2262 RVA: 0x00021468 File Offset: 0x0001F668
		internal static LatencyDetectionContextFactory GetAutoAttendantsFromStoreFactory
		{
			get
			{
				if (PAAUtils.getAutoAttendantsFactory == null)
				{
					lock (PAAUtils.syncObj)
					{
						if (PAAUtils.getAutoAttendantsFactory == null)
						{
							PAAUtils.getAutoAttendantsFactory = PAAUtils.CreateLatencyDetectionFactory("PAA.GetAutoAttendants");
						}
					}
				}
				return PAAUtils.getAutoAttendantsFactory;
			}
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x060008D7 RID: 2263 RVA: 0x000214C4 File Offset: 0x0001F6C4
		internal static LatencyDetectionContextFactory PAAEvaluationFactory
		{
			get
			{
				if (PAAUtils.paaEvaluationFactory == null)
				{
					lock (PAAUtils.syncObj)
					{
						if (PAAUtils.paaEvaluationFactory == null)
						{
							PAAUtils.paaEvaluationFactory = PAAUtils.CreateLatencyDetectionFactory("PAA.Evaluate");
						}
					}
				}
				return PAAUtils.paaEvaluationFactory;
			}
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x060008D8 RID: 2264 RVA: 0x00021520 File Offset: 0x0001F720
		internal static LatencyDetectionContextFactory BuildContactCacheFactory
		{
			get
			{
				if (PAAUtils.buildContactCacheFactory == null)
				{
					lock (PAAUtils.syncObj)
					{
						if (PAAUtils.buildContactCacheFactory == null)
						{
							PAAUtils.buildContactCacheFactory = PAAUtils.CreateLatencyDetectionFactory("PAA.PersonalContactCache.BuildCache");
						}
					}
				}
				return PAAUtils.buildContactCacheFactory;
			}
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x060008D9 RID: 2265 RVA: 0x0002157C File Offset: 0x0001F77C
		internal static LatencyDetectionContextFactory ResolvePersonalContactsFactory
		{
			get
			{
				if (PAAUtils.resolvePersonalContactsFactory == null)
				{
					lock (PAAUtils.syncObj)
					{
						if (PAAUtils.resolvePersonalContactsFactory == null)
						{
							PAAUtils.resolvePersonalContactsFactory = PAAUtils.CreateLatencyDetectionFactory("PAA.UserDataLoader.ResolvePersonalContacts");
						}
					}
				}
				return PAAUtils.resolvePersonalContactsFactory;
			}
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x060008DA RID: 2266 RVA: 0x000215D8 File Offset: 0x0001F7D8
		internal static LatencyDetectionContextFactory GetFreeBusyInfoFactory
		{
			get
			{
				if (PAAUtils.getFreeBusyInfoFactory == null)
				{
					lock (PAAUtils.syncObj)
					{
						if (PAAUtils.getFreeBusyInfoFactory == null)
						{
							PAAUtils.getFreeBusyInfoFactory = PAAUtils.CreateLatencyDetectionFactory("PAA.UserDataLoader.GetFreeBusyInformation");
						}
					}
				}
				return PAAUtils.getFreeBusyInfoFactory;
			}
		}

		// Token: 0x060008DB RID: 2267 RVA: 0x00021634 File Offset: 0x0001F834
		internal static bool IsCompatible(Version version)
		{
			bool flag = version.Major == PAAConstants.CurrentVersion.Major && version.Minor == PAAConstants.CurrentVersion.Minor && version.Build == PAAConstants.CurrentVersion.Build && version.Revision == PAAConstants.CurrentVersion.Revision;
			CallIdTracer.TraceError(ExTraceGlobals.PersonalAutoAttendantTracer, null, "PAAUtils::IsCompatible(Version: {0} CurrentVersion: {1}) returning {2}", new object[]
			{
				version.ToString(),
				PAAConstants.CurrentVersion.ToString(),
				flag
			});
			return flag;
		}

		// Token: 0x060008DC RID: 2268 RVA: 0x000216C5 File Offset: 0x0001F8C5
		private static LatencyDetectionContextFactory CreateLatencyDetectionFactory(string locationIdentity)
		{
			return LatencyDetectionContextFactory.CreateFactory(locationIdentity);
		}

		// Token: 0x040004F1 RID: 1265
		private static object syncObj = new object();

		// Token: 0x040004F2 RID: 1266
		private static LatencyDetectionContextFactory getAutoAttendantsFactory = null;

		// Token: 0x040004F3 RID: 1267
		private static LatencyDetectionContextFactory paaEvaluationFactory = null;

		// Token: 0x040004F4 RID: 1268
		private static LatencyDetectionContextFactory buildContactCacheFactory = null;

		// Token: 0x040004F5 RID: 1269
		private static LatencyDetectionContextFactory resolvePersonalContactsFactory = null;

		// Token: 0x040004F6 RID: 1270
		private static LatencyDetectionContextFactory getFreeBusyInfoFactory = null;

		// Token: 0x040004F7 RID: 1271
		private static LatencyDetectionContextFactory getCallAnsweringDataFactory = null;
	}
}
