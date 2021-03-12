using System;
using System.IO;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.UnifiedContent;
using Microsoft.Exchange.UnifiedContent.Exchange;

namespace Microsoft.Filtering
{
	// Token: 0x02000011 RID: 17
	internal sealed class MapiFipsDataStreamFilteringRequest : FipsDataStreamFilteringRequest
	{
		// Token: 0x06000036 RID: 54 RVA: 0x00002AF5 File Offset: 0x00000CF5
		private MapiFipsDataStreamFilteringRequest(Item item, IExtendedMapiFilteringContext context, string id, ContentManager contentManager) : base(id, contentManager)
		{
			this.Item = item;
			this.Context = context;
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00002B0E File Offset: 0x00000D0E
		// (set) Token: 0x06000038 RID: 56 RVA: 0x00002B16 File Offset: 0x00000D16
		public Item Item { get; private set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000039 RID: 57 RVA: 0x00002B1F File Offset: 0x00000D1F
		// (set) Token: 0x0600003A RID: 58 RVA: 0x00002B27 File Offset: 0x00000D27
		public IExtendedMapiFilteringContext Context { get; private set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00002B30 File Offset: 0x00000D30
		// (set) Token: 0x0600003C RID: 60 RVA: 0x00002B3D File Offset: 0x00000D3D
		public override RecoveryOptions RecoveryOptions
		{
			get
			{
				return this.Context.GetFipsRecoveryOptions();
			}
			set
			{
				this.Context.SetFipsRecoveryOptions(value);
			}
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002B4C File Offset: 0x00000D4C
		public static MapiFipsDataStreamFilteringRequest CreateInstance(Item item, IExtendedMapiFilteringContext context)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			string id = item.Id.ToString();
			ContentManager contentManager = new ContentManager(Path.GetTempPath());
			return new MapiFipsDataStreamFilteringRequest(item, context, id, contentManager);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002B95 File Offset: 0x00000D95
		protected override void Serialize(UnifiedContentSerializer unifiedContentSerializer, bool bypassBodyTextTruncation = true)
		{
			this.Item.Serialize(this.Context, unifiedContentSerializer, bypassBodyTextTruncation);
		}
	}
}
