using System;
using System.Threading.Tasks;
using Microsoft.Exchange.Transport;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x020004B5 RID: 1205
	internal interface ISmtpInMailItemStorage
	{
		// Token: 0x06003661 RID: 13921
		IAsyncResult BeginCommitMailItem(TransportMailItem mailItem, AsyncCallback callback, object state);

		// Token: 0x06003662 RID: 13922
		bool EndCommitMailItem(TransportMailItem mailItem, IAsyncResult asyncResult, out Exception exception);

		// Token: 0x06003663 RID: 13923
		Task CommitMailItemAsync(TransportMailItem mailItem);
	}
}
