using System;
using System.Collections.Generic;
using System.Management.Automation;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020003EF RID: 1007
	[ObjectScope(ConfigScopes.Global)]
	[Serializable]
	public class DeprecatedLoadBalanceSettings : ADConfigurationObject
	{
		// Token: 0x17000CFB RID: 3323
		// (get) Token: 0x06002E26 RID: 11814 RVA: 0x000BBE33 File Offset: 0x000BA033
		internal override ADObjectSchema Schema
		{
			get
			{
				return DeprecatedLoadBalanceSettings.schema;
			}
		}

		// Token: 0x17000CFC RID: 3324
		// (get) Token: 0x06002E27 RID: 11815 RVA: 0x000BBE3A File Offset: 0x000BA03A
		internal override string MostDerivedObjectClass
		{
			get
			{
				return DeprecatedLoadBalanceSettings.mostDerivedClass;
			}
		}

		// Token: 0x17000CFD RID: 3325
		// (get) Token: 0x06002E29 RID: 11817 RVA: 0x000BBE49 File Offset: 0x000BA049
		// (set) Token: 0x06002E2A RID: 11818 RVA: 0x000BBE5B File Offset: 0x000BA05B
		public MultiValuedProperty<ADObjectId> IncludedMailboxDatabases
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[DeprecatedLoadBalanceSettingsSchema.IncludedMailboxDatabases];
			}
			internal set
			{
				this[DeprecatedLoadBalanceSettingsSchema.IncludedMailboxDatabases] = value;
			}
		}

		// Token: 0x17000CFE RID: 3326
		// (get) Token: 0x06002E2B RID: 11819 RVA: 0x000BBE69 File Offset: 0x000BA069
		// (set) Token: 0x06002E2C RID: 11820 RVA: 0x000BBE7B File Offset: 0x000BA07B
		[Parameter]
		public bool UseIncludedMailboxDatabases
		{
			get
			{
				return (bool)this[DeprecatedLoadBalanceSettingsSchema.UseIncludedMailboxDatabases];
			}
			set
			{
				this[DeprecatedLoadBalanceSettingsSchema.UseIncludedMailboxDatabases] = value;
			}
		}

		// Token: 0x17000CFF RID: 3327
		// (get) Token: 0x06002E2D RID: 11821 RVA: 0x000BBE8E File Offset: 0x000BA08E
		// (set) Token: 0x06002E2E RID: 11822 RVA: 0x000BBEA0 File Offset: 0x000BA0A0
		public MultiValuedProperty<ADObjectId> ExcludedMailboxDatabases
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[DeprecatedLoadBalanceSettingsSchema.ExcludedMailboxDatabases];
			}
			internal set
			{
				this[DeprecatedLoadBalanceSettingsSchema.ExcludedMailboxDatabases] = value;
			}
		}

		// Token: 0x17000D00 RID: 3328
		// (get) Token: 0x06002E2F RID: 11823 RVA: 0x000BBEAE File Offset: 0x000BA0AE
		// (set) Token: 0x06002E30 RID: 11824 RVA: 0x000BBEC0 File Offset: 0x000BA0C0
		[Parameter]
		public bool UseExcludedMailboxDatabases
		{
			get
			{
				return (bool)this[DeprecatedLoadBalanceSettingsSchema.UseExcludedMailboxDatabases];
			}
			set
			{
				this[DeprecatedLoadBalanceSettingsSchema.UseExcludedMailboxDatabases] = value;
			}
		}

		// Token: 0x06002E31 RID: 11825 RVA: 0x000BBED3 File Offset: 0x000BA0D3
		protected override void ValidateWrite(List<ValidationError> errors)
		{
			base.ValidateWrite(errors);
			if (this.UseIncludedMailboxDatabases && this.UseExcludedMailboxDatabases)
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.LoadBalanceCannotUseBothInclusionLists, DeprecatedLoadBalanceSettingsSchema.UseExcludedMailboxDatabases, this.UseIncludedMailboxDatabases));
			}
		}

		// Token: 0x17000D01 RID: 3329
		// (get) Token: 0x06002E32 RID: 11826 RVA: 0x000BBF0C File Offset: 0x000BA10C
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x04001F02 RID: 7938
		private static DeprecatedLoadBalanceSettingsSchema schema = ObjectSchema.GetInstance<DeprecatedLoadBalanceSettingsSchema>();

		// Token: 0x04001F03 RID: 7939
		private static string mostDerivedClass = "msExchLoadBalancingSettings";
	}
}
