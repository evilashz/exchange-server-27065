using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.RpcClientAccess.Parser;

namespace Microsoft.Exchange.RpcClientAccess.Handler
{
	// Token: 0x02000099 RID: 153
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class AttachmentView : View
	{
		// Token: 0x06000632 RID: 1586 RVA: 0x00029FAD File Offset: 0x000281AD
		internal AttachmentView(Logon logon, ReferenceCount<CoreItem> coreItemReference, TableFlags tableFlags, NotificationHandler notificationHandler, ServerObjectHandle returnNotificationHandle) : base(logon, tableFlags, View.Capabilities.Basic, ViewType.MessageView, notificationHandler, returnNotificationHandle, null)
		{
			this.coreItemReference = coreItemReference;
			this.coreItemReference.AddRef();
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000633 RID: 1587 RVA: 0x00029FD1 File Offset: 0x000281D1
		protected override ICoreObject CoreObject
		{
			get
			{
				return this.coreItemReference.ReferencedObject;
			}
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x00029FE0 File Offset: 0x000281E0
		protected override IViewDataSource CreateDataSource()
		{
			IViewDataSource result;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				QueryResult queryResult = AttachmentTable.GetQueryResult(this.coreItemReference.ReferencedObject, this.ColumnPropertyDefinitions);
				disposeGuard.Add<QueryResult>(queryResult);
				bool useUnicodeForRestrictions = (byte)(base.TableFlags & TableFlags.MapiUnicode) != 0;
				QueryResultViewDataSource queryResultViewDataSource = new QueryResultViewDataSource(this.coreItemReference.ReferencedObject.Session, base.ServerColumns, queryResult, useUnicodeForRestrictions);
				disposeGuard.Success();
				result = queryResultViewDataSource;
			}
			return result;
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000635 RID: 1589 RVA: 0x0002A070 File Offset: 0x00028270
		protected override NativeStorePropertyDefinition[] ColumnPropertyDefinitions
		{
			get
			{
				return base.GetColumnPropertyDefinitions(this.coreItemReference.ReferencedObject.Session, this.coreItemReference.ReferencedObject.PropertyBag);
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000636 RID: 1590 RVA: 0x0002A098 File Offset: 0x00028298
		protected override StoreId? ContainerFolderId
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000637 RID: 1591 RVA: 0x0002A0AE File Offset: 0x000282AE
		protected override PropertyConverter PropertyConverter
		{
			get
			{
				return PropertyConverter.Attachment;
			}
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x0002A0B5 File Offset: 0x000282B5
		protected override void CheckPropertiesAllowed(PropertyTag[] propertyTags)
		{
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000639 RID: 1593 RVA: 0x0002A0B7 File Offset: 0x000282B7
		protected override ClientSideProperties ClientSideProperties
		{
			get
			{
				return ClientSideProperties.AttachmentInstance;
			}
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x0002A0BE File Offset: 0x000282BE
		protected override void InternalDispose()
		{
			this.coreItemReference.Release();
			base.InternalDispose();
		}

		// Token: 0x0400029D RID: 669
		private readonly ReferenceCount<CoreItem> coreItemReference;
	}
}
