using System;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Threading;
using Microsoft.Win32.SafeHandles;

namespace System.Security.AccessControl
{
	// Token: 0x02000217 RID: 535
	public sealed class EventWaitHandleSecurity : NativeObjectSecurity
	{
		// Token: 0x06001F1F RID: 7967 RVA: 0x0006D32D File Offset: 0x0006B52D
		public EventWaitHandleSecurity() : base(true, ResourceType.KernelObject)
		{
		}

		// Token: 0x06001F20 RID: 7968 RVA: 0x0006D337 File Offset: 0x0006B537
		[SecurityCritical]
		internal EventWaitHandleSecurity(string name, AccessControlSections includeSections) : base(true, ResourceType.KernelObject, name, includeSections, new NativeObjectSecurity.ExceptionFromErrorCode(EventWaitHandleSecurity._HandleErrorCode), null)
		{
		}

		// Token: 0x06001F21 RID: 7969 RVA: 0x0006D350 File Offset: 0x0006B550
		[SecurityCritical]
		internal EventWaitHandleSecurity(SafeWaitHandle handle, AccessControlSections includeSections) : base(true, ResourceType.KernelObject, handle, includeSections, new NativeObjectSecurity.ExceptionFromErrorCode(EventWaitHandleSecurity._HandleErrorCode), null)
		{
		}

		// Token: 0x06001F22 RID: 7970 RVA: 0x0006D36C File Offset: 0x0006B56C
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

		// Token: 0x06001F23 RID: 7971 RVA: 0x0006D3B6 File Offset: 0x0006B5B6
		public override AccessRule AccessRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type)
		{
			return new EventWaitHandleAccessRule(identityReference, accessMask, isInherited, inheritanceFlags, propagationFlags, type);
		}

		// Token: 0x06001F24 RID: 7972 RVA: 0x0006D3C6 File Offset: 0x0006B5C6
		public override AuditRule AuditRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags)
		{
			return new EventWaitHandleAuditRule(identityReference, accessMask, isInherited, inheritanceFlags, propagationFlags, flags);
		}

		// Token: 0x06001F25 RID: 7973 RVA: 0x0006D3D8 File Offset: 0x0006B5D8
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

		// Token: 0x06001F26 RID: 7974 RVA: 0x0006D418 File Offset: 0x0006B618
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

		// Token: 0x06001F27 RID: 7975 RVA: 0x0006D47C File Offset: 0x0006B67C
		public void AddAccessRule(EventWaitHandleAccessRule rule)
		{
			base.AddAccessRule(rule);
		}

		// Token: 0x06001F28 RID: 7976 RVA: 0x0006D485 File Offset: 0x0006B685
		public void SetAccessRule(EventWaitHandleAccessRule rule)
		{
			base.SetAccessRule(rule);
		}

		// Token: 0x06001F29 RID: 7977 RVA: 0x0006D48E File Offset: 0x0006B68E
		public void ResetAccessRule(EventWaitHandleAccessRule rule)
		{
			base.ResetAccessRule(rule);
		}

		// Token: 0x06001F2A RID: 7978 RVA: 0x0006D497 File Offset: 0x0006B697
		public bool RemoveAccessRule(EventWaitHandleAccessRule rule)
		{
			return base.RemoveAccessRule(rule);
		}

		// Token: 0x06001F2B RID: 7979 RVA: 0x0006D4A0 File Offset: 0x0006B6A0
		public void RemoveAccessRuleAll(EventWaitHandleAccessRule rule)
		{
			base.RemoveAccessRuleAll(rule);
		}

		// Token: 0x06001F2C RID: 7980 RVA: 0x0006D4A9 File Offset: 0x0006B6A9
		public void RemoveAccessRuleSpecific(EventWaitHandleAccessRule rule)
		{
			base.RemoveAccessRuleSpecific(rule);
		}

		// Token: 0x06001F2D RID: 7981 RVA: 0x0006D4B2 File Offset: 0x0006B6B2
		public void AddAuditRule(EventWaitHandleAuditRule rule)
		{
			base.AddAuditRule(rule);
		}

		// Token: 0x06001F2E RID: 7982 RVA: 0x0006D4BB File Offset: 0x0006B6BB
		public void SetAuditRule(EventWaitHandleAuditRule rule)
		{
			base.SetAuditRule(rule);
		}

		// Token: 0x06001F2F RID: 7983 RVA: 0x0006D4C4 File Offset: 0x0006B6C4
		public bool RemoveAuditRule(EventWaitHandleAuditRule rule)
		{
			return base.RemoveAuditRule(rule);
		}

		// Token: 0x06001F30 RID: 7984 RVA: 0x0006D4CD File Offset: 0x0006B6CD
		public void RemoveAuditRuleAll(EventWaitHandleAuditRule rule)
		{
			base.RemoveAuditRuleAll(rule);
		}

		// Token: 0x06001F31 RID: 7985 RVA: 0x0006D4D6 File Offset: 0x0006B6D6
		public void RemoveAuditRuleSpecific(EventWaitHandleAuditRule rule)
		{
			base.RemoveAuditRuleSpecific(rule);
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06001F32 RID: 7986 RVA: 0x0006D4DF File Offset: 0x0006B6DF
		public override Type AccessRightType
		{
			get
			{
				return typeof(EventWaitHandleRights);
			}
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06001F33 RID: 7987 RVA: 0x0006D4EB File Offset: 0x0006B6EB
		public override Type AccessRuleType
		{
			get
			{
				return typeof(EventWaitHandleAccessRule);
			}
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06001F34 RID: 7988 RVA: 0x0006D4F7 File Offset: 0x0006B6F7
		public override Type AuditRuleType
		{
			get
			{
				return typeof(EventWaitHandleAuditRule);
			}
		}
	}
}
