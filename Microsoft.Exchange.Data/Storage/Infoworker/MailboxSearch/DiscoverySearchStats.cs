using System;
using System.Text;

namespace Microsoft.Exchange.Data.Storage.Infoworker.MailboxSearch
{
	// Token: 0x0200022F RID: 559
	public class DiscoverySearchStats
	{
		// Token: 0x06001366 RID: 4966 RVA: 0x0003B40B File Offset: 0x0003960B
		internal DiscoverySearchStats()
		{
			this.TotalItemsCopied = 0L;
			this.UnsearchableItemsAdded = 0L;
			this.EstimatedItems = 0L;
			this.TotalDuplicateItems = 0L;
			this.SkippedErrorItems = 0L;
		}

		// Token: 0x170005E9 RID: 1513
		// (get) Token: 0x06001367 RID: 4967 RVA: 0x0003B43B File Offset: 0x0003963B
		// (set) Token: 0x06001368 RID: 4968 RVA: 0x0003B443 File Offset: 0x00039643
		public long TotalItemsCopied { get; set; }

		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x06001369 RID: 4969 RVA: 0x0003B44C File Offset: 0x0003964C
		// (set) Token: 0x0600136A RID: 4970 RVA: 0x0003B454 File Offset: 0x00039654
		public long TotalDuplicateItems { get; set; }

		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x0600136B RID: 4971 RVA: 0x0003B45D File Offset: 0x0003965D
		// (set) Token: 0x0600136C RID: 4972 RVA: 0x0003B465 File Offset: 0x00039665
		public long EstimatedItems { get; set; }

		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x0600136D RID: 4973 RVA: 0x0003B46E File Offset: 0x0003966E
		// (set) Token: 0x0600136E RID: 4974 RVA: 0x0003B476 File Offset: 0x00039676
		public long UnsearchableItemsAdded { get; set; }

		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x0600136F RID: 4975 RVA: 0x0003B47F File Offset: 0x0003967F
		// (set) Token: 0x06001370 RID: 4976 RVA: 0x0003B487 File Offset: 0x00039687
		public long SkippedErrorItems { get; set; }

		// Token: 0x06001371 RID: 4977 RVA: 0x0003B490 File Offset: 0x00039690
		public static DiscoverySearchStats Parse(string value)
		{
			long estimatedItems = 0L;
			long unsearchableItemsAdded = 0L;
			long totalDuplicateItems = 0L;
			long totalItemsCopied = 0L;
			long skippedErrorItems = 0L;
			if (!string.IsNullOrEmpty(value))
			{
				string[] array = value.Split(new char[]
				{
					'\t'
				});
				foreach (string text in array)
				{
					string[] array3 = text.Split(new char[]
					{
						':'
					});
					string a;
					if (array3 != null && array3.Length > 1 && (a = array3[0]) != null)
					{
						if (!(a == "TotalCopiedItems"))
						{
							if (!(a == "EstimatedItems"))
							{
								if (!(a == "UnsearchableItemsAdded"))
								{
									if (!(a == "DuplicatesRemoved"))
									{
										if (a == "SkippedErrorItems")
										{
											skippedErrorItems = long.Parse(array3[1]);
										}
									}
									else
									{
										totalDuplicateItems = long.Parse(array3[1]);
									}
								}
								else
								{
									unsearchableItemsAdded = long.Parse(array3[1]);
								}
							}
							else
							{
								estimatedItems = long.Parse(array3[1]);
							}
						}
						else
						{
							totalItemsCopied = long.Parse(array3[1]);
						}
					}
				}
			}
			return new DiscoverySearchStats
			{
				TotalItemsCopied = totalItemsCopied,
				UnsearchableItemsAdded = unsearchableItemsAdded,
				EstimatedItems = estimatedItems,
				TotalDuplicateItems = totalDuplicateItems,
				SkippedErrorItems = skippedErrorItems
			};
		}

