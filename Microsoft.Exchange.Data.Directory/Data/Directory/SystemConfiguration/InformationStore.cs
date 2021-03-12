using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200048B RID: 1163
	[ObjectScope(ConfigScopes.Server)]
	[Serializable]
	public class InformationStore : ADLegacyVersionableObject
	{
		// Token: 0x17000F90 RID: 3984
		// (get) Token: 0x060034AD RID: 13485 RVA: 0x000D1AA9 File Offset: 0x000CFCA9
		internal override ADObjectSchema Schema
		{
			get
			{
				return InformationStore.schema;
			}
		}

		// Token: 0x17000F91 RID: 3985
		// (get) Token: 0x060034AE RID: 13486 RVA: 0x000D1AB0 File Offset: 0x000CFCB0
		internal override string MostDerivedObjectClass
		{
			get
			{
				return InformationStore.mostDerivedClass;
			}
		}

		// Token: 0x060034AF RID: 13487 RVA: 0x000D1AB7 File Offset: 0x000CFCB7
		internal Container GetChildContainer(string commonName)
		{
			return base.Session.Read<Container>(base.Id.GetChildId(commonName));
		}

		// Token: 0x060034B0 RID: 13488 RVA: 0x000D1AD0 File Offset: 0x000CFCD0
		internal Container GetParentContainer()
		{
			return base.Session.Read<Container>(base.Id.Parent);
		}

		// Token: 0x17000F92 RID: 3986
		// (get) Token: 0x060034B1 RID: 13489 RVA: 0x000D1AE8 File Offset: 0x000CFCE8
		// (set) Token: 0x060034B2 RID: 13490 RVA: 0x000D1AFA File Offset: 0x000CFCFA
		internal int MaxStorageGroups
		{
			get
			{
				return (int)this[InformationStoreSchema.MaxStorageGroups];
			}
			set
			{
				this[InformationStoreSchema.MaxStorageGroups] = value;
			}
		}

		// Token: 0x17000F93 RID: 3987
		// (get) Token: 0x060034B3 RID: 13491 RVA: 0x000D1B0D File Offset: 0x000CFD0D
		// (set) Token: 0x060034B4 RID: 13492 RVA: 0x000D1B1F File Offset: 0x000CFD1F
		internal int MaxStoresPerGroup
		{
			get
			{
				return (int)this[InformationStoreSchema.MaxStoresPerGroup];
			}
			set
			{
				this[InformationStoreSchema.MaxStoresPerGroup] = value;
			}
		}

		// Token: 0x17000F94 RID: 3988
		// (get) Token: 0x060034B5 RID: 13493 RVA: 0x000D1B32 File Offset: 0x000CFD32
		// (set) Token: 0x060034B6 RID: 13494 RVA: 0x000D1B44 File Offset: 0x000CFD44
		internal int MaxStoresTotal
		{
			get
			{
				return (int)this[InformationStoreSchema.MaxStoresTotal];
			}
			set
			{
				this[InformationStoreSchema.MaxStoresTotal] = value;
			}
		}

		// Token: 0x17000F95 RID: 3989
		// (get) Token: 0x060034B7 RID: 13495 RVA: 0x000D1B57 File Offset: 0x000CFD57
		// (set) Token: 0x060034B8 RID: 13496 RVA: 0x000D1B69 File Offset: 0x000CFD69
		internal int MaxRestoreStorageGroups
		{
			get
			{
				return (int)this[InformationStoreSchema.MaxRestoreStorageGroups];
			}
			set
			{
				this[InformationStoreSchema.MaxRestoreStorageGroups] = value;
			}
		}

		// Token: 0x17000F96 RID: 3990
		// (get) Token: 0x060034B9 RID: 13497 RVA: 0x000D1B7C File Offset: 0x000CFD7C
		// (set) Token: 0x060034BA RID: 13498 RVA: 0x000D1B8E File Offset: 0x000CFD8E
		internal int? MinCachePages
		{
			get
			{
				return (int?)this[InformationStoreSchema.MinCachePages];
			}
			set
			{
				this[InformationStoreSchema.MinCachePages] = value;
			}
		}

		// Token: 0x17000F97 RID: 3991
		// (get) Token: 0x060034BB RID: 13499 RVA: 0x000D1BA1 File Offset: 0x000CFDA1
		// (set) Token: 0x060034BC RID: 13500 RVA: 0x000D1BB3 File Offset: 0x000CFDB3
		internal int? MaxCachePages
		{
			get
			{
				return (int?)this[InformationStoreSchema.MaxCachePages];
			}
			set
			{
				this[InformationStoreSchema.MaxCachePages] = value;
			}
		}

		// Token: 0x17000F98 RID: 3992
		// (get) Token: 0x060034BD RID: 13501 RVA: 0x000D1BC6 File Offset: 0x000CFDC6
		public bool? EnableOnlineDefragmentation
		{
			get
			{
				return (bool?)this[InformationStoreSchema.EnableOnlineDefragmentation];
			}
		}

		// Token: 0x17000F99 RID: 3993
		// (get) Token: 0x060034BE RID: 13502 RVA: 0x000D1BD8 File Offset: 0x000CFDD8
		internal int? MaxRpcThreads
		{
			get
			{
				return (int?)this[InformationStoreSchema.MaxRpcThreads];
			}
		}

		// Token: 0x040023E7 RID: 9191
		internal const int MaxStorageGroupsEnt = 100;

		// Token: 0x040023E8 RID: 9192
		internal const int MaxStoresPerGroupEnt = 5;

		// Token: 0x040023E9 RID: 9193
		internal const int MaxStoresTotalEnt = 100;

		// Token: 0x040023EA RID: 9194
		internal const int MaxRestoreStorageGroupsEnt = 1;

		// Token: 0x040023EB RID: 9195
		internal const int MaxStorageGroupsStd = 5;

		// Token: 0x040023EC RID: 9196
		internal const int MaxStoresPerGroupStd = 5;

		// Token: 0x040023ED RID: 9197
		internal const int MaxStoresTotalStd = 5;

		// Token: 0x040023EE RID: 9198
		internal const int MaxRestoreStorageGroupsStd = 1;

		// Token: 0x040023EF RID: 9199
		internal const int MaxStorageGroupsCoEx = 5;

		// Token: 0x040023F0 RID: 9200
		internal const int MaxStoresPerGroupCoEx = 5;

		// Token: 0x040023F1 RID: 9201
		internal const int MaxStoresTotalCoEx = 5;

		// Token: 0x040023F2 RID: 9202
		internal const int MaxRestoreStorageGroupsCoEx = 1;

		// Token: 0x040023F3 RID: 9203
		private static InformationStoreSchema schema = ObjectSchema.GetInstance<InformationStoreSchema>();

		// Token: 0x040023F4 RID: 9204
		private static string mostDerivedClass = "msExchInformationStore";
	}
}
