using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	// Token: 0x02000225 RID: 549
	public class AccessRule<T> : AccessRule where T : struct
	{
		// Token: 0x06001FBD RID: 8125 RVA: 0x0006ECA6 File Offset: 0x0006CEA6
		public AccessRule(IdentityReference identity, T rights, AccessControlType type) : this(identity, (int)((object)rights), false, InheritanceFlags.None, PropagationFlags.None, type)
		{
		}

		// Token: 0x06001FBE RID: 8126 RVA: 0x0006ECBE File Offset: 0x0006CEBE
		public AccessRule(string identity, T rights, AccessControlType type) : this(new NTAccount(identity), (int)((object)rights), false, InheritanceFlags.None, PropagationFlags.None, type)
		{
		}

		// Token: 0x06001FBF RID: 8127 RVA: 0x0006ECDB File Offset: 0x0006CEDB
		public AccessRule(IdentityReference identity, T rights, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type) : this(identity, (int)((object)rights), false, inheritanceFlags, propagationFlags, type)
		{
		}

		// Token: 0x06001FC0 RID: 8128 RVA: 0x0006ECF5 File Offset: 0x0006CEF5
		public AccessRule(string identity, T rights, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type) : this(new NTAccount(identity), (int)((object)rights), false, inheritanceFlags, propagationFlags, type)
		{
		}

		// Token: 0x06001FC1 RID: 8129 RVA: 0x0006ED14 File Offset: 0x0006CF14
		internal AccessRule(IdentityReference identity, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type) : base(identity, accessMask, isInherited, inheritanceFlags, propagationFlags, type)
		{
		}

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x06001FC2 RID: 8130 RVA: 0x0006ED25 File Offset: 0x0006CF25
		public T Rights
		{
			get
			{
				return (T)((object)base.AccessMask);
			}
		}
	}
}
