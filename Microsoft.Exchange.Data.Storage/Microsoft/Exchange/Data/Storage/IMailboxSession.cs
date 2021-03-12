using System;
using System.Globalization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200000B RID: 11
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IMailboxSession : IStoreSession, IDisposable
	{
		// Token: 0x06000133 RID: 307
		DefaultFolderType IsDefaultFolderType(StoreId folderId);

		// Token: 0x06000134 RID: 308
		StoreObjectId GetDefaultFolderId(DefaultFolderType defaultFolderType);

		// Token: 0x06000135 RID: 309
		StoreObjectId CreateDefaultFolder(DefaultFolderType defaultFolderType);

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000136 RID: 310
		string ClientInfoString { get; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000137 RID: 311
		CultureInfo PreferedCulture { get; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000138 RID: 312
		IUserConfigurationManager UserConfigurationManager { get; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000139 RID: 313
		ContactFolders ContactFolders { get; }

		// Token: 0x0600013A RID: 314
		bool IsMailboxOof();

		// Token: 0x0600013B RID: 315
		bool IsGroupMailbox();

		// Token: 0x0600013C RID: 316
		void DeleteDefaultFolder(DefaultFolderType defaultFolderType, DeleteItemFlags deleteItemFlags);

		// Token: 0x0600013D RID: 317
		CumulativeRPCPerformanceStatistics GetStoreCumulativeRPCStats();

		// Token: 0x0600013E RID: 318
		bool TryFixDefaultFolderId(DefaultFolderType defaultFolderType, out StoreObjectId id);
	}
}
