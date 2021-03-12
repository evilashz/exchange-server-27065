using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.RpcClientAccess.Parser;

namespace Microsoft.Exchange.RpcClientAccess.Handler
{
	// Token: 0x0200004E RID: 78
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IPrimaryHierarchyHandler
	{
		// Token: 0x060002F7 RID: 759
		StoreId CreateFolder(string folderName, string description, StoreId parentFolderId, CreateFolderFlags flags, out Guid contentMailboxGuid);

		// Token: 0x060002F8 RID: 760
		void DeleteFolder(StoreId parentFolderId, StoreId folderId, DeleteFolderFlags flags);

		// Token: 0x060002F9 RID: 761
		void MoveFolder(StoreId parentFolderId, StoreId destinationFolderId, StoreId sourceFolderId, string folderName);

		// Token: 0x060002FA RID: 762
		PropertyProblem[] SetProperties(StoreId folderId, PropertyValue[] propertyValues, out Guid contentMailboxGuid);

		// Token: 0x060002FB RID: 763
		PropertyProblem[] DeleteProperties(StoreId folderId, PropertyTag[] propertyTags, out Guid contentMailboxGuid);

		// Token: 0x060002FC RID: 764
		void ModifyPermissions(CoreFolder coreFolder, IModifyTable permissionsTable, IEnumerable<ModifyTableRow> modifyTableRows, ModifyTableOptions options, bool shouldReplaceAllRows);
	}
}
