using System;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020000D4 RID: 212
	[OwaEventStruct("recip")]
	internal sealed class RecipientInfoAC
	{
		// Token: 0x04000513 RID: 1299
		[OwaEventField("dn", false, null)]
		public string DisplayName;

		// Token: 0x04000514 RID: 1300
		[OwaEventField("sa", false, "")]
		public string SmtpAddress;

		// Token: 0x04000515 RID: 1301
		[OwaEventField("ra", false, "")]
		public string RoutingAddress;

		// Token: 0x04000516 RID: 1302
		[OwaEventField("al", false, null)]
		public string Alias;

		// Token: 0x04000517 RID: 1303
		[OwaEventField("rt", false, null)]
		public string RoutingType;

		// Token: 0x04000518 RID: 1304
		[OwaEventField("ao", false, null)]
		public AddressOrigin AddressOrigin;

		// Token: 0x04000519 RID: 1305
		[OwaEventField("rf", false, null)]
		public int RecipientFlags;

		// Token: 0x0400051A RID: 1306
		[OwaEventField("id", false, null)]
		public string ItemId;

		// Token: 0x0400051B RID: 1307
		[OwaEventField("ei", false, null)]
		public EmailAddressIndex EmailAddressIndex;

		// Token: 0x0400051C RID: 1308
		[OwaEventField("uri", true, null)]
		public string SipUri;

		// Token: 0x0400051D RID: 1309
		[OwaEventField("mo", true, null)]
		public string MobilePhoneNumber;
	}
}
