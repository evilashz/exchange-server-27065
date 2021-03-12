using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Principal;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Principal
{
	// Token: 0x02000264 RID: 612
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IExchangePrincipal
	{
		// Token: 0x170007B6 RID: 1974
		// (get) Token: 0x060018B9 RID: 6329
		string PrincipalId { get; }

		// Token: 0x170007B7 RID: 1975
		// (get) Token: 0x060018BA RID: 6330
		ADObjectId ObjectId { get; }

		// Token: 0x170007B8 RID: 1976
		// (get) Token: 0x060018BB RID: 6331
		string LegacyDn { get; }

		// Token: 0x170007B9 RID: 1977
		// (get) Token: 0x060018BC RID: 6332
		string Alias { get; }

		// Token: 0x170007BA RID: 1978
		// (get) Token: 0x060018BD RID: 6333
		ADObjectId DefaultPublicFolderMailbox { get; }

		// Token: 0x170007BB RID: 1979
		// (get) Token: 0x060018BE RID: 6334
		SecurityIdentifier Sid { get; }

		// Token: 0x170007BC RID: 1980
		// (get) Token: 0x060018BF RID: 6335
		SecurityIdentifier MasterAccountSid { get; }

		// Token: 0x170007BD RID: 1981
		// (get) Token: 0x060018C0 RID: 6336
		IEnumerable<SecurityIdentifier> SidHistory { get; }

		// Token: 0x170007BE RID: 1982
		// (get) Token: 0x060018C1 RID: 6337
		IEnumerable<ADObjectId> Delegates { get; }

		// Token: 0x170007BF RID: 1983
		// (get) Token: 0x060018C2 RID: 6338
		IEnumerable<CultureInfo> PreferredCultures { get; }

		// Token: 0x170007C0 RID: 1984
		// (get) Token: 0x060018C3 RID: 6339
		RecipientType RecipientType { get; }

		// Token: 0x170007C1 RID: 1985
		// (get) Token: 0x060018C4 RID: 6340
		RecipientTypeDetails RecipientTypeDetails { get; }

		// Token: 0x170007C2 RID: 1986
		// (get) Token: 0x060018C5 RID: 6341
		bool? IsResource { get; }

		// Token: 0x170007C3 RID: 1987
		// (get) Token: 0x060018C6 RID: 6342
		ModernGroupObjectType ModernGroupType { get; }

		// Token: 0x170007C4 RID: 1988
		// (get) Token: 0x060018C7 RID: 6343
		IEnumerable<SecurityIdentifier> PublicToGroupSids { get; }

		// Token: 0x170007C5 RID: 1989
		// (get) Token: 0x060018C8 RID: 6344
		string ExternalDirectoryObjectId { get; }

		// Token: 0x170007C6 RID: 1990
		// (get) Token: 0x060018C9 RID: 6345
		IEnumerable<Guid> AggregatedMailboxGuids { get; }

		// Token: 0x170007C7 RID: 1991
		// (get) Token: 0x060018CA RID: 6346
		IMailboxInfo MailboxInfo { get; }

		// Token: 0x170007C8 RID: 1992
		// (get) Token: 0x060018CB RID: 6347
		IEnumerable<IMailboxInfo> AllMailboxes { get; }

		// Token: 0x170007C9 RID: 1993
		// (get) Token: 0x060018CC RID: 6348
		bool IsCrossSiteAccessAllowed { get; }

		// Token: 0x170007CA RID: 1994
		// (get) Token: 0x060018CD RID: 6349
		bool IsMailboxInfoRequired { get; }
	}
}
