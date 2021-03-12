using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000E0B RID: 3595
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class FolderHierarchySyncStateInfo : SyncStateInfo
	{
		// Token: 0x17002128 RID: 8488
		// (get) Token: 0x06007BFD RID: 31741 RVA: 0x002238A7 File Offset: 0x00221AA7
		public override int Version
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x17002129 RID: 8489
		// (get) Token: 0x06007BFE RID: 31742 RVA: 0x002238AA File Offset: 0x00221AAA
		// (set) Token: 0x06007BFF RID: 31743 RVA: 0x002238B1 File Offset: 0x00221AB1
		public override string UniqueName
		{
			get
			{
				return "Root";
			}
			set
			{
				throw new InvalidOperationException("FolderHierarchySyncStateInfo.UniqueName is not settable.");
			}
		}

		// Token: 0x040054FC RID: 21756
		public const string UniqueNameString = "Root";
	}
}
