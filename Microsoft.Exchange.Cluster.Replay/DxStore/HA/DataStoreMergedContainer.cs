using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.DxStore.Common;

namespace Microsoft.Exchange.DxStore.HA
{
	// Token: 0x02000167 RID: 359
	public class DataStoreMergedContainer
	{
		// Token: 0x06000E65 RID: 3685 RVA: 0x0003E033 File Offset: 0x0003C233
		public DataStoreMergedContainer(DiffReportVerboseMode diffReportVerboseMode)
		{
			this.Entries = new SortedDictionary<string, DataStoreMergedContainer.KeyEntry>(StringComparer.OrdinalIgnoreCase);
			this.DiffReportVerboseMode = diffReportVerboseMode;
		}

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06000E66 RID: 3686 RVA: 0x0003E052 File Offset: 0x0003C252
		// (set) Token: 0x06000E67 RID: 3687 RVA: 0x0003E05A File Offset: 0x0003C25A
		public bool IsAnalysisComplete { get; private set; }

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06000E68 RID: 3688 RVA: 0x0003E063 File Offset: 0x0003C263
		// (set) Token: 0x06000E69 RID: 3689 RVA: 0x0003E06B File Offset: 0x0003C26B
		public DiffReportVerboseMode DiffReportVerboseMode { get; set; }

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06000E6A RID: 3690 RVA: 0x0003E074 File Offset: 0x0003C274
		// (set) Token: 0x06000E6B RID: 3691 RVA: 0x0003E07C File Offset: 0x0003C27C
		public SortedDictionary<string, DataStoreMergedContainer.KeyEntry> Entries { get; set; }

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x06000E6C RID: 3692 RVA: 0x0003E085 File Offset: 0x0003C285
		// (set) Token: 0x06000E6D RID: 3693 RVA: 0x0003E08D File Offset: 0x0003C28D
		public DataStoreDiffReport Report { get; set; }

		// Token: 0x06000E6E RID: 3694 RVA: 0x0003E098 File Offset: 0x0003C298
		public DataStoreMergedContainer.KeyEntry AddOrUpdateKey(string fullKeyName, bool isClusdb)
		{
			DataStoreMergedContainer.KeyEntry keyEntry = null;
			if (!this.Entries.TryGetValue(fullKeyName, out keyEntry) || keyEntry == null)
			{
				keyEntry = new DataStoreMergedContainer.KeyEntry(fullKeyName);
				this.Entries.Add(fullKeyName, keyEntry);
			}
			keyEntry.Update(isClusdb);
			return keyEntry;
		}

		// Token: 0x06000E6F RID: 3695 RVA: 0x0003E0D8 File Offset: 0x0003C2D8
		public void Analyze()
		{
			if (!this.IsAnalysisComplete)
			{
				this.IsAnalysisComplete = true;
				foreach (DataStoreMergedContainer.KeyEntry keyEntry in this.Entries.Values)
				{
					keyEntry.Analyze();
				}
				this.Report = DataStoreDiffReport.Create(this.Entries.Values, this.DiffReportVerboseMode);
			}
		}

		// Token: 0x02000168 RID: 360
		public class EntryBase
		{
			// Token: 0x06000E70 RID: 3696 RVA: 0x0003E15C File Offset: 0x0003C35C
			public EntryBase(string name)
			{
				this.Name = name;
			}

			// Token: 0x170003A8 RID: 936
			// (get) Token: 0x06000E71 RID: 3697 RVA: 0x0003E16B File Offset: 0x0003C36B
			// (set) Token: 0x06000E72 RID: 3698 RVA: 0x0003E173 File Offset: 0x0003C373
			public string Name { get; set; }

			// Token: 0x170003A9 RID: 937
			// (get) Token: 0x06000E73 RID: 3699 RVA: 0x0003E17C File Offset: 0x0003C37C
			// (set) Token: 0x06000E74 RID: 3700 RVA: 0x0003E184 File Offset: 0x0003C384
			public bool IsPresentInClusdb { get; set; }

			// Token: 0x170003AA RID: 938
			// (get) Token: 0x06000E75 RID: 3701 RVA: 0x0003E18D File Offset: 0x0003C38D
			// (set) Token: 0x06000E76 RID: 3702 RVA: 0x0003E195 File Offset: 0x0003C395
			public bool IsPresentInDxStore { get; set; }

			// Token: 0x170003AB RID: 939
			// (get) Token: 0x06000E77 RID: 3703 RVA: 0x0003E19E File Offset: 0x0003C39E
			public bool IsPresentOnlyInClusdb
			{
				get
				{
					return this.IsPresentInClusdb && !this.IsPresentInDxStore;
				}
			}

			// Token: 0x170003AC RID: 940
			// (get) Token: 0x06000E78 RID: 3704 RVA: 0x0003E1B3 File Offset: 0x0003C3B3
			public bool IsPresentOnlyInDxStore
			{
				get
				{
					return this.IsPresentInDxStore && !this.IsPresentInClusdb;
				}
			}

