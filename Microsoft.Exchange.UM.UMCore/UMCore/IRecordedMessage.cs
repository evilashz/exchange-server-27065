using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x0200004C RID: 76
	internal interface IRecordedMessage
	{
		// Token: 0x06000327 RID: 807
		void DoSubmit(Importance importance, bool markPrivate, Stack<Participant> recips);

		// Token: 0x06000328 RID: 808
		void DoSubmit(Importance importance);

		// Token: 0x06000329 RID: 809
		void DoPostSubmit();
	}
}
