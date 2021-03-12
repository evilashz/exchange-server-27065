using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.ResourceHealth
{
	// Token: 0x02000B21 RID: 2849
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class DatabaseInformation : IDatabaseInformation
	{
		// Token: 0x0600674E RID: 26446 RVA: 0x001B4E70 File Offset: 0x001B3070
		public DatabaseInformation(Guid databaseGuid, string databaseName, string databaseVolumeName)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("databaseName", databaseName);
			ArgumentValidator.ThrowIfNullOrEmpty("databaseVolumeName", databaseVolumeName);
			this.DatabaseGuid = databaseGuid;
			this.DatabaseName = databaseName;
			this.DatabaseVolumeName = databaseVolumeName;
		}

		// Token: 0x17001C69 RID: 7273
		// (get) Token: 0x0600674F RID: 26447 RVA: 0x001B4EA3 File Offset: 0x001B30A3
		// (set) Token: 0x06006750 RID: 26448 RVA: 0x001B4EAB File Offset: 0x001B30AB
		public Guid DatabaseGuid { get; private set; }

		// Token: 0x17001C6A RID: 7274
		// (get) Token: 0x06006751 RID: 26449 RVA: 0x001B4EB4 File Offset: 0x001B30B4
		// (set) Token: 0x06006752 RID: 26450 RVA: 0x001B4EBC File Offset: 0x001B30BC
		public string DatabaseName { get; private set; }

		// Token: 0x17001C6B RID: 7275
		// (get) Token: 0x06006753 RID: 26451 RVA: 0x001B4EC5 File Offset: 0x001B30C5
		// (set) Token: 0x06006754 RID: 26452 RVA: 0x001B4ECD File Offset: 0x001B30CD
		public string DatabaseVolumeName { get; private set; }
	}
}
