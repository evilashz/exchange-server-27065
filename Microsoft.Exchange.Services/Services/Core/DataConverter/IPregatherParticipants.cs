using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020000D5 RID: 213
	internal interface IPregatherParticipants
	{
		// Token: 0x060005CD RID: 1485
		void Pregather(StoreObject storeObject, List<Participant> participants);
	}
}
