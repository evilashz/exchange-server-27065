using System;

namespace Microsoft.Exchange.Extensibility.Internal
{
	// Token: 0x0200004C RID: 76
	internal interface IMailRecipientCollectionFacade
	{
		// Token: 0x060002F0 RID: 752
		void Add(string smtpAddress);

		// Token: 0x060002F1 RID: 753
		void AddWithoutDsnRequested(string smtpAddress);
	}
}
