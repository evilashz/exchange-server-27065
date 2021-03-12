using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Hygiene.Data
{
	// Token: 0x020000CD RID: 205
	internal class DomainProvisioningRequest : ConfigurablePropertyBag
	{
		// Token: 0x060006BF RID: 1727 RVA: 0x000157BF File Offset: 0x000139BF
		public DomainProvisioningRequest()
		{
			this.RequestType = ObjectState.Changed;
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x060006C0 RID: 1728 RVA: 0x000157CE File Offset: 0x000139CE
		public override ObjectId Identity
		{
			get
			{
				return new ConfigObjectId(string.Format("{0}\\{1}", this[DomainProvisioningRequestSchema.OrganizationalUnitRoot], this.DomainName));
			}
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x060006C1 RID: 1729 RVA: 0x000157F0 File Offset: 0x000139F0
		// (set) Token: 0x060006C2 RID: 1730 RVA: 0x00015802 File Offset: 0x00013A02
		public Guid OrganizationalUnitRoot
		{
			get
			{
				return (Guid)this[DomainProvisioningRequestSchema.OrganizationalUnitRoot];
			}
			set
			{
				this[DomainProvisioningRequestSchema.OrganizationalUnitRoot] = value;
			}
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x060006C3 RID: 1731 RVA: 0x00015815 File Offset: 0x00013A15
		// (set) Token: 0x060006C4 RID: 1732 RVA: 0x00015827 File Offset: 0x00013A27
		public string DomainName
		{
			get
			{
				return this[DomainProvisioningRequestSchema.DomainName] as string;
			}
			set
			{
				this[DomainProvisioningRequestSchema.DomainName] = value;
			}
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x060006C5 RID: 1733 RVA: 0x00015835 File Offset: 0x00013A35
		// (set) Token: 0x060006C6 RID: 1734 RVA: 0x0001585B File Offset: 0x00013A5B
		public ObjectState RequestType
		{
			get
			{
				return (ObjectState)Enum.Parse(typeof(ObjectState), (string)this[DomainProvisioningRequestSchema.RequestType]);
			}
			set
			{
				if (value != ObjectState.Changed && value != ObjectState.Deleted)
				{
					throw new ArgumentException("Only Changed and Deleted are valid RequestType values");
				}
				this[DomainProvisioningRequestSchema.RequestType] = value.ToString();
			}
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x060006C7 RID: 1735 RVA: 0x00015886 File Offset: 0x00013A86
		// (set) Token: 0x060006C8 RID: 1736 RVA: 0x00015898 File Offset: 0x00013A98
		public DomainProvisioningRequestFlags RequestFlags
		{
			get
			{
				return (DomainProvisioningRequestFlags)this[DomainProvisioningRequestSchema.RequestFlags];
			}
			set
			{
				this[DomainProvisioningRequestSchema.RequestFlags] = value;
			}
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x000158AB File Offset: 0x00013AAB
		public override Type GetSchemaType()
		{
			return typeof(DomainProvisioningRequestSchema);
		}
	}
}
