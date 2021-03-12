using System;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x02000419 RID: 1049
	internal sealed class SizeGroupByList2 : GroupByList2
	{
		// Token: 0x060025B3 RID: 9651 RVA: 0x000DA696 File Offset: 0x000D8896
		public SizeGroupByList2(SortOrder sortOrder, ItemList2 itemList, UserContext userContext) : base(ColumnId.Size, sortOrder, itemList, userContext)
		{
			base.SetGroupRange(SizeGroupByList2.sizeGroupRange);
		}

		// Token: 0x060025B4 RID: 9652 RVA: 0x000DA6AE File Offset: 0x000D88AE
		public SizeGroupByList2(ColumnId groupByColumn, SortOrder sortOrder, ItemList2 itemList, UserContext userContext) : base(groupByColumn, sortOrder, itemList, userContext)
		{
			base.SetGroupRange(SizeGroupByList2.sizeGroupRange);
		}

		// Token: 0x04001A0A RID: 6666
		private static readonly IGroupRange[] sizeGroupRange = new IGroupRange[]
		{
			new SizeGroupByList2.SizeGroupRange(-461075480, int.MinValue, 10240),
			new SizeGroupByList2.SizeGroupRange(-346569563, 10240, 25600),
			new SizeGroupByList2.SizeGroupRange(-1064340177, 25600, 102400),
			new SizeGroupByList2.SizeGroupRange(-1891554117, 102400, 512000),
			new SizeGroupByList2.SizeGroupRange(1764690851, 512000, 1048576),
			new SizeGroupByList2.SizeGroupRange(-1718242455, 1048576, 5242880),
			new SizeGroupByList2.SizeGroupRange(-639396640, 5242880, int.MaxValue)
		};

		// Token: 0x0200041A RID: 1050
		private class SizeGroupRange : IGroupRange
		{
			// Token: 0x170009F8 RID: 2552
			// (get) Token: 0x060025B6 RID: 9654 RVA: 0x000DA783 File Offset: 0x000D8983
			public string Header
			{
				get
				{
					return LocalizedStrings.GetNonEncoded(this.header);
				}
			}

			// Token: 0x060025B7 RID: 9655 RVA: 0x000DA790 File Offset: 0x000D8990
			public SizeGroupRange(Strings.IDs header, int start, int end)
			{
				this.header = header;
				this.start = start;
				this.end = end;
			}

			// Token: 0x060025B8 RID: 9656 RVA: 0x000DA7B0 File Offset: 0x000D89B0
			public bool IsInGroup(IListViewDataSource dataSource, Column column)
			{
				object itemProperty = dataSource.GetItemProperty<object>(column[0]);
				long num = 0L;
				if (itemProperty != null)
				{
					if (itemProperty is int)
					{
						num = (long)((int)itemProperty);
					}
					else if (itemProperty is long)
					{
						num = (long)itemProperty;
					}
				}
				return (long)this.start <= num && num < (long)this.end;
			}

			// Token: 0x04001A0B RID: 6667
			private Strings.IDs header;

			// Token: 0x04001A0C RID: 6668
			private int start;

			// Token: 0x04001A0D RID: 6669
			private int end;
		}
	}
}
