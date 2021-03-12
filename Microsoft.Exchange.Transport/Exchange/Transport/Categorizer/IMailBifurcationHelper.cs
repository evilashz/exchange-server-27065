using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x020001EB RID: 491
	internal interface IMailBifurcationHelper<T> where T : IEquatable<T>, IComparable<T>, new()
	{
		// Token: 0x060015DF RID: 5599
		bool GetBifurcationInfo(MailRecipient recipient, out T bifurcationInfo);

		// Token: 0x060015E0 RID: 5600
		TransportMailItem GenerateNewMailItem(IList<MailRecipient> newMailItemRecipients, T bifurcationInfo);

		// Token: 0x060015E1 RID: 5601
		bool NeedsBifurcation();
	}
}
