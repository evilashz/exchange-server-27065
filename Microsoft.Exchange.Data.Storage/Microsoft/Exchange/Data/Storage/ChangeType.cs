using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000224 RID: 548
	internal enum ChangeType
	{
		// Token: 0x04001011 RID: 4113
		None,
		// Token: 0x04001012 RID: 4114
		Add,
		// Token: 0x04001013 RID: 4115
		Change,
		// Token: 0x04001014 RID: 4116
		Delete = 4,
		// Token: 0x04001015 RID: 4117
		ReadFlagChange,
		// Token: 0x04001016 RID: 4118
		SoftDelete,
		// Token: 0x04001017 RID: 4119
		OutOfFilter,
		// Token: 0x04001018 RID: 4120
		AssociatedAdd,
		// Token: 0x04001019 RID: 4121
		Send
	}
}
