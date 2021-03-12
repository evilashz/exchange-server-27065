using System;
using System.Text;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Services;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x020003D8 RID: 984
	internal sealed class ServicesFolderHierarchySyncState : ServicesSyncStateBase, IFolderHierarchySyncState, ISyncState
	{
		// Token: 0x06001B90 RID: 7056 RVA: 0x0009CA2F File Offset: 0x0009AC2F
		public ServicesFolderHierarchySyncState(MailboxSession session, StoreObjectId folderId, ISyncProvider syncProvider, string base64SyncData) : base(folderId, syncProvider)
		{
			ExTraceGlobals.SynchronizationTracer.TraceDebug((long)this.GetHashCode(), "ServicesFolderHierarchySyncState constructor called");
			this.session = session;
			base.Version = 1;
			base.Load(base64SyncData);
		}

		// Token: 0x06001B91 RID: 7057 RVA: 0x0009CA65 File Offset: 0x0009AC65
		public FolderHierarchySync GetFolderHierarchySync()
		{
			return this.GetFolderHierarchySync(new ChangeTrackingDelegate(ServicesFolderHierarchySyncState.ComputeChangeTrackingHash));
		}

		// Token: 0x06001B92 RID: 7058 RVA: 0x0009CA79 File Offset: 0x0009AC79
		public FolderHierarchySync GetFolderHierarchySync(ChangeTrackingDelegate changeTrackingDelegate)
		{
			return new FolderHierarchySync(this.session, this, changeTrackingDelegate);
		}

		// Token: 0x06001B93 RID: 7059 RVA: 0x0009CA88 File Offset: 0x0009AC88
		private static int ComputeChangeTrackingHash(MailboxSession session, StoreObjectId folderId, IStorePropertyBag propertyBag)
		{
			if (propertyBag != null)
			{
				StringBuilder stringBuilder = new StringBuilder(128);
				string text;
				stringBuilder.Append(FolderHierarchySync.TryGetPropertyFromBag<string>(propertyBag, FolderSchema.DisplayName, null, out text) ? text : string.Empty);
				StoreObjectId storeObjectId;
				stringBuilder.Append(FolderHierarchySync.TryGetPropertyFromBag<StoreObjectId>(propertyBag, StoreObjectSchema.ParentItemId, null, out storeObjectId) ? storeObjectId.ToString() : string.Empty);
				string text2;
				stringBuilder.Append(FolderHierarchySync.TryGetPropertyFromBag<string>(propertyBag, StoreObjectSchema.ContainerClass, null, out text2) ? text2 : string.Empty);
				return stringBuilder.ToString().GetHashCode();
			}
			int hashCode;
			using (Folder folder = Folder.Bind(session, folderId, null))
			{
				string text3 = folder.DisplayName + folder.ParentId + folder.ClassName;
				hashCode = text3.GetHashCode();
			}
			return hashCode;
		}

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06001B94 RID: 7060 RVA: 0x0009CB60 File Offset: 0x0009AD60
		internal override StringData SyncStateTag
		{
			get
			{
				return ServicesFolderHierarchySyncState.FolderHierarchySyncTagValue;
			}
		}

		// Token: 0x0400121E RID: 4638
		private const int CurrentFolderHierarchySyncStateVersion = 1;

		// Token: 0x0400121F RID: 4639
		internal static StringData FolderHierarchySyncTagValue = new StringData("WS.FolderHierarchySync");

		// Token: 0x04001220 RID: 4640
		private MailboxSession session;
	}
}
