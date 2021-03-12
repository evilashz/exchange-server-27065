using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Web;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020004D9 RID: 1241
	[KnownType(typeof(AdminRoleGroupObject))]
	[DataContract]
	public class AdminRoleGroupObject : AdminRoleGroupRow
	{
		// Token: 0x06003C97 RID: 15511 RVA: 0x000B6006 File Offset: 0x000B4206
		public AdminRoleGroupObject(RoleGroup roleGroup) : base(roleGroup)
		{
		}

		// Token: 0x170023DA RID: 9178
		// (get) Token: 0x06003C98 RID: 15512 RVA: 0x000B600F File Offset: 0x000B420F
		// (set) Token: 0x06003C99 RID: 15513 RVA: 0x000B601C File Offset: 0x000B421C
		[DataMember]
		public string Description
		{
			get
			{
				return base.RoleGroup.Description;
			}
			set
			{
				base.RoleGroup.Description = value;
			}
		}

		// Token: 0x170023DB RID: 9179
		// (get) Token: 0x06003C9A RID: 15514 RVA: 0x000B602C File Offset: 0x000B422C
		[DataMember]
		public string[] ManagedBy
		{
			get
			{
				List<string> list = new List<string>();
				foreach (ADObjectId adobjectId in base.RoleGroup.ManagedBy)
				{
					list.Add(adobjectId.ToString());
				}
				return list.ToArray();
			}
		}

		// Token: 0x170023DC RID: 9180
		// (get) Token: 0x06003C9B RID: 15515 RVA: 0x000B6098 File Offset: 0x000B4298
		// (set) Token: 0x06003C9C RID: 15516 RVA: 0x000B60EF File Offset: 0x000B42EF
		[DataMember]
		public string ManagementScopeName
		{
			get
			{
				if (ManagementScopeRow.IsDefaultScope(this.AggregatedScope.ID))
				{
					return Strings.DefaultScope;
				}
				if (ManagementScopeRow.IsMultipleScope(this.AggregatedScope.ID))
				{
					return Strings.MultipleScopeInRoleGroup;
				}
				return HttpUtility.HtmlEncode(this.AggregatedScope.ID);
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170023DD RID: 9181
		// (get) Token: 0x06003C9D RID: 15517 RVA: 0x000B60F8 File Offset: 0x000B42F8
		// (set) Token: 0x06003C9E RID: 15518 RVA: 0x000B6210 File Offset: 0x000B4410
		[DataMember]
		public AggregatedScope AggregatedScope
		{
			get
			{
				if (this.aggregatedScope == null)
				{
					this.aggregatedScope = new AggregatedScope();
					this.aggregatedScope.IsOrganizationalUnit = false;
					IEnumerable<RoleAssignmentObjectResolverRow> roleAssignments = base.RoleAssignments;
					if (base.IsMultipleScopesScenario)
					{
						this.aggregatedScope.ID = string.Empty;
					}
					else
					{
						foreach (RoleAssignmentObjectResolverRow roleAssignmentObjectResolverRow in roleAssignments)
						{
							if (roleAssignmentObjectResolverRow.CustomConfigWriteScope != null)
							{
								this.aggregatedScope.ID = roleAssignmentObjectResolverRow.CustomConfigWriteScope.Name;
								break;
							}
							if (roleAssignmentObjectResolverRow.CustomRecipientWriteScope != null)
							{
								this.aggregatedScope.ID = roleAssignmentObjectResolverRow.CustomRecipientWriteScope.Name;
								break;
							}
							if (roleAssignmentObjectResolverRow.RecipientOrganizationUnitScope != null)
							{
								this.aggregatedScope.IsOrganizationalUnit = true;
								this.aggregatedScope.ID = roleAssignmentObjectResolverRow.RecipientOrganizationUnitScope.ToString();
								break;
							}
						}
						if (this.aggregatedScope.ID == null)
						{
							this.aggregatedScope.ID = ManagementScopeRow.DefaultScopeId;
						}
					}
				}
				return this.aggregatedScope;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170023DE RID: 9182
		// (get) Token: 0x06003C9F RID: 15519 RVA: 0x000B6217 File Offset: 0x000B4417
		// (set) Token: 0x06003CA0 RID: 15520 RVA: 0x000B6247 File Offset: 0x000B4447
		[DataMember]
		public IList<SecurityPrincipalRow> Members
		{
			get
			{
				if (this.members == null)
				{
					this.members = new List<SecurityPrincipalRow>(SecurityPrincipalObjectResolver.Instance.ResolveObjects(base.RoleGroup.Members));
				}
				return this.members;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170023DF RID: 9183
		// (get) Token: 0x06003CA1 RID: 15521 RVA: 0x000B624E File Offset: 0x000B444E
		// (set) Token: 0x06003CA2 RID: 15522 RVA: 0x000B626E File Offset: 0x000B446E
		[DataMember]
		public IEnumerable<ManagementRoleResolveRow> Roles
		{
			get
			{
				if (this.roleObjectIds == null)
				{
					this.InitializeRoles();
				}
				return ManagementRoleObjectResolver.Instance.ResolveObjects(this.roleObjectIds);
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170023E0 RID: 9184
		// (get) Token: 0x06003CA3 RID: 15523 RVA: 0x000B6275 File Offset: 0x000B4475
		public IEnumerable<Identity> RoleIdentities
		{
			get
			{
				if (this.roleObjectIds == null)
				{
					this.InitializeRoles();
				}
				return this.roleObjectIds.ToIdentities();
			}
		}

		// Token: 0x170023E1 RID: 9185
		// (get) Token: 0x06003CA4 RID: 15524 RVA: 0x000B6290 File Offset: 0x000B4490
		public string OrganizationalUnit
		{
			get
			{
				if (this.AggregatedScope.IsOrganizationalUnit)
				{
					return this.AggregatedScope.ID;
				}
				return Guid.Empty.ToString();
			}
		}

		// Token: 0x170023E2 RID: 9186
		// (get) Token: 0x06003CA5 RID: 15525 RVA: 0x000B62C9 File Offset: 0x000B44C9
		public string ManagementScopeId
		{
			get
			{
				return this.AggregatedScope.ID;
			}
		}

		// Token: 0x170023E3 RID: 9187
		// (get) Token: 0x06003CA6 RID: 15526 RVA: 0x000B62D6 File Offset: 0x000B44D6
		public bool IsOrganizationalUnit
		{
			get
			{
				return this.AggregatedScope.IsOrganizationalUnit;
			}
		}

		// Token: 0x06003CA7 RID: 15527 RVA: 0x000B62E4 File Offset: 0x000B44E4
		private void InitializeRoles()
		{
			Dictionary<string, ADObjectId> dictionary = new Dictionary<string, ADObjectId>();
			foreach (RoleAssignmentObjectResolverRow roleAssignmentObjectResolverRow in base.RoleAssignments)
			{
				dictionary[roleAssignmentObjectResolverRow.RoleIdentity.Name] = roleAssignmentObjectResolverRow.RoleIdentity;
			}
			this.roleObjectIds = dictionary.Values;
		}

		// Token: 0x040027B8 RID: 10168
		private AggregatedScope aggregatedScope;

		// Token: 0x040027B9 RID: 10169
		private List<SecurityPrincipalRow> members;

		// Token: 0x040027BA RID: 10170
		private IEnumerable<ADObjectId> roleObjectIds;
	}
}
