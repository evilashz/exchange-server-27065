using System;
using System.Linq;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.ReportingTask.Common;

namespace Microsoft.Exchange.Management.ReportingTask.Query
{
	// Token: 0x020006B9 RID: 1721
	internal class ResultSizeDecorator<TReportObject> : QueryDecorator<TReportObject> where TReportObject : class
	{
		// Token: 0x06003CC7 RID: 15559 RVA: 0x0010279C File Offset: 0x0010099C
		public ResultSizeDecorator(ITaskContext taskContext) : base(taskContext)
		{
			this.ResultSize = null;
		}

		// Token: 0x1700122A RID: 4650
		// (get) Token: 0x06003CC8 RID: 15560 RVA: 0x001027D4 File Offset: 0x001009D4
		// (set) Token: 0x06003CC9 RID: 15561 RVA: 0x001027DC File Offset: 0x001009DC
		public Unlimited<int>? ResultSize { get; set; }

		// Token: 0x1700122B RID: 4651
		// (get) Token: 0x06003CCA RID: 15562 RVA: 0x001027E5 File Offset: 0x001009E5
		// (set) Token: 0x06003CCB RID: 15563 RVA: 0x001027ED File Offset: 0x001009ED
		public Unlimited<int> DefaultResultSize
		{
			get
			{
				return this.defaultResultSize;
			}
			set
			{
				this.defaultResultSize = value;
			}
		}

		// Token: 0x1700122C RID: 4652
		// (get) Token: 0x06003CCC RID: 15564 RVA: 0x001027F8 File Offset: 0x001009F8
		public Unlimited<int> TargetResultSize
		{
			get
			{
				Unlimited<int>? resultSize = this.ResultSize;
				if (resultSize == null)
				{
					return this.DefaultResultSize;
				}
				return resultSize.GetValueOrDefault();
			}
		}

		// Token: 0x1700122D RID: 4653
		// (get) Token: 0x06003CCD RID: 15565 RVA: 0x00102823 File Offset: 0x00100A23
		public override QueryOrder QueryOrder
		{
			get
			{
				return QueryOrder.Top;
			}
		}

		// Token: 0x06003CCE RID: 15566 RVA: 0x00102828 File Offset: 0x00100A28
		public bool IsResultSizeReached(long totalCount)
		{
			return this.ResultSize != null && !this.ResultSize.Value.IsUnlimited && (long)this.ResultSize.Value.Value < totalCount;
		}

		// Token: 0x06003CCF RID: 15567 RVA: 0x0010287C File Offset: 0x00100A7C
		public bool IsDefaultResultSizeReached(long totalCount)
		{
			return this.ResultSize == null && !this.DefaultResultSize.IsUnlimited && (long)this.DefaultResultSize.Value < totalCount;
		}

		// Token: 0x06003CD0 RID: 15568 RVA: 0x001028C0 File Offset: 0x00100AC0
		public bool IsTargetResultSizeReached(long totalCount)
		{
			return !this.TargetResultSize.IsUnlimited && (long)this.TargetResultSize.Value < totalCount;
		}

		// Token: 0x06003CD1 RID: 15569 RVA: 0x001028F4 File Offset: 0x00100AF4
		public override IQueryable<TReportObject> GetQuery(IQueryable<TReportObject> query)
		{
			if (!this.TargetResultSize.IsUnlimited)
			{
				int num = this.TargetResultSize.Value;
				if (num > 0)
				{
					num++;
				}
				query = query.Take(num);
			}
			return query;
		}

		// Token: 0x06003CD2 RID: 15570 RVA: 0x00102934 File Offset: 0x00100B34
		public override void Validate()
		{
			if (!this.TargetResultSize.IsUnlimited && this.TargetResultSize.Value <= 0)
			{
				base.TaskContext.WriteError(new ReportingException(Strings.ErrorInvalidResultSize), ExchangeErrorCategory.Client, null);
			}
		}

		// Token: 0x04002753 RID: 10067
		private Unlimited<int> defaultResultSize = new Unlimited<int>(DataMart.Instance.DefaultReportResultSize);
	}
}
