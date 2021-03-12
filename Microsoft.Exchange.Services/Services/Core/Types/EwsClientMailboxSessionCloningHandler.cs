using System;
using System.Globalization;
using System.Security.Principal;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000894 RID: 2196
	// (Invoke) Token: 0x06003EC7 RID: 16071
	internal delegate MailboxSession EwsClientMailboxSessionCloningHandler(Guid mailboxGuid, CultureInfo userCulture, LogonType logonType, string userContextKey, ExchangePrincipal mailboxToAccess, IADOrgPerson person, ClientSecurityContext callerSecurityContext, GenericIdentity genericIdentity, bool unifiedLogon);
}
