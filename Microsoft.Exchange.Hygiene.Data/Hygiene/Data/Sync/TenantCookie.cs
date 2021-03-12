using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data.Sync
{
	// Token: 0x02000228 RID: 552
	public class TenantCookie : BaseCookie
	{
		// Token: 0x06001671 RID: 5745 RVA: 0x0004596B File Offset: 0x00043B6B
		public TenantCookie()
		{
		}

		// Token: 0x06001672 RID: 5746 RVA: 0x00045973 File Offset: 0x00043B73
		public TenantCookie(ADObjectId tenantId, string serviceInstance, byte[] data, bool complete = false) : base(serviceInstance, data)
		{
			if (tenantId == null)
			{
				throw new ArgumentNullException("tenantId");
			}
			this.TenantId = tenantId;
			base.Complete = complete;
		}

		// Token: 0x06001673 RID: 5747 RVA: 0x0004599A File Offset: 0x00043B9A
		public TenantCookie(string contextId, string serviceInstance, byte[] data, bool complete = false) : this(new ADObjectId(Guid.Parse(contextId)), serviceInstance, data, complete)
		{
		}

		// Token: 0x17000701 RID: 1793
		// (get) Token: 0x06001674 RID: 5748 RVA: 0x000459B1 File Offset: 0x00043BB1
		// (set) Token: 0x06001675 RID: 5749 RVA: 0x000459C3 File Offset: 0x00043BC3
		public ADObjectId TenantId
		{
			get
			{
				return this[TenantCookieSchema.TenantIdProp] as ADObjectId;
			}
			set
			{
				this[TenantCookieSchema.TenantIdProp] = value;
			}
		}

		// Token: 0x17000702 RID: 1794
		// (get) Token: 0x06001676 RID: 5750 RVA: 0x000459D1 File Offset: 0x00043BD1
		internal override ADObjectSchema Schema
		{
			get
			{
				return TenantCookie.schema;
			}
		}

		// Token: 0x17000703 RID: 1795
		// (get) Token: 0x06001677 RID: 5751 RVA: 0x000459D8 File Offset: 0x00043BD8
		internal override string MostDerivedObjectClass
		{
			get
			{
				return TenantCookie.mostDerivedClass;
			}
		}

		// Token: 0x17000704 RID: 1796
		// (get) Token: 0x06001678 RID: 5752 RVA: 0x000459DF File Offset: 0x00043BDF
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x04000B5A RID: 2906
		private static readonly TenantCookieSchema schema = ObjectSchema.GetInstance<TenantCookieSchema>();

		// Token: 0x04000B5B RID: 2907
		private static string mostDerivedClass = "TenantCookie";
	}
}
