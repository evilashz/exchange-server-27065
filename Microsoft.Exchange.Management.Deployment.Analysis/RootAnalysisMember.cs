using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Exchange.Management.Deployment.Analysis
{
	// Token: 0x02000026 RID: 38
	public class RootAnalysisMember : AnalysisMember<object>
	{
		// Token: 0x06000126 RID: 294 RVA: 0x00006151 File Offset: 0x00004351
		public RootAnalysisMember(Analysis analysis) : base(analysis, RootAnalysisMember.RootFeatureSetBuilder.RootFeatureSet)
		{
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000127 RID: 295 RVA: 0x0000615F File Offset: 0x0000435F
		public override Type ValueType
		{
			get
			{
				return typeof(object);
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000128 RID: 296 RVA: 0x0000616B File Offset: 0x0000436B
		public override IEnumerable<Result> UntypedResults
		{
			get
			{
				return RootAnalysisMember.rootResults;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000129 RID: 297 RVA: 0x00006172 File Offset: 0x00004372
		public override IEnumerable<Result> CachedResults
		{
			get
			{
				return RootAnalysisMember.rootResults;
			}
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00006179 File Offset: 0x00004379
		public override void Start()
		{
		}

		// Token: 0x04000067 RID: 103
		private static readonly Result<object> rootResult = new Result<object>(new object());

		// Token: 0x04000068 RID: 104
		private static readonly IEnumerable<Result> rootResults = new Result[]
		{
			RootAnalysisMember.rootResult
		};

		// Token: 0x02000027 RID: 39
		private sealed class RootFeatureSetBuilder : FeatureSet.Builder
		{
			// Token: 0x1700004B RID: 75
			// (get) Token: 0x0600012C RID: 300 RVA: 0x000061AD File Offset: 0x000043AD
			public static FeatureSet RootFeatureSet
			{
				get
				{
					return RootAnalysisMember.RootFeatureSetBuilder.rootFeatureSet;
				}
			}

			// Token: 0x0600012D RID: 301 RVA: 0x000061C0 File Offset: 0x000043C0
			private FeatureSet Build()
			{
				IEnumerable<Feature> features = Enumerable.Empty<Feature>();
				Feature[] array = new Feature[3];
				array[0] = new EvaluationModeFeature(Evaluate.OnDemand);
				array[1] = new ForEachResultFeature(() => null);
				array[2] = new ResultsFeature((Result x) => RootAnalysisMember.rootResults);
				return base.BuildFeatureSet(features, array);
			}

			// Token: 0x04000069 RID: 105
			private static readonly FeatureSet rootFeatureSet = new RootAnalysisMember.RootFeatureSetBuilder().Build();
		}
	}
}
