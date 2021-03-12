using System;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Exchange.Data.Directory.UnifiedPolicy
{
	// Token: 0x02000A10 RID: 2576
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public class AssociationStorage : UnifiedPolicyStorageBase
	{
		// Token: 0x06007721 RID: 30497 RVA: 0x0018853D File Offset: 0x0018673D
		public AssociationStorage()
		{
			base.SetObjectClass(this.MostDerivedObjectClass);
		}

		// Token: 0x17002A88 RID: 10888
		// (get) Token: 0x06007722 RID: 30498 RVA: 0x00188551 File Offset: 0x00186751
		internal override string MostDerivedObjectClass
		{
			get
			{
				return AssociationStorage.mostDerivedClass;
			}
		}

		// Token: 0x17002A89 RID: 10889
		// (get) Token: 0x06007723 RID: 30499 RVA: 0x00188558 File Offset: 0x00186758
		internal override ADObjectSchema Schema
		{
			get
			{
				return AssociationStorage.schema;
			}
		}

		// Token: 0x17002A8A RID: 10890
		// (get) Token: 0x06007724 RID: 30500 RVA: 0x0018855F File Offset: 0x0018675F
		// (set) Token: 0x06007725 RID: 30501 RVA: 0x00188571 File Offset: 0x00186771
		public AssociationType AssociationType
		{
			get
			{
				return (AssociationType)this[AssociationStorageSchema.Type];
			}
			set
			{
				this[AssociationStorageSchema.Type] = value;
			}
		}

		// Token: 0x17002A8B RID: 10891
		// (get) Token: 0x06007726 RID: 30502 RVA: 0x00188584 File Offset: 0x00186784
		// (set) Token: 0x06007727 RID: 30503 RVA: 0x00188596 File Offset: 0x00186796
		public bool AllowOverride
		{
			get
			{
				return (bool)this[AssociationStorageSchema.AllowOverride];
			}
			set
			{
				this[AssociationStorageSchema.AllowOverride] = value;
			}
		}

		// Token: 0x17002A8C RID: 10892
		// (get) Token: 0x06007728 RID: 30504 RVA: 0x001885A9 File Offset: 0x001867A9
		// (set) Token: 0x06007729 RID: 30505 RVA: 0x001885BB File Offset: 0x001867BB
		public Guid? DefaultPolicyId
		{
			get
			{
				return (Guid?)this[AssociationStorageSchema.DefaultPolicyId];
			}
			set
			{
				this[AssociationStorageSchema.DefaultPolicyId] = value;
			}
		}

		// Token: 0x17002A8D RID: 10893
		// (get) Token: 0x0600772A RID: 30506 RVA: 0x001885CE File Offset: 0x001867CE
		// (set) Token: 0x0600772B RID: 30507 RVA: 0x001885E0 File Offset: 0x001867E0
		public string Scope
		{
			get
			{
				return (string)this[AssociationStorageSchema.Scope];
			}
			set
			{
				this[AssociationStorageSchema.Scope] = value;
			}
		}

		// Token: 0x17002A8E RID: 10894
		// (get) Token: 0x0600772C RID: 30508 RVA: 0x001885EE File Offset: 0x001867EE
		// (set) Token: 0x0600772D RID: 30509 RVA: 0x00188600 File Offset: 0x00186800
		public MultiValuedProperty<Guid> PolicyIds
		{
			get
			{
				return (MultiValuedProperty<Guid>)this[AssociationStorageSchema.PolicyIds];
			}
			set
			{
				this[AssociationStorageSchema.PolicyIds] = value;
			}
		}

		// Token: 0x17002A8F RID: 10895
		// (get) Token: 0x0600772E RID: 30510 RVA: 0x0018860E File Offset: 0x0018680E
		// (set) Token: 0x0600772F RID: 30511 RVA: 0x00188620 File Offset: 0x00186820
		public string Comments
		{
			get
			{
				return (string)this[AssociationStorageSchema.Comments];
			}
			set
			{
				this[AssociationStorageSchema.Comments] = value;
			}
		}

		// Token: 0x17002A90 RID: 10896
		// (get) Token: 0x06007730 RID: 30512 RVA: 0x0018862E File Offset: 0x0018682E
		// (set) Token: 0x06007731 RID: 30513 RVA: 0x00188640 File Offset: 0x00186840
		public string Description
		{
			get
			{
				return (string)this[AssociationStorageSchema.Description];
			}
			set
			{
				this[AssociationStorageSchema.Description] = value;
			}
		}

		// Token: 0x04004C62 RID: 19554
		private static readonly AssociationStorageSchema schema = ObjectSchema.GetInstance<AssociationStorageSchema>();

		// Token: 0x04004C63 RID: 19555
		private static string mostDerivedClass = "msExchUnifiedAssociation";
	}
}
