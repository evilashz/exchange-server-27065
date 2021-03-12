using System;
using System.Security.Principal;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020006A5 RID: 1701
	internal abstract class ADIdentityInformation
	{
		// Token: 0x17000C13 RID: 3091
		// (get) Token: 0x06003471 RID: 13425
		public abstract string SmtpAddress { get; }

		// Token: 0x17000C14 RID: 3092
		// (get) Token: 0x06003472 RID: 13426
		public abstract SecurityIdentifier Sid { get; }

		// Token: 0x17000C15 RID: 3093
		// (get) Token: 0x06003473 RID: 13427
		public abstract Guid ObjectGuid { get; }

		// Token: 0x17000C16 RID: 3094
		// (get) Token: 0x06003474 RID: 13428
		public abstract OrganizationId OrganizationId { get; }

		// Token: 0x17000C17 RID: 3095
		// (get) Token: 0x06003475 RID: 13429 RVA: 0x000BCF37 File Offset: 0x000BB137
		public string OrganizationPrefix
		{
			get
			{
				if (this.OrganizationId.OrganizationalUnit != null)
				{
					return this.OrganizationId.OrganizationalUnit.Name;
				}
				return string.Empty;
			}
		}

		// Token: 0x06003476 RID: 13430
		public abstract IRecipientSession GetADRecipientSession();

		// Token: 0x06003477 RID: 13431
		public abstract IRecipientSession GetGALScopedADRecipientSession(ClientSecurityContext clientSecurityContext);
	}
}
