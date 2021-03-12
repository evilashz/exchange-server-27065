using System;

namespace Microsoft.Exchange.InfoWorker.Common.MultiMailboxSearch
{
	// Token: 0x0200021A RID: 538
	internal class RefinerResult : IRefinerResult
	{
		// Token: 0x06000EAD RID: 3757 RVA: 0x00040A85 File Offset: 0x0003EC85
		public RefinerResult(string value, long count)
		{
			this.value = value;
			this.count = count;
		}

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x06000EAE RID: 3758 RVA: 0x00040A9B File Offset: 0x0003EC9B
		public string Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x06000EAF RID: 3759 RVA: 0x00040AA3 File Offset: 0x0003ECA3
		public long Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x06000EB0 RID: 3760 RVA: 0x00040AAB File Offset: 0x0003ECAB
		public void Merge(IRefinerResult result)
		{
			if (result == null)
			{
				return;
			}
			if (string.Compare(this.value, result.Value, StringComparison.OrdinalIgnoreCase) != 0)
			{
				throw new ArgumentException("RefinerResult: Invalid merge");
			}
			checked
			{
				this.count += result.Count;
			}
		}

		// Token: 0x040009F3 RID: 2547
		private readonly string value;

		// Token: 0x040009F4 RID: 2548
		private long count;
	}
}
