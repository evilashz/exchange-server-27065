using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Hygiene.Data.Sync
{
	// Token: 0x02000224 RID: 548
	internal class SyncPropertyRefresh : ADObject
	{
		// Token: 0x0600164B RID: 5707 RVA: 0x000451F4 File Offset: 0x000433F4
		public SyncPropertyRefresh(string serviceInstance)
		{
			if (serviceInstance == null)
			{
				throw new ArgumentNullException("serviceInstance");
			}
			this.ServiceInstance = serviceInstance;
			base.SetId(new ADObjectId(DalHelper.GetTenantDistinguishedName(this.ServiceInstance), CombGuidGenerator.NewGuid()));
		}

		// Token: 0x0600164C RID: 5708 RVA: 0x0004522C File Offset: 0x0004342C
		public SyncPropertyRefresh()
		{
		}

		// Token: 0x170006FA RID: 1786
		// (get) Token: 0x0600164D RID: 5709 RVA: 0x00045234 File Offset: 0x00043434
		// (set) Token: 0x0600164E RID: 5710 RVA: 0x00045246 File Offset: 0x00043446
		public string ServiceInstance
		{
			get
			{
				return this[ADObjectSchema.RawName] as string;
			}
			set
			{
				this[ADObjectSchema.RawName] = value;
			}
		}

		// Token: 0x170006FB RID: 1787
		// (get) Token: 0x0600164F RID: 5711 RVA: 0x00045254 File Offset: 0x00043454
		// (set) Token: 0x06001650 RID: 5712 RVA: 0x00045266 File Offset: 0x00043466
		public SyncPropertyRefreshStatus Status
		{
			get
			{
				return (SyncPropertyRefreshStatus)this[SyncPropertyRefreshSchema.Status];
			}
			set
			{
				this[SyncPropertyRefreshSchema.Status] = value;
			}
		}

		// Token: 0x170006FC RID: 1788
		// (get) Token: 0x06001651 RID: 5713 RVA: 0x00045279 File Offset: 0x00043479
		internal override ADObjectSchema Schema
		{
			get
			{
				return SyncPropertyRefresh.schema;
			}
		}

		// Token: 0x170006FD RID: 1789
		// (get) Token: 0x06001652 RID: 5714 RVA: 0x00045280 File Offset: 0x00043480
		internal override string MostDerivedObjectClass
		{
			get
			{
				return SyncPropertyRefresh.mostDerivedClass;
			}
		}

		// Token: 0x170006FE RID: 1790
		// (get) Token: 0x06001653 RID: 5715 RVA: 0x00045287 File Offset: 0x00043487
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x04000B4C RID: 2892
		private static readonly string mostDerivedClass = "SyncPropertyRefresh";

		// Token: 0x04000B4D RID: 2893
		private static readonly SyncPropertyRefreshSchema schema = ObjectSchema.GetInstance<SyncPropertyRefreshSchema>();
	}
}
