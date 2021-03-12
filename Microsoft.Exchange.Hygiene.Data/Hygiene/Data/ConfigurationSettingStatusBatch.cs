using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Hygiene.Data
{
	// Token: 0x0200006F RID: 111
	internal class ConfigurationSettingStatusBatch : ConfigurablePropertyBag
	{
		// Token: 0x0600043F RID: 1087 RVA: 0x0000CB17 File Offset: 0x0000AD17
		public ConfigurationSettingStatusBatch(Guid tenantId)
		{
			this.TenantId = tenantId;
			this.Batch = new MultiValuedProperty<IConfigurable>();
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000440 RID: 1088 RVA: 0x0000CB31 File Offset: 0x0000AD31
		public override ObjectId Identity
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000441 RID: 1089 RVA: 0x0000CB38 File Offset: 0x0000AD38
		// (set) Token: 0x06000442 RID: 1090 RVA: 0x0000CB4A File Offset: 0x0000AD4A
		private Guid TenantId
		{
			get
			{
				return (Guid)this[ConfigurationSettingStatusBatchSchema.TenantIdProp];
			}
			set
			{
				this[ConfigurationSettingStatusBatchSchema.TenantIdProp] = value;
			}
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000443 RID: 1091 RVA: 0x0000CB5D File Offset: 0x0000AD5D
		// (set) Token: 0x06000444 RID: 1092 RVA: 0x0000CB6F File Offset: 0x0000AD6F
		private MultiValuedProperty<IConfigurable> Batch
		{
			get
			{
				return (MultiValuedProperty<IConfigurable>)this[ConfigurationSettingBatchSchema.BatchProp];
			}
			set
			{
				this[ConfigurationSettingBatchSchema.BatchProp] = value;
			}
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x0000CB7D File Offset: 0x0000AD7D
		public void Add(IConfigurable configurableObject)
		{
			if (configurableObject == null)
			{
				throw new ArgumentNullException("configurableObject");
			}
			this.Batch.Add(configurableObject);
		}
	}
}
