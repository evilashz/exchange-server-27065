using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200053A RID: 1338
	public class RoleAssignmentObjectResolverRow : AdObjectResolverRow
	{
		// Token: 0x06003F45 RID: 16197 RVA: 0x000BE91C File Offset: 0x000BCB1C
		public RoleAssignmentObjectResolverRow(ADRawEntry entry) : base(entry)
		{
		}

		// Token: 0x170024B3 RID: 9395
		// (get) Token: 0x06003F46 RID: 16198 RVA: 0x000BE925 File Offset: 0x000BCB25
		// (set) Token: 0x06003F47 RID: 16199 RVA: 0x000BE932 File Offset: 0x000BCB32
		public string Role
		{
			get
			{
				return this.RoleIdentity.Name;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170024B4 RID: 9396
		// (get) Token: 0x06003F48 RID: 16200 RVA: 0x000BE939 File Offset: 0x000BCB39
		// (set) Token: 0x06003F49 RID: 16201 RVA: 0x000BE950 File Offset: 0x000BCB50
		public ADObjectId RoleIdentity
		{
			get
			{
				return (ADObjectId)base.ADRawEntry[ExchangeRoleAssignmentSchema.Role];
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170024B5 RID: 9397
		// (get) Token: 0x06003F4A RID: 16202 RVA: 0x000BE958 File Offset: 0x000BCB58
		public ConfigWriteScopeType ConfigWriteScopeType
		{
			get
			{
				ConfigWriteScopeType? configWriteScopeType = new ConfigWriteScopeType?((ConfigWriteScopeType)base.ADRawEntry[ExchangeRoleAssignmentSchema.ConfigWriteScope]);
				if (configWriteScopeType == null)
				{
					configWriteScopeType = new ConfigWriteScopeType?(ConfigWriteScopeType.None);
				}
				return configWriteScopeType.Value;
			}
		}

		// Token: 0x170024B6 RID: 9398
		// (get) Token: 0x06003F4B RID: 16203 RVA: 0x000BE999 File Offset: 0x000BCB99
		public RecipientWriteScopeType RecipientWriteScopeType
		{
			get
			{
				return (RecipientWriteScopeType)base.ADRawEntry[ExchangeRoleAssignmentSchema.RecipientWriteScope];
			}
		}

		// Token: 0x170024B7 RID: 9399
		// (get) Token: 0x06003F4C RID: 16204 RVA: 0x000BE9B0 File Offset: 0x000BCBB0
		public ADObjectId CustomConfigWriteScope
		{
			get
			{
				return (ADObjectId)base.ADRawEntry[ExchangeRoleAssignmentSchema.CustomConfigWriteScope];
			}
		}

		// Token: 0x170024B8 RID: 9400
		// (get) Token: 0x06003F4D RID: 16205 RVA: 0x000BE9C7 File Offset: 0x000BCBC7
		// (set) Token: 0x06003F4E RID: 16206 RVA: 0x000BE9E9 File Offset: 0x000BCBE9
		public ADObjectId CustomRecipientWriteScope
		{
			get
			{
				if (this.RecipientWriteScopeType == RecipientWriteScopeType.OU)
				{
					return null;
				}
				return (ADObjectId)base.ADRawEntry[ExchangeRoleAssignmentSchema.CustomRecipientWriteScope];
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170024B9 RID: 9401
		// (get) Token: 0x06003F4F RID: 16207 RVA: 0x000BE9F0 File Offset: 0x000BCBF0
		public ADObjectId RecipientOrganizationUnitScope
		{
			get
			{
				if (this.RecipientWriteScopeType == RecipientWriteScopeType.OU)
				{
					return (ADObjectId)base.ADRawEntry[ExchangeRoleAssignmentSchema.CustomRecipientWriteScope];
				}
				return null;
			}
		}

		// Token: 0x170024BA RID: 9402
		// (get) Token: 0x06003F50 RID: 16208 RVA: 0x000BEA14 File Offset: 0x000BCC14
		// (set) Token: 0x06003F51 RID: 16209 RVA: 0x000BEA64 File Offset: 0x000BCC64
		public bool IsDelegating
		{
			get
			{
				RoleAssignmentDelegationType? roleAssignmentDelegationType = new RoleAssignmentDelegationType?((RoleAssignmentDelegationType)base.ADRawEntry[ExchangeRoleAssignmentSchema.RoleAssignmentDelegationType]);
				return roleAssignmentDelegationType != null && roleAssignmentDelegationType != RoleAssignmentDelegationType.Regular;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x040028F0 RID: 10480
		public new static PropertyDefinition[] Properties = new List<PropertyDefinition>(AdObjectResolverRow.Properties)
		{
			ADRecipientSchema.DisplayName,
			ADRecipientSchema.RecipientTypeDetails,
			ExchangeRoleAssignmentSchema.ConfigWriteScope,
			ExchangeRoleAssignmentSchema.RecipientWriteScope,
			ExchangeRoleAssignmentSchema.Role,
			ExchangeRoleAssignmentSchema.CustomConfigWriteScope,
			ExchangeRoleAssignmentSchema.CustomRecipientWriteScope
		}.ToArray();
	}
}
