using System;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000829 RID: 2089
	internal class SyncForeignPrincipal : SyncObject
	{
		// Token: 0x060067B4 RID: 26548 RVA: 0x0016DE08 File Offset: 0x0016C008
		public SyncForeignPrincipal(SyncDirection syncDirection) : base(syncDirection)
		{
		}

		// Token: 0x170024B1 RID: 9393
		// (get) Token: 0x060067B5 RID: 26549 RVA: 0x0016DE11 File Offset: 0x0016C011
		public override SyncObjectSchema Schema
		{
			get
			{
				return SyncForeignPrincipal.schema;
			}
		}

		// Token: 0x170024B2 RID: 9394
		// (get) Token: 0x060067B6 RID: 26550 RVA: 0x0016DE18 File Offset: 0x0016C018
		internal override DirectoryObjectClass ObjectClass
		{
			get
			{
				return DirectoryObjectClass.ForeignPrincipal;
			}
		}

		// Token: 0x060067B7 RID: 26551 RVA: 0x0016DE1B File Offset: 0x0016C01B
		protected override DirectoryObject CreateDirectoryObject()
		{
			return new ForeignPrincipal();
		}

		// Token: 0x170024B3 RID: 9395
		// (get) Token: 0x060067B8 RID: 26552 RVA: 0x0016DE22 File Offset: 0x0016C022
		// (set) Token: 0x060067B9 RID: 26553 RVA: 0x0016DE34 File Offset: 0x0016C034
		public SyncProperty<string> DisplayName
		{
			get
			{
				return (SyncProperty<string>)base[SyncForeignPrincipalSchema.DisplayName];
			}
			set
			{
				base[SyncForeignPrincipalSchema.DisplayName] = value;
			}
		}

		// Token: 0x170024B4 RID: 9396
		// (get) Token: 0x060067BA RID: 26554 RVA: 0x0016DE42 File Offset: 0x0016C042
		// (set) Token: 0x060067BB RID: 26555 RVA: 0x0016DE54 File Offset: 0x0016C054
		public SyncProperty<string> Description
		{
			get
			{
				return (SyncProperty<string>)base[SyncForeignPrincipalSchema.Description];
			}
			set
			{
				base[SyncForeignPrincipalSchema.Description] = value;
			}
		}

		// Token: 0x170024B5 RID: 9397
		// (get) Token: 0x060067BC RID: 26556 RVA: 0x0016DE62 File Offset: 0x0016C062
		// (set) Token: 0x060067BD RID: 26557 RVA: 0x0016DE74 File Offset: 0x0016C074
		internal SyncProperty<string> LinkedPartnerGroupId
		{
			get
			{
				return (SyncProperty<string>)base[SyncForeignPrincipalSchema.LinkedPartnerGroupId];
			}
			set
			{
				base[SyncForeignPrincipalSchema.LinkedPartnerGroupId] = value;
			}
		}

		// Token: 0x170024B6 RID: 9398
		// (get) Token: 0x060067BE RID: 26558 RVA: 0x0016DE82 File Offset: 0x0016C082
		// (set) Token: 0x060067BF RID: 26559 RVA: 0x0016DE94 File Offset: 0x0016C094
		internal SyncProperty<string> LinkedPartnerOrganizationId
		{
			get
			{
				return (SyncProperty<string>)base[SyncForeignPrincipalSchema.LinkedPartnerOrganizationId];
			}
			set
			{
				base[SyncForeignPrincipalSchema.LinkedPartnerOrganizationId] = value;
			}
		}

		// Token: 0x0400444C RID: 17484
		private static readonly SyncForeignPrincipalSchema schema = ObjectSchema.GetInstance<SyncForeignPrincipalSchema>();
	}
}
