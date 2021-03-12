using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x02000298 RID: 664
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IExModifyTable : IExInterface, IDisposeTrackable, IDisposable
	{
		// Token: 0x06000C50 RID: 3152
		int GetTable(int ulFlags, out IExMapiTable iMAPITable);

		// Token: 0x06000C51 RID: 3153
		int ModifyTable(int ulFlags, ICollection<RowEntry> lpRowList);
	}
}
