using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data.Reporting
{
	// Token: 0x020001CA RID: 458
	internal class Tenant : ConfigurablePropertyBag
	{
		// Token: 0x170005EF RID: 1519
		// (get) Token: 0x06001343 RID: 4931 RVA: 0x0003A782 File Offset: 0x00038982
		// (set) Token: 0x06001344 RID: 4932 RVA: 0x0003A794 File Offset: 0x00038994
		public Guid TenantId
		{
			get
			{
				return (Guid)this[Tenant.TenantIdProperty];
			}
			internal set
			{
				this[Tenant.TenantIdProperty] = value;
			}
		}

		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x06001345 RID: 4933 RVA: 0x0003A7A7 File Offset: 0x000389A7
		public override ObjectId Identity
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x04000948 RID: 2376
		protected static readonly HygienePropertyDefinition TenantIdProperty = new HygienePropertyDefinition("tenantId", typeof(Guid), Guid.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);
	}
}
