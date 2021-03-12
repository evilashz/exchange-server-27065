using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Management.Deployment.Analysis
{
	// Token: 0x0200001F RID: 31
	internal sealed class ResultsFeature : Feature
	{
		// Token: 0x060000E1 RID: 225 RVA: 0x000048AE File Offset: 0x00002AAE
		public ResultsFeature(Func<Result, IEnumerable<Result>> resultFunc)
		{
			this.resultFunc = resultFunc;
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x000048BD File Offset: 0x00002ABD
		public Func<Result, IEnumerable<Result>> ResultFunc
		{
			get
			{
				return this.resultFunc;
			}
		}

		// Token: 0x04000054 RID: 84
		private readonly Func<Result, IEnumerable<Result>> resultFunc;
	}
}
