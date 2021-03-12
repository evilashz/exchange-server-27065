using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020004DB RID: 1243
	[DataContract]
	public abstract class BaseRoleGroupParameters : SetObjectProperties
	{
		// Token: 0x170023E9 RID: 9193
		// (get) Token: 0x06003CB0 RID: 15536 RVA: 0x000B63AA File Offset: 0x000B45AA
		// (set) Token: 0x06003CB1 RID: 15537 RVA: 0x000B63BC File Offset: 0x000B45BC
		[DataMember]
		public string Name
		{
			get
			{
				return (string)base[ADObjectSchema.Name];
			}
			set
			{
				base[ADObjectSchema.Name] = value;
			}
		}

		// Token: 0x170023EA RID: 9194
		// (get) Token: 0x06003CB2 RID: 15538 RVA: 0x000B63CA File Offset: 0x000B45CA
		// (set) Token: 0x06003CB3 RID: 15539 RVA: 0x000B63DC File Offset: 0x000B45DC
		[DataMember]
		public string Description
		{
			get
			{
				return (string)base[ADRecipientSchema.Description];
			}
			set
			{
				base[ADRecipientSchema.Description] = value.Trim();
			}
		}

		// Token: 0x170023EB RID: 9195
		// (get) Token: 0x06003CB4 RID: 15540 RVA: 0x000B63EF File Offset: 0x000B45EF
		// (set) Token: 0x06003CB5 RID: 15541 RVA: 0x000B63F7 File Offset: 0x000B45F7
		[DataMember]
		public AggregatedScope AggregatedScope { get; set; }

		// Token: 0x170023EC RID: 9196
		// (get) Token: 0x06003CB6 RID: 15542 RVA: 0x000B6400 File Offset: 0x000B4600
		public bool IsScopeModified
		{
			get
			{
				return this.AggregatedScope != null;
			}
		}

		// Token: 0x170023ED RID: 9197
		// (get) Token: 0x06003CB7 RID: 15543 RVA: 0x000B640E File Offset: 0x000B460E
		public string ManagementScopeId
		{
			get
			{
				if (this.IsScopeModified)
				{
					return this.AggregatedScope.ID;
				}
				return null;
			}
		}

		// Token: 0x170023EE RID: 9198
		// (get) Token: 0x06003CB8 RID: 15544 RVA: 0x000B6425 File Offset: 0x000B4625
		public bool IsOrganizationalUnit
		{
			get
			{
				return this.IsScopeModified && this.AggregatedScope.IsOrganizationalUnit;
			}
		}

		// Token: 0x170023EF RID: 9199
		// (get) Token: 0x06003CB9 RID: 15545 RVA: 0x000B643C File Offset: 0x000B463C
		// (set) Token: 0x06003CBA RID: 15546 RVA: 0x000B6444 File Offset: 0x000B4644
		public ManagementScopeRow ManagementScopeRow { get; set; }

		// Token: 0x170023F0 RID: 9200
		// (get) Token: 0x06003CBB RID: 15547 RVA: 0x000B644D File Offset: 0x000B464D
		// (set) Token: 0x06003CBC RID: 15548 RVA: 0x000B6455 File Offset: 0x000B4655
		public AdminRoleGroupObject OriginalObject { get; set; }

		// Token: 0x170023F1 RID: 9201
		// (get) Token: 0x06003CBD RID: 15549 RVA: 0x000B645E File Offset: 0x000B465E
		// (set) Token: 0x06003CBE RID: 15550 RVA: 0x000B6466 File Offset: 0x000B4666
		public ExtendedOrganizationalUnit OrganizationalUnitRow { get; set; }

		// Token: 0x170023F2 RID: 9202
		// (get) Token: 0x06003CBF RID: 15551 RVA: 0x000B646F File Offset: 0x000B466F
		public override string RbacScope
		{
			get
			{
				return "@R:Organization";
			}
		}
	}
}
