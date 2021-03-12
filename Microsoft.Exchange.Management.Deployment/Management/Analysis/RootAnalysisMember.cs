using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Management.Analysis.Features;

namespace Microsoft.Exchange.Management.Analysis
{
	// Token: 0x02000046 RID: 70
	internal class RootAnalysisMember : AnalysisMember
	{
		// Token: 0x060001ED RID: 493 RVA: 0x00007AC5 File Offset: 0x00005CC5
		public RootAnalysisMember(Analysis analysis) : base(() => null, ConcurrencyType.Synchronous, analysis, Enumerable.Empty<Feature>())
		{
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060001EE RID: 494 RVA: 0x00007AF1 File Offset: 0x00005CF1
		public override Type ValueType
		{
			get
			{
				return typeof(object);
			}
		}

		// Token: 0x060001EF RID: 495 RVA: 0x00007AFD File Offset: 0x00005CFD
		public override void Start()
		{
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00007B00 File Offset: 0x00005D00
		public override IEnumerable<Result> GetResults()
		{
			return new Result<object>[]
			{
				Result<object>.Default
			};
		}
	}
}
