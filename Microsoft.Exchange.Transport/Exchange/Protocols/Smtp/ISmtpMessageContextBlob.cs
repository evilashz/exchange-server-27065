using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x02000414 RID: 1044
	internal interface ISmtpMessageContextBlob
	{
		// Token: 0x06003030 RID: 12336
		bool TryGetOrderedListOfBlobsToReceive(string mailCommandParameter, out MailCommandMessageContextParameters messageContextInfo);

		// Token: 0x06003031 RID: 12337
		List<SmtpMessageContextBlob> GetAdvertisedMandatoryBlobs(IEhloOptions ehloOptions);

		// Token: 0x17000E98 RID: 3736
		// (get) Token: 0x06003032 RID: 12338
		AdrcSmtpMessageContextBlob AdrcSmtpMessageContextBlob { get; }

		// Token: 0x17000E99 RID: 3737
		// (get) Token: 0x06003033 RID: 12339
		ExtendedPropertiesSmtpMessageContextBlob ExtendedPropertiesSmtpMessageContextBlob { get; }

		// Token: 0x17000E9A RID: 3738
		// (get) Token: 0x06003034 RID: 12340
		FastIndexSmtpMessageContextBlob FastIndexSmtpMessageContextBlob { get; }
	}
}
