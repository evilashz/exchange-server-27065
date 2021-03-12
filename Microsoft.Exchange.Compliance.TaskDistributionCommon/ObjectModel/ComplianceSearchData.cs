using System;
using System.Collections.Generic;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Compliance.TaskDistributionCommon.ObjectModel
{
	// Token: 0x0200002E RID: 46
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class ComplianceSearchData
	{
		// Token: 0x06000134 RID: 308 RVA: 0x00007670 File Offset: 0x00005870
		static ComplianceSearchData()
		{
			ComplianceSearchData.description.ComplianceStructureId = 9;
			ComplianceSearchData.description.RegisterBytePropertyGetterAndSetter(0, (ComplianceSearchData item) => (byte)item.Version, delegate(ComplianceSearchData item, byte value)
			{
				item.Version = (ComplianceSearchData.ComplianceSearchObjectVersion)value;
			});
			ComplianceSearchData.description.RegisterBytePropertyGetterAndSetter(1, (ComplianceSearchData item) => (byte)item.SearchType, delegate(ComplianceSearchData item, byte value)
			{
				item.SearchType = (ComplianceSearch.ComplianceSearchType)value;
			});
			ComplianceSearchData.description.RegisterBytePropertyGetterAndSetter(2, (ComplianceSearchData item) => (byte)item.LogLevel, delegate(ComplianceSearchData item, byte value)
			{
				item.LogLevel = (ComplianceJobLogLevel)value;
			});
			ComplianceSearchData.description.RegisterStringPropertyGetterAndSetter(0, (ComplianceSearchData item) => item.Language, delegate(ComplianceSearchData item, string value)
			{
				item.Language = value;
			});
			ComplianceSearchData.description.RegisterStringPropertyGetterAndSetter(1, (ComplianceSearchData item) => item.KeywordQuery, delegate(ComplianceSearchData item, string value)
			{
				item.KeywordQuery = value;
			});
			ComplianceSearchData.description.RegisterStringPropertyGetterAndSetter(2, (ComplianceSearchData item) => item.SearchOptions, delegate(ComplianceSearchData item, string value)
			{
				item.SearchOptions = value;
			});
			ComplianceSearchData.description.RegisterCollectionPropertyAccessors(0, () => CollectionItemType.String, (ComplianceSearchData item) => item.StatusMailRecipients.Length, (ComplianceSearchData item, int index) => item.StatusMailRecipients[index], delegate(ComplianceSearchData item, object obj, int index)
			{
				item.StatusMailRecipients[index] = (string)obj;
			}, delegate(ComplianceSearchData item, int count)
			{
				item.StatusMailRecipients = new string[count];
			});
			ComplianceSearchData.description.RegisterCollectionPropertyAccessors(1, () => CollectionItemType.Blob, (ComplianceSearchData item) => item.SearchConditions.Count, (ComplianceSearchData item, int index) => item.SearchConditions[index], delegate(ComplianceSearchData item, object obj, int index)
			{
				item.SearchConditions.Add((byte[])obj);
			}, delegate(ComplianceSearchData item, int count)
			{
				item.SearchConditions = new List<byte[]>();
			});
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00007969 File Offset: 0x00005B69
		public ComplianceSearchData()
		{
			this.Version = ComplianceSearchData.ComplianceSearchObjectVersion.Version1;
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000136 RID: 310 RVA: 0x00007978 File Offset: 0x00005B78
		public static ComplianceSerializationDescription<ComplianceSearchData> Description
		{
			get
			{
				return ComplianceSearchData.description;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000137 RID: 311 RVA: 0x0000797F File Offset: 0x00005B7F
		// (set) Token: 0x06000138 RID: 312 RVA: 0x00007987 File Offset: 0x00005B87
		public ComplianceSearchData.ComplianceSearchObjectVersion Version { get; set; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000139 RID: 313 RVA: 0x00007990 File Offset: 0x00005B90
		// (set) Token: 0x0600013A RID: 314 RVA: 0x00007998 File Offset: 0x00005B98
		public string KeywordQuery { get; set; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600013B RID: 315 RVA: 0x000079A1 File Offset: 0x00005BA1
		// (set) Token: 0x0600013C RID: 316 RVA: 0x000079A9 File Offset: 0x00005BA9
		public List<byte[]> SearchConditions { get; set; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600013D RID: 317 RVA: 0x000079B2 File Offset: 0x00005BB2
		// (set) Token: 0x0600013E RID: 318 RVA: 0x000079BA File Offset: 0x00005BBA
		public ComplianceSearch.ComplianceSearchType SearchType { get; set; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600013F RID: 319 RVA: 0x000079C3 File Offset: 0x00005BC3
		// (set) Token: 0x06000140 RID: 320 RVA: 0x000079CB File Offset: 0x00005BCB
		public string Language { get; set; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000141 RID: 321 RVA: 0x000079D4 File Offset: 0x00005BD4
		// (set) Token: 0x06000142 RID: 322 RVA: 0x000079DC File Offset: 0x00005BDC
		public string[] StatusMailRecipients { get; set; }

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000143 RID: 323 RVA: 0x000079E5 File Offset: 0x00005BE5
		// (set) Token: 0x06000144 RID: 324 RVA: 0x000079ED File Offset: 0x00005BED
		public ComplianceJobLogLevel LogLevel { get; set; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000145 RID: 325 RVA: 0x000079F6 File Offset: 0x00005BF6
		// (set) Token: 0x06000146 RID: 326 RVA: 0x000079FE File Offset: 0x00005BFE
		public string SearchOptions { get; set; }

		// Token: 0x040000B3 RID: 179
		private static ComplianceSerializationDescription<ComplianceSearchData> description = new ComplianceSerializationDescription<ComplianceSearchData>();

		// Token: 0x0200002F RID: 47
		internal enum ComplianceSearchObjectVersion : byte
		{
			// Token: 0x040000D3 RID: 211
			VersionUnknown,
			// Token: 0x040000D4 RID: 212
			Version1
		}
	}
}
