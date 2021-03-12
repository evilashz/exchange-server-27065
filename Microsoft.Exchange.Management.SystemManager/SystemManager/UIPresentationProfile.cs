using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Authorization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.SystemManager.WinForms;

namespace Microsoft.Exchange.Management.SystemManager
{
	// Token: 0x020000CB RID: 203
	public class UIPresentationProfile
	{
		// Token: 0x060006F8 RID: 1784 RVA: 0x000185F1 File Offset: 0x000167F1
		public UIPresentationProfile() : this(new ResultsColumnProfile[0], new FilterColumnProfile[0])
		{
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x00018605 File Offset: 0x00016805
		public UIPresentationProfile(ResultsColumnProfile[] displayedColumnCollection) : this(displayedColumnCollection, new FilterColumnProfile[0])
		{
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x00018614 File Offset: 0x00016814
		public UIPresentationProfile(ResultsColumnProfile[] displayedColumnCollection, FilterColumnProfile[] filterColumnCollection)
		{
			this.DisplayedColumnCollection = (displayedColumnCollection ?? new ResultsColumnProfile[0]);
			this.FilterColumnCollection = (filterColumnCollection ?? new FilterColumnProfile[0]);
			this.ScopeSupportingLevel = ScopeSupportingLevel.NoScoping;
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x060006FB RID: 1787 RVA: 0x00018645 File Offset: 0x00016845
		// (set) Token: 0x060006FC RID: 1788 RVA: 0x0001864D File Offset: 0x0001684D
		public bool AutoGenerateColumns { get; set; }

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x060006FD RID: 1789 RVA: 0x00018656 File Offset: 0x00016856
		// (set) Token: 0x060006FE RID: 1790 RVA: 0x0001865E File Offset: 0x0001685E
		public ResultsColumnProfile[] DisplayedColumnCollection { get; private set; }

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x060006FF RID: 1791 RVA: 0x00018667 File Offset: 0x00016867
		// (set) Token: 0x06000700 RID: 1792 RVA: 0x0001866F File Offset: 0x0001686F
		public FilterColumnProfile[] FilterColumnCollection { get; private set; }

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06000701 RID: 1793 RVA: 0x00018678 File Offset: 0x00016878
		// (set) Token: 0x06000702 RID: 1794 RVA: 0x00018680 File Offset: 0x00016880
		internal ObjectSchema FilterObjectSchema { get; set; }

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06000703 RID: 1795 RVA: 0x00018689 File Offset: 0x00016889
		// (set) Token: 0x06000704 RID: 1796 RVA: 0x00018691 File Offset: 0x00016891
		internal FilterLanguage FilterLanguage { get; set; }

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x06000705 RID: 1797 RVA: 0x0001869C File Offset: 0x0001689C
		internal Dictionary<string, FilterablePropertyDescription> FilterableProperties
		{
			get
			{
				if (this.filterableProperties == null)
				{
					this.filterableProperties = new Dictionary<string, FilterablePropertyDescription>();
					Dictionary<string, string> dictionary = new Dictionary<string, string>();
					foreach (ResultsColumnProfile resultsColumnProfile in this.DisplayedColumnCollection)
					{
						dictionary[resultsColumnProfile.Name] = resultsColumnProfile.Text;
					}
					foreach (FilterColumnProfile filterColumnProfile in this.FilterColumnCollection)
					{
						FilterablePropertyDescription value = new FilterablePropertyDescription(filterColumnProfile.PropertyDefinition, dictionary[filterColumnProfile.RefDisplayedColumn], filterColumnProfile.Operators, filterColumnProfile.PickerProfile, filterColumnProfile.ValueMember)
						{
							ObjectPickerDisplayMember = filterColumnProfile.DisplayMember,
							ColumnType = filterColumnProfile.ColumnType,
							FormatMode = filterColumnProfile.FormatMode,
							FilterableListSource = filterColumnProfile.FilterableListSource
						};
						this.filterableProperties.Add(filterColumnProfile.Name, value);
					}
				}
				return this.filterableProperties;
			}
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x0001879C File Offset: 0x0001699C
		public ExchangeColumnHeader[] CreateColumnHeaders()
		{
			List<ExchangeColumnHeader> list = new List<ExchangeColumnHeader>();
			foreach (ResultsColumnProfile resultsColumnProfile in this.DisplayedColumnCollection)
			{
				list.Add(new ExchangeColumnHeader(resultsColumnProfile.Name, resultsColumnProfile.Text, -2, resultsColumnProfile.IsDefault, resultsColumnProfile.DefaultEmptyText, resultsColumnProfile.CustomFormatter, resultsColumnProfile.FormatString, resultsColumnProfile.FormatProvider)
				{
					IsSortable = (resultsColumnProfile.SortMode != SortMode.NotSupported),
					ToColorFormatter = resultsColumnProfile.ColorFormatter
				});
			}
			return list.ToArray();
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06000707 RID: 1799 RVA: 0x0001882A File Offset: 0x00016A2A
		// (set) Token: 0x06000708 RID: 1800 RVA: 0x00018832 File Offset: 0x00016A32
		public bool HideIcon { get; set; }

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06000709 RID: 1801 RVA: 0x0001883B File Offset: 0x00016A3B
		// (set) Token: 0x0600070A RID: 1802 RVA: 0x00018843 File Offset: 0x00016A43
		public string DisplayName { get; set; }

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x0600070B RID: 1803 RVA: 0x0001884C File Offset: 0x00016A4C
		// (set) Token: 0x0600070C RID: 1804 RVA: 0x00018854 File Offset: 0x00016A54
		public string ImageProperty { get; set; }

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x0600070D RID: 1805 RVA: 0x0001885D File Offset: 0x00016A5D
		// (set) Token: 0x0600070E RID: 1806 RVA: 0x00018865 File Offset: 0x00016A65
		public string SortProperty { get; set; }

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x0600070F RID: 1807 RVA: 0x0001886E File Offset: 0x00016A6E
		// (set) Token: 0x06000710 RID: 1808 RVA: 0x00018876 File Offset: 0x00016A76
		public bool UseTreeViewForm { get; set; }

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000711 RID: 1809 RVA: 0x0001887F File Offset: 0x00016A7F
		// (set) Token: 0x06000712 RID: 1810 RVA: 0x00018887 File Offset: 0x00016A87
		public string HelpTopic { get; set; }

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000713 RID: 1811 RVA: 0x00018890 File Offset: 0x00016A90
		// (set) Token: 0x06000714 RID: 1812 RVA: 0x00018898 File Offset: 0x00016A98
		public ScopeSupportingLevel ScopeSupportingLevel { get; set; }

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06000715 RID: 1813 RVA: 0x000188A1 File Offset: 0x00016AA1
		// (set) Token: 0x06000716 RID: 1814 RVA: 0x000188A9 File Offset: 0x00016AA9
		public ExchangeRunspaceConfigurationSettings.SerializationLevel SerializationLevel { get; set; }

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000717 RID: 1815 RVA: 0x000188B2 File Offset: 0x00016AB2
		// (set) Token: 0x06000718 RID: 1816 RVA: 0x000188BA File Offset: 0x00016ABA
		public bool MultiSelect { get; set; }

		// Token: 0x04000350 RID: 848
		private Dictionary<string, FilterablePropertyDescription> filterableProperties;
	}
}
