using System;
using System.Collections;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x0200037D RID: 893
	internal class FolderListViewEmptyDataSource : ExchangeListViewDataSource, IListViewDataSource
	{
		// Token: 0x0600214B RID: 8523 RVA: 0x000BFB08 File Offset: 0x000BDD08
		public FolderListViewEmptyDataSource(Folder folder, Hashtable properties) : base(properties)
		{
			this.folder = folder;
		}

		// Token: 0x170008B6 RID: 2230
		// (get) Token: 0x0600214C RID: 8524 RVA: 0x000BFB18 File Offset: 0x000BDD18
		public Folder Folder
		{
			get
			{
				return this.folder;
			}
		}

		// Token: 0x170008B7 RID: 2231
		// (get) Token: 0x0600214D RID: 8525 RVA: 0x000BFB20 File Offset: 0x000BDD20
		public string ContainerId
		{
			get
			{
				return Utilities.GetIdAsString(this.folder);
			}
		}

		// Token: 0x170008B8 RID: 2232
		// (get) Token: 0x0600214E RID: 8526 RVA: 0x000BFB2D File Offset: 0x000BDD2D
		public new int StartRange
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170008B9 RID: 2233
		// (get) Token: 0x0600214F RID: 8527 RVA: 0x000BFB30 File Offset: 0x000BDD30
		public new int EndRange
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170008BA RID: 2234
		// (get) Token: 0x06002150 RID: 8528 RVA: 0x000BFB33 File Offset: 0x000BDD33
		public new int RangeCount
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170008BB RID: 2235
		// (get) Token: 0x06002151 RID: 8529 RVA: 0x000BFB36 File Offset: 0x000BDD36
		public override int TotalCount
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170008BC RID: 2236
		// (get) Token: 0x06002152 RID: 8530 RVA: 0x000BFB39 File Offset: 0x000BDD39
		public override int TotalItemCount
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170008BD RID: 2237
		// (get) Token: 0x06002153 RID: 8531 RVA: 0x000BFB3C File Offset: 0x000BDD3C
		public int UnreadCount
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170008BE RID: 2238
		// (get) Token: 0x06002154 RID: 8532 RVA: 0x000BFB3F File Offset: 0x000BDD3F
		public bool UserHasRightToLoad
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002155 RID: 8533 RVA: 0x000BFB42 File Offset: 0x000BDD42
		public void Load(string seekValue, int itemCount)
		{
		}

		// Token: 0x06002156 RID: 8534 RVA: 0x000BFB44 File Offset: 0x000BDD44
		public void Load(int startRange, int itemCount)
		{
		}

		// Token: 0x06002157 RID: 8535 RVA: 0x000BFB46 File Offset: 0x000BDD46
		public void Load(ObjectId seekToObjectId, SeekDirection seekDirection, int itemCount)
		{
		}

		// Token: 0x06002158 RID: 8536 RVA: 0x000BFB48 File Offset: 0x000BDD48
		public bool LoadAdjacent(ObjectId adjacentObjectId, SeekDirection seekDirection, int itemCount)
		{
			throw new NotImplementedException("This API is not needed while using the FolderListViewEmptyDataSource");
		}

		// Token: 0x06002159 RID: 8537 RVA: 0x000BFB54 File Offset: 0x000BDD54
		public new bool MoveNext()
		{
			throw new NotImplementedException("This API is not needed while using the FolderListViewEmptyDataSource");
		}

		// Token: 0x0600215A RID: 8538 RVA: 0x000BFB60 File Offset: 0x000BDD60
		public new void MoveToItem(int itemIndex)
		{
			throw new NotImplementedException("This API is not needed while using the FolderListViewEmptyDataSource");
		}

		// Token: 0x170008BF RID: 2239
		// (get) Token: 0x0600215B RID: 8539 RVA: 0x000BFB6C File Offset: 0x000BDD6C
		public new int CurrentItem
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x0600215C RID: 8540 RVA: 0x000BFB6F File Offset: 0x000BDD6F
		public string GetItemId()
		{
			throw new NotImplementedException("This API is not needed while using the FolderListViewEmptyDataSource");
		}

		// Token: 0x0600215D RID: 8541 RVA: 0x000BFB7B File Offset: 0x000BDD7B
		public string GetItemClass()
		{
			throw new NotImplementedException("This API is not needed while using the FolderListViewEmptyDataSource");
		}

		// Token: 0x0600215E RID: 8542 RVA: 0x000BFB87 File Offset: 0x000BDD87
		public override T GetItemProperty<T>(PropertyDefinition propertyDefinition, T defaultValue)
		{
			throw new NotImplementedException("This API is not needed while using the FolderListViewEmptyDataSource");
		}

		// Token: 0x0600215F RID: 8543 RVA: 0x000BFB93 File Offset: 0x000BDD93
		public new object GetCurrentItem()
		{
			throw new NotImplementedException("This API is not needed while using the FolderListViewEmptyDataSource");
		}

		// Token: 0x040017BB RID: 6075
		private Folder folder;
	}
}
