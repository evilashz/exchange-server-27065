using System;
using System.Security.AccessControl;
using System.Security.Principal;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000007 RID: 7
	internal class AuthzAuthorizationWrapper : IAuthzAuthorization
	{
		// Token: 0x06000021 RID: 33 RVA: 0x00002410 File Offset: 0x00000610
		public int CheckGenericPermission(SecurityIdentifier sid, RawSecurityDescriptor rawSecurityDescriptor, AccessMask requestedAccess)
		{
			return AuthzAuthorization.CheckGenericPermission(sid, rawSecurityDescriptor, requestedAccess);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x0000241A File Offset: 0x0000061A
		public Permission CheckPermissions(IntPtr token, RawSecurityDescriptor rawSecurityDescriptor, SecurityIdentifier principalSelfSid)
		{
			return AuthzAuthorization.CheckPermissions(token, rawSecurityDescriptor, principalSelfSid);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002424 File Offset: 0x00000624
		public Permission CheckPermissions(SecurityIdentifier sid, RawSecurityDescriptor rawSecurityDescriptor, SecurityIdentifier principalSelfSid)
		{
			return AuthzAuthorization.CheckPermissions(sid, rawSecurityDescriptor, principalSelfSid);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x0000242E File Offset: 0x0000062E
		public bool CheckSingleExtendedRight(SecurityIdentifier sid, RawSecurityDescriptor rawSecurityDescriptor, Guid extendedRightGuid)
		{
			return AuthzAuthorization.CheckSingleExtendedRight(sid, rawSecurityDescriptor, extendedRightGuid);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002438 File Offset: 0x00000638
		public bool CheckSinglePermission(IntPtr token, RawSecurityDescriptor rawSecurityDescriptor, Permission permission)
		{
			return AuthzAuthorization.CheckSinglePermission(token, rawSecurityDescriptor, permission);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002442 File Offset: 0x00000642
		public bool CheckSinglePermission(IntPtr token, SecurityDescriptor securityDescriptor, Permission permission)
		{
			return AuthzAuthorization.CheckSinglePermission(token, securityDescriptor, permission);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x0000244C File Offset: 0x0000064C
		public bool CheckSinglePermission(IntPtr token, RawSecurityDescriptor rawSecurityDescriptor, Permission permission, SecurityIdentifier principalSelfSid)
		{
			return AuthzAuthorization.CheckSinglePermission(token, rawSecurityDescriptor, permission, principalSelfSid);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002458 File Offset: 0x00000658
		public bool CheckSinglePermission(IntPtr token, SecurityDescriptor securityDescriptor, Permission permission, SecurityIdentifier principalSelfSid)
		{
			return AuthzAuthorization.CheckSinglePermission(token, securityDescriptor, permission, principalSelfSid);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002464 File Offset: 0x00000664
		public bool CheckSinglePermission(SecurityIdentifier sid, bool isExchangeWellKnownSid, RawSecurityDescriptor rawSecurityDescriptor, Permission permission)
		{
			return AuthzAuthorization.CheckSinglePermission(sid, isExchangeWellKnownSid, rawSecurityDescriptor, permission);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002470 File Offset: 0x00000670
		public bool CheckSinglePermission(SecurityIdentifier sid, bool isExchangeWellKnownSid, RawSecurityDescriptor rawSecurityDescriptor, Permission permission, SecurityIdentifier principalSelfSid)
		{
			return AuthzAuthorization.CheckSinglePermission(sid, isExchangeWellKnownSid, rawSecurityDescriptor, permission, principalSelfSid);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x0000247E File Offset: 0x0000067E
		public bool CheckSinglePermission(SecurityIdentifier sid, bool isExchangeWellKnownSid, SecurityDescriptor securityDescriptor, Permission permission, SecurityIdentifier principalSelfSid)
		{
			return AuthzAuthorization.CheckSinglePermission(sid, isExchangeWellKnownSid, securityDescriptor, permission, principalSelfSid);
		}
	}
}
