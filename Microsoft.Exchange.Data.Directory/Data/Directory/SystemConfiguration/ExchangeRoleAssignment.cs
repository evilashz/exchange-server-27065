using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics.Components.Authorization;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000456 RID: 1110
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public class ExchangeRoleAssignment : ADConfigurationObject
	{
		// Token: 0x17000E64 RID: 3684
		// (get) Token: 0x060031E6 RID: 12774 RVA: 0x000C9754 File Offset: 0x000C7954
		internal override ADObjectSchema Schema
		{
			get
			{
				return ExchangeRoleAssignment.schema;
			}
		}

		// Token: 0x17000E65 RID: 3685
		// (get) Token: 0x060031E7 RID: 12775 RVA: 0x000C975B File Offset: 0x000C795B
		internal override string MostDerivedObjectClass
		{
			get
			{
				return ExchangeRoleAssignment.mostDerivedClass;
			}
		}

		// Token: 0x17000E66 RID: 3686
		// (get) Token: 0x060031E8 RID: 12776 RVA: 0x000C9762 File Offset: 0x000C7962
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeRoleAssignmentSchema.Exchange2009_R4;
			}
		}

		// Token: 0x17000E67 RID: 3687
		// (get) Token: 0x060031E9 RID: 12777 RVA: 0x000C9769 File Offset: 0x000C7969
		internal override bool IsShareable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000E68 RID: 3688
		// (get) Token: 0x060031EA RID: 12778 RVA: 0x000C976C File Offset: 0x000C796C
		// (set) Token: 0x060031EB RID: 12779 RVA: 0x000C97D3 File Offset: 0x000C79D3
		internal bool IsFromEndUserRole
		{
			get
			{
				if (this.isFromEndUserRole == null)
				{
					ExchangeRole exchangeRole = base.Session.Read<ExchangeRole>(this.Role);
					if (exchangeRole == null)
					{
						throw new NotSupportedException(string.Format("Invalid Role Id {0} in Assignment {1}", this.Role, base.Id));
					}
					this.isFromEndUserRole = new bool?(exchangeRole.IsEndUserRole);
				}
				return this.isFromEndUserRole.Value;
			}
			set
			{
				this.isFromEndUserRole = new bool?(value);
			}
		}

		// Token: 0x17000E69 RID: 3689
		// (get) Token: 0x060031EC RID: 12780 RVA: 0x000C97E1 File Offset: 0x000C79E1
		// (set) Token: 0x060031ED RID: 12781 RVA: 0x000C97F3 File Offset: 0x000C79F3
		public ADObjectId User
		{
			get
			{
				return (ADObjectId)this[ExchangeRoleAssignmentSchema.User];
			}
			internal set
			{
				this[ExchangeRoleAssignmentSchema.User] = value;
			}
		}

		// Token: 0x17000E6A RID: 3690
		// (get) Token: 0x060031EE RID: 12782 RVA: 0x000C9801 File Offset: 0x000C7A01
		// (set) Token: 0x060031EF RID: 12783 RVA: 0x000C9813 File Offset: 0x000C7A13
		public ADObjectId Role
		{
			get
			{
				return (ADObjectId)this[ExchangeRoleAssignmentSchema.Role];
			}
			internal set
			{
				this[ExchangeRoleAssignmentSchema.Role] = value;
			}
		}

		// Token: 0x17000E6B RID: 3691
		// (get) Token: 0x060031F0 RID: 12784 RVA: 0x000C9821 File Offset: 0x000C7A21
		// (set) Token: 0x060031F1 RID: 12785 RVA: 0x000C9833 File Offset: 0x000C7A33
		public RoleAssignmentDelegationType RoleAssignmentDelegationType
		{
			get
			{
				return (RoleAssignmentDelegationType)this[ExchangeRoleAssignmentSchema.RoleAssignmentDelegationType];
			}
			internal set
			{
				this[ExchangeRoleAssignmentSchema.RoleAssignmentDelegationType] = value;
			}
		}

		// Token: 0x17000E6C RID: 3692
		// (get) Token: 0x060031F2 RID: 12786 RVA: 0x000C9846 File Offset: 0x000C7A46
		// (set) Token: 0x060031F3 RID: 12787 RVA: 0x000C9858 File Offset: 0x000C7A58
		public RoleAssigneeType RoleAssigneeType
		{
			get
			{
				return (RoleAssigneeType)this[ExchangeRoleAssignmentSchema.RoleAssigneeType];
			}
			internal set
			{
				this[ExchangeRoleAssignmentSchema.RoleAssigneeType] = value;
			}
		}

		// Token: 0x17000E6D RID: 3693
		// (get) Token: 0x060031F4 RID: 12788 RVA: 0x000C986B File Offset: 0x000C7A6B
		// (set) Token: 0x060031F5 RID: 12789 RVA: 0x000C987D File Offset: 0x000C7A7D
		public ADObjectId CustomRecipientWriteScope
		{
			get
			{
				return (ADObjectId)this[ExchangeRoleAssignmentSchema.CustomRecipientWriteScope];
			}
			internal set
			{
				this[ExchangeRoleAssignmentSchema.CustomRecipientWriteScope] = value;
			}
		}

		// Token: 0x17000E6E RID: 3694
		// (get) Token: 0x060031F6 RID: 12790 RVA: 0x000C988B File Offset: 0x000C7A8B
		// (set) Token: 0x060031F7 RID: 12791 RVA: 0x000C989D File Offset: 0x000C7A9D
		public ADObjectId CustomConfigWriteScope
		{
			get
			{
				return (ADObjectId)this[ExchangeRoleAssignmentSchema.CustomConfigWriteScope];
			}
			internal set
			{
				this[ExchangeRoleAssignmentSchema.CustomConfigWriteScope] = value;
			}
		}

		// Token: 0x17000E6F RID: 3695
		// (get) Token: 0x060031F8 RID: 12792 RVA: 0x000C98AB File Offset: 0x000C7AAB
		// (set) Token: 0x060031F9 RID: 12793 RVA: 0x000C98BD File Offset: 0x000C7ABD
		public ScopeType RecipientReadScope
		{
			get
			{
				return (ScopeType)this[ExchangeRoleAssignmentSchema.RecipientReadScope];
			}
			internal set
			{
				this[ExchangeRoleAssignmentSchema.RecipientReadScope] = value;
			}
		}

		// Token: 0x17000E70 RID: 3696
		// (get) Token: 0x060031FA RID: 12794 RVA: 0x000C98D0 File Offset: 0x000C7AD0
		// (set) Token: 0x060031FB RID: 12795 RVA: 0x000C98E2 File Offset: 0x000C7AE2
		public ScopeType ConfigReadScope
		{
			get
			{
				return (ScopeType)this[ExchangeRoleAssignmentSchema.ConfigReadScope];
			}
			internal set
			{
				this[ExchangeRoleAssignmentSchema.ConfigReadScope] = value;
			}
		}

		// Token: 0x17000E71 RID: 3697
		// (get) Token: 0x060031FC RID: 12796 RVA: 0x000C98F5 File Offset: 0x000C7AF5
		// (set) Token: 0x060031FD RID: 12797 RVA: 0x000C9907 File Offset: 0x000C7B07
		public RecipientWriteScopeType RecipientWriteScope
		{
			get
			{
				return (RecipientWriteScopeType)this[ExchangeRoleAssignmentSchema.RecipientWriteScope];
			}
			internal set
			{
				this[ExchangeRoleAssignmentSchema.RecipientWriteScope] = value;
			}
		}

		// Token: 0x17000E72 RID: 3698
		// (get) Token: 0x060031FE RID: 12798 RVA: 0x000C991A File Offset: 0x000C7B1A
		// (set) Token: 0x060031FF RID: 12799 RVA: 0x000C992C File Offset: 0x000C7B2C
		public ConfigWriteScopeType ConfigWriteScope
		{
			get
			{
				return (ConfigWriteScopeType)this[ExchangeRoleAssignmentSchema.ConfigWriteScope];
			}
			internal set
			{
				this[ExchangeRoleAssignmentSchema.ConfigWriteScope] = value;
			}
		}

		// Token: 0x17000E73 RID: 3699
		// (get) Token: 0x06003200 RID: 12800 RVA: 0x000C993F File Offset: 0x000C7B3F
		// (set) Token: 0x06003201 RID: 12801 RVA: 0x000C9951 File Offset: 0x000C7B51
		public bool Enabled
		{
			get
			{
				return (bool)this[ExchangeRoleAssignmentSchema.Enabled];
			}
			internal set
			{
				this[ExchangeRoleAssignmentSchema.Enabled] = value;
			}
		}

		// Token: 0x06003202 RID: 12802 RVA: 0x000C9964 File Offset: 0x000C7B64
		protected override void ValidateRead(List<ValidationError> errors)
		{
			base.ValidateRead(errors);
			ADObjectId adobjectId = (null != base.OrganizationId) ? base.OrganizationId.ConfigurationUnit : null;
			if (this.User == null)
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.UserIsMandatoryInRoleAssignment(this.Identity.ToString()), ExchangeRoleAssignmentSchema.User, null));
			}
			else if (adobjectId != null && this.User.IsDescendantOf(adobjectId) && this.RoleAssigneeType != RoleAssigneeType.RoleAssignmentPolicy)
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.WrongAssigneeTypeForPolicyOrPartnerApplication(this.Identity.ToString()), ExchangeRoleAssignmentSchema.User, null));
			}
			if (this.RoleAssigneeType == RoleAssigneeType.RoleAssignmentPolicy && this.RoleAssignmentDelegationType != RoleAssignmentDelegationType.Regular)
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.WrongDelegationTypeForPolicy(this.Identity.ToString()), ExchangeRoleAssignmentSchema.User, null));
			}
			if (this.Role == null)
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.RoleIsMandatoryInRoleAssignment(this.Identity.ToString()), ExchangeRoleAssignmentSchema.Role, null));
			}
			ScopeType recipientWriteScope = (ScopeType)this.RecipientWriteScope;
			if (this.RecipientReadScope != recipientWriteScope && !RbacScope.IsScopeTypeSmaller(recipientWriteScope, this.RecipientReadScope))
			{
				errors.Add(new ObjectValidationError(DirectoryStrings.RecipientWriteScopeNotLessThan(recipientWriteScope.ToString(), this.RecipientReadScope.ToString()), this.Identity, base.OriginatingServer));
			}
			ScopeType configWriteScope = (ScopeType)this.ConfigWriteScope;
			if (this.ConfigReadScope != configWriteScope && !RbacScope.IsScopeTypeSmaller(configWriteScope, this.ConfigReadScope))
			{
				errors.Add(new ObjectValidationError(DirectoryStrings.ConfigScopeNotLessThan(configWriteScope.ToString(), this.ConfigReadScope.ToString()), this.Identity, base.OriginatingServer));
			}
			bool flag = this.CustomRecipientWriteScope == null || (string.IsNullOrEmpty(this.CustomRecipientWriteScope.DistinguishedName) && this.CustomRecipientWriteScope.ObjectGuid == Guid.Empty);
			RecipientWriteScopeType recipientWriteScope2 = this.RecipientWriteScope;
			switch (recipientWriteScope2)
			{
			case RecipientWriteScopeType.OU:
			case RecipientWriteScopeType.CustomRecipientScope:
				break;
			default:
				if (recipientWriteScope2 != RecipientWriteScopeType.ExclusiveRecipientScope)
				{
					if (!flag)
					{
						errors.Add(new ObjectValidationError(DirectoryStrings.CustomRecipientWriteScopeMustBeEmpty(this.RecipientWriteScope), this.Identity, base.OriginatingServer));
						goto IL_25C;
					}
					goto IL_25C;
				}
				break;
			}
			if (flag)
			{
				errors.Add(new ObjectValidationError(DirectoryStrings.CustomRecipientWriteScopeCannotBeEmpty(this.RecipientWriteScope), this.Identity, base.OriginatingServer));
			}
			if (this.RoleAssignmentDelegationType == RoleAssignmentDelegationType.DelegatingOrgWide)
			{
				errors.Add(new ObjectValidationError(DirectoryStrings.OrgWideDelegatingWriteScopeMustBeTheSameAsRoleImplicitWriteScope(this.RecipientWriteScope), this.Identity, base.OriginatingServer));
			}
			IL_25C:
			bool flag2 = this.CustomConfigWriteScope == null || (string.IsNullOrEmpty(this.CustomConfigWriteScope.DistinguishedName) && this.CustomConfigWriteScope.ObjectGuid == Guid.Empty);
			switch (this.ConfigWriteScope)
			{
			case ConfigWriteScopeType.CustomConfigScope:
			case ConfigWriteScopeType.PartnerDelegatedTenantScope:
			case ConfigWriteScopeType.ExclusiveConfigScope:
				if (flag2)
				{
					errors.Add(new ObjectValidationError(DirectoryStrings.ConfigScopeCannotBeEmpty(this.ConfigWriteScope), this.Identity, base.OriginatingServer));
				}
				if (this.RoleAssignmentDelegationType == RoleAssignmentDelegationType.DelegatingOrgWide)
				{
					errors.Add(new ObjectValidationError(DirectoryStrings.OrgWideDelegatingConfigScopeMustBeTheSameAsRoleImplicitWriteScope(this.ConfigWriteScope), this.Identity, base.OriginatingServer));
					return;
				}
				return;
			}
			if (!flag2)
			{
				errors.Add(new ObjectValidationError(DirectoryStrings.ConfigScopeMustBeEmpty(this.ConfigWriteScope), this.Identity, base.OriginatingServer));
			}
		}

		// Token: 0x06003203 RID: 12803 RVA: 0x000C9CA0 File Offset: 0x000C7EA0
		internal bool AllPresentScopesMatch(ScopeSet rbacScopeSet)
		{
			if (rbacScopeSet.RecipientReadScope is RbacScope)
			{
				RbacScope rbacScope = rbacScopeSet.RecipientReadScope as RbacScope;
				if (!rbacScope.IsScopeTypeSmallerOrEqualThan(this.RecipientReadScope))
				{
					return false;
				}
			}
			else if (!rbacScopeSet.RecipientReadScope.IsEmpty)
			{
				throw new ArgumentException("rbacScopeSet");
			}
			if (rbacScopeSet.ConfigReadScope is RbacScope)
			{
				RbacScope rbacScope2 = rbacScopeSet.ConfigReadScope as RbacScope;
				if (!rbacScope2.IsScopeTypeSmallerOrEqualThan(this.ConfigReadScope))
				{
					return false;
				}
			}
			else if (!rbacScopeSet.ConfigReadScope.IsEmpty)
			{
				throw new ArgumentException("rbacScopeSet");
			}
			if (rbacScopeSet.RecipientWriteScopes != null && rbacScopeSet.RecipientWriteScopes.Count != 0 && rbacScopeSet.RecipientWriteScopes[0].Count != 0)
			{
				bool flag = false;
				foreach (ADScope adscope in rbacScopeSet.RecipientWriteScopes[0])
				{
					RbacScope rbacScope3 = adscope as RbacScope;
					if (rbacScope3 == null)
					{
						throw new ArgumentException("rbacScopeSet");
					}
					ScopeType recipientWriteScope = (ScopeType)this.RecipientWriteScope;
					if (rbacScope3.IsScopeTypeSmallerOrEqualThan(recipientWriteScope))
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06003204 RID: 12804 RVA: 0x000C9DD4 File Offset: 0x000C7FD4
		internal RoleAssignmentScopeSet GetEffectiveScopeSet(Dictionary<ADObjectId, ManagementScope> scopeCache, ISecurityAccessToken securityAccessToken)
		{
			RbacScope recipientReadScope = (this.RecipientReadScope == ScopeType.MyGAL) ? new RbacScope(this.RecipientReadScope, securityAccessToken) : new RbacScope(this.RecipientReadScope);
			RbacScope recipientWriteRbacScope = ExchangeRoleAssignment.GetRecipientWriteRbacScope(this.RecipientWriteScope, this.CustomRecipientWriteScope, scopeCache, securityAccessToken, this.IsFromEndUserRole);
			if (recipientWriteRbacScope == null)
			{
				return null;
			}
			RbacScope configReadScope = new RbacScope(this.ConfigReadScope);
			ConfigWriteScopeType configWriteScope = this.ConfigWriteScope;
			RbacScope configWriteScope2;
			switch (configWriteScope)
			{
			case ConfigWriteScopeType.None:
				break;
			case ConfigWriteScopeType.NotApplicable:
				configWriteScope2 = new RbacScope(ScopeType.NotApplicable);
				goto IL_E0;
			default:
				switch (configWriteScope)
				{
				case ConfigWriteScopeType.OrganizationConfig:
					goto IL_85;
				case ConfigWriteScopeType.CustomConfigScope:
				case ConfigWriteScopeType.ExclusiveConfigScope:
				{
					ManagementScope managementScope = scopeCache[this.CustomConfigWriteScope];
					if (managementScope == null)
					{
						return null;
					}
					configWriteScope2 = new RbacScope((ScopeType)this.ConfigWriteScope, managementScope);
					goto IL_E0;
				}
				case ConfigWriteScopeType.PartnerDelegatedTenantScope:
					if (scopeCache[this.CustomConfigWriteScope] == null)
					{
						return null;
					}
					configWriteScope2 = new RbacScope(ScopeType.OrganizationConfig);
					goto IL_E0;
				}
				configWriteScope2 = null;
				goto IL_E0;
			}
			IL_85:
			configWriteScope2 = new RbacScope((ScopeType)this.ConfigWriteScope);
			IL_E0:
			return new RoleAssignmentScopeSet(recipientReadScope, recipientWriteRbacScope, configReadScope, configWriteScope2);
		}

		// Token: 0x06003205 RID: 12805 RVA: 0x000C9ED0 File Offset: 0x000C80D0
		internal static RbacScope GetRecipientWriteRbacScope(RecipientWriteScopeType recipientWriteScope, ADObjectId customRecipientWriteScope, Dictionary<ADObjectId, ManagementScope> scopeCache, ISecurityAccessToken securityAccessToken, bool isEndUserRole)
		{
			RbacScope result = null;
			switch (recipientWriteScope)
			{
			case RecipientWriteScopeType.None:
			case RecipientWriteScopeType.Organization:
			case RecipientWriteScopeType.Self:
			case RecipientWriteScopeType.MyDirectReports:
			case RecipientWriteScopeType.MyDistributionGroups:
			case RecipientWriteScopeType.MyExecutive:
				return new RbacScope((ScopeType)recipientWriteScope, isEndUserRole);
			case RecipientWriteScopeType.NotApplicable:
				return new RbacScope(ScopeType.NotApplicable, isEndUserRole);
			case RecipientWriteScopeType.MyGAL:
			case RecipientWriteScopeType.MailboxICanDelegate:
				return new RbacScope((ScopeType)recipientWriteScope, securityAccessToken, isEndUserRole);
			case RecipientWriteScopeType.OU:
				return new RbacScope(ScopeType.OU, customRecipientWriteScope, isEndUserRole);
			case RecipientWriteScopeType.CustomRecipientScope:
			case RecipientWriteScopeType.ExclusiveRecipientScope:
			{
				ManagementScope managementScope = scopeCache[customRecipientWriteScope];
				if (managementScope != null)
				{
					return new RbacScope((ScopeType)recipientWriteScope, managementScope, isEndUserRole);
				}
				return result;
			}
			}
			result = null;
			return result;
		}

		// Token: 0x06003206 RID: 12806 RVA: 0x000C9F74 File Offset: 0x000C8174
		private static RoleAssigneeType RoleAssigneeTypeFromRecipientTypeDetails(RecipientTypeDetails recipientTypeDetails)
		{
			if (recipientTypeDetails <= RecipientTypeDetails.ArbitrationMailbox)
			{
				if (recipientTypeDetails > RecipientTypeDetails.MailUser)
				{
					if (recipientTypeDetails <= RecipientTypeDetails.UniversalSecurityGroup)
					{
						if (recipientTypeDetails != RecipientTypeDetails.MailUniversalSecurityGroup)
						{
							if (recipientTypeDetails == RecipientTypeDetails.User)
							{
								return RoleAssigneeType.User;
							}
							if (recipientTypeDetails != RecipientTypeDetails.UniversalSecurityGroup)
							{
								goto IL_193;
							}
						}
					}
					else if (recipientTypeDetails != RecipientTypeDetails.NonUniversalGroup)
					{
						if (recipientTypeDetails != RecipientTypeDetails.DisabledUser && recipientTypeDetails != RecipientTypeDetails.ArbitrationMailbox)
						{
							goto IL_193;
						}
						return RoleAssigneeType.User;
					}
					return RoleAssigneeType.SecurityGroup;
				}
				if (recipientTypeDetails <= RecipientTypeDetails.LegacyMailbox)
				{
					if (recipientTypeDetails <= RecipientTypeDetails.SharedMailbox)
					{
						if (recipientTypeDetails < RecipientTypeDetails.UserMailbox)
						{
							goto IL_193;
						}
						switch ((int)(recipientTypeDetails - RecipientTypeDetails.UserMailbox))
						{
						case 0:
						case 1:
						case 3:
							return RoleAssigneeType.User;
						case 2:
							goto IL_193;
						}
					}
					if (recipientTypeDetails != RecipientTypeDetails.LegacyMailbox)
					{
						goto IL_193;
					}
				}
				else if (recipientTypeDetails != RecipientTypeDetails.RoomMailbox && recipientTypeDetails != RecipientTypeDetails.EquipmentMailbox && recipientTypeDetails != RecipientTypeDetails.MailUser)
				{
					goto IL_193;
				}
			}
			else if (recipientTypeDetails <= RecipientTypeDetails.RemoteRoomMailbox)
			{
				if (recipientTypeDetails <= RecipientTypeDetails.RoleGroup)
				{
					if (recipientTypeDetails == RecipientTypeDetails.MailboxPlan)
					{
						return RoleAssigneeType.MailboxPlan;
					}
					if (recipientTypeDetails != RecipientTypeDetails.LinkedUser)
					{
						if (recipientTypeDetails != RecipientTypeDetails.RoleGroup)
						{
							goto IL_193;
						}
						return RoleAssigneeType.RoleGroup;
					}
				}
				else if (recipientTypeDetails != (RecipientTypeDetails)((ulong)-2147483648))
				{
					if (recipientTypeDetails == RecipientTypeDetails.Computer)
					{
						return RoleAssigneeType.Computer;
					}
					if (recipientTypeDetails != RecipientTypeDetails.RemoteRoomMailbox)
					{
						goto IL_193;
					}
				}
			}
			else if (recipientTypeDetails <= RecipientTypeDetails.TeamMailbox)
			{
				if (recipientTypeDetails != RecipientTypeDetails.RemoteEquipmentMailbox && recipientTypeDetails != RecipientTypeDetails.RemoteSharedMailbox && recipientTypeDetails != RecipientTypeDetails.TeamMailbox)
				{
					goto IL_193;
				}
			}
			else if (recipientTypeDetails != RecipientTypeDetails.RemoteTeamMailbox && recipientTypeDetails != RecipientTypeDetails.LinkedRoomMailbox && recipientTypeDetails != RecipientTypeDetails.AuditLogMailbox)
			{
				goto IL_193;
			}
			return RoleAssigneeType.User;
			IL_193:
			throw new ArgumentException("recipientTypeDetails");
		}

		// Token: 0x06003207 RID: 12807 RVA: 0x000CA120 File Offset: 0x000C8320
		private bool IsDelegationLevelSmallerOrEqual(ExchangeRoleAssignment roleAssignment, out LocalizedString notTrueReason, bool isRecipientScope)
		{
			notTrueReason = LocalizedString.Empty;
			if ((this.RoleAssignmentDelegationType == RoleAssignmentDelegationType.DelegatingOrgWide && roleAssignment.RoleAssignmentDelegationType != RoleAssignmentDelegationType.DelegatingOrgWide) || (this.RoleAssignmentDelegationType == RoleAssignmentDelegationType.Delegating && roleAssignment.RoleAssignmentDelegationType == RoleAssignmentDelegationType.Regular))
			{
				ExTraceGlobals.AccessCheckTracer.TraceDebug(10000L, "this instance has the same write scope as the other role assignment, but it has a larger delegating scope");
				if (isRecipientScope)
				{
					notTrueReason = DirectoryStrings.RecipientWriteScopeNotLessThanBecauseOfDelegationFlags(this.RoleAssignmentDelegationType.ToString(), roleAssignment.RoleAssignmentDelegationType.ToString());
				}
				else
				{
					notTrueReason = DirectoryStrings.ConfigWriteScopeNotLessThanBecauseOfDelegationFlags(this.RoleAssignmentDelegationType.ToString(), roleAssignment.RoleAssignmentDelegationType.ToString());
				}
				return false;
			}
			ExTraceGlobals.AccessCheckTracer.TraceDebug(10000L, "this instance has exactly the same write scope as the other role assignment and smaller or equal delegation scope");
			return true;
		}

		// Token: 0x06003208 RID: 12808 RVA: 0x000CA1E4 File Offset: 0x000C83E4
		internal bool IsRecipientWriteScopeSmallerOrEqualThan(ExchangeRoleAssignment roleAssignment, IDictionary<ADObjectId, ManagementScope> scopeCache, out LocalizedString notTrueReason)
		{
			if (roleAssignment == null)
			{
				throw new ArgumentNullException("roleAssignment");
			}
			ExTraceGlobals.AccessCheckTracer.TraceFunction<ADObjectId, ADObjectId, int>(10000L, "-->IsRecipientWriteScopeSmallerOrEqualThan: this = {0}, other = {1} (scopeCache Count = {2})", base.Id, roleAssignment.Id, (scopeCache == null) ? 0 : scopeCache.Count);
			notTrueReason = LocalizedString.Empty;
			if (roleAssignment.RecipientWriteScope == this.RecipientWriteScope && ADObjectId.Equals(roleAssignment.CustomRecipientWriteScope, this.CustomRecipientWriteScope))
			{
				return this.IsDelegationLevelSmallerOrEqual(roleAssignment, out notTrueReason, true);
			}
			ExTraceGlobals.AccessCheckTracer.TraceDebug(10000L, "IsRecipientWriteScopeSmallerOrEqualThan: this instance has domain write restriction type '{0}' with link '{1}', other has type '{2}' with link '{3}'.", new object[]
			{
				this.RecipientWriteScope,
				(this.CustomRecipientWriteScope == null) ? "null" : this.CustomRecipientWriteScope.ToString(),
				roleAssignment.RecipientWriteScope,
				(roleAssignment.CustomRecipientWriteScope == null) ? "null" : roleAssignment.CustomRecipientWriteScope.ToString()
			});
			if (this.CustomRecipientWriteScope == null && (this.RecipientWriteScope == RecipientWriteScopeType.OU || this.RecipientWriteScope == RecipientWriteScopeType.CustomRecipientScope || this.RecipientWriteScope == RecipientWriteScopeType.ExclusiveRecipientScope))
			{
				ExTraceGlobals.AccessCheckTracer.TraceError(10000L, "IsRecipientWriteScopeSmallerOrEqualThan: this instance has invalid NULL CustomRecipientWriteScope.");
				notTrueReason = DirectoryStrings.CustomRecipientWriteScopeCannotBeEmpty(this.RecipientWriteScope);
				return false;
			}
			if (roleAssignment.CustomRecipientWriteScope == null && (roleAssignment.RecipientWriteScope == RecipientWriteScopeType.OU || roleAssignment.RecipientWriteScope == RecipientWriteScopeType.CustomRecipientScope || roleAssignment.RecipientWriteScope == RecipientWriteScopeType.ExclusiveRecipientScope))
			{
				ExTraceGlobals.AccessCheckTracer.TraceError(10000L, "IsRecipientWriteScopeSmallerOrEqualThan: the other instance has invalid NULL CustomRecipientWriteScope.");
				notTrueReason = DirectoryStrings.CustomRecipientWriteScopeCannotBeEmpty(roleAssignment.RecipientWriteScope);
				return false;
			}
			bool flag;
			switch (this.RecipientWriteScope)
			{
			case RecipientWriteScopeType.None:
				flag = (roleAssignment.RecipientWriteScope != RecipientWriteScopeType.NotApplicable);
				goto IL_41E;
			case RecipientWriteScopeType.NotApplicable:
				flag = true;
				goto IL_41E;
			case RecipientWriteScopeType.Organization:
				flag = (roleAssignment.RecipientWriteScope == RecipientWriteScopeType.Organization);
				goto IL_41E;
			case RecipientWriteScopeType.MyGAL:
			case RecipientWriteScopeType.Self:
			case RecipientWriteScopeType.MyDirectReports:
			case RecipientWriteScopeType.MyDistributionGroups:
			case RecipientWriteScopeType.MyExecutive:
			case RecipientWriteScopeType.MailboxICanDelegate:
				flag = (roleAssignment.RecipientWriteScope == RecipientWriteScopeType.MyGAL || roleAssignment.RecipientWriteScope == RecipientWriteScopeType.Organization);
				goto IL_41E;
			case RecipientWriteScopeType.OU:
				if (roleAssignment.RecipientWriteScope != RecipientWriteScopeType.OU)
				{
					flag = (roleAssignment.RecipientWriteScope == RecipientWriteScopeType.Organization);
					goto IL_41E;
				}
				ExTraceGlobals.AccessCheckTracer.TraceDebug(10000L, "IsRecipientWriteScopeSmallerOrEqualThan: comparing the OU DistinguishedName(s) between two role assignments.");
				if (this.CustomRecipientWriteScope.IsDescendantOf(roleAssignment.CustomRecipientWriteScope))
				{
					flag = true;
					goto IL_41E;
				}
				notTrueReason = DirectoryStrings.OUsNotSmallerOrEqual(this.CustomRecipientWriteScope.ToString(), roleAssignment.CustomRecipientWriteScope.ToString());
				flag = false;
				goto IL_41E;
			case RecipientWriteScopeType.CustomRecipientScope:
			case RecipientWriteScopeType.ExclusiveRecipientScope:
				if (roleAssignment.RecipientWriteScope == RecipientWriteScopeType.OU)
				{
					if (scopeCache != null && scopeCache.ContainsKey(this.CustomRecipientWriteScope) && scopeCache[this.CustomRecipientWriteScope] != null)
					{
						flag = scopeCache[this.CustomRecipientWriteScope].IsFullyCoveredByOU(roleAssignment.CustomRecipientWriteScope, out notTrueReason);
						goto IL_41E;
					}
					ExTraceGlobals.AccessCheckTracer.TraceError<string>(10000L, "IsRecipientWriteScopeSmallerOrEqualThan: we don't find the the key '{0}' within the scope cache or its value is $null.", (this.CustomRecipientWriteScope == null) ? "null" : this.CustomRecipientWriteScope.ToString());
					notTrueReason = DirectoryStrings.CannotCompareAssignmentsMissingScope(base.Id.ToString(), roleAssignment.Id.ToString());
					flag = false;
					goto IL_41E;
				}
				else
				{
					if (roleAssignment.RecipientWriteScope != RecipientWriteScopeType.CustomRecipientScope && roleAssignment.RecipientWriteScope != RecipientWriteScopeType.ExclusiveRecipientScope)
					{
						flag = (roleAssignment.RecipientWriteScope == RecipientWriteScopeType.Organization);
						goto IL_41E;
					}
					if (scopeCache != null && this.CustomRecipientWriteScope != null && roleAssignment.CustomRecipientWriteScope != null && scopeCache.ContainsKey(this.CustomRecipientWriteScope) && scopeCache.ContainsKey(roleAssignment.CustomRecipientWriteScope) && scopeCache[this.CustomRecipientWriteScope] != null && scopeCache[roleAssignment.CustomRecipientWriteScope] != null)
					{
						flag = scopeCache[this.CustomRecipientWriteScope].IsScopeSmallerOrEqualThan(scopeCache[roleAssignment.CustomRecipientWriteScope], out notTrueReason);
						goto IL_41E;
					}
					ExTraceGlobals.AccessCheckTracer.TraceError<string, string>(10000L, "IsRecipientWriteScopeSmallerOrEqualThan: we don't find the the key '{0}' or '{1}' within the scope cache or either of their values are $null.", (this.CustomRecipientWriteScope == null) ? "null" : this.CustomRecipientWriteScope.ToString(), (roleAssignment.CustomRecipientWriteScope == null) ? "null" : roleAssignment.CustomRecipientWriteScope.ToString());
					notTrueReason = DirectoryStrings.CannotCompareAssignmentsMissingScope(base.Id.ToString(), roleAssignment.Id.ToString());
					flag = false;
					goto IL_41E;
				}
				break;
			}
			flag = false;
			IL_41E:
			if (!flag && LocalizedString.Empty == notTrueReason)
			{
				notTrueReason = DirectoryStrings.RecipientWriteScopeNotLessThan(this.RecipientWriteScope.ToString(), roleAssignment.RecipientWriteScope.ToString());
			}
			ExTraceGlobals.AccessCheckTracer.TraceFunction<bool>(10000L, "<--IsRecipientWriteScopeSmallerOrEqualThan: returns {0}", flag);
			return flag;
		}

		// Token: 0x06003209 RID: 12809 RVA: 0x000CA668 File Offset: 0x000C8868
		internal bool IsConfigWriteScopeSmallerOrEqualThan(ExchangeRoleAssignment roleAssignment, IDictionary<ADObjectId, ManagementScope> scopeCache, out LocalizedString notTrueReason)
		{
			if (roleAssignment == null)
			{
				throw new ArgumentNullException("roleAssignment");
			}
			ExTraceGlobals.AccessCheckTracer.TraceFunction<ADObjectId, ADObjectId, int>(10001L, "-->IsConfigReadScopeSmallerOrEqualThan: this = {0}, other = {1} (scopeCache Count = {2})", base.Id, roleAssignment.Id, (scopeCache == null) ? 0 : scopeCache.Count);
			notTrueReason = LocalizedString.Empty;
			if (roleAssignment.ConfigWriteScope == this.ConfigWriteScope && ADObjectId.Equals(roleAssignment.CustomConfigWriteScope, this.CustomConfigWriteScope))
			{
				return this.IsDelegationLevelSmallerOrEqual(roleAssignment, out notTrueReason, false);
			}
			if (roleAssignment.ConfigWriteScope == ConfigWriteScopeType.OrganizationConfig)
			{
				ExTraceGlobals.AccessCheckTracer.TraceDebug(10001L, "the other role assignment has the biggest config scope: ConfigWriteScopeType.OrganizationConfig");
				return true;
			}
			ExTraceGlobals.AccessCheckTracer.TraceDebug(10001L, "IsConfigWriteScopeSmallerOrEqualThan: this instance has config write scope '{0}' with link '{1}', other has type '{2}' with link '{3}'.", new object[]
			{
				this.ConfigWriteScope,
				(this.CustomConfigWriteScope == null) ? "null" : this.CustomConfigWriteScope.ToString(),
				roleAssignment.ConfigWriteScope,
				(roleAssignment.CustomConfigWriteScope == null) ? "null" : roleAssignment.CustomConfigWriteScope.ToString()
			});
			ConfigWriteScopeType configWriteScope = this.ConfigWriteScope;
			bool flag;
			switch (configWriteScope)
			{
			case ConfigWriteScopeType.None:
				flag = (roleAssignment.ConfigWriteScope != ConfigWriteScopeType.NotApplicable);
				break;
			case ConfigWriteScopeType.NotApplicable:
				flag = true;
				break;
			default:
				switch (configWriteScope)
				{
				case ConfigWriteScopeType.OrganizationConfig:
					flag = (roleAssignment.ConfigWriteScope == ConfigWriteScopeType.OrganizationConfig);
					goto IL_27C;
				case ConfigWriteScopeType.CustomConfigScope:
				case ConfigWriteScopeType.ExclusiveConfigScope:
					if (roleAssignment.ConfigWriteScope != this.ConfigWriteScope)
					{
						flag = (roleAssignment.ConfigWriteScope == ConfigWriteScopeType.OrganizationConfig);
						goto IL_27C;
					}
					if (scopeCache != null && this.CustomConfigWriteScope != null && roleAssignment.CustomConfigWriteScope != null && scopeCache.ContainsKey(this.CustomConfigWriteScope) && scopeCache.ContainsKey(roleAssignment.CustomConfigWriteScope) && scopeCache[this.CustomConfigWriteScope] != null && scopeCache[roleAssignment.CustomConfigWriteScope] != null)
					{
						flag = scopeCache[this.CustomConfigWriteScope].IsScopeSmallerOrEqualThan(scopeCache[roleAssignment.CustomConfigWriteScope], out notTrueReason);
						goto IL_27C;
					}
					ExTraceGlobals.AccessCheckTracer.TraceError<string, string>(10000L, "IsConfigWriteScopeSmallerOrEqualThan: we don't find the the key '{0}' or '{1}' within the scope cache or either of their values are $null.", (this.CustomConfigWriteScope == null) ? "null" : this.CustomConfigWriteScope.ToString(), (roleAssignment.CustomConfigWriteScope == null) ? "null" : roleAssignment.CustomConfigWriteScope.ToString());
					notTrueReason = DirectoryStrings.CannotCompareAssignmentsMissingScope(base.Id.ToString(), roleAssignment.Id.ToString());
					flag = false;
					goto IL_27C;
				case ConfigWriteScopeType.PartnerDelegatedTenantScope:
					flag = (roleAssignment.ConfigWriteScope == ConfigWriteScopeType.OrganizationConfig || roleAssignment.ConfigWriteScope == ConfigWriteScopeType.CustomConfigScope);
					goto IL_27C;
				}
				flag = false;
				break;
			}
			IL_27C:
			if (!flag && LocalizedString.Empty == notTrueReason)
			{
				notTrueReason = DirectoryStrings.ConfigScopeNotLessThan(this.ConfigWriteScope.ToString(), roleAssignment.ConfigWriteScope.ToString());
			}
			ExTraceGlobals.AccessCheckTracer.TraceFunction<bool>(10001L, "<--IsConfigWriteScopeSmallerOrEqualThan: returns {0}", flag);
			return flag;
		}

		// Token: 0x0600320A RID: 12810 RVA: 0x000CA948 File Offset: 0x000C8B48
		internal static ExchangeRoleAssignment FindOneWithMaximumWriteScope(IList<ExchangeRoleAssignment> roleAssignments, IDictionary<ADObjectId, ManagementScope> scopeCache, bool compareRecipientWriteScope, out LocalizedString notFoundReason)
		{
			ExTraceGlobals.AccessCheckTracer.TraceFunction<bool>(10002L, "-->FindOneWithMaximumWriteScope: compareRecipientWriteScope = {0}", compareRecipientWriteScope);
			notFoundReason = LocalizedString.Empty;
			if (roleAssignments == null || roleAssignments.Count == 0)
			{
				ExTraceGlobals.AccessCheckTracer.TraceError(10002L, "FindOneWithMaximumWriteScope: there is not any role assignment to be worked against, we return NULL here.");
				return null;
			}
			ExTraceGlobals.AccessCheckTracer.TraceDebug(10002L, "FindOneWithMaximumWriteScope: Going through all the role assignments and find the assignment with maximum recipient or config scope");
			ExchangeRoleAssignment exchangeRoleAssignment = roleAssignments[0];
			for (int i = 1; i < roleAssignments.Count; i++)
			{
				LocalizedString localizedString;
				if (compareRecipientWriteScope)
				{
					if (exchangeRoleAssignment.IsRecipientWriteScopeSmallerOrEqualThan(roleAssignments[i], scopeCache, out localizedString))
					{
						exchangeRoleAssignment = roleAssignments[i];
					}
				}
				else if (exchangeRoleAssignment.IsConfigWriteScopeSmallerOrEqualThan(roleAssignments[i], scopeCache, out localizedString))
				{
					exchangeRoleAssignment = roleAssignments[i];
				}
			}
			ExTraceGlobals.AccessCheckTracer.TraceDebug<ADObjectId>(10002L, "FindOneWithMaximumWriteScope: The temporary role assignment we found with maximum recipient or config scope is {0}.", exchangeRoleAssignment.Id);
			ExTraceGlobals.AccessCheckTracer.TraceDebug(10002L, "FindOneWithMaximumWriteScope: Re-iterate the whole array to make sure it is really the assignment with maximum recipient or config scope");
			for (int j = 1; j < roleAssignments.Count; j++)
			{
				LocalizedString value;
				if (compareRecipientWriteScope)
				{
					if (!roleAssignments[j].IsRecipientWriteScopeSmallerOrEqualThan(exchangeRoleAssignment, scopeCache, out value))
					{
						ExTraceGlobals.AccessCheckTracer.TraceError<ADObjectId, ADObjectId>(10002L, "FindOneWithMaximumWriteScope: role assignment {0} has conflicting recipient write scope with {1}.", exchangeRoleAssignment.Id, roleAssignments[j].Id);
						notFoundReason = DirectoryStrings.AssignmentsWithConflictingScope(exchangeRoleAssignment.Id.ToString(), roleAssignments[j].Id.ToString(), value);
						return null;
					}
				}
				else if (!roleAssignments[j].IsConfigWriteScopeSmallerOrEqualThan(exchangeRoleAssignment, scopeCache, out value))
				{
					ExTraceGlobals.AccessCheckTracer.TraceError<ADObjectId, ADObjectId>(10002L, "FindOneWithMaximumWriteScope: role assignment {0} has conflicting config write scope with {1}.", exchangeRoleAssignment.Id, roleAssignments[j].Id);
					notFoundReason = DirectoryStrings.AssignmentsWithConflictingScope(exchangeRoleAssignment.Id.ToString(), roleAssignments[j].Id.ToString(), value);
					return null;
				}
			}
			ExTraceGlobals.AccessCheckTracer.TraceFunction<ADObjectId>(10002L, "<--FindOneWithMaximumWriteScope: returns {0}.", exchangeRoleAssignment.Id);
			return exchangeRoleAssignment;
		}

		// Token: 0x0600320B RID: 12811 RVA: 0x000CAB3C File Offset: 0x000C8D3C
		internal static RoleAssigneeType RoleAssigneeTypeFromADRecipient(ADRecipient recipient)
		{
			if (recipient is ADGroup)
			{
				RoleGroupType roleGroupType = ((ADGroup)recipient).RoleGroupType;
				if (roleGroupType == RoleGroupType.Linked)
				{
					return RoleAssigneeType.LinkedRoleGroup;
				}
				if (roleGroupType == RoleGroupType.PartnerLinked)
				{
					return RoleAssigneeType.PartnerLinkedRoleGroup;
				}
			}
			return ExchangeRoleAssignment.RoleAssigneeTypeFromRecipientTypeDetails(recipient.RecipientTypeDetails);
		}

		// Token: 0x0600320C RID: 12812 RVA: 0x000CAB76 File Offset: 0x000C8D76
		internal SimpleScopeSet<ScopeType> GetSimpleScopeSet()
		{
			return new SimpleScopeSet<ScopeType>(this.RecipientReadScope, (ScopeType)this.RecipientWriteScope, this.ConfigReadScope, (ScopeType)this.ConfigWriteScope);
		}

		// Token: 0x17000E74 RID: 3700
		// (get) Token: 0x0600320D RID: 12813 RVA: 0x000CAB95 File Offset: 0x000C8D95
		internal bool IsAssignmentToRootRole
		{
			get
			{
				return ExchangeRole.IsRootRoleInternal(this.Role);
			}
		}

		// Token: 0x04002243 RID: 8771
		internal static readonly ADObjectId RdnContainer = new ADObjectId("CN=Role Assignments,CN=RBAC");

		// Token: 0x04002244 RID: 8772
		private static ExchangeRoleAssignmentSchema schema = ObjectSchema.GetInstance<ExchangeRoleAssignmentSchema>();

		// Token: 0x04002245 RID: 8773
		private static string mostDerivedClass = "msExchRoleAssignment";

		// Token: 0x04002246 RID: 8774
		private bool? isFromEndUserRole = null;
	}
}
