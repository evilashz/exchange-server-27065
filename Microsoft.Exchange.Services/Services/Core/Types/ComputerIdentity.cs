using System;
using System.Security.Principal;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000727 RID: 1831
	internal class ComputerIdentity : ADIdentityInformation
	{
		// Token: 0x06003786 RID: 14214 RVA: 0x000C5974 File Offset: 0x000C3B74
		public ComputerIdentity(ADComputer computer)
		{
			this.computer = computer;
		}

		// Token: 0x17000D11 RID: 3345
		// (get) Token: 0x06003787 RID: 14215 RVA: 0x000C5983 File Offset: 0x000C3B83
		public ADComputer Computer
		{
			get
			{
				return this.computer;
			}
		}

		// Token: 0x17000D12 RID: 3346
		// (get) Token: 0x06003788 RID: 14216 RVA: 0x000C598B File Offset: 0x000C3B8B
		public override string SmtpAddress
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000D13 RID: 3347
		// (get) Token: 0x06003789 RID: 14217 RVA: 0x000C598E File Offset: 0x000C3B8E
		public override OrganizationId OrganizationId
		{
			get
			{
				return OrganizationId.ForestWideOrgId;
			}
		}

		// Token: 0x17000D14 RID: 3348
		// (get) Token: 0x0600378A RID: 14218 RVA: 0x000C5995 File Offset: 0x000C3B95
		public override SecurityIdentifier Sid
		{
			get
			{
				return this.computer.Sid;
			}
		}

		// Token: 0x17000D15 RID: 3349
		// (get) Token: 0x0600378B RID: 14219 RVA: 0x000C59A2 File Offset: 0x000C3BA2
		public override Guid ObjectGuid
		{
			get
			{
				return this.computer.Id.ObjectGuid;
			}
		}

		// Token: 0x0600378C RID: 14220 RVA: 0x000C59B4 File Offset: 0x000C3BB4
		public override IRecipientSession GetADRecipientSession()
		{
			return Directory.CreateRootADRecipientSession();
		}

		// Token: 0x0600378D RID: 14221 RVA: 0x000C59BB File Offset: 0x000C3BBB
		public override IRecipientSession GetGALScopedADRecipientSession(ClientSecurityContext clientSecurityContext)
		{
			return Directory.CreateRootADRecipientSession();
		}

		// Token: 0x04001EDC RID: 7900
		private ADComputer computer;
	}
}
