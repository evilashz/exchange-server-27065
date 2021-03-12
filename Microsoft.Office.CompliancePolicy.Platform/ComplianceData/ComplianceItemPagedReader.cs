using System;
using System.Collections.Generic;
using Microsoft.Office.CompliancePolicy.PolicyEvaluation;

namespace Microsoft.Office.CompliancePolicy.ComplianceData
{
	// Token: 0x02000055 RID: 85
	public abstract class ComplianceItemPagedReader : IDisposable
	{
		// Token: 0x060001FD RID: 509 RVA: 0x0000658A File Offset: 0x0000478A
		public ComplianceItemPagedReader(int pageSize, QueryPredicate condition)
		{
			if (pageSize <= 0)
			{
				throw new ArgumentException("pageSize must be greater than 0");
			}
			this.PageSize = pageSize;
			this.Condition = condition;
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060001FE RID: 510 RVA: 0x000065AF File Offset: 0x000047AF
		// (set) Token: 0x060001FF RID: 511 RVA: 0x000065B7 File Offset: 0x000047B7
		public string Query { get; protected set; }

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000200 RID: 512 RVA: 0x000065C0 File Offset: 0x000047C0
		// (set) Token: 0x06000201 RID: 513 RVA: 0x000065C8 File Offset: 0x000047C8
		public QueryPredicate Condition
		{
			get
			{
				return this.condition;
			}
			set
			{
				this.condition = value;
				this.Query = this.GenerateQuery();
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000202 RID: 514 RVA: 0x000065DD File Offset: 0x000047DD
		// (set) Token: 0x06000203 RID: 515 RVA: 0x000065E5 File Offset: 0x000047E5
		public int PageSize { get; private set; }

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000204 RID: 516
		public abstract string PageCookie { get; }

		// Token: 0x06000205 RID: 517
		public abstract IEnumerable<ComplianceItem> GetNextPage();

		// Token: 0x06000206 RID: 518 RVA: 0x000065EE File Offset: 0x000047EE
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06000207 RID: 519
		protected abstract string GenerateQuery();

		// Token: 0x06000208 RID: 520
		protected abstract void Dispose(bool isDisposing);

		// Token: 0x04000102 RID: 258
		private QueryPredicate condition;
	}
}
