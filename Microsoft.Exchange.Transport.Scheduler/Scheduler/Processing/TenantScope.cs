using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Scheduler.Contracts;

namespace Microsoft.Exchange.Transport.Scheduler.Processing
{
	// Token: 0x02000030 RID: 48
	internal sealed class TenantScope : IMessageScope, IEquatable<IMessageScope>, IEquatable<TenantScope>
	{
		// Token: 0x0600011E RID: 286 RVA: 0x00005568 File Offset: 0x00003768
		public TenantScope(Guid tenantId)
		{
			ArgumentValidator.ThrowIfNull("tenantId", tenantId);
			this.tenantId = tenantId;
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600011F RID: 287 RVA: 0x00005587 File Offset: 0x00003787
		public MessageScopeType Type
		{
			get
			{
				return MessageScopeType.Tenant;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000120 RID: 288 RVA: 0x0000558A File Offset: 0x0000378A
		public string Display
		{
			get
			{
				return "Tenant:" + this.Value;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000121 RID: 289 RVA: 0x0000559C File Offset: 0x0000379C
		public object Value
		{
			get
			{
				return this.tenantId;
			}
		}

		// Token: 0x06000122 RID: 290 RVA: 0x000055A9 File Offset: 0x000037A9
		public static bool operator ==(TenantScope left, TenantScope right)
		{
			return object.Equals(left, right);
		}

		// Token: 0x06000123 RID: 291 RVA: 0x000055B2 File Offset: 0x000037B2
		public static bool operator !=(TenantScope left, TenantScope right)
		{
			return !object.Equals(left, right);
		}

		// Token: 0x06000124 RID: 292 RVA: 0x000055BE File Offset: 0x000037BE
		public bool Equals(TenantScope other)
		{
			return !object.ReferenceEquals(null, other) && (object.ReferenceEquals(this, other) || object.Equals(this.tenantId, other.tenantId));
		}

		// Token: 0x06000125 RID: 293 RVA: 0x000055F1 File Offset: 0x000037F1
		public override bool Equals(object obj)
		{
			return !object.ReferenceEquals(null, obj) && (object.ReferenceEquals(this, obj) || (!(obj.GetType() != base.GetType()) && this.Equals((TenantScope)obj)));
		}

		// Token: 0x06000126 RID: 294 RVA: 0x0000562C File Offset: 0x0000382C
		public override int GetHashCode()
		{
			return this.tenantId.GetHashCode();
		}

		// Token: 0x06000127 RID: 295 RVA: 0x0000564D File Offset: 0x0000384D
		public bool Equals(IMessageScope other)
		{
			return this.Equals(other as TenantScope);
		}

		// Token: 0x06000128 RID: 296 RVA: 0x0000565B File Offset: 0x0000385B
		public override string ToString()
		{
			return this.Display;
		}

		// Token: 0x040000A7 RID: 167
		private readonly Guid tenantId;
	}
}
