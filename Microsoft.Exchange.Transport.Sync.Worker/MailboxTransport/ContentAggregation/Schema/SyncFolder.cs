using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.MailboxTransport.ContentAggregation.Schema
{
	// Token: 0x02000218 RID: 536
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class SyncFolder : DisposeTrackableBase, ISyncObject, IDisposeTrackable, IDisposable
	{
		// Token: 0x06001324 RID: 4900 RVA: 0x0004125E File Offset: 0x0003F45E
		internal SyncFolder(string displayName) : this(displayName, DefaultFolderType.None)
		{
		}

		// Token: 0x06001325 RID: 4901 RVA: 0x00041268 File Offset: 0x0003F468
		internal SyncFolder(string displayName, DefaultFolderType defaultFolderType) : this(displayName, defaultFolderType, null)
		{
		}

		// Token: 0x06001326 RID: 4902 RVA: 0x00041286 File Offset: 0x0003F486
		internal SyncFolder(string displayName, DefaultFolderType defaultFolderType, ExDateTime? lastModifiedTime)
		{
			if (displayName == null)
			{
				throw new ArgumentNullException("displayName");
			}
			this.displayName = displayName;
			this.defaultFolderType = defaultFolderType;
			this.lastModifiedTime = lastModifiedTime;
		}

		// Token: 0x17000695 RID: 1685
		// (get) Token: 0x06001327 RID: 4903 RVA: 0x000412B1 File Offset: 0x0003F4B1
		public SchemaType Type
		{
			get
			{
				base.CheckDisposed();
				return SchemaType.Folder;
			}
		}

		// Token: 0x17000696 RID: 1686
		// (get) Token: 0x06001328 RID: 4904 RVA: 0x000412BA File Offset: 0x0003F4BA
		public string DisplayName
		{
			get
			{
				base.CheckDisposed();
				return this.displayName;
			}
		}

		// Token: 0x17000697 RID: 1687
		// (get) Token: 0x06001329 RID: 4905 RVA: 0x000412C8 File Offset: 0x0003F4C8
		public DefaultFolderType DefaultFolderType
		{
			get
			{
				base.CheckDisposed();
				return this.defaultFolderType;
			}
		}

		// Token: 0x17000698 RID: 1688
		// (get) Token: 0x0600132A RID: 4906 RVA: 0x000412D6 File Offset: 0x0003F4D6
		public ExDateTime? LastModifiedTime
		{
			get
			{
				base.CheckDisposed();
				return this.lastModifiedTime;
			}
		}

		// Token: 0x0600132B RID: 4907 RVA: 0x000412E4 File Offset: 0x0003F4E4
		protected override void InternalDispose(bool disposing)
		{
		}

		// Token: 0x0600132C RID: 4908 RVA: 0x000412E6 File Offset: 0x0003F4E6
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<SyncFolder>(this);
		}

		// Token: 0x04000A1B RID: 2587
		private readonly ExDateTime? lastModifiedTime;

		// Token: 0x04000A1C RID: 2588
		private readonly string displayName;

		// Token: 0x04000A1D RID: 2589
		private readonly DefaultFolderType defaultFolderType;
	}
}
