using System;
using System.Management.Automation;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020005C8 RID: 1480
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public class RemoteAccountPolicy : ADConfigurationObject
	{
		// Token: 0x17001663 RID: 5731
		// (get) Token: 0x06004434 RID: 17460 RVA: 0x000FFD35 File Offset: 0x000FDF35
		// (set) Token: 0x06004435 RID: 17461 RVA: 0x000FFD47 File Offset: 0x000FDF47
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan PollingInterval
		{
			get
			{
				return (EnhancedTimeSpan)this[RemoteAccountPolicySchema.PollingInterval];
			}
			set
			{
				this[RemoteAccountPolicySchema.PollingInterval] = value;
			}
		}

		// Token: 0x17001664 RID: 5732
		// (get) Token: 0x06004436 RID: 17462 RVA: 0x000FFD5A File Offset: 0x000FDF5A
		// (set) Token: 0x06004437 RID: 17463 RVA: 0x000FFD6C File Offset: 0x000FDF6C
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan TimeBeforeInactive
		{
			get
			{
				return (EnhancedTimeSpan)this[RemoteAccountPolicySchema.TimeBeforeInactive];
			}
			set
			{
				this[RemoteAccountPolicySchema.TimeBeforeInactive] = value;
			}
		}

		// Token: 0x17001665 RID: 5733
		// (get) Token: 0x06004438 RID: 17464 RVA: 0x000FFD7F File Offset: 0x000FDF7F
		// (set) Token: 0x06004439 RID: 17465 RVA: 0x000FFD91 File Offset: 0x000FDF91
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan TimeBeforeDormant
		{
			get
			{
				return (EnhancedTimeSpan)this[RemoteAccountPolicySchema.TimeBeforeDormant];
			}
			set
			{
				this[RemoteAccountPolicySchema.TimeBeforeDormant] = value;
			}
		}

		// Token: 0x17001666 RID: 5734
		// (get) Token: 0x0600443A RID: 17466 RVA: 0x000FFDA4 File Offset: 0x000FDFA4
		// (set) Token: 0x0600443B RID: 17467 RVA: 0x000FFDB6 File Offset: 0x000FDFB6
		[Parameter(Mandatory = false)]
		public int MaxSyncAccounts
		{
			get
			{
				return (int)this[RemoteAccountPolicySchema.MaxSyncAccounts];
			}
			set
			{
				this[RemoteAccountPolicySchema.MaxSyncAccounts] = value;
			}
		}

		// Token: 0x17001667 RID: 5735
		// (get) Token: 0x0600443C RID: 17468 RVA: 0x000FFDC9 File Offset: 0x000FDFC9
		// (set) Token: 0x0600443D RID: 17469 RVA: 0x000FFDDB File Offset: 0x000FDFDB
		[Parameter(Mandatory = false)]
		public bool SyncNowAllowed
		{
			get
			{
				return (bool)this[RemoteAccountPolicySchema.SyncNowAllowed];
			}
			set
			{
				this[RemoteAccountPolicySchema.SyncNowAllowed] = value;
			}
		}

		// Token: 0x17001668 RID: 5736
		// (get) Token: 0x0600443E RID: 17470 RVA: 0x000FFDEE File Offset: 0x000FDFEE
		internal override ADObjectSchema Schema
		{
			get
			{
				return RemoteAccountPolicy.SchemaObject;
			}
		}

		// Token: 0x17001669 RID: 5737
		// (get) Token: 0x0600443F RID: 17471 RVA: 0x000FFDF5 File Offset: 0x000FDFF5
		internal override string MostDerivedObjectClass
		{
			get
			{
				return "msExchSyncAccountsPolicy";
			}
		}

		// Token: 0x1700166A RID: 5738
		// (get) Token: 0x06004440 RID: 17472 RVA: 0x000FFDFC File Offset: 0x000FDFFC
		internal override ADObjectId ParentPath
		{
			get
			{
				return RemoteAccountPolicy.RemoteAccountPolicysContainer;
			}
		}

		// Token: 0x1700166B RID: 5739
		// (get) Token: 0x06004441 RID: 17473 RVA: 0x000FFE03 File Offset: 0x000FE003
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x04002EA1 RID: 11937
		internal const string TaskNoun = "RemoteAccountPolicy";

		// Token: 0x04002EA2 RID: 11938
		internal const string LdapName = "msExchSyncAccountsPolicy";

		// Token: 0x04002EA3 RID: 11939
		private static readonly ADObjectId RemoteAccountPolicysContainer = new ADObjectId("CN=Remote Accounts Policies Container");

		// Token: 0x04002EA4 RID: 11940
		private static readonly RemoteAccountPolicySchema SchemaObject = ObjectSchema.GetInstance<RemoteAccountPolicySchema>();
	}
}
