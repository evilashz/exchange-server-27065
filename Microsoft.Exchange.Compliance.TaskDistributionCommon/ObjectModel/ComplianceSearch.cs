using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.Extensibility;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.Protocol;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.Protocol.EDiscovery;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.Serialization;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Compliance.TaskDistributionCommon.ObjectModel
{
	// Token: 0x02000029 RID: 41
	[Serializable]
	public class ComplianceSearch : ComplianceJob
	{
		// Token: 0x060000FC RID: 252 RVA: 0x000067EC File Offset: 0x000049EC
		public ComplianceSearch()
		{
			base.JobType = ComplianceJobType.ComplianceSearch;
			this.StartDate = new DateTime?(ComplianceJobConstants.MinComplianceTime);
			this.EndDate = new DateTime?(ComplianceJobConstants.MinComplianceTime);
			this.Language = CultureInfo.InvariantCulture;
			this.SearchType = ComplianceSearch.ComplianceSearchType.EstimateSearch;
			this.LogLevel = ComplianceJobLogLevel.Suppressed;
			this.successfulResults = new List<SearchResult.TargetSearchResult>();
			this.failedResults = new List<SearchResult.TargetSearchResult>();
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000FD RID: 253 RVA: 0x00006855 File Offset: 0x00004A55
		// (set) Token: 0x060000FE RID: 254 RVA: 0x00006867 File Offset: 0x00004A67
		public CultureInfo Language
		{
			get
			{
				return (CultureInfo)this[ComplianceSearchSchema.Language];
			}
			internal set
			{
				this[ComplianceSearchSchema.Language] = value;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000FF RID: 255 RVA: 0x00006875 File Offset: 0x00004A75
		// (set) Token: 0x06000100 RID: 256 RVA: 0x00006887 File Offset: 0x00004A87
		public MultiValuedProperty<string> StatusMailRecipients
		{
			get
			{
				return (MultiValuedProperty<string>)this[ComplianceSearchSchema.StatusMailRecipients];
			}
			internal set
			{
				this[ComplianceSearchSchema.StatusMailRecipients] = value;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000101 RID: 257 RVA: 0x00006895 File Offset: 0x00004A95
		// (set) Token: 0x06000102 RID: 258 RVA: 0x000068A7 File Offset: 0x00004AA7
		public ComplianceJobLogLevel LogLevel
		{
			get
			{
				return (ComplianceJobLogLevel)this[ComplianceSearchSchema.LogLevel];
			}
			set
			{
				this[ComplianceSearchSchema.LogLevel] = value;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000103 RID: 259 RVA: 0x000068BA File Offset: 0x00004ABA
		// (set) Token: 0x06000104 RID: 260 RVA: 0x000068CC File Offset: 0x00004ACC
		public bool IncludeUnindexedItems
		{
			get
			{
				return (bool)this[ComplianceSearchSchema.IncludeUnindexedItems];
			}
			internal set
			{
				this[ComplianceSearchSchema.IncludeUnindexedItems] = value;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000105 RID: 261 RVA: 0x000068DF File Offset: 0x00004ADF
		// (set) Token: 0x06000106 RID: 262 RVA: 0x000068F1 File Offset: 0x00004AF1
		public string KeywordQuery
		{
			get
			{
				return (string)this[ComplianceSearchSchema.KeywordQuery];
			}
			internal set
			{
				this[ComplianceSearchSchema.KeywordQuery] = value;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000107 RID: 263 RVA: 0x000068FF File Offset: 0x00004AFF
		// (set) Token: 0x06000108 RID: 264 RVA: 0x00006911 File Offset: 0x00004B11
		public DateTime? StartDate
		{
			get
			{
				return (DateTime?)this[ComplianceSearchSchema.StartDate];
			}
			internal set
			{
				this[ComplianceSearchSchema.StartDate] = value;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000109 RID: 265 RVA: 0x00006924 File Offset: 0x00004B24
		// (set) Token: 0x0600010A RID: 266 RVA: 0x00006938 File Offset: 0x00004B38
		public DateTime? EndDate
		{
			get
			{
				return (DateTime?)this[ComplianceSearchSchema.EndDate];
			}
			internal set
			{
				if (value != null && value.Value.Hour == 0 && value.Value.Minute == 0 && value.Value.Second == 0)
				{
					this[ComplianceSearchSchema.EndDate] = new DateTime(value.Value.Year, value.Value.Month, value.Value.Day, DateTime.MaxValue.Hour, DateTime.MaxValue.Minute, DateTime.MaxValue.Second);
					return;
				}
				this[ComplianceSearchSchema.EndDate] = value;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600010B RID: 267 RVA: 0x00006A0A File Offset: 0x00004C0A
		// (set) Token: 0x0600010C RID: 268 RVA: 0x00006A1C File Offset: 0x00004C1C
		public ComplianceSearch.ComplianceSearchType SearchType
		{
			get
			{
				return (ComplianceSearch.ComplianceSearchType)this[ComplianceSearchSchema.SearchType];
			}
			internal set
			{
				this[ComplianceSearchSchema.SearchType] = value;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600010D RID: 269 RVA: 0x00006A2F File Offset: 0x00004C2F
		// (set) Token: 0x0600010E RID: 270 RVA: 0x00006A37 File Offset: 0x00004C37
		public long ResultNumber { get; private set; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600010F RID: 271 RVA: 0x00006A40 File Offset: 0x00004C40
		// (set) Token: 0x06000110 RID: 272 RVA: 0x00006A48 File Offset: 0x00004C48
		public long ResultSize { get; private set; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000111 RID: 273 RVA: 0x00006A51 File Offset: 0x00004C51
		public string SuccessResults
		{
			get
			{
				return this.GetPerBindingResultsString(this.successfulResults);
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000112 RID: 274 RVA: 0x00006A5F File Offset: 0x00004C5F
		public string FailedResults
		{
			get
			{
				return this.GetPerBindingResultsString(this.failedResults);
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000113 RID: 275 RVA: 0x00006A6D File Offset: 0x00004C6D
		// (set) Token: 0x06000114 RID: 276 RVA: 0x00006A75 File Offset: 0x00004C75
		public string Errors { get; set; }

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000115 RID: 277 RVA: 0x00006A7E File Offset: 0x00004C7E
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return ComplianceSearch.schema;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000116 RID: 278 RVA: 0x00006A88 File Offset: 0x00004C88
		// (set) Token: 0x06000117 RID: 279 RVA: 0x00006B24 File Offset: 0x00004D24
		internal override byte[] JobData
		{
			get
			{
				ComplianceSearchData complianceSearchData = new ComplianceSearchData
				{
					SearchType = this.SearchType,
					KeywordQuery = this.KeywordQuery,
					StatusMailRecipients = this.StatusMailRecipients.ToArray(),
					LogLevel = this.LogLevel,
					SearchOptions = this.GenerateSearchOptions(),
					SearchConditions = this.GenerateSearchConditions()
				};
				complianceSearchData.Language = this.Language.Name;
				if (string.IsNullOrEmpty(complianceSearchData.Language))
				{
					complianceSearchData.Language = this.Language.TwoLetterISOLanguageName;
				}
				return ComplianceSerializer.Serialize<ComplianceSearchData>(ComplianceSearchData.Description, complianceSearchData);
			}
			set
			{
				ComplianceSearchData complianceSearchData = ComplianceSerializer.DeSerialize<ComplianceSearchData>(ComplianceSearchData.Description, value);
				this.SearchType = complianceSearchData.SearchType;
				this.KeywordQuery = complianceSearchData.KeywordQuery;
				this.StatusMailRecipients = complianceSearchData.StatusMailRecipients;
				this.LogLevel = complianceSearchData.LogLevel;
				if (complianceSearchData.Language.Equals(CultureInfo.InvariantCulture.TwoLetterISOLanguageName))
				{
					this.Language = CultureInfo.InvariantCulture;
				}
				else
				{
					this.Language = new CultureInfo(complianceSearchData.Language);
				}
				this.ParseSearchOptions(complianceSearchData.SearchOptions);
				this.ParseSearchCondtions(complianceSearchData.SearchConditions);
			}
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00006BC0 File Offset: 0x00004DC0
		internal byte[] GetExchangeWorkDefinition()
		{
			SearchWorkDefinition searchWorkDefinition = new SearchWorkDefinition();
			searchWorkDefinition.Parser = SearchWorkDefinition.QueryParser.KQL;
			searchWorkDefinition.DetailCount = 500;
			searchWorkDefinition.Query = this.ToKqlQuery();
			WorkPayload workPayload = new WorkPayload();
			workPayload.WorkDefinition = ComplianceSerializer.Serialize<SearchWorkDefinition>(SearchWorkDefinition.Description, searchWorkDefinition);
			workPayload.WorkDefinitionType = WorkDefinitionType.EDiscovery;
			return ComplianceSerializer.Serialize<WorkPayload>(WorkPayload.Description, workPayload);
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00006C1C File Offset: 0x00004E1C
		internal override void UpdateJobResults()
		{
			long num = 0L;
			long num2 = 0L;
			foreach (KeyValuePair<ComplianceBindingType, ComplianceBinding> keyValuePair in base.Bindings)
			{
				WorkPayload workPayload;
				FaultDefinition faultDefinition;
				if (keyValuePair.Value.JobResults != null && keyValuePair.Value.JobResults.Length != 0 && ComplianceSerializer.TryDeserialize<WorkPayload>(WorkPayload.Description, keyValuePair.Value.JobResults, out workPayload, out faultDefinition, "UpdateJobResults", "f:\\15.00.1497\\sources\\dev\\EDiscovery\\src\\TaskDistributionSystem\\TaskDistributionCommon\\ObjectModel\\ComplianceSearch.cs", 325))
				{
					switch (workPayload.WorkDefinitionType)
					{
					case WorkDefinitionType.EDiscovery:
					{
						SearchResult searchResult = ComplianceSerializer.DeSerialize<SearchResult>(SearchResult.Description, workPayload.WorkDefinition);
						if (searchResult != null)
						{
							num += searchResult.TotalCount;
							num2 += searchResult.TotalSize;
							if (searchResult.Results != null && searchResult.Results.Count != 0)
							{
								foreach (SearchResult.TargetSearchResult item in searchResult.Results)
								{
									this.successfulResults.Add(item);
								}
							}
						}
						break;
					}
					case WorkDefinitionType.Fault:
						this.UpdateFailureResults(workPayload);
						break;
					}
				}
			}
			this.ResultNumber = num;
			this.ResultSize = num2;
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00006DA0 File Offset: 0x00004FA0
		internal string ToKqlQuery()
		{
			string keywordQuery = this.KeywordQuery;
			string additionalFilter = this.GetAdditionalFilter();
			if (string.IsNullOrEmpty(additionalFilter))
			{
				if (string.IsNullOrEmpty(keywordQuery))
				{
					return "size>=0";
				}
				return keywordQuery;
			}
			else
			{
				if (string.IsNullOrEmpty(keywordQuery))
				{
					return additionalFilter;
				}
				new StringBuilder();
				if (keywordQuery.IndexOf(" OR ", StringComparison.OrdinalIgnoreCase) != -1)
				{
					return string.Format("({0}) {1}", keywordQuery, additionalFilter);
				}
				return string.Format("{0} {1}", keywordQuery, additionalFilter);
			}
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00006E0C File Offset: 0x0000500C
		private void UpdateFailureResults(WorkPayload workPayload)
		{
			FaultDefinition results;
			FaultDefinition faultDefinition;
			if (ComplianceSerializer.TryDeserialize<FaultDefinition>(FaultDefinition.Description, workPayload.WorkDefinition, out results, out faultDefinition, "UpdateFailureResults", "f:\\15.00.1497\\sources\\dev\\EDiscovery\\src\\TaskDistributionSystem\\TaskDistributionCommon\\ObjectModel\\ComplianceSearch.cs", 429))
			{
				this.UpdateErrors(results);
			}
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00006E48 File Offset: 0x00005048
		private void UpdateErrors(ResultBase results)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (results != null && results.Faults != null)
			{
				foreach (FaultRecord faultRecord in results.Faults)
				{
					if (faultRecord.Data != null)
					{
						foreach (KeyValuePair<string, string> keyValuePair in faultRecord.Data)
						{
							string key;
							if ((key = keyValuePair.Key) != null && key == "UM")
							{
								stringBuilder.Append(keyValuePair.Value);
							}
						}
					}
				}
			}
			if (stringBuilder.Length > 0)
			{
				this.Errors = stringBuilder.ToString();
			}
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00006F24 File Offset: 0x00005124
		private string GetAdditionalFilter()
		{
			DateTime? startDate = this.StartDate;
			DateTime? endDate = this.EndDate;
			if ((startDate == null || startDate == null) && (endDate == null || endDate == null))
			{
				return null;
			}
			string text = string.Empty;
			if (startDate != null && startDate != null)
			{
				text = string.Format("received>=\"{0}\"", startDate.Value.ToString(this.Language));
			}
			if (endDate != null && endDate != null)
			{
				string text2 = string.Format("received<=\"{0}\"", endDate.Value.ToString(this.Language));
				if (!string.IsNullOrEmpty(text))
				{
					text = string.Format("({0} AND {1})", text, text2);
				}
				else
				{
					text = text2;
				}
			}
			return text;
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00006FEC File Offset: 0x000051EC
		private string GetPerBindingResultsString(List<SearchResult.TargetSearchResult> results)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("{");
			bool flag = true;
			foreach (SearchResult.TargetSearchResult targetSearchResult in results)
			{
				if (flag)
				{
					flag = false;
				}
				else
				{
					stringBuilder.Append(",\r\n ");
				}
				stringBuilder.Append(targetSearchResult.ToString());
			}
			stringBuilder.Append("}");
			return stringBuilder.ToString();
		}

		// Token: 0x0600011F RID: 287 RVA: 0x0000707C File Offset: 0x0000527C
		private string GenerateSearchOptions()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (this.IncludeUnindexedItems)
			{
				stringBuilder.Append("IncludeUnindexedItems");
				stringBuilder.Append(":");
				stringBuilder.Append("true");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000120 RID: 288 RVA: 0x000070C4 File Offset: 0x000052C4
		private void ParseSearchOptions(string searchOptions)
		{
			if (string.IsNullOrEmpty(searchOptions))
			{
				return;
			}
			string[] array = searchOptions.Split(new char[]
			{
				','
			}, StringSplitOptions.RemoveEmptyEntries);
			if (searchOptions == null || searchOptions.Length == 0)
			{
				return;
			}
			foreach (string text in array)
			{
				string[] array3 = text.Split(new char[]
				{
					':'
				}, StringSplitOptions.RemoveEmptyEntries);
				string a;
				if (array3 != null && array3.Length == 2 && (a = array3[0]) != null && a == "IncludeUnindexedItems")
				{
					this.IncludeUnindexedItems = array3[1].Equals("true");
				}
			}
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00007164 File Offset: 0x00005364
		private List<byte[]> GenerateSearchConditions()
		{
			List<byte[]> list = new List<byte[]>();
			if (this.StartDate != null)
			{
				ComplianceSearchCondition complianceSearchCondition = new ComplianceSearchCondition(ComplianceSearchCondition.ConditionName.StartDate, this.StartDate.Value.Ticks.ToString());
				list.Add(complianceSearchCondition.ToBlob());
			}
			if (this.EndDate != null)
			{
				ComplianceSearchCondition complianceSearchCondition2 = new ComplianceSearchCondition(ComplianceSearchCondition.ConditionName.EndDate, this.EndDate.Value.Ticks.ToString());
				list.Add(complianceSearchCondition2.ToBlob());
			}
			return list;
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00007204 File Offset: 0x00005404
		private void ParseSearchCondtions(List<byte[]> conditions)
		{
			if (conditions == null || conditions.Count == 0)
			{
				return;
			}
			foreach (byte[] blob in conditions)
			{
				ComplianceSearchCondition complianceSearchCondition = ComplianceSerializer.DeSerialize<ComplianceSearchCondition>(ComplianceSearchCondition.Description, blob);
				if (complianceSearchCondition != null)
				{
					switch (complianceSearchCondition.Name)
					{
					case ComplianceSearchCondition.ConditionName.StartDate:
					{
						DateTime value;
						if (this.TryParseDateString(complianceSearchCondition.Content, out value))
						{
							this.StartDate = new DateTime?(value);
						}
						break;
					}
					case ComplianceSearchCondition.ConditionName.EndDate:
					{
						DateTime value;
						if (this.TryParseDateString(complianceSearchCondition.Content, out value))
						{
							this.EndDate = new DateTime?(value);
						}
						break;
					}
					}
				}
			}
		}

		// Token: 0x06000123 RID: 291 RVA: 0x000072BC File Offset: 0x000054BC
		private bool TryParseDateString(string ticksStr, out DateTime date)
		{
			date = default(DateTime);
			long ticks;
			if (long.TryParse(ticksStr, out ticks))
			{
				date = new DateTime(ticks, DateTimeKind.Utc);
				return true;
			}
			return false;
		}

		// Token: 0x04000095 RID: 149
		private const string OptionKeyIncludeUnindexedItems = "IncludeUnindexedItems";

		// Token: 0x04000096 RID: 150
		private const string OptionValueTrue = "true";

		// Token: 0x04000097 RID: 151
		private static readonly ComplianceSearchSchema schema = ObjectSchema.GetInstance<ComplianceSearchSchema>();

		// Token: 0x04000098 RID: 152
		private List<SearchResult.TargetSearchResult> successfulResults;

		// Token: 0x04000099 RID: 153
		private List<SearchResult.TargetSearchResult> failedResults;

		// Token: 0x0200002A RID: 42
		public enum ComplianceSearchType : byte
		{
			// Token: 0x0400009E RID: 158
			UnknownType,
			// Token: 0x0400009F RID: 159
			EstimateSearch
		}
	}
}
