using System;
using System.Collections.Generic;
using System.Management.Automation;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200042D RID: 1069
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public sealed class TeamMailboxProvisioningPolicy : MailboxPolicy
	{
		// Token: 0x06003018 RID: 12312 RVA: 0x000C224C File Offset: 0x000C044C
		internal static QueryFilter IsDefaultFilterBuilder(SinglePropertyFilter filter)
		{
			return ADObject.BoolFilterBuilder(filter, new BitMaskAndFilter(MailboxPolicySchema.MailboxPolicyFlags, 1UL));
		}

		// Token: 0x17000DBA RID: 3514
		// (get) Token: 0x06003019 RID: 12313 RVA: 0x000C2260 File Offset: 0x000C0460
		internal override ADObjectSchema Schema
		{
			get
			{
				return TeamMailboxProvisioningPolicy.schema;
			}
		}

		// Token: 0x17000DBB RID: 3515
		// (get) Token: 0x0600301A RID: 12314 RVA: 0x000C2267 File Offset: 0x000C0467
		internal override string MostDerivedObjectClass
		{
			get
			{
				return TeamMailboxProvisioningPolicy.mostDerivedClass;
			}
		}

		// Token: 0x17000DBC RID: 3516
		// (get) Token: 0x0600301B RID: 12315 RVA: 0x000C226E File Offset: 0x000C046E
		internal override ADObjectId ParentPath
		{
			get
			{
				return TeamMailboxProvisioningPolicy.parentPath;
			}
		}

		// Token: 0x17000DBD RID: 3517
		// (get) Token: 0x0600301C RID: 12316 RVA: 0x000C2275 File Offset: 0x000C0475
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2012;
			}
		}

		// Token: 0x0600301D RID: 12317 RVA: 0x000C227C File Offset: 0x000C047C
		internal override bool CheckForAssociatedUsers()
		{
			return false;
		}

		// Token: 0x17000DBE RID: 3518
		// (get) Token: 0x0600301E RID: 12318 RVA: 0x000C227F File Offset: 0x000C047F
		internal override bool IsShareable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600301F RID: 12319 RVA: 0x000C2284 File Offset: 0x000C0484
		protected override void ValidateWrite(List<ValidationError> errors)
		{
			base.ValidateWrite(errors);
			if (0 < this.IssueWarningQuota.CompareTo(this.ProhibitSendReceiveQuota))
			{
				errors.Add(new ObjectValidationError(DirectoryStrings.ErrorProperty1GtProperty2(TeamMailboxProvisioningPolicySchema.IssueWarningQuota.Name, this.IssueWarningQuota.ToString(), TeamMailboxProvisioningPolicySchema.ProhibitSendReceiveQuota.Name, this.ProhibitSendReceiveQuota.ToString()), this.Identity, string.Empty));
			}
		}

		// Token: 0x17000DBF RID: 3519
		// (get) Token: 0x06003020 RID: 12320 RVA: 0x000C2306 File Offset: 0x000C0506
		// (set) Token: 0x06003021 RID: 12321 RVA: 0x000C2318 File Offset: 0x000C0518
		public override bool IsDefault
		{
			get
			{
				return (bool)this[TeamMailboxProvisioningPolicySchema.IsDefault];
			}
			set
			{
				this[TeamMailboxProvisioningPolicySchema.IsDefault] = value;
			}
		}

		// Token: 0x17000DC0 RID: 3520
		// (get) Token: 0x06003022 RID: 12322 RVA: 0x000C232B File Offset: 0x000C052B
		// (set) Token: 0x06003023 RID: 12323 RVA: 0x000C233D File Offset: 0x000C053D
		[Parameter(Mandatory = false)]
		public ByteQuantifiedSize MaxReceiveSize
		{
			get
			{
				return (ByteQuantifiedSize)this[TeamMailboxProvisioningPolicySchema.MaxReceiveSize];
			}
			set
			{
				this[TeamMailboxProvisioningPolicySchema.MaxReceiveSize] = value;
			}
		}

		// Token: 0x17000DC1 RID: 3521
		// (get) Token: 0x06003024 RID: 12324 RVA: 0x000C2350 File Offset: 0x000C0550
		// (set) Token: 0x06003025 RID: 12325 RVA: 0x000C2362 File Offset: 0x000C0562
		[Parameter(Mandatory = false)]
		public ByteQuantifiedSize IssueWarningQuota
		{
			get
			{
				return (ByteQuantifiedSize)this[TeamMailboxProvisioningPolicySchema.IssueWarningQuota];
			}
			set
			{
				this[TeamMailboxProvisioningPolicySchema.IssueWarningQuota] = value;
			}
		}

		// Token: 0x17000DC2 RID: 3522
		// (get) Token: 0x06003026 RID: 12326 RVA: 0x000C2375 File Offset: 0x000C0575
		// (set) Token: 0x06003027 RID: 12327 RVA: 0x000C2387 File Offset: 0x000C0587
		[Parameter(Mandatory = false)]
		public ByteQuantifiedSize ProhibitSendReceiveQuota
		{
			get
			{
				return (ByteQuantifiedSize)this[TeamMailboxProvisioningPolicySchema.ProhibitSendReceiveQuota];
			}
			set
			{
				this[TeamMailboxProvisioningPolicySchema.ProhibitSendReceiveQuota] = value;
			}
		}

		// Token: 0x17000DC3 RID: 3523
		// (get) Token: 0x06003028 RID: 12328 RVA: 0x000C239A File Offset: 0x000C059A
		// (set) Token: 0x06003029 RID: 12329 RVA: 0x000C23AC File Offset: 0x000C05AC
		[Parameter(Mandatory = false)]
		public bool DefaultAliasPrefixEnabled
		{
			get
			{
				return (bool)this[TeamMailboxProvisioningPolicySchema.DefaultAliasPrefixEnabled];
			}
			set
			{
				this[TeamMailboxProvisioningPolicySchema.DefaultAliasPrefixEnabled] = value;
			}
		}

		// Token: 0x17000DC4 RID: 3524
		// (get) Token: 0x0600302A RID: 12330 RVA: 0x000C23BF File Offset: 0x000C05BF
		// (set) Token: 0x0600302B RID: 12331 RVA: 0x000C23D1 File Offset: 0x000C05D1
		[Parameter(Mandatory = false)]
		public string AliasPrefix
		{
			get
			{
				return (string)this[TeamMailboxProvisioningPolicySchema.AliasPrefix];
			}
			set
			{
				this[TeamMailboxProvisioningPolicySchema.AliasPrefix] = value;
			}
		}

		// Token: 0x0400206F RID: 8303
		private static TeamMailboxProvisioningPolicySchema schema = ObjectSchema.GetInstance<TeamMailboxProvisioningPolicySchema>();

		// Token: 0x04002070 RID: 8304
		private static string mostDerivedClass = "msExchTeamMailboxProvisioningPolicy";

		// Token: 0x04002071 RID: 8305
		private static ADObjectId parentPath = new ADObjectId("CN=Team Mailbox Provisioning Policies");
	}
}
