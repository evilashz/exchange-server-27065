using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.RpcClientAccess.Parser;

namespace Microsoft.Exchange.RpcClientAccess.Handler
{
	// Token: 0x0200009E RID: 158
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class PermissionsView : FolderBasedView
	{
		// Token: 0x06000666 RID: 1638 RVA: 0x0002A614 File Offset: 0x00028814
		internal PermissionsView(Logon logon, ReferenceCount<CoreFolder> coreFolderReference, TableFlags tableFlags, NotificationHandler notificationHandler, ServerObjectHandle returnNotificationHandle) : base(logon, coreFolderReference, tableFlags, View.Capabilities.Basic, ViewType.None, notificationHandler, returnNotificationHandle, null)
		{
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x0002A634 File Offset: 0x00028834
		protected override IQueryResult CreateQueryResult(NativeStorePropertyDefinition[] propertyDefinitions)
		{
			ModifyTableOptions modifyTableOptions = ModifyTableOptions.None;
			if ((byte)(base.TableFlags & TableFlags.Associated) == 2)
			{
				modifyTableOptions |= ModifyTableOptions.FreeBusyAware;
			}
			IQueryResult queryResult;
			using (IModifyTable permissionTable = base.CoreFolder.GetPermissionTable(modifyTableOptions))
			{
				queryResult = permissionTable.GetQueryResult(base.Filter, propertyDefinitions);
			}
			return queryResult;
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000668 RID: 1640 RVA: 0x0002A68C File Offset: 0x0002888C
		protected override StoreId? ContainerFolderId
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000669 RID: 1641 RVA: 0x0002A6A2 File Offset: 0x000288A2
		protected override PropertyConverter PropertyConverter
		{
			get
			{
				return PropertyConverter.Permission;
			}
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x0002A6A9 File Offset: 0x000288A9
		protected override void CheckPropertiesAllowed(PropertyTag[] propertyTags)
		{
		}
	}
}
