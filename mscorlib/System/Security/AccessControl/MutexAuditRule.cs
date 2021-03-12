using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	// Token: 0x02000220 RID: 544
	public sealed class MutexAuditRule : AuditRule
	{
		// Token: 0x06001F64 RID: 8036 RVA: 0x0006DB7A File Offset: 0x0006BD7A
		public MutexAuditRule(IdentityReference identity, MutexRights eventRights, AuditFlags flags) : this(identity, (int)eventRights, false, InheritanceFlags.None, PropagationFlags.None, flags)
		{
		}

		// Token: 0x06001F65 RID: 8037 RVA: 0x0006DB88 File Offset: 0x0006BD88
		internal MutexAuditRule(IdentityReference identity, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags) : base(identity, accessMask, isInherited, inheritanceFlags, propagationFlags, flags)
		{
		}

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06001F66 RID: 8038 RVA: 0x0006DB99 File Offset: 0x0006BD99
		public MutexRights MutexRights
		{
			get
			{
				return (MutexRights)base.AccessMask;
			}
		}
	}
}
