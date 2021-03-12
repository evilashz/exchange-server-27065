using System;
using System.Security.Principal;

namespace Microsoft.Exchange.Security.Authentication
{
	// Token: 0x02000BBC RID: 3004
	// (Invoke) Token: 0x06004070 RID: 16496
	internal delegate SecurityStatus ExternalLoginAuthentication(byte[] userid, byte[] password, out WindowsIdentity windowsIdentity, out IAccountValidationContext accountValidationContext);
}
