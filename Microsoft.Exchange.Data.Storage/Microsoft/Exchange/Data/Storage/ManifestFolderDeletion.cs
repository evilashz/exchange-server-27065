using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200083E RID: 2110
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ManifestFolderDeletion : ManifestChangeBase
	{
		// Token: 0x06004E64 RID: 20068 RVA: 0x001487CD File Offset: 0x001469CD
		internal ManifestFolderDeletion(byte[] idSetDeleted)
		{
			this.idSetDeleted = idSetDeleted;
		}

		// Token: 0x1700162F RID: 5679
		// (get) Token: 0x06004E65 RID: 20069 RVA: 0x001487DC File Offset: 0x001469DC
		public byte[] IdsetDeleted
		{
			get
			{
				return this.idSetDeleted;
			}
		}

		// Token: 0x04002AC4 RID: 10948
		private readonly byte[] idSetDeleted;
	}
}
