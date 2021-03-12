using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x02000118 RID: 280
	public class MaxConcurrencyReachedException : Exception
	{
		// Token: 0x17000166 RID: 358
		// (get) Token: 0x0600081C RID: 2076 RVA: 0x00020BFC File Offset: 0x0001EDFC
		// (set) Token: 0x0600081D RID: 2077 RVA: 0x00020C04 File Offset: 0x0001EE04
		public string BucketName { get; private set; }

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x0600081E RID: 2078 RVA: 0x00020C0D File Offset: 0x0001EE0D
		// (set) Token: 0x0600081F RID: 2079 RVA: 0x00020C15 File Offset: 0x0001EE15
		public IConcurrencyGuard Guard { get; set; }

		// Token: 0x06000820 RID: 2080 RVA: 0x00020C1E File Offset: 0x0001EE1E
		public MaxConcurrencyReachedException(IConcurrencyGuard guard, string bucketName)
		{
			this.Guard = guard;
			this.BucketName = bucketName;
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06000821 RID: 2081 RVA: 0x00020C34 File Offset: 0x0001EE34
		public override string Message
		{
			get
			{
				return string.Concat(new object[]
				{
					"The ConcurrencyGuard '",
					ConcurrencyGuard.FormatGuardBucketName(this.Guard, this.BucketName),
					"' has hit the limit of ",
					this.Guard.MaxConcurrency,
					"."
				});
			}
		}
	}
}
