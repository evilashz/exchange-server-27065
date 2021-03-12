using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.UnifiedContent;

namespace Microsoft.Filtering
{
	// Token: 0x0200000F RID: 15
	internal abstract class FipsDataStreamFilteringRequest
	{
		// Token: 0x06000023 RID: 35 RVA: 0x00002936 File Offset: 0x00000B36
		protected FipsDataStreamFilteringRequest(string id, ContentManager contentManager)
		{
			if (contentManager == null)
			{
				throw new ArgumentNullException("contentManager");
			}
			this.Id = id;
			this.ContentManager = contentManager;
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000024 RID: 36 RVA: 0x0000295A File Offset: 0x00000B5A
		// (set) Token: 0x06000025 RID: 37 RVA: 0x00002962 File Offset: 0x00000B62
		public string Id { get; private set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000026 RID: 38 RVA: 0x0000296B File Offset: 0x00000B6B
		// (set) Token: 0x06000027 RID: 39 RVA: 0x00002973 File Offset: 0x00000B73
		public ContentManager ContentManager { get; private set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000028 RID: 40
		// (set) Token: 0x06000029 RID: 41
		public abstract RecoveryOptions RecoveryOptions { get; set; }

		// Token: 0x0600002A RID: 42 RVA: 0x0000297C File Offset: 0x00000B7C
		public FilteringRequest ToFilteringRequest(bool bypassBodyTextTruncation = false)
		{
			ContentManager contentManager = this.ContentManager;
			List<IExtractedContent> list = contentManager.ContentCollection;
			MemoryStream memoryStream = new MemoryStream();
			UnifiedContentSerializer unifiedContentSerializer = new UnifiedContentSerializer(memoryStream, contentManager.GetSharedStream(), list);
			this.Serialize(unifiedContentSerializer, bypassBodyTextTruncation);
			contentManager.GetSharedStream().Flush();
			unifiedContentSerializer.Commit();
			list = (contentManager.ContentCollection = unifiedContentSerializer.ContentCollection);
			FilteringRequest filteringRequest = new FilteringRequest(memoryStream, this.Id)
			{
				ContentCollection = list
			};
			filteringRequest.AddProperty("EnableUnifiedContent", contentManager.GetSharedStream().SharedName);
			return filteringRequest;
		}

		// Token: 0x0600002B RID: 43
		protected abstract void Serialize(UnifiedContentSerializer unifiedContentSerializer, bool bypassBodyTextTruncation = true);
	}
}
