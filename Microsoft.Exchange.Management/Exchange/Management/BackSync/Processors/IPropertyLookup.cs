using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Management.BackSync.Processors
{
	// Token: 0x020000AD RID: 173
	internal interface IPropertyLookup
	{
		// Token: 0x060005B6 RID: 1462
		ADRawEntry GetProperties(ADObjectId id);
	}
}
