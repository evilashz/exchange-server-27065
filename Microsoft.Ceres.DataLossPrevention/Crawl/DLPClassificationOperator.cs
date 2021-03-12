using System;
using System.Collections.Generic;
using Microsoft.Ceres.Evaluation.Operators;
using Microsoft.Ceres.Evaluation.Operators.Graphs;
using Microsoft.Ceres.Evaluation.Operators.PlugIns;
using Microsoft.Ceres.Evaluation.Operators.Utilities;
using Microsoft.Office.CompliancePolicy.Classification;

namespace Microsoft.Ceres.DataLossPrevention.Crawl
{
	// Token: 0x0200000D RID: 13
	[Operator("DLPClassification")]
	[Serializable]
	internal class DLPClassificationOperator : TypedOperatorBase<DLPClassificationOperator>, ITransformationBasedOperator
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00003863 File Offset: 0x00001A63
		// (set) Token: 0x06000056 RID: 86 RVA: 0x0000386B File Offset: 0x00001A6B
		public TransformationSpecification Specification { get; private set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00003874 File Offset: 0x00001A74
		// (set) Token: 0x06000058 RID: 88 RVA: 0x0000387C File Offset: 0x00001A7C
		[Property(Name = "maxRulePackageCacheCount")]
		public int MaxRulePackageCacheCount
		{
			get
			{
				return this.maxRulePackageCacheCount;
			}
			set
			{
				this.maxRulePackageCacheCount = value;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00003885 File Offset: 0x00001A85
		// (set) Token: 0x0600005A RID: 90 RVA: 0x0000388D File Offset: 0x00001A8D
		[Property(Name = "maxRulePackageCacheStorage")]
		public int MaxRulePackageCacheStorage
		{
			get
			{
				return this.maxRulePackageCacheStorage;
			}
			set
			{
				this.maxRulePackageCacheStorage = value;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00003896 File Offset: 0x00001A96
		// (set) Token: 0x0600005C RID: 92 RVA: 0x0000389E File Offset: 0x00001A9E
		[Property(Name = "performanceMonitorPollPeriod")]
		public int PerformanceMonitorPollPeriod
		{
			get
			{
				return this.performanceMonitorPollPeriod;
			}
			set
			{
				this.performanceMonitorPollPeriod = value;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600005D RID: 93 RVA: 0x000038A7 File Offset: 0x00001AA7
		// (set) Token: 0x0600005E RID: 94 RVA: 0x000038AF File Offset: 0x00001AAF
		[Property(Name = "rulePackageCachePollPeriod")]
		public int RulePackageCachePollPeriod
		{
			get
			{
				return this.rulePackageCachePollPeriod;
			}
			set
			{
				this.rulePackageCachePollPeriod = value;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600005F RID: 95 RVA: 0x000038B8 File Offset: 0x00001AB8
		// (set) Token: 0x06000060 RID: 96 RVA: 0x000038C0 File Offset: 0x00001AC0
		[Property(Name = "rulePackageCacheRetrievalWaitTime")]
		public int RulePackageCacheRetrievalWaitTime
		{
			get
			{
				return this.rulePackageCacheRetrievalWaitTime;
			}
			set
			{
				this.rulePackageCacheRetrievalWaitTime = value;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000061 RID: 97 RVA: 0x000038C9 File Offset: 0x00001AC9
		// (set) Token: 0x06000062 RID: 98 RVA: 0x000038D1 File Offset: 0x00001AD1
		[Property(Name = "useLazyRegexCompilation")]
		public bool UseLazyRegexCompilation
		{
			get
			{
				return this.useLazyRegexCompilation;
			}
			set
			{
				this.useLazyRegexCompilation = value;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000063 RID: 99 RVA: 0x000038DA File Offset: 0x00001ADA
		// (set) Token: 0x06000064 RID: 100 RVA: 0x000038E2 File Offset: 0x00001AE2
		[Property(Name = "useMemoryToImprovePerformance")]
		public bool UseMemoryToImprovePerformance
		{
			get
			{
				return this.useMemoryToImprovePerformance;
			}
			set
			{
				this.useMemoryToImprovePerformance = value;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000065 RID: 101 RVA: 0x000038EB File Offset: 0x00001AEB
		// (set) Token: 0x06000066 RID: 102 RVA: 0x000038F3 File Offset: 0x00001AF3
		[Property(Name = "lastScanProperty")]
		public string LastScanProperty
		{
			get
			{
				return this.lastScanProperty;
			}
			set
			{
				this.lastScanProperty = value;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000067 RID: 103 RVA: 0x000038FC File Offset: 0x00001AFC
		// (set) Token: 0x06000068 RID: 104 RVA: 0x00003904 File Offset: 0x00001B04
		[Property(Name = "countProperty")]
		public string CountProperty
		{
			get
			{
				return this.countProperty;
			}
			set
			{
				this.countProperty = value;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000069 RID: 105 RVA: 0x0000390D File Offset: 0x00001B0D
		// (set) Token: 0x0600006A RID: 106 RVA: 0x00003915 File Offset: 0x00001B15
		[Property(Name = "confidenceProperty")]
		public string ConfidenceProperty
		{
			get
			{
				return this.confidenceProperty;
			}
			set
			{
				this.confidenceProperty = value;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600006B RID: 107 RVA: 0x0000391E File Offset: 0x00001B1E
		// (set) Token: 0x0600006C RID: 108 RVA: 0x00003926 File Offset: 0x00001B26
		[Property(Name = "typeProperty")]
		public string TypeProperty
		{
			get
			{
				return this.typeProperty;
			}
			set
			{
				this.typeProperty = value;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600006D RID: 109 RVA: 0x0000392F File Offset: 0x00001B2F
		// (set) Token: 0x0600006E RID: 110 RVA: 0x00003937 File Offset: 0x00001B37
		[Property(Name = "contentField")]
		public string ContentField { get; set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600006F RID: 111 RVA: 0x00003940 File Offset: 0x00001B40
		// (set) Token: 0x06000070 RID: 112 RVA: 0x00003948 File Offset: 0x00001B48
		[Property(Name = "managedPropertiesField")]
		public string ManagedPropertiesField { get; set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000071 RID: 113 RVA: 0x00003951 File Offset: 0x00001B51
		// (set) Token: 0x06000072 RID: 114 RVA: 0x00003959 File Offset: 0x00001B59
		[Property(Name = "pathField")]
		public string PathField { get; set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000073 RID: 115 RVA: 0x00003962 File Offset: 0x00001B62
		// (set) Token: 0x06000074 RID: 116 RVA: 0x0000396A File Offset: 0x00001B6A
		[Property(Name = "propertiesField")]
		public string PropertiesField { get; set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000075 RID: 117 RVA: 0x00003973 File Offset: 0x00001B73
		// (set) Token: 0x06000076 RID: 118 RVA: 0x0000397B File Offset: 0x00001B7B
		[Property(Name = "tenantIdField")]
		public string TenantIdField { get; set; }

		// Token: 0x06000077 RID: 119 RVA: 0x00003984 File Offset: 0x00001B84
		protected override void ValidateAndType(OperatorStatus status, IList<Edge> inputEdges)
		{
			if (!ValidationUtils.ValidateInputExists(status, inputEdges))
			{
				return;
			}
			this.Specification = new TransformationSpecification();
			this.Specification.AddAll(0, inputEdges[0].RecordSetType);
			status.SetSingleOutput(this.Specification.MakeOutputType());
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000078 RID: 120 RVA: 0x000039C4 File Offset: 0x00001BC4
		internal ClassificationConfiguration ClassificationConfiguration
		{
			get
			{
				ClassificationConfiguration classificationConfiguration = new ClassificationConfiguration();
				if (this.maxRulePackageCacheCount > 0)
				{
					classificationConfiguration.MaxRulePackageCacheCount = new int?(this.maxRulePackageCacheCount);
				}
				if (this.maxRulePackageCacheStorage > 0)
				{
					classificationConfiguration.MaxRulePackageCacheStorage = new int?(this.maxRulePackageCacheStorage);
				}
				if (this.performanceMonitorPollPeriod > 0)
				{
					classificationConfiguration.PerformanceMonitorPollPeriod = new int?(this.performanceMonitorPollPeriod);
				}
				if (this.rulePackageCachePollPeriod > 0)
				{
					classificationConfiguration.RulePackageCachePollPeriod = new int?(this.rulePackageCachePollPeriod);
				}
				if (this.rulePackageCacheRetrievalWaitTime > 0)
				{
					classificationConfiguration.RulePackageCacheRetrievalWaitTime = new int?(this.rulePackageCacheRetrievalWaitTime);
				}
				classificationConfiguration.UseLazyRegexCompilation = new bool?(this.useLazyRegexCompilation);
				classificationConfiguration.UseMemoryToImprovePerformance = new bool?(this.useMemoryToImprovePerformance);
				return classificationConfiguration;
			}
		}

		// Token: 0x04000040 RID: 64
		internal const string ExcelAlternateContentFieldName = "64AE120F-487D-445A-8D5A-5258F99CB970:XLTextForDLPClassification";

		// Token: 0x04000041 RID: 65
		private int maxRulePackageCacheCount = -1;

		// Token: 0x04000042 RID: 66
		private int maxRulePackageCacheStorage = -1;

		// Token: 0x04000043 RID: 67
		private int performanceMonitorPollPeriod = -1;

		// Token: 0x04000044 RID: 68
		private int rulePackageCachePollPeriod = -1;

		// Token: 0x04000045 RID: 69
		private int rulePackageCacheRetrievalWaitTime = -1;

		// Token: 0x04000046 RID: 70
		private bool useLazyRegexCompilation;

		// Token: 0x04000047 RID: 71
		private bool useMemoryToImprovePerformance = true;

		// Token: 0x04000048 RID: 72
		private string lastScanProperty = "ClassificationLastScan";

		// Token: 0x04000049 RID: 73
		private string countProperty = "ClassificationCount";

		// Token: 0x0400004A RID: 74
		private string confidenceProperty = "ClassificationConfidence";

		// Token: 0x0400004B RID: 75
		private string typeProperty = "ClassificationType";
	}
}
