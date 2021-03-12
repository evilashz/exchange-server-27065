using System;
using System.Management.Automation;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000427 RID: 1063
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public sealed class RetentionPolicy : MailboxPolicy
	{
		// Token: 0x17000D9E RID: 3486
		// (get) Token: 0x06002FCF RID: 12239 RVA: 0x000C13F1 File Offset: 0x000BF5F1
		internal override ADObjectSchema Schema
		{
			get
			{
				return RetentionPolicy.schema;
			}
		}

		// Token: 0x17000D9F RID: 3487
		// (get) Token: 0x06002FD0 RID: 12240 RVA: 0x000C13F8 File Offset: 0x000BF5F8
		internal override string MostDerivedObjectClass
		{
			get
			{
				return RetentionPolicy.mostDerivedClass;
			}
		}

		// Token: 0x17000DA0 RID: 3488
		// (get) Token: 0x06002FD1 RID: 12241 RVA: 0x000C13FF File Offset: 0x000BF5FF
		internal override ADObjectId ParentPath
		{
			get
			{
				return RetentionPolicy.parentPath;
			}
		}

		// Token: 0x17000DA1 RID: 3489
		// (get) Token: 0x06002FD2 RID: 12242 RVA: 0x000C1406 File Offset: 0x000BF606
		internal override bool IsShareable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000DA2 RID: 3490
		// (get) Token: 0x06002FD3 RID: 12243 RVA: 0x000C1409 File Offset: 0x000BF609
		// (set) Token: 0x06002FD4 RID: 12244 RVA: 0x000C143E File Offset: 0x000BF63E
		[Parameter(Mandatory = false)]
		public Guid RetentionId
		{
			get
			{
				if ((Guid)this[RetentionPolicySchema.RetentionId] == Guid.Empty)
				{
					return base.Guid;
				}
				return (Guid)this[RetentionPolicySchema.RetentionId];
			}
			set
			{
				this[RetentionPolicySchema.RetentionId] = value;
			}
		}

		// Token: 0x17000DA3 RID: 3491
		// (get) Token: 0x06002FD5 RID: 12245 RVA: 0x000C1451 File Offset: 0x000BF651
		// (set) Token: 0x06002FD6 RID: 12246 RVA: 0x000C1463 File Offset: 0x000BF663
		public MultiValuedProperty<ADObjectId> RetentionPolicyTagLinks
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[RetentionPolicySchema.RetentionPolicyTagLinks];
			}
			set
			{
				this[RetentionPolicySchema.RetentionPolicyTagLinks] = value;
			}
		}

		// Token: 0x06002FD7 RID: 12247 RVA: 0x000C1474 File Offset: 0x000BF674
		internal override bool CheckForAssociatedUsers()
		{
			QueryFilter filter = new AndFilter(new QueryFilter[]
			{
				new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.DistinguishedName, base.Id.DistinguishedName),
				new ExistsFilter(RetentionPolicySchema.AssociatedUsers)
			});
			base.Session.SessionSettings.IsSharedConfigChecked = true;
			RetentionPolicy[] array = base.Session.Find<RetentionPolicy>(null, QueryScope.SubTree, filter, null, 1);
			return array != null && array.Length > 0;
		}

		// Token: 0x17000DA4 RID: 3492
		// (get) Token: 0x06002FD8 RID: 12248 RVA: 0x000C14E2 File Offset: 0x000BF6E2
		// (set) Token: 0x06002FD9 RID: 12249 RVA: 0x000C14F4 File Offset: 0x000BF6F4
		public override bool IsDefault
		{
			get
			{
				return (bool)this[RetentionPolicySchema.IsDefault];
			}
			set
			{
				this[RetentionPolicySchema.IsDefault] = value;
			}
		}

		// Token: 0x17000DA5 RID: 3493
		// (get) Token: 0x06002FDA RID: 12250 RVA: 0x000C1507 File Offset: 0x000BF707
		// (set) Token: 0x06002FDB RID: 12251 RVA: 0x000C1519 File Offset: 0x000BF719
		public bool IsDefaultArbitrationMailbox
		{
			get
			{
				return (bool)this[RetentionPolicySchema.IsDefaultArbitrationMailbox];
			}
			set
			{
				this[RetentionPolicySchema.IsDefaultArbitrationMailbox] = value;
			}
		}

		// Token: 0x17000DA6 RID: 3494
		// (get) Token: 0x06002FDC RID: 12252 RVA: 0x000C152C File Offset: 0x000BF72C
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return RetentionPolicy.RetentionPolicyVersion;
			}
		}

		// Token: 0x06002FDD RID: 12253 RVA: 0x000C1533 File Offset: 0x000BF733
		internal override void Initialize()
		{
			if (base.ExchangeVersion == RetentionPolicy.E14RetentionPolicyMajorVersion)
			{
				this.propertyBag.SetField(this.propertyBag.ObjectVersionPropertyDefinition, RetentionPolicy.E14RetentionPolicyFullVersion);
			}
		}

		// Token: 0x17000DA7 RID: 3495
		// (get) Token: 0x06002FDE RID: 12254 RVA: 0x000C1564 File Offset: 0x000BF764
		internal override QueryFilter VersioningFilter
		{
			get
			{
				ExchangeObjectVersion e14RetentionPolicyMajorVersion = RetentionPolicy.E14RetentionPolicyMajorVersion;
				ExchangeObjectVersion nextMajorVersion = e14RetentionPolicyMajorVersion.NextMajorVersion;
				return new AndFilter(new QueryFilter[]
				{
					new ComparisonFilter(ComparisonOperator.GreaterThanOrEqual, ADObjectSchema.ExchangeVersion, e14RetentionPolicyMajorVersion),
					new ComparisonFilter(ComparisonOperator.LessThan, ADObjectSchema.ExchangeVersion, nextMajorVersion)
				});
			}
		}

		// Token: 0x04002046 RID: 8262
		private static RetentionPolicySchema schema = ObjectSchema.GetInstance<RetentionPolicySchema>();

		// Token: 0x04002047 RID: 8263
		private static string mostDerivedClass = "msExchMailboxRecipientTemplate";

		// Token: 0x04002048 RID: 8264
		private static ADObjectId parentPath = new ADObjectId("CN=Retention Policies Container");

		// Token: 0x04002049 RID: 8265
		internal static readonly ExchangeObjectVersion E14RetentionPolicyMajorVersion = ExchangeObjectVersion.Exchange2010.NextMajorVersion;

		// Token: 0x0400204A RID: 8266
		internal static readonly ExchangeObjectVersion E14RetentionPolicyFullVersion = new ExchangeObjectVersion(1, 0, ExchangeObjectVersion.Exchange2010.ExchangeBuild);

		// Token: 0x0400204B RID: 8267
		internal static readonly ExchangeObjectVersion RetentionPolicyVersion = RetentionPolicy.E14RetentionPolicyFullVersion;
	}
}
