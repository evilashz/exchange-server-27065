using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	// Token: 0x0200022E RID: 558
	public sealed class RegistryAuditRule : AuditRule
	{
		// Token: 0x0600201F RID: 8223 RVA: 0x00070F96 File Offset: 0x0006F196
		public RegistryAuditRule(IdentityReference identity, RegistryRights registryRights, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags) : this(identity, (int)registryRights, false, inheritanceFlags, propagationFlags, flags)
		{
		}

		// Token: 0x06002020 RID: 8224 RVA: 0x00070FA6 File Offset: 0x0006F1A6
		public RegistryAuditRule(string identity, RegistryRights registryRights, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags) : this(new NTAccount(identity), (int)registryRights, false, inheritanceFlags, propagationFlags, flags)
		{
		}

		// Token: 0x06002021 RID: 8225 RVA: 0x00070FBB File Offset: 0x0006F1BB
		internal RegistryAuditRule(IdentityReference identity, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags) : base(identity, accessMask, isInherited, inheritanceFlags, propagationFlags, flags)
		{
		}

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x06002022 RID: 8226 RVA: 0x00070FCC File Offset: 0x0006F1CC
		public RegistryRights RegistryRights
		{
			get
			{
				return (RegistryRights)base.AccessMask;
			}
		}
	}
}
