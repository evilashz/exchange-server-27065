using System;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.PushNotifications.Server.Services
{
	// Token: 0x02000023 RID: 35
	internal class TokenBucketBoundary : ITokenBucket
	{
		// Token: 0x060000EA RID: 234 RVA: 0x000042C6 File Offset: 0x000024C6
		private TokenBucketBoundary(bool isUnlimited)
		{
			this.isUnlimited = isUnlimited;
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000EB RID: 235 RVA: 0x000042D5 File Offset: 0x000024D5
		public uint CurrentBalance
		{
			get
			{
				if (!this.isUnlimited)
				{
					return 0U;
				}
				return uint.MaxValue;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000EC RID: 236 RVA: 0x000042E2 File Offset: 0x000024E2
		public ExDateTime NextRecharge
		{
			get
			{
				return ExDateTime.MaxValue;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000ED RID: 237 RVA: 0x000042E9 File Offset: 0x000024E9
		public bool IsFull
		{
			get
			{
				return this.isUnlimited;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000EE RID: 238 RVA: 0x000042F1 File Offset: 0x000024F1
		public bool IsEmpty
		{
			get
			{
				return !this.isUnlimited;
			}
		}

		// Token: 0x060000EF RID: 239 RVA: 0x000042FC File Offset: 0x000024FC
		public bool TryTakeToken()
		{
			return this.isUnlimited;
		}

		// Token: 0x0400005D RID: 93
		internal static readonly TokenBucketBoundary Unlimited = new TokenBucketBoundary(true);

		// Token: 0x0400005E RID: 94
		internal static readonly TokenBucketBoundary Empty = new TokenBucketBoundary(false);

		// Token: 0x0400005F RID: 95
		private readonly bool isUnlimited;
	}
}
