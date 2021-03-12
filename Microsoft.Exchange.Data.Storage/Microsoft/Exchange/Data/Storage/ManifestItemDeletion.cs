using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000840 RID: 2112
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ManifestItemDeletion : ManifestChangeBase
	{
		// Token: 0x06004E69 RID: 20073 RVA: 0x00148829 File Offset: 0x00146A29
		internal ManifestItemDeletion(byte[] idsetDeleted, bool isSoftDeleted, bool isExpired)
		{
			this.idsetDeleted = idsetDeleted;
			this.isSoftDeleted = isSoftDeleted;
			this.isExpired = isExpired;
		}

		// Token: 0x17001632 RID: 5682
		// (get) Token: 0x06004E6A RID: 20074 RVA: 0x00148846 File Offset: 0x00146A46
		public byte[] IdsetDeleted
		{
			get
			{
				return this.idsetDeleted;
			}
		}

		// Token: 0x17001633 RID: 5683
		// (get) Token: 0x06004E6B RID: 20075 RVA: 0x0014884E File Offset: 0x00146A4E
		public bool IsExpired
		{
			get
			{
				return this.isExpired;
			}
		}

		// Token: 0x17001634 RID: 5684
		// (get) Token: 0x06004E6C RID: 20076 RVA: 0x00148856 File Offset: 0x00146A56
		public bool IsSoftDeleted
		{
			get
			{
				return this.isSoftDeleted;
			}
		}

		// Token: 0x04002AC7 RID: 10951
		private readonly byte[] idsetDeleted;

		// Token: 0x04002AC8 RID: 10952
		private readonly bool isSoftDeleted;

		// Token: 0x04002AC9 RID: 10953
		private readonly bool isExpired;
	}
}