		// Token: 0x06001372 RID: 4978 RVA: 0x0003B5E0 File Offset: 0x000397E0
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			this.ToStringHelper<long>(stringBuilder, "EstimatedItems", this.EstimatedItems, false);
			this.ToStringHelper<long>(stringBuilder, "UnsearchableItemsAdded", this.UnsearchableItemsAdded, false);
			this.ToStringHelper<long>(stringBuilder, "DuplicatesRemoved", this.TotalDuplicateItems, false);
			this.ToStringHelper<long>(stringBuilder, "SkippedErrorItems", this.SkippedErrorItems, false);
			this.ToStringHelper<long>(stringBuilder, "TotalCopiedItems", this.TotalItemsCopied, true);
			return stringBuilder.ToString();
		}

		// Token: 0x06001373 RID: 4979 RVA: 0x0003B658 File Offset: 0x00039858
		public string ToHtmlString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("<table>");
			stringBuilder.Append("<tr>");
			stringBuilder.Append("<td>");
			stringBuilder.Append(DataStrings.EstimatedItems);
			stringBuilder.Append("</td>");
			stringBuilder.Append("<td align=\"right\">");
			stringBuilder.Append(this.EstimatedItems);
			stringBuilder.Append("</td>");
			stringBuilder.Append("</tr>");
			stringBuilder.Append("<tr>");
			stringBuilder.Append("<td>");
			stringBuilder.Append(DataStrings.UnsearchableItemsAdded);
			stringBuilder.Append("</td>");
			stringBuilder.Append("<td align=\"right\">+");
			stringBuilder.Append(this.UnsearchableItemsAdded);
			stringBuilder.Append("</td>");
			stringBuilder.Append("</tr>");
			stringBuilder.Append("<tr>");
			stringBuilder.Append("<td>");
			stringBuilder.Append(DataStrings.DuplicatesRemoved);
			stringBuilder.Append("</td>");
			stringBuilder.Append("<td align=\"right\">-");
			stringBuilder.Append(this.TotalDuplicateItems);
			stringBuilder.Append("</td>");
			stringBuilder.Append("</tr>");
			stringBuilder.Append("<tr>");
			stringBuilder.Append("<td>");
			stringBuilder.Append(DataStrings.CopyErrors);
			stringBuilder.Append("</td>");
			stringBuilder.Append("<td align=\"right\">-");
			stringBuilder.Append(this.SkippedErrorItems);
			stringBuilder.Append("</td>");
			stringBuilder.Append("</tr>");
			stringBuilder.Append("<tr>");
			stringBuilder.Append("<td>");
			stringBuilder.Append(DataStrings.TotalCopiedItems);
			stringBuilder.Append("</td>");
			stringBuilder.Append("<td align=\"right\">=");
			stringBuilder.Append(this.TotalItemsCopied);
			stringBuilder.Append("</td>");
			stringBuilder.Append("</tr>");
			stringBuilder.Append("</table>");
			return stringBuilder.ToString();
		}

		// Token: 0x06001374 RID: 4980 RVA: 0x0003B887 File Offset: 0x00039A87
		private void ToStringHelper<T>(StringBuilder sb, string name, T value, bool isLast)
		{
			sb.Append(name);
			sb.Append(':');
			sb.Append(value);
			if (!isLast)
			{
				sb.Append('\t');
			}
		}

		// Token: 0x04000B54 RID: 2900
		public const string TotalItemsCopiedName = "TotalCopiedItems";

		// Token: 0x04000B55 RID: 2901
		public const string UnsearchableItemsAddedName = "UnsearchableItemsAdded";

		// Token: 0x04000B56 RID: 2902
		public const string EstimatedItemsName = "EstimatedItems";

		// Token: 0x04000B57 RID: 2903
		public const string TotalDuplicateItemsName = "DuplicatesRemoved";

		// Token: 0x04000B58 RID: 2904
		public const string SkippedErrorItemsName = "SkippedErrorItems";
	}
}
