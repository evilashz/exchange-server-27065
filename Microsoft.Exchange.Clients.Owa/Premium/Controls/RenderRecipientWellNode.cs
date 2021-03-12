using System;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x02000406 RID: 1030
	// (Invoke) Token: 0x0600255C RID: 9564
	internal delegate bool RenderRecipientWellNode(TextWriter writer, UserContext userContext, string displayName, string smtpAddress, string routingAddress, string routingType, string alias, AddressOrigin addressOrigin, int recipientFlags, StoreObjectId storeObjectId, EmailAddressIndex emailAddressIndex, ADObjectId adObjectId, RecipientWellNode.RenderFlags flags, string sipUri, string mobilePhoneNumber);
}
