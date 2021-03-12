using System;
using System.Linq;

namespace Microsoft.Exchange.Diagnostics.Audit
{
	// Token: 0x0200019E RID: 414
	public sealed class SslPolicyInfo
	{
		// Token: 0x06000B81 RID: 2945 RVA: 0x0002A132 File Offset: 0x00028332
		public SslPolicyInfo()
		{
			this.errors = new SslError[25];
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06000B82 RID: 2946 RVA: 0x0002A152 File Offset: 0x00028352
		internal static SslPolicyInfo Instance
		{
			get
			{
				return SslPolicyInfo.instance;
			}
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06000B83 RID: 2947 RVA: 0x0002A159 File Offset: 0x00028359
		// (set) Token: 0x06000B84 RID: 2948 RVA: 0x0002A161 File Offset: 0x00028361
		public DateTime LastValidationTime { get; set; }

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06000B85 RID: 2949 RVA: 0x0002A17C File Offset: 0x0002837C
		// (set) Token: 0x06000B86 RID: 2950 RVA: 0x0002A208 File Offset: 0x00028408
		public SslError[] Errors
		{
			get
			{
				SslError[] result;
				lock (this.mutex)
				{
					result = (from e in this.errors
					where e != null
					orderby e.Index
					select e).ToArray<SslError>();
				}
				return result;
			}
			set
			{
			}
		}

		// Token: 0x06000B87 RID: 2951 RVA: 0x0002A20C File Offset: 0x0002840C
		public void Add(SslError error)
		{
			lock (this.mutex)
			{
				this.errors[this.errorPosition] = error;
				error.Index = (long)this.errorCount++;
				this.errorPosition = (this.errorPosition + 1) % 25;
			}
		}

		// Token: 0x04000860 RID: 2144
		private const int CyclicListLength = 25;

		// Token: 0x04000861 RID: 2145
		private static readonly SslPolicyInfo instance = new SslPolicyInfo();

		// Token: 0x04000862 RID: 2146
		private volatile int errorPosition;

		// Token: 0x04000863 RID: 2147
		private volatile int errorCount;

		// Token: 0x04000864 RID: 2148
		private object mutex = new object();

		// Token: 0x04000865 RID: 2149
		private readonly SslError[] errors;
	}
}
