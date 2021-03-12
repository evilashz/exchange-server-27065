using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.PowerShell.RbacHostingTools;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020004D8 RID: 1240
	[DataContract]
	public class AdminRoleGroupRow : BaseRow
	{
		// Token: 0x06003C86 RID: 15494 RVA: 0x000B5E70 File Offset: 0x000B4070
		public AdminRoleGroupRow(RoleGroup adminRoleGroupObject) : base(adminRoleGroupObject)
		{
			this.RoleGroup = adminRoleGroupObject;
			this.IsEditRoleGroupAllowed = (RbacPrincipal.Current.RbacConfiguration.IsCmdletAllowedInScope("Update-RoleGroupMember", new string[]
			{
				"Members"
			}, adminRoleGroupObject, ScopeLocation.RecipientWrite) && (!RbacPrincipal.Current.IsInRole("FFO") || !this.RoleGroup.Capabilities.Contains(Capability.Partner_Managed)));
		}

		// Token: 0x170023D2 RID: 9170
		// (get) Token: 0x06003C87 RID: 15495 RVA: 0x000B5EE5 File Offset: 0x000B40E5
		// (set) Token: 0x06003C88 RID: 15496 RVA: 0x000B5EED File Offset: 0x000B40ED
		public RoleGroup RoleGroup { get; set; }

		// Token: 0x170023D3 RID: 9171
		// (get) Token: 0x06003C89 RID: 15497 RVA: 0x000B5EF6 File Offset: 0x000B40F6
		// (set) Token: 0x06003C8A RID: 15498 RVA: 0x000B5F03 File Offset: 0x000B4103
		[DataMember]
		public string Name
		{
			get
			{
				return this.RoleGroup.Name;
			}
			set
			{
				this.RoleGroup.Name = value;
			}
		}

		// Token: 0x170023D4 RID: 9172
		// (get) Token: 0x06003C8B RID: 15499 RVA: 0x000B5F11 File Offset: 0x000B4111
		// (set) Token: 0x06003C8C RID: 15500 RVA: 0x000B5F28 File Offset: 0x000B4128
		[DataMember]
		public string RoleGroupObjectIdentity
		{
			get
			{
				return this.RoleGroup.DataObject.Identity.ToString();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170023D5 RID: 9173
		// (get) Token: 0x06003C8D RID: 15501 RVA: 0x000B5F2F File Offset: 0x000B412F
		// (set) Token: 0x06003C8E RID: 15502 RVA: 0x000B5F41 File Offset: 0x000B4141
		[DataMember]
		public string DisplayName
		{
			get
			{
				return this.RoleGroup.DataObject.Name;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170023D6 RID: 9174
		// (get) Token: 0x06003C8F RID: 15503 RVA: 0x000B5F48 File Offset: 0x000B4148
		// (set) Token: 0x06003C90 RID: 15504 RVA: 0x000B5F50 File Offset: 0x000B4150
		[DataMember]
		public bool IsEditRoleGroupAllowed { get; private set; }

		// Token: 0x170023D7 RID: 9175
		// (get) Token: 0x06003C91 RID: 15505 RVA: 0x000B5F59 File Offset: 0x000B4159
		// (set) Token: 0x06003C92 RID: 15506 RVA: 0x000B5F7B File Offset: 0x000B417B
		[DataMember]
		public bool IsCopyAllowed
		{
			get
			{
				return !this.IsMultipleScopesScenario && this.IsEditRoleGroupAllowed && !this.RoleGroup.IsReadOnly;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170023D8 RID: 9176
		// (get) Token: 0x06003C93 RID: 15507 RVA: 0x000B5F82 File Offset: 0x000B4182
		// (set) Token: 0x06003C94 RID: 15508 RVA: 0x000B5F99 File Offset: 0x000B4199
		[DataMember]
		public bool IsMultipleScopesScenario
		{
			get
			{
				return this.RoleAssignments != null && this.RoleAssignments.HasMultipleScopeTypes();
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170023D9 RID: 9177
		// (get) Token: 0x06003C95 RID: 15509 RVA: 0x000B5FAC File Offset: 0x000B41AC
		public IEnumerable<RoleAssignmentObjectResolverRow> RoleAssignments
		{
			get
			{
				if (this.roleAssignments == null)
				{
					IEnumerable<RoleAssignmentObjectResolverRow> source = RoleAssignmentObjectResolver.Instance.ResolveObjects(this.RoleGroup.RoleAssignments);
					this.roleAssignments = from entry in source
					where !entry.IsDelegating
					select entry;
				}
				return this.roleAssignments;
			}
		}

		// Token: 0x040027B4 RID: 10164
		private IEnumerable<RoleAssignmentObjectResolverRow> roleAssignments;
	}
}
