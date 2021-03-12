using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x0200024C RID: 588
	public interface IMailboxLocationCollection
	{
		// Token: 0x06001CE6 RID: 7398
		IMailboxLocationInfo GetMailboxLocation(MailboxLocationType mailboxLocationType);

		// Token: 0x06001CE7 RID: 7399
		IList<IMailboxLocationInfo> GetMailboxLocations(MailboxLocationType mailboxLocationType);

		// Token: 0x06001CE8 RID: 7400
		IList<IMailboxLocationInfo> GetMailboxLocations();

		// Token: 0x06001CE9 RID: 7401
		IMailboxLocationInfo GetMailboxLocation(Guid mailboxGuid);

		// Token: 0x06001CEA RID: 7402
		void AddMailboxLocation(IMailboxLocationInfo mailboxLocation);

		// Token: 0x06001CEB RID: 7403
		void AddMailboxLocation(Guid mailboxGuid, ADObjectId databaseLocation, MailboxLocationType mailboxLocationType);

		// Token: 0x06001CEC RID: 7404
		void RemoveMailboxLocation(Guid mailboxGuid);

		// Token: 0x06001CED RID: 7405
		void Validate(List<ValidationError> errors);
	}
}
