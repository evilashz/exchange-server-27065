using System;
using System.Security.Principal;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Security.Authentication
{
	// Token: 0x02000040 RID: 64
	internal class WindowsTokenAccessor : CommonAccessTokenAccessor
	{
		// Token: 0x060001CE RID: 462 RVA: 0x0000CE31 File Offset: 0x0000B031
		private WindowsTokenAccessor(CommonAccessToken token) : base(token)
		{
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060001CF RID: 463 RVA: 0x0000CE3A File Offset: 0x0000B03A
		public override AccessTokenType TokenType
		{
			get
			{
				return AccessTokenType.Windows;
			}
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x0000CE40 File Offset: 0x0000B040
		public static WindowsTokenAccessor Create(WindowsIdentity windowsIdentity)
		{
			if (windowsIdentity == null)
			{
				throw new ArgumentNullException("windowsIdentity");
			}
			CommonAccessToken token = new CommonAccessToken(windowsIdentity);
			return new WindowsTokenAccessor(token);
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x0000CE6A File Offset: 0x0000B06A
		public static WindowsTokenAccessor Attach(CommonAccessToken token)
		{
			if (token == null)
			{
				throw new ArgumentNullException("token");
			}
			return new WindowsTokenAccessor(token);
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060001D2 RID: 466 RVA: 0x0000CE80 File Offset: 0x0000B080
		public WindowsAccessToken WindowsAccessToken
		{
			get
			{
				return base.Token.WindowsAccessToken;
			}
		}
	}
}
