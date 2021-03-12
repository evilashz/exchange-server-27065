using System;

namespace System.Globalization
{
	// Token: 0x02000373 RID: 883
	internal sealed class AppDomainSortingSetupInfo
	{
		// Token: 0x06002C7C RID: 11388 RVA: 0x000A9FF1 File Offset: 0x000A81F1
		internal AppDomainSortingSetupInfo()
		{
		}

		// Token: 0x06002C7D RID: 11389 RVA: 0x000A9FFC File Offset: 0x000A81FC
		internal AppDomainSortingSetupInfo(AppDomainSortingSetupInfo copy)
		{
			this._useV2LegacySorting = copy._useV2LegacySorting;
			this._useV4LegacySorting = copy._useV4LegacySorting;
			this._pfnIsNLSDefinedString = copy._pfnIsNLSDefinedString;
			this._pfnCompareStringEx = copy._pfnCompareStringEx;
			this._pfnLCMapStringEx = copy._pfnLCMapStringEx;
			this._pfnFindNLSStringEx = copy._pfnFindNLSStringEx;
			this._pfnFindStringOrdinal = copy._pfnFindStringOrdinal;
			this._pfnCompareStringOrdinal = copy._pfnCompareStringOrdinal;
			this._pfnGetNLSVersionEx = copy._pfnGetNLSVersionEx;
		}

		// Token: 0x040011DB RID: 4571
		internal IntPtr _pfnIsNLSDefinedString;

		// Token: 0x040011DC RID: 4572
		internal IntPtr _pfnCompareStringEx;

		// Token: 0x040011DD RID: 4573
		internal IntPtr _pfnLCMapStringEx;

		// Token: 0x040011DE RID: 4574
		internal IntPtr _pfnFindNLSStringEx;

		// Token: 0x040011DF RID: 4575
		internal IntPtr _pfnCompareStringOrdinal;

		// Token: 0x040011E0 RID: 4576
		internal IntPtr _pfnGetNLSVersionEx;

		// Token: 0x040011E1 RID: 4577
		internal IntPtr _pfnFindStringOrdinal;

		// Token: 0x040011E2 RID: 4578
		internal bool _useV2LegacySorting;

		// Token: 0x040011E3 RID: 4579
		internal bool _useV4LegacySorting;
	}
}