			// Token: 0x170003AD RID: 941
			// (get) Token: 0x06000E79 RID: 3705 RVA: 0x0003E1C8 File Offset: 0x0003C3C8
			public bool IsPresentOnBoth
			{
				get
				{
					return this.IsPresentInDxStore && this.IsPresentInClusdb;
				}
			}
		}

		// Token: 0x02000169 RID: 361
		public class PropertyEntry : DataStoreMergedContainer.EntryBase
		{
			// Token: 0x06000E7A RID: 3706 RVA: 0x0003E1DA File Offset: 0x0003C3DA
			public PropertyEntry(string propertyName) : base(propertyName)
			{
			}

			// Token: 0x170003AE RID: 942
			// (get) Token: 0x06000E7B RID: 3707 RVA: 0x0003E1E3 File Offset: 0x0003C3E3
			// (set) Token: 0x06000E7C RID: 3708 RVA: 0x0003E1EB File Offset: 0x0003C3EB
			public string ClusdbValue { get; set; }

			// Token: 0x170003AF RID: 943
			// (get) Token: 0x06000E7D RID: 3709 RVA: 0x0003E1F4 File Offset: 0x0003C3F4
			// (set) Token: 0x06000E7E RID: 3710 RVA: 0x0003E1FC File Offset: 0x0003C3FC
			public string ClusdbValueKind { get; set; }

			// Token: 0x170003B0 RID: 944
			// (get) Token: 0x06000E7F RID: 3711 RVA: 0x0003E205 File Offset: 0x0003C405
			// (set) Token: 0x06000E80 RID: 3712 RVA: 0x0003E20D File Offset: 0x0003C40D
			public string DxStoreValue { get; set; }

			// Token: 0x170003B1 RID: 945
			// (get) Token: 0x06000E81 RID: 3713 RVA: 0x0003E216 File Offset: 0x0003C416
			// (set) Token: 0x06000E82 RID: 3714 RVA: 0x0003E21E File Offset: 0x0003C41E
			public string DxStoreValueKind { get; set; }

			// Token: 0x170003B2 RID: 946
			// (get) Token: 0x06000E83 RID: 3715 RVA: 0x0003E228 File Offset: 0x0003C428
			public bool IsValueMatches
			{
				get
				{
					if (this.isValueMatches == null)
					{
						if (base.IsPresentOnBoth && Utils.IsEqual(this.ClusdbValue, this.DxStoreValue, StringComparison.OrdinalIgnoreCase) && Utils.IsEqual(this.ClusdbValueKind, this.DxStoreValueKind, StringComparison.OrdinalIgnoreCase))
						{
							this.isValueMatches = new bool?(true);
						}
						else
						{
							this.isValueMatches = new bool?(false);
						}
					}
					return this.isValueMatches.Value;
				}
			}

			// Token: 0x06000E84 RID: 3716 RVA: 0x0003E298 File Offset: 0x0003C498
			public void Update(string propertyValue, string propertyKind, bool isClusdb)
			{
				string text = propertyValue.Replace("\r\n", "\n");
				if (isClusdb)
				{
					base.IsPresentInClusdb = true;
					this.ClusdbValue = text;
					this.ClusdbValueKind = propertyKind;
					return;
				}
				base.IsPresentInDxStore = true;
				this.DxStoreValue = text;
				this.DxStoreValueKind = propertyKind;
			}

			// Token: 0x040005F8 RID: 1528
			private bool? isValueMatches;
		}

		// Token: 0x0200016A RID: 362
		public class KeyEntry : DataStoreMergedContainer.EntryBase
		{
			// Token: 0x06000E85 RID: 3717 RVA: 0x0003E2E4 File Offset: 0x0003C4E4
			public KeyEntry(string fullKeyName) : base(fullKeyName)
			{
				this.Properties = new SortedDictionary<string, DataStoreMergedContainer.PropertyEntry>(StringComparer.OrdinalIgnoreCase);
			}

			// Token: 0x170003B3 RID: 947
			// (get) Token: 0x06000E86 RID: 3718 RVA: 0x0003E2FD File Offset: 0x0003C4FD
			// (set) Token: 0x06000E87 RID: 3719 RVA: 0x0003E305 File Offset: 0x0003C505
			public int PropertyMatchCount { get; set; }

			// Token: 0x170003B4 RID: 948
			// (get) Token: 0x06000E88 RID: 3720 RVA: 0x0003E30E File Offset: 0x0003C50E
			// (set) Token: 0x06000E89 RID: 3721 RVA: 0x0003E316 File Offset: 0x0003C516
			public int PropertiesOnlyInClusdbCount { get; set; }

			// Token: 0x170003B5 RID: 949
			// (get) Token: 0x06000E8A RID: 3722 RVA: 0x0003E31F File Offset: 0x0003C51F
			// (set) Token: 0x06000E8B RID: 3723 RVA: 0x0003E327 File Offset: 0x0003C527
			public int PropertiesOnlyInDxStoreCount { get; set; }

