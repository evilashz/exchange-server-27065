using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.PswsClient;
using Microsoft.Exchange.Security.Authentication;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x02000289 RID: 649
	internal static class AuthenticatorFactory
	{
		// Token: 0x060010DF RID: 4319 RVA: 0x000525F6 File Offset: 0x000507F6
		internal static void SetCreateHook(Func<IAuthenticator> func)
		{
			AuthenticatorFactory.CreateHook = Hookable<Func<IAuthenticator>>.Create(true, func);
		}

		// Token: 0x060010E0 RID: 4320 RVA: 0x00052604 File Offset: 0x00050804
		internal static IAuthenticator Create()
		{
			if (AuthenticatorFactory.CreateHook != null)
			{
				return AuthenticatorFactory.CreateHook.Value();
			}
			return Authenticator.NetworkService;
		}

		// Token: 0x04000C7E RID: 3198
		private static Hookable<Func<IAuthenticator>> CreateHook;
	}
}
