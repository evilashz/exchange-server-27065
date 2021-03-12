using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Security.AccessControl;
using System.Security.Principal;

namespace Microsoft.Exchange.Security.Authorization
{
	// Token: 0x0200064B RID: 1611
	internal class PrincipalPermissionList : List<PrincipalPermissionPair>
	{
		// Token: 0x06001D31 RID: 7473 RVA: 0x000353DE File Offset: 0x000335DE
		public PrincipalPermissionList()
		{
		}

		// Token: 0x06001D32 RID: 7474 RVA: 0x000353E6 File Offset: 0x000335E6
		public PrincipalPermissionList(int capacity) : base(capacity)
		{
		}

		// Token: 0x06001D33 RID: 7475 RVA: 0x000353EF File Offset: 0x000335EF
		public void Add(SecurityIdentifier sid, Permission permission)
		{
			base.Add(new PrincipalPermissionPair(sid, permission, AccessControlType.Allow));
		}

		// Token: 0x06001D34 RID: 7476 RVA: 0x000353FF File Offset: 0x000335FF
		public void AddDeny(SecurityIdentifier sid, Permission permission)
		{
			base.Add(new PrincipalPermissionPair(sid, permission, AccessControlType.Deny));
		}

		// Token: 0x06001D35 RID: 7477 RVA: 0x00035428 File Offset: 0x00033628
		public RawSecurityDescriptor CreateExtendedRightsSecurityDescriptor(SecurityIdentifier owner, SecurityIdentifier group)
		{
			ActiveDirectorySecurity ads = new ActiveDirectorySecurity();
			this.ForEach(delegate(ActiveDirectoryAccessRule ace)
			{
				ads.AddAccessRule(ace);
			});
			ads.SetOwner(owner);
			ads.SetGroup(group);
			return new RawSecurityDescriptor(ads.GetSecurityDescriptorSddlForm(AccessControlSections.All));
		}

		// Token: 0x06001D36 RID: 7478 RVA: 0x00035498 File Offset: 0x00033698
		public RawSecurityDescriptor AddExtendedRightsToSecurityDescriptor(RawSecurityDescriptor rsd)
		{
			ActiveDirectorySecurity ads = new ActiveDirectorySecurity();
			ads.SetSecurityDescriptorSddlForm(rsd.GetSddlForm(AccessControlSections.All));
			this.ForEach(delegate(ActiveDirectoryAccessRule ace)
			{
				ads.AddAccessRule(ace);
			});
			return new RawSecurityDescriptor(ads.GetSecurityDescriptorSddlForm(AccessControlSections.All));
		}

		// Token: 0x06001D37 RID: 7479 RVA: 0x00035504 File Offset: 0x00033704
		public RawSecurityDescriptor RemoveExtendedRightsFromSecurityDescriptor(RawSecurityDescriptor rsd)
		{
			ActiveDirectorySecurity ads = new ActiveDirectorySecurity();
			ads.SetSecurityDescriptorSddlForm(rsd.GetSddlForm(AccessControlSections.All));
			this.ForEach(delegate(ActiveDirectoryAccessRule ace)
			{
				ads.RemoveAccessRule(ace);
			});
			return new RawSecurityDescriptor(ads.GetSecurityDescriptorSddlForm(AccessControlSections.All));
		}

		// Token: 0x06001D38 RID: 7480 RVA: 0x0003555C File Offset: 0x0003375C
		private void ForEach(Action<ActiveDirectoryAccessRule> action)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			foreach (PrincipalPermissionPair principalPermissionPair in this)
			{
				foreach (Guid objectType in WellKnownPermission.ToGuids(principalPermissionPair.Permission))
				{
					ActiveDirectoryAccessRule obj = new ActiveDirectoryAccessRule(principalPermissionPair.Principal, ActiveDirectoryRights.ExtendedRight, principalPermissionPair.AccessControlType, objectType, ActiveDirectorySecurityInheritance.All);
					action(obj);
				}
			}
		}
	}
}
