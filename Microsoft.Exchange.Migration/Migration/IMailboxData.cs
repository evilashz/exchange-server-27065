using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200003A RID: 58
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IMailboxData : IMigrationSerializable
	{
		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000244 RID: 580
		string MailboxIdentifier { get; }

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000245 RID: 581
		MigrationUserRecipientType RecipientType { get; }

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000246 RID: 582
		OrganizationId OrganizationId { get; }

		// Token: 0x06000247 RID: 583
		TIdParameter GetIdParameter<TIdParameter>() where TIdParameter : IIdentityParameter;

		// Token: 0x06000248 RID: 584
		void Update(string identifier, OrganizationId organizationId);
	}
}
