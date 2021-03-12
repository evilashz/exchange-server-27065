using System;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020001A3 RID: 419
	[Serializable]
	public sealed class OwaInvalidCanary14Exception : OwaPermanentException
	{
		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x06000EE4 RID: 3812 RVA: 0x0005E480 File Offset: 0x0005C680
		public UserContextCookie UserContextCookie
		{
			get
			{
				return this.userContextCookie;
			}
		}

		// Token: 0x06000EE5 RID: 3813 RVA: 0x0005E488 File Offset: 0x0005C688
		public OwaInvalidCanary14Exception(UserContextCookie newUserContextCookie, string message = null) : base(message)
		{
			this.userContextCookie = newUserContextCookie;
		}

		// Token: 0x04000A1A RID: 2586
		private UserContextCookie userContextCookie;
	}
}
