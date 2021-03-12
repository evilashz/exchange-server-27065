using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x0200005B RID: 91
	internal abstract class UMSearchFolder : DisposableBase
	{
		// Token: 0x06000379 RID: 889 RVA: 0x0000CEA2 File Offset: 0x0000B0A2
		protected UMSearchFolder(MailboxSession itemStore)
		{
			this.itemStore = itemStore;
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x0600037A RID: 890 RVA: 0x0000CEB1 File Offset: 0x0000B0B1
		internal StoreObjectId SearchFolderId
		{
			get
			{
				if (this.searchFolder == null)
				{
					return this.GetSearchFolderId();
				}
				return this.searchFolder.Id.ObjectId;
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x0600037B RID: 891 RVA: 0x0000CED4 File Offset: 0x0000B0D4
		internal int UnreadCount
		{
			get
			{
				return (int)this.SearchFolder.GetProperties(new PropertyDefinition[]
				{
					FolderSchema.UnreadCount
				})[0];
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x0600037C RID: 892 RVA: 0x0000CF03 File Offset: 0x0000B103
		// (set) Token: 0x0600037D RID: 893 RVA: 0x0000CF19 File Offset: 0x0000B119
		protected internal SearchFolder SearchFolder
		{
			get
			{
				if (this.searchFolder == null)
				{
					this.CreateSearchFolder();
				}
				return this.searchFolder;
			}
			protected set
			{
				this.searchFolder = value;
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x0600037E RID: 894 RVA: 0x0000CF22 File Offset: 0x0000B122
		// (set) Token: 0x0600037F RID: 895 RVA: 0x0000CF2A File Offset: 0x0000B12A
		protected internal MailboxSession ItemStore
		{
			get
			{
				return this.itemStore;
			}
			protected set
			{
				this.itemStore = value;
			}
		}

		// Token: 0x06000380 RID: 896 RVA: 0x0000CF34 File Offset: 0x0000B134
		internal static UMSearchFolder Get(MailboxSession itemStore, UMSearchFolder.Type folderType)
		{
			UMSearchFolder result = null;
			switch (folderType)
			{
			case UMSearchFolder.Type.VoiceMail:
				result = new VoiceMailSearchFolder(itemStore);
				break;
			case UMSearchFolder.Type.Fax:
				result = new FaxSearchFolder(itemStore);
				break;
			}
			return result;
		}

		// Token: 0x06000381 RID: 897 RVA: 0x0000CF68 File Offset: 0x0000B168
		internal void CreateSearchFolder()
		{
			if (this.SearchFolderId != null)
			{
				try
				{
					this.searchFolder = SearchFolder.Bind(this.itemStore, this.SearchFolderId);
					return;
				}
				catch (ObjectNotFoundException arg)
				{
					ExTraceGlobals.UtilTracer.TraceError<ObjectNotFoundException>(0L, "Could not bind to UM search folder. Attempting to repair folder. Exception='{0}'", arg);
					if (!this.InternalTryRepairSearchFolder())
					{
						throw;
					}
					return;
				}
			}
			this.InternalCreateSearchFolder();
		}

		// Token: 0x06000382 RID: 898 RVA: 0x0000CFCC File Offset: 0x0000B1CC
		internal void DeleteSearchFolder()
		{
			if (this.SearchFolderId != null)
			{
				this.InternalDeleteSearchFolder();
			}
		}

		// Token: 0x06000383 RID: 899 RVA: 0x0000CFDC File Offset: 0x0000B1DC
		protected override void InternalDispose(bool disposing)
		{
			if (disposing && this.searchFolder != null)
			{
				this.searchFolder.Dispose();
				this.searchFolder = null;
			}
		}

		// Token: 0x06000384 RID: 900 RVA: 0x0000CFFB File Offset: 0x0000B1FB
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<UMSearchFolder>(this);
		}

		// Token: 0x06000385 RID: 901
		protected abstract StoreObjectId GetSearchFolderId();

		// Token: 0x06000386 RID: 902
		protected abstract void InternalCreateSearchFolder();

		// Token: 0x06000387 RID: 903
		protected abstract void InternalDeleteSearchFolder();

		// Token: 0x06000388 RID: 904
		protected abstract bool InternalTryRepairSearchFolder();

		// Token: 0x0400028F RID: 655
		private SearchFolder searchFolder;

		// Token: 0x04000290 RID: 656
		private MailboxSession itemStore;

		// Token: 0x0200005C RID: 92
		internal enum Type
		{
			// Token: 0x04000292 RID: 658
			VoiceMail,
			// Token: 0x04000293 RID: 659
			Fax
		}
	}
}
