using System;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Threading;
using Microsoft.Win32.SafeHandles;

namespace System.Security.AccessControl
{
	// Token: 0x02000221 RID: 545
	public sealed class MutexSecurity : NativeObjectSecurity
	{
		// Token: 0x06001F67 RID: 8039 RVA: 0x0006DBA1 File Offset: 0x0006BDA1
		public MutexSecurity() : base(true, ResourceType.KernelObject)
		{
		}

		// Token: 0x06001F68 RID: 8040 RVA: 0x0006DBAB File Offset: 0x0006BDAB
		[SecuritySafeCritical]
		public MutexSecurity(string name, AccessControlSections includeSections) : base(true, ResourceType.KernelObject, name, includeSections, new NativeObjectSecurity.ExceptionFromErrorCode(MutexSecurity._HandleErrorCode), null)
		{
		}

		// Token: 0x06001F69 RID: 8041 RVA: 0x0006DBC4 File Offset: 0x0006BDC4
		[SecurityCritical]
		internal MutexSecurity(SafeWaitHandle handle, AccessControlSections includeSections) : base(true, ResourceType.KernelObject, handle, includeSections, new NativeObjectSecurity.ExceptionFromErrorCode(MutexSecurity._HandleErrorCode), null)
		{
		}

		// Token: 0x06001F6A RID: 8042 RVA: 0x0006DBE0 File Offset: 0x0006BDE0
		[SecurityCritical]
		private static Exception _HandleErrorCode(int errorCode, string name, SafeHandle handle, object context)
		{
			Exception result = null;
			if (errorCode == 2 || errorCode == 6 || errorCode == 123)
			{
				if (name != null && name.Length != 0)
				{
					result = new WaitHandleCannotBeOpenedException(Environment.GetResourceString("Threading.WaitHandleCannotBeOpenedException_InvalidHandle", new object[]
					{
						name
					}));
				}
				else
				{
					result = new WaitHandleCannotBeOpenedException();
				}
			}
			return result;
		}

		// Token: 0x06001F6B RID: 8043 RVA: 0x0006DC2A File Offset: 0x0006BE2A
		public override AccessRule AccessRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type)
		{
			return new MutexAccessRule(identityReference, accessMask, isInherited, inheritanceFlags, propagationFlags, type);
		}

		// Token: 0x06001F6C RID: 8044 RVA: 0x0006DC3A File Offset: 0x0006BE3A
		public override AuditRule AuditRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags)
		{
			return new MutexAuditRule(identityReference, accessMask, isInherited, inheritanceFlags, propagationFlags, flags);
		}

		// Token: 0x06001F6D RID: 8045 RVA: 0x0006DC4C File Offset: 0x0006BE4C
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

		// Token: 0x06001F6E RID: 8046 RVA: 0x0006DC8C File Offset: 0x0006BE8C
		[SecurityCritical]
		internal void Persist(SafeWaitHandle handle)
		{
			base.WriteLock();
			try
			{
				AccessControlSections accessControlSectionsFromChanges = this.GetAccessControlSectionsFromChanges();
				if (accessControlSectionsFromChanges != AccessControlSections.None)
				{
					base.Persist(handle, accessControlSectionsFromChanges);
					base.OwnerModified = (base.GroupModified = (base.AuditRulesModified = (base.AccessRulesModified = false)));
				}
			}
			finally
			{
				base.WriteUnlock();
			}
		}

		// Token: 0x06001F6F RID: 8047 RVA: 0x0006DCF0 File Offset: 0x0006BEF0
		public void AddAccessRule(MutexAccessRule rule)
		{
			base.AddAccessRule(rule);
		}

		// Token: 0x06001F70 RID: 8048 RVA: 0x0006DCF9 File Offset: 0x0006BEF9
		public void SetAccessRule(MutexAccessRule rule)
		{
			base.SetAccessRule(rule);
		}

		// Token: 0x06001F71 RID: 8049 RVA: 0x0006DD02 File Offset: 0x0006BF02
		public void ResetAccessRule(MutexAccessRule rule)
		{
			base.ResetAccessRule(rule);
		}

		// Token: 0x06001F72 RID: 8050 RVA: 0x0006DD0B File Offset: 0x0006BF0B
		public bool RemoveAccessRule(MutexAccessRule rule)
		{
			return base.RemoveAccessRule(rule);
		}

		// Token: 0x06001F73 RID: 8051 RVA: 0x0006DD14 File Offset: 0x0006BF14
		public void RemoveAccessRuleAll(MutexAccessRule rule)
		{
			base.RemoveAccessRuleAll(rule);
		}

		// Token: 0x06001F74 RID: 8052 RVA: 0x0006DD1D File Offset: 0x0006BF1D
		public void RemoveAccessRuleSpecific(MutexAccessRule rule)
		{
			base.RemoveAccessRuleSpecific(rule);
		}

		// Token: 0x06001F75 RID: 8053 RVA: 0x0006DD26 File Offset: 0x0006BF26
		public void AddAuditRule(MutexAuditRule rule)
		{
			base.AddAuditRule(rule);
		}

		// Token: 0x06001F76 RID: 8054 RVA: 0x0006DD2F File Offset: 0x0006BF2F
		public void SetAuditRule(MutexAuditRule rule)
		{
			base.SetAuditRule(rule);
		}

		// Token: 0x06001F77 RID: 8055 RVA: 0x0006DD38 File Offset: 0x0006BF38
		public bool RemoveAuditRule(MutexAuditRule rule)
		{
			return base.RemoveAuditRule(rule);
		}

		// Token: 0x06001F78 RID: 8056 RVA: 0x0006DD41 File Offset: 0x0006BF41
		public void RemoveAuditRuleAll(MutexAuditRule rule)
		{
			base.RemoveAuditRuleAll(rule);
		}

		// Token: 0x06001F79 RID: 8057 RVA: 0x0006DD4A File Offset: 0x0006BF4A
		public void RemoveAuditRuleSpecific(MutexAuditRule rule)
		{
			base.RemoveAuditRuleSpecific(rule);
		}

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06001F7A RID: 8058 RVA: 0x0006DD53 File Offset: 0x0006BF53
		public override Type AccessRightType
		{
			get
			{
				return typeof(MutexRights);
			}
		}

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06001F7B RID: 8059 RVA: 0x0006DD5F File Offset: 0x0006BF5F
		public override Type AccessRuleType
		{
			get
			{
				return typeof(MutexAccessRule);
			}
		}

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06001F7C RID: 8060 RVA: 0x0006DD6B File Offset: 0x0006BF6B
		public override Type AuditRuleType
		{
			get
			{
				return typeof(MutexAuditRule);
			}
		}
	}
}
