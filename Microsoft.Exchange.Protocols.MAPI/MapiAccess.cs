using System;

namespace Microsoft.Exchange.Protocols.MAPI
{
	// Token: 0x02000019 RID: 25
	[Flags]
	public enum MapiAccess
	{
		// Token: 0x040000DA RID: 218
		None = 0,
		// Token: 0x040000DB RID: 219
		Modify = 1,
		// Token: 0x040000DC RID: 220
		Read = 2,
		// Token: 0x040000DD RID: 221
		Delete = 4,
		// Token: 0x040000DE RID: 222
		CreateHierarchy = 8,
		// Token: 0x040000DF RID: 223
		CreateContent = 16,
		// Token: 0x040000E0 RID: 224
		CreateAssociated = 32
	}
}
