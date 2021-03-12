using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000283 RID: 643
	internal enum FolderChangeOperation
	{
		// Token: 0x040012CE RID: 4814
		Copy,
		// Token: 0x040012CF RID: 4815
		Move,
		// Token: 0x040012D0 RID: 4816
		MoveToDeletedItems,
		// Token: 0x040012D1 RID: 4817
		SoftDelete,
		// Token: 0x040012D2 RID: 4818
		HardDelete,
		// Token: 0x040012D3 RID: 4819
		DoneWithMessageDelete,
		// Token: 0x040012D4 RID: 4820
		Create,
		// Token: 0x040012D5 RID: 4821
		Update,
		// Token: 0x040012D6 RID: 4822
		Empty
	}
}
