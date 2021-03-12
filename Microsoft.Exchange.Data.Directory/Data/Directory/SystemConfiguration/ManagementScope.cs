using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics.Components.Authorization;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200045C RID: 1116
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public class ManagementScope : ADConfigurationObject
	{
		// Token: 0x17000E75 RID: 3701
		// (get) Token: 0x06003221 RID: 12833 RVA: 0x000CB23F File Offset: 0x000C943F
		internal override ADObjectSchema Schema
		{
			get
			{
				return ManagementScope.schema;
			}
		}

		// Token: 0x17000E76 RID: 3702
		// (get) Token: 0x06003222 RID: 12834 RVA: 0x000CB246 File Offset: 0x000C9446
		internal override string MostDerivedObjectClass
		{
			get
			{
				return ManagementScope.mostDerivedClass;
			}
		}

		// Token: 0x17000E77 RID: 3703
		// (get) Token: 0x06003223 RID: 12835 RVA: 0x000CB24D File Offset: 0x000C944D
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ManagementScopeSchema.ExchangeManagementScope2010_SPVersion;
			}
		}

		// Token: 0x17000E78 RID: 3704
		// (get) Token: 0x06003224 RID: 12836 RVA: 0x000CB254 File Offset: 0x000C9454
		// (set) Token: 0x06003225 RID: 12837 RVA: 0x000CB266 File Offset: 0x000C9466
		public ADObjectId RecipientRoot
		{
			get
			{
				return (ADObjectId)this[ManagementScopeSchema.RecipientRoot];
			}
			internal set
			{
				this[ManagementScopeSchema.RecipientRoot] = value;
			}
		}

		// Token: 0x17000E79 RID: 3705
		// (get) Token: 0x06003226 RID: 12838 RVA: 0x000CB274 File Offset: 0x000C9474
		// (set) Token: 0x06003227 RID: 12839 RVA: 0x000CB286 File Offset: 0x000C9486
		internal string Filter
		{
			get
			{
				return (string)this[ManagementScopeSchema.Filter];
			}
			set
			{
				this[ManagementScopeSchema.Filter] = value;
			}
		}

		// Token: 0x17000E7A RID: 3706
		// (get) Token: 0x06003228 RID: 12840 RVA: 0x000CB294 File Offset: 0x000C9494
		// (set) Token: 0x06003229 RID: 12841 RVA: 0x000CB2A6 File Offset: 0x000C94A6
		public string RecipientFilter
		{
			get
			{
				return (string)this[ManagementScopeSchema.RecipientFilter];
			}
			internal set
			{
				this[ManagementScopeSchema.RecipientFilter] = value;
			}
		}

		// Token: 0x17000E7B RID: 3707
		// (get) Token: 0x0600322A RID: 12842 RVA: 0x000CB2B4 File Offset: 0x000C94B4
		// (set) Token: 0x0600322B RID: 12843 RVA: 0x000CB2C6 File Offset: 0x000C94C6
		public string ServerFilter
		{
			get
			{
				return (string)this[ManagementScopeSchema.ServerFilter];
			}
			internal set
			{
				this[ManagementScopeSchema.ServerFilter] = value;
			}
		}

		// Token: 0x17000E7C RID: 3708
		// (get) Token: 0x0600322C RID: 12844 RVA: 0x000CB2D4 File Offset: 0x000C94D4
		// (set) Token: 0x0600322D RID: 12845 RVA: 0x000CB2E6 File Offset: 0x000C94E6
		public string DatabaseFilter
		{
			get
			{
				return (string)this[ManagementScopeSchema.DatabaseFilter];
			}
			internal set
			{
				this[ManagementScopeSchema.DatabaseFilter] = value;
			}
		}

		// Token: 0x17000E7D RID: 3709
		// (get) Token: 0x0600322E RID: 12846 RVA: 0x000CB2F4 File Offset: 0x000C94F4
		// (set) Token: 0x0600322F RID: 12847 RVA: 0x000CB306 File Offset: 0x000C9506
		public string TenantOrganizationFilter
		{
			get
			{
				return (string)this[ManagementScopeSchema.OrganizationFilter];
			}
			internal set
			{
				this[ManagementScopeSchema.OrganizationFilter] = value;
			}
		}

		// Token: 0x17000E7E RID: 3710
		// (get) Token: 0x06003230 RID: 12848 RVA: 0x000CB314 File Offset: 0x000C9514
		// (set) Token: 0x06003231 RID: 12849 RVA: 0x000CB326 File Offset: 0x000C9526
		public ScopeRestrictionType ScopeRestrictionType
		{
			get
			{
				return (ScopeRestrictionType)this[ManagementScopeSchema.ScopeRestrictionType];
			}
			internal set
			{
				this[ManagementScopeSchema.ScopeRestrictionType] = value;
			}
		}

		// Token: 0x17000E7F RID: 3711
		// (get) Token: 0x06003232 RID: 12850 RVA: 0x000CB339 File Offset: 0x000C9539
		// (set) Token: 0x06003233 RID: 12851 RVA: 0x000CB34B File Offset: 0x000C954B
		public bool Exclusive
		{
			get
			{
				return (bool)this[ManagementScopeSchema.Exclusive];
			}
			internal set
			{
				this[ManagementScopeSchema.Exclusive] = value;
			}
		}

		// Token: 0x17000E80 RID: 3712
		// (get) Token: 0x06003234 RID: 12852 RVA: 0x000CB35E File Offset: 0x000C955E
		// (set) Token: 0x06003235 RID: 12853 RVA: 0x000CB366 File Offset: 0x000C9566
		internal QueryFilter QueryFilter
		{
			get
			{
				return this.queryFilter;
			}
			set
			{
				this.queryFilter = value;
			}
		}

		// Token: 0x06003236 RID: 12854 RVA: 0x000CB370 File Offset: 0x000C9570
		protected override void ValidateRead(List<ValidationError> errors)
		{
			base.ValidateRead(errors);
			bool flag = ADObjectId.IsNullOrEmpty(this.RecipientRoot);
			switch (this.ScopeRestrictionType)
			{
			case ScopeRestrictionType.RecipientScope:
				break;
			case ScopeRestrictionType.ServerScope:
			case ScopeRestrictionType.PartnerDelegatedTenantScope:
			case ScopeRestrictionType.DatabaseScope:
				if (!flag)
				{
					errors.Add(new ObjectValidationError(DirectoryStrings.RootMustBeEmpty(this.ScopeRestrictionType), this.Identity, base.OriginatingServer));
				}
				break;
			default:
				errors.Add(new ObjectValidationError(DirectoryStrings.UnKnownScopeRestrictionType(this.ScopeRestrictionType.ToString(), base.Name), this.Identity, base.OriginatingServer));
				break;
			}
			if (string.IsNullOrEmpty(this.Filter))
			{
				errors.Add(new ObjectValidationError(DirectoryStrings.FilterCannotBeEmpty(this.ScopeRestrictionType), this.Identity, base.OriginatingServer));
			}
			if (this.Exclusive && this.ScopeRestrictionType == ScopeRestrictionType.PartnerDelegatedTenantScope)
			{
				errors.Add(new ObjectValidationError(DirectoryStrings.ScopeCannotBeExclusive(this.ScopeRestrictionType), this.Identity, base.OriginatingServer));
			}
		}

		// Token: 0x06003237 RID: 12855 RVA: 0x000CB46C File Offset: 0x000C966C
		internal bool IsScopeSmallerOrEqualThan(ManagementScope scope, out LocalizedString notTrueReason)
		{
			notTrueReason = LocalizedString.Empty;
			if (object.ReferenceEquals(this, scope))
			{
				return true;
			}
			if (scope == null)
			{
				ExTraceGlobals.AccessCheckTracer.TraceDebug(11000L, "IsScopeSmallerOrEqualThan: cannot compare this instance with $NULL");
				return false;
			}
			ExTraceGlobals.AccessCheckTracer.TraceFunction<ADObjectId, ADObjectId>(11000L, "-->IsScopeSmallerOrEqualThan: this = {0}, other = {1}", base.Id, scope.Id);
			if (this.ScopeRestrictionType == scope.ScopeRestrictionType && ADObjectId.Equals(this.RecipientRoot, scope.RecipientRoot) && string.Equals(this.Filter, scope.Filter, StringComparison.OrdinalIgnoreCase))
			{
				ExTraceGlobals.AccessCheckTracer.TraceDebug(11000L, "IsScopeSmallerOrEqualThan: both management scopes have exactly the same restrction type, root and filter.");
				return true;
			}
			if (this.ScopeRestrictionType == ScopeRestrictionType.RecipientScope && scope.ScopeRestrictionType == ScopeRestrictionType.RecipientScope && string.Equals(this.Filter, scope.Filter, StringComparison.OrdinalIgnoreCase) && this.RecipientRoot != null && scope.RecipientRoot != null)
			{
				ExTraceGlobals.AccessCheckTracer.TraceDebug<ADObjectId, ADObjectId>(11000L, "IsScopeSmallerOrEqualThan: comparing the Root of two scope objects: this (Root = {0}), other (Root = {1})", this.RecipientRoot, scope.RecipientRoot);
				if (this.RecipientRoot.IsDescendantOf(scope.RecipientRoot))
				{
					return true;
				}
				notTrueReason = DirectoryStrings.OUsNotSmallerOrEqual(this.RecipientRoot.ToString(), scope.RecipientRoot.ToString());
			}
			else
			{
				notTrueReason = DirectoryStrings.CannotCompareScopeObjects(base.Id.ToString(), scope.Id.ToString());
				ExTraceGlobals.AccessCheckTracer.TraceDebug(11000L, "IsScopeSmallerOrEqualThan: non-comparable scope objects. We can only compare scope scope objects of RecipientScope with non-empty root and equivalent filter.");
			}
			ExTraceGlobals.AccessCheckTracer.TraceFunction(11000L, "<--IsScopeSmallerOrEqualThan: return false");
			return false;
		}

		// Token: 0x06003238 RID: 12856 RVA: 0x000CB5F4 File Offset: 0x000C97F4
		internal bool IsFullyCoveredByOU(ADObjectId ouIdentity, out LocalizedString notTrueReason)
		{
			notTrueReason = LocalizedString.Empty;
			if (ouIdentity == null)
			{
				throw new ArgumentNullException("ouIdentity");
			}
			if (string.IsNullOrEmpty(ouIdentity.DistinguishedName))
			{
				throw new ArgumentNullException("ouIdentity.DistinguishedName");
			}
			if (ADObjectId.IsNullOrEmpty(this.RecipientRoot))
			{
				notTrueReason = DirectoryStrings.RootCannotBeEmpty(this.ScopeRestrictionType);
				ExTraceGlobals.AccessCheckTracer.TraceError(11001L, "IsFullyCoveredByOU: this instance has invalid empty Root.");
				return false;
			}
			ExTraceGlobals.AccessCheckTracer.TraceFunction(11001L, "-->IsFullyCoveredByOU: this = (Id = {0}, Root = {1}, Type = {2}), OU = {3}", new object[]
			{
				base.Id,
				this.RecipientRoot,
				this.ScopeRestrictionType,
				ouIdentity.DistinguishedName
			});
			bool flag = false;
			if (this.ScopeRestrictionType == ScopeRestrictionType.RecipientScope)
			{
				flag = this.RecipientRoot.IsDescendantOf(ouIdentity);
				if (!flag)
				{
					notTrueReason = DirectoryStrings.OUsNotSmallerOrEqual(this.RecipientRoot.ToString(), ouIdentity.ToString());
				}
			}
			else
			{
				notTrueReason = DirectoryStrings.CannotCompareScopeObjectWithOU(base.Id.ToString(), this.ScopeRestrictionType.ToString(), ouIdentity.ToString());
			}
			ExTraceGlobals.AccessCheckTracer.TraceFunction<bool>(11001L, "<--IsScopeSmallerOrEqualThan: return {0}", flag);
			return flag;
		}

		// Token: 0x04002261 RID: 8801
		public static readonly ADObjectId RdnScopesContainerToOrganization = new ADObjectId("CN=Scopes,CN=RBAC");

		// Token: 0x04002262 RID: 8802
		private static ManagementScopeSchema schema = ObjectSchema.GetInstance<ManagementScopeSchema>();

		// Token: 0x04002263 RID: 8803
		private static string mostDerivedClass = "msExchScope";

		// Token: 0x04002264 RID: 8804
		private QueryFilter queryFilter;
	}
}
