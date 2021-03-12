using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000004 RID: 4
	internal struct RoleDefinition
	{
		// Token: 0x0600000A RID: 10 RVA: 0x00002398 File Offset: 0x00000598
		public static string GetDCSafeNameForRole(string roleName)
		{
			string text = string.Format("DCSafe {0}", roleName);
			if (text.Length > 64)
			{
				text = text.Substring(0, 64);
			}
			return text.Trim();
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000023CB File Offset: 0x000005CB
		public RoleDefinition(string theRoleName, RoleType theRoleType, RoleCmdlet[] theCmdlets)
		{
			this.roleName = theRoleName;
			this.roleType = theRoleType;
			this.cmdlets = theCmdlets;
			this.parentRoleName = null;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000023E9 File Offset: 0x000005E9
		public RoleDefinition(string theRoleName, string theParentRoleName, RoleType theRoleType, RoleCmdlet[] theCmdlets)
		{
			this = new RoleDefinition(theRoleName, theRoleType, theCmdlets);
			this.parentRoleName = theParentRoleName;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000D RID: 13 RVA: 0x000023FC File Offset: 0x000005FC
		public string RoleName
		{
			get
			{
				return this.roleName;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000E RID: 14 RVA: 0x00002404 File Offset: 0x00000604
		public string ParentRoleName
		{
			get
			{
				return this.parentRoleName;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000F RID: 15 RVA: 0x0000240C File Offset: 0x0000060C
		internal RoleType RoleType
		{
			get
			{
				return this.roleType;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000010 RID: 16 RVA: 0x00002414 File Offset: 0x00000614
		internal bool IsEndUserRole
		{
			get
			{
				return Array.BinarySearch<RoleType>(ExchangeRole.EndUserRoleTypes, this.RoleType) >= 0;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000011 RID: 17 RVA: 0x0000242C File Offset: 0x0000062C
		internal RoleCmdlet[] Cmdlets
		{
			get
			{
				return this.cmdlets;
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002434 File Offset: 0x00000634
		public ExchangeRole GenerateRole(List<string> enabledFeatures, ADObjectId rolesContainerId, string suffix, string resolutionType)
		{
			ExchangeRole exchangeRole = new ExchangeRole();
			string input = null;
			exchangeRole.SetId(rolesContainerId.GetChildId(this.RoleName + suffix));
			exchangeRole.RoleType = this.roleType;
			exchangeRole.MailboxPlanIndex = resolutionType;
			exchangeRole.StampImplicitScopes();
			exchangeRole.StampIsEndUserRole();
			foreach (RoleCmdlet roleCmdlet in this.cmdlets)
			{
				if (roleCmdlet.TryGenerateRoleEntry(enabledFeatures, ref input))
				{
					exchangeRole.RoleEntries.Add(RoleEntry.Parse(input));
				}
			}
			return exchangeRole;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000024BC File Offset: 0x000006BC
		public List<string> GetRequiredFeaturesForRoleEntries()
		{
			List<string> list = new List<string>();
			foreach (RoleCmdlet roleCmdlet in this.cmdlets)
			{
				list.AddRange(roleCmdlet.GetRequiredFeaturesForAllParameters());
			}
			return list.Distinct(StringComparer.OrdinalIgnoreCase).ToList<string>();
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002520 File Offset: 0x00000720
		public bool ContainsProhibitedActions(List<string> prohibitedActions)
		{
			List<string> requiredFeaturesForRoleEntries = this.GetRequiredFeaturesForRoleEntries();
			return requiredFeaturesForRoleEntries.FirstOrDefault((string x) => prohibitedActions.Contains(x, StringComparer.OrdinalIgnoreCase)) != null;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002558 File Offset: 0x00000758
		public List<RoleEntry> GetRoleEntriesFilteringProhibitedActions(List<string> features, List<string> prohibitedActions)
		{
			if (!this.ContainsProhibitedActions(prohibitedActions))
			{
				throw new InvalidOperationException(string.Format(" Role '{0}' doesn't have any prohibited action.", this.RoleName));
			}
			string input = null;
			List<RoleEntry> list = new List<RoleEntry>(this.cmdlets.Length);
			foreach (RoleCmdlet roleCmdlet in this.cmdlets)
			{
				if (roleCmdlet.TryGenerateRoleEntryFilteringProhibitedActions(features, prohibitedActions, ref input))
				{
					list.Add(RoleEntry.Parse(input));
				}
			}
			return list;
		}

		// Token: 0x04000007 RID: 7
		private const string SafeRolePrefixFormat = "DCSafe {0}";

		// Token: 0x04000008 RID: 8
		private RoleType roleType;

		// Token: 0x04000009 RID: 9
		private RoleCmdlet[] cmdlets;

		// Token: 0x0400000A RID: 10
		private string roleName;

		// Token: 0x0400000B RID: 11
		private string parentRoleName;
	}
}