			// Token: 0x170003B6 RID: 950
			// (get) Token: 0x06000E8C RID: 3724 RVA: 0x0003E330 File Offset: 0x0003C530
			// (set) Token: 0x06000E8D RID: 3725 RVA: 0x0003E338 File Offset: 0x0003C538
			public int PropertyDifferentCount { get; set; }

			// Token: 0x170003B7 RID: 951
			// (get) Token: 0x06000E8E RID: 3726 RVA: 0x0003E341 File Offset: 0x0003C541
			public bool IsPropertiesMatch
			{
				get
				{
					return this.PropertyMatchCount == this.Properties.Count;
				}
			}

			// Token: 0x170003B8 RID: 952
			// (get) Token: 0x06000E8F RID: 3727 RVA: 0x0003E356 File Offset: 0x0003C556
			// (set) Token: 0x06000E90 RID: 3728 RVA: 0x0003E35E File Offset: 0x0003C55E
			public SortedDictionary<string, DataStoreMergedContainer.PropertyEntry> Properties { get; set; }

			// Token: 0x06000E91 RID: 3729 RVA: 0x0003E367 File Offset: 0x0003C567
			public void Update(bool isClusdb)
			{
				if (isClusdb)
				{
					base.IsPresentInClusdb = true;
					return;
				}
				base.IsPresentInDxStore = true;
			}

			// Token: 0x06000E92 RID: 3730 RVA: 0x0003E37C File Offset: 0x0003C57C
			public DataStoreMergedContainer.PropertyEntry AddOrUpdateProperty(string propertyName, string propertyValue, string kind, bool isClusdb)
			{
				DataStoreMergedContainer.PropertyEntry propertyEntry = null;
				if (!this.Properties.TryGetValue(propertyName, out propertyEntry) || propertyEntry == null)
				{
					propertyEntry = new DataStoreMergedContainer.PropertyEntry(propertyName);
					this.Properties.Add(propertyName, propertyEntry);
				}
				propertyEntry.Update(propertyValue, kind, isClusdb);
				return propertyEntry;
			}

			// Token: 0x06000E93 RID: 3731 RVA: 0x0003E3C0 File Offset: 0x0003C5C0
			public void Analyze()
			{
				if (this.isAnalyzed)
				{
					return;
				}
				foreach (DataStoreMergedContainer.PropertyEntry propertyEntry in this.Properties.Values)
				{
					if (propertyEntry.IsPresentOnlyInClusdb)
					{
						this.PropertiesOnlyInClusdbCount++;
					}
					else if (propertyEntry.IsPresentOnlyInDxStore)
					{
						this.PropertiesOnlyInDxStoreCount++;
					}
					else if (propertyEntry.IsValueMatches)
					{
						this.PropertyMatchCount++;
					}
					else
					{
						this.PropertyDifferentCount++;
					}
				}
				this.isAnalyzed = true;
			}

			// Token: 0x06000E94 RID: 3732 RVA: 0x0003E478 File Offset: 0x0003C678
			public void DumpStats(StringBuilder sb)
			{
				sb.AppendFormat("\nKey:'{0}' Properties - Total: {1} Matching: {2} Different: {3} ClusdbOnly: {4} DxStoreOnly: {5}", new object[]
				{
					base.Name,
					this.Properties.Count,
					this.PropertyMatchCount,
					this.PropertyDifferentCount,
					this.PropertiesOnlyInClusdbCount,
					this.PropertiesOnlyInDxStoreCount
				});
			}

			// Token: 0x06000E95 RID: 3733 RVA: 0x0003E4F0 File Offset: 0x0003C6F0
			public void DumpProperties(StringBuilder sb, bool isIncludeValues)
			{
				foreach (DataStoreMergedContainer.PropertyEntry propertyEntry in this.Properties.Values)
				{
					string arg;
					if (propertyEntry.IsPresentOnlyInClusdb)
					{
						arg = "only in clusdb";
					}
					else if (propertyEntry.IsPresentOnlyInClusdb)
					{
						arg = "only in dxstore";
					}
					else
					{
						arg = (propertyEntry.IsValueMatches ? "matches" : "different");
					}
					sb.AppendFormat("\n   {0} : <{1}>", propertyEntry.Name, arg);
					if (isIncludeValues)
					{
						if (propertyEntry.IsPresentInClusdb)
						{
							sb.AppendFormat("\n   [CLS] => ({0}):{1}", propertyEntry.ClusdbValueKind, propertyEntry.ClusdbValue);
						}
						if (propertyEntry.IsPresentInDxStore)
						{
							sb.AppendFormat("\n   [DXS] => ({0}):{1}", propertyEntry.DxStoreValueKind, propertyEntry.DxStoreValue);
						}
					}
				}
			}

			// Token: 0x040005FD RID: 1533
			private bool isAnalyzed;
		}
	}
}
