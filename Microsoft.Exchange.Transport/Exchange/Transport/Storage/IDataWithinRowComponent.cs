using System;

namespace Microsoft.Exchange.Transport.Storage
{
	// Token: 0x020000B5 RID: 181
	internal interface IDataWithinRowComponent : IDataObjectComponent
	{
		// Token: 0x06000626 RID: 1574
		void LoadFromParentRow(DataTableCursor cursor);

		// Token: 0x06000627 RID: 1575
		void SaveToParentRow(DataTableCursor cursor, Func<bool> checkpointCallback);

		// Token: 0x06000628 RID: 1576
		void Cleanup();
	}
}
