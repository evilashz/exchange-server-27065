using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x02000296 RID: 662
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IExMapiContainer : IExMapiProp, IExInterface, IDisposeTrackable, IDisposable
	{
		// Token: 0x06000C30 RID: 3120
		int GetContentsTable(int ulFlags, out IExMapiTable iMAPITable);

		// Token: 0x06000C31 RID: 3121
		int GetHierarchyTable(int ulFlags, out IExMapiTable iMAPITable);

		// Token: 0x06000C32 RID: 3122
		int OpenEntry(byte[] lpEntryID, Guid lpInterface, int ulFlags, out int lpulObjType, out IExInterface iObj);

		// Token: 0x06000C33 RID: 3123
		int SetSearchCriteria(Restriction lpRestriction, byte[][] lpContainerList, int ulSearchFlags);

		// Token: 0x06000C34 RID: 3124
		int GetSearchCriteria(int ulFlags, out Restriction lpRestriction, out byte[][] lpContainerList, out int ulSearchState);
	}
}
