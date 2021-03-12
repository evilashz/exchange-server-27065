using System;
using System.Text;
using Microsoft.Exchange.Server.Storage.LazyIndexing;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.Diagnostics
{
	// Token: 0x02000043 RID: 67
	public class QueryableCategorizationInfo
	{
		// Token: 0x060001DE RID: 478 RVA: 0x0000D91A File Offset: 0x0000BB1A
		public QueryableCategorizationInfo(CategorizationInfo categorizationInfo)
		{
			this.categorizationInfo = categorizationInfo;
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060001DF RID: 479 RVA: 0x0000D929 File Offset: 0x0000BB29
		public int BaseMessageViewLogicalIndexNumber
		{
			get
			{
				return this.categorizationInfo.BaseMessageViewLogicalIndexNumber;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x0000D936 File Offset: 0x0000BB36
		public bool BaseMessageViewInReverseOrder
		{
			get
			{
				return this.categorizationInfo.BaseMessageViewInReverseOrder;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x0000D943 File Offset: 0x0000BB43
		public int CategoryCount
		{
			get
			{
				return this.categorizationInfo.CategoryCount;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x0000D950 File Offset: 0x0000BB50
		public string CategoryHeaderSortOverrides
		{
			get
			{
				if (this.categoryHeaderSortOverrides == null && this.categorizationInfo.CategoryHeaderSortOverrides != null && this.categorizationInfo.CategoryHeaderSortOverrides.Length > 0)
				{
					StringBuilder stringBuilder = new StringBuilder(50 * this.categorizationInfo.CategoryHeaderSortOverrides.Length);
					for (int i = 0; i < this.categorizationInfo.CategoryHeaderSortOverrides.Length; i++)
					{
						if (i != 0)
						{
							stringBuilder.Append(", ");
						}
						CategoryHeaderSortOverride categoryHeaderSortOverride = this.categorizationInfo.CategoryHeaderSortOverrides[i];
						if (categoryHeaderSortOverride == null)
						{
							stringBuilder.AppendFormat("Level {0}: None", i);
						}
						else
						{
							stringBuilder.AppendFormat("Level {0}: ", i);
							categoryHeaderSortOverride.Column.AppendToString(stringBuilder, StringFormatOptions.IncludeDetails);
							stringBuilder.AppendFormat(" {0} (aggregate by {1})", categoryHeaderSortOverride.Ascending ? "asc" : "desc", categoryHeaderSortOverride.AggregateByMaxValue ? "max" : "min");
						}
					}
					this.categoryHeaderSortOverrides = stringBuilder.ToString();
				}
				return this.categoryHeaderSortOverrides;
			}
		}

		// Token: 0x04000128 RID: 296
		private readonly CategorizationInfo categorizationInfo;

		// Token: 0x04000129 RID: 297
		private string categoryHeaderSortOverrides;
	}
}
