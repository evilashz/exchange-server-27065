using System;
using System.Security.Principal;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Security.Authentication
{
	// Token: 0x0200063F RID: 1599
	internal class SidWithGroupsIdentity : ClientSecurityContextIdentity
	{
		// Token: 0x06001CF7 RID: 7415 RVA: 0x00034575 File Offset: 0x00032775
		internal SidWithGroupsIdentity(string name, string type, ClientSecurityContext clientSecurityContext) : base(name, type)
		{
			if (clientSecurityContext == null)
			{
				throw new ArgumentNullException("clientSecurityContext");
			}
			this.clientSecurityContext = clientSecurityContext;
		}

		// Token: 0x170007E9 RID: 2025
		// (get) Token: 0x06001CF8 RID: 7416 RVA: 0x00034594 File Offset: 0x00032794
		public override SecurityIdentifier Sid
		{
			get
			{
				return this.clientSecurityContext.UserSid;
			}
		}

		// Token: 0x06001CF9 RID: 7417 RVA: 0x000345A1 File Offset: 0x000327A1
		internal override ClientSecurityContext CreateClientSecurityContext()
		{
			return this.clientSecurityContext.Clone();
		}

		// Token: 0x04001D33 RID: 7475
		private ClientSecurityContext clientSecurityContext;
	}
}
