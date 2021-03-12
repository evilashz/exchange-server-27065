using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.Authorization;

namespace Microsoft.Exchange.Configuration.Authorization
{
	// Token: 0x02000236 RID: 566
	internal class WebServiceRunspaceConfiguration : ExchangeRunspaceConfiguration
	{
		// Token: 0x06001418 RID: 5144 RVA: 0x000489E9 File Offset: 0x00046BE9
		public WebServiceRunspaceConfiguration(IIdentity identity) : base(identity, ExchangeRunspaceConfigurationSettings.GetDefaultInstance())
		{
		}

		// Token: 0x06001419 RID: 5145 RVA: 0x000489F8 File Offset: 0x00046BF8
		public virtual bool IsWebMethodAvailable(string webMethodName)
		{
			if (string.IsNullOrEmpty(webMethodName))
			{
				throw new ArgumentNullException("webMethodName");
			}
			RoleEntryInfo roleInfoForWebMethod = RoleEntryInfo.GetRoleInfoForWebMethod(webMethodName);
			int num = this.allRoleEntries.BinarySearch(roleInfoForWebMethod, RoleEntryInfo.NameComparer);
			if (num < 0)
			{
				ExTraceGlobals.AccessCheckTracer.TraceWarning<string, string>((long)this.GetHashCode(), "IsWebMethodAvailable() returns false for user {0} because web method {1} is not in role.", this.identityName, webMethodName);
				return false;
			}
			return true;
		}

		// Token: 0x0600141A RID: 5146 RVA: 0x00048AAC File Offset: 0x00046CAC
		public virtual bool IsWebMethodInRole(string webMethodName, RoleType roleType)
		{
			if (string.IsNullOrEmpty(webMethodName))
			{
				throw new ArgumentNullException("webMethodName");
			}
			List<ADObjectId> rolesFromRoleType = this.GetRolesFromRoleType(roleType);
			if (rolesFromRoleType == null)
			{
				ExTraceGlobals.AccessDeniedTracer.TraceWarning<string, RoleType>((long)this.GetHashCode(), "IsWebMethodInRole() returns false because identity {0} doesn't have role {1}", this.identityName, roleType);
				return false;
			}
			List<RoleEntryInfo> list = this.allRoleEntries.FindAll((RoleEntryInfo x) => x.RoleEntry.Name.Equals(webMethodName, StringComparison.OrdinalIgnoreCase));
			if (list.Count == 0)
			{
				ExTraceGlobals.AccessDeniedTracer.TraceWarning<string, RoleType, string>((long)this.GetHashCode(), "IsWebMethodInRole() returns false because identity {0}'s role {1} doesn't include web method {2}.", this.identityName, roleType, webMethodName);
				return false;
			}
			using (List<ADObjectId>.Enumerator enumerator = rolesFromRoleType.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					ADObjectId roleId = enumerator.Current;
					IEnumerable<ExchangeRoleAssignment> enumerable = from x in this.allRoleAssignments
					where x.Role.Equals(roleId)
					select x;
					foreach (ExchangeRoleAssignment exchangeRoleAssignment in enumerable)
					{
						if (list.Exists((RoleEntryInfo x) => x.RoleAssignment.Role.Equals(roleId)))
						{
							return true;
						}
					}
				}
			}
			ExTraceGlobals.AccessDeniedTracer.TraceWarning<string, RoleType, string>((long)this.GetHashCode(), "IsWebMethodInRole() returns false because identity {0}'s role {1}'s role assigments are not associated with the roles that include web method {2}.", this.identityName, roleType, webMethodName);
			return false;
		}

		// Token: 0x0600141B RID: 5147 RVA: 0x00048C4C File Offset: 0x00046E4C
		public virtual List<ADObjectId> GetRolesFromRoleType(RoleType roleType)
		{
			List<ADObjectId> result = null;
			this.allRoleTypes.TryGetValue(roleType, out result);
			return result;
		}

		// Token: 0x0600141C RID: 5148 RVA: 0x00048C88 File Offset: 0x00046E88
		public virtual bool IsTargetObjectInRoleScope(RoleType roleType, ADRecipient targetRecipient)
		{
			if (targetRecipient == null)
			{
				throw new ArgumentNullException("targetRecipient");
			}
			List<ADObjectId> rolesFromRoleType = this.GetRolesFromRoleType(roleType);
			if (rolesFromRoleType == null)
			{
				ExTraceGlobals.AccessDeniedTracer.TraceWarning<string, RoleType>((long)this.GetHashCode(), "IsTargetObjectInRoleScope() returns false because identity {0} doesn't have role {1}", this.identityName, roleType);
				return false;
			}
			using (List<ADObjectId>.Enumerator enumerator = rolesFromRoleType.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					ADObjectId roleId = enumerator.Current;
					IEnumerable<ExchangeRoleAssignment> enumerable = from x in this.allRoleAssignments
					where x.Role.Equals(roleId)
					select x;
					foreach (ExchangeRoleAssignment exchangeRoleAssignment in enumerable)
					{
						RoleAssignmentScopeSet effectiveScopeSet = exchangeRoleAssignment.GetEffectiveScopeSet(this.allScopes, base.UserAccessToken);
						OrganizationId organizationId = targetRecipient.OrganizationId;
						effectiveScopeSet.RecipientReadScope.PopulateRootAndFilter(organizationId, targetRecipient);
						effectiveScopeSet.RecipientWriteScope.PopulateRootAndFilter(organizationId, targetRecipient);
						ADScopeException ex;
						if (ADSession.TryVerifyIsWithinScopes(targetRecipient, effectiveScopeSet.RecipientReadScope, new ADScopeCollection[]
						{
							new ADScopeCollection(new ADScope[]
							{
								effectiveScopeSet.RecipientWriteScope
							})
						}, this.exclusiveRecipientScopesCollection, false, out ex))
						{
							return true;
						}
					}
				}
			}
			ExTraceGlobals.AccessDeniedTracer.TraceWarning<string, RoleType, ObjectId>((long)this.GetHashCode(), "IsTargetObjectInRoleScope() returns false because identity {0}'s roles of type {1} don't have the scope that covers target object {2}.", this.identityName, roleType, targetRecipient.Identity);
			return false;
		}

		// Token: 0x0600141D RID: 5149 RVA: 0x00048E38 File Offset: 0x00047038
		public RoleType[] ResolveRoleTypeByMethod(string webMethodName)
		{
			if (string.IsNullOrEmpty(webMethodName))
			{
				throw new ArgumentNullException("webMethodName");
			}
			return (from r in this.allRoleTypes.Keys
			where this.IsWebMethodInRole(webMethodName, r)
			select r).ToArray<RoleType>();
		}
	}
}
