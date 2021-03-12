using System;

namespace Microsoft.Exchange.Management.Deployment.Analysis
{
	// Token: 0x02000016 RID: 22
	public class FilteredException : AnalysisException
	{
		// Token: 0x060000C4 RID: 196 RVA: 0x00004308 File Offset: 0x00002508
		public FilteredException() : base(null, Strings.FilteredResult)
		{
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x0000431B File Offset: 0x0000251B
		public FilteredException(AnalysisMember source, Result filteredResult) : base(source, Strings.FilteredResult)
		{
			this.filteredResult = filteredResult;
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x00004335 File Offset: 0x00002535
		public Result FilteredResult
		{
			get
			{
				return this.filteredResult;
			}
		}

		// Token: 0x04000048 RID: 72
		private readonly Result filteredResult;
	}
}
