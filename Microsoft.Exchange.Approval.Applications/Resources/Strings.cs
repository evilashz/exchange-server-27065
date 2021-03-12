using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Approval.Applications.Resources
{
	// Token: 0x02000002 RID: 2
	internal static class Strings
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		static Strings()
		{
			Strings.stringIDs.Add(2918013799U, "Semicolon");
			Strings.stringIDs.Add(816183154U, "AutoGroupRequestApprovedBody");
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002134 File Offset: 0x00000334
		public static LocalizedString AutoGroupRequestFailedSubject(string group)
		{
			return new LocalizedString("AutoGroupRequestFailedSubject", "Ex5FA695", false, true, Strings.ResourceManager, new object[]
			{
				group
			});
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002164 File Offset: 0x00000364
		public static LocalizedString AutoGroupRequestFailedBodyBadRequester(string group, string requester)
		{
			return new LocalizedString("AutoGroupRequestFailedBodyBadRequester", "ExF5FD46", false, true, Strings.ResourceManager, new object[]
			{
				group,
				requester
			});
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000004 RID: 4 RVA: 0x00002197 File Offset: 0x00000397
		public static LocalizedString Semicolon
		{
			get
			{
				return new LocalizedString("Semicolon", "ExC51658", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000021B8 File Offset: 0x000003B8
		public static LocalizedString AutoGroupRequestExpiredSubject(string group)
		{
			return new LocalizedString("AutoGroupRequestExpiredSubject", "ExFF6103", false, true, Strings.ResourceManager, new object[]
			{
				group
			});
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000021E8 File Offset: 0x000003E8
		public static LocalizedString AutoGroupRequestFailedBodyTaskError(string error)
		{
			return new LocalizedString("AutoGroupRequestFailedBodyTaskError", "Ex3FE882", false, true, Strings.ResourceManager, new object[]
			{
				error
			});
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002218 File Offset: 0x00000418
		public static LocalizedString AutoGroupRequestRejectedSubject(string group)
		{
			return new LocalizedString("AutoGroupRequestRejectedSubject", "Ex8AA8DB", false, true, Strings.ResourceManager, new object[]
			{
				group
			});
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002248 File Offset: 0x00000448
		public static LocalizedString AutoGroupRequestRejectedBody(string owners)
		{
			return new LocalizedString("AutoGroupRequestRejectedBody", "Ex377538", false, true, Strings.ResourceManager, new object[]
			{
				owners
			});
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002278 File Offset: 0x00000478
		public static LocalizedString AutoGroupRequestFailedHeader(string group)
		{
			return new LocalizedString("AutoGroupRequestFailedHeader", "Ex0B9BBB", false, true, Strings.ResourceManager, new object[]
			{
				group
			});
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000022A8 File Offset: 0x000004A8
		public static LocalizedString AutoGroupRequestApprovedHeader(string approver, string group)
		{
			return new LocalizedString("AutoGroupRequestApprovedHeader", "Ex6D1B7D", false, true, Strings.ResourceManager, new object[]
			{
				approver,
				group
			});
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000022DB File Offset: 0x000004DB
		public static LocalizedString AutoGroupRequestApprovedBody
		{
			get
			{
				return new LocalizedString("AutoGroupRequestApprovedBody", "Ex876BEB", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000022FC File Offset: 0x000004FC
		public static LocalizedString AutoGroupRequestApprovedSubject(string group)
		{
			return new LocalizedString("AutoGroupRequestApprovedSubject", "ExA17329", false, true, Strings.ResourceManager, new object[]
			{
				group
			});
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000232C File Offset: 0x0000052C
		public static LocalizedString ErrorTaskInvocationFailed(string user)
		{
			return new LocalizedString("ErrorTaskInvocationFailed", "ExFBD273", false, true, Strings.ResourceManager, new object[]
			{
				user
			});
		}

		// Token: 0x0600000E RID: 14 RVA: 0x0000235C File Offset: 0x0000055C
		public static LocalizedString AutoGroupRequestRejectedHeader(string group)
		{
			return new LocalizedString("AutoGroupRequestRejectedHeader", "Ex4F0764", false, true, Strings.ResourceManager, new object[]
			{
				group
			});
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000238C File Offset: 0x0000058C
		public static LocalizedString ErrorNoRBACRoleAssignment(string user)
		{
			return new LocalizedString("ErrorNoRBACRoleAssignment", "ExC8B4DD", false, true, Strings.ResourceManager, new object[]
			{
				user
			});
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000023BC File Offset: 0x000005BC
		public static LocalizedString AutoGroupRequestFailedBodyBadApprover(string group, string requester, string approver)
		{
			return new LocalizedString("AutoGroupRequestFailedBodyBadApprover", "ExC11FBB", false, true, Strings.ResourceManager, new object[]
			{
				group,
				requester,
				approver
			});
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000023F4 File Offset: 0x000005F4
		public static LocalizedString ErrorUserNotFound(string proxy)
		{
			return new LocalizedString("ErrorUserNotFound", "ExDDCC17", false, true, Strings.ResourceManager, new object[]
			{
				proxy
			});
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002424 File Offset: 0x00000624
		public static LocalizedString AutoGroupRequestExpiredBody(string group)
		{
			return new LocalizedString("AutoGroupRequestExpiredBody", "Ex4CD025", false, true, Strings.ResourceManager, new object[]
			{
				group
			});
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002453 File Offset: 0x00000653
		public static LocalizedString GetLocalizedString(Strings.IDs key)
		{
			return new LocalizedString(Strings.stringIDs[(uint)key], Strings.ResourceManager, new object[0]);
		}

		// Token: 0x04000001 RID: 1
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(2);

		// Token: 0x04000002 RID: 2
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.Approval.Applications.Resources.Strings", typeof(Strings).GetTypeInfo().Assembly);

		// Token: 0x02000003 RID: 3
		public enum IDs : uint
		{
			// Token: 0x04000004 RID: 4
			Semicolon = 2918013799U,
			// Token: 0x04000005 RID: 5
			AutoGroupRequestApprovedBody = 816183154U
		}

		// Token: 0x02000004 RID: 4
		private enum ParamIDs
		{
			// Token: 0x04000007 RID: 7
			AutoGroupRequestFailedSubject,
			// Token: 0x04000008 RID: 8
			AutoGroupRequestFailedBodyBadRequester,
			// Token: 0x04000009 RID: 9
			AutoGroupRequestExpiredSubject,
			// Token: 0x0400000A RID: 10
			AutoGroupRequestFailedBodyTaskError,
			// Token: 0x0400000B RID: 11
			AutoGroupRequestRejectedSubject,
			// Token: 0x0400000C RID: 12
			AutoGroupRequestRejectedBody,
			// Token: 0x0400000D RID: 13
			AutoGroupRequestFailedHeader,
			// Token: 0x0400000E RID: 14
			AutoGroupRequestApprovedHeader,
			// Token: 0x0400000F RID: 15
			AutoGroupRequestApprovedSubject,
			// Token: 0x04000010 RID: 16
			ErrorTaskInvocationFailed,
			// Token: 0x04000011 RID: 17
			AutoGroupRequestRejectedHeader,
			// Token: 0x04000012 RID: 18
			ErrorNoRBACRoleAssignment,
			// Token: 0x04000013 RID: 19
			AutoGroupRequestFailedBodyBadApprover,
			// Token: 0x04000014 RID: 20
			ErrorUserNotFound,
			// Token: 0x04000015 RID: 21
			AutoGroupRequestExpiredBody
		}
	}
}
