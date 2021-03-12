using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000615 RID: 1557
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class AnalysisItemsQueryData
	{
		// Token: 0x06003FF1 RID: 16369 RVA: 0x0010A8DA File Offset: 0x00108ADA
		public AnalysisItemsQueryData(object[] item)
		{
			this.key = new AnalysisGroupKey(item);
			this.item = item;
		}

		// Token: 0x1700130B RID: 4875
		// (get) Token: 0x06003FF2 RID: 16370 RVA: 0x0010A8F5 File Offset: 0x00108AF5
		public AnalysisGroupKey Key
		{
			get
			{
				return this.key;
			}
		}

		// Token: 0x1700130C RID: 4876
		// (get) Token: 0x06003FF3 RID: 16371 RVA: 0x0010A8FD File Offset: 0x00108AFD
		public StoreObjectId Id
		{
			get
			{
				return StoreId.GetStoreObjectId((StoreId)this.item[4]);
			}
		}

		// Token: 0x1700130D RID: 4877
		// (get) Token: 0x06003FF4 RID: 16372 RVA: 0x0010A911 File Offset: 0x00108B11
		public int Size
		{
			get
			{
				return (int)this.item[5];
			}
		}

		// Token: 0x1700130E RID: 4878
		// (get) Token: 0x06003FF5 RID: 16373 RVA: 0x0010A920 File Offset: 0x00108B20
		public ExDateTime LastModifiedTime
		{
			get
			{
				return (ExDateTime)this.item[6];
			}
		}

		// Token: 0x1700130F RID: 4879
		// (get) Token: 0x06003FF6 RID: 16374 RVA: 0x0010A930 File Offset: 0x00108B30
		public string ClientInfo
		{
			get
			{
				string text = (this.item[7] as string) ?? string.Empty;
				string text2 = (this.item[8] as string) ?? string.Empty;
				string text3 = (this.item[9] as string) ?? string.Empty;
				return string.Concat(new string[]
				{
					text,
					" \\ ",
					text2,
					" \\ ",
					text3
				});
			}
		}

		// Token: 0x0400235D RID: 9053
		private AnalysisGroupKey key;

		// Token: 0x0400235E RID: 9054
		private object[] item;
	}
}
