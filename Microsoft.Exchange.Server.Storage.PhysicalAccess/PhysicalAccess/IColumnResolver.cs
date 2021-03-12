using System;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccess
{
	// Token: 0x02000043 RID: 67
	internal interface IColumnResolver
	{
		// Token: 0x06000320 RID: 800
		Column Resolve(Column column);
	}
}
