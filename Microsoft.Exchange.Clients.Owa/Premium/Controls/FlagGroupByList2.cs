using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x02000378 RID: 888
	internal sealed class FlagGroupByList2 : GroupByList2
	{
		// Token: 0x0600213D RID: 8509 RVA: 0x000BF29C File Offset: 0x000BD49C
		public FlagGroupByList2(ColumnId sortedColumn, SortOrder sortOrder, ItemList2 itemList, UserContext userContext) : base(sortedColumn, sortOrder, itemList, userContext)
		{
			bool isDueDate = sortedColumn == ColumnId.FlagDueDate || sortedColumn == ColumnId.ContactFlagDueDate;
			List<TimeRange> timeRanges = TimeRange.GetTimeRanges(userContext);
			FlagGroupByList2.FlagGroupRange[] array = new FlagGroupByList2.FlagGroupRange[timeRanges.Count + 2];
			int num = 0;
			array[num++] = new FlagGroupByList2.FlagGroupRange(null, FlagStatus.NotFlagged, isDueDate);
			array[num++] = new FlagGroupByList2.FlagGroupRange(null, FlagStatus.Complete, isDueDate);
			int num2 = timeRanges.Count - 1;
			while (0 <= num2)
			{
				array[num++] = new FlagGroupByList2.FlagGroupRange(timeRanges[num2], FlagStatus.Flagged, isDueDate);
				num2--;
			}
			base.SetGroupRange(array);
		}

		// Token: 0x0200037A RID: 890
		internal class FlagGroupRange : IGroupRange
		{
			// Token: 0x170008B3 RID: 2227
			// (get) Token: 0x06002140 RID: 8512 RVA: 0x000BF32C File Offset: 0x000BD52C
			public string Header
			{
				get
				{
					string result = null;
					switch (this.flagStatus)
					{
					case FlagStatus.NotFlagged:
						result = LocalizedStrings.GetNonEncoded(-41558891);
						break;
					case FlagStatus.Complete:
						result = LocalizedStrings.GetNonEncoded(-1576429907);
						break;
					case FlagStatus.Flagged:
					{
						Strings.IDs? ds = null;
						int range = (int)this.timeRange.Range;
						if (range <= 2048)
						{
							if (range <= 64)
							{
								if (range <= 8)
								{
									switch (range)
									{
									case 1:
										ds = new Strings.IDs?(this.isDueDate ? -1196126296 : 139031534);
										goto IL_3DE;
									case 2:
										ds = new Strings.IDs?(this.isDueDate ? -727236540 : -788047606);
										goto IL_3DE;
									case 3:
										goto IL_3DE;
									case 4:
										ds = new Strings.IDs?(this.isDueDate ? -384794148 : -1216635934);
										goto IL_3DE;
									default:
										if (range != 8)
										{
											goto IL_3DE;
										}
										ds = new Strings.IDs?(this.isDueDate ? 60260340 : 1931460850);
										goto IL_3DE;
									}
								}
								else
								{
									if (range == 16)
									{
										ds = new Strings.IDs?(this.isDueDate ? -855902746 : -254434944);
										goto IL_3DE;
									}
									if (range == 32)
									{
										ds = new Strings.IDs?(this.isDueDate ? -1408071622 : 433632196);
										goto IL_3DE;
									}
									if (range != 64)
									{
										goto IL_3DE;
									}
								}
							}
							else if (range <= 256)
							{
								if (range != 128 && range != 256)
								{
									goto IL_3DE;
								}
							}
							else if (range != 512 && range != 1024 && range != 2048)
							{
								goto IL_3DE;
							}
						}
						else if (range <= 65536)
						{
							if (range <= 8192)
							{
								if (range != 4096)
								{
									if (range != 8192)
									{
										goto IL_3DE;
									}
									ds = new Strings.IDs?(this.isDueDate ? 794966019 : -821217845);
									goto IL_3DE;
								}
							}
							else
							{
								if (range == 16384)
								{
									ds = new Strings.IDs?(this.isDueDate ? -1826572779 : -1202335751);
									goto IL_3DE;
								}
								if (range == 32768)
								{
									ds = new Strings.IDs?(this.isDueDate ? 1529070723 : -1350058101);
									goto IL_3DE;
								}
								if (range != 65536)
								{
									goto IL_3DE;
								}
								ds = new Strings.IDs?(this.isDueDate ? 1783854170 : -1483604308);
								goto IL_3DE;
							}
						}
						else if (range <= 524288)
						{
							if (range == 131072)
							{
								ds = new Strings.IDs?(this.isDueDate ? 1204251419 : -76237725);
								goto IL_3DE;
							}
							if (range == 262144)
							{
								ds = new Strings.IDs?(this.isDueDate ? 1033163602 : 1370918388);
								goto IL_3DE;
							}
							if (range != 524288)
							{
								goto IL_3DE;
							}
							ds = new Strings.IDs?(this.isDueDate ? 1675550818 : 168259188);
							goto IL_3DE;
						}
						else
						{
							if (range == 1048576)
							{
								ds = new Strings.IDs?(this.isDueDate ? 1847943091 : -673220637);
								goto IL_3DE;
							}
							if (range == 2097152)
							{
								ds = new Strings.IDs?(this.isDueDate ? 1030778765 : -674474543);
								goto IL_3DE;
							}
							if (range != 4194304)
							{
								goto IL_3DE;
							}
							ds = new Strings.IDs?(this.isDueDate ? -1639284284 : 564383106);
							goto IL_3DE;
						}
						if (this.isDueDate)
						{
							result = string.Format(LocalizedStrings.GetNonEncoded(-1536093506), this.timeRange.Start.ToString("dddd", CultureInfo.CurrentCulture.DateTimeFormat));
						}
						else
						{
							result = string.Format(LocalizedStrings.GetNonEncoded(-1898240840), this.timeRange.Start.ToString("dddd", CultureInfo.CurrentCulture.DateTimeFormat));
						}
						IL_3DE:
						if (ds != null)
						{
							result = LocalizedStrings.GetNonEncoded(ds.Value);
						}
						break;
					}
					}
					return result;
				}
			}

			// Token: 0x06002141 RID: 8513 RVA: 0x000BF748 File Offset: 0x000BD948
			public FlagGroupRange(TimeRange timeRange, FlagStatus flagStatus, bool isDueDate)
			{
				this.timeRange = timeRange;
				this.isDueDate = isDueDate;
				this.flagStatus = flagStatus;
			}

			// Token: 0x06002142 RID: 8514 RVA: 0x000BF76C File Offset: 0x000BD96C
			public bool IsInGroup(IListViewDataSource dataSource, Column column)
			{
				FlagStatus itemProperty = dataSource.GetItemProperty<FlagStatus>(ItemSchema.FlagStatus, FlagStatus.NotFlagged);
				if (this.flagStatus != itemProperty)
				{
					return false;
				}
				if (itemProperty == FlagStatus.Flagged)
				{
					StorePropertyDefinition propertyDefinition;
					if (column.Id == ColumnId.FlagDueDate || column.Id == ColumnId.ContactFlagDueDate)
					{
						propertyDefinition = ItemSchema.UtcDueDate;
					}
					else
					{
						propertyDefinition = ItemSchema.UtcStartDate;
					}
					ExDateTime itemProperty2 = dataSource.GetItemProperty<ExDateTime>(propertyDefinition, ExDateTime.MinValue);
					return (this.timeRange.Start <= itemProperty2 && itemProperty2 < this.timeRange.End) || (this.timeRange.Start == itemProperty2 && this.timeRange.Start == this.timeRange.End);
				}
				return true;
			}

			// Token: 0x040017B6 RID: 6070
			private TimeRange timeRange;

			// Token: 0x040017B7 RID: 6071
			private FlagStatus flagStatus;

			// Token: 0x040017B8 RID: 6072
			private bool isDueDate = true;
		}
	}
}
