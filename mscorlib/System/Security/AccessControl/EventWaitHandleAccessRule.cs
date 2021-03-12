using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	// Token: 0x02000215 RID: 533
	public sealed class EventWaitHandleAccessRule : AccessRule
	{
		// Token: 0x06001F18 RID: 7960 RVA: 0x0006D2CC File Offset: 0x0006B4CC
		public EventWaitHandleAccessRule(IdentityReference identity, EventWaitHandleRights eventRights, AccessControlType type) : this(identity, (int)eventRights, false, InheritanceFlags.None, PropagationFlags.None, type)
		{
		}

		// Token: 0x06001F19 RID: 7961 RVA: 0x0006D2DA File Offset: 0x0006B4DA
		public EventWaitHandleAccessRule(string identity, EventWaitHandleRights eventRights, AccessControlType type) : this(new NTAccount(identity), (int)eventRights, false, InheritanceFlags.None, PropagationFlags.None, type)
		{
		}

		// Token: 0x06001F1A RID: 7962 RVA: 0x0006D2ED File Offset: 0x0006B4ED
		internal EventWaitHandleAccessRule(IdentityReference identity, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type) : base(identity, accessMask, isInherited, inheritanceFlags, propagationFlags, type)
		{
		}

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06001F1B RID: 7963 RVA: 0x0006D2FE File Offset: 0x0006B4FE
		public EventWaitHandleRights EventWaitHandleRights
		{
			get
			{
				return (EventWaitHandleRights)base.AccessMask;
			}
		}
	}
}
