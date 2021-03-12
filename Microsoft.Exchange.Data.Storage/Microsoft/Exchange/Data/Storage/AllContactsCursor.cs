using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000478 RID: 1144
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class AllContactsCursor : IDisposeTrackable, IDisposable
	{
		// Token: 0x0600330F RID: 13071 RVA: 0x000CF790 File Offset: 0x000CD990
		public AllContactsCursor(MailboxSession session, PropertyDefinition[] properties, SortBy[] sortByProperties)
		{
			using (DisposeGuard disposeGuard = this.Guard())
			{
				Util.ThrowOnNullArgument(session, "session");
				Util.ThrowOnNullArgument(properties, "properties");
				StorageGlobals.TraceConstructIDisposable(this);
				this.disposeTracker = this.GetDisposeTracker();
				this.session = session;
				this.properties = PropertyDefinitionCollection.Merge<PropertyDefinition>(AllContactsCursor.requiredProperties, properties);
				this.sortByProperties = sortByProperties;
				this.PrepareQuery();
				disposeGuard.Success();
			}
		}

		// Token: 0x1700100B RID: 4107
		// (get) Token: 0x06003310 RID: 13072 RVA: 0x000CF828 File Offset: 0x000CDA28
		public IStorePropertyBag Current
		{
			get
			{
				this.CheckDisposed("get_Current");
				if (this.rows == null || this.currentRow < 0)
				{
					return null;
				}
				return this.rows[this.currentRow];
			}
		}

		// Token: 0x1700100C RID: 4108
		// (get) Token: 0x06003311 RID: 13073 RVA: 0x000CF855 File Offset: 0x000CDA55
		public int EstimatedRowCount
		{
			get
			{
				this.CheckDisposed("get_EstimatedRowCount");
				return this.query.EstimatedRowCount;
			}
		}

		// Token: 0x06003312 RID: 13074 RVA: 0x000CF870 File Offset: 0x000CDA70
		public void MoveNext()
		{
			this.CheckDisposed("MoveNext");
			for (;;)
			{
				this.currentRow++;
				if (this.rows == null || this.currentRow >= this.rows.Length)
				{
					this.currentRow = 0;
					this.rows = this.query.GetPropertyBags(this.chunkSize);
					if (this.rows.Length == 0)
					{
						break;
					}
					this.chunkSize = Math.Min(this.chunkSize * 2, 1000);
				}
				IStorePropertyBag storePropertyBag = this.Current;
				object obj = storePropertyBag.TryGetProperty(StoreObjectSchema.ItemClass);
				if (storePropertyBag != null && !(storePropertyBag.TryGetProperty(ItemSchema.Id) is PropertyError) && !(obj is PropertyError) && (ObjectClass.IsContact((string)obj) || ObjectClass.IsDistributionList((string)obj)))
				{
					return;
				}
				AllContactsCursor.Tracer.TraceDebug(0L, "AllContactsCursor.MoveNext: Skipping bogus contact");
			}
			this.rows = null;
		}

		// Token: 0x06003313 RID: 13075 RVA: 0x000CF956 File Offset: 0x000CDB56
		private void ResetRows()
		{
			this.rows = null;
			this.currentRow = 0;
			this.chunkSize = 12;
		}

		// Token: 0x06003314 RID: 13076 RVA: 0x000CF970 File Offset: 0x000CDB70
		private void PrepareQuery()
		{
			this.ResetRows();
			StoreObjectId defaultFolderId = this.session.GetDefaultFolderId(DefaultFolderType.AllContacts);
			if (defaultFolderId == null)
			{
				AllContactsCursor.Tracer.TraceDebug(0L, "AllContactsCursor.PrepareQuery: AllContacts search folder doesn't exist. Creating it.");
				this.session.CreateDefaultFolder(DefaultFolderType.AllContacts);
				defaultFolderId = this.session.GetDefaultFolderId(DefaultFolderType.AllContacts);
			}
			this.folder = Folder.Bind(this.session, defaultFolderId);
			this.query = this.folder.ItemQuery(ItemQueryType.None, null, this.sortByProperties, this.properties);
			this.MoveNext();
		}

		// Token: 0x06003315 RID: 13077 RVA: 0x000CF9F8 File Offset: 0x000CDBF8
		public bool SeekToOffset(int offset)
		{
			this.CheckDisposed("SeekToOffset");
			this.ResetRows();
			if (this.query.SeekToOffset(SeekReference.OriginBeginning, offset) == offset)
			{
				this.MoveNext();
				return true;
			}
			return false;
		}

		// Token: 0x06003316 RID: 13078 RVA: 0x000CFA24 File Offset: 0x000CDC24
		public bool SeekToCondition(SeekReference reference, QueryFilter seekFilter)
		{
			this.CheckDisposed("SeekToCondition");
			this.ResetRows();
			if (this.query.SeekToCondition(reference, seekFilter))
			{
				this.MoveNext();
				return true;
			}
			return false;
		}

		// Token: 0x06003317 RID: 13079 RVA: 0x000CFA4F File Offset: 0x000CDC4F
		protected void CheckDisposed(string methodName)
		{
			if (this.isDisposed)
			{
				StorageGlobals.TraceFailedCheckDisposed(this, methodName);
				throw new ObjectDisposedException(base.GetType().ToString());
			}
		}

		// Token: 0x06003318 RID: 13080 RVA: 0x000CFA71 File Offset: 0x000CDC71
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06003319 RID: 13081 RVA: 0x000CFA80 File Offset: 0x000CDC80
		private void Dispose(bool disposing)
		{
			StorageGlobals.TraceDispose(this, this.isDisposed, disposing);
			if (!this.isDisposed)
			{
				this.isDisposed = true;
				this.InternalDispose(disposing);
			}
		}

		// Token: 0x0600331A RID: 13082 RVA: 0x000CFAA8 File Offset: 0x000CDCA8
		protected virtual void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				if (this.query != null)
				{
					this.query.Dispose();
					this.query = null;
				}
				if (this.folder != null)
				{
					this.folder.Dispose();
					this.folder = null;
				}
				if (this.disposeTracker != null)
				{
					this.disposeTracker.Dispose();
				}
			}
		}

		// Token: 0x0600331B RID: 13083 RVA: 0x000CFAFF File Offset: 0x000CDCFF
		public virtual DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<AllContactsCursor>(this);
		}

		// Token: 0x0600331C RID: 13084 RVA: 0x000CFB07 File Offset: 0x000CDD07
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x04001B8B RID: 7051
		private const int MaxChunkSize = 1000;

		// Token: 0x04001B8C RID: 7052
		private static readonly Trace Tracer = ExTraceGlobals.PersonTracer;

		// Token: 0x04001B8D RID: 7053
		private static readonly PropertyDefinition[] requiredProperties = new PropertyDefinition[]
		{
			ItemSchema.Id,
			StoreObjectSchema.ItemClass
		};

		// Token: 0x04001B8E RID: 7054
		private readonly MailboxSession session;

		// Token: 0x04001B8F RID: 7055
		private readonly PropertyDefinition[] properties;

		// Token: 0x04001B90 RID: 7056
		private readonly SortBy[] sortByProperties;

		// Token: 0x04001B91 RID: 7057
		private readonly DisposeTracker disposeTracker;

		// Token: 0x04001B92 RID: 7058
		private bool isDisposed;

		// Token: 0x04001B93 RID: 7059
		private Folder folder;

		// Token: 0x04001B94 RID: 7060
		private QueryResult query;

		// Token: 0x04001B95 RID: 7061
		private int chunkSize = 12;

		// Token: 0x04001B96 RID: 7062
		private IStorePropertyBag[] rows;

		// Token: 0x04001B97 RID: 7063
		private int currentRow;
	}
}
