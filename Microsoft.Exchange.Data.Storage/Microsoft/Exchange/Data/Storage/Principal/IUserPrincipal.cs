using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Principal
{
	// Token: 0x02000267 RID: 615
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IUserPrincipal : IExchangePrincipal
	{
		// Token: 0x170007E4 RID: 2020
		// (get) Token: 0x0600193C RID: 6460
		string UserPrincipalName { get; }

		// Token: 0x170007E5 RID: 2021
		// (get) Token: 0x0600193D RID: 6461
		SmtpAddress WindowsLiveId { get; }

		// Token: 0x170007E6 RID: 2022
		// (get) Token: 0x0600193E RID: 6462
		NetID NetId { get; }
	}
}
