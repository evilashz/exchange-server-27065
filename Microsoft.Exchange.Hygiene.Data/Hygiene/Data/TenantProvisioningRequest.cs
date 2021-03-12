using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Hygiene.Data
{
	// Token: 0x02000106 RID: 262
	internal class TenantProvisioningRequest : ConfigurablePropertyBag
	{
		// Token: 0x06000A3F RID: 2623 RVA: 0x0001EF17 File Offset: 0x0001D117
		public TenantProvisioningRequest()
		{
			this.RequestType = ObjectState.Changed;
		}

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06000A40 RID: 2624 RVA: 0x0001EF26 File Offset: 0x0001D126
		public override ObjectId Identity
		{
			get
			{
				return new ConfigObjectId(this[TenantProvisioningRequestSchema.OrganizationalUnitRoot].ToString());
			}
		}

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06000A41 RID: 2625 RVA: 0x0001EF3D File Offset: 0x0001D13D
		// (set) Token: 0x06000A42 RID: 2626 RVA: 0x0001EF4F File Offset: 0x0001D14F
		public Guid OrganizationalUnitRoot
		{
			get
			{
				return (Guid)this[TenantProvisioningRequestSchema.OrganizationalUnitRoot];
			}
			set
			{
				this[TenantProvisioningRequestSchema.OrganizationalUnitRoot] = value;
			}
		}

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06000A43 RID: 2627 RVA: 0x0001EF62 File Offset: 0x0001D162
		// (set) Token: 0x06000A44 RID: 2628 RVA: 0x0001EF88 File Offset: 0x0001D188
		public ObjectState RequestType
		{
			get
			{
				return (ObjectState)Enum.Parse(typeof(ObjectState), (string)this[TenantProvisioningRequestSchema.RequestType]);
			}
			set
			{
				if (value != ObjectState.Changed && value != ObjectState.Deleted)
				{
					throw new ArgumentException("Only Changed and Deleted are valid RequestType values");
				}
				this[TenantProvisioningRequestSchema.RequestType] = value.ToString();
			}
		}

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06000A45 RID: 2629 RVA: 0x0001EFB3 File Offset: 0x0001D1B3
		// (set) Token: 0x06000A46 RID: 2630 RVA: 0x0001EFC5 File Offset: 0x0001D1C5
		public TenantProvisioningRequestFlags RequestFlags
		{
			get
			{
				return (TenantProvisioningRequestFlags)this[TenantProvisioningRequestSchema.RequestFlags];
			}
			set
			{
				this[TenantProvisioningRequestSchema.RequestFlags] = value;
			}
		}

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06000A47 RID: 2631 RVA: 0x0001EFD8 File Offset: 0x0001D1D8
		// (set) Token: 0x06000A48 RID: 2632 RVA: 0x0001EFEA File Offset: 0x0001D1EA
		public string MigrateToRegion
		{
			get
			{
				return this[TenantProvisioningRequestSchema.MigrateToRegion] as string;
			}
			set
			{
				this[TenantProvisioningRequestSchema.MigrateToRegion] = value;
			}
		}

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06000A49 RID: 2633 RVA: 0x0001EFF8 File Offset: 0x0001D1F8
		// (set) Token: 0x06000A4A RID: 2634 RVA: 0x0001F00A File Offset: 0x0001D20A
		public string MigrateToInstance
		{
			get
			{
				return this[TenantProvisioningRequestSchema.MigrateToInstance] as string;
			}
			set
			{
				this[TenantProvisioningRequestSchema.MigrateToInstance] = value;
			}
		}

		// Token: 0x06000A4B RID: 2635 RVA: 0x0001F018 File Offset: 0x0001D218
		public override Type GetSchemaType()
		{
			return typeof(TenantProvisioningRequestSchema);
		}
	}
}
