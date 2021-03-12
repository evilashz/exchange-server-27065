using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.RpcClientAccess.Parser;

namespace Microsoft.Exchange.RpcClientAccess.Handler
{
	// Token: 0x020000A0 RID: 160
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class RulesView : FolderBasedView
	{
		// Token: 0x06000682 RID: 1666 RVA: 0x0002A9F0 File Offset: 0x00028BF0
		internal RulesView(Logon logon, ReferenceCount<CoreFolder> coreFolderReference, TableFlags tableFlags, NotificationHandler notificationHandler, ServerObjectHandle returnNotificationHandle) : base(logon, coreFolderReference, tableFlags, View.Capabilities.CanRestrict, ViewType.None, notificationHandler, returnNotificationHandle, null)
		{
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x0002AA10 File Offset: 0x00028C10
		protected override IQueryResult CreateQueryResult(NativeStorePropertyDefinition[] propertyDefinitions)
		{
			IQueryResult queryResult;
			using (IModifyTable ruleTable = base.CoreFolder.GetRuleTable())
			{
				queryResult = ruleTable.GetQueryResult(base.Filter, propertyDefinitions);
			}
			return queryResult;
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000684 RID: 1668 RVA: 0x0002AA54 File Offset: 0x00028C54
		protected override StoreId? ContainerFolderId
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000685 RID: 1669 RVA: 0x0002AA6A File Offset: 0x00028C6A
		protected override PropertyConverter PropertyConverter
		{
			get
			{
				return PropertyConverter.Rule;
			}
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x0002AA71 File Offset: 0x00028C71
		protected override void CheckPropertiesAllowed(PropertyTag[] propertyTags)
		{
		}
	}
}
