using System;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x0200033E RID: 830
	internal class TenantBasedCondition : WaitCondition
	{
		// Token: 0x060023E8 RID: 9192 RVA: 0x00088CF6 File Offset: 0x00086EF6
		public TenantBasedCondition(Guid tenantId)
		{
			this.tenant = tenantId;
		}

		// Token: 0x17000B33 RID: 2867
		// (get) Token: 0x060023E9 RID: 9193 RVA: 0x00088D05 File Offset: 0x00086F05
		public Guid TenantId
		{
			get
			{
				return this.tenant;
			}
		}

		// Token: 0x060023EA RID: 9194 RVA: 0x00088D10 File Offset: 0x00086F10
		public override int CompareTo(object obj)
		{
			TenantBasedCondition tenantBasedCondition = obj as TenantBasedCondition;
			if (tenantBasedCondition == null)
			{
				throw new ArgumentException();
			}
			return this.tenant.CompareTo(tenantBasedCondition.tenant);
		}

		// Token: 0x060023EB RID: 9195 RVA: 0x00088D44 File Offset: 0x00086F44
		public override bool Equals(object obj)
		{
			TenantBasedCondition tenantBasedCondition = obj as TenantBasedCondition;
			return tenantBasedCondition != null && this.Equals(tenantBasedCondition);
		}

		// Token: 0x060023EC RID: 9196 RVA: 0x00088D64 File Offset: 0x00086F64
		public bool Equals(TenantBasedCondition condition)
		{
			return condition != null && this.tenant.Equals(condition.tenant);
		}

		// Token: 0x060023ED RID: 9197 RVA: 0x00088D8C File Offset: 0x00086F8C
		public override int GetHashCode()
		{
			return this.tenant.GetHashCode();
		}

		// Token: 0x060023EE RID: 9198 RVA: 0x00088DB0 File Offset: 0x00086FB0
		public override string ToString()
		{
			return "TenantBasedCondition-" + this.tenant.ToString();
		}

		// Token: 0x04001292 RID: 4754
		private readonly Guid tenant;
	}
}
