using System;
using System.Runtime.CompilerServices;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	// Token: 0x02000213 RID: 531
	public sealed class CryptoKeySecurity : NativeObjectSecurity
	{
		// Token: 0x06001F05 RID: 7941 RVA: 0x0006D191 File Offset: 0x0006B391
		public CryptoKeySecurity() : base(false, ResourceType.FileObject)
		{
		}

		// Token: 0x06001F06 RID: 7942 RVA: 0x0006D19B File Offset: 0x0006B39B
		[SecuritySafeCritical]
		public CryptoKeySecurity(CommonSecurityDescriptor securityDescriptor) : base(ResourceType.FileObject, securityDescriptor)
		{
		}

		// Token: 0x06001F07 RID: 7943 RVA: 0x0006D1A5 File Offset: 0x0006B3A5
		public sealed override AccessRule AccessRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type)
		{
			return new CryptoKeyAccessRule(identityReference, CryptoKeyAccessRule.RightsFromAccessMask(accessMask), type);
		}

		// Token: 0x06001F08 RID: 7944 RVA: 0x0006D1B5 File Offset: 0x0006B3B5
		public sealed override AuditRule AuditRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags)
		{
			return new CryptoKeyAuditRule(identityReference, CryptoKeyAuditRule.RightsFromAccessMask(accessMask), flags);
		}

		// Token: 0x06001F09 RID: 7945 RVA: 0x0006D1C5 File Offset: 0x0006B3C5
		public void AddAccessRule(CryptoKeyAccessRule rule)
		{
			base.AddAccessRule(rule);
		}

		// Token: 0x06001F0A RID: 7946 RVA: 0x0006D1CE File Offset: 0x0006B3CE
		public void SetAccessRule(CryptoKeyAccessRule rule)
		{
			base.SetAccessRule(rule);
		}

		// Token: 0x06001F0B RID: 7947 RVA: 0x0006D1D7 File Offset: 0x0006B3D7
		public void ResetAccessRule(CryptoKeyAccessRule rule)
		{
			base.ResetAccessRule(rule);
		}

		// Token: 0x06001F0C RID: 7948 RVA: 0x0006D1E0 File Offset: 0x0006B3E0
		public bool RemoveAccessRule(CryptoKeyAccessRule rule)
		{
			return base.RemoveAccessRule(rule);
		}

		// Token: 0x06001F0D RID: 7949 RVA: 0x0006D1E9 File Offset: 0x0006B3E9
		public void RemoveAccessRuleAll(CryptoKeyAccessRule rule)
		{
			base.RemoveAccessRuleAll(rule);
		}

		// Token: 0x06001F0E RID: 7950 RVA: 0x0006D1F2 File Offset: 0x0006B3F2
		public void RemoveAccessRuleSpecific(CryptoKeyAccessRule rule)
		{
			base.RemoveAccessRuleSpecific(rule);
		}

		// Token: 0x06001F0F RID: 7951 RVA: 0x0006D1FB File Offset: 0x0006B3FB
		public void AddAuditRule(CryptoKeyAuditRule rule)
		{
			base.AddAuditRule(rule);
		}

		// Token: 0x06001F10 RID: 7952 RVA: 0x0006D204 File Offset: 0x0006B404
		public void SetAuditRule(CryptoKeyAuditRule rule)
		{
			base.SetAuditRule(rule);
		}

		// Token: 0x06001F11 RID: 7953 RVA: 0x0006D20D File Offset: 0x0006B40D
		public bool RemoveAuditRule(CryptoKeyAuditRule rule)
		{
			return base.RemoveAuditRule(rule);
		}

		// Token: 0x06001F12 RID: 7954 RVA: 0x0006D216 File Offset: 0x0006B416
		public void RemoveAuditRuleAll(CryptoKeyAuditRule rule)
		{
			base.RemoveAuditRuleAll(rule);
		}

		// Token: 0x06001F13 RID: 7955 RVA: 0x0006D21F File Offset: 0x0006B41F
		public void RemoveAuditRuleSpecific(CryptoKeyAuditRule rule)
		{
			base.RemoveAuditRuleSpecific(rule);
		}

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06001F14 RID: 7956 RVA: 0x0006D228 File Offset: 0x0006B428
		public override Type AccessRightType
		{
			get
			{
				return typeof(CryptoKeyRights);
			}
		}

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06001F15 RID: 7957 RVA: 0x0006D234 File Offset: 0x0006B434
		public override Type AccessRuleType
		{
			get
			{
				return typeof(CryptoKeyAccessRule);
			}
		}

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06001F16 RID: 7958 RVA: 0x0006D240 File Offset: 0x0006B440
		public override Type AuditRuleType
		{
			get
			{
				return typeof(CryptoKeyAuditRule);
			}
		}

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06001F17 RID: 7959 RVA: 0x0006D24C File Offset: 0x0006B44C
		internal AccessControlSections ChangedAccessControlSections
		{
			[SecurityCritical]
			get
			{
				AccessControlSections accessControlSections = AccessControlSections.None;
				bool flag = false;
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					RuntimeHelpers.PrepareConstrainedRegions();
					try
					{
					}
					finally
					{
						base.ReadLock();
						flag = true;
					}
					if (base.AccessRulesModified)
					{
						accessControlSections |= AccessControlSections.Access;
					}
					if (base.AuditRulesModified)
					{
						accessControlSections |= AccessControlSections.Audit;
					}
					if (base.GroupModified)
					{
						accessControlSections |= AccessControlSections.Group;
					}
					if (base.OwnerModified)
					{
						accessControlSections |= AccessControlSections.Owner;
					}
				}
				finally
				{
					if (flag)
					{
						base.ReadUnlock();
					}
				}
				return accessControlSections;
			}
		}

		// Token: 0x04000B25 RID: 2853
		private const ResourceType s_ResourceType = ResourceType.FileObject;
	}
}
