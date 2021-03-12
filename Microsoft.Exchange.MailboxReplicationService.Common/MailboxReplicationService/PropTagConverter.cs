using System;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200022D RID: 557
	internal class PropTagConverter : IDataConverter<PropTag, int>
	{
		// Token: 0x06001DE3 RID: 7651 RVA: 0x0003DA9F File Offset: 0x0003BC9F
		PropTag IDataConverter<PropTag, int>.GetNativeRepresentation(int ptag)
		{
			return (PropTag)ptag;
		}

		// Token: 0x06001DE4 RID: 7652 RVA: 0x0003DAA2 File Offset: 0x0003BCA2
		int IDataConverter<PropTag, int>.GetDataRepresentation(PropTag ptag)
		{
			return (int)ptag;
		}
	}
}
