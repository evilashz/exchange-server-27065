using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data.Directory
{
	// Token: 0x02000109 RID: 265
	internal class TenantUndeleteRequest : ConfigurablePropertyBag
	{
		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06000A4E RID: 2638 RVA: 0x0001F0BE File Offset: 0x0001D2BE
		// (set) Token: 0x06000A4F RID: 2639 RVA: 0x0001F0D0 File Offset: 0x0001D2D0
		public Guid TenantId
		{
			get
			{
				return (Guid)this[TenantUndeleteRequest.TenantIdPropertyDefinition];
			}
			set
			{
				this[TenantUndeleteRequest.TenantIdPropertyDefinition] = value;
			}
		}

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06000A50 RID: 2640 RVA: 0x0001F0E3 File Offset: 0x0001D2E3
		// (set) Token: 0x06000A51 RID: 2641 RVA: 0x0001F0F5 File Offset: 0x0001D2F5
		public DateTime DeletedDatetime
		{
			get
			{
				return (DateTime)this[TenantUndeleteRequest.DeletedDatetimePropertyDefinition];
			}
			set
			{
				this[TenantUndeleteRequest.DeletedDatetimePropertyDefinition] = value;
			}
		}

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06000A52 RID: 2642 RVA: 0x0001F108 File Offset: 0x0001D308
		public override ObjectId Identity
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x04000558 RID: 1368
		internal static readonly HygienePropertyDefinition TenantIdPropertyDefinition = new HygienePropertyDefinition("id_TenantId", typeof(Guid));

		// Token: 0x04000559 RID: 1369
		internal static readonly HygienePropertyDefinition DeletedDatetimePropertyDefinition = new HygienePropertyDefinition("dt_DeletedDatetime", typeof(DateTime), DateTime.MinValue, ADPropertyDefinitionFlags.PersistDefaultValue);
	}
}
