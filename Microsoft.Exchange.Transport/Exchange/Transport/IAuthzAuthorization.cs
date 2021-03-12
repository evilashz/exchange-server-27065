using System;
using System.Security.AccessControl;
using System.Security.Principal;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000006 RID: 6
	internal interface IAuthzAuthorization
	{
		// Token: 0x06000016 RID: 22
		int CheckGenericPermission(SecurityIdentifier sid, RawSecurityDescriptor rawSecurityDescriptor, AccessMask requestedAccess);

		// Token: 0x06000017 RID: 23
		Permission CheckPermissions(IntPtr token, RawSecurityDescriptor rawSecurityDescriptor, SecurityIdentifier principalSelfSid);

		// Token: 0x06000018 RID: 24
		Permission CheckPermissions(SecurityIdentifier sid, RawSecurityDescriptor rawSecurityDescriptor, SecurityIdentifier principalSelfSid);

		// Token: 0x06000019 RID: 25
		bool CheckSingleExtendedRight(SecurityIdentifier sid, RawSecurityDescriptor rawSecurityDescriptor, Guid extendedRightGuid);

		// Token: 0x0600001A RID: 26
		bool CheckSinglePermission(IntPtr token, RawSecurityDescriptor rawSecurityDescriptor, Permission permission);

		// Token: 0x0600001B RID: 27
		bool CheckSinglePermission(IntPtr token, SecurityDescriptor securityDescriptor, Permission permission);

		// Token: 0x0600001C RID: 28
		bool CheckSinglePermission(IntPtr token, RawSecurityDescriptor rawSecurityDescriptor, Permission permission, SecurityIdentifier principalSelfSid);

		// Token: 0x0600001D RID: 29
		bool CheckSinglePermission(IntPtr token, SecurityDescriptor securityDescriptor, Permission permission, SecurityIdentifier principalSelfSid);

		// Token: 0x0600001E RID: 30
		bool CheckSinglePermission(SecurityIdentifier sid, bool isExchangeWellKnownSid, RawSecurityDescriptor rawSecurityDescriptor, Permission permission);

		// Token: 0x0600001F RID: 31
		bool CheckSinglePermission(SecurityIdentifier sid, bool isExchangeWellKnownSid, RawSecurityDescriptor rawSecurityDescriptor, Permission permission, SecurityIdentifier principalSelfSid);

		// Token: 0x06000020 RID: 32
		bool CheckSinglePermission(SecurityIdentifier sid, bool isExchangeWellKnownSid, SecurityDescriptor securityDescriptor, Permission permission, SecurityIdentifier principalSelfSid);
	}
}
