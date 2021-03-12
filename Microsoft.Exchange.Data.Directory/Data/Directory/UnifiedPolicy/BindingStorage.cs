using System;

namespace Microsoft.Exchange.Data.Directory.UnifiedPolicy
{
	// Token: 0x02000A15 RID: 2581
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public class BindingStorage : UnifiedPolicyStorageBase
	{
		// Token: 0x0600774F RID: 30543 RVA: 0x00188C42 File Offset: 0x00186E42
		public BindingStorage()
		{
			base.SetObjectClass(this.MostDerivedObjectClass);
		}

		// Token: 0x17002A98 RID: 10904
		// (get) Token: 0x06007750 RID: 30544 RVA: 0x00188C56 File Offset: 0x00186E56
		internal override string MostDerivedObjectClass
		{
			get
			{
				return BindingStorage.mostDerivedClass;
			}
		}

		// Token: 0x17002A99 RID: 10905
		// (get) Token: 0x06007751 RID: 30545 RVA: 0x00188C5D File Offset: 0x00186E5D
		internal override ADObjectSchema Schema
		{
			get
			{
				return BindingStorage.schema;
			}
		}

		// Token: 0x17002A9A RID: 10906
		// (get) Token: 0x06007752 RID: 30546 RVA: 0x00188C64 File Offset: 0x00186E64
		// (set) Token: 0x06007753 RID: 30547 RVA: 0x00188C76 File Offset: 0x00186E76
		public Guid PolicyId
		{
			get
			{
				return (Guid)this[BindingStorageSchema.PolicyId];
			}
			set
			{
				this[BindingStorageSchema.PolicyId] = value;
			}
		}

		// Token: 0x17002A9B RID: 10907
		// (get) Token: 0x06007754 RID: 30548 RVA: 0x00188C89 File Offset: 0x00186E89
		// (set) Token: 0x06007755 RID: 30549 RVA: 0x00188C9B File Offset: 0x00186E9B
		public MultiValuedProperty<string> Scopes
		{
			get
			{
				return (MultiValuedProperty<string>)this[BindingStorageSchema.Scopes];
			}
			set
			{
				this[BindingStorageSchema.Scopes] = value;
			}
		}

		// Token: 0x17002A9C RID: 10908
		// (get) Token: 0x06007756 RID: 30550 RVA: 0x00188CA9 File Offset: 0x00186EA9
		// (set) Token: 0x06007757 RID: 30551 RVA: 0x00188CBB File Offset: 0x00186EBB
		internal MultiValuedProperty<string> DeletedScopes
		{
			get
			{
				return (MultiValuedProperty<string>)this[BindingStorageSchema.DeletedScopes];
			}
			set
			{
				this[BindingStorageSchema.DeletedScopes] = value;
			}
		}

		// Token: 0x17002A9D RID: 10909
		// (get) Token: 0x06007758 RID: 30552 RVA: 0x00188CC9 File Offset: 0x00186EC9
		// (set) Token: 0x06007759 RID: 30553 RVA: 0x00188CDB File Offset: 0x00186EDB
		public MultiValuedProperty<ScopeStorage> AppliedScopes
		{
			get
			{
				return (MultiValuedProperty<ScopeStorage>)this[BindingStorageSchema.AppliedScopes];
			}
			set
			{
				this[BindingStorageSchema.AppliedScopes] = value;
			}
		}

		// Token: 0x17002A9E RID: 10910
		// (get) Token: 0x0600775A RID: 30554 RVA: 0x00188CE9 File Offset: 0x00186EE9
		// (set) Token: 0x0600775B RID: 30555 RVA: 0x00188CFB File Offset: 0x00186EFB
		internal MultiValuedProperty<ScopeStorage> RemovedScopes
		{
			get
			{
				return (MultiValuedProperty<ScopeStorage>)this[BindingStorageSchema.RemovedScopes];
			}
			set
			{
				this[BindingStorageSchema.RemovedScopes] = value;
			}
		}

		// Token: 0x17002A9F RID: 10911
		// (get) Token: 0x0600775C RID: 30556 RVA: 0x00188D09 File Offset: 0x00186F09
		// (set) Token: 0x0600775D RID: 30557 RVA: 0x00188D11 File Offset: 0x00186F11
		internal object RawObject { get; set; }

		// Token: 0x04004C76 RID: 19574
		private static readonly BindingStorageSchema schema = ObjectSchema.GetInstance<BindingStorageSchema>();

		// Token: 0x04004C77 RID: 19575
		private static string mostDerivedClass = "msExchUnifiedBinding";
	}
}
