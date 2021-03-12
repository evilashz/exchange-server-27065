using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020000A7 RID: 167
	public interface ITableCentricConfigurable
	{
		// Token: 0x06000544 RID: 1348
		List<ReaderTaskProfile> BuildReaderTaskProfile();

		// Token: 0x06000545 RID: 1349
		List<SaverTaskProfile> BuildSaverTaskProfile();

		// Token: 0x06000546 RID: 1350
		List<DataObjectProfile> BuildDataObjectProfile();

		// Token: 0x06000547 RID: 1351
		List<ColumnProfile> BuildColumnProfile();

		// Token: 0x06000548 RID: 1352
		Dictionary<string, List<string>> BuildPageToDataObjectsMapping();

		// Token: 0x06000549 RID: 1353
		bool CanEnableUICustomization();
	}
}
