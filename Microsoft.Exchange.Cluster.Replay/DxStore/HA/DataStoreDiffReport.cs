using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Exchange.DxStore.HA
{
	// Token: 0x02000166 RID: 358
	public class DataStoreDiffReport
	{
		// Token: 0x06000E44 RID: 3652 RVA: 0x0003DC29 File Offset: 0x0003BE29
		public DataStoreDiffReport()
		{
			this.KeysOnlyInClusdb = new List<DataStoreMergedContainer.KeyEntry>();
			this.KeysOnlyInDxStore = new List<DataStoreMergedContainer.KeyEntry>();
			this.KeysInBothAndPropertiesMatch = new List<DataStoreMergedContainer.KeyEntry>();
			this.KeysInBothButPropertiesMismatch = new List<DataStoreMergedContainer.KeyEntry>();
		}

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06000E45 RID: 3653 RVA: 0x0003DC5D File Offset: 0x0003BE5D
		// (set) Token: 0x06000E46 RID: 3654 RVA: 0x0003DC65 File Offset: 0x0003BE65
		public List<DataStoreMergedContainer.KeyEntry> KeysOnlyInClusdb { get; set; }

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06000E47 RID: 3655 RVA: 0x0003DC6E File Offset: 0x0003BE6E
		// (set) Token: 0x06000E48 RID: 3656 RVA: 0x0003DC76 File Offset: 0x0003BE76
		public List<DataStoreMergedContainer.KeyEntry> KeysOnlyInDxStore { get; set; }

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06000E49 RID: 3657 RVA: 0x0003DC7F File Offset: 0x0003BE7F
		// (set) Token: 0x06000E4A RID: 3658 RVA: 0x0003DC87 File Offset: 0x0003BE87
		public List<DataStoreMergedContainer.KeyEntry> KeysInBothAndPropertiesMatch { get; set; }

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06000E4B RID: 3659 RVA: 0x0003DC90 File Offset: 0x0003BE90
		// (set) Token: 0x06000E4C RID: 3660 RVA: 0x0003DC98 File Offset: 0x0003BE98
		public List<DataStoreMergedContainer.KeyEntry> KeysInBothButPropertiesMismatch { get; set; }

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06000E4D RID: 3661 RVA: 0x0003DCA1 File Offset: 0x0003BEA1
		public bool IsEverythingMatches
		{
			get
			{
				return this.TotalKeysCount == this.CountKeysInClusdbAndDxStoreAndPropertiesMatch;
			}
		}

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06000E4E RID: 3662 RVA: 0x0003DCB1 File Offset: 0x0003BEB1
		public int TotalKeysCount
		{
			get
			{
				return this.CountKeysOnlyInClusdb + this.CountKeysOnlyInDxStore + this.CountKeysInClusdbAndDxStoreAndPropertiesMatch + this.CountKeysInClusdbAndDxStoreButPropertiesDifferent;
			}
		}

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06000E4F RID: 3663 RVA: 0x0003DCCE File Offset: 0x0003BECE
		public int TotalPropertiesCount
		{
			get
			{
				return this.CountPropertiesOnlyInClusdb + this.CountPropertiesOnlyInDxStore + this.CountPropertiesSameInClusdbAndDxStore + this.CountPropertiesDifferentInClusdbAndDxStore;
			}
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06000E50 RID: 3664 RVA: 0x0003DCEB File Offset: 0x0003BEEB
		public int TotalClusdbKeysCount
		{
			get
			{
				return this.CountKeysOnlyInClusdb + this.CountKeysInClusdbAndDxStoreAndPropertiesMatch + this.CountKeysInClusdbAndDxStoreButPropertiesDifferent;
			}
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06000E51 RID: 3665 RVA: 0x0003DD01 File Offset: 0x0003BF01
		public int TotalClusdbPropertiesCount
		{
			get
			{
				return this.CountPropertiesOnlyInClusdb + this.CountPropertiesSameInClusdbAndDxStore + this.CountPropertiesDifferentInClusdbAndDxStore;
			}
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06000E52 RID: 3666 RVA: 0x0003DD17 File Offset: 0x0003BF17
		public int TotalDxStoreKeysCount
		{
			get
			{
				return this.CountKeysOnlyInDxStore + this.CountKeysInClusdbAndDxStoreAndPropertiesMatch + this.CountKeysInClusdbAndDxStoreButPropertiesDifferent;
			}
		}

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06000E53 RID: 3667 RVA: 0x0003DD2D File Offset: 0x0003BF2D
		public int TotalDxStorePropertiesCount
		{
			get
			{
				return this.CountPropertiesOnlyInDxStore + this.CountPropertiesSameInClusdbAndDxStore + this.CountPropertiesDifferentInClusdbAndDxStore;
			}
		}

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06000E54 RID: 3668 RVA: 0x0003DD43 File Offset: 0x0003BF43
		public int CountKeysOnlyInClusdb
		{
			get
			{
				if (this.KeysOnlyInClusdb == null)
				{
					return 0;
				}
				return this.KeysOnlyInClusdb.Count;
			}
		}

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06000E55 RID: 3669 RVA: 0x0003DD5A File Offset: 0x0003BF5A
		public int CountKeysOnlyInDxStore
		{
			get
			{
				if (this.KeysOnlyInDxStore == null)
				{
					return 0;
				}
				return this.KeysOnlyInDxStore.Count;
			}
		}

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06000E56 RID: 3670 RVA: 0x0003DD71 File Offset: 0x0003BF71
		public int CountKeysInClusdbAndDxStoreAndPropertiesMatch
		{
			get
			{
				if (this.KeysInBothAndPropertiesMatch == null)
				{
					return 0;
				}
				return this.KeysInBothAndPropertiesMatch.Count;
			}
		}

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06000E57 RID: 3671 RVA: 0x0003DD88 File Offset: 0x0003BF88
		public int CountKeysInClusdbAndDxStoreButPropertiesDifferent
		{
			get
			{
				if (this.KeysInBothButPropertiesMismatch == null)
				{
					return 0;
				}
				return this.KeysInBothButPropertiesMismatch.Count;
			}
		}

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06000E58 RID: 3672 RVA: 0x0003DD9F File Offset: 0x0003BF9F
		// (set) Token: 0x06000E59 RID: 3673 RVA: 0x0003DDA7 File Offset: 0x0003BFA7
		public int CountPropertiesOnlyInClusdb { get; set; }

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06000E5A RID: 3674 RVA: 0x0003DDB0 File Offset: 0x0003BFB0
		// (set) Token: 0x06000E5B RID: 3675 RVA: 0x0003DDB8 File Offset: 0x0003BFB8
		public int CountPropertiesOnlyInDxStore { get; set; }

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06000E5C RID: 3676 RVA: 0x0003DDC1 File Offset: 0x0003BFC1
		// (set) Token: 0x06000E5D RID: 3677 RVA: 0x0003DDC9 File Offset: 0x0003BFC9
		public int CountPropertiesSameInClusdbAndDxStore { get; set; }

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06000E5E RID: 3678 RVA: 0x0003DDD2 File Offset: 0x0003BFD2
		// (set) Token: 0x06000E5F RID: 3679 RVA: 0x0003DDDA File Offset: 0x0003BFDA
		public int CountPropertiesDifferentInClusdbAndDxStore { get; set; }

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06000E60 RID: 3680 RVA: 0x0003DDE3 File Offset: 0x0003BFE3
		// (set) Token: 0x06000E61 RID: 3681 RVA: 0x0003DDEB File Offset: 0x0003BFEB
		public string VerboseReport { get; set; }

		// Token: 0x06000E62 RID: 3682 RVA: 0x0003DDF4 File Offset: 0x0003BFF4
		public static DataStoreDiffReport Create(IEnumerable<DataStoreMergedContainer.KeyEntry> keyEntries, DiffReportVerboseMode diffReportVerboseMode)
		{
			DataStoreDiffReport dataStoreDiffReport = new DataStoreDiffReport();
			foreach (DataStoreMergedContainer.KeyEntry keyEntry in keyEntries)
			{
				if (keyEntry.IsPresentOnlyInClusdb)
				{
					dataStoreDiffReport.KeysOnlyInClusdb.Add(keyEntry);
				}
				else if (keyEntry.IsPresentOnlyInDxStore)
				{
					dataStoreDiffReport.KeysOnlyInDxStore.Add(keyEntry);
				}
				else if (keyEntry.IsPropertiesMatch)
				{
					dataStoreDiffReport.KeysInBothAndPropertiesMatch.Add(keyEntry);
				}
				else
				{
					dataStoreDiffReport.KeysInBothButPropertiesMismatch.Add(keyEntry);
				}
				dataStoreDiffReport.CountPropertiesOnlyInClusdb += keyEntry.PropertiesOnlyInClusdbCount;
				dataStoreDiffReport.CountPropertiesOnlyInDxStore += keyEntry.PropertiesOnlyInDxStoreCount;
				dataStoreDiffReport.CountPropertiesSameInClusdbAndDxStore += keyEntry.PropertyMatchCount;
				dataStoreDiffReport.CountPropertiesDifferentInClusdbAndDxStore += keyEntry.PropertyDifferentCount;
			}
			dataStoreDiffReport.VerboseReport = dataStoreDiffReport.GenerateVerboseReport(diffReportVerboseMode);
			return dataStoreDiffReport;
		}

		// Token: 0x06000E63 RID: 3683 RVA: 0x0003DEEC File Offset: 0x0003C0EC
		public void DumpKeys(StringBuilder sb, string title, List<DataStoreMergedContainer.KeyEntry> keyEntries, DiffReportVerboseMode diffReportVerboseMode)
		{
			if (keyEntries != null && keyEntries.Count > 0)
			{
				sb.AppendLine(title);
				sb.AppendLine(new string('=', title.Length));
				foreach (DataStoreMergedContainer.KeyEntry keyEntry in keyEntries)
				{
					keyEntry.DumpStats(sb);
					if (diffReportVerboseMode.HasFlag(DiffReportVerboseMode.ShowPropertyNames))
					{
						keyEntry.DumpProperties(sb, diffReportVerboseMode.HasFlag(DiffReportVerboseMode.ShowPropertyValues));
					}
				}
				sb.AppendLine();
			}
		}

		// Token: 0x06000E64 RID: 3684 RVA: 0x0003DF9C File Offset: 0x0003C19C
		public string GenerateVerboseReport(DiffReportVerboseMode diffReportVerboseMode)
		{
			if (diffReportVerboseMode.HasFlag(DiffReportVerboseMode.Disabled))
			{
				return "*** Verbose reporting mode is disabled ***";
			}
			StringBuilder stringBuilder = new StringBuilder(1024);
			this.DumpKeys(stringBuilder, "Keys only present in ClusDb", this.KeysOnlyInClusdb, diffReportVerboseMode);
			this.DumpKeys(stringBuilder, "Keys only present in DxStore", this.KeysOnlyInDxStore, diffReportVerboseMode);
			this.DumpKeys(stringBuilder, "Keys present on both but properties not matching", this.KeysInBothButPropertiesMismatch, diffReportVerboseMode);
			if (diffReportVerboseMode.HasFlag(DiffReportVerboseMode.ShowMatchingKeys))
			{
				this.DumpKeys(stringBuilder, "Keys present on both and properties matching", this.KeysInBothAndPropertiesMatch, diffReportVerboseMode);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040005E7 RID: 1511
		public const int MaxCharsPerLine = 15360;
	}
}
