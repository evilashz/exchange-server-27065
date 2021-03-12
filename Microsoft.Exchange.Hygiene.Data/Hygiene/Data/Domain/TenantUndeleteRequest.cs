using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data.Domain
{
	// Token: 0x0200012F RID: 303
	internal class TenantUndeleteRequest : ConfigurablePropertyBag
	{
		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06000BC5 RID: 3013 RVA: 0x0002583A File Offset: 0x00023A3A
		// (set) Token: 0x06000BC6 RID: 3014 RVA: 0x0002584C File Offset: 0x00023A4C
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

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06000BC7 RID: 3015 RVA: 0x0002585F File Offset: 0x00023A5F
		// (set) Token: 0x06000BC8 RID: 3016 RVA: 0x00025871 File Offset: 0x00023A71
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

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06000BC9 RID: 3017 RVA: 0x00025884 File Offset: 0x00023A84
		public override ObjectId Identity
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x040005F7 RID: 1527
		internal static readonly HygienePropertyDefinition TenantIdPropertyDefinition = new HygienePropertyDefinition("id_TenantId", typeof(Guid));

		// Token: 0x040005F8 RID: 1528
		internal static readonly HygienePropertyDefinition DeletedDatetimePropertyDefinition = new HygienePropertyDefinition("dt_DeletedDatetime", typeof(DateTime), DateTime.MinValue, ADPropertyDefinitionFlags.PersistDefaultValue);
	}
}
