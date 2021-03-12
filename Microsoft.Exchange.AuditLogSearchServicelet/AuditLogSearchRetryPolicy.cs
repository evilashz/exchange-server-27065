using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Servicelets.AuditLogSearch
{
	// Token: 0x0200000A RID: 10
	internal class AuditLogSearchRetryPolicy
	{
		// Token: 0x0600005C RID: 92 RVA: 0x00002E63 File Offset: 0x00001063
		public AuditLogSearchRetryPolicy()
		{
			this.RetryTenants = new ConcurrentQueue<ADUser>();
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00002E76 File Offset: 0x00001076
		public static int RetryLimit
		{
			get
			{
				return AuditLogSearchRetryPolicy.retryLimit;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00002E7D File Offset: 0x0000107D
		// (set) Token: 0x0600005F RID: 95 RVA: 0x00002E85 File Offset: 0x00001085
		public ConcurrentQueue<ADUser> RetryTenants { get; private set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00002E8E File Offset: 0x0000108E
		public bool IsRetrying
		{
			get
			{
				return this.RetryIteration > 0;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00002E99 File Offset: 0x00001099
		public int RetryIteration
		{
			get
			{
				return this.retryIteration;
			}
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002EA4 File Offset: 0x000010A4
		public void Reset()
		{
			this.retryIteration = 0;
			this.RetryTenants = new ConcurrentQueue<ADUser>();
			AuditLogSearchHealth auditLogSearchHealth = AuditLogSearchHealthHandler.GetInstance().AuditLogSearchHealth;
			auditLogSearchHealth.RetryIteration = this.RetryIteration;
			auditLogSearchHealth.NextSearchTime = new DateTime?(DateTime.UtcNow);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002EEA File Offset: 0x000010EA
		public void ClearRetryTenants()
		{
			this.RetryTenants = new ConcurrentQueue<ADUser>();
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00002EF8 File Offset: 0x000010F8
		public int ProceedToNextIteration(int defaultDelay)
		{
			AuditLogSearchHealth auditLogSearchHealth = AuditLogSearchHealthHandler.GetInstance().AuditLogSearchHealth;
			bool flag = !this.RetryTenants.IsEmpty && this.RetryIteration < AuditLogSearchRetryPolicy.retryLimit;
			int num;
			if (flag)
			{
				num = Math.Min(AuditLogSearchRetryPolicy.retryDelays[this.RetryIteration], defaultDelay);
				this.retryIteration = this.RetryIteration + 1;
				auditLogSearchHealth.ClearRetry();
				using (IEnumerator<ADUser> enumerator = this.RetryTenants.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						ADUser tenant = enumerator.Current;
						auditLogSearchHealth.AddRetryTenant(tenant);
					}
					goto IL_90;
				}
			}
			num = defaultDelay;
			this.Reset();
			IL_90:
			Random random = new Random();
			int num2 = (int)TimeSpan.FromMinutes(2.0).TotalMilliseconds;
			num += random.Next(-num2, num2);
			auditLogSearchHealth.RetryIteration = this.RetryIteration;
			auditLogSearchHealth.NextSearchTime = new DateTime?(DateTime.UtcNow + TimeSpan.FromMilliseconds((double)num));
			return num;
		}

		// Token: 0x0400002D RID: 45
		private static readonly int[] retryDelays = new int[]
		{
			(int)TimeSpan.FromMinutes(15.0).TotalMilliseconds,
			(int)TimeSpan.FromMinutes(90.0).TotalMilliseconds,
			(int)TimeSpan.FromMinutes(180.0).TotalMilliseconds
		};

		// Token: 0x0400002E RID: 46
		private static readonly int retryLimit = AuditLogSearchRetryPolicy.retryDelays.Length;

		// Token: 0x0400002F RID: 47
		private int retryIteration;
	}
}
