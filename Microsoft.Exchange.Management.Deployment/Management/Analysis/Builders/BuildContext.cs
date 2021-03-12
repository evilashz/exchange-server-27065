using System;
using System.Collections.Generic;
using Microsoft.Exchange.Management.Analysis.Features;

namespace Microsoft.Exchange.Management.Analysis.Builders
{
	// Token: 0x0200004B RID: 75
	internal abstract class BuildContext<T> : IFeatureBuilder
	{
		// Token: 0x060001FE RID: 510 RVA: 0x00007C6E File Offset: 0x00005E6E
		public BuildContext()
		{
			this.features = new List<Feature>();
			this.Parent = null;
			this.RunAs = ConcurrencyType.Synchronous;
			this.Analysis = null;
			this.features = new List<Feature>();
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060001FF RID: 511 RVA: 0x00007CA1 File Offset: 0x00005EA1
		// (set) Token: 0x06000200 RID: 512 RVA: 0x00007CA9 File Offset: 0x00005EA9
		public Func<AnalysisMember> Parent { get; set; }

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000201 RID: 513 RVA: 0x00007CB2 File Offset: 0x00005EB2
		// (set) Token: 0x06000202 RID: 514 RVA: 0x00007CBA File Offset: 0x00005EBA
		public ConcurrencyType RunAs { get; set; }

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000203 RID: 515 RVA: 0x00007CC3 File Offset: 0x00005EC3
		// (set) Token: 0x06000204 RID: 516 RVA: 0x00007CCB File Offset: 0x00005ECB
		public Analysis Analysis { get; set; }

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000205 RID: 517 RVA: 0x00007CD4 File Offset: 0x00005ED4
		public IEnumerable<Feature> Features
		{
			get
			{
				return this.features;
			}
		}

		// Token: 0x06000206 RID: 518
		public abstract AnalysisMember<T> Construct();

		// Token: 0x06000207 RID: 519 RVA: 0x00007CF4 File Offset: 0x00005EF4
		void IFeatureBuilder.AddFeature(Feature feature)
		{
			if (feature == null)
			{
				throw new ArgumentNullException("feature");
			}
			if (!feature.AllowsMultiple)
			{
				this.features.RemoveAll((Feature x) => x.GetType() == this.features.GetType());
			}
			this.features.Add(feature);
		}

		// Token: 0x04000135 RID: 309
		private List<Feature> features;
	}
}
