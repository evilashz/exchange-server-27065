using System;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000183 RID: 387
	[CLSCompliant(false)]
	public interface IExchangeService
	{
		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06000943 RID: 2371
		// (set) Token: 0x06000944 RID: 2372
		int Timeout { get; set; }

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06000945 RID: 2373
		// (set) Token: 0x06000946 RID: 2374
		string Url { get; set; }

		// Token: 0x06000947 RID: 2375
		GetFolderResponseType GetFolder(GetFolderType getFolder);

		// Token: 0x06000948 RID: 2376
		CreateFolderResponseType CreateFolder(CreateFolderType createFolder);

		// Token: 0x06000949 RID: 2377
		CreateItemResponseType CreateItem(CreateItemType createItem);

		// Token: 0x0600094A RID: 2378
		FindFolderResponseType FindFolder(FindFolderType findFolder);

		// Token: 0x0600094B RID: 2379
		FindItemResponseType FindItem(FindItemType findItem);

		// Token: 0x0600094C RID: 2380
		GetItemResponseType GetItem(GetItemType getItem);

		// Token: 0x0600094D RID: 2381
		ExportItemsResponseType ExportItems(ExportItemsType items);

		// Token: 0x0600094E RID: 2382
		UploadItemsResponseType UploadItems(UploadItemsType items);
	}
}
