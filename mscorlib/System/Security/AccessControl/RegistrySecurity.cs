using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Security.Principal;
using Microsoft.Win32.SafeHandles;

namespace System.Security.AccessControl
{
	// Token: 0x0200022F RID: 559
	public sealed class RegistrySecurity : NativeObjectSecurity
	{
		// Token: 0x06002023 RID: 8227 RVA: 0x00070FD4 File Offset: 0x0006F1D4
		public RegistrySecurity() : base(true, ResourceType.RegistryKey)
		{
		}

		// Token: 0x06002024 RID: 8228 RVA: 0x00070FDE File Offset: 0x0006F1DE
		[SecurityCritical]
		[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
		internal RegistrySecurity(SafeRegistryHandle hKey, string name, AccessControlSections includeSections) : base(true, ResourceType.RegistryKey, hKey, includeSections, new NativeObjectSecurity.ExceptionFromErrorCode(RegistrySecurity._HandleErrorCode), null)
		{
			new RegistryPermission(RegistryPermissionAccess.NoAccess, AccessControlActions.View, name).Demand();
		}

		// Token: 0x06002025 RID: 8229 RVA: 0x00071004 File Offset: 0x0006F204
		[SecurityCritical]
		private static Exception _HandleErrorCode(int errorCode, string name, SafeHandle handle, object context)
		{
			Exception result = null;
			if (errorCode != 2)
			{
				if (errorCode != 6)
				{
					if (errorCode == 123)
					{
						result = new ArgumentException(Environment.GetResourceString("Arg_RegInvalidKeyName", new object[]
						{
							"name"
						}));
					}
				}
				else
				{
					result = new ArgumentException(Environment.GetResourceString("AccessControl_InvalidHandle"));
				}
			}
			else
			{
				result = new IOException(Environment.GetResourceString("Arg_RegKeyNotFound", new object[]
				{
					errorCode
				}));
			}
			return result;
		}

		// Token: 0x06002026 RID: 8230 RVA: 0x00071074 File Offset: 0x0006F274
		public override AccessRule AccessRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type)
		{
			return new RegistryAccessRule(identityReference, accessMask, isInherited, inheritanceFlags, propagationFlags, type);
		}

		// Token: 0x06002027 RID: 8231 RVA: 0x00071084 File Offset: 0x0006F284
		public override AuditRule AuditRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags)
		{
			return new RegistryAuditRule(identityReference, accessMask, isInherited, inheritanceFlags, propagationFlags, flags);
		}

		// Token: 0x06002028 RID: 8232 RVA: 0x00071094 File Offset: 0x0006F294
		internal AccessControlSections GetAccessControlSectionsFromChanges()
		{
			AccessControlSections accessControlSections = AccessControlSections.None;
			if (base.AccessRulesModified)
			{
				accessControlSections = AccessControlSections.Access;
			}
			if (base.AuditRulesModified)
			{
				accessControlSections |= AccessControlSections.Audit;
			}
			if (base.OwnerModified)
			{
				accessControlSections |= AccessControlSections.Owner;
			}
			if (base.GroupModified)
			{
				accessControlSections |= AccessControlSections.Group;
			}
			return accessControlSections;
		}

		// Token: 0x06002029 RID: 8233 RVA: 0x000710D4 File Offset: 0x0006F2D4
		[SecurityCritical]
		[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
		internal void Persist(SafeRegistryHandle hKey, string keyName)
		{
			new RegistryPermission(RegistryPermissionAccess.NoAccess, AccessControlActions.Change, keyName).Demand();
			base.WriteLock();
			try
			{
				AccessControlSections accessControlSectionsFromChanges = this.GetAccessControlSectionsFromChanges();
				if (accessControlSectionsFromChanges != AccessControlSections.None)
				{
					base.Persist(hKey, accessControlSectionsFromChanges);
					base.OwnerModified = (base.GroupModified = (base.AuditRulesModified = (base.AccessRulesModified = false)));
				}
			}
			finally
			{
				base.WriteUnlock();
			}
		}

		// Token: 0x0600202A RID: 8234 RVA: 0x00071144 File Offset: 0x0006F344
		public void AddAccessRule(RegistryAccessRule rule)
		{
			base.AddAccessRule(rule);
		}

		// Token: 0x0600202B RID: 8235 RVA: 0x0007114D File Offset: 0x0006F34D
		public void SetAccessRule(RegistryAccessRule rule)
		{
			base.SetAccessRule(rule);
		}

		// Token: 0x0600202C RID: 8236 RVA: 0x00071156 File Offset: 0x0006F356
		public void ResetAccessRule(RegistryAccessRule rule)
		{
			base.ResetAccessRule(rule);
		}

		// Token: 0x0600202D RID: 8237 RVA: 0x0007115F File Offset: 0x0006F35F
		public bool RemoveAccessRule(RegistryAccessRule rule)
		{
			return base.RemoveAccessRule(rule);
		}

		// Token: 0x0600202E RID: 8238 RVA: 0x00071168 File Offset: 0x0006F368
		public void RemoveAccessRuleAll(RegistryAccessRule rule)
		{
			base.RemoveAccessRuleAll(rule);
		}

		// Token: 0x0600202F RID: 8239 RVA: 0x00071171 File Offset: 0x0006F371
		public void RemoveAccessRuleSpecific(RegistryAccessRule rule)
		{
			base.RemoveAccessRuleSpecific(rule);
		}

		// Token: 0x06002030 RID: 8240 RVA: 0x0007117A File Offset: 0x0006F37A
		public void AddAuditRule(RegistryAuditRule rule)
		{
			base.AddAuditRule(rule);
		}

		// Token: 0x06002031 RID: 8241 RVA: 0x00071183 File Offset: 0x0006F383
		public void SetAuditRule(RegistryAuditRule rule)
		{
			base.SetAuditRule(rule);
		}

		// Token: 0x06002032 RID: 8242 RVA: 0x0007118C File Offset: 0x0006F38C
		public bool RemoveAuditRule(RegistryAuditRule rule)
		{
			return base.RemoveAuditRule(rule);
		}

		// Token: 0x06002033 RID: 8243 RVA: 0x00071195 File Offset: 0x0006F395
		public void RemoveAuditRuleAll(RegistryAuditRule rule)
		{
			base.RemoveAuditRuleAll(rule);
		}

		// Token: 0x06002034 RID: 8244 RVA: 0x0007119E File Offset: 0x0006F39E
		public void RemoveAuditRuleSpecific(RegistryAuditRule rule)
		{
			base.RemoveAuditRuleSpecific(rule);
		}

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x06002035 RID: 8245 RVA: 0x000711A7 File Offset: 0x0006F3A7
		public override Type AccessRightType
		{
			get
			{
				return typeof(RegistryRights);
			}
		}

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06002036 RID: 8246 RVA: 0x000711B3 File Offset: 0x0006F3B3
		public override Type AccessRuleType
		{
			get
			{
				return typeof(RegistryAccessRule);
			}
		}

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06002037 RID: 8247 RVA: 0x000711BF File Offset: 0x0006F3BF
		public override Type AuditRuleType
		{
			get
			{
				return typeof(RegistryAuditRule);
			}
		}
	}
}
