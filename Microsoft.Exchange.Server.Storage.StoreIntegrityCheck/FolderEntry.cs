using System;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.StoreIntegrityCheck
{
	// Token: 0x0200000B RID: 11
	public sealed class FolderEntry
	{
		// Token: 0x06000030 RID: 48 RVA: 0x000042E5 File Offset: 0x000024E5
		public FolderEntry(ExchangeId folderId, short specialFolderNumber, string displayName)
		{
			this.folderId = folderId;
			this.specialFolderNumber = specialFolderNumber;
			this.displayName = displayName;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00004302 File Offset: 0x00002502
		public ExchangeId FolderId
		{
			get
			{
				return this.folderId;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000032 RID: 50 RVA: 0x0000430A File Offset: 0x0000250A
		public short SpecialFolderNumber
		{
			get
			{
				return this.specialFolderNumber;
			}
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00004312 File Offset: 0x00002512
		public bool NameStartsWith(string prefix)
		{
			return !string.IsNullOrEmpty(this.displayName) && this.displayName.StartsWith(prefix);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x0000432F File Offset: 0x0000252F
		public override string ToString()
		{
			return string.Format("{0}({1})", this.FolderId, this.SpecialFolderNumber);
		}

		// Token: 0x04000010 RID: 16
		private readonly ExchangeId folderId;

		// Token: 0x04000011 RID: 17
		private readonly short specialFolderNumber;

		// Token: 0x04000012 RID: 18
		private readonly string displayName;
	}
}
