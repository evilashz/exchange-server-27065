using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	// Token: 0x0200022D RID: 557
	public sealed class RegistryAccessRule : AccessRule
	{
		// Token: 0x06002019 RID: 8217 RVA: 0x00070F37 File Offset: 0x0006F137
		public RegistryAccessRule(IdentityReference identity, RegistryRights registryRights, AccessControlType type) : this(identity, (int)registryRights, false, InheritanceFlags.None, PropagationFlags.None, type)
		{
		}

		// Token: 0x0600201A RID: 8218 RVA: 0x00070F45 File Offset: 0x0006F145
		public RegistryAccessRule(string identity, RegistryRights registryRights, AccessControlType type) : this(new NTAccount(identity), (int)registryRights, false, InheritanceFlags.None, PropagationFlags.None, type)
		{
		}

		// Token: 0x0600201B RID: 8219 RVA: 0x00070F58 File Offset: 0x0006F158
		public RegistryAccessRule(IdentityReference identity, RegistryRights registryRights, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type) : this(identity, (int)registryRights, false, inheritanceFlags, propagationFlags, type)
		{
		}

		// Token: 0x0600201C RID: 8220 RVA: 0x00070F68 File Offset: 0x0006F168
		public RegistryAccessRule(string identity, RegistryRights registryRights, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type) : this(new NTAccount(identity), (int)registryRights, false, inheritanceFlags, propagationFlags, type)
		{
		}

		// Token: 0x0600201D RID: 8221 RVA: 0x00070F7D File Offset: 0x0006F17D
		internal RegistryAccessRule(IdentityReference identity, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type) : base(identity, accessMask, isInherited, inheritanceFlags, propagationFlags, type)
		{
		}

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x0600201E RID: 8222 RVA: 0x00070F8E File Offset: 0x0006F18E
		public RegistryRights RegistryRights
		{
			get
			{
				return (RegistryRights)base.AccessMask;
			}
		}
	}
}
