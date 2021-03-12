using System;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccess
{
	// Token: 0x02000006 RID: 6
	public interface IColumn
	{
		// Token: 0x06000028 RID: 40
		int GetSize(ITWIR context);

		// Token: 0x06000029 RID: 41
		object GetValue(ITWIR context);
	}
}
