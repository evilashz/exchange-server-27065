using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000287 RID: 647
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IUserConfigurationManager
	{
		// Token: 0x17000863 RID: 2147
		// (get) Token: 0x06001ACF RID: 6863
		IMailboxSession MailboxSession { get; }

		// Token: 0x06001AD0 RID: 6864
		IReadableUserConfiguration GetReadOnlyMailboxConfiguration(string configName, UserConfigurationTypes freefetchDataTypes);

		// Token: 0x06001AD1 RID: 6865
		IReadableUserConfiguration GetReadOnlyFolderConfiguration(string configName, UserConfigurationTypes freefetchDataTypes, StoreId folderId);

		// Token: 0x06001AD2 RID: 6866
		IUserConfiguration GetMailboxConfiguration(string configName, UserConfigurationTypes freefetchDataTypes);

		// Token: 0x06001AD3 RID: 6867
		IUserConfiguration GetFolderConfiguration(string configName, UserConfigurationTypes freefetchDataTypes, StoreId folderId);

		// Token: 0x06001AD4 RID: 6868
		OperationResult DeleteMailboxConfigurations(params string[] configurationNames);

		// Token: 0x06001AD5 RID: 6869
		OperationResult DeleteFolderConfigurations(StoreId folderId, params string[] configurationNames);

		// Token: 0x06001AD6 RID: 6870
		IUserConfiguration CreateMailboxConfiguration(string configurationName, UserConfigurationTypes dataTypes);

		// Token: 0x06001AD7 RID: 6871
		IUserConfiguration CreateFolderConfiguration(string configurationName, UserConfigurationTypes dataTypes, StoreId folderId);

		// Token: 0x06001AD8 RID: 6872
		IList<IStorePropertyBag> FetchAllConfigurations(IFolder folder, SortBy[] sorts, int maxRow, params PropertyDefinition[] columns);
	}
}
