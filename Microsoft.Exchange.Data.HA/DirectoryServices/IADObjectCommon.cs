using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.HA.DirectoryServices
{
	// Token: 0x02000008 RID: 8
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IADObjectCommon
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000039 RID: 57
		ADObjectId Id { get; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600003A RID: 58
		ObjectId Identity { get; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600003B RID: 59
		Guid Guid { get; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600003C RID: 60
		string Name { get; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600003D RID: 61
		bool IsValid { get; }

		// Token: 0x0600003E RID: 62
		void Minimize();
	}
}
